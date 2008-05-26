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
using System.Resources;
using System.Globalization;
using SimPe.Plugin;
using SimPe.Interfaces;
using SimPe.Interfaces.Files;
using SimPe.Interfaces.Plugin;

namespace pjse
{
	/// <summary>
	/// Summary description for FileTable.
	/// </summary>
	public class FileTable
	{
		public static FileTable GFT = null;
		static FileTable()
		{
			GFT = new FileTable();
			if (FileTableSettings.FTS.LoadAtStartup) GFT.Refresh();
		}

		public FileTable()
		{
			SimPe.FileTable.FileIndex.FILoad += new System.EventHandler(this.FileIndex_FILoad);
		}

        private void FileIndex_FILoad(object sender, System.EventArgs e) { if (hasLoaded || SimPe.Helper.LocalMode) UIRefresh(); }

        public void UIRefresh()
        {
            SimPe.Wait.Start();
            this.Refresh(true);
            SimPe.Wait.Stop();
        }

        private ArrayList fixedPackages = new ArrayList();
        private ArrayList maxisPackages = new ArrayList();
        protected static Hashtable filenames = new Hashtable();
        private Hashtable packedFiles = new Hashtable();
        private Hashtable pfByPackage = new Hashtable();
        private Hashtable pfByType = new Hashtable();
        private Hashtable pfByGroup = new Hashtable();
        private Hashtable pfByTypeGroup = new Hashtable();
        private Hashtable pfByTypeGroupInstance = new Hashtable();

        private bool hasLoaded = false;
        public void Refresh() { this.Refresh(false); }
        private void Refresh(bool flag)
        {
            if (!hasLoaded && !flag && SimPe.Helper.LocalMode) return;

            IPackageFile cp = currentPackage;
            CurrentPackage = null;

            hasLoaded = true;
            fixedPackages = new ArrayList();
            maxisPackages = new ArrayList();
            filenames = new Hashtable();
            packedFiles = new Hashtable();
            pfByPackage = new Hashtable();
            pfByType = new Hashtable();
            pfByGroup = new Hashtable();
            pfByTypeGroup = new Hashtable();
            pfByTypeGroupInstance = new Hashtable();

            foreach (SimPe.FileTableItem fii in SimPe.FileTable.DefaultFolders)
                if (fii.Use) Add(fii.Name, fii.IsRecursive, fii.Type.AsExpansions != SimPe.Expansions.Custom, true);

            this.Add(Path.Combine(SimPe.Helper.SimPePluginPath, "pjse.coder.plugin\\GlobalStrings.package"), false, false, true);

            this.Add(Path.Combine(SimPe.Helper.SimPePluginDataPath, "pjse.coder.plugin\\Includes"), true, false, true);

            string packages_txt = Path.Combine(SimPe.Helper.SimPePluginDataPath, "pjse.coder.plugin\\packages.txt");
            if (File.Exists(packages_txt))
            {
                System.IO.StreamReader sr = new StreamReader(packages_txt);
                for (string line = sr.ReadLine(); line != null; line = sr.ReadLine())
                    this.Add(line.TrimEnd('+'), line.EndsWith("+"), false, true);
                sr.Close();
            }

            CurrentPackage = cp;
        }


        /// <summary>
		/// Indicates the Refresh() was called
		/// </summary>
		public event EventHandler FiletableRefresh;
		public virtual void OnFiletableRefresh(object sender, EventArgs e)
		{
			if (FiletableRefresh != null) FiletableRefresh(sender, e);
        }




