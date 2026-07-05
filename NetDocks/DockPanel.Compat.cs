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
    partial class DockPanel
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image TabImage
        {
            get { return this.Image; }
            set { this.Image = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TabText
        {
            get { return this.ButtonText; }
            set { this.ButtonText = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AllowFloat {
            get { return true; }
            set {  }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AllowDockBottom
        {
            get { return true; }
            set { }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AllowDockLeft
        {
            get { return true; }
            set { }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AllowDockRight
        {
            get { return true; }
            set { }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AllowDockTop
        {
            get { return true; }
            set { }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AllowDockCenter
        {
            get { return true; }
            set { }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AllowClose
        {
            get { return this.ShowCloseButton; }
            set { this.ShowCloseButton = value;  }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AllowCollapse
        {
            get { return this.ShowCollapseButton; }
            set { this.ShowCollapseButton = value; }
        }

        [Browsable(false)]
        public bool IsDocked
        {
            get { return DockContainer != null; }
            
        }

        [Browsable(false)]
        public bool IsFloating
        {
            get { return this.Floating;  }

        }

        [Browsable(false)]
        public bool IsOpen
        {
            get { return IsFloating || IsDocked; }

        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size FloatingSize
        {
            get { return this.Size; }
            set { }
        }

        public void OpenFloating()
        {
            this.Float(last.Pos);
            if (Opened != null) Opened(this, new EventArgs());
        }

        
    }
}
