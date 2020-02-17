using System.Data;
using System.Data.SqlClient;
using EntityDataFramework.Core.Models.Command;
using EntityDataFramework.Core.Models.Engine;
using EntityDataFramework.MSSQL.Utilities;

namespace EntityDataFramework.MSSQL.Command {
	class ExistsMsSqlDataBaseCommand : BaseDbExecuteCommand, IExistsDataBaseCommand {
		private readonly string _masterConnectionString;
		public string DataBaseName { get; set; }
		public bool IfExist() {
			var value = Execute<int?>();
			return value != null;
		}
		public ExistsMsSqlDataBaseCommand(IDbEngine dbEngine, string masterConnectionString, string dataBaseName) : base(dbEngine) {
			_masterConnectionString = masterConnectionString;
			DataBaseName = dataBaseName;
		}
		public override IDbConnection CreateConnection() {
			return new SqlConnection(_masterConnectionString);
		}
		protected override object Read(IDataReader dataReader) {
			return dataReader.IsDBNull(0) ? (int?)null : dataReader.GetInt32(0);
		}
		protected override void SetCommandParameters(IDataParameterCollection parameterCollection) {
			base.SetCommandParameters(parameterCollection);
			parameterCollection.CreateInputStringParameter(nameof(DataBaseName), DataBaseName);
		}
		public override string GetCommandSql() {
			return $"SELECT DB_ID(@{nameof(DataBaseName)})";
		}
		
	}
}
