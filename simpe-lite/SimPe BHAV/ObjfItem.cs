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

namespace SimPe.PackedFiles.Wrapper
{
	/// <summary>
	/// An Item stored in an OBJf
	/// </summary>
	public class ObjfItem
	{
		ushort nr;
		ushort guard;
		ushort action;
		private Objf parent;
		public ushort LineNumber 
		{
			get {return nr; }
			set {nr = value;}
		}

		public string Name { get { return pjse.GS.GStr(pjse.GS.BhavStr.OBJFDescs, nr); } }

		public ushort Guardian
		{
			get {return guard; }
			set {guard = value;}
		}

		public ushort Action
		{
			get {return action; }
			set {action = value;}
		}


		public ObjfItem(Objf parent)
		{
			this.parent = parent;
			guard = action = 0;
		}

		public override string ToString()
		{
			pjse.FileTable.Entry eg = parent.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, guard);
			pjse.FileTable.Entry ea = parent.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, action);

			string add = "";
			if ((guard == 0 && action == 0) || (eg == null && ea == null))
				add="            ";

			return add + Name +
				": " + (guard == 0 ? "not set" : "0x" + Helper.HexString(guard) + ": " + (eg == null ? "not found" : eg)) +
				" -> " + (action == 0 ? "not set" : "0x" + Helper.HexString(action) + ": " + (ea == null ? "not found" : ea));
		}

	}
}
