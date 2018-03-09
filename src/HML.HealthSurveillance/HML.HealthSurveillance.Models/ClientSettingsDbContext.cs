using System.Data.Entity;
using HML.HealthSurveillance.Common.Interfaces;
using HML.HealthSurveillance.Models.Entities;

namespace HML.HealthSurveillance.Models
{
	public partial class ClientSettingsDbContext : BaseDbContext
	{
		public ClientSettingsDbContext(IConfiguration config, IUsernameProvider usernameProvider, ILogger logger) : base(config, usernameProvider, logger)
		{

		}

		public virtual DbSet<ClientSettingsRecord> ClientSettings { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ClientSettingsRecord>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<ClientSettingsRecord>()
				.Property(e => e.UpdatedBy)
				.IsUnicode(false);
		}
	}
}
