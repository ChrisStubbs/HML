using System.Threading.Tasks;
using HML.Immunisation.Models.ViewModels;

namespace HML.Immunisation.Providers.Interfaces
{
	public interface ILookupsProvider
	{
		Task<Lookups> GetAsync();
	}
}