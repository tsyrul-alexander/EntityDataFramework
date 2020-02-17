using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EntityDataFramework.Core.Models.Command {
	public interface IDbExecuteCommand: IDbCommand {
		IEnumerable<T> Execute<T>();
	}
}
