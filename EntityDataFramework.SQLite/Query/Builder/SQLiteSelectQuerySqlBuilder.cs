using EntityDataFramework.Core.Models.Query.Builder.Abstraction;
using EntityDataFramework.Core.Models.Query.Builder.Contract;

namespace EntityDataFramework.SQLite.Query.Builder {
	public class SQLiteSelectQuerySqlBuilder : SelectQuerySqlBuilder {
		public SQLiteSelectQuerySqlBuilder(IColumnsQuerySqlBuilder columnsQuerySqlBuilder, IJoinsQuerySqlBuilder joinsQuerySqlBuilder, IConditionsQuerySqlBuilder conditionsQuerySqlBuilder) :
			base(columnsQuerySqlBuilder, joinsQuerySqlBuilder, conditionsQuerySqlBuilder) { }
	}
}