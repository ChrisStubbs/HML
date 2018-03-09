using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HML.Immunisation.Models.Entities
{
	[Table("Immunisation.ImmunisationProgress")]
    public partial class ImmunisationProgressRecord : BaseEntity<short>
    {
        
        public ImmunisationProgressRecord()
        {
        }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }
		
    }
}
