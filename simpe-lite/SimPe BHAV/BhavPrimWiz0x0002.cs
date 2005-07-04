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
using SimPe.PackedFiles.UserInterface;
using pjse.BhavNameWizards;
using pjse.BhavOperandWizards;

namespace pjse.BhavOperandWizards.Wiz0x0002
{
	#region internal form
	/// <summary>
	/// Zusammenfassung für BhavInstruction.
	/// </summary>
	internal class UI : System.Windows.Forms.Form
	{
		#region Form variables
		private System.Windows.Forms.ComboBox cbtype1;
		private System.Windows.Forms.ComboBox cbtype2;
		private System.Windows.Forms.ComboBox cboperand;
		private System.Windows.Forms.TextBox tbval1;
		private System.Windows.Forms.TextBox tbval2;
		private System.Windows.Forms.ComboBox cbmotiv1;
		private System.Windows.Forms.ComboBox cbmotiv2;
		internal System.Windows.Forms.Panel pnWiz0x0002;
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

			FormLoad();
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

		
		#region MyForm
		public Instruction Write(Instruction inst)
		{
			try 
			{
				wrappedByteArray ops = inst.Operands;
				ops[0x06] = (byte)cbtype1.SelectedIndex;
				ops[0x07] = (byte)cbtype2.SelectedIndex;
				ops[0x05] = (byte)cboperand.SelectedIndex;

				cbtype1.SelectedIndex = 0;
				cbtype2.SelectedIndex = 0;

				int val1 = Convert.ToUInt16(tbval1.Text, 16);
				int val2 = Convert.ToUInt16(tbval2.Text, 16);

				ops[0x00] = (byte)(val1 & 0xff);
				ops[0x01] = (byte)((val1 >> 8) & 0xff);

				ops[0x02] = (byte)(val2 & 0xff);
				ops[0x03] = (byte)((val2 >> 8) & 0xff);

				return inst;
			} 
			catch (Exception ex) 
			{
				SimPe.Helper.ExceptionMessage(SimPe.Localization.Manager.GetString("errconvert"), ex);
				return null;
			}
		}

		public void Execute(Instruction inst)
		{
			wrappedByteArray ops = inst.Operands;
			byte n1 = ops[0x06];
			byte n2 = ops[0x07];
			byte op = ops[0x05];

			int val1 = (ops[0x01] << 8) | ops[0x00];
			int val2 = (ops[0x03] << 8) | ops[0x02];

			tbval1.Text = "0x"+SimPe.Helper.HexString((ushort)val1);
			tbval2.Text = "0x"+SimPe.Helper.HexString((ushort)val2);

			if (cbtype1.Items.Count>n1) cbtype1.SelectedIndex = n1;
			if (cbtype2.Items.Count>n2) cbtype2.SelectedIndex = n2;

			if (cboperand.Items.Count>op) cboperand.SelectedIndex = op;
		}

		private void FormLoad()
		{
			if (SimPe.Plugin.WrapperFactory.Provider==null) return;

			foreach (string s in SimPe.Plugin.WrapperFactory.Provider.OpcodeProvider.StoredExpressionOperators)
				this.cboperand.Items.Add(s);

			foreach (string s in SimPe.Plugin.WrapperFactory.Provider.OpcodeProvider.StoredDataNames) 
			{
				this.cbtype1.Items.Add(s);
				this.cbtype2.Items.Add(s);
			}

			foreach (string s in SimPe.Plugin.WrapperFactory.Provider.OpcodeProvider.StoredMotives) 
			{
				this.cbmotiv1.Items.Add(s);
			}
		}

