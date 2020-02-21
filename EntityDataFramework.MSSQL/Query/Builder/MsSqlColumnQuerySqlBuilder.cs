using EntityDataFramework.Core.Models.Query.Builder.Abstraction;

namespace EntityDataFramework.MSSQL.Query.Builder {
	public class MsSqlColumnQuerySqlBuilder : BaseColumnQuerySqlBuilder {
		protected override string GetCountQueryFunctionName() {
			return "Count";
		}
	}
}
