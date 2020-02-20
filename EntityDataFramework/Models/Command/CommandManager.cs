namespace EntityDataFramework.Core.Models.Command
{
	public abstract class CommandManager {
		public virtual string CreateTableCommandName => "CREATE TABLE";
		public virtual string PrimaryKeyCommandName => "PRIMARY KEY";
		public virtual string ForeignKeyCommandName => "FOREIGN KEY";
		public virtual string ReferencesCommandName => "REFERENCES";
		public virtual string UniqueCommandName => "UNIQUE";
		public virtual string NullCommandName => "NULL";
		public virtual string StartDelimitedIdentifier => "\"";
		public virtual string EndDelimitedIdentifier => "\"";
		public virtual string EqualCommandName => "=";
		public virtual string AndCommandName => "AND";
		public virtual string OrCommandName => "OR";

	}
}
