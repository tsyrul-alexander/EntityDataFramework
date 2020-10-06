using System;
using System.Collections.Generic;
using System.Text;

namespace EntityDataFramework.Core.Models.Condition
{
	public class BinaryQueryCondition : IQueryCondition {
		public ConditionComparisonType ComparisonType { get; set; }
		public IQueryCondition LeftCondition { get; set; }
		public IQueryCondition RightCondition { get; set; }
		public BinaryQueryCondition(IQueryCondition leftCondition = null, 
			IQueryCondition rightCondition = null,
			ConditionComparisonType comparisonType = ConditionComparisonType.Equal) {
			ComparisonType = comparisonType;
			LeftCondition = leftCondition;
			RightCondition = rightCondition;
		}
	}
}
