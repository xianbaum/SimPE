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
	/// Summary description for BhavInstListItemUI.
	/// </summary>
	public class BhavInstListItemUI : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label instrText;
		private System.Windows.Forms.LinkLabel trueTarget;
		private System.Windows.Forms.LinkLabel falseTarget;
		private System.Windows.Forms.LinkLabel moveDown;
		private System.Windows.Forms.LinkLabel moveUp;
		private System.Windows.Forms.TextBox bhavInstListItem;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public BhavInstListItemUI()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}

		public BhavInstListItemUI(int index, Instruction inst, int max, PictureBox parent, bool isTarget)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			this.isTarget = isTarget;
			this.inst = inst;
			this.Left = 0;
			this.Height = rowHeight;
			if (isTarget)
				this.BackColor = this.bhavInstListItem.BackColor = System.Drawing.Color.White;
			else
				this.BackColor = this.bhavInstListItem.BackColor = System.Drawing.Color.LightGray;
			this.Tag = index;
			this.Parent = parent;
			parent.Controls.SetChildIndex(this, index);



			bhavInstListItem.Text = "";
			instrText.Text = index.ToString("X") + ": " + inst.ToString();

			trueTarget.Text = "true: "+inst.Target1.ToString("X");
			trueTarget.LinkArea = new LinkArea(0, 0);
			if (inst.Target1 <= max)
				trueTarget.Links.Add(6, trueTarget.Text.Length-6, inst.Target1);
			if (inst.Target1 > max && inst.Target1 < 0xFFFC)
				trueTarget.ForeColor = System.Drawing.Color.Fuchsia;

