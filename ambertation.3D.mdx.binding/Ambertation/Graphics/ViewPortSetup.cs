using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Ambertation.Graphics;

public class ViewPortSetup : Form
{
	private IContainer components;

	private PropertyGrid pg;

	private ViewportSetting vp;

	private static bool visible;

	private DirectXPanel panel;

	public static bool Visible => visible;

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		((Form)this).Dispose(disposing);
	}

	private void InitializeComponent()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		pg = new PropertyGrid();
		((Control)this).SuspendLayout();
		((Control)pg).Dock = (DockStyle)5;
		pg.HelpVisible = false;
		pg.LineColor = SystemColors.ScrollBar;
		((Control)pg).Location = new Point(0, 0);
		((Control)pg).Name = "pg";
		((Control)pg).Size = new Size(248, 429);
		((Control)pg).TabIndex = 4;
		pg.ToolbarVisible = false;
		((Form)this).AutoScaleBaseSize = new Size(5, 14);
		((Form)this).ClientSize = new Size(248, 429);
		((Control)this).Controls.Add((Control)(object)pg);
		((Control)this).Font = new Font("Tahoma", 8.25f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
		((Form)this).FormBorderStyle = (FormBorderStyle)5;
		((Control)this).Name = "ViewPortSetup";
		((Control)this).Text = "ViewPort Setup";
		((Control)this).ResumeLayout(false);
	}

	private ViewPortSetup()
	{
		InitializeComponent();
	}

	public static ViewPortSetup Execute(ViewportSetting vp, DirectXPanel panel)
	{
		visible = true;
		ViewPortSetup viewPortSetup = new ViewPortSetup();
		viewPortSetup.vp = vp;
		viewPortSetup.panel = panel;
		viewPortSetup.SetContent(vp);
		((Control)viewPortSetup).Show();
		return viewPortSetup;
	}

	private void pg_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
	{
		panel.Reset();
	}

	public static void Hide(ViewPortSetup f)
	{
		try
		{
			((Form)f).Close();
			visible = false;
		}
		catch
		{
		}
	}

	private void SetContent(ViewportSetting vp)
	{
		pg.SelectedObject = vp;
	}
}
