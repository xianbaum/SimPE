/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Ambertation.Windows.Forms;

namespace SimPe.Plugin.Tool.Dockable
{
    partial class FinderDock
    {
        private booby.gradientpanel GradientPanel1;
        private booby.TaskBox tbResult;
        private Panel panel1;
        private Panel panel2;
        private ComboBox cbTask;
        private Label label1;
        private Label pnErr;
        private ListView lvreal;
        private ToolStrip toolBar1;
        private ToolStripButton biList;
        private ToolStripButton biTile;
        private ToolStripButton biDetail;
        private ColumnHeader columnHeader0;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private System.ComponentModel.IContainer components;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {            
            if (disposing)
            {                
                if (tm != null) tm.Clear();
                tm = null;

                if (components != null)
                {
                    components.Dispose();
                }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FinderDock));
            this.GradientPanel1 = new booby.gradientpanel();
            this.tbResult = new booby.TaskBox();
            this.lvreal = new System.Windows.Forms.ListView();
            this.pnContainer = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbTask = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnErr = new System.Windows.Forms.Label();
            this.toolBar1 = new System.Windows.Forms.ToolStrip();
            this.biList = new System.Windows.Forms.ToolStripButton();
            this.biTile = new System.Windows.Forms.ToolStripButton();
            this.biDetail = new System.Windows.Forms.ToolStripButton();
            this.columnHeader0 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.GradientPanel1.SuspendLayout();
            this.tbResult.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GradientPanel1
            // 
            this.GradientPanel1.Controls.Add(this.tbResult);
            this.GradientPanel1.Controls.Add(this.pnContainer);
            this.GradientPanel1.Controls.Add(this.panel1);
            this.GradientPanel1.Controls.Add(this.pnErr);
            this.GradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GradientPanel1.Location = new System.Drawing.Point(0, 25);
            this.GradientPanel1.Name = "GradientPanel1";
            this.GradientPanel1.Padding = new System.Windows.Forms.Padding(8);
            this.GradientPanel1.Size = new System.Drawing.Size(254, 451);
            this.GradientPanel1.TabIndex = 0;
            // 
            // tbResult
            // 
            this.tbResult.BackColor = System.Drawing.Color.Transparent;
            this.tbResult.Controls.Add(this.lvreal);
            this.tbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbResult.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold);
            this.tbResult.HeaderText = "Results";
            this.tbResult.Icon = ((System.Drawing.Image)(resources.GetObject("tbResult.Icon")));
            this.tbResult.IconLocation = new System.Drawing.Point(4, 0);
            this.tbResult.IconSize = new System.Drawing.Size(32, 32);
            this.tbResult.Location = new System.Drawing.Point(8, 159);
            this.tbResult.Name = "tbResult";
            this.tbResult.Padding = new System.Windows.Forms.Padding(4, 32, 4, 4);
            this.tbResult.Size = new System.Drawing.Size(238, 284);
            this.tbResult.TabIndex = 4;
            this.tbResult.TopGap = 6;
            // 
            // lvreal
            // 
            this.lvreal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvreal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader0,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lvreal.Location = new System.Drawing.Point(8, 36);
            this.lvreal.Name = "lvreal";
            this.lvreal.Size = new System.Drawing.Size(222, 241);
            this.lvreal.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvreal.TabIndex = 0;
            this.lvreal.TileSize = new System.Drawing.Size(350, 90);
            this.lvreal.UseCompatibleStateImageBehavior = false;
            this.lvreal.DoubleClick += new System.EventHandler(this.lvreal_DoubleClick);
            this.lvreal.View = System.Windows.Forms.View.Details;
            this.lvreal.ColumnClick += new ColumnClickEventHandler(lvreal_ColumnClick);
            // 
            // columnHeader0
            // 
            this.columnHeader0.Text = "Resource Name";
            this.columnHeader0.Width = 200;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Type";
            this.columnHeader1.Width = 140;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Group";
            this.columnHeader2.Width = 90;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Instance";
            this.columnHeader3.Width = 140;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Offset";
            this.columnHeader4.Width = 90;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Size";
            this.columnHeader5.Width = 90;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Filename";
            this.columnHeader6.Width = 600;
            // 
            // pnContainer
            // 
            this.pnContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnContainer.Location = new System.Drawing.Point(8, 65);
            this.pnContainer.Name = "pnContainer";
            this.pnContainer.Size = new System.Drawing.Size(238, 94);
            this.pnContainer.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.cbTask);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(8, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 24);
            this.panel1.TabIndex = 3;
            // 
            // cbTask
            // 
            this.cbTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTask.Location = new System.Drawing.Point(48, 0);
            this.cbTask.Name = "cbTask";
            this.cbTask.Size = new System.Drawing.Size(190, 21);
            this.cbTask.TabIndex = 3;
            this.cbTask.SelectedIndexChanged += new System.EventHandler(this.cbTask_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Find:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // toolBar1
            // 
            this.toolBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.biList,
            this.biTile,
            this.biDetail});
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.Size = new System.Drawing.Size(254, 25);
            this.toolBar1.TabIndex = 1;
            this.toolBar1.Text = "toolBar1";
            // 
            // biList
            // 
            this.biList.Image = ((System.Drawing.Image)(resources.GetObject("biList.Image")));
            this.biList.Name = "biList";
            this.biList.Size = new System.Drawing.Size(23, 22);
            this.biList.ToolTipText = "List View";
            this.biList.Click += new System.EventHandler(this.Activate_biList);
            // 
            // biTile
            // 
            this.biTile.Image = ((System.Drawing.Image)(resources.GetObject("biTile.Image")));
            this.biTile.Name = "biTile";
            this.biTile.Size = new System.Drawing.Size(23, 22);
            this.biTile.ToolTipText = "Tiled View";
            this.biTile.Click += new System.EventHandler(this.Activate_biTile);
            // 
            // biDetail
            // 
            this.biDetail.Checked = true;
            this.biDetail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.biDetail.Image = ((System.Drawing.Image)(resources.GetObject("biDetail.Image")));
            this.biDetail.Name = "biDetail";
            this.biDetail.Size = new System.Drawing.Size(23, 22);
            this.biDetail.ToolTipText = "Detailed View";
            this.biDetail.Click += new System.EventHandler(this.Activate_biDetails);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(304, 402);
            this.panel2.TabIndex = 5;
            // 
            // pnErr
            // 
            this.pnErr.BackColor = System.Drawing.Color.Transparent;
            this.pnErr.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnErr.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnErr.ForeColor = System.Drawing.Color.Maroon;
            this.pnErr.Location = new System.Drawing.Point(8, 8);
            this.pnErr.Name = "pnErr";
            this.pnErr.Size = new System.Drawing.Size(238, 33);
            this.pnErr.TabIndex = 6;
            this.pnErr.Text = "Only the first {nr} Results are displayed below.";
            this.pnErr.Visible = false;
            // 
            // FinderDock
            // 
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(208, 288);
            this.ButtonText = "Finder";
            this.CaptionText = "Scenegraph Resource Finder";
            this.Controls.Add(this.GradientPanel1);
            this.Controls.Add(this.toolBar1);
            this.FloatingSize = new System.Drawing.Size(266, 502);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Image = ((System.Drawing.Image)(resources.GetObject("$this.Image")));
            this.Name = "FinderDock";
            this.Size = new System.Drawing.Size(254, 476);
            this.TabImage = ((System.Drawing.Image)(resources.GetObject("$this.TabImage")));
            this.TabText = "Finder";
            this.GradientPanel1.ResumeLayout(false);
            this.tbResult.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.toolBar1.ResumeLayout(false);
            this.toolBar1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}
