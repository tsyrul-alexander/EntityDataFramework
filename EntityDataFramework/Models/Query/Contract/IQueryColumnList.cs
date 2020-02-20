using System.Collections.Generic;
using EntityDataFramework.Core.Models.Query.Column;

namespace EntityDataFramework.Core.Models.Query.Contract
{
	public interface IQueryColumnList {
		List<IQueryColumn> Columns { get; set; }
	}
}
