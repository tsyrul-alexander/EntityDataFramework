using System;
using System.Collections.Generic;
using EntityDataFramework.Core.Models.Command.Abstraction;
using EntityDataFramework.Core.Models.Condition;
using EntityDataFramework.Core.Models.Engine;
using EntityDataFramework.Core.Models.Query;
using EntityDataFramework.Core.Models.Query.Column;
using EntityDataFramework.Core.Models.Query.Contract;

namespace EntityDataFramework.Core.Models.Command {
	public class SelectCommand<T>: BaseTableOperationCommand<T>, ISelectionQuery<T>, IFiltrationQuery<T> {
		public bool UseAllSchemaColumns { get; set; }
		public List<IQueryColumn> Columns { get; set; }
		public List<QueryJoin> Joins { get; set; }
		public List<IQueryCondition> Conditions { get; set; }
		public SelectCommand(IDbEngine dbEngine, string tableName = null) : base(dbEngine, tableName) { }
		public IEnumerable<T> GetEntities() {
			throw new NotImplementedException();
		}
		
	}
}