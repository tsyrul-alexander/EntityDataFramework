using System;
using System.Collections.Generic;
using System.Text;

namespace EntityDataFramework.Core.Models.Query.Contract
{
	public interface ISelectionQuery : ISchemaQuery, IQueryColumnList, IQueryJoinList {
		bool UseAllSchemaColumns { get; set; }
	}
}
