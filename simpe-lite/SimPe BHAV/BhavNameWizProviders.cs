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
			if (instruction.Parent == null || instruction.Parent.Opcodes == null) return null;

			IPackedFileDescriptor pfd = instruction.Parent.Opcodes.LoadGlobalBHAV(instruction.OpCode);
			if (pfd==null) return null;

			Bhav b = new Bhav(instruction.Parent.Opcodes);
			b.ProcessData(pfd, instruction.Parent.Opcodes.BasePackage);
			return b;
		}


		private static System.Collections.Hashtable bhavs = null;
		private static ushort next = 0x0000;
		public static SimPe.Data.Alias First(Bhav parent)
		{
			Init(parent);
			next = 0x0000;
			return Next();
		}

		public static SimPe.Data.Alias Next()
		{
			if (bhavs == null) return null;

			if (next < 0x0100)
				next = 0x0100;

			while (next < 0x1000 && !bhavs.ContainsKey((uint)next)) next++;
			if (next >= 0x1000)
				return null;

			string s = (string)bhavs[(uint)next];
			SimPe.Data.Alias a = new SimPe.Data.Alias(next, s);
			next++;
			return a;
		}

		public static void Init(Bhav parent)
		{
			if (bhavs != null) return;

			if (parent == null || parent.Opcodes == null || parent.FileDescriptor == null) return;

			bhavs = new Hashtable();

			Bhav bhav = new Bhav(parent.Opcodes);
			bhav.Package = parent.Package;
			bhav.FileDescriptor = parent.FileDescriptor;

			if (bhav.Opcodes.BasePackage == null)
				bhav.Opcodes.LoadPackage();
			SimPe.Interfaces.Files.IPackedFileDescriptor[] pfds = bhav.Opcodes.BasePackage.FindFiles(SimPe.Data.MetaData.BHAV_FILE);
			foreach (SimPe.Interfaces.Files.IPackedFileDescriptor pfd in pfds) 
			{
				if (/*(pfd.Instance>=0x0100) && (pfd.Instance<0x1000) &&*/ (pfd.Group==0x7FD46CD0)) // Global BHAVs
				{
					bhav.ProcessData(pfd, bhav.Opcodes.BasePackage);
					bhavs.Add(bhav.FileDescriptor.Instance, bhav.FileName);
				} 
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
			IPackedFileDescriptor pfd =  instruction.Parent.Package.FindFile(SimPe.Data.MetaData.BHAV_FILE, 0, instruction.Parent.FileDescriptor.Group, instruction.OpCode);
			if (pfd==null) return new Bhav(instruction.Parent.Opcodes);

			Bhav b = new Bhav(instruction.Parent.Opcodes);
			b.ProcessData(pfd, instruction.Parent.Package);
			return b;
		}


		private static System.Collections.Hashtable bhavs = null;
		private static ushort next = 0x0000;
		private static SimPe.Interfaces.Files.IPackageFile lastPackage = null;
		private static uint lastGroup = 0x00000000;
		public static SimPe.Data.Alias First(Bhav parent)
		{
			Init(parent);
			next = 0x0000;
			return Next();
		}

		public static SimPe.Data.Alias Next()
		{
			if (bhavs == null) return null;

			if (next < 0x1000)
				next = 0x1000;

			while (next < 0x2000 && !bhavs.ContainsKey((uint)next)) next++;
			if (next >= 0x2000)
				return null;

			string s = (string)bhavs[(uint)next];
			SimPe.Data.Alias a = new SimPe.Data.Alias(next, s);
			next++;
			return a;
		}

		public static void Init(Bhav parent)
		{
			next = 0x0000;

			if (parent == null || parent.Opcodes == null || parent.Package == null || parent.FileDescriptor == null) return;
			if (lastPackage != null && lastPackage == parent.Package && lastGroup == parent.FileDescriptor.Group && bhavs != null) return;

			if (parent.FileDescriptor.Instance < 0x1000 || parent.FileDescriptor.Instance >= 0x2000) // editing a Global or SemiGlobal
			{
				bhavs = null;
				return;
			}

			bhavs = new Hashtable();
			lastPackage = parent.Package;
			lastGroup = parent.FileDescriptor.Group;

			Bhav bhav = new Bhav(parent.Opcodes);
			bhav.Package = parent.Package;
			bhav.FileDescriptor = parent.FileDescriptor;

			SimPe.Interfaces.Files.IPackedFileDescriptor[] pfds = bhav.Package.FindFiles(SimPe.Data.MetaData.BHAV_FILE);
			foreach (SimPe.Interfaces.Files.IPackedFileDescriptor pfd in pfds) 
			{
				if (/*(pfd.Instance>=0x1000) && (pfd.Instance<0x2000) &&*/ (pfd.Group==lastGroup)) 
				{
					bhav.ProcessData(pfd, bhav.Package);
					bhavs.Add(bhav.FileDescriptor.Instance, bhav.FileName);
				} 
			}
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
			IPackedFileDescriptor pfd = instruction.Parent.Package.FindFile(SimPe.Data.MetaData.BHAV_FILE, 0, instruction.Parent.FileDescriptor.Group, instruction.OpCode);
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
				pfd = instruction.Parent.Opcodes.LoadSemiGlobalBHAV(instruction.OpCode, g.SemiGlobalGroup);
			
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


		private static System.Collections.Hashtable bhavs = null;
		private static ushort next = 0x0000;
		private static SimPe.Interfaces.Files.IPackageFile lastPackage = null;
		private static uint lastSemiGroup = 0x00000000;
		private static string semiGroupName = null;
		public static string SemiGroupName { get { return semiGroupName; } }

		public static bool SameGroup(Bhav parent)
		{
			if (parent == null || parent.Package == null || parent.FileDescriptor == null) return false;

			SimPe.Interfaces.Files.IPackageFile pkg = null;
			uint group = 0x0;

			if (parent.FileDescriptor.Instance < 0x1000) // editing a Global
			{
				return false;
			}
			else if (parent.FileDescriptor.Instance >= 0x2000) // editing a Semi
			{
				pkg = parent.Package;
				group = parent.FileDescriptor.Group;
			}
			else
			{
				SimPe.Interfaces.Files.IPackedFileDescriptor gpfd = 
					parent.Package.FindFile(SimPe.Data.MetaData.GLOB_FILE, 0, parent.FileDescriptor.Group, 0x01);
				if (gpfd == null)
				{
					return false;
				}

				Glob glob = new Glob();
				glob.ProcessData(gpfd, parent.Package);

				if (parent.Opcodes.BasePackage == null)
					parent.Opcodes.LoadPackage();

				pkg = parent.Opcodes.BasePackage;
				group = glob.SemiGlobalGroup;
			}

			return (lastPackage == pkg && lastSemiGroup == group);
		}
		public static SimPe.Data.Alias First(Bhav parent)
		{
			Init(parent);
			next = 0x0000;
			return Next();
		}

		public static SimPe.Data.Alias Next()
		{
			if (bhavs == null) return null;

			if (next < 0x2000)
				next = 0x2000;

			while (next < 0xffff && !bhavs.ContainsKey((uint)next)) next++;
			if (next >= 0xffff)
				return null;

			string s = (string)bhavs[(uint)next];
			SimPe.Data.Alias a = new SimPe.Data.Alias(next, s);
			next++;
			return a;
		}

		public static void Init(Bhav parent)
		{
			if (bhavs != null && SameGroup(parent)) return;

			if (parent == null || parent.Package == null || parent.FileDescriptor == null || parent.Opcodes == null) return;

			SimPe.Interfaces.Files.IPackageFile pkg = null;
			uint group = 0x0;

			if (parent.FileDescriptor.Instance < 0x1000) // editing a Global
			{
				bhavs = null;
				return;
			}
			else if (parent.FileDescriptor.Instance >= 0x2000) // editing a Semi
			{
				pkg = parent.Package;
				group = parent.FileDescriptor.Group;
				semiGroupName = "SemiGlobals";
			}
			else
			{
				SimPe.Interfaces.Files.IPackedFileDescriptor gpfd = 
					parent.Package.FindFile(SimPe.Data.MetaData.GLOB_FILE, 0, parent.FileDescriptor.Group, 0x01);
				if (gpfd == null)
				{
					bhavs = null;
					return;
				}

				Glob glob = new Glob();
				glob.ProcessData(gpfd, parent.Package);

				if (parent.Opcodes.BasePackage == null)
					parent.Opcodes.LoadPackage();

				pkg = parent.Opcodes.BasePackage;
				group = glob.SemiGlobalGroup;
				semiGroupName = glob.SemiGlobalName;
			}


			if (lastSemiGroup == group && lastPackage == pkg && bhavs != null) return;

			bhavs = new Hashtable();
			lastSemiGroup = group;
			lastPackage = pkg;

			Bhav bhav = new Bhav(parent.Opcodes);
			bhav.Package = parent.Package;
			bhav.FileDescriptor = parent.FileDescriptor;

			SimPe.Interfaces.Files.IPackedFileDescriptor[] pfds = pkg.FindFiles(SimPe.Data.MetaData.BHAV_FILE);
			foreach (SimPe.Interfaces.Files.IPackedFileDescriptor pfd in pfds) 
			{
				if (/*(pfd.Instance>=0x2000) &&*/ (pfd.Group == lastSemiGroup))
				{
					bhav.ProcessData(pfd, pkg);
					bhavs.Add(bhav.FileDescriptor.Instance, bhav.FileName);
				} 
			}
		}

	}


	public class PrimWizDefault : ANamePrimitiveWiz
	{
		public PrimWizDefault(Bhav parent, ushort opcode, byte[] operands) : base(parent, opcode, operands) {}
		public PrimWizDefault(Instruction i) : base(i) {}
		public override string LongName { get { return ShortName + " (0x" + SimPe.Helper.HexString(instruction.OpCode) + ")"; } }

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
