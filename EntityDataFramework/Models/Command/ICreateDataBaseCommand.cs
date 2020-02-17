namespace EntityDataFramework.Core.Models.Command {
	public interface ICreateDataBaseCommand : IDbExucuteNonQueryCommand {
		string DataBaseName { get; set; }
	}
}
