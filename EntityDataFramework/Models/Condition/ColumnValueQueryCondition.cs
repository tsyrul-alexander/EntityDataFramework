using EntityDataFramework.Core.Models.Condition.Value;
using EntityDataFramework.Core.Models.Query;

namespace EntityDataFramework.Core.Models.Condition {
	public class ColumnValueQueryCondition : IQueryCondition {
		public QueryColumn Column { get; }
		public IConditionValue Value { get; }
		public ColumnValueQueryCondition(QueryColumn column, IConditionValue value) {
			Column = column;
			Value = value;
		}
		public string GetSqlText(string tableName) {
			return string.Empty;
		}
	}
}
