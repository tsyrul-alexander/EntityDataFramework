using System.Collections.Generic;

namespace EntityDataFramework.Core.Models.Command {
	public interface IDbExecuteCommand: IDbCommand {
		IEnumerable<T> Execute<T>();
	}
}
