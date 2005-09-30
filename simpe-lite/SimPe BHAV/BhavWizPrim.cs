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

namespace pjse.BhavOperandWizards.WizDefault
{
	#region internal form
	/// <summary>
	/// Summary description for BhavPrimWizDefault.
	/// </summary>
	internal class UI : System.Windows.Forms.Form
	{
		#region Form variables

		private System.Windows.Forms.TextBox tbInst_Op01_dec;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox tbInst_Unk7;
		private System.Windows.Forms.TextBox tbInst_Unk6;
		private System.Windows.Forms.TextBox tbInst_Unk5;
		private System.Windows.Forms.TextBox tbInst_Unk4;
		private System.Windows.Forms.TextBox tbInst_Unk3;
		private System.Windows.Forms.TextBox tbInst_Unk2;
		private System.Windows.Forms.TextBox tbInst_Unk1;
		private System.Windows.Forms.TextBox tbInst_Unk0;
		private System.Windows.Forms.TextBox tbInst_Op7;
		private System.Windows.Forms.TextBox tbInst_Op6;
		private System.Windows.Forms.TextBox tbInst_Op5;
		private System.Windows.Forms.TextBox tbInst_Op4;
		private System.Windows.Forms.TextBox tbInst_Op3;
		private System.Windows.Forms.TextBox tbInst_Op2;
		private System.Windows.Forms.TextBox tbInst_Op1;
		private System.Windows.Forms.TextBox tbInst_Op0;
		private System.Windows.Forms.TextBox tbInst_Op23_dec;
		private System.Windows.Forms.Label label2;
		internal System.Windows.Forms.Panel pnWizDefault;
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

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			TextBox[] iow = { tbInst_Op01_dec, tbInst_Op23_dec };
			alDec16 = new ArrayList(iow);
			TextBox[] iob = {
								tbInst_Op0  ,tbInst_Op1  ,tbInst_Op2  ,tbInst_Op3
								,tbInst_Op4  ,tbInst_Op5  ,tbInst_Op6  ,tbInst_Op7
								,tbInst_Unk0 ,tbInst_Unk1 ,tbInst_Unk2 ,tbInst_Unk3
								,tbInst_Unk4 ,tbInst_Unk5 ,tbInst_Unk6 ,tbInst_Unk7
							};
			alHex8 = new ArrayList(iob);
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


		#region UI
		private Instruction inst;
		private ArrayList alHex8;
		private ArrayList alDec16;

		internal void Execute(Instruction inst)
		{
			this.inst = inst;

			this.tbInst_Op01_dec.Text = (inst.Operands[0] + (inst.Operands[1] << 8)).ToString();
			this.tbInst_Op23_dec.Text = (inst.Operands[2] + (inst.Operands[3] << 8)).ToString();

			this.tbInst_Op0.Text = SimPe.Helper.HexString(inst.Operands[0]);
			this.tbInst_Op1.Text = SimPe.Helper.HexString(inst.Operands[1]);
			this.tbInst_Op2.Text = SimPe.Helper.HexString(inst.Operands[2]);
			this.tbInst_Op3.Text = SimPe.Helper.HexString(inst.Operands[3]);
			this.tbInst_Op4.Text = SimPe.Helper.HexString(inst.Operands[4]);
			this.tbInst_Op5.Text = SimPe.Helper.HexString(inst.Operands[5]);
			this.tbInst_Op6.Text = SimPe.Helper.HexString(inst.Operands[6]);
			this.tbInst_Op7.Text = SimPe.Helper.HexString(inst.Operands[7]);

			this.tbInst_Unk0.Text = SimPe.Helper.HexString(inst.Reserved1[0]);
			this.tbInst_Unk1.Text = SimPe.Helper.HexString(inst.Reserved1[1]);
			this.tbInst_Unk2.Text = SimPe.Helper.HexString(inst.Reserved1[2]);
			this.tbInst_Unk3.Text = SimPe.Helper.HexString(inst.Reserved1[3]);
			this.tbInst_Unk4.Text = SimPe.Helper.HexString(inst.Reserved1[4]);
			this.tbInst_Unk5.Text = SimPe.Helper.HexString(inst.Reserved1[5]);
			this.tbInst_Unk6.Text = SimPe.Helper.HexString(inst.Reserved1[6]);
			this.tbInst_Unk7.Text = SimPe.Helper.HexString(inst.Reserved1[7]);
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnWizDefault = new System.Windows.Forms.Panel();
			this.tbInst_Op01_dec = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.tbInst_Unk7 = new System.Windows.Forms.TextBox();
			this.tbInst_Unk6 = new System.Windows.Forms.TextBox();
			this.tbInst_Unk5 = new System.Windows.Forms.TextBox();
			this.tbInst_Unk4 = new System.Windows.Forms.TextBox();
			this.tbInst_Unk3 = new System.Windows.Forms.TextBox();
			this.tbInst_Unk2 = new System.Windows.Forms.TextBox();
			this.tbInst_Unk1 = new System.Windows.Forms.TextBox();
			this.tbInst_Unk0 = new System.Windows.Forms.TextBox();
			this.tbInst_Op7 = new System.Windows.Forms.TextBox();
			this.tbInst_Op6 = new System.Windows.Forms.TextBox();
			this.tbInst_Op5 = new System.Windows.Forms.TextBox();
			this.tbInst_Op4 = new System.Windows.Forms.TextBox();
			this.tbInst_Op3 = new System.Windows.Forms.TextBox();
			this.tbInst_Op2 = new System.Windows.Forms.TextBox();
			this.tbInst_Op1 = new System.Windows.Forms.TextBox();
			this.tbInst_Op0 = new System.Windows.Forms.TextBox();
			this.tbInst_Op23_dec = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.pnWizDefault.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnWizDefault
			// 
			this.pnWizDefault.Controls.Add(this.tbInst_Op01_dec);
			this.pnWizDefault.Controls.Add(this.label13);
			this.pnWizDefault.Controls.Add(this.tbInst_Unk7);
			this.pnWizDefault.Controls.Add(this.tbInst_Unk6);
			this.pnWizDefault.Controls.Add(this.tbInst_Unk5);
			this.pnWizDefault.Controls.Add(this.tbInst_Unk4);
			this.pnWizDefault.Controls.Add(this.tbInst_Unk3);
			this.pnWizDefault.Controls.Add(this.tbInst_Unk2);
			this.pnWizDefault.Controls.Add(this.tbInst_Unk1);
			this.pnWizDefault.Controls.Add(this.tbInst_Unk0);
			this.pnWizDefault.Controls.Add(this.tbInst_Op7);
			this.pnWizDefault.Controls.Add(this.tbInst_Op6);
			this.pnWizDefault.Controls.Add(this.tbInst_Op5);
			this.pnWizDefault.Controls.Add(this.tbInst_Op4);
			this.pnWizDefault.Controls.Add(this.tbInst_Op3);
			this.pnWizDefault.Controls.Add(this.tbInst_Op2);
			this.pnWizDefault.Controls.Add(this.tbInst_Op1);
			this.pnWizDefault.Controls.Add(this.tbInst_Op0);
			this.pnWizDefault.Controls.Add(this.tbInst_Op23_dec);
			this.pnWizDefault.Controls.Add(this.label2);
			this.pnWizDefault.Location = new System.Drawing.Point(0, 0);
			this.pnWizDefault.Name = "pnWizDefault";
			this.pnWizDefault.Size = new System.Drawing.Size(264, 72);
			this.pnWizDefault.TabIndex = 0;
			// 
			// tbInst_Op01_dec
			// 
			this.tbInst_Op01_dec.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbInst_Op01_dec.Location = new System.Drawing.Point(72, 0);
			this.tbInst_Op01_dec.MaxLength = 5;
			this.tbInst_Op01_dec.Name = "tbInst_Op01_dec";
			this.tbInst_Op01_dec.Size = new System.Drawing.Size(48, 21);
			this.tbInst_Op01_dec.TabIndex = 26;
			this.tbInst_Op01_dec.Text = "88888";
			this.tbInst_Op01_dec.Validating += new System.ComponentModel.CancelEventHandler(this.dec16_Validating);
			this.tbInst_Op01_dec.Validated += new System.EventHandler(this.dec16_Validated);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label13.Location = new System.Drawing.Point(0, 0);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(64, 17);
			this.label13.TabIndex = 25;
			this.label13.Text = "&Operands:";
			// 
			// tbInst_Unk7
			// 
			this.tbInst_Unk7.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbInst_Unk7.Location = new System.Drawing.Point(240, 48);
			this.tbInst_Unk7.MaxLength = 2;
			this.tbInst_Unk7.Name = "tbInst_Unk7";
			this.tbInst_Unk7.Size = new System.Drawing.Size(24, 21);
			this.tbInst_Unk7.TabIndex = 43;
			this.tbInst_Unk7.Text = "0";
			this.tbInst_Unk7.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Unk7.Validated += new System.EventHandler(this.hex8_Validated);
			// 
			// tbInst_Unk6
			// 
			this.tbInst_Unk6.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbInst_Unk6.Location = new System.Drawing.Point(216, 48);
			this.tbInst_Unk6.MaxLength = 2;
			this.tbInst_Unk6.Name = "tbInst_Unk6";
			this.tbInst_Unk6.Size = new System.Drawing.Size(24, 21);
			this.tbInst_Unk6.TabIndex = 42;
			this.tbInst_Unk6.Text = "0";
			this.tbInst_Unk6.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Unk6.Validated += new System.EventHandler(this.hex8_Validated);
			// 
			// tbInst_Unk5
			// 
			this.tbInst_Unk5.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbInst_Unk5.Location = new System.Drawing.Point(192, 48);
			this.tbInst_Unk5.MaxLength = 2;
			this.tbInst_Unk5.Name = "tbInst_Unk5";
			this.tbInst_Unk5.Size = new System.Drawing.Size(24, 21);
			this.tbInst_Unk5.TabIndex = 41;
			this.tbInst_Unk5.Text = "0";
			this.tbInst_Unk5.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Unk5.Validated += new System.EventHandler(this.hex8_Validated);
			// 
			// tbInst_Unk4
			// 
			this.tbInst_Unk4.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbInst_Unk4.Location = new System.Drawing.Point(168, 48);
			this.tbInst_Unk4.MaxLength = 2;
			this.tbInst_Unk4.Name = "tbInst_Unk4";
			this.tbInst_Unk4.Size = new System.Drawing.Size(24, 21);
			this.tbInst_Unk4.TabIndex = 40;
			this.tbInst_Unk4.Text = "0";
			this.tbInst_Unk4.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Unk4.Validated += new System.EventHandler(this.hex8_Validated);
			// 
			// tbInst_Unk3
			// 
			this.tbInst_Unk3.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbInst_Unk3.Location = new System.Drawing.Point(144, 48);
			this.tbInst_Unk3.MaxLength = 2;
			this.tbInst_Unk3.Name = "tbInst_Unk3";
			this.tbInst_Unk3.Size = new System.Drawing.Size(24, 21);
			this.tbInst_Unk3.TabIndex = 39;
			this.tbInst_Unk3.Text = "0";
			this.tbInst_Unk3.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Unk3.Validated += new System.EventHandler(this.hex8_Validated);
			// 
			// tbInst_Unk2
			// 
			this.tbInst_Unk2.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbInst_Unk2.Location = new System.Drawing.Point(120, 48);
			this.tbInst_Unk2.MaxLength = 2;
			this.tbInst_Unk2.Name = "tbInst_Unk2";
			this.tbInst_Unk2.Size = new System.Drawing.Size(24, 21);
			this.tbInst_Unk2.TabIndex = 38;
			this.tbInst_Unk2.Text = "0";
			this.tbInst_Unk2.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Unk2.Validated += new System.EventHandler(this.hex8_Validated);
			// 
			// tbInst_Unk1
			// 
			this.tbInst_Unk1.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbInst_Unk1.Location = new System.Drawing.Point(96, 48);
			this.tbInst_Unk1.MaxLength = 2;
			this.tbInst_Unk1.Name = "tbInst_Unk1";
			this.tbInst_Unk1.Size = new System.Drawing.Size(24, 21);
			this.tbInst_Unk1.TabIndex = 37;
			this.tbInst_Unk1.Text = "0";
			this.tbInst_Unk1.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Unk1.Validated += new System.EventHandler(this.hex8_Validated);
			// 
			// tbInst_Unk0
			// 
			this.tbInst_Unk0.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbInst_Unk0.Location = new System.Drawing.Point(72, 48);
			this.tbInst_Unk0.MaxLength = 2;
			this.tbInst_Unk0.Name = "tbInst_Unk0";
			this.tbInst_Unk0.Size = new System.Drawing.Size(24, 21);
			this.tbInst_Unk0.TabIndex = 36;
			this.tbInst_Unk0.Text = "0";
			this.tbInst_Unk0.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Unk0.Validated += new System.EventHandler(this.hex8_Validated);
			// 
			// tbInst_Op7
			// 
			this.tbInst_Op7.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbInst_Op7.Location = new System.Drawing.Point(240, 24);
			this.tbInst_Op7.MaxLength = 2;
			this.tbInst_Op7.Name = "tbInst_Op7";
			this.tbInst_Op7.Size = new System.Drawing.Size(24, 21);
			this.tbInst_Op7.TabIndex = 35;
			this.tbInst_Op7.Text = "0";
			this.tbInst_Op7.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Op7.Validated += new System.EventHandler(this.hex8_Validated);
			// 
			// tbInst_Op6
			// 
			this.tbInst_Op6.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbInst_Op6.Location = new System.Drawing.Point(216, 24);
			this.tbInst_Op6.MaxLength = 2;
			this.tbInst_Op6.Name = "tbInst_Op6";
			this.tbInst_Op6.Size = new System.Drawing.Size(24, 21);
			this.tbInst_Op6.TabIndex = 34;
			this.tbInst_Op6.Text = "0";
			this.tbInst_Op6.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Op6.Validated += new System.EventHandler(this.hex8_Validated);
			// 
			// tbInst_Op5
			// 
			this.tbInst_Op5.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbInst_Op5.Location = new System.Drawing.Point(192, 24);
			this.tbInst_Op5.MaxLength = 2;
			this.tbInst_Op5.Name = "tbInst_Op5";
			this.tbInst_Op5.Size = new System.Drawing.Size(24, 21);
			this.tbInst_Op5.TabIndex = 33;
			this.tbInst_Op5.Text = "0";
			this.tbInst_Op5.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Op5.Validated += new System.EventHandler(this.hex8_Validated);
			// 
			// tbInst_Op4
			// 
			this.tbInst_Op4.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbInst_Op4.Location = new System.Drawing.Point(168, 24);
			this.tbInst_Op4.MaxLength = 2;
			this.tbInst_Op4.Name = "tbInst_Op4";
			this.tbInst_Op4.Size = new System.Drawing.Size(24, 21);
			this.tbInst_Op4.TabIndex = 32;
			this.tbInst_Op4.Text = "0";
			this.tbInst_Op4.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Op4.Validated += new System.EventHandler(this.hex8_Validated);
			// 
			// tbInst_Op3
			// 
			this.tbInst_Op3.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbInst_Op3.Location = new System.Drawing.Point(144, 24);
			this.tbInst_Op3.MaxLength = 2;
			this.tbInst_Op3.Name = "tbInst_Op3";
			this.tbInst_Op3.Size = new System.Drawing.Size(24, 21);
			this.tbInst_Op3.TabIndex = 31;
			this.tbInst_Op3.Text = "0";
			this.tbInst_Op3.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Op3.Validated += new System.EventHandler(this.hex8_Validated);
			// 
			// tbInst_Op2
			// 
			this.tbInst_Op2.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbInst_Op2.Location = new System.Drawing.Point(120, 24);
			this.tbInst_Op2.MaxLength = 2;
			this.tbInst_Op2.Name = "tbInst_Op2";
			this.tbInst_Op2.Size = new System.Drawing.Size(24, 21);
			this.tbInst_Op2.TabIndex = 30;
			this.tbInst_Op2.Text = "0";
			this.tbInst_Op2.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Op2.Validated += new System.EventHandler(this.hex8_Validated);
			// 
			// tbInst_Op1
			// 
			this.tbInst_Op1.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbInst_Op1.Location = new System.Drawing.Point(96, 24);
			this.tbInst_Op1.MaxLength = 2;
			this.tbInst_Op1.Name = "tbInst_Op1";
			this.tbInst_Op1.Size = new System.Drawing.Size(24, 21);
			this.tbInst_Op1.TabIndex = 29;
			this.tbInst_Op1.Text = "0";
			this.tbInst_Op1.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Op1.Validated += new System.EventHandler(this.hex8_Validated);
			// 
			// tbInst_Op0
			// 
			this.tbInst_Op0.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbInst_Op0.Location = new System.Drawing.Point(72, 24);
			this.tbInst_Op0.MaxLength = 2;
			this.tbInst_Op0.Name = "tbInst_Op0";
			this.tbInst_Op0.Size = new System.Drawing.Size(24, 21);
			this.tbInst_Op0.TabIndex = 28;
			this.tbInst_Op0.Text = "DD";
			this.tbInst_Op0.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
			this.tbInst_Op0.Validated += new System.EventHandler(this.hex8_Validated);
			// 
			// tbInst_Op23_dec
			// 
			this.tbInst_Op23_dec.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.tbInst_Op23_dec.Location = new System.Drawing.Point(120, 0);
			this.tbInst_Op23_dec.MaxLength = 5;
			this.tbInst_Op23_dec.Name = "tbInst_Op23_dec";
			this.tbInst_Op23_dec.Size = new System.Drawing.Size(48, 21);
			this.tbInst_Op23_dec.TabIndex = 27;
			this.tbInst_Op23_dec.Text = "0";
			this.tbInst_Op23_dec.Validating += new System.ComponentModel.CancelEventHandler(this.dec16_Validating);
			this.tbInst_Op23_dec.Validated += new System.EventHandler(this.dec16_Validated);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label2.Location = new System.Drawing.Point(168, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 17);
			this.label2.TabIndex = 24;
			this.label2.Text = "(decimal)";
			// 
			// UI
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.pnWizDefault);
			this.Name = "UI";
			this.Text = "BhavPrimWizDefaultUI";
			this.pnWizDefault.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void hex8_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try { Convert.ToByte(((TextBox)sender).Text, 16); }
			catch (Exception) { e.Cancel = true; }
		}

		private void hex8_Validated(object sender, System.EventArgs e)
		{
			byte val = Convert.ToByte(((TextBox)sender).Text, 16);

			int i = alHex8.IndexOf(sender);

			if (i < 8)
			{
				if (inst.Operands[i] != val)
				{
					inst.Operands[i] = val;
				}
				this.tbInst_Op01_dec.Text = (inst.Operands[0] + (inst.Operands[1] << 8)).ToString();
				this.tbInst_Op23_dec.Text = (inst.Operands[2] + (inst.Operands[3] << 8)).ToString();
			}
			else
			{
				if (i < 16)
				{
					if (inst.Reserved1[i-8] != val)
					{
						inst.Reserved1[i-8] = val;
					}
				}
				else 
					throw new Exception("hex8_Validated not applicable to control " + sender.ToString());
			}
		}


		private void dec16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try { Convert.ToUInt16(((TextBox)sender).Text); }
			catch (Exception) { e.Cancel = true; }
		}

		private void dec16_Validated(object sender, System.EventArgs e)
		{
			ushort val = Convert.ToUInt16(((TextBox)sender).Text);

			int i = alDec16.IndexOf(sender) * 2;

			if (i > 2)
				throw new Exception("dec16_Validated not applicable to control " + sender.ToString());

			byte v0 = inst.Operands[i];
			byte v1 = inst.Operands[i+1];
			ushort cv = (ushort)(v0 + (v1 * 256));
			if (cv != val)
			{
				inst.Operands[i] = (byte)(val & 0xFF);
				((TextBox)this.alHex8[i]).Text = SimPe.Helper.HexString(inst.Operands[i]);
				inst.Operands[i+1] = (byte)((val >> 8) & 0xFF);
				((TextBox)this.alHex8[i+1]).Text = SimPe.Helper.HexString(inst.Operands[i+1]);
			}
		}

	}
	#endregion
}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWizDefault : pjse.ABhavOperandWiz
	{
		public BhavOperandWizDefault() : base() { }

		public BhavOperandWizDefault(Instruction i) : base(i) { }


		#region pjse.ABhavOperandWiz
		private WizDefault.UI myForm = null;
		public override Panel bhavPrimWizPanel
		{
			get
			{
				if (myForm == null) myForm = new WizDefault.UI();
				return myForm.pnWizDefault;
			}
		}

		public override void Execute()
		{
			if (myForm == null) myForm = new WizDefault.UI();
			myForm.Execute(instruction);
		}

		public override Instruction Write()
		{
			return instruction;
		}


		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm = null;
		}
		#endregion

		#endregion
	}

}

