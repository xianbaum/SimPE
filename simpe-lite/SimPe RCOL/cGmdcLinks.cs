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
	
	public class GmdcLink : GmdcLinkBlock
	{
		#region Attributes

		IntArrayList items1;		
		public IntArrayList Items1
		{
			get { return items1; }
			set { items1 = value; }
		}

		int unknown1;
		public int ReferencedSize 
		{
			get { return unknown1; }
			set { unknown1 = value; }
		}

		int unknown2;
		public int ActiveElements 
		{
			get { return unknown2; }
			set { unknown2 = value; }
		}

		IntArrayList items2;		
		public IntArrayList Items2
		{
			get { return items2; }
			set { items2 = value; }
		}

		IntArrayList items3;		
		public IntArrayList Items3
		{
			get { return items3; }
			set { items3 = value; }
		}

		IntArrayList items4;		
		public IntArrayList Items4
		{
			get { return items4; }
			set { items4 = value; }
		}
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public GmdcLink(GeometryDataContainer parent) : base(parent)
		{
			items1 = new IntArrayList();
			items2 = new IntArrayList();
			items3 = new IntArrayList();
			items4 = new IntArrayList();
		}

		
			
		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		public  void Unserialize(System.IO.BinaryReader reader)
		{
			ReadBlock(reader, items1);

			unknown1 = reader.ReadInt32();
			unknown2 = reader.ReadInt32();
			
			ReadBlock(reader, items2);
			ReadBlock(reader, items3);
			ReadBlock(reader, items4);
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
			WriteBlock(writer, items1);

			writer.Write(unknown1);
			writer.Write(unknown2);

			WriteBlock(writer, items2);
			WriteBlock(writer, items3);
			WriteBlock(writer, items4);
		}

		public override string ToString()
		{
			return items1.Length.ToString()+", "+items2.Length.ToString()+", "+items3.Length.ToString()+", "+items4.Length.ToString();
		}
	}

	#region Container
	/// <summary>
	/// Typesave ArrayList for GmdcLink Objects
	/// </summary>
	public class GmdcLinks : ArrayList 
	{
		public new GmdcLink this[int index]
		{
			get { return ((GmdcLink)base[index]); }
			set { base[index] = value; }
		}

		public GmdcLink this[uint index]
		{
			get { return ((GmdcLink)base[(int)index]); }
			set { base[(int)index] = value; }
		}

		public int Add(GmdcLink item)
		{
			return base.Add(item);
		}

		public void Insert(int index, GmdcLink item)
		{
			base.Insert(index, item);
		}

		public void Remove(GmdcLink item)
		{
			base.Remove(item);
		}

		public bool Contains(GmdcLink item)
		{
			return base.Contains(item);
		}		

		public int Length 
		{
			get { return this.Count; }
		}

		public override object Clone()
		{
			GmdcLinks list = new GmdcLinks();
			foreach (GmdcLink item in this) list.Add(item);

			return list;
		}
	}
	#endregion
}
