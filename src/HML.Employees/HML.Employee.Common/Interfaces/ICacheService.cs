using System;
using System.Threading.Tasks;

namespace HML.Employee.Common.Interfaces
{
	public interface ICacheService
	{
		T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class;

		Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> getItemCallback) where T : class;
	}
}