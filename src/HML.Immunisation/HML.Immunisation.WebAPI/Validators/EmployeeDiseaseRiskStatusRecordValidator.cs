using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Providers.Interfaces;
using HML.Immunisation.WebAPI.Infrastructure;

namespace HML.Immunisation.WebAPI.Validators
{
	public class EmployeeDiseaseRiskStatusRecordValidator : AbstractValidator<EmployeeDiseaseRiskStatusRecord>
	{
		private readonly IEmployeeDiseaseRiskStatusProvider _employeeDiseaseRiskStatusProvider;
		private readonly IDiseaseRiskProvider _diseaseRiskProvider;
		//private IList<DiseaseRiskRecord> _diseaseRisks;
		private IList<EmployeeDiseaseRiskStatusRecord> _employeeDiseaseRiskStatuses;
		///private IList<DiseaseRiskRecord> DiseaseRisks => _diseaseRisks ?? (_diseaseRisks = _diseaseRiskProvider.GetAll());

		public EmployeeDiseaseRiskStatusRecordValidator(
			IEmployeeDiseaseRiskStatusProvider employeeDiseaseRiskStatusProvider,
			IDiseaseRiskProvider diseaseRiskProvider)
		{
			_employeeDiseaseRiskStatusProvider = employeeDiseaseRiskStatusProvider;
			_diseaseRiskProvider = diseaseRiskProvider;
			SetRules();
		}

		private void SetRules()
		{
			RuleFor(e => e.IsRequired)
				.Must((record, isRequired) => !record.IsImmsStatusIdSetWhenNotRequired())
				.WithMessage(record => $"{DiseaseRiskName(record.DiseaseRiskId)}: Immunisation Status should not be set when the disease risk is not required for the employee.");

			RuleFor(e => e.DateProtected)
				.Must((record, dateProtected) => !record.IsDateProtectedSetWhenStatusNotProtected())
				.WithMessage(record => $"{DiseaseRiskName(record.DiseaseRiskId)}: Date Protected should not be set unless Immunisation status is 'Protected'.");

			RuleFor(e => e.DateProtected)
				.Must((record, dateProtected) => record.IsDateProtectedNullOrLessThanOrEqualToToday())
				.WithMessage(record => $"{DiseaseRiskName(record.DiseaseRiskId)}: Date Protected can not be set in the future.");

			RuleFor(e => e.RecallDate)
				.Must((record, recallDate) => !HasRecallDateChangedToDateInThePast(record))
				.WithMessage(record => $"{DiseaseRiskName(record.DiseaseRiskId)}: Recall Date can not be changed to a value in the past.");
		}

		private EmployeeDiseaseRiskStatusRecord GetEmployeeDiseaseRiskStatusRecord(int employeeId, int id)
		{
			if (_employeeDiseaseRiskStatuses == null)
			{
				_employeeDiseaseRiskStatuses = _employeeDiseaseRiskStatusProvider.GetEmployeesDiseaseRiskStatus(employeeId);
			}
			return _employeeDiseaseRiskStatuses?.SingleOrDefault(x => x.Id == id);
		}

		private string DiseaseRiskName(int diseaseRiskId)
		{
			return _diseaseRiskProvider.GetAll()?.SingleOrDefault(x => x.Id == diseaseRiskId)?.Description;
		}

		private bool HasRecallDateChangedToDateInThePast(EmployeeDiseaseRiskStatusRecord record)
		{
			if (!record.RecallDate.HasValue) return false;

			if (record.IsTransient)
			{
				return record.HasRecallDateChangedToValueInThePast(null);
			}
			return record.HasRecallDateChangedToValueInThePast(GetEmployeeDiseaseRiskStatusRecord(record.EmployeeId, record.Id));
		}
	}
}
