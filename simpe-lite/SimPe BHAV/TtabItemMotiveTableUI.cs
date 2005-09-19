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
	/// Summary description for TtabItemMotiveTableUI.
	/// </summary>
	public class TtabItemMotiveTableUI : System.Windows.Forms.UserControl
	{
		#region Form variables
		private SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI ttabMotiveGroupUI1;
		private System.Windows.Forms.Label lbMotive0;
		private System.Windows.Forms.Label lbMotive1;
		private System.Windows.Forms.Label lbMotive2;
		private System.Windows.Forms.Label lbMotive3;
		private System.Windows.Forms.Label lbMotive4;
		private System.Windows.Forms.Label lbMotive5;
		private System.Windows.Forms.Label lbMotive6;
		private System.Windows.Forms.Label lbMotive7;
		private System.Windows.Forms.Label lbMotive9;
		private System.Windows.Forms.Label lbMotive11;
		private System.Windows.Forms.Label lbMotive8;
		private System.Windows.Forms.Label lbMotive10;
		private System.Windows.Forms.Label lbMotive14;
		private System.Windows.Forms.Label lbMotive15;
		private System.Windows.Forms.Label lbMotive13;
		private System.Windows.Forms.Label lbMotive12;
		private System.Windows.Forms.Panel pnAllGroups;
		private SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI ttabMotiveGroupUI2;
		private SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI ttabMotiveGroupUI3;
		private SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI ttabMotiveGroupUI4;
		private SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI ttabMotiveGroupUI5;
		private SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI ttabMotiveGroupUI6;
		private SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI ttabMotiveGroupUI7;
		private System.Windows.Forms.CheckBox cbShowAll;
		private System.Windows.Forms.Panel pnCopyButtons;
		private System.Windows.Forms.Button btnCpyM0;
		private System.Windows.Forms.Button btnCpyM1;
		private System.Windows.Forms.Button btnCpyM2;
		private System.Windows.Forms.Button btnCpyM3;
		private System.Windows.Forms.Button btnCpyM4;
		private System.Windows.Forms.Button btnCpyM5;
		private System.Windows.Forms.Button btnCpyM7;
		private System.Windows.Forms.Button btnCpyM6;
		private System.Windows.Forms.Button btnCpyM9;
		private System.Windows.Forms.Button btnCpyM12;
		private System.Windows.Forms.Button btnCpyM11;
		private System.Windows.Forms.Button btnCpyM10;
		private System.Windows.Forms.Button btnCpyM15;
		private System.Windows.Forms.Button btnCpyM14;
		private System.Windows.Forms.Button btnCpyM13;
		private System.Windows.Forms.Button btnCpyM8;
		private System.Windows.Forms.Label lbCBM0;
		private System.Windows.Forms.Label lbCBM1;
		private System.Windows.Forms.Label lbCBM2;
		private System.Windows.Forms.Label lbCBM3;
		private System.Windows.Forms.Label lbCBM4;
		private System.Windows.Forms.Label lbCBM5;
		private System.Windows.Forms.Label lbCBM6;
		private System.Windows.Forms.Label lbCBM7;
		private System.Windows.Forms.Label lbCBM15;
		private System.Windows.Forms.Label lbCBM11;
		private System.Windows.Forms.Label lbCBM14;
		private System.Windows.Forms.Label lbCBM8;
		private System.Windows.Forms.Label lbCBM9;
		private System.Windows.Forms.Label lbCBM13;
		private System.Windows.Forms.Label lbCBM10;
		private System.Windows.Forms.Label lbCBM12;
		private System.Windows.Forms.Button btnCopyAll;
		private System.Windows.Forms.Button btnHelp;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public TtabItemMotiveTableUI()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			Label[] l = {
							lbMotive0 ,lbMotive1 ,lbMotive2  ,lbMotive3  ,lbMotive4  ,lbMotive5  ,lbMotive6  ,lbMotive7
							,lbMotive8 ,lbMotive9 ,lbMotive10 ,lbMotive11 ,lbMotive12 ,lbMotive13 ,lbMotive14 ,lbMotive15
						};
			aMotiveLabels = l;

			TtabMotiveGroupUI[] t = {
										ttabMotiveGroupUI1  ,ttabMotiveGroupUI2  ,ttabMotiveGroupUI3
										,ttabMotiveGroupUI4  ,ttabMotiveGroupUI5  ,ttabMotiveGroupUI6  ,ttabMotiveGroupUI7
									};
			aMotiveGroups = t;
			Button[] b = {
							 btnCpyM0  ,btnCpyM1  ,btnCpyM2  ,btnCpyM3
							,btnCpyM4  ,btnCpyM5  ,btnCpyM6  ,btnCpyM7
							,btnCpyM8  ,btnCpyM9  ,btnCpyM10 ,btnCpyM11
							,btnCpyM12 ,btnCpyM13 ,btnCpyM14 ,btnCpyM15
							};
			aButtons = b;
			pnAllGroups.Visible = true;
			pnCopyButtons.Visible = false;

			pnCopyButtons.Anchor = pnAllGroups.Anchor = AnchorStyles.Top | AnchorStyles.Left;
			pnCopyButtons.Left = pnAllGroups.Left = ttabMotiveGroupUI1.Right + 1;
			int i = this.Width - (pnAllGroups.Left + 8);
			if (i > ttabMotiveGroupUI2.Width)
				pnAllGroups.Width = i;
			else
				pnAllGroups.Width = ttabMotiveGroupUI2.Width;
			int j = pnAllGroups.Right;
			int k = this.Right;
			pnCopyButtons.Width = pnAllGroups.Width;
			pnCopyButtons.Anchor = pnAllGroups.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			pnCopyButtons.AutoScroll = pnAllGroups.AutoScroll = true;
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


		#region TtabItemMotiveTableUI
		private TtabItem item = null;
		private Label[] aMotiveLabels;
		private TtabMotiveGroupUI[] aMotiveGroups;
		private Button[] aButtons;

		private void doCopyMotive(int m)
		{
			for (int mg = 1; mg < item.nrGroups; mg++)
			{
				item[mg, m, 0] = item[0, m, 0];
				item[mg, m, 1] = item[0, m, 1];
				item[mg, m, 2] = item[0, m, 2];
			}
			SetData();
		}


		public void SetData(TtabItem i)
		{
			item = i;

			for (ushort m = 0; m < aMotiveLabels.Length; m++)
			{
				aMotiveLabels[m].Text = pjse.GS.MotiveName(m);
				aMotiveLabels[m].Left = ttabMotiveGroupUI1.Left - aMotiveLabels[m].Width - 4;
			}

			if (item.nrGroups > 1)
			{
				cbShowAll.Enabled = true;
				for (int m = 0; m < aMotiveGroups.Length; m++) 
					aMotiveGroups[m].SetData(item, m);
			}
			else
			{
				cbShowAll.Enabled = false;
				ttabMotiveGroupUI1.SetData(item, (cbShowAll.Enabled) ? -1 : 0);
			}
			cbShowAll_CheckedChanged(null, null);
		}

		public void SetData() { this.SetData(item); }


		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TtabItemMotiveTableUI));
			this.ttabMotiveGroupUI1 = new SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI();
			this.lbMotive0 = new System.Windows.Forms.Label();
			this.lbMotive1 = new System.Windows.Forms.Label();
			this.lbMotive2 = new System.Windows.Forms.Label();
			this.lbMotive3 = new System.Windows.Forms.Label();
			this.lbMotive4 = new System.Windows.Forms.Label();
			this.lbMotive5 = new System.Windows.Forms.Label();
			this.lbMotive6 = new System.Windows.Forms.Label();
			this.lbMotive7 = new System.Windows.Forms.Label();
			this.lbMotive9 = new System.Windows.Forms.Label();
			this.lbMotive11 = new System.Windows.Forms.Label();
			this.lbMotive8 = new System.Windows.Forms.Label();
			this.lbMotive10 = new System.Windows.Forms.Label();
			this.lbMotive14 = new System.Windows.Forms.Label();
			this.lbMotive15 = new System.Windows.Forms.Label();
			this.lbMotive13 = new System.Windows.Forms.Label();
			this.lbMotive12 = new System.Windows.Forms.Label();
			this.pnAllGroups = new System.Windows.Forms.Panel();
			this.ttabMotiveGroupUI2 = new SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI();
			this.ttabMotiveGroupUI3 = new SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI();
			this.ttabMotiveGroupUI4 = new SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI();
			this.ttabMotiveGroupUI5 = new SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI();
			this.ttabMotiveGroupUI6 = new SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI();
			this.ttabMotiveGroupUI7 = new SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI();
			this.cbShowAll = new System.Windows.Forms.CheckBox();
			this.pnCopyButtons = new System.Windows.Forms.Panel();
			this.btnCopyAll = new System.Windows.Forms.Button();
			this.lbCBM0 = new System.Windows.Forms.Label();
			this.btnCpyM0 = new System.Windows.Forms.Button();
			this.btnCpyM1 = new System.Windows.Forms.Button();
			this.btnCpyM2 = new System.Windows.Forms.Button();
			this.btnCpyM3 = new System.Windows.Forms.Button();
			this.btnCpyM4 = new System.Windows.Forms.Button();
			this.btnCpyM5 = new System.Windows.Forms.Button();
			this.btnCpyM7 = new System.Windows.Forms.Button();
			this.btnCpyM6 = new System.Windows.Forms.Button();
			this.btnCpyM9 = new System.Windows.Forms.Button();
			this.btnCpyM12 = new System.Windows.Forms.Button();
			this.btnCpyM11 = new System.Windows.Forms.Button();
			this.btnCpyM10 = new System.Windows.Forms.Button();
			this.btnCpyM15 = new System.Windows.Forms.Button();
			this.btnCpyM14 = new System.Windows.Forms.Button();
			this.btnCpyM13 = new System.Windows.Forms.Button();
			this.btnCpyM8 = new System.Windows.Forms.Button();
			this.lbCBM1 = new System.Windows.Forms.Label();
			this.lbCBM2 = new System.Windows.Forms.Label();
			this.lbCBM3 = new System.Windows.Forms.Label();
			this.lbCBM4 = new System.Windows.Forms.Label();
			this.lbCBM5 = new System.Windows.Forms.Label();
			this.lbCBM6 = new System.Windows.Forms.Label();
			this.lbCBM7 = new System.Windows.Forms.Label();
			this.lbCBM15 = new System.Windows.Forms.Label();
			this.lbCBM11 = new System.Windows.Forms.Label();
			this.lbCBM14 = new System.Windows.Forms.Label();
			this.lbCBM8 = new System.Windows.Forms.Label();
			this.lbCBM9 = new System.Windows.Forms.Label();
			this.lbCBM13 = new System.Windows.Forms.Label();
			this.lbCBM10 = new System.Windows.Forms.Label();
			this.lbCBM12 = new System.Windows.Forms.Label();
			this.btnHelp = new System.Windows.Forms.Button();
			this.pnAllGroups.SuspendLayout();
			this.pnCopyButtons.SuspendLayout();
			this.SuspendLayout();
			// 
			// ttabMotiveGroupUI1
			// 
			this.ttabMotiveGroupUI1.AccessibleDescription = resources.GetString("ttabMotiveGroupUI1.AccessibleDescription");
			this.ttabMotiveGroupUI1.AccessibleName = resources.GetString("ttabMotiveGroupUI1.AccessibleName");
			this.ttabMotiveGroupUI1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabMotiveGroupUI1.Anchor")));
			this.ttabMotiveGroupUI1.AutoScroll = ((bool)(resources.GetObject("ttabMotiveGroupUI1.AutoScroll")));
			this.ttabMotiveGroupUI1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI1.AutoScrollMargin")));
			this.ttabMotiveGroupUI1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI1.AutoScrollMinSize")));
			this.ttabMotiveGroupUI1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabMotiveGroupUI1.BackgroundImage")));
			this.ttabMotiveGroupUI1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabMotiveGroupUI1.Dock")));
			this.ttabMotiveGroupUI1.Enabled = ((bool)(resources.GetObject("ttabMotiveGroupUI1.Enabled")));
			this.ttabMotiveGroupUI1.Font = ((System.Drawing.Font)(resources.GetObject("ttabMotiveGroupUI1.Font")));
			this.ttabMotiveGroupUI1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabMotiveGroupUI1.ImeMode")));
			this.ttabMotiveGroupUI1.Location = ((System.Drawing.Point)(resources.GetObject("ttabMotiveGroupUI1.Location")));
			this.ttabMotiveGroupUI1.MotiveGroup = 0;
			this.ttabMotiveGroupUI1.Name = "ttabMotiveGroupUI1";
			this.ttabMotiveGroupUI1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabMotiveGroupUI1.RightToLeft")));
			this.ttabMotiveGroupUI1.Size = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI1.Size")));
			this.ttabMotiveGroupUI1.TabIndex = ((int)(resources.GetObject("ttabMotiveGroupUI1.TabIndex")));
			this.ttabMotiveGroupUI1.Visible = ((bool)(resources.GetObject("ttabMotiveGroupUI1.Visible")));
			// 
			// lbMotive0
			// 
			this.lbMotive0.AccessibleDescription = resources.GetString("lbMotive0.AccessibleDescription");
			this.lbMotive0.AccessibleName = resources.GetString("lbMotive0.AccessibleName");
			this.lbMotive0.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbMotive0.Anchor")));
			this.lbMotive0.AutoSize = ((bool)(resources.GetObject("lbMotive0.AutoSize")));
			this.lbMotive0.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbMotive0.Dock")));
			this.lbMotive0.Enabled = ((bool)(resources.GetObject("lbMotive0.Enabled")));
			this.lbMotive0.Font = ((System.Drawing.Font)(resources.GetObject("lbMotive0.Font")));
			this.lbMotive0.Image = ((System.Drawing.Image)(resources.GetObject("lbMotive0.Image")));
			this.lbMotive0.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive0.ImageAlign")));
			this.lbMotive0.ImageIndex = ((int)(resources.GetObject("lbMotive0.ImageIndex")));
			this.lbMotive0.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbMotive0.ImeMode")));
			this.lbMotive0.Location = ((System.Drawing.Point)(resources.GetObject("lbMotive0.Location")));
			this.lbMotive0.Name = "lbMotive0";
			this.lbMotive0.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbMotive0.RightToLeft")));
			this.lbMotive0.Size = ((System.Drawing.Size)(resources.GetObject("lbMotive0.Size")));
			this.lbMotive0.TabIndex = ((int)(resources.GetObject("lbMotive0.TabIndex")));
			this.lbMotive0.Text = resources.GetString("lbMotive0.Text");
			this.lbMotive0.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive0.TextAlign")));
			this.lbMotive0.Visible = ((bool)(resources.GetObject("lbMotive0.Visible")));
			// 
			// lbMotive1
			// 
			this.lbMotive1.AccessibleDescription = resources.GetString("lbMotive1.AccessibleDescription");
			this.lbMotive1.AccessibleName = resources.GetString("lbMotive1.AccessibleName");
			this.lbMotive1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbMotive1.Anchor")));
			this.lbMotive1.AutoSize = ((bool)(resources.GetObject("lbMotive1.AutoSize")));
			this.lbMotive1.BackColor = System.Drawing.SystemColors.Control;
			this.lbMotive1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbMotive1.Dock")));
			this.lbMotive1.Enabled = ((bool)(resources.GetObject("lbMotive1.Enabled")));
			this.lbMotive1.Font = ((System.Drawing.Font)(resources.GetObject("lbMotive1.Font")));
			this.lbMotive1.Image = ((System.Drawing.Image)(resources.GetObject("lbMotive1.Image")));
			this.lbMotive1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive1.ImageAlign")));
			this.lbMotive1.ImageIndex = ((int)(resources.GetObject("lbMotive1.ImageIndex")));
			this.lbMotive1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbMotive1.ImeMode")));
			this.lbMotive1.Location = ((System.Drawing.Point)(resources.GetObject("lbMotive1.Location")));
			this.lbMotive1.Name = "lbMotive1";
			this.lbMotive1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbMotive1.RightToLeft")));
			this.lbMotive1.Size = ((System.Drawing.Size)(resources.GetObject("lbMotive1.Size")));
			this.lbMotive1.TabIndex = ((int)(resources.GetObject("lbMotive1.TabIndex")));
			this.lbMotive1.Text = resources.GetString("lbMotive1.Text");
			this.lbMotive1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive1.TextAlign")));
			this.lbMotive1.Visible = ((bool)(resources.GetObject("lbMotive1.Visible")));
			// 
			// lbMotive2
			// 
			this.lbMotive2.AccessibleDescription = resources.GetString("lbMotive2.AccessibleDescription");
			this.lbMotive2.AccessibleName = resources.GetString("lbMotive2.AccessibleName");
			this.lbMotive2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbMotive2.Anchor")));
			this.lbMotive2.AutoSize = ((bool)(resources.GetObject("lbMotive2.AutoSize")));
			this.lbMotive2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbMotive2.Dock")));
			this.lbMotive2.Enabled = ((bool)(resources.GetObject("lbMotive2.Enabled")));
			this.lbMotive2.Font = ((System.Drawing.Font)(resources.GetObject("lbMotive2.Font")));
			this.lbMotive2.Image = ((System.Drawing.Image)(resources.GetObject("lbMotive2.Image")));
			this.lbMotive2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive2.ImageAlign")));
			this.lbMotive2.ImageIndex = ((int)(resources.GetObject("lbMotive2.ImageIndex")));
			this.lbMotive2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbMotive2.ImeMode")));
			this.lbMotive2.Location = ((System.Drawing.Point)(resources.GetObject("lbMotive2.Location")));
			this.lbMotive2.Name = "lbMotive2";
			this.lbMotive2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbMotive2.RightToLeft")));
			this.lbMotive2.Size = ((System.Drawing.Size)(resources.GetObject("lbMotive2.Size")));
			this.lbMotive2.TabIndex = ((int)(resources.GetObject("lbMotive2.TabIndex")));
			this.lbMotive2.Text = resources.GetString("lbMotive2.Text");
			this.lbMotive2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive2.TextAlign")));
			this.lbMotive2.Visible = ((bool)(resources.GetObject("lbMotive2.Visible")));
			// 
			// lbMotive3
			// 
			this.lbMotive3.AccessibleDescription = resources.GetString("lbMotive3.AccessibleDescription");
			this.lbMotive3.AccessibleName = resources.GetString("lbMotive3.AccessibleName");
			this.lbMotive3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbMotive3.Anchor")));
			this.lbMotive3.AutoSize = ((bool)(resources.GetObject("lbMotive3.AutoSize")));
			this.lbMotive3.BackColor = System.Drawing.SystemColors.Control;
			this.lbMotive3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbMotive3.Dock")));
			this.lbMotive3.Enabled = ((bool)(resources.GetObject("lbMotive3.Enabled")));
			this.lbMotive3.Font = ((System.Drawing.Font)(resources.GetObject("lbMotive3.Font")));
			this.lbMotive3.Image = ((System.Drawing.Image)(resources.GetObject("lbMotive3.Image")));
			this.lbMotive3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive3.ImageAlign")));
			this.lbMotive3.ImageIndex = ((int)(resources.GetObject("lbMotive3.ImageIndex")));
			this.lbMotive3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbMotive3.ImeMode")));
			this.lbMotive3.Location = ((System.Drawing.Point)(resources.GetObject("lbMotive3.Location")));
			this.lbMotive3.Name = "lbMotive3";
			this.lbMotive3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbMotive3.RightToLeft")));
			this.lbMotive3.Size = ((System.Drawing.Size)(resources.GetObject("lbMotive3.Size")));
			this.lbMotive3.TabIndex = ((int)(resources.GetObject("lbMotive3.TabIndex")));
			this.lbMotive3.Text = resources.GetString("lbMotive3.Text");
			this.lbMotive3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive3.TextAlign")));
			this.lbMotive3.Visible = ((bool)(resources.GetObject("lbMotive3.Visible")));
			// 
			// lbMotive4
			// 
			this.lbMotive4.AccessibleDescription = resources.GetString("lbMotive4.AccessibleDescription");
			this.lbMotive4.AccessibleName = resources.GetString("lbMotive4.AccessibleName");
			this.lbMotive4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbMotive4.Anchor")));
			this.lbMotive4.AutoSize = ((bool)(resources.GetObject("lbMotive4.AutoSize")));
			this.lbMotive4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbMotive4.Dock")));
			this.lbMotive4.Enabled = ((bool)(resources.GetObject("lbMotive4.Enabled")));
			this.lbMotive4.Font = ((System.Drawing.Font)(resources.GetObject("lbMotive4.Font")));
			this.lbMotive4.Image = ((System.Drawing.Image)(resources.GetObject("lbMotive4.Image")));
			this.lbMotive4.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive4.ImageAlign")));
			this.lbMotive4.ImageIndex = ((int)(resources.GetObject("lbMotive4.ImageIndex")));
			this.lbMotive4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbMotive4.ImeMode")));
			this.lbMotive4.Location = ((System.Drawing.Point)(resources.GetObject("lbMotive4.Location")));
			this.lbMotive4.Name = "lbMotive4";
			this.lbMotive4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbMotive4.RightToLeft")));
			this.lbMotive4.Size = ((System.Drawing.Size)(resources.GetObject("lbMotive4.Size")));
			this.lbMotive4.TabIndex = ((int)(resources.GetObject("lbMotive4.TabIndex")));
			this.lbMotive4.Text = resources.GetString("lbMotive4.Text");
			this.lbMotive4.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive4.TextAlign")));
			this.lbMotive4.Visible = ((bool)(resources.GetObject("lbMotive4.Visible")));
			// 
			// lbMotive5
			// 
			this.lbMotive5.AccessibleDescription = resources.GetString("lbMotive5.AccessibleDescription");
			this.lbMotive5.AccessibleName = resources.GetString("lbMotive5.AccessibleName");
			this.lbMotive5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbMotive5.Anchor")));
			this.lbMotive5.AutoSize = ((bool)(resources.GetObject("lbMotive5.AutoSize")));
			this.lbMotive5.BackColor = System.Drawing.SystemColors.Control;
			this.lbMotive5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbMotive5.Dock")));
			this.lbMotive5.Enabled = ((bool)(resources.GetObject("lbMotive5.Enabled")));
			this.lbMotive5.Font = ((System.Drawing.Font)(resources.GetObject("lbMotive5.Font")));
			this.lbMotive5.Image = ((System.Drawing.Image)(resources.GetObject("lbMotive5.Image")));
			this.lbMotive5.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive5.ImageAlign")));
			this.lbMotive5.ImageIndex = ((int)(resources.GetObject("lbMotive5.ImageIndex")));
			this.lbMotive5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbMotive5.ImeMode")));
			this.lbMotive5.Location = ((System.Drawing.Point)(resources.GetObject("lbMotive5.Location")));
			this.lbMotive5.Name = "lbMotive5";
			this.lbMotive5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbMotive5.RightToLeft")));
			this.lbMotive5.Size = ((System.Drawing.Size)(resources.GetObject("lbMotive5.Size")));
			this.lbMotive5.TabIndex = ((int)(resources.GetObject("lbMotive5.TabIndex")));
			this.lbMotive5.Text = resources.GetString("lbMotive5.Text");
			this.lbMotive5.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive5.TextAlign")));
			this.lbMotive5.Visible = ((bool)(resources.GetObject("lbMotive5.Visible")));
			// 
			// lbMotive6
			// 
			this.lbMotive6.AccessibleDescription = resources.GetString("lbMotive6.AccessibleDescription");
			this.lbMotive6.AccessibleName = resources.GetString("lbMotive6.AccessibleName");
			this.lbMotive6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbMotive6.Anchor")));
			this.lbMotive6.AutoSize = ((bool)(resources.GetObject("lbMotive6.AutoSize")));
			this.lbMotive6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbMotive6.Dock")));
			this.lbMotive6.Enabled = ((bool)(resources.GetObject("lbMotive6.Enabled")));
			this.lbMotive6.Font = ((System.Drawing.Font)(resources.GetObject("lbMotive6.Font")));
			this.lbMotive6.Image = ((System.Drawing.Image)(resources.GetObject("lbMotive6.Image")));
			this.lbMotive6.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive6.ImageAlign")));
			this.lbMotive6.ImageIndex = ((int)(resources.GetObject("lbMotive6.ImageIndex")));
			this.lbMotive6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbMotive6.ImeMode")));
			this.lbMotive6.Location = ((System.Drawing.Point)(resources.GetObject("lbMotive6.Location")));
			this.lbMotive6.Name = "lbMotive6";
			this.lbMotive6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbMotive6.RightToLeft")));
			this.lbMotive6.Size = ((System.Drawing.Size)(resources.GetObject("lbMotive6.Size")));
			this.lbMotive6.TabIndex = ((int)(resources.GetObject("lbMotive6.TabIndex")));
			this.lbMotive6.Text = resources.GetString("lbMotive6.Text");
			this.lbMotive6.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive6.TextAlign")));
			this.lbMotive6.Visible = ((bool)(resources.GetObject("lbMotive6.Visible")));
			// 
			// lbMotive7
			// 
			this.lbMotive7.AccessibleDescription = resources.GetString("lbMotive7.AccessibleDescription");
			this.lbMotive7.AccessibleName = resources.GetString("lbMotive7.AccessibleName");
			this.lbMotive7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbMotive7.Anchor")));
			this.lbMotive7.AutoSize = ((bool)(resources.GetObject("lbMotive7.AutoSize")));
			this.lbMotive7.BackColor = System.Drawing.SystemColors.Control;
			this.lbMotive7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbMotive7.Dock")));
			this.lbMotive7.Enabled = ((bool)(resources.GetObject("lbMotive7.Enabled")));
			this.lbMotive7.Font = ((System.Drawing.Font)(resources.GetObject("lbMotive7.Font")));
			this.lbMotive7.Image = ((System.Drawing.Image)(resources.GetObject("lbMotive7.Image")));
			this.lbMotive7.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive7.ImageAlign")));
			this.lbMotive7.ImageIndex = ((int)(resources.GetObject("lbMotive7.ImageIndex")));
			this.lbMotive7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbMotive7.ImeMode")));
			this.lbMotive7.Location = ((System.Drawing.Point)(resources.GetObject("lbMotive7.Location")));
			this.lbMotive7.Name = "lbMotive7";
			this.lbMotive7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbMotive7.RightToLeft")));
			this.lbMotive7.Size = ((System.Drawing.Size)(resources.GetObject("lbMotive7.Size")));
			this.lbMotive7.TabIndex = ((int)(resources.GetObject("lbMotive7.TabIndex")));
			this.lbMotive7.Text = resources.GetString("lbMotive7.Text");
			this.lbMotive7.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive7.TextAlign")));
			this.lbMotive7.Visible = ((bool)(resources.GetObject("lbMotive7.Visible")));
			// 
			// lbMotive9
			// 
			this.lbMotive9.AccessibleDescription = resources.GetString("lbMotive9.AccessibleDescription");
			this.lbMotive9.AccessibleName = resources.GetString("lbMotive9.AccessibleName");
			this.lbMotive9.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbMotive9.Anchor")));
			this.lbMotive9.AutoSize = ((bool)(resources.GetObject("lbMotive9.AutoSize")));
			this.lbMotive9.BackColor = System.Drawing.SystemColors.Control;
			this.lbMotive9.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbMotive9.Dock")));
			this.lbMotive9.Enabled = ((bool)(resources.GetObject("lbMotive9.Enabled")));
			this.lbMotive9.Font = ((System.Drawing.Font)(resources.GetObject("lbMotive9.Font")));
			this.lbMotive9.Image = ((System.Drawing.Image)(resources.GetObject("lbMotive9.Image")));
			this.lbMotive9.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive9.ImageAlign")));
			this.lbMotive9.ImageIndex = ((int)(resources.GetObject("lbMotive9.ImageIndex")));
			this.lbMotive9.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbMotive9.ImeMode")));
			this.lbMotive9.Location = ((System.Drawing.Point)(resources.GetObject("lbMotive9.Location")));
			this.lbMotive9.Name = "lbMotive9";
			this.lbMotive9.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbMotive9.RightToLeft")));
			this.lbMotive9.Size = ((System.Drawing.Size)(resources.GetObject("lbMotive9.Size")));
			this.lbMotive9.TabIndex = ((int)(resources.GetObject("lbMotive9.TabIndex")));
			this.lbMotive9.Text = resources.GetString("lbMotive9.Text");
			this.lbMotive9.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive9.TextAlign")));
			this.lbMotive9.Visible = ((bool)(resources.GetObject("lbMotive9.Visible")));
			// 
			// lbMotive11
			// 
			this.lbMotive11.AccessibleDescription = resources.GetString("lbMotive11.AccessibleDescription");
			this.lbMotive11.AccessibleName = resources.GetString("lbMotive11.AccessibleName");
			this.lbMotive11.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbMotive11.Anchor")));
			this.lbMotive11.AutoSize = ((bool)(resources.GetObject("lbMotive11.AutoSize")));
			this.lbMotive11.BackColor = System.Drawing.SystemColors.Control;
			this.lbMotive11.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbMotive11.Dock")));
			this.lbMotive11.Enabled = ((bool)(resources.GetObject("lbMotive11.Enabled")));
			this.lbMotive11.Font = ((System.Drawing.Font)(resources.GetObject("lbMotive11.Font")));
			this.lbMotive11.Image = ((System.Drawing.Image)(resources.GetObject("lbMotive11.Image")));
			this.lbMotive11.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive11.ImageAlign")));
			this.lbMotive11.ImageIndex = ((int)(resources.GetObject("lbMotive11.ImageIndex")));
			this.lbMotive11.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbMotive11.ImeMode")));
			this.lbMotive11.Location = ((System.Drawing.Point)(resources.GetObject("lbMotive11.Location")));
			this.lbMotive11.Name = "lbMotive11";
			this.lbMotive11.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbMotive11.RightToLeft")));
			this.lbMotive11.Size = ((System.Drawing.Size)(resources.GetObject("lbMotive11.Size")));
			this.lbMotive11.TabIndex = ((int)(resources.GetObject("lbMotive11.TabIndex")));
			this.lbMotive11.Text = resources.GetString("lbMotive11.Text");
			this.lbMotive11.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive11.TextAlign")));
			this.lbMotive11.Visible = ((bool)(resources.GetObject("lbMotive11.Visible")));
			// 
			// lbMotive8
			// 
			this.lbMotive8.AccessibleDescription = resources.GetString("lbMotive8.AccessibleDescription");
			this.lbMotive8.AccessibleName = resources.GetString("lbMotive8.AccessibleName");
			this.lbMotive8.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbMotive8.Anchor")));
			this.lbMotive8.AutoSize = ((bool)(resources.GetObject("lbMotive8.AutoSize")));
			this.lbMotive8.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbMotive8.Dock")));
			this.lbMotive8.Enabled = ((bool)(resources.GetObject("lbMotive8.Enabled")));
			this.lbMotive8.Font = ((System.Drawing.Font)(resources.GetObject("lbMotive8.Font")));
			this.lbMotive8.Image = ((System.Drawing.Image)(resources.GetObject("lbMotive8.Image")));
			this.lbMotive8.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive8.ImageAlign")));
			this.lbMotive8.ImageIndex = ((int)(resources.GetObject("lbMotive8.ImageIndex")));
			this.lbMotive8.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbMotive8.ImeMode")));
			this.lbMotive8.Location = ((System.Drawing.Point)(resources.GetObject("lbMotive8.Location")));
			this.lbMotive8.Name = "lbMotive8";
			this.lbMotive8.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbMotive8.RightToLeft")));
			this.lbMotive8.Size = ((System.Drawing.Size)(resources.GetObject("lbMotive8.Size")));
			this.lbMotive8.TabIndex = ((int)(resources.GetObject("lbMotive8.TabIndex")));
			this.lbMotive8.Text = resources.GetString("lbMotive8.Text");
			this.lbMotive8.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive8.TextAlign")));
			this.lbMotive8.Visible = ((bool)(resources.GetObject("lbMotive8.Visible")));
			// 
			// lbMotive10
			// 
			this.lbMotive10.AccessibleDescription = resources.GetString("lbMotive10.AccessibleDescription");
			this.lbMotive10.AccessibleName = resources.GetString("lbMotive10.AccessibleName");
			this.lbMotive10.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbMotive10.Anchor")));
			this.lbMotive10.AutoSize = ((bool)(resources.GetObject("lbMotive10.AutoSize")));
			this.lbMotive10.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbMotive10.Dock")));
			this.lbMotive10.Enabled = ((bool)(resources.GetObject("lbMotive10.Enabled")));
			this.lbMotive10.Font = ((System.Drawing.Font)(resources.GetObject("lbMotive10.Font")));
			this.lbMotive10.Image = ((System.Drawing.Image)(resources.GetObject("lbMotive10.Image")));
			this.lbMotive10.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive10.ImageAlign")));
			this.lbMotive10.ImageIndex = ((int)(resources.GetObject("lbMotive10.ImageIndex")));
			this.lbMotive10.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbMotive10.ImeMode")));
			this.lbMotive10.Location = ((System.Drawing.Point)(resources.GetObject("lbMotive10.Location")));
			this.lbMotive10.Name = "lbMotive10";
			this.lbMotive10.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbMotive10.RightToLeft")));
			this.lbMotive10.Size = ((System.Drawing.Size)(resources.GetObject("lbMotive10.Size")));
			this.lbMotive10.TabIndex = ((int)(resources.GetObject("lbMotive10.TabIndex")));
			this.lbMotive10.Text = resources.GetString("lbMotive10.Text");
			this.lbMotive10.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive10.TextAlign")));
			this.lbMotive10.Visible = ((bool)(resources.GetObject("lbMotive10.Visible")));
			// 
			// lbMotive14
			// 
			this.lbMotive14.AccessibleDescription = resources.GetString("lbMotive14.AccessibleDescription");
			this.lbMotive14.AccessibleName = resources.GetString("lbMotive14.AccessibleName");
			this.lbMotive14.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbMotive14.Anchor")));
			this.lbMotive14.AutoSize = ((bool)(resources.GetObject("lbMotive14.AutoSize")));
			this.lbMotive14.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbMotive14.Dock")));
			this.lbMotive14.Enabled = ((bool)(resources.GetObject("lbMotive14.Enabled")));
			this.lbMotive14.Font = ((System.Drawing.Font)(resources.GetObject("lbMotive14.Font")));
			this.lbMotive14.Image = ((System.Drawing.Image)(resources.GetObject("lbMotive14.Image")));
			this.lbMotive14.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive14.ImageAlign")));
			this.lbMotive14.ImageIndex = ((int)(resources.GetObject("lbMotive14.ImageIndex")));
			this.lbMotive14.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbMotive14.ImeMode")));
			this.lbMotive14.Location = ((System.Drawing.Point)(resources.GetObject("lbMotive14.Location")));
			this.lbMotive14.Name = "lbMotive14";
			this.lbMotive14.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbMotive14.RightToLeft")));
			this.lbMotive14.Size = ((System.Drawing.Size)(resources.GetObject("lbMotive14.Size")));
			this.lbMotive14.TabIndex = ((int)(resources.GetObject("lbMotive14.TabIndex")));
			this.lbMotive14.Text = resources.GetString("lbMotive14.Text");
			this.lbMotive14.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive14.TextAlign")));
			this.lbMotive14.Visible = ((bool)(resources.GetObject("lbMotive14.Visible")));
			// 
			// lbMotive15
			// 
			this.lbMotive15.AccessibleDescription = resources.GetString("lbMotive15.AccessibleDescription");
			this.lbMotive15.AccessibleName = resources.GetString("lbMotive15.AccessibleName");
			this.lbMotive15.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbMotive15.Anchor")));
			this.lbMotive15.AutoSize = ((bool)(resources.GetObject("lbMotive15.AutoSize")));
			this.lbMotive15.BackColor = System.Drawing.SystemColors.Control;
			this.lbMotive15.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbMotive15.Dock")));
			this.lbMotive15.Enabled = ((bool)(resources.GetObject("lbMotive15.Enabled")));
			this.lbMotive15.Font = ((System.Drawing.Font)(resources.GetObject("lbMotive15.Font")));
			this.lbMotive15.Image = ((System.Drawing.Image)(resources.GetObject("lbMotive15.Image")));
			this.lbMotive15.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive15.ImageAlign")));
			this.lbMotive15.ImageIndex = ((int)(resources.GetObject("lbMotive15.ImageIndex")));
			this.lbMotive15.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbMotive15.ImeMode")));
			this.lbMotive15.Location = ((System.Drawing.Point)(resources.GetObject("lbMotive15.Location")));
			this.lbMotive15.Name = "lbMotive15";
			this.lbMotive15.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbMotive15.RightToLeft")));
			this.lbMotive15.Size = ((System.Drawing.Size)(resources.GetObject("lbMotive15.Size")));
			this.lbMotive15.TabIndex = ((int)(resources.GetObject("lbMotive15.TabIndex")));
			this.lbMotive15.Text = resources.GetString("lbMotive15.Text");
			this.lbMotive15.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive15.TextAlign")));
			this.lbMotive15.Visible = ((bool)(resources.GetObject("lbMotive15.Visible")));
			// 
			// lbMotive13
			// 
			this.lbMotive13.AccessibleDescription = resources.GetString("lbMotive13.AccessibleDescription");
			this.lbMotive13.AccessibleName = resources.GetString("lbMotive13.AccessibleName");
			this.lbMotive13.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbMotive13.Anchor")));
			this.lbMotive13.AutoSize = ((bool)(resources.GetObject("lbMotive13.AutoSize")));
			this.lbMotive13.BackColor = System.Drawing.SystemColors.Control;
			this.lbMotive13.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbMotive13.Dock")));
			this.lbMotive13.Enabled = ((bool)(resources.GetObject("lbMotive13.Enabled")));
			this.lbMotive13.Font = ((System.Drawing.Font)(resources.GetObject("lbMotive13.Font")));
			this.lbMotive13.Image = ((System.Drawing.Image)(resources.GetObject("lbMotive13.Image")));
			this.lbMotive13.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive13.ImageAlign")));
			this.lbMotive13.ImageIndex = ((int)(resources.GetObject("lbMotive13.ImageIndex")));
			this.lbMotive13.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbMotive13.ImeMode")));
			this.lbMotive13.Location = ((System.Drawing.Point)(resources.GetObject("lbMotive13.Location")));
			this.lbMotive13.Name = "lbMotive13";
			this.lbMotive13.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbMotive13.RightToLeft")));
			this.lbMotive13.Size = ((System.Drawing.Size)(resources.GetObject("lbMotive13.Size")));
			this.lbMotive13.TabIndex = ((int)(resources.GetObject("lbMotive13.TabIndex")));
			this.lbMotive13.Text = resources.GetString("lbMotive13.Text");
			this.lbMotive13.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive13.TextAlign")));
			this.lbMotive13.Visible = ((bool)(resources.GetObject("lbMotive13.Visible")));
			// 
			// lbMotive12
			// 
			this.lbMotive12.AccessibleDescription = resources.GetString("lbMotive12.AccessibleDescription");
			this.lbMotive12.AccessibleName = resources.GetString("lbMotive12.AccessibleName");
			this.lbMotive12.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbMotive12.Anchor")));
			this.lbMotive12.AutoSize = ((bool)(resources.GetObject("lbMotive12.AutoSize")));
			this.lbMotive12.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbMotive12.Dock")));
			this.lbMotive12.Enabled = ((bool)(resources.GetObject("lbMotive12.Enabled")));
			this.lbMotive12.Font = ((System.Drawing.Font)(resources.GetObject("lbMotive12.Font")));
			this.lbMotive12.Image = ((System.Drawing.Image)(resources.GetObject("lbMotive12.Image")));
			this.lbMotive12.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive12.ImageAlign")));
			this.lbMotive12.ImageIndex = ((int)(resources.GetObject("lbMotive12.ImageIndex")));
			this.lbMotive12.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbMotive12.ImeMode")));
			this.lbMotive12.Location = ((System.Drawing.Point)(resources.GetObject("lbMotive12.Location")));
			this.lbMotive12.Name = "lbMotive12";
			this.lbMotive12.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbMotive12.RightToLeft")));
			this.lbMotive12.Size = ((System.Drawing.Size)(resources.GetObject("lbMotive12.Size")));
			this.lbMotive12.TabIndex = ((int)(resources.GetObject("lbMotive12.TabIndex")));
			this.lbMotive12.Text = resources.GetString("lbMotive12.Text");
			this.lbMotive12.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMotive12.TextAlign")));
			this.lbMotive12.Visible = ((bool)(resources.GetObject("lbMotive12.Visible")));
			// 
			// pnAllGroups
			// 
			this.pnAllGroups.AccessibleDescription = resources.GetString("pnAllGroups.AccessibleDescription");
			this.pnAllGroups.AccessibleName = resources.GetString("pnAllGroups.AccessibleName");
			this.pnAllGroups.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pnAllGroups.Anchor")));
			this.pnAllGroups.AutoScroll = ((bool)(resources.GetObject("pnAllGroups.AutoScroll")));
			this.pnAllGroups.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("pnAllGroups.AutoScrollMargin")));
			this.pnAllGroups.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("pnAllGroups.AutoScrollMinSize")));
			this.pnAllGroups.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnAllGroups.BackgroundImage")));
			this.pnAllGroups.Controls.Add(this.ttabMotiveGroupUI2);
			this.pnAllGroups.Controls.Add(this.ttabMotiveGroupUI3);
			this.pnAllGroups.Controls.Add(this.ttabMotiveGroupUI4);
			this.pnAllGroups.Controls.Add(this.ttabMotiveGroupUI5);
			this.pnAllGroups.Controls.Add(this.ttabMotiveGroupUI6);
			this.pnAllGroups.Controls.Add(this.ttabMotiveGroupUI7);
			this.pnAllGroups.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnAllGroups.Dock")));
			this.pnAllGroups.Enabled = ((bool)(resources.GetObject("pnAllGroups.Enabled")));
			this.pnAllGroups.Font = ((System.Drawing.Font)(resources.GetObject("pnAllGroups.Font")));
			this.pnAllGroups.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnAllGroups.ImeMode")));
			this.pnAllGroups.Location = ((System.Drawing.Point)(resources.GetObject("pnAllGroups.Location")));
			this.pnAllGroups.Name = "pnAllGroups";
			this.pnAllGroups.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnAllGroups.RightToLeft")));
			this.pnAllGroups.Size = ((System.Drawing.Size)(resources.GetObject("pnAllGroups.Size")));
			this.pnAllGroups.TabIndex = ((int)(resources.GetObject("pnAllGroups.TabIndex")));
			this.pnAllGroups.Text = resources.GetString("pnAllGroups.Text");
			this.pnAllGroups.Visible = ((bool)(resources.GetObject("pnAllGroups.Visible")));
			// 
			// ttabMotiveGroupUI2
			// 
			this.ttabMotiveGroupUI2.AccessibleDescription = resources.GetString("ttabMotiveGroupUI2.AccessibleDescription");
			this.ttabMotiveGroupUI2.AccessibleName = resources.GetString("ttabMotiveGroupUI2.AccessibleName");
			this.ttabMotiveGroupUI2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabMotiveGroupUI2.Anchor")));
			this.ttabMotiveGroupUI2.AutoScroll = ((bool)(resources.GetObject("ttabMotiveGroupUI2.AutoScroll")));
			this.ttabMotiveGroupUI2.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI2.AutoScrollMargin")));
			this.ttabMotiveGroupUI2.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI2.AutoScrollMinSize")));
			this.ttabMotiveGroupUI2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabMotiveGroupUI2.BackgroundImage")));
			this.ttabMotiveGroupUI2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabMotiveGroupUI2.Dock")));
			this.ttabMotiveGroupUI2.Enabled = ((bool)(resources.GetObject("ttabMotiveGroupUI2.Enabled")));
			this.ttabMotiveGroupUI2.Font = ((System.Drawing.Font)(resources.GetObject("ttabMotiveGroupUI2.Font")));
			this.ttabMotiveGroupUI2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabMotiveGroupUI2.ImeMode")));
			this.ttabMotiveGroupUI2.Location = ((System.Drawing.Point)(resources.GetObject("ttabMotiveGroupUI2.Location")));
			this.ttabMotiveGroupUI2.MotiveGroup = 1;
			this.ttabMotiveGroupUI2.Name = "ttabMotiveGroupUI2";
			this.ttabMotiveGroupUI2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabMotiveGroupUI2.RightToLeft")));
			this.ttabMotiveGroupUI2.Size = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI2.Size")));
			this.ttabMotiveGroupUI2.TabIndex = ((int)(resources.GetObject("ttabMotiveGroupUI2.TabIndex")));
			this.ttabMotiveGroupUI2.Visible = ((bool)(resources.GetObject("ttabMotiveGroupUI2.Visible")));
			// 
			// ttabMotiveGroupUI3
			// 
			this.ttabMotiveGroupUI3.AccessibleDescription = resources.GetString("ttabMotiveGroupUI3.AccessibleDescription");
			this.ttabMotiveGroupUI3.AccessibleName = resources.GetString("ttabMotiveGroupUI3.AccessibleName");
			this.ttabMotiveGroupUI3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabMotiveGroupUI3.Anchor")));
			this.ttabMotiveGroupUI3.AutoScroll = ((bool)(resources.GetObject("ttabMotiveGroupUI3.AutoScroll")));
			this.ttabMotiveGroupUI3.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI3.AutoScrollMargin")));
			this.ttabMotiveGroupUI3.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI3.AutoScrollMinSize")));
			this.ttabMotiveGroupUI3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabMotiveGroupUI3.BackgroundImage")));
			this.ttabMotiveGroupUI3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabMotiveGroupUI3.Dock")));
			this.ttabMotiveGroupUI3.Enabled = ((bool)(resources.GetObject("ttabMotiveGroupUI3.Enabled")));
			this.ttabMotiveGroupUI3.Font = ((System.Drawing.Font)(resources.GetObject("ttabMotiveGroupUI3.Font")));
			this.ttabMotiveGroupUI3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabMotiveGroupUI3.ImeMode")));
			this.ttabMotiveGroupUI3.Location = ((System.Drawing.Point)(resources.GetObject("ttabMotiveGroupUI3.Location")));
			this.ttabMotiveGroupUI3.MotiveGroup = 2;
			this.ttabMotiveGroupUI3.Name = "ttabMotiveGroupUI3";
			this.ttabMotiveGroupUI3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabMotiveGroupUI3.RightToLeft")));
			this.ttabMotiveGroupUI3.Size = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI3.Size")));
			this.ttabMotiveGroupUI3.TabIndex = ((int)(resources.GetObject("ttabMotiveGroupUI3.TabIndex")));
			this.ttabMotiveGroupUI3.Visible = ((bool)(resources.GetObject("ttabMotiveGroupUI3.Visible")));
			// 
			// ttabMotiveGroupUI4
			// 
			this.ttabMotiveGroupUI4.AccessibleDescription = resources.GetString("ttabMotiveGroupUI4.AccessibleDescription");
			this.ttabMotiveGroupUI4.AccessibleName = resources.GetString("ttabMotiveGroupUI4.AccessibleName");
			this.ttabMotiveGroupUI4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabMotiveGroupUI4.Anchor")));
			this.ttabMotiveGroupUI4.AutoScroll = ((bool)(resources.GetObject("ttabMotiveGroupUI4.AutoScroll")));
			this.ttabMotiveGroupUI4.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI4.AutoScrollMargin")));
			this.ttabMotiveGroupUI4.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI4.AutoScrollMinSize")));
			this.ttabMotiveGroupUI4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabMotiveGroupUI4.BackgroundImage")));
			this.ttabMotiveGroupUI4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabMotiveGroupUI4.Dock")));
			this.ttabMotiveGroupUI4.Enabled = ((bool)(resources.GetObject("ttabMotiveGroupUI4.Enabled")));
			this.ttabMotiveGroupUI4.Font = ((System.Drawing.Font)(resources.GetObject("ttabMotiveGroupUI4.Font")));
			this.ttabMotiveGroupUI4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabMotiveGroupUI4.ImeMode")));
			this.ttabMotiveGroupUI4.Location = ((System.Drawing.Point)(resources.GetObject("ttabMotiveGroupUI4.Location")));
			this.ttabMotiveGroupUI4.MotiveGroup = 3;
			this.ttabMotiveGroupUI4.Name = "ttabMotiveGroupUI4";
			this.ttabMotiveGroupUI4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabMotiveGroupUI4.RightToLeft")));
			this.ttabMotiveGroupUI4.Size = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI4.Size")));
			this.ttabMotiveGroupUI4.TabIndex = ((int)(resources.GetObject("ttabMotiveGroupUI4.TabIndex")));
			this.ttabMotiveGroupUI4.Visible = ((bool)(resources.GetObject("ttabMotiveGroupUI4.Visible")));
			// 
			// ttabMotiveGroupUI5
			// 
			this.ttabMotiveGroupUI5.AccessibleDescription = resources.GetString("ttabMotiveGroupUI5.AccessibleDescription");
			this.ttabMotiveGroupUI5.AccessibleName = resources.GetString("ttabMotiveGroupUI5.AccessibleName");
			this.ttabMotiveGroupUI5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabMotiveGroupUI5.Anchor")));
			this.ttabMotiveGroupUI5.AutoScroll = ((bool)(resources.GetObject("ttabMotiveGroupUI5.AutoScroll")));
			this.ttabMotiveGroupUI5.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI5.AutoScrollMargin")));
			this.ttabMotiveGroupUI5.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI5.AutoScrollMinSize")));
			this.ttabMotiveGroupUI5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabMotiveGroupUI5.BackgroundImage")));
			this.ttabMotiveGroupUI5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabMotiveGroupUI5.Dock")));
			this.ttabMotiveGroupUI5.Enabled = ((bool)(resources.GetObject("ttabMotiveGroupUI5.Enabled")));
			this.ttabMotiveGroupUI5.Font = ((System.Drawing.Font)(resources.GetObject("ttabMotiveGroupUI5.Font")));
			this.ttabMotiveGroupUI5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabMotiveGroupUI5.ImeMode")));
			this.ttabMotiveGroupUI5.Location = ((System.Drawing.Point)(resources.GetObject("ttabMotiveGroupUI5.Location")));
			this.ttabMotiveGroupUI5.MotiveGroup = 4;
			this.ttabMotiveGroupUI5.Name = "ttabMotiveGroupUI5";
			this.ttabMotiveGroupUI5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabMotiveGroupUI5.RightToLeft")));
			this.ttabMotiveGroupUI5.Size = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI5.Size")));
			this.ttabMotiveGroupUI5.TabIndex = ((int)(resources.GetObject("ttabMotiveGroupUI5.TabIndex")));
			this.ttabMotiveGroupUI5.Visible = ((bool)(resources.GetObject("ttabMotiveGroupUI5.Visible")));
			// 
			// ttabMotiveGroupUI6
			// 
			this.ttabMotiveGroupUI6.AccessibleDescription = resources.GetString("ttabMotiveGroupUI6.AccessibleDescription");
			this.ttabMotiveGroupUI6.AccessibleName = resources.GetString("ttabMotiveGroupUI6.AccessibleName");
			this.ttabMotiveGroupUI6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabMotiveGroupUI6.Anchor")));
			this.ttabMotiveGroupUI6.AutoScroll = ((bool)(resources.GetObject("ttabMotiveGroupUI6.AutoScroll")));
			this.ttabMotiveGroupUI6.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI6.AutoScrollMargin")));
			this.ttabMotiveGroupUI6.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI6.AutoScrollMinSize")));
			this.ttabMotiveGroupUI6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabMotiveGroupUI6.BackgroundImage")));
			this.ttabMotiveGroupUI6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabMotiveGroupUI6.Dock")));
			this.ttabMotiveGroupUI6.Enabled = ((bool)(resources.GetObject("ttabMotiveGroupUI6.Enabled")));
			this.ttabMotiveGroupUI6.Font = ((System.Drawing.Font)(resources.GetObject("ttabMotiveGroupUI6.Font")));
			this.ttabMotiveGroupUI6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabMotiveGroupUI6.ImeMode")));
			this.ttabMotiveGroupUI6.Location = ((System.Drawing.Point)(resources.GetObject("ttabMotiveGroupUI6.Location")));
			this.ttabMotiveGroupUI6.MotiveGroup = 5;
			this.ttabMotiveGroupUI6.Name = "ttabMotiveGroupUI6";
			this.ttabMotiveGroupUI6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabMotiveGroupUI6.RightToLeft")));
			this.ttabMotiveGroupUI6.Size = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI6.Size")));
			this.ttabMotiveGroupUI6.TabIndex = ((int)(resources.GetObject("ttabMotiveGroupUI6.TabIndex")));
			this.ttabMotiveGroupUI6.Visible = ((bool)(resources.GetObject("ttabMotiveGroupUI6.Visible")));
			// 
			// ttabMotiveGroupUI7
			// 
			this.ttabMotiveGroupUI7.AccessibleDescription = resources.GetString("ttabMotiveGroupUI7.AccessibleDescription");
			this.ttabMotiveGroupUI7.AccessibleName = resources.GetString("ttabMotiveGroupUI7.AccessibleName");
			this.ttabMotiveGroupUI7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabMotiveGroupUI7.Anchor")));
			this.ttabMotiveGroupUI7.AutoScroll = ((bool)(resources.GetObject("ttabMotiveGroupUI7.AutoScroll")));
			this.ttabMotiveGroupUI7.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI7.AutoScrollMargin")));
			this.ttabMotiveGroupUI7.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI7.AutoScrollMinSize")));
			this.ttabMotiveGroupUI7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabMotiveGroupUI7.BackgroundImage")));
			this.ttabMotiveGroupUI7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabMotiveGroupUI7.Dock")));
			this.ttabMotiveGroupUI7.Enabled = ((bool)(resources.GetObject("ttabMotiveGroupUI7.Enabled")));
			this.ttabMotiveGroupUI7.Font = ((System.Drawing.Font)(resources.GetObject("ttabMotiveGroupUI7.Font")));
			this.ttabMotiveGroupUI7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabMotiveGroupUI7.ImeMode")));
			this.ttabMotiveGroupUI7.Location = ((System.Drawing.Point)(resources.GetObject("ttabMotiveGroupUI7.Location")));
			this.ttabMotiveGroupUI7.MotiveGroup = 6;
			this.ttabMotiveGroupUI7.Name = "ttabMotiveGroupUI7";
			this.ttabMotiveGroupUI7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabMotiveGroupUI7.RightToLeft")));
			this.ttabMotiveGroupUI7.Size = ((System.Drawing.Size)(resources.GetObject("ttabMotiveGroupUI7.Size")));
			this.ttabMotiveGroupUI7.TabIndex = ((int)(resources.GetObject("ttabMotiveGroupUI7.TabIndex")));
			this.ttabMotiveGroupUI7.Visible = ((bool)(resources.GetObject("ttabMotiveGroupUI7.Visible")));
			// 
			// cbShowAll
			// 
			this.cbShowAll.AccessibleDescription = resources.GetString("cbShowAll.AccessibleDescription");
			this.cbShowAll.AccessibleName = resources.GetString("cbShowAll.AccessibleName");
			this.cbShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbShowAll.Anchor")));
			this.cbShowAll.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbShowAll.Appearance")));
			this.cbShowAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbShowAll.BackgroundImage")));
			this.cbShowAll.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbShowAll.CheckAlign")));
			this.cbShowAll.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbShowAll.Dock")));
			this.cbShowAll.Enabled = ((bool)(resources.GetObject("cbShowAll.Enabled")));
			this.cbShowAll.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbShowAll.FlatStyle")));
			this.cbShowAll.Font = ((System.Drawing.Font)(resources.GetObject("cbShowAll.Font")));
			this.cbShowAll.Image = ((System.Drawing.Image)(resources.GetObject("cbShowAll.Image")));
			this.cbShowAll.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbShowAll.ImageAlign")));
			this.cbShowAll.ImageIndex = ((int)(resources.GetObject("cbShowAll.ImageIndex")));
			this.cbShowAll.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbShowAll.ImeMode")));
			this.cbShowAll.Location = ((System.Drawing.Point)(resources.GetObject("cbShowAll.Location")));
			this.cbShowAll.Name = "cbShowAll";
			this.cbShowAll.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbShowAll.RightToLeft")));
			this.cbShowAll.Size = ((System.Drawing.Size)(resources.GetObject("cbShowAll.Size")));
			this.cbShowAll.TabIndex = ((int)(resources.GetObject("cbShowAll.TabIndex")));
			this.cbShowAll.Text = resources.GetString("cbShowAll.Text");
			this.cbShowAll.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbShowAll.TextAlign")));
			this.cbShowAll.Visible = ((bool)(resources.GetObject("cbShowAll.Visible")));
			this.cbShowAll.CheckedChanged += new System.EventHandler(this.cbShowAll_CheckedChanged);
			// 
			// pnCopyButtons
			// 
			this.pnCopyButtons.AccessibleDescription = resources.GetString("pnCopyButtons.AccessibleDescription");
			this.pnCopyButtons.AccessibleName = resources.GetString("pnCopyButtons.AccessibleName");
			this.pnCopyButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pnCopyButtons.Anchor")));
			this.pnCopyButtons.AutoScroll = ((bool)(resources.GetObject("pnCopyButtons.AutoScroll")));
			this.pnCopyButtons.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("pnCopyButtons.AutoScrollMargin")));
			this.pnCopyButtons.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("pnCopyButtons.AutoScrollMinSize")));
			this.pnCopyButtons.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnCopyButtons.BackgroundImage")));
			this.pnCopyButtons.Controls.Add(this.btnCopyAll);
			this.pnCopyButtons.Controls.Add(this.lbCBM0);
			this.pnCopyButtons.Controls.Add(this.btnCpyM0);
			this.pnCopyButtons.Controls.Add(this.btnCpyM1);
			this.pnCopyButtons.Controls.Add(this.btnCpyM2);
			this.pnCopyButtons.Controls.Add(this.btnCpyM3);
			this.pnCopyButtons.Controls.Add(this.btnCpyM4);
			this.pnCopyButtons.Controls.Add(this.btnCpyM5);
			this.pnCopyButtons.Controls.Add(this.btnCpyM7);
			this.pnCopyButtons.Controls.Add(this.btnCpyM6);
			this.pnCopyButtons.Controls.Add(this.btnCpyM9);
			this.pnCopyButtons.Controls.Add(this.btnCpyM12);
			this.pnCopyButtons.Controls.Add(this.btnCpyM11);
			this.pnCopyButtons.Controls.Add(this.btnCpyM10);
			this.pnCopyButtons.Controls.Add(this.btnCpyM15);
			this.pnCopyButtons.Controls.Add(this.btnCpyM14);
			this.pnCopyButtons.Controls.Add(this.btnCpyM13);
			this.pnCopyButtons.Controls.Add(this.btnCpyM8);
			this.pnCopyButtons.Controls.Add(this.lbCBM1);
			this.pnCopyButtons.Controls.Add(this.lbCBM2);
			this.pnCopyButtons.Controls.Add(this.lbCBM3);
			this.pnCopyButtons.Controls.Add(this.lbCBM4);
			this.pnCopyButtons.Controls.Add(this.lbCBM5);
			this.pnCopyButtons.Controls.Add(this.lbCBM6);
			this.pnCopyButtons.Controls.Add(this.lbCBM7);
			this.pnCopyButtons.Controls.Add(this.lbCBM15);
			this.pnCopyButtons.Controls.Add(this.lbCBM11);
			this.pnCopyButtons.Controls.Add(this.lbCBM14);
			this.pnCopyButtons.Controls.Add(this.lbCBM8);
			this.pnCopyButtons.Controls.Add(this.lbCBM9);
			this.pnCopyButtons.Controls.Add(this.lbCBM13);
			this.pnCopyButtons.Controls.Add(this.lbCBM10);
			this.pnCopyButtons.Controls.Add(this.lbCBM12);
			this.pnCopyButtons.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnCopyButtons.Dock")));
			this.pnCopyButtons.Enabled = ((bool)(resources.GetObject("pnCopyButtons.Enabled")));
			this.pnCopyButtons.Font = ((System.Drawing.Font)(resources.GetObject("pnCopyButtons.Font")));
			this.pnCopyButtons.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnCopyButtons.ImeMode")));
			this.pnCopyButtons.Location = ((System.Drawing.Point)(resources.GetObject("pnCopyButtons.Location")));
			this.pnCopyButtons.Name = "pnCopyButtons";
			this.pnCopyButtons.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnCopyButtons.RightToLeft")));
			this.pnCopyButtons.Size = ((System.Drawing.Size)(resources.GetObject("pnCopyButtons.Size")));
			this.pnCopyButtons.TabIndex = ((int)(resources.GetObject("pnCopyButtons.TabIndex")));
			this.pnCopyButtons.Text = resources.GetString("pnCopyButtons.Text");
			this.pnCopyButtons.Visible = ((bool)(resources.GetObject("pnCopyButtons.Visible")));
			// 
			// btnCopyAll
			// 
			this.btnCopyAll.AccessibleDescription = resources.GetString("btnCopyAll.AccessibleDescription");
			this.btnCopyAll.AccessibleName = resources.GetString("btnCopyAll.AccessibleName");
			this.btnCopyAll.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCopyAll.Anchor")));
			this.btnCopyAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCopyAll.BackgroundImage")));
			this.btnCopyAll.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCopyAll.Dock")));
			this.btnCopyAll.Enabled = ((bool)(resources.GetObject("btnCopyAll.Enabled")));
			this.btnCopyAll.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCopyAll.FlatStyle")));
			this.btnCopyAll.Font = ((System.Drawing.Font)(resources.GetObject("btnCopyAll.Font")));
			this.btnCopyAll.Image = ((System.Drawing.Image)(resources.GetObject("btnCopyAll.Image")));
			this.btnCopyAll.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCopyAll.ImageAlign")));
			this.btnCopyAll.ImageIndex = ((int)(resources.GetObject("btnCopyAll.ImageIndex")));
			this.btnCopyAll.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCopyAll.ImeMode")));
			this.btnCopyAll.Location = ((System.Drawing.Point)(resources.GetObject("btnCopyAll.Location")));
			this.btnCopyAll.Name = "btnCopyAll";
			this.btnCopyAll.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCopyAll.RightToLeft")));
			this.btnCopyAll.Size = ((System.Drawing.Size)(resources.GetObject("btnCopyAll.Size")));
			this.btnCopyAll.TabIndex = ((int)(resources.GetObject("btnCopyAll.TabIndex")));
			this.btnCopyAll.Text = resources.GetString("btnCopyAll.Text");
			this.btnCopyAll.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCopyAll.TextAlign")));
			this.btnCopyAll.Visible = ((bool)(resources.GetObject("btnCopyAll.Visible")));
			this.btnCopyAll.Click += new System.EventHandler(this.copy_Click);
			// 
			// lbCBM0
			// 
			this.lbCBM0.AccessibleDescription = resources.GetString("lbCBM0.AccessibleDescription");
			this.lbCBM0.AccessibleName = resources.GetString("lbCBM0.AccessibleName");
			this.lbCBM0.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbCBM0.Anchor")));
			this.lbCBM0.AutoSize = ((bool)(resources.GetObject("lbCBM0.AutoSize")));
			this.lbCBM0.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbCBM0.Dock")));
			this.lbCBM0.Enabled = ((bool)(resources.GetObject("lbCBM0.Enabled")));
			this.lbCBM0.Font = ((System.Drawing.Font)(resources.GetObject("lbCBM0.Font")));
			this.lbCBM0.Image = ((System.Drawing.Image)(resources.GetObject("lbCBM0.Image")));
			this.lbCBM0.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM0.ImageAlign")));
			this.lbCBM0.ImageIndex = ((int)(resources.GetObject("lbCBM0.ImageIndex")));
			this.lbCBM0.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbCBM0.ImeMode")));
			this.lbCBM0.Location = ((System.Drawing.Point)(resources.GetObject("lbCBM0.Location")));
			this.lbCBM0.Name = "lbCBM0";
			this.lbCBM0.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbCBM0.RightToLeft")));
			this.lbCBM0.Size = ((System.Drawing.Size)(resources.GetObject("lbCBM0.Size")));
			this.lbCBM0.TabIndex = ((int)(resources.GetObject("lbCBM0.TabIndex")));
			this.lbCBM0.Text = resources.GetString("lbCBM0.Text");
			this.lbCBM0.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM0.TextAlign")));
			this.lbCBM0.Visible = ((bool)(resources.GetObject("lbCBM0.Visible")));
			// 
			// btnCpyM0
			// 
			this.btnCpyM0.AccessibleDescription = resources.GetString("btnCpyM0.AccessibleDescription");
			this.btnCpyM0.AccessibleName = resources.GetString("btnCpyM0.AccessibleName");
			this.btnCpyM0.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCpyM0.Anchor")));
			this.btnCpyM0.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCpyM0.BackgroundImage")));
			this.btnCpyM0.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCpyM0.Dock")));
			this.btnCpyM0.Enabled = ((bool)(resources.GetObject("btnCpyM0.Enabled")));
			this.btnCpyM0.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCpyM0.FlatStyle")));
			this.btnCpyM0.Font = ((System.Drawing.Font)(resources.GetObject("btnCpyM0.Font")));
			this.btnCpyM0.Image = ((System.Drawing.Image)(resources.GetObject("btnCpyM0.Image")));
			this.btnCpyM0.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM0.ImageAlign")));
			this.btnCpyM0.ImageIndex = ((int)(resources.GetObject("btnCpyM0.ImageIndex")));
			this.btnCpyM0.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCpyM0.ImeMode")));
			this.btnCpyM0.Location = ((System.Drawing.Point)(resources.GetObject("btnCpyM0.Location")));
			this.btnCpyM0.Name = "btnCpyM0";
			this.btnCpyM0.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCpyM0.RightToLeft")));
			this.btnCpyM0.Size = ((System.Drawing.Size)(resources.GetObject("btnCpyM0.Size")));
			this.btnCpyM0.TabIndex = ((int)(resources.GetObject("btnCpyM0.TabIndex")));
			this.btnCpyM0.Text = resources.GetString("btnCpyM0.Text");
			this.btnCpyM0.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM0.TextAlign")));
			this.btnCpyM0.Visible = ((bool)(resources.GetObject("btnCpyM0.Visible")));
			this.btnCpyM0.Click += new System.EventHandler(this.copy_Click);
			// 
			// btnCpyM1
			// 
			this.btnCpyM1.AccessibleDescription = resources.GetString("btnCpyM1.AccessibleDescription");
			this.btnCpyM1.AccessibleName = resources.GetString("btnCpyM1.AccessibleName");
			this.btnCpyM1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCpyM1.Anchor")));
			this.btnCpyM1.BackColor = System.Drawing.SystemColors.Control;
			this.btnCpyM1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCpyM1.BackgroundImage")));
			this.btnCpyM1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCpyM1.Dock")));
			this.btnCpyM1.Enabled = ((bool)(resources.GetObject("btnCpyM1.Enabled")));
			this.btnCpyM1.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCpyM1.FlatStyle")));
			this.btnCpyM1.Font = ((System.Drawing.Font)(resources.GetObject("btnCpyM1.Font")));
			this.btnCpyM1.Image = ((System.Drawing.Image)(resources.GetObject("btnCpyM1.Image")));
			this.btnCpyM1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM1.ImageAlign")));
			this.btnCpyM1.ImageIndex = ((int)(resources.GetObject("btnCpyM1.ImageIndex")));
			this.btnCpyM1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCpyM1.ImeMode")));
			this.btnCpyM1.Location = ((System.Drawing.Point)(resources.GetObject("btnCpyM1.Location")));
			this.btnCpyM1.Name = "btnCpyM1";
			this.btnCpyM1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCpyM1.RightToLeft")));
			this.btnCpyM1.Size = ((System.Drawing.Size)(resources.GetObject("btnCpyM1.Size")));
			this.btnCpyM1.TabIndex = ((int)(resources.GetObject("btnCpyM1.TabIndex")));
			this.btnCpyM1.Text = resources.GetString("btnCpyM1.Text");
			this.btnCpyM1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM1.TextAlign")));
			this.btnCpyM1.Visible = ((bool)(resources.GetObject("btnCpyM1.Visible")));
			this.btnCpyM1.Click += new System.EventHandler(this.copy_Click);
			// 
			// btnCpyM2
			// 
			this.btnCpyM2.AccessibleDescription = resources.GetString("btnCpyM2.AccessibleDescription");
			this.btnCpyM2.AccessibleName = resources.GetString("btnCpyM2.AccessibleName");
			this.btnCpyM2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCpyM2.Anchor")));
			this.btnCpyM2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCpyM2.BackgroundImage")));
			this.btnCpyM2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCpyM2.Dock")));
			this.btnCpyM2.Enabled = ((bool)(resources.GetObject("btnCpyM2.Enabled")));
			this.btnCpyM2.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCpyM2.FlatStyle")));
			this.btnCpyM2.Font = ((System.Drawing.Font)(resources.GetObject("btnCpyM2.Font")));
			this.btnCpyM2.Image = ((System.Drawing.Image)(resources.GetObject("btnCpyM2.Image")));
			this.btnCpyM2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM2.ImageAlign")));
			this.btnCpyM2.ImageIndex = ((int)(resources.GetObject("btnCpyM2.ImageIndex")));
			this.btnCpyM2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCpyM2.ImeMode")));
			this.btnCpyM2.Location = ((System.Drawing.Point)(resources.GetObject("btnCpyM2.Location")));
			this.btnCpyM2.Name = "btnCpyM2";
			this.btnCpyM2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCpyM2.RightToLeft")));
			this.btnCpyM2.Size = ((System.Drawing.Size)(resources.GetObject("btnCpyM2.Size")));
			this.btnCpyM2.TabIndex = ((int)(resources.GetObject("btnCpyM2.TabIndex")));
			this.btnCpyM2.Text = resources.GetString("btnCpyM2.Text");
			this.btnCpyM2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM2.TextAlign")));
			this.btnCpyM2.Visible = ((bool)(resources.GetObject("btnCpyM2.Visible")));
			this.btnCpyM2.Click += new System.EventHandler(this.copy_Click);
			// 
			// btnCpyM3
			// 
			this.btnCpyM3.AccessibleDescription = resources.GetString("btnCpyM3.AccessibleDescription");
			this.btnCpyM3.AccessibleName = resources.GetString("btnCpyM3.AccessibleName");
			this.btnCpyM3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCpyM3.Anchor")));
			this.btnCpyM3.BackColor = System.Drawing.SystemColors.Control;
			this.btnCpyM3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCpyM3.BackgroundImage")));
			this.btnCpyM3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCpyM3.Dock")));
			this.btnCpyM3.Enabled = ((bool)(resources.GetObject("btnCpyM3.Enabled")));
			this.btnCpyM3.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCpyM3.FlatStyle")));
			this.btnCpyM3.Font = ((System.Drawing.Font)(resources.GetObject("btnCpyM3.Font")));
			this.btnCpyM3.Image = ((System.Drawing.Image)(resources.GetObject("btnCpyM3.Image")));
			this.btnCpyM3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM3.ImageAlign")));
			this.btnCpyM3.ImageIndex = ((int)(resources.GetObject("btnCpyM3.ImageIndex")));
			this.btnCpyM3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCpyM3.ImeMode")));
			this.btnCpyM3.Location = ((System.Drawing.Point)(resources.GetObject("btnCpyM3.Location")));
			this.btnCpyM3.Name = "btnCpyM3";
			this.btnCpyM3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCpyM3.RightToLeft")));
			this.btnCpyM3.Size = ((System.Drawing.Size)(resources.GetObject("btnCpyM3.Size")));
			this.btnCpyM3.TabIndex = ((int)(resources.GetObject("btnCpyM3.TabIndex")));
			this.btnCpyM3.Text = resources.GetString("btnCpyM3.Text");
			this.btnCpyM3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM3.TextAlign")));
			this.btnCpyM3.Visible = ((bool)(resources.GetObject("btnCpyM3.Visible")));
			this.btnCpyM3.Click += new System.EventHandler(this.copy_Click);
			// 
			// btnCpyM4
			// 
			this.btnCpyM4.AccessibleDescription = resources.GetString("btnCpyM4.AccessibleDescription");
			this.btnCpyM4.AccessibleName = resources.GetString("btnCpyM4.AccessibleName");
			this.btnCpyM4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCpyM4.Anchor")));
			this.btnCpyM4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCpyM4.BackgroundImage")));
			this.btnCpyM4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCpyM4.Dock")));
			this.btnCpyM4.Enabled = ((bool)(resources.GetObject("btnCpyM4.Enabled")));
			this.btnCpyM4.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCpyM4.FlatStyle")));
			this.btnCpyM4.Font = ((System.Drawing.Font)(resources.GetObject("btnCpyM4.Font")));
			this.btnCpyM4.Image = ((System.Drawing.Image)(resources.GetObject("btnCpyM4.Image")));
			this.btnCpyM4.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM4.ImageAlign")));
			this.btnCpyM4.ImageIndex = ((int)(resources.GetObject("btnCpyM4.ImageIndex")));
			this.btnCpyM4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCpyM4.ImeMode")));
			this.btnCpyM4.Location = ((System.Drawing.Point)(resources.GetObject("btnCpyM4.Location")));
			this.btnCpyM4.Name = "btnCpyM4";
			this.btnCpyM4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCpyM4.RightToLeft")));
			this.btnCpyM4.Size = ((System.Drawing.Size)(resources.GetObject("btnCpyM4.Size")));
			this.btnCpyM4.TabIndex = ((int)(resources.GetObject("btnCpyM4.TabIndex")));
			this.btnCpyM4.Text = resources.GetString("btnCpyM4.Text");
			this.btnCpyM4.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM4.TextAlign")));
			this.btnCpyM4.Visible = ((bool)(resources.GetObject("btnCpyM4.Visible")));
			this.btnCpyM4.Click += new System.EventHandler(this.copy_Click);
			// 
			// btnCpyM5
			// 
			this.btnCpyM5.AccessibleDescription = resources.GetString("btnCpyM5.AccessibleDescription");
			this.btnCpyM5.AccessibleName = resources.GetString("btnCpyM5.AccessibleName");
			this.btnCpyM5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCpyM5.Anchor")));
			this.btnCpyM5.BackColor = System.Drawing.SystemColors.Control;
			this.btnCpyM5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCpyM5.BackgroundImage")));
			this.btnCpyM5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCpyM5.Dock")));
			this.btnCpyM5.Enabled = ((bool)(resources.GetObject("btnCpyM5.Enabled")));
			this.btnCpyM5.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCpyM5.FlatStyle")));
			this.btnCpyM5.Font = ((System.Drawing.Font)(resources.GetObject("btnCpyM5.Font")));
			this.btnCpyM5.Image = ((System.Drawing.Image)(resources.GetObject("btnCpyM5.Image")));
			this.btnCpyM5.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM5.ImageAlign")));
			this.btnCpyM5.ImageIndex = ((int)(resources.GetObject("btnCpyM5.ImageIndex")));
			this.btnCpyM5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCpyM5.ImeMode")));
			this.btnCpyM5.Location = ((System.Drawing.Point)(resources.GetObject("btnCpyM5.Location")));
			this.btnCpyM5.Name = "btnCpyM5";
			this.btnCpyM5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCpyM5.RightToLeft")));
			this.btnCpyM5.Size = ((System.Drawing.Size)(resources.GetObject("btnCpyM5.Size")));
			this.btnCpyM5.TabIndex = ((int)(resources.GetObject("btnCpyM5.TabIndex")));
			this.btnCpyM5.Text = resources.GetString("btnCpyM5.Text");
			this.btnCpyM5.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM5.TextAlign")));
			this.btnCpyM5.Visible = ((bool)(resources.GetObject("btnCpyM5.Visible")));
			this.btnCpyM5.Click += new System.EventHandler(this.copy_Click);
			// 
			// btnCpyM7
			// 
			this.btnCpyM7.AccessibleDescription = resources.GetString("btnCpyM7.AccessibleDescription");
			this.btnCpyM7.AccessibleName = resources.GetString("btnCpyM7.AccessibleName");
			this.btnCpyM7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCpyM7.Anchor")));
			this.btnCpyM7.BackColor = System.Drawing.SystemColors.Control;
			this.btnCpyM7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCpyM7.BackgroundImage")));
			this.btnCpyM7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCpyM7.Dock")));
			this.btnCpyM7.Enabled = ((bool)(resources.GetObject("btnCpyM7.Enabled")));
			this.btnCpyM7.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCpyM7.FlatStyle")));
			this.btnCpyM7.Font = ((System.Drawing.Font)(resources.GetObject("btnCpyM7.Font")));
			this.btnCpyM7.Image = ((System.Drawing.Image)(resources.GetObject("btnCpyM7.Image")));
			this.btnCpyM7.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM7.ImageAlign")));
			this.btnCpyM7.ImageIndex = ((int)(resources.GetObject("btnCpyM7.ImageIndex")));
			this.btnCpyM7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCpyM7.ImeMode")));
			this.btnCpyM7.Location = ((System.Drawing.Point)(resources.GetObject("btnCpyM7.Location")));
			this.btnCpyM7.Name = "btnCpyM7";
			this.btnCpyM7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCpyM7.RightToLeft")));
			this.btnCpyM7.Size = ((System.Drawing.Size)(resources.GetObject("btnCpyM7.Size")));
			this.btnCpyM7.TabIndex = ((int)(resources.GetObject("btnCpyM7.TabIndex")));
			this.btnCpyM7.Text = resources.GetString("btnCpyM7.Text");
			this.btnCpyM7.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM7.TextAlign")));
			this.btnCpyM7.Visible = ((bool)(resources.GetObject("btnCpyM7.Visible")));
			this.btnCpyM7.Click += new System.EventHandler(this.copy_Click);
			// 
			// btnCpyM6
			// 
			this.btnCpyM6.AccessibleDescription = resources.GetString("btnCpyM6.AccessibleDescription");
			this.btnCpyM6.AccessibleName = resources.GetString("btnCpyM6.AccessibleName");
			this.btnCpyM6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCpyM6.Anchor")));
			this.btnCpyM6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCpyM6.BackgroundImage")));
			this.btnCpyM6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCpyM6.Dock")));
			this.btnCpyM6.Enabled = ((bool)(resources.GetObject("btnCpyM6.Enabled")));
			this.btnCpyM6.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCpyM6.FlatStyle")));
			this.btnCpyM6.Font = ((System.Drawing.Font)(resources.GetObject("btnCpyM6.Font")));
			this.btnCpyM6.Image = ((System.Drawing.Image)(resources.GetObject("btnCpyM6.Image")));
			this.btnCpyM6.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM6.ImageAlign")));
			this.btnCpyM6.ImageIndex = ((int)(resources.GetObject("btnCpyM6.ImageIndex")));
			this.btnCpyM6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCpyM6.ImeMode")));
			this.btnCpyM6.Location = ((System.Drawing.Point)(resources.GetObject("btnCpyM6.Location")));
			this.btnCpyM6.Name = "btnCpyM6";
			this.btnCpyM6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCpyM6.RightToLeft")));
			this.btnCpyM6.Size = ((System.Drawing.Size)(resources.GetObject("btnCpyM6.Size")));
			this.btnCpyM6.TabIndex = ((int)(resources.GetObject("btnCpyM6.TabIndex")));
			this.btnCpyM6.Text = resources.GetString("btnCpyM6.Text");
			this.btnCpyM6.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM6.TextAlign")));
			this.btnCpyM6.Visible = ((bool)(resources.GetObject("btnCpyM6.Visible")));
			this.btnCpyM6.Click += new System.EventHandler(this.copy_Click);
			// 
			// btnCpyM9
			// 
			this.btnCpyM9.AccessibleDescription = resources.GetString("btnCpyM9.AccessibleDescription");
			this.btnCpyM9.AccessibleName = resources.GetString("btnCpyM9.AccessibleName");
			this.btnCpyM9.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCpyM9.Anchor")));
			this.btnCpyM9.BackColor = System.Drawing.SystemColors.Control;
			this.btnCpyM9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCpyM9.BackgroundImage")));
			this.btnCpyM9.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCpyM9.Dock")));
			this.btnCpyM9.Enabled = ((bool)(resources.GetObject("btnCpyM9.Enabled")));
			this.btnCpyM9.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCpyM9.FlatStyle")));
			this.btnCpyM9.Font = ((System.Drawing.Font)(resources.GetObject("btnCpyM9.Font")));
			this.btnCpyM9.Image = ((System.Drawing.Image)(resources.GetObject("btnCpyM9.Image")));
			this.btnCpyM9.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM9.ImageAlign")));
			this.btnCpyM9.ImageIndex = ((int)(resources.GetObject("btnCpyM9.ImageIndex")));
			this.btnCpyM9.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCpyM9.ImeMode")));
			this.btnCpyM9.Location = ((System.Drawing.Point)(resources.GetObject("btnCpyM9.Location")));
			this.btnCpyM9.Name = "btnCpyM9";
			this.btnCpyM9.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCpyM9.RightToLeft")));
			this.btnCpyM9.Size = ((System.Drawing.Size)(resources.GetObject("btnCpyM9.Size")));
			this.btnCpyM9.TabIndex = ((int)(resources.GetObject("btnCpyM9.TabIndex")));
			this.btnCpyM9.Text = resources.GetString("btnCpyM9.Text");
			this.btnCpyM9.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM9.TextAlign")));
			this.btnCpyM9.Visible = ((bool)(resources.GetObject("btnCpyM9.Visible")));
			this.btnCpyM9.Click += new System.EventHandler(this.copy_Click);
			// 
			// btnCpyM12
			// 
			this.btnCpyM12.AccessibleDescription = resources.GetString("btnCpyM12.AccessibleDescription");
			this.btnCpyM12.AccessibleName = resources.GetString("btnCpyM12.AccessibleName");
			this.btnCpyM12.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCpyM12.Anchor")));
			this.btnCpyM12.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCpyM12.BackgroundImage")));
			this.btnCpyM12.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCpyM12.Dock")));
			this.btnCpyM12.Enabled = ((bool)(resources.GetObject("btnCpyM12.Enabled")));
			this.btnCpyM12.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCpyM12.FlatStyle")));
			this.btnCpyM12.Font = ((System.Drawing.Font)(resources.GetObject("btnCpyM12.Font")));
			this.btnCpyM12.Image = ((System.Drawing.Image)(resources.GetObject("btnCpyM12.Image")));
			this.btnCpyM12.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM12.ImageAlign")));
			this.btnCpyM12.ImageIndex = ((int)(resources.GetObject("btnCpyM12.ImageIndex")));
			this.btnCpyM12.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCpyM12.ImeMode")));
			this.btnCpyM12.Location = ((System.Drawing.Point)(resources.GetObject("btnCpyM12.Location")));
			this.btnCpyM12.Name = "btnCpyM12";
			this.btnCpyM12.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCpyM12.RightToLeft")));
			this.btnCpyM12.Size = ((System.Drawing.Size)(resources.GetObject("btnCpyM12.Size")));
			this.btnCpyM12.TabIndex = ((int)(resources.GetObject("btnCpyM12.TabIndex")));
			this.btnCpyM12.Text = resources.GetString("btnCpyM12.Text");
			this.btnCpyM12.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM12.TextAlign")));
			this.btnCpyM12.Visible = ((bool)(resources.GetObject("btnCpyM12.Visible")));
			this.btnCpyM12.Click += new System.EventHandler(this.copy_Click);
			// 
			// btnCpyM11
			// 
			this.btnCpyM11.AccessibleDescription = resources.GetString("btnCpyM11.AccessibleDescription");
			this.btnCpyM11.AccessibleName = resources.GetString("btnCpyM11.AccessibleName");
			this.btnCpyM11.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCpyM11.Anchor")));
			this.btnCpyM11.BackColor = System.Drawing.SystemColors.Control;
			this.btnCpyM11.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCpyM11.BackgroundImage")));
			this.btnCpyM11.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCpyM11.Dock")));
			this.btnCpyM11.Enabled = ((bool)(resources.GetObject("btnCpyM11.Enabled")));
			this.btnCpyM11.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCpyM11.FlatStyle")));
			this.btnCpyM11.Font = ((System.Drawing.Font)(resources.GetObject("btnCpyM11.Font")));
			this.btnCpyM11.Image = ((System.Drawing.Image)(resources.GetObject("btnCpyM11.Image")));
			this.btnCpyM11.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM11.ImageAlign")));
			this.btnCpyM11.ImageIndex = ((int)(resources.GetObject("btnCpyM11.ImageIndex")));
			this.btnCpyM11.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCpyM11.ImeMode")));
			this.btnCpyM11.Location = ((System.Drawing.Point)(resources.GetObject("btnCpyM11.Location")));
			this.btnCpyM11.Name = "btnCpyM11";
			this.btnCpyM11.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCpyM11.RightToLeft")));
			this.btnCpyM11.Size = ((System.Drawing.Size)(resources.GetObject("btnCpyM11.Size")));
			this.btnCpyM11.TabIndex = ((int)(resources.GetObject("btnCpyM11.TabIndex")));
			this.btnCpyM11.Text = resources.GetString("btnCpyM11.Text");
			this.btnCpyM11.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM11.TextAlign")));
			this.btnCpyM11.Visible = ((bool)(resources.GetObject("btnCpyM11.Visible")));
			this.btnCpyM11.Click += new System.EventHandler(this.copy_Click);
			// 
			// btnCpyM10
			// 
			this.btnCpyM10.AccessibleDescription = resources.GetString("btnCpyM10.AccessibleDescription");
			this.btnCpyM10.AccessibleName = resources.GetString("btnCpyM10.AccessibleName");
			this.btnCpyM10.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCpyM10.Anchor")));
			this.btnCpyM10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCpyM10.BackgroundImage")));
			this.btnCpyM10.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCpyM10.Dock")));
			this.btnCpyM10.Enabled = ((bool)(resources.GetObject("btnCpyM10.Enabled")));
			this.btnCpyM10.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCpyM10.FlatStyle")));
			this.btnCpyM10.Font = ((System.Drawing.Font)(resources.GetObject("btnCpyM10.Font")));
			this.btnCpyM10.Image = ((System.Drawing.Image)(resources.GetObject("btnCpyM10.Image")));
			this.btnCpyM10.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM10.ImageAlign")));
			this.btnCpyM10.ImageIndex = ((int)(resources.GetObject("btnCpyM10.ImageIndex")));
			this.btnCpyM10.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCpyM10.ImeMode")));
			this.btnCpyM10.Location = ((System.Drawing.Point)(resources.GetObject("btnCpyM10.Location")));
			this.btnCpyM10.Name = "btnCpyM10";
			this.btnCpyM10.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCpyM10.RightToLeft")));
			this.btnCpyM10.Size = ((System.Drawing.Size)(resources.GetObject("btnCpyM10.Size")));
			this.btnCpyM10.TabIndex = ((int)(resources.GetObject("btnCpyM10.TabIndex")));
			this.btnCpyM10.Text = resources.GetString("btnCpyM10.Text");
			this.btnCpyM10.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM10.TextAlign")));
			this.btnCpyM10.Visible = ((bool)(resources.GetObject("btnCpyM10.Visible")));
			this.btnCpyM10.Click += new System.EventHandler(this.copy_Click);
			// 
			// btnCpyM15
			// 
			this.btnCpyM15.AccessibleDescription = resources.GetString("btnCpyM15.AccessibleDescription");
			this.btnCpyM15.AccessibleName = resources.GetString("btnCpyM15.AccessibleName");
			this.btnCpyM15.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCpyM15.Anchor")));
			this.btnCpyM15.BackColor = System.Drawing.SystemColors.Control;
			this.btnCpyM15.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCpyM15.BackgroundImage")));
			this.btnCpyM15.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCpyM15.Dock")));
			this.btnCpyM15.Enabled = ((bool)(resources.GetObject("btnCpyM15.Enabled")));
			this.btnCpyM15.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCpyM15.FlatStyle")));
			this.btnCpyM15.Font = ((System.Drawing.Font)(resources.GetObject("btnCpyM15.Font")));
			this.btnCpyM15.Image = ((System.Drawing.Image)(resources.GetObject("btnCpyM15.Image")));
			this.btnCpyM15.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM15.ImageAlign")));
			this.btnCpyM15.ImageIndex = ((int)(resources.GetObject("btnCpyM15.ImageIndex")));
			this.btnCpyM15.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCpyM15.ImeMode")));
			this.btnCpyM15.Location = ((System.Drawing.Point)(resources.GetObject("btnCpyM15.Location")));
			this.btnCpyM15.Name = "btnCpyM15";
			this.btnCpyM15.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCpyM15.RightToLeft")));
			this.btnCpyM15.Size = ((System.Drawing.Size)(resources.GetObject("btnCpyM15.Size")));
			this.btnCpyM15.TabIndex = ((int)(resources.GetObject("btnCpyM15.TabIndex")));
			this.btnCpyM15.Text = resources.GetString("btnCpyM15.Text");
			this.btnCpyM15.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM15.TextAlign")));
			this.btnCpyM15.Visible = ((bool)(resources.GetObject("btnCpyM15.Visible")));
			this.btnCpyM15.Click += new System.EventHandler(this.copy_Click);
			// 
			// btnCpyM14
			// 
			this.btnCpyM14.AccessibleDescription = resources.GetString("btnCpyM14.AccessibleDescription");
			this.btnCpyM14.AccessibleName = resources.GetString("btnCpyM14.AccessibleName");
			this.btnCpyM14.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCpyM14.Anchor")));
			this.btnCpyM14.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCpyM14.BackgroundImage")));
			this.btnCpyM14.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCpyM14.Dock")));
			this.btnCpyM14.Enabled = ((bool)(resources.GetObject("btnCpyM14.Enabled")));
			this.btnCpyM14.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCpyM14.FlatStyle")));
			this.btnCpyM14.Font = ((System.Drawing.Font)(resources.GetObject("btnCpyM14.Font")));
			this.btnCpyM14.Image = ((System.Drawing.Image)(resources.GetObject("btnCpyM14.Image")));
			this.btnCpyM14.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM14.ImageAlign")));
			this.btnCpyM14.ImageIndex = ((int)(resources.GetObject("btnCpyM14.ImageIndex")));
			this.btnCpyM14.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCpyM14.ImeMode")));
			this.btnCpyM14.Location = ((System.Drawing.Point)(resources.GetObject("btnCpyM14.Location")));
			this.btnCpyM14.Name = "btnCpyM14";
			this.btnCpyM14.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCpyM14.RightToLeft")));
			this.btnCpyM14.Size = ((System.Drawing.Size)(resources.GetObject("btnCpyM14.Size")));
			this.btnCpyM14.TabIndex = ((int)(resources.GetObject("btnCpyM14.TabIndex")));
			this.btnCpyM14.Text = resources.GetString("btnCpyM14.Text");
			this.btnCpyM14.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM14.TextAlign")));
			this.btnCpyM14.Visible = ((bool)(resources.GetObject("btnCpyM14.Visible")));
			this.btnCpyM14.Click += new System.EventHandler(this.copy_Click);
			// 
			// btnCpyM13
			// 
			this.btnCpyM13.AccessibleDescription = resources.GetString("btnCpyM13.AccessibleDescription");
			this.btnCpyM13.AccessibleName = resources.GetString("btnCpyM13.AccessibleName");
			this.btnCpyM13.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCpyM13.Anchor")));
			this.btnCpyM13.BackColor = System.Drawing.SystemColors.Control;
			this.btnCpyM13.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCpyM13.BackgroundImage")));
			this.btnCpyM13.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCpyM13.Dock")));
			this.btnCpyM13.Enabled = ((bool)(resources.GetObject("btnCpyM13.Enabled")));
			this.btnCpyM13.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCpyM13.FlatStyle")));
			this.btnCpyM13.Font = ((System.Drawing.Font)(resources.GetObject("btnCpyM13.Font")));
			this.btnCpyM13.Image = ((System.Drawing.Image)(resources.GetObject("btnCpyM13.Image")));
			this.btnCpyM13.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM13.ImageAlign")));
			this.btnCpyM13.ImageIndex = ((int)(resources.GetObject("btnCpyM13.ImageIndex")));
			this.btnCpyM13.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCpyM13.ImeMode")));
			this.btnCpyM13.Location = ((System.Drawing.Point)(resources.GetObject("btnCpyM13.Location")));
			this.btnCpyM13.Name = "btnCpyM13";
			this.btnCpyM13.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCpyM13.RightToLeft")));
			this.btnCpyM13.Size = ((System.Drawing.Size)(resources.GetObject("btnCpyM13.Size")));
			this.btnCpyM13.TabIndex = ((int)(resources.GetObject("btnCpyM13.TabIndex")));
			this.btnCpyM13.Text = resources.GetString("btnCpyM13.Text");
			this.btnCpyM13.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM13.TextAlign")));
			this.btnCpyM13.Visible = ((bool)(resources.GetObject("btnCpyM13.Visible")));
			this.btnCpyM13.Click += new System.EventHandler(this.copy_Click);
			// 
			// btnCpyM8
			// 
			this.btnCpyM8.AccessibleDescription = resources.GetString("btnCpyM8.AccessibleDescription");
			this.btnCpyM8.AccessibleName = resources.GetString("btnCpyM8.AccessibleName");
			this.btnCpyM8.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCpyM8.Anchor")));
			this.btnCpyM8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCpyM8.BackgroundImage")));
			this.btnCpyM8.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCpyM8.Dock")));
			this.btnCpyM8.Enabled = ((bool)(resources.GetObject("btnCpyM8.Enabled")));
			this.btnCpyM8.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCpyM8.FlatStyle")));
			this.btnCpyM8.Font = ((System.Drawing.Font)(resources.GetObject("btnCpyM8.Font")));
			this.btnCpyM8.Image = ((System.Drawing.Image)(resources.GetObject("btnCpyM8.Image")));
			this.btnCpyM8.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM8.ImageAlign")));
			this.btnCpyM8.ImageIndex = ((int)(resources.GetObject("btnCpyM8.ImageIndex")));
			this.btnCpyM8.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCpyM8.ImeMode")));
			this.btnCpyM8.Location = ((System.Drawing.Point)(resources.GetObject("btnCpyM8.Location")));
			this.btnCpyM8.Name = "btnCpyM8";
			this.btnCpyM8.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCpyM8.RightToLeft")));
			this.btnCpyM8.Size = ((System.Drawing.Size)(resources.GetObject("btnCpyM8.Size")));
			this.btnCpyM8.TabIndex = ((int)(resources.GetObject("btnCpyM8.TabIndex")));
			this.btnCpyM8.Text = resources.GetString("btnCpyM8.Text");
			this.btnCpyM8.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCpyM8.TextAlign")));
			this.btnCpyM8.Visible = ((bool)(resources.GetObject("btnCpyM8.Visible")));
			this.btnCpyM8.Click += new System.EventHandler(this.copy_Click);
			// 
			// lbCBM1
			// 
			this.lbCBM1.AccessibleDescription = resources.GetString("lbCBM1.AccessibleDescription");
			this.lbCBM1.AccessibleName = resources.GetString("lbCBM1.AccessibleName");
			this.lbCBM1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbCBM1.Anchor")));
			this.lbCBM1.AutoSize = ((bool)(resources.GetObject("lbCBM1.AutoSize")));
			this.lbCBM1.BackColor = System.Drawing.SystemColors.Control;
			this.lbCBM1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbCBM1.Dock")));
			this.lbCBM1.Enabled = ((bool)(resources.GetObject("lbCBM1.Enabled")));
			this.lbCBM1.Font = ((System.Drawing.Font)(resources.GetObject("lbCBM1.Font")));
			this.lbCBM1.Image = ((System.Drawing.Image)(resources.GetObject("lbCBM1.Image")));
			this.lbCBM1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM1.ImageAlign")));
			this.lbCBM1.ImageIndex = ((int)(resources.GetObject("lbCBM1.ImageIndex")));
			this.lbCBM1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbCBM1.ImeMode")));
			this.lbCBM1.Location = ((System.Drawing.Point)(resources.GetObject("lbCBM1.Location")));
			this.lbCBM1.Name = "lbCBM1";
			this.lbCBM1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbCBM1.RightToLeft")));
			this.lbCBM1.Size = ((System.Drawing.Size)(resources.GetObject("lbCBM1.Size")));
			this.lbCBM1.TabIndex = ((int)(resources.GetObject("lbCBM1.TabIndex")));
			this.lbCBM1.Text = resources.GetString("lbCBM1.Text");
			this.lbCBM1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM1.TextAlign")));
			this.lbCBM1.Visible = ((bool)(resources.GetObject("lbCBM1.Visible")));
			// 
			// lbCBM2
			// 
			this.lbCBM2.AccessibleDescription = resources.GetString("lbCBM2.AccessibleDescription");
			this.lbCBM2.AccessibleName = resources.GetString("lbCBM2.AccessibleName");
			this.lbCBM2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbCBM2.Anchor")));
			this.lbCBM2.AutoSize = ((bool)(resources.GetObject("lbCBM2.AutoSize")));
			this.lbCBM2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbCBM2.Dock")));
			this.lbCBM2.Enabled = ((bool)(resources.GetObject("lbCBM2.Enabled")));
			this.lbCBM2.Font = ((System.Drawing.Font)(resources.GetObject("lbCBM2.Font")));
			this.lbCBM2.Image = ((System.Drawing.Image)(resources.GetObject("lbCBM2.Image")));
			this.lbCBM2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM2.ImageAlign")));
			this.lbCBM2.ImageIndex = ((int)(resources.GetObject("lbCBM2.ImageIndex")));
			this.lbCBM2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbCBM2.ImeMode")));
			this.lbCBM2.Location = ((System.Drawing.Point)(resources.GetObject("lbCBM2.Location")));
			this.lbCBM2.Name = "lbCBM2";
			this.lbCBM2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbCBM2.RightToLeft")));
			this.lbCBM2.Size = ((System.Drawing.Size)(resources.GetObject("lbCBM2.Size")));
			this.lbCBM2.TabIndex = ((int)(resources.GetObject("lbCBM2.TabIndex")));
			this.lbCBM2.Text = resources.GetString("lbCBM2.Text");
			this.lbCBM2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM2.TextAlign")));
			this.lbCBM2.Visible = ((bool)(resources.GetObject("lbCBM2.Visible")));
			// 
			// lbCBM3
			// 
			this.lbCBM3.AccessibleDescription = resources.GetString("lbCBM3.AccessibleDescription");
			this.lbCBM3.AccessibleName = resources.GetString("lbCBM3.AccessibleName");
			this.lbCBM3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbCBM3.Anchor")));
			this.lbCBM3.AutoSize = ((bool)(resources.GetObject("lbCBM3.AutoSize")));
			this.lbCBM3.BackColor = System.Drawing.SystemColors.Control;
			this.lbCBM3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbCBM3.Dock")));
			this.lbCBM3.Enabled = ((bool)(resources.GetObject("lbCBM3.Enabled")));
			this.lbCBM3.Font = ((System.Drawing.Font)(resources.GetObject("lbCBM3.Font")));
			this.lbCBM3.Image = ((System.Drawing.Image)(resources.GetObject("lbCBM3.Image")));
			this.lbCBM3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM3.ImageAlign")));
			this.lbCBM3.ImageIndex = ((int)(resources.GetObject("lbCBM3.ImageIndex")));
			this.lbCBM3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbCBM3.ImeMode")));
			this.lbCBM3.Location = ((System.Drawing.Point)(resources.GetObject("lbCBM3.Location")));
			this.lbCBM3.Name = "lbCBM3";
			this.lbCBM3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbCBM3.RightToLeft")));
			this.lbCBM3.Size = ((System.Drawing.Size)(resources.GetObject("lbCBM3.Size")));
			this.lbCBM3.TabIndex = ((int)(resources.GetObject("lbCBM3.TabIndex")));
			this.lbCBM3.Text = resources.GetString("lbCBM3.Text");
			this.lbCBM3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM3.TextAlign")));
			this.lbCBM3.Visible = ((bool)(resources.GetObject("lbCBM3.Visible")));
			// 
			// lbCBM4
			// 
			this.lbCBM4.AccessibleDescription = resources.GetString("lbCBM4.AccessibleDescription");
			this.lbCBM4.AccessibleName = resources.GetString("lbCBM4.AccessibleName");
			this.lbCBM4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbCBM4.Anchor")));
			this.lbCBM4.AutoSize = ((bool)(resources.GetObject("lbCBM4.AutoSize")));
			this.lbCBM4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbCBM4.Dock")));
			this.lbCBM4.Enabled = ((bool)(resources.GetObject("lbCBM4.Enabled")));
			this.lbCBM4.Font = ((System.Drawing.Font)(resources.GetObject("lbCBM4.Font")));
			this.lbCBM4.Image = ((System.Drawing.Image)(resources.GetObject("lbCBM4.Image")));
			this.lbCBM4.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM4.ImageAlign")));
			this.lbCBM4.ImageIndex = ((int)(resources.GetObject("lbCBM4.ImageIndex")));
			this.lbCBM4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbCBM4.ImeMode")));
			this.lbCBM4.Location = ((System.Drawing.Point)(resources.GetObject("lbCBM4.Location")));
			this.lbCBM4.Name = "lbCBM4";
			this.lbCBM4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbCBM4.RightToLeft")));
			this.lbCBM4.Size = ((System.Drawing.Size)(resources.GetObject("lbCBM4.Size")));
			this.lbCBM4.TabIndex = ((int)(resources.GetObject("lbCBM4.TabIndex")));
			this.lbCBM4.Text = resources.GetString("lbCBM4.Text");
			this.lbCBM4.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM4.TextAlign")));
			this.lbCBM4.Visible = ((bool)(resources.GetObject("lbCBM4.Visible")));
			// 
			// lbCBM5
			// 
			this.lbCBM5.AccessibleDescription = resources.GetString("lbCBM5.AccessibleDescription");
			this.lbCBM5.AccessibleName = resources.GetString("lbCBM5.AccessibleName");
			this.lbCBM5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbCBM5.Anchor")));
			this.lbCBM5.AutoSize = ((bool)(resources.GetObject("lbCBM5.AutoSize")));
			this.lbCBM5.BackColor = System.Drawing.SystemColors.Control;
			this.lbCBM5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbCBM5.Dock")));
			this.lbCBM5.Enabled = ((bool)(resources.GetObject("lbCBM5.Enabled")));
			this.lbCBM5.Font = ((System.Drawing.Font)(resources.GetObject("lbCBM5.Font")));
			this.lbCBM5.Image = ((System.Drawing.Image)(resources.GetObject("lbCBM5.Image")));
			this.lbCBM5.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM5.ImageAlign")));
			this.lbCBM5.ImageIndex = ((int)(resources.GetObject("lbCBM5.ImageIndex")));
			this.lbCBM5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbCBM5.ImeMode")));
			this.lbCBM5.Location = ((System.Drawing.Point)(resources.GetObject("lbCBM5.Location")));
			this.lbCBM5.Name = "lbCBM5";
			this.lbCBM5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbCBM5.RightToLeft")));
			this.lbCBM5.Size = ((System.Drawing.Size)(resources.GetObject("lbCBM5.Size")));
			this.lbCBM5.TabIndex = ((int)(resources.GetObject("lbCBM5.TabIndex")));
			this.lbCBM5.Text = resources.GetString("lbCBM5.Text");
			this.lbCBM5.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM5.TextAlign")));
			this.lbCBM5.Visible = ((bool)(resources.GetObject("lbCBM5.Visible")));
			// 
			// lbCBM6
			// 
			this.lbCBM6.AccessibleDescription = resources.GetString("lbCBM6.AccessibleDescription");
			this.lbCBM6.AccessibleName = resources.GetString("lbCBM6.AccessibleName");
			this.lbCBM6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbCBM6.Anchor")));
			this.lbCBM6.AutoSize = ((bool)(resources.GetObject("lbCBM6.AutoSize")));
			this.lbCBM6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbCBM6.Dock")));
			this.lbCBM6.Enabled = ((bool)(resources.GetObject("lbCBM6.Enabled")));
			this.lbCBM6.Font = ((System.Drawing.Font)(resources.GetObject("lbCBM6.Font")));
			this.lbCBM6.Image = ((System.Drawing.Image)(resources.GetObject("lbCBM6.Image")));
			this.lbCBM6.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM6.ImageAlign")));
			this.lbCBM6.ImageIndex = ((int)(resources.GetObject("lbCBM6.ImageIndex")));
			this.lbCBM6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbCBM6.ImeMode")));
			this.lbCBM6.Location = ((System.Drawing.Point)(resources.GetObject("lbCBM6.Location")));
			this.lbCBM6.Name = "lbCBM6";
			this.lbCBM6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbCBM6.RightToLeft")));
			this.lbCBM6.Size = ((System.Drawing.Size)(resources.GetObject("lbCBM6.Size")));
			this.lbCBM6.TabIndex = ((int)(resources.GetObject("lbCBM6.TabIndex")));
			this.lbCBM6.Text = resources.GetString("lbCBM6.Text");
			this.lbCBM6.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM6.TextAlign")));
			this.lbCBM6.Visible = ((bool)(resources.GetObject("lbCBM6.Visible")));
			// 
			// lbCBM7
			// 
			this.lbCBM7.AccessibleDescription = resources.GetString("lbCBM7.AccessibleDescription");
			this.lbCBM7.AccessibleName = resources.GetString("lbCBM7.AccessibleName");
			this.lbCBM7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbCBM7.Anchor")));
			this.lbCBM7.AutoSize = ((bool)(resources.GetObject("lbCBM7.AutoSize")));
			this.lbCBM7.BackColor = System.Drawing.SystemColors.Control;
			this.lbCBM7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbCBM7.Dock")));
			this.lbCBM7.Enabled = ((bool)(resources.GetObject("lbCBM7.Enabled")));
			this.lbCBM7.Font = ((System.Drawing.Font)(resources.GetObject("lbCBM7.Font")));
			this.lbCBM7.Image = ((System.Drawing.Image)(resources.GetObject("lbCBM7.Image")));
			this.lbCBM7.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM7.ImageAlign")));
			this.lbCBM7.ImageIndex = ((int)(resources.GetObject("lbCBM7.ImageIndex")));
			this.lbCBM7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbCBM7.ImeMode")));
			this.lbCBM7.Location = ((System.Drawing.Point)(resources.GetObject("lbCBM7.Location")));
			this.lbCBM7.Name = "lbCBM7";
			this.lbCBM7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbCBM7.RightToLeft")));
			this.lbCBM7.Size = ((System.Drawing.Size)(resources.GetObject("lbCBM7.Size")));
			this.lbCBM7.TabIndex = ((int)(resources.GetObject("lbCBM7.TabIndex")));
			this.lbCBM7.Text = resources.GetString("lbCBM7.Text");
			this.lbCBM7.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM7.TextAlign")));
			this.lbCBM7.Visible = ((bool)(resources.GetObject("lbCBM7.Visible")));
			// 
			// lbCBM15
			// 
			this.lbCBM15.AccessibleDescription = resources.GetString("lbCBM15.AccessibleDescription");
			this.lbCBM15.AccessibleName = resources.GetString("lbCBM15.AccessibleName");
			this.lbCBM15.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbCBM15.Anchor")));
			this.lbCBM15.AutoSize = ((bool)(resources.GetObject("lbCBM15.AutoSize")));
			this.lbCBM15.BackColor = System.Drawing.SystemColors.Control;
			this.lbCBM15.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbCBM15.Dock")));
			this.lbCBM15.Enabled = ((bool)(resources.GetObject("lbCBM15.Enabled")));
			this.lbCBM15.Font = ((System.Drawing.Font)(resources.GetObject("lbCBM15.Font")));
			this.lbCBM15.Image = ((System.Drawing.Image)(resources.GetObject("lbCBM15.Image")));
			this.lbCBM15.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM15.ImageAlign")));
			this.lbCBM15.ImageIndex = ((int)(resources.GetObject("lbCBM15.ImageIndex")));
			this.lbCBM15.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbCBM15.ImeMode")));
			this.lbCBM15.Location = ((System.Drawing.Point)(resources.GetObject("lbCBM15.Location")));
			this.lbCBM15.Name = "lbCBM15";
			this.lbCBM15.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbCBM15.RightToLeft")));
			this.lbCBM15.Size = ((System.Drawing.Size)(resources.GetObject("lbCBM15.Size")));
			this.lbCBM15.TabIndex = ((int)(resources.GetObject("lbCBM15.TabIndex")));
			this.lbCBM15.Text = resources.GetString("lbCBM15.Text");
			this.lbCBM15.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM15.TextAlign")));
			this.lbCBM15.Visible = ((bool)(resources.GetObject("lbCBM15.Visible")));
			// 
			// lbCBM11
			// 
			this.lbCBM11.AccessibleDescription = resources.GetString("lbCBM11.AccessibleDescription");
			this.lbCBM11.AccessibleName = resources.GetString("lbCBM11.AccessibleName");
			this.lbCBM11.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbCBM11.Anchor")));
			this.lbCBM11.AutoSize = ((bool)(resources.GetObject("lbCBM11.AutoSize")));
			this.lbCBM11.BackColor = System.Drawing.SystemColors.Control;
			this.lbCBM11.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbCBM11.Dock")));
			this.lbCBM11.Enabled = ((bool)(resources.GetObject("lbCBM11.Enabled")));
			this.lbCBM11.Font = ((System.Drawing.Font)(resources.GetObject("lbCBM11.Font")));
			this.lbCBM11.Image = ((System.Drawing.Image)(resources.GetObject("lbCBM11.Image")));
			this.lbCBM11.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM11.ImageAlign")));
			this.lbCBM11.ImageIndex = ((int)(resources.GetObject("lbCBM11.ImageIndex")));
			this.lbCBM11.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbCBM11.ImeMode")));
			this.lbCBM11.Location = ((System.Drawing.Point)(resources.GetObject("lbCBM11.Location")));
			this.lbCBM11.Name = "lbCBM11";
			this.lbCBM11.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbCBM11.RightToLeft")));
			this.lbCBM11.Size = ((System.Drawing.Size)(resources.GetObject("lbCBM11.Size")));
			this.lbCBM11.TabIndex = ((int)(resources.GetObject("lbCBM11.TabIndex")));
			this.lbCBM11.Text = resources.GetString("lbCBM11.Text");
			this.lbCBM11.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM11.TextAlign")));
			this.lbCBM11.Visible = ((bool)(resources.GetObject("lbCBM11.Visible")));
			// 
			// lbCBM14
			// 
			this.lbCBM14.AccessibleDescription = resources.GetString("lbCBM14.AccessibleDescription");
			this.lbCBM14.AccessibleName = resources.GetString("lbCBM14.AccessibleName");
			this.lbCBM14.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbCBM14.Anchor")));
			this.lbCBM14.AutoSize = ((bool)(resources.GetObject("lbCBM14.AutoSize")));
			this.lbCBM14.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbCBM14.Dock")));
			this.lbCBM14.Enabled = ((bool)(resources.GetObject("lbCBM14.Enabled")));
			this.lbCBM14.Font = ((System.Drawing.Font)(resources.GetObject("lbCBM14.Font")));
			this.lbCBM14.Image = ((System.Drawing.Image)(resources.GetObject("lbCBM14.Image")));
			this.lbCBM14.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM14.ImageAlign")));
			this.lbCBM14.ImageIndex = ((int)(resources.GetObject("lbCBM14.ImageIndex")));
			this.lbCBM14.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbCBM14.ImeMode")));
			this.lbCBM14.Location = ((System.Drawing.Point)(resources.GetObject("lbCBM14.Location")));
			this.lbCBM14.Name = "lbCBM14";
			this.lbCBM14.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbCBM14.RightToLeft")));
			this.lbCBM14.Size = ((System.Drawing.Size)(resources.GetObject("lbCBM14.Size")));
			this.lbCBM14.TabIndex = ((int)(resources.GetObject("lbCBM14.TabIndex")));
			this.lbCBM14.Text = resources.GetString("lbCBM14.Text");
			this.lbCBM14.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM14.TextAlign")));
			this.lbCBM14.Visible = ((bool)(resources.GetObject("lbCBM14.Visible")));
			// 
			// lbCBM8
			// 
			this.lbCBM8.AccessibleDescription = resources.GetString("lbCBM8.AccessibleDescription");
			this.lbCBM8.AccessibleName = resources.GetString("lbCBM8.AccessibleName");
			this.lbCBM8.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbCBM8.Anchor")));
			this.lbCBM8.AutoSize = ((bool)(resources.GetObject("lbCBM8.AutoSize")));
			this.lbCBM8.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbCBM8.Dock")));
			this.lbCBM8.Enabled = ((bool)(resources.GetObject("lbCBM8.Enabled")));
			this.lbCBM8.Font = ((System.Drawing.Font)(resources.GetObject("lbCBM8.Font")));
			this.lbCBM8.Image = ((System.Drawing.Image)(resources.GetObject("lbCBM8.Image")));
			this.lbCBM8.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM8.ImageAlign")));
			this.lbCBM8.ImageIndex = ((int)(resources.GetObject("lbCBM8.ImageIndex")));
			this.lbCBM8.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbCBM8.ImeMode")));
			this.lbCBM8.Location = ((System.Drawing.Point)(resources.GetObject("lbCBM8.Location")));
			this.lbCBM8.Name = "lbCBM8";
			this.lbCBM8.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbCBM8.RightToLeft")));
			this.lbCBM8.Size = ((System.Drawing.Size)(resources.GetObject("lbCBM8.Size")));
			this.lbCBM8.TabIndex = ((int)(resources.GetObject("lbCBM8.TabIndex")));
			this.lbCBM8.Text = resources.GetString("lbCBM8.Text");
			this.lbCBM8.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM8.TextAlign")));
			this.lbCBM8.Visible = ((bool)(resources.GetObject("lbCBM8.Visible")));
			// 
			// lbCBM9
			// 
			this.lbCBM9.AccessibleDescription = resources.GetString("lbCBM9.AccessibleDescription");
			this.lbCBM9.AccessibleName = resources.GetString("lbCBM9.AccessibleName");
			this.lbCBM9.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbCBM9.Anchor")));
			this.lbCBM9.AutoSize = ((bool)(resources.GetObject("lbCBM9.AutoSize")));
			this.lbCBM9.BackColor = System.Drawing.SystemColors.Control;
			this.lbCBM9.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbCBM9.Dock")));
			this.lbCBM9.Enabled = ((bool)(resources.GetObject("lbCBM9.Enabled")));
			this.lbCBM9.Font = ((System.Drawing.Font)(resources.GetObject("lbCBM9.Font")));
			this.lbCBM9.Image = ((System.Drawing.Image)(resources.GetObject("lbCBM9.Image")));
			this.lbCBM9.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM9.ImageAlign")));
			this.lbCBM9.ImageIndex = ((int)(resources.GetObject("lbCBM9.ImageIndex")));
			this.lbCBM9.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbCBM9.ImeMode")));
			this.lbCBM9.Location = ((System.Drawing.Point)(resources.GetObject("lbCBM9.Location")));
			this.lbCBM9.Name = "lbCBM9";
			this.lbCBM9.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbCBM9.RightToLeft")));
			this.lbCBM9.Size = ((System.Drawing.Size)(resources.GetObject("lbCBM9.Size")));
			this.lbCBM9.TabIndex = ((int)(resources.GetObject("lbCBM9.TabIndex")));
			this.lbCBM9.Text = resources.GetString("lbCBM9.Text");
			this.lbCBM9.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM9.TextAlign")));
			this.lbCBM9.Visible = ((bool)(resources.GetObject("lbCBM9.Visible")));
			// 
			// lbCBM13
			// 
			this.lbCBM13.AccessibleDescription = resources.GetString("lbCBM13.AccessibleDescription");
			this.lbCBM13.AccessibleName = resources.GetString("lbCBM13.AccessibleName");
			this.lbCBM13.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbCBM13.Anchor")));
			this.lbCBM13.AutoSize = ((bool)(resources.GetObject("lbCBM13.AutoSize")));
			this.lbCBM13.BackColor = System.Drawing.SystemColors.Control;
			this.lbCBM13.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbCBM13.Dock")));
			this.lbCBM13.Enabled = ((bool)(resources.GetObject("lbCBM13.Enabled")));
			this.lbCBM13.Font = ((System.Drawing.Font)(resources.GetObject("lbCBM13.Font")));
			this.lbCBM13.Image = ((System.Drawing.Image)(resources.GetObject("lbCBM13.Image")));
			this.lbCBM13.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM13.ImageAlign")));
			this.lbCBM13.ImageIndex = ((int)(resources.GetObject("lbCBM13.ImageIndex")));
			this.lbCBM13.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbCBM13.ImeMode")));
			this.lbCBM13.Location = ((System.Drawing.Point)(resources.GetObject("lbCBM13.Location")));
			this.lbCBM13.Name = "lbCBM13";
			this.lbCBM13.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbCBM13.RightToLeft")));
			this.lbCBM13.Size = ((System.Drawing.Size)(resources.GetObject("lbCBM13.Size")));
			this.lbCBM13.TabIndex = ((int)(resources.GetObject("lbCBM13.TabIndex")));
			this.lbCBM13.Text = resources.GetString("lbCBM13.Text");
			this.lbCBM13.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM13.TextAlign")));
			this.lbCBM13.Visible = ((bool)(resources.GetObject("lbCBM13.Visible")));
			// 
			// lbCBM10
			// 
			this.lbCBM10.AccessibleDescription = resources.GetString("lbCBM10.AccessibleDescription");
			this.lbCBM10.AccessibleName = resources.GetString("lbCBM10.AccessibleName");
			this.lbCBM10.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbCBM10.Anchor")));
			this.lbCBM10.AutoSize = ((bool)(resources.GetObject("lbCBM10.AutoSize")));
			this.lbCBM10.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbCBM10.Dock")));
			this.lbCBM10.Enabled = ((bool)(resources.GetObject("lbCBM10.Enabled")));
			this.lbCBM10.Font = ((System.Drawing.Font)(resources.GetObject("lbCBM10.Font")));
			this.lbCBM10.Image = ((System.Drawing.Image)(resources.GetObject("lbCBM10.Image")));
			this.lbCBM10.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM10.ImageAlign")));
			this.lbCBM10.ImageIndex = ((int)(resources.GetObject("lbCBM10.ImageIndex")));
			this.lbCBM10.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbCBM10.ImeMode")));
			this.lbCBM10.Location = ((System.Drawing.Point)(resources.GetObject("lbCBM10.Location")));
			this.lbCBM10.Name = "lbCBM10";
			this.lbCBM10.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbCBM10.RightToLeft")));
			this.lbCBM10.Size = ((System.Drawing.Size)(resources.GetObject("lbCBM10.Size")));
			this.lbCBM10.TabIndex = ((int)(resources.GetObject("lbCBM10.TabIndex")));
			this.lbCBM10.Text = resources.GetString("lbCBM10.Text");
			this.lbCBM10.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM10.TextAlign")));
			this.lbCBM10.Visible = ((bool)(resources.GetObject("lbCBM10.Visible")));
			// 
			// lbCBM12
			// 
			this.lbCBM12.AccessibleDescription = resources.GetString("lbCBM12.AccessibleDescription");
			this.lbCBM12.AccessibleName = resources.GetString("lbCBM12.AccessibleName");
			this.lbCBM12.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbCBM12.Anchor")));
			this.lbCBM12.AutoSize = ((bool)(resources.GetObject("lbCBM12.AutoSize")));
			this.lbCBM12.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbCBM12.Dock")));
			this.lbCBM12.Enabled = ((bool)(resources.GetObject("lbCBM12.Enabled")));
			this.lbCBM12.Font = ((System.Drawing.Font)(resources.GetObject("lbCBM12.Font")));
			this.lbCBM12.Image = ((System.Drawing.Image)(resources.GetObject("lbCBM12.Image")));
			this.lbCBM12.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM12.ImageAlign")));
			this.lbCBM12.ImageIndex = ((int)(resources.GetObject("lbCBM12.ImageIndex")));
			this.lbCBM12.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbCBM12.ImeMode")));
			this.lbCBM12.Location = ((System.Drawing.Point)(resources.GetObject("lbCBM12.Location")));
			this.lbCBM12.Name = "lbCBM12";
			this.lbCBM12.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbCBM12.RightToLeft")));
			this.lbCBM12.Size = ((System.Drawing.Size)(resources.GetObject("lbCBM12.Size")));
			this.lbCBM12.TabIndex = ((int)(resources.GetObject("lbCBM12.TabIndex")));
			this.lbCBM12.Text = resources.GetString("lbCBM12.Text");
			this.lbCBM12.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbCBM12.TextAlign")));
			this.lbCBM12.Visible = ((bool)(resources.GetObject("lbCBM12.Visible")));
			// 
			// btnHelp
			// 
			this.btnHelp.AccessibleDescription = resources.GetString("btnHelp.AccessibleDescription");
			this.btnHelp.AccessibleName = resources.GetString("btnHelp.AccessibleName");
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnHelp.Anchor")));
			this.btnHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHelp.BackgroundImage")));
			this.btnHelp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnHelp.Dock")));
			this.btnHelp.Enabled = ((bool)(resources.GetObject("btnHelp.Enabled")));
			this.btnHelp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnHelp.FlatStyle")));
			this.btnHelp.Font = ((System.Drawing.Font)(resources.GetObject("btnHelp.Font")));
			this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
			this.btnHelp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnHelp.ImageAlign")));
			this.btnHelp.ImageIndex = ((int)(resources.GetObject("btnHelp.ImageIndex")));
			this.btnHelp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnHelp.ImeMode")));
			this.btnHelp.Location = ((System.Drawing.Point)(resources.GetObject("btnHelp.Location")));
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnHelp.RightToLeft")));
			this.btnHelp.Size = ((System.Drawing.Size)(resources.GetObject("btnHelp.Size")));
			this.btnHelp.TabIndex = ((int)(resources.GetObject("btnHelp.TabIndex")));
			this.btnHelp.Text = resources.GetString("btnHelp.Text");
			this.btnHelp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnHelp.TextAlign")));
			this.btnHelp.Visible = ((bool)(resources.GetObject("btnHelp.Visible")));
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// TtabItemMotiveTableUI
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.pnCopyButtons);
			this.Controls.Add(this.cbShowAll);
			this.Controls.Add(this.pnAllGroups);
			this.Controls.Add(this.lbMotive0);
			this.Controls.Add(this.ttabMotiveGroupUI1);
			this.Controls.Add(this.lbMotive1);
			this.Controls.Add(this.lbMotive2);
			this.Controls.Add(this.lbMotive3);
			this.Controls.Add(this.lbMotive4);
			this.Controls.Add(this.lbMotive5);
			this.Controls.Add(this.lbMotive6);
			this.Controls.Add(this.lbMotive7);
			this.Controls.Add(this.lbMotive9);
			this.Controls.Add(this.lbMotive11);
			this.Controls.Add(this.lbMotive8);
			this.Controls.Add(this.lbMotive10);
			this.Controls.Add(this.lbMotive14);
			this.Controls.Add(this.lbMotive15);
			this.Controls.Add(this.lbMotive13);
			this.Controls.Add(this.lbMotive12);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "TtabItemMotiveTableUI";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.pnAllGroups.ResumeLayout(false);
			this.pnCopyButtons.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void copy_Click(object sender, System.EventArgs e)
		{
			ArrayList alBtnCopy = new ArrayList(aButtons);
			int bn = alBtnCopy.IndexOf(sender);
			if (bn >= 0)
				doCopyMotive(bn);
			else
				for(int i = 0; i < item.nrMotives[0]; i++)
					doCopyMotive(i);
		}

		private void cbShowAll_CheckedChanged(object sender, System.EventArgs e)
		{
			if (cbShowAll.Enabled && cbShowAll.Checked)
			{
				pnCopyButtons.Visible = false;
				pnAllGroups.Visible = true;
			}
			else
			{
				pnAllGroups.Visible = false;
				pnCopyButtons.Visible = true;
			}
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			(new pjse.HelpContainer(pjse.HelpContainer.HelpPage.TtabMotives)).Show();
		}

	}
}
