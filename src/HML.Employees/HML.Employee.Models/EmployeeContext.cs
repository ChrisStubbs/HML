using System.Data.Entity;
using HML.Employee.Common.Interfaces;
using HML.Employee.Models.Entities;

namespace HML.Employee.Models
{
	public partial class EmployeeContext : BaseDbContext
	{
	
		public EmployeeContext(IConfiguration config, IUsernameProvider usernameProvider, ILogger logger) : base(config, usernameProvider, logger)
		{
		}

		public virtual DbSet<AddressRecord> Addresses { get; set; }
		public virtual DbSet<ContactRecord> Contacts { get; set; }
		public virtual DbSet<EmployeeRecord> Employees { get; set; }
              
        public virtual DbSet<NoteRecord> Notes { get; set; }
		public virtual DbSet<ClientSpecificFieldRecord> ClientSpecificFields { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AddressRecord>()
				.Property(e => e.Lines)
				.IsUnicode(false);

			modelBuilder.Entity<AddressRecord>()
				.Property(e => e.Postcode)
				.IsUnicode(false);

			modelBuilder.Entity<AddressRecord>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<AddressRecord>()
				.Property(e => e.UpdatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<ClientSpecificFieldRecord>()
				.Property(e => e.Value)
				.IsUnicode(false);

			modelBuilder.Entity<ClientSpecificFieldRecord>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<ClientSpecificFieldRecord>()
				.Property(e => e.UpdatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<ContactRecord>()
				.Property(e => e.Value)
				.IsUnicode(false);

			modelBuilder.Entity<ContactRecord>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<ContactRecord>()
				.Property(e => e.UpdatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<EmployeeRecord>()
				.Property(e => e.EmployeeId)
				.IsUnicode(false);

			modelBuilder.Entity<EmployeeRecord>()
				.Property(e => e.Title)
				.IsUnicode(false);

			modelBuilder.Entity<EmployeeRecord>()
				.Property(e => e.FirstName)
				.IsUnicode(false);

			modelBuilder.Entity<EmployeeRecord>()
				.Property(e => e.LastName)
				.IsUnicode(false);

			modelBuilder.Entity<EmployeeRecord>()
				.Property(e => e.JobTitle)
				.IsUnicode(false);

			modelBuilder.Entity<EmployeeRecord>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<EmployeeRecord>()
				.Property(e => e.UpdatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<EmployeeRecord>()
				.HasMany(e => e.Addresses)
				.WithRequired(e => e.Employee)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<EmployeeRecord>()
				.HasMany(e => e.Contacts)
				.WithRequired(e => e.Employee)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<EmployeeRecord>()
				.HasMany(e => e.ClientSpecificFields)
				.WithRequired(e => e.Employee)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<NoteRecord>()
				.Property(e => e.Text)
				.IsUnicode(false);

			modelBuilder.Entity<NoteRecord>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<NoteRecord>()
				.Property(e => e.UpdatedBy)
				.IsUnicode(false);
		
		}
	}

}
