using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Models.DbContexts;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Models.Helpers;
using HML.Immunisation.Providers.Interfaces;
using LinqKit;

namespace HML.Immunisation.Providers
{

	public class EmployeeDiseaseRiskStatusProvider : IEmployeeDiseaseRiskStatusProvider
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger _logger;
		private readonly IUsernameProvider _usernameProvider;

		public virtual EmployeeDiseaseRiskStatusDbContext GetDbContext()
		{
			var db = new EmployeeDiseaseRiskStatusDbContext(_configuration, _usernameProvider, _logger);
			return db;
		}

		public EmployeeDiseaseRiskStatusProvider(IConfiguration configuration, ILogger logger,
			IUsernameProvider usernameProvider)
		{
			_configuration = configuration;
			_logger = logger;
			_usernameProvider = usernameProvider;
		}

		public async Task<IList<EmployeeDiseaseRiskStatusRecord>> GetEmployeesDiseaseRiskStatusAsync(int employeeId)
		{
			return await Search(ByEmployeeNotDeleted(employeeId));
		}

		public async Task<IList<EmployeeDiseaseRiskStatusRecord>> GetClientsEmployeesDiseaseRiskStatusAsync(Guid clientId)
		{
			return await Search(ByClientNotDeleted(clientId));
		}

		public IList<EmployeeDiseaseRiskStatusRecord> GetEmployeesDiseaseRiskStatus(int employeeId)
		{
			return Task.Run(() => GetEmployeesDiseaseRiskStatusAsync(employeeId)).Result;
		}

		public async Task<IList<EmployeeDiseaseRiskStatusRecord>> SaveAsync(int employeeId,
			IList<EmployeeDiseaseRiskStatusRecord> statuses)
		{
			try
			{
				using (var db = GetDbContext())
				{
					var existingDiseaseRisks = await GetEmployeesDiseaseRiskStatusAsync(employeeId);

					// delete items that are not in the payload
					foreach (
						var itemToDelete in existingDiseaseRisks.Where(x => !x.IsDeleted && !statuses.Select(y => y?.Id).Contains(x.Id)))
					{
						var existingItem = await db.EmployeeDiseaseRiskStatuses.FindAsync(itemToDelete.Id);
						db.Entry(existingItem).Entity.IsDeleted = true;
					}

					// If an existing record has changed delete the current record and create a new record.
					// This way we have an audit of all the changes that have been made to a disease risk for a client
					foreach (var itemToSave in statuses)
					{
						itemToSave.EmployeeId = employeeId;

						if (itemToSave.IsTransient) // Add New Recotds
						{
							existingDiseaseRisks.Add(db.EmployeeDiseaseRiskStatuses.Add(itemToSave));
						}
						else
						{
							var existingItem = await db.EmployeeDiseaseRiskStatuses.FindAsync(itemToSave.Id);
							if (itemToSave.HasChanges(existingItem))
							{
								//delete existing item
								existingItem.IsDeleted = true;
								db.Entry(existingItem);
								// create new Item
								itemToSave.Id = 0;
								existingDiseaseRisks.Add(db.EmployeeDiseaseRiskStatuses.Add(itemToSave));
							}
						}
					}

					await db.SaveChangesAsync().ConfigureAwait(false);
					return await GetEmployeesDiseaseRiskStatusAsync(employeeId);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed whilst saving Employee Disease Risk Statuses for Employee Id: {employeeId}", ex);
				throw;
			}
		}

		public async Task<IList<EmployeeDiseaseRiskStatusRecord>> GetEmployeesDiseaseRiskStatusHistoryAsync(int employeeId,
			int diseaseRiskId)
		{
			return await Search(ByEmployeeDiseaseRiskWithDeleted(employeeId, diseaseRiskId));
		}

		public int NoOfEmployeesWithDiseaseRiskRequiredForRole(Guid clientId, int diseaseriskId)
		{
			try
			{
				using (var db = GetDbContext())
				{
					return db.EmployeeDiseaseRiskStatuses.Count(
						x => 
						!x.IsDeleted
						&& x.IsRequired 
						&& x.DiseaseRiskId == diseaseriskId 
						&& x.ClientId == clientId
						);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed search NoOfEmployeesWithDiseaseRiskRequiredForRole", ex);
				throw;
			}
		}

		private async Task<IList<EmployeeDiseaseRiskStatusRecord>> Search(Expression<Func<EmployeeDiseaseRiskStatusRecord, bool>> filterExpression)
		{
			try
			{
				using (var db = GetDbContext())
				{
					return await db.EmployeeDiseaseRiskStatuses
						.AsExpandable()
						.Where(filterExpression)
						.ToListAsync()
						.ConfigureAwait(false);
				}

			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed search EmployeeDiseaseRiskStatusRecord", ex);
				throw;
			}
		}

		private static Expression<Func<EmployeeDiseaseRiskStatusRecord, bool>> ByClientNotDeleted(Guid clientId)
		{
			return e => e.ClientId == clientId && !e.IsDeleted;
		}

		private static Expression<Func<EmployeeDiseaseRiskStatusRecord, bool>> ByEmployeeNotDeleted(int employeeId)
		{
			return e => e.EmployeeId == employeeId && !e.IsDeleted;
		}

		public Expression<Func<EmployeeDiseaseRiskStatusRecord, bool>> ByEmployeeDiseaseRiskWithDeleted(int employeeId, int diseaseRiskId)
		{
			return e => e.EmployeeId == employeeId && e.DiseaseRiskId == diseaseRiskId;
		}

	}
}