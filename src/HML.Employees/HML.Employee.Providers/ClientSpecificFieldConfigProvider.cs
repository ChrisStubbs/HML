using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HML.Employee.Common.Interfaces;
using HML.Employee.Models;
using HML.Employee.Models.Entities;
using HML.Employee.Providers.Interfaces;
using LinqKit;

namespace HML.Employee.Providers
{
	public class ClientSpecificFieldConfigProvider : IClientSpecificFieldConfigProvider
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger _logger;
		private readonly IUsernameProvider _usernameProvider;

		public virtual ClientContext GetDbContext()
		{
			var db = new ClientContext(_configuration, _usernameProvider, _logger);
			return db;
		}


		public ClientSpecificFieldConfigProvider(IConfiguration configuration, ILogger logger,
			IUsernameProvider usernameProvider)
		{
			_configuration = configuration;
			_logger = logger;
			_usernameProvider = usernameProvider;
		}

		public async Task<IList<ClientSpecificFieldConfigRecord>> UpdateAsync(
			Guid clientId,
			IList<ClientSpecificFieldConfigRecord> clientSpecificFieldConfigs)
		{
			try
			{

				using (var db = GetDbContext())
				{
					db.Configuration.AutoDetectChangesEnabled = true;

					var existConfigs = await db.ClientSpecificFieldsConfigs.Where(x => x.ClientId == clientId).ToListAsync();

					foreach (var itemToDelete in existConfigs.Where(x => !clientSpecificFieldConfigs.Select(y => y?.Id).Contains(x.Id)))
					{
						itemToDelete.IsDeleted = true;
					}

					foreach (var configItem in clientSpecificFieldConfigs)
					{
						configItem.ClientId = clientId;

						if (configItem.IsTransient)
						{
							db.ClientSpecificFieldsConfigs.Add(configItem);
						}
						else
						{
							var existing = await db.ClientSpecificFieldsConfigs.FindAsync(configItem.Id);
							db.Entry(existing).CurrentValues.SetValues(configItem);
						}
					}

					await db.SaveChangesAsync().ConfigureAwait(false);
					return await GetByClientId(clientId);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(
					$"Failed whilst saving client specific field config  for clientId: {clientId}",
					ex);
				throw;
			}
		}

		public async Task<IList<ClientSpecificFieldConfigRecord>> GetByClientId(Guid clientId)
		{
			return await Search(ByClient(clientId));
		}

		public async Task<IList<ClientSpecificFieldConfigRecord>> Search(
			Expression<Func<ClientSpecificFieldConfigRecord, bool>> filterExpression)
		{
			try
			{
				using (var db = GetDbContext())
				{
					return await db.ClientSpecificFieldsConfigs
						.AsExpandable()
						.Where(filterExpression)
						.ToListAsync()
						.ConfigureAwait(false);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed search employee", ex);
				throw;
			}
		}

		public Expression<Func<ClientSpecificFieldConfigRecord, bool>> ByClient(Guid clientId)
		{
			return e => e.ClientId == clientId;
		}
	}
}