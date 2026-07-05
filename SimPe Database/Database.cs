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
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using System.Data.Common;
using System.Data;

namespace SimPe.Database
{
    /// <summary>
    /// A SQLite-backed index of the resources found in the SimPe file table.
    /// </summary>
    class Database : IDisposable
    {
        SqliteConnection sql;

        public string DatabaseName
        {
            get
            {
                return System.IO.Path.Combine(Helper.SimPeDataPath, "cache.db3");
            }
        }

        public Database()
        {
            sql = new SqliteConnection("Data Source=" + DatabaseName);
            sql.Open();
            PrepareDatabase();
        }

        public void Dispose()
        {
            if (sql != null)
            {
                sql.Close();
                sql.Dispose();
                sql = null;
            }
            GC.SuppressFinalize(this);
        }

        ~Database()
        {
            if (sql != null) sql.Close();
        }

        #region Commands
        SqlCommands.CreateTable ctable;
        SqlCommands.CreateTable CreateTableCmd
        {
            get
            {
                if (ctable == null) ctable = new SimPe.Database.SqlCommands.CreateTable(sql);
                return ctable;
            }
        }
        #endregion

        #region Create Tables
        protected bool TableExists(string name)
        {
            CreateTableCmd.TableName = name;
            using (SqliteDataReader dr = CreateTableCmd.Command.ExecuteReader(CommandBehavior.SingleResult))
            {
                return dr.HasRows;
            }
        }

        void PrepareDatabase()
        {
            if (!TableExists("files")) CreateFilesTable();
            if (!TableExists("tgi")) CreateTGITable();
            if (!TableExists("guid")) CreateGuidTable();
            if (!TableExists("thumb")) CreateThumbTable();
            if (!TableExists("outlink")) CreateOutLinkTable();
        }

        void CreateTable(string name, string s)
        {
            using (SqliteCommand mycommand = sql.CreateCommand())
            {
                mycommand.CommandText = String.Format("CREATE TABLE '" + name + "' (" + s + ")");
                mycommand.ExecuteNonQuery();
            }
        }

        void CreateFilesTable()
        {
            try
            {
                CreateTable("files", "'fid' INTEGER NOT NULL PRIMARY KEY, 'filename' VARCHAR(255)  NOT NULL, 'modified' TIMESTAMP  NOT NULL");
            }
            catch (Exception ex) { Helper.ExceptionMessage(ex); }
        }

        void CreateTGITable()
        {
            try
            {
                CreateTable("tgi", "'rid' INTEGER PRIMARY KEY NOT NULL, 't' INTEGER  NOT NULL, 'g' INTEGER  NOT NULL, 'ihi' INTEGER  NOT NULL, 'ilo' INTEGER  NOT NULL, 'name' VARCHAR(255)  NULL, 'fid' INTEGER  NOT NULL");
            }
            catch (Exception ex) { Helper.ExceptionMessage(ex); }
        }

        void CreateGuidTable()
        {
            try
            {
                CreateTable("guid", "'rid' INTEGER  NOT NULL PRIMARY KEY, 'guid' INTEGER  NOT NULL");
            }
            catch (Exception ex) { Helper.ExceptionMessage(ex); }
        }

        void CreateOutLinkTable()
        {
            try
            {
                CreateTable("outlink", "'rid' INTEGER  NOT NULL PRIMARY KEY, 't' INTEGER  NOT NULL, 'g' INTEGER  NOT NULL, 'ihi' INTEGER  NOT NULL, 'ilo' INTEGER  NOT NULL");
            }
            catch (Exception ex) { Helper.ExceptionMessage(ex); }
        }

        void CreateThumbTable()
        {
            try
            {
                CreateTable("thumb", "'rid' INTEGER  UNIQUE NOT NULL PRIMARY KEY, 'img' BLOB  NULL");
            }
            catch (Exception ex) { Helper.ExceptionMessage(ex); }
        }
        #endregion

        #region File discovery
        public class FileList : List<string> { }

        public void LoadUpdateableFiles(FileList list, SimPe.FileTableItem fti)
        {
            if (!fti.IsUseable || fti.Ignore || !fti.IsAvail) return;
            if (fti.IsFile) list.Add(fti.Name);
            else LoadDirectory(list, fti.Name, fti.IsRecursive);
        }

        /// <summary>
        /// Adds all .package files in the given directory to the work list.
        /// </summary>
        public void LoadDirectory(FileList list, string dir, bool rec)
        {
            if (!System.IO.Directory.Exists(dir)) return;

            foreach (string file in System.IO.Directory.GetFiles(dir, "*.package"))
                list.Add(file);

            if (rec)
            {
                foreach (string d in System.IO.Directory.GetDirectories(dir))
                    LoadDirectory(list, d, true);
            }
        }
        #endregion

        #region Sync
        long LastInsertRowId()
        {
            using (SqliteCommand c = sql.CreateCommand())
            {
                c.CommandText = "SELECT last_insert_rowid();";
                return (long)c.ExecuteScalar();
            }
        }

        SimPe.Interfaces.Plugin.Internal.IPackedFileWrapper ResolveWrapper(SimPe.Packages.PackedFileDescriptor pfd)
        {
            SimPe.Data.TypeAlias ta = pfd.TypeName;
            if (ta.IgnoreDuringCacheBuild) return null;
            if (pfd.Type == Data.MetaData.LIFO || pfd.Type == Data.MetaData.GMDC)
                return new SimPe.Plugin.GenericRcol(null, true);
            return SimPe.FileTable.WrapperRegistry.FindHandler(pfd.Type);
        }

