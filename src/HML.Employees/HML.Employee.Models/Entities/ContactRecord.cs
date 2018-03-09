using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HML.Employee.Models.Entities
{
	[Table("employee.Contacts")]
	public partial class ContactRecord : BaseEntity, IEmployeeChild
	{
		public int EmployeeId { get; set; }

		[StringLength(255)]
		public string Value { get; set; }

		public virtual EmployeeRecord Employee { get; set; }

		public virtual ContactType ContactType { get; set; }
	}
}
