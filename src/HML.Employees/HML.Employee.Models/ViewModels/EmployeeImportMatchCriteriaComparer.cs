using System;
using System.Collections.Generic;

namespace HML.Employee.Models.ViewModels
{
	public class EmployeeImportMatchCriteriaComparer : IEqualityComparer<IEmployeeImportMatchCriteria>
	{
		public bool Equals(IEmployeeImportMatchCriteria x, IEmployeeImportMatchCriteria y)
		{
			return x.LastName.Equals(y.LastName, StringComparison.InvariantCultureIgnoreCase) && x.ClientId.Equals(y.ClientId) && x.DateOfBirth.Date.Equals(y.DateOfBirth.Date);
		}

		public int GetHashCode(IEmployeeImportMatchCriteria obj)
		{
			return obj.ClientId.GetHashCode();
		}
	}
}