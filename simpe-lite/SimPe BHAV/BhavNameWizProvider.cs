/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   peter@drealm.info                                                     *
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
using SimPe.PackedFiles.Wrapper;
using SimPe.Interfaces.Files;
using SimPe.Interfaces.Scenegraph;
using pjse.BhavNameWizards;

namespace pjse
{
	/// <summary>
	/// Abstract class that NameWizard providers are based
	/// </summary>
	public abstract class ABhavNameWiz : IDisposable
	{
		protected Instruction instruction = null;
		private Glob glob = null;

		protected ABhavNameWiz(Instruction instruction) 
		{
			SimPe.FileTable.FileIndex.Load();
			this.instruction = instruction;
		}

		public static implicit operator ABhavNameWiz(Instruction i)
		{
			if (i.OpCode < 0x0100) return (ANamePrimitiveWiz)i;
			return (ANameBHAVWiz)i;
		}

		public static implicit operator ABhavNameWiz(ushort opcode)
		{
			return new Instruction(null, opcode);
		}

		public static implicit operator ABhavNameWiz(Bhav wrapper)
		{
			if (wrapper == null || wrapper.FileDescriptor == null)
				throw new InvalidCastException("Invalid BHAV");

			return (ushort)wrapper.FileDescriptor.Instance;
		}


		#region IDisposable Members
		public void Dispose()
		{
			instruction = null;
			glob = null;
		}

		#endregion

		public virtual string ShortName
		{
			get
			{
				return "[" + Prefix + " 0x" + SimPe.Helper.HexString(instruction.OpCode) + "] " + OpcodeName;
			}
		}

		public virtual string LongName { get { return ShortName; } }

		public override string ToString() { return LongName; }

		public virtual Bhav LoadBHAV() { return null; }


		protected abstract string Prefix { get; }

		protected abstract string OpcodeName { get; }


		protected string dataOwner(byte doid, ushort instance)
		{
			ushort[] bcon;
			string doidName = GS.GStr(GS.SF.DataOwners, doid);

			string s = "0x" + SimPe.Helper.HexString(instance);
			if (doidGStr[doid] != null)
				s = "0x" + SimPe.Helper.HexString(instance) + " (" + GS.GStr((uint)doidGStr[doid], instance) + ")";
			switch (doid)
			{
				case 0x0a:
					if (instance == 0)
						s = "";
					break;
				case 0x0b:
				case 0x11:
				case 0x1e:
				case 0x1f:
				case 0x30:
				case 0x31:
					doidName = doidName.Replace("[temp]", "[Temp " + instance.ToString() + "]");
					s = "";
					break;
				case 0x1a:
					bcon = ExpandBCON(instance);
					s = "0x" + SimPe.Helper.HexString(bcon[0]) + ":0x" + SimPe.Helper.HexString((byte)bcon[1])
						+ " " + readBcon((uint)bcon[0], bcon[1]);
					break;
				case 0x2f:
					bcon = ExpandBCON(instance);
					doidName = GS.GStr(GS.SF.DataOwners, 0x1a);
					s = "0x" + SimPe.Helper.HexString(bcon[0]) + ":[Temp " + bcon[1].ToString() + "]";
					break;
			}

			return doidName + (s.Length > 0 ? " " + s : "");
		}


		// I've also changed this from DisaSim2 to be consistent on the choice of Global/Private/Semi
		protected string readBcon(uint instance, int bid)
		{
			// in this context, the group has to be the group of the BHAV you are reading, I think
			// which means the instruction must have a parent
			if (instruction == null || instruction.Parent == null || instruction.Parent.FileDescriptor == null)
				throw new InvalidOperationException("Can't read BCON for instruction with no parent");

			uint bconGroup = 0;
			if (instance < 0x1000)
			{
				bconGroup = 0x7FD46CD0;
			}
			else if (instance < 0x2000)
			{
				bconGroup = instruction.Parent.FileDescriptor.Group;
			}
			else
			{
				bconGroup = SemiGlobalGroup;
			}

			IScenegraphFileIndexItem[] items =
				SimPe.FileTable.FileIndex.FindFile(0x42434F4E, bconGroup, (ulong)instance, null);

			if (items == null || items.Length == 0)
				return "[No BCON file]";

			Bcon bcon = new Bcon();
			bcon.ProcessData(items[0]);
			return bcon.FileName.Trim() + ((bid >= bcon.Constants.Count)
				? " [BCON not set]"
				: ": 0x" + SimPe.Helper.HexString((short)bcon.Constants[bid]));
		}


