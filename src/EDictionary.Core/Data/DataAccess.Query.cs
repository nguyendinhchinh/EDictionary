using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Core.Data
{
	public partial class DataAccess
	{
		private readonly string insertQuery = $@"
			INSERT INTO {WordTable.TableName} (WordID, Definition)
			VALUES (@wordID, @definition)";

		private readonly string createTableQuery = $@"
			CREATE TABLE IF NOT EXISTS {WordTable.TableName} (WordID NVARCHAR, Definition NVARCHAR)";

		private readonly string selectQuery = $@"
			SELECT * FROM {WordTable.TableName}
			WHERE {WordTable.WordID} = @wordID";

		private readonly string globSelectQuery = $@"
			SELECT * FROM {WordTable.TableName}
			WHERE {WordTable.WordID} GLOB @wordID";

		private readonly string selectWordListQuery = $@"
			SELECT {WordTable.WordID}
			FROM {WordTable.TableName}";
	}
}
