namespace EntityDataFramework.Core.Models.Query.Contract {
	public interface ISelectionQuery<T> : ISchemaQuery<T>, IQueryColumnList, IQueryJoinList { }
}