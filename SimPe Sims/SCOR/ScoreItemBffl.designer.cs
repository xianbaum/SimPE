using System;
using System.Collections.Generic;
using System.Text;

namespace SimPe.PackedFiles.Wrapper.SCOR
{
    partial class ScoreItemBffl
    {
        private void InitializeComponent()
        {
            this.rtbBffs = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbBffs
            // 
            this.rtbBffs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbBffs.Location = new System.Drawing.Point(0, 0);
            this.rtbBffs.Name = "rtbBffs";
            this.rtbBffs.Size = new System.Drawing.Size(261, 150);
            this.rtbBffs.TabIndex = 1;
            this.rtbBffs.Text = "";
            // 
            // ScoreItemBffl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.rtbBffs);
            this.Name = "ScoreItemBffl";
            this.Size = new System.Drawing.Size(261, 150);
            this.ResumeLayout(false);
        }
        private System.Windows.Forms.RichTextBox rtbBffs;
    }
}
