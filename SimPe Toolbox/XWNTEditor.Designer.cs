namespace SimPe.Wants
{
    partial class XWNTEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnXWNTEditor = new booby.gradientpanel();
            this.label1 = new System.Windows.Forms.Label();
            this.pjse_banner1 = new pjse.pjse_banner();
            this.lbWant = new System.Windows.Forms.Label();
            this.btnCommit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbProperty = new System.Windows.Forms.ComboBox();
            this.lvWants = new System.Windows.Forms.ListView();
            this.chKey = new System.Windows.Forms.ColumnHeader();
            this.chType = new System.Windows.Forms.ColumnHeader();
            this.chValue = new System.Windows.Forms.ColumnHeader();
            this.label3 = new System.Windows.Forms.Label();
            this.cbVersion = new System.Windows.Forms.ComboBox();
            this.cbValue = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.pnXWNTEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnXWNTEditor
            // 
            this.pnXWNTEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnXWNTEditor.BackgroundImageAnchor = booby.gradientpanel.ImageLayout.TopRight;
            this.pnXWNTEditor.BackgroundImageLocation = new System.Drawing.Point(200, 28);
            this.pnXWNTEditor.BackgroundImageZoomToFit = true;
            this.pnXWNTEditor.Controls.Add(this.label1);
            this.pnXWNTEditor.Controls.Add(this.pjse_banner1);
            this.pnXWNTEditor.Controls.Add(this.lbWant);
            this.pnXWNTEditor.Controls.Add(this.btnCommit);
            this.pnXWNTEditor.Controls.Add(this.label2);
            this.pnXWNTEditor.Controls.Add(this.label4);
            this.pnXWNTEditor.Controls.Add(this.cbProperty);
            this.pnXWNTEditor.Controls.Add(this.lvWants);
            this.pnXWNTEditor.Controls.Add(this.label3);
            this.pnXWNTEditor.Controls.Add(this.cbVersion);
            this.pnXWNTEditor.Controls.Add(this.cbValue);
            this.pnXWNTEditor.Controls.Add(this.btnAdd);
            this.pnXWNTEditor.Controls.Add(this.btnDelete);
            this.pnXWNTEditor.Controls.Add(this.tbValue);
            this.pnXWNTEditor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnXWNTEditor.Location = new System.Drawing.Point(3, 3);
            this.pnXWNTEditor.Name = "pnXWNTEditor";
            this.pnXWNTEditor.Size = new System.Drawing.Size(829, 406);
            this.pnXWNTEditor.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Want:";
            // 
            // pjse_banner1
            // 
            this.pjse_banner1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pjse_banner1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.pjse_banner1.Location = new System.Drawing.Point(0, 0);
            this.pjse_banner1.Margin = new System.Windows.Forms.Padding(0);
            this.pjse_banner1.Name = "pjse_banner1";
            this.pjse_banner1.Size = new System.Drawing.Size(829, 27);
            this.pjse_banner1.TabIndex = 4;
            this.pjse_banner1.TitleText = "XML Want File";
            // 
            // lbWant
            // 
            this.lbWant.AutoSize = true;
            this.lbWant.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWant.Location = new System.Drawing.Point(45, 32);
            this.lbWant.Name = "lbWant";
            this.lbWant.Size = new System.Drawing.Size(53, 16);
            this.lbWant.TabIndex = 2;
            this.lbWant.Text = "lbWant";
            // 
            // btnCommit
            // 
            this.btnCommit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCommit.Enabled = false;
            this.btnCommit.Location = new System.Drawing.Point(742, 34);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(75, 23);
            this.btnCommit.TabIndex = 3;
            this.btnCommit.Text = "Commit";
            this.btnCommit.UseVisualStyleBackColor = true;
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Key:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(644, 34);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Version:";
            // 
            // cbProperty
            // 
            this.cbProperty.FormattingEnabled = true;
            this.cbProperty.Location = new System.Drawing.Point(45, 58);
            this.cbProperty.Name = "cbProperty";
            this.cbProperty.Size = new System.Drawing.Size(121, 21);
            this.cbProperty.TabIndex = 4;
            this.cbProperty.SelectedIndexChanged += new System.EventHandler(this.cbProperty_SelectedIndexChanged);
            // 
            // lvWants
            // 
            this.lvWants.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lvWants.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chKey,
            this.chType,
            this.chValue});
            this.lvWants.FullRowSelect = true;
            this.lvWants.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvWants.HideSelection = false;
            this.lvWants.LabelWrap = false;
            this.lvWants.Location = new System.Drawing.Point(0, 91);
            this.lvWants.Name = "lvWants";
            this.lvWants.Size = new System.Drawing.Size(829, 315);
            this.lvWants.TabIndex = 2;
            this.lvWants.UseCompatibleStateImageBehavior = false;
            this.lvWants.View = System.Windows.Forms.View.Details;
            this.lvWants.SelectedIndexChanged += new System.EventHandler(this.lvWants_SelectedIndexChanged);
            // 
            // chKey
            // 
            this.chKey.Text = "Key";
            this.chKey.Width = 150;
            // 
            // chType
            // 
            this.chType.Text = "Type";
            this.chType.Width = 100;
            // 
            // chValue
            // 
            this.chValue.Text = "Value";
            this.chValue.Width = 500;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(176, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Value:";
            // 
            // cbVersion
            // 
            this.cbVersion.FormattingEnabled = true;
            this.cbVersion.Location = new System.Drawing.Point(699, 30);
            this.cbVersion.Name = "cbVersion";
            this.cbVersion.Size = new System.Drawing.Size(40, 21);
            this.cbVersion.TabIndex = 9;
            this.cbVersion.SelectedIndexChanged += new System.EventHandler(this.cbVersion_SelectedIndexChanged);
            // 
            // cbValue
            // 
            this.cbValue.FormattingEnabled = true;
            this.cbValue.Location = new System.Drawing.Point(221, 58);
            this.cbValue.Name = "cbValue";
            this.cbValue.Size = new System.Drawing.Size(420, 21);
            this.cbValue.TabIndex = 6;
            this.cbValue.Visible = false;
            this.cbValue.SelectedIndexChanged += new System.EventHandler(this.cbValue_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(754, 57);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(664, 57);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(221, 58);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(420, 21);
            this.tbValue.TabIndex = 6;
            this.tbValue.TextChanged += new System.EventHandler(this.tbValue_TextChanged);
            // 
            // XWNTEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 421);
            this.Controls.Add(this.pnXWNTEditor);
            this.Name = "XWNTEditor";
            this.Text = "XWNTEditor";
            this.pnXWNTEditor.ResumeLayout(false);
            this.pnXWNTEditor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private booby.gradientpanel pnXWNTEditor;
        private System.Windows.Forms.ListView lvWants;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbWant;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbProperty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbValue;
        private System.Windows.Forms.ColumnHeader chKey;
        private System.Windows.Forms.ColumnHeader chValue;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Button btnCommit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private pjse.pjse_banner pjse_banner1;
        private System.Windows.Forms.ComboBox cbVersion;
        private System.Windows.Forms.Label label4;

    }
}