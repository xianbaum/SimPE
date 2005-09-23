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
	public abstract class ABhavNameWiz
	{
		protected Instruction instruction = null;

		protected ABhavNameWiz(Instruction instruction) 
		{
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

		public virtual Bhav LoadBHAV() { return null; }

		public abstract string ShortName { get; }

		public abstract string LongName { get; }

		public override string ToString()
		{
			return (LongName != null) ? LongName
				: (ShortName != null ? ShortName
				: "[error: " + SimPe.Helper.HexString(instruction.OpCode) + "]");
		}


		private static Hashtable doidGStr = staticInitialiser();
		private static Hashtable staticInitialiser()
		{
			Hashtable t = new Hashtable();
			t.Add((byte)0x03, (uint)0x008d);
			t.Add((byte)0x04, (uint)0x008d);
			t.Add((byte)0x06, (uint)0x0081);
			t.Add((byte)0x12, (uint)0x00c8);
			t.Add((byte)0x13, (uint)0x00c8);
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

		protected string dataOwner(byte doid, ushort instance)
		{
			string doidName = GS.GStr(GS.SF.DataOwners, doid);

			string s = null;
			if (doidGStr[doid] != null)
				s = GS.GStr((uint)doidGStr[doid], instance);
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
				case 0x0c:
				case 0x0e:
				case 0x0f:
				case 0x1c:
				case 0x1d:
					s = "(0x" + SimPe.Helper.HexString((byte)instance) + " " + GS.GStr(GS.SF.Motives, instance) + ")";
					break;
				case 0x1a:
				case 0x2f:
					int a = instance >> 13;            // x = aaabbbbb bccccccc
					int b = (instance >> 7) & 0x3F;
					int c = instance & 0x7F;
					switch (a) 
					{
						case 0:             // private
							b += 0x1000;
							break;
						case 1:             // semi-global
							b += 0x2000;
							break;
						case 2:            // global
							b += 0x100;
							break;
						default:
							b += 0x140;
							break;
					}
					if (doid == 0x1a)
					{
						s = "0x" + SimPe.Helper.HexString((ushort)b) + ":0x" + SimPe.Helper.HexString((byte)c)
							+ " [=" + readBcon((uint)b, c) + "]";
					}
					else
					{
						doidName = GS.GStr(GS.SF.DataOwners, 0x1a);
						s = "0x" + SimPe.Helper.HexString((ushort)b) + ":[Temp " + c.ToString() + "]";
					}
					break;
			}
			if (s == null) s = "0x" + SimPe.Helper.HexString(instance);

			return doidName + (s.Length > 0 ? " " + s : "");
		}


		protected ushort ToShort(byte lower, byte higher)
		{
			return (ushort)((higher << 8) + lower);
		}

		private string readBcon(uint instance, int bid)
		{
			uint bconGroup = 0;
			if (instance < 0x1000)
			{
				bconGroup = 0x7FD46CD0;
			}
			else if (instance >= 0x2000 && instance < 0x3000)
			{
				bconGroup = ((ANameBHAVWiz)instruction).SemiGlobalGroup;
			}
			else
			{
				bconGroup = instruction.Parent.FileDescriptor.Group;
			}

			IScenegraphFileIndexItem[] items =
				SimPe.FileTable.FileIndex.FindFile(0x42434F4E, bconGroup, (ulong)instance, null);

			if (items == null || items.Length == 0)
				return "[No BCON file]";

			Bcon bcon = new Bcon();
			bcon.ProcessData(items[0]);
			if (bid >= bcon.Constants.Count)
				return "[BCON not set]";
			return "0x" + SimPe.Helper.HexString((short)bcon.Constants[bid]);
		}

		public IPackageFile Package
		{
			get
			{
				return (instruction == null || instruction.Parent == null) ? null : instruction.Parent.Package;
			}
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

		public override string ShortName { get { return GS.GStr(GS.SF.Primitives, instruction.OpCode); } }
	}


	/// <summary>
	/// Abstract class for BHAV name providers (global, local, semiglobal)
	/// </summary>
	public abstract class ANameBHAVWiz : ABhavNameWiz
	{
		static Hashtable bhavFilenames = new Hashtable();

		protected uint group;
		public uint SemiGlobalGroup
		{
			get
			{
				return (this is SemiGlobalWiz) ? group : SemiGlobal.SemiGlobalGroup;
			}
		}

		public Glob SemiGlobal
		{
			get
			{
				if (instruction == null || instruction.Parent == null || instruction.Parent.FileDescriptor == null)
					return null;
				IScenegraphFileIndexItem[] items =
					SimPe.FileTable.FileIndex.FindFile(SimPe.Data.MetaData.GLOB_FILE, instruction.Parent.FileDescriptor.Group, 1, null);
				if (items == null || items.Length == 0)
					return null;
				Glob glob = new Glob();
				glob.ProcessData(items[0]);
				return glob;
			}
		}
		public ANameBHAVWiz(Instruction instruction) : base (instruction) 
		{
			SimPe.FileTable.FileIndex.Load();
		}

		public static implicit operator ANameBHAVWiz(Instruction i)
		{
			if (i.OpCode < 0x0100)
				throw new Exception("OpCode not a BHAV");

			if (i.OpCode < 0x1000) return new GlobalWiz(i);
			if (i.OpCode < 0x2000) return new LocalWiz(i);
			return new SemiGlobalWiz(i);
		}


		public override string ShortName
		{
			get 
			{
				string s = BHAVFilename;
				return ((s != null) ? s : "(BHAV not found)") + " (0x" + SimPe.Helper.HexString(instruction.OpCode) +")";
			}
		}

		public override string LongName
		{
			get
			{
				Bhav b = null;
				b = LoadBHAV();
				if (b == null) return null;

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
					}
					else if ((parms[12] & 0x02) != 0)
					{
						s = " - caller's params";
					}
					else
					{
						for (int i = 0; i < b.Header.ArgumentCount && i < 4; i++)
							s += (i>0 ? ", " : ": ") + "0x" + SimPe.Helper.HexString(ToShort(parms[(i*2)], parms[(i*2) + 1]));
					}
				}

				return b.FileName + " (" + b.Header.ArgumentCount.ToString() + " args" + ((s.Length > 0) ? s : "") + ")";
			}
		}


		public string BHAVFilename
		{
			get
			{
				if (instruction == null) return null;

				IScenegraphFileIndexItem[] items = findBHAV();
				if (items == null)
					return null;

				//if (items.Length > 1)
				//	return "(Multiple BHAVs found)";

				if (items[0].Package != instruction.Parent.Package && bhavFilenames[items[0].FileDescriptor.Filename] != null)
					return (string)bhavFilenames[items[0].FileDescriptor.Filename];

				Bhav b = LoadBHAV();
				return (b == null) ? null : b.FileName;
			}
		}

		public override Bhav LoadBHAV()
		{
			IScenegraphFileIndexItem[] items = findBHAV();
			if (items == null)
				return null;

			Bhav b = new Bhav(null);
			b.ProcessData(items[0]);
			bhavFilenames[items[0].FileDescriptor.Filename] = b.FileName;

			return b;
		}

		private IScenegraphFileIndexItem[] findBHAV()
		{
			if (instruction == null) return null;

			IScenegraphFileIndexItem[] items =
				SimPe.FileTable.FileIndex.FindFile(SimPe.Data.MetaData.BHAV_FILE, group, (ulong)instruction.OpCode, null);
			if (items == null || items.Length == 0) return null;
			return items;
		}


		public ArrayList Aliases
		{
			get
			{
				ArrayList aliases = new ArrayList();
				foreach (IScenegraphFileIndexItem item in SimPe.FileTable.FileIndex.FindFile(SimPe.Data.MetaData.BHAV_FILE, group))
				{
					if (bhavFilenames[item.FileDescriptor.Filename] == null)
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
