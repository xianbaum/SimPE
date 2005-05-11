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
	/// Zusammenfassung für FallbackDecoder.
	/// </summary>
	public class FallbackDecoder :  SimPe.Interfaces.Plugin.IBhavInstructionDecoder, IOpcodeWizard
	{
		public FallbackDecoder()
		{
			
		}

		#region IBhavInstructionDecoder Member

		public string PseudoCodeName(Instruction inst)
		{
			return null;
		}

		public IOpcodeWizard Wizard
		{
			get
			{
				return this;
			}
		}

		public string ListName(Instruction inst)
		{
			return null;
		}

		public IOpcodeDescriptor[] Opcodes
		{
			get
			{
				return new IOpcodeDescriptor[0];
			}
		}

		#endregion

		#region IOpcodeWizard Member

		public System.Windows.Forms.Panel Show(Instruction inst, SimPe.Plugin.DecoderRegistry.ForceUpdate update)
		{
			FallbackForm ff = new FallbackForm();
			ff.inst = inst;
			ff.forceupdate = update;

			ff.tbop1.Tag = true;
			try 
			{
				ff.tbop1.Text = Helper.BytesToHexList(inst.Operands);
				ff.tbop2.Text = Helper.BytesToHexList(inst.Operands2);
			} 
			finally 
			{
				ff.tbop1.Tag = null;
			}
			return ff.pnfallback;
		}

		#endregion
	}
}
