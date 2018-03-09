using System.Data.Entity;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Models.Entities;

namespace HML.Immunisation.Models.DbContexts
{
	public partial class ClientSettingsDbContext : BaseDbContext
	{
		public ClientSettingsDbContext(IConfiguration config, IUsernameProvider usernameProvider, ILogger logger) : base(config, usernameProvider, logger)
		{
		}

		public virtual DbSet<ClientDiseaseRiskRecord> ClientDiseaseRisks { get; set; }
		public virtual DbSet<ClientSettingsRecord> ClientSettings { get; set; }
		public virtual DbSet<DiseaseRiskRecord> DiseaseRisks { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			
			modelBuilder.Entity<ClientDiseaseRiskRecord>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<ClientDiseaseRiskRecord>()
				.Property(e => e.UpdatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<ClientSettingsRecord>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<ClientSettingsRecord>()
				.Property(e => e.UpdatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<ClientSettingsRecord>()
				.HasMany(e => e.ClientDiseaseRisks)
				.WithRequired(e => e.ClientSetting)
				.HasForeignKey(e => e.ClientId)
				.WillCascadeOnDelete(false);

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

			//modelBuilder.Entity<DiseaseRiskRecord>()
			//	.HasMany(e => e.ClientDiseaseRisks)
			//	.WithRequired(e => e.DiseaseRisk)
			//	.WillCascadeOnDelete(false);
		}
	}
}
