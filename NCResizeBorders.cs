using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Floaters
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
