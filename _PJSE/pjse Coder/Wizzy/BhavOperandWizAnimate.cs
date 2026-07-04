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

namespace pjse.BhavOperandWizards.WizAnimate
{
    internal class UI : System.Windows.Forms.Form, iBhavOperandWizForm
    {
        #region Form variables

        internal System.Windows.Forms.Panel pnWizAnimate;
        private FlowLayoutPanel flpnMain;
        private Panel pnObject;
        private ComboBox cbPickerObject;
        private TextBox tbValObject;
        private ComboBox cbdoObject;
        private Label label1;
        private FlowLayoutPanel flpnAnimType;
        private Label label4;
        private TextBox tbValAnimType;
        private ComboBox cbAnimType;
        private TextBox tbAnimType;
        private FlowLayoutPanel flpnAnim;
        private Label lbParam;
        private TextBox tbValAnim;
        private Button btnAnim;
        private TextBox tbAnim;
        private FlowLayoutPanel flpnEventScope;
        private Label label2;
        private ComboBox cbEventScope;
        private FlowLayoutPanel flpnEventTree;
        private LinkLabel llEvent;
        private TextBox tbValEventTree;
        private Button btnEventTree;
        private TextBox tbEventTree;
        private FlowLayoutPanel flpnOptions;
        private GroupBox groupBox1;
        private FlowLayoutPanel flpnOptions1;
        private CheckBox ckbFlipped;
        private CheckBox ckbAnimSpeed;
        private CheckBox ckbParam;
        private CheckBox ckbInterruptible;
        private CheckBox ckbStartTag;
        private CheckBox ckbLoopCount;
        private CheckBox ckbTransToIdle;
        private CheckBox ckbBlendOut;
        private CheckBox ckbBlendIn;
        private GroupBox groupBox2;
        private FlowLayoutPanel flpnOptions2;
        private CheckBox ckbFlipTemp3;
        private CheckBox ckbSync;
        private CheckBox ckbAlignBlend;
        private CheckBox ckbControllerIsSource;
        private CheckBox ckbNotHurryable;
        private Panel pnDoidOptions;
        private CheckBox ckbAttrPicker;
        private CheckBox ckbDecimal;
        private Panel pnIKObject;
        private ComboBox cbPickerIK;
        private TextBox tbValIK;
        private ComboBox cbdoIK;
        private Label label3;
        private GroupBox gbPriority;
        private ComboBox cbPriority;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        #endregion


        /// <summary>
        /// Initialise the Wizard user interface
        /// </summary>
        /// <param name="mode">Specify whether the wizard is for Animate Object, Sim or Overlay</param>
        public UI(String mode)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            #region AnimNames
            cbAnimType.Items.Clear();
            cbAnimType.Items.AddRange(new  String[] {
                "AdultAnims",
                "ChildAnims",
                "SocialAnims",
                "LocoAnims",
                "ObjectAnims",
                "ToddlerAnims",
                "TeenAnims",
                "ElderAnims",
                "CatAnims",
                "DogAnims",
                "BabyAnims",
                "ReachAnims",
                "PuppyAnims",
                "KittenAnims",
                "SmallDogAnims",
                "ElderLargeDogAnims",
                "ElderSmallDogAnims",
                "ElderCatAnims",
            });
            // Two-byte values
            //cbAnimType.Items.AddRange(new String[] {
            //            "ObjectElderAnims",
            //            "ObjectTeenAnims",
            //            "ObjectChildAnims",
            //            "ObjectToddlerAnims",
            //            "ObjectLargeDogAnims",
            //            "ObjectCatAnims",
            //            "ObjectPuppyAnims",
            //            "ObjectKittenAnims",
            //            "ObjectSmallDogAnims",
            //        });
            #endregion

            this.mode = mode;
            switch (mode)
            {
                case "bwp_Object":
                    lckbOptions1 = new List<CheckBox>(new CheckBox[] {
                        ckbFlipped, ckbAnimSpeed, ckbParam, ckbInterruptible, ckbStartTag, ckbLoopCount, ckbBlendOut, ckbBlendIn
                    });
                    lckbOptions2 = new List<CheckBox>(new CheckBox[] {
                        ckbFlipTemp3, null, ckbSync, ckbAlignBlend, ckbNotHurryable
                    });
                    this.flpnMain.Controls.Remove(flpnAnimType);
                    this.flpnMain.Controls.Remove(pnIKObject);
                    this.flpnOptions.Controls.Remove(gbPriority);
                    break;
                case "bwp_Sim":
                    lckbOptions1 = new List<CheckBox>(new CheckBox[] {
                        ckbFlipped, ckbAnimSpeed, ckbParam, ckbInterruptible, ckbStartTag, ckbTransToIdle, ckbBlendOut, ckbBlendIn
                    });
                    lckbOptions2 = new List<CheckBox>(new CheckBox[] {
                        ckbFlipTemp3, ckbSync, null, null, ckbSync, ckbControllerIsSource, ckbNotHurryable
                    });
                    this.flpnMain.Controls.Remove(pnObject);
                    break;
                case "bwp_Overlay":
                    lckbOptions1 = new List<CheckBox>(new CheckBox[] {
                        ckbFlipped, ckbAnimSpeed, ckbParam, ckbInterruptible, ckbStartTag, ckbLoopCount, ckbBlendOut, ckbBlendIn
                    });
                    lckbOptions2 = new List<CheckBox>(new CheckBox[] {
                        ckbFlipTemp3, null, null, null, ckbSync, ckbAlignBlend
                    });
                    this.flpnMain.Controls.Remove(pnIKObject);
                    break;
                default:
                    throw new ArgumentException("Argument must match bwp_{Object,Sim,Overlay}", "mode");
            }
            lckb = new List<CheckBox>(new CheckBox[] {
                ckbAnimSpeed, ckbInterruptible, ckbStartTag, ckbLoopCount,
                ckbTransToIdle, ckbBlendOut, ckbBlendIn, ckbFlipTemp3,
                ckbSync, ckbAlignBlend, ckbControllerIsSource, ckbNotHurryable
            });

            pnWizAnimate.Height = flpnOptions.Bottom;
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


        private String mode = "";
        private Instruction inst = null;

        private DataOwnerControl doidObject = null;
        private DataOwnerControl doidAnim = null;
        private DataOwnerControl doidEvent = null;
        private DataOwnerControl doidAnimType = null;
        private DataOwnerControl doidIK = null;

        private bool internalchg = false;

