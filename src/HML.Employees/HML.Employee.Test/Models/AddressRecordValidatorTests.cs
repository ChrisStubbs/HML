using FluentValidation.TestHelper;
using HML.Employee.WebAPI.Validators;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace HML.Employee.Test.Models
{
	[TestFixture]
	public class AddressRecordValidatorTests
	{
		private readonly AddressRecordValidator _validator = new AddressRecordValidator();

		public class LinesValidation : AddressRecordValidatorTests
		{
			[Test]
			public void TitleInvalidIfGreaterThan500()
			{
				_validator.ShouldHaveValidationErrorFor(x => x.Lines, Helpers.RandomString(501));
			}

			[Test]
			public void TitleValidIfLessThanOrEqualTo500()
			{
				_validator.ShouldNotHaveValidationErrorFor(x => x.Lines, Helpers.RandomString(500));
			}
		}

		public class PostCodeValidation : AddressRecordValidatorTests
		{
			[Test]
			public void PostCodeInvalidIfGreaterThan9Characters()
			{
				_validator.ShouldHaveValidationErrorFor(x => x.Postcode, Helpers.RandomString(10));
			}

			[Test]
			public void ValidPostCode()
			{
				_validator.ShouldNotHaveValidationErrorFor(x => x.Postcode, "CR6 9TP");
			}
		}
	}
}