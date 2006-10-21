/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   peter@drealm.info                                                     *
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


		#region TtabMotiveGroupUI
		private TtabItem item = null;
		private int mgnr;
        private bool isHuman = true;


		public void SetData(TtabItem i, int mg, bool h)
        {
            item = i;
            MotiveGroup = mg;
            isHuman = h;

            int nRows = item[mg].Count;

            int muiW, muiH;
            {
                TtabSingleMotiveUI c = new TtabSingleMotiveUI();
                muiW = c.Width + 2;
                muiH = c.Height + 1;
            }
            this.lbMin.Location = new Point(muiW / 4 - muiW / 8, 13);
            this.lbDelta.Location = new Point(muiW / 2 - muiW / 8, 13);
            this.lbType.Location = new Point((3 * muiW) / 4 + muiW / 8, 13);

            for (int rowN = 0; rowN < nRows; rowN++)
            {
                TtabSingleMotiveUI c = new TtabSingleMotiveUI();
                this.gbMotiveGroup.Controls.Add(c);

                c.Location = new Point(2, 35 + rowN * muiH);
                c.SetData(i, mg, rowN);
            }
            this.btnClear.Location = new Point(this.btnClear.Location.X, 35 + nRows * muiH + 2);
            this.Size =
                new Size(2 + muiW + 2, this.btnClear.Location.Y + this.btnClear.Height + 4);

            this.Visible = true;
        }

        public void SetData(TtabItem i, int j) { this.SetData(i, j, isHuman); }

        public void SetData(TtabItem i) { this.SetData(i, mgnr, isHuman); }

        public void SetData() { this.SetData(item, mgnr, isHuman); }

		public int MotiveGroup
		{
			get { return mgnr; }
			set
			{
				mgnr = value;
                if (isHuman)
                    gbMotiveGroup.Text = pjse.BhavWiz.readStr(pjse.GS.BhavStr.Ages, (ushort)mgnr);
                else
                    gbMotiveGroup.Text = mgnr.ToString();
			}
		}

		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TtabMotiveGroupUI));
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
            resources.ApplyResources(this.gbMotiveGroup, "gbMotiveGroup");
            this.gbMotiveGroup.Name = "gbMotiveGroup";
            this.gbMotiveGroup.TabStop = false;
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.Name = "btnClear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lbMin
            // 
            resources.ApplyResources(this.lbMin, "lbMin");
            this.lbMin.Name = "lbMin";
            // 
            // lbDelta
            // 
            resources.ApplyResources(this.lbDelta, "lbDelta");
            this.lbDelta.Name = "lbDelta";
            // 
            // lbType
            // 
            resources.ApplyResources(this.lbType, "lbType");
            this.lbType.Name = "lbType";
            // 
            // TtabMotiveGroupUI
            // 
            this.Controls.Add(this.gbMotiveGroup);
            this.Name = "TtabMotiveGroupUI";
            resources.ApplyResources(this, "$this");
            this.gbMotiveGroup.ResumeLayout(false);
            this.gbMotiveGroup.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void btnClear_Click(object sender, System.EventArgs e)
		{
            foreach (Control c in this.Controls)
                if (c is TtabSingleMotiveUI)
                    ((TtabSingleMotiveUI)c).Clear();
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
