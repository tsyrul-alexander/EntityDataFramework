using System.Text;
using EntityDataFramework.Core.Models.Query.Contract;

namespace EntityDataFramework.Core.Models.Query.Builder.Contract {
	public interface IColumnQuerySqlBuilder {
		void SetQueryColumnsSql(ISelectionQuery selectQuery, StringBuilder stringBuilder);
	}
}