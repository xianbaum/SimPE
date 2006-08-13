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
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.PackedFiles.Wrapper;

namespace pjse.BhavOperandWizards.Wiz0x0002
{
	/// <summary>
	/// Zusammenfassung für BhavInstruction.
	/// </summary>
    internal class UI : System.Windows.Forms.Form
    {
        #region Form variables

        private System.Windows.Forms.TextBox tbval1;
        private System.Windows.Forms.TextBox tbval2;
        internal System.Windows.Forms.Panel pnWiz0x0002;
        private System.Windows.Forms.ComboBox cbPicker1;
        private System.Windows.Forms.ComboBox cbPicker2;
        private System.Windows.Forms.ComboBox cbOperator;
        private System.Windows.Forms.ComboBox cbDataOwner1;
        private System.Windows.Forms.ComboBox cbDataOwner2;
        private System.Windows.Forms.CheckBox cbDecimal;
        private System.Windows.Forms.CheckBox cbAttrPicker;
        private Label lbConst2;
        private Label lbConst1;
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        #endregion

        public UI()
        {
            //
            // Erforderlich für die Windows Form-Designerunterstützung
            //
            InitializeComponent();
        }

        /// <summary>
        /// Die verwendeten Ressourcen bereinigen.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);

            inst = null;
        }


        #region UI
        private Instruction inst = null;
        private DataOwnerControl doid1 = null;
        private DataOwnerControl doid2 = null;

        public void Execute(Instruction inst)
        {
            this.inst = inst;

            wrappedByteArray ops = inst.Operands;

            doid1 = new DataOwnerControl(inst, this.cbDataOwner1, this.cbPicker1, this.tbval1, this.cbDecimal, this.cbAttrPicker, this.lbConst1,
                ops[0x06], (ushort)((ops[0x01] << 8) | ops[0x00]));
            doid2 = new DataOwnerControl(inst, this.cbDataOwner2, this.cbPicker2, this.tbval2, this.cbDecimal, this.cbAttrPicker, this.lbConst2,
                ops[0x07], (ushort)((ops[0x03] << 8) | ops[0x02]));
            doid2.FlagsFor = doid1;

            cbOperator.Items.Clear();
            cbOperator.Items.AddRange(BhavWiz.readStr(GS.BhavStr.Operators).ToArray());
            cbOperator.SelectedIndex = (cbOperator.Items.Count > ops[0x05]) ? ops[0x05] : -1;
        }

        public Instruction Write(Instruction inst)
        {
            if (inst != null)
            {
                wrappedByteArray ops = inst.Operands;
                ops[0x06] = doid1.DataOwner;
                ops[0x00] = (byte)(doid1.Value & 0xff);
                ops[0x01] = (byte)((doid1.Value >> 8) & 0xff);
                ops[0x05] = (byte)cbOperator.SelectedIndex;
                ops[0x07] = doid2.DataOwner;
                ops[0x02] = (byte)(doid2.Value & 0xff);
                ops[0x03] = (byte)((doid2.Value >> 8) & 0xff);
            }
            return inst;
        }

        #endregion

