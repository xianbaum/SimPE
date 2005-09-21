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
		public GlobalWiz(Instruction i) : base(i) {}

		public override string LongName { get { string s = base.LongName; return (s!=null ? s : ShortName); } }



		public override Bhav LoadBHAV()
		{
			return (instruction == null || instruction.Parent == null ||
				instruction.Parent.Opcodes == null || instruction.Parent.Package == null || instruction.Parent.FileDescriptor == null)
				? null
				: sLoadBHAV(instruction.Parent.Package, 0x7FD46CD0, instruction.OpCode, instruction.Parent.Opcodes);
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
		public LocalWiz(Instruction i) : base(i) {}

		public override string ShortName { get { return "[private] " + base.ShortName; } }
		public override string LongName { get { string s = base.LongName; return (s != null ? "[private] " + s : ShortName); } }

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
		public SemiGlobalWiz(Instruction i) : base(i) {}

		public override string ShortName { get { return "[semiglobal] " + base.ShortName; } }
		public override string LongName { get { string s = base.LongName; return (s != null ? "[semiglobal] " + s : ShortName); } }
		public override Bhav LoadBHAV()
		{
			if (instruction == null || instruction.Parent == null ||
				instruction.Parent.Opcodes == null || instruction.Parent.Package == null || instruction.Parent.FileDescriptor == null)
				return null;

			SimPe.Interfaces.Files.IPackageFile pkg = instruction.Parent.Package;
			uint group = instruction.Parent.FileDescriptor.Group;

			if (!(((ABhavNameWiz)instruction.Parent.FileDescriptor.Instance) is SemiGlobalWiz))
			{
				SimPe.Interfaces.Files.IPackedFileDescriptor pfd =
					pkg.FindFile(SimPe.Data.MetaData.GLOB_FILE, 0, group, 0x01);

				if (pfd == null) 
				{
					SimPe.Interfaces.Providers.IOpcodeProvider o = instruction.Parent.Opcodes;
					if (o.BasePackage == null) o.LoadPackage();
					if (o.BasePackage == null)
						return null;
					pkg = o.BasePackage;
					pfd = o.BasePackage.FindFile(SimPe.Data.MetaData.GLOB_FILE, 0, group, 0x01);
				}

				if (pfd == null)
					return null;

				Glob g = new Glob();
				g.ProcessData(pfd, pkg);
				group = g.SemiGlobalGroup;
			}

			// always look in current package first, even if GLOB was in BasePackage
			return sLoadBHAV(instruction.Parent.Package, group, instruction.OpCode, instruction.Parent.Opcodes);
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
		public PrimWizDefault(Instruction i) : base(i) {}
		public override string LongName { get { return ShortName + " (0x" + SimPe.Helper.HexString(instruction.OpCode) + ")"; } }

	}

	public class PrimWiz0x0000 : ANamePrimitiveWiz
	{
		public PrimWiz0x0000(Instruction i) : base(i) {}
		public override string LongName
		{
			get
			{
				return ShortName
					+ " for " + dataOwner(9, ToShort(instruction.Operands[0], instruction.Operands[1])) + " ticks."
					//+ " (0x" + SimPe.Helper.HexString(instruction.OpCode) + ")"
					;
			}
		}

	}


}
