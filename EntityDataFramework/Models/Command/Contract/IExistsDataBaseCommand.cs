namespace EntityDataFramework.Core.Models.Command {
	public interface IExistsDataBaseCommand : IDbExecuteCommand {
		string DataBaseName { get; set; }
		bool IfExist();
	}
}