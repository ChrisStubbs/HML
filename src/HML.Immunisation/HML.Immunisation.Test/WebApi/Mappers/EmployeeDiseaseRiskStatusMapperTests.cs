using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HML.Immunisation.Common;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Models.ViewModels;
using HML.Immunisation.Test.Factories;
using HML.Immunisation.WebAPI.Infrastructure;
using HML.Immunisation.WebAPI.Mappers;
using NUnit.Framework;

namespace HML.Immunisation.Test.WebApi.Mappers
{
	[TestFixture]
	public class EmployeeDiseaseRiskStatusMapperTests
	{
		private static IMapper Mapper => AutoMapperConfig.GetMapperConfiguration().CreateMapper();
		public class TheGetEmployeeDiseaseRiskStatusesMethod : EmployeeDiseaseRiskStatusMapperTests
		{
			[Test]
			public void ShouldAddMissingDiseaseRiskFromClientDiseaseRisk()
			{
				var empRiskStatuses = new List<EmployeeDiseaseRiskStatusRecord>
				{
					EmployeeDiseaseRiskStatusRecordFactory.New.WithIdAndDiseaseRisk(21,1).Build(),
					EmployeeDiseaseRiskStatusRecordFactory.New.WithIdAndDiseaseRisk(22,2).Build(),
				};

				var clientRisk = new List<ClientDiseaseRiskRecord>
				{
					ClientDiseaseRiskRecordFactory.New.With(x=> x.DiseaseRiskId = 1).Build(),
					ClientDiseaseRiskRecordFactory.New.With(x=> x.DiseaseRiskId = 2).Build(),
					ClientDiseaseRiskRecordFactory.New.With(x=> x.DiseaseRiskId = 3).Build()
				};

				var clientSettings = new ClientSettingsRecord { ClientDiseaseRisks = clientRisk };

				var results = EmployeeDiseaseRiskStatusMapper.GetEmployeeDiseaseRiskStatuses(
					clientSettings,
					new List<DiseaseRiskRecord>(),
					new Lookups(),
					Mapper,
					empRiskStatuses);

				Assert.That(results.Count, Is.EqualTo(3));
				Assert.That(results[0].Id, Is.EqualTo(empRiskStatuses[0].Id));
				Assert.That(results[0].DiseaseRiskId, Is.EqualTo(empRiskStatuses[0].DiseaseRiskId));

				Assert.That(results[1].Id, Is.EqualTo(empRiskStatuses[1].Id));
				Assert.That(results[1].DiseaseRiskId, Is.EqualTo(empRiskStatuses[1].DiseaseRiskId));


				var newEmpRisk = results.SingleOrDefault(x => x.DiseaseRiskId == 3);
				Assert.That(newEmpRisk, Is.Not.Null);
				Assert.That(newEmpRisk.Id, Is.EqualTo(0));
				Assert.That(newEmpRisk.DiseaseRiskId, Is.EqualTo(3));
			}

			[Test]
			public void ShouldRemoveEmployeeDiseaseRisksThatAreNotRequiredAndNotOnClientDiseaseRisk()
			{
				var empRiskStatuses = new List<EmployeeDiseaseRiskStatusRecord>
				{
					EmployeeDiseaseRiskStatusRecordFactory.New.WithIdAndDiseaseRisk(21,1).Build(),
					EmployeeDiseaseRiskStatusRecordFactory.New.WithIdAndDiseaseRisk(22,2).With(x=> x.IsRequired = false).Build(),
				};

				var clientRisk = new List<ClientDiseaseRiskRecord>
				{
					ClientDiseaseRiskRecordFactory.New.With(x=> x.DiseaseRiskId = 1).Build(),
				};

				var clientSettings = new ClientSettingsRecord { ClientDiseaseRisks = clientRisk };

				var results = EmployeeDiseaseRiskStatusMapper.GetEmployeeDiseaseRiskStatuses(
					clientSettings,
					new List<DiseaseRiskRecord>(),
					new Lookups(),
					Mapper,
					empRiskStatuses
					);

				Assert.That(results.Count, Is.EqualTo(1));
				Assert.That(results[0].Id, Is.EqualTo(empRiskStatuses[0].Id));
				Assert.That(results[0].DiseaseRiskId, Is.EqualTo(empRiskStatuses[0].DiseaseRiskId));

			}

			[Test]
			public void ShouldKeepEmployeeDiseaseRisksThatAreRequiredAndNotOnClientDiseaseRisk()
			{
				var empRiskStatuses = new List<EmployeeDiseaseRiskStatusRecord>
					{
						EmployeeDiseaseRiskStatusRecordFactory.New.WithIdAndDiseaseRisk(21,1).Build(),
						EmployeeDiseaseRiskStatusRecordFactory.New.WithIdAndDiseaseRisk(22,2).With(x=> x.IsRequired = true).Build(),
					};

				var clientRisk = new List<ClientDiseaseRiskRecord>
					{
						ClientDiseaseRiskRecordFactory.New.With(x=> x.DiseaseRiskId = 1).Build(),
					};
				var clientSettings = new ClientSettingsRecord { ClientDiseaseRisks = clientRisk };

				var results = EmployeeDiseaseRiskStatusMapper.GetEmployeeDiseaseRiskStatuses(
					clientSettings,
					new List<DiseaseRiskRecord>(),
					new Lookups(),
					Mapper,
					empRiskStatuses
					);


				Assert.That(results.Count, Is.EqualTo(2));
				Assert.That(results[0].Id, Is.EqualTo(empRiskStatuses[0].Id));
				Assert.That(results[1].Id, Is.EqualTo(empRiskStatuses[1].Id));
			}

