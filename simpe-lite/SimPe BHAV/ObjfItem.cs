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
		int nr;
		public int LineNumber 
		{
			get {return nr; }
			set {nr = value;}
		}

		public string Name 
		{
			get 
			{
				if (parent.Opcodes==null) return Localization.Manager.GetString("Unknown");
				else return parent.Opcodes.StoredObjfLines[nr].ToString();
			}
		}

		ushort guard;
		public ushort Guardian
		{
			get {return guard; }
			set {guard = value;}
		}

		ushort action;
		public ushort Action
		{
			get {return action; }
			set {action = value;}
		}

		private Objf parent;

		public ObjfItem(Objf parent)
		{
			this.parent = parent;
			guard = action = 0;
		}

		public override string ToString()
		{
			string add = "";
			if ((guard==0) && (action==0)) add="            ";
			if (parent.Opcodes==null) 
			{
				return add+Name+": 0x"+Helper.HexString(guard)+" -> 0x"+Helper.HexString(action);
			} 
			else 
			{
				string guardname = OpcodeName(this.guard);
				string actionname = OpcodeName(this.action);
				
				if (guard==0) guardname = "null";
				if (action==0) actionname = "null";

				return add+Name+": "+guardname+" -> "+actionname;				
			}
		}

		private string OpcodeName(ushort opcode) 
		{
			Bhav bhav = new Bhav(parent.Opcodes);
			bhav.Package = parent.Package;
			SimPe.Packages.PackedFileDescriptor pfd = new SimPe.Packages.PackedFileDescriptor();
			pfd.Group = parent.FileDescriptor.Group;
			bhav.FileDescriptor = pfd;

			return UserInterface.BhavOperandWiz.OpcodeName(bhav, opcode);
		}

	}
}
