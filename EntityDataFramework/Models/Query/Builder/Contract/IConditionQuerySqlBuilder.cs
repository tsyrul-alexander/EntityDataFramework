using EntityDataFramework.Core.Models.Condition;

namespace EntityDataFramework.Core.Models.Query.Builder.Contract {
	public interface IConditionQuerySqlBuilder {
		string GetQueryConditionSql(IQueryCondition queryCondition);
	}
}