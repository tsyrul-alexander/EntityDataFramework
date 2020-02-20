using EntityDataFramework.Core.Models.Query.Contract;

namespace EntityDataFramework.Core.Models.Command {
	public abstract class BaseTableOperationCommand<T> : ISchemaQuery<T> {
		public string SchemaName { get; set; }
		protected BaseTableOperationCommand(string tableName = null) {
			SchemaName = tableName;
		}
	}
}