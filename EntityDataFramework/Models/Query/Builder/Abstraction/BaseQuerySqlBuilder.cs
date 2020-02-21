namespace EntityDataFramework.Core.Models.Query.Builder.Abstraction {
	public abstract class BaseQuerySqlBuilder {
		public virtual string FromCommandName => "FROM";
		public virtual string ColumnSeparator => ", ";
		public virtual string AllColumnSymbol => "*";
		public virtual string StartDelimitedIdentifier => "\"";
		public virtual string EndDelimitedIdentifier => "\"";
		protected virtual string SetToDelimitedIdentifiers(string content) {
			return $"{StartDelimitedIdentifier}{content}{EndDelimitedIdentifier}";
		}
	}
}