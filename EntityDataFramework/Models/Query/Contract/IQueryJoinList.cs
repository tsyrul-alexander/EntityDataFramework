using System.Collections.Generic;

namespace EntityDataFramework.Core.Models.Query.Contract
{
	public interface IQueryJoinList {
		List<QueryJoin> Joins { get; set; }
	}
}
