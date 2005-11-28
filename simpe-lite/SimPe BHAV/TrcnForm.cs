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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.Interfaces.Plugin;
using SimPe.Plugin;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Zusammenfassung für TrcnForm.
	/// </summary>
	public class TrcnForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables

		private System.Windows.Forms.Panel trcnPanel;
		private System.Windows.Forms.Panel pnHeading;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lbFilename;
		private System.Windows.Forms.TextBox tbFilename;
		private System.Windows.Forms.ListView lvTrcnItem;
		private System.Windows.Forms.ColumnHeader chConstName;
		private System.Windows.Forms.ColumnHeader chUsed;
		private System.Windows.Forms.ColumnHeader chConstId;
		private System.Windows.Forms.ColumnHeader chDefValue;
		private System.Windows.Forms.ColumnHeader chMinValue;
		private System.Windows.Forms.ColumnHeader chMaxValue;
		private System.Windows.Forms.Label lbFormat;
		private System.Windows.Forms.TextBox tbFormat;
		private System.Windows.Forms.Button btnStrDelete;
		private System.Windows.Forms.Button btnStrAdd;
		private System.Windows.Forms.Button btnStrPrev;
		private System.Windows.Forms.Button btnStrNext;
		private System.Windows.Forms.Label lbID;
		private System.Windows.Forms.Label lbDefValue;
		private System.Windows.Forms.Label lbMinValue;
		private System.Windows.Forms.Label lbMaxValue;
		private System.Windows.Forms.Label lbLabel;
		private System.Windows.Forms.TextBox tbDefValue;
		private System.Windows.Forms.TextBox tbMinValue;
		private System.Windows.Forms.TextBox tbMaxValue;
		private System.Windows.Forms.TextBox tbLabel;
		private System.Windows.Forms.CheckBox cbUsed;
		private System.Windows.Forms.ComboBox cbID;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public TrcnForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			TextBox[] t = { tbFilename ,tbLabel ,};
			alText = new ArrayList(t);

			TextBox[] w = { tbDefValue ,tbMinValue ,tbMaxValue ,};
			alHex16 = new ArrayList(w);

			TextBox[] dw = { tbFormat ,};
			alHex32 = new ArrayList(dw);

			ComboBox[] cb = { cbID ,};
			alHex32cb = new ArrayList(cb);
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


		#region TrcnForm
		private Trcn wrapper = null;
		private bool internalchg = false;
		private bool setHandler = false;
		private TrcnItem origItem = null;
		private TrcnItem currentItem = null;
		private ArrayList alText = null;
		private ArrayList alHex16 = null;
		private ArrayList alHex32 = null;
		private ArrayList alHex32cb = null;

		private bool cbHex32_IsValid(object sender)
		{
			if (alHex32cb.IndexOf(sender) < 0)
				throw new Exception("cbHex32_IsValid not applicable to control " + sender.ToString());
			if (((ComboBox)sender).Items.IndexOf(((ComboBox)sender).Text) != -1) return true;

			try { Convert.ToUInt32(((ComboBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private bool hex16_IsValid(object sender)
		{
			if (alHex16.IndexOf(sender) < 0)
				throw new Exception("hex16_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToUInt16(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private bool hex32_IsValid(object sender)
		{
			if (alHex32.IndexOf(sender) < 0)
				throw new Exception("hex32_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToUInt32(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}


		private string[] TrcnItemToStringArray(TrcnItem ti)
		{
			return new string[] {
									"0x" + ti.ConstId.ToString("X")
									, ti.ConstName
									, "0x" + ti.Used.ToString("X")
									, "0x" + SimPe.Helper.HexString(ti.DefValue)
									, "0x" + SimPe.Helper.HexString(ti.MinValue)
									, "0x" + SimPe.Helper.HexString(ti.MaxValue)
								};

		}

		private void SetIDs()
		{
			this.cbID.Items.Clear();

			Bcon bcon = wrapper.BconResource;
			for(int i = 0; i < bcon.Constants.Count; i++)
			{
				this.cbID.Items.Add(
					"0x" + i.ToString("X") + ": "
					+ "0x" + SimPe.Helper.HexString((short)bcon.Constants[i])
					);
			}
		}
		#endregion

		#region IPackedFileUI Member
		/// <summary>
		/// Returns the Panel that will be displayed within SimPe
		/// </summary>
		public System.Windows.Forms.Control GUIHandle
		{
			get
			{
				return trcnPanel;
			}
		}

		/// <summary>
		/// Is called by SimPe (through the Wrapper) when the Panel is going to be displayed, so
		/// you should updatet the Data displayed by the Panel with the Attributes stored in the
		/// passed Wrapper.
		/// </summary>
		/// <remarks>attr.Tag is used to let TextChanged event handlers know the change is being
		/// made internally rather than by the users.</remarks>
		/// <param name="wrp">The Attributes of this Wrapper have to be displayed</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (Trcn)wrp;
			WrapperChanged(wrapper, null);

			internalchg = true;

			this.lvTrcnItem.Items.Clear();
			foreach(TrcnItem ti in wrapper)
				this.lvTrcnItem.Items.Add(new ListViewItem(TrcnItemToStringArray(ti)));

			internalchg = false;

			if (lvTrcnItem.Items.Count > 0)
				lvTrcnItem.Items[0].Selected = true;
			else
				lvTrcnItem_SelectedIndexChanged(null, null);

			if (!setHandler)
			{
				wrapper.WrapperChanged += new System.EventHandler(this.WrapperChanged);
				setHandler = true;
			}

		}

		private void WrapperChanged(object sender, System.EventArgs e)
		{
			this.btnCommit.Enabled = wrapper.Changed;

			if (sender.Equals(wrapper))
			{
				if (internalchg) return;
				internalchg = true;
				this.Text = tbFilename.Text = wrapper.FileName;
				this.tbFormat.Text = "0x" + SimPe.Helper.HexString(wrapper.Version);
				SetIDs();
				internalchg = false;
			}
			else if (sender.Equals(currentItem))
			{
				if (internalchg)
					this.btnCancel.Enabled = true;
				else
					lvTrcnItem_SelectedIndexChanged(null, null);
			}
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TrcnForm));
			this.btnCommit = new System.Windows.Forms.Button();
			this.pnHeading = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.trcnPanel = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.cbID = new System.Windows.Forms.ComboBox();
			this.cbUsed = new System.Windows.Forms.CheckBox();
			this.tbLabel = new System.Windows.Forms.TextBox();
			this.lbID = new System.Windows.Forms.Label();
			this.btnStrPrev = new System.Windows.Forms.Button();
			this.btnStrNext = new System.Windows.Forms.Button();
			this.btnStrDelete = new System.Windows.Forms.Button();
			this.btnStrAdd = new System.Windows.Forms.Button();
			this.lbFormat = new System.Windows.Forms.Label();
			this.tbFormat = new System.Windows.Forms.TextBox();
			this.lvTrcnItem = new System.Windows.Forms.ListView();
			this.chConstId = new System.Windows.Forms.ColumnHeader();
			this.chConstName = new System.Windows.Forms.ColumnHeader();
			this.chUsed = new System.Windows.Forms.ColumnHeader();
			this.chDefValue = new System.Windows.Forms.ColumnHeader();
			this.chMinValue = new System.Windows.Forms.ColumnHeader();
			this.chMaxValue = new System.Windows.Forms.ColumnHeader();
			this.tbFilename = new System.Windows.Forms.TextBox();
			this.lbFilename = new System.Windows.Forms.Label();
			this.lbDefValue = new System.Windows.Forms.Label();
			this.tbDefValue = new System.Windows.Forms.TextBox();
			this.tbMinValue = new System.Windows.Forms.TextBox();
			this.lbMinValue = new System.Windows.Forms.Label();
			this.tbMaxValue = new System.Windows.Forms.TextBox();
			this.lbMaxValue = new System.Windows.Forms.Label();
			this.lbLabel = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.pnHeading.SuspendLayout();
			this.trcnPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCommit
			// 
			this.btnCommit.AccessibleDescription = resources.GetString("btnCommit.AccessibleDescription");
			this.btnCommit.AccessibleName = resources.GetString("btnCommit.AccessibleName");
			this.btnCommit.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCommit.Anchor")));
			this.btnCommit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCommit.BackgroundImage")));
			this.btnCommit.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCommit.Dock")));
			this.btnCommit.Enabled = ((bool)(resources.GetObject("btnCommit.Enabled")));
			this.btnCommit.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCommit.FlatStyle")));
			this.btnCommit.Font = ((System.Drawing.Font)(resources.GetObject("btnCommit.Font")));
			this.btnCommit.Image = ((System.Drawing.Image)(resources.GetObject("btnCommit.Image")));
			this.btnCommit.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCommit.ImageAlign")));
			this.btnCommit.ImageIndex = ((int)(resources.GetObject("btnCommit.ImageIndex")));
			this.btnCommit.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCommit.ImeMode")));
			this.btnCommit.Location = ((System.Drawing.Point)(resources.GetObject("btnCommit.Location")));
			this.btnCommit.Name = "btnCommit";
			this.btnCommit.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCommit.RightToLeft")));
			this.btnCommit.Size = ((System.Drawing.Size)(resources.GetObject("btnCommit.Size")));
			this.btnCommit.TabIndex = ((int)(resources.GetObject("btnCommit.TabIndex")));
			this.btnCommit.Text = resources.GetString("btnCommit.Text");
			this.btnCommit.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCommit.TextAlign")));
			this.btnCommit.Visible = ((bool)(resources.GetObject("btnCommit.Visible")));
			this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
			// 
			// pnHeading
			// 
			this.pnHeading.AccessibleDescription = resources.GetString("pnHeading.AccessibleDescription");
			this.pnHeading.AccessibleName = resources.GetString("pnHeading.AccessibleName");
			this.pnHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pnHeading.Anchor")));
			this.pnHeading.AutoScroll = ((bool)(resources.GetObject("pnHeading.AutoScroll")));
			this.pnHeading.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("pnHeading.AutoScrollMargin")));
			this.pnHeading.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("pnHeading.AutoScrollMinSize")));
			this.pnHeading.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.pnHeading.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnHeading.BackgroundImage")));
			this.pnHeading.Controls.Add(this.label1);
			this.pnHeading.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnHeading.Dock")));
			this.pnHeading.Enabled = ((bool)(resources.GetObject("pnHeading.Enabled")));
			this.pnHeading.Font = ((System.Drawing.Font)(resources.GetObject("pnHeading.Font")));
			this.pnHeading.ForeColor = System.Drawing.SystemColors.ControlText;
			this.pnHeading.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnHeading.ImeMode")));
			this.pnHeading.Location = ((System.Drawing.Point)(resources.GetObject("pnHeading.Location")));
			this.pnHeading.Name = "pnHeading";
			this.pnHeading.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnHeading.RightToLeft")));
			this.pnHeading.Size = ((System.Drawing.Size)(resources.GetObject("pnHeading.Size")));
			this.pnHeading.TabIndex = ((int)(resources.GetObject("pnHeading.TabIndex")));
			this.pnHeading.Text = resources.GetString("pnHeading.Text");
			this.pnHeading.Visible = ((bool)(resources.GetObject("pnHeading.Visible")));
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
			this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
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
			// trcnPanel
			// 
			this.trcnPanel.AccessibleDescription = resources.GetString("trcnPanel.AccessibleDescription");
			this.trcnPanel.AccessibleName = resources.GetString("trcnPanel.AccessibleName");
			this.trcnPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("trcnPanel.Anchor")));
			this.trcnPanel.AutoScroll = ((bool)(resources.GetObject("trcnPanel.AutoScroll")));
			this.trcnPanel.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("trcnPanel.AutoScrollMargin")));
			this.trcnPanel.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("trcnPanel.AutoScrollMinSize")));
			this.trcnPanel.BackColor = System.Drawing.SystemColors.Control;
			this.trcnPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("trcnPanel.BackgroundImage")));
			this.trcnPanel.Controls.Add(this.label4);
			this.trcnPanel.Controls.Add(this.label3);
			this.trcnPanel.Controls.Add(this.label2);
			this.trcnPanel.Controls.Add(this.btnCancel);
			this.trcnPanel.Controls.Add(this.cbID);
			this.trcnPanel.Controls.Add(this.cbUsed);
			this.trcnPanel.Controls.Add(this.tbLabel);
			this.trcnPanel.Controls.Add(this.lbID);
			this.trcnPanel.Controls.Add(this.btnStrPrev);
			this.trcnPanel.Controls.Add(this.btnStrNext);
			this.trcnPanel.Controls.Add(this.btnStrDelete);
			this.trcnPanel.Controls.Add(this.btnStrAdd);
			this.trcnPanel.Controls.Add(this.lbFormat);
			this.trcnPanel.Controls.Add(this.tbFormat);
			this.trcnPanel.Controls.Add(this.lvTrcnItem);
			this.trcnPanel.Controls.Add(this.tbFilename);
			this.trcnPanel.Controls.Add(this.lbFilename);
			this.trcnPanel.Controls.Add(this.btnCommit);
			this.trcnPanel.Controls.Add(this.pnHeading);
			this.trcnPanel.Controls.Add(this.lbDefValue);
			this.trcnPanel.Controls.Add(this.tbDefValue);
			this.trcnPanel.Controls.Add(this.tbMinValue);
			this.trcnPanel.Controls.Add(this.lbMinValue);
			this.trcnPanel.Controls.Add(this.tbMaxValue);
			this.trcnPanel.Controls.Add(this.lbMaxValue);
			this.trcnPanel.Controls.Add(this.lbLabel);
			this.trcnPanel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("trcnPanel.Dock")));
			this.trcnPanel.Enabled = ((bool)(resources.GetObject("trcnPanel.Enabled")));
			this.trcnPanel.Font = ((System.Drawing.Font)(resources.GetObject("trcnPanel.Font")));
			this.trcnPanel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("trcnPanel.ImeMode")));
			this.trcnPanel.Location = ((System.Drawing.Point)(resources.GetObject("trcnPanel.Location")));
			this.trcnPanel.Name = "trcnPanel";
			this.trcnPanel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("trcnPanel.RightToLeft")));
			this.trcnPanel.Size = ((System.Drawing.Size)(resources.GetObject("trcnPanel.Size")));
			this.trcnPanel.TabIndex = ((int)(resources.GetObject("trcnPanel.TabIndex")));
			this.trcnPanel.Text = resources.GetString("trcnPanel.Text");
			this.trcnPanel.Visible = ((bool)(resources.GetObject("trcnPanel.Visible")));
			// 
			// btnCancel
			// 
			this.btnCancel.AccessibleDescription = resources.GetString("btnCancel.AccessibleDescription");
			this.btnCancel.AccessibleName = resources.GetString("btnCancel.AccessibleName");
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCancel.Anchor")));
			this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
			this.btnCancel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCancel.Dock")));
			this.btnCancel.Enabled = ((bool)(resources.GetObject("btnCancel.Enabled")));
			this.btnCancel.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCancel.FlatStyle")));
			this.btnCancel.Font = ((System.Drawing.Font)(resources.GetObject("btnCancel.Font")));
			this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
			this.btnCancel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCancel.ImageAlign")));
			this.btnCancel.ImageIndex = ((int)(resources.GetObject("btnCancel.ImageIndex")));
			this.btnCancel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCancel.ImeMode")));
			this.btnCancel.Location = ((System.Drawing.Point)(resources.GetObject("btnCancel.Location")));
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCancel.RightToLeft")));
			this.btnCancel.Size = ((System.Drawing.Size)(resources.GetObject("btnCancel.Size")));
			this.btnCancel.TabIndex = ((int)(resources.GetObject("btnCancel.TabIndex")));
			this.btnCancel.Text = resources.GetString("btnCancel.Text");
			this.btnCancel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCancel.TextAlign")));
			this.btnCancel.Visible = ((bool)(resources.GetObject("btnCancel.Visible")));
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// cbID
			// 
			this.cbID.AccessibleDescription = resources.GetString("cbID.AccessibleDescription");
			this.cbID.AccessibleName = resources.GetString("cbID.AccessibleName");
			this.cbID.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbID.Anchor")));
			this.cbID.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbID.BackgroundImage")));
			this.cbID.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbID.Dock")));
			this.cbID.Enabled = ((bool)(resources.GetObject("cbID.Enabled")));
			this.cbID.Font = ((System.Drawing.Font)(resources.GetObject("cbID.Font")));
			this.cbID.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbID.ImeMode")));
			this.cbID.IntegralHeight = ((bool)(resources.GetObject("cbID.IntegralHeight")));
			this.cbID.ItemHeight = ((int)(resources.GetObject("cbID.ItemHeight")));
			this.cbID.Location = ((System.Drawing.Point)(resources.GetObject("cbID.Location")));
			this.cbID.MaxDropDownItems = ((int)(resources.GetObject("cbID.MaxDropDownItems")));
			this.cbID.MaxLength = ((int)(resources.GetObject("cbID.MaxLength")));
			this.cbID.Name = "cbID";
			this.cbID.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbID.RightToLeft")));
			this.cbID.Size = ((System.Drawing.Size)(resources.GetObject("cbID.Size")));
			this.cbID.TabIndex = ((int)(resources.GetObject("cbID.TabIndex")));
			this.cbID.Text = resources.GetString("cbID.Text");
			this.cbID.Visible = ((bool)(resources.GetObject("cbID.Visible")));
			this.cbID.Validating += new System.ComponentModel.CancelEventHandler(this.cbHex32_Validating);
			this.cbID.Validated += new System.EventHandler(this.cbHex32_Validated);
			this.cbID.TextChanged += new System.EventHandler(this.cbHex32_TextChanged);
			this.cbID.SelectedIndexChanged += new System.EventHandler(this.cbHex32_SelectedIndexChanged);
			this.cbID.Enter += new System.EventHandler(this.cbHex32_Enter);
			// 
			// cbUsed
			// 
			this.cbUsed.AccessibleDescription = resources.GetString("cbUsed.AccessibleDescription");
			this.cbUsed.AccessibleName = resources.GetString("cbUsed.AccessibleName");
			this.cbUsed.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cbUsed.Anchor")));
			this.cbUsed.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("cbUsed.Appearance")));
			this.cbUsed.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cbUsed.BackgroundImage")));
			this.cbUsed.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbUsed.CheckAlign")));
			this.cbUsed.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cbUsed.Dock")));
			this.cbUsed.Enabled = ((bool)(resources.GetObject("cbUsed.Enabled")));
			this.cbUsed.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cbUsed.FlatStyle")));
			this.cbUsed.Font = ((System.Drawing.Font)(resources.GetObject("cbUsed.Font")));
			this.cbUsed.Image = ((System.Drawing.Image)(resources.GetObject("cbUsed.Image")));
			this.cbUsed.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbUsed.ImageAlign")));
			this.cbUsed.ImageIndex = ((int)(resources.GetObject("cbUsed.ImageIndex")));
			this.cbUsed.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cbUsed.ImeMode")));
			this.cbUsed.Location = ((System.Drawing.Point)(resources.GetObject("cbUsed.Location")));
			this.cbUsed.Name = "cbUsed";
			this.cbUsed.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cbUsed.RightToLeft")));
			this.cbUsed.Size = ((System.Drawing.Size)(resources.GetObject("cbUsed.Size")));
			this.cbUsed.TabIndex = ((int)(resources.GetObject("cbUsed.TabIndex")));
			this.cbUsed.Text = resources.GetString("cbUsed.Text");
			this.cbUsed.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cbUsed.TextAlign")));
			this.cbUsed.Visible = ((bool)(resources.GetObject("cbUsed.Visible")));
			this.cbUsed.CheckedChanged += new System.EventHandler(this.cbUsed_CheckedChanged);
			// 
			// tbLabel
			// 
			this.tbLabel.AccessibleDescription = resources.GetString("tbLabel.AccessibleDescription");
			this.tbLabel.AccessibleName = resources.GetString("tbLabel.AccessibleName");
			this.tbLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbLabel.Anchor")));
			this.tbLabel.AutoSize = ((bool)(resources.GetObject("tbLabel.AutoSize")));
			this.tbLabel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbLabel.BackgroundImage")));
			this.tbLabel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbLabel.Dock")));
			this.tbLabel.Enabled = ((bool)(resources.GetObject("tbLabel.Enabled")));
			this.tbLabel.Font = ((System.Drawing.Font)(resources.GetObject("tbLabel.Font")));
			this.tbLabel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbLabel.ImeMode")));
			this.tbLabel.Location = ((System.Drawing.Point)(resources.GetObject("tbLabel.Location")));
			this.tbLabel.MaxLength = ((int)(resources.GetObject("tbLabel.MaxLength")));
			this.tbLabel.Multiline = ((bool)(resources.GetObject("tbLabel.Multiline")));
			this.tbLabel.Name = "tbLabel";
			this.tbLabel.PasswordChar = ((char)(resources.GetObject("tbLabel.PasswordChar")));
			this.tbLabel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbLabel.RightToLeft")));
			this.tbLabel.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbLabel.ScrollBars")));
			this.tbLabel.Size = ((System.Drawing.Size)(resources.GetObject("tbLabel.Size")));
			this.tbLabel.TabIndex = ((int)(resources.GetObject("tbLabel.TabIndex")));
			this.tbLabel.Text = resources.GetString("tbLabel.Text");
			this.tbLabel.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbLabel.TextAlign")));
			this.tbLabel.Visible = ((bool)(resources.GetObject("tbLabel.Visible")));
			this.tbLabel.WordWrap = ((bool)(resources.GetObject("tbLabel.WordWrap")));
			this.tbLabel.Validated += new System.EventHandler(this.tbText_Validated);
			this.tbLabel.TextChanged += new System.EventHandler(this.tbText_TextChanged);
			// 
			// lbID
			// 
			this.lbID.AccessibleDescription = resources.GetString("lbID.AccessibleDescription");
			this.lbID.AccessibleName = resources.GetString("lbID.AccessibleName");
			this.lbID.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbID.Anchor")));
			this.lbID.AutoSize = ((bool)(resources.GetObject("lbID.AutoSize")));
			this.lbID.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbID.Dock")));
			this.lbID.Enabled = ((bool)(resources.GetObject("lbID.Enabled")));
			this.lbID.Font = ((System.Drawing.Font)(resources.GetObject("lbID.Font")));
			this.lbID.Image = ((System.Drawing.Image)(resources.GetObject("lbID.Image")));
			this.lbID.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbID.ImageAlign")));
			this.lbID.ImageIndex = ((int)(resources.GetObject("lbID.ImageIndex")));
			this.lbID.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbID.ImeMode")));
			this.lbID.Location = ((System.Drawing.Point)(resources.GetObject("lbID.Location")));
			this.lbID.Name = "lbID";
			this.lbID.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbID.RightToLeft")));
			this.lbID.Size = ((System.Drawing.Size)(resources.GetObject("lbID.Size")));
			this.lbID.TabIndex = ((int)(resources.GetObject("lbID.TabIndex")));
			this.lbID.Text = resources.GetString("lbID.Text");
			this.lbID.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbID.TextAlign")));
			this.lbID.Visible = ((bool)(resources.GetObject("lbID.Visible")));
			// 
			// btnStrPrev
			// 
			this.btnStrPrev.AccessibleDescription = resources.GetString("btnStrPrev.AccessibleDescription");
			this.btnStrPrev.AccessibleName = resources.GetString("btnStrPrev.AccessibleName");
			this.btnStrPrev.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnStrPrev.Anchor")));
			this.btnStrPrev.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStrPrev.BackgroundImage")));
			this.btnStrPrev.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnStrPrev.Dock")));
			this.btnStrPrev.Enabled = ((bool)(resources.GetObject("btnStrPrev.Enabled")));
			this.btnStrPrev.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnStrPrev.FlatStyle")));
			this.btnStrPrev.Font = ((System.Drawing.Font)(resources.GetObject("btnStrPrev.Font")));
			this.btnStrPrev.Image = ((System.Drawing.Image)(resources.GetObject("btnStrPrev.Image")));
			this.btnStrPrev.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnStrPrev.ImageAlign")));
			this.btnStrPrev.ImageIndex = ((int)(resources.GetObject("btnStrPrev.ImageIndex")));
			this.btnStrPrev.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnStrPrev.ImeMode")));
			this.btnStrPrev.Location = ((System.Drawing.Point)(resources.GetObject("btnStrPrev.Location")));
			this.btnStrPrev.Name = "btnStrPrev";
			this.btnStrPrev.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnStrPrev.RightToLeft")));
			this.btnStrPrev.Size = ((System.Drawing.Size)(resources.GetObject("btnStrPrev.Size")));
			this.btnStrPrev.TabIndex = ((int)(resources.GetObject("btnStrPrev.TabIndex")));
			this.btnStrPrev.Text = resources.GetString("btnStrPrev.Text");
			this.btnStrPrev.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnStrPrev.TextAlign")));
			this.btnStrPrev.Visible = ((bool)(resources.GetObject("btnStrPrev.Visible")));
			// 
			// btnStrNext
			// 
			this.btnStrNext.AccessibleDescription = resources.GetString("btnStrNext.AccessibleDescription");
			this.btnStrNext.AccessibleName = resources.GetString("btnStrNext.AccessibleName");
			this.btnStrNext.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnStrNext.Anchor")));
			this.btnStrNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStrNext.BackgroundImage")));
			this.btnStrNext.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnStrNext.Dock")));
			this.btnStrNext.Enabled = ((bool)(resources.GetObject("btnStrNext.Enabled")));
			this.btnStrNext.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnStrNext.FlatStyle")));
			this.btnStrNext.Font = ((System.Drawing.Font)(resources.GetObject("btnStrNext.Font")));
			this.btnStrNext.Image = ((System.Drawing.Image)(resources.GetObject("btnStrNext.Image")));
			this.btnStrNext.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnStrNext.ImageAlign")));
			this.btnStrNext.ImageIndex = ((int)(resources.GetObject("btnStrNext.ImageIndex")));
			this.btnStrNext.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnStrNext.ImeMode")));
			this.btnStrNext.Location = ((System.Drawing.Point)(resources.GetObject("btnStrNext.Location")));
			this.btnStrNext.Name = "btnStrNext";
			this.btnStrNext.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnStrNext.RightToLeft")));
			this.btnStrNext.Size = ((System.Drawing.Size)(resources.GetObject("btnStrNext.Size")));
			this.btnStrNext.TabIndex = ((int)(resources.GetObject("btnStrNext.TabIndex")));
			this.btnStrNext.Text = resources.GetString("btnStrNext.Text");
			this.btnStrNext.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnStrNext.TextAlign")));
			this.btnStrNext.Visible = ((bool)(resources.GetObject("btnStrNext.Visible")));
			// 
			// btnStrDelete
			// 
			this.btnStrDelete.AccessibleDescription = resources.GetString("btnStrDelete.AccessibleDescription");
			this.btnStrDelete.AccessibleName = resources.GetString("btnStrDelete.AccessibleName");
			this.btnStrDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnStrDelete.Anchor")));
			this.btnStrDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStrDelete.BackgroundImage")));
			this.btnStrDelete.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnStrDelete.Dock")));
			this.btnStrDelete.Enabled = ((bool)(resources.GetObject("btnStrDelete.Enabled")));
			this.btnStrDelete.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnStrDelete.FlatStyle")));
			this.btnStrDelete.Font = ((System.Drawing.Font)(resources.GetObject("btnStrDelete.Font")));
			this.btnStrDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnStrDelete.Image")));
			this.btnStrDelete.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnStrDelete.ImageAlign")));
			this.btnStrDelete.ImageIndex = ((int)(resources.GetObject("btnStrDelete.ImageIndex")));
			this.btnStrDelete.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnStrDelete.ImeMode")));
			this.btnStrDelete.Location = ((System.Drawing.Point)(resources.GetObject("btnStrDelete.Location")));
			this.btnStrDelete.Name = "btnStrDelete";
			this.btnStrDelete.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnStrDelete.RightToLeft")));
			this.btnStrDelete.Size = ((System.Drawing.Size)(resources.GetObject("btnStrDelete.Size")));
			this.btnStrDelete.TabIndex = ((int)(resources.GetObject("btnStrDelete.TabIndex")));
			this.btnStrDelete.Text = resources.GetString("btnStrDelete.Text");
			this.btnStrDelete.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnStrDelete.TextAlign")));
			this.btnStrDelete.Visible = ((bool)(resources.GetObject("btnStrDelete.Visible")));
			this.btnStrDelete.Click += new System.EventHandler(this.btnStrDelete_Click);
			// 
			// btnStrAdd
			// 
			this.btnStrAdd.AccessibleDescription = resources.GetString("btnStrAdd.AccessibleDescription");
			this.btnStrAdd.AccessibleName = resources.GetString("btnStrAdd.AccessibleName");
			this.btnStrAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnStrAdd.Anchor")));
			this.btnStrAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStrAdd.BackgroundImage")));
			this.btnStrAdd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnStrAdd.Dock")));
			this.btnStrAdd.Enabled = ((bool)(resources.GetObject("btnStrAdd.Enabled")));
			this.btnStrAdd.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnStrAdd.FlatStyle")));
			this.btnStrAdd.Font = ((System.Drawing.Font)(resources.GetObject("btnStrAdd.Font")));
			this.btnStrAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnStrAdd.Image")));
			this.btnStrAdd.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnStrAdd.ImageAlign")));
			this.btnStrAdd.ImageIndex = ((int)(resources.GetObject("btnStrAdd.ImageIndex")));
			this.btnStrAdd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnStrAdd.ImeMode")));
			this.btnStrAdd.Location = ((System.Drawing.Point)(resources.GetObject("btnStrAdd.Location")));
			this.btnStrAdd.Name = "btnStrAdd";
			this.btnStrAdd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnStrAdd.RightToLeft")));
			this.btnStrAdd.Size = ((System.Drawing.Size)(resources.GetObject("btnStrAdd.Size")));
			this.btnStrAdd.TabIndex = ((int)(resources.GetObject("btnStrAdd.TabIndex")));
			this.btnStrAdd.Text = resources.GetString("btnStrAdd.Text");
			this.btnStrAdd.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnStrAdd.TextAlign")));
			this.btnStrAdd.Visible = ((bool)(resources.GetObject("btnStrAdd.Visible")));
			this.btnStrAdd.Click += new System.EventHandler(this.btnStrAdd_Click);
			// 
			// lbFormat
			// 
			this.lbFormat.AccessibleDescription = resources.GetString("lbFormat.AccessibleDescription");
			this.lbFormat.AccessibleName = resources.GetString("lbFormat.AccessibleName");
			this.lbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbFormat.Anchor")));
			this.lbFormat.AutoSize = ((bool)(resources.GetObject("lbFormat.AutoSize")));
			this.lbFormat.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbFormat.Dock")));
			this.lbFormat.Enabled = ((bool)(resources.GetObject("lbFormat.Enabled")));
			this.lbFormat.Font = ((System.Drawing.Font)(resources.GetObject("lbFormat.Font")));
			this.lbFormat.Image = ((System.Drawing.Image)(resources.GetObject("lbFormat.Image")));
			this.lbFormat.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFormat.ImageAlign")));
			this.lbFormat.ImageIndex = ((int)(resources.GetObject("lbFormat.ImageIndex")));
			this.lbFormat.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbFormat.ImeMode")));
			this.lbFormat.Location = ((System.Drawing.Point)(resources.GetObject("lbFormat.Location")));
			this.lbFormat.Name = "lbFormat";
			this.lbFormat.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbFormat.RightToLeft")));
			this.lbFormat.Size = ((System.Drawing.Size)(resources.GetObject("lbFormat.Size")));
			this.lbFormat.TabIndex = ((int)(resources.GetObject("lbFormat.TabIndex")));
			this.lbFormat.Text = resources.GetString("lbFormat.Text");
			this.lbFormat.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFormat.TextAlign")));
			this.lbFormat.Visible = ((bool)(resources.GetObject("lbFormat.Visible")));
			// 
			// tbFormat
			// 
			this.tbFormat.AccessibleDescription = resources.GetString("tbFormat.AccessibleDescription");
			this.tbFormat.AccessibleName = resources.GetString("tbFormat.AccessibleName");
			this.tbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbFormat.Anchor")));
			this.tbFormat.AutoSize = ((bool)(resources.GetObject("tbFormat.AutoSize")));
			this.tbFormat.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbFormat.BackgroundImage")));
			this.tbFormat.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbFormat.Dock")));
			this.tbFormat.Enabled = ((bool)(resources.GetObject("tbFormat.Enabled")));
			this.tbFormat.Font = ((System.Drawing.Font)(resources.GetObject("tbFormat.Font")));
			this.tbFormat.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbFormat.ImeMode")));
			this.tbFormat.Location = ((System.Drawing.Point)(resources.GetObject("tbFormat.Location")));
			this.tbFormat.MaxLength = ((int)(resources.GetObject("tbFormat.MaxLength")));
			this.tbFormat.Multiline = ((bool)(resources.GetObject("tbFormat.Multiline")));
			this.tbFormat.Name = "tbFormat";
			this.tbFormat.PasswordChar = ((char)(resources.GetObject("tbFormat.PasswordChar")));
			this.tbFormat.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbFormat.RightToLeft")));
			this.tbFormat.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbFormat.ScrollBars")));
			this.tbFormat.Size = ((System.Drawing.Size)(resources.GetObject("tbFormat.Size")));
			this.tbFormat.TabIndex = ((int)(resources.GetObject("tbFormat.TabIndex")));
			this.tbFormat.Text = resources.GetString("tbFormat.Text");
			this.tbFormat.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbFormat.TextAlign")));
			this.tbFormat.Visible = ((bool)(resources.GetObject("tbFormat.Visible")));
			this.tbFormat.WordWrap = ((bool)(resources.GetObject("tbFormat.WordWrap")));
			this.tbFormat.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
			this.tbFormat.Validated += new System.EventHandler(this.hex32_Validated);
			this.tbFormat.TextChanged += new System.EventHandler(this.hex32_TextChanged);
			// 
			// lvTrcnItem
			// 
			this.lvTrcnItem.AccessibleDescription = resources.GetString("lvTrcnItem.AccessibleDescription");
			this.lvTrcnItem.AccessibleName = resources.GetString("lvTrcnItem.AccessibleName");
			this.lvTrcnItem.Alignment = ((System.Windows.Forms.ListViewAlignment)(resources.GetObject("lvTrcnItem.Alignment")));
			this.lvTrcnItem.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lvTrcnItem.Anchor")));
			this.lvTrcnItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lvTrcnItem.BackgroundImage")));
			this.lvTrcnItem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						 this.chConstId,
																						 this.chConstName,
																						 this.chUsed,
																						 this.chDefValue,
																						 this.chMinValue,
																						 this.chMaxValue});
			this.lvTrcnItem.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lvTrcnItem.Dock")));
			this.lvTrcnItem.Enabled = ((bool)(resources.GetObject("lvTrcnItem.Enabled")));
			this.lvTrcnItem.Font = ((System.Drawing.Font)(resources.GetObject("lvTrcnItem.Font")));
			this.lvTrcnItem.FullRowSelect = true;
			this.lvTrcnItem.GridLines = true;
			this.lvTrcnItem.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvTrcnItem.HideSelection = false;
			this.lvTrcnItem.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lvTrcnItem.ImeMode")));
			this.lvTrcnItem.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
																					   ((System.Windows.Forms.ListViewItem)(resources.GetObject("lvTrcnItem.Items")))});
			this.lvTrcnItem.LabelWrap = ((bool)(resources.GetObject("lvTrcnItem.LabelWrap")));
			this.lvTrcnItem.Location = ((System.Drawing.Point)(resources.GetObject("lvTrcnItem.Location")));
			this.lvTrcnItem.MultiSelect = false;
			this.lvTrcnItem.Name = "lvTrcnItem";
			this.lvTrcnItem.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lvTrcnItem.RightToLeft")));
			this.lvTrcnItem.Size = ((System.Drawing.Size)(resources.GetObject("lvTrcnItem.Size")));
			this.lvTrcnItem.TabIndex = ((int)(resources.GetObject("lvTrcnItem.TabIndex")));
			this.lvTrcnItem.Text = resources.GetString("lvTrcnItem.Text");
			this.lvTrcnItem.View = System.Windows.Forms.View.Details;
			this.lvTrcnItem.Visible = ((bool)(resources.GetObject("lvTrcnItem.Visible")));
			this.lvTrcnItem.SelectedIndexChanged += new System.EventHandler(this.lvTrcnItem_SelectedIndexChanged);
			// 
			// chConstId
			// 
			this.chConstId.Text = resources.GetString("chConstId.Text");
			this.chConstId.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("chConstId.TextAlign")));
			this.chConstId.Width = ((int)(resources.GetObject("chConstId.Width")));
			// 
			// chConstName
			// 
			this.chConstName.Text = resources.GetString("chConstName.Text");
			this.chConstName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("chConstName.TextAlign")));
			this.chConstName.Width = ((int)(resources.GetObject("chConstName.Width")));
			// 
			// chUsed
			// 
			this.chUsed.Text = resources.GetString("chUsed.Text");
			this.chUsed.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("chUsed.TextAlign")));
			this.chUsed.Width = ((int)(resources.GetObject("chUsed.Width")));
			// 
			// chDefValue
			// 
			this.chDefValue.Text = resources.GetString("chDefValue.Text");
			this.chDefValue.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("chDefValue.TextAlign")));
			this.chDefValue.Width = ((int)(resources.GetObject("chDefValue.Width")));
			// 
			// chMinValue
			// 
			this.chMinValue.Text = resources.GetString("chMinValue.Text");
			this.chMinValue.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("chMinValue.TextAlign")));
			this.chMinValue.Width = ((int)(resources.GetObject("chMinValue.Width")));
			// 
			// chMaxValue
			// 
			this.chMaxValue.Text = resources.GetString("chMaxValue.Text");
			this.chMaxValue.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("chMaxValue.TextAlign")));
			this.chMaxValue.Width = ((int)(resources.GetObject("chMaxValue.Width")));
			// 
			// tbFilename
			// 
			this.tbFilename.AccessibleDescription = resources.GetString("tbFilename.AccessibleDescription");
			this.tbFilename.AccessibleName = resources.GetString("tbFilename.AccessibleName");
			this.tbFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbFilename.Anchor")));
			this.tbFilename.AutoSize = ((bool)(resources.GetObject("tbFilename.AutoSize")));
			this.tbFilename.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbFilename.BackgroundImage")));
			this.tbFilename.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbFilename.Dock")));
			this.tbFilename.Enabled = ((bool)(resources.GetObject("tbFilename.Enabled")));
			this.tbFilename.Font = ((System.Drawing.Font)(resources.GetObject("tbFilename.Font")));
			this.tbFilename.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbFilename.ImeMode")));
			this.tbFilename.Location = ((System.Drawing.Point)(resources.GetObject("tbFilename.Location")));
			this.tbFilename.MaxLength = ((int)(resources.GetObject("tbFilename.MaxLength")));
			this.tbFilename.Multiline = ((bool)(resources.GetObject("tbFilename.Multiline")));
			this.tbFilename.Name = "tbFilename";
			this.tbFilename.PasswordChar = ((char)(resources.GetObject("tbFilename.PasswordChar")));
			this.tbFilename.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbFilename.RightToLeft")));
			this.tbFilename.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbFilename.ScrollBars")));
			this.tbFilename.Size = ((System.Drawing.Size)(resources.GetObject("tbFilename.Size")));
			this.tbFilename.TabIndex = ((int)(resources.GetObject("tbFilename.TabIndex")));
			this.tbFilename.Text = resources.GetString("tbFilename.Text");
			this.tbFilename.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbFilename.TextAlign")));
			this.tbFilename.Visible = ((bool)(resources.GetObject("tbFilename.Visible")));
			this.tbFilename.WordWrap = ((bool)(resources.GetObject("tbFilename.WordWrap")));
			this.tbFilename.Validated += new System.EventHandler(this.tbText_Validated);
			this.tbFilename.TextChanged += new System.EventHandler(this.tbText_TextChanged);
			// 
			// lbFilename
			// 
			this.lbFilename.AccessibleDescription = resources.GetString("lbFilename.AccessibleDescription");
			this.lbFilename.AccessibleName = resources.GetString("lbFilename.AccessibleName");
			this.lbFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbFilename.Anchor")));
			this.lbFilename.AutoSize = ((bool)(resources.GetObject("lbFilename.AutoSize")));
			this.lbFilename.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbFilename.Dock")));
			this.lbFilename.Enabled = ((bool)(resources.GetObject("lbFilename.Enabled")));
			this.lbFilename.Font = ((System.Drawing.Font)(resources.GetObject("lbFilename.Font")));
			this.lbFilename.Image = ((System.Drawing.Image)(resources.GetObject("lbFilename.Image")));
			this.lbFilename.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFilename.ImageAlign")));
			this.lbFilename.ImageIndex = ((int)(resources.GetObject("lbFilename.ImageIndex")));
			this.lbFilename.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbFilename.ImeMode")));
			this.lbFilename.Location = ((System.Drawing.Point)(resources.GetObject("lbFilename.Location")));
			this.lbFilename.Name = "lbFilename";
			this.lbFilename.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbFilename.RightToLeft")));
			this.lbFilename.Size = ((System.Drawing.Size)(resources.GetObject("lbFilename.Size")));
			this.lbFilename.TabIndex = ((int)(resources.GetObject("lbFilename.TabIndex")));
			this.lbFilename.Text = resources.GetString("lbFilename.Text");
			this.lbFilename.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFilename.TextAlign")));
			this.lbFilename.Visible = ((bool)(resources.GetObject("lbFilename.Visible")));
			// 
			// lbDefValue
			// 
			this.lbDefValue.AccessibleDescription = resources.GetString("lbDefValue.AccessibleDescription");
			this.lbDefValue.AccessibleName = resources.GetString("lbDefValue.AccessibleName");
			this.lbDefValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbDefValue.Anchor")));
			this.lbDefValue.AutoSize = ((bool)(resources.GetObject("lbDefValue.AutoSize")));
			this.lbDefValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbDefValue.Dock")));
			this.lbDefValue.Enabled = ((bool)(resources.GetObject("lbDefValue.Enabled")));
			this.lbDefValue.Font = ((System.Drawing.Font)(resources.GetObject("lbDefValue.Font")));
			this.lbDefValue.Image = ((System.Drawing.Image)(resources.GetObject("lbDefValue.Image")));
			this.lbDefValue.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbDefValue.ImageAlign")));
			this.lbDefValue.ImageIndex = ((int)(resources.GetObject("lbDefValue.ImageIndex")));
			this.lbDefValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbDefValue.ImeMode")));
			this.lbDefValue.Location = ((System.Drawing.Point)(resources.GetObject("lbDefValue.Location")));
			this.lbDefValue.Name = "lbDefValue";
			this.lbDefValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbDefValue.RightToLeft")));
			this.lbDefValue.Size = ((System.Drawing.Size)(resources.GetObject("lbDefValue.Size")));
			this.lbDefValue.TabIndex = ((int)(resources.GetObject("lbDefValue.TabIndex")));
			this.lbDefValue.Text = resources.GetString("lbDefValue.Text");
			this.lbDefValue.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbDefValue.TextAlign")));
			this.lbDefValue.Visible = ((bool)(resources.GetObject("lbDefValue.Visible")));
			// 
			// tbDefValue
			// 
			this.tbDefValue.AccessibleDescription = resources.GetString("tbDefValue.AccessibleDescription");
			this.tbDefValue.AccessibleName = resources.GetString("tbDefValue.AccessibleName");
			this.tbDefValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbDefValue.Anchor")));
			this.tbDefValue.AutoSize = ((bool)(resources.GetObject("tbDefValue.AutoSize")));
			this.tbDefValue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbDefValue.BackgroundImage")));
			this.tbDefValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbDefValue.Dock")));
			this.tbDefValue.Enabled = ((bool)(resources.GetObject("tbDefValue.Enabled")));
			this.tbDefValue.Font = ((System.Drawing.Font)(resources.GetObject("tbDefValue.Font")));
			this.tbDefValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbDefValue.ImeMode")));
			this.tbDefValue.Location = ((System.Drawing.Point)(resources.GetObject("tbDefValue.Location")));
			this.tbDefValue.MaxLength = ((int)(resources.GetObject("tbDefValue.MaxLength")));
			this.tbDefValue.Multiline = ((bool)(resources.GetObject("tbDefValue.Multiline")));
			this.tbDefValue.Name = "tbDefValue";
			this.tbDefValue.PasswordChar = ((char)(resources.GetObject("tbDefValue.PasswordChar")));
			this.tbDefValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbDefValue.RightToLeft")));
			this.tbDefValue.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbDefValue.ScrollBars")));
			this.tbDefValue.Size = ((System.Drawing.Size)(resources.GetObject("tbDefValue.Size")));
			this.tbDefValue.TabIndex = ((int)(resources.GetObject("tbDefValue.TabIndex")));
			this.tbDefValue.Text = resources.GetString("tbDefValue.Text");
			this.tbDefValue.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbDefValue.TextAlign")));
			this.tbDefValue.Visible = ((bool)(resources.GetObject("tbDefValue.Visible")));
			this.tbDefValue.WordWrap = ((bool)(resources.GetObject("tbDefValue.WordWrap")));
			this.tbDefValue.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbDefValue.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbDefValue.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			// 
			// tbMinValue
			// 
			this.tbMinValue.AccessibleDescription = resources.GetString("tbMinValue.AccessibleDescription");
			this.tbMinValue.AccessibleName = resources.GetString("tbMinValue.AccessibleName");
			this.tbMinValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbMinValue.Anchor")));
			this.tbMinValue.AutoSize = ((bool)(resources.GetObject("tbMinValue.AutoSize")));
			this.tbMinValue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbMinValue.BackgroundImage")));
			this.tbMinValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbMinValue.Dock")));
			this.tbMinValue.Enabled = ((bool)(resources.GetObject("tbMinValue.Enabled")));
			this.tbMinValue.Font = ((System.Drawing.Font)(resources.GetObject("tbMinValue.Font")));
			this.tbMinValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbMinValue.ImeMode")));
			this.tbMinValue.Location = ((System.Drawing.Point)(resources.GetObject("tbMinValue.Location")));
			this.tbMinValue.MaxLength = ((int)(resources.GetObject("tbMinValue.MaxLength")));
			this.tbMinValue.Multiline = ((bool)(resources.GetObject("tbMinValue.Multiline")));
			this.tbMinValue.Name = "tbMinValue";
			this.tbMinValue.PasswordChar = ((char)(resources.GetObject("tbMinValue.PasswordChar")));
			this.tbMinValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbMinValue.RightToLeft")));
			this.tbMinValue.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbMinValue.ScrollBars")));
			this.tbMinValue.Size = ((System.Drawing.Size)(resources.GetObject("tbMinValue.Size")));
			this.tbMinValue.TabIndex = ((int)(resources.GetObject("tbMinValue.TabIndex")));
			this.tbMinValue.Text = resources.GetString("tbMinValue.Text");
			this.tbMinValue.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbMinValue.TextAlign")));
			this.tbMinValue.Visible = ((bool)(resources.GetObject("tbMinValue.Visible")));
			this.tbMinValue.WordWrap = ((bool)(resources.GetObject("tbMinValue.WordWrap")));
			this.tbMinValue.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbMinValue.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbMinValue.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			// 
			// lbMinValue
			// 
			this.lbMinValue.AccessibleDescription = resources.GetString("lbMinValue.AccessibleDescription");
			this.lbMinValue.AccessibleName = resources.GetString("lbMinValue.AccessibleName");
			this.lbMinValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbMinValue.Anchor")));
			this.lbMinValue.AutoSize = ((bool)(resources.GetObject("lbMinValue.AutoSize")));
			this.lbMinValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbMinValue.Dock")));
			this.lbMinValue.Enabled = ((bool)(resources.GetObject("lbMinValue.Enabled")));
			this.lbMinValue.Font = ((System.Drawing.Font)(resources.GetObject("lbMinValue.Font")));
			this.lbMinValue.Image = ((System.Drawing.Image)(resources.GetObject("lbMinValue.Image")));
			this.lbMinValue.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMinValue.ImageAlign")));
			this.lbMinValue.ImageIndex = ((int)(resources.GetObject("lbMinValue.ImageIndex")));
			this.lbMinValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbMinValue.ImeMode")));
			this.lbMinValue.Location = ((System.Drawing.Point)(resources.GetObject("lbMinValue.Location")));
			this.lbMinValue.Name = "lbMinValue";
			this.lbMinValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbMinValue.RightToLeft")));
			this.lbMinValue.Size = ((System.Drawing.Size)(resources.GetObject("lbMinValue.Size")));
			this.lbMinValue.TabIndex = ((int)(resources.GetObject("lbMinValue.TabIndex")));
			this.lbMinValue.Text = resources.GetString("lbMinValue.Text");
			this.lbMinValue.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMinValue.TextAlign")));
			this.lbMinValue.Visible = ((bool)(resources.GetObject("lbMinValue.Visible")));
			// 
			// tbMaxValue
			// 
			this.tbMaxValue.AccessibleDescription = resources.GetString("tbMaxValue.AccessibleDescription");
			this.tbMaxValue.AccessibleName = resources.GetString("tbMaxValue.AccessibleName");
			this.tbMaxValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tbMaxValue.Anchor")));
			this.tbMaxValue.AutoSize = ((bool)(resources.GetObject("tbMaxValue.AutoSize")));
			this.tbMaxValue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbMaxValue.BackgroundImage")));
			this.tbMaxValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tbMaxValue.Dock")));
			this.tbMaxValue.Enabled = ((bool)(resources.GetObject("tbMaxValue.Enabled")));
			this.tbMaxValue.Font = ((System.Drawing.Font)(resources.GetObject("tbMaxValue.Font")));
			this.tbMaxValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tbMaxValue.ImeMode")));
			this.tbMaxValue.Location = ((System.Drawing.Point)(resources.GetObject("tbMaxValue.Location")));
			this.tbMaxValue.MaxLength = ((int)(resources.GetObject("tbMaxValue.MaxLength")));
			this.tbMaxValue.Multiline = ((bool)(resources.GetObject("tbMaxValue.Multiline")));
			this.tbMaxValue.Name = "tbMaxValue";
			this.tbMaxValue.PasswordChar = ((char)(resources.GetObject("tbMaxValue.PasswordChar")));
			this.tbMaxValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tbMaxValue.RightToLeft")));
			this.tbMaxValue.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("tbMaxValue.ScrollBars")));
			this.tbMaxValue.Size = ((System.Drawing.Size)(resources.GetObject("tbMaxValue.Size")));
			this.tbMaxValue.TabIndex = ((int)(resources.GetObject("tbMaxValue.TabIndex")));
			this.tbMaxValue.Text = resources.GetString("tbMaxValue.Text");
			this.tbMaxValue.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("tbMaxValue.TextAlign")));
			this.tbMaxValue.Visible = ((bool)(resources.GetObject("tbMaxValue.Visible")));
			this.tbMaxValue.WordWrap = ((bool)(resources.GetObject("tbMaxValue.WordWrap")));
			this.tbMaxValue.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
			this.tbMaxValue.Validated += new System.EventHandler(this.hex16_Validated);
			this.tbMaxValue.TextChanged += new System.EventHandler(this.hex16_TextChanged);
			// 
			// lbMaxValue
			// 
			this.lbMaxValue.AccessibleDescription = resources.GetString("lbMaxValue.AccessibleDescription");
			this.lbMaxValue.AccessibleName = resources.GetString("lbMaxValue.AccessibleName");
			this.lbMaxValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbMaxValue.Anchor")));
			this.lbMaxValue.AutoSize = ((bool)(resources.GetObject("lbMaxValue.AutoSize")));
			this.lbMaxValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbMaxValue.Dock")));
			this.lbMaxValue.Enabled = ((bool)(resources.GetObject("lbMaxValue.Enabled")));
			this.lbMaxValue.Font = ((System.Drawing.Font)(resources.GetObject("lbMaxValue.Font")));
			this.lbMaxValue.Image = ((System.Drawing.Image)(resources.GetObject("lbMaxValue.Image")));
			this.lbMaxValue.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMaxValue.ImageAlign")));
			this.lbMaxValue.ImageIndex = ((int)(resources.GetObject("lbMaxValue.ImageIndex")));
			this.lbMaxValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbMaxValue.ImeMode")));
			this.lbMaxValue.Location = ((System.Drawing.Point)(resources.GetObject("lbMaxValue.Location")));
			this.lbMaxValue.Name = "lbMaxValue";
			this.lbMaxValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbMaxValue.RightToLeft")));
			this.lbMaxValue.Size = ((System.Drawing.Size)(resources.GetObject("lbMaxValue.Size")));
			this.lbMaxValue.TabIndex = ((int)(resources.GetObject("lbMaxValue.TabIndex")));
			this.lbMaxValue.Text = resources.GetString("lbMaxValue.Text");
			this.lbMaxValue.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbMaxValue.TextAlign")));
			this.lbMaxValue.Visible = ((bool)(resources.GetObject("lbMaxValue.Visible")));
			// 
			// lbLabel
			// 
			this.lbLabel.AccessibleDescription = resources.GetString("lbLabel.AccessibleDescription");
			this.lbLabel.AccessibleName = resources.GetString("lbLabel.AccessibleName");
			this.lbLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbLabel.Anchor")));
			this.lbLabel.AutoSize = ((bool)(resources.GetObject("lbLabel.AutoSize")));
			this.lbLabel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbLabel.Dock")));
			this.lbLabel.Enabled = ((bool)(resources.GetObject("lbLabel.Enabled")));
			this.lbLabel.Font = ((System.Drawing.Font)(resources.GetObject("lbLabel.Font")));
			this.lbLabel.Image = ((System.Drawing.Image)(resources.GetObject("lbLabel.Image")));
			this.lbLabel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbLabel.ImageAlign")));
			this.lbLabel.ImageIndex = ((int)(resources.GetObject("lbLabel.ImageIndex")));
			this.lbLabel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbLabel.ImeMode")));
			this.lbLabel.Location = ((System.Drawing.Point)(resources.GetObject("lbLabel.Location")));
			this.lbLabel.Name = "lbLabel";
			this.lbLabel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbLabel.RightToLeft")));
			this.lbLabel.Size = ((System.Drawing.Size)(resources.GetObject("lbLabel.Size")));
			this.lbLabel.TabIndex = ((int)(resources.GetObject("lbLabel.TabIndex")));
			this.lbLabel.Text = resources.GetString("lbLabel.Text");
			this.lbLabel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbLabel.TextAlign")));
			this.lbLabel.Visible = ((bool)(resources.GetObject("lbLabel.Visible")));
			// 
			// label2
			// 
			this.label2.AccessibleDescription = resources.GetString("label2.AccessibleDescription");
			this.label2.AccessibleName = resources.GetString("label2.AccessibleName");
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label2.Anchor")));
			this.label2.AutoSize = ((bool)(resources.GetObject("label2.AutoSize")));
			this.label2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label2.Dock")));
			this.label2.Enabled = ((bool)(resources.GetObject("label2.Enabled")));
			this.label2.Font = ((System.Drawing.Font)(resources.GetObject("label2.Font")));
			this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
			this.label2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.ImageAlign")));
			this.label2.ImageIndex = ((int)(resources.GetObject("label2.ImageIndex")));
			this.label2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label2.ImeMode")));
			this.label2.Location = ((System.Drawing.Point)(resources.GetObject("label2.Location")));
			this.label2.Name = "label2";
			this.label2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label2.RightToLeft")));
			this.label2.Size = ((System.Drawing.Size)(resources.GetObject("label2.Size")));
			this.label2.TabIndex = ((int)(resources.GetObject("label2.TabIndex")));
			this.label2.Text = resources.GetString("label2.Text");
			this.label2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.TextAlign")));
			this.label2.Visible = ((bool)(resources.GetObject("label2.Visible")));
			// 
			// label3
			// 
			this.label3.AccessibleDescription = resources.GetString("label3.AccessibleDescription");
			this.label3.AccessibleName = resources.GetString("label3.AccessibleName");
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label3.Anchor")));
			this.label3.AutoSize = ((bool)(resources.GetObject("label3.AutoSize")));
			this.label3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label3.Dock")));
			this.label3.Enabled = ((bool)(resources.GetObject("label3.Enabled")));
			this.label3.Font = ((System.Drawing.Font)(resources.GetObject("label3.Font")));
			this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
			this.label3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label3.ImageAlign")));
			this.label3.ImageIndex = ((int)(resources.GetObject("label3.ImageIndex")));
			this.label3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label3.ImeMode")));
			this.label3.Location = ((System.Drawing.Point)(resources.GetObject("label3.Location")));
			this.label3.Name = "label3";
			this.label3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label3.RightToLeft")));
			this.label3.Size = ((System.Drawing.Size)(resources.GetObject("label3.Size")));
			this.label3.TabIndex = ((int)(resources.GetObject("label3.TabIndex")));
			this.label3.Text = resources.GetString("label3.Text");
			this.label3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label3.TextAlign")));
			this.label3.Visible = ((bool)(resources.GetObject("label3.Visible")));
			// 
			// label4
			// 
			this.label4.AccessibleDescription = resources.GetString("label4.AccessibleDescription");
			this.label4.AccessibleName = resources.GetString("label4.AccessibleName");
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label4.Anchor")));
			this.label4.AutoSize = ((bool)(resources.GetObject("label4.AutoSize")));
			this.label4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label4.Dock")));
			this.label4.Enabled = ((bool)(resources.GetObject("label4.Enabled")));
			this.label4.Font = ((System.Drawing.Font)(resources.GetObject("label4.Font")));
			this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
			this.label4.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.ImageAlign")));
			this.label4.ImageIndex = ((int)(resources.GetObject("label4.ImageIndex")));
			this.label4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label4.ImeMode")));
			this.label4.Location = ((System.Drawing.Point)(resources.GetObject("label4.Location")));
			this.label4.Name = "label4";
			this.label4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label4.RightToLeft")));
			this.label4.Size = ((System.Drawing.Size)(resources.GetObject("label4.Size")));
			this.label4.TabIndex = ((int)(resources.GetObject("label4.TabIndex")));
			this.label4.Text = resources.GetString("label4.Text");
			this.label4.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.TextAlign")));
			this.label4.Visible = ((bool)(resources.GetObject("label4.Visible")));
			// 
			// TrcnForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.trcnPanel);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "TrcnForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.pnHeading.ResumeLayout(false);
			this.trcnPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private void lvTrcnItem_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (this.internalchg) return;

			internalchg = true;

			if (lvTrcnItem.SelectedIndices.Count > 0 && lvTrcnItem.SelectedIndices[0] >= 0)
			{
				currentItem = wrapper[lvTrcnItem.SelectedIndices[0]];
				origItem = currentItem.Clone();

				this.cbID.Enabled = this.tbLabel.Enabled = this.cbUsed.Enabled
					= this.tbDefValue.Enabled = this.tbMinValue.Enabled = this.tbMaxValue.Enabled
					= this.btnStrDelete.Enabled
					= true;

				if (currentItem.ConstId < this.cbID.Items.Count)
				{
					this.cbID.SelectedIndex = -1;
					this.cbID.SelectedIndex = (int)currentItem.ConstId;
				}
				else
				{
					this.cbID.SelectedIndex = -1;
					this.cbID.Text = "0x" + SimPe.Helper.HexString(currentItem.ConstId);
				}
				this.tbLabel.Text = currentItem.ConstName;
				this.cbUsed.CheckState = currentItem.Used != 0
					? System.Windows.Forms.CheckState.Checked
					: System.Windows.Forms.CheckState.Unchecked;
				this.tbDefValue.Text = "0x" + SimPe.Helper.HexString(currentItem.DefValue);
				this.tbMinValue.Text = "0x" + SimPe.Helper.HexString(currentItem.MinValue);
				this.tbMaxValue.Text = "0x" + SimPe.Helper.HexString(currentItem.MaxValue);
			}
			else
			{
				this.cbID.Enabled = this.tbLabel.Enabled = this.cbUsed.Enabled
					= this.tbDefValue.Enabled = this.tbMinValue.Enabled = this.tbMaxValue.Enabled
					= this.btnStrDelete.Enabled
					= false;

				this.cbID.Text = this.tbLabel.Text
					= this.tbDefValue.Text = this.tbMinValue.Text = this.tbMaxValue.Text
					= "";

				this.cbUsed.CheckState = System.Windows.Forms.CheckState.Indeterminate;
			}
			this.btnCancel.Enabled = false;

			internalchg = false;
		}

		private void btnCommit_Click(object sender, System.EventArgs e)
		{
			try 
			{
				wrapper.SynchronizeUserData();
				btnCommit.Enabled = wrapper.Changed;
				lvTrcnItem_SelectedIndexChanged(null, null);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}			
		}


		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			wrapper[lvTrcnItem.SelectedIndices[0]] = origItem.Clone();
			lvTrcnItem_SelectedIndexChanged(null, null);
		}


		private void btnStrAdd_Click(object sender, System.EventArgs e)
		{
			int i = wrapper.Add((lvTrcnItem.SelectedIndices.Count == 0 || lvTrcnItem.SelectedIndices[0] == -1) ? new TrcnItem(wrapper) : currentItem.Clone());
			if (i < 0) return;

			this.lvTrcnItem.Items.Add(new ListViewItem(TrcnItemToStringArray(wrapper[i])));
			foreach(ListViewItem ti in lvTrcnItem.Items)
				ti.Selected = false;
			lvTrcnItem.Items[i].Selected = true;
		}

		private void btnStrDelete_Click(object sender, System.EventArgs e)
		{
			if (lvTrcnItem.SelectedIndices.Count == 0) return;

			int i = lvTrcnItem.SelectedIndices[0];

			lvTrcnItem.Items.RemoveAt(i);
			wrapper.RemoveAt(i);

			foreach(ListViewItem ti in lvTrcnItem.Items)
				ti.Selected = false;

			if (i >= lvTrcnItem.Items.Count)
				i = lvTrcnItem.Items.Count - 1;
			if (i >= 0)
				lvTrcnItem.Items[i].Selected = true;
		}


		private void cbHex32_Enter(object sender, System.EventArgs e)
		{
			((ComboBox)sender).SelectAll();
		}

		private void cbHex32_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (((ComboBox)sender).Items.IndexOf(((ComboBox)sender).Text) != -1) return;

			if (!cbHex32_IsValid(sender)) return;

			internalchg = true;
			uint val = Convert.ToUInt32(((ComboBox)sender).Text, 16);
			switch (alHex32cb.IndexOf(sender))
			{
				case 0: lvTrcnItem.SelectedItems[0].SubItems[0].Text = "0x" + (currentItem.ConstId = val).ToString("X"); break;
			}
			internalchg = false;
		}

		private void cbHex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (((ComboBox)sender).Items.IndexOf(((ComboBox)sender).Text) != -1) return;

			if (cbHex32_IsValid(sender)) return;

			e.Cancel = true;

			bool origstate = internalchg;
			internalchg = true;
			uint val = 0;
			switch (alHex32cb.IndexOf(sender))
			{
				case 0: val = currentItem.ConstId; break;
			}
			if (val < ((ComboBox)sender).Items.Count)
			{
				((ComboBox)sender).SelectedIndex = (int)val;
			}
			else
			{
				((ComboBox)sender).SelectedIndex = -1;
				((ComboBox)sender).Text = "0x" + Helper.HexString(val);
			}
			internalchg = origstate;

			((ComboBox)sender).SelectAll();
		}

		private void cbHex32_Validated(object sender, System.EventArgs e)
		{
			if (((ComboBox)sender).Items.IndexOf(((ComboBox)sender).Text) != -1) return;

			bool origstate = internalchg;
			internalchg = true;
			uint val = Convert.ToUInt32(((ComboBox)sender).Text, 16);
			switch (alHex32cb.IndexOf(sender))
			{
				case 0:
					if (val < ((ComboBox)sender).Items.Count)
					{
						((ComboBox)sender).SelectedIndex = (int)val;
					}
					else
					{
						((ComboBox)sender).SelectedIndex = -1;
						((ComboBox)sender).Text = "0x" + Helper.HexString(val);
					}
					break;
			}
			internalchg = origstate;

			((ComboBox)sender).Select(0, 0);
		}

		private void cbHex32_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			if (((ComboBox)sender).SelectedIndex == -1) return;

			internalchg = true;
			ushort val = (ushort)((ComboBox)sender).SelectedIndex;
			switch (alHex32cb.IndexOf(sender))
			{
				case 0: lvTrcnItem.SelectedItems[0].SubItems[0].Text = "0x" + (currentItem.ConstId = val).ToString("X"); break;
			}
			internalchg = false;
			((ComboBox)sender).SelectAll();
		}


		private void tbText_TextChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			internalchg = true;
			switch(alText.IndexOf(sender))
			{
				case 0: wrapper.FileName = ((TextBox)sender).Text; break;
				case 1: lvTrcnItem.SelectedItems[0].SubItems[1].Text = currentItem.ConstName = ((TextBox)sender).Text; break;
			}
			internalchg = false;
		}

		private void tbText_Validated(object sender, System.EventArgs e)
		{
			((TextBox)sender).SelectAll();
		}


		private void cbUsed_CheckedChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			internalchg = true;
			lvTrcnItem.SelectedItems[0].SubItems[2].Text = "0x" + (currentItem.Used = (uint)(((CheckBox)sender).Checked ? 1 : 0)).ToString("X");
			internalchg = false;
		}


		private void hex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;

			if (!hex16_IsValid(sender)) return;

			internalchg = true;
			ushort val = Convert.ToUInt16(((TextBox)sender).Text, 16);
			switch (alHex16.IndexOf(sender))
			{
				case 0: lvTrcnItem.SelectedItems[0].SubItems[3].Text = "0x" + SimPe.Helper.HexString(currentItem.DefValue = val); break;
				case 1: lvTrcnItem.SelectedItems[0].SubItems[4].Text = "0x" + SimPe.Helper.HexString(currentItem.MinValue = val); break;
				case 2: lvTrcnItem.SelectedItems[0].SubItems[5].Text = "0x" + SimPe.Helper.HexString(currentItem.MaxValue = val); break;
			}
			internalchg = false;
		}

		private void hex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex16_IsValid(sender)) return;

			e.Cancel = true;

			bool origstate = internalchg;
			internalchg = true;
			ushort val = 0;
			switch (alHex16.IndexOf(sender))
			{
				case 0: val = currentItem.DefValue; break;
				case 1: val = currentItem.MinValue; break;
				case 2: val = currentItem.MaxValue; break;
			}
			((TextBox)sender).Text = "0x" + Helper.HexString(val);
			internalchg = origstate;

			((TextBox)sender).SelectAll();
		}

		private void hex16_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + Helper.HexString(Convert.ToUInt16(((TextBox)sender).Text, 16));
			internalchg = origstate;
			((TextBox)sender).SelectAll();
		}


		private void hex32_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;

			if (!hex32_IsValid(sender)) return;

			internalchg = true;
			uint val = Convert.ToUInt32(((TextBox)sender).Text, 16);
			switch (alHex32.IndexOf(sender))
			{
				case 0: wrapper.Version = val; break;
			}
			internalchg = false;
		}

		private void hex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex32_IsValid(sender)) return;

			e.Cancel = true;

			bool origstate = internalchg;
			internalchg = true;
			uint val = 0;
			switch (alHex32.IndexOf(sender))
			{
				case 0: val = wrapper.Version; break;
			}
			((TextBox)sender).Text = "0x" + Helper.HexString(val);
			internalchg = origstate;

			((TextBox)sender).SelectAll();
		}

		private void hex32_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + Helper.HexString(Convert.ToUInt32(((TextBox)sender).Text, 16));
			internalchg = origstate;
			((TextBox)sender).SelectAll();
		}


	}
}
