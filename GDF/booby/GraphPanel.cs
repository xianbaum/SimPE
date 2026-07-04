using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace booby;

[ToolboxBitmap(typeof(Panel))]
public class GraphPanel : UserControl
{
	private Color mBarColour = Color.FromArgb(156, 148, 200);

	private Color mHiColour = SystemColors.Window;

	private Color mNegColour = Color.FromArgb(180, 32, 32);

	private int[] mdatas = new int[3] { 25, 50, 25 };

	private bool mStyle = true;

	private bool mMidline;

	private float gline = 1f;

	private double fMax;

	private IContainer components;

	private Label lbTitle;

	[Localizable(false)]
	[Browsable(false)]
	public float LineWidth
	{
		get
		{
			return gline;
		}
		set
		{
			gline = value;
			((Control)this).Invalidate();
		}
	}

	[Description("the colour for the graph bars")]
	[Localizable(false)]
	[Browsable(true)]
	[Category("Appearance")]
	[DefaultValue(typeof(Color), "Color.FromArgb(156, 148, 200)")]
	public Color BarColour
	{
		get
		{
			return mBarColour;
		}
		set
		{
			mBarColour = value;
			((Control)this).Invalidate();
		}
	}

	[Category("Appearance")]
	[Browsable(true)]
	[DefaultValue(typeof(Color), "SystemColors.Window")]
	[Description("the highlight colour for the graph bars")]
	[Localizable(false)]
	public Color HighlightColour
	{
		get
		{
			return mHiColour;
		}
		set
		{
			mHiColour = value;
			((Control)this).Invalidate();
		}
	}

	[Browsable(true)]
	[Description("the colour for the graph bars below the line")]
	[Localizable(false)]
	[Category("Appearance")]
	[DefaultValue(typeof(Color), "Color.FromArgb(180, 32, 32)")]
	public Color NegativeColour
	{
		get
		{
			return mNegColour;
		}
		set
		{
			mNegColour = value;
			((Control)this).Invalidate();
		}
	}

	[Browsable(true)]
	[Description("the data array to display")]
	[Localizable(false)]
	[Category("Appearance")]
	public int[] Datas
	{
		get
		{
			return mdatas;
		}
		set
		{
			mdatas = value;
			((Control)this).Invalidate();
		}
	}

	[Browsable(true)]
	[DefaultValue("Title")]
	[Description("The Title Text.")]
	[Localizable(true)]
	[Category("Appearance")]
	public string Title
	{
		get
		{
			return ((Control)lbTitle).Text;
		}
		set
		{
			((Control)lbTitle).Text = value;
			((Control)lbTitle).Visible = value != "";
			((Control)this).Invalidate();
		}
	}

	[Category("Appearance")]
	[DefaultValue(typeof(bool), "true")]
	[Description("true = Display Bar Graph, false = Display Line Graph")]
	[Localizable(false)]
	[Browsable(true)]
	public bool UseBars
	{
		get
		{
			return mStyle;
		}
		set
		{
			mStyle = value;
			((Control)this).Invalidate();
		}
	}

	[Browsable(true)]
	[Description("true = Allow negitive values, have centred line")]
	[Category("Appearance")]
	[DefaultValue(typeof(bool), "false")]
	[Localizable(false)]
	public bool AllowNegative
	{
		get
		{
			return mMidline;
		}
		set
		{
			mMidline = value;
			((Control)this).Invalidate();
		}
	}

	[Description("Set the maximum display height to a value higher than the highest input. To display precentages for example set this to 100")]
	[Browsable(true)]
	[Localizable(false)]
	[Category("Appearance")]
	[DefaultValue(typeof(double), "0")]
	public double ForceMax
	{
		get
		{
			return fMax;
		}
		set
		{
			fMax = value;
			((Control)this).Invalidate();
		}
	}

