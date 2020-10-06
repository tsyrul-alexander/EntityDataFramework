using System;
using System.Collections.Generic;
using System.Data;
using EntityDataFramework.Core.Models.Command;
using EntityDataFramework.Core.Models.Condition;
using EntityDataFramework.Core.Models.Engine;
using EntityDataFramework.Core.Models.Query.Builder.Contract;
using EntityDataFramework.Core.Models.Query.Column;
using EntityDataFramework.Core.Models.Query.Contract;
using EntityDataFramework.Core.Models.Query.Join;
using EntityDataFramework.Core.Models.Query.Response;

namespace EntityDataFramework.Core.Models.Query.Select {
	public class SelectQuery: BaseTableOperationQuery, ISelectQuery {
		public bool UseAllSchemaColumns { get; set; }
		public List<IQueryColumn> Columns { get; set; } = new List<IQueryColumn>();
		public List<QueryJoin> Joins { get; set; } = new List<QueryJoin>();
		public List<IQueryCondition> Conditions { get; set; } = new List<IQueryCondition>();
		public SelectQuery(IDbEngine dbEngine, string tableName) : base(dbEngine, tableName) { }
		public IEnumerable<SelectQueryRowValue> GetEntities() {
			var builder = GetSelectQuerySqlBuilder();
			var selectSql = GetSelectQuerySqlText(builder);
			return Execute(selectSql, ReadQueryRowValue);
		}
		protected virtual IEnumerable<T> Execute<T>(string selectSql, Func<IDataReader, T> readFunc) {
			var customSqlCommand = new CustomExecuteSqlCommand(DbEngine);
			return customSqlCommand.Execute(selectSql, readFunc);
		}
		protected virtual string GetSelectQuerySqlText(ISelectQuerySqlBuilder builder) {
			return builder.Build(this);
		}
		protected virtual ISelectQuerySqlBuilder GetSelectQuerySqlBuilder() {
			return DbEngine.GetSelectQuerySqlBuilder();
		}
		protected virtual SelectQueryRowValue ReadQueryRowValue(IDataReader dataReader) {
			var rowValue = new SelectQueryRowValue();
			foreach (var queryColumn in Columns) {
				var (columnName, columnValue) = ReadQueryColumnValue(dataReader, queryColumn);
				rowValue.Values.Add(columnName, columnValue);
			}
			return rowValue;
		}
		protected virtual (string columnName, object columnValue) ReadQueryColumnValue(IDataReader dataReader, IQueryColumn queryColumn) {
			var dataColumnName = queryColumn.Alias;
			var dataValue = dataReader[dataColumnName];
			return(dataColumnName, dataValue);
		}
	}
}