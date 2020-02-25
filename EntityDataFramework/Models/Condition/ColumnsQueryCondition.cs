using EntityDataFramework.Core.Models.Query.Column;

namespace EntityDataFramework.Core.Models.Condition {
	public class ColumnsQueryCondition : IQueryCondition {
		public IQueryColumn QueryColumn1 { get; }
		public IQueryColumn QueryColumn2 { get; }
		public ConditionComparisonType ComparisonType { get; }
		public ColumnsQueryCondition(IQueryColumn queryColumn1, IQueryColumn queryColumn2, ConditionComparisonType comparisonType) {
			QueryColumn1 = queryColumn1;
			QueryColumn2 = queryColumn2;
			ComparisonType = comparisonType;
		}
	}
}
