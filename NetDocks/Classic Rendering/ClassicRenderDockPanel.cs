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
using System.Threading;
using System.Drawing;

namespace Ambertation.Windows.Forms
{
    public class ClassicRenderDockPanel : WhidbeyRenderDockPanel
    {
        const int SIZE_DELTA = 10;
        const int SPEED = 1000 / 50;
       

        public ClassicRenderDockPanel(BaseRenderer parent)
            : base(parent)
        {
            dim = new Dimensions(14, 21, 1, 1, 4, 4, 10, 2, 2);                                    
        }


        #region BaseDockPanelRenderer Member
       
        protected override void RenderCaptionButton(DockPanel dp, DockPanelCaptionButton but, string iname, NCPaintEventArgs e)
        {
            if (but.State != CaptionButtonState.Normal)
            {
                Pen p = new Pen(Color.FromArgb(0x70, 0, 0, 0));
                SolidBrush b = new SolidBrush(Color.Transparent);
                if (but.State == CaptionButtonState.Highlight)
                    b = new SolidBrush(Color.FromArgb(0x70, 0xff, 0xff, 0xff));
                else if (but.State == CaptionButtonState.Selected)
                    b = new SolidBrush(Color.FromArgb(0x40, 0xff, 0xff, 0xff));

                e.Graphics.FillRectangle(b, but.Bounds);
                e.Graphics.DrawRectangle(p, but.Bounds);
            }

            DrawButtonImage(e.Graphics, SetupCaptionButtonName(dp, iname), but.Bounds, dp.CaptionState== CaptionState.Focused);
        }                

        protected override void RenderCaptionBackground(CaptionState state, NCPaintEventArgs e, Rectangle caprect)
        {
            Color c1 = Parent.ColorTable.DockCaptionColorTop;
            if (state == CaptionState.Focused)
            {
                c1 = Parent.ColorTable.DockCaptionFocusColorTop;
            }
            
            e.Graphics.FillRectangle(new SolidBrush(c1), caprect);
        }

        
        protected override void RenderButtonBackground(NCPaintEventArgs e, Rectangle barr, Rectangle r, Point pt1, Point pt2, DockPanel dp)
        {
            RenderButtonBarBackground(e, r, dp.BestOrientation);
        }

        public override void RenderButtonBarBackground(NCPaintEventArgs e, Rectangle r, ButtonOrientation orient)
        {
            Color c1 = ColorTable.DockButtonBarBackgroundTop;
            e.Graphics.FillRectangle(new SolidBrush(c1), r);            
        }

        

        protected override void ModifyButtonRectangle(ref Rectangle r, string caption, Image img, ButtonOrientation orient, ButtonState state, bool renderbackgroundbar)
        {
            base.ModifyButtonRectangle(ref r, caption, img, orient, state, renderbackgroundbar);

            if (orient == ButtonOrientation.Bottom)
            {
                r = new Rectangle(r.Left + 1, r.Top, r.Width - 2, r.Height);
            }
            else if (orient == ButtonOrientation.Top)
            {
                r = new Rectangle(r.Left + 1, r.Top , r.Width - 2, r.Height);
            }
            else if (orient == ButtonOrientation.Left)
            {
                r = new Rectangle(r.Left, r.Top + 1, r.Width, r.Height - 2);
            }
            else
            {
                r = new Rectangle(r.Left , r.Top + 1, r.Width, r.Height - 2);
            }
        }
        

        protected override void SetupButtonColors(Rectangle r, Color c, Color ci, Color fontc, ButtonOrientation orient, ButtonState state, out SolidBrush fontbrush, out SolidBrush linebackgroundbrush, out System.Drawing.Brush backgroundbrush, out Pen borderpen, out Pen borderpeninner)
        {
            base.SetupButtonColors(r, c, ci, fontc, orient, state, out fontbrush, out linebackgroundbrush, out backgroundbrush, out borderpen, out borderpeninner);
                    
            if (state == ButtonState.Normal)
                backgroundbrush = new SolidBrush(ColorTable.DockButtonBackgroundTop);
            else
                backgroundbrush = new SolidBrush(ColorTable.DockButtonHighlightBackgroundTop);
        }
        

        protected override System.Drawing.Drawing2D.GraphicsPath ButtonFullPath(Rectangle r)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            
            path.AddRectangle( new Rectangle(r.Left, r.Top, r.Width, r.Height));
            return path;
        }

        protected override void FixButtonCorners(Graphics g, Brush bg, Pen p, ref Rectangle linerectangle, ref Point linept1, ref Point linept2, Pen pp)
        {
            
        }

        protected override void RenderButtonIcon(Graphics g, Image img, Rectangle imgrect)
        {
            g.DrawImage(
                    img,
                    imgrect,
                    new Rectangle(0, 0, img.Width, img.Height),
                    GraphicsUnit.Pixel
            );
        }

             
        #endregion

        #region IDockPanelRenderer Member              
        #endregion

        
    }
}
