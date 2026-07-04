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
            //if (res && state == CaptionButtonState.Selected) OnClick();
            return res;
        }

        /// <summary>
        /// Performs the click action
        /// </summary>
        public void PerformClick()
        {
            OnClick();
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
