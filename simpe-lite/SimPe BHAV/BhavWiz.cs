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
	public abstract class BhavWiz : IDisposable
	{
		protected Instruction instruction = null;
		private Glob glob = null;

		protected BhavWiz(Instruction instruction) 
		{
			this.instruction = instruction;
		}

		public static implicit operator BhavWiz(Instruction i)
		{
			if (i.OpCode < 0x0100) return (BhavWizPrim)i;
			return (BhavWizBhav)i;
		}

		public static implicit operator BhavWiz(ushort opcode)
		{
			return new Instruction(null, opcode);
		}

		public static implicit operator BhavWiz(Bhav wrapper)
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

		/// <summary>
		/// The descriptive term for this type of opcode
		/// </summary>
		protected abstract string Prefix { get; }

		/// <summary>
		/// The name of this opcode
		/// </summary>
		protected abstract string OpcodeName { get; }


		public virtual string ShortName
		{
			get
			{
				return "[" + Prefix + " 0x" + SimPe.Helper.HexString(instruction.OpCode) + "] " + OpcodeName;
			}
		}

		public virtual string LongName { get { return ShortName; } }

		public override string ToString() { return LongName; }

		public virtual Bhav Wrapper { get { return null; } }

		public Instruction Instruction { get { return instruction; } }

		public static implicit operator Instruction(BhavWiz b) { return b.instruction; }


		#region Utilities
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
						+ " " + readBcon((uint)bcon[0], bcon[1], false);
					break;
				case 0x2f:
					bcon = ExpandBCON(instance);
					doidName = GS.GStr(GS.SF.DataOwners, 0x1a);
					s = "0x" + SimPe.Helper.HexString(bcon[0]) + ":[Temp " + bcon[1].ToString() + "]"
						+ " " + readBcon((uint)bcon[0], bcon[1], true);
					break;
			}

			return doidName + (s.Length > 0 ? " " + s : "");
		}


		// I've also changed this from DisaSim2 to be consistent on the choice of Global/Private/Semi
		protected string readBcon(uint instance, int bid, bool temp)
		{
			// in this context, the group has to be the group of the BHAV you are reading, I think
			// which means the instruction must have a parent
			if (instruction == null || instruction.Parent == null || instruction.Parent.FileDescriptor == null)
				throw new InvalidOperationException("Can't read BCON for instruction with no parent");

			uint bconGroup = 0;
			if (instance < 0x1000)
				bconGroup = 0x7FD46CD0;
			else if (instance < 0x2000)
				bconGroup = instruction.Parent.FileDescriptor.Group;
			else
				bconGroup = SemiGlobalGroup;

			SimPe.FileTable.FileIndex.Load();
			IScenegraphFileIndexItem[] items =
				SimPe.FileTable.FileIndex.FindFile(0x42434F4E, bconGroup, (ulong)instance, null);

			if (items == null || items.Length == 0)
				return "[No BCON file]";

			Bcon bcon = new Bcon();
			bcon.ProcessData(items[0]);
			return bcon.FileName.Trim() + (temp ? "" : (bid >= bcon.Constants.Count)
				? " [BCON not set]"
				: ": 0x" + SimPe.Helper.HexString((short)bcon.Constants[bid]));
		}


		protected string readStr(Scope s, ulong instance, int sid, int maxLen)
		{
			if (instruction == null || instruction.Parent == null || instruction.Parent.FileDescriptor == null)
				throw new InvalidOperationException("Can't read STR# for instruction with no parent");

			uint strGroup = 0;
			if (s == Scope.Global)
				strGroup = 0x7FD46CD0;
			else if (s == Scope.Private)
				strGroup = instruction.Parent.FileDescriptor.Group;
			else
				strGroup = SemiGlobalGroup;

			SimPe.FileTable.FileIndex.Load();
			IScenegraphFileIndexItem[] items =
				SimPe.FileTable.FileIndex.FindFile(SimPe.Data.MetaData.STRING_FILE, strGroup, instance, null);

			if (items == null || items.Length == 0)
				return "[No " + s.ToString() + " STR# 0x" + SimPe.Helper.HexString((ushort)instance) + " file]";

			Str str = new Str();
			str.ProcessData(items[0]);
			return (s != Scope.Global ? str.FileName.Trim() + " ": "")
				+ ((str[1, sid] != null) ? "\"" + myLeft(str[1, sid].Title.Trim(), maxLen) + "\""
				: "[" + s.ToString() + " STR 0x" + SimPe.Helper.HexString((ushort)instance) + ":0x" + SimPe.Helper.HexString((byte)sid) + " not set]"
				);
		}

		protected string readStr(Scope s, ulong instance, int sid) { return readStr(s, instance, sid, -1); }

		private static string myLeft(string str, int len)
		{
			return (len < 0) ? str : str.PadRight(len).Substring(0, len).Trim() + (str.Length > len ? "..." : "");
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

				SimPe.FileTable.FileIndex.Load();
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


		protected enum Scope : int
		{
			Global = 0x00,
			Private = 0x01,
			SemiGlobal = 0x02,
		}
		#endregion
	}

}

