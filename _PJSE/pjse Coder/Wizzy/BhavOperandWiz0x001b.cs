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

namespace pjse.BhavOperandWizards.Wiz0x001b
{
    /// <summary>
    /// Summary description for StrBig.
    /// </summary>
    internal class UI : System.Windows.Forms.Form, iBhavOperandWizForm
	{
		#region Form variables

        internal System.Windows.Forms.Panel pnWiz0x001b;
        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox gbLocation;
        private ComboBox cbLocation;
        private GroupBox gbDirection;
        private ComboBox cbDirection;
        private CheckBox ckbNoFailureTrees;
        private CheckBox ckbDifferentAltitudes;
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

            cbLocation.Items.AddRange(BhavWiz.readStr(GS.BhavStr.RelativeLocations).ToArray());
            cbDirection.Items.AddRange(BhavWiz.readStr(GS.BhavStr.RelativeDirections).ToArray());
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
        //private bool internalchg = false;

        #region iBhavOperandWizForm
        public Panel WizPanel { get { return this.pnWiz0x001b; } }

        public void Execute(Instruction inst)
        {
            this.inst = inst;

            wrappedByteArray ops1 = inst.Operands;
            wrappedByteArray ops2 = inst.Reserved1;
            Boolset ops16 = ops1[6];

            //internalchg = true;

            cbLocation.SelectedIndex = ((byte)(ops1[2] + 2) < cbLocation.Items.Count) ? (byte)(ops1[2] + 2) : -1;
            cbDirection.SelectedIndex = ((byte)(ops1[3] + 2) < cbDirection.Items.Count) ? (byte)(ops1[3] + 2) : -1;

            ckbNoFailureTrees.Checked = ops16[1];
            ckbDifferentAltitudes.Checked = ops16[2];

            //internalchg = false;
        }

		public Instruction Write(Instruction inst)
		{
			if (inst != null)
			{
                wrappedByteArray ops1 = inst.Operands;
                wrappedByteArray ops2 = inst.Reserved1;
                Boolset ops16 = ops1[6];

                if (cbLocation.SelectedIndex >= 0) ops1[2] = ((byte)(cbLocation.SelectedIndex - 2));
                if (cbDirection.SelectedIndex >= 0) ops1[3] = ((byte)(cbDirection.SelectedIndex - 2));

                ops16[1] = ckbNoFailureTrees.Checked;
                ops16[2] = ckbDifferentAltitudes.Checked;
                ops1[6] = ops16;

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
            this.pnWiz0x001b = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbLocation = new System.Windows.Forms.GroupBox();
            this.cbLocation = new System.Windows.Forms.ComboBox();
            this.gbDirection = new System.Windows.Forms.GroupBox();
            this.cbDirection = new System.Windows.Forms.ComboBox();
            this.ckbNoFailureTrees = new System.Windows.Forms.CheckBox();
            this.ckbDifferentAltitudes = new System.Windows.Forms.CheckBox();
            this.pnWiz0x001b.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbLocation.SuspendLayout();
            this.gbDirection.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnWiz0x001b
            // 
            this.pnWiz0x001b.AutoSize = true;
            this.pnWiz0x001b.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnWiz0x001b.Controls.Add(this.flowLayoutPanel1);
            this.pnWiz0x001b.Location = new System.Drawing.Point(0, 0);
            this.pnWiz0x001b.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnWiz0x001b.Name = "pnWiz0x001b";
            this.pnWiz0x001b.Size = new System.Drawing.Size(168, 164);
            this.pnWiz0x001b.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.gbLocation);
            this.flowLayoutPanel1.Controls.Add(this.gbDirection);
            this.flowLayoutPanel1.Controls.Add(this.ckbNoFailureTrees);
            this.flowLayoutPanel1.Controls.Add(this.ckbDifferentAltitudes);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(164, 160);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // gbLocation
            // 
            this.gbLocation.AutoSize = true;
            this.gbLocation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbLocation.Controls.Add(this.cbLocation);
            this.gbLocation.Location = new System.Drawing.Point(2, 2);
            this.gbLocation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbLocation.Name = "gbLocation";
            this.gbLocation.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbLocation.Size = new System.Drawing.Size(160, 55);
            this.gbLocation.TabIndex = 1;
            this.gbLocation.TabStop = false;
            this.gbLocation.Text = "Location";
            // 
            // cbLocation
            // 
            this.cbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLocation.FormattingEnabled = true;
            this.cbLocation.Location = new System.Drawing.Point(5, 17);
            this.cbLocation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbLocation.Name = "cbLocation";
            this.cbLocation.Size = new System.Drawing.Size(151, 21);
            this.cbLocation.TabIndex = 1;
            // 
            // gbDirection
            // 
            this.gbDirection.AutoSize = true;
            this.gbDirection.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbDirection.Controls.Add(this.cbDirection);
            this.gbDirection.Location = new System.Drawing.Point(2, 61);
            this.gbDirection.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbDirection.Name = "gbDirection";
            this.gbDirection.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbDirection.Size = new System.Drawing.Size(160, 55);
            this.gbDirection.TabIndex = 2;
            this.gbDirection.TabStop = false;
            this.gbDirection.Text = "Direction";
            // 
            // cbDirection
            // 
            this.cbDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDirection.FormattingEnabled = true;
            this.cbDirection.Location = new System.Drawing.Point(5, 17);
            this.cbDirection.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbDirection.Name = "cbDirection";
            this.cbDirection.Size = new System.Drawing.Size(151, 21);
            this.cbDirection.TabIndex = 1;
            // 
            // ckbNoFailureTrees
            // 
            this.ckbNoFailureTrees.AutoSize = true;
            this.ckbNoFailureTrees.Location = new System.Drawing.Point(2, 120);
            this.ckbNoFailureTrees.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbNoFailureTrees.Name = "ckbNoFailureTrees";
            this.ckbNoFailureTrees.Size = new System.Drawing.Size(95, 17);
            this.ckbNoFailureTrees.TabIndex = 3;
            this.ckbNoFailureTrees.Text = "no failure trees";
            this.ckbNoFailureTrees.UseVisualStyleBackColor = true;
            // 
            // ckbDifferentAltitudes
            // 
            this.ckbDifferentAltitudes.AutoSize = true;
            this.ckbDifferentAltitudes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbDifferentAltitudes.Location = new System.Drawing.Point(2, 141);
            this.ckbDifferentAltitudes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbDifferentAltitudes.Name = "ckbDifferentAltitudes";
            this.ckbDifferentAltitudes.Size = new System.Drawing.Size(106, 17);
            this.ckbDifferentAltitudes.TabIndex = 4;
            this.ckbDifferentAltitudes.Text = "different altitudes";
            this.ckbDifferentAltitudes.UseVisualStyleBackColor = true;
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(637, 413);
            this.Controls.Add(this.pnWiz0x001b);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UI";
            this.Text = "UI";
            this.pnWiz0x001b.ResumeLayout(false);
            this.pnWiz0x001b.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.gbLocation.ResumeLayout(false);
            this.gbDirection.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion


    }

}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWiz0x001b : pjse.ABhavOperandWiz
	{
        public BhavOperandWiz0x001b(Instruction i) : base(i) { myForm = new Wiz0x001b.UI(); }

		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm = null;
		}
		#endregion

	}

}
