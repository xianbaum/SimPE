/***************************************************************************
 *   Copyright (C) 2005-2008 by Peter L Jones                              *
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
 *   59 Temple Place - Suite 330, Boston, MA  1c111-1307, USA.             *
 ***************************************************************************/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.PackedFiles.Wrapper;

namespace pjse.BhavOperandWizards.Wiz0x001c
{
    /// <summary>
    /// Summary description for StrBig.
    /// </summary>
    internal class UI : System.Windows.Forms.Form, iBhavOperandWizForm
	{
		#region Form variables

        internal System.Windows.Forms.Panel pnWiz0x001c;
        private Label label1;
        private ComboBox cbScope;
        private Label label2;
        private CheckBox tfPrivate;
        private CheckBox tfGlobal;
        private CheckBox tfSemiGlobal;
        private Label label3;
        private ComboBox cbRTBNType;
        private Label label4;
        private CheckBox tfParams;
        private CheckBox tfArgs;
        private Label label8;
        private TextBox tbTree;
        private Label lbTreeName;
        private Button btnTreeName;
        private LabelledDataOwner ldocArg1;
        private LabelledDataOwner ldocArg2;
        private LabelledDataOwner ldocArg3;
        private FlowLayoutPanel flpArgs;
        private FlowLayoutPanel flowLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel4;
        private FlowLayoutPanel flowLayoutPanel3;
        private FlowLayoutPanel flowLayoutPanel6;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public UI()
		{
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            this.cbRTBNType.Items.Clear();
            this.cbRTBNType.Items.AddRange(BhavWiz.readStr(GS.BhavStr.RTBNType).ToArray());

            ldocArg3.Decimal = ldocArg2.Decimal = ldocArg1.Decimal = pjse.Settings.PJSE.DecimalDOValue;
            ldocArg3.UseInstancePicker = ldocArg2.UseInstancePicker = ldocArg1.UseInstancePicker = pjse.Settings.PJSE.InstancePickerAsText;

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );

			inst = null;
        }


        private Instruction inst = null;
        private bool internalchg = false;
        private DataOwnerControl doidTree = null;

        void doidTree_DataOwnerControlChanged(object sender, EventArgs e)
        {
            this.lbTreeName.Text = ((BhavWiz)inst).readStr(this.Scope, GS.GlobalStr.NamedTree, (ushort)(doidTree.Value - 1), -1, pjse.Detail.ErrorNames);
        }

        private Scope Scope
        {
            get
            {
                Scope scope = Scope.Private;
                switch (this.cbScope.SelectedIndex)
                {
                    case 1: scope = Scope.SemiGlobal; break;
                    case 2: scope = Scope.Global; break;
                }
                return scope;
            }
        }

        private void doStrChooser()
        {
            pjse.FileTable.Entry[] items = pjse.FileTable.GFT[(uint)SimPe.Data.MetaData.STRING_FILE, inst.Parent.GroupForScope(this.Scope), (uint)GS.GlobalStr.NamedTree];

            if (items == null || items.Length == 0)
            {
                MessageBox.Show(pjse.Localization.GetString("bow_noStrings")
                    + " (" + pjse.Localization.GetString(this.Scope.ToString()) + ")");
                return; // eek!
            }

            SimPe.PackedFiles.Wrapper.StrWrapper str = new StrWrapper();
            str.ProcessData(items[0].PFD, items[0].Package);

            int i = (new StrChooser(true)).Strnum(str);
            if (i >= 0)
            {
                this.tbTree.Text = "0x" + SimPe.Helper.HexString((ushort)(i+1));
                this.lbTreeName.Text = ((BhavWiz)inst).readStr(this.Scope, GS.GlobalStr.NamedTree, (ushort)i, -1, pjse.Detail.ErrorNames);
            }
        }


        #region iBhavOperandWizForm
        public Panel WizPanel { get { return this.pnWiz0x001c; } }

