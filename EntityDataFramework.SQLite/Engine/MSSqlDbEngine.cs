using System.Data;
using System.Data.SQLite;
using EntityDataFramework.Core.Models.Engine;
using EntityDataFramework.Core.Models.Query.Builder.Contract;
using EntityDataFramework.SQLite.Query.Builder;

namespace EntityDataFramework.SQLite.Engine {
	public class SQLiteDbEngine : BaseDbEngine {
		public SQLiteConnectionStringBuilder ConnectionStringBuilder { get; set; }
		public SQLiteDbEngine(string connectionString) {
			ConnectionStringBuilder = new SQLiteConnectionStringBuilder();
			ConnectionStringBuilder.DataSource = connectionString;
		}
		public override ISelectQuerySqlBuilder GetSelectQuerySqlBuilder() {
			var columnsBuilder = new SQLiteColumnsQuerySqlBuilder();
			var joinsBuilder = new SQLiteJoinsQuerySqlBuilder();
			var conditionsBuilder = new SQLiteConditionsQueryBuilder(columnsBuilder);
			return new SQLiteSelectQuerySqlBuilder(columnsBuilder, joinsBuilder, conditionsBuilder);
		}
		public override IDbConnection CreateConnection() {
			return new SQLiteConnection(ConnectionStringBuilder.ConnectionString);
		}
		public override IDbCommand CreateDbCommand(IDbConnection connection) {
			return new SQLiteCommand((SQLiteConnection)connection);
		}
	}
}