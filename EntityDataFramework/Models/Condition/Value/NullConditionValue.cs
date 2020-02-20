namespace EntityDataFramework.Core.Models.Condition.Value {
	public class NullConditionValue : BaseConditionValue {
		public override string GetValue() {
			return string.Empty;
		}
	}
}