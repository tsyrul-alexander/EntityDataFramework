using System;
using System.Collections.Generic;
using System.Data;
using EntityDataFramework.Core.Models.Engine;

namespace EntityDataFramework.Core.Models.Command.Abstraction
{
	public abstract class BaseDbExecuteCommand: BaseDbCommand, IDbCommand
	{
		public IEnumerable<T> Execute<T>() {
			using (var connection = CreateConnection()) {
				return Execute<T>(connection, Read);
			}
		}
		protected virtual IEnumerable<T> Execute<T>(IDbConnection connection, Func<IDataReader, object> read) {
			using (var command = CreateCommand(connection)) {
				try {
					connection.Open();
					var reader = command.ExecuteReader();
					return Reads<T>(reader, read);
				} finally {
					connection.Close();
				}
			}
		}
		protected virtual IEnumerable<T> Reads<T>(IDataReader dataReader, Func<IDataReader, object> func) {
			var list = new List<T>();
			while (dataReader.Read()) {
				list.Add((T)func(dataReader));
			}
			return list;
		}
		protected virtual object Read(IDataReader dataReader) {
			return default;
		}
		protected BaseDbExecuteCommand(IDbEngine dbEngine) : base(dbEngine) { }
	}
}
