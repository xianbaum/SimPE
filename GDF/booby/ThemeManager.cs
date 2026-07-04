using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using booby.Events;
using booby.Properties;
using booby.Renderer;

namespace booby;

public class ThemeManager : IDisposable
{
	private GuiTheme ctheme;

	private ArrayList ctrls;

	private ToolStripRenderer whidbey;

	private ToolStripRenderer whidbeysquare;

	private ToolStripRenderer square;

	private ToolStripColorTable colortable;

	private GlossyRenderer glossy;

	private GlossyRenderer glossysquare;

	private PinkyRenderer pinky;

	private PinkyRenderer pinkysquare;

	private GreenyRenderer greeny;

	private GreenyRenderer greenysquare;

	private PurpleRenderer purple;

	private PurpleRenderer purplesquare;

	private LilacRenderer lilac;

	private LilacRenderer lilacsquare;

	private PsychodelicRenderer psychodelic;

	private PsychodelicRenderer psychodelicsquare;

	private CoolblueRenderer coolblue;

	private CoolblueRenderer coolbluesquare;

	private GoldenRenderer golden;

	private GoldenRenderer goldensquare;

	private ThemeManager parent;

	private static ThemeManager tm;

	public GuiTheme CurrentTheme
	{
		get
		{
			return ctheme;
		}
		set
		{
			if (ctheme != value)
			{
				ctheme = value;
				SetTheme();
				if (this.ChangedTheme != null)
				{
					this.ChangedTheme(value);
				}
			}
		}
	}

	public Color ThemeColor
	{
		get
		{
			if (ctheme == GuiTheme.Everett)
			{
				return SystemColors.ControlDark;
			}
			if (ctheme == GuiTheme.Office2003)
			{
				return SystemColors.InactiveCaptionText;
			}
			if (ctheme == GuiTheme.Whidbey)
			{
				return Color.FromArgb(212, 210, 202);
			}
			if (ctheme == GuiTheme.Glossy)
			{
				return Color.FromArgb(173, 188, 206);
			}
			if (ctheme == GuiTheme.SoftPink)
			{
				return Color.FromArgb(226, 178, 216);
			}
			if (ctheme == GuiTheme.GreenGlossy)
			{
				return Color.FromArgb(152, 226, 152);
			}
			if (ctheme == GuiTheme.DeepPurple)
			{
				return Color.FromArgb(100, 50, 160);
			}
			if (ctheme == GuiTheme.SoftLilac)
			{
				return Color.FromArgb(192, 192, 255);
			}
			if (ctheme == GuiTheme.Psychodelic)
			{
				return Color.FromArgb(240, 180, 24);
			}
			if (ctheme == GuiTheme.Golden)
			{
				return Color.FromArgb(200, 150, 24);
			}
			return Color.FromArgb(162, 200, 255);
		}
	}

	public Color ThemeColorLight
	{
		get
		{
			if (ctheme == GuiTheme.Everett)
			{
				return SystemColors.ControlLight;
			}
			if (ctheme == GuiTheme.Office2003)
			{
				return SystemColors.InactiveCaption;
			}
			if (ctheme == GuiTheme.Whidbey)
			{
				return Color.FromArgb(252, 252, 252);
			}
			if (ctheme == GuiTheme.Glossy)
			{
				return Color.FromArgb(219, 228, 238);
			}
			if (ctheme == GuiTheme.SoftPink)
			{
				return Color.FromArgb(255, 242, 252);
			}
			if (ctheme == GuiTheme.GreenGlossy)
			{
				return Color.FromArgb(255, 255, 216);
			}
			if (ctheme == GuiTheme.DeepPurple)
			{
				return Color.FromArgb(245, 226, 255);
			}
			if (ctheme == GuiTheme.SoftLilac)
			{
				return Color.FromArgb(252, 248, 255);
			}
			if (ctheme == GuiTheme.Psychodelic)
			{
				return Color.FromArgb(255, 96, 160);
			}
			if (ctheme == GuiTheme.Golden)
			{
				return Color.FromArgb(240, 230, 200);
			}
			return Color.FromArgb(228, 240, 255);
		}
	}