        private IPackageFile currentPackage = null;
        public IPackageFile CurrentPackage
        {
            get { return currentPackage == null ? null : IsFixed(currentPackage) ? null : currentPackage; }

            set
            {
                if (currentPackage != value)
                {
                    if (currentPackage != null)
                    {
                        currentPackage.IndexChanged -= new EventHandler(currentPackage_Changed);
                        if (hasLoaded && !IsMaxis(currentPackage) && !IsFixed(currentPackage))
                            Remove(currentPackage);
                    }
                    currentPackage = value;
                    if (currentPackage != null)
                    {
                        if (hasLoaded && !IsMaxis(currentPackage) && !IsFixed(currentPackage))
                            Add(currentPackage, false, false);
                        currentPackage.IndexChanged += new EventHandler(currentPackage_Changed);
                    }
                    if (hasLoaded)
                        OnFiletableRefresh(this, new EventArgs());
                }
            }
        }

        private void currentPackage_Changed(object sender, EventArgs e)
        {
            IPackageFile cp = currentPackage;
            CurrentPackage = null;
            CurrentPackage = cp;
        }

        private bool IsMaxis(IPackageFile package)
        {
            if (!hasLoaded) Refresh();
            return (package == null || maxisPackages.Contains(package));
        }

        private bool IsFixed(IPackageFile package)
        {
            if (!hasLoaded) Refresh();
            return (package == null || fixedPackages.Contains(package));
        }


        private void Add(string v, bool recurse, bool isMaxis, bool isFixed)
        {
            if (Directory.Exists(v))
            {
                foreach (string i in Directory.GetFiles(v, "*.package"))
                    Add(i, false, isMaxis, isFixed);

                if (recurse)
                    foreach (string i in Directory.GetDirectories(v))
                        Add(i, true, isMaxis, isFixed);
            }

            else if (!v.ToLowerInvariant().EndsWith(SimPe.Helper.PATH_SEP+"globalcatbin.bundle.package") && File.Exists(v))
                Add(SimPe.Packages.File.LoadFromFile(v), isMaxis, isFixed);
        }

        
        private void Add(IPackageFile package, bool isMaxis, bool isFixed)
		{
			if (package == null) return;
			if (pfByPackage[package] != null) return;

            foreach (IPackedFileDescriptor i in package.Index)
                Add(new Entry(package, i, isMaxis, isFixed));

            if (isMaxis)
                maxisPackages.Add(package);

            if (isFixed)
                fixedPackages.Add(package);
        }

        private void Add(Entry key)
        {
            object val = true;
            uint T = key.Type;
            uint G = key.Group;
            uint I = key.Instance;
            IPackageFile P = key.Package;

            Hashtable byPackage = (Hashtable)pfByPackage[P];
            if (byPackage == null) byPackage = (Hashtable)(pfByPackage[P] = new Hashtable());
            if (byPackage[key] != null)
                throw new Exception("byPackage[key] != null");
            byPackage[key] = true;

            if (packedFiles[key] != null)
                throw new Exception("packedFiles[key] != null");
            packedFiles[key] = val;

            if (key.PFD.MarkForDelete) return;

            Hashtable byType = (Hashtable)pfByType[T];
            if (byType == null) byType = (Hashtable)(pfByType[T] = new Hashtable());
            if (byType[key] != null)
                throw new Exception("byType[key] != null");
            byType[key] = val;

            Hashtable byGroup = (Hashtable)pfByGroup[G];
            if (byGroup == null) byGroup = (Hashtable)(pfByGroup[G] = new Hashtable());
            if (byGroup[key] != null)
                throw new Exception("byGroup[key] != null");
            byGroup[key] = val;

            Hashtable tgt = (Hashtable)pfByTypeGroup[T];
            if (tgt == null) tgt = (Hashtable)(pfByTypeGroup[T] = new Hashtable());
            Hashtable byTypeGroup = (Hashtable)((tgt[G] == null) ? (tgt[G] = new Hashtable()) : tgt[G]);
            if (byTypeGroup[key] != null)
                throw new Exception("byTypeGroup[key] != null");
            byTypeGroup[key] = val;

            Hashtable tgit = (Hashtable)pfByTypeGroupInstance[T];
            if (tgit == null) tgit = (Hashtable)(pfByTypeGroupInstance[T] = new Hashtable());
            Hashtable tgitg = (Hashtable)((tgit[G] == null) ? (tgit[G] = new Hashtable()) : tgit[G]);
            Hashtable byTypeGroupInstance = (Hashtable)((tgitg[I] == null) ? (tgitg[I] = new Hashtable()) : tgitg[I]);
            if (byTypeGroupInstance[key] != null)
                throw new Exception("byTypeGroupInstance[key] != null");
            byTypeGroupInstance[key] = val;

            key.PFD.DescriptionChanged += new EventHandler(PFD_DescriptionChanged);
        }

