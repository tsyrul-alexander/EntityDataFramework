using EntityDataFramework.Core.Models.Engine;
using EntityDataFramework.Core.Models.Query.Contract;

namespace EntityDataFramework.Core.Models.Command.Abstraction {
	public abstract class BaseTableOperationCommand<T> : ISchemaQuery<T> {
		public IDbEngine DbEngine { get; }
		public string SchemaName { get; set; }
		protected BaseTableOperationCommand(IDbEngine dbEngine, string tableName = null) {
			DbEngine = dbEngine;
			SchemaName = tableName;
		}
	}
}