using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Floaters
{
    public class NCPaintEventArgs : EventArgs
    {
        Rectangle clientRect, windowRect;
        Region paintRegion;
        Graphics gr;
        public NCPaintEventArgs(Graphics g, Rectangle cr, Rectangle wr, Region pr)
        {
            this.gr = g;
            this.clientRect = cr;
            this.windowRect = wr;
            this.paintRegion = pr;
        }

        public Graphics Graphics
        {
            get { return gr; }
        }

        public Rectangle ClientRectangle
        {
            get { return clientRect; }
        }

        public Rectangle WindowRectangle
        {
            get { return windowRect; }
        }

        public Region PaintRegion
        {
            get { return this.paintRegion; }
        }
    }
}
