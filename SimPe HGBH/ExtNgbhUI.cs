/***************************************************************************
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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces;
using SimPe.PackedFiles.Wrapper.Supporting;
using SimPe.Data;
//using Ambertation.Windows.Forms;
//using SimPe.Windows.Forms;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for ExtNgbhUI.
	/// </summary>
	public class ExtNgbhUI : 
		//System.Windows.Forms.UserControl
		SimPe.Windows.Forms.WrapperBaseControl, SimPe.Interfaces.Plugin.IPackedFileUI
    {
        private IContainer components;
        SimPe.PackedFiles.Wrapper.SimPoolControl spc = null;
		private Panel pnSims;
        private Panel pnBadge;
        private Panel pnHood;
        private Panel pnDebug;
        private ListView lvhood;
        private ListView lvIntern;
        private ImageList memilist;
        private PropertyGrid pg;
        private TextBox tbRawLength;
        private TextBox tbdebug;
        private Label lblength;
        private LinkLabel llSetRawLength;
        private LinkLabel lldel;
        private TabControl tbChood;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private ToolStrip toolBar1;
        private ToolStripButton biSim;
        private ToolStripButton biBadge;
        private ToolStripButton biHood;
        private ToolStripButton biDebug;
        private MenuStrip menuBar1;
        private ContextMenuStrip menu;
        private ToolStripMenuItem miNuke;
        private ToolStripMenuItem miFix;
		private NgbhSlotSelection nssel;
        private NgbhSlotUI nsui;
        private NgbhSkillHelper shelper;
        NgbhSlotUI simslot = null;
        NgbhItem nitem;

		public ExtNgbhUI()
		{
            InitializeComponent();
            if ((byte)Helper.WindowsRegistry.LanguageCode == 1)
            {
                this.tabPage1.Text = "Neighborhood";
                this.tabPage2.Text = "Neighborhood (intern)";
            }
            booby.ThemeManager tm = booby.ThemeManager.Global.CreateChild();
            tm.AddControl(this.toolBar1);
            tm.AddControl(this.menu);
            if (booby.ThemeManager.ThemedForms)
            {
                this.spc.ListBackground = booby.ThemeManager.Global.ThemeColorLighter;
                tm.AddControl(this.pg);
            }
			biSim.Tag = pnSims;
			biDebug.Tag = pnDebug;
			biBadge.Tag = pnBadge;
            biHood.Tag = pnHood;
            if (Helper.WindowsRegistry.HiddenMode)
            {
                biDebug.Visible = true;
                biHood.Visible = false;
            }
            else
            {
                biDebug.Visible = false;
                biHood.Visible = true;
                this.menu.Items.Remove(this.miFix);
            }
			this.SelectButton(biSim);
            biBadge.Enabled = (SimPe.PathProvider.Global.EPInstalled >= 3 || SimPe.PathProvider.Global.STInstalled >= 28);
			SimPe.RemoteControl.HookToMessageQueue(0x4E474248, new SimPe.RemoteControl.ControlEvent(ControlEvent));
            this.tbdebug.Visible = false;
		}

		protected void ControlEvent(object sender, SimPe.RemoteControl.ControlEventArgs e)
		{			
			object[] os = e.Items as object[];
			if (os!=null) 
			{
				Data.NeighborhoodSlots st = (Data.NeighborhoodSlots)os[1];				
				uint inst = (uint)os[0];

				if (st== Data.NeighborhoodSlots.SimsIntern && biBadge.Enabled) this.ChoosePage(biBadge, null);
				else this.ChoosePage(biSim, null);

				PackedFiles.Wrapper.ExtSDesc sdesc = FileTable.ProviderRegistry.SimDescriptionProvider.FindSim((ushort)inst) as PackedFiles.Wrapper.ExtSDesc;
				bool found = SelectSimByInstance(sdesc);
				
				if (!found && sdesc!=null) 
				{
					spc.SelectHousehold(sdesc.HouseholdName);
					SelectSimByInstance(sdesc);
				}

                spc.Refresh(false);
			}
		}

		protected bool SelectSimByInstance(PackedFiles.Wrapper.SDesc sdesc)
		{
			bool ret = false;
			if (sdesc!=null) 
			{
				spc.SelectedElement = sdesc;
				if (spc.SelectedElement!=null) return true;
			}
			return ret;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if (disposing) { if(components != null) { components.Dispose(); } }
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtNgbhUI));
            this.pnSims = new System.Windows.Forms.Panel();
            this.menuBar1 = new System.Windows.Forms.MenuStrip();
            this.spc = new SimPe.PackedFiles.Wrapper.SimPoolControl();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miNuke = new System.Windows.Forms.ToolStripMenuItem();
            this.miFix = new System.Windows.Forms.ToolStripMenuItem();
            this.simslot = new SimPe.Plugin.NgbhSlotUI();
            this.pnHood = new System.Windows.Forms.Panel();
            this.lldel = new System.Windows.Forms.LinkLabel();
            this.tbdebug = new System.Windows.Forms.TextBox();
            this.llSetRawLength = new System.Windows.Forms.LinkLabel();
            this.lblength = new System.Windows.Forms.Label();
            this.tbRawLength = new System.Windows.Forms.TextBox();
            this.pg = new System.Windows.Forms.PropertyGrid();
            this.tbChood = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lvhood = new System.Windows.Forms.ListView();
            this.memilist = new System.Windows.Forms.ImageList(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lvIntern = new System.Windows.Forms.ListView();
            this.pnDebug = new System.Windows.Forms.Panel();
            this.nsui = new SimPe.Plugin.NgbhSlotUI();
            this.nssel = new SimPe.Plugin.NgbhSlotSelection();
            this.pnBadge = new System.Windows.Forms.Panel();
            this.shelper = new SimPe.Plugin.NgbhSkillHelper();
            this.toolBar1 = new System.Windows.Forms.ToolStrip();
            this.biSim = new System.Windows.Forms.ToolStripButton();
            this.biBadge = new System.Windows.Forms.ToolStripButton();
            this.biHood = new System.Windows.Forms.ToolStripButton();
            this.biDebug = new System.Windows.Forms.ToolStripButton();
            this.pnSims.SuspendLayout();
            this.menu.SuspendLayout();
            this.pnHood.SuspendLayout();
            this.tbChood.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.pnDebug.SuspendLayout();
            this.pnBadge.SuspendLayout();
            this.toolBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnSims
            // 
            this.pnSims.BackColor = System.Drawing.Color.Transparent;
            this.pnSims.Controls.Add(this.menuBar1);
            this.pnSims.Controls.Add(this.spc);
            this.pnSims.Controls.Add(this.simslot);
            this.pnSims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnSims.Location = new System.Drawing.Point(0, 76);
            this.pnSims.Name = "pnSims";
            this.pnSims.Size = new System.Drawing.Size(680, 292);
            this.pnSims.TabIndex = 1;
            this.pnSims.Visible = false;
            // 
            // menuBar1
            // 
            this.menuBar1.Location = new System.Drawing.Point(264, 0);
            this.menuBar1.Name = "menuBar1";
            this.menuBar1.Size = new System.Drawing.Size(416, 24);
            this.menuBar1.TabIndex = 5;
            this.menuBar1.Text = "menuBar1";
            this.menuBar1.Visible = false;
            // 
            // spc
            // 
            this.spc.BackColor = System.Drawing.Color.White;
            this.spc.ContextMenuStrip = this.menu;
            this.spc.Dock = System.Windows.Forms.DockStyle.Left;
            this.spc.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.spc.ListBackground = System.Drawing.SystemColors.Info;
            this.spc.Location = new System.Drawing.Point(0, 0);
            this.spc.Name = "spc";
            this.spc.Package = null;
            this.spc.Padding = new System.Windows.Forms.Padding(1);
            this.spc.RightClickSelect = false;
            this.spc.SelectedElement = null;
            this.spc.SelectedSim = null;
            this.spc.SimDetails = false;
            this.spc.Size = new System.Drawing.Size(264, 292);
            this.spc.TabIndex = 0;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNuke,
            this.miFix});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(159, 48);
            this.menu.Text = "(context menu)";
            this.menu.VisibleChanged += new System.EventHandler(this.menu_VisibleChanged);
            // 
            // miNuke
            // 
            this.miNuke.Image = ((System.Drawing.Image)(resources.GetObject("miNuke.Image")));
            this.miNuke.Name = "miNuke";
            this.miNuke.Size = new System.Drawing.Size(158, 22);
            this.miNuke.Text = "Nuke Memories";
            this.miNuke.Click += new System.EventHandler(this.miNuke_Activate);
            // 
            // miFix
            // 
            this.miFix.Image = ((System.Drawing.Image)(resources.GetObject("miFix.Image")));
            this.miFix.Name = "miFix";
            this.miFix.Size = new System.Drawing.Size(158, 22);
            this.miFix.Text = "Fix Memories";
            this.miFix.Click += new System.EventHandler(this.miFix_Activate);
            // 
            // simslot
            // 
            this.simslot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.simslot.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.simslot.Location = new System.Drawing.Point(264, 0);
            this.simslot.Name = "simslot";
            this.simslot.NgbhResource = null;
            this.simslot.SimPoolControl = this.spc;
            this.simslot.Size = new System.Drawing.Size(416, 290);
            this.simslot.Slit = null;
            this.simslot.Slot = null;
            this.simslot.SlotType = SimPe.Data.NeighborhoodSlots.Sims;
            this.simslot.Slut = null;
            this.simslot.TabIndex = 2;
            // 
            // pnHood
            // 
            this.pnHood.BackColor = System.Drawing.Color.Transparent;
            this.pnHood.Controls.Add(this.lldel);
            this.pnHood.Controls.Add(this.tbdebug);
            this.pnHood.Controls.Add(this.llSetRawLength);
            this.pnHood.Controls.Add(this.lblength);
            this.pnHood.Controls.Add(this.tbRawLength);
            this.pnHood.Controls.Add(this.pg);
            this.pnHood.Controls.Add(this.tbChood);
            this.pnHood.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnHood.Location = new System.Drawing.Point(0, 76);
            this.pnHood.Name = "pnHood";
            this.pnHood.Size = new System.Drawing.Size(680, 292);
            this.pnHood.TabIndex = 8;
            this.pnHood.Visible = false;
            // 
            // lldel
            // 
            this.lldel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lldel.AutoSize = true;
            this.lldel.Enabled = false;
            this.lldel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lldel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lldel.LinkArea = new System.Windows.Forms.LinkArea(0, 6);
            this.lldel.Location = new System.Drawing.Point(218, 272);
            this.lldel.Name = "lldel";
            this.lldel.Size = new System.Drawing.Size(54, 13);
            this.lldel.TabIndex = 13;
            this.lldel.TabStop = true;
            this.lldel.Text = "Remove";
            this.lldel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lldel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lldel_LinkClicked);
            // 
            // tbdebug
            // 
            this.tbdebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbdebug.Location = new System.Drawing.Point(301, 268);
            this.tbdebug.Name = "tbdebug";
            this.tbdebug.Size = new System.Drawing.Size(150, 20);
            this.tbdebug.TabIndex = 12;
            this.tbdebug.Text = "Booobies";
            // 
            // llSetRawLength
            // 
            this.llSetRawLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llSetRawLength.AutoSize = true;
            this.llSetRawLength.Enabled = false;
            this.llSetRawLength.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.llSetRawLength.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llSetRawLength.LinkArea = new System.Windows.Forms.LinkArea(0, 3);
            this.llSetRawLength.Location = new System.Drawing.Point(627, 272);
            this.llSetRawLength.Name = "llSetRawLength";
            this.llSetRawLength.Size = new System.Drawing.Size(26, 13);
            this.llSetRawLength.TabIndex = 11;
            this.llSetRawLength.TabStop = true;
            this.llSetRawLength.Text = "Set";
            this.llSetRawLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.llSetRawLength.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSetRawLength_LinkClicked);
            // 
            // lblength
            // 
            this.lblength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblength.AutoSize = true;
            this.lblength.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblength.Location = new System.Drawing.Point(472, 272);
            this.lblength.Name = "lblength";
            this.lblength.Size = new System.Drawing.Size(43, 13);
            this.lblength.TabIndex = 10;
            this.lblength.Text = "Length:";
            this.lblength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbRawLength
            // 
            this.tbRawLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRawLength.Location = new System.Drawing.Point(521, 268);
            this.tbRawLength.Name = "tbRawLength";
            this.tbRawLength.Size = new System.Drawing.Size(100, 20);
            this.tbRawLength.TabIndex = 9;
            this.tbRawLength.Text = "0";
            this.tbRawLength.TextChanged += new System.EventHandler(this.tbRawLength_TextChanged);
            // 
            // pg
            // 
            this.pg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pg.CommandsBackColor = System.Drawing.SystemColors.ControlLight;
            this.pg.HelpVisible = false;
            this.pg.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.pg.Location = new System.Drawing.Point(218, 22);
            this.pg.Name = "pg";
            this.pg.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.pg.Size = new System.Drawing.Size(460, 240);
            this.pg.TabIndex = 1;
            this.pg.ToolbarVisible = false;
            this.pg.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pg_PropertyValueChanged);
            // 
            // tbChood
            // 
            this.tbChood.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbChood.Controls.Add(this.tabPage1);
            this.tbChood.Controls.Add(this.tabPage2);
            this.tbChood.Location = new System.Drawing.Point(2, 2);
            this.tbChood.Name = "tbChood";
            this.tbChood.SelectedIndex = 0;
            this.tbChood.Size = new System.Drawing.Size(214, 288);
            this.tbChood.TabIndex = 0;
            this.tbChood.SelectedIndexChanged += new System.EventHandler(this.tbChood_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lvhood);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(206, 262);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Neighbourhood";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lvhood
            // 
            this.lvhood.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvhood.LargeImageList = this.memilist;
            this.lvhood.Location = new System.Drawing.Point(3, 3);
            this.lvhood.Name = "lvhood";
            this.lvhood.Size = new System.Drawing.Size(200, 256);
            this.lvhood.SmallImageList = this.memilist;
            this.lvhood.StateImageList = this.memilist;
            this.lvhood.TabIndex = 0;
            this.lvhood.UseCompatibleStateImageBehavior = false;
            this.lvhood.View = System.Windows.Forms.View.List;
            this.lvhood.SelectedIndexChanged += new System.EventHandler(this.lvhood_SelectedIndexChanged);
            // 
            // memilist
            // 
            this.memilist.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.memilist.ImageSize = new System.Drawing.Size(20, 22);
            this.memilist.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lvIntern);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(206, 260);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Neighbourhood (intern)";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lvIntern
            // 
            this.lvIntern.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvIntern.LargeImageList = this.memilist;
            this.lvIntern.Location = new System.Drawing.Point(3, 3);
            this.lvIntern.Name = "lvIntern";
            this.lvIntern.Size = new System.Drawing.Size(200, 254);
            this.lvIntern.SmallImageList = this.memilist;
            this.lvIntern.StateImageList = this.memilist;
            this.lvIntern.TabIndex = 0;
            this.lvIntern.UseCompatibleStateImageBehavior = false;
            this.lvIntern.View = System.Windows.Forms.View.List;
            this.lvIntern.SelectedIndexChanged += new System.EventHandler(this.lvIntern_SelectedIndexChanged);
            // 
            // pnDebug
            // 
            this.pnDebug.Controls.Add(this.nsui);
            this.pnDebug.Controls.Add(this.nssel);
            this.pnDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDebug.Location = new System.Drawing.Point(0, 76);
            this.pnDebug.Name = "pnDebug";
            this.pnDebug.Size = new System.Drawing.Size(680, 292);
            this.pnDebug.TabIndex = 3;
            this.pnDebug.Visible = false;
            // 
            // nsui
            // 
            this.nsui.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nsui.BackColor = System.Drawing.Color.Transparent;
            this.nsui.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.nsui.Location = new System.Drawing.Point(280, 8);
            this.nsui.Name = "nsui";
            this.nsui.NgbhResource = null;
            this.nsui.SimPoolControl = null;
            this.nsui.Size = new System.Drawing.Size(392, 276);
            this.nsui.Slit = null;
            this.nsui.Slot = null;
            this.nsui.SlotType = SimPe.Data.NeighborhoodSlots.Sims;
            this.nsui.Slut = null;
            this.nsui.TabIndex = 1;
            // 
            // nssel
            // 
            this.nssel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.nssel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.nssel.Location = new System.Drawing.Point(8, 8);
            this.nssel.Name = "nssel";
            this.nssel.NgbhResource = null;
            this.nssel.Size = new System.Drawing.Size(264, 276);
            this.nssel.TabIndex = 0;
            this.nssel.SelectedSlotChanged += new System.EventHandler(this.nssel_SelectedSlotChanged);
            // 
            // pnBadge
            // 
            this.pnBadge.Controls.Add(this.shelper);
            this.pnBadge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnBadge.Location = new System.Drawing.Point(0, 76);
            this.pnBadge.Name = "pnBadge";
            this.pnBadge.Size = new System.Drawing.Size(680, 292);
            this.pnBadge.TabIndex = 1;
            this.pnBadge.VisibleChanged += new System.EventHandler(this.pnBadge_VisibleChanged);
            // 
            // shelper
            // 
            this.shelper.AutoScroll = true;
            this.shelper.BackColor = System.Drawing.Color.Transparent;
            this.shelper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shelper.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.shelper.Location = new System.Drawing.Point(0, 0);
            this.shelper.Name = "shelper";
            this.shelper.NgbhResource = null;
            this.shelper.Padding = new System.Windows.Forms.Padding(8);
            this.shelper.SimPoolControl = this.spc;
            this.shelper.Size = new System.Drawing.Size(680, 292);
            this.shelper.Slot = null;
            this.shelper.TabIndex = 0;
            this.shelper.ChangedItem += new System.EventHandler(this.shelper_ChangedItem);
            this.shelper.AddedNewItem += new System.EventHandler(this.shelper_AddedNewItem);
            // 
            // toolBar1
            // 
            this.toolBar1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.toolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.biSim,
            this.biBadge,
            this.biHood,
            this.biDebug});
            this.toolBar1.Location = new System.Drawing.Point(0, 24);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.Size = new System.Drawing.Size(680, 52);
            this.toolBar1.TabIndex = 4;
            this.toolBar1.Text = "toolBar1";
            // 
            // biSim
            // 
            this.biSim.Image = ((System.Drawing.Image)(resources.GetObject("biSim.Image")));
            this.biSim.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.biSim.Name = "biSim";
            this.biSim.Size = new System.Drawing.Size(56, 49);
            this.biSim.Text = "Memories";
            this.biSim.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.biSim.Click += new System.EventHandler(this.ChoosePage);
            // 
            // biBadge
            // 
            this.biBadge.Image = ((System.Drawing.Image)(resources.GetObject("biBadge.Image")));
            this.biBadge.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.biBadge.Name = "biBadge";
            this.biBadge.Size = new System.Drawing.Size(46, 49);
            this.biBadge.Text = "Badges";
            this.biBadge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.biBadge.Click += new System.EventHandler(this.ChoosePage);
            // 
            // biHood
            // 
            this.biHood.Image = ((System.Drawing.Image)(resources.GetObject("biHood.Image")));
            this.biHood.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.biHood.Name = "biHood";
            this.biHood.Size = new System.Drawing.Size(45, 49);
            this.biHood.Text = "N\'Hood";
            this.biHood.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.biHood.ToolTipText = "Neighbourhood Inventory";
            this.biHood.Click += new System.EventHandler(this.ChoosePage);
            // 
            // biDebug
            // 
            this.biDebug.Image = ((System.Drawing.Image)(resources.GetObject("biDebug.Image")));
            this.biDebug.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.biDebug.Name = "biDebug";
            this.biDebug.Size = new System.Drawing.Size(42, 49);
            this.biDebug.Text = "Debug";
            this.biDebug.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.biDebug.Click += new System.EventHandler(this.ChoosePage);
            // 
            // ExtNgbhUI
            // 
            this.Controls.Add(this.pnHood);
            this.Controls.Add(this.pnSims);
            this.Controls.Add(this.pnBadge);
            this.Controls.Add(this.pnDebug);
            this.Controls.Add(this.toolBar1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.HeaderText = "Sim Memory Editor";
            this.Name = "ExtNgbhUI";
            this.Size = new System.Drawing.Size(680, 368);
            this.Controls.SetChildIndex(this.toolBar1, 0);
            this.Controls.SetChildIndex(this.pnDebug, 0);
            this.Controls.SetChildIndex(this.pnBadge, 0);
            this.Controls.SetChildIndex(this.pnSims, 0);
            this.Controls.SetChildIndex(this.pnHood, 0);
            this.pnSims.ResumeLayout(false);
            this.pnSims.PerformLayout();
            this.menu.ResumeLayout(false);
            this.pnHood.ResumeLayout(false);
            this.pnHood.PerformLayout();
            this.tbChood.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.pnDebug.ResumeLayout(false);
            this.pnBadge.ResumeLayout(false);
            this.toolBar1.ResumeLayout(false);
            this.toolBar1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
        #endregion

		public ExtNgbh Exngbh
		{
            get { return (ExtNgbh)Wrapper; }
        }

        protected override void RefreshGUI()
        {
            simslot.NgbhResource = Exngbh;
            spc.Package = Exngbh.Package;
            spc_SelectedSimChanged(spc, null, null);
            nssel.NgbhResource = Exngbh;
            this.shelper.NgbhResource = Exngbh;
            Hoodpages();
		}

        private void pool_SelectedSimChanged(object p, object p_2, object p_3)
        {
            throw new Exception("The method or operation is not implemented.");
        }

		public override void OnCommit() { Exngbh.SynchronizeUserData(true, false); }

		public void SelectButton(ToolStripButton b)
		{
			for (int i=0; i<this.toolBar1.Items.Count; i++)
			{
				if (toolBar1.Items[i] is ToolStripButton ) 
				{
					ToolStripButton item = (ToolStripButton )toolBar1.Items[i];
					item.Checked = (item==b);					
					if (item.Tag!=null) 
					{
						Panel pn = (Panel)item.Tag;
						pn.Visible = item.Checked;
					}
				}
			}
			UpdateEnabledState();
		}

		void UpdateEnabledState() { }
		
		private void ChoosePage(object sender, System.EventArgs e)
		{
			SelectButton((ToolStripButton)sender);
            if (pnSims.Visible) pnSims.Controls.Add(this.spc);
            else if (pnBadge.Visible) pnBadge.Controls.Add(this.spc);
            else if (pnHood.Visible) tbChood_SelectedIndexChanged(sender, e);
		}

        private void spc_SelectedSimChanged(object sender, System.Drawing.Image thumb, SimPe.PackedFiles.Wrapper.SDesc sdesc)
        {
            if (spc.SelectedSim != null)
            {
                Collections.NgbhSlots slots = this.Exngbh.GetSlots(Data.NeighborhoodSlots.Sims);
                if (slots != null)
                {
                    NgbhSlot slot = slots.GetInstanceSlot(spc.SelectedSim.Instance);
                    if (slot == null)
                    {
                        slots.AddNew(spc.SelectedSim.Instance);
                    }
                }
            }
		}

		private void nssel_SelectedSlotChanged(object sender, System.EventArgs e)
		{
            nsui.Slut = nssel.SelectedSlot;
            nsui.Slit = nssel.SelectedSlot;
            nsui.Slot = nssel.SelectedSlot;
		}

		bool updateitems;
		private void shelper_AddedNewItem(object sender, System.EventArgs e)
		{
			updateitems = true;
		}
		private void shelper_ChangedItem(object sender, System.EventArgs e)
		{
			updateitems = true;
		}

		protected void RefreshContent()
		{
			nsui.Refresh();
            simslot.Refresh();
		}

		private void pnBadge_VisibleChanged(object sender, System.EventArgs e)
		{
			if (pnBadge.Visible) updateitems=false;
			else if (updateitems)
				RefreshContent();
        }

		#region Extensions by Theo
        void menu_VisibleChanged(object sender, EventArgs e)
        {
            miFix.Enabled = (this.Exngbh != null) && Helper.WindowsRegistry.HiddenMode;
            miNuke.Enabled = (spc.SelectedSim != null);
        }

		private void miNuke_Activate(object sender, System.EventArgs e)
		{
			if (spc.SelectedSim != null) 
			{
				Collections.NgbhSlots slots = this.Exngbh.GetSlots(Data.NeighborhoodSlots.Sims);
				if (slots!=null) 
				{
					NgbhSlot slot = slots.GetInstanceSlot(spc.SelectedSim.Instance);
					if (slot!=null)
					{
						slot.RemoveMyMemories();
						int deletedCount = slot.RemoveMemoriesAboutMe();
						if (deletedCount > 0)
                            SimPe.Message.Show(String.Format("Deleted {0} memories from the sim pool", deletedCount), "Advice", MessageBoxButtons.OK);					
						spc.Refresh();
					}
				}
			}
		}		

		private void miFix_Activate(object sender, System.EventArgs e)
		{
			EnhancedNgbh ngbh = this.Exngbh as EnhancedNgbh;
			if (ngbh!=null) 
			{
				ngbh.FixNeighborhoodMemories();
				this.RefreshGUI();
			}
        }
        #endregion

        #region Neighbourhood
        private NgbhItem Nitem
        {
            get { return nitem; }
            set { nitem = value; }
        }

        protected void AddItem(NgbhItem item, ListView lvw)
        {
            if (item == null) return;
            ListViewItem lvi = new ListViewItem();
            lvi.Text = item.ToString();
            lvi.Tag = item;
            if (item.MemoryCacheItem.Icon != null)
            {
                lvi.ImageIndex = memilist.Images.Count;
                memilist.Images.Add(item.MemoryCacheItem.Icon);
            }
            lvw.Items.Add(lvi);
        }

        private void Hoodpages()
        {
            Ngbh wrp = (Ngbh)Wrapper;
            memilist.Images.Clear();
            lvhood.Items.Clear();
            lvIntern.Items.Clear();
            lldel.Enabled = llSetRawLength.Enabled = false;
            pg.SelectedObject = null;
            int inx = wrp.PreItems.Length - 1;
            if (inx > -1)
            {
                foreach (NgbhItem item in wrp.PreItems[inx].ItemsB) AddItem(item, lvhood);
                foreach (NgbhItem item in wrp.PreItems[inx].ItemsA) AddItem(item, lvIntern);
            }
        }

        private void pg_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
        {
            if (nitem == null) return;
            string[] n = e.ChangedItem.Label.Split(new char[] { ':' }, 2);
            if (n.Length > 0)
            {
                int v = Helper.StringToInt32(n[0], -1, 16);
                if (v >= 0)
                {
                    nitem.PutValue(v, (ushort)((Ambertation.BaseChangeableNumber)e.ChangedItem.Value).Value);
                }
            }
            updatepreitem();
        }

        private void lvhood_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvhood.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvhood.SelectedItems[0];
                if (lvi != null)
                    Nitem = (NgbhItem)lvi.Tag;
                else
                    Nitem = null;
            }
            else
                Nitem = null;
            lldel.Enabled = Nitem != null;
            SetContent();
        }

        private void lvIntern_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvIntern.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvIntern.SelectedItems[0];
                if (lvi != null)
                    Nitem = (NgbhItem)lvi.Tag;
                else
                    Nitem = null;
            }
            else
                Nitem = null;
            lldel.Enabled = Nitem != null;
            SetContent();
        }

        private void SetContent()
        {
            pg.SelectedObject = null;
            if (nitem != null)
            {
                pg.Enabled = true;
                Hashtable ht = new Hashtable();
                byte ct = 0;
                foreach (string v in nitem.MemoryCacheItem.ValueNames)
                    ht[Helper.HexString(ct) + ": " + v] = new Ambertation.BaseChangeableNumber(nitem.GetValue(ct++));

                while (ct < nitem.Data.Length)
                    ht[Helper.HexString(ct) + ":"] = new Ambertation.BaseChangeableNumber(nitem.GetValue(ct++));

                Ambertation.PropertyObjectBuilderExt pob = new Ambertation.PropertyObjectBuilderExt(ht);

                pg.SelectedObject = pob.Instance;
                this.tbRawLength.Text = nitem.Data.Length.ToString();
            }
            else
            {
                pg.Enabled = false;
            }
            llSetRawLength.Enabled = false;
        }

        private void updatepreitem()
        {
            Ngbh wrp = (Ngbh)Wrapper;
            int inx = wrp.PreItems.Length - 1;
            tbdebug.Text = "No Good";
            if (tbChood.SelectedTab == tabPage1)
            {
                if (lvhood.SelectedItems.Count > 0)
                    wrp.PreItems[inx].ItemsB[lvhood.SelectedIndices[0]] = Nitem;
            }
            else
            {
                if (lvIntern.SelectedItems.Count > 0)
                    wrp.PreItems[inx].ItemsB[lvIntern.SelectedIndices[0]] = Nitem;
            }
        }

        private void llSetRawLength_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            if (Nitem != null)
            {
                ushort[] ndata = new ushort[Helper.StringToInt32(this.tbRawLength.Text, Nitem.Data.Length, 10)];
                for (int i = 0; i < ndata.Length; i++)
                    if (i < Nitem.Data.Length) ndata[i] = Nitem.Data[i];
                    else ndata[i] = 0;
                Nitem.Data = ndata;
                SetContent();
            }
        }

        private void lldel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            Ngbh wrp = (Ngbh)Wrapper;
            int inx = wrp.PreItems.Length - 1;
            if (tbChood.SelectedTab == tabPage1)
            {
                if (lvhood.SelectedItems.Count == 0 || Nitem == null) return;
                lvhood.Items.Remove(lvhood.SelectedItems[0]);
                wrp.PreItems[inx].ItemsB.Clear();
                if (lvhood.Items.Count > 0)
                    foreach (ListViewItem lvi in lvhood.Items)
                        wrp.PreItems[inx].ItemsB.Add((NgbhItem)lvi.Tag);
            }
            else
            {
                if (lvIntern.SelectedItems.Count == 0 || Nitem == null) return;
                lvIntern.Items.Remove(lvIntern.SelectedItems[0]);
                wrp.PreItems[inx].ItemsA.Clear();
                if (lvIntern.Items.Count > 0)
                    foreach (ListViewItem lvi in lvIntern.Items)
                        wrp.PreItems[inx].ItemsA.Add((NgbhItem)lvi.Tag);
            }
            tbChood_SelectedIndexChanged(null, null);
        }

        private void tbChood_SelectedIndexChanged(object sender, EventArgs e)
        {
            pg.SelectedObject = null;
            Nitem = null;
            tbRawLength.Text = "0";
            lldel.Enabled = llSetRawLength.Enabled = false;
        }

        private void tbRawLength_TextChanged(object sender, EventArgs e)
        {
            llSetRawLength.Enabled = true;
        }

        #endregion
    }
}
