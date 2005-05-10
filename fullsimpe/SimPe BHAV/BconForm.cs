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
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label44;
		private System.Windows.Forms.TextBox tbFilename;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.TextBox tbconstflag;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox tbvalue;
		private System.Windows.Forms.TextBox tbdec;
		private System.Windows.Forms.LinkLabel llcadd;
		private System.Windows.Forms.LinkLabel llcdel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel bconPanel;
		private System.Windows.Forms.ListBox lbConstants;
		private System.Windows.Forms.LinkLabel llccancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnCommit;
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


		#region Bcon
		private Bcon wrapper;
		private int currentIndex;
		private int origValue;
		private int currentValue;

		private void reindex()
		{
			for (int i=0; i< lbConstants.Items.Count; i++)
				((BconItem)lbConstants.Items[i]).Index = i;
			writeBackConstants();

			lbConstants.Items.Clear();
			foreach (BconItem i in wrapper.Constants)
				lbConstants.Items.Add(i);
		}

		private void writeBackConstants()
		{
			BconItem[] con = new BconItem[lbConstants.Items.Count];
			for (int i=0; i< con.Length; i++)
				con[i] = (BconItem)lbConstants.Items[i];
			wrapper.Constants = con;
			wrapper.Changed = true;
			btnCommit.Enabled = true;
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
			tbconstflag.Text = "0x"+Helper.HexString(wrapper.Flag);
			tbFilename.Tag = null;
			//label28 - static
			//lbConstants;
			lbConstants.Tag = true;
			lbConstants.Items.Clear();
			foreach (BconItem i in wrapper.Constants)
			{
				lbConstants.Items.Add(i);
			}
			lbConstants.Tag = null;
			if (lbConstants.Items.Count > 0) lbConstants.SelectedIndex = 0;
			else lbConstants_IndexChanged(null, null);
			//label18 - static
			//tbvalue - set by SelectedIndex change
			//tbdec - set by SelectedIndex change
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
			this.label27 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label44 = new System.Windows.Forms.Label();
			this.tbFilename = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.tbconstflag = new System.Windows.Forms.TextBox();
			this.label28 = new System.Windows.Forms.Label();
			this.lbConstants = new System.Windows.Forms.ListBox();
			this.tbdec = new System.Windows.Forms.TextBox();
			this.llcdel = new System.Windows.Forms.LinkLabel();
			this.llcadd = new System.Windows.Forms.LinkLabel();
			this.llccancel = new System.Windows.Forms.LinkLabel();
			this.tbvalue = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.bconPanel = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.btnCommit = new System.Windows.Forms.Button();
			this.panel2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.bconPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// label27
			// 
			this.label27.AccessibleDescription = resources.GetString("label27.AccessibleDescription");
			this.label27.AccessibleName = resources.GetString("label27.AccessibleName");
			this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label27.Anchor")));
			this.label27.AutoSize = ((bool)(resources.GetObject("label27.AutoSize")));
			this.label27.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label27.Dock")));
			this.label27.Enabled = ((bool)(resources.GetObject("label27.Enabled")));
			this.label27.Font = ((System.Drawing.Font)(resources.GetObject("label27.Font")));
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
			// panel2
			// 
			this.panel2.AccessibleDescription = resources.GetString("panel2.AccessibleDescription");
			this.panel2.AccessibleName = resources.GetString("panel2.AccessibleName");
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel2.Anchor")));
			this.panel2.AutoScroll = ((bool)(resources.GetObject("panel2.AutoScroll")));
			this.panel2.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel2.AutoScrollMargin")));
			this.panel2.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel2.AutoScrollMinSize")));
			this.panel2.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
			this.panel2.Controls.Add(this.label27);
			this.panel2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panel2.Dock")));
			this.panel2.Enabled = ((bool)(resources.GetObject("panel2.Enabled")));
			this.panel2.Font = ((System.Drawing.Font)(resources.GetObject("panel2.Font")));
			this.panel2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.panel2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panel2.ImeMode")));
			this.panel2.Location = ((System.Drawing.Point)(resources.GetObject("panel2.Location")));
			this.panel2.Name = "panel2";
			this.panel2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panel2.RightToLeft")));
			this.panel2.Size = ((System.Drawing.Size)(resources.GetObject("panel2.Size")));
			this.panel2.TabIndex = ((int)(resources.GetObject("panel2.TabIndex")));
			this.panel2.Text = resources.GetString("panel2.Text");
			this.panel2.Visible = ((bool)(resources.GetObject("panel2.Visible")));
			// 
			// label44
			// 
			this.label44.AccessibleDescription = resources.GetString("label44.AccessibleDescription");
			this.label44.AccessibleName = resources.GetString("label44.AccessibleName");
			this.label44.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label44.Anchor")));
			this.label44.AutoSize = ((bool)(resources.GetObject("label44.AutoSize")));
			this.label44.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label44.Dock")));
			this.label44.Enabled = ((bool)(resources.GetObject("label44.Enabled")));
			this.label44.Font = ((System.Drawing.Font)(resources.GetObject("label44.Font")));
			this.label44.Image = ((System.Drawing.Image)(resources.GetObject("label44.Image")));
			this.label44.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label44.ImageAlign")));
			this.label44.ImageIndex = ((int)(resources.GetObject("label44.ImageIndex")));
			this.label44.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label44.ImeMode")));
			this.label44.Location = ((System.Drawing.Point)(resources.GetObject("label44.Location")));
			this.label44.Name = "label44";
			this.label44.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label44.RightToLeft")));
			this.label44.Size = ((System.Drawing.Size)(resources.GetObject("label44.Size")));
			this.label44.TabIndex = ((int)(resources.GetObject("label44.TabIndex")));
			this.label44.Text = resources.GetString("label44.Text");
			this.label44.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label44.TextAlign")));
			this.label44.Visible = ((bool)(resources.GetObject("label44.Visible")));
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
			// label22
			// 
			this.label22.AccessibleDescription = resources.GetString("label22.AccessibleDescription");
			this.label22.AccessibleName = resources.GetString("label22.AccessibleName");
			this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label22.Anchor")));
			this.label22.AutoSize = ((bool)(resources.GetObject("label22.AutoSize")));
			this.label22.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label22.Dock")));
			this.label22.Enabled = ((bool)(resources.GetObject("label22.Enabled")));
			this.label22.Font = ((System.Drawing.Font)(resources.GetObject("label22.Font")));
			this.label22.Image = ((System.Drawing.Image)(resources.GetObject("label22.Image")));
			this.label22.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label22.ImageAlign")));
			this.label22.ImageIndex = ((int)(resources.GetObject("label22.ImageIndex")));
			this.label22.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label22.ImeMode")));
			this.label22.Location = ((System.Drawing.Point)(resources.GetObject("label22.Location")));
			this.label22.Name = "label22";
			this.label22.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label22.RightToLeft")));
			this.label22.Size = ((System.Drawing.Size)(resources.GetObject("label22.Size")));
			this.label22.TabIndex = ((int)(resources.GetObject("label22.TabIndex")));
			this.label22.Text = resources.GetString("label22.Text");
			this.label22.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label22.TextAlign")));
			this.label22.Visible = ((bool)(resources.GetObject("label22.Visible")));
			// 
			// tbconstflag
			// 
			this.tbconstflag.AccessibleDescription = resources.GetString("tbconstflag.AccessibleDescription");
			this.tbconstflag.AccessibleName = resources.GetString("tbconstflag.AccessibleName");
			this.tbconstflag.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbconstflag.Anchor")));
			this.tbconstflag.AutoSize = ((bool)(resources.GetObject("tbconstflag.AutoSize")));
			this.tbconstflag.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbconstflag.BackgroundImage")));
			this.tbconstflag.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbconstflag.Dock")));
			this.tbconstflag.Enabled = ((bool)(resources.GetObject("tbconstflag.Enabled")));
			this.tbconstflag.Font = ((System.Drawing.Font)(resources.GetObject("tbconstflag.Font")));
			this.tbconstflag.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbconstflag.ImeMode")));
			this.tbconstflag.Location = ((System.Drawing.Point)(resources.GetObject("tbconstflag.Location")));
			this.tbconstflag.MaxLength = ((int)(resources.GetObject("tbconstflag.MaxLength")));
			this.tbconstflag.Multiline = ((bool)(resources.GetObject("tbconstflag.Multiline")));
			this.tbconstflag.Name = "tbconstflag";
			this.tbconstflag.PasswordChar = ((char)(resources.GetObject("tbconstflag.PasswordChar")));
			this.tbconstflag.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbconstflag.RightToLeft")));
			this.tbconstflag.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbconstflag.ScrollBars")));
			this.tbconstflag.Size = ((System.Drawing.Size)(resources.GetObject("tbconstflag.Size")));
			this.tbconstflag.TabIndex = ((int)(resources.GetObject("tbconstflag.TabIndex")));
			this.tbconstflag.Text = resources.GetString("tbconstflag.Text");
			this.tbconstflag.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbconstflag.TextAlign")));
			this.tbconstflag.Visible = ((bool)(resources.GetObject("tbconstflag.Visible")));
			this.tbconstflag.WordWrap = ((bool)(resources.GetObject("tbconstflag.WordWrap")));
			this.tbconstflag.TextChanged += new System.EventHandler(this.tbFilename_TextChanged);
			// 
			// label28
			// 
			this.label28.AccessibleDescription = resources.GetString("label28.AccessibleDescription");
			this.label28.AccessibleName = resources.GetString("label28.AccessibleName");
			this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label28.Anchor")));
			this.label28.AutoSize = ((bool)(resources.GetObject("label28.AutoSize")));
			this.label28.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label28.Dock")));
			this.label28.Enabled = ((bool)(resources.GetObject("label28.Enabled")));
			this.label28.Font = ((System.Drawing.Font)(resources.GetObject("label28.Font")));
			this.label28.Image = ((System.Drawing.Image)(resources.GetObject("label28.Image")));
			this.label28.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label28.ImageAlign")));
			this.label28.ImageIndex = ((int)(resources.GetObject("label28.ImageIndex")));
			this.label28.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label28.ImeMode")));
			this.label28.Location = ((System.Drawing.Point)(resources.GetObject("label28.Location")));
			this.label28.Name = "label28";
			this.label28.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label28.RightToLeft")));
			this.label28.Size = ((System.Drawing.Size)(resources.GetObject("label28.Size")));
			this.label28.TabIndex = ((int)(resources.GetObject("label28.TabIndex")));
			this.label28.Text = resources.GetString("label28.Text");
			this.label28.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label28.TextAlign")));
			this.label28.Visible = ((bool)(resources.GetObject("label28.Visible")));
			// 
			// lbConstants
			// 
			this.lbConstants.AccessibleDescription = resources.GetString("lbConstants.AccessibleDescription");
			this.lbConstants.AccessibleName = resources.GetString("lbConstants.AccessibleName");
			this.lbConstants.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbConstants.Anchor")));
			this.lbConstants.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lbConstants.BackgroundImage")));
			this.lbConstants.ColumnWidth = ((int)(resources.GetObject("lbConstants.ColumnWidth")));
			this.lbConstants.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbConstants.Dock")));
			this.lbConstants.Enabled = ((bool)(resources.GetObject("lbConstants.Enabled")));
			this.lbConstants.Font = ((System.Drawing.Font)(resources.GetObject("lbConstants.Font")));
			this.lbConstants.HorizontalExtent = ((int)(resources.GetObject("lbConstants.HorizontalExtent")));
			this.lbConstants.HorizontalScrollbar = ((bool)(resources.GetObject("lbConstants.HorizontalScrollbar")));
			this.lbConstants.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbConstants.ImeMode")));
			this.lbConstants.IntegralHeight = ((bool)(resources.GetObject("lbConstants.IntegralHeight")));
			this.lbConstants.ItemHeight = ((int)(resources.GetObject("lbConstants.ItemHeight")));
			this.lbConstants.Location = ((System.Drawing.Point)(resources.GetObject("lbConstants.Location")));
			this.lbConstants.Name = "lbConstants";
			this.lbConstants.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbConstants.RightToLeft")));
			this.lbConstants.ScrollAlwaysVisible = ((bool)(resources.GetObject("lbConstants.ScrollAlwaysVisible")));
			this.lbConstants.Size = ((System.Drawing.Size)(resources.GetObject("lbConstants.Size")));
			this.lbConstants.TabIndex = ((int)(resources.GetObject("lbConstants.TabIndex")));
			this.lbConstants.Visible = ((bool)(resources.GetObject("lbConstants.Visible")));
			this.lbConstants.SelectedIndexChanged += new System.EventHandler(this.lbConstants_IndexChanged);
			// 
			// tbdec
			// 
			this.tbdec.AccessibleDescription = resources.GetString("tbdec.AccessibleDescription");
			this.tbdec.AccessibleName = resources.GetString("tbdec.AccessibleName");
			this.tbdec.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbdec.Anchor")));
			this.tbdec.AutoSize = ((bool)(resources.GetObject("tbdec.AutoSize")));
			this.tbdec.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbdec.BackgroundImage")));
			this.tbdec.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbdec.Dock")));
			this.tbdec.Enabled = ((bool)(resources.GetObject("tbdec.Enabled")));
			this.tbdec.Font = ((System.Drawing.Font)(resources.GetObject("tbdec.Font")));
			this.tbdec.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbdec.ImeMode")));
			this.tbdec.Location = ((System.Drawing.Point)(resources.GetObject("tbdec.Location")));
			this.tbdec.MaxLength = ((int)(resources.GetObject("tbdec.MaxLength")));
			this.tbdec.Multiline = ((bool)(resources.GetObject("tbdec.Multiline")));
			this.tbdec.Name = "tbdec";
			this.tbdec.PasswordChar = ((char)(resources.GetObject("tbdec.PasswordChar")));
			this.tbdec.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbdec.RightToLeft")));
			this.tbdec.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbdec.ScrollBars")));
			this.tbdec.Size = ((System.Drawing.Size)(resources.GetObject("tbdec.Size")));
			this.tbdec.TabIndex = ((int)(resources.GetObject("tbdec.TabIndex")));
			this.tbdec.Text = resources.GetString("tbdec.Text");
			this.tbdec.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbdec.TextAlign")));
			this.tbdec.Visible = ((bool)(resources.GetObject("tbdec.Visible")));
			this.tbdec.WordWrap = ((bool)(resources.GetObject("tbdec.WordWrap")));
			this.tbdec.TextChanged += new System.EventHandler(this.tbdec_TextChanged);
			this.tbdec.Leave += new System.EventHandler(this.AutoCommit);
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
			// tbvalue
			// 
			this.tbvalue.AccessibleDescription = resources.GetString("tbvalue.AccessibleDescription");
			this.tbvalue.AccessibleName = resources.GetString("tbvalue.AccessibleName");
			this.tbvalue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbvalue.Anchor")));
			this.tbvalue.AutoSize = ((bool)(resources.GetObject("tbvalue.AutoSize")));
			this.tbvalue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbvalue.BackgroundImage")));
			this.tbvalue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbvalue.Dock")));
			this.tbvalue.Enabled = ((bool)(resources.GetObject("tbvalue.Enabled")));
			this.tbvalue.Font = ((System.Drawing.Font)(resources.GetObject("tbvalue.Font")));
			this.tbvalue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbvalue.ImeMode")));
			this.tbvalue.Location = ((System.Drawing.Point)(resources.GetObject("tbvalue.Location")));
			this.tbvalue.MaxLength = ((int)(resources.GetObject("tbvalue.MaxLength")));
			this.tbvalue.Multiline = ((bool)(resources.GetObject("tbvalue.Multiline")));
			this.tbvalue.Name = "tbvalue";
			this.tbvalue.PasswordChar = ((char)(resources.GetObject("tbvalue.PasswordChar")));
			this.tbvalue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbvalue.RightToLeft")));
			this.tbvalue.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbvalue.ScrollBars")));
			this.tbvalue.Size = ((System.Drawing.Size)(resources.GetObject("tbvalue.Size")));
			this.tbvalue.TabIndex = ((int)(resources.GetObject("tbvalue.TabIndex")));
			this.tbvalue.Text = resources.GetString("tbvalue.Text");
			this.tbvalue.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbvalue.TextAlign")));
			this.tbvalue.Visible = ((bool)(resources.GetObject("tbvalue.Visible")));
			this.tbvalue.WordWrap = ((bool)(resources.GetObject("tbvalue.WordWrap")));
			this.tbvalue.TextChanged += new System.EventHandler(this.tbvalue_TextChanged);
			this.tbvalue.Leave += new System.EventHandler(this.AutoCommit);
			// 
			// label18
			// 
			this.label18.AccessibleDescription = resources.GetString("label18.AccessibleDescription");
			this.label18.AccessibleName = resources.GetString("label18.AccessibleName");
			this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label18.Anchor")));
			this.label18.AutoSize = ((bool)(resources.GetObject("label18.AutoSize")));
			this.label18.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label18.Dock")));
			this.label18.Enabled = ((bool)(resources.GetObject("label18.Enabled")));
			this.label18.Font = ((System.Drawing.Font)(resources.GetObject("label18.Font")));
			this.label18.Image = ((System.Drawing.Image)(resources.GetObject("label18.Image")));
			this.label18.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label18.ImageAlign")));
			this.label18.ImageIndex = ((int)(resources.GetObject("label18.ImageIndex")));
			this.label18.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label18.ImeMode")));
			this.label18.Location = ((System.Drawing.Point)(resources.GetObject("label18.Location")));
			this.label18.Name = "label18";
			this.label18.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label18.RightToLeft")));
			this.label18.Size = ((System.Drawing.Size)(resources.GetObject("label18.Size")));
			this.label18.TabIndex = ((int)(resources.GetObject("label18.TabIndex")));
			this.label18.Text = resources.GetString("label18.Text");
			this.label18.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label18.TextAlign")));
			this.label18.Visible = ((bool)(resources.GetObject("label18.Visible")));
			// 
			// groupBox1
			// 
			this.groupBox1.AccessibleDescription = resources.GetString("groupBox1.AccessibleDescription");
			this.groupBox1.AccessibleName = resources.GetString("groupBox1.AccessibleName");
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox1.Anchor")));
			this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
			this.groupBox1.Controls.Add(this.tbdec);
			this.groupBox1.Controls.Add(this.llcdel);
			this.groupBox1.Controls.Add(this.llcadd);
			this.groupBox1.Controls.Add(this.llccancel);
			this.groupBox1.Controls.Add(this.tbvalue);
			this.groupBox1.Controls.Add(this.label18);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("groupBox1.Dock")));
			this.groupBox1.Enabled = ((bool)(resources.GetObject("groupBox1.Enabled")));
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Font = ((System.Drawing.Font)(resources.GetObject("groupBox1.Font")));
			this.groupBox1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("groupBox1.ImeMode")));
			this.groupBox1.Location = ((System.Drawing.Point)(resources.GetObject("groupBox1.Location")));
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("groupBox1.RightToLeft")));
			this.groupBox1.Size = ((System.Drawing.Size)(resources.GetObject("groupBox1.Size")));
			this.groupBox1.TabIndex = ((int)(resources.GetObject("groupBox1.TabIndex")));
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = resources.GetString("groupBox1.Text");
			this.groupBox1.Visible = ((bool)(resources.GetObject("groupBox1.Visible")));
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
			this.bconPanel.Controls.Add(this.btnCommit);
			this.bconPanel.Controls.Add(this.label44);
			this.bconPanel.Controls.Add(this.tbFilename);
			this.bconPanel.Controls.Add(this.groupBox1);
			this.bconPanel.Controls.Add(this.tbconstflag);
			this.bconPanel.Controls.Add(this.label22);
			this.bconPanel.Controls.Add(this.lbConstants);
			this.bconPanel.Controls.Add(this.panel2);
			this.bconPanel.Controls.Add(this.label28);
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
			this.panel2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.bconPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}


		private void tbFilename_TextChanged(object sender, System.EventArgs e)
		{
			if (tbFilename.Tag != null) return; // Internal change

			try 
			{
				tbFilename.Tag = true;

				wrapper.FileName = tbFilename.Text;
				wrapper.Flag = Convert.ToByte(tbconstflag.Text, 16);
				wrapper.Changed = true;
				btnCommit.Enabled = true;
			}
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			} 
			finally 
			{
				tbFilename.Tag = null;
			}
		}

		private void lbConstants_IndexChanged(object sender, System.EventArgs e)
		{
			if (lbConstants.Tag != null) return; // Internal change

			lbConstants.Tag = true;
			if (lbConstants.SelectedIndex >= lbConstants.Items.Count)
				lbConstants.SelectedIndex = lbConstants.Items.Count - 1;
			lbConstants.Tag = null;

			if (currentIndex == lbConstants.SelectedIndex) return; // no change
			currentIndex = lbConstants.SelectedIndex;

			if (currentIndex < 0) 
			{
				llcdel.Enabled = false;
				origValue = currentValue = -1;
				tbvalue.Tag = true;
				this.tbvalue.Text = "";
				this.tbvalue.Enabled = false;
				this.tbdec.Text = "";
				this.tbdec.Enabled = false;
				tbvalue.Tag = null;
			}
			else
			{
				llcdel.Enabled = true;
				origValue = currentValue = ((BconItem)lbConstants.Items[currentIndex]).Value;
				tbvalue.Tag = true;
				this.tbvalue.Text = "0x"+Helper.HexString((ushort)currentValue);
				this.tbvalue.Enabled = true;
				this.tbdec.Text = currentValue.ToString();
				this.tbdec.Enabled = true;
				tbvalue.Tag = null;
			}
			llccancel.Enabled = false;
		}

		private void tbvalue_TextChanged(object sender, System.EventArgs e)
		{
			if (tbvalue.Tag != null) return; // Internal change
			try 
			{
				int newValue = Convert.ToInt16(tbvalue.Text, 16);
				if (newValue == currentValue) return;
				currentValue = newValue;
			} 
			catch (Exception) 
			{ 
				currentValue = 0;
			}

			AutoCommit(null, null);
			llccancel.Enabled = true;

			tbvalue.Tag = true;
			tbdec.Text = currentValue.ToString();
			tbvalue.Tag = null;
		}

		private void tbdec_TextChanged(object sender, System.EventArgs e)
		{
			if (tbvalue.Tag != null) return; // Internal change
			try 
			{
				int newValue = Convert.ToInt16(tbdec.Text);
				if (newValue == currentValue) return;
				currentValue = newValue;
			} 
			catch (Exception) 
			{ 
				currentValue = 0;
			}

			AutoCommit(null, null);
			llccancel.Enabled = true;

			tbvalue.Tag = true;
			tbvalue.Text = "0x"+Helper.HexString((ushort)currentValue);
			tbvalue.Tag = null;
		}

		private void AutoCommit(object sender, System.EventArgs e)
		{
			if (currentValue == origValue) return;

			lbConstants.Tag = true;
			lbConstants.Items.RemoveAt(currentIndex);
			lbConstants.Items.Insert(currentIndex, new BconItem((short)currentValue, currentIndex, wrapper));
			lbConstants.Update();
			lbConstants.SelectedIndex = currentIndex;
			lbConstants.Tag = null;

			writeBackConstants();
		}

		private void llccancel_Clicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			currentValue = origValue;
			AutoCommit(null, null);
			llccancel.Enabled = false;

			tbvalue.Tag = true;
			tbvalue.Text = "0x"+Helper.HexString((ushort)currentValue);
			tbdec.Text = currentValue.ToString();
			tbvalue.Tag = null;
		}

		private void AddConstantClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			lbConstants.Tag = true;
			lbConstants.Items.Add(new BconItem(0, lbConstants.Items.Count, wrapper));
			reindex();
			lbConstants.Tag = null;

			currentIndex = -1;
			lbConstants.SelectedIndex = lbConstants.Items.Count - 1;
			lbConstants.Update();
		}

		private void DeleteConstantClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			int ci = currentIndex;

			lbConstants.Tag = true;
			lbConstants.Items.Remove(lbConstants.Items[currentIndex]);
			reindex();
			lbConstants.Tag = null;

			currentIndex = lbConstants.Items.Count;
			if (ci > 0) lbConstants.SelectedIndex = ci - 1;
			else if (lbConstants.Items.Count > 0) lbConstants.SelectedIndex = 0;
			else lbConstants_IndexChanged(null, null);
			lbConstants.Update();
		}

		private void btnCommit_Clicked(object sender, System.EventArgs e)
		{
			writeBackConstants(); // should not be required!

			try 
			{
				wrapper.SynchronizeUserData();
				btnCommit.Enabled = false;
				MessageBox.Show(Localization.Manager.GetString("commited"));
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}		
		}

		#endregion
	}
}
