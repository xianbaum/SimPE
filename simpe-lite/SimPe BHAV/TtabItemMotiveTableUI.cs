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
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel pnAllGroups;
		private SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI ttabMotiveGroupUI2;
		private SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI ttabMotiveGroupUI3;
		private SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI ttabMotiveGroupUI4;
		private SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI ttabMotiveGroupUI5;
		private SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI ttabMotiveGroupUI6;
		private SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI ttabMotiveGroupUI7;
		private System.Windows.Forms.CheckBox cbShowAll;
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
		public void SetData(TtabItem i)
		{
			item = i;

			for (ushort m = 0; m < aMotiveLabels.Length; m++)
			{
				aMotiveLabels[m].Text = item.Opcodes.FindMotives(m);
				aMotiveLabels[m].Left = ttabMotiveGroupUI1.Left - aMotiveLabels[m].Width - 4;
			}

			cbShowAll.Enabled = (item.nrGroups > 1);

			doGroups(cbShowAll.Enabled && cbShowAll.Checked);
		}

		public void SetData() { this.SetData(item); }


		private void doGroups(bool show)
		{
			pnAllGroups.Visible = show;
			if (show)
			{
				for (int m = 0; m < aMotiveGroups.Length; m++) 
					aMotiveGroups[m].SetData(item, m);
			}
			else
			{
				ttabMotiveGroupUI1.SetData(item, (cbShowAll.Enabled) ? -1 : 0);
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
			this.label1 = new System.Windows.Forms.Label();
			this.pnAllGroups = new System.Windows.Forms.Panel();
			this.ttabMotiveGroupUI2 = new SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI();
			this.ttabMotiveGroupUI3 = new SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI();
			this.ttabMotiveGroupUI4 = new SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI();
			this.ttabMotiveGroupUI5 = new SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI();
			this.ttabMotiveGroupUI6 = new SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI();
			this.ttabMotiveGroupUI7 = new SimPe.PackedFiles.UserInterface.TtabMotiveGroupUI();
			this.cbShowAll = new System.Windows.Forms.CheckBox();
			this.pnAllGroups.SuspendLayout();
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
			this.ttabMotiveGroupUI1.MotiveClick += new SimPe.PackedFiles.UserInterface.MotiveClickEventHandler(this.MotiveClick);
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
			// label1
			// 
			this.label1.AccessibleDescription = resources.GetString("label1.AccessibleDescription");
			this.label1.AccessibleName = resources.GetString("label1.AccessibleName");
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label1.Anchor")));
			this.label1.AutoSize = ((bool)(resources.GetObject("label1.AutoSize")));
			this.label1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label1.Dock")));
			this.label1.Enabled = ((bool)(resources.GetObject("label1.Enabled")));
			this.label1.Font = ((System.Drawing.Font)(resources.GetObject("label1.Font")));
			this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
			this.label1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.ImageAlign")));
			this.label1.ImageIndex = ((int)(resources.GetObject("label1.ImageIndex")));
			this.label1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label1.ImeMode")));
			this.label1.Location = ((System.Drawing.Point)(resources.GetObject("label1.Location")));
			this.label1.Name = "label1";
			this.label1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label1.RightToLeft")));
			this.label1.Size = ((System.Drawing.Size)(resources.GetObject("label1.Size")));
			this.label1.TabIndex = ((int)(resources.GetObject("label1.TabIndex")));
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.TextAlign")));
			this.label1.Visible = ((bool)(resources.GetObject("label1.Visible")));
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
			this.ttabMotiveGroupUI2.MotiveClick += new SimPe.PackedFiles.UserInterface.MotiveClickEventHandler(this.MotiveClick);
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
			this.ttabMotiveGroupUI3.MotiveClick += new SimPe.PackedFiles.UserInterface.MotiveClickEventHandler(this.MotiveClick);
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
			this.ttabMotiveGroupUI4.MotiveClick += new SimPe.PackedFiles.UserInterface.MotiveClickEventHandler(this.MotiveClick);
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
			this.ttabMotiveGroupUI5.MotiveClick += new SimPe.PackedFiles.UserInterface.MotiveClickEventHandler(this.MotiveClick);
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
			this.ttabMotiveGroupUI6.MotiveClick += new SimPe.PackedFiles.UserInterface.MotiveClickEventHandler(this.MotiveClick);
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
			this.ttabMotiveGroupUI7.MotiveClick += new SimPe.PackedFiles.UserInterface.MotiveClickEventHandler(this.MotiveClick);
			// 
			// cbShowAll
			// 
			this.cbShowAll.AccessibleDescription = resources.GetString("cbShowAll.AccessibleDescription");
			this.cbShowAll.AccessibleName = resources.GetString("cbShowAll.AccessibleName");
			this.cbShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbShowAll.Anchor")));
			this.cbShowAll.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbShowAll.Appearance")));
			this.cbShowAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbShowAll.BackgroundImage")));
			this.cbShowAll.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbShowAll.CheckAlign")));
			this.cbShowAll.Checked = true;
			this.cbShowAll.CheckState = System.Windows.Forms.CheckState.Checked;
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
			// TtabItemMotiveTableUI
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Controls.Add(this.cbShowAll);
			this.Controls.Add(this.pnAllGroups);
			this.Controls.Add(this.label1);
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
			this.ResumeLayout(false);

		}
		#endregion

		private void MotiveClick(object sender, SimPe.PackedFiles.UserInterface.MotiveClickEventArgs e)
		{
			ArrayList al = new ArrayList(this.aMotiveGroups);
			int mg = al.IndexOf(sender);
			if (mg < 0) return;

			if (e.Motive >= 0)
			{
				// Copy all MG[mg].Motive[e.Motive] to the other MGs
			}
			else
			{
				// Copy all MG[0] values to the other MGs
			}
		}

		private void cbShowAll_CheckedChanged(object sender, System.EventArgs e)
		{
			doGroups(cbShowAll.Checked);
		}

	}
}
