using EntityDataFramework.Core.Models.Query.Builder.Abstraction;
using EntityDataFramework.Core.Models.Query.Builder.Contract;

namespace EntityDataFramework.MSSQL.Query.Builder {
	public class MsSqlJoinsQuerySqlBuilder : BaseJoinsQuerySqlBuilder {
		public MsSqlJoinsQuerySqlBuilder(IConditionQuerySqlBuilder conditionQuerySqlBuilder) : base(
			conditionQuerySqlBuilder) { }
	}
}
