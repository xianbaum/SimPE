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
using SimPe.Plugin;
using SimPe.Interfaces.Plugin;

namespace SimPe.Plugin
{
	/// <summary>
	/// Zusammenfassung für Bhav2Form.
	/// </summary>
	public class Bhav2Form : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.Panel wrapperPanel;
		internal System.Windows.Forms.LinkLabel llsort;
		private System.Windows.Forms.Label label16;
		internal System.Windows.Forms.TextBox lbbhav;
		private System.Windows.Forms.ComboBox tba2;
		private System.Windows.Forms.ComboBox tba1;
		internal System.Windows.Forms.LinkLabel lldel;
		private System.Windows.Forms.LinkLabel lladd;
		internal System.Windows.Forms.TextBox tbres;
		internal System.Windows.Forms.TextBox tbopcode;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.TextBox lbtext;
		internal System.Windows.Forms.TextBox tbzero;
		internal System.Windows.Forms.TextBox tblocals;
		internal System.Windows.Forms.TextBox tbflags;
		internal System.Windows.Forms.TextBox tbargc;
		internal System.Windows.Forms.TextBox tbtype;
		internal System.Windows.Forms.TextBox tbformat;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btcommit;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.ListBox lbinst;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tpInst;
		private System.Windows.Forms.TabPage tpOpcode;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Bhav2Form()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();

