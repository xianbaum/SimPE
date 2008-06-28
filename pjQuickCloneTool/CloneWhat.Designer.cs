namespace pj
{
    partial class CloneWhat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.rg1_ByGUID = new System.Windows.Forms.RadioButton();
            this.rg1_ByGroup = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.rg1_ByGUID);
            this.flowLayoutPanel1.Controls.Add(this.rg1_ByGroup);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.tbValue);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(149, 72);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.label1, true);
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Clone what?";
            // 
            // rg1_ByGUID
            // 
            this.rg1_ByGUID.AutoSize = true;
            this.rg1_ByGUID.Location = new System.Drawing.Point(3, 26);
            this.rg1_ByGUID.Name = "rg1_ByGUID";
            this.rg1_ByGUID.Size = new System.Drawing.Size(67, 17);
            this.rg1_ByGUID.TabIndex = 1;
            this.rg1_ByGUID.TabStop = true;
            this.rg1_ByGUID.Text = "By GUID";
            this.rg1_ByGUID.UseVisualStyleBackColor = true;
            // 
            // rg1_ByGroup
            // 
            this.rg1_ByGroup.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.rg1_ByGroup, true);
            this.rg1_ByGroup.Location = new System.Drawing.Point(76, 26);
            this.rg1_ByGroup.Name = "rg1_ByGroup";
            this.rg1_ByGroup.Size = new System.Drawing.Size(69, 17);
            this.rg1_ByGroup.TabIndex = 3;
            this.rg1_ByGroup.TabStop = true;
            this.rg1_ByGroup.Text = "By Group";
            this.rg1_ByGroup.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 46);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label2.Size = new System.Drawing.Size(37, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Value:";
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(46, 49);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(100, 20);
            this.tbValue.TabIndex = 4;
            this.tbValue.Text = "0x00000000";
            this.tbValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbValue.Validating += new System.ComponentModel.CancelEventHandler(this.hex32_Validating);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(81, 78);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(0, 78);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // CloneWhat
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(157, 105);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "CloneWhat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PJ QuickClone";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton rg1_ByGUID;
        private System.Windows.Forms.RadioButton rg1_ByGroup;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label2;

    }
}