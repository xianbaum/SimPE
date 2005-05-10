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
#if PLJ
		private BhavInstList instructions;
#else
		private Instruction[] instructions;
#endif
		/// <summary>
		/// Contains an Opcode Provider
		/// </summary>
		private SimPe.Interfaces.Providers.IOpcodeProvider opcodes;
		#endregion

		#region Accessor methods
		/// <summary>
		/// Returns the Filename
		/// </summary>
		public new string FileName 
		{
			get { return Helper.ToString(filename); }
			set { filename = Helper.ToBytes(value, 0x40); }
		}

		/// <summary>
		/// Returns / Sets the Header
		/// </summary>
		public BhavHeader Header 
		{
			get { return header;	}			
			set { header = value; }
		}

		/// <summary>
		/// Returns/Sets the Instructions
		/// </summary>
#if PLJ
		public BhavInstList Instructions
		{
			get { return instructions;	}			
			set { instructions = value; }
		}
#else
		public Instruction[] Instructions
		{
			get { return instructions;	}			
			set { instructions = value; }
		}
#endif
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
			header = new BhavHeader();
#if PLJ
			instructions = new BhavInstList();
#else
			instructions = new Instruction[0];
#endif
			this.opcodes = opcodes;

			//Instruction.Package = package;
			Instruction.OpcodeProvider = opcodes;
		}


		#region IWrapper member
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
				"BHAV Wrapper",
				"Quaxi",
				"---",
				11
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
#if PLJ
			instructions = new BhavInstList(this, reader);
#else
			instructions = new Instruction[header.InstructionCount];
 			for (int i=0; i < instructions.Length; i++) 
				instructions[i] = new Instruction(this, reader);;
#endif
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

#if PLJ
			instructions.Serialize(writer);
#else
			for (int i=0; i < instructions.Length; i++) 
				instructions[i].Serialize(writer);
#endif
		}
		#endregion

		#region IFileWrapperSaveExtension Member		
		//all covered by Serialize()
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
								   0x42484156  //handles the BHAV File
//								   ,0x54544142 //handles the TTAB File
							   };
				return types;
			}
		}

		#endregion		
	}


	/// <summary>
	/// Class containing a BHAV Header
	/// </summary>
	public class BhavHeader
	{
		#region Attributes
		ushort format;
		uint count;
		byte reserved_00;
		byte type;
		byte argc;
		byte locals;
		ushort flags;
		ushort zero;
		#endregion

		#region Accessor methods
		public ushort Format 
		{
			get { return format; }
			set {format = value; }
		}

		public uint InstructionCount
		{
			get { return count; }
			set {count = value; }
		}

		public byte Type 
		{
			get { return type; }
			set {type = value; }
		}

		public byte ArgumentCount 
		{
			get { return argc; }
			set {argc = value; }
		}

		public byte LocalVarCount 
		{
			get { return locals; }
			set {locals = value; }
		}

		public ushort Flags 
		{
			get { return flags; }
			set {flags = value; }
		}

		public ushort Zero 
		{
			get { return zero; }
			set {zero = value; }
		}
		#endregion

		public BhavHeader()
		{
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
		public BhavInstList() : base() { }


		public BhavInstList(Bhav parent, System.IO.BinaryReader reader)
			: base((int)parent.Header.InstructionCount)
		{
			Unserialise(parent, reader);
		}


		#region BhavInstList
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
		}
		public void Unserialise(Bhav parent, System.IO.BinaryReader reader)
		{
			for (uint i=0; i < parent.Header.InstructionCount; i++)
				this.Add(new Instruction(parent, reader));
		}
		public void Serialize(System.IO.BinaryWriter writer)
		{
			for (int i=0; i < this.Count; i++) 
				this[i].Serialize(writer);
		}

		public void Move(int from, int to)
		{
			if (from < 0 || from >= Count) return;
			if (to < 0 || to >= Count) return;
			while (from < to) SortSwap(from, ++from);
			while(from > to) SortSwap(from, --from);
		}

		#endregion

		#region ArrayList
		public new Instruction this[int index]
		{
			get { return ((Instruction)base[index]); }
			set { base[index] = value; }
		}

		public int Add(Instruction item)
		{
			return base.Add(item);
		}

		public void Insert(int index, Instruction item)
		{
			// this needs to relink everything
			int newIndex = this.Add(item);
			item.Target1 = (ushort)newIndex;
			item.Target2 = (ushort)newIndex;
			this.Move(newIndex, index);
			//base.Insert(index, item);
		}

		public override void RemoveAt(int index)
		{
			// this needs to relink everything
			this.Move(index, this.Count - 1);
			foreach (Instruction i in this)
			{
				if (i.Target1 >= this.Count-1 && i.Target1 < 0xFFFC) i.Target1 = 0xFFFC;
				if (i.Target2 >= this.Count-1 && i.Target2 < 0xFFFC) i.Target2 = 0xFFFC;
			}
			base.RemoveAt(this.Count - 1);
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
			ushort last = 0;
			for (ushort i=0; i<Count-1; i++) 
			{
				Instruction inst = (Instruction)this[i];
				if (inst.Target1 > i+1) 
				{
					if (inst.Target1 < 0xfffc) SortSwap(inst.Target1, (ushort)(i+1));
					else 
					{
						if (inst.Target2 > i+1) 
						{
							if (inst.Target2 < 0xfffc) SortSwap(inst.Target2, (ushort)(i+1));
						}
					}
					last = i;
				}
			}

			for (ushort i=0; i<Count-1; i++) 
			{
				Instruction inst = (Instruction)this[i];
				if (inst.Target2 > last) 
				{
					if (inst.Target2 < 0xfffc) SortSwap(inst.Target2, last);
					last++;
				}
			}
		}
		#endregion
	}
}
