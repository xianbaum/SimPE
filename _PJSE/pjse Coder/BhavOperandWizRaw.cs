/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
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

namespace pjse.BhavOperandWizards.WizRaw
{
	/// <summary>
	/// Summary description for BhavInstruction.
	/// </summary>
    internal class UI : System.Windows.Forms.Form, iBhavOperandWizForm
	{
		#region Form variables
		internal System.Windows.Forms.Panel pnWizRaw;
		private System.Windows.Forms.TextBox tbRaw;
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
            if (SimPe.Helper.WindowsRegistry.UseBigIcons)
            {
                this.tbRaw.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
                this.tbRaw.Size = new System.Drawing.Size(320, 24);
                this.pnWizRaw.Size = new System.Drawing.Size(320, 27);
            }
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
        public Panel WizPanel { get { return this.pnWizRaw; } }

		public void Execute(Instruction inst)
		{
			string s = "";
			for (int i = 0; i < 8; i++)
				s += SimPe.Helper.HexString(inst.Operands[i]);
			for (int i = 0; i < 8; i++)
				s += SimPe.Helper.HexString(inst.Reserved1[i]);
			tbRaw.Text = s;
		}

        public Instruction Write(Instruction inst)
        {
            try
            {
                string s = tbRaw.Text + "00000000000000000000000000000000";
                for (int i = 0; i < 8; i++)
                    inst.Operands[i] = Convert.ToByte(s.Substring(i * 2, 2), 16);
                for (int i = 0; i < 8; i++)
                    inst.Reserved1[i] = Convert.ToByte(s.Substring((i + 8) * 2, 2), 16);

                return inst;
            }
            catch (Exception ex)
            {
                SimPe.Helper.ExceptionMessage(pjse.Localization.GetString("errconvert"), ex);
                return null;
            }
        }

        #endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.pnWizRaw = new System.Windows.Forms.Panel();
            this.tbRaw = new System.Windows.Forms.TextBox();
            this.pnWizRaw.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnWizRaw
            // 
            this.pnWizRaw.Controls.Add(this.tbRaw);
            this.pnWizRaw.Location = new System.Drawing.Point(9, 8);
            this.pnWizRaw.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnWizRaw.Name = "pnWizRaw";
            this.pnWizRaw.Size = new System.Drawing.Size(282, 23);
            this.pnWizRaw.TabIndex = 0;
            // 
            // tbRaw
            // 
            this.tbRaw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbRaw.Location = new System.Drawing.Point(0, 0);
            this.tbRaw.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbRaw.MaxLength = 32;
            this.tbRaw.Name = "tbRaw";
            this.tbRaw.Size = new System.Drawing.Size(282, 20);
            this.tbRaw.TabIndex = 0;
            this.tbRaw.Text = "textBox1";
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(299, 42);
            this.Controls.Add(this.pnWizRaw);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UI";
            this.Text = "UI";
            this.pnWizRaw.ResumeLayout(false);
            this.pnWizRaw.PerformLayout();
            this.ResumeLayout(false);
		}
		#endregion
	}
}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWizRaw : pjse.ABhavOperandWiz
	{
		public BhavOperandWizRaw(Instruction i) : base(i) { myForm = new WizRaw.UI(); }

		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm = null;
		}
		#endregion

	}
}

