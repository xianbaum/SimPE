using System.ComponentModel;

namespace Ambertation.Geometry;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class Vector3i
{
	private int x;

	private int y;

	private int z;

	public int this[int index]
	{
		get
		{
			if (index != 0)
			{
				return (index != 1) ? Z : Y;
			}
			return X;
		}
		set
		{
			if (index == 0)
			{
				X = value;
			}
			if (index == 1)
			{
				Y = value;
			}
			Z = value;
		}
	}

	public int X
	{
		get
		{
			return x;
		}
		set
		{
			x = value;
		}
	}

	public int Y
	{
		get
		{
			return y;
		}
		set
		{
			y = value;
		}
	}

	public int Z
	{
		get
		{
			return z;
		}
		set
		{
			z = value;
		}
	}

	public static Vector3i Zero => new Vector3i(0, 0, 0);

	public Vector3i(int x, int y, int z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is Vector3i))
		{
			return Equals(obj);
		}
		Vector3i vector3i = obj as Vector3i;
		return vector3i.X == X && vector3i.Y == Y && vector3i.Z == Z;
	}

	public override int GetHashCode()
	{
		return GetHashCode();
	}

	public override string ToString()
	{
		return x + ";" + y + ";" + z;
	}
}
