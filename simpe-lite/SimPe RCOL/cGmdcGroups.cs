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
using System.Collections;

namespace SimPe.Plugin.Gmdc
{
	/// <summary>
	/// Contains the Group Section of a GMDC
	/// </summary>
	public class GmdcGroup  : GmdcLinkBlock
	{
		#region Attributes

		int unknown1;
		public int PrimitiveType 
		{
			get { return unknown1; }
			set { unknown1 = value; }
		}

		int alternate;
		public int LinkIndex 
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
		
		IntArrayList items1;
		public IntArrayList Faces 
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

		IntArrayList items2;
		public IntArrayList Subsets 
		{
			get { return items2; }
			set { items2 = value; }
		}
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public GmdcGroup(GeometryDataContainer parent) : base(parent)
		{
			items1 = new IntArrayList();
			items2 = new IntArrayList();
			name = "";
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

			ReadBlock(reader, items1);

			if (parent.Version!=0x03) opacity = reader.ReadInt32();
			else opacity = 0;

			if (parent.Version!=0x01) ReadBlock(reader, items2); 
			else items2.Clear();
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

			WriteBlock(writer, items1);

			if (parent.Version!=0x03) writer.Write(opacity);

			if (parent.Version!=0x01) WriteBlock(writer, items2); 
		}

		public override string ToString()
		{
			int vertcount = 0;
			if (this.LinkIndex<parent.Links.Count) 
			{
				if (LinkIndex>=0 && LinkIndex<parent.Links.Count) 
				{
					foreach(int i in  parent.Links[LinkIndex].Items1)
					{
						if (i<parent.Elements.Count) 
						{
							GmdcElement e = parent.Elements[i];
							if (e.Identity == ElementIdentity.Vertex) vertcount += e.Values.Length;													
						}
					}
				}
			}
			return name + " (FaceCount="+(this.Faces.Count/3).ToString()+", VertexCount="+vertcount.ToString()+")";
		}
	}

	#region Container
	/// <summary>
	/// Typesave ArrayList for GmdcLink Objects
	/// </summary>
	public class GmdcGroups : ArrayList 
	{
		public new GmdcGroup this[int index]
		{
			get { return ((GmdcGroup)base[index]); }
			set { base[index] = value; }
		}

		public GmdcGroup this[uint index]
		{
			get { return ((GmdcGroup)base[(int)index]); }
			set { base[(int)index] = value; }
		}

		public int Add(GmdcGroup item)
		{
			return base.Add(item);
		}

		public void Insert(int index, GmdcGroup item)
		{
			base.Insert(index, item);
		}

		public void Remove(GmdcGroup item)
		{
			base.Remove(item);
		}

		public bool Contains(GmdcGroup item)
		{
			return base.Contains(item);
		}		

		public int Length 
		{
			get { return this.Count; }
		}

		public override object Clone()
		{
			GmdcGroups list = new GmdcGroups();
			foreach (GmdcGroup item in this) list.Add(item);

			return list;
		}
	}
	#endregion
}
