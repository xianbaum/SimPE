namespace pjse
{
    partial class pjse_banner
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbLabel = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnRefreshFT = new System.Windows.Forms.Button();
            this.btnFloat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbLabel
            // 
            this.lbLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbLabel.AutoSize = true;
            this.lbLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbLabel.Location = new System.Drawing.Point(0, 3);
            this.lbLabel.Name = "lbLabel";
            this.lbLabel.Size = new System.Drawing.Size(209, 20);
            this.lbLabel.TabIndex = 1;
            this.lbLabel.Text = "PJSE: file type Editor";
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHelp.Location = new System.Drawing.Point(455, 0);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(57, 27);
            this.btnHelp.TabIndex = 4;
            this.btnHelp.Text = "Help";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnRefreshFT
            // 
            this.btnRefreshFT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshFT.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRefreshFT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRefreshFT.Location = new System.Drawing.Point(397, 0);
            this.btnRefreshFT.Name = "btnRefreshFT";
            this.btnRefreshFT.Size = new System.Drawing.Size(57, 27);
            this.btnRefreshFT.TabIndex = 3;
            this.btnRefreshFT.Text = "RFT";
            this.btnRefreshFT.Click += new System.EventHandler(this.btnRefreshFT_Click);
            // 
            // btnFloat
            // 
            this.btnFloat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFloat.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFloat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFloat.Location = new System.Drawing.Point(300, 0);
            this.btnFloat.Name = "btnFloat";
            this.btnFloat.Size = new System.Drawing.Size(76, 27);
            this.btnFloat.TabIndex = 2;
            this.btnFloat.Text = "Float";
            this.btnFloat.Visible = false;
            this.btnFloat.Click += new System.EventHandler(this.btnFloat_Click);
            // 
            // pjse_banner
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.btnFloat);
            this.Controls.Add(this.btnRefreshFT);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.lbLabel);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.Name = "pjse_banner";
            this.Size = new System.Drawing.Size(512, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbLabel;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnRefreshFT;
        private System.Windows.Forms.Button btnFloat;
    }
}
