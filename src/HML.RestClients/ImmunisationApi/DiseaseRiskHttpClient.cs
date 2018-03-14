using System.Collections.Generic;
using System.Threading.Tasks;
using HMLLib.BusinessObjects.Immunisation;
using RestSharp;

namespace HML.RestClients.ImmunisationApi
{
	public class DiseaseRiskHttpClient : BaseImmunisationApiHttpClient, IDiseaseRiskHttpClient
	{
		public DiseaseRiskHttpClient(IConfig config) : base(config)
		{
		}

		public async Task<IList<DiseaseRisk>> GetDiseaseRisksAsync()
		{
			var request = new RestRequest
			{
				Resource = "disease-risks"
			};
			var risks = await ExecuteAsync<List<DiseaseRisk>>(request).ConfigureAwait(false);

			return risks ?? new List<DiseaseRisk>();
		}
	}
}