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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.PackedFiles.Wrapper;
using SimPe.Interfaces.Plugin;

namespace pjse
{
	/// <summary>
	/// Summary description for StrBig.
	/// </summary>
	public class Chooser : System.Windows.Forms.Form
	{
		#region Form variables
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button OK;
		private System.Windows.Forms.Button Cancel;
		private System.Windows.Forms.ListBox lbFileList;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public Chooser()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

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


		#region Chooser
		public uint Instance(AbstractWrapper wrapper)
		{
			fill(wrapper);

			DialogResult dr = ShowDialog();
			Close();

			switch (dr)
			{
				case DialogResult.OK:
					if (lbFileList.SelectedIndex >= 0) return ((SimPe.Data.Alias)lbFileList.SelectedItem).Id;
					return 0xffffffff;
				default:
					return 0xffffffff;
			}
		}

		private void fill(AbstractWrapper wrapper)
		{
			this.lbFileList.Items.Clear();

			if (wrapper == null || wrapper.Package == null || wrapper.FileDescriptor == null) return;

			AbstractWrapper wrp = null;
			if (wrapper is Ttab)
			{
				if (((Ttab)wrapper).Opcodes == null) return;
				wrp = new Ttab(((Ttab)wrapper).Opcodes);
			}
			else if (wrapper is Str)
			{
				wrp = new Str();
			}

			SimPe.Interfaces.Files.IPackedFileDescriptor[] pfds = wrapper.Package.FindFiles(wrapper.FileDescriptor.Type);
			foreach (SimPe.Interfaces.Files.IPackedFileDescriptor pfd in pfds) 
			{
				if (pfd.Group == wrapper.FileDescriptor.Group)
				{
					wrp.ProcessData(pfd, wrapper.Package);
					lbFileList.Items.Add(new SimPe.Data.Alias(wrp.FileDescriptor.Instance, wrp.ResourceName));
				}
			}
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Chooser));
			this.panel1 = new System.Windows.Forms.Panel();
			this.OK = new System.Windows.Forms.Button();
			this.Cancel = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lbFileList = new System.Windows.Forms.ListBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.AccessibleDescription = resources.GetString("panel1.AccessibleDescription");
			this.panel1.AccessibleName = resources.GetString("panel1.AccessibleName");
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel1.Anchor")));
			this.panel1.AutoScroll = ((bool)(resources.GetObject("panel1.AutoScroll")));
			this.panel1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel1.AutoScrollMargin")));
			this.panel1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel1.AutoScrollMinSize")));
			this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
			this.panel1.Controls.Add(this.OK);
			this.panel1.Controls.Add(this.Cancel);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panel1.Dock")));
			this.panel1.Enabled = ((bool)(resources.GetObject("panel1.Enabled")));
			this.panel1.Font = ((System.Drawing.Font)(resources.GetObject("panel1.Font")));
			this.panel1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panel1.ImeMode")));
			this.panel1.Location = ((System.Drawing.Point)(resources.GetObject("panel1.Location")));
			this.panel1.Name = "panel1";
			this.panel1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panel1.RightToLeft")));
			this.panel1.Size = ((System.Drawing.Size)(resources.GetObject("panel1.Size")));
			this.panel1.TabIndex = ((int)(resources.GetObject("panel1.TabIndex")));
			this.panel1.Text = resources.GetString("panel1.Text");
			this.panel1.Visible = ((bool)(resources.GetObject("panel1.Visible")));
			// 
			// OK
			// 
			this.OK.AccessibleDescription = resources.GetString("OK.AccessibleDescription");
			this.OK.AccessibleName = resources.GetString("OK.AccessibleName");
			this.OK.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("OK.Anchor")));
			this.OK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("OK.BackgroundImage")));
			this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OK.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("OK.Dock")));
			this.OK.Enabled = ((bool)(resources.GetObject("OK.Enabled")));
			this.OK.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("OK.FlatStyle")));
			this.OK.Font = ((System.Drawing.Font)(resources.GetObject("OK.Font")));
			this.OK.Image = ((System.Drawing.Image)(resources.GetObject("OK.Image")));
			this.OK.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("OK.ImageAlign")));
			this.OK.ImageIndex = ((int)(resources.GetObject("OK.ImageIndex")));
			this.OK.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("OK.ImeMode")));
			this.OK.Location = ((System.Drawing.Point)(resources.GetObject("OK.Location")));
			this.OK.Name = "OK";
			this.OK.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("OK.RightToLeft")));
			this.OK.Size = ((System.Drawing.Size)(resources.GetObject("OK.Size")));
			this.OK.TabIndex = ((int)(resources.GetObject("OK.TabIndex")));
			this.OK.Text = resources.GetString("OK.Text");
			this.OK.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("OK.TextAlign")));
			this.OK.Visible = ((bool)(resources.GetObject("OK.Visible")));
			// 
			// Cancel
			// 
			this.Cancel.AccessibleDescription = resources.GetString("Cancel.AccessibleDescription");
			this.Cancel.AccessibleName = resources.GetString("Cancel.AccessibleName");
			this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("Cancel.Anchor")));
			this.Cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cancel.BackgroundImage")));
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Ignore;
			this.Cancel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("Cancel.Dock")));
			this.Cancel.Enabled = ((bool)(resources.GetObject("Cancel.Enabled")));
			this.Cancel.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("Cancel.FlatStyle")));
			this.Cancel.Font = ((System.Drawing.Font)(resources.GetObject("Cancel.Font")));
			this.Cancel.Image = ((System.Drawing.Image)(resources.GetObject("Cancel.Image")));
			this.Cancel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("Cancel.ImageAlign")));
			this.Cancel.ImageIndex = ((int)(resources.GetObject("Cancel.ImageIndex")));
			this.Cancel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("Cancel.ImeMode")));
			this.Cancel.Location = ((System.Drawing.Point)(resources.GetObject("Cancel.Location")));
			this.Cancel.Name = "Cancel";
			this.Cancel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("Cancel.RightToLeft")));
			this.Cancel.Size = ((System.Drawing.Size)(resources.GetObject("Cancel.Size")));
			this.Cancel.TabIndex = ((int)(resources.GetObject("Cancel.TabIndex")));
			this.Cancel.Text = resources.GetString("Cancel.Text");
			this.Cancel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("Cancel.TextAlign")));
			this.Cancel.Visible = ((bool)(resources.GetObject("Cancel.Visible")));
			// 
			// panel2
			// 
			this.panel2.AccessibleDescription = resources.GetString("panel2.AccessibleDescription");
			this.panel2.AccessibleName = resources.GetString("panel2.AccessibleName");
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel2.Anchor")));
			this.panel2.AutoScroll = ((bool)(resources.GetObject("panel2.AutoScroll")));
			this.panel2.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel2.AutoScrollMargin")));
			this.panel2.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel2.AutoScrollMinSize")));
			this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panel2.Dock")));
			this.panel2.Enabled = ((bool)(resources.GetObject("panel2.Enabled")));
			this.panel2.Font = ((System.Drawing.Font)(resources.GetObject("panel2.Font")));
			this.panel2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panel2.ImeMode")));
			this.panel2.Location = ((System.Drawing.Point)(resources.GetObject("panel2.Location")));
			this.panel2.Name = "panel2";
			this.panel2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panel2.RightToLeft")));
			this.panel2.Size = ((System.Drawing.Size)(resources.GetObject("panel2.Size")));
			this.panel2.TabIndex = ((int)(resources.GetObject("panel2.TabIndex")));
			this.panel2.Text = resources.GetString("panel2.Text");
			this.panel2.Visible = ((bool)(resources.GetObject("panel2.Visible")));
			// 
			// lbFileList
			// 
			this.lbFileList.AccessibleDescription = resources.GetString("lbFileList.AccessibleDescription");
			this.lbFileList.AccessibleName = resources.GetString("lbFileList.AccessibleName");
			this.lbFileList.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbFileList.Anchor")));
			this.lbFileList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lbFileList.BackgroundImage")));
			this.lbFileList.ColumnWidth = ((int)(resources.GetObject("lbFileList.ColumnWidth")));
			this.lbFileList.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbFileList.Dock")));
			this.lbFileList.Enabled = ((bool)(resources.GetObject("lbFileList.Enabled")));
			this.lbFileList.Font = ((System.Drawing.Font)(resources.GetObject("lbFileList.Font")));
			this.lbFileList.HorizontalExtent = ((int)(resources.GetObject("lbFileList.HorizontalExtent")));
			this.lbFileList.HorizontalScrollbar = ((bool)(resources.GetObject("lbFileList.HorizontalScrollbar")));
			this.lbFileList.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbFileList.ImeMode")));
			this.lbFileList.IntegralHeight = ((bool)(resources.GetObject("lbFileList.IntegralHeight")));
			this.lbFileList.ItemHeight = ((int)(resources.GetObject("lbFileList.ItemHeight")));
			this.lbFileList.Location = ((System.Drawing.Point)(resources.GetObject("lbFileList.Location")));
			this.lbFileList.Name = "lbFileList";
			this.lbFileList.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbFileList.RightToLeft")));
			this.lbFileList.ScrollAlwaysVisible = ((bool)(resources.GetObject("lbFileList.ScrollAlwaysVisible")));
			this.lbFileList.Size = ((System.Drawing.Size)(resources.GetObject("lbFileList.Size")));
			this.lbFileList.TabIndex = ((int)(resources.GetObject("lbFileList.TabIndex")));
			this.lbFileList.Visible = ((bool)(resources.GetObject("lbFileList.Visible")));
			this.lbFileList.DoubleClick += new System.EventHandler(this.lbFileList_DoubleClick);
			// 
			// Chooser
			// 
			this.AcceptButton = this.OK;
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.CancelButton = this.Cancel;
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.lbFileList);
			this.Controls.Add(this.panel1);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "Chooser";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.ShowInTaskbar = false;
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void lbFileList_DoubleClick(object sender, System.EventArgs e)
		{
			if (lbFileList.SelectedIndex >= 0)
			{
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}
	}
}
