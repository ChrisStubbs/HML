using System.Data.Entity;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Models.Entities;

namespace HML.Immunisation.Models.DbContexts
{
	public partial class EmployeeDiseaseRiskStatusDbContext : BaseDbContext
	{
		public EmployeeDiseaseRiskStatusDbContext(IConfiguration config, IUsernameProvider usernameProvider, ILogger logger) : base(config, usernameProvider, logger)
		{
		}

		public virtual DbSet<EmployeeDiseaseRiskStatusRecord> EmployeeDiseaseRiskStatuses { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<EmployeeDiseaseRiskStatusRecord>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<EmployeeDiseaseRiskStatusRecord>()
				.Property(e => e.UpdatedBy)
				.IsUnicode(false);
		}
	}
}
