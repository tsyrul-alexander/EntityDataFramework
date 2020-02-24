using System.Data;
using System.Data.SqlClient;
using EntityDataFramework.Core.Models.Engine;
using EntityDataFramework.Core.Models.Query.Builder.Contract;
using EntityDataFramework.MSSQL.Query.Builder;
using IDbCommand = System.Data.IDbCommand;

namespace EntityDataFramework.MSSQL.Engine {
	public class MSSqlDbEngine : BaseDbEngine {
		private const string MasterDataBaseName = "master";
		public SqlConnectionStringBuilder ConnectionStringBuilder { get; set; }
		public MSSqlDbEngine(string connectionString) {
			ConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
		}
		protected virtual string GetMasterConnectionString() {
			var masterConnectionBuilder = new SqlConnectionStringBuilder(ConnectionStringBuilder.ConnectionString) {
				InitialCatalog = MasterDataBaseName
			};
			return masterConnectionBuilder.ConnectionString;
		}
		public override ISelectQuerySqlBuilder GetSelectQuerySqlBuilder() {
			var columnsBuilder = new MsSqlColumnsQuerySqlBuilder();
			var joinsBuilder = new MsSqlJoinsQuerySqlBuilder();
			var conditionsBuilder = new MsSqlConditionsQueryBuilder(columnsBuilder);
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