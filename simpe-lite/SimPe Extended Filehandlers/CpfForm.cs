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
	/// Zusammenfassung für Elements2.
	/// </summary>
	public class CpfForm  : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ListBox lbcpf;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RichTextBox rtbcpf;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.RichTextBox rtbcpfname;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox cbtype;
		private System.Windows.Forms.LinkLabel llcpfadd;
		private System.Windows.Forms.LinkLabel llcpfchange;
		private System.Windows.Forms.Button btcpfcommit;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Button btprev;
		private System.Windows.Forms.Panel cpfPanel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public CpfForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			cbtype.Items.Add(Data.MetaData.DataTypes.dtString);
			cbtype.Items.Add(Data.MetaData.DataTypes.dtUInteger);
			cbtype.Items.Add(Data.MetaData.DataTypes.dtInteger);
			cbtype.Items.Add(Data.MetaData.DataTypes.dtSingle);
			cbtype.Items.Add(Data.MetaData.DataTypes.dtBoolean);

			cbtype.SelectedIndex = 0;

			this.fkt = null;
		}

		public CpfForm(SimPe.PackedFiles.UserInterface.CpfForm.ExecutePreview fkt)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			cbtype.Items.Add(Data.MetaData.DataTypes.dtString);
			cbtype.Items.Add(Data.MetaData.DataTypes.dtUInteger);
			cbtype.Items.Add(Data.MetaData.DataTypes.dtInteger);
			cbtype.Items.Add(Data.MetaData.DataTypes.dtSingle);
			cbtype.Items.Add(Data.MetaData.DataTypes.dtBoolean);

			cbtype.SelectedIndex = 0;

			this.fkt = fkt;
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


		#region CpfForm
		private Cpf wrapper;
		private SimPe.PackedFiles.UserInterface.CpfForm.ExecutePreview fkt;
		public delegate void ExecutePreview(Cpf mmat, SimPe.Interfaces.Files.IPackageFile package);
		#endregion

		#region IPackedFileUI Member
		/// <summary>
		/// Returns the Panel that will be displayed within SimPe
		/// </summary>
		public System.Windows.Forms.Panel GUIHandle
		{
			get
			{
				return cpfPanel;
			}
		}

		/// <summary>
		/// Is called by SimPe (through the Wrapper) when the Panel is going to be displayed, so
		/// you should updatet the Data displayed by the Panel with the Attributes stored in the
		/// passed Wrapper.
		/// </summary>
		/// <param name="wrp">The Attributes of this Wrapper have to be displayed</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (Cpf) wrp;

			lbcpf.Items.Clear();
			foreach (CpfItem item in wrapper.Items)
			{
				lbcpf.Items.Add(item);
			}

			llcpfchange.Enabled = false;
			btprev.Visible = (fkt!=null);
		}		

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CpfForm));
			this.cpfPanel = new System.Windows.Forms.Panel();
			this.btprev = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.llcpfadd = new System.Windows.Forms.LinkLabel();
			this.llcpfchange = new System.Windows.Forms.LinkLabel();
			this.cbtype = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.rtbcpfname = new System.Windows.Forms.RichTextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.rtbcpf = new System.Windows.Forms.RichTextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.btcpfcommit = new System.Windows.Forms.Button();
			this.lbcpf = new System.Windows.Forms.ListBox();
			this.panel5 = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.cpfPanel.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel5.SuspendLayout();
			this.SuspendLayout();
			// 
			// cpfPanel
			// 
			this.cpfPanel.AccessibleDescription = resources.GetString("cpfPanel.AccessibleDescription");
			this.cpfPanel.AccessibleName = resources.GetString("cpfPanel.AccessibleName");
			this.cpfPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cpfPanel.Anchor")));
			this.cpfPanel.AutoScroll = ((bool)(resources.GetObject("cpfPanel.AutoScroll")));
			this.cpfPanel.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("cpfPanel.AutoScrollMargin")));
			this.cpfPanel.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("cpfPanel.AutoScrollMinSize")));
			this.cpfPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cpfPanel.BackgroundImage")));
			this.cpfPanel.Controls.Add(this.btprev);
			this.cpfPanel.Controls.Add(this.groupBox2);
			this.cpfPanel.Controls.Add(this.btcpfcommit);
			this.cpfPanel.Controls.Add(this.lbcpf);
			this.cpfPanel.Controls.Add(this.panel5);
			this.cpfPanel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cpfPanel.Dock")));
			this.cpfPanel.Enabled = ((bool)(resources.GetObject("cpfPanel.Enabled")));
			this.cpfPanel.Font = ((System.Drawing.Font)(resources.GetObject("cpfPanel.Font")));
			this.cpfPanel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cpfPanel.ImeMode")));
			this.cpfPanel.Location = ((System.Drawing.Point)(resources.GetObject("cpfPanel.Location")));
			this.cpfPanel.Name = "cpfPanel";
			this.cpfPanel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cpfPanel.RightToLeft")));
			this.cpfPanel.Size = ((System.Drawing.Size)(resources.GetObject("cpfPanel.Size")));
			this.cpfPanel.TabIndex = ((int)(resources.GetObject("cpfPanel.TabIndex")));
			this.cpfPanel.Text = resources.GetString("cpfPanel.Text");
			this.cpfPanel.Visible = ((bool)(resources.GetObject("cpfPanel.Visible")));
			// 
			// btprev
			// 
			this.btprev.AccessibleDescription = resources.GetString("btprev.AccessibleDescription");
			this.btprev.AccessibleName = resources.GetString("btprev.AccessibleName");
			this.btprev.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btprev.Anchor")));
			this.btprev.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btprev.BackgroundImage")));
			this.btprev.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btprev.Dock")));
			this.btprev.Enabled = ((bool)(resources.GetObject("btprev.Enabled")));
			this.btprev.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btprev.FlatStyle")));
			this.btprev.Font = ((System.Drawing.Font)(resources.GetObject("btprev.Font")));
			this.btprev.Image = ((System.Drawing.Image)(resources.GetObject("btprev.Image")));
			this.btprev.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btprev.ImageAlign")));
			this.btprev.ImageIndex = ((int)(resources.GetObject("btprev.ImageIndex")));
			this.btprev.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btprev.ImeMode")));
			this.btprev.Location = ((System.Drawing.Point)(resources.GetObject("btprev.Location")));
			this.btprev.Name = "btprev";
			this.btprev.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btprev.RightToLeft")));
			this.btprev.Size = ((System.Drawing.Size)(resources.GetObject("btprev.Size")));
			this.btprev.TabIndex = ((int)(resources.GetObject("btprev.TabIndex")));
			this.btprev.Text = resources.GetString("btprev.Text");
			this.btprev.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btprev.TextAlign")));
			this.btprev.Visible = ((bool)(resources.GetObject("btprev.Visible")));
			this.btprev.Click += new System.EventHandler(this.btprev_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.AccessibleDescription = resources.GetString("groupBox2.AccessibleDescription");
			this.groupBox2.AccessibleName = resources.GetString("groupBox2.AccessibleName");
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox2.Anchor")));
			this.groupBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox2.BackgroundImage")));
			this.groupBox2.Controls.Add(this.linkLabel1);
			this.groupBox2.Controls.Add(this.llcpfadd);
			this.groupBox2.Controls.Add(this.llcpfchange);
			this.groupBox2.Controls.Add(this.cbtype);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.rtbcpfname);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.rtbcpf);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("groupBox2.Dock")));
			this.groupBox2.Enabled = ((bool)(resources.GetObject("groupBox2.Enabled")));
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Font = ((System.Drawing.Font)(resources.GetObject("groupBox2.Font")));
			this.groupBox2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("groupBox2.ImeMode")));
			this.groupBox2.Location = ((System.Drawing.Point)(resources.GetObject("groupBox2.Location")));
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("groupBox2.RightToLeft")));
			this.groupBox2.Size = ((System.Drawing.Size)(resources.GetObject("groupBox2.Size")));
			this.groupBox2.TabIndex = ((int)(resources.GetObject("groupBox2.TabIndex")));
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = resources.GetString("groupBox2.Text");
			this.groupBox2.Visible = ((bool)(resources.GetObject("groupBox2.Visible")));
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
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DeleteCpf);
			// 
			// llcpfadd
			// 
			this.llcpfadd.AccessibleDescription = resources.GetString("llcpfadd.AccessibleDescription");
			this.llcpfadd.AccessibleName = resources.GetString("llcpfadd.AccessibleName");
			this.llcpfadd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llcpfadd.Anchor")));
			this.llcpfadd.AutoSize = ((bool)(resources.GetObject("llcpfadd.AutoSize")));
			this.llcpfadd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llcpfadd.Dock")));
			this.llcpfadd.Enabled = ((bool)(resources.GetObject("llcpfadd.Enabled")));
			this.llcpfadd.Font = ((System.Drawing.Font)(resources.GetObject("llcpfadd.Font")));
			this.llcpfadd.Image = ((System.Drawing.Image)(resources.GetObject("llcpfadd.Image")));
			this.llcpfadd.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llcpfadd.ImageAlign")));
			this.llcpfadd.ImageIndex = ((int)(resources.GetObject("llcpfadd.ImageIndex")));
			this.llcpfadd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llcpfadd.ImeMode")));
			this.llcpfadd.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llcpfadd.LinkArea")));
			this.llcpfadd.Location = ((System.Drawing.Point)(resources.GetObject("llcpfadd.Location")));
			this.llcpfadd.Name = "llcpfadd";
			this.llcpfadd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llcpfadd.RightToLeft")));
			this.llcpfadd.Size = ((System.Drawing.Size)(resources.GetObject("llcpfadd.Size")));
			this.llcpfadd.TabIndex = ((int)(resources.GetObject("llcpfadd.TabIndex")));
			this.llcpfadd.TabStop = true;
			this.llcpfadd.Text = resources.GetString("llcpfadd.Text");
			this.llcpfadd.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llcpfadd.TextAlign")));
			this.llcpfadd.Visible = ((bool)(resources.GetObject("llcpfadd.Visible")));
			this.llcpfadd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AddCpf);
			// 
			// llcpfchange
			// 
			this.llcpfchange.AccessibleDescription = resources.GetString("llcpfchange.AccessibleDescription");
			this.llcpfchange.AccessibleName = resources.GetString("llcpfchange.AccessibleName");
			this.llcpfchange.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llcpfchange.Anchor")));
			this.llcpfchange.AutoSize = ((bool)(resources.GetObject("llcpfchange.AutoSize")));
			this.llcpfchange.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llcpfchange.Dock")));
			this.llcpfchange.Enabled = ((bool)(resources.GetObject("llcpfchange.Enabled")));
			this.llcpfchange.Font = ((System.Drawing.Font)(resources.GetObject("llcpfchange.Font")));
			this.llcpfchange.Image = ((System.Drawing.Image)(resources.GetObject("llcpfchange.Image")));
			this.llcpfchange.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llcpfchange.ImageAlign")));
			this.llcpfchange.ImageIndex = ((int)(resources.GetObject("llcpfchange.ImageIndex")));
			this.llcpfchange.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llcpfchange.ImeMode")));
			this.llcpfchange.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llcpfchange.LinkArea")));
			this.llcpfchange.Location = ((System.Drawing.Point)(resources.GetObject("llcpfchange.Location")));
			this.llcpfchange.Name = "llcpfchange";
			this.llcpfchange.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llcpfchange.RightToLeft")));
			this.llcpfchange.Size = ((System.Drawing.Size)(resources.GetObject("llcpfchange.Size")));
			this.llcpfchange.TabIndex = ((int)(resources.GetObject("llcpfchange.TabIndex")));
			this.llcpfchange.TabStop = true;
			this.llcpfchange.Text = resources.GetString("llcpfchange.Text");
			this.llcpfchange.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llcpfchange.TextAlign")));
			this.llcpfchange.Visible = ((bool)(resources.GetObject("llcpfchange.Visible")));
			this.llcpfchange.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CpfChange);
			// 
			// cbtype
			// 
			this.cbtype.AccessibleDescription = resources.GetString("cbtype.AccessibleDescription");
			this.cbtype.AccessibleName = resources.GetString("cbtype.AccessibleName");
			this.cbtype.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbtype.Anchor")));
			this.cbtype.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbtype.BackgroundImage")));
			this.cbtype.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbtype.Dock")));
			this.cbtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbtype.Enabled = ((bool)(resources.GetObject("cbtype.Enabled")));
			this.cbtype.Font = ((System.Drawing.Font)(resources.GetObject("cbtype.Font")));
			this.cbtype.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbtype.ImeMode")));
			this.cbtype.IntegralHeight = ((bool)(resources.GetObject("cbtype.IntegralHeight")));
			this.cbtype.ItemHeight = ((int)(resources.GetObject("cbtype.ItemHeight")));
			this.cbtype.Location = ((System.Drawing.Point)(resources.GetObject("cbtype.Location")));
			this.cbtype.MaxDropDownItems = ((int)(resources.GetObject("cbtype.MaxDropDownItems")));
			this.cbtype.MaxLength = ((int)(resources.GetObject("cbtype.MaxLength")));
			this.cbtype.Name = "cbtype";
			this.cbtype.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbtype.RightToLeft")));
			this.cbtype.Size = ((System.Drawing.Size)(resources.GetObject("cbtype.Size")));
			this.cbtype.TabIndex = ((int)(resources.GetObject("cbtype.TabIndex")));
			this.cbtype.Text = resources.GetString("cbtype.Text");
			this.cbtype.Visible = ((bool)(resources.GetObject("cbtype.Visible")));
			this.cbtype.SelectedIndexChanged += new System.EventHandler(this.CpfAutoChange);
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
			// rtbcpfname
			// 
			this.rtbcpfname.AccessibleDescription = resources.GetString("rtbcpfname.AccessibleDescription");
			this.rtbcpfname.AccessibleName = resources.GetString("rtbcpfname.AccessibleName");
			this.rtbcpfname.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rtbcpfname.Anchor")));
			this.rtbcpfname.AutoSize = ((bool)(resources.GetObject("rtbcpfname.AutoSize")));
			this.rtbcpfname.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rtbcpfname.BackgroundImage")));
			this.rtbcpfname.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtbcpfname.BulletIndent = ((int)(resources.GetObject("rtbcpfname.BulletIndent")));
			this.rtbcpfname.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rtbcpfname.Dock")));
			this.rtbcpfname.Enabled = ((bool)(resources.GetObject("rtbcpfname.Enabled")));
			this.rtbcpfname.Font = ((System.Drawing.Font)(resources.GetObject("rtbcpfname.Font")));
			this.rtbcpfname.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rtbcpfname.ImeMode")));
			this.rtbcpfname.Location = ((System.Drawing.Point)(resources.GetObject("rtbcpfname.Location")));
			this.rtbcpfname.MaxLength = ((int)(resources.GetObject("rtbcpfname.MaxLength")));
			this.rtbcpfname.Multiline = ((bool)(resources.GetObject("rtbcpfname.Multiline")));
			this.rtbcpfname.Name = "rtbcpfname";
			this.rtbcpfname.RightMargin = ((int)(resources.GetObject("rtbcpfname.RightMargin")));
			this.rtbcpfname.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rtbcpfname.RightToLeft")));
			this.rtbcpfname.ScrollBars = ((System.Windows.Forms.RichTextBoxScrollBars)(resources.GetObject("rtbcpfname.ScrollBars")));
			this.rtbcpfname.Size = ((System.Drawing.Size)(resources.GetObject("rtbcpfname.Size")));
			this.rtbcpfname.TabIndex = ((int)(resources.GetObject("rtbcpfname.TabIndex")));
			this.rtbcpfname.Text = resources.GetString("rtbcpfname.Text");
			this.rtbcpfname.Visible = ((bool)(resources.GetObject("rtbcpfname.Visible")));
			this.rtbcpfname.WordWrap = ((bool)(resources.GetObject("rtbcpfname.WordWrap")));
			this.rtbcpfname.ZoomFactor = ((System.Single)(resources.GetObject("rtbcpfname.ZoomFactor")));
			this.rtbcpfname.TextChanged += new System.EventHandler(this.CpfAutoChange);
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
			// rtbcpf
			// 
			this.rtbcpf.AccessibleDescription = resources.GetString("rtbcpf.AccessibleDescription");
			this.rtbcpf.AccessibleName = resources.GetString("rtbcpf.AccessibleName");
			this.rtbcpf.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rtbcpf.Anchor")));
			this.rtbcpf.AutoSize = ((bool)(resources.GetObject("rtbcpf.AutoSize")));
			this.rtbcpf.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rtbcpf.BackgroundImage")));
			this.rtbcpf.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtbcpf.BulletIndent = ((int)(resources.GetObject("rtbcpf.BulletIndent")));
			this.rtbcpf.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rtbcpf.Dock")));
			this.rtbcpf.Enabled = ((bool)(resources.GetObject("rtbcpf.Enabled")));
			this.rtbcpf.Font = ((System.Drawing.Font)(resources.GetObject("rtbcpf.Font")));
			this.rtbcpf.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rtbcpf.ImeMode")));
			this.rtbcpf.Location = ((System.Drawing.Point)(resources.GetObject("rtbcpf.Location")));
			this.rtbcpf.MaxLength = ((int)(resources.GetObject("rtbcpf.MaxLength")));
			this.rtbcpf.Multiline = ((bool)(resources.GetObject("rtbcpf.Multiline")));
			this.rtbcpf.Name = "rtbcpf";
			this.rtbcpf.RightMargin = ((int)(resources.GetObject("rtbcpf.RightMargin")));
			this.rtbcpf.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rtbcpf.RightToLeft")));
			this.rtbcpf.ScrollBars = ((System.Windows.Forms.RichTextBoxScrollBars)(resources.GetObject("rtbcpf.ScrollBars")));
			this.rtbcpf.Size = ((System.Drawing.Size)(resources.GetObject("rtbcpf.Size")));
			this.rtbcpf.TabIndex = ((int)(resources.GetObject("rtbcpf.TabIndex")));
			this.rtbcpf.Text = resources.GetString("rtbcpf.Text");
			this.rtbcpf.Visible = ((bool)(resources.GetObject("rtbcpf.Visible")));
			this.rtbcpf.WordWrap = ((bool)(resources.GetObject("rtbcpf.WordWrap")));
			this.rtbcpf.ZoomFactor = ((System.Single)(resources.GetObject("rtbcpf.ZoomFactor")));
			this.rtbcpf.TextChanged += new System.EventHandler(this.CpfAutoChange);
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
			// btcpfcommit
			// 
			this.btcpfcommit.AccessibleDescription = resources.GetString("btcpfcommit.AccessibleDescription");
			this.btcpfcommit.AccessibleName = resources.GetString("btcpfcommit.AccessibleName");
			this.btcpfcommit.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btcpfcommit.Anchor")));
			this.btcpfcommit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btcpfcommit.BackgroundImage")));
			this.btcpfcommit.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btcpfcommit.Dock")));
			this.btcpfcommit.Enabled = ((bool)(resources.GetObject("btcpfcommit.Enabled")));
			this.btcpfcommit.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btcpfcommit.FlatStyle")));
			this.btcpfcommit.Font = ((System.Drawing.Font)(resources.GetObject("btcpfcommit.Font")));
			this.btcpfcommit.Image = ((System.Drawing.Image)(resources.GetObject("btcpfcommit.Image")));
			this.btcpfcommit.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btcpfcommit.ImageAlign")));
			this.btcpfcommit.ImageIndex = ((int)(resources.GetObject("btcpfcommit.ImageIndex")));
			this.btcpfcommit.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btcpfcommit.ImeMode")));
			this.btcpfcommit.Location = ((System.Drawing.Point)(resources.GetObject("btcpfcommit.Location")));
			this.btcpfcommit.Name = "btcpfcommit";
			this.btcpfcommit.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btcpfcommit.RightToLeft")));
			this.btcpfcommit.Size = ((System.Drawing.Size)(resources.GetObject("btcpfcommit.Size")));
			this.btcpfcommit.TabIndex = ((int)(resources.GetObject("btcpfcommit.TabIndex")));
			this.btcpfcommit.Text = resources.GetString("btcpfcommit.Text");
			this.btcpfcommit.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btcpfcommit.TextAlign")));
			this.btcpfcommit.Visible = ((bool)(resources.GetObject("btcpfcommit.Visible")));
			this.btcpfcommit.Click += new System.EventHandler(this.CpfCommit);
			// 
			// lbcpf
			// 
			this.lbcpf.AccessibleDescription = resources.GetString("lbcpf.AccessibleDescription");
			this.lbcpf.AccessibleName = resources.GetString("lbcpf.AccessibleName");
			this.lbcpf.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbcpf.Anchor")));
			this.lbcpf.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lbcpf.BackgroundImage")));
			this.lbcpf.ColumnWidth = ((int)(resources.GetObject("lbcpf.ColumnWidth")));
			this.lbcpf.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbcpf.Dock")));
			this.lbcpf.Enabled = ((bool)(resources.GetObject("lbcpf.Enabled")));
			this.lbcpf.Font = ((System.Drawing.Font)(resources.GetObject("lbcpf.Font")));
			this.lbcpf.HorizontalExtent = ((int)(resources.GetObject("lbcpf.HorizontalExtent")));
			this.lbcpf.HorizontalScrollbar = ((bool)(resources.GetObject("lbcpf.HorizontalScrollbar")));
			this.lbcpf.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbcpf.ImeMode")));
			this.lbcpf.IntegralHeight = ((bool)(resources.GetObject("lbcpf.IntegralHeight")));
			this.lbcpf.ItemHeight = ((int)(resources.GetObject("lbcpf.ItemHeight")));
			this.lbcpf.Location = ((System.Drawing.Point)(resources.GetObject("lbcpf.Location")));
			this.lbcpf.Name = "lbcpf";
			this.lbcpf.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbcpf.RightToLeft")));
			this.lbcpf.ScrollAlwaysVisible = ((bool)(resources.GetObject("lbcpf.ScrollAlwaysVisible")));
			this.lbcpf.Size = ((System.Drawing.Size)(resources.GetObject("lbcpf.Size")));
			this.lbcpf.TabIndex = ((int)(resources.GetObject("lbcpf.TabIndex")));
			this.lbcpf.Visible = ((bool)(resources.GetObject("lbcpf.Visible")));
			this.lbcpf.SelectedIndexChanged += new System.EventHandler(this.CpfItemSelect);
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
			this.panel5.Controls.Add(this.label5);
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
			// CpfForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.cpfPanel);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "CpfForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.cpfPanel.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void CpfItemSelect(object sender, System.EventArgs e)
		{
			if (rtbcpfname.Tag!=null) return;
			this.llcpfchange.Enabled = false;
			if (this.lbcpf.SelectedIndex<0) return;
			this.llcpfchange.Enabled = true;

			rtbcpfname.Tag = true;
			try 
			{
				CpfItem item = (CpfItem)lbcpf.Items[lbcpf.SelectedIndex];
				this.rtbcpfname.Text = item.Name;
				for(int i=0; i<cbtype.Items.Count; i++)
				{					
					cbtype.SelectedIndex = -1;
					Data.MetaData.DataTypes type = (Data.MetaData.DataTypes)cbtype.Items[i];
					if (type==item.Datatype) 
					{
						cbtype.SelectedIndex = i;
						break;
					}
				}

				switch (item.Datatype) 
				{
					case Data.MetaData.DataTypes.dtSingle: 
					{
						rtbcpf.Text = item.SingleValue.ToString();
						break;
					}
					case Data.MetaData.DataTypes.dtInteger:  
					{
						rtbcpf.Text = "0x"+Helper.HexString((uint)item.IntegerValue);
						break;
					}
					case Data.MetaData.DataTypes.dtUInteger:
					{
						rtbcpf.Text = "0x"+Helper.HexString((uint)item.UIntegerValue);
						break;
					}
					case Data.MetaData.DataTypes.dtBoolean: 
					{
						if (item.BooleanValue) rtbcpf.Text = "1";
						else rtbcpf.Text = "0";
						break;
					}
					default:
					{
						rtbcpf.Text = item.StringValue;
						break;
					}
				}
			}
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			} 
			finally 
			{
				rtbcpfname.Tag = null;
			}
		}

		private void CpfChange(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if (cbtype.SelectedIndex<0) cbtype.SelectedIndex = cbtype.Items.Count -1;
			CpfItem item;
			if (lbcpf.SelectedIndex<0) item = new CpfItem();
			else item = (CpfItem)lbcpf.Items[lbcpf.SelectedIndex];
			
			item.Name = rtbcpfname.Text;
			item.Datatype = (Data.MetaData.DataTypes)cbtype.Items[cbtype.SelectedIndex];

			switch (item.Datatype) 
			{
				case Data.MetaData.DataTypes.dtInteger:  
				{
					try 
					{
						item.IntegerValue = Convert.ToInt32(rtbcpf.Text, 16);
					} 
					catch (Exception) 
					{
						item.IntegerValue = 0;
					}
					break;
				}
				case Data.MetaData.DataTypes.dtUInteger:
				{
					try 
					{
						item.UIntegerValue = Convert.ToUInt32(rtbcpf.Text, 16);
					} 
					catch (Exception) 
					{
						item.UIntegerValue = 0;
					}
					break;
				}
				case Data.MetaData.DataTypes.dtSingle: 
				{
					try 
					{
						item.SingleValue = Convert.ToSingle(rtbcpf.Text);
					} 
					catch (Exception) 
					{
						item.SingleValue = 0;
					}
					break;
				}
				case Data.MetaData.DataTypes.dtBoolean: 
				{
					try 
					{
						item.BooleanValue = (Convert.ToByte(rtbcpf.Text)!=0);
					} 
					catch (Exception) 
					{
						item.BooleanValue = false;
					}
					break;
				}
				default: 
				{
					item.StringValue = rtbcpf.Text;
					break;
				}
			} //switch

			if (lbcpf.SelectedIndex<0) lbcpf.Items.Add(item);
			else lbcpf.Items[lbcpf.SelectedIndex] = item;

			if (wrapper!=null) wrapper.Changed = true;
		}

		private void AddCpf(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			lbcpf.SelectedIndex = -1;
			CpfChange(null, null);
			lbcpf.SelectedIndex = lbcpf.Items.Count-1;
			CpfUpdate();
		}

		private void CpfUpdate()
		{
			Cpf wrp = (Cpf)wrapper;	
				
			CpfItem[] items = new CpfItem[lbcpf.Items.Count];
			for (int i=0; i<items.Length; i++) items[i] = (CpfItem)lbcpf.Items[i];
			wrp.Items = items;
		}

		private void CpfCommit(object sender, System.EventArgs e)
		{
			try 
			{
				if (this.lbcpf.SelectedIndex>=0) CpfChange(null, null);
				CpfUpdate();
				Cpf wrp = (Cpf)wrapper;	

				wrp.SynchronizeUserData();
				MessageBox.Show(Localization.Manager.GetString("commited"));
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}
		}

		private void CpfAutoChange()
		{
			if (rtbcpfname.Tag!=null) return;
			if (lbcpf.SelectedIndex<0) return;
			rtbcpfname.Tag = true;
			try 
			{
				CpfChange(null, null);
			}
			finally
			{
				rtbcpfname.Tag = null;
			}
		}

		private void btprev_Click(object sender, System.EventArgs e)
		{
			if (fkt==null) return;
			try 
			{
				Cpf cpf = (Cpf)wrapper;
				fkt(cpf, cpf.Package);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			}
		}

		private void DeleteCpf(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			
			if (lbcpf.SelectedIndex<0) return;
			CpfItem item = (CpfItem)lbcpf.Items[lbcpf.SelectedIndex];
			lbcpf.Items.Remove(item);
			CpfUpdate();
			wrapper.Changed = true;
		}

		private void CpfAutoChange(object sender, System.EventArgs e)
		{
			CpfAutoChange();
		}
		
	}
}
