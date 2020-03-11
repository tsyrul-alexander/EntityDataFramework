using System;
using System.Linq.Expressions;
using EntityDataFramework.Core.Models.Condition;
using EntityDataFramework.Core.Models.Query.Column;
using EntityDataFramework.Core.Models.Query.Contract;
using EntityDataFramework.Core.Models.Query.Expression.Contract;

namespace EntityDataFramework.Core.Models.Query.Expression {
	public static class QueryExpressionUtilities {
		public static void AddAggregatedColumn<T, TResult>(this ISelectionQuery<T> query, Expression<Func<T, TResult>> expression, QueryAggregationFunctionType aggregationFunctionType, string alias = null) {
			var builder = GetColumnQueryExpressionBuilder(query.GetColumnQueryExpressionOptions());
			var queryColumn = builder.Parse(expression);
			var aggregatedColumn = new QueryAggregationFunctionColumn(aggregationFunctionType, queryColumn) {
				Alias = alias
			};
			query.SetQueryColumn(aggregatedColumn);
		}
		public static void AddColumn<T, TResult>(this ISelectionQuery<T> query, Expression<Func<T, TResult>> expression, string alias = null) {
			var builder = GetColumnQueryExpressionBuilder(query.GetColumnQueryExpressionOptions());
			var queryColumn = builder.Parse(expression);
			queryColumn.Alias = alias;
			query.SetQueryColumn(queryColumn);
		}
		public static void AddLocalizedColumn<T>(this ISelectionQuery<T> query, Expression<Func<T, string>> expression, string alias = null) {
			var builder = GetColumnQueryExpressionBuilder(query.GetColumnQueryExpressionOptions());
			var queryColumn = builder.Parse(expression);
			queryColumn.Alias = alias;
			query.SetQueryColumn(queryColumn);
		}
		public static void AddJoin<TMain, TJoin>(this IQueryJoinList query, Expression<Func<TMain, TJoin, bool>> expression) {
			var joinTable = typeof(TJoin).Name;
			var builder = GetJoinQueryExpressionBuilder(query);
			var join = builder.Parse(joinTable, expression);
			query.Joins.Add(join);

		}
		public static void AddWhere<T>(this IFiltrationQuery<T> query, Expression<Func<T, bool>> expression) {
			var builder = GetConditionQueryExpressionBuilder(query.GetConditionQueryExpressionOptions());
			var queryCondition = builder.Parse(expression);
			query.SetQueryCondition(queryCondition);
		}
		public static IJoinQueryExpressionBuilder GetJoinQueryExpressionBuilder(this IQueryJoinList joinList) {
			var conditionBuilder = GetConditionQueryExpressionBuilder(new ConditionQueryExpressionOptions() {
				Joins = joinList.Joins
			});
			return new JoinQueryExpressionBuilder(conditionBuilder, new JoinQueryExpressionOptions(joinList.Joins));
		}
		public static IColumnQueryExpressionBuilder GetColumnQueryExpressionBuilder(ColumnQueryExpressionOptions options) {
			return new ColumnQueryExpressionBuilder(options);
		}
		public static IConditionQueryExpressionBuilder GetConditionQueryExpressionBuilder(ConditionQueryExpressionOptions options) {
			return new ConditionQueryExpressionBuilder(options);
		}
		public static ColumnQueryExpressionOptions GetColumnQueryExpressionOptions(this ISelectionQuery query) {
			return new ColumnQueryExpressionOptions {
				Columns = query.Columns,
				Joins = query.Joins
			};
		}
		public static ConditionQueryExpressionOptions GetConditionQueryExpressionOptions(this IFiltrationQuery query) {
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