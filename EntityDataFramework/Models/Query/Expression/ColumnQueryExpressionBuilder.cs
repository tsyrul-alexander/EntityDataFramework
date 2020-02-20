using System;
using System.Linq.Expressions;
using EntityDataFramework.Core.Models.Query.Column;

namespace EntityDataFramework.Core.Models.Query.Expression {
	public class ColumnQueryExpressionBuilder : BaseQueryExpressionBuilder {
		public ColumnQueryExpressionOptions Options { get; }
		public ColumnQueryExpressionBuilder(ColumnQueryExpressionOptions options) {
			Options = options ?? new ColumnQueryExpressionOptions();
		}
		public virtual IQueryColumn Parse(System.Linq.Expressions.Expression expression) {
			switch (expression) {
				case LambdaExpression lambdaExpression:
					return ParseLambda(lambdaExpression);
				case BinaryExpression binaryExpression:
					return ParseBinary(binaryExpression);
				case MemberExpression memberExpression:
					return ParseMember(memberExpression);
				case ConstantExpression constantExpression:
					return ParseConstant(constantExpression);
				default:
					throw new NotImplementedException(nameof(expression));
			}
		}
		public virtual IQueryColumn ParseLambda(LambdaExpression expression) {
			return Parse(expression.Body);
		}
		public virtual IQueryColumn ParseBinary(BinaryExpression expression) {
			throw new NotImplementedException();
		}
		public virtual IQueryColumn ParseMember(MemberExpression memberExpression) {
			var queryColumn = GetParseMemberColumn(memberExpression, Options);
			return queryColumn;
		}
		public virtual IQueryColumn ParseConstant(ConstantExpression constantExpression) {
			throw new NotImplementedException();
		}
	}
}