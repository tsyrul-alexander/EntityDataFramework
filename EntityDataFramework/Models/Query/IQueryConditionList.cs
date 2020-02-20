using System.Collections.Generic;
using EntityDataFramework.Core.Models.Condition;

namespace EntityDataFramework.Core.Models.Query
{
	interface IQueryConditionList {
		List<IQueryCondition> Conditions { get; set; }
	}
}
