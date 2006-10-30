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
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper;
using pjse;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for BconForm.
	/// </summary>
	public class TtabForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables

		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Panel ttabPanel;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tpSettings;
		private System.Windows.Forms.Label lbaction;
		private System.Windows.Forms.Label lbguard;
		private System.Windows.Forms.Label label40;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox tbGuardian;
		private System.Windows.Forms.CheckBox cbBitE;
		private System.Windows.Forms.CheckBox cbBitF;
		private System.Windows.Forms.CheckBox cbBitC;
		private System.Windows.Forms.CheckBox cbBitD;
		private System.Windows.Forms.CheckBox cbBitB;
		private System.Windows.Forms.CheckBox cbBitA;
		private System.Windows.Forms.CheckBox cbBit9;
		private System.Windows.Forms.CheckBox cbBit8;
		private System.Windows.Forms.CheckBox cbBit7;
		private System.Windows.Forms.CheckBox cbBit6;
		private System.Windows.Forms.CheckBox cbBit5;
		private System.Windows.Forms.CheckBox cbBit4;
		private System.Windows.Forms.CheckBox cbBit3;
		private System.Windows.Forms.CheckBox cbBit2;
		private System.Windows.Forms.CheckBox cbBit1;
		private System.Windows.Forms.TabPage tpHumanMotives;
		private System.Windows.Forms.CheckBox cbBit0;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.TextBox tbAction;
		private System.Windows.Forms.TextBox tbFlags2;
		private System.Windows.Forms.TextBox tbStringIndex;
		private System.Windows.Forms.GroupBox gbFlags;
		private System.Windows.Forms.TextBox tbFlags;
		private System.Windows.Forms.TextBox tbAttenuationValue;
		private System.Windows.Forms.TextBox tbAutonomy;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbJoinIndex;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnGuardian;
        private System.Windows.Forms.Button btnAction;
		private System.Windows.Forms.ComboBox cbAttenuationCode;
		private System.Windows.Forms.ListBox lbttab;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Label lbFilename;
		private System.Windows.Forms.TextBox tbFilename;
		private System.Windows.Forms.TextBox tbFormat;
		private System.Windows.Forms.Label label41;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.Button btnAppend;
		private System.Windows.Forms.TextBox tbUIDispType;
		private System.Windows.Forms.TextBox tbFaceAnimID;
		private System.Windows.Forms.TextBox tbMemIterMult;
		private System.Windows.Forms.TextBox tbObjType;
		private System.Windows.Forms.TextBox tbModelTabID;
		private System.Windows.Forms.ComboBox cbStringIndex;
		private System.Windows.Forms.LinkLabel llAction;
		private System.Windows.Forms.LinkLabel llGuardian;
		private System.Windows.Forms.Button btnNoFlags;
		private System.Windows.Forms.Button btnHelp;
        private Button btnRefreshFT;
        private Button btnStrPrev;
        private Button btnStrNext;
        private TabPage tpAnimalMotives;
        private TtabItemMotiveTableUI timtuiHuman;
        private TtabItemMotiveTableUI timtuiAnimal;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
       
		public TtabForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			TextBox[] tbua = { tbAction, tbGuardian, tbFlags, tbFlags2, tbUIDispType };
			alHex16 = new ArrayList(tbua);

			TextBox[] tbia = { tbFormat, tbStringIndex, tbAutonomy, tbFaceAnimID, tbObjType, tbModelTabID, tbJoinIndex };
			alHex32 = new ArrayList(tbia);

			TextBox[] tbfa = { tbAttenuationValue, tbMemIterMult };
			alFloats = new ArrayList(tbfa);

			CheckBox[] cba = {
							    cbBit0 ,cbBit1 ,cbBit2 ,cbBit3
							   ,cbBit4 ,cbBit5 ,cbBit6 ,cbBit7
							   ,cbBit8 ,cbBit9 ,cbBitA ,cbBitB
							   ,cbBitC ,cbBitD ,cbBitE ,cbBitF
						   };
			alFlags = new ArrayList(cba);

			ComboBox[] cbb = { cbStringIndex ,cbAttenuationCode };
			alHex32cb = new ArrayList(cbb);

            this.label40.Left = this.tbStringIndex.Left - this.label40.Width - 6;
            this.llAction.Left = this.tbStringIndex.Left - this.llAction.Width - 6;
            this.llGuardian.Left = this.tbStringIndex.Left - this.llGuardian.Width - 6;

            Label[] al = { label32, label31, label1, label35, label20, label30, label2, label29, label34, label33 };
            foreach (Label l in al)
                l.Left = cbAttenuationCode.Left - l.Width - 6;