namespace pjse.BhavNameWizards
{
	/// <summary>
	/// Abstract class for primitive name providers
	/// </summary>
	public abstract class BhavWizPrim : BhavWiz
	{
		public BhavWizPrim(Instruction instruction) : base (instruction) { }
		public static implicit operator BhavWizPrim(Instruction i)
		{
			if (i.OpCode >= 0x0100)
				throw new Exception("OpCode not a primative");

			switch(i.OpCode)
			{
				case 0x0000: return new WizPrim0x0000(i);
				case 0x0001: return new WizPrim0x0001(i);
				case 0x0002: return new WizPrim0x0002(i);
				case 0x0008: return new WizPrim0x0008(i);
				case 0x000d: return new WizPrim0x000d(i);
				case 0x0010: return new WizPrim0x0010(i);
				case 0x001c: return new WizPrim0x001c(i);
				case 0x001f: return new WizPrim0x001f(i);
				case 0x0024: return new WizPrim0x0024(i);
				case 0x002a: return new WizPrim0x002a(i);
				case 0x0032: return new WizPrim0x0032(i);
			}
			return new WizPrimDefault(i);
		}

		// Also provide
		// public static implicit operator <Wiz>(byte[] operands);


		protected override string Prefix { get { return "prim"; } }

		protected override string OpcodeName { get { return GS.GStr(GS.SF.Primitives, instruction.OpCode); } }


		public override string ShortName { get { return base.ShortName + " (" + Operands(false) + ")"; } }

		public override string LongName { get { return base.ShortName + " (" + Operands(true) + ")"; } }

		protected virtual string Operands(bool lng) { return ""; }
	}


	public class WizPrimDefault : BhavWizPrim
	{
		public WizPrimDefault(Instruction i) : base(i) { }

		public override string ShortName { get { return base.ShortName; } }

		public override string LongName { get { return base.LongName; } }

	}

	public class WizPrim0x0000 : BhavWizPrim	// Sleep
	{
		public WizPrim0x0000(Instruction i) : base(i) { }

		protected override string Operands(bool lng) { return dataOwner(9, ToShort(instruction.Operands[0], instruction.Operands[1])); }

	}

	public class WizPrim0x0008 : BhavWizPrim	// Random
	{
		public WizPrim0x0008(Instruction i) : base(i) { }

		protected override string Operands(bool lng)
		{
			if (lng)
				return dataOwner(instruction.Operands[2], ToShort(instruction.Operands[0], instruction.Operands[1]))
					+ " := random from 0 to < "
					+ dataOwner(instruction.Operands[6], ToShort(instruction.Operands[4], instruction.Operands[5]));
			else
				return GS.GStr(GS.SF.DataOwners, instruction.Operands[2])
					+ " 0x" + SimPe.Helper.HexString(ToShort(instruction.Operands[0], instruction.Operands[1]))
					+ ", " + GS.GStr(GS.SF.DataOwners, instruction.Operands[6])
					+ " 0x" + SimPe.Helper.HexString(ToShort(instruction.Operands[4], instruction.Operands[5]))
					;
		}
	}

	public class WizPrim0x000d : BhavWizPrim	// Push Interaction
	{
		public WizPrim0x000d(Instruction i) : base(i) { }

		protected override string Operands(bool lng)
		{
			byte[] o = new byte[16];
			((byte[])instruction.Operands).CopyTo(o, 0);
			((byte[])instruction.Reserved1).CopyTo(o, 8);

			string s = "";

			if ((o[3] & 0x10) != 0)
				s += (lng ? "getting interaction # from " : "# [") + dataOwner(o[5], ToShort(o[6], o[7])) + (lng ? "" : "]");
			else if ((o[14] & 2) != 0)
				s += (lng ? "getting interaction # from results of " : "# [") + "last FBA call" + (lng ? "" : "]");
			else
				s += "# 0x" + SimPe.Helper.HexString(o[0]);

			s +=  " of ";
			if ((o[3] & 2) != 0)
				s += dataOwner(0x19, o[1]);	// local
			else
				s += dataOwner(0x09, o[1]);	// param

			s += (lng ? " onto the stack object's queue, " : " onto queue, ") + GS.GStr(GS.SF.Priorities, o[2]);

			if (lng)
			{
				if ((o[3] & 1) != 0)
					s += ", use icon from " + dataOwner(0x19, o[4]);
				else if ((o[14] & 4) != 0)
					s += ", use icon from selector GUID in Temp 4,5";

				if ((o[14] & 8) != 0)
					s += ", Getting Icon Index from Temp 6";
				else
					s += ", Icon Index is 0x" + SimPe.Helper.HexString(o[15]);

				if (0 != (o[14] & 0x01)) s += ", passing on Caller's params 0 to 3";
				// if (o[3] & 4) ht_fprintf(outFile,TYPE_NORMAL,", continue as current");
				if (0 != (o[3] & 0x08)) s += ", use name";
				if (0 != (o[3] & 0x20)) s += ", force run Check Tree";
				if (0 != (o[3] & 0x40)) s += ", linking to interaction with ID in " + dataOwner(o[8], ToShort(o[9], o[10]));
				if (0 != (o[3] & 0x80)) s += ", returing ID in " + dataOwner(o[11], ToShort(o[12], o[13]));
			}

			return s;
		}
	}

	public class WizPrim0x0010 : BhavWizPrim	// Find location for
	{
		public WizPrim0x0010(Instruction i) : base(i) { }

		protected override string Operands(bool lng)
		{
			//return "Find location for";

			byte[] o = new byte[16];
			((byte[])instruction.Operands).CopyTo(o, 0);
			((byte[])instruction.Reserved1).CopyTo(o, 8);

			string s = "";

			if ((o[2] & 0x01) == 0)
			{
				s += "Stack Object";
				if (lng)
					s += ", relative to " + dataOwner(0x19, o[1]);
			}
			else
			{
				s += dataOwner(o[4], ToShort(o[5], o[6]));
				if (lng)
					s += ", relative to " + dataOwner(o[7], ToShort(o[8], o[9]));
			}

			if (lng)
			{
				if ((o[2] & 0x08) != 0)
				{
					s += ", facing ";
					if ((o[3] & 0x01) != 0) s += "N";
					if ((o[3] & 0x02) != 0) s += "NE";
					if ((o[3] & 0x04) != 0) s += "E";
					if ((o[3] & 0x08) != 0) s += "SE";
					if ((o[3] & 0x10) != 0) s += "S";
					if ((o[3] & 0x20) != 0) s += "SW";
					if ((o[3] & 0x40) != 0) s += "W";
					if ((o[3] & 0x80) != 0) s += "NW";
				}

				s += ", " + GS.GStr(GS.SF.FindGLB, o[0]);
				if (o[0] >= 5 && o[0] <= 8)
					s += " 0x" + SimPe.Helper.HexString(o[10]);

				if ((o[2] & 0x02) == 0) s += ", prefer empty";
				if ((o[2] & 0x04) != 0) s += ", user editable";
				if ((o[2] & 0x10) != 0) s += ", on level ground";
				if ((o[2] & 0x20) != 0) s += ", with empty border";
				if ((o[2] & 0x40) != 0) s += ", begin in front of refobj";
				if ((o[2] & 0x80) != 0) s += ", with line of site to center";
			}

			return s;
		}

	}

	public class WizPrim0x001c : BhavWizPrim	// Run Tree By Name
	{
		public WizPrim0x001c(Instruction i) : base(i) { }

		protected override string Operands(bool lng)
		{
			byte[] o = new byte[16];
			((byte[])instruction.Operands).CopyTo(o, 0);
			((byte[])instruction.Reserved1).CopyTo(o, 8);

			string s = "";

			Scope scope;
			if ((o[2] & 1) != 0) scope = Scope.Global;
			else if ((o[2] & 2) != 0) scope = Scope.SemiGlobal;
			else scope = Scope.Private; 

			s += scope.ToString() + ": " + readStr(scope, (ulong)GS.SF.gNamedTreePrim, o[4] - 1);

			if (lng)
			{
				if ((o[2] & 0x20) != 0)
					s += ", Caller's params";

				if ((o[2] & 0x10) == 0)
				{
					// DisaSim2 doesn't decode these
					//for (int i = 0; i < 4; i++, thisArgc--)
					//	s += (i>0 ? ", " : "") + "0x" + SimPe.Helper.HexString(ToShort(o[(i*2)], o[(i*2) + 1]));
				}
				else	// 16 byte format
				{
					for (int i = 0; i < 3; i++)
						s += ", " +
							(lng
							? dataOwner(o[6 + i*3], ToShort(o[6 + (i*3) + 1], o[6 + (i*3) + 2]))
							: GS.GStr(GS.SF.DataOwners, o[6 + i*3]) + " 0x" + SimPe.Helper.HexString(ToShort(o[6 + (i*3) + 1], o[6 + (i*3) + 2]))
							);
				}

				if ((o[2] & 0x20) == 0)
					s += ", Caller's params";

				switch (o[5]) 
				{
					case 0: s += ", run in my stack"; break;
					case 1: s += ", run in Stack Object's stack"; break;
					case 2: s += ", push onto my stack"; break;
				}
			}

			return s;
		}
	}

	public class WizPrim0x001f : BhavWizPrim	// Set to Next
	{
		public WizPrim0x001f(Instruction i) : base(i) { }

		protected override string Operands(bool lng)
		{
			byte[] o = new byte[16];
			((byte[])instruction.Operands).CopyTo(o, 0);
			((byte[])instruction.Reserved1).CopyTo(o, 8);

			string s = "";

			s += GS.GStr(0xa4, (ushort)(o[4] & 0x7f));
			switch(o[4] & 0x7f)
			{
				case 0x04: case 0x07:
					uint d1 = (uint)(o[0] | (o[1] << 8) | (o[2] << 16) | (o[3] << 24));
					s += " GUID 0x" + SimPe.Helper.HexString(d1);
					//readGUID(d1);
					break;
				case 0x09: case 0x22:
					s = s.Replace("[local]", (lng ? dataOwner(0x19, o[6]) // local
						: GS.GStr(GS.SF.DataOwners, 0x19) + " " + o[6].ToString()));
					//w1 = ToShort(o[9], o[10]);
					//w2 = ToShort(o[11], o[12]);
					//if (b[x+8] & 2) ht_fprintf(outFile,TYPE_NORMAL," where %s := %d", gString8D[w1], w2);
					break;
			}

			if ((o[4] & 0x80) == 0)
				s += lng ? ", result in " + dataOwner(0x0a, 0x0000) : ""; // Stack Object
			else
				s += ", result in " + (lng ? dataOwner(o[5], o[7])
					: GS.GStr(GS.SF.DataOwners, o[5]) + " 0x" + SimPe.Helper.HexString(o[7]));
			//if ((o[5] != 0xA || o[7] != 0) && (o[4] & 0x80) != 0)
			//	s += dataOwner(o[5], o[7]) + " := next ";

			if ((o[8] & 0x01) != 0) s += ", [including disabled objects]";
			return s;
		}
	}

	public class WizPrim0x0024 : BhavWizPrim	// Dialog
	{
		public WizPrim0x0024(Instruction i) : base(i) { }

		protected override string Operands(bool lng)
		{
			byte[] o = new byte[16];
			((byte[])instruction.Operands).CopyTo(o, 0);
			((byte[])instruction.Reserved1).CopyTo(o, 8);

			bool nowait   = (o[7] & 0x01) != 0;
			byte iconType = (byte)((o[7] >> 1) & 0x07);
			byte tempVar  = (byte)((o[7] >> 4) & 0x07);
			bool noblock  = (o[7] & 0x80) != 0;

			ushort msg, cnc;
			if (instruction.NodeVersion == 0)
			{
				msg = o[2];	// message
				cnc = o[0];	// cancel
			} 
			else 
			{
				msg = ToShort(o[13], o[14]);	// message
				cnc = ToShort(o[0], o[2]);	// cancel
			}

			string s = "";
			if (lng)
				s += "Type: " + GS.GStr(GS.SF.Dialog, o[5]);

			Scope scope;
			if ((o[8] & 0x01) != 0)
				scope = Scope.SemiGlobal;
			else if ((o[8] & 0x40) != 0) 
				scope = Scope.Global;
			else
				scope = Scope.Private;

			switch (o[5])
			{
				case 0x09:
					if (lng)
						s += ", getting Text ID in " + dataOwner(0x08, tempVar);
					break;
				case 0x08: case 0x0a:
					s += lng ? ", " + dialogStr(scope, (o[8] & 0x02) != 0, msg)
						: readStr(scope, (ulong)GS.SF.gDialogPrim, msg - 1, 60);
					if (lng)
					{
						s += ", priority 0x" + SimPe.Helper.HexString((byte)(o[9] + 1));
						s += ", timeout 0x" + SimPe.Helper.HexString(o[10]);
						s += ", " + ((o[5] != 0x08) ? "getting" : "putting") + " Text ID in " + dataOwner(0x08, tempVar);
					}
					break;
				case 0x0e:
					s += (lng ? ", passing through data starting with " : "") + dataOwner(0x19, o[11]); // local
					break;
				case 0x0b: case 0x0c: case 0x0d: case 0x10: case 0x11: case 0x12: case 0x14:
					// what are these, then?
					break;
				default:
					s += lng ? ", " + dialogStr(scope, (o[8] & 0x02) != 0, msg)
						: readStr(scope, (ulong)GS.SF.gDialogPrim, msg - 1, 60);
					if (lng)
					{
						s += ", Yes: "    + dialogStr(scope, (o[8] & 0x04) != 0, o[3]);
						s += ", No: "     + dialogStr(scope, (o[8] & 0x08) != 0, o[4]);
						s += ", Title: "  + dialogStr(scope, (o[8] & 0x10) != 0, o[6]);
						s += ", Cancel: " + dialogStr(scope, (o[8] & 0x20) != 0, cnc);
					}
					break;
			}

			if (lng)
			{
				/*if (msg != 0x16 && msg != 0x19 && iconType != 1) { }*/
				s += ", icon: " + GS.GStr(GS.SF.DialogIcon, iconType);
				switch (iconType) 
				{
					case 3: s += ": BMP = 0x" + SimPe.Helper.HexString((ushort)(o[1] + 5000)); break;
					case 4: s += " " + dialogStr(scope, false, o[1]); break;
				}

				/*if (nowait || noblock) */
				s += ", " + (nowait ? "don't " : "") + "wait for user";/* + " and " +*/
				s += ", " + (noblock ? "don't " : "") + "block simulation";

				if (o[5] == 0x02)
					s += ", result in "+ dataOwner(0x08, tempVar);

				s += ", Style: ";
				switch (o[12]) 
				{
					case 0: s += "Sim"; break;
					case 1: s += "System"; break;
					case 2: s += "System Dialog"; break;
					case 3: s += "Birthday"; break;
					default: s += "unknown"; break;
				}

				s += "; (";
				if ((o[8] & 0x01) != 0)
					s += "SemiGlobal";
				else if ((o[8] & 0x40) != 0) 
					s += "Global";
				else
					s += "Private";
				s += " STR# 0x012D strings)";

				s += " (" + GS.GStr(GS.SF.DialogDesc, o[5]) + ")";
			}

			return s;
		}


		private string dialogStr(Scope scope, bool temp, ushort instance, int len)
		{
			string s = "[";
			if (temp)
				s += dataOwner(0x08, instance); // temp
			else
			{
				if (instance != 0)
					s += "0x" + SimPe.Helper.HexString(instance) +
						": " + readStr(scope, (ulong)GS.SF.gDialogPrim, instance - 1, len);
				else
					s += "none";
			}
			return s + "]";
		}

		private string dialogStr(Scope scope, bool temp, ushort instance) { return dialogStr(scope, temp, instance, -1); }

	}

	public class WizPrim0x002a : BhavWizPrim	// Create new object instance
	{
		public WizPrim0x002a(Instruction i) : base(i) { }

		protected override string Operands(bool lng)
		{
			byte[] o = new byte[16];
			((byte[])instruction.Operands).CopyTo(o, 0);
			((byte[])instruction.Reserved1).CopyTo(o, 8);

			string s = "";

			if (     (o[5] & 0x04) != 0) s += (lng ? "of " : "") + "neighbor in stack obj";
			else if ((o[5] & 0x40) != 0) s += (lng ? "from " : "") + "GUID in temp Inventory Token";
			else if ((o[5] & 0x80) != 0) s += (lng ? "from " : "") + "GUID in Temp 0,1";
			else                         s += (lng ? "of " : "") + "GUID 0x" + SimPe.Helper.HexString((uint)(o[0] | (o[1]<<8) | (o[2]<<16) | (o[3]<<24)));
				//readGUID(d1);

			if (lng)
			{
				s += ", place " + GS.GStr(GS.SF.ObjectPlace, o[4]);
				switch (o[4]) 
				{
					case 0x04: case 0x0A: s += " 0x" + SimPe.Helper.HexString(o[9]); break;
					case 0x08: case 0x09: s += " 0x" + SimPe.Helper.HexString(o[6]); break;
				}

				if ((o[5] & 0x02) != 0) s += ", pass object ids to main";
				if ((o[5] & 0x10) != 0) s += ", pass Temp 0 to main";
				if ((o[5] & 0x01) != 0) s += ", do not duplicate";
				if ((o[5] & 0x08) != 0) s += ", fail if tile is non-empty";

				if ((o[10] & 0x01) != 0) s += ", moving in a new Sim";
				if ((o[10] & 0x02) != 0) s += ", copying design mode materials from object in Temp 5";
			}

			return s;
		}

	}

	public class WizPrim0x0032 : BhavWizPrim	// Add/Change action string
	{
		public WizPrim0x0032(Instruction i) : base(i) { }

