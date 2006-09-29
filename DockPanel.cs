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

namespace Floaters
{
    
    public partial class DockPanel : NCUserControl
    {
        List<DockPanelCaptionButton> cbuttons;
        DockPanelCollapseButton collapse;
        DockPanelCloseButton close;
        public DockPanel(DockManager manager) : base()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            seperateindockbar = false;
            canundock = true;

            this.manager = manager;
            this.DragBorder = false;
            this.ResizeBorder.Left = false;
            this.ResizeBorder.Right = false;
            this.ResizeBorder.Top = false;
            this.ResizeBorder.Bottom = false;
            

            InitializeComponent();

            this.NonClientMargin = manager.Renderer.DockPanelRenderer.GetPanelBorderSize(BestOrientation);
            SetupCaptionButtons();
        }

        private void SetupCaptionButtons()
        {
            cbuttons = new List<DockPanelCaptionButton>();

            collapse = new DockPanelCollapseButton(this);
            cbuttons.Add(collapse);

            close = new DockPanelCloseButton(this);
            cbuttons.Add(close);
        }

        string text;
        public new string Text
        {
            get { return Name; }
            set {
                if (text != value)
                {
                    text = value;
                    Refresh();
                }
            }
        }

        DockManager manager;
        public DockManager Manager
        {
            get { return manager; }
            set { manager = value; }
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
        }

        public Size GetButtonSize()
        {
            return Manager.Renderer.DockPanelRenderer.GetButtonSize(this);
        }        
        
        protected override void OnNcMouseChanged(NCMouseEventArgs e)
        {
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
            if (chg) this.InvalidateWindow();
            
            if (e.MouseButtons.Left && e.InitialResult == NCHitTestEventArgs.Results.HTBORDER
                && (Math.Abs(e.Delta.X) >= 2 || Math.Abs(e.Delta.Y) >= 2) )
            {        
                bool candrag = MouseOnSelector(e.ControlPosition);

                if (candrag)
                {
                    //Console.WriteLine("Start floating " + Text + ": " + e.Delta + ", " + e.MouseButtons + ", " + e.InitialResult);
                    Float(e);
                }
                
            }
            //else Console.WriteLine("    " + Text + ": " + e.Delta);
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
        

        protected override void OnNcMouseDown(NCMouseEventArgs e)
        {
            base.OnNcMouseUp(e);

            if (MouseOnSelector(e.ControlPosition) && e.MouseButtons.Left)
            {
                //Console.WriteLine("Selected " + Text);
                this.Focus();
                Manager.RepaintAll();
            }

            DockPanel dp = ButtonData.GetHitPanel(e.ControlPosition);
            if (dp != null)
            {
                if (e.MouseButtons.Right)
                {
                    if (dp.DockContainer != null) if (dp.DockContainer.Collapsed) dp.DockContainer.Expand();
                    else dp.DockContainer.Collapse();
                }
                else if (e.MouseButtons.Left)
                {
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
            {                
                //Console.WriteLine("Changed Highlight to " + this.Text);

                DockContainer.ShowDockPanel(this);
                DockContainer.Highlight = this;
            }
        }

        /// <summary>
        /// Force a complete redraw of the Panel
        /// </summary>
        public void InvalidateWindow()
        {
            DoInvalidateWindow();
        }

        private void Float(NCMouseEventArgs e)
        {
            if (this.Parent is DockPanelFloatingForm) return;

            this.NonClientMargin = new Padding(0);

            DockPanelFloatingForm frm = new DockPanelFloatingForm(this);
            frm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            frm.StartPosition = FormStartPosition.Manual;
            frm.ShowInTaskbar = false;
            frm.Width = this.Width + 2 * System.Windows.Forms.SystemInformation.FrameBorderSize.Width;
            frm.Height = this.Height + System.Windows.Forms.SystemInformation.ToolWindowCaptionHeight + 2 * System.Windows.Forms.SystemInformation.FrameBorderSize.Height;
            Point scr = e.ScreenPosition; // PointToScreen(new Point(e.X, e.Y));

            frm.Left = scr.X - e.ControlPosition.X - System.Windows.Forms.SystemInformation.FrameBorderSize.Width;
            frm.Top = scr.Y - e.ControlPosition.Y - System.Windows.Forms.SystemInformation.FrameBorderSize.Height;
            frm.BringToFront();

            DockContainer dc = Parent as DockContainer;
            if (dc != null) dc.RemoveDock(this);

            this.Parent = frm;
            this.Dock = DockStyle.Fill;
            this.ResetNCMouseState();
            Manager.StartDockMode(this);

            frm.Show();
            frm.StartFloatingBlocked(this);
        }                

        internal void DockControl(DockContainer parent)
        {                                   
            DockPanelFloatingForm f = this.Parent as DockPanelFloatingForm;

            if (f!=null) 
            {
                this.Parent = null;
                f.Close();
                f.Dispose();                
            }

            this.NonClientMargin = manager.Renderer.DockPanelRenderer.GetPanelBorderSize(BestOrientation);
            parent.AddDock(this);
        }

        internal void UnFloat(DockPanelFloatingForm f)
        {            
            Manager.StopDockMode(this);
            this.Refresh();
        }

        public void Close()
        {
            DockContainer dc = this.DockContainer;
            if (dc != null)
            {
                dc.RemoveDock(this);
                Manager.Update();
            }

            DockPanelFloatingForm f = this.Parent as DockPanelFloatingForm;
            if (f != null)
            {
                f.Close();
            }

        }

        public void Collapse()
        {
            if (DockContainer != null) DockContainer.Collapse();
        }

        public void Expand()
        {
            if (DockContainer != null) DockContainer.Expand();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Brush b = new SolidBrush(Color.White);
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
            p.Dispose();
        }


        DockPanelButtonManager buttonData;
        protected DockPanelButtonManager ButtonData
        {
            get { return buttonData; }
        }

        protected override void OnNcPaint(NCPaintEventArgs e)
        {
            //base.OnNcPaint(e);
            
            e.Graphics.FillRegion(new SolidBrush(manager.Renderer.ColorTable.DockBackgroundColor), e.PaintRegion);
            
            buttonData = Manager.Renderer.DockPanelRenderer.ConstructButtonData(DockContainer, e);
            buttonData.Render();
            
            Manager.Renderer.DockPanelRenderer.RenderCaption(this, e);
            foreach (DockPanelCaptionButton b in cbuttons) b.Render(e);
            Manager.Renderer.DockPanelRenderer.RenderBorder(this, e);
        }
        
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
                if (close.SetVisible(value)) InvalidateWindow();                
            }
        }

        public bool ShowCollapseButton
        {
            get { return collapse.Visible; }
            set {
                if (collapse.SetVisible(value)) InvalidateWindow();                
            }
        }

        /// <summary>
        /// Returns the state this dock is currently in
        /// </summary> 
        internal CaptionState CaptionState
        {
            get
            {
                bool foc = this.Focused;
                if (DockContainer != null) foc |= DockContainer.Focused;
                if (foc) return CaptionState.Focused;
                else return CaptionState.Normal;
            }
        }
    }
}
