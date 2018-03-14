using System.Threading.Tasks;
using HMLLib.BusinessObjects.Immunisation;
using RestSharp;

namespace HML.RestClients.ImmunisationApi
{
	public class LookupsHttpClient : BaseImmunisationApiHttpClient, ILookupsHttpClient
	{
		public LookupsHttpClient(IConfig config) : base(config)
		{
		}

		public async Task<ImmunisationLookups> GetLookupsAsync()
		{
			var request = new RestRequest
			{
				Resource = @"Lookups"
			};
			return await ExecuteAsync<ImmunisationLookups>(request).ConfigureAwait(false);
		}
	}
}