using HML.Immunisation.Models.ViewModels;

namespace HML.Immunisation.Test.Factories
{
	public class RecallActionLookupFactory : EntityFactory<RecallActionLookupFactory, RecallActionLookup>
	{
		public RecallActionLookupFactory WithKeyOrderDiseaseRiskId(short key, short order, int diseaseRiskId)
		{
			this.With(x => x.Key = key);
			this.With(x => x.Order = order);
			this.With(x => x.DiseaseRiskId = diseaseRiskId);
			return this;
		}
	}
}