using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Linq;
using EDictionary.Core.Utilities;
using EDictionary.Core.Models;
using EDictionary.Core.Extensions;

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
					string wordNumber = word.Id.MatchRegex("[0-9]");
					string wordName = word.Name;

					if (wordNumber != null)
						wordName += " " + wordNumber;

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", Value = word.Id });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@name", Value = wordName }); // TODO: Test
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

		public Result<Word> SelectDefinitionFrom(string wordID)
		{
			string definition;

			try
			{
				List<string> results = new List<string>();
				using (SQLiteCommand command = new SQLiteCommand(selectDefinitionFromQuery, dbConnection))
				{
					OpenConnection();

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", Value = wordID});
					using (SQLiteCommand fmd = dbConnection.CreateCommand())
					{
						command.CommandType = CommandType.Text;
						SQLiteDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							results.Add(Convert.ToString(reader[DictionaryTable.Definition]));
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

		public Result<List<string>> SelectID()
		{
			try
			{
				List<string> results = new List<string>();
				using (SQLiteCommand command = new SQLiteCommand(selectIDQuery, dbConnection))
				{
					OpenConnection();
					using (SQLiteCommand fmd = dbConnection.CreateCommand())
					{
						command.CommandType = CommandType.Text;
						SQLiteDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							results.Add(Convert.ToString(reader[DictionaryTable.ID]));
						}
						return new Result<List<string>>(data:results, message:"", status:Status.Success);
					}
				}
			}
			catch (Exception exception)
			{
				LogWriter.Instance.WriteLine($"Error occured at SelectID in DataAccess:\n{exception.Message}");

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

		public Result<List<string>> SelectName()
		{
			try
			{
				List<string> results = new List<string>();
				using (SQLiteCommand command = new SQLiteCommand(selectNameQuery, dbConnection))
				{
					OpenConnection();
					using (SQLiteCommand fmd = dbConnection.CreateCommand())
					{
						command.CommandType = CommandType.Text;
						SQLiteDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							results.Add(Convert.ToString(reader[DictionaryTable.Name]));
						}
						return new Result<List<string>>(data: results, message: "", status: Status.Success);
					}
				}
			}
			catch (Exception exception)
			{
				LogWriter.Instance.WriteLine($"Error occured at SelectName in DataAccess:\n{exception.Message}");

				return new Result<List<string>>(
						message: exception.Message,
						innerMessage: "",
						status: Status.Error,
						exception: exception
						);
			}
			finally
			{
				CloseConnection();
			}
		}

		public Result<Dictionary<string, string>> SelectIDAndName()
		{
			try
			{
				Dictionary<string, string> results = new Dictionary<string, string>();
				using (SQLiteCommand command = new SQLiteCommand(selectIDAndNameQuery, dbConnection))
				{
					OpenConnection();
					using (SQLiteCommand fmd = dbConnection.CreateCommand())
					{
						command.CommandType = CommandType.Text;
						SQLiteDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							results.Add(
								Convert.ToString(reader[DictionaryTable.ID]),
								Convert.ToString(reader[DictionaryTable.Name]));
						}
						return new Result<Dictionary<string, string>>(data: results, message: "", status: Status.Success);
					}
				}
			}
			catch (Exception exception)
			{
				LogWriter.Instance.WriteLine($"Error occured at SelectIDAndName in DataAccess:\n{exception.Message}");

				return new Result<Dictionary<string, string>>(
						message: exception.Message,
						innerMessage: "",
						status: Status.Error,
						exception: exception
						);
			}
			finally
			{
				CloseConnection();
			}
		}
	}
}
