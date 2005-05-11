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
using System.Globalization;
using SimPe.Plugin.Gmdc;

namespace SimPe.Plugin
{
	internal struct modelInfo
	{
		public int vertexDataList;
		public int normalDataList;
		public int unknownDataList1;
		public int unknownDataList2;
		public int tuDataList;
	}
 
	internal struct quart
	{
		public float a1;
		public float a2;
		public float a3;
		public float a4;
	}

	internal struct uv
	{
		public float u;
		public float v;
	}

	internal struct vertex
	{
		public float x;
		public float y;
		public float z;
	}

	/// <summary>
	/// Zusammenfassung für fGeometryDataContainer.
	/// </summary>
	public class fGeometryDataContainer : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.GroupBox groupBox10;
		internal System.Windows.Forms.TextBox tb_ver;
		private System.Windows.Forms.Label label28;
		internal System.Windows.Forms.TabPage tGeometryDataContainer;
		private System.Windows.Forms.GroupBox groupBox3;
		internal System.Windows.Forms.TextBox tb_data;
		private System.Windows.Forms.Label label12;
		internal System.Windows.Forms.TextBox tb_uk5;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label5;
		internal System.Windows.Forms.TextBox tb_uk1;
		private System.Windows.Forms.Label label6;
		internal System.Windows.Forms.ListBox lb_itemsa;
		internal System.Windows.Forms.TextBox tb_mod2;
		internal System.Windows.Forms.TextBox tb_mod1;
		internal System.Windows.Forms.TextBox tb_id;
		private System.Windows.Forms.GroupBox groupBox1;
		internal System.Windows.Forms.ListBox lb_itemsa2;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.TextBox tb_itemsa2;
		internal System.Windows.Forms.TabPage tGeometryDataContainer2;
		internal System.Windows.Forms.TabPage tGeometryDataContainer3;
		private System.Windows.Forms.GroupBox groupBox2;
		internal System.Windows.Forms.ListBox lb_itemsc;
		internal System.Windows.Forms.TextBox tb_itemsc_name;
		private System.Windows.Forms.Label label11;
		internal System.Windows.Forms.TextBox tb_opacity;
		private System.Windows.Forms.Label label13;
		internal System.Windows.Forms.TextBox tb_uk3;
		private System.Windows.Forms.Label label2;
		internal System.Windows.Forms.TextBox tb_uk2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label4;
		internal System.Windows.Forms.ListBox lb_itemsc2;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label9;
		internal System.Windows.Forms.ListBox lb_itemsc3;
		internal System.Windows.Forms.TextBox tb_itemsc2;
		internal System.Windows.Forms.TextBox tb_itemsc3;
		private System.Windows.Forms.GroupBox groupBox6;
		internal System.Windows.Forms.TextBox tb_itemsb2;
		private System.Windows.Forms.Label label14;
		internal System.Windows.Forms.ListBox lb_itemsb2;
		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.Label label16;
		internal System.Windows.Forms.ListBox lb_itemsb;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.GroupBox groupBox8;
		internal System.Windows.Forms.TextBox tb_itemsb3;
		private System.Windows.Forms.Label label19;
		internal System.Windows.Forms.ListBox lb_itemsb3;
		private System.Windows.Forms.GroupBox groupBox9;
		internal System.Windows.Forms.TextBox tb_itemsb4;
		private System.Windows.Forms.Label label15;
		internal System.Windows.Forms.ListBox lb_itemsb4;
		private System.Windows.Forms.GroupBox groupBox11;
		internal System.Windows.Forms.TextBox tb_itemsb5;
		private System.Windows.Forms.Label label17;
		internal System.Windows.Forms.ListBox lb_itemsb5;
		internal System.Windows.Forms.TextBox tb_uk4;
		internal System.Windows.Forms.TextBox tb_uk6;
		internal System.Windows.Forms.TabPage tMain;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.SaveFileDialog sfd;
		private System.Windows.Forms.Panel pnprev;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label21;
		internal System.Windows.Forms.CheckedListBox lbmodel;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.ColorDialog cd;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public fGeometryDataContainer()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();


