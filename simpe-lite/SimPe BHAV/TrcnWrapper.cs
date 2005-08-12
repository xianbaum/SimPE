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
	public class Trcn
		: AbstractWrapper				//Implements some of the default Behaviur of a Handler, you can Implement yourself if you want more flexibility!
		, IFileWrapper					//This Interface is used when loading a File
		, IFileWrapperSaveExtension		//This Interface (if available) will be used to store a File
		//,IPackedFileProperties		//This Interface can be used by thirdparties to retrive the FIleproperties, however you don't have to implement it!
	{
		#region Attributes
		/// <summary>
		/// Contains the Filename
		/// </summary>
		private byte[] filename = new byte[64];
		/// <summary>
		/// Header of the File
		/// </summary>
		private uint[] header = { 0x5452434e, 0x0000004e, 0x00000000 };
		/// <summary>
		/// Items stored in the File
		/// </summary>
		private TrcnItemArrayList items = new TrcnItemArrayList();

		/// <summary>
		/// Indicates the data content of the wrapper (packed file) has changed
		/// </summary>
		public event EventHandler WrapperChanged;
		/// <summary>
		/// Indicates a wrapper routine is updating the wrapper and will generate the WrapperChanged event
		/// </summary>
		private bool internalchg = false;
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
					OnWrapperChanged();
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
					OnWrapperChanged();
				}
			}
		}

		/// <summary>
		/// Returns the number of Trcn Items in the file
		/// </summary>
		public int Count
		{
			get { return items.Count; }
		}
		/// <summary>
		/// Returns the Item
		/// </summary>
		public TrcnItem this[int index]
		{
			get { return (TrcnItem)items[index]; }
		}

		public string this[uint bcon]
		{
			get
			{
				foreach (TrcnItem i in items)
					if (i.Constant == bcon)
						return i.Label;
				return null;
			}
		}

		public int Add(TrcnItem item)
		{
			item.Parent = this;
			int result = items.Add(item);
			OnWrapperChanged();
			return result;
		}

		public int Add(uint bcon, string label) { return this.Add(new TrcnItem(this, bcon, label)); }


		public void Remove(TrcnItem item) { this.RemoveAt(items.IndexOf(item)); }

		public void RemoveAt(int index)
		{
			if (index < 0 || index >= items.Count) return;

			items.RemoveAt(index);
			OnWrapperChanged();
		}

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public Trcn() : base()
		{
		}


		internal virtual void OnWrapperChanged()
		{
			this.Changed = true;

			if (internalchg) return;
			if (WrapperChanged != null) 
			{
				WrapperChanged(this, new EventArgs());
			}
		}

		
		#region AbstractWrapper Member
		protected override IPackedFileUI CreateDefaultUIHandler()
		{
			return new UserInterface.TrcnForm();
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
				"PJSE TRCN Wrapper",
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
			filename = reader.ReadBytes(0x40);
			this.header[0] = reader.ReadUInt32();
			this.header[1] = reader.ReadUInt32();
			this.header[2] = reader.ReadUInt32();
			int count = reader.ReadInt32();
			items = new TrcnItemArrayList(count);

			for (int i=0; i < count; i++) 
				items.Add(new TrcnItem(this, reader));
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
			writer.Write(this.header[0]);
			writer.Write(this.header[1]);
			writer.Write(this.header[2]);
			writer.Write((uint)items.Count);
 
			for (int i=0; i < items.Count; i++) ((TrcnItem)items[i]).Serialize(writer);
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
				uint[] types = {0x5452434E}; //handles the TRCN File
				return types;
			}
		}

		#endregion		

		#region TrcnItemArrayList
		private class TrcnItemArrayList : ArrayList
		{
			public TrcnItemArrayList() : base() { }

			public TrcnItemArrayList(TrcnItem[] c) : base(c) { }

			public TrcnItemArrayList(int capacity) : base(capacity) { }

			public int Add(TrcnItem item) { return base.Add(item); }

			public void AddRange(TrcnItem[] c) { base.AddRange(c); }

			public void Insert(int index, TrcnItem item) { base.Insert(index, item); }

			public void SetRange(int index, TrcnItem[] c) { base.SetRange(index, c); }

			public new TrcnItem this[int index]
			{
				get { return (TrcnItem)base[index]; }
				set { base[index] = value; }
			}

		}

		#endregion
	}


	/// <summary>
	/// An Item stored in an TRCN
	/// </summary>
	public class TrcnItem
	{
		#region Attributes
		private uint used = 0x00000000;
		private uint bcon = 0x00000000;
		private string label = "";
		private ushort def = 0x0000;
		private ushort min = 0x0000;
		private ushort max = 0x0000;

		private Trcn parent;
		#endregion

		#region Accessor methods
		public uint Constant
		{
			get { return bcon; }
			set 
			{
				if (bcon != value)
				{
					bcon = value;
					parent.OnWrapperChanged();
				}
			}
		}
		public string Label 
		{
			get { return label; }
			set 
			{
				if (label != value)
				{
					label = value;
					parent.OnWrapperChanged();
				}
			}
		}

		public Trcn Parent
		{
			get { return parent; }
			set { parent = value; } // parent not part of wrapper
		}
		#endregion

		public TrcnItem(Trcn parent)
		{
			this.parent = parent;
		}

		public TrcnItem(Trcn parent, System.IO.BinaryReader reader)
		{
			this.parent = parent;
			this.Unserialize(reader);
		}

		public TrcnItem(Trcn parent, uint bcon, string label)
		{
			this.parent = parent;
			this.bcon = bcon;
			this.label = label;
		}

		public void Unserialize(System.IO.BinaryReader reader)
		{
			used = reader.ReadUInt32();
			bcon = reader.ReadUInt32();
			label = Helper.ToString(reader.ReadBytes(reader.ReadByte()));
			def = reader.ReadUInt16();
			min = reader.ReadUInt16();
			max = reader.ReadUInt16();
		}

		public void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(used);
			writer.Write(bcon);
			writer.Write((byte)label.Length);
			writer.Write(Helper.ToBytes(label, (byte)label.Length));
			writer.Write(def);
			writer.Write(min);
			writer.Write(max);
		}


		public override string ToString() { return this.label; }
	}
}
