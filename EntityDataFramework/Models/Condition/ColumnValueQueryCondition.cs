﻿using EntityDataFramework.Core.Models.Condition.Value;
using EntityDataFramework.Core.Models.Query.Column;

namespace EntityDataFramework.Core.Models.Condition {
	public class ColumnValueQueryCondition : IQueryCondition {
		public IQueryColumn Column { get; }
		public IConditionValue Value { get; }
		public ColumnValueQueryCondition(IQueryColumn column, IConditionValue value, ConditionComparisonType comparisonType) {
			Column = column;
			Value = value;
		}
		public string GetSqlText() {
			return string.Empty;
		}
	}
}