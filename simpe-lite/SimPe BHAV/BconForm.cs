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
	public class BconForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables
		private System.Windows.Forms.TextBox tbFilename;
		private System.Windows.Forms.Panel bconPanel;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.ListView lvConstants;
		private System.Windows.Forms.Panel pnHeading;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbValueHex;
		private System.Windows.Forms.TextBox tbValueDec;
		private System.Windows.Forms.ColumnHeader chID;
		private System.Windows.Forms.ColumnHeader chValue;
		private System.Windows.Forms.ColumnHeader chLabel;
		private System.Windows.Forms.Button btnStrDelete;
		private System.Windows.Forms.Button btnStrAdd;
		private System.Windows.Forms.Label lbFilename;
		private System.Windows.Forms.GroupBox gbValue;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox cbFlag;
		private System.Windows.Forms.Button btnStrPrev;
		private System.Windows.Forms.Button btnStrNext;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnTRCNMaker;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public BconForm()
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


		#region Controller
		private Bcon wrapper = null;
		private bool setHandler = false;
		private bool internalchg = false;

		private int index = -1;
		private short origItem = -1;
		private short currentItem = -1;

		private bool hex16_IsValid(object sender)
		{
			try { Convert.ToUInt16(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private bool dec16_IsValid(object sender)
		{
			try { Convert.ToInt16(((TextBox)sender).Text, 10); }
			catch (Exception) { return false; }
			return true;
		}


		private void UpdateBconItem_Value(short val, bool doHex, bool doDec)
		{
			internalchg = true;
			wrapper[index] = currentItem = val;
			lvConstants.SelectedItems[0].SubItems[1].Text = "0x" + SimPe.Helper.HexString(currentItem);
			if (doHex)
				tbValueHex.Text = lvConstants.SelectedItems[0].SubItems[1].Text;
			if (doDec)
				tbValueDec.Text = currentItem.ToString();
			internalchg = false;
		}

		private ListViewItem lvItem(int i)
		{
			string cID = "0x" + i.ToString("X");
			string cValue = "0x" + SimPe.Helper.HexString(wrapper[i]);
			string cLabel = (wrapper.TrcnResource != null && i < wrapper.TrcnResource.Count) ? wrapper.TrcnResource[i].ConstName : "";
			string[] v = { cID, cValue, cLabel };
			return new ListViewItem(v);
		}

		private void updateLists()
		{
			index = -1;

			this.lvConstants.Items.Clear();
			int nItems = wrapper.Count;
			for(int i = 0; i < nItems; i++)
				this.lvConstants.Items.Add(lvItem(i));
		}

		private void setIndex(int i)
		{
			internalchg = true;
			if (i >= 0) this.lvConstants.Items[i].Selected = true;
			else if (index >= 0) this.lvConstants.Items[index].Selected = false;
			internalchg = false;

			if (this.lvConstants.SelectedItems.Count > 0)
			{
				if (this.lvConstants.Focused) this.lvConstants.SelectedItems[0].Focused = true;
				this.lvConstants.SelectedItems[0].EnsureVisible();
			}

			if (index == i) return;
			index = i;
			displayBconItem();
		}


		private void displayBconItem()
		{
			internalchg = true;
			if (index >= 0 && index < wrapper.Count)
			{
				origItem = currentItem = wrapper[index];

				this.tbValueHex.Text = "0x" + SimPe.Helper.HexString(currentItem);
				this.tbValueDec.Text = currentItem.ToString();

				this.tbValueHex.Enabled = this.tbValueDec.Enabled = true;
			}
			else
			{
				origItem = currentItem = -1;
				this.tbValueHex.Text = this.tbValueDec.Text = "";
				this.tbValueHex.Enabled = this.tbValueDec.Enabled = false;
			}
			this.btnStrPrev.Enabled = (index > 0);
			this.btnStrNext.Enabled = (index < this.lvConstants.Items.Count - 1);
			internalchg = false;

			this.btnCancel.Enabled = false;
		}


		private void BconItemAdd()
		{
			bool savedstate = internalchg;
			internalchg = true;

			if (wrapper.Add(0) >= 0)
				this.lvConstants.Items.Add(lvItem(wrapper.Count - 1));

			internalchg = savedstate;

			setIndex(lvConstants.Items.Count - 1);
		}

		private void BconItemDelete()
		{
			bool savedstate = internalchg;
			internalchg = true;

			int i = index;
			wrapper.RemoveAt(i);
			updateLists();

			internalchg = savedstate;

			setIndex((i >= lvConstants.Items.Count) ? lvConstants.Items.Count - 1 : i);
		}

		private void Commit()
		{
			bool savedstate = internalchg;
			internalchg = true;

			try 
			{
				wrapper.SynchronizeUserData();
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(pjse.coder.Localization.Manager.GetString("errwritingfile"), ex);
			}

			btnCommit.Enabled = wrapper.Changed;

			int i = index;
			updateLists();

			internalchg = savedstate;

			setIndex((i >= lvConstants.Items.Count) ? lvConstants.Items.Count - 1 : i);
		}

		private void Cancel()
		{
			bool savedstate = internalchg;
			internalchg = true;

			UpdateBconItem_Value(origItem, true, true);

			internalchg = savedstate;

			displayBconItem();
		}

		private void TRCNMaker()
		{
			int minArgc = 0;
			Trcn trcn = wrapper.TrcnResource;

			wrapper.Package.BeginUpdate();

			// find Trcn for this Bcon
			if (trcn != null)
			{
				// if it exists ask if user wants to preserve content
				DialogResult dr = MessageBox.Show(
                    pjse.coder.Localization.Manager.GetString("keeplabels")
					, btnTRCNMaker.Text
					, MessageBoxButtons.YesNoCancel
					, MessageBoxIcon.Warning);
				if (dr == DialogResult.Cancel)
					return;
				if (dr == DialogResult.Yes)
					minArgc = trcn.Count;
				else
					trcn.Clear();
			}
			else
			{
				// create a new Trcn file
				SimPe.Interfaces.Files.IPackedFileDescriptor npfd
					= wrapper.Package.Add(0x5452434E, 0, wrapper.FileDescriptor.Group, wrapper.FileDescriptor.Instance);
				trcn = new Trcn();
				trcn.ProcessData(npfd, wrapper.Package);
				trcn.FileName = wrapper.FileName;
			}

			for(int arg = minArgc; arg < wrapper.Count; arg++)
			{
				int p = trcn.Add(new TrcnItem(trcn));
				trcn[arg].ConstId = (uint)arg;
				trcn[arg].ConstName = "Label " + arg.ToString();
				trcn[arg].DefValue = trcn[arg].MaxValue = trcn[arg].MinValue = 0;
			}
			trcn.SynchronizeUserData();
			wrapper.Package.EndUpdate();
			this.updateLists();
			MessageBox.Show(
                pjse.coder.Localization.Manager.GetString("done")
                , btnTRCNMaker.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		#endregion

		#region IPackedFileUI Member
		/// <summary>
		/// Returns the Control that will be displayed within SimPe
		/// </summary>
		public Control GUIHandle
		{
			get
			{
				return bconPanel;
			}
		}

		/// <summary>
		/// Called by the AbstractWrapper when the file should be displayed to the user.
		/// </summary>
		/// <param name="wrp">Reference to the wrapper to be displayed.</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (Bcon)wrp;
			WrapperChanged(wrapper, null);

			internalchg = true;
			updateLists();
			internalchg = false;

			setIndex(lvConstants.Items.Count > 0 ? 0 : -1);

			if (!setHandler)
			{
				wrapper.WrapperChanged += new System.EventHandler(this.WrapperChanged);
				setHandler = true;
			}
		}		

		private void WrapperChanged(object sender, System.EventArgs e)
		{
			this.btnCommit.Enabled = wrapper.Changed;
			if (sender.Equals(currentItem))
				this.btnCancel.Enabled = true;

			if (internalchg) return;

			if (sender.Equals(wrapper))
			{
				internalchg = true;
				this.Text = tbFilename.Text = wrapper.FileName;
				this.cbFlag.Checked = wrapper.Flag;
				internalchg = false;
			}
			else if (!(sender is short))
				updateLists();
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BconForm));
			this.label1 = new System.Windows.Forms.Label();
			this.pnHeading = new System.Windows.Forms.Panel();
			this.btnHelp = new System.Windows.Forms.Button();
			this.lbFilename = new System.Windows.Forms.Label();
			this.tbFilename = new System.Windows.Forms.TextBox();
			this.tbValueDec = new System.Windows.Forms.TextBox();
			this.tbValueHex = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.gbValue = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.bconPanel = new System.Windows.Forms.Panel();
			this.btnStrPrev = new System.Windows.Forms.Button();
			this.btnStrNext = new System.Windows.Forms.Button();
			this.cbFlag = new System.Windows.Forms.CheckBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnStrDelete = new System.Windows.Forms.Button();
			this.btnStrAdd = new System.Windows.Forms.Button();
			this.lvConstants = new System.Windows.Forms.ListView();
			this.chID = new System.Windows.Forms.ColumnHeader();
			this.chValue = new System.Windows.Forms.ColumnHeader();
			this.chLabel = new System.Windows.Forms.ColumnHeader();
			this.btnCommit = new System.Windows.Forms.Button();
			this.btnTRCNMaker = new System.Windows.Forms.Button();
			this.pnHeading.SuspendLayout();
			this.gbValue.SuspendLayout();
			this.bconPanel.SuspendLayout();
			this.SuspendLayout();
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
			this.pnHeading.Controls.Add(this.btnHelp);
			this.pnHeading.Controls.Add(this.label1);
			this.pnHeading.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnHeading.Dock")));
			this.pnHeading.Enabled = ((bool)(resources.GetObject("pnHeading.Enabled")));
			this.pnHeading.Font = ((System.Drawing.Font)(resources.GetObject("pnHeading.Font")));
			this.pnHeading.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.pnHeading.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnHeading.ImeMode")));
			this.pnHeading.Location = ((System.Drawing.Point)(resources.GetObject("pnHeading.Location")));
			this.pnHeading.Name = "pnHeading";
			this.pnHeading.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnHeading.RightToLeft")));
			this.pnHeading.Size = ((System.Drawing.Size)(resources.GetObject("pnHeading.Size")));
			this.pnHeading.TabIndex = ((int)(resources.GetObject("pnHeading.TabIndex")));
			this.pnHeading.Text = resources.GetString("pnHeading.Text");
			this.pnHeading.Visible = ((bool)(resources.GetObject("pnHeading.Visible")));
			// 
			// btnHelp
			// 
			this.btnHelp.AccessibleDescription = resources.GetString("btnHelp.AccessibleDescription");
			this.btnHelp.AccessibleName = resources.GetString("btnHelp.AccessibleName");
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnHelp.Anchor")));
			this.btnHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHelp.BackgroundImage")));
			this.btnHelp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnHelp.Dock")));
			this.btnHelp.Enabled = ((bool)(resources.GetObject("btnHelp.Enabled")));
			this.btnHelp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnHelp.FlatStyle")));
			this.btnHelp.Font = ((System.Drawing.Font)(resources.GetObject("btnHelp.Font")));
			this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
			this.btnHelp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnHelp.ImageAlign")));
			this.btnHelp.ImageIndex = ((int)(resources.GetObject("btnHelp.ImageIndex")));
			this.btnHelp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnHelp.ImeMode")));
			this.btnHelp.Location = ((System.Drawing.Point)(resources.GetObject("btnHelp.Location")));
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnHelp.RightToLeft")));
			this.btnHelp.Size = ((System.Drawing.Size)(resources.GetObject("btnHelp.Size")));
			this.btnHelp.TabIndex = ((int)(resources.GetObject("btnHelp.TabIndex")));
			this.btnHelp.Text = resources.GetString("btnHelp.Text");
			this.btnHelp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnHelp.TextAlign")));
			this.btnHelp.Visible = ((bool)(resources.GetObject("btnHelp.Visible")));
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
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
			this.tbFilename.TextChanged += new System.EventHandler(this.tbFilename_TextChanged);
			this.tbFilename.Enter += new System.EventHandler(this.tbText_Enter);
			// 
			// tbValueDec
			// 
			this.tbValueDec.AccessibleDescription = resources.GetString("tbValueDec.AccessibleDescription");
			this.tbValueDec.AccessibleName = resources.GetString("tbValueDec.AccessibleName");
			this.tbValueDec.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbValueDec.Anchor")));
			this.tbValueDec.AutoSize = ((bool)(resources.GetObject("tbValueDec.AutoSize")));
			this.tbValueDec.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbValueDec.BackgroundImage")));
			this.tbValueDec.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbValueDec.Dock")));
			this.tbValueDec.Enabled = ((bool)(resources.GetObject("tbValueDec.Enabled")));
			this.tbValueDec.Font = ((System.Drawing.Font)(resources.GetObject("tbValueDec.Font")));
			this.tbValueDec.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbValueDec.ImeMode")));
			this.tbValueDec.Location = ((System.Drawing.Point)(resources.GetObject("tbValueDec.Location")));
			this.tbValueDec.MaxLength = ((int)(resources.GetObject("tbValueDec.MaxLength")));
			this.tbValueDec.Multiline = ((bool)(resources.GetObject("tbValueDec.Multiline")));
			this.tbValueDec.Name = "tbValueDec";
			this.tbValueDec.PasswordChar = ((char)(resources.GetObject("tbValueDec.PasswordChar")));
			this.tbValueDec.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbValueDec.RightToLeft")));
			this.tbValueDec.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbValueDec.ScrollBars")));
			this.tbValueDec.Size = ((System.Drawing.Size)(resources.GetObject("tbValueDec.Size")));
			this.tbValueDec.TabIndex = ((int)(resources.GetObject("tbValueDec.TabIndex")));
			this.tbValueDec.Text = resources.GetString("tbValueDec.Text");
			this.tbValueDec.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbValueDec.TextAlign")));
			this.tbValueDec.Visible = ((bool)(resources.GetObject("tbValueDec.Visible")));
			this.tbValueDec.WordWrap = ((bool)(resources.GetObject("tbValueDec.WordWrap")));
			this.tbValueDec.Validating += new System.ComponentModel.CancelEventHandler(this.dec16_Validating);
			this.tbValueDec.Validated += new System.EventHandler(this.dec16_Validated);
			this.tbValueDec.TextChanged += new System.EventHandler(this.dec16_TextChanged);
			this.tbValueDec.Enter += new System.EventHandler(this.tbText_Enter);
			// 
			// tbValueHex
			// 
			this.tbValueHex.AccessibleDescription = resources.GetString("tbValueHex.AccessibleDescription");
			this.tbValueHex.AccessibleName = resources.GetString("tbValueHex.AccessibleName");
			this.tbValueHex.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbValueHex.Anchor")));
			this.tbValueHex.AutoSize = ((bool)(resources.GetObject("tbValueHex.AutoSize")));
			this.tbValueHex.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbValueHex.BackgroundImage")));
			this.tbValueHex.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbValueHex.Dock")));
			this.tbValueHex.Enabled = ((bool)(resources.GetObject("tbValueHex.Enabled")));
			this.tbValueHex.Font = ((System.Drawing.Font)(resources.GetObject("tbValueHex.Font")));
			this.tbValueHex.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbValueHex.ImeMode")));
			this.tbValueHex.Location = ((System.Drawing.Point)(resources.GetObject("tbValueHex.Location")));
			this.tbValueHex.MaxLength = ((int)(resources.GetObject("tbValueHex.MaxLength")));
			this.tbValueHex.Multiline = ((bool)(resources.GetObject("tbValueHex.Multiline")));
			this.tbValueHex.Name = "tbValueHex";
			this.tbValueHex.PasswordChar = ((char)(resources.GetObject("tbValueHex.PasswordChar")));
			this.tbValueHex.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbValueHex.RightToLeft")));
			this.tbValueHex.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbValueHex.ScrollBars")));
			this.tbValueHex.Size = ((System.Drawing.Size)(resources.GetObject("tbValueHex.Size")));
			this.tbValueHex.TabIndex = ((int)(resources.GetObject("tbValueHex.TabIndex")));
			this.tbValueHex.Text = resources.GetString("tbValueHex.Text");
			this.tbValueHex.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbValueHex.TextAlign")));
			this.tbValueHex.Visible = ((bool)(resources.GetObject("tbValueHex.Visible")));
			this.tbValueHex.WordWrap = ((bool)(resources.GetObject("tbValueHex.WordWrap")));
			this.tbValueHex.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbValueHex.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbValueHex.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			this.tbValueHex.Enter += new System.EventHandler(this.tbText_Enter);
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
			// gbValue
			// 
			this.gbValue.AccessibleDescription = resources.GetString("gbValue.AccessibleDescription");
			this.gbValue.AccessibleName = resources.GetString("gbValue.AccessibleName");
			this.gbValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gbValue.Anchor")));
			this.gbValue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gbValue.BackgroundImage")));
			this.gbValue.Controls.Add(this.tbValueDec);
			this.gbValue.Controls.Add(this.tbValueHex);
			this.gbValue.Controls.Add(this.label5);
			this.gbValue.Controls.Add(this.label6);
			this.gbValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("gbValue.Dock")));
			this.gbValue.Enabled = ((bool)(resources.GetObject("gbValue.Enabled")));
			this.gbValue.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.gbValue.Font = ((System.Drawing.Font)(resources.GetObject("gbValue.Font")));
			this.gbValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gbValue.ImeMode")));
			this.gbValue.Location = ((System.Drawing.Point)(resources.GetObject("gbValue.Location")));
			this.gbValue.Name = "gbValue";
			this.gbValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("gbValue.RightToLeft")));
			this.gbValue.Size = ((System.Drawing.Size)(resources.GetObject("gbValue.Size")));
			this.gbValue.TabIndex = ((int)(resources.GetObject("gbValue.TabIndex")));
			this.gbValue.TabStop = false;
			this.gbValue.Text = resources.GetString("gbValue.Text");
			this.gbValue.Visible = ((bool)(resources.GetObject("gbValue.Visible")));
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
			// bconPanel
			// 
			this.bconPanel.AccessibleDescription = resources.GetString("bconPanel.AccessibleDescription");
			this.bconPanel.AccessibleName = resources.GetString("bconPanel.AccessibleName");
			this.bconPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("bconPanel.Anchor")));
			this.bconPanel.AutoScroll = ((bool)(resources.GetObject("bconPanel.AutoScroll")));
			this.bconPanel.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("bconPanel.AutoScrollMargin")));
			this.bconPanel.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("bconPanel.AutoScrollMinSize")));
			this.bconPanel.BackColor = System.Drawing.SystemColors.Control;
			this.bconPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bconPanel.BackgroundImage")));
			this.bconPanel.Controls.Add(this.btnStrPrev);
			this.bconPanel.Controls.Add(this.btnStrNext);
			this.bconPanel.Controls.Add(this.cbFlag);
			this.bconPanel.Controls.Add(this.btnCancel);
			this.bconPanel.Controls.Add(this.btnStrDelete);
			this.bconPanel.Controls.Add(this.btnStrAdd);
			this.bconPanel.Controls.Add(this.lvConstants);
			this.bconPanel.Controls.Add(this.btnCommit);
			this.bconPanel.Controls.Add(this.lbFilename);
			this.bconPanel.Controls.Add(this.tbFilename);
			this.bconPanel.Controls.Add(this.gbValue);
			this.bconPanel.Controls.Add(this.pnHeading);
			this.bconPanel.Controls.Add(this.btnTRCNMaker);
			this.bconPanel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("bconPanel.Dock")));
			this.bconPanel.Enabled = ((bool)(resources.GetObject("bconPanel.Enabled")));
			this.bconPanel.Font = ((System.Drawing.Font)(resources.GetObject("bconPanel.Font")));
			this.bconPanel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("bconPanel.ImeMode")));
			this.bconPanel.Location = ((System.Drawing.Point)(resources.GetObject("bconPanel.Location")));
			this.bconPanel.Name = "bconPanel";
			this.bconPanel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("bconPanel.RightToLeft")));
			this.bconPanel.Size = ((System.Drawing.Size)(resources.GetObject("bconPanel.Size")));
			this.bconPanel.TabIndex = ((int)(resources.GetObject("bconPanel.TabIndex")));
			this.bconPanel.Text = resources.GetString("bconPanel.Text");
			this.bconPanel.Visible = ((bool)(resources.GetObject("bconPanel.Visible")));
			// 
			// btnStrPrev
			// 
			this.btnStrPrev.AccessibleDescription = resources.GetString("btnStrPrev.AccessibleDescription");
			this.btnStrPrev.AccessibleName = resources.GetString("btnStrPrev.AccessibleName");
			this.btnStrPrev.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnStrPrev.Anchor")));
			this.btnStrPrev.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStrPrev.BackgroundImage")));
			this.btnStrPrev.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnStrPrev.Dock")));
			this.btnStrPrev.Enabled = ((bool)(resources.GetObject("btnStrPrev.Enabled")));
			this.btnStrPrev.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnStrPrev.FlatStyle")));
			this.btnStrPrev.Font = ((System.Drawing.Font)(resources.GetObject("btnStrPrev.Font")));
			this.btnStrPrev.Image = ((System.Drawing.Image)(resources.GetObject("btnStrPrev.Image")));
			this.btnStrPrev.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnStrPrev.ImageAlign")));
			this.btnStrPrev.ImageIndex = ((int)(resources.GetObject("btnStrPrev.ImageIndex")));
			this.btnStrPrev.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnStrPrev.ImeMode")));
			this.btnStrPrev.Location = ((System.Drawing.Point)(resources.GetObject("btnStrPrev.Location")));
			this.btnStrPrev.Name = "btnStrPrev";
			this.btnStrPrev.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnStrPrev.RightToLeft")));
			this.btnStrPrev.Size = ((System.Drawing.Size)(resources.GetObject("btnStrPrev.Size")));
			this.btnStrPrev.TabIndex = ((int)(resources.GetObject("btnStrPrev.TabIndex")));
			this.btnStrPrev.TabStop = false;
			this.btnStrPrev.Text = resources.GetString("btnStrPrev.Text");
			this.btnStrPrev.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnStrPrev.TextAlign")));
			this.btnStrPrev.Visible = ((bool)(resources.GetObject("btnStrPrev.Visible")));
			this.btnStrPrev.Click += new System.EventHandler(this.btnStrPrev_Click);
			// 
			// btnStrNext
			// 
			this.btnStrNext.AccessibleDescription = resources.GetString("btnStrNext.AccessibleDescription");
			this.btnStrNext.AccessibleName = resources.GetString("btnStrNext.AccessibleName");
			this.btnStrNext.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnStrNext.Anchor")));
			this.btnStrNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStrNext.BackgroundImage")));
			this.btnStrNext.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnStrNext.Dock")));
			this.btnStrNext.Enabled = ((bool)(resources.GetObject("btnStrNext.Enabled")));
			this.btnStrNext.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnStrNext.FlatStyle")));
			this.btnStrNext.Font = ((System.Drawing.Font)(resources.GetObject("btnStrNext.Font")));
			this.btnStrNext.Image = ((System.Drawing.Image)(resources.GetObject("btnStrNext.Image")));
			this.btnStrNext.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnStrNext.ImageAlign")));
			this.btnStrNext.ImageIndex = ((int)(resources.GetObject("btnStrNext.ImageIndex")));
			this.btnStrNext.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnStrNext.ImeMode")));
			this.btnStrNext.Location = ((System.Drawing.Point)(resources.GetObject("btnStrNext.Location")));
			this.btnStrNext.Name = "btnStrNext";
			this.btnStrNext.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnStrNext.RightToLeft")));
			this.btnStrNext.Size = ((System.Drawing.Size)(resources.GetObject("btnStrNext.Size")));
			this.btnStrNext.TabIndex = ((int)(resources.GetObject("btnStrNext.TabIndex")));
			this.btnStrNext.TabStop = false;
			this.btnStrNext.Text = resources.GetString("btnStrNext.Text");
			this.btnStrNext.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnStrNext.TextAlign")));
			this.btnStrNext.Visible = ((bool)(resources.GetObject("btnStrNext.Visible")));
			this.btnStrNext.Click += new System.EventHandler(this.btnStrNext_Click);
			// 
			// cbFlag
			// 
			this.cbFlag.AccessibleDescription = resources.GetString("cbFlag.AccessibleDescription");
			this.cbFlag.AccessibleName = resources.GetString("cbFlag.AccessibleName");
			this.cbFlag.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbFlag.Anchor")));
			this.cbFlag.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbFlag.Appearance")));
			this.cbFlag.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbFlag.BackgroundImage")));
			this.cbFlag.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbFlag.CheckAlign")));
			this.cbFlag.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbFlag.Dock")));
			this.cbFlag.Enabled = ((bool)(resources.GetObject("cbFlag.Enabled")));
			this.cbFlag.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbFlag.FlatStyle")));
			this.cbFlag.Font = ((System.Drawing.Font)(resources.GetObject("cbFlag.Font")));
			this.cbFlag.Image = ((System.Drawing.Image)(resources.GetObject("cbFlag.Image")));
			this.cbFlag.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbFlag.ImageAlign")));
			this.cbFlag.ImageIndex = ((int)(resources.GetObject("cbFlag.ImageIndex")));
			this.cbFlag.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbFlag.ImeMode")));
			this.cbFlag.Location = ((System.Drawing.Point)(resources.GetObject("cbFlag.Location")));
			this.cbFlag.Name = "cbFlag";
			this.cbFlag.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbFlag.RightToLeft")));
			this.cbFlag.Size = ((System.Drawing.Size)(resources.GetObject("cbFlag.Size")));
			this.cbFlag.TabIndex = ((int)(resources.GetObject("cbFlag.TabIndex")));
			this.cbFlag.Text = resources.GetString("cbFlag.Text");
			this.cbFlag.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbFlag.TextAlign")));
			this.cbFlag.Visible = ((bool)(resources.GetObject("cbFlag.Visible")));
			this.cbFlag.CheckedChanged += new System.EventHandler(this.cbFlag_CheckedChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.AccessibleDescription = resources.GetString("btnCancel.AccessibleDescription");
			this.btnCancel.AccessibleName = resources.GetString("btnCancel.AccessibleName");
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCancel.Anchor")));
			this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
			this.btnCancel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCancel.Dock")));
			this.btnCancel.Enabled = ((bool)(resources.GetObject("btnCancel.Enabled")));
			this.btnCancel.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCancel.FlatStyle")));
			this.btnCancel.Font = ((System.Drawing.Font)(resources.GetObject("btnCancel.Font")));
			this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
			this.btnCancel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCancel.ImageAlign")));
			this.btnCancel.ImageIndex = ((int)(resources.GetObject("btnCancel.ImageIndex")));
			this.btnCancel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCancel.ImeMode")));
			this.btnCancel.Location = ((System.Drawing.Point)(resources.GetObject("btnCancel.Location")));
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCancel.RightToLeft")));
			this.btnCancel.Size = ((System.Drawing.Size)(resources.GetObject("btnCancel.Size")));
			this.btnCancel.TabIndex = ((int)(resources.GetObject("btnCancel.TabIndex")));
			this.btnCancel.Text = resources.GetString("btnCancel.Text");
			this.btnCancel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCancel.TextAlign")));
			this.btnCancel.Visible = ((bool)(resources.GetObject("btnCancel.Visible")));
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnStrDelete
			// 
			this.btnStrDelete.AccessibleDescription = resources.GetString("btnStrDelete.AccessibleDescription");
			this.btnStrDelete.AccessibleName = resources.GetString("btnStrDelete.AccessibleName");
			this.btnStrDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnStrDelete.Anchor")));
			this.btnStrDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStrDelete.BackgroundImage")));
			this.btnStrDelete.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnStrDelete.Dock")));
			this.btnStrDelete.Enabled = ((bool)(resources.GetObject("btnStrDelete.Enabled")));
			this.btnStrDelete.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnStrDelete.FlatStyle")));
			this.btnStrDelete.Font = ((System.Drawing.Font)(resources.GetObject("btnStrDelete.Font")));
			this.btnStrDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnStrDelete.Image")));
			this.btnStrDelete.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnStrDelete.ImageAlign")));
			this.btnStrDelete.ImageIndex = ((int)(resources.GetObject("btnStrDelete.ImageIndex")));
			this.btnStrDelete.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnStrDelete.ImeMode")));
			this.btnStrDelete.Location = ((System.Drawing.Point)(resources.GetObject("btnStrDelete.Location")));
			this.btnStrDelete.Name = "btnStrDelete";
			this.btnStrDelete.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnStrDelete.RightToLeft")));
			this.btnStrDelete.Size = ((System.Drawing.Size)(resources.GetObject("btnStrDelete.Size")));
			this.btnStrDelete.TabIndex = ((int)(resources.GetObject("btnStrDelete.TabIndex")));
			this.btnStrDelete.Text = resources.GetString("btnStrDelete.Text");
			this.btnStrDelete.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnStrDelete.TextAlign")));
			this.btnStrDelete.Visible = ((bool)(resources.GetObject("btnStrDelete.Visible")));
			this.btnStrDelete.Click += new System.EventHandler(this.btnStrDelete_Click);
			// 
			// btnStrAdd
			// 
			this.btnStrAdd.AccessibleDescription = resources.GetString("btnStrAdd.AccessibleDescription");
			this.btnStrAdd.AccessibleName = resources.GetString("btnStrAdd.AccessibleName");
			this.btnStrAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnStrAdd.Anchor")));
			this.btnStrAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStrAdd.BackgroundImage")));
			this.btnStrAdd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnStrAdd.Dock")));
			this.btnStrAdd.Enabled = ((bool)(resources.GetObject("btnStrAdd.Enabled")));
			this.btnStrAdd.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnStrAdd.FlatStyle")));
			this.btnStrAdd.Font = ((System.Drawing.Font)(resources.GetObject("btnStrAdd.Font")));
			this.btnStrAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnStrAdd.Image")));
			this.btnStrAdd.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnStrAdd.ImageAlign")));
			this.btnStrAdd.ImageIndex = ((int)(resources.GetObject("btnStrAdd.ImageIndex")));
			this.btnStrAdd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnStrAdd.ImeMode")));
			this.btnStrAdd.Location = ((System.Drawing.Point)(resources.GetObject("btnStrAdd.Location")));
			this.btnStrAdd.Name = "btnStrAdd";
			this.btnStrAdd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnStrAdd.RightToLeft")));
			this.btnStrAdd.Size = ((System.Drawing.Size)(resources.GetObject("btnStrAdd.Size")));
			this.btnStrAdd.TabIndex = ((int)(resources.GetObject("btnStrAdd.TabIndex")));
			this.btnStrAdd.Text = resources.GetString("btnStrAdd.Text");
			this.btnStrAdd.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnStrAdd.TextAlign")));
			this.btnStrAdd.Visible = ((bool)(resources.GetObject("btnStrAdd.Visible")));
			this.btnStrAdd.Click += new System.EventHandler(this.btnStrAdd_Click);
			// 
			// lvConstants
			// 
			this.lvConstants.AccessibleDescription = resources.GetString("lvConstants.AccessibleDescription");
			this.lvConstants.AccessibleName = resources.GetString("lvConstants.AccessibleName");
			this.lvConstants.Alignment = ((System.Windows.Forms.ListViewAlignment)(resources.GetObject("lvConstants.Alignment")));
			this.lvConstants.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lvConstants.Anchor")));
			this.lvConstants.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lvConstants.BackgroundImage")));
			this.lvConstants.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						  this.chID,
																						  this.chValue,
																						  this.chLabel});
			this.lvConstants.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lvConstants.Dock")));
			this.lvConstants.Enabled = ((bool)(resources.GetObject("lvConstants.Enabled")));
			this.lvConstants.Font = ((System.Drawing.Font)(resources.GetObject("lvConstants.Font")));
			this.lvConstants.FullRowSelect = true;
			this.lvConstants.GridLines = true;
			this.lvConstants.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvConstants.HideSelection = false;
			this.lvConstants.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lvConstants.ImeMode")));
			this.lvConstants.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
																						((System.Windows.Forms.ListViewItem)(resources.GetObject("lvConstants.Items")))});
			this.lvConstants.LabelWrap = ((bool)(resources.GetObject("lvConstants.LabelWrap")));
			this.lvConstants.Location = ((System.Drawing.Point)(resources.GetObject("lvConstants.Location")));
			this.lvConstants.MultiSelect = false;
			this.lvConstants.Name = "lvConstants";
			this.lvConstants.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lvConstants.RightToLeft")));
			this.lvConstants.Size = ((System.Drawing.Size)(resources.GetObject("lvConstants.Size")));
			this.lvConstants.TabIndex = ((int)(resources.GetObject("lvConstants.TabIndex")));
			this.lvConstants.Text = resources.GetString("lvConstants.Text");
			this.lvConstants.View = System.Windows.Forms.View.Details;
			this.lvConstants.Visible = ((bool)(resources.GetObject("lvConstants.Visible")));
			this.lvConstants.SelectedIndexChanged += new System.EventHandler(this.lvConstants_SelectedIndexChanged);
			// 
			// chID
			// 
			this.chID.Text = resources.GetString("chID.Text");
			this.chID.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("chID.TextAlign")));
			this.chID.Width = ((int)(resources.GetObject("chID.Width")));
			// 
			// chValue
			// 
			this.chValue.Text = resources.GetString("chValue.Text");
			this.chValue.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("chValue.TextAlign")));
			this.chValue.Width = ((int)(resources.GetObject("chValue.Width")));
			// 
			// chLabel
			// 
			this.chLabel.Text = resources.GetString("chLabel.Text");
			this.chLabel.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("chLabel.TextAlign")));
			this.chLabel.Width = ((int)(resources.GetObject("chLabel.Width")));
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
			this.btnCommit.Click += new System.EventHandler(this.btnCommit_Clicked);
			// 
			// btnTRCNMaker
			// 
			this.btnTRCNMaker.AccessibleDescription = resources.GetString("btnTRCNMaker.AccessibleDescription");
			this.btnTRCNMaker.AccessibleName = resources.GetString("btnTRCNMaker.AccessibleName");
			this.btnTRCNMaker.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnTRCNMaker.Anchor")));
			this.btnTRCNMaker.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTRCNMaker.BackgroundImage")));
			this.btnTRCNMaker.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnTRCNMaker.Dock")));
			this.btnTRCNMaker.Enabled = ((bool)(resources.GetObject("btnTRCNMaker.Enabled")));
			this.btnTRCNMaker.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnTRCNMaker.FlatStyle")));
			this.btnTRCNMaker.Font = ((System.Drawing.Font)(resources.GetObject("btnTRCNMaker.Font")));
			this.btnTRCNMaker.Image = ((System.Drawing.Image)(resources.GetObject("btnTRCNMaker.Image")));
			this.btnTRCNMaker.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnTRCNMaker.ImageAlign")));
			this.btnTRCNMaker.ImageIndex = ((int)(resources.GetObject("btnTRCNMaker.ImageIndex")));
			this.btnTRCNMaker.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnTRCNMaker.ImeMode")));
			this.btnTRCNMaker.Location = ((System.Drawing.Point)(resources.GetObject("btnTRCNMaker.Location")));
			this.btnTRCNMaker.Name = "btnTRCNMaker";
			this.btnTRCNMaker.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnTRCNMaker.RightToLeft")));
			this.btnTRCNMaker.Size = ((System.Drawing.Size)(resources.GetObject("btnTRCNMaker.Size")));
			this.btnTRCNMaker.TabIndex = ((int)(resources.GetObject("btnTRCNMaker.TabIndex")));
			this.btnTRCNMaker.Text = resources.GetString("btnTRCNMaker.Text");
			this.btnTRCNMaker.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnTRCNMaker.TextAlign")));
			this.btnTRCNMaker.Visible = ((bool)(resources.GetObject("btnTRCNMaker.Visible")));
			this.btnTRCNMaker.Click += new System.EventHandler(this.btnTRCNMaker_Click);
			// 
			// BconForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.bconPanel);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "BconForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.pnHeading.ResumeLayout(false);
			this.gbValue.ResumeLayout(false);
			this.bconPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private void lvConstants_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			setIndex((this.lvConstants.SelectedIndices.Count > 0) ? this.lvConstants.SelectedIndices[0] : -1);
		}


		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			pjse.HelpHelper.Help("Constants");
		}

		private void btnCommit_Clicked(object sender, System.EventArgs e)
		{
			this.Commit();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Cancel();
			this.tbValueHex.SelectAll();
			this.tbValueHex.Focus();
		}

		private void btnTRCNMaker_Click(object sender, System.EventArgs e)
		{
			this.TRCNMaker();
		}


		private void btnStrPrev_Click(object sender, System.EventArgs e)
		{
			this.setIndex(index - 1);
		}

		private void btnStrNext_Click(object sender, System.EventArgs e)
		{
			this.setIndex(index + 1);
		}

		private void btnStrAdd_Click(object sender, System.EventArgs e)
		{
			this.BconItemAdd();
			this.tbValueHex.SelectAll();
			this.tbValueHex.Focus();
		}

		private void btnStrDelete_Click(object sender, System.EventArgs e)
		{
			this.BconItemDelete();
		}


		private void cbFlag_CheckedChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			internalchg = true;
			wrapper.Flag = ((CheckBox)sender).Checked;
			internalchg = false;
		}


		private void tbText_Enter(object sender, System.EventArgs e)
		{
			((TextBox)sender).SelectAll();
		}

		private void tbFilename_TextChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			internalchg = true;
			wrapper.FileName = tbFilename.Text;
			internalchg = false;
		}


		private void hex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex16_IsValid(sender)) return;
			UpdateBconItem_Value(Convert.ToInt16(((TextBox)sender).Text, 16), false, true);
		}

		private void hex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex16_IsValid(sender)) return;
			e.Cancel = true;
			hex16_Validated(sender, null);
		}

		private void hex16_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + Helper.HexString(currentItem);
			internalchg = origstate;
		}


		private void dec16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!dec16_IsValid(sender)) return;
			UpdateBconItem_Value(Convert.ToInt16(((TextBox)sender).Text, 10), true, false);
		}

		private void dec16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (dec16_IsValid(sender)) return;
			e.Cancel = true;
			dec16_Validated(sender, null);
		}

		private void dec16_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = currentItem.ToString();
			internalchg = origstate;
		}

	}
}
