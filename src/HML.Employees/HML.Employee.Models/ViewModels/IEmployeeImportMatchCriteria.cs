using System;

namespace HML.Employee.Models.ViewModels
{
	public interface IEmployeeImportMatchCriteria
	{
		DateTime DateOfBirth { get; set; }
		string LastName { get; set; }
		Guid ClientId { get; set; }

	}
}