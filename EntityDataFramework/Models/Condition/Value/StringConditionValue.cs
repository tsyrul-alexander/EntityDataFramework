namespace EntityDataFramework.Core.Models.Condition.Value {
	public class StringConditionValue : BaseConditionValue {
		public string Value { get; }
		public StringConditionValue(string value) {
			Value = value;
		}
		public override string GetValue() {
			return string.Empty;
		}
	}
}
