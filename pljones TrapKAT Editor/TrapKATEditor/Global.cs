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
    public class Global : DataItem
    {
        #region Attributes
        byte[] currentDefaults = new byte[128];
        byte[] userDefaults = new byte[128];

        // 21 bytes
        byte beeperStatus;
        byte bcFunction;
        byte chokeFunction;
        byte fcClosedRegion;
        byte fcPolarity;
        byte bcPolarity;
        byte bcLowLevel;
        byte bcHighLevel;
        byte fcLowLevel;
        byte fcHighLevel;
        byte fcVelocityLevel;
        byte fcWaitModeLevel;
        byte instrumentID;
        byte kitNumber;
        byte kitNumberUser;
        byte kitNumberDemo;
        byte motifNumber;
        byte motifNumberPerc;
        byte motifNumberMel;
        byte midiMergeStatus;
        byte fcOpenRegion;

        private PadDynamics[] padLevels = new PadDynamics[24];

        // 10 bytes
        byte trigLowLevel;
        byte trigHighLevel;
        byte trigGain; // (0 - 3)
        byte prgChgRcvChn; // Program change receive channel
        byte displayAngle;
        byte playMode; // (0 -> demo, 1 -> user)
        byte grooveVol;
        byte grooveStatus; // (1 -> grooves enables)
        byte fcSplashEase;
        byte noteNamesStatus;

        byte[] ttPadData = new byte[12];    // 12

        // 5 bytes
        byte hatNoteGate; // HAT NOTE gate time index
        byte grooveAutoOff; // enabled if > 0
        byte kitNumberKAT;
        byte ttMeter; // Tap tempo meter (quarter, half, eighth, etc.)
        byte hearSoundStatus; // (1 -> on)

        byte[] unused1 = new byte[160];
        byte[] userMargin = new byte[25];
        byte[] unused2 = new byte[231];
        byte[] internalMargin = new byte[25];
        byte[] unused3 = new byte[231];
        byte[] thresholdManual = new byte[25];
        byte[] unused4 = new byte[231];
        byte[] thresholdActual = new byte[25];
        #endregion

        public byte InstrumentID { get { return instrumentID; } }

        public Global() : base()
        {
            for (int i = 0; i < padLevels.Length; i++)
            {
                padLevels[i] = new PadDynamics();
                padLevels[i].DataChanged += new EventHandler(dataChanged);

            }
        }

        void dataChanged(object sender, EventArgs e)
        {
            OnDataChanged(this, new EventArgs());
        }
        public Global(System.IO.BinaryReader r) : base(r) { }
        protected override void Unserialize(System.IO.BinaryReader r)
        {
            currentDefaults = r.ReadBytes(currentDefaults.Length);
            userDefaults = r.ReadBytes(userDefaults.Length);
            beeperStatus = r.ReadByte();
            bcFunction = r.ReadByte();
            chokeFunction = r.ReadByte();
            fcClosedRegion = r.ReadByte();
            fcPolarity = r.ReadByte();
            bcPolarity = r.ReadByte();
            bcLowLevel = r.ReadByte();
            bcHighLevel = r.ReadByte();
            fcLowLevel = r.ReadByte();
            fcHighLevel = r.ReadByte();
            fcVelocityLevel = r.ReadByte();
            fcWaitModeLevel = r.ReadByte();
            instrumentID = r.ReadByte();
            kitNumber = r.ReadByte();
            kitNumberUser = r.ReadByte();
            kitNumberDemo = r.ReadByte();
            motifNumber = r.ReadByte();
            motifNumberPerc = r.ReadByte();
            motifNumberMel = r.ReadByte();
            midiMergeStatus = r.ReadByte();
            fcOpenRegion = r.ReadByte();

            for (int i = 0; i < padLevels.Length; i++)
                padLevels[i] = new PadDynamics(r);

            trigLowLevel = r.ReadByte();
            trigHighLevel = r.ReadByte();
            trigGain = r.ReadByte();
            prgChgRcvChn = r.ReadByte();
            displayAngle = r.ReadByte();
            playMode = r.ReadByte();
            grooveVol = r.ReadByte();
            grooveStatus = r.ReadByte();
            fcSplashEase = r.ReadByte();
            noteNamesStatus = r.ReadByte();

            ttPadData = r.ReadBytes(ttPadData.Length);

            hatNoteGate = r.ReadByte();
            grooveAutoOff = r.ReadByte();
            kitNumberKAT = r.ReadByte();
            ttMeter = r.ReadByte();
            hearSoundStatus = r.ReadByte();

            unused1 = r.ReadBytes(unused1.Length);
            userMargin = r.ReadBytes(userMargin.Length);
            unused2 = r.ReadBytes(unused2.Length);
            internalMargin = r.ReadBytes(internalMargin.Length);
            unused3 = r.ReadBytes(unused3.Length);
            thresholdManual = r.ReadBytes(thresholdManual.Length);
            unused4 = r.ReadBytes(unused4.Length);
            thresholdActual = r.ReadBytes(thresholdActual.Length);
        }
        public void Serialize(System.IO.BinaryWriter w)
        {
            w.Write(currentDefaults);
            w.Write(userDefaults);
            w.Write(beeperStatus);
            w.Write(bcFunction);
            w.Write(chokeFunction);
            w.Write(fcClosedRegion);
            w.Write(fcPolarity);
            w.Write(bcPolarity);
            w.Write(bcLowLevel);
            w.Write(bcHighLevel);
            w.Write(fcLowLevel);
            w.Write(fcHighLevel);
            w.Write(fcVelocityLevel);
            w.Write(fcWaitModeLevel);
            w.Write(instrumentID);
            w.Write(kitNumber);
            w.Write(kitNumberUser);
            w.Write(kitNumberDemo);
            w.Write(motifNumber);
            w.Write(motifNumberPerc);
            w.Write(motifNumberMel);
            w.Write(midiMergeStatus);
            w.Write(fcOpenRegion);

            foreach (PadDynamics pl in padLevels) pl.Serialize(w);

            w.Write(trigLowLevel);
            w.Write(trigHighLevel);
            w.Write(trigGain);
            w.Write(prgChgRcvChn);
            w.Write(displayAngle);
            w.Write(playMode);
            w.Write(grooveVol);
            w.Write(grooveStatus);
            w.Write(fcSplashEase);
            w.Write(noteNamesStatus);

            w.Write(ttPadData);

            w.Write(hatNoteGate);
            w.Write(grooveAutoOff);
            w.Write(kitNumberKAT);
            w.Write(ttMeter);
            w.Write(hearSoundStatus);

            w.Write(unused1);
            w.Write(userMargin);
            w.Write(unused2);
            w.Write(internalMargin);
            w.Write(unused3);
            w.Write(thresholdManual);
            w.Write(unused4);
            w.Write(thresholdActual);
        }

        public List<String> Modes = new List<string>(new String[] { "Off",
            "Pitchbend Up", "Pitchbend Down", "Expression", "Sustain",});
    }

    public class PadDynamics : DataItem
    {
        #region Attributes
        byte lowLevel;
        byte highLevel;
        #endregion

        public PadDynamics() : base() { }
        public PadDynamics(System.IO.BinaryReader r) : base(r) { }
        protected override void Unserialize(System.IO.BinaryReader r)
        {
            lowLevel = r.ReadByte();
            highLevel = r.ReadByte();
        }
        public void Serialize(System.IO.BinaryWriter w)
        {
            w.Write(lowLevel);
            w.Write(highLevel);
        }
        public byte Low
        {
            get { return lowLevel; }
            set
            {
                if (lowLevel == value) return;
                lowLevel = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte High
        {
            get { return highLevel; }
            set
            {
                if (highLevel == value) return;
                highLevel = value;
                OnDataChanged(this, new EventArgs());
            }
        }
    }
}
