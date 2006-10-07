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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Ambertation.Windows.Forms
{
    [ToolboxItem(false)]
    public class RubberBandHelper : Control
    {
        DockContainer dc;
        Dictionary<Control, bool> map;
        DockStyle dock;
        public DockStyle ContainerDock
        {
            get { return dock; }
        }
        internal RubberBandHelper(DockContainer dc)
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.dc = dc;
            map = new Dictionary<Control, bool>();
            foreach (Control c in dc.Controls)
            {
                map[c] = c.Visible;
                c.Visible = false;
            }

            this.Dock = DockStyle.Fill;
            dock = dc.Dock;
            dc.Controls.Add(this);
        }

        internal void Close() {
            dc.Controls.Remove(this);
            foreach (Control c in map.Keys)
            {
                c.Visible = map[c];
                if (c is DockPanel) ((DockPanel)c).NCRefresh();
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            //Console.WriteLine("Moving "+Location);
        }
        

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


            dc.Manager.Renderer.DockPanelRenderer.RenderResizePanel(dc, this, e);
        }

        
    }
}
