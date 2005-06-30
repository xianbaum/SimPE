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
using SimPe.Interfaces.Files;
using SimPe.Interfaces.Wrapper;
using SimPe.Data;
using SimPe.PackedFiles.Wrapper;
using SimPe.Interfaces.Plugin;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for BconForm.
	/// </summary>
	public class OldStrForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form elements

		private System.Windows.Forms.ComboBox cblanguage;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox gbstr;
		private System.Windows.Forms.LinkLabel lldelall;
		private System.Windows.Forms.LinkLabel lldelete;
		private System.Windows.Forms.LinkLabel llchangeall;
		private System.Windows.Forms.LinkLabel lladdall;
		private System.Windows.Forms.LinkLabel lladd;
		private System.Windows.Forms.RichTextBox rtbdesc;
		private System.Windows.Forms.RichTextBox rtbvalue;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ListBox lbtexts;
		private System.Windows.Forms.TextBox tbformat;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label lbstr;
		private System.Windows.Forms.Label banner;
		private System.Windows.Forms.LinkLabel llcreate;
		private System.Windows.Forms.Panel strPanel;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.Button btnCopyAll;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button btnEnglish;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Button btnClearStr;
		private System.Windows.Forms.Button btnSetLike;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox cbLangFrom;
		private System.Windows.Forms.Button btnAllLangs;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button btnDelLang;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public OldStrForm()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();

#if DEBUG
			this.llcreate.Visible = true;
#else
			this.llcreate.Visible = false;
#endif
#if DEBUG
#else
			Control[] ac = { btnDelLang ,label12 ,label5 ,btnUp ,btnDown ,lldelete ,lladd };
			foreach (Control c in ac)
				c.Visible = false;
