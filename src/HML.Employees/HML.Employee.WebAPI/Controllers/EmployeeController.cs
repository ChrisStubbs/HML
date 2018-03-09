using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using HML.Employee.Common.Interfaces;
using HML.Employee.Models.Entities;
using HML.Employee.Providers.Interfaces;

namespace HML.Employee.WebAPI.Controllers
{
	public class EmployeeController : ApiController
	{
		private readonly ILogger _logger;
		private readonly IEmployeeProvider _employeeProvider;

		public EmployeeController(ILogger logger, IEmployeeProvider employeeProvider)
		{
			_logger = logger;
			_employeeProvider = employeeProvider;
		}

		[HttpGet]
		[Route("clients/{clientId:guid}/employees")]
		public async Task<IHttpActionResult> GetClientEmployees(Guid clientId)
		{
			var employees = await _employeeProvider.GetByClientId(clientId);

			if (employees == null || !employees.Any())
			{
				return NotFound();
			}

			return Ok(employees);
		}

		[HttpGet]
		[Route("employees")]
		public async Task<IHttpActionResult> SearchAsync(Guid caseEmployeeId)
		{
			var employee = await _employeeProvider.GetByCaseEmployeeId(caseEmployeeId).ConfigureAwait(false);
			if (employee == null)
			{
				return NotFound();
			}

			return Ok(employee);
		}

        [HttpGet]
        [Route("employees")]
        public async Task<IHttpActionResult> SearchAsync(Guid clientId, string firstName, string lastName, string dob)
        {
            DateTime dt = DateTime.Parse(dob);
            var employee = await _employeeProvider.GetByCaseEmployeeByNameOrClientIdOrDob(clientId, firstName, lastName,dt).ConfigureAwait(false);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpGet]
		[Route("employees/{id:int}")]
		public async Task<IHttpActionResult> GetAsync(int id)
		{
			var employee = await _employeeProvider.GetByIdAsync(id).ConfigureAwait(false);
			if (employee == null)
			{
				return NotFound();
			}

			return Ok(employee);
		}

		[HttpPost]
		[Route("employees")]
		public async Task<IHttpActionResult> PostAsync(EmployeeRecord employee)
		{
			try
			{
				if (ModelState.IsValid)
				{
					await _employeeProvider.CreateAsync(employee);
					return Created($"{Request.RequestUri}/{employee.Id}", employee);
				}

				return BadRequest(ModelState);
			}
			catch (Exception e)
			{
				_logger.LogError("Error when posting employee", e);
				return InternalServerError(e);
			}

		}

		[HttpPut]
		[Route("employees")]
		public async Task<IHttpActionResult> PutAsync(EmployeeRecord employee)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var updated = await _employeeProvider.UpdateAsync(employee);
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
				_logger.LogError("Error when putting employee", e);
				return InternalServerError(e);
			}
		}
	}
}
