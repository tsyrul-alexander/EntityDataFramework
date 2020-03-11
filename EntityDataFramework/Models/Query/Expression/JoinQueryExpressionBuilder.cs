using EntityDataFramework.Core.Models.Query.Expression.Contract;
using EntityDataFramework.Core.Models.Query.Join;

namespace EntityDataFramework.Core.Models.Query.Expression {
	public class JoinQueryExpressionBuilder : BaseQueryExpressionBuilder, IJoinQueryExpressionBuilder {
		public IConditionQueryExpressionBuilder ConditionQueryExpressionBuilder { get; }
		public JoinQueryExpressionOptions Options { get; }
		public JoinQueryExpressionBuilder(IConditionQueryExpressionBuilder conditionQueryExpressionBuilder, JoinQueryExpressionOptions options = null) {
			ConditionQueryExpressionBuilder = conditionQueryExpressionBuilder;
			Options = options ?? new JoinQueryExpressionOptions();
		}
		public QueryJoin Parse(string joinSchemaName, System.Linq.Expressions.Expression expression) {
			return new QueryJoin(joinSchemaName, ConditionQueryExpressionBuilder.Parse(expression));
		}
	}
}
