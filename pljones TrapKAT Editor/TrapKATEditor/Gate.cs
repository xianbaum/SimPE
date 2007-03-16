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

    public class Gate : DataItem
    {
        private byte gate;
        public Gate() : base() { }
        public Gate(System.IO.BinaryReader r) : base(r) { }

        protected override void Unserialize(System.IO.BinaryReader r) { gate = r.ReadByte(); }
        public void Serialize(System.IO.BinaryWriter w) { w.Write(gate); }

        static List<String> mode = new List<string>(new string[] { "Latch Mode", "Infinite", "Roll Mode", });
        public static List<String> Modes { get { return mode; } }

        private void SetGate(byte value)
        {
            if (gate == value) return;
            gate = value;
            OnDataChanged(this, new EventArgs());
        }

        public String Value
        {
            get
            {
                if (gate <= 79) { return (0.005 + (gate * 0.005)).ToString(); }
                else if (gate <= 231) { return (0.4 + (gate - 79) * 0.025).ToString(); }
                else if (gate < 253) { return (4.2 + (gate - 231) * 0.100).ToString(); }
                else if (gate - 253 < mode.Count) return mode[gate - 253];
                else return "[UNK: " + gate.ToString() + "]";
            }
            set
            {
                byte b = (byte)(128 + mode.IndexOf(value));
                if (b >= 128) { SetGate(b); return; }

                Decimal f;
                try { f = Convert.ToDecimal(value); }
                catch (Exception) { throw new ArgumentException(value + " is not a valid gate time"); }

                if (f < 0.005m)
                    throw new ArgumentException(value + " is not a valid gate time");

                if (f < 0.4m) { SetGate((byte)((f - 0.005m) / 0.005m)); return; }// 0.005..0.395 -> 0..78
                if (f < 4.2m) { SetGate((byte)(79 + ((f - 0.4m) / 0.025m))); return; } // 0.400..4.175 -> 79..230
                if (f < 6.4m) { SetGate((byte)(231 + ((f - 4.2m) / 0.1m))); return; } // 4.200..6.300 -> 231..252

                throw new ArgumentException(value + " is not a valid gate time");
            }
        }
    }
}
