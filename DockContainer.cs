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
    [Designer(typeof(DockContainerDesigner)), ToolboxItem(false)]
    public partial class DockContainer : UserControl, IButtonContainer
    {
        protected List<DockContainer> containers;
        DockButtonBar.DockPanelList panels;
        protected DockManager manager;
        

        public enum Status
        {
            Collapsed,
            Expanded,
            Collapsing,
            Expanding
        }
        
        internal DockContainer(DockManager manager)
        {
            layoutct = 0;
            //this.SetStyle(ControlStyles.ContainerControl, true);
            //this.BackColor = Color.DarkCyan;
            
            containers = new List<DockContainer>();
            panels = new DockButtonBar.DockPanelList();

            noclean = false;
            nccleanint = false;
            useastar = false;
            state = Status.Expanded;
            SetManager(manager);
        }

         ~DockContainer()
        {
            if(manager!=null) manager.Renderer.DockPanelRenderer.FinishedAnimation -= new DockAnimationEventHandler(DockPanelRenderer_FinishedAnimation);
        }
        
        public DockContainer() :this(null) {}

        internal void SetManager(DockManager manager)
        {
            this.manager = manager;
            if (manager != null) manager.Renderer.DockPanelRenderer.FinishedAnimation += new DockAnimationEventHandler(DockPanelRenderer_FinishedAnimation);
        }
        
        public DockManager Manager
        {
            get { return manager; }
            set { SetManager(value); }
        }

        DockContainer pc;
        protected DockContainer ParentContainer
        {
            get { return pc; }
            set { pc = value; }
        }

        bool noclean, nccleanint;
        internal void SetNoCleanUpIntern(bool val)
        {
            nccleanint = val;
        }
        public bool NoCleanup
        {
            get { return noclean || nccleanint; }
            set { noclean = value; }
        }

        Size expsz;
        public Size ExpandedSize
        {
            get { return expsz; }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (state == Status.Expanded) expsz = this.Size;

            foreach (DockContainer dc in containers)
            {
                if (dc.Left < 0) dc.Width = Math.Max(dc.Width + dc.Left, MinimumDockSize);
                if (dc.Top < 0) dc.Height = Math.Max(dc.Height + dc.Top, MinimumDockSize);
            }
        }
        

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            //Console.WriteLine("Change Parent of " + Name);
            pc = Parent as DockContainer;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (e.Control == null) return;
            DockContainer dc = e.Control as DockContainer;
            if (dc != null)
            {
                containers.Add(dc);
                dc.ParentContainer = this;
                //Console.WriteLine("Adding Container to " + Name);
                //OnDockContainerAdded(dc);
            }
            else
            {
                DockPanel p = e.Control as DockPanel;
                if (p != null)
                {
                    panels.Add(p);
                    p.Parent = this;
                    p.Dock = DockStyle.Fill;
                    p.EnsureVisible();
                    if (Manager != null) Manager.CleanUp();
                    else CleanUp();
                }
            }
        }

        private void OnDockContainerAdded(DockContainer dc)
        {
            Console.WriteLine("#### Adding Container " + dc.Name);
            ListControls();
            dc.SetForceUseAsTarget(true);
            RearrangeControls();
            RefreshSplitters();
            dc.SetForceUseAsTarget(false);
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            DockContainer dc = e.Control as DockContainer;
            if (dc != null)
            {
                //Console.WriteLine("Removing Container from " + Name);
                containers.Remove(dc);
                dc.ParentContainer = null;
                RefreshSplitters();
            }
            else
            {
                DockPanel p = e.Control as DockPanel;
                if (p != null)
                {
                    p.Parent = null;
                    panels.Remove(p);
                    if (Highlight == p)
                        if (panels.Count > 0)
                            panels[0].EnsureVisible();


                    if (Manager != null) Manager.CleanUp();
                    else CleanUp();
                }
            }
        }

        void GenerateSplitter(DockStyle ds, int index)        
        {
            Console.Write("Adding " + ds + " at " + index);            
            Splitter s = new Splitter();
            s.Parent = this;
            s.Dock = ds;

            Controls.Add(s);
            if (Controls.Contains(s)) Controls.SetChildIndex(s, index);
        }

        public void RefreshSplitters()
        {
            this.SuspendLayout();
            Console.WriteLine("#### Setting Splitters "+Name);
            List<Control>splitters = new List<Control>();
            List<DockPanel> panels = new List<DockPanel>();
            foreach (Control c in Controls)
            {
                if (c is Splitter) splitters.Add(c as Splitter);
                else if (c is DockPanel)
                {
                    DockPanel dp = c as DockPanel;
                    if (dp.Dock == DockStyle.Fill) panels.Add(dp);
                }
            }

            foreach (Splitter s in splitters)
                Controls.Remove(s);

            foreach (DockPanel dp in panels)
                Controls.SetChildIndex(dp, 0);

            ListControls();
            foreach (DockContainer dc in containers)
            {
                int i = Controls.GetChildIndex(dc);
                Console.WriteLine("  -> Found Dock " + dc.Name + " " + dc.IgnoreAsTarget + " " + dc.Dock + " " + dc.Visible + " " + i);
                if (dc.IgnoreAsTarget) 
                    continue;

                if (dc.Dock == DockStyle.Right) GenerateSplitter(DockStyle.Right, i);
                else if (dc.Dock == DockStyle.Left) GenerateSplitter(DockStyle.Left, i);
                else if (dc.Dock == DockStyle.Top) GenerateSplitter(DockStyle.Top, i);
                else if (dc.Dock == DockStyle.Bottom) GenerateSplitter(DockStyle.Bottom, i);
            }

            /*
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                Console.WriteLine(i);
                Control c = Controls[i];
                if (c is Splitter) continue;
                if (c is DockButtonBar) continue;
                DockContainer dc = c as DockContainer;
                if (dc != null)
                {
                    Console.WriteLine("  -> Found Dock " + dc.Name+" "+dc.IgnoreAsTarget+" "+dc.Dock+" "+dc.Visible+" "+i);
                    if (dc.IgnoreAsTarget) continue;
                }

                //Console.WriteLine(c.Dock);
                if (c.Dock == DockStyle.Right) GenerateSplitter(DockStyle.Right, i);
                else if (c.Dock == DockStyle.Left) GenerateSplitter(DockStyle.Left, i);
                else if (c.Dock == DockStyle.Top) GenerateSplitter(DockStyle.Top, i);
                else if (c.Dock == DockStyle.Bottom) GenerateSplitter(DockStyle.Bottom, i);
                
            }*/

            ListControls();
            this.ResumeLayout();
        }

        

        internal void ListControls()
        {
           
            Console.WriteLine("Listing " + Name);
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                Control c = Controls[i];
                Console.WriteLine(i + ": " + c.GetType().Name+" "+c.Dock+" "+c.Name);
            }
        }

        public DockContainer CreateNewContainer()
        {
            DockContainer dc = CreateNewContainer(-1, true, true, DockStyle.None);
            dc.Visible = true;
            return dc;
        }

        public void SetDefaultSize()
        {
            Width = Manager.DefaultSize.Width;
            Height = Manager.DefaultSize.Height;
        }

        public void SetMinSize()
        {
            Width = Math.Max(Width, Manager.DefaultSize.Width);
            Height = Math.Max(Height, Manager.DefaultSize.Height);
        }

        protected virtual void SetNewContainerIndex(ref int index, ref bool after, ref bool toplevel, DockStyle dockstyle)
        {
            if (after && index >= 0) index++; //do not move the first element in the list!
        }
        protected override void OnChangeUICues(UICuesEventArgs e)
        {
            base.OnChangeUICues(e);
            Console.WriteLine("Change UI Cues");
        }
        public DockContainer CreateNewContainer(int index, bool after, bool toplevel, DockStyle dockstyle)
        {
            //Console.WriteLine("1: "+toplevel + " " + after + " " + index);
            SetNewContainerIndex(ref index, ref after, ref toplevel, dockstyle);
            //Console.WriteLine("2: "+toplevel + " " + after + " " + index);
                        
            DockContainer dc = new DockContainer(this.Manager);
            dc.Visible = false;
            dc.Dock = dockstyle;
            this.Controls.Add(dc);
            
            if (index >= 0 && index < Controls.Count && !toplevel)
                Controls.SetChildIndex(dc, index);

            dc.SetDefaultSize();
            this.OnDockContainerAdded(dc);
            return dc;
        }

        public DockButtonBar.DockPanelList GetDockedPanels()
        {            
            return panels;
        }

        internal Point ScreenLocation
        {
            get {
                //if (ParentForm != null && Parent != null) return ParentForm.PointToScreen(Location);
                if (Parent != null) return Parent.PointToScreen(Location);
                return Location; 
            }
        }

        internal Size ScreenSize {
            get
            {
                //if (ParentForm != null && Parent != null) return ParentForm.ClientSize;
                //if (Parent != null) return Parent.ClientSize;
                return ClientSize; 
            }
    }

        internal Rectangle ScreenBounds
        {
            get
            {
                return new Rectangle(ScreenLocation, ScreenSize);
            }
        }

        internal bool HintHitCheck(Point scrpt)
        {
            if (IgnoreAsTarget) return false;
            return Hit(scrpt);
        }

        internal bool Hit(Point scrpt) 
        {
            Point l = ScreenLocation;
            if (scrpt.X > l.X && scrpt.X < l.X + Width)
                if (scrpt.Y > l.Y && scrpt.Y < l.Y + Height)
                    return true;

            return false;
        }               

        internal void AddDock(DockPanel child)
        {
            this.Controls.Add(child);
            
        }

        internal void RemoveDock(DockPanel child)
        {
            this.Controls.Remove(child);
            
        }

        internal virtual void TakeHint(DockHint hint)
        {
            Rectangle r = this.ScreenBounds;
            TakeHint(hint, r, this);
        }

        internal void TakeHint(DockHint hint, Rectangle r, DockContainer d)
        {
            hint.ParentContainer = d;
            hint.SetDesktopLocation(
                r.Left + (r.Width - hint.Width) / 2,
                r.Top + (r.Height - hint.Height) / 2
                );

            if (Manager.DockMode) UpdateHintVisibility();
        }


        protected virtual DockContainer GetDockContainer(Point scrpt)
        {
            if (this.HintHitCheck(scrpt))
            {
                foreach (DockContainer dc in containers)
                {
                    DockContainer ret = dc.GetDockContainer(scrpt);
                    if (ret != null) return ret;
                    if (dc.HintHitCheck(scrpt)) return dc;
                }

                //there was a hit, but non of the childs returned true, so we are over this element
                return this;
            }

            
            return null;
        }
        

        protected virtual void OnTakeHint()
        {
        }

        protected bool IsManager
        {
            get { return this is DockManager; }
        }

        protected virtual void UpdateHintVisibility()
        {
            //Console.WriteLine("Update Visible Hints");
        }


        #region MouseOverHint
        protected struct ContainerInfo
        {
            public bool TopLevel;
            public bool DockInside;
            public DockStyle Dock;
            public Rectangle OverlayRectangle;
            public SelectedHint Hint;
            public DockContainer Parent;
            public DockContainer Seed;
            public int SeedIndex;

            public override string ToString()
            {
                return "hint:"+Hint+", topl:"+TopLevel+", dockins:"+DockInside+", dock:"+Dock+", rect:"+OverlayRectangle+", sid="+SeedIndex+", parent:"+Parent;
            }
        }

        protected void MouseOverHint(DockHint sender, SelectedHint hint)
        {
            if (!sender.Visible) return;
            //Console.WriteLine(hint + "; " + sender.Text);
            ContainerInfo nfo = new ContainerInfo();
            
            nfo.Hint = hint; 
            nfo.Parent = this;
            nfo.DockInside = false;
            nfo.TopLevel = false;
            //Console.WriteLine(this.GetType().Name+" "+nfo.Parent.GetType().Name);
            if (sender.ParentContainer == null)
            {
                nfo.Seed = this;
                SetTargetContainerInfo(sender, ref nfo);
            }
            else
            {
                nfo.Seed = sender.ParentContainer;
                sender.ParentContainer.SetTargetContainerInfo(sender, ref nfo);
            }

            if (nfo.Hint == SelectedHint.Left) nfo.Dock = DockStyle.Left;
            else if (nfo.Hint == SelectedHint.Right) nfo.Dock = DockStyle.Right;
            else if (nfo.Hint == SelectedHint.Top) nfo.Dock = DockStyle.Top;
            else if (nfo.Hint == SelectedHint.Bottom) nfo.Dock = DockStyle.Bottom;           
            else nfo.Dock = DockStyle.Fill;
            //nfo.Hint = hint;

            nfo.Parent.GetTargetRectangle(ref nfo);
            nfo.SeedIndex = nfo.Parent.Controls.IndexOf(nfo.Seed);
            //Console.WriteLine(nfo.OverlayRectangle + " " + nfo.Parent.GetType().Name + " " + nfo.Seed.GetType().Name + " " + nfo.Dock + " " + nfo.SeedIndex + " " + nfo.DockInside + " " + nfo.TopLevel);
            OnMouseOverHint(sender, ref nfo);
        }



        protected virtual void SetTargetContainerInfo(DockHint sender, ref ContainerInfo nfo)
        {
            SetContainerParent(ref nfo);
            if (this.Dock == DockStyle.Left)
            {
                if (nfo.Hint == SelectedHint.Right)
                {
                    nfo.DockInside = true;
                    nfo.Hint = SelectedHint.Left;
                }
            }
            else if (this.Dock == DockStyle.Right)
            {
                if (nfo.Hint == SelectedHint.Left)
                {
                    nfo.DockInside = true;
                    nfo.Hint = SelectedHint.Right;
                }
            }
            else if (this.Dock == DockStyle.Top)
            {
                if (nfo.Hint == SelectedHint.Bottom)
                {
                    nfo.DockInside = true;
                    nfo.Hint = SelectedHint.Top;
                }
            }
            else if (this.Dock == DockStyle.Bottom)
            {
                if (nfo.Hint == SelectedHint.Top)
                {
                    nfo.DockInside = true;
                    nfo.Hint = SelectedHint.Bottom;
                }
            }
            

        }

        private void SetContainerParent(ref ContainerInfo nfo)
        {
            nfo.Parent = this;
            //Console.WriteLine(this.GetType().Name+": "+nfo.Hint + " -- " + this.Dock);
            //Console.WriteLine("Parent IN: " + nfo.Parent.GetType().Name);
            if (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
            {
                if ((nfo.Hint == SelectedHint.Left || nfo.Hint == SelectedHint.Right) && this.ParentContainer != null)
                    nfo.Parent = this.ParentContainer;

            }
            else if (this.Dock == DockStyle.Top || this.Dock == DockStyle.Bottom)
            {
                if ((nfo.Hint == SelectedHint.Top || nfo.Hint == SelectedHint.Bottom) && this.ParentContainer != null)
                    nfo.Parent = this.ParentContainer;
            }
            //Console.WriteLine("Parent OUT: " + nfo.Parent.GetType().Name);
        }

        protected virtual void GetTargetRectangle(ref ContainerInfo nfo)
        {
            //Console.WriteLine(nfo.Parent.GetType().Name + " " + nfo.Seed.GetType().Name + " " + this.GetType().Name);
            Rectangle sb = nfo.Parent.ScreenBounds;
            Rectangle ssb = nfo.Seed.ScreenBounds;

            Size dsz = new Size(
                Math.Max(10, Math.Min(sb.Width / 2, Manager.DefaultSize.Width)),
                Math.Max(10, Math.Min(sb.Height / 2, Manager.DefaultSize.Height))
            );

            if (nfo.DockInside)
            {
                if (nfo.Hint == SelectedHint.Right)
                    nfo.OverlayRectangle = new Rectangle(ssb.Left - dsz.Width, ssb.Top, dsz.Width, ssb.Height);
                else if (nfo.Hint == SelectedHint.Left)
                    nfo.OverlayRectangle = new Rectangle(ssb.Right , ssb.Top, dsz.Width, ssb.Height);
                else if (nfo.Hint == SelectedHint.Bottom)
                    nfo.OverlayRectangle = new Rectangle(ssb.Left, ssb.Top - dsz.Height, ssb.Width, dsz.Height);
                else if (nfo.Hint == SelectedHint.Top)
                    nfo.OverlayRectangle = new Rectangle(ssb.Left, ssb.Bottom, ssb.Width, dsz.Height);
                else if (nfo.Hint == SelectedHint.Center && !IsManager)
                    nfo.OverlayRectangle = sb;
                else nfo.OverlayRectangle = Rectangle.Empty;
            }
            else
            {
                if (nfo.Hint == SelectedHint.Left)
                    nfo.OverlayRectangle = new Rectangle(sb.Left, ssb.Top, dsz.Width, ssb.Height);
                else if (nfo.Hint == SelectedHint.Right)
                    nfo.OverlayRectangle = new Rectangle(sb.Right - dsz.Width, ssb.Top, dsz.Width, ssb.Height);
                else if (nfo.Hint == SelectedHint.Top)
                    nfo.OverlayRectangle = new Rectangle(ssb.Left, sb.Top, ssb.Width, dsz.Height);
                else if (nfo.Hint == SelectedHint.Bottom)
                    nfo.OverlayRectangle = new Rectangle(ssb.Left, sb.Bottom - dsz.Height, ssb.Width, dsz.Height);
                else if (nfo.Hint == SelectedHint.Center && !IsManager)
                    nfo.OverlayRectangle = sb;
                else nfo.OverlayRectangle = Rectangle.Empty;
            }
        }

        protected virtual void OnMouseOverHint(DockHint sender, ref ContainerInfo nfo)
        {
        }

        protected int SubControls
        {
            get {
                int ret = 0;
                foreach (Control c in Controls)
                {
                    if (c is Splitter) continue;
                    if (c is DockContainer) continue;
                    ret++;
                }
                return ret;
            }
        }

        protected virtual void RearrangeControls()
        {
        }

        protected virtual void CleanUp()
        {
            for (int i=containers.Count-1; i>=0; i--)
            {
                DockContainer dc = containers[i];
                
                dc.CleanUp();


                if (dc.SubControls == 0 && !dc.NoCleanup)                
                    dc.Remove();                
            }
        }

        protected void Remove()
        {
            if (this.ParentContainer == null) return;

            MoveChildDocksUp();
            if (this.ParentContainer == null) return;
            this.ParentContainer.Controls.Remove(this);
        }        

        protected void SetParent(DockContainer dc, int index)
        {            
            this.Parent = dc;
            if (index>=0) dc.Controls.SetChildIndex(this, index);
        }
        #endregion

        protected void MoveChildDocksUp()
        {
            MoveChildDocksUp(false);
        }

        protected void MoveChildDocksUp(bool resize)
        {
            if (this.ParentContainer == null) return;
            if (this is DockManager) return;

            DockContainer np = null;
            for (int i = containers.Count - 1; i >= 0; i--)
            {
                DockContainer dc = containers[i];
                if (np == null)
                {
                    np = dc;
                    dc.Dock = DockStyle.None;
                    dc.SetParent(this.ParentContainer, ParentContainer.Controls.IndexOf(this));

                    dc.Dock = this.Dock;
                }
                else
                {
                    dc.SetParent(np, -1);
                }

                if (resize) dc.SetMinSize();
                //Console.WriteLine("--->" + dc.Parent.GetType().Name + " " + dc.Width + " " + dc.Height + " " + dc.Visible);
            }            
        }

        /// <summary>
        /// Make sure that a DockPanel, that is parented by this Container is the one that is visible
        /// </summary>
        /// <param name="pn"></param>
        protected void ShowDockPanel(DockPanel pn)
        {
            int gotoindex = -1;
            int ct = 0;
            foreach (Control c in Controls)
            {
                DockPanel dp = c as DockPanel;
                if (dp != null)
                {
                    if (dp.Dock == DockStyle.Fill && gotoindex == -1)
                    {
                        gotoindex = ct;
                        break;
                    }
                }
                ct++;
            }
            if (gotoindex>=0)
                if (Controls.Contains(pn))
                    Controls.SetChildIndex(pn, gotoindex);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            /*Rectangle r = new Rectangle(ClientRectangle.Location, new Size(Width - 1, Height - 1));
            e.Graphics.FillRectangle(new SolidBrush(BackColor), r);
            e.Graphics.DrawRectangle(new Pen(Color.WhiteSmoke), r);

            if (this.DesignMode) e.Graphics.DrawString(Name, Font, new SolidBrush(ForeColor), 2, 2);*/
        }

        bool useastar;
        internal void SetForceUseAsTarget(bool val)
        {
            useastar = val;
        }
        protected bool IgnoreAsTarget
        {
            get { return !Visible && !useastar; }
        }

        /// <summary>
        /// Makes sure all panels known in this container, and all of it's child containers get repainted
        /// </summary>
        protected void ForcePanelRepaint()
        {
            DockButtonBar.DockPanelList list = this.GetDockedPanels();
            foreach (DockPanel dp in list)
                dp.InvalidateWindow();

            foreach (DockContainer dc in containers)
                dc.ForcePanelRepaint();
        }

        /// <summary>
        /// Makes sure, that all <see cref="DockPanel"/> controls known by this manager get invalidated
        /// </summary>
        public void RepaintAll()
        {
            ForcePanelRepaint();
        }

        #region Collapse /Expand
        Status state;
        public DockAnimationEventArgs.Alignment Alignment
        {
            get { return this.GetAlignment(); }
        }

        protected DockAnimationEventArgs.Alignment GetAlignment()
        {
            DockAnimationEventArgs.Alignment a = DockAnimationEventArgs.Alignment.Undefined;
            if (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right) a = DockAnimationEventArgs.Alignment.Vertical;
            if (this.Dock == DockStyle.Top || this.Dock == DockStyle.Bottom) a = DockAnimationEventArgs.Alignment.Horizontal;
            return a;
        }

        public void Collapse() { Collapse(true); }
        public void Collapse(bool animated)
        {
            if (Collapsed) return;
            DockAnimationEventArgs.Alignment a = GetAlignment();

            DockAnimationEventArgs e = new DockAnimationEventArgs(this, DockAnimationEventArgs.Type.Collapse, a);
            state = Status.Collapsing;

            if (animated) Manager.Renderer.DockPanelRenderer.Animate(e);
            else
            {
                if (e.DockAlignment == DockAnimationEventArgs.Alignment.Horizontal) Height = this.MinimumDockSize;
                else if (e.DockAlignment == DockAnimationEventArgs.Alignment.Vertical) Width = this.MinimumDockSize;

                DockPanelRenderer_FinishedAnimation(Manager.Renderer.DockPanelRenderer, e);
            }
        }


        public void Expand() { Expand(true); }
        public void Expand(bool animated)
        {
            if (Expanded) return;
            DockAnimationEventArgs.Alignment a = GetAlignment();

            DockAnimationEventArgs e = new DockAnimationEventArgs(this, DockAnimationEventArgs.Type.Expand, a);
            state = Status.Expanding;
            if (animated) Manager.Renderer.DockPanelRenderer.Animate(e);
            else
            {
                if (e.DockAlignment == DockAnimationEventArgs.Alignment.Horizontal) Height = this.ExpandedSize.Height;
                else if (e.DockAlignment == DockAnimationEventArgs.Alignment.Vertical) Width = this.ExpandedSize.Width;

                DockPanelRenderer_FinishedAnimation(Manager.Renderer.DockPanelRenderer, e);
            }
        }

        public int MinimumDockSize
        {
            get { return 20; }
        }


        public bool Collapsed
        {
            get { return state == Status.Collapsed || state == Status.Collapsing; }
        }
        public bool Expanded
        {
            get { return state == Status.Expanding || state == Status.Expanded; }
        }

        void DockPanelRenderer_FinishedAnimation(IDockPanelRenderer sender, DockAnimationEventArgs e)
        {
            if (e.Container != this) return;

            if (e.AnimationType == DockAnimationEventArgs.Type.Collapse)
            {
                state = Status.Collapsed;
                this.Visible = false;
                MoveChildDocksUp(true);
                Manager.GetBestButtonBar(this).Add(this);
                if (ParentContainer!=null) ParentContainer.RefreshSplitters();
            }
            else if (e.AnimationType == DockAnimationEventArgs.Type.Expand)
            {
                state = Status.Expanded;
                Manager.GetBestButtonBar(this).Remove(this);
                this.Visible = true;
                if (ParentContainer != null) ParentContainer.RefreshSplitters();
            }
        }        
        #endregion

        #region Equals 'n stuff
        System.Guid? guid = null;
        protected void GenGUID()
        {
            guid = System.Guid.NewGuid();
        }

        public System.Guid Guid
        {
            get
            {
                if (guid == null) GenGUID();
                return (System.Guid)guid;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            DockContainer dc = obj as DockContainer;
            if (dc != null)
                return Guid == dc.Guid;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }

        public static bool operator ==(DockContainer a, DockContainer b)
        {
            bool an = ((object) a)== null;
            bool bn = ((object) b)== null;
            if (an) return bn;
            if (bn) return false;

            return a.Guid == b.Guid;
        }

        public static bool operator !=(DockContainer a, DockContainer b)
        {
            bool an = ((object)a) == null;
            bool bn = ((object)b) == null;
            if (an) return !bn;
            if (bn) return true;

            return a.Guid != b.Guid;
        }
        #endregion

        #region IButtonContainer Member
        DockPanel hldp;
        public DockPanel Highlight
        {
            get { return hldp; }
        }

        public void SetActiveDock(DockPanel dp){
            if (hldp != dp)
                {
                    hldp = dp;
                
                    ShowDockPanel(dp);
                    hldp.MakeVisibleByParentDockContainer();
                }
        }
        public DockButtonBar.DockPanelList GetButtons()
        {
            return this.GetDockedPanels();
        }

        public ButtonOrientation BestOrientation
        {
            get
            {
                return ButtonOrientation.Bottom;
            }
        }

        public Padding GetBorderSize(ButtonOrientation orient)
        {
            return manager.Renderer.DockPanelRenderer.GetPanelBorderSize(orient);
        }

        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DockContainer
            // 
            this.Name = "DockContainer";
            this.ResumeLayout(false);

        }

        public new void Update()
        {
            base.Update();
            CleanUp();
        }

        

        #region Layout
        int layoutct;
        public new void SuspendLayout()
        {
            if (layoutct==0) base.SuspendLayout();
            layoutct++;
        }       

        public new void ResumeLayout()
        {
            layoutct--;
            if (layoutct == 0)
            {
                base.ResumeLayout();
                if (Highlight != null) Highlight.EnsureVisible();
            }
            else if (layoutct < 0) layoutct = 0;
        }

        public void ForceResumeLayout()
        {
            layoutct = 0;
            base.ResumeLayout();
        }
        #endregion
    }
}
