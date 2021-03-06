﻿using System;
using System.Collections.Generic;

namespace HML.Employee.Models.Entities
{
	public interface IBaseEntity
	{
		DateTime? CreateDate { get; set; }
		int Id { get; set; }
		bool IsDeleted { get; set; }
		string CreatedBy { get; set; }
		string UpdatedBy { get; set; }
		DateTime? UpdatedDate { get; set; }
		bool IsTransient { get; }
	}
}