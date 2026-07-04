/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *   Copyright (C) 2008 Peter L Jones                                      *
 *   pljones@users.sf.net                                                  *
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

namespace SimPe.Plugin
{
    /// <summary>
    /// Summary description for LtxtForm.
    /// </summary>
    public class LtxtForm : System.Windows.Forms.Form
    {
        #region Form controls
        internal booby.gradientpanel ltxtPanel;
        private booby.panelheader panel2;
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
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label19;
        private Label label20;
        private Label label21;
        internal Label label22;
        private Label label23;
        internal Label label25;
        private Label label30;
        private Label label31;
        private Label label32;
        internal LinkLabel llFamily;
        internal LinkLabel llSubLot;
        internal LinkLabel llAptBase;
        internal LinkLabel llunknone;
        internal GroupBox gbApartment;
        internal GroupBox gbclarse;
        internal GroupBox gbunown;
        private GroupBox gbFlagg;
        internal GroupBox gbApart;
        internal GroupBox gbtravel;
        internal GroupBox gbhobby;
        internal TextBox tblotname;
        internal TextBox tbdesc;
        internal TextBox tbRoads;
        internal TextBox tbrotation;
        internal TextBox tbtype;
        internal TextBox tbver;
        internal TextBox tbsubver;
        internal TextBox tbhg;
        internal TextBox tbwd;
        internal TextBox tbtop;
        internal TextBox tbleft;
        internal TextBox tbinst;
        internal TextBox tbu2;
        internal ListBox lb;
        internal TextBox tbz;
        internal TextBox tbData;
        internal TextBox tbu0;
        internal TextBox tbu4;
        internal TextBox tbu3;
        internal TextBox tbTexture;
        internal TextBox tbowner;
        internal TextBox tbApBase;
        internal TextBox tbu6;
        internal ListBox lbApts;
        internal TextBox tbElevationAt;
        internal TextBox tbApartment;
        internal TextBox tbSAu3;
        internal TextBox tbSAu2;
        internal TextBox tbSAFamily;
        internal ListBox lbu7;
        internal TextBox tblotclass;
        internal TextBox tbcset;
        internal ComboBox cbtype;
        internal ComboBox cbLotClas;
        internal Ambertation.Windows.Forms.EnumComboBox cborient;
        internal CheckBox cbhidim;
        internal CheckBox cbhbmusic;
        internal CheckBox cbhbsport;
        internal CheckBox cbhbscience;
        internal CheckBox cbhbfitness;
        internal CheckBox cbhbtinker;
        internal CheckBox cbhbnature;
        internal CheckBox cbhbgames;
        internal CheckBox cbhbfilm;
        internal CheckBox cbhbart;
        internal CheckBox cbhbcook;
        internal CheckBox cbtrjflag5;
        internal CheckBox cbtrjflag4;
        internal CheckBox cbtrjflag3;
        internal CheckBox cbtrjflag2;
        internal CheckBox cbtrjflag1;
        internal CheckBox cbtrjungle;
        internal CheckBox cbtrhidec;
        internal CheckBox cbtrpool;
        internal CheckBox cbtrmale;
        internal CheckBox cbtrfem;
        internal CheckBox cbtrbeach;
        internal CheckBox cbtrformal;
        internal CheckBox cbtrteen;
        internal CheckBox cbtrnude;
        internal CheckBox cbtrpern;
        internal CheckBox cgtrwhite;
        internal CheckBox cbtrblue;
        internal CheckBox cbtrredred;
        internal CheckBox cbtradult;
        internal CheckBox cbtrclub;
        internal CheckBox cbBeachy;
        internal PictureBox pb;
        internal Button btnDelApt;
        internal Button btnAddApt;
        internal Button bthbytrvl;
        internal Label lbPlayim;
        internal Label lbOpenim;
        internal TextBox tbpris2;
        internal TextBox tbpris1;
        private Label label26;
        private Label label28;
        private Label label27;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        #endregion

