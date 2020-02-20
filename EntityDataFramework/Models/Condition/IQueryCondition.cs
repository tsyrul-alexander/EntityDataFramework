namespace EntityDataFramework.Core.Models.Condition
{
	public interface IQueryCondition
	{
		string GetSqlText(string tableName);
	}
}
