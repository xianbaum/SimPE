/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
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
using System.IO;
using System.Collections;
using SimPe.Interfaces.Files;

namespace pjse
{
	/// <summary>
	/// Provides an Alias Matching a SimID with it's Name
	/// </summary>
	public class GS
	{
		/// <summary>
		/// Creates the List for the specific Folder
		/// </summary>
		public GS() { }


		public static string GStr(uint instance, ushort sid)
		{
			ArrayList str = gStr(instance);
			if (str != null && sid < str.Count)
				return (string)str[sid];
			else
				return "[" + SimPe.Localization.Manager.GetString("unk") + ": 0x" + SimPe.Helper.HexString(sid) + "]";
		}

		public static string GStr(SF instance, ushort sid) { return GStr((uint)instance, sid); }


		public static ArrayList gStr(uint instance)
		{
			LoadData(instance);
			return (ArrayList)gString[instance];
		}

		public static ArrayList gStr(SF instance) { return gStr((uint)instance); }


		private static SimPe.Interfaces.Files.IPackageFile gStrPackage = null;
		private static Hashtable gString = new Hashtable();
		private static void LoadData(uint instance)
		{
			LoadPackage();
			if (gStrPackage == null) return;

			if (gString[instance] != null) return;

			ArrayList list = new ArrayList();

			IPackedFileDescriptor pfd = gStrPackage.FindFile(SimPe.Data.MetaData.STRING_FILE, 0x00000000, 0x7FE59FD0, instance);
			if (pfd == null) pfd = gStrPackage.FindFile(SimPe.Data.MetaData.STRING_FILE, 0x00000000, 0x7FD46CD0, instance);
			if (pfd == null) return;

			SimPe.PackedFiles.Wrapper.Str str = new SimPe.PackedFiles.Wrapper.Str();
			str.ProcessData(pfd, gStrPackage);

			SimPe.PackedFiles.Wrapper.StrItem si;
			for(int i = 0; (si = str[1, i]) != null; i++)
				list.Add(si.Title);

			gString[instance] = list;
		}

		private static void LoadPackage() 
		{
			if (gStrPackage != null) return;

			string file = System.IO.Path.Combine(SimPe.Helper.SimPePluginPath, "pjse.coder.plugin\\GlobalStrings.package");
			if (!System.IO.File.Exists(file))
				file = System.IO.Path.Combine(SimPe.Helper.WindowsRegistry.SimsPath, "TSData\\Res\\Objects\\objects.package");
			if (System.IO.File.Exists(file))
				gStrPackage = SimPe.Packages.File.LoadFromFile(file);
		}


		public enum SF : uint
		{
			DataOwners = 0x84,
			Motives = 0x86,
			Operators = 0x88,
			Primitives = 0x8b,
			gFlags1 = 0x8e,
			gBodyFlags = 0x8f,
			gCensorFlags = 0xb2,
			gPlacementFlags = 0xca,
			gMoveFlags = 0xcb,
			OBJDDescs = 0xcc,
			gRoomSortFlags = 0xcd,
			gFunctionSortFlags = 0xce,
			gWallAdjFlags = 0xd0,
			gFlags2 = 0xd6,
			Dialog = 0xd9,
			Generics = 0xdc,
			DialogIcon = 0xf4,
			OBJFDescs = 0xf5,
			gExclPlacementFlags = 0xfb,
			gWallCutoutFlags = 0xfd,
			gDialogPrim = 0x12d,
			gHiddenFlags = 0x200,
			gGhostFlags = 0x201,
			gSelectionFlags = 0x202,
			gPersonFlags = 0x204,
		}
	}
}
