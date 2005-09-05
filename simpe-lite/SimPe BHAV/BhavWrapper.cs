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
using System.Collections;
using SimPe.Interfaces.Plugin;

namespace SimPe.PackedFiles.Wrapper
{
	/// <summary>
	/// This is the actual FileWrapper
	/// More or less implements IList but is strongly typed
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
		, ICollection
	{
		#region Attributes
		/// <summary>
		/// Contains the Filename
		/// </summary>
		private byte[] filename = new byte[64];
		/// <summary>
		/// Stores the Header
		/// </summary>
		private BhavHeader header;
		/// <summary>
		/// Contains all available Instruction 
		/// </summary>		
		private BhavItemArrayList items = new BhavItemArrayList();

		/// <summary>
		/// Contains an Opcode Provider
		/// </summary>
		private SimPe.Interfaces.Providers.IOpcodeProvider opcodes;

		/// <summary>
		/// Indicates the data content of the wrapper (packed file) has changed
		/// </summary>
		public event EventHandler WrapperChanged;
		/// <summary>
		/// Indicates a wrapper routine is updating the wrapper and will generate the WrapperChanged event
		/// </summary>
		internal bool internalchg = false;
		#endregion

		#region Accessor methods
		/// <summary>
		/// Returns the Filename
		/// </summary>
		public string FileName 
		{
			get { return Helper.ToString(filename); }
			set 
			{
				if (!Helper.ToString(filename).Equals(value))
				{
					filename = Helper.ToBytes(value, 0x40);
					OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		/// <summary>
		/// Returns the Header
		/// </summary>
		public BhavHeader Header { get { return header; } }


		/// <summary>
		/// Opcode Provider
		/// </summary>
		public SimPe.Interfaces.Providers.IOpcodeProvider Opcodes 
		{
			get { return opcodes; }
		}


		#region Bhav Members
		public int Add(Instruction item)
		{
			if (items.Count >= ((this.Header.Format == 0x8007) ? 0x8000 : 0x80)) // only allow 32K or 128 lines
				return -1;

			item.Parent = this;
			int result = items.Add(item);
			OnWrapperChanged(items, new EventArgs());
			return result;
		}

		public void Clear()
		{
			items.Clear();
			OnWrapperChanged(items, new EventArgs());
		}

		public bool Contains(Instruction item) { return items.Contains(item); }

		public int IndexOf(object item) { return items.IndexOf(item); }

		public void Insert(int index, Instruction item)
		{
			if (items.Count >= ((this.Header.Format == 0x8007) ? 0x8000 : 0x80)) // only allow 32K or 128 lines
				throw(new NotSupportedException("Too many items"));

			item.Parent = this;
			bool savedstate = internalchg;
			internalchg = true;
			items.Move(items.Add(item), index);
			internalchg = savedstate;
			OnWrapperChanged(items, new EventArgs());
		}

		public void Remove(Instruction item)
		{
			bool savedstate = internalchg;
			internalchg = true;
			items.RemoveAt(items.IndexOf(item));
			internalchg = savedstate;
			OnWrapperChanged(items, new EventArgs());
		}

		public void RemoveAt(int i)
		{
			bool savedstate = internalchg;
			internalchg = true;
			items.RemoveAt(i);
			internalchg = savedstate;
			OnWrapperChanged(items, new EventArgs());
		}

		public Instruction this[int index]
		{
			get
			{
				return items[index];
			}
			set
			{
				if (items[index] == null || !items[index].Equals(value))
				{
					value.Parent = this;
					items[index] = value;
					OnWrapperChanged(items, new EventArgs());
				}
			}
		}


		public void Move(int from, int to)
		{
			bool savedstate = internalchg;
			internalchg = true;
			items.Move(from, to);
			internalchg = savedstate;
			OnWrapperChanged(items, new EventArgs());
		}

		public void Sort()
		{
			int start = 0;		// where we got to on True pass
			int startnext = 0;	// where we got to on False pass

			bool savedstate = internalchg;
			bool somethingchanged = false;
			internalchg = true;
			while (start < items.Count)
			{
				for (int i = start; i < items.Count; i++)
				{
					start = i+1;
					if (items[i].Target1 <= i || items[i].Target1 >= items.Count)
					{
						if (items[i].Target2 <= i || items[i].Target2 >= items.Count)
							break;

						items.Move(items[i].Target2, start);
						somethingchanged = true;

						continue;
					}
					if (items[i].Target1 != start)
					{
						items.Move(items[i].Target1, start);
						somethingchanged = true;
					}
				}
				if (start >= items.Count)
					break;

				for (int i = startnext; i < start; i++)
				{
					startnext = i+1;
					if (items[i].Target2 < start || items[i].Target2 >= items.Count)
						continue;
					items.Move(items[i].Target2, start);
					somethingchanged = true;
					break;
				}
			}
			internalchg = savedstate;
			if (somethingchanged) OnWrapperChanged(items, new EventArgs());
		}

		#endregion

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public Bhav(SimPe.Interfaces.Providers.IOpcodeProvider opcodes) : base()
		{
			header = new BhavHeader(this);
			items = new BhavItemArrayList();
			this.opcodes = opcodes;

			//Instruction.Package = package;
			Instruction.OpcodeProvider = opcodes;
		}


		internal virtual void OnWrapperChanged(object sender, EventArgs e)
		{
			this.Changed = true;

			if (internalchg) return;
			if (WrapperChanged != null) 
			{
				WrapperChanged(sender, e);
			}
		}


		#region AbstractWrapper Member
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
				"Advanced SimAntics Editor",
				2
				); 
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
			header.InstructionCount = (uint)items.Count; // oh please... because header doesn't have a parent (yet!)
			header.Serialize(writer);
			foreach (Instruction i in items)
				i.Serialize(writer);
		}
		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		protected override void Unserialize(System.IO.BinaryReader reader)
		{
			filename = reader.ReadBytes(0x40);
			header.Unserialize(reader);
			items = new BhavItemArrayList();

			for (uint i=0; i < this.Header.InstructionCount; i++)
				items.Add(new Instruction(this, reader));
		}

		#endregion

		#region IFileWrapper Member
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

		#endregion		

		#region IFileWrapperSaveExtension Member		
		//all covered by AbstractWrapper
		#endregion

		#region ICollection Members
		public void CopyTo(Array a, int i) { items.CopyTo(a, i); }
		public int Count { get { return items.Count; } }
		public bool IsSynchronized { get { return items.IsSynchronized; } }
		public object SyncRoot { get { return items.SyncRoot; } }
		#region IEnumerable Members
		public IEnumerator GetEnumerator() { return items.GetEnumerator(); }
		#endregion
		#endregion

		#region BhavItemArrayList
		/// <summary>
		/// Manages the list of items within the BHAV file
		/// Private, so only provides strongly typed versions of the members Bhav actually uses (plus the .ctors)
		/// </summary>
		private class BhavItemArrayList : ArrayList
		{
			public BhavItemArrayList() : base() { }

			public BhavItemArrayList(Instruction[] c) : base(c) { }

			public BhavItemArrayList(int capacity) : base(capacity) { }


			#region BhavItemArrayList
			private void SortSwap(int a, int b) 
			{
				Instruction i = this[a];
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

			/// <summary>
			/// Moves an instruction from position 'from' to position 'to', renumbering Targets as required
			/// </summary>
			/// <param name="from">starting position</param>
			/// <param name="to">ending position</param>
			public void Move(int from, int to)
			{
				if (from == to) return;
				if (from < 0 || from >= Count) return;
				if (to < 0 || to >= Count) return;

				while (from < to) this.SortSwap(from, ++from);
				while (from > to) this.SortSwap(from, --from);
			}

			#endregion

			#region ArrayList
			public new Instruction this[int index]
			{
				get { return (Instruction)base[index]; }
				set { base[index] = value; }
			}

			public void Insert(int index, Instruction item)
			{
				this.Move(base.Add(item), index);
			}

			public override void RemoveAt(int index)
			{
				this.Move(index, this.Count - 1);
				base.RemoveAt(this.Count - 1);
			}

			#endregion
		}

		#endregion
	}


	/// <summary>
	/// Class containing a BHAV Header
	/// </summary>
	public class BhavHeader
	{
		#region Attributes
		private Bhav wrapper;
		private ushort format = 0x8007;
		private uint count = 0;
		private byte type = 0;
		private byte argc = 0;
		private byte locals = 0;
		private byte reserved_00 = 0;	// header flag
		private ushort flags = 0;		// int treeVersion
		private ushort zero = 0;		// ...
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
					wrapper.OnWrapperChanged(wrapper, new EventArgs());
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
					wrapper.OnWrapperChanged(wrapper, new EventArgs());
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
					wrapper.OnWrapperChanged(wrapper, new EventArgs());
				}
			}
		}

