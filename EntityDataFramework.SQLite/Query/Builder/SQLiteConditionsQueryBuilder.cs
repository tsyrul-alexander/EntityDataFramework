using EntityDataFramework.Core.Models.Query.Builder.Abstraction;
using EntityDataFramework.Core.Models.Query.Builder.Contract;

namespace EntityDataFramework.SQLite.Query.Builder
{
	public class SQLiteConditionsQueryBuilder: BaseConditionsQuerySqlBuilder
	{
		public SQLiteConditionsQueryBuilder(IColumnQuerySqlBuilder columnQuerySqlBuilder) : base(columnQuerySqlBuilder) { }
	}
}