		#endregion

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnWiz0x0002 = new System.Windows.Forms.Panel();
			this.cbmotiv2 = new System.Windows.Forms.ComboBox();
			this.cbmotiv1 = new System.Windows.Forms.ComboBox();
			this.cboperand = new System.Windows.Forms.ComboBox();
			this.tbval2 = new System.Windows.Forms.TextBox();
			this.cbtype2 = new System.Windows.Forms.ComboBox();
			this.tbval1 = new System.Windows.Forms.TextBox();
			this.cbtype1 = new System.Windows.Forms.ComboBox();
			this.pnWiz0x0002.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnWiz0x0002
			// 
			this.pnWiz0x0002.Controls.Add(this.cbmotiv2);
			this.pnWiz0x0002.Controls.Add(this.cbmotiv1);
			this.pnWiz0x0002.Controls.Add(this.cboperand);
			this.pnWiz0x0002.Controls.Add(this.tbval2);
			this.pnWiz0x0002.Controls.Add(this.cbtype2);
			this.pnWiz0x0002.Controls.Add(this.tbval1);
			this.pnWiz0x0002.Controls.Add(this.cbtype1);
			this.pnWiz0x0002.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pnWiz0x0002.Location = new System.Drawing.Point(8, 8);
			this.pnWiz0x0002.Name = "pnWiz0x0002";
			this.pnWiz0x0002.Size = new System.Drawing.Size(264, 72);
			this.pnWiz0x0002.TabIndex = 0;
			// 
			// cbmotiv2
			// 
			this.cbmotiv2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbmotiv2.Location = new System.Drawing.Point(152, 50);
			this.cbmotiv2.Name = "cbmotiv2";
			this.cbmotiv2.Size = new System.Drawing.Size(112, 21);
			this.cbmotiv2.TabIndex = 6;
			this.cbmotiv2.Visible = false;
			this.cbmotiv2.SelectedIndexChanged += new System.EventHandler(this.Motive2Changed);
			// 
			// cbmotiv1
			// 
			this.cbmotiv1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbmotiv1.Location = new System.Drawing.Point(152, 0);
			this.cbmotiv1.Name = "cbmotiv1";
			this.cbmotiv1.Size = new System.Drawing.Size(112, 21);
			this.cbmotiv1.TabIndex = 5;
			this.cbmotiv1.Visible = false;
			this.cbmotiv1.SelectedIndexChanged += new System.EventHandler(this.Motive1Changed);
			// 
			// cboperand
			// 
			this.cboperand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboperand.Location = new System.Drawing.Point(0, 26);
			this.cboperand.Name = "cboperand";
			this.cboperand.Size = new System.Drawing.Size(264, 21);
			this.cboperand.TabIndex = 4;
			// 
			// tbval2
			// 
			this.tbval2.Location = new System.Drawing.Point(152, 50);
			this.tbval2.Name = "tbval2";
			this.tbval2.Size = new System.Drawing.Size(112, 21);
			this.tbval2.TabIndex = 3;
			this.tbval2.Text = "";
			// 
			// cbtype2
			// 
			this.cbtype2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbtype2.Location = new System.Drawing.Point(0, 50);
			this.cbtype2.Name = "cbtype2";
			this.cbtype2.Size = new System.Drawing.Size(152, 21);
			this.cbtype2.TabIndex = 2;
			this.cbtype2.SelectedIndexChanged += new System.EventHandler(this.SelectVal2Name);
			// 
			// tbval1
			// 
			this.tbval1.Location = new System.Drawing.Point(152, 0);
			this.tbval1.Name = "tbval1";
			this.tbval1.Size = new System.Drawing.Size(112, 21);
			this.tbval1.TabIndex = 1;
			this.tbval1.Text = "";
			// 
			// cbtype1
			// 
			this.cbtype1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbtype1.Location = new System.Drawing.Point(0, 0);
			this.cbtype1.Name = "cbtype1";
			this.cbtype1.Size = new System.Drawing.Size(152, 21);
			this.cbtype1.TabIndex = 0;
			this.cbtype1.SelectedIndexChanged += new System.EventHandler(this.SelectVal1Name);
			// 
			// UI
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(640, 366);
			this.Controls.Add(this.pnWiz0x0002);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "UI";
			this.Text = "UI";
			this.pnWiz0x0002.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void SelectVal1Name(object sender, System.EventArgs e)
		{
			this.cbmotiv1.Visible = (cbtype1.SelectedIndex==0x0E);
			this.tbval1.Visible = !cbmotiv1.Visible;

			//constant
			if (cbtype1.SelectedIndex==0x1a) 
			{
				if (tbval1.Text.IndexOf(":")==-1) 
				{
					try 
					{
						ushort val = Convert.ToUInt16(tbval1.Text, 16);
						tbval1.Text = "0x"+SimPe.Helper.HexString(PrimWiz0x0002.ConstantValueParser(val)[0])+":0x"+SimPe.Helper.HexString((byte)PrimWiz0x0002.ConstantValueParser(val)[1]);
					} 
					catch (Exception) {}
				}
			} 
			else if (cbtype1.SelectedIndex==0xe) 
			{
				try 
				{
					ushort val = Convert.ToUInt16(tbval1.Text, 16);
					this.cbmotiv1.SelectedIndex = val;
				} 
				catch (Exception) {}
			}
			else 
			{
				if (tbval1.Text.IndexOf(":")!=-1) 
				{
					string[] s = tbval1.Text.Split(":".ToCharArray(), 2);
					
					try 
					{
						ushort[] b = new ushort[2];
						b[0] = Convert.ToUInt16(s[0], 16);
						b[1] = Convert.ToUInt16(s[1], 16);
						tbval1.Text = "0x"+SimPe.Helper.HexString(PrimWiz0x0002.ConstantValueParser(b));

					} 
					catch (Exception) {}
				}
			}
		}

