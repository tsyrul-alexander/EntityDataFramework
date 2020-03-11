using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using EntityDataFramework.Core.Models.Condition;
using EntityDataFramework.Core.Models.Condition.Value;
using EntityDataFramework.Core.Models.Query.Expression.Contract;

namespace EntityDataFramework.Core.Models.Query.Expression {
	public class ConditionQueryExpressionBuilder : BaseQueryExpressionBuilder, IConditionQueryExpressionBuilder {
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
				case MemberExpression memberExpression when memberExpression.Member is FieldInfo fieldInfo && fieldInfo.IsStatic:
					return ParseStatic(memberExpression);
				case MemberExpression memberAccess:
					return ParseMember(memberAccess);
				case ConstantExpression constantExpression:
					return ParseConstant(constantExpression);
				default:
					throw new NotImplementedException(nameof(expression));
			}
		}
		private IQueryCondition ParseStatic(MemberExpression memberExpression) {
			var field = (FieldInfo)memberExpression.Member;
			var constantValue = field.GetValue(null);
			return CreateConstValue(constantValue);
		}
		protected virtual IQueryCondition ParseMember(MemberExpression memberAccess) {
			return new ColumnQueryCondition(GetParseMemberColumn(memberAccess, Options));
		}
		protected virtual IQueryCondition ParseConstant(ConstantExpression constantExpression) {
			return CreateConstValue(constantExpression.Value);
		}
		protected virtual IQueryCondition ParseLambda(LambdaExpression expression) {
			return Parse(expression.Body);
		}
		protected virtual IQueryCondition ParseBlock(BlockExpression expression) {
			return new GroupQueryCondition(ConditionLogicalOperation.And,
				expression.Expressions.Select(Parse).ToArray());
		}
		protected virtual IQueryCondition ParseBinary(BinaryExpression expression) {
			switch (expression.NodeType) {
				case ExpressionType.Equal:
					return ParseEqualBinaryOperation(expression);
				case ExpressionType.NotEqual:
					return ParseNotEqualBinaryOperation(expression);
				case ExpressionType.AndAlso:
					return ParseAndAlsoBinaryOperation(expression);
				case ExpressionType.OrElse:
					return ParseOrElseBinaryOperation(expression);
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

		protected virtual IQueryCondition ParseOrElseBinaryOperation(BinaryExpression expression) {
			var condition = new GroupQueryCondition(ConditionLogicalOperation.Or);
			condition.QueryConditions.Add(Parse(expression.Left));
			condition.QueryConditions.Add(Parse(expression.Right));
			return condition;
		}

		protected virtual IQueryCondition ParseComparisonTypeBinaryOperation(BinaryExpression expression,
				ConditionComparisonType comparisonType) {
			var leftExp = expression.Left;
			var rightExp = expression.Right;
			return new BinaryQueryCondition(Parse(leftExp), Parse(rightExp), comparisonType);
		}
		protected virtual IQueryCondition CreateConstValue(object value) {
			return new ConstantQueryCondition(GetConstantValue(value));
		}
		protected virtual IConditionConstantValue ParseConstantValue(ConstantExpression constantExpression) {
			return GetConstantValue(constantExpression.Value);
		}
		protected virtual IConditionConstantValue GetConstantValue(object value) {
			switch (value) {
				case Guid guidValue:
					return new GuidConditionConstantValue(guidValue);
				case string stringValue:
					return new StringConditionConstantValue(stringValue);
				case null:
					return new NullConditionConstantValue();
				default:
					throw new NotImplementedException(nameof(value));
			}
		}
		protected virtual IQueryCondition ParseEqualBinaryOperation(BinaryExpression expression) {
			return ParseComparisonTypeBinaryOperation(expression, ConditionComparisonType.Equal);
		}

		protected virtual IQueryCondition ParseNotEqualBinaryOperation(BinaryExpression expression) {
			return ParseComparisonTypeBinaryOperation(expression, ConditionComparisonType.NotEqual);
		}
	}
}