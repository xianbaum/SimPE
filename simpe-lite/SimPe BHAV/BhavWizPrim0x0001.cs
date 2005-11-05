/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.PackedFiles.Wrapper;
using SimPe.PackedFiles.UserInterface;
using pjse.BhavNameWizards;
using pjse.BhavOperandWizards;

namespace pjse.BhavOperandWizards.Wiz0x0001
{
	#region internal form
	/// <summary>
	/// Zusammenfassung für BhavInstruction.
	/// </summary>
	internal class UI : System.Windows.Forms.Form
	{
		#region Form variables

		internal System.Windows.Forms.Panel pnWiz0x0001;
		private System.Windows.Forms.ComboBox cbGenericSimsCall;
		private System.Windows.Forms.Label lbGenericSimsCallparms;
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
		public void Execute(Instruction inst)
		{
			byte operand0 = inst.Operands[0];

			this.cbGenericSimsCall.Items.Clear();
			for (byte i = 0; i < GS.gStr(GS.BhavStr.Generics).Count; i++)
				this.cbGenericSimsCall.Items.Add("0x" + SimPe.Helper.HexString(i) + ": " + GS.GStr(GS.BhavStr.Generics, i));
			this.lbGenericSimsCallparms.Text = "Should never see this";

			if (operand0 < this.cbGenericSimsCall.Items.Count)
				this.cbGenericSimsCall.SelectedIndex = operand0;
			else
			{
				this.cbGenericSimsCall.SelectedIndex = -1;
				lbGenericSimsCallparms.Text = "Unknown operand 0 value 0x" + SimPe.Helper.HexString(operand0);
			}
		}

		public Instruction Write(Instruction inst)
		{
			if (this.cbGenericSimsCall.SelectedIndex >= 0)
				inst.Operands[0] = (byte)this.cbGenericSimsCall.SelectedIndex;
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
			this.pnWiz0x0001.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pnWiz0x0001.Location = new System.Drawing.Point(8, 8);
			this.pnWiz0x0001.Name = "pnWiz0x0001";
			this.pnWiz0x0001.Size = new System.Drawing.Size(264, 72);
			this.pnWiz0x0001.TabIndex = 0;
			// 
			// lbGenericSimsCallparms
			// 
			this.lbGenericSimsCallparms.Location = new System.Drawing.Point(0, 24);
			this.lbGenericSimsCallparms.Name = "lbGenericSimsCallparms";
			this.lbGenericSimsCallparms.Size = new System.Drawing.Size(264, 48);
			this.lbGenericSimsCallparms.TabIndex = 1;
			this.lbGenericSimsCallparms.Text = "label1";
			// 
			// cbGenericSimsCall
			// 
			this.cbGenericSimsCall.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbGenericSimsCall.DropDownWidth = 352;
			this.cbGenericSimsCall.Location = new System.Drawing.Point(0, 0);
			this.cbGenericSimsCall.Name = "cbGenericSimsCall";
			this.cbGenericSimsCall.Size = new System.Drawing.Size(264, 21);
			this.cbGenericSimsCall.TabIndex = 0;
			this.cbGenericSimsCall.SelectedIndexChanged += new System.EventHandler(this.cbGenericSimsCall_Changed);
			// 
			// UI
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(640, 366);
			this.Controls.Add(this.pnWiz0x0001);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "UI";
			this.Text = "UI";
			this.pnWiz0x0001.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void cbGenericSimsCall_Changed(object sender, System.EventArgs e)
		{
			if (cbGenericSimsCall.SelectedIndex >= 0)
				lbGenericSimsCallparms.Text = ((WizPrim0x0001)(byte)cbGenericSimsCall.SelectedIndex).LongName;
		}

	}

