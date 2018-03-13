using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Providers.Interfaces;
using HML.Immunisation.WebAPI.Mappers;

namespace HML.Immunisation.WebAPI.Controllers
{
	public class EmployeeDiseaseRiskStatusController : ApiController
	{
		private readonly ILogger _logger;
		private readonly IEmployeeDiseaseRiskStatusProvider _employeeDiseaseRiskStatusProvider;
		private readonly IEmployeeDiseaseRiskStatusMapper _employeeDiseaseRiskStatusMapper;


		public EmployeeDiseaseRiskStatusController(
			ILogger logger,
			IEmployeeDiseaseRiskStatusProvider employeeDiseaseRiskStatusProvider,
			IEmployeeDiseaseRiskStatusMapper employeeDiseaseRiskStatusMapper)
		{
			_logger = logger;
			_employeeDiseaseRiskStatusProvider = employeeDiseaseRiskStatusProvider;
			_employeeDiseaseRiskStatusMapper = employeeDiseaseRiskStatusMapper;
		}

		[HttpGet]
		[Route("clients/{clientId:Guid}/employees/{employeeId:int}/disease-risk-status")]
		public async Task<IHttpActionResult> GetEmployeeDiseaseRiskStatusesAsync(Guid clientId, int employeeId)
		{
			var diseaseRiskStatus = await _employeeDiseaseRiskStatusMapper.GetEmployeesDiseaseRiskStatusAsync(clientId,
				await _employeeDiseaseRiskStatusProvider.GetEmployeesDiseaseRiskStatusAsync(employeeId));

			if (diseaseRiskStatus == null || !diseaseRiskStatus.Any())
			{
				return NotFound();
			}
			return Ok(diseaseRiskStatus);
		}

		[HttpGet]
		[Route("clients/{clientId:Guid}/employees/disease-risk-status")]
		public async Task<IHttpActionResult> GetEmployeeDiseaseRiskStatusesAsync(Guid clientId)
		{
			var diseaseRiskStatus = await _employeeDiseaseRiskStatusMapper.GetEmployeesDiseaseRiskStatusAsync(clientId,
					await  _employeeDiseaseRiskStatusProvider.GetClientsEmployeesDiseaseRiskStatusAsync(clientId));

			if (diseaseRiskStatus == null || !diseaseRiskStatus.Any())
			{
				return NotFound();
			}
			return Ok(diseaseRiskStatus);
		}

		[HttpPut]
		[Route("employees/{employeeId:int}/disease-risk-status")]
		public async Task<IHttpActionResult> PutAsync(int employeeId, IList<EmployeeDiseaseRiskStatusRecord> diseaseRiskStatusRecords)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var updated = await
						_employeeDiseaseRiskStatusMapper.MapEmployeesDiseaseRiskStatusAsync(
							await _employeeDiseaseRiskStatusProvider.SaveAsync(employeeId, diseaseRiskStatusRecords)
							);
					
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
				_logger.LogError($"Error when putting Disease Risk Status for employee id {employeeId}", e);
				return InternalServerError(e);
			}
		}
	}
}
