using System.Data.Entity;
using HML.Employee.Common.Interfaces;
using HML.Employee.Models.Entities;

namespace HML.Employee.Models
{
	public partial class ClientContext : BaseDbContext
	{
		public ClientContext(IConfiguration config, IUsernameProvider usernameProvider, ILogger logger) : base(config, usernameProvider, logger)
		{
		}

		public virtual DbSet<ClientSpecificFieldConfigRecord> ClientSpecificFieldsConfigs { get; set; }


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ClientSpecificFieldConfigRecord>()
				.Property(e => e.FieldName)
				.IsUnicode(false);

			modelBuilder.Entity<ClientSpecificFieldConfigRecord>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<ClientSpecificFieldConfigRecord>()
				.Property(e => e.UpdatedBy)
				.IsUnicode(false);
		}

	
	}
}