#endif
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


		#region OldStrForm
		private Str wrapper;
		private void WrapperChanged(object sender, System.EventArgs e)
		{
			this.btnCommit.Enabled = true;
		}


		private void doLanguages()
		{
			cblanguage.Items.Clear();
			cbLangFrom.Items.Clear();
			cbLangFrom.Sorted = cblanguage.Sorted = false;
			foreach (byte l in wrapper.Languages)
			{
				cblanguage.Items.Add(new Language(l));
				if (wrapper.nrStrItems(l) > 0)
					cbLangFrom.Items.Add(new Language(l));
			}
			cbLangFrom.Sorted = cblanguage.Sorted = true;
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
				return strPanel;
			}
		}

		/// <summary>
		/// Is called by SimPe (through the Wrapper) when the Panel is going to be displayed, so
		/// you should updatet the Data displayed by the Panel with the Attributes stored in the
		/// passed Wrapper.
		/// </summary>
		/// <remarks>this.Tag is used to let TextChanged event handlers know the change is being
		/// made internally rather than by the users.</remarks>
		/// <param name="wrp">The Attributes of this Wrapper have to be displayed</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (Str)wrp;

			wrapper.WrapperChanged += new System.EventHandler(this.WrapperChanged);
			this.btnCommit.Enabled = wrapper.Changed;

			Tag = true;

			lbstr.Text = wrapper.FileName;
			tbformat.Text = "0x"+Helper.HexString((ushort)wrapper.Format);

			lbtexts.Items.Clear();			

			llchangeall.Enabled = false;

			rtbvalue.Text = "";
			rtbdesc.Text = "";
			gbstr.Text = "";

			cblanguage.SelectedIndex = -1;
			lbtexts.SelectedIndex = -1;

			doLanguages();

			Tag = null;

			if (cblanguage.Items.Count > 0)
				cblanguage.SelectedIndex = 0;
			if (lbtexts.Items.Count > 0)
				lbtexts.SelectedIndex = 0;
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(OldStrForm));
			this.strPanel = new System.Windows.Forms.Panel();
			this.cbLangFrom = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.btnAllLangs = new System.Windows.Forms.Button();
			this.btnCopyAll = new System.Windows.Forms.Button();
			this.btnCommit = new System.Windows.Forms.Button();
			this.cblanguage = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.gbstr = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btnUp = new System.Windows.Forms.Button();
			this.lldelall = new System.Windows.Forms.LinkLabel();
			this.lldelete = new System.Windows.Forms.LinkLabel();
			this.llchangeall = new System.Windows.Forms.LinkLabel();
			this.lladdall = new System.Windows.Forms.LinkLabel();
			this.lladd = new System.Windows.Forms.LinkLabel();
			this.rtbdesc = new System.Windows.Forms.RichTextBox();
			this.rtbvalue = new System.Windows.Forms.RichTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnDown = new System.Windows.Forms.Button();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lbtexts = new System.Windows.Forms.ListBox();
			this.tbformat = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lbstr = new System.Windows.Forms.Label();
			this.banner = new System.Windows.Forms.Label();
			this.llcreate = new System.Windows.Forms.LinkLabel();
			this.btnEnglish = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.btnClearStr = new System.Windows.Forms.Button();
			this.btnSetLike = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.btnDelLang = new System.Windows.Forms.Button();
			this.strPanel.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.gbstr.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// strPanel
			// 
			this.strPanel.AccessibleDescription = resources.GetString("strPanel.AccessibleDescription");
			this.strPanel.AccessibleName = resources.GetString("strPanel.AccessibleName");
			this.strPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("strPanel.Anchor")));
			this.strPanel.AutoScroll = ((bool)(resources.GetObject("strPanel.AutoScroll")));
			this.strPanel.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("strPanel.AutoScrollMargin")));
			this.strPanel.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("strPanel.AutoScrollMinSize")));
			this.strPanel.BackColor = System.Drawing.SystemColors.Control;
			this.strPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("strPanel.BackgroundImage")));
			this.strPanel.Controls.Add(this.cbLangFrom);
			this.strPanel.Controls.Add(this.label6);
			this.strPanel.Controls.Add(this.btnAllLangs);
			this.strPanel.Controls.Add(this.btnCopyAll);
			this.strPanel.Controls.Add(this.btnCommit);
			this.strPanel.Controls.Add(this.cblanguage);
			this.strPanel.Controls.Add(this.label4);
			this.strPanel.Controls.Add(this.groupBox1);
			this.strPanel.Controls.Add(this.tbformat);
			this.strPanel.Controls.Add(this.label1);
			this.strPanel.Controls.Add(this.panel2);
			this.strPanel.Controls.Add(this.llcreate);
			this.strPanel.Controls.Add(this.btnEnglish);
			this.strPanel.Controls.Add(this.label8);
			this.strPanel.Controls.Add(this.label9);
			this.strPanel.Controls.Add(this.label10);
			this.strPanel.Controls.Add(this.label11);
			this.strPanel.Controls.Add(this.btnClearStr);
			this.strPanel.Controls.Add(this.btnSetLike);
			this.strPanel.Controls.Add(this.label7);
			this.strPanel.Controls.Add(this.label12);
			this.strPanel.Controls.Add(this.btnDelLang);
			this.strPanel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("strPanel.Dock")));
			this.strPanel.Enabled = ((bool)(resources.GetObject("strPanel.Enabled")));
			this.strPanel.Font = ((System.Drawing.Font)(resources.GetObject("strPanel.Font")));
			this.strPanel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("strPanel.ImeMode")));
			this.strPanel.Location = ((System.Drawing.Point)(resources.GetObject("strPanel.Location")));
			this.strPanel.Name = "strPanel";
			this.strPanel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("strPanel.RightToLeft")));
			this.strPanel.Size = ((System.Drawing.Size)(resources.GetObject("strPanel.Size")));
			this.strPanel.TabIndex = ((int)(resources.GetObject("strPanel.TabIndex")));
			this.strPanel.Text = resources.GetString("strPanel.Text");
			this.strPanel.Visible = ((bool)(resources.GetObject("strPanel.Visible")));
			// 
			// cbLangFrom
			// 
			this.cbLangFrom.AccessibleDescription = resources.GetString("cbLangFrom.AccessibleDescription");
			this.cbLangFrom.AccessibleName = resources.GetString("cbLangFrom.AccessibleName");
			this.cbLangFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbLangFrom.Anchor")));
			this.cbLangFrom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbLangFrom.BackgroundImage")));
			this.cbLangFrom.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbLangFrom.Dock")));
			this.cbLangFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbLangFrom.Enabled = ((bool)(resources.GetObject("cbLangFrom.Enabled")));
			this.cbLangFrom.Font = ((System.Drawing.Font)(resources.GetObject("cbLangFrom.Font")));
			this.cbLangFrom.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbLangFrom.ImeMode")));
			this.cbLangFrom.IntegralHeight = ((bool)(resources.GetObject("cbLangFrom.IntegralHeight")));
			this.cbLangFrom.ItemHeight = ((int)(resources.GetObject("cbLangFrom.ItemHeight")));
			this.cbLangFrom.Location = ((System.Drawing.Point)(resources.GetObject("cbLangFrom.Location")));
			this.cbLangFrom.MaxDropDownItems = ((int)(resources.GetObject("cbLangFrom.MaxDropDownItems")));
			this.cbLangFrom.MaxLength = ((int)(resources.GetObject("cbLangFrom.MaxLength")));
			this.cbLangFrom.Name = "cbLangFrom";
			this.cbLangFrom.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbLangFrom.RightToLeft")));
			this.cbLangFrom.Size = ((System.Drawing.Size)(resources.GetObject("cbLangFrom.Size")));
			this.cbLangFrom.TabIndex = ((int)(resources.GetObject("cbLangFrom.TabIndex")));
			this.cbLangFrom.Text = resources.GetString("cbLangFrom.Text");
			this.cbLangFrom.Visible = ((bool)(resources.GetObject("cbLangFrom.Visible")));
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
			// btnAllLangs
			// 
			this.btnAllLangs.AccessibleDescription = resources.GetString("btnAllLangs.AccessibleDescription");
			this.btnAllLangs.AccessibleName = resources.GetString("btnAllLangs.AccessibleName");
			this.btnAllLangs.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAllLangs.Anchor")));
			this.btnAllLangs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAllLangs.BackgroundImage")));
			this.btnAllLangs.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAllLangs.Dock")));
			this.btnAllLangs.Enabled = ((bool)(resources.GetObject("btnAllLangs.Enabled")));
			this.btnAllLangs.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAllLangs.FlatStyle")));
			this.btnAllLangs.Font = ((System.Drawing.Font)(resources.GetObject("btnAllLangs.Font")));
			this.btnAllLangs.Image = ((System.Drawing.Image)(resources.GetObject("btnAllLangs.Image")));
			this.btnAllLangs.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAllLangs.ImageAlign")));
			this.btnAllLangs.ImageIndex = ((int)(resources.GetObject("btnAllLangs.ImageIndex")));
			this.btnAllLangs.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAllLangs.ImeMode")));
			this.btnAllLangs.Location = ((System.Drawing.Point)(resources.GetObject("btnAllLangs.Location")));
			this.btnAllLangs.Name = "btnAllLangs";
			this.btnAllLangs.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAllLangs.RightToLeft")));
			this.btnAllLangs.Size = ((System.Drawing.Size)(resources.GetObject("btnAllLangs.Size")));
			this.btnAllLangs.TabIndex = ((int)(resources.GetObject("btnAllLangs.TabIndex")));
			this.btnAllLangs.Text = resources.GetString("btnAllLangs.Text");
			this.btnAllLangs.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAllLangs.TextAlign")));
			this.btnAllLangs.Visible = ((bool)(resources.GetObject("btnAllLangs.Visible")));
			this.btnAllLangs.Click += new System.EventHandler(this.btnAllLangs_Click);
			// 
			// btnCopyAll
			// 
			this.btnCopyAll.AccessibleDescription = resources.GetString("btnCopyAll.AccessibleDescription");
			this.btnCopyAll.AccessibleName = resources.GetString("btnCopyAll.AccessibleName");
			this.btnCopyAll.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCopyAll.Anchor")));
			this.btnCopyAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCopyAll.BackgroundImage")));
			this.btnCopyAll.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCopyAll.Dock")));
			this.btnCopyAll.Enabled = ((bool)(resources.GetObject("btnCopyAll.Enabled")));
			this.btnCopyAll.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCopyAll.FlatStyle")));
			this.btnCopyAll.Font = ((System.Drawing.Font)(resources.GetObject("btnCopyAll.Font")));
			this.btnCopyAll.Image = ((System.Drawing.Image)(resources.GetObject("btnCopyAll.Image")));
			this.btnCopyAll.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCopyAll.ImageAlign")));
			this.btnCopyAll.ImageIndex = ((int)(resources.GetObject("btnCopyAll.ImageIndex")));
			this.btnCopyAll.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCopyAll.ImeMode")));
			this.btnCopyAll.Location = ((System.Drawing.Point)(resources.GetObject("btnCopyAll.Location")));
			this.btnCopyAll.Name = "btnCopyAll";
			this.btnCopyAll.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCopyAll.RightToLeft")));
			this.btnCopyAll.Size = ((System.Drawing.Size)(resources.GetObject("btnCopyAll.Size")));
			this.btnCopyAll.TabIndex = ((int)(resources.GetObject("btnCopyAll.TabIndex")));
			this.btnCopyAll.Text = resources.GetString("btnCopyAll.Text");
			this.btnCopyAll.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCopyAll.TextAlign")));
			this.btnCopyAll.Visible = ((bool)(resources.GetObject("btnCopyAll.Visible")));
			this.btnCopyAll.Click += new System.EventHandler(this.btnCopyAll_Click);
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
			// cblanguage
			// 
			this.cblanguage.AccessibleDescription = resources.GetString("cblanguage.AccessibleDescription");
			this.cblanguage.AccessibleName = resources.GetString("cblanguage.AccessibleName");
			this.cblanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cblanguage.Anchor")));
			this.cblanguage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cblanguage.BackgroundImage")));
			this.cblanguage.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cblanguage.Dock")));
			this.cblanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cblanguage.Enabled = ((bool)(resources.GetObject("cblanguage.Enabled")));
			this.cblanguage.Font = ((System.Drawing.Font)(resources.GetObject("cblanguage.Font")));
			this.cblanguage.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cblanguage.ImeMode")));
			this.cblanguage.IntegralHeight = ((bool)(resources.GetObject("cblanguage.IntegralHeight")));
			this.cblanguage.ItemHeight = ((int)(resources.GetObject("cblanguage.ItemHeight")));
			this.cblanguage.Location = ((System.Drawing.Point)(resources.GetObject("cblanguage.Location")));
			this.cblanguage.MaxDropDownItems = ((int)(resources.GetObject("cblanguage.MaxDropDownItems")));
			this.cblanguage.MaxLength = ((int)(resources.GetObject("cblanguage.MaxLength")));
			this.cblanguage.Name = "cblanguage";
			this.cblanguage.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cblanguage.RightToLeft")));
			this.cblanguage.Size = ((System.Drawing.Size)(resources.GetObject("cblanguage.Size")));
			this.cblanguage.TabIndex = ((int)(resources.GetObject("cblanguage.TabIndex")));
			this.cblanguage.Text = resources.GetString("cblanguage.Text");
			this.cblanguage.Visible = ((bool)(resources.GetObject("cblanguage.Visible")));
			this.cblanguage.SelectedIndexChanged += new System.EventHandler(this.LanguageChanged);
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
			// groupBox1
			// 
			this.groupBox1.AccessibleDescription = resources.GetString("groupBox1.AccessibleDescription");
			this.groupBox1.AccessibleName = resources.GetString("groupBox1.AccessibleName");
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox1.Anchor")));
			this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
			this.groupBox1.Controls.Add(this.gbstr);
			this.groupBox1.Controls.Add(this.splitter1);
			this.groupBox1.Controls.Add(this.panel1);
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
			// gbstr
			// 
			this.gbstr.AccessibleDescription = resources.GetString("gbstr.AccessibleDescription");
			this.gbstr.AccessibleName = resources.GetString("gbstr.AccessibleName");
			this.gbstr.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gbstr.Anchor")));
			this.gbstr.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gbstr.BackgroundImage")));
			this.gbstr.Controls.Add(this.label5);
			this.gbstr.Controls.Add(this.btnUp);
			this.gbstr.Controls.Add(this.lldelall);
			this.gbstr.Controls.Add(this.lldelete);
			this.gbstr.Controls.Add(this.llchangeall);
			this.gbstr.Controls.Add(this.lladdall);
			this.gbstr.Controls.Add(this.lladd);
			this.gbstr.Controls.Add(this.rtbdesc);
			this.gbstr.Controls.Add(this.rtbvalue);
			this.gbstr.Controls.Add(this.label3);
			this.gbstr.Controls.Add(this.label2);
			this.gbstr.Controls.Add(this.btnDown);
			this.gbstr.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("gbstr.Dock")));
			this.gbstr.Enabled = ((bool)(resources.GetObject("gbstr.Enabled")));
			this.gbstr.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.gbstr.Font = ((System.Drawing.Font)(resources.GetObject("gbstr.Font")));
			this.gbstr.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gbstr.ImeMode")));
			this.gbstr.Location = ((System.Drawing.Point)(resources.GetObject("gbstr.Location")));
			this.gbstr.Name = "gbstr";
			this.gbstr.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("gbstr.RightToLeft")));
			this.gbstr.Size = ((System.Drawing.Size)(resources.GetObject("gbstr.Size")));
			this.gbstr.TabIndex = ((int)(resources.GetObject("gbstr.TabIndex")));
			this.gbstr.TabStop = false;
			this.gbstr.Text = resources.GetString("gbstr.Text");
			this.gbstr.Visible = ((bool)(resources.GetObject("gbstr.Visible")));
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
			// btnUp
			// 
			this.btnUp.AccessibleDescription = resources.GetString("btnUp.AccessibleDescription");
			this.btnUp.AccessibleName = resources.GetString("btnUp.AccessibleName");
			this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnUp.Anchor")));
			this.btnUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUp.BackgroundImage")));
			this.btnUp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnUp.Dock")));
			this.btnUp.Enabled = ((bool)(resources.GetObject("btnUp.Enabled")));
			this.btnUp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnUp.FlatStyle")));
			this.btnUp.Font = ((System.Drawing.Font)(resources.GetObject("btnUp.Font")));
			this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
			this.btnUp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnUp.ImageAlign")));
			this.btnUp.ImageIndex = ((int)(resources.GetObject("btnUp.ImageIndex")));
			this.btnUp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnUp.ImeMode")));
			this.btnUp.Location = ((System.Drawing.Point)(resources.GetObject("btnUp.Location")));
			this.btnUp.Name = "btnUp";
			this.btnUp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnUp.RightToLeft")));
			this.btnUp.Size = ((System.Drawing.Size)(resources.GetObject("btnUp.Size")));
			this.btnUp.TabIndex = ((int)(resources.GetObject("btnUp.TabIndex")));
			this.btnUp.Text = resources.GetString("btnUp.Text");
			this.btnUp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnUp.TextAlign")));
			this.btnUp.Visible = ((bool)(resources.GetObject("btnUp.Visible")));
			this.btnUp.Click += new System.EventHandler(this.btnUpDown_Click);
			// 
			// lldelall
			// 
			this.lldelall.AccessibleDescription = resources.GetString("lldelall.AccessibleDescription");
			this.lldelall.AccessibleName = resources.GetString("lldelall.AccessibleName");
			this.lldelall.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lldelall.Anchor")));
			this.lldelall.AutoSize = ((bool)(resources.GetObject("lldelall.AutoSize")));
			this.lldelall.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lldelall.Dock")));
			this.lldelall.Enabled = ((bool)(resources.GetObject("lldelall.Enabled")));
			this.lldelall.Font = ((System.Drawing.Font)(resources.GetObject("lldelall.Font")));
			this.lldelall.Image = ((System.Drawing.Image)(resources.GetObject("lldelall.Image")));
			this.lldelall.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lldelall.ImageAlign")));
			this.lldelall.ImageIndex = ((int)(resources.GetObject("lldelall.ImageIndex")));
			this.lldelall.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lldelall.ImeMode")));
			this.lldelall.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("lldelall.LinkArea")));
			this.lldelall.Location = ((System.Drawing.Point)(resources.GetObject("lldelall.Location")));
			this.lldelall.Name = "lldelall";
			this.lldelall.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lldelall.RightToLeft")));
			this.lldelall.Size = ((System.Drawing.Size)(resources.GetObject("lldelall.Size")));
			this.lldelall.TabIndex = ((int)(resources.GetObject("lldelall.TabIndex")));
			this.lldelall.TabStop = true;
			this.lldelall.Text = resources.GetString("lldelall.Text");
			this.lldelall.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lldelall.TextAlign")));
			this.lldelall.Visible = ((bool)(resources.GetObject("lldelall.Visible")));
			this.lldelall.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DelInAll);
			// 
			// lldelete
			// 
			this.lldelete.AccessibleDescription = resources.GetString("lldelete.AccessibleDescription");
			this.lldelete.AccessibleName = resources.GetString("lldelete.AccessibleName");
			this.lldelete.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lldelete.Anchor")));
			this.lldelete.AutoSize = ((bool)(resources.GetObject("lldelete.AutoSize")));
			this.lldelete.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lldelete.Dock")));
			this.lldelete.Enabled = ((bool)(resources.GetObject("lldelete.Enabled")));
			this.lldelete.Font = ((System.Drawing.Font)(resources.GetObject("lldelete.Font")));
			this.lldelete.Image = ((System.Drawing.Image)(resources.GetObject("lldelete.Image")));
			this.lldelete.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lldelete.ImageAlign")));
			this.lldelete.ImageIndex = ((int)(resources.GetObject("lldelete.ImageIndex")));
			this.lldelete.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lldelete.ImeMode")));
			this.lldelete.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("lldelete.LinkArea")));
			this.lldelete.Location = ((System.Drawing.Point)(resources.GetObject("lldelete.Location")));
			this.lldelete.Name = "lldelete";
			this.lldelete.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lldelete.RightToLeft")));
			this.lldelete.Size = ((System.Drawing.Size)(resources.GetObject("lldelete.Size")));
			this.lldelete.TabIndex = ((int)(resources.GetObject("lldelete.TabIndex")));
			this.lldelete.TabStop = true;
			this.lldelete.Text = resources.GetString("lldelete.Text");
			this.lldelete.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lldelete.TextAlign")));
			this.lldelete.Visible = ((bool)(resources.GetObject("lldelete.Visible")));
			this.lldelete.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.StrDelete);
			// 
			// llchangeall
			// 
			this.llchangeall.AccessibleDescription = resources.GetString("llchangeall.AccessibleDescription");
			this.llchangeall.AccessibleName = resources.GetString("llchangeall.AccessibleName");
			this.llchangeall.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llchangeall.Anchor")));
			this.llchangeall.AutoSize = ((bool)(resources.GetObject("llchangeall.AutoSize")));
			this.llchangeall.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llchangeall.Dock")));
			this.llchangeall.Enabled = ((bool)(resources.GetObject("llchangeall.Enabled")));
			this.llchangeall.Font = ((System.Drawing.Font)(resources.GetObject("llchangeall.Font")));
			this.llchangeall.Image = ((System.Drawing.Image)(resources.GetObject("llchangeall.Image")));
			this.llchangeall.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llchangeall.ImageAlign")));
			this.llchangeall.ImageIndex = ((int)(resources.GetObject("llchangeall.ImageIndex")));
			this.llchangeall.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llchangeall.ImeMode")));
			this.llchangeall.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llchangeall.LinkArea")));
			this.llchangeall.Location = ((System.Drawing.Point)(resources.GetObject("llchangeall.Location")));
			this.llchangeall.Name = "llchangeall";
			this.llchangeall.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llchangeall.RightToLeft")));
			this.llchangeall.Size = ((System.Drawing.Size)(resources.GetObject("llchangeall.Size")));
			this.llchangeall.TabIndex = ((int)(resources.GetObject("llchangeall.TabIndex")));
			this.llchangeall.TabStop = true;
			this.llchangeall.Text = resources.GetString("llchangeall.Text");
			this.llchangeall.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llchangeall.TextAlign")));
			this.llchangeall.Visible = ((bool)(resources.GetObject("llchangeall.Visible")));
			this.llchangeall.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChangeInAllLanguages);
			// 
			// lladdall
			// 
			this.lladdall.AccessibleDescription = resources.GetString("lladdall.AccessibleDescription");
			this.lladdall.AccessibleName = resources.GetString("lladdall.AccessibleName");
			this.lladdall.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lladdall.Anchor")));
			this.lladdall.AutoSize = ((bool)(resources.GetObject("lladdall.AutoSize")));
			this.lladdall.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lladdall.Dock")));
			this.lladdall.Enabled = ((bool)(resources.GetObject("lladdall.Enabled")));
			this.lladdall.Font = ((System.Drawing.Font)(resources.GetObject("lladdall.Font")));
			this.lladdall.Image = ((System.Drawing.Image)(resources.GetObject("lladdall.Image")));
			this.lladdall.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lladdall.ImageAlign")));
			this.lladdall.ImageIndex = ((int)(resources.GetObject("lladdall.ImageIndex")));
			this.lladdall.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lladdall.ImeMode")));
			this.lladdall.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("lladdall.LinkArea")));
			this.lladdall.Location = ((System.Drawing.Point)(resources.GetObject("lladdall.Location")));
			this.lladdall.Name = "lladdall";
			this.lladdall.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lladdall.RightToLeft")));
			this.lladdall.Size = ((System.Drawing.Size)(resources.GetObject("lladdall.Size")));
			this.lladdall.TabIndex = ((int)(resources.GetObject("lladdall.TabIndex")));
			this.lladdall.TabStop = true;
			this.lladdall.Text = resources.GetString("lladdall.Text");
			this.lladdall.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lladdall.TextAlign")));
			this.lladdall.Visible = ((bool)(resources.GetObject("lladdall.Visible")));
			this.lladdall.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AddToAll);
			// 
			// lladd
			// 
			this.lladd.AccessibleDescription = resources.GetString("lladd.AccessibleDescription");
			this.lladd.AccessibleName = resources.GetString("lladd.AccessibleName");
			this.lladd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lladd.Anchor")));
			this.lladd.AutoSize = ((bool)(resources.GetObject("lladd.AutoSize")));
			this.lladd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lladd.Dock")));
			this.lladd.Enabled = ((bool)(resources.GetObject("lladd.Enabled")));
			this.lladd.Font = ((System.Drawing.Font)(resources.GetObject("lladd.Font")));
			this.lladd.Image = ((System.Drawing.Image)(resources.GetObject("lladd.Image")));
			this.lladd.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lladd.ImageAlign")));
			this.lladd.ImageIndex = ((int)(resources.GetObject("lladd.ImageIndex")));
			this.lladd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lladd.ImeMode")));
			this.lladd.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("lladd.LinkArea")));
			this.lladd.Location = ((System.Drawing.Point)(resources.GetObject("lladd.Location")));
			this.lladd.Name = "lladd";
			this.lladd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lladd.RightToLeft")));
			this.lladd.Size = ((System.Drawing.Size)(resources.GetObject("lladd.Size")));
			this.lladd.TabIndex = ((int)(resources.GetObject("lladd.TabIndex")));
			this.lladd.TabStop = true;
			this.lladd.Text = resources.GetString("lladd.Text");
			this.lladd.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lladd.TextAlign")));
			this.lladd.Visible = ((bool)(resources.GetObject("lladd.Visible")));
			this.lladd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.StrAdd);
			// 
			// rtbdesc
			// 
			this.rtbdesc.AccessibleDescription = resources.GetString("rtbdesc.AccessibleDescription");
			this.rtbdesc.AccessibleName = resources.GetString("rtbdesc.AccessibleName");
			this.rtbdesc.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rtbdesc.Anchor")));
			this.rtbdesc.AutoSize = ((bool)(resources.GetObject("rtbdesc.AutoSize")));
			this.rtbdesc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rtbdesc.BackgroundImage")));
			this.rtbdesc.BulletIndent = ((int)(resources.GetObject("rtbdesc.BulletIndent")));
			this.rtbdesc.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rtbdesc.Dock")));
			this.rtbdesc.Enabled = ((bool)(resources.GetObject("rtbdesc.Enabled")));
			this.rtbdesc.Font = ((System.Drawing.Font)(resources.GetObject("rtbdesc.Font")));
			this.rtbdesc.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rtbdesc.ImeMode")));
			this.rtbdesc.Location = ((System.Drawing.Point)(resources.GetObject("rtbdesc.Location")));
			this.rtbdesc.MaxLength = ((int)(resources.GetObject("rtbdesc.MaxLength")));
			this.rtbdesc.Multiline = ((bool)(resources.GetObject("rtbdesc.Multiline")));
			this.rtbdesc.Name = "rtbdesc";
			this.rtbdesc.RightMargin = ((int)(resources.GetObject("rtbdesc.RightMargin")));
			this.rtbdesc.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rtbdesc.RightToLeft")));
			this.rtbdesc.ScrollBars = ((System.Windows.Forms.RichTextBoxScrollBars)(resources.GetObject("rtbdesc.ScrollBars")));
			this.rtbdesc.Size = ((System.Drawing.Size)(resources.GetObject("rtbdesc.Size")));
			this.rtbdesc.TabIndex = ((int)(resources.GetObject("rtbdesc.TabIndex")));
			this.rtbdesc.Text = resources.GetString("rtbdesc.Text");
			this.rtbdesc.Visible = ((bool)(resources.GetObject("rtbdesc.Visible")));
			this.rtbdesc.WordWrap = ((bool)(resources.GetObject("rtbdesc.WordWrap")));
			this.rtbdesc.ZoomFactor = ((System.Single)(resources.GetObject("rtbdesc.ZoomFactor")));
			this.rtbdesc.Validated += new System.EventHandler(this.text_Validated);
			// 
			// rtbvalue
			// 
			this.rtbvalue.AccessibleDescription = resources.GetString("rtbvalue.AccessibleDescription");
			this.rtbvalue.AccessibleName = resources.GetString("rtbvalue.AccessibleName");
			this.rtbvalue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rtbvalue.Anchor")));
			this.rtbvalue.AutoSize = ((bool)(resources.GetObject("rtbvalue.AutoSize")));
			this.rtbvalue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rtbvalue.BackgroundImage")));
			this.rtbvalue.BulletIndent = ((int)(resources.GetObject("rtbvalue.BulletIndent")));
			this.rtbvalue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rtbvalue.Dock")));
			this.rtbvalue.Enabled = ((bool)(resources.GetObject("rtbvalue.Enabled")));
			this.rtbvalue.Font = ((System.Drawing.Font)(resources.GetObject("rtbvalue.Font")));
			this.rtbvalue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rtbvalue.ImeMode")));
			this.rtbvalue.Location = ((System.Drawing.Point)(resources.GetObject("rtbvalue.Location")));
			this.rtbvalue.MaxLength = ((int)(resources.GetObject("rtbvalue.MaxLength")));
			this.rtbvalue.Multiline = ((bool)(resources.GetObject("rtbvalue.Multiline")));
			this.rtbvalue.Name = "rtbvalue";
			this.rtbvalue.RightMargin = ((int)(resources.GetObject("rtbvalue.RightMargin")));
			this.rtbvalue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rtbvalue.RightToLeft")));
			this.rtbvalue.ScrollBars = ((System.Windows.Forms.RichTextBoxScrollBars)(resources.GetObject("rtbvalue.ScrollBars")));
			this.rtbvalue.Size = ((System.Drawing.Size)(resources.GetObject("rtbvalue.Size")));
			this.rtbvalue.TabIndex = ((int)(resources.GetObject("rtbvalue.TabIndex")));
			this.rtbvalue.Text = resources.GetString("rtbvalue.Text");
			this.rtbvalue.Visible = ((bool)(resources.GetObject("rtbvalue.Visible")));
			this.rtbvalue.WordWrap = ((bool)(resources.GetObject("rtbvalue.WordWrap")));
			this.rtbvalue.ZoomFactor = ((System.Single)(resources.GetObject("rtbvalue.ZoomFactor")));
			this.rtbvalue.Validated += new System.EventHandler(this.text_Validated);
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
			// btnDown
			// 
			this.btnDown.AccessibleDescription = resources.GetString("btnDown.AccessibleDescription");
			this.btnDown.AccessibleName = resources.GetString("btnDown.AccessibleName");
			this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDown.Anchor")));
			this.btnDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDown.BackgroundImage")));
			this.btnDown.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDown.Dock")));
			this.btnDown.Enabled = ((bool)(resources.GetObject("btnDown.Enabled")));
			this.btnDown.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDown.FlatStyle")));
			this.btnDown.Font = ((System.Drawing.Font)(resources.GetObject("btnDown.Font")));
			this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
			this.btnDown.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDown.ImageAlign")));
			this.btnDown.ImageIndex = ((int)(resources.GetObject("btnDown.ImageIndex")));
			this.btnDown.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDown.ImeMode")));
			this.btnDown.Location = ((System.Drawing.Point)(resources.GetObject("btnDown.Location")));
			this.btnDown.Name = "btnDown";
			this.btnDown.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDown.RightToLeft")));
			this.btnDown.Size = ((System.Drawing.Size)(resources.GetObject("btnDown.Size")));
			this.btnDown.TabIndex = ((int)(resources.GetObject("btnDown.TabIndex")));
			this.btnDown.Text = resources.GetString("btnDown.Text");
			this.btnDown.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDown.TextAlign")));
			this.btnDown.Visible = ((bool)(resources.GetObject("btnDown.Visible")));
			this.btnDown.Click += new System.EventHandler(this.btnUpDown_Click);
			// 
			// splitter1
			// 
			this.splitter1.AccessibleDescription = resources.GetString("splitter1.AccessibleDescription");
			this.splitter1.AccessibleName = resources.GetString("splitter1.AccessibleName");
			this.splitter1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("splitter1.Anchor")));
			this.splitter1.BackColor = System.Drawing.SystemColors.Control;
			this.splitter1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("splitter1.BackgroundImage")));
			this.splitter1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("splitter1.Dock")));
			this.splitter1.Enabled = ((bool)(resources.GetObject("splitter1.Enabled")));
			this.splitter1.Font = ((System.Drawing.Font)(resources.GetObject("splitter1.Font")));
			this.splitter1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("splitter1.ImeMode")));
			this.splitter1.Location = ((System.Drawing.Point)(resources.GetObject("splitter1.Location")));
			this.splitter1.MinExtra = ((int)(resources.GetObject("splitter1.MinExtra")));
			this.splitter1.MinSize = ((int)(resources.GetObject("splitter1.MinSize")));
			this.splitter1.Name = "splitter1";
			this.splitter1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("splitter1.RightToLeft")));
			this.splitter1.Size = ((System.Drawing.Size)(resources.GetObject("splitter1.Size")));
			this.splitter1.TabIndex = ((int)(resources.GetObject("splitter1.TabIndex")));
			this.splitter1.TabStop = false;
			this.splitter1.Visible = ((bool)(resources.GetObject("splitter1.Visible")));
			// 
			// panel1
			// 
			this.panel1.AccessibleDescription = resources.GetString("panel1.AccessibleDescription");
			this.panel1.AccessibleName = resources.GetString("panel1.AccessibleName");
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel1.Anchor")));
			this.panel1.AutoScroll = ((bool)(resources.GetObject("panel1.AutoScroll")));
			this.panel1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel1.AutoScrollMargin")));
			this.panel1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel1.AutoScrollMinSize")));
			this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
			this.panel1.Controls.Add(this.lbtexts);
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
			// lbtexts
			// 
			this.lbtexts.AccessibleDescription = resources.GetString("lbtexts.AccessibleDescription");
			this.lbtexts.AccessibleName = resources.GetString("lbtexts.AccessibleName");
			this.lbtexts.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbtexts.Anchor")));
			this.lbtexts.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lbtexts.BackgroundImage")));
			this.lbtexts.ColumnWidth = ((int)(resources.GetObject("lbtexts.ColumnWidth")));
			this.lbtexts.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbtexts.Dock")));
			this.lbtexts.Enabled = ((bool)(resources.GetObject("lbtexts.Enabled")));
			this.lbtexts.Font = ((System.Drawing.Font)(resources.GetObject("lbtexts.Font")));
			this.lbtexts.HorizontalExtent = ((int)(resources.GetObject("lbtexts.HorizontalExtent")));
			this.lbtexts.HorizontalScrollbar = ((bool)(resources.GetObject("lbtexts.HorizontalScrollbar")));
			this.lbtexts.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbtexts.ImeMode")));
			this.lbtexts.IntegralHeight = ((bool)(resources.GetObject("lbtexts.IntegralHeight")));
			this.lbtexts.ItemHeight = ((int)(resources.GetObject("lbtexts.ItemHeight")));
			this.lbtexts.Location = ((System.Drawing.Point)(resources.GetObject("lbtexts.Location")));
			this.lbtexts.Name = "lbtexts";
			this.lbtexts.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbtexts.RightToLeft")));
			this.lbtexts.ScrollAlwaysVisible = ((bool)(resources.GetObject("lbtexts.ScrollAlwaysVisible")));
			this.lbtexts.Size = ((System.Drawing.Size)(resources.GetObject("lbtexts.Size")));
			this.lbtexts.TabIndex = ((int)(resources.GetObject("lbtexts.TabIndex")));
			this.lbtexts.Visible = ((bool)(resources.GetObject("lbtexts.Visible")));
			this.lbtexts.SelectedIndexChanged += new System.EventHandler(this.StringSelected);
			// 
			// tbformat
			// 
			this.tbformat.AccessibleDescription = resources.GetString("tbformat.AccessibleDescription");
			this.tbformat.AccessibleName = resources.GetString("tbformat.AccessibleName");
			this.tbformat.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbformat.Anchor")));
			this.tbformat.AutoSize = ((bool)(resources.GetObject("tbformat.AutoSize")));
			this.tbformat.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbformat.BackgroundImage")));
			this.tbformat.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbformat.Dock")));
			this.tbformat.Enabled = ((bool)(resources.GetObject("tbformat.Enabled")));
			this.tbformat.Font = ((System.Drawing.Font)(resources.GetObject("tbformat.Font")));
			this.tbformat.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbformat.ImeMode")));
			this.tbformat.Location = ((System.Drawing.Point)(resources.GetObject("tbformat.Location")));
			this.tbformat.MaxLength = ((int)(resources.GetObject("tbformat.MaxLength")));
			this.tbformat.Multiline = ((bool)(resources.GetObject("tbformat.Multiline")));
			this.tbformat.Name = "tbformat";
			this.tbformat.PasswordChar = ((char)(resources.GetObject("tbformat.PasswordChar")));
			this.tbformat.ReadOnly = true;
			this.tbformat.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbformat.RightToLeft")));
			this.tbformat.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbformat.ScrollBars")));
			this.tbformat.Size = ((System.Drawing.Size)(resources.GetObject("tbformat.Size")));
			this.tbformat.TabIndex = ((int)(resources.GetObject("tbformat.TabIndex")));
			this.tbformat.TabStop = false;
			this.tbformat.Text = resources.GetString("tbformat.Text");
			this.tbformat.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbformat.TextAlign")));
			this.tbformat.Visible = ((bool)(resources.GetObject("tbformat.Visible")));
			this.tbformat.WordWrap = ((bool)(resources.GetObject("tbformat.WordWrap")));
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
			this.panel2.Controls.Add(this.lbstr);
			this.panel2.Controls.Add(this.banner);
			this.panel2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panel2.Dock")));
			this.panel2.Enabled = ((bool)(resources.GetObject("panel2.Enabled")));
			this.panel2.Font = ((System.Drawing.Font)(resources.GetObject("panel2.Font")));
			this.panel2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.panel2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panel2.ImeMode")));
			this.panel2.Location = ((System.Drawing.Point)(resources.GetObject("panel2.Location")));
			this.panel2.Name = "panel2";
			this.panel2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panel2.RightToLeft")));
			this.panel2.Size = ((System.Drawing.Size)(resources.GetObject("panel2.Size")));
			this.panel2.TabIndex = ((int)(resources.GetObject("panel2.TabIndex")));
			this.panel2.Text = resources.GetString("panel2.Text");
			this.panel2.Visible = ((bool)(resources.GetObject("panel2.Visible")));
			// 
			// lbstr
			// 
			this.lbstr.AccessibleDescription = resources.GetString("lbstr.AccessibleDescription");
			this.lbstr.AccessibleName = resources.GetString("lbstr.AccessibleName");
			this.lbstr.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbstr.Anchor")));
			this.lbstr.AutoSize = ((bool)(resources.GetObject("lbstr.AutoSize")));
			this.lbstr.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbstr.Dock")));
			this.lbstr.Enabled = ((bool)(resources.GetObject("lbstr.Enabled")));
			this.lbstr.Font = ((System.Drawing.Font)(resources.GetObject("lbstr.Font")));
			this.lbstr.Image = ((System.Drawing.Image)(resources.GetObject("lbstr.Image")));
			this.lbstr.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbstr.ImageAlign")));
			this.lbstr.ImageIndex = ((int)(resources.GetObject("lbstr.ImageIndex")));
			this.lbstr.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbstr.ImeMode")));
			this.lbstr.Location = ((System.Drawing.Point)(resources.GetObject("lbstr.Location")));
			this.lbstr.Name = "lbstr";
			this.lbstr.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbstr.RightToLeft")));
			this.lbstr.Size = ((System.Drawing.Size)(resources.GetObject("lbstr.Size")));
			this.lbstr.TabIndex = ((int)(resources.GetObject("lbstr.TabIndex")));
			this.lbstr.Text = resources.GetString("lbstr.Text");
			this.lbstr.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbstr.TextAlign")));
			this.lbstr.Visible = ((bool)(resources.GetObject("lbstr.Visible")));
			// 
			// banner
			// 
			this.banner.AccessibleDescription = resources.GetString("banner.AccessibleDescription");
			this.banner.AccessibleName = resources.GetString("banner.AccessibleName");
			this.banner.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("banner.Anchor")));
			this.banner.AutoSize = ((bool)(resources.GetObject("banner.AutoSize")));
			this.banner.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("banner.Dock")));
			this.banner.Enabled = ((bool)(resources.GetObject("banner.Enabled")));
			this.banner.Font = ((System.Drawing.Font)(resources.GetObject("banner.Font")));
			this.banner.Image = ((System.Drawing.Image)(resources.GetObject("banner.Image")));
			this.banner.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("banner.ImageAlign")));
			this.banner.ImageIndex = ((int)(resources.GetObject("banner.ImageIndex")));
			this.banner.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("banner.ImeMode")));
			this.banner.Location = ((System.Drawing.Point)(resources.GetObject("banner.Location")));
			this.banner.Name = "banner";
			this.banner.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("banner.RightToLeft")));
			this.banner.Size = ((System.Drawing.Size)(resources.GetObject("banner.Size")));
			this.banner.TabIndex = ((int)(resources.GetObject("banner.TabIndex")));
			this.banner.Text = resources.GetString("banner.Text");
			this.banner.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("banner.TextAlign")));
			this.banner.Visible = ((bool)(resources.GetObject("banner.Visible")));
			// 
			// llcreate
			// 
			this.llcreate.AccessibleDescription = resources.GetString("llcreate.AccessibleDescription");
			this.llcreate.AccessibleName = resources.GetString("llcreate.AccessibleName");
			this.llcreate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llcreate.Anchor")));
			this.llcreate.AutoSize = ((bool)(resources.GetObject("llcreate.AutoSize")));
			this.llcreate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llcreate.Dock")));
			this.llcreate.Enabled = ((bool)(resources.GetObject("llcreate.Enabled")));
			this.llcreate.Font = ((System.Drawing.Font)(resources.GetObject("llcreate.Font")));
			this.llcreate.Image = ((System.Drawing.Image)(resources.GetObject("llcreate.Image")));
			this.llcreate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llcreate.ImageAlign")));
			this.llcreate.ImageIndex = ((int)(resources.GetObject("llcreate.ImageIndex")));
			this.llcreate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llcreate.ImeMode")));
			this.llcreate.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llcreate.LinkArea")));
			this.llcreate.Location = ((System.Drawing.Point)(resources.GetObject("llcreate.Location")));
			this.llcreate.Name = "llcreate";
			this.llcreate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llcreate.RightToLeft")));
			this.llcreate.Size = ((System.Drawing.Size)(resources.GetObject("llcreate.Size")));
			this.llcreate.TabIndex = ((int)(resources.GetObject("llcreate.TabIndex")));
			this.llcreate.TabStop = true;
			this.llcreate.Text = resources.GetString("llcreate.Text");
			this.llcreate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llcreate.TextAlign")));
			this.llcreate.Visible = ((bool)(resources.GetObject("llcreate.Visible")));
			this.llcreate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CreateTextFile);
			// 
			// btnEnglish
			// 
			this.btnEnglish.AccessibleDescription = resources.GetString("btnEnglish.AccessibleDescription");
			this.btnEnglish.AccessibleName = resources.GetString("btnEnglish.AccessibleName");
			this.btnEnglish.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnEnglish.Anchor")));
			this.btnEnglish.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEnglish.BackgroundImage")));
			this.btnEnglish.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnEnglish.Dock")));
			this.btnEnglish.Enabled = ((bool)(resources.GetObject("btnEnglish.Enabled")));
			this.btnEnglish.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnEnglish.FlatStyle")));
			this.btnEnglish.Font = ((System.Drawing.Font)(resources.GetObject("btnEnglish.Font")));
			this.btnEnglish.Image = ((System.Drawing.Image)(resources.GetObject("btnEnglish.Image")));
			this.btnEnglish.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnEnglish.ImageAlign")));
			this.btnEnglish.ImageIndex = ((int)(resources.GetObject("btnEnglish.ImageIndex")));
			this.btnEnglish.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnEnglish.ImeMode")));
			this.btnEnglish.Location = ((System.Drawing.Point)(resources.GetObject("btnEnglish.Location")));
			this.btnEnglish.Name = "btnEnglish";
			this.btnEnglish.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnEnglish.RightToLeft")));
			this.btnEnglish.Size = ((System.Drawing.Size)(resources.GetObject("btnEnglish.Size")));
			this.btnEnglish.TabIndex = ((int)(resources.GetObject("btnEnglish.TabIndex")));
			this.btnEnglish.Text = resources.GetString("btnEnglish.Text");
			this.btnEnglish.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnEnglish.TextAlign")));
			this.btnEnglish.Visible = ((bool)(resources.GetObject("btnEnglish.Visible")));
			this.btnEnglish.Click += new System.EventHandler(this.btnEnglish_Click);
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
			// label9
			// 
			this.label9.AccessibleDescription = resources.GetString("label9.AccessibleDescription");
			this.label9.AccessibleName = resources.GetString("label9.AccessibleName");
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label9.Anchor")));
			this.label9.AutoSize = ((bool)(resources.GetObject("label9.AutoSize")));
			this.label9.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label9.Dock")));
			this.label9.Enabled = ((bool)(resources.GetObject("label9.Enabled")));
			this.label9.Font = ((System.Drawing.Font)(resources.GetObject("label9.Font")));
			this.label9.Image = ((System.Drawing.Image)(resources.GetObject("label9.Image")));
			this.label9.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label9.ImageAlign")));
			this.label9.ImageIndex = ((int)(resources.GetObject("label9.ImageIndex")));
			this.label9.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label9.ImeMode")));
			this.label9.Location = ((System.Drawing.Point)(resources.GetObject("label9.Location")));
			this.label9.Name = "label9";
			this.label9.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label9.RightToLeft")));
			this.label9.Size = ((System.Drawing.Size)(resources.GetObject("label9.Size")));
			this.label9.TabIndex = ((int)(resources.GetObject("label9.TabIndex")));
			this.label9.Text = resources.GetString("label9.Text");
			this.label9.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label9.TextAlign")));
			this.label9.Visible = ((bool)(resources.GetObject("label9.Visible")));
			// 
			// label10
			// 
			this.label10.AccessibleDescription = resources.GetString("label10.AccessibleDescription");
			this.label10.AccessibleName = resources.GetString("label10.AccessibleName");
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label10.Anchor")));
			this.label10.AutoSize = ((bool)(resources.GetObject("label10.AutoSize")));
			this.label10.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label10.Dock")));
			this.label10.Enabled = ((bool)(resources.GetObject("label10.Enabled")));
			this.label10.Font = ((System.Drawing.Font)(resources.GetObject("label10.Font")));
			this.label10.Image = ((System.Drawing.Image)(resources.GetObject("label10.Image")));
			this.label10.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label10.ImageAlign")));
			this.label10.ImageIndex = ((int)(resources.GetObject("label10.ImageIndex")));
			this.label10.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label10.ImeMode")));
			this.label10.Location = ((System.Drawing.Point)(resources.GetObject("label10.Location")));
			this.label10.Name = "label10";
			this.label10.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label10.RightToLeft")));
			this.label10.Size = ((System.Drawing.Size)(resources.GetObject("label10.Size")));
			this.label10.TabIndex = ((int)(resources.GetObject("label10.TabIndex")));
			this.label10.Text = resources.GetString("label10.Text");
			this.label10.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label10.TextAlign")));
			this.label10.Visible = ((bool)(resources.GetObject("label10.Visible")));
			// 
			// label11
			// 
			this.label11.AccessibleDescription = resources.GetString("label11.AccessibleDescription");
			this.label11.AccessibleName = resources.GetString("label11.AccessibleName");
			this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label11.Anchor")));
			this.label11.AutoSize = ((bool)(resources.GetObject("label11.AutoSize")));
			this.label11.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label11.Dock")));
			this.label11.Enabled = ((bool)(resources.GetObject("label11.Enabled")));
			this.label11.Font = ((System.Drawing.Font)(resources.GetObject("label11.Font")));
			this.label11.Image = ((System.Drawing.Image)(resources.GetObject("label11.Image")));
			this.label11.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label11.ImageAlign")));
			this.label11.ImageIndex = ((int)(resources.GetObject("label11.ImageIndex")));
			this.label11.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label11.ImeMode")));
			this.label11.Location = ((System.Drawing.Point)(resources.GetObject("label11.Location")));
			this.label11.Name = "label11";
			this.label11.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label11.RightToLeft")));
			this.label11.Size = ((System.Drawing.Size)(resources.GetObject("label11.Size")));
			this.label11.TabIndex = ((int)(resources.GetObject("label11.TabIndex")));
			this.label11.Text = resources.GetString("label11.Text");
			this.label11.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label11.TextAlign")));
			this.label11.Visible = ((bool)(resources.GetObject("label11.Visible")));
			// 
			// btnClearStr
			// 
			this.btnClearStr.AccessibleDescription = resources.GetString("btnClearStr.AccessibleDescription");
			this.btnClearStr.AccessibleName = resources.GetString("btnClearStr.AccessibleName");
			this.btnClearStr.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnClearStr.Anchor")));
			this.btnClearStr.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearStr.BackgroundImage")));
			this.btnClearStr.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnClearStr.Dock")));
			this.btnClearStr.Enabled = ((bool)(resources.GetObject("btnClearStr.Enabled")));
			this.btnClearStr.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnClearStr.FlatStyle")));
			this.btnClearStr.Font = ((System.Drawing.Font)(resources.GetObject("btnClearStr.Font")));
			this.btnClearStr.Image = ((System.Drawing.Image)(resources.GetObject("btnClearStr.Image")));
			this.btnClearStr.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClearStr.ImageAlign")));
			this.btnClearStr.ImageIndex = ((int)(resources.GetObject("btnClearStr.ImageIndex")));
			this.btnClearStr.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnClearStr.ImeMode")));
			this.btnClearStr.Location = ((System.Drawing.Point)(resources.GetObject("btnClearStr.Location")));
			this.btnClearStr.Name = "btnClearStr";
			this.btnClearStr.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnClearStr.RightToLeft")));
			this.btnClearStr.Size = ((System.Drawing.Size)(resources.GetObject("btnClearStr.Size")));
			this.btnClearStr.TabIndex = ((int)(resources.GetObject("btnClearStr.TabIndex")));
			this.btnClearStr.Text = resources.GetString("btnClearStr.Text");
			this.btnClearStr.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClearStr.TextAlign")));
			this.btnClearStr.Visible = ((bool)(resources.GetObject("btnClearStr.Visible")));
			this.btnClearStr.Click += new System.EventHandler(this.btnClearStr_Click);
			// 
			// btnSetLike
			// 
			this.btnSetLike.AccessibleDescription = resources.GetString("btnSetLike.AccessibleDescription");
			this.btnSetLike.AccessibleName = resources.GetString("btnSetLike.AccessibleName");
			this.btnSetLike.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSetLike.Anchor")));
			this.btnSetLike.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSetLike.BackgroundImage")));
			this.btnSetLike.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSetLike.Dock")));
			this.btnSetLike.Enabled = ((bool)(resources.GetObject("btnSetLike.Enabled")));
			this.btnSetLike.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSetLike.FlatStyle")));
			this.btnSetLike.Font = ((System.Drawing.Font)(resources.GetObject("btnSetLike.Font")));
			this.btnSetLike.Image = ((System.Drawing.Image)(resources.GetObject("btnSetLike.Image")));
			this.btnSetLike.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSetLike.ImageAlign")));
			this.btnSetLike.ImageIndex = ((int)(resources.GetObject("btnSetLike.ImageIndex")));
			this.btnSetLike.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSetLike.ImeMode")));
			this.btnSetLike.Location = ((System.Drawing.Point)(resources.GetObject("btnSetLike.Location")));
			this.btnSetLike.Name = "btnSetLike";
			this.btnSetLike.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSetLike.RightToLeft")));
			this.btnSetLike.Size = ((System.Drawing.Size)(resources.GetObject("btnSetLike.Size")));
			this.btnSetLike.TabIndex = ((int)(resources.GetObject("btnSetLike.TabIndex")));
			this.btnSetLike.Text = resources.GetString("btnSetLike.Text");
			this.btnSetLike.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSetLike.TextAlign")));
			this.btnSetLike.Visible = ((bool)(resources.GetObject("btnSetLike.Visible")));
			this.btnSetLike.Click += new System.EventHandler(this.btnSetLike_Click);
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
			// label12
			// 
			this.label12.AccessibleDescription = resources.GetString("label12.AccessibleDescription");
			this.label12.AccessibleName = resources.GetString("label12.AccessibleName");
			this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label12.Anchor")));
			this.label12.AutoSize = ((bool)(resources.GetObject("label12.AutoSize")));
			this.label12.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label12.Dock")));
			this.label12.Enabled = ((bool)(resources.GetObject("label12.Enabled")));
			this.label12.Font = ((System.Drawing.Font)(resources.GetObject("label12.Font")));
			this.label12.Image = ((System.Drawing.Image)(resources.GetObject("label12.Image")));
			this.label12.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label12.ImageAlign")));
			this.label12.ImageIndex = ((int)(resources.GetObject("label12.ImageIndex")));
			this.label12.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label12.ImeMode")));
			this.label12.Location = ((System.Drawing.Point)(resources.GetObject("label12.Location")));
			this.label12.Name = "label12";
			this.label12.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label12.RightToLeft")));
			this.label12.Size = ((System.Drawing.Size)(resources.GetObject("label12.Size")));
			this.label12.TabIndex = ((int)(resources.GetObject("label12.TabIndex")));
			this.label12.Text = resources.GetString("label12.Text");
			this.label12.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label12.TextAlign")));
			this.label12.Visible = ((bool)(resources.GetObject("label12.Visible")));
			// 
			// btnDelLang
			// 
			this.btnDelLang.AccessibleDescription = resources.GetString("btnDelLang.AccessibleDescription");
			this.btnDelLang.AccessibleName = resources.GetString("btnDelLang.AccessibleName");
			this.btnDelLang.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnDelLang.Anchor")));
			this.btnDelLang.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelLang.BackgroundImage")));
			this.btnDelLang.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnDelLang.Dock")));
			this.btnDelLang.Enabled = ((bool)(resources.GetObject("btnDelLang.Enabled")));
			this.btnDelLang.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnDelLang.FlatStyle")));
			this.btnDelLang.Font = ((System.Drawing.Font)(resources.GetObject("btnDelLang.Font")));
			this.btnDelLang.Image = ((System.Drawing.Image)(resources.GetObject("btnDelLang.Image")));
			this.btnDelLang.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelLang.ImageAlign")));
			this.btnDelLang.ImageIndex = ((int)(resources.GetObject("btnDelLang.ImageIndex")));
			this.btnDelLang.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnDelLang.ImeMode")));
			this.btnDelLang.Location = ((System.Drawing.Point)(resources.GetObject("btnDelLang.Location")));
			this.btnDelLang.Name = "btnDelLang";
			this.btnDelLang.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnDelLang.RightToLeft")));
			this.btnDelLang.Size = ((System.Drawing.Size)(resources.GetObject("btnDelLang.Size")));
			this.btnDelLang.TabIndex = ((int)(resources.GetObject("btnDelLang.TabIndex")));
			this.btnDelLang.Text = resources.GetString("btnDelLang.Text");
			this.btnDelLang.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnDelLang.TextAlign")));
			this.btnDelLang.Visible = ((bool)(resources.GetObject("btnDelLang.Visible")));
			this.btnDelLang.Click += new System.EventHandler(this.btnDelLang_Click);
			// 
			// OldStrForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.strPanel);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "OldStrForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.strPanel.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.gbstr.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private void LanguageChanged(object sender, System.EventArgs e)
		{
			int i = lbtexts.SelectedIndex;
			lbtexts.Items.Clear();
			if (this.cblanguage.SelectedIndex<0) return;

			byte lid = ((Language)cblanguage.Items[cblanguage.SelectedIndex]).Lid;

			int nrStrItems = wrapper.nrStrItems(lid);
			for (int index = 0; index < nrStrItems; index++)
				lbtexts.Items.Add("0x" + Helper.HexString((ushort)(index+1)) + " - " + wrapper[lid, index].Title);
			lbtexts.SelectedIndex = -1;
			if (i < 0)
				i = 0;
			if (i >= lbtexts.Items.Count)
				i = lbtexts.Items.Count - 1;
			lbtexts.SelectedIndex = i;
			StringSelected(null, null);
		}

		private void StringSelected(object sender, System.EventArgs e)
		{
			llchangeall.Enabled = false;
			lldelete.Enabled = false;
			lldelall.Enabled = false;
			rtbvalue.Text = "";
			rtbdesc.Text = "";

			if (cblanguage.SelectedIndex < 0) return;
			byte lid = ((Language)cblanguage.Items[cblanguage.SelectedIndex]).Lid;

			if (lbtexts.SelectedIndex < 0) return;
			gbstr.Text = "0x"+Helper.HexString((ushort)(lbtexts.SelectedIndex+1));

			rtbvalue.Text = wrapper[lid, lbtexts.SelectedIndex].Title;
			rtbdesc.Text = wrapper[lid, lbtexts.SelectedIndex].Description;

			llchangeall.Enabled = true;
			lldelete.Enabled = true;
			lldelall.Enabled = true;
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


		private void btnClearStr_Click(object sender, System.EventArgs e)
		{
			foreach (byte lid in wrapper.Languages)
				wrapper.RemoveLanguage(lid);
			wrapper.AddLanguage(0x01);

			int index = cblanguage.SelectedIndex;
			doLanguages();
			cblanguage.SelectedIndex = index;
			LanguageChanged(null, null);
		}

		private void btnAllLangs_Click(object sender, System.EventArgs e)
		{
			byte lid;
			if (cblanguage.SelectedIndex < 0)
				lid = 0x01;
			else
				lid = ((Language)cblanguage.Items[cblanguage.SelectedIndex]).Lid;

			for (byte b = 1; b < 45; b++) wrapper.AddLanguage(b);
			doLanguages();

			cblanguage.SelectedIndex = lid - 1;
		}

		private void btnCopyAll_Click(object sender, System.EventArgs e)
		{
			if (cblanguage.SelectedIndex < 0) return;
			byte lid = ((Language)cblanguage.Items[cblanguage.SelectedIndex]).Lid;

			foreach (byte b in wrapper.Languages)
			{
				if (b == lid) continue;
				wrapper.RemoveLanguage(b);
				wrapper.AddLanguage(b);
				int nrStrItems = wrapper.nrStrItems(lid);
				for (int index = 0; index < nrStrItems; index++)
					wrapper.Add(b, wrapper[lid, index]);
			}
		}

		private void btnEnglish_Click(object sender, System.EventArgs e)
		{
			int index = lbtexts.SelectedIndex;
			for (byte lid = 2; lid < 45; lid++)
				wrapper.RemoveLanguage(lid);
			if (wrapper.Languages.Length == 0)
				wrapper.AddLanguage(0x01);
			doLanguages();
			if (cblanguage.Items.Count > 0)
				cblanguage.SelectedIndex = 0;
			if (index >= lbtexts.Items.Count)
				index = lbtexts.Items.Count - 1;
			lbtexts.SelectedIndex = index;
		}

		private void btnSetLike_Click(object sender, System.EventArgs e)
		{
			if (cblanguage.SelectedIndex < 0) return;
			byte lid = ((Language)cblanguage.Items[cblanguage.SelectedIndex]).Lid;
			if (cbLangFrom.SelectedIndex < 0) return;
			byte lidFrom = ((Language)cbLangFrom.Items[cbLangFrom.SelectedIndex]).Lid;
			if (lid == lidFrom) return;

			int index;
			int nrStrItems = wrapper.nrStrItems(lidFrom);
			for (index = 0; index < nrStrItems; index++)
			{
				StrItem ni = new StrItem(wrapper);
				ni.Title = wrapper[lidFrom, index].Title;
				ni.Description = wrapper[lidFrom, index].Description;
				wrapper[lid, index] = ni;
			}
			while(index < wrapper.nrStrItems(lid)) wrapper.RemoveAt(lid, index);
			LanguageChanged(null, null);
		}

		private void btnDelLang_Click(object sender, System.EventArgs e)
		{
			if (cblanguage.SelectedIndex < 0) return;
			int index = cblanguage.SelectedIndex;
			byte lid = ((Language)cblanguage.Items[cblanguage.SelectedIndex]).Lid;
			if (lid == 0x01) return;

			wrapper.RemoveLanguage(lid);
			this.doLanguages();

			if (index >= cblanguage.Items.Count)
				index = cblanguage.Items.Count - 1;
			cblanguage.SelectedIndex = index;
		}


		private void AddToAll(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if (cblanguage.SelectedIndex < 0)
			{
				if (wrapper.Languages.Length == 0)
				{
					btnAllLangs_Click(null, null);
				}
				cblanguage.SelectedIndex = 0;
			}
			StrAdd(null, null);

			byte lid = ((Language)cblanguage.Items[cblanguage.SelectedIndex]).Lid;
			int sid = lbtexts.SelectedIndex;
			StrItem ci = wrapper[lid, sid];

			foreach (byte b in wrapper.Languages)
			{
				wrapper[b, sid] = new StrItem(wrapper);
				if (wrapper[b, sid] != null)
				{
					wrapper[b, sid].Title = ci.Title;
					wrapper[b, sid].Description = ci.Description;
				}
			}
			LanguageChanged(null, null);
		}

		private void DelInAll(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			int index = lbtexts.SelectedIndex;

			if (index < 0) return;

			foreach (byte b in wrapper.Languages)
				wrapper.RemoveAt(b, index);

			lbtexts.Items.RemoveAt(index);
			if (index >= lbtexts.Items.Count)
				index = lbtexts.Items.Count - 1;
			lbtexts.SelectedIndex = index;
		}

		private void ChangeInAllLanguages(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			int sid = lbtexts.SelectedIndex;

			if (cblanguage.SelectedIndex < 0 || sid < 0) return;

			byte lid = ((Language)cblanguage.Items[cblanguage.SelectedIndex]).Lid;
			StrItem ci = wrapper[lid, sid]; // the item to be copied to all

			if (ci == null) foreach (byte b in wrapper.Languages) wrapper[b, sid] = null;
			else foreach (byte b in wrapper.Languages)
				 {
					 StrItem ni = wrapper[b, sid];
					 if (ni == null) ni = new StrItem(wrapper);
					 ni.Title = ci.Title;
					 ni.Description = ci.Description;
					 wrapper[b, sid] = ni;
				 }
		}


		private void btnUpDown_Click(object sender, System.EventArgs e)
		{
			if (cblanguage.SelectedIndex < 0 || lbtexts.SelectedIndex < 0) return;
			int move = 1;
			if (sender == btnUp) move = -1;

			int index = lbtexts.SelectedIndex;
			int newindex = index + move;

			if (newindex < 0 || newindex >= lbtexts.Items.Count) return;

			byte lid = ((Language)cblanguage.Items[cblanguage.SelectedIndex]).Lid;
			StrItem ci = wrapper[lid, index];

			while (index < newindex)
			{
				wrapper[lid, index] = wrapper[lid, index+1];
				wrapper[lid, index+1] = ci;
				index++;
			}
			while (index > newindex)
			{
				wrapper[lid, index] = wrapper[lid, index-1];
				wrapper[lid, index-1] = ci;
				index--;
			}
			LanguageChanged(null, null);
			lbtexts.SelectedIndex = newindex;
		}

		private void StrAdd(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if (cblanguage.SelectedIndex < 0) return;

			byte lid = ((Language)cblanguage.Items[cblanguage.SelectedIndex]).Lid;
			StrItem ni = wrapper.Add(lid);
			if (ni == null) return;

			if (lbtexts.SelectedIndex != -1)
			{
				StrItem ci = wrapper[lid, lbtexts.SelectedIndex];
				if (ci != null)
				{
					ni.Title = ci.Title;
					ni.Description = ci.Description;
				}
			}
			else
			{
				ni.Title = rtbvalue.Text;
				ni.Description = rtbdesc.Text;
			}
			lbtexts.Items.Add("0x" + Helper.HexString((ushort)(lbtexts.Items.Count)) + " - " + ni.Title);
			lbtexts.SelectedIndex = lbtexts.Items.Count - 1;
		}

		private void StrDelete(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if (cblanguage.SelectedIndex < 0) return;
			int index = lbtexts.SelectedIndex;
			if (index < 0) return;

			wrapper.RemoveAt(((Language)cblanguage.Items[cblanguage.SelectedIndex]).Lid, index);

			lbtexts.Items.RemoveAt(index);
			if (index >= lbtexts.Items.Count)
				index = lbtexts.Items.Count - 1;
			lbtexts.SelectedIndex = index;
		}


		private void CreateTextFile(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if (cblanguage.SelectedIndex < 0) return;

			byte lid = ((Language)cblanguage.Items[cblanguage.SelectedIndex]).Lid;
			try 
			{
				string list = "";
				int nrStrItems = wrapper.nrStrItems(lid);
				for (int i=0; i < nrStrItems; i++)
				{
					StrItem ni = wrapper[lid, i];
					list += "0x" + Helper.HexString((ushort)(i+1)) + " - " + ni.Title + " (" + ni.Description + ")" + Helper.lbr;
				}

				Clipboard.SetDataObject(list, true);
			} 
			catch (Exception) { }
		}


		private void text_Validated(object sender, System.EventArgs e)
		{
			if (cblanguage.SelectedIndex < 0) return;
			byte lid = ((Language)cblanguage.Items[cblanguage.SelectedIndex]).Lid;
			if (lbtexts.SelectedIndex < 0) return;
			int index = lbtexts.SelectedIndex;

			if (sender == rtbvalue)
			{
				wrapper[lid, index].Title = rtbvalue.Text;
				lbtexts.Items[index] = "0x" + Helper.HexString((ushort)(index+1)) + " - " + wrapper[lid, index].Title;
			}
			else
			{
				wrapper[lid, index].Description = rtbdesc.Text;
			}
		}

	}

	internal class Language
	{
		private byte lid;
		internal Language(byte lid)
		{
			this.lid = lid;
		}
		internal byte Lid { get { return lid; } }
		public override string ToString()
		{
			string enumName = ((Data.MetaData.Languages)lid).ToString();
			string s = Localization.Manager.GetString( enumName );
			if (s == null) s = enumName;
			return "0x" + Helper.HexString(lid) + " - " + s;
		}
	}
}