		protected override string Operands(bool lng)
		{
			byte[] o = new byte[16];
			((byte[])instruction.Operands).CopyTo(o, 0);
			((byte[])instruction.Reserved1).CopyTo(o, 8);

			string s = "";

			Scope scope = Scope.Private;
			if (     (o[2] & 0x04) != 0) scope = Scope.Global;
			else if ((o[2] & 0x08) != 0) scope = Scope.SemiGlobal;

			if (o[9] == 0) 
			{
				s += (lng ? "Add / Change Interaction string Mode, " : "");
				if (lng && (o[3] & 1) != 0 && instruction.NodeVersion != 0)
					s += "Disabled, ";

				if (lng)
					s += scope.ToString() + ": ";

				if ((o[2] & 0x10) != 0) s += "STR# 0x012e:[Temp 0]";
				else s += readStr(scope, 0x012e, o[4] - 1);
			}
			else 
			{
				s += "Interaction Icon Change Mode";
				if (lng)
				{
					if ((o[2] & 0x20) != 0)
						s += ", Thumbnail Outfit GUID " + 
							(((o[2] & 0x40) != 0)
							? "from Temp 2,3"
							: "0x" + SimPe.Helper.HexString(o[5] | (o[6]<<8) | (o[7]<<16) | (o[8]<<24)));
						//readGUID(d1);
					else 
						s += ", Using object ID in " + dataOwner(o[11], ToShort(o[12], o[13]));

					s += ", model table icon index " + (((o[2] & 0x80) != 0) ? "Temp 1" : "0x" + SimPe.Helper.HexString(o[10]));
				}
			}

			return s;
		}

	}

}
#if DISASIM
                case 0x03:  // Find Best Interaction
                    w1 = *(UINT16 *) (&b[x]);   // motive
                    w2 = *(UINT16 *) (&b[x+2]); // flags
                    if (w2 == 0)
                        ht_fprintf(outFile,TYPE_NORMAL,"for the N current worst motives.");
                    else {
                        w3 = 1;
                        for (c1 = 0; c1 < 16; c1++) {   // this should only find 1 motive (if any)
                            if (w1 & w3) {
                                CHECK_RANGE("Motives", gString86, c1);
                                ht_fprintf(outFile,TYPE_NORMAL," for %s", gString86[c1]);;
                            }
                            w3 = w3 << 1;
                        }
                        ht_fprintf(outFile,TYPE_NORMAL,".");
                        if (w2 & 1)
                            ht_fprintf(outFile,TYPE_NORMAL," Choose remaining motives from the worst.");
                    }
                    if (w2 & 0x200) {
                        ht_fprintf(outFile,TYPE_NORMAL,", Only test objects in room held in temp 0");
                        if (w2 & 0x400)
                            ht_fprintf(outFile,TYPE_NORMAL," including Out Of World Objects");
                        ht_fprintf(outFile,TYPE_NORMAL,".");
                    }
                    if (w2 & 0x800) {
                        ht_fprintf(outFile,TYPE_NORMAL,", Find Nested Interactions Only");
                        if (w2 & 0x1000)
                            ht_fprintf(outFile,TYPE_NORMAL," and only on current Interaction object");
                        ht_fprintf(outFile,TYPE_NORMAL,".");
                    }
                    break;
                case 0x07:  // Refresh (false = error)
                    if (b[x] == 0)
                        ht_fprintf(outFile,TYPE_NORMAL,"My ");
                    else
                        ht_fprintf(outFile,TYPE_NORMAL,"Stack Object's ");
                    if (b[x+2] == 0)
                        ht_fprintf(outFile,TYPE_NORMAL,"graphic");
                    else if (b[x+2] == 1)
                        ht_fprintf(outFile,TYPE_NORMAL,"lighting contribution");
                    else
                        ht_fprintf(outFile,TYPE_NORMAL,"room score contribution");
                    break;
                case 0x0A:  // Tutorial (unused)
                    break;
                case 0x0B:  // Get Distance To (false = error)
                    w1 = *(UINT16 *) (&b[x]);
                    w2 = *(UINT16 *) (&b[x+4]);
                    data2(8, w1);   // temp
                    ht_fprintf(outFile,TYPE_OPERATOR," := distance from ");
                    if (b[x+2] & 1)
                        data2(b[x+3], w2);
                    else
                        ht_fprintf(outFile,TYPE_NORMAL,"Me");
                    ht_fprintf(outFile,TYPE_NORMAL," to Stack Object");
                    if (b[x+6] & 2)
                        ht_fprintf(outFile,TYPE_NORMAL," in 1/100ths of a tile");
                    break;
                case 0x0C:  // Get Direction To (false = error)
                    w1 = *(UINT16 *) (&b[x]);
                    w2 = *(UINT16 *) (&b[x+6]);
                    data2(b[x+2], w1);
                    ht_fprintf(outFile,TYPE_OPERATOR," := direction from ");
                    if (b[x+4] & 1)
                        data2(b[x+5], w2);
                    else
                        ht_fprintf(outFile,TYPE_NORMAL,"Me");
                    ht_fprintf(outFile,TYPE_NORMAL," to Stack Object");
                    if ((b[x+8] & 2) == 0)
                        ht_fprintf(outFile,TYPE_NORMAL," in degrees");
                    break;
                case 0x0E:  // Find Best Object for Function
                    w1 = *(UINT16 *) (&b[x+4]);
                    CHECK_RANGE("Function table", gStringC9, b[x]);
                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gStringC9[b[x]]);
                    if (b[x+2] & 1)
                        ht_fprintf(outFile,TYPE_NORMAL,", Outside Only");
                    else if (b[x+2] & 2)
                        ht_fprintf(outFile,TYPE_NORMAL,", Inside Only");
                    else if (b[x+2] & 4)
                        ht_fprintf(outFile,TYPE_NORMAL,", In Room Only");
                    if (b[x+2] & 0x10)
                        ht_fprintf(outFile,TYPE_NORMAL,", In Line Of Site");
                    if (b[x+2] & 0x40)
                        ht_fprintf(outFile,TYPE_NORMAL,", Ignoring Lockout");
                    if (b[x+2] & 8) {
                        ht_fprintf(outFile,TYPE_NORMAL,", Relative to object in ");
                        data2(b[x+3],w1);
                    }

                    break;
                case 0x0F:  // Break Point (false = error)
                    w1 = *(UINT16 *) (&b[x]);
                    w2 = *(UINT16 *) (&b[x+2]);
                    ht_fprintf(outFile,TYPE_NORMAL,"if (");
                    data2(b[x+2], w1);
                    ht_fprintf(outFile,TYPE_NORMAL," != 0)");
                    break;
                case 0x11:  // Idle for Input
                    w1 = *(UINT16 *) (&b[x+2]);
                    if (b[x+4] & 1)
                        ht_fprintf(outFile,TYPE_NORMAL,"Handle Sub Queue Interactions");
                    else {
                        ht_fprintf(outFile,TYPE_NORMAL,"for ");
                        data2(9, b[x]);
                        ht_fprintf(outFile,TYPE_NORMAL," ticks, ");
                        if (w1 == 0) ht_fprintf(outFile,TYPE_NORMAL,"do not ");
                        ht_fprintf(outFile,TYPE_NORMAL,"allow push");
                    }
                    break;
                case 0x12:  // Remove Object Instance
                    if (b[x] == 0)
                        ht_fprintf(outFile,TYPE_NORMAL,"Me");
                    else
                        ht_fprintf(outFile,TYPE_NORMAL,"Stack Object");
                    if (b[x+2] & 1) ht_fprintf(outFile,TYPE_NORMAL,", return immediately");
                    if ((b[x+2] & 2) == 0) ht_fprintf(outFile,TYPE_NORMAL,", cleanup all");
                    break;
                case 0x13:  // Make New Character
                    w1 = *(UINT16 *) (&b[x+4]);
                    w2 = *(UINT16 *) (&b[x+7]);
                    w3 = *(UINT16 *) (&b[x+11]);

                    if (b[x+9] & 8)
                        ht_fprintf(outFile,TYPE_NORMAL,"getting data from object with GUID in temp 0/1 ");
                    else if (b[x+9] & 0x10)
                        ht_fprintf(outFile,TYPE_NORMAL,"getting data from object with GUID in temp Token ");
                    else if (b[x+9] & 1) {
                        if (b[x+9] & 2)
                            ht_fprintf(outFile,TYPE_NORMAL,"using Neighbor ID in ");
                        else
                            ht_fprintf(outFile,TYPE_NORMAL,"using ID in ");
                        data2(b[x+6],w2);
                        if (b[x+9] & 4)
                            ht_fprintf(outFile,TYPE_NORMAL," as parent 1 and Neighbor ID in ");
                        else
                            ht_fprintf(outFile,TYPE_NORMAL," as parent 1 and ID in ");
                        data2(b[x+3],w1);
                        ht_fprintf(outFile,TYPE_NORMAL," as parent 2 ");
                    } 
                    if (b[x] != 0 && b[x] != 0xFF) {
                        ht_fprintf(outFile,TYPE_NORMAL,"age in ");
                        data2(0x19, b[x+1]);
                        ht_fprintf(outFile,TYPE_NORMAL,", gender in ");
                        data2(0x19, b[x+2]);
                        ht_fprintf(outFile,TYPE_NORMAL,", skin color in ");
                        data2(0x19, b[x]);
                    }
                    if (b[x+9] & 0x20)
                        ht_fprintf(outFile,TYPE_NORMAL,", Getting character from Bin"); // ?
                    if (b[x+9] & 0x40) {
                        ht_fprintf(outFile,TYPE_NORMAL,", Using external guid for Thumbnail Outfit from ");
                        if (b[x+9] & 0x80)
                            ht_fprintf(outFile,TYPE_NORMAL,"GUID in temp 2/3");
                        else
                            data2(b[x+10],w3);
                    }
                    break;
                case 0x14:  // Run Functional Tree
                    CHECK_RANGE("Functional table", gStringC9, b[x]);
                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gStringC9[b[x]]);
                    c1 = b[x+2];
                    if (c1 & 1) ht_fprintf(outFile,TYPE_NORMAL,", change icon");
                    if (c1 & 2) ht_fprintf(outFile,TYPE_NORMAL,", passing parameters from calling tree");
                    if (c1 & 4) ht_fprintf(outFile,TYPE_NORMAL,", running check tree only");
                    break;
                case 0x16:  // Turn Body Towards
                    CHECK_RANGE("Turn body", gStringD8, b[x]);
                    ht_fprintf(outFile,TYPE_FUNCTION,"%s", gStringD8[b[x]]);
                    break;
                case 0x17:  // Play / Stop Sound Event (false = error)
                    w1 = *(UINT16 *) (&b[x]);
                    w2 = *(UINT16 *) (&b[x+2]);
                    if (b[x+4] & 0x40)
                        ht_fprintf(outFile,TYPE_NORMAL,"Stop Sound ");
                    else
                        ht_fprintf(outFile,TYPE_NORMAL,"Play Sound ");
                    if (w1 < 10000)
                        ht_fprintf(outFile,TYPE_NORMAL,"Private id = 0x%X", w1);
                    else if (w1 < 20000)
                        ht_fprintf(outFile,TYPE_NORMAL,"Global id = 0x%X", w1-10000);
                    else
                        ht_fprintf(outFile,TYPE_NORMAL,"SemiGlobal id = 0x%X", w1-20000);
                    if (b[x+4] & 2) ht_fprintf(outFile,TYPE_NORMAL,", with stack obj as source");
                    if ((b[x+4] & 2) == 0) {
                        if (b[x+4] & 0x10)
                            ht_fprintf(outFile,TYPE_NORMAL,", auto vary");
                        if (w2 != 0)
                            ht_fprintf(outFile,TYPE_NORMAL,", sampled at %d, w2"); // ?
                        if (b[x+5] != 0)
                            ht_fprintf(outFile,TYPE_NORMAL,", volume %d", b[x+5]); // ?
                    }
                    break;
                case 0x19:  // Alter Budget
                    c1 = b[x+1];
                    switch (b[x]) {
                        case 0:
                            c1 = 7;     // literal
                            break;
                        case 1:
                            c1 = 9;     // param
                            break;
                        case 2:
                            c1 = 0x19;  // local
                            break;
                    }
                    w1 = *(UINT16 *) (&b[x+2]);
                    w2 = *(UINT16 *) (&b[x+7]);
                    switch (b[x+4] & 3) {
                        case 0:
                            ht_fprintf(outFile,TYPE_NORMAL,"subtract ");
                            break;
                        case 1:
                            ht_fprintf(outFile,TYPE_NORMAL,"test if ");
                            break;
                        case 2:
                            ht_fprintf(outFile,TYPE_NORMAL,"add ");
                            break;
                        case 3:
                            ht_fprintf(outFile,TYPE_NORMAL,"test if ");
                            break;
                    }
                    if ((b[x+4] & 8) && nodeVersion)
                        ht_fprintf(outFile,TYPE_NORMAL,"amount in temp 2 and 3"); // was "temp 3 and 4"  (SimAntics error)
                    else
                        data2(c1, w1);
                    if (b[x+4] & 4)
                        ht_fprintf(outFile,TYPE_NORMAL," Multiplied by value in Temp2");
                    else if (w2 != 0)
                        ht_fprintf(outFile,TYPE_NORMAL," Multiplied by %d", w2);
                    switch (b[x+4] & 3) {
                        case 1:
                            ht_fprintf(outFile,TYPE_NORMAL," may be subtracted");
                            break;
                        case 3:
                            ht_fprintf(outFile,TYPE_NORMAL," may be added");
                            break;
                    }
                    CHECK_RANGE("Expenses", gStringF0, b[x+6]);
                    ht_fprintf(outFile,TYPE_NORMAL," as %s", gStringF0[b[x+6]]);
                    break;
                case 0x1A:  // Relationship
                    c1 = b[x];      // var
                    c2 = b[x+1];    // flags
                    w2 = *(UINT16 *) (&b[x+6]);

                    if (f < 0x8007) {   // old-style parameter usage
                        if ((c2 & 4) == 0) {
                            data2(b[x+4], w2);
                            ht_fprintf(outFile,TYPE_NORMAL," := ");
                        }

                        ht_fprintf(outFile,TYPE_NORMAL,"var %d of ", c1);
                        switch (c2 & 3) {
                            case 0:
                                ht_fprintf(outFile,TYPE_NORMAL,"Me to Stack Object");
                                break;
                            case 1:
                                ht_fprintf(outFile,TYPE_NORMAL,"Stack Object to Me");
                                break;
                            case 2:
                                ht_fprintf(outFile,TYPE_NORMAL,"Stack Object to ");
                                data2(0x19, b[x+3]);
                                break;
                            case 3:
                                data2(0x19, b[x+3]);
                                ht_fprintf(outFile,TYPE_NORMAL," to Stack Object");
                                break;
                        }
                        if (c2 & 4) {
                            ht_fprintf(outFile,TYPE_NORMAL," := ");
                            data2(b[x+4], w2);
                        }
                    } else {            // new-style parameter usage
                        w1 = *(UINT16 *) (&b[x+9]);
                        w3 = *(UINT16 *) (&b[x+3]);

                        ht_fprintf(outFile,TYPE_NORMAL,"Access var %d ", c1);
                        if (c2 & 2) {
                            CHECK_RANGE("Relation Labels", gRelLabels, c1);
                            ht_fprintf(outFile,TYPE_NORMAL,"(%s) ", gRelLabels[c1]);
                        }
                        ht_fprintf(outFile,TYPE_NORMAL,"of ");
                        data2(b[x+2],w3);

                        ht_fprintf(outFile,TYPE_NORMAL," to ");
                        data2(b[x+5],w2);
                        ht_fprintf(outFile,TYPE_NORMAL,". ");
                        if (c2 & 4)
                            ht_fprintf(outFile,TYPE_NORMAL,"Get value from ");
                        else
                            ht_fprintf(outFile,TYPE_NORMAL,"Put value into ");
                        data2(b[x+8], w1);
                    }
                    if (c2 & 1) ht_fprintf(outFile,TYPE_NORMAL,", fail if too small");
                    if (c2 & 2) ht_fprintf(outFile,TYPE_NORMAL,", use neighbors");
                    break;
                case 0x1B:  // Go To Relative Position
                    c1 = (b[x+2] + 2) & 0xFF;
                    c2 = (b[x+3] + 2) & 0xFF;
                    CHECK_RANGE("Relative locations", gString82, c1);
                    ht_fprintf(outFile,TYPE_NORMAL,"Location = %s, ", gString82[c1]);
                    CHECK_RANGE("Relative directions", gString83, c2);
                    ht_fprintf(outFile,TYPE_NORMAL,"Direction = %s", gString83[c2]);
                    c3 = b[x+6];
                    if (c3 & 2) ht_fprintf(outFile,TYPE_NORMAL,", no failure trees");
                    if (c3 & 4) ht_fprintf(outFile,TYPE_NORMAL,", allow different altitudes");
                    break;
                case 0x1D:  // Set Motive Change (false = error)
                    w1 = *(UINT16 *) (&b[x+4]);
                    w2 = *(UINT16 *) (&b[x+6]);
                    if (b[x+3] & 1) {
                        ht_fprintf(outFile,TYPE_NORMAL,"clear all");
                    } else {
                        data2(0xE, b[x+2]);     // my motives
                        ht_fprintf(outFile,TYPE_NORMAL," += ");
                        data2(b[x], w1);
                        ht_fprintf(outFile,TYPE_NORMAL," per hour, stop at ");
                        data2(b[x+1], w2);
                    }
                    if (b[x+3] & 2)
                        ht_fprintf(outFile,TYPE_NORMAL,", Auto Clearing the Person Data Motive Decay value");
                    break;
                case 0x1E:  // Gosub Found Action
                    CHECK_RANGE("Gosub Found Action", gString1FE, b[x]);
                    ht_fprintf(outFile,TYPE_FUNCTION,"%s",gString1FE[b[x]]);
                    break;
                case 0x20:  // Test Object Type
                    w1 = *(UINT16 *) (&b[x+4]);
                    d1 = *(UINT32 *) (&b[x]);
                    if (b[x+7] & 4)
                        ht_fprintf(outFile,TYPE_NORMAL,"Return GUID selected in Temp0/1 ");
                    else {
                        ht_fprintf(outFile,TYPE_NORMAL,"type of ");
                        data2(b[x+6], w1);
                    }
                    if (d1 == 0x4C7CAB2B)
                        ht_fprintf(outFile,TYPE_NORMAL,"the GUID of the temporary inventory token");
                    else {
                        ht_fprintf(outFile,TYPE_NORMAL," == GUID 0x%08X", d1);
                        readGUID(d1);
                    }
                    if ((b[x+7] & 4) == 0) {
                        if (b[x+7] & 1)
                            ht_fprintf(outFile,TYPE_NORMAL," Checking against original, not current GUID");
                        if (b[x+7] & 2)
                            ht_fprintf(outFile,TYPE_NORMAL,", incoming ID is a neighbor ID");
                    }

                    break;
                case 0x21:  // Find 5 Worst Motives (false = error)
                    w1 = *(UINT16 *) (&b[x+4]);
                    w2 = *(UINT16 *) (&b[x+6]);

                    CHECK_RANGE("Short owner", gString99, w1);
                    ht_fprintf(outFile,TYPE_NORMAL,"%s lowest ", gString99[w1]);

                    CHECK_RANGE("Motive type", gStringA5, w2);
                    ht_fprintf(outFile,TYPE_NORMAL,"%s into temps 0-4", gStringA5[w2]);
                    break;
                case 0x22:  // UI Effect (false = error)
                    c1 = b[x];
                    c2 = b[x+5]; // flags
                    w1 = *(UINT16 *) (&b[x+1]);
                    w2 = *(UINT16 *) (&b[x+3]);
                    w3 = *(UINT16 *) (&b[x+6]);
                    w4 = *(UINT16 *) (&b[x+8]);
                    w5 = *(UINT16 *) (&b[x+11]);

                    switch (c1) {
                        case 0:
                            ht_fprintf(outFile,TYPE_NORMAL,"Press Control ");
                            break;
                        case 1:
                            ht_fprintf(outFile,TYPE_NORMAL,"Disable Control ");
                            break;
                        case 2:
                            ht_fprintf(outFile,TYPE_NORMAL,"Enable Control ");
                            break;
                        case 3:
                            ht_fprintf(outFile,TYPE_NORMAL,"Play Effect on Control ");
                            break;
                        case 4:
                            ht_fprintf(outFile,TYPE_NORMAL,"Set Event tree on Control ");
                            break;
                        case 5:
                            ht_fprintf(outFile,TYPE_NORMAL,"Reset State on All Controls ");
                            break;
                        case 6:
                            ht_fprintf(outFile,TYPE_NORMAL,"Disable All Controls ");
                            break;
                        case 7:
                            ht_fprintf(outFile,TYPE_NORMAL,"Reset event trees on All Controls ");
                            break;
                        case 8:
                            ht_fprintf(outFile,TYPE_NORMAL,"Set Event Tree on TNS node ");
                            break;
                        case 9:
                            ht_fprintf(outFile,TYPE_NORMAL,"Set Control Visible ");
                            break;
                        case 0xA:
                            ht_fprintf(outFile,TYPE_NORMAL,"Set Control Hidden ");
                            break;
                    }
                    if (c1 < 5 || c1 > 8)
                        if (c2 & 4) {
                            if (readString2(GROUP_GLOBAL, 0x96, w2) == 0)
                                ht_fprintf(outFile,TYPE_NORMAL,"[Global STR# 0x96:0x%X]", w2);
                        }
                        else if (c2 & 8){
                            if (readString2(gGlobGroup, 0x96, w2) == 0)
                                ht_fprintf(outFile,TYPE_NORMAL,"[SemiGlobal STR# 0x96:0x%X]", w2);
                        }
                        else {
                            if (readString2(gGroup, 0x96, w2) == 0)
                                ht_fprintf(outFile,TYPE_NORMAL,"[Private STR# 0x96:0x%X]", w2);
                        }
                    if (c1 != 8) {
                        ht_fprintf(outFile,TYPE_NORMAL," in window ");
                        if (c2 & 1) {
                            if (readString2(GROUP_GLOBAL, 0x96, w1) == 0)
                                ht_fprintf(outFile,TYPE_NORMAL,"[Global STR# 0x96:0x%X]", w1);
                        }
                        else if (c2 & 2){
                            if (readString2(gGlobGroup, 0x96, w1) == 0)
                                ht_fprintf(outFile,TYPE_NORMAL,"[SemiGlobal STR# 0x96:0x%X]", w1);
                        }
                        else {
                            if (readString2(gGroup, 0x96, w1) == 0)
                                ht_fprintf(outFile,TYPE_NORMAL,"[Private STR# 0x96:0x%X]", w1);
                        }
                    }
                    if (c1 == 3)
                        if (w3 != 0)
                            ht_fprintf(outFile,TYPE_NORMAL," Starting Effect ");
                        else
                            ht_fprintf(outFile,TYPE_NORMAL," Stopping Effect ");
                    if (c1 == 4 || c1 == 8)
                        if (w4 == 0)
                            ht_fprintf(outFile,TYPE_NORMAL," No Event Tree");
                        else {
                            switch (b[x+10]) {
                                case 0:
                                    ht_fprintf(outFile,TYPE_NORMAL," Using Private Event Tree: ");
                                    break;
                                case 1:
                                    ht_fprintf(outFile,TYPE_NORMAL," Using SemiGlobal Event Tree: ");
                                    break;
                                default:
                                    ht_fprintf(outFile,TYPE_NORMAL," Using Global Event Tree: ");
                                    break;
                            }
                            ht_fprintf(outFile,TYPE_NORMAL,"0x%X (",w4);
                            readFn2(w4);
                            ht_fprintf(outFile,TYPE_NORMAL,")");
                        }
                    if (c1 == 8) {
                        ht_fprintf(outFile,TYPE_NORMAL,", Getting TNS ID from ");
                        data2(b[x+13],w5);
                    }
                    break;
                case 0x23:  // Camera Control (false = error)
                    c1 = b[x+4];    // flags
                    w1 = *(UINT16 *) (&b[x]);
                    if (c1 & 1) {
                        ht_fprintf(outFile,TYPE_NORMAL,"show stack obj");
                        switch (b[x+3]) {
                            case 1:
                                ht_fprintf(outFile,TYPE_NORMAL,", far");
                                break;
                            case 2:
                                ht_fprintf(outFile,TYPE_NORMAL,", mid");
                                break;
                            case 3:
                                ht_fprintf(outFile,TYPE_NORMAL,", near");
                                break;
                        }
                        ht_fprintf(outFile,TYPE_NORMAL," zoom");
                        if ((c1 & 0x40) == 0)
                            ht_fprintf(outFile,TYPE_NORMAL,", slow down");
                        if (c1 & 8)
                            ht_fprintf(outFile,TYPE_NORMAL,", center");
                        if (c1 & 0x20)
                            ht_fprintf(outFile,TYPE_NORMAL,", using timeout in temp 0");
                        else {
                            ht_fprintf(outFile,TYPE_NORMAL,", using timeout of ");
                            if (w1 < 10)
                                ht_fprintf(outFile,TYPE_NORMAL,"%d", w1);
                            else
                                ht_fprintf(outFile,TYPE_NORMAL,"%d (0x%X)", w1, w1);
                        }
                    } else
                        ht_fprintf(outFile,TYPE_NORMAL,"un-show stack obj");
                    break;
                case 0x24:  // Dialog
                    c1 = b[x+5];                 // prim
                    c2 = (b[x+7] >> 4) & 7;      // temp
                    c3 = b[x+8];                 // flags
                    if (nodeVersion) {
                        w1 = *(UINT16 *) (&b[x+13]); // message
                        w2 = b[x+2] << 8 + b[x]; // cancel
                    } else {
                        w1 = b[x+2];             // message
                        w2 = b[x];               // cancel
                    }
                    CHECK_RANGE("Dialog primitives", gStringD9, c1);
                    if (c1 == 2) {
                        data2(8, c2); // Temp
                        ht_fprintf(outFile,TYPE_NORMAL," := tri choice: ");
                    } else
                        ht_fprintf(outFile,TYPE_NORMAL,"%s: ", gStringD9[c1]);

                    if (c1 == 9) {
                        ht_fprintf(outFile,TYPE_NORMAL,", Getting Text Notification ID from temp %d", c2);
                        switch (b[x+12]) {
                            case 0:
                                ht_fprintf(outFile,TYPE_NORMAL,", TNS Sim Type");
                                break;
                            case 1:
                                ht_fprintf(outFile,TYPE_NORMAL,", TNS System Type");
                                break;
                        }
                    }
                    if (c1 == 0xE)
                        ht_fprintf(outFile,TYPE_NORMAL,"passing through data starting with local var %d", b[x+11]);

                    if (c1 < 9 || c1 == 0xA || c1 == 0xF || c1 == 0x13 || c1 > 0x14) {

                        if (c3 & 2) // ?
                            ht_fprintf(outFile,TYPE_NORMAL,", expecting message Index in temp 0");
                        else if (c3 & 1)
                            ht_fprintf(outFile,TYPE_NORMAL,"[SemiGlobal STR# 0x12D:0x%X]", w1 - 1);
                        else if (c3 & 0x40) {
                            CHECK_RANGE("Global Dialog Primitives", gDialogPrim, w1 - 1);
                            ht_fprintf(outFile,TYPE_NORMAL,"%s", gDialogPrim[w1 - 1]);
                        } else
                            ht_fprintf(outFile,TYPE_NORMAL,"[Private STR# 0x12D:0x%X]", w1 - 1);
                            

                        if (c1 == 8 || c1 == 0xA) {
                            switch (b[12]) {
                                case 0:
                                    ht_fprintf(outFile,TYPE_NORMAL,", TNS Sim Type");
                                    break;
                                case 1:
                                    ht_fprintf(outFile,TYPE_NORMAL,", TNS System Type");
                                    break;
                                case 2:
                                    ht_fprintf(outFile,TYPE_NORMAL,", TNS System Dialog Type");
                                    break;
                                case 3:
                                    ht_fprintf(outFile,TYPE_NORMAL,", TNS Birthday Type");
                                    break;
                            }
                            ht_fprintf(outFile,TYPE_NORMAL,", priority %d", b[x+9] + 1);
                            ht_fprintf(outFile,TYPE_NORMAL,", timeout %d", b[x+10]);
                            if (c1 == 0xA)
                                ht_fprintf(outFile,TYPE_NORMAL,", getting Text ID in temp ");
                            else
                                ht_fprintf(outFile,TYPE_NORMAL,", putting Text ID in temp ");
                            ht_fprintf(outFile,TYPE_NORMAL,"%d",c2);

                        } else {

                            if (c3 & 4) // ?
                                ht_fprintf(outFile,TYPE_NORMAL,", expecting Yes button Index in temp 0");
                            else if (b[x+3] != 0) {
                                ht_fprintf(outFile,TYPE_NORMAL,", Yes: ");
                                ht_fprintf(outFile,TYPE_NORMAL,"[STR# 0x12D:0x%X]", b[x+3] - 1);
                            }
                            if (c3 & 8) // ?
                                ht_fprintf(outFile,TYPE_NORMAL,", expecting No button Index in temp 0");
                            else if (b[x+4] != 0) {
                                ht_fprintf(outFile,TYPE_NORMAL,", No: ");
                                ht_fprintf(outFile,TYPE_NORMAL,"[STR# 0x12D:0x%X]", b[x+4] - 1);
                            }
                            if (c3 & 0x10) // ?
                                ht_fprintf(outFile,TYPE_NORMAL,", expecting Title Index in temp 0");
                            else if (b[x+6] != 0) {
                                ht_fprintf(outFile,TYPE_NORMAL,", Title: ");
                                ht_fprintf(outFile,TYPE_NORMAL,"[STR# 0x12D:0x%X]", b[x+6] - 1);
                            }
                            if (c3 & 0x20) // ?
                                ht_fprintf(outFile,TYPE_NORMAL,", expecting Cancel Index in temp 0");
                            else if (w2 != 0) {
                                ht_fprintf(outFile,TYPE_NORMAL,", Cancel: ");
                                ht_fprintf(outFile,TYPE_NORMAL,"[STR# 0x12D:0x%X]", w2 - 1);
                            }
                            if ((b[x+7] & 0x81) != 0) {
                                if (b[x+7] & 1)
                                    ht_fprintf(outFile,TYPE_NORMAL," return");
                                else
                                    ht_fprintf(outFile,TYPE_NORMAL," engage");
                                ht_fprintf(outFile,TYPE_NORMAL," and ");
                                if (c1 & 0x80)
                                    ht_fprintf(outFile,TYPE_NORMAL,"continue sim");
                                else
                                    ht_fprintf(outFile,TYPE_NORMAL,"block sim");
                            }
                        }
                        if (w1 != 0x16 && w1 != 0x19 && (b[x+7] & 0xE) != 2) {
                            ht_fprintf(outFile,TYPE_NORMAL,", icon: ");
                            switch ((b[x+7] >> 1)& 7) {
                                case 3:
                                    ht_fprintf(outFile,TYPE_NORMAL,"BMP = 0x%X", (b[x+1] + 5000));
                                    break;
                                case 4:
                                    ht_fprintf(outFile,TYPE_NORMAL,"[STR# 0x12D:0x%X]", b[x+1] - 1);
                                    break;
                                default:
                                    CHECK_RANGE("Icon types", gStringF4, (b[x+7] >> 1) & 7);
                                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gStringF4[(b[x+7] >> 1) & 7]);
                                    break;
                            }
                        }

                    }
                    break;
                case 0x25:  // Test Sim Interacting With
                    ht_fprintf(outFile,TYPE_NORMAL,"me with stack obj.");
                    break;
                case 0x2D:  // Go To Routing Slot
                    w1 = *(UINT16 *) (&b[x]);
                    w2 = *(UINT16 *) (&b[x+2]);
                    switch (w2) {
                        case 0:
                            ht_fprintf(outFile,TYPE_NORMAL,"in ");
                            data2(9, w1);   // param
                            break;
                        case 1:
                            if (b[x+4] & 2)
                                ht_fprintf(outFile,TYPE_NORMAL,"From Slot index in Temp 0", w1);
                            else
                                ht_fprintf(outFile,TYPE_NORMAL,"0x%X", w1);
                            break;
                        case 2:
                            ht_fprintf(outFile,TYPE_NORMAL,"global 0x%X", w1);
                            break;
                        default:
                            ht_fprintf(outFile,TYPE_NORMAL,"??? 0x%X", w1);
                    }
                    if (b[x+4] & 1) ht_fprintf(outFile,TYPE_NORMAL,", no failure trees");
                    if (b[x+4] & 4) ht_fprintf(outFile,TYPE_NORMAL,", ignoring dest obj footprint");
                    if (b[x+4] & 8) ht_fprintf(outFile,TYPE_NORMAL,", allow different altitudes");
                    break;
                case 0x2E:  // Snap
                    w1 = *(UINT16 *) (&b[x]);
                    w2 = *(UINT16 *) (&b[x+2]);
                    w3 = *(UINT16 *) (&b[x+8]);
                    CHECK_RANGE("Snap mode", gStringCF, w2);
                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gStringCF[w2]);
                    if (w2 == 0 || w2 == 3 || w2 == 4)
                        if (b[x+4] & 8)
                            ht_fprintf(outFile,TYPE_NORMAL," to slot in temp 0");
                        else
                            ht_fprintf(outFile,TYPE_NORMAL," 0x%X", w1);
                    if (w3 == 1)
                        ht_fprintf(outFile,TYPE_NORMAL,", from slot in temp 1");
                    if (b[x+4] & 2)
                        ht_fprintf(outFile,TYPE_NORMAL,", ask person to move");
                    if (b[x+4] & 0x10)
                        ht_fprintf(outFile,TYPE_NORMAL,", TEST ONLY");
                    break;
                case 0x30:  // Stop ALL Sounds (false = error)
                    w1 = *(UINT16 *) (&b[x]);
                    if (w1 == 0)
                        ht_fprintf(outFile,TYPE_NORMAL,"of Me");
                    else
                        ht_fprintf(outFile,TYPE_NORMAL,"of Stack Object");
                    break;
                case 0x31:  // Notify the Stack Object out of Idle (implied, false = error)
                    break;
                case 0x33:  // Manage Inventory
                    w1 = *(UINT16 *) (&b[x+14]);
                    w2 = *(UINT16 *) (&b[x+11]);
                    w3 = *(UINT16 *) (&b[x+2]);
                    ht_fprintf(outFile,TYPE_NORMAL,"Access the ");
                    switch (b[x] & 3) {
                        case 0:
                            ht_fprintf(outFile,TYPE_NORMAL,"Global ");
                            break;
                        case 1:
                            ht_fprintf(outFile,TYPE_NORMAL,"Lot ");
                            break;
                        case 2:
                            ht_fprintf(outFile,TYPE_NORMAL,"Family ");
                            break;
                        case 3:
                            ht_fprintf(outFile,TYPE_NORMAL,"Neighbor ");
                            break;
                    }
                    if (b[x] & 4)
                        ht_fprintf(outFile,TYPE_NORMAL,"Counted Inventory");
                    else
                        ht_fprintf(outFile,TYPE_NORMAL,"Singular Inventory");
                    if ((b[x] & 3) != 0) {
                        ht_fprintf(outFile,TYPE_NORMAL," from ID ");
                        data2(b[x+1], w3);
                    }
                    ht_fprintf(outFile,TYPE_NORMAL,". with category %d",b[x+9]);
                    d1 = *(UINT32 *) (&b[x+5]);
                    if (d1 != 0) {
                        ht_fprintf(outFile,TYPE_NORMAL," GUID 0x%08X", d1);
                        readGUID(d1);
                        ht_fprintf(outFile,TYPE_NORMAL,". "); //?
                    }
                    else
                        ht_fprintf(outFile,TYPE_NORMAL," of GUID from Stack Object. ");
                    if (b[x] & 4)
                        switch (b[x+4]) {
                            case 0:
                                ht_fprintf(outFile,TYPE_NORMAL,"Add token");
                                ht_fprintf(outFile,TYPE_NORMAL,". Pull count from ");
                                data2(b[x+13], w1);
                                break;
                            case 1:
                                ht_fprintf(outFile,TYPE_NORMAL,"Add to token at index from ");
                                data2(b[x+10], w2);
                                ht_fprintf(outFile,TYPE_NORMAL,". Pull count from ");
                                data2(b[x+13], w1);
                                break;
                            case 2:
                                ht_fprintf(outFile,TYPE_NORMAL,"Remove token");
                                ht_fprintf(outFile,TYPE_NORMAL,". Pull count from ");
                                data2(b[x+13], w1);
                                break;
                            case 3:
                                ht_fprintf(outFile,TYPE_NORMAL,"Remove to token at index from");
                                data2(b[x+10], w2);
                                ht_fprintf(outFile,TYPE_NORMAL,". Pull count from ");
                                data2(b[x+13], w1);
                                break;
                            case 4:
                                ht_fprintf(outFile,TYPE_NORMAL,"Remove all tokens");
                                ht_fprintf(outFile,TYPE_NORMAL,".");
                                break;
                            case 5:
                                ht_fprintf(outFile,TYPE_NORMAL,"Remove all tokens from token at index from");
                                data2(b[x+10], w2);
                                ht_fprintf(outFile,TYPE_NORMAL,".");
                                break;
                            case 6:
                                ht_fprintf(outFile,TYPE_NORMAL,"Find the token");
                                ht_fprintf(outFile,TYPE_NORMAL,". Put count into ");
                                data2(b[x+13], w1);
                                ht_fprintf(outFile,TYPE_NORMAL,".");
                                break;
                            case 7:
                                ht_fprintf(outFile,TYPE_NORMAL,"Read token");
                                ht_fprintf(outFile,TYPE_NORMAL," into My Temp Token.");
                                break;
                            case 8:
                                ht_fprintf(outFile,TYPE_NORMAL,"Read token at index from ");
                                data2(b[x+10], w2);
                                ht_fprintf(outFile,TYPE_NORMAL," into My Temp Token.");
                                break;
                            case 9:
                                ht_fprintf(outFile,TYPE_NORMAL,"Set To Next token");
                                ht_fprintf(outFile,TYPE_NORMAL,". Starting at index from ");
                                data2(b[x+10], w2);
                                break;
                            case 0xA:
                                ht_fprintf(outFile,TYPE_NORMAL,"Store the count of the tokens in this inventory into ");
                                data2(b[x+13], w1);
                                ht_fprintf(outFile,TYPE_NORMAL,".");
                                break;
                        }
                    else
                        switch (b[x+4]) {
                            case 0:
                                ht_fprintf(outFile,TYPE_NORMAL,"Add token");
                                ht_fprintf(outFile,TYPE_NORMAL,".");
                                break;
                            case 1:
                                ht_fprintf(outFile,TYPE_NORMAL,"Remove token at index from ");
                                data2(b[x+10], w2);
                                ht_fprintf(outFile,TYPE_NORMAL,".");
                                break;
                            case 2:
                                ht_fprintf(outFile,TYPE_NORMAL,"Remove at tokens");
                                ht_fprintf(outFile,TYPE_NORMAL,".");
                                break;
                            case 3:
                                ht_fprintf(outFile,TYPE_NORMAL,"Set To Next token");
                                ht_fprintf(outFile,TYPE_NORMAL,". Starting at index from ");
                                data2(b[x+10], w2);
                                break;
                            case 4:
                                ht_fprintf(outFile,TYPE_NORMAL,"Push property on token at index from ");
                                data2(b[x+10], w2);
                                ht_fprintf(outFile,TYPE_NORMAL,". Get property value from ");
                                data2(b[x+13], w1);
                                break;
                            case 5:
                                ht_fprintf(outFile,TYPE_NORMAL,"Pop property off token at index from ");
                                data2(b[x+10], w2);
                                ht_fprintf(outFile,TYPE_NORMAL,". Put property value into ");
                                data2(b[x+13], w1);
                                ht_fprintf(outFile,TYPE_NORMAL,".");
                                break;
                            case 6:
                                ht_fprintf(outFile,TYPE_NORMAL,"Read token into My Temp Token at index from ");
                                data2(b[x+10], w2);
                                ht_fprintf(outFile,TYPE_NORMAL,".");
                                break;
                            case 7:
                                ht_fprintf(outFile,TYPE_NORMAL,"Get property from token in My Temp Token at index from ");
                                data2(b[x+10], w2);
                                ht_fprintf(outFile,TYPE_NORMAL,". Put property value into ");
                                data2(b[x+13], w1);
                                ht_fprintf(outFile,TYPE_NORMAL,".");
                                break;
                            case 8:
                                break;
                            case 9:
                                ht_fprintf(outFile,TYPE_NORMAL,"Save My Temp Token back to the location it was loaded from.");
                                break;
                            case 0xA:
                                ht_fprintf(outFile,TYPE_NORMAL,"Store the count of the tokens in this inventory into ");
                                data2(b[x+13], w1);
                                ht_fprintf(outFile,TYPE_NORMAL,".");
                                break;
                            case 0xB:
                                break;
                            case 0xC:
                                ht_fprintf(outFile,TYPE_NORMAL,"Set To Next ");
                                if (b[x] & 8)
                                    ht_fprintf(outFile,TYPE_NORMAL,"visible ");
                                else
                                    ht_fprintf(outFile,TYPE_NORMAL,"hidden ");
                                if (b[x] & 0x10)
                                    ht_fprintf(outFile,TYPE_NORMAL,"memory ");
                                else
                                    ht_fprintf(outFile,TYPE_NORMAL,"non-memory ");
                                ht_fprintf(outFile,TYPE_NORMAL,"token. Starting at index from ");
                                data2(b[x+10], w2);
                                ht_fprintf(outFile,TYPE_NORMAL,".");
                                break;
                            case 0xD:
                                ht_fprintf(outFile,TYPE_NORMAL,"Store the count of the ");
                                if (b[x] & 8)
                                    ht_fprintf(outFile,TYPE_NORMAL,"visible ");
                                else
                                    ht_fprintf(outFile,TYPE_NORMAL,"hidden ");
                                if (b[x] & 0x10)
                                    ht_fprintf(outFile,TYPE_NORMAL,"memory ");
                                else
                                    ht_fprintf(outFile,TYPE_NORMAL,"non-memory ");
                                ht_fprintf(outFile,TYPE_NORMAL,"tokens in this inventory into ");
                                data2(b[x+13], w1);
                                ht_fprintf(outFile,TYPE_NORMAL,".");
                                break;
                        }
                    break;
                case 0x69:  // Animate Object (false = error)
                    c1 = b[x+2];
                    c2 = b[x+10];
                    w1 = *(UINT16 *) (&b[x+7]);
                    w2 = *(UINT16 *) (&b[x]);
                    w3 = *(UINT16 *) (&b[x+4]);
                    if (c1 & 4)
                        ht_fprintf(outFile,TYPE_NORMAL,"anim id in STR# 0x86:[Param %d]", w2);
                    else {
                        if (readString2(gGroup, 0x86, w2) == 0)
                            ht_fprintf(outFile,TYPE_NORMAL,"anim id in STR# 0x86:%X", w2);
                    }
                    ht_fprintf(outFile,TYPE_NORMAL," affecting ");
                    data2(b[x+6],w1);       // target object
                    if (w3 == 0)
                        ht_fprintf(outFile,TYPE_NORMAL,", No Event Tree");
                    else {
                        switch (b[x+9]) {
                            case 0:
                                ht_fprintf(outFile,TYPE_NORMAL,", Private Event Tree: ");
                                break;
                            case 1:
                                ht_fprintf(outFile,TYPE_NORMAL,", SemiGlobal Event Tree: ");
                                break;
                            default:
                                ht_fprintf(outFile,TYPE_NORMAL,", Global Event Tree: ");
                                break;
                        }
                        ht_fprintf(outFile,TYPE_NORMAL,"0x%X (",w3);
                        readFn2(w3);
                        ht_fprintf(outFile,TYPE_NORMAL,")");
                    }
                    if (c1 & 8)
                        ht_fprintf(outFile,TYPE_NORMAL,", Interruptible");
                    if (c1 & 1)
                        ht_fprintf(outFile,TYPE_NORMAL,", Flipped");
                    if (c1 & 0x10)
                        ht_fprintf(outFile,TYPE_NORMAL,", Start at tag in Temp 0");
                    if (c1 & 0x20)
                        ht_fprintf(outFile,TYPE_NORMAL,", Loop Count in Temp 1");
                    if (c1 & 0x80)
                        ht_fprintf(outFile,TYPE_NORMAL,", No Blend in");
                    if (c1 & 0x40)
                        ht_fprintf(outFile,TYPE_NORMAL,", No Blend out");
                    if (c1 & 2)
                        ht_fprintf(outFile,TYPE_NORMAL,", Anim Speed in Temp2");
                    if (c2 & 1)
                        ht_fprintf(outFile,TYPE_NORMAL,", Flip Flag in Temp 3");
                    if (c2 & 4)
                        ht_fprintf(outFile,TYPE_NORMAL,", Sync to calling objects Anim");
                    if (c2 & 8)
                        ht_fprintf(outFile,TYPE_NORMAL,", Align blend out with to calling objects Anim");
                    if (c2 & 0x80)
                        ht_fprintf(outFile,TYPE_NORMAL,", Not Hurryable");
                    break;
                case 0x6A:  // Animate Sim
                    c1 = b[x+2];
                    c2 = b[x+8];
                    w1 = *(UINT16 *) (&b[x]);
                    w2 = *(UINT16 *) (&b[x+4]);
                    w3 = *(UINT16 *) (&b[x+10]);
                    if (c1 & 4)
                        ht_fprintf(outFile,TYPE_NORMAL,"anim id in Param %d", w1);
                    else
                        ht_fprintf(outFile,TYPE_NORMAL,"anim id %X", w1);
                    ht_fprintf(outFile,TYPE_NORMAL," from ");
                    switch (b[x+6]) {
                        case 0x80:
                            ht_fprintf(outFile,TYPE_NORMAL,"Global list");
                            break;
                        case 0x81:
                            ht_fprintf(outFile,TYPE_NORMAL,"Adult list");
                            break;
                        case 0x82:
                            ht_fprintf(outFile,TYPE_NORMAL,"Child list");
                            break;
                        case 0x83:
                            ht_fprintf(outFile,TYPE_NORMAL,"Social Interaction list");
                            break;
                        case 0x89:
                            ht_fprintf(outFile,TYPE_NORMAL,"Toddler list");
                            break;
                        case 0x8A:
                            ht_fprintf(outFile,TYPE_NORMAL,"Teen list");
                            break;
                        case 0x8B:
                            ht_fprintf(outFile,TYPE_NORMAL,"Elder list");
                            break;
                        case 0x8C:
                            ht_fprintf(outFile,TYPE_NORMAL,"Cat list");
                            break;
                        case 0x8D:
                            ht_fprintf(outFile,TYPE_NORMAL,"Dog list");
                            break;
                        case 0x91:
                            ht_fprintf(outFile,TYPE_NORMAL,"Baby list");
                            break;
                        default:
                            ht_fprintf(outFile,TYPE_NORMAL,"Object list");
                            break;
                    }
                    if ((c1 & 4) == 0) {
                        ht_fprintf(outFile,TYPE_NORMAL," (");
                        if (b[x+6] == 0x80) {
                            if (readString2(GROUP_GLOBAL, 0x81, w1) == 0)
                                ht_fprintf(outFile,TYPE_NORMAL,"Global STR# 0x81:%X", 0x81, w1);
                        } else if ((b[x+6] > 0x80 && b[x+6] < 0x84) || (b[x+6] > 0x88 && b[x+6] < 0x8E) || b[x+6] == 91) {
                            if (readString2(gGroup, b[x+6], w1) == 0)
                                  ht_fprintf(outFile,TYPE_NORMAL,"STR# 0x%X:%X", b[x+6], w1);
                        } else
                            if (readString2(gGroup, 0x86, w1) == 0)
                                ht_fprintf(outFile,TYPE_NORMAL,"STR# 0x86:%X", 0x86, w1);
                        ht_fprintf(outFile,TYPE_NORMAL,")");
                    }
                    if (w2 == 0)
                        ht_fprintf(outFile,TYPE_NORMAL,", No Event Tree");
                    else {
                        switch (b[x+7]) {
                            case 0:
                                ht_fprintf(outFile,TYPE_NORMAL,", Private Event Tree: ");
                                break;
                            case 1:
                                ht_fprintf(outFile,TYPE_NORMAL,", SemiGlobal Event Tree: ");
                                break;
                            default:
                                ht_fprintf(outFile,TYPE_NORMAL,", Global Event Tree: ");
                                break;
                        }
                        ht_fprintf(outFile,TYPE_NORMAL,"0x%X (",w2);
                        readFn2(w2);
                        ht_fprintf(outFile,TYPE_NORMAL,")");
                    }
                    if (c1 & 8)
                        ht_fprintf(outFile,TYPE_NORMAL,", Interruptible");
                    if (c1 & 1)
                        ht_fprintf(outFile,TYPE_NORMAL,", Flipped");
                    if (c1 & 0x20)
                        ht_fprintf(outFile,TYPE_NORMAL,", Trans to Idle");
                    if (c1 & 0x80)
                        ht_fprintf(outFile,TYPE_NORMAL,", No Blend in");
                    if (c1 & 0x40)
                        ht_fprintf(outFile,TYPE_NORMAL,", No Blend out");
                    if (c1 & 0x10)
                        ht_fprintf(outFile,TYPE_NORMAL,", Start at tag in Temp 0");
                    if (c1 & 2)
                        ht_fprintf(outFile,TYPE_NORMAL,", Anim Speed in Temp2");
                    if (c2 & 1)
                        ht_fprintf(outFile,TYPE_NORMAL,", Flip Flag in Temp 3");
                    if (c2 & 2)
                        ht_fprintf(outFile,TYPE_NORMAL,", Synching Anim to last Anim Run");
                    if (c2 & 0x10)
                        ht_fprintf(outFile,TYPE_NORMAL,", Using Controlling Object as Anim Source");
                    if (c2 & 0x20)
                        ht_fprintf(outFile,TYPE_NORMAL,", Not Hurryable");
                    ht_fprintf(outFile,TYPE_NORMAL,", IK Object ");
                    data2(b[x+9],w3);
                    switch (b[x+12]) {
                        case 0:
                            ht_fprintf(outFile,TYPE_NORMAL,", low priority");
                            break;
                        case 1:
                            ht_fprintf(outFile,TYPE_NORMAL,", medium priority");
                            break;
                        case 2:
                            ht_fprintf(outFile,TYPE_NORMAL,", high priority");
                            break;
                    }
                    break;
                case 0x6B:  // Animate Overlay (false = error)
                    c1 = b[x+2];
                    c2 = b[x+15];
                    w1 = *(UINT16 *) (&b[x]);
                    w2 = *(UINT16 *) (&b[x+4]);
                    w3 = *(UINT16 *) (&b[x+7]);
                    if (c1 & 4)
                        ht_fprintf(outFile,TYPE_NORMAL,"anim id in Param %d", w1);
                    else
                        ht_fprintf(outFile,TYPE_NORMAL,"anim id %X", w1);
                    ht_fprintf(outFile,TYPE_NORMAL," from ");
                    switch (b[x+6]) {
                        case 0x80:
                            ht_fprintf(outFile,TYPE_NORMAL,"Global list");
                            break;
                        case 0x81:
                            ht_fprintf(outFile,TYPE_NORMAL,"Adult list");
                            break;
                        case 0x82:
                            ht_fprintf(outFile,TYPE_NORMAL,"Child list");
                            break;
                        case 0x83:
                            ht_fprintf(outFile,TYPE_NORMAL,"Social Interaction list");
                            break;
                        case 0x89:
                            ht_fprintf(outFile,TYPE_NORMAL,"Toddler list");
                            break;
                        case 0x8A:
                            ht_fprintf(outFile,TYPE_NORMAL,"Teen list");
                            break;
                        case 0x8B:
                            ht_fprintf(outFile,TYPE_NORMAL,"Elder list");
                            break;
                        case 0x8C:
                            ht_fprintf(outFile,TYPE_NORMAL,"Cat list");
                            break;
                        case 0x8D:
                            ht_fprintf(outFile,TYPE_NORMAL,"Dog list");
                            break;
                        case 0x91:
                            ht_fprintf(outFile,TYPE_NORMAL,"Baby list");
                            break;
                        default:
                            ht_fprintf(outFile,TYPE_NORMAL,"Object list");
                            break;
                    }
                    if ((c1 & 4) == 0) {
                        ht_fprintf(outFile,TYPE_NORMAL," (");
                        if (b[x+9] == 0x80) {
                            if (readString2(GROUP_GLOBAL, 0x81, w1) == 0)
                                ht_fprintf(outFile,TYPE_NORMAL,"Global STR# 0x81:%X", 0x81, w1);
                        } else if ((b[x+9] > 0x80 && b[x+9] < 0x84) || (b[x+9] > 0x88 && b[x+9] < 0x8E) || b[x+9] == 91) {
                            if (readString2(gGroup, b[x+9], w1) == 0)
                                  ht_fprintf(outFile,TYPE_NORMAL,"STR# 0x%X:%X", b[x+9], w1);
                        } else
                            if (readString2(gGroup, 0x86, w1) == 0)
                                ht_fprintf(outFile,TYPE_NORMAL,"STR# 0x86:%X", 0x86, w1);
                        ht_fprintf(outFile,TYPE_NORMAL,")");
                    }
                    ht_fprintf(outFile,TYPE_NORMAL," affecting ");
                    data2(b[x+6],w3);       // target object
                    if (w2 == 0)
                        ht_fprintf(outFile,TYPE_NORMAL,", No Event Tree");
                    else {
                        switch (b[x+14]) {
                            case 0:
                                ht_fprintf(outFile,TYPE_NORMAL,", Private Event Tree: ");
                                break;
                            case 1:
                                ht_fprintf(outFile,TYPE_NORMAL,", SemiGlobal Event Tree: ");
                                break;
                            default:
                                ht_fprintf(outFile,TYPE_NORMAL,", Global Event Tree: ");
                                break;
                        }
                        ht_fprintf(outFile,TYPE_NORMAL,"0x%X (",w2);
                        readFn2(w2);
                        ht_fprintf(outFile,TYPE_NORMAL,")");
                    }
                    if (c1 & 8)
                        ht_fprintf(outFile,TYPE_NORMAL,", Interruptible");
                    if (c1 & 1)
                        ht_fprintf(outFile,TYPE_NORMAL,", Flipped");
                    if (c1 & 0x80)
                        ht_fprintf(outFile,TYPE_NORMAL,", No Blend in");
                    if (c1 & 0x40)
                        ht_fprintf(outFile,TYPE_NORMAL,", No Blend out");
                    if (c1 & 0x10)
                        ht_fprintf(outFile,TYPE_NORMAL,", Start at tag in Temp 0");
                    if (c1 & 0x20)
                        ht_fprintf(outFile,TYPE_NORMAL,", Loop Count in Temp 1");
                    if (c1 & 2)
                        ht_fprintf(outFile,TYPE_NORMAL,", Anim Speed in Temp2");
                    if (c2 & 1)
                        ht_fprintf(outFile,TYPE_NORMAL,", Flip Flag in Temp 3");
                    if (c2 & 0x10)
                        ht_fprintf(outFile,TYPE_NORMAL,", Sync to calling objects Anim");
                    if (c2 & 0x20)
                        ht_fprintf(outFile,TYPE_NORMAL,", Align blend out with to calling objects Anim");
                    if (c2 & 0x40)
                        ht_fprintf(outFile,TYPE_NORMAL,", Using tree owners anim list");
                    if (b[x+12] & 1)
                        ht_fprintf(outFile,TYPE_NORMAL,", Not Hurryable");
                    switch (b[x+11]) {
                        case 0:
                            ht_fprintf(outFile,TYPE_NORMAL,", low priority");
                            break;
                        case 1:
                            ht_fprintf(outFile,TYPE_NORMAL,", medium priority");
                            break;
                        case 2:
                            ht_fprintf(outFile,TYPE_NORMAL,", high priority");
                            break;
                    }
                    break;
                case 0x6C:  // Animate Stop (false = error)
                    c1 = b[x+2];
                    w1 = *(UINT16 *) (&b[x]);
                    w2 = *(UINT16 *) (&b[x+4]);
                    switch (b[x+7]) {
                        case 0:
                            if (c1 & 4)
                                ht_fprintf(outFile,TYPE_NORMAL,"anim id in Param %d", w1);
                            else
                                ht_fprintf(outFile,TYPE_NORMAL,"anim id %X", w1);
                            ht_fprintf(outFile,TYPE_NORMAL," from ");
                            switch (b[x+6]) {
                                case 0x80:
                                    ht_fprintf(outFile,TYPE_NORMAL,"Global list");
                                    break;
                                case 0x81:
                                    ht_fprintf(outFile,TYPE_NORMAL,"Adult list");
                                    break;
                                case 0x82:
                                    ht_fprintf(outFile,TYPE_NORMAL,"Child list");
                                    break;
                                case 0x83:
                                    ht_fprintf(outFile,TYPE_NORMAL,"Social Interaction list");
                                    break;
                                case 0x89:
                                    ht_fprintf(outFile,TYPE_NORMAL,"Toddler list");
                                    break;
                                case 0x8A:
                                    ht_fprintf(outFile,TYPE_NORMAL,"Teen list");
                                    break;
                                case 0x8B:
                                    ht_fprintf(outFile,TYPE_NORMAL,"Elder list");
                                    break;
                                case 0x8C:
                                    ht_fprintf(outFile,TYPE_NORMAL,"Cat list");
                                    break;
                                case 0x8D:
                                    ht_fprintf(outFile,TYPE_NORMAL,"Dog list");
                                    break;
                                case 0x91:
                                    ht_fprintf(outFile,TYPE_NORMAL,"Baby list");
                                    break;
                                default:
                                    ht_fprintf(outFile,TYPE_NORMAL,"Object list");
                                    break;
                            }
                            if ((c1 & 4) == 0) {
                                ht_fprintf(outFile,TYPE_NORMAL," (");
                                if (b[x+6] == 0x80) {
                                    if (readString2(GROUP_GLOBAL, 0x81, w1) == 0)
                                        ht_fprintf(outFile,TYPE_NORMAL,"Global STR# 0x81:%X", 0x81, w1);
                                } else if ((b[x+6] > 0x80 && b[x+6] < 0x84) || (b[x+6] > 0x88 && b[x+6] < 0x8E) || b[x+6] == 91) {
                                    if (readString2(gGroup, b[x+6], w1) == 0)
                                          ht_fprintf(outFile,TYPE_NORMAL,"STR# 0x%X:%X", b[x+6], w1);
                                } else
                                    if (readString2(gGroup, 0x86, w1) == 0)
                                        ht_fprintf(outFile,TYPE_NORMAL,"STR# 0x86:%X", 0x86, w1);
                                ht_fprintf(outFile,TYPE_NORMAL,")");
                            }
                            break;
                        case 1:
                            ht_fprintf(outFile,TYPE_NORMAL,"All Overlay animations running");
                            break;
                        case 2:
                            ht_fprintf(outFile,TYPE_NORMAL,"All Full Body Animations running");
                            break;
                        case 3:
                            ht_fprintf(outFile,TYPE_NORMAL,"All Animations running");
                            break;
                        default:
                            ht_fprintf(outFile,TYPE_NORMAL,"Carry Pose");
                    }
                    ht_fprintf(outFile,TYPE_NORMAL," affecting ");
                    data2(b[x+3],w2);       // target object
                    if (c1 & 1)
                        ht_fprintf(outFile,TYPE_NORMAL,", Flipped");
                    if (c1 & 2)
                        ht_fprintf(outFile,TYPE_NORMAL,", blended Out");
                    if (c1 & 0x20)
                        ht_fprintf(outFile,TYPE_NORMAL,", short blended Out");
                    if (c1 & 8)
                        ht_fprintf(outFile,TYPE_NORMAL,", Flip Flag in Temp 3");
                    if (c1 & 0x40)
                        ht_fprintf(outFile,TYPE_NORMAL,", Stopping normal AND flipped anims");
                    ht_fprintf(outFile,TYPE_NORMAL,", Priority ");
                    switch (b[x+8]) {
                        case 0:
                            ht_fprintf(outFile,TYPE_NORMAL,"Any");
                            break;
                        case 1:
                            ht_fprintf(outFile,TYPE_NORMAL,"Low");
                            break;
                        case 2:
                            ht_fprintf(outFile,TYPE_NORMAL,"Medium");
                            break;
                        default:
                            ht_fprintf(outFile,TYPE_NORMAL,"High");
                    }
                    break;
                case 0x6D:  // Change Material (false = error)
                    w1 = *(UINT16 *) (&b[x+6]);
                    w2 = *(UINT16 *) (&b[x+9]);
                    w3 = *(UINT16 *) (&b[x+3]);
                    w4 = *(UINT16 *) (&b[x]);
                    c1 = b[x+2];
                    if (b[x+13] & 2) {
                        ht_fprintf(outFile,TYPE_NORMAL,"using snap shot generated material");
                        if (c1 & 1) {
                            ht_fprintf(outFile,TYPE_NORMAL,", mesh found in obj in");
                            data2(b[x+8],w2);
                            if (c1 & 0x20)
                                ht_fprintf(outFile,TYPE_NORMAL,", using mesh group index from temp 1");
                            else
                                ht_fprintf(outFile,TYPE_NORMAL,", using mesh group index %d", w3);
                        } else if (c1 & 0x20)
                            ht_fprintf(outFile,TYPE_NORMAL,", using mesh group index from temp 1");
                        else if (b[x+4] & 0x40) // w3 < 0
                            ht_fprintf(outFile,TYPE_NORMAL,", over all model");
                        else {
                            ht_fprintf(outFile,TYPE_NORMAL,", On Mesh Group: ");
                            if (c1 & 0x40)
                                readString2(GROUP_GLOBAL, 0x87, w3);
                            else if (c1 & 0x80)
                                if (readString2(gGlobGroup, 0x87, w3) == 0)
                                    ht_fprintf(outFile,TYPE_NORMAL,"[SemiGlobal STR# 0x87:0x%X]", w3);
                            else if (readString2(gGroup, 0x87, w3) == 0)
                                ht_fprintf(outFile,TYPE_NORMAL,"[Private STR# 0x87:0x%X]", w3);
                        }
                    } else if (c1 & 8) {
                        ht_fprintf(outFile,TYPE_NORMAL,"found in obj in ");
                        data2(b[x+8],w2);
                        if (c1 & 0x10)
                            if (b[x+13] & 1)
                                ht_fprintf(outFile,TYPE_NORMAL,", using Moving Texture Name index from temp 0");
                            else
                                ht_fprintf(outFile,TYPE_NORMAL,", using material index from temp 0");
                        else
                            if (b[x+13] & 1)
                                ht_fprintf(outFile,TYPE_NORMAL,", using Moving Texture Name index %d", w4);
                            else
                                ht_fprintf(outFile,TYPE_NORMAL,", using material index %d", w4);
                    } else {
                        ht_fprintf(outFile,TYPE_NORMAL,"found in me, to: ");
                        if (c1 & 0x10)
                            if (b[x+13] & 1)
                                ht_fprintf(outFile,TYPE_NORMAL,", using Moving Texture Name index from temp 0");
                            else
                                ht_fprintf(outFile,TYPE_NORMAL,", using material index from temp 0");
                        else
                            if (c1 & 2)
                                readString2(GROUP_GLOBAL, 0x88, w4);
                            else if (c1 & 4)
                                if (readString2(gGlobGroup, 0x88, w4) == 0)
                                    ht_fprintf(outFile,TYPE_NORMAL,"[SemiGlobal STR# 0x88:0x%X]", w4);
                            else if (readString2(gGroup, 0x88, w4) == 0)
                                ht_fprintf(outFile,TYPE_NORMAL,"[Private STR# 0x88:0x%X]", w4);
                    }
                    ht_fprintf(outFile,TYPE_NORMAL,", affecting ID in ");
                    data2(b[x+5],w1);       // target object
                    break;
                case 0x6E:  // Look At
                    w1 = *(UINT16 *) (&b[x+2]);
                    w2 = *(UINT16 *) (&b[x+9]);
                    c1 = b[x];
                    if ((b[x] & 1) == 0) {
                        if (w2 == 0)
                            ht_fprintf(outFile,TYPE_NORMAL,"No Event Tree");
                        else {
                            switch (b[x+11]) {
                                case 0:
                                    ht_fprintf(outFile,TYPE_NORMAL,"Private Event Tree: ");
                                    break;
                                case 1:
                                    ht_fprintf(outFile,TYPE_NORMAL,"SemiGlobal Event Tree: ");
                                    break;
                                default:
                                    ht_fprintf(outFile,TYPE_NORMAL,"Global Event Tree: ");
                                    break;
                            }
                            ht_fprintf(outFile,TYPE_NORMAL,"0x%X (",w2);
                            readFn2(w2);
                            ht_fprintf(outFile,TYPE_NORMAL,")");
                        }
                        if ((b[x] & 0x80) == 0) {
                            ht_fprintf(outFile,TYPE_NORMAL,", Targeting ID in ");
                            data2(b[x+1],w1);
                        } else
                            ht_fprintf(outFile,TYPE_NORMAL,", Targeting Camera ");
                        ht_fprintf(outFile,TYPE_NORMAL," using ");
                        switch (b[x+14]) {
                            case 0:
                                ht_fprintf(outFile,TYPE_NORMAL,"default 3/4 height");
                                break;
                            case 1:
                                ht_fprintf(outFile,TYPE_NORMAL,"targeting slot"," number %d",b[x+8]);
                                break;
                            case 2:
                                ht_fprintf(outFile,TYPE_NORMAL,"routing slot"," number %d",b[x+8]);
                                break;
                            default:
                                ht_fprintf(outFile,TYPE_NORMAL," containment slot"," number %d",b[x+8]);
                                break;
                        }
                        if (c1 & 4)
                            ht_fprintf(outFile,TYPE_NORMAL,", including Spine");
                        if (c1 & 0x10)
                            ht_fprintf(outFile,TYPE_NORMAL,", using duration in temp 0");
                        if (c1 & 2)
                            ht_fprintf(outFile,TYPE_NORMAL,", no early exit");
                    } else
                        ht_fprintf(outFile,TYPE_NORMAL,"STOP");
                    if (c1 & 8)
                        ht_fprintf(outFile,TYPE_NORMAL,", using turnTowardsSpeed in temp 1");
                    else
                        ht_fprintf(outFile,TYPE_NORMAL,", turn towards speed of %d  degrees per second",2*b[x+4]);
                    if (b[x+15] & 2)
                        ht_fprintf(outFile,TYPE_NORMAL,", using turnAwaySpeed in temp 1");
                    else if (b[x+15] & 1)
                        ht_fprintf(outFile,TYPE_NORMAL,", using turnAwaySpeed in temp 2");
                    else
                        ht_fprintf(outFile,TYPE_NORMAL,", turn away speed of %d  degrees per second",2*b[x+5]);
                    if (c1 & 0x20)
                        ht_fprintf(outFile,TYPE_NORMAL,", ignoring Room");
                    if (c1 & 0x40)
                        ht_fprintf(outFile,TYPE_NORMAL,", ignoring Frustrum");
                    if (b[x+15] & 4)
                        ht_fprintf(outFile,TYPE_NORMAL,", Not Hurryable");
                    break;
                case 0x6F:  // Change Light (false = error)
                    w1 = *(UINT16 *) (&b[x+3]);
                    w2 = *(UINT16 *) (&b[x+5]);
                    ht_fprintf(outFile,TYPE_NORMAL,"on object in ");
                    data2(b[x+2],w1);       // target object
                    if (b[x+8] != 0xFF) {
                        ht_fprintf(outFile,TYPE_NORMAL,", Targeting light ");
                        if (readString2(gGroup, 0x8E, b[x+8]) == 0)
                            ht_fprintf(outFile,TYPE_NORMAL,"[STR# 0x8E:0x%X]", b[x+8]);
                    } else
                        ht_fprintf(outFile,TYPE_NORMAL,", Targeting all lights on Object");
                    if (b[x+1] & 1)
                        ht_fprintf(outFile,TYPE_NORMAL,", Fade duration in Temp 1,");
                    else
                        ht_fprintf(outFile,TYPE_NORMAL,", Fading over a duration of %d ticks,", w2);
                    if (b[x+1] & 2)
                        ht_fprintf(outFile,TYPE_NORMAL," Intensity in Temp 0");
                    else
                        ht_fprintf(outFile,TYPE_NORMAL," Intensity of %d percent", b[x+7]);
                    break;
                case 0x70:  // Effect Stop/Start (false = error)
                    w1 = *(UINT16 *) (&b[x+2]);
                    w2 = *(UINT16 *) (&b[x+13]);
                    switch (b[x]) {
                        case 0:
                            ht_fprintf(outFile,TYPE_NORMAL,"Soft Start Effect on object in ");
                            break;
                        case 1:
                            ht_fprintf(outFile,TYPE_NORMAL,"Hard start effect on object in ");
                            break;
                        case 2:
                            ht_fprintf(outFile,TYPE_NORMAL,"Soft stop effect on object in ");
                            break;
                        case 3:
                            ht_fprintf(outFile,TYPE_NORMAL,"Hard stop effect on object in ");
                            break;
                        case 4:
                            ht_fprintf(outFile,TYPE_NORMAL,"Soft stop all effects on object in ");
                            break;
                        case 5:
                            ht_fprintf(outFile,TYPE_NORMAL,"Hard stop all effects on object in ");
                            break;
                        case 6:
                            ht_fprintf(outFile,TYPE_NORMAL,"Fire and Forget Effect on object in ");
                            break;
                        case 7:
                            ht_fprintf(outFile,TYPE_NORMAL,"Interrogate Bone for effects on object in ");
                            break;
                        case 8:
                            ht_fprintf(outFile,TYPE_NORMAL,"Hard stop all effects and clear Queue on object in ");
                            break;
                        case 9:
                            ht_fprintf(outFile,TYPE_NORMAL,"Hard stop ALL effects on object in ");
                            break;
                        case 0xA:
                            ht_fprintf(outFile,TYPE_NORMAL,"Set State 1 for all effects on object in ");
                            break;
                        case 0xB:
                            ht_fprintf(outFile,TYPE_NORMAL,"Set State 2 for all effects on object in ");
                            break;
                        case 0xC:
                            ht_fprintf(outFile,TYPE_NORMAL,"Set State 3 for all effects on object in ");
                            break;
                        case 0xD:
                            ht_fprintf(outFile,TYPE_NORMAL,"Set State 4 for all effects on object in ");
                            break;
                        case 0xE:
                            ht_fprintf(outFile,TYPE_NORMAL,"Soft stop ALL effects on object in ");
                            break;
                    }
                    data2(b[x+1],w1);       // target object
                    if (b[x] == 4 || b[x] == 5) {
                        if (b[x+10] & 0x40)
                            ht_fprintf(outFile,TYPE_NORMAL,", Passing in effect ID in temp 1");
                    } else if (b[x] < 7 || b[x] == 0xE)
                        if (b[x+4] != 0xFF) {
                            ht_fprintf(outFile,TYPE_NORMAL,", ");
                            if (b[x+10] & 1)
                                readString2(GROUP_GLOBAL, 0x8F, b[x+4]); // !!!
                            else if (b[x+10] & 2) {
                                if (readString2(gGlobGroup, 0x8F, b[x+4]) == 0)
                                    ht_fprintf(outFile,TYPE_NORMAL,"[SemiGlobal STR# 0x8F:0x%X]", b[x+4]);
                            }
                            else if (readString2(gGroup, 0x8F, b[x+4]) == 0)
                                ht_fprintf(outFile,TYPE_NORMAL,"[Private STR# 0x8F:0x%X]", b[x+4]);
                        } else
                            ht_fprintf(outFile,TYPE_NORMAL,", Affecting default effect");
                    if (b[x] != 9) {
                        ht_fprintf(outFile,TYPE_NORMAL,", of Slot Type ");
                        switch (b[x+9]) {
                            case 0:
                                ht_fprintf(outFile,TYPE_NORMAL,"Target");
                                break;
                            case 1:
                                ht_fprintf(outFile,TYPE_NORMAL,"Routing");
                                break;
                            default:
                                ht_fprintf(outFile,TYPE_NORMAL,"Containment");
                                break;
                        }
                        ht_fprintf(outFile,TYPE_NORMAL,", using slot number %d", b[x+6]);
                    }
                    if (b[x+11] & 0x10)
                        ht_fprintf(outFile,TYPE_NORMAL,", getting icon value from Temp6");
                    else if (b[x+11] & 4) {
                        ht_fprintf(outFile,TYPE_NORMAL,", putting in Icon with GUID in temp4/5 ");
                        if (b[x+11] & 8)
                            ht_fprintf(outFile,TYPE_NORMAL," getting model name index from Temp6");
                        else
                            ht_fprintf(outFile,TYPE_NORMAL," using default object model");
                    } else if (b[x+10] & 0x10) {
                        ht_fprintf(outFile,TYPE_NORMAL,", putting in Icon from neighbor ID in ");
                        data2(b[x+12],w2);
                    } else if (b[x+10] & 0x20) {
                        ht_fprintf(outFile,TYPE_NORMAL,", putting in Conversation Icon index found in ");
                        data2(b[x+12],w2);
                        ht_fprintf(outFile,TYPE_NORMAL," using sheet ");
                        if (readString2(gGroup, 0x95, b[x+15]) == 0) // ??
                            ht_fprintf(outFile,TYPE_NORMAL,"[STR# 0x95:0x%X]", b[x+15]);
                    } else if (b[x+10] & 4) {
                        ht_fprintf(outFile,TYPE_NORMAL,", putting in Icon from object in ");
                        data2(b[x+12],w2);
                        if (b[x+11] & 8)
                            ht_fprintf(outFile,TYPE_NORMAL," getting model name index from Temp6");
                        else
                            ht_fprintf(outFile,TYPE_NORMAL," using default object model");
                    }
                    if (b[x+10] & 0x80)
                        ht_fprintf(outFile,TYPE_NORMAL,", putting effect in priority Queue");
                    break;
                case 0x71:  // Snap Into
                    w1 = *(UINT16 *) (&b[x+1]);
                    w2 = *(UINT16 *) (&b[x+4]);
                    ht_fprintf(outFile,TYPE_NORMAL,"Snap Object in ");
                    data2(b[x],w1);
                    ht_fprintf(outFile,TYPE_NORMAL," into Object in ");
                    data2(b[x+3],w2);
                    if (b[x+9] & 1)
                        ht_fprintf(outFile,TYPE_NORMAL,", using slot # in temp 0");
                    else
                        ht_fprintf(outFile,TYPE_NORMAL,", using slot number %d", b[x+6]);
                    if (b[x+9] & 2)
                        ht_fprintf(outFile,TYPE_NORMAL,", TEST ONLY");
                    if (b[x+9] & 4)
                        ht_fprintf(outFile,TYPE_NORMAL,", Reset root bones");
                    break;
                case 0x72:  // Assign Locomotion Animations (false = error)
                    w1 = *(UINT16 *) (&b[x]);
                    switch (w1) {
                        case 0:
                            ht_fprintf(outFile,TYPE_NORMAL,"Pop all entries from Stack.");
                            break;
                        case 1:
                            ht_fprintf(outFile,TYPE_NORMAL,"Pop last entry from Stack.");
                            break;
                        default:
                            ht_fprintf(outFile,TYPE_NORMAL,"Push group #%d onto Stack", w1-2);
                            ht_fprintf(outFile,TYPE_NORMAL,", getting group from ");
                            if (b[x+2] & 4)
                                ht_fprintf(outFile,TYPE_NORMAL,"Global.");
                            else if (b[x+2] & 2)
                                ht_fprintf(outFile,TYPE_NORMAL,"SemiGlobal.");
                            else
                                ht_fprintf(outFile,TYPE_NORMAL,"Private.");
                    }
                    break;
                case 0x73:  // Debug (false = error)
                    switch (b[x+13]) {
                        case 0:
                            if (b[x+14] & 4)
                                readString2(GROUP_GLOBAL, 0x132, b[x+12]);
                            else if (b[x+14] & 2) {
                                if (readString2(gGlobGroup, 0x132, b[x+12]) == 0)
                                    ht_fprintf(outFile,TYPE_NORMAL,"[SemiGlobal STR# 0x132:0x%X]", b[x+12]);
                            }
                            else if (readString2(gGroup, 0x132, b[x+12]) == 0)
                                ht_fprintf(outFile,TYPE_NORMAL,"[Private STR# 0x132:0x%X]", b[x+12]);
                            w1 = *(UINT16 *) (&b[x+1]);
                            w2 = *(UINT16 *) (&b[x+4]);
                            w3 = *(UINT16 *) (&b[x+7]);
                            w4 = *(UINT16 *) (&b[x+10]);
                            ht_fprintf(outFile,TYPE_NORMAL,", param 0: ");
                            data2(b[x], w1);
                            ht_fprintf(outFile,TYPE_NORMAL,", param 1: ");
                            data2(b[x+3], w2);
                            ht_fprintf(outFile,TYPE_NORMAL,", param 2: ");
                            data2(b[x+6], w3);
                            ht_fprintf(outFile,TYPE_NORMAL,", param 3: ");
                            data2(b[x+9], w4);
                            break;
                        case 1:
                            ht_fprintf(outFile,TYPE_NORMAL,"Toggle Window Open/Close");
                            break;
                        case 2:
                            ht_fprintf(outFile,TYPE_NORMAL,"Open Animation Ticker");
                            break;
                        case 3:
                            ht_fprintf(outFile,TYPE_NORMAL,"Show Slots");
                            break;
                        case 4:
                            ht_fprintf(outFile,TYPE_NORMAL,"Show Bones");
                            break;
                        case 5:
                            ht_fprintf(outFile,TYPE_NORMAL,"Toggle Anim Info to Debug Window");
                            break;
                        case 6:
                            ht_fprintf(outFile,TYPE_NORMAL,"Perform Cheat ");
                            if (b[x+14] & 4)
                                readString2(GROUP_GLOBAL, 0x132, b[x+12]);
                            else if (b[x+14] & 2)
                                if (readString2(gGlobGroup, 0x132, b[x+12]) == 0)
                                    ht_fprintf(outFile,TYPE_NORMAL,"[SemiGlobal STR# 0x132:0x%X]", b[x+12]);
                            else if (readString2(gGroup, 0x132, b[x+12]) == 0)
                                ht_fprintf(outFile,TYPE_NORMAL,"[Private STR# 0x132:0x%X]", b[x+12]);
                            break;
                        case 7:
                            ht_fprintf(outFile,TYPE_NORMAL,"Dump Happy Log");
                            break;
                    }
                    break;
                case 0x74:  // Reach/Put
                    w1 = *(UINT16 *) (&b[x+4]);
                    w2 = *(UINT16 *) (&b[x+13]);
                    w3 = *(UINT16 *) (&b[x+11]);
                    switch (b[x+10]) {
                        case 0:
                            ht_fprintf(outFile,TYPE_NORMAL,"Pick up Object in ");
                            data2(b[x+3],w1);
                            break;
                        case 1:
                            ht_fprintf(outFile,TYPE_NORMAL,"Drop object onto Floor");
                            break;
                        default:
                            ht_fprintf(outFile,TYPE_NORMAL,"Drop onto Object in ");
                            data2(b[x+3],w1);
                    }
                    if (b[x+10]!= 1 && b[x+10]!= 2)
                        if (b[x+9] & 1)
                            ht_fprintf(outFile,TYPE_NORMAL,", using slot # in temp 0");
                        else
                            ht_fprintf(outFile,TYPE_NORMAL,", using slot number %d", b[x+6]);
                    ht_fprintf(outFile,TYPE_NORMAL,", object anim: ");
                    if (w2 != 0xFFFF) {
                        if (readString2(gGroup, 0x86, w2) == 0) // ?
                            ht_fprintf(outFile,TYPE_NORMAL,"[STR# 0x86:0x%X]", w2);
                    } else
                        ht_fprintf(outFile,TYPE_NORMAL,"none");
                    if (b[x+9] & 4)
                        ht_fprintf(outFile,TYPE_NORMAL,", using Sim age as filter on object Anim");
                    ht_fprintf(outFile,TYPE_NORMAL,", grasp anim: ");
                    if (w3 != 0xFFFF) {
                        if (readString2(gGroup, 0x81, w3) == 0) // ?
                            ht_fprintf(outFile,TYPE_NORMAL,"[STR# 0x81:0x%X]", w3);
                    } else
                        ht_fprintf(outFile,TYPE_NORMAL,"none");
                    if (b[x+9] & 2)
                        ht_fprintf(outFile,TYPE_NORMAL,", expecting handedness in temp 3");
                    break;
                case 0x75:  // Age (false = error)
                    if (b[x+1] & 1)
                        ht_fprintf(outFile,TYPE_NORMAL,"Expect Age in Temp 0");
                    else {
                        ht_fprintf(outFile,TYPE_NORMAL,"Set Age to ");
                        switch (b[x]) {
                            case 1:
                                ht_fprintf(outFile,TYPE_NORMAL,"Child");
                                break;
                            case 2:
                                ht_fprintf(outFile,TYPE_NORMAL,"Toddler");
                                break;
                            case 3:
                                ht_fprintf(outFile,TYPE_NORMAL,"Teen");
                                break;
                            case 4:
                                ht_fprintf(outFile,TYPE_NORMAL,"Elder");
                                break;
                            case 7:
                                ht_fprintf(outFile,TYPE_NORMAL,"Baby");
                                break;
                            case 9:
                                ht_fprintf(outFile,TYPE_NORMAL,"Young Adult");
                                break;
                            default:
                                ht_fprintf(outFile,TYPE_NORMAL,"Adult");
                        }
                    }
                    break;
                case 0x76:  // Array Operation
                    w1 = *(UINT16 *) (&b[x+6]);
                    w2 = *(UINT16 *) (&b[x+9]);
                    w3 = *(UINT16 *) (&b[x+3]);
                    if (b[x+2] == 0)
                        ht_fprintf(outFile,TYPE_NORMAL,"My Object Array: ");
                    else
                        ht_fprintf(outFile,TYPE_NORMAL,"Stack Object's Object Array: ");
                    if (readString2(gGroup, 0x118, w3) == 0)
                                ht_fprintf(outFile,TYPE_NORMAL,"[STR# 0x118:0x%X]", w3);
                    ht_fprintf(outFile,TYPE_NORMAL,". Operation: ");
                    switch (b[x+1]) {
                        case 0:
                            ht_fprintf(outFile,TYPE_NORMAL,"Clear contents of array.");
                            break;
                        case 1:
                            ht_fprintf(outFile,TYPE_NORMAL,"Return size of array into ");
                            data2(b[x+5],w1);
                            ht_fprintf(outFile,TYPE_NORMAL,".");
                            break;
                        case 2:
                            ht_fprintf(outFile,TYPE_NORMAL,"Resize array to size from ");
                            data2(b[x+5],w1);
                            ht_fprintf(outFile,TYPE_NORMAL,".");
                            break;
                        case 3:
                            ht_fprintf(outFile,TYPE_NORMAL,"Init all elements of the array with value from ");
                            data2(b[x+5],w1);
                            ht_fprintf(outFile,TYPE_NORMAL,".");
                            break;
                        case 4:
                            ht_fprintf(outFile,TYPE_NORMAL,"Insert element to array from ");
                            data2(b[x+5],w1);
                            ht_fprintf(outFile,TYPE_NORMAL," to the front of the array.");
                            break;
                        case 5:
                            ht_fprintf(outFile,TYPE_NORMAL,"Insert element to array from ");
                            data2(b[x+5],w1);
                            ht_fprintf(outFile,TYPE_NORMAL," to the back of the array.");
                            break;
                        case 6:
                            ht_fprintf(outFile,TYPE_NORMAL,"Insert element to array from ");
                            data2(b[x+5],w1);
                            ht_fprintf(outFile,TYPE_NORMAL," to the position indicated by iterator in ");
                            data2(b[x+8],w2);
                            ht_fprintf(outFile,TYPE_NORMAL,".");
                            break;
                        case 7:
                            ht_fprintf(outFile,TYPE_NORMAL,"Remove front element from array.");
                            break;
                        case 8:
                            ht_fprintf(outFile,TYPE_NORMAL,"Remove back element from array.");
                            break;
                        case 9:
                            ht_fprintf(outFile,TYPE_NORMAL,"Remove element from array at position given by iterator in ");
                            data2(b[x+8],w2);
                            ht_fprintf(outFile,TYPE_NORMAL,".");
                            break;
                        case 0xA:
                            ht_fprintf(outFile,TYPE_NORMAL,"Advance iterator in ");
                            data2(b[x+8],w2);
                            ht_fprintf(outFile,TYPE_NORMAL," to next occurance of element from ");
                            data2(b[x+5],w1);
                            ht_fprintf(outFile,TYPE_NORMAL,".");
                            break;
                        case 0xB:
                            ht_fprintf(outFile,TYPE_NORMAL,"Swap elements of array at iterator ");
                            data2(b[x+5],w1);
                            ht_fprintf(outFile,TYPE_NORMAL," and iterator ");
                            data2(b[x+8],w2);
                            ht_fprintf(outFile,TYPE_NORMAL,".");
                            break;
                        case 0xC:
                            ht_fprintf(outFile,TYPE_NORMAL,"Sort array into highest to lowest order.");
                            break;
                        case 0xD:
                            ht_fprintf(outFile,TYPE_NORMAL,"Sort array into highest to lowest order.");
                            break;
                        default:
                            ht_fprintf(outFile,TYPE_NORMAL,"Alex Fennell's magic array operation. If you see this string....be afraid.");
                    }
                    break;
                case 0x77:  // Message
                    w1 = *(UINT16 *) (&b[x+1]);
                    w2 = *(UINT16 *) (&b[x+6]);
                    w3 = *(UINT16 *) (&b[x+10]);
                    w4 = *(UINT16 *) (&b[x+13]);
                    ht_fprintf(outFile,TYPE_NORMAL,"Sending Message ID inside ");
                    data2(b[x+15],w1);
                    ht_fprintf(outFile,TYPE_NORMAL,", priority %d",b[x+8]);
                    if (b[x+4] & 4) {
                        ht_fprintf(outFile,TYPE_NORMAL," Targetting specific target in ");
                        data2(b[x+5],w2);
                    } else {
                        ht_fprintf(outFile,TYPE_NORMAL," Targetting ");
                        switch (b[x+3]) {
                            case 0:
                                ht_fprintf(outFile,TYPE_NORMAL,"Selectable Sims");
                                break;
                            case 1:
                                ht_fprintf(outFile,TYPE_NORMAL,"Selectable Sims + Neighbors");
                                break;
                            case 2:
                                ht_fprintf(outFile,TYPE_NORMAL,"Selectable Sims + NPCS");
                                break;
                            case 3:
                                ht_fprintf(outFile,TYPE_NORMAL,"Neighbors Only");
                                break;
                            case 4:
                                ht_fprintf(outFile,TYPE_NORMAL,"NPCS Only");
                                break;
                            case 5:
                                ht_fprintf(outFile,TYPE_NORMAL,"All Sims");
                                break;
                            case 6:
                                ht_fprintf(outFile,TYPE_NORMAL,"Objects");
                                break;
                            case 7:
                                ht_fprintf(outFile,TYPE_NORMAL,"Everything");
                                break;
                        }
                        ht_fprintf(outFile,TYPE_NORMAL," using filter ");
                        switch (b[x]) {
                            case 0:
                                ht_fprintf(outFile,TYPE_NORMAL,"In Same Room Only");
                                if (b[x+4] & 1) {
                                    ht_fprintf(outFile,TYPE_NORMAL," targeting room in ");
                                    data2(b[x+5],w2);
                                }
                                break;
                            case 1:
                                ht_fprintf(outFile,TYPE_NORMAL,"On Same Level Only");
                                break;
                            case 2:
                                ht_fprintf(outFile,TYPE_NORMAL,"On Lot");
                                break;
                            case 3:
                                ht_fprintf(outFile,TYPE_NORMAL,"Inside building Only");
                                break;
                            case 4:
                                ht_fprintf(outFile,TYPE_NORMAL,"OutSide Building Only");
                                break;
                        }
                        ht_fprintf(outFile,TYPE_NORMAL,", getting user data from ");
                        data2(b[x+9],w3);
                        ht_fprintf(outFile,TYPE_NORMAL,", and ");
                        data2(b[x+12],w4);
                    }
                    break;
                case 0x78:  // RayTrace
                    w1 = *(UINT16 *) (&b[x+2]);
                    w2 = *(UINT16 *) (&b[x+9]);
                    ht_fprintf(outFile,TYPE_NORMAL,"From Object in ");
                    data2(b[x+1],w1);
                    ht_fprintf(outFile,TYPE_NORMAL," using ");
                    switch (b[x+4]) {
                        case 0:
                            ht_fprintf(outFile,TYPE_NORMAL,"point at 3/4 height of object.");
                            break;
                        case 1:
                            ht_fprintf(outFile,TYPE_NORMAL,"targeting slot");
                            break;
                        case 2:
                            ht_fprintf(outFile,TYPE_NORMAL,"routing slot");
                            break;
                        default:
                            ht_fprintf(outFile,TYPE_NORMAL,"containment slot");
                    }
                    if (b[x+4] != 0)
                        ht_fprintf(outFile,TYPE_NORMAL,", slot number %d into the world. Object hit stored in Temp 0.", b[x+5]);
                    else {
                        ht_fprintf(outFile,TYPE_NORMAL," To Object in ");
                        data2(b[x+8],w2);
                        ht_fprintf(outFile,TYPE_NORMAL," using ");
                        switch (b[x+11]) {
                            case 0:
                                ht_fprintf(outFile,TYPE_NORMAL,"point at 3/4 height of object.");
                                break;
                            case 1:
                                ht_fprintf(outFile,TYPE_NORMAL,"targeting slot");
                                break;
                            case 2:
                                ht_fprintf(outFile,TYPE_NORMAL,"routing slot");
                                break;
                            default:
                                ht_fprintf(outFile,TYPE_NORMAL,"containment slot");
                        }
                        if (b[x+11] != 0)
                            ht_fprintf(outFile,TYPE_NORMAL,", slot number %d.", b[x+12]);
                    }
                    if (b[x+15] & 1)
                        ht_fprintf(outFile,TYPE_NORMAL," Windows Ignored.");
                    break;
                case 0x79:  // Change Outfit (false = error)
                    c1 = b[x];
                    w1 = *(UINT16 *) (&b[x+10]);
                    w2 = *(UINT16 *) (&b[x+2]);
                    d1 = *(UINT32 *) (&b[x+4]);
                    if (c1 & 0x10) {
                        ht_fprintf(outFile,TYPE_NORMAL,"Rebuild current outfit on sim in ");
                        data2(b[x+9],w1);
                    } else {
                        ht_fprintf(outFile,TYPE_NORMAL,"Change Outfit on sim in ");
                        data2(b[x+9],w1);
                        if (c1 & 1)
                            ht_fprintf(outFile,TYPE_NORMAL," using Stack Object");
                        else if (c1 & 2) {
                            ht_fprintf(outFile,TYPE_NORMAL," using GUID of 0x%08X", d1);
                            readGUID(d1);
                        } else if (c1 & 0x40)
                            ht_fprintf(outFile,TYPE_NORMAL,", using GUID in Temp 0/1");
                        else
                            ht_fprintf(outFile,TYPE_NORMAL," using that sims outfits");
                        ht_fprintf(outFile,TYPE_NORMAL," as source, using outfit ");
                        if (c1 & 4) {
                            ht_fprintf(outFile,TYPE_NORMAL,"index from ");
                            data2(b[x+1],w2);
                        } else {
                            CHECK_RANGE("Person outfits", gStringFA, b[x+8]);
                            ht_fprintf(outFile,TYPE_NORMAL,"%s", gStringFA[b[x+8]]);
                        }
                        if (c1 & 0x20)
                            ht_fprintf(outFile,TYPE_NORMAL,", clearing GUID pointers in person data fields");
                        if (c1 & 8)
                            ht_fprintf(outFile,TYPE_NORMAL,", writing changes to the .iff");
                    }
                    break;
                case 0x7A:  // On Timer (false = error)
                    w1 = *(UINT16 *) (&b[x+7]);
                    w2 = *(UINT16 *) (&b[x+10]);
                    w3 = *(UINT16 *) (&b[x+13]);
                    w4 = *(UINT16 *) (&b[x]);
                    w5 = *(UINT16 *) (&b[x+3]);
                    switch (b[x+15]) {
                        case 0:
                            ht_fprintf(outFile,TYPE_NORMAL,"Start Timer");
                            break;
                        case 1:
                            ht_fprintf(outFile,TYPE_NORMAL,"Modify Timer");
                            break;
                        case 2:
                            ht_fprintf(outFile,TYPE_NORMAL,"Delete Timer");
                            break;
                    }
                    if (b[x+15] != 2) {
                        if (b[x+5] & 8)
                            ht_fprintf(outFile,TYPE_NORMAL,", Getting ticks till fire from temp 1");
                        else
                            ht_fprintf(outFile,TYPE_NORMAL,", Ticks till fire %d", w4);

                        if (w5 == 0)
                            ht_fprintf(outFile,TYPE_NORMAL," No Event Tree");
                        else {
                            switch (b[x+2]) {
                                case 0:
                                    ht_fprintf(outFile,TYPE_NORMAL," Using Private Event Tree: ");
                                    break;
                                case 1:
                                    ht_fprintf(outFile,TYPE_NORMAL," Using SemiGlobal Event Tree: ");
                                    break;
                                default:
                                    ht_fprintf(outFile,TYPE_NORMAL," Using Global Event Tree: ");
                                    break;
                            }
                            ht_fprintf(outFile,TYPE_NORMAL,"0x%X (",w5);
                            readFn2(w5);
                            ht_fprintf(outFile,TYPE_NORMAL,")");
                        }
                        if (b[x+5] & 2)
                            ht_fprintf(outFile,TYPE_NORMAL,", Looping");
                        if (b[x+5] & 1)
                            ht_fprintf(outFile,TYPE_NORMAL,", using current params as event tree params");
                        else {
                            ht_fprintf(outFile,TYPE_NORMAL,", passing in params found in  ");
                            data2(b[x+6],w1);
                            ht_fprintf(outFile,TYPE_NORMAL,", ");
                            data2(b[x+9],w2);
                            ht_fprintf(outFile,TYPE_NORMAL,", ");
                            data2(b[x+12],w3);
                        }
                        if (b[x+15] == 1)
                            if (b[x+5] & 4)
                                ht_fprintf(outFile,TYPE_NORMAL,", Reset Timer Ticks");
                    }
                    break;
                case 0x7B:  // Cinematic
                    c1 = b[x+5];
                    w1 = *(UINT16 *) (&b[x+7]);
                    w2 = *(UINT16 *) (&b[x]);
                    w3 = *(UINT16 *) (&b[x+10]);
                    if (c1 & 0x10) {
                        ht_fprintf(outFile,TYPE_NORMAL,"Using scene name index found in ");
                        data2(b[x+6],w1);
                    } else {
                        ht_fprintf(outFile,TYPE_NORMAL,"Using Scene ");
                        if (c1 & 0x20)
                            readString2(GROUP_GLOBAL, 0x97, w2);
                        else if (c1 & 0x40) {
                            if (readString2(gGlobGroup, 0x97, w2) == 0)
                                ht_fprintf(outFile,TYPE_NORMAL,"[SemiGlobal STR# 0x97:0x%X]", w2);
                        }
                        else if (readString2(gGroup, 0x97, w2) == 0)
                            ht_fprintf(outFile,TYPE_NORMAL,"[Private STR# 0x97:0x%X]", w2);
                    }
                    ht_fprintf(outFile,TYPE_NORMAL,", Using array found in objID in ");
                    data2(b[x+9],w3);
                    if (c1 & 1)
                        ht_fprintf(outFile,TYPE_NORMAL,", Flip Cinematic Anims Horizontally");
                    if (c1 & 2)
                        ht_fprintf(outFile,TYPE_NORMAL,", Pass in Flip Cinematic Anims Horizontally value in temp 0");
                    if (c1 & 4)
                        ht_fprintf(outFile,TYPE_NORMAL,", Start Animations Now");
                    if (c1 & 8)
                        ht_fprintf(outFile,TYPE_NORMAL,", Show Entire House");
                    break;
                case 0x7C:  // Want Satisfy
                    d1 = *(UINT32 *) (&b[x+3]);
                    ht_fprintf(outFile,TYPE_NORMAL,"GUID 0x%08X ", d1);

                    w2 = 0xFFFF;
                    w3 = GET_ASIZE(gWants);
                    for (w1 = 0; w1 < w3; w1++) if (d1 == gWants[w1].guid)  w2 = w1;
                    if (w2 != 0xFFFF) {
                        ht_fprintf(outFile,TYPE_NORMAL,"(%s) ", gWants[w2].name);

                        w3 = *(UINT16 *) (&b[x+8]);
                        w4 = *(UINT16 *) (&b[x+11]);
                        w5 = *(UINT16 *) (&b[x+1]);

                        switch (gWants[w2].type) {
                            case 0:        // None
                                ht_fprintf(outFile,TYPE_NORMAL,"with value ");
                                break;
                            case 1:        // Sim
                                ht_fprintf(outFile,TYPE_NORMAL,"with person in ");
                                break;
                            case 2:        // Guid
                            case 3:        // Category
                                ht_fprintf(outFile,TYPE_NORMAL,"with object in ");
                                break;
                            case 4:        // Skill
                                ht_fprintf(outFile,TYPE_NORMAL,"with skill ");
                                break;
                            case 5:        // Career
                                ht_fprintf(outFile,TYPE_NORMAL,"with career ");
                                break;
                        }

                        data2(b[x+7],w3);
                        if (gWants[w2].type == 4 || gWants[w2].type == 5) {
                            ht_fprintf(outFile,TYPE_NORMAL," and level ");
                            data2(b[x+10],w4);
                        }
                    }
                    break;

                case 0x7D:  // Influence (Uni)
                    w1 = *(UINT16 *) (&b[x+1]);
                    w2 = *(UINT16 *) (&b[x+6]);
                    ht_fprintf(outFile,TYPE_NORMAL,"Sim in ");
                    data2(b[x],w1);
                    ht_fprintf(outFile,TYPE_NORMAL,", output result to ");
                    if (b[x+5])
                        ht_fprintf(outFile,TYPE_NORMAL," stack object's object array: ");
                    else
                        ht_fprintf(outFile,TYPE_NORMAL," my object array: ");
                    if (readString2(gGroup, 0x118, w2) == 0)
                        ht_fprintf(outFile,TYPE_NORMAL,"[STR# 0x118:0x%X]", w2);
                    break;

                case 0x7E:  // Lua (NL)
                    w1 = *(UINT16 *) (&b[x]);   // STR#
                    w2 = *(UINT16 *) (&b[x+2]); // index + 1
                    w3 = *(UINT16 *) (&b[x+4]); // mystery value
                    if (w2 != 0) {
                        w2--;
                        if (readString2(gGroup, w1, w2) == 0)
                            ht_fprintf(outFile,TYPE_NORMAL,"[STR# 0x%X:0x%X]", w1, w2);
                        ht_fprintf(outFile,TYPE_NORMAL, ", %d", w3);
                    }
                    break;
                case 0x00:  // Sleep (false = error)
                    ht_fprintf(outFile,TYPE_NORMAL,"for ");
                    data2(9, b[x]);
                    ht_fprintf(outFile,TYPE_NORMAL," ticks");
                    break;
                case 0x01:  // Generic Sims Call
                    c1 = b[x];
                    CHECK_RANGE("Generic Sims Call", gStringDC, c1);
                    ht_fprintf(outFile,TYPE_FUNCTION,"%s", gStringDC[c1]);
                    switch (c1) {
                        case 0:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:neighborhood, Temp 1:evict, Temp 2:save lot, Temp 3:reset tutorial");
                            break;
                        case 4:
                            ht_fprintf(outFile,TYPE_NORMAL," Stack Obj:nID, Temp 0:familyID");
                            break;
                        case 5:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:familyID");
                            break;
                        case 6:
                            ht_fprintf(outFile,TYPE_NORMAL," Stack Obj:nID");
                            break;
                        case 0x0D:
                            ht_fprintf(outFile,TYPE_NORMAL," Stack Obj");
                            break;
                        case 0x11:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:lotID");
                            break;
                        case 0x12:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:speed");
                            break;
                        case 0x15:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:child nID, Temp 1:parent nID");
                            break;
                        case 0x16:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:new spouse nID, Temp 1:initial spouse nID");
                            break;
                        case 0x17:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:remove nID, Temp 1:relative nID");
                            break;
                        case 0x18:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:age");
                            break;
                        case 0x19:
                        case 0x28:
                        case 0x29:
                        case 0x2C:
                        case 0x31:
                        case 0x33:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0");
                            break;
                        case 0x1C:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:amount, Temp 2:multiplier");
                            break;
                        case 0x1D:
                        case 0x26:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:nID");
                            break;
                        case 0x1E:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:outfit");
                            break;
                        case 0x1F:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0,1:GUID");
                            break;
                        case 0x20:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:outfit, Temp 1:result value");
                            break;
                        case 0x22:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:source, Temp 1:destination, Temp 2:result value");
                            break;
                        case 0x23:
                        case 0x24:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:lotID, Temp 1:result value");
                            break;
                        case 0x2D:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:source, Stack Obj:destination");
                            break;
                        case 0x2E:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:tableID, Temp 1:index, Temp 3:fallback");
                            break;
                        case 0x30:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:nID, Temp 1:result value");
                            break;
                        case 0x32:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:take, Temp 1:target nID, Temp 2:percent, Temp 3,4:amount, Temp 5:from assets");
                            break;
                        case 0x34:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:direction, Temp 1:result wall obj");
                            break;
                        case 0x35:
                            ht_fprintf(outFile,TYPE_NORMAL," Temp 0:tableID, Temp 1:index, Temp 2:state, Temp 3:fallback");
                            break;
                    }
                    break;
                case 0x02:  // (Evaluate) Expression (non-comparison operators return false if error)
                    w1 = *(UINT16 *) (&b[x]);
                    w2 = *(UINT16 *) (&b[x+2]);
                    c1 = b[x+5];
                    c2 = b[x+6];
                    data2(c2, w1);            // target data
                    if (c1 == 0x14)           // abs(rhs)
                        ht_fprintf(outFile,TYPE_OPERATOR," := abs(");
                    else if (c1 == 0x15)      // Assign 32bit Value (both target and source are contiguous 32-bit)
                        ht_fprintf(outFile,TYPE_OPERATOR," := int32(");
                    else {
                        CHECK_RANGE("Operator", gString88, c1);
                        ht_fprintf(outFile,TYPE_OPERATOR," %s ",gString88[c1]);
                    }

                    if (b[x+7] == 7 && c1 > 7 && c1 < 11 && w2 > 0) {   // literal (non-0), flag operator
                        if ((c2 == 3 || c2 == 4) && (w1 == 5 || w1 == 8 ||
                                w1 == 0x22 || w1 == 0x28 || w1 == 0x2A ||
                                w1 == 0x2B || w1 == 0x3F || w1 == 0x45)) {
                            switch (w1) {
                                case 5:
                                    CHECK_RANGE("Wall adj. flags", gStringD0, w2 - 1);
                                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gStringD0[w2 - 1]);
                                    break;
                                case 8:
                                    CHECK_RANGE("Flags 1", gString8E, w2 - 1);
                                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gString8E[w2 - 1]);
                                    break;
                                case 0x22:
                                    CHECK_RANGE("Hidden Flags", gString200, w2 - 1);
                                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gString200[w2 - 1]);
                                    break;
                                case 0x28:
                                    CHECK_RANGE("Flags 2", gStringD6, w2 - 1);
                                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gStringD6[w2 - 1]);
                                    break;
                                case 0x2A:
                                    CHECK_RANGE("Placement flags", gStringCA, w2 - 1);
                                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gStringCA[w2 - 1]);
                                    break;
                                case 0x2B:
                                    CHECK_RANGE("Movement flags", gStringCB, w2 - 1);
                                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gStringCB[w2 - 1]);
                                    break;
                                case 0x3F:
                                    CHECK_RANGE("Exclusive placement flags", gStringFB, w2 - 1);
                                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gStringFB[w2 - 1]);
                                    break;
                                case 0x45:
                                    CHECK_RANGE("Wall cutout flags", gStringFD, w2 - 1);
                                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gStringFD[w2 - 1]);
                                    break;
                            }
                        } else if ((c2 == 0x12 || c2 == 0x13 || c2 == 0x20) &&
                        (w1 == 0x1E || w1 == 0x44 || w1 == 0x51 || w1 == 0x9E || w1 == 0x9F)) {
                            switch (w1) {
                                case 0x1E:
                                    CHECK_RANGE("Censorship flags", gStringB2, w2 - 1);
                                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gStringB2[w2 - 1]);
                                    break;
                                case 0x44:
                                    CHECK_RANGE("Ghost flags", gString201, w2 - 1);
                                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gString201[w2 - 1]);
                                    break;
                                case 0x51:
                                    CHECK_RANGE("Body flags", gString8F, w2 - 1);
                                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gString8F[w2 - 1]);
                                    break;
                                case 0x9E:
                                    CHECK_RANGE("Selection flags", gString202, w2 - 1);
                                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gString202[w2 - 1]);
                                    break;
                                case 0x9F:
                                    CHECK_RANGE("Person flags", gString204, w2 - 1);
                                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gString204[w2 - 1]);
                                    break;
                            }
                        } else if ((c2 == 0x15 || c2 == 0x26 || c2 == 0x33) &&
                        (w1 == 0x27 || w1 == 0x28)) {
                            switch (w1) {
                                case 0x27:
                                    CHECK_RANGE("Room sort flags", gStringCD, w2 - 1);
                                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gStringCD[w2 - 1]);
                                    break;
                                case 0x28:
                                    CHECK_RANGE("Function sort flags", gStringCE, w2 - 1);
                                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gStringCE[w2 - 1]);
                                    break;
                            }
                        } else {
                            data2(b[x+7], w2);  // unknown flag
                        }
                    } else {
                        data2(b[x+7], w2);      // not a literal flag

                        // flag, BCON 0x101:x (Standard Heights)
/*
                        if (c1 > 7 && c1 < 11 && b[x+7] == 0x1A && (w2 & 0xFF80) == 0x4080 && (w2 & 0x7F)) {
                            CHECK_RANGE("Allowed height flags", gStringAH, (w2 & 0x7F) - 1);
                            ht_fprintf(outFile,TYPE_NORMAL," %s", gStringAH[(w2 & 0x7F) - 1]);
                        }
*/
                        if (c1 == 0x14 || c1 == 0x15)    // abs(rhs) or Assign 32bit Value
                            ht_fprintf(outFile,TYPE_OPERATOR,")");
                    }
                    break;
                case 0x08:  // Random Number (false = error)
                    w1 = *(UINT16 *) (&b[x]);
                    w2 = *(UINT16 *) (&b[x+4]);
                    data2(b[x+2], w1);
                    ht_fprintf(outFile,TYPE_OPERATOR," := random from 0 to < ");
                    data2(b[x+6], w2);
                    break;
                case 0x1F:  // Set to Next (false = next not found)
                    w1 = *(UINT16 *) (&b[x+9]);
                    w2 = *(UINT16 *) (&b[x+11]);
                    if (b[x+5] != 0xA || b[x+7] != 0 && (b[x+4] & 0x80) != 0) {
                        data2(b[x+5], b[x+7]);
                        ht_fprintf(outFile,TYPE_NORMAL," := next ");
                    }
                    c1 = (b[x+4] & 0x7F);
                    CHECK_RANGE("Next object", gStringA4, c1);
                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gStringA4[c1]);
                    d1 = *(UINT32 *) (&b[x]);
                    if (c1 == 4 || c1 == 7) ht_fprintf(outFile,TYPE_NORMAL," GUID 0x%08X", d1);
                    readGUID(d1);
                    if (c1 == 9 || c1 == 0x22) data2(0x19, b[x+6]);   // local
