namespace EntityDataFramework.Core.Models.Query.Column {
	public abstract class QueryFunctionColumn : IQueryColumn {
		public string Alias { get; set; }
	}
}