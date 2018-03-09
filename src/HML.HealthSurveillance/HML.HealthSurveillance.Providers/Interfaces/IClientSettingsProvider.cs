using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HML.HealthSurveillance.Models.Entities;

namespace HML.HealthSurveillance.Providers.Interfaces
{
	public interface IClientSettingsProvider
	{
		Task<ClientSettingsRecord> GetByClientIdAsync(Guid clientId);
		Task<ClientSettingsRecord> SaveAsync(Guid clientId, ClientSettingsRecord clientSettings);
		Task<IList<ClientSettingsRecord>> GetClientSettingsAsync(bool? enabled);
	}
}