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
using System.Windows.Forms;
using SimPe.PackedFiles.Wrapper;
using pjse.BhavOperandWizards;

namespace pjse
{
	/// <summary>
	/// Provides the operand wizard for a given Bhav Instruction.
	/// </summary>
	public class BhavOperandWizProvider
	{
		public static ABhavOperandWiz For(SimPe.PackedFiles.Wrapper.Instruction i)
		{
			if (i.OpCode < 0x0100)
				switch(i.OpCode)
				{
					case 0x0000:
						//return new BhavOperandWiz0x0000(i);
					case 0x0001:
						return new BhavOperandWiz0x0001(i);
					case 0x0002:
						return new BhavOperandWiz0x0002(i);
					case 0x0008:
					case 0x000B:
					case 0x000C:
					case 0x0016:
					case 0x001E:
						break;
					default:
						return null;
				}
			return null;
		}

	}

	/// <summary>
	/// Abstract class for BHAV Operand Wizards to extend
	/// </summary>
	public abstract class ABhavOperandWiz
	{
		protected Instruction instruction = null;
		protected ABhavOperandWiz() {}
		protected ABhavOperandWiz(Instruction instruction) { this.instruction = instruction; }


		public abstract Panel bhavPrimWizPanel { get; }
		public abstract void Execute();
		public abstract Instruction Write();
	}

}
