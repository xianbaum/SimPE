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
    [Designer(typeof(DockManagerDesigner)), ToolboxItem(true), ToolboxBitmap(typeof(DockManager), "Floaters.dockimg.png")]
    public partial class DockManager : DockContainer
    {
        List<ManagedLayeredForm> layers;
        List<DockHint> hints;
        DockButtonBar.DockPanelList floatingpanels;

        bool dockmode, didinit;
        ContainerInfo last;

        DockHint allleft, allright, alltop, allbottom, allcenter;
        DockOverlay overlay;

        Dictionary<DockStyle, DockButtonBar> colconts;


        public bool DockMode
        {
            get { return dockmode; }
        }

        System.IO.TextWriter writer;
        ~DockManager()
        {
            //writer.Close();
        }
        public DockManager()
            : base()
        {
            /*string flname = System.Windows.Forms.Application.StartupPath;
            flname = System.IO.Path.Combine(flname, "netdocks.log");
            writer = new System.IO.StreamWriter(flname, false);
            //Console.SetOut(writer);*/

            didinit = false;
            last = new ContainerInfo();
            manager = this;

            renderer = new WhidbeyRenderer();
            defsz = new Size(100, 100);

            layers = new List<ManagedLayeredForm>();
            hints = new List<DockHint>();

            overlay = new DockOverlay(this);
            overlay.Hide();

            allcenter = new DockHint(this); allcenter.Text = "Center hint";
            allleft = new DockHint(this, true, false, false, false, false); hints.Add(allleft); allleft.Text = "Left hint";
            alltop = new DockHint(this, false, true, false, false, false); hints.Add(alltop); alltop.Text = "Top hint";
            allright = new DockHint(this, false, false, true, false, false); hints.Add(allright); allright.Text = "Right hint";
            allbottom = new DockHint(this, false, false, false, true, false); hints.Add(allbottom); allbottom.Text = "Bottom hint";
            hints.Add(allcenter);

            ChangeHostControl();

            layers.Add(overlay);
            foreach (DockHint hint in hints)
            {
                layers.Add(hint);
                hint.Hover += new DockHint.HoverEvent(MouseOverHint);
            }

            dockmode = false;

            colconts = new Dictionary<DockStyle, DockButtonBar>();
            BuildSpecialContainer(DockStyle.Left);
            BuildSpecialContainer(DockStyle.Top);
            BuildSpecialContainer(DockStyle.Right);
            BuildSpecialContainer(DockStyle.Bottom);

            floatingpanels = new DockButtonBar.DockPanelList();
        }

        protected override void DoDockChanged()
        {
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible && !didinit)
            {
                didinit = false;
            }
        }

        void BuildSpecialContainer(DockStyle d)
        {
            colconts[d] = new DockButtonBar(this);
            colconts[d].BackColor = SystemColors.Control;
            colconts[d].Width = 20;
            colconts[d].Height = 20;
            colconts[d].Dock = d;
            colconts[d].Parent = this;
            colconts[d].Visible = false;            
        }

        public void ForceCleanUp()
        {
            CleanUp();
            RepaintAll();
        }

        
        protected override void CleanUp()
        {        
            //Console.WriteLine("DO CleanUp");
            base.CleanUp();
            //RearrangeControls();

            ListControls();
        }

        protected override void RearrangeControls()
        {
            base.RearrangeControls();
            if (colconts.Count >= 4)
            {
                if (Controls.GetChildIndex(colconts[DockStyle.Bottom]) > 3 ||
                    Controls.GetChildIndex(colconts[DockStyle.Right]) > 3 ||
                    Controls.GetChildIndex(colconts[DockStyle.Top]) > 3 ||
                    Controls.GetChildIndex(colconts[DockStyle.Left]) > 3)
                {
                    colconts[DockStyle.Bottom].SendToBack();
                    colconts[DockStyle.Right].SendToBack();
                    colconts[DockStyle.Top].SendToBack();
                    colconts[DockStyle.Left].SendToBack();
                }
            }
        }

        protected override void SetNewContainerIndex(ref int index, ref bool after, ref bool toplevel, DockStyle dockstyle)
        {
            base.SetNewContainerIndex(ref index, ref after, ref toplevel, dockstyle);

            if (index == -1)
            {
                index = 0;
                toplevel = false;
            }

        }

        protected void ShowOverlay(Rectangle rect)
        {
            overlay.Location = rect.Location;
            overlay.Size = rect.Size;

            overlay.Show();
            foreach (DockHint dh in hints)
                if (dh.Visible) dh.BringToFront();
        }

        protected void HideOverlay()
        {
            overlay.Hide();
        }

        Point HostToScreen(int x, int y)
        {
            Point pt = new Point(x, y);
            return HostToScreen(pt);
        }

        Point HostToScreen(Point pt)
        {
            return this.PointToScreen(pt);
        }

        protected override void SetTargetContainerInfo(DockHint sender, ref ContainerInfo nfo)
        {
            nfo.DockInside = false;
            nfo.Parent = this;

            nfo.TopLevel = sender != allcenter;
        }

        protected override void OnMouseOverHint(DockHint sender, ref ContainerInfo nfo)
        {
            base.OnMouseOverHint(sender, ref nfo);
            //Console.WriteLine("update last from "+sender.Text+" with "+nfo);


            last = nfo;
            if (nfo.Hint == SelectedHint.None)
            {
                HideOverlay();
                return;
            }

            ShowOverlay(nfo.OverlayRectangle);
        }




        Size defsz;
        public new Size DefaultSize
        {
            get { return defsz; }
            set { defsz = value; }
        }



        protected void ChangeHostControl()
        {
            TakeHint(allcenter);

            SetMainHintLocation();
        }

        private void SetMainHintLocation()
        {
            const int dist = 8;
            Rectangle r = ScreenBounds;
            /*if (Parent!=null)
                if (Parent.Parent!=null)
                    r = new Rectangle(Parent.Parent.PointToScreen(Location), this.Size);*/
            //Console.WriteLine(r + " " + Location + " " + Parent);

            if (allleft == null || allright == null || alltop == null || allbottom == null) return;

            allleft.SetDesktopLocation(
                r.Left + dist,
                r.Top + (r.Height - allleft.Height) / 2
                );

            alltop.SetDesktopLocation(
                r.Left + (r.Width - alltop.Width) / 2,
                r.Top + dist
                );

            allright.SetDesktopLocation(
                r.Left + r.Width - dist - allright.Width,
                r.Top + (r.Height - allleft.Height) / 2
                );

            allbottom.SetDesktopLocation(
                r.Left + (r.Width - allbottom.Width) / 2,
                r.Top + r.Height - dist - allbottom.Height
                );
        }



        /*protected Rectangle GetHostRectangle()
        {
            int hg = Screen.PrimaryScreen.WorkingArea.Height;
            int wd = Screen.PrimaryScreen.WorkingArea.Width;
            int l = 0;
            int t = 0;

            if (host != null)
            {
                hg = host.Height;
                wd = host.Width;
                if (host.Parent != null)
                {
                    l = host.Parent.PointToScreen(new Point(host.Left, host.Top)).X;
                    t = host.Parent.PointToScreen(new Point(host.Left, host.Top)).Y;
                }
                else
                {
                    l = host.Left;
                    t = host.Top;
                }
            }

            return new Rectangle(l, t, wd, hg);
        }*/

        internal override void TakeHint(DockHint hint)
        {
            TakeHint(hint, ScreenBounds, null);
        }

        protected override void OnTakeHint()
        {


        }

        BaseRenderer renderer;
        public BaseRenderer Renderer
        {
            get { return renderer; }
            set
            {
                if (renderer != value)
                {
                    renderer = value;
                    this.Refresh();
                }
            }
        }

        public new void Refresh()
        {

        }


        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ChangeHostControl();
        }

        Control oldparent;
        protected override void OnParentChanged(EventArgs e)
        {
    
            if (oldparent != null)
            {
                oldparent.LocationChanged -= new EventHandler(oldparent_LocationChanged);
                oldparent.SizeChanged -= new EventHandler(oldparent_LocationChanged);
                oldparent.Layout -= new LayoutEventHandler(oldparent_Layout);

            }
            base.OnParentChanged(e);
            oldparent = Parent;
            if (oldparent != null)
            {
                oldparent.LocationChanged += new EventHandler(oldparent_LocationChanged);
                oldparent.SizeChanged += new EventHandler(oldparent_LocationChanged);
                oldparent.Layout += new LayoutEventHandler(oldparent_Layout);
            }
            

            //Console.WriteLine("new Parent");
            ChangeHostControl();
        }

        



        void oldparent_Layout(object sender, LayoutEventArgs e)
        {
            //Console.WriteLine("Parent layout");
            ChangeHostControl();
        }

        protected void oldparent_LocationChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("Parent moved/sized");
            ChangeHostControl();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            ChangeHostControl();
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            /*CleanUp();
            RefreshSplitters();*/
            RepaintAll();
        }


        protected override void UpdateHintVisibility()
        {
            base.UpdateHintVisibility();

            foreach (DockHint hint in hints)
            {
                //Console.WriteLine("    Updating hint " + hint.Text + " from " + hint.Visible);
                if (hint == allcenter) hint.Show();
                else if (!hint.Rectangle.IntersectsWith(allcenter.Rectangle)) hint.Show();
                else hint.Hide();
                //Console.WriteLine("      to " + hint.Visible);
            }
        }

        internal void StartDockMode(DockPanel dock)
        {
            //Console.WriteLine("#### StartDockMode for " + dock.Text + " (" + dockmode + ") from " + dock.Parent);
            if (!dockmode)
            {
                Console.WriteLine(" -> started");
                this.SetMainHintLocation(); //this seems to be needed to ensure working hints with Win2K
                this.SuspendLayout();
                SetMainHintLocation();
                last = new ContainerInfo();
                dockmode = true;
                TakeHint(allcenter);
                UpdateHintVisibility();
                CleanUp();
                this.ResumeLayout();

                OnStartDockMode(dock);

            }
        }


        protected virtual void OnStartDockMode(DockPanel dock)
        {

        }

        internal void StopDockMode(DockPanel dock)
        {

            //Console.WriteLine("#### StopDockMode for " + dock.Text + " (" + dockmode + ")");
            if (dockmode)
            {
                //Console.WriteLine(" -> stopping to "+last);
                this.SuspendLayout();
                dock.SuspendLayout();
                dock.Visible = false;
                dockmode = false;

                foreach (LayeredForm l in layers)
                    l.Hide();


                if (last.Hint != SelectedHint.None)
                {
                    DockContainer dcmain = null;
                    if (last.Parent != null)
                    {
                        //Console.WriteLine(last);
                        dcmain = last.Seed;
                        DockContainer dc = dcmain;
                        dcmain.SuspendLayout();
                        if (last.Hint != SelectedHint.Center)
                        {

                            DockContainer dcnew;
                            if (dock.FloatContainer)
                            {
                                dcnew = dock.DockContainer;
                                dc = dcnew;
                                dcnew.SuspendLayout();
                                if (dcnew.Parent as DockContainer != last.Parent)
                                {
                                    dcnew.Parent = last.Parent;
                                    last.Parent.SetupContainer(last.SeedIndex, !last.DockInside, last.TopLevel, last.Dock, dcnew);                                    
                                }                                                                
                            }
                            else
                            {
                                dcnew = last.Parent.CreateNewContainer(last.SeedIndex, !last.DockInside, last.TopLevel, last.Dock);
                                dc = dcnew;
                                dcnew.SuspendLayout();                                
                            }

                            if (!(last.Parent is DockManager))
                            {
                                dc.Width = Math.Max(20, Math.Min(DefaultSize.Width, last.Parent.Width / 2));
                                dc.Height = Math.Max(20, Math.Min(DefaultSize.Height, last.Parent.Height / 2));
                            }
                            else
                            {
                                dc.Width = Math.Min(last.Parent.Width / 2, dock.Width);
                                dc.Height = Math.Min(last.Parent.Height / 2, dock.Height);
                            }

                            dcnew.Visible = true;
                            dcnew.ResumeLayout();
                        }
                        else if (dock.FloatContainer)
                        {
                            DockContainer olddc = dock.DockContainer;
                            int ct = olddc.GetDockedPanels().Count;
                            for (int i=ct-1; i>=0; i--){
                                DockPanel dp = olddc.GetDockedPanels()[i];
                                dp.DockControl(dc);
                                dp.RefreshMargin();
                            }
                        }

                        if (dock.DockContainer != dc)                        
                            dock.DockControl(dc);

                        dock.EnsureVisible();
                        dock.RefreshAll();

                    }
                    CleanUp();
                    if (dcmain != null) dcmain.ResumeLayout();
                }

                OnStopDockMode(dock);


                this.ResumeLayout();
                dock.Visible = true;
                dock.ResumeLayout();
                dock.NCRefresh();
            }

        }

        protected virtual void OnStopDockMode(DockPanel dock)
        {

        }

        protected override DockContainer GetDockContainer(Point scrpt)
        {
            foreach (ManagedLayeredForm l in layers)
            {
                bool hit = l.Hit(scrpt);
                l.MouseOver(l.PointToClient(scrpt), hit);
            }

            return base.GetDockContainer(scrpt);
        }

        internal void MouseMoved(Point scrpt)
        {
            DockContainer dc = GetDockContainer(scrpt);
            if (dc != null)
                TakeHint(allcenter, dc.ScreenBounds, dc);

            else TakeHint(allcenter);

        }

        public DockButtonBar GetButtonBar(DockContainer dc)
        {
            foreach (DockButtonBar bar in colconts.Values)
            {
                if (bar.Contains(dc)) return bar;
            }
            return null;
        }

        public DockButtonBar GetBestButtonBar(DockContainer dc)
        {
            Control c = dc;
            Control p = c.Parent;
            while (p != null && !(p is DockManager))
            {
                c = p;
                p = c.Parent;
            }

            if (p == null) return colconts[DockStyle.Right];
            if (c.Dock == DockStyle.Fill || c.Dock == DockStyle.None) return colconts[DockStyle.Right];
            return colconts[c.Dock];
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode) CleanUp();
        }


        public void DockPanel(DockPanel dp, DockStyle style)
        {
            bool docked = false;
            this.SuspendLayout();
            foreach (DockContainer dc in containers)
                if (dc.Dock == style)
                {
                    docked = true;
                    dp.DockControl(dc);
                    break;

                }

            if (!docked)
            {

                DockContainer dc = CreateNewContainer(-1, false, true, style);
                dc.SetNoCleanUpIntern(true);
                dc.Visible = true;
                dc.Width = Math.Max(dc.Width, dp.Width);
                dc.Height = Math.Max(dc.Height, dp.Height);

                dp.DockControl(dc);
                dc.SetNoCleanUpIntern(false);
            }
            this.ResumeLayout();
        }

        internal void NotifyFloating(DockPanel dp)
        {
            if (dp.Floating && !floatingpanels.Contains(dp)) floatingpanels.Add(dp);
            else if (!dp.Floating && floatingpanels.Contains(dp)) floatingpanels.Remove(dp);
        }
    }
}
