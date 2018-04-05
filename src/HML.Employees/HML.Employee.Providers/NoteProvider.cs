using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HML.Employee.Common.Interfaces;
using HML.Employee.Models;
using HML.Employee.Models.Entities;
using HML.Employee.Providers.Interfaces;

namespace HML.Employee.Providers
{
	public class NoteProvider : INoteProvider
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger _logger;
		private readonly IUsernameProvider _usernameProvider;

		public virtual NoteContext GetDbContext()
		{
			var db = new NoteContext(_configuration, _usernameProvider, _logger);
			return db;
		}

		public NoteProvider(IConfiguration configuration, ILogger logger, IUsernameProvider usernameProvider)
		{
			_configuration = configuration;
			_logger = logger;
			_usernameProvider = usernameProvider;
		}

		public async Task<List<NoteRecord>> GetByIdAsync(int id)
		{
			try
			{
				using (var db = GetDbContext())
				{
					return await db.Notes
                        .Include(x => x.Employee)
                        .Where(x => x.EmployeeId==id).ToListAsync()
                        
						.ConfigureAwait(false);
				}

			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed whilst getting notes {id}", ex);
				throw;
			}
		}

		public async Task CreateAsync(NoteRecord note)
		{
			try
			{
				using (var db = GetDbContext())
				{
					db.Notes.Add(note);
					await db.SaveChangesAsync().ConfigureAwait(false);
				}

			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed whilst creating note", ex);
				throw;
			}
		}

		public async Task<NoteRecord> UpdateAsync(NoteRecord note)
		{
			try
			{
				using (var db = GetDbContext())
				{
                    var existing = await db.Notes.FindAsync(note.Id).ConfigureAwait(false);
					if (existing == null) return null;
					db.Configuration.AutoDetectChangesEnabled = true;
					db.Entry(existing).CurrentValues.SetValues(note);

					await db.SaveChangesAsync().ConfigureAwait(false);
					return existing;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed whilst creating note {note.Id}", ex);
				throw;
			}
		}
    }
}
