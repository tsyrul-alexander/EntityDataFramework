using System;
using System.Collections.Generic;
using System.Data;
using EntityDataFramework.Core.Models.Engine;

namespace EntityDataFramework.Core.Models.Command {
	public class CustomExecuteSqlCommand {
		public IDbEngine DbEngine { get; }
		public CustomExecuteSqlCommand(IDbEngine dbEngine) {
			DbEngine = dbEngine;
		}
		public IEnumerable<T> Execute<T>(string sqlText, Func<IDataReader, T> readFunc) {
			using (var connection = DbEngine.CreateConnection()) {
				connection.Open();
				using (var command = DbEngine.CreateDbCommand(connection)) {
					command.CommandText = sqlText;
					using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection)) {
						while (reader.Read()) {
							yield return readFunc(reader);
						}
					}
				}
			}
		}
	}
}