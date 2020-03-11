namespace EntityDataFramework.Core.Models.Condition.Value {
	public class StringConditionConstantValue : BaseConditionConstantValue {
		public string Value { get; }
		public StringConditionConstantValue(string value) {
			Value = value;
		}
	}
}
