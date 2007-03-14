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
    public class Kit
    {
        #region Attributes
        Pad[] pads = new Pad[28];
        byte curve;
        byte gate;
        byte channel;
        byte minVelocity;
        byte maxVelocity;
        byte fcFunction;
        byte bcFunction;
        byte prgChg;
        byte prgChgTxmChn;
        byte volume;
        byte hhPad1;
        byte hhPad2;
        byte hhPad3;
        byte hhPad4;
        byte bank;
        byte fcChannel;
        byte fcCurve;
        byte bankMSB;
        byte bankLSB;
        byte unused;
        byte[] kitName = new byte[12];
        #endregion

        public Kit()
        {
            for (int i = 0; i < pads.Length; i++) pads[i] = new Pad();
        }
        public Kit(System.IO.BinaryReader r, bool withName) { Unserialize(r, withName); }

        protected void Unserialize(System.IO.BinaryReader r, bool withName)
        {
            for (int i = 0; i < pads.Length; i++)
                pads[i] = new Pad(r);

            curve = r.ReadByte();
            gate = r.ReadByte();
            channel = r.ReadByte();
            minVelocity = r.ReadByte();
            maxVelocity = r.ReadByte();
            fcFunction = r.ReadByte();
            bcFunction = r.ReadByte();
            prgChg = r.ReadByte();
            prgChgTxmChn = r.ReadByte();
            volume = r.ReadByte();
            hhPad1 = r.ReadByte();
            hhPad2 = r.ReadByte();
            hhPad3 = r.ReadByte();
            hhPad4 = r.ReadByte();
            bank = r.ReadByte();
            fcChannel = r.ReadByte();
            fcCurve = r.ReadByte();
            bankMSB = r.ReadByte();
            bankLSB = r.ReadByte();
            unused = r.ReadByte();
            kitName = withName ? r.ReadBytes(kitName.Length) : null;
        }
        public void Serialize(System.IO.BinaryWriter w, bool withName)
        {
            for (int i = 0; i < pads.Length; i++)
                pads[i].Serialize(w);

            w.Write(curve);
            w.Write(gate);
            w.Write(channel);
            w.Write(minVelocity);
            w.Write(maxVelocity);
            w.Write(fcFunction);
            w.Write(bcFunction);
            w.Write(prgChg);
            w.Write(prgChgTxmChn);
            w.Write(volume);
            w.Write(hhPad1);
            w.Write(hhPad2);
            w.Write(hhPad3);
            w.Write(hhPad4);
            w.Write(bank);
            w.Write(fcChannel);
            w.Write(fcCurve);
            w.Write(bankMSB);
            w.Write(bankLSB);
            w.Write(unused);
            if (withName) w.Write(kitName);
        }

        public String KitName
        {
            get
            {
                String s = "";
                foreach (byte i in kitName)
                    s += (char)i;
                return s;
            }
            set
            {
                if (value.Length > 12)
                    throw new ArgumentException("KitName must be 12 characters or fewer");
                int i = 0;
                while (i < value.Length)
                    kitName[i] = (byte)(value.ToCharArray()[i]);
                while (i < kitName.Length)
                    kitName[i] = (byte)' ';
            }
        }
        public void UnserializeKitName(System.IO.BinaryReader r) { kitName = r.ReadBytes(12); }
        public void SerializeKitName(System.IO.BinaryWriter w) { w.Write(kitName); }
    }
}
