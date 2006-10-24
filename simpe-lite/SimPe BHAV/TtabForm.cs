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
		private System.Windows.Forms.CheckBox cbunk3;
		private System.Windows.Forms.CheckBox cbunk4;
		private System.Windows.Forms.CheckBox cbunk1;
		private System.Windows.Forms.CheckBox cbunk2;
		private System.Windows.Forms.CheckBox cbteens;
		private System.Windows.Forms.CheckBox cbelders;
		private System.Windows.Forms.CheckBox cbtodlers;
		private System.Windows.Forms.CheckBox cbautofirst;
		private System.Windows.Forms.CheckBox cbdebugmenu;
		private System.Windows.Forms.CheckBox cbadults;
		private System.Windows.Forms.CheckBox cbdemochild;
		private System.Windows.Forms.CheckBox cbchildren;
		private System.Windows.Forms.CheckBox cbconsecutive;
		private System.Windows.Forms.CheckBox cbimmediately;
		private System.Windows.Forms.CheckBox cbjoinable;
		private System.Windows.Forms.TabPage tpMotives;
		private System.Windows.Forms.CheckBox cbvisitor;
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
							    cbvisitor   ,cbjoinable  ,cbimmediately ,cbconsecutive
							   ,cbchildren  ,cbdemochild ,cbadults      ,cbdebugmenu
							   ,cbautofirst ,cbtodlers   ,cbelders      ,cbteens
							   ,cbunk1      ,cbunk2      ,cbunk3        ,cbunk4
						   };
			alFlags = new ArrayList(cba);

			ComboBox[] cbb = { cbStringIndex ,cbAttenuationCode };
			alHex32cb = new ArrayList(cbb);

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

		private void doFlags()
		{
			internalchg = true;
			bool val;
			for (int i = 0; i < alFlags.Count; i++)
			{
				switch(i)
				{
					case  0: val = currentItem.Flags.ByVisitors; break;
					case  1: val = currentItem.Flags.Joinable; break;
					case  2: val = currentItem.Flags.RunImmediately; break;
					case  3: val = currentItem.Flags.AvailConsecutive; break;
					case  4: val = currentItem.Flags.ByChildren; break;
					case  5: val = currentItem.Flags.ByDemoChild; break;
					case  6: val = currentItem.Flags.ByAdults; break;
					case  7: val = currentItem.Flags.DebugMenu; break;
					case  8: val = currentItem.Flags.AutoFirstSelect; break;
					case  9: val = currentItem.Flags.ByToddlers; break;
					case 10: val = currentItem.Flags.ByElders; break;
					case 11: val = currentItem.Flags.ByTeens; break;
					case 12: val = currentItem.Flags.Unknown1; break;
					case 13: val = currentItem.Flags.Unknown2; break;
					case 14: val = currentItem.Flags.Unknown3; break;
					case 15: val = currentItem.Flags.Unknown4; break;
					default: val = false; break;
				}
				((CheckBox)alFlags[i]).Checked = val;
			}
			internalchg = false;
		}
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
        }

        private void populateLbttab()
        {
            lbttab.Items.Clear();
            for (int i = 0; i < wrapper.Count; i++) addItem(i);
        }

        private void setFormat()
        {
            if (wrapper.Format == previousFormat) return;

            if (previousFormat >= 0x44 && wrapper.Format < 0x44)
            {
                DialogResult dr = MessageBox.Show(pjse.Localization.GetString("ttabForm_Sure"),
                    pjse.Localization.GetString("ttabForm_Single"),
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (!DialogResult.OK.Equals(dr))
                {
                    wrapper.Format = previousFormat;
                    return;
                }
                changeSize();
            }
            else if (previousFormat < 0x44 && wrapper.Format >= 0x44 && wrapper.Format < 0x54)
            {
                DialogResult dr = MessageBox.Show(pjse.Localization.GetString("ttabForm_Sure"),
                    pjse.Localization.GetString("ttabForm_MultipleFixed"),
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (!DialogResult.OK.Equals(dr))
                {
                    wrapper.Format = previousFormat;
                    return;
                }
                changeSize();
            }
            else if (previousFormat < 0x54 && wrapper.Format >= 0x54)
            {
                DialogResult dr = MessageBox.Show(pjse.Localization.GetString("ttabForm_Sure"),
                    pjse.Localization.GetString("ttabForm_MultipleVaries"),
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (!DialogResult.OK.Equals(dr))
                {
                    wrapper.Format = previousFormat;
                    return;
                }
                changeSize();
            }
            previousFormat = wrapper.Format;

            this.tbUIDispType.Enabled = this.tbFaceAnimID.Enabled =
                this.tbModelTabID.Enabled = this.tbMemIterMult.Enabled = this.tbObjType.Enabled = false;

            if (previousFormat >= 0x45)
            {
                this.tbUIDispType.Enabled = true;
                if (previousFormat >= 0x46)
                {
                    this.tbModelTabID.Enabled = true;
                    if (previousFormat >= 0x4a)
                    {
                        this.tbFaceAnimID.Enabled = true;
                        if (previousFormat >= 0x4c)
                        {
                            this.tbMemIterMult.Enabled = this.tbObjType.Enabled = true;
                        }
                    }
                }
            }

        }

        private void changeSize()
        {
            this.ttabPanel.SuspendLayout();

            this.tabControl1.Enabled = false;
            this.tabControl1.TabPages.Clear();
            this.tabControl1.TabPages.Add(this.tpSettings);

            if (lbttab.SelectedIndex >= 0)
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TtabForm));
                if (wrapper[lbttab.SelectedIndex].HumanMotives != null)
                {
                    TabPage tp = new TabPage();
                    resources.ApplyResources(tp, "tpMotives");
                    TtabItemMotiveTableUI timtui = new TtabItemMotiveTableUI();
                    timtui.Location = new Point(0, 0);
                    timtui.Dock = DockStyle.Fill;
                    timtui.MotiveTable = wrapper[lbttab.SelectedIndex].HumanMotives;
                    tp.Controls.Add(timtui);
                    this.tabControl1.TabPages.Add(tp);
                }
                if (wrapper[lbttab.SelectedIndex].Animals != null)
                    for (int i = 0; i < wrapper[lbttab.SelectedIndex].Animals.Length; i++)
                    {
                        TabPage tp = new TabPage();
                        resources.ApplyResources(tp, "tpMotives");
                        tp.Text = tp.Tag + " [" + i.ToString() + "]";
                        TtabItemMotiveTableUI timtui = new TtabItemMotiveTableUI();
                        timtui.Location = new Point(0, 0);
                        timtui.Dock = DockStyle.Fill;
                        timtui.MotiveTable = wrapper[lbttab.SelectedIndex].Animals[i];
                        tp.Controls.Add(timtui);
                        this.tabControl1.TabPages.Add(tp);
                    }
                this.tabControl1.Enabled = true;
            }

            this.ttabPanel.ResumeLayout();
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
			WrapperChanged(wrapper, null);

			internalchg = true;

            this.cbStringIndex.SelectedIndex = -1;
            GFT_FiletableRefresh(null, null);

			internalchg = false;

            previousFormat = wrapper.Format;

			if (lbttab.Items.Count>0) lbttab.SelectedIndex = 0;
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
			this.Text = tbFilename.Text = wrapper.FileName;
			tbFormat.Text = "0x"+Helper.HexString(wrapper.Format);
            changeSize();
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
            this.cbvisitor = new System.Windows.Forms.CheckBox();
            this.cbunk3 = new System.Windows.Forms.CheckBox();
            this.cbunk4 = new System.Windows.Forms.CheckBox();
            this.cbunk1 = new System.Windows.Forms.CheckBox();
            this.cbunk2 = new System.Windows.Forms.CheckBox();
            this.cbteens = new System.Windows.Forms.CheckBox();
            this.cbelders = new System.Windows.Forms.CheckBox();
            this.cbtodlers = new System.Windows.Forms.CheckBox();
            this.cbautofirst = new System.Windows.Forms.CheckBox();
            this.cbdebugmenu = new System.Windows.Forms.CheckBox();
            this.cbadults = new System.Windows.Forms.CheckBox();
            this.cbdemochild = new System.Windows.Forms.CheckBox();
            this.cbchildren = new System.Windows.Forms.CheckBox();
            this.cbconsecutive = new System.Windows.Forms.CheckBox();
            this.cbimmediately = new System.Windows.Forms.CheckBox();
            this.cbjoinable = new System.Windows.Forms.CheckBox();
            this.tbAction = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbJoinIndex = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tpMotives = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnRefreshFT = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.ttabPanel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpSettings.SuspendLayout();
            this.gbFlags.SuspendLayout();
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
            this.tabControl1.Controls.Add(this.tpMotives);
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
            this.gbFlags.Controls.Add(this.cbvisitor);
            this.gbFlags.Controls.Add(this.cbunk3);
            this.gbFlags.Controls.Add(this.cbunk4);
            this.gbFlags.Controls.Add(this.cbunk1);
            this.gbFlags.Controls.Add(this.cbunk2);
            this.gbFlags.Controls.Add(this.cbteens);
            this.gbFlags.Controls.Add(this.cbelders);
            this.gbFlags.Controls.Add(this.cbtodlers);
            this.gbFlags.Controls.Add(this.cbautofirst);
            this.gbFlags.Controls.Add(this.cbdebugmenu);
            this.gbFlags.Controls.Add(this.cbadults);
            this.gbFlags.Controls.Add(this.cbdemochild);
            this.gbFlags.Controls.Add(this.cbchildren);
            this.gbFlags.Controls.Add(this.cbconsecutive);
            this.gbFlags.Controls.Add(this.cbimmediately);
            this.gbFlags.Controls.Add(this.cbjoinable);
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
            // cbvisitor
            // 
            resources.ApplyResources(this.cbvisitor, "cbvisitor");
            this.cbvisitor.Name = "cbvisitor";
            this.cbvisitor.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbunk3
            // 
            resources.ApplyResources(this.cbunk3, "cbunk3");
            this.cbunk3.Name = "cbunk3";
            this.cbunk3.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbunk4
            // 
            resources.ApplyResources(this.cbunk4, "cbunk4");
            this.cbunk4.Name = "cbunk4";
            this.cbunk4.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbunk1
            // 
            resources.ApplyResources(this.cbunk1, "cbunk1");
            this.cbunk1.Name = "cbunk1";
            this.cbunk1.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbunk2
            // 
            resources.ApplyResources(this.cbunk2, "cbunk2");
            this.cbunk2.Name = "cbunk2";
            this.cbunk2.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbteens
            // 
            resources.ApplyResources(this.cbteens, "cbteens");
            this.cbteens.Name = "cbteens";
            this.cbteens.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbelders
            // 
            resources.ApplyResources(this.cbelders, "cbelders");
            this.cbelders.Name = "cbelders";
            this.cbelders.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbtodlers
            // 
            resources.ApplyResources(this.cbtodlers, "cbtodlers");
            this.cbtodlers.Name = "cbtodlers";
            this.cbtodlers.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbautofirst
            // 
            resources.ApplyResources(this.cbautofirst, "cbautofirst");
            this.cbautofirst.Name = "cbautofirst";
            this.cbautofirst.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbdebugmenu
            // 
            resources.ApplyResources(this.cbdebugmenu, "cbdebugmenu");
            this.cbdebugmenu.Name = "cbdebugmenu";
            this.cbdebugmenu.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbadults
            // 
            resources.ApplyResources(this.cbadults, "cbadults");
            this.cbadults.Name = "cbadults";
            this.cbadults.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbdemochild
            // 
            resources.ApplyResources(this.cbdemochild, "cbdemochild");
            this.cbdemochild.Name = "cbdemochild";
            this.cbdemochild.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbchildren
            // 
            resources.ApplyResources(this.cbchildren, "cbchildren");
            this.cbchildren.Name = "cbchildren";
            this.cbchildren.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbconsecutive
            // 
            resources.ApplyResources(this.cbconsecutive, "cbconsecutive");
            this.cbconsecutive.Name = "cbconsecutive";
            this.cbconsecutive.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbimmediately
            // 
            resources.ApplyResources(this.cbimmediately, "cbimmediately");
            this.cbimmediately.Name = "cbimmediately";
            this.cbimmediately.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbjoinable
            // 
            resources.ApplyResources(this.cbjoinable, "cbjoinable");
            this.cbjoinable.Name = "cbjoinable";
            this.cbjoinable.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
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
            // tpMotives
            // 
            resources.ApplyResources(this.tpMotives, "tpMotives");
            this.tpMotives.Name = "tpMotives";
            this.tpMotives.Tag = "Animal Motive Group";
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
            // TtabForm
            // 
            resources.ApplyResources(this, "$this");
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
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

		}
	
		#endregion

		private void TtabSelect(object sender, System.EventArgs e)
		{
			if (this.internalchg) return;

			this.btnDelete.Enabled = false;
            this.btnStrPrev.Enabled = (lbttab.SelectedIndex > 0);
            this.btnStrNext.Enabled = (lbttab.SelectedIndex < lbttab.Items.Count - 1);

			if (lbttab.SelectedIndex >= 0)
			{
				currentItem = wrapper[lbttab.SelectedIndex];
				origItem = currentItem.Clone();

				internalchg = true;

				btnDelete.Enabled = true;

				setStringIndex(currentItem.StringIndex, true, true);

				setBHAV(0, currentItem.Action, false);
				setBHAV(1, currentItem.Guardian, false);

				this.tbFlags.Text = "0x"+Helper.HexString(currentItem.Flags.Value);
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

                changeSize();

				internalchg = false;
			}
			else
			{
				internalchg = true;
				cbAttenuationCode.SelectedIndex = -1;
				tbGuardian.Text = tbAction.Text = lbguard.Text = lbaction.Text = tbFlags.Text = tbFlags2.Text =
					tbStringIndex.Text = tbAttenuationValue.Text = tbAutonomy.Text = tbJoinIndex.Text =
					tbUIDispType.Text = tbFaceAnimID.Text = tbMemIterMult.Text = tbObjType.Text = tbModelTabID.Text = 
					"";
				for (int i = 0; i < alFlags.Count; i++) ((CheckBox)alFlags[i]).Checked = false;
                changeSize();
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
			ui.Text = pjse.Localization.GetString("viewbhav")
                + ": " + b.FileName + " [" + b.Package.SaveFileName + "]";
			b.RefreshUI();
			ui.Show();
		}

		private void btnCommit_Click(object sender, System.EventArgs e)
		{
			try 
			{
				wrapper.SynchronizeUserData();
				btnCommit.Enabled = wrapper.Changed;
				TtabSelect(null, null);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(pjse.Localization.GetString("errwritingfile"), ex);
			}			
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
            pjse.HelpHelper.Help("Contents");
		}


		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			int i = wrapper.Add((lbttab.SelectedIndex == -1) ? new TtabItem(wrapper) : wrapper[lbttab.SelectedIndex].Clone());
			if (i < 0) return;

            addItem(i);
            lbttab.SelectedIndex = i;
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			if (lbttab.SelectedIndex < 0) return;

			int i = lbttab.SelectedIndex;

			lbttab.Items.RemoveAt(i);
			wrapper.RemoveAt(i);

			if (i >= lbttab.Items.Count)
				i = lbttab.Items.Count - 1;
			lbttab.SelectedIndex = -1;
			lbttab.SelectedIndex = i;
		}

		private void btnAppend_Click(object sender, System.EventArgs e)
		{
			this.Append((new pjse.ResourceChooser()).Execute(wrapper.FileDescriptor.Type, wrapper.FileDescriptor.Group, ttabPanel));
		}

		private void btnNoFlags_Click(object sender, System.EventArgs e)
		{
			internalchg = true;
			currentItem.Flags.Value = (ushort)0x0070;
			this.tbFlags.Text = "0x"+Helper.HexString(currentItem.Flags.Value);
			doFlags();
			internalchg = false;
		}


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


		private void tbFilename_TextChanged(object sender, System.EventArgs e)
		{
			wrapper.FileName = tbFilename.Text;
		}

		private void tbFilename_Validated(object sender, System.EventArgs e)
		{
			tbFilename.SelectAll();
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
					currentItem.Flags.Value = val;
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
					currentItem.Flags.Value = val = origItem.Flags.Value;
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


		private void checkbox_CheckedChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			if (!(sender is CheckBox)) return;
			bool val = ((CheckBox)sender).Checked;

			int i = alFlags.IndexOf(sender);
			switch(i)
			{
				case  0: currentItem.Flags.ByVisitors = val; break;
				case  1: currentItem.Flags.Joinable = val; break;
				case  2: currentItem.Flags.RunImmediately = val; break;
				case  3: currentItem.Flags.AvailConsecutive = val; break;
				case  4: currentItem.Flags.ByChildren = val; break;
				case  5: currentItem.Flags.ByDemoChild = val; break;
				case  6: currentItem.Flags.ByAdults = val; break;
				case  7: currentItem.Flags.DebugMenu = val; break;
				case  8: currentItem.Flags.AutoFirstSelect = val; break;
				case  9: currentItem.Flags.ByToddlers = val; break;
				case 10: currentItem.Flags.ByElders = val; break;
				case 11: currentItem.Flags.ByTeens = val; break;
				case 12: currentItem.Flags.Unknown1 = val; break;
				case 13: currentItem.Flags.Unknown2 = val; break;
				case 14: currentItem.Flags.Unknown3 = val; break;
				case 15: currentItem.Flags.Unknown4 = val; break;
				default:
					throw new Exception("checkbox_CheckedChanged not applicable to control " + sender.ToString());
			}
			internalchg = true;
			this.tbFlags.Text = "0x"+Helper.HexString(currentItem.Flags.Value);
			internalchg = false;
		}

        private void btnRefreshFT_Click(object sender, EventArgs e)
        {
            pjse.FileTable.GFT.UIRefresh();
        }

        private void btnStrPrev_Click(object sender, EventArgs e)
        {
            lbttab.SelectedIndex--;
        }

        private void btnStrNext_Click(object sender, EventArgs e)
        {
            lbttab.SelectedIndex++;
        }

	}
}
