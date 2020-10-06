using EntityDataFramework.Core.Models.Query.Column;

namespace EntityDataFramework.Core.Models.Condition
{
	public class ColumnQueryCondition : IQueryCondition {
		public IQueryColumn QueryColumn { get; }
		public ColumnQueryCondition(IQueryColumn queryColumn) {
			QueryColumn = queryColumn;
		}
	}
}
