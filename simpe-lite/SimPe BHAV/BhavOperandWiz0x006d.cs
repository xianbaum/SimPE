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
    internal class UI : System.Windows.Forms.Form
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
        private Panel pnMaterialDoid;
        private ComboBox cbPicker2;
        private TextBox tbVal2;
        private ComboBox cbDataOwner2;
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
        private Panel pnMeshDoid;
        private ComboBox cbPicker4;
        private TextBox tbVal4;
        private ComboBox cbDataOwner4;
        private Panel pnNotAllOver;
        private CheckBox ckbAllOver;
        private ComboBox comboBox1;
        private Label label4;
        private Label label7;
        private TextBox tbMesh;
        private Button btnMesh;
        private TextBox tbVal5;
        private Label label8;
        private CheckBox checkBox1;
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

            rb1group = new ArrayList(new Control[] { this.rb1ScrShot, this.rb1Me, this.rb1Object });
            rb3group = new ArrayList(new Control[] { this.rb3Me, this.rb3Object });
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
        private DataOwnerControl doid4 = null;
        private DataOwnerControl doid5 = null;
        ArrayList rb1group = null;
        ArrayList rb3group = null;
        private bool internalchg = false;

        private void MaterialFrom(int newState)
        {
            bool isScrShot = false;
            bool isMe = false;
            bool isObject = false;
            switch (newState)
            {
                case 0:
                    isScrShot = true;
                    break;
                case 1:
                    isMe = true;
                    break;
                case 2:
                    isObject = true;
                    break;
            }
            this.pnNotScrShot.Enabled = !isScrShot;
            this.btnMaterial.Visible = this.tbMaterial.Visible = isMe;
            this.pnMaterialDoid.Enabled = isObject;
        }

        private void MeshFrom(int newState)
        {
            bool isMe = false;
            bool isObject = false;
            switch (newState)
            {
                case 0:
                    isMe = true;
                    break;
                case 1:
                    isObject = true;
                    break;
            }
            this.btnMesh.Visible = this.tbMesh.Visible = isMe;
            this.pnMeshDoid.Enabled = isObject;
        }

        public void Execute(Instruction inst)
        {
            this.inst = inst;

            wrappedByteArray ops1 = inst.Operands;
            wrappedByteArray ops2 = inst.Reserved1;

            internalchg = true;

            doid1 = new DataOwnerControl(inst, this.cbDataOwner1, this.cbPicker1, this.tbVal1, ops1[0x05], ops1[0x07]);
            doid2 = new DataOwnerControl(inst, this.cbDataOwner2, this.cbPicker2, this.tbVal2, ops1[0x05], ops1[0x07]);
            doid3 = new DataOwnerControl(inst, null, null, this.tbVal3, 0x07, ops1[0x07]);
            doid4 = new DataOwnerControl(inst, this.cbDataOwner4, this.cbPicker4, this.tbVal4, ops1[0x05], ops1[0x07]);
            doid5 = new DataOwnerControl(inst, null, null, this.tbVal5, 0x07, ops1[0x07]);

            doid1.Decimal = doid2.Decimal = doid3.Decimal = doid4.Decimal = doid5.Decimal =
                this.cbDecimal.Checked = pjse.Settings.PJSE.DecimalDOValue;
            doid1.UseAttrPicker = doid2.UseAttrPicker = doid4.UseAttrPicker =
                this.cbAttrPicker.Checked = pjse.Settings.PJSE.AttrPickerAsText;

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
            this.pnWiz0x006d = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnNotAllOver = new System.Windows.Forms.Panel();
            this.tbMesh = new System.Windows.Forms.TextBox();
            this.btnMesh = new System.Windows.Forms.Button();
            this.tbVal5 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ckbAllOver = new System.Windows.Forms.CheckBox();
            this.rb3Object = new System.Windows.Forms.RadioButton();
            this.rb3Me = new System.Windows.Forms.RadioButton();
            this.pnMeshDoid = new System.Windows.Forms.Panel();
            this.cbPicker4 = new System.Windows.Forms.ComboBox();
            this.tbVal4 = new System.Windows.Forms.TextBox();
            this.cbDataOwner4 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbPicker1 = new System.Windows.Forms.ComboBox();
            this.cbDecimal = new System.Windows.Forms.CheckBox();
            this.tbVal1 = new System.Windows.Forms.TextBox();
            this.cbAttrPicker = new System.Windows.Forms.CheckBox();
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
            this.pnMaterialDoid = new System.Windows.Forms.Panel();
            this.cbPicker2 = new System.Windows.Forms.ComboBox();
            this.tbVal2 = new System.Windows.Forms.TextBox();
            this.cbDataOwner2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnWiz0x006d.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnNotAllOver.SuspendLayout();
            this.pnMeshDoid.SuspendLayout();
            this.pnMaterial.SuspendLayout();
            this.pnNotScrShot.SuspendLayout();
            this.pnMaterialDoid.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnWiz0x006d
            // 
            this.pnWiz0x006d.Controls.Add(this.panel1);
            this.pnWiz0x006d.Controls.Add(this.cbPicker1);
            this.pnWiz0x006d.Controls.Add(this.cbDecimal);
            this.pnWiz0x006d.Controls.Add(this.tbVal1);
            this.pnWiz0x006d.Controls.Add(this.cbAttrPicker);
            this.pnWiz0x006d.Controls.Add(this.cbDataOwner1);
            this.pnWiz0x006d.Controls.Add(this.pnMaterial);
            this.pnWiz0x006d.Controls.Add(this.label1);
            resources.ApplyResources(this.pnWiz0x006d, "pnWiz0x006d");
            this.pnWiz0x006d.Name = "pnWiz0x006d";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnNotAllOver);
            this.panel1.Controls.Add(this.ckbAllOver);
            this.panel1.Controls.Add(this.rb3Object);
            this.panel1.Controls.Add(this.rb3Me);
            this.panel1.Controls.Add(this.pnMeshDoid);
            this.panel1.Controls.Add(this.label2);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // pnNotAllOver
            // 
            this.pnNotAllOver.Controls.Add(this.tbMesh);
            this.pnNotAllOver.Controls.Add(this.btnMesh);
            this.pnNotAllOver.Controls.Add(this.tbVal5);
            this.pnNotAllOver.Controls.Add(this.label8);
            this.pnNotAllOver.Controls.Add(this.checkBox1);
            this.pnNotAllOver.Controls.Add(this.comboBox1);
            this.pnNotAllOver.Controls.Add(this.label4);
            resources.ApplyResources(this.pnNotAllOver, "pnNotAllOver");
            this.pnNotAllOver.Name = "pnNotAllOver";
            // 
            // tbMesh
            // 
            this.tbMesh.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.tbMesh, "tbMesh");
            this.tbMesh.Name = "tbMesh";
            this.tbMesh.ReadOnly = true;
            this.tbMesh.TabStop = false;
            // 
            // btnMesh
            // 
            resources.ApplyResources(this.btnMesh, "btnMesh");
            this.btnMesh.Name = "btnMesh";
            // 
            // tbVal5
            // 
            resources.ApplyResources(this.tbVal5, "tbVal5");
            this.tbVal5.Name = "tbVal5";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.Name = "comboBox1";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // ckbAllOver
            // 
            resources.ApplyResources(this.ckbAllOver, "ckbAllOver");
            this.ckbAllOver.Name = "ckbAllOver";
            this.ckbAllOver.UseVisualStyleBackColor = true;
            this.ckbAllOver.CheckedChanged += new System.EventHandler(this.ckbAllOver_CheckedChanged);
            // 
            // rb3Object
            // 
            resources.ApplyResources(this.rb3Object, "rb3Object");
            this.rb3Object.Name = "rb3Object";
            this.rb3Object.TabStop = true;
            this.rb3Object.UseVisualStyleBackColor = true;
            this.rb3Object.CheckedChanged += new System.EventHandler(this.rb3group_CheckedChanged);
            // 
            // rb3Me
            // 
            resources.ApplyResources(this.rb3Me, "rb3Me");
            this.rb3Me.Name = "rb3Me";
            this.rb3Me.TabStop = true;
            this.rb3Me.UseVisualStyleBackColor = true;
            this.rb3Me.CheckedChanged += new System.EventHandler(this.rb3group_CheckedChanged);
            // 
            // pnMeshDoid
            // 
            this.pnMeshDoid.Controls.Add(this.cbPicker4);
            this.pnMeshDoid.Controls.Add(this.tbVal4);
            this.pnMeshDoid.Controls.Add(this.cbDataOwner4);
            resources.ApplyResources(this.pnMeshDoid, "pnMeshDoid");
            this.pnMeshDoid.Name = "pnMeshDoid";
            // 
            // cbPicker4
            // 
            this.cbPicker4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker4.DropDownWidth = 384;
            resources.ApplyResources(this.cbPicker4, "cbPicker4");
            this.cbPicker4.Name = "cbPicker4";
            // 
            // tbVal4
            // 
            resources.ApplyResources(this.tbVal4, "tbVal4");
            this.tbVal4.Name = "tbVal4";
            // 
            // cbDataOwner4
            // 
            this.cbDataOwner4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner4.DropDownWidth = 384;
            resources.ApplyResources(this.cbDataOwner4, "cbDataOwner4");
            this.cbDataOwner4.Name = "cbDataOwner4";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cbPicker1
            // 
            this.cbPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker1.DropDownWidth = 384;
            resources.ApplyResources(this.cbPicker1, "cbPicker1");
            this.cbPicker1.Name = "cbPicker1";
            // 
            // cbDecimal
            // 
            resources.ApplyResources(this.cbDecimal, "cbDecimal");
            this.cbDecimal.Name = "cbDecimal";
            this.cbDecimal.CheckedChanged += new System.EventHandler(this.cbDecimal_CheckedChanged);
            // 
            // tbVal1
            // 
            resources.ApplyResources(this.tbVal1, "tbVal1");
            this.tbVal1.Name = "tbVal1";
            // 
            // cbAttrPicker
            // 
            resources.ApplyResources(this.cbAttrPicker, "cbAttrPicker");
            this.cbAttrPicker.Name = "cbAttrPicker";
            this.cbAttrPicker.CheckedChanged += new System.EventHandler(this.cbAttrPicker_CheckedChanged);
            // 
            // cbDataOwner1
            // 
            this.cbDataOwner1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner1.DropDownWidth = 384;
            resources.ApplyResources(this.cbDataOwner1, "cbDataOwner1");
            this.cbDataOwner1.Name = "cbDataOwner1";
            // 
            // pnMaterial
            // 
            this.pnMaterial.Controls.Add(this.pnNotScrShot);
            this.pnMaterial.Controls.Add(this.rb1Object);
            this.pnMaterial.Controls.Add(this.rb1Me);
            this.pnMaterial.Controls.Add(this.rb1ScrShot);
            this.pnMaterial.Controls.Add(this.pnMaterialDoid);
            this.pnMaterial.Controls.Add(this.label3);
            resources.ApplyResources(this.pnMaterial, "pnMaterial");
            this.pnMaterial.Name = "pnMaterial";
            // 
            // pnNotScrShot
            // 
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
            resources.ApplyResources(this.pnNotScrShot, "pnNotScrShot");
            this.pnNotScrShot.Name = "pnNotScrShot";
            // 
            // tbMaterial
            // 
            this.tbMaterial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.tbMaterial, "tbMaterial");
            this.tbMaterial.Name = "tbMaterial";
            this.tbMaterial.ReadOnly = true;
            this.tbMaterial.TabStop = false;
            // 
            // btnMaterial
            // 
            resources.ApplyResources(this.btnMaterial, "btnMaterial");
            this.btnMaterial.Name = "btnMaterial";
            // 
            // tbVal3
            // 
            resources.ApplyResources(this.tbVal3, "tbVal3");
            this.tbVal3.Name = "tbVal3";
            // 
            // cbMatScope
            // 
            this.cbMatScope.FormattingEnabled = true;
            resources.ApplyResources(this.cbMatScope, "cbMatScope");
            this.cbMatScope.Name = "cbMatScope";
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
            // ckbMaterialTemp
            // 
            resources.ApplyResources(this.ckbMaterialTemp, "ckbMaterialTemp");
            this.ckbMaterialTemp.Name = "ckbMaterialTemp";
            this.ckbMaterialTemp.UseVisualStyleBackColor = true;
            this.ckbMaterialTemp.CheckedChanged += new System.EventHandler(this.ckbMaterialTemp_CheckedChanged);
            // 
            // rb2MovingTexture
            // 
            resources.ApplyResources(this.rb2MovingTexture, "rb2MovingTexture");
            this.rb2MovingTexture.Name = "rb2MovingTexture";
            this.rb2MovingTexture.TabStop = true;
            this.rb2MovingTexture.UseVisualStyleBackColor = true;
            // 
            // rb2Material
            // 
            resources.ApplyResources(this.rb2Material, "rb2Material");
            this.rb2Material.Name = "rb2Material";
            this.rb2Material.TabStop = true;
            this.rb2Material.UseVisualStyleBackColor = true;
            // 
            // rb1Object
            // 
            resources.ApplyResources(this.rb1Object, "rb1Object");
            this.rb1Object.Name = "rb1Object";
            this.rb1Object.TabStop = true;
            this.rb1Object.UseVisualStyleBackColor = true;
            this.rb1Object.CheckedChanged += new System.EventHandler(this.rb1group_CheckedChanged);
            // 
            // rb1Me
            // 
            resources.ApplyResources(this.rb1Me, "rb1Me");
            this.rb1Me.Name = "rb1Me";
            this.rb1Me.TabStop = true;
            this.rb1Me.UseVisualStyleBackColor = true;
            this.rb1Me.CheckedChanged += new System.EventHandler(this.rb1group_CheckedChanged);
            // 
            // rb1ScrShot
            // 
            resources.ApplyResources(this.rb1ScrShot, "rb1ScrShot");
            this.rb1ScrShot.Name = "rb1ScrShot";
            this.rb1ScrShot.TabStop = true;
            this.rb1ScrShot.UseVisualStyleBackColor = true;
            this.rb1ScrShot.CheckedChanged += new System.EventHandler(this.rb1group_CheckedChanged);
            // 
            // pnMaterialDoid
            // 
            this.pnMaterialDoid.Controls.Add(this.cbPicker2);
            this.pnMaterialDoid.Controls.Add(this.tbVal2);
            this.pnMaterialDoid.Controls.Add(this.cbDataOwner2);
            resources.ApplyResources(this.pnMaterialDoid, "pnMaterialDoid");
            this.pnMaterialDoid.Name = "pnMaterialDoid";
            // 
            // cbPicker2
            // 
            this.cbPicker2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPicker2.DropDownWidth = 384;
            resources.ApplyResources(this.cbPicker2, "cbPicker2");
            this.cbPicker2.Name = "cbPicker2";
            // 
            // tbVal2
            // 
            resources.ApplyResources(this.tbVal2, "tbVal2");
            this.tbVal2.Name = "tbVal2";
            // 
            // cbDataOwner2
            // 
            this.cbDataOwner2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataOwner2.DropDownWidth = 384;
            resources.ApplyResources(this.cbDataOwner2, "cbDataOwner2");
            this.cbDataOwner2.Name = "cbDataOwner2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // UI
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pnWiz0x006d);
            this.Name = "UI";
            this.pnWiz0x006d.ResumeLayout(false);
            this.pnWiz0x006d.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnNotAllOver.ResumeLayout(false);
            this.pnNotAllOver.PerformLayout();
            this.pnMeshDoid.ResumeLayout(false);
            this.pnMeshDoid.PerformLayout();
            this.pnMaterial.ResumeLayout(false);
            this.pnMaterial.PerformLayout();
            this.pnNotScrShot.ResumeLayout(false);
            this.pnNotScrShot.PerformLayout();
            this.pnMaterialDoid.ResumeLayout(false);
            this.pnMaterialDoid.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void cbDecimal_CheckedChanged(object sender, System.EventArgs e)
		{
            doid1.Decimal = doid2.Decimal = doid3.Decimal = doid4.Decimal = doid5.Decimal = pjse.Settings.PJSE.DecimalDOValue = this.cbDecimal.Checked;
		}

		private void cbAttrPicker_CheckedChanged(object sender, System.EventArgs e)
		{
            doid1.UseAttrPicker = doid2.UseAttrPicker = doid4.UseAttrPicker = pjse.Settings.PJSE.AttrPickerAsText = this.cbAttrPicker.Checked;
		}

        private void rb1group_CheckedChanged(object sender, EventArgs e)
        {
            this.MaterialFrom(rb1group.IndexOf(sender));
        }

        private void rb3group_CheckedChanged(object sender, EventArgs e)
        {
            this.MeshFrom(rb3group.IndexOf(sender));
        }

        private void ckbAllOver_CheckedChanged(object sender, EventArgs e)
        {
            this.pnNotAllOver.Enabled = !((CheckBox)sender).Checked;
        }

        private void ckbMaterialTemp_CheckedChanged(object sender, EventArgs e)
        {
            this.tbVal3.Enabled = this.btnMaterial.Enabled = this.tbMaterial.Enabled =
                !((CheckBox)sender).Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.tbVal5.Enabled = this.btnMesh.Enabled = this.tbMesh.Enabled =
                !((CheckBox)sender).Checked;
        }

	}

}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWiz0x006d : pjse.ABhavOperandWiz
	{
		public BhavOperandWiz0x006d() : base() { }

        public BhavOperandWiz0x006d(Instruction i) : base(i) { }


		private Wiz0x006d.UI myForm = null;
		public override Panel bhavPrimWizPanel
		{
			get
			{
				if (myForm == null) myForm = new Wiz0x006d.UI();
				return myForm.pnWiz0x006d;
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