			[Test]
			public void ShouldKeepEmployeeDiseaseRisksThatAreNotRequiredAndInClientDiseaseRisk()
			{
				var empRiskStatuses = new List<EmployeeDiseaseRiskStatusRecord>
				{
					EmployeeDiseaseRiskStatusRecordFactory.New.WithIdAndDiseaseRisk(21,1).Build(),
					EmployeeDiseaseRiskStatusRecordFactory.New.WithIdAndDiseaseRisk(22,2).With(x=> x.IsRequired = false).Build(),
				};

				var clientRisk = new List<ClientDiseaseRiskRecord>
				{
					ClientDiseaseRiskRecordFactory.New.With(x=> x.DiseaseRiskId = 1).Build(),
					ClientDiseaseRiskRecordFactory.New.With(x=> x.DiseaseRiskId = 2).Build(),
				};
				var clientSettings = new ClientSettingsRecord { ClientDiseaseRisks = clientRisk };


				var results = EmployeeDiseaseRiskStatusMapper.GetEmployeeDiseaseRiskStatuses(
					clientSettings,
					new List<DiseaseRiskRecord>(),
					new Lookups(),
					Mapper,
					empRiskStatuses
					);


				Assert.That(results.Count, Is.EqualTo(2));
				Assert.That(results[0].Id, Is.EqualTo(empRiskStatuses[0].Id));
				Assert.That(results[1].Id, Is.EqualTo(empRiskStatuses[1].Id));
			}

			[Test]
			public void ShouldMapDiseaseRisk()
			{
				var empRiskStatuses = new List<EmployeeDiseaseRiskStatusRecord>
				{
					EmployeeDiseaseRiskStatusRecordFactory.New.WithIdAndDiseaseRisk(21,1).With(x=> x.IsRequired = true).Build(),
					EmployeeDiseaseRiskStatusRecordFactory.New.WithIdAndDiseaseRisk(22,2).With(x=> x.IsRequired = true).Build(),
				};

				var diseaseRisks = new List<DiseaseRiskRecord>
				{
					DiseaseRiskRecordFactory.New.WithIdDescriptionAndCode(1, "The Description for disease risk1","Code1").Build(),
					DiseaseRiskRecordFactory.New.WithIdDescriptionAndCode(2, "The Description for disease risk2","Code2").Build()
				};


				var results = EmployeeDiseaseRiskStatusMapper.GetEmployeeDiseaseRiskStatuses(
					new ClientSettingsRecord(), 
					diseaseRisks,
					new Lookups(),
					Mapper,
					empRiskStatuses
					);


				Assert.That(results.Count, Is.EqualTo(2));
				Assert.That(results[0].DiseaseRiskDescription, Is.EqualTo("The Description for disease risk1"));
				Assert.That(results[1].DiseaseRiskDescription, Is.EqualTo("The Description for disease risk2"));

				Assert.That(results[0].DiseaseRiskCode, Is.EqualTo("Code1"));
				Assert.That(results[1].DiseaseRiskCode, Is.EqualTo("Code2"));
			}

			[Test]
			public void ShouldMapLookups()
			{
				var lookups = LookupsFactory.New.Build();
				lookups.ImmunisationProgress.Add(new KeyValuePair<short, string>(1, "progress1"));
				lookups.ImmunisationStatuses.Add(new KeyValuePair<short, string>(1, "status1"));
				lookups.RecallActions.Add(new RecallActionLookup(1, "recallaction1"));

				var empRiskStatuses = new List<EmployeeDiseaseRiskStatusRecord>
				{
					EmployeeDiseaseRiskStatusRecordFactory.New
					.With(x=> x.ImmunisationStatusId = 1)
					.With(x=> x.CurrentProgress = 1)
					.With(x=> x.RecallActionId = 1)
					.With(x=> x.IsRequired = true)
					.Build()
				};

				var results = EmployeeDiseaseRiskStatusMapper.GetEmployeeDiseaseRiskStatuses(
										new ClientSettingsRecord(), 
										new List<DiseaseRiskRecord>(),
										lookups,
										Mapper,
										empRiskStatuses
										);

			
				Assert.That(results.Count, Is.EqualTo(1));
				Assert.That(results[0].ImmunisationStatusDisplayValue, Is.EqualTo(lookups.ImmunisationStatuses.First().DisplayValue()));
				Assert.That(results[0].CurrentProgressDisplayValue, Is.EqualTo(lookups.ImmunisationProgress.First().DisplayValue()));
				Assert.That(results[0].RecallActionDisplayValue, Is.EqualTo(lookups.RecallActions.First().DisplayValue));
			}

		}
	}
}