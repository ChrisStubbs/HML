using System;
using System.Collections.Generic;
using System.Linq;
using HML.Employee.Models.Entities;
using HML.Employee.Models.ViewModels;
using HML.Employee.Test.Factories;
using HML.Employee.WebAPI.Infrastructure;
using HML.Employee.WebAPI.Validators;
using Moq;
using NUnit.Framework;

namespace HML.Employee.Test.WebApi.Validators
{
	[TestFixture]
	public class ImportValidatorTest
	{
		private Mock<ICachedEmployeeImportValidatorProvider> _employeeImportValidatorProvider;

		[SetUp]
		public void SetUp()
		{
			_employeeImportValidatorProvider = new Mock<ICachedEmployeeImportValidatorProvider>();
		}
		public class TheHasDuplicateLastNameDateOfBirthMethod : ImportValidatorTest
		{
			[Test]
			public void ShouldReturnTrueIfHasDuplicates()
			{
				var clientId = Guid.NewGuid();
				var importRecords = new List<EmployeeRecord>
				{
					new EmployeeRecordFactory().WithClientIdNameAndDob(clientId,"Stubbs",DateTime.Now).Build(),
					new EmployeeRecordFactory().WithClientIdNameAndDob(clientId,"Stubbs",DateTime.Today).Build(),
					new EmployeeRecordFactory().WithClientIdNameAndDob(clientId,"Bickers",DateTime.Today).Build(),
				};

				Assert.IsTrue(ImportValidator.HasDuplicateLastNameDateOfBirth(importRecords));

			}

			[Test]
			public void ShouldReturnFalseIfNoDuplicates()
			{
				var clientId = Guid.NewGuid();
				var importRecords = new List<EmployeeRecord>
				{
					new EmployeeRecordFactory().WithClientIdNameAndDob(clientId,"Stubbs",DateTime.Today).Build(),
					new EmployeeRecordFactory().WithClientIdNameAndDob(clientId,"Bickers",DateTime.Today).Build(),
					new EmployeeRecordFactory().WithClientIdNameAndDob(Guid.NewGuid(),"Bickers",DateTime.Today).Build(),
					new EmployeeRecordFactory().WithClientIdNameAndDob(clientId,"Bickers",DateTime.Today.AddDays(50)).Build()
				};

				Assert.IsFalse(ImportValidator.HasDuplicateLastNameDateOfBirth(importRecords));

			}
		}

		public class TheIsEmployeeNameAndDobUniqueForClientMethod : ImportValidatorTest
		{
			[Test]
			public void ShouldReturnFalseIfNotUnique()
			{
				var clientId = Guid.NewGuid();

				var dbRecords  = new List<EmployeeRecord>
				{
					new EmployeeRecordFactory().WithClientIdNameAndDob(clientId,"Stubbs",DateTime.Now).Build(),
					new EmployeeRecordFactory().WithClientIdNameAndDob(clientId,"Bickers",DateTime.Today).Build(),
					new EmployeeRecordFactory().WithClientIdNameAndDob(clientId,"Jones",DateTime.Today).Build(),
				};

				var importRecords = new List<EmployeeRecord>
				{
					new EmployeeRecordFactory().WithClientIdNameAndDob(clientId,"Richardson",DateTime.Now).Build(),
					new EmployeeRecordFactory().WithClientIdNameAndDob(clientId,"Syed",DateTime.Now).Build(),
					new EmployeeRecordFactory().WithClientIdNameAndDob(clientId,"Bickers",DateTime.Today).Build(),
					new EmployeeRecordFactory().WithClientIdNameAndDob(clientId,"Jones",DateTime.Today).Build(),
				};

				_employeeImportValidatorProvider.Setup(x => x.GetEmployeesImportMatchCriteriaByClient(clientId)).Returns(dbRecords.OfType<IEmployeeImportMatchCriteria>().ToList());

				var validator = new ImportValidator(_employeeImportValidatorProvider.Object);
				Assert.That(validator.IsEmployeeNameAndDobUniqueForClient(importRecords.OfType<IEmployeeImportMatchCriteria>().ToList()),Is.False);
			}

			[Test]
			public void ShouldReturnTrueIfUnique()
			{
				var clientId = Guid.NewGuid();

				var dbRecords = new List<EmployeeRecord>
				{
					new EmployeeRecordFactory().WithClientIdNameAndDob(clientId,"Stubbs",DateTime.Now).Build(),
					new EmployeeRecordFactory().WithClientIdNameAndDob(clientId,"Bickers",DateTime.Today).Build(),
					new EmployeeRecordFactory().WithClientIdNameAndDob(clientId,"Jones",DateTime.Today).Build(),
					
				};

				var importRecords = new List<EmployeeRecord>
				{
					new EmployeeRecordFactory().WithClientIdNameAndDob(clientId,"Richardson",DateTime.Now).Build(),
					new EmployeeRecordFactory().WithClientIdNameAndDob(clientId,"Syed",DateTime.Now).Build(),
					new EmployeeRecordFactory().WithClientIdNameAndDob(clientId,"Jones",DateTime.Today.AddDays(-50)).Build(),
				};

				_employeeImportValidatorProvider.Setup(x => x.GetEmployeesImportMatchCriteriaByClient(clientId)).Returns(dbRecords.OfType<IEmployeeImportMatchCriteria>().ToList());

				var validator = new ImportValidator(_employeeImportValidatorProvider.Object);
				Assert.That(validator.IsEmployeeNameAndDobUniqueForClient(importRecords.OfType<IEmployeeImportMatchCriteria>().ToList()),
					Is.True);

			}
		}
	}
}