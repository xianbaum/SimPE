namespace SimPe.Database
{
    partial class DatabaseDock
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) components.Dispose();
                if (db != null) { db.Dispose(); db = null; }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnBuild = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.grid = new System.Windows.Forms.DataGridView();
            this.colTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIhi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIlo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblStatus = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            //
            // pnlTop
            //
            this.pnlTop.Controls.Add(this.txtSearch);
            this.pnlTop.Controls.Add(this.lblSearch);
            this.pnlTop.Controls.Add(this.btnBuild);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(600, 32);
            this.pnlTop.TabIndex = 0;
            //
            // btnBuild
            //
            this.btnBuild.Location = new System.Drawing.Point(4, 4);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(120, 24);
            this.btnBuild.TabIndex = 0;
            this.btnBuild.Text = "Build / Sync Cache";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            //
            // lblSearch
            //
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(136, 9);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(47, 15);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Search:";
            //
            // txtSearch
            //
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(189, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(405, 23);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            //
            // grid
            //
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colTypeName,
                this.colType,
                this.colGroup,
                this.colIhi,
                this.colIlo,
                this.colName,
                this.colFile});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grid.Location = new System.Drawing.Point(0, 32);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersVisible = false;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(600, 336);
            this.grid.TabIndex = 1;
            //
            // colTypeName
            //
            this.colTypeName.HeaderText = "Type";
            this.colTypeName.Name = "colTypeName";
            this.colTypeName.ReadOnly = true;
            this.colTypeName.Width = 80;
            //
            // colType
            //
            this.colType.HeaderText = "T";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.Width = 90;
            //
            // colGroup
            //
            this.colGroup.HeaderText = "Group";
            this.colGroup.Name = "colGroup";
            this.colGroup.ReadOnly = true;
            this.colGroup.Width = 90;
            //
            // colIhi
            //
            this.colIhi.HeaderText = "Instance (hi)";
            this.colIhi.Name = "colIhi";
            this.colIhi.ReadOnly = true;
            this.colIhi.Width = 90;
            //
            // colIlo
            //
            this.colIlo.HeaderText = "Instance";
            this.colIlo.Name = "colIlo";
            this.colIlo.ReadOnly = true;
            this.colIlo.Width = 90;
            //
            // colName
            //
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colName.FillWeight = 40F;
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            //
            // colFile
            //
            this.colFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFile.FillWeight = 60F;
            this.colFile.HeaderText = "Package";
            this.colFile.Name = "colFile";
            this.colFile.ReadOnly = true;
            //
            // lblStatus
            //
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Location = new System.Drawing.Point(0, 368);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lblStatus.Size = new System.Drawing.Size(600, 20);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // timer
            //
            this.timer.Interval = 300;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            //
            // DatabaseDock
            //
            this.ButtonText = "Database";
            this.CaptionText = "Resource Database";
            this.Controls.Add(this.grid);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.lblStatus);
            this.FloatingSize = new System.Drawing.Size(600, 388);
            this.Name = "DatabaseDock";
            this.Size = new System.Drawing.Size(600, 388);
            this.TabText = "Database";
            this.Text = "Resource Database";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIhi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIlo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFile;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer timer;
    }
}
