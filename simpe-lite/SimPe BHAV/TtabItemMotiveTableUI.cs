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
	/// Summary description for TtabItemMotiveTableUI.
	/// </summary>
	public class TtabItemMotiveTableUI : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label label1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TtabItemMotiveTableUI()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			TtabItemMotiveTableUI_Resize(null, null);
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
		public void SetData(TtabItem i)
		{
			item = i;
			this.SuspendLayout();
			this.Controls.Clear();
			this.Controls.Add(label1);

			for (ushort j = 0; j < item.nrMotives[0]; j++)
			{
				Label lb = new Label();
				lb.Text = item.Opcodes.FindMotives(j);
				//lb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				lb.AutoSize = true;
				this.Controls.Add(lb);
			}

			for (int j = 0; j < item.nrGroups; j++)
			{
				TtabMotiveGroupUI mgui = new TtabMotiveGroupUI();
				mgui.SetData(item, j);
				this.Controls.Add(mgui);
			}
			this.ResumeLayout(false);
			TtabItemMotiveTableUI_Resize(null, null);
		}


		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TtabItemMotiveTableUI));
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AccessibleDescription = resources.GetString("label1.AccessibleDescription");
			this.label1.AccessibleName = resources.GetString("label1.AccessibleName");
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label1.Anchor")));
			this.label1.AutoSize = ((bool)(resources.GetObject("label1.AutoSize")));
			this.label1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label1.Dock")));
			this.label1.Enabled = ((bool)(resources.GetObject("label1.Enabled")));
			this.label1.Font = ((System.Drawing.Font)(resources.GetObject("label1.Font")));
			this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
			this.label1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.ImageAlign")));
			this.label1.ImageIndex = ((int)(resources.GetObject("label1.ImageIndex")));
			this.label1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label1.ImeMode")));
			this.label1.Location = ((System.Drawing.Point)(resources.GetObject("label1.Location")));
			this.label1.Name = "label1";
			this.label1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label1.RightToLeft")));
			this.label1.Size = ((System.Drawing.Size)(resources.GetObject("label1.Size")));
			this.label1.TabIndex = ((int)(resources.GetObject("label1.TabIndex")));
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.TextAlign")));
			this.label1.Visible = ((bool)(resources.GetObject("label1.Visible")));
			// 
			// TtabItemMotiveTableUI
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Controls.Add(this.label1);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "TtabItemMotiveTableUI";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.Resize += new System.EventHandler(this.TtabItemMotiveTableUI_Resize);
			this.ResumeLayout(false);

		}
		#endregion

		private void TtabItemMotiveTableUI_Resize(object sender, System.EventArgs e)
		{
			if (item == null) return;

			int lbMaxWidth = 0;
			int nrRows = 1; // mgui header
			int nrCols = 1; // motive labels
			foreach (Control c in this.Controls)
			{
				if (c == label1) continue;
				if (c is Label)
				{
					nrRows++;
					if (c.Width > lbMaxWidth) lbMaxWidth = c.Width;
				}
				if (c is TtabMotiveGroupUI) nrCols++;
			}
			lbMaxWidth += 4;

			int colWidth = this.Width / nrCols;
			if (lbMaxWidth > colWidth) lbMaxWidth = colWidth;
			else colWidth = (this.Width - lbMaxWidth) / (nrCols - 1);

			int lbTop = 17 + label1.Height; // for argument's sake
			int colHeight = this.Height - label1.Height - lbTop;
			int rowHeight = colHeight / nrRows;
			if (lbTop > 17 + rowHeight) lbTop = 17 + rowHeight;
			else rowHeight = (colHeight - lbTop) / (nrRows - 1);

			int mguiLeft = lbMaxWidth;
			lbTop += 4;
			foreach (Control c in this.Controls)
			{
				if (c == label1) continue;
				if (c is Label)
				{
					c.Top = lbTop;
					c.Left = lbMaxWidth - c.Width - 4;
					c.Size = new Size(lbMaxWidth, rowHeight);
					lbTop += rowHeight;
				}
				if (c is TtabMotiveGroupUI)
				{
					c.Top = 0;
					c.Left = mguiLeft;
					c.Size = new Size(colWidth - 4, colHeight);
					mguiLeft += colWidth;
				}
			}
		}
	}
}
