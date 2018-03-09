using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Models;
using HML.Immunisation.Models.DbContexts;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Providers.Interfaces;

namespace HML.Immunisation.Providers
{
	public class DiseaseRiskProvider : IDiseaseRiskProvider
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger _logger;
		private readonly IUsernameProvider _usernameProvider;

		public virtual DiseaseRiskDbContext GetDbContext()
		{
			var db = new DiseaseRiskDbContext(_configuration, _usernameProvider, _logger);
			return db;
		}

		public DiseaseRiskProvider(IConfiguration configuration, ILogger logger,
			IUsernameProvider usernameProvider)
		{
			_configuration = configuration;
			_logger = logger;
			_usernameProvider = usernameProvider;
		}
		public async Task<IList<DiseaseRiskRecord>> GetAllAsync()
		{
			try
			{
				using (var db = GetDbContext())
				{
					return await db.DiseaseRisks
						.ToListAsync()
						.ConfigureAwait(false);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get Disease Risks", ex);
				throw;
			}
		}

		public IList<DiseaseRiskRecord> GetAll()
		{
			return Task.Run(GetAllAsync).Result;
		}
	}
}