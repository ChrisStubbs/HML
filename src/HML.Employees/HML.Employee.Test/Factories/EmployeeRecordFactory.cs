using System;
using HML.Employee.Models.Entities;

namespace HML.Employee.Test.Factories
{
	public class EmployeeRecordFactory : EntityFactory<EmployeeRecordFactory, EmployeeRecord>
	{
		public EmployeeRecordFactory WithClientIdNameAndDob(Guid clientId,string lastName, DateTime dob)
		{
			With(x => x.ClientId = clientId);
			With(x => x.LastName = lastName);
			With(x => x.DateOfBirth = dob);
			return this;
		}
	}
}