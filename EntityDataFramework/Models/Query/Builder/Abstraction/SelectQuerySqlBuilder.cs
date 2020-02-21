using System.Text;
using EntityDataFramework.Core.Models.Query.Builder.Contract;
using EntityDataFramework.Core.Models.Query.Contract;

namespace EntityDataFramework.Core.Models.Query.Builder.Abstraction {
	public abstract class SelectQuerySqlBuilder : BaseQuerySqlBuilder, ISelectQuerySqlBuilder {
		public virtual string SelectCommandName => "SELECT";
		public IColumnQuerySqlBuilder ColumnQuerySqlBuilder { get; }
		public SelectQuerySqlBuilder(IColumnQuerySqlBuilder columnQuerySqlBuilder) {
			ColumnQuerySqlBuilder = columnQuerySqlBuilder;
		}
		public virtual string Build(ISelectionQuery query, StringBuilder stringBuilder = null) {
			stringBuilder = stringBuilder ?? GetStringBuilder();
			SetSelectCommand(query, stringBuilder);
			SetSelectColumns(query, stringBuilder);
			SetFromColumns(query, stringBuilder);
			return stringBuilder.ToString();
		}
		protected virtual StringBuilder GetStringBuilder() {
			return new StringBuilder();
		}
		protected virtual void SetSelectCommand(ISelectionQuery query, StringBuilder stringBuilder) {
			stringBuilder.Append($"{SelectCommandName} ");
		}
		protected virtual void SetSelectColumns(ISelectionQuery query, StringBuilder stringBuilder) {
			ColumnQuerySqlBuilder.SetQueryColumnsSql(query, stringBuilder);
		}
		protected virtual void SetFromColumns(ISelectionQuery query, StringBuilder stringBuilder) {
			stringBuilder.Append($" {FromCommandName} {SetToDelimitedIdentifiers(query.SchemaName)}");
		}
	}
}