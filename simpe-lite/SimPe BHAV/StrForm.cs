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
using SimPe.PackedFiles.Wrapper;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for StrForm.
	/// </summary>
	public class StrForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables
		private System.Windows.Forms.Panel strPanel;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.Label lbFilename;
		private System.Windows.Forms.TextBox tbFilename;
		private System.Windows.Forms.Label lbFormat;
		private System.Windows.Forms.TextBox tbFormat;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.Label lbStringNum;
		private System.Windows.Forms.Panel pnLists;
		private System.Windows.Forms.Label lbPlugin;
		private System.Windows.Forms.Button btnStrDelete;
		private System.Windows.Forms.Button btnStrAdd;
		private System.Windows.Forms.ListBox lbxLngDefault;
		private System.Windows.Forms.ListBox lbxLngCurrent;
		private System.Windows.Forms.Button btnClearAll;
		private System.Windows.Forms.Label lbLngSelect;
		private System.Windows.Forms.ComboBox cbLngSelect;
		private System.Windows.Forms.Button btnLngNext;
		private System.Windows.Forms.Button btnLngPrev;
		private System.Windows.Forms.Button btnLngClear;
		private System.Windows.Forms.RichTextBox rtbTitle;
		private System.Windows.Forms.RichTextBox rtbDescription;
		private System.Windows.Forms.Label lbLngDefault;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public StrForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			Control[] cs = { btnCommit, btnImport, btnExport };
			int left = this.strPanel.Width;
			for (int i = 0; i < cs.Length; i++)
				left = cs[i].Left = left - (cs[i].Width + 4);
			btnImport.Visible = btnExport.Visible = false;

			Control[] af = { tbFormat };
			alHex16 = new ArrayList(af);

			Control[] at = { tbFilename };
			alTextBox = new ArrayList(at);

			Control[] ar = { rtbTitle, rtbDescription };
			alRichTextBox = new ArrayList(ar);
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


		#region StrForm
		private Str wrapper;
		private bool setHandler = false;
		private bool internalchg;

		private ArrayList alHex16 = null;
		private ArrayList alTextBox = null;
		private ArrayList alRichTextBox = null;

		private byte lid = 1;
		private int index = -1;
		private int count = 0;

		private bool hex16_IsValid(object sender)
		{
			if (alHex16.IndexOf(sender) < 0)
				throw new Exception("hex16_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToUInt16(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private void displayStrItem()
		{
			if (internalchg) return;

			internalchg = true;
			this.lbStringNum.Text = this.rtbDescription.Text = this.rtbTitle.Text = "";
			this.btnStrDelete.Enabled = this.rtbDescription.Enabled = this.rtbTitle.Enabled = false;

			StrItem s = wrapper[lid, index];
			if (s != null)
			{
				this.lbStringNum.Text = "Lang " + ((SimPe.Data.MetaData.Languages)lid).ToString() + ", String 0x" + Helper.HexString((ushort)index);
				this.rtbTitle.Text = s.Title;
				this.rtbDescription.Text = s.Description;
				this.btnStrDelete.Enabled = this.rtbDescription.Enabled = this.rtbTitle.Enabled = true;
				this.rtbTitle.SelectAll();
				this.rtbDescription.SelectAll();
			}
			internalchg = false;
		}

		private void populateLbx(ListBox lbx, byte l)
		{
			lbx.SelectedIndex = -1;
			lbx.Items.Clear();
			if (l != 0)
			{
				while(count > 0 && wrapper[l, count-1] == null) wrapper.Add(l, "", "");
				StrItem[] s = wrapper[l];
				for (ushort i = 0; i < count; i++)
					lbx.Items.Add("0x" + Helper.HexString(i) + ": " + s[i]);
			}
		}

		private void updateLists()
		{
			wrapper.CleanUp();

			this.cbLngSelect.SelectedIndex = -1;
			this.cbLngSelect.Items.Clear();
			bool onlyDefault = true;
			for (byte i = 2; i < 44; i++)
			{
				bool empty = wrapper[i].Length == 0;
				this.cbLngSelect.Items.Add(((SimPe.Data.MetaData.Languages)i).ToString() + (empty ? " (empty)" : ""));
				if (!empty) onlyDefault = false;
			}
			this.btnClearAll.Enabled = !onlyDefault;

			count = 0;
			for (byte i = 1; i < 44; i++) count = Math.Max(count, wrapper[i].Length);

			populateLbx(this.lbxLngDefault, 1);
			populateLbx(this.lbxLngCurrent, (byte)((this.cbLngSelect.SelectedIndex != -1) ? this.cbLngSelect.SelectedIndex + 2 : 0));
		}

		#endregion

		#region IPackedFileUI Member
		/// <summary>
		/// Returns the Panel that will be displayed within SimPe
		/// </summary>
		public System.Windows.Forms.Control GUIHandle { get { return strPanel; } }

		/// <summary>
		/// Is called by SimPe (through the Wrapper) when the Panel is going to be displayed, so
		/// you should update the Data displayed by the Panel with the Attributes stored in the
		/// passed Wrapper.
		/// </summary>
		/// <param name="wrp">The Attributes of this Wrapper have to be displayed</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (Str) wrp;
			this.WrapperChanged(wrapper, null);

			internalchg = true;
			updateLists();
			internalchg = false;

			displayStrItem();
			this.btnLngClear.Enabled = false;
			this.btnLngClear.Text = "Clear Lang";

			if (index < 0) index = 0;
			if (index >= count) index = count - 1;
			this.cbLngSelect.SelectedIndex = 0;
			this.lbxLngDefault.SelectedIndex = index;

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
			this.tbFilename.Text = wrapper.FileName;
			this.tbFormat.Text = "0x"+Helper.HexString(wrapper.Format);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StrForm));
			this.strPanel = new System.Windows.Forms.Panel();
			this.lbLngDefault = new System.Windows.Forms.Label();
			this.rtbDescription = new System.Windows.Forms.RichTextBox();
			this.rtbTitle = new System.Windows.Forms.RichTextBox();
			this.btnLngNext = new System.Windows.Forms.Button();
			this.btnLngPrev = new System.Windows.Forms.Button();
			this.btnLngClear = new System.Windows.Forms.Button();
			this.cbLngSelect = new System.Windows.Forms.ComboBox();
			this.lbLngSelect = new System.Windows.Forms.Label();
			this.btnClearAll = new System.Windows.Forms.Button();
			this.pnLists = new System.Windows.Forms.Panel();
			this.lbxLngDefault = new System.Windows.Forms.ListBox();
			this.lbxLngCurrent = new System.Windows.Forms.ListBox();
			this.lbStringNum = new System.Windows.Forms.Label();
			this.tbFilename = new System.Windows.Forms.TextBox();
			this.lbFilename = new System.Windows.Forms.Label();
			this.lbPlugin = new System.Windows.Forms.Label();
			this.btnCommit = new System.Windows.Forms.Button();
			this.lbFormat = new System.Windows.Forms.Label();
			this.tbFormat = new System.Windows.Forms.TextBox();
			this.btnImport = new System.Windows.Forms.Button();
			this.btnExport = new System.Windows.Forms.Button();
			this.btnStrDelete = new System.Windows.Forms.Button();
			this.btnStrAdd = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.strPanel.SuspendLayout();
			this.pnLists.SuspendLayout();
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
			this.strPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("strPanel.BackgroundImage")));
			this.strPanel.Controls.Add(this.label2);
			this.strPanel.Controls.Add(this.label1);
			this.strPanel.Controls.Add(this.lbLngDefault);
			this.strPanel.Controls.Add(this.rtbDescription);
			this.strPanel.Controls.Add(this.rtbTitle);
			this.strPanel.Controls.Add(this.btnLngNext);
			this.strPanel.Controls.Add(this.btnLngPrev);
			this.strPanel.Controls.Add(this.btnLngClear);
			this.strPanel.Controls.Add(this.cbLngSelect);
			this.strPanel.Controls.Add(this.lbLngSelect);
			this.strPanel.Controls.Add(this.btnClearAll);
			this.strPanel.Controls.Add(this.pnLists);
			this.strPanel.Controls.Add(this.lbStringNum);
			this.strPanel.Controls.Add(this.tbFilename);
			this.strPanel.Controls.Add(this.lbFilename);
			this.strPanel.Controls.Add(this.lbPlugin);
			this.strPanel.Controls.Add(this.btnCommit);
			this.strPanel.Controls.Add(this.lbFormat);
			this.strPanel.Controls.Add(this.tbFormat);
			this.strPanel.Controls.Add(this.btnImport);
			this.strPanel.Controls.Add(this.btnExport);
			this.strPanel.Controls.Add(this.btnStrDelete);
			this.strPanel.Controls.Add(this.btnStrAdd);
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
			// lbLngDefault
			// 
			this.lbLngDefault.AccessibleDescription = resources.GetString("lbLngDefault.AccessibleDescription");
			this.lbLngDefault.AccessibleName = resources.GetString("lbLngDefault.AccessibleName");
			this.lbLngDefault.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbLngDefault.Anchor")));
			this.lbLngDefault.AutoSize = ((bool)(resources.GetObject("lbLngDefault.AutoSize")));
			this.lbLngDefault.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbLngDefault.Dock")));
			this.lbLngDefault.Enabled = ((bool)(resources.GetObject("lbLngDefault.Enabled")));
			this.lbLngDefault.Font = ((System.Drawing.Font)(resources.GetObject("lbLngDefault.Font")));
			this.lbLngDefault.Image = ((System.Drawing.Image)(resources.GetObject("lbLngDefault.Image")));
			this.lbLngDefault.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbLngDefault.ImageAlign")));
			this.lbLngDefault.ImageIndex = ((int)(resources.GetObject("lbLngDefault.ImageIndex")));
			this.lbLngDefault.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbLngDefault.ImeMode")));
			this.lbLngDefault.Location = ((System.Drawing.Point)(resources.GetObject("lbLngDefault.Location")));
			this.lbLngDefault.Name = "lbLngDefault";
			this.lbLngDefault.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbLngDefault.RightToLeft")));
			this.lbLngDefault.Size = ((System.Drawing.Size)(resources.GetObject("lbLngDefault.Size")));
			this.lbLngDefault.TabIndex = ((int)(resources.GetObject("lbLngDefault.TabIndex")));
			this.lbLngDefault.Text = resources.GetString("lbLngDefault.Text");
			this.lbLngDefault.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbLngDefault.TextAlign")));
			this.lbLngDefault.Visible = ((bool)(resources.GetObject("lbLngDefault.Visible")));
			// 
			// rtbDescription
			// 
			this.rtbDescription.AccessibleDescription = resources.GetString("rtbDescription.AccessibleDescription");
			this.rtbDescription.AccessibleName = resources.GetString("rtbDescription.AccessibleName");
			this.rtbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rtbDescription.Anchor")));
			this.rtbDescription.AutoSize = ((bool)(resources.GetObject("rtbDescription.AutoSize")));
			this.rtbDescription.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rtbDescription.BackgroundImage")));
			this.rtbDescription.BulletIndent = ((int)(resources.GetObject("rtbDescription.BulletIndent")));
			this.rtbDescription.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rtbDescription.Dock")));
			this.rtbDescription.Enabled = ((bool)(resources.GetObject("rtbDescription.Enabled")));
			this.rtbDescription.Font = ((System.Drawing.Font)(resources.GetObject("rtbDescription.Font")));
			this.rtbDescription.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rtbDescription.ImeMode")));
			this.rtbDescription.Location = ((System.Drawing.Point)(resources.GetObject("rtbDescription.Location")));
			this.rtbDescription.MaxLength = ((int)(resources.GetObject("rtbDescription.MaxLength")));
			this.rtbDescription.Multiline = ((bool)(resources.GetObject("rtbDescription.Multiline")));
			this.rtbDescription.Name = "rtbDescription";
			this.rtbDescription.RightMargin = ((int)(resources.GetObject("rtbDescription.RightMargin")));
			this.rtbDescription.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rtbDescription.RightToLeft")));
			this.rtbDescription.ScrollBars = ((System.Windows.Forms.RichTextBoxScrollBars)(resources.GetObject("rtbDescription.ScrollBars")));
			this.rtbDescription.Size = ((System.Drawing.Size)(resources.GetObject("rtbDescription.Size")));
			this.rtbDescription.TabIndex = ((int)(resources.GetObject("rtbDescription.TabIndex")));
			this.rtbDescription.Text = resources.GetString("rtbDescription.Text");
			this.rtbDescription.Visible = ((bool)(resources.GetObject("rtbDescription.Visible")));
			this.rtbDescription.WordWrap = ((bool)(resources.GetObject("rtbDescription.WordWrap")));
			this.rtbDescription.ZoomFactor = ((System.Single)(resources.GetObject("rtbDescription.ZoomFactor")));
			this.rtbDescription.Validated += new System.EventHandler(this.richTextBox_Validated);
			this.rtbDescription.TextChanged += new System.EventHandler(this.richTextBox_TextChanged);
			// 
			// rtbTitle
			// 
			this.rtbTitle.AccessibleDescription = resources.GetString("rtbTitle.AccessibleDescription");
			this.rtbTitle.AccessibleName = resources.GetString("rtbTitle.AccessibleName");
			this.rtbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rtbTitle.Anchor")));
			this.rtbTitle.AutoSize = ((bool)(resources.GetObject("rtbTitle.AutoSize")));
			this.rtbTitle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rtbTitle.BackgroundImage")));
			this.rtbTitle.BulletIndent = ((int)(resources.GetObject("rtbTitle.BulletIndent")));
			this.rtbTitle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rtbTitle.Dock")));
			this.rtbTitle.Enabled = ((bool)(resources.GetObject("rtbTitle.Enabled")));
			this.rtbTitle.Font = ((System.Drawing.Font)(resources.GetObject("rtbTitle.Font")));
			this.rtbTitle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rtbTitle.ImeMode")));
			this.rtbTitle.Location = ((System.Drawing.Point)(resources.GetObject("rtbTitle.Location")));
			this.rtbTitle.MaxLength = ((int)(resources.GetObject("rtbTitle.MaxLength")));
			this.rtbTitle.Multiline = ((bool)(resources.GetObject("rtbTitle.Multiline")));
			this.rtbTitle.Name = "rtbTitle";
			this.rtbTitle.RightMargin = ((int)(resources.GetObject("rtbTitle.RightMargin")));
			this.rtbTitle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rtbTitle.RightToLeft")));
			this.rtbTitle.ScrollBars = ((System.Windows.Forms.RichTextBoxScrollBars)(resources.GetObject("rtbTitle.ScrollBars")));
			this.rtbTitle.Size = ((System.Drawing.Size)(resources.GetObject("rtbTitle.Size")));
			this.rtbTitle.TabIndex = ((int)(resources.GetObject("rtbTitle.TabIndex")));
			this.rtbTitle.Text = resources.GetString("rtbTitle.Text");
			this.rtbTitle.Visible = ((bool)(resources.GetObject("rtbTitle.Visible")));
			this.rtbTitle.WordWrap = ((bool)(resources.GetObject("rtbTitle.WordWrap")));
			this.rtbTitle.ZoomFactor = ((System.Single)(resources.GetObject("rtbTitle.ZoomFactor")));
			this.rtbTitle.Validated += new System.EventHandler(this.richTextBox_Validated);
			this.rtbTitle.TextChanged += new System.EventHandler(this.richTextBox_TextChanged);
			// 
			// btnLngNext
			// 
			this.btnLngNext.AccessibleDescription = resources.GetString("btnLngNext.AccessibleDescription");
			this.btnLngNext.AccessibleName = resources.GetString("btnLngNext.AccessibleName");
			this.btnLngNext.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnLngNext.Anchor")));
			this.btnLngNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLngNext.BackgroundImage")));
			this.btnLngNext.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnLngNext.Dock")));
			this.btnLngNext.Enabled = ((bool)(resources.GetObject("btnLngNext.Enabled")));
			this.btnLngNext.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnLngNext.FlatStyle")));
			this.btnLngNext.Font = ((System.Drawing.Font)(resources.GetObject("btnLngNext.Font")));
			this.btnLngNext.Image = ((System.Drawing.Image)(resources.GetObject("btnLngNext.Image")));
			this.btnLngNext.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnLngNext.ImageAlign")));
			this.btnLngNext.ImageIndex = ((int)(resources.GetObject("btnLngNext.ImageIndex")));
			this.btnLngNext.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnLngNext.ImeMode")));
			this.btnLngNext.Location = ((System.Drawing.Point)(resources.GetObject("btnLngNext.Location")));
			this.btnLngNext.Name = "btnLngNext";
			this.btnLngNext.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnLngNext.RightToLeft")));
			this.btnLngNext.Size = ((System.Drawing.Size)(resources.GetObject("btnLngNext.Size")));
			this.btnLngNext.TabIndex = ((int)(resources.GetObject("btnLngNext.TabIndex")));
			this.btnLngNext.Text = resources.GetString("btnLngNext.Text");
			this.btnLngNext.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnLngNext.TextAlign")));
			this.btnLngNext.Visible = ((bool)(resources.GetObject("btnLngNext.Visible")));
			this.btnLngNext.Click += new System.EventHandler(this.btnLngNext_Click);
			// 
			// btnLngPrev
			// 
			this.btnLngPrev.AccessibleDescription = resources.GetString("btnLngPrev.AccessibleDescription");
			this.btnLngPrev.AccessibleName = resources.GetString("btnLngPrev.AccessibleName");
			this.btnLngPrev.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnLngPrev.Anchor")));
			this.btnLngPrev.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLngPrev.BackgroundImage")));
			this.btnLngPrev.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnLngPrev.Dock")));
			this.btnLngPrev.Enabled = ((bool)(resources.GetObject("btnLngPrev.Enabled")));
			this.btnLngPrev.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnLngPrev.FlatStyle")));
			this.btnLngPrev.Font = ((System.Drawing.Font)(resources.GetObject("btnLngPrev.Font")));
			this.btnLngPrev.Image = ((System.Drawing.Image)(resources.GetObject("btnLngPrev.Image")));
			this.btnLngPrev.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnLngPrev.ImageAlign")));
			this.btnLngPrev.ImageIndex = ((int)(resources.GetObject("btnLngPrev.ImageIndex")));
			this.btnLngPrev.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnLngPrev.ImeMode")));
			this.btnLngPrev.Location = ((System.Drawing.Point)(resources.GetObject("btnLngPrev.Location")));
			this.btnLngPrev.Name = "btnLngPrev";
			this.btnLngPrev.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnLngPrev.RightToLeft")));
			this.btnLngPrev.Size = ((System.Drawing.Size)(resources.GetObject("btnLngPrev.Size")));
			this.btnLngPrev.TabIndex = ((int)(resources.GetObject("btnLngPrev.TabIndex")));
			this.btnLngPrev.Text = resources.GetString("btnLngPrev.Text");
			this.btnLngPrev.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnLngPrev.TextAlign")));
			this.btnLngPrev.Visible = ((bool)(resources.GetObject("btnLngPrev.Visible")));
			this.btnLngPrev.Click += new System.EventHandler(this.btnLngPrev_Click);
			// 
			// btnLngClear
			// 
			this.btnLngClear.AccessibleDescription = resources.GetString("btnLngClear.AccessibleDescription");
			this.btnLngClear.AccessibleName = resources.GetString("btnLngClear.AccessibleName");
			this.btnLngClear.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnLngClear.Anchor")));
			this.btnLngClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLngClear.BackgroundImage")));
			this.btnLngClear.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnLngClear.Dock")));
			this.btnLngClear.Enabled = ((bool)(resources.GetObject("btnLngClear.Enabled")));
			this.btnLngClear.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnLngClear.FlatStyle")));
			this.btnLngClear.Font = ((System.Drawing.Font)(resources.GetObject("btnLngClear.Font")));
			this.btnLngClear.Image = ((System.Drawing.Image)(resources.GetObject("btnLngClear.Image")));
			this.btnLngClear.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnLngClear.ImageAlign")));
			this.btnLngClear.ImageIndex = ((int)(resources.GetObject("btnLngClear.ImageIndex")));
			this.btnLngClear.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnLngClear.ImeMode")));
			this.btnLngClear.Location = ((System.Drawing.Point)(resources.GetObject("btnLngClear.Location")));
			this.btnLngClear.Name = "btnLngClear";
			this.btnLngClear.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnLngClear.RightToLeft")));
			this.btnLngClear.Size = ((System.Drawing.Size)(resources.GetObject("btnLngClear.Size")));
			this.btnLngClear.TabIndex = ((int)(resources.GetObject("btnLngClear.TabIndex")));
			this.btnLngClear.Text = resources.GetString("btnLngClear.Text");
			this.btnLngClear.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnLngClear.TextAlign")));
			this.btnLngClear.Visible = ((bool)(resources.GetObject("btnLngClear.Visible")));
			this.btnLngClear.Click += new System.EventHandler(this.btnLngClear_Click);
			// 
			// cbLngSelect
			// 
			this.cbLngSelect.AccessibleDescription = resources.GetString("cbLngSelect.AccessibleDescription");
			this.cbLngSelect.AccessibleName = resources.GetString("cbLngSelect.AccessibleName");
			this.cbLngSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbLngSelect.Anchor")));
			this.cbLngSelect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbLngSelect.BackgroundImage")));
			this.cbLngSelect.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbLngSelect.Dock")));
			this.cbLngSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbLngSelect.Enabled = ((bool)(resources.GetObject("cbLngSelect.Enabled")));
			this.cbLngSelect.Font = ((System.Drawing.Font)(resources.GetObject("cbLngSelect.Font")));
			this.cbLngSelect.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbLngSelect.ImeMode")));
			this.cbLngSelect.IntegralHeight = ((bool)(resources.GetObject("cbLngSelect.IntegralHeight")));
			this.cbLngSelect.ItemHeight = ((int)(resources.GetObject("cbLngSelect.ItemHeight")));
			this.cbLngSelect.Location = ((System.Drawing.Point)(resources.GetObject("cbLngSelect.Location")));
			this.cbLngSelect.MaxDropDownItems = ((int)(resources.GetObject("cbLngSelect.MaxDropDownItems")));
			this.cbLngSelect.MaxLength = ((int)(resources.GetObject("cbLngSelect.MaxLength")));
			this.cbLngSelect.Name = "cbLngSelect";
			this.cbLngSelect.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbLngSelect.RightToLeft")));
			this.cbLngSelect.Size = ((System.Drawing.Size)(resources.GetObject("cbLngSelect.Size")));
			this.cbLngSelect.TabIndex = ((int)(resources.GetObject("cbLngSelect.TabIndex")));
			this.cbLngSelect.Text = resources.GetString("cbLngSelect.Text");
			this.cbLngSelect.Visible = ((bool)(resources.GetObject("cbLngSelect.Visible")));
			this.cbLngSelect.SelectedIndexChanged += new System.EventHandler(this.cbLngSelect_SelectedIndexChanged);
			// 
			// lbLngSelect
			// 
			this.lbLngSelect.AccessibleDescription = resources.GetString("lbLngSelect.AccessibleDescription");
			this.lbLngSelect.AccessibleName = resources.GetString("lbLngSelect.AccessibleName");
			this.lbLngSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbLngSelect.Anchor")));
			this.lbLngSelect.AutoSize = ((bool)(resources.GetObject("lbLngSelect.AutoSize")));
			this.lbLngSelect.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbLngSelect.Dock")));
			this.lbLngSelect.Enabled = ((bool)(resources.GetObject("lbLngSelect.Enabled")));
			this.lbLngSelect.Font = ((System.Drawing.Font)(resources.GetObject("lbLngSelect.Font")));
			this.lbLngSelect.Image = ((System.Drawing.Image)(resources.GetObject("lbLngSelect.Image")));
			this.lbLngSelect.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbLngSelect.ImageAlign")));
			this.lbLngSelect.ImageIndex = ((int)(resources.GetObject("lbLngSelect.ImageIndex")));
			this.lbLngSelect.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbLngSelect.ImeMode")));
			this.lbLngSelect.Location = ((System.Drawing.Point)(resources.GetObject("lbLngSelect.Location")));
			this.lbLngSelect.Name = "lbLngSelect";
			this.lbLngSelect.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbLngSelect.RightToLeft")));
			this.lbLngSelect.Size = ((System.Drawing.Size)(resources.GetObject("lbLngSelect.Size")));
			this.lbLngSelect.TabIndex = ((int)(resources.GetObject("lbLngSelect.TabIndex")));
			this.lbLngSelect.Text = resources.GetString("lbLngSelect.Text");
			this.lbLngSelect.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbLngSelect.TextAlign")));
			this.lbLngSelect.Visible = ((bool)(resources.GetObject("lbLngSelect.Visible")));
			// 
			// btnClearAll
			// 
			this.btnClearAll.AccessibleDescription = resources.GetString("btnClearAll.AccessibleDescription");
			this.btnClearAll.AccessibleName = resources.GetString("btnClearAll.AccessibleName");
			this.btnClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnClearAll.Anchor")));
			this.btnClearAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearAll.BackgroundImage")));
			this.btnClearAll.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnClearAll.Dock")));
			this.btnClearAll.Enabled = ((bool)(resources.GetObject("btnClearAll.Enabled")));
			this.btnClearAll.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnClearAll.FlatStyle")));
			this.btnClearAll.Font = ((System.Drawing.Font)(resources.GetObject("btnClearAll.Font")));
			this.btnClearAll.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAll.Image")));
			this.btnClearAll.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClearAll.ImageAlign")));
			this.btnClearAll.ImageIndex = ((int)(resources.GetObject("btnClearAll.ImageIndex")));
			this.btnClearAll.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnClearAll.ImeMode")));
			this.btnClearAll.Location = ((System.Drawing.Point)(resources.GetObject("btnClearAll.Location")));
			this.btnClearAll.Name = "btnClearAll";
			this.btnClearAll.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnClearAll.RightToLeft")));
			this.btnClearAll.Size = ((System.Drawing.Size)(resources.GetObject("btnClearAll.Size")));
			this.btnClearAll.TabIndex = ((int)(resources.GetObject("btnClearAll.TabIndex")));
			this.btnClearAll.Text = resources.GetString("btnClearAll.Text");
			this.btnClearAll.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClearAll.TextAlign")));
			this.btnClearAll.Visible = ((bool)(resources.GetObject("btnClearAll.Visible")));
			this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
			// 
			// pnLists
			// 
			this.pnLists.AccessibleDescription = resources.GetString("pnLists.AccessibleDescription");
			this.pnLists.AccessibleName = resources.GetString("pnLists.AccessibleName");
			this.pnLists.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pnLists.Anchor")));
			this.pnLists.AutoScroll = ((bool)(resources.GetObject("pnLists.AutoScroll")));
			this.pnLists.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("pnLists.AutoScrollMargin")));
			this.pnLists.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("pnLists.AutoScrollMinSize")));
			this.pnLists.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnLists.BackgroundImage")));
			this.pnLists.Controls.Add(this.lbxLngDefault);
			this.pnLists.Controls.Add(this.lbxLngCurrent);
			this.pnLists.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnLists.Dock")));
			this.pnLists.Enabled = ((bool)(resources.GetObject("pnLists.Enabled")));
			this.pnLists.Font = ((System.Drawing.Font)(resources.GetObject("pnLists.Font")));
			this.pnLists.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnLists.ImeMode")));
			this.pnLists.Location = ((System.Drawing.Point)(resources.GetObject("pnLists.Location")));
			this.pnLists.Name = "pnLists";
			this.pnLists.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnLists.RightToLeft")));
			this.pnLists.Size = ((System.Drawing.Size)(resources.GetObject("pnLists.Size")));
			this.pnLists.TabIndex = ((int)(resources.GetObject("pnLists.TabIndex")));
			this.pnLists.Text = resources.GetString("pnLists.Text");
			this.pnLists.Visible = ((bool)(resources.GetObject("pnLists.Visible")));
			this.pnLists.Resize += new System.EventHandler(this.pnLists_Resize);
			// 
			// lbxLngDefault
			// 
			this.lbxLngDefault.AccessibleDescription = resources.GetString("lbxLngDefault.AccessibleDescription");
			this.lbxLngDefault.AccessibleName = resources.GetString("lbxLngDefault.AccessibleName");
			this.lbxLngDefault.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbxLngDefault.Anchor")));
			this.lbxLngDefault.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lbxLngDefault.BackgroundImage")));
			this.lbxLngDefault.ColumnWidth = ((int)(resources.GetObject("lbxLngDefault.ColumnWidth")));
			this.lbxLngDefault.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbxLngDefault.Dock")));
			this.lbxLngDefault.Enabled = ((bool)(resources.GetObject("lbxLngDefault.Enabled")));
			this.lbxLngDefault.Font = ((System.Drawing.Font)(resources.GetObject("lbxLngDefault.Font")));
			this.lbxLngDefault.HorizontalExtent = ((int)(resources.GetObject("lbxLngDefault.HorizontalExtent")));
			this.lbxLngDefault.HorizontalScrollbar = ((bool)(resources.GetObject("lbxLngDefault.HorizontalScrollbar")));
			this.lbxLngDefault.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbxLngDefault.ImeMode")));
			this.lbxLngDefault.IntegralHeight = ((bool)(resources.GetObject("lbxLngDefault.IntegralHeight")));
			this.lbxLngDefault.ItemHeight = ((int)(resources.GetObject("lbxLngDefault.ItemHeight")));
			this.lbxLngDefault.Location = ((System.Drawing.Point)(resources.GetObject("lbxLngDefault.Location")));
			this.lbxLngDefault.Name = "lbxLngDefault";
			this.lbxLngDefault.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbxLngDefault.RightToLeft")));
			this.lbxLngDefault.ScrollAlwaysVisible = ((bool)(resources.GetObject("lbxLngDefault.ScrollAlwaysVisible")));
			this.lbxLngDefault.Size = ((System.Drawing.Size)(resources.GetObject("lbxLngDefault.Size")));
			this.lbxLngDefault.TabIndex = ((int)(resources.GetObject("lbxLngDefault.TabIndex")));
			this.lbxLngDefault.Visible = ((bool)(resources.GetObject("lbxLngDefault.Visible")));
			this.lbxLngDefault.SelectedIndexChanged += new System.EventHandler(this.lbxLngDefault_SelectedIndexChanged);
			// 
			// lbxLngCurrent
			// 
			this.lbxLngCurrent.AccessibleDescription = resources.GetString("lbxLngCurrent.AccessibleDescription");
			this.lbxLngCurrent.AccessibleName = resources.GetString("lbxLngCurrent.AccessibleName");
			this.lbxLngCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbxLngCurrent.Anchor")));
			this.lbxLngCurrent.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lbxLngCurrent.BackgroundImage")));
			this.lbxLngCurrent.ColumnWidth = ((int)(resources.GetObject("lbxLngCurrent.ColumnWidth")));
			this.lbxLngCurrent.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbxLngCurrent.Dock")));
			this.lbxLngCurrent.Enabled = ((bool)(resources.GetObject("lbxLngCurrent.Enabled")));
			this.lbxLngCurrent.Font = ((System.Drawing.Font)(resources.GetObject("lbxLngCurrent.Font")));
			this.lbxLngCurrent.HorizontalExtent = ((int)(resources.GetObject("lbxLngCurrent.HorizontalExtent")));
			this.lbxLngCurrent.HorizontalScrollbar = ((bool)(resources.GetObject("lbxLngCurrent.HorizontalScrollbar")));
			this.lbxLngCurrent.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbxLngCurrent.ImeMode")));
			this.lbxLngCurrent.IntegralHeight = ((bool)(resources.GetObject("lbxLngCurrent.IntegralHeight")));
			this.lbxLngCurrent.ItemHeight = ((int)(resources.GetObject("lbxLngCurrent.ItemHeight")));
			this.lbxLngCurrent.Location = ((System.Drawing.Point)(resources.GetObject("lbxLngCurrent.Location")));
			this.lbxLngCurrent.Name = "lbxLngCurrent";
			this.lbxLngCurrent.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbxLngCurrent.RightToLeft")));
			this.lbxLngCurrent.ScrollAlwaysVisible = ((bool)(resources.GetObject("lbxLngCurrent.ScrollAlwaysVisible")));
			this.lbxLngCurrent.Size = ((System.Drawing.Size)(resources.GetObject("lbxLngCurrent.Size")));
			this.lbxLngCurrent.TabIndex = ((int)(resources.GetObject("lbxLngCurrent.TabIndex")));
			this.lbxLngCurrent.Visible = ((bool)(resources.GetObject("lbxLngCurrent.Visible")));
			this.lbxLngCurrent.SelectedIndexChanged += new System.EventHandler(this.lbxLngCurrent_SelectedIndexChanged);
			// 
			// lbStringNum
			// 
			this.lbStringNum.AccessibleDescription = resources.GetString("lbStringNum.AccessibleDescription");
			this.lbStringNum.AccessibleName = resources.GetString("lbStringNum.AccessibleName");
			this.lbStringNum.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbStringNum.Anchor")));
			this.lbStringNum.AutoSize = ((bool)(resources.GetObject("lbStringNum.AutoSize")));
			this.lbStringNum.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbStringNum.Dock")));
			this.lbStringNum.Enabled = ((bool)(resources.GetObject("lbStringNum.Enabled")));
			this.lbStringNum.Font = ((System.Drawing.Font)(resources.GetObject("lbStringNum.Font")));
			this.lbStringNum.Image = ((System.Drawing.Image)(resources.GetObject("lbStringNum.Image")));
			this.lbStringNum.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbStringNum.ImageAlign")));
			this.lbStringNum.ImageIndex = ((int)(resources.GetObject("lbStringNum.ImageIndex")));
			this.lbStringNum.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbStringNum.ImeMode")));
			this.lbStringNum.Location = ((System.Drawing.Point)(resources.GetObject("lbStringNum.Location")));
			this.lbStringNum.Name = "lbStringNum";
			this.lbStringNum.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbStringNum.RightToLeft")));
			this.lbStringNum.Size = ((System.Drawing.Size)(resources.GetObject("lbStringNum.Size")));
			this.lbStringNum.TabIndex = ((int)(resources.GetObject("lbStringNum.TabIndex")));
			this.lbStringNum.Text = resources.GetString("lbStringNum.Text");
			this.lbStringNum.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbStringNum.TextAlign")));
			this.lbStringNum.Visible = ((bool)(resources.GetObject("lbStringNum.Visible")));
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
			this.tbFilename.Validated += new System.EventHandler(this.textBox_Validated);
			this.tbFilename.TextChanged += new System.EventHandler(this.textBox_TextChanged);
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
			// lbPlugin
			// 
			this.lbPlugin.AccessibleDescription = resources.GetString("lbPlugin.AccessibleDescription");
			this.lbPlugin.AccessibleName = resources.GetString("lbPlugin.AccessibleName");
			this.lbPlugin.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbPlugin.Anchor")));
			this.lbPlugin.AutoSize = ((bool)(resources.GetObject("lbPlugin.AutoSize")));
			this.lbPlugin.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.lbPlugin.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbPlugin.Dock")));
			this.lbPlugin.Enabled = ((bool)(resources.GetObject("lbPlugin.Enabled")));
			this.lbPlugin.Font = ((System.Drawing.Font)(resources.GetObject("lbPlugin.Font")));
			this.lbPlugin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.lbPlugin.Image = ((System.Drawing.Image)(resources.GetObject("lbPlugin.Image")));
			this.lbPlugin.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbPlugin.ImageAlign")));
			this.lbPlugin.ImageIndex = ((int)(resources.GetObject("lbPlugin.ImageIndex")));
			this.lbPlugin.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbPlugin.ImeMode")));
			this.lbPlugin.Location = ((System.Drawing.Point)(resources.GetObject("lbPlugin.Location")));
			this.lbPlugin.Name = "lbPlugin";
			this.lbPlugin.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbPlugin.RightToLeft")));
			this.lbPlugin.Size = ((System.Drawing.Size)(resources.GetObject("lbPlugin.Size")));
			this.lbPlugin.TabIndex = ((int)(resources.GetObject("lbPlugin.TabIndex")));
			this.lbPlugin.Text = resources.GetString("lbPlugin.Text");
			this.lbPlugin.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbPlugin.TextAlign")));
			this.lbPlugin.Visible = ((bool)(resources.GetObject("lbPlugin.Visible")));
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
			// lbFormat
			// 
			this.lbFormat.AccessibleDescription = resources.GetString("lbFormat.AccessibleDescription");
			this.lbFormat.AccessibleName = resources.GetString("lbFormat.AccessibleName");
			this.lbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbFormat.Anchor")));
			this.lbFormat.AutoSize = ((bool)(resources.GetObject("lbFormat.AutoSize")));
			this.lbFormat.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbFormat.Dock")));
			this.lbFormat.Enabled = ((bool)(resources.GetObject("lbFormat.Enabled")));
			this.lbFormat.Font = ((System.Drawing.Font)(resources.GetObject("lbFormat.Font")));
			this.lbFormat.Image = ((System.Drawing.Image)(resources.GetObject("lbFormat.Image")));
			this.lbFormat.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFormat.ImageAlign")));
			this.lbFormat.ImageIndex = ((int)(resources.GetObject("lbFormat.ImageIndex")));
			this.lbFormat.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbFormat.ImeMode")));
			this.lbFormat.Location = ((System.Drawing.Point)(resources.GetObject("lbFormat.Location")));
			this.lbFormat.Name = "lbFormat";
			this.lbFormat.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbFormat.RightToLeft")));
			this.lbFormat.Size = ((System.Drawing.Size)(resources.GetObject("lbFormat.Size")));
			this.lbFormat.TabIndex = ((int)(resources.GetObject("lbFormat.TabIndex")));
			this.lbFormat.Text = resources.GetString("lbFormat.Text");
			this.lbFormat.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFormat.TextAlign")));
			this.lbFormat.Visible = ((bool)(resources.GetObject("lbFormat.Visible")));
			// 
			// tbFormat
			// 
			this.tbFormat.AccessibleDescription = resources.GetString("tbFormat.AccessibleDescription");
			this.tbFormat.AccessibleName = resources.GetString("tbFormat.AccessibleName");
			this.tbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbFormat.Anchor")));
			this.tbFormat.AutoSize = ((bool)(resources.GetObject("tbFormat.AutoSize")));
			this.tbFormat.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbFormat.BackgroundImage")));
			this.tbFormat.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbFormat.Dock")));
			this.tbFormat.Enabled = ((bool)(resources.GetObject("tbFormat.Enabled")));
			this.tbFormat.Font = ((System.Drawing.Font)(resources.GetObject("tbFormat.Font")));
			this.tbFormat.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbFormat.ImeMode")));
			this.tbFormat.Location = ((System.Drawing.Point)(resources.GetObject("tbFormat.Location")));
			this.tbFormat.MaxLength = ((int)(resources.GetObject("tbFormat.MaxLength")));
			this.tbFormat.Multiline = ((bool)(resources.GetObject("tbFormat.Multiline")));
			this.tbFormat.Name = "tbFormat";
			this.tbFormat.PasswordChar = ((char)(resources.GetObject("tbFormat.PasswordChar")));
			this.tbFormat.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbFormat.RightToLeft")));
			this.tbFormat.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbFormat.ScrollBars")));
			this.tbFormat.Size = ((System.Drawing.Size)(resources.GetObject("tbFormat.Size")));
			this.tbFormat.TabIndex = ((int)(resources.GetObject("tbFormat.TabIndex")));
			this.tbFormat.Text = resources.GetString("tbFormat.Text");
			this.tbFormat.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbFormat.TextAlign")));
			this.tbFormat.Visible = ((bool)(resources.GetObject("tbFormat.Visible")));
			this.tbFormat.WordWrap = ((bool)(resources.GetObject("tbFormat.WordWrap")));
			this.tbFormat.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbFormat.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbFormat.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			// 
			// btnImport
			// 
			this.btnImport.AccessibleDescription = resources.GetString("btnImport.AccessibleDescription");
			this.btnImport.AccessibleName = resources.GetString("btnImport.AccessibleName");
			this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnImport.Anchor")));
			this.btnImport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImport.BackgroundImage")));
			this.btnImport.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnImport.Dock")));
			this.btnImport.Enabled = ((bool)(resources.GetObject("btnImport.Enabled")));
			this.btnImport.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnImport.FlatStyle")));
			this.btnImport.Font = ((System.Drawing.Font)(resources.GetObject("btnImport.Font")));
			this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
			this.btnImport.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnImport.ImageAlign")));
			this.btnImport.ImageIndex = ((int)(resources.GetObject("btnImport.ImageIndex")));
			this.btnImport.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnImport.ImeMode")));
			this.btnImport.Location = ((System.Drawing.Point)(resources.GetObject("btnImport.Location")));
			this.btnImport.Name = "btnImport";
			this.btnImport.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnImport.RightToLeft")));
			this.btnImport.Size = ((System.Drawing.Size)(resources.GetObject("btnImport.Size")));
			this.btnImport.TabIndex = ((int)(resources.GetObject("btnImport.TabIndex")));
			this.btnImport.Text = resources.GetString("btnImport.Text");
			this.btnImport.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnImport.TextAlign")));
			this.btnImport.Visible = ((bool)(resources.GetObject("btnImport.Visible")));
			// 
			// btnExport
			// 
			this.btnExport.AccessibleDescription = resources.GetString("btnExport.AccessibleDescription");
			this.btnExport.AccessibleName = resources.GetString("btnExport.AccessibleName");
			this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnExport.Anchor")));
			this.btnExport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExport.BackgroundImage")));
			this.btnExport.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnExport.Dock")));
			this.btnExport.Enabled = ((bool)(resources.GetObject("btnExport.Enabled")));
			this.btnExport.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnExport.FlatStyle")));
			this.btnExport.Font = ((System.Drawing.Font)(resources.GetObject("btnExport.Font")));
			this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
			this.btnExport.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnExport.ImageAlign")));
			this.btnExport.ImageIndex = ((int)(resources.GetObject("btnExport.ImageIndex")));
			this.btnExport.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnExport.ImeMode")));
			this.btnExport.Location = ((System.Drawing.Point)(resources.GetObject("btnExport.Location")));
			this.btnExport.Name = "btnExport";
			this.btnExport.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnExport.RightToLeft")));
			this.btnExport.Size = ((System.Drawing.Size)(resources.GetObject("btnExport.Size")));
			this.btnExport.TabIndex = ((int)(resources.GetObject("btnExport.TabIndex")));
			this.btnExport.Text = resources.GetString("btnExport.Text");
			this.btnExport.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnExport.TextAlign")));
			this.btnExport.Visible = ((bool)(resources.GetObject("btnExport.Visible")));
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
			// StrForm
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
			this.Name = "StrForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.strPanel.ResumeLayout(false);
			this.pnLists.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void pnLists_Resize(object sender, System.EventArgs e)
		{
			this.lbxLngDefault.Left = 0;
			this.lbxLngCurrent.Width = this.lbxLngDefault.Width =
				(this.pnLists.Width / 2) - 4 - 8;
			this.lbxLngCurrent.Left = this.lbxLngDefault.Right + 4;

			this.btnLngPrev.Left = this.lbxLngCurrent.Left + this.pnLists.Left;
			this.cbLngSelect.Left = this.lbLngSelect.Left = this.btnLngPrev.Right;
			this.btnLngNext.Left = this.cbLngSelect.Right;
			this.btnLngClear.Left = this.btnClearAll.Left = this.btnLngNext.Right + 32;

			int minH = (this.lbxLngDefault.Items.Count * this.lbxLngDefault.ItemHeight) + 4 + 20;
			int h = this.pnLists.Height - this.lbxLngDefault.Top;
			this.lbxLngCurrent.Height = this.lbxLngDefault.Height = (h < minH) ? minH : h;

			this.rtbDescription.Width = this.rtbTitle.Width = this.pnLists.Left + this.lbxLngDefault.Width - this.rtbTitle.Left;

			Control[] cs = { tbFormat, lbFormat };
			int left = this.strPanel.Width;
			for (int i = 0; i < cs.Length; i++)
				left = cs[i].Left = left - (cs[i].Width + 4);
			this.lbFilename.Left = this.rtbTitle.Right + 4;
			this.tbFilename.Left = this.lbFilename.Right + 4;
			this.tbFilename.Width = left - (this.tbFilename.Left + 4);
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

			int l = this.cbLngSelect.SelectedIndex;
			bool defsel = this.lbxLngDefault.SelectedIndex != -1;

			bool savedstate = internalchg;
			internalchg = true;
			updateLists();
			internalchg = savedstate;

			displayStrItem();
			this.btnLngClear.Enabled = false;
			this.btnLngClear.Text = "Clear Lang";

			if (index < 0) index = 0;
			if (index >= count) index = count - 1;
			this.cbLngSelect.SelectedIndex = l;
			if (defsel) this.lbxLngDefault.SelectedIndex = index;
			else this.lbxLngCurrent.SelectedIndex = index;
		}


		private void textBox_TextChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			internalchg = true;
			switch(alTextBox.IndexOf(sender))
			{
				case 0: wrapper.FileName = ((TextBox)sender).Text; break;
			}
			internalchg = false;
		}

		private void textBox_Validated(object sender, System.EventArgs e)
		{
			((TextBox)sender).SelectAll();
		}


		private void richTextBox_TextChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			if (index < 0) return;

			internalchg = true;
			switch(alRichTextBox.IndexOf(sender))
			{
				case 0:
					wrapper[lid, index].Title = ((RichTextBox)sender).Text;
					if (this.lbxLngDefault.SelectedIndex >= 0)
						this.lbxLngDefault.Items[index] = "0x" + Helper.HexString((ushort)index) + ": " + wrapper[lid, index];
					if (this.lbxLngCurrent.SelectedIndex >= 0)
						this.lbxLngCurrent.Items[index] = "0x" + Helper.HexString((ushort)index) + ": " + wrapper[lid, index];
					break;
				case 1: wrapper[lid, index].Description = ((RichTextBox)sender).Text; break;
			}
			if (this.lbxLngCurrent.SelectedIndex >= 0)
			{
				bool empty = true;
				StrItem[] s = wrapper[lid];
				for (int j = count - 1; j >= 0 && empty; j--)
					if (s[j] != null && (s[j].Title.Trim().Length + s[j].Description.Trim().Length > 0))
						empty = false;
				this.cbLngSelect.Items[lid - 2] = ((SimPe.Data.MetaData.Languages)lid).ToString() + (empty ? " (empty)" : "");
				this.btnLngClear.Enabled = !empty;
				this.btnLngClear.Text = "Clear " + this.cbLngSelect.Items[lid - 2].ToString();
				for (int i = 0; i < wrapper.Count && empty; i++)
					if ((wrapper[i].LanguageID != 1) && (wrapper[i].Title.Trim().Length + wrapper[i].Description.Trim().Length > 0))
						empty = false;
				this.btnClearAll.Enabled = !empty;
			}
			else
			{
				this.btnLngClear.Enabled = false;
				this.btnLngClear.Text = "Clear Lang";
			}
			internalchg = false;
		}

		private void richTextBox_Validated(object sender, System.EventArgs e)
		{
			((RichTextBox)sender).SelectAll();
		}


		private void hex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex16_IsValid(sender)) return;

			ushort val = Convert.ToUInt16(((TextBox)sender).Text, 16);
			internalchg = true;
			switch (alHex16.IndexOf(sender))
			{
				case 0: wrapper.Format = val; break;
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
				case 0: val = wrapper.Format; break;
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


		private void cbLngSelect_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			internalchg = true;

			int l = this.lbxLngCurrent.SelectedIndex;
			this.lbxLngCurrent.Items.Clear();

			if (this.cbLngSelect.SelectedIndex >= 0)
			{
				lid = (byte)(this.cbLngSelect.SelectedIndex + 2);
				while (count > 0 && wrapper[lid, count-1] == null) wrapper.Add(lid, "", "");
				StrItem[] s = wrapper[lid];
				for (ushort i = 0; i < count; i++)
					this.lbxLngCurrent.Items.Add("0x" + Helper.HexString(i) + ": " + s[i]);
				this.btnLngClear.Enabled = !((string)(this.cbLngSelect.Items[lid - 2])).EndsWith(" (empty)");
				this.btnLngClear.Text = "Clear " + this.cbLngSelect.SelectedItem.ToString();
			}
			else
			{
				this.btnLngClear.Enabled = false;
				this.btnLngClear.Text = "Clear Lang";
			}

			this.btnLngPrev.Enabled = (this.cbLngSelect.SelectedIndex > 0);
			this.btnLngNext.Enabled = (this.cbLngSelect.Items.Count > 0) && (this.cbLngSelect.SelectedIndex < this.cbLngSelect.Items.Count - 1);

			internalchg = false;
			if (l != -1) this.lbxLngCurrent.SelectedIndex = l;
		}

		private void btnLngPrev_Click(object sender, System.EventArgs e)
		{
			if (this.cbLngSelect.SelectedIndex > 0)
				this.cbLngSelect.SelectedIndex --;
		}

		private void btnLngNext_Click(object sender, System.EventArgs e)
		{
			if (this.cbLngSelect.SelectedIndex < this.cbLngSelect.Items.Count - 1)
				this.cbLngSelect.SelectedIndex ++;
		}

		private void btnLngClear_Click(object sender, System.EventArgs e)
		{
			if (this.cbLngSelect.SelectedIndex < 0) return;

			bool savedstate = internalchg;
			internalchg = true;

			int l = this.cbLngSelect.SelectedIndex + 2;
			for (int i = 0; i < wrapper.Count; i++)
				if (wrapper[i].LanguageID == l)
					wrapper[i].Title = wrapper[i].Description = "";

			updateLists();

			internalchg = savedstate;

			displayStrItem();
			this.btnLngClear.Enabled = false;
			this.btnLngClear.Text = "Clear Lang";

			if (index < 0) index = 0;
			if (index >= count) index = count - 1;
			this.cbLngSelect.SelectedIndex = l - 2;
			this.lbxLngDefault.SelectedIndex = index;
		}


		private void lbxLngDefault_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			internalchg = true;
			this.lbxLngCurrent.SelectedIndex = -1;
			internalchg = false;

			lid = 1;
			index = this.lbxLngDefault.SelectedIndex;
			//this.btnLngClear.Enabled = false;
			displayStrItem();
		}

		private void lbxLngCurrent_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			internalchg = true;
			this.lbxLngDefault.SelectedIndex = -1;
			internalchg = false;

			lid = (byte)(this.cbLngSelect.SelectedIndex + 2);
			index = this.lbxLngCurrent.SelectedIndex;
			//this.btnLngClear.Enabled = !((string)(this.cbLngSelect.Items[lid - 2])).EndsWith(" (empty)");
			displayStrItem();
		}


		private void btnClearAll_Click(object sender, System.EventArgs e)
		{
			bool savedstate = internalchg;
			internalchg = true;

			for (int i = 0; i < wrapper.Count; i++)
				if (wrapper[i].LanguageID != 1)
					wrapper[i].Title = wrapper[i].Description = "";

			int l = this.cbLngSelect.SelectedIndex;
			updateLists();

			internalchg = savedstate;

			displayStrItem();
			this.btnLngClear.Enabled = false;
			this.btnLngClear.Text = "Clear Lang";

			if (index < 0) index = 0;
			if (index >= count) index = count - 1;
			this.cbLngSelect.SelectedIndex = l;
			this.lbxLngDefault.SelectedIndex = index;
		}

		private void btnStrAdd_Click(object sender, System.EventArgs e)
		{
			bool savedstate = internalchg;

			internalchg = true;
			count++;
			wrapper.Add(1, "", "");
			this.lbxLngDefault.Items.Add("0x" + Helper.HexString((ushort)(count-1)) + ": " + wrapper[1, count-1]);
			if (this.cbLngSelect.SelectedIndex >= 0)
			{
				byte l = (byte)(this.cbLngSelect.SelectedIndex + 2);
				wrapper.Add(l, "", "");
				this.lbxLngCurrent.Items.Add("0x" + Helper.HexString((ushort)(count-1)) + ": " + wrapper[l, count-1]);
			}
			internalchg = savedstate;

			if (this.lbxLngCurrent.SelectedIndex >= 0)
				this.lbxLngCurrent.SelectedIndex = count - 1;
			else
				this.lbxLngDefault.SelectedIndex = count - 1;
		}

		private void btnStrDelete_Click(object sender, System.EventArgs e)
		{
			if (index < 0) return;

			bool savedstate = internalchg;
			internalchg = true;

			for (byte j = 1; j < 44; j++)
			{
				for (int i = index; i < count - 1; i++)
				{
					StrItem s1 = wrapper[j, i];
					if (s1 != null)
					{
						StrItem s2 = wrapper[j, i+1];
						if (s2 != null)
						{
							s1.Title       = s2.Title;
							s1.Description = s2.Description;
						}
						else
							s1.Title = s1.Description = "";
					}
				}
				wrapper.Remove(wrapper[j, count-1]);
			}

			int l = this.cbLngSelect.SelectedIndex;
			bool defsel = this.lbxLngDefault.SelectedIndex != -1;
			updateLists();

			internalchg = savedstate;

			displayStrItem();
			this.btnLngClear.Enabled = false;
			this.btnLngClear.Text = "Clear Lang";

			if (index < 0) index = 0;
			if (index >= count) index = count - 1;
			this.cbLngSelect.SelectedIndex = l;
			if (defsel) this.lbxLngDefault.SelectedIndex = index;
			else this.lbxLngCurrent.SelectedIndex = index;
		}

	}

}
