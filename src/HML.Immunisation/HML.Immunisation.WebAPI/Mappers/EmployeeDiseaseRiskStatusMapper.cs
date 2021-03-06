﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HML.Immunisation.Common;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Models.ViewModels;
using HML.Immunisation.Providers.Interfaces;

namespace HML.Immunisation.WebAPI.Mappers
{
	public class EmployeeDiseaseRiskStatusMapper : IEmployeeDiseaseRiskStatusMapper
	{
		private readonly IClientSettingsProvider _clientSettingsProvider;
		private readonly IDiseaseRiskProvider _diseaseRiskProvider;
		private readonly ILookupsProvider _lookupsProvider;
		private readonly IMapper _mapper;

		public EmployeeDiseaseRiskStatusMapper(
			IClientSettingsProvider clientSettingsProvider,
			IDiseaseRiskProvider diseaseRiskProvider,
			ILookupsProvider lookupsProvider,
			IMapper mapper
		)
		{
			_clientSettingsProvider = clientSettingsProvider;
			_diseaseRiskProvider = diseaseRiskProvider;
			_lookupsProvider = lookupsProvider;
			_mapper = mapper;
		}

		public async Task<IList<EmployeeDiseaseRiskStatus>> MapEmployeesDiseaseRiskStatusAsync(Guid clientId,
			IList<EmployeeDiseaseRiskStatusRecord> employeeDiseaseRiskStatusRiskStatusRecords)
		{

			var clientDiseaseRiskTask = _clientSettingsProvider.GetByClientIdAsync(clientId);
			var diseaseRisksTask = _diseaseRiskProvider.GetAllAsync();
			var lookupTask = _lookupsProvider.GetAsync();
			await Task.WhenAll(clientDiseaseRiskTask, diseaseRisksTask, lookupTask).ConfigureAwait(false);

			return GetEmployeeDiseaseRiskStatuses(
				clientDiseaseRiskTask.Result,
				diseaseRisksTask.Result,
				lookupTask.Result,
				_mapper,
				employeeDiseaseRiskStatusRiskStatusRecords);
		}

		public async Task<IList<EmployeeDiseaseRiskStatus>> MapEmployeesDiseaseRiskStatusWithNoClientSettingsAsync(
			IList<EmployeeDiseaseRiskStatusRecord> employeeDiseaseRiskStatusRiskStatusRecords)
		{
			var diseaseRisksTask = _diseaseRiskProvider.GetAllAsync();
			var lookupTask = _lookupsProvider.GetAsync();
			await Task.WhenAll(diseaseRisksTask, lookupTask).ConfigureAwait(false);

			return GetEmployeeDiseaseRiskStatusesWithNoClientSettings(
				diseaseRisksTask.Result,
				lookupTask.Result,
				_mapper,
				employeeDiseaseRiskStatusRiskStatusRecords);
		}

		public async Task<IList<EmployeeDiseaseRiskStatus>> MapEmployeesDiseaseRiskStatusAsync(
			IList<EmployeeDiseaseRiskStatusRecord> employeeDiseaseRiskStatusRiskStatusRecords)
		{
			var empDiseaseRisk = employeeDiseaseRiskStatusRiskStatusRecords.FirstOrDefault();
			if (empDiseaseRisk != null)
			{
				return await MapEmployeesDiseaseRiskStatusAsync(empDiseaseRisk.ClientId, employeeDiseaseRiskStatusRiskStatusRecords);
			}
			return new List<EmployeeDiseaseRiskStatus>();
		}

		public static IList<EmployeeDiseaseRiskStatus> GetEmployeeDiseaseRiskStatusesWithNoClientSettings(
			IList<DiseaseRiskRecord> diseaseRisk,
			Lookups lookups,
			IMapper mapper,
			IList<EmployeeDiseaseRiskStatusRecord> employeeDiseaseRiskStatusRiskStatusRecords)
		{
			var mergedStatuses = mapper.Map<IList<EmployeeDiseaseRiskStatus>>(employeeDiseaseRiskStatusRiskStatusRecords).ToList();
			AddLookupDescriptions(diseaseRisk, lookups, mergedStatuses);

			return mergedStatuses.OrderBy(x => x.CreateDate).ToList();
		}

		public static IList<EmployeeDiseaseRiskStatus> GetEmployeeDiseaseRiskStatuses(
			 ClientSettingsRecord clientSettingsRecord,
			 IList<DiseaseRiskRecord> diseaseRisk,
			 Lookups lookups,
			 IMapper mapper,
			 IList<EmployeeDiseaseRiskStatusRecord> employeeDiseaseRiskStatusRiskStatusRecords)
		{
			if (clientSettingsRecord != null)
			{

				var currentClientDiseaseRiskIds =
					clientSettingsRecord.ClientDiseaseRisks.Where(x => !x.IsDeleted).Select(x => x.DiseaseRiskId);

				var mergedStatuses = mapper.Map<IList<EmployeeDiseaseRiskStatus>>(
					employeeDiseaseRiskStatusRiskStatusRecords.Where(x =>
								x.IsRequired || currentClientDiseaseRiskIds.Contains(x.DiseaseRiskId)
					)).ToList();


				foreach (var clientDiseaseRiskId in currentClientDiseaseRiskIds)
				{
					if (mergedStatuses.All(x => x.DiseaseRiskId != clientDiseaseRiskId))
					{
						mergedStatuses.Add(new EmployeeDiseaseRiskStatus
						{
							DiseaseRiskId = clientDiseaseRiskId,
							ClientId = clientSettingsRecord.Id
						});
					}
				}

				AddLookupDescriptions(diseaseRisk, lookups, mergedStatuses);

				return mergedStatuses.OrderBy(x => x.EmployeeId).ThenBy(x => x.DiseaseRiskCode).ToList();
			}

			return new List<EmployeeDiseaseRiskStatus>();
		}

		private static void AddLookupDescriptions(IList<DiseaseRiskRecord> diseaseRisk, Lookups lookups, List<EmployeeDiseaseRiskStatus> mergedStatuses)
		{
			mergedStatuses.ForEach(x =>
			{
				var risk = diseaseRisk.FirstOrDefault(dr => dr.Id == x.DiseaseRiskId);
				x.DiseaseRiskDescription = risk?.Description;
				x.DiseaseRiskCode = risk?.Code;

				x.RecallActionDisplayValue = lookups.RecallActions.FirstOrDefault(a => a.Key == x.RecallActionId)?.DisplayValue ??
											 string.Empty;
				x.CurrentProgressDisplayValue =
					lookups.ImmunisationProgress.FirstOrDefault(a => a.Key == x.CurrentProgress).DisplayValue();
				x.ImmunisationStatusDisplayValue =
					lookups.ImmunisationStatuses.FirstOrDefault(a => a.Key == x.ImmunisationStatusId).DisplayValue();
			});
		}
	}
}