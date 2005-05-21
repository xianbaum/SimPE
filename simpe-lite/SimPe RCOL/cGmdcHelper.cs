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

namespace SimPe.Plugin.Gmdc
{
	public enum BlockFormat : uint
	{
		OneFloat = 0x00,
		TwoFloat = 0x01,
	    ThreeFloat = 0x02,
	    OneDword  = 0x04,		
		Unknown = 0xff
	}

	public enum SetFormat : uint 
	{
		Main = 0x00,
		Normals = 0x01,
		Mapping = 0x02,
		Secondary = 0x03,
		Unknown = 0xff
	}

	public enum ElementIdentity : uint 
	{
		Unknown=0,
		BlendIndex=0x1C4AFC56,
		BlendWeight=0x5C4AFC5C,
		TargetIndex=0x7C4DEE82,
		NormalMorphDelta=0xCB6F3A6A,
		Color=0xCB7206A1,
		ColorDelta=0xEB720693,
		Normal=0x3B83078B,
		Vertex=0x5B830781,
		UVCoordinate=0xBB8307AB,
		UVCoordinateDelta=0xDB830795,
		Binormals=0x9BB38AFB,
		BoneWeights=0x3BD70105,
		BoneAssignment=0xFBD70111,
		BumpMapNormal=0x89D92BA0,
		BumpMapNormalDelta=0x69D92B93,
		MorphVertexDelta=0x5CF2CFE1,
		MorphVertexMap=0xDCF2CFDC
	}

	#region Container
	/// <summary>
	/// Typesave ArrayList for GmdcElementValueBase Objects
	/// </summary>
	public class IntArrayList : ArrayList 
	{
		public new int this[int index]
		{
			get { return ((int)base[index]); }
			set { base[index] = value; }
		}

		public int this[uint index]
		{
			get { return ((int)base[(int)index]); }
			set { base[(int)index] = value; }
		}

		public int Add(int item)
		{
			return base.Add(item);
		}

		public void Insert(int index, int item)
		{
			base.Insert(index, item);
		}

		public void Remove(int item)
		{
			base.Remove(item);
		}

		public bool Contains(int item)
		{
			return base.Contains(item);
		}		

		public int Length 
		{
			get { return this.Count; }
		}

		public override object Clone()
		{
			IntArrayList list = new IntArrayList();
			foreach (int item in this) list.Add(item);

			return list;
		}
	}
	#endregion

	/// <summary>
	/// Contains Methods to process Link Blocks
	/// </summary>
	public class GmdcLinkBlock
	{
		protected GeometryDataContainer parent;	
		/// <summary>
		/// Sets the currently assigned Parent
		/// </summary>
		internal GeometryDataContainer Parent 
		{
			set { parent = value; }
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public GmdcLinkBlock(GeometryDataContainer parent)
		{
			this.parent = parent;
		}

		/// <summary>
		/// Read the value from the Stream
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		protected int ReadValue(System.IO.BinaryReader reader)
		{
			if (parent.Version==0x04) return reader.ReadInt16();
			else return reader.ReadInt32();
		}

		/// <summary>
		/// Write the value to the Stream
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="val"></param>
		protected void WriteValue(System.IO.BinaryWriter writer, int val)
		{
			if (parent.Version==0x04) writer.Write((short)val);
			else writer.Write((int)val);
		}

		/// <summary>
		/// Read a Link Block
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="items"></param>
		protected void ReadBlock(System.IO.BinaryReader reader, IntArrayList items)
		{
			int count = reader.ReadInt32();
			items.Clear();
			for (int i=0; i<count; i++) items.Add(ReadValue(reader));;			
		}

		/// <summary>
		/// Write a Link Block
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="items"></param>
		protected void WriteBlock(System.IO.BinaryWriter writer, IntArrayList items)
		{
			writer.Write((int)items.Length);			
			for (int i=0; i<items.Length; i++) WriteValue(writer, items[i]);
		}
	}

	
}
