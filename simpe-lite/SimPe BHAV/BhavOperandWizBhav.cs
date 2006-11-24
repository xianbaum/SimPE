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
    internal partial class UI : Form
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
        }

        #region UI

        private bool internalchg = false;
        private byte[] operands = null;
        private Instruction inst = null;
        private byte nodeVersion = 0;
        private byte nrArgs = 0;
        private Label[] albParams = null;
        private Panel[] apnParams = null;
        private dataFormat format = dataFormat.none;
        private DataOwnerControl[] adoid = null;
        private ComboBox[] acbDO = null;
        private ComboBox[] acbP = null;
        private TextBox[] atbV = null;
        private Label[] albC = null;

        private bool hex8_IsValid(object sender)
        {
            if (!sender.Equals(this.tbNodeVersion))
                throw new Exception("hex8_IsValid not applicable to control " + sender.ToString());
            try { Convert.ToByte(((TextBox)sender).Text, 16); }
            catch (Exception) { return false; }
            return true;
        }

        private bool NoOperands
        {
            get
            {
                if (nodeVersion > 0)
                    return false;
                else
                {
                    bool noOperands = true;
                    for (int i = 0; noOperands && i < 8; i++)
                        noOperands = operands[i] == 0xFF;
                    return noOperands;
                }
            }
        }

        private void doFormat()
        {
            this.cbParams.SelectedIndex = (int)format;
            cbAttrPicker.Visible = cbDecimal.Visible = format == dataFormat.newformat;

            byte[] o = operands; // lazy...
            for (int i = 0; i < nrArgs && i < apnParams.Length; i++)
            {
                apnParams[i].Enabled = format != dataFormat.none && format != dataFormat.caller;
                acbDO[i].Enabled = format != dataFormat.oldformat;
                switch (format)
                {
                    case dataFormat.none:
                        adoid[i] = new DataOwnerControl(inst, acbDO[i], acbP[i], atbV[i], cbDecimal, cbAttrPicker, albC[i]
                            , 0x07, 0);
                        break;
                    case dataFormat.caller:
                        adoid[i] = new DataOwnerControl(inst, acbDO[i], acbP[i], atbV[i], cbDecimal, cbAttrPicker, albC[i]
                            , 0x09, (ushort)i);
                        break;
                    case dataFormat.oldformat:
                        adoid[i] = new DataOwnerControl(inst, acbDO[i], acbP[i], atbV[i], cbDecimal, cbAttrPicker, albC[i]
                            , 0x07, BhavWiz.ToShort(o[i * 2], o[i * 2 + 1]));
                        break;
                    case dataFormat.newformat:
                        if (i < 4)
                            adoid[i] = new DataOwnerControl(inst, acbDO[i], acbP[i], atbV[i], cbDecimal, cbAttrPicker, albC[i]
                                , o[i * 3], BhavWiz.ToShort(o[(i * 3) + 1], o[(i * 3) + 2]));
                        break;
                }
            }
        }

        private void doNodeVersion()
        {
            if (NoOperands)
                format = dataFormat.none;
            else
            {
                Boolset b12 = operands[12];

                format = b12[0]
                    ? dataFormat.newformat
                    : (nodeVersion == 0 || !b12[1] ? dataFormat.oldformat : dataFormat.caller);
            }
            doFormat();
        }

        private void setFormat(dataFormat newformat)
        {
            if (format == newformat) return;
            format = newformat;
            if (format == dataFormat.none) { for (int i = 0; i < 8; i++) operands[i] = 0xff; return; }
            if (format == dataFormat.newformat) { operands[12] |= 0x01; return; }
            operands[12] &= 0xfe;
            if (format == dataFormat.caller) { operands[12] |= 0x02; return; }
            if (nodeVersion > 0) operands[12] &= 0xfd;
        }

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
            adoid = new DataOwnerControl[nrArgs];

            this.lbBhavName.Text = "0x" + SimPe.Helper.HexString(inst.OpCode) + ": " + wrapper.FileName;
            this.lbArgC.Text = "0x" + SimPe.Helper.HexString(nrArgs);
            this.tbNodeVersion.Text = "0x" + SimPe.Helper.HexString(nodeVersion);

            operands = new byte[16];
            ((byte[])inst.Operands).CopyTo(operands, 0);
            ((byte[])inst.Reserved1).CopyTo(operands, 8);

            for (int i = 0; i < nrArgs; i++)
                if (tprp != null && i < tprp.Count) albParams[i].Text = tprp[false, i].Label;
            for (int i = nrArgs; i < albParams.Length; i++)
            {
                albParams[i].Visible = apnParams[i].Visible = false;
                tlpHeader.Controls.Remove(albParams[i]);
                tlpHeader.Controls.Remove(apnParams[i]);
            }
            doNodeVersion();

            internalchg = false;

            return;
        }

        public Instruction Write(Instruction inst)
        {
            if (inst != null)
            {
                switch (format)
                {
                    case dataFormat.none: break;
                    case dataFormat.caller: break;
                    case dataFormat.oldformat:
                        for (int i = 0; i < nrArgs && i < 8; i++)
                        {
                            operands[i * 2] = (byte)(adoid[i].Value & 0xff);
                            operands[i * 2 + 1] = (byte)(adoid[i].Value >> 8 & 0xff);
                        }
                        break;
                    case dataFormat.newformat:
                        for (int i = 0; i < nrArgs && i < 4; i++)
                        {
                            operands[i * 3] = adoid[i].DataOwner;
                            operands[i * 3 + 1] = (byte)(adoid[i].Value & 0xff);
                            operands[i * 3 + 2] = (byte)(adoid[i].Value >> 8 & 0xff);
                        }
                        break;
                }
                for (int i = 0; i < 8; i++) inst.Operands[i] = operands[i];
                for (int i = 0; i < 8; i++) inst.Reserved1[i] = operands[i + 8];
            }
            return inst;
        }

        #endregion

        private void cbParams_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (nodeVersion == 0 && cbParams.SelectedIndex == (int)dataFormat.caller
                || nodeVersion > 0 && cbParams.SelectedIndex == (int)dataFormat.none)
                cbParams.SelectedIndex = (int)dataFormat.oldformat;

            setFormat((dataFormat)Enum.Parse(format.GetType(), cbParams.SelectedIndex.ToString()));

            doFormat();
        }

        private void hex8_TextChanged(object sender, System.EventArgs ev)
        {
            if (internalchg) return;
            if (!hex8_IsValid(sender)) return;

            internalchg = true;

            nodeVersion = Convert.ToByte(((TextBox)sender).Text, 16);
            doNodeVersion();

            internalchg = false;
        }

        private void hex8_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (hex8_IsValid(sender)) return;

            e.Cancel = true;

            nodeVersion = inst.NodeVersion;
            doNodeVersion();

            ((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(inst.NodeVersion);
            ((TextBox)sender).SelectAll();
        }

        private void hex8_Validated(object sender, System.EventArgs e)
        {
            bool origstate = internalchg;
            internalchg = true;
            ((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(Convert.ToByte(((TextBox)sender).Text, 16));
            ((TextBox)sender).SelectAll();
            internalchg = origstate;
        }
    }
}

namespace pjse.BhavOperandWizards
{
    public class BhavOperandWizBhav : pjse.ABhavOperandWiz
    {
        public BhavOperandWizBhav() : base() { }

        public BhavOperandWizBhav(Instruction i) : base(i) { }


        private WizBhav.UI myForm = null;
        public override Panel bhavPrimWizPanel
        {
            get
            {
                if (myForm == null) myForm = new WizBhav.UI();
                return myForm.pnWizBhav;
            }
        }

        public override void Execute()
        {
            if (instruction != null) myForm.Execute(instruction);
        }

        public override Instruction Write()
        {
            return (instruction == null) ? null : myForm.Write(instruction);
        }


        #region IDisposable Members
        public override void Dispose()
        {
            if (myForm != null) myForm = null;
        }
        #endregion

    }

}
