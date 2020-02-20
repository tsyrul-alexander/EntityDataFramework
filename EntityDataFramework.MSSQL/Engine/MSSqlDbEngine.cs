using System.Data;
using System.Data.SqlClient;
using EntityDataFramework.Core.Models.Command;
using EntityDataFramework.Core.Models.Command.Contract;
using EntityDataFramework.Core.Models.Engine;
using EntityDataFramework.MSSQL.Command;
using IDbCommand = System.Data.IDbCommand;

namespace EntityDataFramework.MSSQL.Engine {
	public class MSSqlDbEngine : BaseDbEngine {
		private const string MasterDataBaseName = "master";
		public SqlConnectionStringBuilder ConnectionStringBuilder { get; set; }
		public MSSqlDbEngine(string connectionString) {
			ConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
		}
		protected override ICreateDataBaseCommand GetCreateDataBaseCommand() {
			return new CreateMsSqlDataBaseCommand(this, ConnectionStringBuilder.InitialCatalog);
		}
		protected override IExistsDataBaseCommand GetIfExistsDataBaseCommand() {
			return new ExistsMsSqlDataBaseCommand(this, GetMasterConnectionString(), ConnectionStringBuilder.InitialCatalog);
		}
		protected virtual string GetMasterConnectionString() {
			var masterConnectionBuilder = new SqlConnectionStringBuilder(ConnectionStringBuilder.ConnectionString) {
				InitialCatalog = MasterDataBaseName
			};
			return masterConnectionBuilder.ConnectionString;
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