		public void Execute(Instruction inst)
		{
            this.inst = ldocArg1.Instruction = ldocArg2.Instruction = ldocArg3.Instruction = inst;

            wrappedByteArray ops1 = inst.Operands;
            wrappedByteArray ops2 = inst.Reserved1;

            internalchg = true;

            Boolset options = (byte)(ops1[0x02] & 0x3f);

            this.cbScope.SelectedIndex = 0; // Private
            if      (options[0]) this.cbScope.SelectedIndex = 2; // Global
            else if (options[1]) this.cbScope.SelectedIndex = 1; // SemiGlobal

            this.tfSemiGlobal.Checked = !options[3];
            this.tfGlobal.Checked     = !options[2];

            this.cbRTBNType.SelectedIndex = ops1[0x05] < this.cbRTBNType.Items.Count ? ops1[0x05] : -1;

            this.flpArgs.Enabled = this.tfArgs.Checked = options[4];
            this.tfParams.Checked = options[5];

            doidTree = new DataOwnerControl(null, null, null, this.tbTree, null, null, null, 0x07, BhavWiz.ToShort(ops1[0x04], (byte)((ops1[0x02] >> 6) & 0x01)));
            doidTree.DataOwnerControlChanged += new EventHandler(doidTree_DataOwnerControlChanged);
            doidTree_DataOwnerControlChanged(null, null);

            ldocArg1.Value = BhavWiz.ToShort(ops1[0x07], ops2[0x00]); ldocArg1.DataOwner = ops1[0x06];
            ldocArg2.Value = BhavWiz.ToShort(ops2[0x02], ops2[0x03]); ldocArg2.DataOwner = ops2[0x01];
            ldocArg3.Value = BhavWiz.ToShort(ops2[0x05], ops2[0x06]); ldocArg3.DataOwner = ops2[0x04];

            internalchg = false;
        }

		public Instruction Write(Instruction inst)
		{
			if (inst != null)
			{
                wrappedByteArray ops1 = inst.Operands;
                wrappedByteArray ops2 = inst.Reserved1;

                Boolset options = (Boolset)(ops1[0x02] & 0xbf);
                int scope = this.cbScope.SelectedIndex;
                options[0] = (scope == 2);
                options[1] = (scope == 1);
                options[2] = !this.tfGlobal.Checked;
                options[3] = !this.tfSemiGlobal.Checked;
                options[4] = this.tfArgs.Checked;
                options[5] = this.tfParams.Checked;
                ops1[0x02] = options;
                ops1[0x02] |= (byte)((doidTree.Value >> 2) & 0x40);

                ops1[0x04] = (byte)(doidTree.Value & 0xff);

                if (this.cbRTBNType.SelectedIndex >= 0)
                    ops1[0x05] = (byte)this.cbRTBNType.SelectedIndex;

                byte[] lohi = { 0, 0 };
                ops1[0x06] = ldocArg1.DataOwner; BhavWiz.FromShort(ref lohi, 0, ldocArg1.Value); ops1[0x07] = lohi[0]; ops2[0x00] = lohi[1];
                ops2[0x01] = ldocArg2.DataOwner; BhavWiz.FromShort(ref ops2, 2, ldocArg2.Value);
                ops2[0x04] = ldocArg3.DataOwner; BhavWiz.FromShort(ref ops2, 5, ldocArg3.Value);
            }
			return inst;
		}

