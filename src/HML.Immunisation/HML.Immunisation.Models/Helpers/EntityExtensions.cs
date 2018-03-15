using HML.Immunisation.Models.Entities;

namespace HML.Immunisation.Models.Helpers
{
	public static class EntityExtensions
	{
		public static bool HasChanges(this EmployeeDiseaseRiskStatusRecord x, EmployeeDiseaseRiskStatusRecord y)
		{
			return
				!(x.Id == y.Id
				  && x.EmployeeId == y.EmployeeId
				  && x.ClientId.Equals(y.ClientId)
				  && x.DiseaseRiskId == y.DiseaseRiskId
				  && x.IsRequired == y.IsRequired
				  && x.ImmunisationStatusId == y.ImmunisationStatusId
				  && x.DateProtected.Equals(y.DateProtected)
				  && x.RecallActionId == y.RecallActionId
				  && x.RecallDate.Equals(y.RecallDate)
				  && x.CurrentProgress == y.CurrentProgress
				  && x.CreateDate.Equals(y.CreateDate)
				  && string.Equals(x.CreatedBy, y.CreatedBy)
				  && x.UpdatedDate.Equals(y.UpdatedDate)
				  && string.Equals(x.UpdatedBy, y.UpdatedBy)
				  && x.IsDeleted == y.IsDeleted);
		}
	}

}
