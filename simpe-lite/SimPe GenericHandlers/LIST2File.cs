/***************************************************************************
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
/*using System;
using SimPe.Data;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Plugin.Internal;
using SimPe.Interfaces;
using SimPe.Interfaces.Files;

namespace SimPe.PackedFiles.Wrapper 
{
	/// <summary>
	/// Handles LIST Conform Files
	/// </summary>
	public class DirectoryList : Generic
	{
		/// <summary>
		/// true, if each entry is 5 dwords long
		/// </summary>
		bool longformat = true;

		/// <summary>
		/// Constructor for the Class
		/// </summary>		
		public DirectoryList() : base() {
			Register(0xE86B1EEF, new CreateFileObject(CreateLIST2File));
		}	
	
		/// <summary>
		/// Constructr for the Class
		/// </summary>
		/// <param name="pfd"></param>
		/// <param name="package"></param>
		public DirectoryList(IPackedFileDescriptor pfd, IPackageFile package) : base() 
		{
			Register(0xE86B1EEF, new CreateFileObject(CreateLIST2File));
			this.ProcessData(pfd, package);
		}	

		/// <summary>
		/// Constructr for the Class
		/// </summary>
		/// <param name="package"></param>
		public DirectoryList(IPackageFile package) : base() 
		{
			this.package = package;
		}

		/// <summary>
		/// Creates a LIST2 File Reader
		/// </summary>
		/// <param name="data">The Binary Data of the FIle</param>
		/// <returns>The Reder in a generic Format</returns>
		protected SimPe.PackedFiles.Wrapper.Generic CreateLIST2File(IPackedFileWrapper wrapper)
		{
			return this;
		}
		
		#region Generic.File Member
		protected override void ParseHeader()
		{
			longformat = ((Reader.BaseStream.Length % 20) == 0);
			
			uint i = 0;
			IPackedFileDescriptor pfd = package.GetFileIndex(i++);
			while (pfd!=null) 
			{
				if (pfd.SubType!=0) 
				{
					longformat=true;
					break;
				}
				pfd = package.GetFileIndex(i++);
			}//while

			//Read the first item for a test
			GenericItem gitem = new SimPe.PackedFiles.Wrapper.GenericItem();
			long pos = Reader.BaseStream.Position;
			ParseFileItem(gitem);
			DirectoryListItem item = gitem;
			Reader.BaseStream.Seek(pos, System.IO.SeekOrigin.Begin);
			

			//check if this item can be found , if so the formattype is correct
			i = 0;
			pfd = package.GetFileIndex(i++);
			bool check = false;
			while (pfd!=null) 
			{
				//found that file!
				if ( (pfd.Type == item.Type) &&
					(pfd.SubType == item.SubType) &&
					(pfd.Group == item.Group) &&
					(pfd.Instance == item.Instance) ) 
				{
					check = true;
					break;
				}
				pfd = package.GetFileIndex(i++);
			}//while
			
			//no matching Filetype found, so we think this is the other Format
			if (!check) longformat = !longformat;

			if(longformat) 
				items = new GenericItem[Reader.BaseStream.Length / 0x14];
			else
				items = new GenericItem[Reader.BaseStream.Length / 0x10];
		}

		protected override void ParseFileItem(GenericItem item)
		{
			item.Properties.Add("Type", Reader.ReadUInt32());
			item.Properties.Add("Group", Reader.ReadUInt32());
			item.Properties.Add("Instance", Reader.ReadUInt32());
			if (longformat) item.Properties.Add("Subtype", Reader.ReadUInt32());
			else item.Properties.Add("Subtype", 0);
			item.Properties.Add("Uncsize", Reader.ReadUInt32());			
		}

		public override string GetTypeName(uint type)
		{
			return "LIST2";
		}
		#endregion

		/// <summary>
		/// Returns or Sets wether the Items are stored in 5-Dword Format or not
		/// </summary>
		public bool LongFormat
		{
			get 
			{
				return longformat;
			}
			set 
			{
				longformat = value;
			}
		}
		
		/// <summary>
		/// Returns the Number of the File matching the passed Descriptor
		/// </summary>
		/// <param name="pfd">A PackedFileDescriptor</param>
		/// <returns>-1 if none was foudn or the index number of the first matching file</returns>
		public int FindFile(IPackedFileDescriptor pfd)
		{
			if (items == null) 
				return -1;
			for(int i=0; i<this.items.Length; i++) 
			{
				DirectoryListItem lfi = items[i];

				if (	(lfi.Group == pfd.Group) &&
						(lfi.Instance == pfd.Instance) &&
						((lfi.SubType == pfd.SubType) || (!longformat) ) && 
						(lfi.Type == pfd.Type) ) return i;

			}

			return -1;
		}		

		/// <summary>
		/// Saves the data representet by this Objet to the writer
		/// </summary>
		/// <param name="writer">The BinaryWriter</param>
		protected override void Serialize(System.IO.BinaryWriter writer)
		{			
			if (items!=null) 
			{
				foreach (DirectoryListItem lfi in items) 
				{
					writer.Write(lfi.Type);
					writer.Write(lfi.Group);
					writer.Write(lfi.Instance);
					if (longformat) writer.Write(lfi.SubType);
					writer.Write(lfi.UncompressedSize);				
				}
			}
		}		

		public void UpdateData(object newdata)
		{
		}	
	
		#region IWrapper Member
		protected override IWrapperInfo CreateWrapperInfo()
		{
			return new AbstractWrapperInfo(
				"Compressed File List Wrapper",
				"Quaxi",
				"---",
				1
				); 
		}
		#endregion
	}
}*/
