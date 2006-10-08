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

    [Designer(typeof(DockPanelDesigner)), ToolboxItem(false)]
    public partial class DockPanel : NCUserControl
    {
        List<DockPanelCaptionButton> cbuttons;
        DockPanelCollapseButton collapse;
        DockPanelCloseButton close;

        public DockPanel(DockManager manager) : base()
        {
            ManagerSingelton.Global.AddPanel(this);
            lastdock = null;
            lastpos = Point.Empty;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ContainerControl, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            seperateindockbar = false;
            canundock = true;

            SetManager(manager);
            this.DragBorder = false;
            this.ResizeBorder.Left = false;
            this.ResizeBorder.Right = false;
            this.ResizeBorder.Top = false;
            this.ResizeBorder.Bottom = false;
            

            InitializeComponent();            
            SetupCaptionButtons();
            SetDefaultImage();
            text = Name;
            btext = "";
        }

        public DockPanel() : this(null) { }

        ~DockPanel()
        {
            ManagerSingelton.Global.RemovePanel(this);
        }
        /*protected override void WndProc(ref Message m)
        {
            if (DockContainer!=null)
                if (DockContainer.CollapseState == DockContainer.Status.Collapsing || DockContainer.CollapseState == DockContainer.Status.Expanding)
                {
                    if (m.Msg == APIHelp.WM_PAINT || m.Msg == APIHelp.WM_SIZING || m.Msg == APIHelp.WM_MOVING || m.Msg == APIHelp.WM_NCCALCSIZE || m.Msg == APIHelp.WM_ENTERSIZEMOVE || m.Msg == APIHelp.WM_EXITSIZEMOVE)
                    {
                        return;
                    }
                }
            base.WndProc(ref m);
        }*/


        internal void SetManager(DockManager manager)
        {
            this.manager = manager;
            this.SetNonClientMargin();
        }

        private void SetupCaptionButtons()
        {
            cbuttons = new List<DockPanelCaptionButton>();

            collapse = new DockPanelCollapseButton(this);
            cbuttons.Add(collapse);

            close = new DockPanelCloseButton(this);
            cbuttons.Add(close);
        }

        public new string Text
        {
            get { return CaptionText; }
            set { CaptionText = value; }
        }

        string text, btext;
        [System.ComponentModel.Localizable(true)]
        public  string CaptionText
        {
            get { return text; }
            set {
                if (text != value) {
                    text = value;
                    if (btext == "") btext = text;
                    NCRefresh();
                }
            }
        }

        [System.ComponentModel.Localizable(true)]
        public string ButtonText
        {
            get { return btext; }
            set
            {
                if (btext != value)
                {
                    btext = value;
                    NCRefresh();
                }
            }
        }

        Image img;
        public Image Image
        {
            get {return img; }
            set
            {
                if (img != value)
                {
                    img = value;
                    if (img == null)
                    {
                        SetDefaultImage();
                    }
                    NCRefresh();
                }
            }
        }

        private void SetDefaultImage()
        {
            System.IO.Stream s = this.GetType().Assembly.GetManifestResourceStream("Ambertation.Windows.Forms.dockimg.png");
            if (s != null) img = Image.FromStream(s);
            else img = new Bitmap(16, 16);
        }

        DockManager manager;
        public DockManager Manager
        {
            get { return manager; }
            set { SetManager(value); }
        }

        bool canundock;
        public bool CanUndock
        {
            get { return canundock; }
            set { canundock = value; }
        }

        public ButtonOrientation BestOrientation
        {
            get
            {
                if (DockContainer != null) return DockContainer.BestOrientation;
                return ButtonOrientation.Bottom;
            }
        }

        public DockContainer DockContainer
        {
            get { return Parent as DockContainer; }
            set
            {
                if (value != Parent as DockContainer)
                {
                    DockControl(value);
                }
            }
        }

        public Size GetButtonSize()
        {
            return Manager.Renderer.DockPanelRenderer.GetButtonSize(this);
        }

        

        protected bool MouseOnSelector(Point mouse)
        {
            bool candrag = false;
            if (canundock)
                candrag = (Manager.Renderer.DockPanelRenderer.GetCaptionRect(this).Contains(mouse));

            if (ButtonData != null && !candrag)
            {
                DockPanel dp = ButtonData.GetHitPanel(mouse);
                candrag = (dp == this);
            }

            return candrag;
        }

        internal void CallNcMouseChanged(NCMouseEventArgs e)
        {
            OnNcMouseChanged(e);
        }
       
        protected override void OnNcMouseChanged(NCMouseEventArgs e)
        {
            if (ManagerSingelton.Global.HasDragPanelForMouseMove)
            {
                DockPanel dp = ButtonData.GetHitPanel(e.ControlPosition);
                if (dp != this  && dp!=null && DockContainer!=null)
                {
                    DockContainer.SwapPanelsInButtonList(this, dp);
                    //Console.WriteLine("Change Location");
                }
                return;
            }
            base.OnNcMouseChanged(e);
            
            bool chg = false;
            foreach (DockPanelCaptionButton b in cbuttons)
            {
                if (b.Hit(e))
                {
                    if (e.MouseButtons.Left) chg |= b.SetState(CaptionButtonState.Selected);
                    else chg |= b.SetState(CaptionButtonState.Highlight);
                }
                else chg |= b.SetState(CaptionButtonState.Normal);
            }
            if (chg) this.NCRefresh();

            if (e.MouseButtons.Left && e.InitialResult == NCHitTestEventArgs.Results.HTBORDER
                && (Math.Abs(e.Delta.X) >= 2 || Math.Abs(e.Delta.Y) >= 2))
            {
                bool candrag = MouseOnSelector(e.ControlPosition);

                

                if (candrag)
                {
                    Rectangle buts = Manager.Renderer.DockPanelRenderer.GetButtonsRectangle(BestOrientation, new NCPaintEventArgs(null, ClientRectangle, Bounds, null), DockContainer);
                    if (buts.Contains(e.ControlPosition))
                    {
                        candrag = false;
                        ManagerSingelton.Global.SetDragPanelOnMouseMove(this, e);
                    }
                    //Console.WriteLine("Start floating " + Text + ": " + e.Delta + ", " + e.MouseButtons + ", " + e.InitialResult);
                    if (candrag) StartDockModeFloat(e);
                }

            }
            //else //Console.WriteLine("    " + Text + ": " + e.Delta);
        }

        protected override void OnNcClick(NCMouseEventArgs e)
        {
            
            base.OnNcClick(e);
            if (DockContainer != null)
            {
                if (DockContainer.CollapseState != DockContainer.Status.Expanded) return;
            }

            foreach (DockPanelCaptionButton b in cbuttons)
            {
                if (b.Hit(e))
                {
                    b.PerformClick();
                    break;
                }
            }
        }

        internal void CallNcMouseDown(NCMouseEventArgs e)
        {
            OnNcMouseDown(e);
        }

        protected override void OnNcMouseDown(NCMouseEventArgs e)
        {
            base.OnNcMouseUp(e);

            if (MouseOnSelector(e.ControlPosition) && e.MouseButtons.Left)
            {
                //Console.WriteLine("Selected " + Text);
                this.EnsureVisible();
                this.Focus();
                
                Manager.RepaintAll();
            }

            DockPanel dp = ButtonData.GetHitPanel(e.ControlPosition);
            if (dp != null)
            {
                if (e.MouseButtons.Left)
                {
                    if (dp.DockContainer != null) dp.DockContainer.Focus();
                    dp.EnsureVisible();                    
                }
            }
        }

        

        /// <summary>
        /// Makes sure, that this Panel is the visible one in the container
        /// </summary>
        public void EnsureVisible()
        {
            if (DockContainer != null)            
                DockContainer.SetActiveDock(this);
            
        }

        internal void MakeVisibleByParentDockContainer()
        {
            this.Focus();
            this.NCRefresh();

        }

        /// <summary>
        /// Force a complete redraw of the Panel
        /// </summary>
        public void InvalidateWindow()
        {
            DoInvalidateWindow();
        }

        public bool Floating
        {
           get { return (this.ParentForm is DockPanelFloatingForm); }
        }

        internal DockPanelFloatingForm FloatForm
        {
            get { return ParentForm as DockPanelFloatingForm; }
        }

        public bool FloatContainer
        {
            get
            {
                if (FloatForm == null) return false;
                return FloatForm.HasContainer;
            }
        }

        public event System.EventHandler StartedFloating;
        internal void StartDockModeFloat(NCMouseEventArgs e)
        {
            if (Floating) return;
            DockContainer dcsofar = DockContainer;
            Point scr = e.ScreenPosition; // PointToScreen(new Point(e.X, e.Y));
            bool dragcontainer = false;
            if (Manager != null && DockContainer!=null)
            {
                Rectangle capt = Manager.Renderer.DockPanelRenderer.GetCaptionRect(this);
                if (capt.Contains(e.ControlPosition)) 
                    dragcontainer = !DockContainer.OneChild;
            }

            /*scr = new Point(
                scr.X - e.ControlPosition.X - System.Windows.Forms.SystemInformation.FrameBorderSize.Width,
                scr.Y - e.ControlPosition.Y - System.Windows.Forms.SystemInformation.FrameBorderSize.Height
                );*/
            DockPanelFloatingForm frm = LetFloat(scr, dragcontainer) as DockPanelFloatingForm;
            Manager.StartDockMode(this);
            
            
            frm.Show();
            frm.StartFloatingBlocked(this);
        }

        public void Float(Point pos)
        {
            DockPanelFloatingForm frm = LetFloat(pos, false) as DockPanelFloatingForm;
            frm.Text = this.CaptionText;
            frm.Show();
        }

        public void Float()
        {
            Point p = PointToScreen(new Point(0, 0));
            Float(p);   
        }

        protected Form LetFloat(Point pos, bool container)
        {
            if (Floating) return FloatForm;
            

            DockPanelFloatingForm frm = new DockPanelFloatingForm(this);
            frm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            frm.StartPosition = FormStartPosition.Manual;
            frm.ShowInTaskbar = false;
            frm.Width = this.Width + 2 * System.Windows.Forms.SystemInformation.FrameBorderSize.Width;
            frm.Height = this.Height + System.Windows.Forms.SystemInformation.ToolWindowCaptionHeight + 2 * System.Windows.Forms.SystemInformation.FrameBorderSize.Height;
            frm.Left = pos.X;
            frm.Top = pos.Y;
            frm.BringToFront();

            DockContainer dc = DockContainer;
            

            if (container && DockContainer != null)
            {
                DockContainer.Parent = frm;
                DockContainer.Dock = DockStyle.Fill;
                frm.DragContainerAlong(DockContainer);
                this.NCRefresh();
            }
            else
            {
                if (dc != null) dc.RemoveDock(this);
                this.Parent = frm;
                this.Visible = true;
                this.Dock = DockStyle.Fill;
            }

            SetNonClientMargin();            
            this.ResetNCMouseState();

            if (StartedFloating != null) StartedFloating(this, new EventArgs());
            if (Manager != null) Manager.NotifyFloating(this);
            
            return frm;
        }                

        internal void DockControl(DockContainer parent)
        {
            if (parent == DockContainer) return;
  
            DockPanelFloatingForm f = this.Parent as DockPanelFloatingForm;

            if (f!=null) 
            {
                this.Parent = null;
                f.Close();
                f.Dispose();                
            }

            SetNonClientMargin();

            parent.AddDock(this);
            parent.RepaintAll();
        }

        private void SetNonClientMargin()
        {
            if (manager != null)
            {
                
                this.NonClientMargin = manager.Renderer.DockPanelRenderer.GetPanelBorderSize(DockContainer, this, BestOrientation);
                //Console.WriteLine("Changed Margin in " + Name + " to " + NonClientMargin);
            }
        }

        internal void UnFloat(DockPanelFloatingForm f)
        {            
            Manager.StopDockMode(this);
            this.Refresh();
        }

        public void Open()
        {
            if (lastdock != null)
            {
                if (lastdock.Parent != null)
                {
                    lastdock.AddDock(this);
                    EnsureVisible();

                    return;
                }
                else if (Manager!=null)
                {
                    Manager.DockPanel(this, lastdock.Dock);
                    return;
                }
            }
            Float(lastpos);

        }

        DockContainer lastdock;
        Point lastpos;
        public void Close()
        {
            DockPanelClosingEvent e = new DockPanelClosingEvent(this);
            if (Closing != null) this.Closing(this, e);
            if (e.Cancel) return;

            lastdock = this.DockContainer;
            lastpos = Location;
            if (lastdock != null)
            {

                lastdock.RemoveDock(this);
                if (lastdock.Highlight != null) lastdock.Highlight.NCRefresh();
                Manager.Update();
            }

            DockPanelFloatingForm f = this.Parent as DockPanelFloatingForm;
            if (f != null)
            {
                f.Close();
                lastpos = f.Location;
            }

            if (Closed != null) Closed(this, new EventArgs());
        }

        public event System.EventHandler Closed;
        public event ClosingHandler Closing;
        public delegate void ClosingHandler(object sender, DockPanelClosingEvent e);
        public class DockPanelClosingEvent : EventArgs
        {
            DockPanel dp;
            bool cancel;

            public bool Cancel
            {
                get { return cancel; }
                set { cancel = value; }
            }
            public DockPanelClosingEvent(DockPanel dp) : base (){ 
                this.dp = dp;
                cancel = false;
            }
        }

        public void Collapse()
        {
            if (DockContainer != null) DockContainer.Collapse();
        }

        public void Collapse(bool anim)
        {
            if (DockContainer != null) DockContainer.Collapse(anim);
        }

        public void Expand()
        {
            if (DockContainer != null) DockContainer.Expand();
        }

        public void Expand(bool anim)
        {
            if (DockContainer != null) DockContainer.Expand(anim);
        }

       


        DockPanelButtonManager buttonData;
        protected DockPanelButtonManager ButtonData
        {
            get { return buttonData; }
        }

        
        protected override void OnNcPaint(NCPaintEventArgs e)
        {
            //base.OnNcPaint(e);
            
            if (Manager!=null) {
                //Console.WriteLine("NCPaint " + Text + " " + Floating);
                e.Graphics.FillRegion(new SolidBrush(manager.Renderer.ColorTable.DockBackgroundColor), e.PaintRegion);

                
                buttonData = Manager.Renderer.DockPanelRenderer.ConstructButtonData(DockContainer, e);
                if ((Floating == false || FloatContainer))
                {
                    if (DockContainer != null)
                    {
                        if (!OnlyChild || !DockContainer.HideSingleButton)
                        {
                            Manager.Renderer.DockPanelRenderer.RenderButtonBackground(this, e);
                            buttonData.Render();
                        }
                    }
                    else
                    {
                        Manager.Renderer.DockPanelRenderer.RenderButtonBackground(this, e);
                        buttonData.Render();
                    }
                }

                if (!Floating)
                {
                    Manager.Renderer.DockPanelRenderer.RenderCaption(this, e);
                    foreach (DockPanelCaptionButton b in cbuttons) b.Render(e);
                    Manager.Renderer.DockPanelRenderer.RenderBorder(this, e);
                }
            }
        }

        protected override void OnBufferedPaint(PaintEventArgs e)
        {
            base.OnBufferedPaint(e);
            //Console.WriteLine("Paint " + Text);
            /*Brush b = new SolidBrush(Color.White);
            e.Graphics.FillEllipse(b, 0, 0, 10, 10);
            e.Graphics.FillEllipse(b, 0, ClientRectangle.Height - 11, 10, 10);
            e.Graphics.FillEllipse(b, ClientRectangle.Width - 11, 0, 10, 10);
            e.Graphics.FillEllipse(b, ClientRectangle.Width - 11, ClientRectangle.Height - 11, 10, 10);
            b.Dispose();

            Pen p = new Pen(Color.Black);
            e.Graphics.DrawEllipse(p, 0, 0, 10, 10);
            e.Graphics.DrawEllipse(p, 0, ClientRectangle.Height - 11, 10, 10);
            e.Graphics.DrawEllipse(p, ClientRectangle.Width - 11, 0, 10, 10);
            e.Graphics.DrawEllipse(p, ClientRectangle.Width - 11, ClientRectangle.Height - 11, 10, 10);
            p.Dispose();*/
        }


        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
        }

        #region Highlight change
        internal void FireHighlightChanged(DockPanel dp)
        {
            if (dp == this)
            {                
                this.Visible = true;
                this.NCRefresh();
                
            }
            else this.Visible = false;
            
            

            if (HighlightChange != null) HighlightChange(this, new HighlightChangeEventArgs(this, dp));
        }
        public event HighlightChangeEvent HighlightChange;

        public delegate void HighlightChangeEvent(DockPanel sender, HighlightChangeEventArgs e);
        public class HighlightChangeEventArgs : EventArgs
        {
            DockPanel newhl, cur;
            public DockPanel NewHighlight
            {
                get { return newhl; }
            }

            public bool GotHighlight
            {
                get { return newhl == cur; }
            }
            
            internal HighlightChangeEventArgs(DockPanel cur, DockPanel newhl)
                : base()
            {
                this.newhl = newhl;
                this.cur = cur;
            }
        }
        #endregion

        bool seperateindockbar;
        internal bool SeperateInDockBar
        {
            get { return seperateindockbar; }
            set { seperateindockbar = value; }
        }

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

        public static bool operator ==(DockPanel a, DockPanel b)
        {
            bool an = ((object)a) == null;
            bool bn = ((object)b) == null;
            if (an) return bn;
            if (bn) return false;

            return a.Guid == b.Guid;
        }

        public static bool operator !=(DockPanel a, DockPanel b)
        {
            bool an = ((object)a) == null;
            bool bn = ((object)b) == null;
            if (an) return !bn;
            if (bn) return true;

            return a.Guid != b.Guid;
        }
        #endregion


        public bool ShowCloseButton
        {
            get { return close.Visible; }
            set {
                if (close.SetVisible(value)) NCRefresh();                
            }
        }

        public bool ShowCollapseButton
        {
            get { return collapse.Visible; }
            set {
                if (collapse.SetVisible(value)) NCRefresh();                
            }
        }

        /// <summary>
        /// Returns the state this dock is currently in
        /// </summary> 
        internal CaptionState CaptionState
        {
            get
            {
                bool foc = this.HasFocus();
                if (DockContainer != null) foc |= DockContainer.Focused;
                if (foc) return CaptionState.Focused;
                else return CaptionState.Normal;
            }
        }

        protected bool HasFocus()
        {
            if (Focused) return true;
            foreach (Control c in Controls)
                if (c.Focused) return true;

            return false;
        }

        /// <summary>
        /// True, if the parent <see cref="DockContainer"/> does only contain this <see cref="DockPanel"/>
        /// </summary>
        public bool OnlyChild
        {
            get
            {
                if (DockContainer == null) return true;
                return DockContainer.OneChild;
            }
        }

        internal void OnPanelCollectionChanged(DockPanel newdp, DockContainer cnt, bool remove)
        {
            if (remove && OnlyChild)
            {
                //Console.WriteLine("Changed Collection " + Name);
                this.SetNonClientMargin();
                RefreshAll();
            }
            else if (!remove && !OnlyChild)
            {
                //Console.WriteLine("Changed Collection " + Name);
                this.SetNonClientMargin();
                RefreshAll();
            }
        }

        public virtual void RefreshMargin()
        {
            this.SetNonClientMargin();
        }

        public override void RefreshAll()
        {
            RefreshMargin();
            base.RefreshAll();
        }
    }
}
