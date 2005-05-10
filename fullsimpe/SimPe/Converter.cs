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

namespace SimPe
{
	/// <summary>
	/// Zusammenfassung für Converter.
	/// </summary>
	public class Converter : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbhex;
		private System.Windows.Forms.TextBox tbdec;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Converter()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Converter));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbhex = new System.Windows.Forms.TextBox();
			this.tbdec = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Hexadecimal:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(38, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "Decimal:";
			// 
			// tbhex
			// 
			this.tbhex.Location = new System.Drawing.Point(104, 8);
			this.tbhex.Name = "tbhex";
			this.tbhex.Size = new System.Drawing.Size(128, 21);
			this.tbhex.TabIndex = 2;
			this.tbhex.Text = "0x00";
			this.tbhex.TextChanged += new System.EventHandler(this.HexChanged);
			// 
			// tbdec
			// 
			this.tbdec.Location = new System.Drawing.Point(104, 32);
			this.tbdec.Name = "tbdec";
			this.tbdec.Size = new System.Drawing.Size(128, 21);
			this.tbdec.TabIndex = 3;
			this.tbdec.Text = "0";
			this.tbdec.TextChanged += new System.EventHandler(this.DecChanged);
			// 
			// Converter
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(242, 72);
			this.Controls.Add(this.tbdec);
			this.Controls.Add(this.tbhex);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Converter";
			this.Text = "Converter";
			this.ResumeLayout(false);

		}
		#endregion

		bool sysupdate = false;
		private void HexChanged(object sender, System.EventArgs e)
		{
			if (sysupdate) return;
			sysupdate = true;
			try 
			{
				tbdec.Text = Convert.ToInt64(tbhex.Text, 16).ToString();
			} 
			catch (Exception) {}
			sysupdate = false;
		}

		private void DecChanged(object sender, System.EventArgs e)
		{
			if (sysupdate) return;
			sysupdate = true;
			try 
			{
				tbhex.Text = "0x"+Helper.HexString((ulong)Convert.ToInt64(tbdec.Text));
			} 
			catch (Exception) {}
			sysupdate = false;
		}
	}
}
