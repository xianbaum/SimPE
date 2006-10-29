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
    public abstract class BaseDockManager : DockContainer
    {
        BaseRenderer rnd;
        public BaseRenderer Renderer
        {
            get { return rnd; }
            set { rnd = value; }
        }

        public BaseDockManager(BaseRenderer renderer)
            : base(null)
        {
            this.rnd = renderer;
            floatingpanels = new DockButtonBar.DockPanelList();
        }

        protected bool dockmode;
        public bool DockMode
        {
            get { return dockmode; }
        }

        protected DockButtonBar.DockPanelList floatingpanels;
        internal void NotifyFloating(DockPanel dp)
        {
            if (dp.Floating && !floatingpanels.Contains(dp)) floatingpanels.Add(dp);
            else if (!dp.Floating && floatingpanels.Contains(dp)) floatingpanels.Remove(dp);
        }

        internal abstract void StartDockMode(DockPanel dock);
        internal abstract void StopDockMode(DockPanel dock);
        internal abstract void MouseMoved(Point scrpt);
        protected abstract bool MeAsCenterDock
        {
            get;
        }

        internal virtual void DockPanelInt(DockPanel dp, DockStyle style)
        {
            bool docked = false;
            this.SuspendLayout();

            if (style == DockStyle.Fill && MeAsCenterDock)
            {
                docked = true;
                dp.DockControl(this);
                
                this.ResumeLayout();
                return;
            }

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
    }
}
