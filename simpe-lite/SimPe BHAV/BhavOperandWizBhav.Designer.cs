namespace pjse.BhavOperandWizards.WizBhav
{
    partial class UI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI));
            this.label1 = new System.Windows.Forms.Label();
            this.pnWizBhav = new System.Windows.Forms.Panel();
            this.tlpHeader = new System.Windows.Forms.TableLayoutPanel();
            this.lbArgC = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbBhavName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNodeVersion = new System.Windows.Forms.TextBox();
            this.pnNoArgs = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbNoArgs = new System.Windows.Forms.CheckBox();
            this.pnWizBhav.SuspendLayout();
            this.tlpHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pnWizBhav
            // 
            this.pnWizBhav.Controls.Add(this.tlpHeader);
            resources.ApplyResources(this.pnWizBhav, "pnWizBhav");
            this.pnWizBhav.Name = "pnWizBhav";
            // 
            // tlpHeader
            // 
            resources.ApplyResources(this.tlpHeader, "tlpHeader");
            this.tlpHeader.Controls.Add(this.cbNoArgs, 1, 3);
            this.tlpHeader.Controls.Add(this.lbArgC, 1, 1);
            this.tlpHeader.Controls.Add(this.label4, 0, 2);
            this.tlpHeader.Controls.Add(this.lbBhavName, 1, 0);
            this.tlpHeader.Controls.Add(this.label3, 0, 1);
            this.tlpHeader.Controls.Add(this.label2, 0, 0);
            this.tlpHeader.Controls.Add(this.tbNodeVersion, 1, 2);
            this.tlpHeader.Name = "tlpHeader";
            // 
            // lbArgC
            // 
            resources.ApplyResources(this.lbArgC, "lbArgC");
            this.lbArgC.Name = "lbArgC";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // lbBhavName
            // 
            resources.ApplyResources(this.lbBhavName, "lbBhavName");
            this.lbBhavName.Name = "lbBhavName";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tbNodeVersion
            // 
            resources.ApplyResources(this.tbNodeVersion, "tbNodeVersion");
            this.tbNodeVersion.Name = "tbNodeVersion";
            // 
            // pnNoArgs
            // 
            resources.ApplyResources(this.pnNoArgs, "pnNoArgs");
            this.pnNoArgs.Name = "pnNoArgs";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // cbNoArgs
            // 
            resources.ApplyResources(this.cbNoArgs, "cbNoArgs");
            this.cbNoArgs.Name = "cbNoArgs";
            this.cbNoArgs.UseVisualStyleBackColor = true;
            // 
            // UI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnNoArgs);
            this.Controls.Add(this.pnWizBhav);
            this.Controls.Add(this.label1);
            this.Name = "UI";
            this.pnWizBhav.ResumeLayout(false);
            this.pnWizBhav.PerformLayout();
            this.tlpHeader.ResumeLayout(false);
            this.tlpHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Panel pnWizBhav;
        private System.Windows.Forms.Panel pnNoArgs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tlpHeader;
        private System.Windows.Forms.Label lbArgC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbBhavName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbNodeVersion;
        private System.Windows.Forms.CheckBox cbNoArgs;
    }
}