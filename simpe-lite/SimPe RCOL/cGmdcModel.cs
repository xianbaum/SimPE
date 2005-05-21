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
	#region Vectors
	/// <summary>
	/// Contains the a 3D Vector
	/// </summary>
	public class Vector3f 
	{
		protected float x, y, z;
		public float X 
		{
			get { return x; }
			set { x = value; }
		}
		public float Y 
		{
			get { return y; }
			set { y = value; }
		}
		public float Z 
		{
			get { return z; }
			set { z = value; }
		}

		internal Vector3f ()
		{
			x = 0; y = 0; z = 0;
		}

		public Vector3f (float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		internal virtual void Unserialize(System.IO.BinaryReader reader)
		{
			x = reader.ReadSingle();			
			y = reader.ReadSingle();
			z = reader.ReadSingle();
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
			writer.Write(x);
			writer.Write(y);
			writer.Write(z);
		}

		public override string ToString()
		{
			return x.ToString("N6")+", "+y.ToString("N6")+", "+z.ToString("N6");
		}

	}

	/// <summary>
	/// Contains the a 4D Vector
	/// </summary>
	public class Vector4f : Vector3f
	{
		protected float w;
		public float W
		{
			get { return w; }
			set { w = value; }
		}

		internal Vector4f () : base()
		{
			w = 0;
		}

		public Vector4f (float x, float y, float z, float w) : base(x, y, z)
		{
			this.w = w;
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		internal override void Unserialize(System.IO.BinaryReader reader)
		{
			base.Unserialize(reader);
			w = reader.ReadSingle();				
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		internal override void Serialize(System.IO.BinaryWriter writer)
		{
			base.Serialize(writer);
			writer.Write(w);
		}

		public override string ToString()
		{
			return base.ToString()+", "+w.ToString("N6");
		}
	}
	#endregion

	public class GmdcNamePair 
	{
		string blendname;
		public string BlendGroupName 
		{
			get { return blendname; }
			set { blendname = value; }
		}

		string elementname;
		public string AssignedElementName
		{
			get { return elementname; }
			set { elementname = value; }
		}

		internal GmdcNamePair()
		{
			blendname = "";
			elementname = "";
		}

		public GmdcNamePair(string blend, string element)
		{
			blendname = blend;
			elementname = element;
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		internal virtual void Unserialize(System.IO.BinaryReader reader)
		{
			blendname = reader.ReadString();
			elementname = reader.ReadString();
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
			writer.Write(blendname);
			writer.Write(elementname);
		}

		public override string ToString()
		{
			return blendname+", "+elementname;
		}

	}

	/// <summary>
	/// Contains the Model Section of a GMDC
	/// </summary>
	public class GmdcModel : GmdcLinkBlock
	{
		#region Attributes
		Vectors3f transforms;
		public Vectors3f Transformations 
		{
			get { return transforms; }
			set {transforms = value; }
		}

		Vectors4f rotations;
		public Vectors4f Rotations 
		{
			get { return rotations; }
			set {rotations = value; }
		}

		GmdcNamePairs names;
		public GmdcNamePairs Names 
		{
			get { return names; }
			set {names = value; }
		}		

		GmdcSubset subset;
		public GmdcSubset Subset
		{
			get { return subset; }
			set { subset = value; }
		}
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public GmdcModel(GeometryDataContainer parent) : base(parent)
		{
			transforms = new Vectors3f();
			rotations = new Vectors4f();

			names = new GmdcNamePairs();

			subset = new GmdcSubset(parent);
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		public  void Unserialize(System.IO.BinaryReader reader)
		{
			int count = reader.ReadInt32();
			transforms.Clear();
			rotations.Clear();
			for (int i=0; i<count; i++)
			{
				Vector4f r = new Vector4f();
				r.Unserialize(reader);
				rotations.Add(r);

				Vector3f t = new Vector3f();
				t.Unserialize(reader);
				transforms.Add(t);
			}

			count = reader.ReadInt32();
			names.Clear();
			for (int i=0; i<count; i++)
			{
				GmdcNamePair p = new GmdcNamePair();
				p.Unserialize(reader);
				names.Add(p);
			}

			subset.Unserialize(reader);
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
			int count = Math.Min(rotations.Length, transforms.Length);
			writer.Write((int)count);
			for (int i=0; i<count; i++)
			{
				rotations[i].Serialize(writer);
				transforms[i].Serialize(writer);
			}

			writer.Write((int)names.Length);
			for (int i=0; i<names.Length; i++) names[i].Serialize(writer);

			subset.Serialize(writer);
		}
	}
	
	#region Container
	/// <summary>
	/// Typesave ArrayList for GmdcModel Objects
	/// </summary>
	public class GmdcModels : ArrayList 
	{
		public new GmdcModel this[int index]
		{
			get { return ((GmdcModel)base[index]); }
			set { base[index] = value; }
		}

		public GmdcModel this[uint index]
		{
			get { return ((GmdcModel)base[(int)index]); }
			set { base[(int)index] = value; }
		}

		public int Add(GmdcModel item)
		{
			return base.Add(item);
		}

		public void Insert(int index, GmdcModel item)
		{
			base.Insert(index, item);
		}

		public void Remove(GmdcModel item)
		{
			base.Remove(item);
		}

		public bool Contains(GmdcModel item)
		{
			return base.Contains(item);
		}		

		public int Length 
		{
			get { return this.Count; }
		}

		public override object Clone()
		{
			GmdcModels list = new GmdcModels();
			foreach (GmdcModel item in this) list.Add(item);

			return list;
		}
	}

	/// <summary>
	/// Typesave ArrayList for Vector3f Objects
	/// </summary>
	public class Vectors3f : ArrayList 
	{
		public new Vector3f this[int index]
		{
			get { return ((Vector3f)base[index]); }
			set { base[index] = value; }
		}

		public Vector3f this[uint index]
		{
			get { return ((Vector3f)base[(int)index]); }
			set { base[(int)index] = value; }
		}

		public int Add(Vector3f item)
		{
			return base.Add(item);
		}

		public void Insert(int index, Vector3f item)
		{
			base.Insert(index, item);
		}

		public void Remove(Vector3f item)
		{
			base.Remove(item);
		}

		public bool Contains(Vector3f item)
		{
			return base.Contains(item);
		}		

		public int Length 
		{
			get { return this.Count; }
		}

		public override object Clone()
		{
			Vectors3f list = new Vectors3f();
			foreach (Vector3f item in this) list.Add(item);

			return list;
		}
	}

	/// <summary>
	/// Typesave ArrayList for Vector4f Objects
	/// </summary>
	public class Vectors4f : ArrayList 
	{
		public new Vector4f this[int index]
		{
			get { return ((Vector4f)base[index]); }
			set { base[index] = value; }
		}

		public Vector4f this[uint index]
		{
			get { return ((Vector4f)base[(int)index]); }
			set { base[(int)index] = value; }
		}

		public int Add(Vector4f item)
		{
			return base.Add(item);
		}

		public void Insert(int index, Vector4f item)
		{
			base.Insert(index, item);
		}

		public void Remove(Vector4f item)
		{
			base.Remove(item);
		}

		public bool Contains(Vector4f item)
		{
			return base.Contains(item);
		}		

		public int Length 
		{
			get { return this.Count; }
		}

		public override object Clone()
		{
			Vectors4f list = new Vectors4f();
			foreach (Vector4f item in this) list.Add(item);

			return list;
		}
	}

	/// <summary>
	/// Typesave ArrayList for GmdcNamePair Objects
	/// </summary>
	public class GmdcNamePairs : ArrayList 
	{
		public new GmdcNamePair this[int index]
		{
			get { return ((GmdcNamePair)base[index]); }
			set { base[index] = value; }
		}

		public GmdcNamePair this[uint index]
		{
			get { return ((GmdcNamePair)base[(int)index]); }
			set { base[(int)index] = value; }
		}

		public int Add(GmdcNamePair item)
		{
			return base.Add(item);
		}

		public void Insert(int index, GmdcNamePair item)
		{
			base.Insert(index, item);
		}

		public void Remove(GmdcNamePair item)
		{
			base.Remove(item);
		}

		public bool Contains(GmdcNamePair item)
		{
			return base.Contains(item);
		}		

		public int Length 
		{
			get { return this.Count; }
		}

		public override object Clone()
		{
			GmdcNamePairs list = new GmdcNamePairs();
			foreach (GmdcNamePairs item in this) list.Add(item);

			return list;
		}
	}
	#endregion
}
