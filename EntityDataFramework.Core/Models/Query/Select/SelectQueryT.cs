using EntityDataFramework.Core.Models.Engine;
using EntityDataFramework.Core.Models.Query.Contract;

namespace EntityDataFramework.Core.Models.Query.Select {
	public class SelectQuery<T> : SelectQuery, IFiltrationQuery<T>, ISelectionQuery<T> {
		public SelectQuery(IDbEngine dbEngine) : base(dbEngine, typeof(T).Name) { }
	}
}
