using System.Collections.Generic;
using EntityDataFramework.Core.Models.Query.Contract;
using EntityDataFramework.Core.Models.Query.Join;

namespace EntityDataFramework.Core.Models.Query.Expression {
	public class JoinQueryExpressionOptions : IQueryJoinList {
		public List<QueryJoin> Joins { get; set; }
		public JoinQueryExpressionOptions(List<QueryJoin> joins = null) {
			Joins = joins;
		}
	}
}