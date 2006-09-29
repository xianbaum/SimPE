using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Floaters
{
    public interface IColorTable
    {
        Color DockBorderColor { get; }
        Color DockBackgroundColor { get; }

        Color DockHintHightlightColor { get; }
        Color DockHintOverlayColor { get; }

        Color DockButtonBorderColor { get; }
        Color DockButtonHighlightBorderColor { get; }

        Color DockCaptionColorTop { get; }
        Color DockCaptionFocusColorTop { get; }
        Color DockCaptionColorBottom { get; }
        Color DockCaptionFocusColorBottom { get; }
        Color DockCaptionTextColor { get;}
        Color DockCaptionFocusTextColor { get;}
    }
}