        private List<CheckBox> lckbOptions1;
        private List<CheckBox> lckbOptions2;
        private List<CheckBox> lckb;

        private void doCkbParam()
        {
            if (ckbParam.Checked)
            {
                lbParam.Text = ((String)lbParam.Tag).Split('|')[0];
            }
            else
            {
                lbParam.Text = ((String)lbParam.Tag).Split('|')[1];
                doStrValue(doidAnim.Value, tbAnim);
            }
            btnAnim.Visible = tbAnim.Visible = !ckbParam.Checked;
        }

        private void doStrChooser(TextBox tbVal, TextBox strText)
        {
            pjse.FileTable.Entry[] items =
                pjse.FileTable.GFT[(uint)SimPe.Data.MetaData.STRING_FILE, inst.Parent.GroupForScope(AnimScope()), (uint)AnimInstance()];

            if (items == null || items.Length == 0)
            {
                MessageBox.Show(pjse.Localization.GetString("bow_noStrings")
                    + " (" + pjse.Localization.GetString(AnimScope().ToString()) + ")");
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
                doStrValue((ushort)i, strText);
                internalchg = savedState;
            }
        }

        private bool IsAnim(ushort i)
        {
            try { return IsAnim((GS.GlobalStr)i); }
            catch { }
            return false;
        }
        private bool IsAnim(GS.GlobalStr g) { return IsAnim(g.ToString()); }
        private bool IsAnim(String s) { return s.EndsWith("Anims"); }

        private Scope AnimScope()
        {
            if (mode.Equals("bwp_Object")) return Scope.Private;
            return (this.doidAnimType.Value == 0x80) ? Scope.Global : Scope.Private;
        }

        private GS.GlobalStr AnimInstance()
        {
            if (mode.Equals("bwp_Object")) return GS.GlobalStr.ObjectAnims;

            if (this.doidAnimType.Value == 0x80) return GS.GlobalStr.AdultAnims;
            if (IsAnim(this.doidAnimType.Value)) return (GS.GlobalStr)this.doidAnimType.Value;
            return GS.GlobalStr.ObjectAnims;
        }

        private void doStrValue(ushort strno, TextBox strText)
        {
            strText.Text = ((BhavWiz)inst).readStr(AnimScope(), AnimInstance(), strno, -1, pjse.Detail.ErrorNames);
        }

        private void doidAnimType_DataOwnerControlChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            internalchg = true;

            try
            {
                cbAnimType.SelectedIndex = cbAnimType.Items.IndexOf(((GS.GlobalStr)doidAnimType.Value).ToString());
                tbAnimType.Text = (cbAnimType.SelectedIndex >= 0) ? this.cbAnimType.SelectedItem.ToString() : "---";
            }
            finally
            {
                internalchg = false;
                doStrValue(doidAnim.Value, tbAnim);
            }
        }

