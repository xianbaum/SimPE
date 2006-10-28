/***************************************************************************
 *   Copyright (C) 2006 by Peter L Jones                                   *
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SimPe.PackedFiles.Wrapper;
using SimPe.PackedFiles.UserInterface;

namespace pjse
{
    public partial class TtabAnimalMotiveWiz : Form
    {
        public TtabAnimalMotiveWiz()
        {
            InitializeComponent();

            this.Controls.Remove(this.label1);
            this.Controls.Remove(this.ttabSingleMotiveUI1);
        }

        private TtabItemAnimalMotiveItem item = null;

        public TtabItemAnimalMotiveItem MotiveSet
        {
            get { return item; }
            set
            {
                if (item != value)
                {
                    item = value;
                    setItem();
                }
            }
        }

        private int lastTop
        {
            get
            {
                return (pairs.Count > 0)
                    ? ((ctrlPair)pairs[pairs.Count - 1]).s.Top
                    : 20;
            }
        }

        private int width
        {
            get
            {
                if (pairs.Count > 0)
                {
                    ctrlPair c = (ctrlPair)pairs[0];
                    return 20 + c.l.Width + 4 + c.s.Width + 20;
                }
                else
                    return 20 + this.btnCancel.Width + 20 + this.btnOkay.Width + 20;
            }
        }

        private class ctrlPair
        {
            public TtabSingleMotiveUI s;
            public Label l;
            public ctrlPair(TtabSingleMotiveUI s, Label l) { this.s = s; this.l = l; }
        }

        private ArrayList pairs = null;

        private void setItem()
        {
            this.SuspendLayout();

            foreach (Control c in this.Controls)
                if (c is TtabSingleMotiveUI )
                    this.Controls.Remove(c);

            pairs = new ArrayList();
            for (int i = 0; i < item.Count; i++)
                addItem(item[i], i);

            this.Size = new Size(width, lastTop + 20 + this.btnOkay.Height + 20);

            this.btnMinus.Enabled = (pairs.Count > 0);

            this.ResumeLayout();
        }

        private void addItem(TtabItemSingleMotiveItem item, int i)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TtabAnimalMotiveWiz));

            TtabSingleMotiveUI s = new TtabSingleMotiveUI();
            resources.ApplyResources(s, "ttabSingleMotiveUI1");
            s.Motive = item;
            s.Location = new Point(this.Right - s.Width - 20, lastTop);
            this.Controls.Add(s);

            Label l = new Label();
            resources.ApplyResources(l, "label1");
            l.Text = "0x" + SimPe.Helper.HexString((short)i);
            l.Location = new Point(s.Left - l.Width - 4, s.Top + 2);
            this.Controls.Add(l);

            pairs.Add(new ctrlPair(s, l));
        }

        private void delItem()
        {
            ctrlPair c = (ctrlPair)pairs[pairs.Count - 1];
            this.Controls.Remove(c.s);
            this.Controls.Remove(c.l);
            pairs.RemoveAt(pairs.Count - 1);
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            TtabItemSingleMotiveItem i = new TtabItemSingleMotiveItem(item.Parent);
            addItem(i, item.Add(i));
            this.Size = new Size(width, lastTop + 20 + this.btnOkay.Height + 20);
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            delItem();
            this.Size = new Size(width, lastTop + 20 + this.btnOkay.Height + 20);

            this.btnMinus.Enabled = (pairs.Count > 0);
        }

    }
}