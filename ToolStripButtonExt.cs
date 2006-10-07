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
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Ambertation.Windows.Forms
{
    internal class ToolStripButtonExt : ToolStripButton
    {
        ToolStripItem item;
        internal ToolStripItem Item
        {
            get {return item; }
        }
        internal ToolStripButtonExt(ToolStripItem item)
            : base()
        {
            intern = false;
            this.Text = item.Text;
            this.Name = "tsbe_" + item.Name;
            this.Image = item.Image;
            this.ImageScaling = item.ImageScaling;
            this.Overflow = ToolStripItemOverflow.Always;
            this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.item = item;
            this.Visible = true;            
            item.VisibleChanged += new EventHandler(item_VisibleChanged);
            item.AvailableChanged += new EventHandler(item_AvailableChanged);

            UpdateChecked();
        }

        void item_AvailableChanged(object sender, EventArgs e)
        {
            UpdateChecked();   
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            if (!intern)
            {       
                item.Available = this.Checked;
            }
        }

        bool intern;
        void item_VisibleChanged(object sender, EventArgs e)
        {            
            UpdateChecked();            
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            item.Available = !item.Available;            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            UpdateChecked();
        }

        
        private void UpdateChecked()
        {
            intern = true;
            this.Checked = item.Available;
            intern = false;
        }

        
    }
}
