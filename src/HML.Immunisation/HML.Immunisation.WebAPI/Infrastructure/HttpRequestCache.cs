using System;
using System.Threading.Tasks;
using System.Web;
using HML.Immunisation.Common.Interfaces;

namespace HML.Immunisation.WebAPI.Infrastructure
{
	public class HttpRequestCache : ICacheService
	{

		public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class
		{
			T item = HttpContext.Current.Items[cacheKey] as T;
			if (item == null)
			{
				item = getItemCallback();
				HttpContext.Current.Items.Add(cacheKey, item);
			}
			return item;
		}

		public async Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> getItemCallback) where T : class
		{
			T item = HttpContext.Current.Items[cacheKey] as T;
			if (item == null)
			{
				item = await getItemCallback();
				HttpContext.Current.Items.Add(cacheKey, item);
			}
			return item;
		}
	}
}