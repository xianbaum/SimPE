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
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.UserInterface;
using SimPe.PackedFiles.Wrapper;

namespace pjse.BhavNameWizards
{
	public class GlobalWiz : ANameBHAVWiz
	{
		private static ushort globalStart = 0x0100;
		public GlobalWiz(Instruction i) : base(i) { group = 0x7FD46CD0; start = globalStart; }

		public static implicit operator GlobalWiz(AbstractWrapper parent)
		{
			if (parent is Bhav) return new GlobalWiz(new Instruction((Bhav)parent, globalStart));
			else
			{
				Bhav b = new Bhav(null);
				b.Package = parent.Package;
				b.FileDescriptor = parent.FileDescriptor;
				return new GlobalWiz(new Instruction(b, globalStart));
			}
		}


		public override string LongName { get { string s = base.LongName; return (s != null ? s : ShortName); } }

	}

	public class LocalWiz : ANameBHAVWiz
	{
		private static ushort localStart = 0x1000;
		public LocalWiz(Instruction i) : base(i) { group = (i != null && i.Parent != null) ? i.Parent.FileDescriptor.Group : 0xffffffff; start = localStart; }

		public static implicit operator LocalWiz(AbstractWrapper parent)
		{
			if (parent is Bhav) return new LocalWiz(new Instruction((Bhav)parent, localStart));
			else
			{
				Bhav b = new Bhav(null);
				b.Package = parent.Package;
				b.FileDescriptor = parent.FileDescriptor;
				return new LocalWiz(new Instruction(b, localStart));
			}
		}


		public override string ShortName { get { return "[private] " + base.ShortName; } }
		public override string LongName { get { string s = base.LongName; return (s != null ? "[private] " + s : ShortName); } }

	}

	public class SemiGlobalWiz : ANameBHAVWiz
	{
		private static ushort semiStart = 0x2000;
		public SemiGlobalWiz(Instruction i) : base(i)
		{
			group = 0xffffff;
			start = semiStart;

			semiGroupName = "SemiGlobals";
			if (i != null && i.Parent != null && i.Parent.FileDescriptor != null)
			{
				if ((((ABhavNameWiz)i.Parent.FileDescriptor.Instance) is SemiGlobalWiz))
				{
					group = i.Parent.FileDescriptor.Group;
				}
				else
				{
					SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem[] items =
						SimPe.FileTable.FileIndex.FindFile(SimPe.Data.MetaData.GLOB_FILE, i.Parent.FileDescriptor.Group, (ulong)1, null);
					if (items != null && items.Length > 0)
					{
						Glob glob = new Glob();
						glob.ProcessData(items[0]);

						group = glob.SemiGlobalGroup;
						semiGroupName = glob.SemiGlobalName;
					}
				}
			}
		}

		public static implicit operator SemiGlobalWiz(AbstractWrapper parent)
		{
			if (parent is Bhav) return new SemiGlobalWiz(new Instruction((Bhav)parent, semiStart));
			else
			{
				Bhav b = new Bhav(null);
				b.Package = parent.Package;
				b.FileDescriptor = parent.FileDescriptor;
				return new SemiGlobalWiz(new Instruction(b, semiStart));
			}
		}


		public override string ShortName { get { return "[semiglobal] " + base.ShortName; } }
		public override string LongName { get { string s = base.LongName; return (s != null ? "[semiglobal] " + s : ShortName); } }


		private static string semiGroupName = null;
		public static string SemiGroupName { get { return semiGroupName; } }

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
