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
using SimPe.Interfaces.Plugin;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace SimPe.Plugin 
{
	/// <summary>
	/// Summary description for TattUI.
	/// </summary>
	public class TattUI : 
		SimPe.Windows.Forms.WrapperBaseControl
		//System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbVer;
		private System.Windows.Forms.TextBox tbRes;
		private System.Windows.Forms.TextBox tbFlname;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListBox lb;
        private Button btmode;
        private System.ComponentModel.Container components = null;
        private bool mode = false;

		public TattUI()
		{
            // Required designer variable.
			InitializeComponent();
            if (booby.ThemeManager.ThemedForms) { booby.ThemeManager.Global.AddControl(this.lb); booby.ThemeManager.Global.AddControl(this.btmode); }
            if (Helper.WindowsRegistry.UseBigIcons)
            {
                this.lb.Font = new System.Drawing.Font("Courier New", 12F);
                this.tbFlname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            }
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

		#region Windows Form Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbVer = new System.Windows.Forms.TextBox();
            this.tbRes = new System.Windows.Forms.TextBox();
            this.tbFlname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lb = new System.Windows.Forms.ListBox();
            this.btmode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(8, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "FileName:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(17, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Version:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(180, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Reserved:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbVer
            // 
            this.tbVer.Location = new System.Drawing.Point(75, 110);
            this.tbVer.Name = "tbVer";
            this.tbVer.Size = new System.Drawing.Size(88, 20);
            this.tbVer.TabIndex = 4;
            this.tbVer.Text = "0x00000000";
            // 
            // tbRes
            // 
            this.tbRes.Location = new System.Drawing.Point(247, 110);
            this.tbRes.Name = "tbRes";
            this.tbRes.Size = new System.Drawing.Size(88, 20);
            this.tbRes.TabIndex = 5;
            this.tbRes.Text = "0x00000000";
            // 
            // tbFlname
            // 
            this.tbFlname.Location = new System.Drawing.Point(75, 83);
            this.tbFlname.Name = "tbFlname";
            this.tbFlname.Size = new System.Drawing.Size(693, 20);
            this.tbFlname.TabIndex = 6;
            this.tbFlname.TextChanged += new System.EventHandler(this.tbFlname_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(25, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Items:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb
            // 
            this.lb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lb.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.lb.HorizontalScrollbar = true;
            this.lb.IntegralHeight = false;
            this.lb.ItemHeight = 14;
            this.lb.Location = new System.Drawing.Point(75, 138);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(693, 270);
            this.lb.TabIndex = 8;
            // 
            // btmode
            // 
            this.btmode.Location = new System.Drawing.Point(399, 109);
            this.btmode.Name = "btmode";
            this.btmode.Size = new System.Drawing.Size(98, 23);
            this.btmode.TabIndex = 9;
            this.btmode.Text = "Show Names";
            this.btmode.UseVisualStyleBackColor = true;
            this.btmode.Click += new System.EventHandler(this.btmode_Click);
            // 
            // TattUI
            // 
            this.Controls.Add(this.btmode);
            this.Controls.Add(this.lb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbFlname);
            this.Controls.Add(this.tbRes);
            this.Controls.Add(this.tbVer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.HeaderText = "TTAT Wrapper";
            this.Name = "TattUI";
            this.ShowLogo = true;
            this.Commited += new System.EventHandler(this.TattUI_Commited);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.tbVer, 0);
            this.Controls.SetChildIndex(this.tbRes, 0);
            this.Controls.SetChildIndex(this.tbFlname, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.lb, 0);
            this.Controls.SetChildIndex(this.btmode, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public Tatt Tatt 
		{
			get { return (Tatt)Wrapper ; }
		}

		//bool inter;
		protected override void RefreshGUI()
		{
			base.RefreshGUI ();

			this.tbFlname.Text =  Tatt.FileName;
			this.tbRes.Text = "0x"+Helper.HexString(Tatt.Reserved);
			this.tbVer.Text = "0x"+Helper.HexString(Tatt.Version);

			this.lb.Items.Clear();
			foreach (TattItem ti in Tatt)
                this.lb.Items.Add(ti);
		}

		private void TattUI_Commited(object sender, System.EventArgs e)
		{
			Tatt.SynchronizeUserData();
		}

		private void tbFlname_TextChanged(object sender, System.EventArgs e)
		{
			Tatt.FileName = tbFlname.Text;
			Tatt.Reserved = Helper.StringToUInt32(tbRes.Text, Tatt.Reserved, 16);
			Tatt.Version = Helper.StringToUInt32(tbVer.Text, Tatt.Version, 16);

			Tatt.Changed = true;
		}

        private void btmode_Click(object sender, EventArgs e)
        {
            this.lb.Items.Clear();
            mode = !mode;
            if (mode)
            {
                foreach (TattItem ti in Tatt)
                {
                    string s = Subhoods.getgooee(ti.GuiD);
                    if (s == "") s = SimPe.Localization.GetString("Unknown");
                    s += ": (0x" + Helper.HexString(ti.GuiD) + ")";
                    this.lb.Items.Add(s);
                }
                this.lb.Sorted = true;
                this.btmode.Text = "Show Raw";
            }
            else
            {
                foreach (TattItem ti in Tatt)
                    this.lb.Items.Add(ti);
                this.btmode.Text = "Show Names";
            }
        }

	}
}
