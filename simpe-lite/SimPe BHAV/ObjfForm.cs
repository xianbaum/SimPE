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
	/// Summary description for BconForm.
	/// </summary>
	public class ObjfForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables

		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.Panel objfPanel;
		private System.Windows.Forms.Label lbFilename;
		private System.Windows.Forms.TextBox tbFilename;
		private System.Windows.Forms.LinkLabel llGuardian;
		private System.Windows.Forms.LinkLabel llAction;
		private System.Windows.Forms.Button btnAction;
		private System.Windows.Forms.Button btnGuardian;
		private System.Windows.Forms.TextBox tbGuardian;
		private System.Windows.Forms.TextBox tbAction;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.Panel pnHeading;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lbAction;
		private System.Windows.Forms.Label lbGuardian;
		private System.Windows.Forms.ListView lvObjfItem;
		private System.Windows.Forms.ColumnHeader chFunction;
		private System.Windows.Forms.ColumnHeader chGuardian;
		private System.Windows.Forms.ColumnHeader chAction;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
       
		public ObjfForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			TextBox[] tbua = { tbAction, tbGuardian };
			alHex16 = new ArrayList(tbua);

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

		
		#region ObjfForm
		/// <summary>
		/// Stores the currently active Wrapper
		/// </summary>
		private Objf wrapper = null;
		private bool internalchg;
		private bool setHandler = false;
		private ArrayList alHex16;
		private ObjfItem origItem;
		private ObjfItem currentItem;

		private bool hex16_IsValid(object sender)
		{
			if (alHex16.IndexOf(sender) < 0)
				throw new Exception("hex16_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToUInt16(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private void setBHAV(int which, ushort target, bool notxt)
		{
			TextBox[] tbaGA = { tbAction, tbGuardian };
			Label[] lbaGA = { lbAction, lbGuardian };
			LinkLabel[] llaGA = { llAction, llGuardian };

			pjse.FileTable.Entry e = wrapper.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, target);
			if (!notxt) tbaGA[which].Text = "0x"+Helper.HexString(target);
			lbaGA[which].Text = (target == 0 || e == null) ? "---" : e;
			llaGA[which].Enabled = (e != null);
			this.lvObjfItem.SelectedItems[0].SubItems[1 + which].Text = (target == 0) ? "---" : tbaGA[which].Text;
		}

		#endregion

		#region IPackedFileUI Member
		/// <summary>
		/// Returns the Panel that will be displayed within SimPe
		/// </summary>
		public System.Windows.Forms.Control GUIHandle
		{
			get
			{
				return objfPanel;
			}
		}

		/// <summary>
		/// Is called by SimPe (through the Wrapper) when the Panel is going to be displayed, so
		/// you should updatet the Data displayed by the Panel with the Attributes stored in the
		/// passed Wrapper.
		/// </summary>
		/// <remarks>internalchg is used to let TextChanged event handlers know the change is being
		/// made internally rather than by the users.</remarks>
		/// <param name="wrapper">The Attributes of this Wrapper have to be displayed</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (Objf)wrp;
			WrapperChanged(wrapper, null);

			internalchg = true;

			this.Text = tbFilename.Text = wrapper.FileName;

			this.lvObjfItem.Items.Clear();
			for(ushort i = 0; i < wrapper.Count; i++)
			{
				this.lvObjfItem.Items.Add( new ListViewItem(
					new string[] {
									 pjse.GS.GStr(pjse.GS.BhavStr.OBJFDescs, i)
									 , wrapper[i].Action   == 0 ? "---" : "0x" + Helper.HexString(wrapper[i].Action)
									 , wrapper[i].Guardian == 0 ? "---" : "0x" + Helper.HexString(wrapper[i].Guardian)
								 }
					) );
			}

			internalchg = false;

			lvObjfItem.Items[0].Selected = true;

			if (!setHandler)
			{
				wrapper.WrapperChanged += new System.EventHandler(this.WrapperChanged);
				setHandler = true;
			}

		}

		private void WrapperChanged(object sender, System.EventArgs e)
		{
			this.btnCommit.Enabled = wrapper.Changed;

			if (internalchg) return;
			internalchg = true;
			this.Text = tbFilename.Text = wrapper.FileName;
			internalchg = false;
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ObjfForm));
			this.objfPanel = new System.Windows.Forms.Panel();
			this.lvObjfItem = new System.Windows.Forms.ListView();
			this.chFunction = new System.Windows.Forms.ColumnHeader();
			this.chAction = new System.Windows.Forms.ColumnHeader();
			this.chGuardian = new System.Windows.Forms.ColumnHeader();
			this.btnCommit = new System.Windows.Forms.Button();
			this.llGuardian = new System.Windows.Forms.LinkLabel();
			this.llAction = new System.Windows.Forms.LinkLabel();
			this.btnAction = new System.Windows.Forms.Button();
			this.btnGuardian = new System.Windows.Forms.Button();
			this.lbAction = new System.Windows.Forms.Label();
			this.lbGuardian = new System.Windows.Forms.Label();
			this.tbGuardian = new System.Windows.Forms.TextBox();
			this.tbAction = new System.Windows.Forms.TextBox();
			this.lbFilename = new System.Windows.Forms.Label();
			this.tbFilename = new System.Windows.Forms.TextBox();
			this.pnHeading = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.objfPanel.SuspendLayout();
			this.pnHeading.SuspendLayout();
			this.SuspendLayout();
			// 
			// objfPanel
			// 
			this.objfPanel.AccessibleDescription = resources.GetString("objfPanel.AccessibleDescription");
			this.objfPanel.AccessibleName = resources.GetString("objfPanel.AccessibleName");
			this.objfPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("objfPanel.Anchor")));
			this.objfPanel.AutoScroll = ((bool)(resources.GetObject("objfPanel.AutoScroll")));
			this.objfPanel.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("objfPanel.AutoScrollMargin")));
			this.objfPanel.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("objfPanel.AutoScrollMinSize")));
			this.objfPanel.BackColor = System.Drawing.SystemColors.Control;
			this.objfPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("objfPanel.BackgroundImage")));
			this.objfPanel.Controls.Add(this.lvObjfItem);
			this.objfPanel.Controls.Add(this.btnCommit);
			this.objfPanel.Controls.Add(this.llGuardian);
			this.objfPanel.Controls.Add(this.llAction);
			this.objfPanel.Controls.Add(this.btnAction);
			this.objfPanel.Controls.Add(this.btnGuardian);
			this.objfPanel.Controls.Add(this.lbAction);
			this.objfPanel.Controls.Add(this.lbGuardian);
			this.objfPanel.Controls.Add(this.tbGuardian);
			this.objfPanel.Controls.Add(this.tbAction);
			this.objfPanel.Controls.Add(this.lbFilename);
			this.objfPanel.Controls.Add(this.tbFilename);
			this.objfPanel.Controls.Add(this.pnHeading);
			this.objfPanel.Controls.Add(this.label19);
			this.objfPanel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("objfPanel.Dock")));
			this.objfPanel.Enabled = ((bool)(resources.GetObject("objfPanel.Enabled")));
			this.objfPanel.Font = ((System.Drawing.Font)(resources.GetObject("objfPanel.Font")));
			this.objfPanel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("objfPanel.ImeMode")));
			this.objfPanel.Location = ((System.Drawing.Point)(resources.GetObject("objfPanel.Location")));
			this.objfPanel.Name = "objfPanel";
			this.objfPanel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("objfPanel.RightToLeft")));
			this.objfPanel.Size = ((System.Drawing.Size)(resources.GetObject("objfPanel.Size")));
			this.objfPanel.TabIndex = ((int)(resources.GetObject("objfPanel.TabIndex")));
			this.objfPanel.Text = resources.GetString("objfPanel.Text");
			this.objfPanel.Visible = ((bool)(resources.GetObject("objfPanel.Visible")));
			// 
			// lvObjfItem
			// 
			this.lvObjfItem.AccessibleDescription = resources.GetString("lvObjfItem.AccessibleDescription");
			this.lvObjfItem.AccessibleName = resources.GetString("lvObjfItem.AccessibleName");
			this.lvObjfItem.Alignment = ((System.Windows.Forms.ListViewAlignment)(resources.GetObject("lvObjfItem.Alignment")));
			this.lvObjfItem.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lvObjfItem.Anchor")));
			this.lvObjfItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lvObjfItem.BackgroundImage")));
			this.lvObjfItem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						 this.chFunction,
																						 this.chAction,
																						 this.chGuardian});
			this.lvObjfItem.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lvObjfItem.Dock")));
			this.lvObjfItem.Enabled = ((bool)(resources.GetObject("lvObjfItem.Enabled")));
			this.lvObjfItem.Font = ((System.Drawing.Font)(resources.GetObject("lvObjfItem.Font")));
			this.lvObjfItem.FullRowSelect = true;
			this.lvObjfItem.GridLines = true;
			this.lvObjfItem.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvObjfItem.HideSelection = false;
			this.lvObjfItem.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lvObjfItem.ImeMode")));
			this.lvObjfItem.LabelWrap = ((bool)(resources.GetObject("lvObjfItem.LabelWrap")));
			this.lvObjfItem.Location = ((System.Drawing.Point)(resources.GetObject("lvObjfItem.Location")));
			this.lvObjfItem.MultiSelect = false;
			this.lvObjfItem.Name = "lvObjfItem";
			this.lvObjfItem.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lvObjfItem.RightToLeft")));
			this.lvObjfItem.Size = ((System.Drawing.Size)(resources.GetObject("lvObjfItem.Size")));
			this.lvObjfItem.TabIndex = ((int)(resources.GetObject("lvObjfItem.TabIndex")));
			this.lvObjfItem.Text = resources.GetString("lvObjfItem.Text");
			this.lvObjfItem.View = System.Windows.Forms.View.Details;
			this.lvObjfItem.Visible = ((bool)(resources.GetObject("lvObjfItem.Visible")));
			this.lvObjfItem.SelectedIndexChanged += new System.EventHandler(this.lvObjfItem_SelectedIndexChanged);
			// 
			// chFunction
			// 
			this.chFunction.Text = resources.GetString("chFunction.Text");
			this.chFunction.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("chFunction.TextAlign")));
			this.chFunction.Width = ((int)(resources.GetObject("chFunction.Width")));
			// 
			// chAction
			// 
			this.chAction.Text = resources.GetString("chAction.Text");
			this.chAction.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("chAction.TextAlign")));
			this.chAction.Width = ((int)(resources.GetObject("chAction.Width")));
			// 
			// chGuardian
			// 
			this.chGuardian.Text = resources.GetString("chGuardian.Text");
			this.chGuardian.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("chGuardian.TextAlign")));
			this.chGuardian.Width = ((int)(resources.GetObject("chGuardian.Width")));
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
			// llGuardian
			// 
			this.llGuardian.AccessibleDescription = resources.GetString("llGuardian.AccessibleDescription");
			this.llGuardian.AccessibleName = resources.GetString("llGuardian.AccessibleName");
			this.llGuardian.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llGuardian.Anchor")));
			this.llGuardian.AutoSize = ((bool)(resources.GetObject("llGuardian.AutoSize")));
			this.llGuardian.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llGuardian.Dock")));
			this.llGuardian.Enabled = ((bool)(resources.GetObject("llGuardian.Enabled")));
			this.llGuardian.Font = ((System.Drawing.Font)(resources.GetObject("llGuardian.Font")));
			this.llGuardian.Image = ((System.Drawing.Image)(resources.GetObject("llGuardian.Image")));
			this.llGuardian.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llGuardian.ImageAlign")));
			this.llGuardian.ImageIndex = ((int)(resources.GetObject("llGuardian.ImageIndex")));
			this.llGuardian.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llGuardian.ImeMode")));
			this.llGuardian.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llGuardian.LinkArea")));
			this.llGuardian.Location = ((System.Drawing.Point)(resources.GetObject("llGuardian.Location")));
			this.llGuardian.Name = "llGuardian";
			this.llGuardian.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llGuardian.RightToLeft")));
			this.llGuardian.Size = ((System.Drawing.Size)(resources.GetObject("llGuardian.Size")));
			this.llGuardian.TabIndex = ((int)(resources.GetObject("llGuardian.TabIndex")));
			this.llGuardian.TabStop = true;
			this.llGuardian.Text = resources.GetString("llGuardian.Text");
			this.llGuardian.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llGuardian.TextAlign")));
			this.llGuardian.Visible = ((bool)(resources.GetObject("llGuardian.Visible")));
			this.llGuardian.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llBhav_LinkClicked);
			// 
			// llAction
			// 
			this.llAction.AccessibleDescription = resources.GetString("llAction.AccessibleDescription");
			this.llAction.AccessibleName = resources.GetString("llAction.AccessibleName");
			this.llAction.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("llAction.Anchor")));
			this.llAction.AutoSize = ((bool)(resources.GetObject("llAction.AutoSize")));
			this.llAction.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("llAction.Dock")));
			this.llAction.Enabled = ((bool)(resources.GetObject("llAction.Enabled")));
			this.llAction.Font = ((System.Drawing.Font)(resources.GetObject("llAction.Font")));
			this.llAction.Image = ((System.Drawing.Image)(resources.GetObject("llAction.Image")));
			this.llAction.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llAction.ImageAlign")));
			this.llAction.ImageIndex = ((int)(resources.GetObject("llAction.ImageIndex")));
			this.llAction.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("llAction.ImeMode")));
			this.llAction.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("llAction.LinkArea")));
			this.llAction.Location = ((System.Drawing.Point)(resources.GetObject("llAction.Location")));
			this.llAction.Name = "llAction";
			this.llAction.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("llAction.RightToLeft")));
			this.llAction.Size = ((System.Drawing.Size)(resources.GetObject("llAction.Size")));
			this.llAction.TabIndex = ((int)(resources.GetObject("llAction.TabIndex")));
			this.llAction.TabStop = true;
			this.llAction.Text = resources.GetString("llAction.Text");
			this.llAction.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("llAction.TextAlign")));
			this.llAction.Visible = ((bool)(resources.GetObject("llAction.Visible")));
			this.llAction.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llBhav_LinkClicked);
			// 
			// btnAction
			// 
			this.btnAction.AccessibleDescription = resources.GetString("btnAction.AccessibleDescription");
			this.btnAction.AccessibleName = resources.GetString("btnAction.AccessibleName");
			this.btnAction.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAction.Anchor")));
			this.btnAction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAction.BackgroundImage")));
			this.btnAction.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAction.Dock")));
			this.btnAction.Enabled = ((bool)(resources.GetObject("btnAction.Enabled")));
			this.btnAction.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAction.FlatStyle")));
			this.btnAction.Font = ((System.Drawing.Font)(resources.GetObject("btnAction.Font")));
			this.btnAction.Image = ((System.Drawing.Image)(resources.GetObject("btnAction.Image")));
			this.btnAction.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAction.ImageAlign")));
			this.btnAction.ImageIndex = ((int)(resources.GetObject("btnAction.ImageIndex")));
			this.btnAction.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAction.ImeMode")));
			this.btnAction.Location = ((System.Drawing.Point)(resources.GetObject("btnAction.Location")));
			this.btnAction.Name = "btnAction";
			this.btnAction.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAction.RightToLeft")));
			this.btnAction.Size = ((System.Drawing.Size)(resources.GetObject("btnAction.Size")));
			this.btnAction.TabIndex = ((int)(resources.GetObject("btnAction.TabIndex")));
			this.btnAction.Text = resources.GetString("btnAction.Text");
			this.btnAction.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAction.TextAlign")));
			this.btnAction.Visible = ((bool)(resources.GetObject("btnAction.Visible")));
			this.btnAction.Click += new System.EventHandler(this.GetObjfAction);
			// 
			// btnGuardian
			// 
			this.btnGuardian.AccessibleDescription = resources.GetString("btnGuardian.AccessibleDescription");
			this.btnGuardian.AccessibleName = resources.GetString("btnGuardian.AccessibleName");
			this.btnGuardian.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnGuardian.Anchor")));
			this.btnGuardian.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGuardian.BackgroundImage")));
			this.btnGuardian.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnGuardian.Dock")));
			this.btnGuardian.Enabled = ((bool)(resources.GetObject("btnGuardian.Enabled")));
			this.btnGuardian.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnGuardian.FlatStyle")));
			this.btnGuardian.Font = ((System.Drawing.Font)(resources.GetObject("btnGuardian.Font")));
			this.btnGuardian.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardian.Image")));
			this.btnGuardian.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnGuardian.ImageAlign")));
			this.btnGuardian.ImageIndex = ((int)(resources.GetObject("btnGuardian.ImageIndex")));
			this.btnGuardian.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnGuardian.ImeMode")));
			this.btnGuardian.Location = ((System.Drawing.Point)(resources.GetObject("btnGuardian.Location")));
			this.btnGuardian.Name = "btnGuardian";
			this.btnGuardian.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnGuardian.RightToLeft")));
			this.btnGuardian.Size = ((System.Drawing.Size)(resources.GetObject("btnGuardian.Size")));
			this.btnGuardian.TabIndex = ((int)(resources.GetObject("btnGuardian.TabIndex")));
			this.btnGuardian.Text = resources.GetString("btnGuardian.Text");
			this.btnGuardian.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnGuardian.TextAlign")));
			this.btnGuardian.Visible = ((bool)(resources.GetObject("btnGuardian.Visible")));
			this.btnGuardian.Click += new System.EventHandler(this.GetObjfGuard);
			// 
			// lbAction
			// 
			this.lbAction.AccessibleDescription = resources.GetString("lbAction.AccessibleDescription");
			this.lbAction.AccessibleName = resources.GetString("lbAction.AccessibleName");
			this.lbAction.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbAction.Anchor")));
			this.lbAction.AutoSize = ((bool)(resources.GetObject("lbAction.AutoSize")));
			this.lbAction.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbAction.Dock")));
			this.lbAction.Enabled = ((bool)(resources.GetObject("lbAction.Enabled")));
			this.lbAction.Font = ((System.Drawing.Font)(resources.GetObject("lbAction.Font")));
			this.lbAction.Image = ((System.Drawing.Image)(resources.GetObject("lbAction.Image")));
			this.lbAction.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbAction.ImageAlign")));
			this.lbAction.ImageIndex = ((int)(resources.GetObject("lbAction.ImageIndex")));
			this.lbAction.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbAction.ImeMode")));
			this.lbAction.Location = ((System.Drawing.Point)(resources.GetObject("lbAction.Location")));
			this.lbAction.Name = "lbAction";
			this.lbAction.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbAction.RightToLeft")));
			this.lbAction.Size = ((System.Drawing.Size)(resources.GetObject("lbAction.Size")));
			this.lbAction.TabIndex = ((int)(resources.GetObject("lbAction.TabIndex")));
			this.lbAction.Text = resources.GetString("lbAction.Text");
			this.lbAction.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbAction.TextAlign")));
			this.lbAction.UseMnemonic = false;
			this.lbAction.Visible = ((bool)(resources.GetObject("lbAction.Visible")));
			// 
			// lbGuardian
			// 
			this.lbGuardian.AccessibleDescription = resources.GetString("lbGuardian.AccessibleDescription");
			this.lbGuardian.AccessibleName = resources.GetString("lbGuardian.AccessibleName");
			this.lbGuardian.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbGuardian.Anchor")));
			this.lbGuardian.AutoSize = ((bool)(resources.GetObject("lbGuardian.AutoSize")));
			this.lbGuardian.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbGuardian.Dock")));
			this.lbGuardian.Enabled = ((bool)(resources.GetObject("lbGuardian.Enabled")));
			this.lbGuardian.Font = ((System.Drawing.Font)(resources.GetObject("lbGuardian.Font")));
			this.lbGuardian.Image = ((System.Drawing.Image)(resources.GetObject("lbGuardian.Image")));
			this.lbGuardian.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbGuardian.ImageAlign")));
			this.lbGuardian.ImageIndex = ((int)(resources.GetObject("lbGuardian.ImageIndex")));
			this.lbGuardian.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbGuardian.ImeMode")));
			this.lbGuardian.Location = ((System.Drawing.Point)(resources.GetObject("lbGuardian.Location")));
			this.lbGuardian.Name = "lbGuardian";
			this.lbGuardian.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbGuardian.RightToLeft")));
			this.lbGuardian.Size = ((System.Drawing.Size)(resources.GetObject("lbGuardian.Size")));
			this.lbGuardian.TabIndex = ((int)(resources.GetObject("lbGuardian.TabIndex")));
			this.lbGuardian.Text = resources.GetString("lbGuardian.Text");
			this.lbGuardian.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbGuardian.TextAlign")));
			this.lbGuardian.UseMnemonic = false;
			this.lbGuardian.Visible = ((bool)(resources.GetObject("lbGuardian.Visible")));
			// 
			// tbGuardian
			// 
			this.tbGuardian.AccessibleDescription = resources.GetString("tbGuardian.AccessibleDescription");
			this.tbGuardian.AccessibleName = resources.GetString("tbGuardian.AccessibleName");
			this.tbGuardian.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbGuardian.Anchor")));
			this.tbGuardian.AutoSize = ((bool)(resources.GetObject("tbGuardian.AutoSize")));
			this.tbGuardian.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbGuardian.BackgroundImage")));
			this.tbGuardian.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbGuardian.Dock")));
			this.tbGuardian.Enabled = ((bool)(resources.GetObject("tbGuardian.Enabled")));
			this.tbGuardian.Font = ((System.Drawing.Font)(resources.GetObject("tbGuardian.Font")));
			this.tbGuardian.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbGuardian.ImeMode")));
			this.tbGuardian.Location = ((System.Drawing.Point)(resources.GetObject("tbGuardian.Location")));
			this.tbGuardian.MaxLength = ((int)(resources.GetObject("tbGuardian.MaxLength")));
			this.tbGuardian.Multiline = ((bool)(resources.GetObject("tbGuardian.Multiline")));
			this.tbGuardian.Name = "tbGuardian";
			this.tbGuardian.PasswordChar = ((char)(resources.GetObject("tbGuardian.PasswordChar")));
			this.tbGuardian.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbGuardian.RightToLeft")));
			this.tbGuardian.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbGuardian.ScrollBars")));
			this.tbGuardian.Size = ((System.Drawing.Size)(resources.GetObject("tbGuardian.Size")));
			this.tbGuardian.TabIndex = ((int)(resources.GetObject("tbGuardian.TabIndex")));
			this.tbGuardian.Text = resources.GetString("tbGuardian.Text");
			this.tbGuardian.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbGuardian.TextAlign")));
			this.tbGuardian.Visible = ((bool)(resources.GetObject("tbGuardian.Visible")));
			this.tbGuardian.WordWrap = ((bool)(resources.GetObject("tbGuardian.WordWrap")));
			this.tbGuardian.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbGuardian.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbGuardian.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			// 
			// tbAction
			// 
			this.tbAction.AccessibleDescription = resources.GetString("tbAction.AccessibleDescription");
			this.tbAction.AccessibleName = resources.GetString("tbAction.AccessibleName");
			this.tbAction.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbAction.Anchor")));
			this.tbAction.AutoSize = ((bool)(resources.GetObject("tbAction.AutoSize")));
			this.tbAction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbAction.BackgroundImage")));
			this.tbAction.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbAction.Dock")));
			this.tbAction.Enabled = ((bool)(resources.GetObject("tbAction.Enabled")));
			this.tbAction.Font = ((System.Drawing.Font)(resources.GetObject("tbAction.Font")));
			this.tbAction.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbAction.ImeMode")));
			this.tbAction.Location = ((System.Drawing.Point)(resources.GetObject("tbAction.Location")));
			this.tbAction.MaxLength = ((int)(resources.GetObject("tbAction.MaxLength")));
			this.tbAction.Multiline = ((bool)(resources.GetObject("tbAction.Multiline")));
			this.tbAction.Name = "tbAction";
			this.tbAction.PasswordChar = ((char)(resources.GetObject("tbAction.PasswordChar")));
			this.tbAction.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbAction.RightToLeft")));
			this.tbAction.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbAction.ScrollBars")));
			this.tbAction.Size = ((System.Drawing.Size)(resources.GetObject("tbAction.Size")));
			this.tbAction.TabIndex = ((int)(resources.GetObject("tbAction.TabIndex")));
			this.tbAction.Text = resources.GetString("tbAction.Text");
			this.tbAction.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbAction.TextAlign")));
			this.tbAction.Visible = ((bool)(resources.GetObject("tbAction.Visible")));
			this.tbAction.WordWrap = ((bool)(resources.GetObject("tbAction.WordWrap")));
			this.tbAction.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbAction.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbAction.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			// 
			// lbFilename
			// 
			this.lbFilename.AccessibleDescription = resources.GetString("lbFilename.AccessibleDescription");
			this.lbFilename.AccessibleName = resources.GetString("lbFilename.AccessibleName");
			this.lbFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbFilename.Anchor")));
			this.lbFilename.AutoSize = ((bool)(resources.GetObject("lbFilename.AutoSize")));
			this.lbFilename.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbFilename.Dock")));
			this.lbFilename.Enabled = ((bool)(resources.GetObject("lbFilename.Enabled")));
			this.lbFilename.Font = ((System.Drawing.Font)(resources.GetObject("lbFilename.Font")));
			this.lbFilename.Image = ((System.Drawing.Image)(resources.GetObject("lbFilename.Image")));
			this.lbFilename.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFilename.ImageAlign")));
			this.lbFilename.ImageIndex = ((int)(resources.GetObject("lbFilename.ImageIndex")));
			this.lbFilename.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbFilename.ImeMode")));
			this.lbFilename.Location = ((System.Drawing.Point)(resources.GetObject("lbFilename.Location")));
			this.lbFilename.Name = "lbFilename";
			this.lbFilename.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbFilename.RightToLeft")));
			this.lbFilename.Size = ((System.Drawing.Size)(resources.GetObject("lbFilename.Size")));
			this.lbFilename.TabIndex = ((int)(resources.GetObject("lbFilename.TabIndex")));
			this.lbFilename.Text = resources.GetString("lbFilename.Text");
			this.lbFilename.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFilename.TextAlign")));
			this.lbFilename.Visible = ((bool)(resources.GetObject("lbFilename.Visible")));
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
			this.tbFilename.Validated += new System.EventHandler(this.tbFilename_Validated);
			this.tbFilename.TextChanged += new System.EventHandler(this.tbFilename_TextChanged);
			// 
			// pnHeading
			// 
			this.pnHeading.AccessibleDescription = resources.GetString("pnHeading.AccessibleDescription");
			this.pnHeading.AccessibleName = resources.GetString("pnHeading.AccessibleName");
			this.pnHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pnHeading.Anchor")));
			this.pnHeading.AutoScroll = ((bool)(resources.GetObject("pnHeading.AutoScroll")));
			this.pnHeading.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("pnHeading.AutoScrollMargin")));
			this.pnHeading.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("pnHeading.AutoScrollMinSize")));
			this.pnHeading.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.pnHeading.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnHeading.BackgroundImage")));
			this.pnHeading.Controls.Add(this.label1);
			this.pnHeading.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnHeading.Dock")));
			this.pnHeading.Enabled = ((bool)(resources.GetObject("pnHeading.Enabled")));
			this.pnHeading.Font = ((System.Drawing.Font)(resources.GetObject("pnHeading.Font")));
			this.pnHeading.ForeColor = System.Drawing.SystemColors.ControlText;
			this.pnHeading.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnHeading.ImeMode")));
			this.pnHeading.Location = ((System.Drawing.Point)(resources.GetObject("pnHeading.Location")));
			this.pnHeading.Name = "pnHeading";
			this.pnHeading.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnHeading.RightToLeft")));
			this.pnHeading.Size = ((System.Drawing.Size)(resources.GetObject("pnHeading.Size")));
			this.pnHeading.TabIndex = ((int)(resources.GetObject("pnHeading.TabIndex")));
			this.pnHeading.Text = resources.GetString("pnHeading.Text");
			this.pnHeading.Visible = ((bool)(resources.GetObject("pnHeading.Visible")));
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
			this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
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
			// label19
			// 
			this.label19.AccessibleDescription = resources.GetString("label19.AccessibleDescription");
			this.label19.AccessibleName = resources.GetString("label19.AccessibleName");
			this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label19.Anchor")));
			this.label19.AutoSize = ((bool)(resources.GetObject("label19.AutoSize")));
			this.label19.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label19.Dock")));
			this.label19.Enabled = ((bool)(resources.GetObject("label19.Enabled")));
			this.label19.Font = ((System.Drawing.Font)(resources.GetObject("label19.Font")));
			this.label19.Image = ((System.Drawing.Image)(resources.GetObject("label19.Image")));
			this.label19.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label19.ImageAlign")));
			this.label19.ImageIndex = ((int)(resources.GetObject("label19.ImageIndex")));
			this.label19.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label19.ImeMode")));
			this.label19.Location = ((System.Drawing.Point)(resources.GetObject("label19.Location")));
			this.label19.Name = "label19";
			this.label19.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label19.RightToLeft")));
			this.label19.Size = ((System.Drawing.Size)(resources.GetObject("label19.Size")));
			this.label19.TabIndex = ((int)(resources.GetObject("label19.TabIndex")));
			this.label19.Text = resources.GetString("label19.Text");
			this.label19.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label19.TextAlign")));
			this.label19.Visible = ((bool)(resources.GetObject("label19.Visible")));
			// 
			// menuItem1
			// 
			this.menuItem1.Enabled = ((bool)(resources.GetObject("menuItem1.Enabled")));
			this.menuItem1.Index = -1;
			this.menuItem1.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem1.Shortcut")));
			this.menuItem1.ShowShortcut = ((bool)(resources.GetObject("menuItem1.ShowShortcut")));
			this.menuItem1.Text = resources.GetString("menuItem1.Text");
			this.menuItem1.Visible = ((bool)(resources.GetObject("menuItem1.Visible")));
			// 
			// menuItem2
			// 
			this.menuItem2.Enabled = ((bool)(resources.GetObject("menuItem2.Enabled")));
			this.menuItem2.Index = -1;
			this.menuItem2.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem2.Shortcut")));
			this.menuItem2.ShowShortcut = ((bool)(resources.GetObject("menuItem2.ShowShortcut")));
			this.menuItem2.Text = resources.GetString("menuItem2.Text");
			this.menuItem2.Visible = ((bool)(resources.GetObject("menuItem2.Visible")));
			// 
			// ObjfForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.objfPanel);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "ObjfForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.objfPanel.ResumeLayout(false);
			this.pnHeading.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private void lvObjfItem_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (this.internalchg) return;

			if (lvObjfItem.SelectedIndices.Count > 0 && lvObjfItem.SelectedIndices[0] >= 0)
			{
				currentItem = wrapper[lvObjfItem.SelectedIndices[0]];
				origItem = currentItem.Clone();

				internalchg = true;

				setBHAV(0, currentItem.Action, false);
				setBHAV(1, currentItem.Guardian, false);
				tbGuardian.Enabled = tbAction.Enabled = true;

				internalchg = false;
			}
			else
			{
				internalchg = true;

				tbGuardian.Text = tbAction.Text = lbGuardian.Text = lbAction.Text = "";
				tbGuardian.Enabled = tbAction.Enabled = false;

				internalchg = false;
			}
		}


		private void llBhav_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			pjse.FileTable.Entry item = wrapper.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, (sender == llAction) ? currentItem.Action : currentItem.Guardian);
			Bhav b = new Bhav();
			b.ProcessData(item.PFD, item.Package);

			BhavForm ui = (BhavForm)b.UIHandler;
			ui.Tag = "Popup"; // tells the SetReadOnly function it's in a popup - so everything locked down
			ui.Text = "View BHAV: " + b.FileName + " [" + b.Package.SaveFileName + "]";
			b.RefreshUI();
			ui.Show();
		}

		private void btnCommit_Click(object sender, System.EventArgs e)
		{
			try 
			{
				wrapper.SynchronizeUserData();
				btnCommit.Enabled = wrapper.Changed;
				lvObjfItem_SelectedIndexChanged(null, null);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}			
		}


		private void GetObjfAction(object sender, System.EventArgs e)
		{
			pjse.FileTable.Entry item = new pjse.ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, objfPanel.Parent);
			if (item != null)
				setBHAV(0, (ushort)item.Instance, false);
		}

		private void GetObjfGuard(object sender, System.EventArgs e)
		{
			pjse.FileTable.Entry item = new pjse.ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, objfPanel.Parent);
			if (item != null)
				setBHAV(1, (ushort)item.Instance, false);
		}


		private void tbFilename_TextChanged(object sender, System.EventArgs e)
		{
			wrapper.FileName = tbFilename.Text;
		}

		private void tbFilename_Validated(object sender, System.EventArgs e)
		{
			tbFilename.SelectAll();
		}


		private void hex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex16_IsValid(sender)) return;

			ushort val = Convert.ToUInt16(((TextBox)sender).Text, 16);
			internalchg = true;
			switch (alHex16.IndexOf(sender))
			{
				case 0: currentItem.Action = val; setBHAV(0, val, true); break;
				case 1: currentItem.Guardian = val; setBHAV(1, val, true); break;
			}
			internalchg = false;
		}

		private void hex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex16_IsValid(sender)) return;

			e.Cancel = true;

			internalchg = true;
			ushort val = 0;
			switch (alHex16.IndexOf(sender))
			{
				case 0:
					currentItem.Action = val = origItem.Action;
					setBHAV(0, val, true);
					break;
				case 1:
					currentItem.Guardian = val = origItem.Guardian;
					setBHAV(1, val, true);
					break;
			}
			((TextBox)sender).Text = "0x" + Helper.HexString(val);
			((TextBox)sender).SelectAll();
			internalchg = false;
		}

		private void hex16_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + Helper.HexString(Convert.ToUInt16(((TextBox)sender).Text, 16));
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}

	}
}
