using System;
using System.Text;
using EntityDataFramework.Core.Models.Query.Builder.Contract;
using EntityDataFramework.Core.Models.Query.Column;
using EntityDataFramework.Core.Models.Query.Contract;
using EntityDataFramework.Core.Utilities;

namespace EntityDataFramework.Core.Models.Query.Builder.Abstraction {
	public abstract class BaseColumnsQuerySqlBuilder : BaseQuerySqlBuilder, IColumnsQuerySqlBuilder, IColumnQuerySqlBuilder {
		protected virtual string AllColumnSymbol => "*";
		public virtual void SetQueryColumnsSql(ISelectionQuery selectQuery, StringBuilder stringBuilder) {
			if (selectQuery.UseAllSchemaColumns) {
				SetAllColumnSql(selectQuery, stringBuilder);
			}
			var columns = selectQuery.Columns;
			for (var i = 0; i < columns.Count; i++) {
				var column = columns[i];
				SetQueryColumnSql(column, stringBuilder);
				if (!columns.GetIsLast(i)) {
					stringBuilder.Append(ColumnSeparator);
				}
			}
		}
		public virtual string GetQueryColumnSql(IQueryColumn queryColumn) {
			switch (queryColumn) {
				case QueryColumn queryColumnValue:
					return GetQueryColumnSql(queryColumnValue);
				case QueryFunctionColumn queryFunctionColumn:
					return GetQueryColumnSql(queryFunctionColumn);
				default:
					throw new NotImplementedException(nameof(queryColumn));
			}
		}
		protected virtual void SetQueryColumnSql(IQueryColumn queryColumn, StringBuilder stringBuilder) {
			var queryColumnSql = GetQueryColumnSql(queryColumn);
			stringBuilder.Append($"{queryColumnSql} AS {queryColumn.Alias}");
		}
		protected virtual void SetAllColumnSql(ISelectionQuery selectQuery, StringBuilder stringBuilder) {
			var isEmptyColumns = selectQuery.Columns.GetIsEmpty();
			var tableName = selectQuery.SchemaName;
			var tableFormat = GetTableFormat(tableName);
			var separateStr = isEmptyColumns ? string.Empty : ColumnSeparator;
			stringBuilder.Append($"{tableFormat}.{AllColumnSymbol} {separateStr}");
		}
		protected virtual string GetQueryColumnSql(QueryColumn queryColumn) {
			var tableFormat = GetTableFormat(queryColumn.TableName);
			var columnFormat = GetColumnFormat(queryColumn.Name);
			return $"{tableFormat}.{columnFormat}";
		}
		protected virtual string GetQueryColumnSql(QueryFunctionColumn queryFunctionColumn) {
			switch (queryFunctionColumn.FunctionType) {
				case QueryFunctionType.Count:
					return GetCountQueryFunctionColumn(queryFunctionColumn);
				case QueryFunctionType.Sum:
					return GetSumQueryFunctionColumn(queryFunctionColumn);
				default:
					throw new NotImplementedException(nameof(queryFunctionColumn.FunctionType));
			}
		}
		protected virtual string GetCountQueryFunctionColumn(QueryFunctionColumn queryFunctionColumn) {
			var countFunctionName = GetCountQueryFunctionName();
			return GetFunctionCallSql(countFunctionName);
		}
		protected virtual string GetSumQueryFunctionColumn(QueryFunctionColumn queryFunctionColumn) {
			var sumFunctionName = GetSumQueryFunctionName();
			return GetFunctionCallSql(sumFunctionName);
		}
		protected virtual string GetFunctionCallSql(string functionName, params string[] args) {
			return $"{functionName}({args.JoinStr(ColumnSeparator)})";
		}
		protected virtual string GetCountQueryFunctionName() {
			return "Count";
		}
		protected virtual string GetSumQueryFunctionName() {
			return "Sum";
		}
	}
}