        #region Vom Windows Form-Designer generierter Code
        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI));
            this.pnWiz0x0002 = new System.Windows.Forms.Panel();
            this.lbConst2 = new System.Windows.Forms.Label();
            this.lbConst1 = new System.Windows.Forms.Label();
            this.cbAttrPicker = new System.Windows.Forms.CheckBox();
            this.cbDecimal = new System.Windows.Forms.CheckBox();
            this.cbPicker2 = new System.Windows.Forms.ComboBox();
            this.cbPicker1 = new System.Windows.Forms.ComboBox();
            this.cbOperator = new System.Windows.Forms.ComboBox();
            this.tbval2 = new System.Windows.Forms.TextBox();
            this.cbDataOwner2 = new System.Windows.Forms.ComboBox();
            this.tbval1 = new System.Windows.Forms.TextBox();
            this.cbDataOwner1 = new System.Windows.Forms.ComboBox();
            this.pnWiz0x0002.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnWiz0x0002
            // 
            this.pnWiz0x0002.Controls.Add(this.lbConst2);
            this.pnWiz0x0002.Controls.Add(this.lbConst1);
            this.pnWiz0x0002.Controls.Add(this.cbAttrPicker);
            this.pnWiz0x0002.Controls.Add(this.cbDecimal);
            this.pnWiz0x0002.Controls.Add(this.cbPicker2);
            this.pnWiz0x0002.Controls.Add(this.cbPicker1);
            this.pnWiz0x0002.Controls.Add(this.cbOperator);
            this.pnWiz0x0002.Controls.Add(this.tbval2);
            this.pnWiz0x0002.Controls.Add(this.cbDataOwner2);
            this.pnWiz0x0002.Controls.Add(this.tbval1);
            this.pnWiz0x0002.Controls.Add(this.cbDataOwner1);
            resources.ApplyResources(this.pnWiz0x0002, "pnWiz0x0002");
            this.pnWiz0x0002.Name = "pnWiz0x0002";
            // 
            // lbConst2
            // 
            resources.ApplyResources(this.lbConst2, "lbConst2");
            this.lbConst2.Name = "lbConst2";
            // 
            // lbConst1
            // 
            resources.ApplyResources(this.lbConst1, "lbConst1");
            this.lbConst1.Name = "lbConst1";
            // 
            // cbAttrPicker
            // 
            resources.ApplyResources(this.cbAttrPicker, "cbAttrPicker");
            this.cbAttrPicker.Name = "cbAttrPicker";
            // 
            // cbDecimal
            // 
            resources.ApplyResources(this.cbDecimal, "cbDecimal");
            this.cbDecimal.Name = "cbDecimal";
            // 
            // cbPicker2
            // 
            this.cbPicker2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker2.DropDownWidth = 384;
            resources.ApplyResources(this.cbPicker2, "cbPicker2");
            this.cbPicker2.Name = "cbPicker2";
            // 
            // cbPicker1
            // 
            this.cbPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker1.DropDownWidth = 384;
            resources.ApplyResources(this.cbPicker1, "cbPicker1");
            this.cbPicker1.Name = "cbPicker1";
            // 
            // cbOperator
            // 
            this.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbOperator, "cbOperator");
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.SelectedIndexChanged += new System.EventHandler(this.cbOperator_SelectedIndexChanged);
            // 
            // tbval2
            // 
            resources.ApplyResources(this.tbval2, "tbval2");
            this.tbval2.Name = "tbval2";
            // 
            // cbDataOwner2
            // 
            this.cbDataOwner2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner2.DropDownWidth = 384;
            resources.ApplyResources(this.cbDataOwner2, "cbDataOwner2");
            this.cbDataOwner2.Name = "cbDataOwner2";
            // 
            // tbval1
            // 
            resources.ApplyResources(this.tbval1, "tbval1");
            this.tbval1.Name = "tbval1";
            // 
            // cbDataOwner1
            // 
            this.cbDataOwner1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner1.DropDownWidth = 384;
            resources.ApplyResources(this.cbDataOwner1, "cbDataOwner1");
            this.cbDataOwner1.Name = "cbDataOwner1";
            // 
            // UI
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pnWiz0x0002);
            this.Name = "UI";
            this.pnWiz0x0002.ResumeLayout(false);
            this.pnWiz0x0002.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private void cbOperator_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cbOperator.SelectedIndex >= 8 && cbOperator.SelectedIndex <= 10)
                doid2.UseFlagNames = true;
            else
                doid2.UseFlagNames = false;
        }
    }
}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWiz0x0002 : pjse.ABhavOperandWiz
	{
		public BhavOperandWiz0x0002() : base() { }

		public BhavOperandWiz0x0002(Instruction i) : base(i) { }


		private Wiz0x0002.UI myForm = null;
		public override Panel bhavPrimWizPanel
		{
			get
			{
				if (myForm == null) myForm = new Wiz0x0002.UI();
				return myForm.pnWiz0x0002;
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
