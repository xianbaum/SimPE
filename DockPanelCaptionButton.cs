using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Floaters
{
    /// <summary>
    /// Used to identify the display state of a <see cref="DockPanelCaptionButton"/>
    /// </summary>
    public enum CaptionButtonState
    {
        /// <summary>
        /// The default button look needs to be displayed
        /// </summary>
        Normal,
        /// <summary>
        /// The button is highlighted. and needs to be displayed that way
        /// </summary>
        Highlight,
        /// <summary>
        /// The button is selected (aka pressed)
        /// </summary>
        Selected
    }

    public abstract class DockPanelCaptionButton
    {
        DockPanel parent;
        CaptionButtonState state;
        bool visible;

        
        public DockPanelCaptionButton(DockPanel dp)
        {
            visible = true;
            parent = dp;
            state = CaptionButtonState.Normal;
        }

        protected IDockPanelRenderer Renderer
        {
            get { return parent.Manager.Renderer.DockPanelRenderer; }
        }

        public DockPanel Parent
        {
            get { return parent; }
        }

        public CaptionButtonState State
        {
            get { return state; }
        }

        /// <summary>
        /// Changes the state of the button
        /// </summary>
        /// <param name="st">The new state</param>
        /// <returns>true, if the state did change</returns>
        internal bool SetState(CaptionButtonState st){
            bool res = (state != st);
            state = st;
            if (res && state == CaptionButtonState.Selected) OnClick();
            return res;
        }

        /// <summary>
        /// Fired whenever teh button get's activated
        /// </summary>
        protected abstract void OnClick();

        public bool Visible
        {
            get { return visible; }            
        }
        /// <summary>
        /// Changes the visibility of the button
        /// </summary>
        /// <param name="vis">The new visibility</param>
        /// <returns>true, if the visibility did change</returns>
        internal bool SetVisible(bool vis)
        {
            bool res = (vis != visible);
            visible = vis;
            return res;
        }

        internal bool Hit(NCMouseEventArgs e)
        {
            return Bounds.Contains(e.ControlPosition);
        }

        public Rectangle Bounds
        {
            get
            {
                Rectangle cr = Renderer.GetCaptionRect(parent);
                return GetBounds(cr);
            }
        }

        internal string ImageName
        {
            get { return GetImageName(); }
        }

        internal void Render(NCPaintEventArgs e)
        {
            Renderer.RenderCaptionButton(parent, this, e);
        }


        protected abstract Rectangle GetBounds(Rectangle captionrect);
        protected abstract string GetImageName();
    }
}
