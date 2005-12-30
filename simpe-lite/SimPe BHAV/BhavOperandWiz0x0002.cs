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
		private System.Windows.Forms.CheckBox cbDecimal;
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

			inst = null;
		}


		#region Controller
		private Instruction inst = null;

		private bool Decimal
		{
			get
			{
				SimPe.XmlRegistryKey  rkf = SimPe.Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey("PJSE\\Bhav\\OperandWiz0x02");
				object o = rkf.GetValue("decimal", false);
				return Convert.ToBoolean(o);
			}

			set
			{
				SimPe.XmlRegistryKey rkf = SimPe.Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey("PJSE\\Bhav\\OperandWiz0x02");
				rkf.SetValue("decimal", value);
			}

		}


		private void FormLoad()
		{
			this.cbDataOwner1.Items.Clear();
			this.cbDataOwner1.Items.AddRange(GS.gStr(GS.BhavStr.DataOwners).ToArray());
			this.cbDataOwner2.Items.Clear();
			this.cbDataOwner2.Items.AddRange(GS.gStr(GS.BhavStr.DataOwners).ToArray());
			this.cbOperator.Items.Clear();
			this.cbOperator.Items.AddRange(GS.gStr(GS.BhavStr.Operators).ToArray());
		}


		public void Execute(Instruction inst)
		{
			this.inst = inst;

			wrappedByteArray ops = inst.Operands;

			int val1 = (ops[0x01] << 8) | ops[0x00];
			int val2 = (ops[0x03] << 8) | ops[0x02];
			tbval1.Text = Decimal ? val1.ToString() : "0x"+SimPe.Helper.HexString((ushort)val1);
			tbval2.Text = Decimal ? val2.ToString() : "0x"+SimPe.Helper.HexString((ushort)val2);

			cbOperator.SelectedIndex   = (cbOperator.Items.Count   > ops[0x05]) ? ops[0x05] : -1;

			cbDataOwner1.SelectedIndex = (cbDataOwner1.Items.Count > ops[0x06]) ? ops[0x06] : -1;
			cbDataOwner2.SelectedIndex = (cbDataOwner2.Items.Count > ops[0x07]) ? ops[0x07] : -1;

			this.cbDecimal.Checked = Decimal;
		}

		public Instruction Write(Instruction inst)
		{
			try 
			{
				wrappedByteArray ops = inst.Operands;
				ops[0x06] = (byte)cbDataOwner1.SelectedIndex;
				ops[0x07] = (byte)cbDataOwner2.SelectedIndex;
				ops[0x05] = (byte)cbOperator.SelectedIndex;

				ushort val1 = Decimal ? (ushort)textToShort(tbval1.Text) : textToUShort(tbval1.Text);
				ushort val2 = Decimal ? (ushort)textToShort(tbval2.Text) : textToUShort(tbval2.Text);

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



		private void doDataOwnerSelectedIndexChanged(ComboBox cbDataOwner, ComboBox cbPicker, TextBox tbValue, bool useFlagnames)
		{
			ArrayList pickerNames = null;

			if (useFlagnames)
			{
				pickerNames = WizPrim0x0002.flagNames((byte)cbDataOwner1.SelectedIndex, (ushort)textToUShort(tbval1.Text));
				if (pickerNames != null)
					pickerNames.Insert(0, "[0: invalid]");
			}
			else if (cbDataOwner.SelectedIndex == 0x00 || cbDataOwner.SelectedIndex == 0x01)
			{
				pickerNames = ((BhavWiz)inst).GetAttrNames(Scope.Private);
			}
			else if (cbDataOwner.SelectedIndex == 0x02 || cbDataOwner.SelectedIndex == 0x05)
			{
				pickerNames = ((BhavWiz)inst).GetAttrNames(Scope.SemiGlobal);
			}
			else if (cbDataOwner.SelectedIndex == 0x09 || cbDataOwner.SelectedIndex == 0x16 || cbDataOwner.SelectedIndex == 0x32) // Param
			{
				pickerNames = ((BhavWiz)inst).GetTPRPnames(false);
			}
			else if (cbDataOwner.SelectedIndex == 0x19) // Local
			{
				pickerNames = ((BhavWiz)inst).GetTPRPnames(true);
			}
			else if (BhavWiz.doidGStr[(byte)cbDataOwner.SelectedIndex] != null)
			{
				pickerNames = GS.gStr((GS.BhavStr)BhavWiz.doidGStr[(byte)cbDataOwner.SelectedIndex]);
			}


			cbPicker.Visible = false;
			if (cbDataOwner.SelectedIndex == 0x1a || cbDataOwner.SelectedIndex == 0x2f) // Constant (and Const[Temp])
			{
				ushort val = Decimal ? (ushort)textToShort(tbValue.Text) : textToUShort(tbValue.Text);
				ushort[] vals = pjse.BhavWiz.ExpandBCON(val);
				tbValue.Text = "0x"+SimPe.Helper.HexString(vals[0])+":0x"+SimPe.Helper.HexString((byte)vals[1]);
			}
			else if (pickerNames != null && pickerNames.Count > 0)
			{
				ushort val = Decimal ? (ushort)textToShort(tbValue.Text) : textToUShort(tbValue.Text);
				cbPicker.Visible = true;
				cbPicker.Items.Clear();
				cbPicker.Items.AddRange(pickerNames.ToArray());
				cbPicker.SelectedIndex = (cbPicker.Items.Count > val) ? val : -1;
			}
			else
			{
				tbValue.Text = Decimal ? textToShort(tbValue.Text).ToString() : "0x"+SimPe.Helper.HexString(textToUShort(tbValue.Text));
			}
			tbValue.Visible = !cbPicker.Visible;
		}


		private void doPickerSelectedIndexChanged(ComboBox cbPicker, TextBox tbValue)
		{
			tbValue.Text = Decimal ? cbPicker.SelectedIndex.ToString() : "0x"+SimPe.Helper.HexString((ushort)cbPicker.SelectedIndex);
		}


		private short textToShort(string text)
		{
			short val = 0;
			try 
			{
				if (text.IndexOf(":") == -1)
				{
					val = Convert.ToInt16(text);
				}
				else 
				{
					string[] s = text.Split(":".ToCharArray(), 2);
					ushort[] b = new ushort[2];
					b[0] = Convert.ToUInt16(s[0], 16);
					b[1] = Convert.ToUInt16(s[1], 16);
					val = (short)pjse.BhavWiz.ExpandBCON(b);
				}
			}
			catch (Exception) { }
			return val;
		}

		private ushort textToUShort(string text)
		{
			ushort val = 0;
			try 
			{
				if (text.IndexOf(":") == -1)
				{
					val = Convert.ToUInt16(text, 16);
				}
				else 
				{
					string[] s = text.Split(":".ToCharArray(), 2);
					ushort[] b = new ushort[2];
					b[0] = Convert.ToUInt16(s[0], 16);
					b[1] = Convert.ToUInt16(s[1], 16);
					val = pjse.BhavWiz.ExpandBCON(b);
				}
			}
			catch (Exception) { }
			return val;
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
			this.cbDecimal = new System.Windows.Forms.CheckBox();
			this.pnWiz0x0002.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnWiz0x0002
			// 
			this.pnWiz0x0002.Controls.Add(this.cbDecimal);
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
			this.pnWiz0x0002.Size = new System.Drawing.Size(240, 96);
			this.pnWiz0x0002.TabIndex = 0;
			// 
			// cbPicker2
			// 
			this.cbPicker2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbPicker2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPicker2.DropDownWidth = 384;
			this.cbPicker2.Location = new System.Drawing.Point(128, 50);
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
			this.cbPicker1.DropDownWidth = 384;
			this.cbPicker1.Location = new System.Drawing.Point(128, 0);
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
			this.cbOperator.Size = new System.Drawing.Size(240, 21);
			this.cbOperator.TabIndex = 3;
			// 
			// tbval2
			// 
			this.tbval2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tbval2.Location = new System.Drawing.Point(128, 50);
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
			this.cbDataOwner2.DropDownWidth = 384;
			this.cbDataOwner2.Location = new System.Drawing.Point(0, 50);
			this.cbDataOwner2.Name = "cbDataOwner2";
			this.cbDataOwner2.Size = new System.Drawing.Size(128, 21);
			this.cbDataOwner2.TabIndex = 4;
			this.cbDataOwner2.SelectedIndexChanged += new System.EventHandler(this.cbDataOwner2_SelectedIndexChanged);
			// 
			// tbval1
			// 
			this.tbval1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tbval1.Location = new System.Drawing.Point(128, 0);
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
			this.cbDataOwner1.DropDownWidth = 384;
			this.cbDataOwner1.Location = new System.Drawing.Point(0, 0);
			this.cbDataOwner1.Name = "cbDataOwner1";
			this.cbDataOwner1.Size = new System.Drawing.Size(128, 21);
			this.cbDataOwner1.TabIndex = 1;
			this.cbDataOwner1.SelectedIndexChanged += new System.EventHandler(this.cbDataOwner1_SelectedIndexChanged);
			// 
			// cbDecimal
			// 
			this.cbDecimal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cbDecimal.Location = new System.Drawing.Point(0, 72);
			this.cbDecimal.Name = "cbDecimal";
			this.cbDecimal.Size = new System.Drawing.Size(240, 24);
			this.cbDecimal.TabIndex = 6;
			this.cbDecimal.Text = "Decimal (except Consts)";
			this.cbDecimal.CheckedChanged += new System.EventHandler(this.cbDecimal_CheckedChanged);
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
			doDataOwnerSelectedIndexChanged(cbDataOwner1, cbPicker1, tbval1, false);
			cbDataOwner2_SelectedIndexChanged(null, null);
		}

		private void cbDataOwner2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			doDataOwnerSelectedIndexChanged(cbDataOwner2, cbPicker2, tbval2,
				cbDataOwner2.SelectedIndex == 7 && cbOperator.SelectedIndex >= 8 && cbOperator.SelectedIndex <= 10);
		}


		private void cbPicker1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			doPickerSelectedIndexChanged(cbPicker1, tbval1);
			cbDataOwner2_SelectedIndexChanged(null, null);
		}

		private void cbPicker2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			doPickerSelectedIndexChanged(cbPicker2, tbval2);
		}


		private void cbDecimal_CheckedChanged(object sender, System.EventArgs e)
		{
			if (Decimal = this.cbDecimal.Checked)
			{
				if (cbDataOwner1.SelectedIndex != 0x1a && cbDataOwner1.SelectedIndex != 0x2f)
					tbval1.Text = ((short)textToUShort(tbval1.Text)).ToString();
				if (cbDataOwner2.SelectedIndex != 0x1a && cbDataOwner2.SelectedIndex != 0x2f)
					tbval2.Text = ((short)textToUShort(tbval2.Text)).ToString();
			}
			else
			{
				if (cbDataOwner1.SelectedIndex != 0x1a && cbDataOwner1.SelectedIndex != 0x2f)
					tbval1.Text = "0x"+SimPe.Helper.HexString((ushort)textToShort(tbval1.Text));
				if (cbDataOwner2.SelectedIndex != 0x1a && cbDataOwner2.SelectedIndex != 0x2f)
					tbval2.Text = "0x"+SimPe.Helper.HexString((ushort)textToShort(tbval2.Text));
			}
		}

	}

}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWiz0x0002 : pjse.ABhavOperandWiz
	{
		public BhavOperandWiz0x0002() : base() { }

		public BhavOperandWiz0x0002(Instruction i) : base(i) { }


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

	}

}
