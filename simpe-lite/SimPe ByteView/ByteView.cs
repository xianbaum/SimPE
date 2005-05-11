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
using SimPe.Interfaces.Files;

namespace SimPe
{
	/// <summary>
	/// Zusammenfassung für ByteView.
	/// </summary>
	public class ByteView : System.Windows.Forms.Form
	{		
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tboffset;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbbyte;
		private System.Windows.Forms.TextBox tbword;
		private System.Windows.Forms.TextBox tbuword;
		private System.Windows.Forms.TextBox tbint;
		private System.Windows.Forms.TextBox tbuint;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rbdec;
		private System.Windows.Forms.RadioButton rbhex;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.LinkLabel linkLabel3;
		private System.Windows.Forms.LinkLabel linkLabel4;
		private System.Windows.Forms.LinkLabel linkLabel5;
		private System.Windows.Forms.LinkLabel linkLabel6;
		private System.Windows.Forms.Button button1;
		private SourceGrid2.Grid grid;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox tbcols;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.TextBox tbsimname;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox tbsimnamei;
		private System.Windows.Forms.LinkLabel linkLabel7;
		private System.Windows.Forms.LinkLabel llmem;
		private System.Windows.Forms.CheckBox cbmarked;
		private System.Windows.Forms.LinkLabel llgobyte;

		
		public ByteView()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();

			grid.Selection.CellGotFocus += new SourceGrid2.CellGotFocusEventHandler(CellGotFocusEventHandler);
			grid.Selection.SelectionMode = SourceGrid2.GridSelectionMode.Cell;
			grid.Selection.EnableMultiSelection = false;
			grid.Selection.BorderMode = SourceGrid2.SelectionBorderMode.Selection;

