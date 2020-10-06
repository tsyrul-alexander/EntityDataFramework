using EntityDataFramework.Core.Models.Query.Column;

namespace EntityDataFramework.Core.Models.Query.Expression.Contract
{
	public interface IColumnQueryExpressionBuilder {
		IQueryColumn Parse(System.Linq.Expressions.Expression expression);
	}
}
