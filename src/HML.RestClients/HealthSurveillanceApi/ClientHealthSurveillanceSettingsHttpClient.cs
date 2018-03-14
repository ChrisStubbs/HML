using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HMLLib.BusinessObjects.HealthSurveillance;
using RestSharp;

namespace HML.RestClients.HealthSurveillanceApi
{
	public class ClientHealthSurveillanceSettingsHttpClient : BaseHealthSurveillanceApiHttpClient, IClientHealthSurveillanceSettingsHttpClient
	{
		public ClientHealthSurveillanceSettingsHttpClient(IConfig config) : base(config)
		{
		}

		public async Task<ClientHealthSurveillanceSettings> GetAsync(Guid clientId)
		{
			var request = new RestRequest
			{
				Resource = $"clients/{clientId}/settings"
			};
			var settings = await ExecuteAsync<ClientHealthSurveillanceSettings>(request);

			return settings;
		}

		public async Task<IRestResponse<ClientHealthSurveillanceSettings>> PutAsyncWithResponse(Guid clientId, ClientHealthSurveillanceSettings clientHealthSurveillanceSettings)
		{
			var request = new RestRequest
			{
				Resource = $"clients/{clientId}/settings",
				RequestFormat = DataFormat.Json,
				Method = Method.PUT
			};

			request.AddBody(clientHealthSurveillanceSettings);
			return await ExecuteAsyncWithResponse<ClientHealthSurveillanceSettings>(request);
		}

		public async Task<List<Guid>> GetEnabledClients()
		{
			var request = new RestRequest
			{
				Resource = "clients/enabled"
			};
			var enabledGuids = await ExecuteAsync<List<Guid>>(request);
			return enabledGuids ?? new List<Guid>();
		}
	}
}