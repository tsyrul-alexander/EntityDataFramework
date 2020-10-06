using EntityDataFramework.Core.Models.Condition.Value;

namespace EntityDataFramework.Core.Models.Condition {
	public class ConstantQueryCondition : IQueryCondition {
		public IConditionConstantValue ConstantValue { get; set; }
		public ConstantQueryCondition(IConditionConstantValue constantValue = null) {
			ConstantValue = constantValue;
		}
	}
}