using System;
using System.Web.Http;
using HML.Employee.Common.Interfaces;
using HML.Employee.Models.Entities;
using HML.Employee.Providers;

namespace HML.Employee.WebAPI.Controllers
{
	public class EmployeeImportController : ApiController
	{
		private readonly ILogger _logger;
		private readonly IEmployeeImportProvider _employeeImportProvider;

		public EmployeeImportController(ILogger logger, IEmployeeImportProvider employeeImportProvider)
		{
			_logger = logger;
			_employeeImportProvider = employeeImportProvider;
		}

		[HttpPost]
		[Route("employees-import")]
		public IHttpActionResult Post(ImportRecord import)
		{
			try
			{
				if (ModelState.IsValid)
				{
					
					return Created($"{Request.RequestUri}", _employeeImportProvider.Import(import));
				}

				return BadRequest(ModelState);
			}
			catch (Exception e)
			{
				_logger.LogError("Error when importing employees", e);
				return InternalServerError(e);
			}
		}

	}
}