			button2.Visible = Helper.WindowsRegistry.HiddenMode;

			
		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
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

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tMain = new System.Windows.Forms.TabPage();
			this.button4 = new System.Windows.Forms.Button();
			this.lbmodel = new System.Windows.Forms.CheckedListBox();
			this.label21 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.pnprev = new System.Windows.Forms.Panel();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.label20 = new System.Windows.Forms.Label();
			this.tGeometryDataContainer = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tb_itemsa2 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lb_itemsa2 = new System.Windows.Forms.ListBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.lb_itemsa = new System.Windows.Forms.ListBox();
			this.tb_data = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.tb_uk5 = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.tb_mod2 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.tb_mod1 = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.tb_id = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tb_uk1 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox10 = new System.Windows.Forms.GroupBox();
			this.tb_ver = new System.Windows.Forms.TextBox();
			this.label28 = new System.Windows.Forms.Label();
			this.tGeometryDataContainer2 = new System.Windows.Forms.TabPage();
			this.groupBox9 = new System.Windows.Forms.GroupBox();
			this.tb_itemsb4 = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.lb_itemsb4 = new System.Windows.Forms.ListBox();
			this.groupBox11 = new System.Windows.Forms.GroupBox();
			this.tb_itemsb5 = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.lb_itemsb5 = new System.Windows.Forms.ListBox();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.tb_itemsb2 = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.lb_itemsb2 = new System.Windows.Forms.ListBox();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.tb_uk4 = new System.Windows.Forms.TextBox();
			this.tb_uk6 = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.lb_itemsb = new System.Windows.Forms.ListBox();
			this.label18 = new System.Windows.Forms.Label();
			this.groupBox8 = new System.Windows.Forms.GroupBox();
			this.tb_itemsb3 = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.lb_itemsb3 = new System.Windows.Forms.ListBox();
			this.tGeometryDataContainer3 = new System.Windows.Forms.TabPage();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.tb_itemsc2 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.lb_itemsc2 = new System.Windows.Forms.ListBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tb_opacity = new System.Windows.Forms.TextBox();
			this.tb_uk2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tb_uk3 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.lb_itemsc = new System.Windows.Forms.ListBox();
			this.tb_itemsc_name = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.tb_itemsc3 = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.lb_itemsc3 = new System.Windows.Forms.ListBox();
			this.sfd = new System.Windows.Forms.SaveFileDialog();
			this.cd = new System.Windows.Forms.ColorDialog();
			this.tabControl1.SuspendLayout();
			this.tMain.SuspendLayout();
			this.tGeometryDataContainer.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox10.SuspendLayout();
			this.tGeometryDataContainer2.SuspendLayout();
			this.groupBox9.SuspendLayout();
			this.groupBox11.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.groupBox8.SuspendLayout();
			this.tGeometryDataContainer3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tMain);
			this.tabControl1.Controls.Add(this.tGeometryDataContainer);
			this.tabControl1.Controls.Add(this.tGeometryDataContainer2);
			this.tabControl1.Controls.Add(this.tGeometryDataContainer3);
			this.tabControl1.Location = new System.Drawing.Point(36, -1);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(800, 328);
			this.tabControl1.TabIndex = 1;
			// 
			// tMain
			// 
			this.tMain.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tMain.Controls.Add(this.button4);
			this.tMain.Controls.Add(this.lbmodel);
			this.tMain.Controls.Add(this.label21);
			this.tMain.Controls.Add(this.button3);
			this.tMain.Controls.Add(this.pnprev);
			this.tMain.Controls.Add(this.button2);
			this.tMain.Controls.Add(this.button1);
			this.tMain.Controls.Add(this.label20);
			this.tMain.Location = new System.Drawing.Point(4, 22);
			this.tMain.Name = "tMain";
			this.tMain.Size = new System.Drawing.Size(792, 302);
			this.tMain.TabIndex = 3;
			this.tMain.Text = "3D Mesh";
			// 
			// button4
			// 
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button4.Location = new System.Drawing.Point(176, 272);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(32, 23);
			this.button4.TabIndex = 26;
			this.button4.Text = "BG";
			this.button4.Click += new System.EventHandler(this.PickColor);
			// 
			// lbmodel
			// 
			this.lbmodel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.lbmodel.CheckOnClick = true;
			this.lbmodel.HorizontalScrollbar = true;
			this.lbmodel.Location = new System.Drawing.Point(16, 32);
			this.lbmodel.Name = "lbmodel";
			this.lbmodel.Size = new System.Drawing.Size(192, 199);
			this.lbmodel.TabIndex = 24;
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label21.Location = new System.Drawing.Point(8, 16);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(45, 16);
			this.label21.TabIndex = 23;
			this.label21.Text = "Models:";
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button3.Location = new System.Drawing.Point(72, 272);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(96, 23);
			this.button3.TabIndex = 4;
			this.button3.Text = "Preview";
			this.button3.Click += new System.EventHandler(this.Preview);
			// 
			// pnprev
			// 
			this.pnprev.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pnprev.Location = new System.Drawing.Point(216, 8);
			this.pnprev.Name = "pnprev";
			this.pnprev.Size = new System.Drawing.Size(288, 288);
			this.pnprev.TabIndex = 3;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button2.Location = new System.Drawing.Point(16, 272);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(48, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "to .x";
			this.button2.Click += new System.EventHandler(this.ExportX);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(16, 240);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(192, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Export to .obj (by Delphy)";
			this.button1.Click += new System.EventHandler(this.ExportObj);
			// 
			// label20
			// 
			this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label20.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.label20.Location = new System.Drawing.Point(512, 8);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(272, 288);
			this.label20.TabIndex = 25;
			this.label20.Text = @"Camera Control:

 Translate Y: left Button + move vertical
 Translate X: left Button + move horizontal
 Translate Z: middle Button + move vertical
 Scale: middle Button + move horizontal
 Rotate X: right Button + move vertical
 Rotate Y: right Button + move horizontal";
			this.label20.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// tGeometryDataContainer
			// 
			this.tGeometryDataContainer.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tGeometryDataContainer.Controls.Add(this.groupBox1);
			this.tGeometryDataContainer.Controls.Add(this.groupBox3);
			this.tGeometryDataContainer.Controls.Add(this.groupBox10);
			this.tGeometryDataContainer.Location = new System.Drawing.Point(4, 22);
			this.tGeometryDataContainer.Name = "tGeometryDataContainer";
			this.tGeometryDataContainer.Size = new System.Drawing.Size(792, 302);
			this.tGeometryDataContainer.TabIndex = 0;
			this.tGeometryDataContainer.Text = "Items 1";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.tb_itemsa2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.lb_itemsa2);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(8, 88);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(264, 208);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Unknown Items 1b";
			// 
			// tb_itemsa2
			// 
			this.tb_itemsa2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tb_itemsa2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_itemsa2.Location = new System.Drawing.Point(56, 176);
			this.tb_itemsa2.Name = "tb_itemsa2";
			this.tb_itemsa2.ReadOnly = true;
			this.tb_itemsa2.Size = new System.Drawing.Size(88, 21);
			this.tb_itemsa2.TabIndex = 24;
			this.tb_itemsa2.Text = "0x00000000";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 184);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 17);
			this.label1.TabIndex = 23;
			this.label1.Text = "Value:";
			// 
			// lb_itemsa2
			// 
			this.lb_itemsa2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lb_itemsa2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lb_itemsa2.HorizontalScrollbar = true;
			this.lb_itemsa2.IntegralHeight = false;
			this.lb_itemsa2.Location = new System.Drawing.Point(8, 24);
			this.lb_itemsa2.Name = "lb_itemsa2";
			this.lb_itemsa2.Size = new System.Drawing.Size(248, 144);
			this.lb_itemsa2.TabIndex = 22;
			this.lb_itemsa2.SelectedIndexChanged += new System.EventHandler(this.SelectItemsA2);
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.lb_itemsa);
			this.groupBox3.Controls.Add(this.tb_data);
			this.groupBox3.Controls.Add(this.label12);
			this.groupBox3.Controls.Add(this.tb_uk5);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Controls.Add(this.tb_mod2);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.tb_mod1);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this.tb_id);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.tb_uk1);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox3.Location = new System.Drawing.Point(280, 8);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(504, 288);
			this.groupBox3.TabIndex = 13;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Unknown Items 1";
			// 
			// lb_itemsa
			// 
			this.lb_itemsa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lb_itemsa.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lb_itemsa.HorizontalScrollbar = true;
			this.lb_itemsa.IntegralHeight = false;
			this.lb_itemsa.Location = new System.Drawing.Point(8, 24);
			this.lb_itemsa.Name = "lb_itemsa";
			this.lb_itemsa.Size = new System.Drawing.Size(152, 256);
			this.lb_itemsa.TabIndex = 21;
			this.lb_itemsa.SelectedIndexChanged += new System.EventHandler(this.SelectItemsA);
			// 
			// tb_data
			// 
			this.tb_data.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tb_data.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_data.Location = new System.Drawing.Point(176, 160);
			this.tb_data.Multiline = true;
			this.tb_data.Name = "tb_data";
			this.tb_data.ReadOnly = true;
			this.tb_data.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tb_data.Size = new System.Drawing.Size(312, 120);
			this.tb_data.TabIndex = 20;
			this.tb_data.Text = "";
			// 
			// label12
			// 
			this.label12.AccessibleDescription = "d";
			this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label12.Location = new System.Drawing.Point(168, 144);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(36, 17);
			this.label12.TabIndex = 19;
			this.label12.Text = "Data:";
			// 
			// tb_uk5
			// 
			this.tb_uk5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tb_uk5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_uk5.Location = new System.Drawing.Point(400, 40);
			this.tb_uk5.Name = "tb_uk5";
			this.tb_uk5.ReadOnly = true;
			this.tb_uk5.Size = new System.Drawing.Size(88, 21);
			this.tb_uk5.TabIndex = 14;
			this.tb_uk5.Text = "0x00000000";
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label10.Location = new System.Drawing.Point(392, 24);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(73, 17);
			this.label10.TabIndex = 13;
			this.label10.Text = "Unknown 5:";
			// 
			// tb_mod2
			// 
			this.tb_mod2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tb_mod2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_mod2.Location = new System.Drawing.Point(288, 80);
			this.tb_mod2.Name = "tb_mod2";
			this.tb_mod2.ReadOnly = true;
			this.tb_mod2.Size = new System.Drawing.Size(88, 21);
			this.tb_mod2.TabIndex = 12;
			this.tb_mod2.Text = "0x00";
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(280, 64);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(40, 17);
			this.label7.TabIndex = 11;
			this.label7.Text = "Mod2:";
			// 
			// tb_mod1
			// 
			this.tb_mod1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tb_mod1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_mod1.Location = new System.Drawing.Point(288, 40);
			this.tb_mod1.Name = "tb_mod1";
			this.tb_mod1.ReadOnly = true;
			this.tb_mod1.Size = new System.Drawing.Size(88, 21);
			this.tb_mod1.TabIndex = 10;
			this.tb_mod1.Text = "0x00000000";
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label8.Location = new System.Drawing.Point(280, 24);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(40, 17);
			this.label8.TabIndex = 9;
			this.label8.Text = "Mod1:";
			// 
			// tb_id
			// 
			this.tb_id.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tb_id.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_id.Location = new System.Drawing.Point(176, 80);
			this.tb_id.Name = "tb_id";
			this.tb_id.ReadOnly = true;
			this.tb_id.Size = new System.Drawing.Size(88, 21);
			this.tb_id.TabIndex = 8;
			this.tb_id.Text = "0x00000000";
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(168, 64);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(54, 17);
			this.label5.TabIndex = 7;
			this.label5.Text = "Identity:";
			// 
			// tb_uk1
			// 
			this.tb_uk1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tb_uk1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_uk1.Location = new System.Drawing.Point(176, 40);
			this.tb_uk1.Name = "tb_uk1";
			this.tb_uk1.ReadOnly = true;
			this.tb_uk1.Size = new System.Drawing.Size(88, 21);
			this.tb_uk1.TabIndex = 6;
			this.tb_uk1.Text = "0x0000";
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(168, 24);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(55, 17);
			this.label6.TabIndex = 5;
			this.label6.Text = "Number:";
			// 
			// groupBox10
			// 
			this.groupBox10.Controls.Add(this.tb_ver);
			this.groupBox10.Controls.Add(this.label28);
			this.groupBox10.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox10.Location = new System.Drawing.Point(8, 8);
			this.groupBox10.Name = "groupBox10";
			this.groupBox10.Size = new System.Drawing.Size(264, 72);
			this.groupBox10.TabIndex = 12;
			this.groupBox10.TabStop = false;
			this.groupBox10.Text = "Settings";
			// 
			// tb_ver
			// 
			this.tb_ver.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_ver.Location = new System.Drawing.Point(16, 40);
			this.tb_ver.Name = "tb_ver";
			this.tb_ver.Size = new System.Drawing.Size(88, 21);
			this.tb_ver.TabIndex = 24;
			this.tb_ver.Text = "0x00000000";
			this.tb_ver.TextChanged += new System.EventHandler(this.SettingsChange);
			// 
			// label28
			// 
			this.label28.AutoSize = true;
			this.label28.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label28.Location = new System.Drawing.Point(8, 24);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(52, 17);
			this.label28.TabIndex = 23;
			this.label28.Text = "Version:";
			// 
			// tGeometryDataContainer2
			// 
			this.tGeometryDataContainer2.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tGeometryDataContainer2.Controls.Add(this.groupBox9);
			this.tGeometryDataContainer2.Controls.Add(this.groupBox11);
			this.tGeometryDataContainer2.Controls.Add(this.groupBox6);
			this.tGeometryDataContainer2.Controls.Add(this.groupBox7);
			this.tGeometryDataContainer2.Controls.Add(this.groupBox8);
			this.tGeometryDataContainer2.Location = new System.Drawing.Point(4, 22);
			this.tGeometryDataContainer2.Name = "tGeometryDataContainer2";
			this.tGeometryDataContainer2.Size = new System.Drawing.Size(792, 302);
			this.tGeometryDataContainer2.TabIndex = 1;
			this.tGeometryDataContainer2.Text = "Items 2";
			// 
			// groupBox9
			// 
			this.groupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox9.Controls.Add(this.tb_itemsb4);
			this.groupBox9.Controls.Add(this.label15);
			this.groupBox9.Controls.Add(this.lb_itemsb4);
			this.groupBox9.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox9.Location = new System.Drawing.Point(552, 8);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new System.Drawing.Size(232, 136);
			this.groupBox9.TabIndex = 29;
			this.groupBox9.TabStop = false;
			this.groupBox9.Text = "Unknown Items 2d";
			// 
			// tb_itemsb4
			// 
			this.tb_itemsb4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tb_itemsb4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_itemsb4.Location = new System.Drawing.Point(56, 104);
			this.tb_itemsb4.Name = "tb_itemsb4";
			this.tb_itemsb4.ReadOnly = true;
			this.tb_itemsb4.Size = new System.Drawing.Size(88, 21);
			this.tb_itemsb4.TabIndex = 24;
			this.tb_itemsb4.Text = "0x00000000";
			// 
			// label15
			// 
			this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label15.Location = new System.Drawing.Point(8, 112);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(41, 17);
			this.label15.TabIndex = 23;
			this.label15.Text = "Value:";
			// 
			// lb_itemsb4
			// 
			this.lb_itemsb4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lb_itemsb4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lb_itemsb4.HorizontalScrollbar = true;
			this.lb_itemsb4.IntegralHeight = false;
			this.lb_itemsb4.Location = new System.Drawing.Point(8, 24);
			this.lb_itemsb4.Name = "lb_itemsb4";
			this.lb_itemsb4.Size = new System.Drawing.Size(216, 72);
			this.lb_itemsb4.TabIndex = 22;
			this.lb_itemsb4.SelectedIndexChanged += new System.EventHandler(this.SelectItemsB4);
			// 
			// groupBox11
			// 
			this.groupBox11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox11.Controls.Add(this.tb_itemsb5);
			this.groupBox11.Controls.Add(this.label17);
			this.groupBox11.Controls.Add(this.lb_itemsb5);
			this.groupBox11.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox11.Location = new System.Drawing.Point(552, 152);
			this.groupBox11.Name = "groupBox11";
			this.groupBox11.Size = new System.Drawing.Size(232, 144);
			this.groupBox11.TabIndex = 30;
			this.groupBox11.TabStop = false;
			this.groupBox11.Text = "Unknown Items 2e";
			// 
			// tb_itemsb5
			// 
			this.tb_itemsb5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tb_itemsb5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_itemsb5.Location = new System.Drawing.Point(56, 112);
			this.tb_itemsb5.Name = "tb_itemsb5";
			this.tb_itemsb5.ReadOnly = true;
			this.tb_itemsb5.Size = new System.Drawing.Size(88, 21);
			this.tb_itemsb5.TabIndex = 24;
			this.tb_itemsb5.Text = "0x00000000";
			// 
			// label17
			// 
			this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label17.AutoSize = true;
			this.label17.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label17.Location = new System.Drawing.Point(8, 120);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(41, 17);
			this.label17.TabIndex = 23;
			this.label17.Text = "Value:";
			// 
			// lb_itemsb5
			// 
			this.lb_itemsb5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lb_itemsb5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lb_itemsb5.HorizontalScrollbar = true;
			this.lb_itemsb5.IntegralHeight = false;
			this.lb_itemsb5.Location = new System.Drawing.Point(8, 24);
			this.lb_itemsb5.Name = "lb_itemsb5";
			this.lb_itemsb5.Size = new System.Drawing.Size(216, 80);
			this.lb_itemsb5.TabIndex = 22;
			this.lb_itemsb5.SelectedIndexChanged += new System.EventHandler(this.SelectItemsB5);
			// 
			// groupBox6
			// 
			this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox6.Controls.Add(this.tb_itemsb2);
			this.groupBox6.Controls.Add(this.label14);
			this.groupBox6.Controls.Add(this.lb_itemsb2);
			this.groupBox6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox6.Location = new System.Drawing.Point(312, 8);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(232, 136);
			this.groupBox6.TabIndex = 27;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Unknown Items 2b";
			// 
			// tb_itemsb2
			// 
			this.tb_itemsb2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tb_itemsb2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_itemsb2.Location = new System.Drawing.Point(56, 104);
			this.tb_itemsb2.Name = "tb_itemsb2";
			this.tb_itemsb2.ReadOnly = true;
			this.tb_itemsb2.Size = new System.Drawing.Size(88, 21);
			this.tb_itemsb2.TabIndex = 24;
			this.tb_itemsb2.Text = "0x00000000";
			// 
			// label14
			// 
			this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label14.Location = new System.Drawing.Point(8, 112);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(41, 17);
			this.label14.TabIndex = 23;
			this.label14.Text = "Value:";
			// 
			// lb_itemsb2
			// 
			this.lb_itemsb2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lb_itemsb2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lb_itemsb2.HorizontalScrollbar = true;
			this.lb_itemsb2.IntegralHeight = false;
			this.lb_itemsb2.Location = new System.Drawing.Point(8, 24);
			this.lb_itemsb2.Name = "lb_itemsb2";
			this.lb_itemsb2.Size = new System.Drawing.Size(216, 72);
			this.lb_itemsb2.TabIndex = 22;
			this.lb_itemsb2.SelectedIndexChanged += new System.EventHandler(this.SelectItemsB2);
			// 
			// groupBox7
			// 
			this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox7.Controls.Add(this.tb_uk4);
			this.groupBox7.Controls.Add(this.tb_uk6);
			this.groupBox7.Controls.Add(this.label16);
			this.groupBox7.Controls.Add(this.lb_itemsb);
			this.groupBox7.Controls.Add(this.label18);
			this.groupBox7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox7.Location = new System.Drawing.Point(8, 7);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(296, 288);
			this.groupBox7.TabIndex = 26;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Unknown Items 2";
			// 
			// tb_uk4
			// 
			this.tb_uk4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tb_uk4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_uk4.Location = new System.Drawing.Point(200, 40);
			this.tb_uk4.Name = "tb_uk4";
			this.tb_uk4.ReadOnly = true;
			this.tb_uk4.Size = new System.Drawing.Size(88, 21);
			this.tb_uk4.TabIndex = 25;
			this.tb_uk4.Text = "0x00000000";
			// 
			// tb_uk6
			// 
			this.tb_uk6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tb_uk6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_uk6.Location = new System.Drawing.Point(200, 80);
			this.tb_uk6.Name = "tb_uk6";
			this.tb_uk6.ReadOnly = true;
			this.tb_uk6.Size = new System.Drawing.Size(88, 21);
			this.tb_uk6.TabIndex = 23;
			this.tb_uk6.Text = "0x00000000";
			// 
			// label16
			// 
			this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label16.Location = new System.Drawing.Point(192, 64);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(73, 17);
			this.label16.TabIndex = 22;
			this.label16.Text = "Unknown 2:";
			// 
			// lb_itemsb
			// 
			this.lb_itemsb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lb_itemsb.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lb_itemsb.HorizontalScrollbar = true;
			this.lb_itemsb.IntegralHeight = false;
			this.lb_itemsb.Location = new System.Drawing.Point(8, 24);
			this.lb_itemsb.Name = "lb_itemsb";
			this.lb_itemsb.Size = new System.Drawing.Size(176, 256);
			this.lb_itemsb.TabIndex = 21;
			this.lb_itemsb.SelectedIndexChanged += new System.EventHandler(this.SelectItemsB);
			// 
			// label18
			// 
			this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label18.AutoSize = true;
			this.label18.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label18.Location = new System.Drawing.Point(192, 24);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(80, 17);
			this.label18.TabIndex = 5;
			this.label18.Text = "VertexCount:";
			// 
			// groupBox8
			// 
			this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox8.Controls.Add(this.tb_itemsb3);
			this.groupBox8.Controls.Add(this.label19);
			this.groupBox8.Controls.Add(this.lb_itemsb3);
			this.groupBox8.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox8.Location = new System.Drawing.Point(312, 152);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(232, 144);
			this.groupBox8.TabIndex = 28;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "Unknown Items 2c";
			// 
			// tb_itemsb3
			// 
			this.tb_itemsb3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tb_itemsb3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_itemsb3.Location = new System.Drawing.Point(56, 112);
			this.tb_itemsb3.Name = "tb_itemsb3";
			this.tb_itemsb3.ReadOnly = true;
			this.tb_itemsb3.Size = new System.Drawing.Size(88, 21);
			this.tb_itemsb3.TabIndex = 24;
			this.tb_itemsb3.Text = "0x00000000";
			// 
			// label19
			// 
			this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label19.AutoSize = true;
			this.label19.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label19.Location = new System.Drawing.Point(8, 120);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(41, 17);
			this.label19.TabIndex = 23;
			this.label19.Text = "Value:";
			// 
			// lb_itemsb3
			// 
			this.lb_itemsb3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lb_itemsb3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lb_itemsb3.HorizontalScrollbar = true;
			this.lb_itemsb3.IntegralHeight = false;
			this.lb_itemsb3.Location = new System.Drawing.Point(8, 24);
			this.lb_itemsb3.Name = "lb_itemsb3";
			this.lb_itemsb3.Size = new System.Drawing.Size(216, 80);
			this.lb_itemsb3.TabIndex = 22;
			this.lb_itemsb3.SelectedIndexChanged += new System.EventHandler(this.SelectItemsB3);
			// 
			// tGeometryDataContainer3
			// 
			this.tGeometryDataContainer3.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tGeometryDataContainer3.Controls.Add(this.groupBox4);
			this.tGeometryDataContainer3.Controls.Add(this.groupBox2);
			this.tGeometryDataContainer3.Controls.Add(this.groupBox5);
			this.tGeometryDataContainer3.Location = new System.Drawing.Point(4, 22);
			this.tGeometryDataContainer3.Name = "tGeometryDataContainer3";
			this.tGeometryDataContainer3.Size = new System.Drawing.Size(792, 302);
			this.tGeometryDataContainer3.TabIndex = 2;
			this.tGeometryDataContainer3.Text = "Items 3";
			// 
			// groupBox4
			// 
			this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox4.Controls.Add(this.tb_itemsc2);
			this.groupBox4.Controls.Add(this.label4);
			this.groupBox4.Controls.Add(this.lb_itemsc2);
			this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox4.Location = new System.Drawing.Point(520, 8);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(264, 136);
			this.groupBox4.TabIndex = 15;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Unknown Items 3b";
			// 
			// tb_itemsc2
			// 
			this.tb_itemsc2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tb_itemsc2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_itemsc2.Location = new System.Drawing.Point(56, 104);
			this.tb_itemsc2.Name = "tb_itemsc2";
			this.tb_itemsc2.ReadOnly = true;
			this.tb_itemsc2.Size = new System.Drawing.Size(88, 21);
			this.tb_itemsc2.TabIndex = 24;
			this.tb_itemsc2.Text = "0x00000000";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(8, 112);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 17);
			this.label4.TabIndex = 23;
			this.label4.Text = "Value:";
			// 
			// lb_itemsc2
			// 
			this.lb_itemsc2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lb_itemsc2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lb_itemsc2.HorizontalScrollbar = true;
			this.lb_itemsc2.IntegralHeight = false;
			this.lb_itemsc2.Location = new System.Drawing.Point(8, 24);
			this.lb_itemsc2.Name = "lb_itemsc2";
			this.lb_itemsc2.Size = new System.Drawing.Size(248, 72);
			this.lb_itemsc2.TabIndex = 22;
			this.lb_itemsc2.SelectedIndexChanged += new System.EventHandler(this.SelectItemsC2);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.tb_opacity);
			this.groupBox2.Controls.Add(this.tb_uk2);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.tb_uk3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.lb_itemsc);
			this.groupBox2.Controls.Add(this.tb_itemsc_name);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(8, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(504, 288);
			this.groupBox2.TabIndex = 14;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Unknown Items 3";
			// 
			// tb_opacity
			// 
			this.tb_opacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tb_opacity.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_opacity.Location = new System.Drawing.Point(400, 40);
			this.tb_opacity.Name = "tb_opacity";
			this.tb_opacity.Size = new System.Drawing.Size(88, 21);
			this.tb_opacity.TabIndex = 6;
			this.tb_opacity.Text = "0x00000000";
			this.tb_opacity.TextChanged += new System.EventHandler(this.ChangeItemsC);
			// 
			// tb_uk2
			// 
			this.tb_uk2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tb_uk2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_uk2.Location = new System.Drawing.Point(176, 40);
			this.tb_uk2.Name = "tb_uk2";
			this.tb_uk2.Size = new System.Drawing.Size(88, 21);
			this.tb_uk2.TabIndex = 25;
			this.tb_uk2.Text = "0x00000000";
			this.tb_uk2.TextChanged += new System.EventHandler(this.ChangeItemsC);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(392, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(52, 17);
			this.label3.TabIndex = 24;
			this.label3.Text = "Opacity:";
			// 
			// tb_uk3
			// 
			this.tb_uk3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tb_uk3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_uk3.Location = new System.Drawing.Point(288, 40);
			this.tb_uk3.Name = "tb_uk3";
			this.tb_uk3.Size = new System.Drawing.Size(88, 21);
			this.tb_uk3.TabIndex = 23;
			this.tb_uk3.Text = "0x00000000";
			this.tb_uk3.TextChanged += new System.EventHandler(this.ChangeItemsC);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(280, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 17);
			this.label2.TabIndex = 22;
			this.label2.Text = "Unknown 2:";
			// 
			// lb_itemsc
			// 
			this.lb_itemsc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lb_itemsc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lb_itemsc.HorizontalScrollbar = true;
			this.lb_itemsc.IntegralHeight = false;
			this.lb_itemsc.Location = new System.Drawing.Point(8, 24);
			this.lb_itemsc.Name = "lb_itemsc";
			this.lb_itemsc.Size = new System.Drawing.Size(152, 256);
			this.lb_itemsc.TabIndex = 21;
			this.lb_itemsc.SelectedIndexChanged += new System.EventHandler(this.SelectItemsC);
			// 
			// tb_itemsc_name
			// 
			this.tb_itemsc_name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tb_itemsc_name.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_itemsc_name.Location = new System.Drawing.Point(176, 80);
			this.tb_itemsc_name.Name = "tb_itemsc_name";
			this.tb_itemsc_name.Size = new System.Drawing.Size(312, 21);
			this.tb_itemsc_name.TabIndex = 8;
			this.tb_itemsc_name.Text = "";
			this.tb_itemsc_name.TextChanged += new System.EventHandler(this.ChangeItemsC);
			// 
			// label11
			// 
			this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label11.Location = new System.Drawing.Point(168, 64);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(42, 17);
			this.label11.TabIndex = 7;
			this.label11.Text = "Name:";
			// 
			// label13
			// 
			this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label13.Location = new System.Drawing.Point(168, 24);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(73, 17);
			this.label13.TabIndex = 5;
			this.label13.Text = "Unknown 1:";
			// 
			// groupBox5
			// 
			this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox5.Controls.Add(this.tb_itemsc3);
			this.groupBox5.Controls.Add(this.label9);
			this.groupBox5.Controls.Add(this.lb_itemsc3);
			this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox5.Location = new System.Drawing.Point(520, 152);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(264, 144);
			this.groupBox5.TabIndex = 25;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Unknown Items 3c";
			// 
			// tb_itemsc3
			// 
			this.tb_itemsc3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tb_itemsc3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tb_itemsc3.Location = new System.Drawing.Point(56, 112);
			this.tb_itemsc3.Name = "tb_itemsc3";
			this.tb_itemsc3.ReadOnly = true;
			this.tb_itemsc3.Size = new System.Drawing.Size(88, 21);
			this.tb_itemsc3.TabIndex = 24;
			this.tb_itemsc3.Text = "0x00000000";
			// 
			// label9
			// 
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label9.Location = new System.Drawing.Point(8, 120);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(41, 17);
			this.label9.TabIndex = 23;
			this.label9.Text = "Value:";
			// 
			// lb_itemsc3
			// 
			this.lb_itemsc3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lb_itemsc3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lb_itemsc3.HorizontalScrollbar = true;
			this.lb_itemsc3.IntegralHeight = false;
			this.lb_itemsc3.Location = new System.Drawing.Point(8, 24);
			this.lb_itemsc3.Name = "lb_itemsc3";
			this.lb_itemsc3.Size = new System.Drawing.Size(248, 80);
			this.lb_itemsc3.TabIndex = 22;
			this.lb_itemsc3.SelectedIndexChanged += new System.EventHandler(this.SelectItemsC3);
			// 
			// cd
			// 
			this.cd.Color = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(255)));
			// 
			// fGeometryDataContainer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(872, 326);
			this.Controls.Add(this.tabControl1);
			this.Name = "fGeometryDataContainer";
			this.Text = "fGeometryDataContainer";
			this.tabControl1.ResumeLayout(false);
			this.tMain.ResumeLayout(false);
			this.tGeometryDataContainer.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox10.ResumeLayout(false);
			this.tGeometryDataContainer2.ResumeLayout(false);
			this.groupBox9.ResumeLayout(false);
			this.groupBox11.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox7.ResumeLayout(false);
			this.groupBox8.ResumeLayout(false);
			this.tGeometryDataContainer3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void SettingsChange(object sender, System.EventArgs e)
		{
			if (tGeometryDataContainer.Tag==null) return;
			try 
			{
				GeometryDataContainer gdc = (GeometryDataContainer)tGeometryDataContainer.Tag;

				gdc.Version = Convert.ToUInt32(tb_ver.Text, 16);

				gdc.Changed = true;
			} 
			catch (Exception) 
			{
				//Helper.ExceptionMessage("", ex);
			}
		}

		private void SelectItemsA(object sender, System.EventArgs e)
		{
			if (lb_itemsa.Tag != null) return;
			if (lb_itemsa.SelectedIndex<0) return;
			try 
			{
				lb_itemsa.Tag = true;
				GeometryDataContainerItem1 item = (GeometryDataContainerItem1)lb_itemsa.Items[lb_itemsa.SelectedIndex];

				this.tb_id.Text = "0x"+Helper.HexString(item.Identity);
				this.tb_uk1.Text = "0x"+Helper.HexString(item.Number);
				this.tb_mod1.Text = "0x"+Helper.HexString((uint)item.BlockFormat);
				this.tb_mod2.Text = "0x"+Helper.HexString((uint)item.SetFormat);
				this.tb_uk5.Text = "0x"+Helper.HexString(item.Repeat);

				//this.tb_data.Text = Helper.BytesToHexList(item.Data);

				lb_itemsa2.Items.Clear();
				foreach (int i in item.Items) lb_itemsa2.Items.Add(i);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			} 
			finally 
			{
				lb_itemsa.Tag = null;
			}
		}

		private void SelectItemsA2(object sender, System.EventArgs e)
		{
			if (lb_itemsa.Tag != null) return;
			if (lb_itemsa.SelectedIndex<0) return;
			if (lb_itemsa2.SelectedIndex<0) return;
			try 
			{
				lb_itemsa.Tag = true;
				GeometryDataContainerItem1 item = (GeometryDataContainerItem1)lb_itemsa.Items[lb_itemsa.SelectedIndex];
				int[] list = item.Items;

				this.tb_itemsa2.Text = "0x"+Helper.HexString(list[lb_itemsa2.SelectedIndex]);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			} 
			finally 
			{
				lb_itemsa.Tag = null;
			}
		}

		private void SelectItemsB(object sender, System.EventArgs e)
		{
			if (lb_itemsb.Tag != null) return;
			if (lb_itemsb.SelectedIndex<0) return;
			try 
			{
				lb_itemsb.Tag = true;
				GeometryDataContainerItem2 item = (GeometryDataContainerItem2)lb_itemsb.Items[lb_itemsb.SelectedIndex];

				this.tb_uk4.Text = "0x"+Helper.HexString(item.VertexCount);
				this.tb_uk6.Text = "0x"+Helper.HexString(item.Unknown2);

				lb_itemsb2.Items.Clear();
				foreach (int i in item.Items1) lb_itemsb2.Items.Add(i);

				lb_itemsb3.Items.Clear();
				foreach (int i in item.Items2) lb_itemsb3.Items.Add(i);

				lb_itemsb4.Items.Clear();
				foreach (int i in item.Items3) lb_itemsb4.Items.Add(i);

				lb_itemsb5.Items.Clear();
				foreach (int i in item.Items4) lb_itemsb5.Items.Add(i);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			} 
			finally 
			{
				lb_itemsb.Tag = null;
			}
		}

		private void SelectItemsB2(object sender, System.EventArgs e)
		{
			if (lb_itemsb.Tag != null) return;
			if (lb_itemsb.SelectedIndex<0) return;
			if (lb_itemsb2.SelectedIndex<0) return;
			try 
			{
				lb_itemsb.Tag = true;
				GeometryDataContainerItem2 item = (GeometryDataContainerItem2)lb_itemsb.Items[lb_itemsb.SelectedIndex];
				int[] list = item.Items1;

				this.tb_itemsb2.Text = "0x"+Helper.HexString(list[lb_itemsb2.SelectedIndex]);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			} 
			finally 
			{
				lb_itemsb.Tag = null;
			}
		}

		private void SelectItemsB3(object sender, System.EventArgs e)
		{
			if (lb_itemsb.Tag != null) return;
			if (lb_itemsb.SelectedIndex<0) return;
			if (lb_itemsb3.SelectedIndex<0) return;
			try 
			{
				lb_itemsb.Tag = true;
				GeometryDataContainerItem2 item = (GeometryDataContainerItem2)lb_itemsb.Items[lb_itemsb.SelectedIndex];
				int[] list = item.Items1;

				this.tb_itemsb3.Text = "0x"+Helper.HexString(list[lb_itemsb3.SelectedIndex]);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			} 
			finally 
			{
				lb_itemsb.Tag = null;
			}
		}

		private void SelectItemsB4(object sender, System.EventArgs e)
		{
			if (lb_itemsb.Tag != null) return;
			if (lb_itemsb.SelectedIndex<0) return;
			if (lb_itemsb4.SelectedIndex<0) return;
			try 
			{
				lb_itemsb.Tag = true;
				GeometryDataContainerItem2 item = (GeometryDataContainerItem2)lb_itemsb.Items[lb_itemsb.SelectedIndex];
				int[] list = item.Items1;

				this.tb_itemsb4.Text = "0x"+Helper.HexString(list[lb_itemsb4.SelectedIndex]);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			} 
			finally 
			{
				lb_itemsb.Tag = null;
			}
		}

		private void SelectItemsB5(object sender, System.EventArgs e)
		{
			if (lb_itemsb.Tag != null) return;
			if (lb_itemsb.SelectedIndex<0) return;
			if (lb_itemsb5.SelectedIndex<0) return;
			try 
			{
				lb_itemsb.Tag = true;
				GeometryDataContainerItem2 item = (GeometryDataContainerItem2)lb_itemsb.Items[lb_itemsb.SelectedIndex];
				int[] list = item.Items1;

				this.tb_itemsb5.Text = "0x"+Helper.HexString(list[lb_itemsb5.SelectedIndex]);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			} 
			finally 
			{
				lb_itemsb.Tag = null;
			}
		}

		private void SelectItemsC(object sender, System.EventArgs e)
		{
			if (lb_itemsc.Tag != null) return;
			if (lb_itemsc.SelectedIndex<0) return;
			try 
			{
				lb_itemsc.Tag = true;
				GeometryDataContainerItem3 item = (GeometryDataContainerItem3)lb_itemsc.Items[lb_itemsc.SelectedIndex];

				this.tb_uk2.Text = "0x"+Helper.HexString(item.Unknown1);
				this.tb_uk3.Text = "0x"+Helper.HexString(item.Alternate);
				this.tb_opacity.Text = "0x"+Helper.HexString(item.Opacity);
				this.tb_itemsc_name.Text = item.Name;

				lb_itemsc2.Items.Clear();
				foreach (int i in item.Items1) lb_itemsc2.Items.Add(i);

				lb_itemsc3.Items.Clear();
				foreach (int i in item.Items2) lb_itemsc3.Items.Add(i);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			} 
			finally 
			{
				lb_itemsc.Tag = null;
			}
		}

		private void SelectItemsC2(object sender, System.EventArgs e)
		{
			if (lb_itemsc.Tag != null) return;
			if (lb_itemsc.SelectedIndex<0) return;
			if (lb_itemsc2.SelectedIndex<0) return;
			try 
			{
				lb_itemsc.Tag = true;
				GeometryDataContainerItem3 item = (GeometryDataContainerItem3)lb_itemsc.Items[lb_itemsc.SelectedIndex];
				int[] list = item.Items1;

				this.tb_itemsc2.Text = "0x"+Helper.HexString(list[lb_itemsc2.SelectedIndex]);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			} 
			finally 
			{
				lb_itemsc.Tag = null;
			}
		}

		private void SelectItemsC3(object sender, System.EventArgs e)
		{
			if (lb_itemsc.Tag != null) return;
			if (lb_itemsc.SelectedIndex<0) return;
			if (lb_itemsc3.SelectedIndex<0) return;
			try 
			{
				lb_itemsc.Tag = true;
				GeometryDataContainerItem3 item = (GeometryDataContainerItem3)lb_itemsc.Items[lb_itemsc.SelectedIndex];
				int[] list = item.Items2;

				this.tb_itemsc3.Text = "0x"+Helper.HexString(list[lb_itemsc3.SelectedIndex]);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			} 
			finally 
			{
				lb_itemsc.Tag = null;
			}
		}

		private void ChangeItemsC(object sender, System.EventArgs e)
		{
			if (lb_itemsc.Tag != null) return;
			if (lb_itemsc.SelectedIndex<0) return;
			try 
			{
				lb_itemsc.Tag = true;
				GeometryDataContainerItem3 item = (GeometryDataContainerItem3)lb_itemsc.Items[lb_itemsc.SelectedIndex];

				item.Unknown1 = (int)Convert.ToUInt32(this.tb_uk2.Text, 16);
				item.Alternate = (int)Convert.ToUInt32(this.tb_uk3.Text, 16);
				item.Opacity = (int)Convert.ToUInt32(this.tb_opacity.Text, 16);
				item.Name = this.tb_itemsc_name.Text;

				lb_itemsc.Items[lb_itemsc.SelectedIndex] = item;

				GeometryDataContainer gdc = (GeometryDataContainer)tGeometryDataContainer.Tag;
				gdc.Changed = true;
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			} 
			finally 
			{
				lb_itemsc.Tag = null;
			}
		}

		#region x Export
		private void ExportX(object sender, System.EventArgs e)
		{
			try
			{
				this.lb_itemsa.Tag = true;
				if (this.tGeometryDataContainer.Tag != null)
				{
					sfd.Filter = "Direct X Mesh (*.x)|*.x|All Files (*.*)|*.*";		
					sfd.AddExtension = true;
					GeometryDataContainer ext1 = (GeometryDataContainer) this.tGeometryDataContainer.Tag;						
					sfd.FileName = Hashes.StripHashFromName(ext1.Parent.FileName).Trim().ToLower();
					if (!sfd.FileName.EndsWith(".x")) sfd.FileName += ".x";
					if (sfd.ShowDialog() == DialogResult.OK) 
					{
						MemoryStream s = (MemoryStream)ext1.GenerateX(GetModels());		
						StreamReader sr = new StreamReader(s);

						
						System.IO.StreamWriter meshwriter = File.CreateText(sfd.FileName);						
						meshwriter.Write(sr.ReadToEnd());
						meshwriter.Close();
					}
				}				
			}
			catch (Exception exception1)
			{
				Helper.ExceptionMessage("", exception1);
				return;
			}
			finally
			{
				this.lb_itemsa.Tag = null;
			}
		}
		#endregion
		
		#region obj Export
		private void ExportObj(object sender, System.EventArgs e)
		{
			try
			{
				this.lb_itemsa.Tag = true;
				if (this.tGeometryDataContainer.Tag != null)
				{
					sfd.Filter = "Maya Object File (*.obj)|*.obj|All Files (*.*)|*.*";
					sfd.AddExtension = true;
					GeometryDataContainer ext1 = (GeometryDataContainer) this.tGeometryDataContainer.Tag;						
					sfd.FileName = Hashes.StripHashFromName(ext1.Parent.FileName).Trim().ToLower();
					if (!sfd.FileName.EndsWith(".obj")) sfd.FileName += ".obj";
					if (sfd.ShowDialog() == DialogResult.OK) 
					{
						MemoryStream s = (MemoryStream)ext1.GenerateObj(GetModels());		
						StreamReader sr = new StreamReader(s);

						
						System.IO.StreamWriter meshwriter = File.CreateText(sfd.FileName);						
						meshwriter.Write(sr.ReadToEnd());
						meshwriter.Close();
					}
				}				
			}
			catch (Exception exception1)
			{
				Helper.ExceptionMessage("", exception1);
				return;
			}
			finally
			{
				this.lb_itemsa.Tag = null;
			}
		}
		#endregion

		Ambertation.Panel3D curpn = null;
		private void Preview(object sender, System.EventArgs e)
		{
			
			if (this.tGeometryDataContainer.Tag != null)
			{
				WaitingScreen.Wait();
				GeometryDataContainer ext1 = (GeometryDataContainer) this.tGeometryDataContainer.Tag;
				Stream xfile = ext1.GenerateX(GetModels());				
				try 
				{
					//stop all running Previews
					Ambertation.Panel3D.StopAll();

					TextureLocator tl = new TextureLocator(ext1.Parent.Package);
					System.Collections.Hashtable txtrs = tl.GetLargestImages(tl.FindTextures(ext1.Parent));

					Ambertation.ViewportSetting vp = null;
					if (curpn!=null) vp = curpn.ViewportSetting;
					curpn = new Ambertation.Panel3D(this.pnprev, new Point(0, 0), new Size(Math.Min(pnprev.Width, pnprev.Height), Math.Min(pnprev.Width, pnprev.Height)), xfile, txtrs, vp);
				} 
				catch (System.IO.FileNotFoundException)
				{
					WaitingScreen.Stop();
					if (MessageBox.Show("The Microsoft Managed DirectX Extensions were not found on your System. Without them, the Preview is not available.\n\nYou can install them manually, by extracting the content of the DirectX\\ManagedDX.CAB on your Sims 2 Installation CD #1. If you double click on the extracted msi File, all needed Files will be installed.\n\nYou can also let SimPE install it automatically. SimPE will download the needed Files (3.5MB) from the SimPE Homepage and install them. Do you want SimPE to download and install the Files?", "Warning", MessageBoxButtons.YesNo)==DialogResult.Yes)
					{
						if (WebUpdate.InstallMDX()) MessageBox.Show("Managed DirectX Extension were installed succesfully!");
					}
					
					return;
				}
				catch (Exception ex)
				{
					WaitingScreen.Stop();
					Helper.ExceptionMessage("", ex);
					return;
				}
				curpn.BackColor = cd.Color;
				curpn.BorderStyle = BorderStyle.FixedSingle;
				WaitingScreen.Stop();
				
			}		
		}

		/// <summary>
		/// Get all Selected Models
		/// </summary>
		/// <returns></returns>
		System.Collections.ArrayList GetModels()
		{
			System.Collections.ArrayList list = new ArrayList();
			for (int i=0; i<lbmodel.CheckedItems.Count; i++)
			{
				list.Add(lbmodel.CheckedItems[i]);
			}

			return list;
		}

		private void PickColor(object sender, System.EventArgs e)
		{
			if (curpn!=null) cd.Color = curpn.BackColor;
			if (cd.ShowDialog()==DialogResult.OK)
			{
				if (curpn!=null) curpn.BackColor = cd.Color;
			}
		}
	}
}
