using System;
using System.Runtime.Caching;
using System.Threading.Tasks;
using HML.Immunisation.Common.Interfaces;

namespace HML.Immunisation.Common
{
	public class InMemoryCache : ICacheService
	{
		public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class
		{
			T item = MemoryCache.Default.Get(cacheKey) as T;
			if (item == null)
			{
				item = getItemCallback();
				MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(30));
			}
			return item;
		}


		public async Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> getItemCallback) where T : class
		{
			T item = MemoryCache.Default.Get(cacheKey) as T;
			if (item == null)
			{
				item = await getItemCallback();
				MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(30));
			}
			return item;
		}
	}
}