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
	public partial class DataAccess
	{
		private readonly static string saveDir = AppDomain.CurrentDomain.BaseDirectory;
		private readonly static string savePath = Path.GetFullPath($"{saveDir}\\words.sqlite");
		private readonly string connectionStr = $"Data Source={savePath};Version=3;";

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

		public Result CreateTable()
		{
			try
			{
				using (SQLiteCommand command = new SQLiteCommand(createTableQuery, dbConnection))
				{
					OpenConnection();
					command.ExecuteNonQuery();

					return new Result(message:"", innerMessage:"", status:Status.Success);
				}
			}
			catch (Exception exception)
			{
				return new Result(message:exception.Message, status:Status.Error, exception:exception);
			}
			finally
			{
				CloseConnection();
			}
		}

		public Result Insert(string wordJsonStr)
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

					return new Result(message:"", innerMessage:"", status:Status.Success);
				}
			}
			catch (Exception exception)
			{
				LogWriter.Instance.WriteLine($"Error occured at Insert in DataAccess:\n{exception.Message}");
				return new Result(message:exception.Message, status:Status.Error, exception:exception);
			}
			finally
			{
				CloseConnection();
			}
		}

		public Result<Word> LookUpSimilar(string wordID)
		{
			string definition;

			try
			{
				List<string> results = new List<string>();
				using (SQLiteCommand command = new SQLiteCommand(globSelectQuery, dbConnection))
				{
					OpenConnection();

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@wordID", Value = wordID + "_?"});
					using (SQLiteCommand fmd = dbConnection.CreateCommand())
					{
						command.CommandType = CommandType.Text;
						SQLiteDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							results.Add(Convert.ToString(reader[WordTable.Definition]));
						}

						definition = results.ElementAtOrDefault(0);
						Word word = null;

						if (definition != null)
							word = JsonHelper.Deserialize(definition);

						return new Result<Word>(data:word, message:"", status:Status.Success);
					}
				}
			}
			catch (Exception exception)
			{
				LogWriter.Instance.WriteLine($"Error occured at LookUp in DataAccess:\n{exception.Message}");
				
				return new Result<Word>(
						message:exception.Message,
						innerMessage:"",
						status:Status.Error,
						exception:exception
						);
			}
			finally
			{
				CloseConnection();
			}
		}

		public Result<Word> LookUp(string wordID)
		{
			string definition;

			try
			{
				List<string> results = new List<string>();
				using (SQLiteCommand command = new SQLiteCommand(selectQuery, dbConnection))
				{
					OpenConnection();

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@wordID", Value = wordID});
					using (SQLiteCommand fmd = dbConnection.CreateCommand())
					{
						command.CommandType = CommandType.Text;
						SQLiteDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							results.Add(Convert.ToString(reader[WordTable.Definition]));
						}

						definition = results.ElementAtOrDefault(0);
						Word word = null;

						if (definition != null)
							word = JsonHelper.Deserialize(definition);

						return new Result<Word>(data:word, message:"", status:Status.Success);
					}
				}
			}
			catch (Exception exception)
			{
				LogWriter.Instance.WriteLine($"Error occured at LookUp in DataAccess:\n{exception.Message}");
				
				return new Result<Word>(
						message:exception.Message,
						innerMessage:"",
						status:Status.Error,
						exception:exception
						);
			}
			finally
			{
				CloseConnection();
			}
		}

		public Result<List<string>> GetWordList()
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
						return new Result<List<string>>(data:results, message:"", status:Status.Success);
					}
				}
			}
			catch (Exception exception)
			{
				LogWriter.Instance.WriteLine($"Error occured at GetWordList in DataAccess:\n{exception.Message}");

				return new Result<List<string>>(
						message:exception.Message,
						innerMessage:"",
						status:Status.Error,
						exception:exception
						);
			}
			finally
			{
				CloseConnection();
			}
		}
	}
}
