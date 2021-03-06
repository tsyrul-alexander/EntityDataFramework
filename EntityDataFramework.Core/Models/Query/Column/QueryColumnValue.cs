﻿using EntityDataFramework.Core.Models.Condition.Value;
using EntityDataFramework.Core.Models.Query.Column;

namespace EntityDataFramework.Core.Models.Query {
	public class QueryColumnValue {
		public IQueryColumn Column { get; set; }
		public IConditionConstantValue ConstantValue { get; set; }
		public QueryColumnValue(IQueryColumn column = null, IConditionConstantValue constantValue = null) {
			Column = column;
			ConstantValue = constantValue;
		}
	}
}