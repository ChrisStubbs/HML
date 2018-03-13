using HML.Immunisation.Models.Entities;

namespace HML.Immunisation.Test.Factories
{
	public class EmployeeDiseaseRiskStatusRecordFactory : EntityFactory<EmployeeDiseaseRiskStatusRecordFactory, EmployeeDiseaseRiskStatusRecord>
	{
		public EmployeeDiseaseRiskStatusRecordFactory WithIdAndDiseaseRisk(int id,int diseaseRiskId)
		{
			this.With(x => x.Id = id);
			this.With(x => x.DiseaseRiskId = diseaseRiskId);
			return this;
		}
	}
}