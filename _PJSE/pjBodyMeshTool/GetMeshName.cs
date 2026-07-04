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
 *   59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.             *
 ***************************************************************************/
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace pj
{
    public class GetMeshName : Form
    {
        private Label label1;
        private TextBox tbMeshName;
        private Label label2;
        private Button btnOK;
        private Button btnBrowse;
        private Button btnCancel;
        private Label label3;
        private CheckBox cbusecres;
        private booby.gradientpanel gradientpanel1;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetMeshName));
            this.label1 = new System.Windows.Forms.Label();
            this.tbMeshName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbusecres = new System.Windows.Forms.CheckBox();
            this.gradientpanel1 = new booby.gradientpanel();
            this.gradientpanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(411, 88);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // tbMeshName
            // 
            this.tbMeshName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMeshName.Location = new System.Drawing.Point(115, 95);
            this.tbMeshName.Margin = new System.Windows.Forms.Padding(2);
            this.tbMeshName.Name = "tbMeshName";
            this.tbMeshName.Size = new System.Drawing.Size(236, 23);
            this.tbMeshName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(9, 98);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Base mesh name";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.AutoSize = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOK.Location = new System.Drawing.Point(363, 95);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(57, 25);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBrowse.AutoSize = true;
            this.btnBrowse.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.btnBrowse.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBrowse.Location = new System.Drawing.Point(112, 122);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(63, 25);
            this.btnBrowse.TabIndex = 12;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(363, 122);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 25);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(9, 128);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Select package";
            // 
            // cbusecres
            // 
            this.cbusecres.AutoSize = true;
            this.cbusecres.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbusecres.Location = new System.Drawing.Point(212, 125);
            this.cbusecres.Name = "cbusecres";
            this.cbusecres.Size = new System.Drawing.Size(135, 19);
            this.cbusecres.TabIndex = 13;
            this.cbusecres.Text = "Try a 3ID Rresource ";
            this.cbusecres.UseVisualStyleBackColor = true;
            this.cbusecres.CheckedChanged += new System.EventHandler(this.cbusecres_CheckedChanged);
            // 
            // gradientpanel1
            // 
            this.gradientpanel1.Controls.Add(this.cbusecres);
            this.gradientpanel1.Controls.Add(this.label3);
            this.gradientpanel1.Controls.Add(this.label2);
            this.gradientpanel1.Controls.Add(this.btnOK);
            this.gradientpanel1.Controls.Add(this.btnBrowse);
            this.gradientpanel1.Controls.Add(this.btnCancel);
            this.gradientpanel1.Controls.Add(this.tbMeshName);
            this.gradientpanel1.Controls.Add(this.label1);
            this.gradientpanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradientpanel1.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.gradientpanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientpanel1.Name = "gradientpanel1";
            this.gradientpanel1.Size = new System.Drawing.Size(429, 159);
            this.gradientpanel1.TabIndex = 13;
            // 
            // GetMeshName
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(429, 159);
            this.ControlBox = false;
            this.Controls.Add(this.gradientpanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "GetMeshName";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sim Mesh Extractor";
            this.gradientpanel1.ResumeLayout(false);
            this.gradientpanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public GetMeshName()
        {
            InitializeComponent();
            if (booby.ThemeManager.ThemedForms)
            {
                booby.ThemeManager tm = booby.ThemeManager.Global.CreateChild();
                tm.AddControl(this.gradientpanel1);
                tm.AddControl(this.btnOK);
                tm.AddControl(this.btnBrowse);
                tm.AddControl(this.btnCancel);
            }
            if (SimPe.Helper.WindowsRegistry.UseBigIcons)
                this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);

            this.cbusecres.Checked = Settings.BodyMeshExtractUseCres;
        }

        public String MeshName
        {
            get
            {
                return tbMeshName.Text;
            }
        }

        private void cbusecres_CheckedChanged(object sender, EventArgs e)
        {
            Settings.BodyMeshExtractUseCres = this.cbusecres.Checked;
        }
    }
}
