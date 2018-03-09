using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HML.Immunisation.Models.Entities;

namespace HML.Immunisation.Providers.Interfaces
{
	public interface IClientSettingsProvider
	{
		Task<ClientSettingsRecord> GetByClientIdAsync(Guid clientId);
		Task<IList<ClientSettingsRecord>> GetClientSettingsAsync(bool? enabled);
		Task<ClientSettingsRecord> SaveAsync(Guid clientId, ClientSettingsRecord clientSettings);
	}
}