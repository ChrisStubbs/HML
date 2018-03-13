using System.Collections.Generic;
using System.Linq;
using System.Threading;
using HML.Immunisation.Models.Entities;

namespace HML.Immunisation.Models.ViewModels
{
	public class Lookups
	{
		public Lookups()
		{
			ImmunisationProgress = new List<KeyValuePair<short, string>>();
			ImmunisationStatuses = new List<KeyValuePair<short, string>>();
			RecallActions = new List<RecallActionLookup>();
		}
		public Lookups(IEnumerable<ImmunisationProgressRecord> progresses, IEnumerable<ImmunisationStatusRecord> statuses, IEnumerable<RecallActionRecord> recallActionRecords)
		{
			ImmunisationProgress = progresses.Select(x => new KeyValuePair<short, string>(x.Id, x.Description)).ToList();
			ImmunisationStatuses = statuses.Select(x => new KeyValuePair<short, string>(x.Id, x.Description)).ToList();
			RecallActions = recallActionRecords.Select(x => new RecallActionLookup
			{
				Key = x.Id,
				Value = x.Description,
				Order = x.Order,
				DiseaseRiskId = x.DiseaseRiskId
			}).ToList();
		}

		public IList<KeyValuePair<short, string>> ImmunisationProgress { get; set; }
		public IList<KeyValuePair<short, string>> ImmunisationStatuses { get; set; }
		public IList<RecallActionLookup> RecallActions { get; set; }
	}

}