        /// <summary>
        /// Adds/updates the given .package file in the cache.
        /// </summary>
        /// <returns>true, if the contents had to be (re)indexed</returns>
        public bool AddPackageFile(string flname)
        {
            if (!System.IO.File.Exists(flname)) return false;

            DateTime filemod = TruncateToSeconds(System.IO.File.GetLastWriteTime(flname));
            long fid;
            bool mod;

            SqlCommands.LoadFileRow lfr = new SimPe.Database.SqlCommands.LoadFileRow(sql);
            lfr.FileName = flname;

            long existingFid = -1;
            DateTime storedMod = DateTime.MinValue;
            using (SqliteDataReader dr = lfr.Command.ExecuteReader(CommandBehavior.SingleResult))
            {
                if (dr.Read())
                {
                    existingFid = dr.GetInt64(0);
                    storedMod = dr.GetDateTime(2);
                }
            }

            if (existingFid < 0)
            {
                SqlCommands.AddFileRow afr = new SimPe.Database.SqlCommands.AddFileRow(sql);
                afr.FileName = flname;
                afr.Modified = filemod;
                afr.Command.ExecuteNonQuery();
                fid = LastInsertRowId();
                mod = true;
            }
            else
            {
                fid = existingFid;
                mod = filemod > storedMod;
                if (mod)
                {
                    SqlCommands.UpdateFileModDate ufmd = new SimPe.Database.SqlCommands.UpdateFileModDate(sql);
                    ufmd.FileName = flname;
                    ufmd.Modified = filemod;
                    ufmd.Command.ExecuteNonQuery();

                    SqlCommands.RemoveResources rr = new SimPe.Database.SqlCommands.RemoveResources(sql);
                    rr.FileId = fid;
                    rr.CleanupPackageResources();
                }
            }

            if (mod)
            {
                SimPe.Packages.File pkg = SimPe.Packages.File.LoadFromFile(flname);
                try
                {
                    SqlCommands.AddResources ar = new SimPe.Database.SqlCommands.AddResources(sql);
                    ar.PrepareLoop(fid);

                    foreach (SimPe.Packages.PackedFileDescriptor pfd in pkg.Index)
                    {
                        SimPe.Interfaces.Plugin.Internal.IPackedFileWrapper wrp = ResolveWrapper(pfd);
                        if (wrp != null)
                        {
                            lock (wrp)
                            {
                                wrp.ProcessData(pfd, pkg, true);
                                ar.AddResourceLoop(fid, pfd, wrp);
                                wrp.Dispose();
                            }
                        }
                        else ar.AddResourceLoop(fid, pfd, null);
                    }

                    ar.FinishLoop();
                }
                finally
                {
                    pkg.Close();
                }
            }

            return mod;
        }
        #endregion

        #region Queries (UI)
        /// <summary>One indexed resource, as surfaced to the dock.</summary>
        public class ResourceEntry
        {
            public uint Type;
            public uint Group;
            public uint SubType;
            public uint Instance;
            public string Name;
            public string FileName;
        }

        public int CountRows(string table)
        {
            using (SqliteCommand c = sql.CreateCommand())
            {
                c.CommandText = "SELECT COUNT(*) FROM " + table + ";";
                object o = c.ExecuteScalar();
                return (o == null || o == DBNull.Value) ? 0 : Convert.ToInt32(o);
            }
        }

        public int ResourceCount { get { return CountRows("tgi"); } }
        public int FileCount { get { return CountRows("files"); } }

        /// <summary>
        /// Returns indexed resources, optionally filtered by a name substring.
        /// </summary>
        public List<ResourceEntry> QueryResources(string filter, int limit)
        {
            List<ResourceEntry> res = new List<ResourceEntry>();
            bool hasFilter = !string.IsNullOrEmpty(filter);
            using (SqliteCommand cmd = sql.CreateCommand())
            {
                cmd.CommandText =
                    "SELECT t.t, t.g, t.ihi, t.ilo, t.name, f.filename " +
                    "FROM tgi t LEFT JOIN files f ON f.fid = t.fid " +
                    (hasFilter ? "WHERE t.name LIKE @like " : "") +
                    "ORDER BY t.t LIMIT @lim;";
                if (hasFilter) cmd.Parameters.AddWithValue("@like", "%" + filter + "%");
                cmd.Parameters.AddWithValue("@lim", limit);

                using (SqliteDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ResourceEntry e = new ResourceEntry();
                        e.Type = (uint)dr.GetInt64(0);
                        e.Group = (uint)dr.GetInt64(1);
                        e.SubType = (uint)dr.GetInt64(2);
                        e.Instance = (uint)dr.GetInt64(3);
                        e.Name = dr.IsDBNull(4) ? "" : dr.GetString(4);
                        e.FileName = dr.IsDBNull(5) ? "" : dr.GetString(5);
                        res.Add(e);
                    }
                }
            }
            return res;
        }
        #endregion

        #region Helpers
        static DateTime TruncateToSeconds(DateTime dt)
        {
            return new DateTime(dt.Ticks - (dt.Ticks % TimeSpan.TicksPerSecond), dt.Kind);
        }

        public static string ToTimeStamp(DateTime tm)
        {
            return tm.ToString("s", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        public static string Now()
        {
            return ToTimeStamp(DateTime.Now);
        }
        #endregion
    }
}
