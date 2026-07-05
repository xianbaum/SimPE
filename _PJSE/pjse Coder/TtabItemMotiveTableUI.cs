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
	/// Summary description for TtabItemMotiveTableUI.
	/// </summary>
	public class TtabItemMotiveTableUI : System.Windows.Forms.UserControl
	{
		#region Form variables

        private System.Windows.Forms.Label lbMotive0;
		private System.Windows.Forms.Label lbMotive1;
		private System.Windows.Forms.Label lbMotive2;
		private System.Windows.Forms.Label lbMotive3;
		private System.Windows.Forms.Label lbMotive4;
		private System.Windows.Forms.Label lbMotive5;
		private System.Windows.Forms.Label lbMotive6;
		private System.Windows.Forms.Label lbMotive7;
		private System.Windows.Forms.Label lbMotive9;
		private System.Windows.Forms.Label lbMotive11;
		private System.Windows.Forms.Label lbMotive8;
		private System.Windows.Forms.Label lbMotive10;
		private System.Windows.Forms.Label lbMotive14;
		private System.Windows.Forms.Label lbMotive15;
		private System.Windows.Forms.Label lbMotive13;
		private System.Windows.Forms.Label lbMotive12;
        private System.Windows.Forms.Panel pnAllGroups;
		private System.Windows.Forms.CheckBox cbShowAll;
		private System.Windows.Forms.Panel pnCopyButtons;
		private System.Windows.Forms.Button btnCpyM0;
		private System.Windows.Forms.Button btnCpyM1;
		private System.Windows.Forms.Button btnCpyM2;
		private System.Windows.Forms.Button btnCpyM3;
		private System.Windows.Forms.Button btnCpyM4;
		private System.Windows.Forms.Button btnCpyM5;
		private System.Windows.Forms.Button btnCpyM7;
		private System.Windows.Forms.Button btnCpyM6;
		private System.Windows.Forms.Button btnCpyM9;
		private System.Windows.Forms.Button btnCpyM12;
		private System.Windows.Forms.Button btnCpyM11;
		private System.Windows.Forms.Button btnCpyM10;
		private System.Windows.Forms.Button btnCpyM15;
		private System.Windows.Forms.Button btnCpyM14;
		private System.Windows.Forms.Button btnCpyM13;
		private System.Windows.Forms.Button btnCpyM8;
		private System.Windows.Forms.Label lbCBM0;
		private System.Windows.Forms.Label lbCBM1;
		private System.Windows.Forms.Label lbCBM2;
		private System.Windows.Forms.Label lbCBM3;
		private System.Windows.Forms.Label lbCBM4;
		private System.Windows.Forms.Label lbCBM5;
		private System.Windows.Forms.Label lbCBM6;
		private System.Windows.Forms.Label lbCBM7;
		private System.Windows.Forms.Label lbCBM15;
		private System.Windows.Forms.Label lbCBM11;
		private System.Windows.Forms.Label lbCBM14;
		private System.Windows.Forms.Label lbCBM8;
		private System.Windows.Forms.Label lbCBM9;
		private System.Windows.Forms.Label lbCBM13;
		private System.Windows.Forms.Label lbCBM10;
		private System.Windows.Forms.Label lbCBM12;
		private System.Windows.Forms.Button btnCopyAll;
        private Label lbNrGroups;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public TtabItemMotiveTableUI()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
            pnCopyButtons.Visible = pnAllGroups.Visible = false;
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



		#region TtabItemMotiveTableUI
        private TtabItemMotiveTable item = null;

		private Button[] aButtons;
        private int maxWidth = 0;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TtabItemMotiveTable MotiveTable
        {
            get { return item; }
            set
            {
                if (this.item != value)
                {
                    if (item != null && item.Wrapper != null)
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
            cbShowAll.Enabled = false;
            this.pnAllGroups.Controls.Clear();

            if (item != null && item.Count > 0)
            {
                this.lbNrGroups.Text = (this.lbNrGroups.Text.Split(':')[0]) + ": " + item.Count.ToString();

                TtabMotiveGroupUI c = new TtabMotiveGroupUI();
                c.MotiveGroup = item[0];
                if (item.Type == TtabItemMotiveTableType.Human)
                    c.MGName = pjse.BhavWiz.readStr(pjse.GS.BhavStr.TTABAges, 0);
                else
                    c.MGName = "[0]";
                setLocations(c);

                if (item.Count > 1)
                {
                    cbShowAll.Enabled = true;
                    int nextLeft = 0;
                    for (int i = 1; i < item.Count; i++)
                    {
                        c = new TtabMotiveGroupUI();
                        this.pnAllGroups.Controls.Add(c);
                        c.MotiveGroup = item[i];
                        if (item.Type == TtabItemMotiveTableType.Human)
                            c.MGName = pjse.BhavWiz.readStr(pjse.GS.BhavStr.TTABAges, (ushort)i);
                        else
                            c.MGName = "[" + i.ToString() + "]";
                        c.Location = new Point(nextLeft, 0);
                        nextLeft += c.Width + 2;
                    }
                }
            }
            else
                this.lbNrGroups.Text = (this.lbNrGroups.Text.Split(':')[0]) + ": 0";

            cbShowAll_CheckedChanged(null, null);
        }

        private void setLocations(TtabMotiveGroupUI c)
        {
            Button[] b = {
							 btnCpyM0  ,btnCpyM1  ,btnCpyM2  ,btnCpyM3  ,btnCpyM4  ,btnCpyM5  ,btnCpyM6  ,btnCpyM7
							,btnCpyM8  ,btnCpyM9  ,btnCpyM10 ,btnCpyM11 ,btnCpyM12 ,btnCpyM13 ,btnCpyM14 ,btnCpyM15
							};
            aButtons = b;

            Label[] lbCBM = {
                lbCBM0, lbCBM1, lbCBM2, lbCBM3, lbCBM4, lbCBM5, lbCBM6, lbCBM7
                ,lbCBM8, lbCBM9, lbCBM10, lbCBM11, lbCBM12, lbCBM13, lbCBM14, lbCBM15
            };

            Label[] aMotiveLabels = {
				lbMotive0 ,lbMotive1 ,lbMotive2  ,lbMotive3  ,lbMotive4  ,lbMotive5  ,lbMotive6  ,lbMotive7
				,lbMotive8 ,lbMotive9 ,lbMotive10 ,lbMotive11 ,lbMotive12 ,lbMotive13 ,lbMotive14 ,lbMotive15
			};


            this.Controls.Clear();
            this.Controls.Add(this.cbShowAll);
            this.Controls.Add(this.lbNrGroups);
            this.Controls.Add(this.pnAllGroups);
            this.Controls.Add(this.pnCopyButtons);
            this.Controls.Add(c);

            maxWidth = this.lbNrGroups.Width;

            int cbW = 0;
            for (ushort m = 0; m < aMotiveLabels.Length; m++)
            {
                this.Controls.Add(aMotiveLabels[m]);
                aMotiveLabels[m].Text = pjse.BhavWiz.readStr(pjse.GS.BhavStr.Motives, m);
                if (aMotiveLabels[m].Width > maxWidth) maxWidth = aMotiveLabels[m].Width;
                cbW = b[m].Width;
            }

            for (ushort m = 0; m < aMotiveLabels.Length; m++)
            {
                aMotiveLabels[m].Location = new Point(maxWidth - aMotiveLabels[m].Width, c.Tops[m] + 2);
                aButtons[m].Location = new Point(0, c.Tops[m]);
                lbCBM[m].Location = new Point(cbW + 2, c.Tops[m] + 2);
            }

            this.cbShowAll.Location = new Point(maxWidth - this.cbShowAll.Width, 2);

            c.Location = new Point(maxWidth + 2, 0);
            this.Height = c.Height + 24;

            this.btnCopyAll.Location = new Point(0, c.Tops[15] + c.Tops[1] - c.Tops[0]);
            this.lbNrGroups.Location = new Point(4, this.btnCopyAll.Top + 2);

            this.pnCopyButtons.Anchor = AnchorStyles.None;
            this.pnCopyButtons.Location = new Point(c.Right + 2, 0);
            this.pnCopyButtons.Size = new Size(lbCBM0.Right + 4, this.Height);
            this.pnCopyButtons.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;

            this.pnAllGroups.Anchor = AnchorStyles.None;
            this.pnAllGroups.Location = new Point(c.Right + 2, 0);
            this.pnAllGroups.Size = new Size(this.Width - this.pnAllGroups.Left, c.Bottom + 24);
            this.pnAllGroups.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
        }


        private void doCopyMotive(int m)
        {
            for (int i = 1; i < item.Count; i++)
                item[0][m].CopyTo(item[i][m]);
        }


		#endregion

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.lbMotive0 = new System.Windows.Forms.Label();
            this.lbMotive1 = new System.Windows.Forms.Label();
            this.lbMotive2 = new System.Windows.Forms.Label();
            this.lbMotive3 = new System.Windows.Forms.Label();
            this.lbMotive4 = new System.Windows.Forms.Label();
            this.lbMotive5 = new System.Windows.Forms.Label();
            this.lbMotive6 = new System.Windows.Forms.Label();
            this.lbMotive7 = new System.Windows.Forms.Label();
            this.lbMotive9 = new System.Windows.Forms.Label();
            this.lbMotive11 = new System.Windows.Forms.Label();
            this.lbMotive8 = new System.Windows.Forms.Label();
            this.lbMotive10 = new System.Windows.Forms.Label();
            this.lbMotive14 = new System.Windows.Forms.Label();
            this.lbMotive15 = new System.Windows.Forms.Label();
            this.lbMotive13 = new System.Windows.Forms.Label();
            this.lbMotive12 = new System.Windows.Forms.Label();
            this.pnAllGroups = new System.Windows.Forms.Panel();
            this.cbShowAll = new System.Windows.Forms.CheckBox();
            this.pnCopyButtons = new System.Windows.Forms.Panel();
            this.btnCopyAll = new System.Windows.Forms.Button();
            this.lbCBM0 = new System.Windows.Forms.Label();
            this.btnCpyM0 = new System.Windows.Forms.Button();
            this.btnCpyM1 = new System.Windows.Forms.Button();
            this.btnCpyM2 = new System.Windows.Forms.Button();
            this.btnCpyM3 = new System.Windows.Forms.Button();
            this.btnCpyM4 = new System.Windows.Forms.Button();
            this.btnCpyM5 = new System.Windows.Forms.Button();
            this.btnCpyM7 = new System.Windows.Forms.Button();
            this.btnCpyM6 = new System.Windows.Forms.Button();
            this.btnCpyM9 = new System.Windows.Forms.Button();
            this.btnCpyM12 = new System.Windows.Forms.Button();
            this.btnCpyM11 = new System.Windows.Forms.Button();
            this.btnCpyM10 = new System.Windows.Forms.Button();
            this.btnCpyM15 = new System.Windows.Forms.Button();
            this.btnCpyM14 = new System.Windows.Forms.Button();
            this.btnCpyM13 = new System.Windows.Forms.Button();
            this.btnCpyM8 = new System.Windows.Forms.Button();
            this.lbCBM1 = new System.Windows.Forms.Label();
            this.lbCBM2 = new System.Windows.Forms.Label();
            this.lbCBM3 = new System.Windows.Forms.Label();
            this.lbCBM4 = new System.Windows.Forms.Label();
            this.lbCBM5 = new System.Windows.Forms.Label();
            this.lbCBM6 = new System.Windows.Forms.Label();
            this.lbCBM7 = new System.Windows.Forms.Label();
            this.lbCBM15 = new System.Windows.Forms.Label();
            this.lbCBM11 = new System.Windows.Forms.Label();
            this.lbCBM14 = new System.Windows.Forms.Label();
            this.lbCBM8 = new System.Windows.Forms.Label();
            this.lbCBM9 = new System.Windows.Forms.Label();
            this.lbCBM13 = new System.Windows.Forms.Label();
            this.lbCBM10 = new System.Windows.Forms.Label();
            this.lbCBM12 = new System.Windows.Forms.Label();
            this.lbNrGroups = new System.Windows.Forms.Label();
            this.pnCopyButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbMotive0
            // 
            this.lbMotive0.AutoSize = true;
            this.lbMotive0.BackColor = System.Drawing.Color.Transparent;
            this.lbMotive0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMotive0.Location = new System.Drawing.Point(103, 33);
            this.lbMotive0.Name = "lbMotive0";
            this.lbMotive0.Size = new System.Drawing.Size(44, 13);
            this.lbMotive0.TabIndex = 0;
            this.lbMotive0.Text = "motive0";
            // 
            // lbMotive1
            // 
            this.lbMotive1.AutoSize = true;
            this.lbMotive1.BackColor = System.Drawing.Color.Transparent;
            this.lbMotive1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMotive1.Location = new System.Drawing.Point(103, 57);
            this.lbMotive1.Name = "lbMotive1";
            this.lbMotive1.Size = new System.Drawing.Size(44, 13);
            this.lbMotive1.TabIndex = 0;
            this.lbMotive1.Text = "motive0";
            // 
            // lbMotive2
            // 
            this.lbMotive2.AutoSize = true;
            this.lbMotive2.BackColor = System.Drawing.Color.Transparent;
            this.lbMotive2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMotive2.Location = new System.Drawing.Point(103, 80);
            this.lbMotive2.Name = "lbMotive2";
            this.lbMotive2.Size = new System.Drawing.Size(44, 13);
            this.lbMotive2.TabIndex = 0;
            this.lbMotive2.Text = "motive0";
            // 
            // lbMotive3
            // 
            this.lbMotive3.AutoSize = true;
            this.lbMotive3.BackColor = System.Drawing.Color.Transparent;
            this.lbMotive3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMotive3.Location = new System.Drawing.Point(103, 103);
            this.lbMotive3.Name = "lbMotive3";
            this.lbMotive3.Size = new System.Drawing.Size(44, 13);
            this.lbMotive3.TabIndex = 0;
            this.lbMotive3.Text = "motive0";
            // 
            // lbMotive4
            // 
            this.lbMotive4.AutoSize = true;
            this.lbMotive4.BackColor = System.Drawing.Color.Transparent;
            this.lbMotive4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMotive4.Location = new System.Drawing.Point(103, 126);
            this.lbMotive4.Name = "lbMotive4";
            this.lbMotive4.Size = new System.Drawing.Size(44, 13);
            this.lbMotive4.TabIndex = 0;
            this.lbMotive4.Text = "motive0";
            // 
            // lbMotive5
            // 
            this.lbMotive5.AutoSize = true;
            this.lbMotive5.BackColor = System.Drawing.Color.Transparent;
            this.lbMotive5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMotive5.Location = new System.Drawing.Point(103, 149);
            this.lbMotive5.Name = "lbMotive5";
            this.lbMotive5.Size = new System.Drawing.Size(44, 13);
            this.lbMotive5.TabIndex = 0;
            this.lbMotive5.Text = "motive0";
            // 
            // lbMotive6
            // 
            this.lbMotive6.AutoSize = true;
            this.lbMotive6.BackColor = System.Drawing.Color.Transparent;
            this.lbMotive6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMotive6.Location = new System.Drawing.Point(103, 172);
            this.lbMotive6.Name = "lbMotive6";
            this.lbMotive6.Size = new System.Drawing.Size(44, 13);
            this.lbMotive6.TabIndex = 0;
            this.lbMotive6.Text = "motive0";
            // 
            // lbMotive7
            // 
            this.lbMotive7.AutoSize = true;
            this.lbMotive7.BackColor = System.Drawing.Color.Transparent;
            this.lbMotive7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMotive7.Location = new System.Drawing.Point(103, 195);
            this.lbMotive7.Name = "lbMotive7";
            this.lbMotive7.Size = new System.Drawing.Size(44, 13);
            this.lbMotive7.TabIndex = 0;
            this.lbMotive7.Text = "motive0";
            // 
            // lbMotive9
            // 
            this.lbMotive9.AutoSize = true;
            this.lbMotive9.BackColor = System.Drawing.Color.Transparent;
            this.lbMotive9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMotive9.Location = new System.Drawing.Point(103, 241);
            this.lbMotive9.Name = "lbMotive9";
            this.lbMotive9.Size = new System.Drawing.Size(44, 13);
            this.lbMotive9.TabIndex = 0;
            this.lbMotive9.Text = "motive0";
            // 
            // lbMotive11
            // 
            this.lbMotive11.AutoSize = true;
            this.lbMotive11.BackColor = System.Drawing.Color.Transparent;
            this.lbMotive11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMotive11.Location = new System.Drawing.Point(103, 287);
            this.lbMotive11.Name = "lbMotive11";
            this.lbMotive11.Size = new System.Drawing.Size(44, 13);
            this.lbMotive11.TabIndex = 0;
            this.lbMotive11.Text = "motive0";
            // 
            // lbMotive8
            // 
            this.lbMotive8.AutoSize = true;
            this.lbMotive8.BackColor = System.Drawing.Color.Transparent;
            this.lbMotive8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMotive8.Location = new System.Drawing.Point(103, 218);
            this.lbMotive8.Name = "lbMotive8";
            this.lbMotive8.Size = new System.Drawing.Size(44, 13);
            this.lbMotive8.TabIndex = 0;
            this.lbMotive8.Text = "motive0";
            // 
            // lbMotive10
            // 
            this.lbMotive10.AutoSize = true;
            this.lbMotive10.BackColor = System.Drawing.Color.Transparent;
            this.lbMotive10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMotive10.Location = new System.Drawing.Point(103, 264);
            this.lbMotive10.Name = "lbMotive10";
            this.lbMotive10.Size = new System.Drawing.Size(44, 13);
            this.lbMotive10.TabIndex = 0;
            this.lbMotive10.Text = "motive0";
            // 
            // lbMotive14
            // 
            this.lbMotive14.AutoSize = true;
            this.lbMotive14.BackColor = System.Drawing.Color.Transparent;
            this.lbMotive14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMotive14.Location = new System.Drawing.Point(103, 356);
            this.lbMotive14.Name = "lbMotive14";
            this.lbMotive14.Size = new System.Drawing.Size(44, 13);
            this.lbMotive14.TabIndex = 0;
            this.lbMotive14.Text = "motive0";
            // 
            // lbMotive15
            // 
            this.lbMotive15.AutoSize = true;
            this.lbMotive15.BackColor = System.Drawing.Color.Transparent;
            this.lbMotive15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMotive15.Location = new System.Drawing.Point(103, 379);
            this.lbMotive15.Name = "lbMotive15";
            this.lbMotive15.Size = new System.Drawing.Size(44, 13);
            this.lbMotive15.TabIndex = 0;
            this.lbMotive15.Text = "motive0";
            // 
            // lbMotive13
            // 
            this.lbMotive13.AutoSize = true;
            this.lbMotive13.BackColor = System.Drawing.Color.Transparent;
            this.lbMotive13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMotive13.Location = new System.Drawing.Point(103, 333);
            this.lbMotive13.Name = "lbMotive13";
            this.lbMotive13.Size = new System.Drawing.Size(44, 13);
            this.lbMotive13.TabIndex = 0;
            this.lbMotive13.Text = "motive0";
            // 
            // lbMotive12
            // 
            this.lbMotive12.AutoSize = true;
            this.lbMotive12.BackColor = System.Drawing.Color.Transparent;
            this.lbMotive12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMotive12.Location = new System.Drawing.Point(103, 310);
            this.lbMotive12.Name = "lbMotive12";
            this.lbMotive12.Size = new System.Drawing.Size(44, 13);
            this.lbMotive12.TabIndex = 0;
            this.lbMotive12.Text = "motive0";
            // 
            // pnAllGroups
            // 
            this.pnAllGroups.AutoScroll = true;
            this.pnAllGroups.BackColor = System.Drawing.Color.Transparent;
            this.pnAllGroups.Location = new System.Drawing.Point(166, 0);
            this.pnAllGroups.Name = "pnAllGroups";
            this.pnAllGroups.Size = new System.Drawing.Size(153, 456);
            this.pnAllGroups.TabIndex = 3;
            // 
            // cbShowAll
            // 
            this.cbShowAll.AutoSize = true;
            this.cbShowAll.BackColor = System.Drawing.Color.Transparent;
            this.cbShowAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbShowAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbShowAll.Location = new System.Drawing.Point(80, 0);
            this.cbShowAll.Name = "cbShowAll";
            this.cbShowAll.Size = new System.Drawing.Size(67, 17);
            this.cbShowAll.TabIndex = 1;
            this.cbShowAll.Text = "Sho&w All";
            this.cbShowAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbShowAll.UseVisualStyleBackColor = false;
            this.cbShowAll.CheckedChanged += new System.EventHandler(this.cbShowAll_CheckedChanged);
            // 
            // pnCopyButtons
            // 
            this.pnCopyButtons.BackColor = System.Drawing.Color.Transparent;
            this.pnCopyButtons.Controls.Add(this.btnCopyAll);
            this.pnCopyButtons.Controls.Add(this.lbCBM0);
            this.pnCopyButtons.Controls.Add(this.btnCpyM0);
            this.pnCopyButtons.Controls.Add(this.btnCpyM1);
            this.pnCopyButtons.Controls.Add(this.btnCpyM2);
            this.pnCopyButtons.Controls.Add(this.btnCpyM3);
            this.pnCopyButtons.Controls.Add(this.btnCpyM4);
            this.pnCopyButtons.Controls.Add(this.btnCpyM5);
            this.pnCopyButtons.Controls.Add(this.btnCpyM7);
            this.pnCopyButtons.Controls.Add(this.btnCpyM6);
            this.pnCopyButtons.Controls.Add(this.btnCpyM9);
            this.pnCopyButtons.Controls.Add(this.btnCpyM12);
            this.pnCopyButtons.Controls.Add(this.btnCpyM11);
            this.pnCopyButtons.Controls.Add(this.btnCpyM10);
            this.pnCopyButtons.Controls.Add(this.btnCpyM15);
            this.pnCopyButtons.Controls.Add(this.btnCpyM14);
            this.pnCopyButtons.Controls.Add(this.btnCpyM13);
            this.pnCopyButtons.Controls.Add(this.btnCpyM8);
            this.pnCopyButtons.Controls.Add(this.lbCBM1);
            this.pnCopyButtons.Controls.Add(this.lbCBM2);
            this.pnCopyButtons.Controls.Add(this.lbCBM3);
            this.pnCopyButtons.Controls.Add(this.lbCBM4);
            this.pnCopyButtons.Controls.Add(this.lbCBM5);
            this.pnCopyButtons.Controls.Add(this.lbCBM6);
            this.pnCopyButtons.Controls.Add(this.lbCBM7);
            this.pnCopyButtons.Controls.Add(this.lbCBM15);
            this.pnCopyButtons.Controls.Add(this.lbCBM11);
            this.pnCopyButtons.Controls.Add(this.lbCBM14);
            this.pnCopyButtons.Controls.Add(this.lbCBM8);
            this.pnCopyButtons.Controls.Add(this.lbCBM9);
            this.pnCopyButtons.Controls.Add(this.lbCBM13);
            this.pnCopyButtons.Controls.Add(this.lbCBM10);
            this.pnCopyButtons.Controls.Add(this.lbCBM12);
            this.pnCopyButtons.Location = new System.Drawing.Point(325, 0);
            this.pnCopyButtons.Name = "pnCopyButtons";
            this.pnCopyButtons.Size = new System.Drawing.Size(142, 456);
            this.pnCopyButtons.TabIndex = 4;
            // 
            // btnCopyAll
            // 
            this.btnCopyAll.BackColor = System.Drawing.Color.Transparent;
            this.btnCopyAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCopyAll.Location = new System.Drawing.Point(0, 401);
            this.btnCopyAll.Name = "btnCopyAll";
            this.btnCopyAll.Size = new System.Drawing.Size(108, 24);
            this.btnCopyAll.TabIndex = 17;
            this.btnCopyAll.Text = "Set all like this";
            this.btnCopyAll.UseVisualStyleBackColor = false;
            this.btnCopyAll.Click += new System.EventHandler(this.copy_Click);
            // 
            // lbCBM0
            // 
            this.lbCBM0.AutoSize = true;
            this.lbCBM0.BackColor = System.Drawing.Color.Transparent;
            this.lbCBM0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCBM0.Location = new System.Drawing.Point(48, 36);
            this.lbCBM0.Name = "lbCBM0";
            this.lbCBM0.Size = new System.Drawing.Size(63, 13);
            this.lbCBM0.TabIndex = 17;
            this.lbCBM0.Text = "motive to all";
            // 
            // btnCpyM0
            // 
            this.btnCpyM0.BackColor = System.Drawing.Color.Transparent;
            this.btnCpyM0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCpyM0.Location = new System.Drawing.Point(0, 33);
            this.btnCpyM0.Name = "btnCpyM0";
            this.btnCpyM0.Size = new System.Drawing.Size(50, 24);
            this.btnCpyM0.TabIndex = 1;
            this.btnCpyM0.Text = "Copy";
            this.btnCpyM0.UseVisualStyleBackColor = false;
            this.btnCpyM0.Click += new System.EventHandler(this.copy_Click);
            // 
            // btnCpyM1
            // 
            this.btnCpyM1.BackColor = System.Drawing.Color.Transparent;
            this.btnCpyM1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCpyM1.Location = new System.Drawing.Point(0, 55);
            this.btnCpyM1.Name = "btnCpyM1";
            this.btnCpyM1.Size = new System.Drawing.Size(50, 24);
            this.btnCpyM1.TabIndex = 2;
            this.btnCpyM1.Text = "Copy";
            this.btnCpyM1.UseVisualStyleBackColor = false;
            this.btnCpyM1.Click += new System.EventHandler(this.copy_Click);
            // 
            // btnCpyM2
            // 
            this.btnCpyM2.BackColor = System.Drawing.Color.Transparent;
            this.btnCpyM2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCpyM2.Location = new System.Drawing.Point(0, 78);
            this.btnCpyM2.Name = "btnCpyM2";
            this.btnCpyM2.Size = new System.Drawing.Size(50, 24);
            this.btnCpyM2.TabIndex = 3;
            this.btnCpyM2.Text = "Copy";
            this.btnCpyM2.UseVisualStyleBackColor = false;
            this.btnCpyM2.Click += new System.EventHandler(this.copy_Click);
            // 
            // btnCpyM3
            // 
            this.btnCpyM3.BackColor = System.Drawing.Color.Transparent;
            this.btnCpyM3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCpyM3.Location = new System.Drawing.Point(0, 101);
            this.btnCpyM3.Name = "btnCpyM3";
            this.btnCpyM3.Size = new System.Drawing.Size(50, 24);
            this.btnCpyM3.TabIndex = 4;
            this.btnCpyM3.Text = "Copy";
            this.btnCpyM3.UseVisualStyleBackColor = false;
            this.btnCpyM3.Click += new System.EventHandler(this.copy_Click);
            // 
            // btnCpyM4
            // 
            this.btnCpyM4.BackColor = System.Drawing.Color.Transparent;
            this.btnCpyM4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCpyM4.Location = new System.Drawing.Point(0, 124);
            this.btnCpyM4.Name = "btnCpyM4";
            this.btnCpyM4.Size = new System.Drawing.Size(50, 24);
            this.btnCpyM4.TabIndex = 5;
            this.btnCpyM4.Text = "Copy";
            this.btnCpyM4.UseVisualStyleBackColor = false;
            this.btnCpyM4.Click += new System.EventHandler(this.copy_Click);
            // 
            // btnCpyM5
            // 
            this.btnCpyM5.BackColor = System.Drawing.Color.Transparent;
            this.btnCpyM5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCpyM5.Location = new System.Drawing.Point(0, 147);
            this.btnCpyM5.Name = "btnCpyM5";
            this.btnCpyM5.Size = new System.Drawing.Size(50, 24);
            this.btnCpyM5.TabIndex = 6;
            this.btnCpyM5.Text = "Copy";
            this.btnCpyM5.UseVisualStyleBackColor = false;
            this.btnCpyM5.Click += new System.EventHandler(this.copy_Click);
            // 
            // btnCpyM7
            // 
            this.btnCpyM7.BackColor = System.Drawing.Color.Transparent;
            this.btnCpyM7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCpyM7.Location = new System.Drawing.Point(0, 193);
            this.btnCpyM7.Name = "btnCpyM7";
            this.btnCpyM7.Size = new System.Drawing.Size(50, 24);
            this.btnCpyM7.TabIndex = 8;
            this.btnCpyM7.Text = "Copy";
            this.btnCpyM7.UseVisualStyleBackColor = false;
            this.btnCpyM7.Click += new System.EventHandler(this.copy_Click);
            // 
            // btnCpyM6
            // 
            this.btnCpyM6.BackColor = System.Drawing.Color.Transparent;
            this.btnCpyM6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCpyM6.Location = new System.Drawing.Point(0, 170);
            this.btnCpyM6.Name = "btnCpyM6";
            this.btnCpyM6.Size = new System.Drawing.Size(50, 24);
            this.btnCpyM6.TabIndex = 7;
            this.btnCpyM6.Text = "Copy";
            this.btnCpyM6.UseVisualStyleBackColor = false;
            this.btnCpyM6.Click += new System.EventHandler(this.copy_Click);
            // 
            // btnCpyM9
            // 
            this.btnCpyM9.BackColor = System.Drawing.Color.Transparent;
            this.btnCpyM9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCpyM9.Location = new System.Drawing.Point(0, 239);
            this.btnCpyM9.Name = "btnCpyM9";
            this.btnCpyM9.Size = new System.Drawing.Size(50, 24);
            this.btnCpyM9.TabIndex = 10;
            this.btnCpyM9.Text = "Copy";
            this.btnCpyM9.UseVisualStyleBackColor = false;
            this.btnCpyM9.Click += new System.EventHandler(this.copy_Click);
            // 
            // btnCpyM12
            // 
            this.btnCpyM12.BackColor = System.Drawing.Color.Transparent;
            this.btnCpyM12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCpyM12.Location = new System.Drawing.Point(0, 308);
            this.btnCpyM12.Name = "btnCpyM12";
            this.btnCpyM12.Size = new System.Drawing.Size(50, 24);
            this.btnCpyM12.TabIndex = 13;
            this.btnCpyM12.Text = "Copy";
            this.btnCpyM12.UseVisualStyleBackColor = false;
            this.btnCpyM12.Click += new System.EventHandler(this.copy_Click);
            // 
            // btnCpyM11
            // 
            this.btnCpyM11.BackColor = System.Drawing.Color.Transparent;
            this.btnCpyM11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCpyM11.Location = new System.Drawing.Point(0, 285);
            this.btnCpyM11.Name = "btnCpyM11";
            this.btnCpyM11.Size = new System.Drawing.Size(50, 24);
            this.btnCpyM11.TabIndex = 12;
            this.btnCpyM11.Text = "Copy";
            this.btnCpyM11.UseVisualStyleBackColor = false;
            this.btnCpyM11.Click += new System.EventHandler(this.copy_Click);
            // 
            // btnCpyM10
            // 
            this.btnCpyM10.BackColor = System.Drawing.Color.Transparent;
            this.btnCpyM10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCpyM10.Location = new System.Drawing.Point(0, 262);
            this.btnCpyM10.Name = "btnCpyM10";
            this.btnCpyM10.Size = new System.Drawing.Size(50, 24);
            this.btnCpyM10.TabIndex = 11;
            this.btnCpyM10.Text = "Copy";
            this.btnCpyM10.UseVisualStyleBackColor = false;
            this.btnCpyM10.Click += new System.EventHandler(this.copy_Click);
            // 
            // btnCpyM15
            // 
            this.btnCpyM15.BackColor = System.Drawing.Color.Transparent;
            this.btnCpyM15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCpyM15.Location = new System.Drawing.Point(0, 377);
            this.btnCpyM15.Name = "btnCpyM15";
            this.btnCpyM15.Size = new System.Drawing.Size(50, 24);
            this.btnCpyM15.TabIndex = 16;
            this.btnCpyM15.Text = "Copy";
            this.btnCpyM15.UseVisualStyleBackColor = false;
            this.btnCpyM15.Click += new System.EventHandler(this.copy_Click);
            // 
            // btnCpyM14
            // 
            this.btnCpyM14.BackColor = System.Drawing.Color.Transparent;
            this.btnCpyM14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCpyM14.Location = new System.Drawing.Point(0, 354);
            this.btnCpyM14.Name = "btnCpyM14";
            this.btnCpyM14.Size = new System.Drawing.Size(50, 24);
            this.btnCpyM14.TabIndex = 15;
            this.btnCpyM14.Text = "Copy";
            this.btnCpyM14.UseVisualStyleBackColor = false;
            this.btnCpyM14.Click += new System.EventHandler(this.copy_Click);
            // 
            // btnCpyM13
            // 
            this.btnCpyM13.BackColor = System.Drawing.Color.Transparent;
            this.btnCpyM13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCpyM13.Location = new System.Drawing.Point(0, 331);
            this.btnCpyM13.Name = "btnCpyM13";
            this.btnCpyM13.Size = new System.Drawing.Size(50, 24);
            this.btnCpyM13.TabIndex = 14;
            this.btnCpyM13.Text = "Copy";
            this.btnCpyM13.UseVisualStyleBackColor = false;
            this.btnCpyM13.Click += new System.EventHandler(this.copy_Click);
            // 
            // btnCpyM8
            // 
            this.btnCpyM8.BackColor = System.Drawing.Color.Transparent;
            this.btnCpyM8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCpyM8.Location = new System.Drawing.Point(0, 216);
            this.btnCpyM8.Name = "btnCpyM8";
            this.btnCpyM8.Size = new System.Drawing.Size(50, 24);
            this.btnCpyM8.TabIndex = 9;
            this.btnCpyM8.Text = "Copy";
            this.btnCpyM8.UseVisualStyleBackColor = false;
            this.btnCpyM8.Click += new System.EventHandler(this.copy_Click);
            // 
            // lbCBM1
            // 
            this.lbCBM1.AutoSize = true;
            this.lbCBM1.BackColor = System.Drawing.Color.Transparent;
            this.lbCBM1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCBM1.Location = new System.Drawing.Point(48, 59);
            this.lbCBM1.Name = "lbCBM1";
            this.lbCBM1.Size = new System.Drawing.Size(63, 13);
            this.lbCBM1.TabIndex = 17;
            this.lbCBM1.Text = "motive to all";
            // 
            // lbCBM2
            // 
            this.lbCBM2.AutoSize = true;
            this.lbCBM2.BackColor = System.Drawing.Color.Transparent;
            this.lbCBM2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCBM2.Location = new System.Drawing.Point(48, 82);
            this.lbCBM2.Name = "lbCBM2";
            this.lbCBM2.Size = new System.Drawing.Size(63, 13);
            this.lbCBM2.TabIndex = 17;
            this.lbCBM2.Text = "motive to all";
            // 
            // lbCBM3
            // 
            this.lbCBM3.AutoSize = true;
            this.lbCBM3.BackColor = System.Drawing.Color.Transparent;
            this.lbCBM3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCBM3.Location = new System.Drawing.Point(48, 105);
            this.lbCBM3.Name = "lbCBM3";
            this.lbCBM3.Size = new System.Drawing.Size(63, 13);
            this.lbCBM3.TabIndex = 17;
            this.lbCBM3.Text = "motive to all";
            // 
            // lbCBM4
            // 
            this.lbCBM4.AutoSize = true;
            this.lbCBM4.BackColor = System.Drawing.Color.Transparent;
            this.lbCBM4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCBM4.Location = new System.Drawing.Point(48, 128);
            this.lbCBM4.Name = "lbCBM4";
            this.lbCBM4.Size = new System.Drawing.Size(63, 13);
            this.lbCBM4.TabIndex = 17;
            this.lbCBM4.Text = "motive to all";
            // 
            // lbCBM5
            // 
            this.lbCBM5.AutoSize = true;
            this.lbCBM5.BackColor = System.Drawing.Color.Transparent;
            this.lbCBM5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCBM5.Location = new System.Drawing.Point(48, 151);
            this.lbCBM5.Name = "lbCBM5";
            this.lbCBM5.Size = new System.Drawing.Size(63, 13);
            this.lbCBM5.TabIndex = 17;
            this.lbCBM5.Text = "motive to all";
            // 
            // lbCBM6
            // 
            this.lbCBM6.AutoSize = true;
            this.lbCBM6.BackColor = System.Drawing.Color.Transparent;
            this.lbCBM6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCBM6.Location = new System.Drawing.Point(48, 173);
            this.lbCBM6.Name = "lbCBM6";
            this.lbCBM6.Size = new System.Drawing.Size(63, 13);
            this.lbCBM6.TabIndex = 17;
            this.lbCBM6.Text = "motive to all";
            // 
            // lbCBM7
            // 
            this.lbCBM7.AutoSize = true;
            this.lbCBM7.BackColor = System.Drawing.Color.Transparent;
            this.lbCBM7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCBM7.Location = new System.Drawing.Point(48, 197);
            this.lbCBM7.Name = "lbCBM7";
            this.lbCBM7.Size = new System.Drawing.Size(63, 13);
            this.lbCBM7.TabIndex = 17;
            this.lbCBM7.Text = "motive to all";
            // 
            // lbCBM15
            // 
            this.lbCBM15.AutoSize = true;
            this.lbCBM15.BackColor = System.Drawing.Color.Transparent;
            this.lbCBM15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCBM15.Location = new System.Drawing.Point(48, 381);
            this.lbCBM15.Name = "lbCBM15";
            this.lbCBM15.Size = new System.Drawing.Size(63, 13);
            this.lbCBM15.TabIndex = 17;
            this.lbCBM15.Text = "motive to all";
            // 
            // lbCBM11
            // 
            this.lbCBM11.AutoSize = true;
            this.lbCBM11.BackColor = System.Drawing.Color.Transparent;
            this.lbCBM11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCBM11.Location = new System.Drawing.Point(48, 289);
            this.lbCBM11.Name = "lbCBM11";
            this.lbCBM11.Size = new System.Drawing.Size(63, 13);
            this.lbCBM11.TabIndex = 17;
            this.lbCBM11.Text = "motive to all";
            // 
            // lbCBM14
            // 
            this.lbCBM14.AutoSize = true;
            this.lbCBM14.BackColor = System.Drawing.Color.Transparent;
            this.lbCBM14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCBM14.Location = new System.Drawing.Point(48, 358);
            this.lbCBM14.Name = "lbCBM14";
            this.lbCBM14.Size = new System.Drawing.Size(63, 13);
            this.lbCBM14.TabIndex = 17;
            this.lbCBM14.Text = "motive to all";
            // 
            // lbCBM8
            // 
            this.lbCBM8.AutoSize = true;
            this.lbCBM8.BackColor = System.Drawing.Color.Transparent;
            this.lbCBM8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCBM8.Location = new System.Drawing.Point(48, 220);
            this.lbCBM8.Name = "lbCBM8";
            this.lbCBM8.Size = new System.Drawing.Size(63, 13);
            this.lbCBM8.TabIndex = 17;
            this.lbCBM8.Text = "motive to all";
            // 
            // lbCBM9
            // 
            this.lbCBM9.AutoSize = true;
            this.lbCBM9.BackColor = System.Drawing.Color.Transparent;
            this.lbCBM9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCBM9.Location = new System.Drawing.Point(48, 243);
            this.lbCBM9.Name = "lbCBM9";
            this.lbCBM9.Size = new System.Drawing.Size(63, 13);
            this.lbCBM9.TabIndex = 17;
            this.lbCBM9.Text = "motive to all";
            // 
            // lbCBM13
            // 
            this.lbCBM13.AutoSize = true;
            this.lbCBM13.BackColor = System.Drawing.Color.Transparent;
            this.lbCBM13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCBM13.Location = new System.Drawing.Point(48, 335);
            this.lbCBM13.Name = "lbCBM13";
            this.lbCBM13.Size = new System.Drawing.Size(63, 13);
            this.lbCBM13.TabIndex = 17;
            this.lbCBM13.Text = "motive to all";
            // 
            // lbCBM10
            // 
            this.lbCBM10.AutoSize = true;
            this.lbCBM10.BackColor = System.Drawing.Color.Transparent;
            this.lbCBM10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCBM10.Location = new System.Drawing.Point(48, 266);
            this.lbCBM10.Name = "lbCBM10";
            this.lbCBM10.Size = new System.Drawing.Size(63, 13);
            this.lbCBM10.TabIndex = 17;
            this.lbCBM10.Text = "motive to all";
            // 
            // lbCBM12
            // 
            this.lbCBM12.AutoSize = true;
            this.lbCBM12.BackColor = System.Drawing.Color.Transparent;
            this.lbCBM12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCBM12.Location = new System.Drawing.Point(48, 312);
            this.lbCBM12.Name = "lbCBM12";
            this.lbCBM12.Size = new System.Drawing.Size(63, 13);
            this.lbCBM12.TabIndex = 17;
            this.lbCBM12.Text = "motive to all";
            // 
            // lbNrGroups
            // 
            this.lbNrGroups.AutoSize = true;
            this.lbNrGroups.BackColor = System.Drawing.Color.Transparent;
            this.lbNrGroups.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbNrGroups.Location = new System.Drawing.Point(3, 408);
            this.lbNrGroups.Name = "lbNrGroups";
            this.lbNrGroups.Size = new System.Drawing.Size(122, 13);
            this.lbNrGroups.TabIndex = 5;
            this.lbNrGroups.Text = "Groups: 0xDDDDDDDD";
            // 
            // TtabItemMotiveTableUI
            // 
            this.Controls.Add(this.lbNrGroups);
            this.Controls.Add(this.pnCopyButtons);
            this.Controls.Add(this.cbShowAll);
            this.Controls.Add(this.pnAllGroups);
            this.Controls.Add(this.lbMotive0);
            this.Controls.Add(this.lbMotive1);
            this.Controls.Add(this.lbMotive2);
            this.Controls.Add(this.lbMotive3);
            this.Controls.Add(this.lbMotive4);
            this.Controls.Add(this.lbMotive5);
            this.Controls.Add(this.lbMotive6);
            this.Controls.Add(this.lbMotive7);
            this.Controls.Add(this.lbMotive9);
            this.Controls.Add(this.lbMotive11);
            this.Controls.Add(this.lbMotive8);
            this.Controls.Add(this.lbMotive10);
            this.Controls.Add(this.lbMotive14);
            this.Controls.Add(this.lbMotive15);
            this.Controls.Add(this.lbMotive13);
            this.Controls.Add(this.lbMotive12);
            this.Name = "TtabItemMotiveTableUI";
            this.Size = new System.Drawing.Size(478, 456);
            this.pnCopyButtons.ResumeLayout(false);
            this.pnCopyButtons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void copy_Click(object sender, System.EventArgs e)
		{
			ArrayList alBtnCopy = new ArrayList(aButtons);
			int bn = alBtnCopy.IndexOf(sender);
			if (bn >= 0)
				doCopyMotive(bn);
			else
                for (int i = 0; i < aButtons.Length; i++)
					doCopyMotive(i);
        }

		private void cbShowAll_CheckedChanged(object sender, System.EventArgs e)
		{
            pnAllGroups.Visible = cbShowAll.Enabled && cbShowAll.Checked;
            pnCopyButtons.Visible = cbShowAll.Enabled && !cbShowAll.Checked;
        }
	}
}
