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
		IList<EmployeeDiseaseRiskStatusRecord> GetEmployeesDiseaseRiskStatus(int employeeId);
		Task<IList<EmployeeDiseaseRiskStatusRecord>> SaveAsync(int employeeId, IList<EmployeeDiseaseRiskStatusRecord> statuses);
		
	}
}