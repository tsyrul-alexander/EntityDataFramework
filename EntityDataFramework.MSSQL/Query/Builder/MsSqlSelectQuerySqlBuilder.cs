using EntityDataFramework.Core.Models.Query.Builder.Abstraction;
using EntityDataFramework.Core.Models.Query.Builder.Contract;

namespace EntityDataFramework.MSSQL.Query.Builder {
	public class MsSqlSelectQuerySqlBuilder : SelectQuerySqlBuilder {
		public MsSqlSelectQuerySqlBuilder(IColumnsQuerySqlBuilder columnsQuerySqlBuilder, IJoinsQuerySqlBuilder joinsQuerySqlBuilder, IConditionsQuerySqlBuilder conditionsQuerySqlBuilder) :
			base(columnsQuerySqlBuilder, joinsQuerySqlBuilder, conditionsQuerySqlBuilder) { }
	}
}