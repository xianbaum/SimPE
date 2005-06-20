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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for TtabSingleMotive.
	/// </summary>
	public class TtabSingleMotiveUI : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.TextBox Min;
		private System.Windows.Forms.TextBox Delta;
		private System.Windows.Forms.TextBox Type;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TtabSingleMotiveUI()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			Min.Top = Delta.Top = Type.Top = Min.Left = 0;
			TtabSingleMotiveUI_Resize(null, null);
		}

		/// <summary> 
		/// Clean up any resources being used.
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


		private bool internalchg = false;
		private TtabItem item = null;
		private int mgNr;
		private int motive;

		public void SetData(TtabItem i, int j, int k)
		{
			item = i;
			mgNr = j;
			motive = k;

			internalchg = true;
			Min.Text   = i[j, k, 0].ToString("X");
			Delta.Text = i[j, k, 1].ToString("X");
			Type.Text  = i[j, k, 2].ToString("X");
			internalchg = false;
		}


		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Min = new System.Windows.Forms.TextBox();
			this.Delta = new System.Windows.Forms.TextBox();
			this.Type = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// Min
			// 
			this.Min.Location = new System.Drawing.Point(0, 0);
			this.Min.Name = "Min";
			this.Min.Size = new System.Drawing.Size(24, 20);
			this.Min.TabIndex = 1;
			this.Min.Text = "DD";
			this.Min.TextChanged += new System.EventHandler(this.tbTextChanged);
			// 
			// Delta
			// 
			this.Delta.Location = new System.Drawing.Point(24, 0);
			this.Delta.Name = "Delta";
			this.Delta.Size = new System.Drawing.Size(24, 20);
			this.Delta.TabIndex = 2;
			this.Delta.Text = "DD";
			this.Delta.TextChanged += new System.EventHandler(this.tbTextChanged);
			// 
			// Type
			// 
			this.Type.Location = new System.Drawing.Point(48, 0);
			this.Type.Name = "Type";
			this.Type.Size = new System.Drawing.Size(24, 20);
			this.Type.TabIndex = 3;
			this.Type.Text = "DD";
			this.Type.TextChanged += new System.EventHandler(this.tbTextChanged);
			// 
			// TtabSingleMotiveUI
			// 
			this.Controls.Add(this.Min);
			this.Controls.Add(this.Delta);
			this.Controls.Add(this.Type);
			this.Name = "TtabSingleMotiveUI";
			this.Size = new System.Drawing.Size(72, 24);
			this.Resize += new System.EventHandler(this.TtabSingleMotiveUI_Resize);
			this.ResumeLayout(false);

		}
		#endregion

		private void TtabSingleMotiveUI_Resize(object sender, System.EventArgs e)
		{
			Min.Height = Delta.Height = Type.Height = this.Height - 1;
			Min.Width = Delta.Width = Type.Width = (this.Width - 1 - 3) / 3;
			Delta.Left = Min.Right + 1;
			Type.Left = Delta.Right + 1;
		}

		private void tbTextChanged(object sender, System.EventArgs e)
		{
			if (!(sender is TextBox)) return;
			if (internalchg) return;

			short val = 0;
			TextBox tb = (TextBox)sender;

			if (tb.Text != "")
			{
				try
				{ val = Convert.ToInt16(tb.Text, 10); }
				catch { return; }
			}

			TextBox[] poss = { Min, Delta, Type };
			for (int i = 0; i < poss.Length; i++)
				if (poss[i] == tb)
					item[mgNr, motive, i] = val; 
		}
	}
}
