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
#define INDEX_SEARCH
//#define VAR_ONLY
//#define USE_TREE
using System;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Plugin.Internal;
using SimPe.Interfaces;
using SimPe.Interfaces.Files;

namespace SimPe.PackedFiles.Wrapper
{

	/// <summary>
	/// Handles CRES Conform Files
	/// </summary>
	public class Cres : Generic
	{
			

		/// <summary>
		/// Constructor of the class
		/// </summary>
		/// <param name="filecontent">Content of the File as Byte Array</param>
		public Cres() : base() 
		{
			Register(0xE519C933, new CreateFileObject(CreateFile)); //CRES		
		}
		
		private static string vars = "";
#if VAR_ONLY
		protected void ProcessData(GenericItem baseitem, System.Collections.ArrayList basedata, ref System.Collections.ArrayList list)
#else
		protected void ProcessData(GenericItem baseitem, System.Collections.ArrayList basedata)
#endif		
		{
			System.DateTime starttime = System.DateTime.Now;
			string types = "";
			byte[] bdata = new byte[basedata.Count];
			basedata.CopyTo(bdata);
			System.IO.BinaryReader br = new System.IO.BinaryReader(new System.IO.MemoryStream(bdata));

			string ALLOWED="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";
			string eALLOWED=((char)0).ToString() + ((char)13).ToString();
			//ALLOWED += eALLOWED;
#if VAR_ONLY
				
#else
			System.Collections.ArrayList list = new System.Collections.ArrayList();
#endif
			
			uint lastoffset = 0;
			uint laststart = 0;
			uint count = 0;
			try 
			{
				for (uint i=3; i<bdata.Length; i++)
				{
					System.TimeSpan time = System.DateTime.Now.Subtract(starttime);
					if (time.Minutes>0) throw new Exception("Timeout occured!");
					br.BaseStream.Seek(i, System.IO.SeekOrigin.Begin);

					byte length = br.ReadByte();
					uint start = i;
					bool check = true;
					bool nonzero = false;
					string name = "";
				
					if (i+length>bdata.Length-1) length = (byte)Math.Max(0, (bdata.Length -1) - i);
					for (int k=0; k<length; k++) 
					{
						char c = (char)br.ReadByte();
						if ((ALLOWED.IndexOf(c)<0)  && (c!=0))
						{
							check = false;
							break;
						}
						if (c!=0) nonzero=true;
						name += c;
					}

					if ((check) && (length>0) &&(nonzero))
					{
				
						GenericItem item = new GenericItem();
						if ((list.Count>0) )
						{
						
							GenericItem last = (GenericItem)list[list.Count-1];

							br.BaseStream.Seek(lastoffset, System.IO.SeekOrigin.Begin);
							uint dif = 0;
							if (start>lastoffset) dif = (start - lastoffset);
							if (dif>4) dif-=4;
							else dif=0;

							if (count==0) dif++;

							byte[] data = new byte[0];
							byte[] followup = new byte[4];
							string sdata = "";
							if (dif>0) 
							{
								data = new byte[dif];
								
								
								for (int j=Math.Max(0, 0); j<data.Length; j++) 
								{
									data[j] = br.ReadByte();
									string d = data[j].ToString("X");
									if (d.Length==1) d= "0"+d;
									sdata += d + " ";
								}	
								last.Properties["Data"] = data;
								last.Properties["DataLong"] = sdata;
								last.Properties["DataLength"] = data.Length;																												
							} 
							else 
							{
								last.Properties["Data"] = new byte[0];
								last.Properties["DataLong"] = "";
								last.Properties["DataLength"] = 0;
							}
							sdata = "";
							try 
							{
								for (int j=0; j<followup.Length; j++) 
								{
									followup[j] = br.ReadByte();
									string d = followup[j].ToString("X");
									if (d.Length==1) d= "0"+d;
									sdata += d + " ";
								}	
							} 
							catch (Exception) 
							{
							}

							string t = ((uint)last.Properties["Type"]).ToString("X");
							while (t.Length<8) t = "0"+t;
							string bt = ((uint)baseitem.Properties["Type"]).ToString("X");
							while (bt.Length<8) bt = "0"+bt;
							string dum = "INSERT INTO Variables (blocktype, vartype, name, datalen, bindata) VALUES ('0x"+bt+"', '0x"+t+"', '"+name.Trim()+"', "+data.Length.ToString()+", '"+sdata+"');\r\n";
							vars += dum;	
						}

						lastoffset = start+length+1;
						laststart = start;
						br.BaseStream.Seek(start-4, System.IO.SeekOrigin.Begin);
						uint type = br.ReadUInt32();

						item.Properties["Type"] = type;
						item.Properties.Add("Name", name);					
						item.Properties.Add("Data", null);
						
						
						list.Add(item);


						i+= (uint)(length);
					}


				}//for

				if ((count>0) && (list.Count>0)) 
				{
					list.Remove(list[list.Count-1]);					
				}
			
#if VAR_ONLY
#else
#if USE_TREE
#else
				string typenames = "";	
				string datas = "";		
				int datalen = 0;
				foreach (GenericItem item in list)
				{
					uint type = (uint)item.Properties["Type"];
					string t = type.ToString("X");
					while (t.Length<8) t= "0"+t;
					types += "0x"+t+", ";
					byte[] b = (byte[])item.Properties["Data"];
					if (b==null) b= new byte[0];
					typenames += item.Properties["Name"].ToString()+" ("+b.Length.ToString("X")+"), ";					
					if (b!=null) 
					{
						for (int i=Math.Max(0, b.Length-2); i<b.Length; i++) 
						{
							string d = b[i].ToString("X");
							if (d.Length==1) d= "0"+d;
							datas +=  d + " ";
						}
					}					
					datas += ", ";
					if (item.Properties["DataSize"] != null)
						datalen += (int)(item.Properties["DataSize"]);
				}

				//baseitem.Properties["DataLong"] = datas;
				baseitem.Properties["TypeIDs"] = types;
				baseitem.Properties["TypeNames"] = typenames;
				baseitem.Properties["TypeCount"] = list.Count;
				baseitem.Properties["SubDataLength"] = datalen;
				string s = "";
				for (int j=0; j<bdata.Length; j++) 
				{
					string d = bdata[j].ToString("X");
					if (d.Length==1) d= "0"+d;

					if ((j<8) || (j>=bdata.Length-8))
						s += d + " ";
					if (j==8) s+= "... ";
				}
#endif
#endif		
			} 
			catch (Exception) 
			{
				types += "???";
			}

			items = new GenericItem[list.Count];
			for(int i=0; i<list.Count; i++) 
			{
				items[i] = (GenericItem)list[i];
			}
			baseitem.Subitems = items;
		}

