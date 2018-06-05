using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace EDictionary.Core.Data
{
	public partial class HistoryAccess : SqliteAccess
	{
		public HistoryAccess(DatabaseInfo dbInfo) : base(dbInfo)
		{
			
		}

		public override Result CreateTable()
		{
			try
			{
				using (SQLiteCommand command = new SQLiteCommand(createTableQuery, dbConnection))
				{
					OpenConnection();
					command.ExecuteNonQuery();

					return new Result(message: "", innerMessage: "", status: Status.Success);
				}
			}
			catch (Exception exception)
			{
				return new Result(message: exception.Message, status: Status.Error, exception: exception);
			}
			finally
			{
				CloseConnection();
			}
		}

		public Result InsertItem<T>(T item)
		{
			try
			{
				using (SQLiteCommand command = new SQLiteCommand(insertQuery, dbConnection))
				{
					OpenConnection();

					command.Parameters.AddWithValue("@name", item);
					command.ExecuteNonQuery();

					return new Result(message: "", innerMessage: "", status: Status.Success);
				}
			}
			catch (Exception exception)
			{
				LogWriter.Instance.WriteLine($"Error occured at InsertWord in HistoryAccess:\n{exception.Message}");
				return new Result(message: exception.Message, status: Status.Error, exception: exception);
			}
			finally
			{
				CloseConnection();
			}
		}

		public Result<List<T>> SelectCollection<T>()
		{
			try
			{
				List<T> results = new List<T>();

				using (SQLiteCommand command = new SQLiteCommand(selectAllQuery, dbConnection))
				{
					OpenConnection();

					using (SQLiteCommand fmd = dbConnection.CreateCommand())
					{
						command.CommandType = CommandType.Text;
						SQLiteDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							results.Add((T)(reader["Name"]));
						}
						return new Result<List<T>>(data: results, message: "", status: Status.Success);
					}
				}
			}
			catch (Exception exception)
			{
				LogWriter.Instance.WriteLine($"Error occured at SelectAllWords in HistoryAccess:\n{exception.Message}");

				return new Result<List<T>>(
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

		public Result DeleteFirstNRows(int count)
		{
			try
			{
				using (SQLiteCommand command = new SQLiteCommand(deleteFirstNQuery(count), dbConnection))
				{
					OpenConnection();
					command.ExecuteNonQuery();

					return new Result(message: "", innerMessage: "", status: Status.Success);
				}
			}
			catch (Exception exception)
			{
				LogWriter.Instance.WriteLine($"Error occured at DeleteFirstNWords in HistoryAccess:\n{exception.Message}");
				return new Result(message: exception.Message, status: Status.Error, exception: exception);
			}
			finally
			{
				CloseConnection();
			}
		}

		public Result<History<T>> SelectParameters<T>()
		{
			try
			{
				using (SQLiteCommand command = new SQLiteCommand(selectParameters, dbConnection))
				{
					OpenConnection();

					using (SQLiteCommand fmd = dbConnection.CreateCommand())
					{
						command.CommandType = CommandType.Text;
						SQLiteDataReader reader = command.ExecuteReader();

						reader.Read();

						History<T> history = new History<T>()
						{
							MaxHistory = int.Parse(reader["MaxHistory"].ToString()),
							CurrentIndex = int.Parse(reader["CurrentIndex"].ToString())
						};

						return new Result<History<T>>(data: history, message: "", status: Status.Success);
					}
				}
			}
			catch (Exception exception)
			{
				LogWriter.Instance.WriteLine($"Error occured at SelectParameters in HistoryAccess:\n{exception.Message}");

				return new Result<History<T>>(
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

		public Result UpdateMaxHistory(int maxHistory)
		{
			try
			{
				using (SQLiteCommand command = new SQLiteCommand(updateMaxHistory, dbConnection))
				{
					OpenConnection();

					command.Parameters.AddWithValue("@maxHistory", maxHistory);
					command.ExecuteNonQuery();

					return new Result(message: "", innerMessage: "", status: Status.Success);
				}
			}
			catch (Exception exception)
			{
				LogWriter.Instance.WriteLine($"Error occured at UpdateMaxHistory in HistoryAccess:\n{exception.Message}");
				return new Result(message: exception.Message, status: Status.Error, exception: exception);
			}
			finally
			{
				CloseConnection();
			}
		}

		public Result UpdateCurrentIndex(int currentIndex)
		{
			try
			{
				using (SQLiteCommand command = new SQLiteCommand(updateCurrentIndex, dbConnection))
				{
					OpenConnection();

					command.Parameters.AddWithValue("@currentIndex", currentIndex);
					command.ExecuteNonQuery();

					return new Result(message: "", innerMessage: "", status: Status.Success);
				}
			}
			catch (Exception exception)
			{
				LogWriter.Instance.WriteLine($"Error occured at UpdateCurrentIndex in HistoryAccess:\n{exception.Message}");
				return new Result(message: exception.Message, status: Status.Error, exception: exception);
			}
			finally
			{
				CloseConnection();
			}
		}
	}
}
