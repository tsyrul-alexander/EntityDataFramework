using System;
using System.Linq;
using System.Linq.Expressions;
using EntityDataFramework.Core.Models.Condition.Value;
using EntityDataFramework.Core.Models.Query;

namespace EntityDataFramework.Core.Models.Condition
{
	public static class ExpressionConditionManager {
		public static void AddColumn<TQuery, T, TResult>(this TQuery query, Expression<Func<T, TResult>> expression)
			where TQuery: IQueryColumnList, IQueryJoinList {
			var options = new ExpressionConditionOptions(query.Joins, query.Columns);
			ParseColumn(expression, options);
		}
		public static void AddWhere<TMain>(this IQueryJoinList joinList, Expression<Func<TMain, bool>> expression) {
			var options = new ExpressionConditionOptions(joinList.Joins);
			var condition = ParseCondition(expression, options);
			RegisterQueryCondition(condition, options);
		}
		public static void ParseColumn(Expression expression, ExpressionConditionOptions options) {
			switch (expression) {
				case LambdaExpression lambdaExpression:
					ParseLambda(lambdaExpression, options);
					break;
				case BinaryExpression binaryExpression:
					ParseBinaryColumn(binaryExpression, options);
					break;
				case MemberExpression memberExpression:
					ParseMemberColumn(memberExpression, options);
					break;
				case ConstantExpression constantExpression:
					ParseConstant(constantExpression, options);
					break;
				default:
					throw new NotImplementedException(nameof(expression));
			}
		}
		public static IQueryCondition ParseCondition(Expression expression, ExpressionConditionOptions options) {
			switch (expression) {
				case LambdaExpression lambdaExpression:
					return ParseLambdaCondition(lambdaExpression, options);
				case BlockExpression blockExpression:
					return ParseBlockCondition(blockExpression, options);
				case BinaryExpression binaryExpression:
					return ParseBinaryCondition(binaryExpression, options);
				default:
					throw new NotImplementedException(nameof(expression));
			}
		}
		public static IConditionValue ParseConstant(ConstantExpression constantExpression, ExpressionConditionOptions options) {
			if (constantExpression.Value is Guid guidValue) {
				return new GuidConditionValue(guidValue);
			} 
			if (constantExpression.Value is string stringValue) {
				return new StringConditionValue(stringValue);
			}
			if (constantExpression.Value == null) {
				return new NullConditionValue();
			}
			throw new NotImplementedException(nameof(constantExpression.Value));
		}
		public static void ParseMemberColumn(MemberExpression memberExpression, ExpressionConditionOptions options) {
			var queryColumn = GetParseMemberColumn(memberExpression, options);
			RegisterQueryColumn(queryColumn, options);
		}
		public static QueryColumn GetParseMemberColumn(MemberExpression memberExpression, ExpressionConditionOptions options) {
			var queryColumn = GetQueryColumn(ref memberExpression, options);
			while (memberExpression != null) {
				var tableName = memberExpression.Expression.Type.Name;
				var columnName = memberExpression.Member.Name;
				var joinTable = new QueryJoin(tableName, columnName);
				RegisterQueryJoin(joinTable, options);
				memberExpression = memberExpression.Expression as MemberExpression;
			}
			return queryColumn;
		}
		private static QueryColumn GetQueryColumn(ref MemberExpression memberExpression, ExpressionConditionOptions options) {
			var tableName = memberExpression.Expression.Type.Name;
			var columnName = memberExpression.Member.Name;
			memberExpression = memberExpression.Expression as MemberExpression;
			var queryColumn = new QueryColumn(tableName, columnName);
			return queryColumn;
		}
		public static void RegisterQueryJoin(QueryJoin join, ExpressionConditionOptions options) {
			if (!options.Joins.Exists(queryJoin => queryJoin == join)) {
				options.Joins.Add(join);
			}
		}
		public static void RegisterQueryCondition(IQueryCondition condition, ExpressionConditionOptions options) {
			if (!options.Conditions.Exists(queryCondition => queryCondition == condition)) {
				options.Conditions.Add(condition);
			}
		}
		public static void RegisterQueryColumn(QueryColumn column, ExpressionConditionOptions options) {
			if (!options.Columns.Exists(queryColumn => queryColumn == column)) {
				options.Columns.Add(column);
			}
		}
		public static IQueryCondition ParseBlockCondition(BlockExpression expression, ExpressionConditionOptions options) {
			return new GroupQueryCondition(ConditionLogicalOperation.And, expression.Expressions.Select(exp => ParseCondition(exp, options)).ToArray());
		}
		public static void ParseLambda(LambdaExpression expression, ExpressionConditionOptions options) {
			ParseColumn(expression.Body, options);
		}
		public static IQueryCondition ParseLambdaCondition(LambdaExpression expression, ExpressionConditionOptions options) {
			return ParseCondition(expression.Body, options);
		}
		public static void ParseBinaryColumn(BinaryExpression expression, ExpressionConditionOptions options) {
			switch (expression.NodeType) {
				default:
					throw new NotImplementedException(nameof(expression.NodeType));
			}
		}
		public static IQueryCondition ParseBinaryCondition(BinaryExpression expression, ExpressionConditionOptions options) {
			switch (expression.NodeType) {
				case ExpressionType.Equal:
					return ParseEqualBinaryOperation(expression, options);
				case ExpressionType.NotEqual:
					return ParseNotEqualBinaryOperation(expression, options);
				case ExpressionType.AndAlso:
					return ParseAndAlsoBinaryOperation(expression, options);
				default:
					throw new NotImplementedException(nameof(expression.NodeType));
			}
		}

