namespace HML.RestClients.EmployeeApi
{
	public class BaseEmployeeApiHttpClient : BaseHttpClient
	{
		public BaseEmployeeApiHttpClient(IConfig config) : base(config)
		{
		}

		protected override string BaseUrl => Config.EmployeeApiBaseUrl;

	}
}