/***************************************************************************
 *   Copyright (C) 2007 by Peter L Jones                                   *
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
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.PackedFiles.Wrapper;

namespace pjse.BhavOperandWizards.Wiz0x0008
{
	/// <summary>
	/// Summary description for BhavInstruction.
	/// </summary>
    internal class UI : System.Windows.Forms.Form, iBhavOperandWizForm
    {
        #region Form variables

        private System.Windows.Forms.TextBox tbval1;
        private System.Windows.Forms.TextBox tbval2;
        internal System.Windows.Forms.Panel pnWiz0x0008;
        private System.Windows.Forms.ComboBox cbPicker1;
        private System.Windows.Forms.ComboBox cbPicker2;
        private System.Windows.Forms.ComboBox cbDataOwner1;
        private System.Windows.Forms.ComboBox cbDataOwner2;
        private System.Windows.Forms.CheckBox cbDecimal;
        private System.Windows.Forms.CheckBox cbAttrPicker;
        private Label lbConst2;
        private Label lbConst1;
        private Label label2;
        private Label label1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        #endregion

        public UI()
        {
            //
            // Required designer variable.
            //
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
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


        private Instruction inst = null;
        private DataOwnerControl doid1 = null;
        private DataOwnerControl doid2 = null;

        #region iBhavOperandWizForm
        public Panel WizPanel { get { return this.pnWiz0x0008; } }
        public void Execute(Instruction inst)
        {
            this.inst = inst;

            wrappedByteArray ops = inst.Operands;

            doid1 = new DataOwnerControl(inst, this.cbDataOwner1, this.cbPicker1, this.tbval1, this.cbDecimal, this.cbAttrPicker, this.lbConst1,
                ops[0x02], (ushort)((ops[0x01] << 8) | ops[0x00]));
            doid2 = new DataOwnerControl(inst, this.cbDataOwner2, this.cbPicker2, this.tbval2, this.cbDecimal, this.cbAttrPicker, this.lbConst2,
                ops[0x06], (ushort)((ops[0x05] << 8) | ops[0x04]));
        }

        public Instruction Write(Instruction inst)
        {
            if (inst != null)
            {
                wrappedByteArray ops = inst.Operands;
                ops[0x02] = doid1.DataOwner;
                ops[0x00] = (byte)(doid1.Value & 0xff);
                ops[0x01] = (byte)((doid1.Value >> 8) & 0xff);
                ops[0x06] = doid2.DataOwner;
                ops[0x04] = (byte)(doid2.Value & 0xff);
                ops[0x05] = (byte)((doid2.Value >> 8) & 0xff);
            }
            return inst;
        }

        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnWiz0x0008 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbConst2 = new System.Windows.Forms.Label();
            this.lbConst1 = new System.Windows.Forms.Label();
            this.cbAttrPicker = new System.Windows.Forms.CheckBox();
            this.cbDecimal = new System.Windows.Forms.CheckBox();
            this.cbPicker2 = new System.Windows.Forms.ComboBox();
            this.cbPicker1 = new System.Windows.Forms.ComboBox();
            this.tbval2 = new System.Windows.Forms.TextBox();
            this.cbDataOwner2 = new System.Windows.Forms.ComboBox();
            this.tbval1 = new System.Windows.Forms.TextBox();
            this.cbDataOwner1 = new System.Windows.Forms.ComboBox();
            this.pnWiz0x0008.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnWiz0x0008
            // 
            this.pnWiz0x0008.Controls.Add(this.label2);
            this.pnWiz0x0008.Controls.Add(this.label1);
            this.pnWiz0x0008.Controls.Add(this.lbConst2);
            this.pnWiz0x0008.Controls.Add(this.lbConst1);
            this.pnWiz0x0008.Controls.Add(this.cbAttrPicker);
            this.pnWiz0x0008.Controls.Add(this.cbDecimal);
            this.pnWiz0x0008.Controls.Add(this.cbPicker2);
            this.pnWiz0x0008.Controls.Add(this.cbPicker1);
            this.pnWiz0x0008.Controls.Add(this.tbval2);
            this.pnWiz0x0008.Controls.Add(this.cbDataOwner2);
            this.pnWiz0x0008.Controls.Add(this.tbval1);
            this.pnWiz0x0008.Controls.Add(this.cbDataOwner1);
            this.pnWiz0x0008.Location = new System.Drawing.Point(0, 0);
            this.pnWiz0x0008.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnWiz0x0008.Name = "pnWiz0x0008";
            this.pnWiz0x0008.Size = new System.Drawing.Size(365, 139);
            this.pnWiz0x0008.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Set:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 62);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "to a random number from zero to:";
            // 
            // lbConst2
            // 
            this.lbConst2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbConst2.AutoSize = true;
            this.lbConst2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbConst2.Location = new System.Drawing.Point(20, 106);
            this.lbConst2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbConst2.Name = "lbConst2";
            this.lbConst2.Size = new System.Drawing.Size(69, 13);
            this.lbConst2.TabIndex = 8;
            this.lbConst2.Text = "Const2 value";
            // 
            // lbConst1
            // 
            this.lbConst1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbConst1.AutoSize = true;
            this.lbConst1.Location = new System.Drawing.Point(20, 44);
            this.lbConst1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbConst1.Name = "lbConst1";
            this.lbConst1.Size = new System.Drawing.Size(69, 13);
            this.lbConst1.TabIndex = 8;
            this.lbConst1.Text = "Const1 value";
            // 
            // cbAttrPicker
            // 
            this.cbAttrPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAttrPicker.AutoSize = true;
            this.cbAttrPicker.Location = new System.Drawing.Point(239, 121);
            this.cbAttrPicker.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbAttrPicker.Name = "cbAttrPicker";
            this.cbAttrPicker.Size = new System.Drawing.Size(117, 17);
            this.cbAttrPicker.TabIndex = 7;
            this.cbAttrPicker.Text = "use Attribute picker";
            // 
            // cbDecimal
            // 
            this.cbDecimal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbDecimal.AutoSize = true;
            this.cbDecimal.Location = new System.Drawing.Point(86, 121);
            this.cbDecimal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbDecimal.Name = "cbDecimal";
            this.cbDecimal.Size = new System.Drawing.Size(140, 17);
            this.cbDecimal.TabIndex = 6;
            this.cbDecimal.Text = "Decimal (except Consts)";
            // 
            // cbPicker2
            // 
            this.cbPicker2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbPicker2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker2.DropDownWidth = 384;
            this.cbPicker2.Location = new System.Drawing.Point(246, 83);
            this.cbPicker2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbPicker2.Name = "cbPicker2";
            this.cbPicker2.Size = new System.Drawing.Size(117, 21);
            this.cbPicker2.TabIndex = 5;
            this.cbPicker2.Visible = false;
            // 
            // cbPicker1
            // 
            this.cbPicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker1.DropDownWidth = 384;
            this.cbPicker1.Location = new System.Drawing.Point(246, 22);
            this.cbPicker1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbPicker1.Name = "cbPicker1";
            this.cbPicker1.Size = new System.Drawing.Size(117, 21);
            this.cbPicker1.TabIndex = 2;
            this.cbPicker1.Visible = false;
            // 
            // tbval2
            // 
            this.tbval2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbval2.Location = new System.Drawing.Point(246, 83);
            this.tbval2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbval2.Name = "tbval2";
            this.tbval2.Size = new System.Drawing.Size(117, 20);
            this.tbval2.TabIndex = 5;
            // 
            // cbDataOwner2
            // 
            this.cbDataOwner2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbDataOwner2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner2.DropDownWidth = 384;
            this.cbDataOwner2.Location = new System.Drawing.Point(20, 83);
            this.cbDataOwner2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbDataOwner2.Name = "cbDataOwner2";
            this.cbDataOwner2.Size = new System.Drawing.Size(228, 21);
            this.cbDataOwner2.TabIndex = 4;
            // 
            // tbval1
            // 
            this.tbval1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbval1.Location = new System.Drawing.Point(246, 22);
            this.tbval1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbval1.Name = "tbval1";
            this.tbval1.Size = new System.Drawing.Size(117, 20);
            this.tbval1.TabIndex = 2;
            // 
            // cbDataOwner1
            // 
            this.cbDataOwner1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbDataOwner1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner1.DropDownWidth = 384;
            this.cbDataOwner1.Location = new System.Drawing.Point(20, 22);
            this.cbDataOwner1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbDataOwner1.Name = "cbDataOwner1";
            this.cbDataOwner1.Size = new System.Drawing.Size(228, 21);
            this.cbDataOwner1.TabIndex = 1;
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 158);
            this.Controls.Add(this.pnWiz0x0008);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UI";
            this.Text = "UI";
            this.pnWiz0x0008.ResumeLayout(false);
            this.pnWiz0x0008.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

    }
}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWiz0x0008 : pjse.ABhavOperandWiz
	{
		public BhavOperandWiz0x0008(Instruction i) : base(i) { myForm = new Wiz0x0008.UI(); }

		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm = null;
		}
		#endregion

	}

}
