using System.Collections.Generic;
using EntityDataFramework.Core.Models.Query.Join;

namespace EntityDataFramework.Core.Models.Query.Contract
{
	public interface IQueryJoinList {
		List<QueryJoin> Joins { get; set; }
	}
}
