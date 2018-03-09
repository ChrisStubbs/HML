using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Providers.Interfaces;

namespace HML.Immunisation.WebAPI.Controllers
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

		[HttpGet]
		[Route("clients/enabled")]
		public async Task<IHttpActionResult> GetEnabledClientIds()
		{
			var settings = await _clientSettingsProvider.GetClientSettingsAsync(true);

			if (settings == null)
			{
				return NotFound();
			}
			return Ok(settings.Select(x =>x.Id));
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
	}
}
