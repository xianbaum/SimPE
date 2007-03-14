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
    public class Pad
    {
        #region Attributes
        byte note1;
        byte note2;
        byte curve;
        byte gate;
        byte channel;
        byte minVelocity;
        byte maxVelocity;
        byte note3;
        byte note4;
        byte note5;
        byte note6;
        byte flags; // bit7: HiHat pad; bit6: motif pad
        #endregion

        public Pad() { }
        public Pad(System.IO.BinaryReader r)
        {
            Unserialize(r);
        }

        protected void Unserialize(System.IO.BinaryReader r)
        {
            note1 = r.ReadByte();
            note2 = r.ReadByte();
            curve = r.ReadByte();
            gate = r.ReadByte();
            channel = r.ReadByte();
            minVelocity = r.ReadByte();
            maxVelocity = r.ReadByte();
            note3 = r.ReadByte();
            note4 = r.ReadByte();
            note5 = r.ReadByte();
            note6 = r.ReadByte();
            flags = r.ReadByte();
        }
        public void Serialize(System.IO.BinaryWriter w)
        {
            w.Write(note1);
            w.Write(note2);
            w.Write(curve);
            w.Write(gate);
            w.Write(channel);
            w.Write(minVelocity);
            w.Write(maxVelocity);
            w.Write(note3);
            w.Write(note4);
            w.Write(note5);
            w.Write(note6);
            w.Write(flags);
        }
    }
}
