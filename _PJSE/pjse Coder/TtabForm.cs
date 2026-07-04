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
using System.Data;
using System.Collections.Generic;
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

        private booby.gradientpanel ttabPanel;
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
        private Button btnStrPrev;
        private Button btnStrNext;
        private TabPage tpAnimalMotives;
        private TtabItemMotiveTableUI timtuiHuman;
        private TtabItemMotiveTableUI timtuiAnimal;
        private GroupBox gbFlags2;
        private TextBox tbFlags2;
        private Button btnNoFlags2;
        private Label label3;
        private CheckBox cb2Bit0;
        private CheckBox cb2BitE;
        private CheckBox cb2BitF;
        private CheckBox cb2BitC;
        private CheckBox cb2BitD;
        private CheckBox cb2BitB;
        private CheckBox cb2BitA;
        private CheckBox cb2Bit9;
        private CheckBox cb2Bit8;
        private CheckBox cb2Bit7;
        private CheckBox cb2Bit6;
        private CheckBox cb2Bit5;
        private CheckBox cb2Bit4;
        private CheckBox cb2Bit3;
        private CheckBox cb2Bit2;
        private CheckBox cb2Bit1;
        private Label lbPieString;
        private pjse_banner pjse_banner1;
        private Button btnMoveDown;
        private Button btnMoveUp;
        private SplitContainer splitContainer1;
        private TableLayoutPanel tlpSettingsHead;
        private Label label4;
        private Label lbTTABEntry;
        private FlowLayoutPanel flpPieStringID;
        private FlowLayoutPanel flpAction;
        private FlowLayoutPanel flpGuard;
        private FlowLayoutPanel flpFileCtrl;
        private TableLayoutPanel tableLayoutPanel1;
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
							    cbBit0 ,cbBit1 ,cbBit2 ,cbBit3 ,cbBit4 ,cbBit5 ,cbBit6 ,cbBit7
							   ,cbBit8 ,cbBit9 ,cbBitA ,cbBitB ,cbBitC ,cbBitD ,cbBitE ,cbBitF
							   ,cb2Bit0 ,cb2Bit1 ,cb2Bit2 ,cb2Bit3 ,cb2Bit4 ,cb2Bit5 ,cb2Bit6 ,cb2Bit7
							   ,cb2Bit8 ,cb2Bit9 ,cb2BitA ,cb2BitB ,cb2BitC ,cb2BitD ,cb2BitE ,cb2BitF
						   };
			alFlags = new ArrayList(cba);

			ComboBox[] cbb = { cbStringIndex ,cbAttenuationCode };
			alHex32cb = new ArrayList(cbb);

            this.label40.Left = this.tbStringIndex.Left - this.label40.Width - 6;
            this.llAction.Left = this.tbStringIndex.Left - this.llAction.Width - 6;
            this.llGuardian.Left = this.tbStringIndex.Left - this.llGuardian.Width - 6;

            Label[] al = { label32, label31, label1, label35, label30, label2, label29, label34, label33 };
            //foreach (Label l in al)
            //    l.Left = cbAttenuationCode.Left - l.Width - 6;
            
            if (booby.ThemeManager.ThemedForms)
            {
                booby.ThemeManager.Global.AddControl(this.ttabPanel);
                booby.ThemeManager.Global.AddControl(this.btnCommit);
                this.lbttab.BackColor = booby.ThemeManager.Global.ThemeColorLighter;
                this.tpSettings.BackColor = booby.ThemeManager.Global.ThemeColorLighter;
                this.tpHumanMotives.BackColor = booby.ThemeManager.Global.ThemeColorLighter;
                this.tpAnimalMotives.BackColor = booby.ThemeManager.Global.ThemeColorLighter;
            }
            if (SimPe.Helper.WindowsRegistry.UseBigIcons)
            {
                this.lbttab.Font = new System.Drawing.Font(this.ttabPanel.Font.Name, 11F);
                this.splitContainer1.SplitterDistance = 400;
                this.ttabPanel.BackgroundImageLocation = new System.Drawing.Point(1010, 0);
            }
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

            uint offset = getTTAsCount();
            uint maxtti = getMaxTtabItemStringIndex() + 1;
            //if (maxtti != wrapper.Count)
            offset = getUserChoice(offset, maxtti, (uint)wrapper.Count);
            if (offset >= 0x8000) return;

            bool savedstate = internalchg;
			internalchg = true;

			ttabPanel.Parent.Cursor = Cursors.WaitCursor;

			Ttab b = (Ttab)e.Wrapper;

			for (int bi = 0; bi < b.Count; bi++)
			{
                wrapper.Add(b[bi]);
                wrapper[wrapper.Count - 1].StringIndex += offset;
                addItem(wrapper.Count - 1);
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

        private uint getTTAsCount()
		{
            Str w = StrRes;
            if (w == null) return 0;

            uint max = 0;
            for (byte lid = 1; lid < 44; lid++) max = (uint)Math.Max(max, w[lid].Count);
            return max;
        }

        private uint getMaxTtabItemStringIndex()
        {
            uint m = 0;
            foreach(TtabItem ti in wrapper) if (ti.StringIndex > m) m = ti.StringIndex;
            return m;
        }

        private uint getUserChoice(uint offset, uint maxtti, uint nr)
        {
            PickANumber pan = new PickANumber(
                    new ushort[] { (ushort)(maxtti & 0x7fff) },
                    new String[] { "Increase new Pie String IDs by" }
                );
            pan.Title = "\"Pie String ID\" increment";
            pan.Prompt = "";
            DialogResult dr = pan.ShowDialog();
            if (dr == DialogResult.OK)
                return pan.Value;
            return 0xffffffff;
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
                //FallbackStrItem si = w[1, i]; // crap, only shows yanky language not real
                FallbackStrItem si = w[(byte)SimPe.Helper.WindowsRegistry.LanguageCode, i]; // This is how it will appear in-game
                if (SimPe.Helper.WindowsRegistry.HiddenMode)
                {
                    this.cbStringIndex.Items.Add("0x" + i.ToString("X") + " (" + i + "): "
                        + ((si == null)
                        ? "*!no default string!*"
                        : si.strItem.Title + (si.lidFallback ? " [LID=1]" : "") + (si.fallback.Count > 0 ? " [*]" : "")
                        ));
                }
                else
                {
                    this.cbStringIndex.Items.Add("0x" + i.ToString("X") + " (" + i + "): "
                        + ((si == null)
                        ? "*!no default string!*"
                        : si.strItem.Title
                        ));
                }
            }

            if (cbStringIndexSelectedIndex >= 0 && cbStringIndexSelectedIndex < this.cbStringIndex.Items.Count)
                this.cbStringIndex.SelectedIndex = cbStringIndexSelectedIndex;
            else
                this.cbStringIndex.SelectedIndex = -1;

            internalchg = prev;
        }

        private void populateLbttab()
        {
            bool prev = internalchg;
            internalchg = true;
            this.ttabPanel.SuspendLayout();

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

            this.ttabPanel.ResumeLayout();
            internalchg = false;
            TtabSelect(null, null);

            internalchg = prev;
        }

        private void doFlags()
        {
            internalchg = true;
            Boolset flags = new Boolset(currentItem.Flags);
            if (wrapper.Format < 0x54) flags.flip(new int[] { 4, 5, 6 });
            for (int i = 0; i < flags.Length; i++)
                ((CheckBox)alFlags[i]).Checked = flags[i];
            internalchg = false;
        }

        private void doFlags2()
        {
            internalchg = true;
            Boolset flags = new Boolset(currentItem.Flags2);
            for (int i = 0; i < flags.Length; i++)
                ((CheckBox)alFlags[i + 16]).Checked = flags[i];
            internalchg = false;
        }

        private uint previousFormat;
        private void resetFormat()
        {
            bool saved = internalchg;
            internalchg = true;

            currentItem = null;
            lbttab.SelectedIndex = -1;

            for (int i = 0; i < wrapper.Count; i++)
                wrapper[i] = wrapper[i].Clone();


            // Flip those flags
            if (previousFormat < 0x54 && wrapper.Format >= 0x54 || previousFormat >= 0x54 && wrapper.Format < 0x54)
            {
                Boolset flags;
                foreach (TtabItem ti in wrapper)
                {
                    flags = new Boolset(ti.Flags);
                    flags.flip(new int[] { 4, 5, 6 });
                    ti.Flags = flags;
                }
            }

            previousFormat = wrapper.Format;

            internalchg = saved;
        }
        private void setFormat()
        {
            int siWas = lbttab.SelectedIndex;

            if (wrapper.Format < 0x44 && previousFormat >= 0x44)
            {
                DialogResult dr = MessageBox.Show(pjse.Localization.GetString("ttabForm_Sure"),
                    pjse.Localization.GetString("ttabForm_Single"),
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (!DialogResult.OK.Equals(dr))
                    wrapper.Format = previousFormat;
                else
                    resetFormat();
            }
            else if (wrapper.Format >= 0x44 && wrapper.Format < 0x54 && (previousFormat < 0x44 || previousFormat >= 0x54))
            {
                DialogResult dr = MessageBox.Show(pjse.Localization.GetString("ttabForm_Sure"),
                    pjse.Localization.GetString("ttabForm_MultipleFixed"),
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (!DialogResult.OK.Equals(dr))
                    wrapper.Format = previousFormat;
                else
                    resetFormat();
            }
            else if (wrapper.Format >= 0x54 && previousFormat < 0x54)
            {
                DialogResult dr = MessageBox.Show(pjse.Localization.GetString("ttabForm_Sure"),
                    pjse.Localization.GetString("ttabForm_MultipleVaries"),
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (!DialogResult.OK.Equals(dr))
                    wrapper.Format = previousFormat;
                else
                    resetFormat();
            }


            this.tbUIDispType.Enabled = this.tbFaceAnimID.Enabled =
                this.tbModelTabID.Enabled = this.tbMemIterMult.Enabled = this.tbObjType.Enabled = false;


            this.tabControl1.TabPages.Remove(this.tpAnimalMotives);

            int index = 0;

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
                                this.tabControl1.TabPages.Add(this.tpAnimalMotives);
                                index = 1;
                            }
                        }
                    }
                }
            }
            this.tpHumanMotives.Text = ((String)this.tpHumanMotives.Tag).Split('/')[index];
            for (int i = 0; i < this.alFlags.Count; i++)
            {
                CheckBox lcb = (CheckBox)alFlags[i];
                if (lcb.Tag != null && lcb.Tag.ToString().Length > 0)
                    lcb.Text = ((String)lcb.Tag).Split('/')[index];
            }

            if (wrapper.Count > 0 && lbttab.Items.Count > siWas && lbttab.SelectedIndex == -1)
                lbttab.SelectedIndex = siWas;
        }

        /// <summary>
        /// Add the ith TtabItem to the lbttab listbox
        /// </summary>
        /// <param name="i">index of TtabItem to add</param>
        private void addItem(int i)
        {
            lbttab.Items.Add(lbttabItem(i));
        }

        private String lbttabItem(int i)
        {
            if (wrapper[i] != null && wrapper[i].StringIndex < cbStringIndex.Items.Count)
                return (String)cbStringIndex.Items[(int)wrapper[i].StringIndex];
            else
                return "[0x" + i.ToString("X") + " (" + i + "): " + pjse.Localization.GetString("unk") + ": 0x" + SimPe.Helper.HexString(wrapper[i].StringIndex) + "]";
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
            currentItem.StringIndex = si;
            lbttab.Items[lbttab.SelectedIndex] = lbPieString.Text = lbttabItem(lbttab.SelectedIndex);

            if (doText) tbStringIndex.Text = "0x" + Helper.HexString(si);
			if (doCB)
			{
                if (si >= 0 && si < cbStringIndex.Items.Count)
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

            if (booby.PrettyGirls.PervyMode && Helper.StartedGui == Executable.Default && this.ttabPanel.BackgroundImage == null)
                this.ttabPanel.BackgroundImage = booby.PrettyGirls.Sorrowful;

            internalchg = true;
            populateLbttab();
            internalchg = false;

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
            }
            else if (sender is List<TtabItem>)
                populateLbttab();
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
            this.ttabPanel = new booby.gradientpanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbttab = new System.Windows.Forms.ListBox();
            this.flpFileCtrl = new System.Windows.Forms.FlowLayoutPanel();
            this.lbFilename = new System.Windows.Forms.Label();
            this.tbFilename = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.tbFormat = new System.Windows.Forms.TextBox();
            this.btnCommit = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.btnStrPrev = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnStrNext = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAppend = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.tlpSettingsHead = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.lbTTABEntry = new System.Windows.Forms.Label();
            this.llGuardian = new System.Windows.Forms.LinkLabel();
            this.label40 = new System.Windows.Forms.Label();
            this.llAction = new System.Windows.Forms.LinkLabel();
            this.flpPieStringID = new System.Windows.Forms.FlowLayoutPanel();
            this.tbStringIndex = new System.Windows.Forms.TextBox();
            this.cbStringIndex = new System.Windows.Forms.ComboBox();
            this.lbPieString = new System.Windows.Forms.Label();
            this.flpAction = new System.Windows.Forms.FlowLayoutPanel();
            this.tbAction = new System.Windows.Forms.TextBox();
            this.btnAction = new System.Windows.Forms.Button();
            this.lbaction = new System.Windows.Forms.Label();
            this.flpGuard = new System.Windows.Forms.FlowLayoutPanel();
            this.tbGuardian = new System.Windows.Forms.TextBox();
            this.btnGuardian = new System.Windows.Forms.Button();
            this.lbguard = new System.Windows.Forms.Label();
            this.gbFlags2 = new System.Windows.Forms.GroupBox();
            this.tbFlags2 = new System.Windows.Forms.TextBox();
            this.btnNoFlags2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cb2Bit0 = new System.Windows.Forms.CheckBox();
            this.cb2BitE = new System.Windows.Forms.CheckBox();
            this.cb2BitF = new System.Windows.Forms.CheckBox();
            this.cb2BitC = new System.Windows.Forms.CheckBox();
            this.cb2BitD = new System.Windows.Forms.CheckBox();
            this.cb2BitB = new System.Windows.Forms.CheckBox();
            this.cb2BitA = new System.Windows.Forms.CheckBox();
            this.cb2Bit9 = new System.Windows.Forms.CheckBox();
            this.cb2Bit8 = new System.Windows.Forms.CheckBox();
            this.cb2Bit7 = new System.Windows.Forms.CheckBox();
            this.cb2Bit6 = new System.Windows.Forms.CheckBox();
            this.cb2Bit5 = new System.Windows.Forms.CheckBox();
            this.cb2Bit4 = new System.Windows.Forms.CheckBox();
            this.cb2Bit3 = new System.Windows.Forms.CheckBox();
            this.cb2Bit2 = new System.Windows.Forms.CheckBox();
            this.cb2Bit1 = new System.Windows.Forms.CheckBox();
            this.cbAttenuationCode = new System.Windows.Forms.ComboBox();
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbJoinIndex = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tpHumanMotives = new System.Windows.Forms.TabPage();
            this.timtuiHuman = new SimPe.PackedFiles.UserInterface.TtabItemMotiveTableUI();
            this.tpAnimalMotives = new System.Windows.Forms.TabPage();
            this.timtuiAnimal = new SimPe.PackedFiles.UserInterface.TtabItemMotiveTableUI();
            this.pjse_banner1 = new pjse.pjse_banner();
            this.ttabPanel.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flpFileCtrl.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpSettings.SuspendLayout();
            this.tlpSettingsHead.SuspendLayout();
            this.flpPieStringID.SuspendLayout();
            this.flpAction.SuspendLayout();
            this.flpGuard.SuspendLayout();
            this.gbFlags2.SuspendLayout();
            this.gbFlags.SuspendLayout();
            this.tpHumanMotives.SuspendLayout();
            this.tpAnimalMotives.SuspendLayout();
            this.SuspendLayout();
            // 
            // ttabPanel
            // 
            this.ttabPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ttabPanel.AutoScroll = true;
            this.ttabPanel.BackgroundImageAnchor = booby.gradientpanel.ImageLayout.BottomLeft;
            this.ttabPanel.BackgroundImageLocation = new System.Drawing.Point(900, 0);
            this.ttabPanel.BackgroundImageZoomToFit = true;
            this.ttabPanel.Controls.Add(this.splitContainer1);
            this.ttabPanel.Controls.Add(this.pjse_banner1);
            this.ttabPanel.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.ttabPanel.Location = new System.Drawing.Point(0, 0);
            this.ttabPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ttabPanel.Name = "ttabPanel";
            this.ttabPanel.Size = new System.Drawing.Size(984, 506);
            this.ttabPanel.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(984, 479);
            this.splitContainer1.SplitterDistance = 291;
            this.splitContainer1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lbttab, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flpFileCtrl, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(291, 476);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // lbttab
            // 
            this.lbttab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbttab.HorizontalScrollbar = true;
            this.lbttab.IntegralHeight = false;
            this.lbttab.Location = new System.Drawing.Point(0, 130);
            this.lbttab.Margin = new System.Windows.Forms.Padding(0);
            this.lbttab.Name = "lbttab";
            this.lbttab.Size = new System.Drawing.Size(291, 346);
            this.lbttab.TabIndex = 1;
            this.lbttab.SelectedIndexChanged += new System.EventHandler(this.TtabSelect);
            // 
            // flpFileCtrl
            // 
            this.flpFileCtrl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flpFileCtrl.AutoSize = true;
            this.flpFileCtrl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpFileCtrl.Controls.Add(this.lbFilename);
            this.flpFileCtrl.Controls.Add(this.tbFilename);
            this.flpFileCtrl.Controls.Add(this.label41);
            this.flpFileCtrl.Controls.Add(this.tbFormat);
            this.flpFileCtrl.Controls.Add(this.btnCommit);
            this.flpFileCtrl.Controls.Add(this.label26);
            this.flpFileCtrl.Controls.Add(this.btnStrPrev);
            this.flpFileCtrl.Controls.Add(this.btnMoveUp);
            this.flpFileCtrl.Controls.Add(this.btnAdd);
            this.flpFileCtrl.Controls.Add(this.btnStrNext);
            this.flpFileCtrl.Controls.Add(this.btnMoveDown);
            this.flpFileCtrl.Controls.Add(this.btnDelete);
            this.flpFileCtrl.Controls.Add(this.btnAppend);
            this.flpFileCtrl.Location = new System.Drawing.Point(0, 0);
            this.flpFileCtrl.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.flpFileCtrl.Name = "flpFileCtrl";
            this.flpFileCtrl.Size = new System.Drawing.Size(288, 126);
            this.flpFileCtrl.TabIndex = 2;
            // 
            // lbFilename
            // 
            this.lbFilename.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbFilename.AutoSize = true;
            this.flpFileCtrl.SetFlowBreak(this.lbFilename, true);
            this.lbFilename.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbFilename.Location = new System.Drawing.Point(0, 0);
            this.lbFilename.Margin = new System.Windows.Forms.Padding(0);
            this.lbFilename.Name = "lbFilename";
            this.lbFilename.Size = new System.Drawing.Size(63, 13);
            this.lbFilename.TabIndex = 8;
            this.lbFilename.Text = "Filename:";
            this.lbFilename.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbFilename
            // 
            this.tbFilename.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flpFileCtrl.SetFlowBreak(this.tbFilename, true);
            this.tbFilename.Location = new System.Drawing.Point(0, 13);
            this.tbFilename.Margin = new System.Windows.Forms.Padding(0);
            this.tbFilename.MaxLength = 64;
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.Size = new System.Drawing.Size(241, 21);
            this.tbFilename.TabIndex = 9;
            this.tbFilename.Text = "ffffff";
            this.tbFilename.TextChanged += new System.EventHandler(this.tbFilename_TextChanged);
            this.tbFilename.Validated += new System.EventHandler(this.tbFilename_Validated);
            // 
            // label41
            // 
            this.label41.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label41.AutoSize = true;
            this.label41.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label41.Location = new System.Drawing.Point(0, 38);
            this.label41.Margin = new System.Windows.Forms.Padding(0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(47, 13);
            this.label41.TabIndex = 10;
            this.label41.Text = "Format";
            // 
            // tbFormat
            // 
            this.tbFormat.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbFormat.Location = new System.Drawing.Point(47, 34);
            this.tbFormat.Margin = new System.Windows.Forms.Padding(0);
            this.tbFormat.MaxLength = 10;
            this.tbFormat.Name = "tbFormat";
            this.tbFormat.Size = new System.Drawing.Size(82, 21);
            this.tbFormat.TabIndex = 11;
            this.tbFormat.Text = "0xDDDDDDDD";
            this.tbFormat.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            this.tbFormat.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbFormat.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            // 
            // btnCommit
            // 
            this.btnCommit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flpFileCtrl.SetFlowBreak(this.btnCommit, true);
            this.btnCommit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCommit.Location = new System.Drawing.Point(149, 34);
            this.btnCommit.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(86, 22);
            this.btnCommit.TabIndex = 12;
            this.btnCommit.Text = "Commit &File";
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.flpFileCtrl.SetFlowBreak(this.label26, true);
            this.label26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label26.Location = new System.Drawing.Point(0, 64);
            this.label26.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(80, 13);
            this.label26.TabIndex = 112;
            this.label26.Text = "Interactions:";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStrPrev
            // 
            this.btnStrPrev.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnStrPrev.Font = new System.Drawing.Font("Wingdings", 8.25F);
            this.btnStrPrev.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrPrev.Location = new System.Drawing.Point(8, 82);
            this.btnStrPrev.Margin = new System.Windows.Forms.Padding(8, 2, 0, 0);
            this.btnStrPrev.Name = "btnStrPrev";
            this.btnStrPrev.Size = new System.Drawing.Size(17, 22);
            this.btnStrPrev.TabIndex = 1;
            this.btnStrPrev.Text = "á         &Up";
            this.btnStrPrev.Click += new System.EventHandler(this.btnStrPrev_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnMoveUp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMoveUp.Location = new System.Drawing.Point(28, 81);
            this.btnMoveUp.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(75, 22);
            this.btnMoveUp.TabIndex = 3;
            this.btnMoveUp.Text = "Move Up";
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flpFileCtrl.SetFlowBreak(this.btnAdd, true);
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(106, 81);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(38, 22);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "&Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnStrNext
            // 
            this.btnStrNext.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnStrNext.Font = new System.Drawing.Font("Wingdings", 8.25F);
            this.btnStrNext.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrNext.Location = new System.Drawing.Point(8, 104);
            this.btnStrNext.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnStrNext.Name = "btnStrNext";
            this.btnStrNext.Size = new System.Drawing.Size(17, 22);
            this.btnStrNext.TabIndex = 2;
            this.btnStrNext.Text = "â         &Down";
            this.btnStrNext.Click += new System.EventHandler(this.btnStrNext_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnMoveDown.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMoveDown.Location = new System.Drawing.Point(28, 104);
            this.btnMoveDown.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(75, 22);
            this.btnMoveDown.TabIndex = 4;
            this.btnMoveDown.Text = "Move Down";
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDelete.Location = new System.Drawing.Point(106, 104);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(51, 22);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "De&lete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAppend
            // 
            this.btnAppend.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAppend.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAppend.Location = new System.Drawing.Point(163, 104);
            this.btnAppend.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnAppend.Name = "btnAppend";
            this.btnAppend.Size = new System.Drawing.Size(59, 22);
            this.btnAppend.TabIndex = 7;
            this.btnAppend.Text = "A&ppend";
            this.btnAppend.Click += new System.EventHandler(this.btnAppend_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Controls.Add(this.tpSettings);
            this.tabControl1.Controls.Add(this.tpHumanMotives);
            this.tabControl1.Controls.Add(this.tpAnimalMotives);
            this.tabControl1.ItemSize = new System.Drawing.Size(58, 18);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(558, 479);
            this.tabControl1.TabIndex = 1;
            // 
            // tpSettings
            // 
            this.tpSettings.AutoScroll = true;
            this.tpSettings.Controls.Add(this.tlpSettingsHead);
            this.tpSettings.Controls.Add(this.gbFlags2);
            this.tpSettings.Controls.Add(this.cbAttenuationCode);
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
            this.tpSettings.Controls.Add(this.gbFlags);
            this.tpSettings.Controls.Add(this.label1);
            this.tpSettings.Controls.Add(this.tbJoinIndex);
            this.tpSettings.Controls.Add(this.label2);
            this.tpSettings.Location = new System.Drawing.Point(4, 22);
            this.tpSettings.Margin = new System.Windows.Forms.Padding(0);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Size = new System.Drawing.Size(550, 453);
            this.tpSettings.TabIndex = 0;
            this.tpSettings.Text = "Settings";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // tlpSettingsHead
            // 
            this.tlpSettingsHead.AutoSize = true;
            this.tlpSettingsHead.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpSettingsHead.ColumnCount = 2;
            this.tlpSettingsHead.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpSettingsHead.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpSettingsHead.Controls.Add(this.label4, 0, 0);
            this.tlpSettingsHead.Controls.Add(this.lbTTABEntry, 1, 0);
            this.tlpSettingsHead.Controls.Add(this.llGuardian, 0, 3);
            this.tlpSettingsHead.Controls.Add(this.label40, 0, 1);
            this.tlpSettingsHead.Controls.Add(this.llAction, 0, 2);
            this.tlpSettingsHead.Controls.Add(this.flpPieStringID, 1, 1);
            this.tlpSettingsHead.Controls.Add(this.flpAction, 1, 2);
            this.tlpSettingsHead.Controls.Add(this.flpGuard, 1, 3);
            this.tlpSettingsHead.Location = new System.Drawing.Point(8, 2);
            this.tlpSettingsHead.Margin = new System.Windows.Forms.Padding(0);
            this.tlpSettingsHead.Name = "tlpSettingsHead";
            this.tlpSettingsHead.RowCount = 4;
            this.tlpSettingsHead.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSettingsHead.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSettingsHead.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSettingsHead.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSettingsHead.Size = new System.Drawing.Size(223, 88);
            this.tlpSettingsHead.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(29, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "TTAB Entry";
            // 
            // lbTTABEntry
            // 
            this.lbTTABEntry.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbTTABEntry.AutoSize = true;
            this.lbTTABEntry.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbTTABEntry.Location = new System.Drawing.Point(99, 6);
            this.lbTTABEntry.Margin = new System.Windows.Forms.Padding(0);
            this.lbTTABEntry.Name = "lbTTABEntry";
            this.lbTTABEntry.Size = new System.Drawing.Size(28, 13);
            this.lbTTABEntry.TabIndex = 2;
            this.lbTTABEntry.Text = "0x0";
            // 
            // llGuardian
            // 
            this.llGuardian.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.llGuardian.AutoSize = true;
            this.llGuardian.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llGuardian.LinkArea = new System.Windows.Forms.LinkArea(0, 13);
            this.llGuardian.Location = new System.Drawing.Point(2, 71);
            this.llGuardian.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.llGuardian.Name = "llGuardian";
            this.llGuardian.Size = new System.Drawing.Size(95, 13);
            this.llGuardian.TabIndex = 7;
            this.llGuardian.TabStop = true;
            this.llGuardian.Text = "Guardian BHAV";
            this.llGuardian.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.llGuardian.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llBhav_LinkClicked);
            // 
            // label40
            // 
            this.label40.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label40.AutoSize = true;
            this.label40.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label40.Location = new System.Drawing.Point(17, 29);
            this.label40.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(80, 13);
            this.label40.TabIndex = 3;
            this.label40.Text = "Pie String ID";
            // 
            // llAction
            // 
            this.llAction.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.llAction.AutoSize = true;
            this.llAction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llAction.LinkArea = new System.Windows.Forms.LinkArea(0, 11);
            this.llAction.Location = new System.Drawing.Point(19, 50);
            this.llAction.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.llAction.Name = "llAction";
            this.llAction.Size = new System.Drawing.Size(78, 13);
            this.llAction.TabIndex = 5;
            this.llAction.TabStop = true;
            this.llAction.Text = "Action BHAV";
            this.llAction.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.llAction.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llBhav_LinkClicked);
            // 
            // flpPieStringID
            // 
            this.flpPieStringID.AutoSize = true;
            this.flpPieStringID.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpPieStringID.Controls.Add(this.tbStringIndex);
            this.flpPieStringID.Controls.Add(this.cbStringIndex);
            this.flpPieStringID.Controls.Add(this.lbPieString);
            this.flpPieStringID.Location = new System.Drawing.Point(99, 25);
            this.flpPieStringID.Margin = new System.Windows.Forms.Padding(0);
            this.flpPieStringID.Name = "flpPieStringID";
            this.flpPieStringID.Size = new System.Drawing.Size(124, 21);
            this.flpPieStringID.TabIndex = 4;
            // 
            // tbStringIndex
            // 
            this.tbStringIndex.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbStringIndex.Location = new System.Drawing.Point(0, 0);
            this.tbStringIndex.Margin = new System.Windows.Forms.Padding(0);
            this.tbStringIndex.MaxLength = 10;
            this.tbStringIndex.Name = "tbStringIndex";
            this.tbStringIndex.Size = new System.Drawing.Size(82, 21);
            this.tbStringIndex.TabIndex = 1;
            this.tbStringIndex.Text = "0xDDDDDDDD";
            this.tbStringIndex.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            this.tbStringIndex.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbStringIndex.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            // 
            // cbStringIndex
            // 
            this.cbStringIndex.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbStringIndex.DisplayMember = "Display";
            this.cbStringIndex.DropDownWidth = 240;
            this.cbStringIndex.ItemHeight = 13;
            this.cbStringIndex.Items.AddRange(new object[] {
            "0x00000000 Zeroth string",
            "0x00000001 Oneth string",
            "0x00000002 Twoth string"});
            this.cbStringIndex.Location = new System.Drawing.Point(82, 0);
            this.cbStringIndex.Margin = new System.Windows.Forms.Padding(0);
            this.cbStringIndex.MaxDropDownItems = 32;
            this.cbStringIndex.MaxLength = 10;
            this.cbStringIndex.Name = "cbStringIndex";
            this.cbStringIndex.Size = new System.Drawing.Size(20, 21);
            this.cbStringIndex.TabIndex = 2;
            this.cbStringIndex.TabStop = false;
            this.cbStringIndex.Text = "0xDDDDDDDD";
            this.cbStringIndex.ValueMember = "Value";
            this.cbStringIndex.Validating += new System.ComponentModel.CancelEventHandler(this.cbHex32_Validating);
            this.cbStringIndex.SelectedIndexChanged += new System.EventHandler(this.cbHex32_SelectedIndexChanged);
            this.cbStringIndex.Enter += new System.EventHandler(this.cbHex32_Enter);
            this.cbStringIndex.Validated += new System.EventHandler(this.cbHex32_Validated);
            this.cbStringIndex.TextChanged += new System.EventHandler(this.cbHex32_TextChanged);
            // 
            // lbPieString
            // 
            this.lbPieString.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbPieString.AutoSize = true;
            this.lbPieString.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbPieString.Location = new System.Drawing.Point(102, 4);
            this.lbPieString.Margin = new System.Windows.Forms.Padding(0);
            this.lbPieString.Name = "lbPieString";
            this.lbPieString.Size = new System.Drawing.Size(22, 13);
            this.lbPieString.TabIndex = 3;
            this.lbPieString.Text = "---";
            this.lbPieString.UseMnemonic = false;
            // 
            // flpAction
            // 
            this.flpAction.AutoSize = true;
            this.flpAction.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpAction.Controls.Add(this.tbAction);
            this.flpAction.Controls.Add(this.btnAction);
            this.flpAction.Controls.Add(this.lbaction);
            this.flpAction.Location = new System.Drawing.Point(99, 46);
            this.flpAction.Margin = new System.Windows.Forms.Padding(0);
            this.flpAction.Name = "flpAction";
            this.flpAction.Size = new System.Drawing.Size(89, 21);
            this.flpAction.TabIndex = 6;
            // 
            // tbAction
            // 
            this.tbAction.Location = new System.Drawing.Point(0, 0);
            this.tbAction.Margin = new System.Windows.Forms.Padding(0);
            this.tbAction.MaxLength = 6;
            this.tbAction.Name = "tbAction";
            this.tbAction.Size = new System.Drawing.Size(50, 21);
            this.tbAction.TabIndex = 4;
            this.tbAction.Text = "0xDDDD";
            this.tbAction.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbAction.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbAction.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // btnAction
            // 
            this.btnAction.Font = new System.Drawing.Font("Webdings", 12F);
            this.btnAction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAction.Location = new System.Drawing.Point(50, 0);
            this.btnAction.Margin = new System.Windows.Forms.Padding(0);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(17, 19);
            this.btnAction.TabIndex = 5;
            this.btnAction.Text = "4";
            this.btnAction.Click += new System.EventHandler(this.GetTTABAction);
            // 
            // lbaction
            // 
            this.lbaction.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbaction.AutoSize = true;
            this.lbaction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbaction.Location = new System.Drawing.Point(67, 4);
            this.lbaction.Margin = new System.Windows.Forms.Padding(0);
            this.lbaction.Name = "lbaction";
            this.lbaction.Size = new System.Drawing.Size(22, 13);
            this.lbaction.TabIndex = 0;
            this.lbaction.Text = "---";
            this.lbaction.UseMnemonic = false;
            // 
            // flpGuard
            // 
            this.flpGuard.AutoSize = true;
            this.flpGuard.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpGuard.Controls.Add(this.tbGuardian);
            this.flpGuard.Controls.Add(this.btnGuardian);
            this.flpGuard.Controls.Add(this.lbguard);
            this.flpGuard.Location = new System.Drawing.Point(99, 67);
            this.flpGuard.Margin = new System.Windows.Forms.Padding(0);
            this.flpGuard.Name = "flpGuard";
            this.flpGuard.Size = new System.Drawing.Size(89, 21);
            this.flpGuard.TabIndex = 8;
            // 
            // tbGuardian
            // 
            this.tbGuardian.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbGuardian.Location = new System.Drawing.Point(0, 0);
            this.tbGuardian.Margin = new System.Windows.Forms.Padding(0);
            this.tbGuardian.MaxLength = 6;
            this.tbGuardian.Name = "tbGuardian";
            this.tbGuardian.Size = new System.Drawing.Size(50, 21);
            this.tbGuardian.TabIndex = 7;
            this.tbGuardian.Text = "0xDDDD";
            this.tbGuardian.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbGuardian.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbGuardian.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // btnGuardian
            // 
            this.btnGuardian.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnGuardian.Font = new System.Drawing.Font("Webdings", 12F);
            this.btnGuardian.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGuardian.Location = new System.Drawing.Point(50, 1);
            this.btnGuardian.Margin = new System.Windows.Forms.Padding(0);
            this.btnGuardian.Name = "btnGuardian";
            this.btnGuardian.Size = new System.Drawing.Size(17, 19);
            this.btnGuardian.TabIndex = 8;
            this.btnGuardian.Text = "4";
            this.btnGuardian.Click += new System.EventHandler(this.GetTTABGuard);
            // 
            // lbguard
            // 
            this.lbguard.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbguard.AutoSize = true;
            this.lbguard.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbguard.Location = new System.Drawing.Point(67, 4);
            this.lbguard.Margin = new System.Windows.Forms.Padding(0);
            this.lbguard.Name = "lbguard";
            this.lbguard.Size = new System.Drawing.Size(22, 13);
            this.lbguard.TabIndex = 0;
            this.lbguard.Text = "---";
            this.lbguard.UseMnemonic = false;
            // 
            // gbFlags2
            // 
            this.gbFlags2.Controls.Add(this.tbFlags2);
            this.gbFlags2.Controls.Add(this.btnNoFlags2);
            this.gbFlags2.Controls.Add(this.label3);
            this.gbFlags2.Controls.Add(this.cb2Bit0);
            this.gbFlags2.Controls.Add(this.cb2BitE);
            this.gbFlags2.Controls.Add(this.cb2BitF);
            this.gbFlags2.Controls.Add(this.cb2BitC);
            this.gbFlags2.Controls.Add(this.cb2BitD);
            this.gbFlags2.Controls.Add(this.cb2BitB);
            this.gbFlags2.Controls.Add(this.cb2BitA);
            this.gbFlags2.Controls.Add(this.cb2Bit9);
            this.gbFlags2.Controls.Add(this.cb2Bit8);
            this.gbFlags2.Controls.Add(this.cb2Bit7);
            this.gbFlags2.Controls.Add(this.cb2Bit6);
            this.gbFlags2.Controls.Add(this.cb2Bit5);
            this.gbFlags2.Controls.Add(this.cb2Bit4);
            this.gbFlags2.Controls.Add(this.cb2Bit3);
            this.gbFlags2.Controls.Add(this.cb2Bit2);
            this.gbFlags2.Controls.Add(this.cb2Bit1);
            this.gbFlags2.Location = new System.Drawing.Point(247, 108);
            this.gbFlags2.Margin = new System.Windows.Forms.Padding(0);
            this.gbFlags2.Name = "gbFlags2";
            this.gbFlags2.Padding = new System.Windows.Forms.Padding(2);
            this.gbFlags2.Size = new System.Drawing.Size(212, 201);
            this.gbFlags2.TabIndex = 3;
            this.gbFlags2.TabStop = false;
            this.gbFlags2.Text = "Flags2";
            // 
            // tbFlags2
            // 
            this.tbFlags2.Location = new System.Drawing.Point(45, 22);
            this.tbFlags2.Margin = new System.Windows.Forms.Padding(2);
            this.tbFlags2.MaxLength = 6;
            this.tbFlags2.Name = "tbFlags2";
            this.tbFlags2.Size = new System.Drawing.Size(64, 21);
            this.tbFlags2.TabIndex = 1;
            this.tbFlags2.Text = "0xDDDD";
            this.tbFlags2.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbFlags2.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbFlags2.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // btnNoFlags2
            // 
            this.btnNoFlags2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnNoFlags2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNoFlags2.Location = new System.Drawing.Point(113, 20);
            this.btnNoFlags2.Margin = new System.Windows.Forms.Padding(2);
            this.btnNoFlags2.Name = "btnNoFlags2";
            this.btnNoFlags2.Size = new System.Drawing.Size(46, 22);
            this.btnNoFlags2.TabIndex = 0;
            this.btnNoFlags2.Text = "&None";
            this.btnNoFlags2.Click += new System.EventHandler(this.btnNoFlags2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Value";
            // 
            // cb2Bit0
            // 
            this.cb2Bit0.AutoSize = true;
            this.cb2Bit0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb2Bit0.Location = new System.Drawing.Point(4, 44);
            this.cb2Bit0.Margin = new System.Windows.Forms.Padding(2);
            this.cb2Bit0.Name = "cb2Bit0";
            this.cb2Bit0.Size = new System.Drawing.Size(63, 17);
            this.cb2Bit0.TabIndex = 2;
            this.cb2Bit0.Tag = "";
            this.cb2Bit0.Text = "babies";
            this.cb2Bit0.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cb2BitE
            // 
            this.cb2BitE.AutoSize = true;
            this.cb2BitE.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb2BitE.Location = new System.Drawing.Point(90, 159);
            this.cb2BitE.Margin = new System.Windows.Forms.Padding(2);
            this.cb2BitE.Name = "cb2BitE";
            this.cb2BitE.Size = new System.Drawing.Size(32, 17);
            this.cb2BitE.TabIndex = 16;
            this.cb2BitE.Tag = "";
            this.cb2BitE.Text = "?";
            this.cb2BitE.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cb2BitF
            // 
            this.cb2BitF.AutoSize = true;
            this.cb2BitF.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb2BitF.Location = new System.Drawing.Point(90, 178);
            this.cb2BitF.Margin = new System.Windows.Forms.Padding(2);
            this.cb2BitF.Name = "cb2BitF";
            this.cb2BitF.Size = new System.Drawing.Size(32, 17);
            this.cb2BitF.TabIndex = 17;
            this.cb2BitF.Tag = "";
            this.cb2BitF.Text = "?";
            this.cb2BitF.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cb2BitC
            // 
            this.cb2BitC.AutoSize = true;
            this.cb2BitC.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb2BitC.Location = new System.Drawing.Point(90, 121);
            this.cb2BitC.Margin = new System.Windows.Forms.Padding(2);
            this.cb2BitC.Name = "cb2BitC";
            this.cb2BitC.Size = new System.Drawing.Size(119, 17);
            this.cb2BitC.TabIndex = 14;
            this.cb2BitC.Tag = "?/adult small dogs";
            this.cb2BitC.Text = "adult small dogs";
            this.cb2BitC.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cb2BitD
            // 
            this.cb2BitD.AutoSize = true;
            this.cb2BitD.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb2BitD.Location = new System.Drawing.Point(90, 140);
            this.cb2BitD.Margin = new System.Windows.Forms.Padding(2);
            this.cb2BitD.Name = "cb2BitD";
            this.cb2BitD.Size = new System.Drawing.Size(120, 17);
            this.cb2BitD.TabIndex = 15;
            this.cb2BitD.Tag = "?/elder small dogs";
            this.cb2BitD.Text = "elder small dogs";
            this.cb2BitD.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cb2BitB
            // 
            this.cb2BitB.AutoSize = true;
            this.cb2BitB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb2BitB.Location = new System.Drawing.Point(90, 102);
            this.cb2BitB.Margin = new System.Windows.Forms.Padding(2);
            this.cb2BitB.Name = "cb2BitB";
            this.cb2BitB.Size = new System.Drawing.Size(82, 17);
            this.cb2BitB.TabIndex = 13;
            this.cb2BitB.Tag = "?/elder cats";
            this.cb2BitB.Text = "elder cats";
            this.cb2BitB.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cb2BitA
            // 
            this.cb2BitA.AutoSize = true;
            this.cb2BitA.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb2BitA.Location = new System.Drawing.Point(90, 84);
            this.cb2BitA.Margin = new System.Windows.Forms.Padding(2);
            this.cb2BitA.Name = "cb2BitA";
            this.cb2BitA.Size = new System.Drawing.Size(107, 17);
            this.cb2BitA.TabIndex = 12;
            this.cb2BitA.Tag = "?/elder big dogs";
            this.cb2BitA.Text = "elder big dogs";
            this.cb2BitA.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cb2Bit9
            // 
            this.cb2Bit9.AutoSize = true;
            this.cb2Bit9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb2Bit9.Location = new System.Drawing.Point(90, 65);
            this.cb2Bit9.Margin = new System.Windows.Forms.Padding(2);
            this.cb2Bit9.Name = "cb2Bit9";
            this.cb2Bit9.Size = new System.Drawing.Size(64, 17);
            this.cb2Bit9.TabIndex = 11;
            this.cb2Bit9.Tag = "?/kittens";
            this.cb2Bit9.Text = "kittens";
            this.cb2Bit9.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cb2Bit8
            // 
            this.cb2Bit8.AutoSize = true;
            this.cb2Bit8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb2Bit8.Location = new System.Drawing.Point(90, 44);
            this.cb2Bit8.Margin = new System.Windows.Forms.Padding(2);
            this.cb2Bit8.Name = "cb2Bit8";
            this.cb2Bit8.Size = new System.Drawing.Size(70, 17);
            this.cb2Bit8.TabIndex = 10;
            this.cb2Bit8.Tag = "?/puppies";
            this.cb2Bit8.Text = "puppies";
            this.cb2Bit8.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cb2Bit7
            // 
            this.cb2Bit7.AutoSize = true;
            this.cb2Bit7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb2Bit7.Location = new System.Drawing.Point(4, 178);
            this.cb2Bit7.Margin = new System.Windows.Forms.Padding(2);
            this.cb2Bit7.Name = "cb2Bit7";
            this.cb2Bit7.Size = new System.Drawing.Size(32, 17);
            this.cb2Bit7.TabIndex = 9;
            this.cb2Bit7.Tag = "";
            this.cb2Bit7.Text = "?";
            this.cb2Bit7.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cb2Bit6
            // 
            this.cb2Bit6.AutoSize = true;
            this.cb2Bit6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb2Bit6.Location = new System.Drawing.Point(4, 159);
            this.cb2Bit6.Margin = new System.Windows.Forms.Padding(2);
            this.cb2Bit6.Name = "cb2Bit6";
            this.cb2Bit6.Size = new System.Drawing.Size(32, 17);
            this.cb2Bit6.TabIndex = 8;
            this.cb2Bit6.Tag = "";
            this.cb2Bit6.Text = "?";
            this.cb2Bit6.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cb2Bit5
            // 
            this.cb2Bit5.AutoSize = true;
            this.cb2Bit5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb2Bit5.Location = new System.Drawing.Point(4, 140);
            this.cb2Bit5.Margin = new System.Windows.Forms.Padding(2);
            this.cb2Bit5.Name = "cb2Bit5";
            this.cb2Bit5.Size = new System.Drawing.Size(32, 17);
            this.cb2Bit5.TabIndex = 7;
            this.cb2Bit5.Tag = "";
            this.cb2Bit5.Text = "?";
            this.cb2Bit5.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cb2Bit4
            // 
            this.cb2Bit4.AutoSize = true;
            this.cb2Bit4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb2Bit4.Location = new System.Drawing.Point(4, 121);
            this.cb2Bit4.Margin = new System.Windows.Forms.Padding(2);
            this.cb2Bit4.Name = "cb2Bit4";
            this.cb2Bit4.Size = new System.Drawing.Size(32, 17);
            this.cb2Bit4.TabIndex = 6;
            this.cb2Bit4.Tag = "";
            this.cb2Bit4.Text = "?";
            this.cb2Bit4.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cb2Bit3
            // 
            this.cb2Bit3.AutoSize = true;
            this.cb2Bit3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb2Bit3.Location = new System.Drawing.Point(4, 102);
            this.cb2Bit3.Margin = new System.Windows.Forms.Padding(2);
            this.cb2Bit3.Name = "cb2Bit3";
            this.cb2Bit3.Size = new System.Drawing.Size(32, 17);
            this.cb2Bit3.TabIndex = 5;
            this.cb2Bit3.Tag = "";
            this.cb2Bit3.Text = "?";
            this.cb2Bit3.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cb2Bit2
            // 
            this.cb2Bit2.AutoSize = true;
            this.cb2Bit2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb2Bit2.Location = new System.Drawing.Point(4, 84);
            this.cb2Bit2.Margin = new System.Windows.Forms.Padding(2);
            this.cb2Bit2.Name = "cb2Bit2";
            this.cb2Bit2.Size = new System.Drawing.Size(32, 17);
            this.cb2Bit2.TabIndex = 4;
            this.cb2Bit2.Tag = "";
            this.cb2Bit2.Text = "?";
            this.cb2Bit2.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cb2Bit1
            // 
            this.cb2Bit1.AutoSize = true;
            this.cb2Bit1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb2Bit1.Location = new System.Drawing.Point(4, 65);
            this.cb2Bit1.Margin = new System.Windows.Forms.Padding(2);
            this.cb2Bit1.Name = "cb2Bit1";
            this.cb2Bit1.Size = new System.Drawing.Size(32, 17);
            this.cb2Bit1.TabIndex = 3;
            this.cb2Bit1.Tag = "";
            this.cb2Bit1.Text = "?";
            this.cb2Bit1.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbAttenuationCode
            // 
            this.cbAttenuationCode.ItemHeight = 13;
            this.cbAttenuationCode.Items.AddRange(new object[] {
            "Custom",
            "None",
            "Low",
            "Moderate",
            "High"});
            this.cbAttenuationCode.Location = new System.Drawing.Point(124, 318);
            this.cbAttenuationCode.Margin = new System.Windows.Forms.Padding(0);
            this.cbAttenuationCode.Name = "cbAttenuationCode";
            this.cbAttenuationCode.Size = new System.Drawing.Size(112, 21);
            this.cbAttenuationCode.TabIndex = 5;
            this.cbAttenuationCode.Text = "0xDDDDDDDD";
            this.cbAttenuationCode.Validating += new System.ComponentModel.CancelEventHandler(this.cbHex32_Validating);
            this.cbAttenuationCode.SelectedIndexChanged += new System.EventHandler(this.cbHex32_SelectedIndexChanged);
            this.cbAttenuationCode.Enter += new System.EventHandler(this.cbHex32_Enter);
            this.cbAttenuationCode.Validated += new System.EventHandler(this.cbHex32_Validated);
            this.cbAttenuationCode.TextChanged += new System.EventHandler(this.cbHex32_TextChanged);
            // 
            // tbModelTabID
            // 
            this.tbModelTabID.Location = new System.Drawing.Point(352, 394);
            this.tbModelTabID.Margin = new System.Windows.Forms.Padding(0);
            this.tbModelTabID.MaxLength = 10;
            this.tbModelTabID.Name = "tbModelTabID";
            this.tbModelTabID.Size = new System.Drawing.Size(91, 21);
            this.tbModelTabID.TabIndex = 19;
            this.tbModelTabID.Text = "0xDDDDDDDD";
            this.tbModelTabID.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            this.tbModelTabID.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbModelTabID.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label33.Location = new System.Drawing.Point(254, 397);
            this.label33.Margin = new System.Windows.Forms.Padding(0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(92, 13);
            this.label33.TabIndex = 18;
            this.label33.Text = "Model Table ID";
            // 
            // tbObjType
            // 
            this.tbObjType.Location = new System.Drawing.Point(352, 372);
            this.tbObjType.Margin = new System.Windows.Forms.Padding(0);
            this.tbObjType.MaxLength = 10;
            this.tbObjType.Name = "tbObjType";
            this.tbObjType.Size = new System.Drawing.Size(91, 21);
            this.tbObjType.TabIndex = 17;
            this.tbObjType.Text = "0xDDDDDDDD";
            this.tbObjType.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            this.tbObjType.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbObjType.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label34.Location = new System.Drawing.Point(268, 375);
            this.label34.Margin = new System.Windows.Forms.Padding(0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(75, 13);
            this.label34.TabIndex = 16;
            this.label34.Text = "Object Type";
            // 
            // tbUIDispType
            // 
            this.tbUIDispType.Location = new System.Drawing.Point(124, 372);
            this.tbUIDispType.Margin = new System.Windows.Forms.Padding(0);
            this.tbUIDispType.MaxLength = 6;
            this.tbUIDispType.Name = "tbUIDispType";
            this.tbUIDispType.Size = new System.Drawing.Size(50, 21);
            this.tbUIDispType.TabIndex = 13;
            this.tbUIDispType.Text = "0xDDDD";
            this.tbUIDispType.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbUIDispType.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbUIDispType.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label35.Location = new System.Drawing.Point(279, 343);
            this.label35.Margin = new System.Windows.Forms.Padding(0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(66, 13);
            this.label35.TabIndex = 10;
            this.label35.Text = "Join Index";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbAutonomy
            // 
            this.tbAutonomy.Location = new System.Drawing.Point(352, 318);
            this.tbAutonomy.Margin = new System.Windows.Forms.Padding(0);
            this.tbAutonomy.MaxLength = 10;
            this.tbAutonomy.Name = "tbAutonomy";
            this.tbAutonomy.Size = new System.Drawing.Size(91, 21);
            this.tbAutonomy.TabIndex = 9;
            this.tbAutonomy.Text = "0xDDDDDDDD";
            this.tbAutonomy.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            this.tbAutonomy.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbAutonomy.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            // 
            // tbMemIterMult
            // 
            this.tbMemIterMult.Location = new System.Drawing.Point(218, 423);
            this.tbMemIterMult.Margin = new System.Windows.Forms.Padding(0);
            this.tbMemIterMult.Name = "tbMemIterMult";
            this.tbMemIterMult.Size = new System.Drawing.Size(79, 21);
            this.tbMemIterMult.TabIndex = 21;
            this.tbMemIterMult.Text = "8.88888889";
            this.tbMemIterMult.TextChanged += new System.EventHandler(this.float_TextChanged);
            this.tbMemIterMult.Validated += new System.EventHandler(this.float_Validated);
            this.tbMemIterMult.Validating += new System.ComponentModel.CancelEventHandler(this.float_Validating);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label29.Location = new System.Drawing.Point(51, 426);
            this.label29.Margin = new System.Windows.Forms.Padding(0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(161, 13);
            this.label29.TabIndex = 20;
            this.label29.Text = "Memory Iterative Multiplier";
            // 
            // tbFaceAnimID
            // 
            this.tbFaceAnimID.Location = new System.Drawing.Point(124, 395);
            this.tbFaceAnimID.Margin = new System.Windows.Forms.Padding(0);
            this.tbFaceAnimID.MaxLength = 10;
            this.tbFaceAnimID.Name = "tbFaceAnimID";
            this.tbFaceAnimID.Size = new System.Drawing.Size(82, 21);
            this.tbFaceAnimID.TabIndex = 15;
            this.tbFaceAnimID.Text = "0xDDDDDDDD";
            this.tbFaceAnimID.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            this.tbFaceAnimID.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbFaceAnimID.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label30.Location = new System.Drawing.Point(28, 378);
            this.label30.Margin = new System.Windows.Forms.Padding(0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(97, 13);
            this.label30.TabIndex = 12;
            this.label30.Text = "UI Display Type";
            // 
            // tbAttenuationValue
            // 
            this.tbAttenuationValue.Location = new System.Drawing.Point(124, 340);
            this.tbAttenuationValue.Margin = new System.Windows.Forms.Padding(0);
            this.tbAttenuationValue.MaxLength = 10;
            this.tbAttenuationValue.Name = "tbAttenuationValue";
            this.tbAttenuationValue.Size = new System.Drawing.Size(82, 21);
            this.tbAttenuationValue.TabIndex = 7;
            this.tbAttenuationValue.Text = "8.88888889";
            this.tbAttenuationValue.TextChanged += new System.EventHandler(this.float_TextChanged);
            this.tbAttenuationValue.Validated += new System.EventHandler(this.float_Validated);
            this.tbAttenuationValue.Validating += new System.ComponentModel.CancelEventHandler(this.float_Validating);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label31.Location = new System.Drawing.Point(18, 346);
            this.label31.Margin = new System.Windows.Forms.Padding(0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(107, 13);
            this.label31.TabIndex = 6;
            this.label31.Text = "Attenuation Value";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label32.Location = new System.Drawing.Point(20, 323);
            this.label32.Margin = new System.Windows.Forms.Padding(0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(106, 13);
            this.label32.TabIndex = 4;
            this.label32.Text = "Attenuation Code";
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
            this.gbFlags.Location = new System.Drawing.Point(8, 108);
            this.gbFlags.Margin = new System.Windows.Forms.Padding(0);
            this.gbFlags.Name = "gbFlags";
            this.gbFlags.Padding = new System.Windows.Forms.Padding(2);
            this.gbFlags.Size = new System.Drawing.Size(228, 201);
            this.gbFlags.TabIndex = 2;
            this.gbFlags.TabStop = false;
            this.gbFlags.Text = "Flags";
            // 
            // btnNoFlags
            // 
            this.btnNoFlags.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnNoFlags.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNoFlags.Location = new System.Drawing.Point(116, 20);
            this.btnNoFlags.Margin = new System.Windows.Forms.Padding(2);
            this.btnNoFlags.Name = "btnNoFlags";
            this.btnNoFlags.Size = new System.Drawing.Size(46, 22);
            this.btnNoFlags.TabIndex = 0;
            this.btnNoFlags.Text = "&None";
            this.btnNoFlags.Click += new System.EventHandler(this.btnNoFlags_Click);
            // 
            // tbFlags
            // 
            this.tbFlags.Location = new System.Drawing.Point(48, 20);
            this.tbFlags.Margin = new System.Windows.Forms.Padding(2);
            this.tbFlags.MaxLength = 6;
            this.tbFlags.Name = "tbFlags";
            this.tbFlags.Size = new System.Drawing.Size(64, 21);
            this.tbFlags.TabIndex = 1;
            this.tbFlags.Text = "0xDDDD";
            this.tbFlags.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbFlags.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbFlags.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label24.Location = new System.Drawing.Point(6, 24);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(38, 13);
            this.label24.TabIndex = 0;
            this.label24.Text = "Value";
            // 
            // cbBit0
            // 
            this.cbBit0.AutoSize = true;
            this.cbBit0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbBit0.Location = new System.Drawing.Point(4, 44);
            this.cbBit0.Margin = new System.Windows.Forms.Padding(2);
            this.cbBit0.Name = "cbBit0";
            this.cbBit0.Size = new System.Drawing.Size(61, 17);
            this.cbBit0.TabIndex = 2;
            this.cbBit0.Tag = "";
            this.cbBit0.Text = "visitor";
            this.cbBit0.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBitE
            // 
            this.cbBitE.AutoSize = true;
            this.cbBitE.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbBitE.Location = new System.Drawing.Point(110, 159);
            this.cbBitE.Margin = new System.Windows.Forms.Padding(2);
            this.cbBitE.Name = "cbBitE";
            this.cbBitE.Size = new System.Drawing.Size(97, 17);
            this.cbBitE.TabIndex = 16;
            this.cbBitE.Text = "allow nested";
            this.cbBitE.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBitF
            // 
            this.cbBitF.AutoSize = true;
            this.cbBitF.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbBitF.Location = new System.Drawing.Point(110, 178);
            this.cbBitF.Margin = new System.Windows.Forms.Padding(2);
            this.cbBitF.Name = "cbBitF";
            this.cbBitF.Size = new System.Drawing.Size(50, 17);
            this.cbBitF.TabIndex = 17;
            this.cbBitF.Text = "nest";
            this.cbBitF.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBitC
            // 
            this.cbBitC.AutoSize = true;
            this.cbBitC.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbBitC.Location = new System.Drawing.Point(110, 121);
            this.cbBitC.Margin = new System.Windows.Forms.Padding(2);
            this.cbBitC.Name = "cbBitC";
            this.cbBitC.Size = new System.Drawing.Size(53, 17);
            this.cbBitC.TabIndex = 14;
            this.cbBitC.Tag = "dogs/adult big dogs";
            this.cbBitC.Text = "dogs";
            this.cbBitC.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBitD
            // 
            this.cbBitD.AutoSize = true;
            this.cbBitD.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbBitD.Location = new System.Drawing.Point(110, 140);
            this.cbBitD.Margin = new System.Windows.Forms.Padding(2);
            this.cbBitD.Name = "cbBitD";
            this.cbBitD.Size = new System.Drawing.Size(49, 17);
            this.cbBitD.TabIndex = 15;
            this.cbBitD.Tag = "cats/adult cats";
            this.cbBitD.Text = "cats";
            this.cbBitD.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBitB
            // 
            this.cbBitB.AutoSize = true;
            this.cbBitB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbBitB.Location = new System.Drawing.Point(110, 102);
            this.cbBitB.Margin = new System.Windows.Forms.Padding(2);
            this.cbBitB.Name = "cbBitB";
            this.cbBitB.Size = new System.Drawing.Size(57, 17);
            this.cbBitB.TabIndex = 13;
            this.cbBitB.Text = "teens";
            this.cbBitB.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBitA
            // 
            this.cbBitA.AutoSize = true;
            this.cbBitA.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbBitA.Location = new System.Drawing.Point(110, 84);
            this.cbBitA.Margin = new System.Windows.Forms.Padding(2);
            this.cbBitA.Name = "cbBitA";
            this.cbBitA.Size = new System.Drawing.Size(61, 17);
            this.cbBitA.TabIndex = 12;
            this.cbBitA.Text = "elders";
            this.cbBitA.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBit9
            // 
            this.cbBit9.AutoSize = true;
            this.cbBit9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbBit9.Location = new System.Drawing.Point(110, 65);
            this.cbBit9.Margin = new System.Windows.Forms.Padding(2);
            this.cbBit9.Name = "cbBit9";
            this.cbBit9.Size = new System.Drawing.Size(72, 17);
            this.cbBit9.TabIndex = 11;
            this.cbBit9.Text = "toddlers";
            this.cbBit9.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBit8
            // 
            this.cbBit8.AutoSize = true;
            this.cbBit8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbBit8.Location = new System.Drawing.Point(110, 44);
            this.cbBit8.Margin = new System.Windows.Forms.Padding(2);
            this.cbBit8.Name = "cbBit8";
            this.cbBit8.Size = new System.Drawing.Size(77, 17);
            this.cbBit8.TabIndex = 10;
            this.cbBit8.Tag = "auto first/auto first?";
            this.cbBit8.Text = "auto first";
            this.cbBit8.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBit7
            // 
            this.cbBit7.AutoSize = true;
            this.cbBit7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbBit7.Location = new System.Drawing.Point(4, 178);
            this.cbBit7.Margin = new System.Windows.Forms.Padding(2);
            this.cbBit7.Name = "cbBit7";
            this.cbBit7.Size = new System.Drawing.Size(97, 17);
            this.cbBit7.TabIndex = 9;
            this.cbBit7.Tag = "debug menu/debug menu?";
            this.cbBit7.Text = "debug menu";
            this.cbBit7.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBit6
            // 
            this.cbBit6.AutoSize = true;
            this.cbBit6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbBit6.Location = new System.Drawing.Point(4, 159);
            this.cbBit6.Margin = new System.Windows.Forms.Padding(2);
            this.cbBit6.Name = "cbBit6";
            this.cbBit6.Size = new System.Drawing.Size(60, 17);
            this.cbBit6.TabIndex = 8;
            this.cbBit6.Tag = "";
            this.cbBit6.Text = "adults";
            this.cbBit6.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBit5
            // 
            this.cbBit5.AutoSize = true;
            this.cbBit5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbBit5.Location = new System.Drawing.Point(4, 140);
            this.cbBit5.Margin = new System.Windows.Forms.Padding(2);
            this.cbBit5.Name = "cbBit5";
            this.cbBit5.Size = new System.Drawing.Size(88, 17);
            this.cbBit5.TabIndex = 7;
            this.cbBit5.Tag = "demo child/2-way?";
            this.cbBit5.Text = "demo child";
            this.cbBit5.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBit4
            // 
            this.cbBit4.AutoSize = true;
            this.cbBit4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbBit4.Location = new System.Drawing.Point(4, 121);
            this.cbBit4.Margin = new System.Windows.Forms.Padding(2);
            this.cbBit4.Name = "cbBit4";
            this.cbBit4.Size = new System.Drawing.Size(71, 17);
            this.cbBit4.TabIndex = 6;
            this.cbBit4.Text = "children";
            this.cbBit4.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBit3
            // 
            this.cbBit3.AutoSize = true;
            this.cbBit3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbBit3.Location = new System.Drawing.Point(4, 102);
            this.cbBit3.Margin = new System.Windows.Forms.Padding(2);
            this.cbBit3.Name = "cbBit3";
            this.cbBit3.Size = new System.Drawing.Size(93, 17);
            this.cbBit3.TabIndex = 5;
            this.cbBit3.Tag = "consecutive/consecutive?";
            this.cbBit3.Text = "consecutive";
            this.cbBit3.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBit2
            // 
            this.cbBit2.AutoSize = true;
            this.cbBit2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbBit2.Location = new System.Drawing.Point(4, 84);
            this.cbBit2.Margin = new System.Windows.Forms.Padding(2);
            this.cbBit2.Name = "cbBit2";
            this.cbBit2.Size = new System.Drawing.Size(96, 17);
            this.cbBit2.TabIndex = 4;
            this.cbBit2.Text = "immediately";
            this.cbBit2.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // cbBit1
            // 
            this.cbBit1.AutoSize = true;
            this.cbBit1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbBit1.Location = new System.Drawing.Point(4, 65);
            this.cbBit1.Margin = new System.Windows.Forms.Padding(2);
            this.cbBit1.Name = "cbBit1";
            this.cbBit1.Size = new System.Drawing.Size(71, 17);
            this.cbBit1.TabIndex = 3;
            this.cbBit1.Tag = "joinable/joinable?";
            this.cbBit1.Text = "joinable";
            this.cbBit1.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(279, 323);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Autonomy";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbJoinIndex
            // 
            this.tbJoinIndex.Location = new System.Drawing.Point(352, 339);
            this.tbJoinIndex.Margin = new System.Windows.Forms.Padding(0);
            this.tbJoinIndex.MaxLength = 10;
            this.tbJoinIndex.Name = "tbJoinIndex";
            this.tbJoinIndex.Size = new System.Drawing.Size(91, 21);
            this.tbJoinIndex.TabIndex = 11;
            this.tbJoinIndex.Text = "0xDDDDDDDD";
            this.tbJoinIndex.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            this.tbJoinIndex.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbJoinIndex.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(11, 400);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Facial Animation ID";
            // 
            // tpHumanMotives
            // 
            this.tpHumanMotives.AutoScroll = true;
            this.tpHumanMotives.Controls.Add(this.timtuiHuman);
            this.tpHumanMotives.Location = new System.Drawing.Point(4, 22);
            this.tpHumanMotives.Margin = new System.Windows.Forms.Padding(2);
            this.tpHumanMotives.Name = "tpHumanMotives";
            this.tpHumanMotives.Size = new System.Drawing.Size(550, 453);
            this.tpHumanMotives.TabIndex = 1;
            this.tpHumanMotives.Tag = "Motives/Human Motives";
            this.tpHumanMotives.Text = "Human Motives";
            this.tpHumanMotives.UseVisualStyleBackColor = true;
            // 
            // timtuiHuman
            // 
            this.timtuiHuman.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.timtuiHuman.Location = new System.Drawing.Point(0, 0);
            this.timtuiHuman.Margin = new System.Windows.Forms.Padding(2);
            this.timtuiHuman.MotiveTable = null;
            this.timtuiHuman.Name = "timtuiHuman";
            this.timtuiHuman.Size = new System.Drawing.Size(550, 352);
            this.timtuiHuman.TabIndex = 0;
            // 
            // tpAnimalMotives
            // 
            this.tpAnimalMotives.AutoScroll = true;
            this.tpAnimalMotives.Controls.Add(this.timtuiAnimal);
            this.tpAnimalMotives.Location = new System.Drawing.Point(4, 22);
            this.tpAnimalMotives.Margin = new System.Windows.Forms.Padding(2);
            this.tpAnimalMotives.Name = "tpAnimalMotives";
            this.tpAnimalMotives.Size = new System.Drawing.Size(550, 453);
            this.tpAnimalMotives.TabIndex = 2;
            this.tpAnimalMotives.Text = "Animal Motives";
            this.tpAnimalMotives.UseVisualStyleBackColor = true;
            // 
            // timtuiAnimal
            // 
            this.timtuiAnimal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.timtuiAnimal.Location = new System.Drawing.Point(0, 0);
            this.timtuiAnimal.Margin = new System.Windows.Forms.Padding(2);
            this.timtuiAnimal.MotiveTable = null;
            this.timtuiAnimal.Name = "timtuiAnimal";
            this.timtuiAnimal.Size = new System.Drawing.Size(550, 352);
            this.timtuiAnimal.TabIndex = 0;
            // 
            // pjse_banner1
            // 
            this.pjse_banner1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pjse_banner1.Location = new System.Drawing.Point(0, 0);
            this.pjse_banner1.Margin = new System.Windows.Forms.Padding(0);
            this.pjse_banner1.Name = "pjse_banner1";
            this.pjse_banner1.Size = new System.Drawing.Size(984, 27);
            this.pjse_banner1.TabIndex = 121;
            this.pjse_banner1.TitleText = "Pie Menu";
            this.pjse_banner1.TreeText = "Comments";
            // 
            // TtabForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(984, 506);
            this.Controls.Add(this.ttabPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TtabForm";
            this.Text = "TtabForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ttabPanel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flpFileCtrl.ResumeLayout(false);
            this.flpFileCtrl.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpSettings.ResumeLayout(false);
            this.tpSettings.PerformLayout();
            this.tlpSettingsHead.ResumeLayout(false);
            this.tlpSettingsHead.PerformLayout();
            this.flpPieStringID.ResumeLayout(false);
            this.flpPieStringID.PerformLayout();
            this.flpAction.ResumeLayout(false);
            this.flpAction.PerformLayout();
            this.flpGuard.ResumeLayout(false);
            this.flpGuard.PerformLayout();
            this.gbFlags2.ResumeLayout(false);
            this.gbFlags2.PerformLayout();
            this.gbFlags.ResumeLayout(false);
            this.gbFlags.PerformLayout();
            this.tpHumanMotives.ResumeLayout(false);
            this.tpAnimalMotives.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion


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

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int i = lbttab.SelectedIndex;
            object a, b;

            internalchg = true;
            a = lbttab.Items[i];
            b = lbttab.Items[i - 1];
            wrapper.Move(i, i - 1);
            lbttab.Items[i] = b;
            lbttab.Items[i - 1] = a;
            internalchg = false;

            lbttab.SelectedIndex--;
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            int i = lbttab.SelectedIndex;
            object a, b;

            internalchg = true;
            a = lbttab.Items[i];
            b = lbttab.Items[i + 1];
            wrapper.Move(i, i + 1);
            lbttab.Items[i] = b;
            lbttab.Items[i + 1] = a;
            internalchg = false;

            lbttab.SelectedIndex++;
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            this.ttabPanel.SuspendLayout();
            internalchg = true;
            wrapper.Add((lbttab.SelectedIndex == -1) ? new TtabItem(wrapper) : wrapper[lbttab.SelectedIndex].Clone());
            addItem(wrapper.Count - 1);
            internalchg = false;
            lbttab.SelectedIndex = wrapper.Count - 1;
            this.ttabPanel.ResumeLayout();
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            wrapper.RemoveAt(lbttab.SelectedIndex);
        }

        private void btnAppend_Click(object sender, System.EventArgs e)
        {
            this.Append((new pjse.ResourceChooser()).Execute(wrapper.FileDescriptor.Type, wrapper.FileDescriptor.Group, ttabPanel, true));
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


            this.btnMoveUp.Enabled = this.btnStrPrev.Enabled = (lbttab.SelectedIndex > 0);
            this.btnMoveDown.Enabled = this.btnStrNext.Enabled = (lbttab.SelectedIndex < lbttab.Items.Count - 1);

            if (lbttab.SelectedIndex >= 0)
			{
                lbTTABEntry.Text = "0x" + lbttab.SelectedIndex.ToString("X");

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
                doFlags2();

                timtuiHuman.MotiveTable = wrapper[lbttab.SelectedIndex].HumanMotives;
                timtuiAnimal.MotiveTable = wrapper[lbttab.SelectedIndex].AnimalMotives;
            }
			else
			{
                lbTTABEntry.Text = "---";

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
			pjse.FileTable.Entry item = new pjse.ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, ttabPanel.Parent, false);
			if (item != null)
				setBHAV(1, (ushort)item.Instance, false);
		}

		private void GetTTABAction(object sender, System.EventArgs e)
		{
			pjse.FileTable.Entry item = new pjse.ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, ttabPanel.Parent, false);
			if (item != null)
				setBHAV(0, (ushort)item.Instance, false);
		}

        private void llBhav_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            pjse.FileTable.Entry item = wrapper.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, (sender == llAction) ? currentItem.Action : currentItem.Guardian);
            Bhav b = new Bhav();
            b.ProcessData(item.PFD, item.Package);

            BhavForm ui = (BhavForm)b.UIHandler;
            ui.Tag = "Popup" // tells the SetReadOnly function it's in a popup - so everything locked down
                + ";callerID=+" + wrapper.FileDescriptor.ExportFileName + "+";
            ui.Text = pjse.Localization.GetString("viewbhav")
                + ": " + b.FileName + " [" + b.Package.SaveFileName + "]";
            b.RefreshUI();
            ui.Show();
        }


        private void btnNoFlags_Click(object sender, System.EventArgs e)
        {
            internalchg = true;
            currentItem.Flags = (ushort)(wrapper.Format < 0x54 ? 0x0070 : 0x0000);
            this.tbFlags.Text = "0x" + Helper.HexString(currentItem.Flags);
            doFlags();
            internalchg = false;
        }

        private void btnNoFlags2_Click(object sender, EventArgs e)
        {
            internalchg = true;
            currentItem.Flags2 = (ushort)0x0000;
            this.tbFlags2.Text = "0x" + Helper.HexString(currentItem.Flags2);
            doFlags2();
            internalchg = false;
        }

        private void checkbox_CheckedChanged(object sender, System.EventArgs e)
        {
            if (internalchg) return;

            if (!(sender is CheckBox)) return;

            int i = alFlags.IndexOf(sender);
            if (i < 0)
                throw new Exception("checkbox_CheckedChanged not applicable to control " + sender.ToString());

            internalchg = true;
            if (i < 16)
            {
                Boolset flags = new Boolset(currentItem.Flags);
                flags.flip(i);
                currentItem.Flags = flags;
                this.tbFlags.Text = "0x" + Helper.HexString(currentItem.Flags);
            }
            else if (i < 32)
            {
                Boolset flags = new Boolset(currentItem.Flags2);
                flags.flip(i - 16);
                currentItem.Flags2 = flags;
                this.tbFlags2.Text = "0x" + Helper.HexString(currentItem.Flags2);
            }
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
                setStringIndex((uint)val, true, false);
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
				case 3:
                    currentItem.Flags2 = val;
                    doFlags2();
                    break;
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
                    setStringIndex(val, false, true);
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