//                    if (b[x+8] & 2) ht_fprintf(outFile,TYPE_NORMAL," where %s := %d", gString8D[w1], w2);
                    if (b[x+8] & 1) ht_fprintf(outFile,TYPE_NORMAL," [including disabled objects]");
                    break;
                case 0x1C:  // Run Tree by Name
                    c1 = b[x+4] - 1;
                    w1 = *(UINT16 *) (&b[x+7]);
                    w2 = *(UINT16 *) (&b[x+10]);
                    w3 = *(UINT16 *) (&b[x+13]);
                    if (b[x+2] & 1) {
                        ht_fprintf(outFile,TYPE_NORMAL,"%s", gNamedTreePrim[c1]);
                    } else if (b[x+2] & 2) {
                        if (readString2(gGlobGroup, 0x12F, c1) == 0)
                            ht_fprintf(outFile,TYPE_NORMAL,"[SemiGlobal STR# 0x12F:0x%X]", c1);
                    } else {
                        if (readString2(gGroup, 0x12F, c1) == 0)
                            ht_fprintf(outFile,TYPE_NORMAL,"[STR# 0x12F:0x%X]", c1);
                    }
                    if (b[x+2] & 0x20) {
                        ht_fprintf(outFile,TYPE_NORMAL,", Passing in params from the current tree");
                    } else if (b[x+2] & 0x10) {
                        ht_fprintf(outFile,TYPE_NORMAL,", Passing in params where param 0 = ");
                        data2(b[x+6], w1);
                        ht_fprintf(outFile,TYPE_NORMAL,", param 1 = ");
                        data2(b[x+9], w2);
                        ht_fprintf(outFile,TYPE_NORMAL,", param 2 = ");
                        data2(b[x+12], w3);
                    }
                    switch (b[x+5]) {
                        case 0:
                            ht_fprintf(outFile,TYPE_NORMAL,", run in my stack");
                            break;
                        case 1:
                            ht_fprintf(outFile,TYPE_NORMAL,", run in Stack Object's stack");
                            break;
                        case 2:
                            ht_fprintf(outFile,TYPE_NORMAL,", push onto my stack");
                            break;
                    }
                    break;
                case 0x0D:  // Push Interaction
                    w1 = *(UINT16 *) (&b[x+6]);
                    w2 = *(UINT16 *) (&b[x+9]);
                    w3 = *(UINT16 *) (&b[x+12]);
                    if (b[x+3] & 0x10) {
                        ht_fprintf(outFile,TYPE_NORMAL,"getting interaction # from ");
                        data2(b[x+5],w1);
                        ht_fprintf(outFile,TYPE_NORMAL," of ");
                    } else if (b[x+14] & 2)
                        ht_fprintf(outFile,TYPE_NORMAL,"getting interaction # from results of last FBA call ");
                    else
                        ht_fprintf(outFile,TYPE_NORMAL,"#0x%X of ", b[x]);
                    if (b[x+3] & 2)
                        data2(0x19, b[x+1]);    // local
                    else
                        data2(9, b[x+1]);       // param
                    ht_fprintf(outFile,TYPE_NORMAL," onto the stack object's queue, ");
                    CHECK_RANGE("Priorities", gStringE0, b[x+2]);
                    ht_fprintf(outFile,TYPE_NORMAL,"%s", gStringE0[b[x+2]]);
                    if (b[x+3] & 1) {
                        ht_fprintf(outFile,TYPE_NORMAL,", use icon from ");
                        data2(0x19, b[x+4]);
                    } else if (b[x+14] & 4)
                        ht_fprintf(outFile,TYPE_NORMAL," use Icon from selector GUID in temp4/5");
                    if (b[x+14] & 8)
                        ht_fprintf(outFile,TYPE_NORMAL,", Getting Icon Index from Temp6");
                    else
                        ht_fprintf(outFile,TYPE_NORMAL,", Icon Index is 0x%X", b[x+15]);
                    // if (b[x+3] & 4) ht_fprintf(outFile,TYPE_NORMAL,", continue as current");
                    if (b[x+3] & 8) ht_fprintf(outFile,TYPE_NORMAL,", use name");
                    if (b[x+3] & 0x20) ht_fprintf(outFile,TYPE_NORMAL,", Force run check tree");
                    if (b[x+14] & 1) ht_fprintf(outFile,TYPE_NORMAL,", Passing first 4 params in");
                    if (b[x+3] & 0x40) {
                        ht_fprintf(outFile,TYPE_NORMAL,", Linking to interaction with ID in ");
                        data2(b[x+8],w2);
                    }
                    if (b[x+3] & 0x80) {
                        ht_fprintf(outFile,TYPE_NORMAL,", Returing ID in ");
                        data2(b[x+11],w3);
                    }
                    break;
                case 0x10:  // Find Location For
                    w1 = *(UINT16 *) (&b[x+5]);
                    w2 = *(UINT16 *) (&b[x+8]);
                    if (b[x+2] & 8) {
                        data2(b[x+4], w1);
                        ht_fprintf(outFile,TYPE_NORMAL," relative to ");
                        data2(b[x+7], w2);
                        switch (b[x]) {
                            case 5:
                                ht_fprintf(outFile,TYPE_NORMAL,", routing slot in stack var %d",b[x+10]);
                                break;
                            case 6:
                                ht_fprintf(outFile,TYPE_NORMAL,", routing slot in local var %d",b[x+10]);
                                break;
                            case 7:
                                ht_fprintf(outFile,TYPE_NORMAL,", literal routing slot %d",b[x+10]);
                                break;
                            case 8:
                                ht_fprintf(outFile,TYPE_NORMAL,", global routing slot %d",b[x+10]);
                                break;
                        }
                        ht_fprintf(outFile,TYPE_NORMAL,", facing");
                        if ((b[x+3] & 1) == 0)
                            ht_fprintf(outFile,TYPE_NORMAL," N");
                        if ((b[x+3] & 2) == 0)
                            ht_fprintf(outFile,TYPE_NORMAL," NE");
                        if ((b[x+3] & 4) == 0)
                            ht_fprintf(outFile,TYPE_NORMAL," E");
                        if ((b[x+3] & 8) == 0)
                            ht_fprintf(outFile,TYPE_NORMAL," SE");
                        if ((b[x+3] & 0x10) == 0)
                            ht_fprintf(outFile,TYPE_NORMAL," S");
                        if ((b[x+3] & 0x20) == 0)
                            ht_fprintf(outFile,TYPE_NORMAL," SW");
                        if ((b[x+3] & 0x40) == 0)
                            ht_fprintf(outFile,TYPE_NORMAL," W");
                        if ((b[x+3] & 0x80) == 0)
                            ht_fprintf(outFile,TYPE_NORMAL," NW");
                    }
                    else if (b[x+2] & 1) {
                        ht_fprintf(outFile,TYPE_NORMAL,"Stack Object, start at ");       // (this seems to make a bit more sense)
                        data2(0x19,b[x+1]);       // local
                    }
                    if (b[x] != 0) {
                        if (b[x+2] & 8) ht_fprintf(outFile,TYPE_NORMAL,", ");
                        CHECK_RANGE("Find GLB", gStringEF, b[x]);
                        ht_fprintf(outFile,TYPE_NORMAL," %s", gStringEF[b[x]]);
                    }
