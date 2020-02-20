using System.Linq.Expressions;
using EntityDataFramework.Core.Models.Query.Column;
using EntityDataFramework.Core.Models.Query.Contract;

namespace EntityDataFramework.Core.Models.Query.Expression {
	public abstract class BaseQueryExpressionBuilder {
		public virtual IQueryColumn GetParseMemberColumn(MemberExpression memberExpression, IQueryJoinList options) {
			var queryColumn = GetQueryColumn(ref memberExpression);
			while (memberExpression != null) {
				var tableName = memberExpression.Expression.Type.Name;
				var columnName = memberExpression.Member.Name;
				var joinTable = new QueryJoin(tableName, columnName);
				RegisterQueryJoin(joinTable, options);
				memberExpression = memberExpression.Expression as MemberExpression;
			}
			return queryColumn;
		}
		public virtual QueryColumn GetQueryColumn(ref MemberExpression memberExpression) {
			var tableName = memberExpression.Expression.Type.Name;
			var columnName = memberExpression.Member.Name;
			memberExpression = memberExpression.Expression as MemberExpression;
			var queryColumn = new QueryColumn(tableName, columnName);
			return queryColumn;
		}
		public virtual void RegisterQueryJoin(QueryJoin join, IQueryJoinList options) {
			if (!options.Joins.Exists(queryJoin => queryJoin == join)) {
				options.Joins.Add(join);
			}
		}
	}
}