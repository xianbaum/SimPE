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
using SimPe.Interfaces.Files;
using SimPe.PackedFiles.UserInterface;
using SimPe.PackedFiles.Wrapper;

namespace pjse.BhavNameWizards
{
	public class GlobalWiz : ANameBHAVWiz
	{
		public GlobalWiz(Bhav parent, ushort opcode, byte[] operands) : base(parent, opcode, operands) {}
		public GlobalWiz(Instruction i) : base(i) {}
		public new bool isGlobalBhav { get { return true; } }
		//public override string ShortName { get { return "to do"; } }
		public override Bhav LoadBHAV()
		{
			if (instruction.Parent == null) return null;

			IPackedFileDescriptor pfd =  instruction.Parent.Opcodes.LoadGlobalBHAV(instruction.Opcode);
			if (pfd==null) return null;

			Bhav b = new Bhav(instruction.Parent.Opcodes);
			b.ProcessData(pfd, instruction.Parent.Opcodes.BasePackage);
			return b;
		}
	}

	public class SemiGlobalWiz : ANameBHAVWiz
	{
		public SemiGlobalWiz(Bhav parent, ushort opcode, byte[] operands) : base(parent, opcode, operands) {}
		public SemiGlobalWiz(Instruction i) : base(i) {}
		public new bool isSemiGlobalBhav { get { return true; } }
		public override string ShortName { get { return "[semiglobal] " + base.ShortName; } }
		public override string LongName { get { return "[semiglobal] " + base.LongName; } }
		public override Bhav LoadBHAV()
		{
			if (instruction.Parent == null) return null;
			IPackedFileDescriptor pfd = instruction.Parent.Package.FindFile(SimPe.Data.MetaData.BHAV_FILE, 0, instruction.Parent.FileDescriptor.Group, instruction.Opcode);
			if (pfd==null)  
			{
				pfd = instruction.Parent.Package.FindFile(SimPe.Data.MetaData.GLOB_FILE, 0, instruction.Parent.FileDescriptor.Group, 0x01);
				if (pfd==null) 
				{
					IPackedFileDescriptor[] pfds = instruction.Parent.Package.FindFiles(SimPe.Data.MetaData.GLOB_FILE);
					if (pfds.Length>0) pfd=pfds[0];

					foreach (IPackedFileDescriptor p in pfds) 
					{
						if (p.Group == instruction.Parent.FileDescriptor.Group) pfd = p;
					}
				}
				if (pfd==null) return null;

				Glob g = new Glob();
				g.ProcessData(pfd, instruction.Parent.Package);
				pfd = instruction.Parent.Opcodes.LoadSemiGlobalBHAV(instruction.Opcode, g.SemiGlobalGroup);
			
				if (pfd==null) return null;
				Bhav b = new Bhav(instruction.Parent.Opcodes);
				b.ProcessData(pfd, instruction.Parent.Opcodes.BasePackage);
				return b;
			} 
			else 
			{
				Bhav b = new Bhav(instruction.Parent.Opcodes);
				b.ProcessData(pfd, instruction.Parent.Package);
				return b;
			}
			
		}

	}

	public class LocalWiz : ANameBHAVWiz
	{
		public LocalWiz(Bhav parent, ushort opcode, byte[] operands) : base(parent, opcode, operands) {}
		public LocalWiz(Instruction i) : base(i) {}
		public new bool isLocalBhav { get { return true; } }

		public override string ShortName { get { return "[private] " + base.ShortName; } }
		public override string LongName { get { return "[private] " + base.LongName; } }
		public override Bhav LoadBHAV()
		{
			if (instruction.Parent == null) return null;
			if (instruction.Parent.Package==null) return new Bhav(instruction.Parent.Opcodes);
			IPackedFileDescriptor pfd =  instruction.Parent.Package.FindFile(SimPe.Data.MetaData.BHAV_FILE, 0, instruction.Parent.FileDescriptor.Group, instruction.Opcode);
			if (pfd==null) return new Bhav(instruction.Parent.Opcodes);

			Bhav b = new Bhav(instruction.Parent.Opcodes);
			b.ProcessData(pfd, instruction.Parent.Package);
			return b;
		}

	}


	public class PrimWizDefault : ANamePrimitiveWiz
	{
		public PrimWizDefault(Bhav parent, ushort opcode, byte[] operands) : base(parent, opcode, operands) {}
		public PrimWizDefault(Instruction i) : base(i) {}
		public override string LongName
		{
			get
			{
				return ShortName;
			}
		}

	}

	public class PrimWiz0x0000 : ANamePrimitiveWiz
	{
		public PrimWiz0x0000(Bhav parent, ushort opcode, byte[] operands) : base(parent, opcode, operands) {}
		public PrimWiz0x0000(Instruction i) : base(i) {}
		public override string LongName
		{
			get
			{
				ushort word = (ushort)(instruction.Operands[0] + 256 * instruction.Operands[1]);
				return ShortName + " for " + dataOwner(9, word) + " ticks.";
			}
		}

	}


}