        public LtxtForm()
        {
            //
            // Required designer variable.
            //
            InitializeComponent();

            wrapper = null;
            this.cborient.ResourceManager = SimPe.Localization.Manager;
            this.cborient.Enum = typeof(Plugin.LotOrientation);

            if (booby.ThemeManager.ThemedForms)
            {
                booby.ThemeManager tm = booby.ThemeManager.Global.CreateChild();
                tm.AddControl(this.ltxtPanel);
                tm.AddControl(this.lb);
                tm.AddControl(this.lbApts);
                tm.AddControl(this.lbu7);
                tm.AddControl(this.tbdesc);
                tm.AddControl(this.tblotname);
                this.gbclarse.BackColor = this.gbFlagg.BackColor = booby.ThemeManager.Global.ThemeColorLighter;
            }
            if (!Helper.WindowsRegistry.UseBigIcons)
            {
                this.pb.Size = new System.Drawing.Size(124, 108);
                this.pb.Location = new System.Drawing.Point(25, 56);
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ltxtPanel = new booby.gradientpanel();
            this.panel2 = new booby.panelheader();
            this.gbunown = new System.Windows.Forms.GroupBox();
            this.tbu2 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lbu7 = new System.Windows.Forms.ListBox();
            this.tbu3 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tbData = new System.Windows.Forms.TextBox();
            this.tbu6 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.lbPlayim = new System.Windows.Forms.Label();
            this.lbOpenim = new System.Windows.Forms.Label();
            this.gbApart = new System.Windows.Forms.GroupBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.tbpris2 = new System.Windows.Forms.TextBox();
            this.tbpris1 = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.gbApartment = new System.Windows.Forms.GroupBox();
            this.llFamily = new System.Windows.Forms.LinkLabel();
            this.tbApartment = new System.Windows.Forms.TextBox();
            this.tbSAu2 = new System.Windows.Forms.TextBox();
            this.llSubLot = new System.Windows.Forms.LinkLabel();
            this.label31 = new System.Windows.Forms.Label();
            this.tbSAu3 = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.tbSAFamily = new System.Windows.Forms.TextBox();
            this.lbApts = new System.Windows.Forms.ListBox();
            this.tbApBase = new System.Windows.Forms.TextBox();
            this.llAptBase = new System.Windows.Forms.LinkLabel();
            this.btnDelApt = new System.Windows.Forms.Button();
            this.btnAddApt = new System.Windows.Forms.Button();
            this.tbdesc = new System.Windows.Forms.TextBox();
            this.gbtravel = new System.Windows.Forms.GroupBox();
            this.cbtrjflag5 = new System.Windows.Forms.CheckBox();
            this.cbtrjflag4 = new System.Windows.Forms.CheckBox();
            this.cbtrjflag3 = new System.Windows.Forms.CheckBox();
            this.cbtrjflag2 = new System.Windows.Forms.CheckBox();
            this.cbtrjflag1 = new System.Windows.Forms.CheckBox();
            this.cbtrjungle = new System.Windows.Forms.CheckBox();
            this.cbtrhidec = new System.Windows.Forms.CheckBox();
            this.cbtrpool = new System.Windows.Forms.CheckBox();
            this.cbtrmale = new System.Windows.Forms.CheckBox();
            this.cbtrfem = new System.Windows.Forms.CheckBox();
            this.cbtrbeach = new System.Windows.Forms.CheckBox();
            this.cbtrformal = new System.Windows.Forms.CheckBox();
            this.cbtrteen = new System.Windows.Forms.CheckBox();
            this.cbtrnude = new System.Windows.Forms.CheckBox();
            this.cbtrpern = new System.Windows.Forms.CheckBox();
            this.cgtrwhite = new System.Windows.Forms.CheckBox();
            this.cbtrblue = new System.Windows.Forms.CheckBox();
            this.cbtrredred = new System.Windows.Forms.CheckBox();
            this.cbtradult = new System.Windows.Forms.CheckBox();
            this.cbtrclub = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tblotname = new System.Windows.Forms.TextBox();
            this.gbhobby = new System.Windows.Forms.GroupBox();
            this.cbhbmusic = new System.Windows.Forms.CheckBox();
            this.cbhbsport = new System.Windows.Forms.CheckBox();
            this.cbhbscience = new System.Windows.Forms.CheckBox();
            this.cbhbfitness = new System.Windows.Forms.CheckBox();
            this.cbhbtinker = new System.Windows.Forms.CheckBox();
            this.cbhbnature = new System.Windows.Forms.CheckBox();
            this.cbhbgames = new System.Windows.Forms.CheckBox();
            this.cbhbfilm = new System.Windows.Forms.CheckBox();
            this.cbhbart = new System.Windows.Forms.CheckBox();
            this.cbhbcook = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.llunknone = new System.Windows.Forms.LinkLabel();
            this.gbFlagg = new System.Windows.Forms.GroupBox();
            this.tbu0 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cbBeachy = new System.Windows.Forms.CheckBox();
            this.cbhidim = new System.Windows.Forms.CheckBox();
            this.gbclarse = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbLotClas = new System.Windows.Forms.ComboBox();
            this.tbcset = new System.Windows.Forms.TextBox();
            this.tblotclass = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lb = new System.Windows.Forms.ListBox();
            this.tbElevationAt = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.tbowner = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.bthbytrvl = new System.Windows.Forms.Button();
            this.tbinst = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbu4 = new System.Windows.Forms.TextBox();
            this.cborient = new Ambertation.Windows.Forms.EnumComboBox();
            this.tbTexture = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbwd = new System.Windows.Forms.TextBox();
            this.tbrotation = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbhg = new System.Windows.Forms.TextBox();
            this.tbRoads = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbver = new System.Windows.Forms.TextBox();
            this.tbtop = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbsubver = new System.Windows.Forms.TextBox();
            this.tbleft = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbz = new System.Windows.Forms.TextBox();
            this.cbtype = new System.Windows.Forms.ComboBox();
            this.tbtype = new System.Windows.Forms.TextBox();
            this.pb = new System.Windows.Forms.PictureBox();
            this.ltxtPanel.SuspendLayout();
            this.gbunown.SuspendLayout();
            this.gbApart.SuspendLayout();
            this.gbApartment.SuspendLayout();
            this.gbtravel.SuspendLayout();
            this.gbhobby.SuspendLayout();
            this.gbFlagg.SuspendLayout();
            this.gbclarse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.SuspendLayout();
            // 
            // ltxtPanel
            // 
            this.ltxtPanel.AutoScroll = true;
            this.ltxtPanel.BackgroundImageLocation = new System.Drawing.Point(998, 50);
            this.ltxtPanel.BackgroundImageZoomToFit = true;
            this.ltxtPanel.Controls.Add(this.lbPlayim);
            this.ltxtPanel.Controls.Add(this.lbOpenim);
            this.ltxtPanel.Controls.Add(this.gbApart);
            this.ltxtPanel.Controls.Add(this.tbdesc);
            this.ltxtPanel.Controls.Add(this.gbtravel);
            this.ltxtPanel.Controls.Add(this.label5);
            this.ltxtPanel.Controls.Add(this.tblotname);
            this.ltxtPanel.Controls.Add(this.gbhobby);
            this.ltxtPanel.Controls.Add(this.label4);
            this.ltxtPanel.Controls.Add(this.llunknone);
            this.ltxtPanel.Controls.Add(this.gbFlagg);
            this.ltxtPanel.Controls.Add(this.gbclarse);
            this.ltxtPanel.Controls.Add(this.gbunown);
            this.ltxtPanel.Controls.Add(this.label7);
            this.ltxtPanel.Controls.Add(this.lb);
            this.ltxtPanel.Controls.Add(this.tbElevationAt);
            this.ltxtPanel.Controls.Add(this.label25);
            this.ltxtPanel.Controls.Add(this.tbowner);
            this.ltxtPanel.Controls.Add(this.label15);
            this.ltxtPanel.Controls.Add(this.label8);
            this.ltxtPanel.Controls.Add(this.bthbytrvl);
            this.ltxtPanel.Controls.Add(this.tbinst);
            this.ltxtPanel.Controls.Add(this.label14);
            this.ltxtPanel.Controls.Add(this.tbu4);
            this.ltxtPanel.Controls.Add(this.cborient);
            this.ltxtPanel.Controls.Add(this.tbTexture);
            this.ltxtPanel.Controls.Add(this.label2);
            this.ltxtPanel.Controls.Add(this.label6);
            this.ltxtPanel.Controls.Add(this.label3);
            this.ltxtPanel.Controls.Add(this.tbwd);
            this.ltxtPanel.Controls.Add(this.tbrotation);
            this.ltxtPanel.Controls.Add(this.label9);
            this.ltxtPanel.Controls.Add(this.label10);
            this.ltxtPanel.Controls.Add(this.tbhg);
            this.ltxtPanel.Controls.Add(this.tbRoads);
            this.ltxtPanel.Controls.Add(this.label12);
            this.ltxtPanel.Controls.Add(this.tbver);
            this.ltxtPanel.Controls.Add(this.tbtop);
            this.ltxtPanel.Controls.Add(this.label13);
            this.ltxtPanel.Controls.Add(this.tbsubver);
            this.ltxtPanel.Controls.Add(this.tbleft);
            this.ltxtPanel.Controls.Add(this.label20);
            this.ltxtPanel.Controls.Add(this.label1);
            this.ltxtPanel.Controls.Add(this.tbz);
            this.ltxtPanel.Controls.Add(this.cbtype);
            this.ltxtPanel.Controls.Add(this.tbtype);
            this.ltxtPanel.Controls.Add(this.panel2);
            this.ltxtPanel.Controls.Add(this.pb);
            this.ltxtPanel.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.ltxtPanel.Location = new System.Drawing.Point(12, 12);
            this.ltxtPanel.Name = "ltxtPanel";
            this.ltxtPanel.Size = new System.Drawing.Size(1000, 526);
            this.ltxtPanel.TabIndex = 0;
            // 
            // gbunown
            // 
            this.gbunown.Controls.Add(this.tbu2);
            this.gbunown.Controls.Add(this.label18);
            this.gbunown.Controls.Add(this.label32);
            this.gbunown.Controls.Add(this.label19);
            this.gbunown.Controls.Add(this.lbu7);
            this.gbunown.Controls.Add(this.tbu3);
            this.gbunown.Controls.Add(this.label16);
            this.gbunown.Controls.Add(this.tbData);
            this.gbunown.Controls.Add(this.tbu6);
            this.gbunown.Controls.Add(this.label23);
            this.gbunown.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gbunown.Location = new System.Drawing.Point(116, 408);
            this.gbunown.Name = "gbunown";
            this.gbunown.Size = new System.Drawing.Size(877, 112);
            this.gbunown.TabIndex = 101;
            this.gbunown.TabStop = false;
            this.gbunown.Visible = false;
            // 
            // tbu2
            // 
            this.tbu2.Location = new System.Drawing.Point(45, 10);
            this.tbu2.Name = "tbu2";
            this.tbu2.Size = new System.Drawing.Size(48, 21);
            this.tbu2.TabIndex = 2;
            this.tbu2.Text = "0x00";
            this.tbu2.TextChanged += new System.EventHandler(this.CommonChange);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(17, 14);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(28, 13);
            this.label18.TabIndex = 1;
            this.label18.Text = "U2:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label32.Location = new System.Drawing.Point(8, 38);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(28, 13);
            this.label32.TabIndex = 1;
            this.label32.Text = "U7:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label19.Location = new System.Drawing.Point(8, 93);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(104, 13);
            this.label19.TabIndex = 1;
            this.label19.Text = "Unknown Data:";
            // 
            // lbu7
            // 
            this.lbu7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbu7.ColumnWidth = 98;
            this.lbu7.IntegralHeight = false;
            this.lbu7.Items.AddRange(new object[] {
            "0x00000000",
            "0x00000000",
            "0x00000000",
            "0x00000000",
            "0x00000000",
            "0x00000000",
            "0x00000000",
            "0x00000000"});
            this.lbu7.Location = new System.Drawing.Point(42, 38);
            this.lbu7.MinimumSize = new System.Drawing.Size(0, 44);
            this.lbu7.MultiColumn = true;
            this.lbu7.Name = "lbu7";
            this.lbu7.Size = new System.Drawing.Size(830, 44);
            this.lbu7.TabIndex = 2;
            // 
            // tbu3
            // 
            this.tbu3.Location = new System.Drawing.Point(148, 10);
            this.tbu3.Name = "tbu3";
            this.tbu3.Size = new System.Drawing.Size(100, 21);
            this.tbu3.TabIndex = 4;
            this.tbu3.Text = "0.0";
            this.tbu3.TextChanged += new System.EventHandler(this.CommonChange);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label16.Location = new System.Drawing.Point(120, 14);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(28, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "U3:";
            // 
            // tbData
            // 
            this.tbData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbData.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.tbData.Location = new System.Drawing.Point(117, 88);
            this.tbData.Name = "tbData";
            this.tbData.Size = new System.Drawing.Size(755, 22);
            this.tbData.TabIndex = 2;
            this.tbData.TextChanged += new System.EventHandler(this.ChangeData);
            // 
            // tbu6
            // 
            this.tbu6.Location = new System.Drawing.Point(298, 10);
            this.tbu6.MaxLength = 29;
            this.tbu6.Name = "tbu6";
            this.tbu6.Size = new System.Drawing.Size(194, 21);
            this.tbu6.TabIndex = 8;
            this.tbu6.Text = "00 00 00 00  00 00 00 00  00";
            this.tbu6.TextChanged += new System.EventHandler(this.ChangeData);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label23.Location = new System.Drawing.Point(270, 14);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(28, 13);
            this.label23.TabIndex = 7;
            this.label23.Text = "U6:";
            // 
            // lbPlayim
            // 
            this.lbPlayim.AutoSize = true;
            this.lbPlayim.Font = new System.Drawing.Font("Verdana", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lbPlayim.ForeColor = System.Drawing.Color.Blue;
            this.lbPlayim.Location = new System.Drawing.Point(714, 217);
            this.lbPlayim.Name = "lbPlayim";
            this.lbPlayim.Size = new System.Drawing.Size(203, 14);
            this.lbPlayim.TabIndex = 103;
            this.lbPlayim.Text = "Close SimPe and Play this Lot";
            this.lbPlayim.DoubleClick += new System.EventHandler(this.llboobs_LinkClicked);
            // 
            // lbOpenim
            // 
            this.lbOpenim.AutoSize = true;
            this.lbOpenim.Font = new System.Drawing.Font("Verdana", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lbOpenim.ForeColor = System.Drawing.Color.Blue;
            this.lbOpenim.Location = new System.Drawing.Point(738, 192);
            this.lbOpenim.Name = "lbOpenim";
            this.lbOpenim.Size = new System.Drawing.Size(157, 14);
            this.lbOpenim.TabIndex = 103;
            this.lbOpenim.Text = "Open this Lot in SimPe";
            this.lbOpenim.DoubleClick += new System.EventHandler(this.lbOpenim_LinkClicked);
            // 
            // gbApart
            // 
            this.gbApart.Controls.Add(this.label28);
            this.gbApart.Controls.Add(this.label27);
            this.gbApart.Controls.Add(this.tbpris2);
            this.gbApart.Controls.Add(this.tbpris1);
            this.gbApart.Controls.Add(this.label26);
            this.gbApart.Controls.Add(this.label22);
            this.gbApart.Controls.Add(this.gbApartment);
            this.gbApart.Controls.Add(this.lbApts);
            this.gbApart.Controls.Add(this.tbApBase);
            this.gbApart.Controls.Add(this.llAptBase);
            this.gbApart.Controls.Add(this.btnDelApt);
            this.gbApart.Controls.Add(this.btnAddApt);
            this.gbApart.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbApart.Location = new System.Drawing.Point(14, 328);
            this.gbApart.Name = "gbApart";
            this.gbApart.Size = new System.Drawing.Size(922, 75);
            this.gbApart.TabIndex = 1;
            this.gbApart.TabStop = false;
            this.gbApart.Text = "Apartments";
            this.gbApart.Visible = false;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(442, 28);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(45, 13);
            this.label28.TabIndex = 12;
            this.label28.Text = "From:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(460, 52);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(27, 13);
            this.label27.TabIndex = 11;
            this.label27.Text = "To:";
            // 
            // tbpris2
            // 
            this.tbpris2.BackColor = System.Drawing.SystemColors.Window;
            this.tbpris2.Location = new System.Drawing.Point(493, 48);
            this.tbpris2.Name = "tbpris2";
            this.tbpris2.ReadOnly = true;
            this.tbpris2.Size = new System.Drawing.Size(73, 21);
            this.tbpris2.TabIndex = 10;
            // 
            // tbpris1
            // 
            this.tbpris1.BackColor = System.Drawing.SystemColors.Window;
            this.tbpris1.Location = new System.Drawing.Point(493, 24);
            this.tbpris1.Name = "tbpris1";
            this.tbpris1.ReadOnly = true;
            this.tbpris1.Size = new System.Drawing.Size(73, 21);
            this.tbpris1.TabIndex = 9;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(449, 2);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(84, 13);
            this.label26.TabIndex = 8;
            this.label26.Text = "Price Range";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(92, 18);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(127, 13);
            this.label22.TabIndex = 7;
            this.label22.Text = "0 Sub Apartments:";
            // 
            // gbApartment
            // 
            this.gbApartment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbApartment.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbApartment.Controls.Add(this.llFamily);
            this.gbApartment.Controls.Add(this.tbApartment);
            this.gbApartment.Controls.Add(this.tbSAu2);
            this.gbApartment.Controls.Add(this.llSubLot);
            this.gbApartment.Controls.Add(this.label31);
            this.gbApartment.Controls.Add(this.tbSAu3);
            this.gbApartment.Controls.Add(this.label30);
            this.gbApartment.Controls.Add(this.tbSAFamily);
            this.gbApartment.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbApartment.Location = new System.Drawing.Point(592, 0);
            this.gbApartment.Name = "gbApartment";
            this.gbApartment.Size = new System.Drawing.Size(330, 75);
            this.gbApartment.TabIndex = 3;
            this.gbApartment.TabStop = false;
            this.gbApartment.Text = "Selected Apartment";
            // 
            // llFamily
            // 
            this.llFamily.AutoSize = true;
            this.llFamily.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.llFamily.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llFamily.Location = new System.Drawing.Point(-2, 51);
            this.llFamily.Name = "llFamily";
            this.llFamily.Size = new System.Drawing.Size(55, 13);
            this.llFamily.TabIndex = 1;
            this.llFamily.TabStop = true;
            this.llFamily.Text = "Family:";
            this.llFamily.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_Click);
            // 
            // tbApartment
            // 
            this.tbApartment.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.tbApartment.Location = new System.Drawing.Point(59, 20);
            this.tbApartment.Name = "tbApartment";
            this.tbApartment.Size = new System.Drawing.Size(86, 21);
            this.tbApartment.TabIndex = 2;
            this.tbApartment.Text = "0x00000000";
            this.tbApartment.TextChanged += new System.EventHandler(this.SAChange);
            // 
            // tbSAu2
            // 
            this.tbSAu2.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.tbSAu2.Location = new System.Drawing.Point(223, 20);
            this.tbSAu2.Name = "tbSAu2";
            this.tbSAu2.Size = new System.Drawing.Size(86, 21);
            this.tbSAu2.TabIndex = 6;
            this.tbSAu2.Text = "0x00000000";
            this.tbSAu2.TextChanged += new System.EventHandler(this.SAChange);
            // 
            // llSubLot
            // 
            this.llSubLot.AutoSize = true;
            this.llSubLot.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.llSubLot.Location = new System.Drawing.Point(-2, 24);
            this.llSubLot.Name = "llSubLot";
            this.llSubLot.Size = new System.Drawing.Size(55, 13);
            this.llSubLot.TabIndex = 1;
            this.llSubLot.TabStop = true;
            this.llSubLot.Text = "SubLot:";
            this.llSubLot.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label31.Location = new System.Drawing.Point(169, 51);
            this.label31.Name = "label31";
            this.label31.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.label31.Size = new System.Drawing.Size(48, 13);
            this.label31.TabIndex = 7;
            this.label31.Text = "SLu3:";
            // 
            // tbSAu3
            // 
            this.tbSAu3.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.tbSAu3.Location = new System.Drawing.Point(223, 47);
            this.tbSAu3.Name = "tbSAu3";
            this.tbSAu3.Size = new System.Drawing.Size(86, 21);
            this.tbSAu3.TabIndex = 8;
            this.tbSAu3.Text = "0x00000000";
            this.tbSAu3.TextChanged += new System.EventHandler(this.SAChange);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label30.Location = new System.Drawing.Point(169, 24);
            this.label30.Name = "label30";
            this.label30.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.label30.Size = new System.Drawing.Size(48, 13);
            this.label30.TabIndex = 5;
            this.label30.Text = "SLu2:";
            // 
            // tbSAFamily
            // 
            this.tbSAFamily.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.tbSAFamily.Location = new System.Drawing.Point(59, 47);
            this.tbSAFamily.Name = "tbSAFamily";
            this.tbSAFamily.Size = new System.Drawing.Size(86, 21);
            this.tbSAFamily.TabIndex = 4;
            this.tbSAFamily.Text = "0x00000000";
            this.tbSAFamily.TextChanged += new System.EventHandler(this.SAChange);
            // 
            // lbApts
            // 
            this.lbApts.ColumnWidth = 98;
            this.lbApts.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lbApts.IntegralHeight = false;
            this.lbApts.Items.AddRange(new object[] {
            "0x00000000",
            "0x00000000",
            "0x00000000",
            "0x00000000"});
            this.lbApts.Location = new System.Drawing.Point(224, 10);
            this.lbApts.MinimumSize = new System.Drawing.Size(0, 44);
            this.lbApts.MultiColumn = true;
            this.lbApts.Name = "lbApts";
            this.lbApts.Size = new System.Drawing.Size(206, 59);
            this.lbApts.TabIndex = 2;
            this.lbApts.SelectedIndexChanged += new System.EventHandler(this.lbApts_SelectedIndexChanged);
            // 
            // tbApBase
            // 
            this.tbApBase.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.tbApBase.Location = new System.Drawing.Point(131, 43);
            this.tbApBase.Name = "tbApBase";
            this.tbApBase.Size = new System.Drawing.Size(86, 21);
            this.tbApBase.TabIndex = 6;
            this.tbApBase.Text = "0x00000000";
            this.tbApBase.TextChanged += new System.EventHandler(this.tbApBase_TextChanged);
            // 
            // llAptBase
            // 
            this.llAptBase.AutoSize = true;
            this.llAptBase.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.llAptBase.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llAptBase.Location = new System.Drawing.Point(6, 47);
            this.llAptBase.Name = "llAptBase";
            this.llAptBase.Size = new System.Drawing.Size(115, 13);
            this.llAptBase.TabIndex = 1;
            this.llAptBase.TabStop = true;
            this.llAptBase.Text = "Apartment base:";
            this.llAptBase.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_Click);
            // 
            // btnDelApt
            // 
            this.btnDelApt.AutoSize = true;
            this.btnDelApt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDelApt.Enabled = false;
            this.btnDelApt.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDelApt.Location = new System.Drawing.Point(42, 20);
            this.btnDelApt.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelApt.Name = "btnDelApt";
            this.btnDelApt.Size = new System.Drawing.Size(38, 23);
            this.btnDelApt.TabIndex = 3;
            this.btnDelApt.Text = "Del";
            this.btnDelApt.UseVisualStyleBackColor = true;
            this.btnDelApt.Click += new System.EventHandler(this.btnDelApt_Click);
            // 
            // btnAddApt
            // 
            this.btnAddApt.AutoSize = true;
            this.btnAddApt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddApt.Location = new System.Drawing.Point(3, 20);
            this.btnAddApt.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddApt.Name = "btnAddApt";
            this.btnAddApt.Size = new System.Drawing.Size(42, 23);
            this.btnAddApt.TabIndex = 3;
            this.btnAddApt.Text = "Add";
            this.btnAddApt.UseVisualStyleBackColor = true;
            this.btnAddApt.Click += new System.EventHandler(this.btnAddApt_Click);
            // 
            // tbdesc
            // 
            this.tbdesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbdesc.Location = new System.Drawing.Point(296, 26);
            this.tbdesc.Name = "tbdesc";
            this.tbdesc.Size = new System.Drawing.Size(697, 21);
            this.tbdesc.TabIndex = 6;
            this.tbdesc.TextChanged += new System.EventHandler(this.CommonChange);
            // 
            // gbtravel
            // 
            this.gbtravel.BackColor = System.Drawing.Color.Transparent;
            this.gbtravel.Controls.Add(this.cbtrjflag5);
            this.gbtravel.Controls.Add(this.cbtrjflag4);
            this.gbtravel.Controls.Add(this.cbtrjflag3);
            this.gbtravel.Controls.Add(this.cbtrjflag2);
            this.gbtravel.Controls.Add(this.cbtrjflag1);
            this.gbtravel.Controls.Add(this.cbtrjungle);
            this.gbtravel.Controls.Add(this.cbtrhidec);
            this.gbtravel.Controls.Add(this.cbtrpool);
            this.gbtravel.Controls.Add(this.cbtrmale);
            this.gbtravel.Controls.Add(this.cbtrfem);
            this.gbtravel.Controls.Add(this.cbtrbeach);
            this.gbtravel.Controls.Add(this.cbtrformal);
            this.gbtravel.Controls.Add(this.cbtrteen);
            this.gbtravel.Controls.Add(this.cbtrnude);
            this.gbtravel.Controls.Add(this.cbtrpern);
            this.gbtravel.Controls.Add(this.cgtrwhite);
            this.gbtravel.Controls.Add(this.cbtrblue);
            this.gbtravel.Controls.Add(this.cbtrredred);
            this.gbtravel.Controls.Add(this.cbtradult);
            this.gbtravel.Controls.Add(this.cbtrclub);
            this.gbtravel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbtravel.Location = new System.Drawing.Point(372, 408);
            this.gbtravel.Name = "gbtravel";
            this.gbtravel.Size = new System.Drawing.Size(621, 108);
            this.gbtravel.TabIndex = 1;
            this.gbtravel.TabStop = false;
            this.gbtravel.Text = "Travel Flags";
            this.gbtravel.Visible = false;
            // 
            // cbtrjflag5
            // 
            this.cbtrjflag5.AutoSize = true;
            this.cbtrjflag5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbtrjflag5.Location = new System.Drawing.Point(500, 85);
            this.cbtrjflag5.Name = "cbtrjflag5";
            this.cbtrjflag5.Size = new System.Drawing.Size(74, 17);
            this.cbtrjflag5.TabIndex = 19;
            this.cbtrjflag5.Text = "Flag 30";
            this.cbtrjflag5.UseVisualStyleBackColor = true;
            this.cbtrjflag5.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbtrjflag4
            // 
            this.cbtrjflag4.AutoSize = true;
            this.cbtrjflag4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbtrjflag4.Location = new System.Drawing.Point(500, 63);
            this.cbtrjflag4.Name = "cbtrjflag4";
            this.cbtrjflag4.Size = new System.Drawing.Size(112, 17);
            this.cbtrjflag4.TabIndex = 18;
            this.cbtrjflag4.Text = "Jungle Flag 4";
            this.cbtrjflag4.UseVisualStyleBackColor = true;
            this.cbtrjflag4.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbtrjflag3
            // 
            this.cbtrjflag3.AutoSize = true;
            this.cbtrjflag3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbtrjflag3.Location = new System.Drawing.Point(500, 41);
            this.cbtrjflag3.Name = "cbtrjflag3";
            this.cbtrjflag3.Size = new System.Drawing.Size(112, 17);
            this.cbtrjflag3.TabIndex = 17;
            this.cbtrjflag3.Text = "Jungle Flag 3";
            this.cbtrjflag3.UseVisualStyleBackColor = true;
            this.cbtrjflag3.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbtrjflag2
            // 
            this.cbtrjflag2.AutoSize = true;
            this.cbtrjflag2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbtrjflag2.Location = new System.Drawing.Point(500, 19);
            this.cbtrjflag2.Name = "cbtrjflag2";
            this.cbtrjflag2.Size = new System.Drawing.Size(112, 17);
            this.cbtrjflag2.TabIndex = 16;
            this.cbtrjflag2.Text = "Jungle Flag 2";
            this.cbtrjflag2.UseVisualStyleBackColor = true;
            this.cbtrjflag2.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbtrjflag1
            // 
            this.cbtrjflag1.AutoSize = true;
            this.cbtrjflag1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbtrjflag1.Location = new System.Drawing.Point(370, 85);
            this.cbtrjflag1.Name = "cbtrjflag1";
            this.cbtrjflag1.Size = new System.Drawing.Size(112, 17);
            this.cbtrjflag1.TabIndex = 15;
            this.cbtrjflag1.Text = "Jungle Flag 1";
            this.cbtrjflag1.UseVisualStyleBackColor = true;
            this.cbtrjflag1.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbtrjungle
            // 
            this.cbtrjungle.AutoSize = true;
            this.cbtrjungle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbtrjungle.Location = new System.Drawing.Point(370, 63);
            this.cbtrjungle.Name = "cbtrjungle";
            this.cbtrjungle.Size = new System.Drawing.Size(92, 17);
            this.cbtrjungle.TabIndex = 14;
            this.cbtrjungle.Text = "Jungle Lot";
            this.cbtrjungle.UseVisualStyleBackColor = true;
            this.cbtrjungle.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbtrhidec
            // 
            this.cbtrhidec.AutoSize = true;
            this.cbtrhidec.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbtrhidec.Location = new System.Drawing.Point(370, 41);
            this.cbtrhidec.Name = "cbtrhidec";
            this.cbtrhidec.Size = new System.Drawing.Size(127, 17);
            this.cbtrhidec.TabIndex = 13;
            this.cbtrhidec.Text = "Hidden Com Lot";
            this.cbtrhidec.UseVisualStyleBackColor = true;
            this.cbtrhidec.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbtrpool
            // 
            this.cbtrpool.AutoSize = true;
            this.cbtrpool.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbtrpool.Location = new System.Drawing.Point(370, 19);
            this.cbtrpool.Name = "cbtrpool";
            this.cbtrpool.Size = new System.Drawing.Size(117, 17);
            this.cbtrpool.TabIndex = 12;
            this.cbtrpool.Text = "Lot has a Pool";
            this.cbtrpool.UseVisualStyleBackColor = true;
            this.cbtrpool.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbtrmale
            // 
            this.cbtrmale.AutoSize = true;
            this.cbtrmale.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbtrmale.Location = new System.Drawing.Point(254, 85);
            this.cbtrmale.Name = "cbtrmale";
            this.cbtrmale.Size = new System.Drawing.Size(96, 17);
            this.cbtrmale.TabIndex = 11;
            this.cbtrmale.Text = "Males Only";
            this.cbtrmale.UseVisualStyleBackColor = true;
            this.cbtrmale.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbtrfem
            // 
            this.cbtrfem.AutoSize = true;
            this.cbtrfem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbtrfem.Location = new System.Drawing.Point(254, 63);
            this.cbtrfem.Name = "cbtrfem";
            this.cbtrfem.Size = new System.Drawing.Size(114, 17);
            this.cbtrfem.TabIndex = 10;
            this.cbtrfem.Text = "Females Only";
            this.cbtrfem.UseVisualStyleBackColor = true;
            this.cbtrfem.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbtrbeach
            // 
            this.cbtrbeach.AutoSize = true;
            this.cbtrbeach.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbtrbeach.Location = new System.Drawing.Point(254, 41);
            this.cbtrbeach.Name = "cbtrbeach";
            this.cbtrbeach.Size = new System.Drawing.Size(89, 17);
            this.cbtrbeach.TabIndex = 9;
            this.cbtrbeach.Text = "Beach Lot";
            this.cbtrbeach.UseVisualStyleBackColor = true;
            this.cbtrbeach.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbtrformal
            // 
            this.cbtrformal.AutoSize = true;
            this.cbtrformal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbtrformal.Location = new System.Drawing.Point(254, 19);
            this.cbtrformal.Name = "cbtrformal";
            this.cbtrformal.Size = new System.Drawing.Size(110, 17);
            this.cbtrformal.TabIndex = 8;
            this.cbtrformal.Text = "Wear Formal";
            this.cbtrformal.UseVisualStyleBackColor = true;
            this.cbtrformal.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbtrteen
            // 
            this.cbtrteen.AutoSize = true;
            this.cbtrteen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbtrteen.Location = new System.Drawing.Point(124, 85);
            this.cbtrteen.Name = "cbtrteen";
            this.cbtrteen.Size = new System.Drawing.Size(102, 17);
            this.cbtrteen.TabIndex = 7;
            this.cbtrteen.Text = "Teen Phone";
            this.cbtrteen.UseVisualStyleBackColor = true;
            this.cbtrteen.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbtrnude
            // 
            this.cbtrnude.AutoSize = true;
            this.cbtrnude.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbtrnude.Location = new System.Drawing.Point(124, 63);
            this.cbtrnude.Name = "cbtrnude";
            this.cbtrnude.Size = new System.Drawing.Size(91, 17);
            this.cbtrnude.TabIndex = 6;
            this.cbtrnude.Text = "Nudist Lot";
            this.cbtrnude.UseVisualStyleBackColor = true;
            this.cbtrnude.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbtrpern
            // 
            this.cbtrpern.AutoSize = true;
            this.cbtrpern.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbtrpern.Location = new System.Drawing.Point(124, 41);
            this.cbtrpern.Name = "cbtrpern";
            this.cbtrpern.Size = new System.Drawing.Size(108, 17);
            this.cbtrpern.TabIndex = 5;
            this.cbtrpern.Text = "Porn Cinema";
            this.cbtrpern.UseVisualStyleBackColor = true;
            this.cbtrpern.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cgtrwhite
            // 
            this.cgtrwhite.AutoSize = true;
            this.cgtrwhite.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cgtrwhite.Location = new System.Drawing.Point(124, 19);
            this.cgtrwhite.Name = "cgtrwhite";
            this.cgtrwhite.Size = new System.Drawing.Size(124, 17);
            this.cgtrwhite.TabIndex = 4;
            this.cgtrwhite.Text = "White Red light";
            this.cgtrwhite.UseVisualStyleBackColor = true;
            this.cgtrwhite.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbtrblue
            // 
            this.cbtrblue.AutoSize = true;
            this.cbtrblue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbtrblue.Location = new System.Drawing.Point(6, 85);
            this.cbtrblue.Name = "cbtrblue";
            this.cbtrblue.Size = new System.Drawing.Size(115, 17);
            this.cbtrblue.TabIndex = 3;
            this.cbtrblue.Text = "Blue Red light";
            this.cbtrblue.UseVisualStyleBackColor = true;
            this.cbtrblue.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbtrredred
            // 
            this.cbtrredred.AutoSize = true;
            this.cbtrredred.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbtrredred.Location = new System.Drawing.Point(6, 63);
            this.cbtrredred.Name = "cbtrredred";
            this.cbtrredred.Size = new System.Drawing.Size(111, 17);
            this.cbtrredred.TabIndex = 2;
            this.cbtrredred.Text = "Red Red light";
            this.cbtrredred.UseVisualStyleBackColor = true;
            this.cbtrredred.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbtradult
            // 
            this.cbtradult.AutoSize = true;
            this.cbtradult.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbtradult.Location = new System.Drawing.Point(6, 41);
            this.cbtradult.Name = "cbtradult";
            this.cbtradult.Size = new System.Drawing.Size(100, 17);
            this.cbtradult.TabIndex = 1;
            this.cbtradult.Text = "Adults Only";
            this.cbtradult.UseVisualStyleBackColor = true;
            this.cbtradult.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbtrclub
            // 
            this.cbtrclub.AutoSize = true;
            this.cbtrclub.Location = new System.Drawing.Point(6, 19);
            this.cbtrclub.Name = "cbtrclub";
            this.cbtrclub.Size = new System.Drawing.Size(110, 17);
            this.cbtrclub.TabIndex = 0;
            this.cbtrclub.Text = "Woohoo Club";
            this.cbtrclub.UseVisualStyleBackColor = true;
            this.cbtrclub.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(210, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Description:";
            // 
            // tblotname
            // 
            this.tblotname.Location = new System.Drawing.Point(296, 57);
            this.tblotname.Name = "tblotname";
            this.tblotname.Size = new System.Drawing.Size(697, 21);
            this.tblotname.TabIndex = 2;
            this.tblotname.TextChanged += new System.EventHandler(this.CommonChange);
            // 
            // gbhobby
            // 
            this.gbhobby.BackColor = System.Drawing.Color.Transparent;
            this.gbhobby.Controls.Add(this.cbhbmusic);
            this.gbhobby.Controls.Add(this.cbhbsport);
            this.gbhobby.Controls.Add(this.cbhbscience);
            this.gbhobby.Controls.Add(this.cbhbfitness);
            this.gbhobby.Controls.Add(this.cbhbtinker);
            this.gbhobby.Controls.Add(this.cbhbnature);
            this.gbhobby.Controls.Add(this.cbhbgames);
            this.gbhobby.Controls.Add(this.cbhbfilm);
            this.gbhobby.Controls.Add(this.cbhbart);
            this.gbhobby.Controls.Add(this.cbhbcook);
            this.gbhobby.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbhobby.Location = new System.Drawing.Point(30, 408);
            this.gbhobby.Name = "gbhobby";
            this.gbhobby.Size = new System.Drawing.Size(338, 108);
            this.gbhobby.TabIndex = 0;
            this.gbhobby.TabStop = false;
            this.gbhobby.Text = "Hobby Flags";
            this.gbhobby.Visible = false;
            // 
            // cbhbmusic
            // 
            this.cbhbmusic.AutoSize = true;
            this.cbhbmusic.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbhbmusic.Location = new System.Drawing.Point(194, 41);
            this.cbhbmusic.Name = "cbhbmusic";
            this.cbhbmusic.Size = new System.Drawing.Size(134, 17);
            this.cbhbmusic.TabIndex = 9;
            this.cbhbmusic.Text = "Music and Dance";
            this.cbhbmusic.UseVisualStyleBackColor = true;
            this.cbhbmusic.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbhbsport
            // 
            this.cbhbsport.AutoSize = true;
            this.cbhbsport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbhbsport.Location = new System.Drawing.Point(6, 85);
            this.cbhbsport.Name = "cbhbsport";
            this.cbhbsport.Size = new System.Drawing.Size(68, 17);
            this.cbhbsport.TabIndex = 3;
            this.cbhbsport.Text = "Sports";
            this.cbhbsport.UseVisualStyleBackColor = true;
            this.cbhbsport.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbhbscience
            // 
            this.cbhbscience.AutoSize = true;
            this.cbhbscience.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbhbscience.Location = new System.Drawing.Point(194, 19);
            this.cbhbscience.Name = "cbhbscience";
            this.cbhbscience.Size = new System.Drawing.Size(76, 17);
            this.cbhbscience.TabIndex = 8;
            this.cbhbscience.Text = "Science";
            this.cbhbscience.UseVisualStyleBackColor = true;
            this.cbhbscience.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbhbfitness
            // 
            this.cbhbfitness.AutoSize = true;
            this.cbhbfitness.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbhbfitness.Location = new System.Drawing.Point(117, 85);
            this.cbhbfitness.Name = "cbhbfitness";
            this.cbhbfitness.Size = new System.Drawing.Size(73, 17);
            this.cbhbfitness.TabIndex = 7;
            this.cbhbfitness.Text = "Fitness";
            this.cbhbfitness.UseVisualStyleBackColor = true;
            this.cbhbfitness.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbhbtinker
            // 
            this.cbhbtinker.AutoSize = true;
            this.cbhbtinker.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbhbtinker.Location = new System.Drawing.Point(117, 63);
            this.cbhbtinker.Name = "cbhbtinker";
            this.cbhbtinker.Size = new System.Drawing.Size(68, 17);
            this.cbhbtinker.TabIndex = 6;
            this.cbhbtinker.Text = "Tinker";
            this.cbhbtinker.UseVisualStyleBackColor = true;
            this.cbhbtinker.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbhbnature
            // 
            this.cbhbnature.AutoSize = true;
            this.cbhbnature.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbhbnature.Location = new System.Drawing.Point(117, 41);
            this.cbhbnature.Name = "cbhbnature";
            this.cbhbnature.Size = new System.Drawing.Size(70, 17);
            this.cbhbnature.TabIndex = 5;
            this.cbhbnature.Text = "Nature";
            this.cbhbnature.UseVisualStyleBackColor = true;
            this.cbhbnature.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbhbgames
            // 
            this.cbhbgames.AutoSize = true;
            this.cbhbgames.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbhbgames.Location = new System.Drawing.Point(117, 19);
            this.cbhbgames.Name = "cbhbgames";
            this.cbhbgames.Size = new System.Drawing.Size(70, 17);
            this.cbhbgames.TabIndex = 4;
            this.cbhbgames.Text = "Games";
            this.cbhbgames.UseVisualStyleBackColor = true;
            this.cbhbgames.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbhbfilm
            // 
            this.cbhbfilm.AutoSize = true;
            this.cbhbfilm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbhbfilm.Location = new System.Drawing.Point(6, 63);
            this.cbhbfilm.Name = "cbhbfilm";
            this.cbhbfilm.Size = new System.Drawing.Size(102, 17);
            this.cbhbfilm.TabIndex = 2;
            this.cbhbfilm.Text = "Film and Lit";
            this.cbhbfilm.UseVisualStyleBackColor = true;
            this.cbhbfilm.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbhbart
            // 
            this.cbhbart.AutoSize = true;
            this.cbhbart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbhbart.Location = new System.Drawing.Point(6, 41);
            this.cbhbart.Name = "cbhbart";
            this.cbhbart.Size = new System.Drawing.Size(110, 17);
            this.cbhbart.TabIndex = 1;
            this.cbhbart.Text = "Art and Craft";
            this.cbhbart.UseVisualStyleBackColor = true;
            this.cbhbart.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // cbhbcook
            // 
            this.cbhbcook.AutoSize = true;
            this.cbhbcook.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbhbcook.Location = new System.Drawing.Point(6, 19);
            this.cbhbcook.Name = "cbhbcook";
            this.cbhbcook.Size = new System.Drawing.Size(78, 17);
            this.cbhbcook.TabIndex = 0;
            this.cbhbcook.Text = "Cooking";
            this.cbhbcook.UseVisualStyleBackColor = true;
            this.cbhbcook.CheckedChanged += new System.EventHandler(this.hobbytravel_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(223, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Lot Name:";
            // 
            // llunknone
            // 
            this.llunknone.AutoSize = true;
            this.llunknone.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.llunknone.Location = new System.Drawing.Point(41, 408);
            this.llunknone.Name = "llunknone";
            this.llunknone.Size = new System.Drawing.Size(70, 13);
            this.llunknone.TabIndex = 102;
            this.llunknone.TabStop = true;
            this.llunknone.Text = "Unknown:";
            this.llunknone.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llunknone_LinkClicked);
            // 
            // gbFlagg
            // 
            this.gbFlagg.Controls.Add(this.tbu0);
            this.gbFlagg.Controls.Add(this.label21);
            this.gbFlagg.Controls.Add(this.cbBeachy);
            this.gbFlagg.Controls.Add(this.cbhidim);
            this.gbFlagg.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbFlagg.Location = new System.Drawing.Point(693, 90);
            this.gbFlagg.Name = "gbFlagg";
            this.gbFlagg.Size = new System.Drawing.Size(141, 94);
            this.gbFlagg.TabIndex = 101;
            this.gbFlagg.TabStop = false;
            this.gbFlagg.Text = "Lot Flags";
            // 
            // tbu0
            // 
            this.tbu0.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.tbu0.Location = new System.Drawing.Point(54, 19);
            this.tbu0.Name = "tbu0";
            this.tbu0.Size = new System.Drawing.Size(79, 21);
            this.tbu0.TabIndex = 24;
            this.tbu0.Text = "0x00000000";
            this.tbu0.TextChanged += new System.EventHandler(this.CommonChange);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label21.Location = new System.Drawing.Point(7, 23);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(47, 13);
            this.label21.TabIndex = 23;
            this.label21.Text = "Value:";
            // 
            // cbBeachy
            // 
            this.cbBeachy.AutoSize = true;
            this.cbBeachy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.cbBeachy.Location = new System.Drawing.Point(7, 67);
            this.cbBeachy.Name = "cbBeachy";
            this.cbBeachy.Size = new System.Drawing.Size(93, 17);
            this.cbBeachy.TabIndex = 54;
            this.cbBeachy.Text = "Has Beach";
            this.cbBeachy.UseVisualStyleBackColor = true;
            this.cbBeachy.CheckedChanged += new System.EventHandler(this.cbhidim_CheckedChanged);
            // 
            // cbhidim
            // 
            this.cbhidim.AutoSize = true;
            this.cbhidim.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.cbhidim.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cbhidim.Location = new System.Drawing.Point(7, 43);
            this.cbhidim.Margin = new System.Windows.Forms.Padding(1, 6, 0, 0);
            this.cbhidim.Name = "cbhidim";
            this.cbhidim.Size = new System.Drawing.Size(71, 17);
            this.cbhidim.TabIndex = 25;
            this.cbhidim.Text = "Hidden";
            this.cbhidim.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbhidim.UseVisualStyleBackColor = true;
            this.cbhidim.CheckedChanged += new System.EventHandler(this.cbhidim_CheckedChanged);
            // 
            // gbclarse
            // 
            this.gbclarse.Controls.Add(this.label11);
            this.gbclarse.Controls.Add(this.cbLotClas);
            this.gbclarse.Controls.Add(this.tbcset);
            this.gbclarse.Controls.Add(this.tblotclass);
            this.gbclarse.Controls.Add(this.label17);
            this.gbclarse.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbclarse.Location = new System.Drawing.Point(845, 90);
            this.gbclarse.Name = "gbclarse";
            this.gbclarse.Size = new System.Drawing.Size(148, 94);
            this.gbclarse.TabIndex = 101;
            this.gbclarse.TabStop = false;
            this.gbclarse.Text = "Lot Class";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 54;
            this.label11.Text = "Is Set:";
            // 
            // cbLotClas
            // 
            this.cbLotClas.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.cbLotClas.FormattingEnabled = true;
            this.cbLotClas.Items.AddRange(new object[] {
            "Not Set",
            "Low",
            "Medium",
            "High"});
            this.cbLotClas.Location = new System.Drawing.Point(57, 16);
            this.cbLotClas.Name = "cbLotClas";
            this.cbLotClas.Size = new System.Drawing.Size(86, 21);
            this.cbLotClas.TabIndex = 0;
            this.cbLotClas.SelectedIndexChanged += new System.EventHandler(this.cbhidim_CheckedChanged);
            // 
            // tbcset
            // 
            this.tbcset.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.tbcset.Location = new System.Drawing.Point(57, 68);
            this.tbcset.Name = "tbcset";
            this.tbcset.Size = new System.Drawing.Size(86, 21);
            this.tbcset.TabIndex = 53;
            // 
            // tblotclass
            // 
            this.tblotclass.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.tblotclass.Location = new System.Drawing.Point(57, 42);
            this.tblotclass.Name = "tblotclass";
            this.tblotclass.Size = new System.Drawing.Size(86, 21);
            this.tblotclass.TabIndex = 26;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label17.Location = new System.Drawing.Point(3, 46);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(51, 13);
            this.label17.TabIndex = 27;
            this.label17.Text = "Value :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(4, 248);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Elevation offsets:";
            // 
            // lb
            // 
            this.lb.ColumnWidth = 98;
            this.lb.Items.AddRange(new object[] {
            "0,0",
            "0,1",
            "0,2",
            "0,3",
            "0,4",
            "0,5",
            "0,6",
            "0,0",
            "0,1",
            "0,2",
            "0,3",
            "0,4",
            "0,5",
            "1,6",
            "0,0",
            "0,1",
            "0,2",
            "0,3",
            "0,4",
            "0,5",
            "2,6",
            "0,0",
            "0,1",
            "0,2",
            "0,3",
            "0,4",
            "0,5",
            "3,6",
            "0,0",
            "0,1",
            "0,2",
            "0,3",
            "0,4",
            "0,5",
            "4,6",
            "0,0",
            "0,1",
            "0,2",
            "0,3",
            "0,4",
            "0,5",
            "5,6",
            "0,0",
            "0,1",
            "0,2"});
            this.lb.Location = new System.Drawing.Point(125, 240);
            this.lb.MultiColumn = true;
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(868, 82);
            this.lb.TabIndex = 2;
            this.lb.SelectedIndexChanged += new System.EventHandler(this.lb_SelectedIndexChanged);
            // 
            // tbElevationAt
            // 
            this.tbElevationAt.Location = new System.Drawing.Point(22, 268);
            this.tbElevationAt.Name = "tbElevationAt";
            this.tbElevationAt.Size = new System.Drawing.Size(100, 21);
            this.tbElevationAt.TabIndex = 2;
            this.tbElevationAt.Text = "0.0";
            this.tbElevationAt.TextChanged += new System.EventHandler(this.tbElevationAt_TextChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label25.Location = new System.Drawing.Point(219, 216);
            this.label25.Margin = new System.Windows.Forms.Padding(3);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(76, 13);
            this.label25.TabIndex = 3;
            this.label25.Text = "Lot Owner:";
            this.label25.DoubleClick += new System.EventHandler(this.label25_Click);
            // 
            // tbowner
            // 
            this.tbowner.Location = new System.Drawing.Point(296, 212);
            this.tbowner.Name = "tbowner";
            this.tbowner.Size = new System.Drawing.Size(86, 21);
            this.tbowner.TabIndex = 4;
            this.tbowner.Text = "0x00000000";
            this.tbowner.TextChanged += new System.EventHandler(this.CommonChange);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(227, 185);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(68, 13);
            this.label15.TabIndex = 1;
            this.label15.Text = "Instance:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(247, 123);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Width:";
            // 
            // bthbytrvl
            // 
            this.bthbytrvl.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bthbytrvl.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.bthbytrvl.Location = new System.Drawing.Point(398, 180);
            this.bthbytrvl.Name = "bthbytrvl";
            this.bthbytrvl.Size = new System.Drawing.Size(151, 23);
            this.bthbytrvl.TabIndex = 5;
            this.bthbytrvl.Text = "Hobby+Travel Flags:";
            this.bthbytrvl.UseVisualStyleBackColor = true;
            this.bthbytrvl.Click += new System.EventHandler(this.Openpntravel);
            // 
            // tbinst
            // 
            this.tbinst.Location = new System.Drawing.Point(296, 181);
            this.tbinst.Name = "tbinst";
            this.tbinst.Size = new System.Drawing.Size(86, 21);
            this.tbinst.TabIndex = 2;
            this.tbinst.Text = "0x00000000";
            this.tbinst.TextChanged += new System.EventHandler(this.CommonChange);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(211, 154);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(84, 13);
            this.label14.TabIndex = 19;
            this.label14.Text = "Orientation:";
            // 
            // tbu4
            // 
            this.tbu4.Location = new System.Drawing.Point(553, 181);
            this.tbu4.Name = "tbu4";
            this.tbu4.Size = new System.Drawing.Size(86, 21);
            this.tbu4.TabIndex = 6;
            this.tbu4.Text = "0x00000000";
            this.tbu4.TextChanged += new System.EventHandler(this.CommonChange);
            // 
            // cborient
            // 
            this.cborient.Enum = null;
            this.cborient.Location = new System.Drawing.Point(296, 150);
            this.cborient.Name = "cborient";
            this.cborient.ResourceManager = null;
            this.cborient.Size = new System.Drawing.Size(64, 21);
            this.cborient.TabIndex = 20;
            this.cborient.SelectedIndexChanged += new System.EventHandler(this.CommonChange);
            // 
            // tbTexture
            // 
            this.tbTexture.Location = new System.Drawing.Point(469, 212);
            this.tbTexture.Name = "tbTexture";
            this.tbTexture.Size = new System.Drawing.Size(186, 21);
            this.tbTexture.TabIndex = 4;
            this.tbTexture.TextChanged += new System.EventHandler(this.CommonChange);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(595, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Roads:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(395, 216);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Texture:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(372, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Rotation:";
            // 
            // tbwd
            // 
            this.tbwd.Location = new System.Drawing.Point(296, 119);
            this.tbwd.Name = "tbwd";
            this.tbwd.Size = new System.Drawing.Size(40, 21);
            this.tbwd.TabIndex = 10;
            this.tbwd.Text = "0x00";
            this.tbwd.TextChanged += new System.EventHandler(this.CommonChange);
            // 
            // tbrotation
            // 
            this.tbrotation.Location = new System.Drawing.Point(437, 150);
            this.tbrotation.Name = "tbrotation";
            this.tbrotation.Size = new System.Drawing.Size(40, 21);
            this.tbrotation.TabIndex = 22;
            this.tbrotation.Text = "0x00";
            this.tbrotation.TextChanged += new System.EventHandler(this.CommonChange);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(342, 123);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Height:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(235, 92);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Version:";
            // 
            // tbhg
            // 
            this.tbhg.Location = new System.Drawing.Point(395, 119);
            this.tbhg.Name = "tbhg";
            this.tbhg.Size = new System.Drawing.Size(40, 21);
            this.tbhg.TabIndex = 12;
            this.tbhg.Text = "0x00";
            this.tbhg.TextChanged += new System.EventHandler(this.CommonChange);
            // 
            // tbRoads
            // 
            this.tbRoads.Location = new System.Drawing.Point(645, 150);
            this.tbRoads.Name = "tbRoads";
            this.tbRoads.Size = new System.Drawing.Size(40, 21);
            this.tbRoads.TabIndex = 8;
            this.tbRoads.Text = "0x00";
            this.tbRoads.TextChanged += new System.EventHandler(this.CommonChange);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(441, 123);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Top:";
            // 
            // tbver
            // 
            this.tbver.BackColor = System.Drawing.SystemColors.Window;
            this.tbver.Location = new System.Drawing.Point(296, 88);
            this.tbver.Name = "tbver";
            this.tbver.ReadOnly = true;
            this.tbver.Size = new System.Drawing.Size(64, 21);
            this.tbver.TabIndex = 2;
            this.tbver.Text = "0x0000";
            // 
            // tbtop
            // 
            this.tbtop.Location = new System.Drawing.Point(476, 119);
            this.tbtop.Name = "tbtop";
            this.tbtop.Size = new System.Drawing.Size(40, 21);
            this.tbtop.TabIndex = 14;
            this.tbtop.Text = "0x00";
            this.tbtop.TextChanged += new System.EventHandler(this.CommonChange);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(522, 123);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "Left:";
            // 
            // tbsubver
            // 
            this.tbsubver.BackColor = System.Drawing.SystemColors.Window;
            this.tbsubver.Location = new System.Drawing.Point(366, 88);
            this.tbsubver.Name = "tbsubver";
            this.tbsubver.ReadOnly = true;
            this.tbsubver.Size = new System.Drawing.Size(64, 21);
            this.tbsubver.TabIndex = 3;
            this.tbsubver.Text = "0x0000";
            // 
            // tbleft
            // 
            this.tbleft.Location = new System.Drawing.Point(558, 119);
            this.tbleft.Name = "tbleft";
            this.tbleft.Size = new System.Drawing.Size(40, 21);
            this.tbleft.TabIndex = 16;
            this.tbleft.Text = "0x00";
            this.tbleft.TextChanged += new System.EventHandler(this.CommonChange);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label20.Location = new System.Drawing.Point(484, 154);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(19, 13);
            this.label20.TabIndex = 17;
            this.label20.Text = "Z:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(436, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Lot Type:";
            // 
            // tbz
            // 
            this.tbz.Location = new System.Drawing.Point(503, 150);
            this.tbz.Name = "tbz";
            this.tbz.Size = new System.Drawing.Size(86, 21);
            this.tbz.TabIndex = 18;
            this.tbz.Text = "0.0";
            this.tbz.TextChanged += new System.EventHandler(this.CommonChange);
            // 
            // cbtype
            // 
            this.cbtype.Location = new System.Drawing.Point(503, 88);
            this.cbtype.Name = "cbtype";
            this.cbtype.Size = new System.Drawing.Size(132, 21);
            this.cbtype.TabIndex = 5;
            this.cbtype.SelectedIndexChanged += new System.EventHandler(this.SelectType);
            // 
            // tbtype
            // 
            this.tbtype.BackColor = System.Drawing.SystemColors.Window;
            this.tbtype.Location = new System.Drawing.Point(637, 88);
            this.tbtype.Name = "tbtype";
            this.tbtype.ReadOnly = true;
            this.tbtype.Size = new System.Drawing.Size(40, 21);
            this.tbtype.TabIndex = 6;
            this.tbtype.Text = "0x00";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.CanCommit = true;
            this.panel2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.panel2.HeaderText = "Lot Description Editor";
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1000, 24);
            this.panel2.TabIndex = 0;
            this.panel2.OnCommit += new booby.panelheader.EventHandler(this.Commit);
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(5, 26);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(200, 200);
            this.pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb.TabIndex = 52;
            this.pb.TabStop = false;
            // 
            // LtxtForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(1023, 554);
            this.Controls.Add(this.ltxtPanel);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.Name = "LtxtForm";
            this.Text = "LtxtForm";
            this.ltxtPanel.ResumeLayout(false);
            this.ltxtPanel.PerformLayout();
            this.gbunown.ResumeLayout(false);
            this.gbunown.PerformLayout();
            this.gbApart.ResumeLayout(false);
            this.gbApart.PerformLayout();
            this.gbApartment.ResumeLayout(false);
            this.gbApartment.PerformLayout();
            this.gbtravel.ResumeLayout(false);
            this.gbtravel.PerformLayout();
            this.gbhobby.ResumeLayout(false);
            this.gbhobby.PerformLayout();
            this.gbFlagg.ResumeLayout(false);
            this.gbFlagg.PerformLayout();
            this.gbclarse.ResumeLayout(false);
            this.gbclarse.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        internal Ltxt wrapper;

        private void SelectType(object sender, System.EventArgs e)
        {
            if (wrapper == null) return;
            if (Enum.IsDefined(typeof(Ltxt.LotType), cbtype.SelectedItem))
                wrapper.Type = (Ltxt.LotType)cbtype.SelectedItem;
            else
                wrapper.Type = Ltxt.LotType.Unknown;
            tbtype.Text = "0x" + Helper.HexString((byte)wrapper.Type);
            btnAddApt.Enabled = btnDelApt.Enabled = (wrapper.Type == Ltxt.LotType.ApartmentBase);
            cbtrclub.Enabled = cbtrhidec.Enabled = gbhobby.Enabled = (wrapper.Type == Ltxt.LotType.Hobby);
            if (wrapper.SubVersion >= LtxtSubVersion.Freetime)
                bthbytrvl.Enabled = (wrapper.Type == Ltxt.LotType.Hobby || booby.PrettyGirls.IsTitsInstalled() || booby.PrettyGirls.IsAngelsInstalled());
            if (wrapper.Type == Ltxt.LotType.ApartmentBase || wrapper.Type == Ltxt.LotType.ApartmentSublot)
            {
                gbApart.Visible = true;
                gbunown.Location = new System.Drawing.Point(116, 408);
                llunknone.Location = new System.Drawing.Point(41, 408);
                gbhobby.Location = new System.Drawing.Point(30, 408);
                gbtravel.Location = new System.Drawing.Point(372, 408);
            }
            else
            {
                gbApart.Visible = false;
                gbunown.Location = new System.Drawing.Point(116, 333);
                llunknone.Location = new System.Drawing.Point(41, 333);
                gbhobby.Location = new System.Drawing.Point(30, 333);
                gbtravel.Location = new System.Drawing.Point(372, 333);
            }

            wrapper.Changed = true;
        }

        private void Commit(object sender, System.EventArgs e)
        {
            if (wrapper == null) return;
            try
            {
                wrapper.SynchronizeUserData();
                MessageBox.Show(Localization.Manager.GetString("commited"));
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
            }
        }

        private void CommonChange(object sender, System.EventArgs e)
        {
            if (wrapper == null) return;
            try
            {
                wrapper.LotRoads = Convert.ToByte(this.tbRoads.Text, 16);

                wrapper.LotSize = new Size(
                    Helper.StringToInt32(tbwd.Text, wrapper.LotSize.Width, 10),
                    Helper.StringToInt32(tbhg.Text, wrapper.LotSize.Height, 10));
                wrapper.LotPosition = new Point(
                    Helper.StringToInt32(tbleft.Text, wrapper.LotPosition.X, 10),
                    Helper.StringToInt32(tbtop.Text, wrapper.LotPosition.Y, 10));
                wrapper.LotElevation = Helper.StringToFloat(tbz.Text, wrapper.LotElevation);

                wrapper.Orientation = (LotOrientation)cborient.SelectedValue;
                wrapper.LotRotation = Convert.ToByte(this.tbrotation.Text, 16);
                wrapper.Unknown0 = Helper.StringToUInt32(tbu0.Text, wrapper.Unknown0, 16);
                Boolset bby = wrapper.Unknown0;
                this.cbhidim.Checked = bby[4];
                this.cbBeachy.Checked = bby[7];
                if (wrapper.Version >= LtxtVersion.Apartment || wrapper.SubVersion >= LtxtSubVersion.Apartment)
                {
                    this.cbLotClas.Enabled = true;
                    if (bby[12]) this.cbLotClas.SelectedIndex = 1;
                    else if (bby[13]) this.cbLotClas.SelectedIndex = 2;
                    else if (bby[14]) this.cbLotClas.SelectedIndex = 3;
                    else this.cbLotClas.SelectedIndex = 0;
                }
                else
                {
                    this.cbLotClas.SelectedIndex = 0;
                    this.cbLotClas.Enabled = false;
                }

                wrapper.LotName = tblotname.Text;
                wrapper.Texture = tbTexture.Text;
                wrapper.LotDesc = tbdesc.Text;

                wrapper.LotInstance = Helper.StringToUInt32(tbinst.Text, wrapper.LotInstance, 16);
                wrapper.Unknown3 = Helper.StringToFloat(tbu3.Text, wrapper.Unknown3);
                wrapper.Unknown4 = Helper.StringToUInt32(tbu4.Text, wrapper.Unknown4, 16);
                wrapper.LotClass = Helper.StringToUInt32(tblotclass.Text, wrapper.LotClass, 16);
                Boolset tty = wrapper.Unknown4;

                this.cbtrjflag5.Checked = tty[30];
                this.cbtrjflag4.Checked = tty[28];
                this.cbtrjflag3.Checked = tty[27];
                this.cbtrjflag2.Checked = tty[26];
                this.cbtrjflag1.Checked = tty[25];
                this.cbtrjungle.Checked = tty[24];
                this.cbtrhidec.Checked = tty[23];
                this.cbtrpool.Checked = tty[22];
                this.cbtrmale.Checked = tty[21];
                this.cbtrfem.Checked = tty[20];
                this.cbtrbeach.Checked = tty[19];
                this.cbtrformal.Checked = tty[18];
                this.cbtrteen.Checked = tty[17];
                this.cbtrnude.Checked = tty[16];
                this.cbtrpern.Checked = tty[15];
                this.cgtrwhite.Checked = tty[14];
                this.cbtrblue.Checked = tty[13];
                this.cbtrredred.Checked = tty[12];
                this.cbtradult.Checked = tty[11];
                this.cbtrclub.Checked = tty[10];
                this.cbhbmusic.Checked = tty[9];
                this.cbhbscience.Checked = tty[8];
                this.cbhbfitness.Checked = tty[7];
                this.cbhbtinker.Checked = tty[6];
                this.cbhbnature.Checked = tty[5];
                this.cbhbgames.Checked = tty[4];
                this.cbhbsport.Checked = tty[3];
                this.cbhbfilm.Checked = tty[2];
                this.cbhbart.Checked = tty[1];
                this.cbhbcook.Checked = tty[0];

                wrapper.Unknown2 = (byte)Helper.StringToUInt16(tbu2.Text, wrapper.Unknown2, 16);
                wrapper.OwnerInstance = Helper.StringToUInt32(tbowner.Text, wrapper.OwnerInstance, 16);

                wrapper.Changed = true;
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage("", ex);
            }
        }

        private void cbhidim_CheckedChanged(object sender, System.EventArgs e)
        {
            if (wrapper == null) return;
            try
            {
                Boolset bby = wrapper.Unknown0;
                bby[4] = this.cbhidim.Checked;
                bby[7] = this.cbBeachy.Checked;
                if (wrapper.Version >= LtxtVersion.Apartment || wrapper.SubVersion >= LtxtSubVersion.Apartment)
                {
                    bby[12] = (this.cbLotClas.SelectedIndex == 1);
                    bby[13] = (this.cbLotClas.SelectedIndex == 2);
                    bby[14] = (this.cbLotClas.SelectedIndex == 3);
                }
                wrapper.Unknown0 = bby;
                this.tbu0.Text = "0x" + Helper.HexString(wrapper.Unknown0);
                wrapper.Changed = true;
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage("", ex);
            }
        }

        private void hobbytravel_CheckedChanged(object sender, System.EventArgs e)
        {
            if (wrapper == null) return;
            try
            {
                uint tty = 0;
                if (this.cbhbcook.Checked) tty += 1;
                if (this.cbhbart.Checked) tty += 2;
                if (this.cbhbfilm.Checked) tty += 4;
                if (this.cbhbsport.Checked) tty += 8;
                if (this.cbhbgames.Checked) tty += 16;
                if (this.cbhbnature.Checked) tty += 32;
                if (this.cbhbtinker.Checked) tty += 64;
                if (this.cbhbfitness.Checked) tty += 128;
                if (this.cbhbscience.Checked) tty += 256;
                if (this.cbhbmusic.Checked) tty += 512;
                if (this.cbtrclub.Checked) tty += 1024;
                if (this.cbtradult.Checked) tty += 2048;
                if (this.cbtrredred.Checked) tty += 4096;
                if (this.cbtrblue.Checked) tty += 8192;
                if (this.cgtrwhite.Checked) tty += 16384;
                if (this.cbtrpern.Checked) tty += 32768;
                if (this.cbtrnude.Checked) tty += 65536;
                if (this.cbtrteen.Checked) tty += 131072;
                if (this.cbtrformal.Checked) tty += 262144;
                if (this.cbtrbeach.Checked) tty += 524288;
                if (this.cbtrfem.Checked) tty += 1048576;
                if (this.cbtrmale.Checked) tty += 2097152;
                if (this.cbtrpool.Checked) tty += 4194304;
                if (this.cbtrhidec.Checked) tty += 8388608;
                if (this.cbtrjungle.Checked) tty += 16777216;
                if (this.cbtrjflag1.Checked) tty += 33554432;
                if (this.cbtrjflag2.Checked) tty += 67108864;
                if (this.cbtrjflag3.Checked) tty += 134217728;
                if (this.cbtrjflag4.Checked) tty += 268435456;
                if (this.cbtrjflag5.Checked) tty += 536870912;
                this.cbtrmale.Enabled = !this.cbtrfem.Checked;
                this.cbtrfem.Enabled = !this.cbtrmale.Checked;
                wrapper.Unknown4 = tty;
                this.tbu4.Text = "0x" + Helper.HexString(wrapper.Unknown4);

                wrapper.Changed = true;
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage("", ex);
            }
        }

        private void Openpntravel(object sender, System.EventArgs e)
        {
            if (wrapper == null) return;
            try
            {
                this.gbunown.Visible = false;
                this.gbhobby.Visible = !this.gbhobby.Visible;
                this.gbtravel.Visible = this.gbhobby.Visible && (booby.PrettyGirls.IsTitsInstalled() || booby.PrettyGirls.IsAngelsInstalled());
                //this.bthbytrvl.Enabled = false;
                wrapper.Changed = true;
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage("", ex);
            }
        }

        private void ChangeData(object sender, System.EventArgs e)
        {
            if (wrapper == null) return;
            try
            {
                wrapper.Unknown6 = Helper.SetLength(Helper.HexListToBytes(this.tbu6.Text), 9);
                wrapper.Followup = Helper.SetLength(Helper.HexListToBytes(this.tbData.Text), 0);

                wrapper.Changed = true;
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage("", ex);
            }
        }

        private void lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (wrapper == null) return;
            Ltxt wrp = wrapper;
            wrapper = null;

            if (lb.SelectedIndex < 0)
                tbElevationAt.Text = "";
            else
                tbElevationAt.Text = wrp.Unknown1[lb.SelectedIndex].ToString();

            wrapper = wrp;
        }

        private void tbElevationAt_TextChanged(object sender, EventArgs e)
        {
            if (wrapper == null) return;
            if (lb.SelectedIndex < 0) return;

            Ltxt wrp = wrapper;
            wrapper = null;

            try
            {
                wrp.Unknown1[lb.SelectedIndex] = Helper.StringToFloat(tbElevationAt.Text, wrp.Unknown1[lb.SelectedIndex]);
                int x, y;
                y = Convert.ToInt32(lb.SelectedIndex / wrp.LotSize.Height);
                x = lb.SelectedIndex - y * wrp.LotSize.Height;
                lb.Items[lb.SelectedIndex] = "(" + x + "," + y + ") " + wrp.Unknown1[lb.SelectedIndex];

                wrp.Changed = true;
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage("", ex);
            }
            finally
            {
                wrapper = wrp;
            }
        }

        private void lbApts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (wrapper == null) return;
            Ltxt wrp = wrapper;
            wrapper = null;

            if (lbApts.SelectedIndex < 0)
            {
                tbApartment.Text = tbSAFamily.Text = tbSAu2.Text = tbSAu3.Text = "";
                btnDelApt.Enabled = llFamily.Enabled = llSubLot.Enabled = false;
            }
            else
            {
                Ltxt.SubLot sl = wrp.SubLots[lbApts.SelectedIndex];
                tbApartment.Text = (string)lbApts.SelectedItem;
                tbSAFamily.Text = "0x" + Helper.HexString(sl.Family);
                tbSAu2.Text = "0x" + Helper.HexString(sl.Unknown2);
                tbSAu3.Text = "0x" + Helper.HexString(sl.Unknown3);
                btnDelApt.Enabled = llFamily.Enabled = llSubLot.Enabled = true;
            }

            wrapper = wrp;
        }

        private void SAChange(object sender, EventArgs e)
        {
            if (wrapper == null) return;
            if (lbApts.SelectedIndex < 0) return;

            Ltxt wrp = wrapper;
            wrapper = null;

            try
            {
                Ltxt.SubLot sl = wrp.SubLots[lbApts.SelectedIndex];
                sl.ApartmentSublot = Helper.StringToUInt32(tbApartment.Text, sl.ApartmentSublot, 16);
                sl.Family = Helper.StringToUInt32(tbSAFamily.Text, sl.Family, 16);
                sl.Unknown2 = Helper.StringToUInt32(tbSAu2.Text, sl.Unknown2, 16);
                sl.Unknown3 = Helper.StringToUInt32(tbSAu3.Text, sl.Unknown3, 16);
                lbApts.Items[lbApts.SelectedIndex] = "0x" + Helper.HexString(sl.ApartmentSublot);

                wrp.Changed = true;
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage("", ex);
            }
            finally
            {
                wrapper = wrp;
            }
        }

        private void ll_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Collections.Generic.List<LinkLabel> lll =
                new System.Collections.Generic.List<LinkLabel>(new LinkLabel[] { llAptBase, llSubLot, llFamily, });

            uint type, inst;
            switch (lll.IndexOf((LinkLabel)sender))
            {
                case 0:
                    type = (uint)0x0BF999E7;
                    inst = wrapper.ApartmentBase;
                    break;
                case 1:
                    type = (uint)0x0BF999E7;
                    inst = wrapper.SubLots[lbApts.SelectedIndex].ApartmentSublot;
                    break;
                case 2:
                    type = (uint)0x46414D49;
                    inst = wrapper.SubLots[lbApts.SelectedIndex].Family;
                    break;
                default:
                    return;
            }

            Interfaces.Files.IPackedFileDescriptor pfd =
                wrapper.Package.NewDescriptor(type, wrapper.FileDescriptor.SubType, wrapper.FileDescriptor.Group, inst);
            pfd = wrapper.Package.FindFile(pfd);
            if (pfd == null) return;

            SimPe.RemoteControl.OpenPackedFile(pfd, wrapper.Package);
        }

        private void btnAddApt_Click(object sender, EventArgs e)
        {
            wrapper.SubLots.Add(new Ltxt.SubLot());
            lbApts.Items.Add("0x" + Helper.HexString(wrapper.SubLots[wrapper.SubLots.Count - 1].ApartmentSublot));
            lbApts.SelectedIndex = wrapper.SubLots.Count - 1;

            wrapper.Changed = true;
        }

        private void btnDelApt_Click(object sender, EventArgs e)
        {
            int i = lbApts.SelectedIndex;

            lbApts.BeginUpdate();
            lbApts.SelectedIndex = -1;

            wrapper.SubLots.RemoveAt(i);
            lbApts.Items.RemoveAt(i);

            if (i > 0) i--;
            else if (lbApts.Items.Count == 0) i = -1;

            lbApts.SelectedIndex = i;
            lbApts.EndUpdate();

            wrapper.Changed = true;
        }

        private void tbApBase_TextChanged(object sender, EventArgs e)
        {
            if (wrapper == null) return;
            wrapper.ApartmentBase = Helper.StringToUInt32(tbApBase.Text, wrapper.ApartmentBase, 16);
            llAptBase.Enabled = (wrapper.ApartmentBase != 0);
        }

        private void label25_Click(object sender, EventArgs e)
        {
            uint simmy = Helper.StringToUInt32(tbowner.Text, wrapper.OwnerInstance, 16);
            if (simmy == 0) return;
            SimPe.PackedFiles.Wrapper.ExtSDesc sdsc = FileTable.ProviderRegistry.SimDescriptionProvider.SimInstance[(ushort)simmy] as SimPe.PackedFiles.Wrapper.ExtSDesc;
            if (sdsc != null)
            {
                Interfaces.Files.IPackedFileDescriptor pfd = sdsc.Package.NewDescriptor(0xAACE2EFB, sdsc.FileDescriptor.SubType, sdsc.FileDescriptor.Group, sdsc.FileDescriptor.Instance);
                pfd = sdsc.Package.FindFile(pfd);
                SimPe.RemoteControl.OpenPackedFile(pfd, sdsc.Package);
            }
        }

        private void llunknone_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.gbunown.Visible = !this.gbunown.Visible;
        }

        private void llboobs_LinkClicked(object sender, EventArgs e)
        {
            if (wrapper == null) return;
            if (!System.IO.File.Exists(SimPe.PathProvider.Global.SimsApplication) || wrapper.appendage == null) return;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = SimPe.PathProvider.Global.SimsApplication;
            p.StartInfo.Arguments = wrapper.appendage;
            p.Start();
            System.Windows.Forms.Application.ExitThread();
        }

        private void lbOpenim_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                SimPe.RemoteControl.OpenPackage(wrapper.lotfile);
            }
            catch { }
        }
    }
}
