using App.Application.Contracts.Caching;
using App.Caching;

namespace App_API.Extension
{
	public static class CachingExtensions
	{
		public static IServiceCollection AddCachingExt(this IServiceCollection services)
		{
			services.AddMemoryCache();
			services.AddSingleton<ICacheService, CacheService>();

			return services;
		}
	}
}
