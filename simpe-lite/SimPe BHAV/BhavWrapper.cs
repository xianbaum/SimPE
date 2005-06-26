/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   peter@drealm.info                                                     *
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
using System.Collections;
using SimPe.Interfaces.Plugin;

namespace SimPe.PackedFiles.Wrapper
{
	/// <summary>
	/// This is the actual FileWrapper
	/// </summary>
	/// <remarks>
	/// The wrapper is used to (un)serialize the Data of a file into it's Attributes. So Basically it reads 
	/// a BinaryStream and translates the data into some userdefine Attributes.
	/// </remarks>
	public class Bhav
		: AbstractWrapper				//Implements some of the default Behaviur of a Handler, you can Implement yourself if you want more flexibility!
		, IFileWrapper					//This Interface is used when loading a File
		, IFileWrapperSaveExtension		//This Interface (if available) will be used to store a File
		//,IPackedFileProperties		//This Interface can be used by thirdparties to retrive the FIleproperties, however you don't have to implement it!
	{
		#region Attributes
		/// <summary>
		/// Indicates the data content of the wrapper (packed file) has changed
		/// </summary>
		public event EventHandler WrapperChanged;
		/// <summary>
		/// Indicates a wrapper routine is updating the wrapper and will generate the WrapperChanged event
		/// </summary>
		internal bool internalchg = false;
		/// <summary>
		/// Contains the Filename
		/// </summary>
		private byte[] filename;
		/// <summary>
		/// Stores the Header
		/// </summary>
		private BhavHeader header;
		/// <summary>
		/// Contains all available Instruction 
		/// </summary>		
		private BhavInstList instructions;
		/// <summary>
		/// Contains an Opcode Provider
		/// </summary>
		private SimPe.Interfaces.Providers.IOpcodeProvider opcodes;
		#endregion

		#region Accessor methods
		/// <summary>
		/// Returns the Filename
		/// </summary>
		public string FileName 
		{
			get { return Helper.ToString(filename); }
			set {
				if (!Helper.ToString(filename).Equals(value))
				{
					filename = Helper.ToBytes(value, 0x40);
					OnWrapperChanged(new EventArgs());
				}
			}
		}

		/// <summary>
		/// Returns / Sets the Header
		/// </summary>
		public BhavHeader Header 
		{
			get { return header;	}			
#if UNUSED
			set { header = value; }
#endif
	}

		/// <summary>
		/// Returns/Sets the Instructions
		/// </summary>
		public BhavInstList Instructions
		{
			get { return instructions;	}			
			set { instructions = value; }
		}
		/// <summary>
		/// Opcode Provider
		/// </summary>
		public SimPe.Interfaces.Providers.IOpcodeProvider Opcodes 
		{
			get { return opcodes; }
		}
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public Bhav(SimPe.Interfaces.Providers.IOpcodeProvider opcodes) : base()
		{
			filename = new byte[64];
			header = new BhavHeader(this);
			instructions = new BhavInstList(this);
			this.opcodes = opcodes;

			//Instruction.Package = package;
			Instruction.OpcodeProvider = opcodes;
		}


		internal virtual void OnWrapperChanged(EventArgs e)
		{
			this.Changed = true;

			if (internalchg) return;
			if (WrapperChanged != null) 
			{
				WrapperChanged(this, e);
			}
		}


		#region AbstractWrapper Member
		protected override IPackedFileUI CreateDefaultUIHandler()
		{
			return new UserInterface.BhavForm();
		}

		/// <summary>
		/// Returns a Human Readable Description of this Wrapper
		/// </summary>
		/// <returns>Human Readable Description</returns>
		protected override IWrapperInfo CreateWrapperInfo()
		{
			///
			/// TODO: Change the Description passed here
			/// 
			return new AbstractWrapperInfo(
				"PJSE BHAV Wrapper",
				"Peter L Jones",
				"---",
				1
				); 
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		protected override void Unserialize(System.IO.BinaryReader reader)
		{
			filename = reader.ReadBytes(64);
			header.Unserialize(reader);
			instructions = new BhavInstList(this, reader);
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		protected override void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(filename);
			header.InstructionCount = (uint)instructions.Count; // oh please...
			header.Serialize(writer);

			instructions.Serialize(writer);
		}
		public override bool CheckVersion(uint version) 
		{
			if ( (version==0012) //0.00
				|| (version==0013) //0.10
				) 
			{
				return true;
			}

			return false;
		}
		#endregion

		#region IFileWrapper Member
		public override string Description
		{
			get
			{
				return "FileName="+this.FileName+", Lines="+this.header.InstructionCount;
			}
		}
		/// <summary>
		/// Returns the Signature that can be used to identify Files processable with this Plugin
		/// </summary>
		public byte[] FileSignature
		{
			get
			{
				return new byte[0];
			}
		}

		/// <summary>
		/// Returns a list of File Type this Plugin can process
		/// </summary>
		public uint[] AssignableTypes
		{
			get
			{
				uint[] types = {
								   0x42484156  // BHAV
							   };
				return types;
			}
		}

		#endregion		

		#region IFileWrapperSaveExtension Member		
		//all covered by Serialize()
		#endregion
	}


	/// <summary>
	/// Class containing a BHAV Header
	/// </summary>
	public class BhavHeader
	{
		#region Attributes
		private Bhav wrapper;
		private ushort format;
		private uint count;
		private byte reserved_00;
		private byte type;
		private byte argc;
		private byte locals;
		private ushort flags;
		private ushort zero;
		#endregion

		#region Accessor methods
		public ushort Format 
		{
			get { return format; }
			set 
			{
				if (format != value)
				{
					format = value;
					wrapper.OnWrapperChanged(new EventArgs());
				}
			}
		}

		public uint InstructionCount
		{
			get { return count; }
			set 
			{
				if (count != value)
				{
					count = value;
					wrapper.OnWrapperChanged(new EventArgs());
				}
			}
		}

		public byte Type 
		{
			get { return type; }
			set 
			{
				if (type != value)
				{
					type = value;
					wrapper.OnWrapperChanged(new EventArgs());
				}
			}
		}

		public byte ArgumentCount 
		{
			get { return argc; }
			set {argc = value; }
		}

		public byte LocalVarCount 
		{
			get { return locals; }
			set 
			{
				if (locals != value)
				{
					locals = value;
					wrapper.OnWrapperChanged(new EventArgs());
				}
			}
		}

		public ushort Flags 
		{
			get { return flags; }
			set 
			{
				if (flags != value)
				{
					flags = value;
					wrapper.OnWrapperChanged(new EventArgs());
				}
			}
		}

		public ushort Zero 
		{
			get { return zero; }
			set 
			{
				if (zero != value)
				{
					zero = value;
					wrapper.OnWrapperChanged(new EventArgs());
				}
			}
		}
		#endregion

		public BhavHeader(Bhav wrapper)
		{
			this.wrapper = wrapper;
			format = 0;
			count = 0;
			reserved_00 = 0;
			type = 0;
			argc = 0;
			locals = 0;
			flags = 0;
			zero = 0;
		}


		/// <summary>
		/// Reads the Data from a Stream
		/// </summary>
		/// <param name="reader"></param>
		public void Unserialize(System.IO.BinaryReader reader) 
		{
			format = reader.ReadUInt16();
			switch (format) 
			{
				case 0x8000:
				case 0x8001:
				case 0x8002:
				case 0x8004:
				case 0x8005:
				case 0x8006:
				case 0x8007: 
				{					
					count = (uint)reader.ReadUInt16();
					type = reader.ReadByte();
					argc = reader.ReadByte();
					locals = reader.ReadByte();
					reserved_00 = reader.ReadByte();
					flags = reader.ReadUInt16();
					zero = reader.ReadUInt16();
					break;
				}
				case 0x8003: 				
				{
					type = reader.ReadByte();
					argc = reader.ReadByte();
					locals = reader.ReadByte();
					zero = reader.ReadByte();
					flags = reader.ReadUInt16();					
					count = reader.ReadUInt32();
					break;
				}
				default: 
				{
					throw new Exception("Unknown BHAV Format "+format.ToString("X"));
				}
			} //switch
		}

		/// <summary>
		/// Writes the Data to a Stream
		/// </summary>
		/// <param name="writer"></param>
		public void Serialize(System.IO.BinaryWriter writer) 
		{
			writer.Write(format);
			switch (format) 
			{
				case 0x8000:
				case 0x8001:
				case 0x8002:
				case 0x8004:
				case 0x8005:
				case 0x8006:
				case 0x8007:
				{					
					writer.Write((ushort)count);
					writer.Write(type);
					writer.Write(argc);
					writer.Write(locals);
					writer.Write((byte)reserved_00);
					writer.Write(flags);
					writer.Write(zero);
					break;
				}
				case 0x8003: 				
				{
					writer.Write((byte)type);
					writer.Write(argc);
					writer.Write(locals);
					writer.Write((byte)zero);
					writer.Write(flags);					
					writer.Write(count);
					break;
				}
				default: 
				{
					throw new Exception("Unknown BHAV Format "+format.ToString("X"));
				}
			} //switch
		}
	}


	/// <summary>
	/// Manages the list of instructions within the BHAV file
	/// </summary>
	public class BhavInstList : ArrayList
	{
		#region Attributes
		private Bhav parent = null;
		private bool internalchg = false;
		#endregion

		public BhavInstList(Bhav parent) : base() { this.parent = parent; }


		public BhavInstList(Bhav parent, System.IO.BinaryReader reader)
			: base((int)parent.Header.InstructionCount)
		{
			this.parent = parent;
			internalchg = true;
			Unserialise(parent, reader);
			internalchg = false;
		}


		#region BhavInstList
		private void Unserialise(Bhav parent, System.IO.BinaryReader reader)
		{
			internalchg = true;
			for (uint i=0; i < parent.Header.InstructionCount; i++)
				this.Add(new Instruction(parent, reader));
			internalchg = false;
		}

		public void Serialize(System.IO.BinaryWriter writer)
		{
			for (int i=0; i < this.Count; i++) 
				this[i].Serialize(writer);
		}

		private void SortSwap(int a, int b) 
		{
			Instruction i = (Instruction)this[a];
			this[a] = this[b];
			this[b] = i;

			foreach (Instruction item in this)
			{
				if (item.Target1 == a) item.Target1 = (ushort)b;
				else if (item.Target1 == b) item.Target1 = (ushort)a;

				if (item.Target2 == a) item.Target2 = (ushort)b;
				else if (item.Target2 == b) item.Target2 = (ushort)a;
			}
			if (!internalchg)
				parent.OnWrapperChanged(new EventArgs());
		}
		public void Move(int from, int to)
		{
			if (from == to) return;
			if (from < 0 || from >= Count) return;
			if (to < 0 || to >= Count) return;

			bool savedstate = internalchg;
			internalchg = true;
			while (from < to) SortSwap(from, ++from);
			while (from > to) SortSwap(from, --from);
			internalchg = savedstate;
			if (!internalchg)
				parent.OnWrapperChanged(new EventArgs());
		}

		#endregion

		#region ArrayList
		public new Instruction this[int index]
		{
			get { return ((Instruction)base[index]); }
			set 
			{ 
				base[index] = value;
				if (!internalchg)
					parent.OnWrapperChanged(new EventArgs());
			}
		}

		public int Add(Instruction item)
		{
			int retVal = base.Add(item);
			if (!internalchg)
				parent.OnWrapperChanged(new EventArgs());
			return retVal;
		}

		public void Insert(int index, Instruction item)
		{
			// this needs to relink everything
			bool savedstate = internalchg;
			internalchg = true;
			int newIndex = this.Add(item);
			/*
			 * At Inge's request, don't overwrite targets
			item.Target1 = (ushort)newIndex;
			item.Target2 = (ushort)newIndex;
			 */
			this.Move(newIndex, index);
			internalchg = savedstate;
			if (!internalchg)
				parent.OnWrapperChanged(new EventArgs());
		}

		public override void RemoveAt(int index)
		{
			// this needs to relink everything
			bool savedstate = internalchg;
			internalchg = true;
			this.Move(index, this.Count - 1);
/*
 * At Inge's request, broken gotos not set to RETURN ERROR.
 * UI to display these in an obvious way.
 			foreach (Instruction i in this)
			{
				if (i.Target1 >= this.Count-1 && i.Target1 < 0xFFFC) i.Target1 = 0xFFFC;
				if (i.Target2 >= this.Count-1 && i.Target2 < 0xFFFC) i.Target2 = 0xFFFC;
			}
*/			base.RemoveAt(this.Count - 1);
			internalchg = savedstate;
			if (!internalchg)
				parent.OnWrapperChanged(new EventArgs());
		}

		public void Remove(Instruction item)
		{
			base.Remove(item);
		}

		public bool Contains(Instruction item)
		{
			return base.Contains(item);
		}		

		public override void Sort()
		{
			int start = 0;		// where we got to on True pass
			int startnext = 0;	// where we got to on False pass

			bool savedstate = internalchg;
			internalchg = true;
			while (start < this.Count)
			{
				for (int i = start; i < this.Count; i++)
				{
					start = i+1;
					if (this[i].Target1 <= i || this[i].Target1 >= this.Count)
					{
						if (this[i].Target2 <= i || this[i].Target2 >= this.Count)
							break;

						Move(this[i].Target2, start);

						continue;
					}
					if (this[i].Target1 != start)
						Move(this[i].Target1, start);
				}
				if (start >= this.Count)
					break;

				for (int i = startnext; i < start; i++)
				{
					startnext = i+1;
					if (this[i].Target2 < start || this[i].Target2 >= this.Count)
						continue;
					Move(this[i].Target2, start);
					break;
				}
			}
			internalchg = savedstate;
			if (!internalchg)
				parent.OnWrapperChanged(new EventArgs());
		}

		#endregion
	}


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
			set  { opcode = value; }
		}