//                    if (b[x+2] & 1) {
//                        if (b[x] != 0 || (b[x+2] & 8)) ht_fprintf(outFile,TYPE_NORMAL,", ");
//                        ht_fprintf(outFile,TYPE_NORMAL,"start at ");
//                        data2(0x19, b[x+1]);    // local
//                    }
                    if (b[x+2] & 4) ht_fprintf(outFile,TYPE_NORMAL,", user editable");
                    if ((b[x+2] & 2) == 0) ht_fprintf(outFile,TYPE_NORMAL,", prefer empty");
                    if (b[x+2] & 0x10) ht_fprintf(outFile,TYPE_NORMAL,", on level ground");
                    if (b[x+2] & 0x20) ht_fprintf(outFile,TYPE_NORMAL,", with empty border");
                    if (b[x+2] & 0x40) ht_fprintf(outFile,TYPE_NORMAL,", begin in front of refobj");
                    if (b[x+2] & 0x80) ht_fprintf(outFile,TYPE_NORMAL,", with line of site to center");
                    break;
                case 0x2A:  // Create New Object Instance
                    if (b[x+5] & 4)
                        ht_fprintf(outFile,TYPE_NORMAL,"neighbor in stack obj");
                    else if (b[x+5] & 0x40)
                        ht_fprintf(outFile,TYPE_NORMAL,"from GUID in temp Inventory Token");
                    else if (b[x+5] & 0x80)
                        ht_fprintf(outFile,TYPE_NORMAL,"from GUID in temp0/1");
                    else {
                        d1 = *(UINT32 *) (&b[x]);
                        if (d1 == 0)
                            ht_fprintf(outFile,TYPE_NORMAL,"GUID 0");
                        else {
                            ht_fprintf(outFile,TYPE_NORMAL,"GUID 0x%08X", d1);
                            readGUID(d1);
                        }
                    }
                    ht_fprintf(outFile,TYPE_NORMAL,", place ");
                    c1 = b[x+4];
                    switch (c1) {
                        case 4:
                            ht_fprintf(outFile,TYPE_NORMAL,"in stack objs slot %d", b[x+9]);
                            break;
                        case 0xA:
                            ht_fprintf(outFile,TYPE_NORMAL,"in obj in temp 0's slot %d", b[x+9]);
                            break;
                        default:
                            CHECK_RANGE("Object place", gStringA7, c1);
                            ht_fprintf(outFile,TYPE_NORMAL,"%s", gStringA7[c1]);
                            break;
                    }
                    if (c1 == 8 || c1 == 9) {
                        ht_fprintf(outFile,TYPE_NORMAL," ");
                        data2(0x19, b[x+6]);     // local
                    }
                    c2 = b[x+5];
                    if (c2 & 2)
                        ht_fprintf(outFile,TYPE_NORMAL,", pass object ids to main");
                    else if (c2 & 0x10)
                        ht_fprintf(outFile,TYPE_NORMAL,", pass temp 0 to main");
                    if (c2 & 1)
                        ht_fprintf(outFile,TYPE_NORMAL,", do not duplicate");
                    if (c2 & 8)
                        ht_fprintf(outFile,TYPE_NORMAL,", fail if tile is non-empty");
                    if (b[x+10] & 1)
                        ht_fprintf(outFile,TYPE_NORMAL,", Moving in a new Sim");
                    if (b[x+10] & 2)
                        ht_fprintf(outFile,TYPE_NORMAL,", copying design mode materials from object in temp5");
                    break;
                case 0x32:  // Add/Change the Action String (false = error)
                    c2 = b[x+2]; // flags
                    if (b[x+9] == 0) {
                        c1 = b[x+4] - 1;
                        ht_fprintf(outFile,TYPE_NORMAL,"Add / Change Interaction string Mode, ");
                        if ((b[x+3] & 1) && nodeVersion)
                            ht_fprintf(outFile,TYPE_NORMAL,"Disabled, ");
                        if (c2 & 0x10) {
                            ht_fprintf(outFile,TYPE_NORMAL,"Getting index from temp 0");
                            if (c2 & 4) {
                                ht_fprintf(outFile,TYPE_NORMAL,", getting string from globals");
                            } else if (c2 & 8) {
                                ht_fprintf(outFile,TYPE_NORMAL,", getting string from semiglobals");
                            } else
                                ht_fprintf(outFile,TYPE_NORMAL,", getting string from privates");
                        }
                        else if (c2 & 4) {
                            CHECK_RANGE("Action string", gActionString, c1);
                            ht_fprintf(outFile,TYPE_NORMAL,"%s", gActionString[c1]);
                        } else if (c2 & 8) {
                            if (readString2(gGlobGroup, 0x12E, c1) == 0)
                                ht_fprintf(outFile,TYPE_NORMAL,"[SemiGlobal STR# 0x12E:0x%X]", c1);
                        }
                        else if (readString2(gGroup, 0x12E, c1) == 0)
                            ht_fprintf(outFile,TYPE_NORMAL,"[Private STR# 0x12E:0x%X]", c1);
                    } else {
                        ht_fprintf(outFile,TYPE_NORMAL,"Interaction Icon Change Mode");
                        if (c2 & 0x20) {
                            ht_fprintf(outFile,TYPE_NORMAL,", Using external guid for Thumbnail Outfit from ");
                            if (c2 & 0x40)
                                ht_fprintf(outFile,TYPE_NORMAL,"GUID in temp2/3");
                            else {
                                d1 = *(UINT32 *) (&b[x+5]);
                                if (d1 == 0)
                                    ht_fprintf(outFile,TYPE_NORMAL,"GUID 0");
                                else {
                                   ht_fprintf(outFile,TYPE_NORMAL,"GUID 0x%08X", d1);
                                   readGUID(d1);
                                }
                            }
                        } else {
                            w1 = *(UINT16 *) (&b[x+12]);
                            ht_fprintf(outFile,TYPE_NORMAL,", Using object ID in ");
                            data2(b[x+11], w1);
                        }
                        if (c2 & 0x80)
                            ht_fprintf(outFile,TYPE_NORMAL,", Getting icon index into model table from temp 1");
                        else
                            ht_fprintf(outFile,TYPE_NORMAL,", Using index of %d",b[x+10]);
                    }
                    break;
#endif
