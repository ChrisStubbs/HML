using System.Collections.Generic;
using HML.Immunisation.Models.Entities;

namespace HML.Immunisation.WebAPI.Infrastructure
{
	public interface ICachedEmployeeDiseaseRiskStatusProvider
	{
		IList<EmployeeDiseaseRiskStatusRecord> GetEmployeesDiseaseRiskStatus(int employeeId);
	}
}