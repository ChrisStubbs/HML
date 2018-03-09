using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HML.HealthSurveillance.Common.Interfaces;
using HML.HealthSurveillance.Models;
using HML.HealthSurveillance.Models.Entities;
using HML.HealthSurveillance.Providers.Interfaces;

namespace HML.HealthSurveillance.Providers
{
	public class ClientSettingsProvider : IClientSettingsProvider
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger _logger;
		private readonly IUsernameProvider _usernameProvider;

		public virtual ClientSettingsDbContext GetDbContext()
		{
			var db = new ClientSettingsDbContext(_configuration, _usernameProvider, _logger);
			return db;
		}

		public ClientSettingsProvider(IConfiguration configuration, ILogger logger,
		IUsernameProvider usernameProvider)
		{
			_configuration = configuration;
			_logger = logger;
			_usernameProvider = usernameProvider;
		}
		public async Task<ClientSettingsRecord> GetByClientIdAsync(Guid clientId)
		{
			try
			{
				using (var db = GetDbContext())
				{
					return await db.ClientSettings
						.SingleOrDefaultAsync(x => x.Id == clientId)
						.ConfigureAwait(false);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get Client Settings Record", ex);
				throw;
			}
		}
		public async Task<ClientSettingsRecord> SaveAsync(Guid clientId, ClientSettingsRecord clientSettings)
		{
			try
			{
				using (var db = GetDbContext())
				{
					var existing = await db.ClientSettings.FindAsync(clientId).ConfigureAwait(false);
					if (existing == null) return await CreateAsync(clientSettings);

					db.Configuration.AutoDetectChangesEnabled = true;
					db.Entry(existing).CurrentValues.SetValues(clientSettings);

					await db.SaveChangesAsync().ConfigureAwait(false);
					return existing;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed whilst saving Client Settings {clientSettings.Id}", ex);
				throw;
			}
		}

		public async Task<IList<ClientSettingsRecord>> GetClientSettingsAsync(bool? enabled)
		{
			try
			{
				using (var db = GetDbContext())
				{
					if (!enabled.HasValue)
					{
						return await db.ClientSettings.ToListAsync();
					}
					return await db.ClientSettings.Where(x => x.IsEnabled == enabled.Value).ToListAsync();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get Client Settings Record", ex);
				throw;
			}
		}

		private async Task<ClientSettingsRecord> CreateAsync(ClientSettingsRecord clientSettings)
		{
			try
			{
				using (var db = GetDbContext())
				{
					db.ClientSettings.Add(clientSettings);
					await db.SaveChangesAsync().ConfigureAwait(false);
					return clientSettings;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed whilst creating client setting", ex);
				throw;
			}
		}
	}
}
