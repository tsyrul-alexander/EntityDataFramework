using System.Data;
using System.Data.SqlClient;
using EntityDataFramework.Core.Models.Engine;
using EntityDataFramework.Core.Models.Query.Builder.Contract;
using EntityDataFramework.MSSQL.Query.Builder;

namespace EntityDataFramework.MSSQL.Engine {
	public class MSSqlDbEngine : BaseDbEngine {
		public SqlConnectionStringBuilder ConnectionStringBuilder { get; set; }
		public MSSqlDbEngine(string connectionString) {
			ConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
		}
		public override ISelectQuerySqlBuilder GetSelectQuerySqlBuilder() {
			var columnsBuilder = new MsSqlColumnsQuerySqlBuilder();
			var conditionsBuilder = new MsSqlConditionsQueryBuilder(columnsBuilder);
			var joinsBuilder = new MsSqlJoinsQuerySqlBuilder(conditionsBuilder);
			
			return new MsSqlSelectQuerySqlBuilder(columnsBuilder, joinsBuilder, conditionsBuilder);
		}
		public override IDbConnection CreateConnection() {
			return new SqlConnection(ConnectionStringBuilder.ConnectionString);
		}
		public override IDbCommand CreateDbCommand(IDbConnection connection) {
			return new SqlCommand {
				Connection = (SqlConnection)connection
			};
		}
	}
}