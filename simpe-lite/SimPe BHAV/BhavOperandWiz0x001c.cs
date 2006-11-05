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
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.PackedFiles.Wrapper;

namespace pjse.BhavOperandWizards.Wiz0x001c
{
    /// <summary>
    /// Summary description for StrBig.
    /// </summary>
    internal class UI : System.Windows.Forms.Form
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
        private Panel pnArgs;
        private CheckBox cbAttrPicker;
        private CheckBox cbDecimal;
        private ComboBox cbPicker1;
        private TextBox tbVal1;
        private ComboBox cbDataOwner1;
        private Label label5;
        private Label label7;
        private Label label6;
        private ComboBox cbPicker3;
        private ComboBox cbPicker2;
        private TextBox tbVal3;
        private TextBox tbVal2;
        private ComboBox cbDataOwner3;
        private ComboBox cbDataOwner2;
        private Label label8;
        private TextBox tbTree;
        private Label lbTreeName;
        private Button btnTreeName;
        private Label lbConst3;
        private Label lbConst2;
        private Label lbConst1;
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


		#region UI
		private Instruction inst = null;
		private DataOwnerControl doid1 = null;
        private DataOwnerControl doid2 = null;
        private DataOwnerControl doid3 = null;
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

        private bool hex8_IsValid(object sender)
        {
            try { Convert.ToByte(((TextBox)sender).Text, 16); }
            catch (Exception) { return false; }
            return true;
        }



		public void Execute(Instruction inst)
		{
			this.inst = inst;

            wrappedByteArray ops1 = inst.Operands;
            wrappedByteArray ops2 = inst.Reserved1;

            internalchg = true;

            this.cbScope.SelectedIndex = 0; // Private
            if ((ops1[0x02] & 0x01) != 0) this.cbScope.SelectedIndex = 2; // Global
            else if ((ops1[0x02] & 0x02) != 0) this.cbScope.SelectedIndex = 1; // SemiGlobal

            this.tfGlobal.Checked = (ops1[0x02] & 0x04) == 0;
            this.tfSemiGlobal.Checked = (ops1[0x02] & 0x08) == 0;

            this.pnArgs.Enabled = this.tfArgs.Checked = (ops1[0x02] & 0x10) != 0;
            this.tfParams.Checked = (ops1[0x02] & 0x20) != 0;

            this.tbTree.Text = "0x" + SimPe.Helper.HexString(ops1[0x04]);
            this.lbTreeName.Text = ((BhavWiz)inst).readStr(this.Scope, GS.GlobalStr.NamedTree, (ushort)(ops1[0x04] - 1), -1, pjse.Detail.ErrorNames);

            this.cbRTBNType.SelectedIndex = ops1[0x05] < this.cbRTBNType.Items.Count ? ops1[0x05] : -1;

            doid1 = new DataOwnerControl(inst, this.cbDataOwner1, this.cbPicker1, this.tbVal1, this.cbDecimal, this.cbAttrPicker, this.lbConst1,
                ops1[0x06], BhavWiz.ToShort(ops1[0x07], ops2[0x00]));
            doid2 = new DataOwnerControl(inst, this.cbDataOwner2, this.cbPicker2, this.tbVal2, this.cbDecimal, this.cbAttrPicker, this.lbConst2,
                ops2[0x01], BhavWiz.ToShort(ops2[0x02], ops2[0x03]));
            doid3 = new DataOwnerControl(inst, this.cbDataOwner3, this.cbPicker3, this.tbVal3, this.cbDecimal, this.cbAttrPicker, this.lbConst3,
                ops2[0x04], BhavWiz.ToShort(ops2[0x05], ops2[0x06]));

            internalchg = false;
        }

