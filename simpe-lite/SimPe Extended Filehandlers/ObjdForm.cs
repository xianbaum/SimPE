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
	/// Zusammenfassung für Elements.
	/// </summary>
	public class ObjdForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables
		private System.Windows.Forms.Panel objdPanel;
		private System.Windows.Forms.CheckBox cbupdate;
		private System.Windows.Forms.Label label63;
		private System.Windows.Forms.TextBox tbproxguid;
		private System.Windows.Forms.Label label97;
		private System.Windows.Forms.TextBox tborgguid;
		private System.Windows.Forms.Label lbtypename;
		private System.Windows.Forms.LinkLabel llgetGUID;
		private System.Windows.Forms.GroupBox gbelements;
		private System.Windows.Forms.Panel pnelements;
		private System.Windows.Forms.Label label65;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox tbObjType;
		private System.Windows.Forms.TextBox tbFilename;
		private System.Windows.Forms.TextBox tbGUID;
		private System.Windows.Forms.LinkLabel llCommit;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public ObjdForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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


		#region ObjdForm
		private Objd wrapper;
		private bool systentextupdate = false;
		#endregion

		#region IPackedFileUI Member
		/// <summary>
		/// Returns the Panel that will be displayed within SimPe
		/// </summary>
		public System.Windows.Forms.Panel GUIHandle
		{
			get 
			{
				return objdPanel;
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
			wrapper = (Objd)wrp;

			Hashtable list = wrapper.Attributes;
			pnelements.Controls.Clear();

			int top = 4;
			ArrayList keys = new ArrayList();
			foreach (string k in list.Keys) keys.Add("0x"+Helper.HexString((ushort)wrapper.GetAttributePosition(k))+": "+k);
			keys.Sort();

			foreach (string k in keys)
			{
				string[] s = k.Split(":".ToCharArray(), 2);
				Label lb = new Label();
				lb.Parent = pnelements;
				lb.AutoSize = true;
				lb.Text = k+" = ";
				lb.Top = top;
				lb.Visible = true;

				TextBox tb = new TextBox();
				tb.BorderStyle = BorderStyle.None;
				tb.Parent = pnelements;
				tb.Left = lb.Left + lb.Width;
				tb.Top = lb.Bottom - tb.Height;
				tb.Width = 50;
				tb.Text = "0x"+Helper.HexString(wrapper.GetAttributeShort(s[1].Trim()));
				tb.Tag = s[1].Trim();
				tb.Visible = true;				
				tb.TextChanged += new EventHandler(HexTextChanged);

				TextBox tb2 = new TextBox();
				tb2.BorderStyle = BorderStyle.None;
				tb2.Parent = pnelements;
				tb2.Left = tb.Left + tb.Width + 4;
				tb2.Top = lb.Bottom - tb.Height;
				tb2.Width = 100;
				tb2.Text = ((short)wrapper.GetAttributeShort(s[1].Trim())).ToString();
				tb2.Tag = null;
				tb2.Visible = true;
				tb2.TextChanged += new EventHandler(DecTextChanged);
				

				top += Math.Max(lb.Height, tb.Height) + 2;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ObjdForm));
			this.objdPanel = new System.Windows.Forms.Panel();
			this.cbupdate = new System.Windows.Forms.CheckBox();
			this.label63 = new System.Windows.Forms.Label();
			this.tbproxguid = new System.Windows.Forms.TextBox();
			this.label97 = new System.Windows.Forms.Label();
			this.tborgguid = new System.Windows.Forms.TextBox();
			this.lbtypename = new System.Windows.Forms.Label();
			this.llgetGUID = new System.Windows.Forms.LinkLabel();
			this.gbelements = new System.Windows.Forms.GroupBox();
			this.pnelements = new System.Windows.Forms.Panel();
			this.llCommit = new System.Windows.Forms.LinkLabel();
			this.tbObjType = new System.Windows.Forms.TextBox();
			this.label65 = new System.Windows.Forms.Label();
			this.tbFilename = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.tbGUID = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.panel6 = new System.Windows.Forms.Panel();
			this.label12 = new System.Windows.Forms.Label();
			this.objdPanel.SuspendLayout();
			this.gbelements.SuspendLayout();
			this.panel6.SuspendLayout();
			this.SuspendLayout();
			// 
			// objdPanel
			// 
			this.objdPanel.AccessibleDescription = resources.GetString("objdPanel.AccessibleDescription");
			this.objdPanel.AccessibleName = resources.GetString("objdPanel.AccessibleName");
			this.objdPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("objdPanel.Anchor")));
			this.objdPanel.AutoScroll = ((bool)(resources.GetObject("objdPanel.AutoScroll")));
			this.objdPanel.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("objdPanel.AutoScrollMargin")));
			this.objdPanel.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("objdPanel.AutoScrollMinSize")));
			this.objdPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("objdPanel.BackgroundImage")));
			this.objdPanel.Controls.Add(this.cbupdate);
			this.objdPanel.Controls.Add(this.label63);
			this.objdPanel.Controls.Add(this.tbproxguid);
			this.objdPanel.Controls.Add(this.label97);
			this.objdPanel.Controls.Add(this.tborgguid);
			this.objdPanel.Controls.Add(this.lbtypename);
			this.objdPanel.Controls.Add(this.llgetGUID);
			this.objdPanel.Controls.Add(this.gbelements);
			this.objdPanel.Controls.Add(this.llCommit);
			this.objdPanel.Controls.Add(this.tbObjType);
			this.objdPanel.Controls.Add(this.label65);
			this.objdPanel.Controls.Add(this.tbFilename);
			this.objdPanel.Controls.Add(this.label9);
			this.objdPanel.Controls.Add(this.tbGUID);
			this.objdPanel.Controls.Add(this.label8);
			this.objdPanel.Controls.Add(this.panel6);
			this.objdPanel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("objdPanel.Dock")));
			this.objdPanel.Enabled = ((bool)(resources.GetObject("objdPanel.Enabled")));
			this.objdPanel.Font = ((System.Drawing.Font)(resources.GetObject("objdPanel.Font")));
			this.objdPanel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("objdPanel.ImeMode")));
			this.objdPanel.Location = ((System.Drawing.Point)(resources.GetObject("objdPanel.Location")));
			this.objdPanel.Name = "objdPanel";
			this.objdPanel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("objdPanel.RightToLeft")));
			this.objdPanel.Size = ((System.Drawing.Size)(resources.GetObject("objdPanel.Size")));
			this.objdPanel.TabIndex = ((int)(resources.GetObject("objdPanel.TabIndex")));
			this.objdPanel.Text = resources.GetString("objdPanel.Text");
			this.objdPanel.Visible = ((bool)(resources.GetObject("objdPanel.Visible")));
			// 
			// cbupdate
			// 
			this.cbupdate.AccessibleDescription = resources.GetString("cbupdate.AccessibleDescription");
			this.cbupdate.AccessibleName = resources.GetString("cbupdate.AccessibleName");
			this.cbupdate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbupdate.Anchor")));
			this.cbupdate.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbupdate.Appearance")));
			this.cbupdate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbupdate.BackgroundImage")));
			this.cbupdate.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbupdate.CheckAlign")));
			this.cbupdate.Checked = true;
			this.cbupdate.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbupdate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbupdate.Dock")));
			this.cbupdate.Enabled = ((bool)(resources.GetObject("cbupdate.Enabled")));
			this.cbupdate.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbupdate.FlatStyle")));
			this.cbupdate.Font = ((System.Drawing.Font)(resources.GetObject("cbupdate.Font")));
			this.cbupdate.Image = ((System.Drawing.Image)(resources.GetObject("cbupdate.Image")));
			this.cbupdate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbupdate.ImageAlign")));
			this.cbupdate.ImageIndex = ((int)(resources.GetObject("cbupdate.ImageIndex")));
			this.cbupdate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbupdate.ImeMode")));
			this.cbupdate.Location = ((System.Drawing.Point)(resources.GetObject("cbupdate.Location")));
			this.cbupdate.Name = "cbupdate";
			this.cbupdate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbupdate.RightToLeft")));
			this.cbupdate.Size = ((System.Drawing.Size)(resources.GetObject("cbupdate.Size")));
			this.cbupdate.TabIndex = ((int)(resources.GetObject("cbupdate.TabIndex")));
			this.cbupdate.Text = resources.GetString("cbupdate.Text");
			this.cbupdate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbupdate.TextAlign")));
			this.cbupdate.Visible = ((bool)(resources.GetObject("cbupdate.Visible")));
			// 
			// label63
			// 
			this.label63.AccessibleDescription = resources.GetString("label63.AccessibleDescription");
			this.label63.AccessibleName = resources.GetString("label63.AccessibleName");
			this.label63.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label63.Anchor")));
			this.label63.AutoSize = ((bool)(resources.GetObject("label63.AutoSize")));
			this.label63.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label63.Dock")));
			this.label63.Enabled = ((bool)(resources.GetObject("label63.Enabled")));
			this.label63.Font = ((System.Drawing.Font)(resources.GetObject("label63.Font")));
			this.label63.Image = ((System.Drawing.Image)(resources.GetObject("label63.Image")));
			this.label63.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label63.ImageAlign")));
			this.label63.ImageIndex = ((int)(resources.GetObject("label63.ImageIndex")));
			this.label63.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label63.ImeMode")));
			this.label63.Location = ((System.Drawing.Point)(resources.GetObject("label63.Location")));
			this.label63.Name = "label63";
			this.label63.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label63.RightToLeft")));
			this.label63.Size = ((System.Drawing.Size)(resources.GetObject("label63.Size")));
			this.label63.TabIndex = ((int)(resources.GetObject("label63.TabIndex")));
			this.label63.Text = resources.GetString("label63.Text");
			this.label63.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label63.TextAlign")));
			this.label63.Visible = ((bool)(resources.GetObject("label63.Visible")));
			// 
			// tbproxguid
			// 
			this.tbproxguid.AccessibleDescription = resources.GetString("tbproxguid.AccessibleDescription");
			this.tbproxguid.AccessibleName = resources.GetString("tbproxguid.AccessibleName");
			this.tbproxguid.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbproxguid.Anchor")));
			this.tbproxguid.AutoSize = ((bool)(resources.GetObject("tbproxguid.AutoSize")));
			this.tbproxguid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbproxguid.BackgroundImage")));
			this.tbproxguid.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbproxguid.Dock")));
			this.tbproxguid.Enabled = ((bool)(resources.GetObject("tbproxguid.Enabled")));
			this.tbproxguid.Font = ((System.Drawing.Font)(resources.GetObject("tbproxguid.Font")));
			this.tbproxguid.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbproxguid.ImeMode")));
			this.tbproxguid.Location = ((System.Drawing.Point)(resources.GetObject("tbproxguid.Location")));
			this.tbproxguid.MaxLength = ((int)(resources.GetObject("tbproxguid.MaxLength")));
			this.tbproxguid.Multiline = ((bool)(resources.GetObject("tbproxguid.Multiline")));
			this.tbproxguid.Name = "tbproxguid";
			this.tbproxguid.PasswordChar = ((char)(resources.GetObject("tbproxguid.PasswordChar")));
			this.tbproxguid.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbproxguid.RightToLeft")));
			this.tbproxguid.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbproxguid.ScrollBars")));
			this.tbproxguid.Size = ((System.Drawing.Size)(resources.GetObject("tbproxguid.Size")));
			this.tbproxguid.TabIndex = ((int)(resources.GetObject("tbproxguid.TabIndex")));
			this.tbproxguid.Text = resources.GetString("tbproxguid.Text");
			this.tbproxguid.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbproxguid.TextAlign")));
			this.tbproxguid.Visible = ((bool)(resources.GetObject("tbproxguid.Visible")));
			this.tbproxguid.WordWrap = ((bool)(resources.GetObject("tbproxguid.WordWrap")));
			// 
			// label97
			// 
			this.label97.AccessibleDescription = resources.GetString("label97.AccessibleDescription");
			this.label97.AccessibleName = resources.GetString("label97.AccessibleName");
			this.label97.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label97.Anchor")));
			this.label97.AutoSize = ((bool)(resources.GetObject("label97.AutoSize")));
			this.label97.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label97.Dock")));
			this.label97.Enabled = ((bool)(resources.GetObject("label97.Enabled")));
			this.label97.Font = ((System.Drawing.Font)(resources.GetObject("label97.Font")));
			this.label97.Image = ((System.Drawing.Image)(resources.GetObject("label97.Image")));
			this.label97.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label97.ImageAlign")));
			this.label97.ImageIndex = ((int)(resources.GetObject("label97.ImageIndex")));
			this.label97.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label97.ImeMode")));
			this.label97.Location = ((System.Drawing.Point)(resources.GetObject("label97.Location")));
			this.label97.Name = "label97";
			this.label97.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label97.RightToLeft")));
			this.label97.Size = ((System.Drawing.Size)(resources.GetObject("label97.Size")));
			this.label97.TabIndex = ((int)(resources.GetObject("label97.TabIndex")));
			this.label97.Text = resources.GetString("label97.Text");
			this.label97.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label97.TextAlign")));
			this.label97.Visible = ((bool)(resources.GetObject("label97.Visible")));
			// 
			// tborgguid
			// 
			this.tborgguid.AccessibleDescription = resources.GetString("tborgguid.AccessibleDescription");
			this.tborgguid.AccessibleName = resources.GetString("tborgguid.AccessibleName");
			this.tborgguid.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tborgguid.Anchor")));
			this.tborgguid.AutoSize = ((bool)(resources.GetObject("tborgguid.AutoSize")));
			this.tborgguid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tborgguid.BackgroundImage")));
			this.tborgguid.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tborgguid.Dock")));
			this.tborgguid.Enabled = ((bool)(resources.GetObject("tborgguid.Enabled")));
			this.tborgguid.Font = ((System.Drawing.Font)(resources.GetObject("tborgguid.Font")));
			this.tborgguid.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tborgguid.ImeMode")));
			this.tborgguid.Location = ((System.Drawing.Point)(resources.GetObject("tborgguid.Location")));
			this.tborgguid.MaxLength = ((int)(resources.GetObject("tborgguid.MaxLength")));
			this.tborgguid.Multiline = ((bool)(resources.GetObject("tborgguid.Multiline")));
			this.tborgguid.Name = "tborgguid";
			this.tborgguid.PasswordChar = ((char)(resources.GetObject("tborgguid.PasswordChar")));
			this.tborgguid.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tborgguid.RightToLeft")));
			this.tborgguid.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tborgguid.ScrollBars")));
			this.tborgguid.Size = ((System.Drawing.Size)(resources.GetObject("tborgguid.Size")));
			this.tborgguid.TabIndex = ((int)(resources.GetObject("tborgguid.TabIndex")));
			this.tborgguid.Text = resources.GetString("tborgguid.Text");
			this.tborgguid.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tborgguid.TextAlign")));
			this.tborgguid.Visible = ((bool)(resources.GetObject("tborgguid.Visible")));
			this.tborgguid.WordWrap = ((bool)(resources.GetObject("tborgguid.WordWrap")));
			// 
			// lbtypename
			// 
			this.lbtypename.AccessibleDescription = resources.GetString("lbtypename.AccessibleDescription");
			this.lbtypename.AccessibleName = resources.GetString("lbtypename.AccessibleName");
			this.lbtypename.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbtypename.Anchor")));
			this.lbtypename.AutoSize = ((bool)(resources.GetObject("lbtypename.AutoSize")));
			this.lbtypename.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbtypename.Dock")));
			this.lbtypename.Enabled = ((bool)(resources.GetObject("lbtypename.Enabled")));
			this.lbtypename.Font = ((System.Drawing.Font)(resources.GetObject("lbtypename.Font")));
			this.lbtypename.Image = ((System.Drawing.Image)(resources.GetObject("lbtypename.Image")));
			this.lbtypename.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbtypename.ImageAlign")));
			this.lbtypename.ImageIndex = ((int)(resources.GetObject("lbtypename.ImageIndex")));
			this.lbtypename.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbtypename.ImeMode")));
			this.lbtypename.Location = ((System.Drawing.Point)(resources.GetObject("lbtypename.Location")));
			this.lbtypename.Name = "lbtypename";
			this.lbtypename.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbtypename.RightToLeft")));
			this.lbtypename.Size = ((System.Drawing.Size)(resources.GetObject("lbtypename.Size")));
			this.lbtypename.TabIndex = ((int)(resources.GetObject("lbtypename.TabIndex")));
			this.lbtypename.Text = resources.GetString("lbtypename.Text");
			this.lbtypename.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbtypename.TextAlign")));
			this.lbtypename.Visible = ((bool)(resources.GetObject("lbtypename.Visible")));
			// 
			// llgetGUID
			// 
			this.llgetGUID.AccessibleDescription = resources.GetString("llgetGUID.AccessibleDescription");
			this.llgetGUID.AccessibleName = resources.GetString("llgetGUID.AccessibleName");
			this.llgetGUID.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llgetGUID.Anchor")));
			this.llgetGUID.AutoSize = ((bool)(resources.GetObject("llgetGUID.AutoSize")));
			this.llgetGUID.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llgetGUID.Dock")));
			this.llgetGUID.Enabled = ((bool)(resources.GetObject("llgetGUID.Enabled")));
			this.llgetGUID.Font = ((System.Drawing.Font)(resources.GetObject("llgetGUID.Font")));
			this.llgetGUID.Image = ((System.Drawing.Image)(resources.GetObject("llgetGUID.Image")));
			this.llgetGUID.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llgetGUID.ImageAlign")));
			this.llgetGUID.ImageIndex = ((int)(resources.GetObject("llgetGUID.ImageIndex")));
			this.llgetGUID.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llgetGUID.ImeMode")));
			this.llgetGUID.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llgetGUID.LinkArea")));
			this.llgetGUID.Location = ((System.Drawing.Point)(resources.GetObject("llgetGUID.Location")));
			this.llgetGUID.Name = "llgetGUID";
			this.llgetGUID.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llgetGUID.RightToLeft")));
			this.llgetGUID.Size = ((System.Drawing.Size)(resources.GetObject("llgetGUID.Size")));
			this.llgetGUID.TabIndex = ((int)(resources.GetObject("llgetGUID.TabIndex")));
			this.llgetGUID.TabStop = true;
			this.llgetGUID.Text = resources.GetString("llgetGUID.Text");
			this.llgetGUID.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llgetGUID.TextAlign")));
			this.llgetGUID.Visible = ((bool)(resources.GetObject("llgetGUID.Visible")));
			this.llgetGUID.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GetGUIDClicked);
			// 
			// gbelements
			// 
			this.gbelements.AccessibleDescription = resources.GetString("gbelements.AccessibleDescription");
			this.gbelements.AccessibleName = resources.GetString("gbelements.AccessibleName");
			this.gbelements.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gbelements.Anchor")));
			this.gbelements.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gbelements.BackgroundImage")));
			this.gbelements.Controls.Add(this.pnelements);
			this.gbelements.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("gbelements.Dock")));
			this.gbelements.Enabled = ((bool)(resources.GetObject("gbelements.Enabled")));
			this.gbelements.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.gbelements.Font = ((System.Drawing.Font)(resources.GetObject("gbelements.Font")));
			this.gbelements.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gbelements.ImeMode")));
			this.gbelements.Location = ((System.Drawing.Point)(resources.GetObject("gbelements.Location")));
			this.gbelements.Name = "gbelements";
			this.gbelements.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("gbelements.RightToLeft")));
			this.gbelements.Size = ((System.Drawing.Size)(resources.GetObject("gbelements.Size")));
			this.gbelements.TabIndex = ((int)(resources.GetObject("gbelements.TabIndex")));
			this.gbelements.TabStop = false;
			this.gbelements.Text = resources.GetString("gbelements.Text");
			this.gbelements.Visible = ((bool)(resources.GetObject("gbelements.Visible")));
			// 
			// pnelements
			// 
			this.pnelements.AccessibleDescription = resources.GetString("pnelements.AccessibleDescription");
			this.pnelements.AccessibleName = resources.GetString("pnelements.AccessibleName");
			this.pnelements.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pnelements.Anchor")));
			this.pnelements.AutoScroll = ((bool)(resources.GetObject("pnelements.AutoScroll")));
			this.pnelements.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("pnelements.AutoScrollMargin")));
			this.pnelements.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("pnelements.AutoScrollMinSize")));
			this.pnelements.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnelements.BackgroundImage")));
			this.pnelements.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnelements.Dock")));
			this.pnelements.Enabled = ((bool)(resources.GetObject("pnelements.Enabled")));
			this.pnelements.Font = ((System.Drawing.Font)(resources.GetObject("pnelements.Font")));
			this.pnelements.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnelements.ImeMode")));
			this.pnelements.Location = ((System.Drawing.Point)(resources.GetObject("pnelements.Location")));
			this.pnelements.Name = "pnelements";
			this.pnelements.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnelements.RightToLeft")));
			this.pnelements.Size = ((System.Drawing.Size)(resources.GetObject("pnelements.Size")));
			this.pnelements.TabIndex = ((int)(resources.GetObject("pnelements.TabIndex")));
			this.pnelements.Text = resources.GetString("pnelements.Text");
			this.pnelements.Visible = ((bool)(resources.GetObject("pnelements.Visible")));
			// 
			// llCommit
			// 
			this.llCommit.AccessibleDescription = resources.GetString("llCommit.AccessibleDescription");
			this.llCommit.AccessibleName = resources.GetString("llCommit.AccessibleName");
			this.llCommit.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llCommit.Anchor")));
			this.llCommit.AutoSize = ((bool)(resources.GetObject("llCommit.AutoSize")));
			this.llCommit.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llCommit.Dock")));
			this.llCommit.Enabled = ((bool)(resources.GetObject("llCommit.Enabled")));
			this.llCommit.Font = ((System.Drawing.Font)(resources.GetObject("llCommit.Font")));
			this.llCommit.Image = ((System.Drawing.Image)(resources.GetObject("llCommit.Image")));
			this.llCommit.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llCommit.ImageAlign")));
			this.llCommit.ImageIndex = ((int)(resources.GetObject("llCommit.ImageIndex")));
			this.llCommit.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llCommit.ImeMode")));
			this.llCommit.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llCommit.LinkArea")));
			this.llCommit.Location = ((System.Drawing.Point)(resources.GetObject("llCommit.Location")));
			this.llCommit.Name = "llCommit";
			this.llCommit.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llCommit.RightToLeft")));
			this.llCommit.Size = ((System.Drawing.Size)(resources.GetObject("llCommit.Size")));
			this.llCommit.TabIndex = ((int)(resources.GetObject("llCommit.TabIndex")));
			this.llCommit.TabStop = true;
			this.llCommit.Text = resources.GetString("llCommit.Text");
			this.llCommit.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llCommit.TextAlign")));
			this.llCommit.Visible = ((bool)(resources.GetObject("llCommit.Visible")));
			this.llCommit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CommitObjdClicked);
			// 
			// tbObjType
			// 
			this.tbObjType.AccessibleDescription = resources.GetString("tbObjType.AccessibleDescription");
			this.tbObjType.AccessibleName = resources.GetString("tbObjType.AccessibleName");
			this.tbObjType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbObjType.Anchor")));
			this.tbObjType.AutoSize = ((bool)(resources.GetObject("tbObjType.AutoSize")));
			this.tbObjType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbObjType.BackgroundImage")));
			this.tbObjType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbObjType.Dock")));
			this.tbObjType.Enabled = ((bool)(resources.GetObject("tbObjType.Enabled")));
			this.tbObjType.Font = ((System.Drawing.Font)(resources.GetObject("tbObjType.Font")));
			this.tbObjType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbObjType.ImeMode")));
			this.tbObjType.Location = ((System.Drawing.Point)(resources.GetObject("tbObjType.Location")));
			this.tbObjType.MaxLength = ((int)(resources.GetObject("tbObjType.MaxLength")));
			this.tbObjType.Multiline = ((bool)(resources.GetObject("tbObjType.Multiline")));
			this.tbObjType.Name = "tbObjType";
			this.tbObjType.PasswordChar = ((char)(resources.GetObject("tbObjType.PasswordChar")));
			this.tbObjType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbObjType.RightToLeft")));
			this.tbObjType.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbObjType.ScrollBars")));
			this.tbObjType.Size = ((System.Drawing.Size)(resources.GetObject("tbObjType.Size")));
			this.tbObjType.TabIndex = ((int)(resources.GetObject("tbObjType.TabIndex")));
			this.tbObjType.Text = resources.GetString("tbObjType.Text");
			this.tbObjType.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbObjType.TextAlign")));
			this.tbObjType.Visible = ((bool)(resources.GetObject("tbObjType.Visible")));
			this.tbObjType.WordWrap = ((bool)(resources.GetObject("tbObjType.WordWrap")));
			// 
			// label65
			// 
			this.label65.AccessibleDescription = resources.GetString("label65.AccessibleDescription");
			this.label65.AccessibleName = resources.GetString("label65.AccessibleName");
			this.label65.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label65.Anchor")));
			this.label65.AutoSize = ((bool)(resources.GetObject("label65.AutoSize")));
			this.label65.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label65.Dock")));
			this.label65.Enabled = ((bool)(resources.GetObject("label65.Enabled")));
			this.label65.Font = ((System.Drawing.Font)(resources.GetObject("label65.Font")));
			this.label65.Image = ((System.Drawing.Image)(resources.GetObject("label65.Image")));
			this.label65.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label65.ImageAlign")));
			this.label65.ImageIndex = ((int)(resources.GetObject("label65.ImageIndex")));
			this.label65.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label65.ImeMode")));
			this.label65.Location = ((System.Drawing.Point)(resources.GetObject("label65.Location")));
			this.label65.Name = "label65";
			this.label65.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label65.RightToLeft")));
			this.label65.Size = ((System.Drawing.Size)(resources.GetObject("label65.Size")));
			this.label65.TabIndex = ((int)(resources.GetObject("label65.TabIndex")));
			this.label65.Text = resources.GetString("label65.Text");
			this.label65.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label65.TextAlign")));
			this.label65.Visible = ((bool)(resources.GetObject("label65.Visible")));
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
			// tbGUID
			// 
			this.tbGUID.AccessibleDescription = resources.GetString("tbGUID.AccessibleDescription");
			this.tbGUID.AccessibleName = resources.GetString("tbGUID.AccessibleName");
			this.tbGUID.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbGUID.Anchor")));
			this.tbGUID.AutoSize = ((bool)(resources.GetObject("tbGUID.AutoSize")));
			this.tbGUID.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbGUID.BackgroundImage")));
			this.tbGUID.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbGUID.Dock")));
			this.tbGUID.Enabled = ((bool)(resources.GetObject("tbGUID.Enabled")));
			this.tbGUID.Font = ((System.Drawing.Font)(resources.GetObject("tbGUID.Font")));
			this.tbGUID.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbGUID.ImeMode")));
			this.tbGUID.Location = ((System.Drawing.Point)(resources.GetObject("tbGUID.Location")));
			this.tbGUID.MaxLength = ((int)(resources.GetObject("tbGUID.MaxLength")));
			this.tbGUID.Multiline = ((bool)(resources.GetObject("tbGUID.Multiline")));
			this.tbGUID.Name = "tbGUID";
			this.tbGUID.PasswordChar = ((char)(resources.GetObject("tbGUID.PasswordChar")));
			this.tbGUID.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbGUID.RightToLeft")));
			this.tbGUID.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbGUID.ScrollBars")));
			this.tbGUID.Size = ((System.Drawing.Size)(resources.GetObject("tbGUID.Size")));
			this.tbGUID.TabIndex = ((int)(resources.GetObject("tbGUID.TabIndex")));
			this.tbGUID.Text = resources.GetString("tbGUID.Text");
			this.tbGUID.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbGUID.TextAlign")));
			this.tbGUID.Visible = ((bool)(resources.GetObject("tbGUID.Visible")));
			this.tbGUID.WordWrap = ((bool)(resources.GetObject("tbGUID.WordWrap")));
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
			// panel6
			// 
			this.panel6.AccessibleDescription = resources.GetString("panel6.AccessibleDescription");
			this.panel6.AccessibleName = resources.GetString("panel6.AccessibleName");
			this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel6.Anchor")));
			this.panel6.AutoScroll = ((bool)(resources.GetObject("panel6.AutoScroll")));
			this.panel6.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel6.AutoScrollMargin")));
			this.panel6.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel6.AutoScrollMinSize")));
			this.panel6.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.panel6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel6.BackgroundImage")));
			this.panel6.Controls.Add(this.label12);
			this.panel6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panel6.Dock")));
			this.panel6.Enabled = ((bool)(resources.GetObject("panel6.Enabled")));
			this.panel6.Font = ((System.Drawing.Font)(resources.GetObject("panel6.Font")));
			this.panel6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.panel6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panel6.ImeMode")));
			this.panel6.Location = ((System.Drawing.Point)(resources.GetObject("panel6.Location")));
			this.panel6.Name = "panel6";
			this.panel6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panel6.RightToLeft")));
			this.panel6.Size = ((System.Drawing.Size)(resources.GetObject("panel6.Size")));
			this.panel6.TabIndex = ((int)(resources.GetObject("panel6.TabIndex")));
			this.panel6.Text = resources.GetString("panel6.Text");
			this.panel6.Visible = ((bool)(resources.GetObject("panel6.Visible")));
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
			// ObjdForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.objdPanel);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "ObjdForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.objdPanel.ResumeLayout(false);
			this.gbelements.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void CommitObjdClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if (wrapper!=null) 
			{
				try 
				{
					this.Cursor = Cursors.WaitCursor;
					SimPe.PackedFiles.Wrapper.Objd objd = (Wrapper.Objd)wrapper;					

					foreach (Control c in pnelements.Controls) 
					{
						if (c.GetType() == typeof(TextBox)) 
						{
							TextBox tb = (TextBox)c;
							if (tb.Tag!=null) 
							{
								string name = (string)tb.Tag;
								Wrapper.ObjdItem item = (Wrapper.ObjdItem)objd.Attributes[name];
								item.val = Convert.ToUInt16(tb.Text, 16);
								objd.Attributes[name] = item;
							}
						}
					}

					objd.Type = (ushort)Helper.HexStringToUInt(tbObjType.Text);
					objd.Guid = (uint)Helper.HexStringToUInt(tbGUID.Text);
					objd.FileName = tbFilename.Text;
					objd.OriginalGuid = (uint)Helper.HexStringToUInt(this.tborgguid.Text);
					objd.ProxyGuid = (uint)Helper.HexStringToUInt(this.tbproxguid.Text);

					objd.SynchronizeUserData();
					MessageBox.Show(Localization.Manager.GetString("commited"));
				} 
				catch (Exception ex) 
				{
					Helper.ExceptionMessage(Localization.Manager.GetString("cantcommitobjd"), ex);
				}
			}
		}

		private void GetGUIDClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Sims.GUID.GUIDGetterForm form = new Sims.GUID.GUIDGetterForm();
			Registry reg = new Registry();

			try 
			{
				uint oldguid = Convert.ToUInt32(tbGUID.Text, 16);
				uint guid = form.GetNewGUID(reg.Username, reg.Password, oldguid);

				reg.Username = form.tbusername.Text;
				reg.Password = form.tbpassword.Text;
				this.tbGUID.Text = "0x"+Helper.HexString(guid);

				if (cbupdate.Checked) 
				{
					SimPe.Plugin.FixGuid fg = new SimPe.Plugin.FixGuid(((SimPe.PackedFiles.Wrapper.Objd)wrapper).Package);
					ArrayList al = new ArrayList();
					SimPe.Plugin.GuidSet gs = new SimPe.Plugin.GuidSet();
					gs.oldguid = oldguid;
					gs.guid = guid;
					al.Add(gs);

					fg.FixGuids(al);

					((SimPe.PackedFiles.Wrapper.Objd)wrapper).Guid = guid;
					wrapper.SynchronizeUserData();
				}
			} 
			catch (Exception) {}
		}

		/// <summary>
		/// Updates the Decimal View
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HexTextChanged(object sender, EventArgs e)
		{
			if (systentextupdate) return;
			systentextupdate = true;
			TextBox tb = (TextBox) sender;

			foreach (Control c in tb.Parent.Controls) 
			{
				if (c.GetType()==typeof(TextBox)) 
				{
					TextBox tb2 = (TextBox)c;
					if ((tb2.Top == tb.Top) && (tb2!=tb))
					{
						try 
						{
							tb2.Text = Convert.ToInt16(tb.Text, 16).ToString();
						} 
						catch (Exception) {}
						break;
					}
				}
			} //foreach
			systentextupdate = false;
		}

		/// <summary>
		/// Updates the Hex View
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DecTextChanged(object sender, EventArgs e)
		{
			if (systentextupdate) return;
			systentextupdate = true;
			TextBox tb = (TextBox) sender;

			foreach (Control c in tb.Parent.Controls) 
			{
				if (c.GetType()==typeof(TextBox)) 
				{
					TextBox tb2 = (TextBox)c;
					if ((tb2.Top == tb.Top) && (tb2!=tb))
					{
						try 
						{
							tb2.Text = "0x"+Helper.HexString((ushort)Convert.ToInt16(tb.Text));
						} 
						catch (Exception) {}
						break;
					}
				}
			} //foreach
			systentextupdate = false;
		}
	}
}
