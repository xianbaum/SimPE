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

namespace Ambertation.Windows.Forms
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
            get { return Color.FromArgb(0x50, DockButtonHighlightBorderColor); }
        }

        public Color DockButtonHighlightBorderColor
        {
            get { return Color.FromArgb(0xAC, 0xA8, 0x99); }
        }
        public Color DockButtonBackgroundTop
        {
            get { return Color.FromArgb(0xED, 0xEC, 0xE0); }
        }
        public Color DockButtonBackgroundBottom
        {
            get { return DockButtonBackgroundTop; }
        }
        public Color DockButtonHighlightBackgroundTop
        {
            get { return Color.FromArgb(0xFC, 0xFC, 0xFE); }
        }
        public Color DockButtonHighlightBackgroundBottom
        {
            get { return DockButtonHighlightBackgroundTop; }
        }


        public Color DockButtonTextColor
        {
            get { return Color.FromArgb(0x71, 0x6F, 0x64); }
        }
        public Color DockButtonHighlightTextColor
        {
            get { return Color.Black; }
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
