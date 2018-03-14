using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HMLLib.BusinessObjects.Employee;

namespace HML.RestClients.EmployeeApi
{
	public interface IClientSpecificFieldConfigHttpClient
	{
		Task<IList<ClientSpecificFieldConfig>> GetClientSpecificFieldConfigAsync(Guid clientId);
		Task<IList<ClientSpecificFieldConfig>> PutAsync(Guid clientId, IList<ClientSpecificFieldConfig> config);
	}
}