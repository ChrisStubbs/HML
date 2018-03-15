using System.Collections.Generic;
using System.Threading.Tasks;
using HML.Immunisation.Common;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Providers.Interfaces;

namespace HML.Immunisation.WebAPI.Infrastructure
{
	public class CachedDiseaseRiskProvider : IDiseaseRiskProvider
	{
		private readonly ICacheService _cachedService;
		private readonly IDiseaseRiskProvider _diseaseRiskProvider;

		public CachedDiseaseRiskProvider(ICacheService cachedService, IDiseaseRiskProvider diseaseRiskProvider)
		{
			_cachedService = cachedService;
			_diseaseRiskProvider = diseaseRiskProvider;
		}

		public IList<DiseaseRiskRecord> GetAll()
		{
			return _cachedService.GetOrSet(CacheKeys.DiseaseRisks, () => _diseaseRiskProvider.GetAll());
		}

		public async Task<IList<DiseaseRiskRecord>> GetAllAsync()
		{
			return 
				await
				_cachedService.GetOrSetAsync(CacheKeys.DiseaseRisks, async () => await  _diseaseRiskProvider.GetAllAsync());
		}
	}
}