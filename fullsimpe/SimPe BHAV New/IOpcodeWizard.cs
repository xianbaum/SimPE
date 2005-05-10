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
using SimPe.Plugin;
using System.Windows.Forms;
using System.Drawing;

namespace SimPe.Interfaces.Plugin
{
	/// <summary>
	/// Used to Display a Opcode Wizard Panel
	/// </summary>
	public interface IOpcodeWizard
	{
		/// <summary>
		/// Called, when the Wizard for an Instruction has to be displayed
		/// </summary>
		/// <param name="inst">The Instruction that should be displayed</param>
		/// <param name="semigroup">The SimeGlobal Group this Instruction belongs to</param>
		/// <param name="update">null or a Delegate for a Method that should be called in 
		/// order to update the Data presented in the Plugin View. The Panel has to call
		/// this delegate in Order to let the Parent Application know that the Data was 
		/// changed</param>
		/// <returns>The Panel you have to show</returns>
		Panel Show(Instruction inst, DecoderRegistry.ForceUpdate update);
	}
}
