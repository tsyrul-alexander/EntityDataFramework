using System.Collections.Generic;
using EntityDataFramework.Core.Models.Query;

namespace EntityDataFramework.Core.Models.Condition {
	public class ExpressionConditionOptions {
		public List<QueryJoin> Joins { get; set; }
		public List<QueryColumn> Columns { get; set; }
		public List<IQueryCondition> Conditions { get; set; }
		public ExpressionConditionOptions(List<QueryJoin> joins = null, List<QueryColumn> columns = null,
				List<IQueryCondition> conditions = null) {
			Joins = joins ?? new List<QueryJoin>();
			Columns = columns ?? new List<QueryColumn>();
			Conditions = conditions ?? new List<IQueryCondition>();
		}
	}
}