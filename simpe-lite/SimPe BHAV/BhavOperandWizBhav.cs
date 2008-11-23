/***************************************************************************
 *   Copyright (C) 2006-2008 by Peter L Jones                              *
 *   peter@users.sf.net                                                    *
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
using SimPe.PackedFiles.Wrapper;

namespace pjse.BhavOperandWizards.WizBhav
{
    internal partial class UI : Form, iBhavOperandWizForm
    {
        enum dataFormat : int
        {
            none, caller, oldformat, newformat
        }

        public UI()
        {
            InitializeComponent();

            albArg = new Label[] { lbArg1, lbArg2, lbArg3, lbArg4, lbArg5, lbArg6, lbArg7, lbArg8, };
            aldoc = new LabelledDataOwner[] { ldocArg1, ldocArg2, ldocArg3, ldocArg4, ldocArg5, ldocArg6, ldocArg7, ldocArg8, };
            arbFormat = new List<RadioButton>(new RadioButton[] { rbNone, rbCallers, rbOld, rbNew });

            pjse.Settings.PJSE.DecimalDOValueChanged += new EventHandler(PJSE_DecimalDOValueChanged);
            pjse.Settings.PJSE.InstancePickerAsTextChanged += new EventHandler(PJSE_InstancePickerAsTextChanged);
        }

        void PJSE_DecimalDOValueChanged(object sender, EventArgs e)
        {
            if (ckbDecimal.Checked != pjse.Settings.PJSE.DecimalDOValue)
                ckbDecimal.Checked = pjse.Settings.PJSE.DecimalDOValue;
        }

        void PJSE_InstancePickerAsTextChanged(object sender, EventArgs e)
        {
            if (ckbUseInstancePicker.Checked != pjse.Settings.PJSE.InstancePickerAsText)
                ckbUseInstancePicker.Checked = pjse.Settings.PJSE.InstancePickerAsText;
        }

        private bool internalchg = false;
        private byte[] operands = null;
        private Instruction inst = null;
        private byte nodeVersion = 0;
        private byte nrArgs = 0;
        private dataFormat format = dataFormat.none;
        private Label[] albArg = null;
        private LabelledDataOwner[] aldoc = null;
        private List<RadioButton> arbFormat = null;

        private void doFormat()
        {
            byte[] o = operands; // lazy...
            pnWizBhav.SuspendLayout();
            ckbUseInstancePicker.Enabled = format == dataFormat.newformat;
            ckbDecimal.Enabled = format != dataFormat.caller && format != dataFormat.none;

            for (int i = 0; i < aldoc.Length; i++)
            {
                aldoc[i].Enabled = (format != dataFormat.none && format != dataFormat.caller)
                    && !(format == dataFormat.newformat && i >= 4);
                aldoc[i].DataOwnerEnabled = format != dataFormat.oldformat;
                switch (format)
                {
                    case dataFormat.none:
                        aldoc[i].Value = 0;
                        aldoc[i].DataOwner = 0x07;
                        break;
                    case dataFormat.caller:
                        aldoc[i].Value = (ushort)i;
                        aldoc[i].DataOwner = 0x09;
                        break;
                    case dataFormat.oldformat:
                        aldoc[i].Value = BhavWiz.ToShort(o[i * 2], o[i * 2 + 1]);
                        aldoc[i].DataOwner = 0x07;
                        break;
                    case dataFormat.newformat:
                        if (i < 4)
                        {
                            aldoc[i].Value = BhavWiz.ToShort(o[(i * 3) + 1], o[(i * 3) + 2]);
                            aldoc[i].DataOwner = o[i * 3];
                        }
                        else
                        {
                            aldoc[i].Value = 0;
                            aldoc[i].DataOwner = 0x07;
                        }
                        break;
                }
            }
            pnWizBhav.ResumeLayout();
        }

        private void setFormat(dataFormat newformat)
        {
            if (format == newformat) return;
            updateOperands();
            format = newformat;
            if (format != dataFormat.newformat) operands[12] &= 0xfe;
            if (format != dataFormat.caller && nodeVersion > 0) operands[12] &= 0xfd;
        }

        private void updateOperands()
        {
            switch (format)
            {
                case dataFormat.none: for (int i = 0; i < 8; i++) operands[i] = 0xff; break;
                case dataFormat.caller: operands[12] &= 0xfe; operands[12] |= 0x02; break;
                case dataFormat.oldformat:
                    for (int i = 0; i < 8; i++)
                        BhavWiz.FromShort(ref operands, i * 2, aldoc[i].Value);
                    operands[12] &= 0xfe;
                    if (nodeVersion > 0) operands[12] &= 0xfd;
                    break;
                case dataFormat.newformat:
                    for (int i = 0; i < 4; i++)
                    {
                        operands[i * 3] = aldoc[i].DataOwner;
                        BhavWiz.FromShort(ref operands, i * 3 + 1, aldoc[i].Value);
                    }
                    operands[12] |= 0x01;
                    break;
            }
        }

        #region iBhavOperandWizForm
        public Panel WizPanel { get { return this.pnWizBhav; } }

        public void Execute(Instruction inst)
        {
            ckbDecimal.Checked = pjse.Settings.PJSE.DecimalDOValue;
            ckbUseInstancePicker.Checked = pjse.Settings.PJSE.InstancePickerAsText;

            internalchg = true;

            this.inst = inst;
            foreach (LabelledDataOwner ldoc in aldoc) ldoc.Instruction = inst;

            nodeVersion = inst.NodeVersion;

            pjse.FileTable.Entry ftEntry = inst.Parent.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, inst.OpCode);
            TPRP tprp = null;
            if (ftEntry != null)
            {
                Bhav wrapper = (Bhav)ftEntry.Wrapper;
                tprp = (TPRP)wrapper.SiblingResource(TPRP.TPRPtype);
                nrArgs = wrapper.Header.ArgumentCount;

                this.lbBhavName.Text = "0x" + SimPe.Helper.HexString(inst.OpCode) + ": " + wrapper.FileName;
                this.lbArgC.Text = "0x" + SimPe.Helper.HexString(nrArgs);
            }
            else
            {
                this.lbBhavName.Text = "0x" + SimPe.Helper.HexString(inst.OpCode) + ": [" + pjse.Localization.GetString("bhavnotfound") + "]";
                this.lbArgC.Text = "(???)";
            }

            operands = new byte[16];
            ((byte[])inst.Operands).CopyTo(operands, 0);
            ((byte[])inst.Reserved1).CopyTo(operands, 8);

            for (int i = 0; i < nrArgs; i++)
                if (tprp != null && !tprp.TextOnly && i < tprp.ParamCount) albArg[i].Text = tprp[false, i].Label;
                else albArg[i].Text = pjse.Localization.GetString("unk");
            for (int i = nrArgs; i < albArg.Length; i++)
                albArg[i].Text = pjse.Localization.GetString("bwb_unused");

            bool noOperands = true;
            if (nodeVersion > 0)
                noOperands = false;
            else
                for (int i = 0; noOperands && i < 8; i++)
                    noOperands = operands[i] == 0xFF;

            if (noOperands)
                format = dataFormat.none;
            else
            {
                Boolset b12 = operands[12];
                format = b12[0]
                    ? dataFormat.newformat
                    : (nodeVersion == 0 || !b12[1] ? dataFormat.oldformat : dataFormat.caller);
            }
            rbNone.Enabled = nodeVersion == 0;
            rbCallers.Enabled = nodeVersion > 0;
            rbCallers.Checked = format == dataFormat.caller;
            rbNew.Checked = format == dataFormat.newformat;
            rbNone.Checked = format == dataFormat.none;
            rbOld.Checked = format == dataFormat.oldformat;

            doFormat();

            internalchg = false;

            return;
        }

        public Instruction Write(Instruction inst)
        {
            if (inst != null)
            {
                updateOperands();
                for (int i = 0; i < 8; i++) inst.Operands[i] = operands[i];
                for (int i = 0; i < 8; i++) inst.Reserved1[i] = operands[i + 8];
            }
            return inst;
        }

        #endregion

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;

            int i = arbFormat.IndexOf((RadioButton)sender);
            if (!arbFormat[i].Checked) return;
            setFormat((dataFormat)Enum.Parse(format.GetType(), i.ToString()));
            doFormat();
        }

        private void ckbDecimal_CheckedChanged(object sender, EventArgs e)
        {
            pjse.Settings.PJSE.DecimalDOValue = ckbDecimal.Checked;
        }

        private void ckbUseInstancePicker_CheckedChanged(object sender, EventArgs e)
        {
            pjse.Settings.PJSE.InstancePickerAsText = ckbUseInstancePicker.Checked;
        }
    }
}

namespace pjse.BhavOperandWizards
{
    public class BhavOperandWizBhav : pjse.ABhavOperandWiz
    {
        public BhavOperandWizBhav(Instruction i) : base(i) { myForm = new WizBhav.UI(); }

        #region IDisposable Members
        public override void Dispose()
        {
            if (myForm != null) myForm = null;
        }
        #endregion

    }

}
