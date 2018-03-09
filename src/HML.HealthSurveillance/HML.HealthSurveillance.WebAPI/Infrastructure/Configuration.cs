using System.Configuration;
using HML.HealthSurveillance.Common.Interfaces;

namespace HML.HealthSurveillance.WebAPI.Infrastructure
{
	public class Configuration : IConfiguration
	{
		public Configuration()
		{
			ConnectionString = ConfigurationManager.ConnectionStrings["HmlOnline"].ConnectionString;
		}
		public string ConnectionString { get; set; }
	}
}