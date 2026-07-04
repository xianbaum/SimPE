using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Ambertation.Drawing;
using Ambertation.Scenes;
using Ambertation.Scenes.Collections;

namespace Ambertation.Graphics;

public class RenderSelection : UserControl
{
	private IContainer components;

	private ListBox lb;

	private Scene scn;

	private SceneToMesh stm;

	private DirectXPanel dx;

	public Scene Scene
	{
		get
		{
			return scn;
		}
		set
		{
			scn = value;
			SetContent();
		}
	}

	public DirectXPanel DirectXPanel
	{
		get
		{
			return dx;
		}
		set
		{
			if (dx != null)
			{
				dx.ResetDevice -= dx_ResetDevice;
			}
			dx = value;
			if (dx != null)
			{
				dx.ResetDevice += dx_ResetDevice;
			}
			SetContent();
		}
	}

	protected override void Dispose(bool disposing)
	{
		dx = null;
		scn = null;
		if (stm != null)
		{
			stm.Dispose();
		}
		stm = null;
		if (disposing && components != null)
		{
			components.Dispose();
		}
		((ContainerControl)this).Dispose(disposing);
	}

	private void InitializeComponent()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		lb = new ListBox();
		((Control)this).SuspendLayout();
		((Control)lb).Dock = (DockStyle)5;
		lb.IntegralHeight = false;
		((Control)lb).Location = new Point(0, 0);
		((Control)lb).Name = "lb";
		lb.SelectionMode = (SelectionMode)3;
		((Control)lb).Size = new Size(172, 329);
		((Control)lb).TabIndex = 0;
		lb.SelectedIndexChanged += lb_SelectedIndexChanged;
		((Control)this).Controls.Add((Control)(object)lb);
		((Control)this).Name = "RenderSelection";
		((Control)this).Size = new Size(172, 329);
		((Control)this).ResumeLayout(false);
	}

	public RenderSelection()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		InitializeComponent();
		lb.DrawMode = (DrawMode)2;
		lb.DrawItem += new DrawItemEventHandler(DrawItemHandler);
		lb.MeasureItem += new MeasureItemEventHandler(MeasureItemHandler);
	}

	private void dx_ResetDevice(object sender, EventArgs e)
	{
		if (!(sender is DirectXPanel directXPanel))
		{
			return;
		}
		try
		{
			directXPanel.Meshes.Clear(dispose: true);
			if (lb.SelectedItem == null)
			{
				directXPanel.Meshes.AddRange(stm.ConvertToDx());
			}
			else if (!(lb.SelectedItem is Joint))
			{
				directXPanel.Meshes.AddRange(stm.ConvertToDx());
			}
			else if (lb.SelectedItems.Count == 1)
			{
				directXPanel.Meshes.AddRange(stm.ConvertToDx(lb.SelectedItem as Joint));
			}
			else
			{
				JointCollection jointCollection = new JointCollection();
				foreach (object selectedItem in lb.SelectedItems)
				{
					if (selectedItem is Joint)
					{
						jointCollection.Add(selectedItem as Joint);
					}
				}
				directXPanel.Meshes.AddRange(stm.ConvertToDx(jointCollection));
			}
			if (stm != null)
			{
			}
		}
		catch
		{
		}
	}

	private void SetContent()
	{
		lb.Items.Clear();
		stm = null;
		if (scn == null || dx == null)
		{
			return;
		}
		stm = new SceneToMesh(scn, dx);
		dx.Reset();
		dx.ResetDefaultViewport();
		lb.Items.Add((object)"--- [Display Mesh] ---");
		foreach (Joint item in scn.JointCollection)
		{
			lb.Items.Add((object)item);
		}
	}

	private void lb_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (dx != null)
		{
			dx.Reset();
		}
	}

	private void DrawItemHandler(object sender, DrawItemEventArgs e)
	{
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Expected O, but got Unknown
		//IL_020f: Expected O, but got Unknown
		e.DrawBackground();
		if (e.Index < lb.Items.Count && e.Index >= 0)
		{
			object obj = lb.Items[e.Index];
			Color foreColor = ((Control)lb).ForeColor;
			if (obj is Joint)
			{
				e.Graphics.CompositingQuality = (CompositingQuality)2;
				e.Graphics.SmoothingMode = (SmoothingMode)2;
				e.Graphics.InterpolationMode = (InterpolationMode)2;
				Joint j = obj as Joint;
				foreColor = stm.GetJointColor(j);
				Routines.FillRoundRect(e.Graphics, (Brush)new SolidBrush(foreColor), new Rectangle(e.Bounds.Left + 3, e.Bounds.Top + 2, e.Bounds.Width - 6, e.Bounds.Height - 5), e.Bounds.Height / 3);
				e.Graphics.FillEllipse((Brush)new SolidBrush(foreColor), new Rectangle(e.Bounds.Left + 1, e.Bounds.Top, e.Bounds.Height + 4, e.Bounds.Height - 1));
				e.Graphics.DrawString(obj.ToString(), new Font(((Control)lb).Font.FontFamily, ((Control)lb).Font.Size, (FontStyle)1, ((Control)lb).Font.Unit), (Brush)new SolidBrush(Color.Black), (RectangleF)new Rectangle(e.Bounds.Left + e.Bounds.Height + 4, e.Bounds.Top + 3, e.Bounds.Width - e.Bounds.Height - 5, e.Bounds.Height - 4));
			}
			else
			{
				e.Graphics.DrawString(obj.ToString(), ((Control)lb).Font, (Brush)new SolidBrush(foreColor), (RectangleF)new Rectangle(e.Bounds.Left + 3, e.Bounds.Top + 4, e.Bounds.Width - 6, e.Bounds.Height - 4));
			}
		}
	}

	private void MeasureItemHandler(object sender, MeasureItemEventArgs e)
	{
		e.ItemHeight += 8;
	}
}
