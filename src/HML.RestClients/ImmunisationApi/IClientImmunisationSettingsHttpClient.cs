using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HMLLib.BusinessObjects.Immunisation;
using RestSharp;

namespace HML.RestClients.ImmunisationApi
{
	public interface IClientImmunisationSettingsHttpClient :  IEnabledClientsHttpClient
	{
		Task<ClientImmunisationSettings> GetAsync(Guid clientId);
		Task<IRestResponse<ClientImmunisationSettings>> PutAsyncWithResponse(Guid clientId, ClientImmunisationSettings clientImmunisationSetting);
		
	}
}