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
using pjse.BhavNameWizards;

namespace pjse
{
	/// <summary>
	/// Provides the operand wizard for a given Bhav Instruction.
	/// </summary>
	public class BhavNameWizProvider
	{
		public static ABhavNameWiz For(Bhav parent, ushort opcode, byte[] operands)
		{
			if (opcode < 0x0100)
			{
				switch(opcode)
				{
					case 0x0000:
						return new PrimWiz0x0000(parent, opcode, operands);
					case 0x0001:
						return new PrimWiz0x0001(parent, opcode, operands);
					case 0x0002:
						return new PrimWiz0x0002(parent, opcode, operands);
					case 0x0008:
					case 0x000B:
					case 0x000C:
					case 0x0016:
					case 0x001E:
					default:
						break;
				}
				return new PrimWizDefault(parent, opcode, operands);
			}
			if (opcode < 0x1000) return new BhavNameWizards.GlobalWiz(parent, opcode, operands);
			if (opcode < 0x2000) return new BhavNameWizards.LocalWiz(parent, opcode, operands);
			return new BhavNameWizards.SemiGlobalWiz(parent, opcode, operands);
		}

		public static ABhavNameWiz For(Instruction i)
		{
			if (i.OpCode < 0x0100)
			{
				switch(i.OpCode)
				{
					case 0x0000:
						return new PrimWiz0x0000(i);
					case 0x0001:
						return new PrimWiz0x0001(i);
					case 0x0002:
						return new PrimWiz0x0002(i);
					case 0x0008:
					case 0x000B:
					case 0x000C:
					case 0x0016:
					case 0x001E:
					default:
						break;
				}
				return new PrimWizDefault(i);
			}
			if (i.OpCode < 0x1000) return new BhavNameWizards.GlobalWiz(i);
			if (i.OpCode < 0x2000) return new BhavNameWizards.LocalWiz(i);
			return new BhavNameWizards.SemiGlobalWiz(i);
		}

	}


	/// <summary>
	/// Abstract class that NameWizard providers are based
	/// </summary>
	public abstract class ABhavNameWiz
	{
		protected Instruction instruction = null;

		public ABhavNameWiz(Instruction instruction) { this.instruction = instruction; }

		public ABhavNameWiz(Bhav parent, ushort opcode, byte[] operands)
		{
			this.instruction = new Instruction(parent, opcode, 0, 0, 0, operands, new byte[8]);
		}

		public virtual bool isPrimitive { get { return false; } }
		public virtual bool isBhav { get { return false; } }
		public virtual bool isGlobalBhav { get { return false; } }
		public virtual bool isSemiGlobalBhav { get { return false; } }
		public virtual bool isLocalBhav { get { return false; } }

		public abstract Bhav LoadBHAV();

		public abstract string ShortName { get; }
		public abstract string LongName { get; }

		public override string ToString()
		{
			if (instruction == null) return ShortName;
			return LongName;
		}


		protected string dataOwner(byte doid, ushort instance)
		{
			string doidName = GS.DataOwnerName(doid);

			string s = null;
			switch (doid)
			{
				case 0x03:
				case 0x04:
					s = GS.GStr(0x8d, instance);
					break;
				case 0x06:
					s = GS.GStr(0x81, instance);
					break;
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
					s = "(0x" + SimPe.Helper.HexString((byte)instance) + " " + GS.MotiveName(instance) + ")";
					break;
				case 0x12:
				case 0x13:
				case 0x20:
					s = "- " + GS.GStr(0xc8, instance);
					break;
				case 0x15:
				case 0x26:
				case 0x33:
					s = GS.GStr(0xcc, instance);
					break;
				case 0x17:
					s = GS.GStr(0xdb, instance);
					break;
				case 0x18:
					s = GS.GStr(0xdd, instance);
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
							+ " (" + readBcon((uint)b, c) + ")";
					}
					else
					{
						doidName = GS.DataOwnerName(0x1a);
						s = "0x" + SimPe.Helper.HexString((ushort)b) + ":[Temp " + c.ToString() + "]";
					}
					break;
				case 0x21:
					s = GS.GStr(0xf3, instance);
					break;
				case 0x22:
					s = GS.GStr(0xf9, instance);
					break;
				case 0x23:
					s = GS.GStr(0xf5, instance);
					break;
				case 0x27:
				case 0x28:
					s = GS.GStr(0xfc, instance);
					break;
			}
			if (s == null) s = "0x" + SimPe.Helper.HexString(instance);

			return doidName + (s.Length > 0 ? " " + s : "");
		}

		private string readBcon(uint instance, int bid)
		{
			SimPe.Interfaces.Files.IPackageFile pkg = null;
			SimPe.Interfaces.Files.IPackedFileDescriptor pfd = null;
			uint group = 0x0;
			if (instance < 0x1000)
			{
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
			return ((short)bcon.Constants[bid]).ToString(); //"0x" + SimPe.Helper.HexString((ushort)bcon.Constants[bid]);
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
		public ANamePrimitiveWiz(Bhav parent, ushort opcode, byte[] operands) : base (parent, opcode, operands) { }
		public ANamePrimitiveWiz(Bhav parent, byte[] operands) : base(parent, 0, operands) { }
		public ANamePrimitiveWiz(Instruction instruction) : base (instruction) { }
		public override bool isPrimitive { get { return true; } }

		public override Bhav LoadBHAV() { return null; }
		public override string ShortName { get { return GS.PrimitiveName(instruction.OpCode); } }
	}


	/// <summary>
	/// Abstract class for BHAV name providers (global, local, semiglobal)
	/// </summary>
	public abstract class ANameBHAVWiz : ABhavNameWiz
	{
		public ANameBHAVWiz(Bhav parent, ushort opcode, byte[] operands) : base (parent, opcode, operands) { }
		public ANameBHAVWiz(Instruction instruction) : base (instruction) { }
		public override bool isBhav { get { return true; } }
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
				if (b == null) return ShortName;

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
						{
							byte doid = parms[i*3];
							ushort instance = (ushort)(parms[(i*3) + 1] + 256 * parms[(i*3) + 2]);
							s += (i>0 ? ", " : "") + dataOwner(doid, instance);
						}
					}
					else if ((parms[12] & 0x02) != 0)
					{
						s = "caller's first " + b.Header.ArgumentCount.ToString() + " params";
					}
					else
					{
						for (int i = 0; i < b.Header.ArgumentCount && i < 4; i++)
						{
							ushort val = (ushort)(parms[(i*2)] + 256 * parms[(i*2) + 1]);
							s += (i>0 ? ", " : "") + "0x" + SimPe.Helper.HexString(val);
						}
					}
				}

				return b.FileName + " (" + b.Header.ArgumentCount.ToString() + " args" + ((s.Length > 0) ? ": " + s : "") + ")";
			}
		}

	}
}
