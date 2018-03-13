namespace HML.Immunisation.Models.ViewModels
{
	public class RecallActionLookup
	{
		public RecallActionLookup()
		{
			
		}
		public RecallActionLookup(short key, string value)
		{
			Key = key;
			Value = value;
		}

		public short Key { get; set; }
		public string Value { get; set; }
		public short Order { get; set; }
		public int DiseaseRiskId { get; set; }

		public string DisplayValue => $"{Order} - {Value}";
	}
}