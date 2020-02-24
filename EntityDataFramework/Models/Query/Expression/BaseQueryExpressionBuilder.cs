using System;
using System.Linq.Expressions;
using System.Reflection;
using EntityDataFramework.Core.Models.Query.Column;
using EntityDataFramework.Core.Models.Query.Contract;
using EntityDataFramework.Core.Models.Query.Join;

namespace EntityDataFramework.Core.Models.Query.Expression {
	public abstract class BaseQueryExpressionBuilder {
		public virtual IQueryColumn GetParseMemberColumn(MemberExpression memberExpression, IQueryJoinList options) {
			var queryColumn = GetQueryColumn(ref memberExpression);
			while (memberExpression != null) {
				var joinTable = CreateQueryJoin(memberExpression);
				RegisterQueryJoin(joinTable, options);
				memberExpression = memberExpression.Expression as MemberExpression;
			}
			return queryColumn;
		}
		public virtual QueryJoin CreateQueryJoin(MemberExpression memberExpression) {
			var nextExpression = memberExpression.Expression;
			var mainTableName = nextExpression.Type.Name;
			var mainTableColumnName = GetMainTableColumnName(nextExpression.Type,
				memberExpression.Type, memberExpression.Member);
			var joinTableName = memberExpression.Type.Name;
			var joinTableColumnName = GetJoinTablePrimaryColumnName(memberExpression.Type);
			var joinTable = new QueryJoin(mainTableName, mainTableColumnName, joinTableName, joinTableColumnName);
			return joinTable;
		}
		protected virtual string GetJoinTablePrimaryColumnName(Type type) {
			return "Id";
		}
		protected virtual string GetMainTableColumnName(Type mainTableType, Type joinTableType, MemberInfo info) {
			if (joinTableType.IsClass) {
				return info.Name + "Id";
			}
			return info.Name;
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