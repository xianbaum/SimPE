using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Floaters
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
            //System.Diagnostics.Debug.WriteLine(m.Msg.ToString("X") + " " + m.WParam.ToString("X") + " " + m.LParam.ToString("X") + " " + m.Result);                
            if (m.Msg == APIHelp.WM_NCMOUSEMOVE || m.Msg == APIHelp.WM_EXITSIZEMOVE)
                if (Manager.DockMode) StopFloating();
            
            
            base.WndProc(ref m);
        }              

       
        internal void StartFloatingBlocked(DockPanel p)
        {
            StartFloating();
            this.Text = p.Text;
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
            if (Manager.DockMode) StopFloating();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);

            if (!Manager.DockMode && Visible) Manager.StartDockMode(this.DockControl);
            Manager.MouseMoved(Cursor.Position);
        }       
    }
}
