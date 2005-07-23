/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   peter@drealm.info                                                     *
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *                                                                         *
 *   This program is free software; you can redistribute it and/or modify  *
 *   it under the terms of the GNU General Public License as published by  *
 *   the Free Software Foundation; either version 2 of the License, or     *
 *   (at your option) any later version.                                   *
 *                                                                         *
 *   This program is distributed in the hope that it will be useful,       *
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of        *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the         *
 *   GNU General Public License for more details.                          *
 *                                                                         *
 *   You should have received a copy of the GNU General Public License     *
 *   along with this program; if not, write to the                         *
 *   Free Software Foundation, Inc.,                                       *
 *   59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.             *
 ***************************************************************************/
using System;
using System.Collections;
using SimPe.Interfaces.Plugin;

namespace SimPe.PackedFiles.Wrapper
{
	/// <summary>
	/// This is the actual FileWrapper
	/// </summary>
	/// <remarks>
	/// The wrapper is used to (un)serialize the Data of a file into it's Attributes. So Basically it reads 
	/// a BinaryStream and translates the data into some userdefine Attributes.
	/// </remarks>
	public class Ttab
		: AbstractWrapper				//Implements some of the default Behaviur of a Handler, you can Implement yourself if you want more flexibility!
		, IFileWrapper					//This Interface is used when loading a File
		, IFileWrapperSaveExtension		//This Interface (if available) will be used to store a File
		//,IPackedFileProperties		//This Interface can be used by thirdparties to retrive the FIleproperties, however you don't have to implement it!
	{
		#region Attributes
		/// <summary>
		/// Indicates the data content of the wrapper (packed file) has changed
		/// </summary>
		public event EventHandler WrapperChanged;
		/// <summary>
		/// Indicates a wrapper routine is updating the wrapper and will generate the WrapperChanged event
		/// </summary>
		private bool internalchg = false;
		/// <summary>
		/// Contains the Filename
		/// </summary>
		private byte[] filename = new byte[64];
		/// <summary>
		/// Header of the File
		/// </summary>
		private uint[] header = new uint[3];
		/// <summary>
		/// Number of Ttab Items in file
		/// </summary>
		private ushort itemCount = 0;
		/// <summary>
		/// Items stored in the File
		/// </summary>
		private ArrayList items = null;
		/// <summary>
		/// Unknown Data following the TTAB
		/// </summary>
		private byte[] footer = new byte[0];
		/// <summary>
		/// Contains an Opcode Provider
		/// </summary>
		private SimPe.Interfaces.Providers.IOpcodeProvider opcodes = null;
		/// <summary>
		/// Contains a valid String Resource that describes the Function Entries
		/// </summary>
		private Str strres = null;
		#endregion

		#region Accessor methods
		/// <summary>
		/// Returns the Filename
		/// </summary>
		public string FileName 
		{
			get { return Helper.ToString(filename); }
			set 
			{
				if (!Helper.ToString(filename).Equals(value))
				{
					filename = Helper.ToBytes(value, 0x40);
					OnWrapperChanged(new EventArgs());
				}
			}
		}

		/// <summary>
		/// Returns / Sets the Format this File is in
		/// </summary>
		public uint Format 
		{
			get { return header[1]; }
			set
			{
				if (header[1] != value )
				{
					header[1] = value;
					OnWrapperChanged(new EventArgs());
				}
			}
		}

		/// <summary>
		/// Returns the number of Ttab Items in the file
		/// </summary>
		public int ItemCount
		{
			get { return items.Count; }
		}
		/// <summary>
		/// Returns the Item
		/// </summary>
		public TtabItem this[int index]
		{
			get { return (TtabItem)items[index]; }
		}

		/// <summary>
		/// Returns the used Opcode Provider
		/// </summary>
		internal SimPe.Interfaces.Providers.IOpcodeProvider Opcodes 
		{
			get { return opcodes; }
		}

		/// <summary>
		/// Returns the describing String Resource
		/// </summary>
		internal PackedFiles.Wrapper.Str StringResource
		{
			get 
			{
				if (strres==null) 
				{
					strres = new PackedFiles.Wrapper.Str();
					if ((Package!=null) && (FileDescriptor!=null)) 
					{
						Interfaces.Files.IPackedFileDescriptor pfd = Package.FindFile(
							Data.MetaData.PIE_STRING_FILE, //Pie String File
							0, 
							FileDescriptor.Group,
							FileDescriptor.Instance
							);

						if (pfd!=null) 
						{
							strres.ProcessData(pfd, Package);
						}
					} 
					else 
					{
						return null;
					}		
				}

				return strres;
			}
		}

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public Ttab(SimPe.Interfaces.Providers.IOpcodeProvider opcodes) : base()
		{
			this.opcodes = opcodes;
		}


		internal virtual void OnWrapperChanged(EventArgs e)
		{
			this.Changed = true;

			if (internalchg) return;
			if (WrapperChanged != null) 
			{
				WrapperChanged(this, e);
			}
		}

		public int AddItem()
		{
			int val = items.Add(new TtabItem(this));
			OnWrapperChanged(new EventArgs());
			return val;
		}

		public void RemoveAt(int index)
		{
			items.RemoveAt(index);
			OnWrapperChanged(new EventArgs());
		}

		
		#region AbstractWrapper Member
		protected override IPackedFileUI CreateDefaultUIHandler()
		{
			return new UserInterface.TtabForm();
		}

		/// <summary>
		/// Returns a Human Readable Description of this Wrapper
		/// </summary>
		/// <returns>Human Readable Description</returns>
		protected override IWrapperInfo CreateWrapperInfo()
		{
			///
			/// TODO: Change the Description passed here
			/// 
			return new AbstractWrapperInfo(
				"PJSE TTAB Wrapper",
				"Peter L Jones",
				"---",
				1
				); 
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		protected override void Unserialize(System.IO.BinaryReader reader)
		{
			// in case we give up...
			itemCount = 0;
			items = null;
			footer = new byte[0];
			strres = null;

			filename = reader.ReadBytes(0x40);

			header = new uint[3];
			header[0] = reader.ReadUInt32();
			if (header[0] != 0xffffffff)
				return;
			header[1] = reader.ReadUInt32();
			header[2] = reader.ReadUInt32();

			itemCount = reader.ReadUInt16();

			TtabItem[] ti = new TtabItem[itemCount];
			items = new ArrayList(ti);
			for (int i = 0; i < itemCount; i++)
				items[i] = new TtabItem(this, reader);

			footer = reader.ReadBytes((int)(reader.BaseStream.Length - reader.BaseStream.Position));
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		protected override void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(filename);
			writer.Write(header[0]);
			writer.Write(header[1]);
			writer.Write(header[2]);

			writer.Write((ushort)items.Count);

			for (int i = 0; i < items.Count; i++)
				if (items[i] != null) ((TtabItem)items[i]).Serialize(writer);

			writer.Write(footer);
		}
		#endregion

		#region IWrapper member
		public override bool CheckVersion(uint version) 
		{
			if ( (version==0012) //0.00
				|| (version==0013) //0.10
				) 
			{
				return true;
			}

			return false;
		}
		#endregion

		#region IFileWrapperSaveExtension Member		
		//all covered by Serialize()
		#endregion

		#region IFileWrapper Member

		/// <summary>
		/// Returns the Signature that can be used to identify Files processable with this Plugin
		/// </summary>
		public byte[] FileSignature
		{
			get
			{
				return new byte[0];
			}
		}

		/// <summary>
		/// Returns a list of File Type this Plugin can process
		/// </summary>
		public uint[] AssignableTypes
		{
			get
			{
				uint[] types = {0x54544142}; //handles the TTAB File
				return types;
			}
		}

		#endregion		
	}


	/// <summary>
	/// An Item stored in an TTAB
	/// </summary>
	public class TtabItem : BhavBaseItem
	{
		#region Attributes
		private ushort action = 0;
		private ushort guard = 0;
		//private int[] counts = null;
		private TtabFlags flags = null;
		private ushort flags2 = 0;
		private uint strindex = 0;
		private uint attenuationcode = 0;
		private uint attenuationvalue = 0;
		private uint autonomy = 0;
		private uint joinindex = 0;
		private uint res5 = 0;
		private uint res6 = 0;
		private float res7 = 0f;
		private uint res8 = 0;
		private ushort res9 = 0;
		private ArrayList groups = null;
		private Ttab parent = null;
		#endregion

		#region Accessor Methods
		public ushort Action
		{
			get {return action; }
			set {
				if (action != value)
				{
					action = value;
					parent.OnWrapperChanged(new EventArgs());
				}
			}
		}

		public string ActionName { get { return this.OpcodeName(this.action); } }

		public ushort Guardian
		{
			get {return guard; }
			set
			{
				if (guard != value)
				{
					guard = value;
					parent.OnWrapperChanged(new EventArgs());
				}
			}
		}		

		public string GuardianName { get { return this.OpcodeName(this.guard); } }

		public TtabFlags Flags
		{
			get {return flags; }
			set
			{
				if (flags != value)
				{
					flags = value;
					parent.OnWrapperChanged(new EventArgs());
				}
			}
		}

		public ushort Flags2
		{
			get {return flags2; }
			set
			{
				if (flags2 != value)
				{
					flags2 = value;
					parent.OnWrapperChanged(new EventArgs());
				}
			}
		}

		public uint StringIndex
		{
			get {return strindex; }
			set
			{
				if (strindex != value)
				{
					strindex = value;
					parent.OnWrapperChanged(new EventArgs());
				}
			}
		}

		public uint AttenuationCode
		{
			get {return attenuationcode; }
			set
			{
				if (attenuationcode != value)
				{
					attenuationcode = value;
					parent.OnWrapperChanged(new EventArgs());
				}
			}
		}

		public uint AttenuationValue
		{
			get {return attenuationvalue; }
			set
			{
				if (attenuationvalue != value)
				{
					attenuationvalue = value;
					parent.OnWrapperChanged(new EventArgs());
				}
			}
		}		

		public uint Autonomy
		{
			get {return autonomy; }
			set
			{
				if (autonomy != value)
				{
					autonomy = value;
					parent.OnWrapperChanged(new EventArgs());
				}
			}
		}

		public uint JoinIndex
		{
			get {return joinindex; }
			set
			{
				if (joinindex != value)
				{
					joinindex = value;
					parent.OnWrapperChanged(new EventArgs());
				}
			}
		}

		public uint Res5
		{
			get {return res5; }
			set
			{
				if (res5 != value)
				{
					res5 = value;
					parent.OnWrapperChanged(new EventArgs());
				}
			}
		}

		public uint Res6
		{
			get {return res6; }
			set
			{
				if (res6 != value)
				{
					res6 = value;
					parent.OnWrapperChanged(new EventArgs());
				}
			}
		}

		public float Res7
		{
			get {return res7; }
			set
			{
				if (res7.Equals(value))
				{
					res7 = value;
					parent.OnWrapperChanged(new EventArgs());
				}
			}
		}

		public uint Res8
		{
			get {return res8; }
			set
			{
				if (res8 != value)
				{
					res8 = value;
					parent.OnWrapperChanged(new EventArgs());
				}
			}
		}

		public ushort Res9
		{
			get {return res9; }
			set
			{
				if (res9 != value)
				{
					res9 = value;
					parent.OnWrapperChanged(new EventArgs());
				}
			}
		}

		public int nrGroups { get { return groups.Count; } }
		public int[] nrMotives
		{
			get
			{
				int[] count = new int[groups.Count];
				for (int i = 0; i < groups.Count; i++)
				{
					count[i] = ((ArrayList)groups[i]).Count;
				}
				return count;
			}
		}
		public short this[int mg, int m, int i]
		{
			get
			{
				ArrayList gg = (ArrayList)groups[mg];
				short[] hh = (short[])gg[m];
				return hh[i];
			}
			set
			{
				ArrayList gg = (ArrayList)groups[mg];
				short[] hh = (short[])gg[m];
				if (hh[i] != value)
				{
					hh[i] = value;
					parent.OnWrapperChanged(new EventArgs());
				}
			}
		}
		public SimPe.Interfaces.Providers.IOpcodeProvider Opcodes { get { return parent.Opcodes; } }

		/// <summary>
		/// Returns the Name of this Item according to the Pie Strings File
		/// </summary>
		public string Name
		{
			get 
			{
				try 
				{
					if (parent==null) return Localization.Manager.GetString("unknown");
					if (parent.StringResource == null) return Localization.Manager.GetString("unknown");

					PackedFiles.Wrapper.StrItem item = parent.StringResource[0x1, (int)StringIndex];
					if (item==null) return Localization.Manager.GetString("unknown");
					return item.Title;
				} 
				catch (Exception) 
				{ 
					return Localization.Manager.GetString("unk"); 
				}
			}
		}

		/// <summary>
		/// Returns the Name of an Opcode in the Context of this Interaction
		/// </summary>
		/// <param name="opcode">The Opcode</param>
		/// <returns>The Name</returns>
		protected string OpcodeName(ushort opcode) 
		{
			Bhav bhav = new Bhav(parent.Opcodes);
			bhav.Package = parent.Package;
			SimPe.Packages.PackedFileDescriptor pfd = new SimPe.Packages.PackedFileDescriptor();
			pfd.Group = parent.FileDescriptor.Group;
			bhav.FileDescriptor = pfd;

			InstructionName name = new InstructionName(bhav);
			return name.OpcodeName(opcode, null);
		}

		#endregion

		public TtabItem(Ttab parent)
		{
			this.parent = parent;
			flags = new TtabFlags(parent, 0);

			int[] counts;
			if (parent.Format<0x44) counts = new int[1];
			else counts = new int[7];

			groups = new ArrayList(counts.Length);
			for (int j=0; j < counts.Length; j++)
			{
				counts[j] = 16;
				groups.Add(new ArrayList(16));
				for (int i=0; i < counts[j]; i++) 
				{
					((ArrayList)groups[j]).Add(new short[3]);
				}
			}
		}

		public TtabItem(Ttab parent, System.IO.BinaryReader reader)
		{
			this.parent = parent;
			Unserialize(reader);
		}


		public override string ToString()
		{
			return "0x"+StringIndex.ToString("X")+": "+Name;// + " ("+this.ActionName+")";
		}


		/// <summary>
		/// Reads Data from the Stream
		/// </summary>
		/// <param name="reader"></param>
		internal void Unserialize(System.IO.BinaryReader reader)
		{
			action = reader.ReadUInt16();
			guard = reader.ReadUInt16();

			int[] counts;
			if (parent.Format<0x44) counts = new int[1];
			else counts = new int[7];
			for (int i=0; i<counts.Length; i++)
				counts[i] = reader.ReadInt32();

			flags = new TtabFlags(parent, reader.ReadUInt16());
			flags2 = reader.ReadUInt16();

			strindex = reader.ReadUInt32();
			attenuationcode = reader.ReadUInt32();
			attenuationvalue = reader.ReadUInt32();
			autonomy = reader.ReadUInt32();
			joinindex = reader.ReadUInt32();

			if (parent.Format >0x44) 
			{
				if (parent.Format >= 0x46)
				{
					res5 = reader.ReadUInt32();
					if (parent.Format >= 0x4a) 
					{
						res6 = reader.ReadUInt32();
						if (parent.Format >= 0x4c)
						{
							res7 = reader.ReadSingle(); //float
							res8 = reader.ReadUInt32();
						}
					}
				}
				res9 = reader.ReadUInt16();
			}

			groups = new ArrayList(counts.Length);
			for (int k=0; k < counts.Length; k++) 
			{
				int g = groups.Add(new ArrayList((int)counts[k]));
				ArrayList gg = (ArrayList)groups[g];
				for (int i=0; i < counts[k]; i++) 
				{
					int h = gg.Add(new short[3]);
					short[] item = (short[])gg[h];
					for (int j = 0; j < item.Length; j++)
					{
						item[j] = reader.ReadInt16();
					}
				}
			}
		}

		/// <summary>
		/// Writes Data to the Stream
		/// </summary>
		/// <param name="reader"></param>
		internal void Serialize(System.IO.BinaryWriter writer)
		{
			int nrGroups;
			if (parent.Format<0x44) nrGroups = 1;
			else nrGroups = 7;

			writer.Write(action);
			writer.Write(guard);

			for (int i=0; i < nrGroups; i++) writer.Write(((ArrayList)groups[i]).Count);

			writer.Write(flags.Value);
			writer.Write(flags2);
			writer.Write(strindex);
			writer.Write(attenuationcode);
			writer.Write(attenuationvalue);
			writer.Write(autonomy);
			writer.Write(joinindex);

			if (parent.Format >0x44) 
			{
				if (parent.Format >= 0x46)
				{
					writer.Write(res5);
					if (parent.Format >= 0x4a) 
					{
						writer.Write(res6);
						if (parent.Format >= 0x4c)
						{
							writer.Write(res7);
							writer.Write(res8);
						}
					}
				}
				writer.Write(res9);
			}

			for (int k=0; k < nrGroups; k++) 
			{
				for (int i=0; i < ((ArrayList)groups[k]).Count; i++) 
				{
					short[] item = (short[])((ArrayList)groups[k])[i];
					for (int j = 0; j < item.Length; j++)
						writer.Write(item[j]);
				}
			}
		}

	}

	public class TtabFlags : FlagBase
	{
		private Ttab parent;
		public TtabFlags(Ttab parent, ushort flags) : base(flags) { this.parent = parent; }

		public new ushort Value
		{
			get { return base.Value; }
			set
			{
				if (base.Value != value)
				{
					base.Value = value;
					parent.OnWrapperChanged(new EventArgs());
				}
			}
		}

		public bool ByVisitors
		{
			get { return GetBit(0); }
			set { SetBit(0, value); parent.OnWrapperChanged(new EventArgs()); }
		}

		public bool Joinable
		{
			get { return GetBit(1); }
			set { SetBit(1, value); parent.OnWrapperChanged(new EventArgs()); }
		}

		public bool RunImmediately
		{
			get { return GetBit(2); }
			set { SetBit(2, value); parent.OnWrapperChanged(new EventArgs()); }
		}

		public bool AvailConsecutive
		{
			get { return GetBit(3); }
			set { SetBit(3, value); parent.OnWrapperChanged(new EventArgs()); }
		}

		public bool ByChildren
		{
			get { return !GetBit(4); }
			set { SetBit(4, !value); parent.OnWrapperChanged(new EventArgs()); }
		}

		public bool ByDemoChild
		{
			get { return !GetBit(5); }
			set { SetBit(5, !value); parent.OnWrapperChanged(new EventArgs()); }
		}

		public bool ByAdults
		{
			get { return !GetBit(6); }
			set { SetBit(6, !value); parent.OnWrapperChanged(new EventArgs()); }
		}

		public bool DebugMenu
		{
			get { return GetBit(7); }
			set { SetBit(7, value); parent.OnWrapperChanged(new EventArgs()); }
		}

		public bool AutoFirstSelect
		{
			get { return GetBit(8); }
			set { SetBit(8, value); parent.OnWrapperChanged(new EventArgs()); }
		}

		public bool ByToddlers
		{
			get { return GetBit(9); }
			set { SetBit(9, value); parent.OnWrapperChanged(new EventArgs()); }
		}

		public bool ByElders
		{
			get { return GetBit(10); }
			set { SetBit(10, value); parent.OnWrapperChanged(new EventArgs()); }
		}

		public bool ByTeens
		{
			get { return GetBit(11); }
			set { SetBit(11, value); parent.OnWrapperChanged(new EventArgs()); }
		}

		public bool Unknown1
		{
			get { return GetBit(12); }
			set { SetBit(12, value); parent.OnWrapperChanged(new EventArgs()); }
		}

		public bool Unknown2
		{
			get { return GetBit(13); }
			set { SetBit(13, value); parent.OnWrapperChanged(new EventArgs()); }
		}

		public bool Unknown3
		{
			get { return GetBit(14); }
			set { SetBit(14, value); parent.OnWrapperChanged(new EventArgs()); }
		}

		public bool Unknown4
		{
			get { return GetBit(15); }
			set { SetBit(15, value); parent.OnWrapperChanged(new EventArgs()); }
		}
	}

	/// <summary>
	/// Names for the Ttab Motive Groups
	/// </summary>
	public enum TtabMotives : int
	{
		Adult = 0x00,
		Child = 0x01,
		Teen = 0x02,
		Toddler = 0x03,
		Elder = 0x04,
		Unknown = 0x05,
		Animals = 0x06
	}
}
