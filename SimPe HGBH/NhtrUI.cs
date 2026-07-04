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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces;
using SimPe.PackedFiles.Wrapper.Supporting;
using SimPe.Data;
using SimPe.Windows.Forms;
using SimPe.PackedFiles.Wrapper;
using System.Media;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for NhtrUI.
	/// </summary>
	public class NhtrUI : 
		SimPe.Windows.Forms.WrapperBaseControl, SimPe.Interfaces.Plugin.IPackedFileUI
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NhtrUI()
		{			
			// Required designer variable.
            InitializeComponent();
            if ((byte)Helper.WindowsRegistry.LanguageCode == 1)
                this.HeaderText = "Neighborhood Terrain";
            if (booby.ThemeManager.ThemedForms)
            {
                booby.ThemeManager tm = booby.ThemeManager.Global.CreateChild();
                tm.AddControl(this.tb);
                tm.AddControl(this.lb);
                tm.AddControl(this.pg);
                tm.AddControl(this.btgoned);
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
            this.lb = new System.Windows.Forms.ListBox();
            this.tb = new System.Windows.Forms.TextBox();
            this.cb = new System.Windows.Forms.ComboBox();
            this.btgoned = new System.Windows.Forms.Button();
            this.pg = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
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
            this.lb.Location = new System.Drawing.Point(8, 56);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(692, 60);
            this.lb.TabIndex = 1;
            this.lb.SelectedIndexChanged += new System.EventHandler(this.lb_SelectedIndexChanged);
            // 
            // tb
            // 
            this.tb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tb.Font = new System.Drawing.Font("Courier New", 11.25F);
            this.tb.Location = new System.Drawing.Point(8, 122);
            this.tb.Multiline = true;
            this.tb.Name = "tb";
            this.tb.ReadOnly = true;
            this.tb.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb.Size = new System.Drawing.Size(564, 275);
            this.tb.TabIndex = 2;
            this.tb.Text = "00 00 00 00  00 00 00 00  00 00 00 00  00 00 00 00";
            // 
            // cb
            // 
            this.cb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb.ItemHeight = 13;
            this.cb.Location = new System.Drawing.Point(8, 30);
            this.cb.Name = "cb";
            this.cb.Size = new System.Drawing.Size(552, 21);
            this.cb.TabIndex = 3;
            this.cb.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btgoned
            // 
            this.btgoned.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btgoned.Location = new System.Drawing.Point(566, 29);
            this.btgoned.Name = "btgoned";
            this.btgoned.Size = new System.Drawing.Size(134, 23);
            this.btgoned.TabIndex = 4;
            this.btgoned.Text = "Delete All";
            this.btgoned.UseVisualStyleBackColor = true;
            this.btgoned.Click += new System.EventHandler(this.btgoned_Click);
            // 
            // pg
            // 
            this.pg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pg.BackColor = System.Drawing.SystemColors.Control;
            this.pg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.pg.HelpVisible = false;
            this.pg.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.pg.Location = new System.Drawing.Point(578, 122);
            this.pg.Name = "pg";
            this.pg.Size = new System.Drawing.Size(122, 275);
            this.pg.TabIndex = 4;
            this.pg.ToolbarVisible = false;
            // 
            // NhtrUI
            // 
            this.ShowLogo = false;
            this.Controls.Add(this.btgoned);
            this.Controls.Add(this.cb);
            this.Controls.Add(this.pg);
            this.Controls.Add(this.lb);
            this.Controls.Add(this.tb);
            this.Enabled = false;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.HeaderText = "Neighbourhood Terrain";
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "NhtrUI";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Size = new System.Drawing.Size(708, 400);
            this.Controls.SetChildIndex(this.tb, 0);
            this.Controls.SetChildIndex(this.lb, 0);
            this.Controls.SetChildIndex(this.pg, 0);
            this.Controls.SetChildIndex(this.cb, 0);
            this.Controls.SetChildIndex(this.btgoned, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private System.Windows.Forms.ListBox lb;
		private System.Windows.Forms.TextBox tb;
        private System.Windows.Forms.ComboBox cb;
        private Button btgoned;
        private SoundPlayer ooh = new SoundPlayer(booby.NoisyGirls.ooh);
        private PropertyGrid pg;

		public Nhtr Nhtr
		{
			get { return (Nhtr)Wrapper; }
		}

		bool intern;
		protected override void RefreshGUI()
		{			
			if (intern) return;			
			intern = true;
			lb.Items.Clear();
			cb.Items.Clear();
			if (Nhtr!=null) 
			{
				foreach (NhtrList list in Nhtr.Items)				
					SimPe.CountedListItem.Add(cb, list);				
				if (cb.Items.Count>0) cb.SelectedIndex = 0;
				lb.Enabled = true;
				this.Enabled = true;
			} 
			else 
			{				
			}
			intern=false;
		}

		public override void OnCommit()
		{
			Nhtr.SynchronizeUserData(true, false);
		}

		private void lb_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lb.SelectedItem==null) 
			{
				tb.Text = "";
				pg.SelectedObject = null;
			}
			else 
			{
				tb.Text = ((lb.SelectedItem as CountedListItem ).Object as NhtrItem).ToLongString();
				pg.SelectedObject = ((lb.SelectedItem as CountedListItem ).Object as NhtrItem);
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			lb.Items.Clear();
            if (cb.SelectedItem == null)
            {
                btgoned.Text = "Delete All";
                btgoned.Enabled = false;
                return;
            }
			
			NhtrList list = (cb.SelectedItem as CountedListItem).Object as NhtrList;
			foreach (NhtrItem i in list)				
				SimPe.CountedListItem.Add(lb, i);
            btgoned.Enabled = list.Count > 0;
            btgoned.Text = "Delete All " + (NhtrListType)cb.SelectedIndex;
		}

        private void btgoned_Click(object sender, EventArgs e)
        {
            foreach (NhtrList list in Nhtr.Items)
            {
                if (list == (cb.SelectedItem as CountedListItem).Object as NhtrList)
                    list.Clear();
            }
            tb.Text = "Only Commit if this is what you Really, Really Want!";
            pg.SelectedObject = null;
            if (booby.PrettyGirls.PervyMode)
                ooh.Play();
            RefreshGUI();
        }
	}
}