		public byte ArgumentCount 
		{
			get { return argc; }
			set 
			{
				if (argc != value)
				{
					argc = value;
					wrapper.OnWrapperChanged(wrapper, new EventArgs());
				}
			}
		}

		public byte LocalVarCount 
		{
			get { return locals; }
			set 
			{
				if (locals != value)
				{
					locals = value;
					wrapper.OnWrapperChanged(wrapper, new EventArgs());
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
					wrapper.OnWrapperChanged(wrapper, new EventArgs());
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
					wrapper.OnWrapperChanged(wrapper, new EventArgs());
				}
			}
		}
		#endregion

		public BhavHeader(Bhav wrapper)
		{
			this.wrapper = wrapper;
		}


		/// <summary>
		/// Reads the Data from a Stream
		/// </summary>
		/// <param name="reader"></param>
		public void Unserialize(System.IO.BinaryReader reader) 
		{
			format = reader.ReadUInt16();				//0x0040 - format
			if (format == 0x8003)
			{
				type = reader.ReadByte();
				argc = reader.ReadByte();
				locals = reader.ReadByte();
				zero = reader.ReadByte();
				flags = reader.ReadUInt16();					
				count = reader.ReadUInt32();
			}
			else
			{
				count = (uint)reader.ReadUInt16();	//0x0042 - # of opcodes
				type = reader.ReadByte();			//0x0044 - tree type
				argc = reader.ReadByte();			//0x0045 - # of args
				locals = reader.ReadByte();			//0x0046 - # of locals
				reserved_00 = reader.ReadByte();	//0x0047 - header flag
				flags = reader.ReadUInt16();		//0x0048 - Tree version (4 bytes)
				zero = reader.ReadUInt16();			//       - Tree version (4 bytes)
			}
		}

