using System.Collections.Generic;
using EntityDataFramework.Core.Models.Condition;

namespace EntityDataFramework.Core.Models.Query.Join {
	public class QueryJoin {
		private string _alias;
		public string JoinTableName { get; set; }
		public IQueryCondition Condition { get; set; }
		public string Alias {
			get => _alias ?? JoinTableName;
			set => _alias = value;
		}
		public QueryJoinType Type { get; set; } = QueryJoinType.InnerJoin;
		public QueryJoin(string joinTableName, IQueryCondition condition, string alias = null) {
			JoinTableName = joinTableName;
			Condition = condition;
			Alias = alias;
		}
		public static bool operator ==(QueryJoin queryJoin1, QueryJoin queryJoin2) {
			return queryJoin1?.Alias == queryJoin2?.Alias;
		}
		public static bool operator !=(QueryJoin queryJoin1, QueryJoin queryJoin2) {
			return !(queryJoin1 == queryJoin2);
		}
	}
}