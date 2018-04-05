using System.Data.Entity;
using HML.Employee.Common.Interfaces;
using HML.Employee.Models.Entities;

namespace HML.Employee.Models
{
	public partial class NoteContext : BaseDbContext
	{
	
		public NoteContext(IConfiguration config, IUsernameProvider usernameProvider, ILogger logger) : base(config, usernameProvider, logger)
		{
		}
        public virtual DbSet<NoteRecord> Notes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
		
			modelBuilder.Entity<NoteRecord>()
				.Property(e => e.Comment)
				.IsUnicode(false);

            modelBuilder.Entity<NoteRecord>()
                .Property(e => e.DocumentLocation)
                .IsUnicode(false);
            modelBuilder.Entity<NoteRecord>()
                .Property(e => e.DocumentName)
                .IsUnicode(false);
            modelBuilder.Entity<NoteRecord>()
               .Property(e => e.FileType)
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
