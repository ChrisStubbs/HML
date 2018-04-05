using System;

namespace HML.Employee.Models.ViewModels
{
	public class EmployeeImportMatchCriteria : IEmployeeImportMatchCriteria
	{
		public string LastName { get; set; }
		public Guid ClientId { get; set; }
		public DateTime DateOfBirth { get; set; }
	}
}