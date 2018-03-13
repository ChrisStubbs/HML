using System;

namespace HML.Immunisation.Models.ViewModels
{
	public class AuditInfo
	{
		public DateTime? CreateDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public string UpdatedBy { get; set; }
		public bool IsDeleted { get; set; }
	}
}
