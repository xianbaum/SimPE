/***************************************************************************
 *   Copyright (C) 2007 by Peter L Jones                                   *
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TrapKATEditor.UI
{
    public partial class MainForm : Form
    {
        List<ToolStripMenuItem> tsmiSave = null;
        List<ToolStripMenuItem> tsmiSaveAs = null;
        List<ComboBox> cbNotes = null;
        List<CheckBox> ckbFlags = null;
        List<NumericUpDown> nudVel = null;
        Data.Pad currentPad = null;
        Data.Kit currentKit = null;

        public MainForm()
        {
            InitializeComponent();
            cbGate.Items.Clear();
            cbGate.Items.AddRange(Data.Gate.Modes.ToArray());
            tsmiSave = new List<ToolStripMenuItem>(new ToolStripMenuItem[] { tsmiFileSaveAllMemory, tsmiFileSaveGlobalMemory, tsmiFileSaveCurrentKit });
            tsmiSaveAs = new List<ToolStripMenuItem>(new ToolStripMenuItem[] { tsmiFileSaveAllMemoryAs, tsmiFileSaveGlobalMemoryAs, tsmiFileSaveCurrentKitAs });
            cbNotes = new List<ComboBox>(new ComboBox[] { cbNote1, cbNote2, cbNote3, cbNote4, cbNote5, cbNote6 });
            ckbFlags = new List<CheckBox>(new CheckBox[] { ckbF0, ckbF1, ckbF2, ckbF3, ckbF4, ckbF5, ckbF6, ckbF7, });
            nudVel = new List<NumericUpDown>(new NumericUpDown[] { nudMinVel, nudMaxVel, });
        }

        private bool internalchg = false;

        private void doExit()
        {
            // check no uncomitted changes
            this.Close();
        }

        private void doNew()
        {
            // check no uncomitted changes
            MainProgram.Reinit();
        }

        #region Pad
        private void doPad(int pad)
        {
            if (currentPad != null)
                currentPad.DataChanged -= new EventHandler(currentPad_DataChanged);
            currentPad = currentKit[pad];
            currentPad.DataChanged += new EventHandler(currentPad_DataChanged);
            currentPad_DataChanged(null, null);

            internalchg = true;
            for (int i = 0; i < cbNotes.Count; i++) doNote(i);

            if (currentPad.Curve < cbPadCurve.Items.Count)
                cbPadCurve.SelectedIndex = currentPad.Curve;
            else
            {
                cbPadCurve.SelectedIndex = -1;
                cbPadCurve.Text = currentPad.Curve.ToString();
            }

            doGate();

            nudChannel.Value = currentPad.Channel + 1;
            nudMinVel.Value = currentPad.MinVelocity;
            nudMaxVel.Value = currentPad.MaxVelocity;
            for (int i = 0; i < ckbFlags.Count; i++)
                ckbFlags[i].Checked = (currentPad.Flags & (1 << i)) != 0;
            internalchg = false;
        }

        void currentPad_DataChanged(object sender, EventArgs e)
        {
            lbPadDataChanged.Visible = currentPad.Changed;
        }

        private void cbPad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            doPad(cbPad.SelectedIndex);
        }
        #endregion


        #region Kit
        void MainProgram_KitChanged(object sender, EventArgs e)
        {
            if (currentKit != null)
                currentKit.DataChanged -= new EventHandler(currentKit_DataChanged);
            currentKit = MainProgram.CurrentKits[MainProgram.CurrentKit];
            currentKit.DataChanged += new EventHandler(currentKit_DataChanged);
            currentKit_DataChanged(null, null);
            
            internalchg = true;
            cbPad.SelectedIndex = -1;

            tbKitName.Text = currentKit.KitName;

            internalchg = false;
            cbPad.SelectedIndex = 0;
        }

        void currentKit_DataChanged(object sender, EventArgs e)
        {
            lbKitDataChanged.Visible = currentKit.Changed;
        }

        private void cbCurrentKit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            MainProgram.CurrentKit = cbCurrentKit.SelectedIndex;
        }
        #endregion

        void MainProgram_AllMemoryChanged(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem tsmi in tsmiSave)
                tsmi.Enabled = false;

            internalchg = true;
            cbCurrentKit.SelectedIndex = -1;

            MainProgram_GlobalChanged(null, null);
            cbCurrentKit.Items.Clear();
            for (int i = 0; i < MainProgram.CurrentKits.Length; i++)
            {
                cbCurrentKit.Items.Add((i + 1).ToString() + ": " + MainProgram.CurrentKits[i].KitName);
            }

            internalchg = false;
            cbCurrentKit.SelectedIndex = 0;
        }

        void MainProgram_GlobalChanged(object sender, EventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MainProgram.AllMemoryChanged += new EventHandler(MainProgram_AllMemoryChanged);
            MainProgram.GlobalChanged += new EventHandler(MainProgram_GlobalChanged);
            MainProgram.KitChanged += new EventHandler(MainProgram_KitChanged);
            MainProgram_AllMemoryChanged(null, null);
        }

        private void tsmiFileExitQuit_Click(object sender, EventArgs e)
        {
            doExit();
        }

        private void tsmiFileOpen_Click(object sender, EventArgs e)
        {
            DialogResult dr = ofdOpenSysEx.ShowDialog();
            if (DialogResult.OK.Equals(dr))
                try
                {
                    MainProgram.OpenFile(ofdOpenSysEx.FileName);
                }
                catch (Dump.SysexDump.OpenException ex)
                {
                    System.Windows.Forms.MessageBox.Show(
                        "Invalid SysEx File:"
                        + "\r\n"
                        + ex.Type.ToString() + " at 0x" + Convert.ToString(ex.Offset, 16)
                        + "", "Invalid SysEx File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (ArgumentOutOfRangeException)
                {
                    System.Windows.Forms.MessageBox.Show(
                        "Please select a Kit before loading a Kit Dump."
                        + "", "Select a Kit first", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
        }

        private void tsmiFileSave_Click(object sender, EventArgs e)
        {
            int i = tsmiSave.IndexOf((ToolStripMenuItem)sender);
            MainProgram.SaveFile((Dump.DumpType)i);
        }

        private void tsmiFileSaveAs_Click(object sender, EventArgs e)
        {
            int i = tsmiSaveAs.IndexOf((ToolStripMenuItem)sender);
            DialogResult dr = sfdSaveSysEx.ShowDialog();
            if (DialogResult.OK.Equals(dr))
                MainProgram.SaveFile(sfdSaveSysEx.FileName, (Dump.DumpType)i);
        }

        private void tsmiFileNew_Click(object sender, EventArgs e)
        {
            doNew();
        }


        #region Note
        private void doNote(int noteN)
        {
            int j = currentPad[noteN];
            cbNotes[noteN].Enabled = true;
            if (j < 128)
            {
                cbNotes[noteN].SelectedIndex = -1;
                cbNotes[noteN].Text = j.ToString();
            }
            else if (j - 128 < cbNotes[noteN].Items.Count)
                cbNotes[noteN].SelectedIndex = j - 128;
            else
            {
                cbNotes[noteN].SelectedIndex = -1;
                cbNotes[noteN].Text = j.ToString();
                cbNotes[noteN].Enabled = false;
            }
        }

        private byte ToNoteNum(string text)
        {
            try { byte b = Convert.ToByte(text); if (b < 128) return b; }
            catch (Exception) { } // carry on with the rest

            List<char> notes = new List<char>(new char[] { 'C', 'C', 'D', 'D', 'E', 'F', 'F', 'G', 'G', 'A', 'A', 'B', });
            int notenum = notes.IndexOf(text.ToUpper().ToCharArray()[0]);
            if (notenum < 0)
                throw new ArgumentException(text + " is not a MIDI Note");

            int i = 1;
            List<char> accid = new List<char>(new char[] { '#', 'b', });
            char[] cbText = text.ToCharArray();
            while (i < cbText.Length && accid.IndexOf(cbText[i]) > 0)
                notenum += accid.IndexOf(cbText[i++]) == 1 ? 1 : -1;

            if (i >= cbText.Length)
                throw new ArgumentException(text + " is not a MIDI Note");

            int octave;
            try { octave = Convert.ToInt16(text.Substring(i)); }
            catch (Exception) { throw new ArgumentException(text + " is not a MIDI Note"); }

            // For now, middle C = C4 = MIDI note 60
            notenum = notenum + ((octave + 1) * 12);

            if ((notenum >= 0) && (notenum < 128)) return (byte)notenum;
            throw new ArgumentException(text + " is not a MIDI Note");
        }

        private bool cbNote_IsValid(ComboBox cb)
        {
            // Can't be blank
            if (cb.Text.Length == 0) return false;

            // Is it one of the predefined entries?
            if (cb.Items.IndexOf(cb.Text) != -1) return true;

            try { ToNoteNum(cb.Text); }
            catch (Exception) { return false; }
            return true;
        }

        private void cbNote_Enter(object sender, System.EventArgs e)
        {
            ((ComboBox)sender).SelectAll();
        }

        private void cbNote_TextChanged(object sender, System.EventArgs ev)
        {
            if (internalchg) return;

            ComboBox cb = (ComboBox)sender;
            int i = cbNotes.IndexOf(cb);
            if (i < 0)
                throw new Exception("cbNote_TextChanged not applicable to control " + sender.ToString());

            if (!cbNote_IsValid(cb)) return;

            if (cb.Items.IndexOf(cb.Text) != -1) return; // handled by SelectedIndexChanged
            currentPad[i] = ToNoteNum(cb.Text);
        }

        private void cbNote_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cbNote_IsValid(cb)) return;

            int i = cbNotes.IndexOf(cb);
            if (i < 0)
                throw new Exception("cbNote_Validating not applicable to control " + sender.ToString());

            e.Cancel = true;

            doNote(i);
            cb.SelectAll();
        }

        private void cbNote_Validated(object sender, System.EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            int i = cbNotes.IndexOf(cb);
            if (i < 0)
                throw new Exception("cbNote_Validated not applicable to control " + sender.ToString());
            if (cb.Items.IndexOf(cb.Text) != -1) return;

            bool origstate = internalchg;
            internalchg = true;
            doNote(i);
            internalchg = origstate;
            cb.Select(0, 0);
        }

        private void cbNote_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (internalchg) return;

            ComboBox cb = (ComboBox)sender;
            int i = cbNotes.IndexOf(cb);
            if (i < 0)
                throw new Exception("cbNote_SelectedIndexChanged not applicable to control " + sender.ToString());

            if (cb.SelectedIndex == -1) return;

            internalchg = true;
            currentPad[i] = (byte)(128 + cb.SelectedIndex);
            internalchg = false;

            cb.SelectAll();
        }
        #endregion

        private void cbPadCurve_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPad.Curve = (byte)((ComboBox)sender).SelectedIndex;
        }


        #region Gate
        private void doGate()
        {
            string s = currentPad.Gate;
            cbGate.SelectedIndex = cbGate.Items.IndexOf(s);
            if (cbGate.SelectedIndex == -1) cbGate.Text = s;
        }

        private bool cbGate_IsValid(ComboBox cb)
        {
            // Can't be blank
            if (cb.Text.Length == 0) return false;

            // Is it one of the predefined entries?
            if (cb.Items.IndexOf(cb.Text) != -1) return true;

            try { (new Data.Gate()).Value = cb.Text; }
            catch (Exception) { return false; }
            return true;
        }

        private void cbGate_Enter(object sender, System.EventArgs e)
        {
            ((ComboBox)sender).SelectAll();
        }

        private void cbGate_TextChanged(object sender, System.EventArgs ev)
        {
            if (internalchg) return;

            ComboBox cb = (ComboBox)sender;

            if (!cbGate_IsValid(cb)) return;

            if (cb.Items.IndexOf(cb.Text) != -1) return; // handled by SelectedIndexChanged
            currentPad.Gate = cb.Text;
        }

        private void cbGate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cbGate_IsValid(cb)) return;

            e.Cancel = true;

            bool origstate = internalchg;
            internalchg = true;
            doGate();
            internalchg = origstate;
            cb.SelectAll();
        }

        private void cbGate_Validated(object sender, System.EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.Items.IndexOf(cb.Text) != -1) return; // Text already set

            bool origstate = internalchg;
            internalchg = true;
            doGate();
            internalchg = origstate;
            cb.Select(0, 0);
        }

        private void cbGate_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (internalchg) return;

            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedIndex == -1) return;

            currentPad.Gate = Data.Gate.Modes[cb.SelectedIndex];

            cb.SelectAll();
        }
        #endregion

        private void nudVel_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            switch (nudVel.IndexOf(nud))
            {
                case 0: currentPad.MinVelocity = (byte)nud.Value; break;
                case 1: currentPad.MaxVelocity = (byte)nud.Value; break;
            }
        }
    }
}