//			falseTarget.Left = trueTarget.Left + trueTarget.Width + 4;
			falseTarget.Text = "false: "+inst.Target2.ToString("X");
			falseTarget.LinkArea = new LinkArea(0, 0);
			if (inst.Target2 <= max)
				falseTarget.Links.Add(7, falseTarget.Text.Length-7, inst.Target2);
			if (inst.Target2 > max && inst.Target2 < 0xFFFC)
				falseTarget.ForeColor = System.Drawing.Color.Fuchsia;

			if (index == 0)
				moveUp.Visible = false;
			if (index == max)
				moveDown.Visible = false;
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


		#region BhavInstListItemUI
		public const int rowHeight = 32;
		public event EventHandler Selected;
		public event EventHandler Unselected;
		public event LinkLabelLinkClickedEventHandler TargetClick;
		public event EventHandler MoveUp;
		public event EventHandler MoveDown;
		protected virtual void OnSelected(EventArgs e) { if (Selected != null) { Selected(this, e); } }
		protected virtual void OnUnselected(EventArgs e) { if (Unselected != null) { Unselected(this, e); } }
		protected virtual void OnTargetClick(LinkLabelLinkClickedEventArgs e) { if (TargetClick != null) { TargetClick(this, e); } }
		protected virtual void OnMoveUp(EventArgs e) { if (MoveUp != null) { MoveUp(this, e); } }
		protected virtual void OnMoveDown(EventArgs e) { if (MoveDown != null) { MoveDown(this, e); } }


		public void MakeSelected()
		{
			if (isTarget)
				this.BackColor = this.bhavInstListItem.BackColor = System.Drawing.Color.PowderBlue;
			else
				this.BackColor = this.bhavInstListItem.BackColor = System.Drawing.Color.PowderBlue;
		}

		public void MakeUnselected()
		{
			if (isTarget)
				this.BackColor = this.bhavInstListItem.BackColor = System.Drawing.Color.White;
			else
				this.BackColor = this.bhavInstListItem.BackColor = System.Drawing.Color.LightGray;
		}

		protected Instruction inst;
		private bool isTarget = false;
		public int index
		{
			get { return (int)Tag; }
			set { Tag = value; }
		}
		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BhavInstListItemUI));
			this.instrText = new System.Windows.Forms.Label();
			this.trueTarget = new System.Windows.Forms.LinkLabel();
			this.falseTarget = new System.Windows.Forms.LinkLabel();
			this.moveDown = new System.Windows.Forms.LinkLabel();
			this.moveUp = new System.Windows.Forms.LinkLabel();
			this.bhavInstListItem = new System.Windows.Forms.TextBox();
			this.bhavInstListItem.SuspendLayout();
			this.SuspendLayout();
			// 
			// instrText
			// 
			this.instrText.AccessibleDescription = resources.GetString("instrText.AccessibleDescription");
			this.instrText.AccessibleName = resources.GetString("instrText.AccessibleName");
			this.instrText.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("instrText.Anchor")));
			this.instrText.AutoSize = ((bool)(resources.GetObject("instrText.AutoSize")));
			this.instrText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.instrText.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("instrText.Dock")));
			this.instrText.Enabled = ((bool)(resources.GetObject("instrText.Enabled")));
			this.instrText.Font = ((System.Drawing.Font)(resources.GetObject("instrText.Font")));
			this.instrText.Image = ((System.Drawing.Image)(resources.GetObject("instrText.Image")));
			this.instrText.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("instrText.ImageAlign")));
			this.instrText.ImageIndex = ((int)(resources.GetObject("instrText.ImageIndex")));
			this.instrText.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("instrText.ImeMode")));
			this.instrText.Location = ((System.Drawing.Point)(resources.GetObject("instrText.Location")));
			this.instrText.Name = "instrText";
			this.instrText.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("instrText.RightToLeft")));
			this.instrText.Size = ((System.Drawing.Size)(resources.GetObject("instrText.Size")));
			this.instrText.TabIndex = ((int)(resources.GetObject("instrText.TabIndex")));
			this.instrText.Text = resources.GetString("instrText.Text");
			this.instrText.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("instrText.TextAlign")));
			this.instrText.Visible = ((bool)(resources.GetObject("instrText.Visible")));
			this.instrText.Click += new System.EventHandler(this.Control_Click);
			// 
			// trueTarget
			// 
			this.trueTarget.AccessibleDescription = resources.GetString("trueTarget.AccessibleDescription");
			this.trueTarget.AccessibleName = resources.GetString("trueTarget.AccessibleName");
			this.trueTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("trueTarget.Anchor")));
			this.trueTarget.AutoSize = ((bool)(resources.GetObject("trueTarget.AutoSize")));
			this.trueTarget.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("trueTarget.Dock")));
			this.trueTarget.Enabled = ((bool)(resources.GetObject("trueTarget.Enabled")));
			this.trueTarget.Font = ((System.Drawing.Font)(resources.GetObject("trueTarget.Font")));
			this.trueTarget.Image = ((System.Drawing.Image)(resources.GetObject("trueTarget.Image")));
			this.trueTarget.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("trueTarget.ImageAlign")));
			this.trueTarget.ImageIndex = ((int)(resources.GetObject("trueTarget.ImageIndex")));
			this.trueTarget.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("trueTarget.ImeMode")));
			this.trueTarget.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("trueTarget.LinkArea")));
			this.trueTarget.Location = ((System.Drawing.Point)(resources.GetObject("trueTarget.Location")));
			this.trueTarget.Name = "trueTarget";
			this.trueTarget.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("trueTarget.RightToLeft")));
			this.trueTarget.Size = ((System.Drawing.Size)(resources.GetObject("trueTarget.Size")));
			this.trueTarget.TabIndex = ((int)(resources.GetObject("trueTarget.TabIndex")));
			this.trueTarget.Text = resources.GetString("trueTarget.Text");
			this.trueTarget.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("trueTarget.TextAlign")));
			this.trueTarget.Visible = ((bool)(resources.GetObject("trueTarget.Visible")));
			this.trueTarget.Click += new System.EventHandler(this.Control_Click);
			this.trueTarget.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Target_LinkClicked);
			// 
			// falseTarget
			// 
			this.falseTarget.AccessibleDescription = resources.GetString("falseTarget.AccessibleDescription");
			this.falseTarget.AccessibleName = resources.GetString("falseTarget.AccessibleName");
			this.falseTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("falseTarget.Anchor")));
			this.falseTarget.AutoSize = ((bool)(resources.GetObject("falseTarget.AutoSize")));
			this.falseTarget.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("falseTarget.Dock")));
			this.falseTarget.Enabled = ((bool)(resources.GetObject("falseTarget.Enabled")));
			this.falseTarget.Font = ((System.Drawing.Font)(resources.GetObject("falseTarget.Font")));
			this.falseTarget.Image = ((System.Drawing.Image)(resources.GetObject("falseTarget.Image")));
			this.falseTarget.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("falseTarget.ImageAlign")));
			this.falseTarget.ImageIndex = ((int)(resources.GetObject("falseTarget.ImageIndex")));
			this.falseTarget.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("falseTarget.ImeMode")));
			this.falseTarget.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("falseTarget.LinkArea")));
			this.falseTarget.Location = ((System.Drawing.Point)(resources.GetObject("falseTarget.Location")));
			this.falseTarget.Name = "falseTarget";
			this.falseTarget.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("falseTarget.RightToLeft")));
			this.falseTarget.Size = ((System.Drawing.Size)(resources.GetObject("falseTarget.Size")));
			this.falseTarget.TabIndex = ((int)(resources.GetObject("falseTarget.TabIndex")));
			this.falseTarget.Text = resources.GetString("falseTarget.Text");
			this.falseTarget.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("falseTarget.TextAlign")));
			this.falseTarget.Visible = ((bool)(resources.GetObject("falseTarget.Visible")));
			this.falseTarget.Click += new System.EventHandler(this.Control_Click);
			this.falseTarget.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Target_LinkClicked);
			// 
			// moveDown
			// 
			this.moveDown.AccessibleDescription = resources.GetString("moveDown.AccessibleDescription");
			this.moveDown.AccessibleName = resources.GetString("moveDown.AccessibleName");
			this.moveDown.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("moveDown.Anchor")));
			this.moveDown.AutoSize = ((bool)(resources.GetObject("moveDown.AutoSize")));
			this.moveDown.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("moveDown.Dock")));
			this.moveDown.Enabled = ((bool)(resources.GetObject("moveDown.Enabled")));
			this.moveDown.Font = ((System.Drawing.Font)(resources.GetObject("moveDown.Font")));
			this.moveDown.Image = ((System.Drawing.Image)(resources.GetObject("moveDown.Image")));
			this.moveDown.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("moveDown.ImageAlign")));
			this.moveDown.ImageIndex = ((int)(resources.GetObject("moveDown.ImageIndex")));
			this.moveDown.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("moveDown.ImeMode")));
			this.moveDown.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("moveDown.LinkArea")));
			this.moveDown.Location = ((System.Drawing.Point)(resources.GetObject("moveDown.Location")));
			this.moveDown.Name = "moveDown";
			this.moveDown.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("moveDown.RightToLeft")));
			this.moveDown.Size = ((System.Drawing.Size)(resources.GetObject("moveDown.Size")));
			this.moveDown.TabIndex = ((int)(resources.GetObject("moveDown.TabIndex")));
			this.moveDown.TabStop = true;
			this.moveDown.Text = resources.GetString("moveDown.Text");
			this.moveDown.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("moveDown.TextAlign")));
			this.moveDown.Visible = ((bool)(resources.GetObject("moveDown.Visible")));
			this.moveDown.Click += new System.EventHandler(this.Control_Click);
			this.moveDown.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moveDown_LinkClicked);
			// 
			// moveUp
			// 
			this.moveUp.AccessibleDescription = resources.GetString("moveUp.AccessibleDescription");
			this.moveUp.AccessibleName = resources.GetString("moveUp.AccessibleName");
			this.moveUp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("moveUp.Anchor")));
			this.moveUp.AutoSize = ((bool)(resources.GetObject("moveUp.AutoSize")));
			this.moveUp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("moveUp.Dock")));
			this.moveUp.Enabled = ((bool)(resources.GetObject("moveUp.Enabled")));
			this.moveUp.Font = ((System.Drawing.Font)(resources.GetObject("moveUp.Font")));
			this.moveUp.Image = ((System.Drawing.Image)(resources.GetObject("moveUp.Image")));
			this.moveUp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("moveUp.ImageAlign")));
			this.moveUp.ImageIndex = ((int)(resources.GetObject("moveUp.ImageIndex")));
			this.moveUp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("moveUp.ImeMode")));
			this.moveUp.LinkArea = ((System.Windows.Forms.LinkArea)(resources.GetObject("moveUp.LinkArea")));
			this.moveUp.Location = ((System.Drawing.Point)(resources.GetObject("moveUp.Location")));
			this.moveUp.Name = "moveUp";
			this.moveUp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("moveUp.RightToLeft")));
			this.moveUp.Size = ((System.Drawing.Size)(resources.GetObject("moveUp.Size")));
			this.moveUp.TabIndex = ((int)(resources.GetObject("moveUp.TabIndex")));
			this.moveUp.TabStop = true;
			this.moveUp.Text = resources.GetString("moveUp.Text");
			this.moveUp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("moveUp.TextAlign")));
			this.moveUp.Visible = ((bool)(resources.GetObject("moveUp.Visible")));
			this.moveUp.Click += new System.EventHandler(this.Control_Click);
			this.moveUp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moveUp_LinkClicked);
			// 
			// bhavInstListItem
			// 
			this.bhavInstListItem.AccessibleDescription = resources.GetString("bhavInstListItem.AccessibleDescription");
			this.bhavInstListItem.AccessibleName = resources.GetString("bhavInstListItem.AccessibleName");
			this.bhavInstListItem.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("bhavInstListItem.Anchor")));
			this.bhavInstListItem.AutoSize = ((bool)(resources.GetObject("bhavInstListItem.AutoSize")));
			this.bhavInstListItem.BackColor = System.Drawing.Color.White;
			this.bhavInstListItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bhavInstListItem.BackgroundImage")));
			this.bhavInstListItem.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.bhavInstListItem.Controls.Add(this.moveUp);
			this.bhavInstListItem.Controls.Add(this.moveDown);
			this.bhavInstListItem.Controls.Add(this.falseTarget);
			this.bhavInstListItem.Controls.Add(this.trueTarget);
			this.bhavInstListItem.Controls.Add(this.instrText);
			this.bhavInstListItem.Cursor = System.Windows.Forms.Cursors.Default;
			this.bhavInstListItem.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("bhavInstListItem.Dock")));
			this.bhavInstListItem.Enabled = ((bool)(resources.GetObject("bhavInstListItem.Enabled")));
			this.bhavInstListItem.Font = ((System.Drawing.Font)(resources.GetObject("bhavInstListItem.Font")));
			this.bhavInstListItem.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("bhavInstListItem.ImeMode")));
			this.bhavInstListItem.Location = ((System.Drawing.Point)(resources.GetObject("bhavInstListItem.Location")));
			this.bhavInstListItem.MaxLength = ((int)(resources.GetObject("bhavInstListItem.MaxLength")));
			this.bhavInstListItem.Multiline = ((bool)(resources.GetObject("bhavInstListItem.Multiline")));
			this.bhavInstListItem.Name = "bhavInstListItem";
			this.bhavInstListItem.PasswordChar = ((char)(resources.GetObject("bhavInstListItem.PasswordChar")));
			this.bhavInstListItem.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("bhavInstListItem.RightToLeft")));
			this.bhavInstListItem.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("bhavInstListItem.ScrollBars")));
			this.bhavInstListItem.Size = ((System.Drawing.Size)(resources.GetObject("bhavInstListItem.Size")));
			this.bhavInstListItem.TabIndex = ((int)(resources.GetObject("bhavInstListItem.TabIndex")));
			this.bhavInstListItem.Text = resources.GetString("bhavInstListItem.Text");
			this.bhavInstListItem.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("bhavInstListItem.TextAlign")));
			this.bhavInstListItem.Visible = ((bool)(resources.GetObject("bhavInstListItem.Visible")));
			this.bhavInstListItem.WordWrap = ((bool)(resources.GetObject("bhavInstListItem.WordWrap")));
			this.bhavInstListItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bhavInstListItem_KeyDown);
			// 
			// BhavInstListItemUI
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackColor = System.Drawing.SystemColors.Control;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Controls.Add(this.bhavInstListItem);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "BhavInstListItemUI";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.Enter += new System.EventHandler(this.bhavInstListItemUI_Enter);
			this.Leave += new System.EventHandler(this.bhavInstListItemUI_Leave);
			this.bhavInstListItem.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void bhavInstListItemUI_Enter(object sender, System.EventArgs e)
		{
			OnSelected(e);
		}

		private void bhavInstListItemUI_Leave(object sender, System.EventArgs e)
		{
			OnUnselected(e);
		}

		private void bhavInstListItem_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.OnKeyDown(e);
		}

		private void Control_Click(object sender, System.EventArgs e)
		{
			this.Focus();
		}

		private void Target_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			OnTargetClick(e);
		}

		private void moveUp_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			OnMoveUp(e);
		}

		private void moveDown_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			OnMoveDown(e);
		}

	}
}