		#endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
		{
            this.pnWiz0x001c = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flpArgs = new System.Windows.Forms.FlowLayoutPanel();
            this.ldocArg1 = new pjse.LabelledDataOwner();
            this.ldocArg2 = new pjse.LabelledDataOwner();
            this.ldocArg3 = new pjse.LabelledDataOwner();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbScope = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbRTBNType = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.tbTree = new System.Windows.Forms.TextBox();
            this.btnTreeName = new System.Windows.Forms.Button();
            this.lbTreeName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.tfPrivate = new System.Windows.Forms.CheckBox();
            this.tfSemiGlobal = new System.Windows.Forms.CheckBox();
            this.tfGlobal = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.tfArgs = new System.Windows.Forms.CheckBox();
            this.tfParams = new System.Windows.Forms.CheckBox();
            this.pnWiz0x001c.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flpArgs.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnWiz0x001c
            // 
            this.pnWiz0x001c.AutoSize = true;
            this.pnWiz0x001c.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnWiz0x001c.Controls.Add(this.tableLayoutPanel1);
            this.pnWiz0x001c.Location = new System.Drawing.Point(0, 0);
            this.pnWiz0x001c.Margin = new System.Windows.Forms.Padding(2);
            this.pnWiz0x001c.Name = "pnWiz0x001c";
            this.pnWiz0x001c.Size = new System.Drawing.Size(428, 233);
            this.pnWiz0x001c.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.flpArgs, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbRTBNType, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel4, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel6, 0, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(428, 233);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // flpArgs
            // 
            this.flpArgs.AutoSize = true;
            this.flpArgs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.flpArgs, 2);
            this.flpArgs.Controls.Add(this.ldocArg1);
            this.flpArgs.Controls.Add(this.ldocArg2);
            this.flpArgs.Controls.Add(this.ldocArg3);
            this.flpArgs.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpArgs.Location = new System.Drawing.Point(0, 96);
            this.flpArgs.Margin = new System.Windows.Forms.Padding(0);
            this.flpArgs.Name = "flpArgs";
            this.flpArgs.Size = new System.Drawing.Size(428, 137);
            this.flpArgs.TabIndex = 2;
            // 
            // ldocArg1
            // 
            this.ldocArg1.AutoSize = true;
            this.ldocArg1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ldocArg1.DataOwner = ((byte)(255));
            this.ldocArg1.DataOwnerEnabled = true;
            this.ldocArg1.DecimalVisible = false;
            this.ldocArg1.Instruction = null;
            this.ldocArg1.Label = "Argument 1";
            this.ldocArg1.LabelSize = new System.Drawing.Size(61, 13);
            this.ldocArg1.Location = new System.Drawing.Point(0, 0);
            this.ldocArg1.Margin = new System.Windows.Forms.Padding(0);
            this.ldocArg1.Name = "ldocArg1";
            this.ldocArg1.Size = new System.Drawing.Size(428, 40);
            this.ldocArg1.TabIndex = 1;
            this.ldocArg1.UseFlagNames = false;
            this.ldocArg1.UseInstancePickerVisible = false;
            this.ldocArg1.Value = ((ushort)(0));
            // 
            // ldocArg2
            // 
            this.ldocArg2.AutoSize = true;
            this.ldocArg2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ldocArg2.DataOwner = ((byte)(255));
            this.ldocArg2.DataOwnerEnabled = true;
            this.ldocArg2.DecimalVisible = false;
            this.ldocArg2.Instruction = null;
            this.ldocArg2.Label = "Argument 2";
            this.ldocArg2.LabelSize = new System.Drawing.Size(61, 13);
            this.ldocArg2.Location = new System.Drawing.Point(0, 40);
            this.ldocArg2.Margin = new System.Windows.Forms.Padding(0);
            this.ldocArg2.Name = "ldocArg2";
            this.ldocArg2.Size = new System.Drawing.Size(428, 40);
            this.ldocArg2.TabIndex = 1;
            this.ldocArg2.UseFlagNames = false;
            this.ldocArg2.UseInstancePickerVisible = false;
            this.ldocArg2.Value = ((ushort)(0));
            // 
            // ldocArg3
            // 
            this.ldocArg3.AutoSize = true;
            this.ldocArg3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ldocArg3.DataOwner = ((byte)(255));
            this.ldocArg3.DataOwnerEnabled = true;
            this.ldocArg3.Instruction = null;
            this.ldocArg3.Label = "Argument 3";
            this.ldocArg3.LabelSize = new System.Drawing.Size(61, 13);
            this.ldocArg3.Location = new System.Drawing.Point(0, 80);
            this.ldocArg3.Margin = new System.Windows.Forms.Padding(0);
            this.ldocArg3.Name = "ldocArg3";
            this.ldocArg3.Size = new System.Drawing.Size(428, 57);
            this.ldocArg3.TabIndex = 1;
            this.ldocArg3.UseFlagNames = false;
            this.ldocArg3.Value = ((ushort)(0));
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Scope";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.cbScope);
            this.flowLayoutPanel2.Controls.Add(this.label8);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(38, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(202, 21);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // cbScope
            // 
            this.cbScope.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScope.FormattingEnabled = true;
            this.cbScope.Items.AddRange(new object[] {
            "Private",
            "SemiGlobal",
            "Global"});
            this.cbScope.Location = new System.Drawing.Point(0, 0);
            this.cbScope.Margin = new System.Windows.Forms.Padding(0);
            this.cbScope.Name = "cbScope";
            this.cbScope.Size = new System.Drawing.Size(127, 21);
            this.cbScope.TabIndex = 1;
            this.cbScope.SelectedIndexChanged += new System.EventHandler(this.cbScope_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(127, 4);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "(for tree name)";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(9, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tree";
            // 
            // cbRTBNType
            // 
            this.cbRTBNType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRTBNType.FormattingEnabled = true;
            this.cbRTBNType.Items.AddRange(new object[] {
            "Private",
            "SemiGlobal",
            "Global"});
            this.cbRTBNType.Location = new System.Drawing.Point(38, 58);
            this.cbRTBNType.Margin = new System.Windows.Forms.Padding(0);
            this.cbRTBNType.Name = "cbRTBNType";
            this.cbRTBNType.Size = new System.Drawing.Size(127, 21);
            this.cbRTBNType.TabIndex = 7;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel4.Controls.Add(this.tbTree);
            this.flowLayoutPanel4.Controls.Add(this.btnTreeName);
            this.flowLayoutPanel4.Controls.Add(this.lbTreeName);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(38, 21);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(390, 20);
            this.flowLayoutPanel4.TabIndex = 1;
            // 
            // tbTree
            // 
            this.tbTree.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbTree.Location = new System.Drawing.Point(0, 0);
            this.tbTree.Margin = new System.Windows.Forms.Padding(0);
            this.tbTree.Name = "tbTree";
            this.tbTree.Size = new System.Drawing.Size(49, 20);
            this.tbTree.TabIndex = 2;
            this.tbTree.Text = "0xDDDD";
            // 
            // btnTreeName
            // 
            this.btnTreeName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnTreeName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnTreeName.Font = new System.Drawing.Font("Webdings", 7.8F);
            this.btnTreeName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnTreeName.Location = new System.Drawing.Point(49, 1);
            this.btnTreeName.Margin = new System.Windows.Forms.Padding(0);
            this.btnTreeName.Name = "btnTreeName";
            this.btnTreeName.Size = new System.Drawing.Size(18, 18);
            this.btnTreeName.TabIndex = 3;
            this.btnTreeName.Text = "8";
            this.btnTreeName.Click += new System.EventHandler(this.btnTreeName_Click);
            // 
            // lbTreeName
            // 
            this.lbTreeName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbTreeName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbTreeName.Location = new System.Drawing.Point(67, 4);
            this.lbTreeName.Margin = new System.Windows.Forms.Padding(0);
            this.lbTreeName.Name = "lbTreeName";
            this.lbTreeName.Size = new System.Drawing.Size(323, 12);
            this.lbTreeName.TabIndex = 2;
            this.lbTreeName.Text = "Tree name";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(7, 62);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Type";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel3, 2);
            this.flowLayoutPanel3.Controls.Add(this.label3);
            this.flowLayoutPanel3.Controls.Add(this.tfPrivate);
            this.flowLayoutPanel3.Controls.Add(this.tfSemiGlobal);
            this.flowLayoutPanel3.Controls.Add(this.tfGlobal);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 41);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(301, 17);
            this.flowLayoutPanel3.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(0, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Search which trees:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tfPrivate
            // 
            this.tfPrivate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tfPrivate.AutoSize = true;
            this.tfPrivate.Checked = true;
            this.tfPrivate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tfPrivate.Enabled = false;
            this.tfPrivate.Location = new System.Drawing.Point(107, 0);
            this.tfPrivate.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.tfPrivate.Name = "tfPrivate";
            this.tfPrivate.Size = new System.Drawing.Size(59, 17);
            this.tfPrivate.TabIndex = 4;
            this.tfPrivate.Text = "Private";
            this.tfPrivate.UseVisualStyleBackColor = true;
            // 
            // tfSemiGlobal
            // 
            this.tfSemiGlobal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tfSemiGlobal.AutoSize = true;
            this.tfSemiGlobal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tfSemiGlobal.Location = new System.Drawing.Point(166, 0);
            this.tfSemiGlobal.Margin = new System.Windows.Forms.Padding(0);
            this.tfSemiGlobal.Name = "tfSemiGlobal";
            this.tfSemiGlobal.Size = new System.Drawing.Size(79, 17);
            this.tfSemiGlobal.TabIndex = 5;
            this.tfSemiGlobal.Text = "SemiGlobal";
            this.tfSemiGlobal.UseVisualStyleBackColor = true;
            // 
            // tfGlobal
            // 
            this.tfGlobal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tfGlobal.AutoSize = true;
            this.tfGlobal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tfGlobal.Location = new System.Drawing.Point(245, 0);
            this.tfGlobal.Margin = new System.Windows.Forms.Padding(0);
            this.tfGlobal.Name = "tfGlobal";
            this.tfGlobal.Size = new System.Drawing.Size(56, 17);
            this.tfGlobal.TabIndex = 6;
            this.tfGlobal.Text = "Global";
            this.tfGlobal.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.AutoSize = true;
            this.flowLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel6, 2);
            this.flowLayoutPanel6.Controls.Add(this.tfArgs);
            this.flowLayoutPanel6.Controls.Add(this.tfParams);
            this.flowLayoutPanel6.Location = new System.Drawing.Point(0, 79);
            this.flowLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(395, 17);
            this.flowLayoutPanel6.TabIndex = 1;
            // 
            // tfArgs
            // 
            this.tfArgs.AutoSize = true;
            this.tfArgs.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tfArgs.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tfArgs.Location = new System.Drawing.Point(0, 0);
            this.tfArgs.Margin = new System.Windows.Forms.Padding(0);
            this.tfArgs.Name = "tfArgs";
            this.tfArgs.Size = new System.Drawing.Size(162, 17);
            this.tfArgs.TabIndex = 8;
            this.tfArgs.Text = "Pass arguments as operands";
            this.tfArgs.UseVisualStyleBackColor = true;
            this.tfArgs.CheckedChanged += new System.EventHandler(this.tfArgs_CheckedChanged);
            // 
            // tfParams
            // 
            this.tfParams.AutoSize = true;
            this.tfParams.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tfParams.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tfParams.Location = new System.Drawing.Point(174, 0);
            this.tfParams.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.tfParams.Name = "tfParams";
            this.tfParams.Size = new System.Drawing.Size(221, 17);
            this.tfParams.TabIndex = 9;
            this.tfParams.Text = "Pass this BHAVs parameters as operands";
            this.tfParams.UseVisualStyleBackColor = true;
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(430, 240);
            this.Controls.Add(this.pnWiz0x001c);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UI";
            this.Text = "UI";
            this.pnWiz0x001c.ResumeLayout(false);
            this.pnWiz0x001c.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flpArgs.ResumeLayout(false);
            this.flpArgs.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        private void btnTreeName_Click(object sender, EventArgs e)
        {
            doStrChooser();
        }

        private void tfArgs_CheckedChanged(object sender, EventArgs e)
        {
            this.flpArgs.Enabled = this.tfArgs.Checked;
        }

        private void cbScope_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            this.lbTreeName.Text = ((BhavWiz)inst).readStr(this.Scope, GS.GlobalStr.NamedTree, (ushort)(doidTree.Value - 1), -1, pjse.Detail.ErrorNames);
        }

	}

}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWiz0x001c : pjse.ABhavOperandWiz
	{
		public BhavOperandWiz0x001c(Instruction i) : base(i) { myForm = new Wiz0x001c.UI(); }

		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm = null;
		}
		#endregion

	}

}
