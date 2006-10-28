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
    public class WhidbeyRenderDockPanel : BaseDockPanelRenderer, IDockPanelRenderer
    {
        const int SIZE_DELTA = 10;
        const int SPEED = 1000 / 50;
        Dimensions dim;
        DockAnimationEventHandler atc;

        public WhidbeyRenderDockPanel(BaseRenderer parent)
            :base(parent)
        {
            dim = new Dimensions(16, 24, 1, 4, 2, 16, 2);
            atc = new DockAnimationEventHandler(InvokedAnimationTimerCallback);
            animtimer = new System.Threading.Timer(new TimerCallback(AnimationTimerCallback), null, Timeout.Infinite, SPEED);
        }

        #region Animation
        System.Threading.Timer animtimer;
        struct AnimationData
        {
            public DockAnimationEventArgs e;
        }
        AnimationData animdata;
        void InvokedAnimationTimerCallback(IDockPanelRenderer sender, DockAnimationEventArgs e)
        {
            
            DockContainer dc = e.Container;
            //Console.WriteLine(dc.Guid.ToString() + ": " + e.DockAlignment + ", " + e.AnimationType);
            if (animdata.e.AnimationType == DockAnimationEventArgs.Type.Collapse)
            {
                if (e.DockAlignment == DockAnimationEventArgs.Alignment.Horizontal)
                {
                    if (dc.Parent == null) DoFinishAnimation(e);
                    else if (dc.Right <= 0) { DoFinishAnimation(e); }
                    else dc.Left -= SIZE_DELTA;
                }
                else if (e.DockAlignment == DockAnimationEventArgs.Alignment.Vertical)
                {
                    if (dc.Parent == null) DoFinishAnimation(e);
                    else if (dc.Left >= dc.Parent.Width) { DoFinishAnimation(e); }
                    else dc.Left += SIZE_DELTA;
                }
                else
                {
                    DoFinishAnimation(e);
                }
            }
            else if (e.AnimationType == DockAnimationEventArgs.Type.Expand)
            {
                if (e.DockAlignment == DockAnimationEventArgs.Alignment.Horizontal)
                {
                    if (dc.Parent == null) DoFinishAnimation(e);
                    else if (dc.Left >= 0) { DoFinishAnimation(e); }
                    else dc.Left = Math.Min(0, dc.Left + SIZE_DELTA);
                }
                else if (e.DockAlignment == DockAnimationEventArgs.Alignment.Vertical)
                {
                    if (dc.Parent == null) DoFinishAnimation(e);
                    else if (dc.Right <= dc.Parent.Width) { DoFinishAnimation(e); }
                    else dc.Left = Math.Max(dc.Parent.Width-dc.Width, dc.Left-SIZE_DELTA);
                }                
                else
                {
                    DoFinishAnimation(e);
                }
            }
            else
            {
                DoFinishAnimation(e);
            }
        }


        void AnimationTimerCallback(Object stateInfo)
        {
            DockContainer dc = animdata.e.Container;

            if (dc.InvokeRequired)
            {
                object[] dum = new object[] { this, animdata.e };
                dc.Invoke(atc, dum);
            }
            else
                atc(this, animdata.e);
            
        }

        void DoFinishAnimation(DockAnimationEventArgs e)
        {
            animtimer.Change(Timeout.Infinite, SPEED);
            if (FinishedAnimation != null) FinishedAnimation(this, e);
            e.Container.ResumeLayout();
            e.Container.RepaintAll();
        }
        #endregion

        #region BaseDockPanelRenderer Member
        protected string SetupCaptionButtonName(DockPanel dp, string name)
        {
            if (dp.CaptionState == CaptionState.Focused) return name;
            return name;
        }

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

            DrawButtonImage(e.Graphics, SetupCaptionButtonName(dp, iname), but.Bounds);
        }        

        protected override void RenderCaptionText(CaptionState state, NCPaintEventArgs e, Rectangle txtrect, string caption)
        {
            Color c = Parent.ColorTable.DockCaptionTextColor;
            if (state == CaptionState.Focused) c = Parent.ColorTable.DockCaptionFocusTextColor;
            e.Graphics.DrawString(
                this.GetFittingString(Parent.FontTable.CaptionFont, caption, ButtonOrientation.Top, txtrect.Size)
                , Parent.FontTable.CaptionFont, new SolidBrush(c), txtrect);
        }

        protected override void RenderCaptionBackground(CaptionState state, NCPaintEventArgs e, Rectangle caprect)
        {
            caprect.Offset(-1, -1);
            caprect.Inflate(1, 1);
            Color c1 = Parent.ColorTable.DockCaptionColorTop;
            Color c2 = Parent.ColorTable.DockCaptionColorBottom;
            if (state == CaptionState.Focused) {
                c1 = Parent.ColorTable.DockCaptionFocusColorTop;
                c2 = Parent.ColorTable.DockCaptionFocusColorBottom;
            }
            System.Drawing.Drawing2D.LinearGradientBrush b = 
                new System.Drawing.Drawing2D.LinearGradientBrush(
                    caprect, 
                    c1, 
                    c2, 
                    System.Drawing.Drawing2D.LinearGradientMode.Vertical
                );

            e.Graphics.FillRectangle(b, caprect);
        }

        protected override void RenderButtonBackground(NCPaintEventArgs e, Rectangle r, Point pt1, Point pt2, SolidBrush brush, Pen pen)
        {
            e.Graphics.FillRectangle(brush, r);
            e.Graphics.DrawLine(pen, pt1, pt2);
        }

        protected System.Drawing.Drawing2D.GraphicsPath ButtonFullPath(Rectangle r)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLine(r.Left + 2, r.Top, r.Right - 2, r.Top);
            path.AddLine(r.Right - 2, r.Top, r.Right, r.Top + 2);
            path.AddLine(r.Right, r.Top + 2, r.Right, r.Bottom - 2);
            path.AddLine(r.Right, r.Bottom - 2, r.Right - 2, r.Bottom );
            path.AddLine(r.Right - 2, r.Bottom , r.Left + 2, r.Bottom );
            path.AddLine(r.Left + 2, r.Bottom , r.Left, r.Bottom - 2);
            path.AddLine(r.Left, r.Bottom - 2, r.Left, r.Top + 2);
            path.CloseFigure();
            return path;
        }

        protected System.Drawing.Drawing2D.GraphicsPath ButtonIndicatorPath(Rectangle r, ButtonOrientation orient)
        {
            return ButtonFullPath(r);
        }


        protected override void RenderButton(System.Drawing.Graphics g, System.Drawing.Rectangle r, string caption, Image img, Color c, Color fontc, Font f, StringFormat sf, ButtonOrientation orient, ButtonState state, bool renderbackgroundbar)
        {
            SolidBrush b; SolidBrush bb; System.Drawing.Drawing2D.LinearGradientBrush bg; Pen p;
            SetupButtonColors(r, c, fontc, orient, state, out b, out bb, out bg, out p);

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

            Pen pp = new Pen(ColorTable.DockButtonHighlightBorderColor);
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
        }     

             
        #endregion

        #region IDockPanelRenderer Member


        public void RenderGrip(DockContainer dc, NCPaintEventArgs e, Rectangle r)
        {
            e.Graphics.FillRectangle(new SolidBrush(ColorTable.DockGripColor), r);
        }

        public void RenderResizePanel(DockContainer dc, RubberBandHelper rbh, PaintEventArgs e)
        {
            Rectangle rect = rbh.ClientRectangle;
            //Console.WriteLine(rect);
            e.Graphics.FillRectangle(new SolidBrush(ColorTable.DockReSizeBackgroundColor), new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1));
            if (rbh.ContainerDock == DockStyle.Right) e.Graphics.FillRectangle(new SolidBrush(ColorTable.DockReSizeGripColor), new Rectangle(rect.Left, rect.Top, 3, rect.Height - 1));
            else if (rbh.ContainerDock == DockStyle.Left) e.Graphics.FillRectangle(new SolidBrush(ColorTable.DockReSizeGripColor), new Rectangle(rect.Width - 4, rect.Top, 3, rect.Height - 1));
            else if (rbh.ContainerDock == DockStyle.Bottom) e.Graphics.FillRectangle(new SolidBrush(ColorTable.DockReSizeGripColor), new Rectangle(rect.Left, rect.Top, rect.Width - 1, 3));
            else if (rbh.ContainerDock == DockStyle.Top) e.Graphics.FillRectangle(new SolidBrush(ColorTable.DockReSizeGripColor), new Rectangle(rect.Left, rect.Height - 4, rect.Width - 1, 3));
        }

        
        public override Dimensions Dimension
        {
            get { return dim; }
        }


        public void RenderBorder(DockPanel dp, NCPaintEventArgs e)
        {
            Pen p = new Pen(Parent.ColorTable.DockBorderColor, Dimension.Border);
            
            Rectangle cl = GetPanelClientRectangle(dp.DockContainer, e, dp.BestOrientation);
            cl = new Rectangle(cl.Left - Dimension.Border, cl.Top - Dimension.Border - Dimension.Caption, cl.Width + 2 * Dimension.Border -1 , cl.Height + 2 * Dimension.Border-1 + Dimension.Caption);
            
            e.Graphics.DrawRectangle(p, cl );
        }

        public void Animate(DockAnimationEventArgs e)
        {            
            animdata.e = e;
            e.Container.SuspendLayout();
            if (e.Container.Dock == DockStyle.Fill) DoFinishAnimation(e);
            else
            {
                animtimer.Change(0, SPEED);
                /*InvokedAnimationTimerCallback(this, e);
                InvokedAnimationTimerCallback(this, e);
                DoFinishAnimation(e);*/
            }
        }

        public event DockAnimationEventHandler FinishedAnimation;

        #endregion

        
    }
}
