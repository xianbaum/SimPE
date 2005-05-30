/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   peter@drealm.info                                                     *
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
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
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.PackedFiles.UserInterface 
{
	/// <summary>
	/// Zusammenfassung für Elements2.
	/// </summary>
	public class NrefForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox tbnref;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox tbnrefhash;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Panel nrefPanel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public NrefForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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


		#region NrefForm
		private Nref wrapper;
		#endregion


		#region IPackedFileUI Member
		/// <summary>
		/// Returns the Panel that will be displayed within SimPe
		/// </summary>
		public System.Windows.Forms.Panel GUIHandle
		{
			get
			{
				return nrefPanel;
			}
		}

		/// <summary>
		/// Is called by SimPe (through the Wrapper) when the Panel is going to be displayed, so
		/// you should updatet the Data displayed by the Panel with the Attributes stored in the
		/// passed Wrapper.
		/// </summary>
		/// <param name="wrp">The Attributes of this Wrapper have to be displayed</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (Nref) wrp;

			tbnref.Tag = true;
			tbnref.Text = wrapper.FileName;
			tbnref.Tag = null;
		}		

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NrefForm));
			this.nrefPanel = new System.Windows.Forms.Panel();
			this.label10 = new System.Windows.Forms.Label();
			this.tbnrefhash = new System.Windows.Forms.TextBox();
			this.tbnref = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.panel4 = new System.Windows.Forms.Panel();
			this.label12 = new System.Windows.Forms.Label();
			this.nrefPanel.SuspendLayout();
			this.panel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// nrefPanel
			// 
			this.nrefPanel.AccessibleDescription = resources.GetString("nrefPanel.AccessibleDescription");
			this.nrefPanel.AccessibleName = resources.GetString("nrefPanel.AccessibleName");
			this.nrefPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("nrefPanel.Anchor")));
			this.nrefPanel.AutoScroll = ((bool)(resources.GetObject("nrefPanel.AutoScroll")));
			this.nrefPanel.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("nrefPanel.AutoScrollMargin")));
			this.nrefPanel.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("nrefPanel.AutoScrollMinSize")));
			this.nrefPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nrefPanel.BackgroundImage")));
			this.nrefPanel.Controls.Add(this.label10);
			this.nrefPanel.Controls.Add(this.tbnrefhash);
			this.nrefPanel.Controls.Add(this.tbnref);
			this.nrefPanel.Controls.Add(this.label9);
			this.nrefPanel.Controls.Add(this.button2);
			this.nrefPanel.Controls.Add(this.panel4);
			this.nrefPanel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("nrefPanel.Dock")));
			this.nrefPanel.Enabled = ((bool)(resources.GetObject("nrefPanel.Enabled")));
			this.nrefPanel.Font = ((System.Drawing.Font)(resources.GetObject("nrefPanel.Font")));
			this.nrefPanel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("nrefPanel.ImeMode")));
			this.nrefPanel.Location = ((System.Drawing.Point)(resources.GetObject("nrefPanel.Location")));
			this.nrefPanel.Name = "nrefPanel";
			this.nrefPanel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("nrefPanel.RightToLeft")));
			this.nrefPanel.Size = ((System.Drawing.Size)(resources.GetObject("nrefPanel.Size")));
			this.nrefPanel.TabIndex = ((int)(resources.GetObject("nrefPanel.TabIndex")));
			this.nrefPanel.Text = resources.GetString("nrefPanel.Text");
			this.nrefPanel.Visible = ((bool)(resources.GetObject("nrefPanel.Visible")));
			// 
			// label10
			// 
			this.label10.AccessibleDescription = resources.GetString("label10.AccessibleDescription");
			this.label10.AccessibleName = resources.GetString("label10.AccessibleName");
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label10.Anchor")));
			this.label10.AutoSize = ((bool)(resources.GetObject("label10.AutoSize")));
			this.label10.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label10.Dock")));
			this.label10.Enabled = ((bool)(resources.GetObject("label10.Enabled")));
			this.label10.Font = ((System.Drawing.Font)(resources.GetObject("label10.Font")));
			this.label10.Image = ((System.Drawing.Image)(resources.GetObject("label10.Image")));
			this.label10.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label10.ImageAlign")));
			this.label10.ImageIndex = ((int)(resources.GetObject("label10.ImageIndex")));
			this.label10.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label10.ImeMode")));
			this.label10.Location = ((System.Drawing.Point)(resources.GetObject("label10.Location")));
			this.label10.Name = "label10";
			this.label10.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label10.RightToLeft")));
			this.label10.Size = ((System.Drawing.Size)(resources.GetObject("label10.Size")));
			this.label10.TabIndex = ((int)(resources.GetObject("label10.TabIndex")));
			this.label10.Text = resources.GetString("label10.Text");
			this.label10.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label10.TextAlign")));
			this.label10.Visible = ((bool)(resources.GetObject("label10.Visible")));
			// 
			// tbnrefhash
			// 
			this.tbnrefhash.AccessibleDescription = resources.GetString("tbnrefhash.AccessibleDescription");
			this.tbnrefhash.AccessibleName = resources.GetString("tbnrefhash.AccessibleName");
			this.tbnrefhash.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbnrefhash.Anchor")));
			this.tbnrefhash.AutoSize = ((bool)(resources.GetObject("tbnrefhash.AutoSize")));
			this.tbnrefhash.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbnrefhash.BackgroundImage")));
			this.tbnrefhash.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbnrefhash.Dock")));
			this.tbnrefhash.Enabled = ((bool)(resources.GetObject("tbnrefhash.Enabled")));
			this.tbnrefhash.Font = ((System.Drawing.Font)(resources.GetObject("tbnrefhash.Font")));
			this.tbnrefhash.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbnrefhash.ImeMode")));
			this.tbnrefhash.Location = ((System.Drawing.Point)(resources.GetObject("tbnrefhash.Location")));
			this.tbnrefhash.MaxLength = ((int)(resources.GetObject("tbnrefhash.MaxLength")));
			this.tbnrefhash.Multiline = ((bool)(resources.GetObject("tbnrefhash.Multiline")));
			this.tbnrefhash.Name = "tbnrefhash";
			this.tbnrefhash.PasswordChar = ((char)(resources.GetObject("tbnrefhash.PasswordChar")));
			this.tbnrefhash.ReadOnly = true;
			this.tbnrefhash.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbnrefhash.RightToLeft")));
			this.tbnrefhash.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbnrefhash.ScrollBars")));
			this.tbnrefhash.Size = ((System.Drawing.Size)(resources.GetObject("tbnrefhash.Size")));
			this.tbnrefhash.TabIndex = ((int)(resources.GetObject("tbnrefhash.TabIndex")));
			this.tbnrefhash.Text = resources.GetString("tbnrefhash.Text");
			this.tbnrefhash.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbnrefhash.TextAlign")));
			this.tbnrefhash.Visible = ((bool)(resources.GetObject("tbnrefhash.Visible")));
			this.tbnrefhash.WordWrap = ((bool)(resources.GetObject("tbnrefhash.WordWrap")));
			// 
			// tbnref
			// 
			this.tbnref.AccessibleDescription = resources.GetString("tbnref.AccessibleDescription");
			this.tbnref.AccessibleName = resources.GetString("tbnref.AccessibleName");
			this.tbnref.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbnref.Anchor")));
			this.tbnref.AutoSize = ((bool)(resources.GetObject("tbnref.AutoSize")));
			this.tbnref.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbnref.BackgroundImage")));
			this.tbnref.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbnref.Dock")));
			this.tbnref.Enabled = ((bool)(resources.GetObject("tbnref.Enabled")));
			this.tbnref.Font = ((System.Drawing.Font)(resources.GetObject("tbnref.Font")));
			this.tbnref.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbnref.ImeMode")));
			this.tbnref.Location = ((System.Drawing.Point)(resources.GetObject("tbnref.Location")));
			this.tbnref.MaxLength = ((int)(resources.GetObject("tbnref.MaxLength")));
			this.tbnref.Multiline = ((bool)(resources.GetObject("tbnref.Multiline")));
			this.tbnref.Name = "tbnref";
			this.tbnref.PasswordChar = ((char)(resources.GetObject("tbnref.PasswordChar")));
			this.tbnref.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbnref.RightToLeft")));
			this.tbnref.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbnref.ScrollBars")));
			this.tbnref.Size = ((System.Drawing.Size)(resources.GetObject("tbnref.Size")));
			this.tbnref.TabIndex = ((int)(resources.GetObject("tbnref.TabIndex")));
			this.tbnref.Text = resources.GetString("tbnref.Text");
			this.tbnref.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbnref.TextAlign")));
			this.tbnref.Visible = ((bool)(resources.GetObject("tbnref.Visible")));
			this.tbnref.WordWrap = ((bool)(resources.GetObject("tbnref.WordWrap")));
			this.tbnref.TextChanged += new System.EventHandler(this.tbnref_TextChanged);
			// 
			// label9
			// 
			this.label9.AccessibleDescription = resources.GetString("label9.AccessibleDescription");
			this.label9.AccessibleName = resources.GetString("label9.AccessibleName");
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label9.Anchor")));
			this.label9.AutoSize = ((bool)(resources.GetObject("label9.AutoSize")));
			this.label9.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label9.Dock")));
			this.label9.Enabled = ((bool)(resources.GetObject("label9.Enabled")));
			this.label9.Font = ((System.Drawing.Font)(resources.GetObject("label9.Font")));
			this.label9.Image = ((System.Drawing.Image)(resources.GetObject("label9.Image")));
			this.label9.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label9.ImageAlign")));
			this.label9.ImageIndex = ((int)(resources.GetObject("label9.ImageIndex")));
			this.label9.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label9.ImeMode")));
			this.label9.Location = ((System.Drawing.Point)(resources.GetObject("label9.Location")));
			this.label9.Name = "label9";
			this.label9.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label9.RightToLeft")));
			this.label9.Size = ((System.Drawing.Size)(resources.GetObject("label9.Size")));
			this.label9.TabIndex = ((int)(resources.GetObject("label9.TabIndex")));
			this.label9.Text = resources.GetString("label9.Text");
			this.label9.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label9.TextAlign")));
			this.label9.Visible = ((bool)(resources.GetObject("label9.Visible")));
			// 
			// button2
			// 
			this.button2.AccessibleDescription = resources.GetString("button2.AccessibleDescription");
			this.button2.AccessibleName = resources.GetString("button2.AccessibleName");
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("button2.Anchor")));
			this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
			this.button2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("button2.Dock")));
			this.button2.Enabled = ((bool)(resources.GetObject("button2.Enabled")));
			this.button2.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("button2.FlatStyle")));
			this.button2.Font = ((System.Drawing.Font)(resources.GetObject("button2.Font")));
			this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
			this.button2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("button2.ImageAlign")));
			this.button2.ImageIndex = ((int)(resources.GetObject("button2.ImageIndex")));
			this.button2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("button2.ImeMode")));
			this.button2.Location = ((System.Drawing.Point)(resources.GetObject("button2.Location")));
			this.button2.Name = "button2";
			this.button2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("button2.RightToLeft")));
			this.button2.Size = ((System.Drawing.Size)(resources.GetObject("button2.Size")));
			this.button2.TabIndex = ((int)(resources.GetObject("button2.TabIndex")));
			this.button2.Text = resources.GetString("button2.Text");
			this.button2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("button2.TextAlign")));
			this.button2.Visible = ((bool)(resources.GetObject("button2.Visible")));
			this.button2.Click += new System.EventHandler(this.NrefCommit);
			// 
			// panel4
			// 
			this.panel4.AccessibleDescription = resources.GetString("panel4.AccessibleDescription");
			this.panel4.AccessibleName = resources.GetString("panel4.AccessibleName");
			this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel4.Anchor")));
			this.panel4.AutoScroll = ((bool)(resources.GetObject("panel4.AutoScroll")));
			this.panel4.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel4.AutoScrollMargin")));
			this.panel4.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel4.AutoScrollMinSize")));
			this.panel4.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
			this.panel4.Controls.Add(this.label12);
			this.panel4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panel4.Dock")));
			this.panel4.Enabled = ((bool)(resources.GetObject("panel4.Enabled")));
			this.panel4.Font = ((System.Drawing.Font)(resources.GetObject("panel4.Font")));
			this.panel4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.panel4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panel4.ImeMode")));
			this.panel4.Location = ((System.Drawing.Point)(resources.GetObject("panel4.Location")));
			this.panel4.Name = "panel4";
			this.panel4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panel4.RightToLeft")));
			this.panel4.Size = ((System.Drawing.Size)(resources.GetObject("panel4.Size")));
			this.panel4.TabIndex = ((int)(resources.GetObject("panel4.TabIndex")));
			this.panel4.Text = resources.GetString("panel4.Text");
			this.panel4.Visible = ((bool)(resources.GetObject("panel4.Visible")));
			// 
			// label12
			// 
			this.label12.AccessibleDescription = resources.GetString("label12.AccessibleDescription");
			this.label12.AccessibleName = resources.GetString("label12.AccessibleName");
			this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label12.Anchor")));
			this.label12.AutoSize = ((bool)(resources.GetObject("label12.AutoSize")));
			this.label12.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label12.Dock")));
			this.label12.Enabled = ((bool)(resources.GetObject("label12.Enabled")));
			this.label12.Font = ((System.Drawing.Font)(resources.GetObject("label12.Font")));
			this.label12.Image = ((System.Drawing.Image)(resources.GetObject("label12.Image")));
			this.label12.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label12.ImageAlign")));
			this.label12.ImageIndex = ((int)(resources.GetObject("label12.ImageIndex")));
			this.label12.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label12.ImeMode")));
			this.label12.Location = ((System.Drawing.Point)(resources.GetObject("label12.Location")));
			this.label12.Name = "label12";
			this.label12.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label12.RightToLeft")));
			this.label12.Size = ((System.Drawing.Size)(resources.GetObject("label12.Size")));
			this.label12.TabIndex = ((int)(resources.GetObject("label12.TabIndex")));
			this.label12.Text = resources.GetString("label12.Text");
			this.label12.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label12.TextAlign")));
			this.label12.Visible = ((bool)(resources.GetObject("label12.Visible")));
			// 
			// NrefForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.nrefPanel);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "NrefForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.nrefPanel.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void tbnref_TextChanged(object sender, System.EventArgs e)
		{
			try 
			{
				tbnrefhash.Text = "0x"+Helper.HexString(wrapper.Group);

				if (tbnref.Tag!=null) return;
				wrapper.FileName = tbnref.Text;
				wrapper.Changed = true;
			} 
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			}
		}

		private void NrefCommit(object sender, System.EventArgs e)
		{
			try 
			{
				wrapper.SynchronizeUserData();
				MessageBox.Show(Localization.Manager.GetString("commited"));
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}
		}

	}
}
