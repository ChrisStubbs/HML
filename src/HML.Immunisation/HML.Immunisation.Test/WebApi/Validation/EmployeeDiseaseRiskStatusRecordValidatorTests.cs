using FluentValidation.TestHelper;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Providers.Interfaces;
using HML.Immunisation.WebAPI.Validators;
using Moq;
using NUnit.Framework;

namespace HML.Immunisation.Test.WebApi.Validation
{
	[TestFixture]
	public class EmployeeDiseaseRiskStatusRecordValidatorTests
	{
		private Mock<IEmployeeDiseaseRiskStatusProvider> _employeeDiseaseRiskStatusProvider;
		private Mock<IDiseaseRiskProvider> _diseaseRiskProvider;

		private EmployeeDiseaseRiskStatusRecordValidator _validator;

		[SetUp]
		public void SetUp()
		{
			_employeeDiseaseRiskStatusProvider = new Mock<IEmployeeDiseaseRiskStatusProvider>();
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

	}
}