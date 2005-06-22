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
			Min.Tag = (Int32) 0;
			Delta.Tag = (Int32) 1;
			Type.Tag = (Int32) 2;
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


		#region TtabSingleMotiveUI
		private TtabItem item = null;
		private int mgNr;
		private int motive;

		public void SetData(TtabItem i, int j, int k)
		{
			this.Visible = true;

			item = i;
			mgNr = j;
			motive = k;

			Min.Text   = i[j, k, 0].ToString("X");
			Delta.Text = i[j, k, 1].ToString("X");
			Type.Text  = i[j, k, 2].ToString("X");
		}

		public void SetData(TtabItem i, int j) { this.SetData(i, j, motive); }

		public void SetData(TtabItem i) { this.SetData(i, mgNr, motive); }

		public void SetData() { this.SetData(item, mgNr, motive); }


		/// <summary>
		/// Which of the sixteen motives the control is editing (0-15)
		/// </summary>
		public int Motive
		{
			get { return motive; }
			set
			{
				if (value < 0 || value > 15)
					throw new Exception("Motive must be in range 0 to 15");

				motive = value;
			}
		}

		#endregion

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
			this.Min.MaxLength = 2;
			this.Min.Name = "Min";
			this.Min.Size = new System.Drawing.Size(24, 20);
			this.Min.TabIndex = 1;
			this.Min.Text = "DD";
			this.Min.Validating += new System.ComponentModel.CancelEventHandler(this.tbValidating);
			this.Min.Validated += new System.EventHandler(this.tbValidated);
			// 
			// Delta
			// 
			this.Delta.Location = new System.Drawing.Point(24, 0);
			this.Delta.MaxLength = 2;
			this.Delta.Name = "Delta";
			this.Delta.Size = new System.Drawing.Size(24, 20);
			this.Delta.TabIndex = 2;
			this.Delta.Text = "DD";
			this.Delta.Validating += new System.ComponentModel.CancelEventHandler(this.tbValidating);
			this.Delta.Validated += new System.EventHandler(this.tbValidated);
			// 
			// Type
			// 
			this.Type.Location = new System.Drawing.Point(48, 0);
			this.Type.MaxLength = 2;
			this.Type.Name = "Type";
			this.Type.Size = new System.Drawing.Size(24, 20);
			this.Type.TabIndex = 3;
			this.Type.Text = "DD";
			this.Type.Validating += new System.ComponentModel.CancelEventHandler(this.tbValidating);
			this.Type.Validated += new System.EventHandler(this.tbValidated);
			// 
			// TtabSingleMotiveUI
			// 
			this.Controls.Add(this.Min);
			this.Controls.Add(this.Delta);
			this.Controls.Add(this.Type);
			this.Name = "TtabSingleMotiveUI";
			this.Size = new System.Drawing.Size(72, 24);
			this.ResumeLayout(false);

		}
		#endregion

		private void tbValidated(object sender, System.EventArgs e)
		{
			TextBox tb = (TextBox)sender;
			short val = Convert.ToInt16(tb.Text, 16);
			item[mgNr, motive, (Int32)tb.Tag] = val;
		}

		private void tbValidating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try { TextBox tb = (TextBox)sender; if (tb.Text != "") Convert.ToInt16(tb.Text, 16); }
			catch { e.Cancel = true; }
			return;
		}
	}
}
