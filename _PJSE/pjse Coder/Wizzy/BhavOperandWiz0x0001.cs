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
 *   59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.             *
 ***************************************************************************/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.PackedFiles.Wrapper;
using pjse.BhavNameWizards;

namespace pjse.BhavOperandWizards.Wiz0x0001
{
	/// <summary>
	/// Summary description for BhavInstruction.
	/// </summary>
	internal class UI : System.Windows.Forms.Form, iBhavOperandWizForm
	{
		#region Form variables

		internal System.Windows.Forms.Panel pnWiz0x0001;
		private System.Windows.Forms.ComboBox cbGenericSimsCall;
		private System.Windows.Forms.Label lbGenericSimsCallparms;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		private string genericSimsCallparamText(int i)
		{
            return BhavWiz.readStr(GS.BhavStr.GenericsDesc, (ushort)i);
		}


		public UI()
		{
			//
			// Required designer variable.
			//
            InitializeComponent();
            if (SimPe.Helper.WindowsRegistry.UseBigIcons) this.lbGenericSimsCallparms.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
        }


        #region iBhavOperandWizForm
        public Panel WizPanel { get { return this.pnWiz0x0001; } }

        public void Execute(Instruction inst)
		{
			byte operand0 = inst.Operands[0];

			this.cbGenericSimsCall.Items.Clear();
			for (byte i = 0; i < BhavWiz.readStr(GS.BhavStr.Generics).Count; i++)
				this.cbGenericSimsCall.Items.Add("0x" + SimPe.Helper.HexString(i) + ": " + BhavWiz.readStr(GS.BhavStr.Generics, i));
			this.lbGenericSimsCallparms.Text = "Should never see this";

			lbGenericSimsCallparms.Text = genericSimsCallparamText(operand0);
			cbGenericSimsCall.SelectedIndex = (operand0 < cbGenericSimsCall.Items.Count) ? operand0 : -1;
		}

		public Instruction Write(Instruction inst)
		{
			if (this.cbGenericSimsCall.SelectedIndex >= 0)
				inst.Operands[0] = (byte)this.cbGenericSimsCall.SelectedIndex;
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
            this.pnWiz0x0001 = new System.Windows.Forms.Panel();
            this.lbGenericSimsCallparms = new System.Windows.Forms.Label();
            this.cbGenericSimsCall = new System.Windows.Forms.ComboBox();
            this.pnWiz0x0001.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnWiz0x0001
            // 
            this.pnWiz0x0001.Controls.Add(this.lbGenericSimsCallparms);
            this.pnWiz0x0001.Controls.Add(this.cbGenericSimsCall);
            this.pnWiz0x0001.Location = new System.Drawing.Point(0, 0);
            this.pnWiz0x0001.Margin = new System.Windows.Forms.Padding(2);
            this.pnWiz0x0001.Name = "pnWiz0x0001";
            this.pnWiz0x0001.Size = new System.Drawing.Size(416, 260);
            this.pnWiz0x0001.TabIndex = 0;
            // 
            // lbGenericSimsCallparms
            // 
            this.lbGenericSimsCallparms.Location = new System.Drawing.Point(0, 21);
            this.lbGenericSimsCallparms.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbGenericSimsCallparms.Name = "lbGenericSimsCallparms";
            this.lbGenericSimsCallparms.Size = new System.Drawing.Size(416, 238);
            this.lbGenericSimsCallparms.TabIndex = 1;
            this.lbGenericSimsCallparms.Text = "label";
            // 
            // cbGenericSimsCall
            // 
            this.cbGenericSimsCall.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGenericSimsCall.DropDownWidth = 352;
            this.cbGenericSimsCall.Location = new System.Drawing.Point(0, 0);
            this.cbGenericSimsCall.Margin = new System.Windows.Forms.Padding(2);
            this.cbGenericSimsCall.Name = "cbGenericSimsCall";
            this.cbGenericSimsCall.Size = new System.Drawing.Size(416, 21);
            this.cbGenericSimsCall.TabIndex = 0;
            this.cbGenericSimsCall.SelectedIndexChanged += new System.EventHandler(this.cbGenericSimsCall_Changed);
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(546, 347);
            this.Controls.Add(this.pnWiz0x0001);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UI";
            this.Text = "UI";
            this.pnWiz0x0001.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void cbGenericSimsCall_Changed(object sender, System.EventArgs e)
		{
			lbGenericSimsCallparms.Text = (cbGenericSimsCall.SelectedIndex >= 0)
				? genericSimsCallparamText(cbGenericSimsCall.SelectedIndex)
				: "";
		}

    }

}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWiz0x0001 : pjse.ABhavOperandWiz
	{
        public BhavOperandWiz0x0001(Instruction i) : base(i) { myForm = new Wiz0x0001.UI(); }

		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm = null;
		}
		#endregion

	}

}
