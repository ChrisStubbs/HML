using System;
using System.Collections;
using HML.Employee.Models.Entities;
using NUnit.Framework;

namespace HML.Employee.Test.Models
{
	[TestFixture]
	public class EmployeeRecordTests
	{
		public class TheIsValidNationalIsuranceNumberMethod : EmployeeRecordTests
		{

			[Test]
			[TestCase("NX548432D", ExpectedResult = true)]
			[TestCase("NX548432A", ExpectedResult = true)]
			[TestCase("NX548432B", ExpectedResult = true)]
			[TestCase("NX548432C", ExpectedResult = true)]
			[TestCase("NX548432D", ExpectedResult = true)]
			[TestCase("NX548432E", ExpectedResult = false)]
			[TestCase("NXY48432D", ExpectedResult = false)]
			[TestCase("NX548432DY", ExpectedResult = false)]
			[TestCase("nx548432d", ExpectedResult = false)]
			[TestCase(null, ExpectedResult = true)]
			[TestCase("", ExpectedResult = true)]
			[TestCase(" ", ExpectedResult = true)]
			public bool ShouldTestForValidity(string niNumber)
			{
				var emp = new EmployeeRecord() { NationalInsuranceNumber = niNumber };
				return emp.IsValidNationalIsuranceNumber();

			}
		}

		public class TheIsEndDateGreaterThanStartDateMethod : EmployeeRecordTests
		{
			public class TestData
			{
				public static IEnumerable TestCases
				{
					get
					{
						yield return new TestCaseData(DateTime.MaxValue, DateTime.MinValue).Returns(false);
						yield return new TestCaseData(DateTime.MinValue, DateTime.MaxValue).Returns(true);
						yield return new TestCaseData(DateTime.MaxValue, DateTime.MaxValue).Returns(true);
						yield return new TestCaseData(null, DateTime.MaxValue).Returns(true);
						yield return new TestCaseData(DateTime.MaxValue, null).Returns(true);
					}
				}
			}

			[Test]
			[TestCaseSource(typeof(TestData), nameof(TestData.TestCases))]
			public bool ShouldTestForValidity(DateTime? startDate, DateTime? endDate)
			{
				var emp = new EmployeeRecord() { StartDate = startDate, EndDate = endDate };
				return emp.IsEndDateGreaterThanStartDate();

			}
		}

		public class TheIsNoOfHoursWorkedValidMethod : EmployeeRecordTests
		{
			[Test]
			[TestCase(null, ExpectedResult = true)]
			[TestCase(0, ExpectedResult = false)]
			[TestCase(1, ExpectedResult = true)]
			[TestCase(1.8, ExpectedResult = true)]
			[TestCase(101, ExpectedResult = false)]
			public bool ShouldTestForValidity(decimal? noOfHours)
			{
				var emp = new EmployeeRecord() { NoOfHoursWorked = noOfHours };
				return emp.IsNoOfHoursWorkedValid();
			}
		}

		public class TheIsEmployeeEndDateSetWhenEmployeeIsActiveMethod : EmployeeRecordTests
		{

			public class TestData
			{
				public static IEnumerable TestCases
				{
					get
					{
						yield return new TestCaseData(DateTime.Now, true).Returns(true);
						yield return new TestCaseData(DateTime.Now, false).Returns(false);
						yield return new TestCaseData(null, true).Returns(false);
						yield return new TestCaseData(null, false).Returns(false);
					}
				}
			}


			[Test]
			[TestCaseSource(typeof(TestData), nameof(TestData.TestCases))]
			public bool IsEndDateSetWhenUserIsActiveTest(DateTime? endDate, bool isActive)
			{
				var emp = new EmployeeRecord() { EndDate = endDate, IsActive = isActive };
				return emp.IsEndDateSetWhenUserIsActive();

			}
		}

		public class TheIsActiveOnDateMethod : EmployeeRecordTests
		{
			public class TestData
			{
				public static IEnumerable TestCases
				{
					get
					{
						yield return new TestCaseData(new EmployeeRecord { EndDate = DateTime.MaxValue, IsActive = false},DateTime.Today).Returns(true);
						yield return new TestCaseData(new EmployeeRecord { EndDate = DateTime.MinValue, IsActive = false }, DateTime.Today).Returns(false);
						yield return new TestCaseData(new EmployeeRecord { EndDate = null, IsActive = false }, DateTime.Today).Returns(false);
						yield return new TestCaseData(new EmployeeRecord { EndDate = null, IsActive = true }, DateTime.Today).Returns(true);
						yield return new TestCaseData(new EmployeeRecord { EndDate = DateTime.MinValue, IsActive = true }, DateTime.Today).Returns(false);
					}
				}
			}

			[Test]
			[TestCaseSource(typeof(TestData), nameof(TestData.TestCases))]
			public bool IsActiveTests(EmployeeRecord emp, DateTime activeOnDate)
			{

				return emp.IsActiveOnDate(activeOnDate);
			}
		}
	}
}
