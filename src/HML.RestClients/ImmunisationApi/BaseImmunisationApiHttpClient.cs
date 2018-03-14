namespace HML.RestClients.ImmunisationApi
{
	public class BaseImmunisationApiHttpClient : BaseHttpClient
	{
		public BaseImmunisationApiHttpClient(IConfig config) : base(config)
		{
		}

		protected override string BaseUrl => Config.ImmunisationApiBaseUrl;

	}
}