using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Providers.Interfaces;

namespace HML.Immunisation.WebAPI.Controllers
{
	public class DiseaseRiskController : ApiController
	{
		private readonly ILogger _logger;
		private readonly IDiseaseRiskProvider _diseaseRiskProvider;

		public DiseaseRiskController(ILogger logger, IDiseaseRiskProvider diseaseRiskProvider)
		{
			_logger = logger;
			_diseaseRiskProvider = diseaseRiskProvider;
		}

		[HttpGet]
		[Route("disease-risks")]
		public async Task<IHttpActionResult> GetAll()
		{
			var risks = await _diseaseRiskProvider.GetAllAsync();

			if (risks == null || !risks.Any())
			{
				return NotFound();
			}

			return Ok(risks);
	
		}
	}
}