		public static IQueryCondition ParseAndAlsoBinaryOperation(BinaryExpression expression, ExpressionConditionOptions options) {
			var condition = new GroupQueryCondition(ConditionLogicalOperation.And);
			condition.QueryConditions.Add(ParseCondition(expression.Left, options));
			condition.QueryConditions.Add(ParseCondition(expression.Right, options));
			return condition;
		}

		public static IQueryCondition ParseComparisonTypeBinaryOperation(BinaryExpression expression,
				ConditionComparisonType comparisonType, ExpressionConditionOptions options) {
			var leftExp = expression.Left;
			var rightExp = expression.Right;
			if (leftExp.NodeType == ExpressionType.MemberAccess && rightExp.NodeType == ExpressionType.Constant) {
				return CreateColumnCondition(GetParseMemberColumn((MemberExpression)leftExp, options),
					ParseConstant((ConstantExpression)rightExp, options), comparisonType, options);
			}
			if (rightExp.NodeType == ExpressionType.MemberAccess && rightExp.NodeType == ExpressionType.Constant) {
				return CreateColumnCondition(GetParseMemberColumn((MemberExpression)rightExp, options),
					ParseConstant((ConstantExpression)leftExp, options), comparisonType, options);
			}
			throw new NotImplementedException(nameof(leftExp.NodeType));
		}

		public static IQueryCondition ParseEqualBinaryOperation(BinaryExpression expression, ExpressionConditionOptions options) {
			return ParseComparisonTypeBinaryOperation(expression, ConditionComparisonType.Equal, options);
		}

		public static IQueryCondition ParseNotEqualBinaryOperation(BinaryExpression expression, ExpressionConditionOptions options) {
			return ParseComparisonTypeBinaryOperation(expression, ConditionComparisonType.NotEqual, options);
		}
		public static IQueryCondition CreateColumnCondition(QueryColumn queryColumn, IConditionValue conditionValue,
			ConditionComparisonType comparisonType, ExpressionConditionOptions options) {
			return new ColumnValueQueryCondition(queryColumn, conditionValue);
		}
		
		public static bool GetIsColumn(Type type) {
			var isColumnType = false;
			if (type == typeof(Guid))
				isColumnType = true;
			else if (type == typeof(string))
				isColumnType = true;
			else if (type == typeof(int))
				isColumnType = true;
			return isColumnType;
		}
	}
}
