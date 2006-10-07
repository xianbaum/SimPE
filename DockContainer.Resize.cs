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
    partial class DockContainer
    {

        protected override CreateParams CreateParams
        {
            get
            {
                return base.CreateParams;
            }
        }
        protected override void OnDockChanged(EventArgs e)
        {
            base.OnDockChanged(e);
            DoDockChanged();
        }

        protected virtual void DoDockChanged()
        {
            if (Manager == null) return;


            this.ResizeBorder.SetAll(false);
            this.NonClientMargin = Manager.Renderer.DockPanelRenderer.GetGripSize(Dock);
            if (Dock == DockStyle.Right) this.ResizeBorder.Left = true;
            else if (Dock == DockStyle.Top) this.ResizeBorder.Bottom = true;
            else if (Dock == DockStyle.Left) this.ResizeBorder.Right = true;
            else if (Dock == DockStyle.Bottom) this.ResizeBorder.Top = true;
        }

        protected override void OnNcPaint(NCPaintEventArgs e)
        {
            base.OnNcPaint(e);
            if (Manager == null) return;
            Manager.Renderer.DockPanelRenderer.RenderGrip(this, e, Manager.Renderer.DockPanelRenderer.GetGripRectangle(e, Dock));
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        


        RubberBandHelper rbh;
        int index;
        protected virtual bool MyWndProc(ref Message m)
        {
            if (m.Msg == APIHelp.WM_ENTERSIZEMOVE)
            {
                if (Manager != null) Manager.SuspendLayout();
                if (Parent != null) index = Parent.Controls.GetChildIndex(this);             
                rbh = new RubberBandHelper(this);
                Rectangle rect = this.Bounds;
                
                /*this.SetBounds(rect.Left, rect.Top, rect.Width, rect.Height);
                this.BringToFront();                */
                if (Manager != null) Manager.ResumeLayout();
            }
            if (m.Msg == APIHelp.WM_EXITSIZEMOVE)
            {
                if (Manager != null) Manager.SuspendLayout();
                if (rbh != null) Dock = rbh.ContainerDock;
                if (Parent != null) Parent.Controls.SetChildIndex(this, index);
                if (rbh != null) rbh.Close();
                rbh = null;
                if (Manager != null) Manager.ResumeLayout();
            }
            return true;
        }
    }
}
