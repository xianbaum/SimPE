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
	/// Contains the Subset Section of a GMDC
	/// </summary>
	public class GmdcSubset : GmdcLinkBlock
	{
		#region Attributes		
		int vcount;
		public int VertexCount
		{
			get { return vcount; }
			set { vcount = value; }
		}

		Vectors3f faces;
		public Vectors3f Faces 
		{
			get { return faces; }
			set { faces = value; }
		}

		IntArrayList items;
		public IntArrayList Items 
		{
			get { return items; }
			set { items = value; }
		}
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public GmdcSubset(GeometryDataContainer parent) : base(parent)
		{
			vcount = 0;
			faces = new Vectors3f();
			items = new IntArrayList();
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		public  void Unserialize(System.IO.BinaryReader reader)
		{
			vcount = reader.ReadInt32();

			if (vcount>0) 
			{
				int count = reader.ReadInt32();
				faces.Clear();
				for (int i=0; i<vcount; i++)
				{
					Vector3f f = new Vector3f();
					f.Unserialize(reader);
					faces.Add(f);
				}

				items.Clear();
				for (int i=0; i<count; i++) items.Add(this.ReadValue(reader));
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
			writer.Write(vcount);

			if (vcount>0) 
			{
				int count = Math.Min(faces.Length, items.Length);
				writer.Write((int)count);
				for (int i=0; i<count; i++) faces[i].Serialize(writer);
				for (int i=0; i<count; i++) this.WriteValue(writer, items[i]);
			}	
		}

		public override string ToString()
		{
			return this.Faces.Count.ToString()+", "+this.Items.Count.ToString();
		}

	}
	
	#region Container
	/// <summary>
	/// Typesave ArrayList for GmdcModel Objects
	/// </summary>
	public class GmdcSubsets : ArrayList 
	{
		public new GmdcSubset this[int index]
		{
			get { return ((GmdcSubset)base[index]); }
			set { base[index] = value; }
		}

		public GmdcSubset this[uint index]
		{
			get { return ((GmdcSubset)base[(int)index]); }
			set { base[(int)index] = value; }
		}

		public int Add(GmdcSubset item)
		{
			return base.Add(item);
		}

		public void Insert(int index, GmdcSubset item)
		{
			base.Insert(index, item);
		}

		public void Remove(GmdcSubset item)
		{
			base.Remove(item);
		}

		public bool Contains(GmdcSubset item)
		{
			return base.Contains(item);
		}		

		public int Length 
		{
			get { return this.Count; }
		}

		public override object Clone()
		{
			GmdcSubsets list = new GmdcSubsets();
			foreach (GmdcSubset item in this) list.Add(item);

			return list;
		}
	}
	#endregion
}
