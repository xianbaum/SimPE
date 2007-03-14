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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void doExit()
        {
            // check no uncomitted changes
            this.Close();
        }

        private void doNew()
        {
            // check no uncomitted changes
            TrapKATEditor.Reinit();
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
                    TrapKATEditor.OpenFile(ofdOpenSysEx.FileName);
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
            TrapKATEditor.SaveFile();
        }

        private void tsmiFileSaveAs_Click(object sender, EventArgs e)
        {
            DialogResult dr = sfdSaveSysEx.ShowDialog();
            if (DialogResult.OK.Equals(dr))
                TrapKATEditor.SaveFile(sfdSaveSysEx.FileName);
        }

        private void tsmiFileNew_Click(object sender, EventArgs e)
        {
            doNew();
        }
    }
}