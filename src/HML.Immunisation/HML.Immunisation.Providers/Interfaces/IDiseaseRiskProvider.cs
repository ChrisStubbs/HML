using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HML.Immunisation.Models.Entities;

namespace HML.Immunisation.Providers.Interfaces
{
	public interface IDiseaseRiskProvider
	{
		Task<IList<DiseaseRiskRecord>> GetAllAsync();
		IList<DiseaseRiskRecord> GetAll();
	}
}
