using System;
using System.Collections.Generic;

namespace EntityDataFramework.Core.Models.Query.Response {
	public class SelectQueryRowValue {
		public IDictionary<string, object> Values { get; set; } = new Dictionary<string, object>();

		public T GetValue<T>(string columnName) {
			return (T)Values[columnName];
		}
		public Guid GetGuid(string columnName) {
			return Guid.Parse(GetValue<string>(columnName));
		}
	}
}