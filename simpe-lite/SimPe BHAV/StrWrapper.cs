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
		/// Format Code of the FIle
		/// </summary>
		private SimPe.Data.MetaData.FormatCode format;
		/// <summary>
		/// Somewhere to keep track of how many StrItems we have
		/// </summary>
		private ushort count;
		/// <summary>
		/// Contains all StrItems by Language
		/// </summary>
		private Hashtable languages = null;
		/// <summary>
		/// Maximum Number of Lines to load
		/// </summary>
		private int limit = 0;
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
		/// Returns /Sets the Format Code
		/// </summary>
		public SimPe.Data.MetaData.FormatCode Format
		{
			get { return format; }			
			set 
			{
				if (format != value)
				{
					format = value;
					OnWrapperChanged(new EventArgs());
				}
			}
		}


		public byte[] Languages
		{
			get
			{
				ArrayList ab = new ArrayList(languages.Keys);
				ab.Sort();
				byte[] b = new byte[ab.Count];
				ab.CopyTo(b);
				return b;
			}
		}

		public StrItem this[byte lid, int index]
		{
			get
			{
				ArrayList al = (ArrayList)languages[lid];
				if (al == null) return null;
				if (index >= al.Count) return null;
				return (StrItem)al[index];
			}
			set
			{
				if (limit > 0 && count >= limit) return;

				if ((ArrayList)languages[lid] == null) AddLanguage(lid);
				ArrayList al = (ArrayList)languages[lid];

				if (value != null)
				{
					while ((limit == 0 || count < limit) && al.Count <= index)
					{
						StrItem z = Add(lid);
						z.Title = "";
					}
					if (index < al.Count && !((StrItem)al[index]).Equals(value))
					{
						al[index] = value;
						OnWrapperChanged(new EventArgs());
					}
				}
				else
				{
					if (index < al.Count)
					{
						StrItem i = (StrItem)al[index];
						if (!(i.Title.Equals("") && i.Description.Equals("")))
						{
							i.Title = "";
							i.Description = "";
							OnWrapperChanged(new EventArgs());
						}
					}
				}
			}
		}

		public StrItem this[bool fallback, byte lid, int index]
		{
			get
			{
				StrItem i = this[lid, index];
				return (!fallback || (i != null && i.Title.Trim() != "")) ? i : this[0x1, index];
			}
		}

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public Str() : base()
		{
			format = SimPe.Data.MetaData.FormatCode.normal;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public Str(int limit) : base()
		{
			format = SimPe.Data.MetaData.FormatCode.normal;
			this.limit = limit;
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

		public void AddLanguage(byte lid)
		{
			if (languages[lid] == null)
			{
				languages[lid] = new ArrayList();
				//OnWrapperChanged(new EventArgs()); //???? no... not until a line gets added
			}
		}

		public void RemoveLanguage(byte lid)
		{
			ArrayList al = (ArrayList)languages[lid];
			if (al != null)
			{
				languages.Remove(lid);
				if (al.Count != 0)
				{
					OnWrapperChanged(new EventArgs());
					count -= (ushort)al.Count;
				}
			}
		}

		public StrItem Add(byte lid)
		{
			if (limit > 0 && count >= limit) return null;

			internalchg = true;
			if (languages[lid] == null)
				AddLanguage(lid);
			ArrayList al = (ArrayList)languages[lid];
			StrItem i = new StrItem(this);
			al.Add(i);
			count++;
			internalchg = false;
			OnWrapperChanged(new EventArgs());
			return i;
		}

		public int Add(byte lid, StrItem item)
		{
			StrItem ni = Add(lid);
			if (ni == null) return -1;

			ni.Title = item.Title;
			ni.Description = item.Description;
			return ((ArrayList)languages[lid]).Count - 1;
		}

		public void RemoveAt(byte lid, int index)
		{
			ArrayList al = (ArrayList)languages[lid];
			if (al != null && index < al.Count)
			{
				al.RemoveAt(index);
				count--;
				OnWrapperChanged(new EventArgs());
			}
		}

		public void Remove(byte lid, StrItem i)
		{
			ArrayList al = (ArrayList)languages[lid];
			if (al != null)
				RemoveAt(lid, al.IndexOf(i));
		}

		/*public void Remove(StrItem i)
		{
			foreach(byte lid in languages.Keys) Remove(lid, i);
		}*/

		public int nrStrItems(byte lid)
		{
			ArrayList al = (ArrayList)languages[lid];
			return (al == null) ? 0 : al.Count;
		}


		#region AbstractWrapper Member
		protected override IPackedFileUI CreateDefaultUIHandler()
		{
			return new UserInterface.OldStrForm();
		}

		/// <summary>
		/// Returns a Human Readable Description of this Wrapper
		/// </summary>
		/// <returns>Human Readable Description</returns>
		protected override IWrapperInfo CreateWrapperInfo()
		{
			return new AbstractWrapperInfo(
				"PJSE STR# Wrapper",
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
			format = (SimPe.Data.MetaData.FormatCode)reader.ReadUInt16();

			count = reader.ReadUInt16();

			languages = new Hashtable();
			for (int i = 0; i < count && (limit == 0 || i < limit); i++)
			{
				byte lid = reader.ReadByte();
				if (languages[lid] == null)
					languages[lid] = new ArrayList();

				((ArrayList)languages[lid]).Add(new StrItem(this, reader));
			}
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
			writer.Write((ushort)format);
			writer.Write((ushort)count);

			int c = 0;
			for (byte lid = 1; lid < 45; lid++)
				if (languages[lid] != null)
					foreach (StrItem i in ((ArrayList)languages[lid]))
					{
						writer.Write(lid);
						i.Serialize(writer);
						c++;
					}
			if (c != count)
				throw new Exception("Oops!  count was " + count.ToString() + " but wrote " + c.ToString() + " StrItems!");
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
				return "filename=" + this.FileName +
					", lines=" + count.ToString() +
					", first=" + this[0x1, 0];
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
	}


	/// <summary>
	/// An Item stored in a STR# File
	/// </summary>
	public class StrItem
	{
		#region Attributes
		private Str parent = null;
		private string title = null;
		private string desc = null;
		#endregion

		#region Accessor methods
		public string Title 
		{
			get { return title; }
			set 
			{
				if (title != value)
				{
					title = value;
					parent.OnWrapperChanged(new EventArgs());
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
					parent.OnWrapperChanged(new EventArgs());
				}
			}
		}

		#endregion

		public StrItem(Str parent) : base()
		{
			this.parent = parent;
			parent.OnWrapperChanged(new System.EventArgs());
		}

		public StrItem(Str parent, System.IO.BinaryReader reader) : base()
		{
			this.parent = parent;
			this.Unserialize(reader);
		}


		public void Unserialize(System.IO.BinaryReader reader)
		{
			title = UnserializeStringZero(reader);
			desc = UnserializeStringZero(reader);
		}

		public void Serialize(System.IO.BinaryWriter writer)
		{
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
