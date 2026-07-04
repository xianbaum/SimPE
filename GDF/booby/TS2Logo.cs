using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace booby;

[ToolboxBitmap(typeof(Panel))]
public class TS2Logo : UserControl
{
	private Image backg;

	private Image fore;

	private Rectangle bckimg;

	private Rectangle forimg;

	private Rectangle finimg;

	private int Lo;

	private int wif = 85;

	private int obve = 15;

	private int locx = 70;

	private int locy;

	private int stor;

	private bool begn;

	private int sped = 32;

	private float sc = 1f;

	private IContainer components;

	private PictureBox pbLogo;

	private Timer NipTimer;

	[Localizable(false)]
	[Browsable(false)]
	public Image BackImage
	{
		get
		{
			return backg;
		}
		set
		{
			backg = value;
		}
	}

	[Localizable(false)]
	[Browsable(false)]
	public Image ForeImage
	{
		get
		{
			return fore;
		}
		set
		{
			fore = value;
		}
	}

	[Browsable(false)]
	[Localizable(false)]
	public int ImageWidth
	{
		get
		{
			return wif;
		}
		set
		{
			wif = value;
		}
	}

	[Browsable(false)]
	[Localizable(false)]
	public int ImageLocationX
	{
		get
		{
			return locx;
		}
		set
		{
			locx = value;
		}
	}

	[Localizable(false)]
	[Browsable(false)]
	public int ImageLocationY
	{
		get
		{
			return locy;
		}
		set
		{
			locy = value;
		}
	}

	[Localizable(false)]
	[Browsable(false)]
	public int OverHead
	{
		get
		{
			return obve;
		}
		set
		{
			obve = value;
		}
	}

	[Browsable(false)]
	[Localizable(false)]
	public int Speed
	{
		get
		{
			return sped;
		}
		set
		{
			NipTimer.Interval = (sped = value);
		}
	}

	[Browsable(false)]
	[Localizable(false)]
	public bool Run
	{
		get
		{
			return begn;
		}
		set
		{
			begn = value;
			if (begn)
			{
				setsize();
				SetLogo();
				NipTimer.Start();
			}
			else
			{
				NipTimer.Stop();
			}
		}
	}

	[Category("Appearance")]
	[Description("the scale for the control")]
	[Localizable(false)]
	[Browsable(true)]
	[DefaultValue(typeof(float), "1")]
	public float Scaled
	{
		get
		{
			return sc;
		}
		set
		{
			if (!begn)
			{
				sc = value;
				((Control)this).Width = Convert.ToInt32(250f * value);
				((Control)this).Height = Convert.ToInt32(235f * value);
				((Control)this).Invalidate();
			}
		}
	}

	public TS2Logo()
	{
		InitializeComponent();
		NipTimer.Tick += TimerEventProcessor;
		NipTimer.Interval = sped;
		NipTimer.Stop();
	}

	private void setsize()
	{
		if (backg != null && fore != null)
		{
			if (stor != wif)
			{
				Lo = 0;
				stor = wif;
			}
			((Control)this).Width = Convert.ToInt32((float)backg.Width * sc);
			((Control)this).Height = Convert.ToInt32((float)(backg.Height + obve) * sc);
			bckimg.X = 0;
			bckimg.Y = Convert.ToInt32((float)obve * sc);
			bckimg.Width = Convert.ToInt32((float)backg.Width * sc);
			bckimg.Height = Convert.ToInt32((float)backg.Height * sc);
			forimg.Width = wif;
			forimg.Height = fore.Height;
			forimg.X = Lo;
			forimg.Y = 0;
			finimg.X = Convert.ToInt32((float)locx * sc);
			finimg.Y = Convert.ToInt32((float)locy * sc);
			finimg.Width = Convert.ToInt32((float)wif * sc);
			finimg.Height = Convert.ToInt32((float)fore.Height * sc);
		}
	}

	private void SetLogo()
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		try
		{
			if (pbLogo.Image != null)
			{
				pbLogo.Image.Dispose();
			}
			if (backg == null || fore == null)
			{
				NipTimer.Stop();
				begn = false;
				pbLogo.Image = null;
				return;
			}
			pbLogo.Image = (Image)new Bitmap(Convert.ToInt32((float)backg.Width * sc), Convert.ToInt32((float)(backg.Height + obve) * sc));
			Graphics val = Graphics.FromImage(pbLogo.Image);
			val.DrawImage(backg, bckimg);
			forimg.X = Lo;
			val.DrawImage(fore, finimg, forimg, (GraphicsUnit)2);
			Lo += wif;
			if (Lo + wif > fore.Width)
			{
				Lo = 0;
			}
			val.Dispose();
			val = null;
		}
		catch
		{
			NipTimer.Stop();
			begn = false;
			pbLogo.Image = null;
		}
	}

	private void TimerEventProcessor(object myObject, EventArgs myEventArgs)
	{
		SetLogo();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		((Component)(object)NipTimer).Dispose();
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		components = new Container();
		pbLogo = new PictureBox();
		NipTimer = new Timer(components);
		((ISupportInitialize)pbLogo).BeginInit();
		((Control)this).SuspendLayout();
		((Control)pbLogo).BackColor = Color.Transparent;
		((Control)pbLogo).BackgroundImageLayout = (ImageLayout)4;
		((Control)pbLogo).Dock = (DockStyle)5;
		((Control)pbLogo).Location = new Point(0, 0);
		((Control)pbLogo).Name = "pbLogo";
		((Control)pbLogo).Size = new Size(250, 235);
		pbLogo.SizeMode = (PictureBoxSizeMode)3;
		pbLogo.TabIndex = 0;
		pbLogo.TabStop = false;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Control)this).Controls.Add((Control)(object)pbLogo);
		((Control)this).Name = "TS2Logo";
		((Control)this).Size = new Size(250, 235);
		((ISupportInitialize)pbLogo).EndInit();
		((Control)this).ResumeLayout(false);
	}
}
