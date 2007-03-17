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
    class Curve : DataItem
    {
        private byte curve;
        public Curve() : base() { }
        public Curve(System.IO.BinaryReader r) : base(r) { }

        protected override void Unserialize(System.IO.BinaryReader r) { curve = r.ReadByte(); }
        public void Serialize(System.IO.BinaryWriter w) { w.Write(curve); }

        static List<String> mode = new List<string>(new string[] {
            "Curve 1", "Curve 2", "Curve 3", "Curve 4", "Curve 5", "Curve 6", "Curve 7", "Curve 8",
            "2nd Note @ Hardest", "2nd Note @ Hard", "2nd Note @ Medium", "2nd Note @ Soft",
            "2 Note Layer", "Xfade @ Middle", "Xswitch @ Middle", "1@Medium;3@Hardest",
            "2@Medium;3@Hard", "2Double 1;3Medium", "3 Note Layer", "4 Note VelShift",
            "4 Note Layer", "Alternate 1,2", "Alternate 1,2,3", "Alternate 1,2,3,4",
        });
        public static List<String> Modes { get { return mode; } }

        private void SetGate(byte value)
        {
            if (curve == value) return;
            curve = value;
            OnDataChanged(this, new EventArgs());
        }

        public String Value
        {
            get
            {
                if (curve < mode.Count) return mode[curve];
                else return "[UNK: " + curve.ToString() + "]";
            }
            set
            {
                if (mode.Contains(value)) { SetGate((byte)mode.IndexOf(value)); return; }
                throw new ArgumentException(value + " is not a valid curve");
            }
        }
    }
}
