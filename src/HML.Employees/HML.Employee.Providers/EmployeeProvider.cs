using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HML.Employee.Common.Interfaces;
using HML.Employee.Models;
using HML.Employee.Models.Entities;
using HML.Employee.Providers.Interfaces;
using LinqKit;

namespace HML.Employee.Providers
{
	public class EmployeeProvider : IEmployeeProvider
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger _logger;
		private readonly IUsernameProvider _usernameProvider;

		public virtual EmployeeContext GetDbContext()
		{
			var db = new EmployeeContext(_configuration, _usernameProvider, _logger);
			return db;
		}

		public EmployeeProvider(IConfiguration configuration, ILogger logger, IUsernameProvider usernameProvider)
		{
			_configuration = configuration;
			_logger = logger;
			_usernameProvider = usernameProvider;
		}

		public async Task<IList<EmployeeRecord>> Search(Expression<Func<EmployeeRecord, bool>> filterExpression)
		{
			try
			{
				using (var db = GetDbContext())
				{
					return await db.Employees
						.Include(x => x.Addresses)
						.Include(x => x.Contacts)
						.Include(x => x.ClientSpecificFields)
						.AsExpandable()
						.Where(filterExpression)
						.ToListAsync()
						.ConfigureAwait(false);
				}

			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed search employee", ex);
				throw;
			}
		}

		public async Task<IList<EmployeeRecord>> SearchLinkedCases(Expression<Func<EmployeeRecord, bool>> filterExpression)
		{
			try
			{
				using (var db = GetDbContext())
				{
					return await db.Employees
						.Include(x => x.Addresses)
						.Include(x => x.Contacts)
						.Include(x => x.ClientSpecificFields)
						.AsExpandable()
						.Where(filterExpression)
						.ToListAsync()
						.ConfigureAwait(false);
				}

			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed search linked cases", ex);
				throw;
			}
		}

		public async Task<IList<EmployeeRecord>> GetByClientId(Guid clientId)
		{
			return await Search(ByClient(clientId));
		}

		public async Task<IList<EmployeeRecord>> GetByCaseEmployeeId(Guid caseEmployeeId)
		{
			return await Search(ByCaseEmployeeId(caseEmployeeId));
		}

		public async Task<IList<EmployeeRecord>> GetByCaseEmployeeByNameOrClientIdOrDob(Guid clientId, string firstName, string lastName, DateTime dob)
		{
			return await SearchLinkedCases(ByEmployeeByNameOrClientIdOrDob(clientId, firstName, lastName, dob));
		}

		public async Task<EmployeeRecord> GetByIdAsync(int id)
		{
			try
			{
				using (var db = GetDbContext())
				{
					return await db.Employees
						.Include(x => x.Addresses)
						.Include(x => x.Contacts)
						.Include(x => x.ClientSpecificFields)
						.SingleOrDefaultAsync(x => x.Id == id)
						.ConfigureAwait(false);
				}

			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed whilst getting employee {id}", ex);
				throw;
			}
		}

		public async Task CreateAsync(EmployeeRecord employee)
		{
			try
			{
				using (var db = GetDbContext())
				{
					db.Employees.Add(employee);
					await db.SaveChangesAsync().ConfigureAwait(false);
				}

			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed whilst creating employee", ex);
				throw;
			}
		}

		public async Task<EmployeeRecord> UpdateAsync(EmployeeRecord employee)
		{
			try
			{
				using (var db = GetDbContext())
				{
					var existing = await db.Employees.FindAsync(employee.Id).ConfigureAwait(false);
					if (existing == null) return null;
					db.Configuration.AutoDetectChangesEnabled = true;
					db.Entry(existing).CurrentValues.SetValues(employee);

					DeleteChildren(employee.Addresses, existing.Addresses);
					DeleteChildren(employee.Contacts, existing.Contacts);
					DeleteChildren(employee.ClientSpecificFields, existing.ClientSpecificFields);

					InsertUpdateChildren(db, existing, employee.Addresses, existing.Addresses);
					InsertUpdateChildren(db, existing, employee.Contacts, existing.Contacts);
					InsertUpdateChildren(db, existing, employee.ClientSpecificFields, existing.ClientSpecificFields);

					await db.SaveChangesAsync().ConfigureAwait(false);
					return existing;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed whilst creating employee {employee.Id}", ex);
				throw;
			}
		}

		private void InsertUpdateChildren<T>(EmployeeContext db, EmployeeRecord employee, IEnumerable<T> newList, ICollection<T> existingList) where T : class, IEmployeeChild
		{
			foreach (var newEntity in newList)
			{
				newEntity.EmployeeId = employee.Id;

				var existingEntity = existingList.SingleOrDefault(c => !newEntity.IsTransient && c.Id == newEntity.Id);

				if (existingEntity != null)
				{
					db.Entry(existingEntity).CurrentValues.SetValues(newEntity);
				}
				else
				{
					existingList.Add(newEntity);
				}
			}
		}

		private static void DeleteChildren<T>(ICollection<T> newList, ICollection<T> existingList) where T : BaseEntity
		{
			if (existingList == null) throw new ArgumentNullException(nameof(existingList));
			foreach (var existingEntity in existingList)
			{
				if (newList.All(x => x.Id != existingEntity.Id && !existingEntity.IsDeleted))
				{
					existingEntity.IsDeleted = true;
				}
			}
		}

		public Expression<Func<EmployeeRecord, bool>> ByClient(Guid clientId)
		{
			return e => e.ClientId == clientId;
		}

		public Expression<Func<EmployeeRecord, bool>> ByCaseEmployeeId(Guid caseEmployeeId)
		{
			return e => e.CaseEmployeeId == caseEmployeeId;
		}

		public Expression<Func<EmployeeRecord, bool>> ByEmployeeByNameOrClientIdOrDob(Guid clientId, string firstName, string lastName, DateTime dob)
		{
			return (e => e.ClientId == clientId
		   && e.LastName.Trim().ToLower() == lastName.Trim().ToLower()
		   && ((e.DateOfBirth >= dob && e.DateOfBirth <= dob) || e.DateOfBirth == null));
		}

	}
}
