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
    public class Kit : DataItem
    {
        #region Attributes
        Pad[] pads = new Pad[28];
        Curve curve;
        Gate gate;
        byte channel = 9;
        byte minVelocity = 1;
        byte maxVelocity = 127;

        byte fcFunction;
        byte bcFunction;
        byte prgChg;
        byte prgChgTxmChn = 9;
        byte volume = 127;

        HHPadList hhPads = null;

        byte bank;
        byte fcChannel = 16;
        Curve fcCurve;
        byte bankMSB;
        byte bankLSB;
        byte unused;

        byte[] kitName = new byte[12];
        #endregion

        public Kit() : base()
        {
            for (int i = 0; i < pads.Length; i++)
            {
                pads[i] = new Pad();
                pads[i].DataChanged += new EventHandler(dataChanged);
            }
            curve = new Curve();
            curve.DataChanged += new EventHandler(dataChanged);
            gate = new Gate();
            gate.DataChanged += new EventHandler(dataChanged);
            hhPads = new HHPadList();
            hhPads.DataChanged += new EventHandler(dataChanged);
            fcCurve = new Curve();
            fcCurve.DataChanged += new EventHandler(dataChanged);
        }
        public Kit(System.IO.BinaryReader r, bool withName) { Unserialize(r, withName); }

        void dataChanged(object sender, EventArgs e) { OnDataChanged(this, e); }

        protected override void Unserialize(System.IO.BinaryReader r) { this.Unserialize(r, true); }
        protected void Unserialize(System.IO.BinaryReader r, bool withName)
        {
            for (int i = 0; i < pads.Length; i++)
            {
                pads[i] = new Pad(r);
                pads[i].DataChanged += new EventHandler(dataChanged);
            }

            curve = new Curve(r);
            curve.DataChanged += new EventHandler(dataChanged);
            gate = new Gate(r);
            gate.DataChanged += new EventHandler(dataChanged);

            channel = r.ReadByte();
            minVelocity = r.ReadByte();
            maxVelocity = r.ReadByte();
            fcFunction = r.ReadByte();
            bcFunction = r.ReadByte();
            prgChg = r.ReadByte();
            prgChgTxmChn = r.ReadByte();
            volume = r.ReadByte();

            hhPads = new HHPadList(r);
            hhPads.DataChanged += new EventHandler(dataChanged);

            bank = r.ReadByte();
            fcChannel = r.ReadByte();

            fcCurve = new Curve(r);
            fcCurve.DataChanged += new EventHandler(dataChanged);

            bankMSB = r.ReadByte();
            bankLSB = r.ReadByte();
            unused = r.ReadByte();
            kitName = withName ? r.ReadBytes(kitName.Length) : null;
        }
        public override void Serialize(System.IO.BinaryWriter w) { this.Serialize(w, true); }
        public void Serialize(System.IO.BinaryWriter w, bool withName)
        {
            for (int i = 0; i < pads.Length; i++)
                pads[i].Serialize(w);

            curve.Serialize(w);
            gate.Serialize(w);

            w.Write(channel);
            w.Write(minVelocity);
            w.Write(maxVelocity);
            w.Write(fcFunction);
            w.Write(bcFunction);
            w.Write(prgChg);
            w.Write(prgChgTxmChn);
            w.Write(volume);

            hhPads.Serialize(w);

            w.Write(bank);
            w.Write(fcChannel);

            fcCurve.Serialize(w);

            w.Write(bankMSB);
            w.Write(bankLSB);
            w.Write(unused);
            if (withName) w.Write(kitName);

//            Changed = false;
        }

        public Pad this[int index] { get { return pads[index]; } }
        public int Length { get { return pads.Length; } }

        public String Curve
        {
            get {
                foreach (Pad pad in pads)
                    if (pad.Curve != curve.Value) return "Various";
                return curve.Value;
            }
            set { curve.Value = value; }
        }
        public String Gate
        {
            get
            {
                foreach (Pad pad in pads)
                    if (pad.Gate != gate.Value) return "Various";
                return gate.Value;
            }
            set { gate.Value = value; }
        }

        public byte Channel
        {
            get { return channel; }
            set
            {
                if (channel == value) return;
                /*if (value > 15)
                    throw new ArgumentOutOfRangeException(value.ToString() + " is not a valid Channel value");*/
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
                /*if (value > 127)
                    throw new ArgumentOutOfRangeException(value.ToString() + " is not a valid Velocity value");*/
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
                /*if (value > 127)
                    throw new ArgumentOutOfRangeException(value.ToString() + " is not a valid Velocity value");*/
                maxVelocity = value;
                OnDataChanged(this, new EventArgs());
            }
        }

        public byte FcFunction
        {
            get { return fcFunction; }
            set
            {
                if (fcFunction == value) return;
                /*if (value > ???)
                    throw new ArgumentOutOfRangeException(value.ToString() + " is not a valid Foot Controller function");*/
                fcFunction = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte BcFunction
        {
            get { return bcFunction; }
            set
            {
                if (bcFunction == value) return;
                /*if (value > ???)
                    throw new ArgumentOutOfRangeException(value.ToString() + " is not a valid Breath Controller function");*/
                bcFunction = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte PrgChg
        {
            get { return prgChg; }
            set
            {
                if (prgChg == value) return;
                /*if (value > 127)
                    throw new ArgumentOutOfRangeException(value.ToString() + " is not a valid Program Change value");*/
                prgChg = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte PrgChgTxmChn
        {
            get { return prgChgTxmChn; }
            set
            {
                if (prgChgTxmChn == value) return;
                /*if (value > 15)
                    throw new ArgumentOutOfRangeException(value.ToString() + " is not a valid Channel value");*/
                prgChgTxmChn = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte Volume
        {
            get { return volume; }
            set
            {
                if (volume == value) return;
                /*if (value > 127)
                    throw new ArgumentOutOfRangeException(value.ToString() + " is not a valid Volume value");*/
                volume = value;
                OnDataChanged(this, new EventArgs());
            }
        }

        public HHPadList HHPads { get { return hhPads; } }

        public byte Bank
        {
            get { return bank; }
            set
            {
                if (bank == value) return;
                bank = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte FcChannel
        {
            get { return fcChannel; }
            set
            {
                if (fcChannel == value) return;
                /*if (value > 15)
                    throw new ArgumentOutOfRangeException(value.ToString() + " is not a valid Channel value");*/
                fcChannel = value;
                OnDataChanged(this, new EventArgs());
            }
        }

        public String FcCurve
        {
            get { return curve.Value; }
            set { curve.Value = value; }
        }

        public byte BankMSB
        {
            get { return bankMSB; }
            set
            {
                if (bankMSB == value) return;
                /*if (value > 127)
                    throw new ArgumentOutOfRangeException(value.ToString() + " is not a valid Bank LSB value");*/
                bankMSB = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte BankLSB
        {
            get { return bankLSB; }
            set
            {
                if (bankLSB == value) return;
                /*if (value > 127)
                    throw new ArgumentOutOfRangeException(value.ToString() + " is not a valid Bank LSB value");*/
                bankLSB = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte Unused
        {
            get { return unused; }
            set
            {
                if (unused == value) return;
                unused = value;
                OnDataChanged(this, new EventArgs());
            }
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
                if (KitName == value) return;
                if (value.Length > 12)
                    throw new ArgumentException("KitName must be 12 characters or fewer");
                int i = 0;
                char[] c = value.ToCharArray();
                byte[] k = new byte[12];
                while (i < value.Length)
                {
                    if (c[i] >= 32 && c[i] <= 126) k[i] = (byte)c[i];
                    i++;
                }
                while (i < k.Length) { k[i++] = (byte)' '; }
                kitName = k;
                OnDataChanged(this, new EventArgs());
            }
        }
        public void UnserializeKitName(System.IO.BinaryReader r) { kitName = r.ReadBytes(12); }
        public void SerializeKitName(System.IO.BinaryWriter w) { w.Write(kitName); }
    }
}
