using EntityDataFramework.Core.Models.Engine;
using Microsoft.Extensions.DependencyInjection;

namespace EntityDataFramework.Core.Extensions {
	public static class EntityDataExtensions {
		public static void UseEntityData(this IServiceCollection serviceCollection, IDbEngine dbEngine) {
			serviceCollection.AddSingleton(provider => dbEngine);
		}
	}
}