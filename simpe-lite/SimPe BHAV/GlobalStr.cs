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
	public class GlobalStr
	{
		/// <summary>
		/// Creates the List for the specific Folder
		/// </summary>
		public GlobalStr() { }


		public static ArrayList gStr(uint instance)
		{
			if (gStrPackage == null) LoadPackage();
			if (gString[instance] == null) LoadData(instance);
			return (ArrayList)gString[instance];
		}


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


		/// <summary>
		/// Returns the names of the Data in an Expression Primitive
		/// </summary>
		public static ArrayList StoredDataNames { get { return gStr(0x84); } }

		/// <summary>
		/// Returns the the name of a Data Owner
		/// </summary>
		/// <param name="owner">Numerical Value of the Owner</param>
		/// <returns>The Name</returns>
		public static string DataOwnerName(byte owner)
		{
			if (StoredDataNames != null && owner < StoredDataNames.Count)
				return (string)StoredDataNames[owner];
			else
				return "[" + SimPe.Localization.Manager.GetString("unk") + ": 0x" + SimPe.Helper.HexString(owner) + "]";
		}


		/// <summary>
		/// Returns the List of known Primitives
		/// </summary>
		public static ArrayList StoredMotives { get { return gStr(0x86); } }

		/// <summary>
		/// Returns the the name of a Motive
		/// </summary>
		/// <param name="owner">Numerical Value</param>
		/// <returns>The Name</returns>
		public static string MotiveName(ushort nr)
		{
			if (StoredMotives != null && nr < StoredMotives.Count)
				return (string)StoredMotives[nr];
			else
				return "[" + SimPe.Localization.Manager.GetString("unk") + ": 0x" + SimPe.Helper.HexString(nr) + "]";
		}


		/// <summary>
		/// Returns the names Operatores in Expression Primitives
		/// </summary>
		public static ArrayList StoredExpressionOperators { get { return gStr(0x88); } }

		/// <summary>
		/// Returns the the name of the Expression Operator
		/// </summary>
		/// <param name="op">Numerical Value of the Operator</param>
		/// <returns>The Name of The Operator</returns>
		public static string FindExpressionOperator(byte op)
		{
			if (StoredExpressionOperators != null && op < StoredExpressionOperators.Count)
				return (string)StoredExpressionOperators[op];
			else
				return "[" + SimPe.Localization.Manager.GetString("unk") + ": 0x" + SimPe.Helper.HexString(op) + "]";
		}		


		/// <summary>
		/// Returns the List of known Primitives
		/// </summary>
		public static ArrayList StoredPrimitives { get { return gStr(0x8B); } }


		public static string PrimitiveName(ushort opcode)
		{
			if (StoredPrimitives != null && opcode < StoredPrimitives.Count)
				return (string)StoredPrimitives[opcode];
			else
				return "[" + SimPe.Localization.Manager.GetString("unk") + ": 0x" + SimPe.Helper.HexString(opcode) + "]";
		}

		/// <summary>
		/// Returns the the name of all Fileds in an Objd File
		/// </summary>
		/// <param name="type">Language ID -- ignored</param>
		public static ArrayList OBJDDescription(ushort type) { return gStr(0xCC); }


		/// <summary>
		/// Returns a list of all known Objf Lines
		/// </summary>
		public static ArrayList StoredObjfLines { get { return gStr(0xF5); } }
	
	}
}
