using System;
using System.Text;
using EntityDataFramework.Core.Models.Query.Builder.Contract;
using EntityDataFramework.Core.Models.Query.Column;
using EntityDataFramework.Core.Models.Query.Contract;
using EntityDataFramework.Core.Utilities;

namespace EntityDataFramework.Core.Models.Query.Builder.Abstraction {
	public abstract class BaseColumnsQuerySqlBuilder : BaseQuerySqlBuilder, IColumnsQuerySqlBuilder, IColumnQuerySqlBuilder {
		public virtual string AllColumnSymbol => "*";
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
		public virtual void SetQueryColumnSql(IQueryColumn queryColumn, StringBuilder stringBuilder) {
			switch (queryColumn) {
				case QueryColumn queryColumnValue:
					SetQueryColumnSql(queryColumnValue, stringBuilder);
					break;
				case QueryFunctionColumn queryFunctionColumn:
					SetQueryColumnSql(queryFunctionColumn, stringBuilder);
					break;
				default:
					throw new NotImplementedException(nameof(queryColumn));
			}
		}
		protected virtual void SetAllColumnSql(ISelectionQuery selectQuery, StringBuilder stringBuilder) {
			var isEmptyColumns = selectQuery.Columns.GetIsEmpty();
			var tableName = selectQuery.SchemaName;
			var tableFormat = GetTableFormat(tableName);
			var separateStr = isEmptyColumns ? string.Empty : ColumnSeparator;
			stringBuilder.Append($"{tableFormat}.{AllColumnSymbol} {separateStr}");
		}
		protected virtual void SetQueryColumnSql(QueryColumn queryColumn, StringBuilder stringBuilder) {
			var tableFormat = GetTableFormat(queryColumn.TableName);
			var columnFormat = GetColumnFormat(queryColumn.Name);
			stringBuilder.AppendFormat("{0}.{1}", tableFormat, columnFormat);
		}
		protected virtual void SetQueryColumnSql(QueryFunctionColumn queryFunctionColumn, StringBuilder stringBuilder) {
			switch (queryFunctionColumn.FunctionType) {
				case QueryFunctionType.Count:
					SetCountQueryFunctionColumn(queryFunctionColumn, stringBuilder);
					break;
				case QueryFunctionType.Sum:
					SetSumQueryFunctionColumn(queryFunctionColumn, stringBuilder);
					break;
				default:
					throw new NotImplementedException(nameof(queryFunctionColumn.FunctionType));
			}
		}
		protected virtual void SetCountQueryFunctionColumn(QueryFunctionColumn queryFunctionColumn,
			StringBuilder stringBuilder) {
			var countFunctionName = GetCountQueryFunctionName();
			stringBuilder.Append(GetFunctionCallSql(countFunctionName));
		}
		protected virtual void SetSumQueryFunctionColumn(QueryFunctionColumn queryFunctionColumn,
			StringBuilder stringBuilder) { }
		protected virtual string GetFunctionCallSql(string functionName, params string[] args) {
			return $"{functionName}({args.JoinStr(ColumnSeparator)})";
		}
		protected abstract string GetCountQueryFunctionName();
	}
}