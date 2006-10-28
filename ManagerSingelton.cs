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
        DockButtonBar.DockPanelList known;
        List<DockPanelFloatingForm> knownf;
        ManagerSingelton()
        {
            topmostfloats = false;
            pnid = 0;
            dm = null;
            known = new DockButtonBar.DockPanelList();
            startdrag = null;
            Application.AddMessageFilter(this);

            dock = new GlossyRenderer();
            knownf = new List<DockPanelFloatingForm>();
            tab = new WhidbeyTabRenderer();
        }

        ~ManagerSingelton()
        {
            Application.RemoveMessageFilter(this);
        }

        bool topmostfloats;

        public bool TopmostFloats
        {
            get { return topmostfloats; }
            set { topmostfloats = value; }
        }

        BaseRenderer dock, tab;
        public BaseRenderer DockRenderer
        {
            get { return dock; }
        }

        public BaseRenderer TabRenderer
        {
            get { return tab; }
        }

        DockManager dm;
        internal void SetMainManager(DockManager m)
        {
            if (dm==null)
                dm = m;
        }
        public DockManager MainDockManager
        {
            get { return dm; }
        }

        #region IMessageFilter Member
        internal void AddFloatForm(DockPanelFloatingForm f)
        {
            if (!knownf.Contains(f))
            {
                knownf.Add(f);
                f.Disposed += new EventHandler(f_Disposed);                
            }
        }

        void f_Disposed(object sender, EventArgs e)
        {
            DockPanelFloatingForm f = sender as DockPanelFloatingForm;
            RemoveFloatForm(f);
        }

        internal void RemoveFloatForm(DockPanelFloatingForm f)
        {
            if (knownf.Contains(f))
            {
                f.Disposed -= new EventHandler(f_Disposed);
                knownf.Remove(f);
            }
        }

        int pnid;
        internal void AddPanel(DockPanel dp)
        {
            if (!known.Contains(dp))
            {
                known.Add(dp);
                dp.Disposed += new EventHandler(dp_Disposed);
                if (dp.Name == "")
                {
                    dp.Name = "ManagedDockPanel" + pnid;
                    pnid++;
                }
            }
        }

        void dp_Disposed(object sender, EventArgs e)
        {
            DockPanel dp = sender as DockPanel;
            RemovePanel(dp);
        }

        internal void RemovePanel(DockPanel dp)
        {
            if (known.Contains(dp))
            {
                dp.Disposed -= new EventHandler(dp_Disposed);
                known.Remove(dp);
            }
        }

        public DockPanel GetPanelWithName(string name)
        {
            foreach (DockPanel dp in known)
                if (dp.Name == name) return dp;

            return null;
        }

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
            
            if (m.Msg == APIHelp.WM_ACTIVATEAPP )
            {
                Console.WriteLine(m.Msg.ToString("X") + " " + m.WParam + " " + m.LParam);
                foreach(DockPanelFloatingForm f in knownf)
                    f.SendeActivateEvent((int)m.WParam != 0);
            }
            else if (m.Msg == APIHelp.WM_ACTIVATEAPP_EXT)
            {
                Console.WriteLine(m.Msg.ToString("X") + " " + m.WParam + " " + m.LParam);
                if (m.LParam.ToInt32() == 0 && (m.WParam.ToInt32()==1 || m.WParam.ToInt32()==0))
                {
                    foreach (DockPanelFloatingForm f in knownf)
                        f.SendeActivateEvent((int)m.WParam == 0);
                }
            }

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
