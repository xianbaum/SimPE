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
using SimPe.Interfaces.Files;

namespace pjse
{
	/// <summary>
	/// Summary description for FileTable.
	/// </summary>
	public class FileTable
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

				string packedFile = Path.Combine(package.FileName, i.Filename);
				packedFiles[packedFile] = e;

				byPackage[packedFile] = e;

				Hashtable byType = (Hashtable)((pfByType[i.Type] == null) ? (pfByType[i.Type] = new Hashtable()) : pfByType[i.Type]);
				byType[packedFile] = e;

				Hashtable byGroup = (Hashtable)((pfByGroup[i.Group] == null) ? (pfByGroup[i.Group] = new Hashtable()) : pfByGroup[i.Group]);
				byGroup[packedFile] = e;

				Hashtable tgt = (Hashtable)((pfByTypeGroup[i.Type] == null) ? (pfByTypeGroup[i.Type] = new Hashtable()) : pfByTypeGroup[i.Type]);
				Hashtable byTypeGroup = (Hashtable)((tgt[i.Group] == null) ? (tgt[i.Group] = new Hashtable()) : tgt[i.Group]);
				byTypeGroup[packedFile] = e;

				Hashtable tgit = (Hashtable)((pfByTypeGroupInstance[i.Type] == null) ? (pfByTypeGroupInstance[i.Type] = new Hashtable()) : pfByTypeGroupInstance[i.Type]);
				Hashtable tgitg = (Hashtable)((tgit[i.Group] == null) ? (tgit[i.Group] = new Hashtable()) : tgit[i.Group]);
				Hashtable byTypeGroupInstance = (Hashtable)((tgitg[i.Instance] == null) ? (tgitg[i.Instance] = new Hashtable()) : tgitg[i.Instance]);
				byTypeGroupInstance[packedFile] = e;
			}
			if (isFixed)
				fixedPackages.Add(package);
		}

		public void Remove(IPackageFile package)
		{
			Hashtable byPackage = (Hashtable)pfByPackage[package];
			if (byPackage == null) return;

			foreach (IPackedFileDescriptor i in package.Index)
			{
				string packedFile = Path.Combine(package.FileName, i.Filename);

				packedFiles[packedFile] = null;
				byPackage[packedFile] = null;

				Hashtable byType = (Hashtable)pfByType[i.Type];
				if (byType != null) byType.Remove(packedFile);

				Hashtable byGroup = (Hashtable)pfByGroup[i.Group];
				if (byGroup != null) byGroup.Remove(packedFile);

				Hashtable tgt = (Hashtable)pfByTypeGroup[i.Type];
				if (tgt != null)
				{
					Hashtable byTypeGroup = (Hashtable)tgt[i.Group];
					if (byTypeGroup != null) byTypeGroup.Remove(packedFile);
				}

				Hashtable tgit = (Hashtable)pfByTypeGroupInstance[i.Type];
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

			pfByPackage.Remove(package);
		}


		private IPackageFile currentPackage = null;
		public IPackageFile CurrentPackage
		{
			get
			{
				return currentPackage;
			}
			set
			{
				if (currentPackage != value)
				{
					if (currentPackage != null && !fixedPackages.Contains(currentPackage)) Remove(currentPackage);
					currentPackage = fixedPackages.Contains(value) ? null : value;
					if (currentPackage != null) Add(value);
				}
			}
		}


		private static ArrayList fixedPackages = new ArrayList();
		public static FileTable GFT = staticInitialiser();
		private static FileTable staticInitialiser()
		{
			FileTable filetable = new FileTable();
			if (SimPe.Helper.WindowsRegistry.SimsEP2Path.Length > 0)
				filetable.AddFixed(Path.Combine(SimPe.Helper.WindowsRegistry.SimsEP2Path, "TSData\\Res\\Objects\\objects.package"));
			else if (SimPe.Helper.WindowsRegistry.SimsEP1Path.Length > 0)
				filetable.AddFixed(Path.Combine(SimPe.Helper.WindowsRegistry.SimsEP1Path, "TSData\\Res\\Objects\\objects.package"));
			else if (SimPe.Helper.WindowsRegistry.SimsPath.Length > 0)
				filetable.AddFixed(Path.Combine(SimPe.Helper.WindowsRegistry.SimsPath, "TSData\\Res\\Objects\\objects.package"));
			filetable.AddFixed(Path.Combine(SimPe.Helper.SimPePluginPath, "pjse.coder.plugin\\GlobalStrings.package"));

			return filetable;
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

		public class BhavEntry : Entry
		{
			private static Hashtable bhavFilenames = new Hashtable();

			public BhavEntry(IPackageFile package, IPackedFileDescriptor pfd) : base(package, pfd) { }


			public BhavEntry(Entry e) : base(e.Package, e.PFD) { }


			public string Filename
			{
				get
				{
					if (bhavFilenames[this.PFD.Filename] == null)
					{
						SimPe.PackedFiles.Wrapper.Bhav b = new SimPe.PackedFiles.Wrapper.Bhav(null);
						b.ProcessData(this.PFD, this.Package);
						bhavFilenames[this.PFD.Filename] = b.FileName;
					}
					return (string)bhavFilenames[this.PFD.Filename];
				}
			}

		}
	}

}
