using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HML.Employee.Models.Entities
{
	[Table("employee.ClientSpecificFields")]
    public partial class ClientSpecificFieldRecord : BaseEntity, IEmployeeChild
	{
        public int EmployeeId { get; set; }

        public int ClientSpecificFieldsConfigId { get; set; }

        //[Required]
        [StringLength(500)]
        public string Value { get; set; }

        public virtual EmployeeRecord Employee { get; set; }
    }
}
