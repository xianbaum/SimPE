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
using pjse.BhavNameWizards;

namespace pjse
{
	public enum Scope : int
	{
		Global = 0x00,
		SemiGlobal = 0x01,
		Private = 0x02,
	}

	public enum Detail : int
	{
		ValueOnly = 0x00,
		Errors = 0x01,
		Full = 0x02,
	}

	public enum Group : int
	{
		Global = 0x7FD46CD0,
		BhavFuncs = 0x7FE59FD0,
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

		public static implicit operator Instruction(BhavWiz b) { return b.instruction; }


		#region IDisposable Members
		public void Dispose()
		{
			instruction = null;
		}

		#endregion

		public Instruction Instruction { get { return instruction; } }

		public override string ToString() { return LongName; }

		public virtual pjse.FileTable.Entry FTEntry { get { return null; } }


		public virtual string ShortName { get { return Name + " (" + Operands(false) + ")"; } }

		public virtual string LongName { get { return Name + " (" + Operands(true) + ")"; } }


		public virtual ABhavOperandWiz Wizard()
		{
			return null;
		}


		protected virtual string Name { get { return "[" + Prefix + " 0x" + SimPe.Helper.HexString(instruction.OpCode) + "] " + OpcodeName; } }

		protected virtual string Prefix { get { return prefix; } }

		protected abstract string OpcodeName { get; }

		protected abstract string Operands(bool lng);

		#region Utilities
		protected string dataOwner(byte doid, ushort instance)
		{
			ushort[] bcon;
			string doidName = readStr(GS.BhavStr.DataOwners, doid);

			string s = "0x" + SimPe.Helper.HexString(instance);
			string temp = "";

			if (doidGStr[doid] != null)
				s += " (" + readStr((GS.BhavStr)doidGStr[doid], instance) + ")";

			switch (doid)
			{
				case 0x00: case 0x01:
					temp = readStr(Scope.Private, GS.GlobalStr.AttributeLabels, instance, -1, pjse.Detail.ValueOnly);
					if (temp != null && temp.Length > 0)
						s += " (" + temp + ")";
					break;
				case 0x02: case 0x05:
					temp = readStr(Scope.SemiGlobal, GS.GlobalStr.AttributeLabels, instance, -1, pjse.Detail.ValueOnly);
					if (temp != null && temp.Length > 0)
						s += " (" + temp + ")";
					break;
				case 0x09:
					temp = readParam(instruction.Parent, instance, pjse.Detail.Errors);
					if (temp.Length > 0)
						s += " (" + temp + ")";
					break;
				case 0x19:
					temp = readLocal(instruction.Parent, instance, pjse.Detail.Errors);
					if (temp.Length > 0)
						s += " (" + temp + ")";
					break;
				case 0x0a:
					if (instance == 0)
						s = "";
					break;
				case 0x0b: case 0x11:
				case 0x1e: case 0x1f:
				case 0x30: case 0x31:
					doidName = doidName.Replace("[temp]", "[Temp " + instance.ToString() + "]");
					s = "";
					break;
				case 0x16: case 0x32:
					doidName = doidName.Replace("[param]", "[" + dataOwner(0x09, instance) + "]");
					s = "";
					break;
				case 0x1a:
					bcon = ExpandBCON(instance, false);
					s = "0x" + SimPe.Helper.HexString(bcon[0]) + ":0x" + SimPe.Helper.HexString((byte)bcon[1]);
					temp = readBcon((uint)bcon[0], bcon[1], false, pjse.Detail.Full);
					if (temp.Length > 0)
						s += " (" + temp + ")";
					break;
				case 0x2f:
					doidName = readStr(GS.BhavStr.DataOwners, 0x1a);
					bcon = ExpandBCON(instance, true);
					s = "0x" + SimPe.Helper.HexString(bcon[0]) + ":[Temp " + bcon[1].ToString() + "]";
					temp = readBcon((uint)bcon[0], bcon[1], true, pjse.Detail.Errors);
					if (temp.Length > 0)
						s += " (" + temp + ")";
					break;
			}

			return doidName + (s.Length > 0 ? " " + s : "");
		}

		protected string dataOwner(byte doid, byte lo, byte hi) { return dataOwner(doid, ToShort(lo, hi)); }

