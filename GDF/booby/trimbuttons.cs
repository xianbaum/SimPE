using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace booby;

[ToolboxBitmap(typeof(Panel))]
public class trimbuttons : UserControl
{
	public delegate void EventHandler(object sender, EventArgs e);

	private IContainer components;

	private Button btup;

	private Button btdown;

	private Button btright;

	private Button btleft;

	private Label lbcentre;

	private int ex;

	private int ey;

	private bool ec;

	private bool eh = true;

	private bool ev = true;

	[Browsable(true)]
	[Category("Data")]
	[DefaultValue(typeof(int), "0")]
	[Description("X Value")]
	[Localizable(false)]
	public int X
	{
		get
		{
			return ex;
		}
		set
		{
			ex = value;
		}
	}

	[Browsable(true)]
	[Category("Data")]
	[DefaultValue(typeof(int), "0")]
	[Description("Y Value")]
	[Localizable(false)]
	public int Y
	{
		get
		{
			return ey;
		}
		set
		{
			ey = value;
		}
	}

	[Description("To Have or Not To Have a Re-Centring control available")]
	[DefaultValue(typeof(bool), "false")]
	[Browsable(true)]
	[Localizable(false)]
	[Category("Appearance")]
	public bool ShowCentre
	{
		get
		{
			return ec;
		}
		set
		{
			ec = value;
			((Control)lbcentre).Visible = ec;
			((Control)this).Invalidate();
		}
	}

	[Localizable(false)]
	[DefaultValue(typeof(bool), "true")]
	[Browsable(true)]
	[Description("To Have or Not To Have Up-Down Buttons available")]
	[Category("Appearance")]
	public bool ShowUPDown
	{
		get
		{
			return eh;
		}
		set
		{
			eh = value;
			Button obj = btup;
			bool visible = (((Control)btdown).Visible = eh);
			((Control)obj).Visible = visible;
			if (!eh)
			{
				ev = true;
				Button obj2 = btright;
				bool visible2 = (((Control)btleft).Visible = true);
				((Control)obj2).Visible = visible2;
			}
			((Control)this).Invalidate();
		}
	}

	[Browsable(true)]
	[Description("To Have or Not To Have Left-Right Buttons available")]
	[Localizable(false)]
	[Category("Appearance")]
	[DefaultValue(typeof(bool), "true")]
	public bool ShowLeftRight
	{
		get
		{
			return ev;
		}
		set
		{
			ev = value;
			Button obj = btright;
			bool visible = (((Control)btleft).Visible = ev);
			((Control)obj).Visible = visible;
			if (!ev)
			{
				eh = true;
				Button obj2 = btup;
				bool visible2 = (((Control)btdown).Visible = true);
				((Control)obj2).Visible = visible2;
			}
			((Control)this).Invalidate();
		}
	}

