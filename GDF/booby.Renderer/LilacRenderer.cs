using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace booby.Renderer;

public class LilacRenderer : ToolStripProfessionalRenderer
{
	private static Image menupattern;

	protected static Image MenuPattern => menupattern;

	protected LilacColorTable Colors => ((ToolStripProfessionalRenderer)this).ColorTable as LilacColorTable;

	public bool RenderRoundedEdges
	{
		get
		{
			return ((ToolStripProfessionalRenderer)this).RoundedEdges;
		}
		set
		{
			((ToolStripProfessionalRenderer)this).RoundedEdges = value;
		}
	}

	public LilacRenderer()
		: base((ProfessionalColorTable)(object)LilacColorTable.Global)
	{
		((ToolStripProfessionalRenderer)this).RoundedEdges = false;
		if (menupattern == null)
		{
			menupattern = Image.FromStream(((object)this).GetType().Assembly.GetManifestResourceStream("booby.pattern.gif"));
		}
	}

	protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
	{
		OnDrawToolStripDropDownMenu(e, overlay: true);
	}

	protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		if (e.ToolStrip is ToolStripDropDownMenu)
		{
			OnDrawToolStripDropDownMenu(e, overlay: false);
			return;
		}
		LinearGradientMode val = (LinearGradientMode)1;
		LinearGradientBrush val2 = new LinearGradientBrush(e.AffectedBounds, ((ProfessionalColorTable)Colors).ToolStripGradientBegin, ((ProfessionalColorTable)Colors).ToolStripGradientMiddle, val);
		try
		{
			ColorBlend val3 = new ColorBlend(4);
			val3.Colors = new Color[4]
			{
				((ProfessionalColorTable)Colors).ToolStripGradientBegin,
				((ProfessionalColorTable)Colors).ToolStripGradientMiddle,
				Colors.ToolStripGradientMiddleEnd,
				((ProfessionalColorTable)Colors).ToolStripGradientEnd
			};
			val3.Positions = new float[4] { 0f, 0.335f, 0.605f, 1f };
			val2.InterpolationColors = val3;
			e.Graphics.FillRectangle((Brush)(object)val2, e.AffectedBounds);
			((Brush)val2).Dispose();
		}
		finally
		{
			((IDisposable)val2)?.Dispose();
		}
	}

	private void OnDrawToolStripDropDownMenu(ToolStripRenderEventArgs e, bool overlay)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		TextureBrush val = new TextureBrush(menupattern);
		val.WrapMode = (WrapMode)3;
		SolidBrush val2 = new SolidBrush(Color.FromArgb(50, ((ProfessionalColorTable)Colors).ImageMarginGradientMiddle));
		e.Graphics.FillRectangle((Brush)(object)val, e.AffectedBounds);
		if (overlay)
		{
			e.Graphics.FillRectangle((Brush)(object)val2, e.AffectedBounds);
		}
		((Brush)val2).Dispose();
		((Brush)val).Dispose();
	}

	protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		if (e.ToolStrip is MenuStrip)
		{
			Pen val = new Pen(((ProfessionalColorTable)Colors).ToolStripBorder);
			e.Graphics.DrawLine(val, new Point(0, ((Control)e.ToolStrip).Height - 1), new Point(((Control)e.ToolStrip).Width, ((Control)e.ToolStrip).Height - 1));
			val.Dispose();
		}
		else
		{
			base.OnRenderToolStripBorder(e);
		}
	}
}
