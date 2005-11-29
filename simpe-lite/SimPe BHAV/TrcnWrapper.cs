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
		: pjse.ExtendedWrapper //AbstractWrapper				//Implements some of the default Behaviur of a Handler, you can Implement yourself if you want more flexibility!
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
		private uint[] header = { 0x5452434E, 0x0000004E, 0x00000000 }; // 'TRCN', version, 0
		/// <summary>
		/// Items stored in the File
		/// </summary>
		private TrcnItemArrayList items = new TrcnItemArrayList();

		/// <summary>
		/// Contains a valid BCON Resource that these labels describe
		/// </summary>
		private Bcon bconres = null;
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
					OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		/// <summary>
		/// Returns the Version
		/// </summary>
		public uint Version
		{
			get { return header[1]; }
			set
			{
				if (header[1] != value)
				{
					header[1] = value;
					OnWrapperChanged(this, new EventArgs());
				}
			}
		}


		/// <summary>
		/// Returns the Constants described by these labels
		/// </summary>
		internal Bcon BconResource
		{
			get 
			{
				if (bconres == null) 
				{
					bconres = new Bcon();
					if ((Package!=null) && (FileDescriptor!=null)) 
					{
						Interfaces.Files.IPackedFileDescriptor pfd = Package.FindFile(
							0x42434F4E, // Constant (values) File (BCON)
							0, 
							FileDescriptor.Group,
							FileDescriptor.Instance
							);

						if (pfd!=null) 
						{
							bconres.ProcessData(pfd, Package);
						}
					} 
					else 
					{
						bconres = null;
					}
				}

				return bconres;
			}
		}
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public Trcn() : base() { }


		#region AbstractWrapper Member
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
			return new AbstractWrapperInfo(
				"PJSE TRCN Wrapper",
				"Peter L Jones",
				"BCON Label Editor",
				1
				); 
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

			writer.Write((uint)items.Count);
						
			for (int i = 0; i < items.Count; i++)
				if (items[i] != null) ((TrcnItem)items[i]).Serialize(writer);
		}
		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		protected override void Unserialize(System.IO.BinaryReader reader)
		{
			// in case we give up...
			items = null;

			filename = reader.ReadBytes(64);

			header = new uint[3];
			header[0] = reader.ReadUInt32();
			header[1] = reader.ReadUInt32();
			header[2] = reader.ReadUInt32();
			if (header[0] != 0x5452434E)
				return;

			uint itemCount = reader.ReadUInt32();

			TrcnItem[] ti = new TrcnItem[itemCount];
			items = new TrcnItemArrayList(ti);
			for (int i = 0; i < itemCount; i++)
				items[i] = new TrcnItem(this, reader);
		}

		#endregion

		#region IFileWrapper Member
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

		#endregion		

		#region IFileWrapperSaveExtension Member		
		//all covered by AbstractWrapper
		#endregion

		#region ICollection Members
		public int Add(TrcnItem item)
		{
			if (items.Count >= 0x80) // only allow 128 lines, as that's all a dataOwner can reference
				return -1;

			item.Parent = this;
			int result = items.Add(item);
			if (result >= 0) OnWrapperChanged(items, new EventArgs());
			return result;
		}

		public void Clear()
		{
			items.Clear();
			OnWrapperChanged(items, new EventArgs());
		}

		public void Remove(TrcnItem item) { this.RemoveAt(items.IndexOf(item)); }

		public void RemoveAt(int index)
		{
			if (index < 0 || index >= items.Count) return;

			items.RemoveAt(index);
			OnWrapperChanged(items, new EventArgs());
		}

		public TrcnItem this[int index]
		{
			get
			{
				return items[index];
			}
			set
			{
				if (items[index] == null || !items[index].Equals(value))
				{
					value.Parent = this;
					items[index] = value;
					OnWrapperChanged(items, new EventArgs());
				}
			}
		}

		public bool Contains(TrcnItem item) { return items.Contains(item); }

		public int IndexOf(object item) { return items.IndexOf(item); }

		public override void CopyTo(Array a, int i) { items.CopyTo(a, i); }

		public override int Count { get { return items.Count; } }

		public override bool IsSynchronized { get { return items.IsSynchronized; } }

		public override object SyncRoot { get { return items.SyncRoot; } }

		#region IEnumerable Members
		public override IEnumerator GetEnumerator() { return items.GetEnumerator(); }

		#endregion
		#endregion

		#region TrcnItemArrayList
		private class TrcnItemArrayList : ArrayList
		{
			public TrcnItemArrayList() : base() { }

			public TrcnItemArrayList(TrcnItem[] c) : base(c) { }

			public TrcnItemArrayList(int capacity) : base(capacity) { }

			public new TrcnItem this[int index]
			{
				get { return (TrcnItem)base[index]; }
				set { base[index] = value; }
			}

		}

		#endregion
	}


	/// <summary>
	/// An Item stored in a TRCN
	/// </summary>
	public class TrcnItem
	{
		#region Attributes
		private uint used = 0x00000000;
		private uint constId = 0x00000000;
		private string constName = "";
		private ushort defValue = 0;
		private ushort minValue = 0;
		private ushort maxValue = 0;

		private Trcn parent = null;
		#endregion

		#region Accessor methods
		public uint Used
		{
			get { return used; }
			set 
			{
				if (used != value)
				{
					used = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public uint ConstId
		{
			get { return constId; }
			set 
			{
				if (constId != value)
				{
					constId = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public string ConstName
		{
			get { return constName; }
			set 
			{
				if (constName != value)
				{
					constName = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public ushort DefValue
		{
			get { return defValue; }
			set 
			{
				if (defValue != value)
				{
					defValue = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public ushort MinValue
		{
			get { return minValue; }
			set 
			{
				if (minValue != value)
				{
					minValue = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public ushort MaxValue
		{
			get { return maxValue; }
			set 
			{
				if (maxValue != value)
				{
					maxValue = value;
					parent.OnWrapperChanged(this, new EventArgs());
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
			Unserialize(reader);
		}


		public TrcnItem Clone()
		{
			TrcnItem clone = new TrcnItem(this.parent);
			clone.used = this.used;
			clone.constId = this.constId;
			clone.constName = this.constName;
			clone.defValue = this.defValue;
			clone.minValue = this.minValue;
			clone.maxValue = this.maxValue;
			return clone;
		}


		/// <summary>
		/// Reads Data from the Stream
		/// </summary>
		/// <param name="reader"></param>
		internal void Unserialize(System.IO.BinaryReader reader)
		{
			this.used = reader.ReadUInt32();
			if (parent.Version < 0x46)
			{
				this.constId = reader.ReadUInt16();
				this.ConstName = UnserializeStringZero(reader);
			}
			else
			{
				this.constId = reader.ReadUInt32();
				this.constName = SimPe.Helper.ToString(reader.ReadBytes(reader.ReadByte()));
			}
			this.defValue = reader.ReadUInt16();
			this.minValue = reader.ReadUInt16();
			this.maxValue = reader.ReadUInt16();
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		internal void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(this.used);
			if (parent.Version < 0x46)
			{
				writer.Write((ushort)this.constId);
				SerializeStringZero(writer, this.constName);
			}
			else
			{
				writer.Write(this.constId);
				writer.Write((byte)this.constName.Length);
				writer.Write(SimPe.Helper.ToBytes(this.constName, this.constName.Length));
			}
			writer.Write(this.defValue);
			writer.Write(this.minValue);
			writer.Write(this.maxValue);
		}

		private string UnserializeStringZero(System.IO.BinaryReader r)
		{
			string s = "";
			while (r.BaseStream.Position < r.BaseStream.Length)
			{
				char b = r.ReadChar();
				if (b == 0) break;
				s += b;
			}
			return s;
		}

		private void SerializeStringZero(System.IO.BinaryWriter w, string s)
		{
			if (s != null) foreach (char c in s) w.Write(c);
			w.Write((char)0);
		}

	}
}
