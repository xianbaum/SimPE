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
using SimPe.Interfaces.Plugin;

namespace SimPe.Plugin.Decoder
{
	/// <summary>
	/// Zusammenfassung für Class1.
	/// </summary>
	public class TestDecoder : SimPe.Interfaces.Plugin.IBhavInstructionDecoder
	{
		public TestDecoder()
		{
		}

		#region IBhavInstructionDecoder Member

		public string PseudoCodeName(Instruction inst)
		{
			return null;
		}

		public string ListName(Instruction inst)
		{
			return "You are my Test";
		}

		public IOpcodeDescriptor[] Opcodes
		{
			get
			{
				IOpcodeDescriptor[] ops = {
											  new SimPe.Plugin.OpcodeDescriptor(0x2011),
											  new SimPe.Plugin.OpcodeDescriptor(0x0118, 0x7F890CAC)
										  };

				return ops;
			}
		}

		public IOpcodeWizard Wizard 
		{
			get
			{
				return null;
			}
		}
		#endregion
	}
}
