using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Providers.Interfaces;
using HML.Immunisation.WebAPI.Controllers;
using Moq;
using NUnit.Framework;

namespace HML.Immunisation.Test.WebApi
{
	[TestFixture]
	public class DiseaseRiskControllerTests : ControllerTestBases<DiseaseRiskController>
	{
		private Mock<ILogger> _logger;
		private Mock<IDiseaseRiskProvider> _diseaseRiskProvider;

		[SetUp]
		public virtual void Setup()
		{
			_logger = new Mock<ILogger>();
			_diseaseRiskProvider = new Mock<IDiseaseRiskProvider>();
			Controller = new DiseaseRiskController(_logger.Object, _diseaseRiskProvider.Object);
			SetupController();
		}

		public class TheGetMethod : DiseaseRiskControllerTests
		{
			[Test]
			public async Task ShouldCallDiseaseRiskProviderAndReturnNotFoundIfNoData()
			{
				IList<DiseaseRiskRecord> risks = new List<DiseaseRiskRecord>();
				_diseaseRiskProvider.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(risks));
				var results = await Controller.GetAll();
				_diseaseRiskProvider.Verify(x => x.GetAllAsync(), Times.Once);

				var notFoundResult = results as NotFoundResult;
				Assert.That(notFoundResult, Is.Not.Null);
			}

			[Test] public async Task ShouldCallDiseaseRiskProviderAndReturnOkIfData()
			{
				IList<DiseaseRiskRecord> risks = new List<DiseaseRiskRecord> {new DiseaseRiskRecord(), new DiseaseRiskRecord()};
				_diseaseRiskProvider.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(risks));
				var results = await Controller.GetAll();
				_diseaseRiskProvider.Verify(x => x.GetAllAsync(), Times.Once);

				var okResult = results as OkNegotiatedContentResult<IList<DiseaseRiskRecord>>;
				Assert.That(okResult, Is.Not.Null);
				Assert.That(okResult.Content.Count(), Is.EqualTo(2));
			}

			[Test]
			public void HasCorrectRouteAttribute()
			{
				MethodInfo controllerMethod = GetMethod(c => c.GetAll());

				var routeAttribute = GetAttributes<RouteAttribute>(controllerMethod).FirstOrDefault();
				Assert.IsNotNull(routeAttribute);
				Assert.AreEqual("disease-risks", routeAttribute.Template);
			}
		}
	}
}
