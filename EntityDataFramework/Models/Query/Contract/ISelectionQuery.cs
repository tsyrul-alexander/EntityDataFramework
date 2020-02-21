namespace EntityDataFramework.Core.Models.Query.Contract {
	public interface ISelectionQuery : ISchemaQuery, IQueryColumnList, IQueryJoinList {
		bool UseAllSchemaColumns { get; set; }
	}
}