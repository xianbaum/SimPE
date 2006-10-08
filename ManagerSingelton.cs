using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Ambertation.Windows.Forms
{
    public class ManagerSingelton : IMessageFilter
    {
        static ManagerSingelton glob;

        public static ManagerSingelton Global
        {
            get {
                if (glob == null) glob = new ManagerSingelton();
                return ManagerSingelton.glob; 
            }
            
        }

        DockPanel startdrag;
        NCMouseEventArgs events;
             

        ManagerSingelton()
        {
            startdrag = null;
            Application.AddMessageFilter(this);
        }

        #region IMessageFilter Member


        public void SetDragPanelOnMouseMove(DockPanel p, NCMouseEventArgs e)
        {            
            this.events = e;
            if (startdrag==null) this.startdrag = p;
        }

        public void ResetDragPanelOnMouseMove()
        {
            startdrag = null;
            events = null;
        }

        public bool HasDragPanelForMouseMove
        {
            get { return startdrag != null; }
        }

        public bool PreFilterMessage(ref Message m)
        {
            

            if (startdrag != null)
            {
                if (m.Msg == APIHelp.WM_LBUTTONUP || m.Msg == APIHelp.WM_NCLBUTTONUP)
                    ResetDragPanelOnMouseMove();

                if (m.Msg == APIHelp.WM_MOUSEMOVE)
                {
                    startdrag.StartDockModeFloat(events);
                    ResetDragPanelOnMouseMove();
                }
            }
            return false;
        }

        #endregion
    }
}
