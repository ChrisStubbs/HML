using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using HML.Employee.Common.Interfaces;
using HML.Employee.Models.Entities;
using HML.Employee.Providers.Interfaces;
using System.Collections.Generic;

namespace HML.Employee.WebAPI.Controllers
{
	public class NoteController : ApiController
	{
		private readonly ILogger _logger;
		private readonly INoteProvider _noteProvider;

		public NoteController(ILogger logger, INoteProvider noteProvider)
		{
			_logger = logger;
			_noteProvider = noteProvider;
		}

	
        [HttpGet]
		[Route("employee/{id:int}/notes")]
		public async Task<IHttpActionResult> GetAsync(int id)
		{
			var notes = await _noteProvider.GetByIdAsync(id).ConfigureAwait(false);
			if (notes == null || !notes.Any())
			{
				return NotFound();
			}

			return Ok(notes);
		}

		[HttpPost]
		[Route("notes")]
		public async Task<IHttpActionResult> PostAsync(NoteRecord note)
		{
			try
			{
				if (ModelState.IsValid)
				{
					await _noteProvider.CreateAsync(note);
					return Created($"{Request.RequestUri}/{note.Id}", note);
				}

				return BadRequest(ModelState);
			}
			catch (Exception e)
			{
				_logger.LogError("Error when posting note", e);
				return InternalServerError(e);
			}

		}

		[HttpPut]
		[Route("notes")]
		public async Task<IHttpActionResult> PutAsync(NoteRecord note)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var updated = await _noteProvider.UpdateAsync(note);
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
				_logger.LogError("Error when putting note", e);
				return InternalServerError(e);
			}
		}
	}
}
