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
	public class TtabForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.ListBox lbttab;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label lbttabfile;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.LinkLabel llchangettab;
		private System.Windows.Forms.TextBox tbttabaction;
		private System.Windows.Forms.TextBox tbttabguard;
		private System.Windows.Forms.TextBox tbinst2;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox tbinst1;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.GroupBox gbttab;
		private System.Windows.Forms.TextBox tbres6;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.TextBox tbres5;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.TextBox tbres2;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.TextBox tbres1;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.TextBox tbres8;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.TextBox tbres7;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.TextBox tbres4;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.TextBox tbres3;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ComboBox cbgroups;
		private System.Windows.Forms.ListBox lblist;
		private System.Windows.Forms.TextBox tblisttype;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.TextBox tbdelta;
		private System.Windows.Forms.Label label38;
		private System.Windows.Forms.TextBox tbmin;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.LinkLabel lllistchange;
		private System.Windows.Forms.TextBox tbpie;
		private System.Windows.Forms.Label label40;
		private System.Windows.Forms.TextBox tbver;
		private System.Windows.Forms.Label label41;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.CheckBox cbvisitor;
		private System.Windows.Forms.CheckBox cbjoinable;
		private System.Windows.Forms.CheckBox cbimmediately;
		private System.Windows.Forms.CheckBox cbconsecutive;
		private System.Windows.Forms.CheckBox cbchildren;
		private System.Windows.Forms.CheckBox cbdemochild;
		private System.Windows.Forms.CheckBox cbteens;
		private System.Windows.Forms.CheckBox cbelders;
		private System.Windows.Forms.CheckBox cbtodlers;
		private System.Windows.Forms.CheckBox cbautofirst;
		private System.Windows.Forms.CheckBox cbdebugmenu;
		private System.Windows.Forms.CheckBox cbadults;
		private System.Windows.Forms.CheckBox cbunk2;
		private System.Windows.Forms.CheckBox cbunk1;
		private System.Windows.Forms.CheckBox cbunk3;
		private System.Windows.Forms.CheckBox cbunk4;
		private System.Windows.Forms.ContextMenu cmcopy;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.Label lbguard;
		private System.Windows.Forms.Label lbaction;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.LinkLabel lldelttab;
		private System.Windows.Forms.Panel ttabPanel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
       
		public TtabForm()
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

		
		#region Ttab
		/// <summary>
		/// Stores the currently active Wrapper
		/// </summary>
		internal Ttab wrapper = null;
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
				return ttabPanel;
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
			//wrapper;
			wrapper = (Ttab)wrp;
			internalchg = true;
			llchangettab.Enabled = false;
			lllistchange.Enabled = false;

			lbttab.Items.Clear();
			foreach (TtabItem i in wrapper.Items) 
			{
				lbttab.Items.Add(i);
			}
			internalchg = false;
			if (lbttab.Items.Count>0) lbttab.SelectedIndex = 0;

			internalchg = true;
			tbver.Text = "0x"+Helper.HexString(wrapper.Format);
			lbttabfile.Text = wrapper.FileName;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TtabForm));
			this.ttabPanel = new System.Windows.Forms.Panel();
			this.lbttab = new System.Windows.Forms.ListBox();
			this.button2 = new System.Windows.Forms.Button();
			this.panel5 = new System.Windows.Forms.Panel();
			this.lbttabfile = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.gbttab = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.lllistchange = new System.Windows.Forms.LinkLabel();
			this.tblisttype = new System.Windows.Forms.TextBox();
			this.label37 = new System.Windows.Forms.Label();
			this.tbdelta = new System.Windows.Forms.TextBox();
			this.label38 = new System.Windows.Forms.Label();
			this.tbmin = new System.Windows.Forms.TextBox();
			this.label39 = new System.Windows.Forms.Label();
			this.lblist = new System.Windows.Forms.ListBox();
			this.cbgroups = new System.Windows.Forms.ComboBox();
			this.lldelttab = new System.Windows.Forms.LinkLabel();
			this.button6 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.lbaction = new System.Windows.Forms.Label();
			this.lbguard = new System.Windows.Forms.Label();
			this.tbver = new System.Windows.Forms.TextBox();
			this.label41 = new System.Windows.Forms.Label();
			this.tbpie = new System.Windows.Forms.TextBox();
			this.label40 = new System.Windows.Forms.Label();
			this.tbres8 = new System.Windows.Forms.TextBox();
			this.label33 = new System.Windows.Forms.Label();
			this.tbres7 = new System.Windows.Forms.TextBox();
			this.label34 = new System.Windows.Forms.Label();
			this.tbres4 = new System.Windows.Forms.TextBox();
			this.label35 = new System.Windows.Forms.Label();
			this.tbres3 = new System.Windows.Forms.TextBox();
			this.label36 = new System.Windows.Forms.Label();
			this.tbres6 = new System.Windows.Forms.TextBox();
			this.label29 = new System.Windows.Forms.Label();
			this.tbres5 = new System.Windows.Forms.TextBox();
			this.label30 = new System.Windows.Forms.Label();
			this.tbres2 = new System.Windows.Forms.TextBox();
			this.label31 = new System.Windows.Forms.Label();
			this.tbres1 = new System.Windows.Forms.TextBox();
			this.label32 = new System.Windows.Forms.Label();
			this.tbinst2 = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.tbinst1 = new System.Windows.Forms.TextBox();
			this.label24 = new System.Windows.Forms.Label();
			this.tbttabaction = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.llchangettab = new System.Windows.Forms.LinkLabel();
			this.tbttabguard = new System.Windows.Forms.TextBox();
			this.label23 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.cbunk3 = new System.Windows.Forms.CheckBox();
			this.cbunk4 = new System.Windows.Forms.CheckBox();
			this.cbunk1 = new System.Windows.Forms.CheckBox();
			this.cbunk2 = new System.Windows.Forms.CheckBox();
			this.cbteens = new System.Windows.Forms.CheckBox();
			this.cbelders = new System.Windows.Forms.CheckBox();
			this.cbtodlers = new System.Windows.Forms.CheckBox();
			this.cbautofirst = new System.Windows.Forms.CheckBox();
			this.cbdebugmenu = new System.Windows.Forms.CheckBox();
			this.cbadults = new System.Windows.Forms.CheckBox();
			this.cbdemochild = new System.Windows.Forms.CheckBox();
			this.cbchildren = new System.Windows.Forms.CheckBox();
			this.cbconsecutive = new System.Windows.Forms.CheckBox();
			this.cbimmediately = new System.Windows.Forms.CheckBox();
			this.cbjoinable = new System.Windows.Forms.CheckBox();
			this.cbvisitor = new System.Windows.Forms.CheckBox();
			this.cmcopy = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.ttabPanel.SuspendLayout();
			this.panel5.SuspendLayout();
			this.gbttab.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// ttabPanel
			// 
			this.ttabPanel.AccessibleDescription = resources.GetString("ttabPanel.AccessibleDescription");
			this.ttabPanel.AccessibleName = resources.GetString("ttabPanel.AccessibleName");
			this.ttabPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabPanel.Anchor")));
			this.ttabPanel.AutoScroll = ((bool)(resources.GetObject("ttabPanel.AutoScroll")));
			this.ttabPanel.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabPanel.AutoScrollMargin")));
			this.ttabPanel.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabPanel.AutoScrollMinSize")));
			this.ttabPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabPanel.BackgroundImage")));
			this.ttabPanel.Controls.Add(this.lbttab);
			this.ttabPanel.Controls.Add(this.button2);
			this.ttabPanel.Controls.Add(this.panel5);
			this.ttabPanel.Controls.Add(this.label26);
			this.ttabPanel.Controls.Add(this.gbttab);
			this.ttabPanel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabPanel.Dock")));
			this.ttabPanel.Enabled = ((bool)(resources.GetObject("ttabPanel.Enabled")));
			this.ttabPanel.Font = ((System.Drawing.Font)(resources.GetObject("ttabPanel.Font")));
			this.ttabPanel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabPanel.ImeMode")));
			this.ttabPanel.Location = ((System.Drawing.Point)(resources.GetObject("ttabPanel.Location")));
			this.ttabPanel.Name = "ttabPanel";
			this.ttabPanel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabPanel.RightToLeft")));
			this.ttabPanel.Size = ((System.Drawing.Size)(resources.GetObject("ttabPanel.Size")));
			this.ttabPanel.TabIndex = ((int)(resources.GetObject("ttabPanel.TabIndex")));
			this.ttabPanel.Text = resources.GetString("ttabPanel.Text");
			this.ttabPanel.Visible = ((bool)(resources.GetObject("ttabPanel.Visible")));
			// 
			// lbttab
			// 
			this.lbttab.AccessibleDescription = resources.GetString("lbttab.AccessibleDescription");
			this.lbttab.AccessibleName = resources.GetString("lbttab.AccessibleName");
			this.lbttab.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbttab.Anchor")));
			this.lbttab.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lbttab.BackgroundImage")));
			this.lbttab.ColumnWidth = ((int)(resources.GetObject("lbttab.ColumnWidth")));
			this.lbttab.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbttab.Dock")));
			this.lbttab.Enabled = ((bool)(resources.GetObject("lbttab.Enabled")));
			this.lbttab.Font = ((System.Drawing.Font)(resources.GetObject("lbttab.Font")));
			this.lbttab.HorizontalExtent = ((int)(resources.GetObject("lbttab.HorizontalExtent")));
			this.lbttab.HorizontalScrollbar = ((bool)(resources.GetObject("lbttab.HorizontalScrollbar")));
			this.lbttab.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbttab.ImeMode")));
			this.lbttab.IntegralHeight = ((bool)(resources.GetObject("lbttab.IntegralHeight")));
			this.lbttab.ItemHeight = ((int)(resources.GetObject("lbttab.ItemHeight")));
			this.lbttab.Location = ((System.Drawing.Point)(resources.GetObject("lbttab.Location")));
			this.lbttab.Name = "lbttab";
			this.lbttab.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbttab.RightToLeft")));
			this.lbttab.ScrollAlwaysVisible = ((bool)(resources.GetObject("lbttab.ScrollAlwaysVisible")));
			this.lbttab.Size = ((System.Drawing.Size)(resources.GetObject("lbttab.Size")));
			this.lbttab.TabIndex = ((int)(resources.GetObject("lbttab.TabIndex")));
			this.lbttab.Visible = ((bool)(resources.GetObject("lbttab.Visible")));
			this.lbttab.SelectedIndexChanged += new System.EventHandler(this.TtabSelect);
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
			this.button2.Click += new System.EventHandler(this.Ttabcommit);
			// 
			// panel5
			// 
			this.panel5.AccessibleDescription = resources.GetString("panel5.AccessibleDescription");
			this.panel5.AccessibleName = resources.GetString("panel5.AccessibleName");
			this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel5.Anchor")));
			this.panel5.AutoScroll = ((bool)(resources.GetObject("panel5.AutoScroll")));
			this.panel5.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel5.AutoScrollMargin")));
			this.panel5.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel5.AutoScrollMinSize")));
			this.panel5.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.panel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel5.BackgroundImage")));
			this.panel5.Controls.Add(this.lbttabfile);
			this.panel5.Controls.Add(this.label25);
			this.panel5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panel5.Dock")));
			this.panel5.Enabled = ((bool)(resources.GetObject("panel5.Enabled")));
			this.panel5.Font = ((System.Drawing.Font)(resources.GetObject("panel5.Font")));
			this.panel5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.panel5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panel5.ImeMode")));
			this.panel5.Location = ((System.Drawing.Point)(resources.GetObject("panel5.Location")));
			this.panel5.Name = "panel5";
			this.panel5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panel5.RightToLeft")));
			this.panel5.Size = ((System.Drawing.Size)(resources.GetObject("panel5.Size")));
			this.panel5.TabIndex = ((int)(resources.GetObject("panel5.TabIndex")));
			this.panel5.Text = resources.GetString("panel5.Text");
			this.panel5.Visible = ((bool)(resources.GetObject("panel5.Visible")));
			// 
			// lbttabfile
			// 
			this.lbttabfile.AccessibleDescription = resources.GetString("lbttabfile.AccessibleDescription");
			this.lbttabfile.AccessibleName = resources.GetString("lbttabfile.AccessibleName");
			this.lbttabfile.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbttabfile.Anchor")));
			this.lbttabfile.AutoSize = ((bool)(resources.GetObject("lbttabfile.AutoSize")));
			this.lbttabfile.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbttabfile.Dock")));
			this.lbttabfile.Enabled = ((bool)(resources.GetObject("lbttabfile.Enabled")));
			this.lbttabfile.Font = ((System.Drawing.Font)(resources.GetObject("lbttabfile.Font")));
			this.lbttabfile.Image = ((System.Drawing.Image)(resources.GetObject("lbttabfile.Image")));
			this.lbttabfile.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbttabfile.ImageAlign")));
			this.lbttabfile.ImageIndex = ((int)(resources.GetObject("lbttabfile.ImageIndex")));
			this.lbttabfile.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbttabfile.ImeMode")));
			this.lbttabfile.Location = ((System.Drawing.Point)(resources.GetObject("lbttabfile.Location")));
			this.lbttabfile.Name = "lbttabfile";
			this.lbttabfile.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbttabfile.RightToLeft")));
			this.lbttabfile.Size = ((System.Drawing.Size)(resources.GetObject("lbttabfile.Size")));
			this.lbttabfile.TabIndex = ((int)(resources.GetObject("lbttabfile.TabIndex")));
			this.lbttabfile.Text = resources.GetString("lbttabfile.Text");
			this.lbttabfile.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbttabfile.TextAlign")));
			this.lbttabfile.Visible = ((bool)(resources.GetObject("lbttabfile.Visible")));
			// 
			// label25
			// 
			this.label25.AccessibleDescription = resources.GetString("label25.AccessibleDescription");
			this.label25.AccessibleName = resources.GetString("label25.AccessibleName");
			this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label25.Anchor")));
			this.label25.AutoSize = ((bool)(resources.GetObject("label25.AutoSize")));
			this.label25.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label25.Dock")));
			this.label25.Enabled = ((bool)(resources.GetObject("label25.Enabled")));
			this.label25.Font = ((System.Drawing.Font)(resources.GetObject("label25.Font")));
			this.label25.Image = ((System.Drawing.Image)(resources.GetObject("label25.Image")));
			this.label25.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label25.ImageAlign")));
			this.label25.ImageIndex = ((int)(resources.GetObject("label25.ImageIndex")));
			this.label25.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label25.ImeMode")));
			this.label25.Location = ((System.Drawing.Point)(resources.GetObject("label25.Location")));
			this.label25.Name = "label25";
			this.label25.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label25.RightToLeft")));
			this.label25.Size = ((System.Drawing.Size)(resources.GetObject("label25.Size")));
			this.label25.TabIndex = ((int)(resources.GetObject("label25.TabIndex")));
			this.label25.Text = resources.GetString("label25.Text");
			this.label25.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label25.TextAlign")));
			this.label25.Visible = ((bool)(resources.GetObject("label25.Visible")));
			// 
			// label26
			// 
			this.label26.AccessibleDescription = resources.GetString("label26.AccessibleDescription");
			this.label26.AccessibleName = resources.GetString("label26.AccessibleName");
			this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label26.Anchor")));
			this.label26.AutoSize = ((bool)(resources.GetObject("label26.AutoSize")));
			this.label26.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label26.Dock")));
			this.label26.Enabled = ((bool)(resources.GetObject("label26.Enabled")));
			this.label26.Font = ((System.Drawing.Font)(resources.GetObject("label26.Font")));
			this.label26.Image = ((System.Drawing.Image)(resources.GetObject("label26.Image")));
			this.label26.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label26.ImageAlign")));
			this.label26.ImageIndex = ((int)(resources.GetObject("label26.ImageIndex")));
			this.label26.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label26.ImeMode")));
			this.label26.Location = ((System.Drawing.Point)(resources.GetObject("label26.Location")));
			this.label26.Name = "label26";
			this.label26.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label26.RightToLeft")));
			this.label26.Size = ((System.Drawing.Size)(resources.GetObject("label26.Size")));
			this.label26.TabIndex = ((int)(resources.GetObject("label26.TabIndex")));
			this.label26.Text = resources.GetString("label26.Text");
			this.label26.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label26.TextAlign")));
			this.label26.Visible = ((bool)(resources.GetObject("label26.Visible")));
			// 
			// gbttab
			// 
			this.gbttab.AccessibleDescription = resources.GetString("gbttab.AccessibleDescription");
			this.gbttab.AccessibleName = resources.GetString("gbttab.AccessibleName");
			this.gbttab.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gbttab.Anchor")));
			this.gbttab.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gbttab.BackgroundImage")));
			this.gbttab.Controls.Add(this.groupBox3);
			this.gbttab.Controls.Add(this.lldelttab);
			this.gbttab.Controls.Add(this.button6);
			this.gbttab.Controls.Add(this.button5);
			this.gbttab.Controls.Add(this.lbaction);
			this.gbttab.Controls.Add(this.lbguard);
			this.gbttab.Controls.Add(this.tbver);
			this.gbttab.Controls.Add(this.label41);
			this.gbttab.Controls.Add(this.tbpie);
			this.gbttab.Controls.Add(this.label40);
			this.gbttab.Controls.Add(this.tbres8);
			this.gbttab.Controls.Add(this.label33);
			this.gbttab.Controls.Add(this.tbres7);
			this.gbttab.Controls.Add(this.label34);
			this.gbttab.Controls.Add(this.tbres4);
			this.gbttab.Controls.Add(this.label35);
			this.gbttab.Controls.Add(this.tbres3);
			this.gbttab.Controls.Add(this.label36);
			this.gbttab.Controls.Add(this.tbres6);
			this.gbttab.Controls.Add(this.label29);
			this.gbttab.Controls.Add(this.tbres5);
			this.gbttab.Controls.Add(this.label30);
			this.gbttab.Controls.Add(this.tbres2);
			this.gbttab.Controls.Add(this.label31);
			this.gbttab.Controls.Add(this.tbres1);
			this.gbttab.Controls.Add(this.label32);
			this.gbttab.Controls.Add(this.tbinst2);
			this.gbttab.Controls.Add(this.label20);
			this.gbttab.Controls.Add(this.tbinst1);
			this.gbttab.Controls.Add(this.label24);
			this.gbttab.Controls.Add(this.tbttabaction);
			this.gbttab.Controls.Add(this.label21);
			this.gbttab.Controls.Add(this.linkLabel1);
			this.gbttab.Controls.Add(this.llchangettab);
			this.gbttab.Controls.Add(this.tbttabguard);
			this.gbttab.Controls.Add(this.label23);
			this.gbttab.Controls.Add(this.groupBox4);
			this.gbttab.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("gbttab.Dock")));
			this.gbttab.Enabled = ((bool)(resources.GetObject("gbttab.Enabled")));
			this.gbttab.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.gbttab.Font = ((System.Drawing.Font)(resources.GetObject("gbttab.Font")));
			this.gbttab.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gbttab.ImeMode")));
			this.gbttab.Location = ((System.Drawing.Point)(resources.GetObject("gbttab.Location")));
			this.gbttab.Name = "gbttab";
			this.gbttab.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("gbttab.RightToLeft")));
			this.gbttab.Size = ((System.Drawing.Size)(resources.GetObject("gbttab.Size")));
			this.gbttab.TabIndex = ((int)(resources.GetObject("gbttab.TabIndex")));
			this.gbttab.TabStop = false;
			this.gbttab.Text = resources.GetString("gbttab.Text");
			this.gbttab.Visible = ((bool)(resources.GetObject("gbttab.Visible")));
			// 
			// groupBox3
			// 
			this.groupBox3.AccessibleDescription = resources.GetString("groupBox3.AccessibleDescription");
			this.groupBox3.AccessibleName = resources.GetString("groupBox3.AccessibleName");
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox3.Anchor")));
			this.groupBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox3.BackgroundImage")));
			this.groupBox3.Controls.Add(this.lllistchange);
			this.groupBox3.Controls.Add(this.tblisttype);
			this.groupBox3.Controls.Add(this.label37);
			this.groupBox3.Controls.Add(this.tbdelta);
			this.groupBox3.Controls.Add(this.label38);
			this.groupBox3.Controls.Add(this.tbmin);
			this.groupBox3.Controls.Add(this.label39);
			this.groupBox3.Controls.Add(this.lblist);
			this.groupBox3.Controls.Add(this.cbgroups);
			this.groupBox3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("groupBox3.Dock")));
			this.groupBox3.Enabled = ((bool)(resources.GetObject("groupBox3.Enabled")));
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Font = ((System.Drawing.Font)(resources.GetObject("groupBox3.Font")));
			this.groupBox3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("groupBox3.ImeMode")));
			this.groupBox3.Location = ((System.Drawing.Point)(resources.GetObject("groupBox3.Location")));
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("groupBox3.RightToLeft")));
			this.groupBox3.Size = ((System.Drawing.Size)(resources.GetObject("groupBox3.Size")));
			this.groupBox3.TabIndex = ((int)(resources.GetObject("groupBox3.TabIndex")));
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = resources.GetString("groupBox3.Text");
			this.groupBox3.Visible = ((bool)(resources.GetObject("groupBox3.Visible")));
			// 
			// lllistchange
			// 
			this.lllistchange.AccessibleDescription = resources.GetString("lllistchange.AccessibleDescription");
			this.lllistchange.AccessibleName = resources.GetString("lllistchange.AccessibleName");
			this.lllistchange.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lllistchange.Anchor")));
			this.lllistchange.AutoSize = ((bool)(resources.GetObject("lllistchange.AutoSize")));
			this.lllistchange.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lllistchange.Dock")));
			this.lllistchange.Enabled = ((bool)(resources.GetObject("lllistchange.Enabled")));
			this.lllistchange.Font = ((System.Drawing.Font)(resources.GetObject("lllistchange.Font")));
			this.lllistchange.Image = ((System.Drawing.Image)(resources.GetObject("lllistchange.Image")));
			this.lllistchange.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lllistchange.ImageAlign")));
			this.lllistchange.ImageIndex = ((int)(resources.GetObject("lllistchange.ImageIndex")));
			this.lllistchange.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lllistchange.ImeMode")));
			this.lllistchange.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("lllistchange.LinkArea")));
			this.lllistchange.Location = ((System.Drawing.Point)(resources.GetObject("lllistchange.Location")));
			this.lllistchange.Name = "lllistchange";
			this.lllistchange.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lllistchange.RightToLeft")));
			this.lllistchange.Size = ((System.Drawing.Size)(resources.GetObject("lllistchange.Size")));
			this.lllistchange.TabIndex = ((int)(resources.GetObject("lllistchange.TabIndex")));
			this.lllistchange.TabStop = true;
			this.lllistchange.Text = resources.GetString("lllistchange.Text");
			this.lllistchange.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lllistchange.TextAlign")));
			this.lllistchange.Visible = ((bool)(resources.GetObject("lllistchange.Visible")));
			this.lllistchange.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ListChangeClick);
			// 
			// tblisttype
			// 
			this.tblisttype.AccessibleDescription = resources.GetString("tblisttype.AccessibleDescription");
			this.tblisttype.AccessibleName = resources.GetString("tblisttype.AccessibleName");
			this.tblisttype.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tblisttype.Anchor")));
			this.tblisttype.AutoSize = ((bool)(resources.GetObject("tblisttype.AutoSize")));
			this.tblisttype.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tblisttype.BackgroundImage")));
			this.tblisttype.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tblisttype.Dock")));
			this.tblisttype.Enabled = ((bool)(resources.GetObject("tblisttype.Enabled")));
			this.tblisttype.Font = ((System.Drawing.Font)(resources.GetObject("tblisttype.Font")));
			this.tblisttype.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tblisttype.ImeMode")));
			this.tblisttype.Location = ((System.Drawing.Point)(resources.GetObject("tblisttype.Location")));
			this.tblisttype.MaxLength = ((int)(resources.GetObject("tblisttype.MaxLength")));
			this.tblisttype.Multiline = ((bool)(resources.GetObject("tblisttype.Multiline")));
			this.tblisttype.Name = "tblisttype";
			this.tblisttype.PasswordChar = ((char)(resources.GetObject("tblisttype.PasswordChar")));
			this.tblisttype.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tblisttype.RightToLeft")));
			this.tblisttype.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tblisttype.ScrollBars")));
			this.tblisttype.Size = ((System.Drawing.Size)(resources.GetObject("tblisttype.Size")));
			this.tblisttype.TabIndex = ((int)(resources.GetObject("tblisttype.TabIndex")));
			this.tblisttype.Text = resources.GetString("tblisttype.Text");
			this.tblisttype.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tblisttype.TextAlign")));
			this.tblisttype.Visible = ((bool)(resources.GetObject("tblisttype.Visible")));
			this.tblisttype.WordWrap = ((bool)(resources.GetObject("tblisttype.WordWrap")));
			this.tblisttype.TextChanged += new System.EventHandler(this.AutoChangeMotive);
			// 
			// label37
			// 
			this.label37.AccessibleDescription = resources.GetString("label37.AccessibleDescription");
			this.label37.AccessibleName = resources.GetString("label37.AccessibleName");
			this.label37.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label37.Anchor")));
			this.label37.AutoSize = ((bool)(resources.GetObject("label37.AutoSize")));
			this.label37.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label37.Dock")));
			this.label37.Enabled = ((bool)(resources.GetObject("label37.Enabled")));
			this.label37.Font = ((System.Drawing.Font)(resources.GetObject("label37.Font")));
			this.label37.Image = ((System.Drawing.Image)(resources.GetObject("label37.Image")));
			this.label37.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label37.ImageAlign")));
			this.label37.ImageIndex = ((int)(resources.GetObject("label37.ImageIndex")));
			this.label37.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label37.ImeMode")));
			this.label37.Location = ((System.Drawing.Point)(resources.GetObject("label37.Location")));
			this.label37.Name = "label37";
			this.label37.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label37.RightToLeft")));
			this.label37.Size = ((System.Drawing.Size)(resources.GetObject("label37.Size")));
			this.label37.TabIndex = ((int)(resources.GetObject("label37.TabIndex")));
			this.label37.Text = resources.GetString("label37.Text");
			this.label37.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label37.TextAlign")));
			this.label37.Visible = ((bool)(resources.GetObject("label37.Visible")));
			// 
			// tbdelta
			// 
			this.tbdelta.AccessibleDescription = resources.GetString("tbdelta.AccessibleDescription");
			this.tbdelta.AccessibleName = resources.GetString("tbdelta.AccessibleName");
			this.tbdelta.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbdelta.Anchor")));
			this.tbdelta.AutoSize = ((bool)(resources.GetObject("tbdelta.AutoSize")));
			this.tbdelta.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbdelta.BackgroundImage")));
			this.tbdelta.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbdelta.Dock")));
			this.tbdelta.Enabled = ((bool)(resources.GetObject("tbdelta.Enabled")));
			this.tbdelta.Font = ((System.Drawing.Font)(resources.GetObject("tbdelta.Font")));
			this.tbdelta.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbdelta.ImeMode")));
			this.tbdelta.Location = ((System.Drawing.Point)(resources.GetObject("tbdelta.Location")));
			this.tbdelta.MaxLength = ((int)(resources.GetObject("tbdelta.MaxLength")));
			this.tbdelta.Multiline = ((bool)(resources.GetObject("tbdelta.Multiline")));
			this.tbdelta.Name = "tbdelta";
			this.tbdelta.PasswordChar = ((char)(resources.GetObject("tbdelta.PasswordChar")));
			this.tbdelta.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbdelta.RightToLeft")));
			this.tbdelta.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbdelta.ScrollBars")));
			this.tbdelta.Size = ((System.Drawing.Size)(resources.GetObject("tbdelta.Size")));
			this.tbdelta.TabIndex = ((int)(resources.GetObject("tbdelta.TabIndex")));
			this.tbdelta.Text = resources.GetString("tbdelta.Text");
			this.tbdelta.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbdelta.TextAlign")));
			this.tbdelta.Visible = ((bool)(resources.GetObject("tbdelta.Visible")));
			this.tbdelta.WordWrap = ((bool)(resources.GetObject("tbdelta.WordWrap")));
			this.tbdelta.TextChanged += new System.EventHandler(this.AutoChangeMotive);
			// 
			// label38
			// 
			this.label38.AccessibleDescription = resources.GetString("label38.AccessibleDescription");
			this.label38.AccessibleName = resources.GetString("label38.AccessibleName");
			this.label38.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label38.Anchor")));
			this.label38.AutoSize = ((bool)(resources.GetObject("label38.AutoSize")));
			this.label38.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label38.Dock")));
			this.label38.Enabled = ((bool)(resources.GetObject("label38.Enabled")));
			this.label38.Font = ((System.Drawing.Font)(resources.GetObject("label38.Font")));
			this.label38.Image = ((System.Drawing.Image)(resources.GetObject("label38.Image")));
			this.label38.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label38.ImageAlign")));
			this.label38.ImageIndex = ((int)(resources.GetObject("label38.ImageIndex")));
			this.label38.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label38.ImeMode")));
			this.label38.Location = ((System.Drawing.Point)(resources.GetObject("label38.Location")));
			this.label38.Name = "label38";
			this.label38.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label38.RightToLeft")));
			this.label38.Size = ((System.Drawing.Size)(resources.GetObject("label38.Size")));
			this.label38.TabIndex = ((int)(resources.GetObject("label38.TabIndex")));
			this.label38.Text = resources.GetString("label38.Text");
			this.label38.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label38.TextAlign")));
			this.label38.Visible = ((bool)(resources.GetObject("label38.Visible")));
			// 
			// tbmin
			// 
			this.tbmin.AccessibleDescription = resources.GetString("tbmin.AccessibleDescription");
			this.tbmin.AccessibleName = resources.GetString("tbmin.AccessibleName");
			this.tbmin.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbmin.Anchor")));
			this.tbmin.AutoSize = ((bool)(resources.GetObject("tbmin.AutoSize")));
			this.tbmin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbmin.BackgroundImage")));
			this.tbmin.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbmin.Dock")));
			this.tbmin.Enabled = ((bool)(resources.GetObject("tbmin.Enabled")));
			this.tbmin.Font = ((System.Drawing.Font)(resources.GetObject("tbmin.Font")));
			this.tbmin.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbmin.ImeMode")));
			this.tbmin.Location = ((System.Drawing.Point)(resources.GetObject("tbmin.Location")));
			this.tbmin.MaxLength = ((int)(resources.GetObject("tbmin.MaxLength")));
			this.tbmin.Multiline = ((bool)(resources.GetObject("tbmin.Multiline")));
			this.tbmin.Name = "tbmin";
			this.tbmin.PasswordChar = ((char)(resources.GetObject("tbmin.PasswordChar")));
			this.tbmin.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbmin.RightToLeft")));
			this.tbmin.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbmin.ScrollBars")));
			this.tbmin.Size = ((System.Drawing.Size)(resources.GetObject("tbmin.Size")));
			this.tbmin.TabIndex = ((int)(resources.GetObject("tbmin.TabIndex")));
			this.tbmin.Text = resources.GetString("tbmin.Text");
			this.tbmin.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbmin.TextAlign")));
			this.tbmin.Visible = ((bool)(resources.GetObject("tbmin.Visible")));
			this.tbmin.WordWrap = ((bool)(resources.GetObject("tbmin.WordWrap")));
			this.tbmin.TextChanged += new System.EventHandler(this.AutoChangeMotive);
			// 
			// label39
			// 
			this.label39.AccessibleDescription = resources.GetString("label39.AccessibleDescription");
			this.label39.AccessibleName = resources.GetString("label39.AccessibleName");
			this.label39.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label39.Anchor")));
			this.label39.AutoSize = ((bool)(resources.GetObject("label39.AutoSize")));
			this.label39.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label39.Dock")));
			this.label39.Enabled = ((bool)(resources.GetObject("label39.Enabled")));
			this.label39.Font = ((System.Drawing.Font)(resources.GetObject("label39.Font")));
			this.label39.Image = ((System.Drawing.Image)(resources.GetObject("label39.Image")));
			this.label39.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label39.ImageAlign")));
			this.label39.ImageIndex = ((int)(resources.GetObject("label39.ImageIndex")));
			this.label39.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label39.ImeMode")));
			this.label39.Location = ((System.Drawing.Point)(resources.GetObject("label39.Location")));
			this.label39.Name = "label39";
			this.label39.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label39.RightToLeft")));
			this.label39.Size = ((System.Drawing.Size)(resources.GetObject("label39.Size")));
			this.label39.TabIndex = ((int)(resources.GetObject("label39.TabIndex")));
			this.label39.Text = resources.GetString("label39.Text");
			this.label39.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label39.TextAlign")));
			this.label39.Visible = ((bool)(resources.GetObject("label39.Visible")));
			// 
			// lblist
			// 
			this.lblist.AccessibleDescription = resources.GetString("lblist.AccessibleDescription");
			this.lblist.AccessibleName = resources.GetString("lblist.AccessibleName");
			this.lblist.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblist.Anchor")));
			this.lblist.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lblist.BackgroundImage")));
			this.lblist.ColumnWidth = ((int)(resources.GetObject("lblist.ColumnWidth")));
			this.lblist.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblist.Dock")));
			this.lblist.Enabled = ((bool)(resources.GetObject("lblist.Enabled")));
			this.lblist.Font = ((System.Drawing.Font)(resources.GetObject("lblist.Font")));
			this.lblist.HorizontalExtent = ((int)(resources.GetObject("lblist.HorizontalExtent")));
			this.lblist.HorizontalScrollbar = ((bool)(resources.GetObject("lblist.HorizontalScrollbar")));
			this.lblist.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblist.ImeMode")));
			this.lblist.IntegralHeight = ((bool)(resources.GetObject("lblist.IntegralHeight")));
			this.lblist.ItemHeight = ((int)(resources.GetObject("lblist.ItemHeight")));
			this.lblist.Location = ((System.Drawing.Point)(resources.GetObject("lblist.Location")));
			this.lblist.Name = "lblist";
			this.lblist.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblist.RightToLeft")));
			this.lblist.ScrollAlwaysVisible = ((bool)(resources.GetObject("lblist.ScrollAlwaysVisible")));
			this.lblist.Size = ((System.Drawing.Size)(resources.GetObject("lblist.Size")));
			this.lblist.TabIndex = ((int)(resources.GetObject("lblist.TabIndex")));
			this.lblist.Visible = ((bool)(resources.GetObject("lblist.Visible")));
			this.lblist.SelectedIndexChanged += new System.EventHandler(this.GroupListChanged);
			// 
			// cbgroups
			// 
			this.cbgroups.AccessibleDescription = resources.GetString("cbgroups.AccessibleDescription");
			this.cbgroups.AccessibleName = resources.GetString("cbgroups.AccessibleName");
			this.cbgroups.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbgroups.Anchor")));
			this.cbgroups.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbgroups.BackgroundImage")));
			this.cbgroups.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbgroups.Dock")));
			this.cbgroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbgroups.Enabled = ((bool)(resources.GetObject("cbgroups.Enabled")));
			this.cbgroups.Font = ((System.Drawing.Font)(resources.GetObject("cbgroups.Font")));
			this.cbgroups.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbgroups.ImeMode")));
			this.cbgroups.IntegralHeight = ((bool)(resources.GetObject("cbgroups.IntegralHeight")));
			this.cbgroups.ItemHeight = ((int)(resources.GetObject("cbgroups.ItemHeight")));
			this.cbgroups.Location = ((System.Drawing.Point)(resources.GetObject("cbgroups.Location")));
			this.cbgroups.MaxDropDownItems = ((int)(resources.GetObject("cbgroups.MaxDropDownItems")));
			this.cbgroups.MaxLength = ((int)(resources.GetObject("cbgroups.MaxLength")));
			this.cbgroups.Name = "cbgroups";
			this.cbgroups.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbgroups.RightToLeft")));
			this.cbgroups.Size = ((System.Drawing.Size)(resources.GetObject("cbgroups.Size")));
			this.cbgroups.TabIndex = ((int)(resources.GetObject("cbgroups.TabIndex")));
			this.cbgroups.Text = resources.GetString("cbgroups.Text");
			this.cbgroups.Visible = ((bool)(resources.GetObject("cbgroups.Visible")));
			this.cbgroups.SelectedIndexChanged += new System.EventHandler(this.ListSelected);
			// 
			// lldelttab
			// 
			this.lldelttab.AccessibleDescription = resources.GetString("lldelttab.AccessibleDescription");
			this.lldelttab.AccessibleName = resources.GetString("lldelttab.AccessibleName");
			this.lldelttab.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lldelttab.Anchor")));
			this.lldelttab.AutoSize = ((bool)(resources.GetObject("lldelttab.AutoSize")));
			this.lldelttab.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lldelttab.Dock")));
			this.lldelttab.Enabled = ((bool)(resources.GetObject("lldelttab.Enabled")));
			this.lldelttab.Font = ((System.Drawing.Font)(resources.GetObject("lldelttab.Font")));
			this.lldelttab.Image = ((System.Drawing.Image)(resources.GetObject("lldelttab.Image")));
			this.lldelttab.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lldelttab.ImageAlign")));
			this.lldelttab.ImageIndex = ((int)(resources.GetObject("lldelttab.ImageIndex")));
			this.lldelttab.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lldelttab.ImeMode")));
			this.lldelttab.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("lldelttab.LinkArea")));
			this.lldelttab.Location = ((System.Drawing.Point)(resources.GetObject("lldelttab.Location")));
			this.lldelttab.Name = "lldelttab";
			this.lldelttab.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lldelttab.RightToLeft")));
			this.lldelttab.Size = ((System.Drawing.Size)(resources.GetObject("lldelttab.Size")));
			this.lldelttab.TabIndex = ((int)(resources.GetObject("lldelttab.TabIndex")));
			this.lldelttab.TabStop = true;
			this.lldelttab.Text = resources.GetString("lldelttab.Text");
			this.lldelttab.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lldelttab.TextAlign")));
			this.lldelttab.Visible = ((bool)(resources.GetObject("lldelttab.Visible")));
			this.lldelttab.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.TtabItemDelete);
			// 
			// button6
			// 
			this.button6.AccessibleDescription = resources.GetString("button6.AccessibleDescription");
			this.button6.AccessibleName = resources.GetString("button6.AccessibleName");
			this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("button6.Anchor")));
			this.button6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button6.BackgroundImage")));
			this.button6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("button6.Dock")));
			this.button6.Enabled = ((bool)(resources.GetObject("button6.Enabled")));
			this.button6.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("button6.FlatStyle")));
			this.button6.Font = ((System.Drawing.Font)(resources.GetObject("button6.Font")));
			this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
			this.button6.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("button6.ImageAlign")));
			this.button6.ImageIndex = ((int)(resources.GetObject("button6.ImageIndex")));
			this.button6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("button6.ImeMode")));
			this.button6.Location = ((System.Drawing.Point)(resources.GetObject("button6.Location")));
			this.button6.Name = "button6";
			this.button6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("button6.RightToLeft")));
			this.button6.Size = ((System.Drawing.Size)(resources.GetObject("button6.Size")));
			this.button6.TabIndex = ((int)(resources.GetObject("button6.TabIndex")));
			this.button6.Text = resources.GetString("button6.Text");
			this.button6.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("button6.TextAlign")));
			this.button6.Visible = ((bool)(resources.GetObject("button6.Visible")));
			this.button6.Click += new System.EventHandler(this.GetTTABAction);
			// 
			// button5
			// 
			this.button5.AccessibleDescription = resources.GetString("button5.AccessibleDescription");
			this.button5.AccessibleName = resources.GetString("button5.AccessibleName");
			this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("button5.Anchor")));
			this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
			this.button5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("button5.Dock")));
			this.button5.Enabled = ((bool)(resources.GetObject("button5.Enabled")));
			this.button5.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("button5.FlatStyle")));
			this.button5.Font = ((System.Drawing.Font)(resources.GetObject("button5.Font")));
			this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
			this.button5.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("button5.ImageAlign")));
			this.button5.ImageIndex = ((int)(resources.GetObject("button5.ImageIndex")));
			this.button5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("button5.ImeMode")));
			this.button5.Location = ((System.Drawing.Point)(resources.GetObject("button5.Location")));
			this.button5.Name = "button5";
			this.button5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("button5.RightToLeft")));
			this.button5.Size = ((System.Drawing.Size)(resources.GetObject("button5.Size")));
			this.button5.TabIndex = ((int)(resources.GetObject("button5.TabIndex")));
			this.button5.Text = resources.GetString("button5.Text");
			this.button5.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("button5.TextAlign")));
			this.button5.Visible = ((bool)(resources.GetObject("button5.Visible")));
			this.button5.Click += new System.EventHandler(this.GetTTABGuard);
			// 
			// lbaction
			// 
			this.lbaction.AccessibleDescription = resources.GetString("lbaction.AccessibleDescription");
			this.lbaction.AccessibleName = resources.GetString("lbaction.AccessibleName");
			this.lbaction.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbaction.Anchor")));
			this.lbaction.AutoSize = ((bool)(resources.GetObject("lbaction.AutoSize")));
			this.lbaction.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbaction.Dock")));
			this.lbaction.Enabled = ((bool)(resources.GetObject("lbaction.Enabled")));
			this.lbaction.Font = ((System.Drawing.Font)(resources.GetObject("lbaction.Font")));
			this.lbaction.Image = ((System.Drawing.Image)(resources.GetObject("lbaction.Image")));
			this.lbaction.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbaction.ImageAlign")));
			this.lbaction.ImageIndex = ((int)(resources.GetObject("lbaction.ImageIndex")));
			this.lbaction.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbaction.ImeMode")));
			this.lbaction.Location = ((System.Drawing.Point)(resources.GetObject("lbaction.Location")));
			this.lbaction.Name = "lbaction";
			this.lbaction.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbaction.RightToLeft")));
			this.lbaction.Size = ((System.Drawing.Size)(resources.GetObject("lbaction.Size")));
			this.lbaction.TabIndex = ((int)(resources.GetObject("lbaction.TabIndex")));
			this.lbaction.Text = resources.GetString("lbaction.Text");
			this.lbaction.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbaction.TextAlign")));
			this.lbaction.Visible = ((bool)(resources.GetObject("lbaction.Visible")));
			// 
			// lbguard
			// 
			this.lbguard.AccessibleDescription = resources.GetString("lbguard.AccessibleDescription");
			this.lbguard.AccessibleName = resources.GetString("lbguard.AccessibleName");
			this.lbguard.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbguard.Anchor")));
			this.lbguard.AutoSize = ((bool)(resources.GetObject("lbguard.AutoSize")));
			this.lbguard.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbguard.Dock")));
			this.lbguard.Enabled = ((bool)(resources.GetObject("lbguard.Enabled")));
			this.lbguard.Font = ((System.Drawing.Font)(resources.GetObject("lbguard.Font")));
			this.lbguard.Image = ((System.Drawing.Image)(resources.GetObject("lbguard.Image")));
			this.lbguard.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbguard.ImageAlign")));
			this.lbguard.ImageIndex = ((int)(resources.GetObject("lbguard.ImageIndex")));
			this.lbguard.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbguard.ImeMode")));
			this.lbguard.Location = ((System.Drawing.Point)(resources.GetObject("lbguard.Location")));
			this.lbguard.Name = "lbguard";
			this.lbguard.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbguard.RightToLeft")));
			this.lbguard.Size = ((System.Drawing.Size)(resources.GetObject("lbguard.Size")));
			this.lbguard.TabIndex = ((int)(resources.GetObject("lbguard.TabIndex")));
			this.lbguard.Text = resources.GetString("lbguard.Text");
			this.lbguard.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbguard.TextAlign")));
			this.lbguard.Visible = ((bool)(resources.GetObject("lbguard.Visible")));
			// 
			// tbver
			// 
			this.tbver.AccessibleDescription = resources.GetString("tbver.AccessibleDescription");
			this.tbver.AccessibleName = resources.GetString("tbver.AccessibleName");
			this.tbver.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbver.Anchor")));
			this.tbver.AutoSize = ((bool)(resources.GetObject("tbver.AutoSize")));
			this.tbver.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbver.BackgroundImage")));
			this.tbver.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbver.Dock")));
			this.tbver.Enabled = ((bool)(resources.GetObject("tbver.Enabled")));
			this.tbver.Font = ((System.Drawing.Font)(resources.GetObject("tbver.Font")));
			this.tbver.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbver.ImeMode")));
			this.tbver.Location = ((System.Drawing.Point)(resources.GetObject("tbver.Location")));
			this.tbver.MaxLength = ((int)(resources.GetObject("tbver.MaxLength")));
			this.tbver.Multiline = ((bool)(resources.GetObject("tbver.Multiline")));
			this.tbver.Name = "tbver";
			this.tbver.PasswordChar = ((char)(resources.GetObject("tbver.PasswordChar")));
			this.tbver.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbver.RightToLeft")));
			this.tbver.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbver.ScrollBars")));
			this.tbver.Size = ((System.Drawing.Size)(resources.GetObject("tbver.Size")));
			this.tbver.TabIndex = ((int)(resources.GetObject("tbver.TabIndex")));
			this.tbver.Text = resources.GetString("tbver.Text");
			this.tbver.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbver.TextAlign")));
			this.tbver.Visible = ((bool)(resources.GetObject("tbver.Visible")));
			this.tbver.WordWrap = ((bool)(resources.GetObject("tbver.WordWrap")));
			this.tbver.TextChanged += new System.EventHandler(this.AutoChangeInteraction);
			// 
			// label41
			// 
			this.label41.AccessibleDescription = resources.GetString("label41.AccessibleDescription");
			this.label41.AccessibleName = resources.GetString("label41.AccessibleName");
			this.label41.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label41.Anchor")));
			this.label41.AutoSize = ((bool)(resources.GetObject("label41.AutoSize")));
			this.label41.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label41.Dock")));
			this.label41.Enabled = ((bool)(resources.GetObject("label41.Enabled")));
			this.label41.Font = ((System.Drawing.Font)(resources.GetObject("label41.Font")));
			this.label41.Image = ((System.Drawing.Image)(resources.GetObject("label41.Image")));
			this.label41.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label41.ImageAlign")));
			this.label41.ImageIndex = ((int)(resources.GetObject("label41.ImageIndex")));
			this.label41.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label41.ImeMode")));
			this.label41.Location = ((System.Drawing.Point)(resources.GetObject("label41.Location")));
			this.label41.Name = "label41";
			this.label41.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label41.RightToLeft")));
			this.label41.Size = ((System.Drawing.Size)(resources.GetObject("label41.Size")));
			this.label41.TabIndex = ((int)(resources.GetObject("label41.TabIndex")));
			this.label41.Text = resources.GetString("label41.Text");
			this.label41.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label41.TextAlign")));
			this.label41.Visible = ((bool)(resources.GetObject("label41.Visible")));
			// 
			// tbpie
			// 
			this.tbpie.AccessibleDescription = resources.GetString("tbpie.AccessibleDescription");
			this.tbpie.AccessibleName = resources.GetString("tbpie.AccessibleName");
			this.tbpie.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbpie.Anchor")));
			this.tbpie.AutoSize = ((bool)(resources.GetObject("tbpie.AutoSize")));
			this.tbpie.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbpie.BackgroundImage")));
			this.tbpie.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbpie.Dock")));
			this.tbpie.Enabled = ((bool)(resources.GetObject("tbpie.Enabled")));
			this.tbpie.Font = ((System.Drawing.Font)(resources.GetObject("tbpie.Font")));
			this.tbpie.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbpie.ImeMode")));
			this.tbpie.Location = ((System.Drawing.Point)(resources.GetObject("tbpie.Location")));
			this.tbpie.MaxLength = ((int)(resources.GetObject("tbpie.MaxLength")));
			this.tbpie.Multiline = ((bool)(resources.GetObject("tbpie.Multiline")));
			this.tbpie.Name = "tbpie";
			this.tbpie.PasswordChar = ((char)(resources.GetObject("tbpie.PasswordChar")));
			this.tbpie.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbpie.RightToLeft")));
			this.tbpie.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbpie.ScrollBars")));
			this.tbpie.Size = ((System.Drawing.Size)(resources.GetObject("tbpie.Size")));
			this.tbpie.TabIndex = ((int)(resources.GetObject("tbpie.TabIndex")));
			this.tbpie.Text = resources.GetString("tbpie.Text");
			this.tbpie.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbpie.TextAlign")));
			this.tbpie.Visible = ((bool)(resources.GetObject("tbpie.Visible")));
			this.tbpie.WordWrap = ((bool)(resources.GetObject("tbpie.WordWrap")));
			this.tbpie.TextChanged += new System.EventHandler(this.AutoChangeInteraction);
			// 
			// label40
			// 
			this.label40.AccessibleDescription = resources.GetString("label40.AccessibleDescription");
			this.label40.AccessibleName = resources.GetString("label40.AccessibleName");
			this.label40.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label40.Anchor")));
			this.label40.AutoSize = ((bool)(resources.GetObject("label40.AutoSize")));
			this.label40.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label40.Dock")));
			this.label40.Enabled = ((bool)(resources.GetObject("label40.Enabled")));
			this.label40.Font = ((System.Drawing.Font)(resources.GetObject("label40.Font")));
			this.label40.Image = ((System.Drawing.Image)(resources.GetObject("label40.Image")));
			this.label40.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label40.ImageAlign")));
			this.label40.ImageIndex = ((int)(resources.GetObject("label40.ImageIndex")));
			this.label40.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label40.ImeMode")));
			this.label40.Location = ((System.Drawing.Point)(resources.GetObject("label40.Location")));
			this.label40.Name = "label40";
			this.label40.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label40.RightToLeft")));
			this.label40.Size = ((System.Drawing.Size)(resources.GetObject("label40.Size")));
			this.label40.TabIndex = ((int)(resources.GetObject("label40.TabIndex")));
			this.label40.Text = resources.GetString("label40.Text");
			this.label40.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label40.TextAlign")));
			this.label40.Visible = ((bool)(resources.GetObject("label40.Visible")));
			// 
			// tbres8
			// 
			this.tbres8.AccessibleDescription = resources.GetString("tbres8.AccessibleDescription");
			this.tbres8.AccessibleName = resources.GetString("tbres8.AccessibleName");
			this.tbres8.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbres8.Anchor")));
			this.tbres8.AutoSize = ((bool)(resources.GetObject("tbres8.AutoSize")));
			this.tbres8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbres8.BackgroundImage")));
			this.tbres8.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbres8.Dock")));
			this.tbres8.Enabled = ((bool)(resources.GetObject("tbres8.Enabled")));
			this.tbres8.Font = ((System.Drawing.Font)(resources.GetObject("tbres8.Font")));
			this.tbres8.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbres8.ImeMode")));
			this.tbres8.Location = ((System.Drawing.Point)(resources.GetObject("tbres8.Location")));
			this.tbres8.MaxLength = ((int)(resources.GetObject("tbres8.MaxLength")));
			this.tbres8.Multiline = ((bool)(resources.GetObject("tbres8.Multiline")));
			this.tbres8.Name = "tbres8";
			this.tbres8.PasswordChar = ((char)(resources.GetObject("tbres8.PasswordChar")));
			this.tbres8.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbres8.RightToLeft")));
			this.tbres8.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbres8.ScrollBars")));
			this.tbres8.Size = ((System.Drawing.Size)(resources.GetObject("tbres8.Size")));
			this.tbres8.TabIndex = ((int)(resources.GetObject("tbres8.TabIndex")));
			this.tbres8.Text = resources.GetString("tbres8.Text");
			this.tbres8.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbres8.TextAlign")));
			this.tbres8.Visible = ((bool)(resources.GetObject("tbres8.Visible")));
			this.tbres8.WordWrap = ((bool)(resources.GetObject("tbres8.WordWrap")));
			this.tbres8.TextChanged += new System.EventHandler(this.AutoChangeInteraction);
			// 
			// label33
			// 
			this.label33.AccessibleDescription = resources.GetString("label33.AccessibleDescription");
			this.label33.AccessibleName = resources.GetString("label33.AccessibleName");
			this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label33.Anchor")));
			this.label33.AutoSize = ((bool)(resources.GetObject("label33.AutoSize")));
			this.label33.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label33.Dock")));
			this.label33.Enabled = ((bool)(resources.GetObject("label33.Enabled")));
			this.label33.Font = ((System.Drawing.Font)(resources.GetObject("label33.Font")));
			this.label33.Image = ((System.Drawing.Image)(resources.GetObject("label33.Image")));
			this.label33.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label33.ImageAlign")));
			this.label33.ImageIndex = ((int)(resources.GetObject("label33.ImageIndex")));
			this.label33.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label33.ImeMode")));
			this.label33.Location = ((System.Drawing.Point)(resources.GetObject("label33.Location")));
			this.label33.Name = "label33";
			this.label33.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label33.RightToLeft")));
			this.label33.Size = ((System.Drawing.Size)(resources.GetObject("label33.Size")));
			this.label33.TabIndex = ((int)(resources.GetObject("label33.TabIndex")));
			this.label33.Text = resources.GetString("label33.Text");
			this.label33.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label33.TextAlign")));
			this.label33.Visible = ((bool)(resources.GetObject("label33.Visible")));
			// 
			// tbres7
			// 
			this.tbres7.AccessibleDescription = resources.GetString("tbres7.AccessibleDescription");
			this.tbres7.AccessibleName = resources.GetString("tbres7.AccessibleName");
			this.tbres7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbres7.Anchor")));
			this.tbres7.AutoSize = ((bool)(resources.GetObject("tbres7.AutoSize")));
			this.tbres7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbres7.BackgroundImage")));
			this.tbres7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbres7.Dock")));
			this.tbres7.Enabled = ((bool)(resources.GetObject("tbres7.Enabled")));
			this.tbres7.Font = ((System.Drawing.Font)(resources.GetObject("tbres7.Font")));
			this.tbres7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbres7.ImeMode")));
			this.tbres7.Location = ((System.Drawing.Point)(resources.GetObject("tbres7.Location")));
			this.tbres7.MaxLength = ((int)(resources.GetObject("tbres7.MaxLength")));
			this.tbres7.Multiline = ((bool)(resources.GetObject("tbres7.Multiline")));
			this.tbres7.Name = "tbres7";
			this.tbres7.PasswordChar = ((char)(resources.GetObject("tbres7.PasswordChar")));
			this.tbres7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbres7.RightToLeft")));
			this.tbres7.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbres7.ScrollBars")));
			this.tbres7.Size = ((System.Drawing.Size)(resources.GetObject("tbres7.Size")));
			this.tbres7.TabIndex = ((int)(resources.GetObject("tbres7.TabIndex")));
			this.tbres7.Text = resources.GetString("tbres7.Text");
			this.tbres7.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbres7.TextAlign")));
			this.tbres7.Visible = ((bool)(resources.GetObject("tbres7.Visible")));
			this.tbres7.WordWrap = ((bool)(resources.GetObject("tbres7.WordWrap")));
			this.tbres7.TextChanged += new System.EventHandler(this.AutoChangeInteraction);
			// 
			// label34
			// 
			this.label34.AccessibleDescription = resources.GetString("label34.AccessibleDescription");
			this.label34.AccessibleName = resources.GetString("label34.AccessibleName");
			this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label34.Anchor")));
			this.label34.AutoSize = ((bool)(resources.GetObject("label34.AutoSize")));
			this.label34.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label34.Dock")));
			this.label34.Enabled = ((bool)(resources.GetObject("label34.Enabled")));
			this.label34.Font = ((System.Drawing.Font)(resources.GetObject("label34.Font")));
			this.label34.Image = ((System.Drawing.Image)(resources.GetObject("label34.Image")));
			this.label34.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label34.ImageAlign")));
			this.label34.ImageIndex = ((int)(resources.GetObject("label34.ImageIndex")));
			this.label34.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label34.ImeMode")));
			this.label34.Location = ((System.Drawing.Point)(resources.GetObject("label34.Location")));
			this.label34.Name = "label34";
			this.label34.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label34.RightToLeft")));
			this.label34.Size = ((System.Drawing.Size)(resources.GetObject("label34.Size")));
			this.label34.TabIndex = ((int)(resources.GetObject("label34.TabIndex")));
			this.label34.Text = resources.GetString("label34.Text");
			this.label34.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label34.TextAlign")));
			this.label34.Visible = ((bool)(resources.GetObject("label34.Visible")));
			// 
			// tbres4
			// 
			this.tbres4.AccessibleDescription = resources.GetString("tbres4.AccessibleDescription");
			this.tbres4.AccessibleName = resources.GetString("tbres4.AccessibleName");
			this.tbres4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbres4.Anchor")));
			this.tbres4.AutoSize = ((bool)(resources.GetObject("tbres4.AutoSize")));
			this.tbres4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbres4.BackgroundImage")));
			this.tbres4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbres4.Dock")));
			this.tbres4.Enabled = ((bool)(resources.GetObject("tbres4.Enabled")));
			this.tbres4.Font = ((System.Drawing.Font)(resources.GetObject("tbres4.Font")));
			this.tbres4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbres4.ImeMode")));
			this.tbres4.Location = ((System.Drawing.Point)(resources.GetObject("tbres4.Location")));
			this.tbres4.MaxLength = ((int)(resources.GetObject("tbres4.MaxLength")));
			this.tbres4.Multiline = ((bool)(resources.GetObject("tbres4.Multiline")));
			this.tbres4.Name = "tbres4";
			this.tbres4.PasswordChar = ((char)(resources.GetObject("tbres4.PasswordChar")));
			this.tbres4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbres4.RightToLeft")));
			this.tbres4.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbres4.ScrollBars")));
			this.tbres4.Size = ((System.Drawing.Size)(resources.GetObject("tbres4.Size")));
			this.tbres4.TabIndex = ((int)(resources.GetObject("tbres4.TabIndex")));
			this.tbres4.Text = resources.GetString("tbres4.Text");
			this.tbres4.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbres4.TextAlign")));
			this.tbres4.Visible = ((bool)(resources.GetObject("tbres4.Visible")));
			this.tbres4.WordWrap = ((bool)(resources.GetObject("tbres4.WordWrap")));
			this.tbres4.TextChanged += new System.EventHandler(this.AutoChangeInteraction);
			// 
			// label35
			// 
			this.label35.AccessibleDescription = resources.GetString("label35.AccessibleDescription");
			this.label35.AccessibleName = resources.GetString("label35.AccessibleName");
			this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label35.Anchor")));
			this.label35.AutoSize = ((bool)(resources.GetObject("label35.AutoSize")));
			this.label35.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label35.Dock")));
			this.label35.Enabled = ((bool)(resources.GetObject("label35.Enabled")));
			this.label35.Font = ((System.Drawing.Font)(resources.GetObject("label35.Font")));
			this.label35.Image = ((System.Drawing.Image)(resources.GetObject("label35.Image")));
			this.label35.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label35.ImageAlign")));
			this.label35.ImageIndex = ((int)(resources.GetObject("label35.ImageIndex")));
			this.label35.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label35.ImeMode")));
			this.label35.Location = ((System.Drawing.Point)(resources.GetObject("label35.Location")));
			this.label35.Name = "label35";
			this.label35.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label35.RightToLeft")));
			this.label35.Size = ((System.Drawing.Size)(resources.GetObject("label35.Size")));
			this.label35.TabIndex = ((int)(resources.GetObject("label35.TabIndex")));
			this.label35.Text = resources.GetString("label35.Text");
			this.label35.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label35.TextAlign")));
			this.label35.Visible = ((bool)(resources.GetObject("label35.Visible")));
			// 
			// tbres3
			// 
			this.tbres3.AccessibleDescription = resources.GetString("tbres3.AccessibleDescription");
			this.tbres3.AccessibleName = resources.GetString("tbres3.AccessibleName");
			this.tbres3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbres3.Anchor")));
			this.tbres3.AutoSize = ((bool)(resources.GetObject("tbres3.AutoSize")));
			this.tbres3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbres3.BackgroundImage")));
			this.tbres3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbres3.Dock")));
			this.tbres3.Enabled = ((bool)(resources.GetObject("tbres3.Enabled")));
			this.tbres3.Font = ((System.Drawing.Font)(resources.GetObject("tbres3.Font")));
			this.tbres3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbres3.ImeMode")));
			this.tbres3.Location = ((System.Drawing.Point)(resources.GetObject("tbres3.Location")));
			this.tbres3.MaxLength = ((int)(resources.GetObject("tbres3.MaxLength")));
			this.tbres3.Multiline = ((bool)(resources.GetObject("tbres3.Multiline")));
			this.tbres3.Name = "tbres3";
			this.tbres3.PasswordChar = ((char)(resources.GetObject("tbres3.PasswordChar")));
			this.tbres3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbres3.RightToLeft")));
			this.tbres3.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbres3.ScrollBars")));
			this.tbres3.Size = ((System.Drawing.Size)(resources.GetObject("tbres3.Size")));
			this.tbres3.TabIndex = ((int)(resources.GetObject("tbres3.TabIndex")));
			this.tbres3.Text = resources.GetString("tbres3.Text");
			this.tbres3.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbres3.TextAlign")));
			this.tbres3.Visible = ((bool)(resources.GetObject("tbres3.Visible")));
			this.tbres3.WordWrap = ((bool)(resources.GetObject("tbres3.WordWrap")));
			this.tbres3.TextChanged += new System.EventHandler(this.AutoChangeInteraction);
			// 
			// label36
			// 
			this.label36.AccessibleDescription = resources.GetString("label36.AccessibleDescription");
			this.label36.AccessibleName = resources.GetString("label36.AccessibleName");
			this.label36.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label36.Anchor")));
			this.label36.AutoSize = ((bool)(resources.GetObject("label36.AutoSize")));
			this.label36.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label36.Dock")));
			this.label36.Enabled = ((bool)(resources.GetObject("label36.Enabled")));
			this.label36.Font = ((System.Drawing.Font)(resources.GetObject("label36.Font")));
			this.label36.Image = ((System.Drawing.Image)(resources.GetObject("label36.Image")));
			this.label36.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label36.ImageAlign")));
			this.label36.ImageIndex = ((int)(resources.GetObject("label36.ImageIndex")));
			this.label36.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label36.ImeMode")));
			this.label36.Location = ((System.Drawing.Point)(resources.GetObject("label36.Location")));
			this.label36.Name = "label36";
			this.label36.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label36.RightToLeft")));
			this.label36.Size = ((System.Drawing.Size)(resources.GetObject("label36.Size")));
			this.label36.TabIndex = ((int)(resources.GetObject("label36.TabIndex")));
			this.label36.Text = resources.GetString("label36.Text");
			this.label36.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label36.TextAlign")));
			this.label36.Visible = ((bool)(resources.GetObject("label36.Visible")));
			// 
			// tbres6
			// 
			this.tbres6.AccessibleDescription = resources.GetString("tbres6.AccessibleDescription");
			this.tbres6.AccessibleName = resources.GetString("tbres6.AccessibleName");
			this.tbres6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbres6.Anchor")));
			this.tbres6.AutoSize = ((bool)(resources.GetObject("tbres6.AutoSize")));
			this.tbres6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbres6.BackgroundImage")));
			this.tbres6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbres6.Dock")));
			this.tbres6.Enabled = ((bool)(resources.GetObject("tbres6.Enabled")));
			this.tbres6.Font = ((System.Drawing.Font)(resources.GetObject("tbres6.Font")));
			this.tbres6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbres6.ImeMode")));
			this.tbres6.Location = ((System.Drawing.Point)(resources.GetObject("tbres6.Location")));
			this.tbres6.MaxLength = ((int)(resources.GetObject("tbres6.MaxLength")));
			this.tbres6.Multiline = ((bool)(resources.GetObject("tbres6.Multiline")));
			this.tbres6.Name = "tbres6";
			this.tbres6.PasswordChar = ((char)(resources.GetObject("tbres6.PasswordChar")));
			this.tbres6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbres6.RightToLeft")));
			this.tbres6.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbres6.ScrollBars")));
			this.tbres6.Size = ((System.Drawing.Size)(resources.GetObject("tbres6.Size")));
			this.tbres6.TabIndex = ((int)(resources.GetObject("tbres6.TabIndex")));
			this.tbres6.Text = resources.GetString("tbres6.Text");
			this.tbres6.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbres6.TextAlign")));
			this.tbres6.Visible = ((bool)(resources.GetObject("tbres6.Visible")));
			this.tbres6.WordWrap = ((bool)(resources.GetObject("tbres6.WordWrap")));
			this.tbres6.TextChanged += new System.EventHandler(this.AutoChangeInteraction);
			// 
			// label29
			// 
			this.label29.AccessibleDescription = resources.GetString("label29.AccessibleDescription");
			this.label29.AccessibleName = resources.GetString("label29.AccessibleName");
			this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label29.Anchor")));
			this.label29.AutoSize = ((bool)(resources.GetObject("label29.AutoSize")));
			this.label29.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label29.Dock")));
			this.label29.Enabled = ((bool)(resources.GetObject("label29.Enabled")));
			this.label29.Font = ((System.Drawing.Font)(resources.GetObject("label29.Font")));
			this.label29.Image = ((System.Drawing.Image)(resources.GetObject("label29.Image")));
			this.label29.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label29.ImageAlign")));
			this.label29.ImageIndex = ((int)(resources.GetObject("label29.ImageIndex")));
			this.label29.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label29.ImeMode")));
			this.label29.Location = ((System.Drawing.Point)(resources.GetObject("label29.Location")));
			this.label29.Name = "label29";
			this.label29.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label29.RightToLeft")));
			this.label29.Size = ((System.Drawing.Size)(resources.GetObject("label29.Size")));
			this.label29.TabIndex = ((int)(resources.GetObject("label29.TabIndex")));
			this.label29.Text = resources.GetString("label29.Text");
			this.label29.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label29.TextAlign")));
			this.label29.Visible = ((bool)(resources.GetObject("label29.Visible")));
			// 
			// tbres5
			// 
			this.tbres5.AccessibleDescription = resources.GetString("tbres5.AccessibleDescription");
			this.tbres5.AccessibleName = resources.GetString("tbres5.AccessibleName");
			this.tbres5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbres5.Anchor")));
			this.tbres5.AutoSize = ((bool)(resources.GetObject("tbres5.AutoSize")));
			this.tbres5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbres5.BackgroundImage")));
			this.tbres5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbres5.Dock")));
			this.tbres5.Enabled = ((bool)(resources.GetObject("tbres5.Enabled")));
			this.tbres5.Font = ((System.Drawing.Font)(resources.GetObject("tbres5.Font")));
			this.tbres5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbres5.ImeMode")));
			this.tbres5.Location = ((System.Drawing.Point)(resources.GetObject("tbres5.Location")));
			this.tbres5.MaxLength = ((int)(resources.GetObject("tbres5.MaxLength")));
			this.tbres5.Multiline = ((bool)(resources.GetObject("tbres5.Multiline")));
			this.tbres5.Name = "tbres5";
			this.tbres5.PasswordChar = ((char)(resources.GetObject("tbres5.PasswordChar")));
			this.tbres5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbres5.RightToLeft")));
			this.tbres5.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbres5.ScrollBars")));
			this.tbres5.Size = ((System.Drawing.Size)(resources.GetObject("tbres5.Size")));
			this.tbres5.TabIndex = ((int)(resources.GetObject("tbres5.TabIndex")));
			this.tbres5.Text = resources.GetString("tbres5.Text");
			this.tbres5.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbres5.TextAlign")));
			this.tbres5.Visible = ((bool)(resources.GetObject("tbres5.Visible")));
			this.tbres5.WordWrap = ((bool)(resources.GetObject("tbres5.WordWrap")));
			this.tbres5.TextChanged += new System.EventHandler(this.AutoChangeInteraction);
			// 
			// label30
			// 
			this.label30.AccessibleDescription = resources.GetString("label30.AccessibleDescription");
			this.label30.AccessibleName = resources.GetString("label30.AccessibleName");
			this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label30.Anchor")));
			this.label30.AutoSize = ((bool)(resources.GetObject("label30.AutoSize")));
			this.label30.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label30.Dock")));
			this.label30.Enabled = ((bool)(resources.GetObject("label30.Enabled")));
			this.label30.Font = ((System.Drawing.Font)(resources.GetObject("label30.Font")));
			this.label30.Image = ((System.Drawing.Image)(resources.GetObject("label30.Image")));
			this.label30.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label30.ImageAlign")));
			this.label30.ImageIndex = ((int)(resources.GetObject("label30.ImageIndex")));
			this.label30.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label30.ImeMode")));
			this.label30.Location = ((System.Drawing.Point)(resources.GetObject("label30.Location")));
			this.label30.Name = "label30";
			this.label30.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label30.RightToLeft")));
			this.label30.Size = ((System.Drawing.Size)(resources.GetObject("label30.Size")));
			this.label30.TabIndex = ((int)(resources.GetObject("label30.TabIndex")));
			this.label30.Text = resources.GetString("label30.Text");
			this.label30.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label30.TextAlign")));
			this.label30.Visible = ((bool)(resources.GetObject("label30.Visible")));
			// 
			// tbres2
			// 
			this.tbres2.AccessibleDescription = resources.GetString("tbres2.AccessibleDescription");
			this.tbres2.AccessibleName = resources.GetString("tbres2.AccessibleName");
			this.tbres2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbres2.Anchor")));
			this.tbres2.AutoSize = ((bool)(resources.GetObject("tbres2.AutoSize")));
			this.tbres2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbres2.BackgroundImage")));
			this.tbres2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbres2.Dock")));
			this.tbres2.Enabled = ((bool)(resources.GetObject("tbres2.Enabled")));
			this.tbres2.Font = ((System.Drawing.Font)(resources.GetObject("tbres2.Font")));
			this.tbres2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbres2.ImeMode")));
			this.tbres2.Location = ((System.Drawing.Point)(resources.GetObject("tbres2.Location")));
			this.tbres2.MaxLength = ((int)(resources.GetObject("tbres2.MaxLength")));
			this.tbres2.Multiline = ((bool)(resources.GetObject("tbres2.Multiline")));
			this.tbres2.Name = "tbres2";
			this.tbres2.PasswordChar = ((char)(resources.GetObject("tbres2.PasswordChar")));
			this.tbres2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbres2.RightToLeft")));
			this.tbres2.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbres2.ScrollBars")));
			this.tbres2.Size = ((System.Drawing.Size)(resources.GetObject("tbres2.Size")));
			this.tbres2.TabIndex = ((int)(resources.GetObject("tbres2.TabIndex")));
			this.tbres2.Text = resources.GetString("tbres2.Text");
			this.tbres2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbres2.TextAlign")));
			this.tbres2.Visible = ((bool)(resources.GetObject("tbres2.Visible")));
			this.tbres2.WordWrap = ((bool)(resources.GetObject("tbres2.WordWrap")));
			this.tbres2.TextChanged += new System.EventHandler(this.AutoChangeInteraction);
			// 
			// label31
			// 
			this.label31.AccessibleDescription = resources.GetString("label31.AccessibleDescription");
			this.label31.AccessibleName = resources.GetString("label31.AccessibleName");
			this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label31.Anchor")));
			this.label31.AutoSize = ((bool)(resources.GetObject("label31.AutoSize")));
			this.label31.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label31.Dock")));
			this.label31.Enabled = ((bool)(resources.GetObject("label31.Enabled")));
			this.label31.Font = ((System.Drawing.Font)(resources.GetObject("label31.Font")));
			this.label31.Image = ((System.Drawing.Image)(resources.GetObject("label31.Image")));
			this.label31.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label31.ImageAlign")));
			this.label31.ImageIndex = ((int)(resources.GetObject("label31.ImageIndex")));
			this.label31.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label31.ImeMode")));
			this.label31.Location = ((System.Drawing.Point)(resources.GetObject("label31.Location")));
			this.label31.Name = "label31";
			this.label31.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label31.RightToLeft")));
			this.label31.Size = ((System.Drawing.Size)(resources.GetObject("label31.Size")));
			this.label31.TabIndex = ((int)(resources.GetObject("label31.TabIndex")));
			this.label31.Text = resources.GetString("label31.Text");
			this.label31.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label31.TextAlign")));
			this.label31.Visible = ((bool)(resources.GetObject("label31.Visible")));
			// 
			// tbres1
			// 
			this.tbres1.AccessibleDescription = resources.GetString("tbres1.AccessibleDescription");
			this.tbres1.AccessibleName = resources.GetString("tbres1.AccessibleName");
			this.tbres1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbres1.Anchor")));
			this.tbres1.AutoSize = ((bool)(resources.GetObject("tbres1.AutoSize")));
			this.tbres1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbres1.BackgroundImage")));
			this.tbres1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbres1.Dock")));
			this.tbres1.Enabled = ((bool)(resources.GetObject("tbres1.Enabled")));
			this.tbres1.Font = ((System.Drawing.Font)(resources.GetObject("tbres1.Font")));
			this.tbres1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbres1.ImeMode")));
			this.tbres1.Location = ((System.Drawing.Point)(resources.GetObject("tbres1.Location")));
			this.tbres1.MaxLength = ((int)(resources.GetObject("tbres1.MaxLength")));
			this.tbres1.Multiline = ((bool)(resources.GetObject("tbres1.Multiline")));
			this.tbres1.Name = "tbres1";
			this.tbres1.PasswordChar = ((char)(resources.GetObject("tbres1.PasswordChar")));
			this.tbres1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbres1.RightToLeft")));
			this.tbres1.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbres1.ScrollBars")));
			this.tbres1.Size = ((System.Drawing.Size)(resources.GetObject("tbres1.Size")));
			this.tbres1.TabIndex = ((int)(resources.GetObject("tbres1.TabIndex")));
			this.tbres1.Text = resources.GetString("tbres1.Text");
			this.tbres1.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbres1.TextAlign")));
			this.tbres1.Visible = ((bool)(resources.GetObject("tbres1.Visible")));
			this.tbres1.WordWrap = ((bool)(resources.GetObject("tbres1.WordWrap")));
			this.tbres1.TextChanged += new System.EventHandler(this.AutoChangeInteraction);
			// 
			// label32
			// 
			this.label32.AccessibleDescription = resources.GetString("label32.AccessibleDescription");
			this.label32.AccessibleName = resources.GetString("label32.AccessibleName");
			this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label32.Anchor")));
			this.label32.AutoSize = ((bool)(resources.GetObject("label32.AutoSize")));
			this.label32.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label32.Dock")));
			this.label32.Enabled = ((bool)(resources.GetObject("label32.Enabled")));
			this.label32.Font = ((System.Drawing.Font)(resources.GetObject("label32.Font")));
			this.label32.Image = ((System.Drawing.Image)(resources.GetObject("label32.Image")));
			this.label32.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label32.ImageAlign")));
			this.label32.ImageIndex = ((int)(resources.GetObject("label32.ImageIndex")));
			this.label32.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label32.ImeMode")));
			this.label32.Location = ((System.Drawing.Point)(resources.GetObject("label32.Location")));
			this.label32.Name = "label32";
			this.label32.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label32.RightToLeft")));
			this.label32.Size = ((System.Drawing.Size)(resources.GetObject("label32.Size")));
			this.label32.TabIndex = ((int)(resources.GetObject("label32.TabIndex")));
			this.label32.Text = resources.GetString("label32.Text");
			this.label32.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label32.TextAlign")));
			this.label32.Visible = ((bool)(resources.GetObject("label32.Visible")));
			// 
			// tbinst2
			// 
			this.tbinst2.AccessibleDescription = resources.GetString("tbinst2.AccessibleDescription");
			this.tbinst2.AccessibleName = resources.GetString("tbinst2.AccessibleName");
			this.tbinst2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbinst2.Anchor")));
			this.tbinst2.AutoSize = ((bool)(resources.GetObject("tbinst2.AutoSize")));
			this.tbinst2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbinst2.BackgroundImage")));
			this.tbinst2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbinst2.Dock")));
			this.tbinst2.Enabled = ((bool)(resources.GetObject("tbinst2.Enabled")));
			this.tbinst2.Font = ((System.Drawing.Font)(resources.GetObject("tbinst2.Font")));
			this.tbinst2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbinst2.ImeMode")));
			this.tbinst2.Location = ((System.Drawing.Point)(resources.GetObject("tbinst2.Location")));
			this.tbinst2.MaxLength = ((int)(resources.GetObject("tbinst2.MaxLength")));
			this.tbinst2.Multiline = ((bool)(resources.GetObject("tbinst2.Multiline")));
			this.tbinst2.Name = "tbinst2";
			this.tbinst2.PasswordChar = ((char)(resources.GetObject("tbinst2.PasswordChar")));
			this.tbinst2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbinst2.RightToLeft")));
			this.tbinst2.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbinst2.ScrollBars")));
			this.tbinst2.Size = ((System.Drawing.Size)(resources.GetObject("tbinst2.Size")));
			this.tbinst2.TabIndex = ((int)(resources.GetObject("tbinst2.TabIndex")));
			this.tbinst2.Text = resources.GetString("tbinst2.Text");
			this.tbinst2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbinst2.TextAlign")));
			this.tbinst2.Visible = ((bool)(resources.GetObject("tbinst2.Visible")));
			this.tbinst2.WordWrap = ((bool)(resources.GetObject("tbinst2.WordWrap")));
			this.tbinst2.TextChanged += new System.EventHandler(this.AutoChangeInteraction);
			// 
			// label20
			// 
			this.label20.AccessibleDescription = resources.GetString("label20.AccessibleDescription");
			this.label20.AccessibleName = resources.GetString("label20.AccessibleName");
			this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label20.Anchor")));
			this.label20.AutoSize = ((bool)(resources.GetObject("label20.AutoSize")));
			this.label20.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label20.Dock")));
			this.label20.Enabled = ((bool)(resources.GetObject("label20.Enabled")));
			this.label20.Font = ((System.Drawing.Font)(resources.GetObject("label20.Font")));
			this.label20.Image = ((System.Drawing.Image)(resources.GetObject("label20.Image")));
			this.label20.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label20.ImageAlign")));
			this.label20.ImageIndex = ((int)(resources.GetObject("label20.ImageIndex")));
			this.label20.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label20.ImeMode")));
			this.label20.Location = ((System.Drawing.Point)(resources.GetObject("label20.Location")));
			this.label20.Name = "label20";
			this.label20.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label20.RightToLeft")));
			this.label20.Size = ((System.Drawing.Size)(resources.GetObject("label20.Size")));
			this.label20.TabIndex = ((int)(resources.GetObject("label20.TabIndex")));
			this.label20.Text = resources.GetString("label20.Text");
			this.label20.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label20.TextAlign")));
			this.label20.Visible = ((bool)(resources.GetObject("label20.Visible")));
			// 
			// tbinst1
			// 
			this.tbinst1.AccessibleDescription = resources.GetString("tbinst1.AccessibleDescription");
			this.tbinst1.AccessibleName = resources.GetString("tbinst1.AccessibleName");
			this.tbinst1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbinst1.Anchor")));
			this.tbinst1.AutoSize = ((bool)(resources.GetObject("tbinst1.AutoSize")));
			this.tbinst1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbinst1.BackgroundImage")));
			this.tbinst1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbinst1.Dock")));
			this.tbinst1.Enabled = ((bool)(resources.GetObject("tbinst1.Enabled")));
			this.tbinst1.Font = ((System.Drawing.Font)(resources.GetObject("tbinst1.Font")));
			this.tbinst1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbinst1.ImeMode")));
			this.tbinst1.Location = ((System.Drawing.Point)(resources.GetObject("tbinst1.Location")));
			this.tbinst1.MaxLength = ((int)(resources.GetObject("tbinst1.MaxLength")));
			this.tbinst1.Multiline = ((bool)(resources.GetObject("tbinst1.Multiline")));
			this.tbinst1.Name = "tbinst1";
			this.tbinst1.PasswordChar = ((char)(resources.GetObject("tbinst1.PasswordChar")));
			this.tbinst1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbinst1.RightToLeft")));
			this.tbinst1.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbinst1.ScrollBars")));
			this.tbinst1.Size = ((System.Drawing.Size)(resources.GetObject("tbinst1.Size")));
			this.tbinst1.TabIndex = ((int)(resources.GetObject("tbinst1.TabIndex")));
			this.tbinst1.Text = resources.GetString("tbinst1.Text");
			this.tbinst1.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbinst1.TextAlign")));
			this.tbinst1.Visible = ((bool)(resources.GetObject("tbinst1.Visible")));
			this.tbinst1.WordWrap = ((bool)(resources.GetObject("tbinst1.WordWrap")));
			this.tbinst1.TextChanged += new System.EventHandler(this.FlagTextChanged);
			// 
			// label24
			// 
			this.label24.AccessibleDescription = resources.GetString("label24.AccessibleDescription");
			this.label24.AccessibleName = resources.GetString("label24.AccessibleName");
			this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label24.Anchor")));
			this.label24.AutoSize = ((bool)(resources.GetObject("label24.AutoSize")));
			this.label24.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label24.Dock")));
			this.label24.Enabled = ((bool)(resources.GetObject("label24.Enabled")));
			this.label24.Font = ((System.Drawing.Font)(resources.GetObject("label24.Font")));
			this.label24.Image = ((System.Drawing.Image)(resources.GetObject("label24.Image")));
			this.label24.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label24.ImageAlign")));
			this.label24.ImageIndex = ((int)(resources.GetObject("label24.ImageIndex")));
			this.label24.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label24.ImeMode")));
			this.label24.Location = ((System.Drawing.Point)(resources.GetObject("label24.Location")));
			this.label24.Name = "label24";
			this.label24.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label24.RightToLeft")));
			this.label24.Size = ((System.Drawing.Size)(resources.GetObject("label24.Size")));
			this.label24.TabIndex = ((int)(resources.GetObject("label24.TabIndex")));
			this.label24.Text = resources.GetString("label24.Text");
			this.label24.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label24.TextAlign")));
			this.label24.Visible = ((bool)(resources.GetObject("label24.Visible")));
			// 
			// tbttabaction
			// 
			this.tbttabaction.AccessibleDescription = resources.GetString("tbttabaction.AccessibleDescription");
			this.tbttabaction.AccessibleName = resources.GetString("tbttabaction.AccessibleName");
			this.tbttabaction.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbttabaction.Anchor")));
			this.tbttabaction.AutoSize = ((bool)(resources.GetObject("tbttabaction.AutoSize")));
			this.tbttabaction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbttabaction.BackgroundImage")));
			this.tbttabaction.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbttabaction.Dock")));
			this.tbttabaction.Enabled = ((bool)(resources.GetObject("tbttabaction.Enabled")));
			this.tbttabaction.Font = ((System.Drawing.Font)(resources.GetObject("tbttabaction.Font")));
			this.tbttabaction.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbttabaction.ImeMode")));
			this.tbttabaction.Location = ((System.Drawing.Point)(resources.GetObject("tbttabaction.Location")));
			this.tbttabaction.MaxLength = ((int)(resources.GetObject("tbttabaction.MaxLength")));
			this.tbttabaction.Multiline = ((bool)(resources.GetObject("tbttabaction.Multiline")));
			this.tbttabaction.Name = "tbttabaction";
			this.tbttabaction.PasswordChar = ((char)(resources.GetObject("tbttabaction.PasswordChar")));
			this.tbttabaction.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbttabaction.RightToLeft")));
			this.tbttabaction.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbttabaction.ScrollBars")));
			this.tbttabaction.Size = ((System.Drawing.Size)(resources.GetObject("tbttabaction.Size")));
			this.tbttabaction.TabIndex = ((int)(resources.GetObject("tbttabaction.TabIndex")));
			this.tbttabaction.Text = resources.GetString("tbttabaction.Text");
			this.tbttabaction.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbttabaction.TextAlign")));
			this.tbttabaction.Visible = ((bool)(resources.GetObject("tbttabaction.Visible")));
			this.tbttabaction.WordWrap = ((bool)(resources.GetObject("tbttabaction.WordWrap")));
			this.tbttabaction.TextChanged += new System.EventHandler(this.ActionOrGuardianChanged);
			// 
			// label21
			// 
			this.label21.AccessibleDescription = resources.GetString("label21.AccessibleDescription");
			this.label21.AccessibleName = resources.GetString("label21.AccessibleName");
			this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label21.Anchor")));
			this.label21.AutoSize = ((bool)(resources.GetObject("label21.AutoSize")));
			this.label21.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label21.Dock")));
			this.label21.Enabled = ((bool)(resources.GetObject("label21.Enabled")));
			this.label21.Font = ((System.Drawing.Font)(resources.GetObject("label21.Font")));
			this.label21.Image = ((System.Drawing.Image)(resources.GetObject("label21.Image")));
			this.label21.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label21.ImageAlign")));
			this.label21.ImageIndex = ((int)(resources.GetObject("label21.ImageIndex")));
			this.label21.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label21.ImeMode")));
			this.label21.Location = ((System.Drawing.Point)(resources.GetObject("label21.Location")));
			this.label21.Name = "label21";
			this.label21.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label21.RightToLeft")));
			this.label21.Size = ((System.Drawing.Size)(resources.GetObject("label21.Size")));
			this.label21.TabIndex = ((int)(resources.GetObject("label21.TabIndex")));
			this.label21.Text = resources.GetString("label21.Text");
			this.label21.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label21.TextAlign")));
			this.label21.Visible = ((bool)(resources.GetObject("label21.Visible")));
			// 
			// linkLabel1
			// 
			this.linkLabel1.AccessibleDescription = resources.GetString("linkLabel1.AccessibleDescription");
			this.linkLabel1.AccessibleName = resources.GetString("linkLabel1.AccessibleName");
			this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("linkLabel1.Anchor")));
			this.linkLabel1.AutoSize = ((bool)(resources.GetObject("linkLabel1.AutoSize")));
			this.linkLabel1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("linkLabel1.Dock")));
			this.linkLabel1.Enabled = ((bool)(resources.GetObject("linkLabel1.Enabled")));
			this.linkLabel1.Font = ((System.Drawing.Font)(resources.GetObject("linkLabel1.Font")));
			this.linkLabel1.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel1.Image")));
			this.linkLabel1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel1.ImageAlign")));
			this.linkLabel1.ImageIndex = ((int)(resources.GetObject("linkLabel1.ImageIndex")));
			this.linkLabel1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("linkLabel1.ImeMode")));
			this.linkLabel1.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("linkLabel1.LinkArea")));
			this.linkLabel1.Location = ((System.Drawing.Point)(resources.GetObject("linkLabel1.Location")));
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("linkLabel1.RightToLeft")));
			this.linkLabel1.Size = ((System.Drawing.Size)(resources.GetObject("linkLabel1.Size")));
			this.linkLabel1.TabIndex = ((int)(resources.GetObject("linkLabel1.TabIndex")));
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = resources.GetString("linkLabel1.Text");
			this.linkLabel1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel1.TextAlign")));
			this.linkLabel1.Visible = ((bool)(resources.GetObject("linkLabel1.Visible")));
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AddTtab);
			// 
			// llchangettab
			// 
			this.llchangettab.AccessibleDescription = resources.GetString("llchangettab.AccessibleDescription");
			this.llchangettab.AccessibleName = resources.GetString("llchangettab.AccessibleName");
			this.llchangettab.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llchangettab.Anchor")));
			this.llchangettab.AutoSize = ((bool)(resources.GetObject("llchangettab.AutoSize")));
			this.llchangettab.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llchangettab.Dock")));
			this.llchangettab.Enabled = ((bool)(resources.GetObject("llchangettab.Enabled")));
			this.llchangettab.Font = ((System.Drawing.Font)(resources.GetObject("llchangettab.Font")));
			this.llchangettab.Image = ((System.Drawing.Image)(resources.GetObject("llchangettab.Image")));
			this.llchangettab.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llchangettab.ImageAlign")));
			this.llchangettab.ImageIndex = ((int)(resources.GetObject("llchangettab.ImageIndex")));
			this.llchangettab.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llchangettab.ImeMode")));
			this.llchangettab.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llchangettab.LinkArea")));
			this.llchangettab.Location = ((System.Drawing.Point)(resources.GetObject("llchangettab.Location")));
			this.llchangettab.Name = "llchangettab";
			this.llchangettab.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llchangettab.RightToLeft")));
			this.llchangettab.Size = ((System.Drawing.Size)(resources.GetObject("llchangettab.Size")));
			this.llchangettab.TabIndex = ((int)(resources.GetObject("llchangettab.TabIndex")));
			this.llchangettab.TabStop = true;
			this.llchangettab.Text = resources.GetString("llchangettab.Text");
			this.llchangettab.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llchangettab.TextAlign")));
			this.llchangettab.Visible = ((bool)(resources.GetObject("llchangettab.Visible")));
			this.llchangettab.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.TtabItemChange);
			// 
			// tbttabguard
			// 
			this.tbttabguard.AccessibleDescription = resources.GetString("tbttabguard.AccessibleDescription");
			this.tbttabguard.AccessibleName = resources.GetString("tbttabguard.AccessibleName");
			this.tbttabguard.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbttabguard.Anchor")));
			this.tbttabguard.AutoSize = ((bool)(resources.GetObject("tbttabguard.AutoSize")));
			this.tbttabguard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbttabguard.BackgroundImage")));
			this.tbttabguard.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbttabguard.Dock")));
			this.tbttabguard.Enabled = ((bool)(resources.GetObject("tbttabguard.Enabled")));
			this.tbttabguard.Font = ((System.Drawing.Font)(resources.GetObject("tbttabguard.Font")));
			this.tbttabguard.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbttabguard.ImeMode")));
			this.tbttabguard.Location = ((System.Drawing.Point)(resources.GetObject("tbttabguard.Location")));
			this.tbttabguard.MaxLength = ((int)(resources.GetObject("tbttabguard.MaxLength")));
			this.tbttabguard.Multiline = ((bool)(resources.GetObject("tbttabguard.Multiline")));
			this.tbttabguard.Name = "tbttabguard";
			this.tbttabguard.PasswordChar = ((char)(resources.GetObject("tbttabguard.PasswordChar")));
			this.tbttabguard.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbttabguard.RightToLeft")));
			this.tbttabguard.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbttabguard.ScrollBars")));
			this.tbttabguard.Size = ((System.Drawing.Size)(resources.GetObject("tbttabguard.Size")));
			this.tbttabguard.TabIndex = ((int)(resources.GetObject("tbttabguard.TabIndex")));
			this.tbttabguard.Text = resources.GetString("tbttabguard.Text");
			this.tbttabguard.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbttabguard.TextAlign")));
			this.tbttabguard.Visible = ((bool)(resources.GetObject("tbttabguard.Visible")));
			this.tbttabguard.WordWrap = ((bool)(resources.GetObject("tbttabguard.WordWrap")));
			this.tbttabguard.TextChanged += new System.EventHandler(this.ActionOrGuardianChanged);
			// 
			// label23
			// 
			this.label23.AccessibleDescription = resources.GetString("label23.AccessibleDescription");
			this.label23.AccessibleName = resources.GetString("label23.AccessibleName");
			this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label23.Anchor")));
			this.label23.AutoSize = ((bool)(resources.GetObject("label23.AutoSize")));
			this.label23.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label23.Dock")));
			this.label23.Enabled = ((bool)(resources.GetObject("label23.Enabled")));
			this.label23.Font = ((System.Drawing.Font)(resources.GetObject("label23.Font")));
			this.label23.Image = ((System.Drawing.Image)(resources.GetObject("label23.Image")));
			this.label23.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label23.ImageAlign")));
			this.label23.ImageIndex = ((int)(resources.GetObject("label23.ImageIndex")));
			this.label23.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label23.ImeMode")));
			this.label23.Location = ((System.Drawing.Point)(resources.GetObject("label23.Location")));
			this.label23.Name = "label23";
			this.label23.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label23.RightToLeft")));
			this.label23.Size = ((System.Drawing.Size)(resources.GetObject("label23.Size")));
			this.label23.TabIndex = ((int)(resources.GetObject("label23.TabIndex")));
			this.label23.Text = resources.GetString("label23.Text");
			this.label23.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label23.TextAlign")));
			this.label23.Visible = ((bool)(resources.GetObject("label23.Visible")));
			// 
			// groupBox4
			// 
			this.groupBox4.AccessibleDescription = resources.GetString("groupBox4.AccessibleDescription");
			this.groupBox4.AccessibleName = resources.GetString("groupBox4.AccessibleName");
			this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox4.Anchor")));
			this.groupBox4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox4.BackgroundImage")));
			this.groupBox4.Controls.Add(this.cbunk3);
			this.groupBox4.Controls.Add(this.cbunk4);
			this.groupBox4.Controls.Add(this.cbunk1);
			this.groupBox4.Controls.Add(this.cbunk2);
			this.groupBox4.Controls.Add(this.cbteens);
			this.groupBox4.Controls.Add(this.cbelders);
			this.groupBox4.Controls.Add(this.cbtodlers);
			this.groupBox4.Controls.Add(this.cbautofirst);
			this.groupBox4.Controls.Add(this.cbdebugmenu);
			this.groupBox4.Controls.Add(this.cbadults);
			this.groupBox4.Controls.Add(this.cbdemochild);
			this.groupBox4.Controls.Add(this.cbchildren);
			this.groupBox4.Controls.Add(this.cbconsecutive);
			this.groupBox4.Controls.Add(this.cbimmediately);
			this.groupBox4.Controls.Add(this.cbjoinable);
			this.groupBox4.Controls.Add(this.cbvisitor);
			this.groupBox4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("groupBox4.Dock")));
			this.groupBox4.Enabled = ((bool)(resources.GetObject("groupBox4.Enabled")));
			this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox4.Font = ((System.Drawing.Font)(resources.GetObject("groupBox4.Font")));
			this.groupBox4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("groupBox4.ImeMode")));
			this.groupBox4.Location = ((System.Drawing.Point)(resources.GetObject("groupBox4.Location")));
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("groupBox4.RightToLeft")));
			this.groupBox4.Size = ((System.Drawing.Size)(resources.GetObject("groupBox4.Size")));
			this.groupBox4.TabIndex = ((int)(resources.GetObject("groupBox4.TabIndex")));
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = resources.GetString("groupBox4.Text");
			this.groupBox4.Visible = ((bool)(resources.GetObject("groupBox4.Visible")));
			// 
			// cbunk3
			// 
			this.cbunk3.AccessibleDescription = resources.GetString("cbunk3.AccessibleDescription");
			this.cbunk3.AccessibleName = resources.GetString("cbunk3.AccessibleName");
			this.cbunk3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbunk3.Anchor")));
			this.cbunk3.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbunk3.Appearance")));
			this.cbunk3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbunk3.BackgroundImage")));
			this.cbunk3.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk3.CheckAlign")));
			this.cbunk3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbunk3.Dock")));
			this.cbunk3.Enabled = ((bool)(resources.GetObject("cbunk3.Enabled")));
			this.cbunk3.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbunk3.FlatStyle")));
			this.cbunk3.Font = ((System.Drawing.Font)(resources.GetObject("cbunk3.Font")));
			this.cbunk3.Image = ((System.Drawing.Image)(resources.GetObject("cbunk3.Image")));
			this.cbunk3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk3.ImageAlign")));
			this.cbunk3.ImageIndex = ((int)(resources.GetObject("cbunk3.ImageIndex")));
			this.cbunk3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbunk3.ImeMode")));
			this.cbunk3.Location = ((System.Drawing.Point)(resources.GetObject("cbunk3.Location")));
			this.cbunk3.Name = "cbunk3";
			this.cbunk3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbunk3.RightToLeft")));
			this.cbunk3.Size = ((System.Drawing.Size)(resources.GetObject("cbunk3.Size")));
			this.cbunk3.TabIndex = ((int)(resources.GetObject("cbunk3.TabIndex")));
			this.cbunk3.Text = resources.GetString("cbunk3.Text");
			this.cbunk3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk3.TextAlign")));
			this.cbunk3.Visible = ((bool)(resources.GetObject("cbunk3.Visible")));
			this.cbunk3.CheckedChanged += new System.EventHandler(this.UpdateFlagsValue);
			// 
			// cbunk4
			// 
			this.cbunk4.AccessibleDescription = resources.GetString("cbunk4.AccessibleDescription");
			this.cbunk4.AccessibleName = resources.GetString("cbunk4.AccessibleName");
			this.cbunk4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbunk4.Anchor")));
			this.cbunk4.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbunk4.Appearance")));
			this.cbunk4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbunk4.BackgroundImage")));
			this.cbunk4.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk4.CheckAlign")));
			this.cbunk4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbunk4.Dock")));
			this.cbunk4.Enabled = ((bool)(resources.GetObject("cbunk4.Enabled")));
			this.cbunk4.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbunk4.FlatStyle")));
			this.cbunk4.Font = ((System.Drawing.Font)(resources.GetObject("cbunk4.Font")));
			this.cbunk4.Image = ((System.Drawing.Image)(resources.GetObject("cbunk4.Image")));
			this.cbunk4.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk4.ImageAlign")));
			this.cbunk4.ImageIndex = ((int)(resources.GetObject("cbunk4.ImageIndex")));
			this.cbunk4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbunk4.ImeMode")));
			this.cbunk4.Location = ((System.Drawing.Point)(resources.GetObject("cbunk4.Location")));
			this.cbunk4.Name = "cbunk4";
			this.cbunk4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbunk4.RightToLeft")));
			this.cbunk4.Size = ((System.Drawing.Size)(resources.GetObject("cbunk4.Size")));
			this.cbunk4.TabIndex = ((int)(resources.GetObject("cbunk4.TabIndex")));
			this.cbunk4.Text = resources.GetString("cbunk4.Text");
			this.cbunk4.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk4.TextAlign")));
			this.cbunk4.Visible = ((bool)(resources.GetObject("cbunk4.Visible")));
			this.cbunk4.CheckedChanged += new System.EventHandler(this.UpdateFlagsValue);
			// 
			// cbunk1
			// 
			this.cbunk1.AccessibleDescription = resources.GetString("cbunk1.AccessibleDescription");
			this.cbunk1.AccessibleName = resources.GetString("cbunk1.AccessibleName");
			this.cbunk1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbunk1.Anchor")));
			this.cbunk1.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbunk1.Appearance")));
			this.cbunk1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbunk1.BackgroundImage")));
			this.cbunk1.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk1.CheckAlign")));
			this.cbunk1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbunk1.Dock")));
			this.cbunk1.Enabled = ((bool)(resources.GetObject("cbunk1.Enabled")));
			this.cbunk1.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbunk1.FlatStyle")));
			this.cbunk1.Font = ((System.Drawing.Font)(resources.GetObject("cbunk1.Font")));
			this.cbunk1.Image = ((System.Drawing.Image)(resources.GetObject("cbunk1.Image")));
			this.cbunk1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk1.ImageAlign")));
			this.cbunk1.ImageIndex = ((int)(resources.GetObject("cbunk1.ImageIndex")));
			this.cbunk1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbunk1.ImeMode")));
			this.cbunk1.Location = ((System.Drawing.Point)(resources.GetObject("cbunk1.Location")));
			this.cbunk1.Name = "cbunk1";
			this.cbunk1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbunk1.RightToLeft")));
			this.cbunk1.Size = ((System.Drawing.Size)(resources.GetObject("cbunk1.Size")));
			this.cbunk1.TabIndex = ((int)(resources.GetObject("cbunk1.TabIndex")));
			this.cbunk1.Text = resources.GetString("cbunk1.Text");
			this.cbunk1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk1.TextAlign")));
			this.cbunk1.Visible = ((bool)(resources.GetObject("cbunk1.Visible")));
			this.cbunk1.CheckedChanged += new System.EventHandler(this.UpdateFlagsValue);
			// 
			// cbunk2
			// 
			this.cbunk2.AccessibleDescription = resources.GetString("cbunk2.AccessibleDescription");
			this.cbunk2.AccessibleName = resources.GetString("cbunk2.AccessibleName");
			this.cbunk2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbunk2.Anchor")));
			this.cbunk2.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbunk2.Appearance")));
			this.cbunk2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbunk2.BackgroundImage")));
			this.cbunk2.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk2.CheckAlign")));
			this.cbunk2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbunk2.Dock")));
			this.cbunk2.Enabled = ((bool)(resources.GetObject("cbunk2.Enabled")));
			this.cbunk2.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbunk2.FlatStyle")));
			this.cbunk2.Font = ((System.Drawing.Font)(resources.GetObject("cbunk2.Font")));
			this.cbunk2.Image = ((System.Drawing.Image)(resources.GetObject("cbunk2.Image")));
			this.cbunk2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk2.ImageAlign")));
			this.cbunk2.ImageIndex = ((int)(resources.GetObject("cbunk2.ImageIndex")));
			this.cbunk2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbunk2.ImeMode")));
			this.cbunk2.Location = ((System.Drawing.Point)(resources.GetObject("cbunk2.Location")));
			this.cbunk2.Name = "cbunk2";
			this.cbunk2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbunk2.RightToLeft")));
			this.cbunk2.Size = ((System.Drawing.Size)(resources.GetObject("cbunk2.Size")));
			this.cbunk2.TabIndex = ((int)(resources.GetObject("cbunk2.TabIndex")));
			this.cbunk2.Text = resources.GetString("cbunk2.Text");
			this.cbunk2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbunk2.TextAlign")));
			this.cbunk2.Visible = ((bool)(resources.GetObject("cbunk2.Visible")));
			this.cbunk2.CheckedChanged += new System.EventHandler(this.UpdateFlagsValue);
			// 
			// cbteens
			// 
			this.cbteens.AccessibleDescription = resources.GetString("cbteens.AccessibleDescription");
			this.cbteens.AccessibleName = resources.GetString("cbteens.AccessibleName");
			this.cbteens.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbteens.Anchor")));
			this.cbteens.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbteens.Appearance")));
			this.cbteens.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbteens.BackgroundImage")));
			this.cbteens.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbteens.CheckAlign")));
			this.cbteens.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbteens.Dock")));
			this.cbteens.Enabled = ((bool)(resources.GetObject("cbteens.Enabled")));
			this.cbteens.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbteens.FlatStyle")));
			this.cbteens.Font = ((System.Drawing.Font)(resources.GetObject("cbteens.Font")));
			this.cbteens.Image = ((System.Drawing.Image)(resources.GetObject("cbteens.Image")));
			this.cbteens.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbteens.ImageAlign")));
			this.cbteens.ImageIndex = ((int)(resources.GetObject("cbteens.ImageIndex")));
			this.cbteens.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbteens.ImeMode")));
			this.cbteens.Location = ((System.Drawing.Point)(resources.GetObject("cbteens.Location")));
			this.cbteens.Name = "cbteens";
			this.cbteens.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbteens.RightToLeft")));
			this.cbteens.Size = ((System.Drawing.Size)(resources.GetObject("cbteens.Size")));
			this.cbteens.TabIndex = ((int)(resources.GetObject("cbteens.TabIndex")));
			this.cbteens.Text = resources.GetString("cbteens.Text");
			this.cbteens.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbteens.TextAlign")));
			this.cbteens.Visible = ((bool)(resources.GetObject("cbteens.Visible")));
			this.cbteens.CheckedChanged += new System.EventHandler(this.UpdateFlagsValue);
			// 
			// cbelders
			// 
			this.cbelders.AccessibleDescription = resources.GetString("cbelders.AccessibleDescription");
			this.cbelders.AccessibleName = resources.GetString("cbelders.AccessibleName");
			this.cbelders.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbelders.Anchor")));
			this.cbelders.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbelders.Appearance")));
			this.cbelders.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbelders.BackgroundImage")));
			this.cbelders.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbelders.CheckAlign")));
			this.cbelders.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbelders.Dock")));
			this.cbelders.Enabled = ((bool)(resources.GetObject("cbelders.Enabled")));
			this.cbelders.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbelders.FlatStyle")));
			this.cbelders.Font = ((System.Drawing.Font)(resources.GetObject("cbelders.Font")));
			this.cbelders.Image = ((System.Drawing.Image)(resources.GetObject("cbelders.Image")));
			this.cbelders.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbelders.ImageAlign")));
			this.cbelders.ImageIndex = ((int)(resources.GetObject("cbelders.ImageIndex")));
			this.cbelders.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbelders.ImeMode")));
			this.cbelders.Location = ((System.Drawing.Point)(resources.GetObject("cbelders.Location")));
			this.cbelders.Name = "cbelders";
			this.cbelders.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbelders.RightToLeft")));
			this.cbelders.Size = ((System.Drawing.Size)(resources.GetObject("cbelders.Size")));
			this.cbelders.TabIndex = ((int)(resources.GetObject("cbelders.TabIndex")));
			this.cbelders.Text = resources.GetString("cbelders.Text");
			this.cbelders.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbelders.TextAlign")));
			this.cbelders.Visible = ((bool)(resources.GetObject("cbelders.Visible")));
			this.cbelders.CheckedChanged += new System.EventHandler(this.UpdateFlagsValue);
			// 
			// cbtodlers
			// 
			this.cbtodlers.AccessibleDescription = resources.GetString("cbtodlers.AccessibleDescription");
			this.cbtodlers.AccessibleName = resources.GetString("cbtodlers.AccessibleName");
			this.cbtodlers.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbtodlers.Anchor")));
			this.cbtodlers.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbtodlers.Appearance")));
			this.cbtodlers.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbtodlers.BackgroundImage")));
			this.cbtodlers.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbtodlers.CheckAlign")));
			this.cbtodlers.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbtodlers.Dock")));
			this.cbtodlers.Enabled = ((bool)(resources.GetObject("cbtodlers.Enabled")));
			this.cbtodlers.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbtodlers.FlatStyle")));
			this.cbtodlers.Font = ((System.Drawing.Font)(resources.GetObject("cbtodlers.Font")));
			this.cbtodlers.Image = ((System.Drawing.Image)(resources.GetObject("cbtodlers.Image")));
			this.cbtodlers.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbtodlers.ImageAlign")));
			this.cbtodlers.ImageIndex = ((int)(resources.GetObject("cbtodlers.ImageIndex")));
			this.cbtodlers.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbtodlers.ImeMode")));
			this.cbtodlers.Location = ((System.Drawing.Point)(resources.GetObject("cbtodlers.Location")));
			this.cbtodlers.Name = "cbtodlers";
			this.cbtodlers.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbtodlers.RightToLeft")));
			this.cbtodlers.Size = ((System.Drawing.Size)(resources.GetObject("cbtodlers.Size")));
			this.cbtodlers.TabIndex = ((int)(resources.GetObject("cbtodlers.TabIndex")));
			this.cbtodlers.Text = resources.GetString("cbtodlers.Text");
			this.cbtodlers.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbtodlers.TextAlign")));
			this.cbtodlers.Visible = ((bool)(resources.GetObject("cbtodlers.Visible")));
			this.cbtodlers.CheckedChanged += new System.EventHandler(this.UpdateFlagsValue);
			// 
			// cbautofirst
			// 
			this.cbautofirst.AccessibleDescription = resources.GetString("cbautofirst.AccessibleDescription");
			this.cbautofirst.AccessibleName = resources.GetString("cbautofirst.AccessibleName");
			this.cbautofirst.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbautofirst.Anchor")));
			this.cbautofirst.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbautofirst.Appearance")));
			this.cbautofirst.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbautofirst.BackgroundImage")));
			this.cbautofirst.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbautofirst.CheckAlign")));
			this.cbautofirst.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbautofirst.Dock")));
			this.cbautofirst.Enabled = ((bool)(resources.GetObject("cbautofirst.Enabled")));
			this.cbautofirst.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbautofirst.FlatStyle")));
			this.cbautofirst.Font = ((System.Drawing.Font)(resources.GetObject("cbautofirst.Font")));
			this.cbautofirst.Image = ((System.Drawing.Image)(resources.GetObject("cbautofirst.Image")));
			this.cbautofirst.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbautofirst.ImageAlign")));
			this.cbautofirst.ImageIndex = ((int)(resources.GetObject("cbautofirst.ImageIndex")));
			this.cbautofirst.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbautofirst.ImeMode")));
			this.cbautofirst.Location = ((System.Drawing.Point)(resources.GetObject("cbautofirst.Location")));
			this.cbautofirst.Name = "cbautofirst";
			this.cbautofirst.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbautofirst.RightToLeft")));
			this.cbautofirst.Size = ((System.Drawing.Size)(resources.GetObject("cbautofirst.Size")));
			this.cbautofirst.TabIndex = ((int)(resources.GetObject("cbautofirst.TabIndex")));
			this.cbautofirst.Text = resources.GetString("cbautofirst.Text");
			this.cbautofirst.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbautofirst.TextAlign")));
			this.cbautofirst.Visible = ((bool)(resources.GetObject("cbautofirst.Visible")));
			this.cbautofirst.CheckedChanged += new System.EventHandler(this.UpdateFlagsValue);
			// 
			// cbdebugmenu
			// 
			this.cbdebugmenu.AccessibleDescription = resources.GetString("cbdebugmenu.AccessibleDescription");
			this.cbdebugmenu.AccessibleName = resources.GetString("cbdebugmenu.AccessibleName");
			this.cbdebugmenu.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbdebugmenu.Anchor")));
			this.cbdebugmenu.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbdebugmenu.Appearance")));
			this.cbdebugmenu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbdebugmenu.BackgroundImage")));
			this.cbdebugmenu.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbdebugmenu.CheckAlign")));
			this.cbdebugmenu.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbdebugmenu.Dock")));
			this.cbdebugmenu.Enabled = ((bool)(resources.GetObject("cbdebugmenu.Enabled")));
			this.cbdebugmenu.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbdebugmenu.FlatStyle")));
			this.cbdebugmenu.Font = ((System.Drawing.Font)(resources.GetObject("cbdebugmenu.Font")));
			this.cbdebugmenu.Image = ((System.Drawing.Image)(resources.GetObject("cbdebugmenu.Image")));
			this.cbdebugmenu.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbdebugmenu.ImageAlign")));
			this.cbdebugmenu.ImageIndex = ((int)(resources.GetObject("cbdebugmenu.ImageIndex")));
			this.cbdebugmenu.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbdebugmenu.ImeMode")));
			this.cbdebugmenu.Location = ((System.Drawing.Point)(resources.GetObject("cbdebugmenu.Location")));
			this.cbdebugmenu.Name = "cbdebugmenu";
			this.cbdebugmenu.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbdebugmenu.RightToLeft")));
			this.cbdebugmenu.Size = ((System.Drawing.Size)(resources.GetObject("cbdebugmenu.Size")));
			this.cbdebugmenu.TabIndex = ((int)(resources.GetObject("cbdebugmenu.TabIndex")));
			this.cbdebugmenu.Text = resources.GetString("cbdebugmenu.Text");
			this.cbdebugmenu.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbdebugmenu.TextAlign")));
			this.cbdebugmenu.Visible = ((bool)(resources.GetObject("cbdebugmenu.Visible")));
			this.cbdebugmenu.CheckedChanged += new System.EventHandler(this.UpdateFlagsValue);
			// 
			// cbadults
			// 
			this.cbadults.AccessibleDescription = resources.GetString("cbadults.AccessibleDescription");
			this.cbadults.AccessibleName = resources.GetString("cbadults.AccessibleName");
			this.cbadults.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbadults.Anchor")));
			this.cbadults.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbadults.Appearance")));
			this.cbadults.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbadults.BackgroundImage")));
			this.cbadults.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbadults.CheckAlign")));
			this.cbadults.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbadults.Dock")));
			this.cbadults.Enabled = ((bool)(resources.GetObject("cbadults.Enabled")));
			this.cbadults.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbadults.FlatStyle")));
			this.cbadults.Font = ((System.Drawing.Font)(resources.GetObject("cbadults.Font")));
			this.cbadults.Image = ((System.Drawing.Image)(resources.GetObject("cbadults.Image")));
			this.cbadults.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbadults.ImageAlign")));
			this.cbadults.ImageIndex = ((int)(resources.GetObject("cbadults.ImageIndex")));
			this.cbadults.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbadults.ImeMode")));
			this.cbadults.Location = ((System.Drawing.Point)(resources.GetObject("cbadults.Location")));
			this.cbadults.Name = "cbadults";
			this.cbadults.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbadults.RightToLeft")));
			this.cbadults.Size = ((System.Drawing.Size)(resources.GetObject("cbadults.Size")));
			this.cbadults.TabIndex = ((int)(resources.GetObject("cbadults.TabIndex")));
			this.cbadults.Text = resources.GetString("cbadults.Text");
			this.cbadults.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbadults.TextAlign")));
			this.cbadults.Visible = ((bool)(resources.GetObject("cbadults.Visible")));
			this.cbadults.CheckedChanged += new System.EventHandler(this.UpdateFlagsValue);
			// 
			// cbdemochild
			// 
			this.cbdemochild.AccessibleDescription = resources.GetString("cbdemochild.AccessibleDescription");
			this.cbdemochild.AccessibleName = resources.GetString("cbdemochild.AccessibleName");
			this.cbdemochild.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbdemochild.Anchor")));
			this.cbdemochild.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbdemochild.Appearance")));
			this.cbdemochild.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbdemochild.BackgroundImage")));
			this.cbdemochild.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbdemochild.CheckAlign")));
			this.cbdemochild.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbdemochild.Dock")));
			this.cbdemochild.Enabled = ((bool)(resources.GetObject("cbdemochild.Enabled")));
			this.cbdemochild.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbdemochild.FlatStyle")));
			this.cbdemochild.Font = ((System.Drawing.Font)(resources.GetObject("cbdemochild.Font")));
			this.cbdemochild.Image = ((System.Drawing.Image)(resources.GetObject("cbdemochild.Image")));
			this.cbdemochild.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbdemochild.ImageAlign")));
			this.cbdemochild.ImageIndex = ((int)(resources.GetObject("cbdemochild.ImageIndex")));
			this.cbdemochild.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbdemochild.ImeMode")));
			this.cbdemochild.Location = ((System.Drawing.Point)(resources.GetObject("cbdemochild.Location")));
			this.cbdemochild.Name = "cbdemochild";
			this.cbdemochild.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbdemochild.RightToLeft")));
			this.cbdemochild.Size = ((System.Drawing.Size)(resources.GetObject("cbdemochild.Size")));
			this.cbdemochild.TabIndex = ((int)(resources.GetObject("cbdemochild.TabIndex")));
			this.cbdemochild.Text = resources.GetString("cbdemochild.Text");
			this.cbdemochild.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbdemochild.TextAlign")));
			this.cbdemochild.Visible = ((bool)(resources.GetObject("cbdemochild.Visible")));
			this.cbdemochild.CheckedChanged += new System.EventHandler(this.UpdateFlagsValue);
			// 
			// cbchildren
			// 
			this.cbchildren.AccessibleDescription = resources.GetString("cbchildren.AccessibleDescription");
			this.cbchildren.AccessibleName = resources.GetString("cbchildren.AccessibleName");
			this.cbchildren.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbchildren.Anchor")));
			this.cbchildren.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbchildren.Appearance")));
			this.cbchildren.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbchildren.BackgroundImage")));
			this.cbchildren.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbchildren.CheckAlign")));
			this.cbchildren.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbchildren.Dock")));
			this.cbchildren.Enabled = ((bool)(resources.GetObject("cbchildren.Enabled")));
			this.cbchildren.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbchildren.FlatStyle")));
			this.cbchildren.Font = ((System.Drawing.Font)(resources.GetObject("cbchildren.Font")));
			this.cbchildren.Image = ((System.Drawing.Image)(resources.GetObject("cbchildren.Image")));
			this.cbchildren.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbchildren.ImageAlign")));
			this.cbchildren.ImageIndex = ((int)(resources.GetObject("cbchildren.ImageIndex")));
			this.cbchildren.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbchildren.ImeMode")));
			this.cbchildren.Location = ((System.Drawing.Point)(resources.GetObject("cbchildren.Location")));
			this.cbchildren.Name = "cbchildren";
			this.cbchildren.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbchildren.RightToLeft")));
			this.cbchildren.Size = ((System.Drawing.Size)(resources.GetObject("cbchildren.Size")));
			this.cbchildren.TabIndex = ((int)(resources.GetObject("cbchildren.TabIndex")));
			this.cbchildren.Text = resources.GetString("cbchildren.Text");
			this.cbchildren.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbchildren.TextAlign")));
			this.cbchildren.Visible = ((bool)(resources.GetObject("cbchildren.Visible")));
			this.cbchildren.CheckedChanged += new System.EventHandler(this.UpdateFlagsValue);
			// 
			// cbconsecutive
			// 
			this.cbconsecutive.AccessibleDescription = resources.GetString("cbconsecutive.AccessibleDescription");
			this.cbconsecutive.AccessibleName = resources.GetString("cbconsecutive.AccessibleName");
			this.cbconsecutive.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbconsecutive.Anchor")));
			this.cbconsecutive.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbconsecutive.Appearance")));
			this.cbconsecutive.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbconsecutive.BackgroundImage")));
			this.cbconsecutive.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbconsecutive.CheckAlign")));
			this.cbconsecutive.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbconsecutive.Dock")));
			this.cbconsecutive.Enabled = ((bool)(resources.GetObject("cbconsecutive.Enabled")));
			this.cbconsecutive.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbconsecutive.FlatStyle")));
			this.cbconsecutive.Font = ((System.Drawing.Font)(resources.GetObject("cbconsecutive.Font")));
			this.cbconsecutive.Image = ((System.Drawing.Image)(resources.GetObject("cbconsecutive.Image")));
			this.cbconsecutive.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbconsecutive.ImageAlign")));
			this.cbconsecutive.ImageIndex = ((int)(resources.GetObject("cbconsecutive.ImageIndex")));
			this.cbconsecutive.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbconsecutive.ImeMode")));
			this.cbconsecutive.Location = ((System.Drawing.Point)(resources.GetObject("cbconsecutive.Location")));
			this.cbconsecutive.Name = "cbconsecutive";
			this.cbconsecutive.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbconsecutive.RightToLeft")));
			this.cbconsecutive.Size = ((System.Drawing.Size)(resources.GetObject("cbconsecutive.Size")));
			this.cbconsecutive.TabIndex = ((int)(resources.GetObject("cbconsecutive.TabIndex")));
			this.cbconsecutive.Text = resources.GetString("cbconsecutive.Text");
			this.cbconsecutive.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbconsecutive.TextAlign")));
			this.cbconsecutive.Visible = ((bool)(resources.GetObject("cbconsecutive.Visible")));
			this.cbconsecutive.CheckedChanged += new System.EventHandler(this.UpdateFlagsValue);
			// 
			// cbimmediately
			// 
			this.cbimmediately.AccessibleDescription = resources.GetString("cbimmediately.AccessibleDescription");
			this.cbimmediately.AccessibleName = resources.GetString("cbimmediately.AccessibleName");
			this.cbimmediately.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbimmediately.Anchor")));
			this.cbimmediately.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbimmediately.Appearance")));
			this.cbimmediately.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbimmediately.BackgroundImage")));
			this.cbimmediately.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbimmediately.CheckAlign")));
			this.cbimmediately.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbimmediately.Dock")));
			this.cbimmediately.Enabled = ((bool)(resources.GetObject("cbimmediately.Enabled")));
			this.cbimmediately.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbimmediately.FlatStyle")));
			this.cbimmediately.Font = ((System.Drawing.Font)(resources.GetObject("cbimmediately.Font")));
			this.cbimmediately.Image = ((System.Drawing.Image)(resources.GetObject("cbimmediately.Image")));
			this.cbimmediately.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbimmediately.ImageAlign")));
			this.cbimmediately.ImageIndex = ((int)(resources.GetObject("cbimmediately.ImageIndex")));
			this.cbimmediately.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbimmediately.ImeMode")));
			this.cbimmediately.Location = ((System.Drawing.Point)(resources.GetObject("cbimmediately.Location")));
			this.cbimmediately.Name = "cbimmediately";
			this.cbimmediately.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbimmediately.RightToLeft")));
			this.cbimmediately.Size = ((System.Drawing.Size)(resources.GetObject("cbimmediately.Size")));
			this.cbimmediately.TabIndex = ((int)(resources.GetObject("cbimmediately.TabIndex")));
			this.cbimmediately.Text = resources.GetString("cbimmediately.Text");
			this.cbimmediately.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbimmediately.TextAlign")));
			this.cbimmediately.Visible = ((bool)(resources.GetObject("cbimmediately.Visible")));
			this.cbimmediately.CheckedChanged += new System.EventHandler(this.UpdateFlagsValue);
			// 
			// cbjoinable
			// 
			this.cbjoinable.AccessibleDescription = resources.GetString("cbjoinable.AccessibleDescription");
			this.cbjoinable.AccessibleName = resources.GetString("cbjoinable.AccessibleName");
			this.cbjoinable.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbjoinable.Anchor")));
			this.cbjoinable.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbjoinable.Appearance")));
			this.cbjoinable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbjoinable.BackgroundImage")));
			this.cbjoinable.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbjoinable.CheckAlign")));
			this.cbjoinable.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbjoinable.Dock")));
			this.cbjoinable.Enabled = ((bool)(resources.GetObject("cbjoinable.Enabled")));
			this.cbjoinable.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbjoinable.FlatStyle")));
			this.cbjoinable.Font = ((System.Drawing.Font)(resources.GetObject("cbjoinable.Font")));
			this.cbjoinable.Image = ((System.Drawing.Image)(resources.GetObject("cbjoinable.Image")));
			this.cbjoinable.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbjoinable.ImageAlign")));
			this.cbjoinable.ImageIndex = ((int)(resources.GetObject("cbjoinable.ImageIndex")));
			this.cbjoinable.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbjoinable.ImeMode")));
			this.cbjoinable.Location = ((System.Drawing.Point)(resources.GetObject("cbjoinable.Location")));
			this.cbjoinable.Name = "cbjoinable";
			this.cbjoinable.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbjoinable.RightToLeft")));
			this.cbjoinable.Size = ((System.Drawing.Size)(resources.GetObject("cbjoinable.Size")));
			this.cbjoinable.TabIndex = ((int)(resources.GetObject("cbjoinable.TabIndex")));
			this.cbjoinable.Text = resources.GetString("cbjoinable.Text");
			this.cbjoinable.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbjoinable.TextAlign")));
			this.cbjoinable.Visible = ((bool)(resources.GetObject("cbjoinable.Visible")));
			this.cbjoinable.CheckedChanged += new System.EventHandler(this.UpdateFlagsValue);
			// 
			// cbvisitor
			// 
			this.cbvisitor.AccessibleDescription = resources.GetString("cbvisitor.AccessibleDescription");
			this.cbvisitor.AccessibleName = resources.GetString("cbvisitor.AccessibleName");
			this.cbvisitor.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbvisitor.Anchor")));
			this.cbvisitor.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbvisitor.Appearance")));
			this.cbvisitor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbvisitor.BackgroundImage")));
			this.cbvisitor.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbvisitor.CheckAlign")));
			this.cbvisitor.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbvisitor.Dock")));
			this.cbvisitor.Enabled = ((bool)(resources.GetObject("cbvisitor.Enabled")));
			this.cbvisitor.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbvisitor.FlatStyle")));
			this.cbvisitor.Font = ((System.Drawing.Font)(resources.GetObject("cbvisitor.Font")));
			this.cbvisitor.Image = ((System.Drawing.Image)(resources.GetObject("cbvisitor.Image")));
			this.cbvisitor.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbvisitor.ImageAlign")));
			this.cbvisitor.ImageIndex = ((int)(resources.GetObject("cbvisitor.ImageIndex")));
			this.cbvisitor.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbvisitor.ImeMode")));
			this.cbvisitor.Location = ((System.Drawing.Point)(resources.GetObject("cbvisitor.Location")));
			this.cbvisitor.Name = "cbvisitor";
			this.cbvisitor.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbvisitor.RightToLeft")));
			this.cbvisitor.Size = ((System.Drawing.Size)(resources.GetObject("cbvisitor.Size")));
			this.cbvisitor.TabIndex = ((int)(resources.GetObject("cbvisitor.TabIndex")));
			this.cbvisitor.Text = resources.GetString("cbvisitor.Text");
			this.cbvisitor.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbvisitor.TextAlign")));
			this.cbvisitor.Visible = ((bool)(resources.GetObject("cbvisitor.Visible")));
			this.cbvisitor.CheckedChanged += new System.EventHandler(this.UpdateFlagsValue);
			// 
			// cmcopy
			// 
			this.cmcopy.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cmcopy.RightToLeft")));
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
			// TtabForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.ttabPanel);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "TtabForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ttabPanel.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.gbttab.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	

		private void AutoChangeInteraction(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			internalchg = true;
			try 
			{
				if (lbttab.SelectedIndex>=0) TtabItemChange(null, null);
			} 
			finally 
			{
				internalchg = false;
			}
		}

		private void TtabItemDelete(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if (lbttab.SelectedIndex>=0) 
			{
				lbttab.Items.Remove(lbttab.Items[lbttab.SelectedIndex]);
				wrapper.Changed = true;
			}
		}

		private void AutoChangeMotive(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			internalchg = true;
			try 
			{
				if (lblist.SelectedIndex>=0) ListChangeClick(null, null);
			} 
			finally 
			{
				internalchg = false;
			}
		}

		private void GetTTABGuard(object sender, System.EventArgs e)
		{
			try 
			{
				Ttab wrp = (Ttab)wrapper;
				Bhav bhav = new Bhav(wrp.Opcodes);
				bhav.Package = wrp.Package;
				bhav.FileDescriptor = wrp.FileDescriptor;
				
				ushort opcode = SimPe.Plugin.WrapperFactory.BhavWizardForm.Execute(bhav, this);

				tbttabguard.Text = "0x"+Helper.HexString(opcode);
			} 
			catch (Exception ex) 
			{
				
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		private void GetTTABAction(object sender, System.EventArgs e)
		{
			try 
			{
				Ttab wrp = (Ttab)wrapper;
				Bhav bhav = new Bhav(wrp.Opcodes);
				bhav.Package = wrp.Package;
				bhav.FileDescriptor = wrp.FileDescriptor;
				
				ushort opcode = SimPe.Plugin.WrapperFactory.BhavWizardForm.Execute(bhav, this);

				tbttabaction.Text = "0x"+Helper.HexString(opcode);
			} 
			catch (Exception ex) 
			{
				
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		private void TtabSelect(object sender, System.EventArgs e)
		{
			if (this.internalchg) return;

			cbgroups.Items.Clear();
			lblist.Items.Clear();
			this.llchangettab.Enabled = false;
			this.lldelttab.Enabled = false;
			if (lbttab.SelectedIndex <0) return;
			llchangettab.Enabled = true;
			lldelttab.Enabled = true;

			try 
			{
				internalchg = true;
				TtabItem item = (TtabItem)lbttab.Items[lbttab.SelectedIndex];
				this.tbttabguard.Text = "0x"+Helper.HexString(item.Guardian);				
				this.tbttabaction.Text = "0x"+Helper.HexString(item.Action);

				this.tbinst1.Text = "0x"+Helper.HexString(item.Flags.Value);
				this.tbinst2.Text = "0x"+Helper.HexString(item.Flags2);
				tbpie.Text = "0x"+Helper.HexString(item.StringIndex);

				tbres1.Text = "0x"+Helper.HexString(item.AttenuationCode);
				tbres2.Text = "0x"+Helper.HexString(item.AttenuationValue);
				tbres3.Text = "0x"+Helper.HexString(item.Autonomy);
				tbres4.Text = "0x"+Helper.HexString(item.Res5);
				tbres5.Text = "0x"+Helper.HexString(item.Res6);
				tbres6.Text = item.Res7.ToString("N9");
				tbres7.Text = "0x"+Helper.HexString(item.Res8);
				tbres8.Text = "0x"+Helper.HexString(item.Res9);

				
				for(int i=0; i<item.Groups.Length; i++)  cbgroups.Items.Add((TtabMotives)i);
				
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

		private void GroupListChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			this.lllistchange.Enabled = false;
			if (lblist.SelectedIndex <0) return;
			lllistchange.Enabled = true;

			try 
			{
				internalchg = true;
				TtabListItem item = (TtabListItem)lblist.Items[lblist.SelectedIndex];

				tbmin.Text = "0x"+Helper.HexString((ushort)item.Minimum);
				tbdelta.Text = "0x"+Helper.HexString((ushort)item.Delta);
				tblisttype.Text = "0x"+Helper.HexString((ushort)item.Type);
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
		
		private void ListSelected(object sender, System.EventArgs e)
		{
			if (cbgroups.SelectedIndex <0) return;
			if (lbttab.SelectedIndex <0) return;

			try 
			{
				TtabItem item = (TtabItem)lbttab.Items[lbttab.SelectedIndex];
				int nr = (int)cbgroups.Items[cbgroups.SelectedIndex];

				lblist.Items.Clear();
				foreach (TtabListItem li in item.Groups[nr]) 
				{
					lblist.Items.Add(li);
				}

				//while (lblist.Items.Count<0x10) lblist.Items.Add(new TtabListItem(-1, null));
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}


		}		

		private void ListChangeClick(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if (lblist.SelectedIndex <0) return;
			if (cbgroups.SelectedIndex <0) return;
			if (lbttab.SelectedIndex <0) return;

			try 
			{
				Ttab wrp = (Ttab)wrapper;
				TtabListItem item = (TtabListItem)lblist.Items[lblist.SelectedIndex];				

				item.Minimum = Convert.ToInt16(tbmin.Text, 16);
				item.Delta = Convert.ToInt16(tbdelta.Text, 16);
				item.Type = Convert.ToInt16(tblisttype.Text, 16);

				wrapper.Changed = true;
				//while (lblist.Items.Count<0x10) lblist.Items.Add(new TtabListItem(-1, null));
			} 
			catch (Exception) 
			{
				//Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		private void TtabItemChange(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			//if (lbttab.SelectedIndex <0) return;

			try 
			{
				Ttab wrp = (Ttab)wrapper;
				TtabItem item = null;
				if (lbttab.SelectedIndex>=0) item = (TtabItem)lbttab.Items[lbttab.SelectedIndex];
				else item = new TtabItem(wrp);

				item.Guardian = Convert.ToUInt16(this.tbttabguard.Text, 16);				
				item.Action = Convert.ToUInt16(this.tbttabaction.Text, 16);

				item.Flags.Value = Convert.ToUInt16(this.tbinst1.Text, 16);
				item.Flags2 = Convert.ToUInt16(this.tbinst2.Text, 16);
				item.StringIndex = Convert.ToUInt32(tbpie.Text, 16);

				item.AttenuationCode = Convert.ToUInt32(tbres1.Text, 16);
				item.AttenuationValue = Convert.ToUInt32(tbres2.Text, 16);
				item.Autonomy = Convert.ToUInt32(tbres3.Text, 16);
				item.Res5 = Convert.ToUInt32(tbres4.Text, 16);
				item.Res6 = Convert.ToUInt32(tbres5.Text, 16);
				item.Res7 = Convert.ToSingle(tbres6.Text);
				item.Res8 = Convert.ToUInt32(tbres7.Text, 16);
				item.Res9 = Convert.ToUInt16(tbres8.Text, 16);

				this.internalchg = true;
				if (lbttab.SelectedIndex>=0) 
				{
					lbttab.Items[lbttab.SelectedIndex] = item;
				} 
				else 
				{
					lbttab.Items.Add(item);
				}
				wrapper.Changed = true;
			} 
			catch (Exception) 
			{
				//Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			} 
			finally 
			{
				this.internalchg = false;
			}
		}
		
		private void Ttabcommit(object sender, System.EventArgs e)
		{
		
			try 
			{
				if (this.lbttab.SelectedIndex>=0) TtabItemChange(null, null);
				Ttab wrp = (Ttab)wrapper;

				TtabItem[] items = new TtabItem[lbttab.Items.Count];
				for(int i=0; i<items.Length; i++)
				{
					TtabItem item = (TtabItem)lbttab.Items[i];
					items[i] = item;
				}
				wrp.Items = items;
				wrp.SynchronizeUserData();
				
				MessageBox.Show(Localization.Manager.GetString("commited"));
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}
		}

		private void AddTtab(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			lbttab.SelectedIndex = -1;
			TtabItemChange(null, null);
			lbttab.SelectedIndex = lbttab.Items.Count -1;
		}

		private void ActionOrGuardianChanged(object sender, System.EventArgs e)
		{
			lbguard.Text = Localization.Manager.GetString("Unknown");
			lbaction.Text = Localization.Manager.GetString("Unknown");

			try 
			{
				Ttab wrp = (Ttab)wrapper;
				TtabItem item = new TtabItem(wrp);
				item.Action = Convert.ToUInt16(tbttabaction.Text, 16);
				item.Guardian = Convert.ToUInt16(tbttabguard.Text, 16);

				lbguard.Text = item.GuardianName;
				lbaction.Text = item.ActionName;
		
				this.AutoChangeInteraction(sender, e);
			} 
			catch (Exception) 
			{
				//Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			} 
		}
		
		private void FlagTextChanged(object sender, System.EventArgs e)
		{
			try 
			{
				TtabFlags val = new TtabFlags(Convert.ToUInt16(this.tbinst1.Text, 16));

				this.cbvisitor.Checked = val.ByVisitors;
				this.cbautofirst.Checked = val.AutoFirstSelect;
				this.cbconsecutive.Checked = val.AvailConsecutive;
				this.cbchildren.Checked = val.ByChildren;
				this.cbdemochild.Checked = val.ByDemoChild;
				this.cbelders.Checked = val.ByElders;
				this.cbteens.Checked = val.ByTeens;
				this.cbtodlers.Checked = val.ByToddlers;
				this.cbdebugmenu.Checked = val.DebugMenu;
				this.cbjoinable.Checked = val.Joinable;
				this.cbimmediately.Checked = val.RunImmediately;
				this.cbadults.Checked = val.ByAdults;
				this.cbunk1.Checked = val.Unknown1;
				this.cbunk2.Checked = val.Unknown2;
				this.cbunk3.Checked = val.Unknown3;
				this.cbunk4.Checked = val.Unknown4;

				this.AutoChangeInteraction(sender, e);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		private void UpdateFlagsValue(object sender, System.EventArgs e)
		{
			try 
			{
				TtabFlags val = new TtabFlags(Convert.ToUInt16(this.tbinst1.Text, 16));

				val.ByVisitors = this.cbvisitor.Checked;
				val.AutoFirstSelect = this.cbautofirst.Checked;
				val.AvailConsecutive = this.cbconsecutive.Checked;
				val.ByChildren = this.cbchildren.Checked;
				val.ByDemoChild = this.cbdemochild.Checked;
				val.ByElders = this.cbelders.Checked;
				val.ByTeens = this.cbteens.Checked;
				val.ByToddlers = this.cbtodlers.Checked;
				val.DebugMenu = this.cbdebugmenu.Checked;
				val.Joinable = this.cbjoinable.Checked;
				val.RunImmediately = this.cbimmediately.Checked;
				val.ByAdults = this.cbadults.Checked;
				val.Unknown1 = this.cbunk1.Checked;
				val.Unknown2 = this.cbunk2.Checked;
				val.Unknown3 = this.cbunk3.Checked;
				val.Unknown4 = this.cbunk4.Checked;

				tbinst1.Text = "0x"+Helper.HexString(val.Value);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		#endregion
	}
}
