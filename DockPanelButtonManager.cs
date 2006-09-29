using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Floaters
{
    
    public class DockPanelButtonManager : IEnumerable<DockPanelButtonManager.Place>
    {
        #region Helper class
        public class Place
        {
            
            public Place(DockPanel panel, Rectangle rect)
            {
                this.panel = panel;
                this.rect = rect;

                ////Console.WriteLine("    Creating " + panel.Text + " " + Rectangle);
            }

            DockPanel panel;
            public DockPanel Panel
            {
                get { return panel; }
            }

            Rectangle rect;
            public Rectangle Rectangle
            {
                get { return rect; }
            }
        }
        #endregion

        List<Place> buttons;
        BaseRenderer renderer;
        NCPaintEventArgs eventarg;
        IButtonContainer cnt;
              

        public DockPanelButtonManager(BaseRenderer renderer, NCPaintEventArgs e, IButtonContainer cnt)
        {
            this.renderer = renderer;
            this.cnt = cnt;
            this.eventarg = e;
            this.buttons = InitStructure(renderer, e, cnt);
        }

        protected virtual List<Place> InitStructure(BaseRenderer renderer, NCPaintEventArgs e, IButtonContainer cnt)
        {
            List<Place>  buttons = new List<Place>();

            if (cnt != null)
            {
                DockButtonBar.DockPanelList panels = cnt.GetButtons();
                System.Windows.Forms.Padding pad = renderer.DockPanelRenderer.GetBorderSize(cnt);


                if (cnt.BestOrientation == ButtonOrientation.Bottom || cnt.BestOrientation == ButtonOrientation.Top)
                {
                    int left; int swd;
                    PlanHorizontalButtons(buttons, panels, e, cnt.BestOrientation, pad, out left, out swd);
                    buttons = SetHorizontalButtons(buttons, panels, e, left, swd, pad);
                }
                else
                {
                    int top; int shg;
                    PlanVerticalButtons(buttons, panels, e, cnt.BestOrientation, pad, out top, out shg);
                    buttons = SetVerticalButtons(buttons, panels, e, top, shg, pad);
                }
            }

            return buttons;
        }

        protected bool NeedSeperatorAfterPanel(DockPanel dp)
        {
            return dp.SeperateInDockBar;
        }

        #region Vertical
        private List<Place> SetVerticalButtons(List<Place> buttons, DockButtonBar.DockPanelList panels, NCPaintEventArgs e, int top, int shg, System.Windows.Forms.Padding pad)
        {
            int maxhg = e.WindowRectangle.Height - pad.Bottom;
            ////Console.WriteLine(maxhg + " > " + top + " ? ("+e.WindowRectangle+" "+pad+")");
            if (top > maxhg)
            {
                ////Console.WriteLine("    Fixing height!");
                if (shg * panels.Count > maxhg) shg = maxhg / panels.Count;
                List<Place> buttonsnew = new List<Place>();

                int? ntop = null;
                foreach (Place bp in buttons)
                {
                    Rectangle r;
                    if (ntop == null)
                        ntop = bp.Rectangle.Top;

                    r = new Rectangle(bp.Rectangle.Left, (int)ntop, bp.Rectangle.Width, shg);
                    ntop += shg;
                    buttonsnew.Add(new Place(bp.Panel, r));
                }

                return buttonsnew;
            }
            return buttons;
        }

        private void PlanVerticalButtons(List<Place> buttons, DockButtonBar.DockPanelList panels, NCPaintEventArgs e, ButtonOrientation orient, System.Windows.Forms.Padding pad, out int top, out int shg)
        {
            top = pad.Top;
            shg = int.MaxValue;
            foreach (DockPanel dp in panels)
            {

                Size sz = renderer.DockPanelRenderer.GetButtonSize(dp, orient);
                Rectangle r;
                if (orient == ButtonOrientation.Left)
                    r = new Rectangle(pad.Left, top, sz.Width-pad.Horizontal, sz.Height);
                else
                    r = new Rectangle(e.WindowRectangle.Width - sz.Width, top, sz.Width, sz.Height);

                buttons.Add(new Place(dp, r));

                top += sz.Height;
                if (dp.SeperateInDockBar) top += renderer.DockPanelRenderer.Dimension.DockBarButtonSpacing;
                shg = Math.Min(sz.Height, shg);
            }
        }
        #endregion

        #region Horizontal
        private List<Place> SetHorizontalButtons(List<Place> buttons, DockButtonBar.DockPanelList panels, NCPaintEventArgs e, int left, int swd, System.Windows.Forms.Padding pad)
        {
            int maxwd = e.WindowRectangle.Width - pad.Right;
            ////Console.WriteLine(maxwd + " > " + left + " ?");
            if (left > maxwd)
            {
                ////Console.WriteLine("    Fixing length!");
                if (swd * panels.Count > maxwd) swd = maxwd / panels.Count;
                List<Place> buttonsnew = new List<Place>();
                               
                int? nleft = null;
                foreach (Place bp in buttons)
                {
                    Rectangle r;
                    if (nleft==null)
                        nleft = bp.Rectangle.Left;                    
                    
                    r = new Rectangle((int)nleft, bp.Rectangle.Top, swd, bp.Rectangle.Height);
                    nleft += swd;
                    buttonsnew.Add(new Place(bp.Panel, r));
                }

                return buttonsnew;
            }
            return buttons;
        }

        private void PlanHorizontalButtons(List<Place> buttons, DockButtonBar.DockPanelList panels, NCPaintEventArgs e, ButtonOrientation orient, System.Windows.Forms.Padding pad, out int left, out int swd)
        {
            left = pad.Left;
            swd = int.MaxValue;
            foreach (DockPanel dp in panels)
            {

                Size sz = renderer.DockPanelRenderer.GetButtonSize(dp, orient);
                Rectangle r;
                if (orient == ButtonOrientation.Top)
                    r = new Rectangle(left, pad.Top, sz.Width, sz.Height);
                else
                    r = new Rectangle(left, e.WindowRectangle.Height - sz.Height, sz.Width, sz.Height);

                buttons.Add(new Place(dp, r));

                left += sz.Width;
                if (dp.SeperateInDockBar) left += renderer.DockPanelRenderer.Dimension.DockBarButtonSpacing;
                swd = Math.Min(sz.Width, swd);
            }
        }
        #endregion

        public DockPanel GetHitPanel(Point mouse)
        {
            foreach (DockPanelButtonManager.Place p in this)            
                if (p.Rectangle.Contains(mouse))                
                    return p.Panel;
            return null;
        }

        public void Render()
        {
            foreach (Place p in buttons)
            {
                ButtonState s = ButtonState.Normal;
                if (p.Panel == cnt.Highlight) s = ButtonState.Highlight;
                renderer.DockPanelRenderer.RenderButton(eventarg.Graphics, p.Rectangle, p.Panel.Text, cnt.BestOrientation, s);
            }
        }

        #region IEnumerable<Place> Member

        public IEnumerator<DockPanelButtonManager.Place> GetEnumerator()
        {
            return buttons.GetEnumerator();
        }

        #endregion

        #region IEnumerable Member

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return buttons.GetEnumerator();
        }

        #endregion
    }

    
}
