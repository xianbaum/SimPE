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
using SimPe.Plugin;
using SimPe.Interfaces;
using SimPe.Interfaces.Files;
using SimPe.Interfaces.Plugin;

namespace pjse
{
	/// <summary>
	/// Summary description for FileTable.
	/// </summary>
	public class FileTable : ITool
	{
		public static FileTable GFT = null;
		static FileTable()
		{
			GFT = new FileTable();
			if (static_getter_LoadAtStartup()) GFT.Refresh();
		}


		public FileTable()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		private Hashtable packedFiles = new Hashtable();
		private Hashtable pfByPackage = new Hashtable();
		private Hashtable pfByType = new Hashtable();
		private Hashtable pfByGroup = new Hashtable();
		private Hashtable pfByTypeGroup = new Hashtable();
		private Hashtable pfByTypeGroupInstance = new Hashtable();
		private bool hasLoaded = false;

		private IPackageFile currentPackage = null;


		private static bool static_getter_LoadAtStartup()
		{
			SimPe.XmlRegistryKey  rkf = SimPe.Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey("PJSE\\Bhav");
			object o = rkf.GetValue("loadAtStartup", false);
			return Convert.ToBoolean(o);
		}

		public void Refresh()
		{
			hasLoaded = true;
			packedFiles = new Hashtable();
			pfByPackage = new Hashtable();
			pfByType = new Hashtable();
			pfByGroup = new Hashtable();
			pfByTypeGroup = new Hashtable();
			pfByTypeGroupInstance = new Hashtable();


			ArrayList folders = SimPe.FileTable.DefaultFolders;

			string[] paths = {
								 SimPe.Helper.WindowsRegistry.SimsSP1Path,
								 SimPe.Helper.WindowsRegistry.SimsEP3Path,
								 SimPe.Helper.WindowsRegistry.SimsEP2Path,
								 SimPe.Helper.WindowsRegistry.SimsEP1Path,
								 SimPe.Helper.WindowsRegistry.SimsPath
							 };

			foreach(string path in paths)
			{
				if (path.Trim().Length.Equals(0)) continue;
				string o = Path.Combine(path, "TSData\\Res\\Objects");
				bool found = false;
				int i = -1;
				while(!found && ++i < folders.Count)
					found = ((SimPe.FileTableItem)folders[i]).Name.ToLower().Trim().Equals(o.ToLower().Trim());
				if (found && !((SimPe.FileTableItem)folders[i]).Ignore)
				{
					this.AddFixed(o + "\\objects.package");
					break;
				}
			}
			this.AddFixed(Path.Combine(SimPe.Helper.SimPePluginPath, "pjse.coder.plugin\\GlobalStrings.package"));

			string packages_txt = Path.Combine(SimPe.Helper.SimPePluginDataPath, "pjse.coder.plugin\\packages.txt");
			if (File.Exists(packages_txt))
			{
				System.IO.StreamReader sr = new StreamReader(packages_txt);
				for (string line = sr.ReadLine(); line != null; line = sr.ReadLine())
					this.AddFixed(line);
				sr.Close();
			}

			RefreshCurrentPackage();

			OnFiletableRefresh(this, new EventArgs());
		}

		/// <summary>
		/// Indicates the Refresh() was called
		/// </summary>
		public event EventHandler FiletableRefresh;
		internal virtual void OnFiletableRefresh(object sender, EventArgs e)
		{
			if (FiletableRefresh != null) 
			{
				FiletableRefresh(sender, e);
			}
		}



		public Entry[] this[IPackageFile package, uint packedFileType]
		{
			get
			{
				if (!hasLoaded) Refresh();

				ArrayList result = new ArrayList();
				foreach (Entry e in ((Hashtable)pfByPackage[package]).Keys)
				{
					if (!e.PFD.MarkForDelete && e.PFD.Type == packedFileType)
						result.Add(e);
				}
				Entry[] es = new Entry[result.Count];
				result.CopyTo(es);
				return es;
			}
		}

		public Entry[] this[uint packedFileType]
		{
			get
			{
				if (!hasLoaded) Refresh();

				return putLocalFirst((Hashtable)pfByType[packedFileType], false);
			}
		}

		public Entry[] this[uint packedFileType, uint group]
		{
			get
			{
				if (!hasLoaded) Refresh();

				Hashtable tgt = (Hashtable)pfByTypeGroup[packedFileType];
				if (tgt == null) return new Entry[0];
				return putLocalFirst((Hashtable)tgt[group], group == 0xffffffff);
			}
		}