	public Color ThemeColorDark
	{
		get
		{
			if (ctheme == GuiTheme.Everett)
			{
				return SystemColors.ControlDarkDark;
			}
			if (ctheme == GuiTheme.Office2003)
			{
				return SystemColors.Highlight;
			}
			if (ctheme == GuiTheme.Whidbey)
			{
				return Color.FromArgb(172, 168, 153);
			}
			if (ctheme == GuiTheme.Glossy)
			{
				return Color.FromArgb(117, 132, 151);
			}
			if (ctheme == GuiTheme.SoftPink)
			{
				return Color.FromArgb(190, 108, 160);
			}
			if (ctheme == GuiTheme.GreenGlossy)
			{
				return Color.FromArgb(8, 128, 64);
			}
			if (ctheme == GuiTheme.DeepPurple)
			{
				return Color.FromArgb(50, 20, 110);
			}
			if (ctheme == GuiTheme.SoftLilac)
			{
				return Color.FromArgb(150, 128, 190);
			}
			if (ctheme == GuiTheme.Psychodelic)
			{
				return Color.FromArgb(200, 8, 16);
			}
			if (ctheme == GuiTheme.Golden)
			{
				return Color.FromArgb(140, 100, 10);
			}
			return Color.FromArgb(80, 140, 240);
		}
	}

	public Color ThemeColourXdark
	{
		get
		{
			if (ctheme == GuiTheme.Everett)
			{
				return Color.FromArgb(50, 50, 50);
			}
			if (ctheme == GuiTheme.Office2003)
			{
				return SystemColors.Highlight;
			}
			if (ctheme == GuiTheme.Whidbey)
			{
				return SystemColors.WindowFrame;
			}
			if (ctheme == GuiTheme.Glossy)
			{
				return Color.FromArgb(44, 60, 75);
			}
			if (ctheme == GuiTheme.SoftPink)
			{
				return Color.FromArgb(96, 24, 60);
			}
			if (ctheme == GuiTheme.GreenGlossy)
			{
				return Color.FromArgb(4, 64, 32);
			}
			if (ctheme == GuiTheme.DeepPurple)
			{
				return Color.FromArgb(32, 4, 64);
			}
			if (ctheme == GuiTheme.SoftLilac)
			{
				return Color.FromArgb(54, 24, 64);
			}
			if (ctheme == GuiTheme.Psychodelic)
			{
				return Color.FromArgb(96, 4, 4);
			}
			if (ctheme == GuiTheme.Golden)
			{
				return Color.FromArgb(80, 40, 0);
			}
			return Color.FromArgb(24, 50, 112);
		}
	}

	public Color ThemeColorLighter
	{
		get
		{
			if (ctheme == GuiTheme.Everett)
			{
				return Color.FromArgb(238, 238, 238);
			}
			if (ctheme == GuiTheme.Office2003)
			{
				return Color.FromArgb(221, 236, 249);
			}
			if (ctheme == GuiTheme.Whidbey)
			{
				return Color.FromArgb(238, 238, 238);
			}
			if (ctheme == GuiTheme.Glossy)
			{
				return Color.FromArgb(238, 248, 255);
			}
			if (ctheme == GuiTheme.SoftPink)
			{
				return Color.FromArgb(255, 250, 255);
			}
			if (ctheme == GuiTheme.GreenGlossy)
			{
				return Color.FromArgb(226, 255, 240);
			}
			if (ctheme == GuiTheme.DeepPurple)
			{
				return Color.FromArgb(255, 236, 255);
			}
			if (ctheme == GuiTheme.SoftLilac)
			{
				return Color.FromArgb(240, 236, 255);
			}
			if (ctheme == GuiTheme.Psychodelic)
			{
				return Color.FromArgb(255, 255, 200);
			}
			if (ctheme == GuiTheme.Golden)
			{
				return Color.FromArgb(255, 250, 233);
			}
			return Color.FromArgb(238, 248, 255);
		}
	}

