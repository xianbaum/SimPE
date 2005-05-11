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
using SimPe.Interfaces.Plugin;

namespace SimPe.Plugin
{
	/// <summary>
	/// Just a Dummy Override to implement a new ToString Methof
	/// </summary>
	public class Instruction2 : Instruction
	{
		

		public Instruction2(int index, Bhav2 parent) : base(index, (Bhav)parent) 
		{
			decoders = new ArrayList();
		}

		public Instruction2(Instruction i, Bhav2 parent) : base(i.Index, (Bhav)parent) 
		{
			decoders = new ArrayList();

			this.OpCode = i.OpCode;
			this.Operands = i.Operands;
			this.Operands2 = i.Operands2;
			this.Reserved0 = i.Reserved0;
			this.Target1 = i.Target1;
			this.Target2 = i.Target2;
		}

		ArrayList decoders;
		public override ushort OpCode 
		{
			get { return base.OpCode; }
			set { 
				base.OpCode = value; 
				decoders = WrapperFactory2.DecoderRegistry.FindDecoders(new OpcodeDescriptor(value, this.SemiGlobalGroup));
			}
		}

		/// <summary>
		/// Returns null or a Opcode Wizard
		/// </summary>
		public IOpcodeWizard Wizard
		{
			get 
			{
				foreach (IBhavInstructionDecoder bid in decoders) 
				{
					if (bid.Wizard!=null) return bid.Wizard;
				}

				return null;
			}
		}

		public override string ToString()
		{
			foreach (IBhavInstructionDecoder bid in decoders) 
			{
				string g = bid.ListName(this);
				if (g!=null) return g;
			}

			return Index.ToString()+": "+base.ToString();
		}

	}
}
