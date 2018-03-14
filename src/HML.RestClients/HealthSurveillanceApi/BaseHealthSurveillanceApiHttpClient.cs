namespace HML.RestClients.HealthSurveillanceApi
{
	public class BaseHealthSurveillanceApiHttpClient : BaseHttpClient
	{
		public BaseHealthSurveillanceApiHttpClient(IConfig config) : base(config)
		{
		}

		protected override string BaseUrl => Config.HealthSurveillanceApiBaseUrl;

	}
}