        void PFD_DescriptionChanged(object sender, EventArgs e)
        {
            IPackedFileDescriptor pfd = (IPackedFileDescriptor)sender;

            Entry key = null;
            foreach (object i in packedFiles.Keys)
                if (((Entry)i).PFD == pfd)
                {
                    key = (Entry)i;
                    break;
                }
            if (key == null)
            {
                pfd.DescriptionChanged -= new EventHandler(PFD_DescriptionChanged);
                return;
            }

            Remove(key);
            key = new Entry(key.Package, pfd, key.IsMaxis, key.IsFixed);
            Add(key);

            OnFiletableRefresh(this, new EventArgs());
        }

        private void Remove(IPackageFile package)
        {
            Hashtable byPackage = (Hashtable)pfByPackage[package];
            if (byPackage == null) return;

            Entry[] keys = new Entry[byPackage.Keys.Count];
            byPackage.Keys.CopyTo(keys, 0);
            try
            {
                foreach (object key in keys)
                    Remove((Entry)key);
            }
            catch (Exception e)
            {
                SimPe.Helper.ExceptionMessage(e);
                throw e;
            }

            pfByPackage.Remove(package);
        }

        private void Remove(Entry key)
        {
            uint T = key.Type;
            uint G = key.Group;
            uint I = key.Instance;
            IPackageFile P = key.Package;

            key.PFD.DescriptionChanged -= new EventHandler(PFD_DescriptionChanged);

            packedFiles.Remove(key);
            filenames.Remove(key);

            Hashtable byPackage = (Hashtable)pfByPackage[P];
            if (byPackage[key] != null) byPackage.Remove(key);

            Hashtable byType = (Hashtable)pfByType[T];
            if (byType != null) byType.Remove(key);

            Hashtable byGroup = (Hashtable)pfByGroup[G];
            if (byGroup != null) byGroup.Remove(key);

            Hashtable tgt = (Hashtable)pfByTypeGroup[T];
            if (tgt != null)
            {
                Hashtable byTypeGroup = (Hashtable)tgt[G];
                if (byTypeGroup != null) byTypeGroup.Remove(key);
            }

            Hashtable tgit = (Hashtable)pfByTypeGroupInstance[T];
            if (tgit != null)
            {
                Hashtable tgitg = (Hashtable)tgit[G];
                if (tgitg != null)
                {
                    Hashtable byTypeGroupInstance = (Hashtable)tgitg[I];
                    if (byTypeGroupInstance != null) byTypeGroupInstance.Remove(key);
                }
            }
        }


        public enum Source { Any, Maxis, Fixed, Local };
		public class Entry : IDisposable, IComparable
		{
			private IPackageFile package;
			private IPackedFileDescriptor pfd;
            private bool isMaxis;
            private bool isFixed;

            public Entry(IPackageFile package, IPackedFileDescriptor pfd, bool isMaxis, bool isFixed)
			{
				this.package = package;
				this.pfd = pfd;
                this.isMaxis = isMaxis;
                this.isFixed = isFixed;

                this.pfd.ChangedData += new SimPe.Events.PackedFileChanged(pfd_ChangedData);
			}

