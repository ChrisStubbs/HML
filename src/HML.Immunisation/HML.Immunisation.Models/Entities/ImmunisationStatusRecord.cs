using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HML.Immunisation.Models.Entities
{
	[Table("Immunisation.ImmunisationStatuses")]
    public partial class ImmunisationStatusRecord  : BaseEntity<short>
	{
        public ImmunisationStatusRecord()
        {
        }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }
		public static short Protected => 1;
		public static short NotProtected => 2;
		public static short Declined => 3;

	}
}
