using System;
using System.Linq.Expressions;
using EntityDataFramework.Core.Models.Condition;
using EntityDataFramework.Core.Models.Query.Column;
using EntityDataFramework.Core.Models.Query.Contract;

namespace EntityDataFramework.Core.Models.Query.Expression {
	public static class QueryExpressionUtilities {
		public static void AddColumn<T, TResult>(this ISelectionQuery<T> query, Expression<Func<T, TResult>> expression, string alias = null) {
			var builder = new ColumnQueryExpressionBuilder(query.GetColumnQueryExpressionOptions());
			var queryColumn = builder.Parse(expression);
			queryColumn.Alias = alias;
			query.SetQueryColumn(queryColumn);
		}
		public static void AddWhere<T>(this IFiltrationQuery<T> query, Expression<Func<T, bool>> expression) {
			var builder = new ConditionQueryExpressionBuilder(query.GetConditionQueryExpressionOptions());
			var queryCondition = builder.Parse(expression);
			query.SetQueryCondition(queryCondition);
		}
		public static ColumnQueryExpressionOptions GetColumnQueryExpressionOptions<T>(this ISelectionQuery<T> query) {
			return new ColumnQueryExpressionOptions {
				Columns = query.Columns,
				Joins = query.Joins
			};
		}
		public static ConditionQueryExpressionOptions GetConditionQueryExpressionOptions<T>(this IFiltrationQuery<T> query) {
			return new ConditionQueryExpressionOptions {
				Joins = query.Joins,
				Conditions = query.Conditions
			};
		}
		public static void SetQueryCondition(this IQueryConditionList options, IQueryCondition condition) {
			if (!options.Conditions.Exists(queryCondition => queryCondition == condition)) {
				options.Conditions.Add(condition);
			}
		}
		public static void SetQueryColumn(this IQueryColumnList options, IQueryColumn column) {
			if (!options.Columns.Exists(queryColumn => queryColumn == column)) {
				options.Columns.Add(column);
			}
		}
	}
}