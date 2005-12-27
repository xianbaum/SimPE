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
using SimPe.Interfaces.Files;
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper;
using SimPe.Interfaces.Scenegraph;

namespace pjse.BhavNameWizards
{

	/// <summary>
	/// Abstract class for BHAV name providers (global, local, semiglobal)
	/// </summary>
	public class BhavWizBhav : BhavWiz, IDisposable
	{
		private pjse.FileTable.Entry ftEntry = null;
		/// <summary>
		/// Which group to look in for the BHAV
		/// </summary>
		private uint group = 0;

		public BhavWizBhav(Instruction i) : base (i)
		{
			if (i.OpCode < 0x0100)
				throw new InvalidOperationException("OpCode not a BHAV");

			if (i.OpCode < 0x1000)
			{
				prefix = "global";
				group = i.Parent.GlobalGroup;
			}

			else if (i.OpCode < 0x2000)
			{
				prefix = "private";
				group = (i.Parent.Context == Scope.Private) ? i.Parent.Group : 0xffffffff;
			}

			else
			{
				prefix = "semi";
				group = (i.Parent.Context == Scope.SemiGlobal) ? i.Parent.Group : i.Parent.SemiGroup;
			}

		}

		public static implicit operator BhavWizBhav(Instruction i)
		{
			if (i.OpCode < 0x0100)
				throw new InvalidCastException("OpCode not a BHAV");

			return new BhavWizBhav(i);
		}

		public Bhav Wrapper
		{
			get
			{
				pjse.FileTable.Entry ftEntry = FTEntry;
				if (ftEntry == null) return null;
				Bhav wrapper = new Bhav();
				wrapper.ProcessData(ftEntry.PFD, ftEntry.Package);
				return wrapper;
			}
		}


		#region BhavWiz
		protected override string OpcodeName
		{
			get
			{
				pjse.FileTable.Entry ftEntry = FTEntry;
				return (ftEntry != null) ? ftEntry : "[BHAV not found]";
			}
		}


		public override pjse.FileTable.Entry FTEntry
		{
			get
			{
				if (ftEntry == null)
				{
					pjse.FileTable.Entry[] items = pjse.FileTable.GFT[SimPe.Data.MetaData.BHAV_FILE, group, instruction.OpCode];
					if(items != null && items.Length > 0) ftEntry = items[0];
				}
				return ftEntry;
			}
		}


		protected override string Operands(bool lng)
		{
			Bhav bhav = Wrapper;
			if (bhav == null)
				return "???";

			string s = "";
			int myArgc = (int)instruction.Parent.Header.ArgumentCount;
			int thisArgc = bhav.Header.ArgumentCount;

			if (lng)
			{
				s += (thisArgc == 0) ? "no" : thisArgc.ToString();
				s += " arg" + (thisArgc == 1 ? "" : "s");
			}

			if (thisArgc > 0)
			{
				byte[] o = new byte[16];
				((byte[])instruction.Operands).CopyTo(o, 0);
				((byte[])instruction.Reserved1).CopyTo(o, 8);

				s += (lng ? ": " : "");


				bool noOperands = o[12] == 0; // original format; check for "operand killer"
				for (int i = 0; noOperands && i < 8; i++)
					noOperands = o[i] == 0xFF;

				int p = 0;
				if ((o[12] & 0x02) != 0)	// Params first
				{
					int i = Math.Min(thisArgc, myArgc);
					if (i > 0)
						s += (lng ? "Caller's params " + p.ToString() + (i > 1 ? " to " + (i - 1).ToString() : "") : "Caller's params");
					thisArgc -= i;
					myArgc -= i;
					p += i;
					if (i > 0 && thisArgc > 0) s += ", ";
				}


				if ((o[12] & 0x01) == 0) // original format
				{
					for (int i = 0; !noOperands && thisArgc > 0 && i < 4; i++, thisArgc--)
					{
						string pn = lng ? readAnyTPRP(bhav.FileDescriptor.Group, bhav.FileDescriptor.Instance, false, i, true) : "";
						s += (i>0 ? ", " : "") +
							((pn != null && pn != "") ? pn + "=" : "") +
                            "0x" + SimPe.Helper.HexString(ToShort(o[(i*2)], o[(i*2) + 1]));
					}
				}
				else	// 16 byte format
				{
					for (int i = 0; thisArgc > 0 && i < 4; i++, thisArgc--)
					{
						string pn = lng ? readAnyTPRP(bhav.FileDescriptor.Group, bhav.FileDescriptor.Instance, false, i, true) : "";
						s += (i>0 ? ", " : "") +
							(pn == "" ? pn : pn + "=") +
							dataOwner(lng, o[i*3], o[(i*3) + 1], o[(i*3) + 2]);
					}
				}

				if (thisArgc > 0)
				{
					s += (noOperands ? "" : ", ")
						+ (lng ? "Caller's params " + p.ToString() + (thisArgc > 1 ? " to " + (p + thisArgc - 1).ToString() : "") : "Caller's params");
				}
			}
			return s;

		}

		#endregion

		#region IDisposable Members

		public new void Dispose()
		{
			ftEntry = null;
		}

		#endregion

	}

}
