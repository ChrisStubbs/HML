using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Models;
using HML.Immunisation.Models.DbContexts;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Models.ViewModels;
using HML.Immunisation.Providers.Interfaces;

namespace HML.Immunisation.Providers
{
	public class LookupsProvider : ILookupsProvider
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger _logger;
		private readonly IUsernameProvider _usernameProvider;

		public virtual LookupsDbContext GetDbContext()
		{

			//var db = new LookupsDbContext(_configuration, _usernameProvider, _logger);
			var db = new LookupsDbContext(_configuration, _usernameProvider, _logger);
			return db;
		}

		public LookupsProvider(IConfiguration configuration, ILogger logger,
			IUsernameProvider usernameProvider)
		{
			_configuration = configuration;
			_logger = logger;
			_usernameProvider = usernameProvider;
		}

		public async Task<Lookups> GetAsync()
		{
			try
			{
				Task<List<ImmunisationProgressRecord>> taskProgresses;
				Task<List<ImmunisationStatusRecord>> taskStatuses;
				Task<List<RecallActionRecord>> taskRecallActions;

				using (var db = GetDbContext())
				using (var db1 = GetDbContext())
				using (var db2 = GetDbContext())
				{
					taskProgresses = db.ImmunisationProgresses.ToListAsync();
					taskStatuses = db1.ImmunisationStatuses.ToListAsync();
					taskRecallActions = db2.RecallActions.ToListAsync();
					await Task.WhenAll(taskProgresses, taskStatuses, taskRecallActions).ConfigureAwait(false);
				}

				return new Lookups(taskProgresses.Result, taskStatuses.Result, taskRecallActions.Result);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get lookups", ex);
				throw;
			}
		}
	}
}