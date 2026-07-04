/***************************************************************************
 *   Copyright (C) 2007 by Peter L Jones                                   *
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
using SimPe.PackedFiles.Wrapper;

namespace pjse.BhavOperandWizards.Wiz0x0076
{
	/// <summary>
	/// Summary description for BhavInstruction.
	/// </summary>
    internal class UI : System.Windows.Forms.Form, iBhavOperandWizForm
    {
        #region Form variables

        internal System.Windows.Forms.Panel pnWiz0x0076;
        private RadioButton rb1StackObj;
        private RadioButton rb1My;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lbOp2;
        private Panel pnOp1;
        private Label lbConst1;
        private ComboBox cbPicker1;
        private TextBox tbval1;
        private ComboBox cbDataOwner1;
        private Label lbOp1;
        private Panel pnOp2;
        private Label lbConst2;
        private ComboBox cbPicker2;
        private TextBox tbval2;
        private ComboBox cbDataOwner2;
        private Panel panel1;
        private CheckBox ckbAttrPicker;
        private CheckBox ckbDecimal;
        private Panel pnArray;
        private Panel panel2;
        private Label label1;
        private Label label3;
        private ComboBox cbOperation;
        private ComboBox cbObjectArray;
        private TextBox tbObjectArray;
        private Panel panel3;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        #endregion

        public UI()
        {
            //
            // Required designer variable.
            //
            InitializeComponent();
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
        private DataOwnerControl doidArray = null;
        private DataOwnerControl doidValue = null;
        private DataOwnerControl doidIndex = null;

        static string sIndex = Localization.GetString("Index");
        static string sValue = Localization.GetString("Value");

        private bool[] d1enable = { false, true, true, true, true, true, true, false, false, false, true, true, false, false, };
        private bool[] d1IndexValue = { false, false, false, false, false, false, false, false, false, false, false, true, false, false, };
        private bool[] d2enable = { false, false, false, false, false, false, true, false, false, true, true, true, false, false, };
        private bool[] d2IndexValue = { true, true, true, true, true, true, true, true, true, true, true, true, true, true, };

        private void setOperation(int val)
        {
            cbOperation.SelectedIndex = (val < cbOperation.Items.Count) ? val : -1;

            pnOp1.Enabled = (val < d1enable.Length && d1enable[val]);
            lbOp1.Text = pnOp1.Enabled ? (d1IndexValue[val] ? sIndex : sValue) : "";

            pnOp2.Enabled = (val < d2enable.Length && d2enable[val]);
            lbOp2.Text = pnOp2.Enabled ? (d2IndexValue[val] ? sIndex : sValue) : "";
        }

        #region iBhavOperandWizForm
        public Panel WizPanel { get { return this.pnWiz0x0076; } }

        public void Execute(Instruction inst)
        {
            this.inst = inst;

			byte[] o = new byte[16];
            ((byte[])inst.Operands).CopyTo(o, 0);
            ((byte[])inst.Reserved1).CopyTo(o, 8);

            setOperation(o[0x01]);
            // See discussion around whether this is a bit vs boolean:
            // http://simlogical.com/SMF/index.php?topic=917.msg6641#msg6641
            rb1StackObj.Checked = !(rb1My.Checked = (o[0x2] == 0));

            doidArray = new DataOwnerControl(inst, null, this.cbObjectArray, this.tbObjectArray,
                this.ckbDecimal, this.ckbAttrPicker, null,
                0x29, BhavWiz.ToShort(o[0x03], o[0x04]));
            doidValue = new DataOwnerControl(inst, this.cbDataOwner1, this.cbPicker1, this.tbval1,
                this.ckbDecimal, this.ckbAttrPicker, this.lbConst1,
                o[0x05], BhavWiz.ToShort(o[0x06], o[0x07]));
            doidIndex = new DataOwnerControl(inst, this.cbDataOwner2, this.cbPicker2, this.tbval2,
                this.ckbDecimal, this.ckbAttrPicker, this.lbConst2,
                o[0x08], BhavWiz.ToShort(o[0x09], o[0x0a]));
        }

        public Instruction Write(Instruction inst)
        {
            if (inst != null)
            {
                wrappedByteArray ops1 = inst.Operands;
                wrappedByteArray ops2 = inst.Reserved1;

                if (cbOperation.SelectedIndex >= 0)
                    ops1[0x01] = (byte)cbOperation.SelectedIndex;
                ops1[0x02] = (byte)(rb1My.Checked ? 0x00 : 0x02); // Not sure why "0x02" at the game treats as 0 / !0

                BhavWiz.FromShort(ref ops1, 3, doidArray.Value);

                ops1[0x05] = doidValue.DataOwner;
                BhavWiz.FromShort(ref ops1, 6, doidValue.Value);

                ops2[0x00] = doidIndex.DataOwner;
                BhavWiz.FromShort(ref ops2, 1, doidIndex.Value);
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
            this.pnWiz0x0076 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnArray = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbOperation = new System.Windows.Forms.ComboBox();
            this.cbObjectArray = new System.Windows.Forms.ComboBox();
            this.tbObjectArray = new System.Windows.Forms.TextBox();
            this.pnOp2 = new System.Windows.Forms.Panel();
            this.lbConst2 = new System.Windows.Forms.Label();
            this.cbPicker2 = new System.Windows.Forms.ComboBox();
            this.tbval2 = new System.Windows.Forms.TextBox();
            this.cbDataOwner2 = new System.Windows.Forms.ComboBox();
            this.lbOp2 = new System.Windows.Forms.Label();
            this.pnOp1 = new System.Windows.Forms.Panel();
            this.lbConst1 = new System.Windows.Forms.Label();
            this.cbPicker1 = new System.Windows.Forms.ComboBox();
            this.tbval1 = new System.Windows.Forms.TextBox();
            this.cbDataOwner1 = new System.Windows.Forms.ComboBox();
            this.lbOp1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ckbAttrPicker = new System.Windows.Forms.CheckBox();
            this.ckbDecimal = new System.Windows.Forms.CheckBox();
            this.rb1StackObj = new System.Windows.Forms.RadioButton();
            this.rb1My = new System.Windows.Forms.RadioButton();
            this.pnWiz0x0076.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnArray.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnOp2.SuspendLayout();
            this.pnOp1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnWiz0x0076
            // 
            this.pnWiz0x0076.AutoSize = true;
            this.pnWiz0x0076.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnWiz0x0076.Controls.Add(this.tableLayoutPanel1);
            this.pnWiz0x0076.Controls.Add(this.rb1StackObj);
            this.pnWiz0x0076.Controls.Add(this.rb1My);
            this.pnWiz0x0076.Location = new System.Drawing.Point(0, 0);
            this.pnWiz0x0076.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnWiz0x0076.Name = "pnWiz0x0076";
            this.pnWiz0x0076.Size = new System.Drawing.Size(463, 202);
            this.pnWiz0x0076.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.pnArray, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnOp2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbOp2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pnOp1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbOp1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 46);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(459, 154);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // pnArray
            // 
            this.pnArray.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnArray.Controls.Add(this.panel3);
            this.pnArray.Controls.Add(this.panel2);
            this.pnArray.Controls.Add(this.cbOperation);
            this.pnArray.Controls.Add(this.cbObjectArray);
            this.pnArray.Controls.Add(this.tbObjectArray);
            this.pnArray.Location = new System.Drawing.Point(112, 0);
            this.pnArray.Margin = new System.Windows.Forms.Padding(0);
            this.pnArray.Name = "pnArray";
            this.pnArray.Size = new System.Drawing.Size(346, 52);
            this.pnArray.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(0, 43);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(347, 1);
            this.panel3.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(265, 13);
            this.panel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(229, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 5, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Array:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(2, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 5, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Operation:";
            // 
            // cbOperation
            // 
            this.cbOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperation.DropDownWidth = 462;
            this.cbOperation.Items.AddRange(new object[] {
            "Clear contents",
            "Get size",
            "Set size",
            "Set all elements",
            "Unshift (drop last element, move elements up, Value to 0th element)",
            "Push (drop 0th element, move elements down, Value to last element)",
            "Insert (drop last element, move elements above Index up, Value to element at Inde" +
                "x)",
            "Shift (drop 0th element, move elements down)",
            "Pop (drop last element, move elements up)",
            "Remove (move elements above Index down)",
            "Set to Index next element of Value",
            "Swap elements",
            "Sort (highest value in 0th element)",
            "Sort (highest value in last)"});
            this.cbOperation.Location = new System.Drawing.Point(2, 16);
            this.cbOperation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbOperation.Name = "cbOperation";
            this.cbOperation.Size = new System.Drawing.Size(228, 21);
            this.cbOperation.TabIndex = 1;
            this.cbOperation.SelectedIndexChanged += new System.EventHandler(this.cbOperation_SelectedIndexChanged);
            // 
            // cbObjectArray
            // 
            this.cbObjectArray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbObjectArray.DropDownWidth = 384;
            this.cbObjectArray.Location = new System.Drawing.Point(229, 16);
            this.cbObjectArray.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbObjectArray.Name = "cbObjectArray";
            this.cbObjectArray.Size = new System.Drawing.Size(117, 21);
            this.cbObjectArray.TabIndex = 1;
            this.cbObjectArray.Visible = false;
            // 
            // tbObjectArray
            // 
            this.tbObjectArray.Location = new System.Drawing.Point(229, 16);
            this.tbObjectArray.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbObjectArray.Name = "tbObjectArray";
            this.tbObjectArray.Size = new System.Drawing.Size(117, 20);
            this.tbObjectArray.TabIndex = 1;
            // 
            // pnOp2
            // 
            this.pnOp2.AutoSize = true;
            this.pnOp2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnOp2.Controls.Add(this.lbConst2);
            this.pnOp2.Controls.Add(this.cbPicker2);
            this.pnOp2.Controls.Add(this.tbval2);
            this.pnOp2.Controls.Add(this.cbDataOwner2);
            this.pnOp2.Location = new System.Drawing.Point(112, 89);
            this.pnOp2.Margin = new System.Windows.Forms.Padding(0);
            this.pnOp2.Name = "pnOp2";
            this.pnOp2.Size = new System.Drawing.Size(347, 38);
            this.pnOp2.TabIndex = 6;
            // 
            // lbConst2
            // 
            this.lbConst2.AutoSize = true;
            this.lbConst2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbConst2.Location = new System.Drawing.Point(2, 25);
            this.lbConst2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbConst2.Name = "lbConst2";
            this.lbConst2.Size = new System.Drawing.Size(69, 13);
            this.lbConst2.TabIndex = 12;
            this.lbConst2.Text = "Const2 value";
            // 
            // cbPicker2
            // 
            this.cbPicker2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker2.DropDownWidth = 384;
            this.cbPicker2.Location = new System.Drawing.Point(228, 2);
            this.cbPicker2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbPicker2.Name = "cbPicker2";
            this.cbPicker2.Size = new System.Drawing.Size(117, 21);
            this.cbPicker2.TabIndex = 11;
            this.cbPicker2.Visible = false;
            // 
            // tbval2
            // 
            this.tbval2.Location = new System.Drawing.Point(228, 2);
            this.tbval2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbval2.Name = "tbval2";
            this.tbval2.Size = new System.Drawing.Size(117, 20);
            this.tbval2.TabIndex = 10;
            // 
            // cbDataOwner2
            // 
            this.cbDataOwner2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner2.DropDownWidth = 384;
            this.cbDataOwner2.Location = new System.Drawing.Point(2, 2);
            this.cbDataOwner2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbDataOwner2.Name = "cbDataOwner2";
            this.cbDataOwner2.Size = new System.Drawing.Size(228, 21);
            this.cbDataOwner2.TabIndex = 9;
            // 
            // lbOp2
            // 
            this.lbOp2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbOp2.AutoSize = true;
            this.lbOp2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbOp2.Location = new System.Drawing.Point(4, 94);
            this.lbOp2.Margin = new System.Windows.Forms.Padding(2, 5, 2, 0);
            this.lbOp2.Name = "lbOp2";
            this.lbOp2.Size = new System.Drawing.Size(106, 26);
            this.lbOp2.TabIndex = 5;
            this.lbOp2.Text = "WWWWWWWWWW";
            // 
            // pnOp1
            // 
            this.pnOp1.AutoSize = true;
            this.pnOp1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnOp1.Controls.Add(this.lbConst1);
            this.pnOp1.Controls.Add(this.cbPicker1);
            this.pnOp1.Controls.Add(this.tbval1);
            this.pnOp1.Controls.Add(this.cbDataOwner1);
            this.pnOp1.Location = new System.Drawing.Point(112, 52);
            this.pnOp1.Margin = new System.Windows.Forms.Padding(0);
            this.pnOp1.Name = "pnOp1";
            this.pnOp1.Size = new System.Drawing.Size(347, 37);
            this.pnOp1.TabIndex = 4;
            // 
            // lbConst1
            // 
            this.lbConst1.AutoSize = true;
            this.lbConst1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbConst1.Location = new System.Drawing.Point(2, 24);
            this.lbConst1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbConst1.Name = "lbConst1";
            this.lbConst1.Size = new System.Drawing.Size(69, 13);
            this.lbConst1.TabIndex = 12;
            this.lbConst1.Text = "Const1 value";
            // 
            // cbPicker1
            // 
            this.cbPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker1.DropDownWidth = 384;
            this.cbPicker1.Location = new System.Drawing.Point(228, 2);
            this.cbPicker1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbPicker1.Name = "cbPicker1";
            this.cbPicker1.Size = new System.Drawing.Size(117, 21);
            this.cbPicker1.TabIndex = 11;
            this.cbPicker1.Visible = false;
            // 
            // tbval1
            // 
            this.tbval1.Location = new System.Drawing.Point(228, 2);
            this.tbval1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbval1.Name = "tbval1";
            this.tbval1.Size = new System.Drawing.Size(117, 20);
            this.tbval1.TabIndex = 10;
            // 
            // cbDataOwner1
            // 
            this.cbDataOwner1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner1.DropDownWidth = 384;
            this.cbDataOwner1.Location = new System.Drawing.Point(2, 2);
            this.cbDataOwner1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbDataOwner1.Name = "cbDataOwner1";
            this.cbDataOwner1.Size = new System.Drawing.Size(228, 21);
            this.cbDataOwner1.TabIndex = 9;
            // 
            // lbOp1
            // 
            this.lbOp1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbOp1.AutoSize = true;
            this.lbOp1.Location = new System.Drawing.Point(4, 57);
            this.lbOp1.Margin = new System.Windows.Forms.Padding(2, 5, 2, 0);
            this.lbOp1.Name = "lbOp1";
            this.lbOp1.Size = new System.Drawing.Size(106, 26);
            this.lbOp1.TabIndex = 3;
            this.lbOp1.Text = "WWWWWWWWWW";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.ckbAttrPicker);
            this.panel1.Controls.Add(this.ckbDecimal);
            this.panel1.Location = new System.Drawing.Point(114, 129);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 23);
            this.panel1.TabIndex = 7;
            // 
            // ckbAttrPicker
            // 
            this.ckbAttrPicker.AutoSize = true;
            this.ckbAttrPicker.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbAttrPicker.Location = new System.Drawing.Point(212, 0);
            this.ckbAttrPicker.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbAttrPicker.Name = "ckbAttrPicker";
            this.ckbAttrPicker.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbAttrPicker.Size = new System.Drawing.Size(113, 21);
            this.ckbAttrPicker.TabIndex = 2;
            this.ckbAttrPicker.Text = "use picker names";
            // 
            // ckbDecimal
            // 
            this.ckbDecimal.AutoSize = true;
            this.ckbDecimal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbDecimal.Location = new System.Drawing.Point(58, 0);
            this.ckbDecimal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbDecimal.Name = "ckbDecimal";
            this.ckbDecimal.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbDecimal.Size = new System.Drawing.Size(144, 21);
            this.ckbDecimal.TabIndex = 1;
            this.ckbDecimal.Text = "Decimal (except Consts)";
            // 
            // rb1StackObj
            // 
            this.rb1StackObj.AutoSize = true;
            this.rb1StackObj.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rb1StackObj.Location = new System.Drawing.Point(2, 24);
            this.rb1StackObj.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rb1StackObj.Name = "rb1StackObj";
            this.rb1StackObj.Size = new System.Drawing.Size(155, 17);
            this.rb1StackObj.TabIndex = 2;
            this.rb1StackObj.TabStop = true;
            this.rb1StackObj.Text = "Stack Object\'s Object Array";
            this.rb1StackObj.UseVisualStyleBackColor = true;
            // 
            // rb1My
            // 
            this.rb1My.AutoSize = true;
            this.rb1My.Location = new System.Drawing.Point(2, 2);
            this.rb1My.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rb1My.Name = "rb1My";
            this.rb1My.Size = new System.Drawing.Size(100, 17);
            this.rb1My.TabIndex = 1;
            this.rb1My.TabStop = true;
            this.rb1My.Text = "My Object Array";
            this.rb1My.UseVisualStyleBackColor = true;
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 250);
            this.Controls.Add(this.pnWiz0x0076);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UI";
            this.Text = "UI";
            this.pnWiz0x0076.ResumeLayout(false);
            this.pnWiz0x0076.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnArray.ResumeLayout(false);
            this.pnArray.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnOp2.ResumeLayout(false);
            this.pnOp2.PerformLayout();
            this.pnOp1.ResumeLayout(false);
            this.pnOp1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void cbOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            setOperation(cbOperation.SelectedIndex);
        }

    }
}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWiz0x0076 : pjse.ABhavOperandWiz
	{
		public BhavOperandWiz0x0076(Instruction i) : base(i) { myForm = new Wiz0x0076.UI(); }

		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm = null;
		}
		#endregion

	}

}
