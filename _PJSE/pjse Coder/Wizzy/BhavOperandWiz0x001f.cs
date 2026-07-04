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

namespace pjse.BhavOperandWizards.Wiz0x001f
{
    /// <summary>
    /// Summary description for StrBig.
    /// </summary>
    internal class UI : System.Windows.Forms.Form, iBhavOperandWizForm
	{
		#region Form variables

        internal System.Windows.Forms.Panel pnWiz0x001f;
        private CheckBox ckbStackObj;
        private Panel pnObject;
        private CheckBox cbAttrPicker;
        private CheckBox cbDecimal;
        private ComboBox cbPicker1;
        private TextBox tbVal1;
        private ComboBox cbDataOwner1;
        private Label label1;
        private Panel pnNodeVersion;
        private CheckBox ckbDisabled;
        private Panel pnWhere;
        private ComboBox cbWhere;
        private TextBox tbWhereVal;
        private Label label4;
        private CheckBox ckbWhere;
        private ComboBox cbToNext;
        private TextBox tbLocalVar;
        private TextBox tbGUID;
        private Label label2;
        private Label lbGUIDText;
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

            this.tbGUID.Visible = false;
            this.tbGUID.Left = this.cbToNext.Left + this.cbToNext.Width + 3;
            this.tbLocalVar.Visible = false;
            this.tbLocalVar.Left = this.cbToNext.Left + this.cbToNext.Width + 3;

            this.cbToNext.Items.AddRange(BhavWiz.readStr(GS.BhavStr.NextObject).ToArray());
            this.cbWhere.Items.AddRange(BhavWiz.readStr(GS.BhavStr.DataLabels).ToArray());
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

        private void setToNext(byte val)
        {
            bool guid = false;
            bool local = false;
            switch (val)
            {
                case 0x04: case 0x07: guid = true; break;
                case 0x09: case 0x22: local = true; break;
            }
            this.lbGUIDText.Visible = this.tbGUID.Visible = guid;
            this.tbLocalVar.Visible = local;
            if (val == cbToNext.SelectedIndex) return;
            cbToNext.SelectedIndex = (val >= cbToNext.Items.Count) ? -1 : val;
        }

        private void setGUID(byte[] o, int sub) { setGUID(true, (UInt32)(o[sub] | o[sub + 1] << 8 | o[sub + 2] << 16 | o[sub + 3] << 24)); }
        private void setGUID(bool setTB, UInt32 guid)
        {
            if (setTB) this.tbGUID.Text = "0x" + SimPe.Helper.HexString(guid);
            this.lbGUIDText.Text = BhavWiz.FormatGUID(true, guid);
        }

        #region iBhavOperandWizForm
        public Panel WizPanel { get { return this.pnWiz0x001f; } }

        public void Execute(Instruction inst)
        {
            this.inst = inst;

            wrappedByteArray ops1 = inst.Operands;
            wrappedByteArray ops2 = inst.Reserved1;

            internalchg = true;

            setGUID(ops1, 0);

            this.cbToNext.SelectedIndex = -1;
            setToNext((byte)(ops1[4] & 0x7f));

            this.ckbStackObj.Checked = (ops1[4] & 0x80) == 0;
            this.pnObject.Enabled = !this.ckbStackObj.Checked;

            doid1 = new DataOwnerControl(inst, this.cbDataOwner1, this.cbPicker1, this.tbVal1, this.cbDecimal, this.cbAttrPicker, null,
                ops1[0x05], ops1[0x07]);

            this.tbLocalVar.Text = "0x" + SimPe.Helper.HexString(ops1[0x06]);

            this.pnNodeVersion.Enabled = (inst.NodeVersion != 0);
            this.ckbDisabled.Checked = (ops2[0x00] & 0x01) != 0;
            this.pnWhere.Enabled = this.ckbWhere.Checked = (ops2[0x00] & 0x02) != 0;

            ushort where = BhavWiz.ToShort(ops2[0x01], ops2[0x02]);
            this.cbWhere.SelectedIndex = -1;
            if (this.cbWhere.Items.Count > where)
                this.cbWhere.SelectedIndex = where;
            this.tbWhereVal.Text = "0x" + SimPe.Helper.HexString(BhavWiz.ToShort(ops2[0x03], ops2[0x04]));

            internalchg = false;
        }

