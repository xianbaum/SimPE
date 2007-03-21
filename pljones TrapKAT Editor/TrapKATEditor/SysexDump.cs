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
using System.IO;
using TrapKATEditor.Data;

namespace TrapKATEditor.Dump
{
    public enum DumpType : byte
    {
        Global = 0,
        AllMemory = 1,
        Kit = 3,
        NotSet = 0xff,
    }

    public class SysexDump
    {
        static long[] expectedLength = new long[] { 2620, 21364, 0, 746 };

        static byte SysExStart = 0xf0;
        static byte[] CompanyID = new byte[] { 0x00, 0x00, 0x15, };
        static byte TrapKATID = 0x63;
        static byte Version = 0x40;
        static byte SysExEnd = 0xf7;

        protected DumpType dumpType = DumpType.NotSet;
        protected byte instrumentID = 0;
        protected byte auxType;

        public static implicit operator SysexDump(Global global) { return new GlobalDump(global); }
        public static implicit operator SysexDump(AllMemory allMemory) { return new AllMemoryDump(allMemory); }
        public static implicit operator SysexDump(Kit kit) { return new KitDump(kit); }

        public SysexDump() { }
        protected SysexDump(DumpType dumpType, byte instrumentID, byte auxType)
        {
            this.dumpType = dumpType;
            this.instrumentID = instrumentID;
            this.auxType = auxType;
        }

        public class OpenException : Exception
        {
            public enum ExceptionType
            {
                UnexpectedFileLength,
                SysExStartMissing,
                BadCompanyID,
                BadTrapKATID,
                UnknownDumpType,
                UnsupportedVersion,
                UnknownAuxType,
                SysExEndMissing
            }
            private ExceptionType type;
            public ExceptionType Type { get { return type; } }
            private long offset;
            public long Offset { get { return offset; } }
            public OpenException(ExceptionType type, long offset) { this.type = type; this.offset = offset; }
        }

        /// <summary>
        /// Opens a file containing TrapKAT SysEx data
        /// </summary>
        /// <param name="filename">Fully qualified, validated filename</param>
        /// <param name="type">Type of dump in file</param>
        /// <returns>A new BinaryReader for the data content</returns>
        /// <exception cref="ArgumentOutOfRangeException">If &quot;type&quot; is invalid</exception>
        /// <exception cref="OpenException">Throws various OpenExceptions</exception>
        public SysexDump Open(String filename)
        {
            FileStream f = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
            BinaryReader br = new BinaryReader(f);

            byte aByte;
            byte[] someBytes;
            int position = 0;

            aByte = br.ReadByte();
            if (!aByte.Equals(SysExStart)) throw new OpenException(OpenException.ExceptionType.SysExStartMissing, position);
            position++;

            someBytes = br.ReadBytes(3);
            for (int i = 0; i < someBytes.Length; i++, position++)
                if (!someBytes[i].Equals(CompanyID[i]))
                    throw new OpenException(OpenException.ExceptionType.BadCompanyID, position);

            aByte = br.ReadByte();
            if (!aByte.Equals(TrapKATID)) throw new OpenException(OpenException.ExceptionType.BadTrapKATID, position);
            position++;

            aByte = br.ReadByte();
            if (!Enum.IsDefined(DumpType.AllMemory.GetType(), aByte))
                throw new OpenException(OpenException.ExceptionType.UnknownDumpType, position);
            if (f.Length != expectedLength[aByte])
                throw new OpenException(OpenException.ExceptionType.UnexpectedFileLength, 0);
            dumpType = (DumpType)aByte;
            position++;

            instrumentID = br.ReadByte();
            position++;

            aByte = br.ReadByte();
            if (!aByte.Equals(Version)) throw new OpenException(OpenException.ExceptionType.UnsupportedVersion, position);
            position++;

            auxType = br.ReadByte();
            position++;

            f.Position = f.Length - 1;
            aByte = br.ReadByte();
            if (!aByte.Equals(SysExEnd)) throw new OpenException(OpenException.ExceptionType.SysExEndMissing, position);

            f.Position = position;

            SysexDump dump = null;
            switch (dumpType)
            {
                case DumpType.Global: dump = new GlobalDump(new SysExReader(f)); break;
                case DumpType.AllMemory: dump = new AllMemoryDump(new SysExReader(f)); break;
                case DumpType.Kit: dump = new KitDump(new SysExReader(f)); break;
            }

            br.Close();
            return dump;
        }
        protected virtual void ReadSysEx(SysExReader r) { }

