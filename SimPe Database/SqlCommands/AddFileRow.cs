using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using System.Data.Common;
using System.Data;

namespace SimPe.Database.SqlCommands
{
    class AddFileRow : CommandBase
    {
        public AddFileRow(SqliteConnection sql)
            : base(sql, "INSERT INTO files (filename, modified) VALUES(@fileName, @modified);")
        {
        }

        SqliteParameter fileName;
        public string FileName
        {
            get { return fileName.Value.ToString(); }
            set { fileName.Value = value.Trim().ToLower(); }
        }

        SqliteParameter modified;
        public DateTime Modified
        {
            get { return (DateTime)modified.Value; }
            set { modified.Value = Database.ToTimeStamp(value); }
        }

        protected override void CreatParameters()
        {
            fileName = Command.Parameters.AddWithValue("@fileName", "");
            modified = Command.Parameters.AddWithValue("@modified", Database.Now());
        }
    }
}
