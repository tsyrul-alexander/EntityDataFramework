using EntityDataFramework.Core.Models.Query.Builder.Abstraction;
using EntityDataFramework.Core.Models.Query.Builder.Contract;

namespace EntityDataFramework.MSSQL.Query.Builder
{
	public class MsSqlConditionsQueryBuilder: BaseConditionsQuerySqlBuilder
	{
		public MsSqlConditionsQueryBuilder(IColumnQuerySqlBuilder columnQuerySqlBuilder) : base(columnQuerySqlBuilder) { }
	}
}
