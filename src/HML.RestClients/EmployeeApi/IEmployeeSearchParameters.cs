using System;

namespace HML.RestClients.EmployeeApi
{
	public interface IEmployeeSearchParameters
	{
		Guid CaseEmployeeId { get; set; }
	}
}