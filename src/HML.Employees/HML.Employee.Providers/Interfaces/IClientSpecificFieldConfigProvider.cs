using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HML.Employee.Models.Entities;

namespace HML.Employee.Providers.Interfaces
{
	public interface IClientSpecificFieldConfigProvider
	{
		Task<IList<ClientSpecificFieldConfigRecord>> UpdateAsync(Guid clientId, IList<ClientSpecificFieldConfigRecord> clientSpecificFieldConfigs);
		Task<IList<ClientSpecificFieldConfigRecord>> GetByClientId(Guid clientId);
	}
}