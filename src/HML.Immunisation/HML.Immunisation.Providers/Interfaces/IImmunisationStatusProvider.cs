using HML.Immunisation.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HML.Immunisation.Providers.Interfaces
{
    public interface IImmunisationStatusProvider
    {
        Task<IList<ImmunisationStatusRecord>> GetAllAsync();
        IList<ImmunisationStatusRecord> GetAll();
    }
}
