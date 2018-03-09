using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HML.HealthSurveillance.Models.Entities
{
	[Table("HealthSurveillance.ClientSettings")]
    public partial class ClientSettingsRecord : BaseEntity<Guid>
	{
        public bool IsEnabled { get; set; }

    }
}
