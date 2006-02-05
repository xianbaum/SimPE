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
		private System.Windows.Forms.Label lbStringNum;
		private System.Windows.Forms.Button btnStrDelete;
		private System.Windows.Forms.Button btnStrAdd;
		private System.Windows.Forms.Button btnClearAll;
		private System.Windows.Forms.Label lbLngSelect;
		private System.Windows.Forms.ComboBox cbLngSelect;
		private System.Windows.Forms.Button btnLngNext;
		private System.Windows.Forms.Button btnLngPrev;
		private System.Windows.Forms.Button btnLngClear;
		private System.Windows.Forms.RichTextBox rtbTitle;
		private System.Windows.Forms.RichTextBox rtbDescription;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnBigString;
		private System.Windows.Forms.Button btnBigDesc;
		private System.Windows.Forms.Button btnAppend;
		private System.Windows.Forms.ColumnHeader chString;
		private System.Windows.Forms.ColumnHeader chDefault;
		private System.Windows.Forms.ColumnHeader chLang;
		private System.Windows.Forms.ListView lvStrItems;
		private System.Windows.Forms.Button btnStrClear;
		private System.Windows.Forms.Label lbDesc;
		private System.Windows.Forms.CheckBox ckbDefault;
		private System.Windows.Forms.Button btnStrPrev;
		private System.Windows.Forms.Button btnStrNext;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Panel pnHeading;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnHelp;
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

			Control[] af = { tbFormat };
			alHex16 = new ArrayList(af);

			Control[] at = { tbFilename, rtbTitle, rtbDescription };
			alTextBoxBase = new ArrayList(at);

			Control[] ab = { btnBigString, btnBigDesc };
			alBigBtn = new ArrayList(ab);
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
		private Str wrapper = null;
		private bool setHandler = false;
		private bool internalchg = false;

		private ArrayList alHex16 = null;
		private ArrayList alTextBoxBase = null;
		private ArrayList alBigBtn = null;

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


		private void updateSelectedItem()
		{
			if (lid == 1)
				this.lvStrItems.Items[index].SubItems[2].Text = wrapper[lid, index].Title;
			this.lvStrItems.Items[index].SubItems[1].Text = wrapper[lid, index].Title;

			bool empty = true;
			StrItem[] sa = wrapper[lid];
			for (int j = count - 1; j >= 0 && empty; j--)
				if (sa[j] != null && (sa[j].Title.Trim().Length + sa[j].Description.Trim().Length > 0))
					empty = false;
			this.btnLngClear.Enabled = (lid == 1) ? false : !empty;
			this.cbLngSelect.Items[lid - 1] = ((SimPe.Data.MetaData.Languages)lid).ToString() + (empty ? " (empty)" : "");

			empty = true;
			foreach (StrItem s in wrapper)
				if ((s.LanguageID != 1) && (s.Title.Trim().Length + s.Description.Trim().Length > 0))
					empty = false;
			this.btnClearAll.Enabled = !empty;
		}

		private void updateLists()
		{
			wrapper.CleanUp();

			lid = 0;
			index = -1;

			this.cbLngSelect.Items.Clear();

			bool onlyDefault = true;
			for (byte i = 1; i < 44; i++)
			{
				bool empty = wrapper[i].Length == 0;
				this.cbLngSelect.Items.Add(((SimPe.Data.MetaData.Languages)i).ToString() + (empty ? " (empty)" : ""));
				if (!empty && i > 1) onlyDefault = false;
			}
			this.btnClearAll.Enabled = !onlyDefault;

			count = 0;
			for (byte i = 1; i < 44; i++) count = Math.Max(count, wrapper[i].Length);
			while (count > 0 && wrapper[1, count-1] == null && wrapper.Add(1, "", "") >= 0);

			this.lvStrItems.Columns[1].Text = "";
			this.lvStrItems.Items.Clear();
			for (int i = 0; i < count; i++)
			{
				StrItem si = wrapper[1, i];
				this.lvStrItems.Items.Add( new ListViewItem(
					new string[] { "0x" + Helper.HexString((ushort)i), "", ((si == null) ? "" : si.Title) }
					) );
				this.lvStrItems.Items[i].UseItemStyleForSubItems = false;
				this.lvStrItems.Items[i].SubItems[2].ForeColor = System.Drawing.SystemColors.ControlDark;
			}
		}


		private void setLid(byte l)
		{
			if (lid == l) return;
			lid = l;

			internalchg = true;
			if (lid > 0) this.cbLngSelect.SelectedIndex = l - 1;
			internalchg = false;
			this.btnLngPrev.Enabled = (this.cbLngSelect.SelectedIndex > 0);
			this.btnLngNext.Enabled = (wrapper.Format != 0x0000) && (this.cbLngSelect.Items.Count > 0) && (this.cbLngSelect.SelectedIndex < this.cbLngSelect.Items.Count - 1);

			this.btnLngClear.Text = "Clear " + ((SimPe.Data.MetaData.Languages)lid).ToString();
			this.btnLngClear.Enabled = (lid > 1) && !this.cbLngSelect.SelectedItem.ToString().EndsWith(" (empty)");

			while (count > 0 && wrapper[lid, count-1] == null && wrapper.Add(lid, "", "") >= 0);
			this.lvStrItems.Columns[1].Text = this.cbLngSelect.SelectedItem.ToString();
			for (int i = 0; i < count; i++)
				this.lvStrItems.Items[i].SubItems[1].Text = wrapper[lid, i].Title;

			displayStrItem();
		}

		private void setIndex(int i)
		{
			internalchg = true;
			if (i >= 0) this.lvStrItems.Items[i].Selected = true;
			else if (index >= 0) this.lvStrItems.Items[index].Selected = false;
			internalchg = false;

			if (this.lvStrItems.SelectedItems.Count > 0)
			{
				if (this.lvStrItems.Focused) this.lvStrItems.SelectedItems[0].Focused = true;
				this.lvStrItems.SelectedItems[0].EnsureVisible();
			}

			if (index == i) return;
			index = i;
			displayStrItem();
		}


		private void displayStrItem()
		{
			StrItem s = (index < 0) ? null : wrapper[lid, index];

			internalchg = true;
			if (s != null)
			{
				this.lbStringNum.Text = "String 0x" + Helper.HexString((ushort)index) + " (" + ((SimPe.Data.MetaData.Languages)lid).ToString() + ")";
				this.rtbTitle.Text = s.Title;
				this.rtbTitle.SelectAll();
				this.btnBigString.Enabled = this.rtbTitle.Enabled = true;
				this.rtbDescription.Text = s.Description;
				this.rtbDescription.SelectAll();
				this.btnBigDesc.Enabled = this.rtbDescription.Enabled = (wrapper.Format != 0x0000 && wrapper.Format != 0xFFFE);
			}
			else
			{
				this.lbStringNum.Text = "";
				this.rtbDescription.Text = this.rtbTitle.Text = "";
				this.btnBigDesc.Enabled = this.rtbDescription.Enabled = this.btnBigString.Enabled = this.rtbTitle.Enabled = false;
			}
			this.btnStrPrev.Enabled = (index > 0);
			this.btnStrNext.Enabled = (index < count - 1);
			internalchg = false;

			this.btnStrDelete.Enabled = index >= 0;
			this.btnStrClear.Enabled = (wrapper.Format != 0x0000 && index >= 0);
		}


		private void LngClear()
		{
			bool savedstate = internalchg;
			internalchg = true;

			foreach (StrItem s in wrapper)
				if (s.LanguageID == lid)
					s.Title = s.Description = "";

			byte l = lid;
			int i = index;
			updateLists();

			internalchg = savedstate;

			setLid(l);
			setIndex((i >= count) ? count - 1 : i);
		}

		private void LngClearAll()
		{
			bool savedstate = internalchg;
			internalchg = true;

			foreach (StrItem s in wrapper)
				if (s.LanguageID != 1)
					s.Title = s.Description = "";

			byte l = lid;
			int i = index;
			updateLists();

			internalchg = savedstate;

			setLid(l);
			setIndex((i >= count) ? count - 1 : i);
		}
		private void StrAdd()
		{
			bool savedstate = internalchg;
			internalchg = true;

			if (wrapper.Add(1, "", "") >= 0)
			{
				count++;
				this.lvStrItems.Items.Add(new ListViewItem(new string[] { "0x" + Helper.HexString((ushort)(count - 1)), "", "" }));
			}

			internalchg = savedstate;

			setLid(1);
			setIndex(count - 1);
		}

		private void StrDelete()
		{
			bool savedstate = internalchg;
			internalchg = true;

			for (byte j = 1; j < 44; j++)
			{
				for (int ix = index; ix < count - 1; ix++)
				{
					StrItem s1 = wrapper[j, ix];
					if (s1 != null)
					{
						StrItem s2 = wrapper[j, ix+1];
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

			byte l = lid;
			int i = index;
			updateLists();

			internalchg = savedstate;

			setLid(l);
			setIndex((i >= count) ? count - 1 : i);
		}

		private void StrReplace()
		{
			pjse.FileTable.Entry e = (new pjse.ResourceChooser()).Execute(wrapper.FileDescriptor.Type, wrapper.FileDescriptor.Group, strPanel);
			if (e == null || !(e.Wrapper is Str)) return;

			Str b = (Str)e.Wrapper;
			int strnum = (new pjse.StrChooser()).Strnum(b);
			if (strnum < 0) return;

			bool savedstate = internalchg;
			internalchg = true;

			if (wrapper.Format == 0x0000)
			{
				wrapper[1, index].Title = b[1, strnum].Title;
				wrapper[1, index].Description = b[1, strnum].Description;
			}
			else
				for (byte m = 1; m < 44; m++)
				{
					while (wrapper[m, index] == null && wrapper.Add(m, "", "") >= 0);
					if (b[m, strnum] == null)
					{
						wrapper[m, index].Title = "";
						wrapper[m, index].Description = "";
					}
					else
					{
						wrapper[m, index].Title = b[m, strnum].Title;
						wrapper[m, index].Description = b[m, strnum].Description;
					}
				}

			byte l = lid;
			int i = index;
			updateLists();

			internalchg = savedstate;

			setLid(l);
			setIndex((i >= count) ? count - 1 : i);
		}

		private void StrClear()
		{
			bool savedstate = internalchg;
			internalchg = true;

			for (byte m = 2; m < 44; m++)
			{
				StrItem s = wrapper[m, index];
				if (s != null) s.Description = s.Title = "";
			}

			byte l = lid;
			int i = index;
			updateLists();

			internalchg = savedstate;

			setLid(l);
			setIndex((i >= count) ? count - 1 : i);
		}

		private void Append(pjse.FileTable.Entry e)
		{
			if (e == null) return;

			bool savedstate = internalchg;
			internalchg = true;

			strPanel.Parent.Cursor = Cursors.WaitCursor;

			using(Str b = (Str)e.Wrapper)
			{
				if (wrapper.Format != 0x0000)
					for (byte m = 1; m < 44; m++)
						while (wrapper[m, count-1] == null && wrapper.Add(m, "", "") >= 0);
				for (int bi = 0; bi < b.Count; bi++)
				{
					if (wrapper.Format == 0x0000 && b[bi].LanguageID != 1) continue;
					if (wrapper.Add(b[bi]) < 0) break;
				}
			}

			strPanel.Parent.Cursor = Cursors.Default;

			byte l = lid;
			int i = index;
			updateLists();

			internalchg = savedstate;

			setLid(l);
			setIndex((i >= count) ? count - 1 : i);
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

			byte l = lid;
			int i = index;
			updateLists();

			internalchg = savedstate;

			setLid(l);
			setIndex((i >= count) ? count - 1 : i);
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
				return strPanel;
			}
		}

		/// <summary>
		/// Called by the AbstractWrapper when the file should be displayed to the user.
		/// </summary>
		/// <param name="wrp">Reference to the wrapper to be displayed.</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (Str) wrp;
			this.WrapperChanged(wrapper, null);

			internalchg = true;
			updateLists();
			internalchg = false;

			setLid(1);
			setIndex(count > 0 ? 0 : -1);
			if (this.ckbDefault.Checked)
				this.ckbDefault.Checked = false;
			else
				ckbDefault_CheckedChanged(null, null);

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
			this.Text = this.tbFilename.Text = wrapper.FileName;
			this.tbFormat.Text = "0x"+Helper.HexString(wrapper.Format);
			if (wrapper.Format == 0x0000)
			{
				this.btnBigDesc.Enabled = this.rtbDescription.Enabled = this.ckbDefault.Enabled = this.cbLngSelect.Enabled = false;
			}
			else if (wrapper.Format == 0xFFFE)
			{
				this.btnBigDesc.Enabled = this.rtbDescription.Enabled = false;
				this.ckbDefault.Enabled = this.cbLngSelect.Enabled = true;
			}
			else
			{
				this.btnBigDesc.Enabled = this.rtbDescription.Enabled = this.ckbDefault.Enabled = this.cbLngSelect.Enabled = true;
			}
			internalchg = false;

			this.ckbDefault.Enabled = this.cbLngSelect.Enabled = (wrapper.Format != 0x0000);
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
			this.pnHeading = new System.Windows.Forms.Panel();
			this.btnHelp = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.btnStrPrev = new System.Windows.Forms.Button();
			this.btnStrNext = new System.Windows.Forms.Button();
			this.ckbDefault = new System.Windows.Forms.CheckBox();
			this.btnStrClear = new System.Windows.Forms.Button();
			this.lvStrItems = new System.Windows.Forms.ListView();
			this.chString = new System.Windows.Forms.ColumnHeader();
			this.chLang = new System.Windows.Forms.ColumnHeader();
			this.chDefault = new System.Windows.Forms.ColumnHeader();
			this.btnBigDesc = new System.Windows.Forms.Button();
			this.btnBigString = new System.Windows.Forms.Button();
			this.lbDesc = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.rtbDescription = new System.Windows.Forms.RichTextBox();
			this.rtbTitle = new System.Windows.Forms.RichTextBox();
			this.btnLngNext = new System.Windows.Forms.Button();
			this.btnLngPrev = new System.Windows.Forms.Button();
			this.btnLngClear = new System.Windows.Forms.Button();
			this.cbLngSelect = new System.Windows.Forms.ComboBox();
			this.lbLngSelect = new System.Windows.Forms.Label();
			this.btnClearAll = new System.Windows.Forms.Button();
			this.lbStringNum = new System.Windows.Forms.Label();
			this.tbFilename = new System.Windows.Forms.TextBox();
			this.lbFilename = new System.Windows.Forms.Label();
			this.btnCommit = new System.Windows.Forms.Button();
			this.lbFormat = new System.Windows.Forms.Label();
			this.tbFormat = new System.Windows.Forms.TextBox();
			this.btnAppend = new System.Windows.Forms.Button();
			this.btnStrDelete = new System.Windows.Forms.Button();
			this.btnStrAdd = new System.Windows.Forms.Button();
			this.btnImport = new System.Windows.Forms.Button();
			this.strPanel.SuspendLayout();
			this.pnHeading.SuspendLayout();
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
			this.strPanel.Controls.Add(this.pnHeading);
			this.strPanel.Controls.Add(this.btnStrPrev);
			this.strPanel.Controls.Add(this.btnStrNext);
			this.strPanel.Controls.Add(this.ckbDefault);
			this.strPanel.Controls.Add(this.btnStrClear);
			this.strPanel.Controls.Add(this.lvStrItems);
			this.strPanel.Controls.Add(this.btnBigDesc);
			this.strPanel.Controls.Add(this.btnBigString);
			this.strPanel.Controls.Add(this.lbDesc);
			this.strPanel.Controls.Add(this.label1);
			this.strPanel.Controls.Add(this.rtbDescription);
			this.strPanel.Controls.Add(this.rtbTitle);
			this.strPanel.Controls.Add(this.btnLngNext);
			this.strPanel.Controls.Add(this.btnLngPrev);
			this.strPanel.Controls.Add(this.btnLngClear);
			this.strPanel.Controls.Add(this.cbLngSelect);
			this.strPanel.Controls.Add(this.lbLngSelect);
			this.strPanel.Controls.Add(this.btnClearAll);
			this.strPanel.Controls.Add(this.lbStringNum);
			this.strPanel.Controls.Add(this.tbFilename);
			this.strPanel.Controls.Add(this.lbFilename);
			this.strPanel.Controls.Add(this.btnCommit);
			this.strPanel.Controls.Add(this.lbFormat);
			this.strPanel.Controls.Add(this.tbFormat);
			this.strPanel.Controls.Add(this.btnAppend);
			this.strPanel.Controls.Add(this.btnStrDelete);
			this.strPanel.Controls.Add(this.btnStrAdd);
			this.strPanel.Controls.Add(this.btnImport);
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
			this.strPanel.Resize += new System.EventHandler(this.strPanel_Resize);
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
			this.pnHeading.Controls.Add(this.label2);
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
			this.btnStrNext.Text = resources.GetString("btnStrNext.Text");
			this.btnStrNext.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnStrNext.TextAlign")));
			this.btnStrNext.Visible = ((bool)(resources.GetObject("btnStrNext.Visible")));
			this.btnStrNext.Click += new System.EventHandler(this.btnStrNext_Click);
			// 
			// ckbDefault
			// 
			this.ckbDefault.AccessibleDescription = resources.GetString("ckbDefault.AccessibleDescription");
			this.ckbDefault.AccessibleName = resources.GetString("ckbDefault.AccessibleName");
			this.ckbDefault.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ckbDefault.Anchor")));
			this.ckbDefault.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("ckbDefault.Appearance")));
			this.ckbDefault.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ckbDefault.BackgroundImage")));
			this.ckbDefault.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbDefault.CheckAlign")));
			this.ckbDefault.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ckbDefault.Dock")));
			this.ckbDefault.Enabled = ((bool)(resources.GetObject("ckbDefault.Enabled")));
			this.ckbDefault.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("ckbDefault.FlatStyle")));
			this.ckbDefault.Font = ((System.Drawing.Font)(resources.GetObject("ckbDefault.Font")));
			this.ckbDefault.Image = ((System.Drawing.Image)(resources.GetObject("ckbDefault.Image")));
			this.ckbDefault.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbDefault.ImageAlign")));
			this.ckbDefault.ImageIndex = ((int)(resources.GetObject("ckbDefault.ImageIndex")));
			this.ckbDefault.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ckbDefault.ImeMode")));
			this.ckbDefault.Location = ((System.Drawing.Point)(resources.GetObject("ckbDefault.Location")));
			this.ckbDefault.Name = "ckbDefault";
			this.ckbDefault.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ckbDefault.RightToLeft")));
			this.ckbDefault.Size = ((System.Drawing.Size)(resources.GetObject("ckbDefault.Size")));
			this.ckbDefault.TabIndex = ((int)(resources.GetObject("ckbDefault.TabIndex")));
			this.ckbDefault.Text = resources.GetString("ckbDefault.Text");
			this.ckbDefault.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ckbDefault.TextAlign")));
			this.ckbDefault.Visible = ((bool)(resources.GetObject("ckbDefault.Visible")));
			this.ckbDefault.CheckedChanged += new System.EventHandler(this.ckbDefault_CheckedChanged);
			// 
			// btnStrClear
			// 
			this.btnStrClear.AccessibleDescription = resources.GetString("btnStrClear.AccessibleDescription");
			this.btnStrClear.AccessibleName = resources.GetString("btnStrClear.AccessibleName");
			this.btnStrClear.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnStrClear.Anchor")));
			this.btnStrClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStrClear.BackgroundImage")));
			this.btnStrClear.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnStrClear.Dock")));
			this.btnStrClear.Enabled = ((bool)(resources.GetObject("btnStrClear.Enabled")));
			this.btnStrClear.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnStrClear.FlatStyle")));
			this.btnStrClear.Font = ((System.Drawing.Font)(resources.GetObject("btnStrClear.Font")));
			this.btnStrClear.Image = ((System.Drawing.Image)(resources.GetObject("btnStrClear.Image")));
			this.btnStrClear.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnStrClear.ImageAlign")));
			this.btnStrClear.ImageIndex = ((int)(resources.GetObject("btnStrClear.ImageIndex")));
			this.btnStrClear.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnStrClear.ImeMode")));
			this.btnStrClear.Location = ((System.Drawing.Point)(resources.GetObject("btnStrClear.Location")));
			this.btnStrClear.Name = "btnStrClear";
			this.btnStrClear.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnStrClear.RightToLeft")));
			this.btnStrClear.Size = ((System.Drawing.Size)(resources.GetObject("btnStrClear.Size")));
			this.btnStrClear.TabIndex = ((int)(resources.GetObject("btnStrClear.TabIndex")));
			this.btnStrClear.Text = resources.GetString("btnStrClear.Text");
			this.btnStrClear.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnStrClear.TextAlign")));
			this.btnStrClear.Visible = ((bool)(resources.GetObject("btnStrClear.Visible")));
			this.btnStrClear.Click += new System.EventHandler(this.btnStrClear_Click);
			// 
			// lvStrItems
			// 
			this.lvStrItems.AccessibleDescription = resources.GetString("lvStrItems.AccessibleDescription");
			this.lvStrItems.AccessibleName = resources.GetString("lvStrItems.AccessibleName");
			this.lvStrItems.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.lvStrItems.Alignment = ((System.Windows.Forms.ListViewAlignment)(resources.GetObject("lvStrItems.Alignment")));
			this.lvStrItems.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lvStrItems.Anchor")));
			this.lvStrItems.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lvStrItems.BackgroundImage")));
			this.lvStrItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						 this.chString,
																						 this.chLang,
																						 this.chDefault});
			this.lvStrItems.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lvStrItems.Dock")));
			this.lvStrItems.Enabled = ((bool)(resources.GetObject("lvStrItems.Enabled")));
			this.lvStrItems.Font = ((System.Drawing.Font)(resources.GetObject("lvStrItems.Font")));
			this.lvStrItems.FullRowSelect = true;
			this.lvStrItems.GridLines = true;
			this.lvStrItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvStrItems.HideSelection = false;
			this.lvStrItems.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lvStrItems.ImeMode")));
			this.lvStrItems.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
																					   ((System.Windows.Forms.ListViewItem)(resources.GetObject("lvStrItems.Items")))});
			this.lvStrItems.LabelWrap = ((bool)(resources.GetObject("lvStrItems.LabelWrap")));
			this.lvStrItems.Location = ((System.Drawing.Point)(resources.GetObject("lvStrItems.Location")));
			this.lvStrItems.MultiSelect = false;
			this.lvStrItems.Name = "lvStrItems";
			this.lvStrItems.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lvStrItems.RightToLeft")));
			this.lvStrItems.Size = ((System.Drawing.Size)(resources.GetObject("lvStrItems.Size")));
			this.lvStrItems.TabIndex = ((int)(resources.GetObject("lvStrItems.TabIndex")));
			this.lvStrItems.Text = resources.GetString("lvStrItems.Text");
			this.lvStrItems.View = System.Windows.Forms.View.Details;
			this.lvStrItems.Visible = ((bool)(resources.GetObject("lvStrItems.Visible")));
			this.lvStrItems.SelectedIndexChanged += new System.EventHandler(this.lvStrItems_SelectedIndexChanged);
			// 
			// chString
			// 
			this.chString.Text = resources.GetString("chString.Text");
			this.chString.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("chString.TextAlign")));
			this.chString.Width = ((int)(resources.GetObject("chString.Width")));
			// 
			// chLang
			// 
			this.chLang.Text = resources.GetString("chLang.Text");
			this.chLang.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("chLang.TextAlign")));
			this.chLang.Width = ((int)(resources.GetObject("chLang.Width")));
			// 
			// chDefault
			// 
			this.chDefault.Text = resources.GetString("chDefault.Text");
			this.chDefault.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("chDefault.TextAlign")));
			this.chDefault.Width = ((int)(resources.GetObject("chDefault.Width")));
			// 
			// btnBigDesc
			// 
			this.btnBigDesc.AccessibleDescription = resources.GetString("btnBigDesc.AccessibleDescription");
			this.btnBigDesc.AccessibleName = resources.GetString("btnBigDesc.AccessibleName");
			this.btnBigDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnBigDesc.Anchor")));
			this.btnBigDesc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBigDesc.BackgroundImage")));
			this.btnBigDesc.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnBigDesc.Dock")));
			this.btnBigDesc.Enabled = ((bool)(resources.GetObject("btnBigDesc.Enabled")));
			this.btnBigDesc.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnBigDesc.FlatStyle")));
			this.btnBigDesc.Font = ((System.Drawing.Font)(resources.GetObject("btnBigDesc.Font")));
			this.btnBigDesc.Image = ((System.Drawing.Image)(resources.GetObject("btnBigDesc.Image")));
			this.btnBigDesc.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnBigDesc.ImageAlign")));
			this.btnBigDesc.ImageIndex = ((int)(resources.GetObject("btnBigDesc.ImageIndex")));
			this.btnBigDesc.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnBigDesc.ImeMode")));
			this.btnBigDesc.Location = ((System.Drawing.Point)(resources.GetObject("btnBigDesc.Location")));
			this.btnBigDesc.Name = "btnBigDesc";
			this.btnBigDesc.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnBigDesc.RightToLeft")));
			this.btnBigDesc.Size = ((System.Drawing.Size)(resources.GetObject("btnBigDesc.Size")));
			this.btnBigDesc.TabIndex = ((int)(resources.GetObject("btnBigDesc.TabIndex")));
			this.btnBigDesc.Text = resources.GetString("btnBigDesc.Text");
			this.btnBigDesc.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnBigDesc.TextAlign")));
			this.btnBigDesc.Visible = ((bool)(resources.GetObject("btnBigDesc.Visible")));
			this.btnBigDesc.Click += new System.EventHandler(this.btnBigString_Click);
			// 
			// btnBigString
			// 
			this.btnBigString.AccessibleDescription = resources.GetString("btnBigString.AccessibleDescription");
			this.btnBigString.AccessibleName = resources.GetString("btnBigString.AccessibleName");
			this.btnBigString.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnBigString.Anchor")));
			this.btnBigString.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBigString.BackgroundImage")));
			this.btnBigString.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnBigString.Dock")));
			this.btnBigString.Enabled = ((bool)(resources.GetObject("btnBigString.Enabled")));
			this.btnBigString.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnBigString.FlatStyle")));
			this.btnBigString.Font = ((System.Drawing.Font)(resources.GetObject("btnBigString.Font")));
			this.btnBigString.Image = ((System.Drawing.Image)(resources.GetObject("btnBigString.Image")));
			this.btnBigString.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnBigString.ImageAlign")));
			this.btnBigString.ImageIndex = ((int)(resources.GetObject("btnBigString.ImageIndex")));
			this.btnBigString.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnBigString.ImeMode")));
			this.btnBigString.Location = ((System.Drawing.Point)(resources.GetObject("btnBigString.Location")));
			this.btnBigString.Name = "btnBigString";
			this.btnBigString.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnBigString.RightToLeft")));
			this.btnBigString.Size = ((System.Drawing.Size)(resources.GetObject("btnBigString.Size")));
			this.btnBigString.TabIndex = ((int)(resources.GetObject("btnBigString.TabIndex")));
			this.btnBigString.Text = resources.GetString("btnBigString.Text");
			this.btnBigString.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnBigString.TextAlign")));
			this.btnBigString.Visible = ((bool)(resources.GetObject("btnBigString.Visible")));
			this.btnBigString.Click += new System.EventHandler(this.btnBigString_Click);
			// 
			// lbDesc
			// 
			this.lbDesc.AccessibleDescription = resources.GetString("lbDesc.AccessibleDescription");
			this.lbDesc.AccessibleName = resources.GetString("lbDesc.AccessibleName");
			this.lbDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbDesc.Anchor")));
			this.lbDesc.AutoSize = ((bool)(resources.GetObject("lbDesc.AutoSize")));
			this.lbDesc.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbDesc.Dock")));
			this.lbDesc.Enabled = ((bool)(resources.GetObject("lbDesc.Enabled")));
			this.lbDesc.Font = ((System.Drawing.Font)(resources.GetObject("lbDesc.Font")));
			this.lbDesc.Image = ((System.Drawing.Image)(resources.GetObject("lbDesc.Image")));
			this.lbDesc.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbDesc.ImageAlign")));
			this.lbDesc.ImageIndex = ((int)(resources.GetObject("lbDesc.ImageIndex")));
			this.lbDesc.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbDesc.ImeMode")));
			this.lbDesc.Location = ((System.Drawing.Point)(resources.GetObject("lbDesc.Location")));
			this.lbDesc.Name = "lbDesc";
			this.lbDesc.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbDesc.RightToLeft")));
			this.lbDesc.Size = ((System.Drawing.Size)(resources.GetObject("lbDesc.Size")));
			this.lbDesc.TabIndex = ((int)(resources.GetObject("lbDesc.TabIndex")));
			this.lbDesc.Text = resources.GetString("lbDesc.Text");
			this.lbDesc.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbDesc.TextAlign")));
			this.lbDesc.Visible = ((bool)(resources.GetObject("lbDesc.Visible")));
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
			this.rtbDescription.TextChanged += new System.EventHandler(this.textBoxBase_TextChanged);
			this.rtbDescription.Enter += new System.EventHandler(this.textBoxBase_Enter);
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
			this.rtbTitle.TextChanged += new System.EventHandler(this.textBoxBase_TextChanged);
			this.rtbTitle.Enter += new System.EventHandler(this.textBoxBase_Enter);
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
			this.tbFilename.TextChanged += new System.EventHandler(this.textBoxBase_TextChanged);
			this.tbFilename.Enter += new System.EventHandler(this.textBoxBase_Enter);
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
			// btnAppend
			// 
			this.btnAppend.AccessibleDescription = resources.GetString("btnAppend.AccessibleDescription");
			this.btnAppend.AccessibleName = resources.GetString("btnAppend.AccessibleName");
			this.btnAppend.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnAppend.Anchor")));
			this.btnAppend.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAppend.BackgroundImage")));
			this.btnAppend.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnAppend.Dock")));
			this.btnAppend.Enabled = ((bool)(resources.GetObject("btnAppend.Enabled")));
			this.btnAppend.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnAppend.FlatStyle")));
			this.btnAppend.Font = ((System.Drawing.Font)(resources.GetObject("btnAppend.Font")));
			this.btnAppend.Image = ((System.Drawing.Image)(resources.GetObject("btnAppend.Image")));
			this.btnAppend.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAppend.ImageAlign")));
			this.btnAppend.ImageIndex = ((int)(resources.GetObject("btnAppend.ImageIndex")));
			this.btnAppend.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnAppend.ImeMode")));
			this.btnAppend.Location = ((System.Drawing.Point)(resources.GetObject("btnAppend.Location")));
			this.btnAppend.Name = "btnAppend";
			this.btnAppend.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnAppend.RightToLeft")));
			this.btnAppend.Size = ((System.Drawing.Size)(resources.GetObject("btnAppend.Size")));
			this.btnAppend.TabIndex = ((int)(resources.GetObject("btnAppend.TabIndex")));
			this.btnAppend.Text = resources.GetString("btnAppend.Text");
			this.btnAppend.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnAppend.TextAlign")));
			this.btnAppend.Visible = ((bool)(resources.GetObject("btnAppend.Visible")));
			this.btnAppend.Click += new System.EventHandler(this.btnAppend_Click);
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
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
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
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
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
			this.pnHeading.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void strPanel_Resize(object sender, System.EventArgs e)
		{
			this.btnBigDesc.Left = this.btnCommit.Right - this.btnBigDesc.Width;

			int width = this.btnBigDesc.Left - this.rtbTitle.Left - this.lbDesc.Width - 8;

			this.rtbDescription.Width = this.rtbTitle.Width = width / 2;
			this.btnBigString.Left = this.rtbTitle.Right;
			this.lbDesc.Left = this.rtbTitle.Right + 4;
			this.rtbDescription.Left = this.lbDesc.Right + 4;
		}


		private void textBoxBase_Enter(object sender, System.EventArgs e)
		{
			((TextBoxBase)sender).SelectAll();
		}

		private void textBoxBase_TextChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			internalchg = true;
			switch(alTextBoxBase.IndexOf(sender))
			{
				case 0: wrapper.FileName = ((TextBoxBase)sender).Text; break;
				case 1: wrapper[lid, index].Title = ((TextBoxBase)sender).Text; updateSelectedItem(); break;
				case 2: wrapper[lid, index].Description = ((TextBoxBase)sender).Text; updateSelectedItem(); break;
			}
			internalchg = false;
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
			hex16_Validated(sender, null);
		}

		private void hex16_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			ushort val = 0;
			switch (alHex16.IndexOf(sender))
			{
				case 0: val = wrapper.Format; break;
			}

			((TextBox)sender).Text = "0x" + Helper.HexString(val);
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}


		private void cbLngSelect_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			if (this.cbLngSelect.SelectedIndex >= 0)
				setLid((byte)(this.cbLngSelect.SelectedIndex + 1));
		}

		private void lvStrItems_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			setIndex((this.lvStrItems.SelectedIndices.Count > 0) ? this.lvStrItems.SelectedIndices[0] : -1);
		}

		private void lvStrItems_ItemActivate(object sender, System.EventArgs e)
		{
			this.rtbTitle.Focus();
		}


		private void ckbDefault_CheckedChanged(object sender, System.EventArgs e)
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StrForm));
			this.chString.Width = ((int)(resources.GetObject("chString.Width")));
			while (this.lvStrItems.Columns.Count > 2)
				this.lvStrItems.Columns.RemoveAt(2);
			if (this.ckbDefault.Checked)
			{
				this.chDefault.Width = this.chLang.Width = (this.lvStrItems.ClientRectangle.Width - this.chString.Width - 18) / 2;
				this.lvStrItems.Columns.Add(this.chDefault);
			}
			else
			{
				this.chLang.Width = (this.lvStrItems.ClientRectangle.Width - this.chString.Width - 18);
			}
		}


		private void btnBigString_Click(object sender, System.EventArgs e)
		{
			int index = alBigBtn.IndexOf(sender);
			if (index < 0)
				throw new Exception("btnBigString_Click not applicable to control " + sender.ToString());

			RichTextBox[] rtb = { rtbTitle, rtbDescription };
			string result = (new pjse.StrBig()).doBig(rtb[index].Text);
			if (result != null) rtb[index].Text = result;
		}


		private void btnStrPrev_Click(object sender, System.EventArgs e)
		{
			setIndex(index - 1);
		}

		private void btnStrNext_Click(object sender, System.EventArgs e)
		{
			setIndex(index + 1);
		}


		private void btnLngPrev_Click(object sender, System.EventArgs e)
		{
			setLid((byte)(lid - 1));
		}

		private void btnLngNext_Click(object sender, System.EventArgs e)
		{
			setLid((byte)(lid + 1));
		}


		private void btnLngClear_Click(object sender, System.EventArgs e)
		{
			this.LngClear();
		}

		private void btnClearAll_Click(object sender, System.EventArgs e)
		{
			this.LngClearAll();
		}

		private void btnStrAdd_Click(object sender, System.EventArgs e)
		{
			this.StrAdd();
		}

		private void btnStrDelete_Click(object sender, System.EventArgs e)
		{
			this.StrDelete();
		}

		private void btnStrClear_Click(object sender, System.EventArgs e)
		{
			this.StrClear();
		}

		private void btnAppend_Click(object sender, System.EventArgs e)
		{
			this.Append((new pjse.ResourceChooser()).Execute(wrapper.FileDescriptor.Type, wrapper.FileDescriptor.Group, strPanel));
		}


		private void btnCommit_Click(object sender, System.EventArgs e)
		{
			this.Commit();
		}

		private void btnImport_Click(object sender, System.EventArgs e)
		{
			this.StrReplace();
		}


		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			pjse.HelpHelper.PluginHelp((wrapper.FileDescriptor.Type == 0x54544173)
				? "PieMenus"
				: "Strings");
		}

	}

}
