using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Floaters
{
    public class DockManager : DockContainer
    {        
        List<ManagedLayeredForm> layers;
        List<DockHint> hints;
        bool dockmode;
        ContainerInfo last;

        DockHint allleft, allright, alltop, allbottom, allcenter;
        DockOverlay overlay;

        Dictionary<DockStyle, DockButtonBar> colconts;

        
        public bool DockMode
        {
            get { return dockmode; }            
        }                       

        public DockManager() 
            : base()
        {
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

        protected override void CleanUp()
        {
            base.CleanUp();

            colconts[DockStyle.Bottom].SendToBack();
            colconts[DockStyle.Right].SendToBack();
            colconts[DockStyle.Top].SendToBack();
            colconts[DockStyle.Left].SendToBack();

            ListControls();
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
            base.OnMouseOverHint(sender,ref nfo);
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
            const int dist = 8;

            Rectangle r = ScreenBounds;
            ////Console.WriteLine(r);

            TakeHint(allcenter);
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
            set {
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
            }
            base.OnParentChanged(e);
            oldparent = Parent;
            if (oldparent != null)
            {
                oldparent.LocationChanged += new EventHandler(oldparent_LocationChanged);
            }
            ChangeHostControl();
        }

        protected void oldparent_LocationChanged(object sender, EventArgs e)
        {

            ChangeHostControl();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            ChangeHostControl();
        }


        protected override void UpdateHintVisibility()
        {
            base.UpdateHintVisibility();
            foreach (DockHint hint in hints)
            {
                if (hint == allcenter) hint.Show();
                else if (!hint.Rectangle.IntersectsWith(allcenter.Rectangle)) hint.Show();
                else hint.Hide();
            }
        }

        internal void StartDockMode(DockPanel dock)
        {
            if (!dockmode)
            {
                last = new ContainerInfo();
                dockmode = true;
                TakeHint(allcenter);
                UpdateHintVisibility();
                CleanUp();
                                
                OnStartDockMode(dock);
            }
        }
        

        protected virtual void OnStartDockMode(DockPanel dock)
        {
            
        }
       
        internal void StopDockMode(DockPanel dock)
        {
            if (dockmode)
            {
                dockmode = false;                

                foreach (LayeredForm l in layers)
                    l.Hide();


                if (last.Hint != SelectedHint.None)
                {
                    if (last.Parent != null)
                    {
                        //Console.WriteLine(last);
                        DockContainer dc = last.Seed;
                        if (last.Hint != SelectedHint.Center)
                        {
                            dc = last.Parent.CreateNewContainer(last.SeedIndex, !last.DockInside, last.TopLevel, last.Dock);                            
                            dc.Width = Math.Max(20, Math.Min(DefaultSize.Width, last.Parent.Width / 2));
                            dc.Height = Math.Max(20, Math.Min(DefaultSize.Height, last.Parent.Height / 2));
                        }

                        dock.DockControl(dc);
                        dock.BringToFront();                        
                    }
                    CleanUp();
                }
                
                OnStopDockMode(dock);
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

        /// <summary>
        /// Makes sure, that all <see cref="DockPanel"/> controls known by this manager get invalidated
        /// </summary>
        public void RepaintAll()
        {
            ForcePanelRepaint();
        }
    }    
}