        private void doidAnim_DataOwnerControlChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            doStrValue(doidAnim.Value, tbAnim);
        }

        private void doidEvent_DataOwnerControlChanged(object sender, EventArgs e)
        {
            bool found = false;
            tbEventTree.Text = pjse.BhavWiz.bhavName(inst.Parent, doidEvent.Value, ref found);
            if (!found) tbEventTree.Text = "---";
            llEvent.Enabled = found;
        }

        private byte getScope(byte scope)
        {
            return (byte)((cbEventScope.SelectedIndex >= 0) ? cbEventScope.SelectedIndex : scope);
        }

        private byte getPriority(byte priority)
        {
            return (byte)((cbPriority.SelectedIndex >= 0) ? cbPriority.SelectedIndex : priority);
        }

        private byte getOptions(List<CheckBox> lckbOptions, Boolset options)
        {
            for (int i = 0; i < lckbOptions.Count; i++)
                if (lckbOptions[i] != null) options[i] = lckbOptions[i].Checked;
            return options;
        }

        #region iBhavOperandWizForm
        public Panel WizPanel { get { return this.pnWizAnimate; } }

        public void Execute(Instruction inst)
        {
            this.inst = inst;

            wrappedByteArray ops1 = inst.Operands;
            wrappedByteArray ops2 = inst.Reserved1;
            Boolset options1 = null;
            Boolset options2 = null;
            int scope = 0;
            int priority = -1;

            internalchg = true;

            foreach (CheckBox c in lckb) c.Visible = false;

            doidAnim = new DataOwnerControl(inst, null, null, tbValAnim,
                ckbDecimal, ckbAttrPicker, null, 0x07, BhavWiz.ToShort(ops1[0], ops1[1]));
            doidAnim.DataOwnerControlChanged += new EventHandler(doidAnim_DataOwnerControlChanged);

            options1 = ops1[2];

            doidEvent = new DataOwnerControl(inst, null, null, tbValEventTree,
                ckbDecimal, ckbAttrPicker, null, 0x07, BhavWiz.ToShort(ops1[4], ops1[5]));
            doidEvent.DataOwnerControlChanged += new EventHandler(doidEvent_DataOwnerControlChanged);

            switch (mode)
            {
                case "bwp_Object":
                    doidObject = new DataOwnerControl(inst, cbdoObject, cbPickerObject, tbValObject,
                        ckbDecimal, ckbAttrPicker, null, ops1[6], BhavWiz.ToShort(ops1[7], ops2[0]));
                    scope = ops2[1];
                    options2 = ops2[2];
                    break;

                case "bwp_Sim":
                    doidAnimType = new DataOwnerControl(inst, null, null, tbValAnimType,
                        ckbDecimal, ckbAttrPicker, null, 0x07, (byte)ops1[6]);
                    doidAnimType.DataOwnerControlChanged += new EventHandler(doidAnimType_DataOwnerControlChanged);
                    scope = ops1[7];
                    options2 = ops2[0];
                    doidIK = new DataOwnerControl(inst, cbdoIK, cbPickerIK, tbValIK,
                        ckbDecimal, ckbAttrPicker, null, ops2[1], BhavWiz.ToShort(ops2[2], ops2[3]));
                    priority = ops2[4];
                    break;

                case "bwp_Overlay":
                    doidObject = new DataOwnerControl(inst, cbdoObject, cbPickerObject, tbValObject,
                        ckbDecimal, ckbAttrPicker, null, ops1[6], BhavWiz.ToShort(ops1[7], ops2[0]));
                    doidAnimType = new DataOwnerControl(inst, null, null, tbValAnimType,
                        ckbDecimal, ckbAttrPicker, null, 0x07, (byte)ops2[1]);
                    doidAnimType.DataOwnerControlChanged += new EventHandler(doidAnimType_DataOwnerControlChanged);
                    if (inst.NodeVersion != 0)
                    {
                        priority = ops2[3];
                        ckbNotHurryable.Checked = (ops2[4] & 0x01) != 0;
                        ckbNotHurryable.Visible = true;
                    }
                    else
                        priority = ops2[4];
                    scope = ops2[6];
                    options2 = ops2[7];
                    break;
            }

            for (int i = 0; i < lckbOptions1.Count; i++)
                if (lckbOptions1[i] != null)
                {
                    lckbOptions1[i].Visible = true;
                    lckbOptions1[i].Checked = options1[i];
                }

            for (int i = 0; i < lckbOptions2.Count; i++)
                if (lckbOptions2[i] != null)
                {
                    lckbOptions2[i].Visible = true;
                    lckbOptions2[i].Checked = options2[i];
                }

            switch (scope)
            {
                case 0: cbEventScope.SelectedIndex = 0; break;
                case 1: cbEventScope.SelectedIndex = 1; break;
                default: cbEventScope.SelectedIndex = 2; break;
            }

            internalchg = false;

            if (!mode.Equals("bwp_Object"))
                doidAnimType_DataOwnerControlChanged(null, null);
            else
                doidAnim_DataOwnerControlChanged(null, null);
            doidEvent_DataOwnerControlChanged(null, null);
            ckbParam_CheckedChanged(null, null);
            ckbFlipTemp3_CheckedChanged(null, null);
            if (priority < cbPriority.Items.Count)
                cbPriority.SelectedIndex = priority;
        }

        public Instruction Write(Instruction inst)
        {
            if (inst != null)
            {
                wrappedByteArray ops1 = inst.Operands;
                wrappedByteArray ops2 = inst.Reserved1;

                BhavWiz.FromShort(ref ops1, 0, doidAnim.Value);

                ops1[2] = getOptions(lckbOptions1, ops1[2]);

                BhavWiz.FromShort(ref ops1, 4, doidEvent.Value);
                byte[] lohi = { 0, 0 };

                switch (mode)
                {
                    case "bwp_Object":
                        ops1[6] = doidObject.DataOwner;
                        BhavWiz.FromShort(ref lohi, 0, doidObject.Value);
                        ops1[7] = lohi[0];
                        ops2[0] = lohi[1];
                        ops2[1] = getScope(ops2[1]);
                        ops2[2] = getOptions(lckbOptions2, ops2[2]);
                        break;

                    case "bwp_Sim":
                        ops1[6] = (byte)(doidAnimType.Value & 0xff);
                        ops1[7] = getScope(ops1[7]);
                        ops2[0] = getOptions(lckbOptions2, ops2[0]);
                        ops2[1] = doidIK.DataOwner;
                        BhavWiz.FromShort(ref ops2, 2, doidIK.Value);
                        ops2[4] = getPriority(ops2[4]);
                        break;

                    case "bwp_Overlay":
                        ops1[6] = doidObject.DataOwner;
                        BhavWiz.FromShort(ref lohi, 0, doidObject.Value);
                        ops1[7] = lohi[0];
                        ops2[0] = lohi[1];
                        ops2[1] = (byte)(doidAnimType.Value & 0xff);

                        if (inst.NodeVersion != 0)
                        {
                            ops2[3] = getPriority(ops2[3]);
                            Boolset options3 = ops2[4];
                            options3[0] = ckbNotHurryable.Checked;
                            ops2[4] = options3;
                        }
                        else
                            ops2[4] = getPriority(ops2[4]);

                        ops2[6] = getScope(ops2[6]);
                        ops2[7] = getOptions(lckbOptions2, ops2[7]);
                        break;
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
            this.pnWizAnimate = new System.Windows.Forms.Panel();
            this.flpnMain = new System.Windows.Forms.FlowLayoutPanel();
            this.pnObject = new System.Windows.Forms.Panel();
            this.cbPickerObject = new System.Windows.Forms.ComboBox();
            this.tbValObject = new System.Windows.Forms.TextBox();
            this.cbdoObject = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnIKObject = new System.Windows.Forms.Panel();
            this.cbPickerIK = new System.Windows.Forms.ComboBox();
            this.tbValIK = new System.Windows.Forms.TextBox();
            this.cbdoIK = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnDoidOptions = new System.Windows.Forms.Panel();
            this.ckbAttrPicker = new System.Windows.Forms.CheckBox();
            this.ckbDecimal = new System.Windows.Forms.CheckBox();
            this.flpnAnimType = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.tbValAnimType = new System.Windows.Forms.TextBox();
            this.cbAnimType = new System.Windows.Forms.ComboBox();
            this.tbAnimType = new System.Windows.Forms.TextBox();
            this.flpnAnim = new System.Windows.Forms.FlowLayoutPanel();
            this.lbParam = new System.Windows.Forms.Label();
            this.tbValAnim = new System.Windows.Forms.TextBox();
            this.btnAnim = new System.Windows.Forms.Button();
            this.tbAnim = new System.Windows.Forms.TextBox();
            this.flpnEventScope = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.cbEventScope = new System.Windows.Forms.ComboBox();
            this.flpnEventTree = new System.Windows.Forms.FlowLayoutPanel();
            this.llEvent = new System.Windows.Forms.LinkLabel();
            this.tbValEventTree = new System.Windows.Forms.TextBox();
            this.btnEventTree = new System.Windows.Forms.Button();
            this.tbEventTree = new System.Windows.Forms.TextBox();
            this.flpnOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flpnOptions1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ckbFlipped = new System.Windows.Forms.CheckBox();
            this.ckbAnimSpeed = new System.Windows.Forms.CheckBox();
            this.ckbParam = new System.Windows.Forms.CheckBox();
            this.ckbInterruptible = new System.Windows.Forms.CheckBox();
            this.ckbStartTag = new System.Windows.Forms.CheckBox();
            this.ckbLoopCount = new System.Windows.Forms.CheckBox();
            this.ckbTransToIdle = new System.Windows.Forms.CheckBox();
            this.ckbBlendOut = new System.Windows.Forms.CheckBox();
            this.ckbBlendIn = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flpnOptions2 = new System.Windows.Forms.FlowLayoutPanel();
            this.ckbFlipTemp3 = new System.Windows.Forms.CheckBox();
            this.ckbSync = new System.Windows.Forms.CheckBox();
            this.ckbAlignBlend = new System.Windows.Forms.CheckBox();
            this.ckbControllerIsSource = new System.Windows.Forms.CheckBox();
            this.ckbNotHurryable = new System.Windows.Forms.CheckBox();
            this.gbPriority = new System.Windows.Forms.GroupBox();
            this.cbPriority = new System.Windows.Forms.ComboBox();
            this.pnWizAnimate.SuspendLayout();
            this.flpnMain.SuspendLayout();
            this.pnObject.SuspendLayout();
            this.pnIKObject.SuspendLayout();
            this.pnDoidOptions.SuspendLayout();
            this.flpnAnimType.SuspendLayout();
            this.flpnAnim.SuspendLayout();
            this.flpnEventScope.SuspendLayout();
            this.flpnEventTree.SuspendLayout();
            this.flpnOptions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flpnOptions1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flpnOptions2.SuspendLayout();
            this.gbPriority.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnWizAnimate
            // 
            this.pnWizAnimate.AutoSize = true;
            this.pnWizAnimate.Controls.Add(this.flpnMain);
            this.pnWizAnimate.Location = new System.Drawing.Point(0, 0);
            this.pnWizAnimate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnWizAnimate.Name = "pnWizAnimate";
            this.pnWizAnimate.Size = new System.Drawing.Size(429, 404);
            this.pnWizAnimate.TabIndex = 0;
            // 
            // flpnMain
            // 
            this.flpnMain.AutoSize = true;
            this.flpnMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpnMain.Controls.Add(this.pnObject);
            this.flpnMain.Controls.Add(this.pnIKObject);
            this.flpnMain.Controls.Add(this.pnDoidOptions);
            this.flpnMain.Controls.Add(this.flpnAnimType);
            this.flpnMain.Controls.Add(this.flpnAnim);
            this.flpnMain.Controls.Add(this.flpnEventScope);
            this.flpnMain.Controls.Add(this.flpnEventTree);
            this.flpnMain.Controls.Add(this.flpnOptions);
            this.flpnMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpnMain.Location = new System.Drawing.Point(0, 0);
            this.flpnMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flpnMain.Name = "flpnMain";
            this.flpnMain.Size = new System.Drawing.Size(427, 399);
            this.flpnMain.TabIndex = 9;
            // 
            // pnObject
            // 
            this.pnObject.Controls.Add(this.cbPickerObject);
            this.pnObject.Controls.Add(this.tbValObject);
            this.pnObject.Controls.Add(this.cbdoObject);
            this.pnObject.Controls.Add(this.label1);
            this.pnObject.Location = new System.Drawing.Point(0, 0);
            this.pnObject.Margin = new System.Windows.Forms.Padding(0);
            this.pnObject.Name = "pnObject";
            this.pnObject.Size = new System.Drawing.Size(424, 24);
            this.pnObject.TabIndex = 1;
            // 
            // cbPickerObject
            // 
            this.cbPickerObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPickerObject.DropDownWidth = 384;
            this.cbPickerObject.Location = new System.Drawing.Point(297, 2);
            this.cbPickerObject.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbPickerObject.Name = "cbPickerObject";
            this.cbPickerObject.Size = new System.Drawing.Size(126, 21);
            this.cbPickerObject.TabIndex = 2;
            this.cbPickerObject.TabStop = false;
            this.cbPickerObject.Visible = false;
            // 
            // tbValObject
            // 
            this.tbValObject.Location = new System.Drawing.Point(297, 2);
            this.tbValObject.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbValObject.Name = "tbValObject";
            this.tbValObject.Size = new System.Drawing.Size(108, 20);
            this.tbValObject.TabIndex = 2;
            this.tbValObject.TabStop = false;
            // 
            // cbdoObject
            // 
            this.cbdoObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbdoObject.DropDownWidth = 384;
            this.cbdoObject.Location = new System.Drawing.Point(57, 2);
            this.cbdoObject.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbdoObject.Name = "cbdoObject";
            this.cbdoObject.Size = new System.Drawing.Size(242, 21);
            this.cbdoObject.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(0, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Object";
            // 
            // pnIKObject
            // 
            this.pnIKObject.Controls.Add(this.cbPickerIK);
            this.pnIKObject.Controls.Add(this.tbValIK);
            this.pnIKObject.Controls.Add(this.cbdoIK);
            this.pnIKObject.Controls.Add(this.label3);
            this.pnIKObject.Location = new System.Drawing.Point(0, 24);
            this.pnIKObject.Margin = new System.Windows.Forms.Padding(0);
            this.pnIKObject.Name = "pnIKObject";
            this.pnIKObject.Size = new System.Drawing.Size(424, 24);
            this.pnIKObject.TabIndex = 1;
            // 
            // cbPickerIK
            // 
            this.cbPickerIK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPickerIK.DropDownWidth = 384;
            this.cbPickerIK.Location = new System.Drawing.Point(297, 2);
            this.cbPickerIK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbPickerIK.Name = "cbPickerIK";
            this.cbPickerIK.Size = new System.Drawing.Size(126, 21);
            this.cbPickerIK.TabIndex = 2;
            this.cbPickerIK.TabStop = false;
            this.cbPickerIK.Visible = false;
            // 
            // tbValIK
            // 
            this.tbValIK.Location = new System.Drawing.Point(297, 2);
            this.tbValIK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbValIK.Name = "tbValIK";
            this.tbValIK.Size = new System.Drawing.Size(108, 20);
            this.tbValIK.TabIndex = 2;
            this.tbValIK.TabStop = false;
            // 
            // cbdoIK
            // 
            this.cbdoIK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbdoIK.DropDownWidth = 384;
            this.cbdoIK.Location = new System.Drawing.Point(57, 2);
            this.cbdoIK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbdoIK.Name = "cbdoIK";
            this.cbdoIK.Size = new System.Drawing.Size(242, 21);
            this.cbdoIK.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(0, 5);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "IK Object";
            // 
            // pnDoidOptions
            // 
            this.pnDoidOptions.Controls.Add(this.ckbAttrPicker);
            this.pnDoidOptions.Controls.Add(this.ckbDecimal);
            this.pnDoidOptions.Location = new System.Drawing.Point(2, 50);
            this.pnDoidOptions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnDoidOptions.Name = "pnDoidOptions";
            this.pnDoidOptions.Size = new System.Drawing.Size(419, 18);
            this.pnDoidOptions.TabIndex = 2;
            // 
            // ckbAttrPicker
            // 
            this.ckbAttrPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbAttrPicker.AutoSize = true;
            this.ckbAttrPicker.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbAttrPicker.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbAttrPicker.Location = new System.Drawing.Point(300, 0);
            this.ckbAttrPicker.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbAttrPicker.Name = "ckbAttrPicker";
            this.ckbAttrPicker.Size = new System.Drawing.Size(117, 17);
            this.ckbAttrPicker.TabIndex = 2;
            this.ckbAttrPicker.Text = "use Attribute picker";
            // 
            // ckbDecimal
            // 
            this.ckbDecimal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbDecimal.AutoSize = true;
            this.ckbDecimal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbDecimal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbDecimal.Location = new System.Drawing.Point(156, 0);
            this.ckbDecimal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbDecimal.Name = "ckbDecimal";
            this.ckbDecimal.Size = new System.Drawing.Size(140, 17);
            this.ckbDecimal.TabIndex = 1;
            this.ckbDecimal.Text = "Decimal (except Consts)";
            // 
            // flpnAnimType
            // 
            this.flpnAnimType.AutoSize = true;
            this.flpnAnimType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpnAnimType.Controls.Add(this.label4);
            this.flpnAnimType.Controls.Add(this.tbValAnimType);
            this.flpnAnimType.Controls.Add(this.cbAnimType);
            this.flpnAnimType.Controls.Add(this.tbAnimType);
            this.flpnAnimType.Location = new System.Drawing.Point(0, 70);
            this.flpnAnimType.Margin = new System.Windows.Forms.Padding(0);
            this.flpnAnimType.Name = "flpnAnimType";
            this.flpnAnimType.Size = new System.Drawing.Size(419, 25);
            this.flpnAnimType.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(2, 5);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 5, 2, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Animation Type";
            // 
            // tbValAnimType
            // 
            this.tbValAnimType.Location = new System.Drawing.Point(84, 2);
            this.tbValAnimType.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tbValAnimType.Name = "tbValAnimType";
            this.tbValAnimType.Size = new System.Drawing.Size(49, 20);
            this.tbValAnimType.TabIndex = 1;
            this.tbValAnimType.Text = "0xDDDD";
            // 
            // cbAnimType
            // 
            this.cbAnimType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAnimType.DropDownWidth = 200;
            this.cbAnimType.FormattingEnabled = true;
            this.cbAnimType.Location = new System.Drawing.Point(133, 2);
            this.cbAnimType.Margin = new System.Windows.Forms.Padding(0, 2, 2, 2);
            this.cbAnimType.MaxDropDownItems = 27;
            this.cbAnimType.Name = "cbAnimType";
            this.cbAnimType.Size = new System.Drawing.Size(20, 21);
            this.cbAnimType.TabIndex = 1;
            this.cbAnimType.TabStop = false;
            this.cbAnimType.SelectedIndexChanged += new System.EventHandler(this.cbAnimType_SelectedIndexChanged);
            // 
            // tbAnimType
            // 
            this.tbAnimType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAnimType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAnimType.Location = new System.Drawing.Point(157, 5);
            this.tbAnimType.Margin = new System.Windows.Forms.Padding(2, 5, 2, 2);
            this.tbAnimType.Name = "tbAnimType";
            this.tbAnimType.ReadOnly = true;
            this.tbAnimType.Size = new System.Drawing.Size(260, 13);
            this.tbAnimType.TabIndex = 0;
            this.tbAnimType.TabStop = false;
            this.tbAnimType.Text = "Animation type";
            this.tbAnimType.WordWrap = false;
            // 
            // flpnAnim
            // 
            this.flpnAnim.AutoSize = true;
            this.flpnAnim.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpnAnim.Controls.Add(this.lbParam);
            this.flpnAnim.Controls.Add(this.tbValAnim);
            this.flpnAnim.Controls.Add(this.btnAnim);
            this.flpnAnim.Controls.Add(this.tbAnim);
            this.flpnAnim.Location = new System.Drawing.Point(0, 95);
            this.flpnAnim.Margin = new System.Windows.Forms.Padding(0);
            this.flpnAnim.Name = "flpnAnim";
            this.flpnAnim.Size = new System.Drawing.Size(417, 24);
            this.flpnAnim.TabIndex = 4;
            // 
            // lbParam
            // 
            this.lbParam.AutoSize = true;
            this.lbParam.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbParam.Location = new System.Drawing.Point(2, 5);
            this.lbParam.Margin = new System.Windows.Forms.Padding(2, 5, 2, 2);
            this.lbParam.Name = "lbParam";
            this.lbParam.Size = new System.Drawing.Size(83, 13);
            this.lbParam.TabIndex = 0;
            this.lbParam.Tag = "Param|Animation String";
            this.lbParam.Text = "Animation String";
            // 
            // tbValAnim
            // 
            this.tbValAnim.Location = new System.Drawing.Point(87, 2);
            this.tbValAnim.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tbValAnim.Name = "tbValAnim";
            this.tbValAnim.Size = new System.Drawing.Size(49, 20);
            this.tbValAnim.TabIndex = 1;
            this.tbValAnim.Text = "0xDDDD";
            // 
            // btnAnim
            // 
            this.btnAnim.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAnim.Font = new System.Drawing.Font("Webdings", 12F, System.Drawing.FontStyle.Bold);
            this.btnAnim.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAnim.Location = new System.Drawing.Point(136, 2);
            this.btnAnim.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.btnAnim.Name = "btnAnim";
            this.btnAnim.Size = new System.Drawing.Size(19, 18);
            this.btnAnim.TabIndex = 2;
            this.btnAnim.Text = "8";
            this.btnAnim.Click += new System.EventHandler(this.btnAnim_Click);
            // 
            // tbAnim
            // 
            this.tbAnim.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAnim.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAnim.Location = new System.Drawing.Point(157, 5);
            this.tbAnim.Margin = new System.Windows.Forms.Padding(2, 5, 2, 2);
            this.tbAnim.Name = "tbAnim";
            this.tbAnim.ReadOnly = true;
            this.tbAnim.Size = new System.Drawing.Size(258, 13);
            this.tbAnim.TabIndex = 0;
            this.tbAnim.TabStop = false;
            this.tbAnim.Text = "Animation name";
            this.tbAnim.WordWrap = false;
            // 
            // flpnEventScope
            // 
            this.flpnEventScope.AutoSize = true;
            this.flpnEventScope.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpnEventScope.Controls.Add(this.label2);
            this.flpnEventScope.Controls.Add(this.cbEventScope);
            this.flpnEventScope.Location = new System.Drawing.Point(0, 119);
            this.flpnEventScope.Margin = new System.Windows.Forms.Padding(0);
            this.flpnEventScope.Name = "flpnEventScope";
            this.flpnEventScope.Size = new System.Drawing.Size(218, 25);
            this.flpnEventScope.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(2, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 4, 2, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 0;
            this.label2.Tag = "";
            this.label2.Text = "Event Tree Scope";
            // 
            // cbEventScope
            // 
            this.cbEventScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEventScope.FormattingEnabled = true;
            this.cbEventScope.Items.AddRange(new object[] {
            "Private",
            "SemiGlobal",
            "Global"});
            this.cbEventScope.Location = new System.Drawing.Point(100, 2);
            this.cbEventScope.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbEventScope.Name = "cbEventScope";
            this.cbEventScope.Size = new System.Drawing.Size(116, 21);
            this.cbEventScope.TabIndex = 1;
            // 
            // flpnEventTree
            // 
            this.flpnEventTree.AutoSize = true;
            this.flpnEventTree.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpnEventTree.Controls.Add(this.llEvent);
            this.flpnEventTree.Controls.Add(this.tbValEventTree);
            this.flpnEventTree.Controls.Add(this.btnEventTree);
            this.flpnEventTree.Controls.Add(this.tbEventTree);
            this.flpnEventTree.Location = new System.Drawing.Point(0, 144);
            this.flpnEventTree.Margin = new System.Windows.Forms.Padding(0);
            this.flpnEventTree.Name = "flpnEventTree";
            this.flpnEventTree.Size = new System.Drawing.Size(427, 24);
            this.flpnEventTree.TabIndex = 6;
            // 
            // llEvent
            // 
            this.llEvent.AutoSize = true;
            this.llEvent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.llEvent.LinkArea = new System.Windows.Forms.LinkArea(0, 11);
            this.llEvent.Location = new System.Drawing.Point(0, 5);
            this.llEvent.Margin = new System.Windows.Forms.Padding(0, 5, 2, 2);
            this.llEvent.Name = "llEvent";
            this.llEvent.Size = new System.Drawing.Size(59, 17);
            this.llEvent.TabIndex = 1;
            this.llEvent.TabStop = true;
            this.llEvent.Text = "Event Tree";
            this.llEvent.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.llEvent.UseCompatibleTextRendering = true;
            this.llEvent.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llEvent_LinkClicked);
            // 
            // tbValEventTree
            // 
            this.tbValEventTree.Location = new System.Drawing.Point(61, 2);
            this.tbValEventTree.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tbValEventTree.Name = "tbValEventTree";
            this.tbValEventTree.Size = new System.Drawing.Size(49, 20);
            this.tbValEventTree.TabIndex = 2;
            this.tbValEventTree.Text = "0xDDDD";
            // 
            // btnEventTree
            // 
            this.btnEventTree.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnEventTree.Font = new System.Drawing.Font("Webdings", 12F, System.Drawing.FontStyle.Bold);
            this.btnEventTree.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEventTree.Location = new System.Drawing.Point(110, 2);
            this.btnEventTree.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.btnEventTree.Name = "btnEventTree";
            this.btnEventTree.Size = new System.Drawing.Size(19, 18);
            this.btnEventTree.TabIndex = 3;
            this.btnEventTree.Text = "8";
            this.btnEventTree.Click += new System.EventHandler(this.btnEventTree_Click);
            // 
            // tbEventTree
            // 
            this.tbEventTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEventTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbEventTree.Location = new System.Drawing.Point(131, 5);
            this.tbEventTree.Margin = new System.Windows.Forms.Padding(2, 5, 2, 2);
            this.tbEventTree.Name = "tbEventTree";
            this.tbEventTree.ReadOnly = true;
            this.tbEventTree.Size = new System.Drawing.Size(294, 13);
            this.tbEventTree.TabIndex = 0;
            this.tbEventTree.TabStop = false;
            this.tbEventTree.Text = "Event tree";
            this.tbEventTree.WordWrap = false;
            // 
            // flpnOptions
            // 
            this.flpnOptions.AutoSize = true;
            this.flpnOptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpnOptions.Controls.Add(this.groupBox1);
            this.flpnOptions.Controls.Add(this.groupBox2);
            this.flpnOptions.Controls.Add(this.gbPriority);
            this.flpnOptions.Location = new System.Drawing.Point(2, 170);
            this.flpnOptions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flpnOptions.Name = "flpnOptions";
            this.flpnOptions.Size = new System.Drawing.Size(403, 227);
            this.flpnOptions.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.flpnOptions1);
            this.groupBox1.Location = new System.Drawing.Point(0, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(164, 223);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options 1";
            // 
            // flpnOptions1
            // 
            this.flpnOptions1.AutoSize = true;
            this.flpnOptions1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpnOptions1.Controls.Add(this.ckbFlipped);
            this.flpnOptions1.Controls.Add(this.ckbAnimSpeed);
            this.flpnOptions1.Controls.Add(this.ckbParam);
            this.flpnOptions1.Controls.Add(this.ckbInterruptible);
            this.flpnOptions1.Controls.Add(this.ckbStartTag);
            this.flpnOptions1.Controls.Add(this.ckbLoopCount);
            this.flpnOptions1.Controls.Add(this.ckbTransToIdle);
            this.flpnOptions1.Controls.Add(this.ckbBlendOut);
            this.flpnOptions1.Controls.Add(this.ckbBlendIn);
            this.flpnOptions1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpnOptions1.Location = new System.Drawing.Point(5, 17);
            this.flpnOptions1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flpnOptions1.Name = "flpnOptions1";
            this.flpnOptions1.Size = new System.Drawing.Size(155, 189);
            this.flpnOptions1.TabIndex = 0;
            // 
            // ckbFlipped
            // 
            this.ckbFlipped.AutoSize = true;
            this.ckbFlipped.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbFlipped.Location = new System.Drawing.Point(2, 2);
            this.ckbFlipped.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbFlipped.Name = "ckbFlipped";
            this.ckbFlipped.Size = new System.Drawing.Size(60, 17);
            this.ckbFlipped.TabIndex = 1;
            this.ckbFlipped.Text = "Flipped";
            this.ckbFlipped.UseVisualStyleBackColor = true;
            // 
            // ckbAnimSpeed
            // 
            this.ckbAnimSpeed.AutoSize = true;
            this.ckbAnimSpeed.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbAnimSpeed.Location = new System.Drawing.Point(2, 23);
            this.ckbAnimSpeed.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbAnimSpeed.Name = "ckbAnimSpeed";
            this.ckbAnimSpeed.Size = new System.Drawing.Size(151, 17);
            this.ckbAnimSpeed.TabIndex = 2;
            this.ckbAnimSpeed.Text = "Animation speed in Temp2";
            this.ckbAnimSpeed.UseVisualStyleBackColor = true;
            // 
            // ckbParam
            // 
            this.ckbParam.AutoSize = true;
            this.ckbParam.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbParam.Location = new System.Drawing.Point(2, 44);
            this.ckbParam.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbParam.Name = "ckbParam";
            this.ckbParam.Size = new System.Drawing.Size(149, 17);
            this.ckbParam.TabIndex = 3;
            this.ckbParam.Text = "Anim string index in Param";
            this.ckbParam.UseVisualStyleBackColor = true;
            this.ckbParam.CheckedChanged += new System.EventHandler(this.ckbParam_CheckedChanged);
            // 
            // ckbInterruptible
            // 
            this.ckbInterruptible.AutoSize = true;
            this.ckbInterruptible.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbInterruptible.Location = new System.Drawing.Point(2, 65);
            this.ckbInterruptible.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbInterruptible.Name = "ckbInterruptible";
            this.ckbInterruptible.Size = new System.Drawing.Size(81, 17);
            this.ckbInterruptible.TabIndex = 4;
            this.ckbInterruptible.Text = "Interruptible";
            this.ckbInterruptible.UseVisualStyleBackColor = true;
            // 
            // ckbStartTag
            // 
            this.ckbStartTag.AutoSize = true;
            this.ckbStartTag.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbStartTag.Location = new System.Drawing.Point(2, 86);
            this.ckbStartTag.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbStartTag.Name = "ckbStartTag";
            this.ckbStartTag.Size = new System.Drawing.Size(117, 17);
            this.ckbStartTag.TabIndex = 5;
            this.ckbStartTag.Text = "Start Tag in Temp0";
            this.ckbStartTag.UseVisualStyleBackColor = true;
            // 
            // ckbLoopCount
            // 
            this.ckbLoopCount.AutoSize = true;
            this.ckbLoopCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbLoopCount.Location = new System.Drawing.Point(2, 107);
            this.ckbLoopCount.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbLoopCount.Name = "ckbLoopCount";
            this.ckbLoopCount.Size = new System.Drawing.Size(128, 17);
            this.ckbLoopCount.TabIndex = 6;
            this.ckbLoopCount.Text = "Loop Count in Temp1";
            this.ckbLoopCount.UseVisualStyleBackColor = true;
            // 
            // ckbTransToIdle
            // 
            this.ckbTransToIdle.AutoSize = true;
            this.ckbTransToIdle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbTransToIdle.Location = new System.Drawing.Point(2, 128);
            this.ckbTransToIdle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbTransToIdle.Name = "ckbTransToIdle";
            this.ckbTransToIdle.Size = new System.Drawing.Size(85, 17);
            this.ckbTransToIdle.TabIndex = 7;
            this.ckbTransToIdle.Text = "Trans to Idle";
            this.ckbTransToIdle.UseVisualStyleBackColor = true;
            // 
            // ckbBlendOut
            // 
            this.ckbBlendOut.AutoSize = true;
            this.ckbBlendOut.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbBlendOut.Location = new System.Drawing.Point(2, 149);
            this.ckbBlendOut.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbBlendOut.Name = "ckbBlendOut";
            this.ckbBlendOut.Size = new System.Drawing.Size(96, 17);
            this.ckbBlendOut.TabIndex = 8;
            this.ckbBlendOut.Text = "Blend Out OFF";
            this.ckbBlendOut.UseVisualStyleBackColor = true;
            // 
            // ckbBlendIn
            // 
            this.ckbBlendIn.AutoSize = true;
            this.ckbBlendIn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbBlendIn.Location = new System.Drawing.Point(2, 170);
            this.ckbBlendIn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbBlendIn.Name = "ckbBlendIn";
            this.ckbBlendIn.Size = new System.Drawing.Size(88, 17);
            this.ckbBlendIn.TabIndex = 9;
            this.ckbBlendIn.Text = "Blend In OFF";
            this.ckbBlendIn.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.flpnOptions2);
            this.groupBox2.Location = new System.Drawing.Point(168, 2);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(162, 139);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options 2";
            // 
            // flpnOptions2
            // 
            this.flpnOptions2.AutoSize = true;
            this.flpnOptions2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpnOptions2.Controls.Add(this.ckbFlipTemp3);
            this.flpnOptions2.Controls.Add(this.ckbSync);
            this.flpnOptions2.Controls.Add(this.ckbAlignBlend);
            this.flpnOptions2.Controls.Add(this.ckbControllerIsSource);
            this.flpnOptions2.Controls.Add(this.ckbNotHurryable);
            this.flpnOptions2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpnOptions2.Location = new System.Drawing.Point(5, 17);
            this.flpnOptions2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flpnOptions2.Name = "flpnOptions2";
            this.flpnOptions2.Size = new System.Drawing.Size(153, 105);
            this.flpnOptions2.TabIndex = 0;
            // 
            // ckbFlipTemp3
            // 
            this.ckbFlipTemp3.AutoSize = true;
            this.ckbFlipTemp3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbFlipTemp3.Location = new System.Drawing.Point(2, 2);
            this.ckbFlipTemp3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbFlipTemp3.Name = "ckbFlipTemp3";
            this.ckbFlipTemp3.Size = new System.Drawing.Size(109, 17);
            this.ckbFlipTemp3.TabIndex = 1;
            this.ckbFlipTemp3.Text = "Flip flag in Temp3";
            this.ckbFlipTemp3.UseVisualStyleBackColor = true;
            this.ckbFlipTemp3.CheckedChanged += new System.EventHandler(this.ckbFlipTemp3_CheckedChanged);
            // 
            // ckbSync
            // 
            this.ckbSync.AutoSize = true;
            this.ckbSync.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbSync.Location = new System.Drawing.Point(2, 23);
            this.ckbSync.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbSync.Name = "ckbSync";
            this.ckbSync.Size = new System.Drawing.Size(134, 17);
            this.ckbSync.TabIndex = 2;
            this.ckbSync.Text = "Synchronise with caller";
            this.ckbSync.UseVisualStyleBackColor = true;
            // 
            // ckbAlignBlend
            // 
            this.ckbAlignBlend.AutoSize = true;
            this.ckbAlignBlend.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbAlignBlend.Location = new System.Drawing.Point(2, 44);
            this.ckbAlignBlend.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbAlignBlend.Name = "ckbAlignBlend";
            this.ckbAlignBlend.Size = new System.Drawing.Size(149, 17);
            this.ckbAlignBlend.TabIndex = 3;
            this.ckbAlignBlend.Text = "Align Blend Out with caller";
            this.ckbAlignBlend.UseVisualStyleBackColor = true;
            // 
            // ckbControllerIsSource
            // 
            this.ckbControllerIsSource.AutoSize = true;
            this.ckbControllerIsSource.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbControllerIsSource.Location = new System.Drawing.Point(2, 65);
            this.ckbControllerIsSource.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbControllerIsSource.Name = "ckbControllerIsSource";
            this.ckbControllerIsSource.Size = new System.Drawing.Size(115, 17);
            this.ckbControllerIsSource.TabIndex = 4;
            this.ckbControllerIsSource.Text = "Controller is source";
            this.ckbControllerIsSource.UseVisualStyleBackColor = true;
            // 
            // ckbNotHurryable
            // 
            this.ckbNotHurryable.AutoSize = true;
            this.ckbNotHurryable.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbNotHurryable.Location = new System.Drawing.Point(2, 86);
            this.ckbNotHurryable.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ckbNotHurryable.Name = "ckbNotHurryable";
            this.ckbNotHurryable.Size = new System.Drawing.Size(91, 17);
            this.ckbNotHurryable.TabIndex = 5;
            this.ckbNotHurryable.Text = "Not Hurryable";
            this.ckbNotHurryable.UseVisualStyleBackColor = true;
            // 
            // gbPriority
            // 
            this.gbPriority.AutoSize = true;
            this.gbPriority.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbPriority.Controls.Add(this.cbPriority);
            this.gbPriority.Location = new System.Drawing.Point(334, 2);
            this.gbPriority.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbPriority.Name = "gbPriority";
            this.gbPriority.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbPriority.Size = new System.Drawing.Size(67, 55);
            this.gbPriority.TabIndex = 3;
            this.gbPriority.TabStop = false;
            this.gbPriority.Text = "Priority";
            // 
            // cbPriority
            // 
            this.cbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPriority.FormattingEnabled = true;
            this.cbPriority.Items.AddRange(new object[] {
            "low",
            "medium",
            "high"});
            this.cbPriority.Location = new System.Drawing.Point(5, 17);
            this.cbPriority.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbPriority.Name = "cbPriority";
            this.cbPriority.Size = new System.Drawing.Size(58, 21);
            this.cbPriority.TabIndex = 1;
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(438, 420);
            this.Controls.Add(this.pnWizAnimate);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UI";
            this.Text = "UI";
            this.pnWizAnimate.ResumeLayout(false);
            this.pnWizAnimate.PerformLayout();
            this.flpnMain.ResumeLayout(false);
            this.flpnMain.PerformLayout();
            this.pnObject.ResumeLayout(false);
            this.pnObject.PerformLayout();
            this.pnIKObject.ResumeLayout(false);
            this.pnIKObject.PerformLayout();
            this.pnDoidOptions.ResumeLayout(false);
            this.pnDoidOptions.PerformLayout();
            this.flpnAnimType.ResumeLayout(false);
            this.flpnAnimType.PerformLayout();
            this.flpnAnim.ResumeLayout(false);
            this.flpnAnim.PerformLayout();
            this.flpnEventScope.ResumeLayout(false);
            this.flpnEventScope.PerformLayout();
            this.flpnEventTree.ResumeLayout(false);
            this.flpnEventTree.PerformLayout();
            this.flpnOptions.ResumeLayout(false);
            this.flpnOptions.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flpnOptions1.ResumeLayout(false);
            this.flpnOptions1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flpnOptions2.ResumeLayout(false);
            this.flpnOptions2.PerformLayout();
            this.gbPriority.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void llEvent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pjse.FileTable.Entry item = inst.Parent.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, doidEvent.Value);
            Bhav b = new Bhav();
            b.ProcessData(item.PFD, item.Package);

            SimPe.PackedFiles.UserInterface.BhavForm ui = (SimPe.PackedFiles.UserInterface.BhavForm)b.UIHandler;
            ui.Tag = "Popup"; // tells the SetReadOnly function it's in a popup - so everything locked down
            ui.Text = pjse.Localization.GetString("viewbhav")
                + ": " + b.FileName + " [" + b.Package.SaveFileName + "]";
            b.RefreshUI();
            ui.Show();
        }

        private void btnEventTree_Click(object sender, EventArgs e)
        {
            pjse.FileTable.Entry item = new pjse.ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, inst.Parent.FileDescriptor.Group, this, false);
            if (item != null)
                tbValEventTree.Text = "0x" + SimPe.Helper.HexString((ushort)item.Instance);
        }

        private void btnAnim_Click(object sender, EventArgs e)
        {
            this.doStrChooser(this.tbValAnim, this.tbAnim);
        }

        private void ckbParam_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            doCkbParam();
        }

        private void ckbFlipTemp3_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            ckbFlipped.Enabled = !ckbFlipTemp3.Checked;
        }

        private void cbAnimType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            internalchg = true;

            try
            {
                if (this.cbAnimType.SelectedIndex >= 0)
                {
                    GS.GlobalStr gs = (GS.GlobalStr)Enum.Parse(typeof(GS.GlobalStr), this.cbAnimType.SelectedItem.ToString());
                    tbValAnimType.Text = "0x" + ((ushort)gs).ToString("X");
                }
            }
            finally
            {
                tbAnimType.Text = (this.cbAnimType.SelectedIndex >= 0) ? this.cbAnimType.SelectedItem.ToString() : "---";
            }
            doStrValue(doidAnim.Value, tbAnim);
            tbValAnimType.Focus();

            internalchg = false;
        }

    }

}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWizAnimate : pjse.ABhavOperandWiz
	{
        public BhavOperandWizAnimate(Instruction i, String mode) : base(i) { myForm = new WizAnimate.UI(mode); }

		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm = null;
		}
		#endregion

	}

}
