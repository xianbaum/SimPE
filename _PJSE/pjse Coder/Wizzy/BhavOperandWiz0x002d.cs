/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   pljones@users.sf.net                                                  *
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
 *   59 Temple Place - Suite 330, Boston, MA  32111-1307, USA.             *
 ***************************************************************************/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.PackedFiles.Wrapper;

namespace pjse.BhavOperandWizards.Wiz0x002d
{
    /// <summary>
    /// Summary description for StrBig.
    /// </summary>
    internal class UI : System.Windows.Forms.Form, iBhavOperandWizForm
	{
		#region Form variables

        internal System.Windows.Forms.Panel pnWiz0x002d;
        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox gbRoutingSlot;
        private Panel pnObject;
        private ComboBox cbSlotType;
        private CheckBox ckbDecimal;
        private TextBox tbVal1;
        private CheckBox ckbNFailTrees;
        private CheckBox ckbIgnDstFootprint;
        private CheckBox ckbDiffAlts;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public UI()
		{
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );

			inst = null;
		}


		private Instruction inst = null;
		private DataOwnerControl doid1 = null;
        //private bool internalchg = false;

        #region iBhavOperandWizForm
        public Panel WizPanel { get { return this.pnWiz0x002d; } }

        public void Execute(Instruction inst)
        {
            this.inst = inst;

            wrappedByteArray ops1 = inst.Operands;
            wrappedByteArray ops2 = inst.Reserved1;
            Boolset ops14 = ops1[4];

            //internalchg = true;

            doid1 = new DataOwnerControl(inst, null, null, this.tbVal1, this.ckbDecimal, null, null,
                0x07, BhavWiz.ToShort(ops1[0x00], ops1[0x01])); // Literal

            int i = 0;
            if (!ops14[1]) i = BhavWiz.ToShort(ops1[2], ops1[3]);
            if (i < cbSlotType.Items.Count) cbSlotType.SelectedIndex = i;

            ckbNFailTrees.Checked = ops14[0];
            ckbIgnDstFootprint.Checked = ops14[2];
            ckbDiffAlts.Checked = ops14[3];

            //internalchg = false;
        }

