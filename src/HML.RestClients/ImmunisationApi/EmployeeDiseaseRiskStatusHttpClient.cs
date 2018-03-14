using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HMLLib.BusinessObjects.Immunisation;
using RestSharp;

namespace HML.RestClients.ImmunisationApi
{
	public class EmployeeDiseaseRiskStatusHttpClient : BaseImmunisationApiHttpClient, IEmployeeDiseaseRiskStatusHttpClient
	{
		public EmployeeDiseaseRiskStatusHttpClient(IConfig config) : base(config)
		{
		}

		public async Task<IList<EmployeeDiseaseRiskStatus>> GetEmployeeDiseaseRiskStatusesAsync(Guid clientId, int employeeId)
		{
			var request = new RestRequest
			{
				Resource = $"clients/{clientId}/employees/{employeeId}/disease-risk-status"
			};
			var risks = await ExecuteAsync<List<EmployeeDiseaseRiskStatus>>(request).ConfigureAwait(false);

			return risks ?? new List<EmployeeDiseaseRiskStatus>();
		}

		public async Task<IRestResponse<List<EmployeeDiseaseRiskStatus>>> PutEmployeeDiseaseRiskStatusesRisksAsync(int employeeId,
			IList<EmployeeDiseaseRiskStatus> employeeDiseaseRiskStatuses)
		{
			var request = new RestRequest
			{
				Resource = $"employees/{employeeId}/disease-risk-status",
				RequestFormat = DataFormat.Json,
				Method = Method.PUT
			};
			request.AddBody(employeeDiseaseRiskStatuses);
			return await ExecuteAsyncWithResponse<List<EmployeeDiseaseRiskStatus>>(request);
		}

		public async Task<IList<EmployeeDiseaseRiskStatus>> GetClientsEmployeeDiseaseRiskStatusesAsync(Guid clientId)
		{
			var request = new RestRequest
			{
				Resource = $"clients/{clientId}/employees/disease-risk-status"
			};

			var risks = await ExecuteAsync<List<EmployeeDiseaseRiskStatus>>(request).ConfigureAwait(false);
			return risks ?? new List<EmployeeDiseaseRiskStatus>();
		}

		public async Task<IList<EmployeeDiseaseRiskStatus>> GetEmployeeDiseaseRiskStatuseHistoryAsync(int employeeId, int diseaseRiskId)
		{
			var request = new RestRequest
			{
				Resource = $"employees/{employeeId}/disease-risk-status/{diseaseRiskId}/history"
			};

			var risks = await ExecuteAsync<List<EmployeeDiseaseRiskStatus>>(request).ConfigureAwait(false);
			return risks ?? new List<EmployeeDiseaseRiskStatus>();
		}

		public async Task<IRestResponse<List<EmployeeDiseaseRiskStatus>>> ValidateAsync(int employeeId, IList<EmployeeDiseaseRiskStatus> employeeDiseaseRiskStatuses)
		{
			var request = new RestRequest
			{
				Resource = $"employees/{employeeId}/validate",
				RequestFormat = DataFormat.Json,
				Method = Method.PUT
			};
			request.AddBody(employeeDiseaseRiskStatuses);
			return await ExecuteAsyncWithResponse<List<EmployeeDiseaseRiskStatus>>(request);
		}
	}
}