using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HML.Immunisation.Models.Entities
{
	[Table("Immunisation.ClientDiseaseRisks")]
    public partial class ClientDiseaseRiskRecord : BaseEntity<int>
	{
        public int DiseaseRiskId { get; set; }

        public Guid ClientId { get; set; }		

		public virtual ClientSettingsRecord ClientSetting { get; set; }
	}
}
