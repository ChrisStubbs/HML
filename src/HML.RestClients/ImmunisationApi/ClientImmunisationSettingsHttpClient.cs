using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HMLLib.BusinessObjects.Immunisation;
using RestSharp;

namespace HML.RestClients.ImmunisationApi
{
	public class ClientImmunisationSettingsHttpClient : BaseImmunisationApiHttpClient, IClientImmunisationSettingsHttpClient
	{
		public ClientImmunisationSettingsHttpClient(IConfig config) : base(config)
		{
		}

		public async Task<ClientImmunisationSettings> GetAsync(Guid clientId)
		{
			var request = new RestRequest
			{
				Resource = $"clients/{clientId}/settings"
			};
			var settings = await ExecuteAsync<ClientImmunisationSettings>(request);
			return settings ?? new ClientImmunisationSettings { Id = clientId };

		}

		public async Task<IRestResponse<ClientImmunisationSettings>> PutAsyncWithResponse(Guid clientId, ClientImmunisationSettings clientImmunisationSetting)
		{
			var request = new RestRequest
			{
				Resource = $"clients/{clientId}/settings",
				RequestFormat = DataFormat.Json,
				Method = Method.PUT
			};

			request.AddBody(clientImmunisationSetting);
			return await ExecuteAsyncWithResponse<ClientImmunisationSettings>(request);
		}

		public async Task<List<Guid>> GetEnabledClients()
		{
			var request = new RestRequest
			{
				Resource = $"clients/enabled"
			};
			var enabledGuids = await ExecuteAsync<List<Guid>>(request);
			return enabledGuids ?? new List<Guid>();
		}
	}
}