		protected string dataOwner(bool lng, byte doid, ushort instance)
		{
			if (lng)
				return dataOwner(doid, instance);

			ushort[] bcon;
			switch(doid)
			{
				case 0x06:
					return (doidGStr[doid] != null) ? readStr((GS.BhavStr)doidGStr[doid], instance) : "";
				case 0x0c: case 0x0e: case 0x0f: case 0x1c: case 0x1d:
					return readStr(GS.BhavStr.DataOwners, doid) + " " + readStr((GS.BhavStr)doidGStr[doid], instance);
				case 0x1a:
					bcon = ExpandBCON(instance, false);
					return readBcon((uint)bcon[0], bcon[1], false, pjse.Detail.Errors) +
						"(" + readStr(GS.BhavStr.DataOwners, 0x1a) + ")";
				default:
					return readStr(GS.BhavStr.DataOwners, doid) + " 0x" + SimPe.Helper.HexString(instance);
			}
		}

		protected string dataOwner(bool lng, byte doid, byte lo, byte hi) {  return dataOwner(lng, doid, ToShort(lo, hi));}


#if DONTUSEREADSTR
		/// <summary>
		/// Get a string identifying the requested STR# resource
		/// </summary>
		/// <param name="s">Scope for STR# resource</param>
		/// <param name="instance">STR# resource identifier</param>
		/// <param name="sid">String number</param>
		/// <param name="maxLen">-1: unlimited; else max string length to return</param>
		/// <param name="silent">true: return "" if not found, else just string; false: identify requested resource</param>
		/// <returns>Scope Instance "STR# 0x" hex(Instance) ":0x" hex(stringID) Left(string, len)</returns>
		private string readStr(string strIprefix, Scope s, uint instance, int sid, int maxLen, Detail detail)
		{
			if (instruction == null || instruction.Parent == null || instruction.Parent.FileDescriptor == null)
				throw new InvalidOperationException("Can't read STR# for instruction with no parent");

			string strI = strIprefix + s.ToString() + " " + "STR# 0x" + SimPe.Helper.HexString((ushort)instance);
			string strS = ":0x" + SimPe.Helper.HexString((ushort)sid);

			if (instruction.Parent.Context == Scope.Global && s != Scope.Global
				|| instruction.Parent.Context == Scope.SemiGlobal && s == Scope.Private)
				return (detail == Detail.Full) ? strI + strS : "";

			pjse.FileTable.Entry[] items = pjse.FileTable.GFT[(uint)SimPe.Data.MetaData.STRING_FILE, instruction.Parent.GroupForScope(s), instance];

			if (items == null || items.Length == 0)
				return (detail == Detail.ValueOnly) ? "" : "[No " + strI + " file]" + (detail == Detail.Full ? strS : "");

			StrWrapper str = new StrWrapper();
			str.ProcessData(items[0].PFD, items[0].Package);
			StrItem si = str[1, sid];

			if (si == null)
				return (detail == Detail.ValueOnly) ? "" : "[" + strI + strS + " not set]";

			return (detail == Detail.Full || detail == Detail.Comments ? strI + strS + " " : "") + "\"" + myLeft(si.Title.Trim(), maxLen) + "\"" ;
		}


		public string readStr(Scope s, GS.GlobalStr instance, int sid, int maxLen, Detail detail)
		{
			return readStr(instance.ToString() + ": ", s, (uint)instance, sid, maxLen, detail);
		}


		protected string readStr(Scope s, uint instance, int sid, int maxLen, Detail detail)
		{
			return readStr("", s, instance, sid, maxLen, detail);
		}

		protected string readStr(Scope s, GS.GlobalStr instance, int sid) { return readStr(s, instance, sid, -1, pjse.Detail.ValueOnly); }

		protected string readStr(Scope s, GS.GlobalStr instance, int sid, Detail detail) { return readStr(s, instance, sid, -1, detail); }

		protected string readStr(bool fallback, GS.GlobalStr instance, int sid, int maxLen, Detail detail)
		{
			Scope c = instruction.Parent.Context;
			string result = readStr(c, instance, sid, maxLen, detail);

			if (fallback && (result.IndexOf('"') < 0))
			{
				if (c == Scope.Private)
					result = readStr(Scope.SemiGlobal, instance, sid, maxLen, detail);

				if (result.IndexOf('"') < 0)
					result = readStr(Scope.Global, instance, sid, maxLen, detail);
			}

			return result.Replace("Private ", "(Private) ").Replace("SemiGlobal ", "(SemiGlobal) ").Replace("Global ", "(Global) ");
		}

#else

		/*
		 * 
		 * Run Tree By Name (0x001C) has flags to prevent/allow string fallback separately for Global and Semiglobal.
		 * We can let the programmer worry about that, though.
		 * 
		 */


