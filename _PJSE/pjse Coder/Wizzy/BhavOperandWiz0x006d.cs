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

namespace pjse.BhavOperandWizards.Wiz0x006d
{
    /// <summary>
    /// Summary description for StrBig.
    /// </summary>
    internal class UI : System.Windows.Forms.Form, iBhavOperandWizForm
    {
        #region Form variables

        internal System.Windows.Forms.Panel pnWiz0x006d;
        private Label label1;
        private Panel pnMaterial;
        private Label label3;
        private ComboBox cbPicker1;
        private CheckBox cbDecimal;
        private TextBox tbVal1;
        private CheckBox cbAttrPicker;
        private ComboBox cbDataOwner1;
        private RadioButton rb1Object;
        private RadioButton rb1Me;
        private RadioButton rb1ScrShot;
        private Panel pnNotScrShot;
        private CheckBox ckbMaterialTemp;
        private RadioButton rb2MovingTexture;
        private RadioButton rb2Material;
        private Label label5;
        private TextBox tbVal3;
        private ComboBox cbMatScope;
        private Label label6;
        private Button btnMaterial;
        private TextBox tbMaterial;
        private Panel panel1;
        private Label label2;
        private RadioButton rb3Object;
        private RadioButton rb3Me;
        private Panel pnNotAllOver;
        private CheckBox ckbAllOver;
        private ComboBox cbMeshScope;
        private Label label4;
        private Label label7;
        private TextBox tbMesh;
        private Button btnMesh;
        private TextBox tbVal5;
        private Label label8;
        private CheckBox ckbMeshTemp;
        private Label label9;
        private ComboBox cbPicker2;
        private TextBox tbVal2;
        private ComboBox cbDataOwner2;
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

            rb1group = new ArrayList(new Control[] { this.rb1ScrShot, this.rb1Me, this.rb1Object });
            rb3group = new ArrayList(new Control[] { this.rb3Me, this.rb3Object });
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);

