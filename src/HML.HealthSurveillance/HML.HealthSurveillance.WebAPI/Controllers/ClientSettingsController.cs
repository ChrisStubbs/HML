using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using HML.HealthSurveillance.Common.Interfaces;
using HML.HealthSurveillance.Models.Entities;
using HML.HealthSurveillance.Providers.Interfaces;

namespace HML.HealthSurveillance.WebAPI.Controllers
{
	public class ClientSettingsController : ApiController
	{
		private readonly ILogger _logger;
		private readonly IClientSettingsProvider _clientSettingsProvider;


		public ClientSettingsController(ILogger logger, IClientSettingsProvider clientSettingsProvider)
		{
			_logger = logger;
			_clientSettingsProvider = clientSettingsProvider;
		}

		[HttpGet]
		[Route("clients/{clientId:guid}/settings")]
		public async Task<IHttpActionResult> GetSettings(Guid clientId)
		{
			var settings = await _clientSettingsProvider.GetByClientIdAsync(clientId);

			if (settings == null)
			{
				return NotFound();
			}
			return Ok(settings);
		}

		[HttpPut]
		[Route("clients/{clientId:guid}/settings")]
		public async Task<IHttpActionResult> PutAsync(Guid clientId, ClientSettingsRecord settings)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var updated = await _clientSettingsProvider.SaveAsync(clientId, settings);
					if (updated != null)
					{
						return Ok(updated);
					}

					return NotFound();
				}

				return BadRequest(ModelState);
			}
			catch (Exception e)
			{
				_logger.LogError($"Error when putting settings for client id {clientId}", e);
				return InternalServerError(e);
			}
		}


		[HttpGet]
		[Route("clients/enabled")]
		public async Task<IHttpActionResult> GetEnabledClientIds()
		{
			var settings = await _clientSettingsProvider.GetClientSettingsAsync(true);

			if (settings == null)
			{
				return NotFound();
			}
			return Ok(settings.Select(x => x.Id));
		}
	}
}
