using System.Collections.Generic;
using System.Data;
using EntityDataFramework.Core.Models.Query.Builder.Contract;
using EntityDataFramework.Core.Models.Table;

namespace EntityDataFramework.Core.Models.Engine
{
	public interface IDbEngine {
		List<IEntityTable> EntityTables { get; set; }
		//bool IsExistDatabase();
		//void CreateDatabaseIfNotExist();
		//void CreateDatabase();
		ISelectQuerySqlBuilder GetSelectQuerySqlBuilder();
		IDbConnection CreateConnection();
		IDbCommand CreateDbCommand(IDbConnection connection);
	}
}
