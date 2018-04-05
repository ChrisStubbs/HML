using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HML.Employee.Models.Entities
{
	[Table("employee.Notes")]
	public partial class NoteRecord : BaseEntity, IEmployeeChild
	{
		public int EmployeeId { get; set; }

        [Required]
        public string Comment { get; set; }

        public string DocumentName { get; set; }

        public string DocumentLocation { get; set; }

        public int? InvuUniqueId { get; set; }

        public bool? Pinned { get; set; }

        public bool? IsClinical { get; set; }

        public int? invuDocTypeId { get; set; }

        [StringLength(255)]
        public string FileType { get; set; }


        public virtual EmployeeRecord Employee { get; set; }


    }
}
