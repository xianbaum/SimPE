using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Floaters
{
    public interface IFontTable
    {
        Font ButtonFont { get; }
        Font ButtonHighlightFont { get; }
        Font CaptionFont { get; }
    }
}