		/// <summary>
		/// Get the Glob resource for the current instruction (or null, indicating a SemiGlobal perhaps)
		/// </summary>
		private Glob SemiGlobal
		{
			get
			{
				if (glob != null)
					return glob;
				if (instruction == null || instruction.Parent == null || instruction.Parent.FileDescriptor == null)
					throw new InvalidOperationException("Can't read GLOB for instruction with no parent");

				IScenegraphFileIndexItem[] items =
					SimPe.FileTable.FileIndex.FindFile(SimPe.Data.MetaData.GLOB_FILE, instruction.Parent.FileDescriptor.Group);
				if (items == null || items.Length == 0)
					return null;
				glob = new Glob();
				glob.ProcessData(items[0]);
				return glob;
			}
		}

		public uint SemiGlobalGroup { get { return (SemiGlobal != null) ? SemiGlobal.SemiGlobalGroup : instruction.Parent.FileDescriptor.Group; } }

		public string SemiGlobalName { get { return (SemiGlobal != null) ? SemiGlobal.SemiGlobalName : "SemiGlobals"; } }


		// I've changed this from what DisaSim2 does so that the top bit just adds 0x40, whatever. -- plj
		public static ushort[] ExpandBCON(ushort instance)
		{
			int a = instance >> 13;            // x = aaabbbbb bccccccc
			int b = (instance >> 7) & 0x3F;
			int c = instance & 0x7F;
			switch (a & 3) 
			{
				case 1:				// semi-global
					b += 0x2000;
					break;
				case 2:				// global
					b += 0x100;
					break;
				default:			// private
					b += 0x1000;
					break;
			}
			if ((a & 4) != 0)
			{
				b += 0x40;
			}
			ushort[] result = new ushort[2];
			result[0] = (ushort)b;
			result[1] = (ushort)c;
			return result;
		}

		public static ushort ToShort(byte lower, byte higher) { return (ushort)((higher << 8) + lower); }

		public static Hashtable doidGStr = staticInitialiser();
		private static Hashtable staticInitialiser()
		{
			Hashtable t = new Hashtable();
			t.Add((byte)0x03, (uint)0x008d);
			t.Add((byte)0x04, (uint)0x008d);
			t.Add((byte)0x06, (uint)0x0081);
			t.Add((byte)0x0c, (uint)GS.SF.Motives);
			t.Add((byte)0x0e, (uint)GS.SF.Motives);
			t.Add((byte)0x0f, (uint)GS.SF.Motives);
			t.Add((byte)0x12, (uint)0x00c8);
			t.Add((byte)0x13, (uint)0x00c8);
			t.Add((byte)0x1c, (uint)GS.SF.Motives);
			t.Add((byte)0x1d, (uint)GS.SF.Motives);
			t.Add((byte)0x20, (uint)0x00c8);
			t.Add((byte)0x15, (uint)0x00cc);
			t.Add((byte)0x26, (uint)0x00cc);
			t.Add((byte)0x33, (uint)0x00cc);
			t.Add((byte)0x17, (uint)0x00db);
			t.Add((byte)0x18, (uint)0x00dd);
			t.Add((byte)0x21, (uint)0x00f3);
			t.Add((byte)0x22, (uint)0x00f9);
			t.Add((byte)0x23, (uint)0x00f5);
			t.Add((byte)0x27, (uint)0x00fc);
			t.Add((byte)0x28, (uint)0x00fc);
			return t;
		}


	}

}


namespace pjse.BhavNameWizards
{
	/// <summary>
	/// Abstract class for primitive name providers
	/// </summary>
	public abstract class ANamePrimitiveWiz : ABhavNameWiz
	{
		public ANamePrimitiveWiz(Instruction instruction) : base (instruction) { }
		public static implicit operator ANamePrimitiveWiz(Instruction i)
		{
			if (i.OpCode >= 0x0100)
				throw new Exception("OpCode not a primative");

			switch(i.OpCode)
			{
				case 0x0000: return new PrimWiz0x0000(i);
				case 0x0001: return new PrimWiz0x0001(i);
				case 0x0002: return new PrimWiz0x0002(i);
			}
			return new PrimWizDefault(i);
		}

		// Also provide
		// public static implicit operator <Wiz>(byte[] operands);


		protected override string Prefix { get { return "prim"; } }

		protected override string OpcodeName { get { return GS.GStr(GS.SF.Primitives, instruction.OpCode); } }

	}


	/// <summary>
	/// Abstract class for BHAV name providers (global, local, semiglobal)
	/// </summary>
	public abstract class ANameBHAVWiz : ABhavNameWiz
	{
		static Hashtable bhavFilenames = new Hashtable();

