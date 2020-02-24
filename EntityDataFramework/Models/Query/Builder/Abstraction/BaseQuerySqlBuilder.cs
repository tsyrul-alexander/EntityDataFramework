namespace EntityDataFramework.Core.Models.Query.Builder.Abstraction {
	public abstract class BaseQuerySqlBuilder {
		public virtual string FromCommandName => "FROM";
		public virtual string ColumnSeparator => ", ";
		public virtual string StartDelimitedIdentifier => "\"";
		public virtual string EndDelimitedIdentifier => "\"";
		public virtual string AsCommandName => "AS";
		protected virtual string GetAliasFormat(string aliasName) {
			return SetToDelimitedIdentifiers(aliasName);
		}
		protected virtual string GetTableFormat(string tableName) {
			return SetToDelimitedIdentifiers(tableName);
		}
		protected virtual string GetColumnFormat(string columnName) {
			return SetToDelimitedIdentifiers(columnName);
		}
		protected virtual string SetToDelimitedIdentifiers(string content) {
			return $"{StartDelimitedIdentifier}{content}{EndDelimitedIdentifier}";
		}
	}
}