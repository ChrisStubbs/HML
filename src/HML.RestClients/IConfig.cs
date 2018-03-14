namespace HML.RestClients
{
	public interface IConfig
	{
		string EmployeeApiBaseUrl { get; }
		string ImmunisationApiBaseUrl { get; }
		string HealthSurveillanceApiBaseUrl { get; }
		string Username { get; }
	}
}