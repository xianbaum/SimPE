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


		protected override string Prefix { get { return "[global]"; } }

	}

	public class LocalWiz : ANameBHAVWiz
	{
		public LocalWiz(Instruction i) : base(i)
		{
			group = 0xffffffff;
			if (i != null && i.Parent != null && i.Parent.FileDescriptor != null)
				group = i.Parent.FileDescriptor.Group;
		}

		public static implicit operator LocalWiz(AbstractWrapper parent)
		{
			if ((parent is Bhav) && !(((ABhavNameWiz)(Bhav)parent) is LocalWiz))
				throw new InvalidCastException("Can't cast non-local BHAVs to LocalWiz");

			return new LocalWiz(new Instruction(parent, 0x1000));
		}


		protected override string Prefix { get { return "[private]"; } }

	}

	public class SemiGlobalWiz : ANameBHAVWiz
	{
		public SemiGlobalWiz(Instruction i) : base(i)
		{
			if ((i.Parent is Bhav) && (((ABhavNameWiz)(Bhav)i.Parent) is SemiGlobalWiz))
				group = SemiGlobalGroup;
			else
				group = ((LocalWiz)i.Parent).SemiGlobalGroup;
		}

		public static implicit operator SemiGlobalWiz(AbstractWrapper parent)
		{
			if ((parent is Bhav) && (((ABhavNameWiz)(Bhav)parent) is GlobalWiz))
				throw new InvalidCastException("Can't cast Global BHAVs to SemiGlobalWiz");
			return new SemiGlobalWiz(new Instruction(parent, 0x2000));
		}


		protected override string Prefix { get { return "[semi]"; } }

	}


	public class PrimWizDefault : ANamePrimitiveWiz
	{
		public PrimWizDefault(Instruction i) : base(i) {}
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
