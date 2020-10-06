using System;
using System.Text;
using EntityDataFramework.Core.Models.Query.Builder.Contract;
using EntityDataFramework.Core.Models.Query.Contract;
using EntityDataFramework.Core.Models.Query.Join;

namespace EntityDataFramework.Core.Models.Query.Builder.Abstraction {
	public abstract class BaseJoinsQuerySqlBuilder : BaseQuerySqlBuilder, IJoinsQuerySqlBuilder {
		public IConditionQuerySqlBuilder ConditionQuerySqlBuilder { get; }
		public virtual string OnCommandName => "ON";
		public BaseJoinsQuerySqlBuilder(IConditionQuerySqlBuilder conditionQuerySqlBuilder) {
			ConditionQuerySqlBuilder = conditionQuerySqlBuilder;
		}
		public virtual void SetQueryJoinsSql(IQueryJoinList queryJoinList, StringBuilder stringBuilder) {
			queryJoinList.Joins.ForEach(join => SetQueryJoinSql(join, stringBuilder));
		}
		protected virtual void SetQueryJoinSql(QueryJoin queryJoin, StringBuilder stringBuilder) {
			var joinTable = GetTableFormat(queryJoin.JoinTableName);
			var joinTableAlias = GetAliasFormat(queryJoin.Alias);
			var joinTypeSql = GetJoinTypeSql(queryJoin.Type);
			stringBuilder.AppendLine($"{joinTypeSql} {joinTable} {AsCommandName} {joinTableAlias} " + 
				$"{OnCommandName} {ConditionQuerySqlBuilder.GetQueryConditionSql(queryJoin.Condition)}");
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