            inst = null;
        }

        private Instruction inst = null;

        private DataOwnerControl doid1 = null;
        private DataOwnerControl doid2 = null;
        private DataOwnerControl doid3 = null;
        private DataOwnerControl doid5 = null;

        ArrayList rb1group = null;
        ArrayList rb3group = null;
        private bool internalchg = false;

        void doid3_DataOwnerControlChanged(object sender, EventArgs e)
        {
            doStrValue(cbMatScope, GS.GlobalStr.MaterialName, doid3.Value, tbMaterial);
        }

        void doid5_DataOwnerControlChanged(object sender, EventArgs e)
        {
            doStrValue(cbMeshScope, GS.GlobalStr.MeshGroup, doid5.Value, tbMesh);
        }

        private void doStrChooser(ComboBox scope, pjse.GS.GlobalStr instance, TextBox tbVal, TextBox strText)
        {
            Scope[] s = { Scope.Private, Scope.SemiGlobal, Scope.Global };
            pjse.FileTable.Entry[] items = (scope.SelectedIndex < 0) ? null :
                pjse.FileTable.GFT[(uint)SimPe.Data.MetaData.STRING_FILE, inst.Parent.GroupForScope(s[scope.SelectedIndex]), (uint)instance];

            if (items == null || items.Length == 0)
            {
                MessageBox.Show(pjse.Localization.GetString("bow_noStrings")
                    + " (" + pjse.Localization.GetString(s[scope.SelectedIndex].ToString()) + ")");
                return; // eek!
            }

            SimPe.PackedFiles.Wrapper.StrWrapper str = new StrWrapper();
            str.ProcessData(items[0].PFD, items[0].Package);

            int i = (new StrChooser(true)).Strnum(str);
            if (i >= 0)
            {
                bool savedState = internalchg;
                internalchg = true;
                tbVal.Text = "0x" + SimPe.Helper.HexString((ushort)i);
                doStrValue(scope, instance, (ushort)i, strText);
                internalchg = savedState;
            }
        }

        private void doStrValue(ComboBox scope, pjse.GS.GlobalStr instance, ushort strno, TextBox strText)
        {
            Scope[] s = { Scope.Private, Scope.Global, Scope.SemiGlobal };
            strText.Text = (scope.SelectedIndex < 0) ? "" :
                ((BhavWiz)inst).readStr(s[scope.SelectedIndex], instance, strno, -1, pjse.Detail.ErrorNames);
        }

        private void MaterialFrom()
        {
            this.pnNotScrShot.Enabled = !this.rb1ScrShot.Checked;
            this.tbVal3.Enabled = !this.ckbMaterialTemp.Checked;
            this.btnMaterial.Enabled = this.tbMaterial.Visible = this.rb1Me.Checked && !this.ckbMaterialTemp.Checked;
        }

        private void MeshFrom()
        {
            this.pnNotAllOver.Enabled = !this.ckbAllOver.Checked;
            this.tbVal5.Enabled = !this.ckbMeshTemp.Checked;
            this.btnMesh.Enabled = this.tbMesh.Visible = !this.ckbAllOver.Checked && this.rb3Me.Checked && !this.ckbMeshTemp.Checked;
        }

        #region iBhavOperandWizForm
        public Panel WizPanel { get { return this.pnWiz0x006d; } }

        public void Execute(Instruction inst)
        {
            this.inst = inst;

            wrappedByteArray ops1 = inst.Operands;
            wrappedByteArray ops2 = inst.Reserved1;

            internalchg = true;

            doid3 = new DataOwnerControl(inst, null, null, this.tbVal3, this.cbDecimal, this.cbAttrPicker, null,
                0x07, BhavWiz.ToShort(ops1[0x00], ops1[0x01]));

            this.rb3Object.Checked = ((ops1[0x02] & 0x01) != 0);
            this.btnMesh.Visible = this.tbMesh.Visible = this.rb3Me.Checked = !this.rb3Object.Checked;

            this.cbMatScope.SelectedIndex = -1;
            switch (ops1[0x02] & 0x06)
            {
                case 0x00: this.cbMatScope.SelectedIndex = 0; break; // Private
                case 0x02: this.cbMatScope.SelectedIndex = 2; break; // Global
                case 0x04: this.cbMatScope.SelectedIndex = 1; break; // SemiGlobal
            }

            this.rb1ScrShot.Checked = ((ops2[0x05] & 0x02) != 0);
            this.rb1Me.Checked = !this.rb1ScrShot.Checked && ((ops1[0x02] & 0x08) == 0);
            this.rb1Object.Checked = !this.rb1ScrShot.Checked && !this.rb1Me.Checked;

            this.rb2MovingTexture.Checked = ((ops2[0x05] & 0x01) != 0);
            this.rb2Material.Checked = !this.rb2MovingTexture.Checked;

            this.ckbMaterialTemp.Checked = ((ops1[0x02] & 0x10) != 0);
            this.ckbMeshTemp.Checked     = ((ops1[0x02] & 0x20) != 0);

            this.cbMeshScope.SelectedIndex = -1;
            switch (ops1[0x02] & 0xc0)
            {
                case 0x00: this.cbMeshScope.SelectedIndex = 0; break; // Private
                case 0x40: this.cbMeshScope.SelectedIndex = 2; break; // Global
                case 0x80: this.cbMeshScope.SelectedIndex = 1; break; // SemiGlobal
            }

            doid5 = new DataOwnerControl(inst, null, null, this.tbVal5, this.cbDecimal, this.cbAttrPicker, null,
                0x07, (ushort)(BhavWiz.ToShort(ops1[0x03], ops1[0x04]) & 0x7fff));
            this.ckbAllOver.Checked = (ops1[0x04] & 0x80) != 0;

            doid1 = new DataOwnerControl(inst, this.cbDataOwner1, this.cbPicker1, this.tbVal1, this.cbDecimal, this.cbAttrPicker, null,
                ops1[0x05], BhavWiz.ToShort(ops1[0x06], ops1[0x07]));
            doid2 = new DataOwnerControl(inst, this.cbDataOwner2, this.cbPicker2, this.tbVal2, this.cbDecimal, this.cbAttrPicker, null,
                ops2[0x00], BhavWiz.ToShort(ops2[0x01], ops2[0x02]));

            doid3.DataOwnerControlChanged += new EventHandler(doid3_DataOwnerControlChanged);
            doid3_DataOwnerControlChanged(null, null);
            doid5.DataOwnerControlChanged += new EventHandler(doid5_DataOwnerControlChanged);
            doid5_DataOwnerControlChanged(null, null);

            internalchg = false;

            this.MaterialFrom();
            this.MeshFrom();
        }

        public Instruction Write(Instruction inst)
        {
            if (inst != null)
            {
                wrappedByteArray ops1 = inst.Operands;
                wrappedByteArray ops2 = inst.Reserved1;

                BhavWiz.FromShort(ref ops1, 0, doid3.Value);

                ops1[0x02] = 0x00;
                ops1[0x02] |= (byte)(this.rb3Object.Checked ? 0x01 : 0x00);
                switch (this.cbMatScope.SelectedIndex)
                {
                    case 2: ops1[0x02] |= 0x02; break; // Global
                    case 1: ops1[0x02] |= 0x04; break; // SemiGlobal
                }
                ops1[0x02] |= (byte)(this.rb1Object.Checked ? 0x08 : 0x00);
                ops1[0x02] |= (byte)(this.ckbMaterialTemp.Checked ? 0x10 : 0x00);
                ops1[0x02] |= (byte)(this.ckbMeshTemp.Checked ? 0x20 : 0x00);
                switch (this.cbMeshScope.SelectedIndex)
                {
                    case 2: ops1[0x02] |= 0x40; break; // Global
                    case 1: ops1[0x02] |= 0x80; break; // SemiGlobal
                }

                BhavWiz.FromShort(ref ops1, 3, (ushort)(doid5.Value & 0x7fff));
                ops1[0x04] |= (byte)(this.ckbAllOver.Checked ? 0x80 : 0x00);

                ops1[0x05] = doid1.DataOwner;
                BhavWiz.FromShort(ref ops1, 6, doid1.Value);

                ops2[0x00] = doid2.DataOwner;
                BhavWiz.FromShort(ref ops2, 1, doid2.Value);

                ops2[0x05] &= 0xfc;
                ops2[0x05] |= (byte)(this.rb2MovingTexture.Checked ? 0x01 : 0x00);
                ops2[0x05] |= (byte)(this.rb1ScrShot.Checked ? 0x02 : 0x00);
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
            this.pnWiz0x006d = new System.Windows.Forms.Panel();
            this.cbPicker2 = new System.Windows.Forms.ComboBox();
            this.cbAttrPicker = new System.Windows.Forms.CheckBox();
            this.cbDecimal = new System.Windows.Forms.CheckBox();
            this.tbVal2 = new System.Windows.Forms.TextBox();
            this.cbDataOwner2 = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnNotAllOver = new System.Windows.Forms.Panel();
            this.tbMesh = new System.Windows.Forms.TextBox();
            this.btnMesh = new System.Windows.Forms.Button();
            this.tbVal5 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ckbMeshTemp = new System.Windows.Forms.CheckBox();
            this.cbMeshScope = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ckbAllOver = new System.Windows.Forms.CheckBox();
            this.rb3Object = new System.Windows.Forms.RadioButton();
            this.rb3Me = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cbPicker1 = new System.Windows.Forms.ComboBox();
            this.tbVal1 = new System.Windows.Forms.TextBox();
            this.cbDataOwner1 = new System.Windows.Forms.ComboBox();
            this.pnMaterial = new System.Windows.Forms.Panel();
            this.pnNotScrShot = new System.Windows.Forms.Panel();
            this.tbMaterial = new System.Windows.Forms.TextBox();
            this.btnMaterial = new System.Windows.Forms.Button();
            this.tbVal3 = new System.Windows.Forms.TextBox();
            this.cbMatScope = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ckbMaterialTemp = new System.Windows.Forms.CheckBox();
            this.rb2MovingTexture = new System.Windows.Forms.RadioButton();
            this.rb2Material = new System.Windows.Forms.RadioButton();
            this.rb1Object = new System.Windows.Forms.RadioButton();
            this.rb1Me = new System.Windows.Forms.RadioButton();
            this.rb1ScrShot = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnWiz0x006d.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnNotAllOver.SuspendLayout();
            this.pnMaterial.SuspendLayout();
            this.pnNotScrShot.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnWiz0x006d
            // 
            this.pnWiz0x006d.Controls.Add(this.cbPicker2);
            this.pnWiz0x006d.Controls.Add(this.cbAttrPicker);
            this.pnWiz0x006d.Controls.Add(this.cbDecimal);
            this.pnWiz0x006d.Controls.Add(this.tbVal2);
            this.pnWiz0x006d.Controls.Add(this.cbDataOwner2);
            this.pnWiz0x006d.Controls.Add(this.panel1);
            this.pnWiz0x006d.Controls.Add(this.cbPicker1);
            this.pnWiz0x006d.Controls.Add(this.tbVal1);
            this.pnWiz0x006d.Controls.Add(this.cbDataOwner1);
            this.pnWiz0x006d.Controls.Add(this.pnMaterial);
            this.pnWiz0x006d.Controls.Add(this.label9);
            this.pnWiz0x006d.Controls.Add(this.label1);
            this.pnWiz0x006d.Location = new System.Drawing.Point(0, 0);
            this.pnWiz0x006d.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnWiz0x006d.Name = "pnWiz0x006d";
            this.pnWiz0x006d.Size = new System.Drawing.Size(446, 288);
            this.pnWiz0x006d.TabIndex = 1;
            // 
            // cbPicker2
            // 
            this.cbPicker2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker2.DropDownWidth = 384;
            this.cbPicker2.Location = new System.Drawing.Point(321, 19);
            this.cbPicker2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbPicker2.Name = "cbPicker2";
            this.cbPicker2.Size = new System.Drawing.Size(126, 21);
            this.cbPicker2.TabIndex = 6;
            this.cbPicker2.Visible = false;
            // 
            // cbAttrPicker
            // 
            this.cbAttrPicker.AutoSize = true;
            this.cbAttrPicker.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbAttrPicker.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbAttrPicker.Location = new System.Drawing.Point(321, 38);
            this.cbAttrPicker.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbAttrPicker.Name = "cbAttrPicker";
            this.cbAttrPicker.Size = new System.Drawing.Size(117, 17);
            this.cbAttrPicker.TabIndex = 8;
            this.cbAttrPicker.Text = "use Attribute picker";
            // 
            // cbDecimal
            // 
            this.cbDecimal.AutoSize = true;
            this.cbDecimal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDecimal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbDecimal.Location = new System.Drawing.Point(156, 38);
            this.cbDecimal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbDecimal.Name = "cbDecimal";
            this.cbDecimal.Size = new System.Drawing.Size(140, 17);
            this.cbDecimal.TabIndex = 7;
            this.cbDecimal.Text = "Decimal (except Consts)";
            // 
            // tbVal2
            // 
            this.tbVal2.Location = new System.Drawing.Point(321, 19);
            this.tbVal2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbVal2.Name = "tbVal2";
            this.tbVal2.Size = new System.Drawing.Size(108, 20);
            this.tbVal2.TabIndex = 6;
            // 
            // cbDataOwner2
            // 
            this.cbDataOwner2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner2.DropDownWidth = 384;
            this.cbDataOwner2.Location = new System.Drawing.Point(81, 19);
            this.cbDataOwner2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbDataOwner2.Name = "cbDataOwner2";
            this.cbDataOwner2.Size = new System.Drawing.Size(242, 21);
            this.cbDataOwner2.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.pnNotAllOver);
            this.panel1.Controls.Add(this.ckbAllOver);
            this.panel1.Controls.Add(this.rb3Object);
            this.panel1.Controls.Add(this.rb3Me);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(0, 173);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(446, 115);
            this.panel1.TabIndex = 10;
            // 
            // pnNotAllOver
            // 
            this.pnNotAllOver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnNotAllOver.Controls.Add(this.tbMesh);
            this.pnNotAllOver.Controls.Add(this.btnMesh);
            this.pnNotAllOver.Controls.Add(this.tbVal5);
            this.pnNotAllOver.Controls.Add(this.label8);
            this.pnNotAllOver.Controls.Add(this.ckbMeshTemp);
            this.pnNotAllOver.Controls.Add(this.cbMeshScope);
            this.pnNotAllOver.Controls.Add(this.label4);
            this.pnNotAllOver.Location = new System.Drawing.Point(38, 58);
            this.pnNotAllOver.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnNotAllOver.Name = "pnNotAllOver";
            this.pnNotAllOver.Size = new System.Drawing.Size(407, 58);
            this.pnNotAllOver.TabIndex = 5;
            // 
            // tbMesh
            // 
            this.tbMesh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMesh.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMesh.Location = new System.Drawing.Point(115, 42);
            this.tbMesh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbMesh.Name = "tbMesh";
            this.tbMesh.ReadOnly = true;
            this.tbMesh.Size = new System.Drawing.Size(292, 13);
            this.tbMesh.TabIndex = 7;
            this.tbMesh.TabStop = false;
            this.tbMesh.Text = "Mesh group name";
            // 
            // btnMesh
            // 
            this.btnMesh.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnMesh.Font = new System.Drawing.Font("Webdings", 12F, System.Drawing.FontStyle.Bold);
            this.btnMesh.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMesh.Location = new System.Drawing.Point(91, 38);
            this.btnMesh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnMesh.Name = "btnMesh";
            this.btnMesh.Size = new System.Drawing.Size(19, 18);
            this.btnMesh.TabIndex = 6;
            this.btnMesh.Text = "8";
            this.btnMesh.Click += new System.EventHandler(this.btnMesh_Click);
            // 
            // tbVal5
            // 
            this.tbVal5.Location = new System.Drawing.Point(43, 38);
            this.tbVal5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbVal5.Name = "tbVal5";
            this.tbVal5.Size = new System.Drawing.Size(49, 20);
            this.tbVal5.TabIndex = 5;
            this.tbVal5.Text = "0xDDDD";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(6, 20);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Index";
            // 
            // ckbMeshTemp
            // 
            this.ckbMeshTemp.AutoSize = true;
            this.ckbMeshTemp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMeshTemp.Location = new System.Drawing.Point(43, 19);
            this.ckbMeshTemp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbMeshTemp.Name = "ckbMeshTemp";
            this.ckbMeshTemp.Size = new System.Drawing.Size(74, 17);
            this.ckbMeshTemp.TabIndex = 4;
            this.ckbMeshTemp.Text = "In Temp 1";
            this.ckbMeshTemp.UseVisualStyleBackColor = true;
            this.ckbMeshTemp.CheckedChanged += new System.EventHandler(this.ckbMeshTemp_CheckedChanged);
            // 
            // cbMeshScope
            // 
            this.cbMeshScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMeshScope.FormattingEnabled = true;
            this.cbMeshScope.Items.AddRange(new object[] {
            "Private",
            "SemiGlobal",
            "Global"});
            this.cbMeshScope.Location = new System.Drawing.Point(43, 0);
            this.cbMeshScope.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbMeshScope.Name = "cbMeshScope";
            this.cbMeshScope.Size = new System.Drawing.Size(116, 21);
            this.cbMeshScope.TabIndex = 2;
            this.cbMeshScope.SelectedIndexChanged += new System.EventHandler(this.cbMatMeshScope_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(0, 2);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Scope";
            // 
            // ckbAllOver
            // 
            this.ckbAllOver.AutoSize = true;
            this.ckbAllOver.Location = new System.Drawing.Point(38, 38);
            this.ckbAllOver.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbAllOver.Name = "ckbAllOver";
            this.ckbAllOver.Size = new System.Drawing.Size(93, 17);
            this.ckbAllOver.TabIndex = 4;
            this.ckbAllOver.Text = "All over object";
            this.ckbAllOver.UseVisualStyleBackColor = true;
            this.ckbAllOver.CheckedChanged += new System.EventHandler(this.ckbAllOver_CheckedChanged);
            // 
            // rb3Object
            // 
            this.rb3Object.AutoSize = true;
            this.rb3Object.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rb3Object.Location = new System.Drawing.Point(84, 19);
            this.rb3Object.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rb3Object.Name = "rb3Object";
            this.rb3Object.Size = new System.Drawing.Size(91, 17);
            this.rb3Object.TabIndex = 3;
            this.rb3Object.TabStop = true;
            this.rb3Object.Text = "Source object";
            this.rb3Object.UseVisualStyleBackColor = true;
            this.rb3Object.CheckedChanged += new System.EventHandler(this.rb3group_CheckedChanged);
            // 
            // rb3Me
            // 
            this.rb3Me.AutoSize = true;
            this.rb3Me.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rb3Me.Location = new System.Drawing.Point(38, 19);
            this.rb3Me.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rb3Me.Name = "rb3Me";
            this.rb3Me.Size = new System.Drawing.Size(40, 17);
            this.rb3Me.TabIndex = 2;
            this.rb3Me.TabStop = true;
            this.rb3Me.Text = "Me";
            this.rb3Me.UseVisualStyleBackColor = true;
            this.rb3Me.CheckedChanged += new System.EventHandler(this.rb3group_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mesh from";
            // 
            // cbPicker1
            // 
            this.cbPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker1.DropDownWidth = 384;
            this.cbPicker1.Location = new System.Drawing.Point(321, 0);
            this.cbPicker1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbPicker1.Name = "cbPicker1";
            this.cbPicker1.Size = new System.Drawing.Size(126, 21);
            this.cbPicker1.TabIndex = 3;
            this.cbPicker1.Visible = false;
            // 
            // tbVal1
            // 
            this.tbVal1.Location = new System.Drawing.Point(321, 0);
            this.tbVal1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbVal1.Name = "tbVal1";
            this.tbVal1.Size = new System.Drawing.Size(108, 20);
            this.tbVal1.TabIndex = 3;
            // 
            // cbDataOwner1
            // 
            this.cbDataOwner1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner1.DropDownWidth = 384;
            this.cbDataOwner1.Location = new System.Drawing.Point(81, 0);
            this.cbDataOwner1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbDataOwner1.Name = "cbDataOwner1";
            this.cbDataOwner1.Size = new System.Drawing.Size(242, 21);
            this.cbDataOwner1.TabIndex = 2;
            // 
            // pnMaterial
            // 
            this.pnMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnMaterial.Controls.Add(this.pnNotScrShot);
            this.pnMaterial.Controls.Add(this.rb1Object);
            this.pnMaterial.Controls.Add(this.rb1Me);
            this.pnMaterial.Controls.Add(this.rb1ScrShot);
            this.pnMaterial.Controls.Add(this.label3);
            this.pnMaterial.Location = new System.Drawing.Point(0, 58);
            this.pnMaterial.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnMaterial.Name = "pnMaterial";
            this.pnMaterial.Size = new System.Drawing.Size(446, 115);
            this.pnMaterial.TabIndex = 9;
            // 
            // pnNotScrShot
            // 
            this.pnNotScrShot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnNotScrShot.Controls.Add(this.tbMaterial);
            this.pnNotScrShot.Controls.Add(this.btnMaterial);
            this.pnNotScrShot.Controls.Add(this.tbVal3);
            this.pnNotScrShot.Controls.Add(this.cbMatScope);
            this.pnNotScrShot.Controls.Add(this.label7);
            this.pnNotScrShot.Controls.Add(this.label6);
            this.pnNotScrShot.Controls.Add(this.label5);
            this.pnNotScrShot.Controls.Add(this.ckbMaterialTemp);
            this.pnNotScrShot.Controls.Add(this.rb2MovingTexture);
            this.pnNotScrShot.Controls.Add(this.rb2Material);
            this.pnNotScrShot.Location = new System.Drawing.Point(38, 38);
            this.pnNotScrShot.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnNotScrShot.Name = "pnNotScrShot";
            this.pnNotScrShot.Size = new System.Drawing.Size(407, 77);
            this.pnNotScrShot.TabIndex = 5;
            // 
            // tbMaterial
            // 
            this.tbMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMaterial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMaterial.Location = new System.Drawing.Point(115, 62);
            this.tbMaterial.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbMaterial.Name = "tbMaterial";
            this.tbMaterial.ReadOnly = true;
            this.tbMaterial.Size = new System.Drawing.Size(292, 13);
            this.tbMaterial.TabIndex = 9;
            this.tbMaterial.TabStop = false;
            this.tbMaterial.Text = "Material name";
            // 
            // btnMaterial
            // 
            this.btnMaterial.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnMaterial.Font = new System.Drawing.Font("Webdings", 12F, System.Drawing.FontStyle.Bold);
            this.btnMaterial.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMaterial.Location = new System.Drawing.Point(91, 58);
            this.btnMaterial.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnMaterial.Name = "btnMaterial";
            this.btnMaterial.Size = new System.Drawing.Size(19, 18);
            this.btnMaterial.TabIndex = 8;
            this.btnMaterial.Text = "8";
            this.btnMaterial.Click += new System.EventHandler(this.btnMaterial_Click);
            // 
            // tbVal3
            // 
            this.tbVal3.Location = new System.Drawing.Point(43, 58);
            this.tbVal3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbVal3.Name = "tbVal3";
            this.tbVal3.Size = new System.Drawing.Size(49, 20);
            this.tbVal3.TabIndex = 7;
            this.tbVal3.Text = "0xDDDD";
            // 
            // cbMatScope
            // 
            this.cbMatScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMatScope.FormattingEnabled = true;
            this.cbMatScope.Items.AddRange(new object[] {
            "Private",
            "SemiGlobal",
            "Global"});
            this.cbMatScope.Location = new System.Drawing.Point(43, 19);
            this.cbMatScope.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbMatScope.Name = "cbMatScope";
            this.cbMatScope.Size = new System.Drawing.Size(116, 21);
            this.cbMatScope.TabIndex = 4;
            this.cbMatScope.SelectedIndexChanged += new System.EventHandler(this.cbMatMeshScope_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(6, 39);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Index";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(0, 22);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Scope";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(0, 2);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Material is a:";
            // 
            // ckbMaterialTemp
            // 
            this.ckbMaterialTemp.AutoSize = true;
            this.ckbMaterialTemp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMaterialTemp.Location = new System.Drawing.Point(43, 38);
            this.ckbMaterialTemp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbMaterialTemp.Name = "ckbMaterialTemp";
            this.ckbMaterialTemp.Size = new System.Drawing.Size(74, 17);
            this.ckbMaterialTemp.TabIndex = 6;
            this.ckbMaterialTemp.Text = "In Temp 0";
            this.ckbMaterialTemp.UseVisualStyleBackColor = true;
            this.ckbMaterialTemp.CheckedChanged += new System.EventHandler(this.ckbMaterialTemp_CheckedChanged);
            // 
            // rb2MovingTexture
            // 
            this.rb2MovingTexture.AutoSize = true;
            this.rb2MovingTexture.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rb2MovingTexture.Location = new System.Drawing.Point(75, 0);
            this.rb2MovingTexture.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rb2MovingTexture.Name = "rb2MovingTexture";
            this.rb2MovingTexture.Size = new System.Drawing.Size(99, 17);
            this.rb2MovingTexture.TabIndex = 2;
            this.rb2MovingTexture.TabStop = true;
            this.rb2MovingTexture.Text = "Moving Texture";
            this.rb2MovingTexture.UseVisualStyleBackColor = true;
            // 
            // rb2Material
            // 
            this.rb2Material.AutoSize = true;
            this.rb2Material.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rb2Material.Location = new System.Drawing.Point(183, 0);
            this.rb2Material.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rb2Material.Name = "rb2Material";
            this.rb2Material.Size = new System.Drawing.Size(62, 17);
            this.rb2Material.TabIndex = 2;
            this.rb2Material.TabStop = true;
            this.rb2Material.Text = "Material";
            this.rb2Material.UseVisualStyleBackColor = true;
            // 
            // rb1Object
            // 
            this.rb1Object.AutoSize = true;
            this.rb1Object.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rb1Object.Location = new System.Drawing.Point(84, 19);
            this.rb1Object.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rb1Object.Name = "rb1Object";
            this.rb1Object.Size = new System.Drawing.Size(91, 17);
            this.rb1Object.TabIndex = 3;
            this.rb1Object.TabStop = true;
            this.rb1Object.Text = "Source object";
            this.rb1Object.UseVisualStyleBackColor = true;
            this.rb1Object.CheckedChanged += new System.EventHandler(this.rb1group_CheckedChanged);
            // 
            // rb1Me
            // 
            this.rb1Me.AutoSize = true;
            this.rb1Me.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rb1Me.Location = new System.Drawing.Point(38, 19);
            this.rb1Me.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rb1Me.Name = "rb1Me";
            this.rb1Me.Size = new System.Drawing.Size(40, 17);
            this.rb1Me.TabIndex = 2;
            this.rb1Me.TabStop = true;
            this.rb1Me.Text = "Me";
            this.rb1Me.UseVisualStyleBackColor = true;
            this.rb1Me.CheckedChanged += new System.EventHandler(this.rb1group_CheckedChanged);
            // 
            // rb1ScrShot
            // 
            this.rb1ScrShot.AutoSize = true;
            this.rb1ScrShot.Location = new System.Drawing.Point(184, 19);
            this.rb1ScrShot.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rb1ScrShot.Name = "rb1ScrShot";
            this.rb1ScrShot.Size = new System.Drawing.Size(82, 17);
            this.rb1ScrShot.TabIndex = 4;
            this.rb1ScrShot.TabStop = true;
            this.rb1ScrShot.Text = "Screen shot";
            this.rb1ScrShot.UseVisualStyleBackColor = true;
            this.rb1ScrShot.CheckedChanged += new System.EventHandler(this.rb1group_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Material from";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(0, 22);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Source object";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Target object";
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(460, 300);
            this.Controls.Add(this.pnWiz0x006d);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UI";
            this.Text = "UI";
            this.pnWiz0x006d.ResumeLayout(false);
            this.pnWiz0x006d.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnNotAllOver.ResumeLayout(false);
            this.pnNotAllOver.PerformLayout();
            this.pnMaterial.ResumeLayout(false);
            this.pnMaterial.PerformLayout();
            this.pnNotScrShot.ResumeLayout(false);
            this.pnNotScrShot.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private void rb1group_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            this.MaterialFrom();
        }

        private void ckbMaterialTemp_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            this.MaterialFrom();
        }

        private void btnMaterial_Click(object sender, EventArgs e)
        {
            this.doStrChooser(this.cbMatScope, GS.GlobalStr.MaterialName, this.tbVal3, this.tbMaterial);
        }

        private void rb3group_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            this.MeshFrom();
        }

        private void ckbAllOver_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            this.MeshFrom();
        }

        private void ckbMeshTemp_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            this.MeshFrom();
        }

        private void btnMesh_Click(object sender, EventArgs e)
        {
            this.doStrChooser(this.cbMeshScope, GS.GlobalStr.MeshGroup, this.tbVal5, this.tbMesh);
        }

        private void cbMatMeshScope_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            if (sender.Equals(this.cbMatScope))
                doStrValue(cbMatScope, GS.GlobalStr.MaterialName, doid3.Value, tbMaterial);
            else
                doStrValue(cbMeshScope, GS.GlobalStr.MeshGroup, doid5.Value, tbMesh);
        }

    }

}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWiz0x006d : pjse.ABhavOperandWiz
	{
        public BhavOperandWiz0x006d(Instruction i) : base(i) { myForm = new Wiz0x006d.UI(); }

		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm = null;
		}
		#endregion

	}

}
