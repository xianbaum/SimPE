using System.Drawing;
using Ambertation.Geometry;

namespace Ambertation.XSI.Template;

public class ColorContainer : ExtendedContainer
{
	public ColorContainer(Container parent, string args)
		: base(parent, args)
	{
	}

	protected Color ReadColor(ref int startline, bool inclalpha)
	{
		Vector3 vector = ((!inclalpha) ? ReadVector3(ref startline) : ReadVector4(ref startline));
		int red = (int)(vector.X * 255.0);
		int green = (int)(vector.Y * 255.0);
		int blue = (int)(vector.Z * 255.0);
		int alpha = 255;
		if (inclalpha)
		{
			alpha = (int)(((Vector4)vector).W * 255.0);
		}
		return Color.FromArgb(alpha, red, green, blue);
	}

	protected void WriteColor(bool inclalpha, Color cl)
	{
		AddLiteral((float)(int)cl.R / 255f);
		AddLiteral((float)(int)cl.G / 255f);
		AddLiteral((float)(int)cl.B / 255f);
		if (inclalpha)
		{
			AddLiteral((float)(int)cl.A / 255f);
		}
	}
}
