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
		private System.Windows.Forms.CheckBox cbAttrPicker;
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

			inst = null;
		}


		#region UI
		private Instruction inst = null;
		private DataOwnerControl doid1 = null;
		private DataOwnerControl doid2 = null;

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

		private bool AttrPicker
		{
			get
			{
				SimPe.XmlRegistryKey  rkf = SimPe.Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey("PJSE\\Bhav\\OperandWiz0x02");
				object o = rkf.GetValue("attrPicker", true);
				return Convert.ToBoolean(o);
			}

			set
			{
				SimPe.XmlRegistryKey rkf = SimPe.Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey("PJSE\\Bhav\\OperandWiz0x02");
				rkf.SetValue("attrPicker", value);
			}

		}


		public void Execute(Instruction inst)
		{
			this.inst = inst;

			wrappedByteArray ops = inst.Operands;

			doid1 = new DataOwnerControl(inst, this.cbDataOwner1, this.cbPicker1, this.tbval1, ops[0x06], (ushort)((ops[0x01] << 8) | ops[0x00]));
			doid2 = new DataOwnerControl(inst, this.cbDataOwner2, this.cbPicker2, this.tbval2, ops[0x07], (ushort)((ops[0x03] << 8) | ops[0x02]));
			doid2.FlagsFor = doid1;
			doid1.SetListener(doid2);

			doid1.Decimal = doid2.Decimal = this.cbDecimal.Checked = Decimal;
			doid1.UseAttrPicker = doid2.UseAttrPicker = this.cbAttrPicker.Checked = AttrPicker;

			cbOperator.Items.Clear();
			cbOperator.Items.AddRange(BhavWiz.readStr(GS.BhavStr.Operators).ToArray());
			cbOperator.SelectedIndex = (cbOperator.Items.Count > ops[0x05]) ? ops[0x05] : -1;
		}

		public Instruction Write(Instruction inst)
		{
			if (inst != null)
			{
				wrappedByteArray ops = inst.Operands;
				ops[0x06] = doid1.DataOwner;
				ops[0x00] = (byte)(doid1.Value & 0xff);
				ops[0x01] = (byte)((doid1.Value >> 8) & 0xff);
				ops[0x05] = (byte)cbOperator.SelectedIndex;
				ops[0x07] = doid2.DataOwner;
				ops[0x02] = (byte)(doid2.Value & 0xff);
				ops[0x03] = (byte)((doid2.Value >> 8) & 0xff);
			}
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
			this.pnWiz0x0002 = new System.Windows.Forms.Panel();
			this.cbAttrPicker = new System.Windows.Forms.CheckBox();
			this.cbDecimal = new System.Windows.Forms.CheckBox();
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
			this.pnWiz0x0002.Controls.Add(this.cbAttrPicker);
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
			this.pnWiz0x0002.Size = new System.Drawing.Size(240, 120);
			this.pnWiz0x0002.TabIndex = 0;
			// 
			// cbAttrPicker
			// 
			this.cbAttrPicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cbAttrPicker.Location = new System.Drawing.Point(0, 96);
			this.cbAttrPicker.Name = "cbAttrPicker";
			this.cbAttrPicker.Size = new System.Drawing.Size(240, 24);
			this.cbAttrPicker.TabIndex = 7;
			this.cbAttrPicker.Text = "use Attribute picker";
			this.cbAttrPicker.CheckedChanged += new System.EventHandler(this.cbAttrPicker_CheckedChanged);
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
			this.cbOperator.SelectedIndexChanged += new System.EventHandler(this.cbOperator_SelectedIndexChanged);
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

		private void cbOperator_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cbOperator.SelectedIndex >= 8 && cbOperator.SelectedIndex <= 10)
				doid2.UseFlagNames = true;
			else
				doid2.UseFlagNames = false;
		}

		private void cbDecimal_CheckedChanged(object sender, System.EventArgs e)
		{
			doid1.Decimal = doid2.Decimal = Decimal = this.cbDecimal.Checked;
		}

		private void cbAttrPicker_CheckedChanged(object sender, System.EventArgs e)
		{
			doid1.UseAttrPicker = doid2.UseAttrPicker = AttrPicker = this.cbAttrPicker.Checked;
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
