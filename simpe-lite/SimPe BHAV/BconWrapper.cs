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
	public class Bcon
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
		/// Just A Flag
		/// </summary>
		private byte flag = 0x00;
		/// <summary>
		/// Contains all available Constants 
		/// </summary>		
		private ArrayList items = new ArrayList();

		/// <summary>
		/// Contains a valid TRCN Resource that describes these values
		/// </summary>
		private Trcn trcnres = null;
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
		/// Returns /Sets the Flag
		/// </summary>
		public byte Flag 
		{
			get { return flag;	}			
			set
			{
				if (flag != value)
				{
					flag = value;
					OnWrapperChanged(this, new EventArgs());
				}
			}
		}


		/// <summary>
		/// Returns the Labels describing these constants
		/// </summary>
		public Trcn TrcnResource
		{
			get 
			{
				if (trcnres == null && FileDescriptor != null)
				{
					pjse.FileTable.Entry[] items = pjse.FileTable.GFT[0x5452434E, FileDescriptor.Group, FileDescriptor.Instance];
					if (items == null || items.Length == 0) return null;

					trcnres = new Trcn();
					trcnres.ProcessData(items[0].PFD, items[0].Package);
				}

				return trcnres;
			}
		}
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public Bcon() : base() { }


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
			return new UserInterface.BconForm();
		}

		/// <summary>
		/// Returns a Human Readable Description of this Wrapper
		/// </summary>
		/// <returns>Human Readable Description</returns>
		protected override IWrapperInfo CreateWrapperInfo()
		{
			return new AbstractWrapperInfo(
				"PJSE BCON Wrapper",
				"Peter L Jones",
				"BCON Value Editor",
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
			writer.Write((byte)items.Count);
			writer.Write(flag);					

			foreach(short v in items)
				writer.Write(v);
		}
		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		protected override void Unserialize(System.IO.BinaryReader reader)
		{
			filename = reader.ReadBytes(64);
			int length = reader.ReadByte();
			flag = reader.ReadByte();							
 
			short[] bi = new short[length];
			items = new ArrayList(bi);
			for (int i = 0; i < length; i++) 
				items[i] = reader.ReadInt16();
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
				uint[] types = {0x42434F4E};	 // BCON
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
		//all covered by Serialize()
		#endregion

		#region ICollection Members
		public int Add(short item)
		{
			if (items.Count >= 0x80) // only allow 128 lines, as that's all a dataOwner can reference
				return -1;

			//item.Parent = this;
			int result = items.Add(item);
			if (result >= 0) OnWrapperChanged(items, new EventArgs());
			return result;
		}

		public void Clear()
		{
			items.Clear();
			OnWrapperChanged(items, new EventArgs());
		}

		public void Remove(short item) { this.RemoveAt(items.IndexOf(item)); }

		public void RemoveAt(int index)
		{
			if (index < 0 || index >= items.Count) return;

			items.RemoveAt(index);
			OnWrapperChanged(items, new EventArgs());
		}

		public short this[int index]
		{
			get
			{
				return (short)items[index];
			}
			set
			{
				if (items[index] == null || !items[index].Equals(value))
				{
					//value.Parent = this;
					items[index] = value;
					OnWrapperChanged(items[index], new EventArgs());
				}
			}
		}

		public bool Contains(short item) { return items.Contains(item); }

		public int IndexOf(object item) { return items.IndexOf(item); }

		public override void CopyTo(Array a, int i) { items.CopyTo(a, i); }

		public override int Count { get { return items.Count; } }

		public override bool IsSynchronized { get { return items.IsSynchronized; } }

		public override object SyncRoot { get { return items.SyncRoot; } }

		#region IEnumerable Members
		public override IEnumerator GetEnumerator() { return items.GetEnumerator(); }

		#endregion
		#endregion

	}
}
