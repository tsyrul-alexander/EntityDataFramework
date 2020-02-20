namespace EntityDataFramework.Core.Models.Query {
	public class QueryJoin {
		public string TableName { get; set; }
		public string ColumnName { get; set; }
		public string Alias { get; set; }
		public QueryJoin(string tableName = null, string columnName = null, string alias = null) {
			TableName = tableName;
			ColumnName = columnName;
			Alias = alias;
		}
		public static bool operator ==(QueryJoin queryJoin1, QueryJoin queryJoin2) {
			return queryJoin1?.TableName == queryJoin2?.TableName && queryJoin1?.Alias == queryJoin2?.Alias &&
				queryJoin1?.ColumnName == queryJoin2?.ColumnName;
		}
		public static bool operator !=(QueryJoin queryJoin1, QueryJoin queryJoin2) {
			return !(queryJoin1 == queryJoin2);
		}
	}
}