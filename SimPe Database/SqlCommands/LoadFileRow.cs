using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using System.Data.Common;
using System.Data;

namespace SimPe.Database.SqlCommands
{
    class LoadFileRow : CommandBase
    {
        public LoadFileRow(SqliteConnection sql)
            : base(sql, "SELECT fid, filename, modified FROM files WHERE filename=@fileName;")
        {
        }

        SqliteParameter fileName;
        public string FileName
        {
            get { return fileName.Value.ToString(); }
            set { fileName.Value = value.Trim().ToLower(); }
        }

        protected override void CreatParameters()
        {
            fileName = Command.Parameters.AddWithValue("@fileName", "");
        }
    }
}
