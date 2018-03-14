using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HML.RestClients
{
	public interface IEnabledClientsHttpClient
	{
		Task<List<Guid>> GetEnabledClients();
	}
}