		public Instruction Write(Instruction inst)
		{
			if (inst != null)
			{
                wrappedByteArray ops1 = inst.Operands;
                wrappedByteArray ops2 = inst.Reserved1;
                Boolset ops14 = ops1[4];

                ops1[0] = (byte)doid1.Value;
                ops1[1] = (byte)(doid1.Value >> 8);

                if (cbSlotType.SelectedIndex >= 1)
                {
                    ops1[2] = (byte)(cbSlotType.SelectedIndex - 1);
                    ops1[3] = (byte)((cbSlotType.SelectedIndex - 1) >> 8);
                }

                ops14[0] = ckbNFailTrees.Checked;
                ops14[1] = (cbSlotType.SelectedIndex == 0);
                ops14[2] = ckbIgnDstFootprint.Checked;
                ops14[3] = ckbDiffAlts.Checked;
                ops1[4] = ops14;

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
            this.pnWiz0x002d = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbRoutingSlot = new System.Windows.Forms.GroupBox();
            this.pnObject = new System.Windows.Forms.Panel();
            this.cbSlotType = new System.Windows.Forms.ComboBox();
            this.ckbDecimal = new System.Windows.Forms.CheckBox();
            this.tbVal1 = new System.Windows.Forms.TextBox();
            this.ckbNFailTrees = new System.Windows.Forms.CheckBox();
            this.ckbIgnDstFootprint = new System.Windows.Forms.CheckBox();
            this.ckbDiffAlts = new System.Windows.Forms.CheckBox();
            this.pnWiz0x002d.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbRoutingSlot.SuspendLayout();
            this.pnObject.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnWiz0x002d
            // 
            this.pnWiz0x002d.AutoSize = true;
            this.pnWiz0x002d.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnWiz0x002d.Controls.Add(this.flowLayoutPanel1);
            this.pnWiz0x002d.Location = new System.Drawing.Point(0, 0);
            this.pnWiz0x002d.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnWiz0x002d.Name = "pnWiz0x002d";
            this.pnWiz0x002d.Size = new System.Drawing.Size(223, 141);
            this.pnWiz0x002d.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.gbRoutingSlot);
            this.flowLayoutPanel1.Controls.Add(this.ckbNFailTrees);
            this.flowLayoutPanel1.Controls.Add(this.ckbIgnDstFootprint);
            this.flowLayoutPanel1.Controls.Add(this.ckbDiffAlts);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(221, 139);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // gbRoutingSlot
            // 
            this.gbRoutingSlot.AutoSize = true;
            this.gbRoutingSlot.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbRoutingSlot.Controls.Add(this.pnObject);
            this.gbRoutingSlot.Location = new System.Drawing.Point(2, 2);
            this.gbRoutingSlot.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbRoutingSlot.Name = "gbRoutingSlot";
            this.gbRoutingSlot.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbRoutingSlot.Size = new System.Drawing.Size(217, 72);
            this.gbRoutingSlot.TabIndex = 1;
            this.gbRoutingSlot.TabStop = false;
            this.gbRoutingSlot.Text = "Go to Routing Slot";
            // 
            // pnObject
            // 
            this.pnObject.AutoSize = true;
            this.pnObject.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnObject.Controls.Add(this.cbSlotType);
            this.pnObject.Controls.Add(this.ckbDecimal);
            this.pnObject.Controls.Add(this.tbVal1);
            this.pnObject.Location = new System.Drawing.Point(5, 17);
            this.pnObject.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnObject.Name = "pnObject";
            this.pnObject.Size = new System.Drawing.Size(208, 38);
            this.pnObject.TabIndex = 1;
            // 
            // cbSlotType
            // 
            this.cbSlotType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSlotType.FormattingEnabled = true;
            this.cbSlotType.Items.AddRange(new object[] {
            "Temp",
            "Param",
            "Literal",
            "Global",
            "Local"});
            this.cbSlotType.Location = new System.Drawing.Point(0, 0);
            this.cbSlotType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbSlotType.Name = "cbSlotType";
            this.cbSlotType.Size = new System.Drawing.Size(96, 21);
            this.cbSlotType.TabIndex = 1;
            // 
            // ckbDecimal
            // 
            this.ckbDecimal.AutoSize = true;
            this.ckbDecimal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbDecimal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbDecimal.Location = new System.Drawing.Point(142, 19);
            this.ckbDecimal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbDecimal.Name = "ckbDecimal";
            this.ckbDecimal.Size = new System.Drawing.Size(64, 17);
            this.ckbDecimal.TabIndex = 3;
            this.ckbDecimal.Text = "Decimal";
            // 
            // tbVal1
            // 
            this.tbVal1.Location = new System.Drawing.Point(96, 0);
            this.tbVal1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbVal1.Name = "tbVal1";
            this.tbVal1.Size = new System.Drawing.Size(108, 20);
            this.tbVal1.TabIndex = 2;
            // 
            // ckbNFailTrees
            // 
            this.ckbNFailTrees.AutoSize = true;
            this.ckbNFailTrees.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbNFailTrees.Location = new System.Drawing.Point(2, 78);
            this.ckbNFailTrees.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbNFailTrees.Name = "ckbNFailTrees";
            this.ckbNFailTrees.Size = new System.Drawing.Size(95, 17);
            this.ckbNFailTrees.TabIndex = 2;
            this.ckbNFailTrees.Text = "no failure trees";
            this.ckbNFailTrees.UseVisualStyleBackColor = true;
            // 
            // ckbIgnDstFootprint
            // 
            this.ckbIgnDstFootprint.AutoSize = true;
            this.ckbIgnDstFootprint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbIgnDstFootprint.Location = new System.Drawing.Point(2, 99);
            this.ckbIgnDstFootprint.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbIgnDstFootprint.Name = "ckbIgnDstFootprint";
            this.ckbIgnDstFootprint.Size = new System.Drawing.Size(136, 17);
            this.ckbIgnDstFootprint.TabIndex = 3;
            this.ckbIgnDstFootprint.Text = "ignore dest obj footprint";
            this.ckbIgnDstFootprint.UseVisualStyleBackColor = true;
            // 
            // ckbDiffAlts
            // 
            this.ckbDiffAlts.AutoSize = true;
            this.ckbDiffAlts.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbDiffAlts.Location = new System.Drawing.Point(2, 120);
            this.ckbDiffAlts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbDiffAlts.Name = "ckbDiffAlts";
            this.ckbDiffAlts.Size = new System.Drawing.Size(133, 17);
            this.ckbDiffAlts.TabIndex = 4;
            this.ckbDiffAlts.Text = "allow different altitudes";
            this.ckbDiffAlts.UseVisualStyleBackColor = true;
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(637, 413);
            this.Controls.Add(this.pnWiz0x002d);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UI";
            this.Text = "UI";
            this.pnWiz0x002d.ResumeLayout(false);
            this.pnWiz0x002d.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.gbRoutingSlot.ResumeLayout(false);
            this.gbRoutingSlot.PerformLayout();
            this.pnObject.ResumeLayout(false);
            this.pnObject.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion


    }

}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWiz0x002d : pjse.ABhavOperandWiz
	{
        public BhavOperandWiz0x002d(Instruction i) : base(i) { myForm = new Wiz0x002d.UI(); }

		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm = null;
		}
		#endregion

	}

}
