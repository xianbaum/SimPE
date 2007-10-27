/***************************************************************************
 *   Copyright (C) 2006 by Peter L Jones                                   *
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

            albParams = new Label[] {
                lbParam0, lbParam1, lbParam2, lbParam3,
                lbParam4, lbParam5, lbParam6, lbParam7,
            };

            apnParams = new Panel[] {
                pnParam0, pnParam1, pnParam2, pnParam3,
                pnParam4, pnParam5, pnParam6, pnParam7,
            };

            acbDO = new ComboBox[] {
                cbDataOwner0, cbDataOwner1, cbDataOwner2, cbDataOwner3,
                cbDataOwner4, cbDataOwner5, cbDataOwner6, cbDataOwner7
            };

            acbP = new ComboBox[] {
                cbPicker0, cbPicker1, cbPicker2, cbPicker3,
                cbPicker4, cbPicker5, cbPicker6, cbPicker7
            };

            atbV = new TextBox[] { tbVal0, tbVal1, tbVal2, tbVal3, tbVal4, tbVal5, tbVal6, tbVal7 };

            albC = new Label[] {
                lbConst0, lbConst1, lbConst2, lbConst3,
                lbConst4, lbConst5, lbConst6, lbConst7
            };

            adoid = new DataOwnerControl[8];
            for (int i = 0; i < adoid.Length; i++)
                adoid[i] = new DataOwnerControl(null, acbDO[i], acbP[i], atbV[i], cbDecimal, cbAttrPicker, albC[i], 0x07, (ushort)0x00);

            arbFormat = new List<RadioButton>(new RadioButton[] { rbNone, rbCallers, rbOld, rbNew });
        }

        private bool internalchg = false;
        private byte[] operands = null;
        private Instruction inst = null;
        private byte nodeVersion = 0;
        private byte nrArgs = 0;
        private Label[] albParams = null;
        private Panel[] apnParams = null;
        private dataFormat format = dataFormat.none;
        private ComboBox[] acbDO = null;
        private ComboBox[] acbP = null;
        private TextBox[] atbV = null;
        private Label[] albC = null;
        private DataOwnerControl[] adoid = null;
        private List<RadioButton> arbFormat = null;

        private void doFormat()
        {
            cbAttrPicker.Enabled = format == dataFormat.newformat;
            cbDecimal.Enabled = format != dataFormat.caller && format != dataFormat.none;

            byte[] o = operands; // lazy...
            pnWizBhav.SuspendLayout();
            for (int i = 0; i < apnParams.Length; i++)
            {
                apnParams[i].Enabled = (format != dataFormat.none && format != dataFormat.caller)
                    && !(format == dataFormat.newformat && i >= 4);
                acbDO[i].Enabled = format != dataFormat.oldformat;
                switch (format)
                {
                    case dataFormat.none:
                        adoid[i].SetDataOwnerControl(inst, acbDO[i], acbP[i], atbV[i], cbDecimal, cbAttrPicker, albC[i]
                            , 0x07, 0);
                        break;
                    case dataFormat.caller:
                        adoid[i].SetDataOwnerControl(inst, acbDO[i], acbP[i], atbV[i], cbDecimal, cbAttrPicker, albC[i]
                            , 0x09, (ushort)i);
                        break;
                    case dataFormat.oldformat:
                        adoid[i].SetDataOwnerControl(inst, acbDO[i], acbP[i], atbV[i], cbDecimal, cbAttrPicker, albC[i]
                            , 0x07, BhavWiz.ToShort(o[i * 2], o[i * 2 + 1]));
                        break;
                    case dataFormat.newformat:
                        if (i < 4)
                            adoid[i].SetDataOwnerControl(inst, acbDO[i], acbP[i], atbV[i], cbDecimal, cbAttrPicker, albC[i]
                                , o[i * 3], BhavWiz.ToShort(o[(i * 3) + 1], o[(i * 3) + 2]));
                        else
                            adoid[i].SetDataOwnerControl(inst, acbDO[i], acbP[i], atbV[i], cbDecimal, cbAttrPicker, albC[i]
                                , 0x07, 0);
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
                        BhavWiz.FromShort(ref operands, i * 2, adoid[i].Value);
                    operands[12] &= 0xfe;
                    if (nodeVersion > 0) operands[12] &= 0xfd;
                    break;
                case dataFormat.newformat:
                    for (int i = 0; i < 4; i++)
                    {
                        operands[i * 3] = adoid[i].DataOwner;
                        BhavWiz.FromShort(ref operands, i * 3 + 1, adoid[i].Value);
                    }
                    operands[12] |= 0x01;
                    break;
            }
        }

        #region iBhavOperandWizForm
        public Panel WizPanel { get { return this.pnWizBhav; } }

        public void Execute(Instruction inst)
        {
            internalchg = true;

            this.inst = inst;
            nodeVersion = inst.NodeVersion;

            pjse.FileTable.Entry ftEntry = inst.Parent.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, inst.OpCode);
            Bhav wrapper = new Bhav();
            wrapper.ProcessData(ftEntry.PFD, ftEntry.Package);
            TPRP tprp = wrapper.TPRPResource;
            nrArgs = wrapper.Header.ArgumentCount;

            this.lbBhavName.Text = "0x" + SimPe.Helper.HexString(inst.OpCode) + ": " + wrapper.FileName;
            this.lbArgC.Text = "0x" + SimPe.Helper.HexString(nrArgs);

            operands = new byte[16];
            ((byte[])inst.Operands).CopyTo(operands, 0);
            ((byte[])inst.Reserved1).CopyTo(operands, 8);

            for (int i = 0; i < nrArgs; i++)
                if (tprp != null && i < tprp.Count) albParams[i].Text = tprp[false, i].Label;
            for (int i = nrArgs; i < albParams.Length; i++)
                albParams[i].Text = pjse.Localization.GetString("bwb_unused");

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
