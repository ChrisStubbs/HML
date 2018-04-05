using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HML.Employee.Models.Entities
{
	[Table("employee.Imports")]
	public class ImportRecord : BaseEntity
	{
		public ImportRecord()
		{
			Employees = new HashSet<EmployeeRecord>();
		}
		
		public string Source { get; set; }

		public virtual ICollection<EmployeeRecord> Employees { get; set; }
	}
}