using System;
using HML.Immunisation.Models.Entities;

namespace HML.Immunisation.Models.ViewModels
{
	public class EmployeeDiseaseRiskStatus : IEntityAudit
	{
		public int Id { get; set; }

		public Guid ClientId { get; set; }

		public int EmployeeId { get; set; }

		public int DiseaseRiskId { get; set; }

		public string DiseaseRiskCode { get; set; }

		public string DiseaseRiskDescription { get; set; }

		public bool IsRequired { get; set; }

		public short? ImmunisationStatusId { get; set; }

		public string ImmunisationStatusDisplayValue { get; set; }

		public DateTime? DateProtected { get; set; }

		public short? RecallActionId { get; set; }

		public string RecallActionDisplayValue { get; set; }

		public DateTime? RecallDate { get; set; }

		public short? CurrentProgress { get; set; }

		public string CurrentProgressDisplayValue { get; set; }

		public DateTime? CreateDate { get; set; }

		public string CreatedBy { get; set; }

		public string UpdatedBy { get; set; }

		public DateTime? UpdatedDate { get; set; }

		public bool IsDeleted { get; set; }
	}
}