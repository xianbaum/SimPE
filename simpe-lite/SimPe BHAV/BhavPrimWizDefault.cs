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

namespace SimPe.PackedFiles.UserInterface
{
	#region internal form
	/// <summary>
	/// Summary description for BhavPrimWizDefault.
	/// </summary>
	internal class BhavPrimWizDefaultUI : System.Windows.Forms.Form
	{
		#region Form variables
		internal System.Windows.Forms.Panel panel1;
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
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public BhavPrimWizDefaultUI()
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


		#region BhavPrimWizDefaultUI
		private Instruction inst;
		private ArrayList alHex8;
		private ArrayList alDec16;

		internal void Execute(Instruction inst)
		{
			this.inst = inst;

			this.tbInst_Op01_dec.Text = (inst.Operands[0] + (inst.Operands[1] << 8)).ToString();
			this.tbInst_Op23_dec.Text = (inst.Operands[2] + (inst.Operands[3] << 8)).ToString();

			this.tbInst_Op0.Text = Helper.HexString(inst.Operands[0]);
			this.tbInst_Op1.Text = Helper.HexString(inst.Operands[1]);
			this.tbInst_Op2.Text = Helper.HexString(inst.Operands[2]);
			this.tbInst_Op3.Text = Helper.HexString(inst.Operands[3]);
			this.tbInst_Op4.Text = Helper.HexString(inst.Operands[4]);
			this.tbInst_Op5.Text = Helper.HexString(inst.Operands[5]);
			this.tbInst_Op6.Text = Helper.HexString(inst.Operands[6]);
			this.tbInst_Op7.Text = Helper.HexString(inst.Operands[7]);

			this.tbInst_Unk0.Text = Helper.HexString(inst.Reserved1[0]);
			this.tbInst_Unk1.Text = Helper.HexString(inst.Reserved1[1]);
			this.tbInst_Unk2.Text = Helper.HexString(inst.Reserved1[2]);
			this.tbInst_Unk3.Text = Helper.HexString(inst.Reserved1[3]);
			this.tbInst_Unk4.Text = Helper.HexString(inst.Reserved1[4]);
			this.tbInst_Unk5.Text = Helper.HexString(inst.Reserved1[5]);
			this.tbInst_Unk6.Text = Helper.HexString(inst.Reserved1[6]);
			this.tbInst_Unk7.Text = Helper.HexString(inst.Reserved1[7]);
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
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
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tbInst_Op01_dec);
			this.panel1.Controls.Add(this.label13);
			this.panel1.Controls.Add(this.tbInst_Unk7);
			this.panel1.Controls.Add(this.tbInst_Unk6);
			this.panel1.Controls.Add(this.tbInst_Unk5);
			this.panel1.Controls.Add(this.tbInst_Unk4);
			this.panel1.Controls.Add(this.tbInst_Unk3);
			this.panel1.Controls.Add(this.tbInst_Unk2);
			this.panel1.Controls.Add(this.tbInst_Unk1);
			this.panel1.Controls.Add(this.tbInst_Unk0);
			this.panel1.Controls.Add(this.tbInst_Op7);
			this.panel1.Controls.Add(this.tbInst_Op6);
			this.panel1.Controls.Add(this.tbInst_Op5);
			this.panel1.Controls.Add(this.tbInst_Op4);
			this.panel1.Controls.Add(this.tbInst_Op3);
			this.panel1.Controls.Add(this.tbInst_Op2);
			this.panel1.Controls.Add(this.tbInst_Op1);
			this.panel1.Controls.Add(this.tbInst_Op0);
			this.panel1.Controls.Add(this.tbInst_Op23_dec);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(264, 72);
			this.panel1.TabIndex = 0;
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
			// BhavPrimWizDefaultUI
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.panel1);
			this.Name = "BhavPrimWizDefaultUI";
			this.Text = "BhavPrimWizDefaultUI";
			this.panel1.ResumeLayout(false);
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
				((TextBox)this.alHex8[i]).Text = Helper.HexString(inst.Operands[i]);
				inst.Operands[i+1] = (byte)((val >> 8) & 0xFF);
				((TextBox)this.alHex8[i+1]).Text = Helper.HexString(inst.Operands[i+1]);
			}
		}

	}
	#endregion

	public class BhavOperandWizDefault : pjse.ABhavOperandWiz
	{
		private BhavPrimWizDefaultUI myForm = null;

		public BhavOperandWizDefault(Instruction i) : base(i) { }


		#region pjse.ABhavOperandWiz
		public override Panel bhavPrimWizPanel
		{
			get
			{
				if (myForm == null) myForm = new BhavPrimWizDefaultUI();
				return myForm.panel1;
			}
		}

		public override void Execute()
		{
			if (myForm == null) myForm = new BhavPrimWizDefaultUI();
			myForm.Execute(instruction);
		}

		public override Instruction Write()
		{
			return instruction;
		}


		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm.Dispose();
		}
		#endregion

		#endregion
	}


}
