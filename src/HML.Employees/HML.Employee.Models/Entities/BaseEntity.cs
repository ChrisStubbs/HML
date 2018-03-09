using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HML.Employee.Models.Entities
{
	public abstract class BaseEntity : IBaseEntity
	{
		[Key]
		public int Id { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? CreateDate { get; set; }

		[StringLength(50)]
		public string CreatedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? UpdatedDate { get; set; }

		public bool IsTransient => Id == default(int);

		[StringLength(50)]
		public string UpdatedBy { get; set; }

		[Column(TypeName = "bit")]
		public bool IsDeleted { get; set; }
	}

}