using System.Collections.Generic;

namespace EntityDataFramework.Core.Models.Query.Contract
{
	interface IQueryColumnValueList {
		List<QueryColumnValue> ColumnValues { get; set; }
	}
}
