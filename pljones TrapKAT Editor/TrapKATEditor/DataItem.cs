/***************************************************************************
 *   Copyright (C) 2007 by Peter L Jones                                   *
 *   peter@drealm.info                                                     *
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
using System.Data;
using System.Text;

namespace TrapKATEditor.Data
{
    public class DataItem
    {
        public DataItem() { }
        public DataItem(System.IO.BinaryReader r) { Unserialize(r); }
        protected virtual void Unserialize(System.IO.BinaryReader r) { }

        private bool changed;
        public bool Changed { get { return changed; } }
        protected void SetChanged() { changed = true; }

        private bool internalchg;
        protected bool InternalChg
        {
            get { return internalchg; }
            set { internalchg = value; }
        }

        public event EventHandler DataChanged;
        protected virtual void OnDataChanged(object sender, EventArgs e)
        {
            changed = true;
            if (internalchg) return;

            if (DataChanged != null)
                DataChanged(sender, e);
        }
    }
}
