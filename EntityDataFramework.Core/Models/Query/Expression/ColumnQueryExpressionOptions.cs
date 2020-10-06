using System.Collections.Generic;
using EntityDataFramework.Core.Models.Query.Column;
using EntityDataFramework.Core.Models.Query.Contract;
using EntityDataFramework.Core.Models.Query.Join;

namespace EntityDataFramework.Core.Models.Query.Expression {
	public class ColumnQueryExpressionOptions : IQueryColumnList, IQueryJoinList {
		public List<IQueryColumn> Columns { get; set; }
		public List<QueryJoin> Joins { get; set; }
	}
}
