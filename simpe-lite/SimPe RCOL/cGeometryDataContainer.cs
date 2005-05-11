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
using System.Globalization;
using SimPe.Plugin.Gmdc;

namespace SimPe.Plugin
{	
	#region GeometryDataContainerItem2
	public class GeometryDataContainerItem2
	{
		#region Attributes

		int[] items1;
		public int[] Items1 
		{
			get { return items1; }
			set { items1 = value; }
		}

		int unknown1;
		public int VertexCount 
		{
			get { return unknown1; }
			set { unknown1 = value; }
		}

		int unknown2;
		public int Unknown2 
		{
			get { return unknown2; }
			set { unknown2 = value; }
		}

		int[] items2;
		public int[] Items2 
		{
			get { return items2; }
			set { items2 = value; }
		}

		int[] items3;
		public int[] Items3 
		{
			get { return items3; }
			set { items3 = value; }
		}

		int[] items4;
		public int[] Items4 
		{
			get { return items4; }
			set { items4 = value; }
		}
		

		GeometryDataContainer parent;
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public GeometryDataContainerItem2(GeometryDataContainer parent)
		{
			items1 = new int[0];
			items2 = new int[0];
			items3 = new int[0];
			items4 = new int[0];

			this.parent = parent;
		}

			
		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		public  void Unserialize(System.IO.BinaryReader reader)
		{
			items1 = new int[reader.ReadInt32()];
			for (int i=0; i<items1.Length; i++)
			{
				if (parent.Version==0x04) items1[i] = reader.ReadInt16();
				else items1[i] = reader.ReadInt32();
			}

			unknown1 = reader.ReadInt32();
			unknown2 = reader.ReadInt32();

			items2 = new int[reader.ReadInt32()];
			for (int i=0; i<items2.Length; i++)
			{
				if (parent.Version==0x04) items2[i] = reader.ReadInt16();
				else items2[i] = reader.ReadInt32();
			}

			items3 = new int[reader.ReadInt32()];
			for (int i=0; i<items3.Length; i++)
			{
				if (parent.Version==0x04) items3[i] = reader.ReadInt16();
				else items3[i] = reader.ReadInt32();
			}

			items4 = new int[reader.ReadInt32()];
			for (int i=0; i<items4.Length; i++)
			{
				if (parent.Version==0x04) items4[i] = reader.ReadInt16();
				else items4[i] = reader.ReadInt32();
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
		public  void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write((int)items1.Length);
			for (int i=0; i<items1.Length; i++)
			{
				if (parent.Version==0x04) writer.Write((short)items1[i]);
				else writer.Write((int)items1[i]);
			}

			writer.Write(unknown1);
			writer.Write(unknown2);

			writer.Write((int)items2.Length);
			for (int i=0; i<items2.Length; i++)
			{
				if (parent.Version==0x04) writer.Write((short)items2[i]);
				else writer.Write((int)items2[i]);
			}

			writer.Write((int)items3.Length);
			for (int i=0; i<items3.Length; i++)
			{
				if (parent.Version==0x04) writer.Write((short)items3[i]);
				else writer.Write((int)items3[i]);
			}

			writer.Write((int)items4.Length);
			for (int i=0; i<items4.Length; i++)
			{
				if (parent.Version==0x04) writer.Write((short)items4[i]);
				else writer.Write((int)items4[i]);
			}
		}

		public override string ToString()
		{
			return "0x"+Helper.HexString((uint)this.unknown1) + " 0x"+Helper.HexString(this.unknown2);
		}
	}
	#endregion

	#region GeometryDataContainerItem3
	public class GeometryDataContainerItem3
	{
		#region Attributes

		int unknown1;
		public int Unknown1 
		{
			get { return unknown1; }
			set { unknown1 = value; }
		}

		int alternate;
		public int Alternate 
		{
			get { return alternate; }
			set { alternate = value; }
		}

		string name;
		public String Name 
		{
			get { return name; }
			set { name = value; }
		}
		
		int[] items1;
		public int[] Items1 
		{
			get { return items1; }
			set { items1 = value; }
		}

		int opacity;
		public int Opacity 
		{
			get { return opacity; }
			set { opacity = value; }
		}

		int[] items2;
		public int[] Items2 
		{
			get { return items2; }
			set { items2 = value; }
		}
		

