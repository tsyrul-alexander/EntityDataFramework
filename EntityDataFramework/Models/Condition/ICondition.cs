using System;
using System.Collections.Generic;
using System.Text;

namespace EntityDataFramework.Core.Models.Condition
{
	interface ICondition
	{
		string GetSqlText(string tableName);
	}
}
