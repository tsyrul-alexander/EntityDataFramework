using System;
using System.Collections.Generic;

namespace EntityDataFramework.Core.Utilities {
	public static class TypeUtilities {
		public static string JoinStr(this IEnumerable<string> array, string separator = " ") {
			return string.Join(separator, array);
		}
		public static bool GetIsLast<T>(this IList<T> array, int index) {
			return (array.Count - 1) == index;
		}
		public static bool GetIsEmpty<T>(this IList<T> array) {
			return array == null || array.Count == 0;
		}
		public static void ForEach<T>(this IEnumerable<T> array, Action<T> action) {
			foreach (var item in array) {
				action(item);
			}
		}
	}
}