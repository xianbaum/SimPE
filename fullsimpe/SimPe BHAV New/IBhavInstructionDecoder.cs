/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
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
using SimPe.Plugin;

namespace SimPe.Interfaces.Plugin
{
	/// <summary>
	/// Used to decode a Bhav Instruction
	/// </summary>
	public interface IBhavInstructionDecoder
	{
		/// <summary>
		/// Get the String of this Instruction, used to display it within a ListBox
		/// </summary>
		/// <param name="inst">The Instruction to decode</param>
		/// <returns>The String</returns>
		/// <remarks>The Decoder was unable to process the instruction if null is returned</remarks>
		string ListName(Instruction inst);

		/// <summary>
		/// Get the String of this Instruction, used to display in a Pseudo Language
		/// </summary>
		/// <param name="inst">The Instruction to decode</param>
		/// <returns>The String</returns>
		/// <remarks>The Decoder was unable to process the instruction if null is returned</remarks>
		string PseudoCodeName(Instruction inst);

		/// <summary>
		/// List all Opcodes this Decoder can process.
		/// </summary>
		/// <remarks>If an empty Array is returned, this Decoder will be used as one of the Fallback Decoders</remarks>
		IOpcodeDescriptor[] Opcodes
		{
			get;
		}

		/// <summary>
		/// Returns a Panel you can use to set the Opcodes for an Instruction
		/// </summary>
		/// <remarks>If null is returned, this Decoder offers no Wizard</remarks>
		IOpcodeWizard Wizard 
		{
			get;
		}
	}
}
