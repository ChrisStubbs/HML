using System;
using FluentValidation.TestHelper;
using HML.Employee.Models.Entities;
using HML.Employee.Providers.Interfaces;
using HML.Employee.WebAPI.Validators;
using Moq;
using NUnit.Framework;

namespace HML.Employee.Test.Models
{
	[TestFixture]
	public class EmployeeRecordValidatorTests
	{
		private Mock<IEmployeeProvider> _employeeProvider;
		private EmployeeRecordValidator _validator;

		[SetUp]
		public void SetUp()
		{
			_employeeProvider = new Mock<IEmployeeProvider>();
			_validator = new EmployeeRecordValidator(_employeeProvider.Object);
		}

		public class TitleValidation : EmployeeRecordValidatorTests
		{
			[Test]
			public void TitleInvalidIfGreaterThan50()
			{
				_validator.ShouldHaveValidationErrorFor(x => x.Title, Helpers.RandomString(51));
			}

			[Test]
			public void TitleValidIfLessThanOrEqualTo50()
			{
				_validator.ShouldNotHaveValidationErrorFor(x => x.Title, Helpers.RandomString(50));
			}

		}

		public class EmployeeIdValidation : EmployeeRecordValidatorTests
		{
			[Test]
			public void EmployeeIdInvalidIfGreaterThan50()
			{
				_employeeProvider.Setup(x => x.IsEmployeeIdIsUniqueForClient(It.IsAny<EmployeeRecord>())).Returns(true);
				_validator.ShouldHaveValidationErrorFor(x => x.EmployeeId, Helpers.RandomString(51));
			}

			[Test]
			public void EmployeeIdValidIfLessThanOrEqualTo50()
			{
				_employeeProvider.Setup(x => x.IsEmployeeIdIsUniqueForClient(It.IsAny<EmployeeRecord>())).Returns(true);
				_validator.ShouldNotHaveValidationErrorFor(x => x.EmployeeId, Helpers.RandomString(50));
			}

			[Test]
			public void EmployeeIdInValidIfEmployeeIdNotUnique()
			{
				_employeeProvider.Setup(x => x.IsEmployeeIdIsUniqueForClient(It.IsAny<EmployeeRecord>())).Returns(true);
				_validator.ShouldNotHaveValidationErrorFor(x => x.EmployeeId, (string)null);
			}

			[Test]
			public void EmployeeIdNotValidIfIdNotUnique()
			{
				var emp = new EmployeeRecord();
				_employeeProvider.Setup(x => x.IsEmployeeIdIsUniqueForClient(emp)).Returns(false);

				_validator.ShouldHaveValidationErrorFor(x => x.EmployeeId, emp);

				_employeeProvider.Verify(x => x.IsEmployeeIdIsUniqueForClient(emp), Times.Once);

			}
		}

		public class FirstNameValidation : EmployeeRecordValidatorTests
		{
			[Test]
			public void FirstNameInvalidIfGreaterThan50()
			{
				_validator.ShouldHaveValidationErrorFor(x => x.FirstName, Helpers.RandomString(51));
			}

			[Test]
			public void FirstNameValidIfLessThanOrEqualTo50()
			{
				_validator.ShouldNotHaveValidationErrorFor(x => x.FirstName, Helpers.RandomString(50));
			}

			[Test]
			public void FirstNameInValidIfEmpty()
			{
				_validator.ShouldHaveValidationErrorFor(x => x.FirstName, string.Empty);
			}

			[Test]
			public void FirstNameInValidIfNull()
			{
				_validator.ShouldHaveValidationErrorFor(x => x.FirstName, (string)null);
			}
		}

		public class LastNameValidation : EmployeeRecordValidatorTests
		{
			[Test]
			public void LastNameInvalidIfGreaterThan50()
			{
				_validator.ShouldHaveValidationErrorFor(x => x.LastName, Helpers.RandomString(51));
			}

			[Test]
			public void LastNameValidIfLessThanOrEqualTo50()
			{
				_validator.ShouldNotHaveValidationErrorFor(x => x.LastName, Helpers.RandomString(50));
			}

			[Test]
			public void LastNameInValidIfEmpty()
			{
				_validator.ShouldHaveValidationErrorFor(x => x.LastName, string.Empty);
			}

			[Test]
			public void LastNameInValidIfNull()
			{
				_validator.ShouldHaveValidationErrorFor(x => x.LastName, (string)null);
			}
		}

