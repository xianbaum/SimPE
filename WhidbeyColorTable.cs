using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Floaters
{
    public class WhidbeyColorTable : IColorTable
    {
        #region IColorTable Member

        public System.Drawing.Color DockBorderColor
        {
            get { return Color.FromArgb(0xAC, 0xA8, 0x99); }
        }

        public System.Drawing.Color DockBackgroundColor
        {
            get { return SystemColors.Control; }
        }

        public System.Drawing.Color DockHintHightlightColor
        {
            get { return SystemColors.MenuHighlight; }
        }

        public System.Drawing.Color DockHintOverlayColor
        {
            get { return Color.FromArgb(128, SystemColors.MenuHighlight); }
        }

        public Color DockButtonBorderColor
        {
            get { return Color.DarkBlue; }
        }

        public Color DockButtonHighlightBorderColor
        {
            get { return Color.Red; }
        }

        public Color DockCaptionColorTop
        {
            get { return Color.FromArgb(0xC0, 0xBB, 0xAF); }
        }

        public Color DockCaptionColorBottom
        {
            get { return DockCaptionColorTop; }
        }

        public Color DockCaptionFocusColorTop
        {
            get { return Color.FromArgb(0x3B, 0x80, 0xED); }
        }        

        public Color DockCaptionFocusColorBottom
        {
            get { return Color.FromArgb(0x31, 0x6A, 0xC5); }
        }

        public Color DockCaptionTextColor
        {
            get { return Color.Black; }
        }

        public Color DockCaptionFocusTextColor
        {
            get { return Color.White; }
        }
        #endregion
    }
}
