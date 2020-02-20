using System;
using System.Collections.Generic;
using System.Text;

namespace EntityDataFramework.Core.Utilities {
	public static class TypeUtilities {
		public static string JoinStr(this IEnumerable<string> array, string separator = " ") {
			return string.Join(separator, array);
		}
		public static void ForEach<T>(this IEnumerable<T> array, Action<T> action) {
			foreach (var item in array) {
				action(item);
			}
		}
	}
}