		public string readStr(GS.GlobalStr instance, ushort sid, int maxlen, Detail detail)
		{
			return readStr(instruction.Parent.Context, (uint)instance, sid, maxlen, detail);
		}

		public string readStr(Scope scope, GS.GlobalStr instance, ushort sid, int maxlen, Detail detail)
		{
			return readStr(scope, (uint)instance, sid, maxlen, detail);
		}

		protected string readStr(Scope scope, uint instance, ushort sid, int maxlen, Detail detail)
		{
			return readStr(instruction.Parent, instruction.Parent.GroupForScope(scope), instance, sid, maxlen, detail);
		}


		public static string readStr(GS.BhavStr instance, ushort sid)
		{
			return readStr(null, (uint)Group.BhavFuncs, (uint)instance, sid, -1, Detail.Errors, false);
		}


		private static string readStr(ExtendedWrapper parent, uint group, uint instance, ushort sid, int maxlen, Detail detail)
		{
			return readStr(parent, group, instance, sid, maxlen, detail, true);
		}


		private static string readStr(ExtendedWrapper parent, uint group, uint instance, ushort sid, int maxlen, Detail detail, bool addQuotes)
		{
			Str str = new Str(parent, group, instance);
			String pfname = "";
			if (detail == Detail.Full)
			{
				if (group == (uint)Group.BhavFuncs)
					try { pfname += (GS.BhavStr)instance + ": "; }
					catch { }
				else
					try { pfname += (GS.GlobalStr)instance + ": "; }
					catch { }
			}
			if (detail == Detail.Full || detail == Detail.Errors)
					pfname += "STR# 0x" + SimPe.Helper.HexString((ushort)instance) + ":0x" + SimPe.Helper.HexString((byte)sid);


			if (str != null)
			{
				FallbackStrItem fsi = str[sid];
				if (fsi != null && fsi.strItem != null)
				{
					String s = "";
					if (detail != Detail.ValueOnly && fsi.fallback != null && fsi.fallback.Count != 0)
					{
						s += "[";
						for(int i=0; i < fsi.fallback.Count; i++) s += (i==0?"":"; ") + fsi.fallback[i];
						s += "] ";
					}
					if (addQuotes)
						return s + "\"" + myLeft(fsi.strItem.Title.Trim(), maxlen) + "\"" + (detail == Detail.Full ? " [" + pfname + "]" : "");
					else
						return s + myLeft(fsi.strItem.Title.Trim(), maxlen) + (detail == Detail.Full ? " [" + pfname + "]" : "");
				}
			}
			if (detail == Detail.ValueOnly)
				return null;
			return "[" + SimPe.Localization.Manager.GetString("unk") + ": " + pfname + "]";
		}


		private static Hashtable gString = new Hashtable();
		public static ArrayList readStr(GS.BhavStr instance)
		{
			if (gString[instance] == null)
			{
				ArrayList list = new ArrayList();
				String s;
				for(ushort i = 0; (s = readStr(null, (uint)Group.BhavFuncs, (uint)instance, i, -1, Detail.ValueOnly, false)) != null; i++) list.Add(s);
				gString[instance] = list;
			}
			return (ArrayList)gString[instance];
		}

#endif
		private static string myLeft(string str, int len)
		{
			return (len < 0) ? str : str.PadRight(len).Substring(0, len).Trim() + (str.Length > len ? "..." : "");
		}


		protected string readBcon(uint instance, int bid, bool temp, Detail detail)
		{
			if (instruction == null || instruction.Parent == null || instruction.Parent.FileDescriptor == null)
				throw new InvalidOperationException("Can't read BCON for instruction with no parent");

			Scope s = Scope.Private;
			if      (instance <  0x1000) s = Scope.Global;
			else if (instance >= 0x2000) s = Scope.SemiGlobal;

			string strI = s.ToString() + " " + "BCON 0x" + SimPe.Helper.HexString((ushort)instance);
			string strS = (temp ? "[Temp " + bid.ToString() + "]" : ":0x" + SimPe.Helper.HexString((byte)bid));

			if (instruction.Parent.Context == Scope.Global && s != Scope.Global
				|| instruction.Parent.Context == Scope.SemiGlobal && s == Scope.Private)
				return (detail == Detail.Full) ? strI + strS : "";

			pjse.FileTable.Entry[] items = pjse.FileTable.GFT[0x42434F4E, instruction.Parent.GroupForScope(s), instance];

			if (items == null || items.Length == 0)
				return (detail == Detail.ValueOnly) ? "" : "[No " + strI + " file]" + (detail == Detail.Full ? strS : "");

			Bcon bcon = new Bcon();
			bcon.ProcessData(items[0].PFD, items[0].Package);

			string f = bcon.FileName.Trim();
			strI = (f.Length > 0 ? f + ": " : "") + strI;

			if (temp)
				return (detail == Detail.ValueOnly || detail == Detail.Errors) ? "" : strI + (detail == Detail.Full ? strS : "");

			string label = readTrcn(bcon, bid, detail).Trim();
			label = label.Length > 0 ? " (" + label + ")" : "";

			if (bid >= bcon.Count)
				return (detail == Detail.ValueOnly) ? "" : "[Not set: " + strI + strS + label + "]";

			return
				"0x" + SimPe.Helper.HexString((short)bcon[bid])
				+ (detail == Detail.Full ? " - " + strI + strS + label : "")
				;
		}

