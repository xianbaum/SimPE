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
 ***************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SimPe.Database
{
    /// <summary>
    /// Dockable panel that builds the SQLite resource index and lets the user
    /// browse / search it.
    /// </summary>
    public partial class DatabaseDock : Ambertation.Windows.Forms.DockPanel, SimPe.Interfaces.IDockableTool
    {
        const int RowLimit = 5000;

        Database db;
        DatabaseSyncThread sync;

        public DatabaseDock()
        {
            InitializeComponent();
        }

        Database Db
        {
            get
            {
                if (db == null) db = new Database();
                return db;
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            RefreshGrid();
        }

        #region IDockableTool Member
        public Ambertation.Windows.Forms.DockPanel GetDockableControl()
        {
            return this;
        }

        public event SimPe.Events.ChangedResourceEvent ShowNewResource;

        public void RefreshDock(object sender, SimPe.Events.ResourceEventArgs e)
        {
            RefreshGrid();
        }
        #endregion

        #region IToolExt Member
        public Image Icon
        {
            get { return this.TabImage; }
        }

        public Shortcut Shortcut
        {
            get { return Shortcut.None; }
        }

        public new virtual bool Visible
        {
            get { return this.IsDocked || this.IsFloating; }
        }
        #endregion

        public override string ToString()
        {
            return this.Text;
        }

        #region UI
        void RefreshGrid()
        {
            if (!this.IsHandleCreated) return;
            try
            {
                List<Database.ResourceEntry> rows = Db.QueryResources(txtSearch.Text.Trim(), RowLimit);
                grid.SuspendLayout();
                grid.Rows.Clear();
                foreach (Database.ResourceEntry en in rows)
                {
                    grid.Rows.Add(
                        TypeName(en.Type),
                        Hex(en.Type),
                        Hex(en.Group),
                        Hex(en.SubType),
                        Hex(en.Instance),
                        en.Name,
                        en.FileName);
                }
                grid.ResumeLayout();
                UpdateStatus(rows.Count);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error: " + ex.Message;
            }
        }

        void UpdateStatus(int shown)
        {
            try
            {
                lblStatus.Text = string.Format(
                    "{0:N0} resources in {1:N0} packages   (showing {2:N0}{3})",
                    Db.ResourceCount, Db.FileCount, shown,
                    shown >= RowLimit ? ", capped -- refine your search" : "");
            }
            catch { /* status is best-effort */ }
        }

        static string Hex(uint v)
        {
            return "0x" + v.ToString("X8");
        }

        static string TypeName(uint t)
        {
            try { return SimPe.Data.MetaData.FindTypeAlias(t).shortname; }
            catch { return ""; }
        }

        void btnBuild_Click(object sender, EventArgs e)
        {
            if (sync != null && sync.Running) return;

            sync = new DatabaseSyncThread(Db);
            btnBuild.Enabled = false;
            txtSearch.Enabled = false;
            lblStatus.Text = "Scanning file table...";
            sync.Start();
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (sync == null)
            {
                timer.Stop();
                return;
            }

            if (sync.Running)
            {
                lblStatus.Text = sync.Total > 0
                    ? string.Format("Indexing {0:N0} / {1:N0} packages...", sync.Done, sync.Total)
                    : "Scanning file table...";
                return;
            }

            // finished
            timer.Stop();
            sync = null;
            btnBuild.Enabled = true;
            txtSearch.Enabled = true;
            RefreshGrid();
        }

        void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (sync != null && sync.Running) return;
            RefreshGrid();
        }
        #endregion
    }
}
