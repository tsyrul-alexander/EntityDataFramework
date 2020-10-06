using System;
using System.Linq.Expressions;
using System.Reflection;
using EntityDataFramework.Core.Models.Condition;
using EntityDataFramework.Core.Models.Query.Column;
using EntityDataFramework.Core.Models.Query.Contract;
using EntityDataFramework.Core.Models.Query.Join;
using EntityDataFramework.Core.Utilities;

namespace EntityDataFramework.Core.Models.Query.Expression {
	public abstract class BaseQueryExpressionBuilder {
		protected virtual IQueryColumn GetParseMemberColumn(MemberExpression memberExpression, IQueryJoinList options) {
			var queryColumn = GetQueryColumn(ref memberExpression);
			while (memberExpression != null) {
				var joinTable = CreateQueryJoin(memberExpression);
				RegisterQueryJoin(joinTable, options);
				memberExpression = memberExpression.Expression as MemberExpression;
			}
			return queryColumn;
		}
		protected virtual QueryJoin CreateQueryJoin(MemberExpression memberExpression) {
			var nextExpression = memberExpression.Expression;
			var mainTableName = nextExpression.Type.Name;
			var mainTableColumnName = GetMainTableColumnName(nextExpression.Type,
				memberExpression.Type, memberExpression.Member);
			var joinTableName = memberExpression.Type.Name;
			var joinTableColumnName = GetJoinTablePrimaryColumnName(memberExpression.Type);
			var joinTable = CreateQueryJoin(joinTableName, joinTableColumnName, mainTableName, mainTableColumnName);
			return joinTable;
		}
		protected virtual QueryJoin CreateQueryJoin(string joinTableName, string joinTableColumnName,
				string mainTableName, string mainTableColumnName) {
			var joinTable = new QueryJoin(joinTableName, new BinaryQueryCondition {
				ComparisonType = ConditionComparisonType.Equal,
				LeftCondition = new ColumnQueryCondition(new QueryColumn(joinTableName, joinTableColumnName)),
				RightCondition = new ColumnQueryCondition(new QueryColumn(mainTableName, mainTableColumnName))
			});
			return joinTable;
		}
		protected virtual string GetJoinTablePrimaryColumnName(Type type) {
			return type.GetKey();
		}
		protected virtual string GetMainTableColumnName(Type mainTableType, Type joinTableType, MemberInfo info) {
			if (joinTableType.IsClass) {
				return info.Name + joinTableType.GetKey();
			}
			return info.Name;
		}
		protected virtual QueryColumn GetQueryColumn(ref MemberExpression memberExpression) {
			var columnName = memberExpression.Member.Name;
			var tableName = memberExpression.Expression.Type.Name;
			if (memberExpression.Expression is MemberExpression nextMemberExpression && GetIsPrimaryColumn(memberExpression.Member)) {
				tableName = nextMemberExpression.Expression.Type.Name;
				columnName = nextMemberExpression.Member.Name + columnName;
			}
			memberExpression = memberExpression.Expression as MemberExpression;
			var queryColumn = new QueryColumn(tableName, columnName);
			return queryColumn;
		}
		protected virtual void RegisterQueryJoin(QueryJoin join, IQueryJoinList options) {
			if (!options.Joins.Exists(queryJoin => queryJoin == join)) {
				options.Joins.Add(join);
			}
		}
		protected virtual bool GetIsPrimaryColumn(MemberInfo memberInfo) {
			return memberInfo.GetIsKey();
		}
	}
}