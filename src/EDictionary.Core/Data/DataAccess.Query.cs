namespace EDictionary.Core.Data
{
	public partial class DataAccess
	{
		private readonly string insertQuery = $@"
			INSERT INTO {DictionaryTable.TableName}
				({DictionaryTable.ID},
				{DictionaryTable.Name},
				{DictionaryTable.Definition})
			VALUES (@ID, @name, @definition)";

		private readonly string createTableQuery = $@"
			CREATE TABLE IF NOT EXISTS {DictionaryTable.TableName}
				({DictionaryTable.ID} NVARCHAR,
				{DictionaryTable.Name} NVARCHAR,
				{DictionaryTable.Definition} NVARCHAR)";

		private readonly string selectDefinitionFromQuery = $@"
			SELECT {DictionaryTable.Definition} FROM {DictionaryTable.TableName}
			WHERE {DictionaryTable.ID} = @ID";

		private readonly string selectIDQuery = $@"
			SELECT {DictionaryTable.ID}
			FROM {DictionaryTable.TableName}";

		private readonly string selectNameQuery = $@"
			SELECT {DictionaryTable.Name}
			FROM {DictionaryTable.TableName}";

		private readonly string selectIDAndNameQuery = $@"
			SELECT {DictionaryTable.ID}, {DictionaryTable.Name}
			FROM {DictionaryTable.TableName}";
	}
}
