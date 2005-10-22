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
		public static string GStr(BhavStr instance, ushort sid)
		{
			ArrayList str = gStr(instance);
			if (str != null && sid < str.Count)
				return (string)str[sid];
			else
				return "[" + SimPe.Localization.Manager.GetString("unk") + ": 0x" + SimPe.Helper.HexString(sid) + "]";
		}


		public static ArrayList gStr(BhavStr instance)
		{
			LoadData(instance);
			return (ArrayList)gString[instance];
		}


		private static Hashtable gString = new Hashtable();

		private static void LoadData(BhavStr instance)
		{
			if (gString[instance] != null) return;

			ArrayList list = new ArrayList();

			pjse.FileTable.Entry[] items = pjse.FileTable.GFT[(uint)SimPe.Data.MetaData.STRING_FILE, 0x7FE59FD0, (uint)instance];
			if (items == null || items.Length == 0) return;

			SimPe.PackedFiles.Wrapper.Str str = new SimPe.PackedFiles.Wrapper.Str();
			str.ProcessData(items[0].PFD, items[0].Package);

			SimPe.PackedFiles.Wrapper.StrItem si;
			for(int i = 0; (si = str[1, i]) != null; i++)
				list.Add(si.Title);

			gString[instance] = list;
		}


		public enum BhavStr : uint
		{
			//Str0x0080 = 0x80, // behavior strings
			GlobalLabels = 0x81,
			//Str0x0082 = 0x82, // relative locations
			//Str0x0083 = 0x83, // relative directions
			DataOwners = 0x84,
			Motives = 0x86,
			//Str0x0087 = 0x87, // miscellaneous strings
			Operators = 0x88,
			//Str0x0089 = 0x89, // unused - search types
			Primitives = 0x8b,
			DataLabels = 0x8d, // Data labels +EP1 +EP2
			Flags1 = 0x8e, // flags for flag field
			BodyFlags = 0x8f,
			NextObject = 0xa4,
			ObjectPlace = 0xa7,
			CensorFlags = 0xb2,
			PersonData = 0xc8,
			PlacementFlags = 0xca, // Allowed Height Flags
			MoveFlags = 0xcb,
			OBJDDescs = 0xcc,
			RoomSortFlags = 0xcd,
			FunctionSortFlags = 0xce,
			WallAdjFlags = 0xd0,
			Flags2 = 0xd6, // Flags for Flag Field 2
			Dialog = 0xd9,
			DialogDesc = 0xda,
			RoomValues = 0xdb,
			Generics = 0xdc,
			NeighborData = 0xdd,
			Priorities = 0xe0,
			WallPlacementFlags = 0xe5,
			FindGLB = 0xef,
			JobData = 0xf3,
			DialogIcon = 0xf4,
			OBJFDescs = 0xf5,
			NeighborhoodData = 0xf9,
			ExclPlacementFlags = 0xfb,
			InventoryDialog = 0xfc,
			WallCutoutFlags = 0xfd,
			HiddenFlags = 0x200,
			GhostFlags = 0x201,
			SelectionFlags = 0x202,
			PersonFlags = 0x204,
		}

		public enum GlobalStr : uint
		{
			gMesgGroup = 0x0087,
			gMaterialName = 0x0088,
			gMyAttributeLabels = 0x100,
			gDialogPrim = 0x012d,
			gMakeAction = 0x012e,
			gNamedTreePrim = 0x012f,
		}
	}
}
