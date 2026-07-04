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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Ambertation.Windows.Forms;
using System.Media;

namespace SimPe.Plugin.Tool.Dockable
{
    /// <summary>
    /// Summary description for DockableWindow1.
    /// </summary>
    public partial class FinderDock : Ambertation.Windows.Forms.DockPanel, SimPe.Interfaces.IDockableTool, SimPe.Interfaces.IFinderResultGui
    {
        booby.ThemeManager tm;
        SoundPlayer Aah = new SoundPlayer(booby.NoisyGirls.Aah);
        SoundPlayer DoMe = new SoundPlayer(booby.NoisyGirls.DoMeBabe);

        System.Collections.Generic.List<string> packages;
        System.Threading.Thread[] threads;
        private Panel pnContainer;
        SimPe.Interfaces.AFinderTool searchtool;
        int runningthreads;
        private Hashtable[] groupTables;
        int groupColumn = 0;

        public FinderDock()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            tm = booby.ThemeManager.Global.CreateChild();
            tm.AddControl(this.GradientPanel1);
            tm.AddControl(this.tbResult);
            tm.AddControl(this.toolBar1);
            if (Helper.WindowsRegistry.UseBigIcons)
                toolBar1.ImageScalingSize = new System.Drawing.Size(32, 32);
            lvreal.View = System.Windows.Forms.View.Details;
            packages = new System.Collections.Generic.List<string>();
            threads = new System.Threading.Thread[Helper.WindowsRegistry.SortProcessCount / 2];
            runningthreads = 0;
            CreateThreads(false);
            foreach (SimPe.Interfaces.AFinderTool tl in Finder.FinderToolRegistry.Global.CreateToolInstances(this))
            {
                this.cbTask.Items.Add(tl);
            }
            if (cbTask.Items.Count > 0) this.cbTask.SelectedIndex = 0;
        }

