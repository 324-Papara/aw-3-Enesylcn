using FluentValidation;
using Para.Schema;

public class CustomerDetailValidator : AbstractValidator<CustomerDetailRequest>
{
    public CustomerDetailValidator()
    {
        RuleFor(x => x.FatherName)
            .NotEmpty().WithMessage("Father's name is required.")
            .MaximumLength(50).WithMessage("Father's name cannot exceed 50 characters.");

        RuleFor(x => x.MotherName)
            .NotEmpty().WithMessage("Mother's name is required.")
            .MaximumLength(50).WithMessage("Mother's name cannot exceed 50 characters.");

        RuleFor(x => x.EducationStatus)
            .NotEmpty().WithMessage("Education status is required.")
            .MaximumLength(50).WithMessage("Education status cannot exceed 50 characters.");

        RuleFor(x => x.MontlyIncome)
            .NotEmpty().WithMessage("Monthly income is required.")
            .MaximumLength(50).WithMessage("Monthly income cannot exceed 50 characters.");

        RuleFor(x => x.Occupation)
            .NotEmpty().WithMessage("Occupation is required.")
            .MaximumLength(50).WithMessage("Occupation cannot exceed 50 characters.");
    }
}
