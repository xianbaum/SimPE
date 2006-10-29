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
    public class GlossyColorTable : IColorTable
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

        public Color DockButtonBorderColorOuter
        {
            get { return Color.FromArgb(0x60, DockButtonHighlightBorderColorOuter); }
        }

        public Color DockButtonBorderColorInner
        {
            get { return Color.FromArgb(0x40, Color.White); }
        }

        public Color DockButtonHighlightBorderColorOuter
        {
            get { return Color.FromArgb(0xA0, 0xAC, 0xA8, 0x99); }
        }

        public Color DockButtonHighlightBorderColorInner
        {
            get { return Color.FromArgb(0xA0, Color.White); }
        }


        public Color DockButtonBarBackgroundTop
        {
            get { return Color.FromArgb(0xEE, 0xEE, 0xEE); }
        }
        public Color DockButtonBarBackgroundBottom
        {
            get { return Color.FromArgb(0xFF, 0xFF, 0xFF); }
        }

        public Color DockButtonBackgroundTop
        {
            get { return Color.FromArgb(0xFF, 0xFF, 0xFF); }
        }
        public Color DockButtonBackgroundBottom
        {
            get { return  Color.FromArgb(0xE9, 0xE9, 0xE9); }
        }
        public Color DockButtonHighlightBackgroundTop
        {
            get { return Color.FromArgb(0xF5, 0xF5, 0xF5); }
        }
        public Color DockButtonHighlightBackgroundBottom
        {
            get { return Color.FromArgb(0xEA, 0xEA, 0xEA); }
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
            get { return Color.FromArgb(0xD3, 0xD3, 0xD7); }
        }

        public Color DockCaptionColorBottom
        {
            get { return Color.FromArgb(0xD0, 0xD0, 0xD4); }
        }

        public Color DockCaptionFocusColorTop
        {
            get { return Color.FromArgb(0x56, 0x5D, 0x61); }
        }        

        public Color DockCaptionFocusColorBottom
        {
            get { return Color.FromArgb(0x4F, 0x55, 0x59); }
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

        public Color DockGripColor
        {
            get { return SystemColors.ControlLight; }
        }

        public Color DockReSizeBackgroundColor
        {
            get { return Color.FromArgb(0x70, 0x72, 0x86); }
        }
        public Color DockReSizeGripColor
        {
            get { return Color.FromArgb(0x3E, 0x3F, 0x4A); }
        }
    }
}
