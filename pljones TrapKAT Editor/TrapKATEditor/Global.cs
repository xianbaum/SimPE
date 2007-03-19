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

        byte[] padLevels = new byte[50];

        // 10 bytes
        byte trigGain; // (0 - 3)
        byte prgChgRcvChn; // Program change receive channel
        byte displayAngle;
        byte playMode; // (0 -> demo, 1 -> user)
        byte grooveVol;
        byte grooveStatus; // (1 -> grooves enables)
        byte fcSplashEase;
        byte noteNamesStatus;

        byte[] ttPadData = new byte[12];

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

        private PadDynamics[] padDynamics = new PadDynamics[25];
        #endregion

        public Global() : base()
        {
            for (int i = 0; i < padDynamics.Length; i++)
            {
                padDynamics[i] = new PadDynamics();
                padDynamics[i].DataChanged += new EventHandler(dataChanged);
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

            padLevels = r.ReadBytes(padLevels.Length);

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

            for (int i = 0; i < padDynamics.Length; i++)
            {
                padDynamics[i] = new PadDynamics(padLevels[i * 2], padLevels[i * 2 + 1], userMargin[i],
                    internalMargin[i], thresholdManual[i], thresholdActual[i]);
                padDynamics[i].DataChanged += new EventHandler(dataChanged);
            }
        }
        public override void Serialize(System.IO.BinaryWriter w)
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

            w.Write(padLevels);

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

        public byte BeeperStatus
        {
            get { return beeperStatus; }
            set
            {
                if (beeperStatus == value) return;
                beeperStatus = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte BCFunction
        {
            get { return bcFunction; }
            set
            {
                if (bcFunction == value) return;
                bcFunction = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte ChokeFunction
        {
            get { return chokeFunction; }
            set
            {
                if (chokeFunction == value) return;
                chokeFunction = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte FCClosedRegion
        {
            get { return fcClosedRegion; }
            set
            {
                if (fcClosedRegion == value) return;
                fcClosedRegion = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte FCPolarity
        {
            get { return fcPolarity; }
            set
            {
                if (fcPolarity == value) return;
                fcPolarity = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte BCPolarity
        {
            get { return bcPolarity; }
            set
            {
                if (bcPolarity == value) return;
                bcPolarity = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte BCLowLevel
        {
            get { return bcLowLevel; }
            set
            {
                if (bcLowLevel == value) return;
                bcLowLevel = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte BCHighLevel
        {
            get { return bcHighLevel; }
            set
            {
                if (bcHighLevel == value) return;
                bcHighLevel = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte FCLowLevel
        {
            get { return fcLowLevel; }
            set
            {
                if (fcLowLevel == value) return;
                fcLowLevel = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte FCHighLevel
        {
            get { return fcHighLevel; }
            set
            {
                if (fcHighLevel == value) return;
                fcHighLevel = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte FCVelocityLevel
        {
            get { return fcVelocityLevel; }
            set
            {
                if (fcVelocityLevel == value) return;
                fcVelocityLevel = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte FCWaitModeLevel
        {
            get { return fcWaitModeLevel; }
            set
            {
                if (fcWaitModeLevel == value) return;
                fcWaitModeLevel = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte InstrumentID
        {
            get { return instrumentID; }
            set
            {
                if (instrumentID == value) return;
                instrumentID = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte KitNumber
        {
            get { return kitNumber; }
            set
            {
                if (kitNumber == value) return;
                kitNumber = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte KitNumberUser
        {
            get { return kitNumberUser; }
            set
            {
                if (kitNumberUser == value) return;
                kitNumberUser = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte KitNumberDemo
        {
            get { return kitNumberDemo; }
            set
            {
                if (kitNumberDemo == value) return;
                kitNumberDemo = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte MotifNumber
        {
            get { return motifNumber; }
            set
            {
                if (motifNumber == value) return;
                motifNumber = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte MotifNumberPerc
        {
            get { return motifNumberPerc; }
            set
            {
                if (motifNumberPerc == value) return;
                motifNumberPerc = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte MotifNumberMel
        {
            get { return motifNumberMel; }
            set
            {
                if (motifNumberMel == value) return;
                motifNumberMel = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte MidiMergeStatus
        {
            get { return midiMergeStatus; }
            set
            {
                if (midiMergeStatus == value) return;
                midiMergeStatus = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte FCOpenRegion
        {
            get { return fcOpenRegion; }
            set
            {
                if (fcOpenRegion == value) return;
                fcOpenRegion = value;
                OnDataChanged(this, new EventArgs());
            }
        }

        public byte TrigGain
        {
            get { return trigGain; }
            set
            {
                if (trigGain == value) return;
                trigGain = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte PrgChgRcvChn
        {
            get { return prgChgRcvChn; }
            set
            {
                if (prgChgRcvChn == value) return;
                prgChgRcvChn = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte DisplayAngle
        {
            get { return displayAngle; }
            set
            {
                if (displayAngle == value) return;
                displayAngle = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte PlayMode
        {
            get { return playMode; }
            set
            {
                if (playMode == value) return;
                playMode = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte GrooveVol
        {
            get { return grooveVol; }
            set
            {
                if (grooveVol == value) return;
                grooveVol = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte GrooveStatus
        {
            get { return grooveStatus; }
            set
            {
                if (grooveStatus == value) return;
                grooveStatus = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte FCSplashEase
        {
            get { return fcSplashEase; }
            set
            {
                if (fcSplashEase == value) return;
                fcSplashEase = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte NoteNamesStatus
        {
            get { return noteNamesStatus; }
            set
            {
                if (noteNamesStatus == value) return;
                noteNamesStatus = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte HatNoteGate
        {
            get { return hatNoteGate; }
            set
            {
                if (hatNoteGate == value) return;
                hatNoteGate = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte GrooveAutoOff
        {
            get { return grooveAutoOff; }
            set
            {
                if (grooveAutoOff == value) return;
                grooveAutoOff = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte KitNumberKAT
        {
            get { return kitNumberKAT; }
            set
            {
                if (kitNumberKAT == value) return;
                kitNumberKAT = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte TTMeter
        {
            get { return ttMeter; }
            set
            {
                if (ttMeter == value) return;
                ttMeter = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte HearSoundStatus
        {
            get { return hearSoundStatus; }
            set
            {
                if (hearSoundStatus == value) return;
                hearSoundStatus = value;
                OnDataChanged(this, new EventArgs());
            }
        }

        public PadDynamics this[int index] { get { return padDynamics[index]; } }

        public readonly List<String> PBModes = new List<string>(new String[] {
            "Off", "Pitchbend Up", "Pitchbend Down", "Expression", "Sustain",
        });
    }

    public class PadDynamics : DataItem
    {
        #region Attributes
        byte lowLevel;
        byte highLevel;
        byte userMargin;
        byte internalMargin;
        byte thresholdManual;
        byte thresholdActual;
        #endregion

        public PadDynamics() : base() { }
        public PadDynamics(byte lowLevel, byte highLevel, byte userMargin, byte internalMargin, byte thresholdManual, byte thresholdActual)
        {
            this.lowLevel = lowLevel;
            this.highLevel = highLevel;
            this.userMargin = userMargin;
            this.internalMargin = internalMargin;
            this.thresholdManual = thresholdManual;
            this.thresholdActual = thresholdActual;
        }
        public PadDynamics(System.IO.BinaryReader r) : base(r) { }
        protected override void Unserialize(System.IO.BinaryReader r) { throw new Exception("This should never happen"); }
        public override void Serialize(System.IO.BinaryWriter w) { throw new Exception("This should never happen"); }
        public byte LowLevel
        {
            get { return lowLevel; }
            set
            {
                if (lowLevel == value) return;
                lowLevel = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte HighLevel
        {
            get { return highLevel; }
            set
            {
                if (highLevel == value) return;
                highLevel = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte UserMargin
        {
            get { return userMargin; }
            set
            {
                if (userMargin == value) return;
                userMargin = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte InternalMargin
        {
            get { return internalMargin; }
            set
            {
                if (internalMargin == value) return;
                internalMargin = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte ThresholdManual
        {
            get { return thresholdManual; }
            set
            {
                if (thresholdManual == value) return;
                thresholdManual = value;
                OnDataChanged(this, new EventArgs());
            }
        }
        public byte ThresholdActual
        {
            get { return thresholdActual; }
            set
            {
                if (thresholdActual == value) return;
                thresholdActual = value;
                OnDataChanged(this, new EventArgs());
            }
        }
    }
}
