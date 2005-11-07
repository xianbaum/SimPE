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
			RelativeLocations = 0x0082,
			RelativeDirections = 0x0083,
			DataOwners = 0x84,
			Motives = 0x86,
			//Str0x0087 = 0x87, // miscellaneous strings
			Operators = 0x88,
			//Str0x0089 = 0x89, // unused - search types
			Primitives = 0x8b,
			DataLabels = 0x8d, // Data labels +EP1 +EP2
			Flags1 = 0x8e, // flags for flag field
			BodyFlags = 0x8f,
			//Str0x0096 unused
			ShortOwner = 0x99,
			//Str0x009a unused
			//Str0x009b unused
			//Str0x009c kill object options
			//Str0x009d tree types
			//Str0x009e .. a3 unused
			NextObject = 0xa4,
			MotiveType = 0xa5, // motive search types
			CreatePlace = 0xa7, // where to create object
			CreateHow = 0xa8, // how to create object
			//Str0x00a9 .. ab unused
			//Str0x00ac Interrupt (idle for input?)
			//Str0x00ad .. ae unused
			//Str0x00b0 0123
			//Str0x00b1 unused
			CensorFlags = 0xb2,
			PersonData = 0xc8,
			FunctionTable = 0xc9,
			PlacementFlags = 0xca, // Allowed Height Flags
			MoveFlags = 0xcb,
			OBJDDescs = 0xcc, // Object Definitions
			RoomSortFlags = 0xcd,
			FunctionSortFlags = 0xce,
			SnapType = 0xcf, // How To Snap
			WallAdjFlags = 0xd0,
			//Str0x00d1 .. d2 unused
			UpdateWho = 0xd3,
			UpdateWhat = 0xd4,
			//Str0x00d5 unused
			Flags2 = 0xd6, // Flags for Flag Field 2
			//Str0x00d7 Routing slot param types (Go To Routing slot - not used here)
			TurnBody = 0xd8,
			Dialog = 0xd9,
			DialogDesc = 0xda,
			RoomValues = 0xdb,
			Generics = 0xdc,
			NeighborData = 0xdd,
			RTBNType = 0xde, // how to call named tree
			Priorities = 0xe0,
			//Str0x00e1 times of day
			//Str0x00e2 tree categories
			//Str0x00e3 .. e4 unused
			WallPlacementFlags = 0xe5,
			//Str0x00e6 entry types
			//Str0x00e7 unused
			//Str0x00e8 personality ads
			Attenuations = 0xe9,
			//Str0x00ea .. ec unused
			//Str0x00ed dialog behavior (Dialog - not used here)
			//Str0x00ee situation action descriptions
			FindGLB = 0xef, // find good location behaviors
			ExpenseType = 0xf0,
			//Str0x00f1 route results
			//Str0x00f2 add subtract
			JobData = 0xf3,
			DialogIcon = 0xf4,
			OBJFDescs = 0xf5, // entry points
			//Str0x00f6 object types
			//Str0x00f7 .. f8 unused
			NeighborhoodData = 0xf9,
			PersonOutfits = 0xfa,
			ExclPlacementFlags = 0xfb,
			InventoryDialog = 0xfc,
			WallCutoutFlags = 0xfd,
			//Str0x00fe unused
			//Str0x01f4 .. 1fd unused
			GosubAction = 0x1fe,
			//Str0x01ff Routing slot directions
			HiddenFlags = 0x200,
			GhostFlags = 0x201,
			SelectionFlags = 0x202,
			//Str0x0203 unused
			PersonFlags = 0x204,
		}

		public enum GlobalStr : uint
		{
			AnimsAdult       = 0x0081,
			AnimsChild       = 0x0082,
			LocoAnums        = 0x0084,
			MeshGroup        = 0x0087,
			MaterialName     = 0x0088,
			AnimsToddler     = 0x0089,
			Effect           = 0x008f,
			AnimsBaby        = 0x0091,
			ReachAnims       = 0x0092,
			IconTexture      = 0x0095,
			UIEffect         = 0x0096,
			//CineCam          = 0x0097,
			MyAttributeLabel = 0x0100,
			Relationship     = 0x0102,
			DialogString     = 0x012d,
			MakeAction       = 0x012e,
			NamedTree        = 0x012f,
			LuaScript        = 0x0130,
			Sound            = 0x4132,
		}
	}
}
