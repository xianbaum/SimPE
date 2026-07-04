using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace booby;

[ToolboxBitmap(typeof(GroupBox))]
[DesignTimeVisible(true)]
public class Lineb : Panel
{
	[Category("Appearance")]
	[Description("not relevent.")]
	[Browsable(false)]
	[DefaultValue(typeof(Size), "(4, 4")]
	[Localizable(false)]
	public override Size MinimumSize => new Size(4, 4);

	public Lineb()
	{
		InitializeComponent();
		base.SetStyle((ControlStyles)16, true);
		base.SetStyle((ControlStyles)8192, true);
		base.SetStyle((ControlStyles)2, true);
		base.SetStyle((ControlStyles)65536, true);
		base.SetStyle((ControlStyles)2048, true);
		((Control)this).BackColor = Color.Transparent;
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		((Control)this).MinimumSize = new Size(4, 4);
		((Control)this).Name = "Lineb";
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		e.Graphics.SmoothingMode = (SmoothingMode)2;
		e.Graphics.DrawLine(new Pen(ThemeManager.Global.ThemeColorLight, 1f), new Point(0, 0), new Point(((Control)this).Width - 4, ((Control)this).Height - 4));
		e.Graphics.DrawLine(new Pen(ThemeManager.Global.ThemeColor, 1f), new Point(1, 1), new Point(((Control)this).Width - 3, ((Control)this).Height - 3));
		e.Graphics.DrawLine(new Pen(ThemeManager.Global.ThemeColorDark, 1f), new Point(2, 2), new Point(((Control)this).Width - 2, ((Control)this).Height - 2));
		e.Graphics.DrawLine(new Pen(ThemeManager.Global.ThemeColourXdark, 1f), new Point(3, 3), new Point(((Control)this).Width - 1, ((Control)this).Height - 1));
	}
}
