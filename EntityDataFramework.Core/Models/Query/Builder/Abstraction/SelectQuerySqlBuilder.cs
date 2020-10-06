using System.Text;
using EntityDataFramework.Core.Models.Query.Builder.Contract;
using EntityDataFramework.Core.Models.Query.Contract;

namespace EntityDataFramework.Core.Models.Query.Builder.Abstraction {
	public abstract class SelectQuerySqlBuilder : BaseQuerySqlBuilder, ISelectQuerySqlBuilder {
		public virtual string SelectCommandName => "SELECT";
		public IColumnsQuerySqlBuilder ColumnsQuerySqlBuilder { get; }
		public IJoinsQuerySqlBuilder JoinsQuerySqlBuilder { get; }
		public IConditionsQuerySqlBuilder ConditionsQuerySqlBuilder { get; }
		public SelectQuerySqlBuilder(IColumnsQuerySqlBuilder columnsQuerySqlBuilder,
				IJoinsQuerySqlBuilder joinsQuerySqlBuilder, IConditionsQuerySqlBuilder conditionsQuerySqlBuilder) {
			ColumnsQuerySqlBuilder = columnsQuerySqlBuilder;
			JoinsQuerySqlBuilder = joinsQuerySqlBuilder;
			ConditionsQuerySqlBuilder = conditionsQuerySqlBuilder;
		}
		public virtual string Build(ISelectQuery query, StringBuilder stringBuilder = null) {
			stringBuilder = stringBuilder ?? GetStringBuilder();
			SetSelectCommand(query, stringBuilder);
			SetSelectColumns(query, stringBuilder);
			SetFromColumns(query, stringBuilder);
			SetJoins(query, stringBuilder);
			SetConditions(query, stringBuilder);
			return stringBuilder.ToString();
		}
		protected virtual StringBuilder GetStringBuilder() {
			return new StringBuilder();
		}
		protected virtual void SetSelectCommand(ISelectionQuery query, StringBuilder stringBuilder) {
			stringBuilder.Append($"{SelectCommandName} ");
		}
		protected virtual void SetSelectColumns(ISelectionQuery query, StringBuilder stringBuilder) {
			ColumnsQuerySqlBuilder.SetQueryColumnsSql(query, stringBuilder);
		}
		protected virtual void SetFromColumns(ISelectionQuery query, StringBuilder stringBuilder) {
			stringBuilder.Append($" {FromCommandName} {SetToDelimitedIdentifiers(query.SchemaName)}");
		}
		protected virtual void SetJoins(ISelectionQuery query, StringBuilder stringBuilder) {
			JoinsQuerySqlBuilder.SetQueryJoinsSql(query, stringBuilder);
		}
		protected virtual void SetConditions(IFiltrationQuery query, StringBuilder stringBuilder) {
			ConditionsQuerySqlBuilder.SetQueryConditionsSql(query, stringBuilder);
		}
	}
}