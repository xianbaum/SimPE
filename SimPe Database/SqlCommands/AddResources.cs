using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using System.Data.Common;
using System.Data;

namespace SimPe.Database.SqlCommands
{
    /// <summary>
    /// Bulk-inserts the resources of one package into the <c>tgi</c> table.
    /// </summary>
    class AddResources
    {
        readonly SqliteConnection sql;
        SqliteTransaction trans;
        SqliteCommand insert;
        SqliteParameter pt, pg, pihi, pilo, pname, pfid;

        public AddResources(SqliteConnection sql)
        {
            this.sql = sql;
        }

        public void PrepareLoop(long fid)
        {
            trans = sql.BeginTransaction();

            insert = sql.CreateCommand();
            insert.Transaction = trans;
            insert.CommandText =
                "INSERT INTO tgi (t, g, ihi, ilo, name, fid) " +
                "VALUES (@t, @g, @ihi, @ilo, @name, @fid);";
            pt = insert.Parameters.AddWithValue("@t", 0L);
            pg = insert.Parameters.AddWithValue("@g", 0L);
            pihi = insert.Parameters.AddWithValue("@ihi", 0L);
            pilo = insert.Parameters.AddWithValue("@ilo", 0L);
            pname = insert.Parameters.AddWithValue("@name", "");
            pfid = insert.Parameters.AddWithValue("@fid", fid);
            insert.Prepare();
        }

        public void AddResourceLoop(long fid, SimPe.Packages.PackedFileDescriptor pfd, SimPe.Interfaces.Plugin.Internal.IPackedFileWrapper wrp)
        {
            pt.Value = (long)pfd.Type;
            pg.Value = (long)pfd.Group;
            pihi.Value = (long)pfd.SubType;
            pilo.Value = (long)pfd.Instance;
            pname.Value = (wrp != null && wrp.ResourceName != null) ? wrp.ResourceName : "";
            pfid.Value = fid;

            insert.ExecuteNonQuery();
        }

        public void FinishLoop()
        {
            lock (sql)
            {
                trans.Commit();
            }
            insert.Dispose();
            insert = null;
            trans.Dispose();
            trans = null;
        }
    }
}
