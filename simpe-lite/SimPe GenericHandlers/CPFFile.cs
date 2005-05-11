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
using SimPe.Data;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Plugin.Internal;
using SimPe.Interfaces;
using SimPe.Interfaces.Files;


namespace SimPe.PackedFiles.Wrapper
{
	/// <summary>
	/// Handles cpf Conform Files
	/// </summary>
	public class CpfG : Generic, SimPe.Interfaces.Plugin.IFileWrapperSaveExtension
	{
		protected byte[] Id 
		{
			get 
			{
				object o = Attributes.Properties["ID"];
				
				if (o==null) return new byte[6];
				else return (byte[])o;
			}
		}

		/// <summary>
		/// Checks if the Buffer Data is a Valid cpf resource
		/// </summary>
		/// <returns>true if the data stored in the buffer is a valid cpf resource</returns>
		public bool IsValid
		{
			get 
			{
				return ((Id[0]==0xE0) &&
						(Id[1]==0x50) &&
						(Id[2]==0xE7) &&
						(Id[3]==0xCB) &&
						(Id[4]==0x02) &&
						(Id[5]==0x00));
			}
		}

		internal virtual SimPe.PackedFiles.Wrapper.Generic CreateSignatureBasedFileObject(IPackedFileWrapper wrapper)
		{
			return this;
		}

		#region IWrapper Member
		protected override IWrapperInfo CreateWrapperInfo()
		{
			return new AbstractWrapperInfo(
				"Generic CPF Wrapper",
				"Quaxi",
				"---",
				1
				); 
		}
		#endregion

		#region Generic.File Member
		/// <exception cref="Exception">An Exception is thrown whe the File represented by the Data does not have the correct Format.</exception>
		protected override void ParseHeader()
		{
			byte[] id = new byte[6];
			for (int i=0; i<id.Length; i++) id[i] = Reader.ReadByte();
			Attributes.Properties["ID"] = id;
			
			if (!IsValid) throw new Exception("You tried top process a non CPF File.");
		
			items = new GenericItem[Reader.ReadUInt32()];
		}
		
		protected override void ParseFileItem(GenericItem item)
		{
			//Load Datatype
			uint datatype = Reader.ReadUInt32();
			
			//Load the Name 
			UInt32 namelength;
			namelength = Reader.ReadUInt32();
			byte[] name = new Byte[namelength];
			for (int k=0; k<name.Length; k++) name[k] = Reader.ReadByte();
			
			//Load Value
			UInt32 valuelength;
			switch ((MetaData.DataTypes)datatype)
			{
				case (MetaData.DataTypes.dtString):
				{
					valuelength = Reader.ReadUInt32();
					break;
				}
				default:
				{
					valuelength = 4;
					break;
				}
			} //switch
			byte[] val = new Byte[valuelength];
			for (int k=0; k<val.Length; k++) val[k] = Reader.ReadByte();

			//Push the loaded Data to the Item
			item.Properties["Name"] = name;
			item.Properties["Value"] = val;
			item.Properties["Datatype"] = (MetaData.DataTypes)datatype;
		}

		public override string GetTypeName(uint type)
		{
			return "CPF";
		}

		public override Byte[] FileSignature
		{
			get 
			{
				Byte[] sig = {
								 0xE0,
								 0x50,
								 0xE7,
								 0xCB,
								 0x02,
								 0x00
							 };
				return sig;
			}
		}
		#endregion

		#region IPackedFileSaveExtension Member
		protected override void Serialize(System.IO.BinaryWriter writer)
		{
			byte[] id = (byte[])Attributes.Properties["ID"];
			
			for (int i=0; i<id.Length; i++) writer.Write(id[i]);
			writer.Write((uint)items.Length);
			

			foreach (CpfItemG item in items) 
			{
				writer.Write((uint)item.Datatype);
				writer.Write((uint)item.PlainName.Length);
				for (int k=0; k<item.PlainName.Length; k++) writer.Write(item.PlainName[k]);
			
				
				switch (item.Datatype)
				{
					case (MetaData.DataTypes.dtString):
					{						
						writer.Write((uint)item.StringValue.Length);
						for (int i=0; i<item.StringValue.Length; i++) 
						{
							writer.Write((byte)item.StringValue[i]);
						}
						break;
					}					
					case (MetaData.DataTypes.dtSingle):
					{
						writer.Write(item.SingleValue);
						break;
					}
					default:
					{
						writer.Write(item.IntegerValue);
						break;
					}
				} //switch
				
			}//foreach
		}


		#endregion
	}
}
