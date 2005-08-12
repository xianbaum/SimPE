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
using SimPe.Plugin;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Zusammenfassung für TrcnForm.
	/// </summary>
	public class TrcnForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables

		private System.Windows.Forms.GroupBox gbprop;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel trcnPanel;
		private System.Windows.Forms.TextBox tbConstant;
		private System.Windows.Forms.TextBox tbLabel;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.TextBox tbFilename;
		private System.Windows.Forms.Label lbFilename;
		private System.Windows.Forms.Label lbFormat;
		private System.Windows.Forms.TextBox tbFormat;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.ListBox lbxLabels;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnAdd;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public TrcnForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			Control[] af = { tbFormat, tbConstant };
			alHex32 = new ArrayList(af);

			Control[] at = { tbFilename, tbLabel };
			alTextBox = new ArrayList(at);
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


		#region TrcnForm
		private Trcn wrapper;
		private bool setHandler = false;
		private bool internalchg;
		private ArrayList alHex32 = null;
		private ArrayList alTextBox = null;
		private TrcnItem currentItem = null;

		private bool hex32_IsValid(object sender)
		{
			if (alHex32.IndexOf(sender) < 0)
				throw new Exception("hex32_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToUInt32(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		#endregion

		#region IPackedFileUI Member
		/// <summary>
		/// Returns the Panel that will be displayed within SimPe
		/// </summary>
		public System.Windows.Forms.Control GUIHandle
		{
			get
			{
				return trcnPanel;
			}
		}

		/// <summary>
		/// Is called by SimPe (through the Wrapper) when the Panel is going to be displayed, so
		/// you should updatet the Data displayed by the Panel with the Attributes stored in the
		/// passed Wrapper.
		/// </summary>
		/// <remarks>attr.Tag is used to let TextChanged event handlers know the change is being
		/// made internally rather than by the users.</remarks>
		/// <param name="wrp">The Attributes of this Wrapper have to be displayed</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (Trcn)wrp;
			this.WrapperChanged(wrapper, null);

			internalchg = true;

			lbxLabels.Items.Clear();
			lbxLabels.Sorted = false;
			for (int i = 0; i < wrapper.Count; i++)
				lbxLabels.Items.Add("0x" + Helper.HexString(wrapper[i].Constant) + ": " + wrapper[i].Label);
			lbxLabels.Sorted = true;

			currentItem = null;
			this.tbConstant.Text = "";
			this.tbLabel.Text = "";

			internalchg = false;

			if (lbxLabels.Items.Count > 0)
				lbxLabels.SelectedIndex = 0;

			if (!setHandler)
			{
				wrapper.WrapperChanged += new System.EventHandler(this.WrapperChanged);
				setHandler = true;
			}
		}		

		private void WrapperChanged(object sender, System.EventArgs e)
		{
			this.btnCommit.Enabled = wrapper.Changed;

			if (internalchg) return;
			internalchg = true;
			this.tbFilename.Text = wrapper.FileName;
			this.tbFormat.Text = "0x"+Helper.HexString(wrapper.Format);
			internalchg = false;
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TrcnForm));
			this.gbprop = new System.Windows.Forms.GroupBox();
			this.tbConstant = new System.Windows.Forms.TextBox();
			this.tbLabel = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lbxLabels = new System.Windows.Forms.ListBox();
			this.trcnPanel = new System.Windows.Forms.Panel();
			this.label27 = new System.Windows.Forms.Label();
			this.tbFilename = new System.Windows.Forms.TextBox();
			this.lbFilename = new System.Windows.Forms.Label();
			this.lbFormat = new System.Windows.Forms.Label();
			this.tbFormat = new System.Windows.Forms.TextBox();
			this.btnCommit = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.gbprop.SuspendLayout();
			this.trcnPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbprop
			// 
			this.gbprop.AccessibleDescription = resources.GetString("gbprop.AccessibleDescription");
			this.gbprop.AccessibleName = resources.GetString("gbprop.AccessibleName");
			this.gbprop.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gbprop.Anchor")));
			this.gbprop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gbprop.BackgroundImage")));
			this.gbprop.Controls.Add(this.tbConstant);
			this.gbprop.Controls.Add(this.tbLabel);
			this.gbprop.Controls.Add(this.label2);
			this.gbprop.Controls.Add(this.label1);
			this.gbprop.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("gbprop.Dock")));
			this.gbprop.Enabled = ((bool)(resources.GetObject("gbprop.Enabled")));
			this.gbprop.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.gbprop.Font = ((System.Drawing.Font)(resources.GetObject("gbprop.Font")));
			this.gbprop.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gbprop.ImeMode")));
			this.gbprop.Location = ((System.Drawing.Point)(resources.GetObject("gbprop.Location")));
			this.gbprop.Name = "gbprop";
			this.gbprop.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("gbprop.RightToLeft")));
			this.gbprop.Size = ((System.Drawing.Size)(resources.GetObject("gbprop.Size")));
			this.gbprop.TabIndex = ((int)(resources.GetObject("gbprop.TabIndex")));
			this.gbprop.TabStop = false;
			this.gbprop.Text = resources.GetString("gbprop.Text");
			this.gbprop.Visible = ((bool)(resources.GetObject("gbprop.Visible")));
			// 
			// tbConstant
			// 
			this.tbConstant.AccessibleDescription = resources.GetString("tbConstant.AccessibleDescription");
			this.tbConstant.AccessibleName = resources.GetString("tbConstant.AccessibleName");
			this.tbConstant.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbConstant.Anchor")));
			this.tbConstant.AutoSize = ((bool)(resources.GetObject("tbConstant.AutoSize")));
			this.tbConstant.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbConstant.BackgroundImage")));
			this.tbConstant.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbConstant.Dock")));
			this.tbConstant.Enabled = ((bool)(resources.GetObject("tbConstant.Enabled")));
			this.tbConstant.Font = ((System.Drawing.Font)(resources.GetObject("tbConstant.Font")));
			this.tbConstant.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbConstant.ImeMode")));
			this.tbConstant.Location = ((System.Drawing.Point)(resources.GetObject("tbConstant.Location")));
			this.tbConstant.MaxLength = ((int)(resources.GetObject("tbConstant.MaxLength")));
			this.tbConstant.Multiline = ((bool)(resources.GetObject("tbConstant.Multiline")));
			this.tbConstant.Name = "tbConstant";
			this.tbConstant.PasswordChar = ((char)(resources.GetObject("tbConstant.PasswordChar")));
			this.tbConstant.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbConstant.RightToLeft")));
			this.tbConstant.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbConstant.ScrollBars")));
			this.tbConstant.Size = ((System.Drawing.Size)(resources.GetObject("tbConstant.Size")));
			this.tbConstant.TabIndex = ((int)(resources.GetObject("tbConstant.TabIndex")));
			this.tbConstant.Text = resources.GetString("tbConstant.Text");
			this.tbConstant.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbConstant.TextAlign")));
			this.tbConstant.Visible = ((bool)(resources.GetObject("tbConstant.Visible")));
			this.tbConstant.WordWrap = ((bool)(resources.GetObject("tbConstant.WordWrap")));
			this.tbConstant.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
			this.tbConstant.Validated += new System.EventHandler(this.hex32_Validated);
			this.tbConstant.TextChanged += new System.EventHandler(this.hex32_TextChanged);
			// 
			// tbLabel
			// 
			this.tbLabel.AccessibleDescription = resources.GetString("tbLabel.AccessibleDescription");
			this.tbLabel.AccessibleName = resources.GetString("tbLabel.AccessibleName");
			this.tbLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbLabel.Anchor")));
			this.tbLabel.AutoSize = ((bool)(resources.GetObject("tbLabel.AutoSize")));
			this.tbLabel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbLabel.BackgroundImage")));
			this.tbLabel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbLabel.Dock")));
			this.tbLabel.Enabled = ((bool)(resources.GetObject("tbLabel.Enabled")));
			this.tbLabel.Font = ((System.Drawing.Font)(resources.GetObject("tbLabel.Font")));
			this.tbLabel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbLabel.ImeMode")));
			this.tbLabel.Location = ((System.Drawing.Point)(resources.GetObject("tbLabel.Location")));
			this.tbLabel.MaxLength = ((int)(resources.GetObject("tbLabel.MaxLength")));
			this.tbLabel.Multiline = ((bool)(resources.GetObject("tbLabel.Multiline")));
			this.tbLabel.Name = "tbLabel";
			this.tbLabel.PasswordChar = ((char)(resources.GetObject("tbLabel.PasswordChar")));
			this.tbLabel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbLabel.RightToLeft")));
			this.tbLabel.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbLabel.ScrollBars")));
			this.tbLabel.Size = ((System.Drawing.Size)(resources.GetObject("tbLabel.Size")));
			this.tbLabel.TabIndex = ((int)(resources.GetObject("tbLabel.TabIndex")));
			this.tbLabel.Text = resources.GetString("tbLabel.Text");
			this.tbLabel.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbLabel.TextAlign")));
			this.tbLabel.Visible = ((bool)(resources.GetObject("tbLabel.Visible")));
			this.tbLabel.WordWrap = ((bool)(resources.GetObject("tbLabel.WordWrap")));
			this.tbLabel.Validated += new System.EventHandler(this.textBox_Validated);
			this.tbLabel.TextChanged += new System.EventHandler(this.textBox_TextChanged);
			// 
			// label2
			// 
			this.label2.AccessibleDescription = resources.GetString("label2.AccessibleDescription");
			this.label2.AccessibleName = resources.GetString("label2.AccessibleName");
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label2.Anchor")));
			this.label2.AutoSize = ((bool)(resources.GetObject("label2.AutoSize")));
			this.label2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label2.Dock")));
			this.label2.Enabled = ((bool)(resources.GetObject("label2.Enabled")));
			this.label2.Font = ((System.Drawing.Font)(resources.GetObject("label2.Font")));
			this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
			this.label2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.ImageAlign")));
			this.label2.ImageIndex = ((int)(resources.GetObject("label2.ImageIndex")));
			this.label2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label2.ImeMode")));
			this.label2.Location = ((System.Drawing.Point)(resources.GetObject("label2.Location")));
			this.label2.Name = "label2";
			this.label2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label2.RightToLeft")));
			this.label2.Size = ((System.Drawing.Size)(resources.GetObject("label2.Size")));
			this.label2.TabIndex = ((int)(resources.GetObject("label2.TabIndex")));
			this.label2.Text = resources.GetString("label2.Text");
			this.label2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.TextAlign")));
			this.label2.Visible = ((bool)(resources.GetObject("label2.Visible")));
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
			// lbxLabels
			// 
			this.lbxLabels.AccessibleDescription = resources.GetString("lbxLabels.AccessibleDescription");
			this.lbxLabels.AccessibleName = resources.GetString("lbxLabels.AccessibleName");
			this.lbxLabels.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbxLabels.Anchor")));
			this.lbxLabels.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lbxLabels.BackgroundImage")));
			this.lbxLabels.ColumnWidth = ((int)(resources.GetObject("lbxLabels.ColumnWidth")));
			this.lbxLabels.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbxLabels.Dock")));
			this.lbxLabels.Enabled = ((bool)(resources.GetObject("lbxLabels.Enabled")));
			this.lbxLabels.Font = ((System.Drawing.Font)(resources.GetObject("lbxLabels.Font")));
			this.lbxLabels.HorizontalExtent = ((int)(resources.GetObject("lbxLabels.HorizontalExtent")));
			this.lbxLabels.HorizontalScrollbar = ((bool)(resources.GetObject("lbxLabels.HorizontalScrollbar")));
			this.lbxLabels.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbxLabels.ImeMode")));
			this.lbxLabels.IntegralHeight = ((bool)(resources.GetObject("lbxLabels.IntegralHeight")));
			this.lbxLabels.ItemHeight = ((int)(resources.GetObject("lbxLabels.ItemHeight")));
			this.lbxLabels.Location = ((System.Drawing.Point)(resources.GetObject("lbxLabels.Location")));
			this.lbxLabels.Name = "lbxLabels";
			this.lbxLabels.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbxLabels.RightToLeft")));
			this.lbxLabels.ScrollAlwaysVisible = ((bool)(resources.GetObject("lbxLabels.ScrollAlwaysVisible")));
			this.lbxLabels.Size = ((System.Drawing.Size)(resources.GetObject("lbxLabels.Size")));
			this.lbxLabels.TabIndex = ((int)(resources.GetObject("lbxLabels.TabIndex")));
			this.lbxLabels.Visible = ((bool)(resources.GetObject("lbxLabels.Visible")));
			this.lbxLabels.SelectedIndexChanged += new System.EventHandler(this.lbxLabels_SelectedIndexChanged);
			// 
			// trcnPanel
			// 
			this.trcnPanel.AccessibleDescription = resources.GetString("trcnPanel.AccessibleDescription");
			this.trcnPanel.AccessibleName = resources.GetString("trcnPanel.AccessibleName");
			this.trcnPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("trcnPanel.Anchor")));
			this.trcnPanel.AutoScroll = ((bool)(resources.GetObject("trcnPanel.AutoScroll")));
			this.trcnPanel.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("trcnPanel.AutoScrollMargin")));
			this.trcnPanel.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("trcnPanel.AutoScrollMinSize")));
			this.trcnPanel.BackColor = System.Drawing.SystemColors.Control;
			this.trcnPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("trcnPanel.BackgroundImage")));
			this.trcnPanel.Controls.Add(this.btnDelete);
			this.trcnPanel.Controls.Add(this.btnAdd);
			this.trcnPanel.Controls.Add(this.btnCommit);
			this.trcnPanel.Controls.Add(this.tbFilename);
			this.trcnPanel.Controls.Add(this.lbFilename);
			this.trcnPanel.Controls.Add(this.lbFormat);
			this.trcnPanel.Controls.Add(this.tbFormat);
			this.trcnPanel.Controls.Add(this.label27);
			this.trcnPanel.Controls.Add(this.gbprop);
			this.trcnPanel.Controls.Add(this.lbxLabels);
			this.trcnPanel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("trcnPanel.Dock")));
			this.trcnPanel.Enabled = ((bool)(resources.GetObject("trcnPanel.Enabled")));
			this.trcnPanel.Font = ((System.Drawing.Font)(resources.GetObject("trcnPanel.Font")));
			this.trcnPanel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("trcnPanel.ImeMode")));
			this.trcnPanel.Location = ((System.Drawing.Point)(resources.GetObject("trcnPanel.Location")));
			this.trcnPanel.Name = "trcnPanel";
			this.trcnPanel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("trcnPanel.RightToLeft")));
			this.trcnPanel.Size = ((System.Drawing.Size)(resources.GetObject("trcnPanel.Size")));
			this.trcnPanel.TabIndex = ((int)(resources.GetObject("trcnPanel.TabIndex")));
			this.trcnPanel.Text = resources.GetString("trcnPanel.Text");
			this.trcnPanel.Visible = ((bool)(resources.GetObject("trcnPanel.Visible")));
			// 
			// label27
			// 
			this.label27.AccessibleDescription = resources.GetString("label27.AccessibleDescription");
			this.label27.AccessibleName = resources.GetString("label27.AccessibleName");
			this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label27.Anchor")));
			this.label27.AutoSize = ((bool)(resources.GetObject("label27.AutoSize")));
			this.label27.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.label27.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label27.Dock")));
			this.label27.Enabled = ((bool)(resources.GetObject("label27.Enabled")));
			this.label27.Font = ((System.Drawing.Font)(resources.GetObject("label27.Font")));
			this.label27.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label27.Image = ((System.Drawing.Image)(resources.GetObject("label27.Image")));
			this.label27.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label27.ImageAlign")));
			this.label27.ImageIndex = ((int)(resources.GetObject("label27.ImageIndex")));
			this.label27.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label27.ImeMode")));
			this.label27.Location = ((System.Drawing.Point)(resources.GetObject("label27.Location")));
			this.label27.Name = "label27";
			this.label27.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label27.RightToLeft")));
			this.label27.Size = ((System.Drawing.Size)(resources.GetObject("label27.Size")));
			this.label27.TabIndex = ((int)(resources.GetObject("label27.TabIndex")));
			this.label27.Text = resources.GetString("label27.Text");
			this.label27.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label27.TextAlign")));
			this.label27.Visible = ((bool)(resources.GetObject("label27.Visible")));
			// 
			// tbFilename
			// 
			this.tbFilename.AccessibleDescription = resources.GetString("tbFilename.AccessibleDescription");
			this.tbFilename.AccessibleName = resources.GetString("tbFilename.AccessibleName");
			this.tbFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbFilename.Anchor")));
			this.tbFilename.AutoSize = ((bool)(resources.GetObject("tbFilename.AutoSize")));
			this.tbFilename.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbFilename.BackgroundImage")));
			this.tbFilename.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbFilename.Dock")));
			this.tbFilename.Enabled = ((bool)(resources.GetObject("tbFilename.Enabled")));
			this.tbFilename.Font = ((System.Drawing.Font)(resources.GetObject("tbFilename.Font")));
			this.tbFilename.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbFilename.ImeMode")));
			this.tbFilename.Location = ((System.Drawing.Point)(resources.GetObject("tbFilename.Location")));
			this.tbFilename.MaxLength = ((int)(resources.GetObject("tbFilename.MaxLength")));
			this.tbFilename.Multiline = ((bool)(resources.GetObject("tbFilename.Multiline")));
			this.tbFilename.Name = "tbFilename";
			this.tbFilename.PasswordChar = ((char)(resources.GetObject("tbFilename.PasswordChar")));
			this.tbFilename.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbFilename.RightToLeft")));
			this.tbFilename.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbFilename.ScrollBars")));
			this.tbFilename.Size = ((System.Drawing.Size)(resources.GetObject("tbFilename.Size")));
			this.tbFilename.TabIndex = ((int)(resources.GetObject("tbFilename.TabIndex")));
			this.tbFilename.Text = resources.GetString("tbFilename.Text");
			this.tbFilename.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbFilename.TextAlign")));
			this.tbFilename.Visible = ((bool)(resources.GetObject("tbFilename.Visible")));
			this.tbFilename.WordWrap = ((bool)(resources.GetObject("tbFilename.WordWrap")));
			this.tbFilename.Validated += new System.EventHandler(this.textBox_Validated);
			this.tbFilename.TextChanged += new System.EventHandler(this.textBox_TextChanged);
			// 
			// lbFilename
			// 
			this.lbFilename.AccessibleDescription = resources.GetString("lbFilename.AccessibleDescription");
			this.lbFilename.AccessibleName = resources.GetString("lbFilename.AccessibleName");
			this.lbFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbFilename.Anchor")));
			this.lbFilename.AutoSize = ((bool)(resources.GetObject("lbFilename.AutoSize")));
			this.lbFilename.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbFilename.Dock")));
			this.lbFilename.Enabled = ((bool)(resources.GetObject("lbFilename.Enabled")));
			this.lbFilename.Font = ((System.Drawing.Font)(resources.GetObject("lbFilename.Font")));
			this.lbFilename.Image = ((System.Drawing.Image)(resources.GetObject("lbFilename.Image")));
			this.lbFilename.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFilename.ImageAlign")));
			this.lbFilename.ImageIndex = ((int)(resources.GetObject("lbFilename.ImageIndex")));
			this.lbFilename.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbFilename.ImeMode")));
			this.lbFilename.Location = ((System.Drawing.Point)(resources.GetObject("lbFilename.Location")));
			this.lbFilename.Name = "lbFilename";
			this.lbFilename.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbFilename.RightToLeft")));
			this.lbFilename.Size = ((System.Drawing.Size)(resources.GetObject("lbFilename.Size")));
			this.lbFilename.TabIndex = ((int)(resources.GetObject("lbFilename.TabIndex")));
			this.lbFilename.Text = resources.GetString("lbFilename.Text");
			this.lbFilename.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFilename.TextAlign")));
			this.lbFilename.Visible = ((bool)(resources.GetObject("lbFilename.Visible")));
			// 
			// lbFormat
			// 
			this.lbFormat.AccessibleDescription = resources.GetString("lbFormat.AccessibleDescription");
			this.lbFormat.AccessibleName = resources.GetString("lbFormat.AccessibleName");
			this.lbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbFormat.Anchor")));
			this.lbFormat.AutoSize = ((bool)(resources.GetObject("lbFormat.AutoSize")));
			this.lbFormat.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbFormat.Dock")));
			this.lbFormat.Enabled = ((bool)(resources.GetObject("lbFormat.Enabled")));
			this.lbFormat.Font = ((System.Drawing.Font)(resources.GetObject("lbFormat.Font")));
			this.lbFormat.Image = ((System.Drawing.Image)(resources.GetObject("lbFormat.Image")));
			this.lbFormat.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFormat.ImageAlign")));
			this.lbFormat.ImageIndex = ((int)(resources.GetObject("lbFormat.ImageIndex")));
			this.lbFormat.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbFormat.ImeMode")));
			this.lbFormat.Location = ((System.Drawing.Point)(resources.GetObject("lbFormat.Location")));
			this.lbFormat.Name = "lbFormat";
			this.lbFormat.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbFormat.RightToLeft")));
			this.lbFormat.Size = ((System.Drawing.Size)(resources.GetObject("lbFormat.Size")));
			this.lbFormat.TabIndex = ((int)(resources.GetObject("lbFormat.TabIndex")));
			this.lbFormat.Text = resources.GetString("lbFormat.Text");
			this.lbFormat.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFormat.TextAlign")));
			this.lbFormat.Visible = ((bool)(resources.GetObject("lbFormat.Visible")));
			// 
			// tbFormat
			// 
			this.tbFormat.AccessibleDescription = resources.GetString("tbFormat.AccessibleDescription");
			this.tbFormat.AccessibleName = resources.GetString("tbFormat.AccessibleName");
			this.tbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbFormat.Anchor")));
			this.tbFormat.AutoSize = ((bool)(resources.GetObject("tbFormat.AutoSize")));
			this.tbFormat.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbFormat.BackgroundImage")));
			this.tbFormat.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbFormat.Dock")));
			this.tbFormat.Enabled = ((bool)(resources.GetObject("tbFormat.Enabled")));
			this.tbFormat.Font = ((System.Drawing.Font)(resources.GetObject("tbFormat.Font")));
			this.tbFormat.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbFormat.ImeMode")));
			this.tbFormat.Location = ((System.Drawing.Point)(resources.GetObject("tbFormat.Location")));
			this.tbFormat.MaxLength = ((int)(resources.GetObject("tbFormat.MaxLength")));
			this.tbFormat.Multiline = ((bool)(resources.GetObject("tbFormat.Multiline")));
			this.tbFormat.Name = "tbFormat";
			this.tbFormat.PasswordChar = ((char)(resources.GetObject("tbFormat.PasswordChar")));
			this.tbFormat.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbFormat.RightToLeft")));
			this.tbFormat.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbFormat.ScrollBars")));
			this.tbFormat.Size = ((System.Drawing.Size)(resources.GetObject("tbFormat.Size")));
			this.tbFormat.TabIndex = ((int)(resources.GetObject("tbFormat.TabIndex")));
			this.tbFormat.Text = resources.GetString("tbFormat.Text");
			this.tbFormat.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbFormat.TextAlign")));
			this.tbFormat.Visible = ((bool)(resources.GetObject("tbFormat.Visible")));
			this.tbFormat.WordWrap = ((bool)(resources.GetObject("tbFormat.WordWrap")));
			this.tbFormat.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
			this.tbFormat.Validated += new System.EventHandler(this.hex32_Validated);
			this.tbFormat.TextChanged += new System.EventHandler(this.hex32_TextChanged);
			// 
			// btnCommit
			// 
			this.btnCommit.AccessibleDescription = resources.GetString("btnCommit.AccessibleDescription");
			this.btnCommit.AccessibleName = resources.GetString("btnCommit.AccessibleName");
			this.btnCommit.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCommit.Anchor")));
			this.btnCommit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCommit.BackgroundImage")));
			this.btnCommit.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCommit.Dock")));
			this.btnCommit.Enabled = ((bool)(resources.GetObject("btnCommit.Enabled")));
			this.btnCommit.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCommit.FlatStyle")));
			this.btnCommit.Font = ((System.Drawing.Font)(resources.GetObject("btnCommit.Font")));
			this.btnCommit.Image = ((System.Drawing.Image)(resources.GetObject("btnCommit.Image")));
			this.btnCommit.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCommit.ImageAlign")));
			this.btnCommit.ImageIndex = ((int)(resources.GetObject("btnCommit.ImageIndex")));
			this.btnCommit.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCommit.ImeMode")));
			this.btnCommit.Location = ((System.Drawing.Point)(resources.GetObject("btnCommit.Location")));
			this.btnCommit.Name = "btnCommit";
			this.btnCommit.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCommit.RightToLeft")));
			this.btnCommit.Size = ((System.Drawing.Size)(resources.GetObject("btnCommit.Size")));
			this.btnCommit.TabIndex = ((int)(resources.GetObject("btnCommit.TabIndex")));
			this.btnCommit.Text = resources.GetString("btnCommit.Text");
			this.btnCommit.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCommit.TextAlign")));
			this.btnCommit.Visible = ((bool)(resources.GetObject("btnCommit.Visible")));
			// 
			// btnDelete
			// 
			this.btnDelete.AccessibleDescription = resources.GetString("btnDelete.AccessibleDescription");
			this.btnDelete.AccessibleName = resources.GetString("btnDelete.AccessibleName");
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDelete.Anchor")));
			this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
			this.btnDelete.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDelete.Dock")));
			this.btnDelete.Enabled = ((bool)(resources.GetObject("btnDelete.Enabled")));
			this.btnDelete.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDelete.FlatStyle")));
			this.btnDelete.Font = ((System.Drawing.Font)(resources.GetObject("btnDelete.Font")));
			this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
			this.btnDelete.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelete.ImageAlign")));
			this.btnDelete.ImageIndex = ((int)(resources.GetObject("btnDelete.ImageIndex")));
			this.btnDelete.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDelete.ImeMode")));
			this.btnDelete.Location = ((System.Drawing.Point)(resources.GetObject("btnDelete.Location")));
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDelete.RightToLeft")));
			this.btnDelete.Size = ((System.Drawing.Size)(resources.GetObject("btnDelete.Size")));
			this.btnDelete.TabIndex = ((int)(resources.GetObject("btnDelete.TabIndex")));
			this.btnDelete.Text = resources.GetString("btnDelete.Text");
			this.btnDelete.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelete.TextAlign")));
			this.btnDelete.Visible = ((bool)(resources.GetObject("btnDelete.Visible")));
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.AccessibleDescription = resources.GetString("btnAdd.AccessibleDescription");
			this.btnAdd.AccessibleName = resources.GetString("btnAdd.AccessibleName");
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAdd.Anchor")));
			this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
			this.btnAdd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAdd.Dock")));
			this.btnAdd.Enabled = ((bool)(resources.GetObject("btnAdd.Enabled")));
			this.btnAdd.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAdd.FlatStyle")));
			this.btnAdd.Font = ((System.Drawing.Font)(resources.GetObject("btnAdd.Font")));
			this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
			this.btnAdd.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAdd.ImageAlign")));
			this.btnAdd.ImageIndex = ((int)(resources.GetObject("btnAdd.ImageIndex")));
			this.btnAdd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAdd.ImeMode")));
			this.btnAdd.Location = ((System.Drawing.Point)(resources.GetObject("btnAdd.Location")));
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAdd.RightToLeft")));
			this.btnAdd.Size = ((System.Drawing.Size)(resources.GetObject("btnAdd.Size")));
			this.btnAdd.TabIndex = ((int)(resources.GetObject("btnAdd.TabIndex")));
			this.btnAdd.Text = resources.GetString("btnAdd.Text");
			this.btnAdd.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAdd.TextAlign")));
			this.btnAdd.Visible = ((bool)(resources.GetObject("btnAdd.Visible")));
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// TrcnForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.trcnPanel);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "TrcnForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.gbprop.ResumeLayout(false);
			this.trcnPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private void hex32_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex32_IsValid(sender)) return;

			uint val = Convert.ToUInt32(((TextBox)sender).Text, 16);
			internalchg = true;
			switch (alHex32.IndexOf(sender))
			{
				case 0: wrapper.Format = val; break;
				case 1:
					currentItem.Constant = val;
					this.lbxLabels.SelectedItem = "0x" + Helper.HexString(currentItem.Constant) + ": " + currentItem.Label;
					break;
			}
			internalchg = false;
		}

		private void hex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex32_IsValid(sender)) return;

			e.Cancel = true;

			internalchg = true;
			uint val = 0;
			switch (alHex32.IndexOf(sender))
			{
				case 0: val = wrapper.Format; break;
				case 1: val = currentItem.Constant; break;
			}

			((TextBox)sender).Text = "0x" + Helper.HexString(val);
			((TextBox)sender).SelectAll();
			internalchg = false;
		}

		private void hex32_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + Helper.HexString(Convert.ToUInt32(((TextBox)sender).Text, 16));
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}


		private void textBox_TextChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			internalchg = true;
			switch(alTextBox.IndexOf(sender))
			{
				case 0: wrapper.FileName = ((TextBox)sender).Text; break;
				case 1:
					currentItem.Label = ((TextBox)sender).Text;
					this.lbxLabels.SelectedItem = "0x" + Helper.HexString(currentItem.Constant) + ": " + currentItem.Label;
					break;
			}
			internalchg = false;
		}

		private void textBox_Validated(object sender, System.EventArgs e)
		{
			((TextBox)sender).SelectAll();
		}


		private void btnCommit_Click(object sender, System.EventArgs e)
		{
			try 
			{
				wrapper.SynchronizeUserData();
				btnCommit.Enabled = wrapper.Changed;
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}			
		}


		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			bool savedstate = internalchg;

			internalchg = true;
			wrapper.Add(0xffffffff, "");
			this.lbxLabels.Items.Add("0xFFFFFFFF: ");
			internalchg = savedstate;

			this.lbxLabels.SelectedIndex = this.lbxLabels.Items.Count - 1;
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			if (this.lbxLabels.SelectedIndex < 0) return;

			bool savedstate = internalchg;
			internalchg = true;

			wrapper.RemoveAt(this.lbxLabels.SelectedIndex);
			int indexwas = this.lbxLabels.SelectedIndex;
			this.lbxLabels.Items.RemoveAt(this.lbxLabels.SelectedIndex);

			internalchg = savedstate;

			this.lbxLabels.SelectedIndex = (indexwas >= this.lbxLabels.Items.Count) ? this.lbxLabels.Items.Count - 1 : indexwas;
		}

		private void lbxLabels_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			internalchg = true;
			if (lbxLabels.SelectedIndex >= 0)
			{
				currentItem = wrapper[lbxLabels.SelectedIndex];
				this.tbConstant.Text = "0x" + Helper.HexString(currentItem.Constant);
				this.tbLabel.Text = currentItem.Label;
				this.btnDelete.Enabled = this.tbConstant.Enabled = this.tbLabel.Enabled = false;
			}
			else
			{
				currentItem = null;
				this.tbConstant.Text = this.tbLabel.Text = "";
				this.btnDelete.Enabled = this.tbConstant.Enabled = this.tbLabel.Enabled = false;
			}
			internalchg = false;
		}
	}
}
