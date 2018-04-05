using System;
using System.Collections.Generic;
using HML.Employee.Models.ViewModels;

namespace HML.Employee.WebAPI.Infrastructure
{
	public interface ICachedEmployeeImportValidatorProvider
	{
		IList<IEmployeeImportMatchCriteria> GetEmployeesImportMatchCriteriaByClient(Guid clientId);
	}
}