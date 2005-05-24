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
using System.Collections;
using System.IO;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Plugin.Internal;
using SimPe.Interfaces;
using SimPe.Interfaces.Files;

namespace SimPe.PackedFiles.Wrapper
{

	/// <summary>
	/// Handles Test Conform Files
	/// </summary>
	public class GenericTtab : Generic
	{
		/// <summary>
		/// Constructor of the class
		/// </summary>
		/// <param name="filecontent">Content of the File as Byte Array</param>
		public GenericTtab() : base() 	
		{
			Register(0x54544142, new CreateFileObject(CreateSTRFile)); //TTAB			
		}

		/// <summary>
		/// Creates a STR File Reader
		/// </summary>
		/// <param name="data">The Binary Data of the File</param>
		/// <returns>The Reder in a generic Format</returns>
		protected SimPe.PackedFiles.Wrapper.Generic CreateSTRFile(IPackedFileWrapper wrapper)
		{
			return this;
		}
	
		#region IWrapper Member
		protected override IWrapperInfo CreateWrapperInfo()
		{
			return new AbstractWrapperInfo(
				"Ultra Experimental TTAB Wrapper",
				"Quaxi",
				"---",
				1
				); 
		}
		#endregion
		
		#region Generic.File Member		
		uint version;
		uint zero;
		ushort count;
		protected override void ParseHeader()
		{
			if (!Helper.DebugMode) return;

			byte [] flname = Reader.ReadBytes(0x40);

			Reader.ReadUInt32();	//0xffffffff
			version = Reader.ReadUInt32();	
			zero = Reader.ReadUInt32();
			count = Reader.ReadUInt16();

			if (count==0) return;

			ArrayList list = new ArrayList();
			long pos = Reader.BaseStream.Position;
			long last = pos;
			GenericItem item = null;
			for (long i=pos; i<Reader.BaseStream.Length-4; i++)
			{				
				Reader.BaseStream.Seek(i, SeekOrigin.Begin);
				uint dword = Reader.ReadUInt32();

				if (dword==0xffffffff) 
				{					
					Reader.BaseStream.Seek(-1 * 0x38, System.IO.SeekOrigin.Current);					
					long now = Reader.BaseStream.Position;
					
					if (item!=null) 
					{
						item.Properties["Data length"] = (int)(now - last);
						item.Properties["Oversized"] = (now - last) - ((uint)item.Properties["Total 1"] * 6);
					}

					item = new GenericItem();
					item.Properties["Action"] = Reader.ReadUInt16();
					item.Properties["Guard"] = Reader.ReadUInt16();

					string header = "";
					uint r = 0;
					uint all = 0;
					r = Reader.ReadUInt32(); header += Helper.HexString(r) + " "; all += r;
					r = Reader.ReadUInt32(); header += Helper.HexString(r) + " "; all += r;
					r = Reader.ReadUInt32(); header += Helper.HexString(r) + " "; all += r;
					r = Reader.ReadUInt32(); header += Helper.HexString(r) + " "; all += r;
					r = Reader.ReadUInt32(); header += Helper.HexString(r) + " "; all += r;
					r = Reader.ReadUInt32(); header += Helper.HexString(r) + " "; all += r;
					r = Reader.ReadUInt32(); header += Helper.HexString(r) + " "; all += r;
					item.Properties["Header 1"] = header;
					item.Properties["Total 1"] = all;
					


					header = ""; r = 0; all = 0;
					r = Reader.ReadUInt16(); header += Helper.HexString(r) + " "; all += r;
					r = Reader.ReadUInt16(); header += Helper.HexString(r) + " "; all += r;
					item.Properties["Header 2"] = header;

					header = ""; r = 0; all = 0;
					r = Reader.ReadUInt32(); header += Helper.HexString(r) + " "; all += r;
					r = Reader.ReadUInt32(); header += Helper.HexString(r) + " "; all += r;
					r = Reader.ReadUInt32(); header += Helper.HexString(r) + " "; all += r;
					r = Reader.ReadUInt32(); header += Helper.HexString(r) + " "; all += r;
					item.Properties["Header 3"] = header;
					
					
					

					list.Add(item);

					Reader.ReadUInt32();
					last = Reader.BaseStream.Position;
				}
			}
			if (item!=null) item.Properties["Data length"] = -1;

			items = new GenericItem[list.Count];
			list.CopyTo(items);

			throw new Exception("Nothing Wrong");
		}
		
		protected override void ParseFileItem(GenericItem item)
		{
			
		}

		public override string GetTypeName(uint type)
		{
			return "Ultra Experimental TTAB v."+Helper.HexString(version)+" ("+count.ToString()+" Items)";
		}
		#endregion		
	}
}

