using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HML.Immunisation.Models.DbContexts;
using HML.Immunisation.Models.Entities;

namespace HML.Immunisation.Providers.Interfaces
{
	public interface IEmployeeDiseaseRiskStatusProvider
	{
		EmployeeDiseaseRiskStatusDbContext GetDbContext();
		Task<IList<EmployeeDiseaseRiskStatusRecord>> GetEmployeesDiseaseRiskStatusAsync(int employeeId);
		Task<IList<EmployeeDiseaseRiskStatusRecord>> GetClientsEmployeesDiseaseRiskStatusAsync(Guid clientId);
		IList<EmployeeDiseaseRiskStatusRecord> GetEmployeesDiseaseRiskStatus(int employeeId);
		Task<IList<EmployeeDiseaseRiskStatusRecord>> SaveAsync(int employeeId, IList<EmployeeDiseaseRiskStatusRecord> statuses);
		Task<IList<EmployeeDiseaseRiskStatusRecord>> GetEmployeesDiseaseRiskStatusHistoryAsync(int employeeId, int diseaseRiskId);
		int NoOfEmployeesWithDiseaseRiskRequiredForRole(Guid clientId, int diseaseriskId);

	}
}