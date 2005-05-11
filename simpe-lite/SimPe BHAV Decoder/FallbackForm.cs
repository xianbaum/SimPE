/***************************************************************************
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

namespace SimPe.Plugin.Decoder
{
	/// <summary>
	/// Zusammenfassung für FallbackForm.
	/// </summary>
	public class FallbackForm : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.Panel pnfallback;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		internal System.Windows.Forms.TextBox tbop1;
		internal System.Windows.Forms.TextBox tbop2;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FallbackForm()
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
			this.pnfallback = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbop1 = new System.Windows.Forms.TextBox();
			this.tbop2 = new System.Windows.Forms.TextBox();
			this.pnfallback.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnfallback
			// 
			this.pnfallback.Controls.Add(this.tbop2);
			this.pnfallback.Controls.Add(this.tbop1);
			this.pnfallback.Controls.Add(this.label2);
			this.pnfallback.Controls.Add(this.label1);
			this.pnfallback.Location = new System.Drawing.Point(8, 8);
			this.pnfallback.Name = "pnfallback";
			this.pnfallback.Size = new System.Drawing.Size(384, 112);
			this.pnfallback.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Operands 1:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(8, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "Operands 2:";
			// 
			// tbop1
			// 
			this.tbop1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbop1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tbop1.Location = new System.Drawing.Point(16, 24);
			this.tbop1.Name = "tbop1";
			this.tbop1.Size = new System.Drawing.Size(360, 20);
			this.tbop1.TabIndex = 2;
			this.tbop1.Text = "";
			this.tbop1.TextChanged += new System.EventHandler(this.ChangeOperands1);
			// 
			// tbop2
			// 
			this.tbop2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbop2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tbop2.Location = new System.Drawing.Point(16, 72);
			this.tbop2.Name = "tbop2";
			this.tbop2.Size = new System.Drawing.Size(360, 20);
			this.tbop2.TabIndex = 3;
			this.tbop2.Text = "";
			this.tbop2.TextChanged += new System.EventHandler(this.ChangeOperands2);
			// 
			// FallbackForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(400, 266);
			this.Controls.Add(this.pnfallback);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "FallbackForm";
			this.Text = "FallbackForm";
			this.pnfallback.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		internal Instruction inst;
		internal DecoderRegistry.ForceUpdate forceupdate;

		private void ChangeOperands1(object sender, System.EventArgs e)
		{
			if (tbop1.Tag!=null) return;
			inst.Parent.Changed = true;
			inst.Operands = Helper.SetLength(Helper.HexListToBytes(tbop1.Text), 8);

			forceupdate(inst);
		}

		private void ChangeOperands2(object sender, System.EventArgs e)
		{
			if (tbop1.Tag!=null) return;
			inst.Parent.Changed = true;
			inst.Operands2 = Helper.SetLength(Helper.HexListToBytes(tbop2.Text), 8);

			forceupdate(inst);
		}
	}
}
