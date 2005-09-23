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
		public GlobalWiz(Instruction i) : base(i) { group = 0x7FD46CD0; }

		public static implicit operator GlobalWiz(AbstractWrapper parent)
		{
			return new GlobalWiz(new Instruction(parent, 0x0100));
		}


		public override string LongName { get { string s = base.LongName; return (s != null ? s : ShortName); } }

	}

	public class LocalWiz : ANameBHAVWiz
	{
		public LocalWiz(Instruction i) : base(i)
		{
			group = 0xffffffff;
			if (i != null && i.Parent != null && i.Parent.FileDescriptor != null)
				if (i.Parent is Bhav)
				{
					ABhavNameWiz wiz = new Instruction(null, (ushort)i.Parent.FileDescriptor.Instance);
					if (wiz is LocalWiz)
						group = i.Parent.FileDescriptor.Group;
				}
				else
				{
					group = i.Parent.FileDescriptor.Group;
				}
		}

		public static implicit operator LocalWiz(AbstractWrapper parent)
		{
			return new LocalWiz(new Instruction(parent, 0x1000));
		}


		public override string ShortName { get { return "[private] " + base.ShortName; } }
		public override string LongName { get { string s = base.LongName; return (s != null ? "[private] " + s : ShortName); } }

	}

	public class SemiGlobalWiz : ANameBHAVWiz
	{
		public SemiGlobalWiz(Instruction i) : base(i)
		{
			group = 0;
			semiGroupName = "SemiGlobals";

			if (i != null && i.Parent != null && i.Parent.FileDescriptor != null)
			{
				if (i.Parent is Bhav)
				{
					ABhavNameWiz wiz = new Instruction(null, (ushort)i.Parent.FileDescriptor.Instance);
					if (wiz is SemiGlobalWiz)
					{
						group = i.Parent.FileDescriptor.Group;
						return;
					}
				}
				Glob g = this.SemiGlobal;
				if (g != null)
				{
					group = g.SemiGlobalGroup;
					semiGroupName = g.SemiGlobalName;
				}
			}
		}

		public static implicit operator SemiGlobalWiz(AbstractWrapper parent)
		{
			return new SemiGlobalWiz(new Instruction(parent, 0x2000));
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
