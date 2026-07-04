using System.ComponentModel;

namespace Ambertation.Geometry;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class PointUV
{
	private int u;

	private int v;

	public int U
	{
		get
		{
			return u;
		}
		set
		{
			u = value;
		}
	}

	public int V
	{
		get
		{
			return v;
		}
		set
		{
			v = value;
		}
	}

	public PointUV(double u, double v)
		: this((int)u, (int)v)
	{
	}

	public PointUV(int u, int v)
	{
		this.u = u;
		this.v = v;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is PointUV))
		{
			return Equals(obj);
		}
		PointUV pointUV = obj as PointUV;
		return pointUV.U == U && pointUV.V == V;
	}

	public override int GetHashCode()
	{
		return GetHashCode();
	}

	public static implicit operator PointUV(Vector2 v)
	{
		return new PointUV((int)v.X, (int)v.Y);
	}

	public override string ToString()
	{
		return u + ";" + v;
	}
}
