using FluentValidation;
using HML.Employee.Models.Entities;

namespace HML.Employee.WebAPI.Validators
{
	public class AddressRecordValidator : AbstractValidator<AddressRecord>
	{
		public AddressRecordValidator()
		{
			RuleFor(e => e.Lines).Length(1, 500).WithMessage("Address Lines: are required with a max length of 500");
			RuleFor(e => e.Postcode)
				.Must((record, postcode) => record.IsValidPostCode())
				.WithMessage((record) => $"Postcode: {record.Postcode} is not a valid postcode");
		}
	}
}
