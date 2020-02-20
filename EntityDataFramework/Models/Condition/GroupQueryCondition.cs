using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityDataFramework.Core.Models.Condition {
	public class GroupQueryCondition : IQueryCondition {
		public ConditionLogicalOperation LogicalOperation { get; }
		public List<IQueryCondition> QueryConditions { get; }
		public GroupQueryCondition(ConditionLogicalOperation logicalOperation, params IQueryCondition[] queryConditions) {
			LogicalOperation = logicalOperation;
			QueryConditions = queryConditions.ToList();
		}
		public string GetSqlText(string tableName) {
			throw new NotImplementedException();
		}
	}
}
