using System.Collections.Generic;
using EntityDataFramework.Core.Models.Query;

namespace EntityDataFramework.Core.Models.Command {
	public class SelectCommand: BaseTableOperationCommand, IQueryColumnList, IQueryJoinList {
		public List<QueryColumn> Columns { get; set; }
		public List<QueryJoin> Joins { get; set; }
	}
}