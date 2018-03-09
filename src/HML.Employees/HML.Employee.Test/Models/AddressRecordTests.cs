using HML.Employee.Models.Entities;
using NUnit.Framework;

namespace HML.Employee.Test.Models
{
	[TestFixture]
	public class AddressRecordTests
	{
		public class TheIsValidPostCodeMethod : AddressRecordTests
		{

			[Test]
			[TestCase("BN2 0BN", ExpectedResult = true)]
			[TestCase("BN22 0BN", ExpectedResult = true)]
			[TestCase("BN222 0BN", ExpectedResult = false)]
			[TestCase("bn2 0bn", ExpectedResult = false)]
			[TestCase("BN22 9TZ", ExpectedResult = true)]
			[TestCase("BN22 9CZ", ExpectedResult = false)]
			[TestCase("BN22 9IZ", ExpectedResult = false)]
			[TestCase("BN22 9KZ", ExpectedResult = false)]
			[TestCase("BN22 9MZ", ExpectedResult = false)]
			[TestCase("BN20BN", ExpectedResult = false)]
			[TestCase("BN2  0BN", ExpectedResult = false)]
			[TestCase("EC1A 1BB", ExpectedResult = true)]
			[TestCase("", ExpectedResult = true)]
			[TestCase(" ", ExpectedResult = true)]
			public bool ShouldTestForValidity(string postcode)
			{
				var emp = new AddressRecord() { Postcode = postcode };
				return emp.IsValidPostCode();

			}
		}
	}
}