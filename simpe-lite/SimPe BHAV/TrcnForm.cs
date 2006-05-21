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
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.ColumnHeader chValue;
		private System.Windows.Forms.TextBox tbID;
		private System.Windows.Forms.ColumnHeader chLine;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnStrPrev;
		private System.Windows.Forms.Button btnStrNext;
		private System.Windows.Forms.Button btnHelp;
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

			TextBox[] dw = { tbFormat ,tbID ,};
			alHex32 = new ArrayList(dw);
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


		#region Controller
		private Trcn wrapper = null;
		private bool setHandler = false;
		private bool internalchg = false;

		private ArrayList alText = null;
		private ArrayList alHex16 = null;
		private ArrayList alHex32 = null;

		private int index = -1;
		private TrcnItem origItem = null;
		private TrcnItem currentItem = null;

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


		private void updateSelectedItem()
		{
			ListViewItem lv = this.lvTrcnItem.SelectedItems[0];
			if (lv == null) return;

			//lv.SubItems[0].Text = "0x" + i.ToString("X"); // Line number doesn't change
			//lv.SubItems[1].Text = tiValue; // BCON item value doesn't change
			//lv.SubItems[2].Text = currentItem.ConstName; // Handled elsewhere
			lv.SubItems[3].Text = "0x" + SimPe.Helper.HexString(currentItem.ConstId);
			lv.SubItems[4].Text = "0x" + currentItem.Used.ToString("X");
			lv.SubItems[5].Text = "0x" + SimPe.Helper.HexString(currentItem.DefValue);
			lv.SubItems[6].Text = "0x" + SimPe.Helper.HexString(currentItem.MinValue);
			lv.SubItems[7].Text = "0x" + SimPe.Helper.HexString(currentItem.MaxValue);
		}

		private string[] trcnItemToStringArray(int i)
		{
			if (i < 0 || i >= wrapper.Count) return new string[] { "", "", "", "", "", "", "", "" };

			TrcnItem ti = wrapper[i];
			Bcon bcon = wrapper.BconResource;
			string tiValue = (bcon != null && i < bcon.Count) ? "0x" + SimPe.Helper.HexString(bcon[i]) : "?";

			return new string[] {
									"0x" + i.ToString("X")
									, tiValue
									, ti.ConstName
									, "0x" + SimPe.Helper.HexString(ti.ConstId)
									, "0x" + ti.Used.ToString("X")
									, "0x" + SimPe.Helper.HexString(ti.DefValue)
									, "0x" + SimPe.Helper.HexString(ti.MinValue)
									, "0x" + SimPe.Helper.HexString(ti.MaxValue)
								};

		}

		private void updateLists()
		{
			wrapper.CleanUp();

			index = -1;

			this.lvTrcnItem.Items.Clear();
			int nItems = wrapper.Count;
			for(int i = 0; i < nItems; i++)
				this.lvTrcnItem.Items.Add(new ListViewItem(trcnItemToStringArray(i)));
		}


		private void setIndex(int i)
		{
			internalchg = true;
			if (i >= 0) this.lvTrcnItem.Items[i].Selected = true;
			else if (index >= 0) this.lvTrcnItem.Items[index].Selected = false;
			internalchg = false;

			if (this.lvTrcnItem.SelectedItems.Count > 0)
			{
				if (this.lvTrcnItem.Focused) this.lvTrcnItem.SelectedItems[0].Focused = true;
				this.lvTrcnItem.SelectedItems[0].EnsureVisible();
			}
			else
			{
				internalchg = true;
				this.tbLabel.Text = "";
				this.tbID.Text = "";
				this.cbUsed.CheckState = System.Windows.Forms.CheckState.Indeterminate;
				this.tbDefValue.Text = "";
				this.tbMinValue.Text = "";
				this.tbMaxValue.Text = "";
				this.btnCancel.Enabled = false;
				internalchg = false;
			}

			if (index == i) return;
			index = i;
			displayTrcnItem();
		}


		private void displayTrcnItem()
		{
			currentItem = (index < 0) ? null : wrapper[index];

			internalchg = true;
			if (currentItem != null)
			{
				origItem = currentItem.Clone();

				string[] s = trcnItemToStringArray(index);
				this.tbLabel.Text = s[2];
				this.tbID.Text = s[3];
				this.cbUsed.CheckState = currentItem.Used != 0
					? System.Windows.Forms.CheckState.Checked
					: System.Windows.Forms.CheckState.Unchecked;
				this.tbDefValue.Text = s[5];
				this.tbMinValue.Text = s[6];
				this.tbMaxValue.Text = s[7];

				this.tbID.Enabled = this.tbLabel.Enabled = this.cbUsed.Enabled
					= this.tbDefValue.Enabled = this.tbMinValue.Enabled = this.tbMaxValue.Enabled
					= this.btnStrDelete.Enabled
					= true;
			}
			else
			{
				origItem = null;

				this.tbID.Text = this.tbLabel.Text
					= this.tbDefValue.Text = this.tbMinValue.Text = this.tbMaxValue.Text
					= "";
				this.cbUsed.CheckState = System.Windows.Forms.CheckState.Indeterminate;

				this.tbID.Enabled = this.tbLabel.Enabled = this.cbUsed.Enabled
					= this.tbDefValue.Enabled = this.tbMinValue.Enabled = this.tbMaxValue.Enabled
					= this.btnStrDelete.Enabled
					= false;
			}
			this.btnStrPrev.Enabled = (index > 0);
			this.btnStrNext.Enabled = (index < lvTrcnItem.Items.Count - 1);
			internalchg = false;

			this.btnCancel.Enabled = false;
		}


		private void TrcnItemAdd()
		{
			bool savedstate = internalchg;
			internalchg = true;

			if (wrapper.Add(new TrcnItem(wrapper)) >= 0)
				this.lvTrcnItem.Items.Add(new ListViewItem(trcnItemToStringArray(wrapper.Count - 1)));

			internalchg = savedstate;

			setIndex(lvTrcnItem.Items.Count - 1);
		}

		private void TrcnItemDelete()
		{
			bool savedstate = internalchg;
			internalchg = true;

			wrapper.Remove(currentItem);
			int i = index;
			updateLists();

			internalchg = savedstate;

			setIndex((i >= lvTrcnItem.Items.Count) ? lvTrcnItem.Items.Count - 1 : i);
		}

		private void Commit()
		{
			bool savedstate = internalchg;
			internalchg = true;

			try 
			{
				wrapper.SynchronizeUserData();
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(pjse.Localization.GetString("errwritingfile"), ex);
			}

			btnCommit.Enabled = wrapper.Changed;

			int i = index;
			updateLists();

			internalchg = savedstate;

			setIndex((i >= lvTrcnItem.Items.Count) ? lvTrcnItem.Items.Count - 1 : i);
		}

		private void Cancel()
		{
			bool savedstate = internalchg;
			internalchg = true;

			this.lvTrcnItem.SelectedItems[0].SubItems[2].Text = currentItem.ConstName = origItem.ConstName;
			currentItem.ConstId = origItem.ConstId;
			currentItem.Used = origItem.Used;
			currentItem.DefValue = origItem.DefValue;
			currentItem.MaxValue = origItem.MaxValue;
			currentItem.MinValue = origItem.MinValue;
			updateSelectedItem();

			internalchg = savedstate;

			displayTrcnItem();
		}

		#endregion

		#region IPackedFileUI Member
		/// <summary>
		/// Returns the Control that will be displayed within SimPe
		/// </summary>
		public Control GUIHandle
		{
			get
			{
				return trcnPanel;
			}
		}

		/// <summary>
		/// Called by the AbstractWrapper when the file should be displayed to the user.
		/// </summary>
		/// <param name="wrp">Reference to the wrapper to be displayed.</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (Trcn)wrp;
			WrapperChanged(wrapper, null);

			internalchg = true;
			updateLists();
			internalchg = false;

			setIndex(lvTrcnItem.Items.Count > 0 ? 0 : -1);

			if (!setHandler)
			{
				wrapper.WrapperChanged += new System.EventHandler(this.WrapperChanged);
				setHandler = true;
			}
		}

		private void WrapperChanged(object sender, System.EventArgs e)
		{
			this.btnCommit.Enabled = wrapper.Changed;
			if (sender.Equals(currentItem))
				this.btnCancel.Enabled = true;

			if (internalchg) return;

			if (sender.Equals(wrapper))
			{
				internalchg = true;
				this.Text = tbFilename.Text = wrapper.FileName;
				this.tbFormat.Text = "0x" + SimPe.Helper.HexString(wrapper.Version);
				internalchg = false;
			}
			else if (!sender.Equals(currentItem))
				updateLists();
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrcnForm));
            this.btnCommit = new System.Windows.Forms.Button();
            this.pnHeading = new System.Windows.Forms.Panel();
            this.btnHelp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.trcnPanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbID = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbUsed = new System.Windows.Forms.CheckBox();
            this.tbLabel = new System.Windows.Forms.TextBox();
            this.lbID = new System.Windows.Forms.Label();
            this.btnStrDelete = new System.Windows.Forms.Button();
            this.btnStrAdd = new System.Windows.Forms.Button();
            this.lbFormat = new System.Windows.Forms.Label();
            this.tbFormat = new System.Windows.Forms.TextBox();
            this.lvTrcnItem = new System.Windows.Forms.ListView();
            this.chLine = new System.Windows.Forms.ColumnHeader();
            this.chValue = new System.Windows.Forms.ColumnHeader();
            this.chConstName = new System.Windows.Forms.ColumnHeader();
            this.chConstId = new System.Windows.Forms.ColumnHeader();
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
            this.btnStrPrev = new System.Windows.Forms.Button();
            this.btnStrNext = new System.Windows.Forms.Button();
            this.pnHeading.SuspendLayout();
            this.trcnPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCommit
            // 
            resources.ApplyResources(this.btnCommit, "btnCommit");
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // pnHeading
            // 
            resources.ApplyResources(this.pnHeading, "pnHeading");
            this.pnHeading.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnHeading.Controls.Add(this.btnHelp);
            this.pnHeading.Controls.Add(this.label1);
            this.pnHeading.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnHeading.Name = "pnHeading";
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Name = "label1";
            // 
            // trcnPanel
            // 
            resources.ApplyResources(this.trcnPanel, "trcnPanel");
            this.trcnPanel.BackColor = System.Drawing.SystemColors.Control;
            this.trcnPanel.Controls.Add(this.label5);
            this.trcnPanel.Controls.Add(this.panel1);
            this.trcnPanel.Controls.Add(this.tbID);
            this.trcnPanel.Controls.Add(this.btnCancel);
            this.trcnPanel.Controls.Add(this.cbUsed);
            this.trcnPanel.Controls.Add(this.tbLabel);
            this.trcnPanel.Controls.Add(this.lbID);
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
            this.trcnPanel.Controls.Add(this.btnStrPrev);
            this.trcnPanel.Controls.Add(this.btnStrNext);
            this.trcnPanel.Name = "trcnPanel";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // tbID
            // 
            resources.ApplyResources(this.tbID, "tbID");
            this.tbID.Name = "tbID";
            this.tbID.Enter += new System.EventHandler(this.tbText_Enter);
            this.tbID.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbID.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            this.tbID.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbUsed
            // 
            resources.ApplyResources(this.cbUsed, "cbUsed");
            this.cbUsed.Name = "cbUsed";
            this.cbUsed.CheckedChanged += new System.EventHandler(this.cbUsed_CheckedChanged);
            // 
            // tbLabel
            // 
            resources.ApplyResources(this.tbLabel, "tbLabel");
            this.tbLabel.Name = "tbLabel";
            this.tbLabel.Enter += new System.EventHandler(this.tbText_Enter);
            this.tbLabel.TextChanged += new System.EventHandler(this.tbText_TextChanged);
            // 
            // lbID
            // 
            resources.ApplyResources(this.lbID, "lbID");
            this.lbID.Name = "lbID";
            // 
            // btnStrDelete
            // 
            resources.ApplyResources(this.btnStrDelete, "btnStrDelete");
            this.btnStrDelete.Name = "btnStrDelete";
            this.btnStrDelete.Click += new System.EventHandler(this.btnStrDelete_Click);
            // 
            // btnStrAdd
            // 
            resources.ApplyResources(this.btnStrAdd, "btnStrAdd");
            this.btnStrAdd.Name = "btnStrAdd";
            this.btnStrAdd.Click += new System.EventHandler(this.btnStrAdd_Click);
            // 
            // lbFormat
            // 
            resources.ApplyResources(this.lbFormat, "lbFormat");
            this.lbFormat.Name = "lbFormat";
            // 
            // tbFormat
            // 
            resources.ApplyResources(this.tbFormat, "tbFormat");
            this.tbFormat.Name = "tbFormat";
            this.tbFormat.Enter += new System.EventHandler(this.tbText_Enter);
            this.tbFormat.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbFormat.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            this.tbFormat.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            // 
            // lvTrcnItem
            // 
            resources.ApplyResources(this.lvTrcnItem, "lvTrcnItem");
            this.lvTrcnItem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chLine,
            this.chValue,
            this.chConstName,
            this.chConstId,
            this.chUsed,
            this.chDefValue,
            this.chMinValue,
            this.chMaxValue});
            this.lvTrcnItem.FullRowSelect = true;
            this.lvTrcnItem.GridLines = true;
            this.lvTrcnItem.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTrcnItem.HideSelection = false;
            this.lvTrcnItem.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("lvTrcnItem.Items")))});
            this.lvTrcnItem.MultiSelect = false;
            this.lvTrcnItem.Name = "lvTrcnItem";
            this.lvTrcnItem.UseCompatibleStateImageBehavior = false;
            this.lvTrcnItem.View = System.Windows.Forms.View.Details;
            this.lvTrcnItem.SelectedIndexChanged += new System.EventHandler(this.lvTrcnItem_SelectedIndexChanged);
            // 
            // chLine
            // 
            resources.ApplyResources(this.chLine, "chLine");
            // 
            // chValue
            // 
            resources.ApplyResources(this.chValue, "chValue");
            // 
            // chConstName
            // 
            resources.ApplyResources(this.chConstName, "chConstName");
            // 
            // chConstId
            // 
            resources.ApplyResources(this.chConstId, "chConstId");
            // 
            // chUsed
            // 
            resources.ApplyResources(this.chUsed, "chUsed");
            // 
            // chDefValue
            // 
            resources.ApplyResources(this.chDefValue, "chDefValue");
            // 
            // chMinValue
            // 
            resources.ApplyResources(this.chMinValue, "chMinValue");
            // 
            // chMaxValue
            // 
            resources.ApplyResources(this.chMaxValue, "chMaxValue");
            // 
            // tbFilename
            // 
            resources.ApplyResources(this.tbFilename, "tbFilename");
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.Enter += new System.EventHandler(this.tbText_Enter);
            this.tbFilename.TextChanged += new System.EventHandler(this.tbText_TextChanged);
            // 
            // lbFilename
            // 
            resources.ApplyResources(this.lbFilename, "lbFilename");
            this.lbFilename.Name = "lbFilename";
            // 
            // lbDefValue
            // 
            resources.ApplyResources(this.lbDefValue, "lbDefValue");
            this.lbDefValue.Name = "lbDefValue";
            // 
            // tbDefValue
            // 
            resources.ApplyResources(this.tbDefValue, "tbDefValue");
            this.tbDefValue.Name = "tbDefValue";
            this.tbDefValue.Enter += new System.EventHandler(this.tbText_Enter);
            this.tbDefValue.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbDefValue.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            this.tbDefValue.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            // 
            // tbMinValue
            // 
            resources.ApplyResources(this.tbMinValue, "tbMinValue");
            this.tbMinValue.Name = "tbMinValue";
            this.tbMinValue.Enter += new System.EventHandler(this.tbText_Enter);
            this.tbMinValue.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbMinValue.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            this.tbMinValue.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            // 
            // lbMinValue
            // 
            resources.ApplyResources(this.lbMinValue, "lbMinValue");
            this.lbMinValue.Name = "lbMinValue";
            // 
            // tbMaxValue
            // 
            resources.ApplyResources(this.tbMaxValue, "tbMaxValue");
            this.tbMaxValue.Name = "tbMaxValue";
            this.tbMaxValue.Enter += new System.EventHandler(this.tbText_Enter);
            this.tbMaxValue.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbMaxValue.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            this.tbMaxValue.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            // 
            // lbMaxValue
            // 
            resources.ApplyResources(this.lbMaxValue, "lbMaxValue");
            this.lbMaxValue.Name = "lbMaxValue";
            // 
            // lbLabel
            // 
            resources.ApplyResources(this.lbLabel, "lbLabel");
            this.lbLabel.Name = "lbLabel";
            // 
            // btnStrPrev
            // 
            resources.ApplyResources(this.btnStrPrev, "btnStrPrev");
            this.btnStrPrev.Name = "btnStrPrev";
            this.btnStrPrev.TabStop = false;
            this.btnStrPrev.Click += new System.EventHandler(this.btnStrPrev_Click);
            // 
            // btnStrNext
            // 
            resources.ApplyResources(this.btnStrNext, "btnStrNext");
            this.btnStrNext.Name = "btnStrNext";
            this.btnStrNext.TabStop = false;
            this.btnStrNext.Click += new System.EventHandler(this.btnStrNext_Click);
            // 
            // TrcnForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.trcnPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "TrcnForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnHeading.ResumeLayout(false);
            this.pnHeading.PerformLayout();
            this.trcnPanel.ResumeLayout(false);
            this.trcnPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private void lvTrcnItem_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			setIndex((this.lvTrcnItem.SelectedIndices.Count > 0) ? this.lvTrcnItem.SelectedIndices[0] : -1);
		}


		private void btnCommit_Click(object sender, System.EventArgs e)
		{
			this.Commit();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Cancel();
			this.tbLabel.SelectAll();
			this.tbLabel.Focus();
		}


		private void btnStrPrev_Click(object sender, System.EventArgs e)
		{
			this.setIndex(index - 1);
		}

		private void btnStrNext_Click(object sender, System.EventArgs e)
		{
			this.setIndex(index + 1);
		}

		private void btnStrAdd_Click(object sender, System.EventArgs e)
		{
			this.TrcnItemAdd();
			this.tbLabel.SelectAll();
			this.tbLabel.Focus();
		}

		private void btnStrDelete_Click(object sender, System.EventArgs e)
		{
			this.TrcnItemDelete();
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			pjse.HelpHelper.Help("Constants");
		}


		private void tbText_Enter(object sender, System.EventArgs e)
		{
			((TextBox)sender).SelectAll();
		}

		private void tbText_TextChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			internalchg = true;
			switch(alText.IndexOf(sender))
			{
				case 0: wrapper.FileName = ((TextBox)sender).Text; break;
				case 1: lvTrcnItem.SelectedItems[0].SubItems[2].Text = currentItem.ConstName = ((TextBox)sender).Text; break;
			}
			internalchg = false;
		}


		private void cbUsed_CheckedChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			currentItem.Used = (uint)(((CheckBox)sender).Checked ? 1 : 0);
			updateSelectedItem();
		}


		private void hex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex16_IsValid(sender)) return;

			internalchg = true;
			ushort val = Convert.ToUInt16(((TextBox)sender).Text, 16);
			switch(alHex16.IndexOf(sender))
			{
				case 0: currentItem.DefValue = val; updateSelectedItem(); break;
				case 1: currentItem.MinValue = val; updateSelectedItem(); break;
				case 2: currentItem.MaxValue = val; updateSelectedItem(); break;
			}
			internalchg = false;
		}

		private void hex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex16_IsValid(sender)) return;
			e.Cancel = true;
			hex16_Validated(sender, null);
		}

		private void hex16_Validated(object sender, System.EventArgs e)
		{
			ushort val = 0;
			switch(alHex16.IndexOf(sender))
			{
				case 0: val = currentItem.DefValue; break;
				case 1: val = currentItem.MinValue; break;
				case 2: val = currentItem.MaxValue; break;
			}

			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + Helper.HexString(val);
			internalchg = origstate;
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
				case 1: currentItem.ConstId = val; updateSelectedItem(); break;
			}
			internalchg = false;
		}

		private void hex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex32_IsValid(sender)) return;
			e.Cancel = true;
			hex32_Validated(sender, null);
		}

		private void hex32_Validated(object sender, System.EventArgs e)
		{
			uint val = 0;
			switch (alHex32.IndexOf(sender))
			{
				case 0: val = wrapper.Version; break;
				case 1: val = currentItem.ConstId;break;
			}

			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + Helper.HexString(val);
			internalchg = origstate;
		}

	}
}
