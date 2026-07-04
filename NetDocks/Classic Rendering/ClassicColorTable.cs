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
    public class ClassicColorTable : IColorTable
    {
        #region IColorTable Member

        public System.Drawing.Color DockBorderColor
        {
            get { return SystemColors.ActiveBorder; }
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
            get { return Color.FromArgb(128, Color.White); }
        }

        public Color DockButtonBorderColorOuter
        {
            get { return SystemColors.InactiveBorder; }
        }

        public Color DockButtonBorderColorInner
        {
            get { return Color.Transparent; }
        }

        public Color DockButtonHighlightBorderColorOuter
        {
            get { return SystemColors.ActiveBorder; }
        }

        public Color DockButtonHighlightBorderColorInner
        {
            get { return Color.Transparent; }
        }


        public Color DockButtonBarBackgroundTop
        {
            get { return SystemColors.ControlDarkDark; }
        }
        public Color DockButtonBarBackgroundBottom
        {
            get { return DockButtonBarBackgroundTop; }
        }

        public Color DockButtonBackgroundTop
        {
            get { return SystemColors.Control; }
        }
        public Color DockButtonBackgroundBottom
        {
            get { return DockButtonBackgroundTop; }
        }

        public Color DockButtonHighlightBackgroundTop
        {
            get { return SystemColors.ControlLightLight; }
        }
        public Color DockButtonHighlightBackgroundBottom
        {
            get { return DockButtonHighlightBackgroundTop; }
        }


        public Color DockButtonTextColor
        {
            get { return SystemColors.GrayText; }
        }
        public Color DockButtonHighlightTextColor
        {
            get { return SystemColors.ControlText; }
        }

        public Color DockCaptionColorTop //ok
        {
            get { return SystemColors.InactiveCaption; }
        }

        public Color DockCaptionColorBottom //ok
        {
            get { return DockCaptionColorTop; }
        }

        public Color DockCaptionFocusColorTop //ok
        {
            get { return SystemColors.ActiveCaption; }
        }        

        public Color DockCaptionFocusColorBottom //ok
        {
            get { return DockCaptionFocusColorTop; }
        }

        public Color DockCaptionTextColor //ok
        {
            get { return SystemColors.InactiveCaptionText; }
        }

        public Color DockCaptionFocusTextColor //ok
        {
            get { return SystemColors.ActiveCaptionText; }
        }
        #endregion

        public Color DockGripColor
        {
            get { return SystemColors.ControlDark; }
        }

        public Color DockReSizeBackgroundColor
        {
            get { return SystemColors.AppWorkspace; }
        }
        public Color DockReSizeGripColor
        {
            get { return SystemColors.ButtonShadow; }
        }
    }
}
