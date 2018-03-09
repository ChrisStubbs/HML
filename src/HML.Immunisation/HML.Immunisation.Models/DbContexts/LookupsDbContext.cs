using System.Data.Entity;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Models.Entities;

namespace HML.Immunisation.Models.DbContexts
{
	public partial class LookupsDbContext : BaseDbContext
	{
		public LookupsDbContext(IConfiguration config, IUsernameProvider usernameProvider, ILogger logger) 
			: base(config, usernameProvider, logger)
		{
		}

		public virtual DbSet<ImmunisationProgressRecord> ImmunisationProgresses { get; set; }
		public virtual DbSet<ImmunisationStatusRecord> ImmunisationStatuses { get; set; }
		public virtual DbSet<RecallActionRecord> RecallActions { get; set; }

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

			modelBuilder.Entity<RecallActionRecord>()
				.Property(e => e.Description)
				.IsUnicode(false);

			modelBuilder.Entity<RecallActionRecord>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<RecallActionRecord>()
				.Property(e => e.UpdatedBy)
				.IsUnicode(false);
		}
	}
}
