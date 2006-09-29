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

namespace Floaters
{
    public partial class DockButtonBar : UserControl, IButtonContainer
    {
        public class DockPanelList : List<DockPanel>{}

        DockPanelList panels;
        Dictionary<DockContainer, DockPanelList> containers;
        DockManager manager;

        public DockButtonBar(DockManager manager)
        {
            InitializeComponent();

            this.manager = manager;
            panels = new DockPanelList();
            containers = new Dictionary<DockContainer, DockPanelList>();
        }


        public DockManager Manager
        {
            get { return manager; }
        }

        protected void SetVisibleState()
        {
            bool v= panels.Count > 0;
            if (v == this.Visible)
            {
                this.Refresh();
            }
            else
            {
                this.Width = Manager.Renderer.DockPanelRenderer.Dimension.Buttons;
                this.Height = this.Width;                
            }
            this.Visible = v;
        }

        public void Add(DockContainer c)
        {
            DockPanelList dp = c.GetDockedPanels();
            containers[c] = dp;

            foreach (DockPanel p in dp)
            {
                p.SeperateInDockBar = false;
                if (!panels.Contains(p)) panels.Add(p);
            }
            if (panels.Count > 0) panels[panels.Count - 1].SeperateInDockBar = true;

            SetVisibleState();
        }

        public void Remove(DockContainer c)
        {
            DockPanelList dp = containers[c];
            if (dp != null)
            {
                containers.Remove(c);
                foreach (DockPanel p in dp)
                {
                    p.SeperateInDockBar = false;
                    panels.Remove(p);
                }
            }
            SetVisibleState();
        }

        DockContainer FindDock(DockPanel p)
        {
            foreach (DockContainer dc in containers.Keys)
            {
                DockPanelList dp = containers[dc];
                foreach (DockPanel pp in dp)
                    if (pp == p) return dc;
            }

            return null;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (ButtonData==null) return;

            if (e.Button == MouseButtons.Left)
            {
                Point mouse = new Point(e.X, e.Y);
                
                DockPanel dp = ButtonData.GetHitPanel(mouse);
                if (dp != null)
                {
                    DockContainer dc = FindDock(dp);
                    if (dc != null)
                    {
                        dc.Expand();
                        dp.EnsureVisible();
                    }
                }
            }
        }



        DockPanelButtonManager buttonData;
        protected DockPanelButtonManager ButtonData
        {
            get { return buttonData; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            NCPaintEventArgs ee = new NCPaintEventArgs(e.Graphics, this.ClientRectangle, this.Bounds, null);
            buttonData = Manager.Renderer.DockPanelRenderer.ConstructButtonData(this, ee);
            buttonData.Render();            
        }

        #region IButtonContainer Member

        public DockPanel Highlight
        {
            get { return null; }
            set { }
        }
        public DockButtonBar.DockPanelList GetButtons()
        {
            return this.panels;
        }
        public ButtonOrientation BestOrientation
        {
            get
            {
                if (Dock == DockStyle.Bottom) return ButtonOrientation.Top;
                if (Dock == DockStyle.Left) return ButtonOrientation.Right;
                if (Dock == DockStyle.Top) return ButtonOrientation.Bottom;
                return ButtonOrientation.Left;
            }
        }
        public Padding GetBorderSize(ButtonOrientation orient)
        {
            return manager.Renderer.DockPanelRenderer.GetBarBorderSize(orient);
        }

        #endregion
    }
}
