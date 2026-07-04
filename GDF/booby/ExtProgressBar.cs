using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace booby;

[DefaultEvent("ChangedValue")]
[ToolboxBitmap(typeof(ProgressBar))]
public class ExtProgressBar : UserControl
{
	private class GraphicsId
	{
		private int wd;

		private int hg;

		private Color cl;

		public GraphicsId(int wd, int hg, Color cl)
		{
			this.wd = wd;
			this.hg = hg;
			this.cl = cl;
		}

		public override int GetHashCode()
		{
			return wd;
		}

		public override bool Equals(object obj)
		{
			if (obj is GraphicsId graphicsId)
			{
				if (graphicsId.wd != wd)
				{
					return false;
				}
				if (graphicsId.hg != hg)
				{
					return false;
				}
				if (graphicsId.cl != cl)
				{
					return false;
				}
				return true;
			}
			return base.Equals(obj);
		}
	}

	private Container components;

	private bool usetokenbuffer;

	private ProgresBarStyle style;

	private int min;

	private int max;

	private int val;

	private bool quality;

	private Color col;

	private Color selcol;

	private Color bcol;

	private Color startgradcol;

	private Color endgradcol;

	private Color bgcol;

	private LinearGradientMode mGradient;

	private Bitmap cachedimgsel;

	private Bitmap cachedimg;

	private bool needredraw;

	private static Dictionary<GraphicsId, Image> tokenmap = new Dictionary<GraphicsId, Image>();

	private static Dictionary<GraphicsId, Image> seltokenmap = new Dictionary<GraphicsId, Image>();

	private int tw;

	private int tc;

	public bool UseTokenBuffer
	{
		get
		{
			return usetokenbuffer;
		}
		set
		{
			usetokenbuffer = value;
		}
	}

	public ProgresBarStyle Style
	{
		get
		{
			return style;
		}
		set
		{
			if (value != style)
			{
				style = value;
				if (style == ProgresBarStyle.Simple)
				{
					endgradcol = Color.Black;
				}
				else
				{
					endgradcol = Color.White;
				}
				CompleteRedraw();
				Invalidate();
			}
		}
	}

	public int Minimum
	{
		get
		{
			return min;
		}
		set
		{
			if (value != min)
			{
				min = Math.Min(value, Maximum);
				((Control)this).Refresh();
				FireChangedEvent(both: true);
			}
		}
	}

	public int Maximum
	{
		get
		{
			return max;
		}
		set
		{
			if (value != max)
			{
				max = Math.Max(Minimum, Math.Max(1, value));
				((Control)this).Refresh();
				FireChangedEvent(both: true);
			}
		}
	}

	public int Value
	{
		get
		{
			return val;
		}
		set
		{
			if (value != val)
			{
				val = Math.Max(Minimum, Math.Min(Maximum, value));
				((Control)this).Refresh();
				FireChangedEvent(both: true);
			}
		}
	}

	public bool Quality
	{
		get
		{
			return quality;
		}
		set
		{
			if (value != quality)
			{
				quality = value;
				Invalidate();
			}
		}
	}

	public Color UnselectedColor
	{
		get
		{
			return col;
		}
		set
		{
			if (value != col)
			{
				col = value;
				CompleteRedraw();
				((Control)this).Invalidate();
			}
		}
	}

	public Color SelectedColor
	{
		get
		{
			return selcol;
		}
		set
		{
			if (value != selcol)
			{
				selcol = value;
				CompleteRedraw();
				((Control)this).Invalidate();
			}
		}
	}

	public Color BorderColor
	{
		get
		{
			return bcol;
		}
		set
		{
			if (value != bcol)
			{
				bcol = value;
				Invalidate();
			}
		}
	}

	public Color ProgressBackColor
	{
		get
		{
			return bgcol;
		}
		set
		{
			if (value != bgcol)
			{
				bgcol = value;
				CompleteRedraw();
				((Control)this).Invalidate();
			}
		}
	}

	public Color GradientStartColor
	{
		get
		{
			return startgradcol;
		}
		set
		{
			if (value != startgradcol)
			{
				startgradcol = value;
				CompleteRedraw();
				((Control)this).Invalidate();
			}
		}
	}