		public Entry[] this[uint packedFileType, uint group, uint instance]
		{
			get
			{
				if (!hasLoaded) Refresh();

				Hashtable tgit = (Hashtable)pfByTypeGroupInstance[packedFileType];
				if (tgit == null) return new Entry[0];
				Hashtable tgitg = (Hashtable)tgit[group];
				if (tgitg == null) return new Entry[0];
				return putLocalFirst((Hashtable)tgitg[instance], group == 0xffffffff);
			}
		}


		public void Add(string packageFile)
		{
			if (!File.Exists(packageFile)) return;
			Add(SimPe.Packages.File.LoadFromFile(packageFile));
		}

		public void Add(IPackageFile package)
		{
			Add(package, false);
		}

		public void AddFixed(string v)
		{
			if (System.IO.Directory.Exists(v))
			{
				string[] va = System.IO.Directory.GetFiles(v, "*.package");
				foreach(string i in va)
					AddFixed(i);
			}
			else if (File.Exists(v))
				AddFixed(SimPe.Packages.File.LoadFromFile(v));
		}

		public void AddFixed(IPackageFile package)
		{
			Add(package, true);
		}


		public void Remove(IPackageFile package)
		{
			Hashtable byPackage = (Hashtable)pfByPackage[package];
			if (byPackage == null) return;
			pfByPackage.Remove(package);

			foreach(object key in byPackage.Keys)
			{
				uint type = ((Entry)key).Type;
				uint group = ((Entry)key).Group;
				uint instance = ((Entry)key).Instance;

				packedFiles.Remove(key);
				filenames.Remove(key);

				Hashtable byType = (Hashtable)pfByType[type];
				if (byType != null) byType.Remove(key);

				Hashtable byGroup = (Hashtable)pfByGroup[group];
				if (byGroup != null) byGroup.Remove(key);

				Hashtable tgt = (Hashtable)pfByTypeGroup[type];
				if (tgt != null)
				{
					Hashtable byTypeGroup = (Hashtable)tgt[group];
					if (byTypeGroup != null) byTypeGroup.Remove(key);
				}

				Hashtable tgit = (Hashtable)pfByTypeGroupInstance[type];
				if (tgit != null)
				{
					Hashtable tgitg = (Hashtable)tgit[group];
					if (tgitg != null)
					{
						Hashtable byTypeGroupInstance = (Hashtable)tgitg[instance];
						if (byTypeGroupInstance != null) byTypeGroupInstance.Remove(key);
					}
				}
			}
		}


		public IPackageFile CurrentPackage
		{
			get { return currentPackage; }

			set
			{
				if (currentPackage != null && !IsFixed(currentPackage))
					Remove(currentPackage);
				if (currentPackage != value)
					currentPackage = IsFixed(value) ? null : value;
				if (currentPackage != null && !IsFixed(currentPackage))
					Add(currentPackage);
			}
		}

		public bool CurrentPackageIsFixed { get { return IsFixed(currentPackage); } }

		private bool IsFixed(IPackageFile package)
		{
			if (package == null || fixedPackages.Contains(package)) return true;

			// There doesn't appear to be a way to compare two paths and have the OS decide if they refer to the same object

			return false;
		}

		private Entry[] putLocalFirst(Hashtable result, bool localOnly)
		{
			if (result == null) return new Entry[0];

			ArrayList local = new ArrayList();
			ArrayList nonlocal = new ArrayList();
			foreach (Entry e in result.Keys)
				if (!e.PFD.MarkForDelete) ((ArrayList)(e.Package == currentPackage ? local : nonlocal)).Add(e);

			Entry[] es = new Entry[local.Count + (localOnly ? 0 : nonlocal.Count)];
			local.CopyTo(es, 0);
			if (!localOnly) nonlocal.CopyTo(es, local.Count);

			return es;
		}

