namespace EntityDataFramework.Core.Models.Command {
	public interface IDbExucuteNonQueryCommand: IDbCommand {
		int ExecuteNonQuery();
	}
}