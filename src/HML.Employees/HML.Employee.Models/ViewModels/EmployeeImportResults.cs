using System;
using HML.Employee.Common;

namespace HML.Employee.Models.ViewModels
{
	public class EmployeeImportResults
	{
		public int ImportId { get; set; }
		public int EmployeeImportCount { get; set; }
		public string ImportedBy { get; set; }
		public string Source { get; set; }
		public DateTime? ImportDate { get; set; }
		public string Message => $"The file: {Source} has been successfully imported. {Environment.NewLine}" +
								 $"Import Count: {EmployeeImportCount} {Environment.NewLine}" +
								 $"User: {ImportedBy} {Environment.NewLine}" +
		                         $"Import Date: {ImportDate.ToDateTimeFormat()}";
	}
}