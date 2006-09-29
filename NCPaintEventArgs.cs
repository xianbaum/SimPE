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
