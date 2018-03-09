using System.Data.Entity;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Models.Entities;

namespace HML.Immunisation.Models.DbContexts
{
	public class DiseaseRiskDbContext : BaseDbContext
	{
		public DiseaseRiskDbContext(IConfiguration config, IUsernameProvider usernameProvider, ILogger logger) : base(config, usernameProvider, logger)
		{
		}

		public virtual DbSet<DiseaseRiskRecord> DiseaseRisks { get; set; }
		public virtual DbSet<ClientDiseaseRiskRecord> ClientDiseaseRisks { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DiseaseRiskRecord>()
				.Property(e => e.Code)
				.IsUnicode(false);

			modelBuilder.Entity<DiseaseRiskRecord>()
				.Property(e => e.Description)
				.IsUnicode(false);

			modelBuilder.Entity<DiseaseRiskRecord>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<DiseaseRiskRecord>()
				.Property(e => e.UpdatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<ClientDiseaseRiskRecord>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<ClientDiseaseRiskRecord>()
				.Property(e => e.UpdatedBy)
				.IsUnicode(false);
		}
	}
}