using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using System.Data.Common;
using System.Data;

namespace SimPe.Database.SqlCommands
{
    public abstract class CommandBase
    {
        SqliteCommand command;
        protected CommandBase(SqliteConnection sql, string cmd)
        {
            command = sql.CreateCommand();
            command.CommandText = cmd;
            CreatParameters();
        }

        protected abstract void CreatParameters();

        public SqliteCommand Command
        {
            get { return command; }
        }
    }
}
