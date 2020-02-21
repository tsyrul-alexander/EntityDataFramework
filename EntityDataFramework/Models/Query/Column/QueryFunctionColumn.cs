using System;

namespace EntityDataFramework.Core.Models.Query.Column {
	public class QueryFunctionColumn : IQueryColumn {
		public QueryFunctionType FunctionType { get; }
		public IQueryColumn QueryColumn { get; }
		public QueryFunctionColumn(QueryFunctionType functionType, IQueryColumn queryColumn) {
			FunctionType = functionType;
			QueryColumn = queryColumn;
		}
		public string GetSqlText() {
			throw new NotImplementedException();
		}
	}
}