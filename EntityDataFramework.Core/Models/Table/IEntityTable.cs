using System.Collections.Generic;
using EntityDataFramework.Core.Models.Column;

namespace EntityDataFramework.Core.Models.Table {
	public interface IEntityTable {
		string Name { get; set; }
		List<IEntityColumn> Columns { get; set; }
	}
}
