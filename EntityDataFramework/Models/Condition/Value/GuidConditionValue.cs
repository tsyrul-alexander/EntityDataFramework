using System;

namespace EntityDataFramework.Core.Models.Condition.Value {
	public class GuidConditionValue : BaseConditionValue {
		public Guid Value { get; }
		public GuidConditionValue(Guid value) {
			Value = value;
		}
		public override string GetValue() {
			return string.Empty;
		}
	}
}