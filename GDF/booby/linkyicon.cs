using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace booby;

[ToolboxBitmap(typeof(LinkLabel))]
public class linkyicon : UserControl
{
	public delegate void EventHandler(object sender, EventArgs e);

	private int gap = 6;

	private string comnt = "";

	private IContainer components;

	private PictureBox Box;

	private LinkLabel Labal;

	private ToolTip tooltit;

	[DefaultValue(typeof(Image), null)]
	[Browsable(true)]
	[Description("Icon Image")]
	[Category("Appearance")]
	[Localizable(false)]
	public Image Icon
	{
		get
		{
			return Box.Image;
		}
		set
		{
			Box.Image = value;
			((Control)this).Invalidate();
		}
	}

	[Localizable(true)]
	[DefaultValue("I Like Girls")]
	[Browsable(true)]
	[Description("The text associated with the control")]
	[Category("Appearance")]
	public string Label
	{
		get
		{
			return ((Control)Labal).Text;
		}
		set
		{
			((Control)Labal).Text = value;
			((Control)this).Invalidate();
		}
	}

	[Description("Determines the colour of the hyperlink when the user clicks the link")]
	[Category("Appearance")]
	[Localizable(false)]
	[Browsable(true)]
	[DefaultValue(typeof(Color), "Color.FromArgb(255, 0, 0)")]
	public Color ActiveLinkColour
	{
		get
		{
			return Labal.ActiveLinkColor;
		}
		set
		{
			Labal.ActiveLinkColor = value;
			((Control)this).Invalidate();
		}
	}

	[Category("Appearance")]
	[DefaultValue(typeof(Color), "Color.FromArgb(85, 85, 85)")]
	[Localizable(false)]
	[Description("Determines the colour of the hyperlink when disabled")]
	[Browsable(true)]
	public Color DisabledLinkColour
	{
		get
		{
			return Labal.DisabledLinkColor;
		}
		set
		{
			Labal.DisabledLinkColor = value;
			((Control)this).Invalidate();
		}
	}

	[Localizable(false)]
	[Description("Determines the colour of the hyperlink in its default state")]
	[Category("Appearance")]
	[Browsable(true)]
	[DefaultValue(typeof(Color), "Color.FromArgb(0, 0, 255)")]
	public Color LinkColour
	{
		get
		{
			return Labal.LinkColor;
		}
		set
		{
			Labal.LinkColor = value;
			((Control)this).Invalidate();
		}
	}

	[DefaultValue(typeof(Color), "Color.FromArgb(128, 0, 128)")]
	[Description("Determines the colour of the hyperlink when the LinkVisited Property is set to true")]
	[Localizable(false)]
	[Browsable(true)]
	[Category("Appearance")]
	public Color VisitedLinkColour
	{
		get
		{
			return Labal.VisitedLinkColor;
		}
		set
		{
			Labal.VisitedLinkColor = value;
			((Control)this).Invalidate();
		}
	}

	[Localizable(false)]
	[Description("The empty space between the Icon and the Label")]
	[Browsable(true)]
	[DefaultValue(typeof(int), "6")]
	[Category("Appearance")]
	public int Gap
	{
		get
		{
			return gap;
		}
		set
		{
			gap = value;
			((Control)this).Invalidate();
		}
	}

	[Browsable(false)]
	[Localizable(false)]
	[Description("Gets the collection of links")]
	[Category("Behavior")]
	[DefaultValue(typeof(LinkLabel.LinkCollection), "")]
	public LinkLabel.LinkCollection Links => Labal.Links;

	[Browsable(true)]
	[Category("Behavior")]
	[DefaultValue("")]
	[Description("The ToolTip text associated with the control")]
	[Localizable(false)]
	public string Comment
	{
		get
		{
			return comnt;
		}
		set
		{
			comnt = value;
			if (comnt == "")
			{
				tooltit.RemoveAll();
			}
			else
			{
				tooltit.SetToolTip((Control)(object)Labal, value);
			}
		}
	}

	[Browsable(false)]
	[Description("not relevent.")]
	[Localizable(false)]
	[Category("Appearance")]
	[DefaultValue(typeof(Color), "System.Drawing.Color.Transparent")]
	public override Color BackColor => Color.Transparent;

	public event EventHandler LinkClicked;

	public linkyicon()
	{
		InitializeComponent();
		if (ThemeManager.ThemedForms)
		{
			ThemeManager.Global.AddControl(this);
		}
	}

	private void Labal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		this.LinkClicked(sender, (EventArgs)(object)e);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		((Control)Box).Height = ((Control)Labal).Height - 2;
		((Control)Box).Width = ((Control)Labal).Height - 2;
		((Control)Labal).Location = new Point(((Control)Labal).Height + gap, 0);
		((Control)this).Height = ((Control)Labal).Height;
		((Control)this).Width = ((Control)Labal).Height + ((Control)Labal).Width + gap;
		base.OnPaint(e);
	}

	protected override void OnFontChanged(EventArgs e)
	{
		base.OnFontChanged(e);
		((Control)Box).Height = ((Control)Labal).Height - 2;
		((Control)Box).Width = ((Control)Labal).Height - 2;
		((Control)Labal).Location = new Point(((Control)Labal).Height + gap, 0);
		((Control)this).Height = ((Control)Labal).Height;
		((Control)this).Width = ((Control)Labal).Height + ((Control)Labal).Width + gap;
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
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Expected O, but got Unknown
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Expected O, but got Unknown
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		components = new Container();
		Box = new PictureBox();
		Labal = new LinkLabel();
		tooltit = new ToolTip(components);
		((ISupportInitialize)Box).BeginInit();
		((Control)this).SuspendLayout();
		((Control)Box).BackColor = Color.Transparent;
		((Control)Box).Location = new Point(0, 1);
		((Control)Box).Margin = new Padding(0);
		((Control)Box).Name = "Box";
		((Control)Box).Size = new Size(11, 11);
		Box.SizeMode = (PictureBoxSizeMode)4;
		Box.TabIndex = 0;
		Box.TabStop = false;
		((Control)Labal).AutoSize = true;
		((Control)Labal).BackColor = Color.Transparent;
		((Control)Labal).Location = new Point(17, 0);
		((Control)Labal).Margin = new Padding(0);
		((Control)Labal).Name = "Labal";
		((Control)Labal).Size = new Size(78, 13);
		((Control)Labal).TabIndex = 1;
		((Label)Labal).TabStop = true;
		((Control)Labal).Text = "I Like Girls";
		Labal.LinkClicked += new LinkLabelLinkClickedEventHandler(Labal_LinkClicked);
		tooltit.ToolTipIcon = (ToolTipIcon)1;
		tooltit.ToolTipTitle = "Note";
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.Transparent;
		((Control)this).Controls.Add((Control)(object)Labal);
		((Control)this).Controls.Add((Control)(object)Box);
		((Control)this).Font = new Font("Verdana", 8.25f, (FontStyle)1);
		((Control)this).ForeColor = Color.Black;
		((Control)this).Margin = new Padding(0);
		((Control)this).Name = "linkyicon";
		((Control)this).Size = new Size(96, 13);
		((ISupportInitialize)Box).EndInit();
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
