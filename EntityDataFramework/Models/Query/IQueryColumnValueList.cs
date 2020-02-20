using System.Collections.Generic;

namespace EntityDataFramework.Core.Models.Query
{
	interface IQueryColumnValueList {
		List<QueryColumnValue> ColumnValues { get; set; }
	}
}