		private void Add(IPackageFile package, bool isFixed)
		{
			if (package == null) return;
			if (pfByPackage[package] != null) return;

			Hashtable byPackage = (Hashtable)(pfByPackage[package] = new Hashtable());

			foreach (IPackedFileDescriptor i in package.Index)
			{
				object val = true;
				object key = new Entry(package, i);

				if (packedFiles[key] != null)
					throw new Exception("How did that get there?");
				packedFiles[key] = val;

				if (byPackage[key] != null)
					throw new Exception("How did that get there?");
				byPackage[key] = val;

				Hashtable byType = (Hashtable)pfByType[i.Type];
				if (byType == null) byType = (Hashtable)(pfByType[i.Type] = new Hashtable());
				if (byType[key] != null)
					throw new Exception("How did that get there?");
				byType[key] = val;

				Hashtable byGroup = (Hashtable)pfByGroup[i.Group];
				if (byGroup == null) byGroup = (Hashtable)(pfByGroup[i.Group] = new Hashtable());
				if (byGroup[key] != null)
					throw new Exception("How did that get there?");
				byGroup[key] = val;

				Hashtable tgt = (Hashtable)pfByTypeGroup[i.Type];
				if (tgt == null) tgt = (Hashtable)(pfByTypeGroup[i.Type] = new Hashtable());
				Hashtable byTypeGroup = (Hashtable)((tgt[i.Group] == null) ? (tgt[i.Group] = new Hashtable()) : tgt[i.Group]);
				if (byTypeGroup[key] != null)
					throw new Exception("How did that get there?");
				byTypeGroup[key] = val;

				Hashtable tgit = (Hashtable)pfByTypeGroupInstance[i.Type];
				if (tgit == null) tgit = (Hashtable)(pfByTypeGroupInstance[i.Type] = new Hashtable());
				Hashtable tgitg = (Hashtable)((tgit[i.Group] == null) ? (tgit[i.Group] = new Hashtable()) : tgit[i.Group]);
				Hashtable byTypeGroupInstance = (Hashtable)((tgitg[i.Instance] == null) ? (tgitg[i.Instance] = new Hashtable()) : tgitg[i.Instance]);
				if (byTypeGroupInstance[key] != null)
					throw new Exception("How did that get there?");
				byTypeGroupInstance[key] = val;
			}
			if (isFixed)
				fixedPackages.Add(package);
		}

		private void RefreshCurrentPackage()
		{
			if (currentPackage != null && !fixedPackages.Contains(currentPackage))
			{
				Remove(currentPackage);
				Add(currentPackage);
			}
		}


		private ArrayList fixedPackages = new ArrayList();

		protected static Hashtable filenames = new Hashtable();
		public class Entry : IDisposable, IComparable
		{
			private IPackageFile package;
			private IPackedFileDescriptor pfd;
			private uint type;
			private uint group;
			private uint instance;

			public Entry(IPackageFile package, IPackedFileDescriptor pfd)
			{
				this.package = package;
				this.pfd = pfd;
				this.type = pfd.Type;
				this.group = pfd.Group;
				this.instance = pfd.Instance;
			}

			public IPackageFile Package { get { return package; } }

			public IPackedFileDescriptor PFD { get { return pfd; } }

			public uint Type { get { return type; } }

			public uint Group { get { return group; } }

			public uint Instance { get { return instance; } }

			public AbstractWrapper Wrapper
			{
				get
				{
					AbstractWrapper wrapper = (AbstractWrapper)SimPe.FileTable.WrapperRegistry.FindHandler(type);
					if (wrapper != null)
						wrapper.ProcessData(pfd, package);
					return wrapper;
				}
			}

			public override string ToString() { return this + " (0x" + SimPe.Helper.HexString((ushort)instance) + ")"; }

			public static implicit operator string(Entry e)
			{
				if (FileTable.filenames[e] == null)
				{
					AbstractWrapper wrapper = e.Wrapper;
					if (wrapper != null)
						FileTable.filenames[e] = SimPe.Helper.ToString(wrapper.StoredData.ReadBytes(64)).Trim();
				}

				return (string)FileTable.filenames[e];
			}


			#region IDisposable Members

			public void Dispose()
			{
				this.package = null;
				this.pfd = null;
			}

			#endregion

			#region IComparable Members

			public int CompareTo(object obj)
			{
				if (!(obj is Entry))
					return -1;
				Entry that = (Entry)obj;

				if (this.type.CompareTo(that.type) != 0)
					return this.type.CompareTo(that.type);
				if (this.group.CompareTo(that.group) != 0)
					return this.group.CompareTo(that.group);
				return this.instance.CompareTo(that.instance);
			}

			#endregion
		}


		#region ITool Members

		public bool IsEnabled(IPackedFileDescriptor pfd, IPackageFile package)
		{
			GFT.CurrentPackage = package;
			return true;
		}

		public IToolResult ShowDialog(ref IPackedFileDescriptor pfd, ref IPackageFile package)
		{
			(new FileTableForm()).Settings();
			return new SimPe.Plugin.ToolResult(false, false);
		}


		#region IToolPlugin Members

		public override string ToString()
		{
			return "PJSE\\Filetable &Settings";
		}

		#endregion
		#endregion
	}


	public class FileTableWrapperFactory : AbstractWrapperFactory, IToolFactory
	{
		#region IToolFactory Members

		public SimPe.Interfaces.IToolPlugin[] KnownTools
		{
			get
			{
				IToolPlugin[] tools = {
										  new pjse.FileTable()
									  };
				return tools;
			}
		}

		#endregion
	}

}
