using System.Threading.Tasks;
using HML.Employee.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HML.Employee.Models;

namespace HML.Employee.Providers.Interfaces
{
	public interface INoteProvider
	{
        Task<List<NoteRecord>> GetByIdAsync(int id);
		Task CreateAsync(NoteRecord note);
		Task<NoteRecord> UpdateAsync(NoteRecord note);
		
	}
}