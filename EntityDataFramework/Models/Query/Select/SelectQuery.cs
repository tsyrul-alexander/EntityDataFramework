using System;
using System.Collections.Generic;
using EntityDataFramework.Core.Models.Command.Abstraction;
using EntityDataFramework.Core.Models.Condition;
using EntityDataFramework.Core.Models.Engine;
using EntityDataFramework.Core.Models.Query.Column;
using EntityDataFramework.Core.Models.Query.Contract;

namespace EntityDataFramework.Core.Models.Query.Select {
	public class SelectQuery: BaseTableOperationQuery, ISelectionQuery, IFiltrationQuery {
		public bool UseAllSchemaColumns { get; set; }
		public List<IQueryColumn> Columns { get; set; } = new List<IQueryColumn>();
		public List<QueryJoin> Joins { get; set; } = new List<QueryJoin>();
		public List<IQueryCondition> Conditions { get; set; } = new List<IQueryCondition>();
		public SelectQuery(IDbEngine dbEngine, string tableName) : base(dbEngine, tableName) { }
		public IEnumerable<object> GetEntities() {
			var builder = DbEngine.GetSelectQuerySqlBuilder();
			var selectSql = builder.Build(this);
			throw new NotImplementedException();
		}
	}
}