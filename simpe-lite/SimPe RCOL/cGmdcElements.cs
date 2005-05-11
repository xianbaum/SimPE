using System;

namespace SimPe.Plugin.Gmdc
{
	public enum BlockFormat : uint
	{
		OneFloat = 0x00,
		TwoFloat = 0x01,
	    ThreeFloat = 0x02,
	    OneDword  = 0x03
	}

	public enum SetFormat : uint 
	{
		Main = 0x00,
		Normals = 0x01,
		Mapping = 0x02,
		Secondary = 0x03
	}


	/// <summary>
	/// Zusammenfassung für GmdcElements.
	/// </summary>
	public class GeometryDataContainerItem1
	{
		#region Attributes

		int number;
		public int Number 
		{
			get { return number; }
			set { number = value; }
		}

		int identity;
		public int Identity 
		{
			get { return identity; }
			set { identity = value; }
		}

		int repeat;
		public int Repeat 
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

		float[] data;		
		public float[] Data
		{
			get { return data; }
			set { data = value; }
		}

		int[] items;		
		public int[] Items
		{
			get { return items; }
			set { items = value; }
		}
		

		GeometryDataContainer parent;
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public GeometryDataContainerItem1(GeometryDataContainer parent)
		{
			data = new float[0];
			items = new int[0];

			this.parent = parent;
		}

			
		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		public  void Unserialize(System.IO.BinaryReader reader)
		{
			number = reader.ReadInt32();
			identity = reader.ReadInt32();
			repeat = reader.ReadInt32();
			blockformat = (SimPe.Plugin.Gmdc.BlockFormat)reader.ReadInt32();
			setformat = (SimPe.Plugin.Gmdc.SetFormat)reader.ReadInt32();
			
			int len = reader.ReadInt32() / 4;
			data = new float[len];
			for (int i=0; i<len; i++) 
			{
				data[i] = reader.ReadSingle();
			}

			items = new int[reader.ReadInt32()];

			for (int i=0; i<items.Length; i++)
			{
				if (parent.Version==0x04) items[i] = reader.ReadInt16();
				else items[i] = reader.ReadInt32();
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
			writer.Write(number);
			writer.Write(identity);
			writer.Write(repeat);
			writer.Write((uint)blockformat);
			writer.Write((uint)setformat);

			writer.Write((int)(data.Length * 4));
			for (int i=0; i<data.Length; i++) 
			{
				//if (parent.Version==0x04) writer.Write((short)data[i]);
				//else 
					writer.Write(data[i]);
			}
			
			writer.Write((int)items.Length);
			for (int i=0; i<items.Length; i++)
			{
				if (parent.Version==0x04) writer.Write((short)items[i]);
				else writer.Write((int)items[i]);
			}
		}

		public override string ToString()
		{
			return "#"+number.ToString()+": 0x"+Helper.HexString((uint)this.identity) + " 0x"+Helper.HexString((uint)this.blockformat) + " 0x"+Helper.HexString((uint)this.setformat);
		}

	}
}
