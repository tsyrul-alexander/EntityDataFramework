using EntityDataFramework.Core.Models.Engine;
using EntityDataFramework.Core.Models.Query.Contract;

namespace EntityDataFramework.Core.Models.Query {
	public abstract class BaseTableOperationQuery : ISchemaQuery {
		public IDbEngine DbEngine { get; }
		public string SchemaName { get; set; }
		protected BaseTableOperationQuery(IDbEngine dbEngine, string tableName = null) {
			DbEngine = dbEngine;
			SchemaName = tableName;
		}
	}
}