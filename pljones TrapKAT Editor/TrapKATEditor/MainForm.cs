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
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TrapKATEditor.UI
{
    public partial class MainForm : Form
    {
        #region Main form and initialisation
        List<ToolStripMenuItem> tsmiSave = null;
        List<ToolStripMenuItem> tsmiSaveAs = null;
        List<ToolStripMenuItem> tsmiTONotes = null;
        List<ComboBox> cbNotes = null;
        List<ComboBox> cbHHPads = null;
        List<CheckBox> ckbFlags = null;
        List<NumericUpDown> nudChannel = null;
        List<NumericUpDown> nudMIDI = null;
        List<NumericUpDown> nudByte = null;
        List<NumericUpDown> nudPadByte = null;

        public MainForm()
        {
            InitializeComponent();
            cbKitCurve.Items.Clear();
            cbKitCurve.Items.AddRange(Data.Curve.Modes.ToArray());
            cbFCCurve.Items.Clear();
            cbFCCurve.Items.AddRange(Data.Curve.Modes.ToArray());
            cbPadCurve.Items.Clear();
            cbPadCurve.Items.AddRange(Data.Curve.Modes.ToArray());
            cbKitGate.Items.Clear();
            cbKitGate.Items.AddRange(Data.Gate.Modes.ToArray());
            cbPadGate.Items.Clear();
            cbPadGate.Items.AddRange(Data.Gate.Modes.ToArray());
            tsmiSave = new List<ToolStripMenuItem>(new ToolStripMenuItem[] { tsmiFileSaveAllMemory, tsmiFileSaveGlobalMemory, tsmiFileSaveCurrentKit });
            tsmiSaveAs = new List<ToolStripMenuItem>(new ToolStripMenuItem[] { tsmiFileSaveAllMemoryAs, tsmiFileSaveGlobalMemoryAs, tsmiFileSaveCurrentKitAs });
            tsmiTONotes = new List<ToolStripMenuItem>(new ToolStripMenuItem[] { tsmiTONotesAsNumbers, tsmiTONotesAsC3, tsmiTONotesAsC4, });
            cbNotes = new List<ComboBox>(new ComboBox[] { cbNote1, cbNote2, cbNote3, cbNote4, cbNote5, cbNote6 });
            cbHHPads = new List<ComboBox>(new ComboBox[] { cbHHPad1, cbHHPad2, cbHHPad3, cbHHPad4, });
            ckbFlags = new List<CheckBox>(new CheckBox[] { ckbF0, ckbF1, ckbF2, ckbF3, ckbF4, ckbF5, ckbF6, ckbF7, });
            nudChannel = new List<NumericUpDown>(new NumericUpDown[] {
                nudFCChannel, nudKitChannel, nudPadChannel, 
                nudPrgChgTxmChn, 
            });
            nudMIDI = new List<NumericUpDown>(new NumericUpDown[] {
                nudPadMinVel, nudPadMaxVel,
                nudKitMinVel, nudKitMaxVel, nudVolume, nudPrgChg, nudBankLSB, nudBankMSB, nudBank, 
                nudGrooveVol, nudInstrumentID, 
            });
            nudByte = new List<NumericUpDown>(new NumericUpDown[] {
                nudMotifNumber, nudMotifNumberMel, nudMotifNumberPerc, 
                nudKitNumber, nudKitNumberDemo, nudKitNumberUser, nudKitNumberKAT, 
                nudGrooveStatus, nudBeeperStatus, nudChokeFunction, nudMidiMergeStatus, 
                nudDisplayAngle, nudPlayMode, nudNoteNamesStatus, nudTTMeter, nudHearSoundStatus, 
                nudBcFunction, nudBcPolarity, nudBcLowLevel, nudBcHighLevel, 
                nudFcOpenRegion,  nudFcClosedRegion, nudFcPolarity,
                nudFcVelocityLevel, nudFcLowLevel, nudFcHighLevel, 
                nudFcSplashEase, nudFcWaitModeLevel, nudHatNoteGate, 
                nudTrigGain, 
            });
            nudPadByte = new List<NumericUpDown>(new NumericUpDown[] {
                nudLowLevel, nudHighLevel,
                nudThresholdManual, nudThresholdActual, 
                nudUserMargin, nudInternalMargin, 
            });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Updates.Checker.Daily();
            internalchg = true;
            tsmiHelpCheckState.Checked = Updates.Settings.UpdateAutomatically;
            internalchg = false;

            MainProgram.AllMemoryChanged += new EventHandler(MainProgram_AllMemoryChanged);
            MainProgram.KitChanged += new EventHandler(MainProgram_KitChanged);
            MainProgram_AllMemoryChanged(null, null);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.TaskManagerClosing:
                    break;
                default:
                    e.Cancel = !okayToSplat();
                    break;
            }
        }
        #endregion


        #region File Menu
        private bool okayToSplat()
        {
            if (!MainProgram.CurrentAllMemory.Changed) return true;

            return DialogResult.OK.Equals(System.Windows.Forms.MessageBox.Show(
                "You have unsaved changes."
                + "\r\n"
                + "OK to continue?"
                + "", Application.ProductName,
                MessageBoxButtons.OKCancel, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2)
            );
        }

        private void tsmiFileNew_Click(object sender, EventArgs e)
        {
            if (!okayToSplat()) return;
            MainProgram.Reinit();

            this.Text = Application.ProductName + " - Editing: "
                + (MainProgram.CurrentFilename.Length > 0 ? MainProgram.CurrentFilename : "New file");
        }

        private void tsmiFileOpen_Click(object sender, EventArgs e)
        {
            if (!okayToSplat()) return;

            DialogResult dr = ofdOpenSysEx.ShowDialog();
            if (DialogResult.OK.Equals(dr))
                try
                {
                    MainProgram.OpenFile(ofdOpenSysEx.FileName);
                    this.Text = Application.ProductName + " - Editing: "
                        + (MainProgram.CurrentFilename.Length > 0 ? MainProgram.CurrentFilename : "New file");
                }
                catch (Dump.SysexDump.OpenException ex)
                {
                    System.Windows.Forms.MessageBox.Show(
                        "Invalid SysEx File:"
                        + "\r\n"
                        + ex.Type.ToString() + " at 0x" + Convert.ToString(ex.Offset, 16)
                        + "", "Invalid SysEx File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void tsmiFileSave_Click(object sender, EventArgs e)
        {
            Dump.DumpType[] dt = { Dump.DumpType.AllMemory, Dump.DumpType.Global, Dump.DumpType.Kit, };
            MainProgram.SaveFile(dt[tsmiSave.IndexOf((ToolStripMenuItem)sender)]);
            MainProgram_AllMemoryChanged(null, null);
        }

        private void tsmiFileSaveAs_Click(object sender, EventArgs e)
        {
            Dump.DumpType[] t = { Dump.DumpType.AllMemory, Dump.DumpType.Global, Dump.DumpType.Kit, };
            String[] s = { "AllMemory", "Global",
                (currentKit != null && currentKit.KitName.Length > 0) ? currentKit.KitName : "CurrentKit" };
            int i = tsmiSaveAs.IndexOf((ToolStripMenuItem)sender);

            sfdSaveSysEx.FileName = (MainProgram.CurrentType == t[i] && MainProgram.CurrentFilename.Length > 0)
                ? MainProgram.CurrentFilename
                : (s[i] + ".syx");

            if (DialogResult.OK.Equals(sfdSaveSysEx.ShowDialog()))
            {
                MainProgram.SaveFile(sfdSaveSysEx.FileName, t[i]);
                MainProgram_AllMemoryChanged(null, null);
            }
        }

        private void tsmiFileExitQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void tsmiTONotes_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            internalchg = true;

            int i = tsmiTONotes.IndexOf((ToolStripMenuItem)sender);
            List<int> indices = new List<int>(new int[] { 0, 1, 2 });
            indices.Remove(i);
            foreach (int index in indices) tsmiTONotes[index].Checked = false;
            Settings.TONotesAs = i;
            for (int j = 0; j < cbNotes.Count; j++) doNote(j);

            internalchg = false;
        }


        #region Help Menu
        private void tsmiHelpContents_Click(object sender, EventArgs e)
        {
            String s = System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location);
            s = System.IO.Path.Combine(s, "TrapKATEditor_Help\\index.html");
            if (System.IO.File.Exists(s))
                Process.Start(new ProcessStartInfo(s));
            else
                MessageBox.Show(L.G("helpNotFound"), L.G("helpPopupTitle"),
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void tsmiHelpCheck_Click(object sender, EventArgs e)
        {
            if (!Updates.Checker.GetUpdate(false))
                System.Windows.Forms.MessageBox.Show(L.G("UHNoUpdate"), L.G("TrapKATEditor_UpdateSettings"));
        }

        private void tsmiHelpCheckState_Click(object sender, EventArgs e)
        {
            if (internalchg == true) return;
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            if (Updates.Settings.UpdateAutomatically = tsmi.Checked) Updates.Checker.Daily(); // yum. beware code like this.
        }

        private void tsmiHelpAbout_Click(object sender, EventArgs e)
        {
            string[] s = { "UTAskMe", "UTDaily", "UTManual" };
            System.Windows.Forms.MessageBox.Show(""
                + L.G("helpAboutCaption")
                + "\r\n"
                + L.G("helpAboutVersion") + ": " + Version.BuildTS
                + "\r\n"
                + "\r\n" + L.G("helpAboutUC") + ": " + L.G(s[Updates.Settings.AutoUpdateChoice])
                + "\r\n" + L.G("helpAboutUU") + ": " + Updates.Settings.AutoUpdateURL
                + "\r\n" + L.G("helpAboutLU") + ": " + Updates.Settings.LastUpdateTS.ToString()
                , L.G("helpAboutCaption")
            );
        }
        #endregion


        private bool internalchg = false;


        #region Note
        private String ToNoteName(int num)
        {
            if (num < 0 || num > 127)
                throw new ArgumentException(num.ToString() + " is not a MIDI Note");

            if (Settings.TONotesAs == 0)
                return num.ToString();

            int octave = num / 12;
            String note = (new String[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B", })[num % 12];
            octave = octave - (Settings.TONotesAs == 1 ? 2 : 1);
            return note + octave.ToString();
        }

        private void doNote(int noteN)
        {
            int j = currentPad[noteN];
            cbNotes[noteN].Enabled = true;
            if (j < 128)
            {
                cbNotes[noteN].SelectedIndex = -1;
                cbNotes[noteN].Text = ToNoteName(j);
            }
            else if (j - 128 < cbNotes[noteN].Items.Count)
                cbNotes[noteN].SelectedIndex = j - 128;
            else
            {
                cbNotes[noteN].SelectedIndex = -1;
                cbNotes[noteN].Text = ToNoteName(j);
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

            notenum = notenum + ((octave + (Settings.TONotesAs == 1 ? 2 : 1)) * 12);

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


        #region Pad
        Data.Pad currentPad = null;
        private void doPad(int pad)
        {
            if (currentPad != null)
                currentPad.DataChanged -= new EventHandler(currentPad_DataChanged);
            currentPad = currentKit[pad];
            currentPad.DataChanged += new EventHandler(currentPad_DataChanged);
            currentPad_DataChanged(null, null);

            bool savedchg = internalchg;
            internalchg = true;
            for (int i = 0; i < cbNotes.Count; i++) doNote(i);

            setCurveComboBox(cbPadCurve, currentPad.Curve);
            setCurveComboBox(cbPadGate, currentPad.Gate);

            nudPadChannel.Value = currentPad.Channel + 1;
            nudPadMinVel.Value = currentPad.MinVelocity;
            nudPadMaxVel.Value = currentPad.MaxVelocity;
            for (int i = 0; i < ckbFlags.Count; i++)
                ckbFlags[i].Checked = (currentPad.Flags & (1 << i)) != 0;
            internalchg = savedchg;
        }

        void currentPad_DataChanged(object sender, EventArgs e)
        {
            lbPadDataChanged.Visible = currentPad.Changed;
        }

        private void cbPad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            doPad(((ComboBox)sender).SelectedIndex);
        }

        private void ckbFlag_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = (CheckBox)sender;
            int i = ckbFlags.IndexOf(ckb);
            currentPad.Flags = (byte)((currentPad.Flags & (1 << i))
                | ((ckb.Checked ? 1 : 0) << i));
        }
        #endregion


        #region Kit
        Data.Kit currentKit = null;
        private void cbCurrentKit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            MainProgram.CurrentKit = cbCurrentKit.SelectedIndex;
        }

        void MainProgram_KitChanged(object sender, EventArgs e)
        {
            if (currentKit != null)
                currentKit.DataChanged -= new EventHandler(currentKit_DataChanged);
            currentKit = MainProgram.CurrentAllMemory[cbCurrentKit.SelectedIndex];
            currentKit.DataChanged += new EventHandler(currentKit_DataChanged);
            currentKit_DataChanged(null, null);

            internalchg = true;

            tbKitName.Text = currentKit.KitName;

            ckbVarCurve.Checked = currentKit.Curve.Equals("Various");
            setCurveComboBox(cbKitCurve, currentKit.Curve);
            ckbVarGate.Checked = currentKit.Gate.Equals("Various");
            cbKitGate.SelectedIndex = Data.Gate.Modes.IndexOf(currentKit.Gate);
            if (cbKitGate.SelectedIndex == -1) cbKitGate.Text = currentKit.Gate;

            if (currentKit.FcFunction < cbFCFunction.Items.Count)
                cbFCFunction.SelectedIndex = currentKit.FcFunction;
            else
                cbFCFunction.Text = currentKit.FcFunction.ToString();
            setCurveComboBox(cbFCCurve, currentKit.FcCurve);

            // You may call this obfuscated...
            nudFCChannel.Value = (
                (ckbAsChick.Checked = (currentKit.FcChannel >= 16))
                    ? currentKit[25].Channel
                    : currentKit.FcChannel
                ) + 1;


            for (int i = 0; i < cbHHPads.Count; i++)
            {
                int j = currentKit.HHPads[i];
                cbHHPads[i].SelectedIndex = (j < cbHHPads[i].Items.Count) ? j : cbHHPads[i].Items.Count - 1;
            }

            ckbVarMaxVel.Checked = ckbVarMinVel.Checked = ckbVarChannel.Checked = false;
            for (int i = 0; i < cbPad.Items.Count; i++)
            {
                if (!ckbVarChannel.Checked) ckbVarChannel.Checked = !currentKit[i].Channel.Equals(currentKit.Channel);
                if (!ckbVarMinVel.Checked) ckbVarMinVel.Checked = !currentKit[i].MinVelocity.Equals(currentKit.MinVelocity);
                if (!ckbVarMaxVel.Checked) ckbVarMaxVel.Checked = !currentKit[i].MaxVelocity.Equals(currentKit.MaxVelocity);
            }
            nudKitChannel.Value = currentKit.Channel + 1;
            nudKitMinVel.Value = currentKit.MinVelocity;
            nudKitMaxVel.Value = currentKit.MaxVelocity;

            nudVolume.Value = (
                (ckbNoVolume.Checked = (currentKit.Volume >= 128))
                    ? (byte)127
                    : currentKit.Volume
                );

            nudPrgChg.Value = (
                (ckbNoPrgChg.Checked = (currentKit.PrgChg >= 128))
                    ? (byte)0
                    : currentKit.PrgChg
                ) + 1;

            nudPrgChgTxmChn.Value = currentKit.PrgChgTxmChn + 1;
            nudBank.Value = currentKit.Bank;
            nudBankLSB.Value = currentKit.BankLSB;
            nudBankMSB.Value = currentKit.BankMSB > 127 ? (byte)127 : currentKit.BankMSB; // TrapKAT bug...

            cbPad.SelectedIndex = 0;

            internalchg = false;

            ckbVarCurve_CheckedChanged(ckbVarCurve, null);
            ckbVarGate_CheckedChanged(ckbVarGate, null);
            ckbVarChannel_CheckedChanged(ckbVarChannel, null);
            ckbVarMinVel_CheckedChanged(ckbVarMinVel, null);
            ckbVarMaxVel_CheckedChanged(ckbVarMaxVel, null);
            ckbAsChick_CheckedChanged(ckbAsChick, null);
            nudVolume.Enabled = !ckbNoVolume.Checked;
            tlpBank.Enabled = nudPrgChgTxmChn.Enabled = nudPrgChg.Enabled = !ckbNoPrgChg.Checked;

            doPad(0);
        }

        private void currentKit_DataChanged(object sender, EventArgs e)
        {
            tsmiFileSaveCurrentKit.Enabled =
                currentKit.Changed
                && (MainProgram.CurrentType == Dump.DumpType.Kit)
                && (MainProgram.CurrentFilename.Length > 0);
            tsmiFileSaveCurrentKit.ShortcutKeys = tsmiFileSaveCurrentKit.Enabled ? Keys.Control | Keys.S : Keys.None;

            lbKitDataChanged.Visible = currentKit.Changed;
        }

        private void tbKitName_TextChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            currentKit.KitName = tbKitName.Text;
        }

        private void doHHPad(int pad, byte value)
        {
            byte oldpad = currentKit.HHPads[pad];
            currentKit[oldpad].Flags &= 0x7f;
            currentKit[pad].Flags |= 0x80;
            currentKit.HHPads[pad] = value;
        }

        private void ckbVarCurve_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            // If setting Kit override, ask first
            CheckBox ckb = (CheckBox)sender;

            internalchg = true;
            if (cbKitCurve.Enabled = !ckb.Checked)
                cbKitCurve.SelectedIndex = Data.Curve.Modes.IndexOf(currentKit.Curve = (new Data.Curve()).Value);

            if (cbPadCurve.Enabled = ckb.Checked)
                cbPadCurve.Text = currentPad.Curve;

            internalchg = false;
        }

        private void ckbVarGate_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            // If setting Kit override, ask first
            CheckBox ckb = (CheckBox)sender;

            internalchg = true;
            if (cbKitGate.Enabled = !ckb.Checked)
                cbKitGate.Text = currentKit.Gate = (new Data.Gate()).Value;

            if (cbPadGate.Enabled = ckb.Checked)
                cbPadGate.Text = currentPad.Gate;

            internalchg = false;
        }

        private void ckbVarChannel_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            // If setting Kit override, ask first
            CheckBox ckb = (CheckBox)sender;

            internalchg = true;
            if (nudKitChannel.Enabled = !ckb.Checked)
                nudKitChannel.Value = (currentKit.Channel = (byte)nudKitChannel.Value);

            if (nudPadChannel.Enabled = ckb.Checked)
                nudPadChannel.Value = currentPad.Channel;

            internalchg = false;
        }

        private void ckbVarMinVel_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            // If setting Kit override, ask first
            CheckBox ckb = (CheckBox)sender;

            internalchg = true;
            if (nudKitMinVel.Enabled = !ckb.Checked)
                nudKitMinVel.Value = (currentKit.MinVelocity = (byte)nudKitMinVel.Value);

            if (nudPadMinVel.Enabled = ckb.Checked)
                nudPadMinVel.Value = currentPad.MinVelocity;

            internalchg = false;
        }

        private void ckbVarMaxVel_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            // If setting Kit override, ask first
            CheckBox ckb = (CheckBox)sender;

            internalchg = true;
            if (nudKitMaxVel.Enabled = !ckb.Checked)
                nudKitMaxVel.Value = (currentKit.MaxVelocity = (byte)nudKitMaxVel.Value);

            if (nudPadMaxVel.Enabled = ckb.Checked)
                nudPadMaxVel.Value = currentPad.MaxVelocity;

            internalchg = false;
        }

        private void ckbAsChick_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            // If setting As Chick, ask first
            CheckBox ckb = (CheckBox)sender;

            internalchg = true;
            if (nudFCChannel.Enabled != ckb.Checked)
                nudFCChannel.Value = (currentKit.FcChannel = currentKit[25].Channel) + 1;
            else
            {
                currentKit.FcChannel = 16;
                nudFCChannel.Value = currentKit[25].Channel + 1;
            }
            

            internalchg = false;
        }

        private void ckbNoVolume_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            // If setting Off, ask first
            CheckBox ckb = (CheckBox)sender;

            internalchg = true;
            if (nudVolume.Enabled = !ckb.Checked)
                currentKit.Volume = 127;
            else
            {
                nudVolume.Value = 127;
                currentKit.Volume = 128;
            }

            internalchg = false;
        }

        private void ckbNoPrgChg_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            // If setting Off, ask first
            CheckBox ckb = (CheckBox)sender;

            internalchg = true;
            tlpBank.Enabled = nudPrgChgTxmChn.Enabled = nudPrgChg.Enabled = !ckb.Checked;

            if (ckb.Checked)
            {
                nudPrgChg.Value = 0;
                currentKit.PrgChg = 128;
            }
            else
                currentKit.PrgChg = 0;

            internalchg = false;
        }

        private void cbHHPad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            ComboBox cb = (ComboBox)sender;
            int pad = cbHHPads.IndexOf(cb);
            /*if (cb.SelectedIndex != 0) // Off is always okay
            {
                byte thisHH = currentKit.HHPads[pad];
                List<int> indices = new List<int>(new int[] { 0, 1, 2, 3, });
                indices.Remove(pad);
                foreach (int index in indices)
                {
                    byte otherHH = currentKit.HHPads[index];
                    if (otherHH == 0) continue; // Off is always okay
                    if (cb.SelectedIndex == otherHH)
                    {
                        internalchg = true;
                        cb.SelectedIndex = thisHH;
                        internalchg = false;
                        return;
                    }
                }
            }*/
            doHHPad(pad, (byte)cb.SelectedIndex);
        }
        #endregion


        // Common to Pad and Kit

        #region Curve
        private void setCurveComboBox(ComboBox cb, String value)
        {
            bool savedchg = internalchg;
            internalchg = true;

            cb.SelectedIndex = Data.Curve.Modes.IndexOf(value);
            if (cb.SelectedIndex == -1) cb.Text = value;

            internalchg = savedchg;
        }

        private void cbCurve_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (internalchg) return;

            ComboBox cb = (ComboBox)sender;

            if (cb == cbPadCurve) { currentPad.Curve = Data.Curve.Modes[cb.SelectedIndex]; return; }
            if (cb == cbKitCurve) { currentKit.Curve = Data.Curve.Modes[cb.SelectedIndex]; return; }
        }
        #endregion


        #region Gate
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

            if (cb.Equals(cbPadGate)) { currentPad.Gate = cb.Text; return; }
            if (cb.Equals(cbKitGate)) { currentKit.Gate = cb.Text; return; }
        }

        private void cbGate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cbGate_IsValid(cb)) return;

            e.Cancel = true;

            bool origstate = internalchg;
            internalchg = true;
            if (cb.Equals(cbPadGate)) { cb.Text = currentPad.Gate; }
            else if (cb.Equals(cbKitGate)) { cb.Text = currentKit.Gate; }
            internalchg = origstate;
            cb.SelectAll();
        }

        private void cbGate_Validated(object sender, System.EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.Items.IndexOf(cb.Text) != -1) return; // Text already set

            bool origstate = internalchg;
            internalchg = true;
            if (cb.Equals(cbPadGate)) { cb.Text = currentPad.Gate; }
            else if (cb.Equals(cbKitGate)) { cb.Text = currentKit.Gate; }
            internalchg = origstate;
            cb.Select(0, 0);
        }

        private void cbGate_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (internalchg) return;

            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedIndex == -1) return;

            if (cb == cbPadGate) currentPad.Gate = Data.Gate.Modes[cb.SelectedIndex];
            else if (cb == cbKitGate) currentKit.Gate = Data.Gate.Modes[cb.SelectedIndex];

            cb.SelectAll();
        }
        #endregion


        private void nud_ValueChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            byte value = (byte)((NumericUpDown)sender).Value;
            ((NumericUpDown)sender).Select(0, 3);

            switch (nudChannel.IndexOf((NumericUpDown)sender))
            {
                case 0: currentKit.FcChannel = --value; return;
                case 1: currentKit.Channel = --value; return;
                case 2: currentPad.Channel = --value; return;
                case 3: currentKit.PrgChgTxmChn = --value; return;
           }

            switch (nudMIDI.IndexOf((NumericUpDown)sender))
            {
                case 0: currentPad.MinVelocity = value; return;
                case 1: currentPad.MaxVelocity = value; return;
                case 2: currentKit.MinVelocity = value; return;
                case 3: currentKit.MaxVelocity = value; return;
                case 4: currentKit.Volume = value; return;
                case 5: currentKit.PrgChg = value; return;
                case 6: currentKit.Bank = value; return;
                case 7: currentKit.BankLSB = value; return;
                case 8: currentKit.BankMSB = value; return;
                case 9: currentGlobal.GrooveVol = value; return;
                case 10: currentGlobal.InstrumentID = value; return;
            }
            switch (nudByte.IndexOf((NumericUpDown)sender))
            {
                case 0: currentGlobal.MotifNumber = value; return;
                case 1: currentGlobal.MotifNumberMel = value; return;
                case 2: currentGlobal.MotifNumberPerc = value; return;
                case 3: currentGlobal.KitNumber = value; return;
                case 4: currentGlobal.KitNumberDemo = value; return;
                case 5: currentGlobal.KitNumberUser = value; return;
                case 6: currentGlobal.KitNumberKAT = value; return;
                case 7: currentGlobal.GrooveStatus = value; return;
                case 8: currentGlobal.BeeperStatus = value; return;
                case 9: currentGlobal.ChokeFunction = value; return;
                case 10: currentGlobal.MidiMergeStatus = value; return;
                case 11: currentGlobal.DisplayAngle = value; return;
                case 12: currentGlobal.PlayMode = value; return;
                case 13: currentGlobal.NoteNamesStatus = value; return;
                case 14: currentGlobal.TTMeter = value; return;
                case 15: currentGlobal.HearSoundStatus = value; return;
                case 16: currentGlobal.BCFunction = value; return;
                case 17: currentGlobal.BCPolarity = value; return;
                case 18: currentGlobal.BCLowLevel = value; return;
                case 19: currentGlobal.BCHighLevel = value; return;
                case 20: currentGlobal.FCOpenRegion = value; return;
                case 21: currentGlobal.FCClosedRegion = value; return;
                case 22: currentGlobal.FCPolarity = value; return;
                case 23: currentGlobal.FCVelocityLevel = value; return;
                case 24: currentGlobal.FCLowLevel = value; return;
                case 25: currentGlobal.FCHighLevel = value; return;
                case 26: currentGlobal.FCSplashEase = value; return;
                case 27: currentGlobal.FCWaitModeLevel = value; return;
                case 28: currentGlobal.HatNoteGate = value; return;
                case 29: currentGlobal.TrigGain = value; return;
            }

            int i = cbPadDynamics.SelectedIndex;
            switch (nudPadByte.IndexOf((NumericUpDown)sender))
            {
                case 0: currentGlobal[i].LowLevel = value; return;
                case 1: currentGlobal[i].HighLevel = value; return;
                case 2: currentGlobal[i].ThresholdManual = value; return;
                case 3: currentGlobal[i].ThresholdActual = value; return;
                case 4: currentGlobal[i].UserMargin = value; return;
                case 5: currentGlobal[i].InternalMargin = value; return;
            }
        }

        private void nud_Enter(object sender, EventArgs e)
        {
            ((NumericUpDown)sender).Select(0, 3);
        }

        // --


        #region Global
        Data.Global currentGlobal = null;
        void MainProgram_GlobalChanged(object sender, EventArgs e)
        {
            if (currentGlobal != null)
                currentGlobal.DataChanged -= new EventHandler(currentGlobal_DataChanged);
            currentGlobal = MainProgram.CurrentAllMemory.Global;
            currentGlobal.DataChanged += new EventHandler(currentGlobal_DataChanged);
            currentGlobal_DataChanged(null, null);

            bool savedchg = internalchg;
            internalchg = true;

            cbPrgChgRcvChn.SelectedIndex = currentGlobal.PrgChgRcvChn;
            nudGrooveVol.Value = currentGlobal.GrooveVol;
            nudInstrumentID.Value = currentGlobal.InstrumentID;
            nudMotifNumber.Value = currentGlobal.MotifNumber;
            nudMotifNumberMel.Value = currentGlobal.MotifNumberMel;
            nudMotifNumberPerc.Value = currentGlobal.MotifNumberPerc;
            nudKitNumber.Value = currentGlobal.KitNumber;
            nudKitNumberDemo.Value = currentGlobal.KitNumberDemo;
            nudKitNumberUser.Value = currentGlobal.KitNumberUser;
            nudKitNumberKAT.Value = currentGlobal.KitNumberKAT;
            nudGrooveStatus.Value = currentGlobal.GrooveStatus;
            nudBeeperStatus.Value = currentGlobal.BeeperStatus;
            nudChokeFunction.Value = currentGlobal.ChokeFunction;
            nudMidiMergeStatus.Value = currentGlobal.MidiMergeStatus;
            nudDisplayAngle.Value = currentGlobal.DisplayAngle;
            nudPlayMode.Value = currentGlobal.PlayMode;
            nudNoteNamesStatus.Value = currentGlobal.NoteNamesStatus;
            nudTTMeter.Value = currentGlobal.TTMeter;
            nudHearSoundStatus.Value = currentGlobal.HearSoundStatus;
            nudBcFunction.Value = currentGlobal.BCFunction;
            nudBcPolarity.Value = currentGlobal.BCPolarity;
            nudBcLowLevel.Value = currentGlobal.BCLowLevel;
            nudBcHighLevel.Value = currentGlobal.BCHighLevel;
            nudFcOpenRegion.Value = currentGlobal.FCOpenRegion;
            nudFcClosedRegion.Value = currentGlobal.FCClosedRegion;
            nudFcPolarity.Value = currentGlobal.FCPolarity;
            nudFcVelocityLevel.Value = currentGlobal.FCVelocityLevel;
            nudFcLowLevel.Value = currentGlobal.FCLowLevel;
            nudFcHighLevel.Value = currentGlobal.FCHighLevel;
            nudFcSplashEase.Value = currentGlobal.FCSplashEase;
            nudFcWaitModeLevel.Value = currentGlobal.FCWaitModeLevel;
            nudHatNoteGate.Value = currentGlobal.HatNoteGate;
            nudTrigGain.Value = currentGlobal.TrigGain;

            cbPadDynamics.SelectedIndex = 0;

            internalchg = savedchg;
            cbPadDynamics_SelectedIndexChanged(null, null);


            /*--Global--
            if (currentKit.BcFunction <cbBCFunction.Items.Count)
                cbBCFunction.SelectedIndex = currentKit.BcFunction;
            else
                cbBCFunction.Text = currentKit.BcFunction.ToString();
            nudBCFunction.Value = currentKit.BcFunction;
             --Global--*/
        }

        void currentGlobal_DataChanged(object sender, EventArgs e)
        {
            lbGlobalDataChanged.Visible = currentGlobal.Changed;
            tsmiFileSaveGlobalMemory.Enabled =
                currentGlobal.Changed
                && (MainProgram.CurrentType == Dump.DumpType.Global)
                && (MainProgram.CurrentFilename.Length > 0);
            tsmiFileSaveGlobalMemory.ShortcutKeys = tsmiFileSaveGlobalMemory.Enabled ? Keys.Control | Keys.S : Keys.None;

            lbGlobalDataChanged.Visible = currentGlobal.Changed;
        }

        private void cbPrgChgRcvChn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            currentGlobal.PrgChgRcvChn = (byte)cbPrgChgRcvChn.SelectedIndex;
        }

        private void cbPadDynamics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            nudLowLevel.Value = currentGlobal[cbPadDynamics.SelectedIndex].LowLevel;
            nudHighLevel.Value = currentGlobal[cbPadDynamics.SelectedIndex].HighLevel;
            nudThresholdManual.Value = currentGlobal[cbPadDynamics.SelectedIndex].ThresholdManual;
            nudThresholdActual.Value = currentGlobal[cbPadDynamics.SelectedIndex].ThresholdActual;
            nudUserMargin.Value = currentGlobal[cbPadDynamics.SelectedIndex].UserMargin;
            nudInternalMargin.Value = currentGlobal[cbPadDynamics.SelectedIndex].InternalMargin;
        }
        #endregion


        #region AllMemory
        Data.AllMemory currentAllMemory = null;
        void MainProgram_AllMemoryChanged(object sender, EventArgs e)
        {
            if (currentAllMemory != null)
                currentAllMemory.DataChanged -= new EventHandler(currentAllMemory_DataChanged);
            currentAllMemory = MainProgram.CurrentAllMemory;
            currentAllMemory.DataChanged += new EventHandler(currentAllMemory_DataChanged);
            currentAllMemory_DataChanged(null, null);


            MainProgram_GlobalChanged(null, null);

            bool savedchg = internalchg;
            internalchg = true;
            cbCurrentKit.Items.Clear();
            for (int i = 0; i < MainProgram.CurrentAllMemory.Length; i++)
                cbCurrentKit.Items.Add((i + 1).ToString() + ": " + MainProgram.CurrentAllMemory[i].KitName);
            cbCurrentKit.SelectedIndex = MainProgram.CurrentKit;
            internalchg = savedchg;

            MainProgram_KitChanged(null, null);
        }

        void currentAllMemory_DataChanged(object sender, EventArgs e)
        {
            tsmiFileSaveAllMemory.Enabled =
                (MainProgram.CurrentType == Dump.DumpType.AllMemory)
                && (MainProgram.CurrentFilename.Length > 0);
            tsmiFileSaveAllMemory.ShortcutKeys = tsmiFileSaveAllMemory.Enabled ? Keys.Control | Keys.S : Keys.None;
        }
        #endregion
    }
}