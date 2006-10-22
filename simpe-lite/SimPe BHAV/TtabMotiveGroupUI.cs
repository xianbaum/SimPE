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
        private TextBox tbUnknown;
        private Label lbUnknown;
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

            this.lbMin.Location = new Point(muiW / 4 - this.lbMin.Width / 2, 13);
            this.lbDelta.Location = new Point(muiW / 2 - this.lbDelta.Width / 2, 13);
            this.lbType.Location = new Point((3 * muiW) / 4 - this.lbType.Width / 2, 13);
            this.Width = 2 + muiW + 2;
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
        private TtabItemMotiveGroup item = null;
        private bool internalchg;


        public TtabItemMotiveGroup MotiveGroup
		{
            get { return item; }
			set
			{
                this.item = value;
                setData();
                item.Wrapper.WrapperChanged += new System.EventHandler(this.WrapperChanged);
            }
		}

        private void WrapperChanged(object sender, System.EventArgs e)
        {
            if (internalchg || sender != item) return;
            setData();
        }


        private void setData()
        {
            foreach (Control c in this.gbMotiveGroup.Controls)
                if (c is TtabSingleMotiveUI)
                    this.Controls.Remove(c);

            int nextTop = 35;
            for (int i = 0; i < item.Count; i++)
            {
                TtabSingleMotiveUI c = new TtabSingleMotiveUI();
                this.gbMotiveGroup.Controls.Add(c);
                c.Motive = item[i];
                c.Location = new Point(2, nextTop);
                nextTop += c.Height + 2;
            }
            this.btnClear.Top = nextTop + 2;
            if (item.Parent.Type == TtabItemMotiveTableType.Human)
            {
                this.lbUnknown.Visible = this.tbUnknown.Visible = false;
                this.Height = this.btnClear.Bottom + 4;
            }
            else
            {
                this.lbUnknown.Top = this.btnClear.Bottom + 2;
                this.tbUnknown.Top = this.lbUnknown.Bottom + 2;
                this.tbUnknown.Text = "0x" + SimPe.Helper.HexString(item.Unknown);
                this.lbUnknown.Visible = this.tbUnknown.Visible = true;
                this.Height = this.tbUnknown.Bottom + 4;
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
            this.tbUnknown = new System.Windows.Forms.TextBox();
            this.lbUnknown = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.lbMin = new System.Windows.Forms.Label();
            this.lbDelta = new System.Windows.Forms.Label();
            this.lbType = new System.Windows.Forms.Label();
            this.gbMotiveGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMotiveGroup
            // 
            this.gbMotiveGroup.Controls.Add(this.tbUnknown);
            this.gbMotiveGroup.Controls.Add(this.lbUnknown);
            this.gbMotiveGroup.Controls.Add(this.btnClear);
            this.gbMotiveGroup.Controls.Add(this.lbMin);
            this.gbMotiveGroup.Controls.Add(this.lbDelta);
            this.gbMotiveGroup.Controls.Add(this.lbType);
            resources.ApplyResources(this.gbMotiveGroup, "gbMotiveGroup");
            this.gbMotiveGroup.Name = "gbMotiveGroup";
            this.gbMotiveGroup.TabStop = false;
            // 
            // tbUnknown
            // 
            resources.ApplyResources(this.tbUnknown, "tbUnknown");
            this.tbUnknown.Name = "tbUnknown";
            // 
            // lbUnknown
            // 
            resources.ApplyResources(this.lbUnknown, "lbUnknown");
            this.lbUnknown.Name = "lbUnknown";
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
            internalchg = true;
            foreach (Control c in this.Controls)
                if (c is TtabSingleMotiveUI)
                    ((TtabSingleMotiveUI)c).Clear();
            internalchg = false;
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