		private void SelectVal2Name(object sender, System.EventArgs e)
		{
			this.cbmotiv2.Visible = (cbtype2.SelectedIndex==0x0E);
			this.tbval2.Visible = !cbmotiv2.Visible;
			//constant
			if (cbtype2.SelectedIndex==0x1a) 
			{
				if (tbval2.Text.IndexOf(":")==-1) 
				{
					try 
					{
						ushort val = Convert.ToUInt16(tbval2.Text, 16);
						tbval2.Text = "0x"+SimPe.Helper.HexString(PrimWiz0x0002.ConstantValueParser(val)[0])+":0x"+SimPe.Helper.HexString((byte)PrimWiz0x0002.ConstantValueParser(val)[1]);
					} 
					catch (Exception) {}
				}
			}
			else if (cbtype2.SelectedIndex==0xe) 
			{
				try 
				{
					ushort val = Convert.ToUInt16(tbval2.Text, 16);
					this.cbmotiv2.SelectedIndex = val;
				} 
				catch (Exception) {}
			}
			else
			{
				if (tbval2.Text.IndexOf(":")!=-1) 
				{
					string[] s = tbval2.Text.Split(":".ToCharArray(), 2);
					
					try 
					{
						ushort[] b = new ushort[2];
						b[0] = Convert.ToUInt16(s[0], 16);
						b[1] = Convert.ToUInt16(s[1], 16);
						tbval2.Text = "0x"+SimPe.Helper.HexString(PrimWiz0x0002.ConstantValueParser(b));

					} 
					catch (Exception) {}
				}
			}
		}

		private void Motive1Changed(object sender, System.EventArgs e)
		{
			tbval1.Text = "0x"+SimPe.Helper.HexString((ushort)this.cbmotiv1.SelectedIndex);
		}

		private void Motive2Changed(object sender, System.EventArgs e)
		{
			tbval2.Text = "0x"+SimPe.Helper.HexString((ushort)this.cbmotiv2.SelectedIndex);
		}


	}

	#endregion
}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWiz0x0002 : pjse.ABhavOperandWiz
	{
		public BhavOperandWiz0x0002() : base() { }

		public BhavOperandWiz0x0002(Instruction i) : base() { instruction = i; }


		#region pjse.ABhavOperandWiz
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

		#endregion
	}
}


