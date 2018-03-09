using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HML.Immunisation.Models.Entities
{
	[Table("Immunisation.DiseaseRisks")]
    public partial class DiseaseRiskRecord : BaseEntity<int>
    {
        public DiseaseRiskRecord()
        {
        }

        [Required]
        [StringLength(5)]
        public string Code { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
	
    }
}
