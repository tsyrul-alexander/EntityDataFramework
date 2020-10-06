using EntityDataFramework.Core.Models.Query;
using EntityDataFramework.Core.Models.Query.Select;

namespace EntityDataFramework.Core.Utilities {
	public static class QueryUtilities {
		public static QueryColumn AddQueryColumn(this SelectQuery query, string tableName, string columnName) {
			var column = new QueryColumn(tableName, columnName);
			query.Columns.Add(column);
			return column;
		}
	}
}