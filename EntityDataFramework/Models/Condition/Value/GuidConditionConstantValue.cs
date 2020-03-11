using System;

namespace EntityDataFramework.Core.Models.Condition.Value {
	public class GuidConditionConstantValue : BaseConditionConstantValue {
		public Guid Value { get; }
		public GuidConditionConstantValue(Guid value) {
			Value = value;
		}
	}
}