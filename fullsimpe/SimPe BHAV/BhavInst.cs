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
using System.IO;

namespace SimPe.PackedFiles.Wrapper
{
	/// <summary>
	/// Class representing an Instruction
	/// </summary>
	public class Instruction
		: InstructionName
	{
		#region Attributes
		private ushort opcode = 0;
		private ushort addr1 = 0;
		private ushort addr2 = 0;
		private byte reserved_00 = 0;
		private byte[] operands = new byte[8];
		private byte[] reserved_01 = null;
		#endregion

		#region Accessor methods
		public virtual ushort OpCode 
		{
			get { return opcode; }
			set { opcode = value; }
		}


		private ushort formatSpecificTarget(ushort target)
		{
			switch (parent.Header.Format)
			{
				case 0x8007:
					return target;
				default:
					if (target < 0xFC) return target;
					return (ushort)(0xFF00 + target);
			}
		}
		private ushort formatSpecificAddr(ushort target)
		{
			switch (parent.Header.Format)
			{
				case 0x8007:
					return target;
				default:
					return (ushort)(0xFF & target);
			}
		}
		public ushort Target1
		{
			get { return formatSpecificTarget(addr1); }
			set {
				addr1 = formatSpecificAddr(value);}
		}

		public ushort Target2
		{
			get { return formatSpecificTarget(addr2); }
			set {
				addr2 = formatSpecificAddr(value);}
		}

		public byte Reserved0
		{
			get {return reserved_00;}
			set {reserved_00 = value;}
		}

		public byte[] Operands
		{
			get {return operands;}
			set {operands = value;}
		}

		public byte[] Reserved1
		{
			get {return reserved_01;}
			set {reserved_01 = value;}
		}
		#endregion

		private void commonConstructor(Bhav parent)
		{
			switch (parent.Header.Format)
			{
				case 0x8001:
				case 0x8002:
					break;
				case 0x8003:
				case 0x8004:
				case 0x8005:
				case 0x8006:
				case 0x8007:
					reserved_01 = new byte[8];
					break;
				default: 
					throw new Exception("Unknown BHAV Format "+parent.Header.Format.ToString("X"));
			}
		}
		/// <summary>
		/// Constructor
		/// </summary>
		internal Instruction (Bhav parent) : base(parent)
		{
			commonConstructor(parent);
		}

		/// <summary>
		/// Constructor
		/// </summary>
		internal Instruction (Bhav parent, System.IO.BinaryReader reader) : base(parent)
		{
			commonConstructor(parent);
			Unserialize(reader);
		}


		public override string ToString()
		{
			return this.OpcodeName(this.opcode, this.operands);
		}


		public Instruction Clone()
		{
			Instruction clone = new Instruction(this.parent);
			clone.opcode      = this.opcode;
			clone.addr1       = this.addr1;
			clone.addr2       = this.addr2;
			clone.reserved_00 = this.reserved_00;
			clone.operands    = (byte[])this.operands.Clone();
			clone.reserved_01 = (byte[])this.reserved_01.Clone();
			return clone;
		}


		/// <summary>
		/// True if this instruction describes a Global Behavior File
		/// </summary>
		public bool GlobalBhav
		{
			get { return IsGlobalBhav(this.opcode); }
		}

		/// <summary>
		/// True if this instruction describes a Local Behavior File
		/// </summary>
		public bool LocalBhav
		{
			get { return IsLocalBhav(this.opcode); }
		}

		/// <summary>
		/// True if this instruction describes a Semi Global Bhav
		/// </summary>
		public bool SemiGlobalBhav
		{
			get { return IsSemiGlobalBhav(this.opcode); }
		}


		/// <summary>
		/// Reads the Data from a Stream
		/// </summary>
		/// <param name="format"></param>
		/// <param name="reader"></param>
		private void Unserialize(BinaryReader reader) 
		{
			switch (parent.Header.Format)
			{
				case 0x8001: 
				case 0x8002: 
				{
					opcode = reader.ReadUInt16();
					addr1 = (ushort)reader.ReadByte();
					addr2 = (ushort)reader.ReadByte();
					operands = reader.ReadBytes(8);
					break;
				}
				case 0x8003: 
				case 0x8004: 
				{
					opcode = reader.ReadUInt16();
					addr1 = (ushort)reader.ReadByte();
					addr2 = (ushort)reader.ReadByte();
					operands = reader.ReadBytes(8);
					reserved_01 = reader.ReadBytes(8);
					break;
				}
				case 0x8006: 
				case 0x8005: 
				{
					opcode = reader.ReadUInt16();
					addr1 = (ushort)reader.ReadByte();
					addr2 = (ushort)reader.ReadByte();
					reserved_00 = reader.ReadByte();
					operands = reader.ReadBytes(8);
					reserved_01 = reader.ReadBytes(8);
					break;
				}
				case 0x8007: 
				{
					opcode = reader.ReadUInt16();
					addr1 = reader.ReadUInt16();
					addr2 = reader.ReadUInt16();
					reserved_00 = reader.ReadByte();
					operands = reader.ReadBytes(8);
					reserved_01 = reader.ReadBytes(8);
					break;
				}
			} //switch
		}

		/// <summary>
		/// Writes the Data to a Stream
		/// </summary>
		/// <param name="format"></param>
		/// <param name="writer"></param>
		internal void Serialize(BinaryWriter writer) 
		{
			switch (parent.Header.Format)
			{
				case 0x8001: 
				case 0x8002: 
				{
					writer.Write(opcode);
					writer.Write((byte)addr1 & 0xFF);
					writer.Write((byte)addr2 & 0xFF);
					writer.Write(operands);	
					break;
				}
				case 0x8003: 
				case 0x8004: 
				{
					writer.Write(opcode);
					writer.Write((byte)addr1 & 0xFF);
					writer.Write((byte)addr2 & 0xFF);
					writer.Write(operands);
					writer.Write(reserved_01);
					break;
				}
				case 0x8006: 
				case 0x8005: 
				{
					writer.Write(opcode);
					writer.Write((byte)addr1 & 0xFF);
					writer.Write((byte)addr2 & 0xFF);
					writer.Write(reserved_00);
					writer.Write(operands);
					writer.Write(reserved_01);
					break;
				}
				case 0x8007: 
				{
					writer.Write(opcode);
					writer.Write(addr1);
					writer.Write(addr2);
					writer.Write(reserved_00);
					writer.Write(operands);
					writer.Write(reserved_01);
					break;
				}
			} //switch
		}

	}
}