#if !(INPROGRESS || DEBUG)
			this.btnAppend.Visible = false;
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
            if (setHandler)
            {
                wrapper.WrapperChanged -= new System.EventHandler(this.WrapperChanged);
                pjse.FileTable.GFT.FiletableRefresh -= new EventHandler(GFT_FiletableRefresh);
                setHandler = false;
            }
		}

		
		#region TtabForm
		/// <summary>
		/// Stores the currently active Wrapper
		/// </summary>
		private Ttab wrapper = null;
		private bool internalchg;
		private bool setHandler = false;
		private ArrayList alHex16;
		private ArrayList alHex32;
		private ArrayList alFloats;
		private ArrayList alFlags;
		private ArrayList alHex32cb;
		private TtabItem origItem;
		private TtabItem currentItem;

		private bool cbHex32_IsValid(object sender)
		{
			if (alHex32cb.IndexOf(sender) < 0)
				throw new Exception("cbHex32_IsValid not applicable to control " + sender.ToString());
			if (((ComboBox)sender).FindStringExact(((ComboBox)sender).Text) >= 0) return true;

			try { Convert.ToUInt32(((ComboBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private bool hex16_IsValid(object sender)
		{
			if (alHex16.IndexOf(sender) < 0)
				throw new Exception("hex16_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToUInt16(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private bool hex32_IsValid(object sender)
		{
			if (alHex32.IndexOf(sender) < 0)
				throw new Exception("hex32_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToUInt32(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private bool float_IsValid(object sender)
		{
			if (alFloats.IndexOf(sender) < 0)
				throw new Exception("float_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToSingle(((TextBox)sender).Text); }
			catch (Exception) { return false; }
			return true;
		}


		public void Append(pjse.FileTable.Entry e)
		{
			if (e == null || !(e.Wrapper is Ttab)) return;

			bool savedstate = internalchg;
			internalchg = true;

			ttabPanel.Parent.Cursor = Cursors.WaitCursor;

			Ttab b = (Ttab)e.Wrapper;
			uint offset = getTTAsCount();
			for (int bi = 0; bi < b.Count; bi++)
			{
				int i = wrapper.Add(b[bi]);
				if (i < 0) break;
				wrapper[i].StringIndex += offset;
				lbttab.Items.Add(wrapper[i]);
			}
			ttabPanel.Parent.Cursor = Cursors.Default;

			internalchg = savedstate;
		}

        private Str str = null;
        private Str StrRes
        {
            get
            {
                if (str == null)
                    str = new Str(wrapper, wrapper.FileDescriptor.Instance, 0x54544173);
                return str;
            }
        }

        private uint previousFormat;


        private uint getTTAsCount()
		{
            Str w = StrRes;
            if (w == null) return 0;

            uint max = 0;
            for (byte lid = 1; lid < 44; lid++) max = (uint)Math.Max(max, w[lid].Length);
            return max;
        }

        private void populateCbStringIndex()
        {
            bool prev = internalchg;
            internalchg = true;

            int cbStringIndexSelectedIndex = this.cbStringIndex.SelectedIndex;

            this.cbStringIndex.Items.Clear();

            uint c = getTTAsCount();
            Str w = StrRes;
            for (int i = 0; i < c; i++)
			{
                FallbackStrItem si = w[1, i];
				this.cbStringIndex.Items.Add("0x" + i.ToString("X") + ": " + ((si == null)
                    ? "*!no default string!*"
                    : si.strItem.Title + (si.lidFallback ? " [LID=1]" : "") + (si.fallback.Count > 0 ? " [*]" :"")));
			}

            if (cbStringIndexSelectedIndex < this.cbStringIndex.Items.Count)
                this.cbStringIndex.SelectedIndex = cbStringIndexSelectedIndex;
            else
                this.cbStringIndex.SelectedIndex = -1;

            internalchg = prev;
        }

        private void populateLbttab()
        {
            bool prev = internalchg;
            internalchg = true;

            int lbttabSelectedIndex = this.lbttab.SelectedIndex;

            lbttab.Items.Clear();
            for (int i = 0; i < wrapper.Count; i++) addItem(i);

            if (lbttabSelectedIndex >= 0)
            {
                if (lbttabSelectedIndex < lbttab.Items.Count)
                    this.lbttab.SelectedIndex = lbttabSelectedIndex;
                else
                    this.lbttab.SelectedIndex = lbttab.Items.Count - 1;
            }

            internalchg = false;
            TtabSelect(null, null);

            internalchg = prev;
        }

        private void doFlags()
        {
            internalchg = true;
            Boolset flags = new Boolset(currentItem.Flags);
            for (int i = 0; i < alFlags.Count; i++)
            {
                bool invert = wrapper.Format < 0x54 && (i == 4 || i == 5 || i == 6);
                ((CheckBox)alFlags[i]).Checked = invert ? !flags[i] : flags[i];
            }
            internalchg = false;
        }

        private void setFormat()
        {
            if (previousFormat >= 0x44 && wrapper.Format < 0x44)
            {
                DialogResult dr = MessageBox.Show(pjse.Localization.GetString("ttabForm_Sure"),
                    pjse.Localization.GetString("ttabForm_Single"),
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (!DialogResult.OK.Equals(dr))
                    wrapper.Format = previousFormat;
            }
            else if (previousFormat < 0x44 && wrapper.Format >= 0x44 && wrapper.Format < 0x54)
            {
                DialogResult dr = MessageBox.Show(pjse.Localization.GetString("ttabForm_Sure"),
                    pjse.Localization.GetString("ttabForm_MultipleFixed"),
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (!DialogResult.OK.Equals(dr))
                    wrapper.Format = previousFormat;
            }
            else if (previousFormat < 0x54 && wrapper.Format >= 0x54)
            {
                DialogResult dr = MessageBox.Show(pjse.Localization.GetString("ttabForm_Sure"),
                    pjse.Localization.GetString("ttabForm_MultipleVaries"),
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (!DialogResult.OK.Equals(dr))
                    wrapper.Format = previousFormat;
            }

            previousFormat = wrapper.Format;


            this.tbUIDispType.Enabled = this.tbFaceAnimID.Enabled =
                this.tbModelTabID.Enabled = this.tbMemIterMult.Enabled = this.tbObjType.Enabled = false;


            this.tabControl1.TabPages.Remove(this.tpAnimalMotives);
            this.tpHumanMotives.Text = ((String)this.tpHumanMotives.Tag).Split('/')[0];

            if (wrapper.Format >= 0x45)
            {
                this.tbUIDispType.Enabled = true;
                if (wrapper.Format >= 0x46)
                {
                    this.tbModelTabID.Enabled = true;
                    if (wrapper.Format >= 0x4a)
                    {
                        this.tbFaceAnimID.Enabled = true;
                        if (wrapper.Format >= 0x4c)
                        {
                            this.tbMemIterMult.Enabled = this.tbObjType.Enabled = true;
                            if (wrapper.Format >= 0x54)
                            {
                                this.tpHumanMotives.Text = ((String)this.tpHumanMotives.Tag).Split('/')[1];
                                this.tabControl1.TabPages.Add(this.tpAnimalMotives);
                                this.cbBit1.Text = ((String)this.cbBit1.Tag).Split('/')[1];
                                this.cbBit3.Text = ((String)this.cbBit3.Tag).Split('/')[1];
                                this.cbBit5.Text = ((String)this.cbBit5.Tag).Split('/')[1];
                                this.cbBit8.Text = ((String)this.cbBit8.Tag).Split('/')[1];
                            }
                            else
                            {
                                this.cbBit1.Text = ((String)this.cbBit1.Tag).Split('/')[0];
                                this.cbBit3.Text = ((String)this.cbBit3.Tag).Split('/')[0];
                                this.cbBit5.Text = ((String)this.cbBit5.Tag).Split('/')[0];
                                this.cbBit8.Text = ((String)this.cbBit8.Tag).Split('/')[0];
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Add the ith TtabItem to the lbttab listbox
        /// </summary>
        /// <param name="i">index of TtabItem to add</param>
        private void addItem(int i)
        {
            if (wrapper[i] != null && wrapper[i].StringIndex < cbStringIndex.Items.Count)
                lbttab.Items.Add(cbStringIndex.Items[(int)wrapper[i].StringIndex]);
            else
                lbttab.Items.Add("0x" + i.ToString("X") + ": " + pjse.Localization.GetString("unk"));
        }

		private void setBHAV(int which, ushort target, bool notxt)
		{
			TextBox[] tbaGA = { tbAction, tbGuardian };
			if (!notxt) tbaGA[which].Text = "0x"+Helper.HexString(target);

			bool found = false;
            Label[] lbaGA = { lbaction, lbguard };
            lbaGA[which].Text = pjse.BhavWiz.bhavName(wrapper, target, ref found);

            LinkLabel[] llaGA = { llAction, llGuardian };
            llaGA[which].Enabled = found;
		}

		private void setStringIndex(uint si, bool doText, bool doCB)
		{
			if (doText) tbStringIndex.Text = "0x"+Helper.HexString(si);
			if (doCB)
			{
                if (si < cbStringIndex.Items.Count)
					this.cbStringIndex.SelectedIndex = (int)si;
				else
				{
					this.cbStringIndex.SelectedIndex = -1;
					this.cbStringIndex.Text = tbStringIndex.Text;
				}
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
				return ttabPanel;
			}
		}

		/// <summary>
		/// Called by the AbstractWrapper when the file should be displayed to the user.
		/// </summary>
		/// <param name="wrp">Reference to the wrapper to be displayed.</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (Ttab) wrp;

            // We don't repopulate cbStringIndex on WrapperChanged
            this.cbStringIndex.SelectedIndex = -1;
            populateCbStringIndex();

            // Avoid warning popups from setFormat()!
            previousFormat = wrapper.Format;
            // WrapperChanged() calls populateLbttab(), so set lbttab.SelectedIndex to -1
            this.lbttab.SelectedIndex = -1;
            WrapperChanged(wrapper, null);

            // Now call TtabSelect (one way or another)
            if (this.lbttab.Items.Count > 0) this.lbttab.SelectedIndex = 0;
            else TtabSelect(null, null);

			if (!setHandler)
			{
				wrapper.WrapperChanged += new System.EventHandler(this.WrapperChanged);
                pjse.FileTable.GFT.FiletableRefresh += new EventHandler(GFT_FiletableRefresh);
				setHandler = true;
			}
		}

        private void GFT_FiletableRefresh(object sender, EventArgs e)
        {
            str = null;
            if (wrapper == null || wrapper.FileDescriptor == null) return;

            populateCbStringIndex();
            populateLbttab();
        }		

		private void WrapperChanged(object sender, System.EventArgs e)
		{
			this.btnCommit.Enabled = wrapper.Changed;

            if (internalchg) return;
            internalchg = true;

            if (sender == wrapper)
            {
                this.Text = tbFilename.Text = wrapper.FileName;
                tbFormat.Text = "0x" + Helper.HexString(wrapper.Format);
                setFormat();
                populateLbttab();
            }
            else if (lbttab.SelectedIndex >= 0 && sender == wrapper[lbttab.SelectedIndex])
                TtabSelect(null, null);

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TtabForm));
            this.ttabPanel = new System.Windows.Forms.Panel();
            this.btnStrPrev = new System.Windows.Forms.Button();
            this.btnStrNext = new System.Windows.Forms.Button();
            this.btnAppend = new System.Windows.Forms.Button();
            this.lbFilename = new System.Windows.Forms.Label();
            this.tbFilename = new System.Windows.Forms.TextBox();
            this.tbFormat = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.btnCommit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lbttab = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.llGuardian = new System.Windows.Forms.LinkLabel();
            this.llAction = new System.Windows.Forms.LinkLabel();
            this.cbStringIndex = new System.Windows.Forms.ComboBox();
            this.cbAttenuationCode = new System.Windows.Forms.ComboBox();
            this.btnAction = new System.Windows.Forms.Button();
            this.btnGuardian = new System.Windows.Forms.Button();
            this.lbaction = new System.Windows.Forms.Label();
            this.lbguard = new System.Windows.Forms.Label();
            this.tbStringIndex = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.tbModelTabID = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.tbObjType = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.tbUIDispType = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.tbAutonomy = new System.Windows.Forms.TextBox();
            this.tbMemIterMult = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.tbFaceAnimID = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.tbAttenuationValue = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.tbFlags2 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbGuardian = new System.Windows.Forms.TextBox();
            this.gbFlags = new System.Windows.Forms.GroupBox();
            this.btnNoFlags = new System.Windows.Forms.Button();
            this.tbFlags = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.cbBit0 = new System.Windows.Forms.CheckBox();
            this.cbBitE = new System.Windows.Forms.CheckBox();
            this.cbBitF = new System.Windows.Forms.CheckBox();
            this.cbBitC = new System.Windows.Forms.CheckBox();
            this.cbBitD = new System.Windows.Forms.CheckBox();
            this.cbBitB = new System.Windows.Forms.CheckBox();
            this.cbBitA = new System.Windows.Forms.CheckBox();
            this.cbBit9 = new System.Windows.Forms.CheckBox();
            this.cbBit8 = new System.Windows.Forms.CheckBox();
            this.cbBit7 = new System.Windows.Forms.CheckBox();
            this.cbBit6 = new System.Windows.Forms.CheckBox();
            this.cbBit5 = new System.Windows.Forms.CheckBox();
            this.cbBit4 = new System.Windows.Forms.CheckBox();
            this.cbBit3 = new System.Windows.Forms.CheckBox();
            this.cbBit2 = new System.Windows.Forms.CheckBox();
            this.cbBit1 = new System.Windows.Forms.CheckBox();
            this.tbAction = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbJoinIndex = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tpHumanMotives = new System.Windows.Forms.TabPage();
            this.tpAnimalMotives = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnRefreshFT = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.timtuiHuman = new SimPe.PackedFiles.UserInterface.TtabItemMotiveTableUI();
            this.timtuiAnimal = new SimPe.PackedFiles.UserInterface.TtabItemMotiveTableUI();
            this.ttabPanel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpSettings.SuspendLayout();
            this.gbFlags.SuspendLayout();
            this.tpHumanMotives.SuspendLayout();
            this.tpAnimalMotives.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // ttabPanel
            // 
            resources.ApplyResources(this.ttabPanel, "ttabPanel");
            this.ttabPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ttabPanel.Controls.Add(this.btnStrPrev);
            this.ttabPanel.Controls.Add(this.btnStrNext);
            this.ttabPanel.Controls.Add(this.btnAppend);
            this.ttabPanel.Controls.Add(this.lbFilename);
            this.ttabPanel.Controls.Add(this.tbFilename);
            this.ttabPanel.Controls.Add(this.tbFormat);
            this.ttabPanel.Controls.Add(this.label41);
            this.ttabPanel.Controls.Add(this.btnCommit);
            this.ttabPanel.Controls.Add(this.btnAdd);
            this.ttabPanel.Controls.Add(this.label26);
            this.ttabPanel.Controls.Add(this.btnDelete);
            this.ttabPanel.Controls.Add(this.lbttab);
            this.ttabPanel.Controls.Add(this.tabControl1);
            this.ttabPanel.Controls.Add(this.panel5);
            this.ttabPanel.Name = "ttabPanel";
            // 
            // btnStrPrev
            // 
            resources.ApplyResources(this.btnStrPrev, "btnStrPrev");
            this.btnStrPrev.Name = "btnStrPrev";
            this.btnStrPrev.Click += new System.EventHandler(this.btnStrPrev_Click);
            // 
            // btnStrNext
            // 
            resources.ApplyResources(this.btnStrNext, "btnStrNext");
            this.btnStrNext.Name = "btnStrNext";
            this.btnStrNext.Click += new System.EventHandler(this.btnStrNext_Click);
            // 
            // btnAppend
            // 
            resources.ApplyResources(this.btnAppend, "btnAppend");
            this.btnAppend.Name = "btnAppend";
            this.btnAppend.Click += new System.EventHandler(this.btnAppend_Click);
            // 
            // lbFilename
            // 
            resources.ApplyResources(this.lbFilename, "lbFilename");
            this.lbFilename.Name = "lbFilename";
            // 
            // tbFilename
            // 
            resources.ApplyResources(this.tbFilename, "tbFilename");
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.Validated += new System.EventHandler(this.tbFilename_Validated);
            this.tbFilename.TextChanged += new System.EventHandler(this.tbFilename_TextChanged);
            // 
            // tbFormat
            // 
            resources.ApplyResources(this.tbFormat, "tbFormat");
            this.tbFormat.Name = "tbFormat";
            this.tbFormat.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbFormat.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            this.tbFormat.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            // 
            // label41
            // 
            resources.ApplyResources(this.label41, "label41");
            this.label41.Name = "label41";
            // 
            // btnCommit
            // 
            resources.ApplyResources(this.btnCommit, "btnCommit");
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lbttab
            // 
            resources.ApplyResources(this.lbttab, "lbttab");
            this.lbttab.Name = "lbttab";
            this.lbttab.SelectedIndexChanged += new System.EventHandler(this.TtabSelect);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tpSettings);
            this.tabControl1.Controls.Add(this.tpHumanMotives);
            this.tabControl1.Controls.Add(this.tpAnimalMotives);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tpSettings
            // 
            resources.ApplyResources(this.tpSettings, "tpSettings");
            this.tpSettings.Controls.Add(this.llGuardian);
            this.tpSettings.Controls.Add(this.llAction);
            this.tpSettings.Controls.Add(this.cbStringIndex);
            this.tpSettings.Controls.Add(this.cbAttenuationCode);
            this.tpSettings.Controls.Add(this.btnAction);
            this.tpSettings.Controls.Add(this.btnGuardian);
            this.tpSettings.Controls.Add(this.lbaction);
            this.tpSettings.Controls.Add(this.lbguard);
            this.tpSettings.Controls.Add(this.tbStringIndex);
            this.tpSettings.Controls.Add(this.label40);
            this.tpSettings.Controls.Add(this.tbModelTabID);
            this.tpSettings.Controls.Add(this.label33);
            this.tpSettings.Controls.Add(this.tbObjType);
            this.tpSettings.Controls.Add(this.label34);
            this.tpSettings.Controls.Add(this.tbUIDispType);
            this.tpSettings.Controls.Add(this.label35);
            this.tpSettings.Controls.Add(this.tbAutonomy);
            this.tpSettings.Controls.Add(this.tbMemIterMult);
            this.tpSettings.Controls.Add(this.label29);
            this.tpSettings.Controls.Add(this.tbFaceAnimID);
            this.tpSettings.Controls.Add(this.label30);
            this.tpSettings.Controls.Add(this.tbAttenuationValue);
            this.tpSettings.Controls.Add(this.label31);
            this.tpSettings.Controls.Add(this.label32);
            this.tpSettings.Controls.Add(this.tbFlags2);
            this.tpSettings.Controls.Add(this.label20);
            this.tpSettings.Controls.Add(this.tbGuardian);
            this.tpSettings.Controls.Add(this.gbFlags);
            this.tpSettings.Controls.Add(this.tbAction);
            this.tpSettings.Controls.Add(this.label1);
            this.tpSettings.Controls.Add(this.tbJoinIndex);
            this.tpSettings.Controls.Add(this.label2);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // llGuardian
            // 
            resources.ApplyResources(this.llGuardian, "llGuardian");
            this.llGuardian.Name = "llGuardian";
            this.llGuardian.TabStop = true;
            this.llGuardian.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llBhav_LinkClicked);
            // 
            // llAction
            // 
            resources.ApplyResources(this.llAction, "llAction");
            this.llAction.Name = "llAction";
            this.llAction.TabStop = true;
            this.llAction.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llBhav_LinkClicked);
            // 
            // cbStringIndex
            // 
            this.cbStringIndex.DisplayMember = "Display";
            this.cbStringIndex.DropDownWidth = 240;
            resources.ApplyResources(this.cbStringIndex, "cbStringIndex");
            this.cbStringIndex.Items.AddRange(new object[] {
            resources.GetString("cbStringIndex.Items"),
            resources.GetString("cbStringIndex.Items1"),
            resources.GetString("cbStringIndex.Items2")});
            this.cbStringIndex.Name = "cbStringIndex";
            this.cbStringIndex.TabStop = false;
            this.cbStringIndex.ValueMember = "Value";
            this.cbStringIndex.Validating += new System.ComponentModel.CancelEventHandler(this.cbHex32_Validating);
            this.cbStringIndex.Validated += new System.EventHandler(this.cbHex32_Validated);
            this.cbStringIndex.Enter += new System.EventHandler(this.cbHex32_Enter);
            this.cbStringIndex.SelectedIndexChanged += new System.EventHandler(this.cbHex32_SelectedIndexChanged);
            this.cbStringIndex.TextChanged += new System.EventHandler(this.cbHex32_TextChanged);
            // 
            // cbAttenuationCode
            // 
            resources.ApplyResources(this.cbAttenuationCode, "cbAttenuationCode");
            this.cbAttenuationCode.Items.AddRange(new object[] {
            resources.GetString("cbAttenuationCode.Items"),
            resources.GetString("cbAttenuationCode.Items1"),
            resources.GetString("cbAttenuationCode.Items2"),
            resources.GetString("cbAttenuationCode.Items3"),
            resources.GetString("cbAttenuationCode.Items4")});
            this.cbAttenuationCode.Name = "cbAttenuationCode";
            this.cbAttenuationCode.Validating += new System.ComponentModel.CancelEventHandler(this.cbHex32_Validating);
            this.cbAttenuationCode.Validated += new System.EventHandler(this.cbHex32_Validated);
            this.cbAttenuationCode.Enter += new System.EventHandler(this.cbHex32_Enter);
            this.cbAttenuationCode.SelectedIndexChanged += new System.EventHandler(this.cbHex32_SelectedIndexChanged);
            this.cbAttenuationCode.TextChanged += new System.EventHandler(this.cbHex32_TextChanged);
            // 
            // btnAction
            // 
            resources.ApplyResources(this.btnAction, "btnAction");
            this.btnAction.Name = "btnAction";
            this.btnAction.Click += new System.EventHandler(this.GetTTABAction);
            // 
            // btnGuardian
            // 
            resources.ApplyResources(this.btnGuardian, "btnGuardian");
            this.btnGuardian.Name = "btnGuardian";
            this.btnGuardian.Click += new System.EventHandler(this.GetTTABGuard);
            // 
            // lbaction
            // 
            resources.ApplyResources(this.lbaction, "lbaction");
            this.lbaction.Name = "lbaction";
            this.lbaction.UseMnemonic = false;
            // 
            // lbguard
            // 
            resources.ApplyResources(this.lbguard, "lbguard");
            this.lbguard.Name = "lbguard";
            this.lbguard.UseMnemonic = false;
            // 
            // tbStringIndex
            // 
            resources.ApplyResources(this.tbStringIndex, "tbStringIndex");
            this.tbStringIndex.Name = "tbStringIndex";
            this.tbStringIndex.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbStringIndex.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            this.tbStringIndex.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            // 
            // label40
            // 
            resources.ApplyResources(this.label40, "label40");
            this.label40.Name = "label40";
            // 
            // tbModelTabID
            // 
            resources.ApplyResources(this.tbModelTabID, "tbModelTabID");
            this.tbModelTabID.Name = "tbModelTabID";
            this.tbModelTabID.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbModelTabID.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            this.tbModelTabID.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            // 
            // tbObjType
            // 
            resources.ApplyResources(this.tbObjType, "tbObjType");
            this.tbObjType.Name = "tbObjType";
            this.tbObjType.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbObjType.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            this.tbObjType.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            // 
            // label34
            // 
            resources.ApplyResources(this.label34, "label34");
            this.label34.Name = "label34";
            // 
            // tbUIDispType
            // 
            resources.ApplyResources(this.tbUIDispType, "tbUIDispType");
            this.tbUIDispType.Name = "tbUIDispType";
            this.tbUIDispType.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbUIDispType.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            this.tbUIDispType.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            // 
            // label35
            // 
            resources.ApplyResources(this.label35, "label35");
            this.label35.Name = "label35";
            // 
            // tbAutonomy
            // 
            resources.ApplyResources(this.tbAutonomy, "tbAutonomy");
            this.tbAutonomy.Name = "tbAutonomy";
            this.tbAutonomy.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbAutonomy.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            this.tbAutonomy.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            // 
            // tbMemIterMult
            // 
            resources.ApplyResources(this.tbMemIterMult, "tbMemIterMult");
            this.tbMemIterMult.Name = "tbMemIterMult";
            this.tbMemIterMult.Validated += new System.EventHandler(this.float_Validated);
            this.tbMemIterMult.Validating += new System.ComponentModel.CancelEventHandler(this.float_Validating);
            this.tbMemIterMult.TextChanged += new System.EventHandler(this.float_TextChanged);
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // tbFaceAnimID
            // 
            resources.ApplyResources(this.tbFaceAnimID, "tbFaceAnimID");
            this.tbFaceAnimID.Name = "tbFaceAnimID";
            this.tbFaceAnimID.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbFaceAnimID.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            this.tbFaceAnimID.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.Name = "label30";
            // 
            // tbAttenuationValue
            // 
            resources.ApplyResources(this.tbAttenuationValue, "tbAttenuationValue");
            this.tbAttenuationValue.Name = "tbAttenuationValue";
            this.tbAttenuationValue.Validated += new System.EventHandler(this.float_Validated);
            this.tbAttenuationValue.Validating += new System.ComponentModel.CancelEventHandler(this.float_Validating);
            this.tbAttenuationValue.TextChanged += new System.EventHandler(this.float_TextChanged);
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.Name = "label31";
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.Name = "label32";
            // 
            // tbFlags2
            // 
            resources.ApplyResources(this.tbFlags2, "tbFlags2");
            this.tbFlags2.Name = "tbFlags2";
            this.tbFlags2.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbFlags2.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            this.tbFlags2.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // tbGuardian
            // 
            resources.ApplyResources(this.tbGuardian, "tbGuardian");
            this.tbGuardian.Name = "tbGuardian";
            this.tbGuardian.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbGuardian.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            this.tbGuardian.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            // 
            // gbFlags
            // 
            this.gbFlags.Controls.Add(this.btnNoFlags);
            this.gbFlags.Controls.Add(this.tbFlags);
            this.gbFlags.Controls.Add(this.label24);
            this.gbFlags.Controls.Add(this.cbBit0);
            this.gbFlags.Controls.Add(this.cbBitE);
            this.gbFlags.Controls.Add(this.cbBitF);
            this.gbFlags.Controls.Add(this.cbBitC);
            this.gbFlags.Controls.Add(this.cbBitD);
            this.gbFlags.Controls.Add(this.cbBitB);
            this.gbFlags.Controls.Add(this.cbBitA);
            this.gbFlags.Controls.Add(this.cbBit9);
            this.gbFlags.Controls.Add(this.cbBit8);
            this.gbFlags.Controls.Add(this.cbBit7);
            this.gbFlags.Controls.Add(this.cbBit6);
            this.gbFlags.Controls.Add(this.cbBit5);
            this.gbFlags.Controls.Add(this.cbBit4);
            this.gbFlags.Controls.Add(this.cbBit3);
            this.gbFlags.Controls.Add(this.cbBit2);
            this.gbFlags.Controls.Add(this.cbBit1);
            this.gbFlags.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.gbFlags, "gbFlags");
            this.gbFlags.Name = "gbFlags";
            this.gbFlags.TabStop = false;
            // 
            // btnNoFlags
            // 
            resources.ApplyResources(this.btnNoFlags, "btnNoFlags");
            this.btnNoFlags.Name = "btnNoFlags";
            this.btnNoFlags.Click += new System.EventHandler(this.btnNoFlags_Click);
            // 
            // tbFlags
            // 
            resources.ApplyResources(this.tbFlags, "tbFlags");
            this.tbFlags.Name = "tbFlags";
            this.tbFlags.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbFlags.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            this.tbFlags.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            // 
            // cbBit0
            // 
            resources.ApplyResources(this.cbBit0, "cbBit0");
            this.cbBit0.Name = "cbBit0";
            this.cbBit0.Tag = "";
            this.cbBit0.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBitE
            // 
            resources.ApplyResources(this.cbBitE, "cbBitE");
            this.cbBitE.Name = "cbBitE";
            this.cbBitE.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBitF
            // 
            resources.ApplyResources(this.cbBitF, "cbBitF");
            this.cbBitF.Name = "cbBitF";
            this.cbBitF.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBitC
            // 
            resources.ApplyResources(this.cbBitC, "cbBitC");
            this.cbBitC.Name = "cbBitC";
            this.cbBitC.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBitD
            // 
            resources.ApplyResources(this.cbBitD, "cbBitD");
            this.cbBitD.Name = "cbBitD";
            this.cbBitD.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBitB
            // 
            resources.ApplyResources(this.cbBitB, "cbBitB");
            this.cbBitB.Name = "cbBitB";
            this.cbBitB.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBitA
            // 
            resources.ApplyResources(this.cbBitA, "cbBitA");
            this.cbBitA.Name = "cbBitA";
            this.cbBitA.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBit9
            // 
            resources.ApplyResources(this.cbBit9, "cbBit9");
            this.cbBit9.Name = "cbBit9";
            this.cbBit9.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBit8
            // 
            resources.ApplyResources(this.cbBit8, "cbBit8");
            this.cbBit8.Name = "cbBit8";
            this.cbBit8.Tag = "auto first/unk";
            this.cbBit8.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBit7
            // 
            resources.ApplyResources(this.cbBit7, "cbBit7");
            this.cbBit7.Name = "cbBit7";
            this.cbBit7.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBit6
            // 
            resources.ApplyResources(this.cbBit6, "cbBit6");
            this.cbBit6.Name = "cbBit6";
            this.cbBit6.Tag = "";
            this.cbBit6.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBit5
            // 
            resources.ApplyResources(this.cbBit5, "cbBit5");
            this.cbBit5.Name = "cbBit5";
            this.cbBit5.Tag = "demo child/unk";
            this.cbBit5.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBit4
            // 
            resources.ApplyResources(this.cbBit4, "cbBit4");
            this.cbBit4.Name = "cbBit4";
            this.cbBit4.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBit3
            // 
            resources.ApplyResources(this.cbBit3, "cbBit3");
            this.cbBit3.Name = "cbBit3";
            this.cbBit3.Tag = "consecutive/unk";
            this.cbBit3.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBit2
            // 
            resources.ApplyResources(this.cbBit2, "cbBit2");
            this.cbBit2.Name = "cbBit2";
            this.cbBit2.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBit1
            // 
            resources.ApplyResources(this.cbBit1, "cbBit1");
            this.cbBit1.Name = "cbBit1";
            this.cbBit1.Tag = "joinable/unk";
            this.cbBit1.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // tbAction
            // 
            resources.ApplyResources(this.tbAction, "tbAction");
            this.tbAction.Name = "tbAction";
            this.tbAction.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbAction.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            this.tbAction.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tbJoinIndex
            // 
            resources.ApplyResources(this.tbJoinIndex, "tbJoinIndex");
            this.tbJoinIndex.Name = "tbJoinIndex";
            this.tbJoinIndex.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbJoinIndex.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            this.tbJoinIndex.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tpHumanMotives
            // 
            resources.ApplyResources(this.tpHumanMotives, "tpHumanMotives");
            this.tpHumanMotives.Controls.Add(this.timtuiHuman);
            this.tpHumanMotives.Name = "tpHumanMotives";
            this.tpHumanMotives.Tag = "Motives/Human Motives";
            this.tpHumanMotives.UseVisualStyleBackColor = true;
            // 
            // tpAnimalMotives
            // 
            resources.ApplyResources(this.tpAnimalMotives, "tpAnimalMotives");
            this.tpAnimalMotives.Controls.Add(this.timtuiAnimal);
            this.tpAnimalMotives.Name = "tpAnimalMotives";
            this.tpAnimalMotives.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel5.Controls.Add(this.btnRefreshFT);
            this.panel5.Controls.Add(this.btnHelp);
            this.panel5.Controls.Add(this.label25);
            this.panel5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel5.Name = "panel5";
            // 
            // btnRefreshFT
            // 
            resources.ApplyResources(this.btnRefreshFT, "btnRefreshFT");
            this.btnRefreshFT.Name = "btnRefreshFT";
            this.btnRefreshFT.Click += new System.EventHandler(this.btnRefreshFT_Click);
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            // 
            // timtuiHuman
            // 
            resources.ApplyResources(this.timtuiHuman, "timtuiHuman");
            this.timtuiHuman.MotiveTable = null;
            this.timtuiHuman.Name = "timtuiHuman";
            // 
            // timtuiAnimal
            // 
            resources.ApplyResources(this.timtuiAnimal, "timtuiAnimal");
            this.timtuiAnimal.MotiveTable = null;
            this.timtuiAnimal.Name = "timtuiAnimal";
            // 
            // TtabForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ttabPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "TtabForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ttabPanel.ResumeLayout(false);
            this.ttabPanel.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpSettings.ResumeLayout(false);
            this.tpSettings.PerformLayout();
            this.gbFlags.ResumeLayout(false);
            this.gbFlags.PerformLayout();
            this.tpHumanMotives.ResumeLayout(false);
            this.tpAnimalMotives.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

		}
	
		#endregion


        // -------------- form
        //
        // form
        //
        // --------------

        private void btnRefreshFT_Click(object sender, EventArgs e)
        {
            pjse.FileTable.GFT.UIRefresh();
        }

        private void btnHelp_Click(object sender, System.EventArgs e)
        {
            pjse.HelpHelper.Help("Contents");
        }


        // -------------- wrapper
        //
        // wrapper
        //
        // --------------

        private void btnCommit_Click(object sender, System.EventArgs e)
        {
            try
            {
                wrapper.SynchronizeUserData();
                btnCommit.Enabled = wrapper.Changed;
                //TtabSelect(null, null);
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage(pjse.Localization.GetString("errwritingfile"), ex);
            }
        }

        private void tbFilename_TextChanged(object sender, System.EventArgs e)
        {
            internalchg = true;
            wrapper.FileName = tbFilename.Text;
            internalchg = false;
        }

        private void tbFilename_Validated(object sender, System.EventArgs e)
        {
            tbFilename.SelectAll();
        }

        // Format is a hex32 field, currently handled with ttabItem
        private void doFormat() { }


        // -------------- wrapper[]
        //
        // wrapper[]
        //
        // --------------

        private void btnStrPrev_Click(object sender, EventArgs e)
        {
            lbttab.SelectedIndex--;
        }

        private void btnStrNext_Click(object sender, EventArgs e)
        {
            lbttab.SelectedIndex++;
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            lbttab.SelectedIndex = wrapper.Add((lbttab.SelectedIndex == -1) ? new TtabItem(wrapper) : wrapper[lbttab.SelectedIndex].Clone());
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            wrapper.RemoveAt(lbttab.SelectedIndex);
        }

        private void btnAppend_Click(object sender, System.EventArgs e)
        {
            this.Append((new pjse.ResourceChooser()).Execute(wrapper.FileDescriptor.Type, wrapper.FileDescriptor.Group, ttabPanel));
        }


        // -------------- ttabItem
        //
        // ttabItem
        //
        // --------------

        private void TtabSelect(object sender, System.EventArgs e)
		{
			if (internalchg) return;

            internalchg = true;

            this.ttabPanel.SuspendLayout();
            this.ttabPanel.Cursor = Cursors.AppStarting;


            this.btnStrPrev.Enabled = (lbttab.SelectedIndex > 0);
            this.btnStrNext.Enabled = (lbttab.SelectedIndex < lbttab.Items.Count - 1);

            if (lbttab.SelectedIndex >= 0)
			{
                tabControl1.Enabled = btnDelete.Enabled = true;

                currentItem = wrapper[lbttab.SelectedIndex];
				origItem = currentItem.Clone();

				setStringIndex(currentItem.StringIndex, true, true);

				setBHAV(0, currentItem.Action, false);
				setBHAV(1, currentItem.Guardian, false);

				this.tbFlags.Text = "0x"+Helper.HexString(currentItem.Flags);
				this.tbFlags2.Text = "0x"+Helper.HexString(currentItem.Flags2);
				if (currentItem.AttenuationCode < this.cbAttenuationCode.Items.Count)
				{
					cbAttenuationCode.SelectedIndex = (int)currentItem.AttenuationCode;
				}
				else
				{
					cbAttenuationCode.SelectedIndex = -1;
					cbAttenuationCode.Text = "0x"+Helper.HexString(currentItem.AttenuationCode);
				}
				tbAttenuationValue.Text = currentItem.AttenuationValue.ToString("N8");
				tbAutonomy.Text = "0x"+Helper.HexString(currentItem.Autonomy);
				tbJoinIndex.Text = "0x"+Helper.HexString(currentItem.JoinIndex);
				tbUIDispType.Text = "0x"+Helper.HexString(currentItem.UIDisplayType);
				tbFaceAnimID.Text = "0x"+Helper.HexString(currentItem.FacialAnimationID);
				tbMemIterMult.Text = currentItem.MemoryIterativeMultiplier.ToString("N8");
				tbObjType.Text = "0x"+Helper.HexString(currentItem.ObjectType);
				tbModelTabID.Text = "0x"+Helper.HexString(currentItem.ModelTableID);

				doFlags();

                timtuiHuman.MotiveTable = wrapper[lbttab.SelectedIndex].HumanMotives;
                timtuiAnimal.MotiveTable = wrapper[lbttab.SelectedIndex].AnimalMotives;
            }
			else
			{
                tabControl1.Enabled = this.btnDelete.Enabled = false;

				cbAttenuationCode.SelectedIndex = -1;
				tbGuardian.Text = tbAction.Text = lbguard.Text = lbaction.Text = tbFlags.Text = tbFlags2.Text =
					tbStringIndex.Text = tbAttenuationValue.Text = tbAutonomy.Text = tbJoinIndex.Text =
					tbUIDispType.Text = tbFaceAnimID.Text = tbMemIterMult.Text = tbObjType.Text = tbModelTabID.Text = 
					"";
				for (int i = 0; i < alFlags.Count; i++) ((CheckBox)alFlags[i]).Checked = false;
			}

            this.ttabPanel.ResumeLayout();
            this.ttabPanel.Cursor = Cursors.Default;

            internalchg = false;
        }		

        /*
         * By way of reminder:
         * action           - ushort - 4 hex digits (BHAV number)
         * guard            - ushort - 4 hex digits (BHAV number)
         * flags            - ushort - 4 hex digits
         * flags2           - ushort - 4 hex digits
         * strindex         - uint   - 8 hex digits
         * attenuationcode  - uint   - 8 hex digits
         * attenuationvalue - uint   - 8 hex digits
         * autonomy         - uint   - 8 hex digits
         * joinindex        - uint   - 8 hex digits
         * uidisplaytype    - ushort - 4 hex digits
         * facialanimation  - uint   - 8 hex digits
         * memoryitermult   - float  - decimal digits and "."
         * objecttype       - uint   - 8 hex digits
         * modeltableid     - uint   - 8 hex digits
         */

        private void GetTTABGuard(object sender, System.EventArgs e)
		{
			pjse.FileTable.Entry item = new pjse.ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, ttabPanel.Parent);
			if (item != null)
				setBHAV(1, (ushort)item.Instance, false);
		}

		private void GetTTABAction(object sender, System.EventArgs e)
		{
			pjse.FileTable.Entry item = new pjse.ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, ttabPanel.Parent);
			if (item != null)
				setBHAV(0, (ushort)item.Instance, false);
		}

        private void llBhav_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            pjse.FileTable.Entry item = wrapper.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, (sender == llAction) ? currentItem.Action : currentItem.Guardian);
            Bhav b = new Bhav();
            b.ProcessData(item.PFD, item.Package);

            BhavForm ui = (BhavForm)b.UIHandler;
            ui.Tag = "Popup"; // tells the SetReadOnly function it's in a popup - so everything locked down
            ui.Text = pjse.Localization.GetString("viewbhav")
                + ": " + b.FileName + " [" + b.Package.SaveFileName + "]";
            b.RefreshUI();
            ui.Show();
        }


        private void btnNoFlags_Click(object sender, System.EventArgs e)
        {
            internalchg = true;
            currentItem.Flags = (ushort)0x0070;
            this.tbFlags.Text = "0x" + Helper.HexString(currentItem.Flags);
            doFlags();
            internalchg = false;
        }

        private void checkbox_CheckedChanged(object sender, System.EventArgs e)
        {
            if (internalchg) return;

            if (!(sender is CheckBox)) return;
            bool val = ((CheckBox)sender).Checked;

            int i = alFlags.IndexOf(sender);
            if (i < 0)
                throw new Exception("checkbox_CheckedChanged not applicable to control " + sender.ToString());

            bool invert = wrapper.Format < 0x54 && (i == 4 || i == 5 || i == 6);

            internalchg = true;
            Boolset flags = new Boolset(currentItem.Flags);
            flags[i] = invert ? !val : val;
            currentItem.Flags = flags;
            this.tbFlags.Text = "0x" + Helper.HexString(currentItem.Flags);
            internalchg = false;
        }


        private void cbHex32_Enter(object sender, System.EventArgs e)
		{
			((ComboBox)sender).SelectAll();
		}

		private void cbHex32_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!cbHex32_IsValid(sender)) return;
			if (((ComboBox)sender).FindStringExact(((ComboBox)sender).Text) >= 0) return;

			uint val = Convert.ToUInt32(((ComboBox)sender).Text, 16);
			internalchg = true;
			switch (alHex32cb.IndexOf(sender))
			{
				case 0:
					currentItem.StringIndex = val;
					setStringIndex(val, true, false);
					lbttab.Items[lbttab.SelectedIndex] = currentItem;
					break;
				case 1: currentItem.AttenuationCode = val; break;
			}
			internalchg = false;
		}

		private void cbHex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (cbHex32_IsValid(sender)) return;

			e.Cancel = true;

			int i = alHex32cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex32_Validating not applicable to control " + sender.ToString());

			uint val = 0;
			switch (i)
			{
				case 0: val = origItem.StringIndex; currentItem.StringIndex = val; break;
				case 1: val = origItem.AttenuationCode; currentItem.AttenuationCode = val; break;
			}

			bool origstate = internalchg;
			internalchg = true;
			if (i == 0)
			{
				setStringIndex(val, true, true);
				lbttab.Items[lbttab.SelectedIndex] = currentItem;
			}
			else if (i == 1)
			{
				if (val < ((ComboBox)sender).Items.Count)
				{
					((ComboBox)sender).SelectedIndex = (int)val;
				}
				else
				{
					((ComboBox)sender).SelectedIndex = -1;
					((ComboBox)sender).Text = "0x"+Helper.HexString(val);
				}
			}
			internalchg = origstate;
			((ComboBox)sender).SelectAll();
		}

		private void cbHex32_Validated(object sender, System.EventArgs e)
		{
			int i = alHex32cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex32_Validated not applicable to control " + sender.ToString());
			if (((ComboBox)sender).FindStringExact(((ComboBox)sender).Text) >= 0) return;

			uint val = Convert.ToUInt32(((ComboBox)sender).Text, 16);

			bool origstate = internalchg;
			internalchg = true;
			if (i == 0)
			{
				setStringIndex(val, true, true);
			}
			else if (i == 1)
			{
				if (val < ((ComboBox)sender).Items.Count)
				{
					((ComboBox)sender).SelectedIndex = (int)val;
				}
				else
				{
					((ComboBox)sender).SelectedIndex = -1;
					((ComboBox)sender).Text = "0x"+Helper.HexString(val);
				}
			}
			internalchg = origstate;
			((ComboBox)sender).Select(0, 0);
		}

		private void cbHex32_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			int i = alHex32cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex32_SelectedIndexChanged not applicable to control " + sender.ToString());
			if (((ComboBox)sender).SelectedIndex == -1) return;

            int val = ((ComboBox)sender).SelectedIndex;

			internalchg = true;
			if (i == 0)
			{
				currentItem.StringIndex = (uint)val;
                setStringIndex(currentItem.StringIndex, true, false);
                if (val < cbStringIndex.Items.Count)
                    lbttab.Items[lbttab.SelectedIndex] = cbStringIndex.Items[val];
                else
                    lbttab.Items[lbttab.SelectedIndex] = "0x" + val.ToString("X") + ": " + pjse.Localization.GetString("UNK");
                tbStringIndex.Focus();
            }
			else if (i == 1)
			{
				currentItem.AttenuationCode = (uint)val;
			}
			internalchg = false;

			((ComboBox)sender).SelectAll();
		}


		private void hex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex16_IsValid(sender)) return;

			ushort val = Convert.ToUInt16(((TextBox)sender).Text, 16);
			internalchg = true;
			switch (alHex16.IndexOf(sender))
			{
				case 0:
					currentItem.Action = val;
					setBHAV(0, val, true);
					break;
				case 1:
					currentItem.Guardian = val;
					setBHAV(1, val, true);
					break;
				case 2:
					currentItem.Flags = val;
					doFlags();
					break;
				case 3: currentItem.Flags2 = val; break;
				case 4: currentItem.UIDisplayType = val; break;
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
				case 2:
					currentItem.Flags = val = origItem.Flags;
					doFlags();
					break;
				case 3: currentItem.Flags2 = val = origItem.Flags2; break;
				case 4: currentItem.UIDisplayType = val = origItem.UIDisplayType; break;
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


		private void hex32_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex32_IsValid(sender)) return;

			uint val = Convert.ToUInt32(((TextBox)sender).Text, 16);
			internalchg = true;
			switch (alHex32.IndexOf(sender))
			{
				case 0: wrapper.Format = val; break;
				case 1:
					currentItem.StringIndex = val;
					setStringIndex(val, false, true);
                    if (val < cbStringIndex.Items.Count)
                        lbttab.Items[lbttab.SelectedIndex] = cbStringIndex.Items[(int)val];
                    else
                        lbttab.Items[lbttab.SelectedIndex] = "0x" + val.ToString("X") + ": " + pjse.Localization.GetString("UNK");
                    break;
				case 2: currentItem.Autonomy = val; break;
				case 3: currentItem.FacialAnimationID = val; break;
				case 4: currentItem.ObjectType = val; break;
				case 5: currentItem.ModelTableID = val; break;
				case 6: currentItem.JoinIndex = val; break;
			}
			internalchg = false;
		}

		private void hex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex32_IsValid(sender)) return;

			e.Cancel = true;

			internalchg = true;
			uint val = 0;
			switch (alHex32.IndexOf(sender))
			{
				case 0: val = wrapper.Format; break;
				case 1:
					currentItem.StringIndex = val = origItem.StringIndex;
					lbttab.Items[lbttab.SelectedIndex] = currentItem;
					break;
				case 2: currentItem.Autonomy = val = origItem.Autonomy; break;
				case 3: currentItem.FacialAnimationID = val = origItem.FacialAnimationID; break;
				case 4: currentItem.ObjectType = val = origItem.ObjectType; break;
				case 5: currentItem.ModelTableID = val = origItem.ModelTableID; break;
				case 6: currentItem.JoinIndex = val = origItem.JoinIndex; break;
			}

			((TextBox)sender).Text = "0x" + Helper.HexString(val);
			((TextBox)sender).SelectAll();
			internalchg = false;
		}

		private void hex32_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + Helper.HexString(Convert.ToUInt32(((TextBox)sender).Text, 16));
			((TextBox)sender).SelectAll();
			internalchg = origstate;
            if (alHex32.IndexOf(sender) == 0) setFormat();
		}


		private void float_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!float_IsValid(sender)) return;

			float val = Convert.ToSingle(((TextBox)sender).Text);
			internalchg = true;
			switch (alFloats.IndexOf(sender))
			{
				case 0: currentItem.AttenuationValue = val; break;
				case 1: currentItem.MemoryIterativeMultiplier = val; break;
			}
			internalchg = false;
		}

		private void float_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (float_IsValid(sender)) return;

			e.Cancel = true;

			internalchg = true;
			float val = 0.0f;
			switch (alFloats.IndexOf(sender))
			{
				case 0: currentItem.AttenuationValue = val = origItem.AttenuationValue; break;
				case 1: currentItem.MemoryIterativeMultiplier = val = origItem.MemoryIterativeMultiplier; break;
			}

			((TextBox)sender).Text = val.ToString("N8");
			((TextBox)sender).SelectAll();
			internalchg = false;
		}

		private void float_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = Convert.ToSingle(((TextBox)sender).Text).ToString("N8");
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}

	}
}
