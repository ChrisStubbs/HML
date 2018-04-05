using System.Collections.Generic;
using HML.Immunisation.Common;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Providers.Interfaces;

namespace HML.Immunisation.WebAPI.Infrastructure
{
	public class CachedEmployeeDiseaseRiskStatusProvider : ICachedEmployeeDiseaseRiskStatusProvider
	{
		private readonly ICacheService _cache;
		private readonly IEmployeeDiseaseRiskStatusProvider _employeeDiseaseRiskStatusProvider;


		public CachedEmployeeDiseaseRiskStatusProvider(ICacheService cache, IEmployeeDiseaseRiskStatusProvider employeeDiseaseRiskStatusProvider)
		{
			_cache = cache;
			_employeeDiseaseRiskStatusProvider = employeeDiseaseRiskStatusProvider;
		}

		public IList<EmployeeDiseaseRiskStatusRecord> GetEmployeesDiseaseRiskStatus(int employeeId)
		{
			return _cache.GetOrSet(CacheKeys.EmployeesDiseaseRiskStatus(employeeId),
			() => _employeeDiseaseRiskStatusProvider.GetEmployeesDiseaseRiskStatus(employeeId));
		}
	}
}