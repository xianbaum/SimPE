/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
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
using System.Collections;
using SimPe.PackedFiles.Wrapper;
using SimPe.Interfaces.Files;
using pjse.BhavNameWizards;

namespace pjse
{
	/// <summary>
	/// Abstract class that NameWizard providers are based
	/// </summary>
	public abstract class ABhavNameWiz
	{
		protected Instruction instruction = null;

		protected ABhavNameWiz(Instruction instruction) { this.instruction = instruction; }

		public static implicit operator ABhavNameWiz(Instruction i)
		{
			if (i.OpCode < 0x0100)
			{
				switch(i.OpCode)
				{
					case 0x0000: return new PrimWiz0x0000(i);
					case 0x0001: return new PrimWiz0x0001(i);
					case 0x0002: return new PrimWiz0x0002(i);
				}
				return new PrimWizDefault(i);
			}
			if (i.OpCode < 0x1000) return new GlobalWiz(i);
			if (i.OpCode < 0x2000) return new LocalWiz(i);
			return new SemiGlobalWiz(i);
		}

		public static implicit operator ABhavNameWiz(ushort opcode)
		{
			return new Instruction(null, opcode);
		}

		public virtual Bhav LoadBHAV() { return null; }

		public abstract string ShortName { get; }

		public abstract string LongName { get; }

		public override string ToString()
		{
			return (LongName != null) ? LongName
				: (ShortName != null ? ShortName
				: "[error: " + SimPe.Helper.HexString(instruction.OpCode));
		}


		private static Hashtable doidGStr = staticInitialiser();
		private static Hashtable staticInitialiser()
		{
			Hashtable t = new Hashtable();
			t.Add((byte)0x03, (uint)0x008d);
			t.Add((byte)0x04, (uint)0x008d);
			t.Add((byte)0x06, (uint)0x0081);
			t.Add((byte)0x12, (uint)0x00c8);
			t.Add((byte)0x13, (uint)0x00c8);
			t.Add((byte)0x20, (uint)0x00c8);
			t.Add((byte)0x15, (uint)0x00cc);
			t.Add((byte)0x26, (uint)0x00cc);
			t.Add((byte)0x33, (uint)0x00cc);
			t.Add((byte)0x17, (uint)0x00db);
			t.Add((byte)0x18, (uint)0x00dd);
			t.Add((byte)0x21, (uint)0x00f3);
			t.Add((byte)0x22, (uint)0x00f9);
			t.Add((byte)0x23, (uint)0x00f5);
			t.Add((byte)0x27, (uint)0x00fc);
			t.Add((byte)0x28, (uint)0x00fc);
			return t;
		}

		protected string dataOwner(byte doid, ushort instance)
		{
			string doidName = GS.GStr(GS.SF.DataOwners, doid);

			string s = null;
			if (doidGStr[doid] != null)
				s = GS.GStr((uint)doidGStr[doid], instance);
			switch (doid)
			{
				case 0x0a:
					if (instance == 0)
						s = "";
					break;
				case 0x0b:
				case 0x11:
				case 0x1e:
				case 0x1f:
				case 0x30:
				case 0x31:
					doidName = doidName.Replace("[temp]", "[Temp " + instance.ToString() + "]");
					s = "";
					break;
				case 0x0c:
				case 0x0e:
				case 0x0f:
				case 0x1c:
				case 0x1d:
					s = "(0x" + SimPe.Helper.HexString((byte)instance) + " " + GS.GStr(GS.SF.Motives, instance) + ")";
					break;
				case 0x1a:
				case 0x2f:
					int a = instance >> 13;            // x = aaabbbbb bccccccc
					int b = (instance >> 7) & 0x3F;
					int c = instance & 0x7F;
					switch (a) 
					{
						case 0:             // private
							b += 0x1000;
							break;
						case 1:             // semi-global
							b += 0x2000;
							break;
						case 2:            // global
							b += 0x100;
							break;
						default:
							b += 0x140;
							break;
					}
					if (doid == 0x1a)
					{
						s = "0x" + SimPe.Helper.HexString((ushort)b) + ":0x" + SimPe.Helper.HexString((byte)c)
							+ " [=" + readBcon((uint)b, c) + "]";
					}
					else
					{
						doidName = GS.GStr(GS.SF.DataOwners, 0x1a);
						s = "0x" + SimPe.Helper.HexString((ushort)b) + ":[Temp " + c.ToString() + "]";
					}
					break;
			}
			if (s == null) s = "0x" + SimPe.Helper.HexString(instance);

			return doidName + (s.Length > 0 ? " " + s : "");
		}


		protected ushort ToShort(byte lower, byte higher)
		{
			return (ushort)((higher << 8) + lower);
		}

