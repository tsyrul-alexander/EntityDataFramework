using EntityDataFramework.Core.Models.Engine;
using EntityDataFramework.Core.Models.Table;

namespace EntityDataFramework.Core.Models.Command {
	public class BaseCreateTableCommand : BaseDbExecuteExecuteNonQueryCommand {
		protected IEntityTable EntityTable { get; }
		public BaseCreateTableCommand(IDbEngine dbEngine, IEntityTable entityTable) : base(dbEngine) {
			EntityTable = entityTable;
		}
		public override string GetCommandSql() {
			return null;
		}
		protected virtual string GetCreateTableSql() {
			return null;
		}
	}
}
