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
    public class AllMemory
    {
        #region Attributes
        Kit[] kits = new Kit[24];
        byte[] unused = new byte[540];
        Global global = null;
        #endregion

        public AllMemory()
        {
            for (int i = 0; i < kits.Length; i++) kits[i] = new Kit();
            global = new Global();
        }
        public AllMemory(System.IO.BinaryReader r) { Unserialize(r); }

        public Global Global
        {
            get { return global; }
            set { global = value; }
        }
        public Kit[] Kits { get { return kits; } }

        protected void Unserialize(System.IO.BinaryReader r)
        {
            for (int i = 0; i < kits.Length; i++)
                kits[i] = new Kit(r, false);
            for (int i = 0; i < kits.Length; i++)
                kits[i].UnserializeKitName(r);
            unused = r.ReadBytes(unused.Length);
            global = new Global(r);
        }

        public void Serialize(System.IO.BinaryWriter w)
        {
            for (int i = 0; i < kits.Length; i++)
                kits[i].Serialize(w, false);
            for (int i = 0; i < kits.Length; i++)
                kits[i].SerializeKitName(w);
            w.Write(unused);
            global.Serialize(w);
        }
    }
}