		private string readBcon(uint instance, int bid)
		{
			SimPe.Interfaces.Files.IPackageFile pkg = null;
			SimPe.Interfaces.Files.IPackedFileDescriptor pfd = null;
			uint group = 0x0;
			if (instance < 0x1000)
			{
				instruction.Parent.Opcodes.LoadPackage();
				pkg = instruction.Parent.Opcodes.BasePackage;
				group = 0x7FD46CD0;
			}
			else if (instance >= 0x2000 && instance < 0x3000 &&
				instruction.Parent.FileDescriptor.Instance >= 0x1000 && instruction.Parent.FileDescriptor.Instance < 0x2000)
			{
				SimPe.Interfaces.Files.IPackedFileDescriptor gpfd =
					instruction.Parent.Package.FindFile(SimPe.Data.MetaData.GLOB_FILE, 0, instruction.Parent.FileDescriptor.Group, 1);
				if (gpfd == null)
					return "[No GLOB file]";
				Glob glob = new Glob();
				glob.ProcessData(gpfd, instruction.Parent.Package);

				pkg = instruction.Parent.Package;
				group = glob.SemiGlobalGroup;
			}
			else
			{
				pkg = instruction.Parent.Package;
				group = instruction.Parent.FileDescriptor.Group;
			}
			pfd = pkg.FindFile(0x42434F4E, 0, group, instance);
			if (pfd == null)
				return "[No BCON file]";

			Bcon bcon = new Bcon();
			bcon.ProcessData(pfd, pkg);
			if (bid >= bcon.Constants.Count)
				return "[BCON not set]";
			return "0x" + SimPe.Helper.HexString((short)bcon.Constants[bid]);
		}

	}

}


namespace pjse.BhavNameWizards
{
	/// <summary>
	/// Abstract class for primitive name providers
	/// </summary>
	public abstract class ANamePrimitiveWiz : ABhavNameWiz
	{
		public ANamePrimitiveWiz(Instruction instruction) : base (instruction) { }
		// Also provide
		// public static implicit operator <Wiz>(byte[] operands);

		public override Bhav LoadBHAV() { return null; }
		public override string ShortName { get { return GS.GStr(GS.SF.Primitives, instruction.OpCode); } }
	}


	/// <summary>
	/// Abstract class for BHAV name providers (global, local, semiglobal)
	/// </summary>
	public abstract class ANameBHAVWiz : ABhavNameWiz
	{
		public ANameBHAVWiz(Instruction instruction) : base (instruction) { }
		public override string ShortName
		{
			get 
			{
				Bhav b = LoadBHAV();
				return ((b != null) ? b.FileName : "(BHAV not found)") + " (0x" + SimPe.Helper.HexString(instruction.OpCode) +")";
			}
		}

		public override string LongName
		{
			get
			{
				Bhav b = LoadBHAV();
				if (b == null) return null;

				string s = "";

				bool noParms = true;
				for (int i = 0; i < 8 && noParms; i++)
					noParms = instruction.Operands[i] == 0xFF;
				if (!noParms)
				{
					byte[] parms = new byte[16];
					((byte[])instruction.Operands).CopyTo(parms, 0);
					((byte[])instruction.Reserved1).CopyTo(parms, 8);
					if ((parms[12] & 0x01) != 0)
					{
						for (int i = 0; i < b.Header.ArgumentCount && i < 4; i++)
							s += (i>0 ? ", " : ": ") + dataOwner(parms[i*3], ToShort(parms[(i*3) + 1], parms[(i*3) + 2]));
					}
					else if ((parms[12] & 0x02) != 0)
					{
						s = " - caller's params";
					}
					else
					{
						for (int i = 0; i < b.Header.ArgumentCount && i < 4; i++)
							s += (i>0 ? ", " : ": ") + "0x" + SimPe.Helper.HexString(ToShort(parms[(i*2)], parms[(i*2) + 1]));
					}
				}

				return b.FileName + " (" + b.Header.ArgumentCount.ToString() + " args" + ((s.Length > 0) ? s : "") + ")";
			}
		}


		protected static Bhav sLoadBHAV(IPackageFile pkg, uint group, uint instance, SimPe.Interfaces.Providers.IOpcodeProvider o)
		{
			IPackedFileDescriptor pfd = null;

			pfd = pkg.FindFile(SimPe.Data.MetaData.BHAV_FILE, 0x0, group, instance);

			if (pfd == null)
			{
				if (o.BasePackage == null) o.LoadPackage();
				pkg = o.BasePackage;
				if (pkg == null) return null;
				pfd = pkg.FindFile(SimPe.Data.MetaData.BHAV_FILE, 0x0, group, instance);
			}

			if (pfd == null) return null;

			Bhav b = new Bhav(o);
			b.ProcessData(pfd, pkg);
			return b;
		}

		public override Bhav LoadBHAV()
		{
			return (instruction == null || instruction.Parent == null ||
				instruction.Parent.Opcodes == null || instruction.Parent.Package == null || instruction.Parent.FileDescriptor == null)
				? null
				: sLoadBHAV(instruction.Parent.Package, instruction.Parent.FileDescriptor.Group, instruction.OpCode, instruction.Parent.Opcodes);
		}
	}
}