		public ANameBHAVWiz(Instruction instruction) : base (instruction) { }

		public static implicit operator ANameBHAVWiz(Instruction i)
		{
			if (i.OpCode < 0x0100)
				throw new Exception("OpCode not a BHAV");

			if (i.OpCode < 0x1000) return new GlobalWiz(i);
			if (i.OpCode < 0x2000) return new LocalWiz(i);
			return new SemiGlobalWiz(i);
		}

		protected override string Prefix { get { return "BHAV"; } }


		protected override string OpcodeName
		{
			get 
			{
				IScenegraphFileIndexItem[] items = findBHAV();
				if (items != null)
				{
					// Always refresh bhavs for current package
					if (items[0].Package == instruction.Parent.Package || bhavFilenames[items[0].FileDescriptor.Filename] == null)
						loadBHAV(items[0]);

					return (string)bhavFilenames[items[0].FileDescriptor.Filename];
				}
				else return "*Not found*";
			}
		}


		public override string ShortName
		{
			get
			{
				Bhav b = LoadBHAV();
				if (b == null) return ShortName;

				string s = "";

				bool noParms = true;
				for (int i = 0; i < 8 && noParms; i++)
					noParms = instruction.Operands[i] == 0xFF;
				if (!noParms)
				{
					byte[] parms = new byte[16];
					((byte[])instruction.Operands).CopyTo(parms, 0);
					((byte[])instruction.Reserved1).CopyTo(parms, 8);
					if ((parms[12] & 0x01) != 0)
					{
						for (int i = 0; i < b.Header.ArgumentCount && i < 4; i++)
							s += (i>0 ? ", " : ": ") + dataOwner(parms[i*3], ToShort(parms[(i*3) + 1], parms[(i*3) + 2]));
						if (b.Header.ArgumentCount > 4)
							s += "...";
					}
					else if ((parms[12] & 0x02) != 0)
					{
						s = " - pass on params";
					}
					else
					{
						for (int i = 0; i < b.Header.ArgumentCount && i < 4; i++)
							s += (i>0 ? ", " : ": ") + "0x" + SimPe.Helper.HexString(ToShort(parms[(i*2)], parms[(i*2) + 1]));
						if (b.Header.ArgumentCount > 4)
							s += "...";
					}
				}

				return prefix + (s.Length > 0 ? " (" + b.Header.ArgumentCount.ToString() + " args" + s + ")" : "");
			}
		}


		private string prefix { get { return base.ShortName; } }

		public override Bhav LoadBHAV()
		{
			IScenegraphFileIndexItem[] items = findBHAV();
			return (items == null) ? null : loadBHAV(items[0]);
		}

		private Bhav loadBHAV(IScenegraphFileIndexItem item)
		{
			Bhav b = new Bhav(null);
			b.ProcessData(item);
			bhavFilenames[item.FileDescriptor.Filename] = b.FileName;
			return b;
		}


		public abstract uint Group { get; }

		private IScenegraphFileIndexItem[] findBHAV()
		{
			if (instruction == null) return null;

			IScenegraphFileIndexItem[] items =
				SimPe.FileTable.FileIndex.FindFile(SimPe.Data.MetaData.BHAV_FILE, Group, (ulong)instruction.OpCode, null);
			if (items == null || items.Length == 0) return null;
#if false
#if DEBUG
			if (items.Length > 1)
			{
				string s = "Multiple BHAVs found:";
				for (int i = 0; i < items.Length; i++)
				{
					Bhav bh = new Bhav(null);
					bh.ProcessData(items[i]);
					s += "(" + i.ToString() + ") " + bh.FileName + " from ";
					s += items[i].Package.SaveFileName;
					s += ", ";
				}
				System.Windows.Forms.MessageBox.Show(s);
			}
#endif
#endif
			return items;
		}


		public ArrayList Aliases
		{
			get
			{
				ArrayList aliases = new ArrayList();
				foreach (IScenegraphFileIndexItem item in SimPe.FileTable.FileIndex.FindFile(SimPe.Data.MetaData.BHAV_FILE, Group))
				{
					if (this is LocalWiz || bhavFilenames[item.FileDescriptor.Filename] == null)
					{
						Bhav b = new Bhav(null);
						b.ProcessData(item);
						bhavFilenames[item.FileDescriptor.Filename] = b.FileName;
					}
					aliases.Add(new SimPe.Data.Alias(item.FileDescriptor.Instance, (string)bhavFilenames[item.FileDescriptor.Filename]));
				}

				return aliases;
			}
		}
	}
}
