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
            ClientDiseaseRisks = new HashSet<ClientDiseaseRiskRecord>();
        }

        public bool IsEnabled { get; set; }

        public virtual ICollection<ClientDiseaseRiskRecord> ClientDiseaseRisks { get; set; }
    }
}
