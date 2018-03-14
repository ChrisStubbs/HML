using System;

namespace HML.RestClients.EmployeeApi
{
	public class EmployeeSearchParameters : IEmployeeSearchParameters
	{
		public Guid CaseEmployeeId { get; set; }
	}
}