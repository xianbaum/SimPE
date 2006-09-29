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

        void RenderButton(System.Drawing.Graphics g, System.Drawing.Rectangle r, string caption, ButtonOrientation orient, ButtonState state); //ok        
                                
        void RenderCaption(DockPanel dp, NCPaintEventArgs e); //ok
        void RenderCaptionButton(DockPanel dp, DockPanelCaptionButton but, NCPaintEventArgs e); //ok
        void RenderBorder(DockPanel dp, NCPaintEventArgs e); //ok

        void Animate(DockAnimationEventArgs e); //ok
        event DockAnimationEventHandler FinishedAnimation; //ok
    }
}
