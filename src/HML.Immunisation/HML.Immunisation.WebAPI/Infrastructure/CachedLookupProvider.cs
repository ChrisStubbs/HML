using System.Threading.Tasks;
using HML.Immunisation.Common;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Models.ViewModels;
using HML.Immunisation.Providers.Interfaces;

namespace HML.Immunisation.WebAPI.Infrastructure
{
	public class CachedLookupProvider : ILookupsProvider
	{
		private readonly ICacheService _cacheService;
		private readonly ILookupsProvider _lookupsProvider;

		public CachedLookupProvider(ICacheService cacheService, ILookupsProvider lookupsProvider)
		{
			_cacheService = cacheService;
			_lookupsProvider = lookupsProvider;
		}
		public async Task<Lookups> GetAsync()
		{
			return await _cacheService.GetOrSetAsync(CacheKeys.Lookups, async () => await _lookupsProvider.GetAsync());
		}
	}
}