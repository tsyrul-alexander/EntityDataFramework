namespace EntityDataFramework.Core.Models.Column {
	public interface IEntityColumn {
		string Name { get; set; }
		bool IsPrimaryKey { get; set; }
		bool IsUnique { get; set; }
		bool IsRequired { get; set; }
		EntityColumnType ColumnType { get; set; }
		EntityColumnForeignKey ForeignKey { get; set; }
	}
}
