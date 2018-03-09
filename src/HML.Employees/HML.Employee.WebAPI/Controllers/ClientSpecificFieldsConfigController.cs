using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using HML.Employee.Common.Interfaces;
using HML.Employee.Models.Entities;
using HML.Employee.Providers.Interfaces;

namespace HML.Employee.WebAPI.Controllers
{
	public class ClientSpecificFieldsConfigController : ApiController
	{
		private readonly ILogger _logger;
		private readonly IClientSpecificFieldConfigProvider _clientSpecificFieldConfigProvider;

		public ClientSpecificFieldsConfigController(ILogger logger, IClientSpecificFieldConfigProvider clientSpecificFieldConfigProvider)
		{
			_logger = logger;
			_clientSpecificFieldConfigProvider = clientSpecificFieldConfigProvider;
		}

		[HttpGet]
		[Route("clients/{clientId:guid}/client-specific-fields-config")]
		public async Task<IHttpActionResult> ClientSpecificFieldsConfig(Guid clientId)
		{
			var csfConfigs = await _clientSpecificFieldConfigProvider.GetByClientId(clientId);

			if (csfConfigs == null || !csfConfigs.Any())
			{
				return NotFound();
			}
			return Ok(csfConfigs);
		}

		[HttpPut]
		[Route("clients/{clientId:guid}/client-specific-fields-config")]
		public async Task<IHttpActionResult> PutAsync(Guid clientId, IList<ClientSpecificFieldConfigRecord> clientSpecificFields)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var updated = await _clientSpecificFieldConfigProvider.UpdateAsync(clientId, clientSpecificFields);
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
				_logger.LogError($"Error when putting client-specific-fields-config for client id {clientId}", e);
				return InternalServerError(e);
			}
		}
	}
}
