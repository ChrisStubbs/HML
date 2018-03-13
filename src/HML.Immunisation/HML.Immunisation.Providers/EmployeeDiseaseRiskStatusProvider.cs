using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Models.DbContexts;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Models.Helpers;
using HML.Immunisation.Providers.Interfaces;

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
			try
			{
				using (var db = GetDbContext())
				{
					return await db.EmployeeDiseaseRiskStatuses
						.Where(x => x.EmployeeId == employeeId)
						.ToListAsync()
						.ConfigureAwait(false);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get Disease Risks", ex);
				throw;
			}
		}

		public async Task<IList<EmployeeDiseaseRiskStatusRecord>> GetClientsEmployeesDiseaseRiskStatusAsync(Guid clientId)
		{
			try
			{
				using (var db = GetDbContext())
				{
					return await db.EmployeeDiseaseRiskStatuses
						.Where(x => x.ClientId == clientId)
						.ToListAsync()
						.ConfigureAwait(false);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get Disease Risks", ex);
				throw;
			}
		}

		public IList<EmployeeDiseaseRiskStatusRecord> GetEmployeesDiseaseRiskStatus(int employeeId)
		{
			return Task.Run(() => GetEmployeesDiseaseRiskStatusAsync(employeeId)).Result;
		}

		public async Task<IList<EmployeeDiseaseRiskStatusRecord>> SaveAsync(int employeeId, IList<EmployeeDiseaseRiskStatusRecord> statuses)
		{
			try
			{
				using (var db = GetDbContext())
				{
					var existingDiseaseRisks = await GetEmployeesDiseaseRiskStatusAsync(employeeId);

					// delete items that are not in the payload
					foreach (var itemToDelete in existingDiseaseRisks.Where(x => !x.IsDeleted && !statuses.Select(y => y?.Id).Contains(x.Id)))
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
	}
}