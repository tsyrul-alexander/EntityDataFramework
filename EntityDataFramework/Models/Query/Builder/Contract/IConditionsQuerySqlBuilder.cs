using System.Text;
using EntityDataFramework.Core.Models.Query.Contract;

namespace EntityDataFramework.Core.Models.Query.Builder.Contract {
	public interface IConditionsQuerySqlBuilder {
		void SetQueryConditionsSql(IFiltrationQuery filtrationQuery, StringBuilder stringBuilder);
	}
}
