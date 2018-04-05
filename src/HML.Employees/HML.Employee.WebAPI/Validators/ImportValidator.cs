using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using HML.Employee.Common;
using HML.Employee.Models.Entities;
using HML.Employee.Models.ViewModels;
using HML.Employee.WebAPI.Infrastructure;

namespace HML.Employee.WebAPI.Validators
{
	public class ImportValidator : AbstractValidator<ImportRecord>
	{
		private readonly ICachedEmployeeImportValidatorProvider _employeeImportValidatorProvider;
		private string _nonUniqueEmployeesMessage;
		public ImportValidator(ICachedEmployeeImportValidatorProvider employeeImportValidatorProvider)
		{
			_employeeImportValidatorProvider = employeeImportValidatorProvider;
			SetRules();
		}

		private void SetRules()
		{
			RuleFor(import => import.Source).NotNull().Length(1, 50).WithMessage("Source: is required with a max length of 500");

			RuleFor(import => import)
				.Must(record => !HasMoreThanOneClient(record.Employees))
				.WithMessage("Employee Import has more than one client. Can not import");

			RuleFor(import => import)
				.Must(record => !HasDuplicateLastNameDateOfBirth(record.Employees))
				.WithMessage(record => GetDuplicateMessage(record.Employees));

			RuleFor(import => import)
				.Must((record) => IsEmployeeNameAndDobUniqueForClient(record.Employees.OfType<IEmployeeImportMatchCriteria>().ToList()))
				.WithMessage(record => _nonUniqueEmployeesMessage);

		}


		private bool HasMoreThanOneClient(IEnumerable<EmployeeRecord> employees)
		{
			return employees.Select(x => x.ClientId).Distinct().Count() > 1;
		}

		public static string GetDuplicateMessage(IEnumerable<IEmployeeImportMatchCriteria> records)
		{
			var duplicates = GetDuplicateGrouping(records).Where(x => x.Count() > 1).SelectMany(x => x.ToList());
			return $"Employee Import has duplicates: {string.Join(",", duplicates.Select(x => $"'{x.LastName} {x.DateOfBirth.ToShortDateFormat()}'").Distinct())}";
		}

		private static IEnumerable<IGrouping<object, IEmployeeImportMatchCriteria>> GetDuplicateGrouping(IEnumerable<IEmployeeImportMatchCriteria> records)
		{
			return records.GroupBy(x => x, new EmployeeImportMatchCriteriaComparer());
		}

		public static bool HasDuplicateLastNameDateOfBirth(IEnumerable<IEmployeeImportMatchCriteria> records)
		{
			return GetDuplicateGrouping(records).Any(g => g.Count() > 1);
		}

		public bool IsEmployeeNameAndDobUniqueForClient(IList<IEmployeeImportMatchCriteria> records)
		{
			if (records != null && records.Any())
			{
				var clientId = records.First().ClientId;

				var existingClientEmployees = _employeeImportValidatorProvider.GetEmployeesImportMatchCriteriaByClient(clientId);

				var commonEmployees = existingClientEmployees.Intersect(records, new EmployeeImportMatchCriteriaComparer()).ToList();

				if (commonEmployees.Any())
				{
					_nonUniqueEmployeesMessage = $"The following employees already exist: {string.Join(",", commonEmployees.Select(x => $"'{x.LastName} {x.DateOfBirth.ToShortDateFormat()}'"))}";
					return false;
				}

				return true;
			}
			return true;
		}

	}
}