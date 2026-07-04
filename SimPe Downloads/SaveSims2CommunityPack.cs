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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace SimPe.Packages
{
	/// <summary>
	/// Summary description for SaveSims2CommunityPack.
	/// </summary>
	internal class SaveSims2CommunityPack : System.Windows.Forms.Form
    {
        private booby.gradientpanel panel1;
        private booby.gradientpanel panel2;
        private Panel panel3;
        private ListBox lblist;
        private ListView filesList;
        private GroupBox gbsettings;
        private GroupBox gbIsettings;
        private TextBox tbname;
        private TextBox tbauthor;
        private TextBox tbguid;
        private TextBox tbver;
        private TextBox tbvalid;
        private TextBox tbdesc;
        private TextBox tbcontact;
        private TextBox tbgameguid;
        private TextBox tbtitle;
        private TextBox tbItitle;
        private TextBox tbIdesc;
        private TextBox tbIname;
        private ComboBox cbcompress;
        private CheckBox cb2cp;
        private CheckBox cbadnoo;
        private Button btdelete;
        private Button btcancel;
        private Button btadd;
        private Button btbrowse;
        private Button btsave;
        private Button btinstall;
        private Button bticancel;
        private Button btgoforit;
        private Button btcleanim;
		private Label label1;
		private Label label2;
		private Label label3;
		private Label label4;
		private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label lbtipe;
        private Label lbwarnim;
        private Label lboverr;
        private Label lbgloby;
        private Label lbbadnpc;
        private Label lbinfeted;
        private Label tlbItitle;
        private Label lbIdesc;
        private Label lbIname;
        private LinkLabel lldep;
        private OpenFileDialog ofd;
		private SaveFileDialog sfd;
        private ColumnHeader columnHeaderCheckBox;
        private ColumnHeader columnHeaderName;
        private ColumnHeader columnHeaderType;
        private ColumnHeader columnHeaderSubfolder;
        private ColumnHeader columnHeaderTitle;
        internal TextBox tbflname;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SaveSims2CommunityPack()
		{
			//
			// Required designer variable.
			//
            InitializeComponent();
            booby.ThemeManager tm = booby.ThemeManager.Global.CreateChild();
            tm.AddControl(this.panel1);
            tm.AddControl(this.panel2);
            if (booby.ThemeManager.ThemedForms)
            {
                tm.AddControl(this.lblist);
                tm.AddControl(this.filesList);
                tm.AddControl(this.tbdesc);
                tm.AddControl(this.tbIdesc);
                tm.AddControl(this.btdelete);
                tm.AddControl(this.btcancel);
                tm.AddControl(this.btadd);
                tm.AddControl(this.btbrowse);
                tm.AddControl(this.btsave);
                tm.AddControl(this.btinstall);
                tm.AddControl(this.btgoforit);
                tm.AddControl(this.bticancel);
                tm.AddControl(this.btcleanim);
                tm.AddControl(this.cbcompress);
                this.lbtipe.ForeColor = booby.ThemeManager.Global.ThemeColorDark;
            }
            if (Helper.WindowsRegistry.UseBigIcons)
            {
                this.lblist.Font = new System.Drawing.Font("Verdana", 9.75F);
                this.filesList.Font = new System.Drawing.Font("Verdana", 9.75F);
            }
            if (booby.PrettyGirls.PrittyBabe == null) this.BackgroundImage = GetImage.GetrandomSim();
            else this.panel3.BackgroundImage = booby.PrettyGirls.RandomGirl;

			this.cbcompress.Items.Add(Sims2CommunityPack.CompressionStrength.Slowest);
			this.cbcompress.Items.Add(Sims2CommunityPack.CompressionStrength.Slower);
			this.cbcompress.Items.Add(Sims2CommunityPack.CompressionStrength.Slow);
			this.cbcompress.Items.Add(Sims2CommunityPack.CompressionStrength.Default);
			this.cbcompress.Items.Add(Sims2CommunityPack.CompressionStrength.Fast);
			this.cbcompress.Items.Add(Sims2CommunityPack.CompressionStrength.Faster);
			this.cbcompress.Items.Add(Sims2CommunityPack.CompressionStrength.Fastest);
			this.cbcompress.Items.Add(Sims2CommunityPack.CompressionStrength.None);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
            if (disposing) { if (components != null) { components.Dispose(); } }
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveSims2CommunityPack));
            this.panel1 = new booby.gradientpanel();
            this.panel2 = new booby.gradientpanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btcleanim = new System.Windows.Forms.Button();
            this.lbtipe = new System.Windows.Forms.Label();
            this.lbwarnim = new System.Windows.Forms.Label();
            this.filesList = new System.Windows.Forms.ListView();
            this.columnHeaderCheckBox = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderType = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderSubfolder = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderTitle = new System.Windows.Forms.ColumnHeader();
            this.lboverr = new System.Windows.Forms.Label();
            this.lbgloby = new System.Windows.Forms.Label();
            this.lbbadnpc = new System.Windows.Forms.Label();
            this.lbinfeted = new System.Windows.Forms.Label();
            this.cbadnoo = new System.Windows.Forms.CheckBox();
            this.bticancel = new System.Windows.Forms.Button();
            this.btgoforit = new System.Windows.Forms.Button();
            this.btinstall = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btcancel = new System.Windows.Forms.Button();
            this.btsave = new System.Windows.Forms.Button();
            this.btdelete = new System.Windows.Forms.Button();
            this.btbrowse = new System.Windows.Forms.Button();
            this.tbflname = new System.Windows.Forms.TextBox();
            this.btadd = new System.Windows.Forms.Button();
            this.gbsettings = new System.Windows.Forms.GroupBox();
            this.tbtitle = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbgameguid = new System.Windows.Forms.TextBox();
            this.tbcontact = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbcompress = new System.Windows.Forms.ComboBox();
            this.tbdesc = new System.Windows.Forms.TextBox();
            this.tbver = new System.Windows.Forms.TextBox();
            this.tbvalid = new System.Windows.Forms.TextBox();
            this.tbguid = new System.Windows.Forms.TextBox();
            this.tbauthor = new System.Windows.Forms.TextBox();
            this.tbname = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lldep = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.gbIsettings = new System.Windows.Forms.GroupBox();
            this.tbItitle = new System.Windows.Forms.TextBox();
            this.tlbItitle = new System.Windows.Forms.Label();
            this.tbIdesc = new System.Windows.Forms.TextBox();
            this.tbIname = new System.Windows.Forms.TextBox();
            this.lbIdesc = new System.Windows.Forms.Label();
            this.lbIname = new System.Windows.Forms.Label();
            this.lblist = new System.Windows.Forms.ListBox();
            this.cb2cp = new System.Windows.Forms.CheckBox();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.gbsettings.SuspendLayout();
            this.gbIsettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btinstall);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btcancel);
            this.panel1.Controls.Add(this.btsave);
            this.panel1.Controls.Add(this.btdelete);
            this.panel1.Controls.Add(this.btbrowse);
            this.panel1.Controls.Add(this.tbflname);
            this.panel1.Controls.Add(this.btadd);
            this.panel1.Controls.Add(this.lblist);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.gbsettings);
            this.panel1.Controls.Add(this.gbIsettings);
            this.panel1.Controls.Add(this.cb2cp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(794, 560);
            this.panel1.TabIndex = 18;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btcleanim);
            this.panel2.Controls.Add(this.lbtipe);
            this.panel2.Controls.Add(this.lbwarnim);
            this.panel2.Controls.Add(this.filesList);
            this.panel2.Controls.Add(this.lboverr);
            this.panel2.Controls.Add(this.lbgloby);
            this.panel2.Controls.Add(this.lbbadnpc);
            this.panel2.Controls.Add(this.lbinfeted);
            this.panel2.Controls.Add(this.cbadnoo);
            this.panel2.Controls.Add(this.bticancel);
            this.panel2.Controls.Add(this.btgoforit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(794, 560);
            this.panel2.TabIndex = 20;
            this.panel2.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel3.Location = new System.Drawing.Point(0, 34);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(794, 292);
            this.panel3.TabIndex = 21;
            this.panel3.Visible = false;
            // 
            // btcleanim
            // 
            this.btcleanim.Location = new System.Drawing.Point(6, 32);
            this.btcleanim.Name = "btcleanim";
            this.btcleanim.Size = new System.Drawing.Size(100, 23);
            this.btcleanim.TabIndex = 21;
            this.btcleanim.Text = "Key Files Only";
            this.btcleanim.UseVisualStyleBackColor = true;
            this.btcleanim.Click += new System.EventHandler(this.btcleanim_Click);
            // 
            // lbtipe
            // 
            this.lbtipe.AutoSize = true;
            this.lbtipe.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtipe.Location = new System.Drawing.Point(2, 4);
            this.lbtipe.Name = "lbtipe";
            this.lbtipe.Size = new System.Drawing.Size(159, 20);
            this.lbtipe.TabIndex = 15;
            this.lbtipe.Text = "File Type: Object";
            // 
            // lbwarnim
            // 
            this.lbwarnim.AutoSize = true;
            this.lbwarnim.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbwarnim.ForeColor = System.Drawing.Color.Crimson;
            this.lbwarnim.Location = new System.Drawing.Point(222, 5);
            this.lbwarnim.Name = "lbwarnim";
            this.lbwarnim.Size = new System.Drawing.Size(399, 32);
            this.lbwarnim.TabIndex = 16;
            this.lbwarnim.Text = "Custom NPC; Any Neighbourhood that runs with this\r\ninstalled will be subject to c" +
                "orruption if you remove it.";
            this.lbwarnim.Visible = false;
            // 
            // filesList
            // 
            this.filesList.AllowColumnReorder = true;
            this.filesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.filesList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.filesList.CheckBoxes = true;
            this.filesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderCheckBox,
            this.columnHeaderName,
            this.columnHeaderType,
            this.columnHeaderSubfolder,
            this.columnHeaderTitle});
            this.filesList.FullRowSelect = true;
            this.filesList.HideSelection = false;
            this.filesList.LabelWrap = false;
            this.filesList.Location = new System.Drawing.Point(3, 61);
            this.filesList.Name = "filesList";
            this.filesList.Size = new System.Drawing.Size(788, 463);
            this.filesList.TabIndex = 12;
            this.filesList.UseCompatibleStateImageBehavior = false;
            this.filesList.View = System.Windows.Forms.View.Details;
            this.filesList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.Handle_ListView_ColumnClick);
            // 
            // columnHeaderCheckBox
            // 
            this.columnHeaderCheckBox.Text = "";
            this.columnHeaderCheckBox.Width = 20;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 355;
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "Type";
            this.columnHeaderType.Width = 132;
            // 
            // columnHeaderSubfolder
            // 
            this.columnHeaderSubfolder.Text = "Install Folder";
            this.columnHeaderSubfolder.Width = 100;
            // 
            // columnHeaderTitle
            // 
            this.columnHeaderTitle.Text = "Title";
            this.columnHeaderTitle.Width = 162;
            // 
            // lboverr
            // 
            this.lboverr.AutoSize = true;
            this.lboverr.ForeColor = System.Drawing.Color.SlateBlue;
            this.lboverr.Location = new System.Drawing.Point(402, 44);
            this.lboverr.Name = "lboverr";
            this.lboverr.Size = new System.Drawing.Size(111, 13);
            this.lboverr.TabIndex = 20;
            this.lboverr.Text = "File Already Exists";
            // 
            // lbgloby
            // 
            this.lbgloby.AutoSize = true;
            this.lbgloby.ForeColor = System.Drawing.Color.DarkRed;
            this.lbgloby.Location = new System.Drawing.Point(283, 44);
            this.lbgloby.Name = "lbgloby";
            this.lbgloby.Size = new System.Drawing.Size(107, 13);
            this.lbgloby.TabIndex = 19;
            this.lbgloby.Text = "Potential Problem";
            // 
            // lbbadnpc
            // 
            this.lbbadnpc.AutoSize = true;
            this.lbbadnpc.ForeColor = System.Drawing.Color.Red;
            this.lbbadnpc.Location = new System.Drawing.Point(192, 44);
            this.lbbadnpc.Name = "lbbadnpc";
            this.lbbadnpc.Size = new System.Drawing.Size(79, 13);
            this.lbbadnpc.TabIndex = 18;
            this.lbbadnpc.Text = "Custom NPC";
            // 
            // lbinfeted
            // 
            this.lbinfeted.AutoSize = true;
            this.lbinfeted.ForeColor = System.Drawing.Color.MediumVioletRed;
            this.lbinfeted.Location = new System.Drawing.Point(525, 44);
            this.lbinfeted.Name = "lbinfeted";
            this.lbinfeted.Size = new System.Drawing.Size(106, 13);
            this.lbinfeted.TabIndex = 24;
            this.lbinfeted.Text = "Hug Bug Infected";
            // 
            // cbadnoo
            // 
            this.cbadnoo.AutoSize = true;
            this.cbadnoo.Checked = true;
            this.cbadnoo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbadnoo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbadnoo.Location = new System.Drawing.Point(3, 535);
            this.cbadnoo.Name = "cbadnoo";
            this.cbadnoo.Size = new System.Drawing.Size(259, 20);
            this.cbadnoo.TabIndex = 17;
            this.cbadnoo.Text = "Create a New Downloads Subfolder";
            this.cbadnoo.UseVisualStyleBackColor = true;
            this.cbadnoo.CheckedChanged += new System.EventHandler(this.cbadnoo_CheckedChanged);
            // 
            // bticancel
            // 
            this.bticancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bticancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bticancel.Location = new System.Drawing.Point(601, 530);
            this.bticancel.Name = "bticancel";
            this.bticancel.Size = new System.Drawing.Size(75, 23);
            this.bticancel.TabIndex = 14;
            this.bticancel.Text = "Cancel";
            this.bticancel.UseVisualStyleBackColor = true;
            this.bticancel.Click += new System.EventHandler(this.bticancel_Click);
            // 
            // btgoforit
            // 
            this.btgoforit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgoforit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btgoforit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btgoforit.Location = new System.Drawing.Point(682, 530);
            this.btgoforit.Name = "btgoforit";
            this.btgoforit.Size = new System.Drawing.Size(102, 23);
            this.btgoforit.TabIndex = 13;
            this.btgoforit.Text = "Install Now";
            this.btgoforit.UseVisualStyleBackColor = true;
            this.btgoforit.Click += new System.EventHandler(this.btgoforit_Click);
            // 
            // btinstall
            // 
            this.btinstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btinstall.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btinstall.Location = new System.Drawing.Point(520, 530);
            this.btinstall.Name = "btinstall";
            this.btinstall.Size = new System.Drawing.Size(75, 23);
            this.btinstall.TabIndex = 19;
            this.btinstall.Text = "Install";
            this.btinstall.UseVisualStyleBackColor = true;
            this.btinstall.Visible = false;
            this.btinstall.Click += new System.EventHandler(this.btinstall_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(5, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "FileName:";
            // 
            // btcancel
            // 
            this.btcancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btcancel.BackColor = System.Drawing.Color.Transparent;
            this.btcancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btcancel.Location = new System.Drawing.Point(601, 530);
            this.btcancel.Name = "btcancel";
            this.btcancel.Size = new System.Drawing.Size(75, 23);
            this.btcancel.TabIndex = 17;
            this.btcancel.Text = "Cancel";
            this.btcancel.UseVisualStyleBackColor = true;
            this.btcancel.Click += new System.EventHandler(this.btcancel_Click);
            // 
            // btsave
            // 
            this.btsave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btsave.BackColor = System.Drawing.Color.Transparent;
            this.btsave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btsave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btsave.Location = new System.Drawing.Point(682, 530);
            this.btsave.Name = "btsave";
            this.btsave.Size = new System.Drawing.Size(102, 23);
            this.btsave.TabIndex = 16;
            this.btsave.Text = "Save";
            this.btsave.UseVisualStyleBackColor = true;
            this.btsave.Click += new System.EventHandler(this.button3_Click);
            // 
            // btdelete
            // 
            this.btdelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btdelete.BackColor = System.Drawing.Color.Transparent;
            this.btdelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btdelete.Location = new System.Drawing.Point(703, 222);
            this.btdelete.Name = "btdelete";
            this.btdelete.Size = new System.Drawing.Size(75, 23);
            this.btdelete.TabIndex = 3;
            this.btdelete.Text = "Delete...";
            this.btdelete.UseVisualStyleBackColor = true;
            this.btdelete.Click += new System.EventHandler(this.DeletePackage);
            // 
            // btbrowse
            // 
            this.btbrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btbrowse.BackColor = System.Drawing.Color.Transparent;
            this.btbrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btbrowse.Location = new System.Drawing.Point(703, 8);
            this.btbrowse.Name = "btbrowse";
            this.btbrowse.Size = new System.Drawing.Size(75, 23);
            this.btbrowse.TabIndex = 1;
            this.btbrowse.Text = "Browse...";
            this.btbrowse.UseVisualStyleBackColor = true;
            this.btbrowse.Click += new System.EventHandler(this.S2CPFilename);
            // 
            // tbflname
            // 
            this.tbflname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbflname.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbflname.Location = new System.Drawing.Point(80, 8);
            this.tbflname.Name = "tbflname";
            this.tbflname.Size = new System.Drawing.Size(613, 21);
            this.tbflname.TabIndex = 0;
            // 
            // btadd
            // 
            this.btadd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btadd.BackColor = System.Drawing.Color.Transparent;
            this.btadd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btadd.Location = new System.Drawing.Point(618, 222);
            this.btadd.Name = "btadd";
            this.btadd.Size = new System.Drawing.Size(72, 23);
            this.btadd.TabIndex = 4;
            this.btadd.Text = "Add...";
            this.btadd.UseVisualStyleBackColor = true;
            this.btadd.Click += new System.EventHandler(this.AddPackage);
            // 
            // gbsettings
            // 
            this.gbsettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbsettings.BackColor = System.Drawing.Color.Transparent;
            this.gbsettings.Controls.Add(this.tbtitle);
            this.gbsettings.Controls.Add(this.label10);
            this.gbsettings.Controls.Add(this.tbgameguid);
            this.gbsettings.Controls.Add(this.tbcontact);
            this.gbsettings.Controls.Add(this.label8);
            this.gbsettings.Controls.Add(this.cbcompress);
            this.gbsettings.Controls.Add(this.tbdesc);
            this.gbsettings.Controls.Add(this.tbver);
            this.gbsettings.Controls.Add(this.tbvalid);
            this.gbsettings.Controls.Add(this.tbguid);
            this.gbsettings.Controls.Add(this.tbauthor);
            this.gbsettings.Controls.Add(this.tbname);
            this.gbsettings.Controls.Add(this.label5);
            this.gbsettings.Controls.Add(this.label4);
            this.gbsettings.Controls.Add(this.label3);
            this.gbsettings.Controls.Add(this.label2);
            this.gbsettings.Controls.Add(this.label11);
            this.gbsettings.Controls.Add(this.label1);
            this.gbsettings.Controls.Add(this.lldep);
            this.gbsettings.Controls.Add(this.label7);
            this.gbsettings.Controls.Add(this.label9);
            this.gbsettings.Enabled = false;
            this.gbsettings.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbsettings.Location = new System.Drawing.Point(8, 248);
            this.gbsettings.Name = "gbsettings";
            this.gbsettings.Size = new System.Drawing.Size(778, 278);
            this.gbsettings.TabIndex = 1;
            this.gbsettings.TabStop = false;
            this.gbsettings.Text = "Settings";
            // 
            // tbtitle
            // 
            this.tbtitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbtitle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbtitle.Location = new System.Drawing.Point(72, 48);
            this.tbtitle.Name = "tbtitle";
            this.tbtitle.Size = new System.Drawing.Size(698, 21);
            this.tbtitle.TabIndex = 7;
            this.tbtitle.TextChanged += new System.EventHandler(this.ChangeText);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(30, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Title:";
            // 
            // tbgameguid
            // 
            this.tbgameguid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbgameguid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbgameguid.Location = new System.Drawing.Point(424, 96);
            this.tbgameguid.Name = "tbgameguid";
            this.tbgameguid.ReadOnly = true;
            this.tbgameguid.Size = new System.Drawing.Size(346, 21);
            this.tbgameguid.TabIndex = 11;
            // 
            // tbcontact
            // 
            this.tbcontact.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcontact.Location = new System.Drawing.Point(72, 96);
            this.tbcontact.Name = "tbcontact";
            this.tbcontact.Size = new System.Drawing.Size(257, 21);
            this.tbcontact.TabIndex = 10;
            this.tbcontact.TextChanged += new System.EventHandler(this.ChangeText);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(11, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Contact:";
            // 
            // cbcompress
            // 
            this.cbcompress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbcompress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcompress.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbcompress.Location = new System.Drawing.Point(626, 72);
            this.cbcompress.Name = "cbcompress";
            this.cbcompress.Size = new System.Drawing.Size(144, 21);
            this.cbcompress.TabIndex = 9;
            this.cbcompress.SelectedIndexChanged += new System.EventHandler(this.SelectCompression);
            // 
            // tbdesc
            // 
            this.tbdesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbdesc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbdesc.Location = new System.Drawing.Point(6, 168);
            this.tbdesc.Multiline = true;
            this.tbdesc.Name = "tbdesc";
            this.tbdesc.Size = new System.Drawing.Size(764, 76);
            this.tbdesc.TabIndex = 14;
            this.tbdesc.TextChanged += new System.EventHandler(this.ChangeText);
            // 
            // tbver
            // 
            this.tbver.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbver.Location = new System.Drawing.Point(118, 124);
            this.tbver.Name = "tbver";
            this.tbver.Size = new System.Drawing.Size(72, 21);
            this.tbver.TabIndex = 12;
            this.tbver.TextChanged += new System.EventHandler(this.ChangeText);
            // 
            // tbvalid
            // 
            this.tbvalid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbvalid.Location = new System.Drawing.Point(608, 250);
            this.tbvalid.Name = "tbvalid";
            this.tbvalid.ReadOnly = true;
            this.tbvalid.Size = new System.Drawing.Size(160, 21);
            this.tbvalid.TabIndex = 12;
            // 
            // tbguid
            // 
            this.tbguid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbguid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbguid.Location = new System.Drawing.Point(424, 128);
            this.tbguid.Name = "tbguid";
            this.tbguid.ReadOnly = true;
            this.tbguid.Size = new System.Drawing.Size(346, 21);
            this.tbguid.TabIndex = 13;
            // 
            // tbauthor
            // 
            this.tbauthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbauthor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbauthor.Location = new System.Drawing.Point(72, 72);
            this.tbauthor.Name = "tbauthor";
            this.tbauthor.Size = new System.Drawing.Size(450, 21);
            this.tbauthor.TabIndex = 8;
            this.tbauthor.TextChanged += new System.EventHandler(this.ChangeText);
            // 
            // tbname
            // 
            this.tbname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbname.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbname.Location = new System.Drawing.Point(72, 24);
            this.tbname.Name = "tbname";
            this.tbname.Size = new System.Drawing.Size(698, 21);
            this.tbname.TabIndex = 6;
            this.tbname.TextChanged += new System.EventHandler(this.ChangeText);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Description:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(339, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "GlobalGUID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Object Version:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Author:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(562, 254);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "State:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // lldep
            // 
            this.lldep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lldep.AutoSize = true;
            this.lldep.LinkArea = new System.Windows.Forms.LinkArea(7, 12);
            this.lldep.Location = new System.Drawing.Point(16, 254);
            this.lldep.Name = "lldep";
            this.lldep.Size = new System.Drawing.Size(155, 18);
            this.lldep.TabIndex = 15;
            this.lldep.TabStop = true;
            this.lldep.Text = "show 0 Dependencies...";
            this.lldep.UseCompatibleTextRendering = true;
            this.lldep.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ShowDependencies);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(533, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Compression:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(335, 99);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "GameGUIDs:";
            // 
            // gbIsettings
            // 
            this.gbIsettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbIsettings.BackColor = System.Drawing.Color.Transparent;
            this.gbIsettings.Controls.Add(this.tbItitle);
            this.gbIsettings.Controls.Add(this.tlbItitle);
            this.gbIsettings.Controls.Add(this.tbIdesc);
            this.gbIsettings.Controls.Add(this.tbIname);
            this.gbIsettings.Controls.Add(this.lbIdesc);
            this.gbIsettings.Controls.Add(this.lbIname);
            this.gbIsettings.Enabled = false;
            this.gbIsettings.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbIsettings.Location = new System.Drawing.Point(8, 353);
            this.gbIsettings.Name = "gbIsettings";
            this.gbIsettings.Size = new System.Drawing.Size(778, 173);
            this.gbIsettings.TabIndex = 20;
            this.gbIsettings.TabStop = false;
            this.gbIsettings.Text = "Settings";
            this.gbIsettings.Visible = false;
            // 
            // tbItitle
            // 
            this.tbItitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbItitle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbItitle.Location = new System.Drawing.Point(72, 48);
            this.tbItitle.Name = "tbItitle";
            this.tbItitle.Size = new System.Drawing.Size(698, 21);
            this.tbItitle.TabIndex = 7;
            this.tbItitle.TextChanged += new System.EventHandler(this.ChangeText);
            // 
            // tlbItitle
            // 
            this.tlbItitle.AutoSize = true;
            this.tlbItitle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbItitle.Location = new System.Drawing.Point(30, 52);
            this.tlbItitle.Name = "tlbItitle";
            this.tlbItitle.Size = new System.Drawing.Size(36, 13);
            this.tlbItitle.TabIndex = 17;
            this.tlbItitle.Text = "Title:";
            // 
            // tbIdesc
            // 
            this.tbIdesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbIdesc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbIdesc.Location = new System.Drawing.Point(6, 88);
            this.tbIdesc.Multiline = true;
            this.tbIdesc.Name = "tbIdesc";
            this.tbIdesc.Size = new System.Drawing.Size(764, 76);
            this.tbIdesc.TabIndex = 14;
            this.tbIdesc.TextChanged += new System.EventHandler(this.ChangeText);
            // 
            // tbIname
            // 
            this.tbIname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbIname.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbIname.Location = new System.Drawing.Point(72, 24);
            this.tbIname.Name = "tbIname";
            this.tbIname.Size = new System.Drawing.Size(698, 21);
            this.tbIname.TabIndex = 6;
            this.tbIname.TextChanged += new System.EventHandler(this.ChangeText);
            // 
            // lbIdesc
            // 
            this.lbIdesc.AutoSize = true;
            this.lbIdesc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIdesc.Location = new System.Drawing.Point(6, 72);
            this.lbIdesc.Name = "lbIdesc";
            this.lbIdesc.Size = new System.Drawing.Size(76, 13);
            this.lbIdesc.TabIndex = 4;
            this.lbIdesc.Text = "Description:";
            // 
            // lbIname
            // 
            this.lbIname.AutoSize = true;
            this.lbIname.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIname.Location = new System.Drawing.Point(22, 28);
            this.lbIname.Name = "lbIname";
            this.lbIname.Size = new System.Drawing.Size(45, 13);
            this.lbIname.TabIndex = 0;
            this.lbIname.Text = "Name:";
            // 
            // lblist
            // 
            this.lblist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblist.IntegralHeight = false;
            this.lblist.Location = new System.Drawing.Point(8, 40);
            this.lblist.Name = "lblist";
            this.lblist.Size = new System.Drawing.Size(778, 180);
            this.lblist.TabIndex = 2;
            this.lblist.SelectedIndexChanged += new System.EventHandler(this.Select);
            // 
            // cb2cp
            // 
            this.cb2cp.AutoSize = true;
            this.cb2cp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cb2cp.Location = new System.Drawing.Point(8, 220);
            this.cb2cp.Name = "cb2cp";
            this.cb2cp.Size = new System.Drawing.Size(255, 18);
            this.cb2cp.TabIndex = 5;
            this.cb2cp.Text = "create Sim2CommunityPackage (s2cp)";
            this.cb2cp.CheckedChanged += new System.EventHandler(this.Checks2cp);
            // 
            // ofd
            // 
            this.ofd.Filter = "Sims 2 Package (*.package)|*.package;*.sims|All Files (*.*)|*.*";
            // 
            // SaveSims2CommunityPack
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(794, 560);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaveSims2CommunityPack";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sims 2 Pack File Browser";
            this.Load += new System.EventHandler(this.SaveSims2CommunityPack_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.AllowClose);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.gbsettings.ResumeLayout(false);
            this.gbsettings.PerformLayout();
            this.gbIsettings.ResumeLayout(false);
            this.gbIsettings.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		bool extension;
        bool ok;
        bool cln;
        string loc = "Downloads";
        string fnm = System.Guid.NewGuid().ToString().Replace("-", "");
        string downlode = Path.Combine(PathProvider.SimSavegameFolder, "Downloads");
        string newf = "New1";
        SoundPlayer savy = new SoundPlayer(booby.NoisyGirls.Save);
        SoundPlayer aahh = new SoundPlayer(booby.NoisyGirls.Aah);

		/// <summary>
		/// Shows the Create Dialogue
		/// </summary>
		/// <param name="files">all packages that should be initially in the File</param>
		/// <param name="extension">true if you want to add the Community Extension, otherwise 
		/// a normal Sims2Pack File will be generated</param>
		/// <returns>A list of all packages this File should contain</returns>
		public S2CPDescriptor[] Execute(SimPe.Packages.GeneratableFile[] files, ref bool extension)
		{
			this.extension = extension;
			ok = false;
            //if (files.Length == 0 && !extension)
            //{
            //    lblist.Visible = false;
            //    panel3.Visible = true;
            //}
            cb2cp.Visible = (booby.PrettyGirls.IsTitsInstalled() || booby.PrettyGirls.IsAngelsInstalled());
			S2CPDescriptor[] s2cps = new S2CPDescriptor[files.Length];
			for (int i=0; i<files.Length; i++) 
			{
                s2cps[i] = new S2CPDescriptor(files[i], "", "", "", "", Sims2CommunityPack.DEFAULT_COMPRESSION_STRENGTH, new S2CPDescriptorBase[0], extension);
                Sims2CommunityPack.gethertitle(files[i], s2cps[i]);
				lblist.Items.Add(s2cps[i]);
			}
			tbflname.ReadOnly = tbauthor.ReadOnly = tbver.ReadOnly = tbdesc.ReadOnly = tbtitle.ReadOnly = tbcontact.ReadOnly = false;
            tbIname.ReadOnly = tbname.ReadOnly = true;
			cb2cp.Checked = extension;
            btadd.Visible = btdelete.Visible = true;
            btinstall.Visible = label11.Visible = tbvalid.Visible = false;
            btbrowse.Enabled = cb2cp.Enabled = cbcompress.Enabled = true;
			btsave.Text = "Save";
			lblist.SelectionMode = SelectionMode.One;
			if (lblist.Items.Count>0) lblist.SelectedIndex=0;
			btdelete.Enabled = (lblist.SelectedIndex>=0);
			this.Checks2cp(this.cb2cp, null);
			this.ShowDialog();
			extension = this.cb2cp.Checked;
			if (ok) 
			{
				s2cps = new S2CPDescriptor[lblist.Items.Count];
				for (int i=0; i<s2cps.Length; i++) 
				{
					s2cps[i] = (S2CPDescriptor)lblist.Items[i];
					if (extension) s2cps[i].Update();
				}
				return s2cps;
			}
			return null;
		}

		/// <summary>
		/// Shows the Load Dialogue
		/// </summary>
		/// <param name="files">All Descriptors within the S2CP File</param>
		/// <param name="selmode">Selection Mode for the Listbox</param>
		/// <returns>All the Packages the user has selected</returns>
        public S2CPDescriptor[] Execute(S2CPDescriptor[] files, SelectionMode selmode, bool s2cp)
        {
            label6.Visible = tbflname.Visible = btbrowse.Visible = btadd.Visible = btdelete.Visible = cb2cp.Visible = tbgameguid.Visible = label9.Visible = false;
            lblist.Location = new System.Drawing.Point(8, 8);
            if (s2cp)
            {
                lblist.Size = new System.Drawing.Size(778, 230);
                tbname.ReadOnly = tbauthor.ReadOnly = tbver.ReadOnly = tbdesc.ReadOnly = tbtitle.ReadOnly = tbcontact.ReadOnly = true;
                gbsettings.Visible = true;
                gbIsettings.Visible = false;
                cbcompress.Enabled = false;
            }
            else
            {
                lblist.Size = new System.Drawing.Size(778, 340); // was 357
                tbIname.ReadOnly = tbIdesc.ReadOnly = tbItitle.ReadOnly = true;
                gbsettings.Visible = false;
                gbIsettings.Visible = true;
            }
            this.Text = "Sims 2 Pack File Browser (" + tbflname.Text + ")";
			this.extension = false;
			ok = false;
            for (int i = 0; i < files.Length; i++) lblist.Items.Add(files[i]);
            btinstall.Visible = true;
			btsave.Text = "Open";
            this.lblist.SelectionMode = selmode;
            if (lblist.Items.Count > 0) lblist.SelectedIndex = 0;
			this.ShowDialog();
			if (ok) 
			{
				S2CPDescriptor[] fls = new S2CPDescriptor[lblist.SelectedItems.Count];
				for (int i=0; i<fls.Length; i++) 
				{
					fls[i] = (S2CPDescriptor)lblist.SelectedItems[i];
				}
				return fls;
			}
			return null;
		}

		/// <summary>
		/// Updates the Link Text for the Dependency Window
		/// </summary>
		/// <param name="s2cp"></param>
		void UpdateDepText(S2CPDescriptor s2cp)
		{
			lldep.Text = "Show "+s2cp.Dependency.Length+" Dependencies...";
			lldep.LinkArea = new LinkArea(lldep.Text.Length - 15, 12);
		}

		/// <summary>
		/// Select a List Item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void Select(object sender, System.EventArgs e) // CJH
		{
			if (lblist.Tag!=null) return;
            gbsettings.Enabled = gbIsettings.Enabled = btdelete.Enabled = (lblist.SelectedIndex >= 0);
            if (lblist.SelectedIndex < 0) return;
			lblist.Tag = true;
			try 
			{
				SimPe.Packages.S2CPDescriptor s2cp = (SimPe.Packages.S2CPDescriptor )lblist.Items[lblist.SelectedIndex];
                tbname.Text = tbIname.Text = s2cp.Name;
				tbguid.Text = s2cp.Guid;
				tbauthor.Text = s2cp.Author;
                tbdesc.Text = tbIdesc.Text = s2cp.Description;
				tbver.Text = s2cp.ObjectVersion;
                tbvalid.Text = s2cp.Valid.ToString();
				tbcontact.Text = s2cp.Contact;
				tbgameguid.Text = s2cp.GameGuid;
                tbtitle.Text = tbItitle.Text = s2cp.Title;
                lldep.Enabled = cb2cp.Checked || s2cp.Dependency.Length > 0; // CJH
				cbcompress.SelectedIndex = cbcompress.Items.Count-1;
				for (int i=0; i<cbcompress.Items.Count; i++)
				{
					Sims2CommunityPack.CompressionStrength cs = (Sims2CommunityPack.CompressionStrength)cbcompress.Items[i];
					if (cs == s2cp.Compressed) 
					{
						cbcompress.SelectedIndex = i;
						break;
					}
				}
                UpdateDepText(s2cp);
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			} 
			finally 
			{
				lblist.Tag = null;
			}
		}

		private void ChangeText(object sender, System.EventArgs e)
		{
			if (lblist.Tag!=null) return;
			if (lblist.SelectedIndex<0) return;
			lblist.Tag = true;
			try 
			{
				SimPe.Packages.S2CPDescriptor s2cp = (SimPe.Packages.S2CPDescriptor )lblist.Items[lblist.SelectedIndex];
                if (extension)
                {
                    s2cp.Name = tbname.Text;
                    s2cp.Author = tbauthor.Text;
                    s2cp.Contact = tbcontact.Text;
                    s2cp.Description = tbdesc.Text;
                    s2cp.ObjectVersion = tbver.Text;
                    s2cp.Title = tbtitle.Text;
                }
                else
                {
                    s2cp.Name = tbIname.Text;
                    s2cp.Description = tbIdesc.Text;
                    s2cp.Title = tbItitle.Text;
                }

				lblist.Items[lblist.SelectedIndex] = s2cp;
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			} 
			finally 
			{
				lblist.Tag = null;
			}
		}

		private void AddPackage(object sender, System.EventArgs e)
		{
            ofd.Filter = "Sims 2 Package (*.package)|*.package;*.sims|All Files (*.*)|*.*";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
                try
                {
                    SimPe.Packages.GeneratableFile package = GeneratableFile.LoadFromFile(ofd.FileName);
                    S2CPDescriptor s2cp = new S2CPDescriptor(package, "", "", "", "", Sims2CommunityPack.DEFAULT_COMPRESSION_STRENGTH, new S2CPDescriptorBase[0], extension);
                    Sims2CommunityPack.gethertitle(package, s2cp);
                    if (!lblist.Visible)
                    {
                        lblist.Visible = true;
                        panel3.Visible = false;
                    }
                    lblist.Items.Add(s2cp);
                    lblist.SelectedIndex = lblist.Items.Count - 1;
                }
                catch { }
			}
		}

		private void DeletePackage(object sender, System.EventArgs e)
		{
            if (lblist.SelectedIndex < 0) return;
			lblist.Items.RemoveAt(lblist.SelectedIndex);
		}

		private void S2CPFilename(object sender, System.EventArgs e)
		{
			if (this.cb2cp.Checked)
				sfd.Filter = "Sims 2 Community Package (*.s2cp)|*.s2cp|All Files (*.*)|*.*";
			else 
				sfd.Filter = "Sims 2 Package (*.sims2pack)|*.sims2pack|All Files (*.*)|*.*";
			if (sfd.ShowDialog()==DialogResult.OK) 
			{
				tbflname.Text = sfd.FileName;
			}
		}

		private void AllowClose(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (tbflname.ReadOnly) 
			{
				if ((lblist.SelectedItems.Count==0) && (ok))
				{
					MessageBox.Show("You have to select at Least one Package");
					btadd.Select();
					e.Cancel = true;
				}
			}
			else 
			{
				if ((tbflname.Text.Trim()=="") && (ok))
				{
					MessageBox.Show("You have to specify a Filename for the Sims2Community Pack File.");
					this.tbflname.Select();
					e.Cancel = true;
				}

				if ((lblist.Items.Count==0) && (ok))
				{
					MessageBox.Show("You have to add at least one Package.");
					btadd.Select();
					e.Cancel = true;
				}
			}
		}

        private void ShowDependencies(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            if (lblist.Tag != null) return;
            if (lblist.SelectedIndex < 0) return;

            lblist.Tag = true;
            try
            {
                SimPe.Packages.S2CPDescriptor s2cp = (SimPe.Packages.S2CPDescriptor)lblist.Items[lblist.SelectedIndex];
                DepSims2Community form = new DepSims2Community();
                form.Execute(s2cp, tbflname.ReadOnly);
                UpdateDepText(s2cp);
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage("", ex);
            }
            finally
            {
                lblist.Tag = null;
            }
        }

		private void btcancel_Click(object sender, System.EventArgs e)
		{
			ok = false;
			Close();
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			ok = true;
			Close();
		}

		private void SelectCompression(object sender, System.EventArgs e)
		{
			if (lblist.Tag!=null) return;
			if (lblist.SelectedIndex<0) return;
			if (cbcompress.SelectedIndex<0) return;
			lblist.Tag = true;
			try 
			{
				SimPe.Packages.S2CPDescriptor s2cp = (SimPe.Packages.S2CPDescriptor )lblist.Items[lblist.SelectedIndex];
				s2cp.Compressed = (SimPe.Packages.Sims2CommunityPack.CompressionStrength)cbcompress.Items[cbcompress.SelectedIndex];
				lblist.Items[lblist.SelectedIndex] = s2cp;
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			} 
			finally 
			{
				lblist.Tag = null;
			}
		}

		private void SaveSims2CommunityPack_Load(object sender, System.EventArgs e) { }

		private void Checks2cp(object sender, System.EventArgs e)
        {
			if (cb2cp.Checked)
            {
                cb2cp.Location = new System.Drawing.Point(8, 220);
                btadd.Location = new System.Drawing.Point(618, 222);
                btdelete.Location = new System.Drawing.Point(703, 222);
                lblist.Size = new System.Drawing.Size(778, 180);
                gbsettings.Visible = true;
                gbIsettings.Visible = false;
                if (this.tbflname.Text.Trim().ToLower().EndsWith(".sims2pack"))
                    this.tbflname.Text = this.tbflname.Text.Trim().Substring(0, this.tbflname.Text.Trim().Length - 10) + ".s2cp";
                if (!lblist.Visible)
                {
                    lblist.Visible = true;
                    panel3.Visible = false;
                }
            }
            else
            {
                cb2cp.Location = new System.Drawing.Point(8, 325);
                btadd.Location = new System.Drawing.Point(618, 327);
                btdelete.Location = new System.Drawing.Point(703, 327);
                lblist.Size = new System.Drawing.Size(778, 285);
                gbsettings.Visible = false;
                gbIsettings.Visible = true;
                if (this.tbflname.Text.Trim().ToLower().EndsWith(".s2cp"))
                    this.tbflname.Text = this.tbflname.Text.Trim().Substring(0, this.tbflname.Text.Trim().Length - 5) + ".sims2pack";
                if (lblist.Items.Count == 0)
                {
                    lblist.Visible = false;
                    panel3.Visible = true;
                }
			}
        }

        private void bticancel_Click(object sender, EventArgs e)
        {
            this.Text = "Sims 2 Pack File Browser (" + tbflname.Text + ")";
            panel2.Visible = false;
        }

        private void cbadnoo_CheckedChanged(object sender, EventArgs e) { setcolour(); }

        private void Handle_ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView lv = sender as ListView;
            if (lv != null)
            {
                if (lv.ListViewItemSorter == null)
                    lv.ListViewItemSorter = new ColumnSorter();
                ColumnSorter cmp = lv.ListViewItemSorter as ColumnSorter;
                if (cmp.CurrentColumn != e.Column)
                {
                    cmp.CurrentColumn = e.Column;
                    cmp.Sorting = SortOrder.Descending;// reset order on column change
                }
                cmp.Sorting ^= (SortOrder.Ascending | SortOrder.Descending); // toggle me
                lv.Sort();
            }
        }

        private void btinstall_Click(object sender, EventArgs e)
        {
            int a = 1;
            cln = false;
            this.Text = "Sims 2 Pack File Installer" + " (" + tbflname.Text + ")";
            while (Directory.Exists(Path.Combine(downlode, "New" + Convert.ToString(a)))) a++;
            newf = "New" + Convert.ToString(a);
            cbadnoo.Text = "Create a New Downloads Subfolder (" + newf + ")";
            filesList.Items.Clear();
            if (!pjse.GUIDIndex.TheGUIDIndex.IsLoaded) pjse.GUIDIndex.TheGUIDIndex.Load();
            panel2.Visible = true;
            loc = getfiletype();
            if (loc == "Teleport") lbtipe.Text = "File Type: Lot";
            else if (loc == "SavedSims") lbtipe.Text = "File Type: Sim";
            string tipe;
            foreach (S2CPDescriptor fil in lblist.Items)
            {
                if (fil.Crc == "0") fil.Crc = System.Guid.NewGuid().ToString().Replace("-", "");
                ListViewItem tItem = new ListViewItem("");
                tItem.SubItems.Add(fil.Name);//name
                tipe = Sims2CommunityPack.s2cptype(fil.Package);
                if (tipe != "Lot") { Interfaces.Files.IPackedFileDescriptor[] inx = fil.Package.Index; if (inx.Length > 1000) tipe += " (Too Many)"; }
                tItem.SubItems.Add(tipe);//type
                if (tipe == "Family") tItem.SubItems.Add(loc);
                else if (tipe == "Person") tItem.SubItems.Add(loc);
                else if (tipe == "Lot") { tItem.SubItems.Add(loc); fnm = fil.Crc; }
                else if (tipe == "Packaged Sim") tItem.SubItems.Add(loc);
                else { tItem.SubItems.Add("Downloads"); cln = true; }//install location
                tItem.SubItems.Add(fil.Title);
                if (loc == "Downloads" && tipe == "Person") { tItem.ForeColor = Color.Red; lbwarnim.Visible = true; tItem.Checked = false; }
                else if (tipe.Contains("Override") || tipe.EndsWith(" Mod") || tipe.StartsWith("SimType") || tipe == "Memory" || tipe.EndsWith("(Too Many)")) { tItem.ForeColor = Color.DarkRed; tItem.Checked = false; }
                else if (tipe == "Hug Bug Infection") { tItem.ForeColor = Color.MediumVioletRed; tItem.Checked = false; }
                else if (tipe == "Unreadable") { tItem.Font = new Font(filesList.Font.FontFamily, filesList.Font.Size, FontStyle.Italic); tItem.ForeColor = Color.DarkRed; tItem.Checked = false; }
                else tItem.Checked = true;
                if (tipe == "Lot") { if (Sims2CommunityPack.findhugbug(fil.Package)) { tItem.ForeColor = Color.MediumVioletRed; aahh.Play(); } }
                tItem.Tag = fil;
                filesList.Items.Add(tItem);
            }
            setcolour();
            if (cln && loc != "Downloads") btcleanim.Text = "Key Files Only"; else btcleanim.Text = "Select All";
        }

        private void setcolour()
        {
            foreach (ListViewItem item in filesList.Items)
            {
                if (item.ForeColor == Color.Red || item.ForeColor == Color.DarkRed || item.ForeColor == Color.MediumVioletRed)
                {
                    item.BackColor = filesList.BackColor;
                    if (item.SubItems[3].Text == "SavedSims")
                    {
                        if (System.IO.File.Exists(Path.Combine(PathProvider.SimSavegameFolder, "SavedSims\\" + item.SubItems[1].Text)))
                            item.BackColor = Color.FromArgb(210, 210, 252);
                    }
                    else if (item.SubItems[3].Text == "Downloads" && !cbadnoo.Checked)
                    {
                        if (System.IO.File.Exists(Path.Combine(PathProvider.SimSavegameFolder, "Downloads\\" + item.SubItems[1].Text)))
                            item.BackColor = Color.FromArgb(210, 210, 252);
                    }
                }
                else
                {
                    item.ForeColor = SystemColors.WindowText;
                    if (item.SubItems[3].Text == "SavedSims")
                    {
                        if (System.IO.File.Exists(Path.Combine(PathProvider.SimSavegameFolder, "SavedSims\\" + item.SubItems[1].Text)))
                            item.ForeColor = Color.SlateBlue;
                    }
                    else if (item.SubItems[3].Text == "Downloads" && !cbadnoo.Checked)
                    {
                        if (System.IO.File.Exists(Path.Combine(PathProvider.SimSavegameFolder, "Downloads\\" + item.SubItems[1].Text)))
                            item.ForeColor = Color.SlateBlue;
                    }
                }
            }
        }

        private string getfiletype()
        {
            foreach (S2CPDescriptor fil in lblist.Items)
            {
                if (fil.Name.EndsWith(".sims")) return "Teleport";
                //if (fil.Package.FindFiles(0x484F5553).Length > 0) return "Teleport";// Lot - Teleport required 
                //if (fil.Package.FindFiles(0xCDB8BDC4).Length > 0) return "Teleport";// Packaged Family - Teleport required 
            }
            foreach (S2CPDescriptor fil in lblist.Items)
            {
                if (fil.Package.FindFiles(0xAC598EAC).Length > 0 && fil.Package.FindFiles(0xEBCF3E27).Length > 0 && fil.Package.FindFiles(0x4F424A44).Length == 0) return "SavedSims";
                //Age Data + Property Set, no Object Data - SavedSim required
            }
            return "Downloads";
        }

        private void btgoforit_Click(object sender, EventArgs e)
        {
            ok = false;
            if (btgoforit.Text == "Close") Close();
            else
            {
                btgoforit.Text = "Close";
                if (!Directory.Exists(Path.Combine(PathProvider.SimSavegameFolder, loc)))
                    Directory.CreateDirectory(Path.Combine(PathProvider.SimSavegameFolder, loc));

                foreach (ListViewItem item in filesList.Items)
                {
                    if (!item.Checked) continue;
                    GeneratableFile toins = (item.Tag as S2CPDescriptor).Package;
                    if (item.SubItems[3].Text == "Teleport")
                    {
                        toins.Save(Path.Combine(PathProvider.SimSavegameFolder, "Teleport\\" + (item.Tag as S2CPDescriptor).Crc + "_0001.Sims2Tmp"));
                        ok = true;
                    }
                    else if (item.SubItems[3].Text == "SavedSims")
                        toins.Save(Path.Combine(PathProvider.SimSavegameFolder, "SavedSims\\" + item.SubItems[1].Text));
                    else
                    {
                        if (cbadnoo.Checked)
                        {
                            if (!Directory.Exists(Path.Combine(downlode, newf)))
                                Directory.CreateDirectory(Path.Combine(downlode, newf));
                            toins.Save(Path.Combine(Path.Combine(downlode, newf), item.SubItems[1].Text));
                        }
                        else toins.Save(Path.Combine(PathProvider.SimSavegameFolder, "Downloads\\" + item.SubItems[1].Text));
                    }
                    if (booby.ThemeManager.Global.CurrentTheme == booby.GuiTheme.Office2003) item.BackColor = booby.ThemeManager.Global.ThemeColorLight;
                    else item.BackColor = booby.ThemeManager.Global.ThemeColor;
                }
                if (loc == "Teleport" && ok)
                {
                    string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";
                    xml += "<Sims2Package type=\"Lot\">\n";
                    xml += "  <ArchiveVersion>1</ArchiveVersion>\n";
                    xml += "  <GameVersion>2141707388.153.1</GameVersion>\n";
                    xml += "  <CodeVersion>17</CodeVersion>\n";
                    int offset = 0;
                    foreach (ListViewItem item in filesList.Items)
                    {
                        if (!item.Checked) continue;
                        if (item.SubItems[3].Text != "Teleport") continue;
                        S2CPDescriptor fle = (item.Tag as S2CPDescriptor);
                        MemoryStream m = fle.Package.Build();
                        xml += "  <PackagedFile>\n";
                        if (item.SubItems[1].Text == "")
                            xml += "    <Name><![CDATA[]]></Name>\n";
                        else
                            xml += "    <Name>" + item.SubItems[1].Text + "</Name>\n";
                        xml += "    <Length>" + m.Length.ToString() + "</Length>\n";
                        if (item.SubItems[2].Text == "Infected Lot!")
                            xml += "    <Type>Lot</Type>\n";
                        else if (item.SubItems[2].Text == "NPC Mod")
                            xml += "    <Type>Person</Type>\n";
                        else
                            xml += "    <Type>" + item.SubItems[2].Text + "</Type>\n";
                        xml += "    <Identifiers>\n";
                        xml += "      <Crc>" + fle.Crc + "</Crc>\n";
                        xml += "      <Guid>" + fle.Crc + "</Guid>\n";
                        xml += "      <Version>0</Version>\n";
                        xml += "    </Identifiers>\n";
                        xml += "    <Offset>" + offset.ToString() + "</Offset>\n";
                        if (fle.Title == "")
                            xml += "    <DisplayName><![CDATA[]]></DisplayName>\n";
                        else
                            xml += "    <DisplayName>" + fle.Title + "</DisplayName>\n";
                        if (fle.Description == "")
                            xml += "    <Description><![CDATA[]]></Description>\n";
                        else
                            xml += "    <Description>" + fle.Description + "</Description>\n";
                        xml += "  </PackagedFile>\n";
                        offset += (int)m.Length;
                        m.Close();
                        m.Dispose();
                        m = null;
                    }
                    xml += "</Sims2Package>";
                    MemoryStream ms = new MemoryStream();
                    BinaryWriter bw = new BinaryWriter(ms);
                    bw.Write(Helper.ToBytes("Sims2 Packager 1.0"));
                    bw.Write((int)(22 + xml.Length));
                    bw.Write(Helper.ToBytes(xml));
                    FileStream fs = new FileStream(Path.Combine(PathProvider.SimSavegameFolder, "Teleport\\" + fnm + "_0001.Sims2Import"), FileMode.Create);
                    try
                    {
                        fs.Write(ms.ToArray(), 0, (int)ms.Length);
                    }
                    finally
                    {
                        fs.Close();
                        fs.Dispose();
                        fs = null;
                    }
                }
                lbtipe.Text = " Done!";
                bticancel.Visible = false;
                cbadnoo.Enabled = false;
                savy.Play();
            }
        }

        private void btcleanim_Click(object sender, EventArgs e)
        {
            if (btcleanim.Text == "Key Files Only")
            {
                foreach (ListViewItem item in filesList.Items)
                { item.Checked = item.SubItems[3].Text != "Downloads"; }
                btcleanim.Text = "Select All";
            }
            else
            {
                foreach (ListViewItem item in filesList.Items) { item.Checked = true; }
                if (cln && loc != "Downloads") btcleanim.Text = "Key Files Only";
            }
        }
	}
}
