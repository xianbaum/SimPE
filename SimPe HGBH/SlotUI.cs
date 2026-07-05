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

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for SlotUI.
	/// </summary>
	public class NgbhSlotUI : System.Windows.Forms.UserControl
	{
		private Ambertation.Windows.Forms.TabControl tabControl1;
		private Ambertation.Windows.Forms.TabPage tabPage1;
        private Ambertation.Windows.Forms.TabPage tabPage2;
        internal Ambertation.Windows.Forms.TabPage tabPage3;
        internal Ambertation.Windows.Forms.TabPage tabPage4;
        internal NgbhItemsListView lv;
        internal NgbhItemsListView lvint;
        private NgbhItemsListView lvfam;
        private NgbhItemsListView lvlot;
		private System.Windows.Forms.Splitter splitter1;
		private MemoryProperties memprop;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NgbhSlotUI()
		{
			SetStyle(
				ControlStyles.SupportsTransparentBackColor |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.UserPaint |
				ControlStyles.ResizeRedraw 
				| ControlStyles.DoubleBuffer
				,true);

			// Required designer variable.
			InitializeComponent();

			SlotType = Data.NeighborhoodSlots.Sims;
            tabPage2_VisibleChanged(null, null);

            if (Helper.WindowsRegistry.HiddenMode)
            {
                this.tabControl1.Controls.Remove(this.tabPage3);
                this.tabControl1.Controls.Remove(this.tabPage4);
            }
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if (disposing) { if (components != null) { components.Dispose(); } }
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl1 = new Ambertation.Windows.Forms.TabControl();
			// NetDocks TabControl is its own manager.
			// set it before adding pages so they render.
			this.tabControl1.Manager = this.tabControl1;
			this.tabPage1 = new Ambertation.Windows.Forms.TabPage();
			this.lv = new SimPe.Plugin.NgbhItemsListView();
			this.tabPage2 = new Ambertation.Windows.Forms.TabPage();
            this.lvint = new SimPe.Plugin.NgbhItemsListView();
            this.tabPage3 = new Ambertation.Windows.Forms.TabPage();
            this.lvfam = new SimPe.Plugin.NgbhItemsListView();
            this.tabPage4 = new Ambertation.Windows.Forms.TabPage();
            this.lvlot = new SimPe.Plugin.NgbhItemsListView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.memprop = new SimPe.Plugin.MemoryProperties();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.Size = new System.Drawing.Size(504, 165);
			this.tabControl1.TabIndex = 3;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.Transparent;
			this.tabPage1.Controls.Add(this.lv);
			this.tabPage1.FloatingSize = new System.Drawing.Size(550, 400);
			this.tabPage1.Location = new System.Drawing.Point(2, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(500, 141);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.TabText = "Memories";
			this.tabPage1.Text = "Memories";
			// 
			// lv
			// 
			this.lv.BackColor = System.Drawing.Color.Transparent;
			this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lv.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.lv.Location = new System.Drawing.Point(0, 0);
			this.lv.Name = "lv";
			this.lv.NgbhItems = null;
			this.lv.Size = new System.Drawing.Size(500, 141);
			this.lv.Slot = null;
            this.lv.ShowGossip = true;
			this.lv.SlotType = SimPe.Data.NeighborhoodSlots.Sims;
			this.lv.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.Color.Transparent;
			this.tabPage2.Controls.Add(this.lvint);
			this.tabPage2.FloatingSize = new System.Drawing.Size(550, 400);
			this.tabPage2.Location = new System.Drawing.Point(2, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(500, 117);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.TabText = "Tokens (Skills, Badges...)";
			this.tabPage2.Text = "Tokens (Skills, Badges...)";
			this.tabPage2.Visible = false;
			this.tabPage2.VisibleChanged += new System.EventHandler(this.tabPage2_VisibleChanged);
			// 
			// lvint
			// 
			this.lvint.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvint.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.lvint.Location = new System.Drawing.Point(0, 0);
			this.lvint.Name = "lvint";
			this.lvint.NgbhItems = null;
			this.lvint.Size = new System.Drawing.Size(500, 117);
            this.lvint.Slot = null;
			this.lvint.SlotType = SimPe.Data.NeighborhoodSlots.Sims;
            this.lvint.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Transparent;
            this.tabPage3.Controls.Add(this.lvfam);
            this.tabPage3.FloatingSize = new System.Drawing.Size(550, 400);
            this.tabPage3.Location = new System.Drawing.Point(2, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(500, 117);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.TabText = "Family Inventory";
            this.tabPage3.Text = "Family Inventory";
            this.tabPage3.Visible = false;
            this.tabPage3.VisibleChanged += new System.EventHandler(this.tabPage2_VisibleChanged);
            // 
            // lvfam
            // 
            this.lvfam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvfam.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lvfam.Location = new System.Drawing.Point(0, 0);
            this.lvfam.Name = "lvfam";
            this.lvfam.NgbhItems = null;
            this.lvfam.Size = new System.Drawing.Size(500, 117);
            this.lvfam.Slot = null;
            this.lvfam.SlotType = SimPe.Data.NeighborhoodSlots.Families;
            this.lvfam.TabIndex = 2;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Transparent;
            this.tabPage4.Controls.Add(this.lvlot);
            this.tabPage4.FloatingSize = new System.Drawing.Size(550, 400);
            this.tabPage4.Location = new System.Drawing.Point(2, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(500, 117);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.TabText = "Lot Inventory";
            this.tabPage4.Text = "Lot Inventory";
            this.tabPage4.Visible = false;
            this.tabPage4.VisibleChanged += new System.EventHandler(this.tabPage2_VisibleChanged);
            // 
            // lvlot
            // 
            this.lvlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvlot.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lvlot.Location = new System.Drawing.Point(0, 0);
            this.lvlot.Name = "lvlot";
            this.lvlot.NgbhItems = null;
            this.lvlot.Size = new System.Drawing.Size(500, 117);
            this.lvlot.Slot = null;
            this.lvlot.SlotType = SimPe.Data.NeighborhoodSlots.Lots;
            this.lvlot.TabIndex = 5;
			// 
			// splitter1
			// 
			this.splitter1.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter1.Location = new System.Drawing.Point(0, 165);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(504, 3);
			this.splitter1.TabIndex = 3;
			this.splitter1.TabStop = false;
			// 
			// memprop
			// 
			this.memprop.BackColor = System.Drawing.Color.Transparent;
			this.memprop.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.memprop.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.memprop.Item = null;
			this.memprop.Location = new System.Drawing.Point(0, 168);
			this.memprop.Name = "memprop";
			this.memprop.NgbhItemsListView = null;
			this.memprop.Size = new System.Drawing.Size(504, 192);
			this.memprop.TabIndex = 4;
			// 
			// NgbhSlotUI
			// 
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.memprop);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "NgbhSlotUI";
			this.Size = new System.Drawing.Size(504, 360);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
			this.ResumeLayout(false);
			this.tabPage1.EnsureVisible();

		}
		#endregion

		#region Properties		
        Data.NeighborhoodSlots st;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Data.NeighborhoodSlots SlotType 
		{
			get {return st;}
			set 
			{
				st = value;
				lv.NgbhItems = null;
                lvint.NgbhItems = null;
                lvfam.NgbhItems = null;
                lvlot.NgbhItems = null;
                lvfam.SlotType = SimPe.Data.NeighborhoodSlots.Families;
                lvlot.SlotType = SimPe.Data.NeighborhoodSlots.Lots;
				if (st== SimPe.Data.NeighborhoodSlots.Sims || st==SimPe.Data.NeighborhoodSlots.SimsIntern) 
				{
					this.tabPage1.Text = SimPe.Localization.GetString("SimPe.Data.NeighborhoodSlots.Sims");
					this.tabPage2.Text = SimPe.Localization.GetString("SimPe.Data.NeighborhoodSlots.SimsIntern");
					
                    lv.SlotType = SimPe.Data.NeighborhoodSlots.Sims;
                    lvint.SlotType = SimPe.Data.NeighborhoodSlots.SimsIntern;
				} 
				else if (st== SimPe.Data.NeighborhoodSlots.Families || st==SimPe.Data.NeighborhoodSlots.FamiliesIntern) 
				{
					this.tabPage1.Text = SimPe.Localization.GetString("SimPe.Data.NeighborhoodSlots.Families");
					this.tabPage2.Text = SimPe.Localization.GetString("SimPe.Data.NeighborhoodSlots.FamiliesIntern");
					
					lv.SlotType = SimPe.Data.NeighborhoodSlots.Families;
					lvint.SlotType = SimPe.Data.NeighborhoodSlots.FamiliesIntern;
				}
                else if (st == SimPe.Data.NeighborhoodSlots.Lots || st == SimPe.Data.NeighborhoodSlots.LotsIntern) 
				{
					this.tabPage1.Text = SimPe.Localization.GetString("SimPe.Data.NeighborhoodSlots.Lots");
					this.tabPage2.Text = SimPe.Localization.GetString("SimPe.Data.NeighborhoodSlots.LotsIntern");
					
					lv.SlotType = SimPe.Data.NeighborhoodSlots.Lots;
					lvint.SlotType = SimPe.Data.NeighborhoodSlots.LotsIntern;
                }
				this.tabPage1.TabText = this.tabPage1.Text;
				this.tabPage2.TabText = this.tabPage2.Text;

				SetContent();
			}			
		}

        NgbhSlot slut;
        NgbhSlot slit;
        NgbhSlot slot;

        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NgbhSlot Slut
        {
            get { return slut; }
            set { slut = value; }
        }

        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NgbhSlot Slit
        {
            get { return slit; }
            set { slit = value; }
        }

        [System.ComponentModel.Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NgbhSlot Slot
        {
            get { return slot; }
            set { slot = value; SetContent(); }
        }

		Ngbh ngbh;
		[System.ComponentModel.Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Ngbh NgbhResource
		{
			get {return ngbh;}
			set 
			{
				ngbh = value;
				SetContent();
				pc_SelectedSimChanged(pc, null, null);
			}
		}

		SimPe.PackedFiles.Wrapper.SimPoolControl pc;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SimPe.PackedFiles.Wrapper.SimPoolControl SimPoolControl
		{
			get {return pc;}
			set {
				if (pc!=null) pc.SelectedSimChanged -= new SimPe.PackedFiles.Wrapper.SimPoolControl.SelectedSimHandler(pc_SelectedSimChanged);
				pc = value;
				if (pc!=null) 
				{
					pc.SelectedSimChanged += new SimPe.PackedFiles.Wrapper.SimPoolControl.SelectedSimHandler(pc_SelectedSimChanged);
					pc_SelectedSimChanged(pc, null, null);
				}
			}
		}

		#endregion

		void SetContent()
        {
			lv.Slot = slot;
            lvint.Slot = slot;
            lvfam.Slot = slut;
            lvlot.Slot = slit;
		}

		public new void Refresh()
        {
            lvlot.Refresh();
            lvfam.Refresh();
            lvint.Refresh();
			lv.Refresh();
			base.Refresh();
		}

        private void pc_SelectedSimChanged(object sender, Image thumb, SimPe.PackedFiles.Wrapper.SDesc sdesc)
        {
            if (ngbh != null && pc != null)
            {
                if (pc.SelectedSim != null)
                {
                    if (Helper.WindowsRegistry.HiddenMode)
                    {
                        this.Slut = null;
                        this.Slit = null;
                    }
                    else
                    {
                        this.Slut = ngbh.GetSlots(SimPe.Data.NeighborhoodSlots.Families).GetInstanceSlot(pc.SelectedSim.FamilyInstance);
                        if (pc.SelectedSim.HouseNumba == 0) this.Slit = null;
                        else this.Slit = ngbh.GetSlots(SimPe.Data.NeighborhoodSlots.Lots).GetInstanceSlot(pc.SelectedSim.HouseNumba);
                    }
                    this.Slot = ngbh.GetSlots(st).GetInstanceSlot(pc.SelectedSim.FileDescriptor.Instance);
                }
                else
                {
                    this.Slut = null;
                    this.Slit = null;
                    this.Slot = null;
                }
            }
        }

		private void tabPage2_VisibleChanged(object sender, System.EventArgs e)
		{
			if (tabControl1.Highlight == this.tabPage1)
				memprop.NgbhItemsListView = lv;
            else if (tabControl1.Highlight == this.tabPage3)
                memprop.NgbhItemsListView = lvfam;
            else if (tabControl1.Highlight == this.tabPage4)
                memprop.NgbhItemsListView = lvlot;
			else
				memprop.NgbhItemsListView = lvint;
		}
	}
}