	public Color ThemeColorMild
	{
		get
		{
			if (ctheme == GuiTheme.Everett)
			{
				return SystemColors.ControlDark;
			}
			if (ctheme == GuiTheme.Office2003)
			{
				return Color.FromArgb(161, 175, 189);
			}
			if (ctheme == GuiTheme.Whidbey)
			{
				return Color.FromArgb(222, 220, 212);
			}
			if (ctheme == GuiTheme.Glossy)
			{
				return Color.FromArgb(196, 208, 232);
			}
			if (ctheme == GuiTheme.SoftPink)
			{
				return Color.FromArgb(236, 202, 236);
			}
			if (ctheme == GuiTheme.GreenGlossy)
			{
				return Color.FromArgb(208, 241, 184);
			}
			if (ctheme == GuiTheme.DeepPurple)
			{
				return Color.FromArgb(174, 138, 208);
			}
			if (ctheme == GuiTheme.SoftLilac)
			{
				return Color.FromArgb(240, 240, 255);
			}
			if (ctheme == GuiTheme.Psychodelic)
			{
				return Color.FromArgb(255, 160, 80);
			}
			if (ctheme == GuiTheme.Golden)
			{
				return Color.FromArgb(220, 180, 64);
			}
			return Color.FromArgb(200, 220, 255);
		}
	}

	public ThemeManager Parent
	{
		get
		{
			return parent;
		}
		set
		{
			if (parent != null)
			{
				ThemeManager themeManager = parent;
				themeManager.ChangedTheme = (ChangedThemeEvent)Delegate.Remove(themeManager.ChangedTheme, new ChangedThemeEvent(ThemeWasChanged));
			}
			parent = value;
			if (parent != null)
			{
				ThemeManager themeManager2 = parent;
				themeManager2.ChangedTheme = (ChangedThemeEvent)Delegate.Combine(themeManager2.ChangedTheme, new ChangedThemeEvent(ThemeWasChanged));
			}
		}
	}

	public static ThemeManager Global
	{
		get
		{
			if (tm == null)
			{
				tm = new ThemeManager((GuiTheme)savedTheme);
			}
			return tm;
		}
	}

	public static byte savedTheme
	{
		get
		{
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\ChrisHatch\\Booby");
			if (registryKey != null)
			{
				object value = registryKey.GetValue("theme", "7");
				return Convert.ToByte(value);
			}
			return 7;
		}
		set
		{
			RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\ChrisHatch\\Booby");
			registryKey.SetValue("theme", Convert.ToInt32(value));
		}
	}

	public static bool ThemedForms
	{
		get
		{
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\ChrisHatch\\Booby");
			if (registryKey != null)
			{
				object value = registryKey.GetValue("MoreTheming", false);
				return Convert.ToBoolean(value);
			}
			return false;
		}
		set
		{
			RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\ChrisHatch\\Booby");
			registryKey.SetValue("MoreTheming", Convert.ToInt32(value));
		}
	}

	protected event ChangedThemeEvent ChangedTheme;

	public ThemeManager(GuiTheme t)
	{
		whidbey = (ToolStripRenderer)(object)new AdvancedToolStripProfessionalRenderer((ProfessionalColorTable)(object)colortable);
		whidbeysquare = (ToolStripRenderer)(object)new ToolStripProfessionalSquareRenderer((ProfessionalColorTable)(object)colortable);
		colortable = new ToolStripColorTable();
		square = (ToolStripRenderer)(object)new ToolStripProfessionalSquareRenderer();
		glossysquare = new GlossyRenderer();
		glossy = new GlossyRenderer();
		glossy.RenderRoundedEdges = true;
		pinkysquare = new PinkyRenderer();
		pinky = new PinkyRenderer();
		pinky.RenderRoundedEdges = true;
		greenysquare = new GreenyRenderer();
		greeny = new GreenyRenderer();
		greeny.RenderRoundedEdges = true;
		purplesquare = new PurpleRenderer();
		purple = new PurpleRenderer();
		purple.RenderRoundedEdges = true;
		lilacsquare = new LilacRenderer();
		lilac = new LilacRenderer();
		lilac.RenderRoundedEdges = true;
		psychodelicsquare = new PsychodelicRenderer();
		psychodelic = new PsychodelicRenderer();
		psychodelic.RenderRoundedEdges = true;
		coolbluesquare = new CoolblueRenderer();
		coolblue = new CoolblueRenderer();
		coolblue.RenderRoundedEdges = true;
		goldensquare = new GoldenRenderer();
		golden = new GoldenRenderer();
		golden.RenderRoundedEdges = true;
		ctheme = t;
		parent = null;
		ctrls = new ArrayList();
	}

