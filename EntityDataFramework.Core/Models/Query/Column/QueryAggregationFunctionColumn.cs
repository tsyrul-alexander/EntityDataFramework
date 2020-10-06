namespace EntityDataFramework.Core.Models.Query.Column {
	public class QueryAggregationFunctionColumn : QueryFunctionColumn {
		public QueryAggregationFunctionType AggregationType { get; }
		public IQueryColumn QueryColumn { get; }
		public QueryAggregationFunctionColumn(QueryAggregationFunctionType aggregationType, IQueryColumn queryColumn) {
			AggregationType = aggregationType;
			QueryColumn = queryColumn;
		}
	}
}
