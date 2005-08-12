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
	public class Str
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
		/// Format Code of the FIle
		/// </summary>
		private ushort format;
		/// <summary>
		/// Somewhere to keep track of how many StrItems we have
		/// </summary>
		//private ushort count;
		/// <summary>
		/// Holds all the StrItems
		/// </summary>
		private StrItemArrayList items = new StrItemArrayList();

		/// <summary>
		/// Maximum Number of Lines to load
		/// </summary>
		private int limit = 0;
		/// <summary>
		/// Indicates the data content of the wrapper (packed file) has changed
		/// </summary>
		public event EventHandler WrapperChanged;
		/// <summary>
		/// Indicates a wrapper routine is updating the wrapper and will generate the WrapperChanged event when it's ready
		/// </summary>
		private bool internalchg = false;
		#endregion

		#region Accessor methods
		/// <summary>
		/// Returns / Sets the Filename
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
		/// Returns / Sets the Format Code
		/// </summary>
		public ushort Format
		{
			get { return format; }			
			set 
			{
				if (format != value)
				{
					format = value;
					OnWrapperChanged();
				}
			}
		}

		/// <summary>
		/// Returns number of strings
		/// </summary>
		public ushort Count { get { return (ushort)items.Count; } }


		/// <summary>
		/// Returns / Sets a specific StrItem
		/// </summary>
		public StrItem this[int index]
		{
			get
			{
				return items[index];
			}
			set
			{
				if (items[index] == null || !items[index].Equals(value))
				{
					items[index] = value;
					OnWrapperChanged();
				}
			}
		}

		public StrItem this[byte lid, int index]
		{
			get
			{
				int count = 0;
				foreach (StrItem i in items)
				{
					if (i.LanguageID == lid)
					{
						if (count == index)
							return i;
						count++;
					}
				}
				return null;
			}
		}

		public StrItem this[bool fallback, byte lid, int index]
		{
			get
			{
				StrItem i = this[lid, index];
				return (fallback && (i == null || i.Title.Trim().Equals(""))) ? this[0x01, index] : i;
			}
		}

		public StrItem[] this[byte lid]
		{
			get
			{
				ArrayList s = new ArrayList();
				foreach (StrItem i in items)
				{
					if (i.LanguageID == lid)
						s.Add(i);
				}
				return (StrItem[])s.ToArray(typeof(StrItem));
			}
		}
		public int Add(StrItem item)
		{
			item.Parent = this;
			int result = items.Add(item);
			if (!item.Title.Trim().Equals("") || !item.Description.Trim().Equals(""))
				OnWrapperChanged();
			return result;
		}

		public int Add(byte lid, string title, string desc) { return this.Add(new StrItem(this, lid, title, desc)); }

		public void Remove(StrItem item) { this.RemoveAt(items.IndexOf(item)); }

		public void RemoveAt(int index)
		{
			if (index < 0 || index >= items.Count) return;

			bool changed = false;
			for (int i = index; !changed && i < items.Count; i++)
				changed = changed || (
					(items[i].LanguageID == items[index].LanguageID)
					&&
					(!items[i].Title.Trim().Equals("") || !items[i].Description.Trim().Equals(""))
					);

			items.RemoveAt(index);
			if (changed) OnWrapperChanged();
		}

		public void CleanUp()
		{
			Hashtable lngs = new Hashtable();
			foreach (StrItem i in items)
			{
				if (lngs[i.LanguageID] == null)
					lngs[i.LanguageID] = new ArrayList();
				((ArrayList)lngs[i.LanguageID]).Add(i);
			}
			foreach (ArrayList l in lngs.Values)
				for (int i = l.Count - 1; i >= 0; i--)
				{
					if ( ((StrItem)l[i]).Title.Trim().Equals("") && ((StrItem)l[i]).Description.Trim().Equals("") )
						items.Remove((StrItem)l[i]);
					else break;
				}
		}
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public Str() : base()
		{
			format = (ushort)SimPe.Data.MetaData.FormatCode.normal;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public Str(int limit) : base()
		{
			format = (ushort)SimPe.Data.MetaData.FormatCode.normal;
			this.limit = limit;
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
			return new UserInterface.StrForm();
		}

		/// <summary>
		/// Returns a Human Readable Description of this Wrapper
		/// </summary>
		/// <returns>Human Readable Description</returns>
		protected override IWrapperInfo CreateWrapperInfo()
		{
			return new AbstractWrapperInfo(
				"PJSE STR#/TTAs/CTSS Wrapper",
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
			format = reader.ReadUInt16();
			ushort count = reader.ReadUInt16();
			items = new StrItemArrayList(count);

			for (int i = 0; i < count && (limit == 0 || i < limit); i++)
				items.Add(new StrItem(this, reader));

			CleanUp();
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
			CleanUp();

			writer.Write(filename);
			writer.Write(format);
			writer.Write((ushort)items.Count);
			foreach (StrItem i in items)
				i.Serialize(writer);
		}
		#endregion

		#region IWrapper member
		public override bool CheckVersion(uint version) 
		{
			return true;
		}
		#endregion

		#region IFileWrapperSaveExtension Member		
		//all covered by Serialize()
		#endregion

		#region IFileWrapper Member
		public override string Description
		{
			get
			{
				return "filename=" + this.FileName
					+ ", lines=" + Count.ToString()
					+ ", first=" + this[0x1, 0]
					;
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

		/// <summary>
		/// Returns a list of File Types this Plugin can process
		/// </summary>
		public uint[] AssignableTypes
		{
			get
			{
				uint[] types = {
								    0x53545223  // STR#
								   ,0x54544173  // TTAs
								   ,0x43545353  // CTSS
							   };
			
				return types;
			}
		}

		#endregion		

		#region StrItemArrayList
		private class StrItemArrayList : ArrayList
		{
			public StrItemArrayList() : base() { }

			public StrItemArrayList(StrItem[] c) : base(c) { }

			public StrItemArrayList(int capacity) : base(capacity) { }

			public int Add(StrItem item) { return base.Add(item); }

			public void AddRange(StrItem[] c) { base.AddRange(c); }

			public void Insert(int index, StrItem item) { base.Insert(index, item); }

			public void SetRange(int index, StrItem[] c) { base.SetRange(index, c); }

			public new StrItem this[int index]
			{
				get { return (StrItem)base[index]; }
				set { base[index] = value; }
			}

		}

		#endregion
	}


	/// <summary>
	/// An Item stored in a STR# File
	/// </summary>
	public class StrItem
	{
		#region Attributes
		private Str parent = null;
		private byte lid = 0;
		private string title = null;
		private string desc = null;
		#endregion

		#region Accessor methods
		public byte LanguageID
		{
			get { return lid; }
			set 
			{
				if (lid != value)
				{
					lid = value;
					if (parent != null) parent.OnWrapperChanged();
				}
			}
		}

		public string Title 
		{
			get { return title; }
			set 
			{
				if (title != value)
				{
					title = value;
					if (parent != null) parent.OnWrapperChanged();
				}
			}
		}

		public string Description 
		{
			get { return desc; }
			set 
			{
				if (desc != value)
				{
					desc = value;
					if (parent != null) parent.OnWrapperChanged();
				}
			}
		}

		public Str Parent
		{
			get { return parent; }
			set { parent = value; } // parent not part of wrapper
		}
		#endregion

		public StrItem(Str parent)
		{
			this.parent = parent;
		}

		public StrItem(Str parent, System.IO.BinaryReader reader)
		{
			this.parent = parent;
			this.Unserialize(reader);
		}

		public StrItem(Str parent, byte lid, string title, string desc)
		{
			this.parent = parent;
			this.lid = lid;
			this.title = title;
			this.desc = desc;
		}

		public void Unserialize(System.IO.BinaryReader reader)
		{
			lid = reader.ReadByte();
			title = UnserializeStringZero(reader);
			desc = UnserializeStringZero(reader);
		}

		public void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(lid);
			SerializeStringZero(writer, title);
			SerializeStringZero(writer, desc);
		}

		private string UnserializeStringZero(System.IO.BinaryReader r)
		{
			char b = r.ReadChar();
			string s = "";
			while (b != 0 && r.BaseStream.Position <= r.BaseStream.Length)
			{
				s += b;
				b = r.ReadChar();
			}
			return s;
		}

		private void SerializeStringZero(System.IO.BinaryWriter w, string s)
		{
			if (s != null) foreach (char c in s) w.Write(c);
			w.Write((char)0);
		}


		public override string ToString() { return this.Title; }

	}

}
