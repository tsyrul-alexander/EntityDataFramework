using EntityDataFramework.Core.Models.Condition;

namespace EntityDataFramework.Core.Models.Query.Expression.Contract {
	public interface IConditionQueryExpressionBuilder {
		IQueryCondition Parse(System.Linq.Expressions.Expression expression);
	}
}