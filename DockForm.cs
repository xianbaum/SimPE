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
using System.Runtime.InteropServices;
using System.Drawing;

namespace Ambertation.Windows.Forms
{
    class DockPanelFloatingForm : Form, IMessageFilter
    {
        DockPanel dock;

        public DockPanel DockControl
        {
            get { return dock; }
        }

        public DockManager Manager
        {
            get {
                if (DockControl == null) return null;
                return DockControl.Manager; 
            }
        }
       
        public DockPanelFloatingForm(DockPanel dock) 
            : base()
        {
            Application.AddMessageFilter(this);
            this.TopMost = true;
            this.dock = dock;
        }

        ~DockPanelFloatingForm()
        {
                       
        }

        DockContainer cnt;
        public void DragContainerAlong(DockContainer cnt)
        {
            this.cnt = cnt;
            foreach (DockPanel dp in cnt.GetDockedPanels())
                dp.RefreshMargin();
        }

        public bool HasContainer
        {
            get { return cnt != null; }
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.HWnd == Handle)
            {
                if (m.Msg == APIHelp.WM_ACTIVATEAPP)
                    OnActivateApplication((int)m.WParam != 0);
            }
            return false;
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            
            //Console.WriteLine("#### #" + m.Msg.ToString("X") + " " + m.WParam.ToString("X") + " " + m.LParam.ToString("X") + " " + m.Result);                
            if (m.Msg == APIHelp.WM_NCLBUTTONUP || m.Msg == APIHelp.WM_EXITSIZEMOVE)
            {
                //Console.WriteLine("#### Stop floating " + m);
                if (Manager!=null) 
                    if (Manager.DockMode) StopFloating();
                base.WndProc(ref m);
            }
            else if (m.Msg == APIHelp.WM_ENTERSIZEMOVE)
            {
                //Console.WriteLine("Start Fire");
                FireLocationChangeEvent();
                base.WndProc(ref m);
            }
            else if (m.Msg == APIHelp.WM_NCHITTEST)
            {
                base.WndProc(ref m);                
            }
            else if (m.Msg == APIHelp.WM_MOVING)
            {
                APIHelp.RECT rc = (APIHelp.RECT)m.GetLParam(typeof(APIHelp.RECT));
                FireLocationChangeEvent();
                base.WndProc(ref m);
            }
            else if (m.Msg == APIHelp.WM_ACTIVATEAPP)
            {
                OnActivateApplication((int)m.WParam != 0);
                m.Result = new IntPtr(0);
            }
            else base.WndProc(ref m);
        }

         protected virtual void OnActivateApplication(bool active)
        {
            //Console.WriteLine("Activate Application " + active);
            if (active) this.TopMost = true;
            else TopMost = false;
        }

               
        internal void StartFloatingBlocked(DockPanel p)
        {
            //Console.WriteLine("#### Start floating blocked " + p.Text);
            StartFloating();
            this.Text = p.CaptionText;
            APIHelp.ReleaseCapture(Handle);

            //this call will block the calling thread
            APIHelp.SendMessage(Handle, APIHelp.WM_NCLBUTTONDOWN, APIHelp.HTCAPTION, 0);             
                        
            StopFloating();
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            if (Manager == null) Close();

            if (Controls.Count == 0 && HasContainer && !Manager.DockMode)
            {
                dock = null;
                Close();
            }
        }

        protected void StartFloating()
        {
            OnStartFloating();
        }

        protected virtual void OnStartFloating()
        {
        }

       
        protected void StopFloating()
        {
            if (dock == null) return;
            //Console.WriteLine("Stop Float "+dock.Name+" "+Controls.Count);
            TopMost = false;
            if (HasContainer)
            {
                DockControl.UnFloat(this);
                if (cnt.GetDockedPanels().Count == 0)
                {
                    cnt.Parent = null;
                    dock = null;
                }
            }
            else
            {
                DockControl.UnFloat(this);
            }
            OnStopFloating();
            if (Controls.Count == 0) dock = null;
            if (dock == null) Close();
            //Console.WriteLine("Stoped Float "  + " " + Controls.Count);
        }

        protected virtual void OnStopFloating()
        {
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            //Console.WriteLine("#### Form MouseUp " + e);
            if (Manager!=null)
                if (Manager.DockMode)
                {
                    APIHelp.ReleaseCapture(Handle);
                    StopFloating();
                }
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);

            //Console.WriteLine("#### Change Form Location " + e);
            FireLocationChangeEvent();
        }

        private void FireLocationChangeEvent()
        {
            //Console.WriteLine("test");
            if (Manager != null)
            {
                //Console.WriteLine("test 2 "+Manager.DockMode + " "+Visible);
                if (!Manager.DockMode && Visible)
                {
                    //Console.WriteLine("Startup");
                    Manager.StartDockMode(this.DockControl);
                }
                Manager.MouseMoved(Cursor.Position);
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (dock != null) dock.Parent = null;
        }

        protected override void Dispose(bool disposing)
        {
            Application.RemoveMessageFilter(this); 
            base.Dispose(disposing);
        }
  
    }
}
