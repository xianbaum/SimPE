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
    public class Pad : DataItem
    {
        #region Attributes
        byte curve;
        Gate gate = null;
        byte channel = 9;
        byte minVelocity = 1;
        byte maxVelocity = 127;
        byte flags; // bit7: HiHat pad; bit6: motif pad

        private List<byte> notes = new List<byte>(new byte[] { 0, 0, 0, 0, 0, 0, });
        #endregion

        public Pad() : base()
        {
            gate = new Gate();
            gate.DataChanged += new EventHandler(gate_DataChanged);
        }

        public Pad(System.IO.BinaryReader r) : base(r) { }

        void gate_DataChanged(object sender, EventArgs e)
        {
            SetChanged();
            OnDataChanged(this, e);
        }

        protected override void Unserialize(System.IO.BinaryReader r)
        {
            notes[0] = r.ReadByte();
            notes[1] = r.ReadByte();
            curve = r.ReadByte();
            gate = new Gate(r);
            gate.DataChanged += new EventHandler(gate_DataChanged);
            channel = r.ReadByte();
            minVelocity = r.ReadByte();
            maxVelocity = r.ReadByte();
            notes[2] = r.ReadByte();
            notes[3] = r.ReadByte();
            notes[4] = r.ReadByte();
            notes[5] = r.ReadByte();
            flags = r.ReadByte();
        }
        public void Serialize(System.IO.BinaryWriter w)
        {
            w.Write(notes[0]);
            w.Write(notes[1]);
            w.Write(curve);
            gate.Serialize(w);
            w.Write(channel);
            w.Write(minVelocity);
            w.Write(maxVelocity);
            w.Write(notes[2]);
            w.Write(notes[3]);
            w.Write(notes[4]);
            w.Write(notes[5]);
            w.Write(flags);
        }

        public byte this[int index]
        {
            get { return notes[index]; }
            set
            {
                if (notes[index] == value) return;
                notes[index] = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte Curve
        {
            get { return curve; }
            set
            {
                if (curve == value) return;
                curve = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public String Gate
        {
            get { return gate.Value; }
            set { gate.Value = value; }
        }
        public byte Channel
        {
            get { return channel; }
            set
            {
                if (channel == value) return;
                channel = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte MinVelocity
        {
            get { return minVelocity; }
            set
            {
                if (minVelocity == value) return;
                minVelocity = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte MaxVelocity
        {
            get { return maxVelocity; }
            set
            {
                if (maxVelocity == value) return;
                maxVelocity = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte Flags
        {
            get { return flags; }
            set
            {
                if (flags == value) return;
                flags = value;
                OnDataChanged(this, new EventArgs());
            }
        }
    }
}
