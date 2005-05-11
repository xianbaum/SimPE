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
	public class SMem: Generic
	{
		SimPe.Interfaces.IProviderRegistry prov;
		/// <summary>
		/// Constructor for the Class
		/// </summary>		
		/// <param name="names">A SimName Provider or null</param>
		public SMem(SimPe.Interfaces.IProviderRegistry prov) : base() 
		{
			this.prov = prov;
			Register(0xCD95548E, new CreateFileObject(CreateLIST2File));
		}	
	
		/// <summary>
		/// Constructr for the Class
		/// </summary>
		/// <param name="pfd"></param>
		/// <param name="package"></param>
		public SMem(IPackedFileDescriptor pfd, IPackageFile package) : base() 
		{
			Register(0xCD95548E, new CreateFileObject(CreateLIST2File));
			this.ProcessData(pfd, package);
		}	

		/// <summary>
		/// Constructr for the Class
		/// </summary>
		/// <param name="package"></param>
		public SMem(IPackageFile package) : base() 
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
				"Sim Unknown Wrapper",
				"Quaxi",
				"---",
				1
				); 
		}
		#endregion
		
		#region Generic.File Member
		protected override void ParseHeader()
		{			
			ArrayList simids = prov.SimFamilynameProvider.GetAllSimIDs();
			simids.Add((uint)0x6DD33865);
			ArrayList list = new ArrayList();
			Reader.BaseStream.Seek(0x63, SeekOrigin.Begin);
			ushort delimeter = (ushort)this.FileDescriptor.Instance;

			try 
			{
				long start = 0x00;
				for (long i=start; i<(Reader.BaseStream.Length-2); i++)
				{
					Reader.BaseStream.Seek(i, SeekOrigin.Begin);
					ushort simid = Reader.ReadUInt16();
					
					if //(simids.Contains(simid))
					   (simid == delimeter) 
					{
						long pos = Reader.BaseStream.Position - 2;
						Reader.BaseStream.Seek(start, SeekOrigin.Begin);

						GenericItem item = new GenericItem();
						item.Properties.Add("Instance", Helper.HexString(Reader.ReadUInt16()));

						byte[] data = Reader.ReadBytes(Math.Max(0, (int)(pos - (start + 2))));
						string sdata="";
						foreach (byte b in data) sdata += Helper.HexString(b)+" ";
						item.Properties.Add("Data", sdata);
						item.Properties.Add("BinData", data);
						item.Properties.Add("Datalen", data.Length);
						item.Properties.Add("Offset", Helper.HexString((uint)start));
						list.Add(item);

						start = pos;
					}
				}
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("The experimental SMem Wrapper caused an Exception!", ex);
			}

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
			return "Sim Memories";
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