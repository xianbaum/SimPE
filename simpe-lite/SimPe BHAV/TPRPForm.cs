/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   peter@drealm.info                                                     *
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
using SimPe.Plugin;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for TPRPForm.
	/// </summary>
	public class TPRPForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables

		private System.Windows.Forms.Panel pnHeading;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lbFilename;
		private System.Windows.Forms.TextBox tbFilename;
		private System.Windows.Forms.Button btnStrDelete;
		private System.Windows.Forms.Button btnStrAdd;
		private System.Windows.Forms.Label lbLabel;
		private System.Windows.Forms.TextBox tbLabel;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.Label lbVersion;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tpParams;
		private System.Windows.Forms.TabPage tpLocals;
		private System.Windows.Forms.Panel tprpPanel;
		private System.Windows.Forms.TextBox tbVersion;
		private System.Windows.Forms.ListView lvParams;
		private System.Windows.Forms.ListView lvLocals;
		private System.Windows.Forms.ColumnHeader chPID;
		private System.Windows.Forms.ColumnHeader chPLabel;
		private System.Windows.Forms.ColumnHeader chLID;
		private System.Windows.Forms.ColumnHeader chLLabel;
		private System.Windows.Forms.Button btnStrPrev;
		private System.Windows.Forms.Button btnStrNext;
		private System.Windows.Forms.Button btnTabNext;
		private System.Windows.Forms.Button btnTabPrev;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public TPRPForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			TextBox[] t = { tbFilename ,tbLabel ,};
			alText = new ArrayList(t);

			TextBox[] dw = { tbVersion ,};
			alHex32 = new ArrayList(dw);
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
		private TPRP wrapper = null;
		private bool setHandler = false;
		private bool internalchg = false;

		private ArrayList alText = null;
		private ArrayList alHex32 = null;

		private int index = -1;
		private int tab = 0;
		private TPRPItem origItem = null;
		private TPRPItem currentItem = null;

		private bool hex32_IsValid(object sender)
		{
			if (alHex32.IndexOf(sender) < 0)
				throw new Exception("hex32_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToUInt32(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}


		private ListView lvCurrent
		{
			get { return (ListView)((tabControl1.SelectedIndex != 0) ? lvLocals : lvParams); }
		}

		private void LVAdd(ListView lv, TPRPItem item)
		{
			string[] s = {
							 "0x" + lv.Items.Count.ToString("X")
							 ,item.Label
						 };
			lv.Items.Add(new ListViewItem(s));
		}

		private void updateLists()
		{
			wrapper.CleanUp();

			index = -1;

			lvParams.Items.Clear();
			lvLocals.Items.Clear();
			foreach (TPRPItem item in wrapper)
				LVAdd((item is TPRPLocalLabel) ? lvLocals : lvParams, item);
		}


		private void setTab(int l)
		{
			internalchg = true;
			this.tabControl1.SelectedIndex = tab = l;
			internalchg = false;

			if (this.lvCurrent.SelectedIndices.Count == 0)
			{
				index = -1;
				setIndex(lvCurrent.Items.Count > 0 ? 0 : -1);
			}
			else
				index = this.lvCurrent.SelectedIndices[0];

			displayTPRPItem();
		}

		private void setIndex(int i)
		{
			internalchg = true;
			if (i >= 0) this.lvCurrent.Items[i].Selected = true;
			else if (index >= 0) this.lvCurrent.Items[index].Selected = false;
			internalchg = false;

			if (this.lvCurrent.SelectedItems.Count > 0)
			{
				if (this.lvCurrent.Focused) this.lvCurrent.SelectedItems[0].Focused = true;
				this.lvCurrent.SelectedItems[0].EnsureVisible();
			}

			if (index == i) return;
			index = i;
			displayTPRPItem();
		}


		private void displayTPRPItem()
		{
			currentItem = (index < 0) ? null : wrapper[tabControl1.SelectedIndex.Equals(1), index];

			internalchg = true;
			if (currentItem != null)
			{
				origItem = currentItem.Clone();
				this.tbLabel.Text = currentItem.Label;
				this.btnStrDelete.Enabled = this.tbLabel.Enabled = true;
				this.tbLabel.SelectAll();
			}
			else
			{
				origItem = null;
				this.tbLabel.Text = "";
				this.btnStrDelete.Enabled = this.tbLabel.Enabled = false;
			}
			this.btnStrPrev.Enabled = (index > 0);
			this.btnStrNext.Enabled = (index < lvCurrent.Items.Count - 1);
			this.btnTabPrev.Enabled = tab > 0;
			this.btnTabNext.Enabled = tab < this.tabControl1.TabCount - 1;

			internalchg = false;

			this.btnCancel.Enabled = false;
		}


		private void TPRPItemAdd()
		{
			bool savedstate = internalchg;
			internalchg = true;

			TPRPItem newItem = tabControl1.SelectedIndex.Equals(1)
				? (TPRPItem)new TPRPLocalLabel(wrapper)
				: (TPRPItem)new TPRPParamLabel(wrapper)
				;

			if (wrapper.Add(newItem) >= 0)
				LVAdd(lvCurrent, newItem);

			internalchg = savedstate;

			setIndex(lvCurrent.Items.Count - 1);
		}

		private void TPRPItemDelete()
		{
			bool savedstate = internalchg;
			internalchg = true;

			wrapper.Remove(currentItem);
			int i = index;
			updateLists();

			internalchg = savedstate;

			setIndex((i >= lvCurrent.Items.Count) ? lvCurrent.Items.Count - 1 : i);
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
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}

			btnCommit.Enabled = wrapper.Changed;

			int i = index;
			updateLists();

			internalchg = savedstate;

			setIndex((i >= lvCurrent.Items.Count) ? lvCurrent.Items.Count - 1 : i);
		}

		private void Cancel()
		{
			bool savedstate = internalchg;
			internalchg = true;

			lvCurrent.SelectedItems[0].SubItems[1].Text = currentItem.Label = origItem.Label;

			internalchg = savedstate;

			displayTPRPItem();
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
				return tprpPanel;
			}
		}

		/// <summary>
		/// Called by the AbstractWrapper when the file should be displayed to the user.
		/// </summary>
		/// <param name="wrp">Reference to the wrapper to be displayed.</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (TPRP)wrp;
			WrapperChanged(wrapper, null);

			internalchg = true;
			updateLists();
			internalchg = false;

			setTab(0);

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
				this.tbVersion.Text = "0x" + SimPe.Helper.HexString(wrapper.Version);
				internalchg = false;
			}
			else if (!sender.Equals(currentItem))
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TPRPForm));
			this.btnCommit = new System.Windows.Forms.Button();
			this.pnHeading = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.tprpPanel = new System.Windows.Forms.Panel();
			this.btnTabNext = new System.Windows.Forms.Button();
			this.btnTabPrev = new System.Windows.Forms.Button();
			this.btnStrPrev = new System.Windows.Forms.Button();
			this.btnStrNext = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpParams = new System.Windows.Forms.TabPage();
			this.lvParams = new System.Windows.Forms.ListView();
			this.chPID = new System.Windows.Forms.ColumnHeader();
			this.chPLabel = new System.Windows.Forms.ColumnHeader();
			this.tpLocals = new System.Windows.Forms.TabPage();
			this.lvLocals = new System.Windows.Forms.ListView();
			this.chLID = new System.Windows.Forms.ColumnHeader();
			this.chLLabel = new System.Windows.Forms.ColumnHeader();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tbLabel = new System.Windows.Forms.TextBox();
			this.btnStrDelete = new System.Windows.Forms.Button();
			this.btnStrAdd = new System.Windows.Forms.Button();
			this.lbVersion = new System.Windows.Forms.Label();
			this.tbVersion = new System.Windows.Forms.TextBox();
			this.tbFilename = new System.Windows.Forms.TextBox();
			this.lbFilename = new System.Windows.Forms.Label();
			this.lbLabel = new System.Windows.Forms.Label();
			this.pnHeading.SuspendLayout();
			this.tprpPanel.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tpParams.SuspendLayout();
			this.tpLocals.SuspendLayout();
			this.SuspendLayout();
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
			// tprpPanel
			// 
			this.tprpPanel.AccessibleDescription = resources.GetString("tprpPanel.AccessibleDescription");
			this.tprpPanel.AccessibleName = resources.GetString("tprpPanel.AccessibleName");
			this.tprpPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tprpPanel.Anchor")));
			this.tprpPanel.AutoScroll = ((bool)(resources.GetObject("tprpPanel.AutoScroll")));
			this.tprpPanel.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tprpPanel.AutoScrollMargin")));
			this.tprpPanel.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tprpPanel.AutoScrollMinSize")));
			this.tprpPanel.BackColor = System.Drawing.SystemColors.Control;
			this.tprpPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tprpPanel.BackgroundImage")));
			this.tprpPanel.Controls.Add(this.btnTabNext);
			this.tprpPanel.Controls.Add(this.btnTabPrev);
			this.tprpPanel.Controls.Add(this.btnStrPrev);
			this.tprpPanel.Controls.Add(this.btnStrNext);
			this.tprpPanel.Controls.Add(this.tabControl1);
			this.tprpPanel.Controls.Add(this.btnCancel);
			this.tprpPanel.Controls.Add(this.tbLabel);
			this.tprpPanel.Controls.Add(this.btnStrDelete);
			this.tprpPanel.Controls.Add(this.btnStrAdd);
			this.tprpPanel.Controls.Add(this.lbVersion);
			this.tprpPanel.Controls.Add(this.tbVersion);
			this.tprpPanel.Controls.Add(this.tbFilename);
			this.tprpPanel.Controls.Add(this.lbFilename);
			this.tprpPanel.Controls.Add(this.btnCommit);
			this.tprpPanel.Controls.Add(this.pnHeading);
			this.tprpPanel.Controls.Add(this.lbLabel);
			this.tprpPanel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tprpPanel.Dock")));
			this.tprpPanel.Enabled = ((bool)(resources.GetObject("tprpPanel.Enabled")));
			this.tprpPanel.Font = ((System.Drawing.Font)(resources.GetObject("tprpPanel.Font")));
			this.tprpPanel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tprpPanel.ImeMode")));
			this.tprpPanel.Location = ((System.Drawing.Point)(resources.GetObject("tprpPanel.Location")));
			this.tprpPanel.Name = "tprpPanel";
			this.tprpPanel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tprpPanel.RightToLeft")));
			this.tprpPanel.Size = ((System.Drawing.Size)(resources.GetObject("tprpPanel.Size")));
			this.tprpPanel.TabIndex = ((int)(resources.GetObject("tprpPanel.TabIndex")));
			this.tprpPanel.Text = resources.GetString("tprpPanel.Text");
			this.tprpPanel.Visible = ((bool)(resources.GetObject("tprpPanel.Visible")));
			// 
			// btnTabNext
			// 
			this.btnTabNext.AccessibleDescription = resources.GetString("btnTabNext.AccessibleDescription");
			this.btnTabNext.AccessibleName = resources.GetString("btnTabNext.AccessibleName");
			this.btnTabNext.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnTabNext.Anchor")));
			this.btnTabNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTabNext.BackgroundImage")));
			this.btnTabNext.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnTabNext.Dock")));
			this.btnTabNext.Enabled = ((bool)(resources.GetObject("btnTabNext.Enabled")));
			this.btnTabNext.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnTabNext.FlatStyle")));
			this.btnTabNext.Font = ((System.Drawing.Font)(resources.GetObject("btnTabNext.Font")));
			this.btnTabNext.Image = ((System.Drawing.Image)(resources.GetObject("btnTabNext.Image")));
			this.btnTabNext.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnTabNext.ImageAlign")));
			this.btnTabNext.ImageIndex = ((int)(resources.GetObject("btnTabNext.ImageIndex")));
			this.btnTabNext.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnTabNext.ImeMode")));
			this.btnTabNext.Location = ((System.Drawing.Point)(resources.GetObject("btnTabNext.Location")));
			this.btnTabNext.Name = "btnTabNext";
			this.btnTabNext.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnTabNext.RightToLeft")));
			this.btnTabNext.Size = ((System.Drawing.Size)(resources.GetObject("btnTabNext.Size")));
			this.btnTabNext.TabIndex = ((int)(resources.GetObject("btnTabNext.TabIndex")));
			this.btnTabNext.TabStop = false;
			this.btnTabNext.Text = resources.GetString("btnTabNext.Text");
			this.btnTabNext.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnTabNext.TextAlign")));
			this.btnTabNext.Visible = ((bool)(resources.GetObject("btnTabNext.Visible")));
			this.btnTabNext.Click += new System.EventHandler(this.btnTabNext_Click);
			// 
			// btnTabPrev
			// 
			this.btnTabPrev.AccessibleDescription = resources.GetString("btnTabPrev.AccessibleDescription");
			this.btnTabPrev.AccessibleName = resources.GetString("btnTabPrev.AccessibleName");
			this.btnTabPrev.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnTabPrev.Anchor")));
			this.btnTabPrev.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTabPrev.BackgroundImage")));
			this.btnTabPrev.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnTabPrev.Dock")));
			this.btnTabPrev.Enabled = ((bool)(resources.GetObject("btnTabPrev.Enabled")));
			this.btnTabPrev.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnTabPrev.FlatStyle")));
			this.btnTabPrev.Font = ((System.Drawing.Font)(resources.GetObject("btnTabPrev.Font")));
			this.btnTabPrev.Image = ((System.Drawing.Image)(resources.GetObject("btnTabPrev.Image")));
			this.btnTabPrev.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnTabPrev.ImageAlign")));
			this.btnTabPrev.ImageIndex = ((int)(resources.GetObject("btnTabPrev.ImageIndex")));
			this.btnTabPrev.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnTabPrev.ImeMode")));
			this.btnTabPrev.Location = ((System.Drawing.Point)(resources.GetObject("btnTabPrev.Location")));
			this.btnTabPrev.Name = "btnTabPrev";
			this.btnTabPrev.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnTabPrev.RightToLeft")));
			this.btnTabPrev.Size = ((System.Drawing.Size)(resources.GetObject("btnTabPrev.Size")));
			this.btnTabPrev.TabIndex = ((int)(resources.GetObject("btnTabPrev.TabIndex")));
			this.btnTabPrev.TabStop = false;
			this.btnTabPrev.Text = resources.GetString("btnTabPrev.Text");
			this.btnTabPrev.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnTabPrev.TextAlign")));
			this.btnTabPrev.Visible = ((bool)(resources.GetObject("btnTabPrev.Visible")));
			this.btnTabPrev.Click += new System.EventHandler(this.btnTabPrev_Click);
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
			// tabControl1
			// 
			this.tabControl1.AccessibleDescription = resources.GetString("tabControl1.AccessibleDescription");
			this.tabControl1.AccessibleName = resources.GetString("tabControl1.AccessibleName");
			this.tabControl1.Alignment = ((System.Windows.Forms.TabAlignment)(resources.GetObject("tabControl1.Alignment")));
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabControl1.Anchor")));
			this.tabControl1.Appearance = ((System.Windows.Forms.TabAppearance)(resources.GetObject("tabControl1.Appearance")));
			this.tabControl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabControl1.BackgroundImage")));
			this.tabControl1.Controls.Add(this.tpParams);
			this.tabControl1.Controls.Add(this.tpLocals);
			this.tabControl1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabControl1.Dock")));
			this.tabControl1.Enabled = ((bool)(resources.GetObject("tabControl1.Enabled")));
			this.tabControl1.Font = ((System.Drawing.Font)(resources.GetObject("tabControl1.Font")));
			this.tabControl1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tabControl1.ImeMode")));
			this.tabControl1.ItemSize = ((System.Drawing.Size)(resources.GetObject("tabControl1.ItemSize")));
			this.tabControl1.Location = ((System.Drawing.Point)(resources.GetObject("tabControl1.Location")));
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.Padding = ((System.Drawing.Point)(resources.GetObject("tabControl1.Padding")));
			this.tabControl1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tabControl1.RightToLeft")));
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.ShowToolTips = ((bool)(resources.GetObject("tabControl1.ShowToolTips")));
			this.tabControl1.Size = ((System.Drawing.Size)(resources.GetObject("tabControl1.Size")));
			this.tabControl1.TabIndex = ((int)(resources.GetObject("tabControl1.TabIndex")));
			this.tabControl1.Text = resources.GetString("tabControl1.Text");
			this.tabControl1.Visible = ((bool)(resources.GetObject("tabControl1.Visible")));
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// tpParams
			// 
			this.tpParams.AccessibleDescription = resources.GetString("tpParams.AccessibleDescription");
			this.tpParams.AccessibleName = resources.GetString("tpParams.AccessibleName");
			this.tpParams.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tpParams.Anchor")));
			this.tpParams.AutoScroll = ((bool)(resources.GetObject("tpParams.AutoScroll")));
			this.tpParams.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tpParams.AutoScrollMargin")));
			this.tpParams.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tpParams.AutoScrollMinSize")));
			this.tpParams.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tpParams.BackgroundImage")));
			this.tpParams.Controls.Add(this.lvParams);
			this.tpParams.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tpParams.Dock")));
			this.tpParams.Enabled = ((bool)(resources.GetObject("tpParams.Enabled")));
			this.tpParams.Font = ((System.Drawing.Font)(resources.GetObject("tpParams.Font")));
			this.tpParams.ImageIndex = ((int)(resources.GetObject("tpParams.ImageIndex")));
			this.tpParams.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tpParams.ImeMode")));
			this.tpParams.Location = ((System.Drawing.Point)(resources.GetObject("tpParams.Location")));
			this.tpParams.Name = "tpParams";
			this.tpParams.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tpParams.RightToLeft")));
			this.tpParams.Size = ((System.Drawing.Size)(resources.GetObject("tpParams.Size")));
			this.tpParams.TabIndex = ((int)(resources.GetObject("tpParams.TabIndex")));
			this.tpParams.Text = resources.GetString("tpParams.Text");
			this.tpParams.ToolTipText = resources.GetString("tpParams.ToolTipText");
			this.tpParams.Visible = ((bool)(resources.GetObject("tpParams.Visible")));
			// 
			// lvParams
			// 
			this.lvParams.AccessibleDescription = resources.GetString("lvParams.AccessibleDescription");
			this.lvParams.AccessibleName = resources.GetString("lvParams.AccessibleName");
			this.lvParams.Alignment = ((System.Windows.Forms.ListViewAlignment)(resources.GetObject("lvParams.Alignment")));
			this.lvParams.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lvParams.Anchor")));
			this.lvParams.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lvParams.BackgroundImage")));
			this.lvParams.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.chPID,
																					   this.chPLabel});
			this.lvParams.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lvParams.Dock")));
			this.lvParams.Enabled = ((bool)(resources.GetObject("lvParams.Enabled")));
			this.lvParams.Font = ((System.Drawing.Font)(resources.GetObject("lvParams.Font")));
			this.lvParams.FullRowSelect = true;
			this.lvParams.GridLines = true;
			this.lvParams.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvParams.HideSelection = false;
			this.lvParams.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lvParams.ImeMode")));
			this.lvParams.LabelWrap = ((bool)(resources.GetObject("lvParams.LabelWrap")));
			this.lvParams.Location = ((System.Drawing.Point)(resources.GetObject("lvParams.Location")));
			this.lvParams.MultiSelect = false;
			this.lvParams.Name = "lvParams";
			this.lvParams.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lvParams.RightToLeft")));
			this.lvParams.Size = ((System.Drawing.Size)(resources.GetObject("lvParams.Size")));
			this.lvParams.TabIndex = ((int)(resources.GetObject("lvParams.TabIndex")));
			this.lvParams.Text = resources.GetString("lvParams.Text");
			this.lvParams.View = System.Windows.Forms.View.Details;
			this.lvParams.Visible = ((bool)(resources.GetObject("lvParams.Visible")));
			this.lvParams.ItemActivate += new System.EventHandler(this.ListView_ItemActivate);
			this.lvParams.SelectedIndexChanged += new System.EventHandler(this.ListView_SelectedIndexChanged);
			// 
			// chPID
			// 
			this.chPID.Text = resources.GetString("chPID.Text");
			this.chPID.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("chPID.TextAlign")));
			this.chPID.Width = ((int)(resources.GetObject("chPID.Width")));
			// 
			// chPLabel
			// 
			this.chPLabel.Text = resources.GetString("chPLabel.Text");
			this.chPLabel.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("chPLabel.TextAlign")));
			this.chPLabel.Width = ((int)(resources.GetObject("chPLabel.Width")));
			// 
			// tpLocals
			// 
			this.tpLocals.AccessibleDescription = resources.GetString("tpLocals.AccessibleDescription");
			this.tpLocals.AccessibleName = resources.GetString("tpLocals.AccessibleName");
			this.tpLocals.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tpLocals.Anchor")));
			this.tpLocals.AutoScroll = ((bool)(resources.GetObject("tpLocals.AutoScroll")));
			this.tpLocals.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tpLocals.AutoScrollMargin")));
			this.tpLocals.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tpLocals.AutoScrollMinSize")));
			this.tpLocals.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tpLocals.BackgroundImage")));
			this.tpLocals.Controls.Add(this.lvLocals);
			this.tpLocals.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tpLocals.Dock")));
			this.tpLocals.Enabled = ((bool)(resources.GetObject("tpLocals.Enabled")));
			this.tpLocals.Font = ((System.Drawing.Font)(resources.GetObject("tpLocals.Font")));
			this.tpLocals.ImageIndex = ((int)(resources.GetObject("tpLocals.ImageIndex")));
			this.tpLocals.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tpLocals.ImeMode")));
			this.tpLocals.Location = ((System.Drawing.Point)(resources.GetObject("tpLocals.Location")));
			this.tpLocals.Name = "tpLocals";
			this.tpLocals.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tpLocals.RightToLeft")));
			this.tpLocals.Size = ((System.Drawing.Size)(resources.GetObject("tpLocals.Size")));
			this.tpLocals.TabIndex = ((int)(resources.GetObject("tpLocals.TabIndex")));
			this.tpLocals.Text = resources.GetString("tpLocals.Text");
			this.tpLocals.ToolTipText = resources.GetString("tpLocals.ToolTipText");
			this.tpLocals.Visible = ((bool)(resources.GetObject("tpLocals.Visible")));
			// 
			// lvLocals
			// 
			this.lvLocals.AccessibleDescription = resources.GetString("lvLocals.AccessibleDescription");
			this.lvLocals.AccessibleName = resources.GetString("lvLocals.AccessibleName");
			this.lvLocals.Alignment = ((System.Windows.Forms.ListViewAlignment)(resources.GetObject("lvLocals.Alignment")));
			this.lvLocals.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lvLocals.Anchor")));
			this.lvLocals.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lvLocals.BackgroundImage")));
			this.lvLocals.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.chLID,
																					   this.chLLabel});
			this.lvLocals.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lvLocals.Dock")));
			this.lvLocals.Enabled = ((bool)(resources.GetObject("lvLocals.Enabled")));
			this.lvLocals.Font = ((System.Drawing.Font)(resources.GetObject("lvLocals.Font")));
			this.lvLocals.FullRowSelect = true;
			this.lvLocals.GridLines = true;
			this.lvLocals.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvLocals.HideSelection = false;
			this.lvLocals.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lvLocals.ImeMode")));
			this.lvLocals.LabelWrap = ((bool)(resources.GetObject("lvLocals.LabelWrap")));
			this.lvLocals.Location = ((System.Drawing.Point)(resources.GetObject("lvLocals.Location")));
			this.lvLocals.MultiSelect = false;
			this.lvLocals.Name = "lvLocals";
			this.lvLocals.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lvLocals.RightToLeft")));
			this.lvLocals.Size = ((System.Drawing.Size)(resources.GetObject("lvLocals.Size")));
			this.lvLocals.TabIndex = ((int)(resources.GetObject("lvLocals.TabIndex")));
			this.lvLocals.Text = resources.GetString("lvLocals.Text");
			this.lvLocals.View = System.Windows.Forms.View.Details;
			this.lvLocals.Visible = ((bool)(resources.GetObject("lvLocals.Visible")));
			this.lvLocals.ItemActivate += new System.EventHandler(this.ListView_ItemActivate);
			this.lvLocals.SelectedIndexChanged += new System.EventHandler(this.ListView_SelectedIndexChanged);
			// 
			// chLID
			// 
			this.chLID.Text = resources.GetString("chLID.Text");
			this.chLID.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("chLID.TextAlign")));
			this.chLID.Width = ((int)(resources.GetObject("chLID.Width")));
			// 
			// chLLabel
			// 
			this.chLLabel.Text = resources.GetString("chLLabel.Text");
			this.chLLabel.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("chLLabel.TextAlign")));
			this.chLLabel.Width = ((int)(resources.GetObject("chLLabel.Width")));
			// 
			// btnCancel
			// 
			this.btnCancel.AccessibleDescription = resources.GetString("btnCancel.AccessibleDescription");
			this.btnCancel.AccessibleName = resources.GetString("btnCancel.AccessibleName");
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCancel.Anchor")));
			this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
			// tbLabel
			// 
			this.tbLabel.AccessibleDescription = resources.GetString("tbLabel.AccessibleDescription");
			this.tbLabel.AccessibleName = resources.GetString("tbLabel.AccessibleName");
			this.tbLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbLabel.Anchor")));
			this.tbLabel.AutoSize = ((bool)(resources.GetObject("tbLabel.AutoSize")));
			this.tbLabel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbLabel.BackgroundImage")));
			this.tbLabel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbLabel.Dock")));
			this.tbLabel.Enabled = ((bool)(resources.GetObject("tbLabel.Enabled")));
			this.tbLabel.Font = ((System.Drawing.Font)(resources.GetObject("tbLabel.Font")));
			this.tbLabel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbLabel.ImeMode")));
			this.tbLabel.Location = ((System.Drawing.Point)(resources.GetObject("tbLabel.Location")));
			this.tbLabel.MaxLength = ((int)(resources.GetObject("tbLabel.MaxLength")));
			this.tbLabel.Multiline = ((bool)(resources.GetObject("tbLabel.Multiline")));
			this.tbLabel.Name = "tbLabel";
			this.tbLabel.PasswordChar = ((char)(resources.GetObject("tbLabel.PasswordChar")));
			this.tbLabel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbLabel.RightToLeft")));
			this.tbLabel.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbLabel.ScrollBars")));
			this.tbLabel.Size = ((System.Drawing.Size)(resources.GetObject("tbLabel.Size")));
			this.tbLabel.TabIndex = ((int)(resources.GetObject("tbLabel.TabIndex")));
			this.tbLabel.Text = resources.GetString("tbLabel.Text");
			this.tbLabel.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbLabel.TextAlign")));
			this.tbLabel.Visible = ((bool)(resources.GetObject("tbLabel.Visible")));
			this.tbLabel.WordWrap = ((bool)(resources.GetObject("tbLabel.WordWrap")));
			this.tbLabel.Validated += new System.EventHandler(this.tbText_Enter);
			this.tbLabel.TextChanged += new System.EventHandler(this.tbText_TextChanged);
			this.tbLabel.Enter += new System.EventHandler(this.tbText_Enter);
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
			// lbVersion
			// 
			this.lbVersion.AccessibleDescription = resources.GetString("lbVersion.AccessibleDescription");
			this.lbVersion.AccessibleName = resources.GetString("lbVersion.AccessibleName");
			this.lbVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbVersion.Anchor")));
			this.lbVersion.AutoSize = ((bool)(resources.GetObject("lbVersion.AutoSize")));
			this.lbVersion.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbVersion.Dock")));
			this.lbVersion.Enabled = ((bool)(resources.GetObject("lbVersion.Enabled")));
			this.lbVersion.Font = ((System.Drawing.Font)(resources.GetObject("lbVersion.Font")));
			this.lbVersion.Image = ((System.Drawing.Image)(resources.GetObject("lbVersion.Image")));
			this.lbVersion.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbVersion.ImageAlign")));
			this.lbVersion.ImageIndex = ((int)(resources.GetObject("lbVersion.ImageIndex")));
			this.lbVersion.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbVersion.ImeMode")));
			this.lbVersion.Location = ((System.Drawing.Point)(resources.GetObject("lbVersion.Location")));
			this.lbVersion.Name = "lbVersion";
			this.lbVersion.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbVersion.RightToLeft")));
			this.lbVersion.Size = ((System.Drawing.Size)(resources.GetObject("lbVersion.Size")));
			this.lbVersion.TabIndex = ((int)(resources.GetObject("lbVersion.TabIndex")));
			this.lbVersion.Text = resources.GetString("lbVersion.Text");
			this.lbVersion.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbVersion.TextAlign")));
			this.lbVersion.Visible = ((bool)(resources.GetObject("lbVersion.Visible")));
			// 
			// tbVersion
			// 
			this.tbVersion.AccessibleDescription = resources.GetString("tbVersion.AccessibleDescription");
			this.tbVersion.AccessibleName = resources.GetString("tbVersion.AccessibleName");
			this.tbVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbVersion.Anchor")));
			this.tbVersion.AutoSize = ((bool)(resources.GetObject("tbVersion.AutoSize")));
			this.tbVersion.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbVersion.BackgroundImage")));
			this.tbVersion.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbVersion.Dock")));
			this.tbVersion.Enabled = ((bool)(resources.GetObject("tbVersion.Enabled")));
			this.tbVersion.Font = ((System.Drawing.Font)(resources.GetObject("tbVersion.Font")));
			this.tbVersion.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbVersion.ImeMode")));
			this.tbVersion.Location = ((System.Drawing.Point)(resources.GetObject("tbVersion.Location")));
			this.tbVersion.MaxLength = ((int)(resources.GetObject("tbVersion.MaxLength")));
			this.tbVersion.Multiline = ((bool)(resources.GetObject("tbVersion.Multiline")));
			this.tbVersion.Name = "tbVersion";
			this.tbVersion.PasswordChar = ((char)(resources.GetObject("tbVersion.PasswordChar")));
			this.tbVersion.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbVersion.RightToLeft")));
			this.tbVersion.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbVersion.ScrollBars")));
			this.tbVersion.Size = ((System.Drawing.Size)(resources.GetObject("tbVersion.Size")));
			this.tbVersion.TabIndex = ((int)(resources.GetObject("tbVersion.TabIndex")));
			this.tbVersion.Text = resources.GetString("tbVersion.Text");
			this.tbVersion.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbVersion.TextAlign")));
			this.tbVersion.Visible = ((bool)(resources.GetObject("tbVersion.Visible")));
			this.tbVersion.WordWrap = ((bool)(resources.GetObject("tbVersion.WordWrap")));
			this.tbVersion.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
			this.tbVersion.Validated += new System.EventHandler(this.hex32_Validated);
			this.tbVersion.TextChanged += new System.EventHandler(this.hex32_TextChanged);
			this.tbVersion.Enter += new System.EventHandler(this.tbText_Enter);
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
			this.tbFilename.Validated += new System.EventHandler(this.tbText_Enter);
			this.tbFilename.TextChanged += new System.EventHandler(this.tbText_TextChanged);
			this.tbFilename.Enter += new System.EventHandler(this.tbText_Enter);
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
			// lbLabel
			// 
			this.lbLabel.AccessibleDescription = resources.GetString("lbLabel.AccessibleDescription");
			this.lbLabel.AccessibleName = resources.GetString("lbLabel.AccessibleName");
			this.lbLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbLabel.Anchor")));
			this.lbLabel.AutoSize = ((bool)(resources.GetObject("lbLabel.AutoSize")));
			this.lbLabel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbLabel.Dock")));
			this.lbLabel.Enabled = ((bool)(resources.GetObject("lbLabel.Enabled")));
			this.lbLabel.Font = ((System.Drawing.Font)(resources.GetObject("lbLabel.Font")));
			this.lbLabel.Image = ((System.Drawing.Image)(resources.GetObject("lbLabel.Image")));
			this.lbLabel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbLabel.ImageAlign")));
			this.lbLabel.ImageIndex = ((int)(resources.GetObject("lbLabel.ImageIndex")));
			this.lbLabel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbLabel.ImeMode")));
			this.lbLabel.Location = ((System.Drawing.Point)(resources.GetObject("lbLabel.Location")));
			this.lbLabel.Name = "lbLabel";
			this.lbLabel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbLabel.RightToLeft")));
			this.lbLabel.Size = ((System.Drawing.Size)(resources.GetObject("lbLabel.Size")));
			this.lbLabel.TabIndex = ((int)(resources.GetObject("lbLabel.TabIndex")));
			this.lbLabel.Text = resources.GetString("lbLabel.Text");
			this.lbLabel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbLabel.TextAlign")));
			this.lbLabel.Visible = ((bool)(resources.GetObject("lbLabel.Visible")));
			// 
			// TPRPForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.CancelButton = this.btnCancel;
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.tprpPanel);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "TPRPForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.pnHeading.ResumeLayout(false);
			this.tprpPanel.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tpParams.ResumeLayout(false);
			this.tpLocals.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			setTab(tabControl1.SelectedIndex);
		}

		private void ListView_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			setIndex((this.lvCurrent.SelectedIndices.Count > 0) ? this.lvCurrent.SelectedIndices[0] : -1);
		}

		private void ListView_ItemActivate(object sender, System.EventArgs e)
		{
			this.tbLabel.Focus();
		}


		private void btnCommit_Click(object sender, System.EventArgs e)
		{
			this.Commit();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Cancel();
			this.tbLabel.SelectAll();
			this.tbLabel.Focus();
		}


		private void btnStrPrev_Click(object sender, System.EventArgs e)
		{
			setIndex(index - 1);
		}

		private void btnStrNext_Click(object sender, System.EventArgs e)
		{
			setIndex(index + 1);
		}

		private void btnTabPrev_Click(object sender, System.EventArgs e)
		{
			this.setTab(tab - 1);
		}

		private void btnTabNext_Click(object sender, System.EventArgs e)
		{
			this.setTab(tab + 1);
		}


		private void btnStrAdd_Click(object sender, System.EventArgs e)
		{
			this.TPRPItemAdd();
			this.tbLabel.SelectAll();
			this.tbLabel.Focus();
		}

		private void btnStrDelete_Click(object sender, System.EventArgs e)
		{
			this.TPRPItemDelete();
		}


		private void tbText_Enter(object sender, System.EventArgs e)
		{
			((TextBox)sender).SelectAll();
		}

		private void tbText_TextChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			internalchg = true;
			switch(alText.IndexOf(sender))
			{
				case 0: wrapper.FileName = ((TextBox)sender).Text; break;
				case 1: lvCurrent.SelectedItems[0].SubItems[1].Text = currentItem.Label = ((TextBox)sender).Text; break;
			}
			internalchg = false;
		}


		private void hex32_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex32_IsValid(sender)) return;

			internalchg = true;
			uint val = Convert.ToUInt32(((TextBox)sender).Text, 16);
			switch (alHex32.IndexOf(sender))
			{
				case 0: wrapper.Version = val; break;
			}
			internalchg = false;
		}

		private void hex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex32_IsValid(sender)) return;
			e.Cancel = true;
			hex32_Validated(sender, null);
		}

		private void hex32_Validated(object sender, System.EventArgs e)
		{
			uint val = 0;
			switch (alHex32.IndexOf(sender))
			{
				case 0: val = wrapper.Version; break;
			}

			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + Helper.HexString(val);
			internalchg = origstate;
		}

	}
}
