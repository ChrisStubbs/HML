namespace HML.Immunisation.Common
{
	public class CacheKeys
	{
		public static string DiseaseRisks => "DiseaseRisks";
		public static string Lookups => "Lookups";

		public static string EmployeesDiseaseRiskStatus(int employeeId)
		{
			return $"EmployeesDiseaseRiskStatus-{employeeId}";
		}
	}
}
