using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Models.DbContexts;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace HML.Immunisation.Providers
{
    public class ImmunisationStatusProvider : IImmunisationStatusProvider
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IUsernameProvider _usernameProvider;

        public virtual ImmunisationStatusDbContext GetDbContext()
        {
            var db = new ImmunisationStatusDbContext(_configuration, _usernameProvider, _logger);
            return db;
        }

        public ImmunisationStatusProvider(IConfiguration configuration, ILogger logger,
            IUsernameProvider usernameProvider)
        {
            _configuration = configuration;
            _logger = logger;
            _usernameProvider = usernameProvider;
        }

        public async Task<IList<ImmunisationStatusRecord>> GetAllAsync()
        {
            try
            {
                using (var db = GetDbContext())
                {
                    return await db.ImmunisationsStatuses
                        .ToListAsync()
                        .ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Immunisation statuses", ex);
                throw;
            }
        }
        public IList<ImmunisationStatusRecord> GetAll()
        {
            return Task.Run(GetAllAsync).Result;
        }


    }
}
