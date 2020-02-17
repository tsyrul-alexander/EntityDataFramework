﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using EntityDataFramework.Core.Models.Engine;

namespace EntityDataFramework.Core.Models.Command
{
	public abstract class BaseDbCommand: IDbCommand {
		protected IDbEngine DbEngine { get; }
		public BaseDbCommand(IDbEngine dbEngine) {
			DbEngine = dbEngine;
		}
		public virtual IDbConnection CreateConnection() {
			return DbEngine.CreateConnection();
		}
		public virtual System.Data.IDbCommand CreateCommand(IDbConnection connection) {
			var command = DbEngine.CreateDbCommand(connection);
			command.CommandText = GetCommandSql();
			SetCommandParameters(command.Parameters);
			return command;
		}
		protected virtual void SetCommandParameters(IDataParameterCollection parameterCollection) {
		}
		public abstract string GetCommandSql();
	}
}