	public Color GradientEndColor
	{
		get
		{
			return endgradcol;
		}
		set
		{
			if (value != endgradcol)
			{
				endgradcol = value;
				CompleteRedraw();
				((Control)this).Invalidate();
			}
		}
	}

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
		}
	}

	public int SensitiveWidth
	{
		get
		{
			if (Style == ProgresBarStyle.Simple)
			{
				return ((Control)this).Width;
			}
			return TokenOffset(TokenCount - 1) + TokenWidth;
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int TokenWidth => tw;

	public virtual int TokenCount
	{
		get
		{
			if (style == ProgresBarStyle.Balance && tc % 2 == 0)
			{
				return tc - 1;
			}
			return tc;
		}
		set
		{
			SetTokenCount(value, force: false);
		}
	}

	public double TokenMinSpacing => (float)(((Control)this).Width - 1 - TokenCount * TokenWidth) / ((float)TokenCount - 1f);

	public event EventHandler Changed;

	public ExtProgressBar()
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		base.SetStyle((ControlStyles)75794, true);
		((Control)this).BackColor = Color.Transparent;
		min = 0;
		max = 100;
		val = 0;
		tw = 6;
		quality = true;
		usetokenbuffer = true;
		style = ProgresBarStyle.Flat;
		startgradcol = Color.White;
		endgradcol = Color.White;
		bgcol = SystemColors.Window;
		bcol = Color.FromArgb(100, Color.Black);
		col = Color.Black;
		selcol = Color.YellowGreen;
		mGradient = (LinearGradientMode)1;
		InitializeComponent();
		base.OnResize((EventArgs)null);
		CompleteRedraw();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	protected void FireChangedEvent(bool both)
	{
		if (this.Changed != null)
		{
			this.Changed(this, new EventArgs());
		}
	}

	public void Invalidate()
	{
		if (base.DesignMode)
		{
			CompleteRedraw();
		}
		((Control)this).Invalidate();
	}

	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
	}

	protected override void OnSizeChanged(EventArgs e)
	{
		SetTokenCount(TokenCount, force: true);
		CompleteRedraw();
		base.OnSizeChanged(e);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		double num = (float)(Value - Minimum) / (float)(Maximum - Minimum);
		int num2 = (int)((double)SensitiveWidth * num) + 1;
		if (num == 0.0)
		{
			num2 = 0;
		}
		Rectangle rectangle = new Rectangle(0, 0, num2, ((Control)this).Height);
		Rectangle rectangle2 = new Rectangle(num2, 0, ((Control)this).Width - num2, ((Control)this).Height);
		SetGraphicsMode(e.Graphics, fast: true);
		e.Graphics.DrawImage((Image)(object)cachedimg, rectangle2, rectangle2, (GraphicsUnit)2);
		e.Graphics.DrawImage((Image)(object)cachedimgsel, rectangle, rectangle, (GraphicsUnit)2);
	}

	private void InitializeComponent()
	{
		((Control)this).SuspendLayout();
		((Control)this).Name = "ExtProgressBar";
		((Control)this).Size = new Size(150, 16);
		((Control)this).ResumeLayout(false);
	}

	internal static void SetGraphicsMode(Graphics g, bool fast)
	{
		if (fast)
		{
			g.SmoothingMode = (SmoothingMode)1;
			g.CompositingQuality = (CompositingQuality)1;
			g.InterpolationMode = (InterpolationMode)0;
		}
		else
		{
			g.SmoothingMode = (SmoothingMode)2;
			g.CompositingQuality = (CompositingQuality)2;
			g.InterpolationMode = (InterpolationMode)7;
		}
	}

	public void CompleteRedraw()
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		if (((Control)this).Width <= 8 || ((Control)this).Height <= 8)
		{
			return;
		}
		if (cachedimg != null)
		{
			((Image)cachedimg).Dispose();
		}
		if (cachedimgsel != null)
		{
			((Image)cachedimgsel).Dispose();
		}
		try
		{
			cachedimg = new Bitmap(((Control)this).Width, ((Control)this).Height);
			cachedimgsel = new Bitmap(((Control)this).Width, ((Control)this).Height);
		}
		catch
		{
			cachedimg = new Bitmap(1, 1);
			cachedimgsel = new Bitmap(1, 1);
			return;
		}
		try
		{
			Graphics val = Graphics.FromImage((Image)(object)cachedimg);
			Graphics val2 = Graphics.FromImage((Image)(object)cachedimgsel);
			CompleteRedraw(val, val2);
			val.Dispose();
			val2.Dispose();
		}
		catch
		{
		}
	}

	protected override void OnVisibleChanged(EventArgs e)
	{
		if (needredraw && ((Control)this).Visible)
		{
			CompleteRedraw();
		}
		base.OnVisibleChanged(e);
	}

	private void CompleteRedraw(Graphics g, Graphics gsel)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		if (!((Control)this).Visible)
		{
			needredraw = true;
			return;
		}
		SetGraphicsMode(g, fast: true);
		SetGraphicsMode(gsel, fast: true);
		g.FillRectangle((Brush)new SolidBrush(((Control)this).BackColor), 0, 0, ((Control)this).Width, ((Control)this).Height);
		SetGraphicsMode(g, !quality);
		SetGraphicsMode(gsel, !quality);
		if (style == ProgresBarStyle.Flat)
		{
			UserDrawFlat(g, gsel);
		}
		else if (style == ProgresBarStyle.Simple)
		{
			UserDrawSimple(g, gsel);
		}
		else if (style == ProgresBarStyle.Increase)
		{
			UserDrawIncrease(g, gsel);
		}
		else if (style == ProgresBarStyle.Decrease)
		{
			UserDrawDecrease(g, gsel);
		}
		else if (style == ProgresBarStyle.Balance)
		{
			UserDrawBalance(g, gsel);
		}
		needredraw = false;
	}

	protected void DrawTokens(Graphics g, Graphics gsel, int left, int top, int width, int height)
	{
		if (!usetokenbuffer)
		{
			DoDrawTokens(g, gsel, left, top, width, height);
			return;
		}
		GraphicsId graphicsId = new GraphicsId(width, height, SelectedColor);
		UpdateTokenBuffer(width, height, graphicsId);
		Image val = tokenmap[graphicsId];
		Image val2 = seltokenmap[graphicsId];
		g.DrawImageUnscaled(val, left, top);
		gsel.DrawImageUnscaled(val2, left, top);
	}

	private void UpdateTokenBuffer(int width, int height, GraphicsId sz)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		if (!tokenmap.ContainsKey(sz))
		{
			Bitmap val = new Bitmap(width + 1, height + 1);
			Graphics val2 = Graphics.FromImage((Image)(object)val);
			Bitmap val3 = new Bitmap(width + 1, height + 1);
			Graphics val4 = Graphics.FromImage((Image)(object)val3);
			DoDrawTokens(val2, val4, 0, 0, width, height);
			val2.Dispose();
			val4.Dispose();
			tokenmap.Add(sz, (Image)(object)val);
			seltokenmap.Add(sz, (Image)(object)val3);
		}
	}

	protected virtual void DoDrawTokens(Graphics g, Graphics gsel, int left, int top, int width, int height)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Expected O, but got Unknown
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		int num = 2;
		g.FillPath((Brush)new SolidBrush(UnselectedColor), RoundRectPath(left, top, width, height, num));
		gsel.FillPath((Brush)new SolidBrush(SelectedColor), RoundRectPath(left, top, width, height, num));
		SetGraphicsMode(g, fast: true);
		SetGraphicsMode(gsel, fast: true);
		LinearGradientBrush val = new LinearGradientBrush(new Rectangle(left, top, width, height), Color.FromArgb(80, GradientStartColor), Color.FromArgb(50, GradientEndColor), (LinearGradientMode)2);
		g.FillPath((Brush)(object)val, RoundRectPath(left, top, width, height, num));
		((Brush)val).Dispose();
		CreateGlossyGradient(gsel, left, top, width, height, num);
		SetGraphicsMode(g, !quality);
		SetGraphicsMode(gsel, !quality);
		g.DrawPath(new Pen(BorderColor), RoundRectPath(left, top, width, height, num));
		gsel.DrawPath(new Pen(BorderColor), RoundRectPath(left, top, width, height, num));
	}

	protected virtual void DrawTokenline(Graphics g, Graphics gsel, int left, int top, int width, int height)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		int radius = 2;
		gsel.FillPath((Brush)new SolidBrush(SelectedColor), RoundRectPath(left + 1, top + 1, width - 1, height - 1, radius));
		CreateGlossyGradient(gsel, left, top, width, height, 3);
	}

	private void CreateGlossyGradient(Graphics g, int left, int top, int width, int height, int rad)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		LinearGradientBrush val = new LinearGradientBrush(new Rectangle(left, top, width, height), GradientStartColor, Color.Transparent, Gradient);
		float[] factors = new float[4] { 0.2f, 0.7f, 1f, 1f };
		float[] positions = new float[4] { 0f, 0.1f, 0.5f, 1f };
		Blend val2 = new Blend();
		val2.Factors = factors;
		val2.Positions = positions;
		val.Blend = val2;
		g.FillPath((Brush)(object)val, RoundRectPath(left, top + 1, width, height - 2, rad));
		((Brush)val).Dispose();
		val = new LinearGradientBrush(new Rectangle(left, top, width, height), GradientEndColor, Color.Transparent, Gradient);
		factors = new float[4] { 1f, 1f, 0.7f, 0.5f };
		positions = new float[4] { 0f, 0.5f, 0.7f, 1f };
		val2 = new Blend();
		val2.Factors = factors;
		val2.Positions = positions;
		val.Blend = val2;
		g.FillPath((Brush)(object)val, RoundRectPath(left, top + 1, width, height - 1, rad));
		((Brush)val).Dispose();
	}

	protected virtual void UserDrawBackground(Graphics g, Graphics gsel)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		gsel.FillPath((Brush)new SolidBrush(ProgressBackColor), RoundRectPath(0, 0, ((Control)this).Width - 2, ((Control)this).Height - 1, 3));
		gsel.FillPath((Brush)new SolidBrush(Color.FromArgb(150, BorderColor)), RoundRectPath(0, 0, ((Control)this).Width - 2, ((Control)this).Height - 1, 3));
		gsel.FillPath((Brush)new SolidBrush(ProgressBackColor), RoundRectPath(1, 1, ((Control)this).Width - 3, ((Control)this).Height - 2, 3));
		gsel.DrawPath(new Pen(Color.FromArgb(200, BorderColor)), RoundRectPath(0, 0, ((Control)this).Width - 2, ((Control)this).Height - 1, 3));
		g.DrawImageUnscaled((Image)(object)cachedimgsel, 0, 0);
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

	protected virtual void UserDrawSimple(Graphics g, Graphics gsel)
	{
		UserDrawBackground(g, gsel);
		DrawTokenline(g, gsel, 2, 2, ((Control)this).Width - 7, ((Control)this).Height - 5);
	}

	protected virtual void UserDrawFlat(Graphics g, Graphics gsel)
	{
		for (int i = 0; i < TokenCount; i++)
		{
			int left = TokenOffset(i);
			DrawTokens(g, gsel, left, 0, TokenWidth, ((Control)this).Height - 1);
		}
	}

	protected virtual void UserDrawIncrease(Graphics g, Graphics gsel)
	{
		double num = (double)(((Control)this).Height - 1) / 4.0;
		double num2 = ((double)(((Control)this).Height - 1) - num) / (double)(TokenCount - 1);
		for (int i = 0; i < TokenCount; i++)
		{
			int left = TokenOffset(i);
			int num3 = (int)Math.Floor(num + (double)i * num2);
			int top = ((Control)this).Height - 1 - num3;
			DrawTokens(g, gsel, left, top, TokenWidth, num3);
		}
	}

	protected virtual void UserDrawDecrease(Graphics g, Graphics gsel)
	{
		double num = (double)(((Control)this).Height - 1) / 4.0;
		double num2 = ((double)(((Control)this).Height - 1) - num) / (double)(TokenCount - 1);
		for (int i = 0; i < TokenCount; i++)
		{
			int left = TokenOffset(i);
			int num3 = (int)Math.Floor(num + (double)(TokenCount - 1 - i) * num2);
			int top = ((Control)this).Height - 1 - num3;
			DrawTokens(g, gsel, left, top, TokenWidth, num3);
		}
	}

	protected virtual void UserDrawBalance(Graphics g, Graphics gsel)
	{
		double num = (double)(((Control)this).Height - 1) / 4.0;
		int num2 = (TokenCount - 1) / 2;
		double num3 = ((double)(((Control)this).Height - 1) - num) / (double)num2;
		for (int i = 0; i < TokenCount; i++)
		{
			int left = TokenOffset(i);
			int num4 = 0;
			num4 = ((i <= num2) ? ((int)Math.Floor(num + (double)(num2 - i) * num3)) : ((int)Math.Floor(num + (double)(i - num2) * num3)));
			int top = ((Control)this).Height - 1 - num4;
			DrawTokens(g, gsel, left, top, TokenWidth, num4);
		}
	}

	public int TokenOffset(int nr)
	{
		return (int)Math.Floor((double)nr * ((double)TokenWidth + Math.Floor(TokenMinSpacing)));
	}

	private void SetTokenWidth(int val)
	{
		if (tw != val)
		{
			tw = Math.Max(4, val);
			tc = Math.Max(2, (int)Math.Floor((double)((((Control)this).Width - 1) / (tw + 2))));
			CompleteRedraw();
			Invalidate();
		}
	}

	private void SetTokenCount(int val, bool force)
	{
		if (tc != val || force)
		{
			tc = Math.Max(2, val);
			tw = Math.Max(4, (((Control)this).Width - 1) / tc - 2);
			CompleteRedraw();
			Invalidate();
		}
	}
}
