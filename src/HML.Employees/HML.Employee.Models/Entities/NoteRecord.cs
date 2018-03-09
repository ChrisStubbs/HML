using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HML.Employee.Models.Entities
{
	[Table("employee.Notes")]
	public partial class NoteRecord : BaseEntity, IEmployeeChild
	{
		public int EmployeeId { get; set; }

		[Required]
		public string Text { get; set; }

		public virtual EmployeeRecord Employee { get; set; }

	}
}
