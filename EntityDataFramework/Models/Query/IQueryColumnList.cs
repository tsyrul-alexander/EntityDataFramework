using System;
using System.Collections.Generic;
using System.Text;

namespace EntityDataFramework.Core.Models.Query
{
	public interface IQueryColumnList {
		List<QueryColumn> Columns { get; set; }
	}
}
