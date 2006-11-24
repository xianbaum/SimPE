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
        public UI()
        {
            InitializeComponent();
        }

        #region UI

        private bool internalchg = false;
        private Instruction inst = null;
        private int nodeVersion = 0;
        private int nrArgs = 0;
        private TPRP tprp = null;
        private bool noOperands = false;

        public void Execute(Instruction inst)
        {
            internalchg = true;

            this.inst = inst;
            nodeVersion = inst.NodeVersion;

            pjse.FileTable.Entry ftEntry = inst.Parent.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, inst.OpCode);
            Bhav wrapper = new Bhav();
            wrapper.ProcessData(ftEntry.PFD, ftEntry.Package);
            nrArgs = wrapper.Header.ArgumentCount;
            tprp = wrapper.TPRPResource;

            wrappedByteArray ops1 = inst.Operands;
            wrappedByteArray ops2 = inst.Reserved1;

			noOperands = true;
			for (int i = 0; noOperands && i < 8; i++)
				noOperands = ops1[i] == 0xFF;

            Boolset b12 = ops2[4];
            if (!b12[0])
            {
            }
            else
            {
            }

            internalchg = false;

            return;
        }

        public Instruction Write(Instruction inst)
        {
            return inst;
        }

        #endregion
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
