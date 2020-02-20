namespace EntityDataFramework.Core.Models.Query {
	public class QueryColumn {
		public string Name { get; set; }
		public string TableName { get; set; }
		public QueryColumn(string tableName = null, string name = null) {
			Name = name;
			TableName = tableName;
		}
		public static bool operator ==(QueryColumn queryColumn1, QueryColumn queryColumn2) {
			return queryColumn1?.Name == queryColumn2?.Name && queryColumn1?.TableName == queryColumn2?.TableName;
		}
		public static bool operator !=(QueryColumn queryColumn1, QueryColumn queryColumn2) {
			return !(queryColumn1 == queryColumn2);
		}
	}
}