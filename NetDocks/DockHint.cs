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
using System.Drawing;
using System.Windows.Forms;

namespace Ambertation.Windows.Forms
{
   public class DockHint : ManagedLayeredForm
   {        


        internal DockHint(DockManager manager)
            : this(manager, true, true, true, true, true)
        {
        }

       internal DockHint(DockManager manager, bool l, bool t, bool r, bool b)
            : this(manager, l, t, r, b, true)
        {
        }

       internal DockHint(DockManager manager, bool l, bool t, bool r, bool b, bool c)
            //: base(Color.FromArgb(128, SystemColors.MenuHighlight), new Size(88, 88))
            : base(manager)
        {
            this.Size = Manager.Renderer.DockRenderer.HintSize;
            Manager.Renderer.DockRenderer.InitHints(l, t, r, b, c);


            parent = null;

            center = c;
            left = l;
            top = t;
            right = r;
            bottom = b;
            wassel = 0;

            Init(BuildHints(SelectedHint.None));

            this.Hide();
            this.Text = "Dock Hint";
        }

        DockContainer parent;
       [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
       internal DockContainer ParentContainer
        {
            get { return parent; }
            set { parent = value; }
        }

        bool center;
       [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
       internal bool CenterIndicator
        {
            get { return center; }
            set { center = value; }
        }

        bool left;
       [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
       internal bool LeftIndicator
        {
            get { return left; }
            set { left = value; }
        }

        bool right;
       [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
       internal bool RightIndicator
        {
            get { return right; }
            set { right = value; }
        }

        bool top;
       [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
       internal bool TopIndicator
        {
            get { return top; }
            set { top = value; }
        }

        bool bottom;
       [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
       internal bool BottomIndicator
        {
            get { return bottom; }
            set { bottom = value; }
        }

        internal Rectangle Rectangle
        {
            get { return DesktopBounds; }
        }

        SelectedHint wassel;
       internal event System.EventHandler HoverLeft;
       internal event System.EventHandler HoverTop;
       internal event System.EventHandler HoverRight;
       internal event System.EventHandler HoverBottom;
       internal event System.EventHandler HoverCenter;
       internal event System.EventHandler HoverNone;

       internal delegate void HoverEvent(DockHint sender, SelectedHint hint);
       internal event HoverEvent Hover;

        internal override void MouseOver(Point pt, bool hit)
        {
            base.MouseOver(pt, hit);
            UpdateCanvas(pt, hit);
        }

        protected virtual void DoRenderHints(SelectedHint sel)
        {
            Bitmap b = BuildHints(sel);
            SelectBitmap(b);

            wassel = sel;
        }

        private Bitmap BuildHints(SelectedHint sel)
        {
            Bitmap b = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(b);

            Manager.Renderer.DockRenderer.RenderHint(g, LeftIndicator, TopIndicator, RightIndicator, BottomIndicator, CenterIndicator, sel);

            if (sel == SelectedHint.None && HoverNone != null) HoverNone(this, new EventArgs());
            else if (sel == SelectedHint.Left && HoverLeft != null) HoverLeft(this, new EventArgs());
            else if (sel == SelectedHint.Top && HoverTop != null) HoverTop(this, new EventArgs());
            else if (sel == SelectedHint.Right && HoverRight != null) HoverRight(this, new EventArgs());
            else if (sel == SelectedHint.Bottom && HoverBottom != null) HoverBottom(this, new EventArgs());
            else if (sel == SelectedHint.Center && HoverCenter != null) HoverCenter(this, new EventArgs());

            if (Hover != null) Hover(this, sel);

            g.Dispose();
            return b;
        }

        private void UpdateCanvas(Point pt, bool hit)
        {
            if (hit)
            {
                SelectedHint issel = GetSelectedHint(pt);
                if (Visible && issel != wassel)
                    DoRenderHints(issel);
            }
            else if (wassel != SelectedHint.None)
                DoRenderHints(SelectedHint.None);            
        }

        private SelectedHint GetSelectedHint(Point pt)
        {
            SelectedHint issel = SelectedHint.None;
            
            if (this.CenterIndicator && Manager.Renderer.DockRenderer.CenterRectangle.Contains(pt)) 
                issel = SelectedHint.Center;
            else if (this.LeftIndicator && Manager.Renderer.DockRenderer.LeftRectangle.Contains(pt)) 
                issel = SelectedHint.Left;
            else if (this.TopIndicator && Manager.Renderer.DockRenderer.TopRectangle.Contains(pt)) 
                issel = SelectedHint.Top;
            else if (this.RightIndicator && Manager.Renderer.DockRenderer.RightRectangle.Contains(pt)) 
                issel = SelectedHint.Right;
            else if (this.BottomIndicator && Manager.Renderer.DockRenderer.BottomRectangle.Contains(pt)) 
                issel = SelectedHint.Bottom;

            
            return issel;
        }        
    }
}
