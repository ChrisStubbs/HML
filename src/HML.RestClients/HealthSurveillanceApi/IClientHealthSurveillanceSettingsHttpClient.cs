using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HMLLib.BusinessObjects.HealthSurveillance;
using RestSharp;

namespace HML.RestClients.HealthSurveillanceApi
{
	public interface IClientHealthSurveillanceSettingsHttpClient : IEnabledClientsHttpClient
	{
		Task<ClientHealthSurveillanceSettings> GetAsync(Guid clientId);
		Task<IRestResponse<ClientHealthSurveillanceSettings>> PutAsyncWithResponse(Guid clientId, ClientHealthSurveillanceSettings clientHealthSurveillanceSettings);
	}
}