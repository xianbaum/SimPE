using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace booby;

[ToolboxBitmap(typeof(Panel))]
public class panelheader : UserControl
{
	public delegate void EventHandler(object sender, EventArgs e);

	private Color mStartColour = SystemColors.ControlLightLight;

	private bool cc;

	private IContainer components;

	private Label lbLabel;

	private Button btnCommit;

	[Browsable(true)]
	[DefaultValue(typeof(bool), "false")]
	[Category("Appearance")]
	[Description("To Have or Not To Have a Commit button available")]
	[Localizable(false)]
	public bool CanCommit
	{
		get
		{
			return cc;
		}
		set
		{
			cc = value;
			((Control)btnCommit).Visible = cc;
			((Control)this).Invalidate();
		}
	}

	[Description("The Commit Button Name.")]
	[Category("Appearance")]
	[DefaultValue("Commit")]
	[Browsable(false)]
	[Localizable(true)]
	public string ButtonText
	{
		get
		{
			return ((Control)btnCommit).Text;
		}
		set
		{
			((Control)btnCommit).Text = value;
		}
	}

	[Browsable(true)]
	[Category("Appearance")]
	[DefaultValue(typeof(Color), "SystemColors.ControlLightLight")]
	[Description("the starting colour of the gradient background.")]
	[Localizable(false)]
	public Color StartColor
	{
		get
		{
			return mStartColour;
		}
		set
		{
			mStartColour = value;
			((Control)this).Invalidate();
		}
	}

	[Localizable(true)]
	[DefaultValue("Title")]
	[Browsable(true)]
	[Description("The Header Title.")]
	[Category("Appearance")]
	public string HeaderText
	{
		get
		{
			return ((Control)lbLabel).Text;
		}
		set
		{
			((Control)lbLabel).Text = value;
		}
	}

	public event EventHandler OnCommit;

	public panelheader()
	{
		InitializeComponent();
		ThemeManager.Global.AddControl(this);
		if (ThemeManager.ThemedForms)
		{
			ThemeManager.Global.AddControl(btnCommit);
		}
	}

	private void btnCommit_Click(object sender, EventArgs e)
	{
		this.OnCommit(sender, e);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
		base.OnPaint(e);
		if (((Control)this).Width > 0 && ((Control)this).Height > 0)
		{
			Rectangle rectangle = new Rectangle(0, 0, ((Control)this).Width, ((Control)this).Height);
			LinearGradientBrush val = new LinearGradientBrush(rectangle, StartColor, ((Control)this).BackColor, (LinearGradientMode)0);
			try
			{
				ColorBlend val2 = new ColorBlend(2);
				val2.Colors = new Color[2]
				{
					StartColor,
					((Control)this).BackColor
				};
				val2.Positions = new float[2] { 0f, 1f };
				val.InterpolationColors = val2;
				e.Graphics.FillRectangle((Brush)(object)val, rectangle);
				((Brush)val).Dispose();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
	}

	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
		((Control)btnCommit).Left = ((Control)this).Width - 4 - ((Control)btnCommit).Width;
		((Control)this).Refresh();
	}

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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Expected O, but got Unknown
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Expected O, but got Unknown
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		lbLabel = new Label();
		btnCommit = new Button();
		((Control)this).SuspendLayout();
		((Control)lbLabel).AutoSize = true;
		((Control)lbLabel).BackColor = Color.Transparent;
		((Control)lbLabel).Font = new Font("Verdana", 11.25f, (FontStyle)1, (GraphicsUnit)3, (byte)0);
		lbLabel.ImeMode = (ImeMode)0;
		((Control)lbLabel).Location = new Point(8, 3);
		((Control)lbLabel).Margin = new Padding(0);
		((Control)lbLabel).Name = "lbLabel";
		((Control)lbLabel).Size = new Size(43, 18);
		((Control)lbLabel).TabIndex = 1;
		((Control)lbLabel).Text = "Title";
		lbLabel.TextAlign = (ContentAlignment)16;
		((Control)btnCommit).Anchor = (AnchorStyles)5;
		((Control)btnCommit).BackColor = Color.Transparent;
		((ButtonBase)btnCommit).FlatStyle = (FlatStyle)3;
		((Control)btnCommit).Font = new Font("Tahoma", 8.25f);
		((Control)btnCommit).ForeColor = SystemColors.WindowText;
		((Control)btnCommit).Location = new Point(689, 0);
		((Control)btnCommit).Name = "btnCommit";
		((Control)btnCommit).Size = new Size(75, 23);
		((Control)btnCommit).TabIndex = 2;
		((Control)btnCommit).Text = "Commit";
		((ButtonBase)btnCommit).UseVisualStyleBackColor = true;
		((Control)btnCommit).Visible = false;
		((Control)btnCommit).Click += btnCommit_Click;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = SystemColors.ControlDark;
		((Control)this).Controls.Add((Control)(object)btnCommit);
		((Control)this).Controls.Add((Control)(object)lbLabel);
		((Control)this).Font = new Font("Verdana", 8.25f, (FontStyle)1);
		((Control)this).ForeColor = Color.White;
		((Control)this).Margin = new Padding(0);
		((Control)this).Name = "panelheader";
		((Control)this).Size = new Size(769, 24);
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
