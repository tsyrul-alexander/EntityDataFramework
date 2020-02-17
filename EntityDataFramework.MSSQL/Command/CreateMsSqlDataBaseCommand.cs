using System;
using EntityDataFramework.Core.Models.Command;
using EntityDataFramework.Core.Models.Engine;

namespace EntityDataFramework.MSSQL.Command {
	public class CreateMsSqlDataBaseCommand : BaseDbExecuteExecuteNonQueryCommand, ICreateDataBaseCommand {
		public string DataBaseName { get; set; }
		public CreateMsSqlDataBaseCommand(IDbEngine dbEngine, string dataBaseName) : base(dbEngine) {
			DataBaseName = dataBaseName;
		}
		public override string GetCommandSql() {
			throw new NotImplementedException();
		}
	}
}