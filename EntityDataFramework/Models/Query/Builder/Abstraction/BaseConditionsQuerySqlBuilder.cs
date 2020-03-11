using System;
using System.Text;
using EntityDataFramework.Core.Models.Condition;
using EntityDataFramework.Core.Models.Condition.Value;
using EntityDataFramework.Core.Models.Query.Builder.Contract;
using EntityDataFramework.Core.Models.Query.Contract;
using EntityDataFramework.Core.Utilities;

namespace EntityDataFramework.Core.Models.Query.Builder.Abstraction {
	public abstract class BaseConditionsQuerySqlBuilder : BaseQuerySqlBuilder, IConditionsQuerySqlBuilder, IConditionQuerySqlBuilder {
		protected virtual string WhereCommandName => "WHERE";
		public IColumnQuerySqlBuilder ColumnQuerySqlBuilder { get; }
		public BaseConditionsQuerySqlBuilder(IColumnQuerySqlBuilder columnQuerySqlBuilder) {
			ColumnQuerySqlBuilder = columnQuerySqlBuilder;
		}
		public virtual void SetQueryConditionsSql(IFiltrationQuery filtrationQuery, StringBuilder stringBuilder) {
			if (filtrationQuery.Conditions.GetIsEmpty()) {
				return;
			}
			stringBuilder.Append($"{WhereCommandName} ");
			var filtrationGroup =
				new GroupQueryCondition(ConditionLogicalOperation.And, filtrationQuery.Conditions.ToArray());
			SetQueryConditionSql(filtrationGroup, stringBuilder);
		}
		protected virtual void SetQueryConditionSql(IQueryCondition queryCondition, StringBuilder stringBuilder) {
			var conditionSql = GetQueryConditionSql(queryCondition);
			stringBuilder.Append(conditionSql);
		}
		public virtual string GetQueryConditionSql(IQueryCondition queryCondition) {
			switch (queryCondition) {
				case BinaryQueryCondition binaryQueryCondition:
					return GetBinaryQueryCondition(binaryQueryCondition);
				case ColumnQueryCondition columnQueryCondition:
					return GetColumnQueryCondition(columnQueryCondition);
				case GroupQueryCondition groupQueryCondition:
					return GetGroupQueryCondition(groupQueryCondition);
				case ConstantQueryCondition constantQueryCondition:
					return GetConstantQueryCondition(constantQueryCondition);
				default:
					throw new NotImplementedException(nameof(queryCondition));

			}
		}
		protected virtual string GetConstantQueryCondition(ConstantQueryCondition condition) {
			return GetConditionValueSql(condition.ConstantValue);
		}
		protected virtual string GetBinaryQueryCondition(BinaryQueryCondition condition) {
			var leftConditionSql = GetQueryConditionSql(condition.LeftCondition);
			var rightConditionSql = GetQueryConditionSql(condition.RightCondition);
			return GetComparisonTypeSql(condition.ComparisonType, leftConditionSql, rightConditionSql);
		}
		protected virtual string GetColumnQueryCondition(ColumnQueryCondition condition) {
			return ColumnQuerySqlBuilder.GetQueryColumnSql(condition.QueryColumn);
		}
		protected virtual string GetGroupQueryCondition(GroupQueryCondition condition) {
			var logicalOperatorSql = $" {GetConditionLogicalOperationSql(condition.LogicalOperation)} ";
			var str = string.Empty;
			str += "(";
			var conditions = condition.QueryConditions;
			conditions.ForEach((queryCondition, index) => {
				str += GetQueryConditionSql(queryCondition);
				if (!conditions.GetIsLast(index)) {
					str += logicalOperatorSql;
				}
			});
			str +=")";
			return str;
		}
		protected virtual string GetComparisonTypeSql(ConditionComparisonType comparisonType,
				string leftExpression, string rightExpression) {
			switch (comparisonType) {
				case ConditionComparisonType.Equal:
					return GetEqualComparisonTypeSql(leftExpression, rightExpression);
				case ConditionComparisonType.NotEqual:
					return GetNotEqualComparisonTypeSql(leftExpression, rightExpression);
				case ConditionComparisonType.Contains:
					return GetContainsComparisonTypeSql(leftExpression, rightExpression);
				default:
					throw new NotImplementedException(nameof(comparisonType));
			}
		}
		protected virtual string GetEqualComparisonTypeSql(string leftExpression, string rightExpression) {
			return $"{leftExpression} = {rightExpression}";
		}
		protected virtual string GetNotEqualComparisonTypeSql(string leftExpression, string rightExpression) {
			return $"{leftExpression} != {rightExpression}";
		}
		protected virtual string GetContainsComparisonTypeSql(string leftExpression, string rightExpression) {
			return $"{leftExpression} LIKE '%{rightExpression}%'";
		}
		protected virtual string GetConditionValueSql(IConditionConstantValue conditionConstantValue) {
			switch (conditionConstantValue) {
				case GuidConditionConstantValue guidConditionValue:
					return GetGuidConditionValueSql(guidConditionValue);
				case StringConditionConstantValue stringConditionValue:
					return GetStringConditionValueSql(stringConditionValue);
				case NullConditionConstantValue nullConditionValue:
					return GetNullConditionValueSql(nullConditionValue);
				default:
					throw new NotImplementedException(nameof(conditionConstantValue));
			}
		}
		protected virtual string GetNullConditionValueSql(NullConditionConstantValue nullConditionConstantValue) {
			return "NULL";
		}
		protected virtual string GetStringConditionValueSql(StringConditionConstantValue stringConditionConstantValue) {
			return $"'{stringConditionConstantValue.Value}'";
		}
		protected virtual string GetGuidConditionValueSql(GuidConditionConstantValue guidConditionConstantValue) {
			return $"'{guidConditionConstantValue.Value:B}'";
		}
		protected virtual string GetConditionLogicalOperationSql(ConditionLogicalOperation logicalOperation) {
			switch (logicalOperation) {
				case ConditionLogicalOperation.And:
					return "AND";
				case ConditionLogicalOperation.Or:
					return "OR";
				default:
					throw new NotImplementedException(nameof(logicalOperation));
			}
		}
	}
}
