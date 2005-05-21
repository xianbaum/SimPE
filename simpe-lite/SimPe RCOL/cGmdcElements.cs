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
	#region GMDCElementValue
	/// <summary>
	/// Contains a SingleFloat Value
	/// </summary>
	public abstract class GmdcElementValueBase
	{
		protected float[] data;		
		/// <summary>
		/// The plain stored Data
		/// </summary>
		public float[] Data
		{
			get { return data; }
			set { data = value; }
		}	
	
		/// <summary>
		/// Returns the number of Float Elements stored here
		/// </summary>
		internal virtual byte Size 
		{
			get {return 0;}
		}

		internal GmdcElementValueBase()
		{
			data = new float[Size];
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		internal virtual void Unserialize(System.IO.BinaryReader reader)
		{
			for (int i=0; i<data.Length; i++) data[i] = reader.ReadSingle();			
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		internal virtual void Serialize(System.IO.BinaryWriter writer)
		{
			for (int i=0; i<data.Length; i++) writer.Write(data[i]);
		}

		public override string ToString()
		{
			string s = "";
			for (int i=0; i<data.Length; i++) 
			{
				if (i!=0) s+=", ";
				s += data[i].ToString("N6");
			}
			return s;
		}

	}

	/// <summary>
	/// Contains a single Float Value
	/// </summary>
	public class GmdcElementValueOneFloat : GmdcElementValueBase
	{
		internal override byte Size
		{
			get {return 1;}
		}

		internal GmdcElementValueOneFloat() : base() {}
		public GmdcElementValueOneFloat(float f1)
		{
			data = new float[Size];
			data[0] = f1;
		}
	}	

	/// <summary>
	/// Contains a two gloat Value
	/// </summary>
	public class GmdcElementValueTwoFloat : GmdcElementValueBase
	{
		internal override byte Size
		{
			get {return 2;}
		}

		internal GmdcElementValueTwoFloat() : base() {}
		public GmdcElementValueTwoFloat(float f1, float f2)
		{
			data = new float[Size];
			data[0] = f1;
			data[1] = f2;
		}
	}

	/// <summary>
	/// Contains a three gloat Value
	/// </summary>
	public class GmdcElementValueThreeFloat : GmdcElementValueBase
	{
		internal  override byte Size
		{
			get {return 3;}
		}

		internal GmdcElementValueThreeFloat() : base() {}
		public GmdcElementValueThreeFloat(float f1, float f2, float f3)
		{
			data = new float[Size];
			data[0] = f1;
			data[1] = f2;
			data[2] = f3;
		}
	}

	/// <summary>
	/// Contains a two gloat Value
	/// </summary>
	public class GmdcElementValueOneInt : GmdcElementValueBase
	{
		internal  override byte Size
		{
			get {return 1;}
		}

		internal GmdcElementValueOneInt() : base() {}
		public GmdcElementValueOneInt(int i1)
		{
			data = new float[Size];
			Value = i1;
		}

		/// <summary>
		/// Returns /Sets the stored Value
		/// </summary>
		public int Value 
		{
			get {return (int)data[0];}
			set {data[0] = (float)value;}
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		internal override void Serialize(BinaryWriter writer)
		{
			writer.Write((int)data[0]);
		}

		internal override void Unserialize(BinaryReader reader)
		{
			data[0] = (float)reader.ReadInt32();
		}


	}
	#endregion

	/// <summary>
	/// This class contains the Elements Section of a Geometric Data Container
	/// </summary>
	public class GmdcElement : GmdcLinkBlock
	{
		#region Attributes

		int number;
		public int Number 
		{
			get { return number; }
			set { number = value; }
		}

		ElementIdentity identity;
		public ElementIdentity Identity 
		{
			get { return identity; }
			set { identity = value; }
		}

		int repeat;
		public int GroupId 
		{
			get { return repeat; }
			set { repeat = value; }
		}

		BlockFormat blockformat;
		public BlockFormat BlockFormat 
		{
			get { return blockformat; }
			set { blockformat = value; }
		}

		SetFormat setformat;
		public SetFormat SetFormat 
		{
			get { return setformat; }
			set { setformat = value; }
		}

		GmdcElementValues data;		
		public GmdcElementValues Values
		{
			get { return data; }
			set { data = value; }
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
		public GmdcElement(GeometryDataContainer parent) : base (parent)
		{
			data = new GmdcElementValues();
			items = new IntArrayList();			
		}

		/// <summary>
		/// Returns an instance of GmdcElementValueBase class in the apropriate Format
		/// </summary>
		/// <returns>A class Instance</returns>
		/// <remarks>The Type of the instance is determined using the SubType</remarks>
		public GmdcElementValueBase GetValueInstance()
		{
			switch (blockformat) 
			{
				case BlockFormat.OneDword :
					return new Gmdc.GmdcElementValueOneInt();
				case BlockFormat.OneFloat:
					return new Gmdc.GmdcElementValueOneFloat();
				case BlockFormat.TwoFloat:
					return new Gmdc.GmdcElementValueTwoFloat();
				case BlockFormat.ThreeFloat:
					return new Gmdc.GmdcElementValueThreeFloat();
			}

			return new Gmdc.GmdcElementValueOneFloat();
		}
			
		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		public  void Unserialize(System.IO.BinaryReader reader)
		{
			number = reader.ReadInt32();
			identity = (ElementIdentity)reader.ReadInt32();
			repeat = reader.ReadInt32();
			blockformat = (SimPe.Plugin.Gmdc.BlockFormat)reader.ReadInt32();
			setformat = (SimPe.Plugin.Gmdc.SetFormat)reader.ReadInt32();
			
			GmdcElementValueBase dummy = GetValueInstance();
			int len = reader.ReadInt32() / (4 * dummy.Size);
			data.Clear();
			for (int i=0; i<len; i++) 
			{
				dummy = GetValueInstance();
				dummy.Unserialize(reader);
				data.Add(dummy);
			}

			this.ReadBlock(reader, items);
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
			writer.Write(number);
			writer.Write((int)identity);
			writer.Write(repeat);
			writer.Write((uint)blockformat);
			writer.Write((uint)setformat);

			int size = 1;
			if (data.Length>0) size = data[0].Size;

			writer.Write((int)(data.Length * 4 * size));
			for (int i=0; i<data.Length; i++) 
			{
				data[i].Serialize(writer);
			}
			
			this.WriteBlock(writer, items);
		}

		public override string ToString()
		{
			return this.Identity.ToString()+", "+this.SetFormat.ToString()+", "+this.BlockFormat.ToString()+" ("+this.Values.Count.ToString()+")";
		}

	}

	#region Container
	/// <summary>
	/// Typesave ArrayList for GmdcElementValueBase Objects
	/// </summary>
	public class GmdcElementValues : ArrayList 
	{
		public new GmdcElementValueBase this[int index]
		{
			get { return ((GmdcElementValueBase)base[index]); }
			set { base[index] = value; }
		}

		public GmdcElementValueBase this[uint index]
		{
			get { return ((GmdcElementValueBase)base[(int)index]); }
			set { base[(int)index] = value; }
		}

		public int Add(GmdcElementValueBase item)
		{
			return base.Add(item);
		}

		public void Insert(int index, GmdcElementValueBase item)
		{
			base.Insert(index, item);
		}

		public void Remove(GmdcElementValueBase item)
		{
			base.Remove(item);
		}

		public bool Contains(GmdcElementValueBase item)
		{
			return base.Contains(item);
		}		

		public int Length 
		{
			get { return this.Count; }
		}

		public override object Clone()
		{
			GmdcElementValues list = new GmdcElementValues();
			foreach (GmdcElementValueBase item in this) list.Add(item);

			return list;
		}
	}

	/// <summary>
	/// Typesave ArrayList for GmdcElement Objects
	/// </summary>
	public class GmdcElements : ArrayList 
	{
		public new GmdcElement this[int index]
		{
			get { return ((GmdcElement)base[index]); }
			set { base[index] = value; }
		}

		public GmdcElement this[uint index]
		{
			get { return ((GmdcElement)base[(int)index]); }
			set { base[(int)index] = value; }
		}

		public int Add(GmdcElement item)
		{
			return base.Add(item);
		}

		public void Insert(int index, GmdcElement item)
		{
			base.Insert(index, item);
		}

		public void Remove(GmdcElement item)
		{
			base.Remove(item);
		}

		public bool Contains(GmdcElement item)
		{
			return base.Contains(item);
		}		

		public int Length 
		{
			get { return this.Count; }
		}

		public override object Clone()
		{
			GmdcElements list = new GmdcElements();
			foreach (GmdcElement item in this) list.Add(item);

			return list;
		}
	}
	#endregion
}
