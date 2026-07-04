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
 *   59 Temple Place - Suite 330, Boston, MA  32111-1307, USA.             *
 ***************************************************************************/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.PackedFiles.Wrapper;

namespace pjse.BhavOperandWizards.Wiz0x0032
{
    /// <summary>
    /// Summary description for StrBig.
    /// </summary>
    internal class UI : System.Windows.Forms.Form, iBhavOperandWizForm
	{
		#region Form variables

        internal System.Windows.Forms.Panel pnWiz0x0032;
        private RadioButton rbModeIcon;
        private RadioButton rbModeAction;
        private Panel pnAction;
        private Panel pnIcon;
        private ComboBox cbScope;
        private Label label1;
        private Label lbDisabled;
        private ComboBox cbDisabled;
        private Label label3;
        private Label label4;
        private Panel pnStrIndex;
        private Label label5;
        private Button btnActionString;
        private TextBox tbStrIndex;
        private Label lbActionString;
        private CheckBox tfActionTemp;
        private CheckBox tfIconTemp;
        private Panel pnIconIndex;
        private Label label6;
        private TextBox tbIconIndex;
        private Panel pnThumbnail;
        private CheckBox tfGUIDTemp;
        private Panel pnGUID;
        private Label label8;
        private TextBox tbGUID;
        private Label label7;
        private RadioButton rbIconSourceObj;
        private RadioButton rbIconSourceTN;
        private Label label10;
        private Panel pnObject;
        private Label label9;
        private ComboBox cbPicker1;
        private TextBox tbVal1;
        private ComboBox cbDataOwner1;
        private CheckBox cbAttrPicker;
        private CheckBox cbDecimal;
        private CheckBox tfSubQ;
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
		private DataOwnerControl doid1 = null;
        private bool internalchg = false;

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
            pjse.FileTable.Entry[] items = pjse.FileTable.GFT[(uint)SimPe.Data.MetaData.STRING_FILE, inst.Parent.GroupForScope(this.Scope), (uint)GS.GlobalStr.MakeAction];

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
                this.tbStrIndex.Text = "0x" + SimPe.Helper.HexString((byte)(i+1));
                this.lbActionString.Text = ((BhavWiz)inst).readStr(this.Scope, GS.GlobalStr.MakeAction, (ushort)i, -1, pjse.Detail.ErrorNames);
            }
        }

        private bool hex8_IsValid(object sender)
        {
            try { Convert.ToByte(((TextBox)sender).Text, 16); }
            catch (Exception) { return false; }
            return true;
        }

        private bool hex16_IsValid(object sender)
        {
            try { Convert.ToUInt16(((TextBox)sender).Text, 16); }
            catch (Exception) { return false; }
            return true;
        }

        private bool hex32_IsValid(object sender)
        {
            try { Convert.ToUInt32(((TextBox)sender).Text, 16); }
            catch (Exception) { return false; }
            return true;
        }



        #region iBhavOperandWizForm
        public Panel WizPanel { get { return this.pnWiz0x0032; } }

        public void Execute(Instruction inst)
        {
            this.inst = inst;

            wrappedByteArray ops1 = inst.Operands;
            wrappedByteArray ops2 = inst.Reserved1;

            internalchg = true;

            this.lbDisabled.Enabled = this.cbDisabled.Enabled = inst.NodeVersion != 0;
            this.tfSubQ.Enabled = inst.NodeVersion > 2;

            this.cbScope.SelectedIndex = -1;
            switch (ops1[0x02] & 0x0c)
            {
                case 0x00: this.cbScope.SelectedIndex = 0; break; // Private
                case 0x04: this.cbScope.SelectedIndex = 2; break; // Global
                case 0x08: this.cbScope.SelectedIndex = 1; break; // SemiGlobal
            }

            this.tfActionTemp.Checked = (ops1[0x02] & 0x10) != 0;
            this.pnStrIndex.Enabled = !this.tfActionTemp.Checked;

            this.pnThumbnail.Enabled = this.rbIconSourceTN.Checked = ((ops1[0x02] & 0x20) != 0);
            this.pnObject.Enabled = this.rbIconSourceObj.Checked = !this.rbIconSourceTN.Checked;

            this.tfGUIDTemp.Checked = ((ops1[0x02] & 0x40) != 0);
            this.pnGUID.Enabled = !this.tfGUIDTemp.Checked;

            this.tfIconTemp.Checked = (ops1[0x02] & 0x80) != 0;
            this.pnIconIndex.Enabled = !this.tfIconTemp.Checked;

            this.cbDisabled.SelectedIndex = -1;
            switch (ops1[0x03] & 0x03)
            {
                case 0x00: this.cbDisabled.SelectedIndex = 2; break;
                case 0x01: this.cbDisabled.SelectedIndex = 0; break;
                case 0x02: this.cbDisabled.SelectedIndex = 1; break;
            }
            this.tfSubQ.Checked = (ops1[0x03] & 0x10) != 0;

            int val = inst.NodeVersion < 2 ? ops1[0x04] : BhavWiz.ToShort(ops2[0x06], ops2[0x07]);
            this.tbStrIndex.Text = "0x" + SimPe.Helper.HexString((ushort)val);
            this.lbActionString.Text = ((BhavWiz)inst).readStr(this.Scope, GS.GlobalStr.MakeAction, (ushort)(val - 1), -1, pjse.Detail.ErrorNames);

            this.tbGUID.Text
                = "0x" + SimPe.Helper.HexString(ops1[0x05] | (ops1[0x06] << 8) | (ops1[0x07] << 16) | (ops2[0x00] << 24));

            this.pnAction.Enabled = this.rbModeAction.Checked = ops2[0x01] == 0;
            this.pnIcon.Enabled = this.rbModeIcon.Checked = !this.rbModeAction.Checked;

            this.tbIconIndex.Text = "0x" + SimPe.Helper.HexString(ops2[0x03]);

            doid1 = new DataOwnerControl(inst, this.cbDataOwner1, this.cbPicker1, this.tbVal1, this.cbDecimal, this.cbAttrPicker, null,
                ops2[0x03], BhavWiz.ToShort(ops2[0x04], ops2[0x05]));

            internalchg = false;
        }

		public Instruction Write(Instruction inst)
		{
			if (inst != null)
			{
                wrappedByteArray ops1 = inst.Operands;
                wrappedByteArray ops2 = inst.Reserved1;

                if (this.rbModeAction.Checked)
                {
                    ops2[0x01] = 0;

                    if (this.cbScope.SelectedIndex >= 0)
                    {
                        ops1[0x02] &= 0xf3;
                        if (this.cbScope.SelectedIndex == 2) ops1[0x02] |= 0x04;
                        if (this.cbScope.SelectedIndex == 1) ops1[0x02] |= 0x08;
                    }

                    ops1[0x02] &= 0xef;
                    if (this.tfActionTemp.Checked)
                        ops1[0x02] |= 0x10;
                    else
                    {
                        ushort val = Convert.ToUInt16(this.tbStrIndex.Text, 16);
                        if (inst.NodeVersion < 2)
                            ops1[0x04] = (byte)(val & 0xff);
                        else
                            BhavWiz.FromShort(ref ops2, 6, val);
                    }

                    if (inst.NodeVersion != 0 && this.cbDisabled.SelectedIndex != -1)
                    {
                        ops1[0x03] &= 0xfc;
                        if (this.cbDisabled.SelectedIndex == 0) ops1[0x03] |= 0x01;
                        else if (this.cbDisabled.SelectedIndex == 1) ops1[0x03] |= 0x02;
                    }
                    if (inst.NodeVersion > 2)
                    {
                        ops1[0x03] &= 0xef;
                        if (this.tfSubQ.Checked)
                            ops1[0x03] |= 0x10;
                    }

                }
                else
                {
                    if (ops2[0x01] == 0) ops2[0x01] = 1;

                    ops1[0x02] &= 0x7f;
                    if (this.tfIconTemp.Checked)
                        ops1[0x02] |= 0x80;
                    else
                        ops2[0x03] = Convert.ToByte(this.tbIconIndex.Text, 16);

                    ops1[0x02] &= 0xdf;
                    if (this.pnThumbnail.Enabled)
                    {
                        ops1[0x02] |= 0x20;

                        ops1[0x02] &= 0xbf;
                        if (this.tfGUIDTemp.Checked)
                            ops1[0x02] |= 0x40;
                        else
                        {
                            uint val = Convert.ToUInt32(this.tbGUID.Text, 16);
                            ops1[0x05] = (byte)(val & 0xff);
                            ops1[0x06] = (byte)((val >> 8) & 0xff);
                            ops1[0x07] = (byte)((val >> 16) & 0xff);
                            ops2[0x00] = (byte)((val >> 24) & 0xff);
                        }
                    }
                    else
                    {
                        ops2[0x03] = doid1.DataOwner;
                        BhavWiz.FromShort(ref ops2, 4, doid1.Value);
                    }
                }
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
            this.pnWiz0x0032 = new System.Windows.Forms.Panel();
            this.rbModeIcon = new System.Windows.Forms.RadioButton();
            this.rbModeAction = new System.Windows.Forms.RadioButton();
            this.pnAction = new System.Windows.Forms.Panel();
            this.tfSubQ = new System.Windows.Forms.CheckBox();
            this.pnStrIndex = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.btnActionString = new System.Windows.Forms.Button();
            this.tbStrIndex = new System.Windows.Forms.TextBox();
            this.lbActionString = new System.Windows.Forms.Label();
            this.tfActionTemp = new System.Windows.Forms.CheckBox();
            this.cbDisabled = new System.Windows.Forms.ComboBox();
            this.cbScope = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbDisabled = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnIcon = new System.Windows.Forms.Panel();
            this.pnObject = new System.Windows.Forms.Panel();
            this.cbAttrPicker = new System.Windows.Forms.CheckBox();
            this.cbDecimal = new System.Windows.Forms.CheckBox();
            this.cbPicker1 = new System.Windows.Forms.ComboBox();
            this.tbVal1 = new System.Windows.Forms.TextBox();
            this.cbDataOwner1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pnThumbnail = new System.Windows.Forms.Panel();
            this.tfGUIDTemp = new System.Windows.Forms.CheckBox();
            this.pnGUID = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.tbGUID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.rbIconSourceObj = new System.Windows.Forms.RadioButton();
            this.rbIconSourceTN = new System.Windows.Forms.RadioButton();
            this.tfIconTemp = new System.Windows.Forms.CheckBox();
            this.pnIconIndex = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.tbIconIndex = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnWiz0x0032.SuspendLayout();
            this.pnAction.SuspendLayout();
            this.pnStrIndex.SuspendLayout();
            this.pnIcon.SuspendLayout();
            this.pnObject.SuspendLayout();
            this.pnThumbnail.SuspendLayout();
            this.pnGUID.SuspendLayout();
            this.pnIconIndex.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnWiz0x0032
            // 
            this.pnWiz0x0032.Controls.Add(this.rbModeIcon);
            this.pnWiz0x0032.Controls.Add(this.rbModeAction);
            this.pnWiz0x0032.Controls.Add(this.pnAction);
            this.pnWiz0x0032.Controls.Add(this.pnIcon);
            this.pnWiz0x0032.Location = new System.Drawing.Point(0, 0);
            this.pnWiz0x0032.Margin = new System.Windows.Forms.Padding(2);
            this.pnWiz0x0032.Name = "pnWiz0x0032";
            this.pnWiz0x0032.Size = new System.Drawing.Size(485, 286);
            this.pnWiz0x0032.TabIndex = 0;
            // 
            // rbModeIcon
            // 
            this.rbModeIcon.AutoSize = true;
            this.rbModeIcon.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rbModeIcon.Location = new System.Drawing.Point(0, 104);
            this.rbModeIcon.Margin = new System.Windows.Forms.Padding(2);
            this.rbModeIcon.Name = "rbModeIcon";
            this.rbModeIcon.Size = new System.Drawing.Size(114, 17);
            this.rbModeIcon.TabIndex = 1;
            this.rbModeIcon.TabStop = true;
            this.rbModeIcon.Text = "Change icon mode";
            this.rbModeIcon.UseVisualStyleBackColor = true;
            this.rbModeIcon.CheckedChanged += new System.EventHandler(this.rbModeIcon_CheckedChanged);
            // 
            // rbModeAction
            // 
            this.rbModeAction.AutoSize = true;
            this.rbModeAction.Location = new System.Drawing.Point(0, 0);
            this.rbModeAction.Margin = new System.Windows.Forms.Padding(2);
            this.rbModeAction.Name = "rbModeAction";
            this.rbModeAction.Size = new System.Drawing.Size(175, 17);
            this.rbModeAction.TabIndex = 1;
            this.rbModeAction.TabStop = true;
            this.rbModeAction.Text = "Add/Change action string mode";
            this.rbModeAction.UseVisualStyleBackColor = true;
            this.rbModeAction.CheckedChanged += new System.EventHandler(this.rbModeAction_CheckedChanged);
            // 
            // pnAction
            // 
            this.pnAction.Controls.Add(this.tfSubQ);
            this.pnAction.Controls.Add(this.pnStrIndex);
            this.pnAction.Controls.Add(this.tfActionTemp);
            this.pnAction.Controls.Add(this.cbDisabled);
            this.pnAction.Controls.Add(this.cbScope);
            this.pnAction.Controls.Add(this.label3);
            this.pnAction.Controls.Add(this.lbDisabled);
            this.pnAction.Controls.Add(this.label1);
            this.pnAction.Location = new System.Drawing.Point(19, 19);
            this.pnAction.Margin = new System.Windows.Forms.Padding(2);
            this.pnAction.Name = "pnAction";
            this.pnAction.Size = new System.Drawing.Size(466, 81);
            this.pnAction.TabIndex = 2;
            // 
            // tfSubQ
            // 
            this.tfSubQ.AutoSize = true;
            this.tfSubQ.Location = new System.Drawing.Point(242, 21);
            this.tfSubQ.Margin = new System.Windows.Forms.Padding(2);
            this.tfSubQ.Name = "tfSubQ";
            this.tfSubQ.Size = new System.Drawing.Size(94, 17);
            this.tfSubQ.TabIndex = 5;
            this.tfSubQ.Text = "To Sub-queue";
            this.tfSubQ.UseVisualStyleBackColor = true;
            // 
            // pnStrIndex
            // 
            this.pnStrIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnStrIndex.Controls.Add(this.label5);
            this.pnStrIndex.Controls.Add(this.btnActionString);
            this.pnStrIndex.Controls.Add(this.tbStrIndex);
            this.pnStrIndex.Controls.Add(this.lbActionString);
            this.pnStrIndex.Location = new System.Drawing.Point(74, 60);
            this.pnStrIndex.Margin = new System.Windows.Forms.Padding(2);
            this.pnStrIndex.Name = "pnStrIndex";
            this.pnStrIndex.Size = new System.Drawing.Size(392, 21);
            this.pnStrIndex.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(0, 2);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "index";
            // 
            // btnActionString
            // 
            this.btnActionString.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnActionString.Font = new System.Drawing.Font("Webdings", 12F, System.Drawing.FontStyle.Bold);
            this.btnActionString.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnActionString.Location = new System.Drawing.Point(90, 0);
            this.btnActionString.Margin = new System.Windows.Forms.Padding(2);
            this.btnActionString.Name = "btnActionString";
            this.btnActionString.Size = new System.Drawing.Size(20, 19);
            this.btnActionString.TabIndex = 2;
            this.btnActionString.Text = "8";
            this.btnActionString.Click += new System.EventHandler(this.btnActionString_Click);
            // 
            // tbStrIndex
            // 
            this.tbStrIndex.Location = new System.Drawing.Point(38, 0);
            this.tbStrIndex.Margin = new System.Windows.Forms.Padding(2);
            this.tbStrIndex.Name = "tbStrIndex";
            this.tbStrIndex.Size = new System.Drawing.Size(54, 20);
            this.tbStrIndex.TabIndex = 1;
            this.tbStrIndex.Text = "0xDDDD";
            this.tbStrIndex.TextChanged += new System.EventHandler(this.hex16_TextChanged);
            this.tbStrIndex.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbStrIndex.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // lbActionString
            // 
            this.lbActionString.AutoSize = true;
            this.lbActionString.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbActionString.Location = new System.Drawing.Point(110, 2);
            this.lbActionString.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbActionString.Name = "lbActionString";
            this.lbActionString.Size = new System.Drawing.Size(65, 13);
            this.lbActionString.TabIndex = 13;
            this.lbActionString.Text = "Action string";
            // 
            // tfActionTemp
            // 
            this.tfActionTemp.AutoSize = true;
            this.tfActionTemp.Location = new System.Drawing.Point(74, 40);
            this.tfActionTemp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tfActionTemp.Name = "tfActionTemp";
            this.tfActionTemp.Size = new System.Drawing.Size(101, 17);
            this.tfActionTemp.TabIndex = 3;
            this.tfActionTemp.Text = "index in Temp 0";
            this.tfActionTemp.UseVisualStyleBackColor = true;
            this.tfActionTemp.CheckedChanged += new System.EventHandler(this.tfActionTemp_CheckedChanged);
            // 
            // cbDisabled
            // 
            this.cbDisabled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDisabled.FormattingEnabled = true;
            this.cbDisabled.Items.AddRange(new object[] {
            "Propagating",
            "Non-propagating",
            "False"});
            this.cbDisabled.Location = new System.Drawing.Point(74, 19);
            this.cbDisabled.Margin = new System.Windows.Forms.Padding(2);
            this.cbDisabled.Name = "cbDisabled";
            this.cbDisabled.Size = new System.Drawing.Size(127, 21);
            this.cbDisabled.TabIndex = 2;
            // 
            // cbScope
            // 
            this.cbScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScope.FormattingEnabled = true;
            this.cbScope.Items.AddRange(new object[] {
            "Private",
            "SemiGlobal",
            "Global"});
            this.cbScope.Location = new System.Drawing.Point(74, 0);
            this.cbScope.Margin = new System.Windows.Forms.Padding(2);
            this.cbScope.Name = "cbScope";
            this.cbScope.Size = new System.Drawing.Size(127, 21);
            this.cbScope.TabIndex = 1;
            this.cbScope.SelectedIndexChanged += new System.EventHandler(this.cbScope_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(0, 39);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Action string";
            // 
            // lbDisabled
            // 
            this.lbDisabled.AutoSize = true;
            this.lbDisabled.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbDisabled.Location = new System.Drawing.Point(18, 22);
            this.lbDisabled.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDisabled.Name = "lbDisabled";
            this.lbDisabled.Size = new System.Drawing.Size(48, 13);
            this.lbDisabled.TabIndex = 4;
            this.lbDisabled.Text = "Disabled";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(30, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Scope";
            // 
            // pnIcon
            // 
            this.pnIcon.Controls.Add(this.pnObject);
            this.pnIcon.Controls.Add(this.pnThumbnail);
            this.pnIcon.Controls.Add(this.rbIconSourceObj);
            this.pnIcon.Controls.Add(this.rbIconSourceTN);
            this.pnIcon.Controls.Add(this.tfIconTemp);
            this.pnIcon.Controls.Add(this.pnIconIndex);
            this.pnIcon.Controls.Add(this.label10);
            this.pnIcon.Controls.Add(this.label4);
            this.pnIcon.Location = new System.Drawing.Point(19, 124);
            this.pnIcon.Margin = new System.Windows.Forms.Padding(2);
            this.pnIcon.Name = "pnIcon";
            this.pnIcon.Size = new System.Drawing.Size(466, 162);
            this.pnIcon.TabIndex = 2;
            // 
            // pnObject
            // 
            this.pnObject.Controls.Add(this.cbAttrPicker);
            this.pnObject.Controls.Add(this.cbDecimal);
            this.pnObject.Controls.Add(this.cbPicker1);
            this.pnObject.Controls.Add(this.tbVal1);
            this.pnObject.Controls.Add(this.cbDataOwner1);
            this.pnObject.Controls.Add(this.label9);
            this.pnObject.Location = new System.Drawing.Point(74, 115);
            this.pnObject.Margin = new System.Windows.Forms.Padding(2);
            this.pnObject.Name = "pnObject";
            this.pnObject.Size = new System.Drawing.Size(392, 45);
            this.pnObject.TabIndex = 4;
            // 
            // cbAttrPicker
            // 
            this.cbAttrPicker.AutoSize = true;
            this.cbAttrPicker.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbAttrPicker.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbAttrPicker.Location = new System.Drawing.Point(259, 26);
            this.cbAttrPicker.Margin = new System.Windows.Forms.Padding(2);
            this.cbAttrPicker.Name = "cbAttrPicker";
            this.cbAttrPicker.Size = new System.Drawing.Size(117, 17);
            this.cbAttrPicker.TabIndex = 4;
            this.cbAttrPicker.Text = "use Attribute picker";
            // 
            // cbDecimal
            // 
            this.cbDecimal.AutoSize = true;
            this.cbDecimal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDecimal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbDecimal.Location = new System.Drawing.Point(95, 26);
            this.cbDecimal.Margin = new System.Windows.Forms.Padding(2);
            this.cbDecimal.Name = "cbDecimal";
            this.cbDecimal.Size = new System.Drawing.Size(140, 17);
            this.cbDecimal.TabIndex = 3;
            this.cbDecimal.Text = "Decimal (except Consts)";
            // 
            // cbPicker1
            // 
            this.cbPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker1.DropDownWidth = 384;
            this.cbPicker1.Location = new System.Drawing.Point(264, 0);
            this.cbPicker1.Margin = new System.Windows.Forms.Padding(2);
            this.cbPicker1.Name = "cbPicker1";
            this.cbPicker1.Size = new System.Drawing.Size(128, 21);
            this.cbPicker1.TabIndex = 2;
            this.cbPicker1.Visible = false;
            // 
            // tbVal1
            // 
            this.tbVal1.Location = new System.Drawing.Point(264, 0);
            this.tbVal1.Margin = new System.Windows.Forms.Padding(2);
            this.tbVal1.Name = "tbVal1";
            this.tbVal1.Size = new System.Drawing.Size(108, 20);
            this.tbVal1.TabIndex = 2;
            // 
            // cbDataOwner1
            // 
            this.cbDataOwner1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner1.DropDownWidth = 384;
            this.cbDataOwner1.Location = new System.Drawing.Point(22, 0);
            this.cbDataOwner1.Margin = new System.Windows.Forms.Padding(2);
            this.cbDataOwner1.Name = "cbDataOwner1";
            this.cbDataOwner1.Size = new System.Drawing.Size(242, 21);
            this.cbDataOwner1.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(0, 2);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "ID";
            // 
            // pnThumbnail
            // 
            this.pnThumbnail.Controls.Add(this.tfGUIDTemp);
            this.pnThumbnail.Controls.Add(this.pnGUID);
            this.pnThumbnail.Controls.Add(this.label7);
            this.pnThumbnail.Location = new System.Drawing.Point(74, 58);
            this.pnThumbnail.Margin = new System.Windows.Forms.Padding(2);
            this.pnThumbnail.Name = "pnThumbnail";
            this.pnThumbnail.Size = new System.Drawing.Size(209, 38);
            this.pnThumbnail.TabIndex = 4;
            // 
            // tfGUIDTemp
            // 
            this.tfGUIDTemp.AutoSize = true;
            this.tfGUIDTemp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tfGUIDTemp.Location = new System.Drawing.Point(92, 0);
            this.tfGUIDTemp.Margin = new System.Windows.Forms.Padding(2);
            this.tfGUIDTemp.Name = "tfGUIDTemp";
            this.tfGUIDTemp.Size = new System.Drawing.Size(115, 17);
            this.tfGUIDTemp.TabIndex = 1;
            this.tfGUIDTemp.Text = "GUID in Temp 2, 3";
            this.tfGUIDTemp.UseVisualStyleBackColor = true;
            this.tfGUIDTemp.CheckedChanged += new System.EventHandler(this.tfGUIDTemp_CheckedChanged);
            // 
            // pnGUID
            // 
            this.pnGUID.Controls.Add(this.label8);
            this.pnGUID.Controls.Add(this.tbGUID);
            this.pnGUID.Location = new System.Drawing.Point(92, 19);
            this.pnGUID.Margin = new System.Windows.Forms.Padding(2);
            this.pnGUID.Name = "pnGUID";
            this.pnGUID.Size = new System.Drawing.Size(117, 19);
            this.pnGUID.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(0, 2);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "GUID";
            // 
            // tbGUID
            // 
            this.tbGUID.Location = new System.Drawing.Point(36, 0);
            this.tbGUID.Margin = new System.Windows.Forms.Padding(2);
            this.tbGUID.Name = "tbGUID";
            this.tbGUID.Size = new System.Drawing.Size(81, 20);
            this.tbGUID.TabIndex = 1;
            this.tbGUID.Text = "0xDDDDDDDD";
            this.tbGUID.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbGUID.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(0, 1);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Thumbnail outfit";
            // 
            // rbIconSourceObj
            // 
            this.rbIconSourceObj.AutoSize = true;
            this.rbIconSourceObj.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rbIconSourceObj.Location = new System.Drawing.Point(74, 96);
            this.rbIconSourceObj.Margin = new System.Windows.Forms.Padding(2);
            this.rbIconSourceObj.Name = "rbIconSourceObj";
            this.rbIconSourceObj.Size = new System.Drawing.Size(56, 17);
            this.rbIconSourceObj.TabIndex = 3;
            this.rbIconSourceObj.TabStop = true;
            this.rbIconSourceObj.Text = "Object";
            this.rbIconSourceObj.UseVisualStyleBackColor = true;
            this.rbIconSourceObj.CheckedChanged += new System.EventHandler(this.rbIconSourceObj_CheckedChanged);
            // 
            // rbIconSourceTN
            // 
            this.rbIconSourceTN.AutoSize = true;
            this.rbIconSourceTN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rbIconSourceTN.Location = new System.Drawing.Point(74, 38);
            this.rbIconSourceTN.Margin = new System.Windows.Forms.Padding(2);
            this.rbIconSourceTN.Name = "rbIconSourceTN";
            this.rbIconSourceTN.Size = new System.Drawing.Size(74, 17);
            this.rbIconSourceTN.TabIndex = 3;
            this.rbIconSourceTN.TabStop = true;
            this.rbIconSourceTN.Text = "Thumbnail";
            this.rbIconSourceTN.UseVisualStyleBackColor = true;
            this.rbIconSourceTN.CheckedChanged += new System.EventHandler(this.rbIconSourceTN_CheckedChanged);
            // 
            // tfIconTemp
            // 
            this.tfIconTemp.AutoSize = true;
            this.tfIconTemp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tfIconTemp.Location = new System.Drawing.Point(74, 0);
            this.tfIconTemp.Margin = new System.Windows.Forms.Padding(2);
            this.tfIconTemp.Name = "tfIconTemp";
            this.tfIconTemp.Size = new System.Drawing.Size(101, 17);
            this.tfIconTemp.TabIndex = 1;
            this.tfIconTemp.Text = "index in Temp 0";
            this.tfIconTemp.UseVisualStyleBackColor = true;
            this.tfIconTemp.CheckedChanged += new System.EventHandler(this.tfIconTemp_CheckedChanged);
            // 
            // pnIconIndex
            // 
            this.pnIconIndex.Controls.Add(this.label6);
            this.pnIconIndex.Controls.Add(this.tbIconIndex);
            this.pnIconIndex.Location = new System.Drawing.Point(74, 19);
            this.pnIconIndex.Margin = new System.Windows.Forms.Padding(2);
            this.pnIconIndex.Name = "pnIconIndex";
            this.pnIconIndex.Size = new System.Drawing.Size(82, 19);
            this.pnIconIndex.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(4, 2);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "index";
            // 
            // tbIconIndex
            // 
            this.tbIconIndex.Location = new System.Drawing.Point(42, 0);
            this.tbIconIndex.Margin = new System.Windows.Forms.Padding(2);
            this.tbIconIndex.Name = "tbIconIndex";
            this.tbIconIndex.Size = new System.Drawing.Size(39, 20);
            this.tbIconIndex.TabIndex = 1;
            this.tbIconIndex.Text = "0xDD";
            this.tbIconIndex.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbIconIndex.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(4, 40);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Icon source";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(42, 1);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Icon";
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(495, 298);
            this.Controls.Add(this.pnWiz0x0032);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UI";
            this.Text = "UI";
            this.pnWiz0x0032.ResumeLayout(false);
            this.pnWiz0x0032.PerformLayout();
            this.pnAction.ResumeLayout(false);
            this.pnAction.PerformLayout();
            this.pnStrIndex.ResumeLayout(false);
            this.pnStrIndex.PerformLayout();
            this.pnIcon.ResumeLayout(false);
            this.pnIcon.PerformLayout();
            this.pnObject.ResumeLayout(false);
            this.pnObject.PerformLayout();
            this.pnThumbnail.ResumeLayout(false);
            this.pnThumbnail.PerformLayout();
            this.pnGUID.ResumeLayout(false);
            this.pnGUID.PerformLayout();
            this.pnIconIndex.ResumeLayout(false);
            this.pnIconIndex.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        private void hex8_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (hex8_IsValid(sender)) return;

            e.Cancel = true;

            bool origstate = internalchg;
            internalchg = true;
            ((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(inst.Reserved1[0x03]);
            ((TextBox)sender).SelectAll();
            internalchg = origstate;
        }

        private void hex8_Validated(object sender, System.EventArgs e)
        {
            bool origstate = internalchg;
            internalchg = true;
            ((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(Convert.ToByte(((TextBox)sender).Text, 16));
            ((TextBox)sender).SelectAll();
            internalchg = origstate;
        }

        private void hex16_TextChanged(object sender, System.EventArgs ev)
        {
            if (internalchg) return;
            if (inst.NodeVersion < 2 && !hex8_IsValid(sender)) return;
            else if (!hex16_IsValid(sender)) return;

            ushort val = Convert.ToUInt16(((TextBox)sender).Text, 16);
            this.lbActionString.Text = ((BhavWiz)inst).readStr(this.Scope, GS.GlobalStr.MakeAction, (ushort)(val - 1), -1, pjse.Detail.ErrorNames);
        }

        private void hex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (inst.NodeVersion < 2 && hex8_IsValid(sender)) return;
            else if (hex16_IsValid(sender)) return;

            e.Cancel = true;

            bool origstate = internalchg;
            internalchg = true;
            ushort val = inst.NodeVersion < 2 ? inst.Operands[0x04] : BhavWiz.ToShort(inst.Reserved1[0x06], inst.Reserved1[0x07]);
            this.lbActionString.Text = ((BhavWiz)inst).readStr(this.Scope, GS.GlobalStr.MakeAction, (ushort)(val - 1), -1, pjse.Detail.ErrorNames);
            ((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(val);
            ((TextBox)sender).SelectAll();
            internalchg = origstate;
        }

        private void hex16_Validated(object sender, System.EventArgs e)
        {
            bool origstate = internalchg;
            internalchg = true;
            ((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(Convert.ToUInt16(((TextBox)sender).Text, 16));
            ((TextBox)sender).SelectAll();
            internalchg = origstate;
        }

        private void hex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (hex32_IsValid(sender)) return;

            e.Cancel = true;

            bool origstate = internalchg;
            internalchg = true;
            ((TextBox)sender).Text
               = "0x" + SimPe.Helper.HexString(inst.Operands[0x05] | (inst.Operands[0x06] << 8) | (inst.Operands[0x07] << 16) | (inst.Reserved1[0x00] << 24));
            ((TextBox)sender).SelectAll();
            internalchg = origstate;
        }

        private void hex32_Validated(object sender, System.EventArgs e)
        {
            bool origstate = internalchg;
            internalchg = true;
            ((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(Convert.ToUInt32(((TextBox)sender).Text, 16));
            ((TextBox)sender).SelectAll();
            internalchg = origstate;
        }

        private void cbScope_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            this.lbActionString.Text = ((BhavWiz)inst).readStr(this.Scope, GS.GlobalStr.MakeAction, (ushort)(Convert.ToByte(this.tbStrIndex.Text, 16) - 1), -1, pjse.Detail.ErrorNames);
        }

        private void tfActionTemp_CheckedChanged(object sender, EventArgs e)
        {
            this.pnStrIndex.Enabled = !((CheckBox)sender).Checked;
        }

        private void tfIconTemp_CheckedChanged(object sender, EventArgs e)
        {
            this.pnIconIndex.Enabled = !((CheckBox)sender).Checked;
        }

        private void rbModeAction_CheckedChanged(object sender, EventArgs e)
        {
            this.pnAction.Enabled = ((RadioButton)sender).Checked;
        }

        private void rbModeIcon_CheckedChanged(object sender, EventArgs e)
        {
            this.pnIcon.Enabled = ((RadioButton)sender).Checked;
        }

        private void rbIconSourceTN_CheckedChanged(object sender, EventArgs e)
        {
            this.pnThumbnail.Enabled = ((RadioButton)sender).Checked;
        }

        private void rbIconSourceObj_CheckedChanged(object sender, EventArgs e)
        {
            this.pnObject.Enabled = ((RadioButton)sender).Checked;
        }

        private void tfGUIDTemp_CheckedChanged(object sender, EventArgs e)
        {
            this.pnGUID.Enabled = !((CheckBox)sender).Checked;
        }

        private void btnActionString_Click(object sender, EventArgs e)
        {
            doStrChooser();
        }

	}

}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWiz0x0032 : pjse.ABhavOperandWiz
	{
		public BhavOperandWiz0x0032(Instruction i) : base(i) { myForm = new Wiz0x0032.UI(); }

		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm = null;
		}
		#endregion

	}

}
