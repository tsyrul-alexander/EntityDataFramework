using System.Collections.Generic;

namespace EntityDataFramework.Core.Models.Query
{
	public interface IQueryJoinList {
		List<QueryJoin> Joins { get; set; }
	}
}
