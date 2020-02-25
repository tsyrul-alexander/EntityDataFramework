namespace EntityDataFramework.Core.Models.Query.Column {
	public class QueryFunctionColumn : IQueryColumn {
		public string Alias {
			get => QueryColumn.Alias;
			set => QueryColumn.Alias = value;
		}
		public QueryFunctionType FunctionType { get; }
		public IQueryColumn QueryColumn { get; }
		public QueryFunctionColumn(QueryFunctionType functionType, IQueryColumn queryColumn) {
			FunctionType = functionType;
			QueryColumn = queryColumn;
		}
		
	}
}