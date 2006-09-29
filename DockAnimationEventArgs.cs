using System;
using System.Collections.Generic;
using System.Text;

namespace Floaters
{
    public delegate void DockAnimationEventHandler(IDockPanelRenderer sender, DockAnimationEventArgs e);
    public class DockAnimationEventArgs
    {
        public enum Alignment
        {
            Horizontal,
            Vertical,
            Undefined
        }

        public enum Type
        {
            Collapse,
            Expand
        }


        DockContainer dc;
        Type tp;
        Alignment a;
        public DockAnimationEventArgs(DockContainer dc, Type tp, Alignment a)
        {
            this.dc = dc;
            this.tp = tp;
            this.a = a;
        }

        public DockContainer Container
        {
            get { return dc; }
        }

        public Type AnimationType
        {
            get { return tp; }
        }

        public Alignment DockAlignment
        {
            get { return a; }
        }

        public override string ToString()
        {
            return "type:"+tp + ", align:" + a + ", name:" + dc.Name;
        }
    }
}
