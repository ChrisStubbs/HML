using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using HML.Employee.Common;

namespace HML.Employee.Models.Entities
{
	[Table("employee.Employees")]
	public partial class EmployeeRecord : BaseEntity
	{
		private const string DateFormat = "{0:dd MMM yyyy}";

		public EmployeeRecord()
		{
			Addresses = new HashSet<AddressRecord>();
			ClientSpecificFields = new HashSet<ClientSpecificFieldRecord>();
			Contacts = new HashSet<ContactRecord>();
			//Notes = new HashSet<NoteRecord>();
		}

		public Guid ClientId { get; set; }

		public Guid? DivisionId { get; set; }

		public Guid? LocationId { get; set; }

		public Guid? DepartmentId { get; set; }

		public Guid? CaseEmployeeId { get; set; }

		public string EmployeeId { get; set; }

		public string Title { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		[Column(TypeName = "date")]
		public DateTime? DateOfBirth { get; set; }

		[StringLength(100)]
		public string JobTitle { get; set; }

		[Column(TypeName = "date")]
		public DateTime? StartDate { get; set; }

		[Column(TypeName = "date")]
		public DateTime? EndDate { get; set; }

		public string NationalInsuranceNumber { get; set; }

		public bool IsShiftWorker { get; set; }

		public bool IsPartTime { get; set; }

		public bool IsNightWorker { get; set; }

		[Column(TypeName = "numeric")]
		public decimal? NoOfHoursWorked { get; set; }

		public bool IsActive { get; set; }

		public virtual ICollection<AddressRecord> Addresses { get; set; }

		public virtual ICollection<ContactRecord> Contacts { get; set; }

		//public virtual ICollection<NoteRecord> Notes { get; set; }

		public virtual ICollection<ClientSpecificFieldRecord> ClientSpecificFields { get; set; }

		public virtual bool IsEndDateGreaterThanStartDate()
		{
			if (EndDate.HasValue)
			{
				if (StartDate.HasValue)
				{
					return StartDate.Value <= EndDate.Value;
				}
				return true;
			}

			return true;
		}

		public virtual bool IsEndDateSetWhenUserIsActive()
		{
			if (EndDate.HasValue && IsActive)
			{
				return true;
			}

			return false;
		}

		public virtual bool IsValidNationalIsuranceNumber()
		{
			
			if (!string.IsNullOrWhiteSpace(NationalInsuranceNumber))
			{
				return Regex.IsMatch(this.NationalInsuranceNumber, RegexPatterns.NationalInsuranceNumber);
			}
			return true;
		}

		public virtual bool IsNoOfHoursWorkedValid()
		{
			if (NoOfHoursWorked.HasValue)
			{
				return NoOfHoursWorked > 0 && NoOfHoursWorked < 100;
			}
			return true;
		}
		
		public bool IsActiveToday => IsActiveOnDate(DateTime.Today);

		public bool IsActiveOnDate(DateTime date)
		{
			if (EndDate.HasValue)
			{
				return EndDate.Value.Date >= date.Date;
			}
			else
			{
				return IsActive;
			}
		}

	}
}
