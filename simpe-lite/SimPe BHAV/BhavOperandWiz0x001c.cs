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
 *   59 Temple Place - Suite 330, Boston, MA  1c111-1307, USA.             *
 ***************************************************************************/
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
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
        private CheckBox ckbPrivate;
        private CheckBox ckbGlobal;
        private CheckBox ckbSemiGlobal;
        private Label label3;
        private ComboBox cbRTBNType;
        private Label label4;
        private Label label8;
        private TextBox tbTree;
        private Label lbTreeName;
        private Button btnTreeName;
        private FlowLayoutPanel flpArgs;
        private LabelledDataOwner ldocArg1;
        private LabelledDataOwner ldocArg2;
        private LabelledDataOwner ldocArg3;
        private FlowLayoutPanel flpTree;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flpOptions;
        private CheckBox ckbLargeTree;
        private CheckBox ckbArguments;
        private CheckBox ckbParameters;
		/// <summary>
		/// Erforderliche Designervariable.
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

            lckb = new List<CheckBox>(new CheckBox[] {
                this.ckbArguments,  // options bit 0
                this.ckbParameters, // options bit 1
                this.ckbLargeTree,  // options bit 2
            });
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
        private DataOwnerControl tree = null;
        private List<CheckBox> lckb = null;

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
                this.tbTree.Text = "0x" + SimPe.Helper.HexString((byte)(i+1));
                this.lbTreeName.Text = ((BhavWiz)inst).readStr(this.Scope, GS.GlobalStr.NamedTree, (ushort)i, -1, pjse.Detail.ErrorNames);
            }
        }

        void tree_DataOwnerControlChanged(object sender, EventArgs e)
        {
            this.lbTreeName.Text = ((BhavWiz)inst).readStr(this.Scope, GS.GlobalStr.NamedTree,
                (ushort)(tree.Value - 1), -1, pjse.Detail.ErrorNames);
        }


        #region iBhavOperandWizForm
        public Panel WizPanel { get { return this.pnWiz0x001c; } }

		public void Execute(Instruction inst)
		{
			this.inst = inst;

            wrappedByteArray ops1 = inst.Operands;
            wrappedByteArray ops2 = inst.Reserved1;

            internalchg = true;

            Boolset options = ops1[0x02];

            this.cbScope.SelectedIndex = 0; // Private
            if      (options[0]) this.cbScope.SelectedIndex = 2; // Global
            else if (options[1]) this.cbScope.SelectedIndex = 1; // SemiGlobal

            this.ckbSemiGlobal.Checked = !options[3];
            this.ckbGlobal.Checked     = !options[2];

            this.cbRTBNType.SelectedIndex = ops1[0x05] < this.cbRTBNType.Items.Count ? ops1[0x05] : -1;

            this.ckbArguments.Checked  =  options[4] && !options[5] && !options[6];
            this.ckbParameters.Checked = !options[4] &&  options[5] && !options[6];
            this.ckbLargeTree.Checked  = !options[4] && !options[5] &&  options[6];

            if (tree != null)
                tree.DataOwnerControlChanged -= new EventHandler(tree_DataOwnerControlChanged);
            if (ckbLargeTree.Checked)
                tree = new DataOwnerControl(null, null, null, this.tbTree, null, null, null, 7, BhavWiz.ToShort(ops1[0x04], ops1[0x07]));
            else
                tree = new DataOwnerControl(null, null, null, this.tbTree, null, null, null, 7, ops1[0x04]);
            tree.DataOwnerControlChanged += new EventHandler(tree_DataOwnerControlChanged);
            tree_DataOwnerControlChanged(null, null);

            this.flpArgs.Enabled = this.ckbArguments.Checked;
            ldocArg1.DataOwner = ops1[0x06]; ldocArg1.Value = BhavWiz.ToShort(ops1[0x07], ops2[0x00]);
            ldocArg2.DataOwner = ops2[0x01]; ldocArg2.Value = BhavWiz.ToShort(ops2[0x02], ops2[0x03]);
            ldocArg3.DataOwner = ops2[0x04]; ldocArg3.Value = BhavWiz.ToShort(ops2[0x05], ops2[0x06]);
            ldocArg1.Instruction = ldocArg2.Instruction = ldocArg3.Instruction = inst;

            internalchg = false;

        }

		public Instruction Write(Instruction inst)
		{
			if (inst != null)
			{
                wrappedByteArray ops1 = inst.Operands;
                wrappedByteArray ops2 = inst.Reserved1;

                Boolset options = ops1[0x02];
                int scope = this.cbScope.SelectedIndex;
                options[0] = (scope == 2);
                options[1] = (scope == 1);
                options[2] = !this.ckbGlobal.Checked;
                options[3] = !this.ckbSemiGlobal.Checked;
                options[4] = this.ckbArguments.Checked;
                options[5] = this.ckbParameters.Checked;
                options[6] = this.ckbLargeTree.Checked;
                ops1[0x02] = options;

                ops1[0x04] = (byte)(tree.Value & 0xFF);
                if (this.cbRTBNType.SelectedIndex >= 0)
                    ops1[0x05] = (byte)this.cbRTBNType.SelectedIndex;

                if (this.ckbArguments.Checked)
                {
                    ops1[0x06] = ldocArg1.DataOwner;
                    byte[] lohi = { 0, 0 };
                    BhavWiz.FromShort(ref lohi, 0, ldocArg1.Value);
                    ops1[0x07] = lohi[0];
                    ops2[0x00] = lohi[1];
                    ops2[0x01] = ldocArg2.DataOwner;
                    BhavWiz.FromShort(ref ops2, 2, ldocArg2.Value);
                    ops2[0x04] = ldocArg3.DataOwner;
                    BhavWiz.FromShort(ref ops2, 5, ldocArg3.Value);
                }
                else if (ckbLargeTree.Checked)
                    ops1[0x07] = (byte)(tree.Value >> 8 & 0xFF);
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
            this.pnWiz0x001c = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flpTree = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbScope = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTree = new System.Windows.Forms.TextBox();
            this.btnTreeName = new System.Windows.Forms.Button();
            this.lbTreeName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ckbPrivate = new System.Windows.Forms.CheckBox();
            this.ckbSemiGlobal = new System.Windows.Forms.CheckBox();
            this.ckbGlobal = new System.Windows.Forms.CheckBox();
            this.flpOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.cbRTBNType = new System.Windows.Forms.ComboBox();
            this.ckbLargeTree = new System.Windows.Forms.CheckBox();
            this.ckbArguments = new System.Windows.Forms.CheckBox();
            this.ckbParameters = new System.Windows.Forms.CheckBox();
            this.flpArgs = new System.Windows.Forms.FlowLayoutPanel();
            this.ldocArg1 = new pjse.LabelledDataOwner();
            this.ldocArg2 = new pjse.LabelledDataOwner();
            this.ldocArg3 = new pjse.LabelledDataOwner();
            this.pnWiz0x001c.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flpTree.SuspendLayout();
            this.flpOptions.SuspendLayout();
            this.flpArgs.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnWiz0x001c
            // 
            resources.ApplyResources(this.pnWiz0x001c, "pnWiz0x001c");
            this.pnWiz0x001c.Controls.Add(this.flowLayoutPanel1);
            this.pnWiz0x001c.Name = "pnWiz0x001c";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.flpTree);
            this.flowLayoutPanel1.Controls.Add(this.flpOptions);
            this.flowLayoutPanel1.Controls.Add(this.flpArgs);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // flpTree
            // 
            resources.ApplyResources(this.flpTree, "flpTree");
            this.flpTree.Controls.Add(this.label1);
            this.flpTree.Controls.Add(this.cbScope);
            this.flpTree.Controls.Add(this.label8);
            this.flpTree.Controls.Add(this.label2);
            this.flpTree.Controls.Add(this.tbTree);
            this.flpTree.Controls.Add(this.btnTreeName);
            this.flpTree.Controls.Add(this.lbTreeName);
            this.flpTree.Controls.Add(this.label3);
            this.flpTree.Controls.Add(this.ckbPrivate);
            this.flpTree.Controls.Add(this.ckbSemiGlobal);
            this.flpTree.Controls.Add(this.ckbGlobal);
            this.flpTree.Name = "flpTree";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cbScope
            // 
            resources.ApplyResources(this.cbScope, "cbScope");
            this.cbScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScope.FormattingEnabled = true;
            this.cbScope.Items.AddRange(new object[] {
            resources.GetString("cbScope.Items"),
            resources.GetString("cbScope.Items1"),
            resources.GetString("cbScope.Items2")});
            this.cbScope.Name = "cbScope";
            this.cbScope.SelectedIndexChanged += new System.EventHandler(this.cbScope_SelectedIndexChanged);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tbTree
            // 
            resources.ApplyResources(this.tbTree, "tbTree");
            this.tbTree.Name = "tbTree";
            // 
            // btnTreeName
            // 
            resources.ApplyResources(this.btnTreeName, "btnTreeName");
            this.flpTree.SetFlowBreak(this.btnTreeName, true);
            this.btnTreeName.Name = "btnTreeName";
            this.btnTreeName.Click += new System.EventHandler(this.btnTreeName_Click);
            // 
            // lbTreeName
            // 
            resources.ApplyResources(this.lbTreeName, "lbTreeName");
            this.flpTree.SetFlowBreak(this.lbTreeName, true);
            this.lbTreeName.Name = "lbTreeName";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // ckbPrivate
            // 
            resources.ApplyResources(this.ckbPrivate, "ckbPrivate");
            this.ckbPrivate.Checked = true;
            this.ckbPrivate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbPrivate.Name = "ckbPrivate";
            this.ckbPrivate.UseVisualStyleBackColor = true;
            // 
            // ckbSemiGlobal
            // 
            resources.ApplyResources(this.ckbSemiGlobal, "ckbSemiGlobal");
            this.ckbSemiGlobal.Name = "ckbSemiGlobal";
            this.ckbSemiGlobal.UseVisualStyleBackColor = true;
            // 
            // ckbGlobal
            // 
            resources.ApplyResources(this.ckbGlobal, "ckbGlobal");
            this.ckbGlobal.Name = "ckbGlobal";
            this.ckbGlobal.UseVisualStyleBackColor = true;
            // 
            // flpOptions
            // 
            resources.ApplyResources(this.flpOptions, "flpOptions");
            this.flpOptions.Controls.Add(this.label4);
            this.flpOptions.Controls.Add(this.cbRTBNType);
            this.flpOptions.Controls.Add(this.ckbLargeTree);
            this.flpOptions.Controls.Add(this.ckbArguments);
            this.flpOptions.Controls.Add(this.ckbParameters);
            this.flpOptions.Name = "flpOptions";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // cbRTBNType
            // 
            resources.ApplyResources(this.cbRTBNType, "cbRTBNType");
            this.cbRTBNType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flpOptions.SetFlowBreak(this.cbRTBNType, true);
            this.cbRTBNType.FormattingEnabled = true;
            this.cbRTBNType.Items.AddRange(new object[] {
            resources.GetString("cbRTBNType.Items"),
            resources.GetString("cbRTBNType.Items1"),
            resources.GetString("cbRTBNType.Items2")});
            this.cbRTBNType.Name = "cbRTBNType";
            // 
            // ckbLargeTree
            // 
            resources.ApplyResources(this.ckbLargeTree, "ckbLargeTree");
            this.ckbLargeTree.Name = "ckbLargeTree";
            this.ckbLargeTree.UseVisualStyleBackColor = true;
            this.ckbLargeTree.CheckedChanged += new System.EventHandler(this.ckb_CheckedChanged);
            this.ckbLargeTree.AppearanceChanged += new System.EventHandler(this.ckb_CheckedChanged);
            // 
            // ckbArguments
            // 
            resources.ApplyResources(this.ckbArguments, "ckbArguments");
            this.ckbArguments.Name = "ckbArguments";
            this.ckbArguments.UseVisualStyleBackColor = true;
            this.ckbArguments.CheckedChanged += new System.EventHandler(this.ckb_CheckedChanged);
            this.ckbArguments.AppearanceChanged += new System.EventHandler(this.ckb_CheckedChanged);
            // 
            // ckbParameters
            // 
            resources.ApplyResources(this.ckbParameters, "ckbParameters");
            this.ckbParameters.Name = "ckbParameters";
            this.ckbParameters.UseVisualStyleBackColor = true;
            this.ckbParameters.CheckedChanged += new System.EventHandler(this.ckb_CheckedChanged);
            this.ckbParameters.AppearanceChanged += new System.EventHandler(this.ckb_CheckedChanged);
            // 
            // flpArgs
            // 
            resources.ApplyResources(this.flpArgs, "flpArgs");
            this.flpArgs.Controls.Add(this.ldocArg1);
            this.flpArgs.Controls.Add(this.ldocArg2);
            this.flpArgs.Controls.Add(this.ldocArg3);
            this.flpArgs.Name = "flpArgs";
            // 
            // ldocArg1
            // 
            resources.ApplyResources(this.ldocArg1, "ldocArg1");
            this.ldocArg1.DataOwner = ((byte)(255));
            this.ldocArg1.Decimal = false;
            this.ldocArg1.DecimalVisible = false;
            this.ldocArg1.FlagsFor = null;
            this.ldocArg1.Instruction = null;
            this.ldocArg1.LabelAutoSize = false;
            this.ldocArg1.LabelSize = new System.Drawing.Size(61, 13);
            this.ldocArg1.Name = "ldocArg1";
            this.ldocArg1.UseAttrPicker = true;
            this.ldocArg1.UseAttrPickerVisible = false;
            this.ldocArg1.UseFlagNames = false;
            this.ldocArg1.Value = ((ushort)(0));
            // 
            // ldocArg2
            // 
            resources.ApplyResources(this.ldocArg2, "ldocArg2");
            this.ldocArg2.DataOwner = ((byte)(255));
            this.ldocArg2.Decimal = false;
            this.ldocArg2.DecimalVisible = false;
            this.ldocArg2.FlagsFor = null;
            this.ldocArg2.Instruction = null;
            this.ldocArg2.LabelAutoSize = false;
            this.ldocArg2.LabelSize = new System.Drawing.Size(61, 13);
            this.ldocArg2.Name = "ldocArg2";
            this.ldocArg2.UseAttrPicker = true;
            this.ldocArg2.UseAttrPickerVisible = false;
            this.ldocArg2.UseFlagNames = false;
            this.ldocArg2.Value = ((ushort)(0));
            // 
            // ldocArg3
            // 
            resources.ApplyResources(this.ldocArg3, "ldocArg3");
            this.ldocArg3.DataOwner = ((byte)(255));
            this.ldocArg3.Decimal = false;
            this.ldocArg3.FlagsFor = null;
            this.ldocArg3.Instruction = null;
            this.ldocArg3.LabelAutoSize = false;
            this.ldocArg3.LabelSize = new System.Drawing.Size(61, 13);
            this.ldocArg3.Name = "ldocArg3";
            this.ldocArg3.UseAttrPicker = true;
            this.ldocArg3.UseFlagNames = false;
            this.ldocArg3.Value = ((ushort)(0));
            // 
            // UI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.pnWiz0x001c);
            this.Name = "UI";
            this.pnWiz0x001c.ResumeLayout(false);
            this.pnWiz0x001c.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flpTree.ResumeLayout(false);
            this.flpTree.PerformLayout();
            this.flpOptions.ResumeLayout(false);
            this.flpOptions.PerformLayout();
            this.flpArgs.ResumeLayout(false);
            this.flpArgs.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        private void btnTreeName_Click(object sender, EventArgs e)
        {
            doStrChooser();
        }

        private void cbScope_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            this.lbTreeName.Text = ((BhavWiz)inst).readStr(this.Scope, GS.GlobalStr.NamedTree, (ushort)(Convert.ToByte(this.tbTree.Text, 16) - 1), -1, pjse.Detail.ErrorNames);
        }

        private void ckb_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            internalchg = true;

            CheckBox ckb = sender as CheckBox;
            if (ckb == null || !lckb.Contains(ckb)) return;

            switch (lckb.IndexOf(ckb))
            {
                case 0: if (ckbArguments.Checked)  ckbLargeTree.Checked  = ckbParameters.Checked = false; break;
                case 1: if (ckbParameters.Checked) ckbLargeTree.Checked  = ckbArguments.Checked  = false; break;
                case 2: if (ckbLargeTree.Checked)  ckbParameters.Checked = ckbArguments.Checked  = false; break;
            }

            tree.ValueIsByte = !ckbLargeTree.Checked;
            flpArgs.Enabled = ckbArguments.Checked;

            internalchg = false;
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
