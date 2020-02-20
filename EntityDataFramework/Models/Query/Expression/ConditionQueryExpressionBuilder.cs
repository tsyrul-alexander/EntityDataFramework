using System;
using System.Linq;
using System.Linq.Expressions;
using EntityDataFramework.Core.Models.Condition;
using EntityDataFramework.Core.Models.Condition.Value;
using EntityDataFramework.Core.Models.Query.Column;

namespace EntityDataFramework.Core.Models.Query.Expression {
	public class ConditionQueryExpressionBuilder : BaseQueryExpressionBuilder {
		private ConditionQueryExpressionOptions Options { get; set; }
		public ConditionQueryExpressionBuilder(ConditionQueryExpressionOptions options) {
			Options = options ?? new ConditionQueryExpressionOptions();
		}
		public virtual IQueryCondition Parse(System.Linq.Expressions.Expression expression) {
			switch (expression) {
				case LambdaExpression lambdaExpression:
					return ParseLambda(lambdaExpression);
				case BlockExpression blockExpression:
					return ParseBlock(blockExpression);
				case BinaryExpression binaryExpression:
					return ParseBinary(binaryExpression);
				default:
					throw new NotImplementedException(nameof(expression));
			}
		}
		protected virtual IQueryCondition ParseLambda(LambdaExpression expression) {
			return Parse(expression.Body);
		}
		protected virtual IQueryCondition ParseBlock(BlockExpression expression) {
			return new GroupQueryCondition(ConditionLogicalOperation.And,
				expression.Expressions.Select(exp => Parse(exp)).ToArray());
		}
		protected virtual IQueryCondition ParseBinary(BinaryExpression expression) {
			switch (expression.NodeType) {
				case ExpressionType.Equal:
					return ParseEqualBinaryOperation(expression);
				case ExpressionType.NotEqual:
					return ParseNotEqualBinaryOperation(expression);
				case ExpressionType.AndAlso:
					return ParseAndAlsoBinaryOperation(expression);
				default:
					throw new NotImplementedException(nameof(expression.NodeType));
			}
		}

		protected virtual IQueryCondition ParseAndAlsoBinaryOperation(BinaryExpression expression) {
			var condition = new GroupQueryCondition(ConditionLogicalOperation.And);
			condition.QueryConditions.Add(Parse(expression.Left));
			condition.QueryConditions.Add(Parse(expression.Right));
			return condition;
		}

		protected virtual IQueryCondition ParseComparisonTypeBinaryOperation(BinaryExpression expression,
			ConditionComparisonType comparisonType) {
			var leftExp = expression.Left;
			var rightExp = expression.Right;
			if (leftExp.NodeType == ExpressionType.MemberAccess && rightExp.NodeType == ExpressionType.Constant) {
				return CreateColumnCondition(GetParseMemberColumn((MemberExpression) leftExp, Options),
					ParseConstant((ConstantExpression) rightExp), comparisonType);
			}
			if (rightExp.NodeType == ExpressionType.MemberAccess && rightExp.NodeType == ExpressionType.Constant) {
				return CreateColumnCondition(GetParseMemberColumn((MemberExpression) rightExp, Options),
					ParseConstant((ConstantExpression) leftExp), comparisonType);
			}
			throw new NotImplementedException(nameof(leftExp.NodeType));
		}
		protected virtual IConditionValue ParseConstant(ConstantExpression constantExpression) {
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
		protected virtual IQueryCondition ParseEqualBinaryOperation(BinaryExpression expression) {
			return ParseComparisonTypeBinaryOperation(expression, ConditionComparisonType.Equal);
		}

		protected virtual IQueryCondition ParseNotEqualBinaryOperation(BinaryExpression expression) {
			return ParseComparisonTypeBinaryOperation(expression, ConditionComparisonType.NotEqual);
		}
		protected virtual IQueryCondition CreateColumnCondition(IQueryColumn queryColumn,
			IConditionValue conditionValue, ConditionComparisonType comparisonType) {
			return new ColumnValueQueryCondition(queryColumn, conditionValue, comparisonType);
		}
	}
}