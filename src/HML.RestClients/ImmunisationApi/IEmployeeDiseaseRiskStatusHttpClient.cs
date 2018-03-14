using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HMLLib.BusinessObjects.Immunisation;
using RestSharp;

namespace HML.RestClients.ImmunisationApi
{
	public interface IEmployeeDiseaseRiskStatusHttpClient
	{
		Task<IList<EmployeeDiseaseRiskStatus>> GetEmployeeDiseaseRiskStatusesAsync(Guid clientId, int employeeId);
		Task<IRestResponse<List<EmployeeDiseaseRiskStatus>>> PutEmployeeDiseaseRiskStatusesRisksAsync(int employeeId, IList<EmployeeDiseaseRiskStatus> employeeDiseaseRiskStatuses);
		Task<IList<EmployeeDiseaseRiskStatus>> GetClientsEmployeeDiseaseRiskStatusesAsync(Guid clientId);
        Task<IList<EmployeeDiseaseRiskStatus>> GetEmployeeDiseaseRiskStatuseHistoryAsync(int employeeId, int diseaseRiskId);
        Task<IRestResponse<List<EmployeeDiseaseRiskStatus>>> ValidateAsync( int employeeId,IList<EmployeeDiseaseRiskStatus> employeeDiseaseRiskStatuses);
    }
}