	#endregion
}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWiz0x0001 : pjse.ABhavOperandWiz
	{
		public BhavOperandWiz0x0001() : base() { }

		public BhavOperandWiz0x0001(Instruction i) : base(i) { }


		#region pjse.ABhavOperandWiz
		private Wiz0x0001.UI myForm = null;
		public override Panel bhavPrimWizPanel
		{
			get
			{
				if (myForm == null) myForm = new Wiz0x0001.UI();
				return myForm.pnWiz0x0001;
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

		#endregion
	}

}

namespace pjse.BhavNameWizards
{
	public class WizPrim0x0001 : BhavWizPrim
	{
		public WizPrim0x0001(Instruction i) : base(i) {}
		public static implicit operator WizPrim0x0001(byte operand0) //: base(null, null)
		{
			Instruction instruction = new Instruction(null, 0x0001);
			instruction.Operands[0] = operand0;
			return new WizPrim0x0001(instruction);
		}


		protected override string Operands(bool lng)
		{
			string s = GS.GStr(GS.BhavStr.Generics, instruction.Operands[0]);
			if (lng)
			{
				if (instruction.Operands[0] >= parms.Length) s += " ([UNK args])";
				else if (parms[instruction.Operands[0]].Trim().Length == 0) s += " (no args)";
				else s+= " (" + parms[instruction.Operands[0]].Trim() + ")";
			}
			return s;
#if DISASIM
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
#endif
		}


		#region genericSimsCall param descriptions
		// taken from DisASim2 by Shy - public domain
		private static string[] parms =
			{
				"Temp 0:neighborhood, Temp 1:evict, Temp 2:save lot, Temp 3:reset tutorial" // 0x00
				,"" // 0x01
				,"" // 0x02
				,"" // 0x03
				,"Stack Obj:nID, Temp 0:familyID" // 0x04
				,"Temp 0:familyID" // 0x05
				,"Stack Obj:nID" // 0x06
				,"" // 0x07
				,"" // 0x08
				,"" // 0x09
				,"" // 0x0a
				,"" // 0x0b
				,"Temp 0:result value" // 0x0c
				,"Stack Obj" // 0x0d
				,"Temp 0:result value" // 0x0e
				,"Temp 0:result value" // 0x0f
				,"" // 0x10
				,"Temp 0:lotID" // 0x11
				,"Temp 0:speed" // 0x12
				,"Temp 0:neighbor ID" // 0x13
				,"Temp 0:family" // 0x14
				,"Temp 0:child nID, Temp 1:parent nID" // 0x15
				,"Temp 0:new spouse nID, Temp 1:initial spouse nID" // 0x16
				,"Temp 0:remove nID, Temp 1:relative nID" // 0x17
				,"Temp 0:age" // 0x18
				,"Temp 0" // 0x19
				,"" // 0x1a
				,"Temp 0:result value" // 0x1b
				,"Temp 0:amount, Temp 2:multiplier" // 0x1c
				,"Temp 0:nID" // 0x1d
				,"Temp 0:outfit" // 0x1e
				,"Temp 0,1:GUID" // 0x1f
				,"Temp 0:outfit, Temp 1:result value" // 0x20
				,"" // 0x21
				,"Temp 0:source, Temp 1:destination, Temp 2:result value" // 0x22
				,"Temp 0:lotID, Temp 1:result value" // 0x23
				,"Temp 0:lotID, Temp 1:result value" // 0x24
				,"" // 0x25
				,"Temp 0:nID" // 0x26
				,"" // 0x27
				,"Temp 0" // 0x28
				,"Temp 0" // 0x29
				,"" // 0x2a
				,"" // 0x2b
				,"Temp 0" // 0x2c
				,"Temp 0:source, Stack Obj:destination" // 0x2d
				,"Temp 0:tableID, Temp 1:index, Temp 3:fallback" // 0x2e
				,"" // 0x2f
				,"Temp 0:nID, Temp 1:result value" // 0x30
				,"Temp 0" // 0x31
				,"Temp 0:take, Temp 1:target nID, Temp 2:percent, Temp 3,4:amount, Temp 5:from assets" // 0x32
				,"Temp 0" // 0x33
				,"Temp 0:direction, Temp 1:result wall obj" // 0x34
				,"Temp 0:tableID, Temp 1:index, Temp 2:state, Temp 3:fallback" // 0x35
			};
		#endregion
	}


}
