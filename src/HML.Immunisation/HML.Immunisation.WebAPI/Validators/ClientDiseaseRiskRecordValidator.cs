using System.Collections.Generic;
using System.Linq;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Providers.Interfaces;
using FluentValidation;
using HML.Immunisation.WebAPI.Infrastructure;

namespace HML.Immunisation.WebAPI.Validators
{
	public class ClientDiseaseRiskRecordValidator : AbstractValidator<ClientDiseaseRiskRecord>
	{
		private readonly IEmployeeDiseaseRiskStatusProvider _employeeDiseaseRiskStatusProvider;
		private readonly IDiseaseRiskProvider _diseaseRiskProvider;


		public ClientDiseaseRiskRecordValidator(
			IEmployeeDiseaseRiskStatusProvider employeeDiseaseRiskStatusProvider,
			IDiseaseRiskProvider diseaseRiskProvider)
		{
			_employeeDiseaseRiskStatusProvider = employeeDiseaseRiskStatusProvider;
			_diseaseRiskProvider = diseaseRiskProvider;
			SetRules();
		}

		private void SetRules()
		{
			RuleFor(e => e.IsDeleted)
			.Must((record, isRequired) => !IsDeletingADiseaseRiskRequiredForEmployeeRole(record))
			.WithMessage(record => $"{DiseaseRiskName(record.DiseaseRiskId)}: Can not disable this client disease risk. Employees have this set as required for role.");
		}

		private bool IsDeletingADiseaseRiskRequiredForEmployeeRole(ClientDiseaseRiskRecord record)
		{
			if (record.IsDeleted)
			{
				return _employeeDiseaseRiskStatusProvider.NoOfEmployeesWithDiseaseRiskRequiredForRole(record.ClientId, record.DiseaseRiskId) > 0;
			}
			return false;
		}

		private string DiseaseRiskName(int diseaseRiskId)
		{
			return _diseaseRiskProvider.GetAll()?.SingleOrDefault(x => x.Id == diseaseRiskId)?.Description;
		}
	}
}