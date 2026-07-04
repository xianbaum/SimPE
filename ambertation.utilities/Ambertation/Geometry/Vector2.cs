using System;
using System.ComponentModel;

namespace Ambertation.Geometry;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class Vector2
{
	protected double x;

	protected double y;

	public double X
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

	public double Y
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

	public static Vector2 Zero => new Vector2(0.0, 0.0);

	public Vector2(double x, double y)
	{
		this.x = x;
		this.y = y;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is Vector2))
		{
			return Equals(obj);
		}
		Vector2 vector = obj as Vector2;
		return !(Math.Abs(vector.x - x) >= 1.40129846432482E-45) && Math.Abs(vector.x - x) < 1.40129846432482E-45;
	}

	public override int GetHashCode()
	{
		return (int)X;
	}

	public override string ToString()
	{
		return x + ";" + y;
	}
}