            void pfd_ChangedData(IPackedFileDescriptor sender)
            {
                if (FileTable.filenames[this] != null)
                    FileTable.filenames.Remove(this);
                FileTable.GFT.OnFiletableRefresh(GFT, new EventArgs());
            }

			public IPackageFile Package { get { return package; } }

			public IPackedFileDescriptor PFD { get { return pfd; } }

            public uint Type { get { return pfd.Type; } }

            public uint Group { get { return pfd.Group; } }

            public uint Instance { get { return pfd.Instance; } }

            public bool IsMaxis { get { return isMaxis; } }

            public bool IsFixed { get { return isFixed; } }

			public AbstractWrapper Wrapper
			{
				get
				{
					AbstractWrapper wrapper = (AbstractWrapper)SimPe.FileTable.WrapperRegistry.FindHandler(Type);
					if (wrapper != null)
						wrapper.ProcessData(pfd, package);
					return wrapper;
				}
			}

			public override string ToString() { return this + " (0x" + SimPe.Helper.HexString((ushort)Instance) + ")"; }

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

                this.pfd.ChangedData -= new SimPe.Events.PackedFileChanged(pfd_ChangedData);
                this.pfd = null;
            }

			#endregion

			#region IComparable Members

			public int CompareTo(object obj)
			{
				if (!(obj is Entry))
					return -1;
				Entry that = (Entry)obj;

				if (this.Type.CompareTo(that.Type) != 0)
					return this.Type.CompareTo(that.Type);
				if (this.Group.CompareTo(that.Group) != 0)
					return this.Group.CompareTo(that.Group);
				return this.Instance.CompareTo(that.Instance);
			}

			#endregion
		}

        public Entry[] this[IPackageFile package, IPackedFileDescriptor pfd]
        {
            get
            {
                if (package == null || pfd == null) return new Entry[0];
                return this[pfd.Type, pfd.Group, pfd.Instance,
                    pfd.Group == 0xffffffff ? Source.Local
                    //: IsMaxis(package) ? Source.Maxis
                    //: IsFixed(package) ? Source.Fixed
                    : Source.Any];
            }
        }

