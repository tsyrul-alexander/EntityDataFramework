using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace EntityDataFramework.Core.Utilities {
	public static class SchemaUtilities {
		public const string DefaultPrimarySchemaColumnName = "Id";
		public static bool GetIsKey(this MemberInfo memberInfo, bool useDefPrimaryColumn = true) {
			return memberInfo.GetCustomAttribute<KeyAttribute>() != null || (useDefPrimaryColumn && memberInfo.Name == DefaultPrimarySchemaColumnName);
		}
		public static string GetKey(this Type type) {
			var properties = type.GetProperties();
			var primaryProperty = properties.FirstOrDefault(info => info.GetIsKey(false));
			return primaryProperty?.Name ?? DefaultPrimarySchemaColumnName;
		}
	}
}