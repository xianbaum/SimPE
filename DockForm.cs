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
    class DockPanelFloatingForm : Form
    {
        DockPanel dock;

        public DockPanel DockControl
        {
            get { return dock; }
        }

        public DockManager Manager
        {
            get { return DockControl.Manager; }
        }
       
        public DockPanelFloatingForm(DockPanel dock) 
            : base()
        {
            this.dock = dock;
        }

        protected override void WndProc(ref Message m)
        {

            //Console.WriteLine("#### #" + m.Msg.ToString("X") + " " + m.WParam.ToString("X") + " " + m.LParam.ToString("X") + " " + m.Result);                
            if (m.Msg == APIHelp.WM_NCMOUSEMOVE || m.Msg == APIHelp.WM_EXITSIZEMOVE)
            {
                //Console.WriteLine("#### Stop floating " + m);
                if (Manager.DockMode) StopFloating();
            }
            else if (m.Msg == APIHelp.WM_MOVING)
            {
                APIHelp.RECT rc = (APIHelp.RECT)m.GetLParam(typeof(APIHelp.RECT));
                FireLocationChangeEvent();
            }
            
            
            base.WndProc(ref m);
        }              

       
        internal void StartFloatingBlocked(DockPanel p)
        {
            //Console.WriteLine("#### Start floating blocked " + p.Text);
            StartFloating();
            this.Text = p.CaptionText;
            APIHelp.ReleaseCapture();

            //this call will block the calling thread
            APIHelp.SendMessage(Handle, APIHelp.WM_NCLBUTTONDOWN, APIHelp.HTCAPTION, 0);             
                        
            StopFloating();
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
            DockControl.UnFloat(this);
            OnStopFloating();
        }

        protected virtual void OnStopFloating()
        {
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            //Console.WriteLine("#### Form MouseUp " + e);
            if (Manager.DockMode) StopFloating();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);

            //Console.WriteLine("#### Change Form Location " + e);
            FireLocationChangeEvent();
        }

        private void FireLocationChangeEvent()
        {
            if (!Manager.DockMode && Visible) Manager.StartDockMode(this.DockControl);
            Manager.MouseMoved(Cursor.Position);
        }       
    }
}
