using System;
using System.Text;
using EntityDataFramework.Core.Models.Condition;
using EntityDataFramework.Core.Models.Condition.Value;
using EntityDataFramework.Core.Models.Query.Builder.Contract;
using EntityDataFramework.Core.Models.Query.Contract;
using EntityDataFramework.Core.Utilities;

namespace EntityDataFramework.Core.Models.Query.Builder.Abstraction {
	public abstract class BaseConditionsQuerySqlBuilder : BaseQuerySqlBuilder, IConditionsQuerySqlBuilder {
		public IColumnQuerySqlBuilder ColumnQuerySqlBuilder { get; }
		public BaseConditionsQuerySqlBuilder(IColumnQuerySqlBuilder columnQuerySqlBuilder) {
			ColumnQuerySqlBuilder = columnQuerySqlBuilder;
		}
		public virtual void SetQueryConditionsSql(IFiltrationQuery filtrationQuery, StringBuilder stringBuilder) {
			var filtrationGroup =
				new GroupQueryCondition(ConditionLogicalOperation.And, filtrationQuery.Conditions.ToArray());
			SetQueryConditionSql(filtrationGroup, stringBuilder);
		}
		protected virtual void SetQueryConditionSql(IQueryCondition queryCondition, StringBuilder stringBuilder) {
			switch (queryCondition) {
				case ColumnValueQueryCondition columnValueQueryCondition: 
					SetColumnValueQueryCondition(columnValueQueryCondition, stringBuilder);
					break;
				case GroupQueryCondition groupQueryCondition:
					SetGroupQueryCondition(groupQueryCondition, stringBuilder);
					break;
				default:
					throw new NotImplementedException(nameof(queryCondition));

			}
		}
		protected virtual void SetColumnValueQueryCondition(ColumnValueQueryCondition condition, StringBuilder stringBuilder) {
			ColumnQuerySqlBuilder.SetQueryColumnSql(condition.Column, stringBuilder);
			stringBuilder.Append(" ");
			stringBuilder.Append(GetConditionValueSql(condition.Value, condition.ComparisonType));
		}
		protected virtual void SetGroupQueryCondition(GroupQueryCondition condition, StringBuilder stringBuilder) {
			var logicalOperatorSql = $" {GetConditionLogicalOperationSql(condition.LogicalOperation)} ";
			stringBuilder.Append("(");
			var conditions = condition.QueryConditions;
			conditions.ForEach((queryCondition, index) => {
				SetQueryConditionSql(queryCondition, stringBuilder);
				if (!conditions.GetIsLast(index)) {
					stringBuilder.Append(logicalOperatorSql);
				}
			});
			stringBuilder.Append(")");
		}
		protected virtual string GetComparisonTypeSql(ConditionComparisonType comparisonType) {
			switch (comparisonType) {
				case ConditionComparisonType.Equal:
					return GetEqualComparisonTypeSql();
				case ConditionComparisonType.NotEqual:
					return GetNotEqualComparisonTypeSql();
				case ConditionComparisonType.Contains:
					return GetContainsComparisonTypeSql();
				default:
					throw new NotImplementedException(nameof(comparisonType));
			}
		}
		protected virtual string GetEqualComparisonTypeSql() {
			return "=";
		}
		protected virtual string GetNotEqualComparisonTypeSql() {
			return "!=";
		}
		protected virtual string GetContainsComparisonTypeSql() {
			return "LIKE";
		}
		protected virtual string GetConditionValueSql(IConditionValue conditionValue, ConditionComparisonType comparisonType) {
			switch (conditionValue) {
				case GuidConditionValue guidConditionValue:
					return GetGuidConditionValueSql(guidConditionValue, comparisonType);
				case StringConditionValue stringConditionValue:
					return GetStringConditionValueSql(stringConditionValue, comparisonType);
				case NullConditionValue nullConditionValue:
					return GetNullConditionValueSql(nullConditionValue, comparisonType);
				default:
					throw new NotImplementedException(nameof(conditionValue));
			}
		}
		protected virtual string GetNullConditionValueSql(NullConditionValue nullConditionValue, ConditionComparisonType comparisonType) {
			switch (comparisonType) {
				case ConditionComparisonType.Equal:
					return "IS NULL";
				case ConditionComparisonType.NotEqual:
					return "IS NOT NULL";
				default:
					throw new NotImplementedException(nameof(comparisonType));
			}
		}
		protected virtual string GetStringConditionValueSql(StringConditionValue stringConditionValue, ConditionComparisonType comparisonType) {
			var conditionTypeSql = GetComparisonTypeSql(comparisonType);
			var value = stringConditionValue.Value;
			switch (comparisonType) {
				case ConditionComparisonType.Equal:
				case ConditionComparisonType.NotEqual:
					return $"{conditionTypeSql} '{value}'";
				case ConditionComparisonType.Contains:
					return $"{conditionTypeSql} '%{value}%'";
				default:
					throw new NotImplementedException(nameof(comparisonType));
			}
		}
		protected virtual string GetGuidConditionValueSql(GuidConditionValue guidConditionValue, ConditionComparisonType comparisonType) {
			var conditionTypeSql = GetComparisonTypeSql(comparisonType);
			var value = guidConditionValue.Value.ToString("B");
			switch (comparisonType) {
				case ConditionComparisonType.Equal:
				case ConditionComparisonType.NotEqual:
					return $"{conditionTypeSql} '{value}'";
				default:
					throw new NotImplementedException(nameof(comparisonType));
			}
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