			//
			// TODO: Fügen Sie den Konstruktorcode nach dem Aufruf von InitializeComponent hinzu
			//
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
			this.wrapperPanel = new System.Windows.Forms.Panel();
			this.lbinst = new System.Windows.Forms.ListBox();
			this.llsort = new System.Windows.Forms.LinkLabel();
			this.label16 = new System.Windows.Forms.Label();
			this.lbbhav = new System.Windows.Forms.TextBox();
			this.tba2 = new System.Windows.Forms.ComboBox();
			this.tba1 = new System.Windows.Forms.ComboBox();
			this.lldel = new System.Windows.Forms.LinkLabel();
			this.lladd = new System.Windows.Forms.LinkLabel();
			this.tbres = new System.Windows.Forms.TextBox();
			this.tbopcode = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.lbtext = new System.Windows.Forms.TextBox();
			this.tbzero = new System.Windows.Forms.TextBox();
			this.tblocals = new System.Windows.Forms.TextBox();
			this.tbflags = new System.Windows.Forms.TextBox();
			this.tbargc = new System.Windows.Forms.TextBox();
			this.tbtype = new System.Windows.Forms.TextBox();
			this.tbformat = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btcommit = new System.Windows.Forms.Button();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpInst = new System.Windows.Forms.TabPage();
			this.tpOpcode = new System.Windows.Forms.TabPage();
			this.wrapperPanel.SuspendLayout();
			this.panel3.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tpInst.SuspendLayout();
			this.SuspendLayout();
			// 
			// wrapperPanel
			// 
			this.wrapperPanel.AutoScroll = true;
			this.wrapperPanel.Controls.Add(this.tabControl1);
			this.wrapperPanel.Controls.Add(this.llsort);
			this.wrapperPanel.Controls.Add(this.label16);
			this.wrapperPanel.Controls.Add(this.lbbhav);
			this.wrapperPanel.Controls.Add(this.tbzero);
			this.wrapperPanel.Controls.Add(this.tblocals);
			this.wrapperPanel.Controls.Add(this.tbflags);
			this.wrapperPanel.Controls.Add(this.tbargc);
			this.wrapperPanel.Controls.Add(this.tbtype);
			this.wrapperPanel.Controls.Add(this.tbformat);
			this.wrapperPanel.Controls.Add(this.label7);
			this.wrapperPanel.Controls.Add(this.label6);
			this.wrapperPanel.Controls.Add(this.label5);
			this.wrapperPanel.Controls.Add(this.label4);
			this.wrapperPanel.Controls.Add(this.label3);
			this.wrapperPanel.Controls.Add(this.label2);
			this.wrapperPanel.Controls.Add(this.btcommit);
			this.wrapperPanel.Controls.Add(this.panel3);
			this.wrapperPanel.Controls.Add(this.lbinst);
			this.wrapperPanel.Controls.Add(this.lldel);
			this.wrapperPanel.Controls.Add(this.lladd);
			this.wrapperPanel.Location = new System.Drawing.Point(8, 8);
			this.wrapperPanel.Name = "wrapperPanel";
			this.wrapperPanel.Size = new System.Drawing.Size(776, 368);
			this.wrapperPanel.TabIndex = 4;
			// 
			// lbinst
			// 
			this.lbinst.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lbinst.Location = new System.Drawing.Point(16, 128);
			this.lbinst.Name = "lbinst";
			this.lbinst.Size = new System.Drawing.Size(336, 199);
			this.lbinst.TabIndex = 41;
			this.lbinst.SelectedIndexChanged += new System.EventHandler(this.SelectInstruction);
			// 
			// llsort
			// 
			this.llsort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.llsort.AutoSize = true;
			this.llsort.Enabled = false;
			this.llsort.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
			this.llsort.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.llsort.LinkArea = new System.Windows.Forms.LinkArea(13, 4);
			this.llsort.Location = new System.Drawing.Point(16, 336);
			this.llsort.Name = "llsort";
			this.llsort.Size = new System.Drawing.Size(110, 17);
			this.llsort.TabIndex = 40;
			this.llsort.TabStop = true;
			this.llsort.Text = "experimental sort";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Enabled = false;
			this.label16.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
			this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label16.Location = new System.Drawing.Point(16, 32);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(67, 17);
			this.label16.TabIndex = 20;
			this.label16.Text = "Filename:";
			// 
			// lbbhav
			// 
			this.lbbhav.Enabled = false;
			this.lbbhav.Location = new System.Drawing.Point(88, 32);
			this.lbbhav.MaxLength = 64;
			this.lbbhav.Name = "lbbhav";
			this.lbbhav.Size = new System.Drawing.Size(544, 20);
			this.lbbhav.TabIndex = 19;
			this.lbbhav.Text = "";
			// 
			// tba2
			// 
			this.tba2.AllowDrop = true;
			this.tba2.Enabled = false;
			this.tba2.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tba2.ItemHeight = 13;
			this.tba2.Items.AddRange(new object[] {
													  "Error",
													  "Return True",
													  "Return False"});
			this.tba2.Location = new System.Drawing.Point(296, 32);
			this.tba2.Name = "tba2";
			this.tba2.Size = new System.Drawing.Size(96, 21);
			this.tba2.TabIndex = 37;
			this.tba2.Text = "Error";
			// 
			// tba1
			// 
			this.tba1.AllowDrop = true;
			this.tba1.Enabled = false;
			this.tba1.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tba1.ItemHeight = 13;
			this.tba1.Items.AddRange(new object[] {
													  "Error",
													  "Return True",
													  "Return False"});
			this.tba1.Location = new System.Drawing.Point(88, 32);
			this.tba1.Name = "tba1";
			this.tba1.Size = new System.Drawing.Size(96, 21);
			this.tba1.TabIndex = 36;
			this.tba1.Text = "0x0";
			// 
			// lldel
			// 
			this.lldel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lldel.AutoSize = true;
			this.lldel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
			this.lldel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lldel.LinkArea = new System.Windows.Forms.LinkArea(0, 6);
			this.lldel.Location = new System.Drawing.Point(272, 336);
			this.lldel.Name = "lldel";
			this.lldel.Size = new System.Drawing.Size(44, 17);
			this.lldel.TabIndex = 35;
			this.lldel.TabStop = true;
			this.lldel.Text = "delete";
			// 
			// lladd
			// 
			this.lladd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lladd.AutoSize = true;
			this.lladd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
			this.lladd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lladd.LinkArea = new System.Windows.Forms.LinkArea(0, 3);
			this.lladd.Location = new System.Drawing.Point(320, 336);
			this.lladd.Name = "lladd";
			this.lladd.Size = new System.Drawing.Size(28, 17);
			this.lladd.TabIndex = 34;
			this.lladd.TabStop = true;
			this.lladd.Text = "add";
			// 
			// tbres
			// 
			this.tbres.Enabled = false;
			this.tbres.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbres.Location = new System.Drawing.Point(296, 8);
			this.tbres.Name = "tbres";
			this.tbres.Size = new System.Drawing.Size(96, 21);
			this.tbres.TabIndex = 12;
			this.tbres.Text = "0x0";
			// 
			// tbopcode
			// 
			this.tbopcode.Enabled = false;
			this.tbopcode.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbopcode.Location = new System.Drawing.Point(88, 8);
			this.tbopcode.Name = "tbopcode";
			this.tbopcode.Size = new System.Drawing.Size(96, 21);
			this.tbopcode.TabIndex = 11;
			this.tbopcode.Text = "0x0";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label12.Location = new System.Drawing.Point(232, 16);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(62, 17);
			this.label12.TabIndex = 8;
			this.label12.Text = "Reserved:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label11.Location = new System.Drawing.Point(24, 16);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(54, 17);
			this.label11.TabIndex = 7;
			this.label11.Text = "OpCode:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label10.Location = new System.Drawing.Point(216, 40);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(79, 17);
			this.label10.TabIndex = 6;
			this.label10.Text = "False Target:";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label9.Location = new System.Drawing.Point(8, 40);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(76, 17);
			this.label9.TabIndex = 5;
			this.label9.Text = "True Target:";
			// 
			// button4
			// 
			this.button4.Enabled = false;
			this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.button4.Font = new System.Drawing.Font("Wingdings 3", 8.25F, System.Drawing.FontStyle.Bold);
			this.button4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.button4.Location = new System.Drawing.Point(184, 8);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(21, 21);
			this.button4.TabIndex = 40;
			this.button4.Text = "u";
			// 
			// lbtext
			// 
			this.lbtext.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lbtext.BackColor = System.Drawing.SystemColors.Control;
			this.lbtext.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lbtext.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.lbtext.Location = new System.Drawing.Point(8, 80);
			this.lbtext.Multiline = true;
			this.lbtext.Name = "lbtext";
			this.lbtext.ReadOnly = true;
			this.lbtext.Size = new System.Drawing.Size(384, 88);
			this.lbtext.TabIndex = 42;
			this.lbtext.Text = "";
			// 
			// tbzero
			// 
			this.tbzero.Enabled = false;
			this.tbzero.Location = new System.Drawing.Point(536, 88);
			this.tbzero.Name = "tbzero";
			this.tbzero.Size = new System.Drawing.Size(96, 20);
			this.tbzero.TabIndex = 15;
			this.tbzero.Text = "";
			// 
			// tblocals
			// 
			this.tblocals.Enabled = false;
			this.tblocals.Location = new System.Drawing.Point(536, 64);
			this.tblocals.Name = "tblocals";
			this.tblocals.Size = new System.Drawing.Size(96, 20);
			this.tblocals.TabIndex = 14;
			this.tblocals.Text = "";
			// 
			// tbflags
			// 
			this.tbflags.Enabled = false;
			this.tbflags.Location = new System.Drawing.Point(312, 88);
			this.tbflags.Name = "tbflags";
			this.tbflags.Size = new System.Drawing.Size(96, 20);
			this.tbflags.TabIndex = 13;
			this.tbflags.Text = "";
			// 
			// tbargc
			// 
			this.tbargc.Enabled = false;
			this.tbargc.Location = new System.Drawing.Point(312, 64);
			this.tbargc.Name = "tbargc";
			this.tbargc.Size = new System.Drawing.Size(96, 20);
			this.tbargc.TabIndex = 12;
			this.tbargc.Text = "";
			// 
			// tbtype
			// 
			this.tbtype.Enabled = false;
			this.tbtype.Location = new System.Drawing.Point(80, 88);
			this.tbtype.Name = "tbtype";
			this.tbtype.Size = new System.Drawing.Size(96, 20);
			this.tbtype.TabIndex = 11;
			this.tbtype.Text = "";
			// 
			// tbformat
			// 
			this.tbformat.Enabled = false;
			this.tbformat.Location = new System.Drawing.Point(80, 64);
			this.tbformat.Name = "tbformat";
			this.tbformat.Size = new System.Drawing.Size(96, 20);
			this.tbformat.TabIndex = 10;
			this.tbformat.Text = "";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Enabled = false;
			this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
			this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label7.Location = new System.Drawing.Point(464, 96);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(68, 17);
			this.label7.TabIndex = 9;
			this.label7.Text = "Reserved:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Enabled = false;
			this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
			this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label6.Location = new System.Drawing.Point(248, 96);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(56, 17);
			this.label6.TabIndex = 8;
			this.label6.Text = "Flags??:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Enabled = false;
			this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
			this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label5.Location = new System.Drawing.Point(16, 96);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(53, 17);
			this.label5.TabIndex = 7;
			this.label5.Text = "Type??:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Enabled = false;
			this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
			this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label4.Location = new System.Drawing.Point(424, 72);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(109, 17);
			this.label4.TabIndex = 6;
			this.label4.Text = "Local Var Count:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Enabled = false;
			this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
			this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label3.Location = new System.Drawing.Point(192, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(113, 17);
			this.label3.TabIndex = 5;
			this.label3.Text = "Argument Count:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Enabled = false;
			this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
			this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label2.Location = new System.Drawing.Point(16, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 17);
			this.label2.TabIndex = 4;
			this.label2.Text = "Format:";
			// 
			// btcommit
			// 
			this.btcommit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btcommit.Enabled = false;
			this.btcommit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btcommit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btcommit.Location = new System.Drawing.Point(664, 336);
			this.btcommit.Name = "btcommit";
			this.btcommit.Size = new System.Drawing.Size(104, 23);
			this.btcommit.TabIndex = 2;
			this.btcommit.Text = "Commit File";
			// 
			// panel3
			// 
			this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel3.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.panel3.Controls.Add(this.label1);
			this.panel3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
			this.panel3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(776, 24);
			this.panel3.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label1.Location = new System.Drawing.Point(0, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(262, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "Behaviour Editor (just for testing)";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tpInst);
			this.tabControl1.Controls.Add(this.tpOpcode);
			this.tabControl1.Location = new System.Drawing.Point(360, 120);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(408, 200);
			this.tabControl1.TabIndex = 42;
			// 
			// tpInst
			// 
			this.tpInst.Controls.Add(this.tbopcode);
			this.tpInst.Controls.Add(this.label12);
			this.tpInst.Controls.Add(this.label11);
			this.tpInst.Controls.Add(this.label10);
			this.tpInst.Controls.Add(this.label9);
			this.tpInst.Controls.Add(this.button4);
			this.tpInst.Controls.Add(this.tba1);
			this.tpInst.Controls.Add(this.tba2);
			this.tpInst.Controls.Add(this.tbres);
			this.tpInst.Controls.Add(this.lbtext);
			this.tpInst.Location = new System.Drawing.Point(4, 22);
			this.tpInst.Name = "tpInst";
			this.tpInst.Size = new System.Drawing.Size(400, 174);
			this.tpInst.TabIndex = 0;
			this.tpInst.Text = "Instruction";
			// 
			// tpOpcode
			// 
			this.tpOpcode.Location = new System.Drawing.Point(4, 22);
			this.tpOpcode.Name = "tpOpcode";
			this.tpOpcode.Size = new System.Drawing.Size(400, 174);
			this.tpOpcode.TabIndex = 1;
			this.tpOpcode.Text = "Opcodes";
			// 
			// Bhav2Form
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(896, 390);
			this.Controls.Add(this.wrapperPanel);
			this.Name = "Bhav2Form";
			this.Text = "Bhav2Form";
			this.wrapperPanel.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tpInst.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		internal Bhav2 wrapper;

		/// <summary>
		/// Called by the Opcode Wizards to update the Listbox
		/// </summary>
		/// <param name="inst">The new Instruction Data</param>
		void ForceUpdate(Instruction inst) 
		{
			if (lbinst.SelectedIndex<0) return;
			lbinst.Tag = true;
			try 
			{
				lbinst.Items[lbinst.SelectedIndex] = inst;
			} 
			finally 
			{
				lbinst.Tag = null;
			}
		}

		/// <summary>
		/// Will Display the Data of an Instruction
		/// </summary>
		/// <param name="inst">The Instruction you wanna Display</param>
		private void ShowInstruction(Instruction2 inst) 
		{
			if (lbinst.Tag!=null) return;
			IOpcodeWizard wz = inst.Wizard;
			tpOpcode.Controls.Clear();
			if (wz!=null) 
			{
				Panel pn = wz.Show(inst, new DecoderRegistry.ForceUpdate(this.ForceUpdate));
				pn.Parent = tpOpcode;
				pn.Left = 0;
				pn.Top = 0;
				pn.Width = tpOpcode.Width;
				pn.Height = tpOpcode.Height;

				pn.Visible = true;
			}

			//now for normal Instruction Informations
			tbopcode.Text = "0x"+Helper.HexString(inst.OpCode);
			tbres.Text = "0x"+Helper.HexString(inst.Reserved0);
			tba1.Text = "0x"+Helper.HexString(inst.Target1);
			tba2.Text = "0x"+Helper.HexString(inst.Target2);
		}

		private void SelectInstruction(object sender, System.EventArgs e)
		{
			if (lbinst.SelectedIndex<0) return;
			ShowInstruction((Instruction2)lbinst.Items[lbinst.SelectedIndex]);
		}
	}
}
