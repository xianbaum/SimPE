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
    [Designer(typeof(DockContainerDesigner)), ToolboxItem(true), ToolboxBitmap(typeof(DockManager), "Floaters.dockimg.png")]
    public class TabControl : BaseDockManager
    {
         
        public TabControl()
            : base(ManagerSingelton.Global.TabRenderer)
        {
        }

        #region Disable crtitical settings
        
        #endregion

        protected override void CleanUp()
        {
            //base.CleanUp();
        }

        protected override void OnControlAdded(System.Windows.Forms.ControlEventArgs e)
        {
            TabPage tp = e.Control as TabPage;            
            if (tp==null) throw new Exception("You can only add TabPage Controls to a TabControl! (tried to add "+e.Control.GetType().Name+")");            
            base.OnControlAdded(e);
            
        }

        /// <summary>
        /// True, if this <see cref="DockContainer"/> does only contain one <see cref="DockPanel"/>
        /// </summary>
        public override bool OneChild
        {
            get { return false; }
        }        

        internal override void StartDockMode(DockPanel dock)
        {
            //dockmode = true;
        }

        internal override void StopDockMode(DockPanel dock)
        {
            //if (dockmode)
            {
                AddPage(dock as TabPage);
            }
        }

        internal override void MouseMoved(Point scrpt)
        {
            
        }

        protected override bool MeAsCenterDock
        {
            get { return true; }
        }

        public void AddPage(TabPage tp)
        {
            this.DockPanelInt(tp, DockStyle.Fill);            
        }

        public override void Collapse(bool animated)
        {            
        }

        public override void Expand(bool animated)
        {            
        }
    }
}
