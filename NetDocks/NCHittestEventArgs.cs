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
using System.Drawing;

namespace Ambertation.Windows.Forms
{

    public class NCButtons
    {
        private bool left = false;
        private bool right = false;
        private bool mid = false;

        internal bool LeftInt
        {
            get { return left; }
            set { left = value; }
        }
        internal bool RightInt
        {
            get { return right; }
            set { right = value; }
        }
        internal bool MiddleInt
        {
            get { return mid; }
            set { mid = value; }
        }

        public bool Left
        {
            get { return left; }
        }
        public bool Right
        {
            get { return right; }
        }
        public bool Middle
        {
            get { return mid; }
        }

        internal System.Windows.Forms.MouseButtons ToMouseButtons()
        {
            if (Left) return System.Windows.Forms.MouseButtons.Left;
            if (Right) return System.Windows.Forms.MouseButtons.Right;
            if (Middle) return System.Windows.Forms.MouseButtons.Middle;
            return System.Windows.Forms.MouseButtons.None;
        }

        internal void Reset()
        {
            left = false;
            right = false;
            mid = false;
        }

        public override string ToString()
        {
            if (!Left && !Right && !Middle) return "None";
            string s = "";
            if (Left) s += "Left ";
            if (Right) s += "Right ";
            if (Middle) s += "Middele ";
            return s;
        }
    }

    public class NCHitTestEventArgs : NCMouseEventArgs
    {
        

        public enum Results : int
        {
            HTERROR = -2,
            HTTRANSPARENT = -1,
            HTNOWHERE = 0,
            HTCLIENT = 1,
            HTCAPTION = 2,
            HTSYSMENU = 3,
            HTGROWBOX = 4,
            HTMENU = 5,
            HTHSCROLL = 6,
            HTVSCROLL = 7,
            HTMINBUTTON = 8,
            HTMAXBUTTON = 9,
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17,
            HTBORDER = 18,
            HTOBJECT = 19,
            HTCLOSE = 20,
            HTHELP = 21
        }

         Results res;
        public NCHitTestEventArgs(Point scrpt, Point ctrlpt, Point absctrlpt, Point delta, IntPtr res, NCButtons mb)
            : base(res, scrpt, ctrlpt, absctrlpt, delta,  mb)
        {
            this.res = (Results)res.ToInt32();
        }

        public Results Result
        {
            get { return res; }
            set { res = value; }
        }

        internal IntPtr GetResult()
        {
            return new System.IntPtr((int)Result);
        }

        public override string ToString()
        {
            return base.ToString() + " " + Result;
        }
    }

   

    public class NCMouseEventArgs : EventArgs
    {
        
        Point spt, crpt, ctrlpt, delta;
        NCButtons mb;
        NCHitTestEventArgs.Results ires;

        public NCMouseEventArgs(IntPtr res, Point scrpt, Point ctrlpt, Point absctrlpt, Point delta, NCButtons mb)
        {
            this.spt = scrpt;
            this.crpt = ctrlpt;
            this.ctrlpt = absctrlpt;
            this.delta = delta;
            
            this.mb = mb;
            this.ires = (NCHitTestEventArgs.Results)res.ToInt32();
        }

        public NCButtons MouseButtons
        {
            get { return mb; }
        }

        public Point Delta
        {
            get { return delta; }
        }

        public Point ScreenPosition
        {
            get { return spt; }
        }

        public Point ControlPosition
        {
            get { return ctrlpt; }
        }

        public Point ClientRectanglePosition
        {
            get { return crpt; }
        }

        public NCHitTestEventArgs.Results InitialResult
        {
            get { return ires; }
        }

       
        public override string ToString()
        {
            return ScreenPosition + " " + ControlPosition + " " + ClientRectanglePosition + " " + MouseButtons + " " + Delta + " " + InitialResult;
        }
    }
}
