using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using System.Data.Common;
using System.Data;

namespace SimPe.Database.SqlCommands
{
    class CreateTable : CommandBase
    {
        public CreateTable(SqliteConnection sql)
            : base(sql, "SELECT name FROM sqlite_master WHERE type='table' AND name=@tableName;")
        {
        }

        SqliteParameter tableName;
        public string TableName
        {
            get { return tableName.Value.ToString(); }
            set { tableName.Value = value; }
        }

        protected override void CreatParameters()
        {
            tableName = Command.Parameters.AddWithValue("@tableName", "");
            tableName.Size = 0xff;
        }
    }
}