        private void CreateThreads(bool start)
        {
            for (int ct = 0; ct < threads.Length; ct++)
            {
                threads[ct] = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadRunner));
                threads[ct].Name = "Search Thread " + (ct);
                if (start) threads[ct].Start();
            }
        }

        public Ambertation.Windows.Forms.DockPanel GetDockableControl() { return this; }
        public event SimPe.Events.ChangedResourceEvent ShowNewResource;
        public void RefreshDock(object sender, SimPe.Events.ResourceEventArgs es) {	}

        #region IToolPlugin Member
        public override string ToString() { return this.Text; }
        #endregion

        private void cbTask_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            pnContainer.Controls.Clear();
            if (cbTask.SelectedItem == null) return;

            Control c = ((SimPe.Interfaces.AFinderTool)cbTask.SelectedItem).SearchGui;
            pnContainer.Height = c.Height;
            pnContainer.Controls.Add(c);
            c.Parent = pnContainer;
            c.Left = 0;
            c.Top = 0;
            c.Dock = DockStyle.Top;
            c.Visible = true;
            c.Refresh();
        }

        private void Activate_biList(object sender, System.EventArgs e)
        {
            lvreal.View = System.Windows.Forms.View.List;
            biList.Checked = true;
            biTile.Checked = false;
            biDetail.Checked = false;
        }

        private void Activate_biTile(object sender, System.EventArgs e)
        {
            lvreal.View = System.Windows.Forms.View.Tile;
            biList.Checked = false;
            biTile.Checked = true;
            biDetail.Checked = false;
        }

        private void Activate_biDetails(object sender, System.EventArgs e)
        {
            lvreal.View = System.Windows.Forms.View.Details;
            biList.Checked = false;
            biTile.Checked = false;
            biDetail.Checked = true;
        }

        private void lvreal_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Set the sort order to ascending when changing
            // column groups; otherwise, reverse the sort order.
            if (lvreal.Sorting == SortOrder.Descending)
            {
                lvreal.Sorting = SortOrder.Ascending;
            }
            else
            {
                lvreal.Sorting = SortOrder.Descending;
            }
            groupColumn = e.Column;
            SetGroups(e.Column);
        }

        private void lvreal_DoubleClick(object sender, System.EventArgs e)
        {
            if (lvreal.SelectedItems.Count != 1) return;
            IFinderResultItem fri = (IFinderResultItem)lvreal.SelectedItems[0];
            fri.OpenResource();
        }

        #region IToolExt Member
        public System.Windows.Forms.Shortcut Shortcut { get { return System.Windows.Forms.Shortcut.None; } }
        public System.Drawing.Image Icon { get { return this.TabImage; } }
        public new bool Visible { get { return this.IsDocked || this.IsFloating; } }
        #endregion

        #region IFinderResultGui Members
        bool truncated;
        bool forcestop;

        public bool ForcedStop
        {
            get { return forcestop; }
            set { if (value) StopSearch(); }
        }

        public void AddResult(SimPe.Interfaces.Files.IPackageFile pkg, SimPe.Interfaces.Files.IPackedFileDescriptor pfd)
        {
            AddResult(null, pkg, pfd);
        }

        delegate void InvokeAddResult(string group, SimPe.Interfaces.Files.IPackageFile pkg, SimPe.Interfaces.Files.IPackedFileDescriptor pfd);
        public void AddResult(string group, SimPe.Interfaces.Files.IPackageFile pkg, SimPe.Interfaces.Files.IPackedFileDescriptor pfd)
        {
            if (this.InvokeRequired) this.BeginInvoke(new InvokeAddResult(InvokedAddResult), new object[] {group, pkg, pfd });
            else InvokedAddResult(group, pkg, pfd);
        }

        protected void InvokedAddResult(string group, SimPe.Interfaces.Files.IPackageFile pkg, SimPe.Interfaces.Files.IPackedFileDescriptor pfd)
        {
            if (lvreal.Items.Count > Helper.WindowsRegistry.MaxSearchResults) { truncated = true; return; }
            ScenegraphResultItem sri = new ScenegraphResultItem(pkg, pfd);
            lvreal.Items.Add(sri);
        }

        protected void SetPackageList()
        {
            FileTable.FileIndex.Load();
            packages.Clear();
            truncated = false;
            pnErr.Visible = false;

            foreach (FileTableItem fti in SimPe.FileTable.FileIndex.BaseFolders)
            {
                if (fti.Use)
                {
                    string name = fti.Name;
                    if (fti.IsFile) AddToPackageList(name);
                    else
                    {
                        string[] files = System.IO.Directory.GetFiles(name, "*.package");
                        foreach (string s in files)
                            AddToPackageList(s);
                    }
                }
            }
        }

        void AddToPackageList(string fl)
        {
            if (!packages.Contains(Helper.CompareableFileName(fl)))
                packages.Add(Helper.CompareableFileName(fl));
        }

        public void StartSearch(SimPe.Interfaces.AFinderTool sender)
        {
            StopSearch();
            lock (packages)
            {
                SetPackageList();
                Wait.ShowAnimation = true;
                Wait.Start(packages.Count + 1);
                Wait.Image = this.TabImage;
                searchtool = sender;
                forcestop = false;
                lvreal.Items.Clear();
                lvreal.BeginUpdate();
            }

            if (sender.ProcessParalell)
            {
                CreateThreads(true);
            }
            else
            {
                CreateThreads(false);
                threads[0].Start();
            }
        }

        public bool Searching { get { return runningthreads > 0; } }

        public void StopSearch()
        {
            lock (packages)
            {
                packages.Clear();
                forcestop = true;
            }
            
            bool stopped =  !Searching;
            while (!stopped)
            {
                Wait.Message = "Stopping Search...";
                System.Threading.Thread.CurrentThread.Join(500);
                stopped = !Searching;
            }            
        }

        delegate void InvokeDoneSearching();
        void DoneSearching()
        {
            groupTables = new Hashtable[lvreal.Columns.Count];
            for (int column = 0; column < lvreal.Columns.Count; column++)
            {
                groupTables[column] = CreateGroupsTable(column);
            }
            SetGroups(6);
            lvreal.EndUpdate();

            if (searchtool!=null) searchtool.NotifyFinishedSearch();
            pnErr.Text = pnErr.Text.Replace("{nr}", Helper.WindowsRegistry.MaxSearchResults.ToString());
            pnErr.Visible = truncated;
            Wait.Stop();
            Wait.Image = null;
            if (booby.PrettyGirls.PervyMode)
            {
                if (lvreal.Items.Count > 0) DoMe.Play();
                else Aah.Play();
            }
        }

        internal void ThreadRunner()
        {
            lock (threads)
            {
                runningthreads++;
            }
            while (true)
            {
                string name = "";
                lock (packages)
                {
                    if (packages.Count == 0 || truncated) break;
                    name = packages[0];
                    packages.RemoveAt(0);
                    Wait.Progress++;
                    Wait.Message = SimPe.Localization.GetString("Searching") + " " + System.IO.Path.GetFileNameWithoutExtension(name);
                }

                if (System.IO.File.Exists(name))
                {
                    SimPe.Packages.File pkg = SimPe.Packages.File.LoadFromFile(name);
                    searchtool.SearchPackage(pkg);
                }
            }

            lock (threads)
            {
                runningthreads--;
                Wait.Message = "Searching...";
                if (runningthreads == 1)
                    Wait.Progress++;
                if (runningthreads == 0)
                {
                    if (this.InvokeRequired) this.BeginInvoke(new InvokeDoneSearching(DoneSearching));
                    else DoneSearching();
                }
            }
        }
        #endregion

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            StopSearch();
        }

        private void SetGroups(int column)
        {
            lvreal.Groups.Clear();
            Hashtable groups = (Hashtable)groupTables[column];
            ListViewGroup[] groupsArray = new ListViewGroup[groups.Count];
            groups.Values.CopyTo(groupsArray, 0);
            Array.Sort(groupsArray, new ListViewGroupSorter(lvreal.Sorting));
            lvreal.Groups.AddRange(groupsArray);
            foreach (ListViewItem item in lvreal.Items)
            {
                string subItemText = item.SubItems[column].Text;
                if (column == 0)
                {
                    subItemText = subItemText.Substring(0, 1);
                }
                item.Group = (ListViewGroup)groups[subItemText];
            }
        }

        private Hashtable CreateGroupsTable(int column)
        {
            Hashtable groups = new Hashtable();
            foreach (ListViewItem item in lvreal.Items)
            {
                string subItemText = item.SubItems[column].Text;
                if (column == 0)
                {
                    subItemText = subItemText.Substring(0, 1);
                }
                if (!groups.Contains(subItemText))
                {
                    groups.Add(subItemText, new ListViewGroup(subItemText, HorizontalAlignment.Left));
                }
            }
            return groups;
        }

        private class ListViewGroupSorter : IComparer
        {
            private SortOrder order;
            public ListViewGroupSorter(SortOrder theOrder)
            {
                order = theOrder;
            }
            public int Compare(object x, object y)
            {
                int result = String.Compare(
                    ((ListViewGroup)x).Header,
                    ((ListViewGroup)y).Header
                );
                if (order == SortOrder.Ascending)
                {
                    return result;
                }
                else
                {
                    return -result;
                }
            }
        }
    }
}