	~ThemeManager()
	{
		try
		{
			Dispose();
		}
		catch
		{
		}
	}

	public ThemeManager CreateChild()
	{
		ThemeManager themeManager = new ThemeManager((GuiTheme)savedTheme);
		themeManager.Parent = this;
		return themeManager;
	}

	private void SetTheme(ToolStrip sdm)
	{
		if (((Control)sdm).Parent is ToolStripContainer)
		{
			if (ctheme == GuiTheme.Everett)
			{
				sdm.RenderMode = (ToolStripRenderMode)1;
			}
			else if (ctheme == GuiTheme.Office2003)
			{
				sdm.RenderMode = (ToolStripRenderMode)2;
			}
			else if (ctheme == GuiTheme.Whidbey)
			{
				sdm.Renderer = whidbey;
			}
			else if (ctheme == GuiTheme.Glossy)
			{
				sdm.Renderer = (ToolStripRenderer)(object)glossy;
			}
			else if (ctheme == GuiTheme.SoftPink)
			{
				sdm.Renderer = (ToolStripRenderer)(object)pinky;
			}
			else if (ctheme == GuiTheme.GreenGlossy)
			{
				sdm.Renderer = (ToolStripRenderer)(object)greeny;
			}
			else if (ctheme == GuiTheme.DeepPurple)
			{
				sdm.Renderer = (ToolStripRenderer)(object)purple;
			}
			else if (ctheme == GuiTheme.SoftLilac)
			{
				sdm.Renderer = (ToolStripRenderer)(object)lilac;
			}
			else if (ctheme == GuiTheme.Psychodelic)
			{
				sdm.Renderer = (ToolStripRenderer)(object)psychodelic;
			}
			else if (ctheme == GuiTheme.Golden)
			{
				sdm.Renderer = (ToolStripRenderer)(object)golden;
			}
			else
			{
				sdm.Renderer = (ToolStripRenderer)(object)coolblue;
			}
		}
		else if (ctheme == GuiTheme.Everett)
		{
			sdm.RenderMode = (ToolStripRenderMode)1;
		}
		else if (ctheme == GuiTheme.Office2003)
		{
			sdm.Renderer = square;
		}
		else if (ctheme == GuiTheme.Whidbey)
		{
			sdm.Renderer = whidbeysquare;
		}
		else if (ctheme == GuiTheme.Glossy)
		{
			sdm.Renderer = (ToolStripRenderer)(object)glossysquare;
		}
		else if (ctheme == GuiTheme.SoftPink)
		{
			sdm.Renderer = (ToolStripRenderer)(object)pinkysquare;
		}
		else if (ctheme == GuiTheme.GreenGlossy)
		{
			sdm.Renderer = (ToolStripRenderer)(object)greenysquare;
		}
		else if (ctheme == GuiTheme.DeepPurple)
		{
			sdm.Renderer = (ToolStripRenderer)(object)purplesquare;
		}
		else if (ctheme == GuiTheme.SoftLilac)
		{
			sdm.Renderer = (ToolStripRenderer)(object)lilacsquare;
		}
		else if (ctheme == GuiTheme.Psychodelic)
		{
			sdm.Renderer = (ToolStripRenderer)(object)psychodelicsquare;
		}
		else if (ctheme == GuiTheme.Golden)
		{
			sdm.Renderer = (ToolStripRenderer)(object)goldensquare;
		}
		else
		{
			sdm.Renderer = (ToolStripRenderer)(object)coolblue;
		}
	}

