using System;
using System.Drawing;
using Ambertation.Geometry;

namespace Ambertation;

public static class Helpers
{
	public static double DegToRad(double deg)
	{
		return deg * 3.14159265358979 / 180.0;
	}

	public static string ForceLength(string s, int len, char fill, bool front)
	{
		if (!front)
		{
			while (s.Length < len)
			{
				s += fill;
			}
			if (s.Length > len)
			{
				s = s.Substring(0, len);
			}
		}
		else
		{
			while (s.Length < len)
			{
				s = fill + s;
			}
			if (s.Length > len)
			{
				s = s.Substring(s.Length - len);
			}
		}
		return s;
	}

	public static double RadToDeg(double rad)
	{
		return rad * 180.0 / 3.14159265358979;
	}

	public static string RemoveDoubleSpaces(string s)
	{
		if (s != null)
		{
			while (s.IndexOf("  ") >= 0)
			{
				s = s.Replace("  ", " ");
			}
			return s.Trim();
		}
		return "";
	}

	public static Color ToColor(Vector4 v)
	{
		return Color.FromArgb((int)(v.W * 255.0), (int)(v.X * 255.0), (int)(v.Y * 255.0), (int)(v.Z * 255.0));
	}

	public static Color ToColor(Vector3 v)
	{
		return Color.FromArgb((int)(v.X * 255.0), (int)(v.Y * 255.0), (int)(v.Z * 255.0));
	}

	public static DateTime ToDateTime(string s, DateTime def)
	{
		try
		{
			return Convert.ToDateTime(s);
		}
		catch
		{
			return def;
		}
	}

	public static float ToFloat(string s)
	{
		return ToInt(s, 0);
	}

	public static float ToFloat(string s, float def)
	{
		try
		{
			return Convert.ToSingle(s);
		}
		catch
		{
			return def;
		}
	}

	public static int ToInt(string s)
	{
		return ToInt(s, 0);
	}

	public static int ToInt(string s, int def)
	{
		try
		{
			return Convert.ToInt32(s);
		}
		catch
		{
			return def;
		}
	}

	public static string ToString(byte[] data)
	{
		string text = "";
		foreach (byte b in data)
		{
			text += (char)b;
		}
		return text;
	}

	public static Vector3 ToVector3(Color c)
	{
		return new Vector3((float)(int)c.R / 255f, (float)(int)c.G / 255f, (float)(int)c.B / 255f);
	}

	public static Vector4 ToVector4(Color c)
	{
		return new Vector4((float)(int)c.R / 255f, (float)(int)c.G / 255f, (float)(int)c.B / 255f, (float)(int)c.A / 255f);
	}
}
