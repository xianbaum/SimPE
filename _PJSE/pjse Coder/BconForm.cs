/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   pljones@users.sf.net                                                  *
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
        private IContainer components;
		#region Form variables

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
            pjse.FileTable.GFT.FiletableRefresh += new System.EventHandler(this.FiletableRefresh);
            if (SimPe.Helper.WindowsRegistry.UseBigIcons) this.lvConstants.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            if (booby.ThemeManager.ThemedForms)
            {
                this.bconPanel.BackColor = booby.ThemeManager.Global.ThemeColorLight;
                this.lvConstants.BackColor = booby.ThemeManager.Global.ThemeColorLighter;
                booby.ThemeManager.Global.AddControl(this.btnCommit);
            }
            this.cbusedec.Visible = Helper.WindowsRegistry.CreatorMode;
            if (!pjse.GUIDIndex.TheGUIDIndex.IsLoaded && Helper.WindowsRegistry.CreatorMode) pjse.GUIDIndex.TheGUIDIndex.Load();
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
            if (setHandler && wrapper != null)
            {
                wrapper.WrapperChanged -= new System.EventHandler(this.WrapperChanged);
                setHandler = false;
            }
            wrapper = null;
            trcnres = null;
        }

		#region Controller
		private Bcon wrapper = null;
        private Trcn trcnres = null;
        private Button btnTRCNMaker;
        private GroupBox gbValue;
        private Button btnCancel;
        private TextBox tbValueDec;
        private TextBox tbValueHex;
        private Label label5;
        private Label label6;
        private TextBox tbFilename;
        private Label lbFilename;
        private Button btnCommit;
        private ListView lvConstants;
        private ColumnHeader chID;
        private ColumnHeader chValue;
        private ColumnHeader chLabel;
        private Button btnStrAdd;
        private Button btnStrDelete;
        private CheckBox cbFlag;
        private Button btnStrNext;
        private Button btnStrPrev;
        private pjse.pjse_banner pjse_banner1;
        private Button btnUpdateBCON;
        private LinkLabel llIsOverride;
        private pjse.CompareButton cmpBCON;
        private Button btnClose;
        private CheckBox cbusedec;
        private Panel bconPanel;
        private TextBox tbValueGui;
        private Label label1;
        private ToolTip ttBconForm;

        private bool setHandler = false;
        private bool internalchg = false;
        private int index = -1;
        private short origItem = -1;
        private short currentItem = -1;
        private short prevItem = -1;

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
            if (cbusedec.Checked)
                lvConstants.SelectedItems[0].SubItems[1].Text = currentItem.ToString();
            else
                lvConstants.SelectedItems[0].SubItems[1].Text = "0x" + SimPe.Helper.HexString(currentItem);
			if (doHex)
				tbValueHex.Text = lvConstants.SelectedItems[0].SubItems[1].Text;
			if (doDec)
				tbValueDec.Text = currentItem.ToString();
            internalchg = false;
            setGuidItem();
		}

		private ListViewItem lvItem(int i)
        {
            string cID;
            string cValue;
            if (cbusedec.Checked)
            {
                cID = i.ToString();
                cValue = wrapper[i].ToString();
            }
            else
            {
                cID = "0x" + i.ToString("X") + " (" + i + ")";
                cValue = "0x" + SimPe.Helper.HexString(wrapper[i]);
            }
            string cLabel = (trcnres != null && !trcnres.TextOnly && i < trcnres.Count) ? trcnres[i].ConstName : "";
			string[] v = { cID, cValue, cLabel };
			return new ListViewItem(v);
		}

		private void updateLists()
		{
			index = -1;
            trcnres = (Trcn)(wrapper == null ? null : wrapper.SiblingResource(Trcn.Trcntype));

			this.lvConstants.Items.Clear();
			int nItems = wrapper == null ? 0 : wrapper.Count;
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
                setGuidItem();
			}
			else
			{
                prevItem = origItem = currentItem = -1;
                this.tbValueHex.Text = this.tbValueDec.Text = tbValueGui.Text = "";
				this.tbValueHex.Enabled = this.tbValueDec.Enabled = false;
                this.tbValueGui.Visible = this.label1.Visible = false;
			}
			this.btnStrPrev.Enabled = (index > 0);
			this.btnStrNext.Enabled = (index < this.lvConstants.Items.Count - 1);
			internalchg = false;
			this.btnCancel.Enabled = false;
		}

        private void setGuidItem()
        {
            if (index > 0 && !cbusedec.Checked && Helper.WindowsRegistry.CreatorMode)
            {
                prevItem = wrapper[index - 1];
                this.tbValueGui.Text = "0x" + SimPe.Helper.HexString(currentItem) + SimPe.Helper.HexString(prevItem);
                this.tbValueGui.Visible = this.label1.Visible = pjse.GUIDIndex.TheGUIDIndex.ContainsKey(SimPe.Helper.HexStringToUInt(this.tbValueGui.Text));
                ttBconForm.SetToolTip(tbValueGui, pjse.GUIDIndex.TheGUIDIndex[SimPe.Helper.HexStringToUInt(this.tbValueGui.Text)]);
            }
            else this.tbValueGui.Visible = this.label1.Visible = false;
        }

        private bool isPopup { get { return this.Tag == null ? false : ((string)(this.Tag)).StartsWith("Popup"); } }
        private bool isNoOverride { get { return this.Tag == null ? false : ((string)(this.Tag)).Contains(";noOverride"); } }
        private string expName
        {
            get
            {
                if (this.Tag != null)
                {
                    string s = (string)this.Tag;
                    int i = s.IndexOf(";expName=+");
                    if (i >= 0) return s.Substring(i + 10).TrimEnd('+');
                }
                foreach (pjse.FileTable.Entry item in pjse.FileTable.GFT[wrapper.Package, wrapper.FileDescriptor])
                    if (item.PFD == wrapper.FileDescriptor)
                    {
                        if (item.IsMaxis) return pjse.Localization.GetString("expCurrent");
                        else break;
                    }
                return pjse.Localization.GetString("expCustom");
            }
        }

        private bool isOverride
        {
            get
            {
                llIsOverride.Tag = null;
                pjse.FileTable.Entry[] items = pjse.FileTable.GFT[wrapper.Package, wrapper.FileDescriptor];
                if (items.Length <= 1) return false;

                pjse.FileTable.Entry item = items[items.Length - 1]; // currentpkg, other, fixed, maxis
                if (item.PFD == wrapper.FileDescriptor) return false;
                if (!item.IsMaxis /*&& !item.IsFixed*/) return false; // only supporting objects.package really

                llIsOverride.Tag = item;
                return true;
            }
        }

        private void common_Popup(pjse.FileTable.Entry item, SimPe.ExpansionItem exp, bool noOverride)
        {
            if (item == null) return; // this should never happen
            Bcon bcon = new Bcon();
            bcon.ProcessData(item.PFD, item.Package);

            BconForm ui = (BconForm)bcon.UIHandler;
            string tag = "Popup"; // tells the SetReadOnly function it's in a popup - so everything locked down
            if (noOverride) tag += ";noOverride"; //
            if (exp != null) tag += ";expName=+" + exp.NameShort + "+";
            ui.Tag = tag;

            bcon.RefreshUI();
            ui.Show();
        }

        private String formTitle
        {
            get
            {
                return pjse.Localization.GetString("pjseWindowTitle"
                    , expName // EP Name or Custom
                    , System.IO.Path.GetFileName(wrapper.Package.SaveFileName) // package Filename without path
                    , wrapper.FileDescriptor.TypeName.shortname // Type (short name)
                    , "0x" + SimPe.Helper.HexString(wrapper.FileDescriptor.Group) // Group Number
                    , "0x" + SimPe.Helper.HexString((ushort)wrapper.FileDescriptor.Instance) // Instance Number
                    , wrapper.FileName
                    , pjse.Localization.GetString(isPopup ? "pjseWindowTitleView" : "pjseWindowTitleEdit")
                    );
            }
        }


        private void doUpdateBCON()
        {
            if (!isOverride) return; // this should never happen
            pjse.FileTable.Entry item = (pjse.FileTable.Entry)llIsOverride.Tag;
            Bcon bcon = new Bcon();
            bcon.ProcessData(item.PFD, item.Package);
            internalchg = true;
            while (wrapper.Count < bcon.Count)
                wrapper.Add(new BconItem(bcon[wrapper.Count]));
            internalchg = false;
            updateLists();
        }



		private void BconItemAdd()
		{
			bool savedstate = internalchg;
			internalchg = true;

            try
            {
                wrapper.Add(0);
                this.lvConstants.Items.Add(lvItem(wrapper.Count - 1));
            }
            catch { }

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
				Helper.ExceptionMessage(pjse.Localization.GetString("errwritingfile"), ex);
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
            bconPanel.Cursor = Cursors.WaitCursor;
            Application.UseWaitCursor = true;
            try
            {
                int minArgc = 0;
                Trcn trcn = (Trcn)wrapper.SiblingResource(Trcn.Trcntype); // find Trcn for this Bcon

                wrapper.Package.BeginUpdate();

                if (trcn != null && trcn.TextOnly)
                {
                    // if it exists but is unreadable, as if user wants to overwrite
                    DialogResult dr = MessageBox.Show(
                        pjse.Localization.GetString("ml_overwriteduff")
                        , btnTRCNMaker.Text
                        , MessageBoxButtons.OKCancel
                        , MessageBoxIcon.Warning);
                    if (dr != DialogResult.OK)
                        return;
                    wrapper.Package.Remove(trcn.FileDescriptor);
                    trcn = null;
                }
                if (trcn != null)
                {
                    uint vers = trcn.Version;
                    // if it exists ask if user wants to preserve content
                    DialogResult dr = MessageBox.Show(
                        pjse.Localization.GetString("ml_keeplabels")
                        , btnTRCNMaker.Text
                        , MessageBoxButtons.YesNoCancel
                        , MessageBoxIcon.Warning);
                    if (dr == DialogResult.Cancel)
                        return;

                    if (!trcn.Package.Equals(wrapper.Package))
                    {
                        // Clone the original into this package
                        if (dr == DialogResult.Yes) Wait.MaxProgress = trcn.Count;
                        SimPe.Interfaces.Files.IPackedFileDescriptor npfd = trcn.FileDescriptor.Clone();
                        Trcn ntrcn = new Trcn();
                        ntrcn.FileDescriptor = npfd;
                        wrapper.Package.Add(npfd, true);
                        ntrcn.ProcessData(npfd, wrapper.Package);
                        if (dr == DialogResult.Yes) foreach (TrcnItem item in trcn) { ntrcn.Add(item); Wait.Progress++; }
                        trcn = ntrcn;
                        trcn.SynchronizeUserData();
                        trcn.Version = vers; // this screws up the header, Version (header[1]) is the only bit we need
                        Wait.MaxProgress = 0;
                    }

                    if (dr == DialogResult.Yes)
                        minArgc = trcn.Count;
                    else
                        trcn.Clear();
                }
                else
                {
                    // create a new Trcn file
                    SimPe.Interfaces.Files.IPackedFileDescriptor npfd = wrapper.FileDescriptor.Clone();
                    trcn = new Trcn();
                    npfd.Type = Trcn.Trcntype;
                    trcn.FileDescriptor = npfd;
                    wrapper.Package.Add(npfd, true);
                    trcn.SynchronizeUserData();
                }

                Wait.MaxProgress = wrapper.Count - minArgc;
                trcn.FileName = wrapper.FileName;

                for (int arg = minArgc; arg < wrapper.Count; arg++)
                {
                    trcn.Add(new TrcnItem(trcn));
                    trcn[arg].ConstId = (uint)arg;
                    trcn[arg].ConstName = "Label " + arg.ToString();
                    trcn[arg].DefValue = trcn[arg].MaxValue = trcn[arg].MinValue = 0;
                    Wait.Progress++;
                }
                trcn.SynchronizeUserData();
                wrapper.Package.EndUpdate();
            }
            finally
            {
                Wait.SubStop();
                bconPanel.Cursor = Cursors.Default;
                Application.UseWaitCursor = false;
            }
            MessageBox.Show(
                    pjse.Localization.GetString("ml_done")
                    , btnTRCNMaker.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FiletableRefresh(object sender, System.EventArgs e)
        {
            pjse_banner1.SiblingEnabled = wrapper != null && wrapper.SiblingResource(Trcn.Trcntype) != null;
            updateLists();
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
            pjse_banner1.SiblingEnabled = wrapper.SiblingResource(Trcn.Trcntype) != null;

			internalchg = true;
			updateLists();
			internalchg = false;

			setIndex(lvConstants.Items.Count > 0 ? 0 : -1);

            //tbFilename.Enabled = cbFlag.Enabled = tbValueHex.Enabled = tbValueDec.Enabled = !isPopup;
            btnClose.Visible = isPopup;

			if (!setHandler)
			{
				wrapper.WrapperChanged += new System.EventHandler(this.WrapperChanged);
				setHandler = true;
			}
		}

		private void WrapperChanged(object sender, System.EventArgs e)
		{
            if (isPopup) wrapper.Changed = false;

            this.btnCommit.Enabled = wrapper.Changed;
            if (index >= 0 && sender is BconItem && wrapper.IndexOf((BconItem)sender) == index)
            {
                this.btnCancel.Enabled = true;
                return;
            }

			if (internalchg) return;

            if (sender.Equals(wrapper))
            {
                internalchg = true;
                this.Text = formTitle;
                this.cbFlag.Checked = wrapper.Flag;
                this.llIsOverride.Visible = !isNoOverride && isOverride;
                tbFilename.Text = wrapper.FileName;
                cmpBCON.Wrapper = wrapper;
                cmpBCON.WrapperName = wrapper.FileName;
                internalchg = false;
			}
            else
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "0xDD (222)",
            "0xDDDD",
            "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F));
            this.btnTRCNMaker = new System.Windows.Forms.Button();
            this.gbValue = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbValueDec = new System.Windows.Forms.TextBox();
            this.tbValueHex = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbFilename = new System.Windows.Forms.TextBox();
            this.lbFilename = new System.Windows.Forms.Label();
            this.btnCommit = new System.Windows.Forms.Button();
            this.lvConstants = new System.Windows.Forms.ListView();
            this.chID = new System.Windows.Forms.ColumnHeader();
            this.chValue = new System.Windows.Forms.ColumnHeader();
            this.chLabel = new System.Windows.Forms.ColumnHeader();
            this.btnStrAdd = new System.Windows.Forms.Button();
            this.btnStrDelete = new System.Windows.Forms.Button();
            this.cbFlag = new System.Windows.Forms.CheckBox();
            this.btnStrNext = new System.Windows.Forms.Button();
            this.btnStrPrev = new System.Windows.Forms.Button();
            this.pjse_banner1 = new pjse.pjse_banner();
            this.btnUpdateBCON = new System.Windows.Forms.Button();
            this.llIsOverride = new System.Windows.Forms.LinkLabel();
            this.cmpBCON = new pjse.CompareButton();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbusedec = new System.Windows.Forms.CheckBox();
            this.bconPanel = new System.Windows.Forms.Panel();
            this.tbValueGui = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ttBconForm = new System.Windows.Forms.ToolTip(this.components);
            this.gbValue.SuspendLayout();
            this.bconPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTRCNMaker
            // 
            this.btnTRCNMaker.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnTRCNMaker.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnTRCNMaker.Location = new System.Drawing.Point(94, 143);
            this.btnTRCNMaker.Margin = new System.Windows.Forms.Padding(2);
            this.btnTRCNMaker.Name = "btnTRCNMaker";
            this.btnTRCNMaker.Size = new System.Drawing.Size(79, 22);
            this.btnTRCNMaker.TabIndex = 2;
            this.btnTRCNMaker.Text = "Make Labels";
            this.btnTRCNMaker.Click += new System.EventHandler(this.btnTRCNMaker_Click);
            // 
            // gbValue
            // 
            this.gbValue.BackColor = System.Drawing.Color.Transparent;
            this.gbValue.Controls.Add(this.btnCancel);
            this.gbValue.Controls.Add(this.tbValueDec);
            this.gbValue.Controls.Add(this.tbValueHex);
            this.gbValue.Controls.Add(this.label5);
            this.gbValue.Controls.Add(this.label6);
            this.gbValue.Location = new System.Drawing.Point(0, 81);
            this.gbValue.Margin = new System.Windows.Forms.Padding(2);
            this.gbValue.Name = "gbValue";
            this.gbValue.Padding = new System.Windows.Forms.Padding(2);
            this.gbValue.Size = new System.Drawing.Size(89, 88);
            this.gbValue.TabIndex = 1;
            this.gbValue.TabStop = false;
            this.gbValue.Text = "Value";
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(5, 62);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(79, 22);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbValueDec
            // 
            this.tbValueDec.Location = new System.Drawing.Point(36, 39);
            this.tbValueDec.Margin = new System.Windows.Forms.Padding(2);
            this.tbValueDec.Name = "tbValueDec";
            this.tbValueDec.Size = new System.Drawing.Size(49, 20);
            this.tbValueDec.TabIndex = 2;
            this.tbValueDec.Text = "-88888";
            this.tbValueDec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbValueDec.TextChanged += new System.EventHandler(this.dec16_TextChanged);
            this.tbValueDec.Validated += new System.EventHandler(this.dec16_Validated);
            this.tbValueDec.Enter += new System.EventHandler(this.tbText_Enter);
            this.tbValueDec.Validating += new System.ComponentModel.CancelEventHandler(this.dec16_Validating);
            // 
            // tbValueHex
            // 
            this.tbValueHex.Location = new System.Drawing.Point(36, 17);
            this.tbValueHex.Margin = new System.Windows.Forms.Padding(2);
            this.tbValueHex.Name = "tbValueHex";
            this.tbValueHex.Size = new System.Drawing.Size(49, 20);
            this.tbValueHex.TabIndex = 1;
            this.tbValueHex.Text = "0xDDDD";
            this.tbValueHex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbValueHex.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbValueHex.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbValueHex.Enter += new System.EventHandler(this.tbText_Enter);
            this.tbValueHex.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(5, 19);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Hex";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(5, 42);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Dec";
            // 
            // tbFilename
            // 
            this.tbFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilename.Location = new System.Drawing.Point(57, 32);
            this.tbFilename.Margin = new System.Windows.Forms.Padding(2);
            this.tbFilename.MaxLength = 64;
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.Size = new System.Drawing.Size(520, 20);
            this.tbFilename.TabIndex = 7;
            this.tbFilename.Text = "fffffffffffffffff";
            this.tbFilename.TextChanged += new System.EventHandler(this.tbFilename_TextChanged);
            this.tbFilename.Enter += new System.EventHandler(this.tbText_Enter);
            // 
            // lbFilename
            // 
            this.lbFilename.AutoSize = true;
            this.lbFilename.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbFilename.Location = new System.Drawing.Point(0, 35);
            this.lbFilename.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFilename.Name = "lbFilename";
            this.lbFilename.Size = new System.Drawing.Size(49, 13);
            this.lbFilename.TabIndex = 6;
            this.lbFilename.Text = "Filename";
            // 
            // btnCommit
            // 
            this.btnCommit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCommit.AutoSize = true;
            this.btnCommit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCommit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCommit.Location = new System.Drawing.Point(636, 31);
            this.btnCommit.Margin = new System.Windows.Forms.Padding(2);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(87, 23);
            this.btnCommit.TabIndex = 9;
            this.btnCommit.Text = "Commit &File";
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Clicked);
            // 
            // lvConstants
            // 
            this.lvConstants.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvConstants.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chID,
            this.chValue,
            this.chLabel});
            this.lvConstants.FullRowSelect = true;
            this.lvConstants.GridLines = true;
            this.lvConstants.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvConstants.HideSelection = false;
            this.lvConstants.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lvConstants.Location = new System.Drawing.Point(177, 63);
            this.lvConstants.Margin = new System.Windows.Forms.Padding(2);
            this.lvConstants.MultiSelect = false;
            this.lvConstants.Name = "lvConstants";
            this.lvConstants.Size = new System.Drawing.Size(552, 254);
            this.lvConstants.TabIndex = 3;
            this.lvConstants.UseCompatibleStateImageBehavior = false;
            this.lvConstants.View = System.Windows.Forms.View.Details;
            this.lvConstants.SelectedIndexChanged += new System.EventHandler(this.lvConstants_SelectedIndexChanged);
            // 
            // chID
            // 
            this.chID.Text = "Line";
            this.chID.Width = 89;
            // 
            // chValue
            // 
            this.chValue.Text = "Value";
            this.chValue.Width = 66;
            // 
            // chLabel
            // 
            this.chLabel.Text = "Label";
            this.chLabel.Width = 374;
            // 
            // btnStrAdd
            // 
            this.btnStrAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStrAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrAdd.Location = new System.Drawing.Point(9, 55);
            this.btnStrAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrAdd.Name = "btnStrAdd";
            this.btnStrAdd.Size = new System.Drawing.Size(79, 22);
            this.btnStrAdd.TabIndex = 4;
            this.btnStrAdd.Text = "&Add Value";
            this.btnStrAdd.Click += new System.EventHandler(this.btnStrAdd_Click);
            // 
            // btnStrDelete
            // 
            this.btnStrDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStrDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrDelete.Location = new System.Drawing.Point(93, 55);
            this.btnStrDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrDelete.Name = "btnStrDelete";
            this.btnStrDelete.Size = new System.Drawing.Size(79, 22);
            this.btnStrDelete.TabIndex = 5;
            this.btnStrDelete.Text = "De&lete Value";
            this.btnStrDelete.Click += new System.EventHandler(this.btnStrDelete_Click);
            // 
            // cbFlag
            // 
            this.cbFlag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFlag.AutoSize = true;
            this.cbFlag.BackColor = System.Drawing.Color.Transparent;
            this.cbFlag.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbFlag.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbFlag.Location = new System.Drawing.Point(583, 34);
            this.cbFlag.Margin = new System.Windows.Forms.Padding(2);
            this.cbFlag.Name = "cbFlag";
            this.cbFlag.Size = new System.Drawing.Size(46, 17);
            this.cbFlag.TabIndex = 8;
            this.cbFlag.Text = "Flag";
            this.cbFlag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbFlag.UseVisualStyleBackColor = false;
            this.cbFlag.CheckedChanged += new System.EventHandler(this.cbFlag_CheckedChanged);
            // 
            // btnStrNext
            // 
            this.btnStrNext.BackColor = System.Drawing.Color.Transparent;
            this.btnStrNext.Font = new System.Drawing.Font("Wingdings", 8.25F);
            this.btnStrNext.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrNext.Location = new System.Drawing.Point(94, 122);
            this.btnStrNext.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrNext.Name = "btnStrNext";
            this.btnStrNext.Size = new System.Drawing.Size(17, 17);
            this.btnStrNext.TabIndex = 11;
            this.btnStrNext.TabStop = false;
            this.btnStrNext.Text = "â         &Down";
            this.btnStrNext.UseVisualStyleBackColor = false;
            this.btnStrNext.Click += new System.EventHandler(this.btnStrNext_Click);
            // 
            // btnStrPrev
            // 
            this.btnStrPrev.BackColor = System.Drawing.Color.Transparent;
            this.btnStrPrev.Font = new System.Drawing.Font("Wingdings", 8.25F);
            this.btnStrPrev.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrPrev.Location = new System.Drawing.Point(94, 100);
            this.btnStrPrev.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrPrev.Name = "btnStrPrev";
            this.btnStrPrev.Size = new System.Drawing.Size(17, 17);
            this.btnStrPrev.TabIndex = 10;
            this.btnStrPrev.TabStop = false;
            this.btnStrPrev.Text = "á         &Up";
            this.btnStrPrev.UseVisualStyleBackColor = false;
            this.btnStrPrev.Click += new System.EventHandler(this.btnStrPrev_Click);
            // 
            // pjse_banner1
            // 
            this.pjse_banner1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pjse_banner1.Location = new System.Drawing.Point(0, 0);
            this.pjse_banner1.Margin = new System.Windows.Forms.Padding(2);
            this.pjse_banner1.Name = "pjse_banner1";
            this.pjse_banner1.SiblingText = "TRCN";
            this.pjse_banner1.SiblingVisible = true;
            this.pjse_banner1.Size = new System.Drawing.Size(731, 27);
            this.pjse_banner1.TabIndex = 12;
            this.pjse_banner1.TitleText = "Behaviour Constant";
            this.pjse_banner1.TreeText = "Comments";
            this.pjse_banner1.SiblingClick += new System.EventHandler(this.pjse_banner1_SiblingClick);
            // 
            // btnUpdateBCON
            // 
            this.btnUpdateBCON.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnUpdateBCON.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnUpdateBCON.Location = new System.Drawing.Point(93, 173);
            this.btnUpdateBCON.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdateBCON.Name = "btnUpdateBCON";
            this.btnUpdateBCON.Size = new System.Drawing.Size(79, 22);
            this.btnUpdateBCON.TabIndex = 13;
            this.btnUpdateBCON.Text = "Update";
            this.btnUpdateBCON.Click += new System.EventHandler(this.btnUpdateBCON_Click);
            // 
            // llIsOverride
            // 
            this.llIsOverride.AutoSize = true;
            this.llIsOverride.BackColor = System.Drawing.Color.Transparent;
            this.llIsOverride.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.llIsOverride.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llIsOverride.LinkArea = new System.Windows.Forms.LinkArea(21, 14);
            this.llIsOverride.Location = new System.Drawing.Point(3, 197);
            this.llIsOverride.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.llIsOverride.Name = "llIsOverride";
            this.llIsOverride.Size = new System.Drawing.Size(125, 35);
            this.llIsOverride.TabIndex = 20;
            this.llIsOverride.TabStop = true;
            this.llIsOverride.Text = "This is an override.\r\nView original.";
            this.llIsOverride.UseCompatibleTextRendering = true;
            this.llIsOverride.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llIsOverride_LinkClicked);
            // 
            // cmpBCON
            // 
            this.cmpBCON.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmpBCON.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmpBCON.Location = new System.Drawing.Point(5, 173);
            this.cmpBCON.Name = "cmpBCON";
            this.cmpBCON.Size = new System.Drawing.Size(79, 22);
            this.cmpBCON.TabIndex = 1;
            this.cmpBCON.Text = "Compare";
            this.cmpBCON.UseVisualStyleBackColor = true;
            this.cmpBCON.Wrapper = null;
            this.cmpBCON.WrapperName = null;
            this.cmpBCON.CompareWith += new pjse.CompareButton.CompareWithEventHandler(this.cmpBCON_CompareWith);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.AutoSize = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(11, 283);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbusedec
            // 
            this.cbusedec.AutoSize = true;
            this.cbusedec.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbusedec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cbusedec.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbusedec.Location = new System.Drawing.Point(29, 235);
            this.cbusedec.Name = "cbusedec";
            this.cbusedec.Size = new System.Drawing.Size(143, 20);
            this.cbusedec.TabIndex = 21;
            this.cbusedec.Text = "Show all in Decimal";
            this.cbusedec.UseVisualStyleBackColor = true;
            this.cbusedec.CheckedChanged += new System.EventHandler(this.cbusedec_CheckedChanged);
            // 
            // bconPanel
            // 
            this.bconPanel.AutoScroll = true;
            this.bconPanel.Controls.Add(this.tbValueGui);
            this.bconPanel.Controls.Add(this.label1);
            this.bconPanel.Controls.Add(this.cbusedec);
            this.bconPanel.Controls.Add(this.btnClose);
            this.bconPanel.Controls.Add(this.cmpBCON);
            this.bconPanel.Controls.Add(this.llIsOverride);
            this.bconPanel.Controls.Add(this.btnUpdateBCON);
            this.bconPanel.Controls.Add(this.pjse_banner1);
            this.bconPanel.Controls.Add(this.btnStrPrev);
            this.bconPanel.Controls.Add(this.btnStrNext);
            this.bconPanel.Controls.Add(this.cbFlag);
            this.bconPanel.Controls.Add(this.btnStrDelete);
            this.bconPanel.Controls.Add(this.btnStrAdd);
            this.bconPanel.Controls.Add(this.lvConstants);
            this.bconPanel.Controls.Add(this.btnCommit);
            this.bconPanel.Controls.Add(this.lbFilename);
            this.bconPanel.Controls.Add(this.tbFilename);
            this.bconPanel.Controls.Add(this.gbValue);
            this.bconPanel.Controls.Add(this.btnTRCNMaker);
            this.bconPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bconPanel.Location = new System.Drawing.Point(0, 0);
            this.bconPanel.Margin = new System.Windows.Forms.Padding(2);
            this.bconPanel.Name = "bconPanel";
            this.bconPanel.Size = new System.Drawing.Size(731, 317);
            this.bconPanel.TabIndex = 0;
            // 
            // tbValueGui
            // 
            this.tbValueGui.Location = new System.Drawing.Point(39, 259);
            this.tbValueGui.Margin = new System.Windows.Forms.Padding(2);
            this.tbValueGui.Name = "tbValueGui";
            this.tbValueGui.ReadOnly = true;
            this.tbValueGui.Size = new System.Drawing.Size(72, 20);
            this.tbValueGui.TabIndex = 23;
            this.tbValueGui.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(2, 263);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "GUID";
            // 
            // ttBconForm
            // 
            this.ttBconForm.AutomaticDelay = 200;
            this.ttBconForm.AutoPopDelay = 6000;
            this.ttBconForm.InitialDelay = 200;
            this.ttBconForm.ReshowDelay = 40;
            // 
            // BconForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(731, 317);
            this.Controls.Add(this.bconPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BconForm";
            this.Text = "BconForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.gbValue.ResumeLayout(false);
            this.gbValue.PerformLayout();
            this.bconPanel.ResumeLayout(false);
            this.bconPanel.PerformLayout();
            this.ResumeLayout(false);
		}

		#endregion

		private void lvConstants_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			setIndex((this.lvConstants.SelectedIndices.Count > 0) ? this.lvConstants.SelectedIndices[0] : -1);
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


        private void pjse_banner1_SiblingClick(object sender, EventArgs e)
        {
            Trcn trcn = (Trcn)wrapper.SiblingResource(Trcn.Trcntype);
            if (trcn == null) return;
            if (trcn.Package != wrapper.Package)
            {
                DialogResult dr = MessageBox.Show(Localization.GetString("OpenOtherPkg"), pjse_banner1.TitleText, MessageBoxButtons.YesNo);
                if (dr != DialogResult.Yes) return;
            }
            SimPe.RemoteControl.OpenPackedFile(trcn.FileDescriptor, trcn.Package);
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


        private void cmpBCON_CompareWith(object sender, pjse.CompareButton.CompareWithEventArgs e)
        {
            common_Popup(e.Item, e.ExpansionItem, true);
        }

        private void llIsOverride_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            common_Popup((pjse.FileTable.Entry)((LinkLabel)sender).Tag, null, false);
        }


        private void btnUpdateBCON_Click(object sender, EventArgs e)
        {
            doUpdateBCON();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.isPopup)
                Close();
        }

        private void cbusedec_CheckedChanged(object sender, EventArgs e)
        {
            updateLists();
            displayBconItem();
        }
	}
}
