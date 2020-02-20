using System.Collections.Generic;
using EntityDataFramework.Core.Models.Condition;

namespace EntityDataFramework.Core.Models.Query.Contract
{
	public interface IQueryConditionList {
		List<IQueryCondition> Conditions { get; set; }
	}
}
