using System;
using System.Collections.Generic;
using System.Text;

namespace EntityDataFramework.Core.Models.Condition
{
	class BaseColumnCondition: ICondition
	{

		public string GetSqlText(string tableName) {
			return null;
		}
	}
}
