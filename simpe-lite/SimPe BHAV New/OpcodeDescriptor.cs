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
using SimPe.Interfaces.Plugin;

namespace SimPe.Plugin
{
	/// <summary>
	/// Zusammenfassung für OpcodeDescriptor.
	/// </summary>
	public class OpcodeDescriptor : SimPe.Interfaces.Plugin.IOpcodeDescriptor
	{
		ushort opcode;
		uint group;

		/// <summary>
		/// Creat a new Descriptor Instance
		/// </summary>
		/// <param name="opcode">The Opcode</param>
		/// <param name="semigroup">The SemiGlobal Group for this Opcode or 0 if any</param>
		public OpcodeDescriptor(ushort opcode, uint semigroup)
		{
			this.opcode = opcode;
			this.group = semigroup;
		}

		/// <summary>
		/// Creat a new Descriptor Instance
		/// </summary>
		/// <param name="OpCode">The Opcode</param>
		public OpcodeDescriptor(ushort OpCode)
		{
			this.opcode = OpCode;
			this.group = 0;
		}

		#region IOpcodeDescriptor Member

		public ushort OpCode
		{
			get
			{
				return opcode;
			}
		}

		public uint SemiGlobalGroup
		{
			get
			{
				return group;
			}
		}

		#endregion

		public override bool Equals(object obj)
		{
			// Check for null values and compare run-time types.
			if ((obj == null) || ((typeof(IOpcodeDescriptor) != obj.GetType().GetInterface("IOpcodeDescriptor")) && (GetType() != obj.GetType()))) 
				return false;
			IOpcodeDescriptor od = (IOpcodeDescriptor)obj;
			return (opcode == od.OpCode) && ( (group==0) || (od.SemiGlobalGroup==0) || (od.SemiGlobalGroup == group) );
		}

		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}


		public static bool operator ==(OpcodeDescriptor x, IOpcodeDescriptor y) 
		{
			return x.Equals(y);
		}

		public static bool operator ==(OpcodeDescriptor x, ushort y) 
		{
			return x.opcode==y;
		}

		public static bool operator !=(OpcodeDescriptor x, IOpcodeDescriptor y) 
		{
			return !x.Equals(y);
		}

		public static bool operator !=(OpcodeDescriptor x, ushort y) 
		{
			return x.opcode!=y;
		}

	}
}
