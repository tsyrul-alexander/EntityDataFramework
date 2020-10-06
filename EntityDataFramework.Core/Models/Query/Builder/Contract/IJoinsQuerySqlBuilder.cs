using System.Text;
using EntityDataFramework.Core.Models.Query.Contract;

namespace EntityDataFramework.Core.Models.Query.Builder.Contract
{
	public interface IJoinsQuerySqlBuilder {
		void SetQueryJoinsSql(IQueryJoinList queryJoinList, StringBuilder stringBuilder);
	}
}
