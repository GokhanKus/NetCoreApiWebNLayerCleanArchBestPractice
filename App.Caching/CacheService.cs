using App.Application.Contracts.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace App.Caching
{
	public class CacheService(IMemoryCache memoryCache) : ICacheService
	{
		public Task<T?> GetAsync<T>(string cacheKey)
		{
			var cachedData = memoryCache.TryGetValue(cacheKey, out T? cachedItem);

			if (!cachedData)
				return Task.FromResult(default(T));

			return Task.FromResult(cachedItem);
			//asenkron metot icerisinde senkron kod kullanılırsa geriye Task.FromResult, Task.CompletedTask vs. donulebilir
		}
		public Task AddAsync<T>(string cacheKey, T value, TimeSpan exprTimeSpan)
		{
			var cacheOptions = new MemoryCacheEntryOptions()
			{
				AbsoluteExpirationRelativeToNow = exprTimeSpan
			};
			memoryCache.Set(cacheKey, value, cacheOptions);
			return Task.CompletedTask;
		}
		public Task RemoveAsync(string cacheKey)
		{
			memoryCache.Remove(cacheKey);
			return Task.CompletedTask;
		}
	}
}
