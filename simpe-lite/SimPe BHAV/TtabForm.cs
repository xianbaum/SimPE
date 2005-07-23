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
using System.Data;
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

		private System.Windows.Forms.ListBox lbttab;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label lbttabfile;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.Panel ttabPanel;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tpSettings;
		private System.Windows.Forms.Label lbaction;
		private System.Windows.Forms.Label lbguard;
		private System.Windows.Forms.Label label40;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.TextBox tbGuardian;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.CheckBox cbunk3;
		private System.Windows.Forms.CheckBox cbunk4;
		private System.Windows.Forms.CheckBox cbunk1;
		private System.Windows.Forms.CheckBox cbunk2;
		private System.Windows.Forms.CheckBox cbteens;
		private System.Windows.Forms.CheckBox cbelders;
		private System.Windows.Forms.CheckBox cbtodlers;
		private System.Windows.Forms.CheckBox cbautofirst;
		private System.Windows.Forms.CheckBox cbdebugmenu;
		private System.Windows.Forms.CheckBox cbadults;
		private System.Windows.Forms.CheckBox cbdemochild;
		private System.Windows.Forms.CheckBox cbchildren;
		private System.Windows.Forms.CheckBox cbconsecutive;
		private System.Windows.Forms.CheckBox cbimmediately;
		private System.Windows.Forms.CheckBox cbjoinable;
		private System.Windows.Forms.TabPage tpMotives;
		private System.Windows.Forms.CheckBox cbvisitor;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.Label label41;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.TextBox tbAction;
		private System.Windows.Forms.TextBox tbFlags2;
		private System.Windows.Forms.TextBox tbStringIndex;
		private System.Windows.Forms.TextBox tbFormat;
		private System.Windows.Forms.GroupBox gbFlags;
		private System.Windows.Forms.TextBox tbFlags;
		private System.Windows.Forms.TextBox tbAttenuationCode;
		private System.Windows.Forms.TextBox tbAttenuationValue;
		private System.Windows.Forms.TextBox tbAutonomy;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbRes9;
		private System.Windows.Forms.TextBox tbJoinIndex;
		private System.Windows.Forms.TextBox tbRes8;
		private System.Windows.Forms.TextBox tbRes7;
		private System.Windows.Forms.TextBox tbRes6;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbRes5;
		private System.Windows.Forms.Button btnGuardian;
		private System.Windows.Forms.Button btnAction;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnDelete;
		private SimPe.PackedFiles.UserInterface.TtabItemMotiveTableUI ttabItemMotiveTableUI1;
		private System.Windows.Forms.Label lbFilename;
		private System.Windows.Forms.TextBox tbFilename;
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
			TextBox[] tbua = {tbAction, tbGuardian, tbFlags, tbFlags2, tbRes9};
			alUshorts = new ArrayList(tbua);

			TextBox[] tbia = {tbFormat, tbStringIndex, tbAttenuationCode, tbAttenuationValue,
							  tbAutonomy, tbRes5, tbRes6, tbRes8, tbJoinIndex};
			alUints = new ArrayList(tbia);

			TextBox[] tbfa = {tbRes7};
			alFloats = new ArrayList(tbfa);

			CheckBox[] cba = {
							    cbvisitor   ,cbjoinable  ,cbimmediately ,cbconsecutive
							   ,cbchildren  ,cbdemochild ,cbadults      ,cbdebugmenu
							   ,cbautofirst ,cbtodlers   ,cbelders      ,cbteens
							   ,cbunk1      ,cbunk2      ,cbunk3        ,cbunk4
						   };
			alFlags = new ArrayList(cba);
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

		
		#region TtabForm
		/// <summary>
		/// Stores the currently active Wrapper
		/// </summary>
		private Ttab wrapper = null;
		private bool internalchg;
		private bool setHandler = false;
		private ArrayList alUshorts;
		private ArrayList alUints;
		private ArrayList alFloats;
		private ArrayList alFlags;

		private void WrapperChanged(object sender, System.EventArgs e)
		{
			this.btnCommit.Enabled = wrapper.Changed;

			if (internalchg) return;
			internalchg = true;
			tbFilename.Text = lbttabfile.Text = wrapper.FileName;
			tbFormat.Text = "0x"+Helper.HexString(wrapper.Format);
			internalchg = false;
		}


		private void doFlags()
		{
			internalchg = true;
			bool val;
			for (int i = 0; i < alFlags.Count; i++)
			{
				switch(i)
				{
					case  0: val = wrapper[this.lbttab.SelectedIndex].Flags.ByVisitors; break;
					case  1: val = wrapper[this.lbttab.SelectedIndex].Flags.Joinable; break;
					case  2: val = wrapper[this.lbttab.SelectedIndex].Flags.RunImmediately; break;
					case  3: val = wrapper[this.lbttab.SelectedIndex].Flags.AvailConsecutive; break;
					case  4: val = wrapper[this.lbttab.SelectedIndex].Flags.ByChildren; break;
					case  5: val = wrapper[this.lbttab.SelectedIndex].Flags.ByDemoChild; break;
					case  6: val = wrapper[this.lbttab.SelectedIndex].Flags.ByAdults; break;
					case  7: val = wrapper[this.lbttab.SelectedIndex].Flags.DebugMenu; break;
					case  8: val = wrapper[this.lbttab.SelectedIndex].Flags.AutoFirstSelect; break;
					case  9: val = wrapper[this.lbttab.SelectedIndex].Flags.ByToddlers; break;
					case 10: val = wrapper[this.lbttab.SelectedIndex].Flags.ByElders; break;
					case 11: val = wrapper[this.lbttab.SelectedIndex].Flags.ByTeens; break;
					case 12: val = wrapper[this.lbttab.SelectedIndex].Flags.Unknown1; break;
					case 13: val = wrapper[this.lbttab.SelectedIndex].Flags.Unknown2; break;
					case 14: val = wrapper[this.lbttab.SelectedIndex].Flags.Unknown3; break;
					case 15: val = wrapper[this.lbttab.SelectedIndex].Flags.Unknown4; break;
					default: val = false; break;
				}
				((CheckBox)alFlags[i]).Checked = val;
			}
			internalchg = false;
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
			wrapper = (Ttab) wrp;
			WrapperChanged(wrapper, null);

			internalchg = true;
			lbttab.Items.Clear();
			for(int i = 0; i < wrapper.ItemCount; i++)
				lbttab.Items.Add(wrapper[i]);
			internalchg = false;

			if (lbttab.Items.Count>0) lbttab.SelectedIndex = 0;

			if (!setHandler)
			{
				wrapper.WrapperChanged += new System.EventHandler(this.WrapperChanged);
				setHandler = true;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TtabForm));
			this.ttabPanel = new System.Windows.Forms.Panel();
			this.lbFilename = new System.Windows.Forms.Label();
			this.tbFilename = new System.Windows.Forms.TextBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.tbFormat = new System.Windows.Forms.TextBox();
			this.label41 = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpSettings = new System.Windows.Forms.TabPage();
			this.btnAction = new System.Windows.Forms.Button();
			this.btnGuardian = new System.Windows.Forms.Button();
			this.lbaction = new System.Windows.Forms.Label();
			this.lbguard = new System.Windows.Forms.Label();
			this.tbStringIndex = new System.Windows.Forms.TextBox();
			this.label40 = new System.Windows.Forms.Label();
			this.tbRes9 = new System.Windows.Forms.TextBox();
			this.label33 = new System.Windows.Forms.Label();
			this.tbRes8 = new System.Windows.Forms.TextBox();
			this.label34 = new System.Windows.Forms.Label();
			this.tbRes5 = new System.Windows.Forms.TextBox();
			this.label35 = new System.Windows.Forms.Label();
			this.tbAutonomy = new System.Windows.Forms.TextBox();
			this.tbRes7 = new System.Windows.Forms.TextBox();
			this.label29 = new System.Windows.Forms.Label();
			this.tbRes6 = new System.Windows.Forms.TextBox();
			this.label30 = new System.Windows.Forms.Label();
			this.tbAttenuationValue = new System.Windows.Forms.TextBox();
			this.label31 = new System.Windows.Forms.Label();
			this.tbAttenuationCode = new System.Windows.Forms.TextBox();
			this.label32 = new System.Windows.Forms.Label();
			this.tbFlags2 = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.tbGuardian = new System.Windows.Forms.TextBox();
			this.label23 = new System.Windows.Forms.Label();
			this.gbFlags = new System.Windows.Forms.GroupBox();
			this.tbFlags = new System.Windows.Forms.TextBox();
			this.label24 = new System.Windows.Forms.Label();
			this.cbvisitor = new System.Windows.Forms.CheckBox();
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
			this.tbAction = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tbJoinIndex = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tpMotives = new System.Windows.Forms.TabPage();
			this.ttabItemMotiveTableUI1 = new SimPe.PackedFiles.UserInterface.TtabItemMotiveTableUI();
			this.lbttab = new System.Windows.Forms.ListBox();
			this.btnCommit = new System.Windows.Forms.Button();
			this.panel5 = new System.Windows.Forms.Panel();
			this.lbttabfile = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.btnDelete = new System.Windows.Forms.Button();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.ttabPanel.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tpSettings.SuspendLayout();
			this.gbFlags.SuspendLayout();
			this.tpMotives.SuspendLayout();
			this.panel5.SuspendLayout();
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
			this.ttabPanel.BackColor = System.Drawing.SystemColors.Control;
			this.ttabPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabPanel.BackgroundImage")));
			this.ttabPanel.Controls.Add(this.lbFilename);
			this.ttabPanel.Controls.Add(this.tbFilename);
			this.ttabPanel.Controls.Add(this.btnAdd);
			this.ttabPanel.Controls.Add(this.tbFormat);
			this.ttabPanel.Controls.Add(this.label41);
			this.ttabPanel.Controls.Add(this.tabControl1);
			this.ttabPanel.Controls.Add(this.lbttab);
			this.ttabPanel.Controls.Add(this.btnCommit);
			this.ttabPanel.Controls.Add(this.panel5);
			this.ttabPanel.Controls.Add(this.label26);
			this.ttabPanel.Controls.Add(this.btnDelete);
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
			this.tbFilename.TextChanged += new System.EventHandler(this.tbFilename_TextChanged);
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
			this.tbFormat.Validating += new System.ComponentModel.CancelEventHandler(this.uintHex_Validating);
			this.tbFormat.Validated += new System.EventHandler(this.uintHex_Validated);
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
			// tabControl1
			// 
			this.tabControl1.AccessibleDescription = resources.GetString("tabControl1.AccessibleDescription");
			this.tabControl1.AccessibleName = resources.GetString("tabControl1.AccessibleName");
			this.tabControl1.Alignment = ((System.Windows.Forms.TabAlignment)(resources.GetObject("tabControl1.Alignment")));
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabControl1.Anchor")));
			this.tabControl1.Appearance = ((System.Windows.Forms.TabAppearance)(resources.GetObject("tabControl1.Appearance")));
			this.tabControl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabControl1.BackgroundImage")));
			this.tabControl1.Controls.Add(this.tpSettings);
			this.tabControl1.Controls.Add(this.tpMotives);
			this.tabControl1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabControl1.Dock")));
			this.tabControl1.Enabled = ((bool)(resources.GetObject("tabControl1.Enabled")));
			this.tabControl1.Font = ((System.Drawing.Font)(resources.GetObject("tabControl1.Font")));
			this.tabControl1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tabControl1.ImeMode")));
			this.tabControl1.ItemSize = ((System.Drawing.Size)(resources.GetObject("tabControl1.ItemSize")));
			this.tabControl1.Location = ((System.Drawing.Point)(resources.GetObject("tabControl1.Location")));
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.Padding = ((System.Drawing.Point)(resources.GetObject("tabControl1.Padding")));
			this.tabControl1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tabControl1.RightToLeft")));
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.ShowToolTips = ((bool)(resources.GetObject("tabControl1.ShowToolTips")));
			this.tabControl1.Size = ((System.Drawing.Size)(resources.GetObject("tabControl1.Size")));
			this.tabControl1.TabIndex = ((int)(resources.GetObject("tabControl1.TabIndex")));
			this.tabControl1.Text = resources.GetString("tabControl1.Text");
			this.tabControl1.Visible = ((bool)(resources.GetObject("tabControl1.Visible")));
			// 
			// tpSettings
			// 
			this.tpSettings.AccessibleDescription = resources.GetString("tpSettings.AccessibleDescription");
			this.tpSettings.AccessibleName = resources.GetString("tpSettings.AccessibleName");
			this.tpSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tpSettings.Anchor")));
			this.tpSettings.AutoScroll = ((bool)(resources.GetObject("tpSettings.AutoScroll")));
			this.tpSettings.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tpSettings.AutoScrollMargin")));
			this.tpSettings.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tpSettings.AutoScrollMinSize")));
			this.tpSettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tpSettings.BackgroundImage")));
			this.tpSettings.Controls.Add(this.btnAction);
			this.tpSettings.Controls.Add(this.btnGuardian);
			this.tpSettings.Controls.Add(this.lbaction);
			this.tpSettings.Controls.Add(this.lbguard);
			this.tpSettings.Controls.Add(this.tbStringIndex);
			this.tpSettings.Controls.Add(this.label40);
			this.tpSettings.Controls.Add(this.tbRes9);
			this.tpSettings.Controls.Add(this.label33);
			this.tpSettings.Controls.Add(this.tbRes8);
			this.tpSettings.Controls.Add(this.label34);
			this.tpSettings.Controls.Add(this.tbRes5);
			this.tpSettings.Controls.Add(this.label35);
			this.tpSettings.Controls.Add(this.tbAutonomy);
			this.tpSettings.Controls.Add(this.tbRes7);
			this.tpSettings.Controls.Add(this.label29);
			this.tpSettings.Controls.Add(this.tbRes6);
			this.tpSettings.Controls.Add(this.label30);
			this.tpSettings.Controls.Add(this.tbAttenuationValue);
			this.tpSettings.Controls.Add(this.label31);
			this.tpSettings.Controls.Add(this.tbAttenuationCode);
			this.tpSettings.Controls.Add(this.label32);
			this.tpSettings.Controls.Add(this.tbFlags2);
			this.tpSettings.Controls.Add(this.label20);
			this.tpSettings.Controls.Add(this.label21);
			this.tpSettings.Controls.Add(this.tbGuardian);
			this.tpSettings.Controls.Add(this.label23);
			this.tpSettings.Controls.Add(this.gbFlags);
			this.tpSettings.Controls.Add(this.tbAction);
			this.tpSettings.Controls.Add(this.label1);
			this.tpSettings.Controls.Add(this.tbJoinIndex);
			this.tpSettings.Controls.Add(this.label2);
			this.tpSettings.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tpSettings.Dock")));
			this.tpSettings.Enabled = ((bool)(resources.GetObject("tpSettings.Enabled")));
			this.tpSettings.Font = ((System.Drawing.Font)(resources.GetObject("tpSettings.Font")));
			this.tpSettings.ImageIndex = ((int)(resources.GetObject("tpSettings.ImageIndex")));
			this.tpSettings.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tpSettings.ImeMode")));
			this.tpSettings.Location = ((System.Drawing.Point)(resources.GetObject("tpSettings.Location")));
			this.tpSettings.Name = "tpSettings";
			this.tpSettings.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tpSettings.RightToLeft")));
			this.tpSettings.Size = ((System.Drawing.Size)(resources.GetObject("tpSettings.Size")));
			this.tpSettings.TabIndex = ((int)(resources.GetObject("tpSettings.TabIndex")));
			this.tpSettings.Text = resources.GetString("tpSettings.Text");
			this.tpSettings.ToolTipText = resources.GetString("tpSettings.ToolTipText");
			this.tpSettings.Visible = ((bool)(resources.GetObject("tpSettings.Visible")));
			// 
			// btnAction
			// 
			this.btnAction.AccessibleDescription = resources.GetString("btnAction.AccessibleDescription");
			this.btnAction.AccessibleName = resources.GetString("btnAction.AccessibleName");
			this.btnAction.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAction.Anchor")));
			this.btnAction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAction.BackgroundImage")));
			this.btnAction.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAction.Dock")));
			this.btnAction.Enabled = ((bool)(resources.GetObject("btnAction.Enabled")));
			this.btnAction.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAction.FlatStyle")));
			this.btnAction.Font = ((System.Drawing.Font)(resources.GetObject("btnAction.Font")));
			this.btnAction.Image = ((System.Drawing.Image)(resources.GetObject("btnAction.Image")));
			this.btnAction.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAction.ImageAlign")));
			this.btnAction.ImageIndex = ((int)(resources.GetObject("btnAction.ImageIndex")));
			this.btnAction.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAction.ImeMode")));
			this.btnAction.Location = ((System.Drawing.Point)(resources.GetObject("btnAction.Location")));
			this.btnAction.Name = "btnAction";
			this.btnAction.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAction.RightToLeft")));
			this.btnAction.Size = ((System.Drawing.Size)(resources.GetObject("btnAction.Size")));
			this.btnAction.TabIndex = ((int)(resources.GetObject("btnAction.TabIndex")));
			this.btnAction.Text = resources.GetString("btnAction.Text");
			this.btnAction.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAction.TextAlign")));
			this.btnAction.Visible = ((bool)(resources.GetObject("btnAction.Visible")));
			this.btnAction.Click += new System.EventHandler(this.GetTTABAction);
			// 
			// btnGuardian
			// 
			this.btnGuardian.AccessibleDescription = resources.GetString("btnGuardian.AccessibleDescription");
			this.btnGuardian.AccessibleName = resources.GetString("btnGuardian.AccessibleName");
			this.btnGuardian.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnGuardian.Anchor")));
			this.btnGuardian.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGuardian.BackgroundImage")));
			this.btnGuardian.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnGuardian.Dock")));
			this.btnGuardian.Enabled = ((bool)(resources.GetObject("btnGuardian.Enabled")));
			this.btnGuardian.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnGuardian.FlatStyle")));
			this.btnGuardian.Font = ((System.Drawing.Font)(resources.GetObject("btnGuardian.Font")));
			this.btnGuardian.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardian.Image")));
			this.btnGuardian.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnGuardian.ImageAlign")));
			this.btnGuardian.ImageIndex = ((int)(resources.GetObject("btnGuardian.ImageIndex")));
			this.btnGuardian.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnGuardian.ImeMode")));
			this.btnGuardian.Location = ((System.Drawing.Point)(resources.GetObject("btnGuardian.Location")));
			this.btnGuardian.Name = "btnGuardian";
			this.btnGuardian.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnGuardian.RightToLeft")));
			this.btnGuardian.Size = ((System.Drawing.Size)(resources.GetObject("btnGuardian.Size")));
			this.btnGuardian.TabIndex = ((int)(resources.GetObject("btnGuardian.TabIndex")));
			this.btnGuardian.Text = resources.GetString("btnGuardian.Text");
			this.btnGuardian.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnGuardian.TextAlign")));
			this.btnGuardian.Visible = ((bool)(resources.GetObject("btnGuardian.Visible")));
			this.btnGuardian.Click += new System.EventHandler(this.GetTTABGuard);
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
			// tbStringIndex
			// 
			this.tbStringIndex.AccessibleDescription = resources.GetString("tbStringIndex.AccessibleDescription");
			this.tbStringIndex.AccessibleName = resources.GetString("tbStringIndex.AccessibleName");
			this.tbStringIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbStringIndex.Anchor")));
			this.tbStringIndex.AutoSize = ((bool)(resources.GetObject("tbStringIndex.AutoSize")));
			this.tbStringIndex.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbStringIndex.BackgroundImage")));
			this.tbStringIndex.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbStringIndex.Dock")));
			this.tbStringIndex.Enabled = ((bool)(resources.GetObject("tbStringIndex.Enabled")));
			this.tbStringIndex.Font = ((System.Drawing.Font)(resources.GetObject("tbStringIndex.Font")));
			this.tbStringIndex.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbStringIndex.ImeMode")));
			this.tbStringIndex.Location = ((System.Drawing.Point)(resources.GetObject("tbStringIndex.Location")));
			this.tbStringIndex.MaxLength = ((int)(resources.GetObject("tbStringIndex.MaxLength")));
			this.tbStringIndex.Multiline = ((bool)(resources.GetObject("tbStringIndex.Multiline")));
			this.tbStringIndex.Name = "tbStringIndex";
			this.tbStringIndex.PasswordChar = ((char)(resources.GetObject("tbStringIndex.PasswordChar")));
			this.tbStringIndex.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbStringIndex.RightToLeft")));
			this.tbStringIndex.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbStringIndex.ScrollBars")));
			this.tbStringIndex.Size = ((System.Drawing.Size)(resources.GetObject("tbStringIndex.Size")));
			this.tbStringIndex.TabIndex = ((int)(resources.GetObject("tbStringIndex.TabIndex")));
			this.tbStringIndex.Text = resources.GetString("tbStringIndex.Text");
			this.tbStringIndex.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbStringIndex.TextAlign")));
			this.tbStringIndex.Visible = ((bool)(resources.GetObject("tbStringIndex.Visible")));
			this.tbStringIndex.WordWrap = ((bool)(resources.GetObject("tbStringIndex.WordWrap")));
			this.tbStringIndex.Validating += new System.ComponentModel.CancelEventHandler(this.uintHex_Validating);
			this.tbStringIndex.Validated += new System.EventHandler(this.uintHex_Validated);
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
			// tbRes9
			// 
			this.tbRes9.AccessibleDescription = resources.GetString("tbRes9.AccessibleDescription");
			this.tbRes9.AccessibleName = resources.GetString("tbRes9.AccessibleName");
			this.tbRes9.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbRes9.Anchor")));
			this.tbRes9.AutoSize = ((bool)(resources.GetObject("tbRes9.AutoSize")));
			this.tbRes9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbRes9.BackgroundImage")));
			this.tbRes9.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbRes9.Dock")));
			this.tbRes9.Enabled = ((bool)(resources.GetObject("tbRes9.Enabled")));
			this.tbRes9.Font = ((System.Drawing.Font)(resources.GetObject("tbRes9.Font")));
			this.tbRes9.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbRes9.ImeMode")));
			this.tbRes9.Location = ((System.Drawing.Point)(resources.GetObject("tbRes9.Location")));
			this.tbRes9.MaxLength = ((int)(resources.GetObject("tbRes9.MaxLength")));
			this.tbRes9.Multiline = ((bool)(resources.GetObject("tbRes9.Multiline")));
			this.tbRes9.Name = "tbRes9";
			this.tbRes9.PasswordChar = ((char)(resources.GetObject("tbRes9.PasswordChar")));
			this.tbRes9.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbRes9.RightToLeft")));
			this.tbRes9.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbRes9.ScrollBars")));
			this.tbRes9.Size = ((System.Drawing.Size)(resources.GetObject("tbRes9.Size")));
			this.tbRes9.TabIndex = ((int)(resources.GetObject("tbRes9.TabIndex")));
			this.tbRes9.Text = resources.GetString("tbRes9.Text");
			this.tbRes9.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbRes9.TextAlign")));
			this.tbRes9.Visible = ((bool)(resources.GetObject("tbRes9.Visible")));
			this.tbRes9.WordWrap = ((bool)(resources.GetObject("tbRes9.WordWrap")));
			this.tbRes9.Validating += new System.ComponentModel.CancelEventHandler(this.ushortHex_Validating);
			this.tbRes9.Validated += new System.EventHandler(this.ushortHex_Validated);
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
			// tbRes8
			// 
			this.tbRes8.AccessibleDescription = resources.GetString("tbRes8.AccessibleDescription");
			this.tbRes8.AccessibleName = resources.GetString("tbRes8.AccessibleName");
			this.tbRes8.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbRes8.Anchor")));
			this.tbRes8.AutoSize = ((bool)(resources.GetObject("tbRes8.AutoSize")));
			this.tbRes8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbRes8.BackgroundImage")));
			this.tbRes8.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbRes8.Dock")));
			this.tbRes8.Enabled = ((bool)(resources.GetObject("tbRes8.Enabled")));
			this.tbRes8.Font = ((System.Drawing.Font)(resources.GetObject("tbRes8.Font")));
			this.tbRes8.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbRes8.ImeMode")));
			this.tbRes8.Location = ((System.Drawing.Point)(resources.GetObject("tbRes8.Location")));
			this.tbRes8.MaxLength = ((int)(resources.GetObject("tbRes8.MaxLength")));
			this.tbRes8.Multiline = ((bool)(resources.GetObject("tbRes8.Multiline")));
			this.tbRes8.Name = "tbRes8";
			this.tbRes8.PasswordChar = ((char)(resources.GetObject("tbRes8.PasswordChar")));
			this.tbRes8.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbRes8.RightToLeft")));
			this.tbRes8.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbRes8.ScrollBars")));
			this.tbRes8.Size = ((System.Drawing.Size)(resources.GetObject("tbRes8.Size")));
			this.tbRes8.TabIndex = ((int)(resources.GetObject("tbRes8.TabIndex")));
			this.tbRes8.Text = resources.GetString("tbRes8.Text");
			this.tbRes8.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbRes8.TextAlign")));
			this.tbRes8.Visible = ((bool)(resources.GetObject("tbRes8.Visible")));
			this.tbRes8.WordWrap = ((bool)(resources.GetObject("tbRes8.WordWrap")));
			this.tbRes8.Validating += new System.ComponentModel.CancelEventHandler(this.uintHex_Validating);
			this.tbRes8.Validated += new System.EventHandler(this.uintHex_Validated);
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
			// tbRes5
			// 
			this.tbRes5.AccessibleDescription = resources.GetString("tbRes5.AccessibleDescription");
			this.tbRes5.AccessibleName = resources.GetString("tbRes5.AccessibleName");
			this.tbRes5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbRes5.Anchor")));
			this.tbRes5.AutoSize = ((bool)(resources.GetObject("tbRes5.AutoSize")));
			this.tbRes5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbRes5.BackgroundImage")));
			this.tbRes5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbRes5.Dock")));
			this.tbRes5.Enabled = ((bool)(resources.GetObject("tbRes5.Enabled")));
			this.tbRes5.Font = ((System.Drawing.Font)(resources.GetObject("tbRes5.Font")));
			this.tbRes5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbRes5.ImeMode")));
			this.tbRes5.Location = ((System.Drawing.Point)(resources.GetObject("tbRes5.Location")));
			this.tbRes5.MaxLength = ((int)(resources.GetObject("tbRes5.MaxLength")));
			this.tbRes5.Multiline = ((bool)(resources.GetObject("tbRes5.Multiline")));
			this.tbRes5.Name = "tbRes5";
			this.tbRes5.PasswordChar = ((char)(resources.GetObject("tbRes5.PasswordChar")));
			this.tbRes5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbRes5.RightToLeft")));
			this.tbRes5.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbRes5.ScrollBars")));
			this.tbRes5.Size = ((System.Drawing.Size)(resources.GetObject("tbRes5.Size")));
			this.tbRes5.TabIndex = ((int)(resources.GetObject("tbRes5.TabIndex")));
			this.tbRes5.Text = resources.GetString("tbRes5.Text");
			this.tbRes5.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbRes5.TextAlign")));
			this.tbRes5.Visible = ((bool)(resources.GetObject("tbRes5.Visible")));
			this.tbRes5.WordWrap = ((bool)(resources.GetObject("tbRes5.WordWrap")));
			this.tbRes5.Validating += new System.ComponentModel.CancelEventHandler(this.uintHex_Validating);
			this.tbRes5.Validated += new System.EventHandler(this.uintHex_Validated);
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
			// tbAutonomy
			// 
			this.tbAutonomy.AccessibleDescription = resources.GetString("tbAutonomy.AccessibleDescription");
			this.tbAutonomy.AccessibleName = resources.GetString("tbAutonomy.AccessibleName");
			this.tbAutonomy.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbAutonomy.Anchor")));
			this.tbAutonomy.AutoSize = ((bool)(resources.GetObject("tbAutonomy.AutoSize")));
			this.tbAutonomy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbAutonomy.BackgroundImage")));
			this.tbAutonomy.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbAutonomy.Dock")));
			this.tbAutonomy.Enabled = ((bool)(resources.GetObject("tbAutonomy.Enabled")));
			this.tbAutonomy.Font = ((System.Drawing.Font)(resources.GetObject("tbAutonomy.Font")));
			this.tbAutonomy.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbAutonomy.ImeMode")));
			this.tbAutonomy.Location = ((System.Drawing.Point)(resources.GetObject("tbAutonomy.Location")));
			this.tbAutonomy.MaxLength = ((int)(resources.GetObject("tbAutonomy.MaxLength")));
			this.tbAutonomy.Multiline = ((bool)(resources.GetObject("tbAutonomy.Multiline")));
			this.tbAutonomy.Name = "tbAutonomy";
			this.tbAutonomy.PasswordChar = ((char)(resources.GetObject("tbAutonomy.PasswordChar")));
			this.tbAutonomy.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbAutonomy.RightToLeft")));
			this.tbAutonomy.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbAutonomy.ScrollBars")));
			this.tbAutonomy.Size = ((System.Drawing.Size)(resources.GetObject("tbAutonomy.Size")));
			this.tbAutonomy.TabIndex = ((int)(resources.GetObject("tbAutonomy.TabIndex")));
			this.tbAutonomy.Text = resources.GetString("tbAutonomy.Text");
			this.tbAutonomy.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbAutonomy.TextAlign")));
			this.tbAutonomy.Visible = ((bool)(resources.GetObject("tbAutonomy.Visible")));
			this.tbAutonomy.WordWrap = ((bool)(resources.GetObject("tbAutonomy.WordWrap")));
			this.tbAutonomy.Validating += new System.ComponentModel.CancelEventHandler(this.uintHex_Validating);
			this.tbAutonomy.Validated += new System.EventHandler(this.uintHex_Validated);
			// 
			// tbRes7
			// 
			this.tbRes7.AccessibleDescription = resources.GetString("tbRes7.AccessibleDescription");
			this.tbRes7.AccessibleName = resources.GetString("tbRes7.AccessibleName");
			this.tbRes7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbRes7.Anchor")));
			this.tbRes7.AutoSize = ((bool)(resources.GetObject("tbRes7.AutoSize")));
			this.tbRes7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbRes7.BackgroundImage")));
			this.tbRes7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbRes7.Dock")));
			this.tbRes7.Enabled = ((bool)(resources.GetObject("tbRes7.Enabled")));
			this.tbRes7.Font = ((System.Drawing.Font)(resources.GetObject("tbRes7.Font")));
			this.tbRes7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbRes7.ImeMode")));
			this.tbRes7.Location = ((System.Drawing.Point)(resources.GetObject("tbRes7.Location")));
			this.tbRes7.MaxLength = ((int)(resources.GetObject("tbRes7.MaxLength")));
			this.tbRes7.Multiline = ((bool)(resources.GetObject("tbRes7.Multiline")));
			this.tbRes7.Name = "tbRes7";
			this.tbRes7.PasswordChar = ((char)(resources.GetObject("tbRes7.PasswordChar")));
			this.tbRes7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbRes7.RightToLeft")));
			this.tbRes7.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbRes7.ScrollBars")));
			this.tbRes7.Size = ((System.Drawing.Size)(resources.GetObject("tbRes7.Size")));
			this.tbRes7.TabIndex = ((int)(resources.GetObject("tbRes7.TabIndex")));
			this.tbRes7.Text = resources.GetString("tbRes7.Text");
			this.tbRes7.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbRes7.TextAlign")));
			this.tbRes7.Visible = ((bool)(resources.GetObject("tbRes7.Visible")));
			this.tbRes7.WordWrap = ((bool)(resources.GetObject("tbRes7.WordWrap")));
			this.tbRes7.Validating += new System.ComponentModel.CancelEventHandler(this.float_Validating);
			this.tbRes7.Validated += new System.EventHandler(this.float_Validated);
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
			// tbRes6
			// 
			this.tbRes6.AccessibleDescription = resources.GetString("tbRes6.AccessibleDescription");
			this.tbRes6.AccessibleName = resources.GetString("tbRes6.AccessibleName");
			this.tbRes6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbRes6.Anchor")));
			this.tbRes6.AutoSize = ((bool)(resources.GetObject("tbRes6.AutoSize")));
			this.tbRes6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbRes6.BackgroundImage")));
			this.tbRes6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbRes6.Dock")));
			this.tbRes6.Enabled = ((bool)(resources.GetObject("tbRes6.Enabled")));
			this.tbRes6.Font = ((System.Drawing.Font)(resources.GetObject("tbRes6.Font")));
			this.tbRes6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbRes6.ImeMode")));
			this.tbRes6.Location = ((System.Drawing.Point)(resources.GetObject("tbRes6.Location")));
			this.tbRes6.MaxLength = ((int)(resources.GetObject("tbRes6.MaxLength")));
			this.tbRes6.Multiline = ((bool)(resources.GetObject("tbRes6.Multiline")));
			this.tbRes6.Name = "tbRes6";
			this.tbRes6.PasswordChar = ((char)(resources.GetObject("tbRes6.PasswordChar")));
			this.tbRes6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbRes6.RightToLeft")));
			this.tbRes6.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbRes6.ScrollBars")));
			this.tbRes6.Size = ((System.Drawing.Size)(resources.GetObject("tbRes6.Size")));
			this.tbRes6.TabIndex = ((int)(resources.GetObject("tbRes6.TabIndex")));
			this.tbRes6.Text = resources.GetString("tbRes6.Text");
			this.tbRes6.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbRes6.TextAlign")));
			this.tbRes6.Visible = ((bool)(resources.GetObject("tbRes6.Visible")));
			this.tbRes6.WordWrap = ((bool)(resources.GetObject("tbRes6.WordWrap")));
			this.tbRes6.Validating += new System.ComponentModel.CancelEventHandler(this.uintHex_Validating);
			this.tbRes6.Validated += new System.EventHandler(this.uintHex_Validated);
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
			// tbAttenuationValue
			// 
			this.tbAttenuationValue.AccessibleDescription = resources.GetString("tbAttenuationValue.AccessibleDescription");
			this.tbAttenuationValue.AccessibleName = resources.GetString("tbAttenuationValue.AccessibleName");
			this.tbAttenuationValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbAttenuationValue.Anchor")));
			this.tbAttenuationValue.AutoSize = ((bool)(resources.GetObject("tbAttenuationValue.AutoSize")));
			this.tbAttenuationValue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbAttenuationValue.BackgroundImage")));
			this.tbAttenuationValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbAttenuationValue.Dock")));
			this.tbAttenuationValue.Enabled = ((bool)(resources.GetObject("tbAttenuationValue.Enabled")));
			this.tbAttenuationValue.Font = ((System.Drawing.Font)(resources.GetObject("tbAttenuationValue.Font")));
			this.tbAttenuationValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbAttenuationValue.ImeMode")));
			this.tbAttenuationValue.Location = ((System.Drawing.Point)(resources.GetObject("tbAttenuationValue.Location")));
			this.tbAttenuationValue.MaxLength = ((int)(resources.GetObject("tbAttenuationValue.MaxLength")));
			this.tbAttenuationValue.Multiline = ((bool)(resources.GetObject("tbAttenuationValue.Multiline")));
			this.tbAttenuationValue.Name = "tbAttenuationValue";
			this.tbAttenuationValue.PasswordChar = ((char)(resources.GetObject("tbAttenuationValue.PasswordChar")));
			this.tbAttenuationValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbAttenuationValue.RightToLeft")));
			this.tbAttenuationValue.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbAttenuationValue.ScrollBars")));
			this.tbAttenuationValue.Size = ((System.Drawing.Size)(resources.GetObject("tbAttenuationValue.Size")));
			this.tbAttenuationValue.TabIndex = ((int)(resources.GetObject("tbAttenuationValue.TabIndex")));
			this.tbAttenuationValue.Text = resources.GetString("tbAttenuationValue.Text");
			this.tbAttenuationValue.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbAttenuationValue.TextAlign")));
			this.tbAttenuationValue.Visible = ((bool)(resources.GetObject("tbAttenuationValue.Visible")));
			this.tbAttenuationValue.WordWrap = ((bool)(resources.GetObject("tbAttenuationValue.WordWrap")));
			this.tbAttenuationValue.Validating += new System.ComponentModel.CancelEventHandler(this.uintHex_Validating);
			this.tbAttenuationValue.Validated += new System.EventHandler(this.uintHex_Validated);
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
			// tbAttenuationCode
			// 
			this.tbAttenuationCode.AccessibleDescription = resources.GetString("tbAttenuationCode.AccessibleDescription");
			this.tbAttenuationCode.AccessibleName = resources.GetString("tbAttenuationCode.AccessibleName");
			this.tbAttenuationCode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbAttenuationCode.Anchor")));
			this.tbAttenuationCode.AutoSize = ((bool)(resources.GetObject("tbAttenuationCode.AutoSize")));
			this.tbAttenuationCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbAttenuationCode.BackgroundImage")));
			this.tbAttenuationCode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbAttenuationCode.Dock")));
			this.tbAttenuationCode.Enabled = ((bool)(resources.GetObject("tbAttenuationCode.Enabled")));
			this.tbAttenuationCode.Font = ((System.Drawing.Font)(resources.GetObject("tbAttenuationCode.Font")));
			this.tbAttenuationCode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbAttenuationCode.ImeMode")));
			this.tbAttenuationCode.Location = ((System.Drawing.Point)(resources.GetObject("tbAttenuationCode.Location")));
			this.tbAttenuationCode.MaxLength = ((int)(resources.GetObject("tbAttenuationCode.MaxLength")));
			this.tbAttenuationCode.Multiline = ((bool)(resources.GetObject("tbAttenuationCode.Multiline")));
			this.tbAttenuationCode.Name = "tbAttenuationCode";
			this.tbAttenuationCode.PasswordChar = ((char)(resources.GetObject("tbAttenuationCode.PasswordChar")));
			this.tbAttenuationCode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbAttenuationCode.RightToLeft")));
			this.tbAttenuationCode.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbAttenuationCode.ScrollBars")));
			this.tbAttenuationCode.Size = ((System.Drawing.Size)(resources.GetObject("tbAttenuationCode.Size")));
			this.tbAttenuationCode.TabIndex = ((int)(resources.GetObject("tbAttenuationCode.TabIndex")));
			this.tbAttenuationCode.Text = resources.GetString("tbAttenuationCode.Text");
			this.tbAttenuationCode.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbAttenuationCode.TextAlign")));
			this.tbAttenuationCode.Visible = ((bool)(resources.GetObject("tbAttenuationCode.Visible")));
			this.tbAttenuationCode.WordWrap = ((bool)(resources.GetObject("tbAttenuationCode.WordWrap")));
			this.tbAttenuationCode.Validating += new System.ComponentModel.CancelEventHandler(this.uintHex_Validating);
			this.tbAttenuationCode.Validated += new System.EventHandler(this.uintHex_Validated);
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
			// tbFlags2
			// 
			this.tbFlags2.AccessibleDescription = resources.GetString("tbFlags2.AccessibleDescription");
			this.tbFlags2.AccessibleName = resources.GetString("tbFlags2.AccessibleName");
			this.tbFlags2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbFlags2.Anchor")));
			this.tbFlags2.AutoSize = ((bool)(resources.GetObject("tbFlags2.AutoSize")));
			this.tbFlags2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbFlags2.BackgroundImage")));
			this.tbFlags2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbFlags2.Dock")));
			this.tbFlags2.Enabled = ((bool)(resources.GetObject("tbFlags2.Enabled")));
			this.tbFlags2.Font = ((System.Drawing.Font)(resources.GetObject("tbFlags2.Font")));
			this.tbFlags2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbFlags2.ImeMode")));
			this.tbFlags2.Location = ((System.Drawing.Point)(resources.GetObject("tbFlags2.Location")));
			this.tbFlags2.MaxLength = ((int)(resources.GetObject("tbFlags2.MaxLength")));
			this.tbFlags2.Multiline = ((bool)(resources.GetObject("tbFlags2.Multiline")));
			this.tbFlags2.Name = "tbFlags2";
			this.tbFlags2.PasswordChar = ((char)(resources.GetObject("tbFlags2.PasswordChar")));
			this.tbFlags2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbFlags2.RightToLeft")));
			this.tbFlags2.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbFlags2.ScrollBars")));
			this.tbFlags2.Size = ((System.Drawing.Size)(resources.GetObject("tbFlags2.Size")));
			this.tbFlags2.TabIndex = ((int)(resources.GetObject("tbFlags2.TabIndex")));
			this.tbFlags2.Text = resources.GetString("tbFlags2.Text");
			this.tbFlags2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbFlags2.TextAlign")));
			this.tbFlags2.Visible = ((bool)(resources.GetObject("tbFlags2.Visible")));
			this.tbFlags2.WordWrap = ((bool)(resources.GetObject("tbFlags2.WordWrap")));
			this.tbFlags2.Validating += new System.ComponentModel.CancelEventHandler(this.ushortHex_Validating);
			this.tbFlags2.Validated += new System.EventHandler(this.ushortHex_Validated);
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
			// tbGuardian
			// 
			this.tbGuardian.AccessibleDescription = resources.GetString("tbGuardian.AccessibleDescription");
			this.tbGuardian.AccessibleName = resources.GetString("tbGuardian.AccessibleName");
			this.tbGuardian.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbGuardian.Anchor")));
			this.tbGuardian.AutoSize = ((bool)(resources.GetObject("tbGuardian.AutoSize")));
			this.tbGuardian.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbGuardian.BackgroundImage")));
			this.tbGuardian.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbGuardian.Dock")));
			this.tbGuardian.Enabled = ((bool)(resources.GetObject("tbGuardian.Enabled")));
			this.tbGuardian.Font = ((System.Drawing.Font)(resources.GetObject("tbGuardian.Font")));
			this.tbGuardian.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbGuardian.ImeMode")));
			this.tbGuardian.Location = ((System.Drawing.Point)(resources.GetObject("tbGuardian.Location")));
			this.tbGuardian.MaxLength = ((int)(resources.GetObject("tbGuardian.MaxLength")));
			this.tbGuardian.Multiline = ((bool)(resources.GetObject("tbGuardian.Multiline")));
			this.tbGuardian.Name = "tbGuardian";
			this.tbGuardian.PasswordChar = ((char)(resources.GetObject("tbGuardian.PasswordChar")));
			this.tbGuardian.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbGuardian.RightToLeft")));
			this.tbGuardian.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbGuardian.ScrollBars")));
			this.tbGuardian.Size = ((System.Drawing.Size)(resources.GetObject("tbGuardian.Size")));
			this.tbGuardian.TabIndex = ((int)(resources.GetObject("tbGuardian.TabIndex")));
			this.tbGuardian.Text = resources.GetString("tbGuardian.Text");
			this.tbGuardian.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbGuardian.TextAlign")));
			this.tbGuardian.Visible = ((bool)(resources.GetObject("tbGuardian.Visible")));
			this.tbGuardian.WordWrap = ((bool)(resources.GetObject("tbGuardian.WordWrap")));
			this.tbGuardian.Validating += new System.ComponentModel.CancelEventHandler(this.ushortHex_Validating);
			this.tbGuardian.Validated += new System.EventHandler(this.ushortHex_Validated);
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
			// gbFlags
			// 
			this.gbFlags.AccessibleDescription = resources.GetString("gbFlags.AccessibleDescription");
			this.gbFlags.AccessibleName = resources.GetString("gbFlags.AccessibleName");
			this.gbFlags.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gbFlags.Anchor")));
			this.gbFlags.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gbFlags.BackgroundImage")));
			this.gbFlags.Controls.Add(this.tbFlags);
			this.gbFlags.Controls.Add(this.label24);
			this.gbFlags.Controls.Add(this.cbvisitor);
			this.gbFlags.Controls.Add(this.cbunk3);
			this.gbFlags.Controls.Add(this.cbunk4);
			this.gbFlags.Controls.Add(this.cbunk1);
			this.gbFlags.Controls.Add(this.cbunk2);
			this.gbFlags.Controls.Add(this.cbteens);
			this.gbFlags.Controls.Add(this.cbelders);
			this.gbFlags.Controls.Add(this.cbtodlers);
			this.gbFlags.Controls.Add(this.cbautofirst);
			this.gbFlags.Controls.Add(this.cbdebugmenu);
			this.gbFlags.Controls.Add(this.cbadults);
			this.gbFlags.Controls.Add(this.cbdemochild);
			this.gbFlags.Controls.Add(this.cbchildren);
			this.gbFlags.Controls.Add(this.cbconsecutive);
			this.gbFlags.Controls.Add(this.cbimmediately);
			this.gbFlags.Controls.Add(this.cbjoinable);
			this.gbFlags.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("gbFlags.Dock")));
			this.gbFlags.Enabled = ((bool)(resources.GetObject("gbFlags.Enabled")));
			this.gbFlags.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.gbFlags.Font = ((System.Drawing.Font)(resources.GetObject("gbFlags.Font")));
			this.gbFlags.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gbFlags.ImeMode")));
			this.gbFlags.Location = ((System.Drawing.Point)(resources.GetObject("gbFlags.Location")));
			this.gbFlags.Name = "gbFlags";
			this.gbFlags.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("gbFlags.RightToLeft")));
			this.gbFlags.Size = ((System.Drawing.Size)(resources.GetObject("gbFlags.Size")));
			this.gbFlags.TabIndex = ((int)(resources.GetObject("gbFlags.TabIndex")));
			this.gbFlags.TabStop = false;
			this.gbFlags.Text = resources.GetString("gbFlags.Text");
			this.gbFlags.Visible = ((bool)(resources.GetObject("gbFlags.Visible")));
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
			this.tbFlags.Validating += new System.ComponentModel.CancelEventHandler(this.ushortHex_Validating);
			this.tbFlags.Validated += new System.EventHandler(this.ushortHex_Validated);
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
			this.cbvisitor.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
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
			this.cbunk3.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
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
			this.cbunk4.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
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
			this.cbunk1.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
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
			this.cbunk2.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
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
			this.cbteens.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
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
			this.cbelders.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
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
			this.cbtodlers.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
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
			this.cbautofirst.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
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
			this.cbdebugmenu.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
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
			this.cbadults.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
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
			this.cbdemochild.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
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
			this.cbchildren.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
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
			this.cbconsecutive.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
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
			this.cbimmediately.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
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
			this.cbjoinable.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
			// 
			// tbAction
			// 
			this.tbAction.AccessibleDescription = resources.GetString("tbAction.AccessibleDescription");
			this.tbAction.AccessibleName = resources.GetString("tbAction.AccessibleName");
			this.tbAction.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbAction.Anchor")));
			this.tbAction.AutoSize = ((bool)(resources.GetObject("tbAction.AutoSize")));
			this.tbAction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbAction.BackgroundImage")));
			this.tbAction.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbAction.Dock")));
			this.tbAction.Enabled = ((bool)(resources.GetObject("tbAction.Enabled")));
			this.tbAction.Font = ((System.Drawing.Font)(resources.GetObject("tbAction.Font")));
			this.tbAction.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbAction.ImeMode")));
			this.tbAction.Location = ((System.Drawing.Point)(resources.GetObject("tbAction.Location")));
			this.tbAction.MaxLength = ((int)(resources.GetObject("tbAction.MaxLength")));
			this.tbAction.Multiline = ((bool)(resources.GetObject("tbAction.Multiline")));
			this.tbAction.Name = "tbAction";
			this.tbAction.PasswordChar = ((char)(resources.GetObject("tbAction.PasswordChar")));
			this.tbAction.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbAction.RightToLeft")));
			this.tbAction.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbAction.ScrollBars")));
			this.tbAction.Size = ((System.Drawing.Size)(resources.GetObject("tbAction.Size")));
			this.tbAction.TabIndex = ((int)(resources.GetObject("tbAction.TabIndex")));
			this.tbAction.Text = resources.GetString("tbAction.Text");
			this.tbAction.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbAction.TextAlign")));
			this.tbAction.Visible = ((bool)(resources.GetObject("tbAction.Visible")));
			this.tbAction.WordWrap = ((bool)(resources.GetObject("tbAction.WordWrap")));
			this.tbAction.Validating += new System.ComponentModel.CancelEventHandler(this.ushortHex_Validating);
			this.tbAction.Validated += new System.EventHandler(this.ushortHex_Validated);
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
			// tbJoinIndex
			// 
			this.tbJoinIndex.AccessibleDescription = resources.GetString("tbJoinIndex.AccessibleDescription");
			this.tbJoinIndex.AccessibleName = resources.GetString("tbJoinIndex.AccessibleName");
			this.tbJoinIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbJoinIndex.Anchor")));
			this.tbJoinIndex.AutoSize = ((bool)(resources.GetObject("tbJoinIndex.AutoSize")));
			this.tbJoinIndex.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbJoinIndex.BackgroundImage")));
			this.tbJoinIndex.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbJoinIndex.Dock")));
			this.tbJoinIndex.Enabled = ((bool)(resources.GetObject("tbJoinIndex.Enabled")));
			this.tbJoinIndex.Font = ((System.Drawing.Font)(resources.GetObject("tbJoinIndex.Font")));
			this.tbJoinIndex.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbJoinIndex.ImeMode")));
			this.tbJoinIndex.Location = ((System.Drawing.Point)(resources.GetObject("tbJoinIndex.Location")));
			this.tbJoinIndex.MaxLength = ((int)(resources.GetObject("tbJoinIndex.MaxLength")));
			this.tbJoinIndex.Multiline = ((bool)(resources.GetObject("tbJoinIndex.Multiline")));
			this.tbJoinIndex.Name = "tbJoinIndex";
			this.tbJoinIndex.PasswordChar = ((char)(resources.GetObject("tbJoinIndex.PasswordChar")));
			this.tbJoinIndex.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbJoinIndex.RightToLeft")));
			this.tbJoinIndex.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbJoinIndex.ScrollBars")));
			this.tbJoinIndex.Size = ((System.Drawing.Size)(resources.GetObject("tbJoinIndex.Size")));
			this.tbJoinIndex.TabIndex = ((int)(resources.GetObject("tbJoinIndex.TabIndex")));
			this.tbJoinIndex.Text = resources.GetString("tbJoinIndex.Text");
			this.tbJoinIndex.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbJoinIndex.TextAlign")));
			this.tbJoinIndex.Visible = ((bool)(resources.GetObject("tbJoinIndex.Visible")));
			this.tbJoinIndex.WordWrap = ((bool)(resources.GetObject("tbJoinIndex.WordWrap")));
			this.tbJoinIndex.Validating += new System.ComponentModel.CancelEventHandler(this.uintHex_Validating);
			this.tbJoinIndex.Validated += new System.EventHandler(this.uintHex_Validated);
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
			// tpMotives
			// 
			this.tpMotives.AccessibleDescription = resources.GetString("tpMotives.AccessibleDescription");
			this.tpMotives.AccessibleName = resources.GetString("tpMotives.AccessibleName");
			this.tpMotives.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tpMotives.Anchor")));
			this.tpMotives.AutoScroll = ((bool)(resources.GetObject("tpMotives.AutoScroll")));
			this.tpMotives.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tpMotives.AutoScrollMargin")));
			this.tpMotives.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tpMotives.AutoScrollMinSize")));
			this.tpMotives.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tpMotives.BackgroundImage")));
			this.tpMotives.Controls.Add(this.ttabItemMotiveTableUI1);
			this.tpMotives.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tpMotives.Dock")));
			this.tpMotives.Enabled = ((bool)(resources.GetObject("tpMotives.Enabled")));
			this.tpMotives.Font = ((System.Drawing.Font)(resources.GetObject("tpMotives.Font")));
			this.tpMotives.ImageIndex = ((int)(resources.GetObject("tpMotives.ImageIndex")));
			this.tpMotives.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tpMotives.ImeMode")));
			this.tpMotives.Location = ((System.Drawing.Point)(resources.GetObject("tpMotives.Location")));
			this.tpMotives.Name = "tpMotives";
			this.tpMotives.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tpMotives.RightToLeft")));
			this.tpMotives.Size = ((System.Drawing.Size)(resources.GetObject("tpMotives.Size")));
			this.tpMotives.TabIndex = ((int)(resources.GetObject("tpMotives.TabIndex")));
			this.tpMotives.Text = resources.GetString("tpMotives.Text");
			this.tpMotives.ToolTipText = resources.GetString("tpMotives.ToolTipText");
			this.tpMotives.Visible = ((bool)(resources.GetObject("tpMotives.Visible")));
			// 
			// ttabItemMotiveTableUI1
			// 
			this.ttabItemMotiveTableUI1.AccessibleDescription = resources.GetString("ttabItemMotiveTableUI1.AccessibleDescription");
			this.ttabItemMotiveTableUI1.AccessibleName = resources.GetString("ttabItemMotiveTableUI1.AccessibleName");
			this.ttabItemMotiveTableUI1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabItemMotiveTableUI1.Anchor")));
			this.ttabItemMotiveTableUI1.AutoScroll = ((bool)(resources.GetObject("ttabItemMotiveTableUI1.AutoScroll")));
			this.ttabItemMotiveTableUI1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabItemMotiveTableUI1.AutoScrollMargin")));
			this.ttabItemMotiveTableUI1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabItemMotiveTableUI1.AutoScrollMinSize")));
			this.ttabItemMotiveTableUI1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabItemMotiveTableUI1.BackgroundImage")));
			this.ttabItemMotiveTableUI1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabItemMotiveTableUI1.Dock")));
			this.ttabItemMotiveTableUI1.Enabled = ((bool)(resources.GetObject("ttabItemMotiveTableUI1.Enabled")));
			this.ttabItemMotiveTableUI1.Font = ((System.Drawing.Font)(resources.GetObject("ttabItemMotiveTableUI1.Font")));
			this.ttabItemMotiveTableUI1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabItemMotiveTableUI1.ImeMode")));
			this.ttabItemMotiveTableUI1.Location = ((System.Drawing.Point)(resources.GetObject("ttabItemMotiveTableUI1.Location")));
			this.ttabItemMotiveTableUI1.Name = "ttabItemMotiveTableUI1";
			this.ttabItemMotiveTableUI1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabItemMotiveTableUI1.RightToLeft")));
			this.ttabItemMotiveTableUI1.Size = ((System.Drawing.Size)(resources.GetObject("ttabItemMotiveTableUI1.Size")));
			this.ttabItemMotiveTableUI1.TabIndex = ((int)(resources.GetObject("ttabItemMotiveTableUI1.TabIndex")));
			this.ttabItemMotiveTableUI1.Visible = ((bool)(resources.GetObject("ttabItemMotiveTableUI1.Visible")));
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
			this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
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
			this.tabControl1.ResumeLayout(false);
			this.tpSettings.ResumeLayout(false);
			this.gbFlags.ResumeLayout(false);
			this.tpMotives.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	
		#endregion

		private void TtabSelect(object sender, System.EventArgs e)
		{
			if (this.internalchg) return;

			this.btnDelete.Enabled = false;
			if (lbttab.SelectedIndex < 0) return;

			TtabItem item = wrapper[lbttab.SelectedIndex];

			internalchg = true;

			btnDelete.Enabled = true;

			this.tbGuardian.Text = "0x"+Helper.HexString(item.Guardian);				
			this.tbAction.Text = "0x"+Helper.HexString(item.Action);
			lbguard.Text = item.GuardianName;
			lbaction.Text = item.ActionName;

			this.tbFlags.Text = "0x"+Helper.HexString(item.Flags.Value);
			this.tbFlags2.Text = "0x"+Helper.HexString(item.Flags2);
			tbStringIndex.Text = "0x"+Helper.HexString(item.StringIndex);

			tbAttenuationCode.Text = "0x"+Helper.HexString(item.AttenuationCode);
			tbAttenuationValue.Text = "0x"+Helper.HexString(item.AttenuationValue);
			tbAutonomy.Text = "0x"+Helper.HexString(item.Autonomy);
			tbJoinIndex.Text = "0x"+Helper.HexString(item.JoinIndex);
			tbRes5.Text = "0x"+Helper.HexString(item.Res5);
			tbRes6.Text = "0x"+Helper.HexString(item.Res6);
			tbRes7.Text = item.Res7.ToString("N9");
			tbRes8.Text = "0x"+Helper.HexString(item.Res8);
			tbRes9.Text = "0x"+Helper.HexString(item.Res9);

			doFlags();

			this.ttabItemMotiveTableUI1.SetData(wrapper[lbttab.SelectedIndex]);
			internalchg = false;
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
			int i = wrapper.AddItem();

			if (lbttab.SelectedIndex != -1)
			{
				TtabItem ci = wrapper[lbttab.SelectedIndex];
				TtabItem ni = wrapper[i];
				ni.Action = ci.Action;
				ni.AttenuationCode = ci.AttenuationCode;
				ni.AttenuationValue = ci.AttenuationValue;
				ni.Autonomy = ci.Autonomy;
				ni.Flags.Value = ci.Flags.Value;
				ni.Flags2 = ci.Flags2;
				ni.Guardian = ci.Guardian;
				ni.JoinIndex = ci.JoinIndex;
				ni.Res5 = ci.Res5;
				ni.Res6 = ci.Res6;
				ni.Res7 = ci.Res7;
				ni.Res8 = ci.Res8;
				ni.Res9 = ci.Res9;
				ni.StringIndex = ci.StringIndex;
				for (int mg = 0; mg < ci.nrGroups; mg++)
					for (int m = 0; m < ci.nrMotives[mg]; m++)
					{
						ni[mg, m, 0] = ci[mg, m, 0];
						ni[mg, m, 1] = ci[mg, m, 1];
						ni[mg, m, 2] = ci[mg, m, 2];
					}
			}
			lbttab.Items.Add(wrapper[i]);
			lbttab.SelectedIndex = i;
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			if (lbttab.SelectedIndex < 0) return;

			int i = lbttab.SelectedIndex;

			lbttab.Items.RemoveAt(i);
			wrapper.RemoveAt(i);

			if (i >= lbttab.Items.Count)
				i = lbttab.Items.Count - 1;
			lbttab.SelectedIndex = -1;
			lbttab.SelectedIndex = i;
		}


		private void GetTTABGuard(object sender, System.EventArgs e)
		{
			try 
			{
				Bhav bhav = new Bhav(wrapper.Opcodes);
				bhav.Package = wrapper.Package;
				bhav.FileDescriptor = wrapper.FileDescriptor;
				
				int opcode = SimPe.Plugin.WrapperFactory.BhavWizardForm.Execute(bhav, ttabPanel.Parent);

				if (opcode != -1)
				{
					TtabItem item = wrapper[this.lbttab.SelectedIndex];
					item.Guardian = (ushort)opcode;
					this.tbGuardian.Text = "0x"+Helper.HexString(item.Guardian);
					lbguard.Text = item.GuardianName;
				}
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
				Bhav bhav = new Bhav(wrapper.Opcodes);
				bhav.Package = wrapper.Package;
				bhav.FileDescriptor = wrapper.FileDescriptor;
				
				int opcode = SimPe.Plugin.WrapperFactory.BhavWizardForm.Execute(bhav, ttabPanel.Parent);

				if (opcode != -1)
				{
					TtabItem item = wrapper[this.lbttab.SelectedIndex];
					item.Action = (ushort)opcode;
					this.tbAction.Text = "0x"+Helper.HexString(item.Action);
					lbaction.Text = item.ActionName;
				}
			} 
			catch (Exception ex) 
			{
				
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}


		private void tbFilename_TextChanged(object sender, System.EventArgs e)
		{
			wrapper.FileName = tbFilename.Text;
		}

		private void tbFilename_Validated(object sender, System.EventArgs e)
		{
			tbFilename.SelectAll();
		}


		/*
		 * By way of reminder:
		 * action           - ushort - 4 hex digits (BHAV number)
		 * guard            - ushort - 4 hex digits (BHAV number)
		 * flags            - ushort - 4 hex digits
		 * flags2           - ushort - 4 hex digits
		 * strindex         - uint   - 8 hex digits
		 * attenuationcode  - uint   - 8 hex digits
		 * attenuationvalue - uint   - 8 hex digits
		 * autonomy         - uint   - 8 hex digits
		 * joinindex        - uint   - 8 hex digits
		 * res5             - uint   - 8 hex digits
		 * res6             - uint   - 8 hex digits
		 * res7             - float  - decimal digits and "."
		 * res8             - uint   - 8 hex digits
		 * res9             - ushort - 4 hex digits
		 */

		private void ushortHex_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try { Convert.ToUInt16(((TextBox)sender).Text, 16); }
			catch { e.Cancel = true; }
		}

		private void uintHex_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try { Convert.ToUInt32(((TextBox)sender).Text, 16); }
			catch { e.Cancel = true; }
		}

		private void float_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try { Convert.ToSingle(((TextBox)sender).Text); }
			catch { e.Cancel = true; }
		}


		private void ushortHex_Validated(object sender, System.EventArgs e)
		{
			ushort val = Convert.ToUInt16(((TextBox)sender).Text, 16);
			int i = alUshorts.IndexOf(sender);

			switch(i)
			{
				case 0:
					wrapper[this.lbttab.SelectedIndex].Action = val;
					lbaction.Text = wrapper[this.lbttab.SelectedIndex].ActionName;
					break;
				case 1:
					wrapper[this.lbttab.SelectedIndex].Guardian = val;
					lbguard.Text = wrapper[this.lbttab.SelectedIndex].GuardianName;
					break;
				case 2:
					wrapper[this.lbttab.SelectedIndex].Flags.Value = val;
					doFlags();
					break;
				case 3: wrapper[this.lbttab.SelectedIndex].Flags2 = val; break;
				case 4: wrapper[this.lbttab.SelectedIndex].Res9 = val; break;
				default:
					throw new Exception("ushortHex_Validated not applicable to control " + sender.ToString());
			}
		}

		private void uintHex_Validated(object sender, System.EventArgs e)
		{
			uint val = Convert.ToUInt32(((TextBox)sender).Text, 16);
			int i = alUints.IndexOf(sender);

			switch(i)
			{
				case 0: wrapper.Format = val; break;
				case 1:
					wrapper[this.lbttab.SelectedIndex].StringIndex = val;
					lbttab.Items[lbttab.SelectedIndex] = wrapper[lbttab.SelectedIndex];
					break;
				case 2: wrapper[this.lbttab.SelectedIndex].AttenuationCode = val; break;
				case 3: wrapper[this.lbttab.SelectedIndex].AttenuationValue = val; break;
				case 4: wrapper[this.lbttab.SelectedIndex].Autonomy = val; break;
				case 5: wrapper[this.lbttab.SelectedIndex].Res5 = val; break;
				case 6: wrapper[this.lbttab.SelectedIndex].Res6 = val; break;
				case 7: wrapper[this.lbttab.SelectedIndex].Res8 = val; break;
				case 8: wrapper[this.lbttab.SelectedIndex].JoinIndex = val; break;
				default:
					throw new Exception("uintHex_Validated not applicable to control " + sender.ToString());
			}
		}

		private void float_Validated(object sender, System.EventArgs e)
		{
			float val = Convert.ToSingle(((TextBox)sender).Text);
			int i = alFloats.IndexOf(sender);

			switch(i)
			{
				case 0: wrapper[this.lbttab.SelectedIndex].Res7 = val; break;
				default:
					throw new Exception("float_Validated not applicable to control " + sender.ToString());
			}
		}


		private void checkbox_CheckedChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			if (!(sender is CheckBox)) return;
			bool val = ((CheckBox)sender).Checked;

			int i = alFlags.IndexOf(sender);
			switch(i)
			{
				case  0: wrapper[this.lbttab.SelectedIndex].Flags.ByVisitors = val; break;
				case  1: wrapper[this.lbttab.SelectedIndex].Flags.Joinable = val; break;
				case  2: wrapper[this.lbttab.SelectedIndex].Flags.RunImmediately = val; break;
				case  3: wrapper[this.lbttab.SelectedIndex].Flags.AvailConsecutive = val; break;
				case  4: wrapper[this.lbttab.SelectedIndex].Flags.ByChildren = val; break;
				case  5: wrapper[this.lbttab.SelectedIndex].Flags.ByDemoChild = val; break;
				case  6: wrapper[this.lbttab.SelectedIndex].Flags.ByAdults = val; break;
				case  7: wrapper[this.lbttab.SelectedIndex].Flags.DebugMenu = val; break;
				case  8: wrapper[this.lbttab.SelectedIndex].Flags.AutoFirstSelect = val; break;
				case  9: wrapper[this.lbttab.SelectedIndex].Flags.ByToddlers = val; break;
				case 10: wrapper[this.lbttab.SelectedIndex].Flags.ByElders = val; break;
				case 11: wrapper[this.lbttab.SelectedIndex].Flags.ByTeens = val; break;
				case 12: wrapper[this.lbttab.SelectedIndex].Flags.Unknown1 = val; break;
				case 13: wrapper[this.lbttab.SelectedIndex].Flags.Unknown2 = val; break;
				case 14: wrapper[this.lbttab.SelectedIndex].Flags.Unknown3 = val; break;
				case 15: wrapper[this.lbttab.SelectedIndex].Flags.Unknown4 = val; break;
				default:
					throw new Exception("checkbox_CheckedChanged not applicable to control " + sender.ToString());
			}
			internalchg = true;
			this.tbFlags.Text = "0x"+Helper.HexString(wrapper[this.lbttab.SelectedIndex].Flags.Value);
			internalchg = false;
		}

	}
}
