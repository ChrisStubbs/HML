using System;

namespace HML.Employee.Common.Interfaces
{
	public class CacheKeys
	{
		public static string EmployeeImportMatchCtriteriaCacheKey(Guid clientId)
		{
			return $"EmployeeImportMatchCtriteriaCacheKey-{clientId}";
		}
	}
}