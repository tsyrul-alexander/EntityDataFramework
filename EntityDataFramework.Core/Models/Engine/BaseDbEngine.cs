using System.Collections.Generic;
using System.Data;
using EntityDataFramework.Core.Models.Query.Builder.Contract;
using EntityDataFramework.Core.Models.Table;
using IDbCommand = System.Data.IDbCommand;

namespace EntityDataFramework.Core.Models.Engine
{
	public abstract class BaseDbEngine: IDbEngine {
		public List<IEntityTable> EntityTables { get; set; }
		public abstract ISelectQuerySqlBuilder GetSelectQuerySqlBuilder();
		public abstract IDbConnection CreateConnection();
		public abstract IDbCommand CreateDbCommand(IDbConnection connection);
		protected virtual void CreateTables() {
			EntityTables.ForEach(CreateTable);
		}
		protected virtual void CreateTable(IEntityTable entityTable) {

		}
	}
}