		protected string readTrcn(Bcon bcon, int bid, Detail detail)
		{
			Trcn trcn = bcon.TrcnResource;
			return (trcn != null && bid < trcn.Count) ? trcn[bid] : ""
				/*(detail == Detail.ValueOnly ? ""
				: "[No TRCN for BCON 0x" + SimPe.Helper.HexString((ushort)bcon.FileDescriptor.Instance)
				+ ":0x" + SimPe.Helper.HexString((byte)bid) + "]")*/
				;
		}


		protected string readParam(Bhav instance, int pno, Detail detail) { return readParamLocal(false, instance, pno, detail); }

		protected string readLocal(Bhav instance, int lno, Detail detail) { return readParamLocal(true, instance, lno, detail); }

		private string readParamLocal(bool local, Bhav bhav, int sid, Detail detail)
		{
			TPRP tprp = bhav.TPRPResource;
			return (tprp != null && sid < (local ? tprp.LocalCount : tprp.ParamCount)) ? tprp[local, sid] : ""
				/*(detail == Detail.ValueOnly ? ""
				: "[No TPRP label for BHAV 0x" + SimPe.Helper.HexString((ushort)bhav.FileDescriptor.Instance)
				+ " " + (local ? "Local" : "Param") + " 0x" + SimPe.Helper.HexString((ushort)sid) + "]")*/
				;
			}


		/// <summary>
		/// Returns a list of Param or Local labels
		/// </summary>
		/// <param name="local">True to retrieve Local labels, false for Params</param>
		/// <returns></returns>
		public ArrayList GetTPRPnames(bool local)
		{
			if (instruction == null || instruction.Parent == null || instruction.Parent.FileDescriptor == null)
				throw new InvalidOperationException("Can't read TPRP for instruction with no parent");

			uint group    = instruction.Parent.FileDescriptor.Group;
			uint instance = instruction.Parent.FileDescriptor.Instance;
			pjse.FileTable.Entry[] items = pjse.FileTable.GFT[0x54505250, group, instance];

			if (items == null || items.Length == 0)
				return null;

			ArrayList TPRPnames = new ArrayList();

			TPRP tprp = new TPRP();
			tprp.ProcessData(items[0].PFD, items[0].Package);
			foreach (TPRPItem i in tprp)
				if ((local && i is TPRPLocalLabel) || (!local && i is TPRPParamLabel))
					TPRPnames.Add(i.Label);
			int limit = local ? instruction.Parent.Header.LocalVarCount : instruction.Parent.Header.ArgumentCount;
			while (TPRPnames.Count < limit)
				TPRPnames.Add("(" + (local ? "Local" : "Param") + TPRPnames.Count.ToString() + ")");
			return TPRPnames;
		}


		public ArrayList GetAttrNames(Scope s)
		{
			if (instruction == null || instruction.Parent == null || instruction.Parent.FileDescriptor == null)
				throw new InvalidOperationException("Can't read STR# for instruction with no parent");

			if (instruction.Parent.Context == Scope.Global && s != Scope.Global
				|| instruction.Parent.Context == Scope.SemiGlobal && s == Scope.Private)
				return null;

			pjse.FileTable.Entry[] items = pjse.FileTable.GFT[(uint)SimPe.Data.MetaData.STRING_FILE, instruction.Parent.GroupForScope(s), (uint)GS.GlobalStr.AttributeLabels];

			if (items == null || items.Length == 0)
				return null;

			StrWrapper str = new StrWrapper();
			str.ProcessData(items[0].PFD, items[0].Package);
			StrItem[] asi = str[(byte)1];

			if (asi == null || asi.Length == 0)
				return null;

			ArrayList al = new ArrayList();
			foreach(StrItem si in asi) al.Add(si.Title);
			return al;
		}


