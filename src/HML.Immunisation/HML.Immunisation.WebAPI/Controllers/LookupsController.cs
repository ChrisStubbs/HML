using System.Threading.Tasks;
using System.Web.Http;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Providers.Interfaces;

namespace HML.Immunisation.WebAPI.Controllers
{
	public class LookupsController : ApiController
	{
		private readonly ILogger _logger;
		private readonly ILookupsProvider _lookupsProvider;


		public LookupsController(ILogger logger, ILookupsProvider lookupsProvider)
		{
			_logger = logger;
			_lookupsProvider = lookupsProvider;
		}

		[HttpGet]
		[Route("lookups")]
		public async Task<IHttpActionResult> GetAsync()
		{
			var lookups = await _lookupsProvider.GetAsync();

			if (lookups == null)
			{
				return NotFound();
			}
			return Ok(lookups);
		}
	}
}
