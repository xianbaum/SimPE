/***************************************************************************
 *   Copyright (C) 2007 by Peter L Jones                                   *
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
    internal class UI : System.Windows.Forms.Form
    {
        #region Form variables

        internal System.Windows.Forms.Panel pnWiz0x0033;
        private FlowLayoutPanel flpnOperation;
        private Label lbOperation;
        private ComboBox cbOperation;
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
        private CheckBox ckbReversed;
        private ComboBox cbInventory;
        private Label lbInventory;
        private FlowLayoutPanel flpnGUID;
        private TextBox tbGUID;
        private TextBox tbObjName;
        private GroupBox gbTokenTypes;
        private CheckBox ckbTTAll;
        private TableLayoutPanel tableLayoutPanel1;
        private CheckBox ckbTTInvShopping;
        private CheckBox ckbTTShopping;
        private CheckBox ckbTTInvMemory;
        private CheckBox ckbTTMemory;
        private CheckBox ckbTTInvVisible;
        private CheckBox ckbTTVisible;
        private GroupBox gbInventoryType;
        private ComboBox cbTargetInv;
        private FlowLayoutPanel flpnInventoryType;
        private RadioButton rb1Counted;
        private RadioButton rb1Singular;
        private Panel pnDoidOptions;
        private CheckBox ckbAttrPicker;
        private CheckBox ckbDecimal;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label lbDoid0;
        private Panel pnDoid0;
        private ComboBox cbPicker0;
        private TextBox tbVal0;
        private ComboBox cbDataOwner0;
        /// <summary>
        /// Erforderliche Designervariable.
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

            cbTargetInv.Items.AddRange(BhavWiz.readStr(GS.BhavStr.InventoryType).ToArray());
            cbInventory.Items.AddRange(BhavWiz.readStr(GS.BhavStr.InventoryType).ToArray());
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


        #region UI

        #region static data
        static List<String> aTokenOpsCounted = BhavWiz.readStr(GS.BhavStr.TokenOpsCounted);
        static List<String> aTokenOpsSingular = BhavWiz.readStr(GS.BhavStr.TokenOpsSingular);
        static bool[][] aDoidsCounted = {
                new bool[] { false, false, false, false,  false, false, false, false,  false, false, false, false, }, // Doid0
                new bool[] { false, false, false, false,  false, false, false, false,  false, false, false, false, }, // Doid1
                new bool[] { false, true , false, true ,  false, true , false, false,  true , true , false, true , }, // Doid2
                new bool[] { true , true , true , true ,  false, false, true , false,  false, false, false, false, }, // Doid3
            };
        static bool[][] aDoidsSingular = {
                new bool[] { false, false, false, false,  false, false, false, false,  false, false, false, false,  false, false, false, false,  false, false, false, false, }, // Doid0
                new bool[] { false, false, false, false,  false, false, false, false,  false, false, false, false,  false, false, true , true ,  false, false, false, false, }, // Doid1
                new bool[] { false, true , false, true ,  true , true , true , true ,  false, false, false, false,  true , false, true , true ,  false, false, true , false, }, // Doid2
                new bool[] { false, false, false, false,  true , true , false, true ,  false, false, true , false,  false, true , true , true ,  false, false, false, false, }, // Doid3
            };
        static String[] names = { "", "Object", "bwp33_index", "bwp33_property", "bwp33_count", "Value" };
        static int[][] aNamesCounted = {
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, }, // Doid0
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, }, // Doid1
            new int[] { 0, 2, 0, 2, 0, 2, 0, 0, 2, 2, 0, 2, }, // Doid2
            new int[] { 4, 4, 4, 4, 0, 0, 4, 0, 0, 0, 4, 1, }, // Doid3
        };
        static int[][] aNamesSingular = {
            new int[] { 0, 0, 0, 0,  0, 0, 0, 0,  0, 0, 0, 0,  0, 0, 2, 2,  0, 0, 0, 0, }, // Doid0
            new int[] { 0, 0, 0, 0,  0, 0, 0, 0,  0, 0, 0, 0,  0, 0, 2, 2,  0, 0, 0, 0, }, // Doid1
            new int[] { 0, 2, 0, 2,  2, 2, 2, 2,  0, 0, 0, 0,  2, 0, 3, 3,  0, 0, 2, 0, }, // Doid2
            new int[] { 0, 0, 0, 0,  3, 3, 0, 3,  0, 0, 4, 0,  0, 4, 5, 5,  0, 0, 1, 0, }, // Doid3
        };
        #endregion

        private bool internalchg = false;

        private Instruction inst = null;

        private DataOwnerControl doid0 = null; // o[1], o[2], o[3]
        private DataOwnerControl doid1 = null; // o[6], o[7], o[8]
        private DataOwnerControl doid2 = null; // o[10], o[11], o[12]
        private DataOwnerControl doid3 = null; // o[13], o[14], o[15]

        private void doTokenOps(List<String> tokenops)
        {
            int i = cbOperation.SelectedIndex;
            cbOperation.Items.Clear();
            cbOperation.Items.AddRange(tokenops.ToArray());
            cbOperation.SelectedIndex = (i > cbOperation.Items.Count) ? -1 : i;
        }

        private void doTokenType(bool all, bool allowTypes)
        {
            ckbTTAll.Enabled = true;
            ckbTTAll.Checked = all;
            if (gbTokenTypes.Enabled = !ckbTTAll.Checked && allowTypes)
            {
                ckbTTInvShopping.Enabled = ckbTTInvMemory.Enabled = ckbTTInvVisible.Enabled = false;
                if (ckbTTVisible.Checked = option2[2]) { ckbTTInvVisible.Enabled = true; ckbTTInvVisible.Checked = option1[4]; }
                if (ckbTTMemory.Checked = option2[3]) { ckbTTInvMemory.Enabled = true; ckbTTInvMemory.Checked = option1[5]; }
                if (ckbTTShopping.Checked = option2[5]) { ckbTTInvShopping.Enabled = true; ckbTTInvShopping.Checked = option2[0]; }
            }
        }

        private void doFromInventory()
        {
            cbInventory.Enabled = true;
            int i = (inst.Operands[6] & 0x07);
            cbInventory.SelectedIndex = (i < cbInventory.Items.Count) ? i : -1;
            pnDoid3.Enabled = (i >= 1 && i <= 3);
        }

        private void doByGUID()
        {
            byte[] o1 = inst.Operands;
            byte[] o2 = inst.Reserved1;

            flpnGUID.Enabled = true;
            uint d1 = (uint)(o1[5] | (o1[6] << 8) | (o1[7] << 16) | (o2[0] << 24));
            tbGUID.Text = "0x" + SimPe.Helper.HexString(d1);
            tbObjName.Text = (d1 == 0 ? BhavWiz.dnStkOb() : BhavWiz.FormatGUID(true, d1));
        }

        private void doCounted()
        {
            doTokenOps(aTokenOpsCounted);
            pnDoid0.Enabled = pnDoid1.Enabled = pnDoid2.Enabled = pnDoid3.Enabled = false;
            gbTokenTypes.Enabled = ckbTTAll.Enabled = ckbReversed.Enabled = false;
            cbInventory.Enabled = false;
            flpnGUID.Enabled = false;

            byte op = inst.Operands[4];

            if (op < aDoidsCounted[0].Length)
            {
                pnDoid1.Enabled = aDoidsCounted[1][op];
                pnDoid2.Enabled = aDoidsCounted[2][op];
                pnDoid3.Enabled = aDoidsCounted[3][op];
            }
            if (op < aNamesCounted[0].Length)
            {
                lbDoid1.Text = aNamesCounted[1][op] > 0 ? pjse.Localization.GetString(names[aNamesCounted[1][op]]) : "";
                lbDoid2.Text = aNamesCounted[2][op] > 0 ? pjse.Localization.GetString(names[aNamesCounted[2][op]]) : "";
                lbDoid3.Text = aNamesCounted[3][op] > 0 ? pjse.Localization.GetString(names[aNamesCounted[3][op]]) : "";
            }

            switch (op)
            {
                case 0x00: doByGUID(); break;
                case 0x02: doByGUID(); break;
                case 0x04: doTokenType(true, false); break;
                case 0x06: doByGUID(); break;
                case 0x07: doByGUID(); break;
                case 0x09: doByGUID(); break;
                case 0x0b: doFromInventory(); break;
            }
        }

        private void doSingular()
        {
            doTokenOps(aTokenOpsSingular);
            pnDoid0.Enabled = pnDoid1.Enabled = pnDoid2.Enabled = pnDoid3.Enabled = false;
            gbTokenTypes.Enabled = ckbTTAll.Enabled = ckbReversed.Enabled = false;
            cbInventory.Enabled = false;
            flpnGUID.Enabled = false;

            byte op = inst.Operands[4];

            if (op < aDoidsSingular[0].Length)
            {
                pnDoid1.Enabled = aDoidsSingular[1][op];
                pnDoid2.Enabled = aDoidsSingular[2][op];
                pnDoid3.Enabled = aDoidsSingular[3][op];
            }
            if (op < aNamesSingular[0].Length)
            {
                lbDoid1.Text = aNamesSingular[1][op] > 0 ? pjse.Localization.GetString(names[aNamesSingular[1][op]]) : "";
                lbDoid2.Text = aNamesSingular[2][op] > 0 ? pjse.Localization.GetString(names[aNamesSingular[2][op]]) : "";
                lbDoid3.Text = aNamesSingular[3][op] > 0 ? pjse.Localization.GetString(names[aNamesSingular[3][op]]) : "";
            }

            switch (op)
            {
                case 0x00: doByGUID(); break;
                case 0x02: doTokenType(true, true); break;
                case 0x03: doTokenType(false, false); ckbReversed.Enabled = true; break;
                case 0x06: doByGUID(); break;
                case 0x0c: doTokenType(false, false); ckbReversed.Enabled = true; break;
                case 0x0d: doTokenType(true, true); break;
                case 0x12: doFromInventory(); break;
            }
        }


        private Boolset option1 = 0;
        private Boolset option2 = 0;
        public void Execute(Instruction inst)
        {
            this.inst = inst;

            wrappedByteArray ops1 = inst.Operands;
            wrappedByteArray ops2 = inst.Reserved1;
            internalchg = true;

            option1 = (inst.NodeVersion >= 1) ? ops1[0] : (byte)(((ops1[0] & 0x3C) << 1) | (ops1[0] & 0x83));
            option2 = (inst.NodeVersion >= 2) ? ops2[1] : (byte)0x0c;

            doid0 = new DataOwnerControl(inst, cbDataOwner0, cbPicker0, tbVal0,
                ckbDecimal, ckbAttrPicker, null, ops1[1], BhavWiz.ToShort(ops1[2], ops1[3]));

            doid1 = new DataOwnerControl(inst, cbDataOwner1, cbPicker1, tbVal1,
                ckbDecimal, ckbAttrPicker, null, ops1[6], BhavWiz.ToShort(ops1[7], ops2[0]));

            doid2 = new DataOwnerControl(inst, cbDataOwner2, cbPicker2, tbVal2,
                ckbDecimal, ckbAttrPicker, null, ops2[2], BhavWiz.ToShort(ops2[3], ops2[4]));

            doid3 = new DataOwnerControl(inst, cbDataOwner3, cbPicker3, tbVal3,
                ckbDecimal, ckbAttrPicker, null, ops2[5], BhavWiz.ToShort(ops2[6], ops2[7]));

            if (option1[3])
            {
                rb1Counted.Checked = true;
                doCounted();
            }
            else
            {
                rb1Counted.Checked = false;
                doSingular();
            }
            if (ops1[4] < cbOperation.Items.Count)
                cbOperation.SelectedIndex = ops1[4];
            ckbReversed.Checked = option1[7];
            rb1Singular.Checked = !rb1Counted.Checked;

            cbTargetInv.SelectedIndex = ((option1 & 0x07) < cbTargetInv.Items.Count) ? option1 & 0x07 : -1;
            pnDoid0.Enabled = ((option1 & 0x07) >= 1 && (option1 & 0x07) <= 3);
            lbDoid0.Text = pnDoid0.Enabled ? pjse.Localization.GetString(names[1]) : "";

            internalchg = false;
        }


        public Instruction Write(Instruction inst)
        {
            if (inst != null)
            {
                wrappedByteArray ops1 = inst.Operands;
                wrappedByteArray ops2 = inst.Reserved1;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI));
            this.pnWiz0x0033 = new System.Windows.Forms.Panel();
            this.ckbReversed = new System.Windows.Forms.CheckBox();
            this.tlpnGetSetValue = new System.Windows.Forms.TableLayoutPanel();
            this.gbTokenTypes = new System.Windows.Forms.GroupBox();
            this.ckbTTAll = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ckbTTInvShopping = new System.Windows.Forms.CheckBox();
            this.ckbTTShopping = new System.Windows.Forms.CheckBox();
            this.ckbTTInvMemory = new System.Windows.Forms.CheckBox();
            this.ckbTTMemory = new System.Windows.Forms.CheckBox();
            this.ckbTTInvVisible = new System.Windows.Forms.CheckBox();
            this.ckbTTVisible = new System.Windows.Forms.CheckBox();
            this.gbInventoryType = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbDoid0 = new System.Windows.Forms.Label();
            this.pnDoid0 = new System.Windows.Forms.Panel();
            this.cbPicker0 = new System.Windows.Forms.ComboBox();
            this.tbVal0 = new System.Windows.Forms.TextBox();
            this.cbDataOwner0 = new System.Windows.Forms.ComboBox();
            this.cbTargetInv = new System.Windows.Forms.ComboBox();
            this.flpnInventoryType = new System.Windows.Forms.FlowLayoutPanel();
            this.rb1Counted = new System.Windows.Forms.RadioButton();
            this.rb1Singular = new System.Windows.Forms.RadioButton();
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
            this.lbGUID = new System.Windows.Forms.Label();
            this.lbDoid3 = new System.Windows.Forms.Label();
            this.lbDoid2 = new System.Windows.Forms.Label();
            this.cbInventory = new System.Windows.Forms.ComboBox();
            this.flpnGUID = new System.Windows.Forms.FlowLayoutPanel();
            this.tbGUID = new System.Windows.Forms.TextBox();
            this.tbObjName = new System.Windows.Forms.TextBox();
            this.pnDoidOptions = new System.Windows.Forms.Panel();
            this.ckbAttrPicker = new System.Windows.Forms.CheckBox();
            this.ckbDecimal = new System.Windows.Forms.CheckBox();
            this.flpnOperation = new System.Windows.Forms.FlowLayoutPanel();
            this.lbOperation = new System.Windows.Forms.Label();
            this.cbOperation = new System.Windows.Forms.ComboBox();
            this.pnWiz0x0033.SuspendLayout();
            this.tlpnGetSetValue.SuspendLayout();
            this.gbTokenTypes.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbInventoryType.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.pnDoid0.SuspendLayout();
            this.flpnInventoryType.SuspendLayout();
            this.pnDoid1.SuspendLayout();
            this.pnDoid3.SuspendLayout();
            this.pnDoid2.SuspendLayout();
            this.flpnGUID.SuspendLayout();
            this.pnDoidOptions.SuspendLayout();
            this.flpnOperation.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnWiz0x0033
            // 
            resources.ApplyResources(this.pnWiz0x0033, "pnWiz0x0033");
            this.pnWiz0x0033.Controls.Add(this.ckbReversed);
            this.pnWiz0x0033.Controls.Add(this.tlpnGetSetValue);
            this.pnWiz0x0033.Controls.Add(this.flpnOperation);
            this.pnWiz0x0033.Name = "pnWiz0x0033";
            // 
            // ckbReversed
            // 
            resources.ApplyResources(this.ckbReversed, "ckbReversed");
            this.ckbReversed.Name = "ckbReversed";
            this.ckbReversed.UseVisualStyleBackColor = true;
            // 
            // tlpnGetSetValue
            // 
            resources.ApplyResources(this.tlpnGetSetValue, "tlpnGetSetValue");
            this.tlpnGetSetValue.Controls.Add(this.gbTokenTypes, 0, 6);
            this.tlpnGetSetValue.Controls.Add(this.gbInventoryType, 0, 6);
            this.tlpnGetSetValue.Controls.Add(this.lbDoid1, 0, 0);
            this.tlpnGetSetValue.Controls.Add(this.pnDoid1, 1, 0);
            this.tlpnGetSetValue.Controls.Add(this.pnDoid3, 1, 4);
            this.tlpnGetSetValue.Controls.Add(this.pnDoid2, 1, 3);
            this.tlpnGetSetValue.Controls.Add(this.lbInventory, 0, 1);
            this.tlpnGetSetValue.Controls.Add(this.lbGUID, 0, 2);
            this.tlpnGetSetValue.Controls.Add(this.lbDoid3, 0, 4);
            this.tlpnGetSetValue.Controls.Add(this.lbDoid2, 0, 3);
            this.tlpnGetSetValue.Controls.Add(this.cbInventory, 1, 1);
            this.tlpnGetSetValue.Controls.Add(this.flpnGUID, 1, 2);
            this.tlpnGetSetValue.Controls.Add(this.pnDoidOptions, 1, 5);
            this.tlpnGetSetValue.Name = "tlpnGetSetValue";
            // 
            // gbTokenTypes
            // 
            resources.ApplyResources(this.gbTokenTypes, "gbTokenTypes");
            this.gbTokenTypes.Controls.Add(this.ckbTTAll);
            this.gbTokenTypes.Controls.Add(this.tableLayoutPanel1);
            this.gbTokenTypes.Name = "gbTokenTypes";
            this.gbTokenTypes.TabStop = false;
            // 
            // ckbTTAll
            // 
            resources.ApplyResources(this.ckbTTAll, "ckbTTAll");
            this.ckbTTAll.Name = "ckbTTAll";
            this.ckbTTAll.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.ckbTTInvShopping, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.ckbTTShopping, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ckbTTInvMemory, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ckbTTMemory, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ckbTTInvVisible, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbTTVisible, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // ckbTTInvShopping
            // 
            resources.ApplyResources(this.ckbTTInvShopping, "ckbTTInvShopping");
            this.ckbTTInvShopping.Name = "ckbTTInvShopping";
            this.ckbTTInvShopping.UseVisualStyleBackColor = true;
            // 
            // ckbTTShopping
            // 
            resources.ApplyResources(this.ckbTTShopping, "ckbTTShopping");
            this.ckbTTShopping.Name = "ckbTTShopping";
            this.ckbTTShopping.UseVisualStyleBackColor = true;
            // 
            // ckbTTInvMemory
            // 
            resources.ApplyResources(this.ckbTTInvMemory, "ckbTTInvMemory");
            this.ckbTTInvMemory.Name = "ckbTTInvMemory";
            this.ckbTTInvMemory.UseVisualStyleBackColor = true;
            // 
            // ckbTTMemory
            // 
            resources.ApplyResources(this.ckbTTMemory, "ckbTTMemory");
            this.ckbTTMemory.Name = "ckbTTMemory";
            this.ckbTTMemory.UseVisualStyleBackColor = true;
            // 
            // ckbTTInvVisible
            // 
            resources.ApplyResources(this.ckbTTInvVisible, "ckbTTInvVisible");
            this.ckbTTInvVisible.Name = "ckbTTInvVisible";
            this.ckbTTInvVisible.UseVisualStyleBackColor = true;
            // 
            // ckbTTVisible
            // 
            resources.ApplyResources(this.ckbTTVisible, "ckbTTVisible");
            this.ckbTTVisible.Name = "ckbTTVisible";
            this.ckbTTVisible.UseVisualStyleBackColor = true;
            // 
            // gbInventoryType
            // 
            resources.ApplyResources(this.gbInventoryType, "gbInventoryType");
            this.gbInventoryType.Controls.Add(this.flowLayoutPanel1);
            this.gbInventoryType.Controls.Add(this.cbTargetInv);
            this.gbInventoryType.Controls.Add(this.flpnInventoryType);
            this.gbInventoryType.Name = "gbInventoryType";
            this.gbInventoryType.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.lbDoid0);
            this.flowLayoutPanel1.Controls.Add(this.pnDoid0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // lbDoid0
            // 
            resources.ApplyResources(this.lbDoid0, "lbDoid0");
            this.lbDoid0.Name = "lbDoid0";
            this.lbDoid0.Tag = "";
            // 
            // pnDoid0
            // 
            resources.ApplyResources(this.pnDoid0, "pnDoid0");
            this.pnDoid0.Controls.Add(this.cbPicker0);
            this.pnDoid0.Controls.Add(this.tbVal0);
            this.pnDoid0.Controls.Add(this.cbDataOwner0);
            this.pnDoid0.Name = "pnDoid0";
            // 
            // cbPicker0
            // 
            this.cbPicker0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker0.DropDownWidth = 384;
            resources.ApplyResources(this.cbPicker0, "cbPicker0");
            this.cbPicker0.Name = "cbPicker0";
            this.cbPicker0.TabStop = false;
            // 
            // tbVal0
            // 
            resources.ApplyResources(this.tbVal0, "tbVal0");
            this.tbVal0.Name = "tbVal0";
            this.tbVal0.TabStop = false;
            // 
            // cbDataOwner0
            // 
            this.cbDataOwner0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner0.DropDownWidth = 384;
            resources.ApplyResources(this.cbDataOwner0, "cbDataOwner0");
            this.cbDataOwner0.Name = "cbDataOwner0";
            // 
            // cbTargetInv
            // 
            this.cbTargetInv.FormattingEnabled = true;
            resources.ApplyResources(this.cbTargetInv, "cbTargetInv");
            this.cbTargetInv.Name = "cbTargetInv";
            // 
            // flpnInventoryType
            // 
            resources.ApplyResources(this.flpnInventoryType, "flpnInventoryType");
            this.flpnInventoryType.Controls.Add(this.rb1Counted);
            this.flpnInventoryType.Controls.Add(this.rb1Singular);
            this.flpnInventoryType.Name = "flpnInventoryType";
            // 
            // rb1Counted
            // 
            resources.ApplyResources(this.rb1Counted, "rb1Counted");
            this.rb1Counted.Name = "rb1Counted";
            this.rb1Counted.TabStop = true;
            this.rb1Counted.UseVisualStyleBackColor = true;
            // 
            // rb1Singular
            // 
            resources.ApplyResources(this.rb1Singular, "rb1Singular");
            this.rb1Singular.Name = "rb1Singular";
            this.rb1Singular.TabStop = true;
            this.rb1Singular.UseVisualStyleBackColor = true;
            // 
            // lbDoid1
            // 
            resources.ApplyResources(this.lbDoid1, "lbDoid1");
            this.lbDoid1.Name = "lbDoid1";
            // 
            // pnDoid1
            // 
            resources.ApplyResources(this.pnDoid1, "pnDoid1");
            this.pnDoid1.Controls.Add(this.cbPicker1);
            this.pnDoid1.Controls.Add(this.tbVal1);
            this.pnDoid1.Controls.Add(this.cbDataOwner1);
            this.pnDoid1.Name = "pnDoid1";
            // 
            // cbPicker1
            // 
            this.cbPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker1.DropDownWidth = 384;
            resources.ApplyResources(this.cbPicker1, "cbPicker1");
            this.cbPicker1.Name = "cbPicker1";
            this.cbPicker1.TabStop = false;
            // 
            // tbVal1
            // 
            resources.ApplyResources(this.tbVal1, "tbVal1");
            this.tbVal1.Name = "tbVal1";
            this.tbVal1.TabStop = false;
            // 
            // cbDataOwner1
            // 
            this.cbDataOwner1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner1.DropDownWidth = 384;
            resources.ApplyResources(this.cbDataOwner1, "cbDataOwner1");
            this.cbDataOwner1.Name = "cbDataOwner1";
            // 
            // pnDoid3
            // 
            resources.ApplyResources(this.pnDoid3, "pnDoid3");
            this.pnDoid3.Controls.Add(this.cbPicker3);
            this.pnDoid3.Controls.Add(this.tbVal3);
            this.pnDoid3.Controls.Add(this.cbDataOwner3);
            this.pnDoid3.Name = "pnDoid3";
            // 
            // cbPicker3
            // 
            this.cbPicker3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker3.DropDownWidth = 384;
            resources.ApplyResources(this.cbPicker3, "cbPicker3");
            this.cbPicker3.Name = "cbPicker3";
            this.cbPicker3.TabStop = false;
            // 
            // tbVal3
            // 
            resources.ApplyResources(this.tbVal3, "tbVal3");
            this.tbVal3.Name = "tbVal3";
            this.tbVal3.TabStop = false;
            // 
            // cbDataOwner3
            // 
            this.cbDataOwner3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner3.DropDownWidth = 384;
            resources.ApplyResources(this.cbDataOwner3, "cbDataOwner3");
            this.cbDataOwner3.Name = "cbDataOwner3";
            // 
            // pnDoid2
            // 
            resources.ApplyResources(this.pnDoid2, "pnDoid2");
            this.pnDoid2.Controls.Add(this.cbPicker2);
            this.pnDoid2.Controls.Add(this.tbVal2);
            this.pnDoid2.Controls.Add(this.cbDataOwner2);
            this.pnDoid2.Name = "pnDoid2";
            // 
            // cbPicker2
            // 
            this.cbPicker2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker2.DropDownWidth = 384;
            resources.ApplyResources(this.cbPicker2, "cbPicker2");
            this.cbPicker2.Name = "cbPicker2";
            this.cbPicker2.TabStop = false;
            // 
            // tbVal2
            // 
            resources.ApplyResources(this.tbVal2, "tbVal2");
            this.tbVal2.Name = "tbVal2";
            this.tbVal2.TabStop = false;
            // 
            // cbDataOwner2
            // 
            this.cbDataOwner2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner2.DropDownWidth = 384;
            resources.ApplyResources(this.cbDataOwner2, "cbDataOwner2");
            this.cbDataOwner2.Name = "cbDataOwner2";
            // 
            // lbInventory
            // 
            resources.ApplyResources(this.lbInventory, "lbInventory");
            this.lbInventory.Name = "lbInventory";
            this.lbInventory.Tag = "";
            // 
            // lbGUID
            // 
            resources.ApplyResources(this.lbGUID, "lbGUID");
            this.lbGUID.Name = "lbGUID";
            this.lbGUID.Tag = "";
            // 
            // lbDoid3
            // 
            resources.ApplyResources(this.lbDoid3, "lbDoid3");
            this.lbDoid3.Name = "lbDoid3";
            this.lbDoid3.Tag = "";
            // 
            // lbDoid2
            // 
            resources.ApplyResources(this.lbDoid2, "lbDoid2");
            this.lbDoid2.Name = "lbDoid2";
            this.lbDoid2.Tag = "";
            // 
            // cbInventory
            // 
            this.cbInventory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInventory.DropDownWidth = 384;
            resources.ApplyResources(this.cbInventory, "cbInventory");
            this.cbInventory.Name = "cbInventory";
            // 
            // flpnGUID
            // 
            resources.ApplyResources(this.flpnGUID, "flpnGUID");
            this.flpnGUID.Controls.Add(this.tbGUID);
            this.flpnGUID.Controls.Add(this.tbObjName);
            this.flpnGUID.Name = "flpnGUID";
            // 
            // tbGUID
            // 
            resources.ApplyResources(this.tbGUID, "tbGUID");
            this.tbGUID.Name = "tbGUID";
            // 
            // tbObjName
            // 
            resources.ApplyResources(this.tbObjName, "tbObjName");
            this.tbObjName.Name = "tbObjName";
            this.tbObjName.ReadOnly = true;
            // 
            // pnDoidOptions
            // 
            resources.ApplyResources(this.pnDoidOptions, "pnDoidOptions");
            this.pnDoidOptions.Controls.Add(this.ckbAttrPicker);
            this.pnDoidOptions.Controls.Add(this.ckbDecimal);
            this.pnDoidOptions.Name = "pnDoidOptions";
            // 
            // ckbAttrPicker
            // 
            resources.ApplyResources(this.ckbAttrPicker, "ckbAttrPicker");
            this.ckbAttrPicker.Name = "ckbAttrPicker";
            // 
            // ckbDecimal
            // 
            resources.ApplyResources(this.ckbDecimal, "ckbDecimal");
            this.ckbDecimal.Name = "ckbDecimal";
            // 
            // flpnOperation
            // 
            resources.ApplyResources(this.flpnOperation, "flpnOperation");
            this.flpnOperation.Controls.Add(this.lbOperation);
            this.flpnOperation.Controls.Add(this.cbOperation);
            this.flpnOperation.Name = "flpnOperation";
            // 
            // lbOperation
            // 
            resources.ApplyResources(this.lbOperation, "lbOperation");
            this.lbOperation.Name = "lbOperation";
            // 
            // cbOperation
            // 
            this.cbOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperation.FormattingEnabled = true;
            resources.ApplyResources(this.cbOperation, "cbOperation");
            this.cbOperation.Name = "cbOperation";
            // 
            // UI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnWiz0x0033);
            this.Name = "UI";
            this.pnWiz0x0033.ResumeLayout(false);
            this.pnWiz0x0033.PerformLayout();
            this.tlpnGetSetValue.ResumeLayout(false);
            this.tlpnGetSetValue.PerformLayout();
            this.gbTokenTypes.ResumeLayout(false);
            this.gbTokenTypes.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.gbInventoryType.ResumeLayout(false);
            this.gbInventoryType.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
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
            this.pnDoidOptions.ResumeLayout(false);
            this.pnDoidOptions.PerformLayout();
            this.flpnOperation.ResumeLayout(false);
            this.flpnOperation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void rb1_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            internalchg = true;

            if (rb1Counted.Checked) doCounted();
            else doSingular();

            internalchg = false;
        }
    }

}

namespace pjse.BhavOperandWizards
{
    public class BhavOperandWiz0x0033 : pjse.ABhavOperandWiz
	{
		public BhavOperandWiz0x0033() : base() { }

        public BhavOperandWiz0x0033(Instruction i) : base(i) { }


        private Wiz0x0033.UI myForm = null;
		public override Panel bhavPrimWizPanel
		{
			get
			{
                if (myForm == null) myForm = new Wiz0x0033.UI();
				return myForm.pnWiz0x0033;
			}
		}

		public override void Execute()
		{
			if (instruction != null) myForm.Execute(instruction);
		}

		public override Instruction Write()
		{
			return (instruction == null) ? null : myForm.Write(instruction);
		}


		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm = null;
		}
		#endregion

	}

}
