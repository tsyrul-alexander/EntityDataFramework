﻿using System.Collections.Generic;
using System.Data;
using EntityDataFramework.Core.Models.Command;
using EntityDataFramework.Core.Models.Command.Contract;
using EntityDataFramework.Core.Models.Table;
using IDbCommand = System.Data.IDbCommand;

namespace EntityDataFramework.Core.Models.Engine
{
	public abstract class BaseDbEngine: IDbEngine {
		public List<IEntityTable> EntityTables { get; set; }
		public virtual bool IsExistDatabase() {
			var createDataBaseCommand = GetIfExistsDataBaseCommand();
			return createDataBaseCommand.IfExist();
		}
		public virtual void CreateDatabaseIfNotExist() {
			if (IsExistDatabase()) {
				return;
			}
			CreateDatabase();
		}
		public virtual void CreateDatabase() {
			var createDataBaseCommand = GetCreateDataBaseCommand();
			createDataBaseCommand.ExecuteNonQuery();
		}
		protected abstract ICreateDataBaseCommand GetCreateDataBaseCommand();
		protected abstract IExistsDataBaseCommand GetIfExistsDataBaseCommand();
		public abstract IDbConnection CreateConnection();
		public abstract IDbCommand CreateDbCommand(IDbConnection connection);
		protected virtual void CreateTables() {
			EntityTables.ForEach(CreateTable);
		}
		protected virtual void CreateTable(IEntityTable entityTable) {

		}
	}
}
