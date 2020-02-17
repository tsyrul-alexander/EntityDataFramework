using System;
using System.Collections.Generic;
using System.Data;

namespace EntityDataFramework.Core.Models.Command
{
	public interface IDbCommand {
		string GetCommandSql();
	}
}