	private void SetTheme(ToolStripContainer sdm)
	{
		SetTheme(sdm.TopToolStripPanel);
		SetTheme(sdm.RightToolStripPanel);
		SetTheme(sdm.BottomToolStripPanel);
		SetTheme(sdm.LeftToolStripPanel);
	}

	private void SetTheme(ToolStripPanel sdm)
	{
		if (((Control)sdm).Parent is ToolStripContainer)
		{
			if (ctheme == GuiTheme.Everett)
			{
				sdm.RenderMode = (ToolStripRenderMode)1;
			}
			else if (ctheme == GuiTheme.Office2003)
			{
				sdm.RenderMode = (ToolStripRenderMode)2;
			}
			else if (ctheme == GuiTheme.Whidbey)
			{
				sdm.Renderer = whidbey;
			}
			else if (ctheme == GuiTheme.Glossy)
			{
				sdm.Renderer = (ToolStripRenderer)(object)glossy;
			}
			else if (ctheme == GuiTheme.SoftPink)
			{
				sdm.Renderer = (ToolStripRenderer)(object)pinky;
			}
			else if (ctheme == GuiTheme.GreenGlossy)
			{
				sdm.Renderer = (ToolStripRenderer)(object)greeny;
			}
			else if (ctheme == GuiTheme.DeepPurple)
			{
				sdm.Renderer = (ToolStripRenderer)(object)purple;
			}
			else if (ctheme == GuiTheme.SoftLilac)
			{
				sdm.Renderer = (ToolStripRenderer)(object)lilac;
			}
			else if (ctheme == GuiTheme.Psychodelic)
			{
				sdm.Renderer = (ToolStripRenderer)(object)psychodelic;
			}
			else if (ctheme == GuiTheme.Golden)
			{
				sdm.Renderer = (ToolStripRenderer)(object)golden;
			}
			else
			{
				sdm.Renderer = (ToolStripRenderer)(object)coolblue;
			}
		}
		else if (ctheme == GuiTheme.Everett)
		{
			sdm.RenderMode = (ToolStripRenderMode)1;
		}
		else if (ctheme == GuiTheme.Office2003)
		{
			sdm.Renderer = square;
		}
		else if (ctheme == GuiTheme.Whidbey)
		{
			sdm.Renderer = whidbeysquare;
		}
		else if (ctheme == GuiTheme.Glossy)
		{
			sdm.Renderer = (ToolStripRenderer)(object)glossysquare;
		}
		else if (ctheme == GuiTheme.SoftPink)
		{
			sdm.Renderer = (ToolStripRenderer)(object)pinkysquare;
		}
		else if (ctheme == GuiTheme.GreenGlossy)
		{
			sdm.Renderer = (ToolStripRenderer)(object)greenysquare;
		}
		else if (ctheme == GuiTheme.DeepPurple)
		{
			sdm.Renderer = (ToolStripRenderer)(object)purplesquare;
		}
		else if (ctheme == GuiTheme.SoftLilac)
		{
			sdm.Renderer = (ToolStripRenderer)(object)lilacsquare;
		}
		else if (ctheme == GuiTheme.Psychodelic)
		{
			sdm.Renderer = (ToolStripRenderer)(object)psychodelicsquare;
		}
		else if (ctheme == GuiTheme.Golden)
		{
			sdm.Renderer = (ToolStripRenderer)(object)goldensquare;
		}
		else
		{
			sdm.Renderer = (ToolStripRenderer)(object)coolbluesquare;
		}
	}