		public static ArrayList flagNames(byte flagOwner, ushort flagType)
		{
			Hashtable flagTypes = (Hashtable)flagOwners[flagOwner];
			return (flagTypes == null || flagTypes[flagType] == null) ? null : BhavWiz.readStr((GS.BhavStr)flagTypes[flagType]);
		}

		public static string flagname(byte flagOwner, ushort flagType, ushort flagValue)
		{
			if (flagValue == 0) return "[0: invalid]";
			Hashtable flagTypes = (Hashtable)flagOwners[flagOwner];
			return (flagTypes == null || flagTypes[flagType] == null) ? null : readStr((GS.BhavStr)flagTypes[flagType], (ushort)(flagValue-1));
		}


		private static Hashtable flagOwners = flagInitaliser();
		private static Hashtable flagInitaliser()
		{
			Hashtable f = new Hashtable();
			Hashtable o = new Hashtable();
			o.Add((ushort)0x05, GS.BhavStr.WallAdjFlags);
			o.Add((ushort)0x08, GS.BhavStr.Flags1);
			o.Add((ushort)0x0d, GS.BhavStr.WallPlacementFlags);
			o.Add((ushort)0x22, GS.BhavStr.HiddenFlags);
			o.Add((ushort)0x28, GS.BhavStr.Flags2);
			o.Add((ushort)0x2a, GS.BhavStr.PlacementFlags);
			o.Add((ushort)0x2b, GS.BhavStr.MoveFlags);
			o.Add((ushort)0x3f, GS.BhavStr.ExclPlacementFlags);
			o.Add((ushort)0x45, GS.BhavStr.WallCutoutFlags);
			f.Add((byte)0x03, o); // 0x03 "My"
			f.Add((byte)0x04, o); // 0x04 "Stack Object's"
			Hashtable p = new Hashtable();
			p.Add((ushort)0x1e, GS.BhavStr.CensorFlags);
			p.Add((ushort)0x44, GS.BhavStr.GhostFlags);
			p.Add((ushort)0x51, GS.BhavStr.BodyFlags);
			p.Add((ushort)0x9e, GS.BhavStr.SelectionFlags);
			p.Add((ushort)0x9f, GS.BhavStr.PersonFlags);
			f.Add((byte)0x12, p); // 0x12 "My Person Data"
			f.Add((byte)0x13, p); // 0x13 "Stack Object's Person Data"
			f.Add((byte)0x20, p); // 0x20 "Neighbour's Person Data"
			Hashtable d = new Hashtable();
			d.Add((ushort)0x27, GS.BhavStr.RoomSortFlags);
			d.Add((ushort)0x28, GS.BhavStr.FunctionSortFlags);
			f.Add((byte)0x15, d); // 0x15 "stack object's definition"
			f.Add((byte)0x26, d); // 0x26 "Neighbor's Object Definition"
			f.Add((byte)0x33, d); // 0x33 "Stack Object's Master Definition"
			return f;
		}


		public static Glob GlobByGroup(uint group)
		{
			pjse.FileTable.Entry[] items = pjse.FileTable.GFT[(uint)SimPe.Data.MetaData.GLOB_FILE, group];
			if (items == null || items.Length == 0) return null;

			Glob glob = new Glob();
			glob.ProcessData(items[0].PFD, items[0].Package);
			return glob;
		}


#if UNDEF
		/*
		 * From disaSim2-23d
		 */
        case 0x1A:  // Constant Value
            a = x >> 13;            // x = aaabbbbb bccccccc
            b = (x >> 7) & 0x3F;
            c = x & 0x7F;

            if (a & 4) {            // extended
                b += 0x40;
                a &= 3;
            }
            switch (a) {
                case 0:             // private
                    b += 0x1000;
                    break;
                case 1:             // semi-global
                    b += 0x2000;
                    break;
                case 2:             // global
                    b += 0x100;
                    break;
                case 3:             // FUBAR
                    b = 0xF5BA;
                    break;
            }
//            ht_fprintf(outFile,TYPE_DATA,"%s 0x%X:0x%X", gString84[o], b, c);
            ht_fprintf(outFile,TYPE_DATA,"%s 0x%X", gString84[o], b);
            readConst2(b, c);
            break;
        case 0x2F:  // Constants [temp]
            a = x >> 13;            // x = aaabbbbb bbbbbccc
            b = (x >> 3) & 0x3FF;
            c = x & 7;

