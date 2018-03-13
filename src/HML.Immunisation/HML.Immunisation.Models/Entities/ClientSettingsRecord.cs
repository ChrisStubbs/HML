using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HML.Immunisation.Models.Entities
{
	[Table("Immunisation.ClientSettings")]
    public partial class ClientSettingsRecord : BaseEntity<Guid>
    {
        public ClientSettingsRecord()
        {
            ClientDiseaseRisks = new List<ClientDiseaseRiskRecord>();
        }

        public bool IsEnabled { get; set; }

        public virtual IList<ClientDiseaseRiskRecord> ClientDiseaseRisks { get; set; }
    }
}