namespace pjse.BhavNameWizards
{
	public class PrimWiz0x0002 : ANamePrimitiveWiz
	{
		public PrimWiz0x0002(Bhav parent, ushort opcode, byte[] operands) : base(parent, opcode, operands) {}
		public PrimWiz0x0002(Bhav parent, byte[] operands) : base(parent, operands) { instruction.Opcode = 0x0002; }
		public PrimWiz0x0002(Instruction i) : base(i) {}
		/*public string VeryShortName
		{
			get
			{
				if (this.instruction == null && this.operands == null) return "";
				if (op0 >= parms.Length) return "Unknown operand 0: 0x" + SimPe.Helper.HexString(op0);
				return op0names[op0];
			}
		}*/

		public override string ShortName
		{
			get
			{
				return OpcodeName(instruction.Parent, instruction.Opcode, instruction.Operands);
				/*
				string s = base.ShortName;
				if (this.instruction == null && this.operands == null) return s;
				if (op0 >= parms.Length) return s + " Unknown operand 0: 0x" + SimPe.Helper.HexString(op0);
				return s + " " + op0names[op0];*/
			}
		}

		public override string LongName
		{
			get
			{
				return ShortName;
				/*if ((this.instruction == null && this.operands == null) || (op0 >= parms.Length)) return ShortName;
				return ShortName + " (" + parms[op0] + ")";*/
			}
		}


		private string OpcodeName(Bhav parent, ushort opcode, wrappedByteArray operands)
		{			
			if (parent==null || operands==null) return SimPe.Localization.Manager.GetString("Unknown");

			string obj1 = parent.Opcodes.FindExpressionDataOwners(operands[6]);
			ushort op1 = ToShort(operands[0], operands[1]);
			string val1 = " 0x" + SimPe.Helper.HexString(op1);
			string obj2 = parent.Opcodes.FindExpressionDataOwners(operands[7]); 
			ushort op2 = ToShort(operands[2], operands[3]);
			string val2 = " 0x" + SimPe.Helper.HexString(op2);
			string name = parent.Opcodes.FindExpressionOperator(operands[5]);

			if (obj1.ToLower()=="my motives") val1 = " "+parent.Opcodes.FindMotives(op1)+ " ("+val1.Trim()+")";
			if (obj2.ToLower()=="my motives") val2 = " "+parent.Opcodes.FindMotives(op2)+ " ("+val2.Trim()+")";

			if (obj1.ToLower()=="constant value") val1 = " 0x"+SimPe.Helper.HexString(ConstantValueParser(op1)[0])+":0x"+SimPe.Helper.HexString((byte)ConstantValueParser(op1)[1]);
			if (obj2.ToLower()=="constant value") val2 = " 0x"+SimPe.Helper.HexString(ConstantValueParser(op2)[0])+":0x"+SimPe.Helper.HexString((byte)ConstantValueParser(op2)[1]);
				

			return obj1+val1+" "+name+" "+obj2+val2;
		}

		internal static ushort[] ConstantValueParser(ushort val) 
		{
			ushort[] ret = new ushort[2];

			ret[0] = (ushort)((val >> 7) & 0x3f);
			ret[1] = (ushort)(val & 0x7F);
			int t = (val >> 13) & 0x7;

			if (t==0) ret[0] += 0x1000;
			else if (t==1) ret[0] += 0x2000;
			else ret[0] += 0x0100;

			return ret;
		}

		internal static ushort ConstantValueParser(ushort[] values) 
		{
			int t = 2;
			if ((values[0]>=0x1000) && (values[0]<0x2000)) 
			{
				values[0] = (ushort)(values[0]-0x1000);
				t = 0;
			} 
			else if (values[0]>0x2000) 
			{
				values[0] = (ushort)(values[0]-0x2000);
				t = 1;
			} 
			else 
			{
				values[0] = (ushort)(values[0]-0x100);
			}
			ushort ret = 0;

			ret = (ushort)(t << 13);
			ret += (ushort)((values[0] & 0x3f)  << 7);
			ret += (ushort)(values[1] & 0x7F);
			
			return ret;
		}


		private ushort ToShort(byte lower, byte higher)
		{
			return (ushort)((higher << 8) + lower);
		}

	}


}
