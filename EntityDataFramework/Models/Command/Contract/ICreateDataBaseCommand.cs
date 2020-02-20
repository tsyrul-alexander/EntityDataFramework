namespace EntityDataFramework.Core.Models.Command.Contract {
	public interface ICreateDataBaseCommand : IDbExucuteNonQueryCommand {
		string DataBaseName { get; set; }
	}
}
