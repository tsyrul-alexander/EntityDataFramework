using System.Data;
using EntityDataFramework.Core.Models.Engine;
using EntityDataFramework.Core.Models.Query.Builder.Contract;
using EntityDataFramework.SQLite.Query.Builder;
using Microsoft.Data.Sqlite;

namespace EntityDataFramework.SQLite.Engine {
	public class SQLiteDbEngine : BaseDbEngine {
		public SqliteConnectionStringBuilder ConnectionStringBuilder { get; set; }
		public SQLiteDbEngine(string connectionString) {
			ConnectionStringBuilder = new SqliteConnectionStringBuilder();
			ConnectionStringBuilder.DataSource = connectionString;
		}
		public override ISelectQuerySqlBuilder GetSelectQuerySqlBuilder() {
			var columnsBuilder = new SQLiteColumnsQuerySqlBuilder();
			var conditionsBuilder = new SQLiteConditionsQueryBuilder(columnsBuilder);
			var joinsBuilder = new SQLiteJoinsQuerySqlBuilder(conditionsBuilder);
			return new SQLiteSelectQuerySqlBuilder(columnsBuilder, joinsBuilder, conditionsBuilder);
		}
		public override IDbConnection CreateConnection() {
			return new SqliteConnection(ConnectionStringBuilder.ConnectionString);
		}
		public override IDbCommand CreateDbCommand(IDbConnection connection) {
			var command = new SqliteCommand {
				Connection = (SqliteConnection) connection
			};
			return command;
		}
	}
}