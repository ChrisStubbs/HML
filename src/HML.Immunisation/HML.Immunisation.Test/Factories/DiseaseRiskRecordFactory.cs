using HML.Immunisation.Models.Entities;

namespace HML.Immunisation.Test.Factories
{
	public class DiseaseRiskRecordFactory: EntityFactory<DiseaseRiskRecordFactory, DiseaseRiskRecord>
	{
		public DiseaseRiskRecordFactory WithIdDescriptionAndCode(int id, string description, string code)
		{
			this.With(x => x.Id = id);
			this.With(x => x.Description = description);
			this.With(x => x.Code = code);
			return this;
		}
	}
}