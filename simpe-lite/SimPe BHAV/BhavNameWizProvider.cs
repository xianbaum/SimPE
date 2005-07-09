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
			if (i.Opcode < 0x0100)
			{
				switch(i.Opcode)
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
			if (i.Opcode < 0x1000) return new BhavNameWizards.GlobalWiz(i);
			if (i.Opcode < 0x2000) return new BhavNameWizards.LocalWiz(i);
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
			this.instruction = new Instruction(parent);
			instruction.Opcode = opcode;
			instruction.Operands = new wrappedByteArray(instruction, operands);
		}

		public bool isPrimitive { get { return false; } }
		public bool isBhav { get { return false; } }
		public bool isGlobalBhav { get { return false; } }
		public bool isSemiGlobalBhav { get { return false; } }
		public bool isLocalBhav { get { return false; } }

		public abstract Bhav LoadBHAV() ;//{ return null; }

		public abstract string ShortName { get; }
		public abstract string LongName { get; }

		public override string ToString()
		{
			if (instruction == null) return ShortName;
			return LongName;
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
		public new bool isPrimitive { get { return true; } }

		public override Bhav LoadBHAV() { return null; }
		public override string ShortName
		{
			get
			{
				return ((instruction.Opcode < gPrims.Length) ? gPrims[instruction.Opcode] : "Unknown opcode") + " (0x" + SimPe.Helper.HexString(instruction.Opcode) +")";
			}
		}
		public static int Length { get { return gPrims.Length; } }
		public static string Name(int index) { return (index < 0 || index >= gPrims.Length) ? null : gPrims[index]; }
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
		protected string dataOwner(byte doid, ushort instance)
		{
			string s = null;
			switch(doid)
			{
				case 0:
				case 1:
					if (instruction.Parent != null && instruction.Parent.FileDescriptor != null)
						s = readStr(instruction.Parent.FileDescriptor.Group, 0x0100, instance);
					if (s != null && !s.Trim().Equals("")) s = " [Group: " + s + "]";
					else s = " [Unknown Group]";
					break;
			}
			return dataOwners[doid] + " 0x" + SimPe.Helper.HexString(instance) + s;
		}
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
				,"parameter number"
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
		private string readStr(uint group, uint instance, int sid)
		{
			if (instruction.Parent == null || instruction.Parent.Package == null) return null;
			SimPe.Interfaces.Files.IPackedFileDescriptor pfd =
				instruction.Parent.Package.FindFile(SimPe.Data.MetaData.STRING_FILE, 0, group, instance);
			if (pfd == null) return null;
			Str s = new Str();
			s.ProcessData(pfd, instruction.Parent.Package);
			return s[0x01, sid].Title;
		}
	}


	/// <summary>
	/// Abstract class for BHAV name providers (global, local, semiglobal)
	/// </summary>
	public abstract class ANameBHAVWiz : ABhavNameWiz
	{
		public ANameBHAVWiz(Bhav parent, ushort opcode, byte[] operands) : base (parent, opcode, operands) { }
		public ANameBHAVWiz(Instruction instruction) : base (instruction) { }
		public new bool isBhav { get { return true; } }
		public override string ShortName
		{
			get {
				Bhav b = LoadBHAV();
				return ((b != null) ? b.FileName : "(BHAV not found)") + " (0x" + SimPe.Helper.HexString(instruction.Opcode) +")";
			}
		}
		public override string LongName
		{
			get
			{
				Bhav b = LoadBHAV();
				if (b == null) return ShortName;
				return b.FileName + " (Args: " + b.Header.ArgumentCount.ToString() + ")";
			}
		}

	}

}
