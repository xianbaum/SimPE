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
	/// <summary>
	/// Zusammenfassung für BhavWizardForm.
	/// </summary>
	public class BhavOperandWiz : System.Windows.Forms.Form
	{
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Panel panel1;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public BhavOperandWiz()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();

			//
			// TODO: Fügen Sie den Konstruktorcode nach dem Aufruf von InitializeComponent hinzu
			//
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

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// linkLabel1
			// 
			this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel1.Location = new System.Drawing.Point(272, 128);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(23, 17);
			this.linkLabel1.TabIndex = 0;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "OK";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OK);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Location = new System.Drawing.Point(0, 120);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(320, 1);
			this.panel1.TabIndex = 1;
			// 
			// BhavOperandWiz
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(314, 151);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.linkLabel1);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "BhavOperandWiz";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Instruction Wizard";
			this.ResumeLayout(false);

		}
		#endregion

		public static bool Available(Instruction i)
		{
			return (pjse.BhavPrimWizProvider.ForOpCode(i.Opcode) != null);
		}

		public static string OpcodeName(Bhav parent, ushort opcode, byte[] operands)
		{
			pjse.ABhavPrimWiz wiz = pjse.BhavPrimWizProvider.ForOpCode(opcode);
			if (wiz != null) return wiz.OpcodeName(parent, opcode, operands);

			string add="";
			if (parent==null) 
			{
				if (add.Trim()!="") return "0x"+Helper.HexString(opcode)+ " ("+add+")";
				else return "0x"+Helper.HexString(opcode);
			} 
			else 
			{				
				string name = "";

				if (((opcode>=0x1000) && (opcode<0x2000))) 
				{
					name = "[private] "+(new OpCode(parent, opcode)).LoadBHAV().FileName;
				}
				else if (opcode>=0x2000) 
				{
					Bhav b = (new OpCode(parent, opcode)).LoadBHAV();
					name = "[semiglobal] ";
					if (b!=null) name += b.FileName;
					else name += Localization.Manager.GetString("Unknown");
				}
				else 
				{
					name = parent.Opcodes.FindName((ushort)(opcode));
				}
				if (add.Trim()!="")	return name+" (0x"+Helper.HexString(opcode)+", "+add+")";
				else return name+" (0x"+Helper.HexString(opcode)+")";
			}
		}

		public static string OpcodeName(Instruction i) { return OpcodeName(i.Parent, i.Opcode, i.Operands); }

		
		public Instruction Execute(Instruction i)
		{
			pjse.ABhavPrimWiz wiz = pjse.BhavPrimWizProvider.ForOpCode(i.Opcode);
			if (wiz == null) return null;

			byte[] operands = new byte[16];
			i.Operands.CopyTo(operands, 0);
			i.Reserved1.CopyTo(operands, 8);

			Panel pn = wiz.bhavPrimWizPanel();
			pn.Parent = this;
			pn.Top = 0;
			pn.Left = 0;
			int headHeight = this.Height - this.DisplayRectangle.Height;
			this.Width = pn.Width + 8;
			this.Height = pn.Height + headHeight + 24;
			wiz.Execute(i);

			this.DialogResult = DialogResult.Cancel;
			switch (ShowDialog())
			{
				case DialogResult.Yes:
				case DialogResult.OK:
					return wiz.Write();
				default:
					return null;
			}
		}

		private void OK(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			Close();
		}
	}
}
