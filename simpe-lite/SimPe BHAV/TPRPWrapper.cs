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
	public class TPRP
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
		private uint[] header = { 0x54505250, 0x0000004E, 0x00000000 }; // 'TPRP', version, 0
		/// <summary>
		/// Count of Param labels
		/// </summary>
		private int paramCount = 0;
		/// <summary>
		/// Count of Local labels
		/// </summary>
		private int localCount = 0;
		/// <summary>
		/// Items stored in the File
		/// </summary>
		private TPRPItemArrayList items = new TPRPItemArrayList();
		/// <summary>
		/// Unknown
		/// </summary>
		private uint reserved = 0;
		/// <summary>
		/// Contains 0x01 for each TPRPParamItem
		/// </summary>
		private byte[] paramData = new byte[0];
		/// <summary>
		/// Trailer of the File
		/// </summary>
		private uint[] trailer = { 0x00000005, 0x00000000 }; // Display code, null

		/// <summary>
		/// Contains a valid BHAV Resource that these labels describe
		/// </summary>
		private Bhav bhavres = null;
		private byte bhavParamCount = 0;
		private byte bhavLocalCount = 0;
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
		/// Returns the BHAV described by these labels
		/// </summary>
		public Bhav BhavResource
		{
			get 
			{
				if (bhavres == null && FileDescriptor != null)
				{
					pjse.FileTable.Entry[] items = pjse.FileTable.GFT[SimPe.Data.MetaData.BHAV_FILE, FileDescriptor.Group, FileDescriptor.Instance];
					if (items == null || items.Length == 0) return null;

					bhavres = new Bhav();
					bhavres.ProcessData(items[0].PFD, items[0].Package);
				}

				return bhavres;
			}
		}

		public byte BhavParamCount
		{
			get { return bhavParamCount = BhavResource.Header.ArgumentCount; }
		}

		public byte BhavLocalCount
		{
			get { return bhavLocalCount = BhavResource.Header.LocalVarCount; }
		}

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public TPRP() : base()
		{
			if (BhavResource != null)
			{
				paramCount = bhavParamCount = BhavResource.Header.ArgumentCount;
				localCount = bhavLocalCount = BhavResource.Header.LocalVarCount;
				BhavResource.WrapperChanged += new EventHandler(BhavResource_WrapperChanged);
			}
		}

		private void BhavResource_WrapperChanged(object sender, System.EventArgs e)
		{
			if (sender == BhavResource)
			{
				if (bhavParamCount != BhavResource.Header.ArgumentCount
					|| bhavLocalCount != BhavResource.Header.LocalVarCount)
					OnWrapperChanged(this, new EventArgs());
			}
		}


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
			return new UserInterface.TPRPForm();
		}

		/// <summary>
		/// Returns a Human Readable Description of this Wrapper
		/// </summary>
		/// <returns>Human Readable Description</returns>
		protected override IWrapperInfo CreateWrapperInfo()
		{
			return new AbstractWrapperInfo(
				"PJSE TPRP Wrapper",
				"Peter L Jones",
				"TREE Label Editor",
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

			writer.Write(paramCount);
			writer.Write(localCount);

			foreach(TPRPItem item in items)
				if (item is TPRPParamLabel) item.Serialize(writer);
			foreach(TPRPItem item in items)
				if (item is TPRPLocalLabel) item.Serialize(writer);

			writer.Write(reserved);
			foreach(TPRPItem item in items)
				if (item is TPRPParamLabel) writer.Write(((TPRPParamLabel)item).PData);

			writer.Write(trailer[0]);
			writer.Write(trailer[1]);
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
			if (header[0] != 0x54505250)
				return;

			paramCount = reader.ReadInt32();
			localCount = reader.ReadInt32();

			TPRPItem[] ti = new TPRPItem[paramCount + localCount];
			items = new TPRPItemArrayList(ti);
			for (int i = 0; i < paramCount; i++)
				items[i] = new TPRPParamLabel(this, reader);
			for (int i = 0; i < localCount; i++)
				items[paramCount + i] = new TPRPLocalLabel(this, reader);

			reserved = reader.ReadUInt32();
			foreach(TPRPItem item in items)
				if (item is TPRPParamLabel) ((TPRPParamLabel)item).ReadPData(reader);

			trailer = new uint[2];
			trailer[0] = reader.ReadUInt32();
			trailer[1] = reader.ReadUInt32();
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
				uint[] types = {0x54505250}; //handles the TPRP File
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
		public int Add(TPRPItem item)
		{
			item.Parent = this;
			int result = items.Add(item);
			if (result >= 0) OnWrapperChanged(items, new EventArgs());
			if (item is TPRPParamLabel)
				this.paramCount++;
			else
				this.localCount++;
			return result;
		}

		public void Clear()
		{
			items.Clear();
			OnWrapperChanged(items, new EventArgs());
		}

		public void Remove(TPRPItem item) { this.RemoveAt(items.IndexOf(item)); }

		public void RemoveAt(int index)
		{
			if (index < 0 || index >= items.Count) return;

			if (items[index] is TPRPParamLabel)
				this.paramCount--;
			else
				this.localCount--;
			items.RemoveAt(index);
			OnWrapperChanged(items, new EventArgs());
		}

		public TPRPItem this[bool local, int index]
		{
			get { return this[(local ? paramCount : 0) + index]; }
			set { this[(local ? paramCount : 0) + index] = value; }
		}

		private TPRPItem this[int index]
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

		public bool Contains(TPRPItem item) { return items.Contains(item); }

		public int IndexOf(object item) { return items.IndexOf(item); }

		public override void CopyTo(Array a, int i) { items.CopyTo(a, i); }

		public override int Count { get { return items.Count; } }

		public override bool IsSynchronized { get { return items.IsSynchronized; } }

		public override object SyncRoot { get { return items.SyncRoot; } }

		#region IEnumerable Members
		public override IEnumerator GetEnumerator() { return items.GetEnumerator(); }

		#endregion
		#endregion

		#region TPRPItemArrayList
		private class TPRPItemArrayList : ArrayList
		{
			public TPRPItemArrayList() : base() { }

			public TPRPItemArrayList(TPRPItem[] c) : base(c) { }

			public TPRPItemArrayList(int capacity) : base(capacity) { }

			public new TPRPItem this[int index]
			{
				get { return (TPRPItem)base[index]; }
				set { base[index] = value; }
			}

		}

		#endregion
	}


	/// <summary>
	/// An Item stored in a TRCN
	/// </summary>
	public abstract class TPRPItem
	{
		#region Attributes
		private string label = "";

		private bool pORl = false;
		private TPRP parent = null;
		#endregion

		#region Accessor methods
		public string Label
		{
			get { return label; }
			set 
			{
				if (label != value)
				{
					label = value;
					parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}


		public bool IsParamLabel { get { return (pORl == false); } }

		public bool IsLocalLabel { get { return (pORl == true); } }

		public TPRP Parent
		{
			get { return parent; }
			set { parent = value; } // parent not part of wrapper
		}

		#endregion

		public TPRPItem(TPRP parent, bool pORl)
		{
			this.parent = parent;
			this.pORl = pORl;
		}

		public TPRPItem(TPRP parent, bool pORl, System.IO.BinaryReader reader) : this(parent, pORl)
		{
			Unserialize(reader);
		}


		public TPRPItem Clone()
		{
			TPRPItem clone = (TPRPItem)base.MemberwiseClone();
			clone.label = (string)this.label.Clone();
			clone.pORl = this.pORl;
			clone.parent = this.parent;
			return clone;
		}


		/// <summary>
		/// Reads Data from the Stream
		/// </summary>
		/// <param name="reader"></param>
		internal void Unserialize(System.IO.BinaryReader reader)
		{
			label = SimPe.Helper.ToString(reader.ReadBytes(reader.ReadByte()));
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
			writer.Write((byte)label.Length);
			foreach (char c in label) writer.Write(c);
		}


		public override string ToString() { return label; }

		public static implicit operator string(TPRPItem i) { return i.label; }

	}

	public class TPRPParamLabel : TPRPItem
	{
		private byte pData = 0x01;
		public byte PData { get { return pData; } }

		/// <summary>
		/// For the time being, I'm explicitly preventing this value being adjusted
		/// </summary>
		/// <param name="reader">Stream containing a byte to read</param>
		public void ReadPData(System.IO.BinaryReader reader) { pData = reader.ReadByte(); }


		public TPRPParamLabel(TPRP parent) : base(parent, false) { }

		public TPRPParamLabel(TPRP parent, System.IO.BinaryReader reader) : base(parent, false, reader) { }


	}

	public class TPRPLocalLabel : TPRPItem
	{
		public TPRPLocalLabel(TPRP parent) : base(parent, true) { }

		public TPRPLocalLabel(TPRP parent, System.IO.BinaryReader reader) : base(parent, true, reader) { }

	}

}
