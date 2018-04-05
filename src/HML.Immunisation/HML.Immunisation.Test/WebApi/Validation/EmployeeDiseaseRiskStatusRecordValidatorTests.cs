using System;
using System.Collections.Generic;
using FluentValidation.TestHelper;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Providers.Interfaces;
using HML.Immunisation.Test.Factories;
using HML.Immunisation.WebAPI.Infrastructure;
using HML.Immunisation.WebAPI.Validators;
using Moq;
using NUnit.Framework;

namespace HML.Immunisation.Test.WebApi.Validation
{
	[TestFixture]
	public class EmployeeDiseaseRiskStatusRecordValidatorTests
	{
		private Mock<ICachedEmployeeDiseaseRiskStatusProvider> _employeeDiseaseRiskStatusProvider;
		private Mock<IDiseaseRiskProvider> _diseaseRiskProvider;

		private EmployeeDiseaseRiskStatusRecordValidator _validator;

		[SetUp]
		public void SetUp()
		{
			_employeeDiseaseRiskStatusProvider = new Mock<ICachedEmployeeDiseaseRiskStatusProvider>();
			_diseaseRiskProvider = new Mock<IDiseaseRiskProvider>();
			_validator = new EmployeeDiseaseRiskStatusRecordValidator(_employeeDiseaseRiskStatusProvider.Object, _diseaseRiskProvider.Object);
		}
		public class ImmunistaionStatusValidation : EmployeeDiseaseRiskStatusRecordValidatorTests
		{

			[Test]
			public void IsRequiredValidatorShouldCallIsImmsStatusIdSetWhenNotRequiredAndErrorIfTrue()
			{
				var emp = new Mock<EmployeeDiseaseRiskStatusRecord>();
				emp.Setup(x => x.IsImmsStatusIdSetWhenNotRequired()).Returns(true);

				_validator.ShouldHaveValidationErrorFor(x => x.IsRequired, emp.Object);

				emp.Verify(x => x.IsImmsStatusIdSetWhenNotRequired(), Times.Once);
			}

			[Test]
			public void IsRequiredValidatorShouldCallIsImmsStatusIdSetWhenNotRequiredAndNotErrorIfFalse()
			{
				var emp = new Mock<EmployeeDiseaseRiskStatusRecord>();
				emp.Setup(x => x.IsImmsStatusIdSetWhenNotRequired()).Returns(false);

				_validator.ShouldNotHaveValidationErrorFor(x => x.IsRequired, emp.Object);

				emp.Verify(x => x.IsImmsStatusIdSetWhenNotRequired(), Times.Once);
			}
		}

		public class DateProtectedValidation : EmployeeDiseaseRiskStatusRecordValidatorTests
		{
			[Test]
			public void ShouldCallIsDateProtectedSetWhenStatusNotProtectedAndReturnErrorIfTrue()
			{
				var emp = new Mock<EmployeeDiseaseRiskStatusRecord>();
				emp.Setup(x => x.IsDateProtectedSetWhenStatusNotProtected()).Returns(true);

				_validator.ShouldHaveValidationErrorFor(x => x.DateProtected, emp.Object);

				emp.Verify(x => x.IsDateProtectedSetWhenStatusNotProtected(), Times.Once);
			}

			[Test]
			public void ShouldCallIsDateProtectedSetWhenStatusNotProtectedAndReturnNoErrorIfFalse()
			{
				var emp = new Mock<EmployeeDiseaseRiskStatusRecord>();
				emp.Setup(x => x.IsDateProtectedSetWhenStatusNotProtected()).Returns(false);

				_validator.ShouldNotHaveValidationErrorFor(x => x.DateProtected, emp.Object);

				emp.Verify(x => x.IsDateProtectedSetWhenStatusNotProtected(), Times.Once);
			}
		}

		public class RecallDateValidation : EmployeeDiseaseRiskStatusRecordValidatorTests
		{
			[Test]
			public void ShouldCallHasRecallDateChangedToDateInThePastAndReturnErrorIfTrue()
			{
				var emp = new Mock<EmployeeDiseaseRiskStatusRecord>();
				emp.Object.Id = 1;
				emp.Object.EmployeeId = 24;
				emp.Object.RecallDate = DateTime.Today.AddDays(-5);

				emp.Setup(x => x.HasRecallDateChangedToValueInThePast(It.IsAny<EmployeeDiseaseRiskStatusRecord>())).Returns(true);

				_validator.ShouldHaveValidationErrorFor(x => x.RecallDate, emp.Object);

			}

			[Test]
			public void ShouldCallHasRecallDateChangedToDateInThePastAndReturnNoErrorIfRecallDateNull()
			{
				var emp = new EmployeeDiseaseRiskStatusRecordFactory().With(x=> x.RecallDate = null).Build();

				_validator.ShouldNotHaveValidationErrorFor(x => x.RecallDate, emp);
			}

			[Test]
			public void ShouldCallHasRecallDateChangedToDateInThePastOnRecordWithNullValueIfNewRecord()
			{
				var emp = new Mock<EmployeeDiseaseRiskStatusRecord>();
				emp.Object.Id = 0;
				emp.Object.RecallDate = DateTime.Today.AddDays(-5);
				
				emp.Setup(x => x.HasRecallDateChangedToValueInThePast(null)).Returns(true);

				_validator.ShouldHaveValidationErrorFor(x => x.RecallDate, emp.Object);

				emp.Verify(x => x.HasRecallDateChangedToValueInThePast(It.IsAny<EmployeeDiseaseRiskStatusRecord>()), Times.Once);
				emp.Verify(x => x.HasRecallDateChangedToValueInThePast(null),Times.Once);
			}

			[Test]
			public void ShouldCallHasRecallDateChangedToDateInThePastOnRecordWithCurrentRecordFromEmployeeDiseaseRiskProvider()
			{
				var emp = new Mock<EmployeeDiseaseRiskStatusRecord>();
				emp.Object.Id = 1;
				emp.Object.EmployeeId = 4;
				emp.Object.RecallDate = DateTime.Today.AddDays(-5);

				var currentEmpDiseaseRiskRecords  = new List<EmployeeDiseaseRiskStatusRecord>
				{
					new EmployeeDiseaseRiskStatusRecordFactory().With(x=> x.Id = 1).Build()
				};

				_employeeDiseaseRiskStatusProvider.Setup(x => x.GetEmployeesDiseaseRiskStatus(emp.Object.EmployeeId))
					.Returns(currentEmpDiseaseRiskRecords);

				emp.Setup(x => x.HasRecallDateChangedToValueInThePast(currentEmpDiseaseRiskRecords[0])).Returns(true);

				_validator.ShouldHaveValidationErrorFor(x => x.RecallDate, emp.Object);

				emp.Verify(x => x.HasRecallDateChangedToValueInThePast(It.IsAny<EmployeeDiseaseRiskStatusRecord>()), Times.Once);
				emp.Verify(x => x.HasRecallDateChangedToValueInThePast(currentEmpDiseaseRiskRecords[0]), Times.Once);

				_employeeDiseaseRiskStatusProvider.Verify(x => x.GetEmployeesDiseaseRiskStatus(emp.Object.EmployeeId),Times.Once);
				_employeeDiseaseRiskStatusProvider.Verify(x => x.GetEmployeesDiseaseRiskStatus(It.IsAny<int>()), Times.Once);
			}
		}
	}
}