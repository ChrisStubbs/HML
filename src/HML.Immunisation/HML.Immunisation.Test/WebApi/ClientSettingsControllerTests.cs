using System;
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
	public class ClientSettingsControllerTests : ControllerTestBases<ClientSettingsController>
	{
		private Mock<ILogger> _logger;
		private Mock<IClientSettingsProvider> _clientSettingsProvider;

		[SetUp]
		public virtual void Setup()
		{
			_logger = new Mock<ILogger>();
			_clientSettingsProvider = new Mock<IClientSettingsProvider>();
			Controller = new ClientSettingsController(_logger.Object, _clientSettingsProvider.Object);
			SetupController();
		}

		public class TheGetMethod : ClientSettingsControllerTests
		{
			[Test]
			public async Task ShouldCallProviderAndReturnNotFoundIfNoData()
			{
				var clientId = Guid.NewGuid();
				_clientSettingsProvider.Setup(x => x.GetByClientIdAsync(clientId)).Returns(Task.FromResult((ClientSettingsRecord)null));

				var results = await Controller.GetSettings(clientId);

				_clientSettingsProvider.Verify(x => x.GetByClientIdAsync(It.IsAny<Guid>()), Times.Once);
				_clientSettingsProvider.Verify(x => x.GetByClientIdAsync(clientId), Times.Once);
				var notFoundResult = results as NotFoundResult;
				Assert.That(notFoundResult, Is.Not.Null);
			}

			[Test]
			public async Task ShouldCallProviderAndReturnOkIfData()
			{
				var clientId = Guid.NewGuid();
				var clientSettingsRecord = new ClientSettingsRecord();
				_clientSettingsProvider.Setup(x => x.GetByClientIdAsync(clientId)).Returns(Task.FromResult(clientSettingsRecord));

				var results = await Controller.GetSettings(clientId);

				_clientSettingsProvider.Verify(x => x.GetByClientIdAsync(clientId), Times.Once);
				_clientSettingsProvider.Verify(x => x.GetByClientIdAsync(It.IsAny<Guid>()), Times.Once);
				var okResult = results as OkNegotiatedContentResult<ClientSettingsRecord>;
				Assert.That(okResult, Is.Not.Null);
				Assert.That(okResult.Content, Is.EqualTo(clientSettingsRecord));
			}

			[Test]
			public void HasCorrectRouteAttribute()
			{
				var clientId = Guid.NewGuid();
				MethodInfo controllerMethod = GetMethod(c => c.GetSettings(clientId));

				var routeAttribute = GetAttributes<RouteAttribute>(controllerMethod).FirstOrDefault();
				Assert.IsNotNull(routeAttribute);
				Assert.AreEqual("clients/{clientId:guid}/settings", routeAttribute.Template);
			}
		}

		public class ThePutAsyncMethod : ClientSettingsControllerTests
		{
			[Test]
			public async Task ShouldCallProvideSaveAsyncIfModelValid()
			{
				var clientId = Guid.NewGuid();
				var clientSettings = new ClientSettingsRecord();
				var updatedClientSettings = new ClientSettingsRecord();
				_clientSettingsProvider.Setup(x => x.SaveAsync(clientId, clientSettings)).Returns(Task.FromResult(updatedClientSettings));
				var results = await Controller.PutAsync(clientId, clientSettings);

				_clientSettingsProvider.Verify(x => x.SaveAsync(clientId, clientSettings), Times.Once);
				_clientSettingsProvider.Verify(x => x.SaveAsync(It.IsAny<Guid>(), It.IsAny<ClientSettingsRecord>()), Times.Once);
				var okResult = results as OkNegotiatedContentResult<ClientSettingsRecord>;
				Assert.That(okResult, Is.Not.Null);
				Assert.That(okResult.Content, Is.EqualTo(updatedClientSettings));
			}
		}
	}
}