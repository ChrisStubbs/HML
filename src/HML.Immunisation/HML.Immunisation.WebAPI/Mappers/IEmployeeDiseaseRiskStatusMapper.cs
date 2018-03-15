using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Models.ViewModels;

namespace HML.Immunisation.WebAPI.Mappers
{
	public interface IEmployeeDiseaseRiskStatusMapper
	{
		Task<IList<EmployeeDiseaseRiskStatus>> MapEmployeesDiseaseRiskStatusAsync(Guid clientId, 
			IList<EmployeeDiseaseRiskStatusRecord> employeeDiseaseRiskStatusRiskStatusRecords);

		Task<IList<EmployeeDiseaseRiskStatus>> MapEmployeesDiseaseRiskStatusAsync(
			IList<EmployeeDiseaseRiskStatusRecord> employeeDiseaseRiskStatusRiskStatusRecords);

		Task<IList<EmployeeDiseaseRiskStatus>> MapEmployeesDiseaseRiskStatusWithNoClientSettingsAsync(
			IList<EmployeeDiseaseRiskStatusRecord> employeeDiseaseRiskStatusRiskStatusRecords);
	}
}