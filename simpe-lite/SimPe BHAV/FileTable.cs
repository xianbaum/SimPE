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
using System.IO;
using System.Collections;
using SimPe.Interfaces;
using SimPe.Interfaces.Files;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Scenegraph;

namespace pjse
{
	/// <summary>
	/// Summary description for FileTable.
	/// </summary>
	public class FileTable : ITool
	{
		public FileTable()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		public Entry[] this[uint packedFileType]
		{
			get
			{
				return putLocalFirst((Hashtable)pfByType[packedFileType], false);
			}
		}

		public Entry[] this[uint packedFileType, uint group]
		{
			get
			{
				Hashtable tgt = (Hashtable)pfByTypeGroup[packedFileType];
				if (tgt == null) return new Entry[0];
				return putLocalFirst((Hashtable)tgt[group], group == 0xffffffff);
			}
		}

		public Entry[] this[uint packedFileType, uint group, uint instance]
		{
			get
			{
				Hashtable tgit = (Hashtable)pfByTypeGroupInstance[packedFileType];
				if (tgit == null) return new Entry[0];
				Hashtable tgitg = (Hashtable)tgit[group];
				if (tgitg == null) return new Entry[0];
				return putLocalFirst((Hashtable)tgitg[instance], group == 0xffffffff);
			}
		}


		private Entry[] putLocalFirst(Hashtable result, bool localOnly)
		{
			if (result == null) return new Entry[0];

			ArrayList local = new ArrayList();
			ArrayList nonlocal = new ArrayList();
			foreach (Entry e in result.Values)
				((ArrayList)(e.Package == currentPackage ? local : nonlocal)).Add(e);

			Entry[] es = new Entry[local.Count + (localOnly ? 0 : nonlocal.Count)];
			local.CopyTo(es, 0);
			if (!localOnly) nonlocal.CopyTo(es, local.Count);

			return es;
		}


		private Hashtable packedFiles = new Hashtable();
		private Hashtable pfByPackage = new Hashtable();
		private Hashtable pfByType = new Hashtable();
		private Hashtable pfByGroup = new Hashtable();
		private Hashtable pfByTypeGroup = new Hashtable();
		private Hashtable pfByTypeGroupInstance = new Hashtable();
		public void Add(string packageFile)
		{
			if (!File.Exists(packageFile)) return;
			Add(SimPe.Packages.File.LoadFromFile(packageFile));
		}

		public void Add(IPackageFile package)
		{
			Add(package, false);
		}

		public void AddFixed(string packageFile)
		{
			if (!File.Exists(packageFile)) return;
			AddFixed(SimPe.Packages.File.LoadFromFile(packageFile));
		}

		public void AddFixed(IPackageFile package)
		{
			Add(package, true);
		}

		private void Add(IPackageFile package, bool isFixed)
		{
			if (package == null) return;
			if (pfByPackage[package] != null) return;

			Hashtable byPackage = (Hashtable)(pfByPackage[package] = new Hashtable());

			foreach (IPackedFileDescriptor i in package.Index)
			{
				Entry e = new Entry(package, i);

				string packedFile = Path.Combine(package.FileName, i.ExportFileName);
				if (packedFiles[packedFile] != null)
					throw new Exception("How did that get there?");
				packedFiles[packedFile] = e;

				if (byPackage[packedFile] != null)
					throw new Exception("How did that get there?");
				byPackage[packedFile] = e;

				Hashtable byType = (Hashtable)pfByType[i.Type];
				if (byType == null) byType = (Hashtable)(pfByType[i.Type] = new Hashtable());
				if (byType[packedFile] != null)
					throw new Exception("How did that get there?");
				byType[packedFile] = e;

				Hashtable byGroup = (Hashtable)pfByGroup[i.Group];
				if (byGroup == null) byGroup = (Hashtable)(pfByGroup[i.Group] = new Hashtable());
				if (byGroup[packedFile] != null)
					throw new Exception("How did that get there?");
				byGroup[packedFile] = e;

				Hashtable tgt = (Hashtable)pfByTypeGroup[i.Type];
				if (tgt == null) tgt = (Hashtable)(pfByTypeGroup[i.Type] = new Hashtable());
				Hashtable byTypeGroup = (Hashtable)((tgt[i.Group] == null) ? (tgt[i.Group] = new Hashtable()) : tgt[i.Group]);
				if (byTypeGroup[packedFile] != null)
					throw new Exception("How did that get there?");
				byTypeGroup[packedFile] = e;

				Hashtable tgit = (Hashtable)pfByTypeGroupInstance[i.Type];
				if (tgit == null) tgit = (Hashtable)(pfByTypeGroupInstance[i.Type] = new Hashtable());
				Hashtable tgitg = (Hashtable)((tgit[i.Group] == null) ? (tgit[i.Group] = new Hashtable()) : tgit[i.Group]);
				Hashtable byTypeGroupInstance = (Hashtable)((tgitg[i.Instance] == null) ? (tgitg[i.Instance] = new Hashtable()) : tgitg[i.Instance]);
				if (byTypeGroupInstance[packedFile] != null)
					throw new Exception("How did that get there?");
				byTypeGroupInstance[packedFile] = e;
			}
			if (isFixed)
				fixedPackages.Add(package);
		}

