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

    

    public abstract partial class NCUserControl : UserControl
    {
        
        Padding ncsz;
        NCButtons mb;
        static Point lastsp;
        
        public NCUserControl()
        {
            doublebuffer = true;
            ncsz = new Padding(6, 6, 6, 20);
            mb = new NCButtons();
            InitializeComponent();

            ersz = new NCResizeBorders();
            dborder = true;
        }

        NCResizeBorders ersz;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("Set all Borders to true, that should allow the User to resize this Control.")]
        public NCResizeBorders ResizeBorder
        {
            get { return ersz; }
        }

        bool dborder;
        [Description("true, if you want allow users to drag this Control at runtime, by holding the mouse down over the border")]
        public bool DragBorder
        {
            get { return dborder; }
            set { dborder = value; }
        }

        protected Padding NonClientMargin
        {
            get { return ncsz; }
            set
            {
                if (value != ncsz)
                {
                    //Console.WriteLine("Set Margin to " + value);
                    ncsz = value;
                    this.Width += 1;
                    this.Width -= 1;
                }
            }
        }

        bool doublebuffer;
        protected bool DoubleBuffer
        {
            get { return doublebuffer; }
            set { doublebuffer = value; }
        }

        // non-client stuff:
        //     - http://www.syncfusion.com/FAQ/WindowsForms/FAQ_c41c.aspx#q1026q

        protected void DoInvalidateWindow()
        {
            Rectangle cr = this.ClientRectangle;
            APIHelp.RECT rc = new APIHelp.RECT(0, 0, Width, Height);
            APIHelp.RedrawWindow(this.Handle, ref rc, IntPtr.Zero,
                 APIHelp.RDW_FRAME | APIHelp.RDW_UPDATENOW | APIHelp.RDW_INVALIDATE);
        } 

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case APIHelp.WM_NCCALCSIZE:
                    {
                        if (m.WParam.ToInt32() == 0)
                        {
                            APIHelp.RECT rc = (APIHelp.RECT)m.GetLParam(typeof(APIHelp.RECT));
                            rc.Left += ncsz.Left;
                            rc.Top += ncsz.Top;
                            rc.Right -= ncsz.Right;
                            rc.Bottom -= ncsz.Bottom;
                            Marshal.StructureToPtr(rc, m.LParam, true);
                            m.Result = IntPtr.Zero;
                        }
                        else
                        {
                            APIHelp.NCCALCSIZE_PARAMS csp;
                            csp = (APIHelp.NCCALCSIZE_PARAMS)m.GetLParam(typeof(APIHelp.NCCALCSIZE_PARAMS));
                            csp.rgrc0.Top += ncsz.Top;
                            csp.rgrc0.Bottom -= ncsz.Bottom;
                            csp.rgrc0.Left += ncsz.Left;
                            csp.rgrc0.Right -= ncsz.Right;
                            Marshal.StructureToPtr(csp, m.LParam, true);
                            //Return zero to preserve client rectangle
                            m.Result = IntPtr.Zero;
                        }
                        break;
                    }
                case APIHelp.WM_NCPAINT:
                    {
                        m.WParam = NCPaint(m.WParam);
                        break;
                    }
                case APIHelp.WM_NCLBUTTONDOWN:
                case APIHelp.WM_NCLBUTTONUP:
                case APIHelp.WM_NCRBUTTONDOWN:
                case APIHelp.WM_NCRBUTTONUP:
                case APIHelp.WM_NCMBUTTONDOWN:
                case APIHelp.WM_NCMBUTTONUP:
                case APIHelp.WM_NCMOUSEMOVE:
                    {
                        NCMouseEventArgs e = GetMouseParams(ref m, true);
                        if (m.Msg == APIHelp.WM_NCLBUTTONUP || m.Msg == APIHelp.WM_NCMBUTTONUP || m.Msg == APIHelp.WM_NCRBUTTONUP)
                            this.OnNcMouseUp(e);

                        //Console.WriteLine("Mouse 0x" + m.Msg.ToString("X") + " " + m.WParam);
                        /*if (m.Msg == APIHelp.WM_NCLBUTTONDOWN) mb.LeftInt = true;
                        else if (m.Msg == APIHelp.WM_NCLBUTTONUP) mb.LeftInt = false;
                        else if (m.Msg == APIHelp.WM_NCRBUTTONDOWN) mb.RightInt = true;
                        else if (m.Msg == APIHelp.WM_NCRBUTTONUP) mb.RightInt = false;
                        else if (m.Msg == APIHelp.WM_NCMBUTTONDOWN) mb.MiddleInt = true;
                        else if (m.Msg == APIHelp.WM_NCMBUTTONUP) mb.MiddleInt = false;*/
                        GetMouseButtonState();
                        if ((!mb.Left && !mb.Right && !mb.Middle) && (m.Msg == APIHelp.WM_NCLBUTTONUP || m.Msg == APIHelp.WM_NCMBUTTONUP || m.Msg == APIHelp.WM_NCRBUTTONUP))
                            this.OnNcClick(e);
                        
                        if (m.Msg == APIHelp.WM_NCLBUTTONDOWN || m.Msg == APIHelp.WM_NCMBUTTONDOWN || m.Msg == APIHelp.WM_NCRBUTTONDOWN)
                            this.OnNcMouseDown(e);
                        
                        
                        this.OnNcMouseChanged(e);
                        break;
                    }
                case APIHelp.WM_NCLBUTTONDBLCLK:
                case APIHelp.WM_NCRBUTTONDBLCLK:
                case APIHelp.WM_NCMBUTTONDBLCLK:
                    {
                        NCButtons b = new NCButtons();
                        /*if (m.Msg == APIHelp.WM_NCLBUTTONDBLCLK) b.LeftInt = true;
                        else if (m.Msg == APIHelp.WM_NCMBUTTONDBLCLK) b.MiddleInt = true;
                        else if (m.Msg == APIHelp.WM_NCRBUTTONDBLCLK) b.RightInt = true;*/
                        GetMouseButtonState();

                        NCMouseEventArgs e = GetMouseParams(ref m, false, b);
                        this.OnNcDoubleClick(e);
                        break;
                    }
                case APIHelp.WM_NCMOUSEHOVER:
                    {
                        NCMouseEventArgs e = GetMouseParams(ref m, false);
                        this.OnNcMouseHover(e);
                        break;
                    }
                case APIHelp.WM_NCMOUSELEAVE:
                    {
                        NCMouseEventArgs e = GetMouseParams(ref m, false);
                        this.OnNcMouseLeave(e);
                        break;
                    }
                case APIHelp.WM_NCHITTEST:
                    {
                        base.WndProc(ref m);
                        NCHitTestEventArgs e = GetHitTestParams(ref m);
                        
                        DoNcHitTest(e);
                        m.Result = e.GetResult();
                        return;
                    }
            }
            base.WndProc(ref m);
        }

        private void GetMouseButtonState()
        {
            mb.LeftInt = APIHelp.GetKeyState(APIHelp.VirtualKeyStates.VK_LBUTTON) < 0;
            mb.MiddleInt = APIHelp.GetKeyState(APIHelp.VirtualKeyStates.VK_MBUTTON) < 0;
            mb.RightInt = APIHelp.GetKeyState(APIHelp.VirtualKeyStates.VK_RBUTTON) < 0;
        }

        protected void ResetNCMouseState()
        {
            mb.Reset();
        }
       
        private NCHitTestEventArgs GetHitTestParams(ref Message m)
        {
            Point screenPoint; Point pt; 
            m = GetNCMessageParams(m, out screenPoint, out pt);
            NCHitTestEventArgs e = new NCHitTestEventArgs(screenPoint, pt, new Point(pt.X + ncsz.Left, pt.Y + ncsz.Top), new Point(0), m.Result, mb);
            return e;
        }

        internal NCMouseEventArgs CallGetMouseParams(ref Message m, bool getdelta)
        {
            GetMouseButtonState();
            return GetMouseParams(ref m, getdelta);
        }

        private NCMouseEventArgs GetMouseParams(ref Message m, bool getdelta)
        {
            return GetMouseParams(ref m, getdelta, mb);
        }

        private NCMouseEventArgs GetMouseParams(ref Message m, bool getdelta, NCButtons mb)
        {
            Point screenPoint; Point pt; 
            m = GetNCMessageParams(m, out screenPoint, out pt);

            Point delta = new Point(0);
            if (getdelta)
            {                
                delta = new Point(screenPoint.X - lastsp.X, screenPoint.Y - lastsp.Y);
                lastsp = screenPoint;
            }            
            

            NCMouseEventArgs e = new NCMouseEventArgs(m.WParam, screenPoint, pt, new Point(pt.X + ncsz.Left, pt.Y + ncsz.Top), delta, mb);
            return e;
        }

        private Message GetNCMessageParams(Message m, out Point screenPoint, out Point pt)
        {
            int lparam = m.LParam.ToInt32();
            screenPoint = new Point(lparam & 0xFFFF, lparam >> 16);

            pt = PointToClient(screenPoint);
            
            return m;
        }
        internal IntPtr CallNCPaint(IntPtr reg)
        {
            return NCPaint(reg);
        }
        
        protected IntPtr NCPaint(IntPtr region)
        {
            IntPtr hDC = APIHelp.GetWindowDC(this.Handle);
            if (hDC != IntPtr.Zero)
            {
                Graphics grTemp = Graphics.FromHdc(hDC);

                int ScrollBarWidth = SystemInformation.VerticalScrollBarWidth;
                int ScrollBarHeight = SystemInformation.HorizontalScrollBarHeight;

                Region UpdateRegion;
                Rectangle WindowRect = GetWindowRectangle(out UpdateRegion);

                IntPtr hRgn = UpdateRegion.GetHrgn(grTemp);
                Point offset = Point.Empty - (Size)WindowRect.Location;

                WindowRect.Offset(offset);
                                
                Rectangle ClientRect = GetClientArea();
                
                //Fill the BorderArea
                Region PaintRegion = new Region(WindowRect);
                PaintRegion.Exclude(ClientRect);


                //Fill the Area between the scrollbars
                if (this.HScroll && this.VScroll)
                {
                    Rectangle ScrollRect = new Rectangle(ClientRect.Right - ScrollBarWidth,
                            ClientRect.Bottom - ScrollBarHeight, ScrollBarWidth + 2, ScrollBarHeight + 2);
                    ScrollRect.Offset(-1, -1);
                }

                if (doublebuffer)
                {
                    Bitmap bmp = new Bitmap(WindowRect.Width, WindowRect.Height);
                    Graphics g = Graphics.FromImage(bmp);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                    NCPaintEventArgs e = new NCPaintEventArgs(g, ClientRectangle, WindowRect, PaintRegion);
                    this.OnNcPaint(e);
                    grTemp.DrawImage(bmp, 0, 0);
                    g.Dispose();
                    bmp.Dispose();
                }
                else
                {
                    NCPaintEventArgs e = new NCPaintEventArgs(grTemp, ClientRectangle, WindowRect, PaintRegion);
                    this.OnNcPaint(e);
                }

                APIHelp.ReleaseDC(Handle, hDC);
                grTemp.Dispose();

                return hRgn;

            }

            return region;
        }

        /// <summary>
        /// Returns the Rectangle of the space, this Control occupies
        /// </summary>
        /// <returns></returns>
        private Rectangle GetWindowRectangle()
        {
            Region UpdateRegion;
            return GetWindowRectangle(out UpdateRegion);
        }

        /// <summary>
        /// Returns the Rectangle of the space, this Control occupies
        /// </summary>
        /// <param name="UpdateRegion"></param>
        /// <returns></returns>
        private Rectangle GetWindowRectangle(out Region UpdateRegion)
        {            
            //Bounds is unreliable as it often reports the incorrect
            //location, especially when part of the window is OffScreen.
            //So we'll use GetWindowInfo as it returns all the info we need.
            APIHelp.WINDOWINFO wi = new APIHelp.WINDOWINFO();
            wi.cbSize = (uint)Marshal.SizeOf(wi);
            APIHelp.GetWindowInfo(Handle, ref wi);

            wi.rcClient.Right--;
            wi.rcClient.Bottom--;

            //Define a Clip Region to pass back to WM_NCPAINTs wParam.
            //Must be in Screen Coordinates, which is what GetWindowInfo returns.
            UpdateRegion = new Region(wi.rcWindow.ToRectangle());
            UpdateRegion.Exclude(wi.rcClient.ToRectangle());

            if (this.HScroll && this.VScroll)
                UpdateRegion.Exclude(Rectangle.FromLTRB
                        (wi.rcClient.Right + 1, wi.rcClient.Bottom + 1,
                        wi.rcWindow.Right, wi.rcWindow.Bottom));



            //For Painting we need to zero offset the Rectangles.
            return  wi.rcWindow.ToRectangle();
        }

        protected virtual void OnNcPaint(NCPaintEventArgs e)
        {
            e.Graphics.FillRegion(SystemBrushes.AppWorkspace, e.PaintRegion);           
        }

        public Rectangle GetClientArea()
        {
            int ScrollBarWidth = SystemInformation.VerticalScrollBarWidth;
            int ScrollBarHeight = SystemInformation.HorizontalScrollBarHeight;
            Rectangle WindowRect = GetWindowRectangle();
            Rectangle cr = new Rectangle(ncsz.Left, ncsz.Top, WindowRect.Width - ncsz.Horizontal, WindowRect.Height - ncsz.Vertical);
            /*if (this.HScroll) cr.Width -= ScrollBarWidth;
            if (this.VScroll) cr.Height -= ScrollBarHeight;*/
            return cr;
        }        

        void DoNcHitTest(NCHitTestEventArgs e)
        {
            Rectangle cr = GetClientArea();


            if (!cr.Contains(e.ControlPosition))
            {
                e.Result = NCHitTestEventArgs.Results.HTBORDER;
                bool l = e.ControlPosition.X < 2 && ersz.Left;
                bool t = e.ControlPosition.Y < 2 && ersz.Top;
                bool r = e.ControlPosition.X > Width - 3 && ersz.Right;
                bool b = e.ControlPosition.Y > Height - 3 && ersz.Bottom;

                if (l && t) e.Result = NCHitTestEventArgs.Results.HTTOPLEFT;
                else if (r && t) e.Result = NCHitTestEventArgs.Results.HTTOPRIGHT;
                else if (r && b) e.Result = NCHitTestEventArgs.Results.HTBOTTOMRIGHT;
                else if (l && b) e.Result = NCHitTestEventArgs.Results.HTBOTTOMLEFT;
                else if (l) e.Result = NCHitTestEventArgs.Results.HTLEFT;
                else if (t) e.Result = NCHitTestEventArgs.Results.HTTOP;
                else if (r) e.Result = NCHitTestEventArgs.Results.HTRIGHT;
                else if (b) e.Result = NCHitTestEventArgs.Results.HTBOTTOM;
            }
            else
            {
                if (e.Result != NCHitTestEventArgs.Results.HTVSCROLL && e.Result != NCHitTestEventArgs.Results.HTHSCROLL)
                    e.Result = NCHitTestEventArgs.Results.HTCLIENT;
                else
                    mb.Reset();
            }
            

            OnNcHitTest(e);
        }

        
        protected virtual void OnNcMouseChanged(NCMouseEventArgs e)
        {
            //Console.WriteLine("NCMouse: " + e);
        }

        protected virtual void OnNcMouseDown(NCMouseEventArgs e)
        {
            //Console.WriteLine("NCDown: " + e);
        }

        protected virtual void OnNcMouseUp(NCMouseEventArgs e)
        {
           //Console.WriteLine("NCUp: " + e);
           
        }

        protected virtual void OnNcClick(NCMouseEventArgs e)
        {
           // //Console.WriteLine("NCClick: " + e);
        }

        protected virtual void OnNcMouseLeave(NCMouseEventArgs e)
        {
            //Console.WriteLine("NCLv: " + e);
        }

        protected virtual void OnNcMouseHover(NCMouseEventArgs e)
        {
            //Console.WriteLine("NCHv: " + e);
        }

        protected virtual void OnNcDoubleClick(NCMouseEventArgs e)
        {
            //Console.WriteLine("NCDbl: " + e);
        }


        protected virtual void OnNcHitTest(NCHitTestEventArgs e)
        {
            if (dborder && e.Result == NCHitTestEventArgs.Results.HTBORDER) e.Result = NCHitTestEventArgs.Results.HTCAPTION;
            //Console.WriteLine("NCHit: " + e);            
        }        
    }
}
