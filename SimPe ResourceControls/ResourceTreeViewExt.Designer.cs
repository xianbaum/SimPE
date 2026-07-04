namespace SimPe.Windows.Forms
{
    partial class ResourceTreeViewExt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResourceTreeViewExt));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbInst = new System.Windows.Forms.ToolStripButton();
            this.tbGroup = new System.Windows.Forms.ToolStripButton();
            this.tbType = new System.Windows.Forms.ToolStripButton();
            this.tv = new System.Windows.Forms.TreeView();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbInst,
            this.tbGroup,
            this.tbType});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tbInst
            // 
            this.tbInst.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbInst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbInst, "tbInst");
            this.tbInst.Name = "tbInst";
            this.tbInst.Click += new System.EventHandler(this.SelectTreeBuilder);
            // 
            // tbGroup
            // 
            this.tbGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbGroup, "tbGroup");
            this.tbGroup.Name = "tbGroup";
            this.tbGroup.Click += new System.EventHandler(this.SelectTreeBuilder);
            // 
            // tbType
            // 
            this.tbType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbType, "tbType");
            this.tbType.Name = "tbType";
            this.tbType.Click += new System.EventHandler(this.SelectTreeBuilder);
            // 
            // tv
            // 
            this.tv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.tv, "tv");
            this.tv.HideSelection = false;
            this.tv.Name = "tv";
            this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
            // 
            // ResourceTreeViewExt
            // 
            this.Controls.Add(this.tv);
            this.Controls.Add(this.toolStrip1);
            resources.ApplyResources(this, "$this");
            this.Name = "ResourceTreeViewExt";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.ToolStripButton tbInst;
        private System.Windows.Forms.ToolStripButton tbGroup;
        private System.Windows.Forms.ToolStripButton tbType;
    }
}
