using EntityDataFramework.Core.Models.Query.Builder.Abstraction;

namespace EntityDataFramework.MSSQL.Query.Builder {
	public class MsSqlColumnsQuerySqlBuilder : BaseColumnsQuerySqlBuilder {
		protected override string GetCountQueryFunctionName() {
			return "Count";
		}
	}
}
