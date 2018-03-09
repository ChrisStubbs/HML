using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Providers.Interfaces;

namespace HML.Immunisation.WebAPI.Controllers
{
	public class EmployeeDiseaseRiskStatusController : ApiController
	{
		private readonly ILogger _logger;
		private readonly IEmployeeDiseaseRiskStatusProvider _employeeDiseaseRiskStatusProvider;


		public EmployeeDiseaseRiskStatusController(ILogger logger, IEmployeeDiseaseRiskStatusProvider employeeDiseaseRiskStatusProvider)
		{
			_logger = logger;
			_employeeDiseaseRiskStatusProvider = employeeDiseaseRiskStatusProvider;
		}

		[HttpGet]
		[Route("employees/{employeeId:int}/disease-risk-status")]
		public async Task<IHttpActionResult> GetEmployeeDiseaseRiskStatusesAsync(int employeeId)
		{
			var diseaseRiskStatus = await _employeeDiseaseRiskStatusProvider.GetEmployeesDiseaseRiskStatusAsync(employeeId);

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
					var updated = await _employeeDiseaseRiskStatusProvider.SaveAsync(employeeId, diseaseRiskStatusRecords);
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
