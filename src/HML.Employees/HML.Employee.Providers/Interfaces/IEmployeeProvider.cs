using System.Threading.Tasks;
using HML.Employee.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HML.Employee.Models;

namespace HML.Employee.Providers.Interfaces
{
	public interface IEmployeeProvider
	{
		Task<EmployeeRecord> GetByIdAsync(int id);
		Task CreateAsync(EmployeeRecord employee);
		Task<EmployeeRecord> UpdateAsync(EmployeeRecord employee);
		Task<IList<EmployeeRecord>> Search(Expression<Func<EmployeeRecord, bool>> filterExpression);
		Task<IList<EmployeeRecord>> GetByClientId(Guid clientId);
		Task<IList<EmployeeRecord>> GetByCaseEmployeeId(Guid caseEmployeeId);
        Task<IList<EmployeeRecord>> GetByCaseEmployeeByNameOrClientIdOrDob(Guid clientId, string firstName, string lastName, DateTime dob);

    }
}