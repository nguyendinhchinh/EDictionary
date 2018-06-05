using EDictionary.Core.Models;

namespace EDictionary.Core.Data
{
   public partial class HistoryAccess
	{
		private static readonly string wordlistTable = "[History]";
		private static readonly string parameterTable = "[Parameter]";

		private readonly string createTableQuery = $@"
			CREATE TABLE IF NOT EXISTS {wordlistTable}
			(
				[ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
				[Name] NVARCHAR
			);
			CREATE TABLE IF NOT EXISTS {parameterTable}
			(
				[CurrentIndex] INTEGER,
				[MaxHistory] INTEGER
			);
			INSERT INTO {parameterTable}
			VALUES ({History<object>.Default.CurrentIndex}, {History<object>.Default.MaxHistory});";

		private readonly string selectAllQuery = $@"
			SELECT [Name] FROM {wordlistTable}";

		private readonly string insertQuery = $@"
			INSERT INTO {wordlistTable} ([Name]) VALUES (@name)";

		private string deleteFirstNQuery(int deleteRows)
		{
			return $@"
				DELETE FROM {wordlistTable}
				WHERE rowid IN (
					SELECT rowid FROM {wordlistTable}
					LIMIT {deleteRows})";
		}

		private readonly string selectParameters = $@"
			SELECT [MaxHistory], [CurrentIndex] FROM {parameterTable}";

		private readonly string updateCurrentIndex = $@"
			UPDATE {parameterTable} SET
			[CurrentIndex] = @currentIndex
			WHERE rowid=1;";

		private readonly string updateMaxHistory = $@"
			UPDATE {parameterTable} SET
			[MaxHistory] = @maxHistory
			WHERE rowid=1";
	}
}
