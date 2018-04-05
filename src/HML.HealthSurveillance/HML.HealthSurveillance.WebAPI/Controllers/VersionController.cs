using System;
using System.IO;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Http;

namespace HML.HealthSurveillance.WebAPI.Controllers
{
    public class VersionController : ApiController
    {
		private static readonly string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
	    private static readonly string Name = Assembly.GetExecutingAssembly().GetName().ToString();
		public IHttpActionResult Get()
	    {
			var deploymentDate = File.GetLastWriteTime(Path.Combine(HostingEnvironment.MapPath("~"), "web.config"));
			string version = $"{Name} {Version} ({deploymentDate.ToShortDateString()})";

		    return Ok(version);
	    }
    }
}
