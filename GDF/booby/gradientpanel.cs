using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace booby;

[ToolboxBitmap(typeof(Panel))]
public class gradientpanel : Panel
{
	public enum ImageLayout
	{
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight,
		Centered,
		CenterLeft,
		CenterRight,
		CenterTop,
		CenterBottom
	}

	private IContainer components;

	private ImageLayout bklayout;

	private Color mStartColour = SystemColors.Control;

	private Color mMiddleColour = SystemColors.Control;

	private Color mEndColour = SystemColors.Control;

	private LinearGradientMode mGradient = (LinearGradientMode)2;

	private float mCentre = 0.7f;

	private Point mPicloc = new Point(0, 0);

	private float mPicZoom = 1f;

	private float mPicOpacity = 1f;

	private bool mPicFit;

	[Browsable(true)]
	[Category("Appearance")]
	[DefaultValue(typeof(Color), "SystemColors.Control")]
	[Description("the starting colour of the gradient background.")]
	[Localizable(false)]
	public Color StartColour
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

	[Description("the middle colour of the gradient background.")]
	[Browsable(true)]
	[DefaultValue(typeof(Color), "SystemColors.Control")]
	[Category("Appearance")]
	[Localizable(false)]
	public Color MiddleColour
	{
		get
		{
			return mMiddleColour;
		}
		set
		{
			mMiddleColour = value;
			((Control)this).Invalidate();
		}
	}

	[Localizable(false)]
	[Browsable(true)]
	[DefaultValue(typeof(Color), "SystemColors.Control")]
	[Description("the end colour of the gradient background.")]
	[Category("Appearance")]
	public Color EndColour
	{
		get
		{
			return mEndColour;
		}
		set
		{
			mEndColour = value;
			((Control)this).Invalidate();
		}
	}

