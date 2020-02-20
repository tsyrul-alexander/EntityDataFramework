using System.Collections.Generic;
using EntityDataFramework.Core.Models.Condition;
using EntityDataFramework.Core.Models.Query;

namespace EntityDataFramework.Core.Models.Command {
	public abstract class BaseTableOperationCommand: ISchemaQuery, IQueryConditionList {
		public string TableName { get; set; }
		public List<IQueryCondition> Conditions { get; set; }
		protected BaseTableOperationCommand(string tableName = null, List<IQueryCondition> conditions = null) {
			TableName = tableName;
			Conditions = conditions;
		}
	}
}