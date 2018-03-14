using System.Threading.Tasks;
using HMLLib.BusinessObjects.Immunisation;

namespace HML.RestClients.ImmunisationApi
{
	public interface ILookupsHttpClient
	{
		Task<ImmunisationLookups> GetLookupsAsync();
	}
}