	[Description("the gradient direction of the gradient background.")]
	[Browsable(true)]
	[Localizable(false)]
	[Category("Appearance")]
	[DefaultValue(typeof(LinearGradientMode), "ForwardDiagonal")]
	public LinearGradientMode Gradient
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mGradient;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			mGradient = value;
			((Control)this).Invalidate();
		}
	}

	[Category("Appearance")]
	[Description("the Centre of the gradient background, must be greater than 0 and less than 1.")]
	[Browsable(true)]
	[Localizable(false)]
	[DefaultValue(typeof(float), "0.7F")]
	public float GradCentre
	{
		get
		{
			return mCentre;
		}
		set
		{
			mCentre = value;
			((Control)this).Invalidate();
		}
	}

	[Description("Auto Scale the background Image To Fit the Panel.")]
	[Category("Appearance")]
	[DefaultValue(typeof(bool), "false")]
	[Browsable(true)]
	[Localizable(false)]
	public bool BackgroundImageZoomToFit
	{
		get
		{
			return mPicFit;
		}
		set
		{
			mPicFit = value;
			((Control)this).Invalidate();
		}
	}

	[Browsable(true)]
	[Category("Appearance")]
	[DefaultValue(typeof(float), "1")]
	[Description("the Scale (multiplication factor) of the background Image.")]
	[Localizable(false)]
	public float BackgroundImageScale
	{
		get
		{
			return mPicZoom;
		}
		set
		{
			if (!mPicFit)
			{
				mPicZoom = value;
				((Control)this).Invalidate();
			}
		}
	}

	[Localizable(false)]
	[Category("Appearance")]
	[Browsable(true)]
	[Description("the Location of the background Image, From BackgroundImageAnchor.")]
	[DefaultValue(typeof(Point), "0, 0")]
	public Point BackgroundImageLocation
	{
		get
		{
			return mPicloc;
		}
		set
		{
			mPicloc = value;
			((Control)this).Invalidate();
		}
	}

	[Category("Appearance")]
	[Description("the Anchor of the background Image.")]
	[DefaultValue(typeof(ImageLayout), "TopLeft")]
	[Browsable(true)]
	[Localizable(false)]
	public ImageLayout BackgroundImageAnchor
	{
		get
		{
			return bklayout;
		}
		set
		{
			bklayout = value;
			((Control)this).Invalidate();
		}
	}

	[Localizable(false)]
	[DefaultValue(typeof(float), "1")]
	[Description("the Opacity of the background Image. Must be between 0 and 1")]
	[Category("Appearance")]
	[Browsable(true)]
	public float BackgroundImageOpacity
	{
		get
		{
			return mPicOpacity;
		}
		set
		{
			mPicOpacity = value;
			((Control)this).Invalidate();
		}
	}

	[DefaultValue(typeof(ImageLayout), "Zoom")]
	[Category("Appearance")]
	[Description("not relevent.")]
	[Localizable(false)]
	[Browsable(false)]
	public override System.Windows.Forms.ImageLayout BackgroundImageLayout => (System.Windows.Forms.ImageLayout)4;

	[Localizable(false)]
	[DefaultValue(typeof(Color), "System.Drawing.Color.Transparent")]
	[Description("not relevent.")]
	[Category("Appearance")]
	[Browsable(false)]
	public override Color BackColor => Color.Transparent;

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
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		((Control)this).SuspendLayout();
		((Control)this).BackColor = Color.Transparent;
		((Control)this).BackgroundImageLayout = (System.Windows.Forms.ImageLayout)4;
		((Control)this).Font = new Font("Verdana", 8.25f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
		components = new Container();
		((Control)this).Name = "gradientpanel";
		((Control)this).Size = new Size(160, 120);
		((Control)this).ResumeLayout(false);
	}

	public gradientpanel()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			InitializeComponent();
		}
		catch
		{
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Expected O, but got Unknown
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Expected O, but got Unknown
		base.OnPaint(e);
		if (((Control)this).Width <= 0 || ((Control)this).Height <= 0)
		{
			return;
		}
		if (GradCentre < 0.01f)
		{
			GradCentre = (mCentre = 0.01f);
		}
		if (GradCentre > 0.99f)
		{
			GradCentre = (mCentre = 0.99f);
		}
		if (bklayout != ImageLayout.Centered)
		{
			mPicloc.Y = Math.Max(mPicloc.Y, 0);
			mPicloc.X = Math.Max(mPicloc.X, 0);
		}
		if (bklayout == ImageLayout.CenterLeft || bklayout == ImageLayout.CenterRight)
		{
			mPicloc.Y = 0;
		}
		if (bklayout == ImageLayout.CenterTop || bklayout == ImageLayout.CenterBottom)
		{
			mPicloc.X = 0;
		}
		Rectangle rectangle = new Rectangle(0, 0, ((Control)this).Width, ((Control)this).Height);
		LinearGradientBrush val = new LinearGradientBrush(rectangle, StartColour, MiddleColour, Gradient);
		try
		{
			ColorBlend val2 = new ColorBlend(3);
			val2.Colors = new Color[3] { StartColour, MiddleColour, EndColour };
			val2.Positions = new float[3] { 0f, GradCentre, 1f };
			val.InterpolationColors = val2;
			e.Graphics.FillRectangle((Brush)(object)val, rectangle);
			((Brush)val).Dispose();
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
		if (((Control)this).BackgroundImage == null || !(mPicOpacity > 0f))
		{
			return;
		}
		int num = ((Control)this).Width - Math.Abs(mPicloc.X);
		int num2 = ((Control)this).Height - Math.Abs(mPicloc.Y);
		if (num <= 5 || num2 <= 5)
		{
			return;
		}
		if (mPicFit)
		{
			if ((float)num2 / ((Control)this).BackgroundImage.PhysicalDimension.Height < (float)num / ((Control)this).BackgroundImage.PhysicalDimension.Width)
			{
				mPicZoom = (float)num2 / ((Control)this).BackgroundImage.PhysicalDimension.Height;
			}
			else
			{
				mPicZoom = (float)num / ((Control)this).BackgroundImage.PhysicalDimension.Width;
			}
		}
		int num3 = Convert.ToInt32((float)((Control)this).BackgroundImage.Width * mPicZoom);
		int num4 = Convert.ToInt32((float)((Control)this).BackgroundImage.Height * mPicZoom);
		int x = mPicloc.X;
		int y = mPicloc.Y;
		if (bklayout == ImageLayout.TopRight)
		{
			x = ((Control)this).Width - num3 - mPicloc.X;
		}
		else if (bklayout == ImageLayout.BottomRight)
		{
			x = ((Control)this).Width - num3 - mPicloc.X;
			y = ((Control)this).Height - num4 - mPicloc.Y;
		}
		else if (bklayout == ImageLayout.BottomLeft)
		{
			y = ((Control)this).Height - num4 - mPicloc.Y;
		}
		else if (bklayout == ImageLayout.Centered)
		{
			x = (((Control)this).Width - num3) / 2 + mPicloc.X / 2;
			y = (((Control)this).Height - num4) / 2 + mPicloc.Y / 2;
		}
		else if (bklayout == ImageLayout.CenterLeft)
		{
			y = (((Control)this).Height - num4) / 2;
		}
		else if (bklayout == ImageLayout.CenterTop)
		{
			x = (((Control)this).Width - num3) / 2;
		}
		else if (bklayout == ImageLayout.CenterRight)
		{
			y = (((Control)this).Height - num4) / 2;
			x = ((Control)this).Width - num3 - mPicloc.X;
		}
		else if (bklayout == ImageLayout.CenterBottom)
		{
			x = (((Control)this).Width - num3) / 2;
			y = ((Control)this).Height - num4 - mPicloc.Y;
		}
		Rectangle rectangle2 = new Rectangle(x, y, num3, num4);
		if (mPicOpacity >= 1f)
		{
			e.Graphics.DrawImage(((Control)this).BackgroundImage, rectangle2);
			return;
		}
		float[][] array = new float[5][]
		{
			new float[5] { 1f, 0f, 0f, 0f, 0f },
			new float[5] { 0f, 1f, 0f, 0f, 0f },
			new float[5] { 0f, 0f, 1f, 0f, 0f },
			new float[5] { 0f, 0f, 0f, mPicOpacity, 0f },
			new float[5] { 0f, 0f, 0f, 0f, 1f }
		};
		ColorMatrix val3 = new ColorMatrix(array);
		ImageAttributes val4 = new ImageAttributes();
		val4.SetColorMatrix(val3, (ColorMatrixFlag)0, (ColorAdjustType)1);
		e.Graphics.DrawImage(((Control)this).BackgroundImage, rectangle2, 0, 0, ((Control)this).BackgroundImage.Width, ((Control)this).BackgroundImage.Height, (GraphicsUnit)2, val4);
		val4.Dispose();
	}

	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
		((Control)this).Invalidate();
	}

	protected override void OnPaintBackground(PaintEventArgs pevent)
	{
	}

	protected override void OnScroll(ScrollEventArgs se)
	{
		base.OnScroll(se);
		((Control)this).Invalidate();
	}
}
