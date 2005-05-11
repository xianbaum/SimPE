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
using System;
using System.IO;
using System.Collections;
using SimPe.Data;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Plugin.Internal;
using SimPe.Interfaces;
using SimPe.Interfaces.Files;

namespace SimPe.PackedFiles.Wrapper 
{
	/// <summary>
	/// Handles NeighborhoodData Conform Files
	/// </summary>
	public class NeighborhoodData: Generic
	{
		Interfaces.IProviderRegistry provider;

		/// <summary>
		/// Constructor for the Class
		/// </summary>		
		/// <param name="names">A SimName Provider or null</param>
		public NeighborhoodData(Interfaces.IProviderRegistry provider) : base() 
		{
			this.provider = provider;
			Register(0x4E474248, new CreateFileObject(CreateLIST2File));
		}	
	
		/// <summary>
		/// Constructr for the Class
		/// </summary>
		/// <param name="pfd"></param>
		/// <param name="package"></param>
		public NeighborhoodData(IPackedFileDescriptor pfd, IPackageFile package) : base() 
		{
			Register(0x4E474248, new CreateFileObject(CreateLIST2File));
			this.ProcessData(pfd, package);
		}	

		
		/// <summary>
		/// Constructr for the Class
		/// </summary>
		/// <param name="package"></param>
		public NeighborhoodData(IPackageFile package) : base() 
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

		#region IWrapper Member
		protected override IWrapperInfo CreateWrapperInfo()
		{
			return new AbstractWrapperInfo(
				"Experimental Neighborhood/Memory Wrapper",
				"Quaxi",
				"---",
				2
				); 
		}
		#endregion
		
		#region Generic.File Member
		
		protected void ParseItem(string name, ref ArrayList list, Hashtable mems)
		{
			GenericItem item = new GenericItem();
			item.Properties["BLOCK"] = name;
			uint guid = Reader.ReadUInt32();
			if (mems.ContainsKey(guid)) 
				item.Properties["GUID"] = mems[guid].ToString();
			else
				item.Properties["GUID"] = guid;
			item.Properties["UNK"] = Reader.ReadUInt16();
			int count = Reader.ReadInt32()*2;
			item.Properties["dLENGTH"] = count;
			byte[] data = Reader.ReadBytes(count);
			string s = "";
			foreach(byte b in data) s += Helper.HexString(b) + " ";
			item.Properties["DATA"] = s;

			list.Add(item);
		}

		protected void ParseSlot(string name, int count, ref ArrayList list, Hashtable mems) 
		{
			for (int i=0; i<count; i++) 
			{
				uint slotid = Reader.ReadUInt32();
				uint itemcount = Reader.ReadUInt32();
				for (int j=0; j<itemcount; j++) ParseItem(name + " - 0x"+Helper.HexString(slotid)+" - I", ref list, mems);
				itemcount = Reader.ReadUInt32();
				for (int j=0; j<itemcount; j++) ParseItem(name + " - 0x"+Helper.HexString(slotid)+" - II", ref list, mems);
			}
		}

		protected override void ParseHeader() 
		{
			ArrayList list = new ArrayList();
			Hashtable mems = provider.OpcodeProvider.StoredMemories;

			byte[] header = Reader.ReadBytes(0x14);
			int textlen = Reader.ReadInt32();
			byte[] temp = Reader.ReadBytes(textlen);
			byte[] zero = Reader.ReadBytes(0x28);

			int blocklen = 0;
			blocklen = Reader.ReadInt32();
			ParseSlot("A", blocklen, ref list, mems);

			blocklen = Reader.ReadInt32();
			ParseSlot("B", blocklen, ref list, mems);

			blocklen = Reader.ReadInt32();
			ParseSlot("C", blocklen, ref list, mems);

			this.items = new GenericItem[list.Count];
			for (int i=0; i<list.Count; i++)
			{
				items[i] = (GenericItem)list[i];
			}

			throw new Exception("nothing wrong");
		}

		protected override void ParseFileItem(GenericItem item)
		{
			
		}

		public override string GetTypeName(uint type)
		{
			return "Neighborhood Data";
		}
		#endregion		

		/// <summary>
		/// Saves the data representet by this Objet to the writer
		/// </summary>
		/// <param name="writer">The BinaryWriter</param>
		protected override void Serialize(System.IO.BinaryWriter writer)
		{						
		}		

		public void UpdateData(object newdata)
		{
		}		
	}
}