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
			TextBox[] tb = { Min, Delta, Type };
			alHex16 = new ArrayList(tb);
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
		private ArrayList alHex16;
		private short[] mv;
		private bool internalchg;

		public void SetData(TtabItem i, int j, int k)
		{
			this.Visible = true;
			mv = new short[3];

			item = i;
			mgNr = j;
			Motive = k;

			Min.Text   = Helper.HexString(mv[0] = i[j, k, 0]);
			Delta.Text = Helper.HexString(mv[1] = i[j, k, 1]);
			Type.Text  = Helper.HexString(mv[2] = i[j, k, 2]);
		}

		public void SetData(TtabItem i, int j) { this.SetData(i, j, motive); }

		public void SetData(TtabItem i) { this.SetData(i, mgNr, motive); }

		public void SetData() { this.SetData(item, mgNr, motive); }


		public void Clear()
		{
			item[mgNr, motive, 0] = 0;
			item[mgNr, motive, 1] = 0;
			item[mgNr, motive, 2] = 0;
			SetData();
		}

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


		private bool hex16_IsValid(object sender)
		{
			if (alHex16.IndexOf(sender) < 0)
				throw new Exception("hex16_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToInt16(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
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
			this.Min.MaxLength = 4;
			this.Min.Name = "Min";
			this.Min.Size = new System.Drawing.Size(40, 20);
			this.Min.TabIndex = 1;
			this.Min.Text = "DDDD";
			this.Min.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.Min.Validated += new System.EventHandler(this.hex16_Validated);
			this.Min.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			// 
			// Delta
			// 
			this.Delta.Location = new System.Drawing.Point(44, 0);
			this.Delta.MaxLength = 4;
			this.Delta.Name = "Delta";
			this.Delta.Size = new System.Drawing.Size(40, 20);
			this.Delta.TabIndex = 2;
			this.Delta.Text = "DDDD";
			this.Delta.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.Delta.Validated += new System.EventHandler(this.hex16_Validated);
			this.Delta.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			// 
			// Type
			// 
			this.Type.Location = new System.Drawing.Point(88, 0);
			this.Type.MaxLength = 4;
			this.Type.Name = "Type";
			this.Type.Size = new System.Drawing.Size(40, 20);
			this.Type.TabIndex = 3;
			this.Type.Text = "DDDD";
			this.Type.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.Type.Validated += new System.EventHandler(this.hex16_Validated);
			this.Type.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			// 
			// TtabSingleMotiveUI
			// 
			this.Controls.Add(this.Min);
			this.Controls.Add(this.Delta);
			this.Controls.Add(this.Type);
			this.Name = "TtabSingleMotiveUI";
			this.Size = new System.Drawing.Size(128, 24);
			this.ResumeLayout(false);

		}
		#endregion

		private void hex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex16_IsValid(sender)) return;

			internalchg = true;
			int i = alHex16.IndexOf(sender);
			item[mgNr, motive, alHex16.IndexOf(sender)] = Convert.ToInt16(((TextBox)sender).Text, 16);
			internalchg = false;
		}

		private void hex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex16_IsValid(sender)) return;

			e.Cancel = true;

			short val = 0;
			int i = alHex16.IndexOf(sender);
			item[mgNr, motive, i] = val = mv[i];

			internalchg = true;
			((TextBox)sender).Text = Helper.HexString(val);
			((TextBox)sender).SelectAll();
			internalchg = false;
		}

		private void hex16_Validated(object sender, System.EventArgs ev)
		{
			((TextBox)sender).Text = Helper.HexString(item[mgNr, motive, alHex16.IndexOf(sender)]);
			((TextBox)sender).SelectAll();
		}

	}
}
