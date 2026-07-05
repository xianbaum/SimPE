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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for TtabMotiveGroupUI.
	/// </summary>
	public class TtabMotiveGroupUI : System.Windows.Forms.UserControl
	{
		#region Form variables
		private System.Windows.Forms.GroupBox gbMotiveGroup;
		private System.Windows.Forms.Label lbMin;
		private System.Windows.Forms.Label lbDelta;
        private System.Windows.Forms.Label lbType;
        private System.Windows.Forms.Button btnClear;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public TtabMotiveGroupUI()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

            int muiW, muiH;
            {
                TtabSingleMotiveUI c = new TtabSingleMotiveUI();
                muiW = c.Width;
                muiH = c.Height;
            }

            this.lbMin.Left = muiW / 6 - this.lbMin.Width / 2;
            this.lbDelta.Left = muiW / 2 - this.lbDelta.Width / 2;
            this.lbType.Left = (5 * muiW) / 6 - this.lbType.Width / 2;
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
        }


        #region Extra attributes
        private TtabItemMotiveGroup item = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public String MGName
        {
            get { return this.gbMotiveGroup.Text; }
            set { this.gbMotiveGroup.Text = value; }
        }

        private ArrayList tops = new ArrayList();
        public int[] Tops { get { return (int[])tops.ToArray(typeof(Int32)); } }
        #endregion

        #region TtabMotiveGroupUI

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TtabItemMotiveGroup MotiveGroup
		{
            get { return item; }
			set
			{
                if (this.item != value)
                {
                    if (item != null)
                        item.Wrapper.WrapperChanged -= new System.EventHandler(this.WrapperChanged);
                    this.item = value;
                    setData();
                    if (item != null)
                        item.Wrapper.WrapperChanged += new System.EventHandler(this.WrapperChanged);
                }
            }
		}

        private void WrapperChanged(object sender, System.EventArgs e)
        {
            if (sender != item) return;
            setData();
        }

        private void setData()
        {
            this.SuspendLayout();

            this.gbMotiveGroup.Controls.Clear();
            tops = new ArrayList();

            int nextTop = this.lbMin.Bottom + 2;
            int width = this.Width;

            if (item != null)
            {
                if (item.Parent.Type == TtabItemMotiveTableType.Human)
                {
                    this.gbMotiveGroup.Controls.Add(this.lbMin);
                    this.gbMotiveGroup.Controls.Add(this.lbDelta);
                    this.gbMotiveGroup.Controls.Add(this.lbType);

                    for (int i = 0; i < item.Count; i++)
                    {
                        TtabSingleMotiveUI c = new TtabSingleMotiveUI();
                        c.Motive = (TtabItemSingleMotiveItem)item[i];

                        this.gbMotiveGroup.Controls.Add(c);
                        c.Location = new Point(2, nextTop);
                        tops.Add(nextTop);
                        nextTop += c.Height + 2;
                        width = c.Width;
                    }
                }
                else
                {
                    for (int i = 0; i < item.Count; i++)
                    {
                        TtabAnimalMotiveUI c = new TtabAnimalMotiveUI();
                        c.Motive = (TtabItemAnimalMotiveItem)item[i];

                        this.gbMotiveGroup.Controls.Add(c);
                        c.Location = new Point(2, nextTop);
                        tops.Add(nextTop);
                        nextTop += c.Height + 2;
                        width = c.Width;
                    }
                }
            }

            this.Width = 2 + width + 2;
            this.gbMotiveGroup.Controls.Add(this.btnClear);
            this.btnClear.Location = new Point((this.Width - this.btnClear.Width) / 2, nextTop + 2);
            this.Height = this.btnClear.Bottom + 4;

            this.ResumeLayout();
        }

		#endregion

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.gbMotiveGroup = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.lbMin = new System.Windows.Forms.Label();
            this.lbDelta = new System.Windows.Forms.Label();
            this.lbType = new System.Windows.Forms.Label();
            this.gbMotiveGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMotiveGroup
            // 
            this.gbMotiveGroup.Controls.Add(this.btnClear);
            this.gbMotiveGroup.Controls.Add(this.lbMin);
            this.gbMotiveGroup.Controls.Add(this.lbDelta);
            this.gbMotiveGroup.Controls.Add(this.lbType);
            this.gbMotiveGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMotiveGroup.Location = new System.Drawing.Point(0, 0);
            this.gbMotiveGroup.Name = "gbMotiveGroup";
            this.gbMotiveGroup.Size = new System.Drawing.Size(152, 441);
            this.gbMotiveGroup.TabIndex = 1;
            this.gbMotiveGroup.TabStop = false;
            this.gbMotiveGroup.Text = "MGName";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClear.Location = new System.Drawing.Point(38, 404);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(72, 24);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lbMin
            // 
            this.lbMin.AutoSize = true;
            this.lbMin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMin.Location = new System.Drawing.Point(13, 16);
            this.lbMin.Name = "lbMin";
            this.lbMin.Size = new System.Drawing.Size(27, 13);
            this.lbMin.TabIndex = 3;
            this.lbMin.Text = "Min.";
            this.lbMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDelta
            // 
            this.lbDelta.AutoSize = true;
            this.lbDelta.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbDelta.Location = new System.Drawing.Point(56, 16);
            this.lbDelta.Name = "lbDelta";
            this.lbDelta.Size = new System.Drawing.Size(32, 13);
            this.lbDelta.TabIndex = 2;
            this.lbDelta.Text = "Delta";
            this.lbDelta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbType
            // 
            this.lbType.AutoSize = true;
            this.lbType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbType.Location = new System.Drawing.Point(104, 16);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(31, 13);
            this.lbType.TabIndex = 1;
            this.lbType.Text = "Type";
            this.lbType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TtabMotiveGroupUI
            // 
            this.Controls.Add(this.gbMotiveGroup);
            this.Name = "TtabMotiveGroupUI";
            this.Size = new System.Drawing.Size(152, 441);
            this.gbMotiveGroup.ResumeLayout(false);
            this.gbMotiveGroup.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void btnClear_Click(object sender, System.EventArgs e)
		{
            item.Clear();
        }

	}

	#region MotiveClickEvent
	public class MotiveClickEventArgs : System.EventArgs
	{
		private int motive;
		public MotiveClickEventArgs(int m) : base()  { motive = m; }
		public int Motive { get { return motive; } }
	}
	public delegate void MotiveClickEventHandler(object sender, MotiveClickEventArgs e);
	#endregion
}
