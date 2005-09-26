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

		private System.Windows.Forms.TextBox tbval1;
		private System.Windows.Forms.TextBox tbval2;
		internal System.Windows.Forms.Panel pnWiz0x0002;
		private System.Windows.Forms.ComboBox cbPicker1;
		private System.Windows.Forms.ComboBox cbPicker2;
		private System.Windows.Forms.ComboBox cbOperator;
		private System.Windows.Forms.ComboBox cbDataOwner1;
		private System.Windows.Forms.ComboBox cbDataOwner2;
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

		
		#region UI
		public void Execute(Instruction inst)
		{
			wrappedByteArray ops = inst.Operands;
			int val1 = (ops[0x01] << 8) | ops[0x00];
			int val2 = (ops[0x03] << 8) | ops[0x02];

			tbval1.Text = "0x"+SimPe.Helper.HexString((ushort)val1);
			tbval2.Text = "0x"+SimPe.Helper.HexString((ushort)val2);

			byte op = ops[0x05];
			if (cbOperator.Items.Count>op) cbOperator.SelectedIndex = op;

			byte n1 = ops[0x06];
			byte n2 = ops[0x07];
			if (cbDataOwner1.Items.Count>n1) cbDataOwner1.SelectedIndex = n1;
			if (cbDataOwner2.Items.Count>n2) cbDataOwner2.SelectedIndex = n2;
			//doFlagThing();
		}

		public Instruction Write(Instruction inst)
		{
			try 
			{
				wrappedByteArray ops = inst.Operands;
				ops[0x06] = (byte)cbDataOwner1.SelectedIndex;
				ops[0x07] = (byte)cbDataOwner2.SelectedIndex;
				ops[0x05] = (byte)cbOperator.SelectedIndex;

				ushort val1 = textToUShort(tbval1.Text);
				ushort val2 = textToUShort(tbval2.Text);

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

		private void FormLoad()
		{
			this.cbDataOwner1.Items.Clear();
			this.cbDataOwner1.Items.AddRange(GS.gStr(GS.SF.DataOwners).ToArray());
			this.cbDataOwner2.Items.Clear();
			this.cbDataOwner2.Items.AddRange(GS.gStr(GS.SF.DataOwners).ToArray());
			this.cbOperator.Items.Clear();
			this.cbOperator.Items.AddRange(GS.gStr(GS.SF.Operators).ToArray());
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
			this.pnWiz0x0002.Controls.Add(this.cbPicker2);
			this.pnWiz0x0002.Controls.Add(this.cbPicker1);
			this.pnWiz0x0002.Controls.Add(this.cbOperator);
			this.pnWiz0x0002.Controls.Add(this.tbval2);
			this.pnWiz0x0002.Controls.Add(this.cbDataOwner2);
			this.pnWiz0x0002.Controls.Add(this.tbval1);
			this.pnWiz0x0002.Controls.Add(this.cbDataOwner1);
			this.pnWiz0x0002.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pnWiz0x0002.Location = new System.Drawing.Point(8, 8);
			this.pnWiz0x0002.Name = "pnWiz0x0002";
			this.pnWiz0x0002.Size = new System.Drawing.Size(464, 72);
			this.pnWiz0x0002.TabIndex = 0;
			// 
			// cbPicker2
			// 
			this.cbPicker2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbPicker2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPicker2.DropDownWidth = 352;
			this.cbPicker2.Location = new System.Drawing.Point(352, 50);
			this.cbPicker2.Name = "cbPicker2";
			this.cbPicker2.Size = new System.Drawing.Size(112, 21);
			this.cbPicker2.TabIndex = 5;
			this.cbPicker2.Visible = false;
			this.cbPicker2.SelectedIndexChanged += new System.EventHandler(this.cbPicker2_SelectedIndexChanged);
			// 
			// cbPicker1
			// 
			this.cbPicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPicker1.DropDownWidth = 352;
			this.cbPicker1.Location = new System.Drawing.Point(352, 0);
			this.cbPicker1.Name = "cbPicker1";
			this.cbPicker1.Size = new System.Drawing.Size(112, 21);
			this.cbPicker1.TabIndex = 2;
			this.cbPicker1.Visible = false;
			this.cbPicker1.SelectedIndexChanged += new System.EventHandler(this.cbPicker1_SelectedIndexChanged);
			// 
			// cbOperator
			// 
			this.cbOperator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbOperator.Location = new System.Drawing.Point(0, 26);
			this.cbOperator.Name = "cbOperator";
			this.cbOperator.Size = new System.Drawing.Size(464, 21);
			this.cbOperator.TabIndex = 3;
			// 
			// tbval2
			// 
			this.tbval2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tbval2.Location = new System.Drawing.Point(352, 50);
			this.tbval2.Name = "tbval2";
			this.tbval2.Size = new System.Drawing.Size(112, 21);
			this.tbval2.TabIndex = 5;
			this.tbval2.Text = "";
			// 
			// cbDataOwner2
			// 
			this.cbDataOwner2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cbDataOwner2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDataOwner2.Location = new System.Drawing.Point(0, 50);
			this.cbDataOwner2.Name = "cbDataOwner2";
			this.cbDataOwner2.Size = new System.Drawing.Size(352, 21);
			this.cbDataOwner2.TabIndex = 4;
			this.cbDataOwner2.SelectedIndexChanged += new System.EventHandler(this.cbDataOwner2_SelectedIndexChanged);
			// 
			// tbval1
			// 
			this.tbval1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tbval1.Location = new System.Drawing.Point(352, 0);
			this.tbval1.Name = "tbval1";
			this.tbval1.Size = new System.Drawing.Size(112, 21);
			this.tbval1.TabIndex = 2;
			this.tbval1.Text = "";
			// 
			// cbDataOwner1
			// 
			this.cbDataOwner1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cbDataOwner1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDataOwner1.Location = new System.Drawing.Point(0, 0);
			this.cbDataOwner1.Name = "cbDataOwner1";
			this.cbDataOwner1.Size = new System.Drawing.Size(352, 21);
			this.cbDataOwner1.TabIndex = 1;
			this.cbDataOwner1.SelectedIndexChanged += new System.EventHandler(this.cbDataOwner1_SelectedIndexChanged);
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

		private void cbDataOwner1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.cbPicker1.Visible = false;
			if (ABhavNameWiz.doidGStr[(byte)cbDataOwner1.SelectedIndex] != null)
			{
				this.cbPicker1.Visible = true;
				this.cbPicker1.Items.Clear();
				this.cbPicker1.Items.AddRange(GS.gStr((uint)ABhavNameWiz.doidGStr[(byte)cbDataOwner1.SelectedIndex]).ToArray());
				try 
				{
					ushort val = textToUShort(tbval1.Text);
					this.cbPicker1.SelectedIndex = val;
				} 
				catch (Exception) { }
			}
			else if (cbDataOwner1.SelectedIndex == 0x1a || cbDataOwner1.SelectedIndex == 0x2f)
			{
				//constant
				ushort[] vals = ConstantValueParser(textToUShort(tbval1.Text));
				tbval1.Text = "0x"+SimPe.Helper.HexString(vals[0])+":0x"+SimPe.Helper.HexString((byte)vals[1]);
			} 
			else
			{
				tbval1.Text = "0x"+SimPe.Helper.HexString(textToUShort(tbval1.Text));
			}
			doFlagThing();
		}

		private void cbDataOwner2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.cbPicker2.Visible = false;
			if (ABhavNameWiz.doidGStr[(byte)cbDataOwner2.SelectedIndex] != null)
			{
				this.cbPicker2.Visible = true;
				this.cbPicker2.Items.Clear();
				this.cbPicker2.Items.AddRange(GS.gStr((uint)ABhavNameWiz.doidGStr[(byte)cbDataOwner2.SelectedIndex]).ToArray());
				try 
				{
					ushort val = textToUShort(tbval2.Text);
					this.cbPicker2.SelectedIndex = val;
				} 
				catch (Exception) { }
			}
			else if (cbDataOwner2.SelectedIndex == 0x1a || cbDataOwner2.SelectedIndex == 0x2f)
			{
				//constant
				ushort[] vals = ConstantValueParser(textToUShort(tbval2.Text));
				tbval2.Text = "0x"+SimPe.Helper.HexString(vals[0])+":0x"+SimPe.Helper.HexString((byte)vals[1]);
			} 
			else
			{
				tbval2.Text = "0x"+SimPe.Helper.HexString(textToUShort(tbval2.Text));
			}
			doFlagThing();
		}

		private void cbPicker1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			tbval1.Text = "0x"+SimPe.Helper.HexString((ushort)this.cbPicker1.SelectedIndex);
			doFlagThing();
		}

		private void cbPicker2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			tbval2.Text = "0x"+SimPe.Helper.HexString((ushort)this.cbPicker2.SelectedIndex);
		}


		private void doFlagThing()
		{
			if (cbDataOwner2.SelectedIndex == 7 && cbOperator.SelectedIndex >= 8 && cbOperator.SelectedIndex <= 10)
			{
				ArrayList flagNames = PrimWiz0x0002.flagNames((byte)cbDataOwner1.SelectedIndex, (ushort)textToUShort(tbval1.Text));
				if (flagNames != null)
				{
					this.cbPicker2.Visible = true;
					this.cbPicker2.Items.Clear();
					this.cbPicker2.Items.AddRange(flagNames.ToArray());
					try 
					{
						ushort val = textToUShort(tbval2.Text);
						this.cbPicker2.SelectedIndex = val;
					} 
					catch (Exception) { }
				}
			}
			this.tbval2.Visible = !cbPicker2.Visible;
		}


		private static ushort textToUShort(string text)
		{
			ushort val = 0;
			try 
			{
				if (text.IndexOf(":")==-1)
				{
					val = Convert.ToUInt16(text, 16);
				}
				else 
				{
					string[] s = text.Split(":".ToCharArray(), 2);
					ushort[] b = new ushort[2];
					b[0] = Convert.ToUInt16(s[0], 16);
					b[1] = Convert.ToUInt16(s[1], 16);
					val = ConstantValueParser(b);
				}
			}
			catch (Exception) { }
			return val;
		}

		private static ushort[] ConstantValueParser(ushort val) 
		{
			return pjse.ABhavNameWiz.ExpandBCON(val);
		}

		private static ushort ConstantValueParser(ushort[] values) 
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
			else if ((values[0] & 0x0140) == 0x0140)
			{
				values[0] = (ushort)(values[0]-0x0140);
				t = 4;
			}
			else
			{
				values[0] = (ushort)(values[0]-0x0100);
			}

			ushort ret = 0;
			ret = (ushort)(t << 13);
			ret += (ushort)((values[0] & 0x3f)  << 7);
			ret += (ushort)(values[1] & 0x7F);
			
			return ret;
		}

	}

	#endregion
}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWiz0x0002 : pjse.ABhavOperandWiz
	{
		public BhavOperandWiz0x0002() : base() { }

		public BhavOperandWiz0x0002(Instruction i) : base(i) { }


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
	public class PrimWiz0x0002 : ANamePrimitiveWiz
	{
		public PrimWiz0x0002(Instruction i) : base(i) {}
		public static implicit operator PrimWiz0x0002(byte[] operands)
		{
			Instruction i = null;
			if (operands.Length == 8)
			{
				i = new Instruction(null, 0x0002, 0, 0, 0, operands, new byte[8]);
			}
			else if (operands.Length == 16)
			{
				byte[] a = new byte[8], b = new byte[8];
				operands.CopyTo(a, 0);
				operands.CopyTo(b, 8);
				i = new Instruction(null, 0x0002, 0, 0, 0, a, b);
			}
			else
				i = new Instruction(null, 0x0002);

			return new PrimWiz0x0002(i);
		}


		public override string ShortName
		{
			get
			{
				byte[] o = instruction.Operands;
				return prefix + " (" +
					GS.GStr(GS.SF.DataOwners, o[6]) + " 0x" + SimPe.Helper.HexString(ToShort(o[0], o[1])) + " " +
					GS.GStr(GS.SF.Operators, o[5]) + " " +
					GS.GStr(GS.SF.DataOwners, o[7]) + " 0x" + SimPe.Helper.HexString(ToShort(o[2], o[3])) + ")";
			}
		}


		public override string LongName
		{
			get
			{
				byte[] operands = new byte[16];
				((byte[])instruction.Operands).CopyTo(operands, 0);
				((byte[])instruction.Reserved1).CopyTo(operands, 8);

				byte lhs_data_owner = operands[6]; // c2
				ushort lhs_value_word = ToShort(operands[0], operands[1]); // w1
				byte _operator = operands[5]; // c1
				byte rhs_data_owner = operands[7]; // b[x+7]
				ushort rhs_value_word = ToShort(operands[2], operands[3]); // w2

				string s = "";
				s += dataOwner(lhs_data_owner, lhs_value_word);
				s += " " + GS.GStr(GS.SF.Operators, _operator) + " ";

				if (_operator >= 8 && _operator <= 10) // Flag operation
				{
					s+= "flag# " + dataOwner(rhs_data_owner, rhs_value_word);
					if (rhs_data_owner == 7 && flagname(lhs_data_owner, lhs_value_word, rhs_value_word) != null)
						s += " (" + flagname(lhs_data_owner, lhs_value_word, rhs_value_word) + ")";
				}
				else
					s+= dataOwner(rhs_data_owner, rhs_value_word);
				return prefix + " (" + s + ")";
			}
		}

		private string prefix { get { return base.ShortName; } }


		public static ArrayList flagNames(byte flagOwner, ushort flagType)
		{
			Hashtable flagTypes = (Hashtable)flagOwners[flagOwner];
			return (flagTypes == null || flagTypes[flagType] == null) ? null : GS.gStr((uint)flagTypes[flagType]);
		}

		public static string flagname(byte flagOwner, ushort flagType, ushort flagValue)
		{
			Hashtable flagTypes = (Hashtable)flagOwners[flagOwner];
			return (flagTypes == null || flagTypes[flagType] == null) ? null : GS.GStr((uint)flagTypes[flagType], flagValue);
		}


		public static Hashtable flagOwners = flagInitaliser();
		private static Hashtable flagInitaliser()
		{
			Hashtable f = new Hashtable();
			Hashtable o = new Hashtable();
			o.Add((ushort)0x05, GS.SF.gWallAdjFlags);
			o.Add((ushort)0x08, GS.SF.gFlags1);
			o.Add((ushort)0x22, GS.SF.gHiddenFlags);
			o.Add((ushort)0x28, GS.SF.gFlags2);
			o.Add((ushort)0x2a, GS.SF.gPlacementFlags);
			o.Add((ushort)0x2b, GS.SF.gMoveFlags);
			o.Add((ushort)0x3f, GS.SF.gExclPlacementFlags);
			o.Add((ushort)0x45, GS.SF.gWallCutoutFlags);
			f.Add((byte)0x03, o); // 0x03 "My"
			f.Add((byte)0x04, o); // 0x04 "Stack Object's"
			Hashtable p = new Hashtable();
			p.Add((ushort)0x1e, GS.SF.gCensorFlags);
			p.Add((ushort)0x44, GS.SF.gGhostFlags);
			p.Add((ushort)0x51, GS.SF.gBodyFlags);
			p.Add((ushort)0x9e, GS.SF.gSelectionFlags);
			p.Add((ushort)0x9f, GS.SF.gPersonFlags);
			f.Add((byte)0x12, p); // 0x12 "My Person Data"
			f.Add((byte)0x13, p); // 0x13 "Stack Object's Person Data"
			f.Add((byte)0x20, p); // 0x20 "Neighbour's Person Data"
			Hashtable d = new Hashtable();
			d.Add((ushort)0x27, GS.SF.gRoomSortFlags);
			d.Add((ushort)0x28, GS.SF.gFunctionSortFlags);
			f.Add((byte)0x15, d); // 0x15 "stack object's definition"
			f.Add((byte)0x26, d); // 0x26 "Neighbor's Object Definition"
			f.Add((byte)0x33, d); // 0x33 "Stack Object's Master Definition"
			return f;
		}

	}
}
