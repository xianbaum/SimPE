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
    public class GlossyRenderDockPanel : WhidbeyRenderDockPanel
    {
        const int SIZE_DELTA = 10;
        const int SPEED = 1000 / 50;
        System.Drawing.Drawing2D.ColorBlend butbarbgblend, butbgblendhl, butbgblend;

        public GlossyRenderDockPanel(BaseRenderer parent)
            : base(parent)
        {
            dim = new Dimensions(16, 27, 1, 4, 6, 3, 16, 2, 2);
            
            
            butbarbgblend = new System.Drawing.Drawing2D.ColorBlend();
            butbarbgblend.Colors = new Color[] {ColorTable.DockButtonBarBackgroundTop,
                           ColorTable.DockButtonBarBackgroundBottom,
                           ColorTable.DockButtonBarBackgroundBottom,
                           ColorTable.DockButtonBarBackgroundTop
            };
            butbarbgblend.Positions = new float[] { 0, 0.60f, 0.70f, 1 };

            butbgblendhl = new System.Drawing.Drawing2D.ColorBlend();
            butbgblendhl.Colors = new Color[] {
                           Parent.Interpolate(ColorTable.DockButtonBackgroundTop, Color.Black, 0.02f),
                           ColorTable.DockButtonBackgroundTop,
                           ColorTable.DockButtonBackgroundBottom,
                           Parent.Interpolate(ColorTable.DockButtonBackgroundBottom, Color.White, 0.2f)
                           
            };
            butbgblendhl.Positions = new float[] { 0, 0.4f, 0.405f, 1 };

            butbgblend = new System.Drawing.Drawing2D.ColorBlend();
            butbgblend.Colors = new Color[] {
                           Parent.Interpolate(ColorTable.DockButtonHighlightBackgroundTop, Color.White, 0.1f),
                           ColorTable.DockButtonHighlightBackgroundTop,
                           ColorTable.DockButtonHighlightBackgroundBottom,
                           Parent.Interpolate(ColorTable.DockButtonHighlightBackgroundBottom, Color.White, 0.1f)
                           
            };
            butbgblend.Positions = new float[] { 0, 0.4f, 0.405f, 1 };
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
            base.RenderCaptionBackground(state, e, caprect);
            if (state == CaptionState.Normal)
            {
                Rectangle c = caprect;
                
                Pen p = new Pen(Color.FromArgb(0x40, Parent.Interpolate(Parent.ColorTable.DockBorderColor, Color.Black, 0.2f)));
                e.Graphics.DrawLine(p, c.Left, c.Top, c.Right, c.Top);
                e.Graphics.DrawLine(p, c.Left, c.Top, c.Left, c.Bottom-1);
            }
        }

        
        protected override void RenderButtonBackground(NCPaintEventArgs e, Rectangle barr, Rectangle r, Point pt1, Point pt2, DockPanel dp)
        {
            RenderButtonBarBackground(e, r, dp.BestOrientation);
        }

        public override void RenderButtonBarBackground(NCPaintEventArgs e, Rectangle r, ButtonOrientation orient)
        {
            System.Drawing.Drawing2D.LinearGradientMode mode = GetGradientMode(orient);

            Color c1 = ColorTable.DockButtonBarBackgroundTop;
            Color c2 = ColorTable.DockButtonBarBackgroundBottom;

            System.Drawing.Drawing2D.LinearGradientBrush backgroundbrush = new System.Drawing.Drawing2D.LinearGradientBrush(r, c1, c2, mode);
            backgroundbrush.InterpolationColors = butbarbgblend;
            e.Graphics.FillRectangle(backgroundbrush, r);
            
        }

        

        protected override void ModifyButtonRectangle(ref Rectangle r, string caption, Image img, ButtonOrientation orient, ButtonState state, bool renderbackgroundbar)
        {
            base.ModifyButtonRectangle(ref r, caption, img, orient, state, renderbackgroundbar);

            if (orient == ButtonOrientation.Bottom)
            {
                r = new Rectangle(r.Left + 1, r.Top, r.Width - 1, r.Height - 3);
            }
            else if (orient == ButtonOrientation.Top)
            {
                r = new Rectangle(r.Left + 1, r.Top + 3, r.Width - 1, r.Height - 3);
            }
            else if (orient == ButtonOrientation.Left)
            {
                r = new Rectangle(r.Left +3, r.Top + 1, r.Width - 3, r.Height - 1);
            }
            else
            {
                r = new Rectangle(r.Left , r.Top + 1, r.Width - 3, r.Height - 1);
            }
        }
        

        protected override void SetupButtonColors(Rectangle r, Color c, Color ci, Color fontc, ButtonOrientation orient, ButtonState state, out SolidBrush fontbrush, out SolidBrush linebackgroundbrush, out System.Drawing.Brush backgroundbrush, out Pen borderpen, out Pen borderpeninner)
        {
            base.SetupButtonColors(r, c, ci, fontc, orient, state, out fontbrush, out linebackgroundbrush, out backgroundbrush, out borderpen, out borderpeninner);
        
            System.Drawing.Drawing2D.LinearGradientMode mode = GetGradientMode(orient);
            System.Drawing.Drawing2D.LinearGradientBrush bg = new System.Drawing.Drawing2D.LinearGradientBrush(r, ColorTable.DockButtonBackgroundTop, ColorTable.DockButtonBackgroundBottom, mode);
            
            if (state == ButtonState.Highlight) bg.InterpolationColors = butbgblendhl;
            else bg.InterpolationColors = butbgblend; 

            backgroundbrush = bg;
        }

        protected override void RenderInnerButtonBorder(Graphics g, Rectangle r, Pen pi, ButtonOrientation orient, ButtonState state, bool renderbackgroundbar)
        {
            Rectangle rs = r;
            rs.Inflate(-2, -2);
            rs.Offset(1, 1);
            System.Drawing.Drawing2D.GraphicsPath pathbgi;
            if (state == ButtonState.Normal) pathbgi = ButtonFullPath(rs);
            else pathbgi = ButtonIndicatorPath(rs, orient);
            g.DrawPath(new Pen(pi.Brush, 2), pathbgi);
        }


        /*protected override void RenderButton(System.Drawing.Graphics g, System.Drawing.Rectangle r, string caption, Image img, Color c, Color ci, Color fontc, Font f, StringFormat sf, ButtonOrientation orient, ButtonState state, bool renderbackgroundbar)
        {
            SolidBrush b; SolidBrush bb; System.Drawing.Brush bg; Pen p; Pen pi;
            SetupButtonColors(r, c, ci, fontc, orient, state, out b, out bb, out bg, out p, out pi);

            Rectangle linerectangle; Point linept1, linept2; Rectangle textrect; Rectangle imgrect;
            r = SetupButtonRectangles(r, f, orient, out linerectangle, out linept1, out linept2, out textrect, out imgrect);

            System.Drawing.Drawing2D.GraphicsPath path;
            if (state == ButtonState.Normal) path = ButtonFullPath(r);
            else path = ButtonIndicatorPath(r, orient);

            StringFormat sfreal = new StringFormat(sf.FormatFlags | StringFormatFlags.NoWrap);
            System.Drawing.Drawing2D.GraphicsPath pathbg = path.Clone() as System.Drawing.Drawing2D.GraphicsPath;
            pathbg.CloseFigure();
           
            g.FillPath(bg, pathbg);
            g.DrawImage(
                    img,
                    new Rectangle(
                        (imgrect.Width - img.Width) / 2 + imgrect.Left + 1,
                        (imgrect.Height - img.Height) / 2 + imgrect.Top + 1,
                        img.Width,
                        img.Height),
                    new Rectangle(0, 0, img.Width, img.Height),
                    GraphicsUnit.Pixel
            );

            

            g.DrawString(this.GetFittingString(f, caption, orient, new Size(textrect.Width, textrect.Height)), f, b, textrect, sfreal);
            g.DrawPath(p, path);
            


            Pen pp = new Pen(ColorTable.DockButtonHighlightBorderColorOuter);
            if (renderbackgroundbar)
            {
                g.FillRectangle(bb, linerectangle);
                if (state != ButtonState.Highlight) g.DrawLine(pp, linept1, linept2);
            }
            else
            {
                g.FillRectangle(bg, linerectangle);
                g.DrawRectangle(p, linerectangle);
                g.DrawLine(new Pen(bg), linept1, linept2);
                g.DrawLine(p, linept1, linept1);
                g.DrawLine(p, linept2, linept2);
            }           
        }     */

             
        #endregion

        #region IDockPanelRenderer Member              
        #endregion

        
    }
}