	private void SetTheme(MenuStrip sdm)
	{
		if (ctheme == GuiTheme.Everett)
		{
			((ToolStrip)sdm).RenderMode = (ToolStripRenderMode)1;
		}
		else if (ctheme == GuiTheme.Office2003)
		{
			((ToolStrip)sdm).RenderMode = (ToolStripRenderMode)2;
		}
		else if (ctheme == GuiTheme.Whidbey)
		{
			((ToolStrip)sdm).Renderer = whidbey;
		}
		else if (ctheme == GuiTheme.Glossy)
		{
			((ToolStrip)sdm).Renderer = (ToolStripRenderer)(object)glossy;
		}
		else if (ctheme == GuiTheme.SoftPink)
		{
			((ToolStrip)sdm).Renderer = (ToolStripRenderer)(object)pinky;
		}
		else if (ctheme == GuiTheme.GreenGlossy)
		{
			((ToolStrip)sdm).Renderer = (ToolStripRenderer)(object)greeny;
		}
		else if (ctheme == GuiTheme.DeepPurple)
		{
			((ToolStrip)sdm).Renderer = (ToolStripRenderer)(object)purple;
		}
		else if (ctheme == GuiTheme.SoftLilac)
		{
			((ToolStrip)sdm).Renderer = (ToolStripRenderer)(object)lilac;
		}
		else if (ctheme == GuiTheme.Psychodelic)
		{
			((ToolStrip)sdm).Renderer = (ToolStripRenderer)(object)psychodelic;
		}
		else if (ctheme == GuiTheme.Golden)
		{
			((ToolStrip)sdm).Renderer = (ToolStripRenderer)(object)golden;
		}
		else
		{
			((ToolStrip)sdm).Renderer = (ToolStripRenderer)(object)coolblue;
		}
	}

	private void SetTheme(ContextMenuStrip sdm)
	{
		if (ctheme == GuiTheme.Everett)
		{
			((ToolStrip)sdm).RenderMode = (ToolStripRenderMode)1;
		}
		else if (ctheme == GuiTheme.Office2003)
		{
			((ToolStrip)sdm).RenderMode = (ToolStripRenderMode)2;
		}
		else if (ctheme == GuiTheme.Whidbey)
		{
			((ToolStrip)sdm).Renderer = whidbey;
		}
		else if (ctheme == GuiTheme.Glossy)
		{
			((ToolStrip)sdm).Renderer = (ToolStripRenderer)(object)glossy;
		}
		else if (ctheme == GuiTheme.SoftPink)
		{
			((ToolStrip)sdm).Renderer = (ToolStripRenderer)(object)pinky;
		}
		else if (ctheme == GuiTheme.GreenGlossy)
		{
			((ToolStrip)sdm).Renderer = (ToolStripRenderer)(object)greeny;
		}
		else if (ctheme == GuiTheme.DeepPurple)
		{
			((ToolStrip)sdm).Renderer = (ToolStripRenderer)(object)purple;
		}
		else if (ctheme == GuiTheme.SoftLilac)
		{
			((ToolStrip)sdm).Renderer = (ToolStripRenderer)(object)lilac;
		}
		else if (ctheme == GuiTheme.Psychodelic)
		{
			((ToolStrip)sdm).Renderer = (ToolStripRenderer)(object)psychodelic;
		}
		else if (ctheme == GuiTheme.Golden)
		{
			((ToolStrip)sdm).Renderer = (ToolStripRenderer)(object)golden;
		}
		else
		{
			((ToolStrip)sdm).Renderer = (ToolStripRenderer)(object)coolblue;
		}
	}

	private void SetTheme(Splitter tb)
	{
		((Control)tb).BackColor = ThemeColorDark;
	}

	private void SetTheme(Control c)
	{
		if (c is ListView || c is ListBox || c is CheckedListBox || c is TextBox || c is RichTextBox)
		{
			c.BackColor = ThemeColorLighter;
		}
		else
		{
			c.BackColor = ThemeColorLight;
		}
	}

