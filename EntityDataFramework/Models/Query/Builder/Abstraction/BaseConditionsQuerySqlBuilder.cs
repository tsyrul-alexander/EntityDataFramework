using System;
using System.Text;
using EntityDataFramework.Core.Models.Condition;
using EntityDataFramework.Core.Models.Condition.Value;
using EntityDataFramework.Core.Models.Query.Builder.Contract;
using EntityDataFramework.Core.Models.Query.Contract;
using EntityDataFramework.Core.Utilities;

namespace EntityDataFramework.Core.Models.Query.Builder.Abstraction {
	public abstract class BaseConditionsQuerySqlBuilder : BaseQuerySqlBuilder, IConditionsQuerySqlBuilder {
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
			switch (queryCondition) {
				case ColumnValueQueryCondition columnValueQueryCondition: 
					SetColumnValueQueryCondition(columnValueQueryCondition, stringBuilder);
					break;
				case ColumnsQueryCondition columnsQueryCondition:
					SetColumnsQueryCondition(columnsQueryCondition, stringBuilder);
					break;
				case GroupQueryCondition groupQueryCondition:
					SetGroupQueryCondition(groupQueryCondition, stringBuilder);
					break;
				default:
					throw new NotImplementedException(nameof(queryCondition));

			}
		}
		protected virtual void SetColumnsQueryCondition(ColumnsQueryCondition condition, StringBuilder stringBuilder) {
			var column1Sql = ColumnQuerySqlBuilder.GetQueryColumnSql(condition.QueryColumn1);
			var column2Sql = ColumnQuerySqlBuilder.GetQueryColumnSql(condition.QueryColumn2);
			stringBuilder.Append(GetComparisonTypeSql(condition.ComparisonType, column1Sql, column2Sql));
		}
		protected virtual void SetColumnValueQueryCondition(ColumnValueQueryCondition condition, StringBuilder stringBuilder) {
			var columnSql = ColumnQuerySqlBuilder.GetQueryColumnSql(condition.Column);
			var valueSql = GetConditionValueSql(condition.Value);
			stringBuilder.Append(GetComparisonTypeSql(condition.ComparisonType, columnSql, valueSql));
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
		protected virtual string GetComparisonTypeSql(ConditionComparisonType comparisonType, string leftExpression, string rightExpression) {
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
		protected virtual string GetConditionValueSql(IConditionValue conditionValue) {
			switch (conditionValue) {
				case GuidConditionValue guidConditionValue:
					return GetGuidConditionValueSql(guidConditionValue);
				case StringConditionValue stringConditionValue:
					return GetStringConditionValueSql(stringConditionValue);
				case NullConditionValue nullConditionValue:
					return GetNullConditionValueSql(nullConditionValue);
				default:
					throw new NotImplementedException(nameof(conditionValue));
			}
		}
		protected virtual string GetNullConditionValueSql(NullConditionValue nullConditionValue) {
			return "NULL";
		}
		protected virtual string GetStringConditionValueSql(StringConditionValue stringConditionValue) {
			return $"'{stringConditionValue.Value}'";
		}
		protected virtual string GetGuidConditionValueSql(GuidConditionValue guidConditionValue) {
			return guidConditionValue.Value.ToString("B");
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
