using EntityDataFramework.Core.Models.Query.Builder.Abstraction;
using EntityDataFramework.Core.Models.Query.Builder.Contract;

namespace EntityDataFramework.SQLite.Query.Builder {
	public class SQLiteJoinsQuerySqlBuilder : BaseJoinsQuerySqlBuilder {
		public SQLiteJoinsQuerySqlBuilder(IConditionQuerySqlBuilder conditionQuerySqlBuilder) : base(
			conditionQuerySqlBuilder) { }
	}
}