		public void Remove(IPackageFile package)
		{
			Hashtable byPackage = (Hashtable)GFT.pfByPackage[package];
			if (byPackage == null) return;

			foreach (IPackedFileDescriptor i in package.Index)
			{
				string packedFile = Path.Combine(package.FileName, i.ExportFileName);

				GFT.packedFiles.Remove(packedFile);
				byPackage.Remove(packedFile);

				Hashtable byType = (Hashtable)GFT.pfByType[i.Type];
				if (byType != null) byType.Remove(packedFile);

				Hashtable byGroup = (Hashtable)GFT.pfByGroup[i.Group];
				if (byGroup != null) byGroup.Remove(packedFile);

				Hashtable tgt = (Hashtable)GFT.pfByTypeGroup[i.Type];
				if (tgt != null)
				{
					Hashtable byTypeGroup = (Hashtable)tgt[i.Group];
					if (byTypeGroup != null) byTypeGroup.Remove(packedFile);
				}

				Hashtable tgit = (Hashtable)GFT.pfByTypeGroupInstance[i.Type];
				if (tgit != null)
				{
					Hashtable tgitg = (Hashtable)tgit[i.Group];
					if (tgitg != null)
					{
						Hashtable byTypeGroupInstance = (Hashtable)tgitg[i.Instance];
						if (byTypeGroupInstance != null) byTypeGroupInstance.Remove(packedFile);
					}
				}
			}

			GFT.pfByPackage.Remove(package);
		}


		private IPackageFile currentPackage = null;
		public IPackageFile CurrentPackage
		{
			get { return currentPackage; }

			set
			{
				if (currentPackage != value)
				{
					if (currentPackage != null && !fixedPackages.Contains(currentPackage))
						Remove(currentPackage);
					currentPackage = fixedPackages.Contains(value) ? null : value;
					if (currentPackage != null) Add(value);
				}
			}
		}


		private ArrayList fixedPackages = new ArrayList();
		public static FileTable GFT = null;
		static FileTable()
		{
			GFT = new FileTable();
			if (SimPe.Helper.WindowsRegistry.SimsEP2Path.Length > 0)
				GFT.AddFixed(Path.Combine(SimPe.Helper.WindowsRegistry.SimsEP2Path, "TSData\\Res\\Objects\\objects.package"));
			else if (SimPe.Helper.WindowsRegistry.SimsEP1Path.Length > 0)
				GFT.AddFixed(Path.Combine(SimPe.Helper.WindowsRegistry.SimsEP1Path, "TSData\\Res\\Objects\\objects.package"));
			else if (SimPe.Helper.WindowsRegistry.SimsPath.Length > 0)
				GFT.AddFixed(Path.Combine(SimPe.Helper.WindowsRegistry.SimsPath, "TSData\\Res\\Objects\\objects.package"));
			GFT.AddFixed(Path.Combine(SimPe.Helper.SimPePluginPath, "pjse.coder.plugin\\GlobalStrings.package"));
		}


		public class Entry : IDisposable
		{
			private IPackageFile package;
			private IPackedFileDescriptor pfd;

			public Entry(IPackageFile package, IPackedFileDescriptor pfd)
			{
				this.package = package;
				this.pfd = pfd;
			}

			public IPackageFile Package { get { return package; } }

			public IPackedFileDescriptor PFD { get { return pfd; } }


			#region IDisposable Members

			public void Dispose()
			{
				this.package = null;
				this.pfd = null;
			}

			#endregion
		}


		#region ITool Members

		public bool IsEnabled(IPackedFileDescriptor pfd, IPackageFile package)
		{
			GFT.CurrentPackage = package;
			return false;
		}

		public IToolResult ShowDialog(ref IPackedFileDescriptor pfd, ref IPackageFile package)
		{
			return new SimPe.Plugin.ToolResult(false, false);
		}

		#endregion

		#region IToolPlugin Members

		public override string ToString()
		{
			// TODO:  Add FileTable.ToString implementation
			return "";
		}

		#endregion
	}

}
