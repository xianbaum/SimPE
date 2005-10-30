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
using SimPe.Interfaces.Plugin;
using pjse.BhavNameWizards;

namespace pjse
{
	public enum Scope : int
	{
		Global = 0x00,
		SemiGlobal = 0x01,
		Private = 0x02,
	}


	public abstract class ExtendedWrapper : AbstractWrapper, IDisposable
	{
		private Scope scope;

		public ExtendedWrapper() : base()
		{
			if (this is Bhav && this.FileDescriptor != null)
			{
				if (this.FileDescriptor.Instance < 0x1000)
					scope = Scope.Global;
				else if (this.FileDescriptor.Instance < 0x2000)
					scope = Scope.Private;
				else
					scope = Scope.SemiGlobal;
			}
			else
				scope = Scope.Private; // at least for now
		}


		/// <summary>
		/// This object's group
		/// </summary>
		public uint Group { get { return this.FileDescriptor.Group; } }

		/// <summary>
		/// The SemiGlobal group for this object
		/// </summary>
		public uint SemiGroup
		{
			get
			{
				if (this.SemiGlobal == null) return 0;
				return SemiGlobal.SemiGlobalGroup;
			}
		}

		/// <summary>
		/// The Global group
		/// </summary>
		public uint GlobalGroup { get { return 0x7FD46CD0; } }


		public Scope Context
		{
			get
			{
				return scope;
			}
		}


		private Glob glob = null;
		private Glob SemiGlobal
		{
			get
			{
				return (glob != null)
					? glob
					: (glob = BhavWiz.GlobByGroup(this.FileDescriptor.Group));
			}
		}


		public uint GroupForScope(Scope s)
		{
			uint group = 0;

			if (s == Scope.Global)
				group = GlobalGroup;
			else if (Context != Scope.Global)
			{
				if (s == Scope.SemiGlobal)
					group = (Context == Scope.SemiGlobal) ? Group : SemiGroup;
				else if (Context != Scope.SemiGlobal)
					group = Group;
			}

			return group;
		}

		public pjse.FileTable.Entry ResourceByInstance(uint type, uint instance)
		{
			pjse.FileTable.Entry[] items;

			items = pjse.FileTable.GFT[type, Group, instance];
			if (items == null || items.Length == 0)
				items = pjse.FileTable.GFT[type, SemiGroup, instance];
			if (items == null || items.Length == 0)
				items = pjse.FileTable.GFT[type, GlobalGroup, instance];
			if (items == null || items.Length == 0)
				return null;

			return items[0];
		}


		#region IDisposable Members

		public override void Dispose()
		{
			glob = null;
			base.Dispose();
		}

		#endregion
	}



	/// <summary>
	/// Abstract wrapper that extends the SimPe Bhav Instruction class for display purposes
	/// </summary>
	/// <remarks>Remember - an Instruction() is the call to a primitive or BHAV...</remarks>
	public abstract class BhavWiz : IDisposable
	{
		protected Instruction instruction = null;
		protected string prefix = null;

		protected BhavWiz(Instruction instruction) 
		{
			this.instruction = instruction;
		}

		public static implicit operator BhavWiz(Instruction i)
		{
			if (i.OpCode < 0x0100) return (BhavWizPrim)i;
			return (BhavWizBhav)i;
		}

		public Instruction Instruction { get { return instruction; } }

		public static implicit operator Instruction(BhavWiz b) { return b.instruction; }


		#region IDisposable Members
		public void Dispose()
		{
			instruction = null;
		}

		#endregion

		public override string ToString() { return LongName; }


		public virtual string ShortName
		{
			get
			{
				return "[" + Prefix + " 0x" + SimPe.Helper.HexString(instruction.OpCode) + "] " + OpcodeName;
			}
		}

		public virtual string LongName { get { return ShortName; } }


		public virtual Bhav Wrapper { get { return null; } }

		protected string Prefix { get { return prefix; } }

		protected abstract string OpcodeName { get; }


