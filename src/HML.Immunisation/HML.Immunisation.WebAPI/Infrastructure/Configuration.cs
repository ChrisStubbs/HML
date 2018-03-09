using System.Configuration;
using HML.Immunisation.Common.Interfaces;

namespace HML.Immunisation.WebAPI.Infrastructure
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