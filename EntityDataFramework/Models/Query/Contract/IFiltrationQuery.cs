namespace EntityDataFramework.Core.Models.Query.Contract {
	public interface IFiltrationQuery<T> : ISchemaQuery<T>, IQueryJoinList, IQueryConditionList { }
}