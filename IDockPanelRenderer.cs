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

namespace Floaters
{
    /// <summary>
    /// This enum determins the orientation of a <see cref="DockPanel"/>-Button that is going to be rendered
    /// </summary>
    public enum ButtonOrientation {
        /// <summary>
        /// Button is placed on the bottom of the dock (default)
        /// </summary>
        Bottom, 
        /// <summary>
        /// Button is placed on the left side of the dock
        /// </summary>
        Left, 
        /// <summary>
        /// Button is placed above the dock
        /// </summary>
        Top, 
        /// <summary>
        /// Button is placed on the right side of the dock
        /// </summary>
        Right
    }

    /// <summary>
    /// Used to identify the display state of a button
    /// </summary>
    public enum CaptionState
    {
        /// <summary>
        /// The default dock is not selected
        /// </summary>
        Normal,
        /// <summary>
        /// The dock has the focus
        /// </summary>
        Focused
    }

    /// <summary>
    /// Used to identify the display state of a button
    /// </summary>
    public enum ButtonState { 
        /// <summary>
        /// The default button look needs to be displayed
        /// </summary>
        Normal, 
        /// <summary>
        /// The button is highlighted. and needs to be displayed that way
        /// </summary>
        Highlight
    }

    /// <summary>
    /// This interface describes the render module, that is used to display a <see cref="DockPanel"/>
    /// </summary>
    public interface IDockPanelRenderer 
    {
        DockPanelButtonManager ConstructButtonData(IButtonContainer cnt, NCPaintEventArgs e); //ok  

        /// <summary>
        /// Returns the size of the Rectangle that displays the buttons
        /// </summary>
        /// <param name="orient">Orientation of the container</param>
        /// <param name="e">the painting events</param>
        /// <returns>The rectangle the buttons can be displayed in</returns>
        Rectangle GetButtonsRectangle(ButtonOrientation orient, NCPaintEventArgs e);

        /// <summary>
        /// Returns the Rectangle where the client can draw
        /// </summary>
        /// <param name="dp">The panel you want to get the client rectangle for</param>
        /// <param name="orient">The orientation of the panel</param>
        /// <returns>The client rectangle</returns>
        Rectangle GetPanelClientRectangle(DockPanel dp, ButtonOrientation orient);

        /// <summary>
        /// Returns the Rectangle where the client can draw
        /// </summary>
        /// <param name="e">The latest paint event</param>
        /// <param name="orient">The orientation of the panel</param>
        /// <returns>The client rectangle</returns>
        Rectangle GetPanelClientRectangle(NCPaintEventArgs e, ButtonOrientation orient);

        System.Windows.Forms.Padding GetPanelBorderSize(ButtonOrientation orient); //ok
        System.Windows.Forms.Padding GetBarBorderSize(ButtonOrientation orient); //ok
        System.Windows.Forms.Padding GetBorderSize(IButtonContainer c); //ok
        Size GetButtonSize(DockPanel dp); //ok
        Size GetButtonSize(DockPanel dp, ButtonOrientation orient); //ok
        BaseDockPanelRenderer.Dimensions Dimension { get;} //ok
        string GetFittingString(System.Drawing.Font font, string caption, ButtonOrientation orient, Size maxsz); //ok

        System.Drawing.Rectangle GetCaptionRect(DockPanel dp); //ok
        System.Drawing.Rectangle GetCaptionRect(DockPanel dp, ButtonOrientation orient); //ok
        System.Drawing.Rectangle GetCloseButtonRect(DockPanel dp, Rectangle caprect); //ok
        System.Drawing.Rectangle GetCollapseButtonRect(DockPanel dp, Rectangle caprect); //ok
        System.Drawing.Rectangle GetCaptionTextRect(DockPanel dp, Rectangle caprect); //ok

        void RenderButtonBackground(DockPanel dp, NCPaintEventArgs e);
        void RenderButton(System.Drawing.Graphics g, System.Drawing.Rectangle r, string caption, Image img, ButtonOrientation orient, ButtonState state); //ok        
                                
        void RenderCaption(DockPanel dp, NCPaintEventArgs e); //ok
        void RenderCaptionButton(DockPanel dp, DockPanelCaptionButton but, NCPaintEventArgs e); //ok
        void RenderBorder(DockPanel dp, NCPaintEventArgs e); //ok

        void Animate(DockAnimationEventArgs e); //ok
        event DockAnimationEventHandler FinishedAnimation; //ok
    }
}
