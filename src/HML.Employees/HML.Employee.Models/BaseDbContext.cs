using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HML.Employee.Common.Interfaces;
using HML.Employee.Models.Entities;

namespace HML.Employee.Models
{
	public abstract class BaseDbContext : DbContext
	{
		private readonly IUsernameProvider _usernameProvider;
		private readonly ILogger _logger;

		protected BaseDbContext(IConfiguration config, IUsernameProvider usernameProvider, ILogger logger)
			: base(config.ConnectionString)
		{
			_usernameProvider = usernameProvider;
			_logger = logger;
		}


		public override int SaveChanges()
		{
			AddTimestamps();
			return base.SaveChanges();
		}

		public override async Task<int> SaveChangesAsync()
		{
			AddTimestamps();
			return await base.SaveChangesAsync();
		}

		private void AddTimestamps()
		{
			var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

			foreach (var entity in entities)
			{
				if (entity.State == EntityState.Added)
				{
					((BaseEntity)entity.Entity).CreateDate = DateTime.UtcNow;
					((BaseEntity)entity.Entity).CreatedBy = _usernameProvider.Username;
				}

				((BaseEntity)entity.Entity).UpdatedDate = DateTime.UtcNow;
				((BaseEntity)entity.Entity).UpdatedBy = _usernameProvider.Username;
			}
		}

	}
}