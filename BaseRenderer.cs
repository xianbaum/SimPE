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

namespace Floaters
{
    public abstract class BaseRenderer 
    {
        
        public BaseRenderer(IColorTable ct, IFontTable fnt)
        {
            table = ct;
            this.fnt = fnt;
        }


        IColorTable table;
        public IColorTable ColorTable {
            get
            {
                return table;
            }
        }

        IFontTable fnt;
        public IFontTable FontTable
        {
            get
            {
                return fnt;
            }
        }

        IRenderDockHints dock;
        public  IRenderDockHints DockRenderer {
            get
            {
                if (dock == null) CreateDockRenderer(out dock);
                return dock;
            }
        }
        protected abstract void CreateDockRenderer(out IRenderDockHints rnd);



        IDockPanelRenderer panel;
        public IDockPanelRenderer DockPanelRenderer
        {
            get
            {
                if (panel == null) CreateDockPanelRenderer(out panel);
                return panel;
            }
        }

        protected abstract void CreateDockPanelRenderer(out IDockPanelRenderer rnd);

        
    }
}
