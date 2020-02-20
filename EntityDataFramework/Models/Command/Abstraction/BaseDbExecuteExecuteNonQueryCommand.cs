using EntityDataFramework.Core.Models.Engine;

namespace EntityDataFramework.Core.Models.Command.Abstraction
{
	public abstract class BaseDbExecuteExecuteNonQueryCommand: BaseDbCommand, IDbExucuteNonQueryCommand {
		protected BaseDbExecuteExecuteNonQueryCommand(IDbEngine dbEngine) : base(dbEngine) { }
		public int ExecuteNonQuery() {
			var sqlCommand = GetCommandSql();
			using (var connection = CreateConnection()) {
				using (var command = CreateCommand(connection)) {
					command.CommandText = sqlCommand;
					try {
						connection.Open();
						return command.ExecuteNonQuery();
					} finally {
						connection.Close();
					}
				}
			}
		}
	}
}
