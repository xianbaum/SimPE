/***************************************************************************
 *   Copyright (C) 2007 by Peter L Jones                                   *
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.PackedFiles.Wrapper;

namespace pjse.BhavOperandWizards.Wiz0x0033
{
    internal class UI : System.Windows.Forms.Form, iBhavOperandWizForm
    {
        #region Form variables

        internal System.Windows.Forms.Panel pnWiz0x0033;
        private TableLayoutPanel tlpnGetSetValue;
        private Panel pnDoid1;
        private ComboBox cbPicker1;
        private TextBox tbVal1;
        private ComboBox cbDataOwner1;
        private Label lbDoid2;
        private Label lbDoid1;
        private Label lbDoid3;
        private Panel pnDoid2;
        private ComboBox cbPicker2;
        private TextBox tbVal2;
        private ComboBox cbDataOwner2;
        private Panel pnDoid3;
        private ComboBox cbPicker3;
        private TextBox tbVal3;
        private ComboBox cbDataOwner3;
        private Label lbGUID;
        private ComboBox cbInventory;
        private Label lbInventory;
        private FlowLayoutPanel flpnGUID;
        private TextBox tbGUID;
        private TextBox tbObjName;
        private GroupBox gbTokenTypes;
        private TableLayoutPanel tableLayoutPanel1;
        private CheckBox ckbTTInvShopping;
        private CheckBox ckbTTShopping;
        private CheckBox ckbTTInvMemory;
        private CheckBox ckbTTMemory;
        private CheckBox ckbTTInvVisible;
        private CheckBox ckbTTVisible;
        private GroupBox gbInventoryType;
        private FlowLayoutPanel flpnInventoryType;
        private RadioButton rb1Counted;
        private RadioButton rb1Singular;
        private FlowLayoutPanel flpnDoid0;
        private Label lbDoid0;
        private Panel pnDoid0;
        private ComboBox cbPicker0;
        private TextBox tbVal0;
        private ComboBox cbDataOwner0;
        private Label lbOperation;
        private FlowLayoutPanel flpnOperation;
        private ComboBox cbOperation;
        private CheckBox ckbReversed;
        private ComboBox cbTargetInv;
        private CheckBox ckbTTAll;
        private FlowLayoutPanel flowLayoutPanel1;
        private CheckBox ckbDecimal;
        private CheckBox ckbAttrPicker;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        #endregion


        /// <summary>
        /// Initialise the Wizard user interface
        /// </summary>
        /// <param name="mode">Specify whether the wizard is for Animate Object, Sim or Overlay</param>
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

        #region static data
        static List<String> aInventoryType = BhavWiz.readStr(GS.BhavStr.InventoryType);
        static List<String> aTokenOpsCounted = BhavWiz.readStr(GS.BhavStr.TokenOpsCounted);
        static List<String> aTokenOpsSingular = BhavWiz.readStr(GS.BhavStr.TokenOpsSingular);
        static String[] names = { "", "Object", "bwp33_index", "bwp33_property", "bwp33_count", "Value" };
        static int[][] aNamesCounted = {
            new int[] { 0, 0, 0, 0,  0, 0, 0, 0,  0, 0, 0, 0, }, // Doid0
            new int[] { 0, 0, 0, 0,  0, 0, 0, 0,  0, 0, 0, 0, }, // Doid1
            new int[] { 0, 2, 0, 2,  0, 2, 2, 0,  2, 2, 0, 2, }, // Doid2
            new int[] { 4, 4, 4, 4,  0, 0, 4, 0,  0, 0, 4, 0, }, // Doid3 was new int[] { 4, 4, 4, 4,  0, 0, 0, 0,  0, 0, 4, 0, },
        };
        static int[][] aNamesSingular = {
            new int[] { 0, 0, 0, 0,  0, 0, 0, 0,  0, 0, 0, 0,  0, 0, 0, 0,  0, 0, 0, 0, }, // Doid0
            new int[] { 0, 0, 0, 0,  0, 0, 0, 0,  0, 0, 0, 0,  0, 0, 2, 2,  0, 0, 0, 0, }, // Doid1
            new int[] { 0, 2, 0, 2,  2, 2, 2, 3,  3, 0, 0, 0,  2, 0, 3, 3,  0, 2, 2, 0, }, // Doid2
            new int[] { 0, 0, 0, 0,  5, 5, 0, 5,  5, 0, 4, 0,  0, 4, 5, 5,  0, 0, 0, 0, }, // Doid3
        };
        static bool[] aByGUIDCounted =
            new bool[] { true , false, true , false,  true , false, true , true ,  false, false, true , false, };
        static bool[] aByGUIDSingular =
            new bool[] { true , false, true , true ,  false, false, false, false,  false, false, false, false,  false, false, false, false,  false, false, false, false, };
        static bool[] aCategoryCounted =
            new bool[] { true , false, true , false,  true , false, true , true ,  false, true , true , true , };
        static bool[] aCategorySingular =
            new bool[] { true , false, true , true ,  false, false, false, false,  false, false, false, true ,  true , true , false, false,  true , false, true , true , };
        #endregion

        private bool internalchg = false;

        private Instruction inst = null;

        private DataOwnerControl doid0 = null; // o[1], o[2], o[3]
        private DataOwnerControl doid1 = null; // o[6], o[7], o[8]
        private DataOwnerControl doid2 = null; // o[10], o[11], o[12]
        private DataOwnerControl doid3 = null; // o[13], o[14], o[15]
        private byte operation = 0;
        private byte[] o5678 = new byte[4];

        private bool hex32_IsValid(object sender)
        {
            try { Convert.ToUInt32(((TextBox)sender).Text, 16); }
            catch (Exception) { return false; }
            return true;
        }

        private void setGUID(byte[] o, int sub) { setGUID(true, (UInt32)(o[sub] | o[sub + 1] << 8 | o[sub + 2] << 16 | o[sub + 3] << 24)); }
        private void setGUID(bool setTB, UInt32 guid)
        {
            if (setTB) this.tbGUID.Text = "0x" + SimPe.Helper.HexString(guid);
            this.tbObjName.Text = (guid == 0) ? BhavWiz.dnStkOb() : BhavWiz.FormatGUID(true, guid);
        }

        private void doTokenOps(List<String> tokenops)
        {
            cbOperation.Items.Clear();
            cbOperation.Items.AddRange(tokenops.ToArray());
            cbOperation.SelectedIndex = (operation < cbOperation.Items.Count) ? operation : -1;
        }

        private void doTokenType()
        {
            gbTokenTypes.Enabled = true;
            ckbTTInvVisible.Enabled = !ckbTTVisible.Enabled || ckbTTVisible.Checked;
            ckbTTInvMemory.Enabled = !ckbTTMemory.Enabled || ckbTTMemory.Checked;
            ckbTTInvShopping.Enabled = ckbTTShopping.Checked;
            ckbTTAll.Checked = !ckbTTVisible.Checked && !ckbTTMemory.Checked && !ckbTTShopping.Checked;
        }

        private void doFromInventory(bool enable)
        {
            if (enable)
                cbInventory.Enabled = true;
            int i = (o5678[1] & 0x07);
            cbInventory.SelectedIndex = (i < cbInventory.Items.Count) ? i : -1;
            lbDoid3.Text = (pnDoid3.Enabled = (i >= 1 && i <= 3)) ? cbInventory.SelectedItem.ToString() : "";
        }

        private void doByGUID()
        {
            flpnGUID.Enabled = true;
            setGUID(o5678, 0);
        }

        private void refreshDoid1()
        {
            tbVal1.Text = "0x" + SimPe.Helper.HexString(BhavWiz.ToShort(o5678[2], o5678[3]));
            cbDataOwner1.SelectedIndex = (cbDataOwner1.Items.Count > o5678[1]) ? o5678[1] : -1;
        }

        private void doBoth(List<String> aTokenOps, int[][] aNames, bool[] aByGUID, bool[] aCategory)
        {
            doTokenOps(aTokenOps);

            pnDoid1.Enabled = pnDoid2.Enabled = pnDoid3.Enabled = false;
            gbTokenTypes.Enabled = ckbReversed.Enabled = false;
            cbInventory.Enabled = false;
            flpnGUID.Enabled = false; tbObjName.Text = tbGUID.Text = "";
            gbInventoryType.Enabled = true;

            if (operation < aByGUID.Length && aByGUID[operation])
                doByGUID();

            if (operation < aCategory.Length && aCategory[operation])
                doTokenType();

            bool doid1Enabled = pnDoid1.Enabled;

            if (operation < aNames[0].Length)
            {
                lbDoid1.Text = (pnDoid1.Enabled = (aNames[1][operation] > 0)) ? pjse.Localization.GetString(names[aNames[1][operation]]) : "";
                lbDoid2.Text = (pnDoid2.Enabled = (aNames[2][operation] > 0)) ? pjse.Localization.GetString(names[aNames[2][operation]]) : "";
                lbDoid3.Text = (pnDoid3.Enabled = (aNames[3][operation] > 0)) ? pjse.Localization.GetString(names[aNames[3][operation]]) : "";
            }

            if (!doid1Enabled && pnDoid1.Enabled) refreshDoid1();
        }

        private void doCounted()
        {
            doBoth(aTokenOpsCounted, aNamesCounted, aByGUIDCounted, aCategoryCounted);

            switch (operation)
            {
                case 0x0b: doFromInventory(true); break;
            }
        }

        private void doSingular()
        {
            doBoth(aTokenOpsSingular, aNamesSingular, aByGUIDSingular, aCategorySingular);

            switch (operation)
            {
                case 0x03: ckbReversed.Enabled = true; break;
                case 0x07: gbInventoryType.Enabled = false; break;
                case 0x08: gbInventoryType.Enabled = false; break;
                case 0x09: gbInventoryType.Enabled = false; break;
                case 0x0c: ckbReversed.Enabled = true; break;
                case 0x12: doFromInventory(true); break;
            }
        }

        #region iBhavOperandWizForm
        public Panel WizPanel { get { return this.pnWiz0x0033; } }

        public void Execute(Instruction inst)
        {
            this.inst = inst;

            wrappedByteArray ops1 = inst.Operands;
            wrappedByteArray ops2 = inst.Reserved1;

            o5678[0] = ops1[5];
            o5678[1] = ops1[6];
            o5678[2] = ops1[7];
            o5678[3] = ops2[0];

            internalchg = true;

            Boolset option1 = ops1[0];
            if (inst.NodeVersion < 1)
            {
                // In the parser we have something like this...
                //option1 = (inst.NodeVersion >= 1) ? ops1[0] : (byte)(((ops1[0] & 0x3C) << 1) | (ops1[0] & 0x83));
                // 8765 4321
                // 0065 4300 <<1 =
                // 0654 3000 |
                // 8000 0021 =
                // 8654 3021

                List<String> aS = new List<string>(aInventoryType.ToArray());
                aS.RemoveRange(4, aS.Count - 4);
                cbTargetInv.Items.Clear();
                cbTargetInv.Items.AddRange(aS.ToArray());
                cbInventory.Items.Clear();
                cbInventory.Items.AddRange(aS.ToArray());
                cbTargetInv.SelectedIndex = ((option1 & 0x03) < cbTargetInv.Items.Count) ? option1 & 0x03 : -1;

                rb1Counted.Checked = option1[2];
                ckbTTInvVisible.Checked = !option1[3];
                ckbTTInvMemory.Checked = !option1[4];
            }
            else
            {
                cbTargetInv.Items.Clear();
                cbTargetInv.Items.AddRange(aInventoryType.ToArray());
                cbInventory.Items.Clear();
                cbInventory.Items.AddRange(aInventoryType.ToArray());
                cbTargetInv.SelectedIndex = ((option1 & 0x07) < cbTargetInv.Items.Count) ? option1 & 0x07 : -1;

                rb1Counted.Checked = option1[3];
                ckbTTInvVisible.Checked = !option1[4];
                ckbTTInvMemory.Checked = !option1[5];
            }
            ckbReversed.Checked = option1[7];

            pnDoid0.Enabled = (cbTargetInv.SelectedIndex >= 1 && cbTargetInv.SelectedIndex <= 3);
            lbDoid0.Text = pnDoid0.Enabled ? cbTargetInv.SelectedItem.ToString() : "";
            rb1Singular.Checked = !rb1Counted.Checked;

            doid0 = new DataOwnerControl(inst, cbDataOwner0, cbPicker0, tbVal0,
                ckbDecimal, ckbAttrPicker, null, ops1[1], BhavWiz.ToShort(ops1[2], ops1[3]));

            operation = ops1[4];

            doid1 = new DataOwnerControl(inst, cbDataOwner1, cbPicker1, tbVal1,
                ckbDecimal, ckbAttrPicker, null, o5678[1], BhavWiz.ToShort(o5678[2], o5678[3]));
            doid1.DataOwnerControlChanged += new EventHandler(doid1_DataOwnerControlChanged);

            ckbTTVisible.Enabled = ckbTTMemory.Enabled = ckbTTShopping.Enabled = (inst.NodeVersion >= 2);
            if (inst.NodeVersion >= 2)
            {
                Boolset option2 = ops2[1];
                ckbTTInvShopping.Checked = !option2[0];
                ckbTTVisible.Checked = option2[2];
                ckbTTMemory.Checked = option2[3];
                ckbTTShopping.Checked = option2[5];
            }

            doid2 = new DataOwnerControl(inst, cbDataOwner2, cbPicker2, tbVal2,
                ckbDecimal, ckbAttrPicker, null, ops2[2], BhavWiz.ToShort(ops2[3], ops2[4]));

            doid3 = new DataOwnerControl(inst, cbDataOwner3, cbPicker3, tbVal3,
                ckbDecimal, ckbAttrPicker, null, ops2[5], BhavWiz.ToShort(ops2[6], ops2[7]));


            if (rb1Counted.Checked)
                doCounted();
            else
                doSingular();

            cbOperation.SelectedIndex = (operation < cbOperation.Items.Count) ? operation : -1;

            internalchg = false;
        }

        void doid1_DataOwnerControlChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            if (doid1.DataOwner >= 0)
                o5678[1] = doid1.DataOwner;
            BhavWiz.FromShort(ref o5678, 2, doid1.Value);
        }


        public Instruction Write(Instruction inst)
        {
            if (inst != null)
            {
                wrappedByteArray ops1 = inst.Operands;
                wrappedByteArray ops2 = inst.Reserved1;

                Boolset option1 = ops1[0];
                if (inst.NodeVersion < 1)
                {
                    if (cbTargetInv.SelectedIndex >= 0)
                        option1 = (byte)((option1 & 0xfc) | (cbTargetInv.SelectedIndex & 0x03));

                    option1[2] = rb1Counted.Checked;
                    option1[3] = !ckbTTInvVisible.Checked;
                    option1[4] = !ckbTTInvMemory.Checked;
                }
                else
                {
                    if (cbTargetInv.SelectedIndex >= 0)
                        option1 = (byte)((option1 & 0xf8) | (cbTargetInv.SelectedIndex & 0x07));

                    option1[3] = rb1Counted.Checked;
                    option1[4] = !ckbTTInvVisible.Checked;
                    option1[5] = !ckbTTInvMemory.Checked;
                }
                option1[7] = ckbReversed.Checked;
                ops1[0] = option1;

                ops1[1] = doid0.DataOwner;
                BhavWiz.FromShort(ref ops1, 2, doid0.Value);

                ops1[4] = operation;

                ops1[5] = o5678[0];
                ops1[6] = o5678[1];
                ops1[7] = o5678[2];
                ops2[0] = o5678[3];

                if (inst.NodeVersion >= 2)
                {
                    Boolset option2 = ops2[1];
                    option2[0] = !ckbTTInvShopping.Checked;
                    option2[2] = ckbTTVisible.Checked;
                    option2[3] = ckbTTMemory.Checked;
                    option2[5] = ckbTTShopping.Checked;
                    ops2[1] = option2;
                }

                ops2[2] = doid2.DataOwner;
                BhavWiz.FromShort(ref ops2, 3, doid2.Value);

                ops2[5] = doid3.DataOwner;
                BhavWiz.FromShort(ref ops2, 6, doid3.Value);
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
            this.pnWiz0x0033 = new System.Windows.Forms.Panel();
            this.tlpnGetSetValue = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ckbDecimal = new System.Windows.Forms.CheckBox();
            this.ckbAttrPicker = new System.Windows.Forms.CheckBox();
            this.lbOperation = new System.Windows.Forms.Label();
            this.flpnOperation = new System.Windows.Forms.FlowLayoutPanel();
            this.cbOperation = new System.Windows.Forms.ComboBox();
            this.ckbReversed = new System.Windows.Forms.CheckBox();
            this.gbTokenTypes = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ckbTTAll = new System.Windows.Forms.CheckBox();
            this.ckbTTInvShopping = new System.Windows.Forms.CheckBox();
            this.ckbTTShopping = new System.Windows.Forms.CheckBox();
            this.ckbTTInvMemory = new System.Windows.Forms.CheckBox();
            this.ckbTTMemory = new System.Windows.Forms.CheckBox();
            this.ckbTTInvVisible = new System.Windows.Forms.CheckBox();
            this.ckbTTVisible = new System.Windows.Forms.CheckBox();
            this.gbInventoryType = new System.Windows.Forms.GroupBox();
            this.flpnDoid0 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbDoid0 = new System.Windows.Forms.Label();
            this.pnDoid0 = new System.Windows.Forms.Panel();
            this.cbPicker0 = new System.Windows.Forms.ComboBox();
            this.tbVal0 = new System.Windows.Forms.TextBox();
            this.cbDataOwner0 = new System.Windows.Forms.ComboBox();
            this.flpnInventoryType = new System.Windows.Forms.FlowLayoutPanel();
            this.rb1Counted = new System.Windows.Forms.RadioButton();
            this.rb1Singular = new System.Windows.Forms.RadioButton();
            this.cbTargetInv = new System.Windows.Forms.ComboBox();
            this.lbDoid1 = new System.Windows.Forms.Label();
            this.pnDoid1 = new System.Windows.Forms.Panel();
            this.cbPicker1 = new System.Windows.Forms.ComboBox();
            this.tbVal1 = new System.Windows.Forms.TextBox();
            this.cbDataOwner1 = new System.Windows.Forms.ComboBox();
            this.pnDoid3 = new System.Windows.Forms.Panel();
            this.cbPicker3 = new System.Windows.Forms.ComboBox();
            this.tbVal3 = new System.Windows.Forms.TextBox();
            this.cbDataOwner3 = new System.Windows.Forms.ComboBox();
            this.pnDoid2 = new System.Windows.Forms.Panel();
            this.cbPicker2 = new System.Windows.Forms.ComboBox();
            this.tbVal2 = new System.Windows.Forms.TextBox();
            this.cbDataOwner2 = new System.Windows.Forms.ComboBox();
            this.lbInventory = new System.Windows.Forms.Label();
            this.lbDoid3 = new System.Windows.Forms.Label();
            this.cbInventory = new System.Windows.Forms.ComboBox();
            this.flpnGUID = new System.Windows.Forms.FlowLayoutPanel();
            this.tbGUID = new System.Windows.Forms.TextBox();
            this.tbObjName = new System.Windows.Forms.TextBox();
            this.lbDoid2 = new System.Windows.Forms.Label();
            this.lbGUID = new System.Windows.Forms.Label();
            this.pnWiz0x0033.SuspendLayout();
            this.tlpnGetSetValue.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flpnOperation.SuspendLayout();
            this.gbTokenTypes.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbInventoryType.SuspendLayout();
            this.flpnDoid0.SuspendLayout();
            this.pnDoid0.SuspendLayout();
            this.flpnInventoryType.SuspendLayout();
            this.pnDoid1.SuspendLayout();
            this.pnDoid3.SuspendLayout();
            this.pnDoid2.SuspendLayout();
            this.flpnGUID.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnWiz0x0033
            // 
            this.pnWiz0x0033.AutoSize = true;
            this.pnWiz0x0033.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnWiz0x0033.Controls.Add(this.tlpnGetSetValue);
            this.pnWiz0x0033.Location = new System.Drawing.Point(0, 0);
            this.pnWiz0x0033.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnWiz0x0033.Name = "pnWiz0x0033";
            this.pnWiz0x0033.Size = new System.Drawing.Size(562, 299);
            this.pnWiz0x0033.TabIndex = 0;
            // 
            // tlpnGetSetValue
            // 
            this.tlpnGetSetValue.AutoSize = true;
            this.tlpnGetSetValue.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpnGetSetValue.ColumnCount = 2;
            this.tlpnGetSetValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpnGetSetValue.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpnGetSetValue.Controls.Add(this.flowLayoutPanel1, 1, 7);
            this.tlpnGetSetValue.Controls.Add(this.lbOperation, 0, 0);
            this.tlpnGetSetValue.Controls.Add(this.flpnOperation, 1, 0);
            this.tlpnGetSetValue.Controls.Add(this.gbTokenTypes, 0, 6);
            this.tlpnGetSetValue.Controls.Add(this.gbInventoryType, 1, 6);
            this.tlpnGetSetValue.Controls.Add(this.lbDoid1, 0, 1);
            this.tlpnGetSetValue.Controls.Add(this.pnDoid1, 1, 1);
            this.tlpnGetSetValue.Controls.Add(this.pnDoid3, 1, 5);
            this.tlpnGetSetValue.Controls.Add(this.pnDoid2, 1, 4);
            this.tlpnGetSetValue.Controls.Add(this.lbInventory, 0, 2);
            this.tlpnGetSetValue.Controls.Add(this.lbDoid3, 0, 5);
            this.tlpnGetSetValue.Controls.Add(this.cbInventory, 1, 2);
            this.tlpnGetSetValue.Controls.Add(this.flpnGUID, 1, 3);
            this.tlpnGetSetValue.Controls.Add(this.lbDoid2, 0, 4);
            this.tlpnGetSetValue.Controls.Add(this.lbGUID, 0, 3);
            this.tlpnGetSetValue.Location = new System.Drawing.Point(0, 0);
            this.tlpnGetSetValue.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tlpnGetSetValue.Name = "tlpnGetSetValue";
            this.tlpnGetSetValue.RowCount = 9;
            this.tlpnGetSetValue.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnGetSetValue.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnGetSetValue.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnGetSetValue.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnGetSetValue.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnGetSetValue.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnGetSetValue.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnGetSetValue.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnGetSetValue.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpnGetSetValue.Size = new System.Drawing.Size(560, 297);
            this.tlpnGetSetValue.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.ckbDecimal);
            this.flowLayoutPanel1.Controls.Add(this.ckbAttrPicker);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(159, 258);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(302, 21);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // ckbDecimal
            // 
            this.ckbDecimal.AutoSize = true;
            this.ckbDecimal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbDecimal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbDecimal.Location = new System.Drawing.Point(2, 2);
            this.ckbDecimal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbDecimal.Name = "ckbDecimal";
            this.ckbDecimal.Size = new System.Drawing.Size(177, 17);
            this.ckbDecimal.TabIndex = 5;
            this.ckbDecimal.Text = "Decimal (except Consts/GUIDs)";
            // 
            // ckbAttrPicker
            // 
            this.ckbAttrPicker.AutoSize = true;
            this.ckbAttrPicker.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbAttrPicker.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbAttrPicker.Location = new System.Drawing.Point(183, 2);
            this.ckbAttrPicker.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbAttrPicker.Name = "ckbAttrPicker";
            this.ckbAttrPicker.Size = new System.Drawing.Size(117, 17);
            this.ckbAttrPicker.TabIndex = 6;
            this.ckbAttrPicker.Text = "use Attribute picker";
            // 
            // lbOperation
            // 
            this.lbOperation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbOperation.AutoSize = true;
            this.lbOperation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbOperation.Location = new System.Drawing.Point(102, 2);
            this.lbOperation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lbOperation.Name = "lbOperation";
            this.lbOperation.Size = new System.Drawing.Size(53, 13);
            this.lbOperation.TabIndex = 0;
            this.lbOperation.Text = "Operation";
            // 
            // flpnOperation
            // 
            this.flpnOperation.AutoSize = true;
            this.flpnOperation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpnOperation.Controls.Add(this.cbOperation);
            this.flpnOperation.Controls.Add(this.ckbReversed);
            this.flpnOperation.Location = new System.Drawing.Point(157, 0);
            this.flpnOperation.Margin = new System.Windows.Forms.Padding(0);
            this.flpnOperation.Name = "flpnOperation";
            this.flpnOperation.Size = new System.Drawing.Size(403, 21);
            this.flpnOperation.TabIndex = 1;
            // 
            // cbOperation
            // 
            this.cbOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperation.DropDownWidth = 480;
            this.cbOperation.FormattingEnabled = true;
            this.cbOperation.Location = new System.Drawing.Point(0, 0);
            this.cbOperation.Margin = new System.Windows.Forms.Padding(0);
            this.cbOperation.Name = "cbOperation";
            this.cbOperation.Size = new System.Drawing.Size(305, 21);
            this.cbOperation.TabIndex = 1;
            this.cbOperation.SelectedIndexChanged += new System.EventHandler(this.cbOperation_SelectedIndexChanged);
            // 
            // ckbReversed
            // 
            this.ckbReversed.AutoSize = true;
            this.ckbReversed.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbReversed.Location = new System.Drawing.Point(329, 2);
            this.ckbReversed.Margin = new System.Windows.Forms.Padding(24, 2, 2, 2);
            this.ckbReversed.Name = "ckbReversed";
            this.ckbReversed.Size = new System.Drawing.Size(72, 17);
            this.ckbReversed.TabIndex = 2;
            this.ckbReversed.Text = "Reversed";
            this.ckbReversed.UseVisualStyleBackColor = true;
            // 
            // gbTokenTypes
            // 
            this.gbTokenTypes.AutoSize = true;
            this.gbTokenTypes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbTokenTypes.Controls.Add(this.tableLayoutPanel1);
            this.gbTokenTypes.Location = new System.Drawing.Point(2, 133);
            this.gbTokenTypes.Margin = new System.Windows.Forms.Padding(2, 2, 4, 2);
            this.gbTokenTypes.Name = "gbTokenTypes";
            this.gbTokenTypes.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbTokenTypes.Size = new System.Drawing.Size(151, 121);
            this.gbTokenTypes.TabIndex = 7;
            this.gbTokenTypes.TabStop = false;
            this.gbTokenTypes.Text = "Category";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.ckbTTAll, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.ckbTTInvShopping, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.ckbTTShopping, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ckbTTInvMemory, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ckbTTMemory, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ckbTTInvVisible, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbTTVisible, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 17);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(143, 87);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ckbTTAll
            // 
            this.ckbTTAll.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ckbTTAll.AutoSize = true;
            this.ckbTTAll.Checked = true;
            this.ckbTTAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbTTAll.Enabled = false;
            this.ckbTTAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbTTAll.Location = new System.Drawing.Point(19, 68);
            this.ckbTTAll.Margin = new System.Windows.Forms.Padding(2, 5, 2, 2);
            this.ckbTTAll.Name = "ckbTTAll";
            this.ckbTTAll.Size = new System.Drawing.Size(37, 17);
            this.ckbTTAll.TabIndex = 0;
            this.ckbTTAll.TabStop = false;
            this.ckbTTAll.Text = "All";
            this.ckbTTAll.UseVisualStyleBackColor = true;
            // 
            // ckbTTInvShopping
            // 
            this.ckbTTInvShopping.AutoSize = true;
            this.ckbTTInvShopping.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbTTInvShopping.Location = new System.Drawing.Point(77, 44);
            this.ckbTTInvShopping.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbTTInvShopping.Name = "ckbTTInvShopping";
            this.ckbTTInvShopping.Size = new System.Drawing.Size(64, 17);
            this.ckbTTInvShopping.TabIndex = 6;
            this.ckbTTInvShopping.Text = "Exclude";
            this.ckbTTInvShopping.UseVisualStyleBackColor = true;
            // 
            // ckbTTShopping
            // 
            this.ckbTTShopping.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbTTShopping.AutoSize = true;
            this.ckbTTShopping.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbTTShopping.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbTTShopping.Location = new System.Drawing.Point(2, 44);
            this.ckbTTShopping.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbTTShopping.Name = "ckbTTShopping";
            this.ckbTTShopping.Size = new System.Drawing.Size(71, 17);
            this.ckbTTShopping.TabIndex = 5;
            this.ckbTTShopping.Text = "Shopping";
            this.ckbTTShopping.UseVisualStyleBackColor = true;
            this.ckbTTShopping.CheckedChanged += new System.EventHandler(this.ckbTT_CheckedChanged);
            // 
            // ckbTTInvMemory
            // 
            this.ckbTTInvMemory.AutoSize = true;
            this.ckbTTInvMemory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbTTInvMemory.Location = new System.Drawing.Point(77, 23);
            this.ckbTTInvMemory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbTTInvMemory.Name = "ckbTTInvMemory";
            this.ckbTTInvMemory.Size = new System.Drawing.Size(64, 17);
            this.ckbTTInvMemory.TabIndex = 4;
            this.ckbTTInvMemory.Text = "Exclude";
            this.ckbTTInvMemory.UseVisualStyleBackColor = true;
            // 
            // ckbTTMemory
            // 
            this.ckbTTMemory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbTTMemory.AutoSize = true;
            this.ckbTTMemory.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbTTMemory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbTTMemory.Location = new System.Drawing.Point(10, 23);
            this.ckbTTMemory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbTTMemory.Name = "ckbTTMemory";
            this.ckbTTMemory.Size = new System.Drawing.Size(63, 17);
            this.ckbTTMemory.TabIndex = 3;
            this.ckbTTMemory.Text = "Memory";
            this.ckbTTMemory.UseVisualStyleBackColor = true;
            this.ckbTTMemory.CheckedChanged += new System.EventHandler(this.ckbTT_CheckedChanged);
            // 
            // ckbTTInvVisible
            // 
            this.ckbTTInvVisible.AutoSize = true;
            this.ckbTTInvVisible.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbTTInvVisible.Location = new System.Drawing.Point(77, 2);
            this.ckbTTInvVisible.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbTTInvVisible.Name = "ckbTTInvVisible";
            this.ckbTTInvVisible.Size = new System.Drawing.Size(64, 17);
            this.ckbTTInvVisible.TabIndex = 2;
            this.ckbTTInvVisible.Text = "Exclude";
            this.ckbTTInvVisible.UseVisualStyleBackColor = true;
            // 
            // ckbTTVisible
            // 
            this.ckbTTVisible.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbTTVisible.AutoSize = true;
            this.ckbTTVisible.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbTTVisible.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbTTVisible.Location = new System.Drawing.Point(17, 2);
            this.ckbTTVisible.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbTTVisible.Name = "ckbTTVisible";
            this.ckbTTVisible.Size = new System.Drawing.Size(56, 17);
            this.ckbTTVisible.TabIndex = 1;
            this.ckbTTVisible.Text = "Visible";
            this.ckbTTVisible.UseVisualStyleBackColor = true;
            this.ckbTTVisible.CheckedChanged += new System.EventHandler(this.ckbTT_CheckedChanged);
            // 
            // gbInventoryType
            // 
            this.gbInventoryType.AutoSize = true;
            this.gbInventoryType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbInventoryType.Controls.Add(this.flpnDoid0);
            this.gbInventoryType.Controls.Add(this.flpnInventoryType);
            this.gbInventoryType.Location = new System.Drawing.Point(157, 133);
            this.gbInventoryType.Margin = new System.Windows.Forms.Padding(0, 2, 2, 2);
            this.gbInventoryType.Name = "gbInventoryType";
            this.gbInventoryType.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbInventoryType.Size = new System.Drawing.Size(392, 79);
            this.gbInventoryType.TabIndex = 8;
            this.gbInventoryType.TabStop = false;
            this.gbInventoryType.Text = "Target inventory";
            // 
            // flpnDoid0
            // 
            this.flpnDoid0.AutoSize = true;
            this.flpnDoid0.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpnDoid0.Controls.Add(this.lbDoid0);
            this.flpnDoid0.Controls.Add(this.pnDoid0);
            this.flpnDoid0.Location = new System.Drawing.Point(4, 39);
            this.flpnDoid0.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flpnDoid0.Name = "flpnDoid0";
            this.flpnDoid0.Size = new System.Drawing.Size(384, 23);
            this.flpnDoid0.TabIndex = 2;
            // 
            // lbDoid0
            // 
            this.lbDoid0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDoid0.AutoSize = true;
            this.lbDoid0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbDoid0.Location = new System.Drawing.Point(2, 2);
            this.lbDoid0.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lbDoid0.Name = "lbDoid0";
            this.lbDoid0.Size = new System.Drawing.Size(35, 13);
            this.lbDoid0.TabIndex = 0;
            this.lbDoid0.Tag = "";
            this.lbDoid0.Text = "Doid0";
            // 
            // pnDoid0
            // 
            this.pnDoid0.AutoSize = true;
            this.pnDoid0.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnDoid0.Controls.Add(this.cbPicker0);
            this.pnDoid0.Controls.Add(this.tbVal0);
            this.pnDoid0.Controls.Add(this.cbDataOwner0);
            this.pnDoid0.Location = new System.Drawing.Point(39, 0);
            this.pnDoid0.Margin = new System.Windows.Forms.Padding(0);
            this.pnDoid0.Name = "pnDoid0";
            this.pnDoid0.Size = new System.Drawing.Size(345, 23);
            this.pnDoid0.TabIndex = 22;
            // 
            // cbPicker0
            // 
            this.cbPicker0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker0.DropDownWidth = 384;
            this.cbPicker0.Location = new System.Drawing.Point(225, 0);
            this.cbPicker0.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbPicker0.Name = "cbPicker0";
            this.cbPicker0.Size = new System.Drawing.Size(118, 21);
            this.cbPicker0.TabIndex = 2;
            this.cbPicker0.TabStop = false;
            this.cbPicker0.Visible = false;
            // 
            // tbVal0
            // 
            this.tbVal0.Location = new System.Drawing.Point(225, 0);
            this.tbVal0.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbVal0.Name = "tbVal0";
            this.tbVal0.Size = new System.Drawing.Size(102, 20);
            this.tbVal0.TabIndex = 2;
            this.tbVal0.TabStop = false;
            // 
            // cbDataOwner0
            // 
            this.cbDataOwner0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner0.DropDownWidth = 384;
            this.cbDataOwner0.Location = new System.Drawing.Point(0, 0);
            this.cbDataOwner0.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbDataOwner0.Name = "cbDataOwner0";
            this.cbDataOwner0.Size = new System.Drawing.Size(228, 21);
            this.cbDataOwner0.TabIndex = 1;
            // 
            // flpnInventoryType
            // 
            this.flpnInventoryType.AutoSize = true;
            this.flpnInventoryType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpnInventoryType.Controls.Add(this.rb1Counted);
            this.flpnInventoryType.Controls.Add(this.rb1Singular);
            this.flpnInventoryType.Controls.Add(this.cbTargetInv);
            this.flpnInventoryType.Location = new System.Drawing.Point(4, 17);
            this.flpnInventoryType.Margin = new System.Windows.Forms.Padding(2, 2, 14, 2);
            this.flpnInventoryType.Name = "flpnInventoryType";
            this.flpnInventoryType.Size = new System.Drawing.Size(264, 22);
            this.flpnInventoryType.TabIndex = 1;
            // 
            // rb1Counted
            // 
            this.rb1Counted.AutoSize = true;
            this.rb1Counted.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rb1Counted.Location = new System.Drawing.Point(2, 2);
            this.rb1Counted.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rb1Counted.Name = "rb1Counted";
            this.rb1Counted.Size = new System.Drawing.Size(65, 17);
            this.rb1Counted.TabIndex = 1;
            this.rb1Counted.TabStop = true;
            this.rb1Counted.Text = "Counted";
            this.rb1Counted.UseVisualStyleBackColor = true;
            this.rb1Counted.CheckedChanged += new System.EventHandler(this.rb1_CheckedChanged);
            // 
            // rb1Singular
            // 
            this.rb1Singular.AutoSize = true;
            this.rb1Singular.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rb1Singular.Location = new System.Drawing.Point(71, 2);
            this.rb1Singular.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rb1Singular.Name = "rb1Singular";
            this.rb1Singular.Size = new System.Drawing.Size(63, 17);
            this.rb1Singular.TabIndex = 2;
            this.rb1Singular.TabStop = true;
            this.rb1Singular.Text = "Singular";
            this.rb1Singular.UseVisualStyleBackColor = true;
            this.rb1Singular.CheckedChanged += new System.EventHandler(this.rb1_CheckedChanged);
            // 
            // cbTargetInv
            // 
            this.cbTargetInv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTargetInv.FormattingEnabled = true;
            this.cbTargetInv.Location = new System.Drawing.Point(136, 1);
            this.cbTargetInv.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.cbTargetInv.Name = "cbTargetInv";
            this.cbTargetInv.Size = new System.Drawing.Size(128, 21);
            this.cbTargetInv.TabIndex = 3;
            this.cbTargetInv.SelectedIndexChanged += new System.EventHandler(this.cbTargetInv_SelectedIndexChanged);
            // 
            // lbDoid1
            // 
            this.lbDoid1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDoid1.AutoSize = true;
            this.lbDoid1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbDoid1.Location = new System.Drawing.Point(120, 23);
            this.lbDoid1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lbDoid1.Name = "lbDoid1";
            this.lbDoid1.Size = new System.Drawing.Size(35, 13);
            this.lbDoid1.TabIndex = 0;
            this.lbDoid1.Text = "Doid1";
            // 
            // pnDoid1
            // 
            this.pnDoid1.AutoSize = true;
            this.pnDoid1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnDoid1.Controls.Add(this.cbPicker1);
            this.pnDoid1.Controls.Add(this.tbVal1);
            this.pnDoid1.Controls.Add(this.cbDataOwner1);
            this.pnDoid1.Location = new System.Drawing.Point(157, 21);
            this.pnDoid1.Margin = new System.Windows.Forms.Padding(0);
            this.pnDoid1.Name = "pnDoid1";
            this.pnDoid1.Size = new System.Drawing.Size(345, 23);
            this.pnDoid1.TabIndex = 2;
            // 
            // cbPicker1
            // 
            this.cbPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker1.DropDownWidth = 384;
            this.cbPicker1.Location = new System.Drawing.Point(225, 0);
            this.cbPicker1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbPicker1.Name = "cbPicker1";
            this.cbPicker1.Size = new System.Drawing.Size(118, 21);
            this.cbPicker1.TabIndex = 2;
            this.cbPicker1.TabStop = false;
            this.cbPicker1.Visible = false;
            // 
            // tbVal1
            // 
            this.tbVal1.Location = new System.Drawing.Point(225, 0);
            this.tbVal1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbVal1.Name = "tbVal1";
            this.tbVal1.Size = new System.Drawing.Size(102, 20);
            this.tbVal1.TabIndex = 2;
            this.tbVal1.TabStop = false;
            // 
            // cbDataOwner1
            // 
            this.cbDataOwner1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner1.DropDownWidth = 384;
            this.cbDataOwner1.Location = new System.Drawing.Point(0, 0);
            this.cbDataOwner1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbDataOwner1.Name = "cbDataOwner1";
            this.cbDataOwner1.Size = new System.Drawing.Size(228, 21);
            this.cbDataOwner1.TabIndex = 1;
            // 
            // pnDoid3
            // 
            this.pnDoid3.AutoSize = true;
            this.pnDoid3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnDoid3.Controls.Add(this.cbPicker3);
            this.pnDoid3.Controls.Add(this.tbVal3);
            this.pnDoid3.Controls.Add(this.cbDataOwner3);
            this.pnDoid3.Location = new System.Drawing.Point(157, 108);
            this.pnDoid3.Margin = new System.Windows.Forms.Padding(0);
            this.pnDoid3.Name = "pnDoid3";
            this.pnDoid3.Size = new System.Drawing.Size(345, 23);
            this.pnDoid3.TabIndex = 6;
            // 
            // cbPicker3
            // 
            this.cbPicker3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker3.DropDownWidth = 384;
            this.cbPicker3.Location = new System.Drawing.Point(225, 0);
            this.cbPicker3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbPicker3.Name = "cbPicker3";
            this.cbPicker3.Size = new System.Drawing.Size(118, 21);
            this.cbPicker3.TabIndex = 2;
            this.cbPicker3.TabStop = false;
            this.cbPicker3.Visible = false;
            // 
            // tbVal3
            // 
            this.tbVal3.Location = new System.Drawing.Point(225, 0);
            this.tbVal3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbVal3.Name = "tbVal3";
            this.tbVal3.Size = new System.Drawing.Size(102, 20);
            this.tbVal3.TabIndex = 2;
            this.tbVal3.TabStop = false;
            // 
            // cbDataOwner3
            // 
            this.cbDataOwner3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner3.DropDownWidth = 384;
            this.cbDataOwner3.Location = new System.Drawing.Point(0, 0);
            this.cbDataOwner3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbDataOwner3.Name = "cbDataOwner3";
            this.cbDataOwner3.Size = new System.Drawing.Size(228, 21);
            this.cbDataOwner3.TabIndex = 1;
            // 
            // pnDoid2
            // 
            this.pnDoid2.AutoSize = true;
            this.pnDoid2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnDoid2.Controls.Add(this.cbPicker2);
            this.pnDoid2.Controls.Add(this.tbVal2);
            this.pnDoid2.Controls.Add(this.cbDataOwner2);
            this.pnDoid2.Location = new System.Drawing.Point(157, 85);
            this.pnDoid2.Margin = new System.Windows.Forms.Padding(0);
            this.pnDoid2.Name = "pnDoid2";
            this.pnDoid2.Size = new System.Drawing.Size(345, 23);
            this.pnDoid2.TabIndex = 5;
            // 
            // cbPicker2
            // 
            this.cbPicker2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker2.DropDownWidth = 384;
            this.cbPicker2.Location = new System.Drawing.Point(225, 0);
            this.cbPicker2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbPicker2.Name = "cbPicker2";
            this.cbPicker2.Size = new System.Drawing.Size(118, 21);
            this.cbPicker2.TabIndex = 2;
            this.cbPicker2.TabStop = false;
            this.cbPicker2.Visible = false;
            // 
            // tbVal2
            // 
            this.tbVal2.Location = new System.Drawing.Point(225, 0);
            this.tbVal2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbVal2.Name = "tbVal2";
            this.tbVal2.Size = new System.Drawing.Size(102, 20);
            this.tbVal2.TabIndex = 2;
            this.tbVal2.TabStop = false;
            // 
            // cbDataOwner2
            // 
            this.cbDataOwner2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner2.DropDownWidth = 384;
            this.cbDataOwner2.Location = new System.Drawing.Point(0, 0);
            this.cbDataOwner2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbDataOwner2.Name = "cbDataOwner2";
            this.cbDataOwner2.Size = new System.Drawing.Size(228, 21);
            this.cbDataOwner2.TabIndex = 1;
            // 
            // lbInventory
            // 
            this.lbInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbInventory.AutoSize = true;
            this.lbInventory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbInventory.Location = new System.Drawing.Point(79, 46);
            this.lbInventory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lbInventory.Name = "lbInventory";
            this.lbInventory.Size = new System.Drawing.Size(76, 13);
            this.lbInventory.TabIndex = 0;
            this.lbInventory.Tag = "";
            this.lbInventory.Text = "From inventory";
            // 
            // lbDoid3
            // 
            this.lbDoid3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDoid3.AutoSize = true;
            this.lbDoid3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbDoid3.Location = new System.Drawing.Point(120, 110);
            this.lbDoid3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lbDoid3.Name = "lbDoid3";
            this.lbDoid3.Size = new System.Drawing.Size(35, 13);
            this.lbDoid3.TabIndex = 0;
            this.lbDoid3.Tag = "";
            this.lbDoid3.Text = "Doid3";
            // 
            // cbInventory
            // 
            this.cbInventory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInventory.DropDownWidth = 384;
            this.cbInventory.Location = new System.Drawing.Point(157, 44);
            this.cbInventory.Margin = new System.Windows.Forms.Padding(0);
            this.cbInventory.Name = "cbInventory";
            this.cbInventory.Size = new System.Drawing.Size(96, 21);
            this.cbInventory.TabIndex = 3;
            this.cbInventory.SelectedIndexChanged += new System.EventHandler(this.cbInventory_SelectedIndexChanged);
            // 
            // flpnGUID
            // 
            this.flpnGUID.AutoSize = true;
            this.flpnGUID.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpnGUID.Controls.Add(this.tbGUID);
            this.flpnGUID.Controls.Add(this.tbObjName);
            this.flpnGUID.Location = new System.Drawing.Point(157, 65);
            this.flpnGUID.Margin = new System.Windows.Forms.Padding(0);
            this.flpnGUID.Name = "flpnGUID";
            this.flpnGUID.Size = new System.Drawing.Size(344, 20);
            this.flpnGUID.TabIndex = 4;
            // 
            // tbGUID
            // 
            this.tbGUID.Location = new System.Drawing.Point(0, 0);
            this.tbGUID.Margin = new System.Windows.Forms.Padding(0);
            this.tbGUID.Name = "tbGUID";
            this.tbGUID.Size = new System.Drawing.Size(78, 20);
            this.tbGUID.TabIndex = 1;
            this.tbGUID.Text = "0xDDDDDDDD";
            this.tbGUID.TextChanged += new System.EventHandler(this.tbGUID_TextChanged);
            this.tbGUID.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbGUID.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            // 
            // tbObjName
            // 
            this.tbObjName.Location = new System.Drawing.Point(78, 0);
            this.tbObjName.Margin = new System.Windows.Forms.Padding(0);
            this.tbObjName.Name = "tbObjName";
            this.tbObjName.ReadOnly = true;
            this.tbObjName.Size = new System.Drawing.Size(266, 20);
            this.tbObjName.TabIndex = 0;
            this.tbObjName.TabStop = false;
            // 
            // lbDoid2
            // 
            this.lbDoid2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDoid2.AutoSize = true;
            this.lbDoid2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbDoid2.Location = new System.Drawing.Point(120, 87);
            this.lbDoid2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lbDoid2.Name = "lbDoid2";
            this.lbDoid2.Size = new System.Drawing.Size(35, 13);
            this.lbDoid2.TabIndex = 0;
            this.lbDoid2.Tag = "";
            this.lbDoid2.Text = "Doid2";
            // 
            // lbGUID
            // 
            this.lbGUID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbGUID.AutoSize = true;
            this.lbGUID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbGUID.Location = new System.Drawing.Point(87, 67);
            this.lbGUID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lbGUID.Name = "lbGUID";
            this.lbGUID.Size = new System.Drawing.Size(68, 13);
            this.lbGUID.TabIndex = 0;
            this.lbGUID.Tag = "";
            this.lbGUID.Text = "Token GUID";
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(572, 339);
            this.Controls.Add(this.pnWiz0x0033);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UI";
            this.Text = "UI";
            this.pnWiz0x0033.ResumeLayout(false);
            this.pnWiz0x0033.PerformLayout();
            this.tlpnGetSetValue.ResumeLayout(false);
            this.tlpnGetSetValue.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flpnOperation.ResumeLayout(false);
            this.flpnOperation.PerformLayout();
            this.gbTokenTypes.ResumeLayout(false);
            this.gbTokenTypes.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.gbInventoryType.ResumeLayout(false);
            this.gbInventoryType.PerformLayout();
            this.flpnDoid0.ResumeLayout(false);
            this.flpnDoid0.PerformLayout();
            this.pnDoid0.ResumeLayout(false);
            this.pnDoid0.PerformLayout();
            this.flpnInventoryType.ResumeLayout(false);
            this.flpnInventoryType.PerformLayout();
            this.pnDoid1.ResumeLayout(false);
            this.pnDoid1.PerformLayout();
            this.pnDoid3.ResumeLayout(false);
            this.pnDoid3.PerformLayout();
            this.pnDoid2.ResumeLayout(false);
            this.pnDoid2.PerformLayout();
            this.flpnGUID.ResumeLayout(false);
            this.flpnGUID.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void rb1_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            internalchg = true;

            if (rb1Counted.Checked) doCounted(); else doSingular();

            internalchg = false;
        }

        private void cbOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            operation = (byte)cbOperation.SelectedIndex;
            rb1_CheckedChanged(sender, e);
        }

        private void tbGUID_TextChanged(object sender, EventArgs e)
        {
            if (internalchg) return;

            if (!hex32_IsValid(sender)) return;
            setGUID(false, Convert.ToUInt32(((TextBox)sender).Text, 16));
        }

        private void hex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (hex32_IsValid(sender)) return;

            e.Cancel = true;

            bool origstate = internalchg;
            internalchg = true;
            byte[] o = { inst.Operands[0x05], inst.Operands[0x06], inst.Operands[0x07], inst.Reserved1[0] };
            setGUID(o, 0);
            ((TextBox)sender).SelectAll();
            internalchg = origstate;
        }

        private void hex32_Validated(object sender, System.EventArgs e)
        {
            bool origstate = internalchg;
            internalchg = true;

            ((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(Convert.ToUInt32(((TextBox)sender).Text, 16));
            ((TextBox)sender).SelectAll();

            UInt32 i = Convert.ToUInt32(((TextBox)sender).Text, 16);
            o5678[0] = (byte)(i & 0xff);
            o5678[1] = (byte)((i >> 8) & 0xff);
            o5678[2] = (byte)((i >> 16) & 0xff);
            o5678[3] = (byte)((i >> 24) & 0xff);
            refreshDoid1();
            doFromInventory(false);

            internalchg = origstate;
        }

        private void ckbTT_CheckedChanged(object sender, EventArgs e)
        {
            List<CheckBox> tt = new List<CheckBox>(new CheckBox[] { ckbTTVisible, ckbTTMemory, ckbTTShopping });
            List<CheckBox> tti = new List<CheckBox>(new CheckBox[] { ckbTTInvVisible, ckbTTInvMemory, ckbTTInvShopping });
            int i = tt.IndexOf((CheckBox)sender);
            tti[i].Enabled = tt[i].Checked;
            ckbTTAll.Checked = !ckbTTVisible.Checked && !ckbTTMemory.Checked && !ckbTTShopping.Checked;
        }

        private void cbTargetInv_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnDoid0.Enabled = (cbTargetInv.SelectedIndex >= 1 && cbTargetInv.SelectedIndex <= 3);
            lbDoid0.Text = pnDoid0.Enabled ? cbTargetInv.SelectedItem.ToString() : "";
        }

        private void cbInventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool origstate = internalchg;
            internalchg = true;

            if (cbInventory.SelectedIndex >= 0 && cbInventory.SelectedIndex <= 7)
                o5678[1] = (byte)((o5678[1] & 0xf8) + cbInventory.SelectedIndex);
            refreshDoid1();

            pnDoid3.Enabled = (cbInventory.SelectedIndex >= 1 && cbInventory.SelectedIndex <= 3);
            lbDoid3.Text = pnDoid3.Enabled ? cbInventory.SelectedItem.ToString() : "";

            internalchg = origstate;
        }
    }

}

namespace pjse.BhavOperandWizards
{
    public class BhavOperandWiz0x0033 : pjse.ABhavOperandWiz
	{
        public BhavOperandWiz0x0033(Instruction i) : base(i) { myForm = new Wiz0x0033.UI(); }

		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm = null;
		}
		#endregion

	}

}
