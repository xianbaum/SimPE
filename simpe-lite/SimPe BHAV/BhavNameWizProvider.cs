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
			string doidName = instruction.Parent.Opcodes.FindExpressionDataOwners(doid);
			if (doidName == null) doidName = "[Unknown data owner: 0x" + SimPe.Helper.HexString(doid) + "] ";

			string s = null;
			switch (doid)
			{
				case 0x03:
				case 0x04:
					s = gStr(0x8d, instance);
					break;
				case 0x06:
					s = gStr(0x81, instance);
					break;
				case 0x07:
					doidName = instance.ToString() + " [0x" + SimPe.Helper.HexString(instance) + "]";
					s = "";
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
					s = "(" + instruction.Parent.Opcodes.FindMotives(instance) + ")";
					break;
				case 0x12:
				case 0x13:
				case 0x20:
					s = "- " + gStr(0xc8, instance);
					break;
				case 0x15:
				case 0x26:
				case 0x33:
					s = gStr(0xcc, instance);
					break;
				case 0x17:
					s = gStr(0xdb, instance);
					break;
				case 0x18:
					s = gStr(0xdd, instance);
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
						doidName = instruction.Parent.Opcodes.FindExpressionDataOwners(0x1a);
						s = "0x" + SimPe.Helper.HexString((ushort)b) + ":[Temp " + c.ToString() + "]";
					}
					break;
				case 0x21:
					s = gStr(0xf3, instance);
					break;
				case 0x22:
					s = gStr(0xf9, instance);
					break;
				case 0x23:
					s = gStr(0xf5, instance);
					break;
				case 0x27:
				case 0x28:
					s = gStr(0xfc, instance);
					break;
			}
			if (s == null) s = "0x" + SimPe.Helper.HexString(instance);

			return doidName + (s.Length > 0 ? " " + s : "");
		}


		private string readStr(uint group, uint instance, int sid)
		{
			if (instruction.Parent == null || instruction.Parent.Package == null) return null;
			SimPe.Interfaces.Files.IPackedFileDescriptor pfd =
				instruction.Parent.Package.FindFile(SimPe.Data.MetaData.STRING_FILE, 0, group, instance);
			if (pfd == null) return null;

			Str s = new Str();
			s.ProcessData(pfd, instruction.Parent.Package);
			return s[1, sid].Title;
		}

		private string gStr(uint instance, int sid)
		{
			if (instruction.Parent == null || instruction.Parent.Package == null) return null;
			instruction.Parent.Opcodes.LoadPackage();
			if (instruction.Parent.Opcodes.BasePackage == null) return null;

			SimPe.Interfaces.Files.IPackedFileDescriptor pfd =
				instruction.Parent.Opcodes.BasePackage.FindFile(SimPe.Data.MetaData.STRING_FILE, 0, 0x7FE59FD0, instance);
			if (pfd == null) return null;

			Str s = new Str();
			s.ProcessData(pfd, instruction.Parent.Opcodes.BasePackage);
			StrItem si = s[1, sid];
			return (si == null) ? null : si.Title;
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


#if UNDEF
		#region data owner names
		private string[] dataOwners =
			{
				"My Attribute" // 0x00
				,"Stack Obj's Attribute"
				,"My Semi Attribute"
				,"My"
				,"Stack Object's" // 0x04
				,"Stack Object's Semi Attribute"
				,"Global"
				,"(Literal Value)"
				,"Temp" // 0x08
				,"param no"
				,"Stack Object"
				,"Temp"
				,"check tree ad range" // 0x0c
				,"stack obj's Temp"
				,"my motives"
				,"stack obj's motives"
				,"stack object's slot" // 0x10
				,"stack obj's motive"
				,"my person data"
				,"stack obj's person data"
				,"my slot" // 0x14
				,"stack object's definition"
				,"stack obj attr [stack param]"
				,"room [Temp 0]"
				,"neighbor in stack object" // 0x18
				,"Local"
				,"Const"
				,"Unused - Stack Object's Dynamic Sprite Flags Of Temp (Sims1)"
				,"check tree ad personality var" // 0x1c
				,"check tree ad min"
				,"my person data"
				,"stack obj's person data"
				,"neighbor's person data" // 0x20
				,"job data [Temp 0,1]"
				,"neighborhood data"
				,"stack object's function"
				,"my type attr" // 0x24
				,"stack obj's type attr"
				,"Neighbor's Object Definition"
				,"Temporary Token"
				,"Stack Object's Temporary Token" // 0x28
				,"My Object Array Iterator Index"
				,"Stack Object's Object Array Iterator Index"
				,"My Object Array Iterator Data"
				,"Stack Object's Object Array Iterator Data" // 0x2c
				,"My Object Array Element At Temp"
				,"Stack Object's Object Array Element At Temp"
				,"Const"
				,"My Slot" // 0x30
				,"Stack Object's Slot"
				,"stack obj Semi attr [stack param]"
				,"Stack Object's Master Definition"
			};
		#endregion
#endif
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
		public override string ShortName
		{
			get
			{
				string s = null;
				if (instruction.Parent.Opcodes.StoredPrimitives != null &&
					instruction.OpCode < instruction.Parent.Opcodes.StoredPrimitives.Count)
					s = (string)instruction.Parent.Opcodes.StoredPrimitives[instruction.OpCode];
				return (s == null ? "Unknown opcode" : s) + " (0x" + SimPe.Helper.HexString(instruction.OpCode) +")";
			}
		}
#if UNDEF
		public static int Length { get { return gPrims.Length; } }
		public static string Name(int index)
		{
			return (index < 0 || index >= gPrims.Length) ? null : gPrims[index];
		}
		#region Primitive names
		protected static string[] gPrims =
			{
				"Sleep" // 0x00
				,"Generic Sims Call"
				,"Expression"
				,"Find Best Interaction"
				,"~(old)Grab" // 0x04
				,"~(old)Drop"
				,"~(old)Change Suit"
				,"Refresh"
				,"Random Number" // 0x08
				,"~(old)Burn"
				,"Tutorial"
				,"Get Distance To"
				,"Get Direction To" // 0x0c
				,"Push Interaction"
				,"Find Best Object for Function"
				,"Break Point"
				,"Find Location For" // 0x10
				,"Idle for Input"
				,"Remove Object Instance"
				,"Make New Character"
				,"Run Functional Tree" // 0x14
				,"~Show String ( UNUSED )"
				,"Turn Body Towards"
				,"Play / Stop Sound Event"
				,"~UNUSED (was old relationship)" // 0x18
				,"Alter Budget"
				,"Relationship"
				,"Go To Relative Position"
				,"Run Tree by Name" // 0x1c
				,"Set Motive Change"
				,"Gosub Found Action"
				,"Set to Next"
				,"Test Object Type" // 0x20
				,"Find 5 Worst Motives"
				,"UI Effect"
				,"Camera Control"
				,"Dialog" // 0x24
				,"Test Sim Interacting With"
				,"~unused"
				,"~unused"
				,"~unused" // 0x28
				,"~(old)Set Balloon/Headline"
				,"Create New Object Instance"
				,"~(old)Drop Onto"
				,"~(old)Animate Sim [old]" // 0x2c
				,"Go To Routing Slot"
				,"Snap"
				,"~(old)Reach"
				,"Stop ALL Sounds" // 0x30
				,"Notify the Stack Object out of Idle"
				,"Add/Change the Action String"
				,"Manage Inventory"
				,"~unused (TSO)" // 0x34
				,"~unused (TSO)"
				,"~unused (TSO)"
				,"~unused (TSO)"
				,"~unused (TSO)" // 0x38
				,"~unused"
				,"~unused"
				,"~unused"
				,"~unused" // 0x3c
				,"~unused"
				,"~unused"
				,"~unused"
				,"~unused" // 0x40
				,"~unused"
				,"~unused"
				,"~unused"
				,"~unused" // 0x44
				,"~unused"
				,"~unused"
				,"~unused"
				,"~unused" // 0x48
				,"~unused"
				,"~unused"
				,"~unused"
				,"~unused" // 0x4c
				,"~unused"
				,"~unused"
				,"~unused"
				,"~unused" // 0x50
				,"~unused"
				,"~unused"
				,"~unused"
				,"~unused" // 0x54
				,"~unused"
				,"~unused"
				,"~unused"
				,"~unused" // 0x58
				,"~unused"
				,"~unused"
				,"~unused"
				,"~unused" // 0x5c
				,"~unused"
				,"~unused"
				,"~unused"
				,"~unused" // 0x60
				,"~unused"
				,"~unused"
				,"~unused"
				,"~reserved" // 0x64
				,"~unused"
				,"~unused"
				,"~unused"
				,"~unused" // 0x68
				,"Animate Object"
				,"Animate Sim"
				,"Animate Overlay"
				,"Animate Stop" // 0x6c
				,"Change Material"
				,"Look At"
				,"Change Light"
				,"Effect Stop/Start" // 0x70
				,"Snap Into"
				,"Assign Locomotion Animations"
				,"Debug"
				,"Reach/Put" // 0x74
				,"Age"
				,"Array Operation"
				,"Message"
				,"RayTrace" // 0x78
				,"Change Outfit"
				,"On Timer"
				,"Cinematic"
				,"Want Satisfy" // 0x7c
				,"Influence"
			};
		#endregion
#endif
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
			get {
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