        public Entry[] this[IPackageFile package, uint packedFileType]
        {
            get
            {
                if (!hasLoaded) Refresh();
                if (package == null || pfByPackage[package] == null) return new Entry[0];

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

        public Entry[] this[uint packedFileType] { get { return this[packedFileType, Source.Any]; } }
        public Entry[] this[uint packedFileType, Source where]
        {
            get
            {
                if (!hasLoaded) Refresh();

                return putLocalFirst((Hashtable)pfByType[packedFileType], where);
            }
        }

        public Entry[] this[uint packedFileType, uint group]
        {
            get { return this[packedFileType, group, group == 0xffffffff ? Source.Local : Source.Any]; }
        }
        public Entry[] this[uint packedFileType, uint group, Source where]
        {
            get
            {
                if (!hasLoaded) Refresh();

                Hashtable tgt = (Hashtable)pfByTypeGroup[packedFileType];
                if (tgt == null) return new Entry[0];
                return putLocalFirst((Hashtable)tgt[group], where);
            }
        }

        public Entry[] this[uint packedFileType, uint group, uint instance]
        {
            get { return this[packedFileType, group, instance, Source.Any]; }
        }
        public Entry[] this[uint packedFileType, uint group, uint instance, Source where]
        {
            get
            {
                if (!hasLoaded) Refresh();

                Hashtable tgit = (Hashtable)pfByTypeGroupInstance[packedFileType];
                if (tgit == null) return new Entry[0];
                Hashtable tgitg = (Hashtable)tgit[group];
                if (tgitg == null) return new Entry[0];
                return putLocalFirst((Hashtable)tgitg[instance], group == 0xffffffff ? Source.Local : where);
            }
        }

        private Entry[] putLocalFirst(Hashtable result, Source where)
        {
            if (result == null) return new Entry[0];

            ArrayList currpkg = new ArrayList();
            ArrayList maxispkg = new ArrayList();
            ArrayList fixedpkg = new ArrayList();
            ArrayList nonfixed = new ArrayList();

            ArrayList[] resultset =
                where == Source.Local ? new ArrayList[] { currpkg }
                : where == Source.Maxis ? IsMaxis(currentPackage) ? new ArrayList[] { currpkg, maxispkg } : new ArrayList[] { maxispkg }
                : where == Source.Fixed ? IsFixed(currentPackage) ? new ArrayList[] { currpkg, fixedpkg } : new ArrayList[] { fixedpkg }
                : new ArrayList[] { currpkg, nonfixed, fixedpkg, maxispkg };

            foreach (Entry e in result.Keys)
                if (!e.PFD.MarkForDelete)
                    ((ArrayList)(e.Package == currentPackage ? currpkg : e.IsMaxis ? maxispkg : e.IsFixed ? fixedpkg : nonfixed)).Add(e);

            int i = 0;
            foreach (ArrayList al in resultset) i += al.Count;

            Entry[] es = new Entry[i];
            i = 0;
            foreach (ArrayList al in resultset) { al.CopyTo(es, i); i += al.Count; }

            return es;
        }
	}

    public class FileTableTool : ITool
    {
        #region ITool Members

        public bool IsEnabled(IPackedFileDescriptor pfd, IPackageFile package)
        {
#if DEBUG
            pjse.FileTable.GFT.CurrentPackage = package;
#else
            try
            {
                pjse.FileTable.GFT.CurrentPackage = package;
            }
            catch (Exception e)
            {
                SimPe.Helper.ExceptionMessage(e);
                throw e;
            }
#endif
            return true;
        }

        public IToolResult ShowDialog(ref IPackedFileDescriptor pfd, ref IPackageFile package)
        {
            SimPe.FileTable.Reload();
            return new SimPe.Plugin.ToolResult(false, false);
        }


        #region IToolPlugin Members

        public override string ToString()
        {
            return "PJSE\\" + pjse.Localization.GetString("ft_Refresh");
        }

        #endregion
        #endregion
    }

    public class FileTableSettings : SimPe.GlobalizedObject, ISettings
    {
        static ResourceManager rm = new ResourceManager(typeof(pjse.Localization));

        private static FileTableSettings fts;
        public static FileTableSettings FTS { get { return fts; } }
        static FileTableSettings() { fts = new FileTableSettings(); }

        const string BASENAME = "PJSE\\Bhav";
        SimPe.XmlRegistryKey xrk = SimPe.Helper.WindowsRegistry.PluginRegistryKey;
        public FileTableSettings() : base(rm) { }

        [System.ComponentModel.Category("FT")]
        public bool LoadAtStartup
        {
            get
            {
                SimPe.XmlRegistryKey rkf = SimPe.Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey(BASENAME);
                object o = rkf.GetValue("loadAtStartup", true);
                return Convert.ToBoolean(o);
            }

            set
            {
                SimPe.XmlRegistryKey rkf = SimPe.Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey(BASENAME);
                rkf.SetValue("loadAtStartup", value);
            }
        }

        #region ISettings Members

        public object GetSettingsObject() { return this; }

        public override string ToString() { return pjse.Localization.GetString("ft_Settings"); }

        [System.ComponentModel.Browsable(false)]
        public System.Drawing.Image Icon { get { return null; } }

        #endregion
    }

    public class FileTableWrapperFactory : AbstractWrapperFactory, IToolFactory, ISettingsFactory
	{
		#region IToolFactory Members

		public IToolPlugin[] KnownTools { get { return new IToolPlugin[] { new
            pjse.FileTableTool()
        }; } }

		#endregion

        #region ISettingsFactory Members

        public ISettings[] KnownSettings { get { return new ISettings[] {
            FileTableSettings.FTS
        }; } }

        #endregion
    }

}
