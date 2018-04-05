using System;
using System.Collections.Generic;
using System.Linq;
using HML.Employee.Common.Interfaces;
using HML.Employee.Models;
using HML.Employee.Models.Entities;
using HML.Employee.Models.ViewModels;

namespace HML.Employee.Providers
{
	public class EmployeeImportProvider : IEmployeeImportProvider
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger _logger;
		private readonly IUsernameProvider _usernameProvider;

		public virtual EmployeeContext GetDbContext()
		{
			var db = new EmployeeContext(_configuration, _usernameProvider, _logger);
			return db;
		}

		public EmployeeImportProvider(IConfiguration configuration, ILogger logger, IUsernameProvider usernameProvider)
		{
			_configuration = configuration;
			_logger = logger;
			_usernameProvider = usernameProvider;
		}

		public EmployeeImportResults Import(ImportRecord importRecord)
		{

			try
			{
				using (var db = GetDbContext())
				{
					db.Configuration.AutoDetectChangesEnabled = false;
					db.Configuration.ValidateOnSaveEnabled = false;

					db.Imports.Add(importRecord);
					 db.SaveChanges();
					return new EmployeeImportResults
					{
						ImportId = importRecord.Id,
						EmployeeImportCount = importRecord.Employees.Count,
						ImportedBy = importRecord.CreatedBy,
						ImportDate = importRecord.CreateDate,
						Source = importRecord.Source
					};
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed employee Import", ex);
				throw;
			}
			
		}

	}
}