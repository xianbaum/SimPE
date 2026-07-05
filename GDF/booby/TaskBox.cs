using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace booby;

[DesignTimeVisible(true)]
[ToolboxBitmap(typeof(GroupBox))]
public class TaskBox : Panel
{
	private Color lhc;

	private Color rhc;

	private Color bc;

	private Color bodc;

	private Color htc;

	private Font font;

	private Size icsz;

	private Point icpt;

	private Bitmap canvas;

	private IContainer components;

	private Image mIcon;

	private Image pretty;

	private string mstrHeaderText;

	private float mPicOpacity;

	private int headerh;

	private int gap;

	private int imgoff;

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Color LeftHeaderColor
	{
		get
		{
			return lhc;
		}
		set
		{
			if (lhc != value)
			{
				lhc = value;
				Invalidate();
			}
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Color RightHeaderColor
	{
		get
		{
			return rhc;
		}
		set
		{
			if (rhc != value)
			{
				rhc = value;
				Invalidate();
			}
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Color BorderColor
	{
		get
		{
			return bc;
		}
		set
		{
			if (bc != value)
			{
				bc = value;
				Invalidate();
			}
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Color HeaderTextColor
	{
		get
		{
			return htc;
		}
		set
		{
			if (htc != value)
			{
				htc = value;
				Invalidate();
			}
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Color BodyColor
	{
		get
		{
			return bodc;
		}
		set
		{
			if (bodc != value)
			{
				bodc = value;
				Invalidate();
			}
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Font HeaderFont
	{
		get
		{
			return font;
		}
		set
		{
			if (font != value)
			{
				font = value;
				Invalidate();
			}
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Size IconSize
	{
		get
		{
			return icsz;
		}
		set
		{
			if (icsz != value)
			{
				icsz = value;
				Invalidate();
			}
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Point IconLocation
	{
		get
		{
			return icpt;
		}
		set
		{
			if (icpt != value)
			{
				icpt = value;
				Invalidate();
			}
		}
	}

	[DefaultValue("Title")]
	[Localizable(true)]
	[Description("Caption text.")]
	[Browsable(true)]
	[Category("Appearance")]
	public string HeaderText
	{
		get
		{
			return mstrHeaderText;
		}
		set
		{
			mstrHeaderText = value;
			Invalidate();
		}
	}

	[Description("Icon")]
	[DefaultValue(typeof(Image), "")]
	[Localizable(false)]
	[Category("Appearance")]
	public Image Icon
	{
		get
		{
			return mIcon;
		}
		set
		{
			mIcon = value;
			Invalidate();
		}
	}

	[DefaultValue(typeof(int), "22")]
	[Localizable(false)]
	[Description("Height of the Headline")]
	[Category("Appearance")]
	public int HeaderHeight
	{
		get
		{
			return headerh;
		}
		set
		{
			headerh = value;
			Invalidate();
		}
	}

	[Description("Height of the Top Gap (for icon)")]
	[DefaultValue(typeof(int), "16")]
	[Localizable(false)]
	[Category("Appearance")]
	public int TopGap
	{
		get
		{
			return gap;
		}
		set
		{
			gap = value;
			Invalidate();
		}
	}

	[Description("BackImage")]
	[Category("Appearance")]
	[DefaultValue(typeof(Image), "")]
	[Localizable(false)]
	public Image BackImage
	{
		get
		{
			return pretty;
		}
		set
		{
			pretty = value;
			Invalidate();
		}
	}

	[Category("Appearance")]
	[DefaultValue(typeof(float), "1")]
	[Description("the Opacity of the BackImage. Must be between 0 and 1")]
	[Localizable(false)]
	public float BackImageOpacity
	{
		get
		{
			return mPicOpacity;
		}
		set
		{
			mPicOpacity = value;
			Invalidate();
		}
	}

	[Description("Offset the BackImage Up or Down")]
	[Localizable(false)]
	[Category("Appearance")]
	[DefaultValue(typeof(int), "0")]
	public int BackImageOffset
	{
		get
		{
			return imgoff;
		}
		set
		{
			imgoff = value;
			Invalidate();
		}
	}

	[Description("returns the usable region as Rectangle")]
	[Browsable(false)]
	internal Rectangle WorkspaceRect => new Rectangle(3, gap + 25, ((Control)this).Width - 7, ((Control)this).Height - (28 + gap));

	[DefaultValue(typeof(Image), "")]
	[Description("not relevent.")]
	[Category("Appearance")]
	[Localizable(false)]
	[Browsable(false)]
	public override Image BackgroundImage => null;

	[DefaultValue(typeof(ImageLayout), "Center")]
	[Category("Appearance")]
	[Localizable(false)]
	[Browsable(false)]
	[Description("not relevent.")]
	public override ImageLayout BackgroundImageLayout => (ImageLayout)2;

	public TaskBox()
	{
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		headerh = 22;
		gap = 16;
		imgoff = 0;
		mPicOpacity = 1f;
		mstrHeaderText = "";
		InitializeComponent();
		base.SetStyle((ControlStyles)16, true);
		base.SetStyle((ControlStyles)8192, true);
		base.SetStyle((ControlStyles)2, true);
		base.SetStyle((ControlStyles)65536, true);
		base.SetStyle((ControlStyles)2048, true);
		base.SetStyle((ControlStyles)1, true);
		((Control)this).BackColor = Color.Transparent;
		bc = SystemColors.ControlDarkDark;
		lhc = SystemColors.ControlDark;
		rhc = SystemColors.ControlDark;
		bodc = SystemColors.ControlLight;
		htc = SystemColors.ControlText;
		font = new Font(((Control)this).Font.Name, ((Control)this).Font.Size + 2f, (FontStyle)1, ((Control)this).Font.Unit);
		icsz = new Size(32, 32);
		icpt = new Point(4, 12);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			if (canvas != null)
			{
				((Image)canvas).Dispose();
			}
			canvas = null;
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		components = new Container();
		((ScrollableControl)this).DockPadding.Bottom = 4;
		((ScrollableControl)this).DockPadding.Left = 4;
		((ScrollableControl)this).DockPadding.Right = 4;
		((ScrollableControl)this).DockPadding.Top = 44;
		((Control)this).Name = "TaskBox";
	}

	protected void RebuildCanvas()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Expected O, but got Unknown
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Expected O, but got Unknown
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Expected O, but got Unknown
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Expected O, but got Unknown
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Expected O, but got Unknown
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Expected O, but got Unknown
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Expected O, but got Unknown
		if (canvas != null)
		{
			((Image)canvas).Dispose();
		}
		if (((Control)this).Width <= 7 || ((Control)this).Height <= headerh + 21)
		{
			canvas = null;
			return;
		}
		canvas = new Bitmap(((Control)this).Width, ((Control)this).Height);
		Graphics val = Graphics.FromImage((Image)(object)canvas);
		val.SmoothingMode = (SmoothingMode)2;
		Rectangle rectangle = new Rectangle(0, gap, ((Control)this).Width - 1, headerh);
		Rectangle rectangle2 = new Rectangle(3, rectangle.Bottom, ((Control)this).Width - 7, ((Control)this).Height - rectangle.Bottom - 4);
		Rectangle rectangle3 = new Rectangle(0, gap, ((Control)this).Width - 1, ((Control)this).Height - (gap + 1));
		Rectangle rectangle4 = ((imgoff >= 0) ? new Rectangle(3, rectangle.Bottom + imgoff, ((Control)this).Width - 7, ((Control)this).Height - rectangle.Bottom - 4 - imgoff) : new Rectangle(3, rectangle.Bottom, ((Control)this).Width - 7, ((Control)this).Height - rectangle.Bottom - 4 + imgoff));
		GraphicsPath val2 = new GraphicsPath();
		LinearGradientBrush val3 = new LinearGradientBrush(rectangle, LeftHeaderColor, RightHeaderColor, (LinearGradientMode)0);
		Pen val4 = new Pen(BorderColor, 1f);
		StringFormat val5 = new StringFormat();
		val5.Alignment = (StringAlignment)0;
		val5.LineAlignment = (StringAlignment)1;
		val5.Trimming = (StringTrimming)3;
		val5.FormatFlags = (StringFormatFlags)4096;
		val4.Alignment = (PenAlignment)1;
		val2 = RoundRectPath(rectangle3.X, rectangle3.Y, rectangle3.Width, rectangle3.Height, 7);
		val.FillPath((Brush)(object)val3, val2);
		val2 = RoundRectPath(rectangle2.X, rectangle2.Y, rectangle2.Width, rectangle2.Height, 7);
		val.FillPath((Brush)new SolidBrush(BodyColor), val2);
		val2 = RoundRectPath(rectangle3.X, rectangle3.Y, rectangle3.Width, rectangle3.Height, 7);
		val.DrawPath(val4, val2);
		Rectangle rectangle6;
		if (mIcon != null)
		{
			Size size = mIcon.Size;
			Rectangle rectangle5 = new Rectangle(IconLocation, size);
			val.DrawImage(mIcon, rectangle5, new Rectangle(0, 0, mIcon.Width, mIcon.Height), (GraphicsUnit)2);
			rectangle6 = new Rectangle(8 + size.Width + IconLocation.X, gap, ((Control)this).Width - (size.Width + IconLocation.X), headerh);
		}
		else
		{
			rectangle6 = new Rectangle(8, gap, ((Control)this).Width - 24, headerh);
		}
		val.DrawString(mstrHeaderText, HeaderFont, (Brush)new SolidBrush(HeaderTextColor), (RectangleF)rectangle6, val5);
		if (pretty != null && mPicOpacity > 0f)
		{
			float num = 1f;
			new Point(0, 0);
			if (rectangle4.Width > 5 && rectangle4.Height > 5)
			{
				num = ((!((float)rectangle4.Height / pretty.PhysicalDimension.Height < (float)rectangle4.Width / pretty.PhysicalDimension.Width)) ? ((float)rectangle4.Width / pretty.PhysicalDimension.Width) : ((float)rectangle4.Height / pretty.PhysicalDimension.Height));
				int num2 = Convert.ToInt32((float)pretty.Width * num);
				int num3 = Convert.ToInt32((float)pretty.Height * num);
				int x = rectangle4.Left + (rectangle4.Width - num2) / 2;
				int y = rectangle4.Top + (rectangle4.Height - num3) / 2;
				Rectangle rectangle7 = new Rectangle(x, y, num2, num3);
				if (mPicOpacity >= 1f)
				{
					val.DrawImage(pretty, rectangle7);
				}
				else
				{
					float[][] array = new float[5][]
					{
						new float[5] { 1f, 0f, 0f, 0f, 0f },
						new float[5] { 0f, 1f, 0f, 0f, 0f },
						new float[5] { 0f, 0f, 1f, 0f, 0f },
						new float[5] { 0f, 0f, 0f, mPicOpacity, 0f },
						new float[5] { 0f, 0f, 0f, 0f, 1f }
					};
					ColorMatrix val6 = new ColorMatrix(array);
					ImageAttributes val7 = new ImageAttributes();
					val7.SetColorMatrix(val6, (ColorMatrixFlag)0, (ColorAdjustType)1);
					val.DrawImage(pretty, rectangle7, 0, 0, pretty.Width, pretty.Height, (GraphicsUnit)2, val7);
					val7.Dispose();
				}
			}
		}
		val2.Dispose();
		((Brush)val3).Dispose();
		val4.Dispose();
		val5.Dispose();
		val.Dispose();
	}

	private static GraphicsPath RoundRectPath(int x, int y, int width, int height, int radius)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		GraphicsPath val = new GraphicsPath();
		if (radius > 1)
		{
			val.AddLine(x + radius, y, x + width - radius, y);
			val.AddArc(x + width - radius, y, radius, radius, 270f, 90f);
			val.AddLine(x + width, y + radius, x + width, y + height - radius);
			val.AddArc(x + width - radius, y + height - radius, radius, radius, 0f, 90f);
			val.AddLine(x + width - radius, y + height, x + radius, y + height);
			val.AddArc(x, y + height - radius, radius, radius, 90f, 90f);
			val.AddLine(x, y + height - radius, x, y + radius);
			val.AddArc(x, y, radius, radius, 180f, 90f);
			val.CloseFigure();
		}
		else
		{
			val.AddRectangle(new Rectangle(x, y, width, height));
		}
		return val;
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		RebuildCanvas();
		if (canvas != null)
		{
			e.Graphics.DrawImage((Image)(object)canvas, e.ClipRectangle, e.ClipRectangle, (GraphicsUnit)2);
		}
		base.OnPaint(e);
	}

	protected override void OnSizeChanged(EventArgs e)
	{
		base.OnSizeChanged(e);
		RebuildCanvas();
	}

	public void Invalidate()
	{
		RebuildCanvas();
		((Control)this).Invalidate();
	}
}
