using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDictionary.Core.Utilities;
using EDictionary.Core.Models;

namespace EDictionary.Core.Presenters
{
	public static class DataAccess
	{
		private static readonly string saveDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\EDictionary\Data");
		private static readonly string savePath =  Path.GetFullPath($"{saveDir}\\words.sqlite");
		private static readonly string connectionStr = $"Data Source={savePath};Version=3;";

		private static readonly string tablename = "wordlist";
		private static readonly string insertQuery = $"INSERT INTO {tablename} (WordID, Definition) VALUES (@wordID, @definition)";
		private static readonly string createTableQuery = $"CREATE TABLE IF NOT EXISTS {tablename} (WordID NVARCHAR, Definition NVARCHAR)";

		private static SQLiteConnection dbConnection;

		// Static class dont have constructor :(
		public static void Create()
		{
			if (!File.Exists(savePath))
			{
				SQLiteConnection.CreateFile(savePath);
			}
			dbConnection = new SQLiteConnection(connectionStr);
			CreateTable();
		}

		private static void OpenConnection()
		{
			if (dbConnection.State != ConnectionState.Open)
				dbConnection.Open();
		}
		private static void CloseConnection()
		{
			if (dbConnection.State != ConnectionState.Closed)
				dbConnection.Close();
		}

		public static void CreateTable()
		{
			try
			{
				using (SQLiteCommand command = new SQLiteCommand(createTableQuery, dbConnection))
				{
					OpenConnection();
					command.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				// TODO
			}
			finally
			{
				CloseConnection();
			}
		}
		public static void Insert(string wordJsonStr)
		{
			try
			{
				using (SQLiteCommand command = new SQLiteCommand(insertQuery, dbConnection))
				{
					OpenConnection();

					Word word = JsonHelper.Deserialize(wordJsonStr);

					command.Parameters.Add(new SQLiteParameter() {ParameterName = "@wordID",     Value = word.Keyword});
					command.Parameters.Add(new SQLiteParameter() {ParameterName = "@definition", Value = wordJsonStr});

					command.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				/* LogWriter.Instance.WriteLine(String.Format("Error occured at SaveVocable in DataBaseAccess:\n{0}", ex.Message)); */
				/* return new Result<int>(ex.Message, "", Status.Error, ex); */
			}
			finally
			{
				CloseConnection();
			}
		}
	}
}