	public GraphPanel()
	{
		InitializeComponent();
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_0150: Expected O, but got Unknown
		//IL_07ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0807: Expected O, but got Unknown
		//IL_0807: Expected O, but got Unknown
		//IL_0825: Unknown result type (might be due to invalid IL or missing references)
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0845: Expected O, but got Unknown
		//IL_0845: Expected O, but got Unknown
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Expected O, but got Unknown
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Expected O, but got Unknown
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Expected O, but got Unknown
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Expected O, but got Unknown
		//IL_077b: Unknown result type (might be due to invalid IL or missing references)
		//IL_078b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0795: Expected O, but got Unknown
		//IL_0795: Expected O, but got Unknown
		//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d6: Expected O, but got Unknown
		//IL_07d6: Expected O, but got Unknown
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Expected O, but got Unknown
		//IL_025c: Expected O, but got Unknown
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Expected O, but got Unknown
		//IL_031a: Expected O, but got Unknown
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Expected O, but got Unknown
		//IL_02e7: Expected O, but got Unknown
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Expected O, but got Unknown
		//IL_02a8: Expected O, but got Unknown
		//IL_0726: Unknown result type (might be due to invalid IL or missing references)
		//IL_0764: Expected O, but got Unknown
		//IL_0688: Unknown result type (might be due to invalid IL or missing references)
		//IL_068f: Expected O, but got Unknown
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Expected O, but got Unknown
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Expected O, but got Unknown
		base.OnPaint(e);
		int num = 24;
		if (((Control)lbTitle).Text == "")
		{
			num = 8;
		}
		int num2 = ((Control)this).Height - num;
		int num3 = ((Control)this).Width - 28;
		double num4 = 0.0;
		double num5 = 4.0;
		double num6 = 0.0;
		float num7 = 2f;
		Pen val = new Pen(ThemeManager.Global.ThemeColorDark, 1f);
		SolidBrush val2 = new SolidBrush(ThemeManager.Global.ThemeColourXdark);
		e.Graphics.DrawLine(val, new Point(3, 4), new Point(3, num2 + 5));
		e.Graphics.DrawLine(val, new Point(num3 + 4, num2 + 5), new Point(num3 + 4, 4));
		if (mdatas.Length == 1)
		{
			e.Graphics.DrawLine(val, new Point(3, 4), new Point(num3 + 4, 4));
			if (mMidline)
			{
				e.Graphics.DrawLine(val, new Point(3, num2 + 6), new Point(num3 + 4, num2 + 6));
			}
		}
		if (mMidline)
		{
			num2 = (num2 + 1) / 2;
		}
		e.Graphics.DrawString("0", new Font("Times New Roman", 10f, (FontStyle)1), (Brush)(object)val2, (float)(num3 + 7), (float)(num2 - 6), new StringFormat());
		e.Graphics.DrawLine(val, new Point(3, num2 + 5), new Point(num3 + 4, num2 + 5));
		if (mdatas.Length > 0)
		{
			if (mMidline)
			{
				for (int i = 0; i < mdatas.Length; i++)
				{
					num6 = Math.Abs(mdatas[i]);
					if (num6 > num4)
					{
						num4 = num6;
					}
				}
			}
			else
			{
				for (int j = 0; j < mdatas.Length; j++)
				{
					mdatas[j] = Math.Max(mdatas[j], 0);
					if ((double)mdatas[j] > num4)
					{
						num4 = mdatas[j];
					}
				}
			}
			if (fMax > num4)
			{
				num4 = fMax;
			}
			if (num4 != 0.0)
			{
				if (num4 < 1000.0)
				{
					e.Graphics.DrawString(Convert.ToString(num4), new Font("Times New Roman", 10f, (FontStyle)1), (Brush)(object)val2, (float)(num3 + 4), 0f, new StringFormat());
					if (mMidline)
					{
						e.Graphics.DrawString("-" + Convert.ToString(num4), new Font("Times New Roman", 10f, (FontStyle)1), (Brush)(object)val2, (float)(num3 + 2), (float)(((Control)this).Height - (num + 4)), new StringFormat());
					}
				}
				else if (num4 < 10000.0)
				{
					e.Graphics.DrawString(Convert.ToString(num4), new Font("Times New Roman", 8f, (FontStyle)0), (Brush)(object)val2, (float)(num3 + 2), 0f, new StringFormat());
				}
				else
				{
					e.Graphics.DrawString(Convert.ToString(num4), new Font("Times New Roman", 6f, (FontStyle)0), (Brush)(object)val2, (float)(num3 - 4), 0f, new StringFormat());
				}
				num4 = (double)num2 / num4;
				num2 += 5;
				if (mStyle)
				{
					double num8;
					for (num8 = num3 / mdatas.Length; num8 * (double)mdatas.Length < (double)num3; num8 += 0.01)
					{
					}
					num7 = ((!(num8 < 5.0)) ? ((float)Math.Ceiling(num8)) : ((float)Math.Floor(num8)));
					Pen val3 = new Pen(mBarColour, num7);
					Pen val4 = new Pen(mNegColour, num7);
					if (num8 < 5.0)
					{
						num5 += num8 / 2.0;
					}
					int[] array = mdatas;
					foreach (int num9 in array)
					{
						try
						{
							if (Convert.ToInt32((double)num9 * num4) > 0)
							{
								if (num8 < 5.0)
								{
									e.Graphics.DrawLine(val3, new Point((int)num5, num2), new Point((int)num5, num2 - Convert.ToInt32((double)num9 * num4)));
								}
								else
								{
									Rectangle rectangle = new Rectangle((int)num5, num2 - Convert.ToInt32((double)num9 * num4), (int)num7, Convert.ToInt32((double)num9 * num4));
									LinearGradientBrush val5 = new LinearGradientBrush(rectangle, mBarColour, mHiColour, (LinearGradientMode)0);
									try
									{
										ColorBlend val6 = new ColorBlend(3);
										val6.Colors = new Color[3] { mBarColour, mHiColour, mBarColour };
										val6.Positions = new float[3] { 0f, 0.7f, 1f };
										val5.InterpolationColors = val6;
										e.Graphics.FillRectangle((Brush)(object)val5, rectangle);
										((Brush)val5).Dispose();
									}
									finally
									{
										((IDisposable)val5)?.Dispose();
									}
								}
							}
							else if (Convert.ToInt32((double)num9 * num4) < 0)
							{
								if (num8 < 5.0)
								{
									e.Graphics.DrawLine(val4, new Point((int)num5, num2), new Point((int)num5, Convert.ToInt32((double)Math.Abs(num9) * num4)));
								}
								else
								{
									Rectangle rectangle2 = new Rectangle((int)num5, num2, (int)num7, Convert.ToInt32((double)Math.Abs(num9) * num4));
									LinearGradientBrush val7 = new LinearGradientBrush(rectangle2, mBarColour, mHiColour, (LinearGradientMode)0);
									try
									{
										ColorBlend val8 = new ColorBlend(3);
										val8.Colors = new Color[3] { mNegColour, mHiColour, mNegColour };
										val8.Positions = new float[3] { 0f, 0.7f, 1f };
										val7.InterpolationColors = val8;
										e.Graphics.FillRectangle((Brush)(object)val7, rectangle2);
										((Brush)val7).Dispose();
									}
									finally
									{
										((IDisposable)val7)?.Dispose();
									}
								}
							}
						}
						catch
						{
						}
						num5 += num8;
					}
				}
				else if (mdatas.Length > 1)
				{
					double num10;
					for (num10 = num3 / (mdatas.Length - 1); num10 * (double)(mdatas.Length - 1) < (double)num3; num10 += 0.01)
					{
					}
					Pen val9 = new Pen(mBarColour, gline);
					int x = (int)num5;
					int num11 = mdatas[0];
					num5 += num10;
					for (int l = 1; l < mdatas.Length; l++)
					{
						try
						{
							e.Graphics.DrawLine(val9, new Point(x, num2 - Convert.ToInt32((double)num11 * num4)), new Point((int)num5, num2 - Convert.ToInt32((double)mdatas[l] * num4)));
						}
						catch
						{
						}
						x = (int)num5;
						num11 = mdatas[l];
						num5 += num10;
					}
				}
				else
				{
					e.Graphics.DrawLine(new Pen(mBarColour, gline), new Point((int)num5, num2 - Convert.ToInt32((double)mdatas[0] * num4)), new Point(num3 + 4, num2 - Convert.ToInt32((double)mdatas[0] * num4)));
				}
			}
			else
			{
				e.Graphics.DrawString("1", new Font("Times New Roman", 10f, (FontStyle)1), (Brush)(object)val2, (float)(num3 + 7), 0f, new StringFormat());
				if (mMidline)
				{
					e.Graphics.DrawString("-1", new Font("Times New Roman", 10f, (FontStyle)1), (Brush)(object)val2, (float)(num3 + 5), (float)(((Control)this).Height - (num + 4)), new StringFormat());
				}
			}
		}
		else
		{
			e.Graphics.DrawString("1", new Font("Times New Roman", 10f, (FontStyle)1), (Brush)(object)val2, (float)(num3 + 7), 0f, new StringFormat());
			if (mMidline)
			{
				e.Graphics.DrawString("-1", new Font("Times New Roman", 10f, (FontStyle)1), (Brush)(object)val2, (float)(num3 + 5), (float)(((Control)this).Height - (num + 4)), new StringFormat());
			}
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
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		lbTitle = new Label();
		((Control)this).SuspendLayout();
		((Control)lbTitle).Dock = (DockStyle)2;
		((Control)lbTitle).Font = new Font("Microsoft Sans Serif", 11.25f, (FontStyle)1, (GraphicsUnit)3, (byte)0);
		((Control)lbTitle).ForeColor = ThemeManager.Global.ThemeColourXdark;
		((Control)lbTitle).Location = new Point(0, 108);
		((Control)lbTitle).Name = "lbTitle";
		((Control)lbTitle).Size = new Size(228, 18);
		((Control)lbTitle).TabIndex = 0;
		((Control)lbTitle).Text = "Title";
		lbTitle.TextAlign = (ContentAlignment)2;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Control)this).BackColor = SystemColors.Window;
		((Control)this).BackgroundImageLayout = (ImageLayout)4;
		((Control)this).Controls.Add((Control)(object)lbTitle);
		((Control)this).Name = "GraphPanel";
		((Control)this).Size = new Size(228, 124);
		((Control)this).ResumeLayout(false);
	}
}
