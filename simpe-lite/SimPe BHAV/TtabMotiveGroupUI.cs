/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   peter@drealm.info                                                     *
 *                                                                         *
 *   This program is free software; you can redistribute it and/or modify  *
 *   it under the terms of the GNU General Public License as published by  *
 *   the Free Software Foundation; either version 2 of the License, or     *
 *   (at your option) any later version.                                   *
 *                                                                         *
 *   This program is distributed in the hope that it will be useful,       *
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of        *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the         *
 *   GNU General Public License for more details.                          *
 *                                                                         *
 *   You should have received a copy of the GNU General Public License     *
 *   along with this program; if not, write to the                         *
 *   Free Software Foundation, Inc.,                                       *
 *   59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.             *
 ***************************************************************************/
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for TtabMotiveGroupUI.
	/// </summary>
	public class TtabMotiveGroupUI : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.GroupBox gbMotiveGroup;
		private System.Windows.Forms.Label Min;
		private System.Windows.Forms.Label Delta;
		private System.Windows.Forms.Label Type;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TtabMotiveGroupUI()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			TtabMotiveGroupUI_Resize(null, null);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		private TtabItem item = null;
		private int mgnr;
		public void SetData(TtabItem i, int j)
		{
			item = i;
			mgnr = j;

			gbMotiveGroup.Text = ((TtabMotives)mgnr).ToString();

			gbMotiveGroup.Controls.Clear();
			gbMotiveGroup.Controls.Add(Min);
			gbMotiveGroup.Controls.Add(Delta);
			gbMotiveGroup.Controls.Add(Type);

			int ts = 1;
			for (int k=0; k < item.nrMotives[mgnr]; k++)
			{
				TtabSingleMotiveUI sm = new TtabSingleMotiveUI();
				sm.TabIndex = ts++;
				sm.Left = 3;
				sm.SetData(item, mgnr, k);
				gbMotiveGroup.Controls.Add(sm);
			}
			TtabMotiveGroupUI_Resize(null, null);
		}


		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TtabMotiveGroupUI));
			this.gbMotiveGroup = new System.Windows.Forms.GroupBox();
			this.Min = new System.Windows.Forms.Label();
			this.Delta = new System.Windows.Forms.Label();
			this.Type = new System.Windows.Forms.Label();
			this.gbMotiveGroup.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbMotiveGroup
			// 
			this.gbMotiveGroup.AccessibleDescription = resources.GetString("gbMotiveGroup.AccessibleDescription");
			this.gbMotiveGroup.AccessibleName = resources.GetString("gbMotiveGroup.AccessibleName");
			this.gbMotiveGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gbMotiveGroup.Anchor")));
			this.gbMotiveGroup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gbMotiveGroup.BackgroundImage")));
			this.gbMotiveGroup.Controls.Add(this.Min);
			this.gbMotiveGroup.Controls.Add(this.Delta);
			this.gbMotiveGroup.Controls.Add(this.Type);
			this.gbMotiveGroup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("gbMotiveGroup.Dock")));
			this.gbMotiveGroup.Enabled = ((bool)(resources.GetObject("gbMotiveGroup.Enabled")));
			this.gbMotiveGroup.Font = ((System.Drawing.Font)(resources.GetObject("gbMotiveGroup.Font")));
			this.gbMotiveGroup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gbMotiveGroup.ImeMode")));
			this.gbMotiveGroup.Location = ((System.Drawing.Point)(resources.GetObject("gbMotiveGroup.Location")));
			this.gbMotiveGroup.Name = "gbMotiveGroup";
			this.gbMotiveGroup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("gbMotiveGroup.RightToLeft")));
			this.gbMotiveGroup.Size = ((System.Drawing.Size)(resources.GetObject("gbMotiveGroup.Size")));
			this.gbMotiveGroup.TabIndex = ((int)(resources.GetObject("gbMotiveGroup.TabIndex")));
			this.gbMotiveGroup.TabStop = false;
			this.gbMotiveGroup.Text = resources.GetString("gbMotiveGroup.Text");
			this.gbMotiveGroup.Visible = ((bool)(resources.GetObject("gbMotiveGroup.Visible")));
			// 
			// Min
			// 
			this.Min.AccessibleDescription = resources.GetString("Min.AccessibleDescription");
			this.Min.AccessibleName = resources.GetString("Min.AccessibleName");
			this.Min.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("Min.Anchor")));
			this.Min.AutoSize = ((bool)(resources.GetObject("Min.AutoSize")));
			this.Min.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("Min.Dock")));
			this.Min.Enabled = ((bool)(resources.GetObject("Min.Enabled")));
			this.Min.Font = ((System.Drawing.Font)(resources.GetObject("Min.Font")));
			this.Min.Image = ((System.Drawing.Image)(resources.GetObject("Min.Image")));
			this.Min.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("Min.ImageAlign")));
			this.Min.ImageIndex = ((int)(resources.GetObject("Min.ImageIndex")));
			this.Min.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("Min.ImeMode")));
			this.Min.Location = ((System.Drawing.Point)(resources.GetObject("Min.Location")));
			this.Min.Name = "Min";
			this.Min.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("Min.RightToLeft")));
			this.Min.Size = ((System.Drawing.Size)(resources.GetObject("Min.Size")));
			this.Min.TabIndex = ((int)(resources.GetObject("Min.TabIndex")));
			this.Min.Text = resources.GetString("Min.Text");
			this.Min.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("Min.TextAlign")));
			this.Min.Visible = ((bool)(resources.GetObject("Min.Visible")));
			// 
			// Delta
			// 
			this.Delta.AccessibleDescription = resources.GetString("Delta.AccessibleDescription");
			this.Delta.AccessibleName = resources.GetString("Delta.AccessibleName");
			this.Delta.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("Delta.Anchor")));
			this.Delta.AutoSize = ((bool)(resources.GetObject("Delta.AutoSize")));
			this.Delta.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("Delta.Dock")));
			this.Delta.Enabled = ((bool)(resources.GetObject("Delta.Enabled")));
			this.Delta.Font = ((System.Drawing.Font)(resources.GetObject("Delta.Font")));
			this.Delta.Image = ((System.Drawing.Image)(resources.GetObject("Delta.Image")));
			this.Delta.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("Delta.ImageAlign")));
			this.Delta.ImageIndex = ((int)(resources.GetObject("Delta.ImageIndex")));
			this.Delta.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("Delta.ImeMode")));
			this.Delta.Location = ((System.Drawing.Point)(resources.GetObject("Delta.Location")));
			this.Delta.Name = "Delta";
			this.Delta.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("Delta.RightToLeft")));
			this.Delta.Size = ((System.Drawing.Size)(resources.GetObject("Delta.Size")));
			this.Delta.TabIndex = ((int)(resources.GetObject("Delta.TabIndex")));
			this.Delta.Text = resources.GetString("Delta.Text");
			this.Delta.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("Delta.TextAlign")));
			this.Delta.Visible = ((bool)(resources.GetObject("Delta.Visible")));
			// 
			// Type
			// 
			this.Type.AccessibleDescription = resources.GetString("Type.AccessibleDescription");
			this.Type.AccessibleName = resources.GetString("Type.AccessibleName");
			this.Type.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("Type.Anchor")));
			this.Type.AutoSize = ((bool)(resources.GetObject("Type.AutoSize")));
			this.Type.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("Type.Dock")));
			this.Type.Enabled = ((bool)(resources.GetObject("Type.Enabled")));
			this.Type.Font = ((System.Drawing.Font)(resources.GetObject("Type.Font")));
			this.Type.Image = ((System.Drawing.Image)(resources.GetObject("Type.Image")));
			this.Type.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("Type.ImageAlign")));
			this.Type.ImageIndex = ((int)(resources.GetObject("Type.ImageIndex")));
			this.Type.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("Type.ImeMode")));
			this.Type.Location = ((System.Drawing.Point)(resources.GetObject("Type.Location")));
			this.Type.Name = "Type";
			this.Type.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("Type.RightToLeft")));
			this.Type.Size = ((System.Drawing.Size)(resources.GetObject("Type.Size")));
			this.Type.TabIndex = ((int)(resources.GetObject("Type.TabIndex")));
			this.Type.Text = resources.GetString("Type.Text");
			this.Type.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("Type.TextAlign")));
			this.Type.Visible = ((bool)(resources.GetObject("Type.Visible")));
			// 
			// TtabMotiveGroupUI
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Controls.Add(this.gbMotiveGroup);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "TtabMotiveGroupUI";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.Resize += new System.EventHandler(this.TtabMotiveGroupUI_Resize);
			this.gbMotiveGroup.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void TtabMotiveGroupUI_Resize(object sender, System.EventArgs e)
		{
			int w = gbMotiveGroup.ClientRectangle.Width;
			Min.Width = Delta.Width = Type.Width = (w / 6) * 2;
			Min.Left = (w / 6) * 1 - (Min.Width / 2);
			Delta.Left = (w / 6) * 3 - (Delta.Width / 2);
			Type.Left = (w / 6) * 5 - (Type.Width / 2);
			Min.Top = Delta.Top = Type.Top = 17;

			if (item == null) return;

			int nrRows = 1; // mgui header
			foreach (Control c in gbMotiveGroup.Controls) if (c is TtabSingleMotiveUI) nrRows++;

			int colHeight = gbMotiveGroup.Height - Min.Top;

			int lbTop = Min.Bottom + 1;
			int rowHeight = colHeight / nrRows;
			if (lbTop > rowHeight) lbTop = rowHeight;
			else rowHeight = (colHeight - lbTop) / (nrRows - 1);

			lbTop += 4;
			foreach (Control c in gbMotiveGroup.Controls)
				if (c is TtabSingleMotiveUI)
				{
					c.Top = lbTop;
					c.Left = 4;
					c.Size = new Size(w - 8, rowHeight - 2);
					lbTop += rowHeight;
				}
		}
	}
}
