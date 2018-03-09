using System;

namespace HML.Immunisation.Models.Entities
{
	public interface IEntityAudit
	{
		DateTime? CreateDate { get; set; }
		string CreatedBy { get; set; }
		string UpdatedBy { get; set; }
		DateTime? UpdatedDate { get; set; }
	}
}