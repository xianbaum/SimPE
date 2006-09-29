using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Floaters
{
    public enum SelectedHint : byte
    {
        Left, Right, Top, Bottom, Center, None
    }

    public interface IRenderDockHints : IControlRenderer
    {        
        Size HintSize { get; }
        /*Image AllHintsImage { get; }
        Image LeftHintImage { get; }
        Image TopHintImage { get; }
        Image RightHintImage { get; }
        Image BottomHintImage { get; }*/

        Rectangle LeftRectangle { get; }
        Rectangle TopRectangle { get; }
        Rectangle RightRectangle { get; }
        Rectangle BottomRectangle { get; }
        Rectangle CenterRectangle { get; }

        void InitHints(bool l, bool t, bool r, bool b, bool c);
        void RenderHint(Graphics g, bool l, bool t, bool r, bool b, bool c, SelectedHint sel);              
    }
}
