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
using pjse.BhavNameWizards;

namespace pjse.BhavOperandWizards.Wiz0x0001
{
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

		private string genericSimsCallparamText(int i)
		{
			if (i >= WizPrim0x0001.parms.Length) return "[UNK args]";
			else if (WizPrim0x0001.parms[i].Trim().Length == 0) return "no args";
			else return WizPrim0x0001.parms[i].Trim();
		}


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
		public BhavOperandWiz0x0001() : base() { }

		public BhavOperandWiz0x0001(Instruction i) : base(i) { }


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

	}

}
