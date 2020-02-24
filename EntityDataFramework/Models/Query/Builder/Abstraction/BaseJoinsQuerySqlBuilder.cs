using System;
using System.Text;
using EntityDataFramework.Core.Models.Query.Builder.Contract;
using EntityDataFramework.Core.Models.Query.Contract;
using EntityDataFramework.Core.Models.Query.Join;

namespace EntityDataFramework.Core.Models.Query.Builder.Abstraction {
	public abstract class BaseJoinsQuerySqlBuilder : BaseQuerySqlBuilder, IJoinsQuerySqlBuilder {
		public virtual string OnCommandName => "ON";
		public virtual void SetQueryJoinsSql(IQueryJoinList queryJoinList, StringBuilder stringBuilder) {
			queryJoinList.Joins.ForEach(join => SetQueryJoinSql(join, stringBuilder));
		}
		protected virtual void SetQueryJoinSql(QueryJoin queryJoin, StringBuilder stringBuilder) {
			var joinTable = GetTableFormat(queryJoin.JoinTableName);
			var joinTableColumn = GetColumnFormat(queryJoin.JoinTableColumnName);
			var mainTable = GetTableFormat(queryJoin.MainTableName);
			var mainTableColumn = GetColumnFormat(queryJoin.MainTableColumnName);
			var joinTableAlias = GetAliasFormat(queryJoin.Alias);
			var joinTypeSql = GetJoinTypeSql(queryJoin.Type);
			stringBuilder.AppendLine($"{joinTypeSql} {joinTable} {AsCommandName} {joinTableAlias} " + 
				$"{OnCommandName} {joinTableAlias}.{joinTableColumn} = {mainTable}.{mainTableColumn}");
		}
		protected virtual string GetJoinTypeSql(QueryJoinType type) {
			switch (type) {
				case QueryJoinType.InnerJoin:
					return GetInnerJoinSql();
				case QueryJoinType.LeftJoin:
					return GetLeftJoinSql();
				default:
					throw new NotImplementedException(nameof(type));
			}
		}
		protected virtual string GetInnerJoinSql() {
			return "INNER JOIN";
		}
		protected virtual string GetLeftJoinSql() {
			return "LEFT JOIN";
		}
	}
}