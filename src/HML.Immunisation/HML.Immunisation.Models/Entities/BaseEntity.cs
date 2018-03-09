using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HML.Immunisation.Models.Entities
{
	public abstract class BaseEntity<TPrimaryKey> : IEntityAudit
	{
		[Key]
		public TPrimaryKey Id { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? CreateDate { get; set; }

		[StringLength(50)]
		public string CreatedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? UpdatedDate { get; set; }

		public bool IsTransient => EqualityComparer<TPrimaryKey>.Default.Equals(Id, default(TPrimaryKey));

		[StringLength(50)]
		public string UpdatedBy { get; set; }

		[Column(TypeName = "bit")]
		public bool IsDeleted { get; set; }
	}

}