using EntityDataFramework.Core.Models.Query.Join;

namespace EntityDataFramework.Core.Models.Query.Expression.Contract {
	public interface IJoinQueryExpressionBuilder {
		QueryJoin Parse(string joinSchemaName, System.Linq.Expressions.Expression expression);
	}
}
