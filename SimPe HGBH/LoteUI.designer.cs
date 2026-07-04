using System;
using System.Collections.Generic;
using System.Text;

namespace SimPe.Plugin
{
    partial class LoteUI
    {

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoteUI));
            this.lbtype = new System.Windows.Forms.Label();
            this.cbtype = new System.Windows.Forms.ComboBox();
            this.lbnotes = new System.Windows.Forms.Label();
            this.rtLotDef = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lbtype
            // 
            resources.ApplyResources(this.lbtype, "lbtype");
            this.lbtype.BackColor = System.Drawing.Color.Transparent;
            this.lbtype.Name = "lbtype";
            // 
            // cbtype
            // 
            this.cbtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbtype, "cbtype");
            this.cbtype.FormattingEnabled = true;
            this.cbtype.Name = "cbtype";
            this.cbtype.SelectedIndexChanged += new System.EventHandler(this.cbtype_SelectedIndexChanged);
            // 
            // lbnotes
            // 
            resources.ApplyResources(this.lbnotes, "lbnotes");
            this.lbnotes.BackColor = System.Drawing.Color.Transparent;
            this.lbnotes.Name = "lbnotes";
            // 
            // rtLotDef
            // 
            this.rtLotDef.AcceptsTab = true;
            resources.ApplyResources(this.rtLotDef, "rtLotDef");
            this.rtLotDef.BackColor = System.Drawing.SystemColors.Window;
            this.rtLotDef.Name = "rtLotDef";
            this.rtLotDef.ReadOnly = true;
            // 
            // LoteUI
            // 
            this.BackgroundImageAnchor = SimPe.Windows.Forms.WrapperBaseControl.ImageLayout.CenterLeft;
            this.BackgroundImageLocation = new System.Drawing.Point(608, 0);
            this.BackgroundImageZoomToFit = true;
            this.ShowLogo = true;
            this.Controls.Add(this.lbtype);
            this.Controls.Add(this.cbtype);
            this.Controls.Add(this.lbnotes);
            this.Controls.Add(this.rtLotDef);
            this.DoubleBuffered = true;
            resources.ApplyResources(this, "$this");
            this.Name = "LoteUI";
            this.Controls.SetChildIndex(this.rtLotDef, 0);
            this.Controls.SetChildIndex(this.lbnotes, 0);
            this.Controls.SetChildIndex(this.cbtype, 0);
            this.Controls.SetChildIndex(this.lbtype, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.RichTextBox rtLotDef;
        private System.Windows.Forms.Label lbnotes;
        private System.Windows.Forms.ComboBox cbtype;
        private System.Windows.Forms.Label lbtype;
    }
}
