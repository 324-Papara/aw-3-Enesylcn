using FluentValidation;
using Para.Schema;

public class CustomerPhoneValidator : AbstractValidator<CustomerPhoneRequest>
{
    public CustomerPhoneValidator()
    {
        RuleFor(x => x.CountyCode)
            .NotEmpty().WithMessage("Country code is required.")
            .MaximumLength(5).WithMessage("Country code cannot exceed 5 characters.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .MaximumLength(15).WithMessage("Phone number cannot exceed 15 characters.");

        RuleFor(x => x.IsDefault)
            .NotNull().WithMessage("IsDefault is required.");
    }
}