	public event EventHandler OnButtonPress;

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Expected O, but got Unknown
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Expected O, but got Unknown
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Expected O, but got Unknown
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(trimbuttons));
		btup = new Button();
		btdown = new Button();
		btright = new Button();
		btleft = new Button();
		lbcentre = new Label();
		((Control)this).SuspendLayout();
		((Control)btup).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)btup).FlatStyle = (FlatStyle)1;
		((ButtonBase)btup).Image = (Image)componentResourceManager.GetObject("btup.Image");
		((Control)btup).Location = new Point(38, 4);
		((Control)btup).Name = "btup";
		((Control)btup).Size = new Size(24, 34);
		((Control)btup).TabIndex = 0;
		((ButtonBase)btup).UseVisualStyleBackColor = true;
		((Control)btup).Click += btup_Click;
		((Control)btdown).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)btdown).FlatStyle = (FlatStyle)1;
		((ButtonBase)btdown).Image = (Image)componentResourceManager.GetObject("btdown.Image");
		((Control)btdown).Location = new Point(38, 62);
		((Control)btdown).Name = "btdown";
		((Control)btdown).Size = new Size(24, 34);
		((Control)btdown).TabIndex = 1;
		((ButtonBase)btdown).UseVisualStyleBackColor = true;
		((Control)btdown).Click += btdown_Click;
		((Control)btright).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)btright).FlatStyle = (FlatStyle)1;
		((ButtonBase)btright).Image = (Image)componentResourceManager.GetObject("btright.Image");
		((Control)btright).Location = new Point(62, 38);
		((Control)btright).Name = "btright";
		((Control)btright).Size = new Size(34, 24);
		((Control)btright).TabIndex = 2;
		((ButtonBase)btright).UseVisualStyleBackColor = true;
		((Control)btright).Click += btright_Click;
		((Control)btleft).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)btleft).FlatStyle = (FlatStyle)1;
		((ButtonBase)btleft).Image = (Image)componentResourceManager.GetObject("btleft.Image");
		((Control)btleft).Location = new Point(4, 38);
		((Control)btleft).Name = "btleft";
		((Control)btleft).Size = new Size(34, 24);
		((Control)btleft).TabIndex = 3;
		((ButtonBase)btleft).UseVisualStyleBackColor = true;
		((Control)btleft).Click += btleft_Click;
		((Control)lbcentre).AutoSize = true;
		((Control)lbcentre).Location = new Point(42, 42);
		((Control)lbcentre).Name = "lbcentre";
		((Control)lbcentre).Size = new Size(17, 16);
		((Control)lbcentre).TabIndex = 4;
		((Control)lbcentre).Text = "0";
		((Control)lbcentre).Visible = false;
		((Control)lbcentre).Click += lbcentre_Click;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.Transparent;
		((Control)this).Controls.Add((Control)(object)btleft);
		((Control)this).Controls.Add((Control)(object)btright);
		((Control)this).Controls.Add((Control)(object)btdown);
		((Control)this).Controls.Add((Control)(object)btup);
		((Control)this).Controls.Add((Control)(object)lbcentre);
		((Control)this).Font = new Font("Verdana", 9.75f, (FontStyle)1, (GraphicsUnit)3, (byte)0);
		((Control)this).Margin = new Padding(0);
		((Control)this).MaximumSize = new Size(100, 100);
		((Control)this).MinimumSize = new Size(100, 100);
		((Control)this).Name = "trimbuttons";
		((Control)this).Size = new Size(100, 100);
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}

	public trimbuttons()
	{
		InitializeComponent();
		ThemeManager.Global.AddControl(btup);
		ThemeManager.Global.AddControl(btdown);
		ThemeManager.Global.AddControl(btright);
		ThemeManager.Global.AddControl(btleft);
	}

	private void btup_Click(object sender, EventArgs e)
	{
		ey++;
		this.OnButtonPress(sender, e);
	}

	private void btright_Click(object sender, EventArgs e)
	{
		ex++;
		this.OnButtonPress(sender, e);
	}

	private void btdown_Click(object sender, EventArgs e)
	{
		ey--;
		this.OnButtonPress(sender, e);
	}

	private void btleft_Click(object sender, EventArgs e)
	{
		ex--;
		this.OnButtonPress(sender, e);
	}

	private void lbcentre_Click(object sender, EventArgs e)
	{
		ex = (ey = 0);
		this.OnButtonPress(sender, e);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		base.OnPaint(e);
		if (((Control)this).Width > 1 && ((Control)this).Height > 1)
		{
			Rectangle rectangle = new Rectangle(0, 0, ((Control)this).Width - 1, ((Control)this).Height - 1);
			LinearGradientBrush val = new LinearGradientBrush(rectangle, ThemeManager.Global.ThemeColorLight, ThemeManager.Global.ThemeColor, (LinearGradientMode)2);
			try
			{
				ColorBlend val2 = new ColorBlend(2);
				val2.Colors = new Color[2]
				{
					ThemeManager.Global.ThemeColorLight,
					ThemeManager.Global.ThemeColor
				};
				val2.Positions = new float[2] { 0f, 1f };
				val.InterpolationColors = val2;
				e.Graphics.FillEllipse((Brush)(object)val, rectangle);
				((Brush)val).Dispose();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			e.Graphics.DrawEllipse(new Pen(ThemeManager.Global.ThemeColorDark), rectangle);
		}
	}
}
