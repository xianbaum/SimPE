using System;
using System.Collections.Generic;
using System.Text;

namespace Floaters
{
    public interface IButtonContainer
    {
        ButtonOrientation BestOrientation
        {
            get;
        }

        DockButtonBar.DockPanelList GetButtons();
        System.Windows.Forms.Padding GetBorderSize(ButtonOrientation orient);
        DockPanel Highlight
        {
            get;
            set;
        }
    }
}