	private void SetTheme(PropertyGrid c)
	{
		c.ViewBackColor = ThemeColorLight;
		if (ctheme == GuiTheme.Office2003)
		{
			((Control)c).BackColor = ThemeColorLight;
		}
		else
		{
			((Control)c).BackColor = ThemeColor;
		}
		c.HelpBackColor = ThemeColorDark;
		c.HelpForeColor = Color.FromArgb(255, 255, 255);
		c.LineColor = ThemeColorMild;
		c.CategoryForeColor = ThemeColourXdark;
	}

	private void SetTheme(Button c)
	{
		((ButtonBase)c).FlatStyle = (FlatStyle)1;
		((Control)c).BackgroundImageLayout = (ImageLayout)3;
		if (ctheme == GuiTheme.SoftPink)
		{
			((Control)c).BackgroundImage = (Image)(object)Resources.Butpink;
		}
		else if (ctheme == GuiTheme.GreenGlossy)
		{
			((Control)c).BackgroundImage = (Image)(object)Resources.Butgreen;
		}
		else if (ctheme == GuiTheme.DeepPurple)
		{
			((Control)c).BackgroundImage = (Image)(object)Resources.Butpurple;
		}
		else if (ctheme == GuiTheme.SoftLilac)
		{
			((Control)c).BackgroundImage = (Image)(object)Resources.Butlilac;
		}
		else if (ctheme == GuiTheme.Psychodelic)
		{
			((Control)c).BackgroundImage = (Image)(object)Resources.Butpsycho;
		}
		else if (ctheme == GuiTheme.Coolblue)
		{
			((Control)c).BackgroundImage = (Image)(object)Resources.Butblue;
		}
		else if (ctheme == GuiTheme.Golden)
		{
			((Control)c).BackgroundImage = (Image)(object)Resources.Butgolden;
		}
		else
		{
			((Control)c).BackgroundImage = (Image)(object)Resources.Butdef;
		}
	}

	private void SetTheme(ComboBox c)
	{
		c.FlatStyle = (FlatStyle)1;
		c.DropDownStyle = (ComboBoxStyle)1;
		if ((object)((object)((Control)c).Parent).GetType() == typeof(TaskBox))
		{
			((Control)c).BackColor = ThemeColorLight;
		}
		else if (ctheme == GuiTheme.SoftLilac)
		{
			((Control)c).BackColor = Color.FromArgb(216, 216, 255);
		}
		else if (ctheme == GuiTheme.DeepPurple || ctheme == GuiTheme.Golden)
		{
			((Control)c).BackColor = ThemeColorLight;
		}
		else
		{
			((Control)c).BackColor = ThemeColorMild;
		}
	}

	private void SetTheme(StatusStrip tb)
	{
		((ToolStrip)tb).BackColor = ThemeColorMild;
	}

