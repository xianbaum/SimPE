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
using pjse;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for BhavForm.
	/// </summary>
	public class BhavForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel pnHeading;
		private System.Windows.Forms.Label lbFilename;
		private System.Windows.Forms.Label lbFormat;
		private System.Windows.Forms.Label lbType;
		private System.Windows.Forms.Label lbLocalC;
		private System.Windows.Forms.Label lbFlags;
		private System.Windows.Forms.Label lbReserved;
		private System.Windows.Forms.TextBox tbFilename;
		private System.Windows.Forms.TextBox tbFormat;
		private System.Windows.Forms.TextBox tbType;
		private System.Windows.Forms.TextBox tbArgC;
		private System.Windows.Forms.TextBox tbLocalC;
		private System.Windows.Forms.TextBox tbFlags;
		private System.Windows.Forms.TextBox tbReserved;
		private System.Windows.Forms.GroupBox gbInstruction;
		private System.Windows.Forms.Panel bhavPanel;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.Button btnSort;
		private System.Windows.Forms.Label lbUpDown;
		private System.Windows.Forms.TextBox tbLines;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Button btnDel;
		private System.Windows.Forms.Button btnAdd;
		private SimPe.PackedFiles.UserInterface.BhavInstListControl pnflowcontainer;
		private System.Windows.Forms.GroupBox gbMove;
		private System.Windows.Forms.Label lbArgC;
		private SimPe.PackedFiles.UserInterface.BhavInstPanel bhavInstPanel1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
       
		public BhavForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.Tag = "Normal"; // Used by SetReadOnly, overridden by caller if desired

			Control[] cs = {
				tbLocalC, lbLocalC, tbArgC, lbArgC, tbReserved, lbReserved,
				tbFlags, lbFlags, tbType, lbType, tbFormat, lbFormat };
			int left = this.bhavPanel.Width;
			for (int i = 0; i < cs.Length; i++)
				left = cs[i].Left = left - (cs[i].Width + 4);
			this.lbFilename.Left = 4;
			this.tbFilename.Left = this.lbFilename.Right + 4;

			TextBox[] iob = { tbType };
			alHex8 = new ArrayList(iob);

			TextBox[] w = { tbFormat ,tbFlags };
			alHex16 = new ArrayList(w);

			TextBox[] db = { tbArgC ,tbLocalC };
			alDec8 = new ArrayList(db);

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

		
		#region BhavForm
		private Bhav wrapper;
		private ArrayList alHex8;
		private ArrayList alHex16;
		private ArrayList alDec8;

		private void SetReadOnly(bool state) 
		{
			if (((string)this.Tag).Equals("Popup"))
			{
				// make it very clear it's read only
				tbFilename.Enabled = tbFormat.Enabled = tbType.Enabled = tbArgC.Enabled = 
					tbLocalC.Enabled = tbFlags.Enabled = tbReserved.Enabled =
					btnSort.Visible = btnCommit.Visible = gbMove.Visible = 
					btnDel.Visible = btnAdd.Visible = 
					false;
				state = true;
			}
			else
			{
				btnUp.Enabled = btnDown.Enabled = btnDel.Enabled = btnAdd.Enabled = !state;
				tbLines.ReadOnly = state;
			}

			bhavInstPanel1.ReadOnly = state;
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
				return bhavPanel;
			}
		}

		/// <summary>
		/// Is called by SimPe (through the Wrapper) when the Panel is going to be displayed, so
		/// you should updatet the Data displayed by the Panel with the Attributes stored in the
		/// passed Wrapper.
		/// </summary>
		/// <remarks>attr.Tag is used to let TextChanged event handlers know the change is being
		/// made internally rather than by the users.</remarks>
		/// <param name="wrapper">The Attributes of this Wrapper have to be displayed</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (Bhav) wrp;
			wrapper.WrapperChanged += new System.EventHandler(this.WrapperChanged);

			tbFilename.Text = wrapper.FileName;
			tbArgC.Text = wrapper.Header.ArgumentCount.ToString();
			tbFlags.Text = "0x"+Helper.HexString(wrapper.Header.Flags);
			tbFormat.Text = "0x"+Helper.HexString(wrapper.Header.Format);
			tbLocalC.Text = wrapper.Header.LocalVarCount.ToString();
			tbType.Text = "0x"+Helper.HexString(wrapper.Header.Type);
			tbReserved.Text = "0x"+Helper.HexString(wrapper.Header.Zero);

			this.btnCommit.Enabled = wrapper.Changed;

			SetReadOnly(false);
			this.pnflowcontainer.UpdateGUI(wrapper, bhavInstPanel1);
		}

		private void WrapperChanged(object sender, System.EventArgs e)
		{
			this.btnCommit.Enabled = true;
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BhavForm));
			this.label1 = new System.Windows.Forms.Label();
			this.gbInstruction = new System.Windows.Forms.GroupBox();
			this.bhavInstPanel1 = new SimPe.PackedFiles.UserInterface.BhavInstPanel();
			this.tbFilename = new System.Windows.Forms.TextBox();
			this.lbFilename = new System.Windows.Forms.Label();
			this.tbReserved = new System.Windows.Forms.TextBox();
			this.tbLocalC = new System.Windows.Forms.TextBox();
			this.tbFlags = new System.Windows.Forms.TextBox();
			this.tbArgC = new System.Windows.Forms.TextBox();
			this.tbType = new System.Windows.Forms.TextBox();
			this.tbFormat = new System.Windows.Forms.TextBox();
			this.lbReserved = new System.Windows.Forms.Label();
			this.lbFlags = new System.Windows.Forms.Label();
			this.lbType = new System.Windows.Forms.Label();
			this.lbLocalC = new System.Windows.Forms.Label();
			this.lbArgC = new System.Windows.Forms.Label();
			this.lbFormat = new System.Windows.Forms.Label();
			this.pnHeading = new System.Windows.Forms.Panel();
			this.bhavPanel = new System.Windows.Forms.Panel();
			this.pnflowcontainer = new SimPe.PackedFiles.UserInterface.BhavInstListControl();
			this.btnDel = new System.Windows.Forms.Button();
			this.gbMove = new System.Windows.Forms.GroupBox();
			this.btnUp = new System.Windows.Forms.Button();
			this.btnDown = new System.Windows.Forms.Button();
			this.lbUpDown = new System.Windows.Forms.Label();
			this.tbLines = new System.Windows.Forms.TextBox();
			this.btnSort = new System.Windows.Forms.Button();
			this.btnCommit = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.gbInstruction.SuspendLayout();
			this.pnHeading.SuspendLayout();
			this.bhavPanel.SuspendLayout();
			this.gbMove.SuspendLayout();
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
			this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
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
			// gbInstruction
			// 
			this.gbInstruction.AccessibleDescription = resources.GetString("gbInstruction.AccessibleDescription");
			this.gbInstruction.AccessibleName = resources.GetString("gbInstruction.AccessibleName");
			this.gbInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gbInstruction.Anchor")));
			this.gbInstruction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gbInstruction.BackgroundImage")));
			this.gbInstruction.Controls.Add(this.bhavInstPanel1);
			this.gbInstruction.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("gbInstruction.Dock")));
			this.gbInstruction.Enabled = ((bool)(resources.GetObject("gbInstruction.Enabled")));
			this.gbInstruction.Font = ((System.Drawing.Font)(resources.GetObject("gbInstruction.Font")));
			this.gbInstruction.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gbInstruction.ImeMode")));
			this.gbInstruction.Location = ((System.Drawing.Point)(resources.GetObject("gbInstruction.Location")));
			this.gbInstruction.Name = "gbInstruction";
			this.gbInstruction.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("gbInstruction.RightToLeft")));
			this.gbInstruction.Size = ((System.Drawing.Size)(resources.GetObject("gbInstruction.Size")));
			this.gbInstruction.TabIndex = ((int)(resources.GetObject("gbInstruction.TabIndex")));
			this.gbInstruction.TabStop = false;
			this.gbInstruction.Text = resources.GetString("gbInstruction.Text");
			this.gbInstruction.Visible = ((bool)(resources.GetObject("gbInstruction.Visible")));
			// 
			// bhavInstPanel1
			// 
			this.bhavInstPanel1.AccessibleDescription = resources.GetString("bhavInstPanel1.AccessibleDescription");
			this.bhavInstPanel1.AccessibleName = resources.GetString("bhavInstPanel1.AccessibleName");
			this.bhavInstPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("bhavInstPanel1.Anchor")));
			this.bhavInstPanel1.AutoScroll = ((bool)(resources.GetObject("bhavInstPanel1.AutoScroll")));
			this.bhavInstPanel1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("bhavInstPanel1.AutoScrollMargin")));
			this.bhavInstPanel1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("bhavInstPanel1.AutoScrollMinSize")));
			this.bhavInstPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bhavInstPanel1.BackgroundImage")));
			this.bhavInstPanel1.CurrentInst = null;
			this.bhavInstPanel1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("bhavInstPanel1.Dock")));
			this.bhavInstPanel1.Enabled = ((bool)(resources.GetObject("bhavInstPanel1.Enabled")));
			this.bhavInstPanel1.Font = ((System.Drawing.Font)(resources.GetObject("bhavInstPanel1.Font")));
			this.bhavInstPanel1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("bhavInstPanel1.ImeMode")));
			this.bhavInstPanel1.Location = ((System.Drawing.Point)(resources.GetObject("bhavInstPanel1.Location")));
			this.bhavInstPanel1.Name = "bhavInstPanel1";
			this.bhavInstPanel1.ReadOnly = true;
			this.bhavInstPanel1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("bhavInstPanel1.RightToLeft")));
			this.bhavInstPanel1.Size = ((System.Drawing.Size)(resources.GetObject("bhavInstPanel1.Size")));
			this.bhavInstPanel1.TabIndex = ((int)(resources.GetObject("bhavInstPanel1.TabIndex")));
			this.bhavInstPanel1.Visible = ((bool)(resources.GetObject("bhavInstPanel1.Visible")));
			this.bhavInstPanel1.Cancelled += new System.EventHandler(this.bhavInstPanel1_Cancelled);
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
			this.tbFilename.Validated += new System.EventHandler(this.tbFilename_Validated);
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
			// tbReserved
			// 
			this.tbReserved.AccessibleDescription = resources.GetString("tbReserved.AccessibleDescription");
			this.tbReserved.AccessibleName = resources.GetString("tbReserved.AccessibleName");
			this.tbReserved.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbReserved.Anchor")));
			this.tbReserved.AutoSize = ((bool)(resources.GetObject("tbReserved.AutoSize")));
			this.tbReserved.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbReserved.BackgroundImage")));
			this.tbReserved.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbReserved.Dock")));
			this.tbReserved.Enabled = ((bool)(resources.GetObject("tbReserved.Enabled")));
			this.tbReserved.Font = ((System.Drawing.Font)(resources.GetObject("tbReserved.Font")));
			this.tbReserved.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbReserved.ImeMode")));
			this.tbReserved.Location = ((System.Drawing.Point)(resources.GetObject("tbReserved.Location")));
			this.tbReserved.MaxLength = ((int)(resources.GetObject("tbReserved.MaxLength")));
			this.tbReserved.Multiline = ((bool)(resources.GetObject("tbReserved.Multiline")));
			this.tbReserved.Name = "tbReserved";
			this.tbReserved.PasswordChar = ((char)(resources.GetObject("tbReserved.PasswordChar")));
			this.tbReserved.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbReserved.RightToLeft")));
			this.tbReserved.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbReserved.ScrollBars")));
			this.tbReserved.Size = ((System.Drawing.Size)(resources.GetObject("tbReserved.Size")));
			this.tbReserved.TabIndex = ((int)(resources.GetObject("tbReserved.TabIndex")));
			this.tbReserved.Text = resources.GetString("tbReserved.Text");
			this.tbReserved.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbReserved.TextAlign")));
			this.tbReserved.Visible = ((bool)(resources.GetObject("tbReserved.Visible")));
			this.tbReserved.WordWrap = ((bool)(resources.GetObject("tbReserved.WordWrap")));
			this.tbReserved.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbReserved.Validated += new System.EventHandler(this.hex16_Validated);
			// 
			// tbLocalC
			// 
			this.tbLocalC.AccessibleDescription = resources.GetString("tbLocalC.AccessibleDescription");
			this.tbLocalC.AccessibleName = resources.GetString("tbLocalC.AccessibleName");
			this.tbLocalC.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbLocalC.Anchor")));
			this.tbLocalC.AutoSize = ((bool)(resources.GetObject("tbLocalC.AutoSize")));
			this.tbLocalC.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbLocalC.BackgroundImage")));
			this.tbLocalC.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbLocalC.Dock")));
			this.tbLocalC.Enabled = ((bool)(resources.GetObject("tbLocalC.Enabled")));
			this.tbLocalC.Font = ((System.Drawing.Font)(resources.GetObject("tbLocalC.Font")));
			this.tbLocalC.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbLocalC.ImeMode")));
			this.tbLocalC.Location = ((System.Drawing.Point)(resources.GetObject("tbLocalC.Location")));
			this.tbLocalC.MaxLength = ((int)(resources.GetObject("tbLocalC.MaxLength")));
			this.tbLocalC.Multiline = ((bool)(resources.GetObject("tbLocalC.Multiline")));
			this.tbLocalC.Name = "tbLocalC";
			this.tbLocalC.PasswordChar = ((char)(resources.GetObject("tbLocalC.PasswordChar")));
			this.tbLocalC.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbLocalC.RightToLeft")));
			this.tbLocalC.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbLocalC.ScrollBars")));
			this.tbLocalC.Size = ((System.Drawing.Size)(resources.GetObject("tbLocalC.Size")));
			this.tbLocalC.TabIndex = ((int)(resources.GetObject("tbLocalC.TabIndex")));
			this.tbLocalC.Text = resources.GetString("tbLocalC.Text");
			this.tbLocalC.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbLocalC.TextAlign")));
			this.tbLocalC.Visible = ((bool)(resources.GetObject("tbLocalC.Visible")));
			this.tbLocalC.WordWrap = ((bool)(resources.GetObject("tbLocalC.WordWrap")));
			this.tbLocalC.Validating += new System.ComponentModel.CancelEventHandler(this.dec8_Validating);
			this.tbLocalC.Validated += new System.EventHandler(this.dec8_Validated);
			// 
			// tbFlags
			// 
			this.tbFlags.AccessibleDescription = resources.GetString("tbFlags.AccessibleDescription");
			this.tbFlags.AccessibleName = resources.GetString("tbFlags.AccessibleName");
			this.tbFlags.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbFlags.Anchor")));
			this.tbFlags.AutoSize = ((bool)(resources.GetObject("tbFlags.AutoSize")));
			this.tbFlags.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbFlags.BackgroundImage")));
			this.tbFlags.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbFlags.Dock")));
			this.tbFlags.Enabled = ((bool)(resources.GetObject("tbFlags.Enabled")));
			this.tbFlags.Font = ((System.Drawing.Font)(resources.GetObject("tbFlags.Font")));
			this.tbFlags.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbFlags.ImeMode")));
			this.tbFlags.Location = ((System.Drawing.Point)(resources.GetObject("tbFlags.Location")));
			this.tbFlags.MaxLength = ((int)(resources.GetObject("tbFlags.MaxLength")));
			this.tbFlags.Multiline = ((bool)(resources.GetObject("tbFlags.Multiline")));
			this.tbFlags.Name = "tbFlags";
			this.tbFlags.PasswordChar = ((char)(resources.GetObject("tbFlags.PasswordChar")));
			this.tbFlags.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbFlags.RightToLeft")));
			this.tbFlags.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbFlags.ScrollBars")));
			this.tbFlags.Size = ((System.Drawing.Size)(resources.GetObject("tbFlags.Size")));
			this.tbFlags.TabIndex = ((int)(resources.GetObject("tbFlags.TabIndex")));
			this.tbFlags.Text = resources.GetString("tbFlags.Text");
			this.tbFlags.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbFlags.TextAlign")));
			this.tbFlags.Visible = ((bool)(resources.GetObject("tbFlags.Visible")));
			this.tbFlags.WordWrap = ((bool)(resources.GetObject("tbFlags.WordWrap")));
			this.tbFlags.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbFlags.Validated += new System.EventHandler(this.hex16_Validated);
			// 
			// tbArgC
			// 
			this.tbArgC.AccessibleDescription = resources.GetString("tbArgC.AccessibleDescription");
			this.tbArgC.AccessibleName = resources.GetString("tbArgC.AccessibleName");
			this.tbArgC.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbArgC.Anchor")));
			this.tbArgC.AutoSize = ((bool)(resources.GetObject("tbArgC.AutoSize")));
			this.tbArgC.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbArgC.BackgroundImage")));
			this.tbArgC.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbArgC.Dock")));
			this.tbArgC.Enabled = ((bool)(resources.GetObject("tbArgC.Enabled")));
			this.tbArgC.Font = ((System.Drawing.Font)(resources.GetObject("tbArgC.Font")));
			this.tbArgC.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbArgC.ImeMode")));
			this.tbArgC.Location = ((System.Drawing.Point)(resources.GetObject("tbArgC.Location")));
			this.tbArgC.MaxLength = ((int)(resources.GetObject("tbArgC.MaxLength")));
			this.tbArgC.Multiline = ((bool)(resources.GetObject("tbArgC.Multiline")));
			this.tbArgC.Name = "tbArgC";
			this.tbArgC.PasswordChar = ((char)(resources.GetObject("tbArgC.PasswordChar")));
			this.tbArgC.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbArgC.RightToLeft")));
			this.tbArgC.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbArgC.ScrollBars")));
			this.tbArgC.Size = ((System.Drawing.Size)(resources.GetObject("tbArgC.Size")));
			this.tbArgC.TabIndex = ((int)(resources.GetObject("tbArgC.TabIndex")));
			this.tbArgC.Text = resources.GetString("tbArgC.Text");
			this.tbArgC.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbArgC.TextAlign")));
			this.tbArgC.Visible = ((bool)(resources.GetObject("tbArgC.Visible")));
			this.tbArgC.WordWrap = ((bool)(resources.GetObject("tbArgC.WordWrap")));
			this.tbArgC.Validating += new System.ComponentModel.CancelEventHandler(this.dec8_Validating);
			this.tbArgC.Validated += new System.EventHandler(this.dec8_Validated);
			// 
			// tbType
			// 
			this.tbType.AccessibleDescription = resources.GetString("tbType.AccessibleDescription");
			this.tbType.AccessibleName = resources.GetString("tbType.AccessibleName");
			this.tbType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbType.Anchor")));
			this.tbType.AutoSize = ((bool)(resources.GetObject("tbType.AutoSize")));
			this.tbType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbType.BackgroundImage")));
			this.tbType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbType.Dock")));
			this.tbType.Enabled = ((bool)(resources.GetObject("tbType.Enabled")));
			this.tbType.Font = ((System.Drawing.Font)(resources.GetObject("tbType.Font")));
			this.tbType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbType.ImeMode")));
			this.tbType.Location = ((System.Drawing.Point)(resources.GetObject("tbType.Location")));
			this.tbType.MaxLength = ((int)(resources.GetObject("tbType.MaxLength")));
			this.tbType.Multiline = ((bool)(resources.GetObject("tbType.Multiline")));
			this.tbType.Name = "tbType";
			this.tbType.PasswordChar = ((char)(resources.GetObject("tbType.PasswordChar")));
			this.tbType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbType.RightToLeft")));
			this.tbType.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbType.ScrollBars")));
			this.tbType.Size = ((System.Drawing.Size)(resources.GetObject("tbType.Size")));
			this.tbType.TabIndex = ((int)(resources.GetObject("tbType.TabIndex")));
			this.tbType.Text = resources.GetString("tbType.Text");
			this.tbType.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbType.TextAlign")));
			this.tbType.Visible = ((bool)(resources.GetObject("tbType.Visible")));
			this.tbType.WordWrap = ((bool)(resources.GetObject("tbType.WordWrap")));
			this.tbType.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbType.Validated += new System.EventHandler(this.hex8_Validated);
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
			this.tbFormat.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbFormat.Validated += new System.EventHandler(this.hex16_Validated);
			// 
			// lbReserved
			// 
			this.lbReserved.AccessibleDescription = resources.GetString("lbReserved.AccessibleDescription");
			this.lbReserved.AccessibleName = resources.GetString("lbReserved.AccessibleName");
			this.lbReserved.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbReserved.Anchor")));
			this.lbReserved.AutoSize = ((bool)(resources.GetObject("lbReserved.AutoSize")));
			this.lbReserved.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbReserved.Dock")));
			this.lbReserved.Enabled = ((bool)(resources.GetObject("lbReserved.Enabled")));
			this.lbReserved.Font = ((System.Drawing.Font)(resources.GetObject("lbReserved.Font")));
			this.lbReserved.Image = ((System.Drawing.Image)(resources.GetObject("lbReserved.Image")));
			this.lbReserved.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbReserved.ImageAlign")));
			this.lbReserved.ImageIndex = ((int)(resources.GetObject("lbReserved.ImageIndex")));
			this.lbReserved.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbReserved.ImeMode")));
			this.lbReserved.Location = ((System.Drawing.Point)(resources.GetObject("lbReserved.Location")));
			this.lbReserved.Name = "lbReserved";
			this.lbReserved.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbReserved.RightToLeft")));
			this.lbReserved.Size = ((System.Drawing.Size)(resources.GetObject("lbReserved.Size")));
			this.lbReserved.TabIndex = ((int)(resources.GetObject("lbReserved.TabIndex")));
			this.lbReserved.Text = resources.GetString("lbReserved.Text");
			this.lbReserved.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbReserved.TextAlign")));
			this.lbReserved.Visible = ((bool)(resources.GetObject("lbReserved.Visible")));
			// 
			// lbFlags
			// 
			this.lbFlags.AccessibleDescription = resources.GetString("lbFlags.AccessibleDescription");
			this.lbFlags.AccessibleName = resources.GetString("lbFlags.AccessibleName");
			this.lbFlags.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbFlags.Anchor")));
			this.lbFlags.AutoSize = ((bool)(resources.GetObject("lbFlags.AutoSize")));
			this.lbFlags.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbFlags.Dock")));
			this.lbFlags.Enabled = ((bool)(resources.GetObject("lbFlags.Enabled")));
			this.lbFlags.Font = ((System.Drawing.Font)(resources.GetObject("lbFlags.Font")));
			this.lbFlags.Image = ((System.Drawing.Image)(resources.GetObject("lbFlags.Image")));
			this.lbFlags.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFlags.ImageAlign")));
			this.lbFlags.ImageIndex = ((int)(resources.GetObject("lbFlags.ImageIndex")));
			this.lbFlags.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbFlags.ImeMode")));
			this.lbFlags.Location = ((System.Drawing.Point)(resources.GetObject("lbFlags.Location")));
			this.lbFlags.Name = "lbFlags";
			this.lbFlags.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbFlags.RightToLeft")));
			this.lbFlags.Size = ((System.Drawing.Size)(resources.GetObject("lbFlags.Size")));
			this.lbFlags.TabIndex = ((int)(resources.GetObject("lbFlags.TabIndex")));
			this.lbFlags.Text = resources.GetString("lbFlags.Text");
			this.lbFlags.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFlags.TextAlign")));
			this.lbFlags.Visible = ((bool)(resources.GetObject("lbFlags.Visible")));
			// 
			// lbType
			// 
			this.lbType.AccessibleDescription = resources.GetString("lbType.AccessibleDescription");
			this.lbType.AccessibleName = resources.GetString("lbType.AccessibleName");
			this.lbType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbType.Anchor")));
			this.lbType.AutoSize = ((bool)(resources.GetObject("lbType.AutoSize")));
			this.lbType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbType.Dock")));
			this.lbType.Enabled = ((bool)(resources.GetObject("lbType.Enabled")));
			this.lbType.Font = ((System.Drawing.Font)(resources.GetObject("lbType.Font")));
			this.lbType.Image = ((System.Drawing.Image)(resources.GetObject("lbType.Image")));
			this.lbType.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbType.ImageAlign")));
			this.lbType.ImageIndex = ((int)(resources.GetObject("lbType.ImageIndex")));
			this.lbType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbType.ImeMode")));
			this.lbType.Location = ((System.Drawing.Point)(resources.GetObject("lbType.Location")));
			this.lbType.Name = "lbType";
			this.lbType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbType.RightToLeft")));
			this.lbType.Size = ((System.Drawing.Size)(resources.GetObject("lbType.Size")));
			this.lbType.TabIndex = ((int)(resources.GetObject("lbType.TabIndex")));
			this.lbType.Text = resources.GetString("lbType.Text");
			this.lbType.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbType.TextAlign")));
			this.lbType.Visible = ((bool)(resources.GetObject("lbType.Visible")));
			// 
			// lbLocalC
			// 
			this.lbLocalC.AccessibleDescription = resources.GetString("lbLocalC.AccessibleDescription");
			this.lbLocalC.AccessibleName = resources.GetString("lbLocalC.AccessibleName");
			this.lbLocalC.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbLocalC.Anchor")));
			this.lbLocalC.AutoSize = ((bool)(resources.GetObject("lbLocalC.AutoSize")));
			this.lbLocalC.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbLocalC.Dock")));
			this.lbLocalC.Enabled = ((bool)(resources.GetObject("lbLocalC.Enabled")));
			this.lbLocalC.Font = ((System.Drawing.Font)(resources.GetObject("lbLocalC.Font")));
			this.lbLocalC.Image = ((System.Drawing.Image)(resources.GetObject("lbLocalC.Image")));
			this.lbLocalC.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbLocalC.ImageAlign")));
			this.lbLocalC.ImageIndex = ((int)(resources.GetObject("lbLocalC.ImageIndex")));
			this.lbLocalC.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbLocalC.ImeMode")));
			this.lbLocalC.Location = ((System.Drawing.Point)(resources.GetObject("lbLocalC.Location")));
			this.lbLocalC.Name = "lbLocalC";
			this.lbLocalC.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbLocalC.RightToLeft")));
			this.lbLocalC.Size = ((System.Drawing.Size)(resources.GetObject("lbLocalC.Size")));
			this.lbLocalC.TabIndex = ((int)(resources.GetObject("lbLocalC.TabIndex")));
			this.lbLocalC.Text = resources.GetString("lbLocalC.Text");
			this.lbLocalC.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbLocalC.TextAlign")));
			this.lbLocalC.Visible = ((bool)(resources.GetObject("lbLocalC.Visible")));
			// 
			// lbArgC
			// 
			this.lbArgC.AccessibleDescription = resources.GetString("lbArgC.AccessibleDescription");
			this.lbArgC.AccessibleName = resources.GetString("lbArgC.AccessibleName");
			this.lbArgC.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbArgC.Anchor")));
			this.lbArgC.AutoSize = ((bool)(resources.GetObject("lbArgC.AutoSize")));
			this.lbArgC.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbArgC.Dock")));
			this.lbArgC.Enabled = ((bool)(resources.GetObject("lbArgC.Enabled")));
			this.lbArgC.Font = ((System.Drawing.Font)(resources.GetObject("lbArgC.Font")));
			this.lbArgC.Image = ((System.Drawing.Image)(resources.GetObject("lbArgC.Image")));
			this.lbArgC.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbArgC.ImageAlign")));
			this.lbArgC.ImageIndex = ((int)(resources.GetObject("lbArgC.ImageIndex")));
			this.lbArgC.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbArgC.ImeMode")));
			this.lbArgC.Location = ((System.Drawing.Point)(resources.GetObject("lbArgC.Location")));
			this.lbArgC.Name = "lbArgC";
			this.lbArgC.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbArgC.RightToLeft")));
			this.lbArgC.Size = ((System.Drawing.Size)(resources.GetObject("lbArgC.Size")));
			this.lbArgC.TabIndex = ((int)(resources.GetObject("lbArgC.TabIndex")));
			this.lbArgC.Text = resources.GetString("lbArgC.Text");
			this.lbArgC.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbArgC.TextAlign")));
			this.lbArgC.Visible = ((bool)(resources.GetObject("lbArgC.Visible")));
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
			this.pnHeading.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnHeading.ImeMode")));
			this.pnHeading.Location = ((System.Drawing.Point)(resources.GetObject("pnHeading.Location")));
			this.pnHeading.Name = "pnHeading";
			this.pnHeading.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnHeading.RightToLeft")));
			this.pnHeading.Size = ((System.Drawing.Size)(resources.GetObject("pnHeading.Size")));
			this.pnHeading.TabIndex = ((int)(resources.GetObject("pnHeading.TabIndex")));
			this.pnHeading.Text = resources.GetString("pnHeading.Text");
			this.pnHeading.Visible = ((bool)(resources.GetObject("pnHeading.Visible")));
			// 
			// bhavPanel
			// 
			this.bhavPanel.AccessibleDescription = resources.GetString("bhavPanel.AccessibleDescription");
			this.bhavPanel.AccessibleName = resources.GetString("bhavPanel.AccessibleName");
			this.bhavPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("bhavPanel.Anchor")));
			this.bhavPanel.AutoScroll = ((bool)(resources.GetObject("bhavPanel.AutoScroll")));
			this.bhavPanel.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("bhavPanel.AutoScrollMargin")));
			this.bhavPanel.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("bhavPanel.AutoScrollMinSize")));
			this.bhavPanel.BackColor = System.Drawing.SystemColors.Control;
			this.bhavPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bhavPanel.BackgroundImage")));
			this.bhavPanel.Controls.Add(this.pnflowcontainer);
			this.bhavPanel.Controls.Add(this.btnDel);
			this.bhavPanel.Controls.Add(this.gbMove);
			this.bhavPanel.Controls.Add(this.btnSort);
			this.bhavPanel.Controls.Add(this.btnCommit);
			this.bhavPanel.Controls.Add(this.lbFilename);
			this.bhavPanel.Controls.Add(this.tbFilename);
			this.bhavPanel.Controls.Add(this.gbInstruction);
			this.bhavPanel.Controls.Add(this.tbReserved);
			this.bhavPanel.Controls.Add(this.tbLocalC);
			this.bhavPanel.Controls.Add(this.tbFlags);
			this.bhavPanel.Controls.Add(this.tbArgC);
			this.bhavPanel.Controls.Add(this.tbType);
			this.bhavPanel.Controls.Add(this.tbFormat);
			this.bhavPanel.Controls.Add(this.lbReserved);
			this.bhavPanel.Controls.Add(this.lbFlags);
			this.bhavPanel.Controls.Add(this.lbType);
			this.bhavPanel.Controls.Add(this.lbLocalC);
			this.bhavPanel.Controls.Add(this.lbArgC);
			this.bhavPanel.Controls.Add(this.lbFormat);
			this.bhavPanel.Controls.Add(this.pnHeading);
			this.bhavPanel.Controls.Add(this.btnAdd);
			this.bhavPanel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("bhavPanel.Dock")));
			this.bhavPanel.Enabled = ((bool)(resources.GetObject("bhavPanel.Enabled")));
			this.bhavPanel.Font = ((System.Drawing.Font)(resources.GetObject("bhavPanel.Font")));
			this.bhavPanel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("bhavPanel.ImeMode")));
			this.bhavPanel.Location = ((System.Drawing.Point)(resources.GetObject("bhavPanel.Location")));
			this.bhavPanel.Name = "bhavPanel";
			this.bhavPanel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("bhavPanel.RightToLeft")));
			this.bhavPanel.Size = ((System.Drawing.Size)(resources.GetObject("bhavPanel.Size")));
			this.bhavPanel.TabIndex = ((int)(resources.GetObject("bhavPanel.TabIndex")));
			this.bhavPanel.Text = resources.GetString("bhavPanel.Text");
			this.bhavPanel.Visible = ((bool)(resources.GetObject("bhavPanel.Visible")));
			this.bhavPanel.Resize += new System.EventHandler(this.bhavPanel_Resize);
			// 
			// pnflowcontainer
			// 
			this.pnflowcontainer.AccessibleDescription = resources.GetString("pnflowcontainer.AccessibleDescription");
			this.pnflowcontainer.AccessibleName = resources.GetString("pnflowcontainer.AccessibleName");
			this.pnflowcontainer.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pnflowcontainer.Anchor")));
			this.pnflowcontainer.AutoScroll = ((bool)(resources.GetObject("pnflowcontainer.AutoScroll")));
			this.pnflowcontainer.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("pnflowcontainer.AutoScrollMargin")));
			this.pnflowcontainer.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("pnflowcontainer.AutoScrollMinSize")));
			this.pnflowcontainer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnflowcontainer.BackgroundImage")));
			this.pnflowcontainer.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnflowcontainer.Dock")));
			this.pnflowcontainer.Enabled = ((bool)(resources.GetObject("pnflowcontainer.Enabled")));
			this.pnflowcontainer.Font = ((System.Drawing.Font)(resources.GetObject("pnflowcontainer.Font")));
			this.pnflowcontainer.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnflowcontainer.ImeMode")));
			this.pnflowcontainer.Location = ((System.Drawing.Point)(resources.GetObject("pnflowcontainer.Location")));
			this.pnflowcontainer.Name = "pnflowcontainer";
			this.pnflowcontainer.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnflowcontainer.RightToLeft")));
			this.pnflowcontainer.SelectedIndex = -1;
			this.pnflowcontainer.Size = ((System.Drawing.Size)(resources.GetObject("pnflowcontainer.Size")));
			this.pnflowcontainer.TabIndex = ((int)(resources.GetObject("pnflowcontainer.TabIndex")));
			this.pnflowcontainer.Visible = ((bool)(resources.GetObject("pnflowcontainer.Visible")));
			this.pnflowcontainer.SelectedInstChanged += new System.EventHandler(this.pnflowcontainer_SelectedInstChanged);
			// 
			// btnDel
			// 
			this.btnDel.AccessibleDescription = resources.GetString("btnDel.AccessibleDescription");
			this.btnDel.AccessibleName = resources.GetString("btnDel.AccessibleName");
			this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDel.Anchor")));
			this.btnDel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDel.BackgroundImage")));
			this.btnDel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDel.Dock")));
			this.btnDel.Enabled = ((bool)(resources.GetObject("btnDel.Enabled")));
			this.btnDel.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDel.FlatStyle")));
			this.btnDel.Font = ((System.Drawing.Font)(resources.GetObject("btnDel.Font")));
			this.btnDel.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.Image")));
			this.btnDel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDel.ImageAlign")));
			this.btnDel.ImageIndex = ((int)(resources.GetObject("btnDel.ImageIndex")));
			this.btnDel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDel.ImeMode")));
			this.btnDel.Location = ((System.Drawing.Point)(resources.GetObject("btnDel.Location")));
			this.btnDel.Name = "btnDel";
			this.btnDel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDel.RightToLeft")));
			this.btnDel.Size = ((System.Drawing.Size)(resources.GetObject("btnDel.Size")));
			this.btnDel.TabIndex = ((int)(resources.GetObject("btnDel.TabIndex")));
			this.btnDel.Text = resources.GetString("btnDel.Text");
			this.btnDel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDel.TextAlign")));
			this.btnDel.Visible = ((bool)(resources.GetObject("btnDel.Visible")));
			this.btnDel.Click += new System.EventHandler(this.btnDel_Clicked);
			// 
			// gbMove
			// 
			this.gbMove.AccessibleDescription = resources.GetString("gbMove.AccessibleDescription");
			this.gbMove.AccessibleName = resources.GetString("gbMove.AccessibleName");
			this.gbMove.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gbMove.Anchor")));
			this.gbMove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gbMove.BackgroundImage")));
			this.gbMove.Controls.Add(this.btnUp);
			this.gbMove.Controls.Add(this.btnDown);
			this.gbMove.Controls.Add(this.lbUpDown);
			this.gbMove.Controls.Add(this.tbLines);
			this.gbMove.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("gbMove.Dock")));
			this.gbMove.Enabled = ((bool)(resources.GetObject("gbMove.Enabled")));
			this.gbMove.Font = ((System.Drawing.Font)(resources.GetObject("gbMove.Font")));
			this.gbMove.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gbMove.ImeMode")));
			this.gbMove.Location = ((System.Drawing.Point)(resources.GetObject("gbMove.Location")));
			this.gbMove.Name = "gbMove";
			this.gbMove.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("gbMove.RightToLeft")));
			this.gbMove.Size = ((System.Drawing.Size)(resources.GetObject("gbMove.Size")));
			this.gbMove.TabIndex = ((int)(resources.GetObject("gbMove.TabIndex")));
			this.gbMove.TabStop = false;
			this.gbMove.Text = resources.GetString("gbMove.Text");
			this.gbMove.Visible = ((bool)(resources.GetObject("gbMove.Visible")));
			// 
			// btnUp
			// 
			this.btnUp.AccessibleDescription = resources.GetString("btnUp.AccessibleDescription");
			this.btnUp.AccessibleName = resources.GetString("btnUp.AccessibleName");
			this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnUp.Anchor")));
			this.btnUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUp.BackgroundImage")));
			this.btnUp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnUp.Dock")));
			this.btnUp.Enabled = ((bool)(resources.GetObject("btnUp.Enabled")));
			this.btnUp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnUp.FlatStyle")));
			this.btnUp.Font = ((System.Drawing.Font)(resources.GetObject("btnUp.Font")));
			this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
			this.btnUp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnUp.ImageAlign")));
			this.btnUp.ImageIndex = ((int)(resources.GetObject("btnUp.ImageIndex")));
			this.btnUp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnUp.ImeMode")));
			this.btnUp.Location = ((System.Drawing.Point)(resources.GetObject("btnUp.Location")));
			this.btnUp.Name = "btnUp";
			this.btnUp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnUp.RightToLeft")));
			this.btnUp.Size = ((System.Drawing.Size)(resources.GetObject("btnUp.Size")));
			this.btnUp.TabIndex = ((int)(resources.GetObject("btnUp.TabIndex")));
			this.btnUp.Text = resources.GetString("btnUp.Text");
			this.btnUp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnUp.TextAlign")));
			this.btnUp.Visible = ((bool)(resources.GetObject("btnUp.Visible")));
			this.btnUp.Click += new System.EventHandler(this.btnMove_Clicked);
			// 
			// btnDown
			// 
			this.btnDown.AccessibleDescription = resources.GetString("btnDown.AccessibleDescription");
			this.btnDown.AccessibleName = resources.GetString("btnDown.AccessibleName");
			this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDown.Anchor")));
			this.btnDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDown.BackgroundImage")));
			this.btnDown.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDown.Dock")));
			this.btnDown.Enabled = ((bool)(resources.GetObject("btnDown.Enabled")));
			this.btnDown.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDown.FlatStyle")));
			this.btnDown.Font = ((System.Drawing.Font)(resources.GetObject("btnDown.Font")));
			this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
			this.btnDown.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDown.ImageAlign")));
			this.btnDown.ImageIndex = ((int)(resources.GetObject("btnDown.ImageIndex")));
			this.btnDown.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDown.ImeMode")));
			this.btnDown.Location = ((System.Drawing.Point)(resources.GetObject("btnDown.Location")));
			this.btnDown.Name = "btnDown";
			this.btnDown.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDown.RightToLeft")));
			this.btnDown.Size = ((System.Drawing.Size)(resources.GetObject("btnDown.Size")));
			this.btnDown.TabIndex = ((int)(resources.GetObject("btnDown.TabIndex")));
			this.btnDown.Text = resources.GetString("btnDown.Text");
			this.btnDown.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDown.TextAlign")));
			this.btnDown.Visible = ((bool)(resources.GetObject("btnDown.Visible")));
			this.btnDown.Click += new System.EventHandler(this.btnMove_Clicked);
			// 
			// lbUpDown
			// 
			this.lbUpDown.AccessibleDescription = resources.GetString("lbUpDown.AccessibleDescription");
			this.lbUpDown.AccessibleName = resources.GetString("lbUpDown.AccessibleName");
			this.lbUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbUpDown.Anchor")));
			this.lbUpDown.AutoSize = ((bool)(resources.GetObject("lbUpDown.AutoSize")));
			this.lbUpDown.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbUpDown.Dock")));
			this.lbUpDown.Enabled = ((bool)(resources.GetObject("lbUpDown.Enabled")));
			this.lbUpDown.Font = ((System.Drawing.Font)(resources.GetObject("lbUpDown.Font")));
			this.lbUpDown.Image = ((System.Drawing.Image)(resources.GetObject("lbUpDown.Image")));
			this.lbUpDown.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbUpDown.ImageAlign")));
			this.lbUpDown.ImageIndex = ((int)(resources.GetObject("lbUpDown.ImageIndex")));
			this.lbUpDown.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbUpDown.ImeMode")));
			this.lbUpDown.Location = ((System.Drawing.Point)(resources.GetObject("lbUpDown.Location")));
			this.lbUpDown.Name = "lbUpDown";
			this.lbUpDown.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbUpDown.RightToLeft")));
			this.lbUpDown.Size = ((System.Drawing.Size)(resources.GetObject("lbUpDown.Size")));
			this.lbUpDown.TabIndex = ((int)(resources.GetObject("lbUpDown.TabIndex")));
			this.lbUpDown.Text = resources.GetString("lbUpDown.Text");
			this.lbUpDown.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbUpDown.TextAlign")));
			this.lbUpDown.Visible = ((bool)(resources.GetObject("lbUpDown.Visible")));
			// 
			// tbLines
			// 
			this.tbLines.AccessibleDescription = resources.GetString("tbLines.AccessibleDescription");
			this.tbLines.AccessibleName = resources.GetString("tbLines.AccessibleName");
			this.tbLines.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbLines.Anchor")));
			this.tbLines.AutoSize = ((bool)(resources.GetObject("tbLines.AutoSize")));
			this.tbLines.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbLines.BackgroundImage")));
			this.tbLines.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbLines.Dock")));
			this.tbLines.Enabled = ((bool)(resources.GetObject("tbLines.Enabled")));
			this.tbLines.Font = ((System.Drawing.Font)(resources.GetObject("tbLines.Font")));
			this.tbLines.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbLines.ImeMode")));
			this.tbLines.Location = ((System.Drawing.Point)(resources.GetObject("tbLines.Location")));
			this.tbLines.MaxLength = ((int)(resources.GetObject("tbLines.MaxLength")));
			this.tbLines.Multiline = ((bool)(resources.GetObject("tbLines.Multiline")));
			this.tbLines.Name = "tbLines";
			this.tbLines.PasswordChar = ((char)(resources.GetObject("tbLines.PasswordChar")));
			this.tbLines.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbLines.RightToLeft")));
			this.tbLines.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbLines.ScrollBars")));
			this.tbLines.Size = ((System.Drawing.Size)(resources.GetObject("tbLines.Size")));
			this.tbLines.TabIndex = ((int)(resources.GetObject("tbLines.TabIndex")));
			this.tbLines.Text = resources.GetString("tbLines.Text");
			this.tbLines.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbLines.TextAlign")));
			this.tbLines.Visible = ((bool)(resources.GetObject("tbLines.Visible")));
			this.tbLines.WordWrap = ((bool)(resources.GetObject("tbLines.WordWrap")));
			this.tbLines.Validating += new System.ComponentModel.CancelEventHandler(this.dec16_Validating);
			// 
			// btnSort
			// 
			this.btnSort.AccessibleDescription = resources.GetString("btnSort.AccessibleDescription");
			this.btnSort.AccessibleName = resources.GetString("btnSort.AccessibleName");
			this.btnSort.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSort.Anchor")));
			this.btnSort.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSort.BackgroundImage")));
			this.btnSort.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSort.Dock")));
			this.btnSort.Enabled = ((bool)(resources.GetObject("btnSort.Enabled")));
			this.btnSort.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSort.FlatStyle")));
			this.btnSort.Font = ((System.Drawing.Font)(resources.GetObject("btnSort.Font")));
			this.btnSort.Image = ((System.Drawing.Image)(resources.GetObject("btnSort.Image")));
			this.btnSort.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSort.ImageAlign")));
			this.btnSort.ImageIndex = ((int)(resources.GetObject("btnSort.ImageIndex")));
			this.btnSort.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSort.ImeMode")));
			this.btnSort.Location = ((System.Drawing.Point)(resources.GetObject("btnSort.Location")));
			this.btnSort.Name = "btnSort";
			this.btnSort.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSort.RightToLeft")));
			this.btnSort.Size = ((System.Drawing.Size)(resources.GetObject("btnSort.Size")));
			this.btnSort.TabIndex = ((int)(resources.GetObject("btnSort.TabIndex")));
			this.btnSort.Text = resources.GetString("btnSort.Text");
			this.btnSort.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSort.TextAlign")));
			this.btnSort.Visible = ((bool)(resources.GetObject("btnSort.Visible")));
			this.btnSort.Click += new System.EventHandler(this.btnSort_Clicked);
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
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Clicked);
			// 
			// BhavForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.bhavPanel);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "BhavForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.gbInstruction.ResumeLayout(false);
			this.pnHeading.ResumeLayout(false);
			this.bhavPanel.ResumeLayout(false);
			this.gbMove.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private void pnflowcontainer_SelectedInstChanged(object sender, System.EventArgs e)
		{
			int index = this.pnflowcontainer.SelectedIndex;
			if (index < 0)
				this.SetReadOnly(true);
			else
				this.SetReadOnly(false);
		}

		private void bhavPanel_Resize(object sender, System.EventArgs e)
		{
			this.tbFilename.Width = this.lbFormat.Left - (this.tbFilename.Left + 4);
		}


		private void btnCommit_Clicked(object sender, System.EventArgs e)
		{
			try 
			{
				wrapper.SynchronizeUserData();
				btnCommit.Enabled = false;
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}			
		}

		private void btnSort_Clicked(object sender, System.EventArgs e)
		{
			this.pnflowcontainer.Sort();
		}

		private void btnMove_Clicked(object sender, System.EventArgs e)
		{
			int mv;
			try { mv = Convert.ToInt32(tbLines.Text); }
			catch (Exception) { return; }
			if (sender == this.btnUp)
				this.pnflowcontainer.MoveInst(mv * -1);
			else
				this.pnflowcontainer.MoveInst(mv);
		}

		private void btnAdd_Clicked(object sender, System.EventArgs e)
		{
			this.pnflowcontainer.Add();
		}

		private void btnDel_Clicked(object sender, System.EventArgs e)
		{
			this.pnflowcontainer.Delete();
		}


		private void bhavInstPanel1_Cancelled(object sender, System.EventArgs e)
		{
			this.pnflowcontainer.Cancel();
		}


		private void tbFilename_Validated(object sender, System.EventArgs e)
		{
			wrapper.FileName = tbFilename.Text;
		}


		private void dec8_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try { Convert.ToByte(((TextBox)sender).Text); }
			catch (Exception) { e.Cancel = true; }
		}

		private void dec16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try { Convert.ToUInt16(((TextBox)sender).Text); }
			catch (Exception) { e.Cancel = true; }
		}

		private void dec32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try { Convert.ToUInt32(((TextBox)sender).Text); }
			catch (Exception) { e.Cancel = true; }
		}

		private void hex8_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try { Convert.ToByte(((TextBox)sender).Text, 16); }
			catch (Exception) { e.Cancel = true; }
		}

		private void hex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try { Convert.ToUInt16(((TextBox)sender).Text, 16); }
			catch (Exception) { e.Cancel = true; }
		}

		private void hex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try { Convert.ToUInt32(((TextBox)sender).Text, 16); }
			catch (Exception) { e.Cancel = true; }
		}


		private void dec8_Validated(object sender, System.EventArgs e)
		{
			byte val = Convert.ToByte(((TextBox)sender).Text);

			int i = alDec8.IndexOf(sender);

			switch (i)
			{
				case 0: wrapper.Header.ArgumentCount = val; break;
				case 1: wrapper.Header.LocalVarCount = val; break;
				default:
					throw new Exception("dec8_Validated not applicable to control " + sender.ToString());
			}
		}

		private void hex8_Validated(object sender, System.EventArgs e)
		{
			byte val = Convert.ToByte(((TextBox)sender).Text, 16);

			switch(alHex8.IndexOf(sender))
			{
				case 0: wrapper.Header.Type = val; break;
				default:
					throw new Exception("hex8_Validated not applicable to control " + sender.ToString());
			}
		}

		private void hex16_Validated(object sender, System.EventArgs e)
		{
			ushort val = Convert.ToUInt16(((TextBox)sender).Text, 16);

			switch (alHex16.IndexOf(sender))
			{
				case 0: wrapper.Header.Format = val; break;
				case 1: wrapper.Header.Flags = val; break;
				case 2: wrapper.Header.Zero = val; break;
				default:
					throw new Exception("hex16_Validated not applicable to control " + sender.ToString());
			}
		}

	}
}
