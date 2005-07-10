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
	public class ObjfForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ListBox lbobjf;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label lbobjffile;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.LinkLabel llchangeobjf;
		private System.Windows.Forms.TextBox tbguard;
		private System.Windows.Forms.Button btcommitobjf;
		private System.Windows.Forms.TextBox tbaction;
		private System.Windows.Forms.Label lbname;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.Button btobjaction;
		private System.Windows.Forms.Button btobjguard;
		private System.Windows.Forms.Panel objfPanel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
       
		public ObjfForm()
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

		
		#region Objf
		/// <summary>
		/// Stores the currently active Wrapper
		/// </summary>
		internal Objf wrapper = null;
		private bool internalchg;
		#endregion

		#region IPackedFileUI Member
		/// <summary>
		/// Returns the Panel that will be displayed within SimPe
		/// </summary>
		public System.Windows.Forms.Panel GUIHandle
		{
			get
			{
				return objfPanel;
			}
		}

		/// <summary>
		/// Is called by SimPe (through the Wrapper) when the Panel is going to be displayed, so
		/// you should updatet the Data displayed by the Panel with the Attributes stored in the
		/// passed Wrapper.
		/// </summary>
		/// <remarks>internalchg is used to let TextChanged event handlers know the change is being
		/// made internally rather than by the users.</remarks>
		/// <param name="wrapper">The Attributes of this Wrapper have to be displayed</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			//wrapper;
			wrapper = (Objf)wrp;

			llchangeobjf.Enabled = false;
			lbobjf.Items.Clear();
			foreach (ObjfItem i in wrapper.Items) 
			{
				lbobjf.Items.Add(i);
			}

			
			lbobjffile.Text = wrapper.FileName;
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ObjfForm));
			this.objfPanel = new System.Windows.Forms.Panel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btobjaction = new System.Windows.Forms.Button();
			this.btobjguard = new System.Windows.Forms.Button();
			this.lbname = new System.Windows.Forms.Label();
			this.tbaction = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.llchangeobjf = new System.Windows.Forms.LinkLabel();
			this.tbguard = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.lbobjf = new System.Windows.Forms.ListBox();
			this.btcommitobjf = new System.Windows.Forms.Button();
			this.panel4 = new System.Windows.Forms.Panel();
			this.lbobjffile = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.objfPanel.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// objfPanel
			// 
			this.objfPanel.AccessibleDescription = resources.GetString("objfPanel.AccessibleDescription");
			this.objfPanel.AccessibleName = resources.GetString("objfPanel.AccessibleName");
			this.objfPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("objfPanel.Anchor")));
			this.objfPanel.AutoScroll = ((bool)(resources.GetObject("objfPanel.AutoScroll")));
			this.objfPanel.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("objfPanel.AutoScrollMargin")));
			this.objfPanel.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("objfPanel.AutoScrollMinSize")));
			this.objfPanel.BackColor = System.Drawing.SystemColors.Control;
			this.objfPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("objfPanel.BackgroundImage")));
			this.objfPanel.Controls.Add(this.groupBox2);
			this.objfPanel.Controls.Add(this.lbobjf);
			this.objfPanel.Controls.Add(this.btcommitobjf);
			this.objfPanel.Controls.Add(this.panel4);
			this.objfPanel.Controls.Add(this.label19);
			this.objfPanel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("objfPanel.Dock")));
			this.objfPanel.Enabled = ((bool)(resources.GetObject("objfPanel.Enabled")));
			this.objfPanel.Font = ((System.Drawing.Font)(resources.GetObject("objfPanel.Font")));
			this.objfPanel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("objfPanel.ImeMode")));
			this.objfPanel.Location = ((System.Drawing.Point)(resources.GetObject("objfPanel.Location")));
			this.objfPanel.Name = "objfPanel";
			this.objfPanel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("objfPanel.RightToLeft")));
			this.objfPanel.Size = ((System.Drawing.Size)(resources.GetObject("objfPanel.Size")));
			this.objfPanel.TabIndex = ((int)(resources.GetObject("objfPanel.TabIndex")));
			this.objfPanel.Text = resources.GetString("objfPanel.Text");
			this.objfPanel.Visible = ((bool)(resources.GetObject("objfPanel.Visible")));
			// 
			// groupBox2
			// 
			this.groupBox2.AccessibleDescription = resources.GetString("groupBox2.AccessibleDescription");
			this.groupBox2.AccessibleName = resources.GetString("groupBox2.AccessibleName");
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox2.Anchor")));
			this.groupBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox2.BackgroundImage")));
			this.groupBox2.Controls.Add(this.btobjaction);
			this.groupBox2.Controls.Add(this.btobjguard);
			this.groupBox2.Controls.Add(this.lbname);
			this.groupBox2.Controls.Add(this.tbaction);
			this.groupBox2.Controls.Add(this.label15);
			this.groupBox2.Controls.Add(this.llchangeobjf);
			this.groupBox2.Controls.Add(this.tbguard);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("groupBox2.Dock")));
			this.groupBox2.Enabled = ((bool)(resources.GetObject("groupBox2.Enabled")));
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Font = ((System.Drawing.Font)(resources.GetObject("groupBox2.Font")));
			this.groupBox2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("groupBox2.ImeMode")));
			this.groupBox2.Location = ((System.Drawing.Point)(resources.GetObject("groupBox2.Location")));
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("groupBox2.RightToLeft")));
			this.groupBox2.Size = ((System.Drawing.Size)(resources.GetObject("groupBox2.Size")));
			this.groupBox2.TabIndex = ((int)(resources.GetObject("groupBox2.TabIndex")));
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = resources.GetString("groupBox2.Text");
			this.groupBox2.Visible = ((bool)(resources.GetObject("groupBox2.Visible")));
			// 
			// btobjaction
			// 
			this.btobjaction.AccessibleDescription = resources.GetString("btobjaction.AccessibleDescription");
			this.btobjaction.AccessibleName = resources.GetString("btobjaction.AccessibleName");
			this.btobjaction.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btobjaction.Anchor")));
			this.btobjaction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btobjaction.BackgroundImage")));
			this.btobjaction.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btobjaction.Dock")));
			this.btobjaction.Enabled = ((bool)(resources.GetObject("btobjaction.Enabled")));
			this.btobjaction.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btobjaction.FlatStyle")));
			this.btobjaction.Font = ((System.Drawing.Font)(resources.GetObject("btobjaction.Font")));
			this.btobjaction.Image = ((System.Drawing.Image)(resources.GetObject("btobjaction.Image")));
			this.btobjaction.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btobjaction.ImageAlign")));
			this.btobjaction.ImageIndex = ((int)(resources.GetObject("btobjaction.ImageIndex")));
			this.btobjaction.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btobjaction.ImeMode")));
			this.btobjaction.Location = ((System.Drawing.Point)(resources.GetObject("btobjaction.Location")));
			this.btobjaction.Name = "btobjaction";
			this.btobjaction.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btobjaction.RightToLeft")));
			this.btobjaction.Size = ((System.Drawing.Size)(resources.GetObject("btobjaction.Size")));
			this.btobjaction.TabIndex = ((int)(resources.GetObject("btobjaction.TabIndex")));
			this.btobjaction.Text = resources.GetString("btobjaction.Text");
			this.btobjaction.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btobjaction.TextAlign")));
			this.btobjaction.Visible = ((bool)(resources.GetObject("btobjaction.Visible")));
			this.btobjaction.Click += new System.EventHandler(this.GetObjfAction);
			// 
			// btobjguard
			// 
			this.btobjguard.AccessibleDescription = resources.GetString("btobjguard.AccessibleDescription");
			this.btobjguard.AccessibleName = resources.GetString("btobjguard.AccessibleName");
			this.btobjguard.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btobjguard.Anchor")));
			this.btobjguard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btobjguard.BackgroundImage")));
			this.btobjguard.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btobjguard.Dock")));
			this.btobjguard.Enabled = ((bool)(resources.GetObject("btobjguard.Enabled")));
			this.btobjguard.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btobjguard.FlatStyle")));
			this.btobjguard.Font = ((System.Drawing.Font)(resources.GetObject("btobjguard.Font")));
			this.btobjguard.Image = ((System.Drawing.Image)(resources.GetObject("btobjguard.Image")));
			this.btobjguard.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btobjguard.ImageAlign")));
			this.btobjguard.ImageIndex = ((int)(resources.GetObject("btobjguard.ImageIndex")));
			this.btobjguard.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btobjguard.ImeMode")));
			this.btobjguard.Location = ((System.Drawing.Point)(resources.GetObject("btobjguard.Location")));
			this.btobjguard.Name = "btobjguard";
			this.btobjguard.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btobjguard.RightToLeft")));
			this.btobjguard.Size = ((System.Drawing.Size)(resources.GetObject("btobjguard.Size")));
			this.btobjguard.TabIndex = ((int)(resources.GetObject("btobjguard.TabIndex")));
			this.btobjguard.Text = resources.GetString("btobjguard.Text");
			this.btobjguard.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btobjguard.TextAlign")));
			this.btobjguard.Visible = ((bool)(resources.GetObject("btobjguard.Visible")));
			this.btobjguard.Click += new System.EventHandler(this.GetObjfGuard);
			// 
			// lbname
			// 
			this.lbname.AccessibleDescription = resources.GetString("lbname.AccessibleDescription");
			this.lbname.AccessibleName = resources.GetString("lbname.AccessibleName");
			this.lbname.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbname.Anchor")));
			this.lbname.AutoSize = ((bool)(resources.GetObject("lbname.AutoSize")));
			this.lbname.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbname.Dock")));
			this.lbname.Enabled = ((bool)(resources.GetObject("lbname.Enabled")));
			this.lbname.Font = ((System.Drawing.Font)(resources.GetObject("lbname.Font")));
			this.lbname.Image = ((System.Drawing.Image)(resources.GetObject("lbname.Image")));
			this.lbname.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbname.ImageAlign")));
			this.lbname.ImageIndex = ((int)(resources.GetObject("lbname.ImageIndex")));
			this.lbname.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbname.ImeMode")));
			this.lbname.Location = ((System.Drawing.Point)(resources.GetObject("lbname.Location")));
			this.lbname.Name = "lbname";
			this.lbname.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbname.RightToLeft")));
			this.lbname.Size = ((System.Drawing.Size)(resources.GetObject("lbname.Size")));
			this.lbname.TabIndex = ((int)(resources.GetObject("lbname.TabIndex")));
			this.lbname.Text = resources.GetString("lbname.Text");
			this.lbname.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbname.TextAlign")));
			this.lbname.Visible = ((bool)(resources.GetObject("lbname.Visible")));
			// 
			// tbaction
			// 
			this.tbaction.AccessibleDescription = resources.GetString("tbaction.AccessibleDescription");
			this.tbaction.AccessibleName = resources.GetString("tbaction.AccessibleName");
			this.tbaction.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbaction.Anchor")));
			this.tbaction.AutoSize = ((bool)(resources.GetObject("tbaction.AutoSize")));
			this.tbaction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbaction.BackgroundImage")));
			this.tbaction.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbaction.Dock")));
			this.tbaction.Enabled = ((bool)(resources.GetObject("tbaction.Enabled")));
			this.tbaction.Font = ((System.Drawing.Font)(resources.GetObject("tbaction.Font")));
			this.tbaction.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbaction.ImeMode")));
			this.tbaction.Location = ((System.Drawing.Point)(resources.GetObject("tbaction.Location")));
			this.tbaction.MaxLength = ((int)(resources.GetObject("tbaction.MaxLength")));
			this.tbaction.Multiline = ((bool)(resources.GetObject("tbaction.Multiline")));
			this.tbaction.Name = "tbaction";
			this.tbaction.PasswordChar = ((char)(resources.GetObject("tbaction.PasswordChar")));
			this.tbaction.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbaction.RightToLeft")));
			this.tbaction.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbaction.ScrollBars")));
			this.tbaction.Size = ((System.Drawing.Size)(resources.GetObject("tbaction.Size")));
			this.tbaction.TabIndex = ((int)(resources.GetObject("tbaction.TabIndex")));
			this.tbaction.Text = resources.GetString("tbaction.Text");
			this.tbaction.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbaction.TextAlign")));
			this.tbaction.Visible = ((bool)(resources.GetObject("tbaction.Visible")));
			this.tbaction.WordWrap = ((bool)(resources.GetObject("tbaction.WordWrap")));
			this.tbaction.TextChanged += new System.EventHandler(this.AutoChangeObjf);
			// 
			// label15
			// 
			this.label15.AccessibleDescription = resources.GetString("label15.AccessibleDescription");
			this.label15.AccessibleName = resources.GetString("label15.AccessibleName");
			this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label15.Anchor")));
			this.label15.AutoSize = ((bool)(resources.GetObject("label15.AutoSize")));
			this.label15.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label15.Dock")));
			this.label15.Enabled = ((bool)(resources.GetObject("label15.Enabled")));
			this.label15.Font = ((System.Drawing.Font)(resources.GetObject("label15.Font")));
			this.label15.Image = ((System.Drawing.Image)(resources.GetObject("label15.Image")));
			this.label15.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label15.ImageAlign")));
			this.label15.ImageIndex = ((int)(resources.GetObject("label15.ImageIndex")));
			this.label15.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label15.ImeMode")));
			this.label15.Location = ((System.Drawing.Point)(resources.GetObject("label15.Location")));
			this.label15.Name = "label15";
			this.label15.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label15.RightToLeft")));
			this.label15.Size = ((System.Drawing.Size)(resources.GetObject("label15.Size")));
			this.label15.TabIndex = ((int)(resources.GetObject("label15.TabIndex")));
			this.label15.Text = resources.GetString("label15.Text");
			this.label15.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label15.TextAlign")));
			this.label15.Visible = ((bool)(resources.GetObject("label15.Visible")));
			// 
			// llchangeobjf
			// 
			this.llchangeobjf.AccessibleDescription = resources.GetString("llchangeobjf.AccessibleDescription");
			this.llchangeobjf.AccessibleName = resources.GetString("llchangeobjf.AccessibleName");
			this.llchangeobjf.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llchangeobjf.Anchor")));
			this.llchangeobjf.AutoSize = ((bool)(resources.GetObject("llchangeobjf.AutoSize")));
			this.llchangeobjf.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llchangeobjf.Dock")));
			this.llchangeobjf.Enabled = ((bool)(resources.GetObject("llchangeobjf.Enabled")));
			this.llchangeobjf.Font = ((System.Drawing.Font)(resources.GetObject("llchangeobjf.Font")));
			this.llchangeobjf.Image = ((System.Drawing.Image)(resources.GetObject("llchangeobjf.Image")));
			this.llchangeobjf.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llchangeobjf.ImageAlign")));
			this.llchangeobjf.ImageIndex = ((int)(resources.GetObject("llchangeobjf.ImageIndex")));
			this.llchangeobjf.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llchangeobjf.ImeMode")));
			this.llchangeobjf.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llchangeobjf.LinkArea")));
			this.llchangeobjf.Location = ((System.Drawing.Point)(resources.GetObject("llchangeobjf.Location")));
			this.llchangeobjf.Name = "llchangeobjf";
			this.llchangeobjf.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llchangeobjf.RightToLeft")));
			this.llchangeobjf.Size = ((System.Drawing.Size)(resources.GetObject("llchangeobjf.Size")));
			this.llchangeobjf.TabIndex = ((int)(resources.GetObject("llchangeobjf.TabIndex")));
			this.llchangeobjf.TabStop = true;
			this.llchangeobjf.Text = resources.GetString("llchangeobjf.Text");
			this.llchangeobjf.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llchangeobjf.TextAlign")));
			this.llchangeobjf.Visible = ((bool)(resources.GetObject("llchangeobjf.Visible")));
			this.llchangeobjf.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ObjfChanged);
			// 
			// tbguard
			// 
			this.tbguard.AccessibleDescription = resources.GetString("tbguard.AccessibleDescription");
			this.tbguard.AccessibleName = resources.GetString("tbguard.AccessibleName");
			this.tbguard.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbguard.Anchor")));
			this.tbguard.AutoSize = ((bool)(resources.GetObject("tbguard.AutoSize")));
			this.tbguard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbguard.BackgroundImage")));
			this.tbguard.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbguard.Dock")));
			this.tbguard.Enabled = ((bool)(resources.GetObject("tbguard.Enabled")));
			this.tbguard.Font = ((System.Drawing.Font)(resources.GetObject("tbguard.Font")));
			this.tbguard.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbguard.ImeMode")));
			this.tbguard.Location = ((System.Drawing.Point)(resources.GetObject("tbguard.Location")));
			this.tbguard.MaxLength = ((int)(resources.GetObject("tbguard.MaxLength")));
			this.tbguard.Multiline = ((bool)(resources.GetObject("tbguard.Multiline")));
			this.tbguard.Name = "tbguard";
			this.tbguard.PasswordChar = ((char)(resources.GetObject("tbguard.PasswordChar")));
			this.tbguard.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbguard.RightToLeft")));
			this.tbguard.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbguard.ScrollBars")));
			this.tbguard.Size = ((System.Drawing.Size)(resources.GetObject("tbguard.Size")));
			this.tbguard.TabIndex = ((int)(resources.GetObject("tbguard.TabIndex")));
			this.tbguard.Text = resources.GetString("tbguard.Text");
			this.tbguard.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbguard.TextAlign")));
			this.tbguard.Visible = ((bool)(resources.GetObject("tbguard.Visible")));
			this.tbguard.WordWrap = ((bool)(resources.GetObject("tbguard.WordWrap")));
			this.tbguard.TextChanged += new System.EventHandler(this.AutoChangeObjf);
			// 
			// label8
			// 
			this.label8.AccessibleDescription = resources.GetString("label8.AccessibleDescription");
			this.label8.AccessibleName = resources.GetString("label8.AccessibleName");
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label8.Anchor")));
			this.label8.AutoSize = ((bool)(resources.GetObject("label8.AutoSize")));
			this.label8.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label8.Dock")));
			this.label8.Enabled = ((bool)(resources.GetObject("label8.Enabled")));
			this.label8.Font = ((System.Drawing.Font)(resources.GetObject("label8.Font")));
			this.label8.Image = ((System.Drawing.Image)(resources.GetObject("label8.Image")));
			this.label8.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label8.ImageAlign")));
			this.label8.ImageIndex = ((int)(resources.GetObject("label8.ImageIndex")));
			this.label8.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label8.ImeMode")));
			this.label8.Location = ((System.Drawing.Point)(resources.GetObject("label8.Location")));
			this.label8.Name = "label8";
			this.label8.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label8.RightToLeft")));
			this.label8.Size = ((System.Drawing.Size)(resources.GetObject("label8.Size")));
			this.label8.TabIndex = ((int)(resources.GetObject("label8.TabIndex")));
			this.label8.Text = resources.GetString("label8.Text");
			this.label8.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label8.TextAlign")));
			this.label8.Visible = ((bool)(resources.GetObject("label8.Visible")));
			// 
			// lbobjf
			// 
			this.lbobjf.AccessibleDescription = resources.GetString("lbobjf.AccessibleDescription");
			this.lbobjf.AccessibleName = resources.GetString("lbobjf.AccessibleName");
			this.lbobjf.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbobjf.Anchor")));
			this.lbobjf.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lbobjf.BackgroundImage")));
			this.lbobjf.ColumnWidth = ((int)(resources.GetObject("lbobjf.ColumnWidth")));
			this.lbobjf.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbobjf.Dock")));
			this.lbobjf.Enabled = ((bool)(resources.GetObject("lbobjf.Enabled")));
			this.lbobjf.Font = ((System.Drawing.Font)(resources.GetObject("lbobjf.Font")));
			this.lbobjf.HorizontalExtent = ((int)(resources.GetObject("lbobjf.HorizontalExtent")));
			this.lbobjf.HorizontalScrollbar = ((bool)(resources.GetObject("lbobjf.HorizontalScrollbar")));
			this.lbobjf.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbobjf.ImeMode")));
			this.lbobjf.IntegralHeight = ((bool)(resources.GetObject("lbobjf.IntegralHeight")));
			this.lbobjf.ItemHeight = ((int)(resources.GetObject("lbobjf.ItemHeight")));
			this.lbobjf.Location = ((System.Drawing.Point)(resources.GetObject("lbobjf.Location")));
			this.lbobjf.Name = "lbobjf";
			this.lbobjf.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbobjf.RightToLeft")));
			this.lbobjf.ScrollAlwaysVisible = ((bool)(resources.GetObject("lbobjf.ScrollAlwaysVisible")));
			this.lbobjf.Size = ((System.Drawing.Size)(resources.GetObject("lbobjf.Size")));
			this.lbobjf.TabIndex = ((int)(resources.GetObject("lbobjf.TabIndex")));
			this.lbobjf.Visible = ((bool)(resources.GetObject("lbobjf.Visible")));
			this.lbobjf.SelectedIndexChanged += new System.EventHandler(this.ObjfChanged);
			// 
			// btcommitobjf
			// 
			this.btcommitobjf.AccessibleDescription = resources.GetString("btcommitobjf.AccessibleDescription");
			this.btcommitobjf.AccessibleName = resources.GetString("btcommitobjf.AccessibleName");
			this.btcommitobjf.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btcommitobjf.Anchor")));
			this.btcommitobjf.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btcommitobjf.BackgroundImage")));
			this.btcommitobjf.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btcommitobjf.Dock")));
			this.btcommitobjf.Enabled = ((bool)(resources.GetObject("btcommitobjf.Enabled")));
			this.btcommitobjf.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btcommitobjf.FlatStyle")));
			this.btcommitobjf.Font = ((System.Drawing.Font)(resources.GetObject("btcommitobjf.Font")));
			this.btcommitobjf.Image = ((System.Drawing.Image)(resources.GetObject("btcommitobjf.Image")));
			this.btcommitobjf.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btcommitobjf.ImageAlign")));
			this.btcommitobjf.ImageIndex = ((int)(resources.GetObject("btcommitobjf.ImageIndex")));
			this.btcommitobjf.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btcommitobjf.ImeMode")));
			this.btcommitobjf.Location = ((System.Drawing.Point)(resources.GetObject("btcommitobjf.Location")));
			this.btcommitobjf.Name = "btcommitobjf";
			this.btcommitobjf.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btcommitobjf.RightToLeft")));
			this.btcommitobjf.Size = ((System.Drawing.Size)(resources.GetObject("btcommitobjf.Size")));
			this.btcommitobjf.TabIndex = ((int)(resources.GetObject("btcommitobjf.TabIndex")));
			this.btcommitobjf.Text = resources.GetString("btcommitobjf.Text");
			this.btcommitobjf.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btcommitobjf.TextAlign")));
			this.btcommitobjf.Visible = ((bool)(resources.GetObject("btcommitobjf.Visible")));
			this.btcommitobjf.Click += new System.EventHandler(this.ObjfCommit);
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
			this.panel4.Controls.Add(this.lbobjffile);
			this.panel4.Controls.Add(this.label17);
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
			// lbobjffile
			// 
			this.lbobjffile.AccessibleDescription = resources.GetString("lbobjffile.AccessibleDescription");
			this.lbobjffile.AccessibleName = resources.GetString("lbobjffile.AccessibleName");
			this.lbobjffile.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbobjffile.Anchor")));
			this.lbobjffile.AutoSize = ((bool)(resources.GetObject("lbobjffile.AutoSize")));
			this.lbobjffile.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbobjffile.Dock")));
			this.lbobjffile.Enabled = ((bool)(resources.GetObject("lbobjffile.Enabled")));
			this.lbobjffile.Font = ((System.Drawing.Font)(resources.GetObject("lbobjffile.Font")));
			this.lbobjffile.Image = ((System.Drawing.Image)(resources.GetObject("lbobjffile.Image")));
			this.lbobjffile.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbobjffile.ImageAlign")));
			this.lbobjffile.ImageIndex = ((int)(resources.GetObject("lbobjffile.ImageIndex")));
			this.lbobjffile.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbobjffile.ImeMode")));
			this.lbobjffile.Location = ((System.Drawing.Point)(resources.GetObject("lbobjffile.Location")));
			this.lbobjffile.Name = "lbobjffile";
			this.lbobjffile.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbobjffile.RightToLeft")));
			this.lbobjffile.Size = ((System.Drawing.Size)(resources.GetObject("lbobjffile.Size")));
			this.lbobjffile.TabIndex = ((int)(resources.GetObject("lbobjffile.TabIndex")));
			this.lbobjffile.Text = resources.GetString("lbobjffile.Text");
			this.lbobjffile.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbobjffile.TextAlign")));
			this.lbobjffile.Visible = ((bool)(resources.GetObject("lbobjffile.Visible")));
			// 
			// label17
			// 
			this.label17.AccessibleDescription = resources.GetString("label17.AccessibleDescription");
			this.label17.AccessibleName = resources.GetString("label17.AccessibleName");
			this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label17.Anchor")));
			this.label17.AutoSize = ((bool)(resources.GetObject("label17.AutoSize")));
			this.label17.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label17.Dock")));
			this.label17.Enabled = ((bool)(resources.GetObject("label17.Enabled")));
			this.label17.Font = ((System.Drawing.Font)(resources.GetObject("label17.Font")));
			this.label17.Image = ((System.Drawing.Image)(resources.GetObject("label17.Image")));
			this.label17.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label17.ImageAlign")));
			this.label17.ImageIndex = ((int)(resources.GetObject("label17.ImageIndex")));
			this.label17.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label17.ImeMode")));
			this.label17.Location = ((System.Drawing.Point)(resources.GetObject("label17.Location")));
			this.label17.Name = "label17";
			this.label17.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label17.RightToLeft")));
			this.label17.Size = ((System.Drawing.Size)(resources.GetObject("label17.Size")));
			this.label17.TabIndex = ((int)(resources.GetObject("label17.TabIndex")));
			this.label17.Text = resources.GetString("label17.Text");
			this.label17.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label17.TextAlign")));
			this.label17.Visible = ((bool)(resources.GetObject("label17.Visible")));
			// 
			// label19
			// 
			this.label19.AccessibleDescription = resources.GetString("label19.AccessibleDescription");
			this.label19.AccessibleName = resources.GetString("label19.AccessibleName");
			this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label19.Anchor")));
			this.label19.AutoSize = ((bool)(resources.GetObject("label19.AutoSize")));
			this.label19.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label19.Dock")));
			this.label19.Enabled = ((bool)(resources.GetObject("label19.Enabled")));
			this.label19.Font = ((System.Drawing.Font)(resources.GetObject("label19.Font")));
			this.label19.Image = ((System.Drawing.Image)(resources.GetObject("label19.Image")));
			this.label19.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label19.ImageAlign")));
			this.label19.ImageIndex = ((int)(resources.GetObject("label19.ImageIndex")));
			this.label19.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label19.ImeMode")));
			this.label19.Location = ((System.Drawing.Point)(resources.GetObject("label19.Location")));
			this.label19.Name = "label19";
			this.label19.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label19.RightToLeft")));
			this.label19.Size = ((System.Drawing.Size)(resources.GetObject("label19.Size")));
			this.label19.TabIndex = ((int)(resources.GetObject("label19.TabIndex")));
			this.label19.Text = resources.GetString("label19.Text");
			this.label19.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label19.TextAlign")));
			this.label19.Visible = ((bool)(resources.GetObject("label19.Visible")));
			// 
			// menuItem1
			// 
			this.menuItem1.Enabled = ((bool)(resources.GetObject("menuItem1.Enabled")));
			this.menuItem1.Index = -1;
			this.menuItem1.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem1.Shortcut")));
			this.menuItem1.ShowShortcut = ((bool)(resources.GetObject("menuItem1.ShowShortcut")));
			this.menuItem1.Text = resources.GetString("menuItem1.Text");
			this.menuItem1.Visible = ((bool)(resources.GetObject("menuItem1.Visible")));
			// 
			// menuItem2
			// 
			this.menuItem2.Enabled = ((bool)(resources.GetObject("menuItem2.Enabled")));
			this.menuItem2.Index = -1;
			this.menuItem2.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem2.Shortcut")));
			this.menuItem2.ShowShortcut = ((bool)(resources.GetObject("menuItem2.ShowShortcut")));
			this.menuItem2.Text = resources.GetString("menuItem2.Text");
			this.menuItem2.Visible = ((bool)(resources.GetObject("menuItem2.Visible")));
			// 
			// ObjfForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.objfPanel);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "ObjfForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.objfPanel.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.ResumeLayout(false);

		}


		private void GetObjfGuard(object sender, System.EventArgs e)
		{
			try 
			{
				Objf wrp = (Objf)wrapper;
				Bhav bhav = new Bhav(wrp.Opcodes);
				bhav.Package = wrp.Package;
				bhav.FileDescriptor = wrp.FileDescriptor;
				
				int opcode = SimPe.Plugin.WrapperFactory.BhavWizardForm.Execute(bhav, objfPanel.Parent);

				if (opcode != -1)
					tbguard.Text = "0x"+Helper.HexString((ushort)opcode);
			} 
			catch (Exception ex) 
			{
				
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		private void GetObjfAction(object sender, System.EventArgs e)
		{
			try 
			{
				Objf wrp = (Objf)wrapper;
				Bhav bhav = new Bhav(wrp.Opcodes);
				bhav.Package = wrp.Package;
				bhav.FileDescriptor = wrp.FileDescriptor;
				
				int opcode = SimPe.Plugin.WrapperFactory.BhavWizardForm.Execute(bhav, objfPanel.Parent);

				if (opcode != -1)
					tbaction.Text = "0x"+Helper.HexString((ushort)opcode);
			} 
			catch (Exception ex) 
			{
				
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		private void ObjfChanged(object sender, System.EventArgs e)
		{
			if (this.internalchg) return;
			this.llchangeobjf.Enabled = false;
			if (lbobjf.SelectedIndex <0) return;
			llchangeobjf.Enabled = true;

			try 
			{
				internalchg = true;
				ObjfItem item = (ObjfItem)lbobjf.Items[lbobjf.SelectedIndex];
				this.tbguard.Text = "0x"+Helper.HexString(item.Guardian);				
				this.tbaction.Text = "0x"+Helper.HexString(item.Action);
				lbname.Text = item.Name;
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			} 
			finally 
			{
				internalchg = false;
			}
		}

		private void ObjfCommit(object sender, System.EventArgs e)
		{
			try 
			{
				Objf wrp = (Objf)wrapper;				

				
				ObjfItem[] items = new ObjfItem[lbobjf.Items.Count];
				for (int i=0; i< items.Length; i++)
				{
					items[i] = (ObjfItem)lbobjf.Items[i];
				}
				wrp.Items = items;

				wrapper.SynchronizeUserData();
				MessageBox.Show(Localization.Manager.GetString("commited"));
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}
		}		

		private void ObjfChanged(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			llchangeobjf.Enabled = false;

			//creat new Item if non is selected
			
			ObjfItem item = null;
			try 
			{
				Objf wrp = (Objf)wrapper;
				item = new ObjfItem(wrp);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
				return;
			}

			try 
			{
				if (lbobjf.SelectedIndex >=0) 
				{
					item = (ObjfItem)lbobjf.Items[lbobjf.SelectedIndex];
					llchangeobjf.Enabled = true;
				}
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
				return;
			}
			

			try 
			{
				item.Guardian = Convert.ToUInt16(tbguard.Text, 16);
				item.Action = Convert.ToUInt16(tbaction.Text, 16);
				wrapper.Changed = true;

				this.internalchg = true;
				if (lbobjf.SelectedIndex <0) 
				{
					item.LineNumber = lbobjf.Items.Count;
					lbobjf.Items.Add(item);
				} 
				else 
				{
					lbobjf.Items[lbobjf.SelectedIndex] = item;
				}
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			} 
			finally 
			{
				this.internalchg = false;
			}
		}

		private void AutoChangeObjf(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			if (lbobjf.SelectedIndex>=0) ObjfChanged(null, null);
		}

		#endregion
	}
}
