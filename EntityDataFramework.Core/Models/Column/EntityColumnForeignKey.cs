namespace EntityDataFramework.Core.Models.Column {
	public class EntityColumnForeignKey {
		public string ReferenceTableName { get; set; }
		public string ReferenceTableColumnName { get; set; }
		public EntityColumnForeignKey(string referenceTableName, string referenceTableColumnName) {
			ReferenceTableName = referenceTableName;
			ReferenceTableColumnName = referenceTableColumnName;
		}
	}
}