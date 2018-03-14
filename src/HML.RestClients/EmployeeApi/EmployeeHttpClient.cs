using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HMLLib.BusinessObjects.Employee;
using RestSharp;

namespace HML.RestClients.EmployeeApi
{
	public class EmployeeHttpClient : BaseEmployeeApiHttpClient, IEmployeeHttpClient
	{
        public EmployeeHttpClient(IConfig config) : base(config)
		{
		}

        public async Task<IList<Employee>> GetClientsEmployeesAsync(Guid clientId)
		{
			var request = new RestRequest
			{
				Resource = $"clients/{clientId}/employees",
				RootElement = "Employee"
			};
			var employees = await ExecuteAsync<List<Employee>>(request).ConfigureAwait(false); ;

			return employees ?? new List<Employee>();
		}

		public async Task<Employee> GetEmployeeAsync(int employeeId)
		{
			var request = new RestRequest
			{
				Resource = $"employees/{employeeId}",
				RootElement = "Employee"
			};
			return await ExecuteAsync<Employee>(request);
		}

		public async Task<List<Employee>> Search(IEmployeeSearchParameters employeeSearchParameters)
		{
			var request = new RestRequest
			{
				Resource = $"employees/?caseEmployeeId={employeeSearchParameters.CaseEmployeeId}",
				RootElement = "Employee"
			};
			return await ExecuteAsync<List<Employee>>(request);

		}
        public async Task<List<Employee>> Search(ILinkedCaseSearchParameters linkedCaseSearchParameters)
        {
            DateTime dt = linkedCaseSearchParameters.Dob;
            var dateValue = dt.ToString("dd/MM/yyyy");
            var request = new RestRequest
            {
                Resource = $"employees/?clientId={linkedCaseSearchParameters.ClientId}&FirstName={linkedCaseSearchParameters.FirstName}&LastName={linkedCaseSearchParameters.LastName}&dob={dateValue}",
                RootElement = "Employee"
            };
            return await ExecuteAsync<List<Employee>>(request);

        }


        public async Task<Employee> PostAsync(Employee employee)
		{
			var response = await PostAsyncWithResponse(employee);
			return response.Data;
		}

		public async Task<IRestResponse<Employee>> PostAsyncWithResponse(Employee employee)
		{
			var request = new RestRequest
			{
				Resource = "Employees",
				RootElement = "Employee",
				RequestFormat = DataFormat.Json,
				Method = Method.POST
			};

			request.AddBody(employee);
			return await ExecuteAsyncWithResponse<Employee>(request);
		}

		public async Task<Employee> PutAsync(Employee employee)
		{
			var response = await PutAsyncWithResponse(employee);
			return response.Data;
		}

		public async Task<IRestResponse<Employee>> PutAsyncWithResponse(Employee employee)
		{
			var request = new RestRequest
			{
				Resource = "Employees",
				RootElement = "Employee",
				RequestFormat = DataFormat.Json,
				Method = Method.PUT,
			};

			request.AddBody(employee);
			return await ExecuteAsyncWithResponse<Employee>(request);
		}


	}
}