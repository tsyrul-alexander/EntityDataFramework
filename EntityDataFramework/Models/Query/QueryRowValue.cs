using System.Collections.Generic;

namespace EntityDataFramework.Core.Models.Query {
	public class QueryRowValue {
		public IEnumerable<QueryColumnValue> Values { get; set; }
	}
}