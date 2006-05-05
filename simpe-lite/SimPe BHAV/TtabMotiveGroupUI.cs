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
	/// Summary description for TtabMotiveGroupUI.
	/// </summary>
	public class TtabMotiveGroupUI : System.Windows.Forms.UserControl
	{
		#region Form variables
		private System.Windows.Forms.GroupBox gbMotiveGroup;
		private System.Windows.Forms.Label Min;
		private System.Windows.Forms.Label Delta;
		private System.Windows.Forms.Label Type;
		private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI1;
		private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI2;
		private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI3;
		private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI4;
		private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI5;
		private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI6;
		private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI7;
		private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI8;
		private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI9;
		private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI10;
		private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI11;
		private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI12;
		private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI13;
		private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI14;
		private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI15;
		private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI16;
		private System.Windows.Forms.Button btnClear;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public TtabMotiveGroupUI()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			TtabSingleMotiveUI[] ts = {
										  ttabSingleMotiveUI1  ,ttabSingleMotiveUI2  ,ttabSingleMotiveUI3  ,ttabSingleMotiveUI4
										  ,ttabSingleMotiveUI5  ,ttabSingleMotiveUI6  ,ttabSingleMotiveUI7  ,ttabSingleMotiveUI8
										  ,ttabSingleMotiveUI9  ,ttabSingleMotiveUI10 ,ttabSingleMotiveUI11 ,ttabSingleMotiveUI12
										  ,ttabSingleMotiveUI13 ,ttabSingleMotiveUI14 ,ttabSingleMotiveUI15 ,ttabSingleMotiveUI16
									  };
			aTtabSingleMotiveUI = ts;
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


		#region TtabMotiveGroupUI
		private TtabItem item = null;
		private int mgnr;
		private TtabSingleMotiveUI[] aTtabSingleMotiveUI;
		public void SetData(TtabItem i, int j)
		{
			this.Visible = true;

			item = i;
			MotiveGroup = j;

			if (j == -1) j = 0;
			for (int k = 0; k < aTtabSingleMotiveUI.Length; k++)
			{
				aTtabSingleMotiveUI[k].Visible = true;
				if (aTtabSingleMotiveUI[k].Motive < item.nrMotives[j])
				{
					aTtabSingleMotiveUI[k].SetData(item, j);
				}
				else
				{
					aTtabSingleMotiveUI[k].Visible = false;
				}
			}
		}

		public void SetData(TtabItem i) { this.SetData(i, mgnr); }

		public void SetData() { this.SetData(item, mgnr); }

		public int MotiveGroup
		{
			get { return mgnr; }
			set
			{
				if (value < 0 || value > 6)
					throw new Exception("Motive group must be in range 0 to 6.");
				mgnr = value;

				gbMotiveGroup.Text = pjse.Localization.GetString(((TtabMotives)mgnr).ToString());
				//gbMotiveGroup.Text = ((TtabMotives)mgnr).ToString();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TtabMotiveGroupUI));
			this.gbMotiveGroup = new System.Windows.Forms.GroupBox();
			this.btnClear = new System.Windows.Forms.Button();
			this.ttabSingleMotiveUI1 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
			this.Min = new System.Windows.Forms.Label();
			this.Delta = new System.Windows.Forms.Label();
			this.Type = new System.Windows.Forms.Label();
			this.ttabSingleMotiveUI2 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
			this.ttabSingleMotiveUI3 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
			this.ttabSingleMotiveUI4 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
			this.ttabSingleMotiveUI8 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
			this.ttabSingleMotiveUI7 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
			this.ttabSingleMotiveUI6 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
			this.ttabSingleMotiveUI5 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
			this.ttabSingleMotiveUI16 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
			this.ttabSingleMotiveUI15 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
			this.ttabSingleMotiveUI14 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
			this.ttabSingleMotiveUI13 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
			this.ttabSingleMotiveUI11 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
			this.ttabSingleMotiveUI9 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
			this.ttabSingleMotiveUI10 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
			this.ttabSingleMotiveUI12 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
			this.gbMotiveGroup.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbMotiveGroup
			// 
			this.gbMotiveGroup.AccessibleDescription = resources.GetString("gbMotiveGroup.AccessibleDescription");
			this.gbMotiveGroup.AccessibleName = resources.GetString("gbMotiveGroup.AccessibleName");
			this.gbMotiveGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gbMotiveGroup.Anchor")));
			this.gbMotiveGroup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gbMotiveGroup.BackgroundImage")));
			this.gbMotiveGroup.Controls.Add(this.btnClear);
			this.gbMotiveGroup.Controls.Add(this.ttabSingleMotiveUI1);
			this.gbMotiveGroup.Controls.Add(this.Min);
			this.gbMotiveGroup.Controls.Add(this.Delta);
			this.gbMotiveGroup.Controls.Add(this.Type);
			this.gbMotiveGroup.Controls.Add(this.ttabSingleMotiveUI2);
			this.gbMotiveGroup.Controls.Add(this.ttabSingleMotiveUI3);
			this.gbMotiveGroup.Controls.Add(this.ttabSingleMotiveUI4);
			this.gbMotiveGroup.Controls.Add(this.ttabSingleMotiveUI8);
			this.gbMotiveGroup.Controls.Add(this.ttabSingleMotiveUI7);
			this.gbMotiveGroup.Controls.Add(this.ttabSingleMotiveUI6);
			this.gbMotiveGroup.Controls.Add(this.ttabSingleMotiveUI5);
			this.gbMotiveGroup.Controls.Add(this.ttabSingleMotiveUI16);
			this.gbMotiveGroup.Controls.Add(this.ttabSingleMotiveUI15);
			this.gbMotiveGroup.Controls.Add(this.ttabSingleMotiveUI14);
			this.gbMotiveGroup.Controls.Add(this.ttabSingleMotiveUI13);
			this.gbMotiveGroup.Controls.Add(this.ttabSingleMotiveUI11);
			this.gbMotiveGroup.Controls.Add(this.ttabSingleMotiveUI9);
			this.gbMotiveGroup.Controls.Add(this.ttabSingleMotiveUI10);
			this.gbMotiveGroup.Controls.Add(this.ttabSingleMotiveUI12);
			this.gbMotiveGroup.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("gbMotiveGroup.Dock")));
			this.gbMotiveGroup.Enabled = ((bool)(resources.GetObject("gbMotiveGroup.Enabled")));
			this.gbMotiveGroup.Font = ((System.Drawing.Font)(resources.GetObject("gbMotiveGroup.Font")));
			this.gbMotiveGroup.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gbMotiveGroup.ImeMode")));
			this.gbMotiveGroup.Location = ((System.Drawing.Point)(resources.GetObject("gbMotiveGroup.Location")));
			this.gbMotiveGroup.Name = "gbMotiveGroup";
			this.gbMotiveGroup.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("gbMotiveGroup.RightToLeft")));
			this.gbMotiveGroup.Size = ((System.Drawing.Size)(resources.GetObject("gbMotiveGroup.Size")));
			this.gbMotiveGroup.TabIndex = ((int)(resources.GetObject("gbMotiveGroup.TabIndex")));
			this.gbMotiveGroup.TabStop = false;
			this.gbMotiveGroup.Text = resources.GetString("gbMotiveGroup.Text");
			this.gbMotiveGroup.Visible = ((bool)(resources.GetObject("gbMotiveGroup.Visible")));
			// 
			// btnClear
			// 
			this.btnClear.AccessibleDescription = resources.GetString("btnClear.AccessibleDescription");
			this.btnClear.AccessibleName = resources.GetString("btnClear.AccessibleName");
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnClear.Anchor")));
			this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
			this.btnClear.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnClear.Dock")));
			this.btnClear.Enabled = ((bool)(resources.GetObject("btnClear.Enabled")));
			this.btnClear.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnClear.FlatStyle")));
			this.btnClear.Font = ((System.Drawing.Font)(resources.GetObject("btnClear.Font")));
			this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
			this.btnClear.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClear.ImageAlign")));
			this.btnClear.ImageIndex = ((int)(resources.GetObject("btnClear.ImageIndex")));
			this.btnClear.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnClear.ImeMode")));
			this.btnClear.Location = ((System.Drawing.Point)(resources.GetObject("btnClear.Location")));
			this.btnClear.Name = "btnClear";
			this.btnClear.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnClear.RightToLeft")));
			this.btnClear.Size = ((System.Drawing.Size)(resources.GetObject("btnClear.Size")));
			this.btnClear.TabIndex = ((int)(resources.GetObject("btnClear.TabIndex")));
			this.btnClear.Text = resources.GetString("btnClear.Text");
			this.btnClear.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnClear.TextAlign")));
			this.btnClear.Visible = ((bool)(resources.GetObject("btnClear.Visible")));
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// ttabSingleMotiveUI1
			// 
			this.ttabSingleMotiveUI1.AccessibleDescription = resources.GetString("ttabSingleMotiveUI1.AccessibleDescription");
			this.ttabSingleMotiveUI1.AccessibleName = resources.GetString("ttabSingleMotiveUI1.AccessibleName");
			this.ttabSingleMotiveUI1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabSingleMotiveUI1.Anchor")));
			this.ttabSingleMotiveUI1.AutoScroll = ((bool)(resources.GetObject("ttabSingleMotiveUI1.AutoScroll")));
			this.ttabSingleMotiveUI1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI1.AutoScrollMargin")));
			this.ttabSingleMotiveUI1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI1.AutoScrollMinSize")));
			this.ttabSingleMotiveUI1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabSingleMotiveUI1.BackgroundImage")));
			this.ttabSingleMotiveUI1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabSingleMotiveUI1.Dock")));
			this.ttabSingleMotiveUI1.Enabled = ((bool)(resources.GetObject("ttabSingleMotiveUI1.Enabled")));
			this.ttabSingleMotiveUI1.Font = ((System.Drawing.Font)(resources.GetObject("ttabSingleMotiveUI1.Font")));
			this.ttabSingleMotiveUI1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabSingleMotiveUI1.ImeMode")));
			this.ttabSingleMotiveUI1.Location = ((System.Drawing.Point)(resources.GetObject("ttabSingleMotiveUI1.Location")));
			this.ttabSingleMotiveUI1.Motive = 0;
			this.ttabSingleMotiveUI1.Name = "ttabSingleMotiveUI1";
			this.ttabSingleMotiveUI1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabSingleMotiveUI1.RightToLeft")));
			this.ttabSingleMotiveUI1.Size = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI1.Size")));
			this.ttabSingleMotiveUI1.TabIndex = ((int)(resources.GetObject("ttabSingleMotiveUI1.TabIndex")));
			this.ttabSingleMotiveUI1.Visible = ((bool)(resources.GetObject("ttabSingleMotiveUI1.Visible")));
			// 
			// Min
			// 
			this.Min.AccessibleDescription = resources.GetString("Min.AccessibleDescription");
			this.Min.AccessibleName = resources.GetString("Min.AccessibleName");
			this.Min.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("Min.Anchor")));
			this.Min.AutoSize = ((bool)(resources.GetObject("Min.AutoSize")));
			this.Min.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("Min.Dock")));
			this.Min.Enabled = ((bool)(resources.GetObject("Min.Enabled")));
			this.Min.Font = ((System.Drawing.Font)(resources.GetObject("Min.Font")));
			this.Min.Image = ((System.Drawing.Image)(resources.GetObject("Min.Image")));
			this.Min.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("Min.ImageAlign")));
			this.Min.ImageIndex = ((int)(resources.GetObject("Min.ImageIndex")));
			this.Min.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("Min.ImeMode")));
			this.Min.Location = ((System.Drawing.Point)(resources.GetObject("Min.Location")));
			this.Min.Name = "Min";
			this.Min.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("Min.RightToLeft")));
			this.Min.Size = ((System.Drawing.Size)(resources.GetObject("Min.Size")));
			this.Min.TabIndex = ((int)(resources.GetObject("Min.TabIndex")));
			this.Min.Text = resources.GetString("Min.Text");
			this.Min.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("Min.TextAlign")));
			this.Min.Visible = ((bool)(resources.GetObject("Min.Visible")));
			// 
			// Delta
			// 
			this.Delta.AccessibleDescription = resources.GetString("Delta.AccessibleDescription");
			this.Delta.AccessibleName = resources.GetString("Delta.AccessibleName");
			this.Delta.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("Delta.Anchor")));
			this.Delta.AutoSize = ((bool)(resources.GetObject("Delta.AutoSize")));
			this.Delta.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("Delta.Dock")));
			this.Delta.Enabled = ((bool)(resources.GetObject("Delta.Enabled")));
			this.Delta.Font = ((System.Drawing.Font)(resources.GetObject("Delta.Font")));
			this.Delta.Image = ((System.Drawing.Image)(resources.GetObject("Delta.Image")));
			this.Delta.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("Delta.ImageAlign")));
			this.Delta.ImageIndex = ((int)(resources.GetObject("Delta.ImageIndex")));
			this.Delta.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("Delta.ImeMode")));
			this.Delta.Location = ((System.Drawing.Point)(resources.GetObject("Delta.Location")));
			this.Delta.Name = "Delta";
			this.Delta.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("Delta.RightToLeft")));
			this.Delta.Size = ((System.Drawing.Size)(resources.GetObject("Delta.Size")));
			this.Delta.TabIndex = ((int)(resources.GetObject("Delta.TabIndex")));
			this.Delta.Text = resources.GetString("Delta.Text");
			this.Delta.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("Delta.TextAlign")));
			this.Delta.Visible = ((bool)(resources.GetObject("Delta.Visible")));
			// 
			// Type
			// 
			this.Type.AccessibleDescription = resources.GetString("Type.AccessibleDescription");
			this.Type.AccessibleName = resources.GetString("Type.AccessibleName");
			this.Type.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("Type.Anchor")));
			this.Type.AutoSize = ((bool)(resources.GetObject("Type.AutoSize")));
			this.Type.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("Type.Dock")));
			this.Type.Enabled = ((bool)(resources.GetObject("Type.Enabled")));
			this.Type.Font = ((System.Drawing.Font)(resources.GetObject("Type.Font")));
			this.Type.Image = ((System.Drawing.Image)(resources.GetObject("Type.Image")));
			this.Type.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("Type.ImageAlign")));
			this.Type.ImageIndex = ((int)(resources.GetObject("Type.ImageIndex")));
			this.Type.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("Type.ImeMode")));
			this.Type.Location = ((System.Drawing.Point)(resources.GetObject("Type.Location")));
			this.Type.Name = "Type";
			this.Type.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("Type.RightToLeft")));
			this.Type.Size = ((System.Drawing.Size)(resources.GetObject("Type.Size")));
			this.Type.TabIndex = ((int)(resources.GetObject("Type.TabIndex")));
			this.Type.Text = resources.GetString("Type.Text");
			this.Type.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("Type.TextAlign")));
			this.Type.Visible = ((bool)(resources.GetObject("Type.Visible")));
			// 
			// ttabSingleMotiveUI2
			// 
			this.ttabSingleMotiveUI2.AccessibleDescription = resources.GetString("ttabSingleMotiveUI2.AccessibleDescription");
			this.ttabSingleMotiveUI2.AccessibleName = resources.GetString("ttabSingleMotiveUI2.AccessibleName");
			this.ttabSingleMotiveUI2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabSingleMotiveUI2.Anchor")));
			this.ttabSingleMotiveUI2.AutoScroll = ((bool)(resources.GetObject("ttabSingleMotiveUI2.AutoScroll")));
			this.ttabSingleMotiveUI2.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI2.AutoScrollMargin")));
			this.ttabSingleMotiveUI2.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI2.AutoScrollMinSize")));
			this.ttabSingleMotiveUI2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabSingleMotiveUI2.BackgroundImage")));
			this.ttabSingleMotiveUI2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabSingleMotiveUI2.Dock")));
			this.ttabSingleMotiveUI2.Enabled = ((bool)(resources.GetObject("ttabSingleMotiveUI2.Enabled")));
			this.ttabSingleMotiveUI2.Font = ((System.Drawing.Font)(resources.GetObject("ttabSingleMotiveUI2.Font")));
			this.ttabSingleMotiveUI2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabSingleMotiveUI2.ImeMode")));
			this.ttabSingleMotiveUI2.Location = ((System.Drawing.Point)(resources.GetObject("ttabSingleMotiveUI2.Location")));
			this.ttabSingleMotiveUI2.Motive = 1;
			this.ttabSingleMotiveUI2.Name = "ttabSingleMotiveUI2";
			this.ttabSingleMotiveUI2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabSingleMotiveUI2.RightToLeft")));
			this.ttabSingleMotiveUI2.Size = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI2.Size")));
			this.ttabSingleMotiveUI2.TabIndex = ((int)(resources.GetObject("ttabSingleMotiveUI2.TabIndex")));
			this.ttabSingleMotiveUI2.Visible = ((bool)(resources.GetObject("ttabSingleMotiveUI2.Visible")));
			// 
			// ttabSingleMotiveUI3
			// 
			this.ttabSingleMotiveUI3.AccessibleDescription = resources.GetString("ttabSingleMotiveUI3.AccessibleDescription");
			this.ttabSingleMotiveUI3.AccessibleName = resources.GetString("ttabSingleMotiveUI3.AccessibleName");
			this.ttabSingleMotiveUI3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabSingleMotiveUI3.Anchor")));
			this.ttabSingleMotiveUI3.AutoScroll = ((bool)(resources.GetObject("ttabSingleMotiveUI3.AutoScroll")));
			this.ttabSingleMotiveUI3.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI3.AutoScrollMargin")));
			this.ttabSingleMotiveUI3.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI3.AutoScrollMinSize")));
			this.ttabSingleMotiveUI3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabSingleMotiveUI3.BackgroundImage")));
			this.ttabSingleMotiveUI3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabSingleMotiveUI3.Dock")));
			this.ttabSingleMotiveUI3.Enabled = ((bool)(resources.GetObject("ttabSingleMotiveUI3.Enabled")));
			this.ttabSingleMotiveUI3.Font = ((System.Drawing.Font)(resources.GetObject("ttabSingleMotiveUI3.Font")));
			this.ttabSingleMotiveUI3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabSingleMotiveUI3.ImeMode")));
			this.ttabSingleMotiveUI3.Location = ((System.Drawing.Point)(resources.GetObject("ttabSingleMotiveUI3.Location")));
			this.ttabSingleMotiveUI3.Motive = 2;
			this.ttabSingleMotiveUI3.Name = "ttabSingleMotiveUI3";
			this.ttabSingleMotiveUI3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabSingleMotiveUI3.RightToLeft")));
			this.ttabSingleMotiveUI3.Size = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI3.Size")));
			this.ttabSingleMotiveUI3.TabIndex = ((int)(resources.GetObject("ttabSingleMotiveUI3.TabIndex")));
			this.ttabSingleMotiveUI3.Visible = ((bool)(resources.GetObject("ttabSingleMotiveUI3.Visible")));
			// 
			// ttabSingleMotiveUI4
			// 
			this.ttabSingleMotiveUI4.AccessibleDescription = resources.GetString("ttabSingleMotiveUI4.AccessibleDescription");
			this.ttabSingleMotiveUI4.AccessibleName = resources.GetString("ttabSingleMotiveUI4.AccessibleName");
			this.ttabSingleMotiveUI4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabSingleMotiveUI4.Anchor")));
			this.ttabSingleMotiveUI4.AutoScroll = ((bool)(resources.GetObject("ttabSingleMotiveUI4.AutoScroll")));
			this.ttabSingleMotiveUI4.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI4.AutoScrollMargin")));
			this.ttabSingleMotiveUI4.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI4.AutoScrollMinSize")));
			this.ttabSingleMotiveUI4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabSingleMotiveUI4.BackgroundImage")));
			this.ttabSingleMotiveUI4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabSingleMotiveUI4.Dock")));
			this.ttabSingleMotiveUI4.Enabled = ((bool)(resources.GetObject("ttabSingleMotiveUI4.Enabled")));
			this.ttabSingleMotiveUI4.Font = ((System.Drawing.Font)(resources.GetObject("ttabSingleMotiveUI4.Font")));
			this.ttabSingleMotiveUI4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabSingleMotiveUI4.ImeMode")));
			this.ttabSingleMotiveUI4.Location = ((System.Drawing.Point)(resources.GetObject("ttabSingleMotiveUI4.Location")));
			this.ttabSingleMotiveUI4.Motive = 3;
			this.ttabSingleMotiveUI4.Name = "ttabSingleMotiveUI4";
			this.ttabSingleMotiveUI4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabSingleMotiveUI4.RightToLeft")));
			this.ttabSingleMotiveUI4.Size = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI4.Size")));
			this.ttabSingleMotiveUI4.TabIndex = ((int)(resources.GetObject("ttabSingleMotiveUI4.TabIndex")));
			this.ttabSingleMotiveUI4.Visible = ((bool)(resources.GetObject("ttabSingleMotiveUI4.Visible")));
			// 
			// ttabSingleMotiveUI8
			// 
			this.ttabSingleMotiveUI8.AccessibleDescription = resources.GetString("ttabSingleMotiveUI8.AccessibleDescription");
			this.ttabSingleMotiveUI8.AccessibleName = resources.GetString("ttabSingleMotiveUI8.AccessibleName");
			this.ttabSingleMotiveUI8.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabSingleMotiveUI8.Anchor")));
			this.ttabSingleMotiveUI8.AutoScroll = ((bool)(resources.GetObject("ttabSingleMotiveUI8.AutoScroll")));
			this.ttabSingleMotiveUI8.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI8.AutoScrollMargin")));
			this.ttabSingleMotiveUI8.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI8.AutoScrollMinSize")));
			this.ttabSingleMotiveUI8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabSingleMotiveUI8.BackgroundImage")));
			this.ttabSingleMotiveUI8.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabSingleMotiveUI8.Dock")));
			this.ttabSingleMotiveUI8.Enabled = ((bool)(resources.GetObject("ttabSingleMotiveUI8.Enabled")));
			this.ttabSingleMotiveUI8.Font = ((System.Drawing.Font)(resources.GetObject("ttabSingleMotiveUI8.Font")));
			this.ttabSingleMotiveUI8.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabSingleMotiveUI8.ImeMode")));
			this.ttabSingleMotiveUI8.Location = ((System.Drawing.Point)(resources.GetObject("ttabSingleMotiveUI8.Location")));
			this.ttabSingleMotiveUI8.Motive = 7;
			this.ttabSingleMotiveUI8.Name = "ttabSingleMotiveUI8";
			this.ttabSingleMotiveUI8.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabSingleMotiveUI8.RightToLeft")));
			this.ttabSingleMotiveUI8.Size = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI8.Size")));
			this.ttabSingleMotiveUI8.TabIndex = ((int)(resources.GetObject("ttabSingleMotiveUI8.TabIndex")));
			this.ttabSingleMotiveUI8.Visible = ((bool)(resources.GetObject("ttabSingleMotiveUI8.Visible")));
			// 
			// ttabSingleMotiveUI7
			// 
			this.ttabSingleMotiveUI7.AccessibleDescription = resources.GetString("ttabSingleMotiveUI7.AccessibleDescription");
			this.ttabSingleMotiveUI7.AccessibleName = resources.GetString("ttabSingleMotiveUI7.AccessibleName");
			this.ttabSingleMotiveUI7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabSingleMotiveUI7.Anchor")));
			this.ttabSingleMotiveUI7.AutoScroll = ((bool)(resources.GetObject("ttabSingleMotiveUI7.AutoScroll")));
			this.ttabSingleMotiveUI7.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI7.AutoScrollMargin")));
			this.ttabSingleMotiveUI7.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI7.AutoScrollMinSize")));
			this.ttabSingleMotiveUI7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabSingleMotiveUI7.BackgroundImage")));
			this.ttabSingleMotiveUI7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabSingleMotiveUI7.Dock")));
			this.ttabSingleMotiveUI7.Enabled = ((bool)(resources.GetObject("ttabSingleMotiveUI7.Enabled")));
			this.ttabSingleMotiveUI7.Font = ((System.Drawing.Font)(resources.GetObject("ttabSingleMotiveUI7.Font")));
			this.ttabSingleMotiveUI7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabSingleMotiveUI7.ImeMode")));
			this.ttabSingleMotiveUI7.Location = ((System.Drawing.Point)(resources.GetObject("ttabSingleMotiveUI7.Location")));
			this.ttabSingleMotiveUI7.Motive = 6;
			this.ttabSingleMotiveUI7.Name = "ttabSingleMotiveUI7";
			this.ttabSingleMotiveUI7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabSingleMotiveUI7.RightToLeft")));
			this.ttabSingleMotiveUI7.Size = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI7.Size")));
			this.ttabSingleMotiveUI7.TabIndex = ((int)(resources.GetObject("ttabSingleMotiveUI7.TabIndex")));
			this.ttabSingleMotiveUI7.Visible = ((bool)(resources.GetObject("ttabSingleMotiveUI7.Visible")));
			// 
			// ttabSingleMotiveUI6
			// 
			this.ttabSingleMotiveUI6.AccessibleDescription = resources.GetString("ttabSingleMotiveUI6.AccessibleDescription");
			this.ttabSingleMotiveUI6.AccessibleName = resources.GetString("ttabSingleMotiveUI6.AccessibleName");
			this.ttabSingleMotiveUI6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabSingleMotiveUI6.Anchor")));
			this.ttabSingleMotiveUI6.AutoScroll = ((bool)(resources.GetObject("ttabSingleMotiveUI6.AutoScroll")));
			this.ttabSingleMotiveUI6.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI6.AutoScrollMargin")));
			this.ttabSingleMotiveUI6.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI6.AutoScrollMinSize")));
			this.ttabSingleMotiveUI6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabSingleMotiveUI6.BackgroundImage")));
			this.ttabSingleMotiveUI6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabSingleMotiveUI6.Dock")));
			this.ttabSingleMotiveUI6.Enabled = ((bool)(resources.GetObject("ttabSingleMotiveUI6.Enabled")));
			this.ttabSingleMotiveUI6.Font = ((System.Drawing.Font)(resources.GetObject("ttabSingleMotiveUI6.Font")));
			this.ttabSingleMotiveUI6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabSingleMotiveUI6.ImeMode")));
			this.ttabSingleMotiveUI6.Location = ((System.Drawing.Point)(resources.GetObject("ttabSingleMotiveUI6.Location")));
			this.ttabSingleMotiveUI6.Motive = 5;
			this.ttabSingleMotiveUI6.Name = "ttabSingleMotiveUI6";
			this.ttabSingleMotiveUI6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabSingleMotiveUI6.RightToLeft")));
			this.ttabSingleMotiveUI6.Size = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI6.Size")));
			this.ttabSingleMotiveUI6.TabIndex = ((int)(resources.GetObject("ttabSingleMotiveUI6.TabIndex")));
			this.ttabSingleMotiveUI6.Visible = ((bool)(resources.GetObject("ttabSingleMotiveUI6.Visible")));
			// 
			// ttabSingleMotiveUI5
			// 
			this.ttabSingleMotiveUI5.AccessibleDescription = resources.GetString("ttabSingleMotiveUI5.AccessibleDescription");
			this.ttabSingleMotiveUI5.AccessibleName = resources.GetString("ttabSingleMotiveUI5.AccessibleName");
			this.ttabSingleMotiveUI5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabSingleMotiveUI5.Anchor")));
			this.ttabSingleMotiveUI5.AutoScroll = ((bool)(resources.GetObject("ttabSingleMotiveUI5.AutoScroll")));
			this.ttabSingleMotiveUI5.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI5.AutoScrollMargin")));
			this.ttabSingleMotiveUI5.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI5.AutoScrollMinSize")));
			this.ttabSingleMotiveUI5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabSingleMotiveUI5.BackgroundImage")));
			this.ttabSingleMotiveUI5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabSingleMotiveUI5.Dock")));
			this.ttabSingleMotiveUI5.Enabled = ((bool)(resources.GetObject("ttabSingleMotiveUI5.Enabled")));
			this.ttabSingleMotiveUI5.Font = ((System.Drawing.Font)(resources.GetObject("ttabSingleMotiveUI5.Font")));
			this.ttabSingleMotiveUI5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabSingleMotiveUI5.ImeMode")));
			this.ttabSingleMotiveUI5.Location = ((System.Drawing.Point)(resources.GetObject("ttabSingleMotiveUI5.Location")));
			this.ttabSingleMotiveUI5.Motive = 4;
			this.ttabSingleMotiveUI5.Name = "ttabSingleMotiveUI5";
			this.ttabSingleMotiveUI5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabSingleMotiveUI5.RightToLeft")));
			this.ttabSingleMotiveUI5.Size = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI5.Size")));
			this.ttabSingleMotiveUI5.TabIndex = ((int)(resources.GetObject("ttabSingleMotiveUI5.TabIndex")));
			this.ttabSingleMotiveUI5.Visible = ((bool)(resources.GetObject("ttabSingleMotiveUI5.Visible")));
			// 
			// ttabSingleMotiveUI16
			// 
			this.ttabSingleMotiveUI16.AccessibleDescription = resources.GetString("ttabSingleMotiveUI16.AccessibleDescription");
			this.ttabSingleMotiveUI16.AccessibleName = resources.GetString("ttabSingleMotiveUI16.AccessibleName");
			this.ttabSingleMotiveUI16.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabSingleMotiveUI16.Anchor")));
			this.ttabSingleMotiveUI16.AutoScroll = ((bool)(resources.GetObject("ttabSingleMotiveUI16.AutoScroll")));
			this.ttabSingleMotiveUI16.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI16.AutoScrollMargin")));
			this.ttabSingleMotiveUI16.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI16.AutoScrollMinSize")));
			this.ttabSingleMotiveUI16.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabSingleMotiveUI16.BackgroundImage")));
			this.ttabSingleMotiveUI16.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabSingleMotiveUI16.Dock")));
			this.ttabSingleMotiveUI16.Enabled = ((bool)(resources.GetObject("ttabSingleMotiveUI16.Enabled")));
			this.ttabSingleMotiveUI16.Font = ((System.Drawing.Font)(resources.GetObject("ttabSingleMotiveUI16.Font")));
			this.ttabSingleMotiveUI16.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabSingleMotiveUI16.ImeMode")));
			this.ttabSingleMotiveUI16.Location = ((System.Drawing.Point)(resources.GetObject("ttabSingleMotiveUI16.Location")));
			this.ttabSingleMotiveUI16.Motive = 15;
			this.ttabSingleMotiveUI16.Name = "ttabSingleMotiveUI16";
			this.ttabSingleMotiveUI16.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabSingleMotiveUI16.RightToLeft")));
			this.ttabSingleMotiveUI16.Size = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI16.Size")));
			this.ttabSingleMotiveUI16.TabIndex = ((int)(resources.GetObject("ttabSingleMotiveUI16.TabIndex")));
			this.ttabSingleMotiveUI16.Visible = ((bool)(resources.GetObject("ttabSingleMotiveUI16.Visible")));
			// 
			// ttabSingleMotiveUI15
			// 
			this.ttabSingleMotiveUI15.AccessibleDescription = resources.GetString("ttabSingleMotiveUI15.AccessibleDescription");
			this.ttabSingleMotiveUI15.AccessibleName = resources.GetString("ttabSingleMotiveUI15.AccessibleName");
			this.ttabSingleMotiveUI15.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabSingleMotiveUI15.Anchor")));
			this.ttabSingleMotiveUI15.AutoScroll = ((bool)(resources.GetObject("ttabSingleMotiveUI15.AutoScroll")));
			this.ttabSingleMotiveUI15.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI15.AutoScrollMargin")));
			this.ttabSingleMotiveUI15.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI15.AutoScrollMinSize")));
			this.ttabSingleMotiveUI15.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabSingleMotiveUI15.BackgroundImage")));
			this.ttabSingleMotiveUI15.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabSingleMotiveUI15.Dock")));
			this.ttabSingleMotiveUI15.Enabled = ((bool)(resources.GetObject("ttabSingleMotiveUI15.Enabled")));
			this.ttabSingleMotiveUI15.Font = ((System.Drawing.Font)(resources.GetObject("ttabSingleMotiveUI15.Font")));
			this.ttabSingleMotiveUI15.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabSingleMotiveUI15.ImeMode")));
			this.ttabSingleMotiveUI15.Location = ((System.Drawing.Point)(resources.GetObject("ttabSingleMotiveUI15.Location")));
			this.ttabSingleMotiveUI15.Motive = 14;
			this.ttabSingleMotiveUI15.Name = "ttabSingleMotiveUI15";
			this.ttabSingleMotiveUI15.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabSingleMotiveUI15.RightToLeft")));
			this.ttabSingleMotiveUI15.Size = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI15.Size")));
			this.ttabSingleMotiveUI15.TabIndex = ((int)(resources.GetObject("ttabSingleMotiveUI15.TabIndex")));
			this.ttabSingleMotiveUI15.Visible = ((bool)(resources.GetObject("ttabSingleMotiveUI15.Visible")));
			// 
			// ttabSingleMotiveUI14
			// 
			this.ttabSingleMotiveUI14.AccessibleDescription = resources.GetString("ttabSingleMotiveUI14.AccessibleDescription");
			this.ttabSingleMotiveUI14.AccessibleName = resources.GetString("ttabSingleMotiveUI14.AccessibleName");
			this.ttabSingleMotiveUI14.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabSingleMotiveUI14.Anchor")));
			this.ttabSingleMotiveUI14.AutoScroll = ((bool)(resources.GetObject("ttabSingleMotiveUI14.AutoScroll")));
			this.ttabSingleMotiveUI14.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI14.AutoScrollMargin")));
			this.ttabSingleMotiveUI14.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI14.AutoScrollMinSize")));
			this.ttabSingleMotiveUI14.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabSingleMotiveUI14.BackgroundImage")));
			this.ttabSingleMotiveUI14.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabSingleMotiveUI14.Dock")));
			this.ttabSingleMotiveUI14.Enabled = ((bool)(resources.GetObject("ttabSingleMotiveUI14.Enabled")));
			this.ttabSingleMotiveUI14.Font = ((System.Drawing.Font)(resources.GetObject("ttabSingleMotiveUI14.Font")));
			this.ttabSingleMotiveUI14.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabSingleMotiveUI14.ImeMode")));
			this.ttabSingleMotiveUI14.Location = ((System.Drawing.Point)(resources.GetObject("ttabSingleMotiveUI14.Location")));
			this.ttabSingleMotiveUI14.Motive = 13;
			this.ttabSingleMotiveUI14.Name = "ttabSingleMotiveUI14";
			this.ttabSingleMotiveUI14.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabSingleMotiveUI14.RightToLeft")));
			this.ttabSingleMotiveUI14.Size = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI14.Size")));
			this.ttabSingleMotiveUI14.TabIndex = ((int)(resources.GetObject("ttabSingleMotiveUI14.TabIndex")));
			this.ttabSingleMotiveUI14.Visible = ((bool)(resources.GetObject("ttabSingleMotiveUI14.Visible")));
			// 
			// ttabSingleMotiveUI13
			// 
			this.ttabSingleMotiveUI13.AccessibleDescription = resources.GetString("ttabSingleMotiveUI13.AccessibleDescription");
			this.ttabSingleMotiveUI13.AccessibleName = resources.GetString("ttabSingleMotiveUI13.AccessibleName");
			this.ttabSingleMotiveUI13.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabSingleMotiveUI13.Anchor")));
			this.ttabSingleMotiveUI13.AutoScroll = ((bool)(resources.GetObject("ttabSingleMotiveUI13.AutoScroll")));
			this.ttabSingleMotiveUI13.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI13.AutoScrollMargin")));
			this.ttabSingleMotiveUI13.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI13.AutoScrollMinSize")));
			this.ttabSingleMotiveUI13.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabSingleMotiveUI13.BackgroundImage")));
			this.ttabSingleMotiveUI13.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabSingleMotiveUI13.Dock")));
			this.ttabSingleMotiveUI13.Enabled = ((bool)(resources.GetObject("ttabSingleMotiveUI13.Enabled")));
			this.ttabSingleMotiveUI13.Font = ((System.Drawing.Font)(resources.GetObject("ttabSingleMotiveUI13.Font")));
			this.ttabSingleMotiveUI13.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabSingleMotiveUI13.ImeMode")));
			this.ttabSingleMotiveUI13.Location = ((System.Drawing.Point)(resources.GetObject("ttabSingleMotiveUI13.Location")));
			this.ttabSingleMotiveUI13.Motive = 12;
			this.ttabSingleMotiveUI13.Name = "ttabSingleMotiveUI13";
			this.ttabSingleMotiveUI13.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabSingleMotiveUI13.RightToLeft")));
			this.ttabSingleMotiveUI13.Size = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI13.Size")));
			this.ttabSingleMotiveUI13.TabIndex = ((int)(resources.GetObject("ttabSingleMotiveUI13.TabIndex")));
			this.ttabSingleMotiveUI13.Visible = ((bool)(resources.GetObject("ttabSingleMotiveUI13.Visible")));
			// 
			// ttabSingleMotiveUI11
			// 
			this.ttabSingleMotiveUI11.AccessibleDescription = resources.GetString("ttabSingleMotiveUI11.AccessibleDescription");
			this.ttabSingleMotiveUI11.AccessibleName = resources.GetString("ttabSingleMotiveUI11.AccessibleName");
			this.ttabSingleMotiveUI11.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabSingleMotiveUI11.Anchor")));
			this.ttabSingleMotiveUI11.AutoScroll = ((bool)(resources.GetObject("ttabSingleMotiveUI11.AutoScroll")));
			this.ttabSingleMotiveUI11.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI11.AutoScrollMargin")));
			this.ttabSingleMotiveUI11.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI11.AutoScrollMinSize")));
			this.ttabSingleMotiveUI11.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabSingleMotiveUI11.BackgroundImage")));
			this.ttabSingleMotiveUI11.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabSingleMotiveUI11.Dock")));
			this.ttabSingleMotiveUI11.Enabled = ((bool)(resources.GetObject("ttabSingleMotiveUI11.Enabled")));
			this.ttabSingleMotiveUI11.Font = ((System.Drawing.Font)(resources.GetObject("ttabSingleMotiveUI11.Font")));
			this.ttabSingleMotiveUI11.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabSingleMotiveUI11.ImeMode")));
			this.ttabSingleMotiveUI11.Location = ((System.Drawing.Point)(resources.GetObject("ttabSingleMotiveUI11.Location")));
			this.ttabSingleMotiveUI11.Motive = 10;
			this.ttabSingleMotiveUI11.Name = "ttabSingleMotiveUI11";
			this.ttabSingleMotiveUI11.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabSingleMotiveUI11.RightToLeft")));
			this.ttabSingleMotiveUI11.Size = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI11.Size")));
			this.ttabSingleMotiveUI11.TabIndex = ((int)(resources.GetObject("ttabSingleMotiveUI11.TabIndex")));
			this.ttabSingleMotiveUI11.Visible = ((bool)(resources.GetObject("ttabSingleMotiveUI11.Visible")));
			// 
			// ttabSingleMotiveUI9
			// 
			this.ttabSingleMotiveUI9.AccessibleDescription = resources.GetString("ttabSingleMotiveUI9.AccessibleDescription");
			this.ttabSingleMotiveUI9.AccessibleName = resources.GetString("ttabSingleMotiveUI9.AccessibleName");
			this.ttabSingleMotiveUI9.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabSingleMotiveUI9.Anchor")));
			this.ttabSingleMotiveUI9.AutoScroll = ((bool)(resources.GetObject("ttabSingleMotiveUI9.AutoScroll")));
			this.ttabSingleMotiveUI9.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI9.AutoScrollMargin")));
			this.ttabSingleMotiveUI9.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI9.AutoScrollMinSize")));
			this.ttabSingleMotiveUI9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabSingleMotiveUI9.BackgroundImage")));
			this.ttabSingleMotiveUI9.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabSingleMotiveUI9.Dock")));
			this.ttabSingleMotiveUI9.Enabled = ((bool)(resources.GetObject("ttabSingleMotiveUI9.Enabled")));
			this.ttabSingleMotiveUI9.Font = ((System.Drawing.Font)(resources.GetObject("ttabSingleMotiveUI9.Font")));
			this.ttabSingleMotiveUI9.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabSingleMotiveUI9.ImeMode")));
			this.ttabSingleMotiveUI9.Location = ((System.Drawing.Point)(resources.GetObject("ttabSingleMotiveUI9.Location")));
			this.ttabSingleMotiveUI9.Motive = 8;
			this.ttabSingleMotiveUI9.Name = "ttabSingleMotiveUI9";
			this.ttabSingleMotiveUI9.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabSingleMotiveUI9.RightToLeft")));
			this.ttabSingleMotiveUI9.Size = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI9.Size")));
			this.ttabSingleMotiveUI9.TabIndex = ((int)(resources.GetObject("ttabSingleMotiveUI9.TabIndex")));
			this.ttabSingleMotiveUI9.Visible = ((bool)(resources.GetObject("ttabSingleMotiveUI9.Visible")));
			// 
			// ttabSingleMotiveUI10
			// 
			this.ttabSingleMotiveUI10.AccessibleDescription = resources.GetString("ttabSingleMotiveUI10.AccessibleDescription");
			this.ttabSingleMotiveUI10.AccessibleName = resources.GetString("ttabSingleMotiveUI10.AccessibleName");
			this.ttabSingleMotiveUI10.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabSingleMotiveUI10.Anchor")));
			this.ttabSingleMotiveUI10.AutoScroll = ((bool)(resources.GetObject("ttabSingleMotiveUI10.AutoScroll")));
			this.ttabSingleMotiveUI10.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI10.AutoScrollMargin")));
			this.ttabSingleMotiveUI10.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI10.AutoScrollMinSize")));
			this.ttabSingleMotiveUI10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabSingleMotiveUI10.BackgroundImage")));
			this.ttabSingleMotiveUI10.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabSingleMotiveUI10.Dock")));
			this.ttabSingleMotiveUI10.Enabled = ((bool)(resources.GetObject("ttabSingleMotiveUI10.Enabled")));
			this.ttabSingleMotiveUI10.Font = ((System.Drawing.Font)(resources.GetObject("ttabSingleMotiveUI10.Font")));
			this.ttabSingleMotiveUI10.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabSingleMotiveUI10.ImeMode")));
			this.ttabSingleMotiveUI10.Location = ((System.Drawing.Point)(resources.GetObject("ttabSingleMotiveUI10.Location")));
			this.ttabSingleMotiveUI10.Motive = 9;
			this.ttabSingleMotiveUI10.Name = "ttabSingleMotiveUI10";
			this.ttabSingleMotiveUI10.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabSingleMotiveUI10.RightToLeft")));
			this.ttabSingleMotiveUI10.Size = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI10.Size")));
			this.ttabSingleMotiveUI10.TabIndex = ((int)(resources.GetObject("ttabSingleMotiveUI10.TabIndex")));
			this.ttabSingleMotiveUI10.Visible = ((bool)(resources.GetObject("ttabSingleMotiveUI10.Visible")));
			// 
			// ttabSingleMotiveUI12
			// 
			this.ttabSingleMotiveUI12.AccessibleDescription = resources.GetString("ttabSingleMotiveUI12.AccessibleDescription");
			this.ttabSingleMotiveUI12.AccessibleName = resources.GetString("ttabSingleMotiveUI12.AccessibleName");
			this.ttabSingleMotiveUI12.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ttabSingleMotiveUI12.Anchor")));
			this.ttabSingleMotiveUI12.AutoScroll = ((bool)(resources.GetObject("ttabSingleMotiveUI12.AutoScroll")));
			this.ttabSingleMotiveUI12.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI12.AutoScrollMargin")));
			this.ttabSingleMotiveUI12.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI12.AutoScrollMinSize")));
			this.ttabSingleMotiveUI12.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ttabSingleMotiveUI12.BackgroundImage")));
			this.ttabSingleMotiveUI12.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ttabSingleMotiveUI12.Dock")));
			this.ttabSingleMotiveUI12.Enabled = ((bool)(resources.GetObject("ttabSingleMotiveUI12.Enabled")));
			this.ttabSingleMotiveUI12.Font = ((System.Drawing.Font)(resources.GetObject("ttabSingleMotiveUI12.Font")));
			this.ttabSingleMotiveUI12.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ttabSingleMotiveUI12.ImeMode")));
			this.ttabSingleMotiveUI12.Location = ((System.Drawing.Point)(resources.GetObject("ttabSingleMotiveUI12.Location")));
			this.ttabSingleMotiveUI12.Motive = 11;
			this.ttabSingleMotiveUI12.Name = "ttabSingleMotiveUI12";
			this.ttabSingleMotiveUI12.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ttabSingleMotiveUI12.RightToLeft")));
			this.ttabSingleMotiveUI12.Size = ((System.Drawing.Size)(resources.GetObject("ttabSingleMotiveUI12.Size")));
			this.ttabSingleMotiveUI12.TabIndex = ((int)(resources.GetObject("ttabSingleMotiveUI12.TabIndex")));
			this.ttabSingleMotiveUI12.Visible = ((bool)(resources.GetObject("ttabSingleMotiveUI12.Visible")));
			// 
			// TtabMotiveGroupUI
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Controls.Add(this.gbMotiveGroup);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "TtabMotiveGroupUI";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.gbMotiveGroup.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < aTtabSingleMotiveUI.Length; i++)
				aTtabSingleMotiveUI[i].Clear();
		}

	}

	#region MotiveClickEvent
	public class MotiveClickEventArgs : System.EventArgs
	{
		private int motive;
		public MotiveClickEventArgs(int m) : base()  { motive = m; }
		public int Motive { get { return motive; } }
	}
	public delegate void MotiveClickEventHandler(object sender, MotiveClickEventArgs e);
	#endregion
}
