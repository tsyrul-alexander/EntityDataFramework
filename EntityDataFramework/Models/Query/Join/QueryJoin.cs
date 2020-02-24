namespace EntityDataFramework.Core.Models.Query.Join {
	public class QueryJoin {
		private string _alias;
		public string MainTableName { get; set; }
		public string MainTableColumnName { get; set; }
		public string JoinTableName { get; set; }
		public string JoinTableColumnName { get; set; }
		public string Alias {
			get {
				return _alias ?? JoinTableName;
			}
			set => _alias = value;
		}
		public QueryJoinType Type { get; set; } = QueryJoinType.InnerJoin;
		public QueryJoin(string mainTableName = null, string mainTableColumnName = null,
			string joinTableName = null, string joinTableColumnName = null, string alias = null) {
			JoinTableName = joinTableName;
			JoinTableColumnName = joinTableColumnName;
			MainTableName = mainTableName;
			MainTableColumnName = mainTableColumnName;
			Alias = alias;
		}
		public static bool operator ==(QueryJoin queryJoin1, QueryJoin queryJoin2) {
			return queryJoin1?.MainTableName == queryJoin2?.MainTableName &&
				queryJoin1?.MainTableColumnName == queryJoin2?.MainTableColumnName &&
				queryJoin1?.JoinTableName == queryJoin2?.JoinTableName &&
				queryJoin1?.JoinTableColumnName == queryJoin2?.JoinTableColumnName &&
				queryJoin1?.Alias == queryJoin2?.Alias;
		}
		public static bool operator !=(QueryJoin queryJoin1, QueryJoin queryJoin2) {
			return !(queryJoin1 == queryJoin2);
		}
	}
}