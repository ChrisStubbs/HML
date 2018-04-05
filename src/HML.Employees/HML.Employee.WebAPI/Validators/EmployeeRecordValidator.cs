using FluentValidation;
using HML.Employee.Common;
using HML.Employee.Models.Entities;
using HML.Employee.Providers.Interfaces;

namespace HML.Employee.WebAPI.Validators
{
	public class EmployeeRecordValidator : AbstractValidator<EmployeeRecord>
	{
		private readonly IEmployeeProvider _employeeProvider;

		public EmployeeRecordValidator(IEmployeeProvider employeeProvider)
		{
			_employeeProvider = employeeProvider;
			SetRules();
		}

		private void SetRules()
		{
			RuleFor(e => e.Title).NotNull().Length(1, 50).WithMessage("Title: is required with a max length of 50");
			RuleFor(e => e.EmployeeId).Length(0, 50).WithMessage("Employee Id: has a max length of 50");
			RuleFor(e => e.EmployeeId)
				.Must((record, employeeId) => IsEmployeeIdIsUniqueForClient(record))
				.WithMessage(record => $"Employee Id: '{record.EmployeeId}' is used by another employee");

			RuleFor(e => e.FirstName).NotNull().Length(1, 50).WithMessage("First Name: is required with a max length of 50");
			RuleFor(e => e.LastName).NotNull().Length(1, 50).WithMessage("Last Name: is required with a max length of 50");
			RuleFor(e => e.DateOfBirth).NotNull().WithMessage("Date Of Birth: is required");
			RuleFor(e => e.DateOfBirth)
				.Must((record, dateOfBirt) => !record.IsDateOfBirthInTheFuture())
				.WithMessage(record => "Date Of Birth: can not be set in the future.");
			RuleFor(e => e.JobTitle).Length(0, 100).WithMessage("Job Title: has a max length of 100");
			RuleFor(e => e.EndDate)
				.Must((record, endDate) => record.IsEndDateGreaterThanStartDate())
				.WithMessage((record) => $"Employees End Date: {record.EndDate.ToShortDateFormat()} is before their Start Date: {record.StartDate.ToShortDateFormat()}");
			RuleFor(e => e.EndDate)
				.Must((record, endDate) => !record.IsEndDateSetWhenUserIsActive())
				.WithMessage((record) => $"Employees End Date: {record.EndDate.ToShortDateFormat()} can not be set when user is active");
			RuleFor(e => e.NationalInsuranceNumber)
				.Must((record, nino) => record.IsValidNationalIsuranceNumber())
				.WithMessage((record) => $"National Insurance Number: {record.NationalInsuranceNumber} is not a valid national insurance number");
			RuleFor(e => e.NoOfHoursWorked)
				.Must((record, hrsWorked) => record.IsNoOfHoursWorkedValid())
				.WithMessage((record) => $"Employess No Of hours worked: {record.NoOfHoursWorked} must be greater than 0 hours and less than 100 hours.");
		}


		private bool IsEmployeeIdIsUniqueForClient(EmployeeRecord employee)
		{
			return _employeeProvider.IsEmployeeIdIsUniqueForClient(employee);
		}

	}
}