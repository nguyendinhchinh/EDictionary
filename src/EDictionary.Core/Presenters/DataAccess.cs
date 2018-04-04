using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Core.Presenters
{
    public static class DataAccess
    {
        private static SQLiteConnection SQLiteConnection;

        public static void Init(string connectionStr, string filename)
        {
            SQLiteConnection.CreateFile(filename);
            SQLiteConnection = new SQLiteConnection(connectionStr);
            SQLiteConnection.Open();
        }

        public static void Execute(string cmd)
        {
            SQLiteCommand command = new SQLiteCommand(cmd, SQLiteConnection);
            command = new SQLiteCommand(cmd, SQLiteConnection);
            command.ExecuteNonQuery();
        }

        public static void Insert(string table, string values)
        {
            Execute("insert into " + table + " " +  values);
        }
    }
}
