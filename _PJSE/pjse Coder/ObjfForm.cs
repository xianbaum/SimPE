/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   pljones@users.sf.net                                                  *
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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for ObjfForm.
	/// </summary>
	public class ObjfForm : System.Windows.Forms.Form, IPackedFileUI
	{
		#region Form variables

		private System.Windows.Forms.Label label19;
		private booby.gradientpanel objfPanel;
		private System.Windows.Forms.Label lbFilename;
		private System.Windows.Forms.TextBox tbFilename;
		private System.Windows.Forms.LinkLabel llGuardian;
		private System.Windows.Forms.LinkLabel llAction;
		private System.Windows.Forms.Button btnAction;
		private System.Windows.Forms.Button btnGuardian;
		private System.Windows.Forms.TextBox tbGuardian;
		private System.Windows.Forms.TextBox tbAction;
        private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.Label lbAction;
		private System.Windows.Forms.Label lbGuardian;
		private System.Windows.Forms.ListView lvObjfItem;
		private System.Windows.Forms.ColumnHeader chFunction;
		private System.Windows.Forms.ColumnHeader chGuardian;
        private System.Windows.Forms.ColumnHeader chAction;
        private Label lbFunction;
        private pjse.pjse_banner pjse_banner1;
        private IContainer components = null;
        #endregion

		public ObjfForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            lbFunction.Text = "";
            TextBox[] tbua = { tbAction, tbGuardian };
			alHex16 = new ArrayList(tbua);

            pjse.FileTable.GFT.FiletableRefresh += new EventHandler(GFT_FiletableRefresh);
            booby.ThemeManager.Global.AddControl(this.objfPanel);
            if (booby.ThemeManager.ThemedForms)
            {
                this.lvObjfItem.BackColor = booby.ThemeManager.Global.ThemeColorLighter;
                booby.ThemeManager.Global.AddControl(this.btnCommit);
            }
            if (booby.PrettyGirls.PervyMode && Helper.StartedGui == Executable.Default)
                this.objfPanel.BackgroundImage = booby.PrettyGirls.Kirsten;
            if (SimPe.Helper.WindowsRegistry.UseBigIcons)
            {
                this.lvObjfItem.Font = new System.Drawing.Font(this.objfPanel.Font.Name, 11F);
                this.chAction.Width = 350;
                this.chGuardian.Width = 350;
                this.chFunction.Width = 350;
            }
        }

        void GFT_FiletableRefresh(object sender, EventArgs e)
        {
            if (wrapper.FileDescriptor == null) return;

            bool savedchg = internalchg;
            internalchg = true;

            bool parm = false;

            funcDescs = new pjse.Str(pjse.GS.BhavStr.OBJFDescs);
            if (wrapper.Count == 0)
            {
                int max = pjse.BhavWiz.readStr(pjse.GS.BhavStr.OBJFDescs).Count;
                for (int i = 0; i < max; i++) wrapper.Add(new ObjfItem(wrapper));
                lvObjfItem.Items[0].Selected = true;
            }
            for (ushort i = 0; i < lvObjfItem.Items.Count; i++)
            {
                lvObjfItem.Items[i].SubItems[0].Text = pjse.BhavWiz.readStr(pjse.GS.BhavStr.OBJFDescs, i);
                lvObjfItem.Items[i].SubItems[1].Text = pjse.BhavWiz.bhavName(wrapper, wrapper[i].Action, ref parm);
                lvObjfItem.Items[i].SubItems[2].Text = pjse.BhavWiz.bhavName(wrapper, wrapper[i].Guardian, ref parm);
            }

            if (lvObjfItem.SelectedIndices.Count > 0)
                setLabel(lvObjfItem.SelectedIndices[0]);

            if (currentItem != null)
            {
                setBHAV(0, currentItem.Action, false);
                setBHAV(1, currentItem.Guardian, false);
            }

            internalchg = savedchg;
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


		#region ObjfForm
		/// <summary>
		/// Stores the currently active Wrapper
		/// </summary>
		private Objf wrapper = null;
		private bool internalchg;
		private bool setHandler = false;
		private ArrayList alHex16;
		private ObjfItem origItem;
		private ObjfItem currentItem;

        private static pjse.Str funcDescs = new pjse.Str(pjse.GS.BhavStr.OBJFDescs);
        private void setLabel(int index)
        {
            lbFunction.Text = "";
            if (funcDescs == null || index < 0 || ((pjse.FallbackStrItem)funcDescs[index]) == null) return;
            StrItem s = ((pjse.FallbackStrItem)funcDescs[index]).strItem;
            if (s != null) lbFunction.Text = s.Description;
        }

		private bool hex16_IsValid(object sender)
		{
			if (alHex16.IndexOf(sender) < 0)
				throw new Exception("hex16_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToUInt16(((TextBox)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}


		private void setBHAV(int which, ushort target, bool notxt)
		{
			TextBox[] tbaAG = { tbAction, tbGuardian };
			if (!notxt) tbaAG[which].Text = "0x"+Helper.HexString(target);

			Label[] lbaAG = { lbAction, lbGuardian };
			LinkLabel[] llaAG = { llAction, llGuardian };
			bool found = false;
			this.lvObjfItem.SelectedItems[0].SubItems[1 + which].Text = lbaAG[which].Text = pjse.BhavWiz.bhavName(wrapper, target, ref found);
			llaAG[which].Enabled = found;
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
				return objfPanel;
			}
		}

		/// <summary>
		/// Called by the AbstractWrapper when the file should be displayed to the user.
		/// </summary>
		/// <param name="wrp">Reference to the wrapper to be displayed.</param>
        public void UpdateGUI(IFileWrapper wrp)
        {
            wrapper = (Objf)wrp;
            WrapperChanged(wrapper, null);
            internalchg = true;

            if (booby.PrettyGirls.PervyMode && Helper.StartedGui == Executable.Default)
            {
                if (lvObjfItem.Height > 522)
                {
                    int neht = lvObjfItem.Width - (lvObjfItem.Height - 522);
                    this.lvObjfItem.Size = new System.Drawing.Size(neht, lvObjfItem.Height);
                }
            }

            this.lvObjfItem.Items.Clear();
            bool parm = false;

            // There appears to be no clean way to handle a "new" resource being created in the wrapper
            // so this is in here.  Yuck.
            if (wrapper.Count == 0)
            {
                int max = pjse.BhavWiz.readStr(pjse.GS.BhavStr.OBJFDescs).Count;
                for (int i = 0; i < max; i++) wrapper.Add(new ObjfItem(wrapper));
            }
            for (ushort i = 0; i < wrapper.Count; i++)
                this.lvObjfItem.Items.Add(new ListViewItem(
                    new string[] {
									 pjse.BhavWiz.readStr(pjse.GS.BhavStr.OBJFDescs, i)
									 , pjse.BhavWiz.bhavName(wrapper, wrapper[i].Action, ref parm)
									 , pjse.BhavWiz.bhavName(wrapper, wrapper[i].Guardian, ref parm)
								 }
                    ));

            internalchg = false;

            lvObjfItem.Items[0].Selected = true;

            if (!setHandler)
            {
                wrapper.WrapperChanged += new System.EventHandler(this.WrapperChanged);
                setHandler = true;
            }
        }

		private void WrapperChanged(object sender, System.EventArgs e)
		{
			this.btnCommit.Enabled = wrapper.Changed;

			if (internalchg) return;
			internalchg = true;
			this.Text = tbFilename.Text = wrapper.FileName;
			internalchg = false;
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.objfPanel = new booby.gradientpanel();
            this.btnCommit = new System.Windows.Forms.Button();
            this.btnGuardian = new System.Windows.Forms.Button();
            this.btnAction = new System.Windows.Forms.Button();
            this.tbAction = new System.Windows.Forms.TextBox();
            this.tbGuardian = new System.Windows.Forms.TextBox();
            this.llAction = new System.Windows.Forms.LinkLabel();
            this.llGuardian = new System.Windows.Forms.LinkLabel();
            this.pjse_banner1 = new pjse.pjse_banner();
            this.lbFunction = new System.Windows.Forms.Label();
            this.lvObjfItem = new System.Windows.Forms.ListView();
            this.chFunction = new System.Windows.Forms.ColumnHeader();
            this.chAction = new System.Windows.Forms.ColumnHeader();
            this.chGuardian = new System.Windows.Forms.ColumnHeader();
            this.lbAction = new System.Windows.Forms.Label();
            this.lbGuardian = new System.Windows.Forms.Label();
            this.lbFilename = new System.Windows.Forms.Label();
            this.tbFilename = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.objfPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // objfPanel
            // 
            this.objfPanel.AutoScroll = true;
            this.objfPanel.BackColor = System.Drawing.Color.Transparent;
            this.objfPanel.BackgroundImageAnchor = booby.gradientpanel.ImageLayout.TopRight;
            this.objfPanel.BackgroundImageLocation = new System.Drawing.Point(0, 243);
            this.objfPanel.BackgroundImageZoomToFit = true;
            this.objfPanel.Controls.Add(this.btnCommit);
            this.objfPanel.Controls.Add(this.btnGuardian);
            this.objfPanel.Controls.Add(this.btnAction);
            this.objfPanel.Controls.Add(this.tbAction);
            this.objfPanel.Controls.Add(this.tbGuardian);
            this.objfPanel.Controls.Add(this.llAction);
            this.objfPanel.Controls.Add(this.llGuardian);
            this.objfPanel.Controls.Add(this.pjse_banner1);
            this.objfPanel.Controls.Add(this.lbFunction);
            this.objfPanel.Controls.Add(this.lvObjfItem);
            this.objfPanel.Controls.Add(this.lbAction);
            this.objfPanel.Controls.Add(this.lbGuardian);
            this.objfPanel.Controls.Add(this.lbFilename);
            this.objfPanel.Controls.Add(this.tbFilename);
            this.objfPanel.Controls.Add(this.label19);
            this.objfPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objfPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.objfPanel.Location = new System.Drawing.Point(0, 0);
            this.objfPanel.Name = "objfPanel";
            this.objfPanel.Size = new System.Drawing.Size(872, 304);
            this.objfPanel.TabIndex = 0;
            // 
            // btnCommit
            // 
            this.btnCommit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCommit.AutoEllipsis = true;
            this.btnCommit.BackColor = System.Drawing.Color.Transparent;
            this.btnCommit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCommit.Location = new System.Drawing.Point(775, 31);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(87, 23);
            this.btnCommit.TabIndex = 9;
            this.btnCommit.Text = "Commit &File";
            this.btnCommit.UseVisualStyleBackColor = true;
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // btnGuardian
            // 
            this.btnGuardian.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardian.Font = new System.Drawing.Font("Webdings", 14F);
            this.btnGuardian.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGuardian.Location = new System.Drawing.Point(789, 167);
            this.btnGuardian.Margin = new System.Windows.Forms.Padding(0);
            this.btnGuardian.Name = "btnGuardian";
            this.btnGuardian.Size = new System.Drawing.Size(18, 21);
            this.btnGuardian.TabIndex = 5;
            this.btnGuardian.Text = "4";
            this.btnGuardian.UseCompatibleTextRendering = true;
            this.btnGuardian.Click += new System.EventHandler(this.GetObjfGuard);
            // 
            // btnAction
            // 
            this.btnAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAction.Font = new System.Drawing.Font("Webdings", 14F);
            this.btnAction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAction.Location = new System.Drawing.Point(789, 92);
            this.btnAction.Margin = new System.Windows.Forms.Padding(0);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(18, 21);
            this.btnAction.TabIndex = 3;
            this.btnAction.Text = "4";
            this.btnAction.UseCompatibleTextRendering = true;
            this.btnAction.Click += new System.EventHandler(this.GetObjfAction);
            // 
            // tbAction
            // 
            this.tbAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAction.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.tbAction.Location = new System.Drawing.Point(709, 92);
            this.tbAction.Margin = new System.Windows.Forms.Padding(0);
            this.tbAction.MaxLength = 6;
            this.tbAction.Name = "tbAction";
            this.tbAction.Size = new System.Drawing.Size(72, 21);
            this.tbAction.TabIndex = 2;
            this.tbAction.Text = "0xDDDD";
            this.tbAction.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbAction.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbAction.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // tbGuardian
            // 
            this.tbGuardian.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbGuardian.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.tbGuardian.Location = new System.Drawing.Point(709, 167);
            this.tbGuardian.Margin = new System.Windows.Forms.Padding(0);
            this.tbGuardian.MaxLength = 6;
            this.tbGuardian.Name = "tbGuardian";
            this.tbGuardian.Size = new System.Drawing.Size(72, 21);
            this.tbGuardian.TabIndex = 4;
            this.tbGuardian.Text = "0xDDDD";
            this.tbGuardian.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbGuardian.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbGuardian.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // llAction
            // 
            this.llAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llAction.AutoSize = true;
            this.llAction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llAction.LinkArea = new System.Windows.Forms.LinkArea(0, 11);
            this.llAction.Location = new System.Drawing.Point(623, 96);
            this.llAction.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.llAction.Name = "llAction";
            this.llAction.Size = new System.Drawing.Size(78, 13);
            this.llAction.TabIndex = 6;
            this.llAction.TabStop = true;
            this.llAction.Text = "Action BHAV";
            this.llAction.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llBhav_LinkClicked);
            // 
            // llGuardian
            // 
            this.llGuardian.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llGuardian.AutoSize = true;
            this.llGuardian.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llGuardian.LinkArea = new System.Windows.Forms.LinkArea(0, 13);
            this.llGuardian.Location = new System.Drawing.Point(606, 171);
            this.llGuardian.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.llGuardian.Name = "llGuardian";
            this.llGuardian.Size = new System.Drawing.Size(95, 13);
            this.llGuardian.TabIndex = 7;
            this.llGuardian.TabStop = true;
            this.llGuardian.Text = "Guardian BHAV";
            this.llGuardian.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llBhav_LinkClicked);
            // 
            // pjse_banner1
            // 
            this.pjse_banner1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pjse_banner1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.pjse_banner1.Location = new System.Drawing.Point(0, 0);
            this.pjse_banner1.Name = "pjse_banner1";
            this.pjse_banner1.Size = new System.Drawing.Size(872, 26);
            this.pjse_banner1.TabIndex = 10;
            this.pjse_banner1.TitleText = "Object Functions";
            this.pjse_banner1.TreeText = "Comments";
            // 
            // lbFunction
            // 
            this.lbFunction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFunction.AutoEllipsis = true;
            this.lbFunction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbFunction.Location = new System.Drawing.Point(79, 54);
            this.lbFunction.Name = "lbFunction";
            this.lbFunction.Size = new System.Drawing.Size(783, 30);
            this.lbFunction.TabIndex = 0;
            this.lbFunction.Text = "---";
            this.lbFunction.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lvObjfItem
            // 
            this.lvObjfItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvObjfItem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFunction,
            this.chAction,
            this.chGuardian});
            this.lvObjfItem.FullRowSelect = true;
            this.lvObjfItem.GridLines = true;
            this.lvObjfItem.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvObjfItem.HideSelection = false;
            this.lvObjfItem.Location = new System.Drawing.Point(0, 87);
            this.lvObjfItem.MultiSelect = false;
            this.lvObjfItem.Name = "lvObjfItem";
            this.lvObjfItem.Size = new System.Drawing.Size(600, 214);
            this.lvObjfItem.TabIndex = 1;
            this.lvObjfItem.UseCompatibleStateImageBehavior = false;
            this.lvObjfItem.View = System.Windows.Forms.View.Details;
            this.lvObjfItem.SelectedIndexChanged += new System.EventHandler(this.lvObjfItem_SelectedIndexChanged);
            // 
            // chFunction
            // 
            this.chFunction.Text = "Function";
            this.chFunction.Width = 278;
            // 
            // chAction
            // 
            this.chAction.Text = "Action";
            this.chAction.Width = 158;
            // 
            // chGuardian
            // 
            this.chGuardian.Text = "Guardian";
            this.chGuardian.Width = 158;
            // 
            // lbAction
            // 
            this.lbAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbAction.Location = new System.Drawing.Point(609, 121);
            this.lbAction.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lbAction.Name = "lbAction";
            this.lbAction.Size = new System.Drawing.Size(253, 38);
            this.lbAction.TabIndex = 0;
            this.lbAction.Text = "---";
            this.lbAction.UseMnemonic = false;
            // 
            // lbGuardian
            // 
            this.lbGuardian.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbGuardian.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbGuardian.Location = new System.Drawing.Point(612, 196);
            this.lbGuardian.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lbGuardian.Name = "lbGuardian";
            this.lbGuardian.Size = new System.Drawing.Size(250, 37);
            this.lbGuardian.TabIndex = 0;
            this.lbGuardian.Text = "---";
            this.lbGuardian.UseMnemonic = false;
            // 
            // lbFilename
            // 
            this.lbFilename.AutoSize = true;
            this.lbFilename.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbFilename.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbFilename.Location = new System.Drawing.Point(6, 32);
            this.lbFilename.Name = "lbFilename";
            this.lbFilename.Size = new System.Drawing.Size(67, 13);
            this.lbFilename.TabIndex = 0;
            this.lbFilename.Text = "Filename";
            this.lbFilename.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbFilename
            // 
            this.tbFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilename.Location = new System.Drawing.Point(79, 29);
            this.tbFilename.MaxLength = 64;
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.Size = new System.Drawing.Size(690, 21);
            this.tbFilename.TabIndex = 8;
            this.tbFilename.TextChanged += new System.EventHandler(this.tbFilename_TextChanged);
            this.tbFilename.Validated += new System.EventHandler(this.tbFilename_Validated);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label19.Location = new System.Drawing.Point(2, 70);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(70, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "Functions";
            // 
            // ObjfForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(872, 304);
            this.Controls.Add(this.objfPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ObjfForm";
            this.Text = "ObjfForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.objfPanel.ResumeLayout(false);
            this.objfPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private void lvObjfItem_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (this.internalchg) return;

			if (lvObjfItem.SelectedIndices.Count > 0 && lvObjfItem.SelectedIndices[0] >= 0)
			{
				currentItem = wrapper[lvObjfItem.SelectedIndices[0]];
                setLabel(lvObjfItem.SelectedIndices[0]);
				origItem = currentItem.Clone();

				internalchg = true;

				setBHAV(0, currentItem.Action, false);
				setBHAV(1, currentItem.Guardian, false);
				tbGuardian.Enabled = tbAction.Enabled = true;

				internalchg = false;
			}
			else
			{
				internalchg = true;

				tbGuardian.Text = tbAction.Text = lbGuardian.Text = lbAction.Text = "";
				tbGuardian.Enabled = tbAction.Enabled = false;

				internalchg = false;
			}
		}

		private void llBhav_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			pjse.FileTable.Entry item = wrapper.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, (sender == llAction) ? currentItem.Action : currentItem.Guardian);
			Bhav b = new Bhav();
			b.ProcessData(item.PFD, item.Package);

			BhavForm ui = (BhavForm)b.UIHandler;
            ui.Tag = "Popup" // tells the SetReadOnly function it's in a popup - so everything locked down
                + ";callerID=+" + wrapper.FileDescriptor.ExportFileName + "+";
            ui.Text = pjse.Localization.GetString("viewbhav") + ": " + b.FileName + " [" + b.Package.SaveFileName + "]";
            b.RefreshUI();
			ui.Show();
		}

		private void btnCommit_Click(object sender, System.EventArgs e)
		{
			try
			{
				wrapper.SynchronizeUserData();
				btnCommit.Enabled = wrapper.Changed;
				lvObjfItem_SelectedIndexChanged(null, null);
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(pjse.Localization.GetString("errwritingfile"), ex);
			}
		}

		private void GetObjfAction(object sender, System.EventArgs e)
		{
			pjse.FileTable.Entry item = new pjse.ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, objfPanel.Parent, false);
			if (item != null)
				setBHAV(0, (ushort)item.Instance, false);
		}

		private void GetObjfGuard(object sender, System.EventArgs e)
		{
			pjse.FileTable.Entry item = new pjse.ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, objfPanel.Parent, false);
			if (item != null)
				setBHAV(1, (ushort)item.Instance, false);
		}

		private void tbFilename_TextChanged(object sender, System.EventArgs e)
		{
			wrapper.FileName = tbFilename.Text;
		}

		private void tbFilename_Validated(object sender, System.EventArgs e)
		{
			tbFilename.SelectAll();
		}

		private void hex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex16_IsValid(sender)) return;

			ushort val = Convert.ToUInt16(((TextBox)sender).Text, 16);
			internalchg = true;
			switch (alHex16.IndexOf(sender))
			{
				case 0: currentItem.Action = val; setBHAV(0, val, true); break;
				case 1: currentItem.Guardian = val; setBHAV(1, val, true); break;
			}
			internalchg = false;
		}

		private void hex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex16_IsValid(sender)) return;

			e.Cancel = true;

			internalchg = true;
			ushort val = 0;
			switch (alHex16.IndexOf(sender))
			{
				case 0:
					currentItem.Action = val = origItem.Action;
					setBHAV(0, val, true);
					break;
				case 1:
					currentItem.Guardian = val = origItem.Guardian;
					setBHAV(1, val, true);
					break;
			}
			((TextBox)sender).Text = "0x" + Helper.HexString(val);
			((TextBox)sender).SelectAll();
			internalchg = false;
		}

		private void hex16_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBox)sender).Text = "0x" + Helper.HexString(Convert.ToUInt16(((TextBox)sender).Text, 16));
			((TextBox)sender).SelectAll();
			internalchg = origstate;
		}
	}
}
