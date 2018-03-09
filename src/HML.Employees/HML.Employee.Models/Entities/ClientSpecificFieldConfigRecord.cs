using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HML.Employee.Models.Entities
{
	[Table("employee.ClientSpecificFieldsConfig")]
	public partial class ClientSpecificFieldConfigRecord : BaseEntity
	{

		[Required]
		[StringLength(50)]
		public string FieldName { get; set; }
		public Guid ClientId { get; set; }
		public int FieldTypeId { get; set; }
	}
}