        public void Save(String filename)
        {
            FileStream f = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryWriter bw = new BinaryWriter(f);

            bw.Write(SysExStart);
            bw.Write(CompanyID);
            bw.Write(TrapKATID);
            bw.Write((byte)dumpType);
            bw.Write(instrumentID);
            bw.Write(Version);
            bw.Write(auxType);
            WriteSysEx(new SysExWriter(f));
            bw.Write(SysExEnd);
            bw.Close();
        }
        protected virtual void WriteSysEx(SysExWriter w) { }

        public byte InstrumentID
        {
            get { return instrumentID; }
            set { instrumentID = value; }
        }
    }

    public class GlobalDump : SysexDump
    {
        private Global global;
        public Global Global { get { return global; } }

        public GlobalDump(Global global) : base(DumpType.Global, global.InstrumentID, 0) { this.global = global; }
        public GlobalDump(SysExReader r) : base(DumpType.Global, 0, 0) { ReadSysEx(r); instrumentID = global.InstrumentID; }
        protected override void ReadSysEx(SysExReader r) { global = new Global(r); }
        protected override void WriteSysEx(SysExWriter w) { global.Serialize(w); }
    }

    public class AllMemoryDump : SysexDump
    {
        AllMemory allMemory = null;
        public AllMemory AllMemory { get { return allMemory; } }

        public AllMemoryDump(AllMemory allMemory) : base(DumpType.AllMemory, allMemory.Global.InstrumentID, 0) { this.allMemory = allMemory; }
        public AllMemoryDump(SysExReader r) : base(DumpType.AllMemory, 0, 0) { ReadSysEx(r); instrumentID = allMemory.Global.InstrumentID; }
        protected override void ReadSysEx(SysExReader r) { allMemory = new AllMemory(r); }
        protected override void WriteSysEx(SysExWriter w) { allMemory.Serialize(w); }
    }

    public class KitDump : SysexDump
    {
        private Kit kit = null;
        public Kit Kit { get { return kit; } }

        public KitDump(Kit kit) : base(DumpType.Kit, 0, (byte)MainProgram.CurrentKit) { this.kit = kit; }
        public KitDump(SysExReader r) : base(DumpType.Kit, 0, (byte)MainProgram.CurrentKit) { ReadSysEx(r); }
        protected override void ReadSysEx(SysExReader r) { kit = new Kit(r, true); }
        protected override void WriteSysEx(SysExWriter w) { kit.SerializeWithName(w); }
    }

    public class SysExReader : BinaryReader
    {
        public SysExReader(Stream r) : base(r) { }
        public override byte ReadByte()
        {
            byte lo, hi;
            lo = base.ReadByte();
            hi = base.ReadByte();
            return (byte)(lo + (hi << 4));
        }
        public override byte[] ReadBytes(int count)
        {
            byte[] result = new byte[count];
            for (int i = 0; i < count; i++) result[i] = ReadByte();
            return result;
        }
    }

    public class SysExWriter : BinaryWriter
    {
        public SysExWriter(Stream w) : base(w) { }

        public override void Write(byte value)
        {
            base.Write((byte)(value & 0x0f)); // lo
            base.Write((byte)((value >> 4) & 0x0f)); // hi
        }
        public override void Write(byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++) Write(buffer[i]);
        }
    }
}