		private ushort formatSpecificTarget(ushort target)
		{
			switch (parent.Header.Format)
			{
				case 0x8007:
					return target;
				default:
					switch (target)
					{
						case 0xFD: return (ushort)0xFFFE;
						case 0xFE: return (ushort)0xFFFC;
						case 0xFF: return (ushort)0xFFFD;
						default: return target;
					}
			}
		}
		private ushort formatSpecificAddr(ushort target)
		{
			switch (parent.Header.Format)
			{
				case 0x8007:
					return target;
				default:
					switch (target)
					{
						case 0xFFFE: return (ushort)0x00FD;
						case 0xFFFC: return (ushort)0x00FE;
						case 0xFFFD: return (ushort)0x00FF;
						default: return (ushort)(target & 0x00FF);
					}
			}
		}
		public ushort Target1
		{
			get { return formatSpecificTarget(addr1); }
			set { addr1 = formatSpecificAddr(value); }
		}

		public ushort Target2
		{
			get { return formatSpecificTarget(addr2); }
			set { addr2 = formatSpecificAddr(value); }
		}

		public byte Reserved0
		{
			get {return reserved_00;}
			set { reserved_00 = value; }
		}

		public byte[] Operands
		{
			get {return operands;}
			set { operands = value; }
		}

		public byte[] Reserved1
		{
			get {return reserved_01;}
			set { reserved_01 = value; }
		}
		#endregion

		private void commonConstructor(Bhav parent)
		{
			this.parent = parent;
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
					writer.Write((byte)(addr1 & 0xFF));
					writer.Write((byte)(addr2 & 0xFF));
					writer.Write(operands);	
					break;
				}
				case 0x8003: 
				case 0x8004: 
				{
					writer.Write(opcode);
					writer.Write((byte)(addr1 & 0xFF));
					writer.Write((byte)(addr2 & 0xFF));
					writer.Write(operands);
					writer.Write(reserved_01);
					break;
				}
				case 0x8006: 
				case 0x8005: 
				{
					writer.Write(opcode);
					writer.Write((byte)(addr1 & 0xFF));
					writer.Write((byte)(addr2 & 0xFF));
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
