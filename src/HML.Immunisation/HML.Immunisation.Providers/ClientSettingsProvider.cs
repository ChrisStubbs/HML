using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Models.DbContexts;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Providers.Interfaces;

namespace HML.Immunisation.Providers
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
						.Include(x => x.ClientDiseaseRisks)
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

					DeleteChildren<ClientDiseaseRiskRecord, int>(clientSettings.ClientDiseaseRisks, existing.ClientDiseaseRisks);

					AddUpdateClientDiseaseRisks(clientId, clientSettings, existing, db);

					await db.SaveChangesAsync().ConfigureAwait(false);
					return existing;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed whilst saving clientSettings {clientSettings.Id}", ex);
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

		private static void AddUpdateClientDiseaseRisks(Guid clientId, ClientSettingsRecord clientSettings, ClientSettingsRecord existing,
			ClientSettingsDbContext db)
		{
			foreach (var clientRisk in clientSettings.ClientDiseaseRisks)
			{
				clientRisk.ClientId = clientId;
				var existingEntity = existing.ClientDiseaseRisks.SingleOrDefault(c => !clientRisk.IsTransient && c.Id == clientRisk.Id);
				if (existingEntity != null)
				{
					db.Entry(existingEntity).CurrentValues.SetValues(clientRisk);
				}
				else
				{
					existing.ClientDiseaseRisks.Add(clientRisk);
				}
			}
		}

		//TODO: may need to move this to a base provider for reuse!
		static void DeleteChildren<T, TB>(ICollection<T> newList, ICollection<T> existingList) where T : BaseEntity<TB>
		{
			if (existingList == null) throw new ArgumentNullException(nameof(existingList));
			foreach (var existingEntity in existingList)
			{
				if (newList.All(x => !x.Id.Equals(existingEntity.Id) && !existingEntity.IsDeleted))
				{
					existingEntity.IsDeleted = true;
				}
			}
		}
	}
}