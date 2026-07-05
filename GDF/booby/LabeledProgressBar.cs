using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace booby;

[ToolboxBitmap(typeof(ProgressBar))]
[DefaultEvent("ChangedValue")]
public class LabeledProgressBar : UserControl
{
	private Panel pntb;

	private Panel pnlb;

	private Panel pn;

	private Label lb;

	private SubExtProgressBar pb;

	private TextBox tb;

	private Container components;

	private double ns;

	private string nf;

	private string npre;

	private string nsu;

	private int no;

	private int dno;

	private bool internalupdate;

	[Browsable(true)]
	[Category("Appearance")]
	[Description("The overall shape of the progress bar")]
	[Localizable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public ProgresBarStyle Style
	{
		get
		{
			return pb.Style;
		}
		set
		{
			pb.Style = value;
		}
	}

	[Description("The filled colour on the progress bar")]
	[Browsable(true)]
	[Category("Appearance")]
	[Localizable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Color SelectedColor
	{
		get
		{
			return pb.SelectedColor;
		}
		set
		{
			pb.SelectedColor = value;
		}
	}

	[Category("Appearance")]
	[Browsable(true)]
	[Description("The unfilled colour on the progress bar")]
	[Localizable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Color UnselectedColor
	{
		get
		{
			return pb.UnselectedColor;
		}
		set
		{
			pb.UnselectedColor = value;
		}
	}

	[Browsable(false)]
	[Localizable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Color GradientEndColour
	{
		get
		{
			return pb.GradientEndColor;
		}
		set
		{
			pb.GradientEndColor = value;
		}
	}

	[Browsable(false)]
	[Localizable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Color GradientStartColour
	{
		get
		{
			return pb.GradientStartColor;
		}
		set
		{
			pb.GradientStartColor = value;
		}
	}

	[Browsable(false)]
	[Localizable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Color TextBoxBackColour
	{
		get
		{
			return ((Control)tb).BackColor;
		}
		set
		{
			((Control)tb).BackColor = value;
		}
	}

	[Localizable(false)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Color ProgressBackColour
	{
		get
		{
			return pb.ProgressBackColor;
		}
		set
		{
			pb.ProgressBackColor = value;
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int TokenWidth => pb.TokenWidth;

	[Localizable(false)]
	[Category("Appearance")]
	[Browsable(true)]
	[Description("The number of blocks in the progress bar")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int TokenCount
	{
		get
		{
			return pb.TokenCount;
		}
		set
		{
			pb.TokenCount = value;
		}
	}

	[Localizable(false)]
	[Browsable(true)]
	[Description("The width of the number display")]
	[Category("Appearance")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int TextboxWidth
	{
		get
		{
			return ((Control)pntb).Width;
		}
		set
		{
			((Control)pntb).Width = value;
		}
	}

	[Browsable(true)]
	[Localizable(true)]
	[Category("Appearance")]
	[Description("The Label Text for this control")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string LabelText
	{
		get
		{
			return ((Control)lb).Text;
		}
		set
		{
			((Control)lb).Text = value;
		}
	}

	[Description("The Label Font for this control")]
	[Browsable(true)]
	[Localizable(false)]
	[Category("Appearance")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Font LabelFont
	{
		get
		{
			return ((Control)lb).Font;
		}
		set
		{
			((Control)lb).Font = value;
		}
	}

	[Localizable(true)]
	[Description("The Label Width, the progress bar will change size to suit")]
	[Category("Appearance")]
	[Browsable(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int LabelWidth
	{
		get
		{
			return ((Control)pnlb).Width;
		}
		set
		{
			((Control)pnlb).Width = value;
		}
	}

	[Localizable(false)]
	[Browsable(true)]
	[Description("The Label Alignment (bottom or right)")]
	[Category("Appearance")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public DockStyle LabelAlignment
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return ((Control)lb).Dock;
		}
		set
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			((Control)lb).Dock = value;
		}
	}

	[Browsable(true)]
	[Category("Data")]
	[Description("Maiximum Value of the progress bar")]
	[Localizable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int Maximum
	{
		get
		{
			return pb.Maximum;
		}
		set
		{
			pb.Maximum = Math.Max(1, value);
			Update();
		}
	}

	[Localizable(false)]
	[Browsable(true)]
	[Description("Current Value")]
	[Category("Data")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int Value
	{
		get
		{
			return pb.Value + NumberOffset;
		}
		set
		{
			pb.Value = value - NumberOffset;
			Update();
		}
	}

	[Localizable(false)]
	[Browsable(true)]
	[Category("Data")]
	[Description("Sets the ratio between the Text and the Bar")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public double NumberScale
	{
		get
		{
			return ns;
		}
		set
		{
			ns = value;
			if (ns == 0.0)
			{
				ns = 1.0;
			}
			Update();
		}
	}

	[Category("Appearance")]
	[Localizable(false)]
	[Description("The Text display format (N + number of decimal places)")]
	[Browsable(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string NumberFormat
	{
		get
		{
			return nf;
		}
		set
		{
			nf = value;
			Update();
		}
	}

	private string NumberPrefix
	{
		get
		{
			return npre;
		}
		set
		{
			npre = value;
			Update();
		}
	}

	private string NumberSuffix
	{
		get
		{
			return nsu;
		}
		set
		{
			nsu = value;
			Update();
		}
	}

	[Browsable(true)]
	[Category("Data")]
	[Description("Offsets Value from the bar value")]
	[Localizable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int NumberOffset
	{
		get
		{
			return no;
		}
		set
		{
			no = value;
			Update();
		}
	}

	[Localizable(false)]
	[Category("Data")]
	[Description("Offsets the text value from the bar value")]
	[Browsable(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int DisplayOffset
	{
		get
		{
			return dno;
		}
		set
		{
			dno = value;
			Update();
		}
	}

	public event EventHandler Changed;

	public event EventHandler ChangedValue;

	public LabeledProgressBar()
	{
		base.SetStyle((ControlStyles)75778, true);
		((Control)this).BackColor = Color.Transparent;
		ns = 1.0;
		internalupdate = false;
		nf = "N0";
		nsu = (npre = "");
		no = (dno = 0);
		InitializeComponent();
		pb.TokenCount = 10;
		pb.Maximum = 100;
		pb.Value = 0;
		((Control)pb).TabStop = false;
		Update();
	}

	protected void FireChangedEvent(bool both)
	{
		if (this.Changed != null && both)
		{
			this.Changed(this, new EventArgs());
		}
		if (this.ChangedValue != null)
		{
			this.ChangedValue(this, new EventArgs());
		}
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
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Expected O, but got Unknown
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Expected O, but got Unknown
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Expected O, but got Unknown
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Expected O, but got Unknown
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Expected O, but got Unknown
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		pn = new Panel();
		pb = new SubExtProgressBar();
		lb = new Label();
		pntb = new Panel();
		tb = new TextBox();
		pnlb = new Panel();
		((Control)pn).SuspendLayout();
		((Control)pntb).SuspendLayout();
		((Control)pnlb).SuspendLayout();
		((Control)this).SuspendLayout();
		((Control)pn).Controls.Add((Control)(object)pb);
		((Control)pn).Dock = (DockStyle)5;
		((Control)pn).Location = new Point(80, 0);
		((Control)pn).Name = "pn";
		((Control)pn).Padding = new Padding(8, 0, 4, 0);
		((Control)pn).Size = new Size(280, 18);
		((Control)pn).TabIndex = 0;
		((Control)pb).BackColor = Color.Transparent;
		pb.BorderColor = Color.FromArgb(100, 0, 0, 0);
		((Control)pb).Dock = (DockStyle)5;
		pb.Gradient = (LinearGradientMode)1;
		pb.GradientEndColor = Color.White;
		pb.GradientStartColor = Color.White;
		((Control)pb).Location = new Point(8, 0);
		pb.Maximum = 100;
		pb.Minimum = 0;
		((Control)pb).Name = "pb";
		pb.ProgressBackColor = SystemColors.Window;
		pb.Quality = true;
		pb.SelectedColor = Color.YellowGreen;
		((Control)pb).Size = new Size(268, 18);
		pb.Style = ProgresBarStyle.Flat;
		((Control)pb).TabIndex = 0;
		pb.UnselectedColor = Color.Black;
		pb.UseTokenBuffer = true;
		pb.Value = 0;
		((Control)pb).MouseMove += new MouseEventHandler(pb_MouseMove);
		((Control)pb).MouseDown += new MouseEventHandler(pb_MouseDown);
		((Control)pb).MouseUp += new MouseEventHandler(pb_MouseUp);
		((Control)lb).AutoSize = true;
		((Control)lb).Dock = (DockStyle)2;
		((Control)lb).Font = new Font("Tahoma", 8.25f, (FontStyle)1, (GraphicsUnit)3, (byte)0);
		((Control)lb).Location = new Point(0, 5);
		((Control)lb).Name = "lb";
		((Control)lb).Size = new Size(0, 13);
		((Control)lb).TabIndex = 0;
		lb.TextAlign = (ContentAlignment)64;
		((Control)pntb).Controls.Add((Control)(object)tb);
		((Control)pntb).Dock = (DockStyle)4;
		((Control)pntb).Location = new Point(360, 0);
		((Control)pntb).Name = "pntb";
		((Control)pntb).Size = new Size(40, 18);
		((Control)pntb).TabIndex = 1;
		((TextBoxBase)tb).BorderStyle = (BorderStyle)0;
		((Control)tb).Dock = (DockStyle)2;
		((Control)tb).Location = new Point(0, 4);
		((Control)tb).Name = "tb";
		((Control)tb).Size = new Size(40, 14);
		((Control)tb).TabIndex = 0;
		((Control)tb).TextChanged += tb_TextChanged;
		((Control)pnlb).Controls.Add((Control)(object)lb);
		((Control)pnlb).Dock = (DockStyle)3;
		((Control)pnlb).Location = new Point(0, 0);
		((Control)pnlb).Name = "pnlb";
		((Control)pnlb).Size = new Size(80, 18);
		((Control)pnlb).TabIndex = 2;
		((Control)this).Controls.Add((Control)(object)pn);
		((Control)this).Controls.Add((Control)(object)pnlb);
		((Control)this).Controls.Add((Control)(object)pntb);
		((Control)this).Font = new Font("Tahoma", 8.25f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
		((Control)this).Name = "LabeledProgressBar";
		((Control)this).Padding = new Padding(0, 0, 0, 2);
		((Control)this).Size = new Size(400, 20);
		((Control)pn).ResumeLayout(false);
		((Control)pntb).ResumeLayout(false);
		((Control)pntb).PerformLayout();
		((Control)pnlb).ResumeLayout(false);
		((Control)pnlb).PerformLayout();
		((Control)this).ResumeLayout(false);
	}

	public void Update()
	{
		if (internalupdate)
		{
			return;
		}
		internalupdate = true;
		try
		{
			double num = (double)(float)Value * ns + (double)dno;
			((Control)tb).Text = npre + num.ToString(nf) + nsu;
		}
		catch
		{
		}
		finally
		{
			internalupdate = false;
		}
	}

	protected override void OnVisibleChanged(EventArgs e)
	{
		base.OnVisibleChanged(e);
		((Control)pb).Visible = ((Control)this).Visible;
	}

	private void tb_TextChanged(object sender, EventArgs e)
	{
		if (internalupdate)
		{
			FireChangedEvent(both: true);
			return;
		}
		internalupdate = true;
		try
		{
			int val = (int)(Convert.ToDouble(((Control)tb).Text) / ns - (double)dno);
			Value = Math.Max(pb.Minimum + no, Math.Min(pb.Maximum + no, val));
		}
		catch
		{
		}
		finally
		{
			FireChangedEvent(both: true);
			internalupdate = false;
		}
	}

	private void ProgressBarUpdate(MouseEventArgs e)
	{
		if (e != null)
		{
			int num = Math.Max(pb.Minimum, Math.Min(pb.Maximum, Convert.ToInt32(Math.Round((double)e.X / (double)pb.SensitiveWidth * (double)pb.Maximum))));
			bool flag = num != pb.Value;
			pb.Value = num;
			if (flag)
			{
				FireChangedEvent(both: false);
			}
			Update();
		}
	}

	private void pb_MouseDown(object sender, MouseEventArgs e)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Invalid comparison between Unknown and I4
		if ((int)e.Button == 1048576)
		{
			ProgressBarUpdate(e);
		}
	}

	private void pb_MouseMove(object sender, MouseEventArgs e)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Invalid comparison between Unknown and I4
		if ((int)e.Button == 1048576)
		{
			ProgressBarUpdate(e);
		}
	}

	private void pb_MouseUp(object sender, MouseEventArgs e)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Invalid comparison between Unknown and I4
		if ((int)e.Button == 1048576)
		{
			ProgressBarUpdate(e);
		}
	}

	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
	}

	public void CompleteRedraw()
	{
		pb.CompleteRedraw();
		((Control)pb).Refresh();
	}

	private void lb_SizeChanged(object sender, EventArgs e)
	{
	}
}