		#region Utilities
		protected string dataOwner(byte doid, ushort instance)
		{
			ushort[] bcon;
			string doidName = GS.GStr(GS.BhavStr.DataOwners, doid);

			string s = "0x" + SimPe.Helper.HexString(instance);
			string temp = "";

			if (doidGStr[doid] != null)
				s += " (" + GS.GStr((GS.BhavStr)doidGStr[doid], instance) + ")";

			switch (doid)
			{
				case 0x00:
					temp = readStr(Scope.Private, GS.GlobalStr.MyAttributeLabel, instance);
					if (temp != null)
						s += " (" + temp + ")";
					break;
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
					doidName = GS.GStr(GS.BhavStr.DataOwners, 0x1a);
					bcon = ExpandBCON(instance);
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
				bconGroup = instruction.Parent.GroupForScope(Scope.Global);
			else if (instance < 0x2000)
				bconGroup = instruction.Parent.GroupForScope(Scope.Private);
			else
				bconGroup = instruction.Parent.GroupForScope(Scope.SemiGlobal);

			pjse.FileTable.Entry[] items = pjse.FileTable.GFT[0x42434F4E, bconGroup, instance];

			if (items == null || items.Length == 0)
				return "[No BCON file]";

			Bcon bcon = new Bcon();
			bcon.ProcessData(items[0].PFD, items[0].Package);
			return bcon.FileName.Trim() + (temp ? "" : (bid >= bcon.Constants.Count)
				? " [BCON not set]"
				: ": 0x" + SimPe.Helper.HexString((short)bcon.Constants[bid]));
		}


		protected string readStr(Scope s, GS.GlobalStr instance, int sid, int maxLen)
		{
			if (instruction == null || instruction.Parent == null || instruction.Parent.FileDescriptor == null)
				throw new InvalidOperationException("Can't read STR# for instruction with no parent");

			pjse.FileTable.Entry[] items = null;

			items = pjse.FileTable.GFT[(uint)SimPe.Data.MetaData.STRING_FILE, instruction.Parent.GroupForScope(s), (uint)instance];

			if (items == null || items.Length == 0)
				return "[No " + s.ToString() + " " + instance.ToString() + " (STR# 0x" + SimPe.Helper.HexString((ushort)instance) + ") file]";

			Str str = new Str();
			str.ProcessData(items[0].PFD, items[0].Package);
			return ((str[1, sid] != null)
				? "\"" + myLeft(str[1, sid].Title.Trim(), maxLen) + "\""
				: "[" + s.ToString() + " " + instance.ToString() + " STR# 0x" + SimPe.Helper.HexString((ushort)instance) + ":0x" + SimPe.Helper.HexString((byte)sid) + " not set]"
				);
		}

		protected string readStr(Scope s, GS.GlobalStr instance, int sid) { return readStr(s, instance, sid, -1); }

		private static string myLeft(string str, int len)
		{
			return (len < 0) ? str : str.PadRight(len).Substring(0, len).Trim() + (str.Length > len ? "..." : "");
		}

		public static Glob GlobByGroup(uint group)
		{
			pjse.FileTable.Entry[] items = pjse.FileTable.GFT[(uint)SimPe.Data.MetaData.GLOB_FILE, group];
			if (items == null || items.Length == 0) return null;

			Glob glob = new Glob();
			glob.ProcessData(items[0].PFD, items[0].Package);
			return glob;
		}


		// I've changed this from what DisaSim2 does so that the top bit just adds 0x40, whatever. -- plj
		public static ushort[] ExpandBCON(ushort instance)
		{
			int a = instance >> 13;            // x = aaabbbbb bccccccc
			int b = (instance >> 7) & 0x3F;
			int c = instance & 0x7F;
			switch (a & 3) 
			{
				case 0:			// private
					b += 0x1000;
					break;
				case 1:			// semi-global
					b += 0x2000;
					break;
				case 2:			// global
					b += 0x100;
					break;
				case 3:			// FUBAR, as it says in disaSim2-23b
					b = 0xF5BA;
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
			t.Add((byte)0x03, GS.BhavStr.DataLabels);
			t.Add((byte)0x04, GS.BhavStr.DataLabels);
			t.Add((byte)0x06, GS.BhavStr.GlobalLabels);
			t.Add((byte)0x0c, GS.BhavStr.Motives);
			t.Add((byte)0x0e, GS.BhavStr.Motives);
			t.Add((byte)0x0f, GS.BhavStr.Motives);
			t.Add((byte)0x12, GS.BhavStr.PersonData);
			t.Add((byte)0x13, GS.BhavStr.PersonData);
			t.Add((byte)0x1c, GS.BhavStr.Motives);
			t.Add((byte)0x1d, GS.BhavStr.Motives);
			t.Add((byte)0x20, GS.BhavStr.PersonData);
			t.Add((byte)0x15, GS.BhavStr.OBJDDescs);
			t.Add((byte)0x26, GS.BhavStr.OBJDDescs);
			t.Add((byte)0x33, GS.BhavStr.OBJDDescs);
			t.Add((byte)0x17, GS.BhavStr.RoomValues);
			t.Add((byte)0x18, GS.BhavStr.NeighborData);
			t.Add((byte)0x21, GS.BhavStr.JobData);
			t.Add((byte)0x22, GS.BhavStr.NeighborhoodData);
			t.Add((byte)0x23, GS.BhavStr.OBJFDescs);
			t.Add((byte)0x27, GS.BhavStr.InventoryDialog);
			t.Add((byte)0x28, GS.BhavStr.InventoryDialog);
			return t;
		}


		#endregion
	}

}

