using EntityDataFramework.Core.Models.Condition.Value;
using EntityDataFramework.Core.Models.Query.Column;

namespace EntityDataFramework.Core.Models.Query {
	public class QueryColumnValue {
		public IQueryColumn Column { get; set; }
		public IConditionValue Value { get; set; }
		public QueryColumnValue(IQueryColumn column = null, IConditionValue value = null) {
			Column = column;
			Value = value;
		}
	}
}