using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HML.Immunisation.Models.Entities
{
	[Table("Immunisation.EmployeeDiseaseRiskStatuses")]
	public partial class EmployeeDiseaseRiskStatusRecord : BaseEntity<int> 
	{
		public int EmployeeId { get; set; }

		public int DiseaseRiskId { get; set; }

		public bool IsRequired { get; set; }

		public short? ImmunisationStatusId { get; set; }

		public DateTime? DateProtected { get; set; }

		public short? RecallActionId { get; set; }

		public DateTime? RecallDate { get; set; }

		public short? CurrentProgress { get; set; }

		/// <summary>
		/// If Is required is set to false 
		/// then the Imms Status information for the disease risk cannot be 
		/// entered and will not be reported to the Client.
		/// </summary>
		public virtual bool IsImmsStatusIdSetWhenNotRequired()
		{
			if (!IsRequired && ImmunisationStatusId.HasValue)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Date Protected should not be set unless Immunisation status is Protected
		/// </summary>
		/// <returns></returns>
		public virtual bool IsDateProtectedSetWhenStatusNotProtected()
		{
			if (DateProtected.HasValue)
			{
				if (!ImmunisationStatusId.HasValue || ImmunisationStatusId != ImmunisationStatusRecord.Protected)
				{
					return true;
				}
			}
			return false;
		}

		public bool IsDateProtectedNullOrLessThanOrEqualToToday()
		{
			if (DateProtected.HasValue)
			{
				return DateProtected.Value.Date <= DateTime.Today;
			}
			return true;
		}

		public bool HasRecallDateChangedToValueInThePast(EmployeeDiseaseRiskStatusRecord originalRecord)
		{
			if (RecallDate.HasValue)
			{
				if (RecallDateHasChanged(originalRecord?.RecallDate))
				{
					return RecallDate.Value.Date < DateTime.Today;
				}
			}
			return false;
		}

		private bool RecallDateHasChanged(DateTime? originalRecordRecallDate)
		{
			return Nullable.Compare(originalRecordRecallDate, RecallDate) != 0;
		}
	}
}
