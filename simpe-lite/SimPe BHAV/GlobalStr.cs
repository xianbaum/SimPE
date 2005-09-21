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
using SimPe.Data;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Plugin.Internal;
using SimPe.Interfaces;
using SimPe.Interfaces.Files;
using SimPe.Providers;

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


		public static ArrayList gStr(uint instance)
		{
			if (gStrPackage == null) LoadPackage();
			if (gString[instance] == null) LoadData(instance);
			return (ArrayList)gString[instance];
		}

		public static ArrayList gStr(SF instance) { return gStr((uint)instance); }

		public static string GStr(uint instance, ushort sid)
		{
			if (gStr(instance) != null && sid < gStr(instance).Count)
				return (string)gStr(instance)[sid];
			else
				return "[" + SimPe.Localization.Manager.GetString("unk") + ": 0x" + SimPe.Helper.HexString(sid) + "]";
		}

		public static string GStr(SF instance, ushort sid) { return GStr((uint)instance, sid); }


		private static SimPe.Interfaces.Files.IPackageFile gStrPackage = null;
		private static Hashtable gString = null;
		/// <summary>
		/// Loads the ObjectsPackage if not already loaded
		/// </summary>
		private static void LoadPackage() 
		{
			string file = System.IO.Path.Combine(SimPe.Helper.SimPePluginPath, "pjse.coder.plugin\\GlobalStrings.package");
			if (!System.IO.File.Exists(file))
			{
				SimPe.Registry reg = SimPe.Helper.WindowsRegistry;
				file = System.IO.Path.Combine(reg.SimsPath, "TSData\\Res\\Objects\\objects.package");
			}
			if (System.IO.File.Exists(file))
				gStrPackage = SimPe.Packages.File.LoadFromFile(file);
			else
				gStrPackage = null;
			gString = new Hashtable();
		}

		/// <summary>
		/// Loads String Resource from the Package
		/// </summary>
		/// <param name="list">The List where you want to store the Resource</param>
		/// <param name="instance">The Instance of the TextFile</param>
		/// <param name="lang">The Language Number</param>
		private static void LoadData(uint instance)
		{
			ArrayList list = new ArrayList();
			gString[instance] = list;

			if (gStrPackage == null) return;

			IPackedFileDescriptor pfd = gStrPackage.FindFile(SimPe.Data.MetaData.STRING_FILE, 0x00000000, 0x7FE59FD0, instance);
			if (pfd == null) return;

			SimPe.PackedFiles.Wrapper.Str str = new SimPe.PackedFiles.Wrapper.Str();
			str.ProcessData(pfd, gStrPackage);

			SimPe.PackedFiles.Wrapper.StrItem si;
			for(int i = 0; (si = str[1, i]) != null; i++)
				list.Add(si.Title);
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
			Generics = 0xdc,
			OBJFDescs = 0xf5,
			gExclPlacementFlags = 0xfb,
			gWallCutoutFlags = 0xfd,
			gHiddenFlags = 0x200,
			gGhostFlags = 0x201,
			gSelectionFlags = 0x202,
			gPersonFlags = 0x204,
		}
	}
}
