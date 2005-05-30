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
	/// Summary description for BconForm.
	/// </summary>
	public class BconForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables
		private System.Windows.Forms.TextBox tbFilename;
		private System.Windows.Forms.LinkLabel llcadd;
		private System.Windows.Forms.LinkLabel llcdel;
		private System.Windows.Forms.Panel bconPanel;
		private System.Windows.Forms.LinkLabel llccancel;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.ColumnHeader lineNumber;
		private System.Windows.Forms.ColumnHeader data;
		private System.Windows.Forms.ListView lvConstants;
		private System.Windows.Forms.Panel pnHeading;
		private System.Windows.Forms.GroupBox gbConstant;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbFlag;
		private System.Windows.Forms.TextBox tbValueHex;
		private System.Windows.Forms.TextBox tbValueDec;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public BconForm()
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


		#region BconForm
		private Bcon wrapper;
		private int currentIndex;
		private int origValue;
		private int currentValue;

		private void UpdateBconValue()
		{
			if ((short)wrapper.Constants[currentIndex] == currentValue) return;
			wrapper.Constants[currentIndex] = (short)currentValue;
			wrapper.Changed = true;
			btnCommit.Enabled = true;

			lvConstants.Items[currentIndex].SubItems[1].Text = currentValue.ToString();
			lvConstants.Update();
		}
		#endregion

		#region IPackedFileUI Member
		/// <summary>
		/// Returns the Panel that will be displayed within SimPe
		/// </summary>
		public System.Windows.Forms.Panel GUIHandle
		{
			get
			{
				return bconPanel;
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
			//wrapper;
			wrapper = (Bcon)wrp;
			//currentIndex;
			currentIndex = -2; // force update
			//currentValue;
			currentValue = -1;
			//origValue;
			origValue = -1;
			//label27 - static
			//panel2 - static
			//label44 - static
			//tbFilename;
			tbFilename.Tag = true;
			tbFilename.Text = wrapper.FileName;
			tbFilename.Tag = null;
			//label22 - static
			//tbconstflag;
			tbFilename.Tag = true;
			tbFlag.Text = "0x"+Helper.HexString(wrapper.Flag);
			tbFilename.Tag = null;
			//label28 - static
			//lvConstants;
			lvConstants.Tag = true;
			lvConstants.Items.Clear();
			foreach (short item in wrapper.Constants)
			{
				ListViewItem i = new ListViewItem("0x" + lvConstants.Items.Count.ToString("X"));
				i.SubItems.Add(item.ToString());
				lvConstants.Items.Add(i);
			}
			lvConstants.Tag = null;
			lvConstants_IndexChanged(null, null);
			//label18 - static
			//tbValueHex - set by SelectedIndex change
			//tbValueDec - set by SelectedIndex change
			//llccancel - set by SelectedIndex change
			//llcadd;
			//llcdel - set by SelectedIndex change
			//groupBox1 - static
			//btnCommit;
			btnCommit.Enabled = false;
			//bconPanel - static
		}		

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BconForm));
			this.label1 = new System.Windows.Forms.Label();
			this.pnHeading = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.tbFilename = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tbFlag = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tbValueDec = new System.Windows.Forms.TextBox();
			this.llcdel = new System.Windows.Forms.LinkLabel();
			this.llcadd = new System.Windows.Forms.LinkLabel();
			this.llccancel = new System.Windows.Forms.LinkLabel();
			this.tbValueHex = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.gbConstant = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.bconPanel = new System.Windows.Forms.Panel();
			this.lvConstants = new System.Windows.Forms.ListView();
			this.lineNumber = new System.Windows.Forms.ColumnHeader();
			this.data = new System.Windows.Forms.ColumnHeader();
			this.btnCommit = new System.Windows.Forms.Button();
			this.pnHeading.SuspendLayout();
			this.gbConstant.SuspendLayout();
			this.bconPanel.SuspendLayout();
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
			// pnHeading
			// 
			this.pnHeading.AccessibleDescription = resources.GetString("pnHeading.AccessibleDescription");
			this.pnHeading.AccessibleName = resources.GetString("pnHeading.AccessibleName");
			this.pnHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pnHeading.Anchor")));
			this.pnHeading.AutoScroll = ((bool)(resources.GetObject("pnHeading.AutoScroll")));
			this.pnHeading.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("pnHeading.AutoScrollMargin")));
			this.pnHeading.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("pnHeading.AutoScrollMinSize")));
			this.pnHeading.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.pnHeading.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnHeading.BackgroundImage")));
			this.pnHeading.Controls.Add(this.label1);
			this.pnHeading.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnHeading.Dock")));
			this.pnHeading.Enabled = ((bool)(resources.GetObject("pnHeading.Enabled")));
			this.pnHeading.Font = ((System.Drawing.Font)(resources.GetObject("pnHeading.Font")));
			this.pnHeading.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.pnHeading.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnHeading.ImeMode")));
			this.pnHeading.Location = ((System.Drawing.Point)(resources.GetObject("pnHeading.Location")));
			this.pnHeading.Name = "pnHeading";
			this.pnHeading.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnHeading.RightToLeft")));
			this.pnHeading.Size = ((System.Drawing.Size)(resources.GetObject("pnHeading.Size")));
			this.pnHeading.TabIndex = ((int)(resources.GetObject("pnHeading.TabIndex")));
			this.pnHeading.Text = resources.GetString("pnHeading.Text");
			this.pnHeading.Visible = ((bool)(resources.GetObject("pnHeading.Visible")));
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
			this.tbFilename.TextChanged += new System.EventHandler(this.tbFilename_TextChanged);
			// 
			// label3
			// 
			this.label3.AccessibleDescription = resources.GetString("label3.AccessibleDescription");
			this.label3.AccessibleName = resources.GetString("label3.AccessibleName");
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label3.Anchor")));
			this.label3.AutoSize = ((bool)(resources.GetObject("label3.AutoSize")));
			this.label3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label3.Dock")));
			this.label3.Enabled = ((bool)(resources.GetObject("label3.Enabled")));
			this.label3.Font = ((System.Drawing.Font)(resources.GetObject("label3.Font")));
			this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
			this.label3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label3.ImageAlign")));
			this.label3.ImageIndex = ((int)(resources.GetObject("label3.ImageIndex")));
			this.label3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label3.ImeMode")));
			this.label3.Location = ((System.Drawing.Point)(resources.GetObject("label3.Location")));
			this.label3.Name = "label3";
			this.label3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label3.RightToLeft")));
			this.label3.Size = ((System.Drawing.Size)(resources.GetObject("label3.Size")));
			this.label3.TabIndex = ((int)(resources.GetObject("label3.TabIndex")));
			this.label3.Text = resources.GetString("label3.Text");
			this.label3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label3.TextAlign")));
			this.label3.Visible = ((bool)(resources.GetObject("label3.Visible")));
			// 
			// tbFlag
			// 
			this.tbFlag.AccessibleDescription = resources.GetString("tbFlag.AccessibleDescription");
			this.tbFlag.AccessibleName = resources.GetString("tbFlag.AccessibleName");
			this.tbFlag.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbFlag.Anchor")));
			this.tbFlag.AutoSize = ((bool)(resources.GetObject("tbFlag.AutoSize")));
			this.tbFlag.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbFlag.BackgroundImage")));
			this.tbFlag.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbFlag.Dock")));
			this.tbFlag.Enabled = ((bool)(resources.GetObject("tbFlag.Enabled")));
			this.tbFlag.Font = ((System.Drawing.Font)(resources.GetObject("tbFlag.Font")));
			this.tbFlag.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbFlag.ImeMode")));
			this.tbFlag.Location = ((System.Drawing.Point)(resources.GetObject("tbFlag.Location")));
			this.tbFlag.MaxLength = ((int)(resources.GetObject("tbFlag.MaxLength")));
			this.tbFlag.Multiline = ((bool)(resources.GetObject("tbFlag.Multiline")));
			this.tbFlag.Name = "tbFlag";
			this.tbFlag.PasswordChar = ((char)(resources.GetObject("tbFlag.PasswordChar")));
			this.tbFlag.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbFlag.RightToLeft")));
			this.tbFlag.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbFlag.ScrollBars")));
			this.tbFlag.Size = ((System.Drawing.Size)(resources.GetObject("tbFlag.Size")));
			this.tbFlag.TabIndex = ((int)(resources.GetObject("tbFlag.TabIndex")));
			this.tbFlag.Text = resources.GetString("tbFlag.Text");
			this.tbFlag.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbFlag.TextAlign")));
			this.tbFlag.Visible = ((bool)(resources.GetObject("tbFlag.Visible")));
			this.tbFlag.WordWrap = ((bool)(resources.GetObject("tbFlag.WordWrap")));
			this.tbFlag.TextChanged += new System.EventHandler(this.tbFlag_TextChanged);
			// 
			// label4
			// 
			this.label4.AccessibleDescription = resources.GetString("label4.AccessibleDescription");
			this.label4.AccessibleName = resources.GetString("label4.AccessibleName");
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label4.Anchor")));
			this.label4.AutoSize = ((bool)(resources.GetObject("label4.AutoSize")));
			this.label4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label4.Dock")));
			this.label4.Enabled = ((bool)(resources.GetObject("label4.Enabled")));
			this.label4.Font = ((System.Drawing.Font)(resources.GetObject("label4.Font")));
			this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
			this.label4.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.ImageAlign")));
			this.label4.ImageIndex = ((int)(resources.GetObject("label4.ImageIndex")));
			this.label4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label4.ImeMode")));
			this.label4.Location = ((System.Drawing.Point)(resources.GetObject("label4.Location")));
			this.label4.Name = "label4";
			this.label4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label4.RightToLeft")));
			this.label4.Size = ((System.Drawing.Size)(resources.GetObject("label4.Size")));
			this.label4.TabIndex = ((int)(resources.GetObject("label4.TabIndex")));
			this.label4.Text = resources.GetString("label4.Text");
			this.label4.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.TextAlign")));
			this.label4.Visible = ((bool)(resources.GetObject("label4.Visible")));
			// 
			// tbValueDec
			// 
			this.tbValueDec.AccessibleDescription = resources.GetString("tbValueDec.AccessibleDescription");
			this.tbValueDec.AccessibleName = resources.GetString("tbValueDec.AccessibleName");
			this.tbValueDec.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbValueDec.Anchor")));
			this.tbValueDec.AutoSize = ((bool)(resources.GetObject("tbValueDec.AutoSize")));
			this.tbValueDec.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbValueDec.BackgroundImage")));
			this.tbValueDec.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbValueDec.Dock")));
			this.tbValueDec.Enabled = ((bool)(resources.GetObject("tbValueDec.Enabled")));
			this.tbValueDec.Font = ((System.Drawing.Font)(resources.GetObject("tbValueDec.Font")));
			this.tbValueDec.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbValueDec.ImeMode")));
			this.tbValueDec.Location = ((System.Drawing.Point)(resources.GetObject("tbValueDec.Location")));
			this.tbValueDec.MaxLength = ((int)(resources.GetObject("tbValueDec.MaxLength")));
			this.tbValueDec.Multiline = ((bool)(resources.GetObject("tbValueDec.Multiline")));
			this.tbValueDec.Name = "tbValueDec";
			this.tbValueDec.PasswordChar = ((char)(resources.GetObject("tbValueDec.PasswordChar")));
			this.tbValueDec.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbValueDec.RightToLeft")));
			this.tbValueDec.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbValueDec.ScrollBars")));
			this.tbValueDec.Size = ((System.Drawing.Size)(resources.GetObject("tbValueDec.Size")));
			this.tbValueDec.TabIndex = ((int)(resources.GetObject("tbValueDec.TabIndex")));
			this.tbValueDec.Text = resources.GetString("tbValueDec.Text");
			this.tbValueDec.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbValueDec.TextAlign")));
			this.tbValueDec.Visible = ((bool)(resources.GetObject("tbValueDec.Visible")));
			this.tbValueDec.WordWrap = ((bool)(resources.GetObject("tbValueDec.WordWrap")));
			this.tbValueDec.TextChanged += new System.EventHandler(this.tbdec_TextChanged);
			// 
			// llcdel
			// 
			this.llcdel.AccessibleDescription = resources.GetString("llcdel.AccessibleDescription");
			this.llcdel.AccessibleName = resources.GetString("llcdel.AccessibleName");
			this.llcdel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llcdel.Anchor")));
			this.llcdel.AutoSize = ((bool)(resources.GetObject("llcdel.AutoSize")));
			this.llcdel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llcdel.Dock")));
			this.llcdel.Enabled = ((bool)(resources.GetObject("llcdel.Enabled")));
			this.llcdel.Font = ((System.Drawing.Font)(resources.GetObject("llcdel.Font")));
			this.llcdel.Image = ((System.Drawing.Image)(resources.GetObject("llcdel.Image")));
			this.llcdel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llcdel.ImageAlign")));
			this.llcdel.ImageIndex = ((int)(resources.GetObject("llcdel.ImageIndex")));
			this.llcdel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llcdel.ImeMode")));
			this.llcdel.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llcdel.LinkArea")));
			this.llcdel.Location = ((System.Drawing.Point)(resources.GetObject("llcdel.Location")));
			this.llcdel.Name = "llcdel";
			this.llcdel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llcdel.RightToLeft")));
			this.llcdel.Size = ((System.Drawing.Size)(resources.GetObject("llcdel.Size")));
			this.llcdel.TabIndex = ((int)(resources.GetObject("llcdel.TabIndex")));
			this.llcdel.TabStop = true;
			this.llcdel.Text = resources.GetString("llcdel.Text");
			this.llcdel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llcdel.TextAlign")));
			this.llcdel.Visible = ((bool)(resources.GetObject("llcdel.Visible")));
			this.llcdel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DeleteConstantClicked);
			// 
			// llcadd
			// 
			this.llcadd.AccessibleDescription = resources.GetString("llcadd.AccessibleDescription");
			this.llcadd.AccessibleName = resources.GetString("llcadd.AccessibleName");
			this.llcadd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llcadd.Anchor")));
			this.llcadd.AutoSize = ((bool)(resources.GetObject("llcadd.AutoSize")));
			this.llcadd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llcadd.Dock")));
			this.llcadd.Enabled = ((bool)(resources.GetObject("llcadd.Enabled")));
			this.llcadd.Font = ((System.Drawing.Font)(resources.GetObject("llcadd.Font")));
			this.llcadd.Image = ((System.Drawing.Image)(resources.GetObject("llcadd.Image")));
			this.llcadd.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llcadd.ImageAlign")));
			this.llcadd.ImageIndex = ((int)(resources.GetObject("llcadd.ImageIndex")));
			this.llcadd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llcadd.ImeMode")));
			this.llcadd.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llcadd.LinkArea")));
			this.llcadd.Location = ((System.Drawing.Point)(resources.GetObject("llcadd.Location")));
			this.llcadd.Name = "llcadd";
			this.llcadd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llcadd.RightToLeft")));
			this.llcadd.Size = ((System.Drawing.Size)(resources.GetObject("llcadd.Size")));
			this.llcadd.TabIndex = ((int)(resources.GetObject("llcadd.TabIndex")));
			this.llcadd.TabStop = true;
			this.llcadd.Text = resources.GetString("llcadd.Text");
			this.llcadd.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llcadd.TextAlign")));
			this.llcadd.Visible = ((bool)(resources.GetObject("llcadd.Visible")));
			this.llcadd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AddConstantClicked);
			// 
			// llccancel
			// 
			this.llccancel.AccessibleDescription = resources.GetString("llccancel.AccessibleDescription");
			this.llccancel.AccessibleName = resources.GetString("llccancel.AccessibleName");
			this.llccancel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llccancel.Anchor")));
			this.llccancel.AutoSize = ((bool)(resources.GetObject("llccancel.AutoSize")));
			this.llccancel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llccancel.Dock")));
			this.llccancel.Enabled = ((bool)(resources.GetObject("llccancel.Enabled")));
			this.llccancel.Font = ((System.Drawing.Font)(resources.GetObject("llccancel.Font")));
			this.llccancel.Image = ((System.Drawing.Image)(resources.GetObject("llccancel.Image")));
			this.llccancel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llccancel.ImageAlign")));
			this.llccancel.ImageIndex = ((int)(resources.GetObject("llccancel.ImageIndex")));
			this.llccancel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llccancel.ImeMode")));
			this.llccancel.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llccancel.LinkArea")));
			this.llccancel.Location = ((System.Drawing.Point)(resources.GetObject("llccancel.Location")));
			this.llccancel.Name = "llccancel";
			this.llccancel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llccancel.RightToLeft")));
			this.llccancel.Size = ((System.Drawing.Size)(resources.GetObject("llccancel.Size")));
			this.llccancel.TabIndex = ((int)(resources.GetObject("llccancel.TabIndex")));
			this.llccancel.TabStop = true;
			this.llccancel.Text = resources.GetString("llccancel.Text");
			this.llccancel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llccancel.TextAlign")));
			this.llccancel.Visible = ((bool)(resources.GetObject("llccancel.Visible")));
			this.llccancel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llccancel_Clicked);
			// 
			// tbValueHex
			// 
			this.tbValueHex.AccessibleDescription = resources.GetString("tbValueHex.AccessibleDescription");
			this.tbValueHex.AccessibleName = resources.GetString("tbValueHex.AccessibleName");
			this.tbValueHex.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbValueHex.Anchor")));
			this.tbValueHex.AutoSize = ((bool)(resources.GetObject("tbValueHex.AutoSize")));
			this.tbValueHex.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbValueHex.BackgroundImage")));
			this.tbValueHex.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbValueHex.Dock")));
			this.tbValueHex.Enabled = ((bool)(resources.GetObject("tbValueHex.Enabled")));
			this.tbValueHex.Font = ((System.Drawing.Font)(resources.GetObject("tbValueHex.Font")));
			this.tbValueHex.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbValueHex.ImeMode")));
			this.tbValueHex.Location = ((System.Drawing.Point)(resources.GetObject("tbValueHex.Location")));
			this.tbValueHex.MaxLength = ((int)(resources.GetObject("tbValueHex.MaxLength")));
			this.tbValueHex.Multiline = ((bool)(resources.GetObject("tbValueHex.Multiline")));
			this.tbValueHex.Name = "tbValueHex";
			this.tbValueHex.PasswordChar = ((char)(resources.GetObject("tbValueHex.PasswordChar")));
			this.tbValueHex.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbValueHex.RightToLeft")));
			this.tbValueHex.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbValueHex.ScrollBars")));
			this.tbValueHex.Size = ((System.Drawing.Size)(resources.GetObject("tbValueHex.Size")));
			this.tbValueHex.TabIndex = ((int)(resources.GetObject("tbValueHex.TabIndex")));
			this.tbValueHex.Text = resources.GetString("tbValueHex.Text");
			this.tbValueHex.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbValueHex.TextAlign")));
			this.tbValueHex.Visible = ((bool)(resources.GetObject("tbValueHex.Visible")));
			this.tbValueHex.WordWrap = ((bool)(resources.GetObject("tbValueHex.WordWrap")));
			this.tbValueHex.TextChanged += new System.EventHandler(this.tbvalue_TextChanged);
			// 
			// label5
			// 
			this.label5.AccessibleDescription = resources.GetString("label5.AccessibleDescription");
			this.label5.AccessibleName = resources.GetString("label5.AccessibleName");
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label5.Anchor")));
			this.label5.AutoSize = ((bool)(resources.GetObject("label5.AutoSize")));
			this.label5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label5.Dock")));
			this.label5.Enabled = ((bool)(resources.GetObject("label5.Enabled")));
			this.label5.Font = ((System.Drawing.Font)(resources.GetObject("label5.Font")));
			this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
			this.label5.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label5.ImageAlign")));
			this.label5.ImageIndex = ((int)(resources.GetObject("label5.ImageIndex")));
			this.label5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label5.ImeMode")));
			this.label5.Location = ((System.Drawing.Point)(resources.GetObject("label5.Location")));
			this.label5.Name = "label5";
			this.label5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label5.RightToLeft")));
			this.label5.Size = ((System.Drawing.Size)(resources.GetObject("label5.Size")));
			this.label5.TabIndex = ((int)(resources.GetObject("label5.TabIndex")));
			this.label5.Text = resources.GetString("label5.Text");
			this.label5.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label5.TextAlign")));
			this.label5.Visible = ((bool)(resources.GetObject("label5.Visible")));
			// 
			// gbConstant
			// 
			this.gbConstant.AccessibleDescription = resources.GetString("gbConstant.AccessibleDescription");
			this.gbConstant.AccessibleName = resources.GetString("gbConstant.AccessibleName");
			this.gbConstant.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gbConstant.Anchor")));
			this.gbConstant.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gbConstant.BackgroundImage")));
			this.gbConstant.Controls.Add(this.tbValueDec);
			this.gbConstant.Controls.Add(this.llcdel);
			this.gbConstant.Controls.Add(this.llcadd);
			this.gbConstant.Controls.Add(this.llccancel);
			this.gbConstant.Controls.Add(this.tbValueHex);
			this.gbConstant.Controls.Add(this.label5);
			this.gbConstant.Controls.Add(this.label6);
			this.gbConstant.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("gbConstant.Dock")));
			this.gbConstant.Enabled = ((bool)(resources.GetObject("gbConstant.Enabled")));
			this.gbConstant.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.gbConstant.Font = ((System.Drawing.Font)(resources.GetObject("gbConstant.Font")));
			this.gbConstant.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gbConstant.ImeMode")));
			this.gbConstant.Location = ((System.Drawing.Point)(resources.GetObject("gbConstant.Location")));
			this.gbConstant.Name = "gbConstant";
			this.gbConstant.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("gbConstant.RightToLeft")));
			this.gbConstant.Size = ((System.Drawing.Size)(resources.GetObject("gbConstant.Size")));
			this.gbConstant.TabIndex = ((int)(resources.GetObject("gbConstant.TabIndex")));
			this.gbConstant.TabStop = false;
			this.gbConstant.Text = resources.GetString("gbConstant.Text");
			this.gbConstant.Visible = ((bool)(resources.GetObject("gbConstant.Visible")));
			// 
			// label6
			// 
			this.label6.AccessibleDescription = resources.GetString("label6.AccessibleDescription");
			this.label6.AccessibleName = resources.GetString("label6.AccessibleName");
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label6.Anchor")));
			this.label6.AutoSize = ((bool)(resources.GetObject("label6.AutoSize")));
			this.label6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label6.Dock")));
			this.label6.Enabled = ((bool)(resources.GetObject("label6.Enabled")));
			this.label6.Font = ((System.Drawing.Font)(resources.GetObject("label6.Font")));
			this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
			this.label6.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.ImageAlign")));
			this.label6.ImageIndex = ((int)(resources.GetObject("label6.ImageIndex")));
			this.label6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label6.ImeMode")));
			this.label6.Location = ((System.Drawing.Point)(resources.GetObject("label6.Location")));
			this.label6.Name = "label6";
			this.label6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label6.RightToLeft")));
			this.label6.Size = ((System.Drawing.Size)(resources.GetObject("label6.Size")));
			this.label6.TabIndex = ((int)(resources.GetObject("label6.TabIndex")));
			this.label6.Text = resources.GetString("label6.Text");
			this.label6.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.TextAlign")));
			this.label6.Visible = ((bool)(resources.GetObject("label6.Visible")));
			// 
			// bconPanel
			// 
			this.bconPanel.AccessibleDescription = resources.GetString("bconPanel.AccessibleDescription");
			this.bconPanel.AccessibleName = resources.GetString("bconPanel.AccessibleName");
			this.bconPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("bconPanel.Anchor")));
			this.bconPanel.AutoScroll = ((bool)(resources.GetObject("bconPanel.AutoScroll")));
			this.bconPanel.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("bconPanel.AutoScrollMargin")));
			this.bconPanel.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("bconPanel.AutoScrollMinSize")));
			this.bconPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bconPanel.BackgroundImage")));
			this.bconPanel.Controls.Add(this.lvConstants);
			this.bconPanel.Controls.Add(this.btnCommit);
			this.bconPanel.Controls.Add(this.label2);
			this.bconPanel.Controls.Add(this.tbFilename);
			this.bconPanel.Controls.Add(this.gbConstant);
			this.bconPanel.Controls.Add(this.tbFlag);
			this.bconPanel.Controls.Add(this.label3);
			this.bconPanel.Controls.Add(this.pnHeading);
			this.bconPanel.Controls.Add(this.label4);
			this.bconPanel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("bconPanel.Dock")));
			this.bconPanel.Enabled = ((bool)(resources.GetObject("bconPanel.Enabled")));
			this.bconPanel.Font = ((System.Drawing.Font)(resources.GetObject("bconPanel.Font")));
			this.bconPanel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("bconPanel.ImeMode")));
			this.bconPanel.Location = ((System.Drawing.Point)(resources.GetObject("bconPanel.Location")));
			this.bconPanel.Name = "bconPanel";
			this.bconPanel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("bconPanel.RightToLeft")));
			this.bconPanel.Size = ((System.Drawing.Size)(resources.GetObject("bconPanel.Size")));
			this.bconPanel.TabIndex = ((int)(resources.GetObject("bconPanel.TabIndex")));
			this.bconPanel.Text = resources.GetString("bconPanel.Text");
			this.bconPanel.Visible = ((bool)(resources.GetObject("bconPanel.Visible")));
			// 
			// lvConstants
			// 
			this.lvConstants.AccessibleDescription = resources.GetString("lvConstants.AccessibleDescription");
			this.lvConstants.AccessibleName = resources.GetString("lvConstants.AccessibleName");
			this.lvConstants.Alignment = ((System.Windows.Forms.ListViewAlignment)(resources.GetObject("lvConstants.Alignment")));
			this.lvConstants.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lvConstants.Anchor")));
			this.lvConstants.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lvConstants.BackgroundImage")));
			this.lvConstants.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						  this.lineNumber,
																						  this.data});
			this.lvConstants.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lvConstants.Dock")));
			this.lvConstants.Enabled = ((bool)(resources.GetObject("lvConstants.Enabled")));
			this.lvConstants.Font = ((System.Drawing.Font)(resources.GetObject("lvConstants.Font")));
			this.lvConstants.FullRowSelect = true;
			this.lvConstants.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvConstants.HideSelection = false;
			this.lvConstants.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lvConstants.ImeMode")));
			this.lvConstants.LabelWrap = ((bool)(resources.GetObject("lvConstants.LabelWrap")));
			this.lvConstants.Location = ((System.Drawing.Point)(resources.GetObject("lvConstants.Location")));
			this.lvConstants.MultiSelect = false;
			this.lvConstants.Name = "lvConstants";
			this.lvConstants.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lvConstants.RightToLeft")));
			this.lvConstants.Size = ((System.Drawing.Size)(resources.GetObject("lvConstants.Size")));
			this.lvConstants.TabIndex = ((int)(resources.GetObject("lvConstants.TabIndex")));
			this.lvConstants.Text = resources.GetString("lvConstants.Text");
			this.lvConstants.View = System.Windows.Forms.View.Details;
			this.lvConstants.Visible = ((bool)(resources.GetObject("lvConstants.Visible")));
			this.lvConstants.SelectedIndexChanged += new System.EventHandler(this.lvConstants_IndexChanged);
			// 
			// lineNumber
			// 
			this.lineNumber.Text = resources.GetString("lineNumber.Text");
			this.lineNumber.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("lineNumber.TextAlign")));
			this.lineNumber.Width = ((int)(resources.GetObject("lineNumber.Width")));
			// 
			// data
			// 
			this.data.Text = resources.GetString("data.Text");
			this.data.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("data.TextAlign")));
			this.data.Width = ((int)(resources.GetObject("data.Width")));
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
			this.btnCommit.Click += new System.EventHandler(this.btnCommit_Clicked);
			// 
			// BconForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.bconPanel);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "BconForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.pnHeading.ResumeLayout(false);
			this.gbConstant.ResumeLayout(false);
			this.bconPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private void tbFilename_TextChanged(object sender, System.EventArgs e)
		{
			if (tbFilename.Tag != null) return;
			wrapper.FileName = tbFilename.Text;
			wrapper.Changed = true;
			btnCommit.Enabled = true;
		}

		private void tbFlag_TextChanged(object sender, System.EventArgs e)
		{
			if (tbFilename.Tag != null) return;
			try 
			{
				wrapper.Flag = Convert.ToByte(tbFlag.Text, 16);
				wrapper.Changed = true;
				btnCommit.Enabled = true;
			}
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			} 
		}

		private void lvConstants_IndexChanged(object sender, System.EventArgs e)
		{
			if (lvConstants.Tag != null) return; // Internal change

			if (lvConstants.SelectedIndices.Count > 0)
			{
				if (currentIndex == lvConstants.SelectedIndices[0]) return; // no change
				currentIndex = lvConstants.SelectedIndices[0];
			}

			if (currentIndex < 0) 
			{
				llcdel.Enabled = false;
				origValue = currentValue = -1;
				tbValueHex.Tag = true;
				this.tbValueHex.Text = "";
				this.tbValueHex.Enabled = false;
				this.tbValueDec.Text = "";
				this.tbValueDec.Enabled = false;
				tbValueHex.Tag = null;
			}
			else
			{
				llcdel.Enabled = true;
				origValue = currentValue = (short)wrapper.Constants[currentIndex];
				tbValueHex.Tag = true;
				this.tbValueHex.Text = "0x"+Helper.HexString((ushort)currentValue);
				this.tbValueHex.Enabled = true;
				this.tbValueDec.Text = currentValue.ToString();
				this.tbValueDec.Enabled = true;
				tbValueHex.Tag = null;
			}
			llccancel.Enabled = false;
		}

		private void tbvalue_TextChanged(object sender, System.EventArgs e)
		{
			if (tbValueHex.Tag != null) return; // Internal change
			try 
			{
				int newValue = Convert.ToInt16(tbValueHex.Text, 16);
				if (newValue == currentValue) return;
				currentValue = newValue;
			} 
			catch (Exception) 
			{ 
				currentValue = 0;
			}

			UpdateBconValue();
			llccancel.Enabled = true;

			tbValueHex.Tag = true;
			tbValueDec.Text = currentValue.ToString();
			tbValueHex.Tag = null;
		}

		private void tbdec_TextChanged(object sender, System.EventArgs e)
		{
			if (tbValueHex.Tag != null) return; // Internal change
			try 
			{
				int newValue = Convert.ToInt16(tbValueDec.Text);
				if (newValue == currentValue) return;
				currentValue = newValue;
			} 
			catch (Exception) 
			{ 
				currentValue = 0;
			}

			UpdateBconValue();
			llccancel.Enabled = true;

			tbValueHex.Tag = true;
			tbValueHex.Text = "0x"+Helper.HexString((ushort)currentValue);
			tbValueHex.Tag = null;
		}

		private void llccancel_Clicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			currentValue = origValue;
			UpdateBconValue();
			llccancel.Enabled = false;

			tbValueHex.Tag = true;
			tbValueHex.Text = "0x"+Helper.HexString((ushort)currentValue);
			tbValueDec.Text = currentValue.ToString();
			tbValueHex.Tag = null;
		}

		private void AddConstantClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			wrapper.Constants.Add((short)0);
			wrapper.Changed = true;
			btnCommit.Enabled = true;

			ListViewItem i = new ListViewItem("0x" + lvConstants.Items.Count.ToString("X"));
			i.SubItems.Add("0");
			lvConstants.Items.Add(i);

			currentIndex = -1;
			lvConstants.EnsureVisible(lvConstants.Items.Count - 1);
			lvConstants.Update();
		}

		private void DeleteConstantClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			int ci = currentIndex;
			wrapper.Constants.RemoveAt(currentIndex--);
			wrapper.Changed = true;
			btnCommit.Enabled = true;

			lvConstants.Items.RemoveAt(ci);
			for (int i = ci; i < wrapper.Constants.Count; i++)
				lvConstants.Items[i].Text = "0x" + i.ToString("X");
			lvConstants.Update();
			lvConstants.EnsureVisible(ci - 1);
		}

		private void btnCommit_Clicked(object sender, System.EventArgs e)
		{
			try 
			{
				wrapper.SynchronizeUserData();
				btnCommit.Enabled = false;
//				MessageBox.Show(Localization.Manager.GetString("commited"));
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}		
		}

	}
}
