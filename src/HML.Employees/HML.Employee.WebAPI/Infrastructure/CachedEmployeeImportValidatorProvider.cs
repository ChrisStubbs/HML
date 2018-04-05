using System;
using System.Collections.Generic;
using HML.Employee.Common.Interfaces;
using HML.Employee.Models.ViewModels;
using HML.Employee.Providers.Interfaces;

namespace HML.Employee.WebAPI.Infrastructure
{
	public class CachedEmployeeImportValidatorProvider : ICachedEmployeeImportValidatorProvider
	{
		private readonly ICacheService _cache;
		private readonly IEmployeeProvider _employeeProvider;

		public CachedEmployeeImportValidatorProvider(ICacheService cache, IEmployeeProvider employeeProvider)
		{
			_cache = cache;
			_employeeProvider = employeeProvider;
		}

		public IList<IEmployeeImportMatchCriteria> GetEmployeesImportMatchCriteriaByClient(Guid clientId)
		{
			return _cache.GetOrSet(CacheKeys.EmployeeImportMatchCtriteriaCacheKey(clientId),
				() => _employeeProvider.GetEmployeesImportMatchCriteriaByClient(clientId));
		}
	}
}