		GeometryDataContainer parent;
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public GeometryDataContainerItem3(GeometryDataContainer parent)
		{
			items1 = new int[0];
			items2 = new int[0];
			name = "";

			this.parent = parent;
		}

			
		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		public  void Unserialize(System.IO.BinaryReader reader)
		{
			unknown1 = reader.ReadInt32();
			alternate = reader.ReadInt32();
			name = reader.ReadString();

			items1 = new int[reader.ReadInt32()];
			for (int i=0; i<items1.Length; i++)
			{
				if (parent.Version==0x04) items1[i] = reader.ReadInt16();
				else items1[i] = reader.ReadInt32();
			}

			if ((parent.Version==0x01) || (parent.Version==0x02) || (parent.Version==0x04)) opacity = reader.ReadInt32();
			else opacity = 0;

			if ((parent.Version==0x02) || (parent.Version==0x04)) 
			{
				items2 = new int[reader.ReadInt32()];
				for (int i=0; i<items2.Length; i++)
				{
					if (parent.Version==0x04) items2[i] = reader.ReadInt16();
					else items2[i] = reader.ReadInt32();
				}
			} 
			else 
			{
				items2 = new int[0];
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
		public  void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(unknown1);
			writer.Write(alternate);
			writer.Write(name);

			writer.Write((int)items1.Length);
			for (int i=0; i<items1.Length; i++)
			{
				if (parent.Version==0x04) writer.Write((short)items1[i]);
				else writer.Write((int)items1[i]);
			}

			if ((parent.Version==0x01) || (parent.Version==0x02) || (parent.Version==0x04)) writer.Write(opacity);

			if ((parent.Version==0x02) || (parent.Version==0x04)) 
			{
				writer.Write((int)items2.Length);
				for (int i=0; i<items2.Length; i++)
				{
					if (parent.Version==0x04) writer.Write((short)items2[i]);
					else writer.Write((int)items2[i]);
				}
			} 
		}

		public override string ToString()
		{
			return name;
		}
	}
	#endregion

	#region GeometryDataContainerItem4
	public class GeometryDataContainerItem4
	{
		#region Attributes
		byte[] data;
		GeometryDataContainer parent;
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public GeometryDataContainerItem4(GeometryDataContainer parent)
		{
			data = new byte[0];
			this.parent = parent;
		}

			
		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		public  void Unserialize(System.IO.BinaryReader reader)
		{
			data = reader.ReadBytes(0x1c);
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		public  void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(data);
		}
	}
	#endregion

	#region GeometryDataContainerItem5
	public class GeometryDataContainerItem5
	{
		#region Attributes
		string name1;
		string name2;
		GeometryDataContainer parent;
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public GeometryDataContainerItem5(GeometryDataContainer parent)
		{
			name1 = "";
			name2 = "";
			this.parent = parent;
		}

			
		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		public  void Unserialize(System.IO.BinaryReader reader)
		{
			name1 = reader.ReadString();
			name2 = reader.ReadString();
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		public  void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(name1);
			writer.Write(name2);
		}
	}
	#endregion

	#region GeometryDataContainerItem6
	public class GeometryDataContainerItem6
	{
		#region Attributes
		Point3D[] points;
		int[] items;
		GeometryDataContainer parent;
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public GeometryDataContainerItem6(GeometryDataContainer parent)
		{
			points = new Point3D[0];
			items = new int[0];
			this.parent = parent;
		}

			
		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		public  void Unserialize(System.IO.BinaryReader reader)
		{
			points = new Point3D[reader.ReadUInt32()];
			if (points.Length>0) 
			{
				items = new int[reader.ReadUInt32()];
				for (int i=0; i<points.Length; i++)
				{
					points[i] = new Point3D();
					points[i].X = reader.ReadInt32();
					points[i].Y = reader.ReadInt32();
					points[i].Z = reader.ReadInt32();
				}

				for (int i=0; i<items.Length; i++)
				{
					if (parent.Version==0x04) items[i] = reader.ReadInt16();
					else items[i] = reader.ReadInt32();
				}
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
		public  void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write((uint)points.Length);
			if (points.Length>0) 
			{
				writer.Write((uint)items.Length);
				for (int i=0; i<points.Length; i++)
				{
					writer.Write((int)points[i].X);
					writer.Write((int)points[i].Y);
					writer.Write((int)points[i].Z);
				}

				for (int i=0; i<items.Length; i++)
				{
					if (parent.Version==0x04) writer.Write((short)items[i]);
					else writer.Write((int)items[i]);
				}
			}
		}
	}
	#endregion

