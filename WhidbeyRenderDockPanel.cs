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

namespace Floaters
{
    public class WhidbeyRenderDockPanel : BaseDockPanelRenderer, IDockPanelRenderer
    {
        const int SIZE_DELTA = 40;
        const int SPEED = 1000 / 50;
        Dimensions dim;
        DockAnimationEventHandler atc;

        public WhidbeyRenderDockPanel(BaseRenderer parent)
            :base(parent)
        {
            dim = new Dimensions(16, 32, 1, 4);
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
                    if (dc.Height < dc.MinimumDockSize + SIZE_DELTA) { DoFinishAnimation(e); }
                    else dc.Height -= SIZE_DELTA;
                }
                else if (e.DockAlignment == DockAnimationEventArgs.Alignment.Vertical)
                {
                    if (dc.Width < dc.MinimumDockSize + SIZE_DELTA) { DoFinishAnimation(e); }
                    else dc.Width -= SIZE_DELTA;
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
                    if (dc.Height >= dc.ExpandedSize.Height) { DoFinishAnimation(e); }
                    else dc.Height = Math.Min(dc.Height + SIZE_DELTA, dc.ExpandedSize.Height);
                }
                else if (e.DockAlignment == DockAnimationEventArgs.Alignment.Vertical)
                {
                    if (dc.Width >= dc.ExpandedSize.Width) { DoFinishAnimation(e); }
                    else dc.Width = Math.Min(dc.Width + SIZE_DELTA, dc.ExpandedSize.Width);
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
        }
        #endregion

        #region BaseDockPanelRenderer Member
        protected string SetupCaptionButtonName(DockPanel dp, string name)
        {
            if (dp.CaptionState == CaptionState.Focused) return name + "_f";
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
            e.Graphics.DrawString(caption, Parent.FontTable.CaptionFont, new SolidBrush(c), txtrect);
        }

        protected override void RenderCaptionBackground(CaptionState state, NCPaintEventArgs e, Rectangle caprect)
        {
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


        protected override void RenderButton(System.Drawing.Graphics g, System.Drawing.Rectangle r, string caption, Color c, Font f, StringFormat sf)
        {
            SolidBrush b = new SolidBrush(c);
            Pen p = new Pen(b);
            g.DrawRectangle(p, r);

            g.DrawString(caption, f, b, new PointF(r.Left, r.Top), sf);
        }       
        #endregion

        #region IDockPanelRenderer Member

        public override Dimensions Dimension
        {
            get { return dim; }
        }


        public void RenderBorder(DockPanel dp, NCPaintEventArgs e)
        {
            Pen p = new Pen(Parent.ColorTable.DockBorderColor, Dimension.Border);
            Padding pad = GetPanelBorderSize(dp.BestOrientation);
            e.Graphics.DrawRectangle(p,
                new Rectangle(
                    pad.Left - Dimension.Border,
                    pad.Top - Dimension.Border - Dimension.Caption,
                    e.WindowRectangle.Width - pad.Horizontal + 2 * Dimension.Border -1,
                    e.WindowRectangle.Height - pad.Vertical + 2 * Dimension.Border + Dimension.Caption-1
                )
            );
        }

        public void Animate(DockAnimationEventArgs e)
        {            
            animdata.e = e;
            animtimer.Change(0, SPEED);
        }

        public event DockAnimationEventHandler FinishedAnimation;

        #endregion

        
    }
}
