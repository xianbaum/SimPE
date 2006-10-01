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
using System.ComponentModel;

namespace Ambertation.Windows.Forms
{
    [TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public class NCResizeBorders
    {
        public NCResizeBorders() : this(false, false, true, true) { }
        public NCResizeBorders(bool l, bool t, bool r, bool b)
        {
            left = l;
            top = t;
            right = r;
            bottom = b;
        }

        private bool left;
        public bool Left
        {
            get { return left; }
            set { left = value; }
        }

        private bool right;
        public bool Right
        {
            get { return right; }
            set { right = value; }
        }

        private bool top;
        public bool Top
        {
            get { return top; }
            set { top = value; }
        }

        private bool bottom;
        public bool Bottom
        {
            get { return bottom; }
            set { bottom = value; }
        }

        public override string ToString()
        {
            string s = "";
            if (Left) s += "[Left] ";
            if (Bottom) s += "[Bottom] ";
            if (Top) s += "[Top] ";
            if (Right) s += "[Right] ";
            s = s.Trim();
            if (s == "") s = "[None]";
            return s;
        }
    }
}
