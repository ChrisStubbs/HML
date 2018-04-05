using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Providers.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace HML.Immunisation.WebAPI.Controllers
{
    public class ImmunisationStatusController : ApiController
    {
        private readonly ILogger _logger;
        private readonly IImmunisationStatusProvider _immunisationStatusProvider;

        public ImmunisationStatusController(ILogger logger, IImmunisationStatusProvider immunisationStatusProvider)
        {
            _logger = logger;
            _immunisationStatusProvider = immunisationStatusProvider;
        }

        [HttpGet]
        [Route("immunisation-statuses")]
        public async Task<IHttpActionResult> GetAll()
        {
            var statuses = await _immunisationStatusProvider.GetAllAsync();

            if (statuses == null || !statuses.Any())
            {
                return NotFound();
            }

            return Ok(statuses);
        }
    }
}
