namespace EntityDataFramework.Core.Models.Query.Response {
	public class SelectQueryColumnValue {
		public string ColumnName { get; set; }
		internal object Value { get; set; }
		public SelectQueryColumnValue(string columnName = null, object value = null) {
			ColumnName = columnName;
			Value = value;
		}
	}
}