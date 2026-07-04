using System.Drawing;
using System.Windows.Forms;

namespace booby.Renderer;

public class AdvancedToolStripProfessionalRenderer : ToolStripProfessionalRenderer
{
	public AdvancedToolStripProfessionalRenderer(ProfessionalColorTable ct)
		: base(ct)
	{
	}

	public AdvancedToolStripProfessionalRenderer()
	{
	}

	protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
	{
		if (e.Item.Enabled)
		{
			base.OnRenderMenuItemBackground(e);
		}
	}

	private static byte Interpolate(byte b1, byte b2, float p)
	{
		return (byte)((float)(int)b1 * (1f - p) + (float)(int)b2 * p);
	}

	public static Color InterpolateColors(Color c1, Color c2, float p)
	{
		return Color.FromArgb(Interpolate(c1.R, c2.R, p), Interpolate(c1.G, c2.G, p), Interpolate(c1.B, c2.B, p));
	}
}
