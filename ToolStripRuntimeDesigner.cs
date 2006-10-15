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
using System.Windows.Forms;

namespace Ambertation.Windows.Forms
{
    

    public class ToolStripRuntimeDesigner
    {
        public static void Add(ToolStrip ts)
        {
            List<ToolStripButton> buts = new List<ToolStripButton>();
            int top = 0;
            foreach (ToolStripItem item in ts.Items)
            {
                ToolStripButton bt = new ToolStripButtonExt(item, ref top);
                buts.Add(bt);
            }

            foreach (ToolStripButton bt in buts)
            {
                ts.Items.Add(bt);
            }
        }

        protected static void Add(ToolStripPanel pn, ContextMenuStrip men)
        {
           
            foreach (Control c in pn.Controls)
            {
                ToolStrip ts = c as ToolStrip;
                if (ts == null) continue;

                ToolStripButton im = new MenuStripButtonExt(ts);
                
                men.Items.Add(im);

                if (ts != null) Add(ts);
            }

            pn.ContextMenuStrip = men;
        }

        
        public static void Add(ToolStripContainer cnt)
        {
            ContextMenuStrip men = new ContextMenuStrip();
            
            Add(cnt.TopToolStripPanel, men);
            Add(cnt.LeftToolStripPanel, men);
            Add(cnt.BottomToolStripPanel, men);
            Add(cnt.RightToolStripPanel, men);
        }

        public static void LineUpToolBars(ToolStripContainer cnt)
        {
            LineUpToolBars(cnt.TopToolStripPanel, true);
            LineUpToolBars(cnt.LeftToolStripPanel, false);
            LineUpToolBars(cnt.BottomToolStripPanel, true);
            LineUpToolBars(cnt.RightToolStripPanel, false);
        }

        public static void LineUpToolBars(ToolStripPanel pn, bool horz)
        {
            int left = 0;
            int top = 0;
            foreach (Control c in pn.Controls)
            {
                ToolStrip ts = c as ToolStrip;
                if (ts == null) continue;
                ts.Left = left;
                ts.Top = top;

                if (horz) left = ts.Left + ts.Width + 1;
                else top = ts.Top + ts.Height + 1;
            }
        }
    }
}