	private void SetTheme(gradientpanel gp)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		if (ThemedForms)
		{
			if (ctheme == GuiTheme.Psychodelic)
			{
				gp.StartColour = Color.FromArgb(255, 240, 0);
			}
			else
			{
				gp.StartColour = ThemeColorLighter;
			}
			if (ctheme == GuiTheme.SoftLilac || ctheme == GuiTheme.SoftPink)
			{
				gp.MiddleColour = ThemeColor;
				((Control)gp).Font = new Font("Comic Sans MS", ((Control)gp).Font.Size);
			}
			else
			{
				gp.MiddleColour = ThemeColorMild;
			}
			if (ctheme == GuiTheme.Psychodelic)
			{
				gp.EndColour = ThemeColorDark;
			}
			else
			{
				gp.EndColour = ThemeColorLight;
			}
		}
		else
		{
			gp.GradCentre = 0.5f;
			gp.StartColour = ThemeColorLight;
			gp.MiddleColour = ThemeColorMild;
			gp.EndColour = ThemeColor;
		}
	}

	private void SetTheme(panelheader gp)
	{
		if (ThemedForms)
		{
			((Control)gp).BackColor = ThemeColorMild;
		}
		else
		{
			((Control)gp).BackColor = ThemeColorDark;
		}
		gp.StartColor = ThemeColorDark;
	}

	private void SetTheme(TaskBox gp)
	{
		gp.LeftHeaderColor = ThemeColor;
		gp.RightHeaderColor = ThemeColorDark;
		gp.BodyColor = ThemeColorLight;
		if (ctheme == GuiTheme.DeepPurple || ctheme == GuiTheme.Golden)
		{
			gp.HeaderTextColor = ThemeColorLighter;
		}
		else
		{
			gp.HeaderTextColor = ThemeColourXdark;
		}
		gp.BorderColor = ThemeColorLighter;
	}

	private void SetTheme(linkyicon gp)
	{
		gp.LinkColour = ThemeColorDark;
		gp.VisitedLinkColour = ThemeColorLighter;
		gp.DisabledLinkColour = ThemeColorMild;
	}

	private void SetTheme(LabeledProgressBar gp)
	{
		gp.GradientEndColour = ThemeColorLighter;
		gp.GradientStartColour = ThemeColorLighter;
		if (ctheme == GuiTheme.DeepPurple)
		{
			gp.SelectedColor = ThemeColor;
		}
		else
		{
			gp.SelectedColor = ThemeColorDark;
		}
	}

	public void Theme(object o)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Expected O, but got Unknown
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Expected O, but got Unknown
		if (o is Splitter)
		{
			SetTheme((Splitter)o);
		}
		else if (o is StatusStrip)
		{
			SetTheme((StatusStrip)o);
		}
		else if (o is ContextMenuStrip)
		{
			SetTheme((ContextMenuStrip)o);
		}
		else if (o is MenuStrip)
		{
			SetTheme((MenuStrip)o);
		}
		else if (o is ToolStrip)
		{
			SetTheme((ToolStrip)o);
		}
		else if (o is ToolStripContainer)
		{
			SetTheme((ToolStripContainer)o);
		}
		else if (o is PropertyGrid)
		{
			SetTheme((PropertyGrid)o);
		}
		else if (o is gradientpanel)
		{
			SetTheme((gradientpanel)o);
		}
		else if (o is panelheader)
		{
			SetTheme((panelheader)o);
		}
		else if (o is TaskBox)
		{
			SetTheme((TaskBox)o);
		}
		else if (o is linkyicon)
		{
			SetTheme((linkyicon)o);
		}
		else if (o is LabeledProgressBar)
		{
			SetTheme((LabeledProgressBar)o);
		}
		else if (o is Button)
		{
			SetTheme((Button)o);
		}
		else if (o is ComboBox)
		{
			SetTheme((ComboBox)o);
		}
		else if (o is Control)
		{
			SetTheme((Control)o);
		}
	}

	public void AddControl(object o)
	{
		if (!ctrls.Contains(o))
		{
			ctrls.Add(o);
			Theme(o);
		}
	}

	public void Clear()
	{
		ctrls.Clear();
	}

	public void RemoveControl(object o)
	{
		ctrls.Remove(o);
	}

	public void SetTheme()
	{
		foreach (object ctrl in ctrls)
		{
			Theme(ctrl);
		}
	}

	private void ThemeWasChanged(GuiTheme t)
	{
		CurrentTheme = t;
		switch (t)
		{
		case GuiTheme.Everett:
			savedTheme = 0;
			break;
		case GuiTheme.Office2003:
			savedTheme = 1;
			break;
		case GuiTheme.Whidbey:
			savedTheme = 2;
			break;
		case GuiTheme.Glossy:
			savedTheme = 3;
			break;
		case GuiTheme.SoftPink:
			savedTheme = 4;
			break;
		case GuiTheme.GreenGlossy:
			savedTheme = 5;
			break;
		case GuiTheme.DeepPurple:
			savedTheme = 6;
			break;
		case GuiTheme.SoftLilac:
			savedTheme = 7;
			break;
		case GuiTheme.Psychodelic:
			savedTheme = 8;
			break;
		case GuiTheme.Coolblue:
			savedTheme = 9;
			break;
		default:
			savedTheme = 10;
			break;
		}
	}

	public void Dispose()
	{
		Parent = null;
		Clear();
	}
}
