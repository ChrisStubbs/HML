using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HMLLib.BusinessObjects.Employee;
using RestSharp;

namespace HML.RestClients.EmployeeApi
{
	public interface IEmployeeHttpClient
	{
		Task<IList<Employee>> GetClientsEmployeesAsync(Guid clientId);
		Task<Employee> GetEmployeeAsync(int employeeId);
		Task<Employee> PostAsync(Employee employee);
		Task<IRestResponse<Employee>> PostAsyncWithResponse(Employee employee);
		Task<Employee> PutAsync(Employee employee);
		Task<IRestResponse<Employee>> PutAsyncWithResponse(Employee employee);
		Task<List<Employee>> Search(IEmployeeSearchParameters employeeSearchParameters);
	}
}