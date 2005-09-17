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
//using pjse.BhavNameWizards;
using pjse.BhavOperandWizards;

namespace pjse.BhavOperandWizards.WizRaw
{
	#region internal form
	/// <summary>
	/// Zusammenfassung für BhavInstruction.
	/// </summary>
	internal class UI : System.Windows.Forms.Form
	{
		#region Form variables
		internal System.Windows.Forms.Panel pnWizRaw;
		private System.Windows.Forms.TextBox tbRaw;
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

		
		#region MyForm
		public Instruction Write(Instruction inst)
		{
			try 
			{
				for (int i = 0; i < 8; i++)
					inst.Operands[i] = Convert.ToByte(tbRaw.Text.Substring(i * 2, 2), 16);
				for (int i = 0; i < 8; i++)
					inst.Reserved1[i] = Convert.ToByte(tbRaw.Text.Substring((i+8) * 2, 2), 16);

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
			string s = "";
			for (int i = 0; i < 8; i++)
				s += SimPe.Helper.HexString(inst.Operands[i]);
			for (int i = 0; i < 8; i++)
				s += SimPe.Helper.HexString(inst.Reserved1[i]);
			tbRaw.Text = s;
		}

		#endregion

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
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
			this.pnWizRaw.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pnWizRaw.Location = new System.Drawing.Point(8, 8);
			this.pnWizRaw.Name = "pnWizRaw";
			this.pnWizRaw.Size = new System.Drawing.Size(264, 24);
			this.pnWizRaw.TabIndex = 0;
			// 
			// tbRaw
			// 
			this.tbRaw.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbRaw.Location = new System.Drawing.Point(0, 0);
			this.tbRaw.MaxLength = 32;
			this.tbRaw.Name = "tbRaw";
			this.tbRaw.Size = new System.Drawing.Size(264, 21);
			this.tbRaw.TabIndex = 0;
			this.tbRaw.Text = "textBox1";
			// 
			// UI
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(640, 366);
			this.Controls.Add(this.pnWizRaw);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "UI";
			this.Text = "UI";
			this.pnWizRaw.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

	}

	#endregion
}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWizRaw : pjse.ABhavOperandWiz
	{
		public BhavOperandWizRaw() : base() { }

		public BhavOperandWizRaw(Instruction i) : base(i) { }


		#region pjse.ABhavOperandWiz
		private WizRaw.UI myForm = null;
		public override Panel bhavPrimWizPanel
		{
			get
			{
				if (myForm == null) myForm = new WizRaw.UI();
				return myForm.pnWizRaw;
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
			if (myForm != null) myForm.Dispose();
		}
		#endregion

		#endregion
	}
}

