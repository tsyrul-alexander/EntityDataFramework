using System.Collections.Generic;

namespace EntityDataFramework.Core.Models.Query.Response {
	public class SelectQueryRowValue {
		public List<SelectQueryColumnValue> Values { get; set; } = new List<SelectQueryColumnValue>();
	}
}