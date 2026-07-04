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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Ambertation.Windows.Forms
{
    public class TabPage : DockPanel
    {
        public TabPage()
            :this (null)
        {
        }

        public TabPage(TabControl tc)
            : base(tc)
        {
            base.Dock = DockStyle.Fill;
            base.ShowCollapseButton = false;
        }

        #region Disable crtitical settings
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), ReadOnly(true)]
        public new DockStyle Dock
        {
            get { return base.Dock; }
            set { }
        }

        protected override void OnDockChanged(EventArgs e)
        {
            base.Dock = DockStyle.Fill;
            base.OnDockChanged(e);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), ReadOnly(true)]        
        public override bool ShowCollapseButton
        {
            get { return base.ShowCollapseButton; }
            set { }
        }
        #endregion

        public TabControl TabControl
        {
            get { return Manager as TabControl; }
        }

        public override bool OnlyChild
        {
            get { return false; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), ReadOnly(true)]
        public override string ButtonText
        {
            get
            {
                return base.CaptionText;
            }
            set
            {
                base.CaptionText = value;
            }
        }
    }
}