		/// <summary>
		/// Creates a CRES File Reader
		/// </summary>
		/// <param name="data">The Binary Data of the File</param>
		/// <returns>The Reder in a generic Format</returns>
		protected SimPe.PackedFiles.Wrapper.Generic CreateFile(IPackedFileWrapper wrapper)
		{
			return this;
		}

		internal virtual SimPe.PackedFiles.Wrapper.Generic CreateSignatureBasedFileObject(IPackedFileWrapper wrapper)
		{
			return this;
		}

		public override Byte[] FileSignature
		{
			get 
			{
				byte[] b = {0x01, 0x00, 0xff, 0xff};
				return new byte[0];
			}
		}

		#region IWrapper Member
		protected override IWrapperInfo CreateWrapperInfo()
		{
			return new AbstractWrapperInfo(
				"CRES Wrapper",
				"Quaxi",
				"---",
				1
				); 
		}
		#endregion

		#region Generic.File Member


#if INDEX_SEARCH
		protected override void ParseHeader()
		{	
			Reader.BaseStream.Seek(4, System.IO.SeekOrigin.Begin);
			uint count = Reader.ReadUInt32(); //Reference File Count
			for (uint i=0; i<count; i++)
			{
				Reader.ReadUInt32(); //Group
				Reader.ReadUInt32(); //Instance
				Reader.ReadUInt32(); //ClassID
				Reader.ReadUInt32(); //TypeID
			}
			count = Reader.ReadUInt32(); //Index Size

			System.Collections.ArrayList list = new System.Collections.ArrayList();
			uint[] index = new uint[count];
			//Reader.BaseStream.Seek(0x001c, System.IO.SeekOrigin.Begin);
			for(int i=0; i<index.Length; i++) 
			{
				index[i] = Reader.ReadUInt32();
			}

			string s = "";
			int pos = 0;
			GenericItem item = null;
			System.Collections.ArrayList data = new System.Collections.ArrayList();

			long baseoffset = Reader.BaseStream.Position;
			for (long i=baseoffset; i<Reader.BaseStream.Length-4; i++) 
			{
				Reader.BaseStream.Seek(i, System.IO.SeekOrigin.Begin);
				byte b = Reader.ReadByte();
				data.Add(b);
				s += GenericCommon.ToPrintableChar((char)b, '.');	

				Reader.BaseStream.Seek(i, System.IO.SeekOrigin.Begin);
				uint id = Reader.ReadUInt32();
				if ((id==index[pos]) || (i==Reader.BaseStream.Length-5))//|| (id==0xE9075BC5) || (id==0xE519C933) || (id==0x6A836D56) || (id==0x65246462) || (id==0x65245517))
				{
					if (item!=null) 
					{																
						item.Properties["DataLength"] = s.Length;
						item.Properties["Data"] = s;
#if VAR_ONLY
						ProcessData(item, data, ref list);									
#else
						ProcessData(item, data);
						list.Add(item);		
#endif						
					}					

					item = new GenericItem();
					item.Properties["Name"] = pos;
					item.Properties["Type"] = id;
					item.Properties["Offset"] = i;
#if USE_TREE
#else
					item.Properties["DataLong"] = "";
#endif
					

					if (id==index[pos]) pos++;
						
					if (pos>=index.Length) pos = (index.Length-1);

					s = "";
					data = new System.Collections.ArrayList();
					i+=3;
				}
			}

			System.IO.StreamWriter tw = System.IO.File.CreateText("p:\\vars.txt");
			tw.Write(vars);
			tw.Close();
			
			items = new GenericItem[list.Count];
			for(int i=0; i<list.Count; i++) 
			{
				items[i] = (GenericItem)list[i];
			}
			throw new Exception("Nothing Wrong");
			
		}
#else
		protected override void ParseHeader()
		{	
			string ALLOWED="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";
			string eALLOWED=((char)0).ToString() + ((char)13).ToString();
			ALLOWED += eALLOWED;

			System.Collections.ArrayList list = new System.Collections.ArrayList();
			uint lastoffset = 0;
			uint laststart = 0;
			for (uint i=0x02E0; i<Reader.BaseStream.Length - 0x80; i++) 
			{
				Reader.BaseStream.Seek(i, System.IO.SeekOrigin.Begin);

				byte length = Reader.ReadByte();
				uint start = i;
				bool check = true;
				bool nonzero = false;
				string name = "";
				
				for (int k=0; k<length; k++) 
				{
					char c = (char)Reader.ReadByte();
					if ((ALLOWED.IndexOf(c)<0)  && (c!=0))
					{
						check = false;
						break;
					}
					if (c!=0) nonzero=true;
					name += c;
				}

				if ((check) && (length>0) &&(nonzero))
				{
					GenericItem item = new GenericItem();
					if (list.Count>0) 
					{
						
						Reader.BaseStream.Seek(lastoffset, System.IO.SeekOrigin.Begin);
						uint dif = (start - lastoffset)+1;
						byte[] data = new byte[dif-1];
						string sdata = "";
						for (int j=0; j<data.Length; j++) 
						{
							data[j] = Reader.ReadByte();
							string d = data[j].ToString("X");
							if (d.Length==1) d= "0"+d;
							sdata += d + " ";
						}
						GenericItem last = (GenericItem)list[list.Count-1];

						if (data.Length==0x08) 
						{
							last.Properties["Type?"] = "_"+last.Properties["Type?"].ToString();
						}
						last.Properties["Data"] = data;
						last.Properties["DataLong"] = sdata;
						last.Properties["DataSize"] = data.Length;
						last.Properties["BlockSpan"] = start - laststart;
					}
					lastoffset = start+length+1;
					laststart = start;
					Reader.BaseStream.Seek(start-4, System.IO.SeekOrigin.Begin);
					uint type = Reader.ReadUInt32();
					item.Properties["Type?"] = type;

					item.Properties.Add("Properties", name);
					
					item.Properties.Add("Data", null);
					item.Properties.Add("Length", length);
					item.Properties.Add("Offset", start);
					list.Add(item);

					i+= (uint)(length);
				}
			}
			
			items = new GenericItem[list.Count];
			for(int i=0; i<list.Count; i++) 
			{
				items[i] = (GenericItem)list[i];
			}
			throw new Exception("Nothing Wrong");
			
		}
#endif
		
