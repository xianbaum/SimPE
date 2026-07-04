/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   pljones@users.sf.net                                                  *
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
	/// Summary description for TrcnForm.
	/// </summary>
	public class TrcnForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables

        private System.Windows.Forms.Panel trcnPanel;
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
        private TextBox tbDesc;
        private Label lbDesc;
        private pjse.pjse_banner pjse_banner1;
        private TableLayoutPanel tlpUnused;
        private Panel panel2;
        private Button btSetAll;
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
            this.lvTrcnItem.Items.Clear();

            TextBox[] t = { tbFilename, tbLabel, };
			alText = new ArrayList(t);

			TextBox[] w = { tbDefValue ,tbMinValue ,tbMaxValue ,};
			alHex16 = new ArrayList(w);

			TextBox[] dw = { tbFormat ,tbID ,};
			alHex32 = new ArrayList(dw);

            pjse.FileTable.GFT.FiletableRefresh += new EventHandler(this.FiletableRefresh);
            if (SimPe.Helper.WindowsRegistry.UseBigIcons)
            {
                this.lvTrcnItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
                this.chUsed.Width = 48;
                this.chDefValue.Width = 72;
                this.chMinValue.Width = 72;
                this.chMaxValue.Width = 78;
                this.chLine.Width = 84;
            }
            if (booby.ThemeManager.ThemedForms)
            {
                booby.ThemeManager.Global.AddControl(this.btSetAll);
                booby.ThemeManager.Global.AddControl(this.btnCommit);
                this.trcnPanel.BackColor = booby.ThemeManager.Global.ThemeColorLight;
                this.lvTrcnItem.BackColor = booby.ThemeManager.Global.ThemeColorLighter;
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
            if (setHandler && wrapper != null)
            {
                wrapper.WrapperChanged -= new System.EventHandler(this.WrapperChanged);
                setHandler = false;
            }
            wrapper = null;
            bconres = null;
        }


		#region Controller
		private Trcn wrapper = null;
        private Bcon bconres = null;
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

        private void doTextOnly()
        {
            trcnPanel.SuspendLayout();
            trcnPanel.Controls.Clear();
            trcnPanel.Controls.Add(this.pjse_banner1);
            trcnPanel.Controls.Add(this.lbFilename);
            tbFilename.ReadOnly = true;
            tbFilename.Text = wrapper.FileName;
            tbFormat.Text = SimPe.Helper.HexString(wrapper.Version);
            trcnPanel.Controls.Add(this.tbFilename);
            trcnPanel.Controls.Add(this.lbFormat);
            trcnPanel.Controls.Add(this.tbFormat);

            Label lb = new Label();
            lb.AutoSize = true;
            lb.Location = new Point(0, tbFormat.Bottom + 6);
            lb.Text = pjse.Localization.GetString("trcnTextOnly");

            TextBox tb = new TextBox();
            tb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tb.Multiline = true;
            tb.Location = new Point(0, lb.Bottom + 6);
            tb.ReadOnly = true;
            tb.ScrollBars = ScrollBars.Both;
            tb.Size = trcnPanel.Size;
            tb.Height -= tb.Top;

            tb.Text = getText(wrapper.StoredData);

            trcnPanel.Controls.Add(lb);
            trcnPanel.Controls.Add(tb);
            trcnPanel.ResumeLayout(true);
        }

        private string getText(System.IO.BinaryReader br)
        {
            br.BaseStream.Seek(0x50, System.IO.SeekOrigin.Begin); // Skip filename, header and item count
            string s = "";
            bool hadNL = true;
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                byte b = br.ReadByte();
                if (b < 0x20 || b > 0x7e)
                {
                    if (!hadNL)
                    {
                        s += "\r\n";
                        hadNL = true;
                    }
                }
                else
                {
                    s += Convert.ToChar(b);
                    hadNL = false;
                }
            }
            return s;
        }


		private void updateSelectedItem()
		{
			ListViewItem lv = this.lvTrcnItem.SelectedItems[0];
			if (lv == null) return;

			lv.SubItems[3].Text = "0x" + SimPe.Helper.HexString(currentItem.ConstId);
			lv.SubItems[4].Text = "0x" + currentItem.Used.ToString("X");
            if (wrapper.Version > 0x53) lv.SubItems[5].Text = "0x" + SimPe.Helper.HexString((byte)currentItem.DefValue);
			else lv.SubItems[5].Text = "0x" + SimPe.Helper.HexString(currentItem.DefValue);
			lv.SubItems[6].Text = "0x" + SimPe.Helper.HexString(currentItem.MinValue);
			lv.SubItems[7].Text = "0x" + SimPe.Helper.HexString(currentItem.MaxValue);
		}

		private string[] trcnItemToStringArray(int i)
		{
			if (i < 0 || i >= wrapper.Count) return new string[] { "", "", "", "", "", "", "", "" };

			TrcnItem ti = wrapper[i];
            string tiValue = (bconres != null && i < bconres.Count) ? "0x" + SimPe.Helper.HexString(bconres[i]) : "?";

			return new string[] {
									"0x" + i.ToString("X") + " (" + i + ")"
									, tiValue
									, ti.ConstName
									, "0x" + SimPe.Helper.HexString(ti.ConstId & (wrapper.Version == 0x3f ? 0x000f : 0xffffffff))
									, "0x" + ti.Used.ToString("X")
									, "0x" + (wrapper.Version > 0x53 ? SimPe.Helper.HexString((byte)ti.DefValue) : SimPe.Helper.HexString(ti.DefValue))
									, "0x" + SimPe.Helper.HexString(ti.MinValue)
									, "0x" + SimPe.Helper.HexString(ti.MaxValue)
								};

		}

        private void updateLists()
		{
            if (wrapper != null) wrapper.CleanUp();

			index = -1;
            bconres = (Bcon)(wrapper == null ? null : wrapper.SiblingResource(Bcon.Bcontype));

			this.lvTrcnItem.Items.Clear();
            int nItems = wrapper == null ? 0 : wrapper.Count;
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
                this.tbDesc.Text = "";
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
                this.tbDesc.Text = currentItem.ConstDesc;
				this.tbDefValue.Text = s[5];
				this.tbMinValue.Text = s[6];
				this.tbMaxValue.Text = s[7];

                this.tbID.Enabled = this.tbLabel.Enabled
                    = this.tbDefValue.Enabled = this.tbMinValue.Enabled = this.tbMaxValue.Enabled
                    = this.btnStrDelete.Enabled
                    = true;
                this.cbUsed.Enabled = (wrapper.Version > 0x3e);
                this.tbDefValue.Enabled = this.tbID.Enabled = this.tbMinValue.Enabled = this.tbMaxValue.Enabled = (wrapper.Version > 1);
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

            try
            {
                wrapper.Add(new TrcnItem(wrapper));
                this.lvTrcnItem.Items.Add(new ListViewItem(trcnItemToStringArray(wrapper.Count - 1)));
            }
            catch { }

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
            if (tbFormat.Text == "0x00000001") tbFormat.Text = "0x" + SimPe.Helper.HexString(wrapper.Version);

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

        void FiletableRefresh(object sender, EventArgs e)
        {
            pjse_banner1.SiblingEnabled = wrapper != null && wrapper.SiblingResource(Bcon.Bcontype) != null;
            updateLists();
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
            pjse_banner1.SiblingEnabled = wrapper.SiblingResource(Bcon.Bcontype) != null;

			internalchg = true;
			updateLists();
			internalchg = false;

			setIndex(lvTrcnItem.Items.Count > 0 ? 0 : -1);

			if (!setHandler)
			{
				wrapper.WrapperChanged += new System.EventHandler(this.WrapperChanged);
				setHandler = true;
			}
            this.btSetAll.Visible = (wrapper.Version != 1 && (booby.PrettyGirls.IsTitsInstalled() || booby.PrettyGirls.IsAngelsInstalled()));
		}

		private void WrapperChanged(object sender, System.EventArgs e)
		{
            if (wrapper.TextOnly)
            {
                doTextOnly();
                return;
            }

            this.tbDesc.ReadOnly = (wrapper.Version <= 0x53);
            this.btnCommit.Enabled = (wrapper.Changed || wrapper.Version == 1); 
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "0xDD (222)", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 7.8F)),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "0xDDDD"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "0xDDDDDDDD", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Verdana", 8.25F)),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "0xDD"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "0xDDDD"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "0xDDDD"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "0xDDDD")}, -1);
            this.btnCommit = new System.Windows.Forms.Button();
            this.trcnPanel = new System.Windows.Forms.Panel();
            this.btSetAll = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnStrAdd = new System.Windows.Forms.Button();
            this.lbLabel = new System.Windows.Forms.Label();
            this.btnStrDelete = new System.Windows.Forms.Button();
            this.tbLabel = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnStrPrev = new System.Windows.Forms.Button();
            this.btnStrNext = new System.Windows.Forms.Button();
            this.tlpUnused = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.lbID = new System.Windows.Forms.Label();
            this.tbID = new System.Windows.Forms.TextBox();
            this.lbDesc = new System.Windows.Forms.Label();
            this.tbDesc = new System.Windows.Forms.TextBox();
            this.lbDefValue = new System.Windows.Forms.Label();
            this.tbDefValue = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbMinValue = new System.Windows.Forms.Label();
            this.tbMinValue = new System.Windows.Forms.TextBox();
            this.lbMaxValue = new System.Windows.Forms.Label();
            this.tbMaxValue = new System.Windows.Forms.TextBox();
            this.cbUsed = new System.Windows.Forms.CheckBox();
            this.tbFormat = new System.Windows.Forms.TextBox();
            this.lbFormat = new System.Windows.Forms.Label();
            this.tbFilename = new System.Windows.Forms.TextBox();
            this.lbFilename = new System.Windows.Forms.Label();
            this.pjse_banner1 = new pjse.pjse_banner();
            this.lvTrcnItem = new System.Windows.Forms.ListView();
            this.chLine = new System.Windows.Forms.ColumnHeader();
            this.chValue = new System.Windows.Forms.ColumnHeader();
            this.chConstName = new System.Windows.Forms.ColumnHeader();
            this.chConstId = new System.Windows.Forms.ColumnHeader();
            this.chUsed = new System.Windows.Forms.ColumnHeader();
            this.chDefValue = new System.Windows.Forms.ColumnHeader();
            this.chMinValue = new System.Windows.Forms.ColumnHeader();
            this.chMaxValue = new System.Windows.Forms.ColumnHeader();
            this.trcnPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tlpUnused.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCommit
            // 
            this.btnCommit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCommit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCommit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCommit.Location = new System.Drawing.Point(775, 32);
            this.btnCommit.Margin = new System.Windows.Forms.Padding(2);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(78, 22);
            this.btnCommit.TabIndex = 23;
            this.btnCommit.Text = "Commit &File";
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // trcnPanel
            // 
            this.trcnPanel.AutoScroll = true;
            this.trcnPanel.Controls.Add(this.btSetAll);
            this.trcnPanel.Controls.Add(this.panel2);
            this.trcnPanel.Controls.Add(this.tlpUnused);
            this.trcnPanel.Controls.Add(this.btnCommit);
            this.trcnPanel.Controls.Add(this.tbFormat);
            this.trcnPanel.Controls.Add(this.lbFormat);
            this.trcnPanel.Controls.Add(this.tbFilename);
            this.trcnPanel.Controls.Add(this.lbFilename);
            this.trcnPanel.Controls.Add(this.pjse_banner1);
            this.trcnPanel.Controls.Add(this.lvTrcnItem);
            this.trcnPanel.Location = new System.Drawing.Point(0, 0);
            this.trcnPanel.Margin = new System.Windows.Forms.Padding(2);
            this.trcnPanel.Name = "trcnPanel";
            this.trcnPanel.Size = new System.Drawing.Size(853, 341);
            this.trcnPanel.TabIndex = 0;
            // 
            // btSetAll
            // 
            this.btSetAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btSetAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btSetAll.Location = new System.Drawing.Point(8, 316);
            this.btSetAll.Name = "btSetAll";
            this.btSetAll.Size = new System.Drawing.Size(75, 23);
            this.btSetAll.TabIndex = 24;
            this.btSetAll.Text = "Set all Used";
            this.btSetAll.UseVisualStyleBackColor = true;
            this.btSetAll.Click += new System.EventHandler(this.btSetAll_Click);
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.btnStrAdd);
            this.panel2.Controls.Add(this.lbLabel);
            this.panel2.Controls.Add(this.btnStrDelete);
            this.panel2.Controls.Add(this.tbLabel);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnStrPrev);
            this.panel2.Controls.Add(this.btnStrNext);
            this.panel2.Location = new System.Drawing.Point(0, 59);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(207, 85);
            this.panel2.TabIndex = 0;
            // 
            // btnStrAdd
            // 
            this.btnStrAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrAdd.Location = new System.Drawing.Point(39, 2);
            this.btnStrAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrAdd.Name = "btnStrAdd";
            this.btnStrAdd.Size = new System.Drawing.Size(78, 22);
            this.btnStrAdd.TabIndex = 17;
            this.btnStrAdd.Text = "&Add Label";
            this.btnStrAdd.Click += new System.EventHandler(this.btnStrAdd_Click);
            // 
            // lbLabel
            // 
            this.lbLabel.AutoSize = true;
            this.lbLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbLabel.Location = new System.Drawing.Point(2, 29);
            this.lbLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbLabel.Name = "lbLabel";
            this.lbLabel.Size = new System.Drawing.Size(33, 13);
            this.lbLabel.TabIndex = 0;
            this.lbLabel.Text = "Label";
            // 
            // btnStrDelete
            // 
            this.btnStrDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrDelete.Location = new System.Drawing.Point(127, 2);
            this.btnStrDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrDelete.Name = "btnStrDelete";
            this.btnStrDelete.Size = new System.Drawing.Size(78, 22);
            this.btnStrDelete.TabIndex = 18;
            this.btnStrDelete.Text = "De&lete Label";
            this.btnStrDelete.Click += new System.EventHandler(this.btnStrDelete_Click);
            // 
            // tbLabel
            // 
            this.tbLabel.Location = new System.Drawing.Point(39, 26);
            this.tbLabel.Margin = new System.Windows.Forms.Padding(2);
            this.tbLabel.MaxLength = 64;
            this.tbLabel.Name = "tbLabel";
            this.tbLabel.Size = new System.Drawing.Size(166, 20);
            this.tbLabel.TabIndex = 2;
            this.tbLabel.Text = "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";
            this.tbLabel.TextChanged += new System.EventHandler(this.tbText_TextChanged);
            this.tbLabel.Enter += new System.EventHandler(this.tbText_Enter);
            // 
            // btnCancel
            // 
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(39, 50);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 22);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnStrPrev
            // 
            this.btnStrPrev.Font = new System.Drawing.Font("Wingdings", 8.25F);
            this.btnStrPrev.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrPrev.Location = new System.Drawing.Point(188, 50);
            this.btnStrPrev.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrPrev.Name = "btnStrPrev";
            this.btnStrPrev.Size = new System.Drawing.Size(17, 17);
            this.btnStrPrev.TabIndex = 15;
            this.btnStrPrev.TabStop = false;
            this.btnStrPrev.Text = "á         &Up";
            this.btnStrPrev.Click += new System.EventHandler(this.btnStrPrev_Click);
            // 
            // btnStrNext
            // 
            this.btnStrNext.Font = new System.Drawing.Font("Wingdings", 8.25F);
            this.btnStrNext.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStrNext.Location = new System.Drawing.Point(188, 67);
            this.btnStrNext.Margin = new System.Windows.Forms.Padding(2);
            this.btnStrNext.Name = "btnStrNext";
            this.btnStrNext.Size = new System.Drawing.Size(17, 16);
            this.btnStrNext.TabIndex = 16;
            this.btnStrNext.TabStop = false;
            this.btnStrNext.Text = "â         &Down";
            this.btnStrNext.Click += new System.EventHandler(this.btnStrNext_Click);
            // 
            // tlpUnused
            // 
            this.tlpUnused.AutoSize = true;
            this.tlpUnused.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpUnused.ColumnCount = 2;
            this.tlpUnused.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpUnused.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpUnused.Controls.Add(this.label5, 0, 1);
            this.tlpUnused.Controls.Add(this.lbID, 0, 2);
            this.tlpUnused.Controls.Add(this.tbID, 1, 2);
            this.tlpUnused.Controls.Add(this.lbDesc, 0, 4);
            this.tlpUnused.Controls.Add(this.tbDesc, 1, 4);
            this.tlpUnused.Controls.Add(this.lbDefValue, 0, 5);
            this.tlpUnused.Controls.Add(this.tbDefValue, 1, 5);
            this.tlpUnused.Controls.Add(this.panel1, 0, 0);
            this.tlpUnused.Controls.Add(this.lbMinValue, 0, 6);
            this.tlpUnused.Controls.Add(this.tbMinValue, 1, 6);
            this.tlpUnused.Controls.Add(this.lbMaxValue, 0, 7);
            this.tlpUnused.Controls.Add(this.tbMaxValue, 1, 7);
            this.tlpUnused.Controls.Add(this.cbUsed, 0, 3);
            this.tlpUnused.Location = new System.Drawing.Point(0, 150);
            this.tlpUnused.Name = "tlpUnused";
            this.tlpUnused.RowCount = 8;
            this.tlpUnused.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpUnused.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpUnused.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpUnused.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpUnused.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpUnused.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpUnused.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpUnused.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpUnused.Size = new System.Drawing.Size(225, 159);
            this.tlpUnused.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.tlpUnused.SetColumnSpan(this.label5, 2);
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(2, 5);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "The following attributes are unused:";
            // 
            // lbID
            // 
            this.lbID.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbID.AutoSize = true;
            this.lbID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbID.Location = new System.Drawing.Point(38, 23);
            this.lbID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbID.Name = "lbID";
            this.lbID.Size = new System.Drawing.Size(18, 13);
            this.lbID.TabIndex = 3;
            this.lbID.Text = "ID";
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(60, 20);
            this.tbID.Margin = new System.Windows.Forms.Padding(2);
            this.tbID.MaxLength = 10;
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(80, 20);
            this.tbID.TabIndex = 4;
            this.tbID.Text = "0xDDDDDDDD";
            this.tbID.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            this.tbID.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbID.Enter += new System.EventHandler(this.tbText_Enter);
            this.tbID.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            // 
            // lbDesc
            // 
            this.lbDesc.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbDesc.AutoSize = true;
            this.lbDesc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbDesc.Location = new System.Drawing.Point(24, 68);
            this.lbDesc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDesc.Name = "lbDesc";
            this.lbDesc.Size = new System.Drawing.Size(32, 13);
            this.lbDesc.TabIndex = 6;
            this.lbDesc.Text = "Desc";
            // 
            // tbDesc
            // 
            this.tbDesc.Location = new System.Drawing.Point(60, 65);
            this.tbDesc.Margin = new System.Windows.Forms.Padding(2);
            this.tbDesc.MaxLength = 64;
            this.tbDesc.Name = "tbDesc";
            this.tbDesc.ReadOnly = true;
            this.tbDesc.Size = new System.Drawing.Size(159, 20);
            this.tbDesc.TabIndex = 7;
            this.tbDesc.Text = "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";
            this.tbDesc.TextChanged += new System.EventHandler(this.tbDesc_TextChanged);
            // 
            // lbDefValue
            // 
            this.lbDefValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbDefValue.AutoSize = true;
            this.lbDefValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbDefValue.Location = new System.Drawing.Point(5, 92);
            this.lbDefValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDefValue.Name = "lbDefValue";
            this.lbDefValue.Size = new System.Drawing.Size(51, 13);
            this.lbDefValue.TabIndex = 8;
            this.lbDefValue.Text = "DefValue";
            // 
            // tbDefValue
            // 
            this.tbDefValue.Location = new System.Drawing.Point(60, 89);
            this.tbDefValue.Margin = new System.Windows.Forms.Padding(2);
            this.tbDefValue.MaxLength = 6;
            this.tbDefValue.Name = "tbDefValue";
            this.tbDefValue.Size = new System.Drawing.Size(49, 20);
            this.tbDefValue.TabIndex = 9;
            this.tbDefValue.Text = "0xDDDD";
            this.tbDefValue.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbDefValue.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbDefValue.Enter += new System.EventHandler(this.tbText_Enter);
            this.tbDefValue.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpUnused.SetColumnSpan(this.panel1, 2);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(221, 1);
            this.panel1.TabIndex = 25;
            // 
            // lbMinValue
            // 
            this.lbMinValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbMinValue.AutoSize = true;
            this.lbMinValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMinValue.Location = new System.Drawing.Point(5, 116);
            this.lbMinValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbMinValue.Name = "lbMinValue";
            this.lbMinValue.Size = new System.Drawing.Size(51, 13);
            this.lbMinValue.TabIndex = 10;
            this.lbMinValue.Text = "MinValue";
            // 
            // tbMinValue
            // 
            this.tbMinValue.Location = new System.Drawing.Point(60, 113);
            this.tbMinValue.Margin = new System.Windows.Forms.Padding(2);
            this.tbMinValue.MaxLength = 6;
            this.tbMinValue.Name = "tbMinValue";
            this.tbMinValue.Size = new System.Drawing.Size(49, 20);
            this.tbMinValue.TabIndex = 11;
            this.tbMinValue.Text = "0xDDDD";
            this.tbMinValue.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbMinValue.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbMinValue.Enter += new System.EventHandler(this.tbText_Enter);
            this.tbMinValue.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // lbMaxValue
            // 
            this.lbMaxValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbMaxValue.AutoSize = true;
            this.lbMaxValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMaxValue.Location = new System.Drawing.Point(2, 140);
            this.lbMaxValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbMaxValue.Name = "lbMaxValue";
            this.lbMaxValue.Size = new System.Drawing.Size(54, 13);
            this.lbMaxValue.TabIndex = 12;
            this.lbMaxValue.Text = "MaxValue";
            // 
            // tbMaxValue
            // 
            this.tbMaxValue.Location = new System.Drawing.Point(60, 137);
            this.tbMaxValue.Margin = new System.Windows.Forms.Padding(2);
            this.tbMaxValue.MaxLength = 6;
            this.tbMaxValue.Name = "tbMaxValue";
            this.tbMaxValue.Size = new System.Drawing.Size(49, 20);
            this.tbMaxValue.TabIndex = 13;
            this.tbMaxValue.Text = "0xDDDD";
            this.tbMaxValue.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbMaxValue.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbMaxValue.Enter += new System.EventHandler(this.tbText_Enter);
            this.tbMaxValue.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // cbUsed
            // 
            this.cbUsed.AutoSize = true;
            this.cbUsed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tlpUnused.SetColumnSpan(this.cbUsed, 2);
            this.cbUsed.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbUsed.Location = new System.Drawing.Point(24, 44);
            this.cbUsed.Margin = new System.Windows.Forms.Padding(24, 2, 2, 2);
            this.cbUsed.Name = "cbUsed";
            this.cbUsed.Size = new System.Drawing.Size(51, 17);
            this.cbUsed.TabIndex = 5;
            this.cbUsed.Text = "Used";
            this.cbUsed.CheckedChanged += new System.EventHandler(this.cbUsed_CheckedChanged);
            // 
            // tbFormat
            // 
            this.tbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFormat.Location = new System.Drawing.Point(690, 34);
            this.tbFormat.Margin = new System.Windows.Forms.Padding(2);
            this.tbFormat.MaxLength = 10;
            this.tbFormat.Name = "tbFormat";
            this.tbFormat.ReadOnly = true;
            this.tbFormat.Size = new System.Drawing.Size(81, 20);
            this.tbFormat.TabIndex = 22;
            this.tbFormat.Text = "0xDDDDDDDD";
            this.tbFormat.TextChanged += new System.EventHandler(this.hex32_TextChanged);
            this.tbFormat.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbFormat.Enter += new System.EventHandler(this.tbText_Enter);
            this.tbFormat.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            // 
            // lbFormat
            // 
            this.lbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFormat.AutoSize = true;
            this.lbFormat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbFormat.Location = new System.Drawing.Point(647, 37);
            this.lbFormat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFormat.Name = "lbFormat";
            this.lbFormat.Size = new System.Drawing.Size(39, 13);
            this.lbFormat.TabIndex = 21;
            this.lbFormat.Text = "Format";
            // 
            // tbFilename
            // 
            this.tbFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilename.Location = new System.Drawing.Point(57, 34);
            this.tbFilename.Margin = new System.Windows.Forms.Padding(2);
            this.tbFilename.MaxLength = 64;
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.Size = new System.Drawing.Size(586, 20);
            this.tbFilename.TabIndex = 20;
            this.tbFilename.Text = "ffffff";
            this.tbFilename.TextChanged += new System.EventHandler(this.tbText_TextChanged);
            this.tbFilename.Enter += new System.EventHandler(this.tbText_Enter);
            // 
            // lbFilename
            // 
            this.lbFilename.AutoSize = true;
            this.lbFilename.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbFilename.Location = new System.Drawing.Point(4, 37);
            this.lbFilename.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFilename.Name = "lbFilename";
            this.lbFilename.Size = new System.Drawing.Size(49, 13);
            this.lbFilename.TabIndex = 19;
            this.lbFilename.Text = "Filename";
            // 
            // pjse_banner1
            // 
            this.pjse_banner1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pjse_banner1.Location = new System.Drawing.Point(0, 0);
            this.pjse_banner1.Name = "pjse_banner1";
            this.pjse_banner1.SiblingText = "BCON";
            this.pjse_banner1.SiblingVisible = true;
            this.pjse_banner1.Size = new System.Drawing.Size(853, 27);
            this.pjse_banner1.TabIndex = 0;
            this.pjse_banner1.TitleText = "Behaviour Constant Labels";
            this.pjse_banner1.TreeText = "Comments";
            this.pjse_banner1.SiblingClick += new System.EventHandler(this.pjse_banner1_SiblingClick);
            // 
            // lvTrcnItem
            // 
            this.lvTrcnItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
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
            listViewItem1});
            this.lvTrcnItem.Location = new System.Drawing.Point(226, 58);
            this.lvTrcnItem.Margin = new System.Windows.Forms.Padding(2);
            this.lvTrcnItem.MultiSelect = false;
            this.lvTrcnItem.Name = "lvTrcnItem";
            this.lvTrcnItem.Size = new System.Drawing.Size(625, 282);
            this.lvTrcnItem.TabIndex = 1;
            this.lvTrcnItem.UseCompatibleStateImageBehavior = false;
            this.lvTrcnItem.View = System.Windows.Forms.View.Details;
            this.lvTrcnItem.Resize += new System.EventHandler(this.lvTrcnItem_Resize);
            this.lvTrcnItem.SelectedIndexChanged += new System.EventHandler(this.lvTrcnItem_SelectedIndexChanged);
            // 
            // chLine
            // 
            this.chLine.Text = "Line";
            this.chLine.Width = 77;
            // 
            // chValue
            // 
            this.chValue.Text = "Value";
            this.chValue.Width = 65;
            // 
            // chConstName
            // 
            this.chConstName.Text = "Label";
            this.chConstName.Width = 80;
            // 
            // chConstId
            // 
            this.chConstId.Text = "ID";
            this.chConstId.Width = 105;
            // 
            // chUsed
            // 
            this.chUsed.Text = "Used";
            this.chUsed.Width = 45;
            // 
            // chDefValue
            // 
            this.chDefValue.Text = "DefValue";
            this.chDefValue.Width = 69;
            // 
            // chMinValue
            // 
            this.chMinValue.Text = "MinValue";
            this.chMinValue.Width = 69;
            // 
            // chMaxValue
            // 
            this.chMaxValue.Text = "MaxValue";
            this.chMaxValue.Width = 72;
            // 
            // TrcnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(857, 343);
            this.Controls.Add(this.trcnPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TrcnForm";
            this.Text = "TrcnForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.trcnPanel.ResumeLayout(false);
            this.trcnPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tlpUnused.ResumeLayout(false);
            this.tlpUnused.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private void lvTrcnItem_Resize(object sender, EventArgs e)
        {
            int before = lvTrcnItem.Columns[0].Width + lvTrcnItem.Columns[1].Width;
            int after = 0;
            for (int i = 3; i < lvTrcnItem.Columns.Count; i++) after += lvTrcnItem.Columns[i].Width;
            lvTrcnItem.Columns[2].Width = lvTrcnItem.Width - (before + after + 36);
        }

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


        private void pjse_banner1_SiblingClick(object sender, EventArgs e)
        {
            Bcon bcon = (Bcon)wrapper.SiblingResource(Bcon.Bcontype);
            if (bcon == null) return;
            if (bcon.Package != wrapper.Package)
            {
                DialogResult dr = MessageBox.Show(Localization.GetString("OpenOtherPkg"), pjse_banner1.TitleText, MessageBoxButtons.YesNo);
                if (dr != DialogResult.Yes) return;
            }
            SimPe.RemoteControl.OpenPackedFile(bcon.FileDescriptor, bcon.Package);
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

        private void tbDesc_TextChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            if (wrapper.Version > 0x53) currentItem.ConstDesc = this.tbDesc.Text;
        }

        private void btSetAll_Click(object sender, EventArgs e)
        {
            if (internalchg) return;
            internalchg = true;
            uint fid = 0;
            foreach (TrcnItem fing in wrapper)
            {
                fid++;
                fing.Used = 1;
                if (fing.MaxValue == 0) fing.MaxValue = 100;
                if (fing.ConstId == 0) fing.ConstId = fid;
            }
            internalchg = false;
            updateLists();
        }
	}
}