            if (a & 4) {            // extended
                b += 0x40;
                a &= 3;
            }
            switch (a) {
                case 0:             // private
                    b += 0x1000;
                    break;
                case 1:             // semi-global
                    b += 0x2000;
                    break;
                case 2:             // global
                    b += 0x100;
                    break;
                case 3:             // FUBAR
                    b = 0xF5BA;
                    break;
            }
            ht_fprintf(outFile,TYPE_DATA,"%s ", gString84[o]);
            ht_fprintf(outFile,TYPE_DATA,"0x%X", b);
            readConst2(b, 0xFFFF);
            ht_fprintf(outFile,TYPE_DATA,":[Temp %d]", c);
            break;
#endif

		// not temp:
		// x = baabbbbb bccccccc, where a is scope, b is BCON instance and c is constant id
		// temp:
		// x = baabbbbb bbbbbccc, where a is scope, b is BCON instance and c is temp #
		public static ushort[] ExpandBCON(ushort instance, bool temp)
		{
			ushort[] result = new ushort[2];
			result[1] = (ushort)(instance & (!temp ? 0x7f : 0x07));	// ........ .ccccccc -or- ........ .....ccc

			int b;
			if (!temp) b = ((instance >> 9) & 0x0040) | ((instance >> 7) & 0x003f);	// b..bbbbb b.......
			else       b = ((instance >> 5) & 0x0400) | ((instance >> 3) & 0x03ff);	// b..bbbbb bbbbb...

			int a = (instance >> 13) & 0x03;						// .aa..... ........
			switch (a)
			{
				case 0: b += 0x1000; break; // private
				case 1: b += 0x2000; break; // semi-global
				case 2: b += 0x0100; break; // global
				//case 3: b |= 0xF5BA; break; // do nothing
			}

			result[0] = (ushort)b;
			return result;
		}

		public static ushort ExpandBCON(ushort[] values, bool temp)
		{
			int output = 0;

			int b = values[0];
			if (!temp) { output = (b & 0x0040) << 9; b -= (b & 0x0040); }	// b....... ........
			else       { output = (b & 0x0400) << 5; b -= (b & 0x0400); }	// b....... ........

			int a;
			if      ((b & 0x2000) != 0) { b -= 0x2000; a = 1; }			// Semi-Global
			else if ((b & 0x1000) != 0) { b -= 0x1000; a = 0; }			// Private
			else if ((b & 0x0300) == 0x0100) { b -= 0x0100; a = 2; }	// Global
			else                        {              a = 3; }			// do nothing
			output |= (a << 13);							// .aa..... ........

			if (!temp) output |= (b & 0x003f) << 7;			// ...bbbbb b.......
			else       output |= (b & 0x03ff) << 3;			// ...bbbbb bbbbb...

			output |= values[1] & (!temp ? 0x7f : 0x07);	// ........ .ccccccc -or- ........ .....ccc

			return (ushort)output;
		}

		public static string ExpandBCONtoString(ushort instance, bool temp)
		{
			ushort[] result = ExpandBCON(instance, temp);
			return !temp
				? "0x" + SimPe.Helper.HexString(result[0]) + ":0x" + SimPe.Helper.HexString((byte)result[1])
				: "0x" + SimPe.Helper.HexString(result[0]) + ":[Temp " + result[1].ToString() + "]";
		}

		public static ushort StringtoExpandBCON(string text, bool temp)
		{
			string[] s = text.Split(":".ToCharArray(), 2);
			if (s.Length != 2
				|| (temp && !(s[1].StartsWith("[Temp ") && s[1].EndsWith("]") && s[1].Length.Equals(8))))
				throw new InvalidCastException();

			ushort[] b = new ushort[2];
			b[0] = Convert.ToUInt16(s[0], 16);
			b[1] = !temp
				? Convert.ToUInt16(s[1], 16)
				: Convert.ToUInt16(s[1].Substring(6, 1));

			ushort c = ExpandBCON(b, temp);

			ushort[] d = ExpandBCON(c, temp);
			if (d[0] != b[0] || d[1] != b[1])
				throw new InvalidCastException();

			return c;
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

	public interface IDataOwner
	{
		byte DataOwner { get; }
		ushort Value { get; }
	}

	public interface IDataOwnerListener : IDataOwner
	{
		IDataOwner FlagsFor { set; }
		void Notify();
	}

}