		protected override void ParseFileItem(GenericItem item)
		{
			
		}

		/// <summary>
		/// This Contains the last 4 Bytes readed by ReadByte()
		/// </summary>
		private byte[] readbuffer;
		/// <summary>
		/// One possible EndToken
		/// </summary>
		private byte[] match1 = {0xF7, 0xE4, 0x61, 0xEB};
		/// <summary>
		/// Another possible Endtoken
		/// </summary>
		private byte[] match2 = {0x18, 0xEA, 0x8B, 0x0B};
		/// <summary>
		/// Another possible Endtoken
		/// </summary>
		private byte[] match3 = {0x08, 0x87, 0xC7, 0xAB};

		/// <summary>
		/// Returns the Byte on the current Possition of the Reader and stores 
		/// the last 4 Bytes in a Bufer
		/// </summary>
		/// <returns></returns>
		private byte ReadByte()
		{
			readbuffer[0] = readbuffer[1];
			readbuffer[1] = readbuffer[2];
			readbuffer[2] = readbuffer[3];
			readbuffer[3] = Reader.ReadByte();
			return readbuffer[3];
		}

		/// <summary>
		/// Resets the ReadByte() Buffer.
		/// </summary>
		private void BufferReset()
		{
			readbuffer = new byte[4];
			readbuffer[0] = 0;
			readbuffer[1] = 0;
			readbuffer[2] = 0;
			readbuffer[3] = 0;
		}

		/// <summary>
		/// Checks if the bytes read int the Buffer match with the passed pattern
		/// </summary>
		/// <param name="match">The pattern to match</param>
		/// <returns>true if buffer and match contain the same pattern</returns>
		private bool BufferMatch(byte[] match) 
		{
			for (int i=0; i< Math.Min(match.Length, readbuffer.Length); i++)
			{
				if (match[i]!=readbuffer[i]) return false;
			}
			return true;
		}

		public override string GetTypeName(uint type)
		{
			return "CRES (experimental)";
		}
		#endregion
	}
}