	/// <summary>
	/// Zusammenfassung für cGeometryDataContainer.
	/// </summary>
	public class GeometryDataContainer
		: AbstractRcolBlock
	{
		#region Attributes

		protected GeometryDataContainerItem1[] items1;
		public GeometryDataContainerItem1[] P1Vetices
		{
			get { return items1; }
			set { items1 = value; }
		}
		protected GeometryDataContainerItem2[] items2;
		public GeometryDataContainerItem2[] P2VertexLink
		{
			get { return items2; }
			set { items2 = value; }
		}

		protected GeometryDataContainerItem3[] items3;
		public GeometryDataContainerItem3[] P3Model
		{
			get { return items3; }
			set { items3 = value; }
		}

		protected GeometryDataContainerItem4[] items4;
		public GeometryDataContainerItem4[] P4a
		{
			get { return items4; }
			set { items4 = value; }
		}
		protected GeometryDataContainerItem5[] items5;
		public GeometryDataContainerItem5[] P4b
		{
			get { return items5; }
			set { items5 = value; }
		}
		
		protected GeometryDataContainerItem6 item6;
		public GeometryDataContainerItem6 P4c
		{
			get { return item6; }
			set { item6 = value; }
		}
		protected GeometryDataContainerItem6[] items6;
		public GeometryDataContainerItem6[] P5
		{
			get { return items6; }
			set { items6 = value; }
		}
		#endregion
		

		/// <summary>
		/// Constructor
		/// </summary>
		public GeometryDataContainer(Interfaces.IProviderRegistry provider, Rcol parent) : base(provider, parent)
		{
			sgres = new SGResource(provider, null);

			items1 = new GeometryDataContainerItem1[0];
			items2 = new GeometryDataContainerItem2[0];
			items3 = new GeometryDataContainerItem3[0];
			items4 = new GeometryDataContainerItem4[0];
			items5 = new GeometryDataContainerItem5[0];
			items6 = new GeometryDataContainerItem6[0];
			item6 = new GeometryDataContainerItem6(this);

			version = 0x04;
			BlockID = 0xAC4F8687;
		}
		
		#region IRcolBlock Member

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		public override void Unserialize(System.IO.BinaryReader reader)
		{
			version = reader.ReadUInt32();

			string name = reader.ReadString();
			uint myid = reader.ReadUInt32();		
			sgres.Unserialize(reader);
			sgres.BlockID = myid;

			items1 = new GeometryDataContainerItem1[reader.ReadInt32()];
			for (int i=0; i<items1.Length; i++)
			{
				items1[i] = new GeometryDataContainerItem1(this);
				items1[i].Unserialize(reader);
			}

			items2 = new GeometryDataContainerItem2[reader.ReadInt32()];
			for (int i=0; i<items2.Length; i++)
			{
				items2[i] = new GeometryDataContainerItem2(this);
				items2[i].Unserialize(reader);
			}

			items3 = new GeometryDataContainerItem3[reader.ReadInt32()];
			for (int i=0; i<items3.Length; i++)
			{
				items3[i] = new GeometryDataContainerItem3(this);
				items3[i].Unserialize(reader);
			}

			items4 = new GeometryDataContainerItem4[reader.ReadInt32()];
			for (int i=0; i<items4.Length; i++)
			{
				items4[i] = new GeometryDataContainerItem4(this);
				items4[i].Unserialize(reader);
			}

			items5 = new GeometryDataContainerItem5[reader.ReadInt32()];
			for (int i=0; i<items5.Length; i++)
			{
				items5[i] = new GeometryDataContainerItem5(this);
				items5[i].Unserialize(reader);
			}

			item6.Unserialize(reader);

			items6 = new GeometryDataContainerItem6[reader.ReadInt32()];
			for (int i=0; i<items6.Length; i++)
			{
				items6[i] = new GeometryDataContainerItem6(this);
				items6[i].Unserialize(reader);
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
		public override void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(version);

			writer.Write(sgres.BlockName);
			writer.Write(sgres.BlockID);
			sgres.Serialize(writer);

			writer.Write((int)items1.Length);
			for (int i=0; i<items1.Length; i++) items1[i].Serialize(writer);

			writer.Write((int)items2.Length);
			for (int i=0; i<items2.Length; i++) items2[i].Serialize(writer);

			writer.Write((int)items3.Length);
			for (int i=0; i<items3.Length; i++) items3[i].Serialize(writer);

			writer.Write((int)items4.Length);
			for (int i=0; i<items4.Length; i++) items4[i].Serialize(writer);

			writer.Write((int)items5.Length);
			for (int i=0; i<items5.Length; i++) items5[i].Serialize(writer);

			item6.Serialize(writer);

			writer.Write((int)items6.Length);
			for (int i=0; i<items6.Length; i++) items6[i].Serialize(writer);
		}

		fGeometryDataContainer form = null;
		public override System.Windows.Forms.TabPage TabPage
		{
			get
			{
				if (form==null) form = new fGeometryDataContainer(); 
				return form.tMain;
			}
		}
		#endregion

		/// <summary>
		/// You can use this to setop the Controls on a TabPage befor it is dispplayed
		/// </summary>
		protected override void InitTabPage() 
		{
			if (form==null) form = new fGeometryDataContainer(); 
			form.tb_ver.Text = "0x"+Helper.HexString(this.version);

			form.lb_itemsa2.Items.Clear();
			form.lb_itemsa.Items.Clear();
			foreach (GeometryDataContainerItem1 i in this.items1) form.lb_itemsa.Items.Add(i); 

			form.lb_itemsb2.Items.Clear();
			form.lb_itemsb3.Items.Clear();
			form.lb_itemsb4.Items.Clear();
			form.lb_itemsb5.Items.Clear();
			form.lb_itemsb.Items.Clear();
			foreach (GeometryDataContainerItem2 i in this.items2) form.lb_itemsb.Items.Add(i); 

			form.lb_itemsc2.Items.Clear();
			form.lb_itemsc3.Items.Clear();
			form.lb_itemsc.Items.Clear();
			form.lbmodel.Items.Clear();
			foreach (GeometryDataContainerItem3 i in this.items3) 
			{
				form.lb_itemsc.Items.Add(i); 
				form.lbmodel.Items.Add(i, true);
			}

		}

		public override void ExtendTabControl(System.Windows.Forms.TabControl tc)
		{
			form.tGeometryDataContainer.Tag = this;
			tc.TabPages.Add(form.tGeometryDataContainer);

			form.tGeometryDataContainer2.Tag = this;
			tc.TabPages.Add(form.tGeometryDataContainer2);

			form.tGeometryDataContainer3.Tag = this;
			tc.TabPages.Add(form.tGeometryDataContainer3);
		}
	
		#region obj Files
		/// <summary>
		/// Generates a .obj File for ONE Model
		/// </summary>
		/// <param name="model">The Model you want to use</param>
		/// <param name="modelnr">Number of that Model in the GMDC (0 based)</param>
		/// <param name="vertexoffset">number of previous Vertices</param>
		/// <returns>The content of the obj File</returns>
		/// <remarks>Doesn't generate any Header informations</remarks>
		protected MemoryStream GenerateModelObj(GeometryDataContainerItem3 model, int modelnr, ref int vertexoffset)
		{
			System.IO.StreamWriter sw = new StreamWriter(new MemoryStream());
						
			modelInfo info1;
				
			GeometryDataContainerItem2 modelitem = (GeometryDataContainerItem2) this.P2VertexLink[model.Alternate];
			info1 = new modelInfo();
			info1.vertexDataList = modelitem.Items1[0];
			info1.normalDataList = modelitem.Items1[1];
			info1.tuDataList = modelitem.Items1[2];

			sw.WriteLine("# Object number: " + modelnr);
			sw.WriteLine("# VertexList ref: " + info1.vertexDataList);
			sw.WriteLine("g " + model.ToString());

				
			GeometryDataContainerItem1 item3 = (GeometryDataContainerItem1) this.P1Vetices[info1.vertexDataList];			
			int vertexcount = 0;
			for (int i = 0; i < Math.Min(item3.Data.Length, modelitem.VertexCount*3); i+=3)
			{
				vertexcount++;					
				sw.WriteLine("v " + 
					item3.Data[i].ToString(CultureInfo.InvariantCulture) + " "+
					item3.Data[i+1].ToString(CultureInfo.InvariantCulture) + " "+
					item3.Data[i+2].ToString(CultureInfo.InvariantCulture) + " "+
					"# " + (i/3 + vertexoffset));
			}


			item3 = (GeometryDataContainerItem1) this.P1Vetices[info1.normalDataList];				
			for (int i = 0; i < Math.Min(item3.Data.Length, modelitem.VertexCount*3); i+=3)
			{
				sw.WriteLine("vn " + 
					item3.Data[i].ToString(CultureInfo.InvariantCulture) + " "+
					item3.Data[i+1].ToString(CultureInfo.InvariantCulture) + " "+
					item3.Data[i+2].ToString(CultureInfo.InvariantCulture) + " "+
					"# " + (i/3));
			}


			item3 = (GeometryDataContainerItem1) this.P1Vetices[info1.tuDataList];
			for (int i = 0; i < Math.Min(item3.Data.Length, modelitem.VertexCount*2); i+=2)
			{
				sw.WriteLine("vt " + 
					item3.Data[i].ToString(CultureInfo.InvariantCulture) + " "+
					(-item3.Data[i+1]).ToString(CultureInfo.InvariantCulture) + " "+
					"# " + (i/2));
			}


			int[] numArray1 = item3.Items;
			int num12 = numArray1.Length;
				
			sw.WriteLine("# number of polygons: " + (model.Items1.Length / 3));
			if (modelnr > 0) sw.WriteLine("# vertsSoFar: " + ((vertexoffset+vertexcount) - 2).ToString());
			else sw.WriteLine("# vertsSoFar: 0");			
			sw.WriteLine("# totalVertices: " + (vertexoffset+vertexcount));
			sw.WriteLine("# vertGroupStart: " + vertexoffset);

			for (int i = 0; i < model.Items1.Length; i++)
			{
				int vertexnr = model.Items1[i] + 1 + vertexoffset;
				if (i%3 == 0)
				{
					sw.Write("f " +
						vertexnr.ToString() +  "/" + 
						vertexnr.ToString() +  "/" + 
						vertexnr.ToString());
				} 
				else if (i%3 == 1)
				{
					sw.Write(" " + vertexnr.ToString() +  "/" + 
						vertexnr.ToString() +  "/" + 
						vertexnr.ToString());
				} 
				else 
				{
					sw.WriteLine(" " + vertexnr.ToString() +  "/" + 
						vertexnr.ToString() +  "/" + 
						vertexnr.ToString());
				}
			}

			vertexoffset += vertexcount;
			sw.Flush();
			sw.BaseStream.Seek(0, SeekOrigin.Begin);
			return (MemoryStream)sw.BaseStream;
		}

		/// <summary>
		/// Creates a .obj File for all Models stored in the GMDC
		/// </summary>
		/// <returns>The content of the obj File</returns>
		public MemoryStream GenerateObj()
		{			
			System.Collections.ArrayList list = new System.Collections.ArrayList();
			for (int modelnr = 0; modelnr < this.P3Model.Length; modelnr++)
			{				
				GeometryDataContainerItem3 model = (GeometryDataContainerItem3) this.P3Model[modelnr];
				list.Add(model);
			} //for modelnr
			
			return GenerateObj(list);
		}

		/// <summary>
		/// Creates a .obj File for all Models stored in the GMDC
		/// </summary>
		/// <returns>The content of the obj File</returns>
		public MemoryStream GenerateObj(System.Collections.ArrayList models)
		{			
			System.IO.StreamWriter sw = new StreamWriter(new MemoryStream());
			sw.WriteLine("# File based on the GMDC plugin by Delphy");

			int vertexoffset = 0;
			for (int modelnr = 0; modelnr < models.Count; modelnr++)
			{				
				GeometryDataContainerItem3 model = (GeometryDataContainerItem3) models[modelnr];
				MemoryStream s = this.GenerateModelObj(model, modelnr, ref vertexoffset);
				StreamReader sr = new StreamReader(s);
				sw.WriteLine(sr.ReadToEnd());
			} //for modelnr

			sw.Flush();
			sw.BaseStream.Seek(0, SeekOrigin.Begin);
			return (MemoryStream)sw.BaseStream;
		}
		#endregion

		#region .x-Files
		System.Collections.ArrayList modelnames;
		/// <summary>
		/// Returns a unique Modelname
		/// </summary>
		/// <param name="name">The name of the Model</param>
		/// <returns>the unique Name</returns>
		string GetUniqueModelName(string name) 
		{
			string oname = name;
			int i=0;
			while (modelnames.Contains(name)) 
			{
				name = oname+i.ToString();
				i++;
			}

			modelnames.Add(name);
			return name;
		}

		/// <summary>
		/// Generates a .x File for ONE Model
		/// </summary>
		/// <param name="model">The Model you want to use</param>
		/// <param name="modelnr">Number of that Model in the GMDC (0 based)</param>
		/// <param name="txtrname">name of the texture File</param>
		/// <returns>The content of the x File</returns>
		/// <remarks>Doesn't generate any Header informations</remarks>
		protected MemoryStream GenerateModelX(GeometryDataContainerItem3 model, int modelnr, string txtrname)
		{
			System.IO.StreamWriter sw = new StreamWriter(new MemoryStream());
			modelInfo info1;
				
			GeometryDataContainerItem2 modelitem = (GeometryDataContainerItem2) this.P2VertexLink[model.Alternate];
			info1 = new modelInfo();
			info1.vertexDataList = modelitem.Items1[0];
			info1.normalDataList = modelitem.Items1[1];
			if (modelitem.Items1.Length>2) info1.tuDataList = modelitem.Items1[2];

			string umodelname = GetUniqueModelName(model.ToString());
			sw.WriteLine("Frame " + umodelname + " {");			
			sw.WriteLine("Mesh {");
			sw.WriteLine();

			
				
			
			GeometryDataContainerItem1 item3 = null;

			item3 = (GeometryDataContainerItem1) this.P1Vetices[info1.vertexDataList];			
			int vertexcount = 0;
			//sw.WriteLine((item3.Data.Length/3).ToString() + "; //Vertex Count");
			sw.WriteLine(Math.Min(item3.Data.Length, modelitem.VertexCount).ToString() + "; //Vertex Count");
			for (int i = 0; i < Math.Min(item3.Data.Length, modelitem.VertexCount*3); i+=3)
			{
				vertexcount++;					
				if (i!=0) sw.WriteLine(",");
				sw.Write(
					item3.Data[i].ToString("N9", CultureInfo.InvariantCulture) + ";"+
					item3.Data[i+1].ToString("N9", CultureInfo.InvariantCulture) + ";"+
					item3.Data[i+2].ToString("N9", CultureInfo.InvariantCulture) + "; "
					);
			}
			sw.WriteLine(";");
			

			//now build a Face list
			string faces = (model.Items1.Length / 3).ToString() + "; //Face Count";
			for (int i = 0; i < model.Items1.Length; i++)
			{
				int vertexnr = model.Items1[i];				
				if (i%3 == 0)
				{
					if (i!=0) faces += ", ";
					faces += Helper.lbr + "3;" +
						vertexnr.ToString() + ",";
				} 
				else if (i%3 == 1)
				{
					faces +=  vertexnr.ToString() + ",";
				} 
				else 
				{
					faces +=  vertexnr.ToString() + ";";
				}
			}
			faces += ";";
			sw.WriteLine(faces);
			sw.WriteLine();

			//Add a MeshNormal Section

			sw.WriteLine("MeshNormals {");
				item3 = (GeometryDataContainerItem1) this.P1Vetices[info1.normalDataList];	
				//sw.WriteLine((item3.Data.Length/3).ToString() + "; //Vertext Count");
				sw.WriteLine(Math.Min(item3.Data.Length, modelitem.VertexCount).ToString() + "; //Vertex Count");
				for (int i = 0; i < Math.Min(item3.Data.Length, modelitem.VertexCount)*3; i+=3)
				{
					if (i!=0) sw.WriteLine(",");
					sw.Write(
						item3.Data[i].ToString("N9", CultureInfo.InvariantCulture) + ";"+
						item3.Data[i+1].ToString("N9", CultureInfo.InvariantCulture) + ";"+
						item3.Data[i+2].ToString("N9", CultureInfo.InvariantCulture) + ";"
						);
				}
				sw.WriteLine(";");
				sw.WriteLine(faces);
				sw.WriteLine("}");
				sw.WriteLine();
			

			//now Material Definitions
			sw.WriteLine("MeshMaterialList{");
			sw.WriteLine("1;");
			sw.WriteLine((model.Items1.Length / 3).ToString() + "; //Face Count");
			for (int i = 0; i < model.Items1.Length; i+=3)
			{
				if (i!=0) sw.Write(",");
				if (i%10==0) sw.WriteLine();
				sw.Write("0");
			}
			sw.WriteLine(";;");
			//add a Meterial
			sw.WriteLine("Material {");
			sw.WriteLine("0.300000;0.300000;0.300000;1.000000;;");
			sw.WriteLine("0.300000;");
			sw.WriteLine("1.000000;1.000000;1.000000;;");
			sw.WriteLine("0.100000;0.100000;0.100000;;");
			if (txtrname!=null) 
			{
				///
				///TODO: Remove the following Line if you found a way how to texture two subsets with te same name
				///
				txtrname = umodelname;
				sw.WriteLine("TextureFilename{\""+txtrname.Replace("\\", "\\\\")+"\";}");
			}
			sw.WriteLine("}");
			sw.WriteLine("}");

			if (modelitem.Items1.Length>2) 
			{				
				//now the Texture Cords
				sw.WriteLine("MeshTextureCoords {");
				item3 = (GeometryDataContainerItem1) this.P1Vetices[info1.tuDataList];
				//sw.WriteLine((item3.Data.Length/2).ToString() + "; //Vertex Count");
				sw.WriteLine(Math.Min(item3.Data.Length, modelitem.VertexCount).ToString() + "; //Vertex Count");
				for (int i = 0; i < Math.Min(item3.Data.Length, modelitem.VertexCount)*2; i+=2)
				{
					//if (i!=0) ret += ",";
					sw.WriteLine(
						(item3.Data[i]).ToString("N9", CultureInfo.InvariantCulture) + ";"+
						(item3.Data[i+1]).ToString("N9", CultureInfo.InvariantCulture) + ";"
						);
				}
				sw.WriteLine(";");
				sw.WriteLine("}"); 
			}
			

			//close all opend Containers
			sw.WriteLine("} //Mesh");
			sw.WriteLine("} //Frame");

			sw.Flush();
			sw.BaseStream.Seek(0, SeekOrigin.Begin);
			return (MemoryStream)sw.BaseStream;
		}

		/// <summary>
		/// Creates a .x File for all Models stored in the GMDC
		/// </summary>
		/// <returns>The content of the x File</returns>
		public MemoryStream GenerateX()
		{
			modelnames = new System.Collections.ArrayList();
			System.Collections.ArrayList list = new System.Collections.ArrayList();
			for (int modelnr = 0; modelnr < this.P3Model.Length; modelnr++)
			{				
				GeometryDataContainerItem3 model = (GeometryDataContainerItem3) this.P3Model[modelnr];
				list.Add(model);
			} //for modelnr
			
			return GenerateX(list);
		}

		/// <summary>
		/// Creates a .x File for all Models stored in the GMDC
		/// </summary>
		/// <param name="models">List of all P3Models you want to export</param>
		/// <returns>The content of the x File</returns>
		public MemoryStream GenerateX(System.Collections.ArrayList models)
		{
			modelnames = new System.Collections.ArrayList();
			System.IO.StreamWriter sw = new StreamWriter(new MemoryStream());
			
			sw.WriteLine("xof 0303txt 0032");
			sw.WriteLine();
			sw.WriteLine("# This DirectX File was generated by SimPE");
			sw.WriteLine();
			sw.WriteLine("Frame SCENE_ROOT{");

			for (int modelnr = 0; modelnr < models.Count; modelnr++)
			{				
				GeometryDataContainerItem3 model = (GeometryDataContainerItem3) models[modelnr];
				MemoryStream s = (MemoryStream)this.GenerateModelX(model, modelnr, model.ToString());
				StreamReader sr = new StreamReader(s);
				sw.WriteLine(sr.ReadToEnd());
				//break;
			} //for modelnr
			sw.WriteLine("}");
			
			sw.Flush();
			sw.BaseStream.Seek(0, SeekOrigin.Begin);
			return (MemoryStream)sw.BaseStream;
		}
		#endregion
	}
}
