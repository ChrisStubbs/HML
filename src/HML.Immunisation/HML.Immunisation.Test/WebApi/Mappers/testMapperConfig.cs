using System.Collections.Generic;
using AutoMapper;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Models.ViewModels;
using HML.Immunisation.WebAPI;
using NUnit.Framework;
using Unity;

namespace HML.Immunisation.Test.WebApi.Mappers
{
	[TestFixture]
	public class TestMapperConfig
	{

		[Test]
		public void Test()
		{
			var mapper = UnityConfig.Container.Resolve<IMapper>();

			var rec = new EmployeeDiseaseRiskStatusRecord {Id = 54};
			var view = new EmployeeDiseaseRiskStatus();

			var result = mapper.Map<EmployeeDiseaseRiskStatus>(rec);

			Assert.That(result.Id, Is.EqualTo(54));

			var lrec = new List<EmployeeDiseaseRiskStatusRecord> {rec};
			var results = mapper.Map<IList<EmployeeDiseaseRiskStatus>>(lrec);
			Assert.That(results.Count, Is.EqualTo(1));
			Assert.That(results[0].Id, Is.EqualTo(54));

		}
	}
}
