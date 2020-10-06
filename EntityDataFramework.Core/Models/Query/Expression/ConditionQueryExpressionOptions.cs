using System.Collections.Generic;
using EntityDataFramework.Core.Models.Condition;
using EntityDataFramework.Core.Models.Query.Contract;
using EntityDataFramework.Core.Models.Query.Join;

namespace EntityDataFramework.Core.Models.Query.Expression {
	public class ConditionQueryExpressionOptions : IQueryJoinList, IQueryConditionList {
		public List<QueryJoin> Joins { get; set; }
		public List<IQueryCondition> Conditions { get; set; }
	}
}