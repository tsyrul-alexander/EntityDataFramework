using System.Data;
using System.Data.SqlClient;

namespace EntityDataFramework.MSSQL.Utilities
{
	internal static class CommandUtilities {
		private const string ParameterPrefix = "@";
		public static void CreateInputStringParameter(this IDataParameterCollection collection, string parameterName, string parameterValue) {
			if (!parameterName.EndsWith(ParameterPrefix)) {
				parameterName = ParameterPrefix + parameterName;
			}
			collection.Add(new SqlParameter(parameterName, SqlDbType.NVarChar) {
				Direction = ParameterDirection.Input,
				Value = parameterValue
			});
		}
	}
}