			zerocell.BackColor = Color.LemonChiffon;
			simidcell.BackColor = Color.SteelBlue;
			memidcell.BackColor = Color.Tomato;
			markedcell.BackColor = Color.SaddleBrown;

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ByteView));
			this.panel1 = new System.Windows.Forms.Panel();
			this.llmem = new System.Windows.Forms.LinkLabel();
			this.linkLabel7 = new System.Windows.Forms.LinkLabel();
			this.tbsimnamei = new System.Windows.Forms.TextBox();
			this.tbsimname = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.linkLabel6 = new System.Windows.Forms.LinkLabel();
			this.linkLabel5 = new System.Windows.Forms.LinkLabel();
			this.linkLabel4 = new System.Windows.Forms.LinkLabel();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.llgobyte = new System.Windows.Forms.LinkLabel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.tbcols = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.rbhex = new System.Windows.Forms.RadioButton();
			this.rbdec = new System.Windows.Forms.RadioButton();
			this.tbuint = new System.Windows.Forms.TextBox();
			this.tbint = new System.Windows.Forms.TextBox();
			this.tbuword = new System.Windows.Forms.TextBox();
			this.tbword = new System.Windows.Forms.TextBox();
			this.tbbyte = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tboffset = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.grid = new SourceGrid2.Grid();
			this.cbmarked = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.AccessibleDescription = resources.GetString("panel1.AccessibleDescription");
			this.panel1.AccessibleName = resources.GetString("panel1.AccessibleName");
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel1.Anchor")));
			this.panel1.AutoScroll = ((bool)(resources.GetObject("panel1.AutoScroll")));
			this.panel1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel1.AutoScrollMargin")));
			this.panel1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel1.AutoScrollMinSize")));
			this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
			this.panel1.Controls.Add(this.cbmarked);
			this.panel1.Controls.Add(this.llmem);
			this.panel1.Controls.Add(this.linkLabel7);
			this.panel1.Controls.Add(this.tbsimnamei);
			this.panel1.Controls.Add(this.tbsimname);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.linkLabel6);
			this.panel1.Controls.Add(this.linkLabel5);
			this.panel1.Controls.Add(this.linkLabel4);
			this.panel1.Controls.Add(this.linkLabel3);
			this.panel1.Controls.Add(this.linkLabel2);
			this.panel1.Controls.Add(this.llgobyte);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.tbuint);
			this.panel1.Controls.Add(this.tbint);
			this.panel1.Controls.Add(this.tbuword);
			this.panel1.Controls.Add(this.tbword);
			this.panel1.Controls.Add(this.tbbyte);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.tboffset);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panel1.Dock")));
			this.panel1.Enabled = ((bool)(resources.GetObject("panel1.Enabled")));
			this.panel1.Font = ((System.Drawing.Font)(resources.GetObject("panel1.Font")));
			this.panel1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panel1.ImeMode")));
			this.panel1.Location = ((System.Drawing.Point)(resources.GetObject("panel1.Location")));
			this.panel1.Name = "panel1";
			this.panel1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panel1.RightToLeft")));
			this.panel1.Size = ((System.Drawing.Size)(resources.GetObject("panel1.Size")));
			this.panel1.TabIndex = ((int)(resources.GetObject("panel1.TabIndex")));
			this.panel1.Text = resources.GetString("panel1.Text");
			this.panel1.Visible = ((bool)(resources.GetObject("panel1.Visible")));
			// 
			// llmem
			// 
			this.llmem.AccessibleDescription = resources.GetString("llmem.AccessibleDescription");
			this.llmem.AccessibleName = resources.GetString("llmem.AccessibleName");
			this.llmem.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llmem.Anchor")));
			this.llmem.AutoSize = ((bool)(resources.GetObject("llmem.AutoSize")));
			this.llmem.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llmem.Dock")));
			this.llmem.Enabled = ((bool)(resources.GetObject("llmem.Enabled")));
			this.llmem.Font = ((System.Drawing.Font)(resources.GetObject("llmem.Font")));
			this.llmem.Image = ((System.Drawing.Image)(resources.GetObject("llmem.Image")));
			this.llmem.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llmem.ImageAlign")));
			this.llmem.ImageIndex = ((int)(resources.GetObject("llmem.ImageIndex")));
			this.llmem.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llmem.ImeMode")));
			this.llmem.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llmem.LinkArea")));
			this.llmem.Location = ((System.Drawing.Point)(resources.GetObject("llmem.Location")));
			this.llmem.Name = "llmem";
			this.llmem.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llmem.RightToLeft")));
			this.llmem.Size = ((System.Drawing.Size)(resources.GetObject("llmem.Size")));
			this.llmem.TabIndex = ((int)(resources.GetObject("llmem.TabIndex")));
			this.llmem.TabStop = true;
			this.llmem.Text = resources.GetString("llmem.Text");
			this.llmem.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llmem.TextAlign")));
			this.llmem.Visible = ((bool)(resources.GetObject("llmem.Visible")));
			this.llmem.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HighlightMemories);
			// 
			// linkLabel7
			// 
			this.linkLabel7.AccessibleDescription = resources.GetString("linkLabel7.AccessibleDescription");
			this.linkLabel7.AccessibleName = resources.GetString("linkLabel7.AccessibleName");
			this.linkLabel7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("linkLabel7.Anchor")));
			this.linkLabel7.AutoSize = ((bool)(resources.GetObject("linkLabel7.AutoSize")));
			this.linkLabel7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("linkLabel7.Dock")));
			this.linkLabel7.Enabled = ((bool)(resources.GetObject("linkLabel7.Enabled")));
			this.linkLabel7.Font = ((System.Drawing.Font)(resources.GetObject("linkLabel7.Font")));
			this.linkLabel7.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel7.Image")));
			this.linkLabel7.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel7.ImageAlign")));
			this.linkLabel7.ImageIndex = ((int)(resources.GetObject("linkLabel7.ImageIndex")));
			this.linkLabel7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("linkLabel7.ImeMode")));
			this.linkLabel7.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("linkLabel7.LinkArea")));
			this.linkLabel7.Location = ((System.Drawing.Point)(resources.GetObject("linkLabel7.Location")));
			this.linkLabel7.Name = "linkLabel7";
			this.linkLabel7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("linkLabel7.RightToLeft")));
			this.linkLabel7.Size = ((System.Drawing.Size)(resources.GetObject("linkLabel7.Size")));
			this.linkLabel7.TabIndex = ((int)(resources.GetObject("linkLabel7.TabIndex")));
			this.linkLabel7.TabStop = true;
			this.linkLabel7.Text = resources.GetString("linkLabel7.Text");
			this.linkLabel7.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel7.TextAlign")));
			this.linkLabel7.Visible = ((bool)(resources.GetObject("linkLabel7.Visible")));
			this.linkLabel7.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ScanForSimID);
			// 
			// tbsimnamei
			// 
			this.tbsimnamei.AccessibleDescription = resources.GetString("tbsimnamei.AccessibleDescription");
			this.tbsimnamei.AccessibleName = resources.GetString("tbsimnamei.AccessibleName");
			this.tbsimnamei.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbsimnamei.Anchor")));
			this.tbsimnamei.AutoSize = ((bool)(resources.GetObject("tbsimnamei.AutoSize")));
			this.tbsimnamei.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbsimnamei.BackgroundImage")));
			this.tbsimnamei.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbsimnamei.Dock")));
			this.tbsimnamei.Enabled = ((bool)(resources.GetObject("tbsimnamei.Enabled")));
			this.tbsimnamei.Font = ((System.Drawing.Font)(resources.GetObject("tbsimnamei.Font")));
			this.tbsimnamei.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbsimnamei.ImeMode")));
			this.tbsimnamei.Location = ((System.Drawing.Point)(resources.GetObject("tbsimnamei.Location")));
			this.tbsimnamei.MaxLength = ((int)(resources.GetObject("tbsimnamei.MaxLength")));
			this.tbsimnamei.Multiline = ((bool)(resources.GetObject("tbsimnamei.Multiline")));
			this.tbsimnamei.Name = "tbsimnamei";
			this.tbsimnamei.PasswordChar = ((char)(resources.GetObject("tbsimnamei.PasswordChar")));
			this.tbsimnamei.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbsimnamei.RightToLeft")));
			this.tbsimnamei.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbsimnamei.ScrollBars")));
			this.tbsimnamei.Size = ((System.Drawing.Size)(resources.GetObject("tbsimnamei.Size")));
			this.tbsimnamei.TabIndex = ((int)(resources.GetObject("tbsimnamei.TabIndex")));
			this.tbsimnamei.Text = resources.GetString("tbsimnamei.Text");
			this.tbsimnamei.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbsimnamei.TextAlign")));
			this.tbsimnamei.Visible = ((bool)(resources.GetObject("tbsimnamei.Visible")));
			this.tbsimnamei.WordWrap = ((bool)(resources.GetObject("tbsimnamei.WordWrap")));
			// 
			// tbsimname
			// 
			this.tbsimname.AccessibleDescription = resources.GetString("tbsimname.AccessibleDescription");
			this.tbsimname.AccessibleName = resources.GetString("tbsimname.AccessibleName");
			this.tbsimname.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbsimname.Anchor")));
			this.tbsimname.AutoSize = ((bool)(resources.GetObject("tbsimname.AutoSize")));
			this.tbsimname.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbsimname.BackgroundImage")));
			this.tbsimname.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbsimname.Dock")));
			this.tbsimname.Enabled = ((bool)(resources.GetObject("tbsimname.Enabled")));
			this.tbsimname.Font = ((System.Drawing.Font)(resources.GetObject("tbsimname.Font")));
			this.tbsimname.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbsimname.ImeMode")));
			this.tbsimname.Location = ((System.Drawing.Point)(resources.GetObject("tbsimname.Location")));
			this.tbsimname.MaxLength = ((int)(resources.GetObject("tbsimname.MaxLength")));
			this.tbsimname.Multiline = ((bool)(resources.GetObject("tbsimname.Multiline")));
			this.tbsimname.Name = "tbsimname";
			this.tbsimname.PasswordChar = ((char)(resources.GetObject("tbsimname.PasswordChar")));
			this.tbsimname.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbsimname.RightToLeft")));
			this.tbsimname.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbsimname.ScrollBars")));
			this.tbsimname.Size = ((System.Drawing.Size)(resources.GetObject("tbsimname.Size")));
			this.tbsimname.TabIndex = ((int)(resources.GetObject("tbsimname.TabIndex")));
			this.tbsimname.Text = resources.GetString("tbsimname.Text");
			this.tbsimname.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbsimname.TextAlign")));
			this.tbsimname.Visible = ((bool)(resources.GetObject("tbsimname.Visible")));
			this.tbsimname.WordWrap = ((bool)(resources.GetObject("tbsimname.WordWrap")));
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
			// linkLabel6
			// 
			this.linkLabel6.AccessibleDescription = resources.GetString("linkLabel6.AccessibleDescription");
			this.linkLabel6.AccessibleName = resources.GetString("linkLabel6.AccessibleName");
			this.linkLabel6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("linkLabel6.Anchor")));
			this.linkLabel6.AutoSize = ((bool)(resources.GetObject("linkLabel6.AutoSize")));
			this.linkLabel6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("linkLabel6.Dock")));
			this.linkLabel6.Enabled = ((bool)(resources.GetObject("linkLabel6.Enabled")));
			this.linkLabel6.Font = ((System.Drawing.Font)(resources.GetObject("linkLabel6.Font")));
			this.linkLabel6.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel6.Image")));
			this.linkLabel6.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel6.ImageAlign")));
			this.linkLabel6.ImageIndex = ((int)(resources.GetObject("linkLabel6.ImageIndex")));
			this.linkLabel6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("linkLabel6.ImeMode")));
			this.linkLabel6.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("linkLabel6.LinkArea")));
			this.linkLabel6.Location = ((System.Drawing.Point)(resources.GetObject("linkLabel6.Location")));
			this.linkLabel6.Name = "linkLabel6";
			this.linkLabel6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("linkLabel6.RightToLeft")));
			this.linkLabel6.Size = ((System.Drawing.Size)(resources.GetObject("linkLabel6.Size")));
			this.linkLabel6.TabIndex = ((int)(resources.GetObject("linkLabel6.TabIndex")));
			this.linkLabel6.TabStop = true;
			this.linkLabel6.Text = resources.GetString("linkLabel6.Text");
			this.linkLabel6.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel6.TextAlign")));
			this.linkLabel6.Visible = ((bool)(resources.GetObject("linkLabel6.Visible")));
			this.linkLabel6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GoOffsetClicked);
			// 
			// linkLabel5
			// 
			this.linkLabel5.AccessibleDescription = resources.GetString("linkLabel5.AccessibleDescription");
			this.linkLabel5.AccessibleName = resources.GetString("linkLabel5.AccessibleName");
			this.linkLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("linkLabel5.Anchor")));
			this.linkLabel5.AutoSize = ((bool)(resources.GetObject("linkLabel5.AutoSize")));
			this.linkLabel5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("linkLabel5.Dock")));
			this.linkLabel5.Enabled = ((bool)(resources.GetObject("linkLabel5.Enabled")));
			this.linkLabel5.Font = ((System.Drawing.Font)(resources.GetObject("linkLabel5.Font")));
			this.linkLabel5.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel5.Image")));
			this.linkLabel5.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel5.ImageAlign")));
			this.linkLabel5.ImageIndex = ((int)(resources.GetObject("linkLabel5.ImageIndex")));
			this.linkLabel5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("linkLabel5.ImeMode")));
			this.linkLabel5.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("linkLabel5.LinkArea")));
			this.linkLabel5.Location = ((System.Drawing.Point)(resources.GetObject("linkLabel5.Location")));
			this.linkLabel5.Name = "linkLabel5";
			this.linkLabel5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("linkLabel5.RightToLeft")));
			this.linkLabel5.Size = ((System.Drawing.Size)(resources.GetObject("linkLabel5.Size")));
			this.linkLabel5.TabIndex = ((int)(resources.GetObject("linkLabel5.TabIndex")));
			this.linkLabel5.TabStop = true;
			this.linkLabel5.Text = resources.GetString("linkLabel5.Text");
			this.linkLabel5.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel5.TextAlign")));
			this.linkLabel5.Visible = ((bool)(resources.GetObject("linkLabel5.Visible")));
			this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GoUShortClicked);
			// 
			// linkLabel4
			// 
			this.linkLabel4.AccessibleDescription = resources.GetString("linkLabel4.AccessibleDescription");
			this.linkLabel4.AccessibleName = resources.GetString("linkLabel4.AccessibleName");
			this.linkLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("linkLabel4.Anchor")));
			this.linkLabel4.AutoSize = ((bool)(resources.GetObject("linkLabel4.AutoSize")));
			this.linkLabel4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("linkLabel4.Dock")));
			this.linkLabel4.Enabled = ((bool)(resources.GetObject("linkLabel4.Enabled")));
			this.linkLabel4.Font = ((System.Drawing.Font)(resources.GetObject("linkLabel4.Font")));
			this.linkLabel4.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel4.Image")));
			this.linkLabel4.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel4.ImageAlign")));
			this.linkLabel4.ImageIndex = ((int)(resources.GetObject("linkLabel4.ImageIndex")));
			this.linkLabel4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("linkLabel4.ImeMode")));
			this.linkLabel4.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("linkLabel4.LinkArea")));
			this.linkLabel4.Location = ((System.Drawing.Point)(resources.GetObject("linkLabel4.Location")));
			this.linkLabel4.Name = "linkLabel4";
			this.linkLabel4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("linkLabel4.RightToLeft")));
			this.linkLabel4.Size = ((System.Drawing.Size)(resources.GetObject("linkLabel4.Size")));
			this.linkLabel4.TabIndex = ((int)(resources.GetObject("linkLabel4.TabIndex")));
			this.linkLabel4.TabStop = true;
			this.linkLabel4.Text = resources.GetString("linkLabel4.Text");
			this.linkLabel4.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel4.TextAlign")));
			this.linkLabel4.Visible = ((bool)(resources.GetObject("linkLabel4.Visible")));
			this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GoUIntClicked);
			// 
			// linkLabel3
			// 
			this.linkLabel3.AccessibleDescription = resources.GetString("linkLabel3.AccessibleDescription");
			this.linkLabel3.AccessibleName = resources.GetString("linkLabel3.AccessibleName");
			this.linkLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("linkLabel3.Anchor")));
			this.linkLabel3.AutoSize = ((bool)(resources.GetObject("linkLabel3.AutoSize")));
			this.linkLabel3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("linkLabel3.Dock")));
			this.linkLabel3.Enabled = ((bool)(resources.GetObject("linkLabel3.Enabled")));
			this.linkLabel3.Font = ((System.Drawing.Font)(resources.GetObject("linkLabel3.Font")));
			this.linkLabel3.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel3.Image")));
			this.linkLabel3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel3.ImageAlign")));
			this.linkLabel3.ImageIndex = ((int)(resources.GetObject("linkLabel3.ImageIndex")));
			this.linkLabel3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("linkLabel3.ImeMode")));
			this.linkLabel3.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("linkLabel3.LinkArea")));
			this.linkLabel3.Location = ((System.Drawing.Point)(resources.GetObject("linkLabel3.Location")));
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("linkLabel3.RightToLeft")));
			this.linkLabel3.Size = ((System.Drawing.Size)(resources.GetObject("linkLabel3.Size")));
			this.linkLabel3.TabIndex = ((int)(resources.GetObject("linkLabel3.TabIndex")));
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = resources.GetString("linkLabel3.Text");
			this.linkLabel3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel3.TextAlign")));
			this.linkLabel3.Visible = ((bool)(resources.GetObject("linkLabel3.Visible")));
			this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GoIntClicked);
			// 
			// linkLabel2
			// 
			this.linkLabel2.AccessibleDescription = resources.GetString("linkLabel2.AccessibleDescription");
			this.linkLabel2.AccessibleName = resources.GetString("linkLabel2.AccessibleName");
			this.linkLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("linkLabel2.Anchor")));
			this.linkLabel2.AutoSize = ((bool)(resources.GetObject("linkLabel2.AutoSize")));
			this.linkLabel2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("linkLabel2.Dock")));
			this.linkLabel2.Enabled = ((bool)(resources.GetObject("linkLabel2.Enabled")));
			this.linkLabel2.Font = ((System.Drawing.Font)(resources.GetObject("linkLabel2.Font")));
			this.linkLabel2.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel2.Image")));
			this.linkLabel2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel2.ImageAlign")));
			this.linkLabel2.ImageIndex = ((int)(resources.GetObject("linkLabel2.ImageIndex")));
			this.linkLabel2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("linkLabel2.ImeMode")));
			this.linkLabel2.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("linkLabel2.LinkArea")));
			this.linkLabel2.Location = ((System.Drawing.Point)(resources.GetObject("linkLabel2.Location")));
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("linkLabel2.RightToLeft")));
			this.linkLabel2.Size = ((System.Drawing.Size)(resources.GetObject("linkLabel2.Size")));
			this.linkLabel2.TabIndex = ((int)(resources.GetObject("linkLabel2.TabIndex")));
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = resources.GetString("linkLabel2.Text");
			this.linkLabel2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("linkLabel2.TextAlign")));
			this.linkLabel2.Visible = ((bool)(resources.GetObject("linkLabel2.Visible")));
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GoShortClicked);
			// 
			// llgobyte
			// 
			this.llgobyte.AccessibleDescription = resources.GetString("llgobyte.AccessibleDescription");
			this.llgobyte.AccessibleName = resources.GetString("llgobyte.AccessibleName");
			this.llgobyte.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llgobyte.Anchor")));
			this.llgobyte.AutoSize = ((bool)(resources.GetObject("llgobyte.AutoSize")));
			this.llgobyte.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llgobyte.Dock")));
			this.llgobyte.Enabled = ((bool)(resources.GetObject("llgobyte.Enabled")));
			this.llgobyte.Font = ((System.Drawing.Font)(resources.GetObject("llgobyte.Font")));
			this.llgobyte.Image = ((System.Drawing.Image)(resources.GetObject("llgobyte.Image")));
			this.llgobyte.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llgobyte.ImageAlign")));
			this.llgobyte.ImageIndex = ((int)(resources.GetObject("llgobyte.ImageIndex")));
			this.llgobyte.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llgobyte.ImeMode")));
			this.llgobyte.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llgobyte.LinkArea")));
			this.llgobyte.Location = ((System.Drawing.Point)(resources.GetObject("llgobyte.Location")));
			this.llgobyte.Name = "llgobyte";
			this.llgobyte.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llgobyte.RightToLeft")));
			this.llgobyte.Size = ((System.Drawing.Size)(resources.GetObject("llgobyte.Size")));
			this.llgobyte.TabIndex = ((int)(resources.GetObject("llgobyte.TabIndex")));
			this.llgobyte.TabStop = true;
			this.llgobyte.Text = resources.GetString("llgobyte.Text");
			this.llgobyte.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llgobyte.TextAlign")));
			this.llgobyte.Visible = ((bool)(resources.GetObject("llgobyte.Visible")));
			this.llgobyte.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GoByteClicked);
			// 
			// groupBox1
			// 
			this.groupBox1.AccessibleDescription = resources.GetString("groupBox1.AccessibleDescription");
			this.groupBox1.AccessibleName = resources.GetString("groupBox1.AccessibleName");
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox1.Anchor")));
			this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
			this.groupBox1.Controls.Add(this.linkLabel1);
			this.groupBox1.Controls.Add(this.tbcols);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.rbhex);
			this.groupBox1.Controls.Add(this.rbdec);
			this.groupBox1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("groupBox1.Dock")));
			this.groupBox1.Enabled = ((bool)(resources.GetObject("groupBox1.Enabled")));
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Font = ((System.Drawing.Font)(resources.GetObject("groupBox1.Font")));
			this.groupBox1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("groupBox1.ImeMode")));
			this.groupBox1.Location = ((System.Drawing.Point)(resources.GetObject("groupBox1.Location")));
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("groupBox1.RightToLeft")));
			this.groupBox1.Size = ((System.Drawing.Size)(resources.GetObject("groupBox1.Size")));
			this.groupBox1.TabIndex = ((int)(resources.GetObject("groupBox1.TabIndex")));
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = resources.GetString("groupBox1.Text");
			this.groupBox1.Visible = ((bool)(resources.GetObject("groupBox1.Visible")));
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
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SetColumns);
			// 
			// tbcols
			// 
			this.tbcols.AccessibleDescription = resources.GetString("tbcols.AccessibleDescription");
			this.tbcols.AccessibleName = resources.GetString("tbcols.AccessibleName");
			this.tbcols.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbcols.Anchor")));
			this.tbcols.AutoSize = ((bool)(resources.GetObject("tbcols.AutoSize")));
			this.tbcols.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbcols.BackgroundImage")));
			this.tbcols.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbcols.Dock")));
			this.tbcols.Enabled = ((bool)(resources.GetObject("tbcols.Enabled")));
			this.tbcols.Font = ((System.Drawing.Font)(resources.GetObject("tbcols.Font")));
			this.tbcols.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbcols.ImeMode")));
			this.tbcols.Location = ((System.Drawing.Point)(resources.GetObject("tbcols.Location")));
			this.tbcols.MaxLength = ((int)(resources.GetObject("tbcols.MaxLength")));
			this.tbcols.Multiline = ((bool)(resources.GetObject("tbcols.Multiline")));
			this.tbcols.Name = "tbcols";
			this.tbcols.PasswordChar = ((char)(resources.GetObject("tbcols.PasswordChar")));
			this.tbcols.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbcols.RightToLeft")));
			this.tbcols.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbcols.ScrollBars")));
			this.tbcols.Size = ((System.Drawing.Size)(resources.GetObject("tbcols.Size")));
			this.tbcols.TabIndex = ((int)(resources.GetObject("tbcols.TabIndex")));
			this.tbcols.Text = resources.GetString("tbcols.Text");
			this.tbcols.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbcols.TextAlign")));
			this.tbcols.Visible = ((bool)(resources.GetObject("tbcols.Visible")));
			this.tbcols.WordWrap = ((bool)(resources.GetObject("tbcols.WordWrap")));
			// 
			// label7
			// 
			this.label7.AccessibleDescription = resources.GetString("label7.AccessibleDescription");
			this.label7.AccessibleName = resources.GetString("label7.AccessibleName");
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label7.Anchor")));
			this.label7.AutoSize = ((bool)(resources.GetObject("label7.AutoSize")));
			this.label7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label7.Dock")));
			this.label7.Enabled = ((bool)(resources.GetObject("label7.Enabled")));
			this.label7.Font = ((System.Drawing.Font)(resources.GetObject("label7.Font")));
			this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
			this.label7.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label7.ImageAlign")));
			this.label7.ImageIndex = ((int)(resources.GetObject("label7.ImageIndex")));
			this.label7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label7.ImeMode")));
			this.label7.Location = ((System.Drawing.Point)(resources.GetObject("label7.Location")));
			this.label7.Name = "label7";
			this.label7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label7.RightToLeft")));
			this.label7.Size = ((System.Drawing.Size)(resources.GetObject("label7.Size")));
			this.label7.TabIndex = ((int)(resources.GetObject("label7.TabIndex")));
			this.label7.Text = resources.GetString("label7.Text");
			this.label7.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label7.TextAlign")));
			this.label7.Visible = ((bool)(resources.GetObject("label7.Visible")));
			// 
			// rbhex
			// 
			this.rbhex.AccessibleDescription = resources.GetString("rbhex.AccessibleDescription");
			this.rbhex.AccessibleName = resources.GetString("rbhex.AccessibleName");
			this.rbhex.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rbhex.Anchor")));
			this.rbhex.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("rbhex.Appearance")));
			this.rbhex.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbhex.BackgroundImage")));
			this.rbhex.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbhex.CheckAlign")));
			this.rbhex.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rbhex.Dock")));
			this.rbhex.Enabled = ((bool)(resources.GetObject("rbhex.Enabled")));
			this.rbhex.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("rbhex.FlatStyle")));
			this.rbhex.Font = ((System.Drawing.Font)(resources.GetObject("rbhex.Font")));
			this.rbhex.Image = ((System.Drawing.Image)(resources.GetObject("rbhex.Image")));
			this.rbhex.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbhex.ImageAlign")));
			this.rbhex.ImageIndex = ((int)(resources.GetObject("rbhex.ImageIndex")));
			this.rbhex.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rbhex.ImeMode")));
			this.rbhex.Location = ((System.Drawing.Point)(resources.GetObject("rbhex.Location")));
			this.rbhex.Name = "rbhex";
			this.rbhex.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rbhex.RightToLeft")));
			this.rbhex.Size = ((System.Drawing.Size)(resources.GetObject("rbhex.Size")));
			this.rbhex.TabIndex = ((int)(resources.GetObject("rbhex.TabIndex")));
			this.rbhex.Text = resources.GetString("rbhex.Text");
			this.rbhex.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbhex.TextAlign")));
			this.rbhex.Visible = ((bool)(resources.GetObject("rbhex.Visible")));
			this.rbhex.CheckedChanged += new System.EventHandler(this.DisplayModeChanged);
			// 
			// rbdec
			// 
			this.rbdec.AccessibleDescription = resources.GetString("rbdec.AccessibleDescription");
			this.rbdec.AccessibleName = resources.GetString("rbdec.AccessibleName");
			this.rbdec.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rbdec.Anchor")));
			this.rbdec.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("rbdec.Appearance")));
			this.rbdec.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbdec.BackgroundImage")));
			this.rbdec.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbdec.CheckAlign")));
			this.rbdec.Checked = true;
			this.rbdec.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rbdec.Dock")));
			this.rbdec.Enabled = ((bool)(resources.GetObject("rbdec.Enabled")));
			this.rbdec.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("rbdec.FlatStyle")));
			this.rbdec.Font = ((System.Drawing.Font)(resources.GetObject("rbdec.Font")));
			this.rbdec.Image = ((System.Drawing.Image)(resources.GetObject("rbdec.Image")));
			this.rbdec.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbdec.ImageAlign")));
			this.rbdec.ImageIndex = ((int)(resources.GetObject("rbdec.ImageIndex")));
			this.rbdec.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rbdec.ImeMode")));
			this.rbdec.Location = ((System.Drawing.Point)(resources.GetObject("rbdec.Location")));
			this.rbdec.Name = "rbdec";
			this.rbdec.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rbdec.RightToLeft")));
			this.rbdec.Size = ((System.Drawing.Size)(resources.GetObject("rbdec.Size")));
			this.rbdec.TabIndex = ((int)(resources.GetObject("rbdec.TabIndex")));
			this.rbdec.TabStop = true;
			this.rbdec.Text = resources.GetString("rbdec.Text");
			this.rbdec.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbdec.TextAlign")));
			this.rbdec.Visible = ((bool)(resources.GetObject("rbdec.Visible")));
			this.rbdec.CheckedChanged += new System.EventHandler(this.DisplayModeChanged);
			// 
			// tbuint
			// 
			this.tbuint.AccessibleDescription = resources.GetString("tbuint.AccessibleDescription");
			this.tbuint.AccessibleName = resources.GetString("tbuint.AccessibleName");
			this.tbuint.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbuint.Anchor")));
			this.tbuint.AutoSize = ((bool)(resources.GetObject("tbuint.AutoSize")));
			this.tbuint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbuint.BackgroundImage")));
			this.tbuint.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbuint.Dock")));
			this.tbuint.Enabled = ((bool)(resources.GetObject("tbuint.Enabled")));
			this.tbuint.Font = ((System.Drawing.Font)(resources.GetObject("tbuint.Font")));
			this.tbuint.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbuint.ImeMode")));
			this.tbuint.Location = ((System.Drawing.Point)(resources.GetObject("tbuint.Location")));
			this.tbuint.MaxLength = ((int)(resources.GetObject("tbuint.MaxLength")));
			this.tbuint.Multiline = ((bool)(resources.GetObject("tbuint.Multiline")));
			this.tbuint.Name = "tbuint";
			this.tbuint.PasswordChar = ((char)(resources.GetObject("tbuint.PasswordChar")));
			this.tbuint.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbuint.RightToLeft")));
			this.tbuint.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbuint.ScrollBars")));
			this.tbuint.Size = ((System.Drawing.Size)(resources.GetObject("tbuint.Size")));
			this.tbuint.TabIndex = ((int)(resources.GetObject("tbuint.TabIndex")));
			this.tbuint.Text = resources.GetString("tbuint.Text");
			this.tbuint.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbuint.TextAlign")));
			this.tbuint.Visible = ((bool)(resources.GetObject("tbuint.Visible")));
			this.tbuint.WordWrap = ((bool)(resources.GetObject("tbuint.WordWrap")));
			this.tbuint.TextChanged += new System.EventHandler(this.UIntTextChanged);
			// 
			// tbint
			// 
			this.tbint.AccessibleDescription = resources.GetString("tbint.AccessibleDescription");
			this.tbint.AccessibleName = resources.GetString("tbint.AccessibleName");
			this.tbint.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbint.Anchor")));
			this.tbint.AutoSize = ((bool)(resources.GetObject("tbint.AutoSize")));
			this.tbint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbint.BackgroundImage")));
			this.tbint.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbint.Dock")));
			this.tbint.Enabled = ((bool)(resources.GetObject("tbint.Enabled")));
			this.tbint.Font = ((System.Drawing.Font)(resources.GetObject("tbint.Font")));
			this.tbint.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbint.ImeMode")));
			this.tbint.Location = ((System.Drawing.Point)(resources.GetObject("tbint.Location")));
			this.tbint.MaxLength = ((int)(resources.GetObject("tbint.MaxLength")));
			this.tbint.Multiline = ((bool)(resources.GetObject("tbint.Multiline")));
			this.tbint.Name = "tbint";
			this.tbint.PasswordChar = ((char)(resources.GetObject("tbint.PasswordChar")));
			this.tbint.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbint.RightToLeft")));
			this.tbint.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbint.ScrollBars")));
			this.tbint.Size = ((System.Drawing.Size)(resources.GetObject("tbint.Size")));
			this.tbint.TabIndex = ((int)(resources.GetObject("tbint.TabIndex")));
			this.tbint.Text = resources.GetString("tbint.Text");
			this.tbint.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbint.TextAlign")));
			this.tbint.Visible = ((bool)(resources.GetObject("tbint.Visible")));
			this.tbint.WordWrap = ((bool)(resources.GetObject("tbint.WordWrap")));
			this.tbint.TextChanged += new System.EventHandler(this.IntTextChanged);
			// 
			// tbuword
			// 
			this.tbuword.AccessibleDescription = resources.GetString("tbuword.AccessibleDescription");
			this.tbuword.AccessibleName = resources.GetString("tbuword.AccessibleName");
			this.tbuword.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbuword.Anchor")));
			this.tbuword.AutoSize = ((bool)(resources.GetObject("tbuword.AutoSize")));
			this.tbuword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbuword.BackgroundImage")));
			this.tbuword.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbuword.Dock")));
			this.tbuword.Enabled = ((bool)(resources.GetObject("tbuword.Enabled")));
			this.tbuword.Font = ((System.Drawing.Font)(resources.GetObject("tbuword.Font")));
			this.tbuword.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbuword.ImeMode")));
			this.tbuword.Location = ((System.Drawing.Point)(resources.GetObject("tbuword.Location")));
			this.tbuword.MaxLength = ((int)(resources.GetObject("tbuword.MaxLength")));
			this.tbuword.Multiline = ((bool)(resources.GetObject("tbuword.Multiline")));
			this.tbuword.Name = "tbuword";
			this.tbuword.PasswordChar = ((char)(resources.GetObject("tbuword.PasswordChar")));
			this.tbuword.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbuword.RightToLeft")));
			this.tbuword.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbuword.ScrollBars")));
			this.tbuword.Size = ((System.Drawing.Size)(resources.GetObject("tbuword.Size")));
			this.tbuword.TabIndex = ((int)(resources.GetObject("tbuword.TabIndex")));
			this.tbuword.Text = resources.GetString("tbuword.Text");
			this.tbuword.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbuword.TextAlign")));
			this.tbuword.Visible = ((bool)(resources.GetObject("tbuword.Visible")));
			this.tbuword.WordWrap = ((bool)(resources.GetObject("tbuword.WordWrap")));
			this.tbuword.TextChanged += new System.EventHandler(this.UShortTextChanged);
			// 
			// tbword
			// 
			this.tbword.AccessibleDescription = resources.GetString("tbword.AccessibleDescription");
			this.tbword.AccessibleName = resources.GetString("tbword.AccessibleName");
			this.tbword.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbword.Anchor")));
			this.tbword.AutoSize = ((bool)(resources.GetObject("tbword.AutoSize")));
			this.tbword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbword.BackgroundImage")));
			this.tbword.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbword.Dock")));
			this.tbword.Enabled = ((bool)(resources.GetObject("tbword.Enabled")));
			this.tbword.Font = ((System.Drawing.Font)(resources.GetObject("tbword.Font")));
			this.tbword.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbword.ImeMode")));
			this.tbword.Location = ((System.Drawing.Point)(resources.GetObject("tbword.Location")));
			this.tbword.MaxLength = ((int)(resources.GetObject("tbword.MaxLength")));
			this.tbword.Multiline = ((bool)(resources.GetObject("tbword.Multiline")));
			this.tbword.Name = "tbword";
			this.tbword.PasswordChar = ((char)(resources.GetObject("tbword.PasswordChar")));
			this.tbword.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbword.RightToLeft")));
			this.tbword.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbword.ScrollBars")));
			this.tbword.Size = ((System.Drawing.Size)(resources.GetObject("tbword.Size")));
			this.tbword.TabIndex = ((int)(resources.GetObject("tbword.TabIndex")));
			this.tbword.Text = resources.GetString("tbword.Text");
			this.tbword.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbword.TextAlign")));
			this.tbword.Visible = ((bool)(resources.GetObject("tbword.Visible")));
			this.tbword.WordWrap = ((bool)(resources.GetObject("tbword.WordWrap")));
			this.tbword.TextChanged += new System.EventHandler(this.ShortTextChanged);
			// 
			// tbbyte
			// 
			this.tbbyte.AccessibleDescription = resources.GetString("tbbyte.AccessibleDescription");
			this.tbbyte.AccessibleName = resources.GetString("tbbyte.AccessibleName");
			this.tbbyte.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbbyte.Anchor")));
			this.tbbyte.AutoSize = ((bool)(resources.GetObject("tbbyte.AutoSize")));
			this.tbbyte.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbbyte.BackgroundImage")));
			this.tbbyte.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbbyte.Dock")));
			this.tbbyte.Enabled = ((bool)(resources.GetObject("tbbyte.Enabled")));
			this.tbbyte.Font = ((System.Drawing.Font)(resources.GetObject("tbbyte.Font")));
			this.tbbyte.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbbyte.ImeMode")));
			this.tbbyte.Location = ((System.Drawing.Point)(resources.GetObject("tbbyte.Location")));
			this.tbbyte.MaxLength = ((int)(resources.GetObject("tbbyte.MaxLength")));
			this.tbbyte.Multiline = ((bool)(resources.GetObject("tbbyte.Multiline")));
			this.tbbyte.Name = "tbbyte";
			this.tbbyte.PasswordChar = ((char)(resources.GetObject("tbbyte.PasswordChar")));
			this.tbbyte.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbbyte.RightToLeft")));
			this.tbbyte.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbbyte.ScrollBars")));
			this.tbbyte.Size = ((System.Drawing.Size)(resources.GetObject("tbbyte.Size")));
			this.tbbyte.TabIndex = ((int)(resources.GetObject("tbbyte.TabIndex")));
			this.tbbyte.Text = resources.GetString("tbbyte.Text");
			this.tbbyte.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbbyte.TextAlign")));
			this.tbbyte.Visible = ((bool)(resources.GetObject("tbbyte.Visible")));
			this.tbbyte.WordWrap = ((bool)(resources.GetObject("tbbyte.WordWrap")));
			this.tbbyte.TextChanged += new System.EventHandler(this.ByteTextChanged);
			// 
			// label6
			// 
			this.label6.AccessibleDescription = resources.GetString("label6.AccessibleDescription");
			this.label6.AccessibleName = resources.GetString("label6.AccessibleName");
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label6.Anchor")));
			this.label6.AutoSize = ((bool)(resources.GetObject("label6.AutoSize")));
			this.label6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label6.Dock")));
			this.label6.Enabled = ((bool)(resources.GetObject("label6.Enabled")));
			this.label6.Font = ((System.Drawing.Font)(resources.GetObject("label6.Font")));
			this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
			this.label6.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.ImageAlign")));
			this.label6.ImageIndex = ((int)(resources.GetObject("label6.ImageIndex")));
			this.label6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label6.ImeMode")));
			this.label6.Location = ((System.Drawing.Point)(resources.GetObject("label6.Location")));
			this.label6.Name = "label6";
			this.label6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label6.RightToLeft")));
			this.label6.Size = ((System.Drawing.Size)(resources.GetObject("label6.Size")));
			this.label6.TabIndex = ((int)(resources.GetObject("label6.TabIndex")));
			this.label6.Text = resources.GetString("label6.Text");
			this.label6.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.TextAlign")));
			this.label6.Visible = ((bool)(resources.GetObject("label6.Visible")));
			// 
			// label5
			// 
			this.label5.AccessibleDescription = resources.GetString("label5.AccessibleDescription");
			this.label5.AccessibleName = resources.GetString("label5.AccessibleName");
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label5.Anchor")));
			this.label5.AutoSize = ((bool)(resources.GetObject("label5.AutoSize")));
			this.label5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label5.Dock")));
			this.label5.Enabled = ((bool)(resources.GetObject("label5.Enabled")));
			this.label5.Font = ((System.Drawing.Font)(resources.GetObject("label5.Font")));
			this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
			this.label5.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label5.ImageAlign")));
			this.label5.ImageIndex = ((int)(resources.GetObject("label5.ImageIndex")));
			this.label5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label5.ImeMode")));
			this.label5.Location = ((System.Drawing.Point)(resources.GetObject("label5.Location")));
			this.label5.Name = "label5";
			this.label5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label5.RightToLeft")));
			this.label5.Size = ((System.Drawing.Size)(resources.GetObject("label5.Size")));
			this.label5.TabIndex = ((int)(resources.GetObject("label5.TabIndex")));
			this.label5.Text = resources.GetString("label5.Text");
			this.label5.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label5.TextAlign")));
			this.label5.Visible = ((bool)(resources.GetObject("label5.Visible")));
			// 
			// label4
			// 
			this.label4.AccessibleDescription = resources.GetString("label4.AccessibleDescription");
			this.label4.AccessibleName = resources.GetString("label4.AccessibleName");
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label4.Anchor")));
			this.label4.AutoSize = ((bool)(resources.GetObject("label4.AutoSize")));
			this.label4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label4.Dock")));
			this.label4.Enabled = ((bool)(resources.GetObject("label4.Enabled")));
			this.label4.Font = ((System.Drawing.Font)(resources.GetObject("label4.Font")));
			this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
			this.label4.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.ImageAlign")));
			this.label4.ImageIndex = ((int)(resources.GetObject("label4.ImageIndex")));
			this.label4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label4.ImeMode")));
			this.label4.Location = ((System.Drawing.Point)(resources.GetObject("label4.Location")));
			this.label4.Name = "label4";
			this.label4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label4.RightToLeft")));
			this.label4.Size = ((System.Drawing.Size)(resources.GetObject("label4.Size")));
			this.label4.TabIndex = ((int)(resources.GetObject("label4.TabIndex")));
			this.label4.Text = resources.GetString("label4.Text");
			this.label4.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.TextAlign")));
			this.label4.Visible = ((bool)(resources.GetObject("label4.Visible")));
			// 
			// label3
			// 
			this.label3.AccessibleDescription = resources.GetString("label3.AccessibleDescription");
			this.label3.AccessibleName = resources.GetString("label3.AccessibleName");
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label3.Anchor")));
			this.label3.AutoSize = ((bool)(resources.GetObject("label3.AutoSize")));
			this.label3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label3.Dock")));
			this.label3.Enabled = ((bool)(resources.GetObject("label3.Enabled")));
			this.label3.Font = ((System.Drawing.Font)(resources.GetObject("label3.Font")));
			this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
			this.label3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label3.ImageAlign")));
			this.label3.ImageIndex = ((int)(resources.GetObject("label3.ImageIndex")));
			this.label3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label3.ImeMode")));
			this.label3.Location = ((System.Drawing.Point)(resources.GetObject("label3.Location")));
			this.label3.Name = "label3";
			this.label3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label3.RightToLeft")));
			this.label3.Size = ((System.Drawing.Size)(resources.GetObject("label3.Size")));
			this.label3.TabIndex = ((int)(resources.GetObject("label3.TabIndex")));
			this.label3.Text = resources.GetString("label3.Text");
			this.label3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label3.TextAlign")));
			this.label3.Visible = ((bool)(resources.GetObject("label3.Visible")));
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
			// tboffset
			// 
			this.tboffset.AccessibleDescription = resources.GetString("tboffset.AccessibleDescription");
			this.tboffset.AccessibleName = resources.GetString("tboffset.AccessibleName");
			this.tboffset.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tboffset.Anchor")));
			this.tboffset.AutoSize = ((bool)(resources.GetObject("tboffset.AutoSize")));
			this.tboffset.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tboffset.BackgroundImage")));
			this.tboffset.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tboffset.Dock")));
			this.tboffset.Enabled = ((bool)(resources.GetObject("tboffset.Enabled")));
			this.tboffset.Font = ((System.Drawing.Font)(resources.GetObject("tboffset.Font")));
			this.tboffset.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tboffset.ImeMode")));
			this.tboffset.Location = ((System.Drawing.Point)(resources.GetObject("tboffset.Location")));
			this.tboffset.MaxLength = ((int)(resources.GetObject("tboffset.MaxLength")));
			this.tboffset.Multiline = ((bool)(resources.GetObject("tboffset.Multiline")));
			this.tboffset.Name = "tboffset";
			this.tboffset.PasswordChar = ((char)(resources.GetObject("tboffset.PasswordChar")));
			this.tboffset.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tboffset.RightToLeft")));
			this.tboffset.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tboffset.ScrollBars")));
			this.tboffset.Size = ((System.Drawing.Size)(resources.GetObject("tboffset.Size")));
			this.tboffset.TabIndex = ((int)(resources.GetObject("tboffset.TabIndex")));
			this.tboffset.Text = resources.GetString("tboffset.Text");
			this.tboffset.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tboffset.TextAlign")));
			this.tboffset.Visible = ((bool)(resources.GetObject("tboffset.Visible")));
			this.tboffset.WordWrap = ((bool)(resources.GetObject("tboffset.WordWrap")));
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
			// panel2
			// 
			this.panel2.AccessibleDescription = resources.GetString("panel2.AccessibleDescription");
			this.panel2.AccessibleName = resources.GetString("panel2.AccessibleName");
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel2.Anchor")));
			this.panel2.AutoScroll = ((bool)(resources.GetObject("panel2.AutoScroll")));
			this.panel2.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel2.AutoScrollMargin")));
			this.panel2.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel2.AutoScrollMinSize")));
			this.panel2.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
			this.panel2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panel2.Dock")));
			this.panel2.Enabled = ((bool)(resources.GetObject("panel2.Enabled")));
			this.panel2.Font = ((System.Drawing.Font)(resources.GetObject("panel2.Font")));
			this.panel2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panel2.ImeMode")));
			this.panel2.Location = ((System.Drawing.Point)(resources.GetObject("panel2.Location")));
			this.panel2.Name = "panel2";
			this.panel2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panel2.RightToLeft")));
			this.panel2.Size = ((System.Drawing.Size)(resources.GetObject("panel2.Size")));
			this.panel2.TabIndex = ((int)(resources.GetObject("panel2.TabIndex")));
			this.panel2.Text = resources.GetString("panel2.Text");
			this.panel2.Visible = ((bool)(resources.GetObject("panel2.Visible")));
			// 
			// button1
			// 
			this.button1.AccessibleDescription = resources.GetString("button1.AccessibleDescription");
			this.button1.AccessibleName = resources.GetString("button1.AccessibleName");
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("button1.Anchor")));
			this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
			this.button1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("button1.Dock")));
			this.button1.Enabled = ((bool)(resources.GetObject("button1.Enabled")));
			this.button1.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("button1.FlatStyle")));
			this.button1.Font = ((System.Drawing.Font)(resources.GetObject("button1.Font")));
			this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
			this.button1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("button1.ImageAlign")));
			this.button1.ImageIndex = ((int)(resources.GetObject("button1.ImageIndex")));
			this.button1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("button1.ImeMode")));
			this.button1.Location = ((System.Drawing.Point)(resources.GetObject("button1.Location")));
			this.button1.Name = "button1";
			this.button1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("button1.RightToLeft")));
			this.button1.Size = ((System.Drawing.Size)(resources.GetObject("button1.Size")));
			this.button1.TabIndex = ((int)(resources.GetObject("button1.TabIndex")));
			this.button1.Text = resources.GetString("button1.Text");
			this.button1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("button1.TextAlign")));
			this.button1.Visible = ((bool)(resources.GetObject("button1.Visible")));
			this.button1.Click += new System.EventHandler(this.CommitClick);
			// 
			// grid
			// 
			this.grid.AccessibleDescription = resources.GetString("grid.AccessibleDescription");
			this.grid.AccessibleName = resources.GetString("grid.AccessibleName");
			this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grid.Anchor")));
			this.grid.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("grid.AutoScrollMargin")));
			this.grid.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("grid.AutoScrollMinSize")));
			this.grid.AutoSizeMinHeight = 10;
			this.grid.AutoSizeMinWidth = 10;
			this.grid.AutoStretchColumnsToFitWidth = false;
			this.grid.AutoStretchRowsToFitHeight = false;
			this.grid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grid.BackgroundImage")));
			this.grid.ContextMenuStyle = SourceGrid2.ContextMenuStyle.None;
			this.grid.CustomSort = false;
			this.grid.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grid.Dock")));
			this.grid.Enabled = ((bool)(resources.GetObject("grid.Enabled")));
			this.grid.Font = ((System.Drawing.Font)(resources.GetObject("grid.Font")));
			this.grid.GridToolTipActive = true;
			this.grid.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grid.ImeMode")));
			this.grid.Location = ((System.Drawing.Point)(resources.GetObject("grid.Location")));
			this.grid.Name = "grid";
			this.grid.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grid.RightToLeft")));
			this.grid.Size = ((System.Drawing.Size)(resources.GetObject("grid.Size")));
			this.grid.SpecialKeys = SourceGrid2.GridSpecialKeys.Default;
			this.grid.TabIndex = ((int)(resources.GetObject("grid.TabIndex")));
			this.grid.Text = resources.GetString("grid.Text");
			this.grid.Visible = ((bool)(resources.GetObject("grid.Visible")));
			this.grid.Resize += new System.EventHandler(this.GridResized);
			// 
			// cbmarked
			// 
			this.cbmarked.AccessibleDescription = resources.GetString("cbmarked.AccessibleDescription");
			this.cbmarked.AccessibleName = resources.GetString("cbmarked.AccessibleName");
			this.cbmarked.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbmarked.Anchor")));
			this.cbmarked.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbmarked.Appearance")));
			this.cbmarked.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbmarked.BackgroundImage")));
			this.cbmarked.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbmarked.CheckAlign")));
			this.cbmarked.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbmarked.Dock")));
			this.cbmarked.Enabled = ((bool)(resources.GetObject("cbmarked.Enabled")));
			this.cbmarked.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbmarked.FlatStyle")));
			this.cbmarked.Font = ((System.Drawing.Font)(resources.GetObject("cbmarked.Font")));
			this.cbmarked.Image = ((System.Drawing.Image)(resources.GetObject("cbmarked.Image")));
			this.cbmarked.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbmarked.ImageAlign")));
			this.cbmarked.ImageIndex = ((int)(resources.GetObject("cbmarked.ImageIndex")));
			this.cbmarked.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbmarked.ImeMode")));
			this.cbmarked.Location = ((System.Drawing.Point)(resources.GetObject("cbmarked.Location")));
			this.cbmarked.Name = "cbmarked";
			this.cbmarked.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbmarked.RightToLeft")));
			this.cbmarked.Size = ((System.Drawing.Size)(resources.GetObject("cbmarked.Size")));
			this.cbmarked.TabIndex = ((int)(resources.GetObject("cbmarked.TabIndex")));
			this.cbmarked.Text = resources.GetString("cbmarked.Text");
			this.cbmarked.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbmarked.TextAlign")));
			this.cbmarked.Visible = ((bool)(resources.GetObject("cbmarked.Visible")));
			this.cbmarked.CheckedChanged += new System.EventHandler(this.MarkCell);
			// 
			// ByteView
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.grid);
			this.Controls.Add(this.panel1);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "ByteView";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ClosingForm);
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		
		/// <summary>
		/// Stores the currently selected Cell
		/// </summary>
		SourceGrid2.CellGotFocusEventArgs cellcache = null;

		/// <summary>
		/// True if the Textboxes are changed by the influence of a System Event (not user Input)
		/// </summary>
		bool triggeredchange = false;

		/// <summary>
		/// Contains the Style for Zeroed Cells
		/// </summary>
		SourceGrid2.VisualModels.Common zerocell =  new SourceGrid2.VisualModels.Common();

		/// <summary>
		/// Contains the Style for SimID Cells
		/// </summary>
		SourceGrid2.VisualModels.Common simidcell =  new SourceGrid2.VisualModels.Common();

		/// <summary>
		/// Contains the Style for MemID Cells
		/// </summary>
		SourceGrid2.VisualModels.Common memidcell =  new SourceGrid2.VisualModels.Common();

		/// <summary>
		/// Contains the Style for MemID Cells
		/// </summary>
		SourceGrid2.VisualModels.Common markedcell =  new SourceGrid2.VisualModels.Common();

		/// <summary>
		/// Current Offset (only valid if cellcache is set)
		/// </summary>
		uint offset = 0;

		/// <summary>
		/// True if the OK Button was clicked
		/// </summary>
		bool ok = false;

		/// <summary>
		/// The Data passed to the ByteView
		/// </summary>
		byte[] data;

		/// <summary>
		/// A Provider Registry that can be used
		/// </summary>
		Interfaces.IProviderRegistry provider;


		/// <summary>
		/// Called when the USer selects a Cell in the Grid
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event Attributes</param>
		protected void CellGotFocusEventHandler(object sender, SourceGrid2.CellGotFocusEventArgs e)
		{			
			if (e==null) e = cellcache;
			if (e==null) return;

			triggeredchange = true;
			offset = (uint)(e.Position.Row * grid.ColumnsCount + e.Position.Column);
			try 
			{
				this.cbmarked.Checked = (e.Cell.VisualModel == this.markedcell);
				if (rbhex.Checked) 
				{	
					tboffset.Text = "0x"+Helper.HexString(offset);
					string s = e.Cell.ToString();
					if (IsHexGrid) tbbyte.Text = "0x"+Helper.HexString(Convert.ToByte(e.Cell.ToString(), 16));
					else tbbyte.Text = "0x"+Helper.HexString(Convert.ToByte(e.Cell.ToString()));
					tbword.Text = "0x"+Helper.HexString((ushort)GetFromGrid(e.Position, 2));
					tbuword.Text = "0x"+Helper.HexString((ushort)GetFromGrid(e.Position, 2));
					tbint.Text = "0x"+Helper.HexString((uint)GetFromGrid(e.Position, 4));
					tbuint.Text = "0x"+Helper.HexString((uint)GetFromGrid(e.Position, 4));
				} 
				else 
				{
					tboffset.Text = (offset).ToString();
					if (IsHexGrid) tbbyte.Text = Convert.ToByte(e.Cell.ToString(), 16).ToString();
					else tbbyte.Text = Convert.ToByte(e.Cell.ToString()).ToString();
					tbword.Text = ((short)GetFromGrid(e.Position, 2)).ToString();
					tbuword.Text = ((ushort)GetFromGrid(e.Position, 2)).ToString();
					tbint.Text = ((int)GetFromGrid(e.Position, 4)).ToString();
					tbuint.Text = ((uint)GetFromGrid(e.Position, 4)).ToString();
				}
			} 
			catch (Exception) {}

			tbsimname.Text = "";
			tbsimnamei.Text = "";
			if (provider!=null) 
			{
				Interfaces.Wrapper.ISDesc sdesc = provider.SimDescriptionProvider.FindSim((uint)GetFromGrid(e.Position, 4));					
				if (sdesc != null) tbsimname.Text = sdesc.SimName+" "+sdesc.SimFamilyName;
				
				sdesc = provider.SimDescriptionProvider.FindSim((ushort)GetFromGrid(e.Position, 2));					
				if (sdesc != null) tbsimnamei.Text = sdesc.SimName+" "+sdesc.SimFamilyName;
			}
			SetInputEnabled(true, null);
			triggeredchange = false;


			cellcache = e;
		}

		/// <summary>
		/// True if the Grid displays Hex Values
		/// </summary>
		protected bool IsHexGrid
		{
			get 
			{
				if (grid.Tag==null) return rbhex.Checked;
				return (bool)grid.Tag;
			}
		}

		/// <summary>
		/// Move Selection to the passed Offset
		/// </summary>
		/// <param name="offset">the new Offset</param>
		protected void GoTo(uint offset)
		{
			long row = (offset / grid.ColumnsCount);
			long col = (offset % grid.ColumnsCount);
			SourceGrid2.Position pos = new SourceGrid2.Position((int)row, (int)col);

			grid.Selection.Clear();
			grid.Selection.Add(pos);
			try 
			{
				cellcache = new SourceGrid2.CellGotFocusEventArgs(
					pos,
					grid[(int)row, (int)col],
					new SourceGrid2.Position(0, 0)
					);
				CellGotFocusEventHandler(null, null);
			} 
			catch (Exception) 
			{
				cellcache = null;
				tboffset.Text = "";
				SetInputEnabled(false, null);
			}
		}

		/// <summary>
		/// Returns Byte nr from the Value val
		/// </summary>
		/// <param name="nr">Number of the Byte you want to retrive</param>
		/// <param name="val">The value you want to read the byte from</param>
		/// <returns>A byte Value</returns>
		protected byte GetByte(byte nr, long val) 
		{
			nr = (byte)(nr*8);
			val = val >> nr;
			val = val & (0x00000000000000ff);

			return (byte)val;
		}

		/// <summary>
		/// Returns a Value stored in a sequence of count Cells
		/// </summary>
		/// <param name="pos">Starting Position</param>
		/// <param name="count">Number of bytes to process</param>
		/// <returns>The generated value</returns>
		private long GetFromGrid(SourceGrid2.Position pos, byte count)		
		{
			int row = pos.Row;
			int col = pos.Column;
			try 
			{
				long v = 0;
				
				for (byte i=0; i<count; i++) 
				{
					//next Row
					if (col>=grid.ColumnsCount) 
					{
						row++;
						col=0;
					}

					byte b = 0;
					if (IsHexGrid) b = Convert.ToByte(grid[row, col].ToString(), 16);
					else b = Convert.ToByte(grid[row, col].ToString());

					v += (b << (i*8));
					col++;
				}
				
				return v;
			} 
			catch (Exception) {}
			return 0;
		}

		/// <summary>
		/// Stores a Value into The Grid
		/// </summary>
		/// <param name="pos">Starting Position</param>
		/// <param name="val">The new Value</param>
		/// <param name="count">Number of bytes to process</param>
		/// <returns>The generated value</returns>
		private void SetToGrid(SourceGrid2.Position pos, long val, byte count)		
		{
			int row = pos.Row;
			int col = pos.Column;
			try 
			{
				for (byte i=0; i<count; i++) 
				{
					//next Row
					if (col>=grid.ColumnsCount) 
					{
						row++;
						col=0;
					}

					grid[row, col] = MakeCell(GetByte(i, val));
					col++;
				}				
			} 
			catch (Exception) {}			
		}

		/// <summary>
		/// Fills the Grid with the stored data
		/// </summary>
		/// <param name="columns">Number of Columsn to display</param>
		/// <param name="hex">true, if you want to display numbers as Hex</param>
		protected void DisplayGrid(byte columns, bool hex) 
		{
			grid.Tag = hex;
			grid.Visible = false;
			grid.RowsCount = 0;
			grid.ColumnsCount = columns;
			//grid.RowsCount = (data.Length / grid.ColumnsCount) + 1;

			int row = -1;
			int col = 0;
			for (int i=0; i<data.Length; i++) 
			{
				if ((i%grid.ColumnsCount) == 0) 
				{
					row++;
					col = 0;
					grid.RowsCount ++;
				}

				grid[row, col] = MakeCell(data[i]);
				col++;
			}

			GridResized(grid, null);
			grid.Visible = true;
		}

		/// <summary>
		/// Stores the Grid Data in the byte Array
		/// </summary>
		protected void StoreGrid()
		{
			int row = -1;
			int col = 0;			
			for (int i=0; i<data.Length; i++) 
			{
				if ((i%grid.ColumnsCount) == 0) 
				{
					row++;
					col = 0;
					grid.RowsCount ++;
				}

				if (IsHexGrid) data[i] = Convert.ToByte(grid[row, col].ToString(), 16);
			    else data[i] = Convert.ToByte(grid[row, col].ToString());
				col++;
			}
		}

		Form f;
		IPackedFileDescriptor pfd;
		/// <summary>
		/// Used to startup the Form
		/// </summary>
		/// <param name="pfd">Discriptor for the File you want to Display</param>
		/// <param name="package">The Package that stores the File</param>
		public void Execute(IPackedFileDescriptor pfd, IPackageFile package, Form f, Interfaces.IProviderRegistry provider) 
		{
			this.f = f;
			this.pfd = pfd;
			this.provider = null;
#if DEBUG
			this.rbhex.Checked = true;
#endif
			this.provider = provider;
			f.Cursor = Cursors.WaitCursor;
			data = package.Read(pfd).UncompressedData;

			DisplayGrid(16, this.rbhex.Checked);
			tbcols.Text = grid.ColumnsCount.ToString();
			
			f.Cursor = Cursors.Default;
			this.Show();

			
		}

		/// <summary>
		/// Called when the Grid is Resized
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Arguments</param>
		private void GridResized(object sender, System.EventArgs e)
		{
			int cwidth = (((SourceGrid2.Grid)sender).ClientRectangle.Width / ((SourceGrid2.Grid)sender).ColumnsCount) - 2;
			for (int i=0; i<((SourceGrid2.Grid)sender).ColumnsCount; i++) 
			{
				((SourceGrid2.Grid)sender).Columns[i].Width = cwidth;
			}
		}

		/// <summary>
		/// Called when the Display Mode Selection was changed
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Arguments</param>
		private void DisplayModeChanged(object sender, System.EventArgs e)
		{
			if (this.provider==null) return;
			StoreGrid();
			DisplayGrid((byte)grid.ColumnsCount, this.rbhex.Checked);
			CellGotFocusEventHandler(null, null);
		}

		/// <summary>
		/// Creates a New Cell containing the given Value
		/// </summary>
		/// <param name="val">Value to put in the Cell</param>
		protected SourceGrid2.Cells.ICell MakeCell(byte val)
		{
			string val_str = "";
			if (IsHexGrid) val_str = Helper.HexString(val);
			else val_str = val.ToString();
			
			SourceGrid2.Cells.ICell cell = new SourceGrid2.Cells.Real.Cell(val_str);
			if (val==0) cell.VisualModel = zerocell;

			return cell;
		}

		/// <summary>
		/// Sets the Enabled Value of all Input Boxes to the passed value
		/// </summary>
		/// <param name="enable">true if Enabled</param>
		/// <param name="tb">A TextBox you want to exclude</param>
		protected void SetInputEnabled(bool enable, TextBox tb) 
		{
			if (tbbyte != tb) tbbyte.Enabled = enable;
			if (tbword != tb) tbword.Enabled = enable;
			if (tbuword != tb) tbuword.Enabled = enable;
			if (tbint != tb) tbint.Enabled = enable;
			if (tbuint != tb) tbuint.Enabled = enable;
		}

		#region Byte Values		
		/// <summary>
		/// Converts a String to a Byte Value
		/// </summary>
		/// <param name="t">The String</param>
		/// <returns>The Value</returns>
		private byte SaveConvertToByte(string t) 
		{
			try 
			{
				byte v;
				if (t.Trim()=="") v=0;
				else if (rbdec.Checked) v = Convert.ToByte(t);
				else v = Convert.ToByte(t, 16);

				return v;
			} 
			catch (Exception) {}

			return 0;
		}

		/// <summary>
		/// Change the Cell Content according to the Byte Value
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ByteTextChanged(object sender, System.EventArgs e)
		{
			if (triggeredchange) return;
			if (cellcache == null) return;
			TextBox tb = (TextBox)sender;
			
			grid[cellcache.Position.Row, cellcache.Position.Column] = MakeCell(SaveConvertToByte(tb.Text));
			cellcache.Cell = grid[cellcache.Position.Row, cellcache.Position.Column];
			grid.Selection.Add(cellcache.Position);

			SetInputEnabled(false, tbbyte);
		}

		/// <summary>
		/// Move Offset by the Number of Bytes displayed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GoByteClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			byte v = SaveConvertToByte(tbbyte.Text);			
			GoTo(offset + v + 0);
		}
		#endregion 

		#region Word Values
		/// <summary>
		/// Converts a String to a Value
		/// </summary>
		/// <param name="t">The String</param>
		/// <returns>The Value</returns>
		private short SaveConvertToShort(string t) 
		{
			try 
			{
				short v;
				if (t.Trim()=="") v=0;
				else if (rbdec.Checked) v = Convert.ToInt16(t);
				else v = Convert.ToInt16(t, 16);
				return v;
			} 
			catch (Exception) {}

			return 0;
		}

		/// <summary>
		/// Change the Cell Content according to the Value
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ShortTextChanged(object sender, System.EventArgs e)
		{
			if (triggeredchange) return;
			if (cellcache == null) return;
			TextBox tb = (TextBox)sender;
			
			short v = SaveConvertToShort(tb.Text);

			grid.Selection.Clear();	
			SetToGrid(cellcache.Position, v, 2);			
			cellcache.Cell = grid[cellcache.Position.Row, cellcache.Position.Column];			
			grid.Selection.Add(cellcache.Position);

			SetInputEnabled(false, tbword);
		}

		/// <summary>
		/// Move Offset by the Number displayed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GoShortClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			short v = SaveConvertToShort(tbbyte.Text);			
			GoTo((uint)(offset + v + 1));
		}
		#endregion 

		#region uWord Values
		/// <summary>
		/// Converts a String to a Value
		/// </summary>
		/// <param name="t">The String</param>
		/// <returns>The Value</returns>
		private ushort SaveConvertToUShort(string t) 
		{
			try 
			{
				ushort v;
				if (t.Trim()=="") v=0;
				else if (rbdec.Checked) v = Convert.ToUInt16(t);
				else v = Convert.ToUInt16(t, 16);
				return v;
			} 
			catch (Exception) {}

			return 0;
		}

		/// <summary>
		/// Change the Cell Content according to the Value
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UShortTextChanged(object sender, System.EventArgs e)
		{
			if (triggeredchange) return;
			if (cellcache == null) return;
			TextBox tb = (TextBox)sender;
			
			ushort v = SaveConvertToUShort(tb.Text);

			grid.Selection.Clear();	
			SetToGrid(cellcache.Position, v, 2);			
			cellcache.Cell = grid[cellcache.Position.Row, cellcache.Position.Column];			
			grid.Selection.Add(cellcache.Position);

			SetInputEnabled(false, tbuword);
		}

		/// <summary>
		/// Move Offset by the Number displayed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GoUShortClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			ushort v = SaveConvertToUShort(tbbyte.Text);			
			GoTo(offset + v + 1);
		}
		#endregion 

		#region DWord Values
		

		/// <summary>
		/// Converts a String to a Value
		/// </summary>
		/// <param name="t">The String</param>
		/// <returns>The Value</returns>
		private int SaveConvertToInt(string t) 
		{
			try 
			{
				int v;
				if (t.Trim()=="") v=0;
				else if (rbdec.Checked) v = Convert.ToInt32(t);
				else v = Convert.ToInt32(t, 16);
				return v;
			} 
			catch (Exception) {}

			return 0;
		}

		/// <summary>
		/// Change the Cell Content according to the Value
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void IntTextChanged(object sender, System.EventArgs e)
		{
			if (triggeredchange) return;
			if (cellcache == null) return;
			TextBox tb = (TextBox)sender;
			
			int v = SaveConvertToInt(tb.Text);

			grid.Selection.Clear();	
			SetToGrid(cellcache.Position, v, 4);			
			cellcache.Cell = grid[cellcache.Position.Row, cellcache.Position.Column];			
			grid.Selection.Add(cellcache.Position);	
		
			SetInputEnabled(false, tbint);			
		}

		/// <summary>
		/// Move Offset by the Number displayed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GoIntClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			int v = SaveConvertToInt(tbbyte.Text);			
			GoTo((uint)(offset + v + 3));
		}
		#endregion 

		#region uDWord Values
		/// <summary>
		/// Converts a String to a Value
		/// </summary>
		/// <param name="t">The String</param>
		/// <returns>The Value</returns>
		private uint SaveConvertToUInt(string t) 
		{
			try 
			{
				uint v;
				if (t.Trim()=="") v=0;
				else if (rbdec.Checked) v = Convert.ToUInt32(t);
				else v = Convert.ToUInt32(t, 16);
				return v;
			} 
			catch (Exception) {}

			return 0;
		}

		/// <summary>
		/// Change the Cell Content according to the Value
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UIntTextChanged(object sender, System.EventArgs e)
		{
			if (triggeredchange) return;
			if (cellcache == null) return;
			TextBox tb = (TextBox)sender;
			
			uint v = SaveConvertToUInt(tb.Text);

			grid.Selection.Clear();	
			SetToGrid(cellcache.Position, v, 4);			
			cellcache.Cell = grid[cellcache.Position.Row, cellcache.Position.Column];			
			grid.Selection.Add(cellcache.Position);

			SetInputEnabled(false, tbuint);
		}

		/// <summary>
		/// Move Offset by the Number  displayed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GoUIntClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			uint v = SaveConvertToUInt(tbbyte.Text);			
			GoTo(offset + v + 3);
		}
		#endregion 

		private void GoOffsetClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			uint v = SaveConvertToUInt(tboffset.Text);
			GoTo((uint)(v));
		}

		private void CommitClick(object sender, System.EventArgs e)
		{
			ok = true;
			Close();
		}

		private void SetColumns(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			try 
			{
				byte b = 1;
				if (tbcols.Text.Trim()!="") b = Math.Max((byte)1, Convert.ToByte(tbcols.Text));

				if (b!=grid.ColumnsCount) 
				{
					StoreGrid();
					DisplayGrid(b, this.rbhex.Checked);
				}
			} 
			catch ( Exception) 
			{
				tbcols.Text = grid.ColumnsCount.ToString();
			}
			finally 
			{
				this.Cursor = Cursors.Default;
			}
		}

		protected void HighlightCells(SourceGrid2.Position pos, byte length, SourceGrid2.VisualModels.IVisualModel model) 
		{
			int c = pos.Column;
			int r = pos.Row;
			for (byte i=0; i<length; i++) 
			{
				if (c>=grid.ColumnsCount) 
				{
					c = 0;
					r++;
				}

				grid[r, c++].VisualModel = model;
			}
		}

		private void ScanForSimID(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			for (int r=0; r<grid.RowsCount; r++)
			{
				try 
				{
					for (int c=0; c<grid.ColumnsCount; c++) 
					{
						SourceGrid2.Position p = new SourceGrid2.Position(r, c);
						uint nr = (uint)GetFromGrid(p, 4);

						Interfaces.IAlias a = provider.SimNameProvider.FindName(nr);
						if (a.Name != Localization.Manager.GetString("unknown")) 
						{
							HighlightCells(new SourceGrid2.Position(r, c), 4, simidcell);
						}
					}
				} 
				catch (Exception) {}
			}
			this.Cursor = Cursors.Default;
		}

		private void HighlightMemories(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			for (int r=0; r<grid.RowsCount; r++)
			{
				try 
				{
					for (int c=0; c<grid.ColumnsCount; c++) 
					{
						SourceGrid2.Position p = new SourceGrid2.Position(r, c);
						uint nr = (uint)GetFromGrid(p, 4);

						Interfaces.IAlias a = provider.OpcodeProvider.FindMemory(nr);
						if (a.Name != Localization.Manager.GetString("unknown")) 
						{
							HighlightCells(new SourceGrid2.Position(r, c), 4, memidcell);
						}
					}
				} 
				catch (Exception) {}
			}
			this.Cursor = Cursors.Default;
		}

		private void MarkCell(object sender, System.EventArgs e)
		{
			if (triggeredchange) return;
			if (this.cbmarked.Checked)
				grid[cellcache.Position.Row, cellcache.Position.Column].VisualModel = this.markedcell;
			else 
			{
				grid[cellcache.Position.Row, cellcache.Position.Column] = MakeCell(SaveConvertToByte(tbbyte.Text));
			}
		}

		private void ClosingForm(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (ok) 
			{
				f.Cursor = Cursors.WaitCursor;
				StoreGrid();

				pfd.UserData = data;
				f.Cursor = Cursors.Default;
			}
		}
	}
}
