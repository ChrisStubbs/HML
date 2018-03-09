using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HML.Immunisation.Models.Entities
{
	[Table("Immunisation.RecallActions")]
    public partial class RecallActionRecord : BaseEntity<short> 
    {
       
        public RecallActionRecord()
        {
        }

		public short Order { get; set; }

		public int DiseaseRiskId { get; set; }
		[Required]

        [StringLength(100)]
        public string Description { get; set; }
    }
}

