using System.Configuration;
using HML.Employee.Common.Interfaces;

namespace HML.Employee.WebAPI.Infrastructure
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