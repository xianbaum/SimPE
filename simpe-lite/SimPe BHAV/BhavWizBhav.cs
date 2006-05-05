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
        private Bhav wrapper = null;

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
				prefix = pjse.Localization.GetString("lcGlobal");
				group = i.Parent.GlobalGroup;
			}

			else if (i.OpCode < 0x2000)
			{
				prefix = pjse.Localization.GetString("lcPrivate");
				group = (i.Parent.Context == Scope.Private) ? i.Parent.Group : 0xffffffff;
			}

			else
			{
				prefix = pjse.Localization.GetString("lcSemiGlobal");
				group = (i.Parent.Context == Scope.SemiGlobal) ? i.Parent.Group : i.Parent.SemiGroup;
			}
		}

		public static implicit operator BhavWizBhav(Instruction i)
		{
			if (i.OpCode < 0x0100)
				throw new InvalidCastException("OpCode not a BHAV");

			return new BhavWizBhav(i);
		}


		#region IDisposable Members

		public new void Dispose() { GFT_FiletableRefresh(null, null); }

		#endregion

        void GFT_FiletableRefresh(object sender, EventArgs e)
        {
            FileTable.GFT.FiletableRefresh -= new EventHandler(GFT_FiletableRefresh);
            ftEntry = null;
            wrapper = null;
        }

		private Bhav Wrapper
		{
			get
			{
                if (wrapper != null) return wrapper;

                if (ftEntry == null)
                {
                    if (instruction == null || instruction.Parent == null)
                        throw new Exception("Can't find wrapper for instruction with no parent");

                    ftEntry = instruction.Parent.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, instruction.OpCode);
                    if (ftEntry == null) return null;
                }

				wrapper = new Bhav();
				wrapper.ProcessData(ftEntry.PFD, ftEntry.Package);
                FileTable.GFT.FiletableRefresh += new EventHandler(GFT_FiletableRefresh);
				return wrapper;
			}
		}


		protected override string OpcodeName
		{
			get
			{
                pjse.FileTable.Entry ftEntry = instruction.Parent.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, instruction.OpCode);
                return (ftEntry != null) ? ftEntry : pjse.Localization.GetString("bhavnotfound");
			}
		}


		/// <summary>
		/// Returns a description of the operands of the call to another BHAV
		/// </summary>
		/// <param name="lng">true to get long description</param>
		/// <returns>description of the BHAV call operands</returns>
		/// <remarks>See http://www5.modthesims2.com/showthread.php?goto=newpost&t=117411 for more info</remarks>
		protected override string Operands(bool lng)
		{
			Bhav bhav = Wrapper;
			if (bhav == null)
				return "???";

			int myArgc = (int)instruction.Parent.Header.ArgumentCount;
			int thisArgc = bhav.Header.ArgumentCount;

			if (thisArgc == 0)
                return lng ? pjse.Localization.GetString("noargs") : "";

			string s = "";
			if (lng)
				s += thisArgc.ToString() + " "
                    + (thisArgc == 1
                        ? pjse.Localization.GetString("oneArg")
                        : pjse.Localization.GetString("manyArgs"))
                    + ": ";

			byte[] o = new byte[16];
			((byte[])instruction.Operands).CopyTo(o, 0);
			((byte[])instruction.Reserved1).CopyTo(o, 8);

			bool noOperands = true;
			for (int i = 0; noOperands && i < 8; i++)
				noOperands = o[i] == 0xFF;

			byte nv = instruction.NodeVersion;
			Boolset b12 = o[12];
			TPRP tprp = bhav.TPRPResource;


			if (!b12[0]) // b12.Matches("xxxxxxx0")
			{
				if (!noOperands)
				{
					if (nv == 0 || !b12[1]) // b12.Matches("xxxxxx00")
						s += this.do8Cx(thisArgc, lng, tprp, o, nv == 0);
					else // nv == 1 && b12.Matches("xxxxxx10")
					{
						if (thisArgc < 9)
							s += this.doParams(thisArgc, myArgc, lng, tprp);
						else
							s += this.doZero(thisArgc, lng, tprp);
					}
				}
				else // noOperands
				{
					if (nv == 0 || !b12[1]) // b12.Matches("xxxxxx00")
						s += this.doUnknown(thisArgc, lng, tprp);
					else // nv == 1 && b12.Matches("xxxxxx10")
						s += this.doZero(thisArgc, lng, tprp);
				}
			}
			else // b12.Matches("xxxxxxx1")
			{
				if (nv == 0 || o[12] != 0xff)
				{
					if (!noOperands && thisArgc < 9)
						s += this.do4OI(thisArgc, lng, tprp, o);
					else
						s += this.doZero(thisArgc, lng, tprp);
				}
				else // nv == 1 && o12 = 0xff
				{
					if (!noOperands)
						s += this.do8Cx(thisArgc, lng, tprp, o, false);
					else
						s += this.doUnknown(thisArgc, lng, tprp);
				}
			}

			return s;
		}


		private string do4OI(int thisArgc, bool lng, TPRP tprp, byte[] o)
		{
			string s = "";
			for (int i = 0; thisArgc > 0 && i < 4; i++, thisArgc--)
			{
				string pn = (lng && tprp != null && tprp.ParamCount > i) ? tprp[false, i] : "";
				s += (i>0 ? ", " : "") +
					((pn != null && pn != "") ? pn + "=" : "") +
					dataOwner(lng, o[i*3], o[(i*3) + 1], o[(i*3) + 2]);
			}
			if (thisArgc > 0)
				s += doZero(thisArgc, lng, tprp, 4);
			return s;
		}

		private string do8Cx(int thisArgc, bool lng, TPRP tprp, byte[] o, bool z12)
		{
			string s = "";
			for (int i = 0; thisArgc > 0 && i < 8; i++, thisArgc--)
			{
				string pn = (lng && tprp != null && tprp.ParamCount > i) ? tprp[false, i] : "";
				s += (i>0 ? ", " : "") +
					((pn != null && pn != "") ? pn + "=" : "") +
					"0x" + SimPe.Helper.HexString(ToShort((z12 && i == 6) ? (byte)0 : o[(i*2)], o[(i*2) + 1]));
			}
			if (thisArgc > 0)
				s += doZero(thisArgc, lng, tprp, 8);
			return s;
		}

		private string doParams(int thisArgc, int myArgc, bool lng, TPRP tprp)
		{
			if (!lng)
                return pjse.Localization.GetString("bwb_callerparams");

			string s = "";
			for (int i = 0; thisArgc > 0 && i < myArgc; i++, thisArgc--)
			{
				string pn = (lng && tprp != null && tprp.ParamCount > i) ? tprp[false, i] : "";
				s += (i>0 ? ", " : "") +
					((pn != null && pn != "") ? pn + "=" : "") + dataOwner(9, (ushort)i);
			}
			if (thisArgc > 0)
				s += doZero(thisArgc, lng, tprp, myArgc);
			return s;
		}

		private string doZero(int thisArgc, bool lng, TPRP tprp) { return this.doZero(thisArgc, lng, tprp, 0); }

		private string doZero(int thisArgc, bool lng, TPRP tprp, int start)
		{
			if (start >= 8)
				return doUnknown(thisArgc, lng, tprp, start);

			if (!lng)
				return (start > 0 ? "," : pjse.Localization.GetString("allZeros"));

			string s = "";
			for (int i = start; thisArgc > 0 && i < 8; i++, thisArgc--)
			{
				string pn = (lng && tprp != null && tprp.ParamCount > i) ? tprp[false, i] : "";
				s += (i>0 ? ", " : "") +
					((pn != null && pn != "") ? pn + "=" : "") + "0x0000";
			}
			if (thisArgc > 0)
				s += doUnknown(thisArgc, lng, tprp, 8);
			return s;
		}

		private string doUnknown(int thisArgc, bool lng, TPRP tprp) { return this.doUnknown(thisArgc, lng, tprp, 0); }

		private string doUnknown(int thisArgc, bool lng, TPRP tprp, int start)
		{
			if (!lng)
				return (start > 0 ? ", " : "")
                    + pjse.Localization.GetString("unkops");

			string s = "";
			for (int i = start; thisArgc > 0; i++, thisArgc--)
			{
				string pn = (lng && tprp != null && tprp.ParamCount > i) ? tprp[false, i] : "";
				s += (i>0 ? ", " : "") +
					((pn != null && pn != "") ? pn + "=" : "") + "????";
			}
			return s;
		}

	}

}