		public class JobTitleValidation : EmployeeRecordValidatorTests
		{
			[Test]
			public void JobTitleInvalidIfGreaterThan100()
			{
				_validator.ShouldHaveValidationErrorFor(x => x.JobTitle, Helpers.RandomString(101));
			}

			[Test]
			public void JobTitleValidIfLessThanOrEqualTo100()
			{
				_validator.ShouldNotHaveValidationErrorFor(x => x.JobTitle, Helpers.RandomString(100));
			}

			[Test]
			public void JobTitleValidIfEmpty()
			{
				_validator.ShouldNotHaveValidationErrorFor(x => x.JobTitle, string.Empty);
			}

			[Test]
			public void JobTitleValidIfNull()
			{
				_validator.ShouldNotHaveValidationErrorFor(x => x.JobTitle, (string)null);
			}
		}

		public class EndDateValidation : EmployeeRecordValidatorTests
		{
			[Test]
			public void EndDateValidationShouldCallIsEndDateGreaterThanStartDateMethod()
			{
				var emp = new Mock<EmployeeRecord>();
				emp.Setup(x => x.IsEndDateGreaterThanStartDate()).Returns(true);
				_validator.ShouldNotHaveValidationErrorFor(x => x.EndDate, emp.Object);
				emp.Verify(x => x.IsEndDateGreaterThanStartDate(), Times.Once);
			}

			[Test]
			public void ValidationShouldFailIfIsEndDateSetWhenUserIsActiveReturnsTrue()
			{
				var emp = new Mock<EmployeeRecord>();
				emp.Setup(x => x.IsEndDateSetWhenUserIsActive()).Returns(true);
				_validator.ShouldHaveValidationErrorFor(x => x.EndDate, emp.Object);
				emp.Verify(x => x.IsEndDateSetWhenUserIsActive(), Times.Once);
			}

			[Test]
			public void IfEndDateIsSetAndUserIsActiveValidationError()
			{
				var emp = new EmployeeRecord { EndDate = DateTime.Now, IsActive = true };
				_validator.ShouldHaveValidationErrorFor(x => x.EndDate, emp);
			}

			[Test]
			public void IfEndDateIsSetAndUserIsInActiveNoValidationError()
			{
				var emp = new EmployeeRecord { EndDate = DateTime.Now, IsActive = false };
				_validator.ShouldNotHaveValidationErrorFor(x => x.EndDate, emp);
			}

			[Test]
			public void IfEndDateNotSetAndUserIsInActiveNoValidationError()
			{
				var emp = new EmployeeRecord { EndDate = null, IsActive = false };
				_validator.ShouldNotHaveValidationErrorFor(x => x.EndDate, emp);
			}

			[Test]
			public void IfEndDateNotSetAndUserIsActiveNoValidationError()
			{
				var emp = new EmployeeRecord { EndDate = null, IsActive = true };
				_validator.ShouldNotHaveValidationErrorFor(x => x.EndDate, emp);
			}
		}

		public class NationalInsuranceNumberValidation : EmployeeRecordValidatorTests
		{
			[Test]
			public void NationalInsuranceNumberShouldCallIsValidNationalIsuranceNumber()
			{
				var emp = new Mock<EmployeeRecord>();
				emp.Setup(x => x.IsValidNationalIsuranceNumber()).Returns(true);
				_validator.ShouldNotHaveValidationErrorFor(x => x.NationalInsuranceNumber, emp.Object);
				emp.Verify(x => x.IsValidNationalIsuranceNumber(), Times.Once);
			}
		}

		public class NoOfHoursWorkedValidation : EmployeeRecordValidatorTests
		{
			[Test]
			public void NoOfHoursWorkedShouldCallIsNoOfHoursWorkedValid()
			{
				var emp = new Mock<EmployeeRecord>();
				emp.Setup(x => x.IsNoOfHoursWorkedValid()).Returns(true);
				_validator.ShouldNotHaveValidationErrorFor(x => x.NoOfHoursWorked, emp.Object);
				emp.Verify(x => x.IsNoOfHoursWorkedValid(), Times.Once);
			}
		}

		public class DateOfBirthValidation : EmployeeRecordValidatorTests
		{
			[Test]
			public void DateOfBirthInValidIfDateInFuture()
			{
				_validator.ShouldHaveValidationErrorFor(x => x.DateOfBirth, DateTime.Today.AddDays(1));
			}

		}


	}
}