﻿using System.Text;
using EntityDataFramework.Core.Models.Query.Column;

namespace EntityDataFramework.Core.Models.Query.Builder.Contract {
	public interface IColumnQuerySqlBuilder {
		string GetQueryColumnSql(IQueryColumn queryColumn);
	}
}
