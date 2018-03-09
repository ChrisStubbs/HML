using System;
using System.Collections;
using HML.Immunisation.Models.Entities;
using NUnit.Framework;

namespace HML.Immunisation.Test.Models
{
	[TestFixture]
	public class EmployeeDiseaseRiskStatusRecordTest
	{
		public class TheIsImmsStatusIdSetWhenNotRequiredMethod : EmployeeDiseaseRiskStatusRecordTest
		{
			public static IEnumerable TestCases
			{
				get
				{
					yield return new TestCaseData(true, null).Returns(false);
					yield return new TestCaseData(true, (short)1).Returns(false);
					yield return new TestCaseData(false, null).Returns(false);
					yield return new TestCaseData(false, (short)1).Returns(true);
				}
			}

			[Test]
			[TestCaseSource(typeof(TheIsImmsStatusIdSetWhenNotRequiredMethod), nameof(TestCases))]
			public bool IsImmsStatusIdSetWhenNotRequired(bool isRequired, short? immsStatusId)
			{
				var status = new EmployeeDiseaseRiskStatusRecord { IsRequired = isRequired, ImmunisationStatusId = immsStatusId };
				return status.IsImmsStatusIdSetWhenNotRequired();
			}
		}

		public class TheIsDateProtectedSetWhenStatusNotProtectedMethod : EmployeeDiseaseRiskStatusRecordTest
		{
			public static IEnumerable TestCases
			{
				get
				{
					yield return new TestCaseData((short)1, null).Returns(false);
					yield return new TestCaseData((short)2, null).Returns(false);
					yield return new TestCaseData((short)3, null).Returns(false);
					yield return new TestCaseData(null, null).Returns(false);
					yield return new TestCaseData((short)1, DateTime.Now).Returns(false);
					yield return new TestCaseData((short)2, DateTime.Now).Returns(true);
					yield return new TestCaseData((short)3, DateTime.Now).Returns(true);
					yield return new TestCaseData(null, DateTime.Now).Returns(true);
				}
			}


			[Test]
			[TestCaseSource(typeof(TheIsDateProtectedSetWhenStatusNotProtectedMethod),nameof(TestCases))]
			public bool IsDateProtectedSetWhenStatusNotProtected(short? immsStatusId, DateTime? dateProtected)
			{
				var status = new EmployeeDiseaseRiskStatusRecord
				{
					ImmunisationStatusId = immsStatusId,
					DateProtected = dateProtected,

				};
				return status.IsDateProtectedSetWhenStatusNotProtected();
			}

		}
	}
}