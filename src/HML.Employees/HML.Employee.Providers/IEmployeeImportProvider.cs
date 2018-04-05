using HML.Employee.Models;
using HML.Employee.Models.Entities;
using HML.Employee.Models.ViewModels;

namespace HML.Employee.Providers
{
	public interface IEmployeeImportProvider
	{
		EmployeeContext GetDbContext();
		EmployeeImportResults Import(ImportRecord importRecord);
	}
}