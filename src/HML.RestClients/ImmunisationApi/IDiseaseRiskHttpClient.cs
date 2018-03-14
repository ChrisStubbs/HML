using System.Collections.Generic;
using System.Threading.Tasks;
using HMLLib.BusinessObjects.Immunisation;

namespace HML.RestClients.ImmunisationApi
{
	public interface IDiseaseRiskHttpClient
	{
		Task<IList<DiseaseRisk>> GetDiseaseRisksAsync();
	}
}