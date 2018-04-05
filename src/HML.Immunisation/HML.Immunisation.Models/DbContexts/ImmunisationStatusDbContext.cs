using System.Data.Entity;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Models.Entities;

namespace HML.Immunisation.Models.DbContexts
{
    public class ImmunisationStatusDbContext : BaseDbContext
    {
        public ImmunisationStatusDbContext(IConfiguration config, IUsernameProvider usernameProvider, ILogger logger) : base(config, usernameProvider, logger)
        {
        }

        public virtual DbSet<ImmunisationStatusRecord> ImmunisationsStatuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImmunisationProgressRecord>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ImmunisationProgressRecord>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<ImmunisationProgressRecord>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<ImmunisationStatusRecord>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ImmunisationStatusRecord>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<ImmunisationStatusRecord>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);
        }
    }
}
