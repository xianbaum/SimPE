using System;
using System.ComponentModel;

namespace Ambertation.Geometry;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class Vector4 : Vector3, ICloneable
{
	public Vector4 DeHomogenize
	{
		get
		{
			if (W != 0.0)
			{
				return this / W;
			}
			return new Vector4();
		}
	}

	public override double Norm => Math.Pow(base.X, 2.0) + Math.Pow(base.Y, 2.0) + Math.Pow(base.Z, 2.0) + Math.Pow(W, 2.0);

	public new Vector4 UnitVector
	{
		get
		{
			double length = base.Length;
			if (length != 0.0)
			{
				return this / length;
			}
			return new Vector4();
		}
	}

	public virtual double W
	{
		get
		{
			return w;
		}
		set
		{
			w = value;
		}
	}

	public Vector4(Matrix m)
	{
		if (m.Rows < 3 || m.Rows > 4 || m.Columns != 1)
		{
			throw new GeometryException("Matrix cannot be converted to a Vector4!");
		}
		base.X = m[0, 0];
		base.Y = m[1, 0];
		base.Z = m[2, 0];
		if (m.Rows != 4)
		{
			w = 1.0;
		}
		else
		{
			w = m[3, 0];
		}
	}

	public Vector4(Vector3 v)
		: this(v.X, v.Y, v.Z, 1.0)
	{
	}

	public Vector4(Vector3 v, double w)
		: this(v.X, v.Y, v.Z, w)
	{
	}

	public Vector4()
	{
		W = 1.0;
	}

	public Vector4(double x, double y, double z)
		: base(x, y, z)
	{
	}

	public Vector4(double x, double y, double z, double w)
		: base(x, y, z)
	{
		W = w;
	}

	public override object Clone()
	{
		return new Vector4(base.X, base.Y, base.Z, W);
	}

	public static double Dot(Vector4 a, Vector4 b)
	{
		return a * b;
	}

	public override bool Equals(object obj)
	{
		return base.Equals(obj);
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public static Vector4 operator +(Vector4 v1, Vector4 v2)
	{
		return new Vector4((Matrix)v1 + (Matrix)v2);
	}

	public static Vector4 operator +(Vector3 v1, Vector4 v2)
	{
		return new Vector4((Matrix)v1 + (Matrix)v2);
	}

	public static Vector4 operator +(Vector4 v1, Vector3 v2)
	{
		return new Vector4((Matrix)v1 + (Matrix)v2);
	}

	public static double operator &(Vector4 v1, Vector4 v2)
	{
		return v1 * v2;
	}

	public static Vector4 operator /(Vector4 v1, double d)
	{
		return new Vector4((Matrix)v1 / d);
	}

	public static bool operator ==(Vector4 v1, Vector4 v2)
	{
		if (!(v1 == null) && v2 != null)
		{
			return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z && v1.W == v2.W;
		}
		return !(v1 != null) && v2 == null;
	}

	public static bool operator ==(Vector4 v1, Vector3 v2)
	{
		if (!(v1 == null) && v2 != null)
		{
			v1 = v1.DeHomogenize;
			return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
		}
		return !(v1 != null) && v2 == null;
	}

	public static explicit operator Matrix(Vector4 v)
	{
		return new Matrix(4, 1)
		{
			[0, 0] = v.X,
			[1, 0] = v.Y,
			[2, 0] = v.Z,
			[3, 0] = v.w
		};
	}

	public static bool operator !=(Vector4 v1, Vector4 v2)
	{
		if (!(v1 == null) && v2 != null)
		{
			return v1.X != v2.X || v1.Y != v2.Y || v1.Z != v2.Z || v1.W != v2.W;
		}
		return v1 != null || v2 != null;
	}

	public static bool operator !=(Vector4 v1, Vector3 v2)
	{
		if (!(v1 == null) && v2 != null)
		{
			v1 = v1.DeHomogenize;
			return v1.X != v2.X || v1.Y != v2.Y || v1.Z != v2.Z;
		}
		return v1 != null || v2 != null;
	}

	public static Vector4 operator !(Vector4 v)
	{
		Vector4 vector = v * -1.0;
		vector.W = v.W;
		return vector;
	}

	public static Vector4 operator *(Vector4 v1, double d)
	{
		return new Vector4((Matrix)v1 * d);
	}

	public static Vector4 operator *(double d, Vector4 v1)
	{
		return new Vector4((Matrix)v1 * d);
	}

	public static double operator *(Vector4 v1, Vector4 v2)
	{
		return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z + v1.W * v2.W;
	}

	public static Vector4 operator *(Matrix m, Vector4 v)
	{
		m *= (Matrix)v;
		return new Vector4(m);
	}

	public static double operator *(Vector3 v1, Vector4 v2)
	{
		return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z + v2.W;
	}

	public static double operator *(Vector4 v1, Vector3 v2)
	{
		return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z + v1.W;
	}

	public static Vector4 operator -(Vector4 v1, Vector4 v2)
	{
		return new Vector4((Matrix)v1 - (Matrix)v2);
	}

	public static Vector4 operator -(Vector3 v1, Vector4 v2)
	{
		return new Vector4((Matrix)v1 - (Matrix)v2);
	}

	public static Vector4 operator -(Vector4 v1, Vector3 v2)
	{
		return new Vector4((Matrix)v1 - (Matrix)v2);
	}

	public override string ToString()
	{
		return "(" + base.X.ToString("N2") + ", " + base.Y.ToString("N2") + ", " + base.Z.ToString("N2") + ", " + W.ToString("N2") + ")";
	}

	public new Vector4 Transform(Matrix m)
	{
		return m * this;
	}

	public static Vector4 Transform(Matrix m, Vector4 v)
	{
		return m * v;
	}
}
