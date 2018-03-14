using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HMLLib.BusinessObjects.Employee;
using RestSharp;

namespace HML.RestClients.EmployeeApi
{
	public class ClientSpecificFieldConfigHttpClient : BaseEmployeeApiHttpClient, IClientSpecificFieldConfigHttpClient
	{

		public ClientSpecificFieldConfigHttpClient(IConfig config) : base(config)
		{
		}

		public async Task<IList<ClientSpecificFieldConfig>> GetClientSpecificFieldConfigAsync(Guid clientId)
		{
			var request = new RestRequest
			{
				Resource = $"clients/{clientId}/client-specific-fields-config",
			};
			var configs = await ExecuteAsync<List<ClientSpecificFieldConfig>>(request);

			return configs ?? new List<ClientSpecificFieldConfig>();
		}

		public async Task<IList<ClientSpecificFieldConfig>> PutAsync(Guid clientId, IList<ClientSpecificFieldConfig> config)
		{
			var request = new RestRequest
			{
				Resource = $"clients/{clientId}/client-specific-fields-config",
				RequestFormat = DataFormat.Json,
				Method = Method.PUT
			};

			request.AddBody(config);
			return await ExecuteAsync<List<ClientSpecificFieldConfig>>(request);
		}

	}
}