		public Instruction Write(Instruction inst)
		{
			if (inst != null)
			{
                wrappedByteArray ops1 = inst.Operands;
                wrappedByteArray ops2 = inst.Reserved1;

                ops1[0x02] &= 0xc0;
                int scope = this.cbScope.SelectedIndex;
                if (scope == 2) ops1[0x02] |= 0x01;
                if (scope == 1) ops1[0x02] |= 0x02;
                if (!this.tfGlobal.Checked) ops1[0x02] |= 0x04;
                if (!this.tfSemiGlobal.Checked) ops1[0x02] |= 0x08;
                if (this.tfArgs.Checked) ops1[0x02] |= 0x10;
                if (this.tfParams.Checked) ops1[0x02] |= 0x20;

                ops1[0x04] = Convert.ToByte(this.tbTree.Text, 16);
                if (this.cbRTBNType.SelectedIndex >= 0)
                    ops1[0x05] = (byte)this.cbRTBNType.SelectedIndex;

                ops1[0x06] = doid1.DataOwner;
                ops1[0x07] = (byte)(doid1.Value & 0xff);
                ops2[0x00] = (byte)((doid1.Value >> 8) & 0xff);
                ops2[0x01] = doid2.DataOwner;
                ops2[0x02] = (byte)(doid2.Value & 0xff);
                ops2[0x03] = (byte)((doid2.Value >> 8) & 0xff);
                ops2[0x04] = doid3.DataOwner;
                ops2[0x05] = (byte)(doid3.Value & 0xff);
                ops2[0x06] = (byte)((doid3.Value >> 8) & 0xff);
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
            this.btnTreeName = new System.Windows.Forms.Button();
            this.tbTree = new System.Windows.Forms.TextBox();
            this.pnArgs = new System.Windows.Forms.Panel();
            this.lbConst3 = new System.Windows.Forms.Label();
            this.lbConst2 = new System.Windows.Forms.Label();
            this.lbConst1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbPicker3 = new System.Windows.Forms.ComboBox();
            this.cbAttrPicker = new System.Windows.Forms.CheckBox();
            this.cbPicker2 = new System.Windows.Forms.ComboBox();
            this.tbVal3 = new System.Windows.Forms.TextBox();
            this.cbDecimal = new System.Windows.Forms.CheckBox();
            this.tbVal2 = new System.Windows.Forms.TextBox();
            this.cbDataOwner3 = new System.Windows.Forms.ComboBox();
            this.cbPicker1 = new System.Windows.Forms.ComboBox();
            this.cbDataOwner2 = new System.Windows.Forms.ComboBox();
            this.tbVal1 = new System.Windows.Forms.TextBox();
            this.cbDataOwner1 = new System.Windows.Forms.ComboBox();
            this.tfGlobal = new System.Windows.Forms.CheckBox();
            this.tfParams = new System.Windows.Forms.CheckBox();
            this.tfArgs = new System.Windows.Forms.CheckBox();
            this.tfSemiGlobal = new System.Windows.Forms.CheckBox();
            this.tfPrivate = new System.Windows.Forms.CheckBox();
            this.cbRTBNType = new System.Windows.Forms.ComboBox();
            this.cbScope = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbTreeName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnWiz0x001c.SuspendLayout();
            this.pnArgs.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnWiz0x001c
            // 
            this.pnWiz0x001c.Controls.Add(this.btnTreeName);
            this.pnWiz0x001c.Controls.Add(this.tbTree);
            this.pnWiz0x001c.Controls.Add(this.pnArgs);
            this.pnWiz0x001c.Controls.Add(this.tfGlobal);
            this.pnWiz0x001c.Controls.Add(this.tfParams);
            this.pnWiz0x001c.Controls.Add(this.tfArgs);
            this.pnWiz0x001c.Controls.Add(this.tfSemiGlobal);
            this.pnWiz0x001c.Controls.Add(this.tfPrivate);
            this.pnWiz0x001c.Controls.Add(this.cbRTBNType);
            this.pnWiz0x001c.Controls.Add(this.cbScope);
            this.pnWiz0x001c.Controls.Add(this.label3);
            this.pnWiz0x001c.Controls.Add(this.lbTreeName);
            this.pnWiz0x001c.Controls.Add(this.label2);
            this.pnWiz0x001c.Controls.Add(this.label4);
            this.pnWiz0x001c.Controls.Add(this.label8);
            this.pnWiz0x001c.Controls.Add(this.label1);
            resources.ApplyResources(this.pnWiz0x001c, "pnWiz0x001c");
            this.pnWiz0x001c.Name = "pnWiz0x001c";
            // 
            // btnTreeName
            // 
            resources.ApplyResources(this.btnTreeName, "btnTreeName");
            this.btnTreeName.Name = "btnTreeName";
            this.btnTreeName.Click += new System.EventHandler(this.btnTreeName_Click);
            // 
            // tbTree
            // 
            resources.ApplyResources(this.tbTree, "tbTree");
            this.tbTree.Name = "tbTree";
            this.tbTree.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbTree.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            this.tbTree.TextChanged += new System.EventHandler(this.hex8_TextChanged);
            // 
            // pnArgs
            // 
            this.pnArgs.Controls.Add(this.lbConst3);
            this.pnArgs.Controls.Add(this.lbConst2);
            this.pnArgs.Controls.Add(this.lbConst1);
            this.pnArgs.Controls.Add(this.label7);
            this.pnArgs.Controls.Add(this.label6);
            this.pnArgs.Controls.Add(this.label5);
            this.pnArgs.Controls.Add(this.cbPicker3);
            this.pnArgs.Controls.Add(this.cbAttrPicker);
            this.pnArgs.Controls.Add(this.cbPicker2);
            this.pnArgs.Controls.Add(this.tbVal3);
            this.pnArgs.Controls.Add(this.cbDecimal);
            this.pnArgs.Controls.Add(this.tbVal2);
            this.pnArgs.Controls.Add(this.cbDataOwner3);
            this.pnArgs.Controls.Add(this.cbPicker1);
            this.pnArgs.Controls.Add(this.cbDataOwner2);
            this.pnArgs.Controls.Add(this.tbVal1);
            this.pnArgs.Controls.Add(this.cbDataOwner1);
            resources.ApplyResources(this.pnArgs, "pnArgs");
            this.pnArgs.Name = "pnArgs";
            // 
            // lbConst3
            // 
            resources.ApplyResources(this.lbConst3, "lbConst3");
            this.lbConst3.Name = "lbConst3";
            // 
            // lbConst2
            // 
            resources.ApplyResources(this.lbConst2, "lbConst2");
            this.lbConst2.Name = "lbConst2";
            // 
            // lbConst1
            // 
            resources.ApplyResources(this.lbConst1, "lbConst1");
            this.lbConst1.Name = "lbConst1";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // cbPicker3
            // 
            this.cbPicker3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker3.DropDownWidth = 384;
            resources.ApplyResources(this.cbPicker3, "cbPicker3");
            this.cbPicker3.Name = "cbPicker3";
            // 
            // cbAttrPicker
            // 
            resources.ApplyResources(this.cbAttrPicker, "cbAttrPicker");
            this.cbAttrPicker.Name = "cbAttrPicker";
            // 
            // cbPicker2
            // 
            this.cbPicker2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker2.DropDownWidth = 384;
            resources.ApplyResources(this.cbPicker2, "cbPicker2");
            this.cbPicker2.Name = "cbPicker2";
            // 
            // tbVal3
            // 
            resources.ApplyResources(this.tbVal3, "tbVal3");
            this.tbVal3.Name = "tbVal3";
            // 
            // cbDecimal
            // 
            resources.ApplyResources(this.cbDecimal, "cbDecimal");
            this.cbDecimal.Name = "cbDecimal";
            // 
            // tbVal2
            // 
            resources.ApplyResources(this.tbVal2, "tbVal2");
            this.tbVal2.Name = "tbVal2";
            // 
            // cbDataOwner3
            // 
            this.cbDataOwner3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner3.DropDownWidth = 384;
            resources.ApplyResources(this.cbDataOwner3, "cbDataOwner3");
            this.cbDataOwner3.Name = "cbDataOwner3";
            // 
            // cbPicker1
            // 
            this.cbPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker1.DropDownWidth = 384;
            resources.ApplyResources(this.cbPicker1, "cbPicker1");
            this.cbPicker1.Name = "cbPicker1";
            // 
            // cbDataOwner2
            // 
            this.cbDataOwner2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner2.DropDownWidth = 384;
            resources.ApplyResources(this.cbDataOwner2, "cbDataOwner2");
            this.cbDataOwner2.Name = "cbDataOwner2";
            // 
            // tbVal1
            // 
            resources.ApplyResources(this.tbVal1, "tbVal1");
            this.tbVal1.Name = "tbVal1";
            // 
            // cbDataOwner1
            // 
            this.cbDataOwner1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner1.DropDownWidth = 384;
            resources.ApplyResources(this.cbDataOwner1, "cbDataOwner1");
            this.cbDataOwner1.Name = "cbDataOwner1";
            // 
            // tfGlobal
            // 
            resources.ApplyResources(this.tfGlobal, "tfGlobal");
            this.tfGlobal.Name = "tfGlobal";
            this.tfGlobal.UseVisualStyleBackColor = true;
            // 
            // tfParams
            // 
            resources.ApplyResources(this.tfParams, "tfParams");
            this.tfParams.Name = "tfParams";
            this.tfParams.UseVisualStyleBackColor = true;
            // 
            // tfArgs
            // 
            resources.ApplyResources(this.tfArgs, "tfArgs");
            this.tfArgs.Name = "tfArgs";
            this.tfArgs.UseVisualStyleBackColor = true;
            this.tfArgs.CheckedChanged += new System.EventHandler(this.tfArgs_CheckedChanged);
            // 
            // tfSemiGlobal
            // 
            resources.ApplyResources(this.tfSemiGlobal, "tfSemiGlobal");
            this.tfSemiGlobal.Name = "tfSemiGlobal";
            this.tfSemiGlobal.UseVisualStyleBackColor = true;
            // 
            // tfPrivate
            // 
            resources.ApplyResources(this.tfPrivate, "tfPrivate");
            this.tfPrivate.Checked = true;
            this.tfPrivate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tfPrivate.Name = "tfPrivate";
            this.tfPrivate.UseVisualStyleBackColor = true;
            // 
            // cbRTBNType
            // 
            this.cbRTBNType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRTBNType.FormattingEnabled = true;
            this.cbRTBNType.Items.AddRange(new object[] {
            resources.GetString("cbRTBNType.Items"),
            resources.GetString("cbRTBNType.Items1"),
            resources.GetString("cbRTBNType.Items2")});
            resources.ApplyResources(this.cbRTBNType, "cbRTBNType");
            this.cbRTBNType.Name = "cbRTBNType";
            // 
            // cbScope
            // 
            this.cbScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScope.FormattingEnabled = true;
            this.cbScope.Items.AddRange(new object[] {
            resources.GetString("cbScope.Items"),
            resources.GetString("cbScope.Items1"),
            resources.GetString("cbScope.Items2")});
            resources.ApplyResources(this.cbScope, "cbScope");
            this.cbScope.Name = "cbScope";
            this.cbScope.SelectedIndexChanged += new System.EventHandler(this.cbScope_SelectedIndexChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // lbTreeName
            // 
            resources.ApplyResources(this.lbTreeName, "lbTreeName");
            this.lbTreeName.Name = "lbTreeName";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // UI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.pnWiz0x001c);
            this.Name = "UI";
            this.pnWiz0x001c.ResumeLayout(false);
            this.pnWiz0x001c.PerformLayout();
            this.pnArgs.ResumeLayout(false);
            this.pnArgs.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        private void btnTreeName_Click(object sender, EventArgs e)
        {
            doStrChooser();
        }

        private void tfArgs_CheckedChanged(object sender, EventArgs e)
        {
            this.pnArgs.Enabled = this.tfArgs.Checked;
        }


        private void hex8_TextChanged(object sender, System.EventArgs ev)
        {
            if (internalchg) return;
            if (!hex8_IsValid(sender)) return;

            byte val = Convert.ToByte(((TextBox)sender).Text, 16);
            this.lbTreeName.Text = ((BhavWiz)inst).readStr(this.Scope, GS.GlobalStr.NamedTree, (ushort)(val - 1), -1, pjse.Detail.ErrorNames);
        }

        private void hex8_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (hex8_IsValid(sender)) return;

            e.Cancel = true;

            bool origstate = internalchg;
            internalchg = true;
            byte val = inst.Operands[0x04];
            ((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(val);
            ((TextBox)sender).SelectAll();
            this.lbTreeName.Text = ((BhavWiz)inst).readStr(this.Scope, GS.GlobalStr.NamedTree, (ushort)(val - 1), -1, pjse.Detail.ErrorNames);
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

        private void cbScope_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            this.lbTreeName.Text = ((BhavWiz)inst).readStr(this.Scope, GS.GlobalStr.NamedTree, (ushort)(Convert.ToByte(this.tbTree.Text, 16) - 1), -1, pjse.Detail.ErrorNames);
        }

	}

}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWiz0x001c : pjse.ABhavOperandWiz
	{
		public BhavOperandWiz0x001c() : base() { }

		public BhavOperandWiz0x001c(Instruction i) : base(i) { }


		private Wiz0x001c.UI myForm = null;
		public override Panel bhavPrimWizPanel
		{
			get
			{
				if (myForm == null) myForm = new Wiz0x001c.UI();
				return myForm.pnWiz0x001c;
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
