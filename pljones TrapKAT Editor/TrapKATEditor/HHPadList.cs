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
using System.Text;

namespace TrapKATEditor.Data
{
    public class HHPadList : DataItem
    {
        List<byte> hhPads = new List<byte>(new byte[4] { 1, 4, 0, 0, });
        public HHPadList() : base() { }
        public HHPadList(System.IO.BinaryReader r) : base(r) { }

        protected override void Unserialize(System.IO.BinaryReader r) { hhPads = new List<byte>(r.ReadBytes(4)); }
        public void Serialize(System.IO.BinaryWriter w) { w.Write(hhPads.ToArray()); }
        public byte this[int index]
        {
            get { return hhPads[index]; }
            set
            {
                if (hhPads[index] == value) return;
                hhPads[index] = value;
                OnDataChanged(this, new EventArgs());
            }
        }
    }
}
