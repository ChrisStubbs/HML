using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using HML.Employee.Common;

namespace HML.Employee.Models.Entities
{
	[Table("employee.Addresses")]
	public partial class AddressRecord : BaseEntity, IEmployeeChild
	{
		public int EmployeeId { get; set; }

		[Column("Address")]
		public string Lines { get; set; }
		public string Postcode { get; set; }

		public virtual EmployeeRecord Employee { get; set; }

		public AddressType AddressType { get; set; }

		public virtual bool IsValidPostCode()
		{
			if (!string.IsNullOrWhiteSpace(Postcode))
			{
				return Regex.IsMatch(this.Postcode, RegexPatterns.Postcode);
			}
			return true;
		}
	}
}
