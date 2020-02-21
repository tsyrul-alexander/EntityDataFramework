using EntityDataFramework.Core.Models.Query.Builder.Abstraction;
using EntityDataFramework.Core.Models.Query.Builder.Contract;

namespace EntityDataFramework.MSSQL.Query.Builder {
	public class MsSqlSelectQuerySqlBuilder : SelectQuerySqlBuilder {
		public MsSqlSelectQuerySqlBuilder(IColumnQuerySqlBuilder columnQuerySqlBuilder) :
			base(columnQuerySqlBuilder) { }
	}
}