		/// <summary>
		/// Writes the Data to a Stream
		/// </summary>
		/// <param name="writer"></param>
		public void Serialize(System.IO.BinaryWriter writer) 
		{
			writer.Write(format);
			if (format == 0x8003)
			{
				writer.Write((byte)type);
				writer.Write(argc);
				writer.Write(locals);
				writer.Write((byte)zero);
				writer.Write(flags);					
				writer.Write(count);
			}
			else
			{
				writer.Write((ushort)count);
				writer.Write(type);
				writer.Write(argc);
				writer.Write(locals);
				writer.Write((byte)reserved_00);
				writer.Write(flags);
				writer.Write(zero);
			}
		}
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
		private wrappedByteArray operands = null;
		private wrappedByteArray reserved_01 = null;
		#endregion

		#region Accessor methods
		public ushort OpCode 
		{
			get { return opcode; }
			set
			{
				if (opcode != value)
				{
					opcode = value;
					if (parent != null) parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}


		private ushort formatSpecificSetAddr(ushort addr)
		{
			switch (parent.Header.Format)
			{
				case 0x8007:
					return addr;
				default:
				switch (addr)
				{
					case 0xFD: return (ushort)0xFFFC;	// error
					case 0xFE: return (ushort)0xFFFD;	// true
					case 0xFF: return (ushort)0xFFFE;	// false
					default: return addr;
				}
			}
		}

		private ushort formatSpecificGetAddr(ushort target)
		{
			switch (parent.Header.Format)
			{
				case 0x8007:
					return target;
				default:
				switch (target)
				{
					case 0xFFFC: return (ushort)0x00FD;	// error
					case 0xFFFD: return (ushort)0x00FE;	// true
					case 0xFFFE: return (ushort)0x00FF;	// false
					default: return (ushort)(target & 0x00FF);
				}
			}
		}

		public ushort Target1
		{
			get { return addr1; }
			set
			{
				if (addr1 != value)
				{
					addr1 = value;
					if (parent != null) parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public ushort Target2
		{
			get { return addr2; }
			set
			{
				if (addr2 != value)
				{
					addr2 = value;
					if (parent != null) parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public byte Reserved0
		{
			get {return reserved_00;}
			set
			{
				if (reserved_00 != value)
				{
					reserved_00 = value;
					if (parent != null) parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		public wrappedByteArray Operands { get { return operands; } }

		public wrappedByteArray Reserved1 { get { return reserved_01; } }

		public new Bhav Parent
		{
			get { return parent; }
			set
			{
				if (parent != value)
				{
					parent = value;
					if (parent != null) parent.OnWrapperChanged(this, new EventArgs());
				}
			}
		}

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		internal Instruction (Bhav parent) : base(parent)
		{
			this.parent = parent;
			this.operands = new wrappedByteArray(this, new byte[8]);
			this.reserved_01 = new wrappedByteArray(this, new byte[8]);
		}

		/// <summary>
		/// Constructor
		/// </summary>
		internal Instruction (Bhav parent, System.IO.BinaryReader reader) : base(parent)
		{
			this.parent = parent;
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
			clone.operands    = operands.Clone();
			clone.operands.Parent = clone;
			clone.reserved_01 = reserved_01.Clone();
			clone.reserved_01.Parent = clone;
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
		private void Unserialize(System.IO.BinaryReader reader) 
		{
			opcode = reader.ReadUInt16();
			if (parent.Header.Format < 0x8007)
			{
				addr1 = formatSpecificSetAddr((ushort)reader.ReadByte());
				addr2 = formatSpecificSetAddr((ushort)reader.ReadByte());
			}
			else
			{
				addr1 = formatSpecificSetAddr(reader.ReadUInt16());
				addr2 = formatSpecificSetAddr(reader.ReadUInt16());
			}

			if (parent.Header.Format < 0x8003)
			{
				operands = new wrappedByteArray(this, reader);
				reserved_01 = new wrappedByteArray(this, new byte[8]);
			}
			else if (parent.Header.Format < 0x8005)
			{
				operands = new wrappedByteArray(this, reader);
				reserved_01 = new wrappedByteArray(this, reader);
			}
			else
			{
				reserved_00 = reader.ReadByte();
				operands = new wrappedByteArray(this, reader);
				reserved_01 = new wrappedByteArray(this, reader);
			}

		}

		/// <summary>
		/// Writes the Data to a Stream
		/// </summary>
		/// <param name="format"></param>
		/// <param name="writer"></param>
		internal void Serialize(System.IO.BinaryWriter writer) 
		{
			writer.Write(opcode);
			if (parent.Header.Format < 0x8007)
			{
				writer.Write((byte)formatSpecificGetAddr(addr1));
				writer.Write((byte)formatSpecificGetAddr(addr2));
			}
			else
			{
				writer.Write(formatSpecificGetAddr(addr1));
				writer.Write(formatSpecificGetAddr(addr2));
			}

			if (parent.Header.Format < 0x8003)
			{
				operands.Serialize(writer);
			}
			else if (parent.Header.Format < 0x8005)
			{
				operands.Serialize(writer);;
				reserved_01.Serialize(writer);
			}
			else
			{
				writer.Write(reserved_00);
				operands.Serialize(writer);
				reserved_01.Serialize(writer);
			}
		}

	}



	public class wrappedByteArray
	{
		private byte[] array;
		private Instruction parent;

		public wrappedByteArray(Instruction parent, byte[] array) { this.parent = parent; this.array = array; }
		public wrappedByteArray(Instruction parent, System.IO.BinaryReader reader)
		{
			this.parent = parent;
			this.array = new byte[8];
			Unserialize(reader);
		}


		public byte this[int index]
		{
			get { return array[index]; }
			set
			{
				if (array[index] != value)
				{
					array[index] = value;
					if (parent != null) parent.Parent.OnWrapperChanged(parent, new EventArgs());
				}
			}
		}

		internal wrappedByteArray Clone() { return new wrappedByteArray(parent, (byte[])array.Clone()); }
		public static implicit operator byte[] (wrappedByteArray a)
		{
			return (byte[])a.array.Clone();
		}


		internal Instruction Parent { set { parent = value; } }


		private void Unserialize(System.IO.BinaryReader reader)
		{
			array = reader.ReadBytes(8);
		}
		internal void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(array);
		}

	}

}
