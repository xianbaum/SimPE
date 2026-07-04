/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   pljones@users.sf.net                                                  *
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
using System.Collections.Generic;
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
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnLngFirst;
        private System.Windows.Forms.Button btnStrDefault;
        private ColumnHeader chLangDesc;
        private ColumnHeader chDefaultDesc;
        private CheckBox ckbDescription;
        private Button btnImport;
        private Button btnExport;
        private Button btnStrCopy;
        private pjse.pjse_banner pjse_banner1;
        private Button BtnClean;
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

            pjse.FileTable.GFT.FiletableRefresh += new EventHandler(GFT_FiletableRefresh);
            if (booby.ThemeManager.ThemedForms)
            {
                this.strPanel.BackColor = booby.ThemeManager.Global.ThemeColorLight;
                this.lvStrItems.BackColor = booby.ThemeManager.Global.ThemeColorLighter;
                booby.ThemeManager.Global.AddControl(this.btnCommit);
            }
            if (Helper.WindowsRegistry.UseBigIcons) this.lvStrItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
        }

        void GFT_FiletableRefresh(object sender, EventArgs e)
        {
            if (wrapper.FileDescriptor == null) return;

            byte oldLid = lid;
            int oldIndex = index;
            bool savedchg = internalchg;
            internalchg = true;

            updateLists();

            setLid(oldLid); // sets internalchg to false
            setIndex(oldIndex);

            internalchg = savedchg;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Controller
        private StrWrapper wrapper = null;
        private bool setHandler = false;
        private bool internalchg = false;
        private ArrayList alHex16 = null;
        private ArrayList alTextBoxBase = null;
        private ArrayList alBigBtn = null;
        private byte lid = 1;
        private int index = -1;
        private int count = 0;
        private bool[] isEmpty = new bool[45];
        private String langName = pjse.BhavWiz.readStr(pjse.GS.BhavStr.Languages, 1);

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
            {
                this.lvStrItems.Items[index].SubItems[3].Text = wrapper[1, index].Title;
                this.lvStrItems.Items[index].SubItems[4].Text = wrapper[1, index].Description;
            }
            this.lvStrItems.Items[index].SubItems[1].Text = wrapper[lid, index].Title;
            this.lvStrItems.Items[index].SubItems[2].Text = wrapper[lid, index].Description;

            isEmpty[lid] = true;
            List<StrItem> sa = wrapper[lid];
            for (int j = count - 1; j >= 0 && isEmpty[lid]; j--)
                if (sa[j] != null && (sa[j].Title.Trim().Length + sa[j].Description.Trim().Length > 0))
                    isEmpty[lid] = false;
            this.cbLngSelect.Items[lid - 1] = langName + (isEmpty[lid] ? " (" + pjse.Localization.GetString("empty") + ")" : "");

            doButtons();
        }

        private void doButtons()
        {
            // (index >= 0) means row selected
            // isEmpty[lid] means rows exist
            // empty means only default language has strings

            bool empty = true;
            foreach (StrItem s in wrapper)
                if ((s.LanguageID != 1) && (s.Title.Trim().Length + s.Description.Trim().Length > 0))
                    empty = false;

            this.btnStrPrev.Enabled = (index > 0);
            this.btnStrNext.Enabled = (index < count - 1);

            this.btnClearAll.Enabled = !empty; // "Default lang only"
            this.btnLngClear.Enabled = (lid != 1) && !isEmpty[lid]; // "Clear this lang"

            this.btnStrAdd.Enabled = (lid == 1);
            this.btnStrDelete.Enabled = (lid == 1) && (index >= 0);
            this.btnStrDefault.Enabled = (lid != 1) && !isEmpty[lid] && (index >= 0); // "Make default"
            this.btnStrClear.Enabled = (wrapper.Format != 0x0000) && !empty && (index >= 0); // "Default string only"
            this.btnStrCopy.Enabled = (wrapper.Format != 0x0000) && !isEmpty[lid] && (index >= 0);
            this.btnReplace.Enabled = (lid == 1);
            this.BtnClean.Enabled = (wrapper.Format != 0x0000 && wrapper.Format != 0xFFFE);
        }

        private void updateLists()
        {
            wrapper.CleanUp();

            lid = 0;
            index = -1;
            count = 0;
            bool onlyDefault = true;
            this.cbLngSelect.Items.Clear();
            this.cbLngSelect.Items.AddRange(pjse.BhavWiz.readStr(pjse.GS.BhavStr.Languages).ToArray());

            // I really wish there were a nicer way...
            for (byte i = 0; i < 44; i++)
            {
                isEmpty[i] = !wrapper.HasLanguage(i);
                if (!isEmpty[i] && i > 1) onlyDefault = false;

                while (i >= this.cbLngSelect.Items.Count)
                    this.cbLngSelect.Items.Add("0x" + SimPe.Helper.HexString((byte)this.cbLngSelect.Items.Count) + " (" + pjse.Localization.GetString("unk") + ")");
                this.cbLngSelect.Items[i] += isEmpty[i] ? " (" + pjse.Localization.GetString("empty") + ")" : "";
                if (i > 0) count = Math.Max(count, wrapper.CountOf(i));
            }

            this.btnClearAll.Enabled = !onlyDefault;
            this.cbLngSelect.Items.RemoveAt(0);
            while (wrapper.CountOf(1) < count) wrapper.Add(1, "", "");
            this.lvStrItems.Columns.Clear();
            this.lvStrItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.chString, this.chLang, this.chLangDesc, this.chDefault, this.chDefaultDesc});
            this.lvStrItems.Columns[1].Text = "";
            this.lvStrItems.Items.Clear();
            for (int i = 0; i < count; i++)
            {
                StrItem si = wrapper[1, i];
                this.lvStrItems.Items.Add(new ListViewItem(
                    new string[] {
                        "0x" + Helper.HexString((ushort)i) + " (" + i + ")",
                        "",
                        "",
                        ((si == null) ? "" : si.Title),
                        ((si == null) ? "" : si.Description)
                    }));
                this.lvStrItems.Items[i].UseItemStyleForSubItems = false;
                this.lvStrItems.Items[i].SubItems[2].ForeColor = System.Drawing.SystemColors.ControlDark;
                this.lvStrItems.Items[i].SubItems[3].ForeColor = System.Drawing.SystemColors.ControlDark;
                this.lvStrItems.Items[i].SubItems[4].ForeColor = System.Drawing.SystemColors.ControlDark;
            }
        }

        private void setLid(byte l)
        {
            if (lid == l) return;
            lid = l;
            langName = pjse.BhavWiz.readStr(pjse.GS.BhavStr.Languages, lid);

            internalchg = true;
            if (lid > 0) this.cbLngSelect.SelectedIndex = l - 1;
            internalchg = false;
            this.btnLngFirst.Enabled = this.btnLngPrev.Enabled = (this.cbLngSelect.SelectedIndex > 0);
            this.btnLngNext.Enabled = (wrapper.Format != 0x0000) && (this.cbLngSelect.Items.Count > 0) && (this.cbLngSelect.SelectedIndex < this.cbLngSelect.Items.Count - 1);
            this.ckbDefault.Enabled = lid > 1;

            this.btnLngClear.Text = pjse.Localization.GetString("Clear") + " " + langName;

            while (wrapper.CountOf(lid) < count) wrapper.Add(lid, "", "");
            this.lvStrItems.Columns[1].Text = this.cbLngSelect.SelectedItem.ToString();
            for (int i = 0; i < count; i++)
            {
                this.lvStrItems.Items[i].SubItems[1].Text = wrapper[lid, i].Title;
                this.lvStrItems.Items[i].SubItems[2].Text = wrapper[lid, i].Description;
            }
            ckb_CheckedChanged(null, null);
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
                this.lbStringNum.Text = pjse.Localization.GetString("String") + " 0x"
                    + Helper.HexString((ushort)index) + " (" + langName + ")";
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
            internalchg = false;

            doButtons();
        }


        private void LngClear()
        {
            bool savedstate = internalchg;
            internalchg = true;

            wrapper.Remove(lid);

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

            wrapper.DefaultOnly();

            byte l = lid;
            int i = index;
            updateLists();

            internalchg = savedstate;

            setLid(l);
            setIndex((i >= count) ? count - 1 : i);
        }

        private void CleanAll()
        {
            bool savedstate = internalchg;
            internalchg = true;

            wrapper.CleanHim();

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

            string title, desc;
            if (index >= 0)
            {
                StrItem si = (StrItem)wrapper[1, index];
                if (si != null)
                {
                    title = si.Title;
                    desc = si.Description;
                }
                else
                    title = desc = "";
            }
            else
                title = desc = "";


            try
            {
                wrapper.Add(1, title, desc);
                count++;
                this.lvStrItems.Items.Add(new ListViewItem(new string[] { "0x" + Helper.HexString((ushort)(count - 1)) + " (" + ((ushort)(count - 1)) + ")", title, desc, title, desc }));
            }
            catch { }

            internalchg = savedstate;

            //setLid(1);
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
                        StrItem s2 = wrapper[j, ix + 1];
                        if (s2 != null)
                        {
                            s1.Title = s2.Title;
                            s1.Description = s2.Description;
                        }
                        else
                            s1.Title = s1.Description = "";
                    }
                }
                wrapper.Remove(wrapper[j, count - 1]);
            }

            byte l = lid;
            int i = index;
            updateLists();

            internalchg = savedstate;

            setLid(l);
            setIndex((i >= count) ? count - 1 : i);
        }

        private void StrCopy()
        {
            bool savedstate = internalchg;
            internalchg = true;

            for (byte m = 1; m < 44; m++)
            {
                if (m == lid) continue;

                while (wrapper[m, index] == null) wrapper.Add(m, "", "");
                wrapper[m, index].Title = wrapper[lid, index].Title;
                wrapper[m, index].Description = wrapper[lid, index].Description;
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
            pjse.FileTable.Entry e = (new pjse.ResourceChooser()).Execute(wrapper.FileDescriptor.Type, wrapper.FileDescriptor.Group, strPanel, true);
            if (e == null || !(e.Wrapper is StrWrapper)) return;

            StrWrapper b = (StrWrapper)e.Wrapper;
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
                    while (wrapper[m, index] == null) wrapper.Add(m, "", "");
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

        private void StrDefault()
        {
            StrItem di = wrapper[1, index];
            StrItem si = wrapper[lid, index];

            di.Title = si.Title;
            di.Description = si.Description;

            this.lvStrItems.Items[index].SubItems[3].Text = wrapper[1, index].Title;
            this.lvStrItems.Items[index].SubItems[4].Text = wrapper[1, index].Description;

            isEmpty[1] = true;
            List<StrItem> sa = wrapper[(byte)1];
            for (int j = count - 1; j >= 0 && isEmpty[1]; j--)
                if (sa[j] != null && (sa[j].Title.Trim().Length + sa[j].Description.Trim().Length > 0))
                    isEmpty[1] = false;
            this.cbLngSelect.Items[0] = pjse.BhavWiz.readStr(pjse.GS.BhavStr.Languages, 1)
                + (isEmpty[1] ? " (" + pjse.Localization.GetString("empty") + ")" : "");
        }

        private void Append(pjse.FileTable.Entry e)
        {
            if (e == null) return;

            bool savedstate = internalchg;
            internalchg = true;

            strPanel.Parent.Cursor = Cursors.WaitCursor;

            using (StrWrapper b = (StrWrapper)e.Wrapper)
            {
                if (wrapper.Format != 0x0000)
                    for (byte m = 1; m < 44; m++)
                        while (wrapper[m, count - 1] == null) wrapper.Add(m, "", "");
                for (int bi = 0; bi < b.Count; bi++)
                {
                    if (wrapper.Format == 0x0000 && b[bi].LanguageID != 1) continue;
                    try { wrapper.Add(b[bi]); }
                    catch { break; }
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
                Helper.ExceptionMessage(pjse.Localization.GetString("errwritingfile"), ex);
            }

            btnCommit.Enabled = wrapper.Changed;

            byte l = lid;
            int i = index;
            updateLists();

            internalchg = savedstate;

            setLid(l);
            setIndex((i >= count) ? count - 1 : i);
        }

        private void StringFile(bool load)
        {
            FileDialog fd = load ? (FileDialog)new OpenFileDialog() : (FileDialog)new SaveFileDialog();
            fd.AddExtension = true;
            fd.CheckFileExists = load;
            fd.CheckPathExists = true;
            fd.DefaultExt = "txt";
            fd.DereferenceLinks = true;
            fd.FileName = langName + ".txt";
            fd.Filter = pjse.Localization.GetString("strLangFilter");
            fd.FilterIndex = 1;
            fd.RestoreDirectory = false;
            fd.ShowHelp = false;
            //fd.SupportMultiDottedExtensions = false; // Methods missing from Mono
            fd.Title = load
                ? pjse.Localization.GetString("strLangLoad")
                : pjse.Localization.GetString("strLangSave");
            fd.ValidateNames = true;
            DialogResult dr = fd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if (load)
                {
                    bool savedstate = internalchg;
                    internalchg = true;

                    wrapper.ImportLanguage(lid, fd.FileName);

                    byte l = lid;
                    int i = index;
                    updateLists();

                    internalchg = savedstate;

                    setLid(l);
                    setIndex((i >= count) ? count - 1 : i);
                }
                else
                    wrapper.ExportLanguage(lid, fd.FileName);
            }
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
            wrapper = (StrWrapper)wrp;
            this.WrapperChanged(wrapper, null);

            internalchg = true;
            updateLists();
            this.ckbDefault.Checked = pjse.Settings.PJSE.StrShowDefault;
            this.ckbDescription.Checked = pjse.Settings.PJSE.StrShowDesc;
            internalchg = false;

            setLid(1);
            setIndex(count > 0 ? 0 : -1);
            ckb_CheckedChanged(null, null);

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
            this.tbFormat.Text = "0x" + Helper.HexString(wrapper.Format);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StrForm));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "0xDDDD (22222)", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F)),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Other language", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)))),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "WWWWWwibbbly"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "American"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "WWWWWwibbly")}, -1);
            this.strPanel = new System.Windows.Forms.Panel();
            this.BtnClean = new System.Windows.Forms.Button();
            this.pjse_banner1 = new pjse.pjse_banner();
            this.ckbDescription = new System.Windows.Forms.CheckBox();
            this.btnLngFirst = new System.Windows.Forms.Button();
            this.btnStrPrev = new System.Windows.Forms.Button();
            this.btnStrNext = new System.Windows.Forms.Button();
            this.ckbDefault = new System.Windows.Forms.CheckBox();
            this.btnStrClear = new System.Windows.Forms.Button();
            this.lvStrItems = new System.Windows.Forms.ListView();
            this.chString = new System.Windows.Forms.ColumnHeader();
            this.chLang = new System.Windows.Forms.ColumnHeader();
            this.chLangDesc = new System.Windows.Forms.ColumnHeader();
            this.chDefault = new System.Windows.Forms.ColumnHeader();
            this.chDefaultDesc = new System.Windows.Forms.ColumnHeader();
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
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnAppend = new System.Windows.Forms.Button();
            this.btnStrDelete = new System.Windows.Forms.Button();
            this.btnStrAdd = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnStrCopy = new System.Windows.Forms.Button();
            this.btnStrDefault = new System.Windows.Forms.Button();
            this.strPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // strPanel
            // 
            this.strPanel.AutoScroll = true;
            this.strPanel.Controls.Add(this.BtnClean);
            this.strPanel.Controls.Add(this.pjse_banner1);
            this.strPanel.Controls.Add(this.ckbDescription);
            this.strPanel.Controls.Add(this.btnLngFirst);
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
            this.strPanel.Controls.Add(this.btnImport);
            this.strPanel.Controls.Add(this.btnExport);
            this.strPanel.Controls.Add(this.btnAppend);
            this.strPanel.Controls.Add(this.btnStrDelete);
            this.strPanel.Controls.Add(this.btnStrAdd);
            this.strPanel.Controls.Add(this.btnReplace);
            this.strPanel.Controls.Add(this.btnStrCopy);
            this.strPanel.Controls.Add(this.btnStrDefault);
            this.strPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.strPanel.Location = new System.Drawing.Point(0, 0);
            this.strPanel.Margin = new System.Windows.Forms.Padding(2);
            this.strPanel.Name = "strPanel";
            this.strPanel.Size = new System.Drawing.Size(621, 380);
            this.strPanel.TabIndex = 0;
            this.strPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.strPanel_Paint);
            this.strPanel.Resize += new System.EventHandler(this.strPanel_Resize);
            // 
            // BtnClean
            // 
            this.BtnClean.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BtnClean.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnClean.Location = new System.Drawing.Point(499, 116);
            this.BtnClean.Margin = new System.Windows.Forms.Padding(2);
            this.BtnClean.Name = "BtnClean";
            this.BtnClean.Size = new System.Drawing.Size(75, 22);
            this.BtnClean.TabIndex = 28;
            this.BtnClean.Text = "Clean File";
            this.BtnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // pjse_banner1
            // 
            this.pjse_banner1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pjse_banner1.Location = new System.Drawing.Point(0, 0);
            this.pjse_banner1.Margin = new System.Windows.Forms.Padding(2);
            this.pjse_banner1.Name = "pjse_banner1";
            this.pjse_banner1.Size = new System.Drawing.Size(621, 28);
            this.pjse_banner1.TabIndex = 27;
            this.pjse_banner1.TitleText = "Text Lists";
            this.pjse_banner1.TreeText = "Comments";
            // 
            // ckbDescription
            // 
            this.ckbDescription.AutoSize = true;
            this.ckbDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbDescription.Location = new System.Drawing.Point(336, 142);
            this.ckbDescription.Margin = new System.Windows.Forms.Padding(2);
            this.ckbDescription.Name = "ckbDescription";
            this.ckbDescription.Size = new System.Drawing.Size(147, 17);
            this.ckbDescription.TabIndex = 15;
            this.ckbDescription.Text = "Reveal string descriptions";
            this.ckbDescription.CheckedChanged += new System.EventHandler(this.ckb_CheckedChanged);
            // 
            // btnLngFirst
            // 
            this.btnLngFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnLngFirst.Image")));
            this.btnLngFirst.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLngFirst.Location = new System.Drawing.Point(74, 116);
            this.btnLngFirst.Margin = new System.Windows.Forms.Padding(2);
            this.btnLngFirst.Name = "btnLngFirst";
            this.btnLngFirst.Size = new System.Drawing.Size(18, 21);
            this.btnLngFirst.TabIndex = 10;
            this.btnLngFirst.Text = "    &^";
            this.btnLngFirst.Click += new System.EventHandler(this.btnLngFirst_Click);
            // 
            // btnStrPrev
            // 
            this.btnStrPrev.Font = new System.Drawing.Font("Wingdings", 8.25F);
            this.btnStrPrev.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrPrev.Location = new System.Drawing.Point(0, 93);
            this.btnStrPrev.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrPrev.Name = "btnStrPrev";
            this.btnStrPrev.Size = new System.Drawing.Size(17, 17);
            this.btnStrPrev.TabIndex = 5;
            this.btnStrPrev.Text = "á         &Up";
            this.btnStrPrev.Click += new System.EventHandler(this.btnStrPrev_Click);
            // 
            // btnStrNext
            // 
            this.btnStrNext.Font = new System.Drawing.Font("Wingdings", 8.25F);
            this.btnStrNext.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrNext.Location = new System.Drawing.Point(22, 93);
            this.btnStrNext.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrNext.Name = "btnStrNext";
            this.btnStrNext.Size = new System.Drawing.Size(17, 17);
            this.btnStrNext.TabIndex = 6;
            this.btnStrNext.Text = "â         &Down";
            this.btnStrNext.Click += new System.EventHandler(this.btnStrNext_Click);
            // 
            // ckbDefault
            // 
            this.ckbDefault.AutoSize = true;
            this.ckbDefault.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbDefault.Location = new System.Drawing.Point(108, 142);
            this.ckbDefault.Margin = new System.Windows.Forms.Padding(2);
            this.ckbDefault.Name = "ckbDefault";
            this.ckbDefault.Size = new System.Drawing.Size(214, 17);
            this.ckbDefault.TabIndex = 14;
            this.ckbDefault.Text = "Reveal English (US) for comparison";
            this.ckbDefault.CheckedChanged += new System.EventHandler(this.ckb_CheckedChanged);
            // 
            // btnStrClear
            // 
            this.btnStrClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStrClear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrClear.Location = new System.Drawing.Point(0, 298);
            this.btnStrClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrClear.Name = "btnStrClear";
            this.btnStrClear.Size = new System.Drawing.Size(106, 22);
            this.btnStrClear.TabIndex = 24;
            this.btnStrClear.Text = "US string only";
            this.btnStrClear.Click += new System.EventHandler(this.btnStrClear_Click);
            // 
            // lvStrItems
            // 
            this.lvStrItems.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvStrItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvStrItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chString,
            this.chLang,
            this.chLangDesc,
            this.chDefault,
            this.chDefaultDesc});
            this.lvStrItems.FullRowSelect = true;
            this.lvStrItems.GridLines = true;
            this.lvStrItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvStrItems.HideSelection = false;
            this.lvStrItems.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lvStrItems.LabelWrap = false;
            this.lvStrItems.Location = new System.Drawing.Point(110, 166);
            this.lvStrItems.Margin = new System.Windows.Forms.Padding(2);
            this.lvStrItems.MultiSelect = false;
            this.lvStrItems.Name = "lvStrItems";
            this.lvStrItems.Size = new System.Drawing.Size(508, 214);
            this.lvStrItems.TabIndex = 26;
            this.lvStrItems.UseCompatibleStateImageBehavior = false;
            this.lvStrItems.View = System.Windows.Forms.View.Details;
            this.lvStrItems.ItemActivate += new System.EventHandler(this.lvStrItems_ItemActivate);
            this.lvStrItems.SelectedIndexChanged += new System.EventHandler(this.lvStrItems_SelectedIndexChanged);
            // 
            // chString
            // 
            this.chString.Text = "#";
            this.chString.Width = 115;
            // 
            // chLang
            // 
            this.chLang.Text = "Language";
            this.chLang.Width = 166;
            // 
            // chLangDesc
            // 
            this.chLangDesc.Text = "Description";
            this.chLangDesc.Width = 50;
            // 
            // chDefault
            // 
            this.chDefault.Text = "English (US)";
            this.chDefault.Width = 122;
            // 
            // chDefaultDesc
            // 
            this.chDefaultDesc.Text = "Description";
            this.chDefaultDesc.Width = 50;
            // 
            // btnBigDesc
            // 
            this.btnBigDesc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBigDesc.Font = new System.Drawing.Font("Webdings", 10F);
            this.btnBigDesc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBigDesc.Location = new System.Drawing.Point(592, 93);
            this.btnBigDesc.Margin = new System.Windows.Forms.Padding(2);
            this.btnBigDesc.Name = "btnBigDesc";
            this.btnBigDesc.Size = new System.Drawing.Size(17, 17);
            this.btnBigDesc.TabIndex = 4;
            this.btnBigDesc.Text = "4";
            this.btnBigDesc.Click += new System.EventHandler(this.btnBigString_Click);
            // 
            // btnBigString
            // 
            this.btnBigString.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBigString.Font = new System.Drawing.Font("Webdings", 10F);
            this.btnBigString.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBigString.Location = new System.Drawing.Point(293, 94);
            this.btnBigString.Margin = new System.Windows.Forms.Padding(2);
            this.btnBigString.Name = "btnBigString";
            this.btnBigString.Size = new System.Drawing.Size(18, 17);
            this.btnBigString.TabIndex = 3;
            this.btnBigString.Text = "4";
            this.btnBigString.Click += new System.EventHandler(this.btnBigString_Click);
            // 
            // lbDesc
            // 
            this.lbDesc.AutoSize = true;
            this.lbDesc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbDesc.Location = new System.Drawing.Point(316, 75);
            this.lbDesc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDesc.Name = "lbDesc";
            this.lbDesc.Size = new System.Drawing.Size(32, 13);
            this.lbDesc.TabIndex = 0;
            this.lbDesc.Text = "Desc";
            this.lbDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(14, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "String";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rtbDescription
            // 
            this.rtbDescription.Location = new System.Drawing.Point(352, 73);
            this.rtbDescription.Margin = new System.Windows.Forms.Padding(2);
            this.rtbDescription.Name = "rtbDescription";
            this.rtbDescription.Size = new System.Drawing.Size(239, 39);
            this.rtbDescription.TabIndex = 2;
            this.rtbDescription.Text = "Description";
            this.rtbDescription.Enter += new System.EventHandler(this.textBoxBase_Enter);
            this.rtbDescription.TextChanged += new System.EventHandler(this.textBoxBase_TextChanged);
            // 
            // rtbTitle
            // 
            this.rtbTitle.Location = new System.Drawing.Point(55, 73);
            this.rtbTitle.Margin = new System.Windows.Forms.Padding(2);
            this.rtbTitle.Name = "rtbTitle";
            this.rtbTitle.Size = new System.Drawing.Size(239, 39);
            this.rtbTitle.TabIndex = 1;
            this.rtbTitle.Text = "Title";
            this.rtbTitle.Enter += new System.EventHandler(this.textBoxBase_Enter);
            this.rtbTitle.TextChanged += new System.EventHandler(this.textBoxBase_TextChanged);
            // 
            // btnLngNext
            // 
            this.btnLngNext.Font = new System.Drawing.Font("Webdings", 16F);
            this.btnLngNext.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLngNext.Location = new System.Drawing.Point(239, 116);
            this.btnLngNext.Margin = new System.Windows.Forms.Padding(2);
            this.btnLngNext.Name = "btnLngNext";
            this.btnLngNext.Size = new System.Drawing.Size(17, 21);
            this.btnLngNext.TabIndex = 13;
            this.btnLngNext.Text = "4 &>";
            this.btnLngNext.Click += new System.EventHandler(this.btnLngNext_Click);
            // 
            // btnLngPrev
            // 
            this.btnLngPrev.Font = new System.Drawing.Font("Webdings", 16F);
            this.btnLngPrev.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLngPrev.Location = new System.Drawing.Point(92, 116);
            this.btnLngPrev.Margin = new System.Windows.Forms.Padding(2);
            this.btnLngPrev.Name = "btnLngPrev";
            this.btnLngPrev.Size = new System.Drawing.Size(17, 21);
            this.btnLngPrev.TabIndex = 11;
            this.btnLngPrev.Text = "3 &<";
            this.btnLngPrev.Click += new System.EventHandler(this.btnLngPrev_Click);
            // 
            // btnLngClear
            // 
            this.btnLngClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLngClear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLngClear.Location = new System.Drawing.Point(0, 192);
            this.btnLngClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnLngClear.Name = "btnLngClear";
            this.btnLngClear.Size = new System.Drawing.Size(106, 22);
            this.btnLngClear.TabIndex = 20;
            this.btnLngClear.Text = "Clear lang";
            this.btnLngClear.Click += new System.EventHandler(this.btnLngClear_Click);
            // 
            // cbLngSelect
            // 
            this.cbLngSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLngSelect.DropDownWidth = 200;
            this.cbLngSelect.ItemHeight = 13;
            this.cbLngSelect.Location = new System.Drawing.Point(113, 116);
            this.cbLngSelect.Margin = new System.Windows.Forms.Padding(2);
            this.cbLngSelect.Name = "cbLngSelect";
            this.cbLngSelect.Size = new System.Drawing.Size(122, 21);
            this.cbLngSelect.TabIndex = 12;
            this.cbLngSelect.SelectedIndexChanged += new System.EventHandler(this.cbLngSelect_SelectedIndexChanged);
            // 
            // lbLngSelect
            // 
            this.lbLngSelect.AutoSize = true;
            this.lbLngSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbLngSelect.Location = new System.Drawing.Point(5, 118);
            this.lbLngSelect.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbLngSelect.Name = "lbLngSelect";
            this.lbLngSelect.Size = new System.Drawing.Size(63, 13);
            this.lbLngSelect.TabIndex = 0;
            this.lbLngSelect.Text = "Go to / Add";
            // 
            // btnClearAll
            // 
            this.btnClearAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClearAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClearAll.Location = new System.Drawing.Point(0, 166);
            this.btnClearAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(106, 22);
            this.btnClearAll.TabIndex = 19;
            this.btnClearAll.Text = "US English Only";
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // lbStringNum
            // 
            this.lbStringNum.AutoSize = true;
            this.lbStringNum.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbStringNum.Location = new System.Drawing.Point(52, 57);
            this.lbStringNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbStringNum.Name = "lbStringNum";
            this.lbStringNum.Size = new System.Drawing.Size(80, 13);
            this.lbStringNum.TabIndex = 0;
            this.lbStringNum.Text = "String 0xDDDD";
            this.lbStringNum.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tbFilename
            // 
            this.tbFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilename.Location = new System.Drawing.Point(55, 35);
            this.tbFilename.Margin = new System.Windows.Forms.Padding(2);
            this.tbFilename.MaxLength = 64;
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.Size = new System.Drawing.Size(378, 20);
            this.tbFilename.TabIndex = 7;
            this.tbFilename.Text = "Filename";
            this.tbFilename.TextChanged += new System.EventHandler(this.textBoxBase_TextChanged);
            this.tbFilename.Enter += new System.EventHandler(this.textBoxBase_Enter);
            // 
            // lbFilename
            // 
            this.lbFilename.AutoSize = true;
            this.lbFilename.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbFilename.Location = new System.Drawing.Point(-2, 38);
            this.lbFilename.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFilename.Name = "lbFilename";
            this.lbFilename.Size = new System.Drawing.Size(49, 13);
            this.lbFilename.TabIndex = 0;
            this.lbFilename.Text = "Filename";
            this.lbFilename.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // btnCommit
            // 
            this.btnCommit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCommit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCommit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCommit.Location = new System.Drawing.Point(534, 35);
            this.btnCommit.Margin = new System.Windows.Forms.Padding(2);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(75, 22);
            this.btnCommit.TabIndex = 9;
            this.btnCommit.Text = "Commit &File";
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // lbFormat
            // 
            this.lbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFormat.AutoSize = true;
            this.lbFormat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbFormat.Location = new System.Drawing.Point(432, 38);
            this.lbFormat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFormat.Name = "lbFormat";
            this.lbFormat.Size = new System.Drawing.Size(39, 13);
            this.lbFormat.TabIndex = 0;
            this.lbFormat.Text = "Format";
            this.lbFormat.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // tbFormat
            // 
            this.tbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFormat.Location = new System.Drawing.Point(479, 37);
            this.tbFormat.Margin = new System.Windows.Forms.Padding(2);
            this.tbFormat.MaxLength = 6;
            this.tbFormat.Name = "tbFormat";
            this.tbFormat.Size = new System.Drawing.Size(49, 20);
            this.tbFormat.TabIndex = 8;
            this.tbFormat.Text = "0xDDDD";
            this.tbFormat.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbFormat.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbFormat.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // btnImport
            // 
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnImport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnImport.Location = new System.Drawing.Point(340, 116);
            this.btnImport.Margin = new System.Windows.Forms.Padding(2);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 22);
            this.btnImport.TabIndex = 17;
            this.btnImport.Text = "Import Lang";
            this.btnImport.Click += new System.EventHandler(this.btnStringFile_Click);
            // 
            // btnExport
            // 
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnExport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExport.Location = new System.Drawing.Point(260, 116);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 22);
            this.btnExport.TabIndex = 16;
            this.btnExport.Text = "Export Lang";
            this.btnExport.Click += new System.EventHandler(this.btnStringFile_Click);
            // 
            // btnAppend
            // 
            this.btnAppend.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAppend.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAppend.Location = new System.Drawing.Point(420, 116);
            this.btnAppend.Margin = new System.Windows.Forms.Padding(2);
            this.btnAppend.Name = "btnAppend";
            this.btnAppend.Size = new System.Drawing.Size(75, 22);
            this.btnAppend.TabIndex = 18;
            this.btnAppend.Text = "A&ppend File";
            this.btnAppend.Click += new System.EventHandler(this.btnAppend_Click);
            // 
            // btnStrDelete
            // 
            this.btnStrDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStrDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrDelete.Location = new System.Drawing.Point(55, 245);
            this.btnStrDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrDelete.Name = "btnStrDelete";
            this.btnStrDelete.Size = new System.Drawing.Size(50, 22);
            this.btnStrDelete.TabIndex = 22;
            this.btnStrDelete.Text = "De&lete";
            this.btnStrDelete.Click += new System.EventHandler(this.btnStrDelete_Click);
            // 
            // btnStrAdd
            // 
            this.btnStrAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStrAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrAdd.Location = new System.Drawing.Point(0, 245);
            this.btnStrAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrAdd.Name = "btnStrAdd";
            this.btnStrAdd.Size = new System.Drawing.Size(50, 22);
            this.btnStrAdd.TabIndex = 21;
            this.btnStrAdd.Text = "&Add";
            this.btnStrAdd.Click += new System.EventHandler(this.btnStrAdd_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnReplace.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnReplace.Location = new System.Drawing.Point(0, 351);
            this.btnReplace.Margin = new System.Windows.Forms.Padding(2);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(106, 22);
            this.btnReplace.TabIndex = 25;
            this.btnReplace.Text = "Replace string...";
            this.btnReplace.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnStrCopy
            // 
            this.btnStrCopy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStrCopy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrCopy.Location = new System.Drawing.Point(0, 324);
            this.btnStrCopy.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrCopy.Name = "btnStrCopy";
            this.btnStrCopy.Size = new System.Drawing.Size(106, 22);
            this.btnStrCopy.TabIndex = 23;
            this.btnStrCopy.Text = "Copy string to all";
            this.btnStrCopy.Click += new System.EventHandler(this.btnStrCopy_Click);
            // 
            // btnStrDefault
            // 
            this.btnStrDefault.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStrDefault.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrDefault.Location = new System.Drawing.Point(0, 272);
            this.btnStrDefault.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrDefault.Name = "btnStrDefault";
            this.btnStrDefault.Size = new System.Drawing.Size(106, 22);
            this.btnStrDefault.TabIndex = 23;
            this.btnStrDefault.Text = "Make default";
            this.btnStrDefault.Click += new System.EventHandler(this.btnStrDefault_Click);
            // 
            // StrForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(621, 380);
            this.Controls.Add(this.strPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "StrForm";
            this.Text = "StrForm";
            this.strPanel.ResumeLayout(false);
            this.strPanel.PerformLayout();
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
            switch (alTextBoxBase.IndexOf(sender))
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

        private void lvStrItems_ItemActivate(object sender, System.EventArgs e)
        {
            this.rtbTitle.Focus();
        }

        private void lvStrItems_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (internalchg) return;
            setIndex((this.lvStrItems.SelectedIndices.Count > 0) ? this.lvStrItems.SelectedIndices[0] : -1);
        }

        private void ckb_CheckedChanged(object sender, System.EventArgs e)
        {
            if (internalchg) return;
            pjse.Settings.PJSE.StrShowDefault = this.ckbDefault.Checked;
            pjse.Settings.PJSE.StrShowDesc = this.ckbDescription.Checked;

            int w1 = this.lvStrItems.ClientRectangle.Width - 115;//was this.chString.Width - 18 
            int w2 = this.ckbDescription.Checked ? w1 / 2 : 0;
            /*
            this.chString.Width = 115;
            this.chLang.Width = 166;
            this.chLangDesc.Width = 50;
            this.chDefault.Width = 122;
            this.chDefaultDesc.Width = 50;
            */
            if (this.ckbDefault.Checked && lid != 1) { w1 /= 2; w2 /= 4; }
            w1 -= w2;
            this.chLangDesc.Width = this.chDefault.Width = this.chDefaultDesc.Width = 0;
            this.chLang.Width = w1;
            this.chLangDesc.Width = w2;
            if (this.ckbDefault.Checked && lid != 1)
            {
                this.chDefault.Width = w1;
                this.chDefaultDesc.Width = w2;
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

        private void btnLngFirst_Click(object sender, System.EventArgs e)
        {
            setLid(1);
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
            this.rtbTitle.Focus();
        }

        private void btnStrDelete_Click(object sender, System.EventArgs e)
        {
            this.StrDelete();
        }

        private void btnStrDefault_Click(object sender, System.EventArgs e)
        {
            StrDefault();
        }

        private void btnClean_Click(object sender, System.EventArgs e)
        {
            CleanAll();
        }

        private void btnStrClear_Click(object sender, System.EventArgs e)
        {
            this.StrClear();
        }

        private void btnAppend_Click(object sender, System.EventArgs e)
        {
            this.Append((new pjse.ResourceChooser()).Execute(wrapper.FileDescriptor.Type, wrapper.FileDescriptor.Group, strPanel, true));
        }

        private void btnStrCopy_Click(object sender, EventArgs e)
        {
            this.StrCopy();
        }

        private void btnImport_Click(object sender, System.EventArgs e)
        {
            this.StrReplace();
        }

        private void btnCommit_Click(object sender, System.EventArgs e)
        {
            this.Commit();
        }

        private void btnStringFile_Click(object sender, EventArgs e)
        {
            this.StringFile(sender.Equals(this.btnImport));
        }

        private void strPanel_Paint(object sender, PaintEventArgs e)
        { }
    }
}
