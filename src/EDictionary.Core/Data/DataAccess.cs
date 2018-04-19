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
using EDictionary.Core.Data;

namespace EDictionary.Core.Data
{
	public class DataAccess
	{
		private readonly static string saveDir = AppDomain.CurrentDomain.BaseDirectory;
		private readonly static string savePath = Path.GetFullPath($"{saveDir}\\words.sqlite");
		private readonly string connectionStr = $"Data Source={savePath};Version=3;";

		private readonly string insertQuery = $"INSERT INTO {WordTable.TableName} (WordID, Definition) VALUES (@wordID, @definition)";
		private readonly string createTableQuery = $"CREATE TABLE IF NOT EXISTS {WordTable.TableName} (WordID NVARCHAR, Definition NVARCHAR)";
		private readonly string selectDefinitionQuery = $"SELECT * FROM {WordTable.TableName} WHERE {WordTable.WordID} = @wordID";
		private readonly string selectWordListQuery = $"SELECT {WordTable.WordID} FROM {WordTable.TableName}";

		private SQLiteConnection dbConnection;

		public DataAccess()
		{
			if (!File.Exists(savePath))
			{
				SQLiteConnection.CreateFile(savePath);
			}
			dbConnection = new SQLiteConnection(connectionStr);
			CreateTable();
		}

		private void OpenConnection()
		{
			if (dbConnection.State != ConnectionState.Open)
				dbConnection.Open();
		}

		private void CloseConnection()
		{
			if (dbConnection.State != ConnectionState.Closed)
				dbConnection.Close();
		}

		public void CreateTable()
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

		public void Insert(string wordJsonStr)
		{
			try
			{
				using (SQLiteCommand command = new SQLiteCommand(insertQuery, dbConnection))
				{
					OpenConnection();

					Word word = JsonHelper.Deserialize(wordJsonStr);

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@wordID", Value = word.Id });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@definition", Value = wordJsonStr });

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

		public Word LookUp(string wordID)
		{
			string definition;

			try
			{
				List<string> results = new List<string>();
				using (SQLiteCommand command = new SQLiteCommand(selectDefinitionQuery, dbConnection))
				{
					OpenConnection();

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@wordID", Value = wordID });
					using (SQLiteCommand fmd = dbConnection.CreateCommand())
					{
						command.CommandType = CommandType.Text;
						SQLiteDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							results.Add(Convert.ToString(reader[WordTable.Definition]));
						}
						definition = results[0];

						return JsonHelper.Deserialize(definition);
					}
				}
			}
			catch (Exception ex)
			{
				/* LogWriter.Instance.WriteLine(String.Format("Error occured at SaveVocable in DataBaseAccess:\n{0}", ex.Message)); */
				/* return new Result<int>(ex.Message, "", Status.Error, ex); */
				return null;
			}
			finally
			{
				CloseConnection();
			}
		}

		public List<string> GetWordList()
		{
			try
			{
				List<string> results = new List<string>();
				using (SQLiteCommand command = new SQLiteCommand(selectWordListQuery, dbConnection))
				{
					OpenConnection();
					using (SQLiteCommand fmd = dbConnection.CreateCommand())
					{
						command.CommandType = CommandType.Text;
						SQLiteDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							results.Add(Convert.ToString(reader[WordTable.WordID]));
						}
						return results;
					}
				}
			}
			catch (Exception ex)
			{
				/* LogWriter.Instance.WriteLine(String.Format("Error occured at SaveVocable in DataBaseAccess:\n{0}", ex.Message)); */
				/* return new Result<int>(ex.Message, "", Status.Error, ex); */
				return null;
			}
			finally
			{
				CloseConnection();
			}
		}

	}
}
