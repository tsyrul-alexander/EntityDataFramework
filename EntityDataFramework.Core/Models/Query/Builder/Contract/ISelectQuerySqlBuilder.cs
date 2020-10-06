using System.Text;
using EntityDataFramework.Core.Models.Query.Contract;

namespace EntityDataFramework.Core.Models.Query.Builder.Contract {
	public interface ISelectQuerySqlBuilder {
		string Build(ISelectQuery query, StringBuilder stringBuilder = null);
	}
}