		public Instruction Write(Instruction inst)
		{
			if (inst != null)
			{
                wrappedByteArray ops1 = inst.Operands;
                wrappedByteArray ops2 = inst.Reserved1;

                UInt32 val = Convert.ToUInt32(this.tbGUID.Text, 16);
                ops1[0x00] = (byte)(val & 0xff);
                ops1[0x01] = (byte)(val >> 8 & 0xff);
                ops1[0x02] = (byte)(val >> 16 & 0xff);
                ops1[0x03] = (byte)(val >> 24 & 0xff);
                if (this.cbToNext.SelectedIndex >= 0)
                    ops1[0x04] = (byte)(this.cbToNext.SelectedIndex & 0x7f);
                ops1[0x04] |= (byte)(!this.ckbStackObj.Checked ? 0x80 : 0x00);
                ops1[0x05] = doid1.DataOwner;
                ops1[0x06] = Convert.ToByte(this.tbLocalVar.Text, 16);
                ops1[0x07] = (byte)(doid1.Value & 0xff);

                ops2[0x00] &= 0xfc;
                ops2[0x00] |= (byte)(this.ckbDisabled.Checked ? 0x01 : 0x00);
                ops2[0x00] |= (byte)(this.ckbWhere.Checked ? 0x02 : 0x00);
                if (this.cbWhere.SelectedIndex >= 0)
                    BhavWiz.FromShort(ref ops2, 1, (ushort)this.cbWhere.SelectedIndex);
                BhavWiz.FromShort(ref ops2, 3, (ushort)Convert.ToUInt32(this.tbWhereVal.Text, 16));
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
            this.pnWiz0x001f = new System.Windows.Forms.Panel();
            this.cbToNext = new System.Windows.Forms.ComboBox();
            this.tbLocalVar = new System.Windows.Forms.TextBox();
            this.tbGUID = new System.Windows.Forms.TextBox();
            this.lbGUIDText = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnNodeVersion = new System.Windows.Forms.Panel();
            this.pnWhere = new System.Windows.Forms.Panel();
            this.cbWhere = new System.Windows.Forms.ComboBox();
            this.tbWhereVal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ckbWhere = new System.Windows.Forms.CheckBox();
            this.ckbDisabled = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnObject = new System.Windows.Forms.Panel();
            this.cbAttrPicker = new System.Windows.Forms.CheckBox();
            this.cbDecimal = new System.Windows.Forms.CheckBox();
            this.cbPicker1 = new System.Windows.Forms.ComboBox();
            this.tbVal1 = new System.Windows.Forms.TextBox();
            this.cbDataOwner1 = new System.Windows.Forms.ComboBox();
            this.ckbStackObj = new System.Windows.Forms.CheckBox();
            this.pnWiz0x001f.SuspendLayout();
            this.pnNodeVersion.SuspendLayout();
            this.pnWhere.SuspendLayout();
            this.pnObject.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnWiz0x001f
            // 
            this.pnWiz0x001f.Controls.Add(this.cbToNext);
            this.pnWiz0x001f.Controls.Add(this.tbLocalVar);
            this.pnWiz0x001f.Controls.Add(this.tbGUID);
            this.pnWiz0x001f.Controls.Add(this.lbGUIDText);
            this.pnWiz0x001f.Controls.Add(this.label2);
            this.pnWiz0x001f.Controls.Add(this.pnNodeVersion);
            this.pnWiz0x001f.Controls.Add(this.label1);
            this.pnWiz0x001f.Controls.Add(this.pnObject);
            this.pnWiz0x001f.Controls.Add(this.ckbStackObj);
            this.pnWiz0x001f.Location = new System.Drawing.Point(0, 0);
            this.pnWiz0x001f.Margin = new System.Windows.Forms.Padding(2);
            this.pnWiz0x001f.Name = "pnWiz0x001f";
            this.pnWiz0x001f.Size = new System.Drawing.Size(404, 170);
            this.pnWiz0x001f.TabIndex = 0;
            // 
            // cbToNext
            // 
            this.cbToNext.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbToNext.DropDownWidth = 450;
            this.cbToNext.FormattingEnabled = true;
            this.cbToNext.Location = new System.Drawing.Point(59, 72);
            this.cbToNext.Margin = new System.Windows.Forms.Padding(2);
            this.cbToNext.Name = "cbToNext";
            this.cbToNext.Size = new System.Drawing.Size(254, 21);
            this.cbToNext.TabIndex = 17;
            this.cbToNext.SelectedIndexChanged += new System.EventHandler(this.cbToNext_SelectedIndexChanged);
            // 
            // tbLocalVar
            // 
            this.tbLocalVar.Location = new System.Drawing.Point(312, 72);
            this.tbLocalVar.Margin = new System.Windows.Forms.Padding(2);
            this.tbLocalVar.Name = "tbLocalVar";
            this.tbLocalVar.Size = new System.Drawing.Size(33, 20);
            this.tbLocalVar.TabIndex = 15;
            this.tbLocalVar.Text = "0xDD";
            this.tbLocalVar.Validated += new System.EventHandler(this.hex8_Validated);
            this.tbLocalVar.Validating += new System.ComponentModel.CancelEventHandler(this.hex8_Validating);
            // 
            // tbGUID
            // 
            this.tbGUID.Location = new System.Drawing.Point(312, 72);
            this.tbGUID.Margin = new System.Windows.Forms.Padding(2);
            this.tbGUID.Name = "tbGUID";
            this.tbGUID.Size = new System.Drawing.Size(82, 20);
            this.tbGUID.TabIndex = 16;
            this.tbGUID.Text = "0xDDDDDDDD";
            this.tbGUID.TextChanged += new System.EventHandler(this.tbGUID_TextChanged);
            this.tbGUID.Validated += new System.EventHandler(this.hex32_Validated);
            this.tbGUID.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            // 
            // lbGUIDText
            // 
            this.lbGUIDText.AutoSize = true;
            this.lbGUIDText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbGUIDText.Location = new System.Drawing.Point(56, 98);
            this.lbGUIDText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbGUIDText.Name = "lbGUIDText";
            this.lbGUIDText.Size = new System.Drawing.Size(58, 13);
            this.lbGUIDText.TabIndex = 14;
            this.lbGUIDText.Text = "GUID Text";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(10, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "To next";
            // 
            // pnNodeVersion
            // 
            this.pnNodeVersion.Controls.Add(this.pnWhere);
            this.pnNodeVersion.Controls.Add(this.ckbWhere);
            this.pnNodeVersion.Controls.Add(this.ckbDisabled);
            this.pnNodeVersion.Location = new System.Drawing.Point(6, 117);
            this.pnNodeVersion.Margin = new System.Windows.Forms.Padding(2);
            this.pnNodeVersion.Name = "pnNodeVersion";
            this.pnNodeVersion.Size = new System.Drawing.Size(312, 50);
            this.pnNodeVersion.TabIndex = 10;
            // 
            // pnWhere
            // 
            this.pnWhere.Controls.Add(this.cbWhere);
            this.pnWhere.Controls.Add(this.tbWhereVal);
            this.pnWhere.Controls.Add(this.label4);
            this.pnWhere.Location = new System.Drawing.Point(57, 0);
            this.pnWhere.Margin = new System.Windows.Forms.Padding(2);
            this.pnWhere.Name = "pnWhere";
            this.pnWhere.Size = new System.Drawing.Size(250, 19);
            this.pnWhere.TabIndex = 15;
            // 
            // cbWhere
            // 
            this.cbWhere.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWhere.DropDownWidth = 280;
            this.cbWhere.FormattingEnabled = true;
            this.cbWhere.Location = new System.Drawing.Point(0, 0);
            this.cbWhere.Margin = new System.Windows.Forms.Padding(2);
            this.cbWhere.Name = "cbWhere";
            this.cbWhere.Size = new System.Drawing.Size(151, 21);
            this.cbWhere.TabIndex = 17;
            // 
            // tbWhereVal
            // 
            this.tbWhereVal.Location = new System.Drawing.Point(200, 0);
            this.tbWhereVal.Margin = new System.Windows.Forms.Padding(2);
            this.tbWhereVal.Name = "tbWhereVal";
            this.tbWhereVal.Size = new System.Drawing.Size(51, 20);
            this.tbWhereVal.TabIndex = 16;
            this.tbWhereVal.Text = "0xDDDD";
            this.tbWhereVal.Validated += new System.EventHandler(this.hex16_Validated);
            this.tbWhereVal.Validating += new System.ComponentModel.CancelEventHandler(this.hex16_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(155, 2);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "equals";
            // 
            // ckbWhere
            // 
            this.ckbWhere.AutoSize = true;
            this.ckbWhere.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbWhere.Location = new System.Drawing.Point(0, 2);
            this.ckbWhere.Margin = new System.Windows.Forms.Padding(2);
            this.ckbWhere.Name = "ckbWhere";
            this.ckbWhere.Size = new System.Drawing.Size(55, 17);
            this.ckbWhere.TabIndex = 10;
            this.ckbWhere.Text = "where";
            this.ckbWhere.UseVisualStyleBackColor = true;
            this.ckbWhere.CheckedChanged += new System.EventHandler(this.ckbWhere_CheckedChanged);
            // 
            // ckbDisabled
            // 
            this.ckbDisabled.AutoSize = true;
            this.ckbDisabled.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbDisabled.Location = new System.Drawing.Point(0, 26);
            this.ckbDisabled.Margin = new System.Windows.Forms.Padding(2);
            this.ckbDisabled.Name = "ckbDisabled";
            this.ckbDisabled.Size = new System.Drawing.Size(140, 17);
            this.ckbDisabled.TabIndex = 10;
            this.ckbDisabled.Text = "Include disabled objects";
            this.ckbDisabled.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Set";
            // 
            // pnObject
            // 
            this.pnObject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnObject.Controls.Add(this.cbAttrPicker);
            this.pnObject.Controls.Add(this.cbDecimal);
            this.pnObject.Controls.Add(this.cbPicker1);
            this.pnObject.Controls.Add(this.tbVal1);
            this.pnObject.Controls.Add(this.cbDataOwner1);
            this.pnObject.Location = new System.Drawing.Point(28, 19);
            this.pnObject.Margin = new System.Windows.Forms.Padding(2);
            this.pnObject.Name = "pnObject";
            this.pnObject.Size = new System.Drawing.Size(369, 49);
            this.pnObject.TabIndex = 5;
            // 
            // cbAttrPicker
            // 
            this.cbAttrPicker.AutoSize = true;
            this.cbAttrPicker.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbAttrPicker.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbAttrPicker.Location = new System.Drawing.Point(248, 30);
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
            this.cbDecimal.Location = new System.Drawing.Point(92, 30);
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
            this.cbPicker1.Location = new System.Drawing.Point(242, 0);
            this.cbPicker1.Margin = new System.Windows.Forms.Padding(2);
            this.cbPicker1.Name = "cbPicker1";
            this.cbPicker1.Size = new System.Drawing.Size(128, 21);
            this.cbPicker1.TabIndex = 2;
            this.cbPicker1.Visible = false;
            // 
            // tbVal1
            // 
            this.tbVal1.Location = new System.Drawing.Point(242, 0);
            this.tbVal1.Margin = new System.Windows.Forms.Padding(2);
            this.tbVal1.Name = "tbVal1";
            this.tbVal1.Size = new System.Drawing.Size(108, 20);
            this.tbVal1.TabIndex = 2;
            // 
            // cbDataOwner1
            // 
            this.cbDataOwner1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner1.DropDownWidth = 384;
            this.cbDataOwner1.Location = new System.Drawing.Point(0, 0);
            this.cbDataOwner1.Margin = new System.Windows.Forms.Padding(2);
            this.cbDataOwner1.Name = "cbDataOwner1";
            this.cbDataOwner1.Size = new System.Drawing.Size(242, 21);
            this.cbDataOwner1.TabIndex = 1;
            // 
            // ckbStackObj
            // 
            this.ckbStackObj.AutoSize = true;
            this.ckbStackObj.Location = new System.Drawing.Point(32, 0);
            this.ckbStackObj.Margin = new System.Windows.Forms.Padding(2);
            this.ckbStackObj.Name = "ckbStackObj";
            this.ckbStackObj.Size = new System.Drawing.Size(88, 17);
            this.ckbStackObj.TabIndex = 0;
            this.ckbStackObj.Text = "Stack Object";
            this.ckbStackObj.UseVisualStyleBackColor = true;
            this.ckbStackObj.CheckedChanged += new System.EventHandler(this.ckbStackObj_CheckedChanged);
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(637, 413);
            this.Controls.Add(this.pnWiz0x001f);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UI";
            this.Text = "UI";
            this.pnWiz0x001f.ResumeLayout(false);
            this.pnWiz0x001f.PerformLayout();
            this.pnNodeVersion.ResumeLayout(false);
            this.pnNodeVersion.PerformLayout();
            this.pnWhere.ResumeLayout(false);
            this.pnWhere.PerformLayout();
            this.pnObject.ResumeLayout(false);
            this.pnObject.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        private void hex8_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (hex8_IsValid(sender)) return;

            e.Cancel = true;

            bool origstate = internalchg;
            internalchg = true;
            ((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(inst.Operands[0x06]);
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

        private void hex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (inst.NodeVersion < 2 && hex8_IsValid(sender)) return;
            else if (hex16_IsValid(sender)) return;

            e.Cancel = true;

            bool origstate = internalchg;
            internalchg = true;
            ((TextBox)sender).Text = "0x" + SimPe.Helper.HexString(BhavWiz.ToShort(inst.Reserved1[0x03], inst.Reserved1[0x04]));
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

        private void tbGUID_TextChanged(object sender, EventArgs e)
        {
            if (!hex32_IsValid(sender)) return;
            setGUID(false, Convert.ToUInt32(((TextBox)sender).Text, 16));
        }

        private void hex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (hex32_IsValid(sender)) return;

            e.Cancel = true;

            bool origstate = internalchg;
            internalchg = true;
            ((TextBox)sender).Text
               = "0x" + SimPe.Helper.HexString(inst.Operands[0x00] | (inst.Operands[0x01] << 8) | (inst.Operands[0x02] << 16) | (inst.Operands[0x03] << 24));
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

        private void cbToNext_SelectedIndexChanged(object sender, EventArgs e)
        {
            setToNext((byte)((ComboBox)sender).SelectedIndex);
        }

        private void ckbStackObj_CheckedChanged(object sender, EventArgs e)
        {
            this.pnObject.Enabled = !this.ckbStackObj.Checked;
        }

        private void ckbWhere_CheckedChanged(object sender, EventArgs e)
        {
            this.pnWhere.Enabled = this.ckbWhere.Checked;
        }

	}

}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWiz0x001f : pjse.ABhavOperandWiz
	{
        public BhavOperandWiz0x001f(Instruction i) : base(i) { myForm = new Wiz0x001f.UI(); }

		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm = null;
		}
		#endregion

	}

}
